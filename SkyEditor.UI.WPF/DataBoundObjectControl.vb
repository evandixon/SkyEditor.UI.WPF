Imports System.ComponentModel
Imports System.Reflection
Imports System.Windows
Imports System.Windows.Controls
Imports SkyEditor.Core
Imports SkyEditor.Core.UI
Imports SkyEditor.Core.Utilities

''' <summary>
''' A view that directly supports WPF DataBinding.
''' </summary>
Public Class DataBoundViewControl
    Inherits UserControl
    Implements IViewControl
    Implements INotifyPropertyChanged

#Region "Dependency Properties"
    Public Shared ReadOnly HeaderProperty As DependencyProperty = DependencyProperty.Register(NameOf(Header), GetType(Object), GetType(DataBoundViewControl), New FrameworkPropertyMetadata(AddressOf OnHeaderChanged))
    Private Shared Sub OnHeaderChanged(d As DependencyObject, e As DependencyPropertyChangedEventArgs)
        DirectCast(d, DataBoundViewControl).Header = e.NewValue
    End Sub
#End Region

#Region "Events"
    ''' <summary>
    ''' Raised when Header is changed.
    ''' </summary>
    Public Event HeaderUpdated As EventHandler(Of HeaderUpdatedEventArgs) Implements IViewControl.HeaderUpdated

    ''' <summary>
    ''' Raised when IsModified is changed.
    ''' </summary>
    Public Event IsModifiedChanged As EventHandler Implements IViewControl.IsModifiedChanged

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
#End Region

#Region "Properties"
    ''' <summary>
    ''' Returns the value of the Header.  Only used when the iObjectControl is behaving as a tab.
    ''' </summary>
    ''' <returns></returns>
    Public Property Header As String Implements IViewControl.Header
        Get
            Return _header
        End Get
        Set(value As String)
            If Not _header = value Then
                _header = value
                RaiseEvent HeaderUpdated(Me, New HeaderUpdatedEventArgs With {.NewValue = value})
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Header)))
            End If
        End Set
    End Property
    Dim _header As String

    ''' <summary>
    ''' Whether or not the EditingObject has been modified without saving.
    ''' Set to true when the user changes anything in the GUI.
    ''' Set to false when the object is saved, or if the user undoes every change.
    ''' </summary>
    ''' <returns></returns>
    Public Property IsModified As Boolean Implements IViewControl.IsModified
        Get
            Return _isModified
        End Get
        Set(value As Boolean)
            If Not value = _isModified Then
                Dim oldValue As Boolean = _isModified
                _isModified = value
                RaiseEvent IsModifiedChanged(Me, New EventArgs)
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(IsModified)))
            End If
        End Set
    End Property
    Dim _isModified As Boolean

    ''' <summary>
    ''' The object the control edits
    ''' </summary>
    ''' <returns></returns>
    Public Overridable Property ViewModel As Object Implements IViewControl.ViewModel
        Get
            Return Me.DataContext
        End Get
        Set(value As Object)
            Me.DataContext = value
        End Set
    End Property

    ''' <summary>
    ''' Returns the sort order of this control when editing the given type.
    ''' Note: The returned value is context-specific.  Higher values make a Control more likely to be used, but lower values make tabs appear higher in the list of tabs.
    ''' </summary>
    Public Property SortOrder As Integer

    ''' <summary>
    ''' The instance of the current application ViewModel
    ''' </summary>
    Public Property CurrentApplicationViewModel As ApplicationViewModel

    ''' <summary>
    ''' The type of the object to edit
    ''' </summary>
    Public Property TargetType As Type

    ''' <summary>
    ''' If True, this control will not be used if another one exists.
    ''' </summary>
    Public Property IsBackupControl As Boolean
#End Region

    ''' <summary>
    ''' Returns an IEnumeriable of Types that this control can display or edit.
    ''' </summary>
    ''' <returns></returns>
    Public Overridable Function GetSupportedTypes() As IEnumerable(Of TypeInfo) Implements IViewControl.GetSupportedTypes
        If TargetType IsNot Nothing Then
            Return {TargetType}
        Else
            Return {}
        End If
    End Function

    ''' <summary>
    ''' Determines whether or not the control supports the given object.
    ''' The given object will inherit or implement one of the types in GetSupportedTypes.
    ''' </summary>
    ''' <param name="Obj"></param>
    ''' <returns></returns>
    Public Overridable Function SupportsObject(Obj As Object) As Boolean Implements IViewControl.SupportsObject
        Return True
    End Function

    ''' <summary>
    ''' If True, this control will not be used if another one exists.
    ''' </summary>
    ''' <returns></returns>
    Public Overridable Function GetIsBackupControl() As Boolean Implements IViewControl.GetIsBackupControl
        Return IsBackupControl
    End Function

    Public Overridable Function GetSortOrder(currentType As TypeInfo, isTab As Boolean) As Integer Implements IViewControl.GetSortOrder
        Return SortOrder
    End Function

    Public Sub SetApplicationViewModel(appViewModel As ApplicationViewModel) Implements IViewControl.SetApplicationViewModel
        CurrentApplicationViewModel = appViewModel
    End Sub

End Class

