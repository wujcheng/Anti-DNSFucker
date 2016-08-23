Public Class StatusStripMain
    Inherits System.Windows.Forms.StatusStrip

    Private ToolStripLabelMessage As ToolStripLabel

    Public Sub New()
        ToolStripLabelMessage = New ToolStripLabel
        With ToolStripLabelMessage
            .Text = ""
        End With

        With Me
            .SizingGrip = False
            .Items.Add(ToolStripLabelMessage)
        End With
    End Sub

    Public Sub ShowMessage(ByVal Message As String)
        ToolStripLabelMessage.Text = Message
    End Sub
End Class
