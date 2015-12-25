Public Class NumberBox


    Private Sub NumberBox_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.TextChanged
        If IsNumeric(Me.Text) Then
            Me.BackColor = Color.White
            Me.ForeColor = Color.Black
        Else
            Me.BackColor = Color.Red
            Me.ForeColor = Color.White
        End If
    End Sub

    Private Sub NumberBox_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If IsNumeric(Me.Text) Then
            Me.BackColor = Color.White
            Me.ForeColor = Color.Black
        Else
            Me.BackColor = Color.Red
            Me.ForeColor = Color.White
        End If
    End Sub
End Class
