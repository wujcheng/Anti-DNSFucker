''' <summary>
''' This class is the container of the DomainNameItem.
''' </summary>
Public Class TableLayoutPanelList
    ' It inherits from TableLayoutPanel.
    Inherits System.Windows.Forms.TableLayoutPanel

    ''' <summary>
    ''' This property presents the check state of all CheckBoxes.
    ''' If all ChechBoxes are selected, this property is assigned CheckState.Checked
    ''' If all CheckBoxes are unselected, this property is assigned CheckState.Unchecked.
    ''' If some CheckBoxes are selected, and others are unselected, this property is assigned heckState.Indeterminate.
    ''' </summary>
    Public SelectedState As CheckState = CheckState.Indeterminate
    ''' <summary>
    ''' This event is triggered when CheckBox of any DomainNameItem is changed.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Public Event SelectedChanged(sender As Object, e As EventArgs)
    ''' <summary>
    ''' This property represent the path of the configuration file.
    ''' </summary>
    Public ConfigurationPath As String
    ''' <summary>
    ''' This property is readonly.
    ''' It indicates whether all domain name is resolved.
    ''' </summary>
    ''' <returns></returns>
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

    ' This array list is used to store the column styles of this item.
    Private ColumnStyleList As ArrayList

    ''' <summary>
    ''' This is the constructor.
    ''' </summary>
    ''' <param name="ColumnStyleList"></param>
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

    ''' <summary>
    ''' This sub is triggered when the size of TableLayoutPanelList is changed.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Me_ClientSizeChanged(sender As Object, e As EventArgs)
        With Me
            If .VerticalScroll.Visible = True Then
                .Padding = New Padding(0)
            Else
                .Padding = New Padding(0, 0, SystemInformation.VerticalScrollBarWidth, 0)
            End If
        End With
    End Sub

    ''' <summary>
    ''' This function is used to fill the TableLayoutPanelList according to the configuration file.
    ''' </summary>
    ''' <param name="ConfigurationPath"></param>
    ''' <returns></returns>
    Public Function Fill(ByVal ConfigurationPath As String) As Boolean
        ' Create and load the configuration.
        Dim Configuration As New Configuration
        If Not Configuration.Load(ConfigurationPath) Then
            ' If there is something wrong, exit this sub.
            Return False
        End If

        ' Create and load the data table.
        Dim DataTable As New DataTable
        If Not Configuration.GetConfig(TableNames.DomainNameItemList, DataTable) Then
            ' If there is something wrong, exit this sub.
            Return False
        End If


        With Me
            ' Clear all elements of the TableLayoutPanelList.
            .Controls.Clear()
            .RowStyles.Clear()
            .RowCount = 0

            ' For each row in the data table,
            For Each Row As DataRow In DataTable.Rows
                ' create a DomainNameItem,
                Dim DomainNameItem As New DomainNameItem(ColumnStyleList)
                ' set the attributes of the DomainNameItem.
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
                ' Add the DomainNameItem into the TableLayoutPanelList.
                Me.AddItem(DomainNameItem)
            Next
        End With

        Return True
    End Function

    ''' <summary>
    ''' This function is used to save the TablyLayoutPanelList into the configuration.
    ''' </summary>
    ''' <returns></returns>
    Public Function SaveConfiguration() As Boolean
        Return SaveConfiguration(ConfigurationPath)
    End Function

    ''' <summary>
    ''' This function is used to save the TablyLayoutPanelList into the configuration.
    ''' Comparing with the above function, this function has a parameter, to which the configuration is saved.
    ''' </summary>
    ''' <param name="ConfigurationPath"></param>
    ''' <returns></returns>
    Public Function SaveConfiguration(ByVal ConfigurationPath As String) As Boolean
        ' Create a configuration.
        Dim Configuration As New Configuration
        ' Create an empty data table.
        Dim DataTable As New DataTable
        ' Set the name of the data table.
        DataTable.TableName = TableNames.DomainNameItemList

        ' If the TablyLayoutPanelList is not empty, fill the data table.
        If Not Me.RowCount = 0 Then
            ' Get the first item of the TablyLayoutPanelList.
            Dim FirstItem As DomainNameItem = CType(Me.GetControlFromPosition(0, 0), DomainNameItem)
            With DataTable
                ' Set the columns of the new data table.
                For Each Control As Control In FirstItem.Controls
                    If Control.Name.Trim = "" Then
                        Continue For
                    End If
                    .Columns.Add(Control.Name)
                Next

                ' For each DomainNameItem, read its values, and fill them into the data table.
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

        ' Add the data table into the configuration.
        Configuration.SetConfig(DataTable)
        ' Save the configuration into the file.
        Configuration.SaveAs(ConfigurationPath)
        Return True
    End Function

    ''' <summary>
    ''' This function is used to refresh the TableLayoutPanelList.
    ''' </summary>
    Public Overrides Sub Refresh()
        ' Save the configuration into a temporary file.
        Dim TempPath As String = ".\TempConfiguration.xml"
        Me.SaveConfiguration(TempPath)

        ' Save the position of the scroll bar.
        Dim Position As Point = Me.AutoScrollPosition

        ' Reload the configuration.
        Me.Fill(TempPath)

        ' Set the position of the scroll bar.
        Me.AutoScrollPosition = New Point(-Position.X, -Position.Y)

        ' Delete the temporary file.
        My.Computer.FileSystem.DeleteFile(TempPath)
    End Sub

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
