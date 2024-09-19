' -----------------------------------------------------------------------
' Handler.asmx.vb
' -----------------------------------------------------------------------

Imports System.Web.Services
Imports System.Xml
Imports System.IO
Imports System.Web.Services.Description
Imports System.Web.Services.Protocols
Imports WS_IControlIT.Connect.ServiceNow.Models
Imports System.Text
Imports WS_IControlIT.Connect.ServiceNow.Processors

Namespace Connect.ServiceNow.Handlers

    <WebService(Namespace:="https://icontrolit.com.br/Connect/ServiceNow/Handlers/Handler.asmx")>
    <WebServiceBinding()>
    Public Class Handler
        Inherits WebService

        ' Caminho para o arquivo de log
        Private logFilePath As String = "C:\Temp\LogActions.txt"

        ' Método para escrever o log
        Private Sub EscreveLog(ByVal mensagem As String)
            Try
                Using sw As StreamWriter = New StreamWriter(logFilePath, True)
                    sw.WriteLine($"{DateTime.Now}: {mensagem}")
                End Using
            Catch ex As Exception
                Throw New Exception("Erro ao gravar log: " & ex.Message)
            End Try
        End Sub

        ' Método para decodificar credenciais Basic Auth
        Private Function DecodificarCredenciais(ByVal credenciaisBase64 As String) As String()
            ' Decodificar as credenciais base64
            Dim credenciais As String = Encoding.UTF8.GetString(Convert.FromBase64String(credenciaisBase64))
            ' Retornar o usuário e a senha
            Return credenciais.Split(":"c)
        End Function

        ' Método principal para processar as requisições
        <WebMethod()>
        Public Function createInstance() As String
            Try
                EscreveLog("(Handler) Iniciando createInstance.")

                ' Obter o cabeçalho Authorization da requisição
                Dim authorizationHeader As String = Context.Request.Headers("Authorization")
                If String.IsNullOrEmpty(authorizationHeader) OrElse Not authorizationHeader.StartsWith("Basic ") Then
                    EscreveLog("(Handler) Cabeçalho Authorization não encontrado ou não é Basic.")
                    Context.Response.StatusCode = 401 ' Unauthorized
                    Return "Erro: Cabeçalho Authorization ausente ou inválido."
                End If

                ' Decodificar as credenciais
                Dim credenciaisBase64 As String = authorizationHeader.Substring(6) ' Remover "Basic "
                Dim credenciais() As String = DecodificarCredenciais(credenciaisBase64)

                ' Obter o usuário e senha do web.config
                Dim authUser As String = ConfigurationManager.AppSettings("AuthUserSoap")
                Dim authPass As String = ConfigurationManager.AppSettings("AuthPassSoap")

                ' Verificar as credenciais
                If credenciais.Length <> 2 OrElse credenciais(0) <> authUser OrElse credenciais(1) <> authPass Then
                    EscreveLog("(Handler) Credenciais inválidas.")
                    Context.Response.StatusCode = 401 ' Unauthorized
                    Return "Erro: Credenciais inválidas."
                End If

                EscreveLog("(Handler) Credenciais válidas.")

                ' Obter o conteúdo do envelope SOAP bruto da requisição
                Dim httpRequest As HttpRequest = Context.Request
                Dim inputStream As Stream = httpRequest.InputStream
                inputStream.Position = 0 ' Reseta a posição do stream
                Dim xmlData As String = New StreamReader(inputStream).ReadToEnd()

                ' Verificar se o XML foi recebido corretamente
                If String.IsNullOrEmpty(xmlData) Then
                    EscreveLog("(Handler) O conteúdo do XML está vazio.")
                    Return "Erro: O conteúdo do XML está vazio."
                End If

                ' Logar o XML bruto recebido
                EscreveLog($"(Handler) Conteúdo do XML recebido: {xmlData}")

                ' Carregar o XML para um XmlDocument
                Dim xmlDoc As New XmlDocument()
                xmlDoc.LoadXml(xmlData)

                ' Extrair o conteúdo de xmlString
                Dim xmlStringNode As XmlNode = xmlDoc.SelectSingleNode("//xmlString")
                If xmlStringNode Is Nothing OrElse String.IsNullOrWhiteSpace(xmlStringNode.InnerText) Then
                    EscreveLog("(Handler) O conteúdo do nó xmlString está vazio ou nulo.")
                    Return "Erro: O conteúdo do xmlString está vazio ou nulo."
                End If

                Dim xmlString As String = xmlStringNode.InnerText
                EscreveLog($"(Handler) Conteúdo de xmlString extraído: {xmlString}")

                ' Declarar a variável request como Object
                Dim request As Object = Nothing

                ' Processar com base no tipo de requisição
                If xmlString.Contains("Profile_request") Then
                    EscreveLog("(Handler) Ação identificada como Profile.")
                    request = ParseProfileXml(xmlString)
                    ' Processar a ação de Profile
                    Return ProcessProfileActions(CType(request, ProfileActionsRequestModel))
                ElseIf xmlString.Contains("Mobile_request") Then
                    EscreveLog("(Handler) Ação identificada como Mobile.")
                    request = ParseMobileXml(xmlString)
                    ' Processar a ação de Mobile
                    Return ProcessMobileActions(CType(request, MobileActionsRequestModel))
                Else
                    EscreveLog("(Handler) Tipo de ação não identificado.")
                    Return "Erro: Tipo de requisição não reconhecido."
                End If

            Catch ex As Exception
                EscreveLog($"(Handler) Erro em createInstance: {ex.Message}")
                Return $"Erro: {ex.Message}"
            End Try
        End Function

        ' Método para processar ações de perfil (Profile)
        Private Function ProcessProfileActions(ByVal request As ProfileActionsRequestModel) As String
            Try
                EscreveLog("(Handler) Iniciando ProcessProfileActions.")
                ' Processa a ação de Profile
                Dim processor As New ServiceProcessor()
                processor.ProcessProfileActions(request)
                Return "ProfileActions processado com sucesso."
            Catch ex As Exception
                EscreveLog($"(Handler) Erro em ProcessProfileActions: {ex.Message}")
                Return $"Erro: {ex.Message}"
            End Try
        End Function

        ' Método para processar ações móveis (Mobile)
        Private Function ProcessMobileActions(ByVal request As MobileActionsRequestModel) As String
            Try
                EscreveLog("(Handler) Iniciando ProcessMobileActions.")
                ' Processa a ação de Mobile
                Dim processor As New ServiceProcessor()
                processor.ProcessMobileActions(request)
                Return "MobileActions processado com sucesso."
            Catch ex As Exception
                EscreveLog($"(Handler) Erro em ProcessMobileActions: {ex.Message}")
                Return $"Erro: {ex.Message}"
            End Try
        End Function

        ' Método para processar o XML e extrair os dados do Profile
        Private Function ParseProfileXml(ByVal xmlData As String) As ProfileActionsRequestModel
            Dim request As New ProfileActionsRequestModel()
            Try
                EscreveLog("(Handler) Iniciando ParseProfileXml.")
                Dim xmlDoc As New XmlDocument()
                xmlDoc.LoadXml(xmlData)

                ' Gerenciando namespaces
                Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
                nsmgr.AddNamespace("ns0", "http://xmlns.oracle.com/Profile_request")

                ' Extrair valores do XML
                request.Action = xmlDoc.SelectSingleNode("//ns0:action", nsmgr)?.InnerText
                request.RequestNumber = xmlDoc.SelectSingleNode("//ns0:requestNumber", nsmgr)?.InnerText
                request.WorkOrderNumber = xmlDoc.SelectSingleNode("//ns0:workOrderNumber", nsmgr)?.InnerText
                request.UserName = xmlDoc.SelectSingleNode("//ns0:userName", nsmgr)?.InnerText
                request.UserNumber = xmlDoc.SelectSingleNode("//ns0:userNumber", nsmgr)?.InnerText
                request.ManagerOrAdm = xmlDoc.SelectSingleNode("//ns0:managerOrAdm", nsmgr)?.InnerText
                request.ViewProfile = xmlDoc.SelectSingleNode("//ns0:viewProfile", nsmgr)?.InnerText
                request.ManagerNumber = xmlDoc.SelectSingleNode("//ns0:managerNumberList", nsmgr)?.InnerText
                request.TransactionID = xmlDoc.SelectSingleNode("//ns0:transactionID", nsmgr)?.InnerText

                EscreveLog("(Handler) ParseProfileXml concluído com sucesso.")
                Return request
            Catch ex As Exception
                EscreveLog($"(Handler) Erro ao processar XML de Profile: {ex.Message}")
                Throw New Exception($"Erro ao processar XML de Profile: {ex.Message}")
            End Try
        End Function

        ' Método para processar o XML e extrair os dados do Mobile
        Private Function ParseMobileXml(ByVal xmlData As String) As MobileActionsRequestModel
            Dim request As New MobileActionsRequestModel()
            Try
                EscreveLog("(Handler) Iniciando ParseMobileXml.")
                Dim xmlDoc As New XmlDocument()
                xmlDoc.LoadXml(xmlData)

                ' Gerenciando namespaces
                Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
                nsmgr.AddNamespace("ns0", "http://xmlns.oracle.com/Mobile_request")

                ' Extrair valores do XML
                request.Action = xmlDoc.SelectSingleNode("//ns0:action", nsmgr)?.InnerText
                request.RequestNumber = xmlDoc.SelectSingleNode("//ns0:requestNumber", nsmgr)?.InnerText
                request.WorkOrderNumber = xmlDoc.SelectSingleNode("//ns0:workOrderNumber", nsmgr)?.InnerText
                request.UserName = xmlDoc.SelectSingleNode("//ns0:userName", nsmgr)?.InnerText
                request.UserNumber = xmlDoc.SelectSingleNode("//ns0:userNumber", nsmgr)?.InnerText
                request.DesignationProduct = xmlDoc.SelectSingleNode("//ns0:designationProduct", nsmgr)?.InnerText
                request.TelecomProvider = xmlDoc.SelectSingleNode("//ns0:telecomProvider", nsmgr)?.InnerText
                request.TransactionID = xmlDoc.SelectSingleNode("//ns0:transactionID", nsmgr)?.InnerText

                EscreveLog("(Handler) ParseMobileXml concluído com sucesso.")
                Return request
            Catch ex As Exception
                EscreveLog($"(Handler) Erro ao processar XML de Mobile: {ex.Message}")
                Throw New Exception($"Erro ao processar XML de Mobile: {ex.Message}")
            End Try
        End Function
    End Class
End Namespace
