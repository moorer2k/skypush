Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports System.Drawing.Text

'|------DO-NOT-REMOVE------|
'
' Creator: HazelDev
' Site   : HazelDev.com
' Created: 12.Sep.2014
' Changed: 26.Sep.2014
' Version: 1.0.0
' Modified by MooreR
'
'|------DO-NOT-REMOVE------|
Namespace ThemeBase

    Class MonoFlatThemeContainer
        Inherits ContainerControl

#Region "Fields"

        Protected State As MouseState

        Private B1x, B2x, B3, B4 As Boolean
        Private Cap As Boolean = False
        Private Current, Previous As Integer
        Private GetIndexPoint As Point
        Private HasShown As Boolean
        Private HeaderRect As Rectangle
        Private Messages(8) As Message
        Private MouseP As New Point(0, 0)
        Private MoveHeight As Integer
        Private WM_LMBUTTONDOWN As Boolean
        Private _ControlMode As Boolean
        Private _IsParentForm As Boolean
        Private _RoundCorners As Boolean = True
        Private _Sizable As Boolean = True
        Private _SmartBounds As Boolean = True
        Private _StartPosition As FormStartPosition

#End Region 'Fields

#Region "Constructors"

        Sub New()
            SetStyle(DirectCast(139270, ControlStyles), True)
            BackColor = Color.FromArgb(44, 62, 80)
            Padding = New Padding(10, 70, 10, 9)
            DoubleBuffered = True
            Dock = DockStyle.Fill
            MoveHeight = 66
            Font = New Font("Segoe UI", 9)
        End Sub

#End Region 'Constructors

#Region "Enumerations"

        Enum MouseState As Byte
            None = 0
            Over = 1
            Down = 2
            Block = 3
        End Enum

#End Region 'Enumerations

#Region "Properties"

        Protected Property ControlMode() As Boolean
            Get
                Return _ControlMode
            End Get
            Set(ByVal v As Boolean)
                _ControlMode = v
                Invalidate()
            End Set
        End Property

        Protected ReadOnly Property IsParentForm() As Boolean
            Get
                Return _IsParentForm
            End Get
        End Property

        Protected ReadOnly Property IsParentMdi() As Boolean
            Get
                If Parent Is Nothing Then Return False
                Return Parent.Parent IsNot Nothing
            End Get
        End Property

        Property RoundCorners() As Boolean
            Get
                Return _RoundCorners
            End Get
            Set(ByVal value As Boolean)
                _RoundCorners = value
                Invalidate()
            End Set
        End Property

        Property Sizable() As Boolean
            Get
                Return _Sizable
            End Get
            Set(ByVal value As Boolean)
                _Sizable = value
            End Set
        End Property

        Property SmartBounds() As Boolean
            Get
                Return _SmartBounds
            End Get
            Set(ByVal value As Boolean)
                _SmartBounds = value
            End Set
        End Property

        Property StartPosition() As FormStartPosition
            Get
                If _IsParentForm AndAlso Not _ControlMode Then Return ParentForm.StartPosition Else Return _StartPosition
            End Get
            Set(ByVal value As FormStartPosition)
                _StartPosition = value

                If _IsParentForm AndAlso Not _ControlMode Then
                    ParentForm.StartPosition = value
                End If
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Protected Overrides Sub CreateHandle()
            MyBase.CreateHandle()
        End Sub

        Protected Overrides Sub OnInvalidated(ByVal e As InvalidateEventArgs)
            MyBase.OnInvalidated(e)
            ParentForm.Text = Text
        End Sub

        Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
            MyBase.OnMouseDown(e)
            Focus()
            If e.Button = MouseButtons.Left Then SetState(MouseState.Down)
            If Not (_IsParentForm AndAlso ParentForm.WindowState = FormWindowState.Maximized OrElse _ControlMode) Then
                If HeaderRect.Contains(e.Location) Then
                    Capture = False
                    WM_LMBUTTONDOWN = True
                    DefWndProc(Messages(0))
                ElseIf _Sizable AndAlso Not Previous = 0 Then
                    Capture = False
                    WM_LMBUTTONDOWN = True
                    DefWndProc(Messages(Previous))
                End If
            End If
        End Sub

        Protected Overrides Sub OnMouseMove(ByVal e As MouseEventArgs)
            MyBase.OnMouseMove(e)
            If Not (_IsParentForm AndAlso ParentForm.WindowState = FormWindowState.Maximized) Then
                If _Sizable AndAlso Not _ControlMode Then InvalidateMouse()
            End If
            If Cap Then

                Parent.Location = Point.Subtract(MousePosition, MouseP)

            End If
        End Sub

        Protected Overrides Sub OnMouseUp(ByVal e As MouseEventArgs)
            MyBase.OnMouseUp(e)
            Cap = False
        End Sub

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            MyBase.OnPaint(e)
            Dim G As Graphics = e.Graphics

            G.Clear(Color.FromArgb(44, 62, 80))
            G.FillRectangle(New SolidBrush(Color.FromArgb(26, 188, 156)), New Rectangle(0, 0, Width, 60))

            If _RoundCorners = True Then
                ' Draw Left upper corner
                G.FillRectangle(Brushes.Fuchsia, 0, 0, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, 1, 0, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, 2, 0, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, 3, 0, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, 0, 1, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, 0, 2, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, 0, 3, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, 1, 1, 1, 1)

                G.FillRectangle(New SolidBrush(Color.FromArgb(26, 188, 156)), 1, 3, 1, 1)
                G.FillRectangle(New SolidBrush(Color.FromArgb(26, 188, 156)), 1, 2, 1, 1)
                G.FillRectangle(New SolidBrush(Color.FromArgb(26, 188, 156)), 2, 1, 1, 1)
                G.FillRectangle(New SolidBrush(Color.FromArgb(26, 188, 156)), 3, 1, 1, 1)

                ' Draw right upper corner
                G.FillRectangle(Brushes.Fuchsia, Width - 1, 0, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, Width - 2, 0, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, Width - 3, 0, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, Width - 4, 0, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, Width - 1, 1, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, Width - 1, 2, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, Width - 1, 3, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, Width - 2, 1, 1, 1)

                G.FillRectangle(New SolidBrush(Color.FromArgb(26, 188, 156)), Width - 2, 3, 1, 1)
                G.FillRectangle(New SolidBrush(Color.FromArgb(26, 188, 156)), Width - 2, 2, 1, 1)
                G.FillRectangle(New SolidBrush(Color.FromArgb(26, 188, 156)), Width - 3, 1, 1, 1)
                G.FillRectangle(New SolidBrush(Color.FromArgb(26, 188, 156)), Width - 4, 1, 1, 1)

                ' Draw Left bottom corner
                G.FillRectangle(Brushes.Fuchsia, 0, Height - 1, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, 0, Height - 2, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, 0, Height - 3, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, 0, Height - 4, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, 1, Height - 1, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, 2, Height - 1, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, 3, Height - 1, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, 1, Height - 1, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, 1, Height - 2, 1, 1)

                G.FillRectangle(New SolidBrush(Color.FromArgb(44, 62, 80)), 1, Height - 3, 1, 1)
                G.FillRectangle(New SolidBrush(Color.FromArgb(44, 62, 80)), 1, Height - 4, 1, 1)
                G.FillRectangle(New SolidBrush(Color.FromArgb(44, 62, 80)), 3, Height - 2, 1, 1)
                G.FillRectangle(New SolidBrush(Color.FromArgb(44, 62, 80)), 2, Height - 2, 1, 1)

                ' Draw right bottom corner
                G.FillRectangle(Brushes.Fuchsia, Width - 1, Height, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, Width - 2, Height, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, Width - 3, Height, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, Width - 4, Height, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, Width - 1, Height - 1, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, Width - 1, Height - 2, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, Width - 1, Height - 3, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, Width - 2, Height - 1, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, Width - 3, Height - 1, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, Width - 4, Height - 1, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, Width - 1, Height - 4, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, Width - 2, Height - 2, 1, 1)

                G.FillRectangle(New SolidBrush(Color.FromArgb(44, 62, 80)), Width - 2, Height - 3, 1, 1)
                G.FillRectangle(New SolidBrush(Color.FromArgb(44, 62, 80)), Width - 2, Height - 4, 1, 1)
                G.FillRectangle(New SolidBrush(Color.FromArgb(44, 62, 80)), Width - 4, Height - 2, 1, 1)
                G.FillRectangle(New SolidBrush(Color.FromArgb(44, 62, 80)), Width - 3, Height - 2, 1, 1)
            End If

            G.DrawString(Text, New Font("Microsoft Sans Serif", 12, FontStyle.Bold), New SolidBrush(Color.FromArgb(255, 254, 255)), New Rectangle(20, 20, Width - 1, Height), New StringFormat() With {.Alignment = StringAlignment.Near, .LineAlignment = StringAlignment.Near})
        End Sub

        Protected Overrides Sub OnPaintBackground(e As PaintEventArgs)
            MyBase.OnPaintBackground(e)
        End Sub

        Protected NotOverridable Overrides Sub OnParentChanged(ByVal e As EventArgs)
            MyBase.OnParentChanged(e)

            If Parent Is Nothing Then Return
            _IsParentForm = TypeOf Parent Is Form

            If Not _ControlMode Then
                InitializeMessages()

                If _IsParentForm Then
                    Me.ParentForm.FormBorderStyle = FormBorderStyle.None
                    Me.ParentForm.TransparencyKey = Color.Fuchsia

                    If Not DesignMode Then
                        AddHandler ParentForm.Shown, AddressOf FormShown
                    End If
                End If
                Parent.BackColor = BackColor
                '   Parent.MinimumSize = New Size(261, 65)
            End If
        End Sub

        Protected NotOverridable Overrides Sub OnSizeChanged(ByVal e As EventArgs)
            MyBase.OnSizeChanged(e)
            If Not _ControlMode Then HeaderRect = New Rectangle(0, 0, Width - 14, MoveHeight - 7)
            Invalidate()
        End Sub

        Protected Overrides Sub OnTextChanged(ByVal e As EventArgs)
            MyBase.OnTextChanged(e)
            Invalidate()
        End Sub

        Protected Overrides Sub WndProc(ByRef m As Message)
            MyBase.WndProc(m)

            If WM_LMBUTTONDOWN AndAlso m.Msg = 513 Then
                WM_LMBUTTONDOWN = False

                SetState(MouseState.Over)
                If Not _SmartBounds Then Return

                If IsParentMdi Then
                    CorrectBounds(New Rectangle(Point.Empty, Parent.Parent.Size))
                Else
                    CorrectBounds(Screen.FromControl(Parent).WorkingArea)
                End If
            End If
        End Sub

        Private Sub CorrectBounds(ByVal bounds As Rectangle)
            If Parent.Width > bounds.Width Then Parent.Width = bounds.Width
            If Parent.Height > bounds.Height Then Parent.Height = bounds.Height

            Dim X As Integer = Parent.Location.X
            Dim Y As Integer = Parent.Location.Y

            If X < bounds.X Then X = bounds.X
            If Y < bounds.Y Then Y = bounds.Y

            Dim Width As Integer = bounds.X + bounds.Width
            Dim Height As Integer = bounds.Y + bounds.Height

            If X + Parent.Width > Width Then X = Width - Parent.Width
            If Y + Parent.Height > Height Then Y = Height - Parent.Height

            Parent.Location = New Point(X, Y)
        End Sub

        Private Sub FormShown(ByVal sender As Object, ByVal e As EventArgs)
            If _ControlMode OrElse HasShown Then Return

            If _StartPosition = FormStartPosition.CenterParent OrElse _StartPosition = FormStartPosition.CenterScreen Then
                Dim SB As Rectangle = Screen.PrimaryScreen.Bounds
                Dim CB As Rectangle = ParentForm.Bounds
                ParentForm.Location = New Point(SB.Width \ 2 - CB.Width \ 2, SB.Height \ 2 - CB.Width \ 2)
            End If
            HasShown = True
        End Sub

        Private Function GetIndex() As Integer
            GetIndexPoint = PointToClient(MousePosition)
            B1x = GetIndexPoint.X < 7
            B2x = GetIndexPoint.X > Width - 7
            B3 = GetIndexPoint.Y < 7
            B4 = GetIndexPoint.Y > Height - 7

            If B1x AndAlso B3 Then Return 4
            If B1x AndAlso B4 Then Return 7
            If B2x AndAlso B3 Then Return 5
            If B2x AndAlso B4 Then Return 8
            If B1x Then Return 1
            If B2x Then Return 2
            If B3 Then Return 3
            If B4 Then Return 6
            Return 0
        End Function

        Private Sub InitializeMessages()
            Messages(0) = Message.Create(Parent.Handle, 161, New IntPtr(2), IntPtr.Zero)
            For I As Integer = 1 To 8
                Messages(I) = Message.Create(Parent.Handle, 161, New IntPtr(I + 9), IntPtr.Zero)
            Next
        End Sub

        Private Sub InvalidateMouse()
            Current = GetIndex()
            If Current = Previous Then Return

            Previous = Current
            Select Case Previous
                Case 0
                    Cursor = Cursors.Default
                Case 6
                    Cursor = Cursors.SizeNS
                Case 8
                    Cursor = Cursors.SizeNWSE
                Case 7
                    Cursor = Cursors.SizeNESW
            End Select
        End Sub

        Private Sub SetState(ByVal current As MouseState)
            State = current
            Invalidate()
        End Sub

