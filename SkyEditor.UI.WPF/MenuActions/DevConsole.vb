Imports System.Threading.Tasks
Imports SkyEditor.Core.ConsoleCommands
Imports SkyEditor.Core.UI

Namespace MenuActions
    Public Class DevConsole
        Inherits MenuAction

        Public Overrides Sub DoAction(Targets As IEnumerable(Of Object))
            Internal.ConsoleManager.Show()
            ConsoleHelper.RunConsole(CurrentPluginManager)
        End Sub

        Public Sub New()
            MyBase.New({My.Resources.Language.MenuDev, My.Resources.Language.MenuDevConsole})
            AlwaysVisible = True
            SortOrder = 10.1
            DevOnly = True
        End Sub
    End Class
End Namespace

