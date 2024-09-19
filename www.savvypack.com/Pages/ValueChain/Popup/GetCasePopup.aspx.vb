Imports System.Data
Imports System.Data.OleDb
Imports System
Imports VChainGetData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_ValueChain_Popup_GetCasePopup
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            hidCasedes.Value = Request.QueryString("Des").ToString()
            hidCaseid.Value = Request.QueryString("ID").ToString()
            If Request.QueryString("MODID") Is Nothing Then
                hidModId.Value = ""
            Else
                hidModId.Value = Request.QueryString("MODID").ToString()
            End If

            If Not IsPostBack Then
                GetCaseDetails()
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub GetCaseDetails()
        Dim ds As New DataSet
        Dim objGetData As New VChainGetData.Selectdata()
        Try
            If hidModId.Value = "" Then
                ds = objGetData.GetCases(-1, Request.QueryString("CaseId").ToString(), txtCaseDe1.Text.Trim.ToString(), txtCaseDe2.Text.Trim.ToString(), Session("VChainUserName").ToString(), Request.QueryString("Schm").ToString())
            Else
                ds = objGetData.GetModuleCases(-1, Request.QueryString("CaseId").ToString(), txtCaseDe1.Text.Trim.ToString(), txtCaseDe2.Text.Trim.ToString(), Session("VChainUserName").ToString(), hidModId.Value)
            End If

            grdCases.DataSource = ds
            grdCases.DataBind()
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetCaseDetails:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            GetCaseDetails()
        Catch ex As Exception
            _lErrorLble.Text = "Error:btnSearch_Click:" + ex.Message.ToString()
        End Try
    End Sub
End Class