#End Region 'Methods

    End Class

    Class MonoFlat_Button
        Inherits Control

#Region "Fields"

        Private InactiveGB, PressedGB As LinearGradientBrush
        Private MouseState As Integer
        Private P1, P3 As Pen
        Private R1 As Rectangle
        Private Shape As GraphicsPath
        Private _Image As Image
        Private _ImageAlign As ContentAlignment = ContentAlignment.MiddleLeft
        Private _ImageSize As Size
        Private _TextAlignment As StringAlignment = StringAlignment.Center
        Private _TextColor As Color = Color.FromArgb(150, 150, 150)

#End Region 'Fields

#Region "Constructors"

        Sub New()
            SetStyle(ControlStyles.AllPaintingInWmPaint Or _
                     ControlStyles.OptimizedDoubleBuffer Or _
                     ControlStyles.ResizeRedraw Or _
                     ControlStyles.SupportsTransparentBackColor Or _
                     ControlStyles.UserPaint, True)

            BackColor = Color.Transparent
            DoubleBuffered = True
            Font = New Font("Segoe UI", 12)
            ForeColor = Color.FromArgb(255, 255, 255)
            Size = New Size(146, 41)
            _TextAlignment = StringAlignment.Center
            P1 = New Pen(Color.FromArgb(26, 188, 156)) ' P1 = Border color
            P3 = New Pen(Color.FromArgb(22, 160, 133))  ' P3 = Border color when pressed
        End Sub

#End Region 'Constructors

