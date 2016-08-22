Imports System.Runtime.InteropServices

Public Class TextBoxWithWaterMark
    Inherits System.Windows.Forms.TextBox

    Private Const ECM_FIRST As UInteger = &H1500
    Private Const EM_SETCUEBANNER As UInteger = ECM_FIRST + 1

    <DllImport("user32.dll", CharSet:=CharSet.Auto, SetLastError:=False)>
    Private Shared Function SendMessage(hWnd As IntPtr, Msg As UInteger, wParam As UInteger, <MarshalAs(UnmanagedType.LPWStr)> lParam As String) As IntPtr
    End Function

    Public Property WaterMarkText As String
        Get
            Return Me.Tag
        End Get
        Set(value As String)
            Me.Tag = value
            SetWaterMark(value)
        End Set
    End Property

    Private Sub SetWaterMark(waterMarkText As String)
        SendMessage(Me.Handle, EM_SETCUEBANNER, 0, waterMarkText)
    End Sub

    Public Sub New()
        'Me.BorderStyle = BorderStyle.FixedSingle
    End Sub
End Class
