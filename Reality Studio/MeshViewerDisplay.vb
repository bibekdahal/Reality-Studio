Imports System
Imports System.IO
Imports System.Windows.Forms

Public Class MeshViewerDisplay

    Public WithEvents Game As bdGame
    Public SelectedIndex As Integer
    Public SphrColl As List(Of bd3DInstance) = New List(Of bd3DInstance)
    Public SphereObj As bd3DObject

    Public SelectedIndexB As Integer
    Public BoxColl As List(Of bd3DBoxInstance) = New List(Of bd3DBoxInstance)
    Public BoxObj As bd3DBox

    Private Shift As Boolean
    Private Sub Form1_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Game.Clean()
    End Sub
    Private Sub MeshViewer_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Game.Render()
        Me.Invalidate()
    End Sub
    Public Sub RemoveSphere(ByVal Index As Integer)
        SphrColl.RemoveAt(Index)

        SphereObj.RemoveInstance(Index, Game.CurrentLevel)
    End Sub
    Public Sub RemoveBox(ByVal Index As Integer)
        BoxColl.RemoveAt(Index)

        BoxObj.RemoveInstance(Index, Game.CurrentLevel)
    End Sub
    Private Sub Rendering() Handles Game.AfterRender
        Dim grass As MeshViewer = Me.Parent
        Try
            grass.Label2.Text = "Radius = " + (SphrColl(SelectedIndex).Mesh.Radius * SphrColl(SelectedIndex).ScaleFactor).ToString
        Catch
        End Try
    End Sub
    Private Sub Form1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Shift = e.Shift
        If e.KeyCode = Keys.NumPad4 Then CMoveLeft()
        If e.KeyCode = Keys.NumPad6 Then CMoveRight()
        If e.KeyCode = Keys.NumPad8 Then CMoveFront()
        If e.KeyCode = Keys.NumPad2 Then CMoveBack()
        If e.KeyCode = Keys.NumPad9 Then CMoveUp()
        If e.KeyCode = Keys.NumPad3 Then CMoveDown()

        If e.KeyCode = Keys.Delete Then CRotateLeft()
        If e.KeyCode = Keys.PageDown Then CRotateRight()
        If e.KeyCode = Keys.Home Then CRotateUp()
        If e.KeyCode = Keys.End Then CRotateDown()

        If (Shift = False And SelectedIndex > -1) Or (Shift = True And SelectedIndexB > -1) Then
            If e.KeyCode = Keys.A Then MoveLeft()
            If e.KeyCode = Keys.D Then MoveRight()
            If e.KeyCode = Keys.W Then MoveFront()
            If e.KeyCode = Keys.S Then MoveBack()
            If e.KeyCode = Keys.T Then MoveUp()
            If e.KeyCode = Keys.G Then MoveDown()
            If e.KeyCode = Keys.Insert Then SizeUP()
            If e.KeyCode = Keys.PageUp Then SizeDown()

            Select Case e.KeyCode
                Case Keys.D1
                    SizeXUp()
                Case Keys.D2
                    SizeXDown()
                Case Keys.D3
                    SizeYUp()
                Case Keys.D4
                    SizeYDown()
                Case Keys.D5
                    SizeZUp()
                Case Keys.D6
                    SizeZDown()
            End Select

        End If
        Dim gogo As MeshViewer = Me.Parent
        If gogo.DoSuppress = True Then
            e.SuppressKeyPress = True
        End If

    End Sub
    Public Sub AddSphere()
       
        Dim hhh As bd3DInstance = SphereObj.AddInstance(Game.CurrentLevel)
        hhh.X = SphrColl(SelectedIndex).X
        hhh.Y = SphrColl(SelectedIndex).Y
        hhh.Z = SphrColl(SelectedIndex).Z
        hhh.Mesh.AmbientColor = (Color.FromArgb(75, Color.HotPink))
        hhh.Mesh.DiffuseColor = (Color.FromArgb(75, Color.HotPink))
        'hhh.Mesh.Filled = False
        SelectedIndex += 1
        SphrColl.Add(hhh)
    End Sub

    Public Sub AddBox()

        Dim hhh As bd3DBoxInstance = BoxObj.AddInstance(Game.CurrentLevel)
        hhh.X = BoxColl(SelectedIndexB).X
        hhh.Y = BoxColl(SelectedIndexB).Y
        hhh.Z = BoxColl(SelectedIndexB).Z
        hhh.ScaleX = BoxColl(SelectedIndexB).ScaleX
        hhh.ScaleY = BoxColl(SelectedIndexB).ScaleY
        hhh.ScaleZ = BoxColl(SelectedIndexB).ScaleZ
        hhh.Mesh.AmbientColor = (Color.FromArgb(75, Color.HotPink))
        hhh.Mesh.DiffuseColor = (Color.FromArgb(75, Color.HotPink))
        'hhh.Mesh.Filled = False
        SelectedIndexB += 1
        BoxColl.Add(hhh)
    End Sub

    Public Sub AddSphere(ByVal CollisionSpheres As List(Of CollisionSphere.CollisionSpheres))

        For Each cbb As CollisionSphere.CollisionSpheres In CollisionSpheres
          
            Dim hhh As bd3DInstance = SphereObj.AddInstance(Game.CurrentLevel)
            hhh.X = cbb.CenterX
            hhh.Y = cbb.CenterY
            hhh.Z = cbb.CenterZ
            hhh.ScaleFactor = cbb.Radius / hhh.Mesh.Radius          'to del
            hhh.Mesh.AmbientColor = (Color.FromArgb(75, Color.HotPink))
            hhh.Mesh.DiffuseColor = (Color.FromArgb(75, Color.HotPink))
            'hhh.Mesh.Filled = False
            SelectedIndex += 1
            SphrColl.Add(hhh)
        Next
    End Sub
    Public Sub SizeUP()
        If Shift = False Then
            SphrColl(SelectedIndex).ScaleFactor += 0.01
        Else
            BoxColl(SelectedIndexB).ScaleX += 0.01
            BoxColl(SelectedIndexB).ScaleY += 0.01
            BoxColl(SelectedIndexB).ScaleZ += 0.01
        End If

    End Sub
    Public Sub SizeDown()
        If Shift = False Then
            SphrColl(SelectedIndex).ScaleFactor -= 0.01
        Else
            BoxColl(SelectedIndexB).ScaleX -= 0.01
            BoxColl(SelectedIndexB).ScaleY -= 0.01
            BoxColl(SelectedIndexB).ScaleZ -= 0.01
        End If
    End Sub

    Public Sub SizeXUp()
        BoxColl(SelectedIndexB).ScaleX += 0.01
    End Sub
    Public Sub SizeXDown()
        BoxColl(SelectedIndexB).ScaleX -= 0.01
    End Sub
    Public Sub SizeYUp()
        BoxColl(SelectedIndexB).ScaleY += 0.01
    End Sub
    Public Sub SizeYDown()
        BoxColl(SelectedIndexB).ScaleY -= 0.01
    End Sub
    Public Sub SizeZUp()
        BoxColl(SelectedIndexB).ScaleZ += 0.01
    End Sub
    Public Sub SizeZDown()
        BoxColl(SelectedIndexB).ScaleZ -= 0.01
    End Sub

    Public Sub MoveLeft()
        If Shift = False Then
            SphrColl(SelectedIndex).X -= 0.1
        Else
            BoxColl(SelectedIndexB).X -= 0.1
        End If
    End Sub
    Public Sub MoveRight()
        If Shift = False Then
            SphrColl(SelectedIndex).X += 0.1
        Else
            BoxColl(SelectedIndexB).X += 0.1
        End If
    End Sub
    Public Sub MoveDown()
        If Shift = False Then
            SphrColl(SelectedIndex).Y -= 0.1
        Else
            BoxColl(SelectedIndexB).Y -= 0.1
        End If
    End Sub
    Public Sub MoveUp()
        If Shift = False Then
            SphrColl(SelectedIndex).Y += 0.1
        Else
            BoxColl(SelectedIndexB).Y += 0.1
        End If
    End Sub
    Public Sub MoveFront()
        If Shift = False Then
            SphrColl(SelectedIndex).Z += 0.1
        Else
            BoxColl(SelectedIndexB).Z += 0.1
        End If
    End Sub
    Public Sub MoveBack()
        If Shift = False Then
            SphrColl(SelectedIndex).Z -= 0.1
        Else
            BoxColl(SelectedIndexB).Z -= 0.1
        End If
    End Sub

    Public Sub CRotateLeft()

        Game.Camera.TargetX -= 0.5
        Game.Camera.TargetZ -= 0.5
    End Sub
    Public Sub CRotateRight()

        Game.Camera.TargetX += 0.5
        Game.Camera.TargetZ += 0.5
    End Sub
    Public Sub CRotateUp()
        Game.Camera.TargetY += 0.5
    End Sub
    Public Sub CRotateDown()
        Game.Camera.TargetY -= 0.5
    End Sub
    Public Sub CMoveLeft()
        Game.Camera.X -= 0.1
        Game.Camera.TargetX -= 0.1
    End Sub
    Public Sub CMoveRight()
        Game.Camera.X += 0.1
        Game.Camera.TargetX += 0.1
    End Sub
    Public Sub CMoveDown()
        Game.Camera.Y -= 0.1
        Game.Camera.TargetY -= 0.1
    End Sub
    Public Sub CMoveUp()
        Game.Camera.Y += 0.1
        Game.Camera.TargetY += 0.1
    End Sub
    Public Sub CMoveFront()
        Game.Camera.Z += 0.1
        Game.Camera.TargetZ += 0.1
    End Sub
    Public Sub CMoveBack()
        Game.Camera.Z -= 0.1
        Game.Camera.TargetZ -= 0.1
    End Sub

    Private Sub MeshViewerDisplay_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.Opaque, True)
        Game = New bdGame()
        Game.Initialize(Me.Handle)
        Game.Camera.Z = -15
        Game.Camera.Y = 3
        My.Computer.FileSystem.CreateDirectory(Application.StartupPath + "\Temp")

        Dim infile As New FileStream(Application.StartupPath + "\Temp\MeshTMP.x", FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write)

        infile.Write(MainForm.CurrentMeshView, 0, MainForm.CurrentMeshView.Length)
        infile.Close()
        Dim msh As bdMesh = Game.AddMesh(Application.StartupPath + "\Temp\MeshTMP.x", Application.StartupPath + "\Textures", _animated)
        msh.DefaultXAngle = MainForm.CurrentMeshXAngle
        msh.DefaultYAngle = MainForm.CurrentMeshYAngle
        msh.DefaultZAngle = MainForm.CurrentMeshZAngle
        msh.DefaultScale = MainForm.CurrentMeshScale
        Dim obj As bd3DObject = Game.Add3DObject(msh)
        obj.AddInstance(Game.CurrentLevel)
        My.Computer.FileSystem.DeleteDirectory(Application.StartupPath + "\Temp", FileIO.DeleteDirectoryOption.DeleteAllContents)
        obj.GetInstance(0, 0).Mesh.SphereCollection.Clear()
        obj.GetInstance(0, 0).Mesh.BoxCollection.Clear()

        Dim rrr As Integer = MainForm.mstore.MeshText.IndexOf(MainForm.CurrentMeshView)

        Dim karka As CollisionSphere = MainForm.mstore.CollisionSpheres(rrr)
        If karka.NotYet = True Then
            Dim volde As New bdCollisionSphere
            Dim potty As bdMesh = obj.GetInstance(0, 0).Mesh
            volde.Center = potty.GetCenter
            volde.Radius = potty.Radius
            potty.SphereCollection.Add(volde)
        Else
            For Each hermi As CollisionSphere.CollisionSpheres In karka.Spheres
                Dim ron As New bdCollisionSphere
                ron.Center = New Vector3(hermi.CenterX, hermi.CenterY, hermi.CenterZ)
                ron.Radius = hermi.Radius
                obj.GetInstance(0, 0).Mesh.SphereCollection.Add(ron)
            Next
        End If

        SelectedIndex = -1
        For Each gone As bdCollisionSphere In obj.GetInstance(0, 0).Mesh.SphereCollection

            SphereObj = Game.Add3DObject(Game.AddMesh(gone.Radius, 15, 15))
            Dim hhh As bd3DInstance = SphereObj.AddInstance(Game.CurrentLevel)

            hhh.X = gone.Center.X
            hhh.Y = gone.Center.Y
            hhh.Z = gone.Center.Z
            hhh.Mesh.AmbientColor = Color.FromArgb(75, Color.HotPink)
            hhh.Mesh.DiffuseColor = Color.FromArgb(75, Color.HotPink)
            'hhh.Mesh.Filled = False
            SelectedIndex += 1
            SphrColl.Add(hhh)
        Next

        Dim karkab As CollisionBox = MainForm.mstore.CollisionBoxes(rrr)
        If karkab.NotYet = True Then
            Dim voldeb As New bdAxisAlignedBox
            Dim pottyb As bdMesh = obj.GetInstance(0, 0).Mesh
            voldeb.MinPos = pottyb.BoundingBox.MinPos
            voldeb.MaxPos = pottyb.BoundingBox.MaxPos
            pottyb.BoxCollection.Add(voldeb)
        Else
            For Each hermib As CollisionBox.CollisionBoxes In karkab.Boxes
                Dim ronb As New bdAxisAlignedBox
                ronb.MinPos = New Vector3(hermib.MinX, hermib.MinY, hermib.MinZ)
                ronb.MaxPos = New Vector3(hermib.MaxX, hermib.MaxY, hermib.MaxZ)
                obj.GetInstance(0, 0).Mesh.BoxCollection.Add(ronb)
            Next
        End If

        SelectedIndexB = -1
        For Each gone As bdAxisAlignedBox In obj.GetInstance(0, 0).Mesh.BoxCollection

            Dim width As Single = gone.MaxPos.X - gone.MinPos.X
            Dim height As Single = gone.MaxPos.Y - gone.MinPos.Y
            Dim depth As Single = gone.MaxPos.Z - gone.MinPos.Z

            BoxObj = Game.Add3DBox(width, height, depth)
            Dim hhh As bd3DBoxInstance = BoxObj.AddInstance(Game.CurrentLevel)

            hhh.X = gone.MinPos.X
            hhh.Y = gone.MinPos.Y
            hhh.Z = gone.MinPos.Z
            hhh.Mesh.AmbientColor = Color.FromArgb(75, Color.HotPink)
            hhh.Mesh.DiffuseColor = Color.FromArgb(75, Color.HotPink)
            'hhh.Mesh.Filled = False
            SelectedIndexB += 1
            BoxColl.Add(hhh)
        Next
        Me.Refresh()
    End Sub

    Public Sub New(ByVal Animated As Boolean)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _animated = Animated
    End Sub

    Private _animated As Boolean
End Class
