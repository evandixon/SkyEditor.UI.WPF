Imports System.Reflection
Imports System.Windows.Forms
Imports SkyEditor.Core.IO
Imports SkyEditor.Core.Projects
Imports SkyEditor.Core.UI

Namespace MenuActions
    Public Class FileSaveAll
        Inherits MenuAction
        Private Property saveAction As MenuAction
        Public Overrides Function SupportedTypes() As IEnumerable(Of TypeInfo)
            Return {GetType(Solution).GetTypeInfo, GetType(ISavable).GetTypeInfo}
        End Function
        Public Overrides Function SupportsObjects(Objects As IEnumerable(Of Object)) As Boolean
            Dim hasProject = From o In Objects Where TypeOf o Is Solution
            Dim hasSavable = From o In Objects Where TypeOf o Is FileViewModel AndAlso (DirectCast(o, FileViewModel).CanSave(CurrentPluginManager) OrElse DirectCast(o, FileViewModel).CanSaveAs(CurrentPluginManager))

            Return hasProject.Any AndAlso hasSavable.Any
        End Function
        Public Overrides Sub DoAction(Targets As IEnumerable(Of Object))
            For Each item In Targets
                If TypeOf item Is Solution Then
                    DirectCast(item, Solution).SaveAllProjects(CurrentPluginManager.CurrentIOProvider)
                ElseIf TypeOf item Is FileViewModel Then
                    saveAction.CurrentPluginManager = CurrentPluginManager
                    saveAction.DoAction({item})
                End If
            Next
        End Sub
        Public Sub New()
            MyBase.New({My.Resources.Language.MenuFile, My.Resources.Language.MenuFileSave, My.Resources.Language.MenuFileSaveAll})
            SortOrder = 1.34
            saveAction = New FileSave
        End Sub
    End Class
End Namespace

