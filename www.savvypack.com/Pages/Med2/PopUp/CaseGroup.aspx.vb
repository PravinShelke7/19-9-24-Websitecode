Imports System.Data
Imports System.Data.OleDb
Imports System
Imports Med2GetData
Imports Med2UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_MedEcon2_PopUp_CaseGroup
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            hidCaseid.Value = Request.QueryString("ID").ToString()
            hidCaseDes.Value = Request.QueryString("Des").ToString()
            hidCaseidD.Value = Request.QueryString("IdD").ToString()
            If Not IsPostBack Then
                GetCaseDetails()

            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetCaseDetails()
        Dim ds As New DataSet
        Dim objGetData As New Med2GetData.Selectdata()
        Try
            ds = objGetData.GetGroupCaseDet(Session("USERID").ToString())

            grdCaseSearch.DataSource = ds
            grdCaseSearch.DataBind()


        Catch ex As Exception
            _lErrorLble.Text = "Error:GetCaseDetails:" + ex.Message.ToString()
        End Try
    End Sub
End Class
