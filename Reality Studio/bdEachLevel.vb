Public Class bdEachLevel
    Private AllObjects As List(Of String) = New List(Of String)
    Private InstanceID As List(Of String) = New List(Of String)
    Private AllX As List(Of Single) = New List(Of Single)
    Private AllY As List(Of Single) = New List(Of Single)
    Private AllZ As List(Of Single) = New List(Of Single)
    Private _LevelName As String

    Private All2DObjects As List(Of String) = New List(Of String)
    Private Instance2DID As List(Of String) = New List(Of String)
    Private All2DX As List(Of Single) = New List(Of Single)
    Private All2DY As List(Of Single) = New List(Of Single)

    Private BoxX As New List(Of Single)
    Private BoxY As New List(Of Single)
    Private BoxZ As New List(Of Single)
    Private BoxXAngle As New List(Of Single)
    Private BoxYAngle As New List(Of Single)
    Private BoxZAngle As New List(Of Single)
    Private BoxScale As New List(Of Single)
    Private BoxWidth As New List(Of Single)
    Private BoxDepth As New List(Of Single)
    Private BoxHeight As New List(Of Single)

    Public Property BoxHeights() As List(Of Single)
        Get
            Return BoxHeight
        End Get
        Set(ByVal value As List(Of Single))
            BoxHeight = value
        End Set
    End Property
    Public Property BoxDepths() As List(Of Single)
        Get
            Return BoxDepth
        End Get
        Set(ByVal value As List(Of Single))
            BoxDepth = value
        End Set
    End Property
    Public Property BoxWidths() As List(Of Single)
        Get
            Return BoxWidth
        End Get
        Set(ByVal value As List(Of Single))
            BoxWidth = value
        End Set
    End Property
    Public Property BoxScales() As List(Of Single)
        Get
            Return BoxScale
        End Get
        Set(ByVal value As List(Of Single))
            BoxScale = value
        End Set
    End Property
    Public Property BoxXAngles() As List(Of Single)
        Get
            Return BoxXAngle
        End Get
        Set(ByVal value As List(Of Single))
            BoxXAngle = value
        End Set
    End Property
    Public Property BoxYAngles() As List(Of Single)
        Get
            Return BoxYAngle
        End Get
        Set(ByVal value As List(Of Single))
            BoxYAngle = value
        End Set
    End Property
    Public Property BoxZAngles() As List(Of Single)
        Get
            Return BoxZAngle
        End Get
        Set(ByVal value As List(Of Single))
            BoxZAngle = value
        End Set
    End Property
    Public Property BoxXs() As List(Of Single)
        Get
            Return BoxX
        End Get
        Set(ByVal value As List(Of Single))
            BoxX = value
        End Set
    End Property
    Public Property BoxYs() As List(Of Single)
        Get
            Return BoxY
        End Get
        Set(ByVal value As List(Of Single))
            BoxY = value
        End Set
    End Property
    Public Property BoxZs() As List(Of Single)
        Get
            Return BoxZ
        End Get
        Set(ByVal value As List(Of Single))
            BoxZ = value
        End Set
    End Property

    Public Property ObjectsUsed() As List(Of String)
        Get
            Return AllObjects
        End Get
        Set(ByVal value As List(Of String))
            AllObjects = value
        End Set
    End Property
    Public Property Objects2DUsed() As List(Of String)
        Get
            Return All2DObjects
        End Get
        Set(ByVal value As List(Of String))
            All2DObjects = value
        End Set
    End Property
    Public Property LevelName() As String
        Get
            Return _LevelName
        End Get
        Set(ByVal value As String)
            _LevelName = value
        End Set
    End Property
    Public Property InstancesID() As List(Of String)
        Get
            Return InstanceID
        End Get
        Set(ByVal value As List(Of String))
            InstanceID = value
        End Set
    End Property
    Public Property Xs() As List(Of Single)
        Get
            Return AllX
        End Get
        Set(ByVal value As List(Of Single))
            AllX = value
        End Set
    End Property
    Public Property Ys() As List(Of Single)
        Get
            Return AllY
        End Get
        Set(ByVal value As List(Of Single))
            AllY = value
        End Set
    End Property
    Public Property Instances2DID() As List(Of String)
        Get
            Return Instance2DID
        End Get
        Set(ByVal value As List(Of String))
            Instance2DID = value
        End Set
    End Property
    Public Property X2Ds() As List(Of Single)
        Get
            Return All2DX
        End Get
        Set(ByVal value As List(Of Single))
            All2DX = value
        End Set
    End Property
    Public Property Y2Ds() As List(Of Single)
        Get
            Return All2DY
        End Get
        Set(ByVal value As List(Of Single))
            All2DY = value
        End Set
    End Property
    Public Property Zs() As List(Of Single)
        Get
            Return AllZ
        End Get
        Set(ByVal value As List(Of Single))
            AllZ = value
        End Set
    End Property

End Class
