Public Class FormTest
    Private ColumnStyleList As ArrayList
    Private TableLayoutPanelList As TableLayoutPanel

    Public Sub New()

        InitializeComponent()

        ColumnStyleList = New ArrayList
        With ColumnStyleList
            .Add(New ColumnStyle(SizeType.Percent, 1))
            .Add(New ColumnStyle(SizeType.Percent, 2))
            .Add(New ColumnStyle(SizeType.Percent, 5))
            .Add(New ColumnStyle(SizeType.Percent, 2))
            .Add(New ColumnStyle(SizeType.Percent, 2))
        End With

        TableLayoutPanelList = New TableLayoutPanel
        With TableLayoutPanelList
            .Dock = DockStyle.Fill
            .ColumnStyles.Add(New ColumnStyle(SizeType.AutoSize))

            .RowStyles.Add(New RowStyle(SizeType.AutoSize))
            .Controls.Add(New DomainNameItem(ColumnStyleList), 0, 0)
            .Parent = Me
        End With


    End Sub

End Class