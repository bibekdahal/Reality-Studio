<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GameEditor
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GameEditor))
        Me.Button3 = New System.Windows.Forms.Button
        Me.EventsBox = New System.Windows.Forms.ListBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.ActionsBox = New System.Windows.Forms.ListBox
        Me.Button4 = New System.Windows.Forms.Button
        Me.Button5 = New System.Windows.Forms.Button
        Me.Button6 = New System.Windows.Forms.Button
        Me.Button7 = New System.Windows.Forms.Button
        Me.Button8 = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Button3
        '
        Me.Button3.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button3.Location = New System.Drawing.Point(458, 331)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(97, 26)
        Me.Button3.TabIndex = 7
        Me.Button3.Text = "CLOSE"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'EventsBox
        '
        Me.EventsBox.BackColor = System.Drawing.Color.White
        Me.EventsBox.FormattingEnabled = True
        Me.EventsBox.Location = New System.Drawing.Point(12, 29)
        Me.EventsBox.Name = "EventsBox"
        Me.EventsBox.Size = New System.Drawing.Size(200, 264)
        Me.EventsBox.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(15, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(43, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Events:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(230, 13)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(45, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Actions:"
        '
        'ActionsBox
        '
        Me.ActionsBox.BackColor = System.Drawing.Color.White
        Me.ActionsBox.FormattingEnabled = True
        Me.ActionsBox.Location = New System.Drawing.Point(227, 29)
        Me.ActionsBox.Name = "ActionsBox"
        Me.ActionsBox.Size = New System.Drawing.Size(200, 264)
        Me.ActionsBox.TabIndex = 5
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(12, 299)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(96, 28)
        Me.Button4.TabIndex = 2
        Me.Button4.Text = "Add Event"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(115, 299)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(97, 28)
        Me.Button5.TabIndex = 3
        Me.Button5.Text = "Remove Event"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(270, 299)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(97, 28)
        Me.Button6.TabIndex = 6
        Me.Button6.Text = "Remove Action"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'Button7
        '
        Me.Button7.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button7.Location = New System.Drawing.Point(470, 29)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(85, 264)
        Me.Button7.TabIndex = 7
        Me.Button7.TabStop = False
        Me.Button7.Text = "Add Code"
        Me.Button7.UseVisualStyleBackColor = True
        '
        'Button8
        '
        Me.Button8.Location = New System.Drawing.Point(66, 331)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(94, 27)
        Me.Button8.TabIndex = 4
        Me.Button8.Text = "Change Event"
        Me.Button8.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"), System.Drawing.Image)
        Me.Button1.Location = New System.Drawing.Point(434, 56)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(30, 30)
        Me.Button1.TabIndex = 8
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Image = CType(resources.GetObject("Button2.Image"), System.Drawing.Image)
        Me.Button2.Location = New System.Drawing.Point(434, 92)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(30, 30)
        Me.Button2.TabIndex = 9
        Me.Button2.UseVisualStyleBackColor = True
        '
        'GameEditor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.Button3
        Me.ClientSize = New System.Drawing.Size(569, 369)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Button7)
        Me.Controls.Add(Me.Button8)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ActionsBox)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.EventsBox)
        Me.Controls.Add(Me.Button3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "GameEditor"
        Me.Text = "Game Controller"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents EventsBox As System.Windows.Forms.ListBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ActionsBox As System.Windows.Forms.ListBox
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents Button8 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
End Class
