Public Class ToolStripCheckBox
    Inherits System.Windows.Forms.ToolStripControlHost

    Private CheckBox As CheckBox

    Public Overrides Property Text As String
        Get
            Return CheckBox.Text
        End Get
        Set(value As String)
            CheckBox.Text = value
        End Set
    End Property

    'Public Property LeftPadding As Integer
    '    Get
    '        Return Me.Padding.Left
    '    End Get
    '    Set(value As Integer)
    '        With Me.Padding
    '            Me.Padding = New Padding(value, .Top, .Right, .Bottom)
    '        End With
    '    End Set
    'End Property

    'Public Property RightPadding As Integer
    '    Get
    '        Return Me.Padding.Right
    '    End Get
    '    Set(value As Integer)
    '        With Me.Padding
    '            Me.Padding = New Padding(.Left, .Top, value, .Bottom)
    '        End With
    '    End Set
    'End Property

    'Public Property HorizontalPadding As Integer
    '    Get
    '        Return Me.Padding.Right
    '    End Get
    '    Set(value As Integer)
    '        With Me.Padding
    '            Me.Padding = New Padding(value, .Top, value, .Bottom)
    '        End With
    '    End Set
    'End Property

    Public Align As HorizontalAlignment
    Public Shadows Width As Integer

    Public Sub New()
        MyBase.New(New CheckBox)

        CheckBox = Me.Control
        CheckBox.Anchor = AnchorStyles.None
        CheckBox.BackColor = Color.FromArgb(0, 255, 255, 255)
        'AddHandler CheckBox.SizeChanged, AddressOf CheckBox_SizeChanged
        CheckBox.MinimumSize = New Size(100, CheckBox.MinimumSize.Height)
        CheckBox.Margin = New Padding(100, 0, 100, 0)
    End Sub

    Public Sub CheckBox_SizeChanged()
        Dim PaddingValue As Integer = 0

        With Me.Padding
            Select Case Align
                Case HorizontalAlignment.Center
                    PaddingValue = 0.5 * (Me.Width - CheckBox.Width)
                    Me.Padding = New Padding(PaddingValue, .Top, PaddingValue, .Bottom)
                Case HorizontalAlignment.Left
                    PaddingValue = Me.Width - CheckBox.Width
                    Me.Padding = New Padding(0, .Top, PaddingValue, .Bottom)
                Case HorizontalAlignment.Right
                    PaddingValue = Me.Width - CheckBox.Width
                    Me.Padding = New Padding(PaddingValue, .Top, 0, .Bottom)
            End Select
        End With
    End Sub
End Class
