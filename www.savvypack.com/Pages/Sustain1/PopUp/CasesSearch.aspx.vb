Imports System.Data
Imports System.Data.OleDb
Imports System
Imports S1GetData
Imports S1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Sustain1_PopUp_CasesSearch
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
	    If Session("Back") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If
            hidCaseid.Value = Request.QueryString("ID").ToString()
            hidCaseDes.Value = Request.QueryString("Des").ToString()
            hidCaseidD.Value = Request.QueryString("IdD").ToString()
            If Not IsPostBack Then
                GetCaseDetails()
                If hidCaseid.Value = "hidPropCase" Then
                    btnCaseViewer.Attributes.Add("onclick", "return CaseViewer('" + Request.QueryString("GrpID").ToString() + "')")
                Else
                    btnCaseViewer.Attributes.Add("onclick", "return CaseViewer('0')")
                End If

            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            GetCaseDetails()
        Catch ex As Exception
            _lErrorLble.Text = "Error:btnSearch_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetCaseDetails()
        Dim ds As New DataSet
        Dim objGetData As New S1GetData.Selectdata()
        Try
            If hidCaseid.Value = "hidPropCase" Or hidCaseid.Value = "hidTargetApp" Or hidCaseid.Value = "hidTargetProp" Then
                If Session("S1LicAdmin") = "N" Then
                    If Request.QueryString("GrpID") <> "0" Then
                        ds = objGetData.GetGroupPCases(Session("USERID"), txtCaseDe1.Text.Trim.ToString().Replace("'", "''"), txtCaseDe2.Text.Trim.ToString().Replace("'", "''"), Request.QueryString("GrpID").ToString())
                    Else
                        ds = objGetData.GetPropCases(Session("USERID"), txtCaseDe1.Text.Trim.ToString().Replace("'", "''"), txtCaseDe2.Text.Trim.ToString().Replace("'", "''"))
                    End If

                Else
                    ds = objGetData.GetPropCasesByLicense(Session("USERID"), txtCaseDe1.Text.Trim.ToString().Replace("'", "''"), txtCaseDe2.Text.Trim.ToString().Replace("'", "''"))
                End If
            Else
                If Session("S1LicAdmin") = "N" Then
                    ds = objGetData.GetApprovedCases(Session("USERID"), txtCaseDe1.Text.Trim.ToString().Replace("'", "''"), txtCaseDe2.Text.Trim.ToString().Replace("'", "''"))
                Else
                    ds = objGetData.GetAppCasesByLicense(Session("USERID"), txtCaseDe1.Text.Trim.ToString().Replace("'", "''"), txtCaseDe2.Text.Trim.ToString().Replace("'", "''"))
                End If

            End If

            grdCaseSearch.DataSource = ds
            grdCaseSearch.DataBind()

            If ds.Tables(0).Rows.Count > 1 Then
                btnCaseViewer.Visible = True
            Else
                btnCaseViewer.Visible = False
            End If

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetCaseDetails:" + ex.Message.ToString()
        End Try
    End Sub
End Class
