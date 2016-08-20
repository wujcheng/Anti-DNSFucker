Imports System.Text.RegularExpressions

Public Class IP
    Public IPv4Address As String
    Public IPv6Address As String

    Private WebBrowser As WebBrowser
    Private IsResolving As Boolean

    Private IPv4Regex As New Regex("\b(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b")
    Private IPv6Regex As New Regex("(([0-9a-fA-F]{1,4}:){7,7}[0-9a-fA-F]{1,4}|([0-9a-fA-F]{1,4}:){1,7}:|([0-9a-fA-F]{1,4}:){1,6}:[0-9a-fA-F]{1,4}|([0-9a-fA-F]{1,4}:){1,5}(:[0-9a-fA-F]{1,4}){1,2}|([0-9a-fA-F]{1,4}:){1,4}(:[0-9a-fA-F]{1,4}){1,3}|([0-9a-fA-F]{1,4}:){1,3}(:[0-9a-fA-F]{1,4}){1,4}|([0-9a-fA-F]{1,4}:){1,2}(:[0-9a-fA-F]{1,4}){1,5}|[0-9a-fA-F]{1,4}:((:[0-9a-fA-F]{1,4}){1,6})|:((:[0-9a-fA-F]{1,4}){1,7}|:)|fe80:(:[0-9a-fA-F]{0,4}){0,4}%[0-9a-zA-Z]{1,}|::(ffff(:0{1,4}){0,1}:){0,1}((25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9])\.){3,3}(25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9])|([0-9a-fA-F]{1,4}:){1,4}:((25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9])\.){3,3}(25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9]))")

    Public Sub New()
        With Me
            .IPv4Address = ""
            .IPv6Address = ""
            .WebBrowser = New WebBrowser
            AddHandler .WebBrowser.DocumentCompleted, AddressOf WebBrowser_DocumentCompleted
        End With
    End Sub

    Public Sub Resolve(ByVal DomainName As String)
        IPv4Address = ""
        IPv6Address = ""

        WebBrowser.Url = New Uri("http://geoip.neu.edu.cn/?ip=" & DomainName)
        IsResolving = True
        While IsResolving
            Application.DoEvents()
        End While
        MsgBox(IPv4Address & " | " & IPv6Address)
    End Sub

    Private Sub WebBrowser_DocumentCompleted()
        For Each HtmlElement As HtmlElement In WebBrowser.Document.GetElementsByTagName("span")
            If IPv4Regex.IsMatch(HtmlElement.InnerText) Then
                IPv4Address = HtmlElement.InnerText
                Continue For
            End If

            If IPv6Regex.IsMatch(HtmlElement.InnerText) Then
                IPv6Address = HtmlElement.InnerText
            End If
        Next

        IsResolving = False
    End Sub
End Class
