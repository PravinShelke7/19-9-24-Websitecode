Imports System.Data
Imports System.Data.OleDb
Imports System
Imports SMed2GetData
Imports SMed2UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_MedSustain2_PopUp_LicenseUser
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("Back") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If
            hidCaseid.Value = Request.QueryString("ID").ToString()
            hidCaseDes.Value = Request.QueryString("Des").ToString()

            If Not IsPostBack Then
                GetCaseDetails()

            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetCaseDetails()
        Dim ds As New DataSet
        Dim objGetData As New SMed2GetData.Selectdata()
        Try
            ds = objGetData.GetUserCompanyUsersBem(Session("S2UserName").ToString())
            If ds.Tables(0).Rows.Count > 0 Then
                grdCaseSearch.DataSource = ds
                grdCaseSearch.DataBind()
            End If



        Catch ex As Exception
            _lErrorLble.Text = "Error:GetCaseDetails:" + ex.Message.ToString()
        End Try
    End Sub
End Class
