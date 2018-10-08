Imports SkyEditor.Core
Imports SkyEditor.Core.IO
Imports SkyEditor.Core.IO.PluginInfrastructure
Imports SkyEditor.Core.UI
Imports SkyEditor.Core.Utilities
Namespace MenuActions
    Public Class FileNewFile
        Inherits MenuAction

        Public Sub New(pluginManager As PluginManager, applicationViewModel As ApplicationViewModel)
            MyBase.New({My.Resources.Language.MenuFile, My.Resources.Language.MenuFileNew, My.Resources.Language.MenuFileNewFile})

            CurrentApplicationViewModel = applicationViewModel
            CurrentPluginManager = pluginManager
            AlwaysVisible = CurrentPluginManager.GetRegisteredObjects(Of ICreatableFile).Any()
            SortOrder = 1.11
        End Sub

        Public Property CurrentApplicationViewModel As ApplicationViewModel
        Public Property CurrentPluginManager As PluginManager

        Public Overrides Sub DoAction(Targets As IEnumerable(Of Object))
            Dim w As New NewFileWindow()
            Dim games As New Dictionary(Of String, Type)
            For Each item In IOHelper.GetCreateableFileTypes(CurrentPluginManager)
                games.Add(ReflectionHelpers.GetTypeFriendlyName(item), item)
            Next
            w.SetGames(games)
            If w.ShowDialog Then
                Dim file As Object = IOHelper.CreateNewFile(w.SelectedName, w.SelectedType, CurrentPluginManager)
                CurrentApplicationViewModel.OpenFile(file, True)
            End If
        End Sub

    End Class
End Namespace

