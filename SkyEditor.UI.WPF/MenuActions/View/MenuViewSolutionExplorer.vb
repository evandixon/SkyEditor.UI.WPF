Imports SkyEditor.Core.IO
Imports SkyEditor.Core.UI
Imports SkyEditor.UI.WPF.ViewModels

Namespace MenuActions.View
    Public Class MenuViewSolutionExplorer
        Inherits MenuAction

        Public Sub New()
            MyBase.New({My.Resources.Language.MenuView, My.Resources.Language.SolutionExplorerToolWindowTitle})
            AlwaysVisible = True
            SortOrder = 3.1
        End Sub

        Public Overrides Sub DoAction(Targets As IEnumerable(Of Object))
            CurrentPluginManager.CurrentIOUIManager.ShowAnchorable(New SolutionExplorerViewModel)
        End Sub

        Private Sub FileNewSolution_CurrentPluginManagerChanged(sender As Object, e As EventArgs) Handles Me.CurrentPluginManagerChanged
            Me.AlwaysVisible = CurrentPluginManager IsNot Nothing AndAlso CurrentPluginManager.GetRegisteredObjects(Of Solution).Count() > 1
        End Sub
    End Class
End Namespace

