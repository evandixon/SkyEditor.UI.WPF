Imports System.Reflection
Imports System.Windows
Imports System.Windows.Controls
Imports SkyEditor.Core
Imports SkyEditor.Core.UI
Imports SkyEditor.Core.Utilities

Public Class WpfUiHelper

    ''' <summary>
    ''' Generates ObjectTabs using the given ObjectControls
    ''' </summary>
    ''' <param name="ObjectControls"></param>
    ''' <returns></returns>
    Public Shared Function GenerateObjectTabs(objectControls As IEnumerable(Of IObjectControl)) As List(Of ObjectTab)
        If objectControls Is Nothing Then
            Throw New ArgumentNullException(NameOf(objectControls))
        End If

        Dim output As New List(Of ObjectTab)

        For Each item In objectControls
            output.Add(New ObjectTab(item))
        Next

        Return output
    End Function

    ''' <summary>
    ''' Recursively pdates the visibility of the MenuItems based on the currently selected Types.
    ''' </summary>
    ''' <param name="SelectedObjects">IEnumerable of the currently seleted objects.  Used to determine visibility.</param>
    ''' <param name="MainMenu">Menu containing menu items of which to update the visibility.</param>
    Public Shared Sub UpdateMenuItemVisibility(selectedObjects As IEnumerable(Of Object), mainMenu As Menu)
        If selectedObjects Is Nothing Then
            Throw New ArgumentNullException(NameOf(selectedObjects))
        End If
        If mainMenu Is Nothing Then
            Throw New ArgumentNullException(NameOf(mainMenu))
        End If
        For Each item As MenuItem In mainMenu.Items
            UpdateMenuItemVisibility(selectedObjects, item)
        Next
    End Sub

    ''' <summary>
    ''' Recursively pdates the visibility of the MenuItems based on the currently selected Types.
    ''' </summary>
    ''' <param name="SelectedObjects">IEnumerable of the currently seleted objects.  Used to determine visibility.</param>
    ''' <param name="Parents">MenuItems of which to recursively update the visibility.</param>
    Public Shared Sub UpdateMenuItemVisibility(selectedObjects As IEnumerable(Of Object), parents As IEnumerable(Of MenuItem))
        If selectedObjects Is Nothing Then
            Throw New ArgumentNullException(NameOf(selectedObjects))
        End If
        If parents Is Nothing Then
            Throw New ArgumentNullException(NameOf(parents))
        End If

        For Each item In parents
            UpdateMenuItemVisibility(selectedObjects, item)
        Next
    End Sub

    ''' <summary>
    ''' Recursively pdates the visibility of the MenuItems based on the currently selected Types.
    ''' </summary>
    ''' <param name="SelectedObjects">IEnumerable of the currently seleted objects.  Used to determine visibility.</param>
    ''' <param name="Parent">Root menu item of which to recursively update the visibility.</param>
    Public Shared Async Sub UpdateMenuItemVisibility(selectedObjects As IEnumerable(Of Object), parent As MenuItem)
        If selectedObjects Is Nothing Then
            Throw New ArgumentNullException(NameOf(selectedObjects))
        End If
        If parent Is Nothing Then
            Throw New ArgumentNullException(NameOf(parent))
        End If

        For Each item In parent.Items
            UpdateMenuItemVisibility(selectedObjects, item)
        Next
        'If this tag has at least one action
        If parent.Tag IsNot Nothing AndAlso TypeOf parent.Tag Is List(Of MenuAction) AndAlso DirectCast(parent.Tag, List(Of MenuAction)).Count > 0 Then
            Dim tags = DirectCast(parent.Tag, List(Of MenuAction))
            Dim hasMatch As Boolean = False
            'Each menu item has one or more menu action
            For Each tag In tags

                If Not hasMatch Then
                    'Each action can target multiple things
                    hasMatch = tag.AlwaysVisible OrElse Await tag.SupportsObjects(selectedObjects)
                Else
                    Exit For
                End If

            Next

            If hasMatch Then
                parent.Visibility = Visibility.Visible
            Else
                parent.Visibility = Visibility.Collapsed
            End If
        End If
        'If this tag has child tags
        If parent.HasItems Then
            'This menu item doesn't have an action.
            'Setting visibility to whether or not it has visible children.
            If MenuItemHasVisibleChildren(parent) Then
                parent.Visibility = Visibility.Visible
            Else
                parent.Visibility = Visibility.Collapsed
            End If
        End If
    End Sub

    ''' <summary>
    ''' Determines whether or not the given menu item has visible children.
    ''' </summary>
    ''' <param name="Item"></param>
    Public Shared Function MenuItemHasVisibleChildren(item As MenuItem) As Boolean
        Dim q = From m As MenuItem In item.Items Where m.Visibility = Visibility.Visible

        Return q.Any
    End Function

End Class
