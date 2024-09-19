<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Consulta_Chamado.aspx.vb" Inherits="IControlIT.Consulta_Chamado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- INICIO PÁGINA -->

    <style type="text/css">
        .modal-custom {
            display: none;
            position: fixed;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            background-color: #fff;
            border-radius: 8px;
            box-shadow: 0 4px 20px rgba(0, 0, 0, 0.2);
            max-width: 900px;
            width: 90%;
            padding: 20px;
            z-index: 1000;
        }

        .modal-header {
            border-bottom: 1px solid #ddd;
            padding-bottom: 10px;
            display: flex;
            justify-content: space-between;
            align-items: center;
        }

        .modal-title {
            font-size: 1.5rem;
            font-weight: 600;
            margin: 0;
        }

        .close {
            background: none;
            border: none;
            font-size: 1.5rem;
            cursor: pointer;
        }

        .modal-body {
            padding-top: 20px;
            font-size: 14px;
        }

        .left-column {
            width: 40%;
            padding-right: 20px;
        }

        .right-column {
            width: 60%;
            padding-left: 20px;
        }

        .form-group-row {
            display: flex;
            gap: 20px;
            justify-content: space-between;
        }

        .form-group {
            flex: 1;
            min-width: 0;
        }

        .form-control {
            border-radius: 4px;
            border: 1px solid #ddd;
            padding: 10px;
            width: 100%;
        }

        .custom-file {
            position: relative;
            border: 1px dashed #ccc;
            border-radius: 4px;
            padding: 10px;
            text-align: center;
            cursor: pointer;
        }

        #loadingSpinner {
            display: none;
            margin-top: 10px;
            text-align: center;
        }

        .vertical-separator {
            border-left: 1px solid #ddd;
            height: auto;
            margin: 0 20px;
        }

        .divider {
            border-bottom: 1px solid #ddd;
            margin: 10px 0 20px 0;
        }

        .modal-footer {
            border-top: 1px solid #ddd;
            padding-top: 10px;
            display: flex;
            justify-content: space-between;
        }

        .btn-secondary {
            background-color: #aaa;
            border: none;
            color: #fff;
            padding: 10px 20px;
            border-radius: 4px;
            cursor: pointer;
        }

        /* Manter o restante do seu CSS atual */
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
            padding: 15px 20px 15px 25px;
            border-radius: 10px;
        }

        #divTitulo {
            display: none;
        }

        .date-time {
            font-size: 12px;
            padding-right: 10px;
        }

        .modal-dialog {
            max-width: 60% !important;
        }

        .modal-backdrop-custom {
            background-color: rgba(0, 0, 0, 0.5);
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            z-index: 1050 !important;
        }

        .flex-fill {
            display: flex;
            flex-direction: column;
        }

        .verde {
            background-color: #32B877 !important;
        }

        .azul {
            background-color: #538DD7 !important;
        }

        .esconde {
            display: none;
        }

        .modal-custom{
            z-index: 1051 !important;
        }

        .dropdown-emails {
            display: none;
            border: 1px solid #ddd;
            border-radius: 4px;
            max-height: 150px;
            overflow-y: auto;
            padding: 10px;
            position: absolute;
            background-color: white;
            z-index: 1000;
            width: calc(60% - 20px);
        }

        .dropdown-emails.active {
            display: block;
        }




    .pagination-container {
        text-align: center;
        margin: 20px 0;
    }

    .pagination {
        display: inline-block;
    }

    .pagination .page-buttons {
        display: inline-flex;
        align-items: center;
    }

    .pagination .page-buttons .asp-button {
        margin: 0 10px;
    }

    .items-per-page {
        margin-top: 10px;
    }

</style>

