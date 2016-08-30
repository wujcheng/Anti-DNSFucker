''' <summary>
''' This class is the head of TableLayoutPanelList.
''' </summary>
Public Class TableLayoutPanelHead
    ' It inherits from TableLayoutPanel.
    Inherits System.Windows.Forms.TableLayoutPanel

    ' This CheckBox is used to select/unselect all the CheckBoxes in the TableLayoutPanelList.
    Private CheckBoxSelectAll As CheckBox
    ' This is the head of "Domain Name".
    Private LabelDomainName As Label
    ' This is the head of "Get IPb6".
    Private LabelGetIPv6Address As Label
    ' This is the head of "Get IPv4".
    Private LabelGetIPv4Address As Label
    ' This is the head of "Enable".
    Private LabelEnable As Label

    ' This constant is the height if this head.
    Private Const ItemHeight As Integer = 27
    ' This is the padding of the CheckBox.
    Private CheckBoxPadding As Padding = New Padding(0, 2, 0, 0)
    ' This is the padding of the Label.
    Private LabelPadding As Padding = New Padding(0, 7, 0, 0)
    ' This is the transparent color.
    Private TransparentColor As Color = Color.FromArgb(0, Color.Black)

    ''' <summary>
    ''' This event will triggered when the check state of CheckBoxSelectAll is changed.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Public Event SelectAllCheckedChanged(sender As Object, e As EventArgs)

    ''' <summary>
    ''' This property represents the check state of the CheckBoxSelectAll.
    ''' </summary>
    ''' <returns></returns>
    Public Property SelectAllChecked As CheckState
        Get
            Return CheckBoxSelectAll.CheckState
        End Get
        Set(value As CheckState)
            CheckBoxSelectAll.CheckState = value
        End Set
    End Property

    ''' <summary>
    ''' This property is writeonly.
    ''' When this property is assigned True, then add handler CheckBoxSelectAll.CheckedChanged,
    ''' else, remove handler CheckBoxSelectAll.CheckedChanged.
    ''' </summary>
    Public WriteOnly Property CheckBoxSelectAllEventEnabled As Boolean
        Set(value As Boolean)
            If value Then
                AddHandler CheckBoxSelectAll.CheckedChanged, AddressOf CheckBoxSelectAll_CheckedChanged
            Else
                RemoveHandler CheckBoxSelectAll.CheckedChanged, AddressOf CheckBoxSelectAll_CheckedChanged
            End If
        End Set
    End Property

    ''' <summary>
    ''' This is the constructor.
    ''' </summary>
    ''' <param name="ColumnStyleList"></param>
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

    ''' <summary>
    ''' This sub is triggered when the check state of CheckBoxSelectAll is changed.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub CheckBoxSelectAll_CheckedChanged(sender As Object, e As EventArgs)
        RaiseEvent SelectAllCheckedChanged(sender, e)
    End Sub

    ''' <summary>
    ''' This sub is triggered when this head is painted.
    ''' It is used to draw borders.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
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
