Imports System.Reflection
Imports System.Windows
Imports SkyEditor.Core.UI
Imports SkyEditor.UI.WPF.ViewModels.Projects

Namespace MenuActions.Context
    Public Class SolutionProjectDelete
        Inherits MenuAction

        Public Overrides Sub DoAction(targets As IEnumerable(Of Object))
            For Each item In Targets
                If TypeOf item Is ProjectBaseHeiarchyItemViewModel Then
                    If MessageBox.Show(My.Resources.Language.DeleteItemConfirmation, My.Resources.Language.MainTitle, MessageBoxButton.YesNo) = MessageBoxResult.Yes Then
                        DirectCast(item, ProjectBaseHeiarchyItemViewModel).RemoveCurrentNode()
                    End If
                End If
            Next
        End Sub

        Public Overrides Function GetSupportedTypes() As IEnumerable(Of TypeInfo)
            Return {GetType(SolutionHeiarchyItemViewModel).GetTypeInfo, GetType(ProjectHeiarchyItemViewModel).GetTypeInfo}
        End Function

        Public Overrides Function SupportsObject(obj As Object) As Task(Of Boolean)
            If TypeOf Obj Is ProjectBaseHeiarchyItemViewModel Then
                Return Task.FromResult(DirectCast(Obj, ProjectBaseHeiarchyItemViewModel).CanDeleteCurrentNode)
            Else
                Return Task.FromResult(False)
            End If
        End Function

        Public Sub New()
            MyBase.New({My.Resources.Language.MenuDelete})
            IsContextBased = True
        End Sub
    End Class
End Namespace