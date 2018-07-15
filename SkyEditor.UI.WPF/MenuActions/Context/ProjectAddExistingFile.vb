Imports System.Reflection
Imports System.Windows.Forms
Imports SkyEditor.Core
Imports SkyEditor.Core.Projects
Imports SkyEditor.Core.UI
Imports SkyEditor.UI.WPF.ViewModels.Projects

Namespace MenuActions.Context
    Public Class ProjectAddExistingFile
        Inherits MenuAction

        Public Sub New(pluginManager As PluginManager, applicationViewModel As ApplicationViewModel)
            MyBase.New({My.Resources.Language.MenuAddFile})
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

                Dim w As New OpenFileDialog
                w.Filter = CurrentApplicationViewModel.GetIOFilter(parentProject.GetSupportedImportFileExtensions(currentPath))

                If w.ShowDialog = DialogResult.OK Then
                    Dim fileAddTask = Task.Run(Sub() parentProject.AddExistingFile(currentPath, w.FileName, CurrentPluginManager.CurrentIOProvider))
                    CurrentApplicationViewModel.ShowLoading(fileAddTask, My.Resources.Language.LoadingCopyingFile)
                End If
            Next
        End Sub

        Public Overrides Function GetSupportedTypes() As IEnumerable(Of TypeInfo)
            Return {GetType(SolutionHeiarchyItemViewModel).GetTypeInfo, GetType(ProjectHeiarchyItemViewModel).GetTypeInfo}
        End Function

        Public Overrides Function SupportsObject(obj As Object) As Task(Of Boolean)
            If TypeOf obj Is ProjectHeiarchyItemViewModel Then
                Dim node As ProjectHeiarchyItemViewModel = obj
                Return Task.FromResult(node.IsDirectory AndAlso node.Project.CanImportFile(node.CurrentPath))
            ElseIf TypeOf obj Is SolutionHeiarchyItemViewModel Then
                Dim node As SolutionHeiarchyItemViewModel = obj
                Return Task.FromResult(Not node.IsDirectory AndAlso node.GetNodeProject.CanImportFile(""))
            Else
                Return Task.FromResult(False)
            End If
        End Function

    End Class
End Namespace

