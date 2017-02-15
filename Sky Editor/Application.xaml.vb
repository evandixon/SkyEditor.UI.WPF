Imports SkyEditor.UI.WPF

Class Application

    ' Application-level events, such as Startup, Exit, and DispatcherUnhandledException
    ' can be handled in this file.

    Private Async Sub Application_Startup(sender As Object, e As StartupEventArgs) Handles Me.Startup
        StartupHelpers.EnableErrorDialog()
        Await StartupHelpers.RunWPFStartupSequence()
    End Sub

#If DEBUG Then
    Public Class DebugTraceListener
        Inherits TraceListener

        Public Overrides Sub Write(message As String)
        End Sub

        Public Overrides Sub WriteLine(message As String)
            Debugger.Break()
        End Sub
    End Class
#End If
End Class
