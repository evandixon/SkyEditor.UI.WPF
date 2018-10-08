Imports System.IO
Imports System.Reflection
Imports System.Windows
Imports SkyEditor.Core
Imports SkyEditor.Core.IO
Imports SkyEditor.Core.IO.PluginInfrastructure
Imports SkyEditor.Core.UI
Imports SkyEditor.UI.WPF.ViewModels.Projects

Namespace MenuActions.Context
    Public Class ProjectNodeOpenFile
        Inherits MenuAction

        Public Shared Async Function OpenFile(target As ProjectHeiarchyItemViewModel, appViewModel As ApplicationViewModel, pluginManager As PluginManager) As Task
            If Not target.IsDirectory Then
                Dim obj = Await target.GetFile(pluginManager, AddressOf IOHelper.PickFirstDuplicateMatchSelector)
                If obj IsNot Nothing Then
                    appViewModel.OpenFile(obj, target.Project)
                Else
                    Dim f = Path.Combine(Path.GetDirectoryName(target.Project.Filename), target.GetFilename)
                    If Not File.Exists(f) Then
                        MessageBox.Show(String.Format(My.Resources.Language.ErrorCantFindFileAt, f))
                    End If
                End If
            End If
        End Function

        Public Sub New(pluginManager As PluginManager, applicationViewModel As ApplicationViewModel)
            MyBase.New({My.Resources.Language.MenuFileOpen})
            IsContextBased = True

            CurrentApplicationViewModel = applicationViewModel
            CurrentPluginManager = pluginManager
        End Sub

        Public Property CurrentApplicationViewModel As ApplicationViewModel

        Public Property CurrentPluginManager As PluginManager

        Public Overrides Async Sub DoAction(targets As IEnumerable(Of Object))
            For Each item As ProjectHeiarchyItemViewModel In targets
                Await OpenFile(item, CurrentApplicationViewModel, CurrentPluginManager)
            Next
        End Sub

        Public Overrides Function GetSupportedTypes() As IEnumerable(Of TypeInfo)
            Return {GetType(ProjectHeiarchyItemViewModel).GetTypeInfo}
        End Function

        Public Overrides Function SupportsObject(obj As Object) As Task(Of Boolean)
            Return Task.FromResult(TypeOf obj Is ProjectHeiarchyItemViewModel AndAlso Not DirectCast(obj, ProjectHeiarchyItemViewModel).IsDirectory)
        End Function

    End Class
End Namespace

