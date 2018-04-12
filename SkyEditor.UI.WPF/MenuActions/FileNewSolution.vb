Imports SkyEditor.Core.Projects
Imports SkyEditor.Core.UI
Imports SkyEditor.Core.Settings

Namespace MenuActions
    Public Class FileNewSolution
        Inherits MenuAction

        Public Sub New()
            MyBase.New({My.Resources.Language.MenuFile, My.Resources.Language.MenuFileNew, My.Resources.Language.MenuFileNewSolution})
            Me.AlwaysVisible = True
            SortOrder = 1.12
        End Sub

        Public Overrides Async Sub DoAction(Targets As IEnumerable(Of Object))
            Dim newSol As New NewSolutionWindow(CurrentApplicationViewModel)
            If newSol.ShowDialog Then
                Dim solutionCreateTask = ProjectBase.CreateProject(Of Solution)(newSol.SelectedLocation, newSol.SelectedName, newSol.SelectedSolution.GetType, CurrentApplicationViewModel.CurrentPluginManager)
                CurrentApplicationViewModel.ShowLoading(solutionCreateTask)

                Dim solution = Await solutionCreateTask
                CurrentApplicationViewModel.CurrentSolution = solution

                Dim initTask = solution.Initialize()
                CurrentApplicationViewModel.ShowLoading(initTask)
                CurrentApplicationViewModel.ShowLoading(solution.LoadingTask)

                Await initTask
                Await solution.LoadingTask

                If solution.RequiresInitializationWizard Then
                    Dim wizard = solution.GetInitializationWizard
                    Dim wizardForm As New WizardForm(wizard, CurrentApplicationViewModel)
                    wizardForm.ShowDialog()
                    'To-do: do something if dialog result is not true
                End If
            End If
        End Sub

        Private Sub FileNewSolution_CurrentPluginManagerChanged(sender As Object, e As EventArgs) Handles Me.CurrentApplicationViewModelChanged
            Me.AlwaysVisible = CurrentApplicationViewModel IsNot Nothing AndAlso
                (CurrentApplicationViewModel.CurrentPluginManager.GetRegisteredObjects(Of Solution).Count() > 1 OrElse
                CurrentApplicationViewModel.CurrentPluginManager.CurrentSettingsProvider.GetIsDevMode)
        End Sub
    End Class
End Namespace