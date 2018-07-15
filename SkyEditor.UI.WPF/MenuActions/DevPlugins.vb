Imports SkyEditor.Core
Imports SkyEditor.Core.UI

Namespace MenuActions
    Public Class DevPlugins
        Inherits MenuAction

        Public Sub New(pluginManager As PluginManager, applicationViewModel As ApplicationViewModel)
            MyBase.New({My.Resources.Language.MenuDev, My.Resources.Language.MenuDevPlugins})
            Me.AlwaysVisible = True
            Me.DevOnly = True
            SortOrder = 10.3

            CurrentApplicationViewModel = applicationViewModel
            CurrentPluginManager = pluginManager
        End Sub

        Public Property CurrentApplicationViewModel As ApplicationViewModel

        Public Property CurrentPluginManager As PluginManager

        Public Overrides Sub DoAction(Targets As IEnumerable(Of Object))
            CurrentApplicationViewModel.OpenFile(CurrentPluginManager, False)
        End Sub

    End Class
End Namespace

