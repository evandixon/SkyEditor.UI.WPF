Imports System.Windows
Imports System.Windows.Controls
Imports SkyEditor.Core.UI
Imports SkyEditor.Core.Utilities

Public Class ObjectTab
    Inherits TabItem

    Public Sub New()
        Margin = New Thickness(0, 0, 0, 0)
    End Sub

    Public Sub New(containedViewControl As IViewControl)
        Me.New
        Me.ContainedViewControl = containedViewControl
    End Sub

    Public Property ContainedViewControl As IViewControl
        Get
            If TypeOf Me.Content Is IViewControl Then
                Return Me.Content
            Else
                Return Nothing
            End If
        End Get
        Set(value As IViewControl)
            If Me.Content IsNot Nothing AndAlso TypeOf Me.Content Is IViewControl Then
                RemoveHandler DirectCast(Me.Content, IViewControl).HeaderUpdated, AddressOf OnContentHeaderChanged
            End If

            If TypeOf value Is UserControl Then
                Me.Content = value
            End If

            If value.Header IsNot Nothing Then
                Me.Header = value.Header
            Else
                Me.Header = ReflectionHelpers.GetTypeFriendlyName(value.GetType)
            End If

            If Me.Content IsNot Nothing AndAlso TypeOf Me.Content Is IViewControl Then
                AddHandler DirectCast(Me.Content, IViewControl).HeaderUpdated, AddressOf OnContentHeaderChanged
            End If
        End Set
    End Property

    Private Sub OnContentHeaderChanged(sender As Object, e As HeaderUpdatedEventArgs)
        Me.Header = e.NewValue
    End Sub

End Class
