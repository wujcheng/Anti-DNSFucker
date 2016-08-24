Public Class FormMain
    Public Sub New()
        With Me
            .Size = New Size(0, 0)
            .Opacity = 0
            .ShowInTaskbar = False

            ' Do something according to the arguments.
            For Each Argument In Environment.GetCommandLineArgs()
                Select Case Argument
                    Case "MoveHostsFile"
                        MoveHostsFile()
                        Exit For
                End Select
            Next
        End With
        End
    End Sub

    Private Sub MoveHostsFile()
        Dim HostsFilePath As String = Environment.SystemDirectory & "\drivers\etc\hosts"
        Dim TempHostsFilePath As String = ".\hosts"

        If Not My.Computer.FileSystem.FileExists(TempHostsFilePath) Then
            Exit Sub
        End If

        My.Computer.FileSystem.MoveFile(TempHostsFilePath, HostsFilePath, True)
    End Sub
End Class
