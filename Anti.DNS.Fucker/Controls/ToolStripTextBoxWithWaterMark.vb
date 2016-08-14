Imports System.Runtime.InteropServices

Public Class ToolStripTextBoxWithWaterMark
    Inherits System.Windows.Forms.ToolStripTextBox

    Shadows TextBox As TextBox


    Private Const ECM_FIRST As UInteger = &H1500
    Private Const EM_SETCUEBANNER As UInteger = ECM_FIRST + 1

    <DllImport("user32.dll", CharSet:=CharSet.Auto, SetLastError:=False)>
    Private Shared Function SendMessage(hWnd As IntPtr, Msg As UInteger, wParam As UInteger, <MarshalAs(UnmanagedType.LPWStr)> lParam As String) As IntPtr
    End Function

    Public Sub New()
        TextBox = Me.Control

        With TextBox
            .BorderStyle = BorderStyle.FixedSingle
            'AddHandler .TextChanged, AddressOf TextBox_TextChanged
            'AddHandler .Leave, AddressOf TextBox_TextChanged
        End With

        SetWatermark("Domain Name")
    End Sub

    Private Sub SetWatermark(watermarkText As String)
        SendMessage(TextBox.Handle, EM_SETCUEBANNER, 0, watermarkText)
    End Sub


    'Private Sub TextBox_TextChanged()
    '    If TextBox.Text = "" Then
    '        Dim Graphics As Graphics = TextBox.CreateGraphics
    '        Graphics.DrawString("23333", (New ToolStripMenuItem).Font, SystemBrushes.GrayText, New Point(0, 2))
    '    End If
    'End Sub


End Class
