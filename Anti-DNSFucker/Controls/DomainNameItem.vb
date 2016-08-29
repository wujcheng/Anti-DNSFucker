''' <summary>
''' This class is the item in the TableLayoutPanelList.
''' This item contains:
'''   a CheckBox which is used to indicate if this item is selected,
'''   a CheckBox which is used to indicate if this item is enable,
'''   a CheckBox which is used to indicate if get IPv4 address of this item,
'''   a CheckBox which is used to indicate if get IPv6 address of this item, and
'''   a TextBox which is used to display the domain name of this item.
''' </summary>
Public Class DomainNameItem
    ' This class inherits TableLayoutPanel.
    Inherits System.Windows.Forms.TableLayoutPanel

    ''' <summary>
    ''' This property is used to store the IPv4 address of this item.
    ''' </summary>
    Public IPv4Address As String = ""
    ''' <summary>
    ''' This property is used to store the IPv6 address of this item.
    ''' </summary>
    Public IPv6Address As String = ""
    ''' <summary>
    ''' This flag is used to indicate if the domain name of this item has been resolved.
    ''' </summary>
    Public IsResolved As Boolean = False
    ''' <summary>
    ''' This event is triggered when checked state of CheckBoxSelect is changed.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Public Event SelectCheckedChanged(sender As Object, e As EventArgs)
    ''' <summary>
    ''' This property is used to indicate if this item is selected.
    ''' </summary>
    ''' <returns></returns>
    Public Property Selected As Boolean
        Get
            Return CheckBoxSelect.Checked
        End Get
        Set(value As Boolean)
            CheckBoxSelect.Checked = value
        End Set
    End Property

    ''' <summary>
    ''' This property is used to indicate if this item is enabled.
    ''' </summary>
    ''' <returns></returns>
    Public Shadows Property Enabled As Boolean
        Get
            Return CheckBoxEnable.Checked
        End Get
        Set(value As Boolean)
            CheckBoxEnable.Checked = value
        End Set
    End Property

    ''' <summary>
    ''' This property is used to indicate if get IPv4 address of the domain name of this item.
    ''' </summary>
    ''' <returns></returns>
    Public Property GetIPv4Address As Boolean
        Get
            Return CheckBoxGetIPv4Address.Checked
        End Get
        Set(value As Boolean)
            CheckBoxGetIPv4Address.Checked = value
        End Set
    End Property

    ''' <summary>
    ''' This property is used to indicate if get IPv6 address of the domain name of this item.
    ''' </summary>
    ''' <returns></returns>
    Public Property GetIPv6Address As Boolean
        Get
            Return CheckBoxGetIPv6Address.Checked
        End Get
        Set(value As Boolean)
            CheckBoxGetIPv6Address.Checked = value
        End Set
    End Property

    ''' <summary>
    ''' This property is the domain name of this item.
    ''' </summary>
    ''' <returns></returns>
    Public Property DomainName As String
        Get
            Return TextBoxDomainName.Text
        End Get
        Set(value As String)
            TextBoxDomainName.Text = value
        End Set
    End Property

    ''' <summary>
    ''' This writeonly property is used to enable/disable the CheckedChanged event of CheckBoxSelect.
    ''' </summary>
    Public WriteOnly Property CheckBoxSelectEventEnabled As Boolean
        Set(value As Boolean)
            If value Then
                AddHandler CheckBoxSelect.CheckedChanged, AddressOf CheckBox_CheckedChanged
            Else
                RemoveHandler CheckBoxSelect.CheckedChanged, AddressOf CheckBox_CheckedChanged
            End If
        End Set
    End Property


    ' This CheckBox is used to indicate if this item is selected.
    Private CheckBoxSelect As CheckBox
    ' This CheckBox which is used to indicate if this item is enable.
    Private CheckBoxEnable As CheckBox
    ' This CheckBox which is used to indicate if get IPv4 address of this item.
    Private CheckBoxGetIPv4Address As CheckBoxAdvanced
    ' This CheckBox which is used to indicate if get IPv6 address of this item.
    Private CheckBoxGetIPv6Address As CheckBoxAdvanced
    ' TextBox which is used to display the domain name of this item.
    Private TextBoxDomainName As TextBoxWithWaterMark
    ' This is the height of this item.
    Private Const ItemHeight As Integer = 27
    ' This is the padding of all CheckBoxes in this item.
    Private CheckBoxPadding As Padding = New Padding(0, 0, 0, 0)
    ' This is the margin of all CheckBoxes in this item.
    Private CheckBoxMargin As Padding = New Padding(3, 0, 0, 0)
    ' This is the margin of the TextBox in this item.
    Private TextBoxMargin As Padding = New Padding(3, 3, 3, 3)
    ' This color is backcolor of the TextBox if the IP address is resolved successfully.
    Private SuccessfulColor As Color = Color.FromArgb(&HFF7FFF7F)
    ' This color is backcolor of the TextBox if the IP address is not resolved successfully.
    Private FailedColor As Color = Color.FromArgb(&HFFFF7F7F)
    ' This delegate refer to sub SetCheckBoxGetIPvXEnable.
    ' This array list is used to store the column styles of this item.
    Private ColumnNameList As ArrayList
    Delegate Sub DelegateSetCheckBoxGetIPvXEnable()

    ''' <summary>
    ''' This is the constructor.
    ''' </summary>
    ''' <param name="ColumnStyleList"></param>
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

    ''' <summary>
    ''' This function is used to resolve the IP addresses of the domain name of this item.
    ''' </summary>
    ''' <returns>If the domain name is resolved successfully, return true, else, return false.</returns>
    Public Function Resolve() As Boolean
        Dim IP As New IP
        IP.ResolveByNEU(DomainName)
        IPv4Address = IP.IPv4Address
        IPv6Address = IP.IPv6Address

        Me.Invoke(New DelegateSetCheckBoxGetIPvXEnable(AddressOf SetCheckBoxGetIPvXEnable))
        IsResolved = True

        Return Not (IPv4Address.Trim = "" And IPv6Address.Trim = "")
    End Function

    ''' <summary>
    ''' This sub is used to set the properties Broken, Enable, and BackColor of this item according to the result of resolving.
    ''' </summary>
    Private Sub SetCheckBoxGetIPvXEnable()
        CheckBoxGetIPv4Address.Broken = IPv4Address.Trim = ""
        CheckBoxGetIPv6Address.Broken = IPv6Address.Trim = ""

        CheckBoxGetIPv4Address.Enabled = CheckBoxEnable.Checked
        CheckBoxGetIPv6Address.Enabled = CheckBoxEnable.Checked

        TextBoxDomainName.BackColor = If(CheckBoxGetIPv4Address.Broken And CheckBoxGetIPv6Address.Broken, FailedColor, SuccessfulColor)
    End Sub

    ''' <summary>
    ''' This sub will be triggered when the text of TextBoxDomainName is changed.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextBoxDomainName_TextChanged(sender As Object, e As EventArgs)
        IsResolved = False
    End Sub

    ''' <summary>
    ''' This sub will be triggered when check state of CheckBoxEnable or CheckBoxSelect is changed.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub CheckBox_CheckedChanged(sender As Object, e As EventArgs)
        If sender Is CheckBoxEnable Then
            ' If this sub is triggered by CheckBoxEnable.

            ' Set the Broken of CheckBoxGetIPv4Address and CheckBoxGetIPv6Address.
            CheckBoxGetIPv4Address.Broken = IPv4Address.Trim = ""
            CheckBoxGetIPv6Address.Broken = IPv6Address.Trim = ""
            ' Then, set the Enabled of CheckBoxGetIPv4Address, CheckBoxGetIPv6Address, and TextBoxDomainName.
            CheckBoxGetIPv4Address.Enabled = CheckBoxEnable.Checked
            CheckBoxGetIPv6Address.Enabled = CheckBoxEnable.Checked
            TextBoxDomainName.Enabled = CheckBoxEnable.Checked
        ElseIf sender Is CheckBoxSelect Then
            ' If this sub is triggered by CheckBoxSelect.
            ' Raise the event SelectCheckedChanged.
            RaiseEvent SelectCheckedChanged(sender, e)
        End If
    End Sub

    ''' <summary>
    ''' This sub is used to draw vertical line on this item.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
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

    ''' <summary>
    ''' This function is used to get the value of control by name of the control.
    ''' If there is no this control, return an empty string.
    ''' </summary>
    ''' <param name="Name"></param>
    ''' <returns></returns>
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
