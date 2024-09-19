Imports System.Data
Imports System.Data.OleDb
Imports System
Imports MoldE2GetData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_MoldEcon2_PopUp_GetE1CasePopup
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            hidCasedes.Value = Request.QueryString("Des").ToString()
            hidCaseid.Value = Request.QueryString("ID").ToString()
            If Not IsPostBack Then
                GetCaseDetails()
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub GetCaseDetails()
        Dim ds As New DataSet
        Dim objGetData As New MoldE2GetData.Selectdata()
        Try

            ds = objGetData.GetEcon1Cases(-1, Session("MoldE2CaseId").ToString(), txtCaseDe1.Text.Trim.ToString(), txtCaseDe2.Text.Trim.ToString(), Session("UserId").ToString())
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
