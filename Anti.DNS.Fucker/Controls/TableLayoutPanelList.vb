Public Class TableLayoutPanelList
    Inherits System.Windows.Forms.TableLayoutPanel

    Public SelectedState As CheckState = CheckState.Indeterminate
    Public Event SelectedChanged(sender As Object, e As EventArgs)

    Private ColumnStyleList As ArrayList
    Public ConfigurationPath As String

    Public ReadOnly Property AllResolved As Boolean
        Get
            For Each DomainNameItem As DomainNameItem In Me.Controls
                If Not DomainNameItem.IsResolved Then
                    Return False
                End If
            Next
            Return True
        End Get
    End Property

    Public Sub New(ByVal ColumnStyleList As ArrayList)
        With Me
            .ColumnStyles.Add(New ColumnStyle(SizeType.AutoSize))
            .HorizontalScroll.Maximum = 0
            .AutoScroll = False
            .HorizontalScroll.Visible = False
            .HorizontalScroll.Enabled = False
            .VerticalScroll.Enabled = True
            '.VerticalScroll.Visible = True
            .AutoScroll = True
            .Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Top Or AnchorStyles.Right
            .ColumnStyleList = ColumnStyleList

            AddHandler .ClientSizeChanged, AddressOf Me_ClientSizeChanged
        End With
    End Sub

    Private Sub Me_ClientSizeChanged(sender As Object, e As EventArgs)
        With Me
            If .VerticalScroll.Visible = True Then
                .Padding = New Padding(0)
            Else
                .Padding = New Padding(0, 0, SystemInformation.VerticalScrollBarWidth, 0)
            End If
        End With
    End Sub

    Public Function Fill(ByVal ConfigurationPath As String) As Boolean
        Dim Configuration As New Configuration
        If Not Configuration.Load(ConfigurationPath) Then
            Return False
        End If

        Dim DataTable As New DataTable
        If Not Configuration.GetConfig(TableNames.DomainNameItemList, DataTable) Then
            Return False
        End If

        With Me
            .Controls.Clear()
            .RowStyles.Clear()
            .RowCount = 0

            For Each Row As DataRow In DataTable.Rows
                Dim DomainNameItem As New DomainNameItem(ColumnStyleList)
                For Each Column As DataColumn In DataTable.Columns
                    For Each Control As Control In DomainNameItem.Controls
                        If Not Control.Name = Column.Caption Then
                            Continue For
                        End If

                        If TypeOf Control Is CheckBox Then
                            CType(Control, CheckBox).Checked = Row(Control.Name)
                        ElseIf TypeOf Control Is TextBoxWithWaterMark Then
                            CType(Control, TextBoxWithWaterMark).Text = Row(Control.Name)
                        End If
                    Next
                Next
                Me.AddItem(DomainNameItem)
            Next
        End With

        Return True
    End Function

    Public Function SaveConfiguration() As Boolean
        Return SaveConfiguration(ConfigurationPath)
    End Function

    Public Function SaveConfiguration(ByVal ConfigurationPath As String) As Boolean
        Dim Configuration As New Configuration
        Dim DataTable As New DataTable
        DataTable.TableName = TableNames.DomainNameItemList

        If Not Me.RowCount = 0 Then
            Dim FirstItem As DomainNameItem = CType(Me.GetControlFromPosition(0, 0), DomainNameItem)
            With DataTable
                For Each Control As Control In FirstItem.Controls
                    If Control.Name.Trim = "" Then
                        Continue For
                    End If
                    .Columns.Add(Control.Name)
                Next

                For i As Integer = 0 To Me.RowCount - 1
                    Dim Row As DataRow = .NewRow

                    For Each Control As Control In Me.GetControlFromPosition(0, i).Controls
                        If Control.Name.Trim = "" Then
                            Continue For
                        End If

                        Row(Control.Name) = CType(Me.GetControlFromPosition(0, i), DomainNameItem).GetValueByName(Control.Name)
                    Next
                    .Rows.Add(Row)
                Next
            End With
        End If

        Configuration.SetConfig(DataTable)
        Configuration.SaveAs(ConfigurationPath)
        Return True
    End Function

    Public Sub AddItem()
        Dim DomainNameItem As New DomainNameItem(ColumnStyleList)
        With DomainNameItem
            .Dock = DockStyle.Fill
            AddHandler .SelectCheckedChanged, AddressOf DomainNameItem_SelectCheckedChanged
        End With

        With Me
            .RowStyles.Add(New RowStyle(SizeType.AutoSize))
            .Controls.Add(DomainNameItem)
            .RowCount += 1
            .ScrollControlIntoView(DomainNameItem)
        End With

        DomainNameItem_SelectCheckedChanged(Nothing, Nothing)
    End Sub

    Public Sub AddItem(ByVal DomainNameItem As DomainNameItem)
        With DomainNameItem
            .Dock = DockStyle.Fill
            AddHandler .SelectCheckedChanged, AddressOf DomainNameItem_SelectCheckedChanged
        End With

        With Me
            .RowStyles.Add(New RowStyle(SizeType.AutoSize))
            .Controls.Add(DomainNameItem)
            .RowCount += 1
            .ScrollControlIntoView(DomainNameItem)
        End With

        DomainNameItem_SelectCheckedChanged(Nothing, Nothing)
    End Sub

    Public Sub RemoveSelectedItems()
        Dim RemovedItemList As ArrayList = New ArrayList

        For i As Integer = 0 To Me.RowCount - 1
            With CType(Me.GetControlFromPosition(0, i), DomainNameItem)
                If Not .Selected Then
                    Continue For
                End If

                RemovedItemList.Add(Me.GetControlFromPosition(0, i))
            End With
        Next

        For Each RemovedItem As DomainNameItem In RemovedItemList
            Me.Controls.Remove(RemovedItem)
            Me.RowStyles.RemoveAt(Me.RowStyles.Count - 1)
            Me.RowCount -= 1
        Next

        Dim Position As Point = Me.AutoScrollPosition
        Me.AutoScroll = False
        Me.AutoScroll = True
        Me.AutoScrollPosition = New Point(-Position.X, -Position.Y)

        DomainNameItem_SelectCheckedChanged(Nothing, Nothing)
    End Sub

    Public Sub SelectAllItems(Optional Selected As Boolean = True)
        For i As Integer = 0 To Me.RowCount - 1
            With CType(Me.GetControlFromPosition(0, i), DomainNameItem)
                .CheckBoxSelectEventEnabled = False
                .Selected = Selected
                .CheckBoxSelectEventEnabled = True
            End With
        Next
    End Sub

    Public Sub DomainNameItem_SelectCheckedChanged(sender As Object, e As EventArgs)
        Dim SelectedCount As Integer = 0

        For i As Integer = 0 To Me.RowCount - 1
            With CType(Me.GetControlFromPosition(0, i), DomainNameItem)
                SelectedCount += If(.Selected, 1, 0)
            End With
        Next

        Select Case SelectedCount
            Case 0
                SelectedState = CheckState.Unchecked
            Case Me.RowCount
                SelectedState = CheckState.Checked
            Case Else
                SelectedState = CheckState.Indeterminate
        End Select

        RaiseEvent SelectedChanged(sender, e)
    End Sub

    Public Sub EnableSelectedItems(ByVal Enabled As Boolean)
        For Each DomainNameItem As DomainNameItem In Me.Controls
            If DomainNameItem.Selected Then
                DomainNameItem.Enabled = Enabled
            End If
        Next
    End Sub

    Public Sub GetIPvXAddressSelectedItems(ByVal Enabled As Boolean, ByVal Version As Integer)
        For Each DomainNameItem As DomainNameItem In Me.Controls
            If DomainNameItem.Selected Then
                If Version = 4 Then
                    DomainNameItem.GetIPv4Address = Enabled
                Else
                    DomainNameItem.GetIPv6Address = Enabled
                End If
            End If
        Next
    End Sub
End Class
