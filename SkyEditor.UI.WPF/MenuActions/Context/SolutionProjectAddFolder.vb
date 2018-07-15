Imports System.Reflection
Imports SkyEditor.Core
Imports SkyEditor.Core.UI
Imports SkyEditor.UI.WPF.ViewModels.Projects

Namespace MenuActions.Context
    Public Class SolutionProjectAddFolder
        Inherits MenuAction

        Public Sub New(pluginManager As PluginManager, applicationViewModel As ApplicationViewModel)
            MyBase.New({My.Resources.Language.ContextMenuAddFolder})
            IsContextBased = True

            CurrentPluginManager = pluginManager
        End Sub

        Public Property CurrentPluginManager As PluginManager

        Public Overrides Sub DoAction(targets As IEnumerable(Of Object))
            For Each obj In targets
                Dim w As New NewNameWindow(My.Resources.Language.NewFolderQuestion, My.Resources.Language.NewFolder)
                If w.ShowDialog Then
                    Dim node As ProjectBaseHeiarchyItemViewModel = obj
                    node.Project.CreateDirectory(node.CurrentPath & "/" & w.SelectedName)
                End If
            Next
        End Sub

        Public Overrides Function GetSupportedTypes() As IEnumerable(Of TypeInfo)
            Return {GetType(ProjectBaseHeiarchyItemViewModel).GetTypeInfo}
        End Function

        Public Overrides Function SupportsObject(obj As Object) As Task(Of Boolean)
            If TypeOf obj Is ProjectBaseHeiarchyItemViewModel Then
                Dim node As ProjectBaseHeiarchyItemViewModel = obj
                Return Task.FromResult(node.Project.CanCreateDirectory(node.CurrentPath))
            Else
                Return Task.FromResult(False)
            End If
        End Function

    End Class

End Namespace
