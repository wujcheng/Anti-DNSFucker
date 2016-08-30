''' <summary>
''' This class is the status strip at the bottom of the FormMain.
''' </summary>
Public Class StatusStripMain
    ' It inherits from StatusStrip
    Inherits System.Windows.Forms.StatusStrip

    ' This is a Label which is used to show the message.
    Private ToolStripLabelMessage As ToolStripLabel

    ''' <summary>
    ''' This is the constructor.
    ''' </summary>
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

    ''' <summary>
    ''' This sub is used to show the message on the Label.
    ''' </summary>
    ''' <param name="Message"></param>
    Public Sub ShowMessage(ByVal Message As String)
        ToolStripLabelMessage.Text = Message
    End Sub
End Class
