Imports System.Reflection
Imports SkyEditor.Core
Imports SkyEditor.Core.Projects
Imports SkyEditor.Core.UI
Imports SkyEditor.Core.Utilities
Imports SkyEditor.UI.WPF.ViewModels.Projects

Namespace MenuActions.Context
    Public Class SolutionCreateProject
        Inherits MenuAction

        Public Sub New(pluginManager As PluginManager, applicationViewModel As ApplicationViewModel)
            MyBase.New({My.Resources.Language.MenuCreateProject})
            IsContextBased = True

            CurrentApplicationViewModel = applicationViewModel
            CurrentPluginManager = pluginManager
        End Sub

        Public Property CurrentApplicationViewModel As ApplicationViewModel

        Public Property CurrentPluginManager As PluginManager

        Public Overrides Sub DoAction(targets As IEnumerable(Of Object))
            For Each item In targets
                Dim parentSolution As Solution
                Dim parentPath As String

                If TypeOf item Is SolutionHeiarchyItemViewModel Then
                    parentSolution = DirectCast(item, SolutionHeiarchyItemViewModel).Project
                    parentPath = DirectCast(item, SolutionHeiarchyItemViewModel).CurrentPath
                Else
                    Throw New ArgumentException(String.Format(My.Resources.Language.ErrorUnsupportedType, item.GetType.Name))
                End If

                Dim w As New NewFileWindow
                Dim types As New Dictionary(Of String, Type)
                For Each supported In parentSolution.GetSupportedProjectTypes(parentPath, CurrentPluginManager)
                    types.Add(ReflectionHelpers.GetTypeFriendlyName(supported), supported)
                Next
                w.SetGames(types)

                If w.ShowDialog Then
                    CurrentApplicationViewModel.ShowLoading(DirectCast(item, SolutionHeiarchyItemViewModel).Project.AddNewProject(parentPath, w.SelectedName, w.SelectedType, CurrentPluginManager))
                End If
            Next
        End Sub

        Public Overrides Function GetSupportedTypes() As IEnumerable(Of TypeInfo)
            Return {GetType(SolutionHeiarchyItemViewModel).GetTypeInfo}
        End Function

        Public Overrides Function SupportsObject(obj As Object) As Task(Of Boolean)
            If TypeOf obj Is SolutionHeiarchyItemViewModel Then
                Dim node As SolutionHeiarchyItemViewModel = obj
                Return Task.FromResult(node.IsDirectory AndAlso node.Project.CanCreateProject(node.CurrentPath))
            Else
                Return Task.FromResult(False)
            End If
        End Function

    End Class
End Namespace

