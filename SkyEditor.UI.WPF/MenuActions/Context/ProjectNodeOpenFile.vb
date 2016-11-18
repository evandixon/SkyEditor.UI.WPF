Imports System.IO
Imports System.Reflection
Imports System.Windows
Imports SkyEditor.Core
Imports SkyEditor.Core.IO
Imports SkyEditor.Core.UI
Imports SkyEditor.UI.WPF.ViewModels.Projects

Namespace MenuActions.Context
    Public Class ProjectNodeOpenFile
        Inherits MenuAction

        Public Shared Async Function OpenFile(target As ProjectHeiarchyItemViewModel, manager As PluginManager) As Task
            If Not target.IsDirectory Then
                Dim obj = Await target.GetFile(manager, AddressOf IOHelper.PickFirstDuplicateMatchSelector)
                If obj IsNot Nothing Then
                    manager.CurrentIOUIManager.OpenFile(obj, target.Project)
                Else
                    Dim f = Path.Combine(Path.GetDirectoryName(target.Project.Filename), target.GetFilename)
                    If Not File.Exists(f) Then
                        MessageBox.Show(String.Format(My.Resources.Language.ErrorCantFindFileAt, f))
                    End If
                End If
            End If
        End Function



        Public Overrides Async Sub DoAction(targets As IEnumerable(Of Object))
            For Each item As ProjectHeiarchyItemViewModel In Targets
                Await OpenFile(item, CurrentPluginManager)
            Next
        End Sub

        Public Overrides Function SupportedTypes() As IEnumerable(Of TypeInfo)
            Return {GetType(ProjectHeiarchyItemViewModel).GetTypeInfo}
        End Function

        Public Overrides Function SupportsObject(obj As Object) As Task(Of Boolean)
            Return Task.FromResult(TypeOf Obj Is ProjectHeiarchyItemViewModel AndAlso Not DirectCast(Obj, ProjectHeiarchyItemViewModel).IsDirectory)
        End Function

        Public Sub New()
            MyBase.New({My.Resources.Language.MenuFileOpen})
            IsContextBased = True
        End Sub
    End Class
End Namespace

