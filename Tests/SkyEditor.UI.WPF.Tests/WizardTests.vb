Imports ManualTestingForm
Imports SkyEditor.Core

<TestClass>
Public Class WizardTests
    Public Const TestCategory = "Wizard Tests"

    ''' <summary>
    ''' Make sure the next button in the wizard is enabled after the step is completed.
    ''' </summary>
    <TestMethod>
    <TestCategory(TestCategory)>
    Public Sub NextButtonBoundToWizardCanGoForward()
        Using manager As New PluginManager
            Using appVM As New ApplicationViewModel(manager)
                manager.LoadCore(New WPFCoreSkyEditorPlugin).Wait()

                Dim wizard As New AddingWizard(manager)
                Dim wizardForm As New WizardForm(wizard, appVM)

                'We're currently on the first step. It has a number filled by default.
                wizard.GoForward()

                'We're now on the second step. It needs a number, and we can't proceed until it has one.
                Assert.IsFalse(wizardForm.btnNext.IsEnabled)

                wizard.Term2Step.Term2 = 10
                'We can now proceed.
                Assert.IsTrue(wizardForm.btnNext.IsEnabled)
            End Using
        End Using
    End Sub
End Class
