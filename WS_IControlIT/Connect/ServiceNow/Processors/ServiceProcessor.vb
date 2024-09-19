' -----------------------------------------------------------------------
' ServiceProcessor.vb
' Autor: Seu Nome
' Data: 05/09/2024
' Descrição:Faz o processamento dos dados
' -----------------------------------------------------------------------
Imports WS_IControlIT.Connect.ServiceNow.Models
Imports System.Web.Services.Protocols
Imports System.IO

Namespace Connect.ServiceNow.Processors
    Public Class ServiceProcessor
        ' Instância do WSChamado para acessar as funções do WebService
        Private WSChamado As New WSChamado()

        ' Diretório e arquivo de log
        Private logFilePath As String = "C:\Temp\Log.txt"

        ' Método para garantir que o arquivo de log pode ser criado ou acessado
        Private Sub InicializaLog()
            Try
                ' Verifica se a pasta existe, senão cria
                Dim logDirectory As String = Path.GetDirectoryName(logFilePath)
                If Not Directory.Exists(logDirectory) Then
                    Directory.CreateDirectory(logDirectory)
                End If

                ' Verifica se o arquivo existe, senão cria
                If Not File.Exists(logFilePath) Then
                    File.Create(logFilePath).Dispose()
                End If
            Catch ex As Exception
                ' Caso ocorra um erro ao criar diretório ou arquivo de log
                Throw New Exception("Erro ao inicializar o arquivo de log: " & ex.Message)
            End Try
        End Sub

        ' Método para escrever log em arquivo de texto
        Private Sub EscreveLog(ByVal mensagem As String)
            Try
                ' Inicializa o log (cria pasta/arquivo se necessário)
                InicializaLog()

                ' Escreve a mensagem no arquivo de log
                Using sw As StreamWriter = New StreamWriter(logFilePath, True)
                    sw.WriteLine($"{DateTime.Now}: {mensagem}")
                End Using
            Catch ex As Exception
                ' Se falhar ao escrever o log, lida com o erro
                Throw New Exception("Erro ao gravar log: " & ex.Message)
            End Try
        End Sub

        ' Método para processar MobileActions
        Public Sub ProcessMobileActions(ByVal request As MobileActionsRequestModel)
            Try
                ' Log da inicialização do processo
                EscreveLog($"(ServiceProcessor)Processando MobileActions para RequestNumber: {request.RequestNumber}")

                ' Prepara a conexão com o banco de dados
                Dim pPConn_Banco As String = ConfigurationManager.AppSettings("VALE")

                ' Parâmetros da chamada para o WSChamado (insere ou atualiza chamado)
                Dim pPakage As String = "insere_chamado"
                Dim pId_Chamado As Integer = 0 ' ID atual ou 0 caso seja inserção
                Dim pRetorno As Boolean = True

                ' Chama o método Chamado do WSChamado
                EscreveLog("(ServiceProcessor)Chamando WSChamado.Chamado para MobileActions com os seguintes parâmetros:" &
                           $" RequestNumber: {request.RequestNumber}, WorkOrderNumber: {request.WorkOrderNumber}, Action: {request.Action}")

                ' Alinhando a chamada do método Chamado com os parâmetros atualizados
                Dim result As DataSet = WSChamado.Chamado(pPConn_Banco,
                                                          pPakage,
                                                          Nothing,
                                                          Nothing,
                                                          pId_Chamado,
                                                          request.RequestNumber,
                                                          request.WorkOrderNumber,
                                                          "Pendente", ' Estado inicial
                                                          Nothing, ' Comentários podem ser nulos
                                                          "MobileActions", ' Atribuído para
                                                          request.Action,
                                                          request.TransactionID,
                                                          Nothing, ' Id_Consumidor
                                                          Nothing, ' Id_Ativo
                                                          Nothing, ' Id_Conglomerado
                                                          request.UserName,
                                                          request.UserNumber,
                                                          request.DesignationProduct,
                                                          request.TelecomProvider,
                                                          request.FramingPlan,
                                                          request.MigrationDevice,
                                                          request.ServicePack,
                                                          request.NewAreaCode,
                                                          request.NewUserNumber,
                                                          request.NewTelecomProvider,
                                                          request.CountryDateForRoaming,
                                                          Nothing, ' ManagerOrAdm
                                                          Nothing, ' ViewProfile
                                                          Nothing, ' ManagerNumber
                                                          Nothing, ' AdditionalInformation
                                                          Nothing, ' Name
                                                          pRetorno)

                ' Log do resultado da chamada
                If result IsNot Nothing Then
                    EscreveLog("(ServiceProcessor) WSChamado.Chamado executado com sucesso.")
                Else
                    EscreveLog("(ServiceProcessor)  Nenhum dado retornado.")
                End If

            Catch ex As SoapException
                ' Log de exceção SOAP
                EscreveLog($"(ServiceProcessor) Erro SOAP ao processar MobileActions: {ex.Message}")
                Throw New Exception($"(ServiceProcessor) Erro SOAP ao processar MobileActions: {ex.Message}")
            Catch ex As Exception
                ' Log de exceção genérica
                EscreveLog($"(ServiceProcessor) Erro ao processar MobileActions: {ex.Message}")
                Throw New Exception($"(ServiceProcessor) Erro ao processar MobileActions: {ex.Message}")
            End Try
        End Sub


        ' Método para processar ProfileActions
        Public Sub ProcessProfileActions(ByVal request As ProfileActionsRequestModel)
            Try
                ' Log da inicialização do processo
                EscreveLog($"(ServiceProcessor)Processando ProfileActions para RequestNumber: {request.RequestNumber}")

                ' Prepara a conexão com o banco de dados
                Dim pPConn_Banco As String = ConfigurationManager.AppSettings("VALE")

                ' Parâmetros da chamada para o WSChamado (insere ou atualiza chamado)
                Dim pPakage As String = "insere_chamado"
                Dim pId_Chamado As Integer = 0 ' ID atual ou 0 caso seja inserção
                Dim pRetorno As Boolean = True

                ' Chama o método Chamado do WSChamado
                EscreveLog("(ServiceProcessor)Chamando WSChamado.Chamado para ProfileActions com os seguintes parâmetros:" &
                           $" RequestNumber: {request.RequestNumber}, WorkOrderNumber: {request.WorkOrderNumber}, Action: {request.Action}")

                ' Alinhando a chamada do método Chamado com os parâmetros atualizados
                Dim result As DataSet = WSChamado.Chamado(pPConn_Banco,
                                                          pPakage,
                                                          Nothing,
                                                          Nothing,
                                                          pId_Chamado,
                                                          request.RequestNumber,
                                                          request.WorkOrderNumber,
                                                          "Pendente", ' Estado inicial
                                                          Nothing, ' Comentários podem ser nulos
                                                          "ProfileActions", ' Atribuído para
                                                          request.Action,
                                                          request.TransactionID,
                                                          Nothing, ' Id_Consumidor
                                                          Nothing, ' Id_Ativo
                                                          Nothing, ' Id_Conglomerado
                                                          request.UserName,
                                                          request.UserNumber,
                                                          Nothing, ' DesignationProduct
                                                          Nothing, ' TelecomProvider
                                                          Nothing, ' FramingPlan
                                                          Nothing, ' MigrationDevice
                                                          Nothing, ' ServicePack
                                                          Nothing, ' NewAreaCode
                                                          Nothing, ' NewUserNumber
                                                          Nothing, ' NewTelecomProvider
                                                          Nothing, ' CountryDateForRoaming
                                                          request.ManagerOrAdm,
                                                          request.ViewProfile,
                                                          request.ManagerNumber,
                                                          Nothing, ' AdditionalInformation
                                                          Nothing, ' Name
                                                          pRetorno)

                ' Log do resultado da chamada
                If result IsNot Nothing Then
                    EscreveLog("(ServiceProcessor) WSChamado.Chamado executado com sucesso.")
                Else
                    EscreveLog("(ServiceProcessor) Nenhum dado retornado.")
                End If

            Catch ex As SoapException
                ' Log de exceção SOAP
                EscreveLog($"(ServiceProcessor) Erro SOAP ao processar ProfileActions: {ex.Message}")
                Throw New Exception($"(ServiceProcessor) Erro SOAP ao processar ProfileActions: {ex.Message}")
            Catch ex As Exception
                ' Log de exceção genérica
                EscreveLog($"(ServiceProcessor) Erro ao processar ProfileActions: {ex.Message}")
                Throw New Exception($"(ServiceProcessor) Erro ao processar ProfileActions: {ex.Message}")
            End Try
        End Sub









        '' Método para processar ProfileActions
        'Public Sub ProcessProfileActions(ByVal request As ProfileActionsRequestModel)
        '    Try
        '        ' Log da inicialização do processo
        '        EscreveLog($"(ServiceProcessor)Processando ProfileActions para RequestNumber: {request.RequestNumber}")

        '        ' Prepara a conexão com o banco de dados
        '        Dim pPConn_Banco As String = ConfigurationManager.AppSettings("VALE")

        '        ' Parâmetros da chamada para o WSChamado (insere ou atualiza chamado)
        '        Dim pPakage As String = "insere_chamado"
        '        Dim pId_Chamado As Integer = 0 ' ID atual ou 0 caso seja inserção
        '        Dim pRetorno As Boolean = True

        '        ' Chama o método Chamado do WSChamado
        '        EscreveLog("(ServiceProcessor) Chamando WSChamado.Chamado para ProfileActions com os seguintes parâmetros:" &
        '                   $" RequestNumber: {request.RequestNumber}, WorkOrderNumber: {request.WorkOrderNumber}, Action: {request.Action}")

        '        ' Alinhando a chamada do método Chamado com os parâmetros atualizados
        '        Dim result As DataSet = WSChamado.Chamado(pPConn_Banco, ' Conexão com o banco de dados
        '                                     "busca_todos_dados",  ' Nome do pacote/procedimento
        '                                     Nothing,              ' idChamado (0 se não houver ID de chamado)
        '                                     Nothing,        ' requestNumber
        '                                     Nothing,        ' workOrderNumber
        '                                     Nothing,        ' estado
        '                                     Nothing,        ' comentarios
        '                                     Nothing,        ' atribuidoPara
        '                                     Nothing,        ' tipoSolicitacao
        '                                     Nothing,        ' transactionID
        '                                     Nothing,              ' idConsumidor (0 se não houver ID do consumidor)
        '                                     Nothing,              ' idAtivo (0 se não houver ID do ativo)
        '                                     Nothing,              ' idConglomerado (0 se não houver ID do conglomerado)
        '                                     Nothing,        ' userName
        '                                     Nothing,        ' userNumber
        '                                     Nothing,        ' designationProduct
        '                                     Nothing,        ' telecomProvider
        '                                     Nothing,        ' framingPlan
        '                                     Nothing,        ' migrationDevice
        '                                     Nothing,        ' servicePack
        '                                     Nothing,        ' newAreaCode
        '                                     Nothing,        ' newUserNumber
        '                                     Nothing,        ' newTelecomProvider
        '                                     Nothing,        ' countryDateForRoaming
        '                                     Nothing,        ' managerOrAdm
        '                                     Nothing,        ' viewProfile
        '                                     Nothing,        ' managerNumber
        '                                     Nothing,        ' additionalInformation
        '                                     Nothing,        ' name
        '                                     Nothing,       ' action
        '                                     True)           ' Valor de retorno

        '        ' Verificar se o DataSet não é Nothing e tem pelo menos uma tabela com dados
        '        If result IsNot Nothing AndAlso result.Tables.Count > 0 Then
        '            EscreveLog("(ServiceProcessor)ProcessProfileActions - WSChamado.Chamado executado com sucesso. Número de tabelas: " & result.Tables.Count)

        '            ' Iterar sobre as tabelas no DataSet
        '            For Each table As DataTable In result.Tables
        '                EscreveLog("Tabela: " & table.TableName & " - Número de linhas: " & table.Rows.Count)

        '                ' Verificar se a tabela tem linhas
        '                If table.Rows.Count > 0 Then
        '                    ' Iterar sobre as linhas da tabela
        '                    For Each row As DataRow In table.Rows
        '                        Dim rowLog As String = "Linha: "

        '                        ' Iterar sobre as colunas da linha
        '                        For Each column As DataColumn In table.Columns
        '                            ' Concatenar os valores das colunas e seus respectivos nomes
        '                            rowLog &= column.ColumnName & ": " & row(column).ToString() & "; "
        '                        Next

        '                        ' Escrever o log de cada linha
        '                        EscreveLog(rowLog)
        '                    Next
        '                Else
        '                    EscreveLog("A tabela " & table.TableName & " não contém linhas.")
        '                End If
        '            Next
        '        Else
        '            EscreveLog("(ServiceProcessor) ProcessProfileActions - Nenhum dado retornado.")
        '        End If


        '    Catch ex As SoapException
        '        ' Log de exceção SOAP
        '        EscreveLog($"(ServiceProcessor)Erro SOAP ao processar ProfileActions: {ex.Message}")
        '        Throw New Exception($"Erro SOAP ao processar ProfileActions: {ex.Message}")
        '    Catch ex As Exception
        '        ' Log de exceção genérica
        '        EscreveLog($"(ServiceProcessor)Erro ao processar ProfileActions: {ex.Message}")
        '        Throw New Exception($"Erro ao processar ProfileActions: {ex.Message}")
        '    End Try
        'End Sub
    End Class
End Namespace
