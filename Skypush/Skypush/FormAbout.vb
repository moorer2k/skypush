Public Class FormAbout

    Private Sub FormAbout_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Hide()
        Parent = Nothing
        e.Cancel = True
    End Sub

    Private Sub MonoFlat_LinkLabel1_Click(sender As Object, e As EventArgs) Handles MonoFlat_LinkLabel1.Click

        Using p As New Process
            p.StartInfo.FileName = "http://moorer-software.com"
            p.Start()
        End Using

    End Sub
End Class