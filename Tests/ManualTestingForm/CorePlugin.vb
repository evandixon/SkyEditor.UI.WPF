Imports SkyEditor.Core
Imports SkyEditor.Core.UI
Imports SkyEditor.UI.WPF

Public Class CorePlugin
    Inherits WPFCoreSkyEditorPlugin

    Public Overrides ReadOnly Property PluginName As String
        Get
            Return "Test"
        End Get
    End Property

    Public Overrides ReadOnly Property PluginAuthor As String
        Get
            Return "Test"
        End Get
    End Property

    Public Overrides ReadOnly Property Credits As String
        Get
            Return "Test"
        End Get
    End Property

    Public Overrides Function IsCorePluginAssemblyDynamicTypeLoadEnabled() As Boolean
        Return True
    End Function

    Public Overrides Sub Load(manager As PluginManager)
        MyBase.Load(manager)
        manager.RegisterType(Of MenuAction, AddingWizardTestMenuItem)()
    End Sub
End Class
