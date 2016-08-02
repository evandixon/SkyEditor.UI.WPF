Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports SkyEditor.Core.IO
Imports SkyEditor.Core.Projects
Imports SkyEditor.Core.UI

Namespace MenuActions
    Public Class FileOpenAuto
        Inherits MenuAction

        Public Sub New()
            MyBase.New({My.Resources.Language.MenuFile, My.Resources.Language.MenuFileOpen, My.Resources.Language.MenuFileOpenAuto})
            AlwaysVisible = True
            SortOrder = 1.21
        End Sub

        Public Overrides Async Sub DoAction(Targets As IEnumerable(Of Object))
            Dim o = CurrentPluginManager.CurrentIOUIManager.GetOpenFileDialog
            If o.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                If o.FileName.ToLower.EndsWith(".skysln") Then
                    CurrentPluginManager.CurrentIOUIManager.CurrentSolution = Solution.OpenSolutionFile(o.FileName, CurrentPluginManager)
                Else
                    Await CurrentPluginManager.CurrentIOUIManager.OpenFile(o.FileName, AddressOf IOHelper.PickFirstDuplicateMatchSelector)
                End If
            End If
        End Sub

        Private Sub FileNewSolution_CurrentPluginManagerChanged(sender As Object, e As EventArgs) Handles Me.CurrentPluginManagerChanged
            Me.AlwaysVisible = CurrentPluginManager IsNot Nothing AndAlso (CurrentPluginManager.GetRegisteredObjects(Of IOpenableFile).Any() OrElse CurrentPluginManager.GetRegisteredObjects(Of IFileOpener).Any(Function(x As IFileOpener) TypeOf x IsNot OpenableFileOpener))
        End Sub

    End Class
End Namespace

