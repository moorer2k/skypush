Imports System.Threading
Imports SKYPE4COMLib

Public Class FormMain

    Private WithEvents _skype As New Skype
    Private ReadOnly _hkMan As New HotKeyMan()

    Private Sub FormMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        My.Settings.HKEnabled = checkEnable.Toggled
        My.Settings.Save()
    End Sub
    Private Sub FormMain_Load(sender As Object, e As EventArgs) Handles Me.Load

        If My.Settings.HKEnabled Then LowLevelKeyboardHook1.StartHook()

        CheckHKControls()

        winControlBox.AboutForm = FormAbout


        Dim tLoad As New Thread(CType(Sub()

                                          _skype.Attach(5, False)

                                      End Sub, ThreadStart))

        tLoad.Start()

        checkEnable.Toggled = My.Settings.HKEnabled

    End Sub
    Private Sub _skype_AttachmentStatus(status As TAttachmentStatus) Handles _skype.AttachmentStatus
        If status = TAttachmentStatus.apiAttachSuccess Then
            checkEnable.Enabled = True
            CheckHkControls()
        End If
    End Sub

    Private Sub _skype_Reply(pCommand As Command) Handles _skype.Reply
        'useful for detecting various other non-supported events from Skype
    End Sub

    Private Sub CheckHkControls()

        checkEnable.Toggled = My.Settings.HKEnabled

        If String.IsNullOrEmpty(My.Settings.HotKey) Then
            TextHotKey.Text = "Click here to set."
            labelHotKey.Text = "No hotkey(s) set."
        Else
            _hkMan.RefreshHotKeys(Hotkey1)
            TextHotKey.Text = My.Settings.HotKey
            labelHotKey.Text = "Hotkey(s) assigned!"
        End If

    End Sub

    Private _isPressed As Boolean
    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, ByVal keyData As Keys) As Boolean

        Try

            If _hotKeySelected Then

                Select Case keyData

                    Case Keys.Escape

                        CheckHKControls()

                        headOptions.Focus()

                    Case Keys.Return

                        _hkMan.RefreshHotKeys(Hotkey1)

                        CheckHKControls()

                        headOptions.Focus()

                        Return True

                    Case Keys.Space

                    Case Else

                        _hkMan.ProcessKeys(keyData)

                        TextHotKey.Text = My.Settings.HotKey

                        Return True

                End Select

            End If

        Catch ex As Exception
            Debug.WriteLine(ex.Message)
        End Try

        Return MyBase.ProcessCmdKey(msg, keyData)

    End Function

    Private _hotKeySelected As Boolean

    Private Sub TextHotKey_Enter(sender As Object, e As EventArgs) Handles TextHotKey.Enter

        TextHotKey.Text = ""
        _hotKeySelected = True
        Hotkey1.Enabled = False
        labelHotKey.Text = "Return to Save / ESC to Cancel."

    End Sub

    Private Sub TextHotKey_Leave(sender As Object, e As EventArgs) Handles TextHotKey.Leave

        _hotKeySelected = False
        CheckHkControls()

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

        End If

    End Sub

End Class