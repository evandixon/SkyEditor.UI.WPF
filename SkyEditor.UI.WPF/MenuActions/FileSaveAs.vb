Imports System.Reflection
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports SkyEditor.Core.IO
Imports SkyEditor.Core.UI

Namespace MenuActions
    Public Class FileSaveAs
        Inherits MenuAction
        Private WithEvents SaveFileDialog1 As SaveFileDialog
        Public Overrides Function SupportedTypes() As IEnumerable(Of TypeInfo)
            Return {GetType(FileViewModel).GetTypeInfo}
        End Function

        Public Overrides Function SupportsObject(Obj As Object) As Boolean
            Return TypeOf Obj Is FileViewModel AndAlso DirectCast(Obj, FileViewModel).CanSaveAs(CurrentPluginManager)
        End Function
        Public Overrides Sub DoAction(Targets As IEnumerable(Of Object))
            For Each item As FileViewModel In Targets
                SaveFileDialog1.Filter = CurrentPluginManager.CurrentIOUIManager.IOFiltersStringSaveAs(IO.Path.GetExtension(item.GetDefaultExtension(CurrentPluginManager)))
                If SaveFileDialog1.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                    item.Save(SaveFileDialog1.FileName, CurrentPluginManager)
                End If
            Next
        End Sub
        Public Sub New()
            MyBase.New({My.Resources.Language.MenuFile, My.Resources.Language.MenuFileSave, My.Resources.Language.MenuFileSaveFileAs})
            SaveFileDialog1 = New SaveFileDialog
            SortOrder = 1.32
        End Sub
    End Class
End Namespace

