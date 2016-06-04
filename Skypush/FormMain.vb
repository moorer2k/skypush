Imports System.Threading
Imports ManagedWinapi
Imports ManagedWinapi.Hooks

Imports SKYPE4COMLib
Imports Skypush.ThemeBase

Public Class FormMain

#Region "Fields"

    Private ReadOnly _hkMan As New HotKeyMan()

    Private _hotKeySelected As Boolean
    Private _isPressed As Boolean
    Private WithEvents _skype As New Skype

#End Region 'Fields

#Region "Methods"

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        Try

            If _hotKeySelected Then

                LowLevelKeyboardHook1.Unhook()
                Hotkey1.Enabled = False

                Select Case keyData

                    Case Keys.Escape

                        CheckHkControls(False)

                        checkEnable.Focus()

                    Case Keys.Return

                        CheckHkControls(True)

                        checkEnable.Focus()

                        Return True

                    Case Keys.Space

                    Case Else

                        TextHotKey.Text = _hkMan.ProcessKeys(keyData)

                        Return True

                End Select

            End If

        Catch ex As Exception
            Debug.WriteLine(ex.Message)
        End Try

        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

    Private Sub CheckHkControls(save As Boolean)

        checkEnable.Toggled = My.Settings.HKEnabled

        If save Then

            My.Settings.HotKey = TextHotKey.Text
            My.Settings.Save()

            _hkMan.RefreshHotKeys(Hotkey1)

        End If

        If String.IsNullOrEmpty(My.Settings.HotKey) Then
            TextHotKey.Text = "Click here to set."
            labelHotKey.Text = "No hotkey(s) set."
        Else
            TextHotKey.Text = My.Settings.HotKey
            labelHotKey.Text = "Hotkey(s) assigned!"
        End If

        headHotKeys.Focus()


    End Sub

    Private Sub FormMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        My.Settings.HKEnabled = checkEnable.Toggled
        My.Settings.Save()
    End Sub

    Private Sub SetNotification(title As String, msg As String, type As MonoFlat_NotificationBox.Type)

        With notificationStatus
            .Title = title
            .Text = msg
            .NotificationType = type
            .Height = 132
            .Visible = True
            .BringToFront()
            .Show()
        End With

    End Sub
    Private Function CheckSkype()

        Dim isRunning As Boolean

        Try

            isRunning = Process.GetProcessesByName("Skype").FirstOrDefault().Responding

        Catch ex As Exception
            Return False
        End Try

        Return isRunning

    End Function

    Private Sub FormMain_Load(sender As Object, e As EventArgs) Handles Me.Load

        CheckHkControls(False)

        winControlBox.AboutForm = FormAbout

        If CheckSkype() Then
            _skype.Attach(5, False)
        Else
            SetNotification("SKYPE NOT RUNNING?", "Skype is either not open or the original file name has been changed. Re-Open Skype or use Skype.exe as the EXE name and try again.", 3)
        End If

    End Sub

    Private Sub Hotkey1_HotkeyPressed(sender As Object, e As EventArgs) Handles Hotkey1.HotkeyPressed

        _isPressed = True
        DirectCast(_skype, ISkype).Mute = False

        Debug.WriteLine("KeyPressed")

    End Sub

    Private Sub LowLevelKeyboardHook1_KeyIntercepted(msg As Integer, vkCode As Integer, scanCode As Integer, flags As Integer, time As Integer, dwExtraInfo As IntPtr, ByRef handled As Boolean) Handles LowLevelKeyboardHook1.KeyIntercepted

        If Not vkCode = Hotkey1.KeyCode Then

            _isPressed = False
            DirectCast(_skype, ISkype).Mute = True

            Debug.WriteLine("Unpressed")
        Else
            Debug.WriteLine("KeyPressed")

        End If

    End Sub

    Private Sub TextHotKey_Enter(sender As Object, e As EventArgs) Handles TextHotKey.Enter
        TextHotKey.Text = ""
        _hotKeySelected = True
        Hotkey1.Enabled = False
        labelHotKey.Text = "Return to Save / ESC to Cancel."
    End Sub

    Private Sub TextHotKey_Leave(sender As Object, e As EventArgs) Handles TextHotKey.Leave
        _hotKeySelected = False
        CheckHkControls(False)
    End Sub

    Private Sub _skype_AttachmentStatus(status As TAttachmentStatus) Handles _skype.AttachmentStatus
        Debug.WriteLine(status)
        Select Case status

            Case TAttachmentStatus.apiAttachAvailable

            Case TAttachmentStatus.apiAttachNotAvailable

            Case TAttachmentStatus.apiAttachPendingAuthorization
                SetNotification("CONFIRM ACCESS TO SKYPE", "Please check your Skype, the request should be displayed at the top section of whichever window you have displayed.", 0)

            Case TAttachmentStatus.apiAttachRefused

                SetNotification("ACCESS HAS BEEN BLOCKED!", "Skypush was blocked from Skype! Please go to Tools -> Options -> Advanced and look for 'Manage other programs access to Skype' then remove Skypush from the list.", 3)

            Case TAttachmentStatus.apiAttachSuccess

                notificationStatus.Hide()
                NotifyIcon1.ShowBalloonTip(100, "Successfuly attached to Skype!", "You may now use Skypush!", ToolTipIcon.Info)
                checkEnable.Enabled = True
                CheckHkControls(False)

            Case TAttachmentStatus.apiAttachUnknown




        End Select

    End Sub

    Private Sub _skype_Reply(pCommand As Command) Handles _skype.Reply
        'useful for detecting various other non-supported events from Skype
        'Debug.WriteLine(pCommand.Command)
    End Sub

#End Region 'Methods

    Private Sub checkEnable_ToggledChanged() Handles checkEnable.ToggledChanged

        My.Settings.HKEnabled = checkEnable.Toggled
        My.Settings.Save()

        If checkEnable.Toggled Then

            Hotkey1.Enabled = False

            Hotkey1 = New Hotkey()
            LowLevelKeyboardHook1 = New LowLevelKeyboardHook()

            _hkMan.RefreshHotKeys(Hotkey1)

            LowLevelKeyboardHook1.StartHook()

        Else

            Hotkey1.Dispose()
            LowLevelKeyboardHook1.Dispose()

        End If


    End Sub

End Class