Imports SkyEditor.Core
Imports SkyEditor.Core.IO
Imports SkyEditor.Core.Projects

Namespace ViewModels.Projects
    Public Class ProjectHeiarchyItemViewModel
        Inherits ProjectBaseHeiarchyItemViewModel

        Public Sub New(solution As Project)
            MyBase.New(solution)
            SetPrefix()
        End Sub

        Public Sub New(solution As Project, parent As ProjectHeiarchyItemViewModel, path As String)
            MyBase.New(solution, parent, path)
            SetPrefix()
        End Sub

        Private Sub SetPrefix()
            If IsDirectory Then
                Prefix = My.Resources.Language.DirectoryPrefix
            Else
                Prefix = String.Empty
            End If
        End Sub

        Public Shadows Property Project() As Project
            Get
                Return MyBase.Project
            End Get
            Protected Set(value As Project)
                MyBase.Project = value
            End Set
        End Property

        Protected Overrides Function CreateNode(project As ProjectBase, path As String) As ProjectBaseHeiarchyItemViewModel
            Return New ProjectHeiarchyItemViewModel(project, Me, path)
        End Function

        Public Function GetFilename() As String
            Dim project As Project = Me.Project
            Return project.GetFilename(CurrentPath)
        End Function

        Public Async Function GetFile(manager As PluginManager, duplicateMatchSelector As IOHelper.DuplicateMatchSelector) As Task(Of Object)
            Dim project As Project = Me.Project
            Return Await project.GetFileByPath(CurrentPath, manager, duplicateMatchSelector)
        End Function

        Public Overrides Function CanDeleteCurrentNode() As Boolean
            If Me.IsDirectory Then
                Return Project.CanDeleteDirectory(CurrentPath)
            Else
                Return Project.CanDeleteFile(CurrentPath)
            End If
        End Function

        Public Overrides Sub RemoveCurrentNode()
            If CanDeleteCurrentNode() Then
                If IsDirectory Then
                    Project.DeleteDirectory(CurrentPath)
                Else
                    Project.DeleteFile(CurrentPath)
                End If
            End If
        End Sub
    End Class
End Namespace
