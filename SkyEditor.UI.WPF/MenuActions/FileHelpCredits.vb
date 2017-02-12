Imports SkyEditor.Core.UI
Imports SkyEditor.UI.WPF.Models

Namespace MenuActions
    Public Class FileHelpCredits
        Inherits MenuAction

        Public Overrides Sub DoAction(Targets As IEnumerable(Of Object))
            If Not CurrentApplicationViewModel.OpenFiles.Any(Function(x As FileViewModel) As Boolean
                                                                 Return TypeOf x.Model Is CreditsModel
                                                             End Function) Then
                CurrentApplicationViewModel.OpenFile(New CreditsModel(CurrentApplicationViewModel.CurrentPluginManager), False)
            End If
        End Sub

        Public Sub New()
            MyBase.New({My.Resources.Language.MenuHelp, My.Resources.Language.MenuHelpCredits})
            AlwaysVisible = True
            SortOrder = 100
        End Sub
    End Class

End Namespace
