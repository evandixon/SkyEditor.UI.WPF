Imports System.Runtime.CompilerServices
Imports System.Windows.Forms
Imports SkyEditor.Core
Imports SkyEditor.Core.UI

Public Module IOUIManagerExtensions
    ''' <summary>
    ''' Gets an OpenFileDialog that can open any supported file.
    ''' </summary>
    ''' <param name="ioui"></param>
    ''' <returns>An OpenFileDialog with a filter that displays all supported files.</returns>
    <Extension> Public Function GetOpenFileDialog(ioui As IOUIManager) As OpenFileDialog
        Dim o As New OpenFileDialog
        o.Filter = ioui.GetIOFilter
        Return o
    End Function

    ''' <summary>
    ''' Gets a SaveFileDialog with a filter especially for the given file.
    ''' </summary>
    ''' <param name="ioui"></param>
    ''' <param name="file">File for which to get the SaveFileDialog.</param>
    ''' <returns>A SaveFileDialog with a filter containing the <paramref name="file"/>'s supported extensions, with the currently selected one being the default extension.</returns>
    <Extension> Public Function GetSaveFileDialog(ioui As IOUIManager, file As FileViewModel) As SaveFileDialog
        Dim s As New SaveFileDialog
        Dim extensions = file.GetSupportedExtensions(ioui.CurrentPluginManager)
        s.Filter = ioui.GetIOFilter(extensions, False, True)
        s.FilterIndex = extensions.ToList.IndexOf(file.GetDefaultExtension(ioui.CurrentPluginManager)) + 1
        Return s
    End Function
End Module
