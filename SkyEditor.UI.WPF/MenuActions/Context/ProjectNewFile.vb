Imports System.Reflection
Imports SkyEditor.Core
Imports SkyEditor.Core.Projects
Imports SkyEditor.Core.UI
Imports SkyEditor.Core.Utilities
Imports SkyEditor.UI.WPF.ViewModels.Projects

Namespace MenuActions.Context
    Public Class ProjectNewFile
        Inherits MenuAction

        Public Sub New(pluginManager As PluginManager, applicationViewModel As ApplicationViewModel)
            MyBase.New({My.Resources.Language.MenuCreateFile})
            IsContextBased = True

            CurrentApplicationViewModel = applicationViewModel
            CurrentPluginManager = pluginManager
        End Sub

        Public Property CurrentApplicationViewModel As ApplicationViewModel

        Public Property CurrentPluginManager As PluginManager

        Public Overrides Sub DoAction(targets As IEnumerable(Of Object))
            For Each item In targets
                Dim currentPath As String
                Dim parentProject As Project
                If TypeOf item Is SolutionHeiarchyItemViewModel Then
                    currentPath = ""
                    parentProject = DirectCast(item, SolutionHeiarchyItemViewModel).GetNodeProject
                ElseIf TypeOf item Is ProjectHeiarchyItemViewModel Then
                    currentPath = DirectCast(item, ProjectHeiarchyItemViewModel).CurrentPath
                    parentProject = DirectCast(item, ProjectHeiarchyItemViewModel).Project
                Else
                    Throw New ArgumentException(String.Format(My.Resources.Language.ErrorUnsupportedType, item.GetType.Name))
                End If
                Dim w As New NewFileWindow
                Dim types As New Dictionary(Of String, Type)
                For Each supported In parentProject.GetSupportedFileTypes(currentPath, CurrentPluginManager)
                    types.Add(ReflectionHelpers.GetTypeFriendlyName(supported), supported)
                Next
                w.SetGames(types)
                If w.ShowDialog Then
                    parentProject.CreateFile(currentPath, w.SelectedName, w.SelectedType)
                End If
            Next
        End Sub

        Public Overrides Function GetSupportedTypes() As IEnumerable(Of TypeInfo)
            Return {GetType(SolutionHeiarchyItemViewModel).GetTypeInfo, GetType(ProjectHeiarchyItemViewModel).GetTypeInfo}
        End Function

        Public Overrides Function SupportsObject(obj As Object) As Task(Of Boolean)
            If TypeOf obj Is ProjectHeiarchyItemViewModel Then
                Dim node As ProjectHeiarchyItemViewModel = obj
                Return Task.FromResult(node.IsDirectory AndAlso node.Project.CanCreateFile(node.CurrentPath))
            ElseIf TypeOf obj Is SolutionHeiarchyItemViewModel Then
                Dim node As SolutionHeiarchyItemViewModel = obj
                Return Task.FromResult(Not node.IsDirectory AndAlso node.GetNodeProject.CanCreateFile(""))
            Else
                Return Task.FromResult(False)
            End If
        End Function

    End Class
End Namespace

