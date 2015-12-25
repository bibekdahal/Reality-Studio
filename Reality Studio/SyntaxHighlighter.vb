
Public Class SyntaxHighlighter
    Private Declare Function SendMessage Lib "user32" Alias "SendMessageA" _
       (ByVal hWnd As IntPtr, ByVal wMsg As Integer, ByVal wParam As Integer, _
        ByVal lParam As Integer) As Integer
    Private Declare Function GetScrollPos Lib "user32" Alias "GetScrollPos" (ByVal hWnd As IntPtr, ByVal NBar As Integer) As Integer
    Private Declare Function SetScrollPos Lib "user32" Alias "SetScrollPos" (ByVal hWnd As IntPtr, ByVal nBar As Integer, ByVal nPos As Integer, ByVal bRedraw As Boolean) As Integer
    Private Declare Function PostMessageA Lib "user32" Alias "PostMessageA" (ByVal hWnd As IntPtr, ByVal nBar As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Boolean

    Private Declare Function LockWindowUpdate Lib "user32" (ByVal hWnd As Integer) As Integer
    Private _rtb As System.Windows.Forms.RichTextBox
    Private _SyntaxHighlight_CaseSensitive As Boolean = False
    Friend Words As New DataTable
    'Contains Windows Messages for the SendMessage API call
    Private Enum EditMessages
        LineIndex = 187
        LineFromChar = 201
        GetFirstVisibleLine = 206
        CharFromPos = 215
        PosFromChar = 1062
    End Enum


    Private Const SB_HORZ As Integer = 0
    Private Const SB_VERT As Integer = 1
    Private Const WM_HSCROLL As Integer = 276
    Private Const WM_VSCROLL As Integer = 277
    Private Const SB_THUMBPOSITION As Integer = 4

    Private Property HScrollPos() As Integer
        Get
            Return GetScrollPos(_rtb.Handle.ToInt32, SB_HORZ)
        End Get
        Set(ByVal value As Integer)
            SetScrollPos(DirectCast(_rtb.Handle, IntPtr), SB_HORZ, value, True)
            PostMessageA(DirectCast(_rtb.Handle, IntPtr), WM_HSCROLL, SB_THUMBPOSITION + 65536 * value, 0)
        End Set
    End Property

    Private Property VScrollPos() As Integer
        Get
            Return GetScrollPos(_rtb.Handle.ToInt32, SB_VERT)
        End Get
        Set(ByVal value As Integer)
            SetScrollPos(DirectCast(_rtb.Handle, IntPtr), SB_VERT, value, True)
            PostMessageA(DirectCast(_rtb.Handle, IntPtr), WM_VSCROLL, SB_THUMBPOSITION + 65536 * value, 0)
        End Set
    End Property

    Public Sub New(ByVal RichTextBox As System.Windows.Forms.RichTextBox)
        _rtb = RichTextBox
        AddSyntax()
    End Sub

    Private Sub AddSyntax()
        ClearSyntaxWords()
        '----------------vb keywords
        AddSyntaxWord("\b(addhandler|addressof|alias|and|andalso|as|boolean|byref|byte|byval|call|case|catch|cbool|cbyte|cchar|" _
        & "cdate|cdec|cdbl|char|cint|class|clng|cobj|const|continue|csbyte|cshort|csng|cstr|ctype|cuint|culng|" _
        & "cushort|date|decimal|declare|default|delegate|dim|directcast|do|double|each|else|elseif|" _
        & "end|endif|enum|erase|error|event|exit|false|finally|for|friend|function|get|gettype|global|" _
        & "gosub|goto|handles|if|implements|imports|in|inherits|integer|interface|is|isnot|let|lib|" _
        & "like|long|loop|me|mod|module|mustinherit|mustoverride|mybase|myclass|namespace|narrowing|" _
        & "my|new|next|not|nothing|notinheritable|notoverridable|object|of|on|operator|option|optional|" _
        & "or|orelse|overloads|overridable|overrides|paramarray|partial|private|property|protected|public|" _
        & "raiseevent|readonly|redim|rem|removehandler|resume|return|sbyte|select|" _
        & "set|shadows|shared|short|single|static|step|stop|string|structure|sub|synclock|then|throw|to|true|" _
        & "try|trycast|typeof|variant|wend|uinteger|ulong|ushort|using|when|while|widening|with|withevents|" _
        & "writeonly|xor|#const|#else|#elseif|#end|#if)\b", Color.Blue) '|-|&|&=|*|*=|/|/=|\|\=|^|^=|+|+=|=|-=|" _
        '----------------bdGameEngine keywords
        AddSyntaxWord("\b(bd2dinstance|bd2dinstancecollection|bd2dobject|bd2dsound|bd2dtext" _
        & "|bd3dinstance|bd3dinstancecollection|bd3dobject|bd3dsound|bdcamera" _
        & "|bdcollisionsphere|bdimage|bdkeyboardinput|bdmaingame|bdmesh|bdmouseinput" _
        & "|bdsprite|bdwall)\b", Color.DeepSkyBlue)


        AddSyntaxWord("'.*", Color.Green)
        AddSyntaxWord("(?<!\'.*)\""[^""]*\""", Color.DarkRed)

        'AddSyntaxWord("\""[^""]*\""", Color.DarkRed)

    End Sub

    Public Function ClearSyntaxWords()
        Words = New DataTable
        'Load all the keywords and the colors to make them 
        Words.Columns.Add("Word")
        Words.PrimaryKey = New DataColumn() {Words.Columns(0)}
        Words.Columns.Add("Color")
        Return True
    End Function

    Public Function AddSyntaxWord(ByVal strWord As String, ByVal clrColor As Color)
        Dim MyRow As DataRow
        MyRow = Words.NewRow()
        MyRow("Word") = strWord
        MyRow("Color") = clrColor.Name
        Words.Rows.Add(MyRow)
        Return True
    End Function

    Public Sub ColorAllRtb(ByVal Sender As Object)

        Sender.ColorCoding = False
        Dim vvv As Long = VScrollPos
        Dim hhh As Long = HScrollPos

        RefreshAll()

        VScrollPos = vvv
        HScrollPos = hhh
        Sender.ColorCoding = True
    End Sub
    Public Sub ColorLines(ByVal Sender As Object, ByVal FirstLineNumber As Integer, ByVal LastLineNumber As Integer)

        Sender.ColorCoding = False
        Dim vvv As Long = VScrollPos
        Dim hhh As Long = HScrollPos
        If FirstLineNumber = LastLineNumber Then
            ColorLine(FirstLineNumber)
        Else
            ColorMultipleLines(FirstLineNumber, LastLineNumber)
        End If
        VScrollPos = vvv
        HScrollPos = hhh
        Sender.ColorCoding = True
    End Sub

    Public Sub ColorPartOfText(ByVal Sender As Object, ByVal FirstCharIndex As Integer, ByVal LastCharIndex As Integer)

        Sender.ColorCoding = False
        Dim vvv As Long = VScrollPos
        Dim hhh As Long = HScrollPos
        ColorTextPart(FirstCharIndex, LastCharIndex)
        VScrollPos = vvv
        HScrollPos = hhh
        Sender.ColorCoding = True
    End Sub

    Private Sub RefreshAll()
        On Error Resume Next
        Dim i As Integer = 0
        Dim SelectionAt As Integer = _rtb.SelectionStart
        Dim SelectionLen As Integer = _rtb.SelectionLength
        Dim SelColor As Color = _rtb.SelectionColor
        Dim MyRow As DataRow

        ' Lock the update
        LockWindowUpdate(_rtb.Handle.ToInt32)

        _rtb.SelectionStart = 1
        _rtb.SelectionLength = _rtb.Text.Length
        _rtb.SelectionColor = Color.Black

        'Check for matches in a particular line number
        Dim rm As System.Text.RegularExpressions.MatchCollection
        Dim m As System.Text.RegularExpressions.Match

        For Each MyRow In Words.Rows

            rm = System.Text.RegularExpressions.Regex.Matches(_rtb.Text.ToLower, MyRow("Word"))
            For Each m In rm
                _rtb.SelectionStart = m.Index
                _rtb.SelectionLength = m.Length
                _rtb.SelectionColor = Color.FromName(MyRow("color"))
            Next
        Next

        ' Restore the selectionstart
        _rtb.SelectionStart = SelectionAt
        _rtb.SelectionLength = SelectionLen
        _rtb.SelectionColor = SelColor
        ' Unlock the update
        LockWindowUpdate(0)
    End Sub

    Private Sub ColorLine(ByVal LineNumber As Integer)
        On Error Resume Next
        Dim i As Integer = 0
        Dim SelectionAt As Integer = _rtb.SelectionStart
        Dim SelectionLen As Integer = _rtb.SelectionLength
        Dim SelColor As Color = _rtb.SelectionColor
        Dim MyRow As DataRow

        ' Lock the update
        LockWindowUpdate(_rtb.Handle.ToInt32)

        _rtb.SelectionStart = _rtb.GetFirstCharIndexFromLine(LineNumber)
        _rtb.SelectionLength = _rtb.Lines(LineNumber).Length
        _rtb.SelectionColor = Color.Black
        'Check for matches in a particular line number
        Dim rm As System.Text.RegularExpressions.MatchCollection
        Dim m As System.Text.RegularExpressions.Match

        For Each MyRow In Words.Rows

            rm = System.Text.RegularExpressions.Regex.Matches(_rtb.Lines(LineNumber).ToLower, MyRow("Word"))
            For Each m In rm
                _rtb.SelectionStart = _rtb.GetFirstCharIndexFromLine(LineNumber) + m.Index
                _rtb.SelectionLength = m.Length
                _rtb.SelectionColor = Color.FromName(MyRow("color"))
            Next
        Next

        ' Restore the selectionstart
        _rtb.SelectionStart = SelectionAt
        _rtb.SelectionLength = SelectionLen
        _rtb.SelectionColor = SelColor
        ' Unlock the update
        LockWindowUpdate(0)
    End Sub

    Private Sub ColorMultipleLines(ByVal FirstLineNumber As Integer, ByVal LastLineNumber As Integer)
        On Error Resume Next
        Dim i As Integer = 0
        Dim SelectionAt As Integer = _rtb.SelectionStart
        Dim SelectionLen As Integer = _rtb.SelectionLength
        Dim SelColor As Color = _rtb.SelectionColor
        Dim MyRow As DataRow

        ' Lock the update
        LockWindowUpdate(_rtb.Handle.ToInt32)

        Dim textToCheck As String = ""
        For ii As Integer = FirstLineNumber To LastLineNumber
            textToCheck = textToCheck + _rtb.Lines(ii) + Chr(10)
        Next

        _rtb.SelectionStart = _rtb.GetFirstCharIndexFromLine(FirstLineNumber)
        _rtb.SelectionLength = textToCheck.Length
        _rtb.SelectionColor = Color.Black

        'Check for matches in a particular line number
        Dim rm As System.Text.RegularExpressions.MatchCollection
        Dim m As System.Text.RegularExpressions.Match

        For Each MyRow In Words.Rows

            rm = System.Text.RegularExpressions.Regex.Matches(textToCheck.ToLower, MyRow("Word"))
            For Each m In rm
                _rtb.SelectionStart = _rtb.GetFirstCharIndexFromLine(FirstLineNumber) + m.Index
                _rtb.SelectionLength = m.Length
                _rtb.SelectionColor = Color.FromName(MyRow("color"))
            Next
        Next

        ' Restore the selectionstart
        _rtb.SelectionStart = SelectionAt
        _rtb.SelectionLength = SelectionLen
        _rtb.SelectionColor = SelColor
        ' Unlock the update
        LockWindowUpdate(0)
    End Sub

    Private Sub ColorTextPart(ByVal FirstCharIndex As Integer, ByVal LastCharIndex As Integer)
        On Error Resume Next
        Dim i As Integer = 0
        Dim SelectionAt As Integer = _rtb.SelectionStart
        Dim SelectionLen As Integer = _rtb.SelectionLength
        Dim SelColor As Color = _rtb.SelectionColor
        Dim MyRow As DataRow

        ' Lock the update
        LockWindowUpdate(_rtb.Handle.ToInt32)

        Dim texttocheck As String
        _rtb.SelectionStart = FirstCharIndex
        _rtb.SelectionLength = LastCharIndex - FirstCharIndex
        texttocheck = _rtb.SelectedText

        'Check for matches in a particular line number
        Dim rm As System.Text.RegularExpressions.MatchCollection
        Dim m As System.Text.RegularExpressions.Match

        For Each MyRow In Words.Rows

            rm = System.Text.RegularExpressions.Regex.Matches(texttocheck.ToLower, MyRow("Word"))
            For Each m In rm
                _rtb.SelectionStart = FirstCharIndex + m.Index
                _rtb.SelectionLength = m.Length
                _rtb.SelectionColor = Color.FromName(MyRow("color"))
            Next
        Next

        ' Restore the selectionstart
        _rtb.SelectionStart = SelectionAt
        _rtb.SelectionLength = SelectionLen
        _rtb.SelectionColor = SelColor
        ' Unlock the update
        LockWindowUpdate(0)
    End Sub

End Class
