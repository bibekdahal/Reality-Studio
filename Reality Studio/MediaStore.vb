Imports System
Imports System.IO


Public Class MediaStore
#Region "3D Objects"
    Private _3DObjectName As List(Of String) = New List(Of String)
    Private ObjMesh As List(Of String) = New List(Of String)

    Public Sub Add3DObject(ByVal Name As String, ByVal Mesh As String)
        _3DObjectName.Add(Name)
        ObjMesh.Add(Mesh)
    End Sub
    Public Property Object3DName() As List(Of String)
        Get
            Return _3DObjectName
        End Get
        Set(ByVal value As List(Of String))
            _3DObjectName = value
        End Set
    End Property
    Public Property Object3DMesh() As List(Of String)
        Get
            Return ObjMesh
        End Get
        Set(ByVal value As List(Of String))
            ObjMesh = value
        End Set
    End Property
#End Region

#Region "2D Objects"
    Private _2DObjectName As List(Of String) = New List(Of String)
    Private ObjSpr As List(Of String) = New List(Of String)

    Public Sub Add2DObject(ByVal Name As String, ByVal Sprite As String)
        _2DObjectName.Add(Name)
        ObjSpr.Add(Sprite)
    End Sub
    Public Property Object2DName() As List(Of String)
        Get
            Return _2DObjectName
        End Get
        Set(ByVal value As List(Of String))
            _2DObjectName = value
        End Set
    End Property
    Public Property Object2DSprite() As List(Of String)
        Get
            Return ObjSpr
        End Get
        Set(ByVal value As List(Of String))
            ObjSpr = value
        End Set
    End Property
#End Region

