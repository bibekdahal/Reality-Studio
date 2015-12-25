<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MeshViewer
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
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.SphrBox = New System.Windows.Forms.ListBox
        Me.Button1 = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        Me.Button3 = New System.Windows.Forms.Button
        Me.Button4 = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.Button5 = New System.Windows.Forms.Button
        Me.Button6 = New System.Windows.Forms.Button
        Me.Button7 = New System.Windows.Forms.Button
        Me.Button8 = New System.Windows.Forms.Button
        Me.Button9 = New System.Windows.Forms.Button
        Me.Button10 = New System.Windows.Forms.Button
        Me.BoxBox = New System.Windows.Forms.ListBox
        Me.Label3 = New System.Windows.Forms.Label
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox1.Location = New System.Drawing.Point(0, 1)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(437, 317)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(450, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(90, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Collision Spheres:"
        '
        'SphrBox
        '
        Me.SphrBox.BackColor = System.Drawing.Color.White
        Me.SphrBox.FormattingEnabled = True
        Me.SphrBox.Location = New System.Drawing.Point(453, 19)
        Me.SphrBox.Name = "SphrBox"
        Me.SphrBox.Size = New System.Drawing.Size(146, 121)
        Me.SphrBox.TabIndex = 0
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(605, 19)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(27, 121)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "ADD"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(453, 146)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(146, 26)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "REMOVE"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(453, 233)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(66, 31)
        Me.Button3.TabIndex = 3
        Me.Button3.Text = "OK"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(525, 233)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(64, 31)
        Me.Button4.TabIndex = 4
        Me.Button4.Text = "CANCEL"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(451, 267)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(43, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Radius:"
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(453, 283)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(88, 25)
        Me.Button5.TabIndex = 8
        Me.Button5.Text = "Move Far Back"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(547, 283)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(88, 25)
        Me.Button6.TabIndex = 9
        Me.Button6.Text = "Move Far Front"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'Button7
        '
        Me.Button7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button7.Location = New System.Drawing.Point(453, 177)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(146, 22)
        Me.Button7.TabIndex = 10
        Me.Button7.Text = "Save Spheres Data"
        Me.Button7.UseVisualStyleBackColor = True
        '
        'Button8
        '
        Me.Button8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button8.Location = New System.Drawing.Point(453, 205)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(146, 22)
        Me.Button8.TabIndex = 11
        Me.Button8.Text = "Load Spheres Data"
        Me.Button8.UseVisualStyleBackColor = True
        '
        'Button9
        '
        Me.Button9.Location = New System.Drawing.Point(638, 146)
        Me.Button9.Name = "Button9"
        Me.Button9.Size = New System.Drawing.Size(146, 26)
        Me.Button9.TabIndex = 15
        Me.Button9.Text = "REMOVE"
        Me.Button9.UseVisualStyleBackColor = True
        '
        'Button10
        '
        Me.Button10.Location = New System.Drawing.Point(790, 19)
        Me.Button10.Name = "Button10"
        Me.Button10.Size = New System.Drawing.Size(27, 121)
        Me.Button10.TabIndex = 14
        Me.Button10.Text = "ADD"
        Me.Button10.UseVisualStyleBackColor = True
        '
        'BoxBox
        '
        Me.BoxBox.BackColor = System.Drawing.Color.White
        Me.BoxBox.FormattingEnabled = True
        Me.BoxBox.Location = New System.Drawing.Point(638, 19)
        Me.BoxBox.Name = "BoxBox"
        Me.BoxBox.Size = New System.Drawing.Size(146, 121)
        Me.BoxBox.TabIndex = 12
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(635, 3)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(77, 13)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "Collision Boxes"
        '
        'MeshViewer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(816, 320)
        Me.Controls.Add(Me.Button9)
        Me.Controls.Add(Me.Button10)
        Me.Controls.Add(Me.BoxBox)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Button8)
        Me.Controls.Add(Me.Button7)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.SphrBox)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PictureBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "MeshViewer"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Mesh Viewer"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents SphrBox As System.Windows.Forms.ListBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents Button8 As System.Windows.Forms.Button
    Friend WithEvents Button9 As System.Windows.Forms.Button
    Friend WithEvents Button10 As System.Windows.Forms.Button
    Friend WithEvents BoxBox As System.Windows.Forms.ListBox
    Friend WithEvents Label3 As System.Windows.Forms.Label

End Class
