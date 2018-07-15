Imports SkyEditor.Core.Projects
Imports SkyEditor.Core.UI
Imports SkyEditor.Core.Settings
Imports SkyEditor.Core

Namespace MenuActions
    Public Class FileNewSolution
        Inherits MenuAction

        Public Sub New(pluginManager As PluginManager, applicationViewModel As ApplicationViewModel)
            MyBase.New({My.Resources.Language.MenuFile, My.Resources.Language.MenuFileNew, My.Resources.Language.MenuFileNewSolution})

            CurrentApplicationViewModel = applicationViewModel
            CurrentPluginManager = pluginManager
            Me.AlwaysVisible = CurrentPluginManager.GetRegisteredObjects(Of Solution).Count() > 1 OrElse CurrentPluginManager.CurrentSettingsProvider.GetIsDevMode
            SortOrder = 1.12
        End Sub

        Public Property CurrentApplicationViewModel As ApplicationViewModel

        Public Property CurrentPluginManager As PluginManager

        Public Overrides Async Sub DoAction(Targets As IEnumerable(Of Object))
            Dim newSol As New NewSolutionWindow(CurrentPluginManager)
            If newSol.ShowDialog Then
                Dim solutionCreateTask = ProjectBase.CreateProject(Of Solution)(newSol.SelectedLocation, newSol.SelectedName, newSol.SelectedSolution.GetType, CurrentPluginManager)
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
    End Class
End Namespace