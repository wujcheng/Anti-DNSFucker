Public Class DomainNameItem
    Inherits System.Windows.Forms.TableLayoutPanel

    Private CheckBoxSelect As CheckBox
    Private CheckBoxEnable As CheckBox
    Private CheckBoxGetIPv6Address As CheckBoxAdvanced
    Private CheckBoxGetIPv4Address As CheckBoxAdvanced
    Private TextBoxDomainName As TextBoxWithWaterMark

    Public IPv4Address As String = ""
    Public IPv6Address As String = ""

    Private Const ItemHeight As Integer = 27
    Private CheckBoxPadding As Padding = New Padding(0, 0, 0, 0)
    Private CheckBoxMargin As Padding = New Padding(3, 0, 0, 0)
    Private TextBoxMargin As Padding = New Padding(3, 3, 3, 3)
    Public Event SelectCheckedChanged(sender As Object, e As EventArgs)

    Delegate Sub DelegateSetCheckBoxGetIPvXEnable()
    Public IsResolved As Boolean = False

    Public Function Resolve() As Boolean
        Dim IP As New IP
        IP.Resolve(DomainName)
        IPv4Address = IP.IPv4Address
        IPv6Address = IP.IPv6Address

        Me.Invoke(New DelegateSetCheckBoxGetIPvXEnable(AddressOf SetCheckBoxGetIPvXEnable))
        IsResolved = True

        Return Not (IPv4Address.Trim = "" And IPv6Address.Trim = "")
    End Function

    Private Sub SetCheckBoxGetIPvXEnable()
        CheckBoxGetIPv4Address.Broken = IPv4Address.Trim = ""
        CheckBoxGetIPv6Address.Broken = IPv6Address.Trim = ""

        CheckBoxGetIPv4Address.Enabled = CheckBoxEnable.Checked
        CheckBoxGetIPv6Address.Enabled = CheckBoxEnable.Checked
    End Sub

    Public Property Selected As Boolean
        Get
            Return CheckBoxSelect.Checked
        End Get
        Set(value As Boolean)
            CheckBoxSelect.Checked = value
        End Set
    End Property

    Public Shadows Property Enabled As Boolean
        Get
            Return CheckBoxEnable.Checked
        End Get
        Set(value As Boolean)
            CheckBoxEnable.Checked = value
        End Set
    End Property

    Public Property GetIPv4Address As Boolean
        Get
            Return CheckBoxGetIPv4Address.Checked
        End Get
        Set(value As Boolean)
            CheckBoxGetIPv4Address.Checked = value
        End Set
    End Property

    Public Property GetIPv6Address As Boolean
        Get
            Return CheckBoxGetIPv6Address.Checked
        End Get
        Set(value As Boolean)
            CheckBoxGetIPv6Address.Checked = value
        End Set
    End Property

    Public Property DomainName As String
        Get
            Return TextBoxDomainName.Text
        End Get
        Set(value As String)
            TextBoxDomainName.Text = value
        End Set
    End Property

    Public ColumnNameList As ArrayList

    Public WriteOnly Property CheckBoxSelectEventEnabled As Boolean
        Set(value As Boolean)
            If value Then
                AddHandler CheckBoxSelect.CheckedChanged, AddressOf CheckBox_CheckedChanged
            Else
                RemoveHandler CheckBoxSelect.CheckedChanged, AddressOf CheckBox_CheckedChanged
            End If
        End Set
    End Property

    Public Sub New(ByVal ColumnStyleList As ArrayList)
        With Me
            .Height = ItemHeight
            .Margin = New Padding(0, 0, 0, 0)

            For Each ColumnStyle As ColumnStyle In ColumnStyleList
                .ColumnStyles.Add(New ColumnStyle(ColumnStyle.SizeType, ColumnStyle.Width))
            Next
            .ColumnCount = ColumnStyleList.Count
            .ColumnNameList = New ArrayList

            CheckBoxSelect = New CheckBox
            With CheckBoxSelect
                .Text = ""
                .Padding = CheckBoxPadding
                .Margin = CheckBoxMargin
                AddHandler .CheckedChanged, AddressOf CheckBox_CheckedChanged
            End With

            CheckBoxEnable = New CheckBox
            With CheckBoxEnable
                .Text = ""
                .Name = NameOf(CheckBoxEnable)
                .Checked = True
                .Padding = CheckBoxPadding
                .Margin = CheckBoxMargin
                AddHandler .CheckedChanged, AddressOf CheckBox_CheckedChanged
            End With

            CheckBoxGetIPv6Address = New CheckBoxAdvanced
            With CheckBoxGetIPv6Address
                .Text = ""
                .Checked = True
                .Name = NameOf(CheckBoxGetIPv6Address)
                .Padding = CheckBoxPadding
                .Margin = CheckBoxMargin
                AddHandler .CheckedChanged, AddressOf CheckBox_CheckedChanged
            End With

            CheckBoxGetIPv4Address = New CheckBoxAdvanced
            With CheckBoxGetIPv4Address
                .Text = ""
                .Checked = True
                .Name = NameOf(CheckBoxGetIPv4Address)
                .Padding = CheckBoxPadding
                .Margin = CheckBoxMargin
                AddHandler .CheckedChanged, AddressOf CheckBox_CheckedChanged
            End With

            TextBoxDomainName = New TextBoxWithWaterMark
            With TextBoxDomainName
                .Dock = DockStyle.Fill
                .Name = NameOf(TextBoxDomainName)
                .Margin = TextBoxMargin
                .BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
                .WaterMarkText = "Click Here to Input a New Domain Name."
                AddHandler .TextChanged, AddressOf TextBoxDomainName_TextChanged
            End With

            .Controls.Add(CheckBoxSelect, 0, 0)
            .Controls.Add(CheckBoxEnable, 1, 0)
            .Controls.Add(TextBoxDomainName, 2, 0)
            .Controls.Add(CheckBoxGetIPv4Address, 3, 0)
            .Controls.Add(CheckBoxGetIPv6Address, 4, 0)

            AddHandler .Paint, AddressOf Me_Paint
        End With
    End Sub

    Private Sub TextBoxDomainName_TextChanged(sender As Object, e As EventArgs)
        IsResolved = False
    End Sub

    Private Sub CheckBox_CheckedChanged(sender As Object, e As EventArgs)
        If sender Is CheckBoxEnable Then
            TextBoxDomainName.Enabled = CheckBoxEnable.Checked

            CheckBoxGetIPv4Address.Broken = IPv4Address.Trim = ""
            CheckBoxGetIPv6Address.Broken = IPv6Address.Trim = ""
            CheckBoxGetIPv4Address.Enabled = CheckBoxEnable.Checked
            CheckBoxGetIPv6Address.Enabled = CheckBoxEnable.Checked
        ElseIf sender Is CheckBoxSelect Then
            RaiseEvent SelectCheckedChanged(sender, e)
        End If
    End Sub

    Private Sub Me_Paint(sender As Object, e As PaintEventArgs)
        Dim Graphics As Graphics = Me.CreateGraphics
        Dim Pen As New Pen(Color.LightGray, 1)
        Dim LeftPadding As Integer = 0

        With Me
            For i As Integer = 0 To .ColumnStyles.Count - 2
                LeftPadding += .GetColumnWidths(i)
                Graphics.DrawLine(Pen, New Point(LeftPadding, .Height - 1), New Point(LeftPadding, 0))
            Next
        End With
    End Sub

    Public Function GetValueByName(ByVal Name As String) As String
        For Each Control As Control In Me.Controls
            If Control.Name = Name Then
                If TypeOf (Control) Is CheckBox Then
                    Return CType(Control, CheckBox).Checked
                ElseIf TypeOf Control Is TextBoxWithWaterMark Then
                    Return CType(Control, TextBoxWithWaterMark).Text
                End If
            End If
        Next

        Return ""
    End Function
End Class
