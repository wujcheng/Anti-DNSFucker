Public Class FormMain
    Private TableLayoutPanelHead As TableLayoutPanel
    Private CheckBoxHeadSelectAll As CheckBox
    Private LabelHeadDomainName As Label
    Private LabelHeadGetIPv6Address As Label
    Private LabelHeadGetIPv4Address As Label
    Private LabelHeadEnable As Label

    Private TableLayoutPanelList As TableLayoutPanel

    Private ToolStrip As ToolStrip
    Private ToolStripButtonAdd As ToolStripButton
    Private ToolStripButtonRemove As ToolStripButton
    Private ToolStripButtonQuit As ToolStripButton
    Private ToolStripButtonSaveAs As ToolStripButton

    Private StatusStrip As StatusStrip


    Private TopPadding As Padding = New Padding(0, 4, 0, 0)
    Private TransparentColor As Color = Color.FromArgb(0, Color.Black)
    Private MouseDownLocation As Point
    Private FormLastLocation As Point

    Public Sub New()
        InitializeComponent()

        With Me
            .Width = 400
            .Height = 500
            .ControlBox = False
            .FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        End With

        InitializeToolStrip()
        InitializeStatusStrip()
        InitializeHeadTableLayoutPanel()
    End Sub

    Private Sub InitializeToolStrip()
        ToolStripButtonAdd = New ToolStripButton
        With ToolStripButtonAdd
            .Image = Icons.Add
            AddHandler .Click, AddressOf ToolStripButton_Click
        End With

        ToolStripButtonRemove = New ToolStripButton
        With ToolStripButtonRemove
            .Image = Icons.Remove
            AddHandler .Click, AddressOf ToolStripButton_Click
        End With

        ToolStripButtonQuit = New ToolStripButton
        With ToolStripButtonQuit
            .Image = Icons.Quit
            AddHandler .Click, AddressOf ToolStripButton_Click
        End With

        ToolStripButtonSaveAs = New ToolStripButton
        With ToolStripButtonSaveAs
            .Image = Icons.SaveAs
            AddHandler .Click, AddressOf ToolStripButton_Click
        End With

        ToolStrip = New ToolStrip
        With ToolStrip
            .GripStyle = ToolStripGripStyle.Hidden
            .RenderMode = ToolStripRenderMode.Professional
            .Parent = Me
            .BackColor = Color.White
            .Items.Add(ToolStripButtonAdd)
            .Items.Add(ToolStripButtonRemove)
            .Items.Add(New ToolStripSeparator)
            .Items.Add(ToolStripButtonSaveAs)
            .Items.Add(New ToolStripSeparator)
            .Items.Add(ToolStripButtonQuit)

            AddHandler .MouseDown, AddressOf ToolStrip_MouseDown
            AddHandler .MouseUp, AddressOf ToolStrip_MouseUp
            AddHandler .MouseMove, AddressOf ToolStrip_MouseMove
        End With
    End Sub

    Private Sub ToolStripButton_Click(sender As Object, e As EventArgs)
        If sender Is ToolStripButtonQuit Then
            End
        End If

    End Sub

    Private Sub ToolStrip_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs)
        ' Change the cursor shape.
        Me.Cursor = Cursors.SizeAll
        ' Save the location of mouse.
        Me.MouseDownLocation = e.Location
        ' Save the location of FormMain.
        Me.FormLastLocation = Me.Location
    End Sub

    Private Sub ToolStrip_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs)
        ' Recover the cursor shape.
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub ToolStrip_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs)
        ' If the mouse button is not pressed, exit this sub.
        If Not Me.Cursor Is Cursors.SizeAll Then
            Exit Sub
        End If

        ' Update the location of the FormMain.
        Me.Location = New Point(Me.Location.X - Me.MouseDownLocation.X + e.Location.X,
                            Me.Location.Y - Me.MouseDownLocation.Y + e.Location.Y)
    End Sub

    Private Sub InitializeStatusStrip()
        StatusStrip = New StatusStrip
        With StatusStrip
            .Parent = Me
            .SizingGrip = False
        End With
    End Sub

    Private Sub InitializeHeadTableLayoutPanel()
        CheckBoxHeadSelectAll = New CheckBox
        With CheckBoxHeadSelectAll
            .Text = ""
        End With

        LabelHeadDomainName = New Label
        With LabelHeadDomainName
            .Text = "Domain Name"
            .Padding = TopPadding
            .BackColor = TransparentColor
        End With

        LabelHeadGetIPv4Address = New Label
        With LabelHeadGetIPv4Address
            .Text = "Get IPv4"
            .Padding = TopPadding
            .BackColor = TransparentColor
        End With

        LabelHeadGetIPv6Address = New Label
        With LabelHeadGetIPv6Address
            .Text = "Get IPv6"
            .Padding = TopPadding
            .BackColor = TransparentColor
        End With

        LabelHeadEnable = New Label
        With LabelHeadEnable
            .Text = "Enable"
            .Padding = TopPadding
            .BackColor = TransparentColor
        End With

        TableLayoutPanelHead = New TableLayoutPanel
        With TableLayoutPanelHead
            .Width = Me.ClientSize.Width
            .Height = 22
            .Location = New Point(0, ToolStrip.Height)
            .CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
            .RowStyles.Add(New RowStyle(SizeType.Absolute, .Height))
            .ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 1))
            .ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 2))
            .ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 5))
            .ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 2))
            .ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 2))
            .Controls.Add(CheckBoxHeadSelectAll, 0, 0)
            .Controls.Add(LabelHeadEnable, 1, 0)
            .Controls.Add(LabelHeadDomainName, 2, 0)
            .Controls.Add(LabelHeadGetIPv4Address, 3, 0)
            .Controls.Add(LabelHeadGetIPv6Address, 4, 0)
            .Parent = Me
        End With


    End Sub
End Class