<div runat="server" title="CHAMADOS">
        
    <!-- Campos ocultos essenciais para as ações -->
    <input type="text" id="hfNewUserNumber" class="esconde" runat="server" />
    <input type="text" id="hfNewAreaCode" class="esconde" runat="server" />
    <input type="text" id="hfTelecomProvider" class="esconde" runat="server" />
    <input type="text" id="hfUserNumber" class="esconde" runat="server" />
    <input type="text" id="hfIdAtivo" class="esconde" runat="server" />
    <input type="text" id="hfIdConsumidor" class="esconde" runat="server" />
    <input type="text" id="hfNewPlanoContrato" class="esconde" runat="server" />
    <input type="text" id="hfIdConglomerado" class="esconde" runat="server" />
    <input type="text" id="hfIdChamado" class="esconde" runat="server" />
    <input type="text" id="hfTipoSolicitacao" class="esconde" runat="server" />
    <input type="text" id="hfComentarios" class="esconde" runat="server" />
    <input type="text" id="hfEstado" class="esconde" runat="server" />
    <input type="text" id="hfRequestNumber" class="esconde" runat="server" />
    <input type="text" id="hfWorkOrderNumber" class="esconde" runat="server" />
    <input type="text" id="hfUserName" class="esconde" runat="server" />
    

    

    <div>
        <div class="row">
            <div class="col-md-12">
                <div class="d-flex justify-content-between align-items-center activity">
                    <div>
                        <span class="ml-2" style="font-size: 24px; font-weight: bold;">Chamados Recentes</span>
                    </div>
                </div>
                <div class="mt-3">
                    <ul class="list list-inline">
                        <asp:Repeater ID="rptChamados" runat="server">
                            <ItemTemplate>
                                <li class="d-flex justify-content-between">
                                    <div class="d-flex flex-row align-items-center">
                                        <div class="ml-3">
                                            <div class="d-flex flex-row text-black-50 date-time" style="font-size: 13px; font-weight: 400;">
                                                <div class="mr-3" style="font-size: 15px;">
                                                    <span class='badge badge-pill badge-primary <%# GetBadgeClass(Eval("Estado")) %>' style="padding: 6px; border-radius: 4px;">
                                                        <%# Eval("Estado") %>
                                                    </span>
                                                </div>
                                                <div><span style="font-weight: 600;"><%# Eval("Tipo_Solicitacao") %></span></div>
                                                <div class="ml-4">
                                                    <i class="fa fa-hashtag"></i>
                                                    <span class="ml-2"><%# Eval("RequestNumber") %></span>
                                                </div>
                                                <div class="ml-4">
                                                    <i class="fa fa-clipboard"></i>
                                                    <span class="ml-2"><%# Eval("WorkOrderNumber") %></span>
                                                </div>
                                                <div class="ml-4">
                                                    <i class="fa fa-user"></i>
                                                    <span class="ml-2"><%# Eval("UserName") %></span>
                                                </div>
                                            </div>
                                            <p class='<%# If(String.IsNullOrEmpty(Eval("Comentarios").ToString()), "esconde", "") %>'>
                                                <%# Eval("Comentarios") %>
                                            </p>
                                        </div>
                                    </div>
                                    <div class="d-flex flex-row align-items-center">
                                        <div class="d-flex flex-column mr-2">
                                            <span class="date-time">Criado em: <%# Eval("Data_Criacao", "{0:dd/MM/yyyy HH:mm:ss}") %></span>
                                            <span class="date-time">Atualizado em: <%# Eval("Data_Atualizacao", "{0:dd/MM/yyyy HH:mm:ss}") %></span>
                                        </div>
                                        <i class="fa fa-bars" style="cursor: pointer; font-size: 20px; margin-right: 10px; margin-left: 20px;" onclick="abrirModalChamado(
                                            '<%# Eval("Id_Chamado") %>',
                                            '<%# Eval("RequestNumber") %>',
                                            '<%# Eval("WorkOrderNumber") %>',
                                            '<%# Eval("Estado") %>',
                                            '<%# Eval("Comentarios") %>',
                                            '<%# Eval("UserName") %>',
                                            '<%# Eval("TransactionID") %>',
                                            '<%# Eval("Tipo_Solicitacao") %>',
                                            '<%# Eval("UserNumber") %>',
                                            '<%# Eval("TelecomProvider") %>',
                                            '<%# Eval("NewAreaCode") %>',
                                            '<%# Eval("NewUserNumber") %>',
                                            '<%# Eval("FramingPlan") %>',
                                            '<%# Eval("ServicePack") %>',
                                            '<%# Eval("PlanoContratoAtual") %>',
                                            '<%# Eval("Id_Ativo") %>',
                                            '<%# Eval("Id_Conglomerado") %>'
                                        )"></i>

                                    </div>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
                <div class="pagination-container">
                    <div class="pagination">
                        <div class="page-buttons">
                            <asp:Button ID="btnPreviousPage" runat="server" Text="Anterior" OnClick="BtnPreviousPage_Click" CssClass="asp-button" />
                            <asp:Label ID="lblPageNumber" runat="server" Text="1"></asp:Label>
                            <asp:Button ID="btnNextPage" runat="server" Text="Próxima" OnClick="BtnNextPage_Click" CssClass="asp-button" />
                        </div>
                    </div>

                    <!-- Dropdown para selecionar itens por página -->
                    <div class="items-per-page">
                        <label for="ddlItemsPerPage">Itens por página:</label>
                        <asp:DropDownList ID="ddlItemsPerPage" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlItemsPerPage_SelectedIndexChanged">
                            <asp:ListItem Text="10" Value="10" Selected="True"/>
                            <asp:ListItem Text="30" Value="30" />
                            <asp:ListItem Text="50" Value="50" />
                            <asp:ListItem Text="100" Value="100" />
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

    <!-- SCRIPTS ------------------------------------------------------------------------------------------->

    <!-- TRATAMENTO DO MODAL -->

    <script type="text/javascript">

        // Função para resetar os campos da direita
        function resetCamposDireita() {
            // Empresa Contratante e Fatura Agrupadora (Dropdowns)
            document.getElementById('ContentPlaceHolder2_empresaContratante').selectedIndex = 0;
            document.getElementById('ContentPlaceHolder2_faturaDropdown').selectedIndex = 0;

            // Corpo do Email (Text Area)
            document.getElementById('ContentPlaceHolder2_corpoEmail').value = '';

            // Operadora Emails
            document.getElementById('operadoraEmails').value = '';
            document.querySelectorAll('#emailDropdown input[type="checkbox"]').forEach(function (checkbox) {
                checkbox.checked = false;
            });
            document.getElementById('ContentPlaceHolder2_selectedEmails').innerHTML = ''; // Limpa a lista de emails selecionados

            // Anexos (limpar a lista e a variável de arquivos selecionados)
            arquivosSelecionados = []; // Reseta a lista de anexos selecionados
            document.getElementById('listaAnexos').innerHTML = ''; // Limpa a exibição dos anexos

            // Email do Responsável na Regional
            document.getElementById('ContentPlaceHolder2_emailResponsavelRegional').value = '';
        }

        function abrirModalChamado(idChamado, requestNumber, workOrderNumber, estado, comentarios, userName, transactionID, tipoSolicitacao, userNumber, telecomProvider, newAreaCode, newUserNumber, framingPlan, servicePack, planoContratoAtual, idAtivo, idConglomerado) {


            function loadFields() {

                // Funções de utilidade para configurar visibilidade e valores
                function setElementVisibility(elementId, value) {
                    var element = document.getElementById(elementId);
                    if (element) {
                        if (value && value.trim() !== "") {
                            element.style.display = "block";
                            element.querySelector('span').innerText = value;
                        } else {
                            element.style.display = "none";
                        }
                    } else {
                        console.error("Elemento com ID " + elementId + " não encontrado.");
                    }
                }

                function setInputValue(elementId, value) {
                    var element = document.getElementById(elementId);
                    if (element) {
                        element.value = value || '';
                    } else {
                        console.error("Elemento com ID " + elementId + " não encontrado.");
                    }
                }

                // Preencher os novos campos adicionais
                setInputValue('ContentPlaceHolder1_hfUserNumber', userNumber);
                setInputValue('ContentPlaceHolder1_hfTelecomProvider', telecomProvider);
                setInputValue('ContentPlaceHolder1_hfNewAreaCode', newAreaCode);
                setInputValue('ContentPlaceHolder1_hfNewUserNumber', newUserNumber);
                setInputValue('ContentPlaceHolder1_hfIdAtivo', idAtivo);
                setInputValue('ContentPlaceHolder1_hfIdConsumidor', transactionID);
                setInputValue('ContentPlaceHolder1_hfIdChamado', idChamado);
                setInputValue('ContentPlaceHolder1_hfIdConglomerado', idConglomerado);
                setInputValue('ContentPlaceHolder1_hfNewPlanoContrato', framingPlan);
                setInputValue('ContentPlaceHolder1_hfTipoSolicitacao', tipoSolicitacao);
                setInputValue('ContentPlaceHolder1_hfComentarios', comentarios);
                setInputValue('ContentPlaceHolder1_hfEstado', estado);
                setInputValue('ContentPlaceHolder1_hfRequestNumber', requestNumber);
                setInputValue('ContentPlaceHolder1_hfWorkOrderNumber', workOrderNumber);
                setInputValue('ContentPlaceHolder1_hfUserName', userName);
                
                
                // Converte o estado para minúsculas e verifica se é 'concluído'
                if (estado.toLowerCase() === 'concluído') {
                    // Esconde a coluna direita e o rodapé do modal
                    document.getElementById('rightColumn').style.display = 'none';
                    document.getElementById('modalFooter').style.display = 'none';
                    document.getElementById('verticalSeparator').style.display = 'none';
                } else {
                    // Exibe a coluna direita e o rodapé do modal (caso não seja "Concluído")
                    document.getElementById('rightColumn').style.display = 'flex'; // Flex para manter o layout
                    document.getElementById('modalFooter').style.display = 'flex';
                    document.getElementById('verticalSeparator').style.display = 'flex';
                }

                // Exibir campos padrão
                setElementVisibility('modalIdChamadoContainer', idChamado);
                setElementVisibility('modalRequestNumberContainer', requestNumber);
                setElementVisibility('modalWorkOrderNumberContainer', workOrderNumber);
                setElementVisibility('modalEstadoContainer', estado);
                setElementVisibility('modalComentariosContainer', comentarios);
                setElementVisibility('modalUserNameContainer', userName);
                setElementVisibility('modalTransactionIDContainer', transactionID);
                setElementVisibility('modalTipoSolicitacaoContainer', tipoSolicitacao);
                setElementVisibility('modalPlanoContratoAtualContainer', planoContratoAtual);

                // Limpar campos condicionais
                var camposCondicionaisContainer = document.getElementById('ContentPlaceHolder2_camposCondicionaisContainer');
                camposCondicionaisContainer.innerHTML = "";
            }

            function showConditionalFields(solicitacao) {
                // Exibir campos condicionais
                switch (solicitacao) {
                    case 'ALTERAR DDD':
                        camposCondicionaisContainer.innerHTML = '<p><strong>Novo DDD:</strong> <span>' + newAreaCode + '</span></p>';
                        break;
                    case 'ALTERAR NUMERO':
                        camposCondicionaisContainer.innerHTML = '<p><strong>Novo Número:</strong> <span>' + newUserNumber + '</span></p>';
                        break;
                    case 'MIGRACAO DE PLANO':
                        camposCondicionaisContainer.innerHTML = '<p><strong>Novo Plano:</strong> <span>' + framingPlan + '</span></p>';
                        break;
                    case 'CONTRATAR PACOTE DE ROAMING INTERNACIONAL':
                        camposCondicionaisContainer.innerHTML = '<p><strong>Dados do Pacote:</strong> <span>' + servicePack + '</span></p>';
                        break;
                    case 'HABILITAR ACESSO':
                        camposCondicionaisContainer.innerHTML = '<p><strong>Ação:</strong> <span>Habilitar Acesso</span></p>';
                        break;
                    case 'DESABILITAR ACESSO':
                        camposCondicionaisContainer.innerHTML = '<p><strong>Ação:</strong> <span>Desabilitar Acesso</span></p>';
                        break;
                    case 'CANCELAR LINHA':
                    case 'PERDA/ROUBO':
                        camposCondicionaisContainer.innerHTML = '<p><strong>Motivo:</strong> <span>' + comentarios + '</span></p>';
                        break;
                    case 'PORTABILIDADE DE LINHA':
                        camposCondicionaisContainer.innerHTML = '<p><strong>Data de Portabilidade:</strong> <span>' + comentarios + '</span></p>';
                        break;
                    case 'ALTERAR PROPRIETARIO':
                        camposCondicionaisContainer.innerHTML = '<p><strong>Novo Proprietário:</strong> <span>' + comentarios + '</span></p>';
                        break;
                    default:
                        console.error('Tipo de solicitação desconhecido.');
                        break;
                }
            }

            function carregarEmailsOperadora(idConglomerado) {
                $.ajax({
                    type: "POST",
                    url: "Consulta_Chamado.aspx/BuscarEmailsOperadora",
                    data: JSON.stringify({ idConglomerado: idConglomerado }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        try {
                            var emails = JSON.parse(response.d);  // Faz o parse da string JSON em um array
                            console.log("Emails processados: ", emails); // Verifica o que foi extraído de 'response.d'
                        } catch (e) {
                            console.error("Erro ao fazer parse da resposta: ", e);
                            return;
                        }

                        var emailDropdown = $('#emailDropdown');
                        emailDropdown.empty(); // Limpa o dropdown antes de preencher

                        if (emails && emails.length > 0) {
                            // Adiciona os emails no dropdown com checkboxes
                            emails.forEach(function (email) {
                                emailDropdown.append('<label><input type="checkbox" value="' + email + '"> ' + email + '</label><br>');
                            });

                            // Adiciona dinamicamente o evento de change nos checkboxes
                            emailDropdown.find('input[type="checkbox"]').change(function () {
                                atualizarEmailsSelecionados();
                            });
                        } else {
                            emailDropdown.append('<span>Nenhum email encontrado.</span>');
                        }
                    },
                    error: function (error) {
                        console.error('Erro ao carregar os emails da operadora: ', error);
                        $('#emailDropdown').append('<span>Erro ao carregar os emails.</span>');
                    }
                });
            }



            // Função para atualizar a exibição dos e-mails selecionados
            function atualizarEmailsSelecionados() {
                var selectedEmailsDiv = document.getElementById('ContentPlaceHolder2_selectedEmails');
                var selectedEmails = [];

                document.querySelectorAll('#emailDropdown input[type="checkbox"]:checked').forEach(function (checkedBox) {
                    selectedEmails.push(checkedBox.value);
                });

                // Atualiza a div com os e-mails selecionados
                selectedEmailsDiv.innerHTML = selectedEmails.length > 0 ? selectedEmails.join(', ') : 'Nenhum e-mail selecionado.';
            }


            function carregarFaturaAgrupadora(idConglomerado) {
                $.ajax({
                    type: "POST",
                    url: "Consulta_Chamado.aspx/BuscarFaturaAgrupadora",
                    data: JSON.stringify({ idConglomerado: idConglomerado }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        try {
                            var faturas = JSON.parse(response.d);  // Faz o parse da string JSON em um array
                            console.log("Faturas processadas: ", faturas); // Verifica o que foi extraído de 'response.d'
                        } catch (e) {
                            console.error("Erro ao fazer parse da resposta: ", e);
                            return;
                        }

                        var faturaDropdown = $('#ContentPlaceHolder2_faturaDropdown');
                        faturaDropdown.empty(); // Limpa o dropdown antes de preencher
                        faturaDropdown.append('<option value="">Selecione a Fatura</option>'); // Adiciona o placeholder

                        if (faturas && faturas.length > 0) {
                            // Adiciona as faturas no dropdown
                            faturas.forEach(function (fatura) {
                                faturaDropdown.append('<option value="' + fatura + '">' + fatura + '</option>');
                            });
                        } else {
                            faturaDropdown.append('<option value="">Nenhuma fatura agrupadora encontrada</option>');
                        }
                    },
                    error: function (error) {
                        console.error('Erro ao carregar as faturas agrupadoras: ', error);
                        $('#faturaDropdown').empty().append('<option value="">Erro ao carregar as faturas agrupadoras</option>');
                    }
                });
            }

            // Resetar campos da direita (sempre que o modal for aberto)
            resetCamposDireita();

            // Carrega campos de acordo com o chamado selecionado
            loadFields();

            // Carrega campos condicionais
            showConditionalFields(tipoSolicitacao)

            // Chama a função para carregar os emails ao abrir o modal
            carregarEmailsOperadora(idConglomerado);

            // Chama a função para carregar os emails ao abrir o modal
            carregarFaturaAgrupadora(idConglomerado);

            // Exibir o modal
            document.getElementById('backdropCustom').style.display = 'block';
            document.getElementById('chamadoModal').style.display = 'block';
        }

        function fecharModal() {
            document.getElementById('backdropCustom').style.display = 'none';
            document.getElementById('chamadoModal').style.display = 'none';
        }

    </script>




    <!-- UPLOAD DE ARQUIVOS NO MODAL -->

    <script type="text/javascript">
        let arquivosSelecionados = [];

        function abrirDropdownEmails() {
            var dropdown = document.getElementById('emailDropdown');
            dropdown.style.display = 'block';
        }

        document.querySelectorAll('#emailDropdown input[type="checkbox"]').forEach(function (checkbox) {
            checkbox.addEventListener('change', function () {
                var selectedEmailsDiv = document.getElementById('ContentPlaceHolder2_selectedEmails');
                var selectedEmails = [];
                document.querySelectorAll('#emailDropdown input[type="checkbox"]:checked').forEach(function (checkedBox) {
                    selectedEmails.push(checkedBox.value);
                });
                selectedEmailsDiv.innerHTML = selectedEmails.join(', ');
            });
        });

        // Previne que o dropdown feche ao selecionar os emails
        document.getElementById('emailDropdown').addEventListener('click', function (e) {
            e.stopPropagation();
        });

        // Fecha o dropdown se clicar fora, mas não ao clicar dentro
        document.addEventListener('click', function (e) {
            if (!document.getElementById('operadoraEmails').contains(e.target) && !document.getElementById('emailDropdown').contains(e.target)) {
                document.getElementById('emailDropdown').style.display = 'none';
            }
        });

        // Adicionar novos arquivos sem sobrescrever os anteriores
        function adicionarArquivos() {
            var input = document.getElementById('anexos');
            var listaAnexos = document.getElementById('listaAnexos');
            var arquivos = input.files;

            // Exibe o loading spinner enquanto processa os arquivos
            document.getElementById('loadingSpinner').style.display = 'block';

            setTimeout(function () { // Simulando um tempo para o loading
                for (var i = 0; i < arquivos.length; i++) {
                    var arquivo = arquivos[i];
                    arquivosSelecionados.push(arquivo); // Adiciona à lista de arquivos selecionados

                    var index = arquivosSelecionados.length - 1; // Índice do arquivo recém-adicionado

                    var divArquivo = document.createElement('div');
                    divArquivo.style.display = 'flex';
                    divArquivo.style.alignItems = 'center';
                    divArquivo.style.justifyContent = 'space-between';
                    divArquivo.style.marginBottom = '5px';
                    divArquivo.innerHTML = '<span>' + arquivo.name + '</span>' +
                        '<button type="button" style="background: none; border: none; color: red; cursor: pointer;" onclick="removerArquivo(' + index + ')">X</button>';

                    listaAnexos.appendChild(divArquivo);
                }

                // Esconde o loading spinner
                document.getElementById('loadingSpinner').style.display = 'none';
            }, 500);
        }

        // Função para remover um arquivo da lista
        function removerArquivo(index) {
            arquivosSelecionados.splice(index, 1); // Remove o arquivo da lista
            atualizarListaAnexos(); // Atualiza a lista exibida
        }

        // Atualiza a exibição da lista de anexos após a remoção
        function atualizarListaAnexos() {
            var listaAnexos = document.getElementById('listaAnexos');
            listaAnexos.innerHTML = ''; // Limpa a lista antes de renderizar novamente
            arquivosSelecionados.forEach(function (arquivo, index) {
                var divArquivo = document.createElement('div');
                divArquivo.style.display = 'flex';
                divArquivo.style.alignItems = 'center';
                divArquivo.style.justifyContent = 'space-between';
                divArquivo.style.marginBottom = '5px';
                divArquivo.innerHTML = '<span>' + arquivo.name + '</span>' +
                    '<button type="button" style="background: none; border: none; color: red; cursor: pointer;" onclick="removerArquivo(' + index + ')">X</button>';

                listaAnexos.appendChild(divArquivo);
            });
        }
    </script>
