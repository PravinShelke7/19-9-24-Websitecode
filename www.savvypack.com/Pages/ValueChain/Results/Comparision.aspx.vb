Imports System.Data
Imports System.Data.OleDb
Imports System
Imports VChainGetData
Imports VChainUpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_ValueChain_Results_Comparision
    Inherits System.Web.UI.Page
#Region "Get Set Variables"

    Dim _strType As String
    Dim _strChType As String
    Dim _strModName As String
    Dim _strCaseID As String

    Public Property Type() As String
        Get
            Return _strType
        End Get
        Set(ByVal Value As String)
            _strType = Value
        End Set
    End Property
    Public Property ModName() As String
        Get
            Return _strModName
        End Get
        Set(ByVal Value As String)
            _strModName = Value
        End Set
    End Property
    Public Property CaseId() As String
        Get
            Return _strCaseID
        End Get
        Set(ByVal Value As String)
            _strCaseID = Value
        End Set
    End Property
    Public Property ChType() As String
        Get
            Return _strChType
        End Get
        Set(ByVal Value As String)
            _strChType = Value
        End Set
    End Property
   
#End Region
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("Back") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If
            Type = Convert.ToString(Request.QueryString("Type"))
            If Type.Length > 0 Then
                Dim objCryptoHelper As New CryptoHelper()
                Type = objCryptoHelper.Decrypt(Type)
                ModName = objCryptoHelper.Decrypt(Request.QueryString("ModName").ToString())
                CaseId = objCryptoHelper.Decrypt(Request.QueryString("CaseId").ToString())
            End If

            'If Type = "PL" Then
            '    lblTitle.Text = "Econ1 - Profit and Loss Comparison Selector"
            '    Page.Title = "E1-Profit and Loss Comparison Selector"
            'ElseIf Type = "PLDEP" Then
            '    lblTitle.Text = "Econ1 - Profit and Loss With Depreciation Comparison Selector"
            '    Page.Title = "E1-Profit and Loss With Depreciation Comparison Selector"
            'ElseIf Type = "CST" Then
            '    lblTitle.Text = "Econ1 - Cost Comparison Selector"
            '    Page.Title = "E1-Cost Comparison Selector"
            'ElseIf Type = "CSTDEP" Then
            '    lblTitle.Text = "Econ1 - Cost Comparison With Depreciation Selector"
            '    Page.Title = "E1-Cost Comparison With Depreciation Selector"
            'End If

          
        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString() + ""
        End Try
    End Sub
    Protected Sub btnType_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnType.Click
        Dim TypeDes As String = String.Empty
        Dim objCryptoHelper As New CryptoHelper()
        Dim URL As String = String.Empty
        Dim Title As String = String.Empty
        Try
            TypeDes = objCryptoHelper.Encrypt(ddlType.SelectedValue)
            Title = ddlType.SelectedValue
            URL = "CResultspl.aspx?Type=" + TypeDes + "&ModName=" + objCryptoHelper.Encrypt(ModName) + "&CaseId=" + objCryptoHelper.Encrypt(CaseId) + ""

            Page.ClientScript.RegisterStartupScript(Me.GetType(), Type, "OpenNewWindow('" + URL + "','" + Title + "');", True)


        Catch ex As Exception
            _lErrorLble.Text = "Error:btnType_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
End Class
