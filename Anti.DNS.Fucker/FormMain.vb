Public Class FormMain
    Public Sub New()

        ' 此调用是设计器所必需的。
        InitializeComponent()

        Dim wap As New WebAddressPanel
        Me.Controls.Add(wap)
    End Sub
End Class
