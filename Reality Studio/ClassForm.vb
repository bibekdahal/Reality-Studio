Public Class ClassForm
    Public ColorCoding As Boolean = False
    Public _colorcoder As SyntaxHighlighter

    Public ClassIndex As Integer

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click

        ToolStripButton3_Click(Me, Nothing)
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click
        Try
            Dim ggg As MainForm = Me.MdiParent
            ggg.mstore.ClassTexts(ClassIndex) = RichTextBox1.Text
            ggg.mstore.ClassNames(ClassIndex) = ToolStripTextBox1.Text
            ggg.TreeView1.Nodes("ClassNode").Nodes(ClassIndex).Text = ToolStripTextBox1.Text
        Catch
        End Try
    End Sub
    Private Sub CodeForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'ToolStrip1.Renderer = New ToolStripProfessionalRenderer(New MS2007Colors())
        SnippetButton.DropDownItems.Clear()
        For Each hhh As String In SnippetReader.GetSnippets
            SnippetButton.DropDownItems.Add(hhh)
            AddHandler SnippetButton.DropDownItems(SnippetButton.DropDownItems.Count - 1).Click, AddressOf SnippetButton_DropDownItemClicked
        Next
        Dim ggg As MainForm = Me.MdiParent
        Dim goru As String = ggg.mstore.ClassTexts(ClassIndex)
        RichTextBox1.Text = goru
        ToolStripTextBox1.Text = ggg.mstore.ClassNames(ClassIndex)
        Me.Text = "Edit Class: " + ggg.mstore.ClassNames(ClassIndex)
        _colorcoder.ColorAllRtb(Me)
    End Sub

    Private Sub RichTextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles RichTextBox1.KeyDown
        If e.Modifiers = Windows.Forms.Keys.Control Then Exit Sub
        If e.KeyCode = Windows.Forms.Keys.ControlKey Then Exit Sub
        If ColorCoding = True Then
            RichTextBox1.SelectionColor = Color.Black
        End If
    End Sub

    Private Sub RichTextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles RichTextBox1.KeyPress
        RichTextBox1_SelectionChanged(Me, Nothing)
        If e.KeyChar = Chr(Windows.Forms.Keys.Enter) Then
            e.Handled = True
            Dim pos As Integer
            pos = RichTextBox1.SelectionStart
            Dim lineNumber = RichTextBox1.GetLineFromCharIndex(pos) - 1
            Dim currentLineStr As String
            currentLineStr = RichTextBox1.Lines(lineNumber)
            Dim firstChar As Integer = 0
            While firstChar <> currentLineStr.Length
                If Not Char.IsWhiteSpace(currentLineStr(firstChar)) Then
                    Exit While
                Else
                    firstChar = firstChar + 1

                End If
            End While

            Dim indent As String
            indent = currentLineStr.Substring(0, firstChar)
            RichTextBox1.SelectedText = indent
        End If

    End Sub

    Private Sub RichTextBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles RichTextBox1.KeyUp
        If e.Modifiers = Windows.Forms.Keys.Control Then Exit Sub
        If e.KeyCode = Windows.Forms.Keys.ControlKey Then Exit Sub
        If ColorCoding = True Then
            If RichTextBox1.Text <> "" Then
                _colorcoder.ColorLines(Me, RichTextBox1.GetLineFromCharIndex(RichTextBox1.SelectionStart), RichTextBox1.GetLineFromCharIndex(RichTextBox1.SelectionStart + RichTextBox1.SelectionLength))
            End If
        End If
    End Sub

    Public Sub New(ByVal Index As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ClassIndex = Index
        _colorcoder = New SyntaxHighlighter(Me.RichTextBox1)
    End Sub

    Private Sub RichTextBox1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RichTextBox1.SelectionChanged
        CopyToolStripButton.Enabled = IIf(RichTextBox1.SelectionLength > 0, True, False)
        CutToolStripButton.Enabled = IIf(RichTextBox1.SelectionLength > 0, True, False)
        PasteToolStripButton.Enabled = IIf(Len(Clipboard.GetText) <> 0, True, False)
    End Sub

    Private Sub CutToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CutToolStripButton.Click, CutToolStripMenuItem.Click
        RichTextBox1.Cut()
        If ColorCoding = True Then
            _colorcoder.ColorLines(Me, RichTextBox1.GetLineFromCharIndex(RichTextBox1.SelectionStart), RichTextBox1.GetLineFromCharIndex(RichTextBox1.SelectionStart))
        End If
        RichTextBox1.Focus()
    End Sub

    Private Sub CopyToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyToolStripButton.Click, CopyToolStripMenuItem.Click
        RichTextBox1.Copy()
        If ColorCoding = True Then
            _colorcoder.ColorLines(Me, RichTextBox1.GetLineFromCharIndex(RichTextBox1.SelectionStart), RichTextBox1.GetLineFromCharIndex(RichTextBox1.SelectionStart))
        End If
        RichTextBox1.Focus()
    End Sub

    Private Sub PasteToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PasteToolStripButton.Click, PasteToolStripMenuItem.Click
        Dim firstChar As Integer = RichTextBox1.SelectionStart
        RichTextBox1.Paste(DataFormats.GetFormat(DataFormats.Text))
        RichTextBox1.SelectionColor = Color.Black
        If ColorCoding = True Then
            _colorcoder.ColorLines(Me, RichTextBox1.GetLineFromCharIndex(firstChar), RichTextBox1.GetLineFromCharIndex(RichTextBox1.SelectionStart))
        End If
        RichTextBox1.Focus()
    End Sub

    Private Sub RefreshToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshToolStripButton.Click, RefreshToolStripMenuItem.Click
        _colorcoder.ColorAllRtb(Me)
    End Sub

   
    Private Sub ToolStripButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton4.Click, CommentSelectedToolStripMenuItem.Click
        Dim ggg As Integer = RichTextBox1.SelectionStart
        Dim hhh As Integer = RichTextBox1.SelectionLength
        Dim cha As Integer = 0
        Dim iii As Integer = Me.RichTextBox1.GetLineFromCharIndex(RichTextBox1.SelectionStart)
        Dim kkk As Integer = Me.RichTextBox1.GetLineFromCharIndex(RichTextBox1.SelectionStart + RichTextBox1.SelectionLength - 1)
        For jjj As Integer = iii To kkk
            RichTextBox1.SelectionLength = 0
            RichTextBox1.SelectionStart = RichTextBox1.GetFirstCharIndexFromLine(jjj)
            RichTextBox1.SelectedText = "'"
            cha += 1
        Next
        RefreshToolStripButton_Click(Me, Nothing)
        RichTextBox1.SelectionStart = ggg + 1
        RichTextBox1.SelectionLength = hhh + cha - 1

    End Sub

    Private Sub ToolStripButton5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton5.Click, DecommentSelectedToolStripMenuItem.Click
        On Error Resume Next
        Dim ggg As Integer = RichTextBox1.SelectionStart
        Dim hhh As Integer = RichTextBox1.SelectionLength
        Dim cha As Integer = 0
        Dim iii As Integer = Me.RichTextBox1.GetLineFromCharIndex(RichTextBox1.SelectionStart)
        Dim kkk As Integer = Me.RichTextBox1.GetLineFromCharIndex(RichTextBox1.SelectionStart + RichTextBox1.SelectionLength)
        For jjj As Integer = iii To kkk
            If RichTextBox1.Lines(jjj).Contains("'") Then
                Dim jadugar As Integer = InStr("'", RichTextBox1.Lines(jjj))

                RichTextBox1.SelectionStart = RichTextBox1.GetFirstCharIndexFromLine(jjj) + jadugar
                RichTextBox1.SelectionLength = 1
                RichTextBox1.SelectedText = ""
                cha += 1
            End If
        Next
        RefreshToolStripButton_Click(Me, Nothing)
        If cha > 0 Then ggg -= 1
        RichTextBox1.SelectionStart = ggg
        If hhh < cha Then Exit Sub
        If cha < 1 Then Exit Sub

        RichTextBox1.SelectionLength = hhh - cha + 1
    End Sub

    Private Sub ToolStripButton6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton6.Click
        Dim od As New SaveFileDialog
        od.Filter = "Visual Basic Source File| *.vb"
        If od.ShowDialog = Windows.Forms.DialogResult.OK Then
            Try
                Me.RichTextBox1.SaveFile(od.FileName, RichTextBoxStreamType.PlainText)
            Catch ex As Exception
                MsgBox("Error saving the class to the file " + od.FileName)
            End Try
        End If
    End Sub

    Private Sub ToolStripButton7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton7.Click
        Dim od As New OpenFileDialog
        od.Filter = "Visual Basic Source File| *.vb"
        od.Multiselect = False
        If od.ShowDialog = Windows.Forms.DialogResult.OK Then
            Try
                Me.RichTextBox1.LoadFile(od.FileName, RichTextBoxStreamType.PlainText)
            Catch ex As Exception
                MsgBox("Error loading the class from the file " + od.FileName)
            End Try
        End If
        RefreshToolStripButton_Click(Me, Nothing)
    End Sub
    Private Sub RichTextBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RichTextBox1.TextChanged
        RichTextBox1.ClearUndo()
    End Sub
    Private Sub SnippetButton_DropDownItemClicked(ByVal sender As Object, ByVal e As EventArgs)
        Me.RichTextBox1.SelectedText = SnippetReader.StringList(SnippetButton.DropDownItems.IndexOf(sender))
        _colorcoder.ColorAllRtb(Me)
    End Sub
End Class