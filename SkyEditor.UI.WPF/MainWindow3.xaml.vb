Imports System.ComponentModel
Imports System.Globalization
Imports System.Reflection
Imports System.Windows
Imports System.Windows.Input
Imports SkyEditor.Core
Imports SkyEditor.Core.IO
Imports SkyEditor.Core.Projects
Imports SkyEditor.Core.UI
Imports SkyEditor.Core.Utilities
Imports SkyEditor.UI.WPF.Settings

Public Class MainWindow3
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Dim versionAssembly = Assembly.GetEntryAssembly
        If versionAssembly Is Nothing Then
            versionAssembly = Assembly.GetExecutingAssembly
        End If
        Me.Title = String.Format(CultureInfo.InvariantCulture, My.Resources.Language.FormattedTitle, My.Resources.Language.VersionPrefix, versionAssembly.GetName.Version.ToString)
    End Sub

#Region "Properties"
    Public Property DisplayStatusBar As Boolean
        Get
            Return statusBarRow.Height.Value > 0
        End Get
        Set(value As Boolean)
            If value Then
                statusBarRow.Height = New GridLength(30)
            Else
                statusBarRow.Height = New GridLength(0)
            End If
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>Currently, this window only supports plugin managers that have a WPFIOUIManager as the CurrentIOUIManager</remarks>
    Public Property CurrentApplicationViewModel As WPFApplicationViewModel
        Get
            Return _currentApplicationViewModel
        End Get
        Set(value As WPFApplicationViewModel)
            _currentApplicationViewModel = value
            DataContext = value
        End Set
    End Property
    Private WithEvents _currentApplicationViewModel As WPFApplicationViewModel

    ''' <summary>
    ''' If true, application will be restarted when the form is closed.
    ''' </summary>
    ''' <returns></returns>
    Private Property RestartOnExit As Boolean
#End Region

#Region "Event Handlers"

    Private Sub OnIOUIManagerFileClosing(sender As Object, e As FileClosingEventArgs) Handles _currentApplicationViewModel.FileClosing
        If e.File.IsFileModified Then
            e.Cancel = Not (MessageBox.Show(My.Resources.Language.DocumentCloseConfirmation, My.Resources.Language.MainTitle, MessageBoxButton.YesNo) = MessageBoxResult.Yes)
        End If
    End Sub

    Private Async Sub MainWindow3_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If My.Computer.Keyboard.CtrlKeyDown Then
            For Each item In CurrentApplicationViewModel.CurrentPluginManager.GetRegisteredObjects(Of ControlKeyAction)
                item.CurrentApplicationViewModel = CurrentApplicationViewModel
                If item.Keys.All(Function(x) e.KeyboardDevice.IsKeyDown(x)) Then
                    Await item.DoAction
                End If
            Next
        End If
    End Sub

    Private Async Sub MainWindow3_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        With CurrentApplicationViewModel.CurrentPluginManager.CurrentSettingsProvider
            Dim height = .GetMainWindowHeight
            Dim width = .GetMainWindowWidth
            Dim isMax = .GetMainWindowIsMaximized

            If height.HasValue Then
                Me.Height = height.Value
            End If

            If width.HasValue Then
                Me.Width = width.Value
            End If

            If isMax Then
                Me.WindowState = WindowState.Maximized
            Else
                Me.WindowState = WindowState.Normal
            End If

        End With
        menuMain.ItemsSource = Await CurrentApplicationViewModel.GetRootMenuItems
    End Sub

    Private Sub MainWindow3_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If CurrentApplicationViewModel.OpenFiles.Where(Function(x) x.IsFileModified).Any Then
            Dim closeConfirmation = MessageBox.Show(My.Resources.Language.MainWindowCloseConfirmation, My.Resources.Language.MainTitle, MessageBoxButton.YesNo)
            If closeConfirmation <> MessageBoxResult.Yes Then
                'Don't close the window
                e.Cancel = True
                RestartOnExit = False
            Else
                'Close like normal
            End If
        End If

        'Save the settings
        With CurrentApplicationViewModel.CurrentPluginManager.CurrentSettingsProvider
            If Not Me.WindowState = WindowState.Maximized Then
                'Setting width and height while maximized results in the window being the same size when restored
                .SetMainWindowHeight(Me.Height)
                .SetMainWindowWidth(Me.Width)
            End If

            .SetMainWindowIsMaximized(Me.WindowState = WindowState.Maximized)
            .Save(CurrentApplicationViewModel.CurrentPluginManager.CurrentIOProvider)
        End With
    End Sub

    Private Sub MainWindow3_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        If RestartOnExit Then
            Forms.Application.Restart()
            Process.GetCurrentProcess().Kill()
        End If
    End Sub

    Private Sub dockingManager_Drop(sender As Object, e As DragEventArgs) Handles dockingManager.Drop
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            Dim files = e.Data.GetData(DataFormats.FileDrop)
            For Each file In files
                CurrentApplicationViewModel.OpenFile(file, AddressOf IOHelper.PickFirstDuplicateMatchSelector)
            Next
        End If
    End Sub

    Private Sub _currentApplicationViewModel_RestartRequested(sender As Object, e As EventArgs) Handles _currentApplicationViewModel.RestartRequested
        RestartOnExit = True
        Me.Close()
    End Sub

#End Region

End Class
