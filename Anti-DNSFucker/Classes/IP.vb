Imports System.Text.RegularExpressions

Public Class IP
    Public IPv4Address As String
    Public IPv6Address As String

    Private WebBrowser As WebBrowser
    Private IsResolving As Boolean
    Private Timer As Timer

    Public Sub New()
        With Me
            .IPv4Address = ""
            .IPv6Address = ""
        End With
    End Sub

    Public Sub Resolve(ByVal DomainName As String)
        IPv4Address = ""
        IPv6Address = ""

        Timer = New Timer
        With Timer
            .Interval = 2000
            AddHandler .Tick, AddressOf Timer_Tick
            .Start()
        End With
        WebBrowser = New WebBrowser
        AddHandler WebBrowser.DocumentCompleted, AddressOf WebBrowser_DocumentCompleted

        WebBrowser.Url = New Uri("http://geoip.neu.edu.cn/?ip=" & DomainName)
        IsResolving = True
        While IsResolving
            Application.DoEvents()
        End While

        WebBrowser.Dispose()
    End Sub

    Public Sub Timer_Tick(sender As Object, e As EventArgs)
        IsResolving = False
        Timer.Stop()
        Timer.Dispose()
    End Sub

    Private Sub WebBrowser_DocumentCompleted()
        If WebBrowser.DocumentText.Contains("您的IP地址") Then
            IsResolving = False
            Exit Sub
        End If

        For Each SpanName As String In {"div", "span"}
            For Each HtmlElement As HtmlElement In WebBrowser.Document.GetElementsByTagName(SpanName)
                If HtmlElement.InnerText Is Nothing Then
                    Continue For
                End If

                If Regexes.IPv4Address.IsMatch(HtmlElement.InnerText) Then
                    IPv4Address = HtmlElement.InnerText.Trim
                ElseIf Regexes.IPv6Address.IsMatch(HtmlElement.InnerText) Then
                    IPv6Address = HtmlElement.InnerText.Trim
                End If
            Next
        Next

        IsResolving = False
    End Sub
End Class
