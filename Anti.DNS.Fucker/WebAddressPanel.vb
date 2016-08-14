Public Class WebAddressPanel
    Inherits System.Windows.Forms.Panel

    Private EnableCheckBox As CheckBox
    Private DomainNameTextBox As WaterMarkTextBox
    Private GetIPv4AddressCheckBox As CheckBox
    Private GetIPv6AddressCheckBox As CheckBox
    Private DeleteButton As Button

    Private ToolStrip As ToolStrip

    Const ControlSeparation As Integer = 4

    Public Sub New()
        With Me
            .Width = 200
            .Height = 27
            .BorderStyle = BorderStyle.FixedSingle
        End With

        EnableCheckBox = New CheckBox
        With EnableCheckBox
            .Text = ""
            .Width = 20
            .Location = New Point(0, 0)
            .Anchor = AnchorStyles.Left Or AnchorStyles.Top
            .Parent = Me
        End With

        DeleteButton = New Button
        With DeleteButton
            .Image = Image.FromFile(Application.StartupPath & "\Icon\Delete.ico")
            .Width = Me.Height
            .Height = .Width
            .ImageAlign = ContentAlignment.MiddleCenter
            .FlatStyle = FlatStyle.Flat
            .FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255)
            .FlatAppearance.BorderSize = 0
            .BackgroundImageLayout = ImageLayout.Zoom

            .Location = New Point(Me.Width - .Width, 0)
            .Anchor = AnchorStyles.Left Or AnchorStyles.Top
            .Parent = Me
        End With

        GetIPv6AddressCheckBox = New CheckBox
        With GetIPv6AddressCheckBox
            .Text = ""
            .Width = 20
            .Location = New Point(Me.Width - DeleteButton.Width - ControlSeparation - .Width, 0)
            .Anchor = AnchorStyles.Right Or AnchorStyles.Top
            .Parent = Me
        End With

        GetIPv4AddressCheckBox = New CheckBox
        With GetIPv4AddressCheckBox
            .Text = ""
            .Width = 20
            .Location = New Point(Me.Width - DeleteButton.Width - ControlSeparation - GetIPv6AddressCheckBox.Width - ControlSeparation - .Width, 0)
            .Anchor = AnchorStyles.Right Or AnchorStyles.Top
            .Parent = Me
        End With

        DomainNameTextBox = New WaterMarkTextBox
        With DomainNameTextBox
            .WaterMarkText = "Domain Name"
            .Width = Me.Width - DeleteButton.Width - ControlSeparation - EnableCheckBox.Width - ControlSeparation - GetIPv4AddressCheckBox.Width - ControlSeparation - GetIPv6AddressCheckBox.Width - ControlSeparation
            .Text = ""
            .Location = New Point(EnableCheckBox.Width + ControlSeparation, 0)
            .Anchor = AnchorStyles.Left Or AnchorStyles.Top Or AnchorStyles.Right
            .Parent = Me
        End With
    End Sub
End Class
