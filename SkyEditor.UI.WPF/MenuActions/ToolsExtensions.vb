Imports SkyEditor.Core.Extensions
Imports SkyEditor.Core.UI

Namespace MenuActions
    Public Class ToolsExtensions
        Inherits MenuAction

        Public Overrides Sub DoAction(Targets As IEnumerable(Of Object))
            CurrentApplicationViewModel.OpenFile(New ExtensionHelper, False)
        End Sub

        Public Sub New()
            MyBase.New({My.Resources.Language.MenuTools, My.Resources.Language.MenuToolsExtensions})
            AlwaysVisible = True
            SortOrder = 3.2
        End Sub
    End Class
End Namespace

