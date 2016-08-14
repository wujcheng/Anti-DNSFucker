Public Class WebAddressPanel
    Inherits System.Windows.Forms.ToolStrip

    Private EnableCheckBox As CheckBox
    Private DomainNameTextBox As WaterMarkTextBox
    Private GetIPv4AddressCheckBox As CheckBox
    Private GetIPv6AddressCheckBox As CheckBox
    Private DeleteButton As Button




    Const ControlSeparation As Integer = 4

    Public Sub New()
        MyBase.New
        With Me
            .GripStyle = ToolStripGripStyle.Hidden
            .RenderMode = ToolStripRenderMode.Professional
            .Items.Add(New ToolStripTextBoxWithWaterMark)
            .Items.Add(New ToolStripButton)
            .Items.Add(New ToolStripControlHost(New CheckBox))
        End With


        With Me
            .Width = 200
        End With

    End Sub
End Class
