Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports SkyEditor.Core.Projects
Imports SkyEditor.Core.UI

Namespace ViewModels.Projects
    Public Class ProjectReferencesViewModel
        Inherits GenericViewModel(Of Project)
        Implements INotifyPropertyChanged

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Public Property ProjectReferences As ObservableCollection(Of String)
            Get
                Return _projectReferences
            End Get
            Set(value As ObservableCollection(Of String))
                If _projectReferences IsNot value Then
                    _projectReferences = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(ProjectReferences)))
                End If
            End Set
        End Property
        Private _projectReferences As New ObservableCollection(Of String)

        Public Overrides Sub SetModel(model As Object)
            MyBase.SetModel(model)

            ProjectReferences = New ObservableCollection(Of String)(DirectCast(model, Project).ProjectReferenceNames)
        End Sub

        Public Overrides Sub UpdateModel(model As Object)
            MyBase.UpdateModel(model)

            DirectCast(model, Project).ProjectReferenceNames = ProjectReferences.ToList()
        End Sub
    End Class
End Namespace
