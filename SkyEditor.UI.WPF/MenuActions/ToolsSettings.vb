Imports System.Threading.Tasks
Imports SkyEditor.Core
Imports SkyEditor.Core.UI
Imports SkyEditor.UI.WPF.ViewModels
Imports SkyEditor.UI.WPF.ViewModels.Settings

Namespace MenuActions
    Public Class ToolsSettings
        Inherits MenuAction

        Public Sub New(settings As ISettingsProvider, applicationViewModel As ApplicationViewModel)
            MyBase.New({My.Resources.Language.MenuTools, My.Resources.Language.MenuToolsSettings})
            AlwaysVisible = True
            SortOrder = 3.1

            CurrentApplicationViewModel = applicationViewModel
            CurrentSettingsProvider = settings
        End Sub

        Protected Property CurrentApplicationViewModel As ApplicationViewModel

        Protected Property CurrentSettingsProvider As ISettingsProvider

        Public Overrides Sub DoAction(Targets As IEnumerable(Of Object))
            CurrentApplicationViewModel.OpenFile(New GeneralSettingsViewModel(CurrentSettingsProvider), False)
        End Sub

    End Class
End Namespace

