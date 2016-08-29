Imports System.IO
Imports System.Reflection
Imports System.Windows
Imports SkyEditor.Core
Imports SkyEditor.Core.IO
Imports SkyEditor.Core.Projects
Imports SkyEditor.Core.UI

Namespace MenuActions.Context
    Public Class ProjectNodeOpenFile
        Inherits MenuAction

        Public Shared Async Function OpenFile(target As ProjectNode, manager As PluginManager) As Task
            If Not target.IsDirectory Then
                Dim obj = Await target.GetFile(manager, AddressOf IOHelper.PickFirstDuplicateMatchSelector)
                If obj IsNot Nothing Then
                    manager.CurrentIOUIManager.OpenFile(obj, target.ParentProject)
                Else
                    Dim f = Path.Combine(Path.GetDirectoryName(target.ParentProject.Filename), target.Filename)
                    If Not File.Exists(f) Then
                        MessageBox.Show(String.Format(My.Resources.Language.ErrorCantFindFileAt, f))
                    End If
                End If
            End If
        End Function



        Public Overrides Async Sub DoAction(Targets As IEnumerable(Of Object))
            For Each item As ProjectNode In Targets
                Await OpenFile(item, CurrentPluginManager)
            Next
        End Sub

        Public Overrides Function SupportedTypes() As IEnumerable(Of TypeInfo)
            Return {GetType(ProjectNode).GetTypeInfo}
        End Function

        Public Overrides Function SupportsObject(Obj As Object) As Boolean
            Return TypeOf Obj Is ProjectNode AndAlso Not DirectCast(Obj, ProjectNode).IsDirectory
        End Function

        Public Sub New()
            MyBase.New({My.Resources.Language.MenuFileOpen})
            IsContextBased = True
        End Sub
    End Class
End Namespace

