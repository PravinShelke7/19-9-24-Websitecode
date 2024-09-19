Imports System.Data
Imports System.Data.OleDb
Imports System
Imports S1GetData
Imports S1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Imports ToolCCS
Partial Class Pages_Sustain1_Tools_AdminTool
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            lblTag.Text = "Copyright 1997 - " + Now.Year().ToString() + " Savvypack Corporation."
            If Session("Back") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If

            If Not IsPostBack Then
                GetBCaseDetails()
            End If
        Catch ex As Exception
            lblError.Text = "Error:Page_Load:" + ex.Message.ToString() + ""
        End Try
    End Sub



    Protected Sub GetBCaseDetails()
        Dim objGetData As New S1GetData.Selectdata()
        Dim ds As New DataSet()
        Try
            ds = objGetData.GetBCaseDetails()
            With ddlPCases
                .DataSource = ds
                .DataTextField = "CASEDES"
                .DataValueField = "CASEID"
                .DataBind()
                .Font.Size = 8
            End With

            With ddlTarget1
                .DataSource = ds
                .DataTextField = "CASEDES"
                .DataValueField = "CASEID"
                .DataBind()
                .Font.Size = 8
            End With

            If ds.Tables(0).Rows.Count <= 0 Then
                btnCopyPcase.Visible = False
                btnPTransfer.Visible = False
                btnSynchronize.Visible = False
                btnPRename.Visible = False
                btnDelete.Visible = False
                ddlPCases.Visible = False
                lblPCase.Visible = True

            Else
                btnCopyPcase.Visible = True
                btnPTransfer.Visible = True
                btnSynchronize.Visible = True
                btnPRename.Visible = True
                btnDelete.Visible = True
                ddlPCases.Visible = True
                lblPCase.Visible = False

            End If
        Catch ex As Exception
            lblError.Text = "Error:GetPCaseDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub


    Protected Sub GetCaseDetails()
        Dim objGetData As New S1GetData.Selectdata()
        Dim ds As New DataSet
        Dim CaseId As New Integer
        Try
            CaseId = CInt(ddlPCases.SelectedValue)
            ds = objGetData.GetCaseDetails(CaseId.ToString())
            txtCaseDe1.Text = ds.Tables(0).Rows(0).Item("CASEDE1").ToString()
            txtCaseDe2.Text = ds.Tables(0).Rows(0).Item("CASEDE2").ToString()
            txtCaseDe3.Text = ds.Tables(0).Rows(0).Item("CASEDE3").ToString()


        Catch ex As Exception
            lblError.Text = "Error:GetCaseDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub





#Region "Copy A Case"

    Protected Sub btnCopyPcase_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCopyPcase.Click
        Dim SCaseId As String = String.Empty
        Dim TCaseId As String = String.Empty
        Dim Flag As Boolean
        Try
            SCaseId = ddlPCases.SelectedValue.ToString()
            TCaseId = CreateCase()
            Flag = CopyCase(SCaseId, TCaseId, "EconConnectionString")
            Flag = CopyCase(SCaseId, TCaseId, "Sustain1ConnectionString")
            If Flag Then
                GetBCaseDetails()
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Case #" + TCaseId.ToString() + " created successfully.\nCase #" + SCaseId.ToString() + " variables transferred to Case #" + TCaseId.ToString() + " successfully.');", True)
            End If
        Catch ex As Exception
            lblError.Text = "Error:btnCopyPcase_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
#End Region

#Region "Transfer A Case"
    Protected Sub btnPS2T_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPS2T.Click
        Dim SCaseId As String = String.Empty
        Dim TCaseId As String = String.Empty
        Dim Flag As Boolean

        Try
            SCaseId = ddlPCases.SelectedValue.ToString()
            TCaseId = ddlTarget1.SelectedValue.ToString()
            Flag = CopyCase(SCaseId, TCaseId, "Sustain1ConnectionString")
            If Flag Then
                GetBCaseDetails()
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Case #" + SCaseId.ToString() + " variables transferred to Case #" + TCaseId.ToString() + " successfully.');", True)
            End If
        Catch ex As Exception
            lblError.Text = "Error:btnPS2T_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
