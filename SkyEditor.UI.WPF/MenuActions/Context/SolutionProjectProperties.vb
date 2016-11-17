Imports System.Reflection
Imports SkyEditor.Core.UI
Imports SkyEditor.UI.WPF.ViewModels.Projects

Namespace MenuActions
    Public Class SolutionProjectProperties
        Inherits MenuAction

        Public Overrides Sub DoAction(Targets As IEnumerable(Of Object))
            For Each item In Targets
                If TypeOf item Is SolutionHeiarchyItemViewModel Then
                    Dim target As SolutionHeiarchyItemViewModel = item
                    If target.IsRoot Then
                        'Open the solution
                        CurrentPluginManager.CurrentIOUIManager.OpenFile(target.Project, False)
                    ElseIf Not target.IsDirectory Then
                        'Open the selected project
                        CurrentPluginManager.CurrentIOUIManager.OpenFile(target.GetNodeProject, False)
                    End If
                End If
            Next
        End Sub

        Public Overrides Function SupportedTypes() As IEnumerable(Of TypeInfo)
            Return {GetType(SolutionHeiarchyItemViewModel).GetTypeInfo}
        End Function

        Public Overrides Function SupportsObject(Obj As Object) As Boolean
            If TypeOf Obj Is SolutionHeiarchyItemViewModel Then
                Return Not DirectCast(Obj, SolutionHeiarchyItemViewModel).IsDirectory 'Is this a project?
            Else
                Return False
            End If
        End Function

        Public Sub New()
            MyBase.New({My.Resources.Language.MenuProperties})
            Me.IsContextBased = True
        End Sub
    End Class
End Namespace

