Imports System.ComponentModel
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
    Implements INotifyPropertyChanged

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Public ReadOnly Property Name As String Implements INamed.Name
        Get
            Return "Term 1"
        End Get
    End Property

    Public Property Term1 As Integer?
        Get
            Return _term1
        End Get
        Set(value As Integer?)
            _term1 = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Term1)))
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(IsComplete)))
        End Set
    End Property
    Private _term1 As Integer? = 0

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
    Implements INotifyPropertyChanged

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Public ReadOnly Property Name As String Implements INamed.Name
        Get
            Return "Term 2"
        End Get
    End Property

    Public Property Term2 As Integer?
        Get
            Return _term2
        End Get
        Set(value As Integer?)
            _term2 = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Term2)))
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(IsComplete)))
        End Set
    End Property
    Private _term2 As Integer?

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
    Implements INotifyPropertyChanged

    Public Sub New(ByVal wizard As AddingWizard)
        Me.Wizard = wizard
    End Sub

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

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
        Get
            Return _resultApproved
        End Get
        Set(value As Boolean)
            _resultApproved = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(ResultApproved)))
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(IsComplete)))
        End Set
    End Property
    Private _resultApproved As Boolean

    Public ReadOnly Property IsComplete As Boolean Implements IWizardStepViewModel.IsComplete
        Get
            Return ResultApproved
        End Get
    End Property

    Public Function GetConsoleCommand() As ConsoleCommand Implements IWizardStepViewModel.GetConsoleCommand
        Throw New NotImplementedException()
    End Function
End Class
