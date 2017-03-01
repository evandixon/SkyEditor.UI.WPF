Imports System.ComponentModel
Imports SkyEditor.Core
Imports SkyEditor.Core.Projects
Imports SkyEditor.Core.UI
Imports SkyEditor.Core.Utilities

Namespace ViewModels.Projects
    Public Class ProjectBaseBuildViewModel
        Inherits GenericViewModel(Of ProjectBase)
        Implements INotifyPropertyChanged

        Public Sub New(model As ProjectBase, appViewModel As ApplicationViewModel)
            MyBase.New(model, appViewModel)
        End Sub

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Public Property Name As String
            Get
                Return Model.Name
            End Get
            Set(value As String)
                If Not Model.Name = value Then
                    Model.Name = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Model.Name)))
                End If
            End Set
        End Property

        Public Property BuildStatusMessage As String
            Get
                Return Model.Message
            End Get
            Set(value As String)
                If Not Model.Message = value Then
                    Model.Message = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Model.Message)))
                End If
            End Set
        End Property

        Public Property BuildProgress As Single
            Get
                Return Model.Progress
            End Get
            Set(value As Single)
                If Not Model.Progress = value Then
                    Model.Progress = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Model.Progress)))
                End If
            End Set
        End Property

        Public Property IsBuildProgressIndeterminate As Boolean
            Get
                Return Model.IsIndeterminate
            End Get
            Set(value As Boolean)
                If Not Model.IsIndeterminate = value Then
                    Model.IsIndeterminate = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Model.IsIndeterminate)))
                End If
            End Set
        End Property

        Public Overrides Sub SetModel(model As Object)
            If Me.Model IsNot Nothing Then
                RemoveHandler Me.Model.ProgressChanged, AddressOf Project_BuildStatusChanged
            End If

            MyBase.SetModel(model)

            If Me.Model IsNot Nothing Then
                AddHandler Me.Model.ProgressChanged, AddressOf Project_BuildStatusChanged
            End If
        End Sub

        Private Sub Project_BuildStatusChanged(sender As Object, e As ProgressReportedEventArgs)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(BuildStatusMessage)))
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(BuildProgress)))
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(IsBuildProgressIndeterminate)))
        End Sub
    End Class
End Namespace
