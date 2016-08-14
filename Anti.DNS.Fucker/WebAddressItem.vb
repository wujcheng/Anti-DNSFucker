Public Class WebAddressItem
    Public DomainName As String = ""
    Public Behaviour As Behaviour = Behaviour.GetAllAddresses

    Public Sub New()
    End Sub

    Public Sub New(ByVal Behaviour As Behaviour, ByVal DomainName As String)
        With Me
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




