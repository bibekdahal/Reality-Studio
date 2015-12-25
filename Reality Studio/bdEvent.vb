
Public Class bdEvent
    Private EvntName As String
    Private EvntHandler As String
    Private EvntArgs As String
    Private ActionCollection As List(Of String) = New List(Of String)
    Private ActionTypes As List(Of ActionType) = New List(Of ActionType)

    Public Enum ActionType
        TypeCode = 0
    End Enum

    Public Property EventName() As String
        Get
            Return EvntName
        End Get
        Set(ByVal value As String)
            EvntName = value
        End Set
    End Property
    Public Property EventHandler() As String
        Get
            Return EvntHandler
        End Get
        Set(ByVal value As String)
            EvntHandler = value
        End Set
    End Property
    Public Property EventArguments() As String
        Get
            Return EvntArgs
        End Get
        Set(ByVal value As String)
            EvntArgs = value
        End Set
    End Property

    Public Property ActionsCollection() As List(Of String)
        Get
            Return ActionCollection
        End Get
        Set(ByVal value As List(Of String))
            ActionCollection = value
        End Set
    End Property
    Public Property ActionsTypes() As List(Of ActionType)
        Get
            Return ActionTypes
        End Get
        Set(ByVal value As List(Of ActionType))
            ActionTypes = value
        End Set
    End Property
    Public Function GetActionType(ByVal ActionName As String) As ActionType
        If ActionName.StartsWith("Add Code: ") Then
            Return ActionType.TypeCode
        Else
            Return Nothing
        End If
    End Function
End Class
