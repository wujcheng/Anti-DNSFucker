Public Class ToolStripMain
    Inherits System.Windows.Forms.ToolStrip

    Private ToolStripLabelTitle As ToolStripLabel
    Private ToolStripButtonAdd As ToolStripButton
    Private ToolStripButtonRemove As ToolStripButton
    Private ToolStripButtonQuit As ToolStripButton
    Private ToolStripButtonEnable As ToolStripButton
    Private ToolStripButtonDisable As ToolStripButton
    Private ToolStripButtonIPv4Enable As ToolStripButton
    Private ToolStripButtonIPv6Enable As ToolStripButton
    Private ToolStripButtonIPv6Disable As ToolStripButton
    Private ToolStripButtonIPv4Disable As ToolStripButton
    Private ToolStripButtonOpen As ToolStripButton
    Private ToolStripButtonSave As ToolStripButton
    Private ToolStripButtonRun As ToolStripButton

    Private MouseDownLocation As Point
    Private FormLastLocation As Point

    Public Event ToolStripButtonAdd_Click(sender As Object, e As EventArgs)
    Public Event ToolStripButtonRemove_Click(sender As Object, e As EventArgs)
    Public Event ToolStripButtonQuit_Click(sender As Object, e As EventArgs)
    Public Event ToolStripButtonEnable_Click(sender As Object, e As EventArgs)
    Public Event ToolStripButtonDisable_Click(sender As Object, e As EventArgs)
    Public Event ToolStripButtonIPv4Enable_Click(sender As Object, e As EventArgs)
    Public Event ToolStripButtonIPv6Enable_Click(sender As Object, e As EventArgs)
    Public Event ToolStripButtonIPv6Disable_Click(sender As Object, e As EventArgs)
    Public Event ToolStripButtonIPv4Disable_Click(sender As Object, e As EventArgs)
    Public Event ToolStripButtonOpen_Click(sender As Object, e As EventArgs)
    Public Event ToolStripButtonSave_Click(sender As Object, e As EventArgs)


    Public Shadows Property Text As String
        Get
            Return ToolStripLabelTitle.Text
        End Get
        Set(value As String)
            ToolStripLabelTitle.Text = value
        End Set
    End Property

    Public Sub New()
        MyBase.New

        ToolStripLabelTitle = New ToolStripLabel
        With ToolStripLabelTitle
            .Text = Me.Text

            AddHandler .MouseDown, AddressOf ToolStrip_MouseDown
            AddHandler .MouseUp, AddressOf ToolStrip_MouseUp
            AddHandler .MouseMove, AddressOf ToolStrip_MouseMove
        End With

        ToolStripButtonAdd = New ToolStripButton
        With ToolStripButtonAdd
            .Image = Icons.Add
            .ToolTipText = "Add New Domain Name"
            AddHandler .Click, AddressOf ToolStripButton_Click
        End With

        ToolStripButtonRemove = New ToolStripButton
        With ToolStripButtonRemove
            .Image = Icons.Remove
            .ToolTipText = "Remove Selected Items"
            AddHandler .Click, AddressOf ToolStripButton_Click
        End With

        ToolStripButtonQuit = New ToolStripButton
        With ToolStripButtonQuit
            .Image = Icons.Quit
            .ToolTipText = "Quit"
            AddHandler .Click, AddressOf ToolStripButton_Click
        End With

        ToolStripButtonSave = New ToolStripButton
        With ToolStripButtonSave
            .Image = Icons.Save
            .ToolTipText = "Save Configuration to File"
            AddHandler .Click, AddressOf ToolStripButton_Click
        End With

        ToolStripButtonEnable = New ToolStripButton
        With ToolStripButtonEnable
            .Image = Icons.Enable
            .ToolTipText = "Enable Selected Items"
            AddHandler .Click, AddressOf ToolStripButton_Click
        End With

        ToolStripButtonDisable = New ToolStripButton
        With ToolStripButtonDisable
            .Image = Icons.Disable
            .ToolTipText = "Disable Selected Items"
            AddHandler .Click, AddressOf ToolStripButton_Click
        End With

        ToolStripButtonIPv4Enable = New ToolStripButton
        With ToolStripButtonIPv4Enable
            .Image = Icons.IPv4Enable
            .ToolTipText = "Get IPv4 Addresses"
            AddHandler .Click, AddressOf ToolStripButton_Click
        End With

        ToolStripButtonIPv4Disable = New ToolStripButton
        With ToolStripButtonIPv4Disable
            .Image = Icons.IPv4Disable
            .ToolTipText = "Don't Get IPv4 Addresses"
            AddHandler .Click, AddressOf ToolStripButton_Click
        End With

        ToolStripButtonIPv6Enable = New ToolStripButton
        With ToolStripButtonIPv6Enable
            .Image = Icons.IPv6Enable
            .ToolTipText = "Get IPv6 Addresses"
            AddHandler .Click, AddressOf ToolStripButton_Click
        End With

        ToolStripButtonIPv6Disable = New ToolStripButton
        With ToolStripButtonIPv6Disable
            .Image = Icons.IPv6Disable
            .ToolTipText = "Don't Get IPv6 Addresses"
            AddHandler .Click, AddressOf ToolStripButton_Click
        End With

        ToolStripButtonOpen = New ToolStripButton
        With ToolStripButtonOpen
            .Image = Icons.Open
            .ToolTipText = "Open Configuration"
            AddHandler .Click, AddressOf ToolStripButton_Click
        End With

        ToolStripButtonRun = New ToolStripButton
        With ToolStripButtonRun
            .Image = Icons.Run
            .ToolTipText = "Run"
            AddHandler .Click, AddressOf ToolStripButton_Click
        End With

        With Me
            .GripStyle = ToolStripGripStyle.Hidden
            .RenderMode = ToolStripRenderMode.Professional
            .BackColor = Color.Transparent

            .Items.Add(ToolStripLabelTitle)
            .Items.Add(New ToolStripSeparator)
            .Items.Add(ToolStripButtonAdd)
            .Items.Add(ToolStripButtonRemove)
            .Items.Add(New ToolStripSeparator)
            .Items.Add(ToolStripButtonEnable)
            .Items.Add(ToolStripButtonDisable)
            .Items.Add(New ToolStripSeparator)
            .Items.Add(ToolStripButtonIPv4Enable)
            .Items.Add(ToolStripButtonIPv4Disable)
            .Items.Add(ToolStripButtonIPv6Enable)
            .Items.Add(ToolStripButtonIPv6Disable)
            .Items.Add(New ToolStripSeparator)
            .Items.Add(ToolStripButtonOpen)
            .Items.Add(ToolStripButtonSave)
            .Items.Add(New ToolStripSeparator)
            .Items.Add(ToolStripButtonRun)
            .Items.Add(New ToolStripSeparator)
            .Items.Add(ToolStripButtonQuit)

            AddHandler .MouseDown, AddressOf ToolStrip_MouseDown
            AddHandler .MouseUp, AddressOf ToolStrip_MouseUp
            AddHandler .MouseMove, AddressOf ToolStrip_MouseMove
        End With
    End Sub

    Private Sub ToolStripButton_Click(sender As Object, e As EventArgs)
        If sender Is ToolStripButtonAdd Then
            RaiseEvent ToolStripButtonAdd_Click(sender, e)
        ElseIf sender Is ToolStripButtonRemove Then
            RaiseEvent ToolStripButtonRemove_Click(sender, e)
        ElseIf sender Is ToolStripButtonQuit Then
            RaiseEvent ToolStripButtonQuit_Click(sender, e)
        ElseIf sender Is ToolStripButtonEnable Then
            RaiseEvent ToolStripButtonEnable_Click(sender, e)
        ElseIf sender Is ToolStripButtonDisable Then
            RaiseEvent ToolStripButtonDisable_Click(sender, e)
        ElseIf sender Is ToolStripButtonIPv4Enable Then
            RaiseEvent ToolStripButtonIPv4Enable_Click(sender, e)
        ElseIf sender Is ToolStripButtonIPv6Enable Then
            RaiseEvent ToolStripButtonIPv6Enable_Click(sender, e)
        ElseIf sender Is ToolStripButtonIPv4Disable Then
            RaiseEvent ToolStripButtonIPv4Disable_Click(sender, e)
        ElseIf sender Is ToolStripButtonIPv6Disable Then
            RaiseEvent ToolStripButtonIPv6Disable_Click(sender, e)
        ElseIf sender Is ToolStripButtonOpen Then
            RaiseEvent ToolStripButtonOpen_Click(sender, e)
        ElseIf sender Is ToolStripButtonSave Then
            RaiseEvent ToolStripButtonSave_Click(sender, e)
        End If
    End Sub

    Private Sub ToolStrip_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs)
        If Me.Parent Is Nothing Then
            Exit Sub
        End If

        ' Change the cursor shape.
        Me.Cursor = Cursors.SizeAll
        ' Save the location of mouse.
        Me.MouseDownLocation = e.Location
        ' Save the location of FormMain.
        Me.FormLastLocation = Me.Location
    End Sub

    Private Sub ToolStrip_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs)
        If Me.Parent Is Nothing Then
            Exit Sub
        End If

        ' Recover the cursor shape.
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub ToolStrip_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs)
        If Me.Parent Is Nothing Then
            Exit Sub
        End If

        ' If the mouse button is not pressed, exit this sub.
        If Not Me.Cursor Is Cursors.SizeAll Then
            Exit Sub
        End If

        ' Update the location of the FormMain.
        Me.Parent.Location = New Point(Me.Parent.Location.X - Me.MouseDownLocation.X + e.Location.X,
                                       Me.Parent.Location.Y - Me.MouseDownLocation.Y + e.Location.Y)
    End Sub

    Public Sub SetEnable(ByVal Enabled As Boolean)
        ToolStripButtonRemove.Enabled = Enabled
        ToolStripButtonEnable.Enabled = Enabled
        ToolStripButtonDisable.Enabled = Enabled
        ToolStripButtonIPv4Enable.Enabled = Enabled
        ToolStripButtonIPv6Enable.Enabled = Enabled
        ToolStripButtonIPv6Disable.Enabled = Enabled
        ToolStripButtonIPv4Disable.Enabled = Enabled
    End Sub
End Class
