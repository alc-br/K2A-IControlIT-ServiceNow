Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data
Imports System.Runtime.CompilerServices

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="WSChamado", Name:="WSChamado")>
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
Public Class WSChamado
    Inherits WebService

    Dim oBanco As New cls_Banco
    Dim vParametro() As SqlClient.SqlParameter


    '----- Busca todos os chamados
    <WebMethod()>
    Public Function TodosChamados(ByVal pPConn_Banco As System.String,
                        ByVal pRetorno As System.Boolean) As System.Data.DataSet

        ReDim vParametro(0)
        oBanco.monta_Parametro(vParametro, "busca_todos_dados", "@pPAKAGE", False)

        Return oBanco.retorna_Query("dbo.pa_Chamado", vParametro, pPConn_Banco)

    End Function

    '----- Insere, atualiza, exclui, ou busca chamados
    <WebMethod()>
    Public Function Chamado(ByVal pPConn_Banco As System.String,
                        ByVal pPakage As System.String,
                        ByVal pId_Chamado As System.Int32,
                        ByVal pCorrelationId As System.String,
                        ByVal pNumero_Solicitacao As System.String,
                        ByVal pEstado As System.String,
                        ByVal pComentarios As System.String,
                        ByVal pAtribuido_Para As System.String,
                        ByVal pTipo_Solicitacao As System.String,
                        ByVal pTransactionID As System.String,
                        ByVal pId_Consumidor As System.Int32,
                        ByVal pId_Ativo As System.Int32,
                        ByVal pId_Conglomerado As System.Int32,
                        ByVal pId_Plano As System.Int32,
                        ByVal pRetorno As System.Boolean) As System.Data.DataSet



        If (pPakage <> "busca_todos_dados") Then
            ReDim vParametro(12)
            oBanco.monta_Parametro(vParametro, pPakage, "@pPAKAGE", False)
            oBanco.monta_Parametro(vParametro, pId_Chamado, "@pId_Chamado", False)
            oBanco.monta_Parametro(vParametro, pCorrelationId, "@pCorrelationId", False)
            oBanco.monta_Parametro(vParametro, pNumero_Solicitacao, "@pNumero_Solicitacao", False)
            oBanco.monta_Parametro(vParametro, pEstado, "@pEstado", False)
            oBanco.monta_Parametro(vParametro, pComentarios, "@pComentarios", False)
            oBanco.monta_Parametro(vParametro, pAtribuido_Para, "@pAtribuido_Para", False)
            oBanco.monta_Parametro(vParametro, pTipo_Solicitacao, "@pTipo_Solicitacao", False)
            oBanco.monta_Parametro(vParametro, pTransactionID, "@pTransactionID", False)
            oBanco.monta_Parametro(vParametro, pId_Consumidor, "@pId_Consumidor", False)
            oBanco.monta_Parametro(vParametro, pId_Ativo, "@pId_Ativo", False)
            oBanco.monta_Parametro(vParametro, pId_Conglomerado, "@pId_Conglomerado", False)
            oBanco.monta_Parametro(vParametro, pId_Plano, "@pId_Plano", False)
        Else
            ReDim vParametro(0)
            oBanco.monta_Parametro(vParametro, "busca_todos_dados", "@pPAKAGE", False)
        End If

        Return oBanco.retorna_Query("dbo.pa_Chamado", vParametro, pPConn_Banco)

    End Function






    '----- Gerencia as operações relacionadas aos ativos
    <WebMethod()>
    Public Function Ativo_Chamado(ByVal pPConn_Banco As System.String,
                                  ByVal pPakage As System.String,
                                  ByVal pId_Ativo As System.Int32,
                                  ByVal pId_Ativo_Status As System.Int32,
                                  ByVal pNr_Ativo As System.String,
                                  ByVal pNewNr_Ativo As System.String,
                                  ByVal pNewAreaCode As System.String,
                                  ByVal pId_Conglomerado As System.Int32,
                                  ByVal pPlano_Contrato As System.String,
                                  ByVal pRetorno As System.Boolean) As System.Data.DataSet

        ReDim vParametro(7)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPAKAGE", False)
        oBanco.monta_Parametro(vParametro, pId_Ativo, "@pId_Ativo", False)
        oBanco.monta_Parametro(vParametro, pId_Ativo_Status, "@pId_Ativo_Status", False)
        oBanco.monta_Parametro(vParametro, pNr_Ativo, "@pNr_Ativo", False)
        oBanco.monta_Parametro(vParametro, pNewNr_Ativo, "@pNewNr_Ativo", False)
        oBanco.monta_Parametro(vParametro, pNewAreaCode, "@pNewAreaCode", False)
        oBanco.monta_Parametro(vParametro, pId_Conglomerado, "@pId_Conglomerado", False)
        oBanco.monta_Parametro(vParametro, pPlano_Contrato, "@pPlano_Contrato", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Ativo_Chamado", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Ativo_Chamado", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function

    '----- Gerencia o relacionamento entre ativos e consumidores
    <WebMethod()>
    Public Function Ativo_Relacionamento(ByVal pPConn_Banco As System.String,
                                         ByVal pPakage As System.String,
                                         ByVal pId_Ativo As System.Int32,
                                         ByVal pId_Consumidor As System.Int32,
                                         ByVal pDt_Hr_Desativacao As System.DateTime,
                                         ByVal pRetorno As System.Boolean) As System.Data.DataSet

        ReDim vParametro(3)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPAKAGE", False)
        oBanco.monta_Parametro(vParametro, pId_Ativo, "@pId_Ativo", False)
        oBanco.monta_Parametro(vParametro, pId_Consumidor, "@pId_Consumidor", False)
        oBanco.monta_Parametro(vParametro, pDt_Hr_Desativacao, "@pDt_Hr_Desativacao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Ativo_Relacionamento", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Ativo_Relacionamento", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function
End Class
