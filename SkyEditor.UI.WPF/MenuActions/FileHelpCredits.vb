Imports SkyEditor.Core.UI
Imports SkyEditor.UI.WPF.Models

Namespace MenuActions
    Public Class FileHelpCredits
        Inherits MenuAction

        Public Overrides Sub DoAction(Targets As IEnumerable(Of Object))
            If Not CurrentPluginManager.CurrentIOUIManager.OpenFiles.Any(Function(x As FileViewModel) As Boolean
                                                                             Return TypeOf x.File Is CreditsModel
                                                                         End Function) Then
                CurrentPluginManager.CurrentIOUIManager.OpenFile(New CreditsModel(CurrentPluginManager), False)
            End If
        End Sub

        Public Sub New()
            MyBase.New({My.Resources.Language.MenuHelp, My.Resources.Language.MenuHelpCredits})
            AlwaysVisible = True
            SortOrder = 100
        End Sub
    End Class

End Namespace
