Imports System.Threading.Tasks
Imports SkyEditor.Core
Imports SkyEditor.Core.ConsoleCommands
Imports SkyEditor.Core.UI

Namespace MenuActions
    Public Class DevConsole
        Inherits MenuAction

        Public Sub New(applicationViewModel As ApplicationViewModel)
            MyBase.New({My.Resources.Language.MenuDev, My.Resources.Language.MenuDevConsole})
            AlwaysVisible = True
            SortOrder = 10.1
            DevOnly = True

            CurrentApplicationViewModel = applicationViewModel
        End Sub

        Public Property CurrentApplicationViewModel As ApplicationViewModel

        Public Overrides Async Sub DoAction(Targets As IEnumerable(Of Object))
            Internal.ConsoleManager.Show()
            Await CurrentApplicationViewModel.CurrentConsoleShell.RunConsole()
        End Sub

    End Class
End Namespace

