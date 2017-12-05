Imports System.IO
Imports System.Deployment.Application
Imports System.Globalization

Public Class SeppOcrClient
    Public Enum ColumnTypes
        Found = 0
        Faction = 1
        PrevInfluence = 2
        Influence = 3
        InfluenceDiff = 4
        PrevState = 5
        State = 6
    End Enum

    Public Enum TickTimeState
        PreTick = 0
        PostTick = 1
    End Enum

    Private AppDataDir As String = Environment.GetEnvironmentVariable("USERPROFILE") + "\AppData\Local\SeppOcrClient"
    Private clientVersion As String = Application.ProductVersion
    Private noLogDups As String = ""

    Public Property ColorLockedBackground As Color = ColorTranslator.FromHtml("#F0F0F0")
    Public Property ColorLockedForeground As Color = ColorTranslator.FromHtml("#333")
    Public Property ColorAttention As Color = ColorTranslator.FromHtml("#C22")
    Public Property ColorSuccess As Color = Color.DarkGreen

    Private Sub SeppOcrClient_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not Directory.Exists(AppDataDir) Then
            My.Computer.FileSystem.CreateDirectory(AppDataDir)
        End If
        Parameters.InitDefaultParameters()  ' Call this f to set default values
        Comms.InitCommsCodes()
        JournalFolder.Text = Parameters.GetParameter("JournalDirectory")
        Dim logOcrText As String = Parameters.GetParameter("LogOcrText")
        If logOcrText = "True" Then
            LogOcrCheckbox.Checked = True
        End If
        ScanMarginLeft.Text = Parameters.GetParameter("ScanMarginLeft2")
        UpdateClock()
        InitTickTime()
        CommanderName.Text = DataCache.GetDataCache("Store", "LastCommander")
        If String.IsNullOrEmpty(CommanderName.Text) Then
            CommanderName.Text = "Jameson"
        End If
        LogOutput("Version: " & getVersion())
        LogOutput("AppData: " & AppDataDir)
        SetupGrid()
        Me.Refresh()                     ' Ensure the app is fully loaded before 
        CommsTimer.Enabled = True         ' opening comms - avoids possible exceptions.
    End Sub
    Private Sub SetupGrid()
        SoftDataGrid.Columns(ColumnTypes.PrevInfluence).DefaultCellStyle.BackColor = ColorLockedBackground
        SoftDataGrid.Columns(ColumnTypes.PrevState).DefaultCellStyle.BackColor = ColorLockedBackground
        SoftDataGrid.Columns(ColumnTypes.InfluenceDiff).DefaultCellStyle.BackColor = ColorLockedBackground

        SoftDataGrid.Columns(ColumnTypes.PrevInfluence).DefaultCellStyle.ForeColor = ColorLockedForeground
        SoftDataGrid.Columns(ColumnTypes.PrevState).DefaultCellStyle.ForeColor = ColorLockedForeground
        SoftDataGrid.Columns(ColumnTypes.InfluenceDiff).DefaultCellStyle.ForeColor = ColorAttention
    End Sub

    Private Sub BrowserForDir_Click(sender As Object, e As EventArgs) Handles BrowserForDir.Click
        If FolderBrowser.ShowDialog() = DialogResult.OK Then
            JournalFolder.Text = FolderBrowser.SelectedPath
        End If
    End Sub
    Private Sub JournalFolder_TextChanged(sender As Object, e As EventArgs) Handles JournalFolder.TextChanged
        Parameters.SetParameter("JournalDirectory", JournalFolder.Text)
    End Sub
    Public Sub LogEverywhere(message As String)
        StatusLog(message)
        LogOutput(message)
    End Sub
    Public Sub StatusLog(message As String)
        If Not String.IsNullOrEmpty(StatusBox.Text) Then
            message = vbNewLine & message
        End If
        StatusBox.AppendText(message)
    End Sub

    Friend Sub LogOutput(logText As String)
        Debug.WriteLine(logText)
        Try
            If noLogDups <> logText Then
                LogTextBox.AppendText(Now().ToString + " - " + logText + vbNewLine)
                If Tabs.SelectedTab Is LogTab Then
                    LogTab.Text = "Log"
                Else
                    LogTab.Text = "Log *"
                End If
            End If
        Catch ex As Exception
            LogTextBox.Text = "" ' Guess it's full - emptying is a harsh workaround but lets see if it ever happens
        End Try
    End Sub
    Private Sub CaptureEDScreen_Click(sender As Object, e As EventArgs) Handles CaptureEDScreen.Click
        Dim bounds As Rectangle
        Dim screenshot As System.Drawing.Bitmap
        Dim graph As Graphics
        Dim scanMarginPercentage As Integer = 23

        If SoftData.AreAllFactionsOCRed() Then
            MessageBox.Show("I can't do that Commander!" & vbCrLf &
                "All rows are marked As OCRed which means theres nothing left to update.",
                "Orly", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        CaptureEDScreen.Enabled = False
        ScanningPanel.Show()
        ScanningPanel.Refresh()

        bounds = Screen.PrimaryScreen.Bounds
        Try
            scanMarginPercentage = Integer.Parse(Parameters.GetParameter("ScanMarginLeft2"))
        Catch ex As Exception
        End Try
        Dim scanMargin As Integer = CInt(bounds.Width * (scanMarginPercentage / 100))

        screenshot = New System.Drawing.Bitmap(scanMargin, bounds.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb) ' Format32bppArgb
        graph = Graphics.FromImage(screenshot)
        graph.CopyFromScreen(bounds.X, bounds.Y, 0, 0, New Size(scanMargin, bounds.Height), CopyPixelOperation.SourceCopy)

        CompleteEDScreen(screenshot)
        ScanningPanel.Hide()
        CaptureEDScreen.Enabled = True
    End Sub

    Private Sub CompleteEDScreen(screenshot As System.Drawing.Bitmap)
        Const resizeScale = 4
        Dim procBitmap As New Bitmap(CInt(screenshot.Width * resizeScale), CInt(screenshot.Height * resizeScale))
        Dim grBitmap As Graphics = Graphics.FromImage(procBitmap)
        grBitmap.DrawImage(screenshot, 0, 0, procBitmap.Width, procBitmap.Height)
        Call Global.SeppOcrClient.ProcEDScreen(procBitmap)
        EDCapture.Image = procBitmap

        EDCapture.Refresh()
        Call Global.SeppOcrClient.ProcessOCRTextChg()
        If CInt(InfTotalVal.Text) > 98 And CInt(InfTotalVal.Text) < 102 Then
            SoftData.AlreadyProcessedCheck()
        End If
    End Sub

    Private Sub UpdateBgsData_Click(sender As Object, e As EventArgs) Handles UpdateBgsData.Click
        If SelectedSystem.SelectedItem IsNot Nothing Then
            If Not SoftData.HasUserFinishedOCRing() Then
                Dim ok = MsgBox("The influence total looks off. Are you sure you want update the server?", MsgBoxStyle.OkCancel)
                If ok <> MsgBoxResult.Ok Then
                    Return
                End If
            End If
        End If
        StatusLog("Sending '" & SelectedSystem.SelectedItem.ToString & "' data to server...")
        Dim factionsSent As Integer = 0
        Dim done As Boolean = False
        SoftData.SendFactionsData()
        GoToNextSystem()
    End Sub

    Private Async Sub commsTimer_Tick(sender As Object, e As EventArgs) Handles CommsTimer.Tick
        Try
            Await Comms.ProcUpdate()
            SoftData.Ready = True
        Catch ex As Exception
            LogEverywhere("update went boom")
        End Try
    End Sub

    Private Sub SelectedSystem_SelectedIndexChanged(sender As Object, e As EventArgs) Handles SelectedSystem.SelectedIndexChanged
        If SelectedSystem.SelectedItem.ToString <> "" Then
            ResetSystemNameBox(SelectedSystem.SelectedItem.ToString)
            My.Computer.Clipboard.SetText(SystemNameBox.Text)
            StatusLog("Copied '" & SelectedSystem.SelectedItem.ToString & "' to clipboard!")
            Call Global.SeppOcrClient.ProcessSystemChange(SelectedSystem.SelectedItem.ToString)
            SoftDataGrid.Select()
        End If
    End Sub

    Private Sub EDCapture_Click(sender As Object, e As EventArgs)
        If Not EDCapture.Image Is Nothing Then
            ViewImage.ImgBox.Width = EDCapture.Image.Width
            ViewImage.ImgBox.Height = EDCapture.Image.Height
            ViewImage.ImgBox.Image = EDCapture.Image
            ViewImage.Show()
        End If
    End Sub

    Private Sub SoftDataGrid_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles SoftDataGrid.CellValueChanged
        Call Global.SeppOcrClient.ProcessOCRTextChg()
    End Sub

    Private Sub onTop_CheckedChanged(sender As Object, e As EventArgs) Handles AlwaysOnTopCheckbox.CheckedChanged
        If AlwaysOnTopCheckbox.Checked Then
            Me.TopMost = True
        Else
            Me.TopMost = False
        End If
    End Sub

    Friend Function getVersion() As String
        If ApplicationDeployment.IsNetworkDeployed Then
            Return ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString
        Else
            Return Application.ProductVersion
        End If
    End Function

    Private Sub ViewWebTracker_Click(sender As Object, e As EventArgs) Handles ViewWebTracker.Click
        Dim webAddress As String = "http://sepps-bgs-tracker.s3-website-us-east-1.amazonaws.com/"
        Process.Start(webAddress)
    End Sub

    Private Sub GoToNextSystem()
        If SelectedSystem.SelectedIndex < 0 Then
            SelectedSystem.SelectedIndex = 1
        ElseIf SelectedSystem.SelectedIndex = SelectedSystem.Items.Count - 1 Then
            StatusLog("No more systems!")
        Else
            SelectedSystem.SelectedIndex = SelectedSystem.SelectedIndex + 1
        End If
    End Sub

    Private Sub ResetSystemNameBox(systemName As String)
        SystemNameBox.Text = systemName
        AddButton.Enabled = False
        RemoveButton.Enabled = True
    End Sub

    Private Async Sub AddButton_Click(sender As Object, e As EventArgs)
        Dim systemName As String
        systemName = SystemNameBox.Text.ToUpper()

        StatusLog("Adding the System Name to the server...")
        If MessageBox.Show("Add " & systemName & " to the list of systems we're updating?",
                           "Orly?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = DialogResult.OK Then
            If Await Comms.AddSystemToRoster(systemName) Then
                StatusLog("Successfully added " & systemName & "to the SEPP known systems list!")
                SelectedSystem.SelectedIndex = SelectedSystem.Items.Count - 1
            Else
                StatusLog("Failed to add the system, sorry!")
            End If
        End If
    End Sub

    Private Sub SystemNameBox_TextChanged(sender As Object, e As EventArgs)
        AddButton.Enabled = True
        RemoveButton.Enabled = False
    End Sub

    Private Async Sub RemoveButton_Click(sender As Object, e As EventArgs)
        Dim systemName As String
        systemName = SystemNameBox.Text.ToUpper()

        StatusLog("Removing the System Name to the server...")
        If MessageBox.Show("So... you want to remove " & systemName & " from the list of systems we're updating?",
                           "Huh?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = DialogResult.OK Then
            If Await Comms.RemoveSystemFromRoster(systemName) Then
                StatusLog("Successfully removed " & systemName & "from the SEPP known systems list!")
            Else
                StatusLog("Failed to remove the system, sorry!")
            End If
        End If
    End Sub

    Private Sub SoftDataGrid_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles SoftDataGrid.CellEndEdit
        Dim cell = SoftDataGrid(e.ColumnIndex, e.RowIndex)

        PostChangeUpdate(cell)
    End Sub
    Private Sub PostChangeUpdate(cell As DataGridViewCell)
        Select Case cell.ColumnIndex
            Case ColumnTypes.Faction
                Try
                    Dim faction = cell.Value.ToString
                    cell.Value = Trim(UCase(SoftData.WhitelistChars(faction)))
                Catch ex As Exception
                End Try
            Case ColumnTypes.Influence
                Try
                    Dim influence = Decimal.Parse(cell.Value.ToString)
                Catch ex As Exception
                    cell.Value = "0"
                End Try

                Try
                    Dim prevInfluenceCell = SoftDataGrid(ColumnTypes.PrevInfluence, cell.RowIndex)
                    Dim InfluenceDiffCell = SoftDataGrid(ColumnTypes.InfluenceDiff, cell.RowIndex)
                    If prevInfluenceCell.Value IsNot Nothing AndAlso cell.Value IsNot Nothing Then
                        Dim diff = SoftData.CalcInfluenceDiff(prevInfluenceCell.Value.ToString, cell.Value.ToString)
                        InfluenceDiffCell.Value = diff
                    End If
                Catch ex As Exception
                End Try
        End Select

    End Sub

    Private Sub SoftDataGrid_KeyPress(sender As Object, e As KeyPressEventArgs) Handles SoftDataGrid.KeyPress
        Dim cell = SoftDataGrid.CurrentCell
        If Not cell.ReadOnly Then
            If e.KeyChar.Equals(vbBack) Then
                cell.Value = Nothing
                SoftDataGrid.NotifyCurrentCellDirty(True)
                PostChangeUpdate(cell)
            End If
        End If
    End Sub

    Private Sub SoftDataGrid_KeyDown(sender As Object, e As KeyEventArgs) Handles SoftDataGrid.KeyDown
        Dim cell = SoftDataGrid.CurrentCell

        If e.KeyCode = Keys.C AndAlso e.Modifiers = Keys.Control Then
            My.Computer.Clipboard.SetText(cell.Value.ToString)
        End If

        If Not cell.ReadOnly Then
            If e.KeyCode.Equals(Keys.Delete) Then
                cell.Value = Nothing
                SoftDataGrid.NotifyCurrentCellDirty(True)
                PostChangeUpdate(cell)
            End If

            If e.KeyCode = Keys.V AndAlso e.Modifiers = Keys.Control Then
                cell.Value = My.Computer.Clipboard.GetText()
                SoftDataGrid.NotifyCurrentCellDirty(True)
                PostChangeUpdate(cell)
            End If

            If e.KeyCode = Keys.X AndAlso e.Modifiers = Keys.Control Then
                My.Computer.Clipboard.SetText(cell.Value.ToString)
                cell.Value = Nothing
                SoftDataGrid.NotifyCurrentCellDirty(True)
                PostChangeUpdate(cell)
            End If
        End If

    End Sub

    Private Sub LogOcrCheckbox_CheckedChanged(sender As Object, e As EventArgs)
        If LogOcrCheckbox.Checked Then
            Parameters.SetParameter("LogOcrText", "True")
        Else
            Parameters.SetParameter("LogOcrText", "False")
        End If
    End Sub

    Private Sub ScanMarginLeft_TextChanged(sender As Object, e As EventArgs)
        Try
            Dim value = Integer.Parse(Trim(ScanMarginLeft.Text))
            Parameters.SetParameter("ScanMarginLeft2", ScanMarginLeft.Text)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub CommanderName_TextChanged(sender As Object, e As EventArgs) Handles CommanderName.TextChanged
        DataCache.SetDataCache("Store", "LastCommander", CommanderName.Text)
    End Sub

    Public Sub ShowBgsTools()
        If SelectedSystem.Items.Count > 0 Then
            SelectedSystem.SelectedIndex = 0
        End If

        SelectedSystem.Show()
        SoftDataGrid.Show()
        CollectionTimingLabel.Show()
        TickLabel.Show()
        CollectionDateLabel.Show()
        IgtLabel.Show()
        PreOrPostTick.Show()
        EntryDate.Show()
    End Sub

    Private Sub SoftDataGrid_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles SoftDataGrid.RowsRemoved
        Dim n As Integer
        For n = e.RowIndex To e.RowIndex + e.RowCount - 1
            Try
                Dim factionName = SoftDataGrid(n, ColumnTypes.Faction).Value.ToString
                SoftData.ClearFaction(factionName)
            Catch ex As Exception
            End Try
        Next

    End Sub
    Public Function TickTimeToDate(tickTimeValue As String) As Date
        Try
            Return Date.ParseExact(tickTimeValue, "HH:mm", CultureInfo.InvariantCulture)
        Catch ex As Exception
            Return Date.Parse("14:00")
        End Try
    End Function

    Private Sub TickTimePicker_ValueChanged(sender As Object, e As EventArgs) Handles TickTimePicker.ValueChanged
        If TickTimePicker.Value.ToString("HH:mm") <> "01:23" Then
            Parameters.SetParameter("TickTime", TickTimePicker.Value.ToString("HH:mm"))
        End If
    End Sub

    Private Sub InitTickTime()
        TickTimePicker.Value = TickTimeToDate(Parameters.GetParameter("TickTime"))
        If Date.UtcNow().TimeOfDay() > TickTimePicker.Value().TimeOfDay() Then
            PreOrPostTick.SelectedIndex = TickTimeState.PostTick
        Else
            PreOrPostTick.SelectedIndex = TickTimeState.PreTick
        End If
        UpdateCollectionDate()
    End Sub
    Private Sub UpdateCollectionDate()
        Dim DaysSince = 0
        If PreOrPostTick.SelectedIndex = TickTimeState.PreTick Then
            DaysSince = -1
        Else
        End If
        Dim PrevEntryDate = EntryDate.Text
        EntryDate.Text = Date.UtcNow().AddDays(DaysSince).ToString("yyyy-MM-dd")

        If Not String.IsNullOrEmpty(PrevEntryDate) Then
            SoftData.UpdateCurrentFactionData(PrevEntryDate)
        End If
    End Sub
    Private Sub PreOrPostTick_SelectedIndexChanged(sender As Object, e As EventArgs) Handles PreOrPostTick.SelectedIndexChanged
        UpdateCollectionDate()
    End Sub
    Private Sub UpdateClock()
        Clock.Text = Date.UtcNow.ToString("ddd yyyy-MM-dd HH:mm IGT")
    End Sub
    Private Sub ClockTimer_Tick(sender As Object, e As EventArgs) Handles ClockTimer.Tick
        UpdateClock()
    End Sub
End Class
