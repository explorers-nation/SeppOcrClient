<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SeppOcrClient
    Inherits Global.System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <Global.System.Diagnostics.DebuggerNonUserCode()>
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
    Private components As Global.System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <Global.System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SeppOcrClient))
        Me.JourneyDir = New System.DirectoryServices.DirectoryEntry()
        Me.FolderBrowser = New System.Windows.Forms.FolderBrowserDialog()
        Me.CommsTimer = New System.Windows.Forms.Timer(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.BrowserForDir = New System.Windows.Forms.Button()
        Me.JournalFolder = New System.Windows.Forms.TextBox()
        Me.CaptureEDScreen = New System.Windows.Forms.Button()
        Me.UpdateBgsData = New System.Windows.Forms.Button()
        Me.SelectedSystem = New System.Windows.Forms.ComboBox()
        Me.InfTotal = New System.Windows.Forms.Label()
        Me.InfTotalVal = New System.Windows.Forms.Label()
        Me.ViewWebTracker = New System.Windows.Forms.Button()
        Me.LogOcrCheckbox = New System.Windows.Forms.CheckBox()
        Me.ScanMarginLeft = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.EDCapture = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.RemoveButton = New System.Windows.Forms.Button()
        Me.AddButton = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.EnteredByLabel = New System.Windows.Forms.Label()
        Me.PrevEnteredByLabel = New System.Windows.Forms.Label()
        Me.LogTab = New System.Windows.Forms.TabPage()
        Me.LogTextBox = New System.Windows.Forms.RichTextBox()
        Me.SettingsTab = New System.Windows.Forms.TabPage()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.SystemNameBox = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.TickTimePicker = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CommanderName = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.SoftDataTab = New System.Windows.Forms.TabPage()
        Me.ScanningPanel = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.IgtLabel = New System.Windows.Forms.Label()
        Me.TickLabel = New System.Windows.Forms.Label()
        Me.Clock = New System.Windows.Forms.TextBox()
        Me.CollectionDateLabel = New System.Windows.Forms.Label()
        Me.EntryDate = New System.Windows.Forms.TextBox()
        Me.CollectionTimingLabel = New System.Windows.Forms.Label()
        Me.PreOrPostTick = New System.Windows.Forms.ComboBox()
        Me.StatusBox = New System.Windows.Forms.TextBox()
        Me.SystemLabel = New System.Windows.Forms.Label()
        Me.SoftDataGrid = New System.Windows.Forms.DataGridView()
        Me.Found = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Faction = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PrevInfluence = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Influence = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.InfluenceDiff = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PrevState = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.State = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LoadingLabel = New System.Windows.Forms.Label()
        Me.AlwaysOnTopCheckbox = New System.Windows.Forms.CheckBox()
        Me.Tabs = New System.Windows.Forms.TabControl()
        Me.ClockTimer = New System.Windows.Forms.Timer(Me.components)
        Me.GameScreenWidth = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.GameScreenHeight = New System.Windows.Forms.TextBox()
        Me.RecalcScreenResButton = New System.Windows.Forms.Button()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.TestOcrButton = New System.Windows.Forms.Button()
        CType(Me.EDCapture, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LogTab.SuspendLayout()
        Me.SettingsTab.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SoftDataTab.SuspendLayout()
        Me.ScanningPanel.SuspendLayout()
        CType(Me.SoftDataGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Tabs.SuspendLayout()
        Me.SuspendLayout()
        '
        'CommsTimer
        '
        Me.CommsTimer.Interval = 3000
        '
        'ToolTip1
        '
        Me.ToolTip1.AutoPopDelay = 200
        Me.ToolTip1.InitialDelay = 400
        Me.ToolTip1.ReshowDelay = 100
        '
        'BrowserForDir
        '
        Me.BrowserForDir.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BrowserForDir.Location = New System.Drawing.Point(402, 18)
        Me.BrowserForDir.Name = "BrowserForDir"
        Me.BrowserForDir.Size = New System.Drawing.Size(36, 24)
        Me.BrowserForDir.TabIndex = 2
        Me.BrowserForDir.Text = "..."
        Me.ToolTip1.SetToolTip(Me.BrowserForDir, "Select the location of your ED Journal")
        Me.BrowserForDir.UseVisualStyleBackColor = True
        '
        'JournalFolder
        '
        Me.JournalFolder.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.JournalFolder.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.JournalFolder.Location = New System.Drawing.Point(78, 19)
        Me.JournalFolder.Multiline = True
        Me.JournalFolder.Name = "JournalFolder"
        Me.JournalFolder.Size = New System.Drawing.Size(321, 21)
        Me.JournalFolder.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.JournalFolder, "Set the location of your ED Journal")
        Me.JournalFolder.WordWrap = False
        '
        'CaptureEDScreen
        '
        Me.CaptureEDScreen.Location = New System.Drawing.Point(10, 255)
        Me.CaptureEDScreen.Name = "CaptureEDScreen"
        Me.CaptureEDScreen.Size = New System.Drawing.Size(77, 46)
        Me.CaptureEDScreen.TabIndex = 23
        Me.CaptureEDScreen.Text = "OCR Scan System Map"
        Me.ToolTip1.SetToolTip(Me.CaptureEDScreen, "Capture the Image from your Main Screen")
        Me.CaptureEDScreen.UseVisualStyleBackColor = True
        '
        'UpdateBgsData
        '
        Me.UpdateBgsData.Location = New System.Drawing.Point(515, 255)
        Me.UpdateBgsData.Name = "UpdateBgsData"
        Me.UpdateBgsData.Size = New System.Drawing.Size(61, 46)
        Me.UpdateBgsData.TabIndex = 25
        Me.UpdateBgsData.Text = "Send Data"
        Me.ToolTip1.SetToolTip(Me.UpdateBgsData, "Update Sepps Soft Data" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Only available when Influence Total is 100%")
        Me.UpdateBgsData.UseVisualStyleBackColor = True
        '
        'SelectedSystem
        '
        Me.SelectedSystem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.SelectedSystem.FormattingEnabled = True
        Me.SelectedSystem.Items.AddRange(New Object() {"Loading..."})
        Me.SelectedSystem.Location = New System.Drawing.Point(54, 32)
        Me.SelectedSystem.Name = "SelectedSystem"
        Me.SelectedSystem.Size = New System.Drawing.Size(163, 21)
        Me.SelectedSystem.TabIndex = 28
        Me.SelectedSystem.TabStop = False
        Me.ToolTip1.SetToolTip(Me.SelectedSystem, "Select the system for Soft Data update")
        Me.SelectedSystem.Visible = False
        '
        'InfTotal
        '
        Me.InfTotal.AutoSize = True
        Me.InfTotal.ForeColor = System.Drawing.Color.DarkRed
        Me.InfTotal.Location = New System.Drawing.Point(414, 269)
        Me.InfTotal.Name = "InfTotal"
        Me.InfTotal.Size = New System.Drawing.Size(51, 26)
        Me.InfTotal.TabIndex = 32
        Me.InfTotal.Text = "Influence" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Total:"
        Me.InfTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ToolTip1.SetToolTip(Me.InfTotal, "Influence Total has to be 100% before update is enabled")
        '
        'InfTotalVal
        '
        Me.InfTotalVal.AutoSize = True
        Me.InfTotalVal.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InfTotalVal.ForeColor = System.Drawing.Color.DarkRed
        Me.InfTotalVal.Location = New System.Drawing.Point(465, 274)
        Me.InfTotalVal.Name = "InfTotalVal"
        Me.InfTotalVal.Size = New System.Drawing.Size(16, 18)
        Me.InfTotalVal.TabIndex = 36
        Me.InfTotalVal.Text = "0"
        Me.ToolTip1.SetToolTip(Me.InfTotalVal, "Influence Total has to be 100% before update is enabled")
        '
        'ViewWebTracker
        '
        Me.ViewWebTracker.Location = New System.Drawing.Point(482, 22)
        Me.ViewWebTracker.Name = "ViewWebTracker"
        Me.ViewWebTracker.Size = New System.Drawing.Size(94, 31)
        Me.ViewWebTracker.TabIndex = 45
        Me.ViewWebTracker.Text = "Web Tracker"
        Me.ToolTip1.SetToolTip(Me.ViewWebTracker, "Update Sepps Soft Data" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Only available when Influence Total is 100%")
        Me.ViewWebTracker.UseVisualStyleBackColor = True
        '
        'LogOcrCheckbox
        '
        Me.LogOcrCheckbox.AutoSize = True
        Me.LogOcrCheckbox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LogOcrCheckbox.Location = New System.Drawing.Point(239, 39)
        Me.LogOcrCheckbox.Name = "LogOcrCheckbox"
        Me.LogOcrCheckbox.Size = New System.Drawing.Size(146, 17)
        Me.LogOcrCheckbox.TabIndex = 2
        Me.LogOcrCheckbox.Text = "Log OCR text translations"
        Me.ToolTip1.SetToolTip(Me.LogOcrCheckbox, "Covert the Image to Grey Scale prior to OCR processing" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "This can improve OCR accu" &
        "racy but at a minor delay")
        Me.LogOcrCheckbox.UseVisualStyleBackColor = True
        '
        'ScanMarginLeft
        '
        Me.ScanMarginLeft.Location = New System.Drawing.Point(344, 111)
        Me.ScanMarginLeft.Name = "ScanMarginLeft"
        Me.ScanMarginLeft.Size = New System.Drawing.Size(41, 20)
        Me.ScanMarginLeft.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.ScanMarginLeft, "Set how much of the screen to look at when capturing text. This will stop other t" &
        "ext like this window contaminating the results")
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(210, 114)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(131, 13)
        Me.Label14.TabIndex = 9
        Me.Label14.Text = "Area of left screen to scan"
        Me.ToolTip1.SetToolTip(Me.Label14, "Set how much of the screen to look at when capturing text. This will stop other t" &
        "ext like this window contaminating the results")
        '
        'EDCapture
        '
        Me.EDCapture.BackColor = System.Drawing.Color.DarkGray
        Me.EDCapture.Location = New System.Drawing.Point(8, 33)
        Me.EDCapture.Name = "EDCapture"
        Me.EDCapture.Size = New System.Drawing.Size(176, 242)
        Me.EDCapture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.EDCapture.TabIndex = 54
        Me.EDCapture.TabStop = False
        Me.ToolTip1.SetToolTip(Me.EDCapture, "Click on the Image to see full view")
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(5, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(85, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Last OCR Image" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.ToolTip1.SetToolTip(Me.Label1, "Set how much of the screen to look at when capturing text. This will stop other t" &
        "ext like this window contaminating the results")
        '
        'RemoveButton
        '
        Me.RemoveButton.Enabled = False
        Me.RemoveButton.Location = New System.Drawing.Point(349, 18)
        Me.RemoveButton.Name = "RemoveButton"
        Me.RemoveButton.Size = New System.Drawing.Size(88, 54)
        Me.RemoveButton.TabIndex = 0
        Me.RemoveButton.Text = "Remove from Collection List"
        Me.ToolTip1.SetToolTip(Me.RemoveButton, "Add the entered system name to the list of official SEPP Systems")
        Me.RemoveButton.UseVisualStyleBackColor = True
        '
        'AddButton
        '
        Me.AddButton.Location = New System.Drawing.Point(236, 19)
        Me.AddButton.Name = "AddButton"
        Me.AddButton.Size = New System.Drawing.Size(107, 53)
        Me.AddButton.TabIndex = 3
        Me.AddButton.Text = "Add To Collection List"
        Me.ToolTip1.SetToolTip(Me.AddButton, "Add the entered system name to the list of official SEPP Systems")
        Me.AddButton.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(11, 21)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(75, 13)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "System Name:"
        Me.ToolTip1.SetToolTip(Me.Label7, "Set how much of the screen to look at when capturing text. This will stop other t" &
        "ext like this window contaminating the results")
        '
        'EnteredByLabel
        '
        Me.EnteredByLabel.AutoSize = True
        Me.EnteredByLabel.ForeColor = System.Drawing.Color.MediumBlue
        Me.EnteredByLabel.Location = New System.Drawing.Point(93, 261)
        Me.EnteredByLabel.Name = "EnteredByLabel"
        Me.EnteredByLabel.Size = New System.Drawing.Size(181, 13)
        Me.EnteredByLabel.TabIndex = 59
        Me.EnteredByLabel.Text = "Entered by Jameson on 20XX-XX-XX"
        Me.ToolTip1.SetToolTip(Me.EnteredByLabel, "Influence Total has to be 100% before update is enabled")
        Me.EnteredByLabel.Visible = False
        '
        'PrevEnteredByLabel
        '
        Me.PrevEnteredByLabel.AutoSize = True
        Me.PrevEnteredByLabel.ForeColor = System.Drawing.SystemColors.WindowText
        Me.PrevEnteredByLabel.Location = New System.Drawing.Point(93, 282)
        Me.PrevEnteredByLabel.Name = "PrevEnteredByLabel"
        Me.PrevEnteredByLabel.Size = New System.Drawing.Size(185, 13)
        Me.PrevEnteredByLabel.TabIndex = 60
        Me.PrevEnteredByLabel.Text = "Previous Data: Jameson 20XX-XX-XX"
        Me.ToolTip1.SetToolTip(Me.PrevEnteredByLabel, "Influence Total has to be 100% before update is enabled")
        Me.PrevEnteredByLabel.Visible = False
        '
        'LogTab
        '
        Me.LogTab.Controls.Add(Me.LogTextBox)
        Me.LogTab.Location = New System.Drawing.Point(4, 22)
        Me.LogTab.Name = "LogTab"
        Me.LogTab.Padding = New System.Windows.Forms.Padding(3)
        Me.LogTab.Size = New System.Drawing.Size(866, 304)
        Me.LogTab.TabIndex = 1
        Me.LogTab.Text = "Log"
        Me.LogTab.UseVisualStyleBackColor = True
        '
        'LogTextBox
        '
        Me.LogTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LogTextBox.Location = New System.Drawing.Point(3, 3)
        Me.LogTextBox.Name = "LogTextBox"
        Me.LogTextBox.ReadOnly = True
        Me.LogTextBox.Size = New System.Drawing.Size(860, 298)
        Me.LogTextBox.TabIndex = 0
        Me.LogTextBox.Text = ""
        '
        'SettingsTab
        '
        Me.SettingsTab.Controls.Add(Me.GroupBox3)
        Me.SettingsTab.Controls.Add(Me.GroupBox2)
        Me.SettingsTab.Controls.Add(Me.GroupBox1)
        Me.SettingsTab.Location = New System.Drawing.Point(4, 22)
        Me.SettingsTab.Name = "SettingsTab"
        Me.SettingsTab.Size = New System.Drawing.Size(866, 304)
        Me.SettingsTab.TabIndex = 2
        Me.SettingsTab.Text = "Settings"
        Me.SettingsTab.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Controls.Add(Me.SystemNameBox)
        Me.GroupBox3.Controls.Add(Me.RemoveButton)
        Me.GroupBox3.Controls.Add(Me.AddButton)
        Me.GroupBox3.Location = New System.Drawing.Point(418, 140)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(445, 164)
        Me.GroupBox3.TabIndex = 0
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "System Rota Management"
        '
        'SystemNameBox
        '
        Me.SystemNameBox.Location = New System.Drawing.Point(92, 18)
        Me.SystemNameBox.Name = "SystemNameBox"
        Me.SystemNameBox.Size = New System.Drawing.Size(138, 20)
        Me.SystemNameBox.TabIndex = 2
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.TickTimePicker)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.JournalFolder)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.CommanderName)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.BrowserForDir)
        Me.GroupBox2.Location = New System.Drawing.Point(417, 3)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(444, 130)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Journal Scraping"
        '
        'TickTimePicker
        '
        Me.TickTimePicker.CustomFormat = "HH:mm"
        Me.TickTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.TickTimePicker.Location = New System.Drawing.Point(149, 96)
        Me.TickTimePicker.Name = "TickTimePicker"
        Me.TickTimePicker.ShowUpDown = True
        Me.TickTimePicker.Size = New System.Drawing.Size(68, 20)
        Me.TickTimePicker.TabIndex = 0
        Me.TickTimePicker.Value = New Date(2000, 1, 1, 1, 23, 0, 0)
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label4.Location = New System.Drawing.Point(10, 98)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(130, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Estimated Tick Time (IGT)"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label2.Location = New System.Drawing.Point(49, 62)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(94, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Commander Name"
        '
        'CommanderName
        '
        Me.CommanderName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CommanderName.Location = New System.Drawing.Point(149, 57)
        Me.CommanderName.Name = "CommanderName"
        Me.CommanderName.Size = New System.Drawing.Size(250, 21)
        Me.CommanderName.TabIndex = 5
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label9.Location = New System.Drawing.Point(10, 24)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(62, 13)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "Logs Folder"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TestOcrButton)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.RecalcScreenResButton)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.GameScreenHeight)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.GameScreenWidth)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label17)
        Me.GroupBox1.Controls.Add(Me.EDCapture)
        Me.GroupBox1.Controls.Add(Me.ScanMarginLeft)
        Me.GroupBox1.Controls.Add(Me.LogOcrCheckbox)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(408, 296)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "OCR Calibration"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 278)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(336, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Use it to check the Factions text is the only part getting OCR scanned" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(385, 114)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(15, 13)
        Me.Label17.TabIndex = 59
        Me.Label17.Text = "%"
        '
        'SoftDataTab
        '
        Me.SoftDataTab.Controls.Add(Me.ScanningPanel)
        Me.SoftDataTab.Controls.Add(Me.PrevEnteredByLabel)
        Me.SoftDataTab.Controls.Add(Me.EnteredByLabel)
        Me.SoftDataTab.Controls.Add(Me.IgtLabel)
        Me.SoftDataTab.Controls.Add(Me.TickLabel)
        Me.SoftDataTab.Controls.Add(Me.Clock)
        Me.SoftDataTab.Controls.Add(Me.CollectionDateLabel)
        Me.SoftDataTab.Controls.Add(Me.EntryDate)
        Me.SoftDataTab.Controls.Add(Me.CollectionTimingLabel)
        Me.SoftDataTab.Controls.Add(Me.PreOrPostTick)
        Me.SoftDataTab.Controls.Add(Me.ViewWebTracker)
        Me.SoftDataTab.Controls.Add(Me.StatusBox)
        Me.SoftDataTab.Controls.Add(Me.SystemLabel)
        Me.SoftDataTab.Controls.Add(Me.SoftDataGrid)
        Me.SoftDataTab.Controls.Add(Me.InfTotalVal)
        Me.SoftDataTab.Controls.Add(Me.InfTotal)
        Me.SoftDataTab.Controls.Add(Me.SelectedSystem)
        Me.SoftDataTab.Controls.Add(Me.UpdateBgsData)
        Me.SoftDataTab.Controls.Add(Me.CaptureEDScreen)
        Me.SoftDataTab.Controls.Add(Me.LoadingLabel)
        Me.SoftDataTab.Controls.Add(Me.AlwaysOnTopCheckbox)
        Me.SoftDataTab.Location = New System.Drawing.Point(4, 22)
        Me.SoftDataTab.Name = "SoftDataTab"
        Me.SoftDataTab.Size = New System.Drawing.Size(866, 304)
        Me.SoftDataTab.TabIndex = 3
        Me.SoftDataTab.Text = "OCR"
        Me.SoftDataTab.UseVisualStyleBackColor = True
        '
        'ScanningPanel
        '
        Me.ScanningPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ScanningPanel.Controls.Add(Me.Label5)
        Me.ScanningPanel.Location = New System.Drawing.Point(190, 104)
        Me.ScanningPanel.Name = "ScanningPanel"
        Me.ScanningPanel.Size = New System.Drawing.Size(200, 100)
        Me.ScanningPanel.TabIndex = 61
        Me.ScanningPanel.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(59, 41)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(84, 16)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "Scanning..."
        '
        'IgtLabel
        '
        Me.IgtLabel.AutoSize = True
        Me.IgtLabel.Location = New System.Drawing.Point(414, 36)
        Me.IgtLabel.Name = "IgtLabel"
        Me.IgtLabel.Size = New System.Drawing.Size(25, 13)
        Me.IgtLabel.TabIndex = 58
        Me.IgtLabel.Text = "IGT"
        Me.IgtLabel.Visible = False
        '
        'TickLabel
        '
        Me.TickLabel.AutoSize = True
        Me.TickLabel.Location = New System.Drawing.Point(415, 7)
        Me.TickLabel.Name = "TickLabel"
        Me.TickLabel.Size = New System.Drawing.Size(24, 13)
        Me.TickLabel.TabIndex = 57
        Me.TickLabel.Text = "tick"
        Me.TickLabel.Visible = False
        '
        'Clock
        '
        Me.Clock.Location = New System.Drawing.Point(55, 3)
        Me.Clock.Name = "Clock"
        Me.Clock.ReadOnly = True
        Me.Clock.Size = New System.Drawing.Size(162, 20)
        Me.Clock.TabIndex = 56
        '
        'CollectionDateLabel
        '
        Me.CollectionDateLabel.AutoSize = True
        Me.CollectionDateLabel.Location = New System.Drawing.Point(265, 36)
        Me.CollectionDateLabel.Name = "CollectionDateLabel"
        Me.CollectionDateLabel.Size = New System.Drawing.Size(79, 13)
        Me.CollectionDateLabel.TabIndex = 55
        Me.CollectionDateLabel.Text = "Collection Date"
        Me.CollectionDateLabel.Visible = False
        '
        'EntryDate
        '
        Me.EntryDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.EntryDate.Location = New System.Drawing.Point(345, 32)
        Me.EntryDate.Name = "EntryDate"
        Me.EntryDate.Size = New System.Drawing.Size(69, 20)
        Me.EntryDate.TabIndex = 54
        Me.EntryDate.Visible = False
        '
        'CollectionTimingLabel
        '
        Me.CollectionTimingLabel.AutoSize = True
        Me.CollectionTimingLabel.Location = New System.Drawing.Point(282, 7)
        Me.CollectionTimingLabel.Name = "CollectionTimingLabel"
        Me.CollectionTimingLabel.Size = New System.Drawing.Size(63, 13)
        Me.CollectionTimingLabel.TabIndex = 53
        Me.CollectionTimingLabel.Text = "Collection is"
        Me.CollectionTimingLabel.Visible = False
        '
        'PreOrPostTick
        '
        Me.PreOrPostTick.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.PreOrPostTick.FormattingEnabled = True
        Me.PreOrPostTick.Items.AddRange(New Object() {"Before", "After"})
        Me.PreOrPostTick.Location = New System.Drawing.Point(345, 3)
        Me.PreOrPostTick.Name = "PreOrPostTick"
        Me.PreOrPostTick.Size = New System.Drawing.Size(69, 21)
        Me.PreOrPostTick.TabIndex = 51
        Me.PreOrPostTick.Visible = False
        '
        'StatusBox
        '
        Me.StatusBox.Location = New System.Drawing.Point(582, 0)
        Me.StatusBox.Multiline = True
        Me.StatusBox.Name = "StatusBox"
        Me.StatusBox.ReadOnly = True
        Me.StatusBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.StatusBox.Size = New System.Drawing.Size(281, 301)
        Me.StatusBox.TabIndex = 44
        '
        'SystemLabel
        '
        Me.SystemLabel.AutoSize = True
        Me.SystemLabel.Location = New System.Drawing.Point(9, 37)
        Me.SystemLabel.Name = "SystemLabel"
        Me.SystemLabel.Size = New System.Drawing.Size(41, 13)
        Me.SystemLabel.TabIndex = 29
        Me.SystemLabel.Text = "System"
        '
        'SoftDataGrid
        '
        Me.SoftDataGrid.AllowUserToOrderColumns = True
        Me.SoftDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.SoftDataGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Found, Me.Faction, Me.PrevInfluence, Me.Influence, Me.InfluenceDiff, Me.PrevState, Me.State})
        Me.SoftDataGrid.Location = New System.Drawing.Point(10, 59)
        Me.SoftDataGrid.MultiSelect = False
        Me.SoftDataGrid.Name = "SoftDataGrid"
        Me.SoftDataGrid.RowHeadersWidth = 24
        Me.SoftDataGrid.ShowCellErrors = False
        Me.SoftDataGrid.Size = New System.Drawing.Size(566, 190)
        Me.SoftDataGrid.TabIndex = 37
        Me.SoftDataGrid.Visible = False
        '
        'Found
        '
        Me.Found.HeaderText = "OCRed"
        Me.Found.Name = "Found"
        Me.Found.Width = 50
        '
        'Faction
        '
        Me.Faction.HeaderText = "Faction"
        Me.Faction.MaxInputLength = 250
        Me.Faction.Name = "Faction"
        Me.Faction.ToolTipText = "Faction Name"
        Me.Faction.Width = 170
        '
        'PrevInfluence
        '
        Me.PrevInfluence.HeaderText = "Prev Influence"
        Me.PrevInfluence.Name = "PrevInfluence"
        Me.PrevInfluence.ReadOnly = True
        Me.PrevInfluence.ToolTipText = "Influence from previous collection"
        Me.PrevInfluence.Width = 55
        '
        'Influence
        '
        Me.Influence.HeaderText = "Influence"
        Me.Influence.MaxInputLength = 5
        Me.Influence.Name = "Influence"
        Me.Influence.ToolTipText = "Faction Influence %"
        Me.Influence.Width = 55
        '
        'InfluenceDiff
        '
        Me.InfluenceDiff.HeaderText = "Influence Diff"
        Me.InfluenceDiff.Name = "InfluenceDiff"
        Me.InfluenceDiff.ReadOnly = True
        Me.InfluenceDiff.ToolTipText = "Influence difference after collection"
        Me.InfluenceDiff.Width = 55
        '
        'PrevState
        '
        Me.PrevState.HeaderText = "PrevState"
        Me.PrevState.Name = "PrevState"
        Me.PrevState.ToolTipText = "State from previous collection"
        Me.PrevState.Width = 76
        '
        'State
        '
        Me.State.HeaderText = "State"
        Me.State.Name = "State"
        Me.State.ToolTipText = "Faction State (if any)"
        Me.State.Width = 76
        '
        'LoadingLabel
        '
        Me.LoadingLabel.AutoSize = True
        Me.LoadingLabel.Location = New System.Drawing.Point(59, 37)
        Me.LoadingLabel.Name = "LoadingLabel"
        Me.LoadingLabel.Size = New System.Drawing.Size(54, 13)
        Me.LoadingLabel.TabIndex = 49
        Me.LoadingLabel.Text = "Loading..."
        '
        'AlwaysOnTopCheckbox
        '
        Me.AlwaysOnTopCheckbox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.AlwaysOnTopCheckbox.Location = New System.Drawing.Point(442, -1)
        Me.AlwaysOnTopCheckbox.Name = "AlwaysOnTopCheckbox"
        Me.AlwaysOnTopCheckbox.Size = New System.Drawing.Size(134, 23)
        Me.AlwaysOnTopCheckbox.TabIndex = 42
        Me.AlwaysOnTopCheckbox.Text = "Always on Top"
        Me.AlwaysOnTopCheckbox.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.AlwaysOnTopCheckbox.UseVisualStyleBackColor = True
        '
        'Tabs
        '
        Me.Tabs.Controls.Add(Me.SoftDataTab)
        Me.Tabs.Controls.Add(Me.SettingsTab)
        Me.Tabs.Controls.Add(Me.LogTab)
        Me.Tabs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Tabs.Location = New System.Drawing.Point(0, 0)
        Me.Tabs.Name = "Tabs"
        Me.Tabs.SelectedIndex = 0
        Me.Tabs.Size = New System.Drawing.Size(874, 330)
        Me.Tabs.TabIndex = 5
        '
        'ClockTimer
        '
        Me.ClockTimer.Enabled = True
        Me.ClockTimer.Interval = 5000
        '
        'GameScreenWidth
        '
        Me.GameScreenWidth.Location = New System.Drawing.Point(314, 137)
        Me.GameScreenWidth.Name = "GameScreenWidth"
        Me.GameScreenWidth.Size = New System.Drawing.Size(71, 20)
        Me.GameScreenWidth.TabIndex = 14
        Me.ToolTip1.SetToolTip(Me.GameScreenWidth, "Set how much of the screen to look at when capturing text. This will stop other t" &
        "ext like this window contaminating the results")
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(190, 140)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(115, 13)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Override Screen Width"
        Me.ToolTip1.SetToolTip(Me.Label6, "Set how much of the screen to look at when capturing text. This will stop other t" &
        "ext like this window contaminating the results")
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(190, 166)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(118, 13)
        Me.Label8.TabIndex = 1
        Me.Label8.Text = "Override Screen Height"
        Me.ToolTip1.SetToolTip(Me.Label8, "Set how much of the screen to look at when capturing text. This will stop other t" &
        "ext like this window contaminating the results")
        '
        'GameScreenHeight
        '
        Me.GameScreenHeight.Location = New System.Drawing.Point(314, 163)
        Me.GameScreenHeight.Name = "GameScreenHeight"
        Me.GameScreenHeight.Size = New System.Drawing.Size(71, 20)
        Me.GameScreenHeight.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.GameScreenHeight, "Set how much of the screen to look at when capturing text. This will stop other t" &
        "ext like this window contaminating the results")
        '
        'RecalcScreenResButton
        '
        Me.RecalcScreenResButton.Location = New System.Drawing.Point(193, 192)
        Me.RecalcScreenResButton.Name = "RecalcScreenResButton"
        Me.RecalcScreenResButton.Size = New System.Drawing.Size(67, 51)
        Me.RecalcScreenResButton.TabIndex = 4
        Me.RecalcScreenResButton.Text = "Recalc Screen Resolution"
        Me.ToolTip1.SetToolTip(Me.RecalcScreenResButton, "Add the entered system name to the list of official SEPP Systems")
        Me.RecalcScreenResButton.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(224, 249)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(161, 13)
        Me.Label10.TabIndex = 7
        Me.Label10.Text = "For people using Terra's laptop ^"
        Me.ToolTip1.SetToolTip(Me.Label10, "Set how much of the screen to look at when capturing text. This will stop other t" &
        "ext like this window contaminating the results")
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(388, 140)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(18, 13)
        Me.Label11.TabIndex = 68
        Me.Label11.Text = "px"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(389, 166)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(18, 13)
        Me.Label12.TabIndex = 69
        Me.Label12.Text = "px"
        '
        'TestOcrButton
        '
        Me.TestOcrButton.Location = New System.Drawing.Point(292, 189)
        Me.TestOcrButton.Name = "TestOcrButton"
        Me.TestOcrButton.Size = New System.Drawing.Size(93, 54)
        Me.TestOcrButton.TabIndex = 6
        Me.TestOcrButton.Text = "Test OCR"
        Me.ToolTip1.SetToolTip(Me.TestOcrButton, "Add the entered system name to the list of official SEPP Systems")
        Me.TestOcrButton.UseVisualStyleBackColor = True
        '
        'SeppOcrClient
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(874, 330)
        Me.Controls.Add(Me.Tabs)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "SeppOcrClient"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SEPP OCR Client"
        CType(Me.EDCapture, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LogTab.ResumeLayout(False)
        Me.SettingsTab.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.SoftDataTab.ResumeLayout(False)
        Me.SoftDataTab.PerformLayout()
        Me.ScanningPanel.ResumeLayout(False)
        Me.ScanningPanel.PerformLayout()
        CType(Me.SoftDataGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Tabs.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LoadTimer As Timer
    Friend WithEvents JourneyDir As DirectoryServices.DirectoryEntry
    Friend WithEvents FolderBrowser As FolderBrowserDialog
    Friend WithEvents CommsTimer As Timer
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents LogTab As TabPage
    Friend WithEvents LogTextBox As RichTextBox
    Friend WithEvents SettingsTab As TabPage
    Friend WithEvents CommanderName As TextBox
    Friend WithEvents JournalFolder As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents BrowserForDir As Button
    Friend WithEvents SoftDataTab As TabPage
    Friend WithEvents StatusBox As TextBox
    Friend WithEvents ViewWebTracker As Button
    Friend WithEvents SystemLabel As Label
    Friend WithEvents SoftDataGrid As DataGridView
    Friend WithEvents InfTotalVal As Label
    Friend WithEvents InfTotal As Label
    Friend WithEvents SelectedSystem As ComboBox
    Friend WithEvents UpdateBgsData As Button
    Friend WithEvents CaptureEDScreen As Button
    Friend WithEvents LoadingLabel As Label
    Friend WithEvents Tabs As TabControl
    Friend WithEvents AlwaysOnTopCheckbox As CheckBox
    Friend WithEvents ScanMarginLeft As TextBox
    Friend WithEvents Label14 As Label
    Friend WithEvents EDCapture As PictureBox
    Friend WithEvents LogOcrCheckbox As CheckBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents TickTimePicker As DateTimePicker
    Friend WithEvents Label4 As Label
    Friend WithEvents CollectionTimingLabel As Label
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents EntryDate As TextBox
    Friend WithEvents PreOrPostTick As ComboBox
    Friend WithEvents Label7 As Label
    Friend WithEvents SystemNameBox As TextBox
    Friend WithEvents RemoveButton As Button
    Friend WithEvents AddButton As Button
    Friend WithEvents CollectionDateLabel As Label
    Friend WithEvents IgtLabel As Label
    Friend WithEvents TickLabel As Label
    Friend WithEvents Clock As TextBox
    Friend WithEvents ClockTimer As Timer
    Friend WithEvents Found As DataGridViewCheckBoxColumn
    Friend WithEvents Faction As DataGridViewTextBoxColumn
    Friend WithEvents PrevInfluence As DataGridViewTextBoxColumn
    Friend WithEvents Influence As DataGridViewTextBoxColumn
    Friend WithEvents InfluenceDiff As DataGridViewTextBoxColumn
    Friend WithEvents PrevState As DataGridViewTextBoxColumn
    Friend WithEvents State As DataGridViewTextBoxColumn
    Friend WithEvents PrevEnteredByLabel As Label
    Friend WithEvents EnteredByLabel As Label
    Friend WithEvents ScanningPanel As Panel
    Friend WithEvents Label5 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents RecalcScreenResButton As Button
    Friend WithEvents Label8 As Label
    Friend WithEvents GameScreenHeight As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents GameScreenWidth As TextBox
    Friend WithEvents TestOcrButton As Button
End Class
