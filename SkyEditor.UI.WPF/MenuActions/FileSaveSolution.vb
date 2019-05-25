Imports System.Reflection
Imports SkyEditor.Core.IO
Imports SkyEditor.Core.Projects
Imports SkyEditor.Core.UI
Imports SkyEditor.IO.FileSystem

Namespace MenuActions
    Public Class FileSaveSolution
        Inherits MenuAction

        Public Sub New(provider As IFileSystem)
            MyBase.New({My.Resources.Language.MenuFile, My.Resources.Language.MenuFileSave, My.Resources.Language.MenuFileSaveSolution})
            SortOrder = 1.33

            CurrentFileSystem = provider
        End Sub

        Public Property CurrentFileSystem As IFileSystem

        Public Overrides Function GetSupportedTypes() As IEnumerable(Of TypeInfo)
            Return {GetType(Solution).GetTypeInfo}
        End Function

        Public Overrides Sub DoAction(Targets As IEnumerable(Of Object))
            For Each item As Solution In Targets
                item.Save(CurrentFileSystem)
                item.SaveAllProjects()
            Next
        End Sub

    End Class
End Namespace