<!-- FIM PÁGINA -->
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

    <div id="tbBotao" runat="server" class="scrollmenu">
        <div class="btn-menu-toolbar divEspaco"></div>
        <asp:LinkButton ID="btVoltar" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false">
            <i class="fas fa-arrow-left"></i>
            <br />
            <span>Voltar</span>
        </asp:LinkButton>
    </div>

    <!-- Modal ----------------------------------------------------------------------------------------------------->

    <!-- Backgrop do modal -->
    <div id="backdropCustom" class="modal-backdrop-custom" style="display: none;"></div>

    <!-- Modal -->
    <div id="chamadoModal" class="modal-custom" style="display: none;">
        <div class="modal-header">
            <h5 class="modal-title">Detalhes do Chamado</h5>
            <button type="button" class="close" onclick="fecharModal()" aria-label="Close">
                <span aria-hidden="true">&times;</span>
            </button>
        </div>
        <div class="modal-body d-flex">
            <!-- Coluna Esquerda -->
            <div class="flex-fill left-column">
                <p id="modalIdChamadoContainer" runat="server">
                    <strong>Id Chamado:</strong> <span id="modalIdChamado" runat="server"></span>
                </p>
                <p id="modalRequestNumberContainer">
                    <strong>Request Number:</strong> <span id="modalRequestNumber"></span>
                </p>
                <p id="modalWorkOrderNumberContainer">
                    <strong>Work Order Number:</strong> <span id="modalWorkOrderNumber"></span>
                </p>
                <p id="modalEstadoContainer">
                    <strong>Estado:</strong> <span id="modalEstado"></span>
                </p>
                <p id="modalUserNameContainer">
                    <strong>Nome do usuário:</strong> <span id="modalUserName"></span>
                </p>
                <p id="modalTransactionIDContainer">
                    <strong>ID da transação:</strong> <span id="modalTransactionID"></span>
                </p>
                <p id="modalTipoSolicitacaoContainer">
                    <strong>Tipo de Solicitação:</strong> <span id="modalTipoSolicitacao" runat="server"></span>
                </p>
                <p id="modalPlanoContratoAtualContainer">
                    <strong>Plano atual:</strong> <span id="modalPlanoContratoAtual"></span>
                </p>
                <div class="divider"></div>
                <p id="modalComentariosContainer">
                    <strong>Comentários:</strong> <span id="modalComentarios"></span>
                </p>
                <div id="camposCondicionaisContainer" runat="server"></div>
            </div>

            <!-- Separador Vertical -->
            <div class="vertical-separator" id="verticalSeparator"></div>

            <!-- Coluna Direita -->
            <div class="flex-fill right-column" id="rightColumn">
                <div class="form-group-row">
                    <div class="form-group">
                        <label for="empresaContratante">Empresa Contratante</label>
                        <asp:DropDownList ID="empresaContratante" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Selecione a Empresa" Value="" />
                        </asp:DropDownList>

                    </div>
                    <div class="form-group">
                        <label for="faturaDropdown">Fatura Agrupadora</label>
                        <select id="faturaDropdown" class="form-control" runat="server">
                            <option value="">Selecione a Fatura</option>
                        </select>
                    </div>
                </div>

                <div class="form-group">
                    <label for="corpoEmail">Corpo do Email</label>
                    <textarea id="corpoEmail" class="form-control" rows="2" runat="server"></textarea>
                </div>

                <div class="form-group">
                    <label for="operadoraEmails">Operadora Emails</label>
                    <input type="text" id="operadoraEmails" class="form-control" placeholder="Selecione emails" onclick="abrirDropdownEmails()" />
                    <div id="emailDropdown" class="dropdown-emails">
                        <!-- Os emails serão preenchidos aqui dinamicamente pelo JavaScript -->
                    </div>
                    <div id="selectedEmails" style="margin-top: 10px;" runat="server"></div>
                </div>


                <div class="form-group">
                    <label for="anexos">Anexos</label>
                    <div class="custom-file" onclick="document.getElementById('anexos').click()">
                        <span id="anexosText">Clique aqui para anexar arquivos</span>
                        <input type="file" id="anexos" class="form-control" multiple onchange="adicionarArquivos()" />
                    </div>
                    <div id="listaAnexos"></div>
                    <div id="loadingSpinner">
                        <img src="spinner.gif" alt="Carregando..." />
                    </div>
                </div>

                <div class="form-group">
                    <label for="emailResponsavelRegional">Email do Responsável na Regional</label>
                    <input type="email" id="emailResponsavelRegional" class="form-control" runat="server"/>
                </div>
            </div>
        </div>

        <div class="modal-footer" id="modalFooter">
            <button type="button" class="btn btn-secondary" onclick="fecharModal()">Fechar</button>
            <asp:Button ID="btnExecutar" runat="server" CssClass="btn btn-primary azul" Text="Executar" OnClick="BtnExecutar_Click" />
        </div>
    </div>







    <!-- FIM CÓDIGOS ADICIONAIS -->
</asp:Content>