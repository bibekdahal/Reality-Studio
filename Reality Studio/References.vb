Imports System.Windows.Forms

Public Class References

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        MainForm.Assemblies.Clear()
        For Each tah As String In ListBox1.Items
            MainForm.Assemblies.Add(tah)
        Next
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub References_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ListBox1.Items.Clear()
        For Each tah As String In MainForm.Assemblies
            ListBox1.Items.Add(tah)
        Next
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim tah As String = InputBox("Enter the reference name.")
        ListBox1.Items.Add(tah)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim ood As New OpenFileDialog
        ood.Multiselect = False
        If ood.ShowDialog = Windows.Forms.DialogResult.OK Then
            ListBox1.Items.Add(ood.FileName)
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If ListBox1.SelectedItem Is Nothing Then Exit Sub
        If ListBox1.SelectedIndex > -1 Then
            ListBox1.Items.RemoveAt(ListBox1.SelectedIndex)
        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If ListBox1.SelectedItem Is Nothing Then Exit Sub
        If ListBox1.SelectedIndex > -1 Then
            Dim tah As String = InputBox("Enter the new reference name.", , ListBox1.SelectedItem)
            ListBox1.Items(ListBox1.SelectedIndex) = tah
        End If
    End Sub
End Class
