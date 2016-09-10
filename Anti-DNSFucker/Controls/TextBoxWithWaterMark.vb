Imports System.Runtime.InteropServices

''' <summary>
''' This class is a TextBox with a water mark.
''' </summary>
Public Class TextBoxWithWaterMark
    Inherits System.Windows.Forms.TextBox

    Private Const ECM_FIRST As UInteger = &H1500
    Private Const EM_SETCUEBANNER As UInteger = ECM_FIRST + 1

    <DllImport("user32.dll", CharSet:=CharSet.Auto, SetLastError:=False)>
    Private Shared Function SendMessage(hWnd As IntPtr, Msg As UInteger, wParam As UInteger, <MarshalAs(UnmanagedType.LPWStr)> lParam As String) As IntPtr
    End Function

    ''' <summary>
    ''' This property is the water mark text of this TextBox.
    ''' It is saved in Tag of this TextBox.
    ''' </summary>
    ''' <returns></returns>
    Public Property WaterMarkText As String
        Get
            Return Me.Tag
        End Get
        Set(value As String)
            Me.Tag = value
            SetWaterMark(value)
        End Set
    End Property

    ''' <summary>
    ''' This sub is used to set the water mark property.
    ''' </summary>
    ''' <param name="waterMarkText"></param>
    Private Sub SetWaterMark(waterMarkText As String)
        SendMessage(Me.Handle, EM_SETCUEBANNER, 0, waterMarkText)
    End Sub
End Class
