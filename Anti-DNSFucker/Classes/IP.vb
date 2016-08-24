Imports System.Text.RegularExpressions

Public Class IP
    Public IPv4Address As String
    Public IPv6Address As String
    Private Const TimeOut As Integer = 2000
    Private Const Flag As String = "您查询的IP地址"

    Public Sub New()
        With Me
            .IPv4Address = ""
            .IPv6Address = ""
        End With
    End Sub

    Public Sub ResolveByNEU(ByVal DomainName As String)
        IPv4Address = ""
        IPv6Address = ""

        Dim Request As System.Net.HttpWebRequest = System.Net.HttpWebRequest.Create("http://geoip.neu.edu.cn/?ip=" & DomainName)
        Request.Timeout = TimeOut

        Dim HtmlCode As String = ""
        Try
            HtmlCode = (New System.IO.StreamReader(Request.GetResponse().GetResponseStream())).ReadToEnd
        Catch ex As Exception
            Exit Sub
        End Try

        If Not HtmlCode.Contains(Flag) Then
            Exit Sub
        End If

        HtmlCode = HtmlCode.Remove(0, HtmlCode.IndexOf(Flag) + Flag.Length).Replace(" ", "")

        For Each IPAddress As String In GetIPAddresses(HtmlCode)
            If Regexes.IPv4Address.IsMatch(IPAddress) Then
                IPv4Address = IPAddress
            ElseIf Regexes.IPv6Address.IsMatch(IPAddress) Then
                IPv6Address = IPAddress
            End If
        Next

    End Sub

    Private Function GetIPAddresses(ByVal HtmlCode As String) As ArrayList
        Dim IPAddressList As New ArrayList

        Dim IPv4AddressContainer As New Regex(">" & Regexes.IPv4Address.ToString & "<")
        Dim IPv6AddressContainer As New Regex(">" & Regexes.IPv6Address.ToString & "<")

        For Each IPAddress As String In JoinMatches(IPv4AddressContainer.Matches(HtmlCode), IPv6AddressContainer.Matches(HtmlCode))
            IPAddress = IPAddress.Trim("<").Trim(">").Trim()

            If Not System.Net.IPAddress.TryParse(IPAddress, Nothing) Then
                Continue For
            End If

            Dim ExistIPAddress As Boolean = False
            For Each Item As String In IPAddressList
                If Item = IPAddress Then
                    ExistIPAddress = True
                    Exit For
                End If
            Next

            If Not ExistIPAddress Then
                IPAddressList.Add(IPAddress)
            End If
        Next

        Return IPAddressList
    End Function

    Private Function JoinMatches(ByVal FirstMatches As System.Text.RegularExpressions.MatchCollection, ByVal SecondMatches As System.Text.RegularExpressions.MatchCollection) As ArrayList
        Dim Matches As New ArrayList
        For i As Integer = 0 To FirstMatches.Count - 1
            Matches.Add(FirstMatches.Item(i).Value)
        Next

        For i As Integer = 0 To SecondMatches.Count - 1
            Matches.Add(SecondMatches.Item(i).Value)
        Next
        Return Matches
    End Function
End Class
