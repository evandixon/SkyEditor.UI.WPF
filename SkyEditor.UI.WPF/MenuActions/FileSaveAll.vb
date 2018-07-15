Imports System.Reflection
Imports System.Windows.Forms
Imports SkyEditor.Core
Imports SkyEditor.Core.IO
Imports SkyEditor.Core.Projects
Imports SkyEditor.Core.UI

Namespace MenuActions
    Public Class FileSaveAll
        Inherits MenuAction

        Public Sub New(pluginManager As PluginManager, applicationViewModel As ApplicationViewModel)
            MyBase.New({My.Resources.Language.MenuFile, My.Resources.Language.MenuFileSave, My.Resources.Language.MenuFileSaveAll})
            SortOrder = 1.34
            SaveAction = New FileSave(pluginManager, applicationViewModel)

            CurrentApplicationViewModel = applicationViewModel
            CurrentPluginManager = pluginManager
        End Sub

        Public Property CurrentApplicationViewModel As ApplicationViewModel

        Public Property CurrentPluginManager As PluginManager

        Private Property SaveAction As MenuAction

        Public Overrides Function GetSupportedTypes() As IEnumerable(Of TypeInfo)
            Return {GetType(Solution).GetTypeInfo, GetType(ISavable).GetTypeInfo}
        End Function

        Public Overrides Function SupportsObjects(objects As IEnumerable(Of Object)) As Task(Of Boolean)
            Dim hasProject = From o In objects Where TypeOf o Is Solution
            Dim hasSavable = From o In objects Where TypeOf o Is FileViewModel AndAlso (DirectCast(o, FileViewModel).CanSave(CurrentPluginManager) OrElse DirectCast(o, FileViewModel).CanSaveAs(CurrentPluginManager))

            Return Task.FromResult(hasProject.Any AndAlso hasSavable.Any)
        End Function

        Public Overrides Sub DoAction(targets As IEnumerable(Of Object))
            For Each item In targets
                If TypeOf item Is Solution Then
                    DirectCast(item, Solution).SaveAllProjects()
                ElseIf TypeOf item Is FileViewModel Then
                    SaveAction.DoAction({item})
                End If
            Next
        End Sub

    End Class
End Namespace

