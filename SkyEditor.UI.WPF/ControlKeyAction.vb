Imports System.Windows.Input
''' <summary>
''' A class that can respond to key press event
''' </summary>
Public MustInherit Class ControlKeyAction

    Public Property CurrentApplicationViewModel As WPFApplicationViewModel

    ''' <summary>
    ''' The combination of keys that must be pressed, in addition to Control, for the action to trigger
    ''' </summary>
    Public MustOverride ReadOnly Property Keys As Key()

    Public MustOverride Function DoAction() As Task

End Class