#Region "Properties"

        Public Overrides Property ForeColor() As Color
            Get
                Return Me._TextColor
            End Get
            Set(ByVal value As Color)
                Me._TextColor = value
                Me.Invalidate()
            End Set
        End Property

        Public Property ImageAlign() As ContentAlignment
            Get
                Return _ImageAlign
            End Get
            Set(ByVal Value As ContentAlignment)
                _ImageAlign = Value
                Invalidate()
            End Set
        End Property

        Public Property TextAlignment() As StringAlignment
            Get
                Return Me._TextAlignment
            End Get
            Set(ByVal value As StringAlignment)
                Me._TextAlignment = value
                Me.Invalidate()
            End Set
        End Property

        Protected ReadOnly Property ImageSize() As Size
            Get
                Return _ImageSize
            End Get
        End Property

        Property Image() As Image
            Get
                Return _Image
            End Get
            Set(ByVal value As Image)
                If value Is Nothing Then
                    _ImageSize = Size.Empty
                Else
                    _ImageSize = value.Size
                End If

                _Image = value
                Invalidate()
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
            MouseState = 1
            Focus()
            Invalidate()
            MyBase.OnMouseDown(e)
        End Sub

        Protected Overrides Sub OnMouseLeave(ByVal e As EventArgs)
            MouseState = 0
            Invalidate()
            MyBase.OnMouseLeave(e)
        End Sub

        Protected Overrides Sub OnMouseUp(ByVal e As MouseEventArgs)
            MouseState = 0
            Invalidate()
            MyBase.OnMouseUp(e)
        End Sub

        Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
            With e.Graphics
                .SmoothingMode = SmoothingMode.HighQuality
                Dim ipt As PointF = ImageLocation(GetStringFormat(ImageAlign), Size, ImageSize)

                Select Case MouseState
                    Case 0 'Inactive
                        .FillPath(InactiveGB, Shape) ' Fill button body with InactiveGB color gradient
                        .DrawPath(P1, Shape) ' Draw button border [InactiveGB]
                        If IsNothing(Image) Then
                            .DrawString(Text, Font, New SolidBrush(ForeColor), R1, New StringFormat() With {.Alignment = _TextAlignment, .LineAlignment = StringAlignment.Center})
                        Else
                            .DrawImage(_Image, ipt.X, ipt.Y, ImageSize.Width, ImageSize.Height)
                            .DrawString(Text, Font, New SolidBrush(ForeColor), R1, New StringFormat() With {.Alignment = _TextAlignment, .LineAlignment = StringAlignment.Center})
                        End If
                    Case 1 'Pressed
                        .FillPath(PressedGB, Shape) ' Fill button body with PressedGB color gradient
                        .DrawPath(P3, Shape) ' Draw button border [PressedGB]

                        If IsNothing(Image) Then
                            .DrawString(Text, Font, New SolidBrush(ForeColor), R1, New StringFormat() With {.Alignment = _TextAlignment, .LineAlignment = StringAlignment.Center})
                        Else
                            .DrawImage(_Image, ipt.X, ipt.Y, ImageSize.Width, ImageSize.Height)
                            .DrawString(Text, Font, New SolidBrush(ForeColor), R1, New StringFormat() With {.Alignment = _TextAlignment, .LineAlignment = StringAlignment.Center})
                        End If
                End Select
            End With
            MyBase.OnPaint(e)
        End Sub

        Protected Overrides Sub OnResize(ByVal e As EventArgs)
            MyBase.OnResize(e)
            If Width > 0 AndAlso Height > 0 Then

                Shape = New GraphicsPath
                R1 = New Rectangle(0, 0, Width, Height)

                InactiveGB = New LinearGradientBrush(New Rectangle(0, 0, Width, Height), Color.FromArgb(26, 188, 156), Color.FromArgb(26, 188, 156), 90.0F)
                PressedGB = New LinearGradientBrush(New Rectangle(0, 0, Width, Height), Color.FromArgb(22, 160, 133), Color.FromArgb(22, 160, 133), 90.0F)
            End If

            With Shape
                .AddArc(0, 0, 10, 10, 180, 90)
                .AddArc(Width - 11, 0, 10, 10, -90, 90)
                .AddArc(Width - 11, Height - 11, 10, 10, 0, 90)
                .AddArc(0, Height - 11, 10, 10, 90, 90)
                .CloseAllFigures()
            End With
            Invalidate()
        End Sub

        Protected Overrides Sub OnTextChanged(ByVal e As EventArgs)
            Invalidate()
            MyBase.OnTextChanged(e)
        End Sub

        Private Shared Function ImageLocation(ByVal SF As StringFormat, ByVal Area As SizeF, ByVal ImageArea As SizeF) As PointF
            Dim MyPoint As PointF
            Select Case SF.Alignment
                Case StringAlignment.Center
                    MyPoint.X = CSng((Area.Width - ImageArea.Width) / 2)
                Case StringAlignment.Near
                    MyPoint.X = 2
                Case StringAlignment.Far
                    MyPoint.X = Area.Width - ImageArea.Width - 2

            End Select

            Select Case SF.LineAlignment
                Case StringAlignment.Center
                    MyPoint.Y = CSng((Area.Height - ImageArea.Height) / 2)
                Case StringAlignment.Near
                    MyPoint.Y = 2
                Case StringAlignment.Far
                    MyPoint.Y = Area.Height - ImageArea.Height - 2
            End Select
            Return MyPoint
        End Function

        Private Function GetStringFormat(ByVal _ContentAlignment As ContentAlignment) As StringFormat
            Dim SF As StringFormat = New StringFormat()
            Select Case _ContentAlignment
                Case ContentAlignment.MiddleCenter
                    SF.LineAlignment = StringAlignment.Center
                    SF.Alignment = StringAlignment.Center
                Case ContentAlignment.MiddleLeft
                    SF.LineAlignment = StringAlignment.Center
                    SF.Alignment = StringAlignment.Near
                Case ContentAlignment.MiddleRight
                    SF.LineAlignment = StringAlignment.Center
                    SF.Alignment = StringAlignment.Far
                Case ContentAlignment.TopCenter
                    SF.LineAlignment = StringAlignment.Near
                    SF.Alignment = StringAlignment.Center
                Case ContentAlignment.TopLeft
                    SF.LineAlignment = StringAlignment.Near
                    SF.Alignment = StringAlignment.Near
                Case ContentAlignment.TopRight
                    SF.LineAlignment = StringAlignment.Near
                    SF.Alignment = StringAlignment.Far
                Case ContentAlignment.BottomCenter
                    SF.LineAlignment = StringAlignment.Far
                    SF.Alignment = StringAlignment.Center
                Case ContentAlignment.BottomLeft
                    SF.LineAlignment = StringAlignment.Far
                    SF.Alignment = StringAlignment.Near
                Case ContentAlignment.BottomRight
                    SF.LineAlignment = StringAlignment.Far
                    SF.Alignment = StringAlignment.Far
            End Select
            Return SF
        End Function

#End Region 'Methods

    End Class

    <DefaultEvent("CheckedChanged")> _
    Class MonoFlat_CheckBox
        Inherits Control

#Region "Fields"

        Private Shape As GraphicsPath
        Private X As Integer
        Private _Checked As Boolean = False

#End Region 'Fields

#Region "Constructors"

        Sub New()
            Width = 148
            Height = 16
            Font = New Font("Microsoft Sans Serif", 9)
            DoubleBuffered = True
        End Sub

#End Region 'Constructors

#Region "Events"

        Event CheckedChanged(ByVal sender As Object)

#End Region 'Events

#Region "Properties"

        Property Checked() As Boolean
            Get
                Return _Checked
            End Get
            Set(ByVal value As Boolean)
                _Checked = value
                Invalidate()
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
            _Checked = Not _Checked
            Focus()
            RaiseEvent CheckedChanged(Me)
            MyBase.OnMouseDown(e)
        End Sub

        Protected Overrides Sub OnMouseMove(ByVal e As MouseEventArgs)
            MyBase.OnMouseMove(e)
            X = e.Location.X
            Invalidate()
        End Sub

        Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
            MyBase.OnPaint(e)
            Dim G As Graphics = e.Graphics
            G.Clear(Parent.BackColor)

            If _Checked Then
                G.FillRectangle(New SolidBrush(Color.FromArgb(66, 76, 85)), New Rectangle(0, 0, 16, 16))
                G.FillRectangle(New SolidBrush(Color.FromArgb(66, 76, 85)), New Rectangle(1, 1, 16 - 2, 16 - 2))
            Else
                G.FillRectangle(New SolidBrush(Color.FromArgb(66, 76, 85)), New Rectangle(0, 0, 16, 16))
                G.FillRectangle(New SolidBrush(Color.FromArgb(66, 76, 85)), New Rectangle(1, 1, 16 - 2, 16 - 2))
            End If

            If Enabled = True Then
                If _Checked Then G.DrawString("a", New Font("Marlett", 16), New SolidBrush(Color.FromArgb(26, 188, 156)), New Point(-5, -3))
            Else
                If _Checked Then G.DrawString("a", New Font("Marlett", 16), New SolidBrush(Color.Gray), New Point(-5, -3))
            End If

            G.DrawString(Text, Font, New SolidBrush(Color.FromArgb(116, 125, 132)), New Point(20, 0))
        End Sub

        Protected Overrides Sub OnResize(e As EventArgs)
            MyBase.OnResize(e)

            Me.Height = 16

            Shape = New GraphicsPath
            With Shape
                .AddArc(0, 0, 10, 10, 180, 90)
                .AddArc(Width - 11, 0, 10, 10, -90, 90)
                .AddArc(Width - 11, Height - 11, 10, 10, 0, 90)
                .AddArc(0, Height - 11, 10, 10, 90, 90)
                .CloseAllFigures()
            End With
            Invalidate()
        End Sub

#End Region 'Methods

    End Class

    Class MonoFlat_ControlBox
        Inherits Control

#Region "Fields"

        Private ButtonHState As ButtonHoverState = ButtonHoverState.None

        'Private _EnableMaximize As Boolean = True
        'Property EnableMaximizeButton() As Boolean
        '    Get
        '        Return _EnableMaximize
        '    End Get
        '    Set(ByVal value As Boolean)
        '        _EnableMaximize = value
        '        Invalidate()
        '    End Set
        'End Property
        Private _aboutForm As Form
        Private _enableAbout As Boolean = True
        Private _enableHoverHighlight As Boolean = False
        Private _enableMinimize As Boolean = True

#End Region 'Fields

#Region "Constructors"

        Sub New()
            MyBase.New()
            DoubleBuffered = True
            Anchor = AnchorStyles.Top Or AnchorStyles.Right
        End Sub

#End Region 'Constructors

#Region "Enumerations"

        Enum ButtonHoverState
            Minimize
            'Maximize
            Close
            None
            About
        End Enum

#End Region 'Enumerations

