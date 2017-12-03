Imports System.Reflection
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports SkyEditor.Core.IO
Imports SkyEditor.Core.UI

Namespace MenuActions
    Public Class FileSave
        Inherits MenuAction
        Public Overrides Function GetSupportedTypes() As IEnumerable(Of TypeInfo)
            Return {GetType(FileViewModel).GetTypeInfo}
        End Function

        Public Overrides Function SupportsObject(obj As Object) As Task(Of Boolean)
            Return Task.FromResult(TypeOf obj Is FileViewModel AndAlso
                                   (DirectCast(obj, FileViewModel).CanSave(CurrentApplicationViewModel.CurrentPluginManager) OrElse
                                   DirectCast(obj, FileViewModel).CanSaveAs(CurrentApplicationViewModel.CurrentPluginManager)))
        End Function

        Public Overrides Async Sub DoAction(targets As IEnumerable(Of Object))
            Dim CurrentPluginManager = CurrentApplicationViewModel.CurrentPluginManager
            For Each item As FileViewModel In targets
                If item.CanSave(CurrentPluginManager) Then
                    Await item.Save(CurrentPluginManager)
                ElseIf item.CanSaveAs(CurrentPluginManager) Then
                    Dim s = CurrentApplicationViewModel.GetSaveFileDialog(item, False)
                    If s.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                        Await item.Save(s.FileName, CurrentPluginManager)
                    End If
                    'If the dialog result is not OK, then the user can click the menu item again
                End If
            Next
        End Sub
        Public Sub New()
            MyBase.New({My.Resources.Language.MenuFile, My.Resources.Language.MenuFileSave, My.Resources.Language.MenuFileSaveFile})
            'AlwaysVisible = True
            SortOrder = 1.31
        End Sub
    End Class
End Namespace

