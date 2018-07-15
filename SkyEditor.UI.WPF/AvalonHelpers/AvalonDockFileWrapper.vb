Imports System.ComponentModel
Imports SkyEditor.Core
Imports SkyEditor.Core.UI

Namespace AvalonHelpers
    Public Class AvalonDockFileWrapper
        Inherits FileViewModel
        Implements INotifyPropertyChanged

        Public Sub New(manager As PluginManager)
            MyBase.New(manager)
        End Sub
        Public Sub New(file As Object, manager As PluginManager)
            MyBase.New(file, manager)
        End Sub

        Public ReadOnly Property Tooltip As String
            Get
                Return ""
            End Get
        End Property

    End Class
End Namespace

