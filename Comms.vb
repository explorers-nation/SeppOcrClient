Imports System.Net.Sockets
Imports System.Text
Imports Amazon
Imports Amazon.DynamoDBv2
Imports System.Configuration
Imports Amazon.Runtime
Imports Amazon.DynamoDBv2.Model

Public Enum Transmission
    Authenticated = 1
    UpdateNotRequired = 2
    SystemUpdated = 3
    SeppSystems = 4
    StationUpdated = 5
    ActivityRecorded = 6
    SeppsSystemFaction = 7
    SoftDataUpdated = 8
    UnableToAuthenticate = 9
End Enum

Module Comms
    Private gotSystems As Boolean = False
    Private responceCodes As New Hashtable()
    Private systemFactions As New Hashtable()
    Private sendQueue As New Queue()
    Private sendCmdQueue As New Queue()
    Private sendSoftDataQueue As New Queue()
    Private lastSendTime As DateTime = DateTime.Now
    Private isReceivingData As Boolean = True
    Private authenticated As Boolean = False
    Private dataIsLoaded As Boolean = False
    Private awsClient As AmazonDynamoDBClient

    Private Function Connect() As Boolean
        Dim accessKey = ConfigurationManager.AppSettings("awsAccessKeyId")
        Dim secretKey = ConfigurationManager.AppSettings("awsSecretAccessKey")

        awsClient = BuildClient(accessKey, secretKey)
        authenticated = True
        Return True
    End Function

    ' TODO: Move away from the old TCP base logic
    Friend Function SendUpdate(softData As String) As Boolean
        Try
            sendSoftDataQueue.Enqueue(Transmission.SeppsSystemFaction & ":" & softData)
            Return True
        Catch ex As Exception
            SeppOcrClient.LogOutput("Update failed: " & ex.Message)
            Return False
        End Try
    End Function

    Friend Async Function ProcUpdate() As Task
        If Not authenticated Then
            Try
                Connect()
            Catch ex As Exception
                SeppOcrClient.LogOutput("Connect failed: " & ex.Message)
            End Try
        ElseIf isReceivingData Then
            isReceivingData = False
            Try
                Await ReceiveData()
            Catch ex As Exception
                SeppOcrClient.LogEverywhere("Receive Data failed: " & ex.Message)
            End Try
        Else
            If authenticated Then
                Try
                    Dim sendData As String = ""
                    Dim elapsedTime As TimeSpan = DateTime.Now.Subtract(lastSendTime)
                    Dim elapsedMilliSecs As Double = elapsedTime.TotalMilliseconds

                    If sendCmdQueue.Count > 0 Then
                        sendData = CType(sendCmdQueue.Peek(), String)
                        sendCmdQueue.Dequeue()
                    ElseIf sendSoftDataQueue.Count > 0 Then
                        sendData = CType(sendSoftDataQueue.Peek(), String)
                        sendSoftDataQueue.Dequeue()
                    ElseIf elapsedMilliSecs > 1499 And sendQueue.Count > 0 Then
                        sendData = CType(sendQueue.Peek(), String)
                        sendQueue.Dequeue()
                        lastSendTime = DateTime.Now
                    End If
                    If sendData <> "" Then
                        Await SendDataUpdate(sendData)
                    End If
                Catch ex As Exception
                    SeppOcrClient.LogOutput("SendData failed: " & ex.Message)
                End Try
            End If
        End If
    End Function

    Private Async Function SendDataUpdate(data As String) As Task
        ' 
        If data(0) = CStr(Transmission.SeppsSystemFaction) Then
            Await SendFactionItemToAws(data)
        End If
        '.AppSettings["awsAccessKeyId"]
    End Function

    Private Async Function SendFactionItemToAws(data As String) As Task
        SeppOcrClient.LogOutput("Transmitting Report to AWS:   " & data)

        Dim items = Split(data, ":")

        If items.Length < 6 Then
            SeppOcrClient.LogOutput("Error: bad packet")

            Throw New System.Exception("Bad packet")
        End If

        Dim system = Trim(items(1))
        Dim faction = Trim(items(2))
        Dim state = Trim(items(3))
        Dim influence = Trim(items(4))
        Dim updateType = Trim(items(5))
        Dim entryDate = Trim(items(6))
        Dim commander = SeppOcrClient.CommanderName.Text

        Dim id = "" & system & "-" & faction & "-" & entryDate

        If (String.IsNullOrWhiteSpace(system) Or
        String.IsNullOrWhiteSpace(faction) Or
        String.IsNullOrWhiteSpace(entryDate) Or
        String.IsNullOrWhiteSpace(influence)) Then
            SeppOcrClient.LogOutput("Error: data missing")

            Throw New System.Exception("Data missing")
        End If

        Dim attributes = New Dictionary(Of String, AttributeValue)() From {
            {"id", New AttributeValue() With {
                .S = id
            }},
            {"system", New AttributeValue() With {
                .S = system
            }},
            {"faction", New AttributeValue() With {
                .S = faction
            }},
            {"date", New AttributeValue() With {
                .S = entryDate
            }},
            {"influence", New AttributeValue() With {
                .N = influence
            }},
            {"updateType", New AttributeValue() With {
                .S = updateType
            }}
        }

        If Not String.IsNullOrWhiteSpace(state) Then
            attributes.Add("state", New AttributeValue() With {
                .S = state
            })
        End If

        If Not String.IsNullOrWhiteSpace(commander) Then
            attributes.Add("commander", New AttributeValue() With {
                .S = commander
            })
        End If

        Try
            Dim response = Await awsClient.PutItemAsync(
                tableName:="sepp-factions",
                item:=attributes)

            If response.HttpStatusCode >= 300 Then
                SeppOcrClient.LogEverywhere("FAILED! Couldn't send '" & system & " - " & faction & "' data")
                SeppOcrClient.LogOutput("FAILED! (HTTP CODE = " & response.HttpStatusCode & ")")
            Else
                SeppOcrClient.LogEverywhere("SUCCESS! Sent '" & system & " - " & faction & "' data")
                SeppOcrClient.LogOutput("(HTTP CODE = " & response.HttpStatusCode & ")")
            End If
        Catch ex As Exception
            SeppOcrClient.LogEverywhere("FAILED! Couldn't send '" & system & " - " & faction & "' data")
            SeppOcrClient.LogOutput("Error: " & ex.Message)
        End Try

    End Function
    Private Async Function ReadSystemsFromAws() As Task
        Dim request = New ScanRequest() With {
            .TableName = "sepp-systems"
        }
        Dim response = Await awsClient.ScanAsync(request)

        SeppOcrClient.SelectedSystem.Items.Clear()
        For Each sys In response.Items.OrderBy(Function(system) system("datetime").S)
            SoftData.AddSystem(sys("system").S)
        Next
    End Function

    Private Async Function ReadFactionsFromAws() As Task
        Dim lastWeek = String.Format("{0:yyyy-MM-dd}", Date.UtcNow - New TimeSpan(24 * 7, 0, 0))
        Dim request = New ScanRequest() With {
            .TableName = "sepp-factions",
            .FilterExpression = "#entrydate >= :lastweek",
            .ExpressionAttributeNames = New Dictionary(Of String, String)() From {
                {"#entrydate", "date"}
            },
            .ExpressionAttributeValues = New Dictionary(Of String, AttributeValue)() From {
                {":lastweek", New AttributeValue() With {.S = lastWeek}}
            },
            .Limit = 5000
        }
        SeppOcrClient.LogEverywhere("Requesting factions from AWS. At the moment this sometimes takes a while... :/")
        Dim response = Await awsClient.ScanAsync(request)

        For Each systemName As String In SeppOcrClient.SelectedSystem.Items
            ReadFactionFromResults(response.Items, systemName)
        Next
    End Function
    Private Sub ReadFactionFromResults(factionsData As List(Of Dictionary(Of String, AttributeValue)), systemName As String)
        Dim systemFactions = factionsData.Where(Function(faction) faction("system").S.Equals(systemName))
        SeppOcrClient.LogOutput("Downloading factions from " & systemName)

        If systemFactions.Count > 0 Then
            Dim lastEntry = systemFactions _
                    .OrderBy(Function(faction) faction("date").S) _
                    .Select(Function(faction) faction("date").S) _
                    .Last()

            Dim factionsList = systemFactions _
                .Where(Function(faction) faction("date").S.Equals(lastEntry)) _
                .OrderByDescending(Function(faction) Decimal.Parse(faction("influence").N)) _
                .Select(Function(faction) New Faction() With {
                    .System = faction("system").S,
                    .FactionName = faction("faction").S,
                    .PrevEntryDate = faction("date").S,
                    .PrevCommander = If(faction.ContainsKey("commander"), faction("commander").S, Nothing),
                    .PrevInfluence = If(faction.ContainsKey("influence"), Decimal.Parse(faction("influence").N), Nothing),
                    .PrevState = If(faction.ContainsKey("state"), faction("state").S, Nothing),
                    .EntryDate = If(.PrevEntryDate = SeppOcrClient.EntryDate.Text, .PrevEntryDate, Nothing),
                    .Influence = If(.PrevEntryDate = SeppOcrClient.EntryDate.Text, .PrevInfluence, Nothing),
                    .State = If(.PrevEntryDate = SeppOcrClient.EntryDate.Text, .PrevState, Nothing),
                    .Commander = If(.PrevEntryDate = SeppOcrClient.EntryDate.Text, .Commander, Nothing),
                    .Downloaded = True
                }) _
                .ToList()

            SoftData.AddFactions(systemName, factionsList)
            End If
    End Sub
    Private Async Function AddFactionNameToAws(systemName As String) As Task
        SeppOcrClient.LogOutput("Transmitting Add System Name ('" + systemName + "') to AWS: ")

        Dim utc = DateTime.UtcNow
        Dim entryDateTime = String.Format("{0:yyyy-MM-dd hh:mm:ss}", utc)
        Dim commander = SeppOcrClient.CommanderName.Text

        Try
            Dim attributes = New Dictionary(Of String, AttributeValue)() From {
                {"system", New AttributeValue() With {
                    .S = systemName
                }},
                {"datetime", New AttributeValue() With {
                    .S = entryDateTime
                }}
            }

            If Not String.IsNullOrWhiteSpace(commander) Then
                attributes.Add("commander", New AttributeValue() With {
                .S = commander
            })
            End If

            Dim response = Await awsClient.PutItemAsync(
                tableName:="sepp-systems",
                item:=attributes)

            If response.HttpStatusCode >= 300 Then
                SeppOcrClient.LogEverywhere("FAILED! Couldn't add the new System Name to AWS")
                SeppOcrClient.LogOutput("FAILED! (HTTP CODE = " & response.HttpStatusCode & ")")
            Else
                SeppOcrClient.LogEverywhere("SUCCESS! Added the new System Name to AWS")
                SeppOcrClient.LogOutput("(HTTP CODE = " & response.HttpStatusCode & ")")
            End If
        Catch ex As Exception
            SeppOcrClient.LogEverywhere("FAILED! Couldn't add the new System Name to AWS")
            SeppOcrClient.LogOutput("Error: " & ex.Message)
        End Try
    End Function
    Private Async Function RemoveFactionNameFromAws(systemName As String) As Task
        SeppOcrClient.LogOutput("Transmitting Add System Name ('" + systemName + "') to AWS: ")

        Dim utc = DateTime.UtcNow
        Dim entryDateTime = String.Format("{0:yyyy-MM-dd hh:mm:ss}", utc)
        Dim commander = SeppOcrClient.CommanderName.Text

        Try
            Dim attributes = New Dictionary(Of String, AttributeValue)() From {
                {"system", New AttributeValue() With {
                    .S = systemName
                }}
            }

            Dim response = Await awsClient.DeleteItemAsync(
                tableName:="sepp-systems",
                key:=attributes)

            If response.HttpStatusCode >= 300 Then
                SeppOcrClient.LogEverywhere("FAILED! Couldn't remove the System Name from AWS")
                SeppOcrClient.LogOutput("FAILED! (HTTP CODE = " & response.HttpStatusCode & ")")
            Else
                SeppOcrClient.LogEverywhere("SUCCESS! Removed the System Name from AWS")
                SeppOcrClient.LogOutput("(HTTP CODE = " & response.HttpStatusCode & ")")
            End If
        Catch ex As Exception
            SeppOcrClient.LogEverywhere("FAILED! Couldn't remove the System Name from AWS")
            SeppOcrClient.LogOutput("Error: " & ex.Message)
        End Try
    End Function

    Private Function BuildClient(accessKey As String, secretKey As String) As AmazonDynamoDBClient
        Dim credentials = New BasicAWSCredentials(accessKey:=accessKey, secretKey:=secretKey)
        Dim config = New AmazonDynamoDBConfig()
        config.RegionEndpoint = RegionEndpoint.USEast1
        Return New AmazonDynamoDBClient(credentials, config)
    End Function

    Private Async Function ReceiveData() As Task
        If (Not dataIsLoaded) Then
            Files.IdLastJournal()
            Files.TailJournal()
            SeppOcrClient.LogEverywhere("Downloading Systems...")
            Await ReadSystemsFromAws()
            Await ReadFactionsFromAws()
            SeppOcrClient.LogEverywhere("Systems are ready!")
            SeppOcrClient.ShowBgsTools()
            dataIsLoaded = True
        End If
    End Function

    Private Function GetResponseDesc(rCode As String) As String
        Dim retValue As String = "Unknown Responce"
        For Each de As DictionaryEntry In responceCodes
            If CType(de.Key, String) = Left(rCode, 1) Then
                retValue = CType(de.Value, String)
                Exit For
            End If
        Next de
        Return retValue
    End Function
    Friend Sub InitCommsCodes()
        responceCodes.Add("9", "Unable to Authenticate")
        responceCodes.Add("1", "Authenticated")
        responceCodes.Add("2", "Update not required")
        responceCodes.Add("3", "System Updated")
        responceCodes.Add("4", "Sepps Systems")
        responceCodes.Add("5", "Station Updated")
        responceCodes.Add("6", "Activity Recorded")
        responceCodes.Add("7", "Sepps System Faction")
        responceCodes.Add("8", "Soft Data Updated")
        responceCodes.Add("-", "Unknown command")
    End Sub

    Public Async Function AddSystemToRoster(systemName As String) As Task(Of Boolean)
        Try
            Await AddFactionNameToAws(systemName)
            SoftData.AddSystem(systemName)
        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function

    Public Async Function RemoveSystemFromRoster(systemName As String) As Task(Of Boolean)
        Try
            Await RemoveFactionNameFromAws(systemName)
            SoftData.RemoveSystem(systemName)
        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function

End Module