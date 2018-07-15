Imports System.ComponentModel
Imports System.Windows.Media
Imports SkyEditor.Core
Imports SkyEditor.Core.IO
Imports SkyEditor.Core.UI
Imports SkyEditor.UI.WPF.Settings

Namespace ViewModels
    Public Class TextFileViewModel
        Inherits GenericViewModel(Of TextFile)
        Implements INotifyModified
        Implements INotifyPropertyChanged

        Public Sub New(settings As ISettingsProvider)
            CurrentSettingsProvider = settings
        End Sub

        Public Event Modified As EventHandler Implements INotifyModified.Modified
        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Public Property Text As String
            Get
                Return Model.Contents
            End Get
            Set(value As String)
                If Model.Contents <> value Then
                    Model.Contents = value
                    RaiseEvent Modified(Me, New EventArgs)
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Text)))
                End If
            End Set
        End Property

        Protected Property CurrentSettingsProvider As ISettingsProvider

        Public ReadOnly Property FontFamily As FontFamily
            Get
                'Have to convert from Windows Forms font into WPF FontFamily
                Return New FontFamily(CurrentSettingsProvider.GetFont.FontFamily.Name)
            End Get
        End Property

        Public ReadOnly Property FontSize As Single
            Get
                Return CurrentSettingsProvider.GetFont.Size
            End Get
        End Property

        Public Sub RefreshFont()
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(FontSize)))
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(FontFamily)))
        End Sub

    End Class
End Namespace
