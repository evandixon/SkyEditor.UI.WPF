Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports SkyEditor.Core

Namespace Settings
    Public Module ISettingsProviderExtensions

        <Extension> Function GetMainWindowHeight(provider As ISettingsProvider) As Double?
            Dim setting = provider.GetSetting(My.Resources.SettingNames.MainWindowHeight)
            If TypeOf setting Is Double Then
                Return setting
            Else
                Return Nothing
            End If
        End Function

        <Extension> Sub SetMainWindowHeight(provider As ISettingsProvider, value As Double)
            provider.SetSetting(My.Resources.SettingNames.MainWindowHeight, value)
        End Sub

        <Extension> Function GetMainWindowWidth(provider As ISettingsProvider) As Double?
            Dim setting = provider.GetSetting(My.Resources.SettingNames.MainWindowWidth)
            If TypeOf setting Is Double Then
                Return setting
            Else
                Return Nothing
            End If
        End Function

        <Extension> Sub SetMainWindowWidth(provider As ISettingsProvider, value As Double)
            provider.SetSetting(My.Resources.SettingNames.MainWindowWidth, value)
        End Sub

        <Extension> Function GetMainWindowIsMaximized(provider As ISettingsProvider) As Boolean
            Dim setting = provider.GetSetting(My.Resources.SettingNames.MainWindowMaximized)
            If TypeOf setting Is Boolean Then
                Return setting
            Else
                Return False
            End If
        End Function

        <Extension> Sub SetMainWindowIsMaximized(provider As ISettingsProvider, value As Boolean)
            provider.SetSetting(My.Resources.SettingNames.MainWindowMaximized, value)
        End Sub

        <Extension> Function GetFont(provider As ISettingsProvider) As Font
            Dim setting = provider.GetSetting(My.Resources.SettingNames.Font)
            If TypeOf setting Is Font Then
                Return setting
            Else
                Return Nothing
            End If
        End Function

        <Extension> Sub SetFont(provider As ISettingsProvider, value As Font)
            provider.SetSetting(My.Resources.SettingNames.Font, value)
        End Sub

    End Module
End Namespace

