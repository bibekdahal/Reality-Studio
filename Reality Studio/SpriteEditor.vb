Imports System
Imports System.IO

Public Class SpriteEditor
    Public SpriteIndex As Integer
    Private ImageList As New List(Of Byte())

    Private WithEvents SEditor As ImgViewer


    Private Sub SpriteEditor_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        SEditor.Close()
    End Sub

    Private Sub Sprite_Editor_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ggg As MainForm = Me.ParentForm
        Me.Text = "Edit Sprite: " + ggg.mstore.SpriteNames(SpriteIndex)
        Me.TextBox1.Text = ggg.mstore.SpriteNames(SpriteIndex)
        Me.PictureBox2.BackColor = ggg.mstore.TransparentColor(SpriteIndex)
        Me.CheckBox2.Checked = ggg.mstore.Transparent(SpriteIndex)
        Me.Label5.BackColor = ggg.mstore.TransparentColor(SpriteIndex)
        SEditor = New ImgViewer

        SEditor.TopLevel = False
        Me.Controls.Add(SEditor)
        SEditor.Size = PictureBox1.Size
        SEditor.Show()
        SEditor.Location = PictureBox1.Location


        PictureBox1.SendToBack()


        For Each kow As Byte() In ggg.mstore.Sprites(SpriteIndex).ImageTexts
            ImgBox.Items.Add(ggg.mstore.Sprites(SpriteIndex).ImageNames(ggg.mstore.Sprites(SpriteIndex).ImageTexts.IndexOf(kow)))
            My.Computer.FileSystem.CreateDirectory(Application.StartupPath + "\Temp")
            My.Computer.FileSystem.GetDirectoryInfo(Application.StartupPath + "\Temp").Attributes = FileAttributes.Hidden

            Dim infile As New FileStream(Application.StartupPath + "\Temp\img.bmp", FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write)

            infile.Write(kow, 0, kow.Length)
            infile.Close()
            SEditor.AddImage(Application.StartupPath + "\Temp\img.bmp")
            My.Computer.FileSystem.DeleteDirectory(Application.StartupPath + "\Temp", FileIO.DeleteDirectoryOption.DeleteAllContents)
            ImageList.Add(kow)
        Next
        Try
            Me.ImgBox.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub
    Public Sub New(ByVal Index As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        SpriteIndex = Index



    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Try
            Dim OFD As New OpenFileDialog
            OFD.Filter = "All Files | *.*"
            OFD.Multiselect = True
            If OFD.ShowDialog = Windows.Forms.DialogResult.OK Then
                For Each fname As String In OFD.FileNames
                    Dim infile As New System.IO.FileStream(fname, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read)
                    Dim buffer(infile.Length - 1) As Byte
                    Dim count As Integer = infile.Read(buffer, 0, buffer.Length)
                    If count <> buffer.Length Then
                        infile.Close()
                        MsgBox("Bad File")
                        Return
                    End If
                    infile.Close()
                    ImageList.Add(buffer)
                    SEditor.AddImage(fname)

                    ImgBox.Items.Add("Image" + (ImgBox.Items.Count).ToString)
                    ImgBox.SelectedItem = ImgBox.Items(ImgBox.Items.Count - 1)
                Next
            End If
        Catch
            ' SEditor.txt.Text = "Bad File!"
        End Try
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        If ImgBox.Items.Count > 0 Then
            If Not (ImgBox.SelectedItem Is Nothing) Then
                ImageList.RemoveAt(ImgBox.SelectedIndex)
                SEditor.RemoveImage(ImgBox.SelectedIndex)
                ImgBox.Items.RemoveAt(ImgBox.SelectedIndex)
            End If
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If ImgBox.Items.Count > 0 Then
            If Not (ImgBox.SelectedItem Is Nothing) Then
                Dim newname As String = InputBox("Enter the string value to use as new " + Environment.NewLine + "name for the selected image." _
                                             , "Rename Image", ImgBox.SelectedItem)
                ImgBox.SelectedItem = newname
            End If
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Button5_Click(Me, Nothing)
        Me.Close()
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Try
            Dim ggg As MainForm = Me.ParentForm
            ggg.mstore.SpriteNames(SpriteIndex) = Me.TextBox1.Text
            ggg.mstore.Transparent(SpriteIndex) = CheckBox2.Checked
            ggg.mstore.TransparentColor(SpriteIndex) = PictureBox2.BackColor
            ggg.TreeView1.Nodes("SpriteNode").Nodes(SpriteIndex).Text = Me.TextBox1.Text

            ggg.mstore.Sprites(SpriteIndex).ImageNames.Clear()
            ggg.mstore.Sprites(SpriteIndex).ImageTexts.Clear()
            For Each kow As String In ImgBox.Items
                ggg.mstore.Sprites(SpriteIndex).ImageNames.Add(kow)
                ggg.mstore.Sprites(SpriteIndex).ImageTexts.Add(ImageList(ImgBox.Items.IndexOf(kow)))
            Next
        Catch
        End Try
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.Close()
    End Sub

    Private Sub ImgBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImgBox.SelectedIndexChanged
        If ImgBox.SelectedItem Is Nothing Then Exit Sub
        SEditor.ChangeImage(ImgBox.SelectedIndex)
        ' SEditor.txt.Text = ""
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            SEditor.Speed = 1
        Else
            SEditor.Speed = 0
        End If
    End Sub

    Private Sub PictureBox1_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles SEditor.MouseClick
        On Error GoTo Ex

        Dim bmpTmp As Bitmap
        Dim dumb As New ScreenShot.ScreenCapture
        'Dim g As Graphics = SEditor.

        bmpTmp = dumb.CaptureDeskTopRectangle(SEditor.RectangleToScreen(SEditor.ClientRectangle), SEditor.RectangleToScreen(SEditor.ClientRectangle).Width, SEditor.RectangleToScreen(SEditor.ClientRectangle).Height)
        Dim clrTmp As Color


        clrTmp = bmpTmp.GetPixel(e.X, e.Y)
        Me.Label5.BackColor = clrTmp

        Me.PictureBox2.BackColor = clrTmp
        bmpTmp.Dispose()

Ex:
        Exit Sub
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            Dim OFD As New OpenFileDialog
            OFD.Filter = "All Files | *.*"
            OFD.Multiselect = True
            If OFD.ShowDialog = Windows.Forms.DialogResult.OK Then
                For Each fname As String In OFD.FileNames
                    Dim img As Image = Image.FromFile(fname)

                    Dim FrameDimensions As System.Drawing.Imaging.FrameDimension = New System.Drawing.Imaging.FrameDimension(img.FrameDimensionsList(0))

                   
                    Dim NumberOfFrames As Integer = img.GetFrameCount(FrameDimensions)

                    If NumberOfFrames > 0 Then
                        For www As Integer = 0 To NumberOfFrames - 1
                            img.SelectActiveFrame(FrameDimensions, www)
                            My.Computer.FileSystem.CreateDirectory(Application.StartupPath + "\GifImages\")
                            My.Computer.FileSystem.GetDirectoryInfo(Application.StartupPath + "\GifImages\").Attributes = FileAttributes.Hidden
                            Dim bmp As New Bitmap(img)

                            bmp.Save(Application.StartupPath + "\GifImages\img.bmp")
                            Dim infile As New System.IO.FileStream(Application.StartupPath + "\GifImages\img.bmp", IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read)
                            Dim buffer(infile.Length - 1) As Byte
                            Dim count As Integer = infile.Read(buffer, 0, buffer.Length)
                            If count <> buffer.Length Then
                                infile.Close()
                                MsgBox("Bad File")
                                Return
                            End If
                            infile.Close()
                            ImageList.Add(buffer)
                            SEditor.AddImage(Application.StartupPath + "\GifImages\img.bmp")

                            ImgBox.Items.Add("Image" + (ImgBox.Items.Count).ToString)
                            ImgBox.SelectedItem = ImgBox.Items(ImgBox.Items.Count - 1)

                            My.Computer.FileSystem.DeleteDirectory(Application.StartupPath + "\GifImages", FileIO.DeleteDirectoryOption.DeleteAllContents)
                        Next
                    End If
                    
                Next
            End If
        Catch
            ' SEditor.txt.Text = "Bad File!"
        End Try
    End Sub

End Class