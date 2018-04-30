Imports System.IO
Imports SkyEditor.UI.WPF


Class Application

    ' Application-level events, such as Startup, Exit, and DispatcherUnhandledException
    ' can be handled in this file.

    Private Async Sub Application_Startup(sender As Object, e As StartupEventArgs) Handles Me.Startup
        StartupHelpers.EnableErrorDialog()

        'Checks if Java is installed'
        If File.Exists("C:\Program Files (x86)\Common Files\Oracle\Java\javapath\java.exe") Then
            Debug.WriteLine("Java is installed!")
        Else
            Debug.WriteLine("Java is NOT INSTALLED!")
        End If



        Await StartupHelpers.ShowMainWindow()
    End Sub

End Class
