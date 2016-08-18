Public Class TableLayoutPanelWithDoubleBuffer
    Inherits TableLayoutPanel

    Public Sub New()
        MyBase.New
        Me.DoubleBuffered = True
    End Sub
End Class
