Imports SkyEditor.Core.Projects

Namespace ViewModels.Projects
    Public Class SolutionHeiarchyItemViewModel
        Inherits ProjectBaseHeiarchyItemViewModel

        Public Sub New(solution As Solution)
            MyBase.New(solution)
        End Sub

        Public Sub New(solution As Solution, parent As SolutionHeiarchyItemViewModel, path As String)
            MyBase.New(solution, parent, path)
        End Sub

        Protected Property ProjectRootNode As ProjectHeiarchyItemViewModel

        Protected Overrides Sub PopulateChildren()
            If IsDirectory Then
                MyBase.PopulateChildren()
            Else
                Dim solution As Solution = Me.Project
                ProjectRootNode = New ProjectHeiarchyItemViewModel(solution.GetProjectByPath(Me.CurrentPath))
                Me.Children = ProjectRootNode.Children
            End If
        End Sub
    End Class
End Namespace
