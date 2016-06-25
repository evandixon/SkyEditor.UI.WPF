Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports SkyEditor.Core.IO
Imports SkyEditor.Core.UI

Namespace MenuActions
    Public Class FileOpenAuto
        Inherits MenuAction

        Private WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog

        Public Overrides Async Sub DoAction(Targets As IEnumerable(Of Object))
            OpenFileDialog1.Filter = CurrentPluginManager.CurrentIOUIManager.IOFiltersString
            If OpenFileDialog1.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                If OpenFileDialog1.FileName.ToLower.EndsWith(".skysln") Then
                    CurrentPluginManager.CurrentIOUIManager.CurrentSolution = Solution.OpenSolutionFile(OpenFileDialog1.FileName, CurrentPluginManager)
                Else
                    CurrentPluginManager.CurrentIOUIManager.OpenFile(Await IOHelper.OpenObject(OpenFileDialog1.FileName, AddressOf IOHelper.PickFirstDuplicateMatchSelector, CurrentPluginManager), True)
                End If
            End If
        End Sub

        Public Sub New()
            MyBase.New({My.Resources.Language.MenuFile, My.Resources.Language.MenuFileOpen, My.Resources.Language.MenuFileOpenAuto})
            AlwaysVisible = True
            OpenFileDialog1 = New OpenFileDialog
            SortOrder = 1.21
        End Sub
    End Class
End Namespace

