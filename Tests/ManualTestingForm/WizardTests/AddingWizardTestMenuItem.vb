Imports SkyEditor.Core.UI
Imports SkyEditor.UI.WPF

Public Class AddingWizardTestMenuItem
    Inherits MenuAction

    Public Sub New()
        MyBase.New({"Tests", "Wizards", "Adding Wizard"})
        AlwaysVisible = True
    End Sub

    Public Overrides Sub DoAction(targets As IEnumerable(Of Object))
        Dim wizardForm As New WizardForm(New AddingWizard(CurrentApplicationViewModel), CurrentApplicationViewModel)
        wizardForm.ShowDialog()
    End Sub
End Class
