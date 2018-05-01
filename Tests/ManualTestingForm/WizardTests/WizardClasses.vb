Imports SkyEditor.Core
Imports SkyEditor.Core.ConsoleCommands
Imports SkyEditor.Core.UI
Imports SkyEditor.Core.Utilities

Public Class AddingWizard
    Inherits Wizard
    Implements INamed

    Public Sub New(ByVal currentPluginManager As PluginManager)
        MyBase.New(currentPluginManager)

        Term1Step = New AddingWizardTerm1()
        Term2Step = New AddingWizardTerm2()
        ResultStep = New AddingWizardResult(Me)
        StepsInternal.Add(Term1Step)
        StepsInternal.Add(Term2Step)
        StepsInternal.Add(ResultStep)
    End Sub

    Public Property Term1Step As AddingWizardTerm1

    Public Property Term2Step As AddingWizardTerm2

    Public Property ResultStep As AddingWizardResult

    Public Overrides ReadOnly Property Name As String
        Get
            Return "Adding Wizard"
        End Get
    End Property
End Class

Public Class AddingWizardTerm1
    Implements IWizardStepViewModel

    Public ReadOnly Property Name As String Implements INamed.Name
        Get
            Return "Term 1"
        End Get
    End Property

    Public Property Term1 As Integer? = 0

    Public ReadOnly Property IsComplete As Boolean Implements IWizardStepViewModel.IsComplete
        Get
            Return Term1.HasValue
        End Get
    End Property

    Public Function GetConsoleCommand() As ConsoleCommand Implements IWizardStepViewModel.GetConsoleCommand
        Throw New NotImplementedException()
    End Function
End Class

Public Class AddingWizardTerm2
    Implements IWizardStepViewModel

    Public ReadOnly Property Name As String Implements INamed.Name
        Get
            Return "Term 2"
        End Get
    End Property

    Public Property Term2 As Integer?

    Public ReadOnly Property IsComplete As Boolean Implements IWizardStepViewModel.IsComplete
        Get
            Return Term2.HasValue
        End Get
    End Property

    Public Function GetConsoleCommand() As ConsoleCommand Implements IWizardStepViewModel.GetConsoleCommand
        Throw New NotImplementedException()
    End Function
End Class

Public Class AddingWizardResult
    Implements IWizardStepViewModel

    Public Sub New(ByVal wizard As AddingWizard)
        wizard = wizard
    End Sub

    Protected Property Wizard As AddingWizard

    Public ReadOnly Property Name As String Implements INamed.Name
        Get
            Return "Result"
        End Get
    End Property

    Public ReadOnly Property Result As Integer
        Get
            Return Wizard.Term1Step.Term1.Value + Wizard.Term2Step.Term2.Value
        End Get
    End Property

    Public Property ResultApproved As Boolean

    Public ReadOnly Property IsComplete As Boolean Implements IWizardStepViewModel.IsComplete
        Get
            Return ResultApproved
        End Get
    End Property

    Public Function GetConsoleCommand() As ConsoleCommand Implements IWizardStepViewModel.GetConsoleCommand
        Throw New NotImplementedException()
    End Function
End Class
