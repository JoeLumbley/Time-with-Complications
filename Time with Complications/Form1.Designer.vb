<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
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
        Timer1 = New Timer(components)
        MenuStrip1 = New MenuStrip()
        MainToolStripMenuItem = New ToolStripMenuItem()
        TwelveHourMenuItem = New ToolStripMenuItem()
        TwentyFourHourMenuItem = New ToolStripMenuItem()
        TopToolStripMenuItem = New ToolStripMenuItem()
        TopDayMenuItem = New ToolStripMenuItem()
        TopShortDayMenuItem = New ToolStripMenuItem()
        TopLongDateMenuItem = New ToolStripMenuItem()
        TopMedDateMenuItem = New ToolStripMenuItem()
        TopShortDateMenuItem = New ToolStripMenuItem()
        TopTimeZoneMenuItem = New ToolStripMenuItem()
        TopTimeZoneCityMenuItem = New ToolStripMenuItem()
        TopLocalTimeMenuItem = New ToolStripMenuItem()
        BottomToolStripMenuItem = New ToolStripMenuItem()
        BottomDayMenuItem = New ToolStripMenuItem()
        BottomShortDayMenuItem = New ToolStripMenuItem()
        BottomLongDateMenuItem = New ToolStripMenuItem()
        BottomMediumDateMenuItem = New ToolStripMenuItem()
        MenuStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' Timer1
        ' 
        ' 
        ' MenuStrip1
        ' 
        MenuStrip1.ImageScalingSize = New Size(24, 24)
        MenuStrip1.Items.AddRange(New ToolStripItem() {MainToolStripMenuItem, TopToolStripMenuItem, BottomToolStripMenuItem})
        MenuStrip1.Location = New Point(0, 0)
        MenuStrip1.Name = "MenuStrip1"
        MenuStrip1.Size = New Size(800, 33)
        MenuStrip1.TabIndex = 0
        MenuStrip1.Text = "MenuStrip1"
        ' 
        ' MainToolStripMenuItem
        ' 
        MainToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {TwelveHourMenuItem, TwentyFourHourMenuItem})
        MainToolStripMenuItem.Name = "MainToolStripMenuItem"
        MainToolStripMenuItem.Size = New Size(67, 29)
        MainToolStripMenuItem.Text = "Main"
        ' 
        ' TwelveHourMenuItem
        ' 
        TwelveHourMenuItem.Checked = True
        TwelveHourMenuItem.CheckState = CheckState.Checked
        TwelveHourMenuItem.Name = "TwelveHourMenuItem"
        TwelveHourMenuItem.Size = New Size(179, 34)
        TwelveHourMenuItem.Text = "12 Hour"
        ' 
        ' TwentyFourHourMenuItem
        ' 
        TwentyFourHourMenuItem.Name = "TwentyFourHourMenuItem"
        TwentyFourHourMenuItem.Size = New Size(179, 34)
        TwentyFourHourMenuItem.Text = "24 Hour"
        ' 
        ' TopToolStripMenuItem
        ' 
        TopToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {TopDayMenuItem, TopShortDayMenuItem, TopLongDateMenuItem, TopMedDateMenuItem, TopShortDateMenuItem, TopTimeZoneMenuItem, TopTimeZoneCityMenuItem, TopLocalTimeMenuItem})
        TopToolStripMenuItem.Name = "TopToolStripMenuItem"
        TopToolStripMenuItem.Size = New Size(57, 29)
        TopToolStripMenuItem.Text = "Top"
        ' 
        ' TopDayMenuItem
        ' 
        TopDayMenuItem.Checked = True
        TopDayMenuItem.CheckState = CheckState.Checked
        TopDayMenuItem.Name = "TopDayMenuItem"
        TopDayMenuItem.Size = New Size(270, 34)
        TopDayMenuItem.Text = "Day"
        ' 
        ' TopShortDayMenuItem
        ' 
        TopShortDayMenuItem.Name = "TopShortDayMenuItem"
        TopShortDayMenuItem.Size = New Size(270, 34)
        TopShortDayMenuItem.Text = "Short Day"
        ' 
        ' TopLongDateMenuItem
        ' 
        TopLongDateMenuItem.Name = "TopLongDateMenuItem"
        TopLongDateMenuItem.Size = New Size(270, 34)
        TopLongDateMenuItem.Text = "Long Date"
        ' 
        ' TopMedDateMenuItem
        ' 
        TopMedDateMenuItem.Name = "TopMedDateMenuItem"
        TopMedDateMenuItem.Size = New Size(270, 34)
        TopMedDateMenuItem.Text = "Medium Date"
        ' 
        ' TopShortDateMenuItem
        ' 
        TopShortDateMenuItem.Name = "TopShortDateMenuItem"
        TopShortDateMenuItem.Size = New Size(270, 34)
        TopShortDateMenuItem.Text = "Short Date"
        ' 
        ' TopTimeZoneMenuItem
        ' 
        TopTimeZoneMenuItem.Name = "TopTimeZoneMenuItem"
        TopTimeZoneMenuItem.Size = New Size(270, 34)
        TopTimeZoneMenuItem.Text = "Time Zone"
        ' 
        ' TopTimeZoneCityMenuItem
        ' 
        TopTimeZoneCityMenuItem.Name = "TopTimeZoneCityMenuItem"
        TopTimeZoneCityMenuItem.Size = New Size(270, 34)
        TopTimeZoneCityMenuItem.Text = "Time Zone City"
        ' 
        ' TopLocalTimeMenuItem
        ' 
        TopLocalTimeMenuItem.Name = "TopLocalTimeMenuItem"
        TopLocalTimeMenuItem.Size = New Size(270, 34)
        TopLocalTimeMenuItem.Text = "Local Time"
        ' 
        ' BottomToolStripMenuItem
        ' 
        BottomToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {BottomDayMenuItem, BottomShortDayMenuItem, BottomLongDateMenuItem, BottomMediumDateMenuItem})
        BottomToolStripMenuItem.Name = "BottomToolStripMenuItem"
        BottomToolStripMenuItem.Size = New Size(88, 29)
        BottomToolStripMenuItem.Text = "Bottom"
        ' 
        ' BottomDayMenuItem
        ' 
        BottomDayMenuItem.Name = "BottomDayMenuItem"
        BottomDayMenuItem.Size = New Size(270, 34)
        BottomDayMenuItem.Text = "Day"
        ' 
        ' BottomShortDayMenuItem
        ' 
        BottomShortDayMenuItem.Name = "BottomShortDayMenuItem"
        BottomShortDayMenuItem.Size = New Size(270, 34)
        BottomShortDayMenuItem.Text = "Short Day"
        ' 
        ' BottomLongDateMenuItem
        ' 
        BottomLongDateMenuItem.Name = "BottomLongDateMenuItem"
        BottomLongDateMenuItem.Size = New Size(270, 34)
        BottomLongDateMenuItem.Text = "Long Date"
        ' 
        ' BottomMediumDateMenuItem
        ' 
        BottomMediumDateMenuItem.Name = "BottomMediumDateMenuItem"
        BottomMediumDateMenuItem.Size = New Size(270, 34)
        BottomMediumDateMenuItem.Text = "Medium Date"
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(MenuStrip1)
        MainMenuStrip = MenuStrip1
        Name = "Form1"
        Text = "Form1"
        MenuStrip1.ResumeLayout(False)
        MenuStrip1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Timer1 As Timer
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents MainToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TwelveHourMenuItem As ToolStripMenuItem
    Friend WithEvents TwentyFourHourMenuItem As ToolStripMenuItem
    Friend WithEvents TopToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TopDayMenuItem As ToolStripMenuItem
    Friend WithEvents TopShortDayMenuItem As ToolStripMenuItem
    Friend WithEvents TopLongDateMenuItem As ToolStripMenuItem
    Friend WithEvents TopMedDateMenuItem As ToolStripMenuItem
    Friend WithEvents TopShortDateMenuItem As ToolStripMenuItem
    Friend WithEvents TopTimeZoneMenuItem As ToolStripMenuItem
    Friend WithEvents TopTimeZoneCityMenuItem As ToolStripMenuItem
    Friend WithEvents TopLocalTimeMenuItem As ToolStripMenuItem
    Friend WithEvents BottomToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BottomDayMenuItem As ToolStripMenuItem
    Friend WithEvents BottomShortDayMenuItem As ToolStripMenuItem
    Friend WithEvents BottomLongDateMenuItem As ToolStripMenuItem
    Friend WithEvents BottomMediumDateMenuItem As ToolStripMenuItem

End Class
