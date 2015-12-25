Imports System
Imports System.IO
Public Class bdSpriteStore
    Private _imageTexts As List(Of Byte()) = New List(Of Byte())
    Private _imageNames As New List(Of String)
    Public Property ImageTexts() As List(Of Byte())
        Get
            Return _imageTexts
        End Get
        Set(ByVal value As List(Of Byte()))
            _imageTexts = value
        End Set
    End Property
    Public Property ImageNames() As List(Of String)
        Get
            Return _imageNames
        End Get
        Set(ByVal value As List(Of String))
            _imageNames = value
        End Set
    End Property
End Class
