Imports System.Collections.ObjectModel
Imports SkyEditor.Core
Imports SkyEditor.Core.UI

Namespace ViewModels
    Public Class ApplicationErrors
        Inherits AnchorableViewModel

        Public Sub New(viewModel As ApplicationViewModel)
            MyBase.New(viewModel)
            Header = My.Resources.Language.Anchorable_Errors_Header
        End Sub

        Public ReadOnly Property Errors As ObservableCollection(Of ErrorInfo)
            Get
                Return CurrentApplicationViewModel.Errors
            End Get
        End Property
    End Class
End Namespace
