Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Data
Imports System.IO
Imports System.Diagnostics
'Imports Microsoft.DirectX
'Imports Microsoft.DirectX.Direct3D
Imports System.Runtime.InteropServices
Imports bdGameEngine

Public Class MainClass
    Inherits Form
    Private Structure SHFILEINFO
        Public hIcon As IntPtr            ' : icon
        Public iIcon As Integer           ' : icondex
        Public dwAttributes As Integer    ' : SFGAO_ flags
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=260)> _
        Public szDisplayName As String
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=80)> _
        Public szTypeName As String
    End Structure

    Private Declare Auto Function SHGetFileInfo Lib "shell32.dll" _
            (ByVal pszPath As String, _
             ByVal dwFileAttributes As Integer, _
             ByRef psfi As SHFILEINFO, _
             ByVal cbFileInfo As Integer, _
             ByVal uFlags As Integer) As IntPtr

    Private Const SHGFI_ICON As Integer = &H100
    Private Const SHGFI_SMALLICON As Integer = &H1
    Private Const SHGFI_LARGEICON As Integer = &H0    ' Large icon

    Public Shared Sub Main()

        Application.Run(New MainClass())
    End Sub

    Public Sub New()
        Dim hImgSmall As IntPtr
        Dim shinfo As SHFILEINFO
        shinfo = New SHFILEINFO()
        hImgSmall = SHGetFileInfo(Application.ExecutablePath, 0, shinfo, _
                 Marshal.SizeOf(shinfo), _
                 SHGFI_ICON Or SHGFI_SMALLICON)
        Dim myIcon As System.Drawing.Icon
        myIcon = System.Drawing.Icon.FromHandle(shinfo.hIcon)

        Me.Icon = myIcon
        Me.InitializeComponent()
        'Try
        '    My.Computer.FileSystem.CreateDirectory(Application.StartupPath + "\Textures")
        '    My.Computer.FileSystem.GetDirectoryInfo(Application.StartupPath + "\Textures").Attributes = FileAttributes.Hidden
        'Catch
        'End Try
        Try
            My.Computer.FileSystem.CreateDirectory(Application.StartupPath + "\Temp")
            My.Computer.FileSystem.GetDirectoryInfo(Application.StartupPath + "\Temp").Attributes = FileAttributes.Hidden
        Catch
        End Try


        Me.SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.Opaque, True)
        Dim FullScreen As Boolean = False
        REM Code For FullScreen
        ',FullScreen)
        If Game.Initialize(Me.Handle) = False Then
            MsgBox("Can't initialize DirectX or DirectX device!")
            Application.Exit()
        End If
        Game.Camera.Z = -10
        Game.Camera.Y = 3
        Camera = Game.Camera

        REM Code for New Event 

        Dim fs As New FileStream(FileName, FileMode.Open, FileAccess.Read)
        Dim w As New BinaryReader(fs)
        Dim dummy As String

        mstore.MeshName.Clear()
        mstore.MeshText.Clear()
        mstore.MeshXOrig.Clear()
        mstore.MeshYOrig.Clear()
        mstore.MeshZOrig.Clear()
        mstore.SpriteNames.Clear()
        mstore.Sprites.Clear()
        mstore.Transparent.Clear()
        mstore.TransparentColor.Clear()
        mstore.SpriteXOrigins.Clear()
        mstore.SpriteYOrigins.Clear()


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

                mstore.TextureLists.Add(New TextureList)
                Dim textureCount As Integer = w.ReadInt32
                If textureCount <> 0 Then
                    For trenalew As Integer = 0 To textureCount - 1
                        mstore.TextureLists(lld).TextureName.Add(w.ReadString)
                        Dim kookoo As Integer = w.ReadInt32
                        mstore.TextureLists(lld).TextureText.Add(w.ReadBytes(kookoo))
                    Next
                End If

                dummy = w.ReadString

            Next
        End If

        dummy = w.ReadString
        mstore.Sprites.Clear()
        mstore.SpriteNames.Clear()
        mstore.SpriteXOrigins.Clear()
        mstore.SpriteYOrigins.Clear()
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
        Dim mscCount As Integer = w.ReadInt32
        dummy = w.ReadString
        mstore.MusicText.Clear()
        If mscCount <> 0 Then
            For log As Integer = 0 To mscCount - 1
                Dim mscLng As Integer = w.ReadInt32
                mstore.MusicText.Add(w.ReadBytes(mscLng))
            Next
        End If
        dummy = w.ReadString
        Dim snd2dcount As Integer = w.ReadInt32
        If snd2dcount <> 0 Then
            For kurkucha As Integer = 0 To snd2dcount - 1
                Dim ln2d As Integer = w.ReadInt32
                mstore.Sound2DTexts.Add(w.ReadBytes(ln2d))
            Next
        End If

        dummy = w.ReadString
        Dim snd3dcount As Integer = w.ReadInt32
        If snd3dcount <> 0 Then
            For kurkucha As Integer = 0 To snd3dcount - 1
                Dim ln3d As Integer = w.ReadInt32
                mstore.Sound3DTexts.Add(w.ReadBytes(ln3d))
            Next
        End If
        w.Close()
        fs.Close()

        Try

            My.Computer.FileSystem.CreateDirectory(Application.StartupPath + "\Temp")
            For rrr As Integer = 0 To mstore.MeshName.Count - 1
                For Each goru As String In mstore.TextureLists(rrr).TextureName
                    Dim infile As New IO.FileStream(Application.StartupPath + "\Temp\" + goru, IO.FileMode.OpenOrCreate, IO.FileAccess.Write, IO.FileShare.Write)

                    infile.Write(mstore.TextureLists(rrr).TextureText(mstore.TextureLists(rrr).TextureName.IndexOf(goru)), 0, mstore.TextureLists(rrr).TextureText(mstore.TextureLists(rrr).TextureName.IndexOf(goru)).Length)
                    infile.Close()

                Next
            Next
        Catch
        End Try


        BeforeLoad()


        Me.Show()
    End Sub
    Private Sub BeforeLoad()
        REM Code for Load Event
    End Sub
    Private Sub Form1_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        Game.Clean()
        REM DISPOSING

        My.Computer.FileSystem.DeleteDirectory(Application.StartupPath + "\TempM", FileIO.DeleteDirectoryOption.DeleteAllContents)

        My.Computer.FileSystem.DeleteDirectory(Application.StartupPath + "\Temp", FileIO.DeleteDirectoryOption.DeleteAllContents)
        
    End Sub
    Private Sub Form1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Game.Render()
        Me.Invalidate()
    End Sub
    Public Sub InitializeComponent()
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        'Me.Size = New System.Drawing.Size(640, 480)

        REM Code for resizing

        Me.MaximizeBox = False

        Me.ShowIcon = True
        Me.ShowInTaskbar = True
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.Text = ""
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
        Me.ResumeLayout(False)
    End Sub

    Public Function AddMesh(ByVal Index As Integer, ByVal Animated As Boolean) As bdMesh


        'My.Computer.FileSystem.CreateDirectory(Application.StartupPath + "\Temp")

        Dim infile As New FileStream(Application.StartupPath + "\Temp\MeshTMP.x", FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write)

        infile.Write(mstore.MeshText(Index), 0, mstore.MeshText(index).Length)
        infile.Close()

        Dim newm As bdMesh = Game.AddMesh(Application.StartupPath + "\Temp\MeshTMP.x", Application.StartupPath + "\Temp", Animated)
        'newm.XOrigin = mstore.MeshXOrig(Index)
        'newm.YOrigin = mstore.MeshYOrig(Index)
        'newm.ZOrigin = mstore.MeshZOrig(Index)

        'My.Computer.FileSystem.DeleteDirectory(Application.StartupPath + "\Temp", FileIO.DeleteDirectoryOption.DeleteAllContents)
        My.Computer.FileSystem.DeleteFile(Application.StartupPath + "\Temp\MeshTMP.x")
        Return newm
    End Function
    Public Function AddSprite(ByVal Index As Integer) As bdSprite
        Dim newm As bdSprite = Game.AddSprite

        Dim dkdk As Integer = Index
        For Each donkey() As Byte In mstore.Sprites(dkdk).ImageTexts
            'My.Computer.FileSystem.CreateDirectory(Application.StartupPath + "\Temp")

            Dim infile As New FileStream(Application.StartupPath + "\Temp\imga.bmp", FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write)

            infile.Write(donkey, 0, donkey.Length)
            infile.Close()
            Dim img As Image = Image.FromFile(Application.StartupPath + "\Temp\imga.bmp")
            newm.AddImageFromFile(Application.StartupPath + "\Temp\imga.bmp", mstore.Transparent(dkdk), mstore.TransparentColor(dkdk))
            img.Dispose()

            'My.Computer.FileSystem.DeleteDirectory(Application.StartupPath + "\Temp", FileIO.DeleteDirectoryOption.DeleteAllContents)
            My.Computer.FileSystem.DeleteFile(Application.StartupPath + "\Temp\imga.bmp")
        Next
        Return newm
    End Function

    Public Function AddMusic(ByVal Index As Integer) As Object 'bdMusic
        'On Error Resume Next
        'My.Computer.FileSystem.CreateDirectory(Application.StartupPath + "\TempM")

        'Dim infile As New FileStream(Application.StartupPath + "\TempM\snd" + index.tostring + ".mid", FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write)

        'infile.Write(mstore.MusicText(Index), 0, mstore.MusicText(index).Length)
        'infile.Close()

        'Dim newm As bdMusic = New bdMusic(Application.StartupPath + "\TempM\snd" + index.tostring + ".mid")
        'My.Computer.FileSystem.GetDirectoryInfo(Application.StartupPath + "\TempM").Attributes = FileAttributes.Hidden

        'Return newm
        Return Nothing
    End Function
    Public Function Add2DSound(ByVal Index As Integer) As Object 'bd2DSound


        'My.Computer.FileSystem.CreateDirectory(Application.StartupPath + "\Temp")

        'Dim infile As New FileStream(Application.StartupPath + "\Temp\snd.avi", FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write)

        'infile.Write(mstore.Sound2DTexts(Index), 0, mstore.Sound2DTexts(index).Length)
        'infile.Close()

        'Dim newm As bd2DSound = Game.Add2DSound(Application.StartupPath + "\Temp\snd.avi")

        'My.Computer.FileSystem.DeleteDirectory(Application.StartupPath + "\Temp", FileIO.DeleteDirectoryOption.DeleteAllContents)

        'Return newm
        Return Nothing
    End Function

    Public Function Add3DSound(ByVal Index As Integer) As Object 'bd3DSound


        'My.Computer.FileSystem.CreateDirectory(Application.StartupPath + "\Temp")

        'Dim infile As New FileStream(Application.StartupPath + "\Temp\snd.avi", FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write)

        'infile.Write(mstore.Sound3DTexts(Index), 0, mstore.Sound3DTexts(index).Length)
        'infile.Close()

        'Dim newm As bd3DSound = Game.Add3DSound(Application.StartupPath + "\Temp\snd.avi")

        'My.Computer.FileSystem.DeleteDirectory(Application.StartupPath + "\Temp", FileIO.DeleteDirectoryOption.DeleteAllContents)

        'Return newm
        Return Nothing
    End Function

    REM More Events Here

    Public mstore As New mstore
    Public WithEvents Game As New bdGame
    Public Camera As bdCamera

    REM More Declarations Here


End Class
