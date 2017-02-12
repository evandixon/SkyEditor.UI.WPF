Imports SkyEditor.Core.IO
Imports SkyEditor.Core.Projects
Imports SkyEditor.Core.UI
Imports SkyEditor.Core.Settings

Namespace MenuActions
    Public Class FileOpenAuto
        Inherits MenuAction

        Public Sub New()
            MyBase.New({My.Resources.Language.MenuFile, My.Resources.Language.MenuFileOpen, My.Resources.Language.MenuFileOpenAuto})
            AlwaysVisible = True
            SortOrder = 1.21
        End Sub

        Public Overrides Async Sub DoAction(Targets As IEnumerable(Of Object))
            Dim o = CurrentApplicationViewModel.GetOpenFileDialog
            If o.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                If o.FileName.ToLower.EndsWith(".skysln") Then
                    CurrentApplicationViewModel.CurrentSolution = Await ProjectBase.OpenProjectFile(Of Solution)(o.FileName, CurrentApplicationViewModel.CurrentPluginManager)
                Else
                    Await CurrentApplicationViewModel.OpenFile(o.FileName, AddressOf IOHelper.PickFirstDuplicateMatchSelector)
                End If
            End If
        End Sub

        Private Sub FileNewSolution_CurrentPluginManagerChanged(sender As Object, e As EventArgs) Handles Me.CurrentApplicationViewModelChanged
            With CurrentApplicationViewModel?.CurrentPluginManager
                Me.AlwaysVisible = CurrentApplicationViewModel?.CurrentPluginManager IsNot Nothing AndAlso
                (.GetRegisteredObjects(Of IOpenableFile).Any() OrElse
                .GetRegisteredObjects(Of IFileOpener).Any(Function(x As IFileOpener) TypeOf x IsNot OpenableFileOpener) OrElse
                .CurrentSettingsProvider.GetIsDevMode)
            End With
        End Sub

    End Class
End Namespace

