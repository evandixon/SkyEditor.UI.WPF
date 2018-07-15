Imports System.Reflection
Imports System.Windows.Forms
Imports SkyEditor.Core
Imports SkyEditor.Core.UI

Namespace MenuActions
    Public Class FileSaveAs
        Inherits MenuAction

        Public Sub New(pluginManager As PluginManager, applicationViewModel As ApplicationViewModel)
            MyBase.New({My.Resources.Language.MenuFile, My.Resources.Language.MenuFileSave, My.Resources.Language.MenuFileSaveFileAs})
            SortOrder = 1.32

            CurrentApplicationViewModel = applicationViewModel
            CurrentPluginManager = pluginManager
        End Sub

        Public Property CurrentApplicationViewModel As ApplicationViewModel
        Public Property CurrentPluginManager As PluginManager

        Public Overrides Function GetSupportedTypes() As IEnumerable(Of TypeInfo)
            Return {GetType(FileViewModel).GetTypeInfo}
        End Function

        Public Overrides Function SupportsObject(obj As Object) As Task(Of Boolean)
            Return Task.FromResult(TypeOf obj Is FileViewModel AndAlso DirectCast(obj, FileViewModel).CanSaveAs(CurrentPluginManager))
        End Function

        Public Overrides Async Sub DoAction(targets As IEnumerable(Of Object))
            For Each item As FileViewModel In targets
                Dim s = CurrentApplicationViewModel.GetSaveFileDialog(item, False, CurrentPluginManager)
                If s.ShowDialog = DialogResult.OK Then
                    Await item.Save(s.FileName, CurrentPluginManager)
                End If
            Next
        End Sub

    End Class
End Namespace

