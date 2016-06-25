Imports System.Threading.Tasks
Imports SkyEditor.Core.UI

Namespace MenuActions
    Public Class ToolsSettings
        Inherits MenuAction

        Public Overrides Sub DoAction(Targets As IEnumerable(Of Object))
            CurrentPluginManager.CurrentIOUIManager.OpenFile(CurrentPluginManager.CurrentSettingsProvider, False)
        End Sub

        Public Sub New()
            MyBase.New({My.Resources.Language.MenuTools, My.Resources.Language.MenuToolsSettings})
            AlwaysVisible = True
            SortOrder = 3.1
        End Sub
    End Class
End Namespace

