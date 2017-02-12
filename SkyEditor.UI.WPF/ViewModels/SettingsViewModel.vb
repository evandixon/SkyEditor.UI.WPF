Imports System.Reflection
Imports SkyEditor.Core
Imports SkyEditor.Core.UI
Imports SkyEditor.Core.Settings
Imports SkyEditor.Core.IO
Imports System.ComponentModel
Imports SkyEditor.Core.Utilities

Namespace ViewModels
    Public Class SettingsViewModel
        Inherits GenericViewModel(Of ISettingsProvider)
        Implements INotifyModified
        Implements INotifyPropertyChanged
        Implements ISavable
        Implements INamed

        Public Sub New()
            MyBase.New
        End Sub

        Public Sub New(provider As ISettingsProvider)
            MyBase.New
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

        Public Event FileSaved As EventHandler Implements ISavable.FileSaved
        Public Event Modified As EventHandler Implements INotifyModified.Modified
        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Public Async Function Save(provider As IIOProvider) As Task Implements ISavable.Save
            Await Model.Save(provider)
            RaiseEvent FileSaved(Me, New EventArgs)
        End Function
    End Class
End Namespace

