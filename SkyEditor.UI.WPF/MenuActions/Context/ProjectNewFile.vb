Imports System.Reflection
Imports SkyEditor.Core.Projects
Imports SkyEditor.Core.UI
Imports SkyEditor.Core.Utilities
Imports SkyEditor.UI.WPF.ViewModels.Projects

Namespace MenuActions.Context
    Public Class ProjectNewFile
        Inherits MenuAction

        Public Overrides Sub DoAction(targets As IEnumerable(Of Object))
            For Each item In Targets
                Dim currentPath As String
                Dim parentProject As Project
                If TypeOf item Is SolutionHeiarchyItemViewModel Then
                    CurrentPath = ""
                    ParentProject = DirectCast(item, SolutionHeiarchyItemViewModel).GetNodeProject
                ElseIf TypeOf item Is ProjectHeiarchyItemViewModel Then
                    CurrentPath = DirectCast(item, ProjectHeiarchyItemViewModel).CurrentPath
                    ParentProject = DirectCast(item, ProjectHeiarchyItemViewModel).Project
                Else
                    Throw New ArgumentException(String.Format(My.Resources.Language.ErrorUnsupportedType, item.GetType.Name))
                End If
                Dim w As New NewFileWindow
                Dim types As New Dictionary(Of String, Type)
                For Each supported In ParentProject.GetSupportedFileTypes(CurrentPath, CurrentPluginManager)
                    types.Add(ReflectionHelpers.GetTypeFriendlyName(supported), supported)
                Next
                w.SetGames(types)
                If w.ShowDialog Then
                    ParentProject.CreateFile(CurrentPath, w.SelectedName, w.SelectedType)
                End If
            Next
        End Sub

        Public Overrides Function SupportedTypes() As IEnumerable(Of TypeInfo)
            Return {GetType(SolutionHeiarchyItemViewModel).GetTypeInfo, GetType(ProjectHeiarchyItemViewModel).GetTypeInfo}
        End Function

        Public Overrides Function SupportsObject(obj As Object) As Task(Of Boolean)
            If TypeOf Obj Is ProjectHeiarchyItemViewModel Then
                Dim node As ProjectHeiarchyItemViewModel = Obj
                Return Task.FromResult(node.IsDirectory AndAlso node.Project.CanCreateFile(node.CurrentPath))
            ElseIf TypeOf Obj Is SolutionHeiarchyItemViewModel Then
                Dim node As SolutionHeiarchyItemViewModel = Obj
                Return Task.FromResult(Not node.IsDirectory AndAlso node.GetNodeProject.CanCreateFile(""))
            Else
                Return Task.FromResult(False)
            End If
        End Function

        Public Sub New()
            MyBase.New({My.Resources.Language.MenuCreateFile})
            IsContextBased = True
        End Sub
    End Class
End Namespace

