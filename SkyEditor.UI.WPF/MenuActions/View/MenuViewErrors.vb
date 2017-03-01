Imports SkyEditor.Core.Projects
Imports SkyEditor.Core.UI
Imports SkyEditor.UI.WPF.ViewModels

Namespace MenuActions.View
    Public Class MenuViewErrors
        Inherits MenuAction

        Public Sub New()
            MyBase.New({My.Resources.Language.MenuView, My.Resources.Language.MenuViewApplicationErrors})
            AlwaysVisible = True
            SortOrder = 3.3
        End Sub

        Public Overrides Sub DoAction(Targets As IEnumerable(Of Object))
            CurrentApplicationViewModel.ShowAnchorable(New ApplicationErrors)
        End Sub

        Private Sub FileNewSolution_CurrentPluginManagerChanged(sender As Object, e As EventArgs) Handles Me.CurrentApplicationViewModelChanged
            Me.AlwaysVisible = CurrentApplicationViewModel IsNot Nothing
        End Sub
    End Class
End Namespace
