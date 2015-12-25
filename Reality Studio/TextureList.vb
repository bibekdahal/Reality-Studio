Public Class TextureList
    Dim txtName As New List(Of String)
    Dim txtText As New List(Of Byte())
    Public Property TextureText() As List(Of Byte())
        Get
            Return txtText
        End Get
        Set(ByVal value As List(Of Byte()))
            txtText = value
        End Set
    End Property
    Public Property TextureName() As List(Of String)
        Get
            Return txtName
        End Get
        Set(ByVal value As List(Of String))
            txtName = value
        End Set
    End Property
End Class
