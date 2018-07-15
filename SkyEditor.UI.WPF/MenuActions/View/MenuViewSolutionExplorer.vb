Imports SkyEditor.Core.Projects
Imports SkyEditor.Core.UI
Imports SkyEditor.Core.Settings
Imports SkyEditor.UI.WPF.ViewModels
Imports SkyEditor.Core

Namespace MenuActions.View
    Public Class MenuViewSolutionExplorer
        Inherits MenuAction

        Public Sub New(pluginManager As PluginManager, applicationViewModel As ApplicationViewModel)
            MyBase.New({My.Resources.Language.MenuView, My.Resources.Language.SolutionExplorerToolWindowTitle})

            CurrentApplicationViewModel = applicationViewModel
            CurrentPluginManager = pluginManager
            AlwaysVisible = (CurrentPluginManager.GetRegisteredObjects(Of Solution).Count() > 1 OrElse CurrentPluginManager.CurrentSettingsProvider.GetIsDevMode)
            SortOrder = 3.1
        End Sub

        Public Property CurrentApplicationViewModel As ApplicationViewModel

        Public Property CurrentPluginManager As PluginManager

        Public Overrides Sub DoAction(Targets As IEnumerable(Of Object))
            CurrentApplicationViewModel.ShowAnchorable(New SolutionExplorerViewModel(CurrentApplicationViewModel, CurrentPluginManager))
        End Sub

    End Class
End Namespace

