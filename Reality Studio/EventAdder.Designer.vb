<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EventAdder
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.OK_Button = New System.Windows.Forms.Button
        Me.Cancel_Button = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.TextBox2 = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.TextBox3 = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.TemplatesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.LoadEventToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AfterRenderEventToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.BeforeRenderEventToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.BeginRenderEventToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.EndRenderEventToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DeviceResetEventToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DeviceLostEventToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DeviceDisposingEventToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.TableLayoutPanel1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(178, 156)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 4
        Me.OK_Button.Text = "OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 5
        Me.Cancel_Button.Text = "Cancel"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 43)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Event Name:"
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.Color.White
        Me.TextBox1.Location = New System.Drawing.Point(93, 36)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(228, 20)
        Me.TextBox1.TabIndex = 1
        '
        'TextBox2
        '
        Me.TextBox2.BackColor = System.Drawing.Color.White
        Me.TextBox2.Location = New System.Drawing.Point(93, 66)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(228, 20)
        Me.TextBox2.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 73)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(78, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Event Handler:"
        '
        'TextBox3
        '
        Me.TextBox3.BackColor = System.Drawing.Color.White
        Me.TextBox3.Location = New System.Drawing.Point(93, 95)
        Me.TextBox3.Multiline = True
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextBox3.Size = New System.Drawing.Size(228, 52)
        Me.TextBox3.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(11, 102)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(60, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Arguments:"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TemplatesToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.MenuStrip1.Size = New System.Drawing.Size(336, 24)
        Me.MenuStrip1.TabIndex = 6
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'TemplatesToolStripMenuItem
        '
        Me.TemplatesToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LoadEventToolStripMenuItem, Me.ToolStripSeparator2, Me.AfterRenderEventToolStripMenuItem, Me.BeforeRenderEventToolStripMenuItem, Me.ToolStripSeparator3, Me.BeginRenderEventToolStripMenuItem, Me.EndRenderEventToolStripMenuItem, Me.ToolStripSeparator1, Me.DeviceResetEventToolStripMenuItem, Me.DeviceLostEventToolStripMenuItem, Me.DeviceDisposingEventToolStripMenuItem})
        Me.TemplatesToolStripMenuItem.Name = "TemplatesToolStripMenuItem"
        Me.TemplatesToolStripMenuItem.Size = New System.Drawing.Size(68, 20)
        Me.TemplatesToolStripMenuItem.Text = "Templates"
        '
        'LoadEventToolStripMenuItem
        '
        Me.LoadEventToolStripMenuItem.Name = "LoadEventToolStripMenuItem"
        Me.LoadEventToolStripMenuItem.Size = New System.Drawing.Size(196, 22)
        Me.LoadEventToolStripMenuItem.Text = "Load Event"
        '
        'AfterRenderEventToolStripMenuItem
        '
        Me.AfterRenderEventToolStripMenuItem.Name = "AfterRenderEventToolStripMenuItem"
        Me.AfterRenderEventToolStripMenuItem.Size = New System.Drawing.Size(196, 22)
        Me.AfterRenderEventToolStripMenuItem.Text = "After Render Event"
        '
        'BeforeRenderEventToolStripMenuItem
        '
        Me.BeforeRenderEventToolStripMenuItem.Name = "BeforeRenderEventToolStripMenuItem"
        Me.BeforeRenderEventToolStripMenuItem.Size = New System.Drawing.Size(196, 22)
        Me.BeforeRenderEventToolStripMenuItem.Text = "Before Render Event"
        '
        'BeginRenderEventToolStripMenuItem
        '
        Me.BeginRenderEventToolStripMenuItem.Name = "BeginRenderEventToolStripMenuItem"
        Me.BeginRenderEventToolStripMenuItem.Size = New System.Drawing.Size(196, 22)
        Me.BeginRenderEventToolStripMenuItem.Text = "Begin Render Event"
        '
        'EndRenderEventToolStripMenuItem
        '
        Me.EndRenderEventToolStripMenuItem.Name = "EndRenderEventToolStripMenuItem"
        Me.EndRenderEventToolStripMenuItem.Size = New System.Drawing.Size(196, 22)
        Me.EndRenderEventToolStripMenuItem.Text = "End Render Event"
        '
        'DeviceResetEventToolStripMenuItem
        '
        Me.DeviceResetEventToolStripMenuItem.Name = "DeviceResetEventToolStripMenuItem"
        Me.DeviceResetEventToolStripMenuItem.Size = New System.Drawing.Size(196, 22)
        Me.DeviceResetEventToolStripMenuItem.Text = "Device Reset Event"
        '
        'DeviceLostEventToolStripMenuItem
        '
        Me.DeviceLostEventToolStripMenuItem.Name = "DeviceLostEventToolStripMenuItem"
        Me.DeviceLostEventToolStripMenuItem.Size = New System.Drawing.Size(196, 22)
        Me.DeviceLostEventToolStripMenuItem.Text = "Device Lost Event"
        '
        'DeviceDisposingEventToolStripMenuItem
        '
        Me.DeviceDisposingEventToolStripMenuItem.Name = "DeviceDisposingEventToolStripMenuItem"
        Me.DeviceDisposingEventToolStripMenuItem.Size = New System.Drawing.Size(196, 22)
        Me.DeviceDisposingEventToolStripMenuItem.Text = "Device Disposing Event"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(193, 6)
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(193, 6)
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(193, 6)
        '
        'EventAdder
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(336, 183)
        Me.Controls.Add(Me.TextBox3)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "EventAdder"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Event Properties"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents TemplatesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LoadEventToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AfterRenderEventToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BeforeRenderEventToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BeginRenderEventToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EndRenderEventToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeviceResetEventToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeviceLostEventToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeviceDisposingEventToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator

End Class
