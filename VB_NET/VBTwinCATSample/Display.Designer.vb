<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Display
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        indicator_in_0 = New Label()
        indicator_in_1 = New Label()
        Label0 = New Label()
        Label1 = New Label()
        Timer1 = New Timer(components)
        Label4 = New Label()
        indicator_in_n1 = New Label()
        Label5 = New Label()
        indicator_in_string = New Label()
        TextBox1 = New TextBox()
        Button1 = New Button()
        Button2 = New Button()
        SuspendLayout()
        ' 
        ' indicator_in_0
        ' 
        indicator_in_0.AutoSize = True
        indicator_in_0.Location = New Point(140, 33)
        indicator_in_0.Name = "indicator_in_0"
        indicator_in_0.Size = New Size(28, 15)
        indicator_in_0.TabIndex = 0
        indicator_in_0.Text = "OFF"
        ' 
        ' indicator_in_1
        ' 
        indicator_in_1.AutoSize = True
        indicator_in_1.Location = New Point(140, 66)
        indicator_in_1.Name = "indicator_in_1"
        indicator_in_1.Size = New Size(28, 15)
        indicator_in_1.TabIndex = 2
        indicator_in_1.Text = "OFF"
        ' 
        ' Label0
        ' 
        Label0.AutoSize = True
        Label0.Location = New Point(55, 32)
        Label0.Name = "Label0"
        Label0.Size = New Size(44, 15)
        Label0.TabIndex = 1
        Label0.Text = "Input 0"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(55, 65)
        Label1.Name = "Label1"
        Label1.Size = New Size(44, 15)
        Label1.TabIndex = 3
        Label1.Text = "Input 1"
        ' 
        ' Timer1
        ' 
        Timer1.Enabled = True
        Timer1.Interval = 1000
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(55, 113)
        Label4.Name = "Label4"
        Label4.Size = New Size(50, 15)
        Label4.TabIndex = 9
        Label4.Text = "Number"
        ' 
        ' indicator_in_n1
        ' 
        indicator_in_n1.AutoSize = True
        indicator_in_n1.Location = New Point(155, 113)
        indicator_in_n1.Name = "indicator_in_n1"
        indicator_in_n1.Size = New Size(13, 15)
        indicator_in_n1.TabIndex = 8
        indicator_in_n1.Text = "0"
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(23, 185)
        Label5.Name = "Label5"
        Label5.Size = New Size(53, 15)
        Label5.TabIndex = 10
        Label5.Text = "Message"
        ' 
        ' indicator_in_string
        ' 
        indicator_in_string.AutoSize = True
        indicator_in_string.Location = New Point(23, 210)
        indicator_in_string.Name = "indicator_in_string"
        indicator_in_string.Size = New Size(0, 15)
        indicator_in_string.TabIndex = 11
        indicator_in_string.TextAlign = ContentAlignment.TopRight
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(23, 150)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(343, 23)
        TextBox1.TabIndex = 12
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(244, 29)
        Button1.Name = "Button1"
        Button1.Size = New Size(75, 23)
        Button1.TabIndex = 13
        Button1.Text = "Button1"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(244, 61)
        Button2.Name = "Button2"
        Button2.Size = New Size(75, 23)
        Button2.TabIndex = 14
        Button2.Text = "Button2"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Display
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(378, 244)
        Controls.Add(Button2)
        Controls.Add(Button1)
        Controls.Add(TextBox1)
        Controls.Add(indicator_in_string)
        Controls.Add(Label5)
        Controls.Add(Label4)
        Controls.Add(indicator_in_n1)
        Controls.Add(Label1)
        Controls.Add(indicator_in_1)
        Controls.Add(Label0)
        Controls.Add(indicator_in_0)
        Name = "Display"
        Text = "TwinCAT Input Display"
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents indicator_in_0 As Label
    Friend WithEvents Label0 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents indicator_in_1 As Label
    Friend WithEvents Timer1 As Timer
    Friend WithEvents Label4 As Label
    Friend WithEvents indicator_in_n1 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents indicator_in_string As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button

End Class
