Public Class MeshEditor
    Public MeshIndex As Integer

    Private txtList As New List(Of Byte())

    Private msh As Byte()
    Private Sub MeshEditor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim ggg As MainForm = Me.ParentForm

        msh = ggg.mstore.MeshText(MeshIndex)

        Me.Text = "Edit Mesh: " + ggg.mstore.MeshName(MeshIndex)
        Me.TextBox1.Text = ggg.mstore.MeshName(MeshIndex)
        Me.TextBox2.Text = ggg.mstore.MeshXOrig(MeshIndex).ToString
        Me.TextBox3.Text = ggg.mstore.MeshYOrig(MeshIndex).ToString
        Me.TextBox4.Text = ggg.mstore.MeshZOrig(MeshIndex).ToString

        Me.NumberBox4.Text = ggg.mstore.MeshXAngle(MeshIndex).ToString
        Me.NumberBox3.Text = ggg.mstore.MeshYAngle(MeshIndex).ToString
        Me.NumberBox2.Text = ggg.mstore.MeshZAngle(MeshIndex).ToString

        Me.NumberBox5.Text = ggg.mstore.MeshScale(MeshIndex).ToString
        Me.CheckBox1.Checked = ggg.mstore.Animated(MeshIndex)

        For Each kow As String In ggg.mstore.TextureLists(MeshIndex).TextureName
            TxtBox.Items.Add(kow)
            txtList.Add(ggg.mstore.TextureLists(MeshIndex).TextureText(ggg.mstore.TextureLists(MeshIndex).TextureName.IndexOf(kow)))
        Next
    End Sub

    Public Sub New(ByVal Index As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        MeshIndex = Index


    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Button5_Click(Me, Nothing)
        Dim ggg As MainForm = Me.ParentForm
        ggg.CurrentMeshView = msh
        ggg.AnimatedMesh = Me.CheckBox1.Checked
        ggg.CurrentMeshXAngle = Val(Me.NumberBox4.Text)
        ggg.CurrentMeshYAngle = Val(Me.NumberBox3.Text)
        ggg.CurrentMeshZAngle = Val(Me.NumberBox2.Text)
        ggg.CurrentMeshScale = Val(Me.NumberBox5.Text)
        MeshViewer.Visible = False
        MeshViewer.ShowDialog()
        
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim ggg As MainForm = Me.ParentForm
        Dim opener As New OpenFileDialog
        opener.Multiselect = False
        opener.Filter = "DirectX Mesh Files| *.x"
        If opener.ShowDialog = Windows.Forms.DialogResult.OK Then
            Try
                msh = ggg.mstore.ChangeMesh(opener.FileName)
            Catch
                MsgBox("Cannot add mesh from file """ + opener.FileName + """.")
            End Try
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Button5_Click(Me, Nothing)
        Me.Close()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.Close()
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Try
            Dim ggg As MainForm = Me.ParentForm
            ggg.mstore.MeshName(MeshIndex) = Me.TextBox1.Text
            ggg.mstore.MeshText(MeshIndex) = msh
            ggg.mstore.MeshXOrig(MeshIndex) = Val(Me.TextBox2.Text)
            ggg.mstore.MeshYOrig(MeshIndex) = Val(Me.TextBox3.Text)
            ggg.mstore.MeshZOrig(MeshIndex) = Val(Me.TextBox4.Text)

            ggg.mstore.MeshXAngle(MeshIndex) = Val(Me.NumberBox4.Text)
            ggg.mstore.MeshYAngle(MeshIndex) = Val(Me.NumberBox3.Text)
            ggg.mstore.MeshZAngle(MeshIndex) = Val(Me.NumberBox2.Text)

            ggg.mstore.MeshScale(MeshIndex) = Val(Me.NumberBox5.Text)

            ggg.mstore.Animated(MeshIndex) = Me.CheckBox1.Checked

            ggg.TreeView1.Nodes("MeshNode").Nodes(MeshIndex).Text = Me.TextBox1.Text

            ggg.mstore.TextureLists(MeshIndex).TextureName.Clear()
            ggg.mstore.TextureLists(MeshIndex).TextureText.Clear()
            For Each kow As String In TxtBox.Items
                ggg.mstore.TextureLists(MeshIndex).TextureName.Add(kow)
                ggg.mstore.TextureLists(MeshIndex).TextureText.Add(txtList(TxtBox.Items.IndexOf(kow)))
            Next
        Catch
        End Try
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
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
                txtList.Add(buffer)
                TxtBox.Items.Add(My.Computer.FileSystem.GetName(fname))
            Next
        End If
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        If TxtBox.Items.Count > 0 Then
            If Not (TxtBox.SelectedItem Is Nothing) Then
                txtList.RemoveAt(TxtBox.SelectedIndex)
                TxtBox.Items.RemoveAt(TxtBox.SelectedIndex)

            End If
        End If
    End Sub
End Class
