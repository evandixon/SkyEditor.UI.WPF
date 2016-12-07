Imports System.Reflection
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports SkyEditor.Core.IO
Imports SkyEditor.Core.UI

Namespace MenuActions
    Public Class FileSave
        Inherits MenuAction
        Public Overrides Function SupportedTypes() As IEnumerable(Of TypeInfo)
            Return {GetType(FileViewModel).GetTypeInfo}
        End Function

        Public Overrides  Function SupportsObject(obj As Object) As Task(Of Boolean)
            Return task.FromResult(TypeOf Obj Is FileViewModel AndAlso (DirectCast(Obj, FileViewModel).CanSave(CurrentPluginManager) OrElse DirectCast(Obj, FileViewModel).CanSaveAs(CurrentPluginManager)))
        End Function

        Public Overrides Sub DoAction(targets As IEnumerable(Of Object))
            For Each item As FileViewModel In Targets
                If item.CanSave(CurrentPluginManager) Then
                    item.Save(CurrentPluginManager)
                ElseIf item.CanSaveAs(CurrentPluginManager) Then
                    Dim s = CurrentPluginManager.CurrentIOUIManager.GetSaveFileDialog(item)
                    If s.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                        item.Save(s.FileName, CurrentPluginManager)
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