#Region "Properties"

        Property AboutForm() As Form
            Get
                Return _aboutForm
            End Get
            Set(ByVal value As Form)
                _aboutForm = value
                Invalidate()
            End Set
        End Property

        Property EnableAbout() As Boolean
            Get
                Return _enableAbout
            End Get
            Set(ByVal value As Boolean)
                _enableAbout = value
                Invalidate()
            End Set
        End Property

        Property EnableHoverHighlight() As Boolean
            Get
                Return _enableHoverHighlight
            End Get
            Set(ByVal value As Boolean)
                _enableHoverHighlight = value
                Invalidate()
            End Set
        End Property

        Property EnableMinimizeButton() As Boolean
            Get
                Return _enableMinimize
            End Get
            Set(ByVal value As Boolean)
                _enableMinimize = value
                Invalidate()
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Protected Overrides Sub OnCreateControl()
            MyBase.OnCreateControl()
            Try
                'Location = New Point(Parent.Width - 126, 10)
            Catch ex As Exception
            End Try
        End Sub

        Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
            MyBase.OnMouseDown(e)
            Focus()
        End Sub

        Protected Overrides Sub OnMouseLeave(ByVal e As EventArgs)
            MyBase.OnMouseLeave(e)
            ButtonHState = ButtonHoverState.None : Invalidate()
        End Sub

        Protected Overrides Sub OnMouseMove(ByVal e As MouseEventArgs)
            MyBase.OnMouseMove(e)
            Dim X As Integer = e.Location.X
            Dim Y As Integer = e.Location.Y
            If Y > 0 AndAlso Y < (Height - 2) Then
                If X > 0 AndAlso X < 34 Then
                    ButtonHState = ButtonHoverState.Minimize
                ElseIf X > 33 AndAlso X < 65 Then
                    ButtonHState = ButtonHoverState.About
                ElseIf X > 64 AndAlso X < Width Then
                    ButtonHState = ButtonHoverState.Close
                Else
                    ButtonHState = ButtonHoverState.None
                End If
            Else
                ButtonHState = ButtonHoverState.None
            End If
            Invalidate()
        End Sub

        Protected Overrides Sub OnMouseUp(ByVal e As MouseEventArgs)
            MyBase.OnMouseDown(e)
            Select Case ButtonHState

                Case ButtonHoverState.Close
                    Parent.FindForm().Close()
                Case ButtonHoverState.Minimize

                    If _enableMinimize Then
                        Parent.FindForm().WindowState = FormWindowState.Minimized
                    End If

                Case ButtonHoverState.About
                    If _enableAbout Then

                        Dim f As New Form
                        f = AboutForm
                        f.Bounds = FormMain.Bounds

                        f.ShowDialog()

                    End If

                    'Case ButtonHoverState.Maximize
                    '    If _EnableMaximize = True Then
                    '        If Parent.FindForm().WindowState = FormWindowState.Normal Then
                    '            Parent.FindForm().WindowState = FormWindowState.Maximized
                    '        Else
                    '            Parent.FindForm().WindowState = FormWindowState.Normal
                    '        End If
                    '    End If
            End Select
        End Sub

        Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
            MyBase.OnPaint(e)
            Dim g As Graphics = e.Graphics
            g.Clear(Color.FromArgb(26, 188, 156))

            If _enableHoverHighlight Then

                Select Case ButtonHState

                    Case ButtonHoverState.None
                        g.Clear(Color.FromArgb(26, 188, 156))
                    Case ButtonHoverState.Minimize
                        If _enableMinimize Then
                            g.FillRectangle(New SolidBrush(Color.FromArgb(22, 160, 133)), New Rectangle(3, 0, 30, Height))
                        End If
                        'Case ButtonHoverState.Maximize
                        '    If _EnableMaximize = True Then
                        '        G.FillRectangle(New SolidBrush(Color.FromArgb(22, 160, 133)), New Rectangle(35, 0, 30, Height))
                        '    End If
                    Case ButtonHoverState.Close
                        g.FillRectangle(New SolidBrush(Color.FromArgb(22, 160, 133)), New Rectangle(66, 0, 35, Height))

                    Case ButtonHoverState.About
                        If _enableAbout Then
                            g.FillRectangle(New SolidBrush(Color.FromArgb(22, 160, 133)), New Rectangle(35, 0, 30, Height))
                        End If

                End Select
            End If

            'Close
            g.DrawString("r", New Font("Marlett", 12), New SolidBrush(Color.FromArgb(255, 254, 255)), New Point(Width - 16, 8), New StringFormat With {.Alignment = StringAlignment.Center})

            'Maximize
            Select Case Parent.FindForm().WindowState
                'Case FormWindowState.Maximized
                '    If _EnableMaximize = True Then
                '        G.DrawString("2", New Font("Marlett", 12), New SolidBrush(Color.FromArgb(255, 254, 255)), New Point(51, 7), New StringFormat With {.Alignment = StringAlignment.Center})
                '    Else
                '        G.DrawString("2", New Font("Marlett", 12), New SolidBrush(Color.LightGray), New Point(51, 7), New StringFormat With {.Alignment = StringAlignment.Center})
                '    End If
                Case FormWindowState.Normal
                    'If _EnableMaximize = True Then
                    '    G.DrawString("1", New Font("Marlett", 12), New SolidBrush(Color.FromArgb(255, 254, 255)), New Point(51, 7), New StringFormat With {.Alignment = StringAlignment.Center})
                    'Else
                    '    G.DrawString("1", New Font("Marlett", 12), New SolidBrush(Color.LightGray), New Point(51, 7), New StringFormat With {.Alignment = StringAlignment.Center})
                    'End If
            End Select

            'Minimize
            If _enableMinimize Then
                g.DrawString("0", New Font("Marlett", 12), New SolidBrush(Color.FromArgb(255, 254, 255)), New Point(25, 7), New StringFormat With {.Alignment = StringAlignment.Center})
            Else
                'g.DrawString("0", New Font("Marlett", 12), New SolidBrush(Color.LightGray), New Point(25, 7), New StringFormat With {.Alignment = StringAlignment.Center})
            End If

            If _enableAbout Then
                g.DrawString("s", New Font("Marlett", 12), New SolidBrush(Color.FromArgb(255, 254, 255)), New Point(51, 7), New StringFormat With {.Alignment = StringAlignment.Center})
            Else
                'g.DrawString("s", New Font("Marlett", 12), New SolidBrush(Color.LightGray), New Point(51, 7), New StringFormat With {.Alignment = StringAlignment.Center})
            End If
        End Sub

        Protected Overrides Sub OnResize(ByVal e As EventArgs)
            MyBase.OnResize(e)
            Size = New Size(100, 25)
        End Sub

#End Region 'Methods

    End Class

    Class MonoFlat_HeaderLabel
        Inherits Label

#Region "Constructors"

        Sub New()
            Font = New Font("Segoe UI", 11, FontStyle.Bold)
            ForeColor = Color.FromArgb(255, 255, 255)
            BackColor = Color.Transparent
        End Sub

#End Region 'Constructors

    End Class

    Class MonoFlat_Label
        Inherits Label

#Region "Constructors"

        Sub New()
            Font = New Font("Segoe UI", 9)
            ForeColor = Color.FromArgb(116, 125, 132)
            BackColor = Color.Transparent
        End Sub

#End Region 'Constructors

    End Class

    Class MonoFlat_LinkLabel
        Inherits LinkLabel

#Region "Constructors"

        Sub New()
            Font = New Font("Segoe UI", 9, FontStyle.Regular)
            BackColor = Color.Transparent
            LinkColor = Color.FromArgb(26, 188, 156)
            ActiveLinkColor = Color.FromArgb(153, 34, 34)
            VisitedLinkColor = Color.FromArgb(26, 188, 156)
            LinkBehavior = LinkBehavior.NeverUnderline
        End Sub

#End Region 'Constructors

    End Class

    Class MonoFlat_NotificationBox
        Inherits Control

#Region "Fields"

        Private _closeCoordinates As Point
        Private _createRoundPath As GraphicsPath
        Private _isOverClose As Boolean
        Private _notificationText As String = Nothing
        Private _borderCurve As Integer = 8
        Private _image As Image
        Private _imageSize As Size
        Private _notificationType As Type
        Private _roundedCorners As Boolean
        Private _showCloseButton As Boolean
        Private _title As String

#End Region 'Fields

#Region "Constructors"

        Sub New()
            SetStyle(ControlStyles.AllPaintingInWmPaint Or _
                     ControlStyles.UserPaint Or _
                     ControlStyles.OptimizedDoubleBuffer Or _
                     ControlStyles.ResizeRedraw, True)

            Font = New Font("Tahoma", 9)

            Me.MinimumSize = New Size(100, 40)
            RoundCorners = False
            ShowCloseButton = True
        End Sub

