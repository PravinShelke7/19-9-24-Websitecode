Imports System.Data
Imports System.Data.OleDb
Imports System
Imports E2GetData
Imports E2UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Econ2_PopUp_CaseSearch
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            hidCaseid.Value = Request.QueryString("ID").ToString()
            If Not IsPostBack Then
                GetCaseDetails()
                If hidCaseid.Value = "ddlPCase" Then
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
        Dim objGetData As New E2GetData.Selectdata()
        Try
            If hidCaseid.Value = "ddlPCase" Then
                'If Session("E2LicAdmin") = "N" Then
                '    If Request.QueryString("GrpID") <> "0" Then
                '        ds = objGetData.GetGroupCases(Session("USERID"), txtCaseDe1.Text.Trim.ToString().Replace("'", "''"), txtCaseDe2.Text.Trim.ToString().Replace("'", "''"), Request.QueryString("GrpID").ToString())
                '    Else
                '        ds = objGetData.GetCases(Session("USERID"), txtCaseDe1.Text.Trim.ToString().Replace("'", "''"), txtCaseDe2.Text.Trim.ToString().Replace("'", "''"))
                '    End If
                'Else
                '    ds = objGetData.GetCasesByLicense(Session("USERID"), txtCaseDe1.Text.Trim.ToString().Replace("'", "''"), txtCaseDe2.Text.Trim.ToString().Replace("'", "''"))
                'End If
                'AdminLicense
                If Request.QueryString("GrpID") <> "0" Then
                    ds = objGetData.GetGroupCases(Session("USERID"), txtCaseDe1.Text.Trim.ToString().Replace("'", "''"), txtCaseDe2.Text.Trim.ToString().Replace("'", "''"), Request.QueryString("GrpID").ToString())
                Else
                    ds = objGetData.GetCases(Session("USERID"), txtCaseDe1.Text.Trim.ToString().Replace("'", "''"), txtCaseDe2.Text.Trim.ToString().Replace("'", "''"))
                End If
                'AdminLicense
            Else
                ds = objGetData.GetBCases(txtCaseDe1.Text.Trim.ToString().Replace("'", "''"), txtCaseDe2.Text.Trim.ToString().Replace("'", "''"))
            End If

            grdCaseSearch.DataSource = ds
            grdCaseSearch.DataBind()

            If ds.Tables(0).Rows.Count > 0 Then
                btnCaseViewer.Visible = True
            Else
                btnCaseViewer.Visible = False
            End If

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetCaseDetails:" + ex.Message.ToString()
        End Try
    End Sub

End Class
