Imports System.Reflection
Imports SkyEditor.Core.IO
Imports SkyEditor.Core.Projects

Namespace ObjectControls
    Public Class ProjectReferences
        Inherits ObjectControl

        Public Overrides Sub RefreshDisplay()
            MyBase.RefreshDisplay()
            With GetEditingObject(Of Project)()
                listReferences.ItemsSource = .ProjectReferenceNames
            End With
        End Sub

        Public Overrides Sub UpdateObject()
            MyBase.UpdateObject()
            With GetEditingObject(Of Project)()
                .ProjectReferenceNames = listReferences.ItemsSource
            End With
        End Sub

        Public Overrides Function GetSupportedTypes() As IEnumerable(Of TypeInfo)
            Return {GetType(Project)}
        End Function
    End Class

End Namespace

