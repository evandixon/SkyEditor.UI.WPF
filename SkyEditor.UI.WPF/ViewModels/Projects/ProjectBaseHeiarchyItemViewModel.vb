Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Windows
Imports SkyEditor.Core.Projects

Namespace ViewModels.Projects
    Public MustInherit Class ProjectBaseHeiarchyItemViewModel
        Implements INotifyPropertyChanged

        ''' <summary>
        ''' Creates a new instance of <see cref="ProjectBaseHeiarchyItemViewModel"/> representing the root node of the given project.
        ''' </summary>
        ''' <param name="project"></param>
        Public Sub New(project As ProjectBase)
            Me.Project = project
            Me.CurrentPath = ""
            IsDirectory = project.DirectoryExists(CurrentPath) 'Assumed to be file if it doesn't exist.
            Children = New ObservableCollection(Of ProjectBaseHeiarchyItemViewModel)
            PopulateChildren()
        End Sub

        ''' <summary>
        ''' Creates a new instance of <see cref="ProjectBaseHeiarchyItemViewModel"/>.
        ''' </summary>
        ''' <param name="project"></param>
        ''' <param name="parent"></param>
        ''' <param name="currentPath"></param>
        Public Sub New(project As ProjectBase, parent As ProjectBaseHeiarchyItemViewModel, currentPath As String)
            Me.Parent = parent
            Me.Project = project
            Me.CurrentPath = currentPath 'Order matters, because events.  This should be set after project and parent
            IsDirectory = project.DirectoryExists(currentPath) 'Assumed to be file if it doesn't exist.
            Children = New ObservableCollection(Of ProjectBaseHeiarchyItemViewModel)
            PopulateChildren()
        End Sub

#Region "Events"
        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
        Private Event CurrentPathChanged()
#End Region

#Region "Properties"
        Public Property Project As ProjectBase
            Get
                Return _project
            End Get
            Protected Set(value As ProjectBase)
                RemoveHandlers()
                _project = value
            End Set
        End Property
        'Not using VB's "WithEvents" keyword, sinc we only want the root node to handle events.
        'It's possible there are thousands of child nodes that we don't want to handle the event.
        'Saying "If IShouldHandleThis" in the handler thousands of times when there's only one node (the root) that should handle it could slow things down.
        Dim _project As ProjectBase

        Public Property CurrentPath As String
            Get
                Return _currentPath
            End Get
            Private Set(value As String)
                _currentPath = value
                RaiseEvent CurrentPathChanged()
            End Set
        End Property
        Dim _currentPath As String

        Public Property IsDirectory As Boolean
            Get
                Return _isDirectory
            End Get
            Private Set(value As Boolean)
                _isDirectory = value
            End Set
        End Property
        Dim _isDirectory As Boolean

        Public Property Prefix As String
            Get
                Return _prefix
            End Get
            Protected Set(value As String)
                If _prefix <> value Then
                    _prefix = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Prefix)))
                End If
            End Set
        End Property
        Dim _prefix As String

        Public Property Name As String
            Get
                Return _name
            End Get
            'Renaming is not yet supported.  When it is, change to Public Set (or simply Set), then rename the path in the project
            Private Set(value As String)
                If _name <> value Then
                    _name = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Name)))
                End If
            End Set
        End Property
        Dim _name As String

        Public Property Parent As ProjectBaseHeiarchyItemViewModel

        Public Property Children As ObservableCollection(Of ProjectBaseHeiarchyItemViewModel)

        ''' <summary>
        ''' Whether or not this is the root node.
        ''' </summary>
        ''' <returns>A boolean indicating whether this is the root node.</returns>
        ''' <remarks>Whether or not a node is the root node depends on whether or not there is a parent node.
        ''' If a node is the parent node, it is responsible for handling all events.</remarks>
        Public ReadOnly Property IsRoot As Boolean
            Get
                Return Parent Is Nothing
            End Get
        End Property


#End Region

