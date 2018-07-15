Imports SkyEditor.Core
Imports SkyEditor.Core.UI
Imports SkyEditor.UI.WPF.Models

Namespace MenuActions
    Public Class FileHelpCredits
        Inherits MenuAction

        Public Sub New(pluginManager As PluginManager, applicationViewModel As ApplicationViewModel)
            MyBase.New({My.Resources.Language.MenuHelp, My.Resources.Language.MenuHelpCredits})
            AlwaysVisible = True
            SortOrder = 100

            CurrentApplicationViewModel = applicationViewModel
            CurrentPluginManager = pluginManager
        End Sub

        Public Property CurrentApplicationViewModel As ApplicationViewModel

        Public Property CurrentPluginManager As PluginManager

        Public Overrides Sub DoAction(Targets As IEnumerable(Of Object))
            If Not CurrentApplicationViewModel.OpenFiles.Any(Function(x As FileViewModel) As Boolean
                                                                 Return TypeOf x.Model Is CreditsModel
                                                             End Function) Then
                CurrentApplicationViewModel.OpenFile(New CreditsModel(CurrentPluginManager), False)
            End If
        End Sub

    End Class

End Namespace
