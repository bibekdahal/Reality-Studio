Imports System.Windows.Forms

Public Class EventAdder
    Private _index As Integer
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        MainForm.mstore.GetEvent(_index).EventName = TextBox1.Text
        MainForm.mstore.GetEvent(_index).EventHandler = TextBox2.Text
        MainForm.mstore.GetEvent(_index).EventArguments = TextBox3.Text
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub


    Private Sub EventAdder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _index = MainForm.CurrentEventIndex
        SetText(MainForm.mstore.GetEvent(_index).EventName, _
                 MainForm.mstore.GetEvent(_index).EventHandler, _
                 MainForm.mstore.GetEvent(_index).EventArguments)
    End Sub

    Private Sub LoadEventToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadEventToolStripMenuItem.Click
        SetText("Load_Event", "Me.Load", "ByVal sender As Object, ByVal e As System.EventArgs")
    End Sub

    Private Sub SetText(ByVal EventName As String, ByVal EventHandler As String, Optional ByVal EventArguments As String = "")
        TextBox1.Text = EventName
        TextBox2.Text = EventHandler
        TextBox3.Text = EventArguments
    End Sub

    Private Sub AfterRenderEventToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AfterRenderEventToolStripMenuItem.Click
        SetText("After_Render_Event", "Game.AfterRender")
    End Sub

    Private Sub BeforeRenderEventToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BeforeRenderEventToolStripMenuItem.Click
        SetText("Before_Render_Event", "Game.BeforeRender")
    End Sub

    Private Sub BeginRenderEventToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BeginRenderEventToolStripMenuItem.Click
        SetText("Begin_Render_Event", "Game.RenderBegin")
    End Sub

    Private Sub EndRenderEventToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EndRenderEventToolStripMenuItem.Click
        SetText("End_Render_Event", "Game.RenderEnd")
    End Sub

    Private Sub DeviceResetEventToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeviceResetEventToolStripMenuItem.Click
        SetText("Device_Reset_Event", "Game.DeviceReset")
    End Sub

    Private Sub DeviceLostEventToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeviceLostEventToolStripMenuItem.Click
        SetText("Device_Lost_Event", "Game.DeviceLost")
    End Sub

    Private Sub DeviceDisposingEventToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeviceDisposingEventToolStripMenuItem.Click
        SetText("Device_Disposing_Event", "Game.DeviceDisposing")
    End Sub
End Class
