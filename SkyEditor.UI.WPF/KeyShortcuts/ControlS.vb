Imports System.Windows.Input
Imports SkyEditor.Core.UI

Namespace KeyShortcuts
    Public Class ControlS
        Inherits ControlKeyAction

        Public Overrides ReadOnly Property Keys As Key()
            Get
                Return {Key.S}
            End Get
        End Property

        Private Async Function SaveFile(file As FileViewModel) As Task
            Dim manager = CurrentApplicationViewModel.CurrentPluginManager
            If file.CanSave(manager) Then
                Await file.Save(manager)
            ElseIf file.CanSaveAs(manager) Then
                Dim s = CurrentApplicationViewModel.GetSaveFileDialog(file)

                If s.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                    Await file.Save(s.FileName, manager)
                End If
            End If
        End Function

        Public Overrides Async Function DoAction() As Task
            'Save the current file
            Dim selectedFile = CurrentApplicationViewModel.SelectedFile
            If selectedFile IsNot Nothing Then
                Await SaveFile(selectedFile)
            End If

            'Save all
            If My.Computer.Keyboard.ShiftKeyDown Then
                'Save solution and projects
                If CurrentApplicationViewModel.CurrentSolution IsNot Nothing Then
                    Await CurrentApplicationViewModel.CurrentSolution.SaveWithAllProjects
                End If

                'Save other files
                For Each item In CurrentApplicationViewModel.OpenFiles
                    'But not the selected one, since it was already saved
                    If item IsNot Nothing AndAlso item IsNot selectedFile Then
                        Await SaveFile(item)
                    End If
                Next
            End If
        End Function
    End Class
End Namespace