#Region "Event Handlers"
        Private Sub ProjectBaseHeiarchyItemViewModel_CurrentPathChanged() Handles Me.CurrentPathChanged
            Name = IO.Path.GetFileName(CurrentPath.TrimEnd(""))
            IsDirectory = Project.DirectoryExists(CurrentPath) 'Assumed to be file if it doesn't exist.
            ResetHandlers()
        End Sub

        Private Sub Project_DirectoryCreated(sender As Object, e As DirectoryCreatedEventArgs)
            Application.Current.Dispatcher.Invoke(Sub()
                                                      Dim newNode = CreateNode(Me.Project, e.FullPath)
                                                      Dim parentNode = FindNode(IO.Path.GetDirectoryName(e.FullPath).Replace("\", "/").TrimEnd("/"))
                                                      parentNode.AddChild(newNode)
                                                  End Sub)
        End Sub

        Private Sub Project_DirectoryDeleted(sender As Object, e As DirectoryDeletedEventArgs)
            Application.Current.Dispatcher.Invoke(Sub()
                                                      Dim targetNode = FindNode(e.FullPath)
                                                      If targetNode IsNot Nothing Then
                                                          targetNode.Parent.RemoveChild(targetNode)
                                                      End If
                                                  End Sub)
        End Sub

        Private Sub Project_ItemAdded(sender As Object, e As ItemAddedEventArgs)
            Application.Current.Dispatcher.Invoke(Sub()
                                                      Dim newNode = CreateNode(Me.Project, e.FullPath)
                                                      Dim parentNode = FindNode(IO.Path.GetDirectoryName(e.FullPath).Replace("\", "/").TrimStart("/"))
                                                      parentNode.AddChild(newNode)
                                                  End Sub)
        End Sub

        Private Sub Project_ItemRemoved(sender As Object, e As ItemRemovedEventArgs)
            Application.Current.Dispatcher.Invoke(Sub()
                                                      Dim targetNode = FindNode(e.FullPath)
                                                      If targetNode IsNot Nothing Then
                                                          targetNode.Parent.RemoveChild(targetNode)
                                                      End If
                                                  End Sub)
        End Sub

        Private Sub RemoveHandlers()
            If _project IsNot Nothing Then
                RemoveHandler _project.DirectoryCreated, AddressOf Project_DirectoryCreated
                RemoveHandler _project.DirectoryDeleted, AddressOf Project_DirectoryDeleted
                RemoveHandler _project.ItemAdded, AddressOf Project_ItemAdded
                RemoveHandler _project.ItemRemoved, AddressOf Project_ItemRemoved
            End If
        End Sub

        Private Sub AddHandlers()
            If _project IsNot Nothing Then
                AddHandler _project.DirectoryCreated, AddressOf Project_DirectoryCreated
                AddHandler _project.DirectoryDeleted, AddressOf Project_DirectoryDeleted
                AddHandler _project.ItemAdded, AddressOf Project_ItemAdded
                AddHandler _project.ItemRemoved, AddressOf Project_ItemRemoved
            End If
        End Sub

        Private Sub ResetHandlers()
            RemoveHandlers()
            If IsRoot Then
                AddHandlers()
            End If
        End Sub
#End Region

        ''' <summary>
        ''' Creates a new instance of a node to represent the item at the given path.
        ''' </summary>
        ''' <param name="project"></param>
        ''' <param name="path"></param>
        ''' <returns></returns>
        Protected MustOverride Function CreateNode(project As ProjectBase, path As String) As ProjectBaseHeiarchyItemViewModel

        ''' <summary>
        ''' Finds the node with the given absolute path.  Should only be called on the root node, will fail on any other node.
        ''' </summary>
        ''' <param name="path"></param>
        ''' <returns>The <see cref="ProjectBaseHeiarchyItemViewModel"/> at the given path, or null if it does not exist.</returns>
        Protected Function FindNode(path As String) As ProjectBaseHeiarchyItemViewModel
            Dim pathParts = path.Split("/")

            Dim start As Integer
            If String.IsNullOrEmpty(pathParts(0)) Then
                'Path starts with slash
                start = 1
            Else
                'Path starts with name
                start = 0
            End If

            'Find the node
            Dim current = Me
            For count = start To pathParts.Length - 1
                If current Is Nothing Then
                    Exit For
                Else
                    current = current.FindItem(pathParts(count))
                End If
            Next
            Return current
        End Function

        ''' <summary>
        ''' Finds the child node with the given name.
        ''' </summary>
        ''' <param name="name"></param>
        ''' <returns>The <see cref="ProjectBaseHeiarchyItemViewModel"/> with the given name, or null if it does not exist.</returns>
        Protected Function FindItem(name As String) As ProjectBaseHeiarchyItemViewModel
            Return Me.Children.Where(Function(x) x.Name.ToLowerInvariant = name.ToLowerInvariant).SingleOrDefault
        End Function

        Public Sub AddChild(node As ProjectBaseHeiarchyItemViewModel)
            Me.Children.Add(node)
        End Sub

        Public Sub RemoveChild(name As String)
            Dim toRemove = FindItem(name)
            RemoveChild(toRemove)
        End Sub

        Public Sub RemoveChild(node As ProjectBaseHeiarchyItemViewModel)
            If node IsNot Nothing Then
                Me.Children.Remove(node)
            End If
        End Sub

        Public MustOverride Function CanDeleteCurrentNode() As Boolean

        Public MustOverride Sub RemoveCurrentNode()

        Protected Overridable Sub PopulateChildren()
            If IsDirectory Then
                'Create directories
                For Each item In Project.GetDirectories(CurrentPath, False)
                    AddChild(CreateNode(Project, item))
                Next

                'Create items
                For Each item In Project.GetItems(CurrentPath, False)
                    AddChild(CreateNode(Project, item.Key))
                Next
            End If
        End Sub

    End Class
End Namespace

