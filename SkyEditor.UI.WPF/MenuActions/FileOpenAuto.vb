Imports SkyEditor.Core.IO
Imports SkyEditor.Core.Projects
Imports SkyEditor.Core.UI
Imports SkyEditor.Core.Settings
Imports System.Windows

Namespace MenuActions
    Public Class FileOpenAuto
        Inherits MenuAction

        Public Sub New()
            MyBase.New({My.Resources.Language.MenuFile, My.Resources.Language.MenuFileOpen, My.Resources.Language.MenuFileOpenAuto})
            AlwaysVisible = True
            SortOrder = 1.21
        End Sub

        Public Overrides Async Sub DoAction(Targets As IEnumerable(Of Object))
            Dim o = CurrentApplicationViewModel.GetOpenFileDialog(True)
            If o.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                If o.FileName.ToLower.EndsWith(".skysln") Then
                    Dim openProjectTask = ProjectBase.OpenProjectFile(o.FileName, CurrentApplicationViewModel.CurrentPluginManager)
                    CurrentApplicationViewModel.ShowLoading(openProjectTask)

                    Dim solution = Await openProjectTask
                    If TypeOf solution Is Solution Then
                        CurrentApplicationViewModel.CurrentSolution = solution
                    Else
                        MessageBox.Show(My.Resources.Language.Menu_FileOpenAuto_SolutionOpenFailed)
                    End If
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

