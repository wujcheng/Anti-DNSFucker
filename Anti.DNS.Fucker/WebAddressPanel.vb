Public Class WebAddressPanel
    Inherits System.Windows.Forms.ToolStrip


    Private DomainNameTextBox As ToolStripTextBoxWithWaterMark
    Private EnableCheckBox As ToolStripCheckBox
    Private GetIPv6CheckBox As ToolStripCheckBox
    Private GetIPv4CheckBox As ToolStripCheckBox
    Private DeleteButton As ToolStripButton

    Public Sub New()
        MyBase.New

        With Me
            .Width = 200
        End With

        DomainNameTextBox = New ToolStripTextBoxWithWaterMark
        With DomainNameTextBox
            .WaterMarkText = "Domain Name"
        End With

        EnableCheckBox = New ToolStripCheckBox
        With EnableCheckBox
            .Text = "1"
            ' .Width = 100
            .Align = HorizontalAlignment.Left

        End With

        'GetIPv6CheckBox = New ToolStripCheckBox
        'With GetIPv6CheckBox
        '    .Text = "2"
        '    .Width = 50
        '    .Align = HorizontalAlignment.Left
        'End With

        'GetIPv4CheckBox = New ToolStripCheckBox
        'With GetIPv4CheckBox
        '    .Text = "3"
        '    .Width = 50
        '    .Align = HorizontalAlignment.Left
        'End With

        DeleteButton = New ToolStripButton
        With DeleteButton
            .Text = ""
            .Image = Image.FromFile(Application.StartupPath & "\Icon\Delete.ico")
            AddHandler .Click, AddressOf DeleteButton_Click
        End With

        With Me
            .GripStyle = ToolStripGripStyle.Hidden
            .RenderMode = ToolStripRenderMode.Professional
            .Items.Add(EnableCheckBox)
            .Items.Add(New ToolStripSeparator)
            .Items.Add(DomainNameTextBox)
            .Items.Add(New ToolStripSeparator)
            '.Items.Add(GetIPv4CheckBox)
            '.Items.Add(New ToolStripSeparator)
            '.Items.Add(GetIPv6CheckBox)
            '.Items.Add(New ToolStripSeparator)
            .Items.Add(DeleteButton)
        End With




    End Sub

    Private Sub DeleteButton_Click()
        MsgBox(EnableCheckBox.Padding.ToString)
    End Sub
End Class
