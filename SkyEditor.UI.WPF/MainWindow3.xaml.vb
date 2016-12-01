Imports System.ComponentModel
Imports System.Globalization
Imports System.Reflection
Imports System.Windows
Imports SkyEditor.Core
Imports SkyEditor.Core.IO
Imports SkyEditor.Core.Projects
Imports SkyEditor.UI.WPF.Settings

Public Class MainWindow3
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Title = String.Format(CultureInfo.InvariantCulture, My.Resources.Language.FormattedTitle, My.Resources.Language.VersionPrefix, Assembly.GetEntryAssembly.GetName.Version.ToString)

        AddHandler SkyEditor.Core.Windows.Utilities.RedistributionHelpers.ApplicationRestartRequested, AddressOf OnRestartRequested
    End Sub

#Region "Properties"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>Currently, this window only supports plugin managers that have a WPFIOUIManager as the CurrentIOUIManager</remarks>
    Public Property CurrentPluginManager As PluginManager
        Get
            Return _currentPluginManager
        End Get
        Set(value As PluginManager)
            If _currentPluginManager?.CurrentIOUIManager IsNot Nothing Then
                RemoveHandler _currentPluginManager.CurrentIOUIManager.FileClosing, AddressOf OnIOUIManagerFileClosing
            End If

            _currentPluginManager = value

            If _currentPluginManager?.CurrentIOUIManager IsNot Nothing Then
                AddHandler _currentPluginManager.CurrentIOUIManager.FileClosing, AddressOf OnIOUIManagerFileClosing
            End If
        End Set
    End Property
    Dim _currentPluginManager As PluginManager

    ''' <summary>
    ''' If true, application will be restarted when the form is closed.
    ''' </summary>
    ''' <returns></returns>
    Private Property RestartOnExit As Boolean
#End Region

#Region "Event Handlers"
    Private Sub OnIOUIManagerFileClosing(sender As Object, e As FileClosingEventArgs)
        If e.File.IsFileModified Then
            e.Cancel = Not (MessageBox.Show(My.Resources.Language.DocumentCloseConfirmation, My.Resources.Language.MainTitle, MessageBoxButton.YesNo) = MessageBoxResult.Yes)
        End If
    End Sub

    Private Async Sub MainWindow3_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        With CurrentPluginManager.CurrentSettingsProvider
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
        menuMain.ItemsSource = Await CurrentPluginManager.CurrentIOUIManager.GetRootMenuItems
    End Sub

    Private Sub MainWindow3_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If CurrentPluginManager.CurrentIOUIManager.OpenFiles.Where(Function(x) x.IsFileModified).Any Then
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
        With CurrentPluginManager.CurrentSettingsProvider
            If Not Me.WindowState = WindowState.Maximized Then
                'Setting width and height while maximized results in the window being the same size when restored
                .SetMainWindowHeight(Me.Height)
                .SetMainWindowWidth(Me.Width)
            End If

            .SetMainWindowIsMaximized(Me.WindowState = WindowState.Maximized)
            .Save(CurrentPluginManager.CurrentIOProvider)
        End With
    End Sub

    Private Sub OnRestartRequested(sender As Object, e As EventArgs)
        RestartOnExit = True
        Me.Close()
    End Sub

    Private Sub MainWindow3_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        If RestartOnExit Then
            CurrentPluginManager.Dispose()
            Forms.Application.Restart()
            Process.GetCurrentProcess().Kill()
        End If
    End Sub
#End Region

End Class
