Imports System.IO
Imports SkyEditor.UI.WPF


Class Application

    ' Application-level events, such as Startup, Exit, and DispatcherUnhandledException
    ' can be handled in this file.

    Private Async Sub Application_Startup(sender As Object, e As StartupEventArgs) Handles Me.Startup
        StartupHelpers.EnableErrorDialog()

        Dim jcheck As String = "c:\Program Files\Java"
        Dim jcheck2 As String = "c:\Program Files\Java\"

        If File.Exists(jcheck) = True Then
            Debug.WriteLine("Java is installed! C1")
        ElseIf File.Exists(jcheck2) = True Then
            Debug.WriteLine("Java is installed! C2")
        Else
            Debug.WriteLine("Java is NOT INSTALLED!")
        End If



        Await StartupHelpers.ShowMainWindow()
    End Sub

End Class
