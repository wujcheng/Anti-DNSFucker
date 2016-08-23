Public Class TableLayoutPanelHead
    Inherits System.Windows.Forms.TableLayoutPanel

    Private CheckBoxSelectAll As CheckBox
    Private LabelDomainName As Label
    Private LabelGetIPv6Address As Label
    Private LabelGetIPv4Address As Label
    Private LabelEnable As Label

    Private Const ItemHeight As Integer = 27
    Private CheckBoxPadding As Padding = New Padding(0, 2, 0, 0)
    Private LabelPadding As Padding = New Padding(0, 7, 0, 0)
    Private TransparentColor As Color = Color.FromArgb(0, Color.Black)

    Public Event SelectAllCheckedChanged(sender As Object, e As EventArgs)

    Public Property SelectAllChecked As CheckState
        Get
            Return CheckBoxSelectAll.CheckState
        End Get
        Set(value As CheckState)
            CheckBoxSelectAll.CheckState = value
        End Set
    End Property

    Public WriteOnly Property CheckBoxSelectAllEventEnabled As Boolean
        Set(value As Boolean)
            If value Then
                AddHandler CheckBoxSelectAll.CheckedChanged, AddressOf CheckBoxSelectAll_CheckedChanged
            Else
                RemoveHandler CheckBoxSelectAll.CheckedChanged, AddressOf CheckBoxSelectAll_CheckedChanged
            End If
        End Set
    End Property

    Public Sub New(ByVal ColumnStyleList As ArrayList)
        With Me
            .Height = ItemHeight
            For Each ColumnStyle As ColumnStyle In ColumnStyleList
                .ColumnStyles.Add(New ColumnStyle(ColumnStyle.SizeType, ColumnStyle.Width))
            Next
            .ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, System.Windows.Forms.SystemInformation.VerticalScrollBarWidth))
            .ColumnCount = ColumnStyleList.Count + 1
            .RowStyles.Add(New RowStyle(SizeType.Absolute, ItemHeight))

            CheckBoxSelectAll = New CheckBox
            With CheckBoxSelectAll
                .Text = ""
                .Padding = CheckBoxPadding
                AddHandler .CheckedChanged, AddressOf CheckBoxSelectAll_CheckedChanged
            End With

            LabelDomainName = New Label
            With LabelDomainName
                .Text = "Domain Name"
                .Padding = LabelPadding
                .BackColor = TransparentColor
            End With

            LabelGetIPv4Address = New Label
            With LabelGetIPv4Address
                .Text = "Get IPv4"
                .Padding = LabelPadding
                .BackColor = TransparentColor
            End With

            LabelGetIPv6Address = New Label
            With LabelGetIPv6Address
                .Text = "Get IPv6"
                .Padding = LabelPadding
                .BackColor = TransparentColor
            End With

            LabelEnable = New Label
            With LabelEnable
                .Text = "Enable"
                .Padding = LabelPadding
                .BackColor = TransparentColor
            End With

            .Controls.Add(CheckBoxSelectAll, 0, 0)
            .Controls.Add(LabelEnable, 1, 0)
            .Controls.Add(LabelDomainName, 2, 0)
            .Controls.Add(LabelGetIPv4Address, 3, 0)
            .Controls.Add(LabelGetIPv6Address, 4, 0)

            AddHandler .Paint, AddressOf Me_Paint
        End With
    End Sub

    Private Sub CheckBoxSelectAll_CheckedChanged(sender As Object, e As EventArgs)
        RaiseEvent SelectAllCheckedChanged(sender, e)
    End Sub

    Private Sub Me_Paint(sender As Object, e As PaintEventArgs)
        Dim Graphics As Graphics = Me.CreateGraphics
        Dim Pen As New Pen(Color.LightGray, 1)
        Dim LeftPadding As Integer = 0

        With Me
            Graphics.DrawLine(Pen, New Point(0, .Height - 1), New Point(.Width, .Height - 1))
            ' Graphics.DrawLine(Pen, New Point(0, -1), New Point(.Width, -1))

            For i As Integer = 0 To .ColumnStyles.Count - 2
                LeftPadding += .GetColumnWidths(i)
                Graphics.DrawLine(Pen, New Point(LeftPadding, .Height - 1), New Point(LeftPadding, 0))
            Next
        End With
    End Sub
End Class
