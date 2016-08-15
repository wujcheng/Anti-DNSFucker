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
        End With

    End Sub

    Public Property WaterMarkText As String
        Get
            Return TextBox.Tag
        End Get
        Set(value As String)
            TextBox.Tag = value
            SetWatermark(value)
        End Set
    End Property

    Private Sub SetWaterMark(waterMarkText As String)
        SendMessage(TextBox.Handle, EM_SETCUEBANNER, 0, waterMarkText)
    End Sub
End Class
