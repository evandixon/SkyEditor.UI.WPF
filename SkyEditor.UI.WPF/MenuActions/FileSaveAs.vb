Imports System.Reflection
Imports System.Windows.Forms
Imports SkyEditor.Core.UI

Namespace MenuActions
    Public Class FileSaveAs
        Inherits MenuAction
        Public Overrides Function GetSupportedTypes() As IEnumerable(Of TypeInfo)
            Return {GetType(FileViewModel).GetTypeInfo}
        End Function

        Public Overrides Function SupportsObject(obj As Object) As Task(Of Boolean)
            Return Task.FromResult(TypeOf obj Is FileViewModel AndAlso DirectCast(obj, FileViewModel).CanSaveAs(CurrentApplicationViewModel.CurrentPluginManager))
        End Function

        Public Overrides Sub DoAction(targets As IEnumerable(Of Object))
            For Each item As FileViewModel In targets
                Dim s = CurrentApplicationViewModel.GetSaveFileDialog(item)
                If s.ShowDialog = DialogResult.OK Then
                    item.Save(s.FileName, CurrentApplicationViewModel.CurrentPluginManager)
                End If
            Next
        End Sub

        Public Sub New()
            MyBase.New({My.Resources.Language.MenuFile, My.Resources.Language.MenuFileSave, My.Resources.Language.MenuFileSaveFileAs})
            SortOrder = 1.32
        End Sub
    End Class
End Namespace

