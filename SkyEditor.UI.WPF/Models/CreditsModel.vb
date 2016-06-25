Imports SkyEditor.Core

Namespace Models
    Public Class CreditsModel
        Public Sub New(manager As PluginManager)
            CurrentPluginManager = CurrentPluginManager
        End Sub

        Public ReadOnly Property CurrentPluginManager As PluginManager

        Public ReadOnly Property CurrentPlugins As List(Of SkyEditorPlugin)
            Get
                Return CurrentPluginManager.Plugins
            End Get
        End Property
    End Class
End Namespace

