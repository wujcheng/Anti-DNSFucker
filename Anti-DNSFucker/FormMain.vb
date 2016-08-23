Public Class FormMain
    Private ColumnStyleList As ArrayList
    Private ToolStripMain As ToolStripMain
    Private StatusStripMain As StatusStripMain
    Private TableLayoutPanelList As TableLayoutPanelList
    Private TableLayoutPanelHead As TableLayoutPanelHead

    Private Configuration As Configuration
    Private ConfigurationPath As String = Application.StartupPath & "\Configuration.xml"

    Private ThreadResolve As Threading.Thread
    Private Timer As Timer

    Private MouseDownLocation As Point
    Private FormLastLocation As Point

    Private PanelInner As Panel

    Public Sub New()
        InitializeComponent()

        With Me
            .Width = 500
            .Height = 400
            .Text = "Anti-DNSFucker"
            .Icon = Icons.Logo
            .ControlBox = False
            .FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        End With

        PanelInner = New Panel
        With PanelInner
            .Dock = DockStyle.Fill
            .BorderStyle = BorderStyle.FixedSingle
            .Parent = Me
        End With

        Configuration = New Configuration
        If Not Configuration.Load(ConfigurationPath) Then
        End If

        InitializeColumnStyleList()
        InitializeStatusStripMain()
        InitializeToolStripMain()
        InitializeTableLayoutPanelHead()
        InitializeTableLayoutPanelList()

        Timer = New Timer
        With Timer
            .Interval = 1000
            AddHandler .Tick, AddressOf Timer_Tick
            Timer.Start()
        End With
    End Sub

    Private Sub Timer_Tick(sender As Object, e As EventArgs)
        ThreadResolve = New Threading.Thread(AddressOf ResolveThreadSub)
        ThreadResolve.SetApartmentState(Threading.ApartmentState.STA)
        ThreadResolve.Start()
    End Sub

    Private Sub InitializeColumnStyleList()
        ColumnStyleList = New ArrayList
        With ColumnStyleList
            .Add(New ColumnStyle(SizeType.Percent, 1))
            .Add(New ColumnStyle(SizeType.Percent, 2))
            .Add(New ColumnStyle(SizeType.Percent, 9))
            .Add(New ColumnStyle(SizeType.Percent, 3))
            .Add(New ColumnStyle(SizeType.Percent, 3))
        End With
    End Sub

    Private Sub InitializeToolStripMain()
        ToolStripMain = New ToolStripMain
        With ToolStripMain
            .Text = Me.Text
            .Parent = PanelInner

            AddHandler .ToolStripButtonAdd_Click, AddressOf ToolStripButtonAdd_Click
            AddHandler .ToolStripButtonRemove_Click, AddressOf ToolStripButtonRemove_Click
            AddHandler .ToolStripButtonQuit_Click, AddressOf ToolStripButtonQuit_Click
            AddHandler .ToolStripButtonEnable_Click, AddressOf ToolStripButtonEnable_Click
            AddHandler .ToolStripButtonDisable_Click, AddressOf ToolStripButtonDisable_Click
            AddHandler .ToolStripButtonIPv4Enable_Click, AddressOf ToolStripButtonIPv4Enable_Click
            AddHandler .ToolStripButtonIPv4Disable_Click, AddressOf ToolStripButtonIPv4Disable_Click
            AddHandler .ToolStripButtonIPv6Enable_Click, AddressOf ToolStripButtonIPv6Enable_Click
            AddHandler .ToolStripButtonIPv6Disable_Click, AddressOf ToolStripButtonIPv6Disable_Click
            AddHandler .ToolStripButtonSave_Click, AddressOf ToolStripButtonSave_Click
            AddHandler .ToolStripButtonOpen_Click, AddressOf ToolStripButtonOpen_Click
            AddHandler .ToolStripButtonRun_Click, AddressOf ToolStripButtonRun_Click
            AddHandler .ToolStripButtonAbout_Click, AddressOf ToolStripButtonAbout_Click
            AddHandler .ToolStrip_MouseDown, AddressOf ToolStrip_MouseDown
            AddHandler .ToolStrip_MouseMove, AddressOf ToolStrip_MouseMove
            AddHandler .ToolStrip_MouseUp, AddressOf ToolStrip_MouseUp
        End With

        ShowMessage("Toolbar has been initialized.")
    End Sub

    Private Sub InitializeStatusStripMain()
        StatusStripMain = New StatusStripMain
        With StatusStripMain
            .Parent = PanelInner
        End With
        ShowMessage("Statusbar has been initialized.")
    End Sub

    Private Sub InitializeTableLayoutPanelHead()
        TableLayoutPanelHead = New TableLayoutPanelHead(ColumnStyleList)
        With TableLayoutPanelHead
            .Width = PanelInner.Width
            .Location = New Point(0, ToolStripMain.Height)
            .Anchor = AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Top
            .Parent = PanelInner

            AddHandler .SelectAllCheckedChanged, AddressOf TableLayoutPanelHead_SelectAllCheckedChanged
        End With
        ShowMessage("Titlebar has been initialized.")
    End Sub

    Private Sub InitializeTableLayoutPanelList()
        TableLayoutPanelList = New TableLayoutPanelList(ColumnStyleList)
        With TableLayoutPanelList
            .Location = New Point(0, TableLayoutPanelHead.Height + ToolStripMain.Height)
            .Size = New Size(PanelInner.Width, PanelInner.Height - TableLayoutPanelHead.Height - ToolStripMain.Height - StatusStripMain.Height)
            .Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
            .Parent = PanelInner

            AddHandler .SelectedChanged, AddressOf TableLayoutPanelList_SelectedChanged
        End With

        TableLayoutPanelList.Fill(ConfigurationPath)
        TableLayoutPanelList.ConfigurationPath = ConfigurationPath
        ShowMessage("Configuration has been loaded.")
    End Sub

    Private Sub ResolveThreadSub()
        RemoveHandler Timer.Tick, AddressOf Timer_Tick
        For Each DomainNameItem As DomainNameItem In TableLayoutPanelList.Controls
            If Not DomainNameItem.IsResolved Then
                If DomainNameItem.Resolve() Then
                    ShowMessage("The domain name """ & DomainNameItem.DomainName & """ has been resolved.")
                Else
                    ShowMessage("The domain name """ & DomainNameItem.DomainName & """ is failed to be resolved.")
                End If
            End If
        Next
        ShowMessage("All domain names are resolved, please click ""Run"" button to overwrite the Hosts file.")
        AddHandler Timer.Tick, AddressOf Timer_Tick
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        For Each Control As Object In ToolStripMain.Items
            If Not TypeOf Control Is ToolStripButton Then
                Continue For
            End If

            If Control.Tag Is Nothing Then
                Continue For
            End If

            If Not Control.Tag = keyData Then
                Continue For
            End If

            If Not CType(Control, ToolStripButton).Enabled Then
                Exit For
            End If

            ToolStripMain.ToolStripButton_Click(Control, Nothing)
            Exit For
        Next

        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

    Private Sub ToolStrip_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs)
        ' Change the cursor shape.
        Me.Cursor = Cursors.SizeAll
        ' Save the location of mouse.
        Me.MouseDownLocation = e.Location
        ' Save the location of FormMain.
        Me.FormLastLocation = Me.Location
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

    Private Sub ToolStrip_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs)
        ' Recover the cursor shape.
        Me.Cursor = Cursors.Default
    End Sub


    Private Sub ToolStripButtonAdd_Click(sender As Object, e As EventArgs)
        TableLayoutPanelList.AddItem()
    End Sub

    Private Sub ToolStripButtonRemove_Click(sender As Object, e As EventArgs)
        TableLayoutPanelList.RemoveSelectedItems()
    End Sub

    Private Sub ToolStripButtonQuit_Click(sender As Object, e As EventArgs)
        If Not ThreadResolve Is Nothing Then
            If ThreadResolve.ThreadState = Threading.ThreadState.Running Then
                ThreadResolve.Abort()
            End If
        End If
        TableLayoutPanelList.SaveConfiguration()
        End
    End Sub

    Private Sub ToolStripButtonEnable_Click(sender As Object, e As EventArgs)
        TableLayoutPanelList.EnableSelectedItems(True)
    End Sub

    Private Sub ToolStripButtonDisable_Click(sender As Object, e As EventArgs)
        TableLayoutPanelList.EnableSelectedItems(False)
    End Sub

    Private Sub ToolStripButtonIPv4Enable_Click(sender As Object, e As EventArgs)
        TableLayoutPanelList.GetIPvXAddressSelectedItems(True, 4)
    End Sub

    Private Sub ToolStripButtonIPv4Disable_Click(sender As Object, e As EventArgs)
        TableLayoutPanelList.GetIPvXAddressSelectedItems(False, 4)
    End Sub

    Private Sub ToolStripButtonIPv6Enable_Click(sender As Object, e As EventArgs)
        TableLayoutPanelList.GetIPvXAddressSelectedItems(True, 6)
    End Sub

    Private Sub ToolStripButtonIPv6Disable_Click(sender As Object, e As EventArgs)
        TableLayoutPanelList.GetIPvXAddressSelectedItems(False, 6)
    End Sub

    Private Sub ToolStripButtonSave_Click(sender As Object, e As EventArgs)
        Dim SaveFileDialog As New SaveFileDialog
        With SaveFileDialog
            .FileName = ""
            .Filter = "Configuration File|*.cfg"
        End With

        If Not SaveFileDialog.ShowDialog = DialogResult.OK Then
            Exit Sub
        End If

        TableLayoutPanelList.SaveConfiguration(SaveFileDialog.FileName)
    End Sub

    Private Sub ToolStripButtonOpen_Click(sender As Object, e As EventArgs)
        Dim OpenFileDialog As New OpenFileDialog
        With OpenFileDialog
            .FileName = ""
            .Filter = "Configuration File|*.cfg"
            .Multiselect = False
        End With

        If Not OpenFileDialog.ShowDialog = DialogResult.OK Then
            Exit Sub
        End If

        TableLayoutPanelList.Fill(OpenFileDialog.FileName)
    End Sub

    Private Sub ToolStripButtonRun_Click(sender As Object, e As EventArgs)
        While Not TableLayoutPanelList.AllResolved
            Application.DoEvents()
        End While

        Dim Hosts As New Hosts
        For Each DomainNameItem As DomainNameItem In TableLayoutPanelList.Controls
            If Not DomainNameItem.Enabled Then
                Hosts.Remove(DomainNameItem.DomainName)
                Continue For
            End If

            If DomainNameItem.GetIPv4Address Then
                Hosts.Add(DomainNameItem.DomainName, DomainNameItem.IPv4Address)
            End If

            If DomainNameItem.GetIPv6Address Then
                Hosts.Add(DomainNameItem.DomainName, DomainNameItem.IPv6Address)
            End If
        Next
        Hosts.Save()
        MsgBox("The Hosts file has been overwritted.", MsgBoxStyle.OkOnly, "Done")
    End Sub

    Public Sub ToolStripButtonAbout_Click(sender As Object, e As EventArgs)
        Dim FormAbout As New FormAbout
        FormAbout.StartPosition = FormStartPosition.Manual
        FormAbout.Location = New Point(Me.Location.X + 0.5 * (Me.Width - FormAbout.Width),
                                       Me.Location.Y + 0.5 * (Me.Height - FormAbout.Height))
        FormAbout.ShowDialog()
    End Sub

    Private Sub TableLayoutPanelHead_SelectAllCheckedChanged(sender As Object, e As EventArgs)
        TableLayoutPanelList.SelectAllItems(TableLayoutPanelHead.SelectAllChecked)
        ToolStripMain.SetEnable(TableLayoutPanelHead.SelectAllChecked)
    End Sub

    Private Sub TableLayoutPanelList_SelectedChanged(sender As Object, e As EventArgs)
        TableLayoutPanelHead.CheckBoxSelectAllEventEnabled = False
        TableLayoutPanelHead.SelectAllChecked = TableLayoutPanelList.SelectedState
        TableLayoutPanelHead.CheckBoxSelectAllEventEnabled = True

        ToolStripMain.SetEnable(Not TableLayoutPanelList.SelectedState = CheckState.Unchecked)
    End Sub

    Private Sub ShowMessage(ByVal Message As String)
        StatusStripMain.ShowMessage(Message)
    End Sub
End Class