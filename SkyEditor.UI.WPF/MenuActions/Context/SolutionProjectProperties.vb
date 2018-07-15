Imports System.Reflection
Imports SkyEditor.Core
Imports SkyEditor.Core.UI
Imports SkyEditor.UI.WPF.ViewModels.Projects

Namespace MenuActions.Context
    Public Class SolutionProjectProperties
        Inherits MenuAction

        Public Sub New(pluginManager As PluginManager, applicationViewModel As ApplicationViewModel)
            MyBase.New({My.Resources.Language.MenuProperties})
            IsContextBased = True

            CurrentApplicationViewModel = applicationViewModel
            CurrentPluginManager = pluginManager
        End Sub

        Public Property CurrentApplicationViewModel As ApplicationViewModel

        Public Property CurrentPluginManager As PluginManager

        Public Overrides Sub DoAction(targets As IEnumerable(Of Object))
            For Each item In targets
                If TypeOf item Is SolutionHeiarchyItemViewModel Then
                    Dim target As SolutionHeiarchyItemViewModel = item
                    If target.IsRoot Then
                        'Open the solution
                        CurrentApplicationViewModel.OpenFile(target.Project, False)
                    ElseIf Not target.IsDirectory Then
                        'Open the selected project
                        CurrentApplicationViewModel.OpenFile(target.GetNodeProject, False)
                    End If
                End If
            Next
        End Sub

        Public Overrides Function GetSupportedTypes() As IEnumerable(Of TypeInfo)
            Return {GetType(SolutionHeiarchyItemViewModel).GetTypeInfo}
        End Function

        Public Overrides Function SupportsObject(obj As Object) As Task(Of Boolean)
            If TypeOf obj Is SolutionHeiarchyItemViewModel Then
                Return Task.FromResult(Not DirectCast(obj, SolutionHeiarchyItemViewModel).IsDirectory) 'Is this a project?
            Else
                Return Task.FromResult(False)
            End If
        End Function

    End Class
End Namespace

