Public Class DomainNameItem
    Inherits System.Windows.Forms.TableLayoutPanel

    Private CheckBoxSelect As CheckBox
    Private CheckBoxEnable As CheckBox
    Private CheckBoxGetIPv6Address As CheckBox
    Private CheckBoxGetIPv4Address As CheckBox
    Private TextBoxDomainName As TextBoxWithWaterMark

    Private IPv4Address As String
    Private IPv6Address As String

    Private Const ItemHeight As Integer = 27
    Private CheckBoxPadding As Padding = New Padding(0, 2, 0, 0)

    Public Sub New(ByVal ColumnStyleList As ArrayList)
        With Me
            .Dock = DockStyle.Fill
            .Height = ItemHeight

            For Each ColumnStyle As ColumnStyle In ColumnStyleList
                .ColumnStyles.Add(New ColumnStyle(ColumnStyle.SizeType, ColumnStyle.Width))
            Next
            .ColumnCount = ColumnStyleList.Count

            CheckBoxSelect = New CheckBox
            With CheckBoxSelect
                .Text = ""
                .Padding = CheckBoxPadding
                .Margin = New Padding(0)
                AddHandler .CheckedChanged, AddressOf CheckBox_CheckedChanged
            End With

            CheckBoxEnable = New CheckBox
            With CheckBoxEnable
                .Text = ""
                .Padding = CheckBoxPadding
                .Margin = New Padding(0)
                AddHandler .CheckedChanged, AddressOf CheckBox_CheckedChanged
            End With

            CheckBoxGetIPv6Address = New CheckBox
            With CheckBoxGetIPv6Address
                .Text = ""
                .Padding = CheckBoxPadding
                .Margin = New Padding(0)
                AddHandler .CheckedChanged, AddressOf CheckBox_CheckedChanged
            End With

            CheckBoxGetIPv4Address = New CheckBox
            With CheckBoxGetIPv4Address
                .Text = ""
                .Padding = CheckBoxPadding
                .Margin = New Padding(0)
                AddHandler .CheckedChanged, AddressOf CheckBox_CheckedChanged
            End With

            TextBoxDomainName = New TextBoxWithWaterMark
            With TextBoxDomainName
                .WaterMarkText = "Please Input Domain Name."
                .Dock = DockStyle.Fill
            End With

            .Controls.Add(CheckBoxSelect, 0, 0)
            .Controls.Add(CheckBoxEnable, 1, 0)
            .Controls.Add(TextBoxDomainName, 2, 0)
            .Controls.Add(CheckBoxGetIPv4Address, 3, 0)
            .Controls.Add(CheckBoxGetIPv6Address, 4, 0)
        End With
    End Sub

    Private Sub CheckBox_CheckedChanged(sender As Object, e As EventArgs)

    End Sub
End Class
