Imports SkyEditor.Core.UI
Imports SkyEditor.UI.WPF.ViewModels

Namespace MenuActions.View
    Public Class MenuViewSolutionBuildProgress
        Inherits MenuAction

        Public Overrides Sub DoAction(Targets As IEnumerable(Of Object))
            CurrentPluginManager.CurrentIOUIManager.ShowAnchorable(New SolutionBuildProgressViewModel)
        End Sub

        Public Sub New()
            MyBase.New({My.Resources.Language.MenuView, My.Resources.Language.MenuViewSolutionBuildProgress})
            AlwaysVisible = True
            SortOrder = 3.2
        End Sub
    End Class
End Namespace

