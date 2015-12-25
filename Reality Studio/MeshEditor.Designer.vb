<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MeshEditor
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MeshEditor))
        Me.Button1 = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Button3 = New System.Windows.Forms.Button
        Me.Button4 = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Button5 = New System.Windows.Forms.Button
        Me.TxtBox = New System.Windows.Forms.ListBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Button6 = New System.Windows.Forms.Button
        Me.Button7 = New System.Windows.Forms.Button
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.NumberBox5 = New Reality_Studio.NumberBox(Me.components)
        Me.NumberBox2 = New Reality_Studio.NumberBox(Me.components)
        Me.NumberBox3 = New Reality_Studio.NumberBox(Me.components)
        Me.NumberBox4 = New Reality_Studio.NumberBox(Me.components)
        Me.TextBox4 = New Reality_Studio.NumberBox(Me.components)
        Me.TextBox3 = New Reality_Studio.NumberBox(Me.components)
        Me.TextBox2 = New Reality_Studio.NumberBox(Me.components)
        Me.CheckBox1 = New System.Windows.Forms.CheckBox
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(77, 285)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(134, 33)
        Me.Button1.TabIndex = 8
        Me.Button1.Text = "Change Mesh"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(309, 277)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(128, 36)
        Me.Button2.TabIndex = 9
        Me.Button2.Text = "View Mesh"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.Color.White
        Me.TextBox1.Location = New System.Drawing.Point(79, 17)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(194, 20)
        Me.TextBox1.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(67, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Mesh Name:"
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(9, 239)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(75, 39)
        Me.Button3.TabIndex = 5
        Me.Button3.Text = "OK"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button4.Location = New System.Drawing.Point(197, 239)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(76, 39)
        Me.Button4.TabIndex = 7
        Me.Button4.Text = "CANCEL"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(3, 45)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(67, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Rot Origin X:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(3, 71)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(67, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Rot Origin Y:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(3, 97)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(67, 13)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Rot Origin Z:"
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(90, 239)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(101, 39)
        Me.Button5.TabIndex = 6
        Me.Button5.Text = "APPLY"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'TxtBox
        '
        Me.TxtBox.BackColor = System.Drawing.Color.White
        Me.TxtBox.FormattingEnabled = True
        Me.TxtBox.Location = New System.Drawing.Point(293, 35)
        Me.TxtBox.Name = "TxtBox"
        Me.TxtBox.Size = New System.Drawing.Size(157, 199)
        Me.TxtBox.TabIndex = 10
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(292, 17)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(95, 13)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "Included Textures:"
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(293, 246)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(71, 25)
        Me.Button6.TabIndex = 11
        Me.Button6.Text = "ADD"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'Button7
        '
        Me.Button7.Location = New System.Drawing.Point(371, 246)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(79, 25)
        Me.Button7.TabIndex = 12
        Me.Button7.Text = "REMOVE"
        Me.Button7.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(2, 181)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(67, 13)
        Me.Label5.TabIndex = 21
        Me.Label5.Text = "Def. AngleZ:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(2, 155)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(67, 13)
        Me.Label7.TabIndex = 20
        Me.Label7.Text = "Def. AngleY:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(2, 129)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(67, 13)
        Me.Label8.TabIndex = 19
        Me.Label8.Text = "Def. AngleX:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(3, 216)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(60, 13)
        Me.Label9.TabIndex = 23
        Me.Label9.Text = "Def. Scale:"
        '
        'NumberBox5
        '
        Me.NumberBox5.BackColor = System.Drawing.Color.White
        Me.NumberBox5.ForeColor = System.Drawing.Color.Black
        Me.NumberBox5.Location = New System.Drawing.Point(79, 213)
        Me.NumberBox5.Name = "NumberBox5"
        Me.NumberBox5.Size = New System.Drawing.Size(194, 20)
        Me.NumberBox5.TabIndex = 22
        Me.NumberBox5.Text = "0"
        '
        'NumberBox2
        '
        Me.NumberBox2.BackColor = System.Drawing.Color.White
        Me.NumberBox2.ForeColor = System.Drawing.Color.Black
        Me.NumberBox2.Location = New System.Drawing.Point(78, 178)
        Me.NumberBox2.Name = "NumberBox2"
        Me.NumberBox2.Size = New System.Drawing.Size(194, 20)
        Me.NumberBox2.TabIndex = 18
        Me.NumberBox2.Text = "0"
        '
        'NumberBox3
        '
        Me.NumberBox3.BackColor = System.Drawing.Color.White
        Me.NumberBox3.ForeColor = System.Drawing.Color.Black
        Me.NumberBox3.Location = New System.Drawing.Point(78, 152)
        Me.NumberBox3.Name = "NumberBox3"
        Me.NumberBox3.Size = New System.Drawing.Size(193, 20)
        Me.NumberBox3.TabIndex = 17
        Me.NumberBox3.Text = "0"
        '
        'NumberBox4
        '
        Me.NumberBox4.BackColor = System.Drawing.Color.White
        Me.NumberBox4.ForeColor = System.Drawing.Color.Black
        Me.NumberBox4.Location = New System.Drawing.Point(78, 126)
        Me.NumberBox4.Name = "NumberBox4"
        Me.NumberBox4.Size = New System.Drawing.Size(194, 20)
        Me.NumberBox4.TabIndex = 16
        Me.NumberBox4.Text = "0"
        '
        'TextBox4
        '
        Me.TextBox4.BackColor = System.Drawing.Color.White
        Me.TextBox4.ForeColor = System.Drawing.Color.Black
        Me.TextBox4.Location = New System.Drawing.Point(79, 94)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(194, 20)
        Me.TextBox4.TabIndex = 4
        Me.TextBox4.Text = "0"
        '
        'TextBox3
        '
        Me.TextBox3.BackColor = System.Drawing.Color.White
        Me.TextBox3.ForeColor = System.Drawing.Color.Black
        Me.TextBox3.Location = New System.Drawing.Point(79, 68)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(193, 20)
        Me.TextBox3.TabIndex = 3
        Me.TextBox3.Text = "0"
        '
        'TextBox2
        '
        Me.TextBox2.BackColor = System.Drawing.Color.White
        Me.TextBox2.ForeColor = System.Drawing.Color.Black
        Me.TextBox2.Location = New System.Drawing.Point(79, 42)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(194, 20)
        Me.TextBox2.TabIndex = 2
        Me.TextBox2.Text = "0"
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(9, 301)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(70, 17)
        Me.CheckBox1.TabIndex = 24
        Me.CheckBox1.Text = "Animated"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'MeshEditor
        '
        Me.AcceptButton = Me.Button3
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.Button4
        Me.ClientSize = New System.Drawing.Size(462, 324)
        Me.Controls.Add(Me.NumberBox5)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.NumberBox2)
        Me.Controls.Add(Me.NumberBox3)
        Me.Controls.Add(Me.NumberBox4)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.TextBox4)
        Me.Controls.Add(Me.TextBox3)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.Button7)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TxtBox)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.CheckBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "MeshEditor"
        Me.ShowInTaskbar = False
        Me.Text = "Mesh Editor"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents TxtBox As System.Windows.Forms.ListBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents TextBox2 As Reality_Studio.NumberBox
    Friend WithEvents TextBox3 As Reality_Studio.NumberBox
    Friend WithEvents TextBox4 As Reality_Studio.NumberBox
    Friend WithEvents NumberBox2 As Reality_Studio.NumberBox
    Friend WithEvents NumberBox3 As Reality_Studio.NumberBox
    Friend WithEvents NumberBox4 As Reality_Studio.NumberBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents NumberBox5 As Reality_Studio.NumberBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
End Class
