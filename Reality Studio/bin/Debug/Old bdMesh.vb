Imports Microsoft.DirectX
Imports Microsoft.DirectX.Direct3D
Imports Microsoft.DirectX.Direct3D.D3DX
Public Class bdMesh
    Private _mesh As Mesh
    Private _device As Device
    Private _material() As Material 'for the meshes from files
    Private _materiala As Material  'for rest of the meshes

    Private _meshTexture() As Texture
    Private _extMaterial() As ExtendedMaterial = Nothing



    Private _xorigin As Single = 0
    Private _yorigin As Single = 0
    Private _zorigin As Single = 0

    Private _defaultXangle As Single = 0
    Private _defaultYangle As Single = 0
    Private _defaultZangle As Single = 0
    Private _boundingbox As bdMainGame.BoundingBox

    Private _width As Single
    Private _height As Single
    Private _depth As Single

    Private _radius As Single
    Private _center As Vector3

    Private _meshFile As Boolean

    Private _type As bd3DObjectTypes

    Private _SphereRadius As Single

    Private _filled As Boolean = True

    Private _sphereCollection As List(Of bdCollisionSphere) = New List(Of bdCollisionSphere)

#Region "Properties"
    Public Property DefaultXAngle() As Single
        Get
            Return _defaultXangle
        End Get
        Set(ByVal value As Single)
            _defaultXangle = value
        End Set
    End Property
    Public Property DefaultYAngle() As Single
        Get
            Return _defaultYangle
        End Get
        Set(ByVal value As Single)
            _defaultYangle = value
        End Set
    End Property
    Public Property DefaultZAngle() As Single
        Get
            Return _defaultZangle
        End Get
        Set(ByVal value As Single)
            _defaultZangle = value
        End Set
    End Property
    Public ReadOnly Property SphereRadius() As Single
        Get
            Return _SphereRadius
        End Get
    End Property
    Public Property Filled() As Boolean
        Get
            Return _filled
        End Get
        Set(ByVal value As Boolean)
            _filled = value
        End Set
    End Property
    Public Property SphereCollection() As List(Of bdCollisionSphere)
        Get
            Return _sphereCollection
        End Get
        Set(ByVal value As List(Of bdCollisionSphere))
            _sphereCollection = value
        End Set
    End Property
    Public ReadOnly Property Width() As Single
        Get
            Return _width
        End Get
    End Property
    Public ReadOnly Property Height() As Single
        Get
            Return _height
        End Get
    End Property
    Public ReadOnly Property Depth() As Single
        Get
            Return _depth
        End Get
    End Property
    Public ReadOnly Property Radius() As Single
        Get
            Return _radius
        End Get
    End Property
    Public Property XOrigin() As Single
        Get
            Return _xorigin
        End Get
        Set(ByVal value As Single)
            _xorigin = value
        End Set
    End Property
    Public Property YOrigin() As Single
        Get
            Return _yorigin
        End Get
        Set(ByVal value As Single)
            _yorigin = value
        End Set
    End Property
    Public Property ZOrigin() As Single
        Get
            Return _zorigin
        End Get
        Set(ByVal value As Single)
            _zorigin = value
        End Set
    End Property
    Public Property D3DMesh() As Mesh
        Get
            Return _mesh
        End Get
        Set(ByVal value As Mesh)
            _mesh = value
        End Set
    End Property
    Public Property ExtendedMaterial() As ExtendedMaterial()
        Get
            Return _extMaterial
        End Get
        Set(ByVal value As ExtendedMaterial())
            _extMaterial = value
        End Set
    End Property

    Public Property Material() As Material()
        Get
            Return _material
        End Get
        Set(ByVal value As Material())
            _material = value
        End Set
    End Property
    Public ReadOnly Property Type() As bd3DObjectTypes
        Get
            Return _type
        End Get
    End Property
