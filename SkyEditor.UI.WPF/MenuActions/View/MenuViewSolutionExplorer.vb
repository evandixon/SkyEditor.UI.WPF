Imports SkyEditor.Core.UI
Imports SkyEditor.UI.WPF.ViewModels

Namespace MenuActions.View
    Public Class MenuViewSolutionExplorer
        Inherits MenuAction

        Public Overrides Sub DoAction(Targets As IEnumerable(Of Object))
            CurrentPluginManager.CurrentIOUIManager.ShowAnchorable(New SolutionExplorerViewModel)
        End Sub

        Public Sub New()
            MyBase.New({My.Resources.Language.MenuView, My.Resources.Language.SolutionExplorerToolWindowTitle})
            AlwaysVisible = True
            SortOrder = 3.1
        End Sub
    End Class
End Namespace

