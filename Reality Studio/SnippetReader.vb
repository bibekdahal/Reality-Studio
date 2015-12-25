Imports System.IO
Public Class SnippetReader
    Private Shared currentText As String = ""
    Private Shared Reading As Boolean = False
    Public Shared StringList As New List(Of String)
    Public Shared Function GetSnippets() As List(Of String)
        Dim ggg As New List(Of String)
        Dim fs As StreamReader = My.Computer.FileSystem.OpenTextFileReader(Application.StartupPath + "\Code Snippets.txt")
        Reading = False
        While Not fs.EndOfStream
            ParseString(fs.ReadLine, ggg)
        End While
        fs.Close()
        Return ggg
    End Function

    Private Shared Sub ParseString(ByVal Str As String, ByRef strlst As List(Of String))
        If Trim(Str).StartsWith("#") Then
            If Trim(Str).Length > 1 Then strlst.Add(Mid(Str, 2))
            If Reading = False Then
                Reading = True
            Else
                StringList.Add(currentText)
                currentText = ""
            End If
        Else
            If Reading = True Then
                currentText += Str + Environment.NewLine
            End If
        End If
    End Sub
End Class
