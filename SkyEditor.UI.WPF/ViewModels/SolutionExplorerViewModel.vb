Imports System.Collections.ObjectModel
Imports SkyEditor.Core.IO
Imports SkyEditor.Core.Projects
Imports SkyEditor.Core.UI
Imports SkyEditor.UI.WPF.ViewModels.Projects

Namespace ViewModels
    Public Class SolutionExplorerViewModel
        Inherits AnchorableViewModel

        Public ReadOnly Property SolutionRoots As ObservableCollection(Of SolutionHeiarchyItemViewModel)

        Public Sub New()
            Me.Header = My.Resources.Language.SolutionExplorerToolWindowTitle
            SolutionRoots = New ObservableCollection(Of SolutionHeiarchyItemViewModel)
        End Sub

        Private Sub SolutionExplorerViewModel_CurrentSolutionChanged(sender As Object, e As EventArgs) Handles Me.CurrentSolutionChanged
            SolutionRoots.Clear()
            SolutionRoots.Add(New SolutionHeiarchyItemViewModel(CurrentApplicationViewModel.CurrentSolution))
        End Sub
    End Class

End Namespace
