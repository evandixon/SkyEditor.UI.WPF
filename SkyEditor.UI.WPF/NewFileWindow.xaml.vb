Imports System.Windows
Imports System.Windows.Controls

Public Class NewFileWindow
    Inherits Window

    Private Sub btnOK_Click(sender As Object, e As RoutedEventArgs)
        DialogResult = True
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As RoutedEventArgs)
        DialogResult = False
        Me.Close()
    End Sub

    Private Sub NewFileWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        UpdateBtnOKEnabled()
    End Sub

    Private Sub txtName_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtName.TextChanged
        UpdateBtnOKEnabled()
    End Sub

    Private Sub cbType_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbType.SelectionChanged
        UpdateBtnOKEnabled()
    End Sub

    Private Sub UpdateBtnOKEnabled()
        btnOK.IsEnabled = Not String.IsNullOrEmpty(txtName.Text) AndAlso cbType.SelectedItem IsNot Nothing
    End Sub

    Public Property SelectedType As Type
        Get
            Return cbType.SelectedValue
        End Get
        Set(value As Type)
            cbType.SelectedValue = value
        End Set
    End Property

    Public ReadOnly Property SelectedName As String
        Get
            Return txtName.Text
        End Get
    End Property

    Public Sub SetGames(Games As Dictionary(Of String, Type))
        cbType.ItemsSource = Games
    End Sub

    Public Shadows Function ShowDialog() As Boolean
        Return MyBase.ShowDialog
    End Function

End Class

