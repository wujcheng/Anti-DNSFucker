Imports System.Text.RegularExpressions

Public Class Hosts
    Dim Items As ArrayList
    Dim HostsFilePath As String = Environment.SystemDirectory & "\drivers\etc\hosts"

    Public Sub New()
        Items = New ArrayList

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
            If Item.Trim = "" Then
                Continue For
            End If

            If Item.IndexOf("#") = 0 Then
                Continue For
            End If

            Dim IPAddress As String = Item.Remove(Item.IndexOf(" ")).Trim
            Dim _DomainName As String = Item.Remove(0, Item.IndexOf(" ") + 1).Trim

            If Not Regex.IsMatch(IPAddress) Then
                Continue For
            End If

            If Not DomainName.Trim.ToLower = _DomainName.Trim.ToLower Then
                Continue For
            End If

            Return Items.IndexOf(Item)
        Next
        Return -1
    End Function

    Public Function Add(ByVal DomainName As String, ByVal IPAddress As String) As Boolean
        Dim Version As Integer = 0
        If Regexes.IPv4Address.IsMatch(IPAddress) Then
            Version = 4
        ElseIf Regexes.IPv6Address.IsMatch(IPAddress) Then
            Version = 6
        Else
            Return False
        End If

        Dim Index As Integer = FindIPAddress(DomainName, Version)
        If FindIPAddress(DomainName, Version) >= 0 Then
            Items(Index) = IPAddress.Trim & " " & DomainName.Trim
        Else
            Items.Add(IPAddress.Trim & " " & DomainName.Trim)
        End If
        Return True
    End Function

    Public Sub Remove(ByVal DomainName As String)
        Dim RemoveItemList As New ArrayList

        For Each Item As String In Items
            Item = Item.Trim
            If Item.Trim = "" Then
                Continue For
            End If

            If Item.IndexOf("#") = 0 Then
                Continue For
            End If

            Dim _DomainName As String = Item.Remove(0, Item.IndexOf(" ") + 1).Trim

            If DomainName.Trim.ToLower = _DomainName.Trim.ToLower Then
                RemoveItemList.Add(Item)
            End If
        Next

        For Each Item As String In RemoveItemList
            Items.Remove(Item)
        Next
    End Sub

    Public Sub Save()
        Dim Writer As New IO.StreamWriter(HostsFilePath, False, System.Text.Encoding.Default)
        For Each Line As String In Items
            Writer.WriteLine(Line)
        Next
        Writer.Close()
    End Sub

    Public Sub Save(ByVal HostsFilePath As String)
        Dim Writer As New IO.StreamWriter(HostsFilePath, False, System.Text.Encoding.Default)
        For Each Line As String In Items
            Writer.WriteLine(Line)
        Next
        Writer.Close()
    End Sub
End Class
