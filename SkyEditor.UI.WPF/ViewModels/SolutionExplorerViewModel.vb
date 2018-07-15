Imports System.Collections.ObjectModel
Imports SkyEditor.Core
Imports SkyEditor.Core.IO
Imports SkyEditor.Core.Projects
Imports SkyEditor.Core.UI
Imports SkyEditor.UI.WPF.MenuActions.Context
Imports SkyEditor.UI.WPF.ViewModels.Projects

Namespace ViewModels
    Public Class SolutionExplorerViewModel
        Inherits AnchorableViewModel

        Public Sub New(applicationViewModel As ApplicationViewModel, pluginManager As PluginManager)
            MyBase.New(applicationViewModel)
            Me.Header = My.Resources.Language.SolutionExplorerToolWindowTitle
            SolutionRoots = New ObservableCollection(Of SolutionHeiarchyItemViewModel)
            CurrentApplicationViewModel = applicationViewModel
            CurrentPluginManager = pluginManager
        End Sub

        Public ReadOnly Property SolutionRoots As ObservableCollection(Of SolutionHeiarchyItemViewModel)

        Protected Property CurrentApplicationViewModel As ApplicationViewModel

        Protected Property CurrentPluginManager As PluginManager

        Public Async Function OpenNode(node As Object) As Task
            Await ProjectNodeOpenFile.OpenFile(node, CurrentApplicationViewModel, CurrentPluginManager)
        End Function

        Private Sub SolutionExplorerViewModel_CurrentSolutionChanged(sender As Object, e As EventArgs) Handles Me.CurrentSolutionChanged
            SolutionRoots.Clear()
            SolutionRoots.Add(New SolutionHeiarchyItemViewModel(CurrentApplicationViewModel.CurrentSolution))
        End Sub
    End Class

End Namespace
