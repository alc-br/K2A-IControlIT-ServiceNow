
Public Class Consulta_Caixa_Entrada
    Inherits System.Web.UI.Page
    Dim WS_Cadastro As New WS_GUA_Cadastro.WSCadastro
    Dim WS_Manutencao As New WS_GUA_Manutencao.WSManutencao
    Dim oConfig As New cls_Config
    Dim vdataset As System.Data.DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
            WS_Manutencao.Credentials = System.Net.CredentialCache.DefaultCredentials

            Dim vTraduzir As New cls_Traducao
            '-----verifica permissao de acesso a tela
            Call vTraduzir.Permissao_Tela(Page.Request.Path)
            '-----traduz e passa titulo para master page
            Call Master.Titulo( _
                IIf(vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma")) = Nothing, _
                "Caixa de Entrada", _
                vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma"))))

            '-----home
            If Not Session("Id_Usuario_Perfil_Acesso") = 1 Then
                Call Master.home("usuario")
            End If

            '-----voltar
            Call Master.Voltar("javascript:history.go(-1);", Nothing)
            Call Master.Localizar(Nothing, Nothing)

            oConfig.CarregaCombo(cboMenssagem, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Mail_Sender", Nothing))

            '-----localiza
            WS_Manutencao.Credentials = System.Net.CredentialCache.DefaultCredentials
            Session("DataSet") = WS_Manutencao.caixa_entrada(Session("Conn_Banco"), "sd_Caixa_Entrada", Session("Id_Usuario"), Nothing, Nothing, True)
            dtgLocaliza.DataSource = Session("DataSet")
            dtgLocaliza.DataBind()
        End If
    End Sub

    Protected Sub dtgLocaliza_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgLocaliza.PageIndexChanged
        dtgLocaliza.CurrentPageIndex = e.NewPageIndex
        dtgLocaliza.DataSource = Session("DataSet")
        dtgLocaliza.DataBind()
    End Sub

    Protected Sub dtgLocaliza_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles dtgLocaliza.SelectedIndexChanged
        WS_Manutencao.Credentials = System.Net.CredentialCache.DefaultCredentials
        hdfIdMail.Value = dtgLocaliza.SelectedItem.Cells(5).Text

        vdataset = WS_Manutencao.caixa_entrada(Session("Conn_Banco"), "sd_Detalhamento", Nothing, Nothing, hdfIdMail.Value, True)
        lblDescricaoAssunto.Text = vdataset.Tables(0).Rows(0).Item("Assunto")
        lblDescricaoEmailDestino.Text = vdataset.Tables(0).Rows(0).Item("E_Mail_Destino")
        lblDescricaoEmailCopia.Text = vdataset.Tables(0).Rows(0).Item("E_Mail_Copia")
        lblDescricaoTexto1.Text = vdataset.Tables(0).Rows(0).Item("Texto_1")
        lblDescricaoTexto2.Text = vdataset.Tables(0).Rows(0).Item("Texto_2")
        lblDescricaoTexto3.Text = vdataset.Tables(0).Rows(0).Item("Texto_3")
        lblDescricaoTexto4.Text = vdataset.Tables(0).Rows(0).Item("Texto_4")
        lblDescricaoTexto5.Text = vdataset.Tables(0).Rows(0).Item("Texto_5")
        lblDescricaoTextAdicional.Text = vdataset.Tables(0).Rows(0).Item("Texto_Adicional")

        pnlmsg.Visible = True
    End Sub

    Protected Sub btFecharMsg_Click(sender As Object, e As EventArgs) Handles btFecharMsg.Click
        pnlmsg.Visible = False
        lblDescricaoAssunto.Text = ""
        lblDescricaoEmailCopia.Text = ""
        lblDescricaoTexto1.Text = ""
        lblDescricaoTexto2.Text = ""
        lblDescricaoTexto3.Text = ""
        lblDescricaoTexto4.Text = ""
        lblDescricaoTexto5.Text = ""
        lblDescricaoTextAdicional.Text = ""
    End Sub

    Protected Sub btReenviarEmail_Click(sender As Object, e As System.EventArgs) Handles btReenviarEmail.Click
        '-----reenvia email
        WS_Manutencao.caixa_entrada(Session("Conn_Banco"), "sd_Reenviar", Nothing, Nothing, hdfIdMail.Value, False)
        pnlmsg.Visible = False
        lblDescricaoAssunto.Text = ""
        lblDescricaoEmailCopia.Text = ""
        lblDescricaoTexto1.Text = ""
        lblDescricaoTexto2.Text = ""
        lblDescricaoTexto3.Text = ""
        lblDescricaoTexto4.Text = ""
        lblDescricaoTexto5.Text = ""
        lblDescricaoTextAdicional.Text = ""

        '-----localiza
        WS_Manutencao.Credentials = System.Net.CredentialCache.DefaultCredentials
        Session("DataSet") = WS_Manutencao.caixa_entrada(Session("Conn_Banco"), "sd_Caixa_Entrada", Session("Id_Usuario"), Nothing, Nothing, True)
        dtgLocaliza.DataSource = Session("DataSet")
        dtgLocaliza.DataBind()
    End Sub

    Protected Sub btExecutar_Click(sender As Object, e As System.EventArgs) Handles btExecutar.Click
        '-----localiza com descricao
        WS_Manutencao.Credentials = System.Net.CredentialCache.DefaultCredentials
        Session("DataSet") = WS_Manutencao.caixa_entrada(Session("Conn_Banco"), "sd_Caixa_Entrada", Session("Id_Usuario"), Nothing, IIf(cboMenssagem.SelectedValue = "", Nothing, cboMenssagem.SelectedValue), True)
        dtgLocaliza.DataSource = Session("DataSet")
        dtgLocaliza.DataBind()
    End Sub

    Protected Sub btVoltar_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "javascript:history.go(-1);", True)
    End Sub

End Class
