Imports System.Windows.Forms
Imports System
Imports System.IO
Imports System.CodeDom.Compiler



Public Class MainForm
    Public FileSaved As Boolean
    Public SaveName As String = "untitled.rg1"
    Public CurrentIcon As Icon

    Public Assemblies As New List(Of String)

    Public AnimatedMesh As Boolean = False

    Private Sub MainForm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If Me.WindowState = FormWindowState.Minimized Then Me.WindowState = FormWindowState.Maximized
        My.Settings.Left = Me.Left
        My.Settings.Top = Me.Top
        My.Settings.Height = Me.Height
        My.Settings.Width = Me.Width
        If Me.WindowState = FormWindowState.Maximized Then
            My.Settings.WindowMax = True
        Else
            My.Settings.WindowMax = False
        End If
        My.Settings.MnuFile1 = Me.RcntFile1ToolStripMenuItem.Text
        My.Settings.MnuFile2 = Me.RcntFile2ToolStripMenuItem.Text
        My.Settings.MnuFile3 = Me.RcntFile3ToolStripMenuItem.Text
        My.Settings.mnuFile4 = Me.RcntFile4ToolStripMenuItem.Text
        My.Settings.Save()

    End Sub
    Private Sub MainForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If My.Settings.WindowMax = True Then
            Me.WindowState = FormWindowState.Maximized
        Else
            Me.Top = My.Settings.Top
            Me.Left = My.Settings.Left
            Me.Width = My.Settings.Width
            Me.Height = My.Settings.Height
        End If

        Dim ctl As Control
        Dim ctlMDI As MdiClient

        ' Loop through all of the form's controls looking
        ' for the control of type MdiClient.
        For Each ctl In Me.Controls
            Try
                ' Attempt to cast the control to type MdiClient.
                ctlMDI = CType(ctl, MdiClient)

                ' Set the BackColor of the MdiClient control.
                ctlMDI.BackColor = Me.BackColor

                ctlMDI.BackgroundImage = Me.BackgroundImage
            Catch exc As InvalidCastException
                ' Catch and ignore the error if casting failed.
            End Try
        Next

        'ToolStrip.Renderer = New ToolStripProfessionalRenderer(New MS2007Colors())
        ' MenuStrip.Renderer = New ToolStripProfessionalRenderer(New MS2007Colors())
        ' StatusStrip.Renderer = New ToolStripProfessionalRenderer(New MS2007Colors())
        'ToolStrip1.Renderer = New ToolStripProfessionalRenderer(New MS2007Colors())
        ' ToolStrip2.Renderer = New ToolStripProfessionalRenderer(New MS2007Colors())
        
        LoadRcntMnus(My.Settings.MnuFile1, My.Settings.MnuFile2, My.Settings.MnuFile3, My.Settings.mnuFile4)
        NewFile(Me, Nothing)

        Me.ToolStripStatusLabel.Text = "Press F1 for help..."
    End Sub

    Private Sub LoadRcntMnus(ByVal File1 As String, ByVal File2 As String, ByVal File3 As String, ByVal File4 As String)
        Me.RcntFile1ToolStripMenuItem.Text = File1
        Me.RcntFile2ToolStripMenuItem.Text = File2
        Me.RcntFile3ToolStripMenuItem.Text = File3
        Me.RcntFile4ToolStripMenuItem.Text = File4
        Me.RcntFile1ToolStripMenuItem.Visible = True
        Me.RcntFile2ToolStripMenuItem.Visible = True
        Me.RcntFile3ToolStripMenuItem.Visible = True
        Me.RcntFile4ToolStripMenuItem.Visible = True
        If File1 = "" Then
            Me.RcntFile1ToolStripMenuItem.Visible = False
        End If
        If File2 = "" Then
            Me.RcntFile2ToolStripMenuItem.Visible = False
        End If
        If File3 = "" Then
            Me.RcntFile3ToolStripMenuItem.Visible = False
        End If
        If File4 = "" Then
            Me.RcntFile4ToolStripMenuItem.Visible = False
        End If
        If File1 = "" And File2 = "" And File3 = "" And File4 = "" Then
            Me.ToolStripSeparator4.Visible = False
        Else
            Me.ToolStripSeparator4.Visible = True
        End If
    End Sub
    Private Sub NewFile(ByVal sender As Object, ByVal e As EventArgs) Handles NewToolStripMenuItem.Click, NewToolStripButton.Click
        Assemblies.Clear()
        TreeView1.Nodes("MeshNode").Nodes.Clear()
        TreeView1.Nodes("ObjectNode").Nodes.Clear()
        TreeView1.Nodes("SpriteNode").Nodes.Clear()
        TreeView1.Nodes("Object2DNode").Nodes.Clear()
        TreeView1.Nodes("LevelNode").Nodes.Clear()
        TreeView1.Nodes("ClassNode").Nodes.Clear()
        TreeView1.SelectedNode = Nothing
        FullScreenBtn.Checked = False
        EscCancelBtn.Checked = True
        mstore = New MediaStore
        FileSaved = False
        CurrentIcon = My.Resources.Icon1
        IcoButton.Image = CurrentIcon.ToBitmap
        MediaBtn.Text = "Media"
        TitleButton.Text = "My Game"
        WidthButton.Text = "640"
        HeightButton.Text = "480"
        Me.CloseAllToolStripMenuItem_Click(Me, Nothing)
        SaveName = "untitled.rg1"
        Me.Text = "Reality Game Studio - " + SaveName

    End Sub

    Private Sub OpenFile(ByVal sender As Object, ByVal e As EventArgs) Handles OpenToolStripMenuItem.Click, OpenToolStripButton.Click
        Dim OpenFileDialog As New OpenFileDialog
        OpenFileDialog.Multiselect = False
        OpenFileDialog.Filter = "Reality Game Studio V1.0 Files |*.rg1"
        If (OpenFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
           
            Dim FileName As String = OpenFileDialog.FileName

            Open(FileName)

        End If
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SaveAsToolStripMenuItem.Click
        Dim SaveFileDialog As New SaveFileDialog
        SaveFileDialog.FileName = SaveName
        SaveFileDialog.Filter = "Reality Game Studio V1.0 Files |*.rg1"

        If (SaveFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            Dim FileName As String = SaveFileDialog.FileName

            Save(FileName)

        End If
    End Sub


    Private Sub SaveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripMenuItem.Click, SaveToolStripButton.Click
        If FileSaved = False Then
            SaveAsToolStripMenuItem_Click(Me, Nothing)
        Else
            Save(SaveName)
        End If
    End Sub


    Private Sub ExitToolsStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ExitToolStripMenuItem.Click
        Global.System.Windows.Forms.Application.Exit()
    End Sub


    Private Sub ToolBarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolBarToolStripMenuItem.Click
        Me.ToolStrip.Visible = Me.ToolBarToolStripMenuItem.Checked
    End Sub

    Private Sub StatusBarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles StatusBarToolStripMenuItem.Click
        Me.StatusStrip.Visible = Me.StatusBarToolStripMenuItem.Checked
    End Sub

    Private Sub CascadeToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CascadeToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.Cascade)
    End Sub

    Private Sub TileVerticleToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TileVerticalToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.TileVertical)
    End Sub

    Private Sub TileHorizontalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TileHorizontalToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.TileHorizontal)
    End Sub

    Private Sub ArrangeIconsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ArrangeIconsToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.ArrangeIcons)
    End Sub

    Private Sub CloseAllToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CloseAllToolStripMenuItem.Click
        ' Close all child forms of the parent.
        For Each ChildForm As Form In Me.MdiChildren
            ChildForm.Close()
        Next
    End Sub

    Private m_ChildFormNumber As Integer = 0

    Public mstore As New MediaStore
    Public CurrentMeshView As Byte()

    Public CurrentMeshXAngle As Single
    Public CurrentMeshYAngle As Single
    Public CurrentMeshZAngle As Single
    Public CurrentMeshScale As Single

    Public CurrentEventIndex As Integer
    Public CurrentActionIndex As Integer

    Private Sub Add_Mesh(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddMeshToolStripMenuItem.Click, ToolStripButton4.Click
        Dim opener As New OpenFileDialog
        opener.Multiselect = True
        opener.Filter = "DirectX Mesh Files| *.x"
        If opener.ShowDialog = Windows.Forms.DialogResult.OK Then
            For Each fname As String In opener.FileNames
                Try
                    mstore.AddMesh("Mesh" + Trim(Str(mstore.MeshName.Count + 1)), 0, 0, 0, fname)
                    Dim ad As MeshEditor = New MeshEditor(mstore.MeshName.Count - 1)
                    ad.MdiParent = Me
                    ad.Show()
                    TreeView1.Nodes("MeshNode").Nodes.Add("Mesh" + Trim(Str(mstore.MeshName.Count)))
                    TreeView1.Nodes("MeshNode").Expand()
                Catch
                    MsgBox("Cannot add mesh from file """ + fname + """.")
                End Try
            Next
        End If

    End Sub


    Private Sub Add_Object(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddObjectToolStripMenuItem.Click, ToolStripButton6.Click
        mstore.Add3DObject("Object3D" + Trim(Str(mstore.Object3DName.Count + 1)), "")
        Dim ad As ObjectEditor = New ObjectEditor(mstore.Object3DName.Count - 1)
        ad.MdiParent = Me
        ad.Show()
        TreeView1.Nodes("ObjectNode").Nodes.Add("Object3D" + Trim(Str(mstore.Object3DName.Count)))
        TreeView1.Nodes("ObjectNode").Expand()

    End Sub


    Private Sub Add_Level(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddLevelToolStripMenuItem.Click, ToolStripButton8.Click
        mstore.Levels.Add(New bdEachLevel)
        mstore.Levels(mstore.Levels.Count - 1).LevelName = "Level" + Trim(Str(mstore.Levels.Count))
        TreeView1.Nodes("LevelNode").Nodes.Add("Level" + Trim(Str(mstore.Levels.Count)))
        TreeView1.Nodes("LevelNode").Expand()
        Dim ad As LevelEditor = New LevelEditor(mstore.Levels.Count - 1)
        ad.MdiParent = Me
        ad.Show()

    End Sub


    Private Sub Save(ByVal FileName As String)
        ProgressBar1.Visible = True
        ProgressBar1.Minimum = 0
        ProgressBar1.Maximum = 17
        ProgressBar1.Step = 1
        ProgressBar1.Value = 0
        Dim fs As New FileStream(FileName, FileMode.Create, FileAccess.Write)
        Dim w As New BinaryWriter(fs)
        ProgressBar1.PerformStep()
        w.Write("'Do not modify this file at all manually unless you want to risk the loss of data in file")
        w.Write("----Meshes")
        w.Write(mstore.MeshName.Count)
        If mstore.MeshName.Count <> 0 Then
            For iii As Integer = 0 To mstore.MeshName.Count - 1
                w.Write(mstore.MeshName(iii))
                w.Write(mstore.MeshText(iii).Length)
                w.Write("[start]")
                w.Write(mstore.MeshText(iii))
                w.Write("[stop]")
                w.Write(mstore.MeshXOrig(iii))
                w.Write(mstore.MeshYOrig(iii))
                w.Write(mstore.MeshZOrig(iii))

                w.Write(mstore.MeshXAngle(iii))
                w.Write(mstore.MeshYAngle(iii))
                w.Write(mstore.MeshZAngle(iii))

                w.Write(mstore.MeshScale(iii))

                w.Write(mstore.TextureLists(iii).TextureName.Count)
                If mstore.TextureLists(iii).TextureName.Count <> 0 Then
                    For firenze As Integer = 0 To mstore.TextureLists(iii).TextureName.Count - 1
                        w.Write(mstore.TextureLists(iii).TextureName(firenze))
                        w.Write(mstore.TextureLists(iii).TextureText(firenze).Length)
                        w.Write(mstore.TextureLists(iii).TextureText(firenze))
                    Next
                End If

                'for shperes
                w.Write(mstore.CollisionSpheres(iii).NotYet)
                w.Write(mstore.CollisionSpheres(iii).Spheres.Count)
                If mstore.CollisionSpheres(iii).Spheres.Count <> 0 Then
                    For hagr As Integer = 0 To mstore.CollisionSpheres(iii).Spheres.Count - 1
                        w.Write(mstore.CollisionSpheres(iii).Spheres(hagr).CenterX)
                        w.Write(mstore.CollisionSpheres(iii).Spheres(hagr).CenterY)
                        w.Write(mstore.CollisionSpheres(iii).Spheres(hagr).CenterZ)
                        w.Write(mstore.CollisionSpheres(iii).Spheres(hagr).Radius)
                    Next
                End If
                'end spheres

                'for boxes
                w.Write(mstore.CollisionBoxes(iii).NotYet)
                w.Write(mstore.CollisionBoxes(iii).Boxes.Count)
                If mstore.CollisionBoxes(iii).Boxes.Count <> 0 Then
                    For hagr As Integer = 0 To mstore.CollisionBoxes(iii).Boxes.Count - 1
                        w.Write(mstore.CollisionBoxes(iii).Boxes(hagr).MinX)
                        w.Write(mstore.CollisionBoxes(iii).Boxes(hagr).MinY)
                        w.Write(mstore.CollisionBoxes(iii).Boxes(hagr).MinZ)
                        w.Write(mstore.CollisionBoxes(iii).Boxes(hagr).MaxX)
                        w.Write(mstore.CollisionBoxes(iii).Boxes(hagr).MaxY)
                        w.Write(mstore.CollisionBoxes(iii).Boxes(hagr).MaxZ)
                    Next
                End If
                'end boxes

                w.Write(mstore.Animated(iii))

                w.Write("--------------------------------------")
            Next
        End If
        ProgressBar1.PerformStep()
        'Some Settings
        w.Write(FullScreenBtn.Checked)
        w.Write(EscCancelBtn.Checked)

        w.Write(TitleButton.Text)

        w.Write(WidthButton.Text)
        w.Write(HeightButton.Text)

        w.Write(Me.MediaBtn.Text)

        w.Write(Assemblies.Count)
        If Assemblies.Count > 0 Then
            For i As Integer = 0 To Assemblies.Count - 1
                w.Write(Assemblies(i))
            Next
        End If

        ProgressBar1.PerformStep()

        Dim bn As New Bitmap(IcoButton.Image)
        Dim fffs As New FileStream(Application.StartupPath + "\Icon1.ico", FileMode.OpenOrCreate)
        My.Computer.FileSystem.GetFileInfo(Application.StartupPath + "\Icon1.ico").Attributes = FileAttributes.Hidden
        CurrentIcon.Save(fffs)
        fffs.Close()

        Dim infile As New FileStream(Application.StartupPath + "\Icon1.ico", FileMode.Open, FileAccess.Read, FileShare.Read)
        Dim buffer(infile.Length - 1) As Byte
        Dim count As Integer = infile.Read(buffer, 0, buffer.Length)
        If count <> buffer.Length Then
            infile.Close()
            MsgBox("Bad File")
            Return
        End If
        infile.Close()

        w.Write(buffer.Length)
        w.Write(buffer)

        My.Computer.FileSystem.DeleteFile(Application.StartupPath + "\Icon1.ico")

        ProgressBar1.PerformStep()

        w.Write("----Sprite")
        w.Write(mstore.Sprites.Count)
        If mstore.Sprites.Count <> 0 Then
            For iii As Integer = 0 To mstore.Sprites.Count - 1
                w.Write(mstore.SpriteNames(iii))
                w.Write(mstore.Sprites(iii).ImageNames.Count)
                If mstore.Sprites(iii).ImageNames.Count <> 0 Then
                    For tail As Integer = 0 To mstore.Sprites(iii).ImageNames.Count - 1
                        w.Write(mstore.Sprites(iii).ImageNames(tail))
                        w.Write(mstore.Sprites(iii).ImageTexts(tail).Length)
                        w.Write("[start]")
                        w.Write(mstore.Sprites(iii).ImageTexts(tail))
                        w.Write("[stop]")
                    Next
                End If
                'w.Write(mstore.SpriteXOrigins(iii))
                'w.Write(mstore.SpriteYOrigins(iii))
                w.Write(mstore.Transparent(iii))
                w.Write(mstore.TransparentColor(iii).ToArgb)
                w.Write("--------------------------------------")
            Next
        End If
        ProgressBar1.PerformStep()
        w.Write("----Music")
        w.Write(mstore.MusicName.Count)
        If mstore.MusicName.Count <> 0 Then
            For iii As Integer = 0 To mstore.MusicName.Count - 1
                w.Write(mstore.MusicName(iii))
                w.Write(mstore.MusicText(iii).Length)
                w.Write(mstore.MusicText(iii))
            Next
        End If

        ProgressBar1.PerformStep()
        w.Write("----3DObjects")
        w.Write(mstore.Object3DName.Count)
        If mstore.Object3DName.Count <> 0 Then
            For jjj As Integer = 0 To mstore.Object3DName.Count - 1
                w.Write(mstore.Object3DName(jjj))
                w.Write(mstore.Object3DMesh(jjj))
                w.Write("--------------------------------------")
            Next
        End If
        ProgressBar1.PerformStep()
        w.Write("----2DObjects")
        w.Write(mstore.Object2DName.Count)
        If mstore.Object2DName.Count <> 0 Then
            For jjj As Integer = 0 To mstore.Object2DName.Count - 1
                w.Write(mstore.Object2DName(jjj))
                w.Write(mstore.Object2DSprite(jjj))
                w.Write("--------------------------------------")
            Next
        End If
        ProgressBar1.PerformStep()
        w.Write("----2DSounds")
        w.Write(mstore.Sound2DNames.Count)
        If mstore.Sound2DNames.Count <> 0 Then
            For kurkucha As Integer = 0 To mstore.Sound2DNames.Count - 1
                w.Write(mstore.Sound2DTexts(kurkucha).Length)
                w.Write(mstore.Sound2DTexts(kurkucha))
                w.Write(mstore.Sound2DNames(kurkucha))
            Next
        End If
        ProgressBar1.PerformStep()
        w.Write("----3DSounds")
        w.Write(mstore.Sound3DLevels.Count)
        If mstore.Sound3DLevels.Count <> 0 Then
            For kurkucha As Integer = 0 To mstore.Sound3DLevels.Count - 1
                w.Write(mstore.Sound3DTexts(kurkucha).Length)
                w.Write(mstore.Sound3DTexts(kurkucha))
                w.Write(mstore.Sound3DLevels(kurkucha))
                w.Write(mstore.Sound3DNames(kurkucha))
                w.Write(mstore.Sound3DXs(kurkucha))
                w.Write(mstore.Sound3DYs(kurkucha))
                w.Write(mstore.Sound3DZs(kurkucha))
            Next
        End If
        ProgressBar1.PerformStep()
        w.Write("----Levels")
        w.Write(mstore.Levels.Count)
        If mstore.Levels.Count <> 0 Then
            For kkk As Integer = 0 To mstore.Levels.Count - 1
                w.Write(mstore.Levels(kkk).LevelName)
                Dim done As bdEachLevel = mstore.Levels(kkk)
                w.Write(done.InstancesID.Count)
                If done.InstancesID.Count <> 0 Then
                    For ddd As Integer = 0 To done.InstancesID.Count - 1
                        w.Write("[StartIS]")
                        w.Write(done.InstancesID(ddd))
                        w.Write(done.ObjectsUsed(ddd))
                        w.Write(done.Xs(ddd))
                        w.Write(done.Ys(ddd))
                        w.Write(done.Zs(ddd))
                        w.Write("[StopIS]")
                    Next
                End If
                w.Write(done.Instances2DID.Count)
                If done.Instances2DID.Count <> 0 Then
                    For ddd As Integer = 0 To done.Instances2DID.Count - 1
                        w.Write("[StartIS]")
                        w.Write(done.Instances2DID(ddd))
                        w.Write(done.Objects2DUsed(ddd))
                        w.Write(done.X2Ds(ddd))
                        w.Write(done.Y2Ds(ddd))
                        w.Write("[StopIS]")
                    Next
                End If
                w.Write(done.BoxScales.Count)
                If done.BoxScales.Count <> 0 Then
                    For bow As Integer = 0 To done.BoxScales.Count - 1
                        w.Write(done.BoxXs(bow))
                        w.Write(done.BoxYs(bow))
                        w.Write(done.BoxZs(bow))
                        w.Write(done.BoxXAngles(bow))
                        w.Write(done.BoxYAngles(bow))
                        w.Write(done.BoxZAngles(bow))
                        w.Write(done.BoxScales(bow))
                        w.Write(done.BoxWidths(bow))
                        w.Write(done.BoxHeights(bow))
                        w.Write(done.BoxDepths(bow))
                    Next
                End If
                w.Write("--------------------------------------")
            Next
        End If
        ProgressBar1.PerformStep()
        w.Write("----Events and Actions")
        w.Write(mstore.GetTotalEvents)
        If mstore.GetTotalEvents <> 0 Then
            For cow As Integer = 0 To mstore.GetTotalEvents - 1
                Dim dog As bdEvent = mstore.GetEvent(cow)
                w.Write(dog.EventName)
                w.Write(dog.EventHandler)
                w.Write(dog.EventArguments)
                w.Write(dog.ActionsCollection.Count)
                If dog.ActionsCollection.Count <> 0 Then
                    For hello As Integer = 0 To dog.ActionsCollection.Count - 1
                        w.Write(dog.ActionsCollection(hello))
                    Next
                End If
            Next
        End If
        ProgressBar1.PerformStep()
        If mstore.MoreCode Is Nothing Then mstore.MoreCode = ""
        w.Write("----More Code")
        w.Write(mstore.MoreCode)
        ProgressBar1.PerformStep()

        w.Write("----Classes")
        w.Write(mstore.ClassNames.Count)
        If mstore.ClassNames.Count <> 0 Then
            For Each pandey As String In mstore.ClassNames
                w.Write(pandey)
                w.Write(mstore.ClassTexts(mstore.ClassNames.IndexOf(pandey)))
            Next
        End If
        ProgressBar1.PerformStep()
        w.Close()
        fs.Close()
        ProgressBar1.PerformStep()
        FileSaved = True
        SaveName = FileName
        If Me.RcntFile1ToolStripMenuItem.Text = FileName Then
            'do nothing
        ElseIf Me.RcntFile2ToolStripMenuItem.Text = FileName Then
            Me.LoadRcntMnus(FileName, _
                            Me.RcntFile1ToolStripMenuItem.Text, _
                            Me.RcntFile3ToolStripMenuItem.Text, _
                            Me.RcntFile4ToolStripMenuItem.Text)
        ElseIf Me.RcntFile3ToolStripMenuItem.Text = FileName Then
            Me.LoadRcntMnus(FileName, _
                            Me.RcntFile1ToolStripMenuItem.Text, _
                            Me.RcntFile2ToolStripMenuItem.Text, _
                            Me.RcntFile4ToolStripMenuItem.Text)
        Else
            Me.LoadRcntMnus(FileName, _
                            Me.RcntFile1ToolStripMenuItem.Text, _
                            Me.RcntFile2ToolStripMenuItem.Text, _
                            Me.RcntFile3ToolStripMenuItem.Text)
        End If
        Me.Text = "Reality Game Studio - " + SaveName
        ProgressBar1.Value = 17
        Me.Refresh()

        ProgressBar1.Visible = False
    End Sub
    Private Sub Open(ByVal FileName As String)

        ProgressBar1.Visible = True
        ProgressBar1.Minimum = 0
        ProgressBar1.Maximum = 17
        ProgressBar1.Step = 1
        ProgressBar1.Value = 0

        Try
            TreeView1.Nodes("MeshNode").Nodes.Clear()
            TreeView1.Nodes("ObjectNode").Nodes.Clear()
            TreeView1.Nodes("Object2DNode").Nodes.Clear()
            TreeView1.Nodes("SpriteNode").Nodes.Clear()
            TreeView1.Nodes("LevelNode").Nodes.Clear()
            TreeView1.Nodes("ClassNode").Nodes.Clear()
            mstore = New MediaStore
            ProgressBar1.PerformStep()
            Dim fs As New FileStream(FileName, FileMode.Open, FileAccess.Read)
            Dim w As New BinaryReader(fs)
            Dim dummy As String
            dummy = w.ReadString
            dummy = w.ReadString

            mstore.MeshName.Clear()
            mstore.MeshText.Clear()
            mstore.MeshXOrig.Clear()
            mstore.MeshYOrig.Clear()
            mstore.MeshZOrig.Clear()
            mstore.TextureLists.Clear()
            ProgressBar1.PerformStep()
            Dim meshcount As Integer = w.ReadInt32
            If meshcount <> 0 Then
                For lld As Integer = 0 To meshcount - 1

                    mstore.MeshName.Add(w.ReadString())
                    Dim sss As Integer = w.ReadInt32
                    dummy = w.ReadString
                    mstore.MeshText.Add(w.ReadBytes(sss))
                    dummy = w.ReadString
                    mstore.MeshXOrig.Add(w.ReadSingle)
                    mstore.MeshYOrig.Add(w.ReadSingle)
                    mstore.MeshZOrig.Add(w.ReadSingle)

                    mstore.MeshXAngle.Add(w.ReadSingle)
                    mstore.MeshYAngle.Add(w.ReadSingle)
                    mstore.MeshZAngle.Add(w.ReadSingle)

                    mstore.MeshScale.Add(w.ReadSingle)

                    mstore.TextureLists.Add(New TextureList)
                    Dim textureCount As Integer = w.ReadInt32
                    If textureCount <> 0 Then
                        For trenalew As Integer = 0 To textureCount - 1
                            mstore.TextureLists(lld).TextureName.Add(w.ReadString)
                            Dim kookoo As Integer = w.ReadInt32
                            mstore.TextureLists(lld).TextureText.Add(w.ReadBytes(kookoo))
                        Next
                    End If

                    'Spheres
                    mstore.CollisionSpheres.Add(New CollisionSphere)
                    mstore.CollisionSpheres(lld).NotYet = w.ReadBoolean
                    Dim csbCount As Integer = w.ReadInt32
                    If csbCount <> 0 Then
                        For hagr As Integer = 0 To csbCount - 1
                            Dim aragog As New CollisionSphere.CollisionSpheres
                            aragog.CenterX = w.ReadSingle
                            aragog.CenterY = w.ReadSingle
                            aragog.CenterZ = w.ReadSingle
                            aragog.Radius = w.ReadSingle

                            mstore.CollisionSpheres(lld).Spheres.Add(aragog)

                        Next
                    End If
                    'End Spheres

                    'Boxes
                    mstore.CollisionBoxes.Add(New CollisionBox)
                    mstore.CollisionBoxes(lld).NotYet = w.ReadBoolean
                    Dim cbCount As Integer = w.ReadInt32
                    If cbCount <> 0 Then
                        For hagr As Integer = 0 To cbCount - 1
                            Dim aragog As New CollisionBox.CollisionBoxes
                            aragog.MinX = w.ReadSingle
                            aragog.MinY = w.ReadSingle
                            aragog.MinZ = w.ReadSingle
                            aragog.MaxX = w.ReadSingle
                            aragog.MaxY = w.ReadSingle
                            aragog.MaxZ = w.ReadSingle

                            mstore.CollisionBoxes(lld).Boxes.Add(aragog)

                        Next
                    End If
                    'End Boxes
                    mstore.Animated.Add(w.ReadBoolean)

                    dummy = w.ReadString
                Next
            End If
            ProgressBar1.PerformStep()
            Dim fullScreenSt As Boolean = w.ReadBoolean
            Dim CancelEscSt As Boolean = w.ReadBoolean
            Dim TitleText As String = w.ReadString
            Dim widthText As String = w.ReadString
            Dim heightText As String = w.ReadString

            MediaBtn.Text = w.ReadString
            Assemblies.Clear()
            Dim asc As Integer = w.ReadInt32
            If asc > 0 Then
                For i As Integer = 0 To asc - 1
                    Assemblies.Add(w.ReadString)
                Next
            End If
            ProgressBar1.PerformStep()

            Dim bl As Integer = w.ReadInt32
            Dim buffer As Byte() = w.ReadBytes(bl)

            My.Computer.FileSystem.WriteAllBytes(Application.StartupPath + "\Icon.ico", buffer, False)
            Dim ico As New Icon(Application.StartupPath + "\Icon.ico")

            Me.IcoButton.Image = ico.ToBitmap
            CurrentIcon = ico
            My.Computer.FileSystem.DeleteFile(Application.StartupPath + "\Icon.ico")

            ProgressBar1.PerformStep()

            dummy = w.ReadString
            mstore.Sprites.Clear()
            mstore.SpriteNames.Clear()
            Dim mudblood As Integer = w.ReadInt32
            If mudblood <> 0 Then
                For iii As Integer = 0 To mudblood - 1
                    mstore.SpriteNames.Add(w.ReadString)
                    mstore.Sprites.Add(New bdSpriteStore)
                    Dim pureblood As Integer = w.ReadInt32
                    If pureblood <> 0 Then
                        For tail As Integer = 0 To pureblood - 1
                            mstore.Sprites(iii).ImageNames.Add(w.ReadString)
                            Dim halfblood As Integer = w.ReadInt32
                            dummy = w.ReadString
                            mstore.Sprites(iii).ImageTexts.Add(w.ReadBytes(halfblood))
                            dummy = w.ReadString

                        Next
                    End If
                    mstore.Transparent.Add(w.ReadBoolean)
                    mstore.TransparentColor.Add(Color.FromArgb(w.ReadInt32))
                    dummy = w.ReadString
                Next
            End If
            ProgressBar1.PerformStep()
            dummy = w.ReadString
            mstore.MusicName.Clear()
            mstore.MusicText.Clear()
            Dim mscCount As Integer = w.ReadInt32
            If mscCount <> 0 Then
                For log As Integer = 0 To mscCount - 1
                    mstore.MusicName.Add(w.ReadString)
                    Dim mscLng As Integer = w.ReadInt32
                    mstore.MusicText.Add(w.ReadBytes(mscLng))
                Next
            End If
            ProgressBar1.PerformStep()
            dummy = w.ReadString
            mstore.Object3DMesh.Clear()
            mstore.Object3DName.Clear()
            Dim objcount As Integer = w.ReadInt32
            If objcount <> 0 Then
                For log As Integer = 0 To objcount - 1

                    mstore.Object3DName.Add(w.ReadString)
                    mstore.Object3DMesh.Add(w.ReadString)
                    dummy = w.ReadString
                Next
            End If
            ProgressBar1.PerformStep()
            dummy = w.ReadString
            mstore.Object2DSprite.Clear()
            mstore.Object2DName.Clear()
            Dim obj2Dcount As Integer = w.ReadInt32
            If obj2Dcount <> 0 Then
                For log As Integer = 0 To obj2Dcount - 1

                    mstore.Object2DName.Add(w.ReadString)
                    mstore.Object2DSprite.Add(w.ReadString)
                    dummy = w.ReadString
                Next
            End If
            ProgressBar1.PerformStep()
            dummy = w.ReadString
            Dim snd2dcount As Integer = w.ReadInt32
            If snd2dcount <> 0 Then
                For kurkucha As Integer = 0 To snd2dcount - 1
                    Dim ln2d As Integer = w.ReadInt32
                    mstore.Sound2DTexts.Add(w.ReadBytes(ln2d))
                    mstore.Sound2DNames.Add(w.ReadString)
                Next
            End If
            ProgressBar1.PerformStep()
            dummy = w.ReadString
            Dim snd3dcount As Integer = w.ReadInt32
            If snd3dcount <> 0 Then
                For kurkucha As Integer = 0 To snd3dcount - 1
                    Dim ln3d As Integer = w.ReadInt32
                    mstore.Sound3DTexts.Add(w.ReadBytes(ln3d))
                    mstore.Sound3DLevels.Add(w.ReadInt32)
                    mstore.Sound3DNames.Add(w.ReadString)
                    mstore.Sound3DXs.Add(w.ReadSingle)
                    mstore.Sound3DYs.Add(w.ReadSingle)
                    mstore.Sound3DZs.Add(w.ReadSingle)
                Next
            End If

            ProgressBar1.PerformStep()
            mstore.Levels.Clear()

            dummy = w.ReadString
            Dim levelcount As Integer = w.ReadInt32
            If levelcount <> 0 Then
                For levelid As Integer = 0 To levelcount - 1
                    mstore.Levels.Add(New bdEachLevel)
                    mstore.Levels(levelid).LevelName = w.ReadString
                    Dim inscount As Integer = w.ReadInt32
                    If inscount <> 0 Then
                        For insid As Integer = 0 To inscount - 1

                            dummy = w.ReadString
                            mstore.Levels(levelid).InstancesID.Add(w.ReadString)
                            mstore.Levels(levelid).ObjectsUsed.Add(w.ReadString)
                            mstore.Levels(levelid).Xs.Add(w.ReadSingle)
                            mstore.Levels(levelid).Ys.Add(w.ReadSingle)
                            mstore.Levels(levelid).Zs.Add(w.ReadSingle)
                            dummy = w.ReadString
                        Next
                    End If
                    Dim ins2Dcount As Integer = w.ReadInt32
                    If ins2Dcount <> 0 Then
                        For insid As Integer = 0 To ins2Dcount - 1

                            dummy = w.ReadString
                            mstore.Levels(levelid).Instances2DID.Add(w.ReadString)
                            mstore.Levels(levelid).Objects2DUsed.Add(w.ReadString)
                            mstore.Levels(levelid).X2Ds.Add(w.ReadSingle)
                            mstore.Levels(levelid).Y2Ds.Add(w.ReadSingle)
                            dummy = w.ReadString
                        Next
                    End If
                    Dim wallCount As Integer = w.ReadInt32
                    If wallCount <> 0 Then
                        For wallId As Integer = 0 To wallCount - 1
                            mstore.Levels(levelid).BoxXs.Add(w.ReadSingle)
                            mstore.Levels(levelid).BoxYs.Add(w.ReadSingle)
                            mstore.Levels(levelid).BoxZs.Add(w.ReadSingle)
                            mstore.Levels(levelid).BoxXAngles.Add(w.ReadSingle)
                            mstore.Levels(levelid).BoxYAngles.Add(w.ReadSingle)
                            mstore.Levels(levelid).BoxZAngles.Add(w.ReadSingle)
                            mstore.Levels(levelid).BoxScales.Add(w.ReadSingle)
                            mstore.Levels(levelid).BoxWidths.Add(w.ReadSingle)
                            mstore.Levels(levelid).BoxHeights.Add(w.ReadSingle)
                            mstore.Levels(levelid).BoxDepths.Add(w.ReadSingle)
                        Next
                    End If
                    dummy = w.ReadString
                Next
            End If
            ProgressBar1.PerformStep()
            dummy = w.ReadString

            Dim EvntCount As Integer = w.ReadInt32
            If EvntCount <> 0 Then
                For how As Integer = 0 To EvntCount - 1
                    mstore.AddEvent(w.ReadString)
                    Dim cow As bdEvent = mstore.GetEvent(mstore.GetTotalEvents - 1)
                    cow.EventHandler = w.ReadString
                    cow.EventArguments = w.ReadString
                    Dim actioncount As Integer = w.ReadInt32
                    If actioncount <> 0 Then
                        For iowwoi As Integer = 0 To actioncount - 1
                            Dim huhow As String = w.ReadString
                            cow.ActionsCollection.Add(huhow)
                            cow.ActionsTypes.Add(cow.GetActionType(huhow))
                        Next
                    End If

                Next
            End If
            ProgressBar1.PerformStep()
            dummy = w.ReadString
            mstore.MoreCode = w.ReadString
            ProgressBar1.PerformStep()
            dummy = w.ReadString
            Dim clsCount As Integer = w.ReadInt32
            If clsCount <> 0 Then
                For pandu As Integer = 0 To clsCount - 1
                    mstore.ClassNames.Add(w.ReadString)
                    mstore.ClassTexts.Add(w.ReadString)
                Next
            End If
            w.Close()
            fs.Close()
            ProgressBar1.PerformStep()
            Me.CloseAllToolStripMenuItem_Click(Me, Nothing)

            For Each meshstr As String In mstore.MeshName
                TreeView1.Nodes("meshNode").Nodes.Add(meshstr)
                TreeView1.Nodes("meshNode").Expand()
            Next
            For Each sprstr As String In mstore.SpriteNames
                TreeView1.Nodes("SpriteNode").Nodes.Add(sprstr)
                TreeView1.Nodes("SpriteNode").Expand()
            Next
            For Each objstr As String In mstore.Object3DName
                TreeView1.Nodes("ObjectNode").Nodes.Add(objstr)
                TreeView1.Nodes("ObjectNode").Expand()
            Next
            For Each obj2Dstr As String In mstore.Object2DName
                TreeView1.Nodes("Object2DNode").Nodes.Add(obj2Dstr)
                TreeView1.Nodes("Object2DNode").Expand()
            Next
            For iooi As Integer = 0 To mstore.Levels.Count - 1
                TreeView1.Nodes("LevelNode").Nodes.Add(mstore.Levels(iooi).LevelName)
                TreeView1.Nodes("LevelNode").Expand()
            Next

            For Each gas As String In mstore.ClassNames
                TreeView1.Nodes("ClassNode").Nodes.Add(gas)
                TreeView1.Nodes("ClassNode").Expand()
            Next
            ProgressBar1.PerformStep()
            FileSaved = True
            SaveName = FileName
            If Me.RcntFile1ToolStripMenuItem.Text = FileName Then
                'do nothing
            ElseIf Me.RcntFile2ToolStripMenuItem.Text = FileName Then
                Me.LoadRcntMnus(FileName, _
                                Me.RcntFile1ToolStripMenuItem.Text, _
                                Me.RcntFile3ToolStripMenuItem.Text, _
                                Me.RcntFile4ToolStripMenuItem.Text)
            ElseIf Me.RcntFile3ToolStripMenuItem.Text = FileName Then
                Me.LoadRcntMnus(FileName, _
                                Me.RcntFile1ToolStripMenuItem.Text, _
                                Me.RcntFile2ToolStripMenuItem.Text, _
                                Me.RcntFile4ToolStripMenuItem.Text)
            Else
                Me.LoadRcntMnus(FileName, _
                                Me.RcntFile1ToolStripMenuItem.Text, _
                                Me.RcntFile2ToolStripMenuItem.Text, _
                                Me.RcntFile3ToolStripMenuItem.Text)
            End If
            Me.Text = "Reality Game Studio - " + SaveName
            FullScreenBtn.Checked = fullScreenSt
            EscCancelBtn.Checked = CancelEscSt
            TitleButton.Text = TitleText
            WidthButton.Text = widthText
            HeightButton.Text = heightText
            ProgressBar1.Value = 17
            ProgressBar1.Visible = False

        Catch ex As Exception
            ProgressBar1.Visible = False
            MsgBox("Error opening file: " + FileName)
            NewFile(Me, Nothing)
        End Try

    End Sub

    Private Sub CtrlBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GameControllerToolStripMenuItem.Click
        Dim alreadyOpened As Boolean = False
        For Each ggg As Form In Me.MdiChildren
            If ggg.Text.StartsWith("Game Controller") Then
                Dim kkk As GameEditor = ggg
                kkk.Focus()
                alreadyOpened = True
            End If
        Next
        If alreadyOpened = False Then
            Dim god As New GameEditor
            god.MdiParent = Me
            god.Show()
        End If

    End Sub

    Private Sub Remove_Mesh(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveMeshToolStripMenuItem.Click, ToolStripButton5.Click
        Try
            If SelectedNode Is Nothing Then Exit Sub
            If Not (SelectedNode.Parent.Name = "MeshNode") Then Exit Sub
            If MsgBox("Are you sure to remove the mesh you selected?", MsgBoxStyle.YesNo, "Reality Game Studio") = MsgBoxResult.No Then Exit Sub
            mstore.MeshName.RemoveAt(SelectedNode.Index)
            mstore.MeshText.RemoveAt(SelectedNode.Index)
            mstore.MeshXOrig.RemoveAt(SelectedNode.Index)
            mstore.MeshYOrig.RemoveAt(SelectedNode.Index)
            mstore.MeshZOrig.RemoveAt(SelectedNode.Index)
            mstore.MeshXAngle.RemoveAt(SelectedNode.Index)
            mstore.MeshYAngle.RemoveAt(SelectedNode.Index)
            mstore.MeshZAngle.RemoveAt(SelectedNode.Index)
            mstore.MeshScale.RemoveAt(SelectedNode.Index)
            mstore.CollisionSpheres.RemoveAt(SelectedNode.Index)
            mstore.TextureLists.RemoveAt(SelectedNode.Index)
            TreeView1.Nodes("MeshNode").Nodes.RemoveAt(SelectedNode.Index)
        Catch
        End Try

    End Sub

    Private Sub Remove_Object(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveObjectToolStripMenuItem.Click, ToolStripButton7.Click
        Try
            If SelectedNode Is Nothing Then Exit Sub
            If Not (SelectedNode.Parent.Name = "ObjectNode") Then Exit Sub
            If MsgBox("Are you sure to remove the 3D object you selected?", MsgBoxStyle.YesNo, "Reality Game Studio") = MsgBoxResult.No Then Exit Sub
            mstore.Object3DMesh.RemoveAt(SelectedNode.Index)
            mstore.Object3DName.RemoveAt(SelectedNode.Index)
            For Each gau As bdEachLevel In mstore.Levels
                For Each gum As String In gau.ObjectsUsed
                    If gum = SelectedNode.Text Then
                        Dim crow As Integer = gau.ObjectsUsed.IndexOf(gum)
                        gau.InstancesID.RemoveAt(crow)
                        gau.Xs.RemoveAt(crow)
                        gau.Ys.RemoveAt(crow)
                        gau.Zs.RemoveAt(crow)

                        gau.ObjectsUsed.Remove(gum)

                    End If
                Next
            Next
            TreeView1.Nodes("ObjectNode").Nodes.RemoveAt(SelectedNode.Index)
        Catch
        End Try

    End Sub

    Private Sub Remove_Level(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveLevelToolStripMenuItem.Click, ToolStripButton9.Click
        Try
            If SelectedNode Is Nothing Then Exit Sub
            If Not (SelectedNode.Parent.Name = "LevelNode") Then Exit Sub
            If MsgBox("Are you sure to remove the level you selected?", MsgBoxStyle.YesNo, "Reality Game Studio") = MsgBoxResult.No Then Exit Sub
            mstore.Levels.RemoveAt(SelectedNode.Index)
            For Each gham As Integer In mstore.Sound3DLevels
                If gham = SelectedNode.Index Then
                    Dim bam As Integer = mstore.Sound3DLevels.IndexOf(gham)
                    mstore.Sound3DLevels.RemoveAt(bam)
                    mstore.Sound3DNames.RemoveAt(bam)
                    mstore.Sound3DTexts.RemoveAt(bam)
                    mstore.Sound3DXs.RemoveAt(bam)
                    mstore.Sound3DYs.RemoveAt(bam)
                    mstore.Sound3DZs.RemoveAt(bam)
                End If
            Next
            TreeView1.Nodes("LevelNode").Nodes.RemoveAt(SelectedNode.Index)
        Catch
        End Try

    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        If mstore.Levels.Count < 1 Then
            MsgBox("You need to creare at least one level " + Environment.NewLine + "before you compile your game !")
            Exit Sub
        End If
        SaveExe(Application.StartupPath + "\Temp.exe", Application.StartupPath, True)
        My.Computer.FileSystem.DeleteFile(Application.StartupPath + "\Icon1.ico")
    End Sub

    Private Sub SaveExe(ByVal FileName As String, ByVal FolderName As String, ByVal Run As Boolean)

        ProgressBar1.Visible = True
        ProgressBar1.Minimum = 0
        ProgressBar1.Maximum = 20
        ProgressBar1.Step = 1
        ProgressBar1.Value = 0
        Dim fs As New FileStream(FolderName + "\" + MediaBtn.Text, FileMode.OpenOrCreate, FileAccess.Write)
        Dim w As New BinaryWriter(fs)

        ProgressBar1.PerformStep()

        w.Write(mstore.MeshName.Count)
        If mstore.MeshName.Count <> 0 Then
            For iii As Integer = 0 To mstore.MeshName.Count - 1
                w.Write(mstore.MeshName(iii))
                w.Write(mstore.MeshText(iii).Length)
                w.Write("[start]")
                w.Write(mstore.MeshText(iii))
                w.Write("[stop]")
                w.Write(mstore.MeshXOrig(iii))
                w.Write(mstore.MeshYOrig(iii))
                w.Write(mstore.MeshZOrig(iii))

                w.Write(mstore.TextureLists(iii).TextureName.Count)
                If mstore.TextureLists(iii).TextureName.Count <> 0 Then
                    For firenze As Integer = 0 To mstore.TextureLists(iii).TextureName.Count - 1
                        w.Write(mstore.TextureLists(iii).TextureName(firenze))
                        w.Write(mstore.TextureLists(iii).TextureText(firenze).Length)
                        w.Write(mstore.TextureLists(iii).TextureText(firenze))
                    Next
                End If

                w.Write("--------------------------------------")
            Next
        End If

        ProgressBar1.PerformStep()

        w.Write("----Sprite")
        w.Write(mstore.Sprites.Count)
        If mstore.Sprites.Count <> 0 Then
            For iii As Integer = 0 To mstore.Sprites.Count - 1
                w.Write(mstore.SpriteNames(iii))
                w.Write(mstore.Sprites(iii).ImageNames.Count)
                If mstore.Sprites(iii).ImageNames.Count <> 0 Then
                    For tail As Integer = 0 To mstore.Sprites(iii).ImageNames.Count - 1
                        w.Write(mstore.Sprites(iii).ImageNames(tail))
                        w.Write(mstore.Sprites(iii).ImageTexts(tail).Length)
                        w.Write("[start]")
                        w.Write(mstore.Sprites(iii).ImageTexts(tail))
                        w.Write("[stop]")
                    Next
                End If
                w.Write(mstore.Transparent(iii))
                w.Write(mstore.TransparentColor(iii).ToArgb)
                w.Write("--------------------------------------")
            Next
        End If
        ProgressBar1.PerformStep()
        w.Write(mstore.MusicName.Count)
        w.Write("----Music")
        If mstore.MusicName.Count <> 0 Then
            For iii As Integer = 0 To mstore.MusicName.Count - 1
                w.Write(mstore.MusicText(iii).Length)
                w.Write(mstore.MusicText(iii))
            Next
        End If
        ProgressBar1.PerformStep()
        w.Write("----2DSounds")
        w.Write(mstore.Sound2DNames.Count)
        If mstore.Sound2DNames.Count <> 0 Then
            For kurkucha As Integer = 0 To mstore.Sound2DNames.Count - 1
                w.Write(mstore.Sound2DTexts(kurkucha).Length)
                w.Write(mstore.Sound2DTexts(kurkucha))
            Next
        End If
        ProgressBar1.PerformStep()

        w.Write("----3DSounds")
        w.Write(mstore.Sound3DLevels.Count)
        If mstore.Sound3DLevels.Count <> 0 Then
            For kurkucha As Integer = 0 To mstore.Sound3DLevels.Count - 1
                w.Write(mstore.Sound3DTexts(kurkucha).Length)
                w.Write(mstore.Sound3DTexts(kurkucha))
            Next
        End If
        ProgressBar1.PerformStep()

        w.Close()
        fs.Close()

        ProgressBar1.PerformStep()
        My.Computer.FileSystem.GetFileInfo(FolderName + "\" + MediaBtn.Text).Attributes = FileAttributes.Hidden


        Dim codefname As String = Application.StartupPath + "\bdGameEngine\"

        Dim fos As String = My.Computer.FileSystem.ReadAllText(Application.StartupPath + "\MainClass.vb")
        Dim texttowrite As String

        texttowrite = "Dim FileName as string = Application.StartupPath + ""\""+""" + MediaBtn.Text + """" + Environment.NewLine
        fos = Replace(fos, "REM Code for New Event", texttowrite)
        ProgressBar1.PerformStep()
        If FullScreenBtn.Checked = True Then
            texttowrite = "FullScreen = True"
        Else
            texttowrite = "FullScreen = False"
        End If

        fos = Replace(fos, "REM Code For FullScreen", texttowrite)
        ProgressBar1.PerformStep()
        texttowrite = ""
        For Each goal As String In mstore.MeshName
            texttowrite = texttowrite + "Public " + goal + " as bdMesh" + Environment.NewLine
        Next
        For Each score As String In mstore.SpriteNames
            texttowrite = texttowrite + "Public " + score + " as bdSprite" + Environment.NewLine
        Next
        For Each gool As String In mstore.Object3DName
            texttowrite = texttowrite + "Public " + gool + " as bd3DObject" + Environment.NewLine
        Next
        For Each gool As String In mstore.Object2DName
            texttowrite = texttowrite + "Public " + gool + " as bd2DObject" + Environment.NewLine
        Next
        'For Each gool As String In mstore.Sound2DNames
        '    texttowrite = texttowrite + "Public " + gool + " as bd2DSound" + Environment.NewLine
        'Next
        'For Each gool As String In mstore.Sound3DNames
        '    texttowrite = texttowrite + "Public " + gool + " as bd3DSound" + Environment.NewLine
        'Next
        'For Each gool As String In mstore.MusicName
        '    texttowrite = texttowrite + "Public " + gool + " as bdMusic" + Environment.NewLine
        'Next
        For Each bool As bdEachLevel In mstore.Levels
            For Each ggl As String In bool.InstancesID
                texttowrite = texttowrite + "Public " + ggl + " as bd3DInstance" + Environment.NewLine
            Next
            For Each ggl As String In bool.Instances2DID
                texttowrite = texttowrite + "Public " + ggl + " as bd2DInstance" + Environment.NewLine
            Next
        Next

        texttowrite = texttowrite + mstore.MoreCode + Environment.NewLine
        fos = Replace(fos, "REM More Declarations Here", texttowrite)
        ProgressBar1.PerformStep()
        texttowrite = ""
        texttowrite = texttowrite + "Me.ClientSize =  New Size(" + WidthButton.Text + ", " + HeightButton.Text + ")" + Environment.NewLine

        fos = Replace(fos, "REM Code for resizing", texttowrite)

        ProgressBar1.PerformStep()
        texttowrite = ""
        For Each dull As String In mstore.MeshName
            texttowrite = texttowrite + dull + " = AddMesh(" + mstore.MeshName.IndexOf(dull).ToString + "," + mstore.Animated(mstore.MeshName.IndexOf(dull)).ToString + ")" + Environment.NewLine
            texttowrite = texttowrite + dull + ".DefaultXAngle = " + mstore.MeshXAngle(mstore.MeshName.IndexOf(dull)).ToString + Environment.NewLine
            texttowrite = texttowrite + dull + ".DefaultYAngle = " + mstore.MeshYAngle(mstore.MeshName.IndexOf(dull)).ToString + Environment.NewLine
            texttowrite = texttowrite + dull + ".DefaultZAngle = " + mstore.MeshZAngle(mstore.MeshName.IndexOf(dull)).ToString + Environment.NewLine
            texttowrite = texttowrite + dull + ".DefaultScale = " + mstore.MeshScale(mstore.MeshName.IndexOf(dull)).ToString + Environment.NewLine
            If mstore.CollisionSpheres(mstore.MeshName.IndexOf(dull)).NotYet = False Then
                texttowrite = texttowrite + dull + ".SphereCollection.Clear()" + Environment.NewLine
                For Each tren As CollisionSphere.CollisionSpheres In mstore.CollisionSpheres(mstore.MeshName.IndexOf(dull)).Spheres
                    Dim cbsTxt As String = "cbs" + mstore.MeshName.IndexOf(dull).ToString + mstore.CollisionSpheres(mstore.MeshName.IndexOf(dull)).Spheres.IndexOf(tren).ToString
                    texttowrite = texttowrite + "Dim " + cbsTxt + " as New bdCollisionSphere" + Environment.NewLine
                    texttowrite = texttowrite + cbsTxt + ".Center = New Vector3(" + tren.CenterX.ToString + ", " + tren.CenterY.ToString + ", " + tren.CenterZ.ToString + ")" + Environment.NewLine
                    texttowrite = texttowrite + cbsTxt + ".Radius = " + tren.Radius.ToString + Environment.NewLine
                    texttowrite = texttowrite + dull + ".SphereCollection.Add(" + cbsTxt + ")" + Environment.NewLine
                Next
            End If
            If mstore.CollisionBoxes(mstore.MeshName.IndexOf(dull)).NotYet = False Then
                texttowrite = texttowrite + dull + ".BoxCollection.Clear()" + Environment.NewLine
                For Each tren As CollisionBox.CollisionBoxes In mstore.CollisionBoxes(mstore.MeshName.IndexOf(dull)).Boxes
                    Dim cbsTxt As String = "cbb" + mstore.MeshName.IndexOf(dull).ToString + mstore.CollisionBoxes(mstore.MeshName.IndexOf(dull)).Boxes.IndexOf(tren).ToString
                    texttowrite = texttowrite + "Dim " + cbsTxt + " as New bdAxisAlignedBox" + Environment.NewLine
                    texttowrite = texttowrite + cbsTxt + ".MinPos = New Vector3(" + tren.MinX.ToString + ", " + tren.MinY.ToString + ", " + tren.MinZ.ToString + ")" + Environment.NewLine
                    texttowrite = texttowrite + cbsTxt + ".MaxPos = New Vector3(" + tren.MaxX.ToString + ", " + tren.MaxY.ToString + ", " + tren.MaxZ.ToString + ")" + Environment.NewLine
                    texttowrite = texttowrite + dull + ".BoxCollection.Add(" + cbsTxt + ")" + Environment.NewLine
                Next
            End If
        Next

        'For Each dull As String In mstore.MusicName
        '    texttowrite = texttowrite + dull + " = AddMusic(" + mstore.MusicName.IndexOf(dull).ToString + ")" + Environment.NewLine
        'Next
        'For Each dull As String In mstore.Sound2DNames
        '    texttowrite = texttowrite + dull + " = Add2DSound(" + mstore.Sound2DNames.IndexOf(dull).ToString + ")" + Environment.NewLine
        'Next
        'For Each dull As String In mstore.Sound3DNames
        '    texttowrite = texttowrite + dull + " = Add3DSound(" + mstore.Sound3DNames.IndexOf(dull).ToString + ")" + Environment.NewLine
        '    texttowrite = texttowrite + dull + ".Level = " + mstore.Sound3DLevels(mstore.Sound3DNames.IndexOf(dull)).ToString + Environment.NewLine
        '    texttowrite = texttowrite + dull + ".X = " + mstore.Sound3DXs(mstore.Sound3DNames.IndexOf(dull)).ToString + Environment.NewLine
        '    texttowrite = texttowrite + dull + ".Y = " + mstore.Sound3DYs(mstore.Sound3DNames.IndexOf(dull)).ToString + Environment.NewLine
        '    texttowrite = texttowrite + dull + ".Z = " + mstore.Sound3DZs(mstore.Sound3DNames.IndexOf(dull)).ToString + Environment.NewLine
        'Next

        For Each dull As String In mstore.SpriteNames
            Dim ind As Integer = mstore.SpriteNames.IndexOf(dull)
            texttowrite = texttowrite + dull + " = AddSprite(" + ind.ToString + ")" + Environment.NewLine
        Next
        For Each lvl As bdEachLevel In mstore.Levels
            If mstore.Levels.IndexOf(lvl) > 0 Then
                texttowrite = texttowrite + "Game.AddLevel()" + Environment.NewLine
            End If
        Next
        For Each dog As String In mstore.Object3DName
            texttowrite = texttowrite + dog + " = Game.Add3DObject(" + mstore.Object3DMesh(mstore.Object3DName.IndexOf(dog)) + ")" + Environment.NewLine
        Next
        For Each dog As String In mstore.Object2DName
            texttowrite = texttowrite + dog + " = Game.Add2DObject(" + mstore.Object2DSprite(mstore.Object2DName.IndexOf(dog)) + ")" + Environment.NewLine
        Next
        For Each man As bdEachLevel In mstore.Levels
            For Each boy As String In man.InstancesID
                texttowrite = texttowrite + boy + " = " + man.ObjectsUsed(man.InstancesID.IndexOf(boy)) + _
                            ".AddInstance(" + mstore.Levels.IndexOf(man).ToString + ")" _
                            + Environment.NewLine
                texttowrite = texttowrite + boy + ".X = " + man.Xs(man.InstancesID.IndexOf(boy)).ToString + _
                                Environment.NewLine
                texttowrite = texttowrite + boy + ".Y = " + man.Ys(man.InstancesID.IndexOf(boy)).ToString + _
                                Environment.NewLine
                texttowrite = texttowrite + boy + ".Z = " + man.Zs(man.InstancesID.IndexOf(boy)).ToString + _
                                Environment.NewLine
            Next
            For Each boy As String In man.Instances2DID
                texttowrite = texttowrite + boy + " = " + man.Objects2DUsed(man.Instances2DID.IndexOf(boy)) + _
                            ".AddInstance(" + mstore.Levels.IndexOf(man).ToString + ")" _
                            + Environment.NewLine
                texttowrite = texttowrite + boy + ".X = " + man.X2Ds(man.Instances2DID.IndexOf(boy)).ToString + _
                                Environment.NewLine
                texttowrite = texttowrite + boy + ".Y = " + man.Y2Ds(man.Instances2DID.IndexOf(boy)).ToString + _
                                Environment.NewLine
            Next
            For Each wallX As Single In man.BoxXs
                Dim wlid As Integer = man.BoxXs.IndexOf(wallX)
                texttowrite = texttowrite + "Game.AddWall(" + man.BoxXs(wlid).ToString + ", " + man.BoxYs(wlid).ToString + ", " + man.BoxZs(wlid).ToString + ", " + man.BoxXAngles(wlid).ToString + ", " + man.BoxYAngles(wlid).ToString + ", " + man.BoxZAngles(wlid).ToString + ", " + man.BoxScales(wlid).ToString + ", " + man.BoxWidths(wlid).ToString + ", " + man.BoxHeights(wlid).ToString + ", " + man.BoxDepths(wlid).ToString + ")" + Environment.NewLine
            Next
        Next
        'If EscCancelBtn.Checked = True Then
        '    texttowrite = texttowrite + "Game.CancelOnEscape = True" + Environment.NewLine
        'Else
        '    texttowrite = texttowrite + "Game.CancelOnEscape = False" + Environment.NewLine
        'End If

        texttowrite = texttowrite + "Me.Text = """ + TitleButton.Text + """" + Environment.NewLine
        fos = Replace(fos, "REM Code for Load Event", texttowrite)
        ProgressBar1.PerformStep()
        texttowrite = ""
        If mstore.GetTotalEvents > 0 Then
            For god As Integer = 0 To mstore.GetTotalEvents - 1
                Dim evnt As bdEvent = mstore.GetEvent(god)
                Dim txt As String = "Public Sub " + evnt.EventName + "(" + evnt.EventArguments + ") Handles " + evnt.EventHandler + Environment.NewLine
                For Each oow As String In evnt.ActionsCollection
                    If evnt.ActionsTypes(evnt.ActionsCollection.IndexOf(oow)) = bdEvent.ActionType.TypeCode Then
                        Dim dady As String = Mid(oow, "Add Code:".Length + 1)
                        txt = txt + dady + Environment.NewLine
                    End If
                Next
                txt = txt + "End Sub" + Environment.NewLine
                texttowrite = texttowrite + txt + Environment.NewLine
            Next
        End If
        fos = Replace(fos, "REM More Events Here", texttowrite)

        texttowrite = ""
        'For Each dull As String In mstore.MusicName
        '    texttowrite = texttowrite + dull + ".Dispose()" + Environment.NewLine
        'Next
        fos = Replace(fos, "REM DISPOSING", texttowrite)

        ProgressBar1.PerformStep()
        My.Computer.FileSystem.WriteAllText(codefname + "MainClass.vb", fos, False)

        For Each dandu As String In mstore.ClassNames

            My.Computer.FileSystem.WriteAllText(codefname + dandu + ".vb", mstore.ClassTexts(mstore.ClassNames.IndexOf(dandu)), False)
        Next
        CompileFile(FileName, Run)

        For Each dandu As String In mstore.ClassNames
            My.Computer.FileSystem.DeleteFile(codefname + dandu + ".vb")
        Next

        '------------------------------------to here.
    End Sub
    Private Sub CompileFile(ByVal FileName As String, ByVal Execute As Boolean)
        ProgressBar1.PerformStep()
        'Dim codefname As String = Application.StartupPath + "\bdGameEngine\"
        'Dim CompilingFiles(My.Computer.FileSystem.GetFiles(codefname).Count - 1) As String
        'My.Computer.FileSystem.GetFiles(codefname).CopyTo(CompilingFiles, 0)
        Dim CompilingFiles(3) As String
        CompilingFiles(0) = Application.StartupPath + "\bdGameEngine\MainClass.vb"
        CompilingFiles(1) = Application.StartupPath + "\bdGameEngine\mstore.vb"
        CompilingFiles(2) = Application.StartupPath + "\bdGameEngine\TextureList.vb"
        CompilingFiles(3) = Application.StartupPath + "\bdGameEngine\bdSpriteStore.vb"

        ProgressBar1.PerformStep()
        Dim codeProviderVB As New VBCodeProvider()
        Dim codeProviderCS As New Microsoft.CSharp.CSharpCodeProvider

        Dim Output As String = FileName


        Dim parameters As New CompilerParameters()
        Dim results As CompilerResults

        Dim bn As New Bitmap(IcoButton.Image)
        Dim fffs As New FileStream(Application.StartupPath + "\Icon1.ico", FileMode.OpenOrCreate)
        My.Computer.FileSystem.GetFileInfo(Application.StartupPath + "\Icon1.ico").Attributes = FileAttributes.Hidden
        CurrentIcon.Save(fffs)
        fffs.Close()
        My.Computer.FileSystem.CurrentDirectory = Application.StartupPath
        parameters.CompilerOptions = "/imports:Microsoft.VisualBasic,System,System.Collections,System.Collections.Generic,System.Data,System.Drawing,System.Diagnostics,System.Windows.Forms /target:winexe /win32icon:Icon1.ico"

        ProgressBar1.PerformStep()
        'Make sure we generate an EXE, not a DLL
        parameters.GenerateExecutable = True

        parameters.TreatWarningsAsErrors = False
        Dim executingAssembly As System.Reflection.Assembly
        executingAssembly = System.Reflection.Assembly.GetExecutingAssembly()

        For Each assemblyName As System.Reflection.AssemblyName In executingAssembly.GetReferencedAssemblies()
            If Not (assemblyName.Name.StartsWith("bdGameEngine")) Then parameters.ReferencedAssemblies.Add(System.Reflection.Assembly.Load(assemblyName).Location)
        Next

        My.Computer.FileSystem.CreateDirectory(Application.StartupPath + "\Engine")

        My.Computer.FileSystem.WriteAllBytes(Application.StartupPath + "\Engine\bdGameEngine.dll", My.Resources.bdGameEngine, False)
        My.Computer.FileSystem.GetFileInfo(Application.StartupPath + "\Engine\bdGameEngine.dll").Attributes = FileAttributes.Hidden
        parameters.ReferencedAssemblies.Add("Engine\bdGameEngine.dll")

        For Each taha As String In Assemblies
            parameters.ReferencedAssemblies.Add(taha)
        Next

        parameters.OutputAssembly = Output
        ProgressBar1.PerformStep()
        results = codeProviderVB.CompileAssemblyFromFile(parameters, CompilingFiles)
        ProgressBar1.PerformStep()

        ProgressBar1.Value = 20
        ProgressBar1.Visible = False
        Me.Refresh()

        If results.Errors.Count > 0 Then
            'There were compiler errors

            Dim CompErr As CompilerError
            Dim ErrorText As String = ""
            Dim i As Integer = 0
            i = i + 1
            For Each CompErr In results.Errors
                If i < 8 Then
                    ErrorText = ErrorText & _
                    "FileName: " & CompErr.FileName & _
                    ", Line number " & CompErr.Line & _
                    ", Error Number: " & CompErr.ErrorNumber & _
                    ", '" & CompErr.ErrorText & ";" + Environment.NewLine + Environment.NewLine
                    i = i + 1
                End If
            Next
            Timer1.Start()
            MsgBox("Error:" + Environment.NewLine + ErrorText)
        Else
            My.Computer.FileSystem.GetFileInfo(FileName).Attributes = FileAttributes.Hidden
            If Execute = True Then

                'Me.Visible = False
                Shell(FileName, AppWinStyle.NormalFocus, True)
                'Me.Visible = True
                Try
                    My.Computer.FileSystem.DeleteDirectory(Application.StartupPath + "\Engine", FileIO.DeleteDirectoryOption.DeleteAllContents)
                Catch ex As Exception

                End Try
                Timer1.Start()
            End If
        End If

    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        On Error Resume Next
        If mstore.Levels.Count < 1 Then
            MsgBox("You need to creare at least one level " + Environment.NewLine + "before you compile your game !")
            Exit Sub
        End If
        Dim savedialog As New SaveFileDialog
        savedialog.Title = "Save EXE file as"
        savedialog.FileName = "untitled.exe"
        savedialog.Filter = "Executables |*.exe"
        If savedialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            SaveExe(Application.StartupPath + "\Temp.exe", Application.StartupPath, False)

            SaveExe2(Application.StartupPath + "\TempNew.exe", Application.StartupPath + "\Temp.exe", Application.StartupPath + "\" + MediaBtn.Text)

            My.Computer.FileSystem.CopyFile(Application.StartupPath + "\TempNew.exe", savedialog.FileName, True)
            My.Computer.FileSystem.CopyFile(Application.StartupPath + "\" + MediaBtn.Text + "k", My.Computer.FileSystem.GetFileInfo(savedialog.FileName).DirectoryName + "\" + MediaBtn.Text, True)
            My.Computer.FileSystem.DeleteFile(Application.StartupPath + "\Icon1.ico")
            My.Computer.FileSystem.GetFileInfo(savedialog.FileName).Attributes = FileAttributes.Normal
            My.Computer.FileSystem.GetFileInfo(My.Computer.FileSystem.GetFileInfo(savedialog.FileName).DirectoryName + "\" + MediaBtn.Text).Attributes = FileAttributes.Normal
            Timer1.Start()
        End If


    End Sub

    Public Sub SaveExe2(ByVal filename As String, ByVal ExeFilename As String, ByVal MediaFilename As String)
        Dim fs As New FileStream(Application.StartupPath + "\" + IO.Path.GetFileNameWithoutExtension(MediaFilename) + "k" _
                        , FileMode.OpenOrCreate, FileAccess.Write)
        Dim w As New BinaryWriter(fs)

        w.Write(My.Resources.bdGameEngine.Length)
        w.Write(My.Resources.bdGameEngine)

        w.Write(My.Computer.FileSystem.ReadAllBytes(MediaFilename).Length)
        w.Write(My.Computer.FileSystem.ReadAllBytes(MediaFilename))

        w.Write(My.Computer.FileSystem.ReadAllBytes(ExeFilename).Length)
        w.Write(My.Computer.FileSystem.ReadAllBytes(ExeFilename))

        w.Close()
        fs.Close()



        Dim codetocompile As String = My.Resources.MainCode
        codetocompile = Replace(codetocompile, "Rem FILENAME", "FileName = """ + IO.Path.GetFileNameWithoutExtension(ExeFilename) + """" + Environment.NewLine _
                            + "MedFileName = """ + IO.Path.GetFileNameWithoutExtension(MediaFilename) + """" _
                            + Environment.NewLine _
                            + "Dim k as string = """ + IO.Path.GetFileNameWithoutExtension(MediaFilename) + """")

        Dim codeProviderVB As New VBCodeProvider()

        Dim Output As String = Application.StartupPath + "\" + IO.Path.GetFileName(filename)

        Dim parameters As New CompilerParameters()
        Dim results As CompilerResults

        Dim bn As New Bitmap(IcoButton.Image)
        Dim fffs As New FileStream(Application.StartupPath + "\Icon1.ico", FileMode.OpenOrCreate)
        My.Computer.FileSystem.GetFileInfo(Application.StartupPath + "\Icon1.ico").Attributes = FileAttributes.Hidden
        CurrentIcon.Save(fffs)
        fffs.Close()
        My.Computer.FileSystem.CurrentDirectory = Application.StartupPath
        parameters.CompilerOptions = "/imports:Microsoft.VisualBasic,System,System.Collections,System.Collections.Generic,System.Data,System.Drawing,System.Diagnostics,System.Windows.Forms /target:winexe /win32icon:Icon1.ico"

        'Make sure we generate an EXE, not a DLL
        parameters.GenerateExecutable = True

        parameters.TreatWarningsAsErrors = False
        Dim executingAssembly As System.Reflection.Assembly
        executingAssembly = System.Reflection.Assembly.GetExecutingAssembly()

        For Each assemblyName As System.Reflection.AssemblyName In executingAssembly.GetReferencedAssemblies()
            parameters.ReferencedAssemblies.Add(System.Reflection.Assembly.Load(assemblyName).Location)
        Next


        parameters.OutputAssembly = Output

        results = codeProviderVB.CompileAssemblyFromSource(parameters, codetocompile)

        If results.Errors.Count > 0 Then
            'There were compiler errors

            Dim CompErr As CompilerError
            Dim ErrorText As String = ""
            Dim i As Integer = 0
            i = i + 1
            For Each CompErr In results.Errors
                If i < 8 Then
                    ErrorText = ErrorText & _
                    "FileName: " & CompErr.FileName & _
                    ", Line number " & CompErr.Line & _
                    ", Error Number: " & CompErr.ErrorNumber & _
                    ", '" & CompErr.ErrorText & ";" + Environment.NewLine + Environment.NewLine
                    i = i + 1
                End If
            Next
            MsgBox("Error:" + Environment.NewLine + ErrorText)

        Else

        End If
    End Sub

    Private Sub RcntFile1ToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RcntFile1ToolStripMenuItem.Click
        Open(RcntFile1ToolStripMenuItem.Text)
    End Sub

    Private Sub RcntFile2ToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RcntFile2ToolStripMenuItem.Click
        Open(RcntFile2ToolStripMenuItem.Text)
    End Sub

    Private Sub RcntFile3ToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RcntFile3ToolStripMenuItem.Click
        Open(RcntFile3ToolStripMenuItem.Text)
    End Sub

    Private Sub RcntFile4ToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RcntFile4ToolStripMenuItem.Click
        Open(RcntFile4ToolStripMenuItem.Text)
    End Sub

    Private Sub Add_Class(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddClassToolStripMenuItem.Click, ToolStripButton10.Click

        mstore.ClassNames.Add("Class" + Trim(Str(mstore.ClassNames.Count + 1)))
        Dim NewCode As String
        NewCode = "Public Class " + mstore.ClassNames(mstore.ClassNames.Count - 1) + Environment.NewLine + _
                    "" + Environment.NewLine + _
                    "End Class" + Environment.NewLine
        mstore.ClassTexts.Add(NewCode)
        TreeView1.Nodes("ClassNode").Nodes.Add(mstore.ClassNames(mstore.ClassNames.Count - 1))
        TreeView1.Nodes("ClassNode").Expand()
        Dim ad As ClassForm = New ClassForm(TreeView1.Nodes("ClassNode").Nodes.Count - 1)
        ad.MdiParent = Me
        ad.Show()
    End Sub

   

    Private Sub Remove_Class(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveClassToolStripMenuItem.Click, ToolStripButton11.Click
        Try
            If SelectedNode Is Nothing Then Exit Sub
            If Not (SelectedNode.Parent.Name = "ClassNode") Then Exit Sub
            If MsgBox("Are you sure to remove the class you selected?", MsgBoxStyle.YesNo, "Reality Game Studio") = MsgBoxResult.No Then Exit Sub
            mstore.ClassNames.RemoveAt(SelectedNode.Index)
            mstore.ClassTexts.RemoveAt(SelectedNode.Index)
            TreeView1.Nodes("ClassNode").Nodes.RemoveAt(SelectedNode.Index)
        Catch
        End Try
    End Sub

    Private Sub Code_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MoreCodeForMainClassToolStripMenuItem.Click
        Dim alreadyOpened As Boolean = False
        For Each ggg As Form In Me.MdiChildren
            If ggg.Text.StartsWith("More Code for the Main Class") Then
                Dim kkk As MoreCodeForm = ggg
                kkk.Focus()
                alreadyOpened = True
            End If
        Next
        If alreadyOpened = False Then
            Dim ad As MoreCodeForm = New MoreCodeForm
            ad.MdiParent = Me
            ad.Show()
        End If

    End Sub

    Private Sub IcoButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IcoButton.Click
        Dim odb As New OpenFileDialog
        odb.Multiselect = False
        odb.Filter = "Icon Files| *.ico"
        If odb.ShowDialog = Windows.Forms.DialogResult.OK Then
            IcoButton.Image = New Icon(odb.FileName).ToBitmap
            CurrentIcon = New Icon(odb.FileName)
        End If

    End Sub

    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click
        IcoButton.Image = My.Resources.Icon1.ToBitmap
        CurrentIcon = My.Resources.Icon1

    End Sub

    Private Sub MediaBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MediaBtn.Click
        Dim txt As String = InputBox("Enter the name of the new media store file " + Environment.NewLine _
                                      + "Do not type the full path name!!!", "Change Media Store File" _
                                        , MediaBtn.Text)
        If txt <> "" Then
            MediaBtn.Text = txt
        End If
    End Sub


    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        On Error Resume Next
        My.Computer.FileSystem.DeleteFile(Application.StartupPath + "\Temp.exe")
        My.Computer.FileSystem.DeleteFile(Application.StartupPath + "\" + MediaBtn.Text)
        My.Computer.FileSystem.DeleteFile(Application.StartupPath + "\TempNew.exe")
        My.Computer.FileSystem.DeleteFile(Application.StartupPath + "\" + MediaBtn.Text + "k")
        My.Computer.FileSystem.DeleteDirectory(Application.StartupPath + "\Engine", FileIO.DeleteDirectoryOption.DeleteAllContents)
        Timer1.Stop()

        Me.Focus()
    End Sub

    Private Sub ResourcesPanelToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ResourcesPanelToolStripMenuItem.Click
        Me.Panel1.Visible = Me.ResourcesPanelToolStripMenuItem.Checked
        Me.Splitter1.Visible = Me.Panel1.Visible
    End Sub

    Private Sub OptionsPanelToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OptionsPanelToolStripMenuItem.Click
        Me.ToolStrip1.Visible = Me.OptionsPanelToolStripMenuItem.Checked
    End Sub
    Private Sub MainForm_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Me.DragDrop
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            Dim MyFiles() As String
            MyFiles = e.Data.GetData(DataFormats.FileDrop)
            ToolStripStatusLabel.Text = "Opening..."
            Try
                Dim fname As String
                For Each fname In MyFiles
                    Open(fname)
                Next
            Catch ex As Exception

                MsgBox("There was an error opening the file.")
            End Try
            ToolStripStatusLabel.Text = "For Help, press F1"
        End If

    End Sub

    Private Sub MainForm_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Me.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.All
        End If
    End Sub


#Region "Treeview"
    Private SelectedNode As TreeNode

    Private Sub TreeView1_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterSelect
        If e.Action = TreeViewAction.ByMouse Then
            SelectedNode = e.Node
        End If
    End Sub

    Private Sub TreeView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TreeView1.KeyDown
        If e.Modifiers = Keys.Shift Then
            If e.KeyCode = Keys.Delete Then
                If SelectedNode Is Nothing Then Exit Sub
                If SelectedNode.Name = "MeshNode" Then
                    'Do Nothing
                ElseIf SelectedNode.Name = "ObjectNode" Then
                    'Do Nothing
                ElseIf SelectedNode.Name = "LevelNode" Then
                    'Do Nothing
                ElseIf SelectedNode.Name = "ClassNode" Then
                    'Do Nothing
                ElseIf SelectedNode.Name = "SpriteNode" Then
                    'DO Nothing
                ElseIf SelectedNode.Name = "Object2DNode" Then
                    'DO Nothing
                ElseIf SelectedNode.Name = "CtrlNode" Then
                    'DO Nothing
                ElseIf SelectedNode.Name = "CodeNode" Then
                    'DO Nothing
                ElseIf SelectedNode.Name = "MusicNode" Then
                    'DO Nothing
                ElseIf SelectedNode.Parent.Name = "MeshNode" Then
                    Me.Remove_Mesh(Me, Nothing)
                ElseIf SelectedNode.Parent.Name = "SpriteNode" Then
                    Me.RemoveSpriteToolStripMenuItem_Click(Me, Nothing)
                ElseIf SelectedNode.Parent.Name = "ObjectNode" Then
                    Me.Remove_Object(Me, Nothing)
                ElseIf SelectedNode.Parent.Name = "Object2DNode" Then
                    Me.Remove2DObjectToolStripMenuItem_Click(Me, Nothing)
                ElseIf SelectedNode.Parent.Name = "LevelNode" Then
                    Me.Remove_Level(Me, Nothing)
                ElseIf SelectedNode.Parent.Name = "ClassNode" Then
                    Me.Remove_Class(Me, Nothing)
                Else
                    'Do nothing
                End If

            End If
        End If
    End Sub

    Private Sub TreeView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TreeView1.DoubleClick

        If SelectedNode Is Nothing Then Exit Sub
        If SelectedNode.Name = "MeshNode" Then
            'Do Nothing
        ElseIf SelectedNode.Name = "ObjectNode" Then
            'Do Nothing
        ElseIf SelectedNode.Name = "LevelNode" Then
            'Do Nothing
        ElseIf SelectedNode.Name = "ClassNode" Then
            'Do Nothing
        ElseIf SelectedNode.Name = "SpriteNode" Then
            'DO Nothing
        ElseIf SelectedNode.Name = "Object2DNode" Then
            'DO Nothing
        ElseIf SelectedNode.Name = "CtrlNode" Then
            CtrlBtn_Click(Me, Nothing)
        ElseIf SelectedNode.Name = "CodeNode" Then
            Code_Click(Me, Nothing)
        ElseIf SelectedNode.Name = "MusicNode" Then
            Dim alreadyOpened As Boolean = False
            For Each ggg As Form In Me.MdiChildren
                If ggg.Text.StartsWith("Music List") Then
                    Dim kkk As MusicList = ggg
                    kkk.Focus()
                    alreadyOpened = True
                End If
            Next
            If alreadyOpened = False Then
                Dim god As New MusicList
                god.MdiParent = Me
                god.Show()
            End If

        ElseIf SelectedNode.Parent.Name = "MeshNode" Then
            Dim alreadyOpened As Boolean = False
            For Each ggg As Form In Me.MdiChildren
                If ggg.Text.StartsWith("Edit Mesh:") Then
                    Dim kkk As MeshEditor = ggg
                    If kkk.MeshIndex = SelectedNode.Index Then
                        kkk.Focus()
                        alreadyOpened = True
                    End If
                End If
            Next
            If alreadyOpened = False Then
                Dim ad As MeshEditor = New MeshEditor(SelectedNode.Index)
                ad.MdiParent = Me
                ad.Show()
            End If

        ElseIf SelectedNode.Parent.Name = "SpriteNode" Then
            Dim alreadyOpened As Boolean = False
            For Each ggg As Form In Me.MdiChildren
                If ggg.Text.StartsWith("Edit Sprite:") Then
                    Dim kkk As SpriteEditor = ggg
                    If kkk.SpriteIndex = SelectedNode.Index Then
                        kkk.Focus()
                        alreadyOpened = True
                    End If
                End If
            Next
            If alreadyOpened = False Then
                Dim ad As SpriteEditor = New SpriteEditor(SelectedNode.Index)
                ad.MdiParent = Me
                ad.Show()
            End If

        ElseIf SelectedNode.Parent.Name = "ObjectNode" Then
            Dim alreadyOpened As Boolean = False
            For Each ggg As Form In Me.MdiChildren
                If ggg.Text.StartsWith("Edit 3D Object:") Then
                    Dim kkk As ObjectEditor = ggg
                    If kkk.ObjectIndex = SelectedNode.Index Then
                        kkk.Focus()
                        alreadyOpened = True
                    End If
                End If
            Next
            If alreadyOpened = False Then
                Dim ad As ObjectEditor = New ObjectEditor(SelectedNode.Index)
                ad.MdiParent = Me
                ad.Show()
            End If

        ElseIf SelectedNode.Parent.Name = "Object2DNode" Then
            Dim alreadyOpened As Boolean = False
            For Each ggg As Form In Me.MdiChildren
                If ggg.Text.StartsWith("Edit 2D Object:") Then
                    Dim kkk As Object2DEditor = ggg
                    If kkk.ObjectIndex = SelectedNode.Index Then
                        kkk.Focus()
                        alreadyOpened = True
                    End If
                End If
            Next
            If alreadyOpened = False Then
                Dim ad As Object2DEditor = New Object2DEditor(SelectedNode.Index)
                ad.MdiParent = Me
                ad.Show()
            End If

        ElseIf SelectedNode.Parent.Name = "LevelNode" Then
            Dim alreadyOpened As Boolean = False
            For Each ggg As Form In Me.MdiChildren
                If ggg.Text.StartsWith("Edit Level:") Then
                    Dim kkk As LevelEditor = ggg
                    If kkk.LevelIndex = SelectedNode.Index Then
                        kkk.Focus()
                        alreadyOpened = True
                    End If
                End If
            Next
            If alreadyOpened = False Then
                Dim ad As LevelEditor = New LevelEditor(SelectedNode.Index)
                ad.MdiParent = Me
                ad.Show()
            End If

        ElseIf SelectedNode.Parent.Name = "ClassNode" Then
            Dim alreadyOpened As Boolean = False
            For Each ggg As Form In Me.MdiChildren
                If ggg.Text.StartsWith("Edit Class:") Then
                    Dim kkk As ClassForm = ggg
                    If kkk.ClassIndex = SelectedNode.Index Then
                        kkk.Focus()
                        alreadyOpened = True
                    End If
                End If
            Next
            If alreadyOpened = False Then
                Dim ad As ClassForm = New ClassForm(SelectedNode.Index)
                ad.MdiParent = Me
                ad.Show()
            End If

        Else
            'Do nothing
        End If
    End Sub
#End Region

    Private Sub ResourcesToolbarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ResourcesToolbarToolStripMenuItem.Click
        Me.ToolStrip2.Visible = Me.ResourcesToolbarToolStripMenuItem.Checked
    End Sub

    Private Sub AddSpriteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddSpriteToolStripMenuItem.Click, ToolStripButton12.Click
        mstore.Sprites.Add(New bdSpriteStore)
        mstore.SpriteNames.Add("Sprite" + (mstore.SpriteNames.Count + 1).ToString)
        mstore.TransparentColor.Add(Color.Magenta)
        mstore.Transparent.Add(False)
        Dim ad As SpriteEditor = New SpriteEditor(mstore.SpriteNames.Count - 1)
        ad.MdiParent = Me
        ad.Show()
        TreeView1.Nodes("SpriteNode").Nodes.Add("Sprite" + (mstore.SpriteNames.Count).ToString)
        TreeView1.Nodes("SpriteNode").Expand()

    End Sub

    Private Sub RemoveSpriteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveSpriteToolStripMenuItem.Click, ToolStripButton13.Click
        Try
            If SelectedNode Is Nothing Then Exit Sub
            If Not (SelectedNode.Parent.Name = "SpriteNode") Then Exit Sub
            If MsgBox("Are you sure to remove the sprite you selected?", MsgBoxStyle.YesNo, "Reality Game Studio") = MsgBoxResult.No Then Exit Sub
            mstore.Sprites.RemoveAt(SelectedNode.Index)
            mstore.SpriteNames.RemoveAt(SelectedNode.Index)
            TreeView1.Nodes("SpriteNode").Nodes.RemoveAt(SelectedNode.Index)
        Catch
        End Try

    End Sub

    Private Sub Add2DObjectToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Add2DObjectToolStripMenuItem.Click, ToolStripButton14.Click
        mstore.Add2DObject("Object2D" + Trim(Str(mstore.Object2DName.Count + 1)), "")
        Dim ad As Object2DEditor = New Object2DEditor(mstore.Object2DName.Count - 1)
        ad.MdiParent = Me
        ad.Show()
        TreeView1.Nodes("Object2DNode").Nodes.Add("Object2D" + Trim(Str(mstore.Object2DName.Count)))
        TreeView1.Nodes("Object2DNode").Expand()

    End Sub

    Private Sub Remove2DObjectToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Remove2DObjectToolStripMenuItem.Click, ToolStripButton15.Click

        Try
            If SelectedNode Is Nothing Then Exit Sub
            If Not (SelectedNode.Parent.Name = "Object2DNode") Then Exit Sub
            If MsgBox("Are you sure to remove the 2D object you selected?", MsgBoxStyle.YesNo, "Reality Game Studio") = MsgBoxResult.No Then Exit Sub
            mstore.Object2DSprite.RemoveAt(SelectedNode.Index)
            mstore.Object2DName.RemoveAt(SelectedNode.Index)
            For Each gau As bdEachLevel In mstore.Levels
                For Each gum As String In gau.Objects2DUsed
                    If gum = SelectedNode.Text Then
                        Dim crow As Integer = gau.Objects2DUsed.IndexOf(gum)
                        gau.Instances2DID.RemoveAt(crow)
                        gau.X2Ds.RemoveAt(crow)
                        gau.Y2Ds.RemoveAt(crow)
                        gau.Objects2DUsed.Remove(gum)

                    End If
                Next
            Next
            TreeView1.Nodes("Object2DNode").Nodes.RemoveAt(SelectedNode.Index)
        Catch
        End Try

    End Sub

    Private Sub TitleButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TitleButton.Click
        Dim txt As String = InputBox("Enter the new name or title for your game", "Change Game Title", TitleButton.Text)
        If txt <> "" Then
            TitleButton.Text = txt
        End If

    End Sub

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        AboutBox.ShowDialog()
    End Sub

    Private Sub HelpToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ContentsToolStripMenuItem.Click, HelpToolStripButton.Click
        Help.ShowHelp(Me, Application.StartupPath + "/Reality Game Studio.chm")
    End Sub

    Private Sub WidthButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WidthButton.Click
        Dim txt As String = InputBox("Enter the new width for the game's window", _
                                    "Change Window Width" _
                                       , WidthButton.Text)
        If txt <> "" Then
            WidthButton.Text = Val(txt).ToString
        End If

    End Sub

    Private Sub HeightButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HeightButton.Click
        Dim txt As String = InputBox("Enter the new height for the game's window", _
                                            "Change Window Height" _
                                               , HeightButton.Text)
        If txt <> "" Then
            HeightButton.Text = Val(txt).ToString
        End If

    End Sub

    Private Sub ToolStripButton16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton16.Click
        References.ShowDialog()
    End Sub

    Private Sub ToolStripButton17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton17.Click
        Try
            If SelectedNode Is Nothing Then Exit Sub
            If Not (SelectedNode.Parent.Name = "ObjectNode") Then Exit Sub
            Dim ndi As Integer = SelectedNode.Parent.Nodes(SelectedNode.Index).PrevNode.Index
            Dim msh As String = mstore.Object3DMesh(ndi + 1)
            Dim nme As String = mstore.Object3DName(ndi + 1)
            Dim nd As TreeNode = SelectedNode

            mstore.Object3DMesh(ndi + 1) = mstore.Object3DMesh(ndi)
            mstore.Object3DName(ndi + 1) = mstore.Object3DName(ndi)
            Dim g As TreeNode = nd.Clone
            SelectedNode.Parent.Nodes.Insert(ndi, g)
            SelectedNode.Parent.Nodes.RemoveAt(ndi + 2)

            mstore.Object3DMesh(ndi) = msh
            mstore.Object3DName(ndi) = nme
            TreeView1.SelectedNode = g
            SelectedNode = g
        Catch
        End Try
    End Sub

    Private Sub ToolStripButton18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton18.Click
        Try
            If SelectedNode Is Nothing Then Exit Sub
            If Not (SelectedNode.Parent.Name = "ObjectNode") Then Exit Sub
            Dim ndi As Integer = SelectedNode.Parent.Nodes(SelectedNode.Index).NextNode.Index
            Dim msh As String = mstore.Object3DMesh(ndi - 1)
            Dim nme As String = mstore.Object3DName(ndi - 1)
            Dim nd As TreeNode = SelectedNode

            mstore.Object3DMesh(ndi - 1) = mstore.Object3DMesh(ndi)
            mstore.Object3DName(ndi - 1) = mstore.Object3DName(ndi)
            Dim g As TreeNode = nd.Clone
            SelectedNode.Parent.Nodes.Insert(ndi + 1, g)
            SelectedNode.Parent.Nodes.RemoveAt(ndi - 1)

            mstore.Object3DMesh(ndi) = msh
            mstore.Object3DName(ndi) = nme
            TreeView1.SelectedNode = g
            SelectedNode = g
        Catch
        End Try
    End Sub

    Private Sub ToolStripButton19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton19.Click
        Try
            If SelectedNode Is Nothing Then Exit Sub
            If Not (SelectedNode.Parent.Name = "Object2DNode") Then Exit Sub
            Dim ndi As Integer = SelectedNode.Parent.Nodes(SelectedNode.Index).PrevNode.Index
            Dim msh As String = mstore.Object2DSprite(ndi + 1)
            Dim nme As String = mstore.Object2DName(ndi + 1)
            Dim nd As TreeNode = SelectedNode

            mstore.Object2DSprite(ndi + 1) = mstore.Object2DSprite(ndi)
            mstore.Object2DName(ndi + 1) = mstore.Object2DName(ndi)
            Dim g As TreeNode = nd.Clone
            SelectedNode.Parent.Nodes.Insert(ndi, g)
            SelectedNode.Parent.Nodes.RemoveAt(ndi + 2)

            mstore.Object2DSprite(ndi) = msh
            mstore.Object2DName(ndi) = nme
            TreeView1.SelectedNode = g
            SelectedNode = g
        Catch
        End Try
    End Sub

    Private Sub ToolStripButton20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton20.Click
        Try
            If SelectedNode Is Nothing Then Exit Sub
            If Not (SelectedNode.Parent.Name = "Object2DNode") Then Exit Sub
            Dim ndi As Integer = SelectedNode.Parent.Nodes(SelectedNode.Index).NextNode.Index
            Dim msh As String = mstore.Object2DSprite(ndi - 1)
            Dim nme As String = mstore.Object2DName(ndi - 1)
            Dim nd As TreeNode = SelectedNode

            mstore.Object2DSprite(ndi - 1) = mstore.Object2DSprite(ndi)
            mstore.Object2DName(ndi - 1) = mstore.Object2DName(ndi)
            Dim g As TreeNode = nd.Clone
            SelectedNode.Parent.Nodes.Insert(ndi + 1, g)
            SelectedNode.Parent.Nodes.RemoveAt(ndi - 1)

            mstore.Object2DSprite(ndi) = msh
            mstore.Object2DName(ndi) = nme
            TreeView1.SelectedNode = g
            SelectedNode = g
        Catch
        End Try
    End Sub

End Class


