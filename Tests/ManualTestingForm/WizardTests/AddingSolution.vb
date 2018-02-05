Imports SkyEditor.Core.Projects
Imports SkyEditor.Core.UI

Public Class AddingSolution
    Inherits Solution

    Public Overrides ReadOnly Property RequiresInitializationWizard As Boolean
        Get
            Return True
        End Get
    End Property

    Public Overrides Function GetInitializationWizard() As Wizard
        Return New AddingWizard(CurrentPluginManager)
    End Function
End Class