#Region "Meshes"
    Private MeshXOrigin As List(Of Single) = New List(Of Single)
    Private MeshYOrigin As List(Of Single) = New List(Of Single)
    Private MeshZOrigin As List(Of Single) = New List(Of Single)
    Private MeshFile As List(Of Byte()) = New List(Of Byte())
    Private _MeshName As List(Of String) = New List(Of String)
    Private _sphereList As List(Of CollisionSphere) = New List(Of CollisionSphere)
    Private _boxList As New List(Of CollisionBox)
    Private _txtLists As New List(Of TextureList)

    Private _MeshXAngle As New List(Of Single)
    Private _MeshYAngle As New List(Of Single)
    Private _MeshZAngle As New List(Of Single)

    Private _meshScale As New List(Of Single)
    Private _Animated As New List(Of Boolean)

    Public Sub AddMesh(ByVal Name As String, ByVal XOrigin As Single, ByVal YOrigin As Single, ByVal ZOrigin As Single, ByVal FileName As String)
        Try
            Dim infile As New FileStream(FileName, FileMode.Open, FileAccess.Read, FileShare.Read)
            Dim buffer(infile.Length - 1) As Byte
            Dim count As Integer = infile.Read(buffer, 0, buffer.Length)
            If count <> buffer.Length Then
                infile.Close()
                MsgBox("Bad File")
                Return
            End If
            infile.Close()
            MeshFile.Add(buffer)

            MeshXOrigin.Add(XOrigin)
            MeshYOrigin.Add(YOrigin)
            MeshZOrigin.Add(ZOrigin)
            _Animated.Add(False)
            _MeshName.Add(Name)

            _MeshXAngle.Add(0)
            _MeshYAngle.Add(0)
            _MeshZAngle.Add(0)

            _meshScale.Add(1)

            Dim goat As New CollisionSphere
            goat.NotYet = True
            _sphereList.Add(goat)

            Dim gaat As New CollisionBox
            gaat.NotYet = True
            _boxList.Add(gaat)

            _txtLists.Add(New TextureList)
        Catch ex As Exception
            MsgBox("Bad File")
        End Try
    End Sub
    Public Function ChangeMesh(ByVal NewFileName As String) As Byte()
        Try
            Dim infile As New FileStream(NewFileName, FileMode.Open, FileAccess.Read, FileShare.Read)
            Dim buffer(infile.Length - 1) As Byte
            Dim count As Integer = infile.Read(buffer, 0, buffer.Length)
            If count <> buffer.Length Then
                infile.Close()
                MsgBox("Bad File")
                Return Nothing
            End If
            infile.Close()
            Return buffer

        Catch ex As Exception
            MsgBox("Bad File")
            Return Nothing
        End Try
    End Function
    Public Property Animated() As List(Of Boolean)
        Get
            Return _Animated
        End Get
        Set(ByVal value As List(Of Boolean))
            _Animated = value
        End Set
    End Property
    Public Property MeshXAngle() As List(Of Single)
        Get
            Return _MeshXAngle
        End Get
        Set(ByVal value As List(Of Single))
            _MeshXAngle = value
        End Set
    End Property
    Public Property MeshYAngle() As List(Of Single)
        Get
            Return _MeshYAngle
        End Get
        Set(ByVal value As List(Of Single))
            _MeshYAngle = value
        End Set
    End Property
    Public Property MeshZAngle() As List(Of Single)
        Get
            Return _MeshZAngle
        End Get
        Set(ByVal value As List(Of Single))
            _MeshZAngle = value
        End Set
    End Property
    Public Property MeshScale() As List(Of Single)
        Get
            Return _meshScale
        End Get
        Set(ByVal value As List(Of Single))
            _meshScale = value
        End Set
    End Property
    Public Property TextureLists() As List(Of TextureList)
        Get
            Return _txtLists
        End Get
        Set(ByVal value As List(Of TextureList))
            _txtLists = value
        End Set
    End Property
    Public Property CollisionSpheres() As List(Of CollisionSphere)
        Get
            Return _sphereList
        End Get
        Set(ByVal value As List(Of CollisionSphere))
            _sphereList = value
        End Set
    End Property

    Public Property CollisionBoxes() As List(Of CollisionBox)
        Get
            Return _boxList
        End Get
        Set(ByVal value As List(Of CollisionBox))
            _boxList = value
        End Set
    End Property

    Public Property MeshXOrig() As List(Of Single)
        Get
            Return MeshXOrigin
        End Get
        Set(ByVal value As List(Of Single))
            MeshXOrigin = value
        End Set
    End Property
    Public Property MeshYOrig() As List(Of Single)
        Get
            Return MeshYOrigin
        End Get
        Set(ByVal value As List(Of Single))
            MeshYOrigin = value
        End Set
    End Property
    Public Property MeshZOrig() As List(Of Single)
        Get
            Return MeshZOrigin
        End Get
        Set(ByVal value As List(Of Single))
            MeshZOrigin = value
        End Set
    End Property
    Public Property MeshText() As List(Of Byte())
        Get
            Return MeshFile
        End Get
        Set(ByVal value As List(Of Byte()))
            MeshFile = value
        End Set
    End Property
    Public Property MeshName() As List(Of String)
        Get
            Return _MeshName
        End Get
        Set(ByVal value As List(Of String))
            _MeshName = value
        End Set
    End Property
#End Region

#Region "Levels"

    Private AllLevels As List(Of bdEachLevel) = New List(Of bdEachLevel)

    Public Function AddLevel() As bdEachLevel
        AllLevels.Add(New bdEachLevel)
        Return AllLevels(AllLevels.Count - 1)
    End Function

    Public Property Levels() As List(Of bdEachLevel)
        Get
            Return AllLevels
        End Get
        Set(ByVal value As List(Of bdEachLevel))
            AllLevels = value
        End Set
    End Property

#End Region

#Region "Events ans Actions"
    Private _events As List(Of bdEvent) = New List(Of bdEvent)

    Public Function AddEvent(ByVal EventName As String) As bdEvent
        Dim bishwas As bdEvent = New bdEvent
        bishwas.EventName = EventName
        _events.Add(bishwas)
        Return _events(_events.IndexOf(bishwas))
    End Function
    Public Function GetEventIndex(ByVal _Event As bdEvent) As Integer
        Return _events.IndexOf(_Event)
    End Function
    Public Function GetEvent(ByVal index As Integer) As bdEvent
        Return _events(index)
    End Function
    Public Function GetTotalEvents() As Integer
        Return _events.Count
    End Function
    Public Sub RemoveEvent(ByVal Index As Integer)
        _events.RemoveAt(Index)
    End Sub
#End Region

