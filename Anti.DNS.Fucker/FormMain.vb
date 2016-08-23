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

    Public Sub New()
        InitializeComponent()

        With Me
            .Width = 420
            .Height = 400
            .Text = "Anti-DNSFucker"
            .ControlBox = False
            .FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        End With

        Configuration = New Configuration
        If Not Configuration.Load(ConfigurationPath) Then
        End If

        InitializeColumnStyleList()
        InitializeToolStripMain()
        InitializeStatusStripMain()
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

    'Private Sub StartThreadResolve()
    '    If ThreadResolve Is Nothing Then
    '        ThreadResolve = New Threading.Thread(AddressOf ResolveThreadSub)
    '    End If
    '    ThreadResolve.SetApartmentState(Threading.ApartmentState.STA)

    '    If ThreadResolve.IsAlive Then
    '        Exit Sub
    '    End If
    '    ThreadResolve.Start()
    'End Sub

    Private Sub InitializeColumnStyleList()
        ColumnStyleList = New ArrayList
        With ColumnStyleList
            .Add(New ColumnStyle(SizeType.Percent, 1))
            .Add(New ColumnStyle(SizeType.Percent, 2))
            .Add(New ColumnStyle(SizeType.Percent, 5))
            .Add(New ColumnStyle(SizeType.Percent, 2))
            .Add(New ColumnStyle(SizeType.Percent, 2))
        End With
    End Sub

    Private Sub InitializeToolStripMain()
        ToolStripMain = New ToolStripMain
        With ToolStripMain
            .Text = Me.Text
            '.Dock = DockStyle.Fill
            .Parent = Me

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
        End With
    End Sub

    Private Sub InitializeStatusStripMain()
        StatusStripMain = New StatusStripMain
        With StatusStripMain
            .Parent = Me
        End With
    End Sub

    Private Sub InitializeTableLayoutPanelHead()
        TableLayoutPanelHead = New TableLayoutPanelHead(ColumnStyleList)
        With TableLayoutPanelHead
            .Width = Me.ClientSize.Width
            .Location = New Point(0, ToolStripMain.Height)
            .Anchor = AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Top
            .Parent = Me

            AddHandler .SelectAllCheckedChanged, AddressOf TableLayoutPanelHead_SelectAllCheckedChanged
        End With
    End Sub

    Private Sub InitializeTableLayoutPanelList()
        TableLayoutPanelList = New TableLayoutPanelList(ColumnStyleList)
        With TableLayoutPanelList
            .Location = New Point(0, TableLayoutPanelHead.Height + ToolStripMain.Height)
            .Size = New Size(Me.ClientSize.Width, Me.ClientSize.Height - TableLayoutPanelHead.Height - ToolStripMain.Height - StatusStripMain.Height)
            .Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
            .Parent = Me

            AddHandler .SelectedChanged, AddressOf TableLayoutPanelList_SelectedChanged
        End With

        TableLayoutPanelList.Fill(ConfigurationPath)
        TableLayoutPanelList.ConfigurationPath = ConfigurationPath
    End Sub

    Private Sub ResolveThreadSub()
        RemoveHandler Timer.Tick, AddressOf Timer_Tick
        For Each DomainNameItem As DomainNameItem In TableLayoutPanelList.Controls
            If Not DomainNameItem.IsResolved Then
                DomainNameItem.Resolve()
            End If
        Next
        AddHandler Timer.Tick, AddressOf Timer_Tick
    End Sub

    Private Sub ToolStripButtonAdd_Click(sender As Object, e As EventArgs)
        TableLayoutPanelList.AddItem()
    End Sub

    Private Sub ToolStripButtonRemove_Click(sender As Object, e As EventArgs)
        TableLayoutPanelList.RemoveSelectedItems()
    End Sub

    Private Sub ToolStripButtonQuit_Click(sender As Object, e As EventArgs)
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
        MsgBox("Done")

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
End Class