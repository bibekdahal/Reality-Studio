Imports System.Windows.Forms
Imports System
Imports System.IO


Public Class MeshViewer

    Private MEditor As MeshViewerDisplay
    Public DoSuppress As Boolean
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim rrr As Integer = MainForm.mstore.MeshText.IndexOf(MainForm.CurrentMeshView)
        Dim karka As CollisionSphere = MainForm.mstore.CollisionSpheres(rrr)
        karka.NotYet = False
        karka.Spheres.Clear()
        For Each gunda As bd3DInstance In MEditor.SphrColl
            Dim sapk As CollisionSphere.CollisionSpheres
            sapk.CenterX = gunda.X
            sapk.CenterY = gunda.Y
            sapk.CenterZ = gunda.Z
            sapk.Radius = gunda.Mesh.Radius * gunda.ScaleFactor
            karka.Spheres.Add(sapk)

        Next
        Dim karkab As CollisionBox = MainForm.mstore.CollisionBoxes(rrr)
        karkab.NotYet = False
        karkab.Boxes.Clear()
        For Each gunda As bd3DInstance In MEditor.BoxColl
            Dim sapk As CollisionBox.CollisionBoxes
            sapk.MinX = gunda.GetBoundingBox(0).MinPos.X
            sapk.MinY = gunda.GetBoundingBox(0).MinPos.Y
            sapk.MinZ = gunda.GetBoundingBox(0).MinPos.Z
            sapk.MaxX = gunda.GetBoundingBox(0).MaxPos.X
            sapk.MaxY = gunda.GetBoundingBox(0).MaxPos.Y
            sapk.MaxZ = gunda.GetBoundingBox(0).MaxPos.Z
            'sapk.Radius = gunda.Mesh.Radius * gunda.ScaleFactor
            karkab.Boxes.Add(sapk)

        Next
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click

        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub MeshViewer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Click
        MEditor.Focus()
    End Sub

    Private Sub MeshViewer_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        MEditor.Close()
        Try
            My.Computer.FileSystem.DeleteDirectory(Application.StartupPath + "\Textures", FileIO.DeleteDirectoryOption.DeleteAllContents)
            My.Computer.FileSystem.DeleteDirectory(Application.StartupPath + "\Temp", FileIO.DeleteDirectoryOption.DeleteAllContents)
        Catch
        End Try
    End Sub

    Private Sub Events_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        DoSuppress = True
    End Sub

    Private Sub MeshViewer_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If My.Computer.FileSystem.DirectoryExists(Application.StartupPath + "\Temp") Then
                My.Computer.FileSystem.DeleteDirectory(Application.StartupPath + "\Temp", FileIO.DeleteDirectoryOption.DeleteAllContents)
            End If
            If My.Computer.FileSystem.DirectoryExists(Application.StartupPath + "\Textures") Then
                My.Computer.FileSystem.DeleteDirectory(Application.StartupPath + "\Textures", FileIO.DeleteDirectoryOption.DeleteAllContents)
            End If
            For Each ctl As Control In Me.Controls
                AddHandler ctl.KeyDown, AddressOf Events_KeyDown
            Next
            SphrBox.Items.Clear()
            Dim rrr As Integer = MainForm.mstore.MeshText.IndexOf(MainForm.CurrentMeshView)
            Dim karka As CollisionSphere = MainForm.mstore.CollisionSpheres(rrr)
            If karka.NotYet = True Then
                SphrBox.Items.Add("Sphr0")
                SphrBox.SelectedIndex = 0
            Else
                Dim iiio As Integer = 0
                For Each ojha As CollisionSphere.CollisionSpheres In karka.Spheres
                    SphrBox.Items.Add("Sphr" + iiio.ToString())
                    iiio += 1
                Next
            End If
            BoxBox.Items.Clear()
            Dim karkab As CollisionBox = MainForm.mstore.CollisionBoxes(rrr)
            If karkab.NotYet = True Then
                BoxBox.Items.Add("Box0")
                BoxBox.SelectedIndex = 0
            Else
                Dim iiio As Integer = 0
                For Each ojha As CollisionBox.CollisionBoxes In karkab.Boxes
                    BoxBox.Items.Add("Box" + iiio.ToString())
                    iiio += 1
                Next
            End If
            Try
                My.Computer.FileSystem.CreateDirectory(Application.StartupPath + "\Textures")

                For Each goru As String In MainForm.mstore.TextureLists(rrr).TextureName
                    Dim infile As New FileStream(Application.StartupPath + "\Textures\" + goru, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write)

                    infile.Write(MainForm.mstore.TextureLists(rrr).TextureText(MainForm.mstore.TextureLists(rrr).TextureName.IndexOf(goru)), 0, MainForm.mstore.TextureLists(rrr).TextureText(MainForm.mstore.TextureLists(rrr).TextureName.IndexOf(goru)).Length)
                    infile.Close()

                Next
            Catch
            End Try

            MEditor = New MeshViewerDisplay(MainForm.AnimatedMesh)
            'MEditor.MdiParent = Me
            MEditor.TopLevel = False
            Me.Controls.Add(MEditor)
            MEditor.Show()

            PictureBox1.SendToBack()

            MEditor.Focus()
        Catch
            MsgBox("Error Loading")
            Me.Close()
        End Try
    End Sub

    Private Sub SphrBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SphrBox.SelectedIndexChanged
        Try
            MEditor.SelectedIndex = SphrBox.SelectedIndex
            MEditor.Focus()
        Catch
        End Try

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            SphrBox.Items.Add("Sphr" + SphrBox.Items.Count.ToString)
            MEditor.AddSphere()
            SphrBox.SelectedIndex = SphrBox.Items.Count - 1
            MEditor.Focus()
        Catch
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            If SphrBox.Items.Count < 2 Then Exit Sub
            If SphrBox.SelectedIndex < 0 Then Exit Sub
            'MEditor.SphrColl.RemoveAt(SphrBox.SelectedIndex)
            MEditor.RemoveSphere(SphrBox.SelectedIndex)
            SphrBox.Items.RemoveAt(SphrBox.SelectedIndex)

            MEditor.Focus()
        Catch
        End Try
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        MEditor.Game.Camera.Z -= 400
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        MEditor.Game.Camera.Z += 400
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Dim od As New SaveFileDialog
        od.Filter = "Text Files |*.txt"
        If od.ShowDialog = Windows.Forms.DialogResult.OK Then
            Try
                Dim TheText As String = ""
                TheText = "///Do not edit this file manually at all"
                For Each gunda As bd3DInstance In MEditor.SphrColl
                    TheText = TheText + Environment.NewLine + gunda.X.ToString
                    TheText = TheText + Environment.NewLine + gunda.Y.ToString
                    TheText = TheText + Environment.NewLine + gunda.Z.ToString
                    TheText = TheText + Environment.NewLine + (gunda.Mesh.Radius * gunda.ScaleFactor).ToString
                Next

                My.Computer.FileSystem.WriteAllText(od.FileName, TheText, False)
            Catch
                MsgBox("Error saving Spheres Data to the file " + od.FileName)
            End Try

        End If
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Dim od As New OpenFileDialog
        od.Multiselect = False
        od.Filter = "Text Files |*.txt"
        If od.ShowDialog = Windows.Forms.DialogResult.OK Then
            Try
                Dim sr As StreamReader = My.Computer.FileSystem.OpenTextFileReader(od.FileName)
                Dim dummy As String = sr.ReadLine()
                Dim cbb As New List(Of CollisionSphere.CollisionSpheres)
                While sr.EndOfStream = False
                    Dim kukhura As New CollisionSphere.CollisionSpheres
                    kukhura.CenterX = Val(sr.ReadLine)
                    kukhura.CenterY = Val(sr.ReadLine)
                    kukhura.CenterZ = Val(sr.ReadLine)
                    kukhura.Radius = Val(sr.ReadLine)
                    cbb.Add(kukhura)
                    SphrBox.Items.Add("Sphr" + SphrBox.Items.Count.ToString)

                End While
                MEditor.AddSphere(cbb)
            Catch ex As Exception
                MsgBox("Error loading Spheres Data from the file " + od.FileName)
            End Try
        End If
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Try
            BoxBox.Items.Add("Box" + BoxBox.Items.Count.ToString)
            MEditor.AddBox()
            BoxBox.SelectedIndex = BoxBox.Items.Count - 1
            MEditor.Focus()
        Catch
        End Try
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Try
            If BoxBox.Items.Count < 2 Then Exit Sub
            If BoxBox.SelectedIndex < 0 Then Exit Sub
            'MEditor.SphrColl.RemoveAt(SphrBox.SelectedIndex)
            MEditor.RemoveBox(BoxBox.SelectedIndex)
            BoxBox.Items.RemoveAt(BoxBox.SelectedIndex)

            MEditor.Focus()
        Catch
        End Try
    End Sub

    Private Sub BoxBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BoxBox.SelectedIndexChanged
        Try
            MEditor.SelectedIndexB = BoxBox.SelectedIndex
            MEditor.Focus()
        Catch
        End Try
    End Sub
End Class

