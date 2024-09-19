Imports System.Data
Imports System.Data.OleDb
Imports System
Imports LoginGetData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Imports ToolCCS
Imports LogFiles
Partial Class Universal_loginN_Pages_UniversalM
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim obj As New CryptoHelper
        Try
            Try
                lblTag.Text = "Copyright 1997 - " + Now.Year().ToString() + " SavvyPack Corporation"
            Catch ex As Exception
            End Try
            If Session("Back") = Nothing Then
                Response.Redirect("ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If
            If Not IsPostBack Then
                If Session("LicenseNo") <> "0" Then
                    GetPermissionDetails()
                    GetModuleDetails()
                    GetKnowledgeBaseDetails()
                    GetReportDetails()
                    GetSubscriptionDetails()
                    GetMessageCount()
                    GetModType()
                Else
                    Response.Redirect("ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE114").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                End If

            End If
        Catch ex As Exception
            lblError.Text = "Error:Page_Load:" + ex.Message.ToString() + ""
        End Try
    End Sub
    Protected Sub GetMessageCount()
        Dim objGetData As New LoginGetData.Selectdata()
        Try
            lnkAnnouncement.Text = "Announcements (" + objGetData.GetMessagesCnt(Session("UserId").ToString()).ToString() + ")"
        Catch ex As Exception
            lblError.Text = "Error:GetMessageCount:" + ex.Message.ToString() + ""
        End Try
    End Sub
    Protected Sub GetModType()
        Dim objGetData As New E1GetData.Selectdata()
        Dim ds As New DataSet()
        Try
            ds = objGetData.GetModuleType(Session("USERNAME").ToString())
            If ds.Tables(0).Rows.Count > 0 Then
                Session("SavvyModType") = ds.Tables(0).Rows(0).Item("MODELTID").ToString()
            End If
        Catch ex As Exception
            lblError.Text = "Error:GetMessageCount:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Public Sub GetPermissionDetails()
        Dim objGetData As New LoginGetData.Selectdata()
        Dim dsPer As New DataSet()
        Dim obj As New CryptoHelper
        Dim i As Integer
        dsPer = objGetData.ValidateUserPermissions(Session("UserId").ToString())
        If dsPer.Tables(0).Rows.Count > 0 Then

            For i = 0 To dsPer.Tables(0).Rows.Count - 1
                If dsPer.Tables(0).Rows(i).Item("ServiceType").ToString() = "1" Then
                    hvPermission1.Value = 1
                ElseIf dsPer.Tables(0).Rows(i).Item("ServiceType").ToString() = "2" Then
                    hvPermission2.Value = 1
                ElseIf dsPer.Tables(0).Rows(i).Item("ServiceType").ToString() = "3" Then
                    hvPermission3.Value = 1
                End If
            Next

        Else
            Response.Redirect("ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE114").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
        End If
    End Sub
    Public Sub GetModuleDetails()
        Dim objGetData As New LoginGetData.Selectdata()
        Dim ds As New DataSet()
        Try
            If hvPermission1.Value = "1" Then
                dvPermission1.Style.Add("Display", "inline")
                ds = objGetData.GetModuleDetails(Session("UserId").ToString(), "")
                If ds.Tables(0).Rows.Count > 0 Then
                    With ddlModule
                        .DataSource = ds
                        .DataTextField = "SERVICENAME"
                        .DataValueField = "TID"
                        .DataBind()
                        .Font.Size = 8
                    End With
                Else
                    dvPermission1.Style.Add("Display", "none")
                End If
            Else
                dvPermission1.Style.Add("Display", "none")
            End If


        Catch ex As Exception
            lblError.Text = "GetData:GetModuleDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub
    Public Sub GetKnowledgeBaseDetails()
        Dim objGetData As New LoginGetData.Selectdata()
        Dim ds As New DataSet()
        Try
            If hvPermission2.Value = "1" Then
                dvPermission2.Style.Add("Display", "inline")
                ds = objGetData.GetKnowledgeBaseDetails(Session("UserId").ToString(), "")
                If ds.Tables(0).Rows.Count > 0 Then
                    With ddlKBase
                        .DataSource = ds
                        .DataTextField = "SERVICENAME"
                        .DataValueField = "TID"
                        .DataBind()
                        .Font.Size = 8
                    End With
                End If
            Else
                dvPermission2.Style.Add("Display", "none")
            End If
        Catch ex As Exception
            lblError.Text = "GetData:GetKnowledgeBaseDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub
    Public Sub GetReportDetails()
        Dim objGetData As New LoginGetData.Selectdata()
        Dim ds As New DataSet()
        Try
            If hvPermission3.Value = "1" Then
                dvPermission3.Style.Add("Display", "inline")
                ds = objGetData.GetReportDetails(Session("UserId").ToString())
                If ds.Tables(0).Rows.Count > 0 Then
                    With ddlReports
                        .DataSource = ds
                        .DataTextField = "SERVICENAME"
                        .DataValueField = "TID"
                        .DataBind()
                        .Font.Size = 8
                    End With
                End If
            Else
                dvPermission3.Style.Add("Display", "none")
            End If

        Catch ex As Exception
            lblError.Text = "GetData:GetReportDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub imgAnnouncement_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkAnnouncement.Click
        Try
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinB", "OpenNewWindow('../../Wizard/Default.aspx?UserId=" + Session("UserId").ToString() + "&Type=MSG');", True)

        Catch ex As Exception
            lblError.Text = "Error:btnBCase_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub imgLogoff_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgLogoff.Click
        Dim objUpdate As New LoginUpdateData.Selectdata
        If Session("TID") <> Nothing Then
           
            If Session("LogInCount") <> Nothing Then
                'objUpdate.UpdateLogOffDetails2(Session("TID"), Session.SessionID, Session("UserId"), Session("LogInCount").ToString())
            Else
                'objUpdate.UpdateLogOffDetails2(Session("TID"), Session.SessionID, Session("UserId"), "")

            End If
        End If
        'Session.Abandon()
        'Session.RemoveAll()
        Response.Redirect("~/Index.aspx", True)
    End Sub

    Protected Sub btnSAnalysis_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSAnalysis.Click
        Dim objGetData As New LoginGetData.Selectdata()
        Dim objUpdateData As New LoginUpdateData.Selectdata()
        Dim obj As New CryptoHelper
        Dim ds As New DataSet()
        Dim dsLicenseUser As New DataSet()
        Try
            If ddlModule.SelectedItem.Value = "1" Then
                Session("TID") = ddlModule.SelectedItem.Value
                Session("Service") = Nothing
                ModuleRedirection("EconConnectionString", "E1")


            ElseIf ddlModule.SelectedItem.Value = "2" Then
                Session("TID") = ddlModule.SelectedItem.Value
                ModuleRedirection("Econ2ConnectionString", "E2")

            ElseIf ddlModule.SelectedItem.Value = "3" Then
                Session("TID") = ddlModule.SelectedItem.Value
                ModuleRedirection("Econ3ConnectionString", "E3")

            ElseIf ddlModule.SelectedItem.Value = "4" Then
                Session("TID") = ddlModule.SelectedItem.Value
                ds = objGetData.GetInuseDetails(Session("UserId").ToString(), "EconConnectionString")
                If CInt(ds.Tables(0).Rows(0).Item("count")) > 0 Then
                    'Response.Redirect("ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE115").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                    Response.Redirect("ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE115").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&Schema=" + obj.Encrypt("EconConnectionString").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!"))
                Else
                    dsLicenseUser = objGetData.ValidateLicenseUser(Session("USERID").ToString(), "EconConnectionString", "Econ")
                    If dsLicenseUser.Tables(0).Rows.Count > 0 Then
                        'Inserting Data in Inuse table
                        objUpdateData.InuseLoginInsert(Session.SessionID, Session("USERID").ToString(), Session("LicenseNo").ToString().Replace("'", "''"), "EconConnectionString")
                        Response.Redirect("~/Session.aspx?ID=Chart&UserName='" + Session("UserName").ToString() + "'", True)
                    Else
                        Response.Redirect("EconLicenseAgreement.aspx")
                    End If
                End If

            ElseIf ddlModule.SelectedItem.Value = "5" Then
                Session("Service") = Nothing
                Session("TID") = ddlModule.SelectedItem.Value
                ModuleRedirection("Sustain1ConnectionString", "S1")

            ElseIf ddlModule.SelectedItem.Value = "6" Then
                Session("TID") = ddlModule.SelectedItem.Value
                ModuleRedirection("Sustain2ConnectionString", "S2")

            ElseIf ddlModule.SelectedItem.Value = "7" Then
                Session("TID") = ddlModule.SelectedItem.Value
                ModuleRedirection("Echem1ConnectionString", "EC1")


            ElseIf ddlModule.SelectedItem.Value = "8" Then
                Session("TID") = ddlModule.SelectedItem.Value
                ModuleRedirection("Schem1ConnectionString", "SC1")

            ElseIf ddlModule.SelectedItem.Value = "9" Then
                Session("TID") = ddlModule.SelectedItem.Value
                ModuleRedirection("Sustain3ConnectionString", "S3")

            ElseIf ddlModule.SelectedItem.Value = "10" Then
                Session("TID") = ddlModule.SelectedItem.Value
                ModuleRedirection("Sustain4ConnectionString", "S4")

            ElseIf ddlModule.SelectedItem.Value = "11" Then
                Session("TID") = ddlModule.SelectedItem.Value
                ModuleRedirection("Econ4ConnectionString", "E4")

            ElseIf ddlModule.SelectedItem.Value = "12" Then
                Session("TID") = ddlModule.SelectedItem.Value
                ModuleRedirection("Market1ConnectionString", "M1")

            ElseIf ddlModule.SelectedItem.Value = "23" Then
                Session("TID") = ddlModule.SelectedItem.Value
                ModuleRedirection("SpecConnectionString", "SPEC")
            ElseIf ddlModule.SelectedItem.Value = "18" Then
                Session("TID") = ddlModule.SelectedItem.Value
                ModuleRedirection("DistributionConnectionString", "DIST")
            ElseIf ddlModule.SelectedItem.Value = "19" Then
                Session("TID") = ddlModule.SelectedItem.Value
                ModuleRedirection("RetailConnectionString", "RETL")
            ElseIf ddlModule.SelectedItem.Value = "22" Then
                Session("TID") = ddlModule.SelectedItem.Value
                ModuleRedirection("SDistributionConnectionString", "SDIST")
                'Change for Value Chain
            ElseIf ddlModule.SelectedItem.Value = "24" Then
                Session("TID") = ddlModule.SelectedItem.Value
                ModuleRedirection("VChainConnectionString", "VChain")
                'For Old Asp Pages
            ElseIf ddlModule.SelectedItem.Value = "7" Or ddlModule.SelectedItem.Value = "14" Or ddlModule.SelectedItem.Value = "15" Or ddlModule.SelectedItem.Value = "16" Or ddlModule.SelectedItem.Value = "17" Then
                Response.Redirect("../../Econ/security.asp?ID=" + Session("ID").ToString() + "&M1=" + ddlModule.SelectedItem.Value.ToString(), True)
                'Comp Module E1/S1
            ElseIf ddlModule.SelectedItem.Value = "26" Then
                Session("TID") = ddlModule.SelectedItem.Value
                Session("Service") = "COMP"
                ModuleRedirection("EconConnectionString", "COMP")
            ElseIf ddlModule.SelectedItem.Value = "27" Then
                Session("TID") = ddlModule.SelectedItem.Value
                Session("Service") = "COMPS1"
                ModuleRedirection("Sustain1ConnectionString", "COMPS1")
            ElseIf ddlModule.SelectedItem.Value = "28" Then
                Session("TID") = ddlModule.SelectedItem.Value
                Session("Service") = "SBASSIST"
                ModuleRedirection("SBAConnectionString", "StandAssist")
             ElseIf ddlModule.SelectedItem.Value = "29" Then
                Session("TID") = ddlModule.SelectedItem.Value
                ModuleRedirection("MedEcon1ConnectionString", "MEDECON1")
            ElseIf ddlModule.SelectedItem.Value = "30" Then
                Session("TID") = ddlModule.SelectedItem.Value
                ModuleRedirection("MedEcon2ConnectionString", "MEDECON2")
            ElseIf ddlModule.SelectedItem.Value = "31" Then
                Session("TID") = ddlModule.SelectedItem.Value
                ModuleRedirection("MedSustain1ConnectionString", "MEDSUSTAIN1")
            ElseIf ddlModule.SelectedItem.Value = "32" Then
                Session("TID") = ddlModule.SelectedItem.Value
                ModuleRedirection("MedSustain2ConnectionString", "MEDSUSTAIN2")
				ElseIf ddlModule.SelectedItem.Value = "35" Then
                Session("TID") = ddlModule.SelectedItem.Value
                ModuleRedirection("MoldE1ConnectionString", "MOLDE1")
            ElseIf ddlModule.SelectedItem.Value = "36" Then
                Session("TID") = ddlModule.SelectedItem.Value
                ModuleRedirection("MoldE2ConnectionString", "MOLDE2")
            ElseIf ddlModule.SelectedItem.Value = "37" Then
                Session("TID") = ddlModule.SelectedItem.Value
                ModuleRedirection("MoldS1ConnectionString", "MOLDS1")
            ElseIf ddlModule.SelectedItem.Value = "38" Then
                Session("TID") = ddlModule.SelectedItem.Value
                ModuleRedirection("MoldS2ConnectionString", "MOLDS2")
            End If

        Catch ex As Exception
            lblError.Text = "btnSAnalysis_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    
    Protected Sub ModuleRedirection(ByVal strCon As String, ByVal modName As String)
        Dim objGetData As New LoginGetData.Selectdata()
        Dim objUpdateData As New LoginUpdateData.Selectdata()
        Dim obj As New CryptoHelper
        Dim ds, dsUser, dsCont, dsLic, dsId As New DataSet()
        Dim dsLicenseUser As New DataSet()
        Try
            'Get Max Count of License
            dsLic = objGetData.GetMaxCountByLic(Session("UserId").ToString(), strCon)
            Session("MaxLicCount") = dsLic.Tables(0).Rows(0).Item("MAXCOUNT")

            If modName = "COMP" Or modName = "COMPS1" Then
                dsUser = objGetData.GetInuseDetailsByCompUser(Session("UserId").ToString(), strCon)
            Else
                dsUser = objGetData.GetInuseDetailsByUser(Session("UserId").ToString(), strCon)
            End If


            If CInt(dsUser.Tables(0).Rows(0).Item("count")) = 0 Then

                ds = objGetData.GetInuseDetailsByLic(Session("UserId").ToString())
                If CInt(ds.Tables(0).Rows(0).Item("count")) >= CInt(Session("MaxLicCount")) Then
                    maintainLog(ddlModule.SelectedItem.Text)
                    Response.Redirect("ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE115").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&Schema=" + obj.Encrypt(strCon).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!"))
                Else
                    dsLicenseUser = objGetData.ValidateLicenseUser(Session("USERID").ToString(), strCon, modName)
                    If dsLicenseUser.Tables(0).Rows.Count > 0 Then
                        'Inserting Data in Inuse table

                        If modName = "CONTR" Then
                            objUpdateData.InuseLoginInsertContract(Session.SessionID, Session("UserId").ToString(), Session("LicenseNo").ToString().Replace("'", "''"), Request.ServerVariables("REMOTE_ADDR").ToString(), Request.ServerVariables("HTTP_ACCEPT_LANGUAGE").ToString(), Request.ServerVariables("HTTP_USER_AGENT").ToString(), strCon)
                            dsCont = objGetData.GetSelectionData(Session("USERID").ToString(), Session("Password").ToString(), strCon)
                            If dsCont.Tables(0).Rows.Count = 0 Then
                                objUpdateData.InsertSelection(Session("USERID").ToString(), Session("Password").ToString(), strCon)
                            End If
                        ElseIf modName = "PACKPROD" Then
                            objUpdateData.InuseLoginInsertPackProd(Session.SessionID, Session("USERID").ToString(), Session("LicenseNo").ToString().Replace("'", "''"), Request.ServerVariables("REMOTE_ADDR").ToString(), Request.ServerVariables("HTTP_ACCEPT_LANGUAGE").ToString(), Request.ServerVariables("HTTP_USER_AGENT").ToString(), strCon)
                            dsCont = objGetData.GetSelectionData(Session("USERID").ToString(), Session("Password").ToString(), strCon)
                            If dsCont.Tables(0).Rows.Count = 0 Then
                                objUpdateData.InsertSelection(Session("USERID").ToString(), Session("Password").ToString(), strCon)
                            End If
                        Else
                            If modName = "COMP" Or modName = "COMPS1" Then
                                objUpdateData.InuseCompLoginInsert(Session.SessionID, Session("USERID").ToString(), Session("LicenseNo").ToString().Replace("'", "''"), strCon)
                            Else
                                objUpdateData.InuseLoginInsert(Session.SessionID, Session("USERID").ToString(), Session("LicenseNo").ToString().Replace("'", "''"), strCon)
                            End If

                        End If
                        Response.Redirect("~/Session.aspx?Moduls=" + modName + "&ID=" + "'", True)
                    Else
                        If modName = "CONTR" Then
                            Response.Redirect("ContLicenseAgr.aspx")
                        ElseIf modName = "PACKPROD" Then
                            Response.Redirect("PProdLicenseAgr.aspx")
                        Else
                            Response.Redirect("EconLicenseAgreement.aspx")
                        End If

                    End If
                End If
            Else
                dsId = objGetData.ValidateID(Session("UserName"))
                If dsId.Tables(0).Rows(0).Item("ISINTERNALUSR").ToString() = "Y" Then
                    ds = objGetData.GetInuseDetailsByLic(Session("UserId").ToString())
                    If CInt(ds.Tables(0).Rows(0).Item("count")) > CInt(Session("MaxLicCount")) Then
                        maintainLog(ddlModule.SelectedItem.Text)
                        Response.Redirect("ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE115").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&Schema=" + obj.Encrypt(strCon).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!"))
                    Else
                        dsLicenseUser = objGetData.ValidateLicenseUser(Session("USERID").ToString(), strCon, modName)
                        If dsLicenseUser.Tables(0).Rows.Count > 0 Then
                            If modName = "COMP" Or modName = "COMPS1" Then
                                objUpdateData.InuseCompLoginInsert(Session.SessionID, Session("USERID").ToString(), Session("LicenseNo").ToString().Replace("'", "''"), strCon)
                            Else
                                objUpdateData.InuseLoginInsert(Session.SessionID, Session("USERID").ToString(), Session("LicenseNo").ToString().Replace("'", "''"), strCon)
                            End If

                            Response.Redirect("~/Session.aspx?Moduls=" + modName + "&ID=" + "'", True)
                        Else
                            Response.Redirect("EconLicenseAgreement.aspx")
                        End If
                    End If
                Else
                    maintainLog(ddlModule.SelectedItem.Text)
                    Response.Redirect("ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE115").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&Schema=" + obj.Encrypt(strCon).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!"))
                End If
                'maintainLog(ddlModule.SelectedItem.Text)
                'Response.Redirect("ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE115").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&Schema=" + obj.Encrypt(strCon).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!"))
            End If

        Catch ex As Exception
            lblError.Text = "ModuleRedirection:" + ex.Message.ToString() + ""
        End Try

    End Sub

    Protected Sub ModuleKnowBaseRedirection(ByVal strCon As String, ByVal modName As String)
        Dim objGetData As New LoginGetData.Selectdata()
        Dim objUpdateData As New LoginUpdateData.Selectdata()
        Dim obj As New CryptoHelper
        Dim ds, dsUser, dsCont, dsLic As New DataSet()
        Dim dsLicenseUser As New DataSet()
        Try
            'Get Max Count of License
            dsLic = objGetData.GetMaxCountByLic(Session("UserId").ToString(), strCon)
            Session("MaxLicKCount") = dsLic.Tables(0).Rows(0).Item("CONTRMAXCOUNT")
            dsUser = objGetData.GetInuseDetailsByUser(Session("UserId").ToString(), strCon)
            If CInt(dsUser.Tables(0).Rows(0).Item("count")) = 0 Then
                ds = objGetData.GetInuseDetails(Session("UserId").ToString(), strCon)
                If CInt(ds.Tables(0).Rows(0).Item("count")) >= CInt(Session("MaxLicKCount")) Then
                    Response.Redirect("ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE115").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&Schema=" + obj.Encrypt(strCon).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!"))
                Else
                    dsLicenseUser = objGetData.ValidateLicenseUser(Session("USERID").ToString(), strCon, modName)
                    If dsLicenseUser.Tables(0).Rows.Count > 0 Then
                        'Inserting Data in Inuse table

                        If modName = "CONTR" Then
                            objUpdateData.InuseLoginInsertContract(Session.SessionID, Session("USERID").ToString(), Session("LicenseNo").ToString().Replace("'", "''"), Request.ServerVariables("REMOTE_ADDR").ToString(), Request.ServerVariables("HTTP_ACCEPT_LANGUAGE").ToString(), Request.ServerVariables("HTTP_USER_AGENT").ToString(), strCon)
                            dsCont = objGetData.GetSelectionData(Session("USERID").ToString(), Session("Password").ToString(), strCon)
                            If dsCont.Tables(0).Rows.Count = 0 Then
                                objUpdateData.InsertSelection(Session("USERID").ToString(), Session("Password").ToString(), strCon)
                            End If
                        ElseIf modName = "PACKPROD" Then
                            objUpdateData.InuseLoginInsertPackProd(Session.SessionID, Session("USERID").ToString(), Session("LicenseNo").ToString().Replace("'", "''"), Request.ServerVariables("REMOTE_ADDR").ToString(), Request.ServerVariables("HTTP_ACCEPT_LANGUAGE").ToString(), Request.ServerVariables("HTTP_USER_AGENT").ToString(), strCon)
                            dsCont = objGetData.GetSelectionData(Session("USERID").ToString(), Session("Password").ToString(), strCon)
                            If dsCont.Tables(0).Rows.Count = 0 Then
                                objUpdateData.InsertSelection(Session("USERID").ToString(), Session("Password").ToString(), strCon)
                            End If
                        End If
                        Response.Redirect("~/Session.aspx?Moduls=" + modName + "&ID=" + "'", True)
                    Else
                        If modName = "CONTR" Or modName = "PACKPROD" Then
                            Response.Redirect("ContLicenseAgr.aspx")
                        Else
                            Response.Redirect("EconLicenseAgreement.aspx")
                        End If

                    End If
                End If
            Else
                Response.Redirect("ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE115").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&Schema=" + obj.Encrypt(strCon).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!"))
            End If

        Catch ex As Exception
            lblError.Text = "ModuleRedirection:" + ex.Message.ToString() + ""
        End Try

    End Sub

    Protected Sub ModuleRedirectionReport(ByVal strCon As String, ByVal modName As String)
        Dim objGetData As New LoginGetData.Selectdata()
        Dim objUpdateData As New LoginUpdateData.Selectdata()
        Dim obj As New CryptoHelper
        Dim ds, dsUser, dsCont, dsLic As New DataSet()
        Dim dsLicenseUser As New DataSet()
        Try
            'Get Max Count of License
            dsLic = objGetData.GetMaxCountByLic(Session("UserId").ToString(), strCon)
            Session("MaxLicRepCount") = dsLic.Tables(0).Rows(0).Item("REPMAXCOUNT")
            dsUser = objGetData.GetInuseDetailsByUser(Session("UserId").ToString(), strCon)
            If CInt(dsUser.Tables(0).Rows(0).Item("count")) = 0 Then
                ds = objGetData.GetInuseDetails(Session("UserId").ToString(), strCon)
                If CInt(ds.Tables(0).Rows(0).Item("count")) >= CInt(Session("MaxLicRepCount")) Then
                    Response.Redirect("ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE115").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&Schema=" + obj.Encrypt(strCon).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!"))
                Else
                    dsLicenseUser = objGetData.ValidateLicenseUser(Session("USERID").ToString(), strCon, modName)
                    If dsLicenseUser.Tables(0).Rows.Count > 0 Then
                        'Inserting Data in Inuse table                       
                        objUpdateData.InuseLoginInsertReport(Session.SessionID, Session("USERID").ToString(), Session("LicenseNo").ToString().Replace("'", "''"), Session("Password").ToString(), strCon)
                        Response.Redirect("../../Oracle/security5.asp?ID=" + Session("ID").ToString() + "&studyselection=" + ddlReports.SelectedItem.Value.ToString() + "&sessionId=" + Session.SessionID, True)
                    Else
                        Response.Redirect("ReportLicAgreement.aspx")
                    End If
                End If
            Else
                Response.Redirect("ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE115").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&Schema=" + obj.Encrypt(strCon).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!"))
            End If

        Catch ex As Exception
            lblError.Text = "ModuleRedirectionReport:" + ex.Message.ToString() + ""
        End Try

    End Sub

    Protected Sub btnSSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSSearch.Click
        Dim objGetData As New LoginGetData.Selectdata()
        Dim ds As New DataSet()
        Try
            If ddlKBase.SelectedItem.Value = "21" Then
                Session("TID") = ddlKBase.SelectedItem.Value
                ModuleKnowBaseRedirection("ContractConnectionString", "CONTR")
            ElseIf ddlKBase.SelectedItem.Value = "20" Then
                Session("TID") = ddlKBase.SelectedItem.Value
                ModuleKnowBaseRedirection("PackProdConnectionString", "PACKPROD")
            Else

                Response.Redirect("../../Contract/security.asp?ID=" + Session("ID").ToString() + "&C1=" + ddlKBase.SelectedItem.Value.ToString(), True)
            End If
            'Response.Redirect("../../Contract/security.asp?ID=" + Session("ID").ToString() + "&C1=" + ddlKBase.SelectedItem.Value.ToString(), True)
        Catch ex As Exception
            lblError.Text = "btnSSearch_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnSReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSReport.Click
        Dim objGetData As New LoginGetData.Selectdata()
        Dim ds As New DataSet()
        Try
            ModuleRedirectionReport("ReportConnectionString", "REPORT")
            ' Response.Redirect("../../Oracle/security5.asp?ID=" + Session("ID").ToString() + "&studyselection=" + ddlReports.SelectedItem.Value.ToString(), True)
        Catch ex As Exception
            lblError.Text = "btnSReport_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Public Sub maintainLog(ByVal modName As String)
        Try
            Dim str As String = String.Empty
            Dim objGetdata As New LoginGetData.Selectdata
            Dim ds As New DataSet
            'Updating Log
            Try
                Dim path As String = String.Empty
                Dim Err As New CreateLogFiles()
                ds = objGetdata.getLicenseDetail(Session("LicenseNo").ToString())
                If ds.Tables(0).Rows.Count > 0 Then
                    path = ConfigurationManager.AppSettings.GetValues("LogConflictPATH")(0) + "LoginConflict_LOG"
                    Err.LoginConflictLog(path, Session("UserName").ToString(), ds.Tables(0).Rows(0).Item("LICENSENAME").ToString(), modName)
                End If

            Catch ex As Exception
            End Try

        Catch ex As Exception

        End Try
    End Sub
    Public Sub GetSubscriptionDetails()
        Dim objGetData As New LoginGetData.Selectdata()
        Dim ds As New DataSet()
        Try
            Try
                ds = objGetData.GetSubscriptionDetails(Session("UserId").ToString())
                If ds.Tables(0).Rows.Count > 0 Then
                    divSubscription.Style.Add("Display", "inline")
                    With ddlSubscp
                        .DataSource = ds
                        .DataTextField = "SERVICENAME"
                        .DataValueField = "SERVICEID"
                        .DataBind()
                        .Font.Size = 8
                    End With
                Else
                    divSubscription.Style.Add("Display", "none")
                End If
            Catch ex As Exception

            End Try


        Catch ex As Exception
            lblError.Text = "GetSubscriptionDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub
    
 Protected Sub btnSubscr_Click_ORG(ByVal sender As Object, ByVal e As System.EventArgs) 
        Dim objGetData As New LoginGetData.Selectdata()
        Dim objUpdateData As New LoginUpdateData.Selectdata()
        Dim obj As New CryptoHelper
        Dim ds, dsLic, dsUser As New DataSet()
        Dim dsLicenseUser As New DataSet()
        Try
            dsLic = objGetData.GetMaxCountByLic(Session("UserId").ToString(), "Market1ConnectionString")
            ds = objGetData.GetInuseDetailsByUserMKT(Session("UserId").ToString(), "Market1ConnectionString")
            Session("MaxLicCount") = dsLic.Tables(0).Rows(0).Item("MAXCOUNT")
            If CInt(ds.Tables(0).Rows(0).Item("count")) = 0 Then
                dsUser = objGetData.GetInuseDetailsByLicMKT(Session("UserId").ToString())
                If CInt(dsUser.Tables(0).Rows(0).Item("count")) >= CInt(Session("MaxLicCount")) Then
                    maintainLog(ddlSubscp.SelectedItem.Text)
                    Response.Redirect("ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE115").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&Schema=" + obj.Encrypt("Market1ConnectionString").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!"))
                Else
                    dsLicenseUser = objGetData.ValidateLicenseUser(Session("USERID").ToString(), "Market1ConnectionString", "Market1")
                    If dsLicenseUser.Tables(0).Rows.Count > 0 Then
                        'Inserting Data in Inuse table
                        objUpdateData.InuseLoginInsert(Session.SessionID, Session("USERID").ToString(), Session("LicenseNo").ToString().Replace("'", "''"), "Market1ConnectionString")
                        Session("M1ServiceID") = ddlSubscp.SelectedItem.Value
                        Session("TID") = ddlSubscp.SelectedItem.Value
                        Response.Redirect("~/Pages/Market1Sub/Default.aspx")
                    Else
                        Response.Redirect("EconLicenseAgreement.aspx")
                    End If
                End If
            Else
                maintainLog(ddlSubscp.SelectedItem.Text)
                Response.Redirect("ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE115").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&Schema=" + obj.Encrypt("Market1ConnectionString").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!"))

            End If

                If CInt(ds.Tables(0).Rows(0).Item("count")) > 0 Then

                Else
                    
                End If



        Catch ex As Exception
            lblError.Text = "btnSubscr_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

