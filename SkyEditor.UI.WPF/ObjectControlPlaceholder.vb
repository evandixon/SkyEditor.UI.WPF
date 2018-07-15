Imports System.ComponentModel
Imports System.Globalization
Imports System.Windows
Imports System.Windows.Controls
Imports SkyEditor.Core
Imports SkyEditor.Core.IO
Imports SkyEditor.Core.UI
Imports SkyEditor.Core.Utilities

Public Class ObjectControlPlaceholder
    Inherits UserControl
    Implements IDisposable

    Public Shared ReadOnly CurrentApplicationViewModelProperty As DependencyProperty = DependencyProperty.Register(NameOf(CurrentApplicationViewModel), GetType(ApplicationViewModel), GetType(ObjectControlPlaceholder), New FrameworkPropertyMetadata(AddressOf OnCurrentApplicationViewModelChanged))
    Public Shared ReadOnly ObjectToEditProperty As DependencyProperty = DependencyProperty.Register(NameOf(ObjectToEdit), GetType(Object), GetType(ObjectControlPlaceholder), New FrameworkPropertyMetadata(AddressOf OnObjectToEditChanged))
    Private Shared Sub OnCurrentApplicationViewModelChanged(d As DependencyObject, e As DependencyPropertyChangedEventArgs)
        DirectCast(d, ObjectControlPlaceholder).CurrentApplicationViewModel = e.NewValue
    End Sub

    Private Shared Sub OnObjectToEditChanged(d As DependencyObject, e As DependencyPropertyChangedEventArgs)
        DirectCast(d, ObjectControlPlaceholder).ObjectToEdit = e.NewValue
    End Sub

    Public Sub New()
        MyBase.New
        Me.TabControlOrientation = Dock.Left
    End Sub


    ''' <summary>
    ''' Raised when the contained object raises its Modified event, if it implements iModifiable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>

    Public Event Modified(sender As Object, e As EventArgs)

    Private Sub ObjectToEditFVM_Changed(sender As Object, e As PropertyChangedEventArgs)
        If e.PropertyName = NameOf(FileViewModel.Model) Then
            'Refresh the UI
            ObjectToEdit = _object
        End If
    End Sub

    Private Sub ObjectControlPlaceholder_Unloaded(sender As Object, e As RoutedEventArgs) Handles Me.Unloaded
        ClearEventHandlers()
        _object = Nothing
    End Sub

    ''' <summary>
    ''' Whether or not to enable tabs.
    ''' When true, multiple object controls will be used
    ''' </summary>
    ''' <returns></returns>
    Public Property EnableTabs As Boolean

    ''' <summary>
    ''' Gets or sets the underlying view model
    ''' </summary>
    ''' <returns></returns>
    Public Property ObjectToEdit As Object
        Get
            If TypeOf Me.Content Is IViewControl Then
                _object = DirectCast(Me.Content, IViewControl).ViewModel
            ElseIf TypeOf Me.Content Is TabControl Then
                For Each item In DirectCast(Me.Content, TabControl).Items
                    If TypeOf item.Content Is IViewControl Then
                        _object = DirectCast(item.Content, IViewControl).ViewModel
                    End If
                Next
            End If
            Return _object
        End Get
        Set(value As Object)
            If CurrentApplicationViewModel Is Nothing Then
                _pendingObject = value
            Else
                ClearEventHandlers()

                _object = value

                If TypeOf value Is INotifyModified Then
                    AddHandler DirectCast(value, INotifyModified).Modified, AddressOf OnModified
                End If
                If TypeOf value Is FileViewModel Then
                    AddHandler DirectCast(value, FileViewModel).PropertyChanged, AddressOf ObjectToEditFVM_Changed
                End If

                If EnableTabs Then
                    'Tab control if applicable
                    If value IsNot Nothing Then
                        Dim tabs = UIHelper.GetViewControlTabs(value, {GetType(UserControl)}, CurrentApplicationViewModel, CurrentApplicationViewModel.CurrentPluginManager)
                        Dim count = tabs.Count '- (From t In ucTabs Where t.GetSortOrder(value.GetType, True) < 0).Count
                        If count > 1 Then
                            Dim tabControl As New TabControl
                            tabControl.TabStripPlacement = TabControlOrientation
                            For Each item In WPFUiHelper.GenerateObjectTabs(tabs)
                                tabControl.Items.Add(item)
                                AddHandler item.ContainedViewControl.IsModifiedChanged, AddressOf OnModified
                            Next
                            Me.Content = tabControl

                        ElseIf count = 1 Then
                            Dim control = tabs.First '(From t In ucTabs Where t.GetSortOrder(value.GetType, True) >= 0).First
                            Me.Content = control
                            AddHandler control.IsModifiedChanged, AddressOf OnModified
                        Else
                            'Nothing is registered to edit this object.
                            Dim label As New Label
                            label.Content = String.Format(CultureInfo.InvariantCulture, My.Resources.Language.NoAvailableUI, value.GetType.FullName)
                            Me.Content = label
                        End If
                    End If

                Else
                    'Always one control
                    Dim objControl = UIHelper.GetViewControl(value, {GetType(UserControl)}, CurrentApplicationViewModel, CurrentApplicationViewModel.CurrentPluginManager)
                    If objControl IsNot Nothing Then
                        Content = objControl
                    Else
                        'Nothing is registered to edit this object.
                        Dim label As New Label
                        label.Content = String.Format(CultureInfo.InvariantCulture, My.Resources.Language.NoAvailableUI, value.GetType.FullName)
                        Me.Content = label
                    End If
                End If
            End If
        End Set
    End Property
    Dim _object As Object

    Public Property TabControlOrientation As Dock

    Public Property CurrentApplicationViewModel As ApplicationViewModel
        Get
            Return _currentApplicationViewModel
        End Get
        Set(value As ApplicationViewModel)
            _currentApplicationViewModel = value
            If _pendingObject IsNot Nothing Then
                ObjectToEdit = _pendingObject
            End If
        End Set
    End Property
    Dim _currentApplicationViewModel As ApplicationViewModel
    Dim _pendingObject As Object

    Private Sub ObjectControlPlaceholder_DataContextChanged(sender As Object, e As DependencyPropertyChangedEventArgs) Handles Me.DataContextChanged
        'If CurrentPluginManager IsNot Nothing Then
        '    'Then the plugin manager has been set and we're good to go.
        '    If TypeOf e.NewValue Is ContentPresenter Then
        '        ObjectToEdit = DirectCast(e.NewValue, ContentPresenter).Content
        '    Else
        '        ObjectToEdit = e.NewValue
        '    End If
        'Else
        '    'The plugin manager hasn't been set yet.  Let's log the object we would use, and use it when the plugin manager is set
        '    If TypeOf e.NewValue Is ContentPresenter Then
        '        _pendingObject = DirectCast(e.NewValue, ContentPresenter).Content
        '    Else
        '        _pendingObject = e.NewValue
        '    End If
        'End If
    End Sub

    Private Sub ClearEventHandlers()
        If _object IsNot Nothing AndAlso TypeOf _object Is FileViewModel Then
            RemoveHandler DirectCast(_object, FileViewModel).PropertyChanged, AddressOf ObjectToEditFVM_Changed
        End If
        If _object IsNot Nothing AndAlso TypeOf _object Is INotifyModified Then
            RemoveHandler DirectCast(_object, INotifyModified).Modified, AddressOf OnModified
        End If
    End Sub

    Private Sub OnModified(sender As Object, e As EventArgs)
        RaiseEvent Modified(sender, e)
    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ClearEventHandlers()
                If _object IsNot Nothing AndAlso TypeOf _object Is IDisposable Then
                    DirectCast(_object, IDisposable).Dispose()
                End If
            End If
        End If
        disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        ' TODO: uncomment the following line if Finalize() is overridden above.
        ' GC.SuppressFinalize(Me)
    End Sub

#End Region

End Class