Imports System.Windows.Forms

Public Class MusicList

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.Close()
    End Sub

    Private Sub MusicList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim ggg As MainForm = Me.MdiParent
        For Each gaida As String In ggg.mstore.MusicName
            MusicBox.Items.Add(gaida)
        Next
        For Each gaida As String In ggg.mstore.Sound2DNames
            SndBox.Items.Add(gaida)
        Next
    End Sub

    Private Sub AddButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddButton.Click
        Dim od As New OpenFileDialog
        Dim ggg As MainForm = Me.ParentForm
        od.Multiselect = True
        od.Filter = "All Files|*.*"
        If od.ShowDialog = Windows.Forms.DialogResult.OK Then
            For Each fname As String In od.FileNames
                Try
                    Dim str As String = InputBox("Type in the name of the music " + Chr(13) + _
                                                "stored in file: """ + fname + """.", , "MyMusic")
                    Dim infile As New IO.FileStream(fname, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read)
                    Dim buffer(infile.Length - 1) As Byte
                    Dim count As Integer = infile.Read(buffer, 0, buffer.Length)
                    If count <> buffer.Length Then
                        infile.Close()
                        MsgBox("Couldn't add music file:" + fname)
                        Exit Sub
                    End If
                    infile.Close()
                    ggg.mstore.MusicText.Add(buffer)
                    ggg.mstore.MusicName.Add(str)
                    MusicBox.Items.Add(str)
                Catch
                    MsgBox("Couldn't add music file:" + fname)
                End Try
            Next
        End If
    End Sub

    Private Sub RemoveButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveButton.Click
        If MusicBox.SelectedIndex <> -1 And Not (MusicBox.SelectedItem Is Nothing) Then
            Dim ggg As MainForm = Me.ParentForm
            ggg.mstore.MusicName.RemoveAt(MusicBox.SelectedIndex)
            ggg.mstore.MusicText.RemoveAt(MusicBox.SelectedIndex)
            MusicBox.Items.RemoveAt(MusicBox.SelectedIndex)
        End If
    End Sub

  
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim od As New OpenFileDialog
        Dim ggg As MainForm = Me.ParentForm
        od.Multiselect = True
        od.Filter = "All Files|*.*"
        If od.ShowDialog = Windows.Forms.DialogResult.OK Then
            For Each fname As String In od.FileNames
                Try
                    Dim str As String = InputBox("Type in the name of the sound " + Chr(13) + _
                                                "stored in file: """ + fname + """.", , "MySound")
                    Dim infile As New IO.FileStream(fname, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read)
                    Dim buffer(infile.Length - 1) As Byte
                    Dim count As Integer = infile.Read(buffer, 0, buffer.Length)
                    If count <> buffer.Length Then
                        infile.Close()
                        MsgBox("Couldn't add music file:" + fname)
                        Exit Sub
                    End If
                    infile.Close()
                    ggg.mstore.Sound2DTexts.Add(buffer)
                    ggg.mstore.Sound2DNames.Add(str)
                    SndBox.Items.Add(str)
                Catch
                    MsgBox("Couldn't add sound file:" + fname)
                End Try
            Next
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If SndBox.SelectedIndex <> -1 And Not (SndBox.SelectedItem Is Nothing) Then
            Dim ggg As MainForm = Me.ParentForm
            ggg.mstore.Sound2DNames.RemoveAt(SndBox.SelectedIndex)
            ggg.mstore.Sound2DTexts.RemoveAt(SndBox.SelectedIndex)
            SndBox.Items.RemoveAt(SndBox.SelectedIndex)
        End If
    End Sub
End Class
