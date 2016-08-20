Imports System.Text.RegularExpressions

Public Class IP
    Public IPv4Address As String
    Public IPv6Address As String

    Private WebBrowser As WebBrowser
    Private IsResolving As Boolean


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
            If Regexes.IPv4Address.IsMatch(HtmlElement.InnerText) Then
                IPv4Address = HtmlElement.InnerText
                Continue For
            End If

            If Regexes.IPv6Address.IsMatch(HtmlElement.InnerText) Then
                IPv6Address = HtmlElement.InnerText
            End If
        Next

        IsResolving = False
    End Sub
End Class
