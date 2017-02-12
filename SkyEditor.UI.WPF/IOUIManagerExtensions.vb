﻿Imports System.Runtime.CompilerServices
Imports System.Windows.Forms
Imports SkyEditor.Core
Imports SkyEditor.Core.UI

Public Module IOUIManagerExtensions
    ''' <summary>
    ''' Gets an OpenFileDialog that can open any supported file.
    ''' </summary>
    ''' <param name="appViewModel">Instance of the current application ViewModel</param>
    ''' <returns>An OpenFileDialog with a filter that displays all supported files.</returns>
    <Extension> Public Function GetOpenFileDialog(appViewModel As ApplicationViewModel) As OpenFileDialog
        Dim o As New OpenFileDialog
        o.Filter = appViewModel.GetIOFilter
        Return o
    End Function

    ''' <summary>
    ''' Gets an OpenFileDialog that can open any supported file.
    ''' </summary>
    ''' <param name="appViewModel">Instance of the current application ViewModel</param>
    ''' <param name="filters">The file extensions to include in the filter.</param>
    ''' <returns>An OpenFileDialog with a filter defined by <paramref name="filters"/>.</returns>
    <Extension> Public Function GetOpenFileDialog(appViewModel As ApplicationViewModel, filters As ICollection(Of String)) As OpenFileDialog
        Dim o As New OpenFileDialog
        o.Filter = appViewModel.GetIOFilter(filters, True, True)
        Return o
    End Function

    ''' <summary>
    ''' Gets a SaveFileDialog with a filter especially for the given file.
    ''' </summary>
    ''' <param name="appViewModel">Instance of the current application ViewModel</param>
    ''' <param name="file">File for which to get the SaveFileDialog.</param>
    ''' <returns>A SaveFileDialog with a filter containing the <paramref name="file"/>'s supported extensions, with the currently selected one being the default extension.</returns>
    <Extension> Public Function GetSaveFileDialog(appViewModel As ApplicationViewModel, file As FileViewModel) As SaveFileDialog
        Dim s As New SaveFileDialog
        Dim extensions = file.GetSupportedExtensions(appViewModel.CurrentPluginManager)
        s.Filter = appViewModel.GetIOFilter(extensions, False, True)
        s.FilterIndex = extensions.ToList.IndexOf(file.GetDefaultExtension(appViewModel.CurrentPluginManager)) + 1
        Return s
    End Function
End Module
