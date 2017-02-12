Imports System.Reflection
Imports SkyEditor.Core.UI
Imports SkyEditor.UI.WPF.ViewModels.Projects

Namespace MenuActions.Context
    Public Class SolutionProjectAddFolder
        Inherits MenuAction

        Public Overrides Sub DoAction(targets As IEnumerable(Of Object))
            For Each obj In Targets
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
            If TypeOf Obj Is ProjectBaseHeiarchyItemViewModel Then
                Dim node As ProjectBaseHeiarchyItemViewModel = Obj
                Return Task.FromResult(node.Project.CanCreateDirectory(node.CurrentPath))
            Else
                Return Task.FromResult(False)
            End If
        End Function

        Public Sub New()
            MyBase.New({My.Resources.Language.ContextMenuAddFolder})
            IsContextBased = True
        End Sub
    End Class

End Namespace
