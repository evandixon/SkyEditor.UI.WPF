Imports System.Reflection
Imports SkyEditor.Core
Imports SkyEditor.Core.UI
Imports SkyEditor.UI.WPF.KeyShortcuts
Imports SkyEditor.UI.WPF.MenuActions
Imports SkyEditor.UI.WPF.MenuActions.Context
Imports SkyEditor.UI.WPF.MenuActions.View
Imports SkyEditor.UI.WPF.ObjectControls
Imports SkyEditor.UI.WPF.ViewModels
Imports SkyEditor.UI.WPF.Views

Public Class WPFCoreSkyEditorPlugin
    Inherits CoreSkyEditorPlugin

    ''' <summary>
    ''' Creates a new instance of <see cref="WPFCoreSkyEditorPlugin"/>
    ''' </summary>
    Public Sub New()

    End Sub

    ''' <summary>
    ''' Creates a new instance of <see cref="WPFCoreSkyEditorPlugin"/> that directly loads the given plugin
    ''' </summary>
    ''' <param name="plugin">Plugin to load</param>
    Public Sub New(plugin As SkyEditorPlugin)
        _plugin = plugin
    End Sub
    Dim _plugin As SkyEditorPlugin
    Public Overrides ReadOnly Property Credits As String
        Get
            Return My.Resources.Language.PluginCredits
        End Get
    End Property

    Public Overrides ReadOnly Property PluginAuthor As String
        Get
            Return My.Resources.Language.PluginAuthor
        End Get
    End Property

    Public Overrides ReadOnly Property PluginName As String
        Get
            Return My.Resources.Language.PluginName
        End Get
    End Property

    Public Overrides Function CreateApplicationViewModel(manager As PluginManager) As ApplicationViewModel
        Return New WPFApplicationViewModel(manager)
    End Function


    Public Overrides Sub Load(manager As PluginManager)
        MyBase.Load(manager)

        manager.RegisterTypeRegister(Of IViewControl)()
        manager.RegisterTypeRegister(Of ControlKeyAction)()

        manager.RegisterType(Of IViewControl, GenericIList)()
        manager.RegisterType(Of IViewControl, SolutionExplorer)()
        manager.RegisterType(Of IViewControl, SolutionBuildProgress)()
        manager.RegisterType(Of IViewControl, ExtensionManager)()

        manager.RegisterType(Of ControlKeyAction, ControlS)()

        manager.RegisterType(Of AnchorableViewModel, SolutionExplorerViewModel)()
        manager.RegisterType(Of AnchorableViewModel, SolutionBuildProgressViewModel)()

        manager.RegisterType(Of MenuAction, DevConsole)()
        manager.RegisterType(Of MenuAction, DevPlugins)()
        manager.RegisterType(Of MenuAction, FileNewFile)()
        manager.RegisterType(Of MenuAction, FileNewSolution)()
        manager.RegisterType(Of MenuAction, FileOpenAuto)()
        manager.RegisterType(Of MenuAction, FileOpenManual)()
        manager.RegisterType(Of MenuAction, FileSave)()
        manager.RegisterType(Of MenuAction, FileSaveAll)()
        manager.RegisterType(Of MenuAction, FileSaveAs)()
        manager.RegisterType(Of MenuAction, FileSaveSolution)()
        manager.RegisterType(Of MenuAction, SolutionBuild)()
        manager.RegisterType(Of MenuAction, ToolsExtensions)()
        manager.RegisterType(Of MenuAction, ToolsSettings)()
        manager.RegisterType(Of MenuAction, MenuViewSolutionExplorer)()
        manager.RegisterType(Of MenuAction, MenuViewSolutionBuildProgress)()

        manager.RegisterType(Of MenuAction, SolutionProjectAddFolder)()
        manager.RegisterType(Of MenuAction, ProjectNodeOpenFile)()
        manager.RegisterType(Of MenuAction, SolutionCreateProject)()
        manager.RegisterType(Of MenuAction, ProjectNewFile)()
        manager.RegisterType(Of MenuAction, SolutionAddExistingProject)()
        manager.RegisterType(Of MenuAction, ProjectAddExistingFile)()
        manager.RegisterType(Of MenuAction, SolutionProjectProperties)()
        manager.RegisterType(Of MenuAction, SolutionProjectDelete)()

        If _plugin IsNot Nothing Then
            manager.LoadRequiredPlugin(_plugin, Me)
        End If
    End Sub

End Class
