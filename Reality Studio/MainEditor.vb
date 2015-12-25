Imports System
Imports System.IO
Public Class MainEditor
    Private currentobj As Integer = 0
    Private current2Dobj As Integer = 0
    Public WithEvents Game As New bdGame
    Public CurrentMesh As bd3DInstance

    Public CurrentIns As bd2DInstance

    Public WallC As List(Of bd3DInstance) = New List(Of bd3DInstance)
    Public WallObj As bd3DObject
    Public SelectedWall As Integer = -1

    Public SndC As List(Of bd3DInstance) = New List(Of bd3DInstance)
    Public sndObj As bd3DObject
    Public SelectedSnd As Integer = -1

    Private Move2d As Boolean = True
    Private CurrentView As Views = Views.Front

    Private Enum Views
        Front = 0
        Top = 1
    End Enum

    Private Sub Form1_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Game.Clean()
    End Sub
    Private Sub AfterEvnt() Handles Game.AfterRender
        If Game.GetKeyboardState(Key.LEFT) Then MoveLeft(False)
        If Game.GetKeyboardState(Key.RIGHT) Then MoveRight(False)
        If Game.GetKeyboardState(Key.UP) Then MoveUp(False)
        If Game.GetKeyboardState(Key.DOWN) Then MoveDown(False)
        Dim ggg As LevelEditor = Me.ParentForm
        ggg.CameraPosButton.Text = "Camera Pos: (" + Math.Round(Game.Camera.X).ToString + ", " + _
                           Math.Round(Game.Camera.Y).ToString + ", " + _
                            Math.Round(Game.Camera.Z).ToString + ") Target: (" + _
                             Math.Round(Game.Camera.TargetX).ToString + ", " + _
                             Math.Round(Game.Camera.TargetY).ToString + ", " + _
                             Math.Round(Game.Camera.TargetZ).ToString + ")"
    End Sub
    Public Sub Form1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Modifiers = Windows.Forms.Keys.Shift Then
            If e.KeyCode = Windows.Forms.Keys.A Then MoveSndLeft()
            If e.KeyCode = Windows.Forms.Keys.D Then MoveSndRight()
            If e.KeyCode = Windows.Forms.Keys.W Then MoveSndFront()
            If e.KeyCode = Windows.Forms.Keys.S Then MoveSndBack()
            If e.KeyCode = Windows.Forms.Keys.T Then MoveSndUp()
            If e.KeyCode = Windows.Forms.Keys.G Then MoveSndDown()
        Else
            If e.KeyCode = Windows.Forms.Keys.A Then MoveLeft(True)
            If e.KeyCode = Windows.Forms.Keys.D Then MoveRight(True)
            If e.KeyCode = Windows.Forms.Keys.W Then MoveFront()
            If e.KeyCode = Windows.Forms.Keys.S Then MoveBack()
            If e.KeyCode = Windows.Forms.Keys.T Then MoveUp(True)
            If e.KeyCode = Windows.Forms.Keys.G Then MoveDown(True)
        End If

        If e.KeyCode = Windows.Forms.Keys.NumPad4 Then CMoveLeft()
        If e.KeyCode = Windows.Forms.Keys.NumPad6 Then CMoveRight()
        If e.KeyCode = Windows.Forms.Keys.NumPad8 Then CMoveFront()
        If e.KeyCode = Windows.Forms.Keys.NumPad2 Then CMoveBack()
        If e.KeyCode = Windows.Forms.Keys.NumPad9 Then CMoveUp()
        If e.KeyCode = Windows.Forms.Keys.NumPad3 Then CMoveDown()

        If e.KeyCode = Windows.Forms.Keys.Delete Then CTargetLeft()
        If e.KeyCode = Windows.Forms.Keys.PageDown Then CTargetRight()
        If e.KeyCode = Windows.Forms.Keys.Home Then CTargetUp()
        If e.KeyCode = Windows.Forms.Keys.End Then CTargetDown()

        If e.KeyCode = Windows.Forms.Keys.J Then MoveWallLeft()
        If e.KeyCode = Windows.Forms.Keys.L Then MoveWallRight()
        If e.KeyCode = Windows.Forms.Keys.I Then MoveWallFront()
        If e.KeyCode = Windows.Forms.Keys.K Then MoveWallBack()
        If e.KeyCode = Windows.Forms.Keys.Y Then MoveWallUp()
        If e.KeyCode = Windows.Forms.Keys.H Then MoveWallDown()

        If SelectedWall > -1 Then
            If e.KeyCode = Windows.Forms.Keys.M Then WallC(SelectedWall).ScaleFactor += 0.01
            If e.KeyCode = Windows.Forms.Keys.N Then WallC(SelectedWall).ScaleFactor -= 0.01
            If e.KeyCode = Windows.Forms.Keys.Z Then WallC(SelectedWall).XAngle -= 90
            If e.KeyCode = Windows.Forms.Keys.X Then WallC(SelectedWall).YAngle -= 90
            If e.KeyCode = Windows.Forms.Keys.C Then WallC(SelectedWall).ZAngle -= 90
        End If

        Dim gogo As LevelEditor = Me.Parent
        If gogo.DoSuppress = True Then
            e.SuppressKeyPress = True
        End If
    End Sub
    Public Sub ViewTop()
        If CurrentView = Views.Top Then Exit Sub
        Game.Camera.Y = 30
        Game.Camera.Z += 20
        Game.Camera.TargetX = Game.Camera.X
        Game.Camera.TargetZ = Game.Camera.Z + 0.01
        Game.Camera.TargetY = Game.Camera.Y - 1
        CurrentView = Views.Top
    End Sub
    Public Sub ViewFront()
        If CurrentView = Views.Front Then Exit Sub
        Game.Camera.Y = 3
        Game.Camera.Z -= 20
        Game.Camera.TargetX = Game.Camera.X
        Game.Camera.TargetZ = Game.Camera.Z + 0.01
        Game.Camera.TargetY = Game.Camera.Y
        CurrentView = Views.Front
    End Sub

    Private Sub MainEditor_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        Move2d = True
    End Sub
    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Show()
        Me.SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.Opaque, True)
        Game.Initialize(Me.Handle)
        Game.Camera.Z = -10
        Game.Camera.Y = 3
        Me.Focus()
    End Sub

    Public Sub AddMeshObj(ByVal Index As Integer)

        Dim ggg As MainForm = MainForm

        My.Computer.FileSystem.CreateDirectory(Application.StartupPath + "\Temp")

        Dim infile As New FileStream(Application.StartupPath + "\Temp\MeshTMP.x", FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write)

        Dim okok As Integer = ggg.mstore.MeshName.IndexOf(ggg.mstore.Object3DMesh(Index))
        infile.Write(ggg.mstore.MeshText(okok), 0, ggg.mstore.MeshText(okok).Length)
        infile.Close()

        Dim newm As bdMesh = Game.AddMesh(Application.StartupPath + "\Temp\MeshTMP.x", Application.StartupPath + "\Textures", ggg.mstore.Animated(okok))
        newm.DefaultXAngle = ggg.mstore.MeshXAngle(okok)
        newm.DefaultYAngle = ggg.mstore.MeshYAngle(okok)
        newm.DefaultZAngle = ggg.mstore.MeshZAngle(okok)
        newm.DefaultScale = ggg.mstore.MeshScale(okok)
        Dim newmobj As bd3DObject = Game.Add3DObject(newm)

        My.Computer.FileSystem.DeleteDirectory(Application.StartupPath + "\Temp", FileIO.DeleteDirectoryOption.DeleteAllContents)

    End Sub
    Public Sub AddSndMesh()
        AddSndMesh(0, 0, 0)
    End Sub
    Public Sub AddSndMesh(ByVal X As Single, ByVal Y As Single, ByVal Z As Single)
        Dim kow As bd3DInstance = sndObj.AddInstance(Game.CurrentLevel)
        kow.X = X
        kow.Y = Y
        kow.Z = Z
        SndC.Add(kow)
    End Sub
    Public Sub Add2DObj(ByVal Index As Integer)

        Dim ggg As MainForm = MainForm

        Dim newm As bdSprite = Game.AddSprite

        Dim newmobj As bd2DObject = Game.Add2DObject(newm)
        Dim dkdk As Integer = ggg.mstore.SpriteNames.IndexOf(ggg.mstore.Object2DSprite(Index))
        For Each donkey() As Byte In ggg.mstore.Sprites(dkdk).ImageTexts
            My.Computer.FileSystem.CreateDirectory(Application.StartupPath + "\Temp")

            Dim infile As New FileStream(Application.StartupPath + "\Temp\imga.bmp", FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write)

            infile.Write(donkey, 0, donkey.Length)
            infile.Close()
            Dim img As Image = Image.FromFile(Application.StartupPath + "\Temp\imga.bmp")
            newm.AddImageFromFile(Application.StartupPath + "\Temp\imga.bmp",  ggg.mstore.Transparent(dkdk), ggg.mstore.TransparentColor(dkdk))
            img.Dispose()
            My.Computer.FileSystem.DeleteDirectory(Application.StartupPath + "\Temp", FileIO.DeleteDirectoryOption.DeleteAllContents)
        Next

    End Sub
    Public Sub MakeWallVisible()
        For Each g As bd3DInstance In WallC
            g.Visible = True
        Next
    End Sub
    Public Sub MakeWallInVisible()
        For Each g As bd3DInstance In WallC
            g.Visible = False
        Next
    End Sub
    Public Sub ChangeCurrentMesh(ByVal ObjIndex As Integer, ByVal InsIndex As Integer)

        CurrentMesh = Game.Get3DObject(ObjIndex).GetInstance(InsIndex, Game.CurrentLevel)
    End Sub
    Public Sub ChangeCurrent2DInstance(ByVal ObjIndex As Integer, ByVal InsIndex As Integer)
        CurrentIns = Game.Get2DObject(ObjIndex).GetInstance(InsIndex, Game.CurrentLevel)
    End Sub
    Public Sub Load2DInstances(ByVal LevelIndex As Integer)
        Dim ggg As MainForm = MainForm
        Dim bbb As bdEachLevel = ggg.mstore.Levels(LevelIndex)
        For Each ins As String In bbb.Instances2DID
            Dim kkkk As Integer = ggg.mstore.Object2DName.IndexOf(bbb.Objects2DUsed(current2Dobj))
            Dim dude As bd2DObject = Game.Get2DObject(kkkk)
            Dim kukhura As bd2DInstance = dude.AddInstance(Game.CurrentLevel)
            kukhura.X = bbb.X2Ds(current2Dobj)
            kukhura.Y = bbb.Y2Ds(current2Dobj)
            current2Dobj += 1
        Next
    End Sub

    Public Sub LoadInstances(ByVal LevelIndex As Integer)
        Dim ggg As MainForm = MainForm
        Dim bbb As bdEachLevel = ggg.mstore.Levels(LevelIndex)

        For Each ins As String In bbb.InstancesID
            Dim kkkk As Integer = ggg.mstore.Object3DName.IndexOf(bbb.ObjectsUsed(currentobj))
            Dim dude As bd3DObject = Game.Get3DObject(kkkk)
            Dim kukhura As bd3DInstance = dude.AddInstance(Game.CurrentLevel)
            kukhura.X = bbb.Xs(currentobj)
            kukhura.Y = bbb.Ys(currentobj)
            kukhura.Z = bbb.Zs(currentobj)
            currentobj += 1
        Next
        Dim NewMesh As bdMesh
        If bbb.BoxXs.Count > 0 Then
            NewMesh = Game.AddMesh(bbb.BoxWidths(0), bbb.BoxHeights(0), bbb.BoxDepths(0))

        Else
            NewMesh = Game.AddMesh(1.0F, 1.0F, 1.0F)
        End If
        'NewMesh.Filled = False



        NewMesh.AmbientColor = Color.FromArgb(100, Color.Red)
        NewMesh.DiffuseColor = Color.FromArgb(100, Color.Red)
        WallObj = Game.Add3DObject(NewMesh)



        Dim sndMesh As bdMesh = Game.AddMesh(0.7F, 0.7F, 0.7F)
        sndObj = Game.Add3DObject(sndMesh)
        'sndMesh.Filled = False
        sndMesh.AmbientColor = Color.FromArgb(100, Color.Green)
        sndMesh.DiffuseColor = Color.FromArgb(100, Color.Green)
    End Sub
    Public Sub LoadWalls(ByVal LevelIndex As Integer)
        Dim ggg As MainForm = MainForm
        Dim bbb As bdEachLevel = ggg.mstore.Levels(LevelIndex)
        For Each wall As Single In bbb.BoxXs
            AddWalla()
            Dim ind As Integer = bbb.BoxXs.IndexOf(wall)
            WallC(SelectedWall).X = bbb.BoxXs(ind)
            WallC(SelectedWall).Y = bbb.BoxYs(ind)
            WallC(SelectedWall).Z = bbb.BoxZs(ind)
            WallC(SelectedWall).XAngle = bbb.BoxXAngles(ind)
            WallC(SelectedWall).YAngle = bbb.BoxYAngles(ind)
            WallC(SelectedWall).ZAngle = bbb.BoxZAngles(ind)
            WallC(SelectedWall).ScaleFactor = bbb.BoxScales(ind)
        Next
    End Sub
    Public Sub LoadSounds(ByVal LevelIndex As Integer)
        Dim ggg As MainForm = MainForm
        For Each wall As Single In ggg.mstore.Sound3DXs
            AddSndMesh(wall, ggg.mstore.Sound3DYs(ggg.mstore.Sound3DXs.IndexOf(wall)), ggg.mstore.Sound3DZs(ggg.mstore.Sound3DXs.IndexOf(wall)))
        Next
    End Sub
    Public Sub AddWall()
        Dim coward As bd3DInstance = WallObj.AddInstance(Game.CurrentLevel)
        If SelectedWall <> -1 Then
            coward.X = WallC(SelectedWall).X
            coward.Y = WallC(SelectedWall).Y
            coward.Z = WallC(SelectedWall).Z
            coward.XAngle = WallC(SelectedWall).XAngle
            coward.YAngle = WallC(SelectedWall).YAngle
            coward.ZAngle = WallC(SelectedWall).ZAngle
            coward.ScaleFactor = WallC(SelectedWall).ScaleFactor
            WallC.Add(coward)
            SelectedWall = WallC.IndexOf(coward)
        Else
            Dim Wd As Single = Val(InputBox("Enter The Width", , "1.0"))
            Dim Hd As Single = Val(InputBox("Enter The Height", , "1.0"))
            Dim Dd As Single = Val(InputBox("Enter The Depth", , "1.0"))
            Dim brave As bdMesh = Game.AddMesh(Wd, Hd, Dd)
            'brave.Filled = False
            brave.AmbientColor = Color.FromArgb(100, Color.GhostWhite)
            brave.DiffuseColor = Color.FromArgb(100, Color.GhostWhite)
            WallObj.ChangeMesh(brave)
            coward.X = 0
            coward.Y = 0
            coward.Z = 0
            WallC.Add(coward)
            SelectedWall = WallC.IndexOf(coward)
        End If
    End Sub
    Public Sub AddWalla()
        Dim coward As bd3DInstance = WallObj.AddInstance(Game.CurrentLevel)
        If SelectedWall <> -1 Then
            coward.X = WallC(SelectedWall).X
            coward.Y = WallC(SelectedWall).Y
            coward.Z = WallC(SelectedWall).Z
            coward.XAngle = WallC(SelectedWall).XAngle
            coward.YAngle = WallC(SelectedWall).YAngle
            coward.ZAngle = WallC(SelectedWall).ZAngle
            coward.ScaleFactor = WallC(SelectedWall).ScaleFactor
            WallC.Add(coward)
            SelectedWall = WallC.IndexOf(coward)
        Else
            coward.X = 0
            coward.Y = 0
            coward.Z = 0
            WallC.Add(coward)
            SelectedWall = WallC.IndexOf(coward)
        End If
    End Sub
    Public Sub RemoveWall(ByVal index As Integer)
        WallC.RemoveAt(index)
        WallObj.RemoveInstance(index, Game.CurrentLevel)
        SelectedWall -= 1
    End Sub
    Public Sub RemoveSnd(ByVal index As Integer)
        SndC.RemoveAt(index)
        sndObj.RemoveInstance(index, Game.CurrentLevel)
        SelectedSnd -= 1
    End Sub
    Public Sub AddInstance(ByVal ObjIndex As Integer)
        Dim dude As bd3DObject = Game.Get3DObject(ObjIndex)
        Dim ggg As bd3DInstance = dude.AddInstance(Game.CurrentLevel)
        If CurrentMesh Is Nothing Then
            With ggg
                .X = 0
                .Y = 0
                .Z = 0
            End With
        Else
            With ggg
                .X = CurrentMesh.X
                .Y = CurrentMesh.Y
                .Z = CurrentMesh.Z
            End With
        End If
        CurrentMesh = ggg

    End Sub
    Public Sub Add2DInstance(ByVal ObjIndex As Integer)
        Dim dude As bd2DObject = Game.Get2DObject(ObjIndex)
        Dim ggg As bd2DInstance = dude.AddInstance(Game.CurrentLevel)
        If CurrentIns Is Nothing Then
            With ggg
                .X = 0
                .Y = 0
            End With
        Else
            With ggg
                .X = CurrentIns.X
                .Y = CurrentIns.Y
            End With

        End If
        CurrentIns = ggg
    End Sub
    Public Sub RemoveInstance(ByVal ObjIndex As Integer, ByVal InsIndex As Integer)
        Dim dude As bd3DObject = Game.Get3DObject(ObjIndex)
        dude.RemoveInstance(InsIndex, Game.CurrentLevel)
    End Sub
    Public Sub Remove2DInstance(ByVal ObjIndex As Integer, ByVal InsIndex As Integer)
        Dim dude As bd2DObject = Game.Get2DObject(ObjIndex)
        dude.RemoveInstance(InsIndex, Game.CurrentLevel)
    End Sub

    Private Sub MainEditor_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown
        Dim ggg As LevelEditor = Me.ParentForm

        If Not (Game.GetKeyboardState(Key.LCONTROL) Or _
                    Game.GetKeyboardState(Key.RCONTROL)) Then
            If Game.Total2DObject > 0 Then
                For iii As Integer = 0 To Game.Total2DObject - 1
                    Dim dude As bd2DObject = Game.Get2DObject(iii)
                    If dude.GetTotalInstances(Game.CurrentLevel) > 0 Then
                        For jjj As Integer = 0 To dude.GetTotalInstances(Game.CurrentLevel) - 1
                            Dim ins As bd2DInstance = dude.GetInstance(jjj, Game.CurrentLevel)
                            If e.X >= ins.X And e.X <= ins.X + ins.Width And _
                                  e.Y >= ins.Y And e.Y <= ins.Y + ins.Height Then
                                ggg.Obj2DBox.SelectedIndex = iii
                                ggg.Obj2DBox_SelectedIndexChanged(ggg, Nothing)
                                ggg.Ins2DBox.SelectedIndex = jjj
                                ggg.Ins2DBox_SelectedIndexChanged(ggg, Nothing)
                                If e.Button = Windows.Forms.MouseButtons.Right Then

                                    dude.RemoveInstance(jjj, Game.CurrentLevel)
                                    Dim hhh As MainForm = MainForm
                                    Dim kkk As bdEachLevel = hhh.mstore.Levels(ggg.LevelIndex)
                                    Dim bishwas As Integer = kkk.Instances2DID.IndexOf(ggg.Ins2DBox.SelectedItem)
                                    kkk.Objects2DUsed.RemoveAt(bishwas)
                                    kkk.Instances2DID.RemoveAt(bishwas)
                                    kkk.X2Ds.RemoveAt(bishwas)
                                    kkk.Y2Ds.RemoveAt(bishwas)
                                    ggg.Ins2DBox.Items.RemoveAt(jjj)
                                    CurrentIns = Nothing
                                    Exit Sub
                                End If
                            End If
                        Next
                    End If
                Next
            End If
        Else
            If e.Button = Windows.Forms.MouseButtons.Left Then
                ggg.ToolStripButton8_Click(ggg, Nothing)
                If Not CurrentIns Is Nothing Then
                    CurrentIns.X = Math.Floor(e.X / ggg.XSnapBox.Text) * ggg.XSnapBox.Text
                    CurrentIns.Y = Math.Floor(e.Y / ggg.YSnapBox.Text) * ggg.YSnapBox.Text
                    Dim hhh As MainForm = MainForm
                    Dim kkk As bdEachLevel = hhh.mstore.Levels(ggg.LevelIndex)
                    Dim bishwas As Integer = kkk.Instances2DID.IndexOf(ggg.Ins2DBox.SelectedItem)
                    kkk.X2Ds(bishwas) = CurrentIns.X
                    kkk.Y2Ds(bishwas) = CurrentIns.Y
                End If
            End If
        End If
        Me.Focus()
    End Sub

    Private Sub MainEditor_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
        Dim ggg As LevelEditor = Me.ParentForm
        ggg.XLabel.Text = "Mouse X:" + (Math.Floor(e.X / ggg.XSnapBox.Text) * ggg.XSnapBox.Text).ToString
        ggg.YLabel.Text = "Mouse Y:" + (Math.Floor(e.Y / ggg.YSnapBox.Text) * ggg.YSnapBox.Text).ToString
    End Sub

    Private Sub Form1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Game.Render()
        Me.Invalidate()

    End Sub

    Public Sub MoveWallLeft()
        If SelectedWall < 0 Then Exit Sub
        WallC(SelectedWall).X -= 0.1
    End Sub
    Public Sub MoveWallRight()
        If SelectedWall < 0 Then Exit Sub
        WallC(SelectedWall).X += 0.1
    End Sub
    Public Sub MoveWallDown()
        If SelectedWall < 0 Then Exit Sub
        WallC(SelectedWall).Y -= 0.1
    End Sub
    Public Sub MoveWallUp()
        If SelectedWall < 0 Then Exit Sub
        WallC(SelectedWall).Y += 0.1
    End Sub
    Public Sub MoveWallFront()
        If SelectedWall < 0 Then Exit Sub
        WallC(SelectedWall).Z += 0.1
    End Sub
    Public Sub MoveWallBack()
        If SelectedWall < 0 Then Exit Sub
        WallC(SelectedWall).Z -= 0.1
    End Sub

    Public Sub MoveSndLeft()
        If SelectedSnd < 0 Then Exit Sub
        SndC(SelectedSnd).X -= 0.1
    End Sub
    Public Sub MoveSndRight()
        If SelectedSnd < 0 Then Exit Sub
        SndC(SelectedSnd).X += 0.1
    End Sub
    Public Sub MoveSndDown()
        If SelectedSnd < 0 Then Exit Sub
        SndC(SelectedSnd).Y -= 0.1
    End Sub
    Public Sub MoveSndUp()
        If SelectedSnd < 0 Then Exit Sub
        SndC(SelectedSnd).Y += 0.1
    End Sub
    Public Sub MoveSndFront()
        If SelectedSnd < 0 Then Exit Sub
        SndC(SelectedSnd).Z += 0.1
    End Sub
    Public Sub MoveSndBack()
        If SelectedSnd < 0 Then Exit Sub
        SndC(SelectedSnd).Z -= 0.1
    End Sub

    Public Sub MoveLeft(ByVal Move3D As Boolean)
        If Move3D = True Then
            If CurrentMesh Is Nothing Then Exit Sub
            CurrentMesh.X -= 0.1
            Dim ggg As LevelEditor = Me.ParentForm
            Dim hhh As MainForm = MainForm
            Dim kkk As bdEachLevel = hhh.mstore.Levels(ggg.LevelIndex)
            Dim bishwas As Integer = kkk.InstancesID.IndexOf(ggg.InsBox.SelectedItem)
            kkk.Xs(bishwas) = CurrentMesh.X
        Else
            If CurrentIns Is Nothing Then Exit Sub
            If Move2d = False Then Exit Sub
            Try
                Dim ggg As LevelEditor = Me.ParentForm
                CurrentIns.X -= Val(ggg.XSnapBox.Text)
                Dim hhh As MainForm = MainForm
                Dim kkk As bdEachLevel = hhh.mstore.Levels(ggg.LevelIndex)
                Dim bishwas As Integer = kkk.Instances2DID.IndexOf(ggg.Ins2DBox.SelectedItem)
                kkk.X2Ds(bishwas) = CurrentIns.X
                Move2d = False
            Catch
            End Try
        End If
    End Sub
    Public Sub MoveRight(ByVal Move3D As Boolean)
        If Move3D = True Then
            If CurrentMesh Is Nothing Then Exit Sub
            CurrentMesh.X += 0.1
            Dim ggg As LevelEditor = Me.ParentForm
            Dim hhh As MainForm = MainForm
            Dim kkk As bdEachLevel = hhh.mstore.Levels(ggg.LevelIndex)
            Dim bishwas As Integer = kkk.InstancesID.IndexOf(ggg.InsBox.SelectedItem)
            kkk.Xs(bishwas) = CurrentMesh.X
        Else
            If CurrentIns Is Nothing Then Exit Sub
            If Move2d = False Then Exit Sub
            Try
                Dim ggg As LevelEditor = Me.ParentForm
                CurrentIns.X += Val(ggg.XSnapBox.Text)
                Dim hhh As MainForm = MainForm
                Dim kkk As bdEachLevel = hhh.mstore.Levels(ggg.LevelIndex)
                Dim bishwas As Integer = kkk.Instances2DID.IndexOf(ggg.Ins2DBox.SelectedItem)
                kkk.X2Ds(bishwas) = CurrentIns.X
                Move2d = False
            Catch
            End Try
        End If
    End Sub
    Public Sub MoveDown(ByVal Move3D As Boolean)
        If Move3D = True Then
            If CurrentMesh Is Nothing Then Exit Sub
            CurrentMesh.Y -= 0.1
            Dim ggg As LevelEditor = Me.ParentForm
            Dim hhh As MainForm = MainForm
            Dim kkk As bdEachLevel = hhh.mstore.Levels(ggg.LevelIndex)
            Dim bishwas As Integer = kkk.InstancesID.IndexOf(ggg.InsBox.SelectedItem)
            kkk.Ys(bishwas) = CurrentMesh.Y
        Else
            If CurrentIns Is Nothing Then Exit Sub
            If Move2d = False Then Exit Sub
            Try
                Dim ggg As LevelEditor = Me.ParentForm
                CurrentIns.Y += Val(ggg.YSnapBox.Text)
                Dim hhh As MainForm = MainForm
                Dim kkk As bdEachLevel = hhh.mstore.Levels(ggg.LevelIndex)
                Dim bishwas As Integer = kkk.Instances2DID.IndexOf(ggg.Ins2DBox.SelectedItem)
                kkk.Y2Ds(bishwas) = CurrentIns.Y
                Move2d = False
            Catch
            End Try
        End If
    End Sub
    Public Sub MoveUp(ByVal Move3D As Boolean)
        If Move3D = True Then
            If CurrentMesh Is Nothing Then Exit Sub
            CurrentMesh.Y += 0.1
            Dim ggg As LevelEditor = Me.ParentForm
            Dim hhh As MainForm = MainForm
            Dim kkk As bdEachLevel = hhh.mstore.Levels(ggg.LevelIndex)
            Dim bishwas As Integer = kkk.InstancesID.IndexOf(ggg.InsBox.SelectedItem)
            kkk.Ys(bishwas) = CurrentMesh.Y
        Else
            If CurrentIns Is Nothing Then Exit Sub
            If Move2d = False Then Exit Sub
            Try
                Dim ggg As LevelEditor = Me.ParentForm
                CurrentIns.Y -= Val(ggg.YSnapBox.Text)
                Dim hhh As MainForm = MainForm
                Dim kkk As bdEachLevel = hhh.mstore.Levels(ggg.LevelIndex)
                Dim bishwas As Integer = kkk.Instances2DID.IndexOf(ggg.Ins2DBox.SelectedItem)
                kkk.Y2Ds(bishwas) = CurrentIns.Y
                Move2d = False
            Catch
            End Try
        End If
    End Sub
    Public Sub MoveFront()

        If CurrentMesh Is Nothing Then Exit Sub
        CurrentMesh.Z += 0.1
        Dim ggg As LevelEditor = Me.ParentForm
        Dim hhh As MainForm = MainForm
        Dim kkk As bdEachLevel = hhh.mstore.Levels(ggg.LevelIndex)
        Dim bishwas As Integer = kkk.InstancesID.IndexOf(ggg.InsBox.SelectedItem)
        kkk.Zs(bishwas) = CurrentMesh.Z
    End Sub
    Public Sub MoveBack()

        If CurrentMesh Is Nothing Then Exit Sub
        CurrentMesh.Z -= 0.1
        Dim ggg As LevelEditor = Me.ParentForm
        Dim hhh As MainForm = MainForm
        Dim kkk As bdEachLevel = hhh.mstore.Levels(ggg.LevelIndex)
        Dim bishwas As Integer = kkk.InstancesID.IndexOf(ggg.InsBox.SelectedItem)
        kkk.Zs(bishwas) = CurrentMesh.Z
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

    Public Sub CTargetLeft()
        Game.Camera.TargetX -= 0.1
    End Sub
    Public Sub CTargetRight()
        Game.Camera.TargetX += 0.1
    End Sub
    Public Sub CTargetUp()
        Game.Camera.TargetY += 0.1
    End Sub
    Public Sub CTargetDown()
        Game.Camera.TargetY -= 0.1
    End Sub


    'Public Sub RotateLeft()
    '    If CurrentMesh Is Nothing Then Exit Sub
    '    CurrentMesh.YAngle -= 1
    'End Sub
    'Public Sub RotateRight()
    '    If CurrentMesh Is Nothing Then Exit Sub
    '    CurrentMesh.YAngle += 1
    'End Sub
    'Public Sub RotateDown()
    '    If CurrentMesh Is Nothing Then Exit Sub
    '    CurrentMesh.ZAngle -= 1
    'End Sub
    'Public Sub RotateUp()
    '    If CurrentMesh Is Nothing Then Exit Sub
    '    CurrentMesh.ZAngle += 1
    'End Sub
    'Public Sub RotateForward()
    '    If CurrentMesh Is Nothing Then Exit Sub
    '    CurrentMesh.XAngle += 1
    'End Sub
    'Public Sub RotateBack()
    '    If CurrentMesh Is Nothing Then Exit Sub
    '    CurrentMesh.XAngle -= 1
    'End Sub

  
End Class