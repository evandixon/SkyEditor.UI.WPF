Imports System.Reflection
Imports SkyEditor.Core.IO
Imports SkyEditor.Core.Projects
Imports SkyEditor.Core.UI
Imports SkyEditor.UI.WPF.ViewModels.Projects

Namespace MenuActions.Context
    Public Class SolutionProjectAddFolder
        Inherits MenuAction

        Public Overrides Sub DoAction(Targets As IEnumerable(Of Object))
            For Each obj In Targets
                Dim w As New NewNameWindow(My.Resources.Language.NewFolderQuestion, My.Resources.Language.NewFolder)
                If w.ShowDialog Then
                    Dim node As ProjectBaseHeiarchyItemViewModel = obj
                    node.Project.CreateDirectory(w.SelectedName)
                End If
            Next
        End Sub

        Public Overrides Function SupportedTypes() As IEnumerable(Of TypeInfo)
            Return {GetType(ProjectBaseHeiarchyItemViewModel).GetTypeInfo}
        End Function

        Public Overrides Function SupportsObject(Obj As Object) As Boolean
            If TypeOf Obj Is ProjectBaseHeiarchyItemViewModel Then
                Dim node As ProjectBaseHeiarchyItemViewModel = Obj
                Return node.Project.CanCreateDirectory(node.CurrentPath)
            Else
                Return False
            End If
        End Function

        Public Sub New()
            MyBase.New({My.Resources.Language.ContextMenuAddFolder})
            Me.IsContextBased = True
        End Sub
    End Class

End Namespace
