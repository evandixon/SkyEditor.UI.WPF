Imports System.ComponentModel
Imports System.Windows
Imports SkyEditor.Core
Imports SkyEditor.Core.UI

Public Class WizardForm
    Public Sub New(wizard As Wizard, applicationViewModel As ApplicationViewModel)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ContentPlaceholder.CurrentApplicationViewModel = applicationViewModel
        _wizard = wizard
        DataContext = wizard
        RefreshButtonEnabling()
    End Sub

    Private WithEvents _wizard As Wizard

    Private Sub _wizard_PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Handles _wizard.PropertyChanged
        RefreshButtonEnabling()
    End Sub

    Private Sub RefreshButtonEnabling()
        btnPrevious.IsEnabled = _wizard.CanGoBack
        btnNext.IsEnabled = _wizard.CanGoForward
        btnFinish.IsEnabled = _wizard.IsComplete
    End Sub

    Private Sub btnPrevious_Click(sender As Object, e As RoutedEventArgs) Handles btnPrevious.Click
        _wizard.GoBack()
    End Sub

    Private Sub btnNext_Click(sender As Object, e As RoutedEventArgs) Handles btnNext.Click
        _wizard.GoForward()
    End Sub

    Private Sub btnFinish_Click(sender As Object, e As RoutedEventArgs) Handles btnFinish.Click
        DialogResult = True
        Me.Close()
    End Sub
End Class
