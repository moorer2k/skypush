Imports ManagedWinapi

Public Class HotKeyMan
    Public Property IsHotKeySet As Boolean
    Public Property HasMultipleKeys As Boolean
    Public Property HotKeys As New HashSet(Of Keys)
    Public Property HotKeysString As String

    Public Function ProcessKeys(keyData As Keys) As String

        Dim hotkey = ""

        Dim pKeys As String() = keyData.ToString().Split(",")

        Array.Reverse(pKeys)   'reverse order of array to display proper sequence of keys detected.

        For i = 0 To pKeys.Length - 1
            pKeys(i) = pKeys(i).Trim
            If i = 0 Then
                hotkey += pKeys(0)
            Else
                If pKeys(1).StartsWith(pKeys(0).Trim()) Then _
                    'detect if only one special key was pressed, like control etc. 
                    hotkey = pKeys(0).Trim()
                    Continue For
                End If
                hotkey += " + " & pKeys(i)
            End If
        Next


        Me.HotKeysString = hotkey

        Return hotkey

    End Function

    Public Function RefreshHotKeys(hotkey As Hotkey) As Hotkey

        Try

            If My.Settings.HotKey IsNot Nothing Then

                HotKeys = New HashSet(Of Keys)()

                Me.IsHotKeySet = True

                Dim hk As String = My.Settings.HotKey

                Dim hkCount As Integer = hk.Split(" + ").Count

                If hkCount <> 0 Then
                    For Each key In From key1 In hk.Split(" + ") Where Not key1 = "+"
                        Me.HotKeys.Add(GetKeyFromString(key.Trim))
                    Next
                    Me.HasMultipleKeys = True
                Else
                    Me.HasMultipleKeys = False
                    Me.HotKeys.Add(GetKeyFromString(hk))
                End If

            Else
                Me.IsHotKeySet = False
            End If


        Catch ex As Exception
            Debug.WriteLine("RefreshHotKeys[ERROR]: " & ex.Message)
        End Try

        Return SetHotKeys(hotkey)

    End Function

    Private Function SetHotKeys(hotkey As Hotkey) As Hotkey

        Try

            hotkey.Alt = False
            hotkey.Ctrl = False
            hotkey.Shift = False
            hotkey.WindowsKey = False

            For Each k In Me.HotKeys

                Select Case k.ToString()

                    Case "Control"
                        hotkey.Ctrl = True
                    Case "Alt"
                        hotkey.Alt = True
                    Case "Shift"
                        hotkey.Shift = True
                    Case "ShiftKey"
                        hotkey.Shift = True
                    Case "LWin"
                        hotkey.WindowsKey = True
                    Case "RWin"
                        hotkey.WindowsKey = True
                    Case Else
                        hotkey.KeyCode = k

                End Select

            Next

            hotkey.Enabled = True


        Catch ex As Exception

            Debug.WriteLine("SetHotKeys[ERROR]: " & ex.Message)

        End Try

        Return hotkey
    End Function

    Public Function GetKeyFromString(keyStr As String) As Keys
        Return [Enum].Parse(GetType(Keys), keyStr, True)
    End Function

End Class