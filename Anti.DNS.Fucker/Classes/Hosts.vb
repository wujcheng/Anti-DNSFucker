Imports System.Text.RegularExpressions

Public Class Hosts
    Dim Items As ArrayList

    Public Sub New()
        Items = New ArrayList

        Dim HostsFilePath As String = Environment.SystemDirectory & "\drivers\etc\hosts"
        Dim Reader As New IO.StreamReader(HostsFilePath, System.Text.Encoding.Default)

        While Not Reader.EndOfStream
            Items.Add(Reader.ReadLine)
        End While

        Reader.Close()
    End Sub

    Public Function FindIPAddress(ByVal DomainName As String, ByVal Version As Integer) As Integer
        Dim Regex As Regex = If(Version = 4, Regexes.IPv4Address, Regexes.IPv6Address)

        For Each Item As String In Items
            Item = Item.Trim
            If Item.IndexOf("#") = 0 Then
                Continue For
            End If

            Dim SplitItem() As String = Item.Split(" ")
            If Not SplitItem.Length = 2 Then
                Continue For
            End If

            If Not Regex.IsMatch(SplitItem(0)) Then
                Continue For
            End If

            If Not DomainName.Trim.ToLower = SplitItem(1).Trim.ToLower Then
                Continue For
            End If

            Return Items.IndexOf(Item)
        Next
        Return -1
    End Function
End Class
