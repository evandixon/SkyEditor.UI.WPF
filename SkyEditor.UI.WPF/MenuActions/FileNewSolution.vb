﻿Imports SkyEditor.Core.Projects
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

        Public Overrides Sub DoAction(Targets As IEnumerable(Of Object))
            Dim newSol As New NewSolutionWindow(CurrentApplicationViewModel)
            If newSol.ShowDialog Then
                CurrentApplicationViewModel.CurrentSolution = ProjectBase.CreateProject(Of Solution)(newSol.SelectedLocation, newSol.SelectedName, newSol.SelectedSolution.GetType, CurrentApplicationViewModel.CurrentPluginManager)
            End If
        End Sub

        Private Sub FileNewSolution_CurrentPluginManagerChanged(sender As Object, e As EventArgs) Handles Me.CurrentApplicationViewModelChanged
            Me.AlwaysVisible = CurrentApplicationViewModel IsNot Nothing AndAlso (CurrentApplicationViewModel.CurrentPluginManager.GetRegisteredObjects(Of Solution).Count() > 1 OrElse CurrentApplicationViewModel.CurrentPluginManager.CurrentSettingsProvider.GetIsDevMode)
        End Sub
    End Class
End Namespace