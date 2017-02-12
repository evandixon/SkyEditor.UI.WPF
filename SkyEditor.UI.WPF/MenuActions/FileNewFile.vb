Imports SkyEditor.Core.IO
Imports SkyEditor.Core.UI
Imports SkyEditor.Core.Utilities
Namespace MenuActions
    Public Class FileNewFile
        Inherits MenuAction

        Public Sub New()
            MyBase.New({My.Resources.Language.MenuFile, My.Resources.Language.MenuFileNew, My.Resources.Language.MenuFileNewFile})
            AlwaysVisible = True
            SortOrder = 1.11
        End Sub

        Public Overrides Sub DoAction(Targets As IEnumerable(Of Object))
            Dim w As New NewFileWindow()
            Dim games As New Dictionary(Of String, Type)
            For Each item In IOHelper.GetCreateableFileTypes(CurrentApplicationViewModel.CurrentPluginManager)
                games.Add(ReflectionHelpers.GetTypeFriendlyName(item), item)
            Next
            w.SetGames(games)
            If w.ShowDialog Then
                Dim file As Object = IOHelper.CreateNewFile(w.SelectedName, w.SelectedType)
                CurrentApplicationViewModel.OpenFile(file, True)
            End If
        End Sub

        Private Sub FileNewSolution_CurrentPluginManagerChanged(sender As Object, e As EventArgs) Handles Me.CurrentApplicationViewModelChanged
            Me.AlwaysVisible = CurrentApplicationViewModel IsNot Nothing AndAlso (CurrentApplicationViewModel.CurrentPluginManager.GetRegisteredObjects(Of ICreatableFile).Any())
        End Sub
    End Class
End Namespace

