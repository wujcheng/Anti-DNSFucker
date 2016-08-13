Public Class WebAddressItem
    Public DomainName As String = ""
    Public Behaviour As Behaviour = Behaviour.GetAllAddresses

    Public Sub New()
    End Sub

    Public Sub New(ByVal Code As String)
        With Me
            .Behaviour = Val(Code.Remove(Code.IndexOf("#")))
            .DomainName = Code.Remove(0, Code.IndexOf("#") + 1)
        End With
    End Sub
End Class

Public Enum Behaviour
    GetIPv6Address
    GetIPv4Address
    GetAllAddresses
    GetNoneAddress
End Enum




