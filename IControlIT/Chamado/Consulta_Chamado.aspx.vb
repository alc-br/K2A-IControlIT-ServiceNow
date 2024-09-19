Imports System.IO
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Web.Script.Serialization
Imports System.Web.Services
Imports System.Text
Imports iTextSharp.text.pdf.codec.wmf
Imports Microsoft.Extensions.Logging
Public Class Consulta_Chamado
    Inherits System.Web.UI.Page
    Dim WS_Chamado As New WS_GUA_Chamado.WSChamado
    Dim vdataset As Data.DataSet

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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Dim itemsPerPage As Integer = Convert.ToInt32(ddlItemsPerPage.SelectedValue)

            Dim connBancoValue = Session("Conn_Banco")

            If String.IsNullOrEmpty(connBancoValue) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('A string de conexão está vazia.');", True)
            Else
                BindChamados(1, 10)
                BindEmpresaContratante()
            End If
        End If
    End Sub

    Private Sub BindChamados(ByVal pageNumber As Integer, ByVal pageSize As Integer)
        Try
            ' Inicialização da conexão com o banco de dados
            Dim pPConn_Banco As String = Session("Conn_Banco")
            If String.IsNullOrEmpty(pPConn_Banco) Then
                Throw New InvalidOperationException("A conexão com o banco de dados não foi encontrada.")
            End If

            ' Chamada inicial para buscar dados do Chamado
            Dim vdataset As DataSet = WS_Chamado.Chamado(pPConn_Banco, "busca_todos_dados", pageNumber, pageSize, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True)

            ' Verifica se o DataSet contém informações
            If vdataset IsNot Nothing AndAlso vdataset.Tables.Count > 0 Then
                ' Atribui o DataSet diretamente ao rptChamados
                rptChamados.DataSource = vdataset.Tables(0)
                rptChamados.DataBind()
            Else
                rptChamados.DataSource = Nothing
                rptChamados.DataBind()
            End If
        Catch ex As Exception
            EscreveLog("(Consulta_Chamado.aspx.vb)Erro: " & ex.Message & vbCrLf & ex.StackTrace)
        End Try
    End Sub

    Private Property CurrentPage As Integer
        Get
            Return If(ViewState("CurrentPage") Is Nothing, 1, Convert.ToInt32(ViewState("CurrentPage")))
        End Get
        Set(ByVal value As Integer)
            ViewState("CurrentPage") = value
        End Set
    End Property

    Protected Sub BtnPreviousPage_Click(sender As Object, e As EventArgs)
        Dim itemsPerPage As Integer = Convert.ToInt32(ddlItemsPerPage.SelectedValue)

        If CurrentPage > 1 Then
            CurrentPage -= 1
            BindChamados(CurrentPage, itemsPerPage)
            lblPageNumber.Text = CurrentPage.ToString()
        End If
    End Sub

    Protected Sub BtnNextPage_Click(sender As Object, e As EventArgs)

        Dim itemsPerPage As Integer = Convert.ToInt32(ddlItemsPerPage.SelectedValue)

        CurrentPage += 1
        BindChamados(CurrentPage, itemsPerPage)
        lblPageNumber.Text = CurrentPage.ToString()

    End Sub


    Protected Sub BtnExecutar_Click(sender As Object, e As EventArgs)

        Dim itemsPerPage As Integer = Convert.ToInt32(ddlItemsPerPage.SelectedValue)

        Try
            ExecutarAcaoChamado()
            RetornoAPI()
            'EnviarEmailChamado()
            BindChamados(CurrentPage, itemsPerPage)

        Catch ex As Exception
            EscreveLog("(Consulta_Chamado.BtnExecutar_Click) Erro ao executar a ação: " & ex.Message)
        End Try

    End Sub

    Private Async Sub RetornoAPI()

        ' Definir o URL da API
        Dim apiUrl As String = "https://valedev.service-now.com/api/115628/integrationframeworkworkapi/sistel/updaterequest"

        ' Criar um objeto HttpClient
        Using client As New HttpClient()

            ' Lendo usuário e senha do web.config
            Dim usuarioSistel As String = ConfigurationManager.AppSettings("usuario_sistel")
            Dim senhaSistel As String = ConfigurationManager.AppSettings("senha_sistel")

            ' Montando as credenciais em Base64
            Dim byteArray As Byte() = Encoding.ASCII.GetBytes($"{usuarioSistel}:{senhaSistel}")
            client.DefaultRequestHeaders.Authorization = New AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray))

            ' Definir o Content-Type
            client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))

            ' Montar os dados do corpo da requisição
            Dim estadoChamado As String = If(hfEstado.Value.ToLower() = "concluído", "ENCERRADA", "PENDENTE")
            Dim userNumber As Integer = If(Not String.IsNullOrEmpty(hfNewUserNumber.Value), Convert.ToInt32(hfNewUserNumber.Value), Convert.ToInt32(hfUserNumber.Value))

            Dim requestData As New With {
            .Action = hfTipoSolicitacao.Value,
            .RequestNumber = hfRequestNumber.Value,
            .WorkOrderNumber = hfWorkOrderNumber.Value,
            .Registration = userNumber.ToString(),
            .Status = estadoChamado,
            .Description = hfComentarios.Value,
            .assigned_to = "a1f183fd1b6de4500b4e85dde54bcb10"
        }

            ' Serializar os dados para JSON
            Dim jsonRequestBody As String = Newtonsoft.Json.JsonConvert.SerializeObject(requestData)
            Dim content As New StringContent(jsonRequestBody, Encoding.UTF8, "application/json")

            Try
                ' Fazer a requisição PUT
                Dim response As HttpResponseMessage = Await client.PutAsync(apiUrl, content)

                ' Verificar se a requisição foi bem-sucedida
                If response.IsSuccessStatusCode Then
                    Dim responseData As String = Await response.Content.ReadAsStringAsync()
                    ' Processar a resposta da API aqui (opcional)
                    EscreveLog("Requisição para a API realizada com sucesso.")
                Else
                    ' Tratar erro da requisição
                    EscreveLog($"Erro na requisição: {response.StatusCode}. Mensagem: {Await response.Content.ReadAsStringAsync()}")
                End If

            Catch ex As Exception
                ' Capturar exceções relacionadas à requisição
                EscreveLog("Erro ao chamar a API: " & ex.Message)
            End Try

        End Using

    End Sub

    ' Função que executa a ação baseada no tipo de solicitação
    Private Sub ExecutarAcaoChamado()
        Const c_acao_alterar_proprietario As String = "alterar-proprietario"
        Try
            Dim tipoSolicitacao As String = hfTipoSolicitacao.Value.ToLower().Replace(" ", "-").Replace("/", "-")

            Dim idChamado As Integer = Convert.ToInt32(hfIdChamado.Value)
            Dim idAtivo As Integer = Convert.ToInt32(hfIdAtivo.Value)
            Dim userNumber As Integer = Convert.ToInt32(hfUserNumber.Value)
            Dim idConglomerado As Integer = Convert.ToInt32(hfIdConglomerado.Value)

            Dim nrAtivo As String = hfUserNumber.Value
            Dim newNrAtivo As String = hfNewUserNumber.Value
            Dim newAreaCode As String = hfNewAreaCode.Value
            Dim newPlanoContrato As String = hfNewPlanoContrato.Value
            Dim comentarios As String = hfComentarios.Value


            ' Chama a função que controla o tipo de solicitação
            If tipoSolicitacao = c_acao_alterar_proprietario Then
                WS_Chamado.AtivoRelacionamento(Session("Conn_Banco"), idChamado, idAtivo, userNumber, tipoSolicitacao, True)
            Else
                WS_Chamado.AtivoChamado(Session("Conn_Banco"), tipoSolicitacao, idChamado, idAtivo, nrAtivo, newNrAtivo, newAreaCode, idConglomerado, newPlanoContrato, comentarios, True)
            End If

        Catch ex As Exception
            ' Em caso de erro, registra no log
            EscreveLog($"(Consulta_Chamado.ExecutarAcaoChamado) Erro ao processar o chamado: {ex.Message}")
            Throw
        End Try
    End Sub

    Private Sub EnviarEmailChamado()

        ' Recuperando os valores da página
        Dim idChamado As String = hfIdChamado.Value
        Dim requestNumber As String = hfRequestNumber.Value
        Dim workOrderNumber As String = hfWorkOrderNumber.Value
        Dim estado As String = hfEstado.Value
        Dim nomeUsuario As String = hfUserName.Value
        Dim idTransacao As String = hfIdConsumidor.Value
        Dim tipoSolicitacao As String = hfTipoSolicitacao.Value
        Dim planoAtual As String = hfNewPlanoContrato.Value
        Dim comentarios As String = hfComentarios.Value
        Dim camposCondicionais As String = camposCondicionaisContainer.InnerHtml

        ' Recuperando as informações adicionais da página
        Dim empresaContratanteSelecionada As String = empresaContratante.SelectedValue
        Dim faturaAgrupadoraSelecionada As String = faturaDropdown.Value
        Dim corpoDoEmail As String = corpoEmail.Value
        Dim emailsSelecionados As String = selectedEmails.InnerText ' Emails separados por vírgula
        Dim emailRespRegional As String = emailResponsavelRegional.Value

        Dim pTextoAdicional As String

        If String.IsNullOrEmpty(idChamado) Then idChamado = "N/A"
        If String.IsNullOrEmpty(requestNumber) Then requestNumber = "N/A"

        If Not String.IsNullOrEmpty(corpoDoEmail) Then
            pTextoAdicional = $"<p>{corpoDoEmail}</p><br>" &
                              "<p><strong>Dados do Chamado</strong></p><br>"
        Else
            pTextoAdicional = "<p><strong>Dados do Chamado</strong></p><br>"
        End If

        pTextoAdicional &= "<p><strong>Id Chamado:</strong> " & idChamado & "</p>" &
                          "<p><strong>Request Number:</strong> " & requestNumber & "</p>" &
                          "<p><strong>Work Order Number:</strong> " & workOrderNumber & "</p>" &
                          "<p><strong>Estado:</strong> " & estado & "</p>" &
                          "<p><strong>Nome do usuário:</strong> " & nomeUsuario & "</p>" &
                          "<p><strong>ID da transação:</strong> " & idTransacao & "</p>" &
                          "<p><strong>Tipo de Solicitação:</strong> " & tipoSolicitacao & "</p>" &
                          "<p><strong>Plano atual:</strong> " & planoAtual & "</p>"

        ' Verificando se existem comentários para adicionar
        If Not String.IsNullOrEmpty(comentarios) Then
            pTextoAdicional &= "<p><strong>Comentários:</strong> " & comentarios & "</p>"
        End If

        If Not String.IsNullOrEmpty(camposCondicionais) Then
            pTextoAdicional &= "<br>" & camposCondicionais
        End If

        ' Adicionando informações da empresa, fatura, corpo do email e emails selecionados
        pTextoAdicional &= "<br><p><strong>Mais Detalhes</strong></p><br>" &
                           "<p><strong>Empresa Contratante:</strong> " & empresaContratanteSelecionada & "</p>" &
                           "<p><strong>Fatura Agrupadora:</strong> " & faturaAgrupadoraSelecionada & "</p>" &
                           "<p><strong>Corpo do Email:</strong> " & corpoDoEmail & "</p>" &
                           "<p><strong>Emails Selecionados:</strong> " & emailsSelecionados & "</p>"

        Dim pAssuntoEmail As String = "[VALE] " & tipoSolicitacao

        ' Aqui você chama a função para enviar o e-mail com o HTML gerado
        AgendarEnvioEmail(emailsSelecionados, emailRespRegional, pTextoAdicional, pAssuntoEmail)

    End Sub



    Public Function GetBadgeClass(estado As Object) As String
        If estado IsNot Nothing Then
            If estado.ToString() = "Pendente" Then
                Return "azul"
            ElseIf estado.ToString() = "Concluído" Then
                Return "verde"
            End If
        End If
        Return "azul" ' Classe padrão para outros estados ou nulo
    End Function

    ' Função para buscar e-mails da operadora por id_Conglomerado
    <WebMethod()>
    Public Shared Function BuscarEmailsOperadora(ByVal idConglomerado As Integer) As String
        Try
            ' Chamar o método Operadora do WebService
            Dim WS_Chamado As New WS_GUA_Chamado.WSChamado
            Dim pPConn_Banco As String = HttpContext.Current.Session("Conn_Banco")
            Dim ds As DataSet = WS_Chamado.ChamadoAuxiliar(pPConn_Banco, "buscar_emails_operadora", idConglomerado, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True)

            ' Verifica se o DataSet contém resultados
            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                ' Serializa a lista de e-mails para o formato JSON
                Dim emailList As New List(Of String)
                For Each row As DataRow In ds.Tables(0).Rows
                    emailList.Add(row("nm_Email").ToString())
                Next
                Dim jsSerializer As New JavaScriptSerializer()
                Return jsSerializer.Serialize(emailList)
            Else
                Return "[]"
            End If
        Catch ex As Exception
            Return "[]"
        End Try
    End Function

    ' Função para buscar e-mails da operadora por id_Conglomerado
    <WebMethod()>
    Public Shared Function BuscarFaturaAgrupadora(ByVal idConglomerado As Integer) As String
        Try
            ' Chamar o método Operadora do WebService
            Dim WS_Chamado As New WS_GUA_Chamado.WSChamado
            Dim pPConn_Banco As String = HttpContext.Current.Session("Conn_Banco")
            Dim ds As DataSet = WS_Chamado.ChamadoAuxiliar(pPConn_Banco, "buscar_fatura_agrupadora", idConglomerado, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True)

            ' Verifica se o DataSet contém resultados
            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                ' Serializa a lista de e-mails para o formato JSON
                Dim emailList As New List(Of String)
                For Each row As DataRow In ds.Tables(0).Rows
                    emailList.Add(row("Nr_Plano_Conta").ToString())
                Next
                Dim jsSerializer As New JavaScriptSerializer()
                Return jsSerializer.Serialize(emailList)
            Else
                Return "[]"
            End If
        Catch ex As Exception
            Return "[]"
        End Try
    End Function

    Protected Sub BindEmpresaContratante()
        Try
            ' Chamar o método Operadora do WebService
            Dim WS_Chamado As New WS_GUA_Chamado.WSChamado
            Dim pPConn_Banco As String = HttpContext.Current.Session("Conn_Banco")
            Dim ds As DataSet = WS_Chamado.ChamadoAuxiliar(pPConn_Banco, "buscar_nomes_filiais", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True)

            ' Verifica se o DataSet contém resultados
            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                ' Limpa as opções existentes no DropDownList (empresaContratante)
                empresaContratante.Items.Clear()

                ' Adiciona a opção padrão
                empresaContratante.Items.Add(New ListItem("Selecione a Empresa", ""))

                ' Preenche o DropDownList com os dados do DataSet
                For Each row As DataRow In ds.Tables(0).Rows
                    Dim nomeFilial As String = row("Nm_Filial").ToString()
                    empresaContratante.Items.Add(New ListItem(nomeFilial, nomeFilial))
                Next
            Else
                ' Adiciona uma mensagem padrão se não houver resultados
                empresaContratante.Items.Clear()
                empresaContratante.Items.Add(New ListItem("Nenhuma empresa disponível", ""))
            End If
        Catch ex As Exception
            ' Tratamento de exceções
            empresaContratante.Items.Clear()
            empresaContratante.Items.Add(New ListItem("Erro ao carregar as empresas", ""))
        End Try
    End Sub

    Protected Sub ddlItemsPerPage_SelectedIndexChanged(sender As Object, e As EventArgs)
        ' Recupera o valor selecionado no dropdown
        Dim itemsPerPage As Integer = Convert.ToInt32(ddlItemsPerPage.SelectedValue)

        ' Atualiza a páginação e a exibição dos chamados com o novo número de itens por página
        ' Aqui, você deve chamar seu método de exibição com o novo valor de itemsPerPage
        BindChamados(CurrentPage, itemsPerPage)
    End Sub

    ' Função para agendar envio de email
    Private Function AgendarEnvioEmail(ByVal pEmailDestino As String, ByVal pEmailCopia As String, ByVal pTextoAdicional As String, ByVal pAssuntoEmail As String) As String
        Try
            Dim id_Mail_Sender As Integer

            Dim WS_Chamado As New WS_GUA_Chamado.WSChamado
            Dim pPConn_Banco As String = HttpContext.Current.Session("Conn_Banco")

            ' Busca o id_Mail_Sender baseado no assunto
            Dim ds As DataSet = WS_Chamado.ChamadoAuxiliar(pPConn_Banco, "buscar_mail_sender", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, pAssuntoEmail, True)

            id_Mail_Sender = Convert.ToInt32(ds.Tables(0).Rows(0)("Id_Mail_Sender"))

            ' Agenda disparo do email
            WS_Chamado.ChamadoAuxiliar(pPConn_Banco, "agendar_disparo_email", Nothing, Nothing, pEmailDestino, pEmailCopia, id_Mail_Sender, pTextoAdicional, Nothing, True)

            Return "Email agendado com sucesso."

        Catch ex As Exception
            Return "Erro ao agendar o email: " & ex.Message
        End Try

    End Function


End Class