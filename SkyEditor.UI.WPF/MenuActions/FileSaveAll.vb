Imports System.Reflection
Imports System.Windows.Forms
Imports SkyEditor.Core.IO
Imports SkyEditor.Core.Projects
Imports SkyEditor.Core.UI

Namespace MenuActions
    Public Class FileSaveAll
        Inherits MenuAction
        Private Property SaveAction As MenuAction
        Public Overrides Function SupportedTypes() As IEnumerable(Of TypeInfo)
            Return {GetType(Solution).GetTypeInfo, GetType(ISavable).GetTypeInfo}
        End Function
        Public Overrides Function SupportsObjects(objects As IEnumerable(Of Object)) As Task(Of Boolean)
            Dim hasProject = From o In Objects Where TypeOf o Is Solution
            Dim hasSavable = From o In Objects Where TypeOf o Is FileViewModel AndAlso (DirectCast(o, FileViewModel).CanSave(CurrentPluginManager) OrElse DirectCast(o, FileViewModel).CanSaveAs(CurrentPluginManager))

            Return Task.FromResult(hasProject.Any AndAlso hasSavable.Any)
        End Function
        Public Overrides Sub DoAction(targets As IEnumerable(Of Object))
            For Each item In Targets
                If TypeOf item Is Solution Then
                    DirectCast(item, Solution).SaveAllProjects(CurrentPluginManager.CurrentIOProvider)
                ElseIf TypeOf item Is FileViewModel Then
                    SaveAction.CurrentPluginManager = CurrentPluginManager
                    SaveAction.DoAction({item})
                End If
            Next
        End Sub
        Public Sub New()
            MyBase.New({My.Resources.Language.MenuFile, My.Resources.Language.MenuFileSave, My.Resources.Language.MenuFileSaveAll})
            SortOrder = 1.34
            SaveAction = New FileSave
        End Sub
    End Class
End Namespace

