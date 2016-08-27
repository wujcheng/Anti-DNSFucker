''' <summary>
''' This class inherits CheckBox, and it is an advanced CheckBox.
''' Comparing with the CheckBox, CheckBoxAdvanced has a public property: Broken, which is a Boolean.
''' If the property Broken is True, the properties Enabled and Checked cannot be operated.
''' </summary>
Public Class CheckBoxAdvanced
    Inherits System.Windows.Forms.CheckBox

    ''' <summary>
    ''' This is the newly added property: Broken. 
    ''' The value of this proporty is save in the IsBroken which is a Boolean, too.
    ''' If the property Broken is assigned True, the properties Enabled and Checked is assigned False.
    ''' </summary>
    ''' <returns></returns>
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

    ''' <summary>
    ''' This property is used to save the value of the property Broken.
    ''' </summary>
    Private IsBroken As Boolean

    ''' <summary>
    ''' This is property is used to override the property Enabled.
    ''' If this property is read, there is nothing different.
    ''' But if this property is assigned, the property Broken is checked first.
    ''' If the property Broken is True, the property Enabled does not work.
    ''' </summary>
    ''' <returns></returns>
    Public Shadows Property Enabled As Boolean
        Get
            Return MyBase.Enabled
        End Get
        Set(value As Boolean)
            MyBase.Enabled = If(Broken, False, value)
        End Set
    End Property

    ''' <summary>
    ''' This is property is used to override the property Checked.
    ''' If this property is read, there is nothing different.
    ''' But if this property is assigned, the property Broken is checked first.
    ''' If the property Broken is True, the property Checked does not work.
    ''' </summary>
    ''' <returns></returns>
    Public Shadows Property Checked As Boolean
        Get
            Return MyBase.Checked
        End Get
        Set(value As Boolean)
            MyBase.Checked = If(Broken, False, value)
        End Set
    End Property

    ''' <summary>
    ''' This is the constructor.
    ''' </summary>
    Public Sub New()
        Broken = False
    End Sub
End Class
