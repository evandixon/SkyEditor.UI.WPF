Imports SkyEditor.Core.UI

Namespace MenuActions
    Public Class DevPlugins
        Inherits MenuAction

        Public Overrides Sub DoAction(Targets As IEnumerable(Of Object))
            CurrentPluginManager.CurrentIOUIManager.OpenFile(CurrentPluginManager, False)
        End Sub

        Public Sub New()
            MyBase.New({My.Resources.Language.MenuDev, My.Resources.Language.MenuDevPlugins})
            Me.AlwaysVisible = True
            Me.DevOnly = True
            SortOrder = 10.3
        End Sub
    End Class
End Namespace

