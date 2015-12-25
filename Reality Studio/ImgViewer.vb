Public Class ImgViewer
    Public Game As New bdGame
    Private obj As bd2DObject
    Private ins As bd2DInstance
    Private spr As bdSprite

    'Public txt As bd2DText
    Private Sub ImgViewer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.Opaque, True)
        Game.Initialize(Me.Handle)
        spr = Game.AddSprite()
        spr.ImageSpeed = 0
        obj = Game.Add2DObject(spr)
        ins = obj.AddInstance(Game.CurrentLevel)
        Game.BackColor = Color.WhiteSmoke

        'txt = Game.Add2DText(New Font("Times New Roman", 70, FontStyle.Bold))
        'txt.TextColor = Color.Tomato
        'txt.Position = New Point(2, 893)
    End Sub
    Public Sub AddImage(ByVal filename As String)
        Dim img As Image = Image.FromFile(filename)
        Dim ddd As bdImage = spr.AddImageFromFile(filename, False, Color.Black)
        img.Dispose()
        'spr.ImageIndex = spr.GetImageIndex(ddd)
    End Sub

    Public Sub RemoveImage(ByVal index As Integer)
        spr.RemoveImage(index)
    End Sub
    Public Sub ChangeImage(ByVal index As Integer)
        spr.ImageIndex = index
    End Sub

    Private Sub ImgViewer_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Game.Clean()
    End Sub
    Private Sub ImgViewer_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Try
            Game.Render()
            Me.Invalidate()
        Catch
        End Try
    End Sub
    Public Property Speed() As Double
        Get
            Return spr.ImageSpeed
        End Get
        Set(ByVal value As Double)
            spr.ImageSpeed = value
        End Set
    End Property
End Class