Imports System.ComponentModel
Imports System.Windows
Imports System.Windows.Input
Imports SkyEditor.Core
Imports SkyEditor.Core.Projects
Imports SkyEditor.UI.WPF.MenuActions.Context
Imports SkyEditor.UI.WPF.ViewModels
Imports SkyEditor.UI.WPF.ViewModels.Projects

Namespace ObjectControls
    Public Class SolutionExplorer

        Public Sub New(applicationViewModel As ApplicationViewModel, pluginManager As PluginManager, settingsProvider As ISettingsProvider)

            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            menuContext = New TargetedContextMenu(applicationViewModel, pluginManager, settingsProvider)
            tvSolutions.ContextMenu = menuContext
        End Sub

        Protected menuContext As TargetedContextMenu

        Private Async Sub tvSolutions_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles tvSolutions.MouseDoubleClick
            If tvSolutions.SelectedItem IsNot Nothing AndAlso TypeOf tvSolutions.SelectedItem Is ProjectHeiarchyItemViewModel Then
                Await DirectCast(ViewModel, SolutionExplorerViewModel).OpenNode(tvSolutions.SelectedItem)
            End If
        End Sub

        Private Sub tvSolutions_SelectedItemChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Object)) Handles tvSolutions.SelectedItemChanged
            menuContext.Target = tvSolutions.SelectedItem
        End Sub
    End Class

End Namespace
