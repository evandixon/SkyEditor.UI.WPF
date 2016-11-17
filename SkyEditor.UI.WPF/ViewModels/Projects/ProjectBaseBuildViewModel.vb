Imports System.ComponentModel
Imports SkyEditor.Core
Imports SkyEditor.Core.Projects
Imports SkyEditor.Core.UI
Imports SkyEditor.Core.Utilities

Namespace ViewModels.Projects
    Public Class ProjectBaseBuildViewModel
        Inherits GenericViewModel(Of ProjectBase)
        Implements INotifyPropertyChanged

        Public Sub New(model As ProjectBase, manager As PluginManager)
            MyBase.New(model, manager)
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
                Return Model.BuildStatusMessage
            End Get
            Set(value As String)
                If Not Model.BuildStatusMessage = value Then
                    Model.BuildStatusMessage = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Model.BuildStatusMessage)))
                End If
            End Set
        End Property

        Public Property BuildProgress As Single
            Get
                Return Model.BuildProgress
            End Get
            Set(value As Single)
                If Not Model.BuildProgress = value Then
                    Model.BuildProgress = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Model.BuildProgress)))
                End If
            End Set
        End Property

        Public Property IsBuildProgressIndeterminate As Boolean
            Get
                Return Model.IsBuildProgressIndeterminate
            End Get
            Set(value As Boolean)
                If Not Model.IsBuildProgressIndeterminate = value Then
                    Model.IsBuildProgressIndeterminate = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Model.IsBuildProgressIndeterminate)))
                End If
            End Set
        End Property

        Public Overrides Sub SetModel(model As Object)
            If Me.Model IsNot Nothing Then
                RemoveHandler Me.Model.BuildStatusChanged, AddressOf Project_BuildStatusChanged
            End If

            MyBase.SetModel(model)

            If Me.Model IsNot Nothing Then
                AddHandler Me.Model.BuildStatusChanged, AddressOf Project_BuildStatusChanged
            End If
        End Sub

        Private Sub Project_BuildStatusChanged(sender As Object, e As ProgressReportedEventArgs)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Model.BuildStatusMessage)))
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Model.BuildProgress)))
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Model.IsBuildProgressIndeterminate)))
        End Sub
    End Class
End Namespace
