Imports System.Reflection
Imports System.Windows
Imports SkyEditor.Core
Imports SkyEditor.Core.UI
Imports SkyEditor.UI.WPF.ViewModels.Projects

Namespace MenuActions.Context
    Public Class SolutionProjectDelete
        Inherits MenuAction

        Public Sub New(pluginManager As PluginManager, applicationViewModel As ApplicationViewModel)
            MyBase.New({My.Resources.Language.MenuDelete})
            IsContextBased = True

            CurrentApplicationViewModel = applicationViewModel
            CurrentPluginManager = pluginManager
        End Sub

        Public Property CurrentApplicationViewModel As ApplicationViewModel

        Public Property CurrentPluginManager As PluginManager

        Public Overrides Sub DoAction(targets As IEnumerable(Of Object))
            For Each item In targets
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
            If TypeOf obj Is ProjectBaseHeiarchyItemViewModel Then
                Return Task.FromResult(DirectCast(obj, ProjectBaseHeiarchyItemViewModel).CanDeleteCurrentNode)
            Else
                Return Task.FromResult(False)
            End If
        End Function

    End Class
End Namespace