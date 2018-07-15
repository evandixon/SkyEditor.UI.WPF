Imports System.Windows
Imports System.Windows.Controls
Imports SkyEditor.Core
Imports SkyEditor.Core.UI
Imports SkyEditor.Core.Settings

Public Class TargetedContextMenu
    Inherits ContextMenu

    Public Shared ReadOnly CurrentApplicationViewModelProperty As DependencyProperty = DependencyProperty.Register(NameOf(CurrentApplicationViewModel), GetType(ApplicationViewModel), GetType(TargetedContextMenu), New FrameworkPropertyMetadata(AddressOf OnCurrentPluginManagerChanged))
    Public Shared ReadOnly ObjectToEditProperty As DependencyProperty = DependencyProperty.Register(NameOf(Target), GetType(Object), GetType(TargetedContextMenu), New FrameworkPropertyMetadata(AddressOf OnTargetChanged))
    Private Shared Sub OnCurrentPluginManagerChanged(d As DependencyObject, e As DependencyPropertyChangedEventArgs)
        DirectCast(d, ObjectControlPlaceholder).CurrentApplicationViewModel = e.NewValue
    End Sub
    Private Shared Sub OnTargetChanged(d As DependencyObject, e As DependencyPropertyChangedEventArgs)
        DirectCast(d, TargetedContextMenu).Target = e.NewValue
    End Sub

    Public Sub New(applicationViewModel As ApplicationViewModel, pluginManager As PluginManager, settingsProvider As ISettingsProvider)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        CurrentApplicationViewModel = applicationViewModel
        CurrentPluginManager = pluginManager
        CurrentSettingsProvider = settingsProvider
    End Sub

    Public Property Target As Object
        Get
            Return _target
        End Get
        Set(value As Object)
            _target = value
            UpdateDataContext()
        End Set
    End Property
    Dim _target As Object

    Protected Property CurrentApplicationViewModel As ApplicationViewModel
    Protected Property CurrentPluginManager As PluginManager
    Protected Property CurrentSettingsProvider As ISettingsProvider

    Private Async Sub UpdateDataContext()
        Dim actions = UIHelper.GenerateLogicalMenuItems(Await UIHelper.GetContextMenuItemInfo(_target, CurrentApplicationViewModel, CurrentPluginManager, CurrentPluginManager.CurrentSettingsProvider.GetIsDevMode), CurrentApplicationViewModel, CurrentPluginManager, {Target})
        Me.DataContext = actions
    End Sub

End Class
