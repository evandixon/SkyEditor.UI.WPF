Imports SkyEditor.UI.WPF

Class Application

    ' Application-level events, such as Startup, Exit, and DispatcherUnhandledException
    ' can be handled in this file.

    Private Async Sub Application_Startup(sender As Object, e As StartupEventArgs) Handles Me.Startup
        StartupHelpers.EnableErrorDialog()

        If FileAttr.exists(jcheck) Then
        End If


        Await StartupHelpers.ShowMainWindow()
    End Sub

End Class
