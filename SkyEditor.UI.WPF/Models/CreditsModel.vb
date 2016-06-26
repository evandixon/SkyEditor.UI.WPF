Imports System.ComponentModel
Imports SkyEditor.Core
Imports SkyEditor.Core.Utilities

Namespace Models
    Public Class CreditsModel
        Implements INotifyPropertyChanged
        Implements INamed

        Public Sub New(manager As PluginManager)
            CurrentPluginManager = manager
            If CurrentPlugins.Count > 0 Then
                SelectedPlugin = CurrentPlugins.First
            End If
        End Sub

        Public ReadOnly Property CurrentPluginManager As PluginManager

        Public ReadOnly Property CurrentPlugins As List(Of SkyEditorPlugin)
            Get
                Return CurrentPluginManager.Plugins
            End Get
        End Property

        Public Property SelectedPlugin As SkyEditorPlugin
            Get
                Return _selectedPlugin
            End Get
            Set(value As SkyEditorPlugin)
                If value IsNot _selectedPlugin Then
                    _selectedPlugin = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(SelectedPlugin)))
                End If
            End Set
        End Property

        Public ReadOnly Property Name As String Implements INamed.Name
            Get
                Return My.Resources.Language.Credits
            End Get
        End Property

        Dim _selectedPlugin As SkyEditorPlugin

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    End Class
End Namespace