#End Region


#Region "Synchronize"
    Protected Sub btnSynchronize_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSynchronize.Click
        Dim obj As New ToolCCS
        Dim Flag As Boolean
        Dim SCaseId As String = String.Empty
        Dim TCaseId As String = String.Empty
        Try

            SCaseId = ddlPCases.SelectedValue.ToString()
            TCaseId = ddlPCases.SelectedValue.ToString()
            obj.BaseCaseSynch(SCaseId, TCaseId, "ECON", "SUSTAIN", "Sustain1ConnectionString", "SystemConnectionString")
            Flag = True
        Catch ex As Exception
            Flag = False
            lblError.Text = "Error:btnSynchronize_Click:" + ex.Message.ToString() + ""
        End Try
        If Flag Then
            GetBCaseDetails()
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('You have suceessfully synchronized Case #" + SCaseId.ToString() + " from Sustain1 to Econ1');", True)
        End If
    End Sub
#End Region

#Region "Rename Case"
    Protected Sub btnPRename_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPRename.Click
        Try
            divModify.Visible = True
            GetCaseDetails()
        Catch ex As Exception
            lblError.Text = "Error:btnPRename_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnRenameC_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRenameC.Click
        Try
            divModify.Visible = False
        Catch ex As Exception
            lblError.Text = "Error:btnRenameC_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnRename_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRename.Click
        Dim obj As New ToolCCS
        Dim CaseId As String = String.Empty
        Try
            CaseId = ddlPCases.SelectedValue.ToString()
            obj.CaseRenameBase(ddlPCases.SelectedValue.ToString(), txtCaseDe1.Text.Trim.ToString().Replace("'", "''"), txtCaseDe2.Text.Trim.ToString().Replace("'", "''"), txtCaseDe3.Text.Trim.ToString().Replace("'", "''"), "Sustain1ConnectionString")
            GetBCaseDetails()
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Case#" + CaseId.ToString() + " updated successfully');", True)
            divModify.Visible = False
        Catch ex As Exception
            lblError.Text = "Error:btnRename_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
#End Region

#Region "Delete Case"
    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim obj As New ToolCCS
        Dim CaseId As New Integer
        Try
            CaseId = CInt(ddlPCases.SelectedValue.ToString())
            obj.CaseBaseDelete(Session("USERID").ToString(), CaseId.ToString(), "EconConnectionString")
            obj.CaseBaseDelete(Session("USERID").ToString(), CaseId.ToString(), "Sustain1ConnectionString")
            GetBCaseDetails()
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Case#" + CaseId.ToString() + " deleted successfully');", True)
        Catch ex As Exception
            lblError.Text = "Error:btnDelete_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
#End Region

#Region "Create Case"
    Protected Sub btnCreate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreate.Click
        Try
            Dim CaseId As New Integer
            CaseId = CreateCase()
            GetBCaseDetails()
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Case #" + CaseId.ToString() + " created successfully');", True)

        Catch ex As Exception
            lblError.Text = "Error:btnCreate_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub


#End Region

#Region "Tools Function"
    Protected Function CreateCase() As Integer
        Dim CaseId As Integer
        Dim obj As New ToolCCS
        Try
            CaseId = obj.CreateBaseCase("EconConnectionString", "E1", 0)
            obj.CreateBaseCase("Sustain1ConnectionString", "E1", CaseId)
            Return CaseId
        Catch ex As Exception
            lblError.Text = "Error:CreatedCase:" + ex.Message.ToString() + ""
        End Try
    End Function

    Protected Function CopyCase(ByVal SCaseID As String, ByVal TCaseId As String, ByVal Schema As String) As Boolean
        Dim obj As New ToolCCS
        Try
            obj.CopyBaseCase(SCaseID, TCaseId, Schema)
            Return True
        Catch ex As Exception
            lblError.Text = "Error:CopyCase:" + ex.Message.ToString() + ""
            Return False
        End Try
    End Function
#End Region

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        Try
            hidBackcheck.Value = "0"
        Catch ex As Exception

        End Try
    End Sub
End Class
