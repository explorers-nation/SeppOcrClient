Module Parameters
    Private defaultValues As New Hashtable()
    Private userValues As New Hashtable()
    Private iniFile As String = Environment.GetEnvironmentVariable("USERPROFILE") + "\AppData\Local\SeppOcrClient\SeppOcrClient.ini"

    Friend Sub InitDefaultParameters()
        ' SeppOcrClient Default Values
        defaultValues.Add("JournalDirectory", Environment.GetEnvironmentVariable("USERPROFILE") + "\Saved Games\Frontier Developments\Elite Dangerous\")
        defaultValues.Add("HostAddress", "")
        defaultValues.Add("HostPort", "4526")
        defaultValues.Add("UpdateSiteActivity", "O")
        defaultValues.Add("LogOcrText", "False")
        defaultValues.Add("ScanMarginLeft2", "23")
        defaultValues.Add("TickTime", "14:00")
    End Sub

    Private Function GetDefaultParameter(pKey As String) As String
        Dim retValue As String = ""
        For Each de As DictionaryEntry In defaultValues
            If de.Key.ToString = pKey Then
                retValue = CType(de.Value, String)
                Exit For
            End If
        Next de
        Return retValue
    End Function

    Friend Function GetParameter(pKey As String) As String
        Dim retValue As String = ""
        For Each de As DictionaryEntry In userValues
            If de.Key.ToString = pKey Then
                retValue = CType(de.Value, String)
                Exit For
            End If
        Next de
        If retValue = "" Then
            retValue = Interaction.readIniFile("Client", pKey, iniFile)
            If retValue = "" Then
                retValue = GetDefaultParameter(pKey)
                If retValue <> "" Then
                    SetParameter(pKey, retValue)
                End If
            End If
        End If
        Return retValue
    End Function

    Friend Sub SetParameter(pKey As String, pValue As String)
        Interaction.WriteINIFile("Client", pKey, pValue, iniFile)
        If userValues.ContainsKey(pKey) Then
            userValues.Remove(pKey)
        End If
        userValues.Add(pKey, pValue)
    End Sub

End Module
