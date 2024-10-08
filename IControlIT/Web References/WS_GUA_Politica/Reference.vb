﻿'------------------------------------------------------------------------------
' <auto-generated>
'     O código foi gerado por uma ferramenta.
'     Versão de Tempo de Execução:4.0.30319.42000
'
'     As alterações ao arquivo poderão causar comportamento incorreto e serão perdidas se
'     o código for gerado novamente.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports System
Imports System.ComponentModel
Imports System.Data
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization

'
'Este código-fonte foi gerado automaticamente por Microsoft.VSDesigner, Versão 4.0.30319.42000.
'
Namespace WS_GUA_Politica
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Web.Services.WebServiceBindingAttribute(Name:="WSPoliticaSoap", [Namespace]:="http://tempuri.org/")>  _
    Partial Public Class WSPolitica
        Inherits System.Web.Services.Protocols.SoapHttpClientProtocol
        
        Private Politica_ConsumidorOperationCompleted As System.Threading.SendOrPostCallback
        
        Private Troca_HierarquiaOperationCompleted As System.Threading.SendOrPostCallback
        
        Private useDefaultCredentialsSetExplicitly As Boolean
        
        '''<remarks/>
        Public Sub New()
            MyBase.New
            Me.Url = Global.IControlIT.My.MySettings.Default.IControlIT_WS_GUA_Politica_WSPolitica
            If (Me.IsLocalFileSystemWebService(Me.Url) = true) Then
                Me.UseDefaultCredentials = true
                Me.useDefaultCredentialsSetExplicitly = false
            Else
                Me.useDefaultCredentialsSetExplicitly = true
            End If
        End Sub
        
        Public Shadows Property Url() As String
            Get
                Return MyBase.Url
            End Get
            Set
                If (((Me.IsLocalFileSystemWebService(MyBase.Url) = true)  _
                            AndAlso (Me.useDefaultCredentialsSetExplicitly = false))  _
                            AndAlso (Me.IsLocalFileSystemWebService(value) = false)) Then
                    MyBase.UseDefaultCredentials = false
                End If
                MyBase.Url = value
            End Set
        End Property
        
        Public Shadows Property UseDefaultCredentials() As Boolean
            Get
                Return MyBase.UseDefaultCredentials
            End Get
            Set
                MyBase.UseDefaultCredentials = value
                Me.useDefaultCredentialsSetExplicitly = true
            End Set
        End Property
        
        '''<remarks/>
        Public Event Politica_ConsumidorCompleted As Politica_ConsumidorCompletedEventHandler
        
        '''<remarks/>
        Public Event Troca_HierarquiaCompleted As Troca_HierarquiaCompletedEventHandler
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Politica_Consumidor", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function Politica_Consumidor(ByVal pPConn_Banco As String, ByVal pId_Consumidor As Integer, ByVal pId_Ativo_Tipo As Integer, ByVal pNm_Consumidor As String, ByVal pValor_Politica As Double, ByVal pMarca_Ligacao As Integer, ByVal pId_Lote As Double, ByVal pId_Usuario_Permissao As Integer, ByVal pPakage As String, ByVal pRetorno As Boolean) As System.Data.DataSet
            Dim results() As Object = Me.Invoke("Politica_Consumidor", New Object() {pPConn_Banco, pId_Consumidor, pId_Ativo_Tipo, pNm_Consumidor, pValor_Politica, pMarca_Ligacao, pId_Lote, pId_Usuario_Permissao, pPakage, pRetorno})
            Return CType(results(0),System.Data.DataSet)
        End Function
        
        '''<remarks/>
        Public Overloads Sub Politica_ConsumidorAsync(ByVal pPConn_Banco As String, ByVal pId_Consumidor As Integer, ByVal pId_Ativo_Tipo As Integer, ByVal pNm_Consumidor As String, ByVal pValor_Politica As Double, ByVal pMarca_Ligacao As Integer, ByVal pId_Lote As Double, ByVal pId_Usuario_Permissao As Integer, ByVal pPakage As String, ByVal pRetorno As Boolean)
            Me.Politica_ConsumidorAsync(pPConn_Banco, pId_Consumidor, pId_Ativo_Tipo, pNm_Consumidor, pValor_Politica, pMarca_Ligacao, pId_Lote, pId_Usuario_Permissao, pPakage, pRetorno, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub Politica_ConsumidorAsync(ByVal pPConn_Banco As String, ByVal pId_Consumidor As Integer, ByVal pId_Ativo_Tipo As Integer, ByVal pNm_Consumidor As String, ByVal pValor_Politica As Double, ByVal pMarca_Ligacao As Integer, ByVal pId_Lote As Double, ByVal pId_Usuario_Permissao As Integer, ByVal pPakage As String, ByVal pRetorno As Boolean, ByVal userState As Object)
            If (Me.Politica_ConsumidorOperationCompleted Is Nothing) Then
                Me.Politica_ConsumidorOperationCompleted = AddressOf Me.OnPolitica_ConsumidorOperationCompleted
            End If
            Me.InvokeAsync("Politica_Consumidor", New Object() {pPConn_Banco, pId_Consumidor, pId_Ativo_Tipo, pNm_Consumidor, pValor_Politica, pMarca_Ligacao, pId_Lote, pId_Usuario_Permissao, pPakage, pRetorno}, Me.Politica_ConsumidorOperationCompleted, userState)
        End Sub
        
        Private Sub OnPolitica_ConsumidorOperationCompleted(ByVal arg As Object)
            If (Not (Me.Politica_ConsumidorCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent Politica_ConsumidorCompleted(Me, New Politica_ConsumidorCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Troca_Hierarquia", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function Troca_Hierarquia(ByVal pPConn_Banco As String, ByVal pId_Filial As Integer, ByVal pId_Centro_Custo As Integer, ByVal pId_Departamento As Integer, ByVal pId_Setor As Integer, ByVal pId_Secao As Integer, ByVal pId_Usuario_Permissao As Integer, ByVal pPakage As String) As System.Data.DataSet
            Dim results() As Object = Me.Invoke("Troca_Hierarquia", New Object() {pPConn_Banco, pId_Filial, pId_Centro_Custo, pId_Departamento, pId_Setor, pId_Secao, pId_Usuario_Permissao, pPakage})
            Return CType(results(0),System.Data.DataSet)
        End Function
        
        '''<remarks/>
        Public Overloads Sub Troca_HierarquiaAsync(ByVal pPConn_Banco As String, ByVal pId_Filial As Integer, ByVal pId_Centro_Custo As Integer, ByVal pId_Departamento As Integer, ByVal pId_Setor As Integer, ByVal pId_Secao As Integer, ByVal pId_Usuario_Permissao As Integer, ByVal pPakage As String)
            Me.Troca_HierarquiaAsync(pPConn_Banco, pId_Filial, pId_Centro_Custo, pId_Departamento, pId_Setor, pId_Secao, pId_Usuario_Permissao, pPakage, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub Troca_HierarquiaAsync(ByVal pPConn_Banco As String, ByVal pId_Filial As Integer, ByVal pId_Centro_Custo As Integer, ByVal pId_Departamento As Integer, ByVal pId_Setor As Integer, ByVal pId_Secao As Integer, ByVal pId_Usuario_Permissao As Integer, ByVal pPakage As String, ByVal userState As Object)
            If (Me.Troca_HierarquiaOperationCompleted Is Nothing) Then
                Me.Troca_HierarquiaOperationCompleted = AddressOf Me.OnTroca_HierarquiaOperationCompleted
            End If
            Me.InvokeAsync("Troca_Hierarquia", New Object() {pPConn_Banco, pId_Filial, pId_Centro_Custo, pId_Departamento, pId_Setor, pId_Secao, pId_Usuario_Permissao, pPakage}, Me.Troca_HierarquiaOperationCompleted, userState)
        End Sub
        
        Private Sub OnTroca_HierarquiaOperationCompleted(ByVal arg As Object)
            If (Not (Me.Troca_HierarquiaCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent Troca_HierarquiaCompleted(Me, New Troca_HierarquiaCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        Public Shadows Sub CancelAsync(ByVal userState As Object)
            MyBase.CancelAsync(userState)
        End Sub
        
        Private Function IsLocalFileSystemWebService(ByVal url As String) As Boolean
            If ((url Is Nothing)  _
                        OrElse (url Is String.Empty)) Then
                Return false
            End If
            Dim wsUri As System.Uri = New System.Uri(url)
            If ((wsUri.Port >= 1024)  _
                        AndAlso (String.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) = 0)) Then
                Return true
            End If
            Return false
        End Function
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")>  _
    Public Delegate Sub Politica_ConsumidorCompletedEventHandler(ByVal sender As Object, ByVal e As Politica_ConsumidorCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class Politica_ConsumidorCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As System.Data.DataSet
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),System.Data.DataSet)
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")>  _
    Public Delegate Sub Troca_HierarquiaCompletedEventHandler(ByVal sender As Object, ByVal e As Troca_HierarquiaCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class Troca_HierarquiaCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As System.Data.DataSet
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),System.Data.DataSet)
            End Get
        End Property
    End Class
End Namespace