Protected Sub btnSubscr_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubscr.Click
        Dim objGetData As New LoginGetData.Selectdata()
        Dim objUpdateData As New LoginUpdateData.Selectdata()
        Dim obj As New CryptoHelper
        Dim ds, dsLic, dsUser As New DataSet()
        Dim dsLicenseUser As New DataSet()
        Try
            dsLic = objGetData.GetMaxCountByLic(Session("UserId").ToString(), "Market1ConnectionString")
            ds = objGetData.GetInuseDetailsByUserMKT(Session("UserId").ToString(), "Market1ConnectionString")
            Session("MaxLicCount") = dsLic.Tables(0).Rows(0).Item("MAXCOUNT")
            If CInt(ds.Tables(0).Rows(0).Item("count")) = 0 Then
                'dsUser = objGetData.GetInuseDetailsByLic(Session("UserId").ToString())
                dsLicenseUser = objGetData.ValidateLicenseUser(Session("USERID").ToString(), "Market1ConnectionString", "Market1")
                If dsLicenseUser.Tables(0).Rows.Count > 0 Then
                    'Inserting Data in Inuse table
                    objUpdateData.InuseLoginInsert(Session.SessionID, Session("USERID").ToString(), Session("LicenseNo").ToString().Replace("'", "''"), "Market1ConnectionString")
                    Session("M1ServiceID") = ddlSubscp.SelectedItem.Value
                    Session("TID") = ddlSubscp.SelectedItem.Value
                    Response.Redirect("~/Pages/Market1Sub/Default.aspx")
                Else
                    Response.Redirect("EconLicenseAgreement.aspx")
                End If
            Else
                maintainLog(ddlSubscp.SelectedItem.Text)
                Response.Redirect("ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE115").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&Schema=" + obj.Encrypt("Market1ConnectionString").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!"))

            End If

          Catch ex As Exception
            lblError.Text = "btnSubscr_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
End Class
