Imports SkyEditor.Core
Imports SkyEditor.Core.UI
Imports SkyEditor.UI.WPF.AvalonHelpers

Public Class WPFApplicationViewModel
    Inherits ApplicationViewModel

    Public Sub New(manager As PluginManager)
        MyBase.New(manager)
        AvalonDockLayout = New AvalonDockLayoutViewModel(Me, manager)
    End Sub

    Public Event RestartRequested As EventHandler

    Public Property AvalonDockLayout As AvalonDockLayoutViewModel

    Protected Overrides Function CreateViewModel(model As Object) As FileViewModel
        Return New AvalonDockFileWrapper(model, CurrentPluginManager)
    End Function

    Public Sub RequestRestart()
        RaiseEvent RestartRequested(Me, New EventArgs())
    End Sub
End Class
