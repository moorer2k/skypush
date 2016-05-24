﻿Imports System.ComponentModel
Imports ManagedWinapi
Imports ManagedWinapi.Hooks
Imports Microsoft.VisualBasic.CompilerServices
Imports Skypush.ThemeBase

<DesignerGenerated()> _
Partial Class FormMain
    Inherits Form

    'Form overrides dispose to clean up the component list.
    <DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Hotkey1 = New ManagedWinapi.Hotkey(Me.components)
        Me.LowLevelKeyboardHook1 = New ManagedWinapi.Hooks.LowLevelKeyboardHook()
        Me.MonoFlat_ThemeContainer1 = New Skypush.ThemeBase.MonoFlatThemeContainer()
        Me.winControlBox = New Skypush.ThemeBase.MonoFlat_ControlBox()
        Me.checkEnable = New Skypush.ThemeBase.MonoFlat_Toggle()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.panelHotKeys = New Skypush.ThemeBase.MonoFlat_Panel()
        Me.headHotKeys = New Skypush.ThemeBase.MonoFlat_HeaderLabel()
        Me.labelHotKey = New Skypush.ThemeBase.MonoFlat_Label()
        Me.TextHotKey = New Skypush.ThemeBase.MonoFlat_TextBox()
        Me.panelOptions = New Skypush.ThemeBase.MonoFlat_Panel()
        Me.headOptions = New Skypush.ThemeBase.MonoFlat_HeaderLabel()
        Me.checkStartup = New Skypush.ThemeBase.MonoFlat_CheckBox()
        Me.MonoFlat_ThemeContainer1.SuspendLayout()
        Me.panelHotKeys.SuspendLayout()
        Me.panelOptions.SuspendLayout()
        Me.SuspendLayout()
        '
        'Hotkey1
        '
        Me.Hotkey1.Alt = False
        Me.Hotkey1.Ctrl = False
        Me.Hotkey1.Enabled = False
        Me.Hotkey1.KeyCode = System.Windows.Forms.Keys.None
        Me.Hotkey1.Shift = False
        Me.Hotkey1.WindowsKey = False
        '
        'LowLevelKeyboardHook1
        '
        Me.LowLevelKeyboardHook1.Type = ManagedWinapi.Hooks.HookType.WH_KEYBOARD_LL
        '
        'MonoFlat_ThemeContainer1
        '
        Me.MonoFlat_ThemeContainer1.BackColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.MonoFlat_ThemeContainer1.Controls.Add(Me.winControlBox)
        Me.MonoFlat_ThemeContainer1.Controls.Add(Me.checkEnable)
        Me.MonoFlat_ThemeContainer1.Controls.Add(Me.Button1)
        Me.MonoFlat_ThemeContainer1.Controls.Add(Me.panelHotKeys)
        Me.MonoFlat_ThemeContainer1.Controls.Add(Me.panelOptions)
        Me.MonoFlat_ThemeContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MonoFlat_ThemeContainer1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.MonoFlat_ThemeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.MonoFlat_ThemeContainer1.Name = "MonoFlat_ThemeContainer1"
        Me.MonoFlat_ThemeContainer1.Padding = New System.Windows.Forms.Padding(10, 70, 10, 9)
        Me.MonoFlat_ThemeContainer1.RoundCorners = False
        Me.MonoFlat_ThemeContainer1.Sizable = False
        Me.MonoFlat_ThemeContainer1.Size = New System.Drawing.Size(226, 278)
        Me.MonoFlat_ThemeContainer1.SmartBounds = True
        Me.MonoFlat_ThemeContainer1.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.MonoFlat_ThemeContainer1.TabIndex = 17
        Me.MonoFlat_ThemeContainer1.Text = "Skypush"
        '
        'winControlBox
        '
        Me.winControlBox.AboutForm = Nothing
        Me.winControlBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.winControlBox.EnableAbout = True
        Me.winControlBox.EnableHoverHighlight = True
        Me.winControlBox.EnableMinimizeButton = True
        Me.winControlBox.Location = New System.Drawing.Point(116, 12)
        Me.winControlBox.Name = "winControlBox"
        Me.winControlBox.Size = New System.Drawing.Size(100, 25)
        Me.winControlBox.TabIndex = 19
        Me.winControlBox.Text = "MonoFlat_ControlBox1"
        '
        'checkEnable
        '
        Me.checkEnable.Location = New System.Drawing.Point(13, 233)
        Me.checkEnable.Name = "checkEnable"
        Me.checkEnable.Size = New System.Drawing.Size(76, 33)
        Me.checkEnable.TabIndex = 2
        Me.checkEnable.Toggled = False
        Me.checkEnable.Type = Skypush.ThemeBase.MonoFlat_Toggle._Type.OnOff
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(300, 145)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(46, 24)
        Me.Button1.TabIndex = 18
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'panelHotKeys
        '
        Me.panelHotKeys.BackColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(63, Byte), Integer))
        Me.panelHotKeys.Controls.Add(Me.headHotKeys)
        Me.panelHotKeys.Controls.Add(Me.labelHotKey)
        Me.panelHotKeys.Controls.Add(Me.TextHotKey)
        Me.panelHotKeys.Location = New System.Drawing.Point(10, 134)
        Me.panelHotKeys.Name = "panelHotKeys"
        Me.panelHotKeys.Padding = New System.Windows.Forms.Padding(5)
        Me.panelHotKeys.Size = New System.Drawing.Size(206, 88)
        Me.panelHotKeys.TabIndex = 17
        Me.panelHotKeys.Text = "MonoFlat_Panel2"
        '
        'headHotKeys
        '
        Me.headHotKeys.AutoSize = True
        Me.headHotKeys.BackColor = System.Drawing.Color.Transparent
        Me.headHotKeys.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.headHotKeys.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.headHotKeys.Location = New System.Drawing.Point(7, 0)
        Me.headHotKeys.Name = "headHotKeys"
        Me.headHotKeys.Size = New System.Drawing.Size(65, 15)
        Me.headHotKeys.TabIndex = 20
        Me.headHotKeys.Text = "HotKey(s):"
        '
        'labelHotKey
        '
        Me.labelHotKey.BackColor = System.Drawing.Color.Transparent
        Me.labelHotKey.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.labelHotKey.ForeColor = System.Drawing.Color.FromArgb(CType(CType(116, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(132, Byte), Integer))
        Me.labelHotKey.Location = New System.Drawing.Point(11, 62)
        Me.labelHotKey.Name = "labelHotKey"
        Me.labelHotKey.Size = New System.Drawing.Size(184, 19)
        Me.labelHotKey.TabIndex = 19
        Me.labelHotKey.Text = "No hotkey set."
        Me.labelHotKey.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TextHotKey
        '
        Me.TextHotKey.BackColor = System.Drawing.Color.Transparent
        Me.TextHotKey.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.TextHotKey.ForeColor = System.Drawing.Color.FromArgb(CType(CType(176, Byte), Integer), CType(CType(183, Byte), Integer), CType(CType(191, Byte), Integer))
        Me.TextHotKey.Image = Nothing
        Me.TextHotKey.Location = New System.Drawing.Point(11, 29)
        Me.TextHotKey.MaxLength = 32767
        Me.TextHotKey.Multiline = False
        Me.TextHotKey.Name = "TextHotKey"
        Me.TextHotKey.ReadOnly = False
        Me.TextHotKey.Size = New System.Drawing.Size(184, 30)
        Me.TextHotKey.TabIndex = 21
        Me.TextHotKey.Text = "Click here to set."
        Me.TextHotKey.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left
        Me.TextHotKey.UseSystemPasswordChar = False
        '
        'panelOptions
        '
        Me.panelOptions.BackColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(63, Byte), Integer))
        Me.panelOptions.Controls.Add(Me.headOptions)
        Me.panelOptions.Controls.Add(Me.checkStartup)
        Me.panelOptions.Location = New System.Drawing.Point(10, 73)
        Me.panelOptions.Name = "panelOptions"
        Me.panelOptions.Padding = New System.Windows.Forms.Padding(5)
        Me.panelOptions.Size = New System.Drawing.Size(206, 55)
        Me.panelOptions.TabIndex = 16
        Me.panelOptions.Text = "MonoFlat_Panel1"
        '
        'headOptions
        '
        Me.headOptions.AutoSize = True
        Me.headOptions.BackColor = System.Drawing.Color.Transparent
        Me.headOptions.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.headOptions.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.headOptions.Location = New System.Drawing.Point(8, 0)
        Me.headOptions.Name = "headOptions"
        Me.headOptions.Size = New System.Drawing.Size(53, 15)
        Me.headOptions.TabIndex = 21
        Me.headOptions.Text = "Options:"
        '
        'checkStartup
        '
        Me.checkStartup.Checked = False
        Me.checkStartup.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.checkStartup.Location = New System.Drawing.Point(11, 27)
        Me.checkStartup.Name = "checkStartup"
        Me.checkStartup.Size = New System.Drawing.Size(184, 16)
        Me.checkStartup.TabIndex = 17
        Me.checkStartup.Text = "Start with windows"
        '
        'FormMain
        '
        Me.ClientSize = New System.Drawing.Size(226, 278)
        Me.Controls.Add(Me.MonoFlat_ThemeContainer1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Skypush"
        Me.TransparencyKey = System.Drawing.Color.Fuchsia
        Me.MonoFlat_ThemeContainer1.ResumeLayout(False)
        Me.panelHotKeys.ResumeLayout(False)
        Me.panelHotKeys.PerformLayout()
        Me.panelOptions.ResumeLayout(False)
        Me.panelOptions.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents MonoFlat_ThemeContainer1 As MonoFlatThemeContainer
    Friend WithEvents panelOptions As MonoFlat_Panel
    Friend WithEvents checkStartup As MonoFlat_CheckBox
    Friend WithEvents panelHotKeys As MonoFlat_Panel
    Friend WithEvents headHotKeys As MonoFlat_HeaderLabel
    Friend WithEvents labelHotKey As MonoFlat_Label
    Friend WithEvents headOptions As MonoFlat_HeaderLabel
    Friend WithEvents Button1 As Button
    Friend WithEvents TextHotKey As MonoFlat_TextBox
    Friend WithEvents LowLevelKeyboardHook1 As LowLevelKeyboardHook
    Public WithEvents Hotkey1 As Hotkey
    Friend WithEvents winControlBox As Skypush.ThemeBase.MonoFlat_ControlBox
    Friend WithEvents checkEnable As Skypush.ThemeBase.MonoFlat_Toggle

End Class