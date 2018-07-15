Imports SkyEditor.Core
Imports SkyEditor.Core.Extensions
Imports SkyEditor.Core.UI

Namespace MenuActions
    Public Class ToolsExtensions
        Inherits MenuAction

        Public Sub New(applicationViewModel As ApplicationViewModel)
            MyBase.New({My.Resources.Language.MenuTools, My.Resources.Language.MenuToolsExtensions})
            AlwaysVisible = True
            SortOrder = 3.2

            CurrentApplicationViewModel = applicationViewModel
        End Sub

        Public Property CurrentApplicationViewModel As ApplicationViewModel

        Public Overrides Sub DoAction(Targets As IEnumerable(Of Object))
            CurrentApplicationViewModel.OpenFile(New ExtensionHelper, False)
        End Sub

    End Class
End Namespace

