Imports SkyEditor.Core.Projects

Namespace ViewModels.Projects
    Public Class SolutionHeiarchyItemViewModel
        Inherits ProjectBaseHeiarchyItemViewModel

        Public Sub New(solution As Solution)
            MyBase.New(solution)
            SetPrefix()
        End Sub

        Public Sub New(solution As Solution, parent As SolutionHeiarchyItemViewModel, path As String)
            MyBase.New(solution, parent, path)
            SetPrefix()
        End Sub

        Private Sub SetPrefix()
            If IsDirectory Then
                Prefix = My.Resources.Language.DirectoryPrefix
            Else
                Prefix = My.Resources.Language.ProjectPrefix
            End If
        End Sub

        Protected Property ProjectRootNode As ProjectHeiarchyItemViewModel

        Public Shadows Property Project() As Solution
            Get
                Return MyBase.Project
            End Get
            Protected Set(value As Solution)
                MyBase.Project = value
            End Set
        End Property

        Protected Overrides Sub PopulateChildren()
            If IsDirectory Then
                MyBase.PopulateChildren()
            Else
                Dim solution As Solution = Me.Project
                ProjectRootNode = New ProjectHeiarchyItemViewModel(solution.GetProject(Me.CurrentPath))
                Me.Children = ProjectRootNode.Children
            End If
        End Sub

        Protected Overrides Function CreateNode(project As ProjectBase, path As String) As ProjectBaseHeiarchyItemViewModel
            Return New SolutionHeiarchyItemViewModel(project, Me, path)
        End Function

        Public Function GetNodeProject() As Project
            Dim solution As Solution = Me.Project
            Return solution.GetProject(Me.CurrentPath)
        End Function

        Public Overrides Function CanDeleteCurrentNode() As Boolean
            If Me.IsDirectory Then
                Return Project.CanDeleteDirectory(CurrentPath)
            Else
                Return Project.CanDeleteProject(CurrentPath)
            End If
        End Function

        Public Overrides Sub RemoveCurrentNode()
            If CanDeleteCurrentNode() Then
                If IsDirectory Then
                    Project.DeleteDirectory(CurrentPath)
                Else
                    Project.DeleteProject(CurrentPath)
                End If
            End If
        End Sub
    End Class
End Namespace
