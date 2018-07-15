Imports System.Reflection
Imports System.Threading.Tasks
Imports SkyEditor.Core
Imports SkyEditor.Core.IO
Imports SkyEditor.Core.Projects
Imports SkyEditor.Core.UI

Namespace MenuActions
    Public Class SolutionBuild
        Inherits MenuAction

        Public Sub New(applicationViewModel As ApplicationViewModel)
            MyBase.New({My.Resources.Language.MenuSolution, My.Resources.Language.MenuSolutionBuild})
            SortOrder = 2.1

            CurrentApplicationViewModel = applicationViewModel
        End Sub

        Public Property CurrentApplicationViewModel As ApplicationViewModel

        Public Overrides Function GetSupportedTypes() As IEnumerable(Of TypeInfo)
            Return {GetType(Solution).GetTypeInfo}
        End Function

        Public Overrides Sub DoAction(Targets As IEnumerable(Of Object))
            CurrentApplicationViewModel.ClearErrors()
            For Each item As Solution In Targets
                CurrentApplicationViewModel.ShowLoading(item.Build())
            Next
        End Sub

    End Class
End Namespace

