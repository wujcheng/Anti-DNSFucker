Public Class WebAddressItem
    Public Enable As Boolean = True
    Public DomainName As String = ""
    Public Behaviour As Behaviour = Behaviour.GetAllAddresses

    Public Sub New()
    End Sub

    Public Sub New(ByVal Enable As Boolean, ByVal Behaviour As Behaviour, ByVal DomainName As String)
        With Me
            .Enable = Enable
            .Behaviour = Behaviour
            .DomainName = DomainName
        End With
    End Sub
End Class

Public Enum Behaviour
    GetIPv6Address
    GetIPv4Address
    GetAllAddresses
    GetNoneAddress
End Enum




