''' <summary>
''' This form is invisible.
''' This form cannot run without parameter. If there is no parameter, the form will be disposed.
''' There is only one parameter of this form. In other words, this form is used to do only one thing:
''' move the hosts file to "X:\Windows\System32\drivers\etc\hosts".
''' </summary>
Public Class FormMain
    ''' <summary>
    ''' This is the constructor.
    ''' </summary>
    Public Sub New()
        With Me
            ' The following three lines are used to hide the main form.
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

    ''' <summary>
    ''' This sub is used to move the '.\hosts' file to "X:\Windows\System32\drivers\etc\hosts".
    ''' </summary>
    Private Sub MoveHostsFile()
        ' The path of the source file.
        Dim HostsFilePath As String = Environment.SystemDirectory & "\drivers\etc\hosts"
        ' The path of the distination file.
        Dim TempHostsFilePath As String = ".\hosts"

        ' If the source file does not exist, exit this sub.
        If Not My.Computer.FileSystem.FileExists(TempHostsFilePath) Then
            Exit Sub
        End If

        ' Move the source file to the distination file.
        My.Computer.FileSystem.MoveFile(TempHostsFilePath, HostsFilePath, True)
    End Sub
End Class
