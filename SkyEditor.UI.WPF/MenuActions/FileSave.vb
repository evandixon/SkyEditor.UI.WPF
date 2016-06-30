Imports System.Reflection
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports SkyEditor.Core.IO
Imports SkyEditor.Core.UI

Namespace MenuActions
    Public Class FileSave
        Inherits MenuAction
        Private WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
        Public Overrides Function SupportedTypes() As IEnumerable(Of TypeInfo)
            Return {GetType(FileViewModel).GetTypeInfo}
        End Function

        Public Overrides Function SupportsObject(Obj As Object) As Boolean
            Return TypeOf Obj Is FileViewModel AndAlso (DirectCast(Obj, FileViewModel).CanSave(CurrentPluginManager) OrElse DirectCast(Obj, FileViewModel).CanSaveAs(CurrentPluginManager))
        End Function

        Public Overrides Sub DoAction(Targets As IEnumerable(Of Object))
            For Each item As FileViewModel In Targets
                If item.CanSave(CurrentPluginManager) Then
                    item.Save(CurrentPluginManager)
                ElseIf item.CanSaveAs(CurrentPluginManager) Then
                    Dim defaultExt = item.GetDefaultExtension(CurrentPluginManager)
                    If Not String.IsNullOrEmpty(defaultExt) Then
                        SaveFileDialog1.Filter = CurrentPluginManager.CurrentIOUIManager.IOFiltersString(IsSaveAs:=True) 'Todo: use default extension
                    Else
                        SaveFileDialog1.Filter = CurrentPluginManager.CurrentIOUIManager.IOFiltersStringSaveAs(IO.Path.GetExtension(DirectCast(item, IOnDisk).Filename))
                    End If

                    If SaveFileDialog1.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                        item.Save(SaveFileDialog1.FileName, CurrentPluginManager)
                    End If
                    'If the dialog result is not OK, then the user can click the menu item again
                End If
            Next
        End Sub
        Public Sub New()
            MyBase.New({My.Resources.Language.MenuFile, My.Resources.Language.MenuFileSave, My.Resources.Language.MenuFileSaveFile})
            SaveFileDialog1 = New SaveFileDialog
            'AlwaysVisible = True
            SortOrder = 1.31
        End Sub
    End Class
End Namespace