#End Region 'Constructors

#Region "Enumerations"

        ' Create a list of Notification Types
        Enum Type
            [Notice]
            [Success]
            [Warning]
            [Error]
            [Custom]
        End Enum

#End Region 'Enumerations

#Region "Properties"

        ' Integer value to determine the curve level of the borders
        Public Property BorderCurve() As Integer
            Get
                Return _BorderCurve
            End Get
            Set(ByVal value As Integer)
                _BorderCurve = value
                Invalidate()
            End Set
        End Property

        ' Create a NotificationType property and add the Type enum to it
        Public Property NotificationType() As Type
            Get
                Return _NotificationType
            End Get
            Set(ByVal value As Type)
                _NotificationType = value
                Invalidate()
            End Set
        End Property

        ' Boolean value to determine whether the control should use border radius
        Public Property RoundCorners() As Boolean
            Get
                Return _RoundedCorners
            End Get
            Set(ByVal value As Boolean)
                _RoundedCorners = value
                Invalidate()
            End Set
        End Property

        ' Boolean value to determine whether the control should draw the close button
        Public Property ShowCloseButton() As Boolean
            Get
                Return _ShowCloseButton
            End Get
            Set(ByVal value As Boolean)
                _ShowCloseButton = value
                Invalidate()
            End Set
        End Property

        Public Property Title() As String
            Get
                Return _Title
            End Get
            Set(ByVal value As String)
                _Title = value
                Invalidate()
            End Set
        End Property

        ' Size value - returns the image size
        Protected ReadOnly Property ImageSize() As Size
            Get
                Return _ImageSize
            End Get
        End Property

        ' Image value to determine whether the control should draw an image before the header
        Property Image() As Image
            Get
                Return _Image
            End Get
            Set(ByVal value As Image)
                If value Is Nothing Then
                    _ImageSize = Size.Empty
                Else
                    _ImageSize = value.Size
                End If

                _Image = value
                Invalidate()
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Friend Function CreateRoundRect(ByVal r As Rectangle, ByVal curve As Integer) As GraphicsPath
            ' Draw a border radius
            Try
                _createRoundPath = New GraphicsPath(FillMode.Winding)
                _createRoundPath.AddArc(r.X, r.Y, curve, curve, 180.0F, 90.0F)
                _createRoundPath.AddArc(r.Right - curve, r.Y, curve, curve, 270.0F, 90.0F)
                _createRoundPath.AddArc(r.Right - curve, r.Bottom - curve, curve, curve, 0.0F, 90.0F)
                _createRoundPath.AddArc(r.X, r.Bottom - curve, curve, curve, 90.0F, 90.0F)
                _createRoundPath.CloseFigure()
            Catch ex As Exception
                MessageBox.Show(ex.Message & vbNewLine & vbNewLine & "Value must be either '1' or higher", "Invalid Integer", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                ' Return to the default border curve if the parameter is less than "1"
                _BorderCurve = 8
                BorderCurve = 8
            End Try
            Return _createRoundPath
        End Function

        Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
            MyBase.OnMouseDown(e)

            ' Disposes the control when the close button is clicked
            If _ShowCloseButton = True Then
                If _isOverClose Then
                    Dispose()
                End If
            End If
        End Sub

        Protected Overrides Sub OnMouseMove(ByVal e As MouseEventArgs)
            MyBase.OnMouseMove(e)

            ' Decides the location of the drawn ellipse. If mouse is over the correct coordinates, "IsOverClose" boolean will be triggered to draw the ellipse
            If e.X >= Width - 19 AndAlso e.X <= Width - 10 AndAlso e.Y > _closeCoordinates.Y AndAlso e.Y < _closeCoordinates.Y + 12 Then
                _isOverClose = True
            Else
                _isOverClose = False
            End If
            ' Updates the control
            Invalidate()
        End Sub

        Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
            MyBase.OnPaint(e)

            ' Declare Graphics to draw the control
            Dim gfx As Graphics = e.Graphics
            ' Declare Color to paint the control's Text, Background and Border
            Dim ForeColor, BackgroundColor, BorderColor As Color
            ' Determine the header Notification Type font
            Dim typeFont As New Font(Font.FontFamily, Font.Size, FontStyle.Bold)
            ' Decalre a new rectangle to draw the control inside it
            Dim MainRectangle As New Rectangle(0, 0, Width - 1, Height - 1)
            ' Declare a GraphicsPath to create a border radius
            Dim CrvBorderPath As GraphicsPath = CreateRoundRect(MainRectangle, _BorderCurve)

            gfx.SmoothingMode = SmoothingMode.HighQuality
            gfx.TextRenderingHint = TextRenderingHint.ClearTypeGridFit
            gfx.Clear(Parent.BackColor)

            Select Case _NotificationType

                Case Type.Notice
                    BackgroundColor = Color.FromArgb(111, 177, 199)
                    BorderColor = Color.FromArgb(111, 177, 199)
                    ForeColor = Color.White
                Case Type.Success
                    BackgroundColor = Color.FromArgb(91, 195, 162)
                    BorderColor = Color.FromArgb(91, 195, 162)
                    ForeColor = Color.White
                Case Type.Warning
                    BackgroundColor = Color.FromArgb(254, 209, 108)
                    BorderColor = Color.FromArgb(254, 209, 108)
                    ForeColor = Color.DimGray
                Case Type.Error
                    BackgroundColor = Color.FromArgb(217, 103, 93)
                    BorderColor = Color.FromArgb(217, 103, 93)
                    ForeColor = Color.White
                Case Type.Custom
                    BackgroundColor = Color.FromArgb(39, 174, 96)
                    BorderColor = Color.FromArgb(39, 174, 96)
                    ForeColor = Color.White

            End Select

            If _RoundedCorners = True Then
                gfx.FillPath(New SolidBrush(BackgroundColor), CrvBorderPath)
                gfx.DrawPath(New Pen(BorderColor), CrvBorderPath)
            Else
                gfx.FillRectangle(New SolidBrush(BackgroundColor), MainRectangle)
                gfx.DrawRectangle(New Pen(BorderColor), MainRectangle)
            End If

            _notificationText = Me.Title

            If IsNothing(Image) Then
                gfx.DrawString(_notificationText, typeFont, New SolidBrush(ForeColor), New Point(10, 5))
                gfx.DrawString(Text, Font, New SolidBrush(ForeColor), New Rectangle(10, 21, Width - 17, Height - 5))
            Else
                gfx.DrawImage(_Image, 12, 4, 16, 16)
                gfx.DrawString(_notificationText, typeFont, New SolidBrush(ForeColor), New Point(30, 5))
                gfx.DrawString(Text, Font, New SolidBrush(ForeColor), New Rectangle(10, 21, Width - 17, Height - 5))
            End If

            _closeCoordinates = New Point(Width - 26, 4)

            If _ShowCloseButton = True Then
                ' Draw the close button
                gfx.DrawString("r", New Font("Marlett", 7, FontStyle.Regular), New SolidBrush(Color.FromArgb(130, 130, 130)), New Rectangle(Width - 20, 10, Width, Height), New StringFormat() With {.Alignment = StringAlignment.Near, .LineAlignment = StringAlignment.Near})
            End If

            CrvBorderPath.Dispose()
        End Sub

#End Region 'Methods

    End Class

    Class MonoFlat_Panel
        Inherits ContainerControl

#Region "Fields"

        Private Shape As GraphicsPath

#End Region 'Fields

#Region "Constructors"

        Sub New()
            SetStyle(ControlStyles.SupportsTransparentBackColor, True)
            SetStyle(ControlStyles.UserPaint, True)

            BackColor = Color.FromArgb(39, 51, 63)
            Me.Size = New Size(187, 117)
            Padding = New Padding(5, 5, 5, 5)
            DoubleBuffered = True
        End Sub

#End Region 'Constructors

#Region "Methods"

        Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
            MyBase.OnPaint(e)
            Dim B As New Bitmap(Width, Height)
            Dim G = Graphics.FromImage(B)

            G.SmoothingMode = SmoothingMode.HighQuality

            G.Clear(Color.FromArgb(44, 62, 80)) ' Set control background to transparent
            G.FillPath(New SolidBrush(Color.FromArgb(39, 51, 63)), Shape) ' Draw RTB background
            G.DrawPath(New Pen(Color.FromArgb(39, 51, 63)), Shape) ' Draw border

            G.Dispose()
            e.Graphics.DrawImage(B.Clone(), 0, 0)
            B.Dispose()
        End Sub

        Protected Overrides Sub OnResize(ByVal e As EventArgs)
            MyBase.OnResize(e)

            Shape = New GraphicsPath
            With Shape
                .AddArc(0, 0, 10, 10, 180, 90)
                .AddArc(Width - 11, 0, 10, 10, -90, 90)
                .AddArc(Width - 11, Height - 11, 10, 10, 0, 90)
                .AddArc(0, Height - 11, 10, 10, 90, 90)
                .CloseAllFigures()
            End With
        End Sub

#End Region 'Methods

    End Class

    <DefaultEvent("CheckedChanged")> _
    Class MonoFlat_RadioButton
        Inherits Control

#Region "Fields"

        Private X As Integer
        Private _Checked As Boolean

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Width = 159
            Height = 17
            DoubleBuffered = True
        End Sub

#End Region 'Constructors

#Region "Events"

        Event CheckedChanged(ByVal sender As Object)

#End Region 'Events

#Region "Properties"

        Property Checked() As Boolean
            Get
                Return _Checked
            End Get
            Set(ByVal value As Boolean)
                _Checked = value
                InvalidateControls()
                RaiseEvent CheckedChanged(Me)
                Invalidate()
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
            If Not _Checked Then Checked = True
            Focus()
            MyBase.OnMouseDown(e)
        End Sub

        Protected Overrides Sub OnMouseMove(ByVal e As MouseEventArgs)
            MyBase.OnMouseMove(e)
            X = e.X
            Invalidate()
        End Sub

        Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
            MyBase.OnPaint(e)
            Dim G As Graphics = e.Graphics
            G.Clear(Parent.BackColor)
            G.SmoothingMode = SmoothingMode.HighQuality

            G.FillEllipse(New SolidBrush(Color.FromArgb(66, 76, 85)), New Rectangle(0, 0, 16, 16))

            If _Checked Then
                G.DrawString("a", New Font("Marlett", 15), New SolidBrush(Color.FromArgb(26, 188, 156)), New Point(-3, -2))
            End If

            G.DrawString(Text, Font, New SolidBrush(Color.FromArgb(116, 125, 132)), New Point(20, 0))
        End Sub

        Protected Overrides Sub OnResize(e As EventArgs)
            MyBase.OnResize(e)
            Me.Height = 17
        End Sub

        Protected Overrides Sub OnTextChanged(ByVal e As EventArgs)
            MyBase.OnTextChanged(e)
            Dim textSize As Integer
            textSize = Me.CreateGraphics.MeasureString(Text, Font).Width
            Me.Width = 28 + textSize
        End Sub

        Private Sub InvalidateControls()
            If Not IsHandleCreated OrElse Not _Checked Then Return

            For Each _Control As Control In Parent.Controls
                If _Control IsNot Me AndAlso TypeOf _Control Is MonoFlat_RadioButton Then
                    DirectCast(_Control, MonoFlat_RadioButton).Checked = False
                End If
            Next
        End Sub

#End Region 'Methods

    End Class

    Class MonoFlat_Separator
        Inherits Control

#Region "Constructors"

        Sub New()
            SetStyle(ControlStyles.ResizeRedraw, True)
            Me.Size = New Point(120, 10)
        End Sub

#End Region 'Constructors

#Region "Methods"

        Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
            MyBase.OnPaint(e)
            e.Graphics.DrawLine(New Pen(Color.FromArgb(45, 57, 68)), 0, 5, Width, 5)
        End Sub

#End Region 'Methods

    End Class

    Class MonoFlat_SocialButton
        Inherits Control

#Region "Fields"

        Private EllipseColor As Color = Color.FromArgb(66, 76, 85)
        Private _Image As Image
        Private _ImageSize As Size

#End Region 'Fields

#Region "Constructors"

        Sub New()
            DoubleBuffered = True
        End Sub

#End Region 'Constructors

#Region "Properties"

        Protected ReadOnly Property ImageSize() As Size
            Get
                Return _ImageSize
            End Get
        End Property

        Property Image() As Image
            Get
                Return _Image
            End Get
            Set(ByVal value As Image)
                If value Is Nothing Then
                    _ImageSize = Size.Empty
                Else
                    _ImageSize = value.Size
                End If

                _Image = value
                Invalidate()
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
            MyBase.OnMouseDown(e)
            EllipseColor = Color.FromArgb(153, 34, 34)
            Focus()
            Refresh()
        End Sub

        Protected Overrides Sub OnMouseEnter(e As EventArgs)
            MyBase.OnMouseEnter(e)
            EllipseColor = Color.FromArgb(26, 188, 156)
            Refresh()
        End Sub

        Protected Overrides Sub OnMouseLeave(e As EventArgs)
            MyBase.OnMouseLeave(e)
            EllipseColor = Color.FromArgb(66, 76, 85)
            Refresh()
        End Sub

        Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
            MyBase.OnMouseUp(e)
            EllipseColor = Color.FromArgb(26, 188, 156)
            Refresh()
        End Sub

        Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
            Dim G As Graphics = e.Graphics
            G.Clear(Parent.BackColor)
            G.SmoothingMode = SmoothingMode.HighQuality

            Dim ImgPoint As PointF = ImageLocation(GetStringFormat(ContentAlignment.MiddleCenter), Size, ImageSize)
            G.FillEllipse(New SolidBrush(EllipseColor), New Rectangle(0, 0, 53, 53))

            ' HINTS:
            ' The best size for the drawn image is 32x32\
            ' The best matching color of drawn image is (RGB: 31, 40, 49)
            If Image IsNot Nothing Then
                G.DrawImage(_Image, ImgPoint.X, ImgPoint.Y, ImageSize.Width, ImageSize.Height)
            End If
        End Sub

        Protected Overrides Sub OnResize(e As EventArgs)
            MyBase.OnResize(e)
            Me.Size = New Size(54, 54)
        End Sub

        Private Shared Function ImageLocation(ByVal SF As StringFormat, ByVal Area As SizeF, ByVal ImageArea As SizeF) As PointF
            Dim MyPoint As PointF
            Select Case SF.Alignment
                Case StringAlignment.Center
                    MyPoint.X = CSng((Area.Width - ImageArea.Width) / 2)
            End Select

            Select Case SF.LineAlignment
                Case StringAlignment.Center
                    MyPoint.Y = CSng((Area.Height - ImageArea.Height) / 2)
            End Select
            Return MyPoint
        End Function

        Private Function GetStringFormat(ByVal _ContentAlignment As ContentAlignment) As StringFormat
            Dim SF As StringFormat = New StringFormat()
            Select Case _ContentAlignment
                Case ContentAlignment.MiddleCenter
                    SF.LineAlignment = StringAlignment.Center
                    SF.Alignment = StringAlignment.Center
            End Select
            Return SF
        End Function

#End Region 'Methods

    End Class

    <DefaultEvent("TextChanged")> _
    Class MonoFlat_TextBox
        Inherits Control

#Region "Fields"

        Public WithEvents MonoFlatTB As New TextBox

        Private ALNType As HorizontalAlignment
        Private B1 As SolidBrush
        Private isPasswordMasked As Boolean = False
        Private P1 As Pen
        Private Shape As GraphicsPath
        Private _Image As Image
        Private _ImageSize As Size
        Private _maxchars As Integer = 32767
        Private _Multiline As Boolean
        Private _ReadOnly As Boolean

#End Region 'Fields

#Region "Constructors"

        Sub New()
            SetStyle(ControlStyles.SupportsTransparentBackColor, True)
            SetStyle(ControlStyles.UserPaint, True)

            AddTextBox()
            Controls.Add(MonoFlatTB)

            P1 = New Pen(Color.FromArgb(44, 62, 80))
            B1 = New SolidBrush(Color.FromArgb(66, 76, 85))
            BackColor = Color.Transparent
            ForeColor = Color.FromArgb(176, 183, 191)

            Text = Nothing
            Font = New Font("Tahoma", 10)
            Size = New Size(135, 43)
            DoubleBuffered = True
        End Sub

#End Region 'Constructors

#Region "Properties"

        Public Shadows Property MaxLength() As Integer
            Get
                Return _maxchars
            End Get
            Set(ByVal Val As Integer)
                _maxchars = Val
                MonoFlatTB.MaxLength = MaxLength
                Invalidate()
            End Set
        End Property

        Public Shadows Property TextAlignment() As HorizontalAlignment
            Get
                Return ALNType
            End Get
            Set(ByVal Val As HorizontalAlignment)
                ALNType = Val
                Invalidate()
            End Set
        End Property

        Public Shadows Property UseSystemPasswordChar() As Boolean
            Get
                Return isPasswordMasked
            End Get
            Set(ByVal Val As Boolean)
                MonoFlatTB.UseSystemPasswordChar = UseSystemPasswordChar
                isPasswordMasked = Val
                Invalidate()
            End Set
        End Property

        Protected ReadOnly Property ImageSize() As Size
            Get
                Return _ImageSize
            End Get
        End Property

        Property Image() As Image
            Get
                Return _Image
            End Get
            Set(ByVal value As Image)
                If value Is Nothing Then
                    _ImageSize = Size.Empty
                Else
                    _ImageSize = value.Size
                End If

                _Image = value

                If Image Is Nothing Then
                    MonoFlatTB.Location = New Point(8, 10)
                Else
                    MonoFlatTB.Location = New Point(35, 11)
                End If
                Invalidate()
            End Set
        End Property

        Property Multiline() As Boolean
            Get
                Return _Multiline
            End Get
            Set(ByVal value As Boolean)
                _Multiline = value
                If MonoFlatTB IsNot Nothing Then
                    MonoFlatTB.Multiline = value

                    If value Then
                        MonoFlatTB.Height = Height - 13
                    Else
                        Height = MonoFlatTB.Height + 13
                    End If
                End If
            End Set
        End Property

        Property [ReadOnly]() As Boolean
            Get
                Return _ReadOnly
            End Get
            Set(ByVal value As Boolean)
                _ReadOnly = value
                If MonoFlatTB IsNot Nothing Then
                    MonoFlatTB.ReadOnly = value
                End If
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Protected Overrides Sub OnFontChanged(ByVal e As EventArgs)
            MyBase.OnFontChanged(e)
            MonoFlatTB.Font = Font
        End Sub

        Protected Overrides Sub OnForeColorChanged(ByVal e As EventArgs)
            MyBase.OnForeColorChanged(e)
            MonoFlatTB.ForeColor = ForeColor
            Invalidate()
        End Sub

        Protected Overrides Sub OnGotFocus(ByVal e As EventArgs)
            MyBase.OnGotFocus(e)
            MonoFlatTB.Focus()
        End Sub

        Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
            MyBase.OnPaint(e)
            Dim B As New Bitmap(Width, Height)
            Dim G As Graphics = Graphics.FromImage(B)

            G.SmoothingMode = SmoothingMode.AntiAlias

            With MonoFlatTB

                If Image Is Nothing Then
                    .Width = Width - 18
                Else
                    .Width = Width - 45
                End If

                .TextAlign = TextAlignment
                .UseSystemPasswordChar = UseSystemPasswordChar
            End With

            G.Clear(Color.Transparent)

            G.FillPath(B1, Shape)
            G.DrawPath(P1, Shape)

            If Image IsNot Nothing Then
                G.DrawImage(_Image, 5, 8, 24, 24)
                ' 24x24 is the perfect size of the image
            End If

            e.Graphics.DrawImage(B.Clone(), 0, 0)
            G.Dispose() : B.Dispose()
        End Sub

        Protected Overrides Sub OnPaintBackground(e As PaintEventArgs)
            MyBase.OnPaintBackground(e)
        End Sub

        Protected Overrides Sub OnResize(ByVal e As EventArgs)
            MyBase.OnResize(e)
            If _Multiline Then
                MonoFlatTB.Height = Height - 13
            Else
                Height = MonoFlatTB.Height + 13
            End If

            Shape = New GraphicsPath
            With Shape
                .AddArc(0, 0, 10, 10, 180, 90)
                .AddArc(Width - 11, 0, 10, 10, -90, 90)
                .AddArc(Width - 11, Height - 11, 10, 10, 0, 90)
                .AddArc(0, Height - 11, 10, 10, 90, 90)
                .CloseAllFigures()
            End With
        End Sub

        Protected Overrides Sub OnTextChanged(ByVal e As EventArgs)
            MyBase.OnTextChanged(e)
            Invalidate()
        End Sub

        Sub AddTextBox()
            With MonoFlatTB
                .Location = New Point(8, 10)
                .Text = String.Empty
                .BorderStyle = BorderStyle.None
                .TextAlign = HorizontalAlignment.Left
                .Font = New Font("Tahoma", 10)
                .UseSystemPasswordChar = UseSystemPasswordChar
                .Multiline = False
                .BackColor = Color.FromArgb(66, 76, 85)
                .ScrollBars = ScrollBars.None
            End With
            AddHandler MonoFlatTB.KeyDown, AddressOf _OnKeyDown
            AddHandler MonoFlatTB.Enter, AddressOf _Enter
            AddHandler MonoFlatTB.Leave, AddressOf _Leave
        End Sub

        Sub _BaseTextChanged() Handles MyBase.TextChanged
            MonoFlatTB.Text = Text
        End Sub

        Private Sub _Enter(ByVal Obj As Object, ByVal e As EventArgs)
            P1 = New Pen(Color.FromArgb(26, 188, 156))
            Refresh()
        End Sub

        Private Sub _Leave(ByVal Obj As Object, ByVal e As EventArgs)
            P1 = New Pen(Color.FromArgb(44, 62, 80))
            Refresh()
        End Sub

        Private Sub _OnKeyDown(ByVal Obj As Object, ByVal e As KeyEventArgs)
            If e.Control AndAlso e.KeyCode = Keys.A Then
                MonoFlatTB.SelectAll()
                e.SuppressKeyPress = True
            End If
            If e.Control AndAlso e.KeyCode = Keys.C Then
                MonoFlatTB.Copy()
                e.SuppressKeyPress = True
            End If
        End Sub

        Sub _TextChanged() Handles MonoFlatTB.TextChanged
            Text = MonoFlatTB.Text
        End Sub

#End Region 'Methods

    End Class

    <DefaultEvent("ToggledChanged")> _
    Class MonoFlat_Toggle
        Inherits Control

#Region "Fields"

        Private ToggleType As _Type
        Private _bar As Rectangle
        Private _Toggled As Boolean
        Private _width, _height As Integer

#End Region 'Fields

#Region "Constructors"

        Sub New()
            SetStyle(ControlStyles.AllPaintingInWmPaint Or _
                      ControlStyles.DoubleBuffer Or _
                      ControlStyles.ResizeRedraw Or _
                      ControlStyles.UserPaint, True)
        End Sub

#End Region 'Constructors

#Region "Enumerations"

        Enum _Type
            CheckMark
            OnOff
            YesNo
            IO
        End Enum

#End Region 'Enumerations

#Region "Events"

        Event ToggledChanged()

#End Region 'Events

#Region "Properties"

        Public Property Toggled() As Boolean
            Get
                Return _Toggled
            End Get
            Set(ByVal value As Boolean)
                _Toggled = value
                Invalidate()
                RaiseEvent ToggledChanged()
            End Set
        End Property

        Public Property Type() As _Type
            Get
                Return ToggleType
            End Get
            Set(value As _Type)
                ToggleType = value
                Invalidate()
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Protected Overrides Sub OnMouseUp(ByVal e As MouseEventArgs)
            MyBase.OnMouseUp(e)
            Toggled = Not Toggled
            Focus()
        End Sub

        Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
            MyBase.OnPaint(e)
            Dim g As Graphics = e.Graphics

            g.SmoothingMode = SmoothingMode.HighQuality
            g.Clear(Parent.BackColor)
            _width = Width - 1 : _height = Height - 1

            Dim baseRect As New Rectangle(0, 0, _width, _height)

            With g
                .SmoothingMode = 2
                .PixelOffsetMode = 2
                .TextRenderingHint = 5
                .Clear(BackColor)

                Dim gp As GraphicsPath = RoundRect(baseRect, 4)
                Dim thumbRect As New Rectangle(4, 4, 36, _height - 8)
                Dim gp2 As GraphicsPath = RoundRect(thumbRect, 4)
                .FillPath(New SolidBrush(Color.FromArgb(231, 76, 60)), gp)
                .FillPath(New SolidBrush(Color.FromArgb(66, 76, 85)), gp2)

                If _Toggled Then
                    gp = RoundRect(baseRect, 4)
                    thumbRect = New Rectangle((_width / 2), 4, 36, _height - 8)
                    gp2 = RoundRect(thumbRect, 2)
                    .FillPath(New SolidBrush(Color.FromArgb(26, 188, 156)), gp)
                    .FillPath(New SolidBrush(Color.FromArgb(66, 76, 85)), gp2)
                End If

                ' Draw string
                Select Case ToggleType
                    Case _Type.CheckMark
                        If Toggled Then
                            g.DrawString("ü", New Font("Wingdings", 18, FontStyle.Regular), Brushes.WhiteSmoke, _bar.X + 18, _bar.Y + 19, New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
                        Else
                            g.DrawString("r", New Font("Marlett", 14, FontStyle.Regular), Brushes.DimGray, _bar.X + 59, _bar.Y + 18, New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
                        End If
                    Case _Type.OnOff
                        If Toggled Then
                            g.DrawString("ON", New Font("Segoe UI", 8, FontStyle.Regular), Brushes.WhiteSmoke, _bar.X + 18, _bar.Y + 16, New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
                        Else
                            g.DrawString("OFF", New Font("Segoe UI", 8, FontStyle.Regular), Brushes.Azure, _bar.X + 57, _bar.Y + 16, New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
                        End If
                    Case _Type.YesNo
                        If Toggled Then
                            g.DrawString("YES", New Font("Segoe UI", 12, FontStyle.Regular), Brushes.WhiteSmoke, _bar.X + 19, _bar.Y + 16, New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
                        Else
                            g.DrawString("NO", New Font("Segoe UI", 12, FontStyle.Regular), Brushes.DimGray, _bar.X + 56, _bar.Y + 16, New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
                        End If
                    Case _Type.IO
                        If Toggled Then
                            g.DrawString("I", New Font("Segoe UI", 12, FontStyle.Regular), Brushes.WhiteSmoke, _bar.X + 18, _bar.Y + 16, New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
                        Else
                            g.DrawString("O", New Font("Segoe UI", 12, FontStyle.Regular), Brushes.DimGray, _bar.X + 57, _bar.Y + 16, New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
                        End If
                End Select
            End With
        End Sub

        Protected Overrides Sub OnResize(e As EventArgs)
            MyBase.OnResize(e)

            Me.Size = New Size(76, 33)
        End Sub

#End Region 'Methods

    End Class

    <DefaultEvent("ValueChanged")> _
    Class MonoFlat_TrackBar
        Inherits Control

#Region "Fields"

        Private Cap As Boolean
        Private DividedValue As ValueDivisor = ValueDivisor.By1
        Private FillValue, PipeBorder, TrackBarHandleRect As Rectangle
        Private ThumbSize As Size = New Size(14, 14)
        Private TrackThumb As Rectangle
        Private ValueDrawer As Integer
        Private _JumpToMouse As Boolean = False
        Private _Maximum As Integer = 10
        Private _Minimum As Integer = 0
        Private _Value As Integer = 0

#End Region 'Fields

#Region "Constructors"

        Sub New()
            SetStyle(ControlStyles.AllPaintingInWmPaint Or _
             ControlStyles.UserPaint Or _
             ControlStyles.ResizeRedraw Or _
             ControlStyles.DoubleBuffer, True)

            Size = New Size(80, 22)
            MinimumSize = New Size(47, 22)
        End Sub

#End Region 'Constructors

#Region "Enumerations"

        Enum ValueDivisor
            By1 = 1
            By10 = 10
            By100 = 100
            By1000 = 1000
        End Enum

#End Region 'Enumerations

#Region "Events"

        Event ValueChanged()

#End Region 'Events

#Region "Properties"

        Public Property JumpToMouse() As Boolean
            Get
                Return _JumpToMouse
            End Get
            Set(ByVal value As Boolean)
                _JumpToMouse = value
                Invalidate()
            End Set
        End Property

        Public Property Maximum() As Integer
            Get
                Return _Maximum
            End Get
            Set(ByVal value As Integer)

                If value <= _Minimum Then value = _Minimum + 10
                If _Value > value Then _Value = value

                _Maximum = value
                Invalidate()
            End Set
        End Property

        Public Property Minimum() As Integer
            Get
                Return _Minimum
            End Get
            Set(ByVal value As Integer)

                If value >= _Maximum Then value = _Maximum - 10
                If _Value < value Then _Value = value

                _Minimum = value
                Invalidate()
            End Set
        End Property

        Public Property Value() As Integer
            Get
                Return _Value
            End Get
            Set(ByVal value As Integer)
                If _Value <> value Then
                    If value < _Minimum Then
                        _Value = _Minimum
                    Else
                        If value > _Maximum Then
                            _Value = _Maximum
                        Else
                            _Value = value
                        End If
                    End If
                    Invalidate()
                    RaiseEvent ValueChanged()
                End If
            End Set
        End Property

        Public Property ValueDivison() As ValueDivisor
            Get
                Return DividedValue
            End Get
            Set(ByVal Value As ValueDivisor)
                DividedValue = Value
                Invalidate()
            End Set
        End Property

        <Browsable(False)> _
        Public Property ValueToSet() As Single
            Get
                Return CSng(_Value / DividedValue)
            End Get
            Set(ByVal Val As Single)
                Value = CInt(Val * DividedValue)
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
            MyBase.OnMouseDown(e)
            If e.Button = MouseButtons.Left Then
                ValueDrawer = CInt((_Value - _Minimum) / (_Maximum - _Minimum) * (Width - 11))
                TrackBarHandleRect = New Rectangle(ValueDrawer, 0, 25, 25)
                Cap = TrackBarHandleRect.Contains(e.Location)
                Focus()
                If _JumpToMouse Then
                    Value = _Minimum + CInt((_Maximum - _Minimum) * (e.X / Width))
                End If
            End If
        End Sub

        Protected Overrides Sub OnMouseMove(ByVal e As MouseEventArgs)
            MyBase.OnMouseMove(e)
            If Cap = True AndAlso e.X > -1 AndAlso e.X < (Width + 1) Then
                Value = _Minimum + CInt((_Maximum - _Minimum) * (e.X / Width))
            End If
        End Sub

        Protected Overrides Sub OnMouseUp(ByVal e As MouseEventArgs)
            MyBase.OnMouseUp(e)
            Cap = False
        End Sub

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            MyBase.OnPaint(e)
            Dim G As Graphics = e.Graphics

            G.Clear(Parent.BackColor)
            G.SmoothingMode = SmoothingMode.AntiAlias
            TrackThumb = New Rectangle(7, 10, Width - 16, 2)
            PipeBorder = New Rectangle(1, 10, Width - 3, 2)

            Try
                ValueDrawer = CInt((_Value - _Minimum) / (_Maximum - _Minimum) * (Width))
            Catch ex As Exception
            End Try

            TrackBarHandleRect = New Rectangle(ValueDrawer, 0, 3, 20)

            G.FillRectangle(New SolidBrush(Color.FromArgb(124, 131, 137)), PipeBorder)
            FillValue = New Rectangle(0, 10, TrackBarHandleRect.X + TrackBarHandleRect.Width - 4, 3)

            G.ResetClip()

            G.SmoothingMode = SmoothingMode.Default
            G.DrawRectangle(New Pen(Color.FromArgb(124, 131, 137)), PipeBorder) ' Draw pipe border
            G.FillRectangle(New SolidBrush(Color.FromArgb(26, 188, 156)), FillValue)

            G.ResetClip()

            G.SmoothingMode = SmoothingMode.HighQuality
            G.FillEllipse(New SolidBrush(Color.FromArgb(26, 188, 156)), TrackThumb.X + CInt(TrackThumb.Width * (Value / Maximum)) - CInt(ThumbSize.Width / 2), TrackThumb.Y + CInt((TrackThumb.Height / 2)) - CInt(ThumbSize.Height / 2), ThumbSize.Width, ThumbSize.Height)
            G.DrawEllipse(New Pen(Color.FromArgb(26, 188, 156)), TrackThumb.X + CInt(TrackThumb.Width * (Value / Maximum)) - CInt(ThumbSize.Width / 2), TrackThumb.Y + CInt((TrackThumb.Height / 2)) - CInt(ThumbSize.Height / 2), ThumbSize.Width, ThumbSize.Height)
        End Sub

        Protected Overrides Sub OnResize(e As EventArgs)
            MyBase.OnResize(e)
            Height = 22
        End Sub

#End Region 'Methods

    End Class

    Module RoundRectangle

#Region "Methods"

        Public Function RoundRect(ByVal Rectangle As Rectangle, ByVal Curve As Integer) As GraphicsPath
            Dim P As GraphicsPath = New GraphicsPath()
            Dim ArcRectangleWidth As Integer = Curve * 2
            P.AddArc(New Rectangle(Rectangle.X, Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), -180, 90)
            P.AddArc(New Rectangle(Rectangle.Width - ArcRectangleWidth + Rectangle.X, Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), -90, 90)
            P.AddArc(New Rectangle(Rectangle.Width - ArcRectangleWidth + Rectangle.X, Rectangle.Height - ArcRectangleWidth + Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), 0, 90)
            P.AddArc(New Rectangle(Rectangle.X, Rectangle.Height - ArcRectangleWidth + Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), 90, 90)
            P.AddLine(New Point(Rectangle.X, Rectangle.Height - ArcRectangleWidth + Rectangle.Y), New Point(Rectangle.X, Curve + Rectangle.Y))
            Return P
        End Function

#End Region 'Methods

    End Module

End Namespace

