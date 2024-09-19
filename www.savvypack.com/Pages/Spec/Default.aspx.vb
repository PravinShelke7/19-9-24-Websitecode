Imports System.Data
Imports System.Data.OleDb
Imports System
Imports SpecGetData
Imports SpecUpdateData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter

Partial Class Pages_Spec_Default2
    Inherits System.Web.UI.Page

#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_PAGES_ECON1_DEFAULT")
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        objRefresh.Render(Page)
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Try
                lblTag.Text = "Copyright 1997 - " + Now.Year().ToString() + " SavvyPack Corporation"
            Catch ex As Exception
            End Try
            If Session("Back") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If

            If Not IsPostBack Then

            End If
        Catch ex As Exception
            lblError.Text = "Error:Page_Load:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetRFPDetails()
        Dim objGetData As New SpecGetData.Selectdata()
        Dim ds As New DataSet
        Dim CaseId As New Integer
        Try
            CaseId = CInt(hidRFP.Value)
            ds = objGetData.GetRFPDetails(CaseId.ToString())
            txtCaseDe1.Text = ds.Tables(0).Rows(0).Item("DES1").ToString()
            txtCaseDe2.Text = ds.Tables(0).Rows(0).Item("DES2").ToString()
            txtCaseDe3.Text = ds.Tables(0).Rows(0).Item("DES3").ToString()

        Catch ex As Exception
            lblError.Text = "Error:GetRFPDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnCreate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreate.Click
        Try
            divCreate.Visible = True
        Catch ex As Exception
            lblError.Text = "Error:btnCreate_Click:" + ex.Message.ToString() + ""
        End Try

    End Sub

    Protected Sub btnStart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnStart.Click
        Try

            

        Catch ex As Exception
            lblError.Text = "Error:btnStart_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnCreateR_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreateR.Click
        Dim CaseId As Integer
        Dim obj As New SpecUpdateData.UpdateInsert()
        Dim Des1 As String
        Dim Des2 As String
        Dim Des3 As String
        Try
            If txtDes1.Text = "" Or txtDes2.Text = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Please enter RFP Des1 and Des2');", True)
            Else
                Des1 = txtDes1.Text
                Des2 = txtDes2.Text
                Des3 = txtDes3.Text

                CaseId = obj.CreateRFP(Session("UserName"), Des1, Des2, Des3)
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('RFP #" + CaseId.ToString() + " created successfully');", True)
                divCreate.Visible = False
                txtDes1.Text = " "
                txtDes2.Text = ""
                txtDes3.Text = ""
            End If

        Catch ex As Exception
            lblError.Text = "Error:btnCreateR_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnCreateC_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreateC.Click
        Try
            divCreate.Visible = False
        Catch ex As Exception
            lblError.Text = "Error:btnCreateC_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btRename_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btRename.Click
        If hidRFPD.Value = "" Or hidRFPD.Value = "Select RFP" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Show", "alert('Please select RFP')", True)
        Else
            divModify.Visible = True
            lnkRFPs.Text = hidRFPD.Value
            GetRFPDetails()
        End If
    End Sub

    Protected Sub btnRenameR_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRenameR.Click
        Dim obj As New SpecUpdateData.UpdateInsert()
        Dim CaseId As String = String.Empty
        Try
            CaseId = hidRFP.Value
            obj.RFPRename(CaseId, txtCaseDe1.Text.Trim.ToString().Replace("'", "''"), txtCaseDe2.Text.Trim.ToString().Replace("'", "''"), txtCaseDe3.Text.Trim.ToString().Replace("'", "''"))
            GetRFPDetails()
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('RFP#" + CaseId.ToString() + " updated successfully');", True)
            hidRFP.Value = "0"
            hidRFPD.Value = "Select RFP"
            lnkRFPs.Text = "Select RFP"
            divModify.Visible = False
        Catch ex As Exception
            lblError.Text = "Error:btnRenameR_Click1:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnRenameC_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRenameC.Click
        Try
            divModify.Visible = False
            hidRFP.Value = "0"
            hidRFPD.Value = "Select RFP"
            lnkRFPs.Text = "Select RFP"
        Catch ex As Exception
            lblError.Text = "Error:btnRenameC_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
End Class
