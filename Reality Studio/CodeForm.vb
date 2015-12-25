Imports System.Windows.Forms

Public Class CodeForm
    Public ColorCoding As Boolean = False
    Public _colorcoder As SyntaxHighlighter

    Private EventIndex As Integer
    Private ActionIndex As Integer

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        MainForm.mstore.GetEvent(EventIndex).ActionsCollection(ActionIndex) = "Add Code: " + RichTextBox1.Text

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub CodeForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'ToolStrip1.Renderer = New ToolStripProfessionalRenderer(New MS2007Colors())
        SnippetButton.DropDownItems.Clear()
        For Each ggg As String In SnippetReader.GetSnippets
            SnippetButton.DropDownItems.Add(ggg)
            AddHandler SnippetButton.DropDownItems(SnippetButton.DropDownItems.Count - 1).Click, AddressOf SnippetButton_DropDownItemClicked
        Next
        EventIndex = MainForm.CurrentEventIndex
        ActionIndex = MainForm.CurrentActionIndex

        Dim goru As String = Mid(MainForm.mstore.GetEvent(EventIndex).ActionsCollection(ActionIndex), 1 + "Add Code: ".Length)
        RichTextBox1.Text = goru
        _colorcoder.ColorAllRtb(Me)
    End Sub


    Private Sub RichTextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles RichTextBox1.KeyDown
        If e.Modifiers = Keys.Control Then Exit Sub
        If e.KeyCode = Keys.ControlKey Then Exit Sub
        If ColorCoding = True Then
            RichTextBox1.SelectionColor = Color.Black
        End If
    End Sub

    Private Sub RichTextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles RichTextBox1.KeyPress
        RichTextBox1_SelectionChanged(Me, Nothing)
        If e.KeyChar = Chr(Keys.Enter) Then
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
        If e.Modifiers = Keys.Control Then Exit Sub
        If e.KeyCode = Keys.ControlKey Then Exit Sub
        If ColorCoding = True Then
            If RichTextBox1.Text <> "" Then
                _colorcoder.ColorLines(Me, RichTextBox1.GetLineFromCharIndex(RichTextBox1.SelectionStart), RichTextBox1.GetLineFromCharIndex(RichTextBox1.SelectionStart + RichTextBox1.SelectionLength))
            End If
        End If
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
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
    Private Sub RichTextBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RichTextBox1.TextChanged
        RichTextBox1.ClearUndo()
    End Sub

    Private Sub SnippetButton_DropDownItemClicked(ByVal sender As Object, ByVal e As EventArgs)
        Me.RichTextBox1.SelectedText = SnippetReader.StringList(SnippetButton.DropDownItems.IndexOf(sender))
        _colorcoder.ColorAllRtb(Me)
    End Sub
End Class
