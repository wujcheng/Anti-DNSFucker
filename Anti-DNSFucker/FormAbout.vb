Public Class FormAbout
    Private PanelMain As Panel
    Private PictureBoxLogo As PictureBox
    Private LabelMessage As Label

    Public Sub New()
        InitializeComponent()

        With Me
            .Size = New Size(400, 160)
            .FormBorderStyle = FormBorderStyle.None
            .ControlBox = False
            .ShowInTaskbar = False
        End With

        PanelMain = New Panel
        With PanelMain
            .Dock = DockStyle.Fill
            .BorderStyle = BorderStyle.FixedSingle
            .Parent = Me
        End With

        PictureBoxLogo = New PictureBox
        With PictureBoxLogo
            .Location = New Point(0, 0)
            .Size = New Size(Me.Height, Me.Height)
            .Image = Image.FromFile(".\Icons\Logo.png")
            .SizeMode = PictureBoxSizeMode.StretchImage
            .Parent = PanelMain
            AddHandler .Click, AddressOf Me_Click
        End With

        LabelMessage = New Label
        With LabelMessage
            .AutoSize = False
            .Size = New Size(Me.Width - Me.Height, Me.Height)
            .TextAlign = ContentAlignment.MiddleLeft
            .Location = New Point(Me.Height, 0)
            .Font = New Font((New ToolStripButton).Font.FontFamily, 12, FontStyle.Regular)
            .Text = "Anti-DNSFucker" & vbCr &
                    "v1.0" & vbCr & vbCr &
                    "Author: Qiqi" & vbCr &
                    "Email: qiqi@hust.edu.cn" & vbCr &
                    "GitHub: github.com/zqmillet" & vbCr &
                    "HomePage: zqmillet.github.io"
            .Parent = PanelMain
            AddHandler .Click, AddressOf Me_Click
        End With
    End Sub

    Private Sub Me_Click(sender As Object, e As EventArgs)
        Me.Dispose()
    End Sub
End Class