#Region "Classes"
    Private _ClassName As List(Of String) = New List(Of String)
    Private _ClassText As List(Of String) = New List(Of String)
    Public Property ClassNames() As List(Of String)
        Get
            Return _ClassName
        End Get
        Set(ByVal value As List(Of String))
            _ClassName = value
        End Set
    End Property
    Public Property ClassTexts() As List(Of String)
        Get
            Return _ClassText
        End Get
        Set(ByVal value As List(Of String))
            _ClassText = value
        End Set
    End Property
#End Region

#Region "More Code"
    Private _moreCode As String

    Public Property MoreCode() As String
        Get
            Return _moreCode
        End Get
        Set(ByVal value As String)
            _moreCode = value
        End Set
    End Property
#End Region

#Region "Sprites"
    Private _spriteList As New List(Of bdSpriteStore)
   
    Private _spriteName As New List(Of String)

    Private _transparent As New List(Of Boolean)
    Private _transcolor As New List(Of Color)
    Public Property Transparent() As List(Of Boolean)
        Get
            Return _transparent
        End Get
        Set(ByVal value As List(Of Boolean))
            _transparent = value
        End Set
    End Property
    Public Property TransparentColor() As List(Of Color)
        Get
            Return _transcolor
        End Get
        Set(ByVal value As List(Of Color))
            _transcolor = value
        End Set
    End Property
    Public Property Sprites() As List(Of bdSpriteStore)
        Get
            Return _spriteList
        End Get
        Set(ByVal value As List(Of bdSpriteStore))
            _spriteList = value
        End Set
    End Property
    
    Public Property SpriteNames() As List(Of String)
        Get
            Return _spriteName
        End Get
        Set(ByVal value As List(Of String))
            _spriteName = value
        End Set
    End Property
#End Region

#Region "2D Sounds"
    Private snd2dTexts As New List(Of Byte())

    Private snd2dNames As New List(Of String)
    Public Property Sound2DNames() As List(Of String)
        Get
            Return snd2dNames
        End Get
        Set(ByVal value As List(Of String))
            snd2dNames = value
        End Set
    End Property
    Public Property Sound2DTexts() As List(Of Byte())
        Get
            Return snd2dTexts
        End Get
        Set(ByVal value As List(Of Byte()))
            snd2dTexts = value
        End Set
    End Property
#End Region

#Region "3D Sounds"
    Private snd3dTexts As New List(Of Byte())
    Private snd3dLevels As New List(Of Integer)
    Private snd3dNames As New List(Of String)
    Private snd3DXs As New List(Of Single)
    Private snd3DYs As New List(Of Single)
    Private snd3DZs As New List(Of Single)

    Public Property Sound3DXs() As List(Of Single)
        Get
            Return snd3DXs
        End Get
        Set(ByVal value As List(Of Single))
            snd3DXs = value
        End Set
    End Property
    Public Property Sound3DYs() As List(Of Single)
        Get
            Return snd3DYs
        End Get
        Set(ByVal value As List(Of Single))
            snd3DYs = value
        End Set
    End Property
    Public Property Sound3DZs() As List(Of Single)
        Get
            Return snd3DZs
        End Get
        Set(ByVal value As List(Of Single))
            snd3DZs = value
        End Set
    End Property
    Public Property Sound3DNames() As List(Of String)
        Get
            Return snd3dNames
        End Get
        Set(ByVal value As List(Of String))
            snd3dNames = value
        End Set
    End Property
    Public Property Sound3DTexts() As List(Of Byte())
        Get
            Return snd3dTexts
        End Get
        Set(ByVal value As List(Of Byte()))
            snd3dTexts = value
        End Set
    End Property
    Public Property Sound3DLevels() As List(Of Integer)
        Get
            Return snd3dLevels
        End Get
        Set(ByVal value As List(Of Integer))
            snd3dLevels = value
        End Set
    End Property
#End Region

#Region "Music"
    Private _musicText As New List(Of Byte())
    Private _musicName As New List(Of String)
    Public Property MusicText() As List(Of Byte())
        Get
            Return _musicText
        End Get
        Set(ByVal value As List(Of Byte()))
            _musicText = value
        End Set
    End Property
    Public Property MusicName() As List(Of String)
        Get
            Return _musicName
        End Get
        Set(ByVal value As List(Of String))
            _musicName = value
        End Set
    End Property
#End Region
End Class
