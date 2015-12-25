Public Class GameEditor
    
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        MainForm.mstore.AddEvent("<Event Name>")
        EventsBox.Items.Add("<Event Name>")
        EventsBox.SelectedIndex = EventsBox.Items.Count - 1
        Button8_Click(Me, Nothing)
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        If EventsBox.SelectedIndex < 0 Then Exit Sub
        MainForm.CurrentEventIndex = EventsBox.SelectedIndex
        If EventAdder.ShowDialog() = Windows.Forms.DialogResult.OK Then
            EventsBox.Items(MainForm.CurrentEventIndex) = MainForm.mstore.GetEvent(MainForm.CurrentEventIndex).EventName
        End If
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        If EventsBox.SelectedIndex < 0 Then Exit Sub
        MainForm.mstore.RemoveEvent(EventsBox.SelectedIndex)
        EventsBox.Items.RemoveAt(EventsBox.SelectedIndex)
        ActionsBox.Items.Clear()
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        If EventsBox.SelectedIndex < 0 Then Exit Sub
        Dim ggg As bdEvent = MainForm.mstore.GetEvent(EventsBox.SelectedIndex)
        ggg.ActionsCollection.Add("Add Code: ")
        ggg.ActionsTypes.Add(bdEvent.ActionType.TypeCode)
        ActionsBox.Items.Add("Add Code: ")
        ActionsBox.SelectedIndex = ActionsBox.Items.Count - 1
        ActionsBox_DoubleClick(Me, Nothing)
    End Sub

    Private Sub ActionsBox_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ActionsBox.DoubleClick
        If ActionsBox.SelectedIndex < 0 Then Exit Sub
        MainForm.CurrentEventIndex = EventsBox.SelectedIndex
        MainForm.CurrentActionIndex = ActionsBox.SelectedIndex
        If MainForm.mstore.GetEvent(MainForm.CurrentEventIndex).ActionsTypes(MainForm.CurrentActionIndex) = bdEvent.ActionType.TypeCode Then
            If CodeForm.ShowDialog = Windows.Forms.DialogResult.OK Then
                ActionsBox.Items(MainForm.CurrentActionIndex) = MainForm.mstore.GetEvent(MainForm.CurrentEventIndex).ActionsCollection(MainForm.CurrentActionIndex)
            End If
        End If
    End Sub

    Private Sub GameEditor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If MainForm.mstore.GetTotalEvents = 0 Then Exit Sub
        For i As Integer = 0 To MainForm.mstore.GetTotalEvents - 1
            EventsBox.Items.Add(MainForm.mstore.GetEvent(i).EventName)
        Next
        Try
            EventsBox.SelectedIndex = 0
        Catch

        End Try
    End Sub

    Private Sub EventsBox_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles EventsBox.DoubleClick
        Button8_Click(Me, Nothing)
    End Sub

    Private Sub EventsBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EventsBox.SelectedIndexChanged
        If EventsBox.SelectedIndex < 0 Then Exit Sub
        ActionsBox.Items.Clear()
        For Each ggg As String In MainForm.mstore.GetEvent(EventsBox.SelectedIndex).ActionsCollection
            ActionsBox.Items.Add(ggg)
        Next
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        If ActionsBox.SelectedIndex < 0 Then Exit Sub
        MainForm.mstore.GetEvent(EventsBox.SelectedIndex).ActionsCollection.RemoveAt(ActionsBox.SelectedIndex)
        MainForm.mstore.GetEvent(EventsBox.SelectedIndex).ActionsTypes.RemoveAt(ActionsBox.SelectedIndex)
        EventsBox_SelectedIndexChanged(Me, Nothing)
    End Sub

    
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If EventsBox.SelectedIndex < 0 Then Exit Sub
        If ActionsBox.SelectedIndex < 1 Then Exit Sub
        Dim currentInd As Integer = ActionsBox.SelectedIndex
        Dim currentItem As String = ActionsBox.SelectedItem
        ActionsBox.Items.RemoveAt(currentInd)
        ActionsBox.Items.Insert(currentInd - 1, currentItem)
        MainForm.mstore.GetEvent(EventsBox.SelectedIndex).ActionsCollection.RemoveAt(currentInd)
        Dim currentType = MainForm.mstore.GetEvent(EventsBox.SelectedIndex).ActionsTypes(currentInd)
        MainForm.mstore.GetEvent(EventsBox.SelectedIndex).ActionsTypes.RemoveAt(currentInd)
        MainForm.mstore.GetEvent(EventsBox.SelectedIndex).ActionsCollection.Insert(currentInd - 1, currentItem)
        MainForm.mstore.GetEvent(EventsBox.SelectedIndex).ActionsTypes.Insert(currentInd - 1, currentType)
        ActionsBox.SelectedIndex = currentInd - 1
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If EventsBox.SelectedIndex < 0 Then Exit Sub
        If ActionsBox.SelectedIndex < 0 Or ActionsBox.SelectedIndex > ActionsBox.Items.Count - 2 Then Exit Sub
        Dim currentInd As Integer = ActionsBox.SelectedIndex
        Dim currentItem As String = ActionsBox.SelectedItem
        ActionsBox.Items.RemoveAt(currentInd)
        ActionsBox.Items.Insert(currentInd + 1, currentItem)
        MainForm.mstore.GetEvent(EventsBox.SelectedIndex).ActionsCollection.RemoveAt(currentInd)
        Dim currentType = MainForm.mstore.GetEvent(EventsBox.SelectedIndex).ActionsTypes(currentInd)
        MainForm.mstore.GetEvent(EventsBox.SelectedIndex).ActionsTypes.RemoveAt(currentInd)
        MainForm.mstore.GetEvent(EventsBox.SelectedIndex).ActionsCollection.Insert(currentInd + 1, currentItem)
        MainForm.mstore.GetEvent(EventsBox.SelectedIndex).ActionsTypes.Insert(currentInd + 1, currentType)
        ActionsBox.SelectedIndex = currentInd + 1
    End Sub
End Class