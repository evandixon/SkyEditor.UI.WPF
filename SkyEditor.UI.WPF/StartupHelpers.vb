Imports System.IO
Imports System.Threading
Imports System.Windows.Forms
Imports SkyEditor.Core
Imports SkyEditor.Core.Utilities

Public Class StartupHelpers

    Public Shared Async Function GetMainWindow() As Task(Of MainWindow3)
        Return Await GetMainWindow(New WPFCoreSkyEditorPlugin)
    End Function

    Public Shared Async Function GetMainWindow(coreMod As CoreSkyEditorPlugin) As Task(Of MainWindow3)
        Dim args = Environment.GetCommandLineArgs()

        'Set the current culture if in the command-line args
        If args.Contains("-culture") Then
            Dim index = Array.IndexOf(args, "-culture")
            If args.Count > index + 1 Then
                Dim culture = args(index + 1)
                Thread.CurrentThread.CurrentCulture = New Globalization.CultureInfo(culture)
                Thread.CurrentThread.CurrentUICulture = New Globalization.CultureInfo(culture)
            End If
        End If

        Dim manager As New PluginManager
        Await manager.LoadCore(coreMod)

        Dim appViewModel As New WPFApplicationViewModel(manager)

        Dim m As New MainWindow3
        m.CurrentApplicationViewModel = appViewModel

        Return m
    End Function

    Public Shared Async Function ShowMainWindow() As Task
        Await ShowMainWindow(New WPFCoreSkyEditorPlugin)
    End Function

    Public Shared Async Function ShowMainWindow(coreMod As CoreSkyEditorPlugin) As Task
        Try
            Dim mainWindow = Await GetMainWindow()
            mainWindow.ShowDialog()
            Cleanup()
        Catch originalException As Exception
            Try
                ErrorWindow.ShowErrorDialog(originalException, False)
            Catch secondaryException As Exception
                'Something went really bad, and we can't tell the user.
                'Let's save the error message to the current working directory so this isn't just a mystery crash

                Dim errorMessage = "A fatal exception occurred and the details could not be displayed." & vbCrLf &
                                   "Original exception: " & originalException.ToString() & vbCrLf &
                                   "Reporting exception: " & secondaryException.ToString() & vbCrLf

                File.WriteAllText("Error-" & DateTime.Now.ToString("YYYY-MM-DD-hh-mm-ss") & ".txt", errorMessage)
            End Try
        End Try
    End Function

    Public Shared Sub Cleanup()
        'Delete .tmp files
        For Each item In Directory.GetFiles(EnvironmentPaths.GetRootResourceDirectory, "*.tmp", SearchOption.AllDirectories)
            Try
                File.Delete(item)
            Catch ex As IOException
                'Something's keeping the file from being deleted.  It's probably still open.  It will get deleted the next time the program exits.
            End Try
        Next
    End Sub

    Public Shared Sub EnableErrorDialog()
        'Add the event handler for handling UI thread exceptions to the event.
        AddHandler Application.ThreadException, AddressOf UIThreadException

        'Set the unhandled exception mode to force all Windows Forms errors to go through our handler.
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException)

        'Add the event handler for handling non-UI thread exceptions to the event. 
        AddHandler AppDomain.CurrentDomain.UnhandledException, AddressOf CurrentDomain_UnhandledException

        'Listen for WPF binding exceptions
        PresentationTraceSources.Refresh()
        PresentationTraceSources.DataBindingSource.Listeners.Add(New ConsoleTraceListener)
        PresentationTraceSources.DataBindingSource.Listeners.Add(New ErrorWindowTraceListener)
        PresentationTraceSources.DataBindingSource.Switch.Level = SourceLevels.Warning Or SourceLevels.Error

        'Set visual style needed for error window
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)
    End Sub

    'Handle the UI exceptions by showing a dialog box, and asking the user whether or not they wish to abort execution.
    Private Shared Sub UIThreadException(sender As Object, t As ThreadExceptionEventArgs)
        Try
            ErrorWindow.ShowErrorDialog(t.Exception, True)
        Catch ex As Exception
            MessageBox.Show(String.Format(My.Resources.Language.ErrorHandling_FatalError, t.Exception.ToString))
        End Try
    End Sub

    'Handle the UI exceptions by showing a dialog box, and asking the user whether
    'or not they wish to abort execution.
    'NOTE: This exception cannot be kept from terminating the application - it can only 
    'log the event, and inform the user about it. 
    Private Shared Sub CurrentDomain_UnhandledException(sender As Object, e As UnhandledExceptionEventArgs)
        Try
            ErrorWindow.ShowErrorDialog(DirectCast(e.ExceptionObject, Exception), True)
        Catch ex As Exception
            MessageBox.Show(String.Format(My.Resources.Language.ErrorHandling_FatalError, e.ExceptionObject.ToString))
        End Try
    End Sub

    Public Class ErrorWindowTraceListener
        Inherits TraceListener

        Public Overrides Sub Write(message As String)
        End Sub

        Public Overrides Sub WriteLine(message As String)
            ErrorWindow.ShowErrorDialog(My.Resources.Language.ErrorHandling_UIThreadErrorMessage, New BindingException(message), True)
        End Sub
    End Class

    Public Class BindingException
        Inherits Exception
        Public Sub New(message As String)
            MyBase.New(message)
        End Sub
    End Class
End Class