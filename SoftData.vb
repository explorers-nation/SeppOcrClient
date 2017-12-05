Imports Emgu.CV
Imports Emgu.CV.CvEnum
Imports Emgu.CV.OCR
Imports System.Text.RegularExpressions
Imports System.Text

Module SoftData
    Private seppsOcr As Tesseract
    Private selectedSystem As String = ""
    Private allStates As New Hashtable()
    Private exeDir As String = AppDomain.CurrentDomain.BaseDirectory
    Private influenceAccountedFor As Decimal = 0
    Private systemFactions As New Dictionary(Of String, List(Of Faction))
    Private factions As List(Of Faction)
    Private OCRUpdating As Boolean = False
    Public Property Ready As Boolean = False

    Friend Sub ProcEDScreen(bitmapImage As System.Drawing.Bitmap)
        If Not OCRUpdating Then
            Try
                OCRUpdating = True
                UpdateFactionsData()
                seppsOcr = New Tesseract() ' OcrEngineMode.TesseractCubeCombined
                seppsOcr.SetVariable("tessedit_char_whitelist", "QWERTYUIOPASDFGHJKLZXCVBNM.0987654321%:")
                seppsOcr.Init(exeDir + "\tessdata", "eng", OcrEngineMode.TesseractOnly)
                seppsOcr.Recognize(New Image(Of [Structure].Gray, Byte)(bitmapImage))
                Dim elements() As String
                Dim stringSeparators() As String = {vbCrLf}
                elements = seppsOcr.GetText.Split(stringSeparators, StringSplitOptions.None)
                Dim factionFirstLine As String = ""
                Dim factionName As String = ""
                Dim influence As String = ""
                Dim influenceVal As Decimal = 0
                For Each line As String In elements
                    If Trim(line) <> "" Then
                        ' Uncommit to see OCR text
                        ' SeppOcrClient.LogEverywhere(line)

                        ' line = whitelistChars(line)
                        line = UCase(line)
                        If factionFirstLine <> "" Then
                            LogOcrLine(line)
                            If Not isGovermentText(line) Then
                                factionFirstLine = Trim(factionFirstLine + " " + Trim(line))
                            End If
                            factionName = MatchFaction(factionFirstLine)
                            ' MsgBox(catchFaction + vbNewLine + factionName)
                            factionFirstLine = ""
                            influence = ""
                            influenceVal = 0

                        End If
                        If isFactionText(line) Then
                            factionFirstLine = Mid(line, 10, Len(line) - 9)
                            LogOcrLine(line)
                        End If
                        If factionName <> "" AndAlso isInfluenceText(line) Then
                            influenceVal = MatchInfluence(Trim(line))
                            influence = Replace(influenceVal.ToString, ",", ".")
                            LogOcrLine(line)
                        End If
                        If influenceVal <> 0 AndAlso isStateText(line) Then
                            Dim s As String = MatchState(Trim(line))
                            AddEDCaptureText(factionName, influence, s, influenceVal)
                            factionName = ""
                            influence = ""
                            influenceVal = 0
                            LogOcrLine(line)
                        End If
                        If isRelationshipText(line) Then
                            factionName = ""
                            influence = ""
                            influenceVal = 0
                            LogOcrLine(line)
                        End If
                    End If
                Next
            Catch ex As Exception
                MsgBox("Something went wrong with OCR - Try Again")
            End Try
            OCRUpdating = False
        End If
    End Sub
    Private Sub LogOcrLine(line As String)
        If Parameters.GetParameter("logOcrText") = "True" Then
            SeppOcrClient.LogOutput(line)
        End If
    End Sub

    Public Function AreAllFactionsOCRed() As Boolean
        Return factions.FirstOrDefault(Function(f) Not f.Found) Is Nothing
    End Function

    Private Sub AddEDCaptureText(factionName As String, influence As String, state As String, influenceVal As Decimal)
        Try
            Dim faction = factions.First(Function(f) f.FactionName.Equals(factionName))
            If faction IsNot Nothing Then
                If Not faction.Found Then
                    faction.Influence = influenceVal
                    faction.State = state
                    If influenceVal > 0 Then
                        faction.Found = True
                    End If
                    UpdateDataGridRow(faction, True)
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub
    Public Sub UpdateDataGridRow(faction As Faction, Optional scrollToChanges As Boolean = False)
        Dim doInsert As Boolean = True
        Dim influenceDiff = CalcInfluenceDiff(faction.PrevInfluence.ToString, faction.Influence.ToString)
        For Each row As DataGridViewRow In SeppOcrClient.SoftDataGrid.Rows
            If Not row.IsNewRow Then
                If row.Cells(SeppOcrClient.ColumnTypes.Faction).Value IsNot Nothing Then
                    If row.Cells(SeppOcrClient.ColumnTypes.Faction).Value.ToString = faction.FactionName Then
                        row.Cells(SeppOcrClient.ColumnTypes.Influence).Value = faction.Influence
                        row.Cells(SeppOcrClient.ColumnTypes.State).Value = faction.State
                        row.Cells(SeppOcrClient.ColumnTypes.PrevInfluence).Value = faction.PrevInfluence
                        row.Cells(SeppOcrClient.ColumnTypes.InfluenceDiff).Value = influenceDiff
                        row.Cells(SeppOcrClient.ColumnTypes.PrevState).Value = faction.PrevState
                        row.Cells(SeppOcrClient.ColumnTypes.Found).Value = faction.Found
                        If scrollToChanges Then
                            Dim scrollIndex = row.Index - 6
                            If scrollIndex >= 0 Then
                                SeppOcrClient.SoftDataGrid.FirstDisplayedScrollingRowIndex = row.Index - 6
                            End If
                        End If
                        doInsert = False
                        Exit For
                    End If
                End If
            End If
        Next
        If doInsert Then
            SeppOcrClient.SoftDataGrid.Rows.Add(
                faction.Found,
                faction.FactionName,
                faction.PrevInfluence,
                faction.Influence,
                influenceDiff,
                faction.PrevState,
                faction.State)
        End If
    End Sub
    Public Function CalcInfluenceDiff(prevInfluence As String, influence As String) As String
        Try
            Dim influenceVal = Decimal.Parse(influence)
            Dim prevInfluenceVal = Decimal.Parse(prevInfluence)
            If prevInfluenceVal > 0 And influenceVal > 0 Then
                Dim diff = (influenceVal - prevInfluenceVal).ToString
                If Decimal.Parse(diff) = 0 Then
                    diff = ""
                ElseIf Decimal.Parse(diff) > 0 Then
                    diff = "+" & diff
                End If
                Return diff
            End If
        Catch ex As Exception
            Return ""
        End Try
        Return ""
    End Function

    Friend Sub ProcessOCRTextChg()
        If Ready And Not OCRUpdating Then
            UpdateFactionsData()
            Dim i As Decimal = 0
            For Each row As DataGridViewRow In SeppOcrClient.SoftDataGrid.Rows
                If Not row.IsNewRow Then
                    If Not row.IsNewRow Then
                        Dim n As Decimal = 0
                        Try
                            n = Decimal.Parse(row.Cells(SeppOcrClient.ColumnTypes.Influence).Value.ToString)
                        Catch ex As Exception
                            ' Don't care
                        End Try
                        i = i + n
                    End If
                End If
            Next

            influenceAccountedFor = i
            SeppOcrClient.InfTotalVal.Text = influenceAccountedFor.ToString
            If HasUserFinishedOCRing() Then
                SeppOcrClient.InfTotal.ForeColor = SeppOcrClient.ColorSuccess
                SeppOcrClient.InfTotalVal.ForeColor = SeppOcrClient.ColorSuccess
                SeppOcrClient.SoftDataGrid.Columns(SeppOcrClient.ColumnTypes.InfluenceDiff).DefaultCellStyle.ForeColor = SeppOcrClient.ColorSuccess
                SeppOcrClient.UpdateBgsData.ForeColor = Color.DarkBlue
            Else
                SeppOcrClient.InfTotal.ForeColor = SeppOcrClient.ColorAttention
                SeppOcrClient.InfTotalVal.ForeColor = SeppOcrClient.ColorAttention
                SeppOcrClient.SoftDataGrid.Columns(SeppOcrClient.ColumnTypes.InfluenceDiff).DefaultCellStyle.ForeColor = SeppOcrClient.ColorAttention
                SeppOcrClient.UpdateBgsData.ForeColor = SeppOcrClient.ColorAttention
            End If

            If factions IsNot Nothing AndAlso factions.Count > 0 Then
                Dim faction = factions.First
                If Not String.IsNullOrEmpty(faction.EntryDate) Then
                    SeppOcrClient.EnteredByLabel.Text = "Today's data: " & faction.Commander & " on " & faction.EntryDate
                    SeppOcrClient.EnteredByLabel.Show()
                Else
                    SeppOcrClient.EnteredByLabel.Hide()
                End If
                If Not String.IsNullOrEmpty(faction.PrevEntryDate) Then
                    SeppOcrClient.PrevEnteredByLabel.Text = "Prev data: " & faction.PrevCommander & " on " & faction.PrevEntryDate
                    SeppOcrClient.PrevEnteredByLabel.Show()
                Else
                    SeppOcrClient.PrevEnteredByLabel.Hide()
                End If
            Else
                SeppOcrClient.EnteredByLabel.Hide()
                SeppOcrClient.PrevEnteredByLabel.Hide()
            End If
        End If
    End Sub

    Public Function HasUserFinishedOCRing() As Boolean
        Return influenceAccountedFor > 99.8 And influenceAccountedFor <= 100.1
    End Function

    Friend Sub ProcessSystemChange(systemName As String)
        If systemName <> selectedSystem Then
            SeppOcrClient.SoftDataGrid.Enabled = False
            ProcessOCRTextChg()
            SaveSystemFactions()
            LoadSystemFactions(systemName)
            selectedSystem = systemName
            SeppOcrClient.SoftDataGrid.Enabled = True
            ProcessOCRTextChg()
        End If
    End Sub

    Private Function MatchFaction(targetFaction As String) As String
        Dim factionName As String = ""
        targetFaction = Trim(UCase(targetFaction))
        targetFaction = Regex.Replace(targetFaction, "[,'\.]", "")
        targetFaction = Trim(Replace(targetFaction, "  ", " "))
        If factions IsNot Nothing Then
            If factions.FirstOrDefault(Function(faction) faction.Equals(targetFaction)) IsNot Nothing Then
                Return targetFaction
            Else
                For Each faction In factions
                    Dim minMatchingChars As Integer = CInt(targetFaction.Length * 0.75)
                    If WordMatchScore(targetFaction, faction.FactionName) > minMatchingChars Then
                        Return faction.FactionName
                    End If
                Next
            End If
        End If
        Return ""
    End Function

    Private Function MatchInfluence(influence As String) As Decimal
        Dim val As Decimal = 0
        Dim pattern As String = ".*\b(\d{1,2})\.?(\d).*"
        Try
            influence = Regex.Replace(influence, "['""][1I]", "1")
            influence = influence.Replace("B", "8")
            influence = influence.Replace(",", ".")
            influence = influence.Replace("  ", " ")
            influence = Regex.Replace(influence, "([\d.]) ", "$1")
            If Regex.Match(influence, pattern).Success Then
                influence = Regex.Replace(influence, pattern, "$1.$2")
                val = Decimal.Parse(influence)
            End If
        Catch ex As Exception
            val = 0
        End Try
        Return val
    End Function

    Private Function MatchState(stateText As String) As String
        Dim text As String = Replace(stateText, "_", "")
        text = Regex.Replace(text, "^[^ ]* +(\w.+)$", "$1")

        If WordMatchScore(text, "CIVIL UNREST") > 8 Then
            Return "Civil unrest"
        End If
        If WordMatchScore(text, "CIVIL WAR") >= 7 Then
            Return "Civil war"
        End If
        If WordMatchScore(text, "INVESTMENT") >= 6 Then
            Return "Investment"
        End If
        If WordMatchScore(text, "EXPANSION") >= 6 Then
            Return "Expansion"
        End If
        If WordMatchScore(text, "LOCKDOWN") >= 5 Then
            Return "Lockdown"
        End If
        If WordMatchScore(text, "ELECTION") >= 5 Then
            Return "Election"
        End If
        If WordMatchScore(text, "OUTBREAK") >= 5 Then
            Return "Outbreak"
        End If
        If WordMatchScore(text, "RETREAT") >= 4 Then
            Return "Retreat"
        End If
        If WordMatchScore(text, "FAMINE") >= 4 Then
            Return "Famine"
        End If
        If WordMatchScore(text, "BOOM") >= 3 Then
            Return "Boom"
        End If
        If WordMatchScore(text, "BUST") >= 3 Then
            Return "Bust"
        End If
        If WordMatchScore(text, "WAR") >= 2 Then
            Return "War"
        End If
        Return " "
    End Function

    Public Sub AddFactions(systemName As String, factions As List(Of Faction))
        systemFactions.Add(systemName, factions)
    End Sub
    Public Sub ClearFaction(factionName As String)
        Try
            Dim factions = systemFactions(selectedSystem)
            Dim faction = factions.First(Function(f) f.FactionName.Equals(factionName))
            faction.Influence = 0
            faction.State = ""
        Catch ex As Exception

        End Try
    End Sub
    Public Sub SaveSystemFactions()
        UpdateFactionsData()

        If factions IsNot Nothing And systemFactions IsNot Nothing Then
            If Not String.IsNullOrEmpty(selectedSystem) And systemFactions.ContainsKey(selectedSystem) Then
                systemFactions(selectedSystem) = factions
            End If
        End If
    End Sub
    Public Sub LoadSystemFactions(systemName As String)
        SeppOcrClient.SoftDataGrid.Rows.Clear()

        If Not systemFactions.ContainsKey(systemName) Then
            systemFactions.Add(systemName, New List(Of Faction))
        End If
        factions = systemFactions(systemName)
        For Each faction In factions
            UpdateDataGridRow(faction)
        Next
    End Sub
    Public Function WhitelistChars(cleanString As String) As String
        Try
            Dim ch As Char, ln As Integer, x As Integer = 0
            ln = cleanString.Length
            Do While x < ln
                ch = cleanString.Chars(x)
                If Not (Char.IsLetterOrDigit(ch)) And Not (ch = " ") And Not (ch = "-") And Not (ch = ":") Then
                    cleanString = cleanString.Replace(ch, "")
                    ln -= 1
                    x -= 1
                End If
                x += 1
            Loop
        Catch ex As Exception

        End Try
        Return cleanString
    End Function

    Public Function AddSystem(systemName As String) As String
        Dim cleanSystemName As String = SoftData.WhitelistChars(Trim(systemName))
        SeppOcrClient.SelectedSystem.Items.Add(cleanSystemName)

        Return cleanSystemName
    End Function
    Public Function RemoveSystem(systemName As String) As String
        Dim cleanSystemName As String = SoftData.WhitelistChars(Trim(systemName))
        SeppOcrClient.SelectedSystem.Items.Remove(cleanSystemName)

        Return cleanSystemName
    End Function

    Public Sub UpdateFactionsData()
        If Ready AndAlso SeppOcrClient.SoftDataGrid.Visible AndAlso SeppOcrClient.SoftDataGrid.Enabled Then
            If systemFactions IsNot Nothing Then
                If Not systemFactions.ContainsKey(selectedSystem) Then
                    systemFactions.Add(selectedSystem, New List(Of Faction))
                End If
                Dim updatedFactions = systemFactions(selectedSystem)
                For Each row As DataGridViewRow In SeppOcrClient.SoftDataGrid.Rows
                    If updatedFactions IsNot Nothing And Not row.IsNewRow Then
                        Dim gridFaction As String = Trim(UCase(WhitelistChars(row.Cells(SeppOcrClient.ColumnTypes.Faction).Value.ToString)))
                        If Not String.IsNullOrEmpty(gridFaction) Then
                            If factions.FirstOrDefault(Function(f) f.FactionName.Equals(gridFaction)) Is Nothing Then
                                Dim faction As New Faction() With {
                                    .FactionName = gridFaction
                                }
                                updatedFactions.Add(faction)
                            End If
                        End If
                    End If
                Next
                For Each row As DataGridViewRow In SeppOcrClient.SoftDataGrid.Rows
                    If Not row.IsNewRow Then
                        Dim gridFaction As String = Trim(UCase(WhitelistChars(row.Cells(SeppOcrClient.ColumnTypes.Faction).Value.ToString)))
                        If Not String.IsNullOrEmpty(gridFaction) Then
                            Dim faction = updatedFactions.First(Function(f) f.FactionName.Equals(gridFaction))
                            Try
                                faction.Influence = Decimal.Parse(row.Cells(SeppOcrClient.ColumnTypes.Influence).Value.ToString)
                            Catch ex As Exception
                            End Try
                            faction.State = SafeString(row.Cells(SeppOcrClient.ColumnTypes.State).Value)
                            Try
                                faction.Found = CBool(row.Cells(SeppOcrClient.ColumnTypes.Found).Value)
                            Catch ex As Exception
                            End Try
                        End If
                    End If
                Next
            End If
        End If
    End Sub
    Public Sub SendFactionsData()
        Dim factionsSent = 0
        Dim entryDate = SeppOcrClient.EntryDate.Text

        For Each faction In factions
            Try
                If Not String.IsNullOrEmpty(faction.FactionName) Then
                    If faction.Influence = 0 And faction.PrevInfluence > 0 Then
                        faction.State = "Gone"
                    End If
                    If faction.Influence > 0 And faction.PrevInfluence = 0 Then
                        faction.State = "New"
                    End If

                    If faction.Influence > 0 Or faction.PrevInfluence > 0 Then
                        faction.Commander = SeppOcrClient.CommanderName.Text
                        faction.EntryDate = entryDate
                        faction.System = SeppOcrClient.SelectedSystem.SelectedItem.ToString

                        Dim cwaitForCompletion As Boolean = Comms.SendUpdate(
                            faction.System & ":" &
                            faction.FactionName.ToString.ToUpper & ":" &
                            faction.State.ToString & ":" &
                            faction.Influence.ToString & ":OCR v" &
                            SeppOcrClient.getVersion() & ":" &
                            faction.EntryDate)
                        factionsSent += 1
                    Else
                        SeppOcrClient.LogEverywhere("Skipping " & faction.FactionName & " because the infuence is zero")
                    End If
                End If
            Catch ex As Exception
                SeppOcrClient.LogEverywhere("Unable to send one of the rows")
            End Try
        Next
        SeppOcrClient.LogOutput("Updated " & factionsSent & "/" & factions.Count & " Factions in " & SeppOcrClient.SelectedSystem.SelectedItem.ToString)

    End Sub

    Private Function SafeString(str As Object) As String
        Try
            Return str.ToString
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Function isFactionText(line As String) As Boolean
        Return LabelMatchScore(line, "FACTION", ExtraChars:=3) >= 5
    End Function

    Private Function isInfluenceText(line As String) As Boolean
        Return LabelMatchScore(line, "INFLUENCE", ExtraChars:=2) >= 7
    End Function

    Private Function isStateText(line As String) As Boolean
        Return LabelMatchScore(line, "STATE", ExtraChars:=3) >= 4
    End Function

    Private Function isGovermentText(line As String) As Boolean
        Return LabelMatchScore(line, "GOVERMENT", ExtraChars:=3) >= 7
    End Function
    Private Function isRelationshipText(line As String) As Boolean
        Return LabelMatchScore(line, "RELATIONSHIP", ExtraChars:=3) >= 10
    End Function

    Private Function LabelMatchScore(line As String, matchWord As String, Optional ExtraChars As Integer = 0) As Integer
        Dim found As Boolean = False
        If Len(line) >= matchWord.Count + ExtraChars And line.IndexOf(" ") > -1 Then
            Dim label = line.Split(" "c)(0)
            Return WordMatchScore(label, matchWord)
        End If
        Return 0
    End Function

    Private Function WordMatchScore(line As String, matchWord As String) As Integer
        Dim found As Boolean = False
        Dim candidate = line.Replace("  ", " ")
        candidate = candidate.Replace("|<", " K")
        candidate = Regex.Replace(candidate, "['""][1I]", "1")

        candidate = Regex.Replace(candidate, "[^\w ]+", "")
        matchWord = Regex.Replace(matchWord, "[^\w ]+", "")
        If Len(candidate) >= matchWord.Count - 1 Then
            Dim score = 0
            Dim n As Integer
            For n = 0 To candidate.Count - 1
                If n < matchWord.Count Then
                    Dim pattern = MatchGlpyhsPatternFor(matchWord.Chars(n))
                    Dim prevIndex = Math.Min(Math.Max(n - 1, 0), candidate.Count - 1)
                    Dim currentIndex = Math.Min(n, candidate.Count - 1)
                    Dim nextIndex = Math.Min(n + 1, candidate.Count - 1)

                    If Regex.Match(candidate.ElementAt(prevIndex), pattern).Success Then
                        candidate = ReplaceAt(candidate, prevIndex, "#"c)
                        score += 1
                    ElseIf Regex.Match(candidate.ElementAt(currentIndex), pattern).Success Then
                        candidate = ReplaceAt(candidate, currentIndex, "#"c)
                        score += 1
                    ElseIf Regex.Match(candidate.ElementAt(nextIndex), pattern).Success Then
                        candidate = ReplaceAt(candidate, nextIndex, "#"c)
                        score += 1
                    End If
                End If
            Next
            Return score
        End If
        Return 0
    End Function
    Private Function MatchGlpyhsPatternFor(c As Char) As String
        Select Case c.ToString
            Case "B"
                Return "[BRE]"
            Case "C"
                Return "[CEO]"
            Case "F"
                Return "[FP]"
            Case "G"
                Return "[GC]"
            Case "O"
                Return "[OD]"
            Case "R"
                Return "R|H|FI"
            Case "8"
                Return "[8B]"
            Case Else
                Return c.ToString
        End Select
    End Function
    Public Function ReplaceAt(input As String, index As Integer, newChar As Char) As String
        Dim chars = input.ToCharArray()
        chars(index) = newChar
        Return New String(chars)
    End Function
    Public Sub UpdateCurrentFactionData(PrevEntryDate As String)
        For Each systemName In SeppOcrClient.SelectedSystem.Items
            If systemFactions.ContainsKey(systemName.ToString) Then
                For Each faction In systemFactions(systemName.ToString)
                    If PrevEntryDate = faction.PrevEntryDate Then
                        faction.EntryDate = ""
                        faction.Commander = ""
                        faction.Influence = 0
                        faction.State = ""
                    End If
                    If SeppOcrClient.EntryDate.Text = faction.PrevEntryDate Then
                        faction.EntryDate = faction.PrevEntryDate
                        faction.Commander = faction.PrevCommander
                        faction.Influence = faction.PrevInfluence
                        faction.State = faction.PrevState
                    End If
                Next
            End If
        Next
        If Not String.IsNullOrEmpty(selectedSystem) Then
            LoadSystemFactions(selectedSystem)
        End If
        ProcessOCRTextChg()
    End Sub
    Public Sub AlreadyProcessedCheck()
        If selectedSystem = "ELEU" Then
            Try
                If factions.FirstOrDefault(Function(f) _
                                               Not String.IsNullOrEmpty(
                                                    CalcInfluenceDiff(f.PrevInfluence.ToString(), f.Influence.ToString()))
                                               ) Is Nothing Then
                    MessageBox.Show("We're noticing that that the influence has changed in Eleu since the BGS update. It's likely that the Tick hasn't happened yet today. It's time does vary.",
                                    "Hmm...", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            Catch ex As Exception
            End Try
        End If

    End Sub

End Module