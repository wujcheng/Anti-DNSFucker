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
    Public Event ToolStripButtonRun_Click(sender As Object, e As EventArgs)
    Public Event ToolStrip_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs)
    Public Event ToolStrip_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs)
    Public Event ToolStrip_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs)

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
            .ToolTipText = "Drag Here to Move the Form"
            AddHandler .MouseDown, AddressOf Me_MouseDown
            AddHandler .MouseUp, AddressOf Me_MouseUp
            AddHandler .MouseMove, AddressOf Me_MouseMove
        End With

        ToolStripButtonAdd = New ToolStripButton
        With ToolStripButtonAdd
            .Image = Icons.Add
            .Tag = Keys.Control Or Keys.N
            .ToolTipText = "Add New Domain Name" & vbCr & KeyDataToString(.Tag)
            AddHandler .Click, AddressOf ToolStripButton_Click
        End With

        ToolStripButtonRemove = New ToolStripButton
        With ToolStripButtonRemove
            .Image = Icons.Remove
            .Tag = Keys.Control Or Keys.X
            .ToolTipText = "Remove Selected Items" & vbCr & KeyDataToString(.Tag)
            AddHandler .Click, AddressOf ToolStripButton_Click
        End With

        ToolStripButtonQuit = New ToolStripButton
        With ToolStripButtonQuit
            .Image = Icons.Quit
            .Tag = Keys.Alt Or Keys.Q
            .ToolTipText = "Quit" & vbCr & KeyDataToString(.Tag)
            AddHandler .Click, AddressOf ToolStripButton_Click
        End With

        ToolStripButtonSave = New ToolStripButton
        With ToolStripButtonSave
            .Image = Icons.Save
            .Tag = Keys.Control Or Keys.S
            .ToolTipText = "Save Configuration to File" & vbCr & KeyDataToString(.Tag)
            AddHandler .Click, AddressOf ToolStripButton_Click
        End With

        ToolStripButtonEnable = New ToolStripButton
        With ToolStripButtonEnable
            .Image = Icons.Enable
            .Tag = Keys.Control Or Keys.E
            .ToolTipText = "Enable Selected Items" & vbCr & KeyDataToString(.Tag)
            AddHandler .Click, AddressOf ToolStripButton_Click
        End With

        ToolStripButtonDisable = New ToolStripButton
        With ToolStripButtonDisable
            .Image = Icons.Disable
            .Tag = Keys.Control Or Keys.D
            .ToolTipText = "Disable Selected Items" & vbCr & KeyDataToString(.Tag)
            AddHandler .Click, AddressOf ToolStripButton_Click
        End With

        ToolStripButtonIPv4Enable = New ToolStripButton
        With ToolStripButtonIPv4Enable
            .Image = Icons.IPv4Enable
            .Tag = Keys.Control Or Keys.D4
            .ToolTipText = "Get IPv4 Addresses" & vbCr & KeyDataToString(.Tag)
            AddHandler .Click, AddressOf ToolStripButton_Click
        End With

        ToolStripButtonIPv4Disable = New ToolStripButton
        With ToolStripButtonIPv4Disable
            .Image = Icons.IPv4Disable
            .Tag = Keys.Control Or Keys.Shift Or Keys.D4
            .ToolTipText = "Don't Get IPv4 Addresses" & vbCr & KeyDataToString(.Tag)
            AddHandler .Click, AddressOf ToolStripButton_Click
        End With

        ToolStripButtonIPv6Enable = New ToolStripButton
        With ToolStripButtonIPv6Enable
            .Image = Icons.IPv6Enable
            .Tag = Keys.Control Or Keys.D6
            .ToolTipText = "Get IPv6 Addresses" & vbCr & KeyDataToString(.Tag)
            AddHandler .Click, AddressOf ToolStripButton_Click
        End With

        ToolStripButtonIPv6Disable = New ToolStripButton
        With ToolStripButtonIPv6Disable
            .Image = Icons.IPv6Disable
            .Tag = Keys.Control Or Keys.Shift Or Keys.D6
            .ToolTipText = "Don't Get IPv6 Addresses" & vbCr & KeyDataToString(.Tag)
            AddHandler .Click, AddressOf ToolStripButton_Click
        End With

        ToolStripButtonOpen = New ToolStripButton
        With ToolStripButtonOpen
            .Image = Icons.Open
            .Tag = Keys.Control Or Keys.O
            .ToolTipText = "Open Configuration" & vbCr & KeyDataToString(.Tag)
            AddHandler .Click, AddressOf ToolStripButton_Click
        End With

        ToolStripButtonRun = New ToolStripButton
        With ToolStripButtonRun
            .Image = Icons.Run
            .Tag = Keys.F5
            .ToolTipText = "Run" & vbCr & KeyDataToString(.Tag)
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

            AddHandler .MouseDown, AddressOf Me_MouseDown
            AddHandler .MouseUp, AddressOf Me_MouseUp
            AddHandler .MouseMove, AddressOf Me_MouseMove
        End With
    End Sub

    Private Function KeyDataToString(ByVal KeyData As Keys) As String
        Dim KeysConverter As New KeysConverter
        Return KeysConverter.ConvertToString(KeyData)
    End Function

    Public Sub ToolStripButton_Click(sender As Object, e As EventArgs)
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
        ElseIf sender Is ToolStripButtonRun Then
            RaiseEvent ToolStripButtonRun_Click(sender, e)
        End If
    End Sub

    Private Sub Me_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs)
        RaiseEvent ToolStrip_MouseDown(sender, e)
    End Sub

    Private Sub Me_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs)
        RaiseEvent ToolStrip_MouseUp(sender, e)
    End Sub

    Private Sub Me_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs)
        RaiseEvent ToolStrip_MouseMove(sender, e)
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
