Imports System.Windows.Forms

Public Class LevelEditor
    Public LevelIndex As Integer
    Private MEditor As MainEditor
    Public DoSuppress As Boolean

    Private Snd3DTextList As New List(Of Byte())


    Private Sub LevelEditor_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason = CloseReason.UserClosing Then
            ToolStripButton1_Click(Me, Nothing)
        End If
        Dim ggg As MainForm = MainForm
        For Each kangaroo As Form In ggg.MdiChildren
            kangaroo.Visible = True
        Next
        For Each kan As ToolStripMenuItem In ggg.MainMenuStrip.Items
            kan.Visible = True
        Next
        ggg.StatusStrip.Visible = ggg.StatusBarToolStripMenuItem.Checked
        ggg.ToolStrip.Visible = ggg.ToolBarToolStripMenuItem.Checked
        ggg.ToolStrip1.Visible = ggg.OptionsPanelToolStripMenuItem.Checked
        ggg.ToolStrip2.Visible = ggg.ResourcesToolbarToolStripMenuItem.Checked
        ggg.Panel1.Visible = ggg.ResourcesPanelToolStripMenuItem.Checked
        ggg.Splitter1.Visible = ggg.ResourcesPanelToolStripMenuItem.Checked
        MEditor.Close()
        Try
            My.Computer.FileSystem.DeleteDirectory(Application.StartupPath + "\Textures", FileIO.DeleteDirectoryOption.DeleteAllContents)
            My.Computer.FileSystem.DeleteDirectory(Application.StartupPath + "\Temp", FileIO.DeleteDirectoryOption.DeleteAllContents)
        Catch
        End Try
        
    End Sub

    Private Sub LevelEditor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        On Error Resume Next
        AddHandler InsBox.KeyDown, AddressOf Events_KeyDown
        AddHandler ObjBox.KeyDown, AddressOf Events_KeyDown
        AddHandler SolidBox.KeyDown, AddressOf Events_KeyDown
        AddHandler Obj2DBox.KeyDown, AddressOf Events_KeyDown
        AddHandler Ins2DBox.KeyDown, AddressOf Events_KeyDown

        'ToolStrip.Renderer = New ToolStripProfessionalRenderer(New MS2007Colors())
        'StatusStrip.Renderer = New ToolStripProfessionalRenderer(New MS2007Colors())
        'ToolStrip1.Renderer = New ToolStripProfessionalRenderer(New MS2007Colors())

        Me.Text = "Edit Level: " + MainForm.mstore.Levels(LevelIndex).LevelName
        Me.ToolStripTextBox1.Text = MainForm.mstore.Levels(LevelIndex).LevelName



        My.Computer.FileSystem.CreateDirectory(Application.StartupPath + "\Textures")
        My.Computer.FileSystem.GetDirectoryInfo(Application.StartupPath + "\Textures").Attributes = IO.FileAttributes.Hidden
        For rrr As Integer = 0 To MainForm.mstore.MeshName.Count - 1
            For Each goru As String In MainForm.mstore.TextureLists(rrr).TextureName
                Dim infile As New IO.FileStream(Application.StartupPath + "\Textures\" + goru, IO.FileMode.OpenOrCreate, IO.FileAccess.Write, IO.FileShare.Write)

                infile.Write(MainForm.mstore.TextureLists(rrr).TextureText(MainForm.mstore.TextureLists(rrr).TextureName.IndexOf(goru)), 0, MainForm.mstore.TextureLists(rrr).TextureText(MainForm.mstore.TextureLists(rrr).TextureName.IndexOf(goru)).Length)
                infile.Close()

            Next
        Next

        Dim ggg As MainForm = MainForm
        ggg.Refresh()
        For Each kangaroo As Form In ggg.MdiChildren
            If Not kangaroo Is Me Then kangaroo.Visible = False
        Next
        For Each kan As ToolStripMenuItem In ggg.MainMenuStrip.Items
            kan.Visible = False
        Next

        Me.Location = New Point(Me.Location.X + ggg.Panel1.Width, Me.Location.Y + ggg.ToolStrip.Height _
                                + ggg.ToolStrip2.Height)
        ggg.Panel1.Visible = False
        ggg.Splitter1.Visible = False
        ggg.StatusStrip.Visible = False
        ggg.ToolStrip.Visible = False
        ggg.ToolStrip1.Visible = False
        ggg.ToolStrip2.Visible = False

        MEditor = New MainEditor
        MEditor.Size = New Size(ggg.WidthButton.Text, ggg.HeightButton.Text)
        'MEditor.MdiParent = Me
        MEditor.TopLevel = False
        Me.Controls.Add(MEditor)
        MEditor.SendToBack()
        MEditor.Location = New Point(MEditor.Location.X, MEditor.Location.Y + ToolStrip.Height * 2)
        MEditor.Show()
        LoadInstances()
        LoadWalls()
        LoadSounds()


        ObjBox.SelectedIndex = 0
        Me.ObjBox_SelectedIndexChanged(Me, Nothing)
        InsBox.SelectedIndex = 0
        Obj2DBox.SelectedIndex = 0
        Me.Obj2DBox_SelectedIndexChanged(Me, Nothing)
        Ins2DBox.SelectedIndex = 0

        MEditor.Focus()
    End Sub
    Private Sub LoadInstances()
        Dim ggg As MainForm = MainForm
        For Each kkk As String In ggg.mstore.Object3DName
            ObjBox.Items.Add(kkk)
            MEditor.AddMeshObj(ObjBox.Items.Count - 1)
        Next
        MEditor.LoadInstances(LevelIndex)

        For Each lll As String In ggg.mstore.Object2DName
            Obj2DBox.Items.Add(lll)
            MEditor.Add2DObj(Obj2DBox.Items.Count - 1)
        Next
        MEditor.Load2DInstances(LevelIndex)

    End Sub

  
    Private Sub LoadWalls()
        Dim ggg As MainForm = MainForm
        For Each wall As Single In ggg.mstore.Levels(LevelIndex).BoxXs
            SolidBox.Items.Add("Wall" + (SolidBox.Items.Count).ToString)
        Next
        MEditor.LoadWalls(LevelIndex)
    End Sub
    Private Sub LoadSounds()
        Dim ggg As MainForm = MainForm
        For Each snd As String In ggg.mstore.Sound3DNames
            Snd3DBox.Items.Add(snd)
            Snd3DTextList.Add(ggg.mstore.Sound3DTexts(ggg.mstore.Sound3DNames.IndexOf(snd)))
        Next
        MEditor.LoadSounds(LevelIndex)
    End Sub
    Public Sub New(ByVal Index As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        LevelIndex = Index
        
    End Sub

    Private Sub Events_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        DoSuppress = True
    End Sub

    Private Sub ObjBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ObjBox.SelectedIndexChanged
        Try
            Dim ggg As MainForm = MainForm
            InsBox.Items.Clear()
            Dim CurrentOBJ As Integer = 0
            Dim lolo As bdEachLevel = ggg.mstore.Levels(LevelIndex)
            For Each dude As String In lolo.InstancesID
                If lolo.ObjectsUsed(CurrentOBJ) = ObjBox.SelectedItem Then
                    InsBox.Items.Add(dude)
                End If
                CurrentOBJ += 1
            Next
        Catch
        End Try
        Try
            InsBox.SelectedIndex = 0
        Catch ex As Exception

        End Try
        MEditor.Focus()
    End Sub

    

    Private Sub InsBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InsBox.SelectedIndexChanged

        MEditor.ChangeCurrentMesh(ObjBox.SelectedIndex, InsBox.SelectedIndex)

        MEditor.Focus()

    End Sub

    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click
        If ObjBox.SelectedItem Is Nothing Then Exit Sub
        MEditor.AddInstance(ObjBox.SelectedIndex)
        Dim ggg As MainForm = MainForm
        Dim kkk As bdEachLevel = ggg.mstore.Levels(LevelIndex)
        kkk.ObjectsUsed.Add(ObjBox.SelectedItem)
        kkk.InstancesID.Add(ObjBox.SelectedItem + "_" + GetRndId() + "_" + Trim(Str(LevelIndex)))
        kkk.Xs.Add(MEditor.CurrentMesh.X)
        kkk.Ys.Add(MEditor.CurrentMesh.Y)
        kkk.Zs.Add(MEditor.CurrentMesh.Z)
        InsBox.Items.Add(ObjBox.SelectedItem + "_" + GetRndId() + "_" + Trim(Str(LevelIndex)))
        InsBox.SelectedIndex = InsBox.Items.Count - 1
        InsBox_SelectedIndexChanged(Me, Nothing)
    End Sub



    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        Me.Close()
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Try
            Dim ggg As MainForm = MainForm
            Dim kkk As bdEachLevel = ggg.mstore.Levels(LevelIndex)
            kkk.LevelName = Me.ToolStripTextBox1.Text
            ggg.TreeView1.Nodes("LevelNode").Nodes(LevelIndex).Text = Me.ToolStripTextBox1.Text
            'kkk.Xs.Clear()
            'kkk.Ys.Clear()
            'kkk.Zs.Clear()
            'kkk.X2Ds.Clear()
            'kkk.Y2Ds.Clear()
            kkk.BoxWidths.Clear()
            kkk.BoxHeights.Clear()
            kkk.BoxDepths.Clear()
            kkk.BoxScales.Clear()
            kkk.BoxXAngles.Clear()
            kkk.BoxXs.Clear()
            kkk.BoxYAngles.Clear()
            kkk.BoxYs.Clear()
            kkk.BoxZAngles.Clear()
            kkk.BoxZs.Clear()

            ggg.mstore.Sound3DXs.Clear()
            ggg.mstore.Sound3DYs.Clear()
            ggg.mstore.Sound3DZs.Clear()
            ggg.mstore.Sound3DTexts.Clear()
            ggg.mstore.Sound3DNames.Clear()
            ggg.mstore.Sound3DLevels.Clear()

            'For i As Integer = 0 To MEditor.Game.Total3DObjects - 1
            '    Dim dude As bd3DObject = MEditor.Game.Get3DObject(i)
            '    For j As Integer = 0 To dude.GetTotalInstances(MEditor.Game.CurrentLevel) - 1
            '        kkk.Xs.Add(dude.GetInstance(j, MEditor.Game.CurrentLevel).X)
            '        kkk.Ys.Add(dude.GetInstance(j, MEditor.Game.CurrentLevel).Y)
            '        kkk.Zs.Add(dude.GetInstance(j, MEditor.Game.CurrentLevel).Z)
            '    Next
            'Next

            'For l As Integer = 0 To MEditor.Game.Total2DObjects - 1
            '    Dim dude As bd2DObject = MEditor.Game.Get2DObject(l)
            '    For m As Integer = 0 To dude.GetTotalInstances(MEditor.Game.CurrentLevel) - 1
            '        kkk.X2Ds.Add(dude.GetInstance(m, MEditor.Game.CurrentLevel).X)
            '        kkk.Y2Ds.Add(dude.GetInstance(m, MEditor.Game.CurrentLevel).Y)
            '    Next
            'Next

            For Each man As bd3DInstance In MEditor.WallC
                kkk.BoxXs.Add(man.X)
                kkk.BoxYs.Add(man.Y)
                kkk.BoxZs.Add(man.Z)
                kkk.BoxXAngles.Add(man.XAngle)
                kkk.BoxYAngles.Add(man.YAngle)
                kkk.BoxZAngles.Add(man.ZAngle)
                kkk.BoxScales.Add(man.ScaleFactor)
                'kkk.BoxWidths.Add(man.Mesh.Width)
                'kkk.BoxHeights.Add(man.Mesh.Height)
                'kkk.BoxDepths.Add(man.Mesh.Depth)
            Next

            For Each ttt As Byte() In Snd3DTextList
                ggg.mstore.Sound3DTexts.Add(ttt)
                ggg.mstore.Sound3DNames.Add(Snd3DBox.Items(Snd3DTextList.IndexOf(ttt)))
                ggg.mstore.Sound3DLevels.Add(LevelIndex)
                ggg.mstore.Sound3DXs.Add(MEditor.SndC(Snd3DTextList.IndexOf(ttt)).X)
                ggg.mstore.Sound3DYs.Add(MEditor.SndC(Snd3DTextList.IndexOf(ttt)).Y)
                ggg.mstore.Sound3DZs.Add(MEditor.SndC(Snd3DTextList.IndexOf(ttt)).Z)
            Next
        Catch
        End Try
    End Sub

   
    Private Sub ToolStripButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton4.Click
        ToolStripButton1_Click(Me, Nothing)
        Me.Close()
    End Sub

    Private Sub ToolStripButton5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton5.Click
        If InsBox.SelectedItem Is Nothing Then Exit Sub
        MEditor.RemoveInstance(ObjBox.SelectedIndex, InsBox.SelectedIndex)
        Dim ggg As MainForm = MainForm
        Dim kkk As bdEachLevel = ggg.mstore.Levels(LevelIndex)
        Dim bishwas As Integer = kkk.InstancesID.IndexOf(InsBox.SelectedItem)
        kkk.ObjectsUsed.RemoveAt(bishwas)
        kkk.InstancesID.RemoveAt(bishwas)
        kkk.Xs.RemoveAt(bishwas)
        kkk.Ys.RemoveAt(bishwas)
        kkk.Zs.RemoveAt(bishwas)
        Dim selind As Integer = InsBox.SelectedIndex
        InsBox.Items.Remove(InsBox.SelectedItem)
        If selind > 0 And selind <> InsBox.Items.Count Then
            InsBox.SelectedIndex = selind
            InsBox_SelectedIndexChanged(Me, Nothing)
        End If
    End Sub

    Private Sub ToolStripButton6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton6.Click
        SolidBox.Items.Add("Wall" + (SolidBox.Items.Count).ToString)
        MEditor.AddWall()
        SolidBox.SelectedIndex = MEditor.SelectedWall
        MEditor.Focus()
    End Sub

    Private Sub ToolStripButton7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton7.Click
        If SolidBox.SelectedIndex > -1 And Not (SolidBox.SelectedItem Is Nothing) Then
            MEditor.RemoveWall(SolidBox.SelectedIndex)
            SolidBox.Items.RemoveAt(SolidBox.SelectedIndex)
            SolidBox.SelectedIndex = MEditor.SelectedWall
        End If
        MEditor.Focus()
    End Sub

    Private Sub SolidBox_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles SolidBox.SelectedIndexChanged
        MEditor.SelectedWall = SolidBox.SelectedIndex
        MEditor.Focus()
    End Sub

    Public Function GetRndId() As String
        Dim str As String = ""

        str = (New Random(Environment.TickCount).Next).ToString

        Return str
    End Function

    Public Sub ToolStripButton8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton8.Click
        If Obj2DBox.SelectedItem Is Nothing Then Exit Sub
        MEditor.Add2DInstance(Obj2DBox.SelectedIndex)
        Dim ggg As MainForm = MainForm
        Dim kkk As bdEachLevel = ggg.mstore.Levels(LevelIndex)
        kkk.Objects2DUsed.Add(Obj2DBox.SelectedItem)
        kkk.Instances2DID.Add(Obj2DBox.SelectedItem + "_" + GetRndId() + "_" + Trim(Str(LevelIndex)))
        kkk.X2Ds.Add(MEditor.CurrentIns.X)
        kkk.Y2Ds.Add(MEditor.CurrentIns.Y)
        Ins2DBox.Items.Add(Obj2DBox.SelectedItem + "_" + GetRndId() + "_" + Trim(Str(LevelIndex)))
        Ins2DBox.SelectedIndex = Ins2DBox.Items.Count - 1
        Ins2DBox_SelectedIndexChanged(Me, Nothing)
    End Sub

    Public Sub ToolStripButton9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton9.Click
        If Ins2DBox.SelectedItem Is Nothing Then Exit Sub
        MEditor.Remove2DInstance(Obj2DBox.SelectedIndex, Ins2DBox.SelectedIndex)
        Dim ggg As MainForm = MainForm
        Dim kkk As bdEachLevel = ggg.mstore.Levels(LevelIndex)
        Dim bishwas As Integer = kkk.Instances2DID.IndexOf(Ins2DBox.SelectedItem)
        kkk.Objects2DUsed.RemoveAt(bishwas)
        kkk.Instances2DID.RemoveAt(bishwas)
        kkk.X2Ds.RemoveAt(bishwas)
        kkk.Y2Ds.RemoveAt(bishwas)
        Dim selind As Integer = Ins2DBox.SelectedIndex
        Ins2DBox.Items.Remove(Ins2DBox.SelectedItem)
        If selind > 0 And selind <> Ins2DBox.Items.Count Then
            Ins2DBox.SelectedIndex = selind
            Ins2DBox_SelectedIndexChanged(Me, Nothing)
        End If
    End Sub

    Public Sub Obj2DBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Obj2DBox.SelectedIndexChanged
        Try
            Dim ggg As MainForm = MainForm
            Ins2DBox.Items.Clear()
            Dim CurrentOBJ As Integer = 0
            Dim lolo As bdEachLevel = ggg.mstore.Levels(LevelIndex)
            For Each dude As String In lolo.Instances2DID
                If lolo.Objects2DUsed(CurrentOBJ) = Obj2DBox.SelectedItem Then
                    Ins2DBox.Items.Add(dude)
                End If
                CurrentOBJ += 1
            Next
        Catch
        End Try
        Try
            Ins2DBox.SelectedIndex = 0
        Catch ex As Exception

        End Try
        MEditor.Focus()
    End Sub

    Public Sub Ins2DBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Ins2DBox.SelectedIndexChanged
        MEditor.ChangeCurrent2DInstance(Obj2DBox.SelectedIndex, Ins2DBox.SelectedIndex)

        MEditor.Focus()

    End Sub

    Private Sub ToolStripButton10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton10.Click
        MEditor.ViewTop()
    End Sub

    Private Sub ToolStripButton11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton11.Click
        MEditor.ViewFront()
    End Sub

    Private Sub ToolStripButton12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton12.Click
        If ToolStripButton12.Checked = True Then
            MEditor.MakeWallVisible()
        Else
            MEditor.MakeWallInVisible()
        End If
    End Sub


    Private Sub ToolStripButton15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton15.Click
        Dim ofd As New OpenFileDialog
        ofd.Multiselect = True
        ofd.Filter = "All Files| *.*"
        If ofd.ShowDialog = Windows.Forms.DialogResult.OK Then
            For Each fname As String In ofd.FileNames
                'Try
                Dim infile As New System.IO.FileStream(fname, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read)
                Dim buffer(infile.Length - 1) As Byte
                Dim count As Integer = infile.Read(buffer, 0, buffer.Length)
                If count <> buffer.Length Then
                    infile.Close()
                    MsgBox("Bad File")
                    Return
                End If
                infile.Close()

                Snd3DTextList.Add(buffer)

                Dim name As String = InputBox("Enter the name of the newly loaded 3D sound", "Sound Name", "snd_" + LevelIndex.ToString + Snd3DTextList.Count.ToString)
                If name = "" Then name = "snd_" + LevelIndex.ToString + Snd3DTextList.Count.ToString
                Snd3DBox.Items.Add(name)

                MEditor.AddSndMesh()

                'Catch ex As Exception
                '    MsgBox("Bad File")
                'End Try
            Next
        End If
    End Sub

    Private Sub ToolStripButton16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton16.Click
        If Snd3DBox.Items.Count < 1 Then Exit Sub
        If Snd3DBox.SelectedItem Is Nothing Then Exit Sub
        Snd3DTextList.RemoveAt(Snd3DBox.SelectedIndex)
        MEditor.RemoveSnd(Snd3DBox.SelectedIndex)
        Snd3DBox.Items.RemoveAt(Snd3DBox.SelectedIndex)
    End Sub

    Private Sub Snd3DBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Snd3DBox.SelectedIndexChanged
        MEditor.SelectedSnd = Snd3DBox.SelectedIndex
        MEditor.Focus()
    End Sub

End Class
