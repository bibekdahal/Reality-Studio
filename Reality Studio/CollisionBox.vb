Public Class CollisionBox

    Public Structure CollisionBoxes
        Dim MinX As Single
        Dim MinY As Single
        Dim MinZ As Single
        Dim MaxX As Single
        Dim MaxY As Single
        Dim MaxZ As Single
    End Structure
    Private noDone As New Boolean
    Private BoxList As List(Of CollisionBoxes) = New List(Of CollisionBoxes)
    Public Property NotYet() As Boolean
        Get
            Return noDone
        End Get
        Set(ByVal value As Boolean)
            noDone = value
        End Set
    End Property
    Public Property Boxes() As List(Of CollisionBoxes)
        Get
            Return BoxList
        End Get
        Set(ByVal value As List(Of CollisionBoxes))
            BoxList = value
        End Set
    End Property
End Class
