' -----------------------------------------------------------------------
' WSChamado.asmx
' Autor: Seu Nome
' Data: 05/09/2024
' Descrição: Web Service para os Chamados, com funcionalidade para buscar
' e atualizar chamados e suas relações.
' -----------------------------------------------------------------------

Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data
Imports System.IO ' Para o log em arquivo de texto

<WebService(Namespace:="WSChamado", Name:="WSChamado")>
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
Public Class WSChamado
    Inherits WebService

    ' Instância da classe oBanco
    Dim oBanco As cls_Banco

    ' Diretório e arquivo de log
    Private logFilePath As String = "C:\Temp\Log.txt"

    ' Construtor para inicializar o banco de dados
    Public Sub New()
        Try
            oBanco = New cls_Banco()
            If oBanco Is Nothing Then
                Throw New Exception("Erro ao inicializar o objeto oBanco.")
            End If
        Catch ex As Exception
            ' Log de erro ao inicializar o banco
            EscreveLog("(WSChamado) Erro ao inicializar o banco: " & ex.Message)
            Throw New Exception("Erro ao inicializar o banco: " & ex.Message)
        End Try
    End Sub

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

    ' Verifica se o oBanco foi inicializado corretamente
    Private Sub VerificaOBanco()
        If oBanco Is Nothing Then
            Dim mensagemErro As String = "Erro: oBanco não foi inicializado."
            EscreveLog(mensagemErro)
            Throw New Exception(mensagemErro)
        End If
    End Sub








    ' Função para inserir, atualizar ou consultar chamados
    <WebMethod()>
    Public Function Chamado(ByVal pPConn_Banco As String,
                            ByVal pPakage As String,
                            ByVal pageNumber As Integer,
                            ByVal pageSize As Integer,
                            ByVal idChamado As Integer,
                            ByVal requestNumber As String,
                            ByVal workOrderNumber As String,
                            ByVal estado As String,
                            ByVal comentarios As String,
                            ByVal atribuidoPara As String,
                            ByVal tipoSolicitacao As String,
                            ByVal transactionID As String,
                            ByVal idConsumidor As Integer,
                            ByVal idAtivo As Integer,
                            ByVal idConglomerado As Integer,
                            ByVal userName As String,
                            ByVal userNumber As String,
                            ByVal designationProduct As String,
                            ByVal telecomProvider As String,
                            ByVal framingPlan As String,
                            ByVal migrationDevice As String,
                            ByVal servicePack As String,
                            ByVal newAreaCode As String,
                            ByVal newUserNumber As String,
                            ByVal newTelecomProvider As String,
                            ByVal countryDateForRoaming As String,
                            ByVal managerOrAdm As String,
                            ByVal viewProfile As String,
                            ByVal managerNumber As String,
                            ByVal additionalInformation As String,
                            ByVal name As String,
                            ByVal pRetorno As Boolean) As DataSet
        Try

            VerificaOBanco()

            ' Log de início da operação
            EscreveLog($"(WSChamado) Action: {tipoSolicitacao}, Chamado ID: {idChamado}, pPakage: {pPakage}")

            If String.IsNullOrEmpty(pPConn_Banco) Then Throw New Exception("Erro: pPConn_Banco está vazio.")

            ' Verificações de parâmetros
            If pPakage.Trim().ToLower() <> "busca_todos_dados" Then
                If String.IsNullOrEmpty(tipoSolicitacao) Then Throw New Exception("Erro: action está vazio.")
                If String.IsNullOrEmpty(requestNumber) Then Throw New Exception("Erro: requestNumber está vazio.")
                If String.IsNullOrEmpty(workOrderNumber) Then Throw New Exception("Erro: workOrderNumber está vazio.")
            End If

            ' Inicializa a lista dinâmica para os parâmetros
            Dim parametros As New List(Of SqlClient.SqlParameter)()

            ' Adiciona os parâmetros à lista diretamente
            parametros.Add(New SqlClient.SqlParameter("@pPakage", pPakage))
            parametros.Add(New SqlClient.SqlParameter("@pageNumber", pageNumber))
            parametros.Add(New SqlClient.SqlParameter("@pageSize", pageSize))
            parametros.Add(New SqlClient.SqlParameter("@pId_Chamado", idChamado))
            parametros.Add(New SqlClient.SqlParameter("@requestNumber", requestNumber))
            parametros.Add(New SqlClient.SqlParameter("@workOrderNumber", workOrderNumber))
            parametros.Add(New SqlClient.SqlParameter("@estado", estado))
            parametros.Add(New SqlClient.SqlParameter("@comentarios", comentarios))
            parametros.Add(New SqlClient.SqlParameter("@atribuidoPara", atribuidoPara))
            parametros.Add(New SqlClient.SqlParameter("@tipoSolicitacao", tipoSolicitacao))
            parametros.Add(New SqlClient.SqlParameter("@transactionID", transactionID))
            parametros.Add(New SqlClient.SqlParameter("@idConsumidor", idConsumidor))
            parametros.Add(New SqlClient.SqlParameter("@idAtivo", idAtivo))
            parametros.Add(New SqlClient.SqlParameter("@idConglomerado", idConglomerado))

            ' Dados auxiliares
            parametros.Add(New SqlClient.SqlParameter("@userName", userName))
            parametros.Add(New SqlClient.SqlParameter("@userNumber", userNumber))
            parametros.Add(New SqlClient.SqlParameter("@designationProduct", designationProduct))
            parametros.Add(New SqlClient.SqlParameter("@telecomProvider", telecomProvider))
            parametros.Add(New SqlClient.SqlParameter("@framingPlan", framingPlan))
            parametros.Add(New SqlClient.SqlParameter("@migrationDevice", migrationDevice))
            parametros.Add(New SqlClient.SqlParameter("@servicePack", servicePack))
            parametros.Add(New SqlClient.SqlParameter("@newAreaCode", newAreaCode))
            parametros.Add(New SqlClient.SqlParameter("@newUserNumber", newUserNumber))
            parametros.Add(New SqlClient.SqlParameter("@newTelecomProvider", newTelecomProvider))
            parametros.Add(New SqlClient.SqlParameter("@countryDateForRoaming", countryDateForRoaming))
            parametros.Add(New SqlClient.SqlParameter("@additionalInformation", additionalInformation))
            parametros.Add(New SqlClient.SqlParameter("@name", name))

            ' Verificações para ProfileActions
            If Not String.IsNullOrEmpty(managerOrAdm) Then
                parametros.Add(New SqlClient.SqlParameter("@managerOrAdm", managerOrAdm))
            End If
            If Not String.IsNullOrEmpty(viewProfile) Then
                parametros.Add(New SqlClient.SqlParameter("@viewProfile", viewProfile))
            End If
            If Not String.IsNullOrEmpty(managerNumber) Then
                parametros.Add(New SqlClient.SqlParameter("@managerNumber", managerNumber))
            End If

            ' Converte a lista para array e retorna o resultado da query
            Return oBanco.retorna_Query("dbo.pa_Chamado", parametros.ToArray(), pPConn_Banco)

        Catch ex As Exception
            ' Log do erro detalhado
            EscreveLog("(WSChamado) Erro em Chamado: " & ex.Message & vbCrLf & ex.StackTrace)
            Throw New Exception("Erro em Chamado: " & ex.Message)
        End Try
    End Function






    'Essa função vai chamar a procedure pa_Ativo_Chamado e passar os parâmetros necessários conforme o exemplo de código da função Chamado.
    <WebMethod()>
    Public Function AtivoChamado(ByVal pPConn_Banco As String,
                             ByVal pPakage As String,
                             ByVal pId_Chamado As Integer,
                             ByVal pId_Ativo As Integer,
                             ByVal pNr_Ativo As String,
                             ByVal pNewNr_Ativo As String,
                             ByVal pNewAreaCode As String,
                             ByVal pId_Conglomerado As Integer,
                             ByVal pPlano_Contrato As String,
                             ByVal pComentarios As String,
                             ByVal pRetorno As Boolean) As DataSet
        Try
            ' Log da operação
            EscreveLog($"(WSChamado) pPakage: {pPakage}, Ativo ID: {pId_Ativo}")

            ' Verificação de parâmetros
            If String.IsNullOrEmpty(pPConn_Banco) Then Throw New Exception("Erro: pPConn_Banco está vazio.")
            If String.IsNullOrEmpty(pPakage) Then Throw New Exception("Erro: pPakage está vazio.")

            ' Parâmetros a serem passados para a procedure
            Dim parametros As New List(Of SqlClient.SqlParameter)()
            parametros.Add(New SqlClient.SqlParameter("@pPAKAGE", pPakage))
            parametros.Add(New SqlClient.SqlParameter("@pId_Chamado", pId_Chamado))
            parametros.Add(New SqlClient.SqlParameter("@pId_Ativo", pId_Ativo))
            parametros.Add(New SqlClient.SqlParameter("@pNr_Ativo", pNr_Ativo))
            parametros.Add(New SqlClient.SqlParameter("@pNewNr_Ativo", pNewNr_Ativo))
            parametros.Add(New SqlClient.SqlParameter("@pNewAreaCode", pNewAreaCode))
            parametros.Add(New SqlClient.SqlParameter("@pId_Conglomerado", pId_Conglomerado))
            parametros.Add(New SqlClient.SqlParameter("@pPlano_Contrato", pPlano_Contrato))
            parametros.Add(New SqlClient.SqlParameter("@pComentarios", pComentarios))

            ' Retorno da procedure pa_Ativo_Chamado
            Return oBanco.retorna_Query("dbo.pa_Ativo_Chamado", parametros.ToArray(), pPConn_Banco)

        Catch ex As Exception
            EscreveLog($"(WSChamado) Erro em AtivoChamado: {ex.Message}")
            Throw New Exception($"Erro em AtivoChamado: {ex.Message}")
        End Try
    End Function






    'Essa função vai chamar a procedure pa_Ativo_Relacionamento para gerenciar a alteração de proprietários dos ativos.
    <WebMethod()>
    Public Function AtivoRelacionamento(ByVal pPConn_Banco As String,
                                    ByVal pId_Chamado As Integer,
                                    ByVal pId_Ativo As Integer,
                                    ByVal pUserNumber As Integer,
                                    ByVal pPakage As String,
                                    ByVal pRetorno As Boolean) As DataSet
        Try
            EscreveLog($"(WSChamado) pPakage: {pPakage}, Ativo ID: {pId_Ativo}")

            If String.IsNullOrEmpty(pPConn_Banco) Then Throw New Exception("Erro: pPConn_Banco está vazio.")
            If String.IsNullOrEmpty(pPakage) Then Throw New Exception("Erro: pPakage está vazio.")

            Dim parametros As New List(Of SqlClient.SqlParameter)()
            parametros.Add(New SqlClient.SqlParameter("@pPAKAGE", pPakage))
            parametros.Add(New SqlClient.SqlParameter("@pId_Chamado", pId_Chamado))
            parametros.Add(New SqlClient.SqlParameter("@pId_Ativo", pId_Ativo))
            parametros.Add(New SqlClient.SqlParameter("@pUserNumber", pUserNumber))

            Return oBanco.retorna_Query("dbo.pa_Ativo_Relacionamento", parametros.ToArray(), pPConn_Banco)

        Catch ex As Exception
            EscreveLog($"(WSChamado) Erro em AtivoRelacionamento: {ex.Message}")
            Throw New Exception($"Erro em AtivoRelacionamento: {ex.Message}")
        End Try
    End Function






    'Essa função vai chamar a procedure pa_Operadores_Email para realizar operações de CRUD na tabela Operadores_Email.
    <WebMethod()>
    Public Function ChamadoAuxiliar(ByVal pPConn_Banco As String,
                                ByVal pAcao As String,
                                ByVal id_Conglomerado As Integer,
                                ByVal id_Ativo As Integer,
                                ByVal pEmailDestino As String,
                                ByVal pEmailCopia As String,
                                ByVal id_Mail_Sender As Integer,
                                ByVal pTextoAdicional As String,
                                ByVal pAssuntoEmail As String,
                                ByVal pRetorno As Boolean) As DataSet
        Try
            If String.IsNullOrEmpty(pPConn_Banco) Then Throw New Exception("pPConn_Banco está vazio.")
            If String.IsNullOrEmpty(pAcao) Then Throw New Exception("pAcao está vazio.")

            Dim parametros As New List(Of SqlClient.SqlParameter)()
            parametros.Add(New SqlClient.SqlParameter("@pAcao", pAcao))
            parametros.Add(New SqlClient.SqlParameter("@id_Conglomerado", id_Conglomerado))
            parametros.Add(New SqlClient.SqlParameter("@id_Ativo", id_Ativo))
            parametros.Add(New SqlClient.SqlParameter("@pEmailDestino", pEmailDestino))
            parametros.Add(New SqlClient.SqlParameter("@pEmailCopia", pEmailCopia))
            parametros.Add(New SqlClient.SqlParameter("@id_Mail_Sender", id_Mail_Sender))
            parametros.Add(New SqlClient.SqlParameter("@pTextoAdicional", pTextoAdicional))
            parametros.Add(New SqlClient.SqlParameter("@pAssuntoEmail", pAssuntoEmail))


            Return oBanco.retorna_Query("dbo.pa_ChamadoAuxiliar", parametros.ToArray(), pPConn_Banco)

        Catch ex As Exception
            EscreveLog($"(WSChamado.ChamadoAuxiliar) Erro em OperadoresEmail: {ex.Message}")
            Throw New Exception($"(WSChamado.ChamadoAuxiliar)Erro em OperadoresEmail: {ex.Message}")
        End Try
    End Function

End Class
