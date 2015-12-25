Public Class Object2DEditor
    Public ObjectIndex As Integer
    Public Sub New(ByVal Index As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ObjectIndex = Index


    End Sub

    Private Sub ObjectEditor_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        RefreshMenu()
        Dim ggg As MainForm = Me.ParentForm
        Me.Text = "Edit 2D Object: " + ggg.mstore.Object2DName(ObjectIndex)
        Me.TextBox1.Text = ggg.mstore.Object2DSprite(ObjectIndex)
        Me.TextBox2.Text = ggg.mstore.Object2DName(ObjectIndex)
        SprStrip.Visible = False
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Button2_Click(Me, Nothing)
        Me.Close()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        RefreshMenu()
    End Sub
    Public Sub RefreshMenu()
        SprStrip.Items.Clear()
        Dim ggg As MainForm = Me.ParentForm
        For Each ddd As String In ggg.mstore.SpriteNames
            SprStrip.Items.Add(ddd)
        Next
        SprStrip.Visible = True
        SprStrip.Focus()
    End Sub

    Private Sub SprStrip_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SprStrip.Click
        TextBox1.Text = SprStrip.Text
        SprStrip.Visible = False
    End Sub

    Private Sub SprStrip_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles SprStrip.GotFocus
        SprStrip.Text = TextBox1.Text
    End Sub

    Private Sub SprStrip_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles SprStrip.LostFocus
        SprStrip.Visible = False
    End Sub

    Private Sub ObjectEditor_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseClick
        SprStrip.Visible = False
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            Dim kakleto As New List(Of Integer)
            Dim ggg As MainForm = Me.ParentForm
            For Each kow As bdEachLevel In ggg.mstore.Levels
                For Each iii As String In kow.Objects2DUsed
                    If iii = ggg.mstore.Object2DName(ObjectIndex) Then
                        kakleto.Add(kow.Objects2DUsed.IndexOf(iii))
                    End If
                Next
                For Each jjj As Integer In kakleto
                    kow.Objects2DUsed(jjj) = Me.TextBox2.Text
                Next
            Next
            ggg.mstore.Object2DName(ObjectIndex) = Me.TextBox2.Text
            ggg.mstore.Object2DSprite(ObjectIndex) = Me.TextBox1.Text
            ggg.TreeView1.Nodes("Object2DNode").Nodes(ObjectIndex).Text = Me.TextBox2.Text
        Catch
        End Try
    End Sub

End Class