Imports System.ComponentModel
Imports SkyEditor.Core.IO
Imports SkyEditor.Core.UI

Namespace ViewModels
    Public Class TextFileViewModel
        Inherits GenericViewModel(Of TextFile)
        Implements INotifyModified
        Implements INotifyPropertyChanged

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

        Public Event Modified As EventHandler Implements INotifyModified.Modified
        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    End Class
End Namespace
