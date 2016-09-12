Imports SkyEditor.Core.Projects

Namespace ViewModels.Projects
    Public Class ProjectHeiarchyItemViewModel
        Inherits ProjectBaseHeiarchyItemViewModel

        Public Sub New(solution As Project)
            MyBase.New(solution)
        End Sub

        Public Sub New(solution As Project, parent As ProjectHeiarchyItemViewModel, path As String)
            MyBase.New(solution, parent, path)
        End Sub

    End Class
End Namespace
