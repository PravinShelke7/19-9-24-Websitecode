﻿Imports System.Data
Imports System.Data.OleDb
Imports System
Imports MoldE1GetData
Imports MoldE1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_MoldEcon1_Default
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
                If Session("Service") = "COMP" Then
                    E1table.Attributes.Add("class", "E1CompModule")
                    Page.Title = "Econ1-Competitive Module"
                    divMainHeading.InnerHtml = "Econ1-Competitive Module- Global Manager"
                End If

                SetSessions()
                GetBCaseDetails()
                GetPCaseGroupDetails()
                GetPCaseDetails()
                btSerach.Attributes.Add("onclick", "return ShowPopWindow('PopUp/CaseSearch.aspx?Id=ddlPCase&GrpID=" + ddlCaseGroup.SelectedValue + "')")

            End If
            SetToolButton()
        Catch ex As Exception
            lblError.Text = "Error:Page_Load:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub SetSessions()
        Dim objGetData As New MoldE1GetData.Selectdata()
        Dim ds As New DataSet()
        Try
            If Session("Service") = "COMP" Then
                ds = objGetData.GetCompUserDetails(Session("ID"))
            Else
                ds = objGetData.GetUserDetails(Session("ID"))
            End If


            Session("UserId") = ds.Tables(0).Rows(0).Item("USERID").ToString()
            Session("MoldE1UserRole") = ds.Tables(0).Rows(0).Item("USERROLE").ToString()
            Session("MoldE1ServiceRole") = ds.Tables(0).Rows(0).Item("SERVIECROLE").ToString()
            Session("MoldE1UserName") = ds.Tables(0).Rows(0).Item("USERNAME").ToString()
            Session("MoldE1ToolUserName") = ds.Tables(0).Rows(0).Item("TOOLUSERNAME").ToString()
            Session("MoldE1MaxCaseCount") = ds.Tables(0).Rows(0).Item("MAXCASECOUNT").ToString()

            'Set for License Administrator
            Session("MoldE1LicAdmin") = ds.Tables(0).Rows(0).Item("ISIADMINLICUSR").ToString()
            Session("MoldE1LicenseId") = ds.Tables(0).Rows(0).Item("LICENSEID").ToString()

            'Comp Mod
            Session("ServiceId") = ds.Tables(0).Rows(0).Item("SERVICEID").ToString()

        Catch ex As Exception
            lblError.Text = "Error:SetSessions:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetBCaseDetails()
        Dim objGetData As New MoldE1GetData.Selectdata()
        Dim ds As New DataSet()
        Dim dsComp As New DataSet()
        Dim i As Integer
        Dim serviceId As String = ""
        Try
            If Session("Service") = "COMP" Then
                ds = objGetData.GetBCaseCompDetails()
                If ds.Tables(0).Rows.Count > 0 Then
                    dsComp = objGetData.GetServiceCompUser(ds.Tables(0).Rows(0).Item("SERVICEUSERID").ToString())
                    If dsComp.Tables(0).Rows.Count > 0 Then
                        For i = 0 To dsComp.Tables(0).Rows.Count - 1
                            If i = 0 Then
                                serviceId = dsComp.Tables(0).Rows(i).Item("SERVICEID").ToString()
                            Else
                                serviceId = serviceId + "," + dsComp.Tables(0).Rows(i).Item("SERVICEID").ToString()
                            End If
                            If dsComp.Tables(0).Rows(i).Item("SERVICEDE").ToString() = "CompEcon" Then
                                Session("ServiceE1") = dsComp.Tables(0).Rows(i).Item("SERVICEID").ToString()
                            ElseIf dsComp.Tables(0).Rows(i).Item("SERVICEDE").ToString() = "CompSustain" Then
                                Session("ServiceS1") = dsComp.Tables(0).Rows(i).Item("SERVICEID").ToString()
                            End If
                        Next
                        Session("CompServiceId") = serviceId

                    End If
                End If
            Else
                ds = objGetData.GetBCaseDetails()
            End If

            With ddlBaseCases
                .DataSource = ds
                .DataTextField = "CASEDES"
                .DataValueField = "CASEID"
                .DataBind()
                .Font.Size = 8
            End With
        Catch ex As Exception
            lblError.Text = "Error:GetBCaseDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetPCaseDetails()
        Dim objGetData As New MoldE1GetData.Selectdata()
        Dim ds As New DataSet()
        Try
            If ddlCaseGroup.SelectedValue <> "0" Then
                ds = objGetData.GetPCaseDetailsByGroup(Session("UserId").ToString(), ddlCaseGroup.SelectedValue)
            Else
                If Session("MoldE1LicAdmin") = "N" Then
                    If Session("Service") = "COMP" Then
                        ds = objGetData.GetPropCaseDetails(Session("MoldE1UserName").ToString(), Session("ServiceId"))
                    Else
                        ds = objGetData.GetPCaseDetails(Session("UserId").ToString())
                    End If
                Else
                    If Session("Service") = "COMP" Then
                        ds = objGetData.GetCompPCaseDetailsByLicense(Session("MoldE1UserName").ToString(), Session("ServiceId").ToString())
                    Else
                        ds = objGetData.GetPCaseDetailsByLicense(Session("MoldE1UserName").ToString())
                    End If

                End If
            End If

            With ddlPCase
                .DataSource = ds
                .DataTextField = "CASEDES"
                .DataValueField = "CASEID"
                .DataBind()
                .Font.Size = 8
            End With
            If ds.Tables(0).Rows.Count <= 0 Then
                btnPCase.Visible = False
                btRename.Visible = False
                btSerach.Visible = False
                ddlPCase.Visible = False
                If ddlCaseGroup.SelectedValue <> "0" Then
                    lblPCase.Visible = True
                    lblPCase.Text = "You currently have no Proprietary Cases assigned to this Group."
                Else
                    lblPCase.Visible = True
                End If
            Else
                btnPCase.Visible = True
                btRename.Visible = True
                btSerach.Visible = True
                ddlPCase.Visible = True
                lblPCase.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = "Error:GetPCaseDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetPCaseGroupDetails()
        Dim Dts As New DataSet
        Dim objGetData As New MoldE1GetData.Selectdata
        Dim lst As New ListItem

        Try
            lst.Text = "-------------All Groups and All Cases------------"
            lst.Value = "0"
            lst.Selected = True
            ddlCaseGroup.Items.Add(lst)
            ddlCaseGroup.AppendDataBoundItems = True

            If Session("Service") = "COMP" Then
                Dts = objGetData.GetCompGroupCaseDetails(Session("USERID").ToString(), Session("CompServiceId").ToString())
            Else
                Dts = objGetData.GetGroupCaseDetails(Session("USERID").ToString())
            End If

            If Dts.Tables(0).Rows.Count > 0 Then
                ddlCaseGroup.DataSource = Dts
                ddlCaseGroup.DataValueField = "GROUPID"
                ddlCaseGroup.DataTextField = "GROUPDES"
                ddlCaseGroup.DataBind()
            End If

        Catch ex As Exception
            lblError.Text = "Error:GetPCaseGroupDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub SetToolButton()
        Try
            If Session("MoldE1ServiceRole") <> "ReadWrite" Then
                btnToolBox.Enabled = False
            End If
        Catch ex As Exception
            lblError.Text = "Error:SetToolButton:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnBCase_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBCase.Click
        Try
            Session("MoldE1CaseId") = ddlBaseCases.SelectedValue.ToString()
            If Session("Service") = "COMP" Then
                Session("ServiceN") = "BASE"
            End If
            If Not objRefresh.IsRefresh Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinB", "OpenNewWindow('Assumptions/CaseManager.aspx');", True)
            End If
        Catch ex As Exception
            lblError.Text = "Error:btnBCase_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnPCase_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPCase.Click
        Try
            Session("MoldE1CaseId") = ddlPCase.SelectedValue.ToString()
            If Session("Service") = "COMP" Then
                Session("ServiceN") = "PROP"
            End If
            If Not objRefresh.IsRefresh Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "OpenNewWindow('Assumptions/CaseManager.aspx');", True)
            End If
        Catch ex As Exception
            lblError.Text = "Error:btnPCase_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnToolBox_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnToolBox.Click
        Try
            Response.Redirect("Tools/Tool.aspx", True)
        Catch ex As Exception
            lblError.Text = "Error:btnToolBox_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        'Dim objUpIns As New E1UpInsData.UpdateInsert()
        Dim obj As New ToolCCS
        Dim ds As New DataSet()
        Try
            'objUpIns.CaseDesUpdate(txtCaseid.Value, txtCaseDe1.Text.Trim().ToString().Replace("'", "''"), txtCaseDe2.Text.Trim().ToString().Replace("'", "''"))
            'GetPCaseDetails()
            obj.CaseRename(txtCaseid.Value, txtCaseDe1.Text.Trim().ToString().Replace("'", "''"), txtCaseDe2.Text.Trim().ToString().Replace("'", "''"), txtCaseDe3.Text.Trim.ToString().Replace("'", "''"), "MoldE1ConnectionString")
            GetPCaseDetails()
            divRename.Style.Add("Display", "none")
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Case#" + txtCaseid.Value.ToString() + " updated successfully');", True)
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            divRename.Style.Add("Display", "none")
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub ddlCaseGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCaseGroup.SelectedIndexChanged
        Try
            btSerach.Attributes.Add("onclick", "return ShowPopWindow('PopUp/CaseSearch.aspx?Id=ddlPCase&GrpID=" + ddlCaseGroup.SelectedValue + "')")
            GetPCaseDetails()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub btRename_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btRename.Click
        Try
            divRename.Style.Add("Display", "block")
            GetCaseDetails()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub GetCaseDetails()
        Dim objGetData As New MoldE1GetData.Selectdata()
        Dim ds As New DataSet
        Dim CaseId As New Integer
        Try
            CaseId = CInt(ddlPCase.SelectedValue)
            ds = objGetData.GetCaseDetails(CaseId.ToString())
            txtCaseDe1.Text = ds.Tables(0).Rows(0).Item("CASEDE1").ToString()
            txtCaseDe2.Text = ds.Tables(0).Rows(0).Item("CASEDE2").ToString()
            txtCaseDe3.Text = ds.Tables(0).Rows(0).Item("CASEDE3").ToString()


        Catch ex As Exception
            lblError.Text = "Error:GetCaseDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub
End Class