#End Region

    Enum bd3DObjectTypes
        Text3D = 0
        Box = 1
        Cylinder = 2
        Sphere = 3
        Polygon = 4
        Torus = 5
        Teapot = 6
        File = 7
    End Enum

    Public Sub New(ByVal IDevice As Device, ByVal Font As System.Drawing.Font, ByVal Text As String, ByVal Deviation As Single, ByVal Extrusion As Single)
        _device = IDevice
        _mesh = Mesh.TextFromFont(_device, Font, Text, Deviation, Extrusion)

        _type = bd3DObjectTypes.Text3D

        ComputeRadius()
        ComputeWHD()
        Dim ggg As New bdCollisionSphere
        ggg.Center = _center
        ggg.Radius = _radius
        _sphereCollection.Add(ggg)
    End Sub
    Public Sub New(ByVal IDevice As Device, ByVal Width As Single, ByVal Height As Single, ByVal Depth As Single)
        _device = IDevice
        _mesh = Mesh.Box(_device, Width, Height, Depth)
        _type = bd3DObjectTypes.Box
        ComputeRadius()
        ComputeWHD()
        Dim ggg As New bdCollisionSphere
        ggg.Center = _center
        ggg.Radius = _radius
        _sphereCollection.Add(ggg)
    End Sub
    Public Sub New(ByVal IDevice As Device, ByVal Radius1 As Single, ByVal Radius2 As Single, ByVal Height As Single, ByVal Slices As Integer, ByVal Stacks As Integer)
        _device = IDevice
        _mesh = Mesh.Cylinder(_device, Radius1, Radius2, Height, Slices, Stacks)
        _type = bd3DObjectTypes.Cylinder

        ComputeRadius()
        ComputeWHD()
        Dim ggg As New bdCollisionSphere
        ggg.Center = _center
        ggg.Radius = _radius
        _sphereCollection.Add(ggg)
    End Sub
    Public Sub New(ByVal IDevice As Device, ByVal Length As Single, ByVal Sides As Integer)
        _device = IDevice
        _mesh = Mesh.Polygon(_device, Length, Sides)
        _type = bd3DObjectTypes.Cylinder

        ComputeRadius()
        ComputeWHD()
        Dim ggg As New bdCollisionSphere
        ggg.Center = _center
        ggg.Radius = _radius
        _sphereCollection.Add(ggg)
    End Sub

    Public Sub New(ByVal IDevice As Device, ByVal Radius As Single, ByVal Slices As Integer, ByVal Stacks As Integer)
        _device = IDevice
        _mesh = Mesh.Sphere(_device, Radius, Slices, Stacks)
        _type = bd3DObjectTypes.Sphere
        _SphereRadius = Radius
        ComputeRadius()
        ComputeWHD()
        Dim ggg As New bdCollisionSphere
        ggg.Center = _center
        ggg.Radius = _radius
        _sphereCollection.Add(ggg)
    End Sub

    Public Sub New(ByVal IDevice As Device, ByVal InnerRadius As Single, ByVal OuterRadius As Single, ByVal Sides As Integer, ByVal Rings As Integer)
        _device = IDevice
        _mesh = Mesh.Torus(_device, InnerRadius, OuterRadius, Sides, Rings)
        _type = bd3DObjectTypes.Teapot

        ComputeRadius()
        ComputeWHD()
        Dim ggg As New bdCollisionSphere
        ggg.Center = _center
        ggg.Radius = _radius
        _sphereCollection.Add(ggg)
    End Sub

    Public Sub New(ByVal IDevice As Device, ByVal FileName As String, Optional ByVal TextureFileName As String = "", Optional ByVal TexturePath As String = "")
        _device = IDevice
        LoadMesh(FileName, TextureFileName, TexturePath)
        _meshFile = True
        _type = bd3DObjectTypes.File
    End Sub

    Public Sub LoadMesh(ByVal FileName As String, ByVal TextureFileName As String, ByVal TexturePath As String)
        Dim actualPath As String = ""
        _mesh = Mesh.FromFile(FileName, MeshFlags.Managed, _device, _extMaterial)
        If (Not (_extMaterial Is Nothing)) AndAlso (_extMaterial.Length > 0) Then
            _meshTexture = New Texture(_extMaterial.Length) {}
            _material = New Material(_extMaterial.Length) {}
            Dim i As Integer = 0
            While i < _extMaterial.Length
                _material(i) = _extMaterial(i).Material3D
                _material(i).Ambient = _material(i).Diffuse
                If TextureFileName <> "" Then
                    _extMaterial(i).TextureFilename = TextureFileName
                End If

                If Not (_extMaterial(i).TextureFilename Is Nothing) AndAlso (Not (_extMaterial(i).TextureFilename = String.Empty)) Then
                    If TexturePath <> "" Then
                        actualPath = IO.Directory.GetCurrentDirectory
                        IO.Directory.SetCurrentDirectory(TexturePath)
                    End If
                    _meshTexture(i) = TextureLoader.FromFile(_device, _extMaterial(i).TextureFilename)
                    If TexturePath <> "" Then
                        IO.Directory.SetCurrentDirectory(actualPath)
                    End If
                End If
                System.Math.Min(System.Threading.Interlocked.Increment(i), i - 1)
            End While
        End If
        ComputeRadius()
        ComputeWHD()
        Dim ggg As New bdCollisionSphere
        ggg.Center = _center
        ggg.Radius = _radius
        _sphereCollection.Add(ggg)
    End Sub

    Public Function BoundingBox() As bdMainGame.BoundingBox
        ComputeWHD()
        Return _boundingbox
    End Function

    Public Function GetCenter() As Vector3
        ComputeRadius()
        Return _center
    End Function
    Private Sub ComputeWHD()
        Dim vertexBuffer As VertexBuffer = Nothing
        Try
            vertexBuffer = _mesh.VertexBuffer
            Dim gStream As GraphicsStream = vertexBuffer.Lock(0, 0, LockFlags.None)

            Geometry.ComputeBoundingBox(gStream, _mesh.NumberVertices, _mesh.VertexFormat, _boundingbox.Min, _boundingbox.Max)
            _width = _boundingbox.Max.X - _boundingbox.Min.X
            _height = _boundingbox.Max.Y - _boundingbox.Min.Y
            _depth = _boundingbox.Max.Z - _boundingbox.Min.Z

        Finally
            vertexBuffer.Unlock()
            vertexBuffer.Dispose()
        End Try
    End Sub
    Private Sub ComputeRadius()
        Dim vertexBuffer As VertexBuffer = Nothing
        Try
            vertexBuffer = _mesh.VertexBuffer
            Dim gStream As GraphicsStream = vertexBuffer.Lock(0, 0, LockFlags.None)

            _radius = Geometry.ComputeBoundingSphere(gStream, _mesh.NumberVertices, _mesh.VertexFormat, _center)
        Finally
            vertexBuffer.Unlock()
            vertexBuffer.Dispose()
        End Try
    End Sub

    Public Sub ChangeColor(ByVal NewColor As Color) 'not for meshes from files
        _materiala.Ambient = NewColor
        _materiala.Diffuse = NewColor

    End Sub


    Public Sub Render(ByVal XScale As Single, ByVal YScale As Single, ByVal ZScale As Single, ByVal XAngle As Single, ByVal YAngle As Single, ByVal ZAngle As Single, ByVal X As Single, ByVal Y As Single, ByVal Z As Single)
        _device.Transform.World = Matrix.Identity
        'Generally you want to do all the stuff thats local to the mesh first then position it in the world, something along the lines of:
        'scale * rotX * rotY * rotZ * translation
        'When dealing with the view matrix it turns out to be the other way around because the view is always at the center and its the world that moves around it, something along the lines of:
        'translation * rotZ * rotY * rotX

        _device.Transform.World = Matrix.Scaling(XScale, YScale, ZScale)

        _device.Transform.World = Matrix.Multiply(_device.Transform.World, Matrix.RotationX(Me.DefaultXAngle / 180 * Math.PI))
        _device.Transform.World = Matrix.Multiply(_device.Transform.World, Matrix.RotationY(Me.DefaultYAngle / 180 * Math.PI))
        _device.Transform.World = Matrix.Multiply(_device.Transform.World, Matrix.RotationZ(Me.DefaultZAngle / 180 * Math.PI))

        _device.Transform.World = Matrix.Multiply(_device.Transform.World, Matrix.Translation(-Me.XOrigin, -Me.YOrigin, -Me.ZOrigin))
        _device.Transform.World = Matrix.Multiply(_device.Transform.World, Matrix.RotationX(XAngle / 180 * Math.PI))
        _device.Transform.World = Matrix.Multiply(_device.Transform.World, Matrix.RotationY(YAngle / 180 * Math.PI))
        _device.Transform.World = Matrix.Multiply(_device.Transform.World, Matrix.RotationZ(ZAngle / 180 * Math.PI))
        _device.Transform.World = Matrix.Multiply(_device.Transform.World, Matrix.Translation(Me.XOrigin, Me.YOrigin, Me.ZOrigin))
        _device.Transform.World = Matrix.Multiply(_device.Transform.World, Matrix.Translation(New Vector3(X, Y, Z)))


        If _filled = False Then
            _device.RenderState.AlphaBlendEnable = True

            _device.RenderState.SourceBlend = Blend.SourceAlpha
            _device.RenderState.DestinationBlend = Blend.InvSourceAlpha

        End If

        If _type = bd3DObjectTypes.File Then
            Dim i As Integer = 0
            While i < _material.Length

                _device.Material = _material(i)
                _device.SetTexture(0, _meshTexture(i))
                _mesh.DrawSubset(i)
                System.Math.Min(System.Threading.Interlocked.Increment(i), i - 1)
            End While
        Else
            _device.Material = _materiala
            _device.SetTexture(0, Nothing)
            _mesh.DrawSubset(0)
        End If
        _device.RenderState.AlphaBlendEnable = False
        _device.RenderState.AlphaTestEnable = False

    End Sub
End Class
