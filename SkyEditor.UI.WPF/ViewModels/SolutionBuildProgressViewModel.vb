Imports System.Collections.ObjectModel
Imports System.Windows
Imports SkyEditor.Core.IO
Imports SkyEditor.Core.Projects
Imports SkyEditor.Core.UI
Imports SkyEditor.Core.Utilities
Imports SkyEditor.UI.WPF.ViewModels.Projects

Namespace ViewModels
    Public Class SolutionBuildProgressViewModel
        Inherits AnchorableViewModel

        Public Sub New()
            Me.Header = My.Resources.Language.BuildProgress
            BuildingProjects = New ObservableCollection(Of ProjectBaseBuildViewModel)
        End Sub

        Public Property BuildingProjects As ObservableCollection(Of ProjectBaseBuildViewModel)

        Private WithEvents CurrentSolution As Solution

        Private Sub SolutionBuildProgressViewModel_CurrentSolutionChanged(sender As Object, e As EventArgs) Handles Me.CurrentSolutionChanged, Me.CurrentIOUIManagerChanged
            CurrentSolution = CurrentApplicationViewModel.CurrentSolution
        End Sub

        Private Sub Solution_BuildStarted(sender As Object, e As EventArgs) Handles CurrentSolution.SolutionBuildStarted
            Application.Current.Dispatcher.Invoke(Sub()
                                                      BuildingProjects.Clear()
                                                  End Sub)

            For Each item In DirectCast(sender, Solution).GetAllProjects
                AddHandler item.ProgressChanged, AddressOf Project_BuildStatusChanged
            Next
        End Sub

        Private Sub Solution_BuildCompleted(sender As Object, e As EventArgs) Handles CurrentSolution.SolutionBuildCompleted
            For Each item In DirectCast(sender, Solution).GetAllProjects
                RemoveHandler item.ProgressChanged, AddressOf Project_BuildStatusChanged
            Next
        End Sub

        Private Sub Project_BuildStatusChanged(sender As Object, e As ProgressReportedEventArgs)
            If Not BuildingProjects.Any(Function(x) x.Model Is sender) Then
                Application.Current.Dispatcher.Invoke(Sub()
                                                          BuildingProjects.Add(New ProjectBaseBuildViewModel(sender, CurrentApplicationViewModel))
                                                      End Sub)
            End If
        End Sub
    End Class
End Namespace

