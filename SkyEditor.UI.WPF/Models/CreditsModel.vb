Imports System.ComponentModel
Imports SkyEditor.Core

Namespace Models
    Public Class CreditsModel
        Implements INotifyPropertyChanged

        Public Sub New(manager As PluginManager)
            CurrentPluginManager = manager
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
        Dim _selectedPlugin As SkyEditorPlugin

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    End Class
End Namespace

