Imports SkyEditor.Core.Projects
Imports SkyEditor.Core.UI
Imports SkyEditor.Core.Settings
Imports SkyEditor.UI.WPF.ViewModels
Imports SkyEditor.Core

Namespace MenuActions.View
    Public Class MenuViewSolutionBuildProgress
        Inherits MenuAction

        Public Sub New(pluginManager As PluginManager, applicationViewModel As ApplicationViewModel)
            MyBase.New({My.Resources.Language.MenuView, My.Resources.Language.MenuViewSolutionBuildProgress})

            CurrentApplicationViewModel = applicationViewModel
            CurrentPluginManager = pluginManager
            AlwaysVisible = CurrentPluginManager.GetRegisteredObjects(Of Solution).Count() > 1 OrElse CurrentPluginManager.CurrentSettingsProvider.GetIsDevMode
            SortOrder = 3.2
        End Sub

        Public Property CurrentApplicationViewModel As ApplicationViewModel

        Public Property CurrentPluginManager As PluginManager

        Public Overrides Sub DoAction(Targets As IEnumerable(Of Object))
            CurrentApplicationViewModel.ShowAnchorable(New SolutionBuildProgressViewModel(CurrentApplicationViewModel))
        End Sub

    End Class
End Namespace

