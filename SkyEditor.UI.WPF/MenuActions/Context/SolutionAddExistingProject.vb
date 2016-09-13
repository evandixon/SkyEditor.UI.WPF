Imports System.Reflection
Imports System.Windows.Forms
Imports SkyEditor.Core.Projects
Imports SkyEditor.Core.UI
Imports SkyEditor.UI.WPF.ViewModels.Projects

Namespace MenuActions.Context
    Public Class SolutionAddExistingProject
        Inherits MenuAction

        Public Overrides Sub DoAction(Targets As IEnumerable(Of Object))
            For Each item In Targets
                Dim ParentSolution As Solution
                Dim ParentPath As String

                If TypeOf item Is SolutionHeiarchyItemViewModel Then
                    ParentSolution = DirectCast(item, SolutionHeiarchyItemViewModel).Project
                    ParentPath = DirectCast(item, SolutionHeiarchyItemViewModel).CurrentPath
                Else
                    Throw New ArgumentException(String.Format(My.Resources.Language.ErrorUnsupportedType, item.GetType.Name))
                End If

                Dim w As New OpenFileDialog
                w.Filter = CurrentPluginManager.CurrentIOUIManager.GetProjectIOFilter

                If w.ShowDialog = DialogResult.OK Then
                    ParentSolution.AddExistingProject(ParentPath, w.FileName, CurrentPluginManager)
                End If
            Next
        End Sub

        Public Overrides Function SupportedTypes() As IEnumerable(Of TypeInfo)
            Return {GetType(SolutionHeiarchyItemViewModel).GetTypeInfo}
        End Function

        Public Overrides Function SupportsObject(Obj As Object) As Boolean
            If TypeOf Obj Is SolutionHeiarchyItemViewModel Then
                Dim node As SolutionHeiarchyItemViewModel = Obj
                Return node.Project.CanCreateProject(node.CurrentPath)
            Else
                Return False
            End If
        End Function

        Public Sub New()
            MyBase.New({My.Resources.Language.MenuAddProject})
            IsContextBased = True
        End Sub
    End Class
End Namespace

