Public Class CheckBoxAdvanced
    Inherits System.Windows.Forms.CheckBox

    Public Property Broken As Boolean
        Get
            Return IsBroken
        End Get
        Set(value As Boolean)
            IsBroken = value
            If IsBroken Then
                Enabled = False
                Checked = False
            End If
        End Set
    End Property
    Private IsBroken As Boolean

    Public Shadows Property Enabled As Boolean
        Get
            Return MyBase.Enabled
        End Get
        Set(value As Boolean)
            MyBase.Enabled = If(Broken, False, value)
        End Set
    End Property

    Public Shadows Property Checked As Boolean
        Get
            Return MyBase.Checked
        End Get
        Set(value As Boolean)
            MyBase.Checked = If(Broken, False, value)
        End Set
    End Property

    Public Sub New()
        Broken = False
    End Sub
End Class
