Imports System.Windows
Imports System.Windows.Threading
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

    Public Overrides Sub ReportError([error] As ErrorInfo)
        Application.Current.Dispatcher.Invoke(Sub() MyBase.ReportError([error]))
    End Sub

    Public Overrides Sub ReportError(exception As Exception)
        Application.Current.Dispatcher.Invoke(Sub() MyBase.ReportError(exception))
    End Sub

End Class
