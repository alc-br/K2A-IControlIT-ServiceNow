<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Consulta_Chamado.aspx.vb" Inherits="IControlIT.Consulta_Chamado" EnableEventValidation="true" %>

<%@ MasterType VirtualPath="~/Principal.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div runat="server" title="CHAMADOS">
        <style type="text/css">
            .ajax__tab_xp .ajax__tab_body {
                font-family: Arial;
                font-size: 10pt;
                border-top: 0;
                border: 1px solid #999999;
                padding: 8px;
                background-color: transparent;
            }

            .justify-content-between {
              justify-content: space-between !important;
              margin: 20px 0;
              background: #fff;
              padding: 15px 20px 10px 25px;
              border-radius: 10px;
            }

            #divTitulo{
                display:none;
            }

            .date-time{
                font-size: 12px;
                padding-right: 10px;
            }
        </style>
    </div>

    <!--Tela *************************************************************************************** -->
<div>
    <div class="row">
        <div class="col-md-12">
            <div class="d-flex justify-content-between align-items-center activity">
                <div><span class="ml-2" style="font-size:24px;font-size: 18px;font-weight: bold;">Chamados Recentes</span></div>
            </div>
            <div class="mt-3">
                <ul class="list list-inline">
                    <asp:Repeater ID="rptChamados" runat="server">
                        <ItemTemplate>
                            <li class="d-flex justify-content-between">
                                <div class="d-flex flex-row align-items-center">
                                    <i class="fa fa-check-circle checkicon" style="font-size:20px;"></i>
                                    <div class="ml-3">
                                        <div class="d-flex flex-row text-black-50 date-time">
                                            <div><span><%# Eval("Tipo_Solicitacao") %></span></div>
                                            <div class="ml-4"><i class="fa fa-hashtag"></i><span class="ml-2"><%# Eval("Numero_Solicitacao") %></span></div>
                                            <div class="ml-4"><i class="fa fa-user"></i><span class="ml-2"><%# Eval("Consumidor") %></span></div>
                                            <div class="ml-4" style="margin-top: 3px;font-size: 14px;"><span class="badge badge-primary badge-pill"><%# Eval("Estado") %></span></div>
                                        </div>
                                        <p class="mt-1"><%# Eval("Comentarios") %></p>
                                    </div>
                                </div>
                                <div class="d-flex flex-row align-items-center">
                                    <div class="d-flex flex-column mr-2">
                                        <span class="date-time">Criado em: <%# Eval("Data_Criacao") %></span>
                                        <span class="date-time">Atualizado em: <%# Eval("Data_Atualizacao") %></span>
                                    </div>
                                    <i class="fa fa-ellipsis-h"></i>
                                </div>
                                <!-- Campos ocultos -->
                                <asp:HiddenField ID="hfIdChamado" Value='<%# Eval("Id_Chamado") %>' runat="server" />
                                <asp:HiddenField ID="hfIdConsumidor" Value='<%# Eval("Id_Consumidor") %>' runat="server" />
                                <asp:HiddenField ID="hfIdAtivo" Value='<%# Eval("Id_Ativo") %>' runat="server" />
                                <asp:HiddenField ID="hfIdConglomerado" Value='<%# Eval("Id_Conglomerado") %>' runat="server" />
                                <asp:HiddenField ID="hfIdPlano" Value='<%# Eval("Id_Plano") %>' runat="server" />
                                <asp:HiddenField ID="hfTransactionID" Value='<%# Eval("TransactionID") %>' runat="server" />
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </div>
    </div>
</div>













</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

    <div id="tbBotao" runat="server" class="scrollmenu">
        <div class="btn-menu-toolbar divEspaco"></div>
        <asp:LinkButton ID="btVoltar" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false">
            <i class="fas fa-arrow-left"></i>
            <br />
            <span>Voltar</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btEmail" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" >
            <i class="fas fa-envelope"></i>
            <br />
            <span>Email</span>
        </asp:LinkButton>
    </div>

</asp:Content>


