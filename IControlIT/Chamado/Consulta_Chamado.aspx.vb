Public Class Consulta_Chamado
    Inherits System.Web.UI.Page
    Dim WS_Cadastro As New WS_GUA_Cadastro.WSCadastro
    Dim WS_Chamado As New WS_GUA_Chamado.WSChamado
    Dim vdataset As Data.DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Dim connBancoValue = Session("Conn_Banco")

            If String.IsNullOrEmpty(connBancoValue) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('A string de conex�o est� vazia.');", True)
            Else
                BindChamados()
            End If
        End If
    End Sub


    Private Sub BindChamados()
        ' Obt�m a conex�o com o banco de dados
        Dim pPConn_Banco As String = Session("Conn_Banco")

        ' Defina os par�metros como Nullable, caso sejam do tipo valor
        Dim pId_Chamado As Integer? = 1
        Dim pCorrelationId As String = "A"
        Dim pNumero_Solicitacao As String = "A"
        Dim pEstado As String = "A"
        Dim pComentarios As String = "A"
        Dim pAtribuido_Para As String = "A"
        Dim pTipo_Solicitacao As String = "A"
        Dim pTransactionID As String = "A"
        Dim pId_Consumidor As Integer? = 1
        Dim pId_Ativo As Integer? = 1
        Dim pId_Conglomerado As Integer? = 1
        Dim pId_Plano As Integer? = 1
        Dim pPakage As String = "busca_todos_dados"
        Dim pRetorno As Boolean = True

        ' Certifique-se de que a conex�o com o banco de dados n�o � nula
        If String.IsNullOrEmpty(pPConn_Banco) Then
            ' Trate o caso onde a conex�o n�o est� dispon�vel
            Throw New InvalidOperationException("A conex�o com o banco de dados n�o foi encontrada.")
        End If

        ' Chama o m�todo do Web Service para obter os chamados
        Dim vdataset As DataSet = WS_Chamado.Chamado(pPConn_Banco, pPakage, pId_Chamado, pCorrelationId, pNumero_Solicitacao, pEstado, pComentarios, pAtribuido_Para, pTipo_Solicitacao, pTransactionID, pId_Consumidor, pId_Ativo, pId_Conglomerado, pId_Plano, pRetorno)

        ' Verifica se h� dados retornados
        If vdataset IsNot Nothing AndAlso vdataset.Tables.Count > 0 Then
            ' Associa os dados ao Repeater
            rptChamados.DataSource = vdataset.Tables(0)
            rptChamados.DataBind()
        Else
            ' L�gica para quando n�o h� dados
            rptChamados.DataSource = Nothing
            rptChamados.DataBind()
        End If
    End Sub

End Class


