Imports System.Data
Imports E1UpInsData
Partial Class Pages_Econ1_EconLicenseAgreement
    Inherits System.Web.UI.Page
    Dim _strSchema As String
    Dim _strSerID As String
    Public Property Schema() As String
        Get
            Return _strSchema
        End Get
        Set(ByVal value As String)
            Dim obj As New CryptoHelper
            _strSchema = value
        End Set
    End Property

    Public Property ServID() As String
        Get
            Return _strSerID
        End Get
        Set(ByVal value As String)
            Dim obj As New CryptoHelper
            _strSerID = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim obj As New CryptoHelper
        Try
            lblTag.Text = "Copyright 1997 - " + Now.Year().ToString() + " Savvypack Corporation."
            ServID = Request.QueryString("ServID").ToString()
            Schema = Request.QueryString("Schema").ToString()
            'If Not IsPostBack Then
            '    ServID = Request.QueryString("ServID").ToString()
            '    If ServID.ToString() <> "" Then
            '        hidTId.Value = obj.Decrypt(ServID).Replace("!Plus!", "+").Replace("!Hash!", "#").Replace("!And!", "&")
            '    Else
            '        Page.ClientScript.RegisterStartupScript(Me.GetType(), "LicenAgreemnt", "javascript:alert('Something went wrong! Please try again later.'); window.close();", True)
            '    End If
            'End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnAccept_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAccept.Click
        Try
            Dim objUpdateData As New LoginUpdateData.Selectdata
            Dim password As String
            Dim userName As String
            userName = Session("USERID").ToString()
            password = Session("Password").ToString()
            objUpdateData.InsertLicense(userName, password, "Econ")
            Response.Redirect("ModuleRedirect.aspx?ServID=" + ServID + "&Schema=" + Schema + "", False)
            'FindServc()
            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClsTab", "CloseTab();", True)
        Catch ex As Exception
            lblError.Text = "Error:SetSessions:" + ex.Message.ToString()
        End Try

    End Sub

    Protected Sub btnDecline_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDecline.Click
        Try
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "CloseWindow", "javascript:window.close();", True)
        Catch ex As Exception
            lblError.Text = "Error:SetSessions:" + ex.Message.ToString()
        End Try

    End Sub

    'Protected Sub FindServc()
    '    Dim objGetData As New LoginGetData.Selectdata()
    '    Dim objUpdateData As New LoginUpdateData.Selectdata()
    '    Dim obj As New CryptoHelper
    '    Dim ds As New DataSet()
    '    Dim dsLicenseUser As New DataSet()
    '    Try
    '        If hidTId.Value = "1" Then
    '            Session("TID") = hidTId.Value
    '            Session("Service") = Nothing
    '            ModuleRedirection("EconConnectionString", "E1")


    '        ElseIf hidTId.Value = "2" Then
    '            Session("TID") = hidTId.Value
    '            ModuleRedirection("Econ2ConnectionString", "E2")

    '        ElseIf hidTId.Value = "3" Then
    '            Session("TID") = hidTId.Value
    '            ModuleRedirection("Econ3ConnectionString", "E3")

    '        ElseIf hidTId.Value = "4" Then
    '            Session("TID") = hidTId.Value
    '            ds = objGetData.GetInuseDetails(Session("UserId").ToString(), "EconConnectionString")
    '            If CInt(ds.Tables(0).Rows(0).Item("count")) > 0 Then
    '                Response.Redirect("~/Universal_loginN/Pages/ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE115").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&Schema=" + obj.Encrypt("EconConnectionString").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!"), False)
    '            Else
    '                dsLicenseUser = objGetData.ValidateLicenseUser(Session("USERID").ToString(), "EconConnectionString", "Econ")
    '                If dsLicenseUser.Tables(0).Rows.Count > 0 Then
    '                    'Inserting Data in Inuse table
    '                    objUpdateData.InuseLoginInsert(Session.SessionID, Session("USERID").ToString(), Session("LicenseNo").ToString().Replace("'", "''"), "EconConnectionString")
    '                    Response.Redirect("~/Session.aspx?ID=Chart&UserName='" + Session("UserName").ToString() + "'", False)
    '                Else
    '                    'Page.ClientScript.RegisterStartupScript(Me.GetType(), "LicenAgreemnt", "javascript:window.open('/www.savvypack.com/Universal_loginN/Pages/EconLicenseAgreement.aspx?ServID=','_blank');", True)
    '                    Response.Redirect("~/Universal_loginN/Pages/EconLicenseAgreement.aspx?ServID=", False)
    '                End If
    '            End If

    '        ElseIf hidTId.Value = "5" Then
    '            Session("Service") = Nothing
    '            Session("TID") = hidTId.Value
    '            ModuleRedirection("Sustain1ConnectionString", "S1")

    '        ElseIf hidTId.Value = "6" Then
    '            Session("TID") = hidTId.Value
    '            ModuleRedirection("Sustain2ConnectionString", "S2")

    '        ElseIf hidTId.Value = "7" Then
    '            Session("TID") = hidTId.Value
    '            ModuleRedirection("Echem1ConnectionString", "EC1")


    '        ElseIf hidTId.Value = "8" Then
    '            Session("TID") = hidTId.Value
    '            ModuleRedirection("Schem1ConnectionString", "SC1")

    '        ElseIf hidTId.Value = "9" Then
    '            Session("TID") = hidTId.Value
    '            ModuleRedirection("Sustain3ConnectionString", "S3")

    '        ElseIf hidTId.Value = "10" Then
    '            Session("TID") = hidTId.Value
    '            ModuleRedirection("Sustain4ConnectionString", "S4")

    '        ElseIf hidTId.Value = "11" Then
    '            Session("TID") = hidTId.Value
    '            ModuleRedirection("Econ4ConnectionString", "E4")

    '        ElseIf hidTId.Value = "12" Then
    '            Session("TID") = hidTId.Value
    '            ModuleRedirection("Market1ConnectionString", "M1")

    '        ElseIf hidTId.Value = "23" Then
    '            Session("TID") = hidTId.Value
    '            ModuleRedirection("SpecConnectionString", "SPEC")
    '        ElseIf hidTId.Value = "18" Then
    '            Session("TID") = hidTId.Value
    '            ModuleRedirection("DistributionConnectionString", "DIST")
    '        ElseIf hidTId.Value = "19" Then
    '            Session("TID") = hidTId.Value
    '            ModuleRedirection("RetailConnectionString", "RETL")
    '        ElseIf hidTId.Value = "22" Then
    '            Session("TID") = hidTId.Value
    '            ModuleRedirection("SDistributionConnectionString", "SDIST")
    '            'Change for Value Chain
    '        ElseIf hidTId.Value = "24" Then
    '            Session("TID") = hidTId.Value
    '            ModuleRedirection("VChainConnectionString", "VChain")
    '            'For Old Asp Pages
    '        ElseIf hidTId.Value = "7" Or hidTId.Value = "14" Or hidTId.Value = "15" Or hidTId.Value = "16" Or hidTId.Value = "17" Then
    '            Response.Redirect("../../Econ/security.asp?ID=" + Session("ID").ToString() + "&M1=" + hidTId.Value.ToString(), False)
    '            'Comp Module E1/S1
    '        ElseIf hidTId.Value = "26" Then
    '            Session("TID") = hidTId.Value
    '            Session("Service") = "COMP"
    '            ModuleRedirection("EconConnectionString", "COMP")
    '        ElseIf hidTId.Value = "27" Then
    '            Session("TID") = hidTId.Value
    '            Session("Service") = "COMPS1"
    '            ModuleRedirection("Sustain1ConnectionString", "COMPS1")
    '        ElseIf hidTId.Value = "28" Then
    '            Session("TID") = hidTId.Value
    '            Session("Service") = "SBASSIST"
    '            ModuleRedirection("SBAConnectionString", "StandAssist")
    '        ElseIf hidTId.Value = "29" Then
    '            Session("TID") = hidTId.Value
    '            ModuleRedirection("MedEcon1ConnectionString", "MEDECON1")
    '        ElseIf hidTId.Value = "30" Then
    '            Session("TID") = hidTId.Value
    '            ModuleRedirection("MedEcon2ConnectionString", "MEDECON2")
    '        ElseIf hidTId.Value = "31" Then
    '            Session("TID") = hidTId.Value
    '            ModuleRedirection("MedSustain1ConnectionString", "MEDSUSTAIN1")
    '        ElseIf hidTId.Value = "32" Then
    '            Session("TID") = hidTId.Value
    '            ModuleRedirection("MedSustain2ConnectionString", "MEDSUSTAIN2")
    '        ElseIf hidTId.Value = "35" Then
    '            Session("TID") = hidTId.Value
    '            ModuleRedirection("MoldE1ConnectionString", "MOLDE1")
    '        ElseIf hidTId.Value = "36" Then
    '            Session("TID") = hidTId.Value
    '            ModuleRedirection("MoldE2ConnectionString", "MOLDE2")
    '        ElseIf hidTId.Value = "37" Then
    '            Session("TID") = hidTId.Value
    '            ModuleRedirection("MoldS1ConnectionString", "MOLDS1")
    '        ElseIf hidTId.Value = "38" Then
    '            Session("TID") = hidTId.Value
    '            ModuleRedirection("MoldS2ConnectionString", "MOLDS2")
    '        End If

    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Protected Sub ModuleRedirection(ByVal strCon As String, ByVal modName As String)
    '    Dim objGetData As New LoginGetData.Selectdata()
    '    Dim objUpdateData As New LoginUpdateData.Selectdata()
    '    Dim obj As New CryptoHelper
    '    Dim ds, dsUser, dsCont, dsLic, dsId As New DataSet()
    '    Dim dsLicenseUser As New DataSet()
    '    Try
    '        'Get Max Count of License
    '        dsLic = objGetData.GetMaxCountByLic(Session("UserId").ToString(), strCon)
    '        Session("MaxLicCount") = dsLic.Tables(0).Rows(0).Item("MAXCOUNT")

    '        If modName = "COMP" Or modName = "COMPS1" Then
    '            dsUser = objGetData.GetInuseDetailsByCompUser(Session("UserId").ToString(), strCon)
    '        Else
    '            dsUser = objGetData.GetInuseDetailsByUser(Session("UserId").ToString(), strCon)
    '        End If


    '        If CInt(dsUser.Tables(0).Rows(0).Item("count")) = 0 Then

    '            ds = objGetData.GetInuseDetailsByLic(Session("UserId").ToString())
    '            If CInt(ds.Tables(0).Rows(0).Item("count")) >= CInt(Session("MaxLicCount")) Then
    '                'maintainLog(ddlModule.SelectedItem.Text)
    '                Response.Redirect("~/Universal_loginN/Pages/ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE115").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&Schema=" + obj.Encrypt(strCon).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!"), False)
    '            Else
    '                dsLicenseUser = objGetData.ValidateLicenseUser(Session("USERID").ToString(), strCon, modName)
    '                If dsLicenseUser.Tables(0).Rows.Count > 0 Then
    '                    'Inserting Data in Inuse table

    '                    If modName = "CONTR" Then
    '                        objUpdateData.InuseLoginInsertContract(Session.SessionID, Session("UserId").ToString(), Session("LicenseNo").ToString().Replace("'", "''"), Request.ServerVariables("REMOTE_ADDR").ToString(), Request.ServerVariables("HTTP_ACCEPT_LANGUAGE").ToString(), Request.ServerVariables("HTTP_USER_AGENT").ToString(), strCon)
    '                        dsCont = objGetData.GetSelectionData(Session("USERID").ToString(), Session("Password").ToString(), strCon)
    '                        If dsCont.Tables(0).Rows.Count = 0 Then
    '                            objUpdateData.InsertSelection(Session("USERID").ToString(), Session("Password").ToString(), strCon)
    '                        End If
    '                    ElseIf modName = "PACKPROD" Then
    '                        objUpdateData.InuseLoginInsertPackProd(Session.SessionID, Session("USERID").ToString(), Session("LicenseNo").ToString().Replace("'", "''"), Request.ServerVariables("REMOTE_ADDR").ToString(), Request.ServerVariables("HTTP_ACCEPT_LANGUAGE").ToString(), Request.ServerVariables("HTTP_USER_AGENT").ToString(), strCon)
    '                        dsCont = objGetData.GetSelectionData(Session("USERID").ToString(), Session("Password").ToString(), strCon)
    '                        If dsCont.Tables(0).Rows.Count = 0 Then
    '                            objUpdateData.InsertSelection(Session("USERID").ToString(), Session("Password").ToString(), strCon)
    '                        End If
    '                    Else
    '                        If modName = "COMP" Or modName = "COMPS1" Then
    '                            objUpdateData.InuseCompLoginInsert(Session.SessionID, Session("USERID").ToString(), Session("LicenseNo").ToString().Replace("'", "''"), strCon)
    '                        Else
    '                            objUpdateData.InuseLoginInsert(Session.SessionID, Session("USERID").ToString(), Session("LicenseNo").ToString().Replace("'", "''"), strCon)
    '                        End If

    '                    End If
    '                    'Page.ClientScript.RegisterStartupScript(Me.GetType(), "UserServ", "javascript:window.open('../../Session.aspx?Moduls=" + modName + "&ID=" + "','_blank'); window.close();", True)
    '                    Response.Redirect("~/Session.aspx?Moduls=" + modName + "&ID=" + "", False)
    '                Else
    '                    If modName = "CONTR" Then
    '                        'Page.ClientScript.RegisterStartupScript(Me.GetType(), "LicenAgreemnt", "javascript:window.open('/www.savvypack.com/Universal_loginN/Pages/ContLicenseAgr.aspx?ServID=','_blank');", True)
    '                        Response.Redirect("~/Universal_loginN/Pages/ContLicenseAgr.aspx?ServID=", False)
    '                    ElseIf modName = "PACKPROD" Then
    '                        'Page.ClientScript.RegisterStartupScript(Me.GetType(), "LicenAgreemnt", "javascript:window.open('/www.savvypack.com/Universal_loginN/Pages/PProdLicenseAgr.aspx?ServID=','_blank');", True)
    '                        Response.Redirect("~/Universal_loginN/Pages/PProdLicenseAgr.aspx?ServID=", False)
    '                    Else
    '                        'Page.ClientScript.RegisterStartupScript(Me.GetType(), "LicenAgreemnt", "javascript:window.open('/www.savvypack.com/Universal_loginN/Pages/EconLicenseAgreement.aspx?ServID=','_blank');", True)
    '                        Response.Redirect("~/Universal_loginN/Pages/EconLicenseAgreement.aspx?ServID=", False)
    '                    End If

    '                End If
    '            End If
    '        Else
    '            dsId = objGetData.ValidateID(Session("UserName"))
    '            If dsId.Tables(0).Rows(0).Item("ISINTERNALUSR").ToString() = "Y" Then
    '                ds = objGetData.GetInuseDetailsByLic(Session("UserId").ToString())
    '                If CInt(ds.Tables(0).Rows(0).Item("count")) > CInt(Session("MaxLicCount")) Then
    '                    'maintainLog(ddlModule.SelectedItem.Text)
    '                    Response.Redirect("~/Universal_loginN/Pages/ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE115").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&Schema=" + obj.Encrypt(strCon).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!"), False)
    '                Else
    '                    dsLicenseUser = objGetData.ValidateLicenseUser(Session("USERID").ToString(), strCon, modName)
    '                    If dsLicenseUser.Tables(0).Rows.Count > 0 Then
    '                        If modName = "COMP" Or modName = "COMPS1" Then
    '                            objUpdateData.InuseCompLoginInsert(Session.SessionID, Session("USERID").ToString(), Session("LicenseNo").ToString().Replace("'", "''"), strCon)
    '                        Else
    '                            objUpdateData.InuseLoginInsert(Session.SessionID, Session("USERID").ToString(), Session("LicenseNo").ToString().Replace("'", "''"), strCon)
    '                        End If
    '                        'Page.ClientScript.RegisterStartupScript(Me.GetType(), "UserServ", "javascript:window.open('Session.aspx?Moduls=" + modName + "&ID=" + "','_blank');", True)
    '                        Response.Redirect("~/Session.aspx?Moduls=" + modName + "&ID=" + "", False)
    '                    Else
    '                        'Page.ClientScript.RegisterStartupScript(Me.GetType(), "LicenAgreemnt", "javascript:window.open('/www.savvypack.com/Universal_loginN/Pages/EconLicenseAgreement.aspx?ServID=','_blank');", True)
    '                        Response.Redirect("~/Universal_loginN/Pages/EconLicenseAgreement.aspx?ServID=", False)
    '                    End If
    '                End If
    '            Else
    '                Response.Redirect("~/Universal_loginN/Pages/ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE115").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&Schema=" + obj.Encrypt(strCon).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!"), False)
    '            End If
    '        End If

    '    Catch ex As Exception
    '        ' lblError.Text = "ModuleRedirection:" + ex.Message.ToString() + ""
    '    End Try

    'End Sub

End Class