Imports System.Reflection
Imports SkyEditor.Core
Imports SkyEditor.Core.UI
Imports SkyEditor.Core.Settings
Imports SkyEditor.Core.IO
Imports System.ComponentModel
Imports SkyEditor.Core.Utilities
Imports System.Windows.Forms
Imports SkyEditor.UI.WPF.Settings
Imports System.Windows.Input
Imports SkyEditor.IO.FileSystem

Namespace ViewModels.Settings
    Public Class GeneralSettingsViewModel
        Inherits GenericViewModel(Of ISettingsProvider)
        Implements INotifyModified
        Implements INotifyPropertyChanged
        Implements ISavable
        Implements INamed

        Public Sub New()
            MyBase.New
            SetFontCommand = New RelayCommand(AddressOf ShowFontDialog)
        End Sub

        Public Sub New(provider As ISettingsProvider)
            Me.New
            Me.Model = provider
        End Sub

        Public Property IsDevMode As Boolean
            Get
                Return Model.GetIsDevMode()
            End Get
            Set(value As Boolean)
                Model.SetIsDevMode(value)
                RaiseEvent Modified(Me, New EventArgs)
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(IsDevMode)))
            End Set
        End Property

        Public ReadOnly Property Name As String Implements INamed.Name
            Get
                Return My.Resources.Language.Settings
            End Get
        End Property

        Public ReadOnly Property CurrentFontDisplay As String
            Get
                Dim font = Model.GetFont()
                If font Is Nothing Then
                    Return My.Resources.Language.Settings_FontDefault
                Else
                    Return String.Format(My.Resources.Language.Settings_FontDisplay, font.FontFamily.Name, font.SizeInPoints)
                End If
            End Get
        End Property

        Public ReadOnly Property SetFontCommand As ICommand

        Public Event FileSaved As EventHandler Implements ISavable.FileSaved
        Public Event Modified As EventHandler Implements INotifyModified.Modified
        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Public Async Function Save(provider As IFileSystem) As Task Implements ISavable.Save
            Await Model.Save(provider)
            RaiseEvent FileSaved(Me, New EventArgs)
        End Function

        Private Sub ShowFontDialog()
            Dim fontDialog = New FontDialog

            Dim currentFont = Model.GetFont()
            If currentFont IsNot Nothing Then
                fontDialog.Font = currentFont
            End If

            If fontDialog.ShowDialog = DialogResult.OK Then
                Model.SetFont(fontDialog.Font)
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(CurrentFontDisplay)))
            End If
        End Sub
    End Class
End Namespace

