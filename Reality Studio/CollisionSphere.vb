Public Class CollisionSphere
    Public Structure CollisionSpheres
        Dim CenterX As Single
        Dim CenterY As Single
        Dim CenterZ As Single
        Dim Radius As Single
    End Structure
    Private noDone As New Boolean
    Private SphereList As List(Of CollisionSpheres) = New List(Of CollisionSpheres)
    Public Property NotYet() As Boolean
        Get
            Return noDone
        End Get
        Set(ByVal value As Boolean)
            noDone = value
        End Set
    End Property
    Public Property Spheres() As List(Of CollisionSpheres)
        Get
            Return SphereList
        End Get
        Set(ByVal value As List(Of CollisionSpheres))
            SphereList = value
        End Set
    End Property
End Class
