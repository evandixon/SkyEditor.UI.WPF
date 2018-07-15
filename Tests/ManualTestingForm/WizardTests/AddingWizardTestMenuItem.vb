Imports SkyEditor.Core
Imports SkyEditor.Core.UI
Imports SkyEditor.UI.WPF

Public Class AddingWizardTestMenuItem
    Inherits MenuAction

    Public Sub New(applicationViewModel As ApplicationViewModel, pluginManager As PluginManager)
        MyBase.New({"Tests", "Wizards", "Adding Wizard"})
        AlwaysVisible = True

        CurrentApplicationViewModel = applicationViewModel
        CurrentPluginManager = pluginManager
    End Sub

    Protected Property CurrentApplicationViewModel As ApplicationViewModel

    Protected Property CurrentPluginManager As PluginManager

    Public Overrides Sub DoAction(targets As IEnumerable(Of Object))
        Dim wizardForm As New WizardForm(New AddingWizard(CurrentPluginManager), CurrentApplicationViewModel)
        wizardForm.ShowDialog()
    End Sub
End Class
