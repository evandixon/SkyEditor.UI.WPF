Imports SkyEditor.Core.Projects
Imports SkyEditor.Core.UI

Namespace ViewModels
    Public Class SolutionViewModel
        Inherits GenericViewModel(Of Solution)

        Public Overrides Sub SetModel(model As Object)
            MyBase.SetModel(model)

            'Todo: register event handlers
            'Todo: load directories and projects
        End Sub
    End Class
End Namespace
