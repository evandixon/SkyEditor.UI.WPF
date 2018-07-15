Imports SkyEditor.Core
Imports SkyEditor.UI.WPF.Settings
Imports SkyEditor.UI.WPF.ViewModels

Namespace Views
    Public Class TextFileView
        Inherits DataBoundViewControl

        Public Sub New(settings As ISettingsProvider)
            InitializeComponent()

            CurrentSettingsProvider = settings
        End Sub

        Protected Property CurrentSettingsProvider As ISettingsProvider

        Private Sub txtText_MouseWheel(sender As Object, e As Windows.Input.MouseWheelEventArgs) Handles txtText.MouseWheel
            If My.Computer.Keyboard.CtrlKeyDown Then
                Dim currentFont = CurrentSettingsProvider.GetFont()
                If e.Delta > 0 Then
                    currentFont = New Drawing.Font(currentFont.FontFamily, currentFont.Size + 1, currentFont.Style)
                Else
                    currentFont = New Drawing.Font(currentFont.FontFamily, currentFont.Size - 1, currentFont.Style)
                End If
                CurrentSettingsProvider.SetFont(currentFont)

                If TypeOf ViewModel Is TextFileViewModel Then
                    DirectCast(ViewModel, TextFileViewModel).RefreshFont
                End If
            End If
        End Sub
    End Class
End Namespace

