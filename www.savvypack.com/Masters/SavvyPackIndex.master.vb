Imports System
Imports System.Data
Imports System.Net.Mail
Imports System.Data.OleDb
Imports LoginGetData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Imports ToolCCS
Imports LogFiles

Partial Class Masters_SavvyPackIndex
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim UserLogin_Path As String = ""
            Dim objGetData As New UsersGetData.Selectdata()
            Dim ds As New DataSet()
            'If HttpContext.Current.Request.IsSecureConnection = True Then
            'UserLogin_Path = System.Configuration.ConfigurationManager.AppSettings("UserLogin_PathI")
            'Else
            'UserLogin_Path = System.Configuration.ConfigurationManager.AppSettings("UserLogin_Path")
            'End If

            Dim currenturl = Request.ServerVariables("HTTP_HOST")
            UserLogin_Path = Session("UserLoginpath")
            'If currenturl.Contains("www.savvypack.com") Then

            '    If HttpContext.Current.Request.IsSecureConnection.Equals(False) Then
            '        Response.Redirect(Request.Url.ToString().Replace("http://", "https://"))
            '    End If
            'End If
            If HttpContext.Current.Request.IsSecureConnection = True Then
                UserLogin_Path = System.Configuration.ConfigurationManager.AppSettings("UserLogin_PathS")
            Else
                UserLogin_Path = System.Configuration.ConfigurationManager.AppSettings("UserLogin_Path")
            End If
            If UserLogin_Path <> "" Then
                hdnUserLoginPath.Value = UserLogin_Path
            End If

            If Not IsPostBack Then
            Else
                If hdnAlert.Value <> "0" Then
                    hdnAlert.Value = "0"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Thank you for creating your account. Your email address has been verified, and you can now use your account.');", True)
                End If
            End If

            If Session("UserId") = Nothing Then
                lnkLogout.Attributes.Add("onclick", "return OpenLoginPopup('N');")
                lnkLogout.Text = "Login"
                lnkSavvyPack.Attributes.Add("onclick", "return OpenLoginPopup('Y');")
                lnkOData.Attributes.Add("onclick", "return OpenLoginPopup('YO');")
                lnkSavvy.Attributes.Add("onclick", "return OpenLoginPopup('YS');")
                lnkAcco.Attributes.Add("onclick", "return OpenLoginPopup('AI');")
            Else
                lnkLogout.Enabled = True
                lnkLogout.Text = "Logout"
                ds = objGetData.GetUserDetails(Session("UserId").ToString())
                If ds.Tables(0).Rows.Count > 0 Then
                    lblUserMess.Text = "Welcome " + ds.Tables(0).Rows(0).Item("FIRSTNAME").ToString() + " " + ds.Tables(0).Rows(0).Item("LASTNAME").ToString()
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error:Page_Load " + ex.Message.ToString()
        End Try
    End Sub
Protected Sub lnkAnal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkAnal.Click
        Try
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "UManager", "javascript:window.open('/AnalyticalService.aspx');", True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "UManager", "OpenWindowMaster1('http://www.savvypack.com/AnalyticalService.aspx');", True)
        Catch ex As Exception
            lblError.Text = "Error:lnkAnal_Click " + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub lnkStrc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkStrc.Click
        Try
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "UManager", "javascript:window.open('/Structure Assistant.aspx');", True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "UManager", "OpenWindowMaster2('http://www.savvypack.com/Structure Assistant.aspx');", True)

        Catch ex As Exception
            lblError.Text = "Error:lnkStrc_Click " + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub imgLogo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgLogo.Click
        Try
            Session("SavvyI") = "Y"
            If Session("UserId") = Nothing Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Login_SavvyPack", "OpenLoginPopup('Y');", True)
            Else
                SavvyLinkRedirect()
            End If
        Catch ex As Exception
            lblError.Text = "Error:lnkCntUs_Click " + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub lnkLogout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkLogout.Click
        Try
            Dim objUpdate As New LoginUpdateData.Selectdata
            If Session("TID") <> Nothing Then
                'objUpdate.UpdateLogOffDetails2(Session("UserName"), Session("TID"), Session.SessionID)
                If Session("LogInCount") <> Nothing Then
                    objUpdate.UpdateLogOffDetails2(Session("TID"), Session.SessionID, Session("UserId"), Session("LogInCount").ToString())
                Else
                    objUpdate.UpdateLogOffDetails2(Session("TID"), Session.SessionID, Session("UserId"), "")
                End If
            End If
            Session.Abandon()
            Session.RemoveAll()
            Response.Redirect("~/Index.aspx", True)
        Catch ex As Exception
            lblError.Text = "Error:lnkLogout_Click " + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            ScriptManager.RegisterStartupScript(Page, Me.GetType(), "JSScript", "openNav();", True)
        Catch ex As Exception
            lblError.Text = "Error:btnRefresh_Click " + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub lnkAbtUs_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkAbtUs.Click
        Try
            Response.Redirect("~/AboutUs.aspx")
        Catch ex As Exception
            lblError.Text = "Error:lnkAbtUs_Click " + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub lnkCntUs_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCntUs.Click
        Try
            Response.Redirect("~/ContactUsN.aspx")
        Catch ex As Exception
            lblError.Text = "Error:lnkCntUs_Click " + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub lnkAcco_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkAcco.Click
        Dim obj As New CryptoHelper
        Try
            If Session("UserId") = Nothing Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Login_SavvyPack", "OpenLoginPopup('AI');", True)
            Else
                Response.Redirect("Users_Login/AddEditUser.aspx?Mode=" + obj.Encrypt("Edit").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
            End If
        Catch ex As Exception
            lblError.Text = "Error:lnkAcco_Click " + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnAccInfo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAccInfo.Click
        Dim obj As New CryptoHelper
        Try
            Response.Redirect("Users_Login/AddEditUser.aspx?Mode=" + obj.Encrypt("Edit").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
        Catch ex As Exception

        End Try
    End Sub

    'Protected Sub lnkAcco_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkAcco.Click
    '    Try
    '        Dim obj As New CryptoHelper
    '        Response.Redirect("Users_Login/AddEditUser.aspx?Mode=" + obj.Encrypt("Edit").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
    '    Catch ex As Exception
    '        lblError.Text = "Error:lnkAcco_Click " + ex.Message.ToString()
    '    End Try
    'End Sub

    'Protected Sub lnkSavvyPack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSavvyPack.Click
    '    Try
    '        If Session("UserId") = Nothing Then
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Login_SavvyPack", "OpenLoginPopup('Y');", True)
    '        Else
    '            If Session("INTUSR") = "Y" Then
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "UManager", "OpenUManagerPopUp('../Popup/UIntManager.aspx?Id=hidTId');", True)
    '            Else
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "UManager", "OpenUManagerPopUp('../Popup/UManager.aspx?Id=hidTId');", True)
    '            End If

    '        End If
    '    Catch ex As Exception
    '        lblError.Text = "Error:lnkCntUs_Click " + ex.Message.ToString()
    '    End Try
    'End Sub

    Protected Sub lnkOData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkOData.Click
        Try
            If Session("UserId") = Nothing Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Login_SavvyPack", "OpenLoginPopup('YO');", True)
            Else
                MarketLinkRedirect()
            End If
        Catch ex As Exception
            lblError.Text = "Error:lnkCntUs_Click " + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnOData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOData.Click
        Try
            MarketLinkRedirect()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkSavvyPack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSavvyPack.Click
        Try
            If Session("UserId") = Nothing Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Login_SavvyPack", "OpenLoginPopup('Y');", True)
            Else
                SavvyLinkRedirect()
            End If
        Catch ex As Exception
            lblError.Text = "Error:lnkSavvyPack_Click " + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnSavvy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSavvy.Click
        Dim objGetData As New LoginGetData.Selectdata()
        Try
            SavvyLinkRedirect()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkSavvy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSavvy.Click
        Try
            If Session("UserId") = Nothing Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Login_SavvyPack", "OpenLoginPopup('Y');", True)
            Else
                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "UManager", "javascript:window.open('/OnlineForm/ProjectManager.aspx');", True)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "UManager", "OpenWindowMaster3('OnlineForm/ProjectManager.aspx');", True)
            End If
        Catch ex As Exception
            lblError.Text = "Error:lnkCntUs_Click " + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnProj_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProj.Click
        Try
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "UManager", "OpenNewWindow('OnlineForm/ProjectManager.aspx');", True)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub SavvyLinkRedirect()
        Dim objGetData As New LoginGetData.Selectdata()
        Dim dsE As New DataSet()
        Dim dsU As New DataSet()
        Try
            If Session("UserId") <> Nothing Then
                If Session("ID") <> Nothing Then
                    Dim ds, dsSecLvl, dsPer As New DataSet()
                    ds = objGetData.ValidateUser(Session("UserName").ToString(), Session("Password").ToString())
                    dsSecLvl = objGetData.GetSecuirtyDetails(ds.Tables(0).Rows(0).Item("SECURITYLVL").ToString())
                    If ds.Tables(0).Rows.Count > 0 Then
                        Session("SecurityLevel") = dsSecLvl.Tables(0).Rows(0).Item("SECLEVEL").ToString()
                        dsPer = objGetData.ValidateUserPermissions(Session("UserId").ToString())
                        If dsPer.Tables(0).Rows.Count > 0 Then
                            If Session("SecurityLevel") <> "5" Then            'Code added to implement HTTPS
                                'Response.Redirect("https://www.allied-dev.com/Universal_loginN/Pages/UniversalM.aspx", True)
                                'Response.Redirect("UniversalM.aspx", True)
                                If Session("INTUSR") = "Y" Then
                                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "UManager", "OpenUManagerPopUp('Popup/UIntManager.aspx?Id=hidTId');", True)
                                Else
                                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "UManager", "OpenUManagerPopUp('Popup/Umanager.aspx?Id=hidTId');", True)
                                End If
                            Else
                                If Session("INTUSR") = "Y" Then
                                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "UManager", "OpenUManagerPopUp('Popup/UIntManager.aspx?Id=hidTId');", True)
                                Else
                                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "UManager", "OpenUManagerPopUp('Popup/Umanager.aspx?Id=hidTId');", True)
                                End If
                            End If
                        Else
                            Session("Back") = Nothing
                            Session("SBack") = "Secure"
                            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "UManager", "OpenUMgrPopUp('Popup/UniversalMgr.aspx');", True)
                            If Session("INTUSR") = "Y" Then
                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "UManager", "OpenUManagerPopUp('Popup/UIntManager.aspx?Id=hidTId');", True)
                            Else
                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "UManager", "OpenUManagerPopUp('Popup/Umanager.aspx?Id=hidTId');", True)
                            End If
                        End If

                    End If
                Else
                    dsE = objGetData.GetUserDetailsByID(Session("UserId").ToString())
                    dsU = objGetData.ValidateUserSavvy(dsE.Tables(0).Rows(0).Item("USERNAME").ToString(), dsE.Tables(0).Rows(0).Item("PASSWORD").ToString().Replace("'", "''"))

                    Session("UserId") = dsU.Tables(0).Rows(0).Item("USERID").ToString()
                    Session("UserName") = dsU.Tables(0).Rows(0).Item("USERNAME").ToString()
                    Session("Password") = dsU.Tables(0).Rows(0).Item("PASSWORD").ToString()
                    Session("LicenseNo") = Nothing
                    Session("Back") = Nothing
                    Session("SBack") = "Secure"
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "UManager", "OpenUMgrPopUp('Popup/UniversalMgr.aspx');", True)
                    If Session("INTUSR") = "Y" Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "UManager", "OpenUManagerPopUp('Popup/UIntManager.aspx?Id=hidTId');", True)
                    Else
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "UManager", "OpenUManagerPopUp('Popup/Umanager.aspx?Id=hidTId');", True)
                    End If
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub MarketLinkRedirect()
        Dim objGetData As New LoginGetData.Selectdata()
        Dim dsE As New DataSet()
        Dim dsU As New DataSet()
        Try
            If Session("UserId") <> Nothing Then
                If Session("ID") <> Nothing Then
                    Dim ds, dsSecLvl, dsPer As New DataSet()
                    ds = objGetData.ValidateUser(Session("UserName").ToString(), Session("Password").ToString())
                    dsSecLvl = objGetData.GetSecuirtyDetails(ds.Tables(0).Rows(0).Item("SECURITYLVL").ToString())
                    If ds.Tables(0).Rows.Count > 0 Then
                        Session("SecurityLevel") = dsSecLvl.Tables(0).Rows(0).Item("SECLEVEL").ToString()
                        dsPer = objGetData.ValidateUserPermissions(Session("UserId").ToString())
                        If dsPer.Tables(0).Rows.Count > 0 Then
                            If Session("SecurityLevel") <> "5" Then            'Code added to implement HTTPS
                                'Response.Redirect("https://www.allied-dev.com/Universal_loginN/Pages/UniversalM.aspx", True)
                                'Response.Redirect("UniversalM.aspx", True)
                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "UManager", "OpenUManagerPopUp('Popup/ODatabase.aspx?Id=hidTId');", True)
                            Else
                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "UManager", "OpenUManagerPopUp('Popup/ODatabase.aspx?Id=hidTId');", True)
                            End If
                        Else
                            Session("Back") = Nothing
                            Session("SBack") = "Secure"
                            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "UManager", "OpenUMgrPopUp('Popup/UniversalMgr.aspx');", True)
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "UManager", "OpenUManagerPopUp('Popup/ODatabase.aspx?Id=hidTId');", True)
                        End If

                    End If
                Else
                    dsE = objGetData.GetUserDetailsByID(Session("UserId").ToString())
                    dsU = objGetData.ValidateUserSavvy(dsE.Tables(0).Rows(0).Item("USERNAME").ToString(), dsE.Tables(0).Rows(0).Item("PASSWORD").ToString().Replace("'", "''"))

                    Session("UserId") = dsU.Tables(0).Rows(0).Item("USERID").ToString()
                    Session("UserName") = dsU.Tables(0).Rows(0).Item("USERNAME").ToString()
                    Session("Password") = dsU.Tables(0).Rows(0).Item("PASSWORD").ToString()
                    Session("LicenseNo") = Nothing
                    Session("Back") = Nothing
                    Session("SBack") = "Secure"
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "UManager", "OpenUMgrPopUp('Popup/UniversalMgr.aspx');", True)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "UManager", "OpenUManagerPopUp('Popup/ODatabase.aspx?Id=hidTId');", True)
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnServ_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnServ.Click
        Dim objGetData As New LoginGetData.Selectdata()
        Dim objUpdateData As New LoginUpdateData.Selectdata()
        Dim obj As New CryptoHelper
        Dim ds As New DataSet()
        Dim dsLicenseUser As New DataSet()
        Try
            If hidTId.Value = "1" Then
                Session("TID") = hidTId.Value
                Session("Service") = Nothing
                ModuleRedirection("EconConnectionString", "E1")

            ElseIf hidTId.Value = "2" Then
                Session("TID") = hidTId.Value
                ModuleRedirection("Econ2ConnectionString", "E2")

            ElseIf hidTId.Value = "3" Then
                Session("TID") = hidTId.Value
                ModuleRedirection("Econ3ConnectionString", "E3")

                'ElseIf hidTId.Value = "4" Then
                '    Session("TID") = hidTId.Value
                '    ds = objGetData.GetInuseDetails(Session("UserId").ToString(), "EconConnectionString")
                '    If CInt(ds.Tables(0).Rows(0).Item("count")) > 0 Then
                '        Response.Redirect("Universal_loginN/Pages/ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE115").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&Schema=" + obj.Encrypt("EconConnectionString").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!"))
                '    Else
                '        dsLicenseUser = objGetData.ValidateLicenseUser(Session("USERID").ToString(), "EconConnectionString", "Econ")
                '        If dsLicenseUser.Tables(0).Rows.Count > 0 Then
                '            'Inserting Data in Inuse table
                '            objUpdateData.InuseLoginInsert(Session.SessionID, Session("USERID").ToString(), Session("LicenseNo").ToString().Replace("'", "''"), "EconConnectionString")
                '            Response.Redirect("~/Session.aspx?ID=Chart&UserName='" + Session("UserName").ToString() + "'", True)
                '        Else
                '            Page.ClientScript.RegisterStartupScript(Me.GetType(), "LicenAgreemnt", "javascript:window.open('/Universal_loginN/Pages/EconLicenseAgreement.aspx?ServID=" + obj.Encrypt(hidTId.Value).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "','_blank');", True)
                '        End If
                '    End If

            ElseIf hidTId.Value = "5" Then
                Session("Service") = Nothing
                Session("TID") = hidTId.Value
                ModuleRedirection("Sustain1ConnectionString", "S1")

            ElseIf hidTId.Value = "6" Then
                Session("TID") = hidTId.Value
                ModuleRedirection("Sustain2ConnectionString", "S2")

            ElseIf hidTId.Value = "7" Then
                Session("TID") = hidTId.Value
                ModuleRedirection("Echem1ConnectionString", "EC1")

            ElseIf hidTId.Value = "8" Then
                Session("TID") = hidTId.Value
                ModuleRedirection("Schem1ConnectionString", "SC1")

            ElseIf hidTId.Value = "9" Then
                Session("TID") = hidTId.Value
                ModuleRedirection("Sustain3ConnectionString", "S3")

            ElseIf hidTId.Value = "10" Then
                Session("TID") = hidTId.Value
                ModuleRedirection("Sustain4ConnectionString", "S4")

            ElseIf hidTId.Value = "11" Then
                Session("TID") = hidTId.Value
                ModuleRedirection("Econ4ConnectionString", "E4")

            ElseIf hidTId.Value = "12" Then
                Session("TID") = hidTId.Value
                ModuleRedirection("Market1ConnectionString", "M1")

            ElseIf hidTId.Value = "23" Then
                Session("TID") = hidTId.Value
                ModuleRedirection("SpecConnectionString", "SPEC")
            ElseIf hidTId.Value = "18" Then
                Session("TID") = hidTId.Value
                ModuleRedirection("DistributionConnectionString", "DIST")
            ElseIf hidTId.Value = "19" Then
                Session("TID") = hidTId.Value
                ModuleRedirection("RetailConnectionString", "RETL")
            ElseIf hidTId.Value = "22" Then
                Session("TID") = hidTId.Value
                ModuleRedirection("SDistributionConnectionString", "SDIST")
                'Change for Value Chain
            ElseIf hidTId.Value = "24" Then
                Session("TID") = hidTId.Value
                ModuleRedirection("VChainConnectionString", "VChain")
                'For Old Asp Pages
                'ElseIf hidTId.Value = "7" Or hidTId.Value = "14" Or hidTId.Value = "15" Or hidTId.Value = "16" Or hidTId.Value = "17" Then
                '    Response.Redirect("../../Econ/security.asp?ID=" + Session("ID").ToString() + "&M1=" + hidTId.Value.ToString(), True)
                'Comp Module E1/S1
            ElseIf hidTId.Value = "26" Then
                Session("TID") = hidTId.Value
                Session("Service") = "COMP"
                ModuleRedirection("EconConnectionString", "COMP")
            ElseIf hidTId.Value = "27" Then
                Session("TID") = hidTId.Value
                Session("Service") = "COMPS1"
                ModuleRedirection("Sustain1ConnectionString", "COMPS1")
            ElseIf hidTId.Value = "28" Then
                Session("TID") = hidTId.Value
                Session("Service") = "SBASSIST"
                ModuleRedirection("SBAConnectionString", "StandAssist")
            ElseIf hidTId.Value = "29" Then
                Session("TID") = hidTId.Value
                ModuleRedirection("MedEcon1ConnectionString", "MEDECON1")
            ElseIf hidTId.Value = "30" Then
                Session("TID") = hidTId.Value
                ModuleRedirection("MedEcon2ConnectionString", "MEDECON2")
            ElseIf hidTId.Value = "31" Then
                Session("TID") = hidTId.Value
                ModuleRedirection("MedSustain1ConnectionString", "MEDSUSTAIN1")
            ElseIf hidTId.Value = "32" Then
                Session("TID") = hidTId.Value
                ModuleRedirection("MedSustain2ConnectionString", "MEDSUSTAIN2")
            ElseIf hidTId.Value = "35" Then
                Session("TID") = hidTId.Value
                ModuleRedirection("MoldE1ConnectionString", "MOLDE1")
            ElseIf hidTId.Value = "36" Then
                Session("TID") = hidTId.Value
                ModuleRedirection("MoldE2ConnectionString", "MOLDE2")
            ElseIf hidTId.Value = "37" Then
                Session("TID") = hidTId.Value
                ModuleRedirection("MoldS1ConnectionString", "MOLDS1")
            ElseIf hidTId.Value = "38" Then
                Session("TID") = hidTId.Value
                ModuleRedirection("MoldS2ConnectionString", "MOLDS2")
            ElseIf hidTId.Value = "40" Then
                Session("TID") = hidTId.Value
                ModuleRedirection("EmonitorConnectionString", "EMON")
            ElseIf hidTId.Value = "41" Then
                Session("TID") = hidTId.Value
                ModuleRedirection("SavvyPackProConnectionString", "IContract")
            End If

        Catch ex As Exception

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

            'Old Code: Inuse table check Start
            'If modName = "COMP" Or modName = "COMPS1" Then
            '    dsUser = objGetData.GetInuseDetailsByCompUser(Session("UserId").ToString(), strCon)
            'Else
            '    dsUser = objGetData.GetInuseDetailsByUser(Session("UserId").ToString(), strCon)
            'End If


            'Check License Entry first in Licnese table
            'dsLicenseUser = objGetData.ValidateLicenseUser(Session("USERID").ToString(), strCon, modName)
            'If dsLicenseUser.Tables(0).Rows.Count > 0 Then
            '    If CInt(dsUser.Tables(0).Rows(0).Item("count")) = 0 Then

            '        ds = objGetData.GetInuseDetailsByLic(Session("UserId").ToString())
            '        If CInt(ds.Tables(0).Rows(0).Item("count")) >= CInt(Session("MaxLicCount")) Then
            '            'Response.Redirect("Universal_loginN/Pages/ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE115").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&Schema=" + obj.Encrypt(strCon).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!"))
            '            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "javascript:window.open('Universal_loginN/Pages/ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE115").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&Schema=" + obj.Encrypt(strCon).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&ServID=" + obj.Encrypt(hidTId.Value).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "','_blank');", True)
            '        Else
            '            'Inserting Data in Inuse table
            '            If modName = "CONTR" Then
            '                objUpdateData.InuseLoginInsertContract(Session.SessionID, Session("UserId").ToString(), Session("LicenseNo").ToString().Replace("'", "''"), Request.ServerVariables("REMOTE_ADDR").ToString(), Request.ServerVariables("HTTP_ACCEPT_LANGUAGE").ToString(), Request.ServerVariables("HTTP_USER_AGENT").ToString(), strCon)
            '                dsCont = objGetData.GetSelectionData(Session("USERID").ToString(), Session("Password").ToString(), strCon)
            '                If dsCont.Tables(0).Rows.Count = 0 Then
            '                    objUpdateData.InsertSelection(Session("USERID").ToString(), Session("Password").ToString(), strCon)
            '                End If
            '            ElseIf modName = "PACKPROD" Then
            '                objUpdateData.InuseLoginInsertPackProd(Session.SessionID, Session("USERID").ToString(), Session("LicenseNo").ToString().Replace("'", "''"), Request.ServerVariables("REMOTE_ADDR").ToString(), Request.ServerVariables("HTTP_ACCEPT_LANGUAGE").ToString(), Request.ServerVariables("HTTP_USER_AGENT").ToString(), strCon)
            '                dsCont = objGetData.GetSelectionData(Session("USERID").ToString(), Session("Password").ToString(), strCon)
            '                If dsCont.Tables(0).Rows.Count = 0 Then
            '                    objUpdateData.InsertSelection(Session("USERID").ToString(), Session("Password").ToString(), strCon)
            '                End If
            '            Else
            '                If modName = "COMP" Or modName = "COMPS1" Then
            '                    objUpdateData.InuseCompLoginInsert(Session.SessionID, Session("USERID").ToString(), Session("LicenseNo").ToString().Replace("'", "''"), strCon)
            '                Else
            '                    objUpdateData.InuseLoginInsert(Session.SessionID, Session("USERID").ToString(), Session("LicenseNo").ToString().Replace("'", "''"), strCon)
            '                End If
            '            End If
            '            Page.ClientScript.RegisterStartupScript(Me.GetType(), "UserServ", "javascript:window.open('Session.aspx?Moduls=" + modName + "&ID=" + "','_blank');", True)
            '        End If
            '    Else
            '        dsId = objGetData.ValidateID(Session("UserName"))
            '        If dsId.Tables(0).Rows(0).Item("ISINTERNALUSR").ToString() = "Y" Then
            '            ds = objGetData.GetInuseDetailsByLic(Session("UserId").ToString())
            '            If CInt(ds.Tables(0).Rows(0).Item("count")) > CInt(Session("MaxLicCount")) Then
            '                'Response.Redirect("Universal_loginN/Pages/ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE115").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&Schema=" + obj.Encrypt(strCon).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!"))
            '                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "javascript:window.open('Universal_loginN/Pages/ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE115").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&Schema=" + obj.Encrypt(strCon).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&ServID=" + obj.Encrypt(hidTId.Value).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "','_blank');", True)
            '            Else
            '                If modName = "COMP" Or modName = "COMPS1" Then
            '                    objUpdateData.InuseCompLoginInsert(Session.SessionID, Session("USERID").ToString(), Session("LicenseNo").ToString().Replace("'", "''"), strCon)
            '                Else
            '                    objUpdateData.InuseLoginInsert(Session.SessionID, Session("USERID").ToString(), Session("LicenseNo").ToString().Replace("'", "''"), strCon)
            '                End If
            '                Page.ClientScript.RegisterStartupScript(Me.GetType(), "UserServ", "javascript:window.open('Session.aspx?Moduls=" + modName + "&ID=" + "','_blank');", True)
            '            End If
            '        Else
            '            'Response.Redirect("Universal_loginN/Pages/ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE115").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&Schema=" + obj.Encrypt(strCon).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!"))
            '            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "javascript:window.open('Universal_loginN/Pages/ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE115").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&Schema=" + obj.Encrypt(strCon).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&ServID=" + obj.Encrypt(hidTId.Value).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "','_blank');", True)
            '        End If
            '    End If
            'Else
            '    If modName = "CONTR" Then
            '        Page.ClientScript.RegisterStartupScript(Me.GetType(), "LicenAgreemnt", "javascript:window.open('/Universal_loginN/Pages/ContLicenseAgr.aspx?ServID=" + obj.Encrypt(hidTId.Value).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&Schema=" + obj.Encrypt(strCon).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "','_blank');", True)
            '    ElseIf modName = "PACKPROD" Then
            '        Page.ClientScript.RegisterStartupScript(Me.GetType(), "LicenAgreemnt", "javascript:window.open('/Universal_loginN/Pages/PProdLicenseAgr.aspx?ServID=" + obj.Encrypt(hidTId.Value).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&Schema=" + obj.Encrypt(strCon).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "','_blank');", True)
            '    Else
            '        Page.ClientScript.RegisterStartupScript(Me.GetType(), "LicenAgreemnt", "javascript:window.open('/Universal_loginN/Pages/EconLicenseAgreement.aspx?ServID=" + obj.Encrypt(hidTId.Value).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&Schema=" + obj.Encrypt(strCon).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "','_blank');", True)
            '    End If
            'End If
            'Old Code: Inuse table check End

            dsLicenseUser = objGetData.ValidateLicenseUser(Session("USERID").ToString(), strCon, modName)
            If dsLicenseUser.Tables(0).Rows.Count > 0 Then
                ds = objGetData.GetInuseDetailsByLic(Session("UserId").ToString())
                If CInt(ds.Tables(0).Rows(0).Item("count")) >= CInt(Session("MaxLicCount")) Then
                    'Response.Redirect("Universal_loginN/Pages/ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE115").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&Schema=" + obj.Encrypt(strCon).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!"))
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "javascript:window.open('Universal_loginN/Pages/ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE115").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&Schema=" + obj.Encrypt(strCon).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&ServID=" + obj.Encrypt(hidTId.Value).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "','_blank');", True)
                Else
                    'Delete data from Inuse Tbale
                    objUpdateData.DelInUseEntry(Session("USERID").ToString(), modName)

                    'Inserting Data in Inuse table
                    If modName = "COMP" Or modName = "COMPS1" Then
                        objUpdateData.InuseCompLoginInsert(Session.SessionID, Session("USERID").ToString(), Session("LicenseNo").ToString().Replace("'", "''"), strCon)
                    Else
                        objUpdateData.InuseLoginInsert(Session.SessionID, Session("USERID").ToString(), Session("LicenseNo").ToString().Replace("'", "''"), strCon)
                    End If
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "UserServ", "javascript:window.open('Session.aspx?Moduls=" + modName + "&ID=" + "','_blank');", True)
                End If
            Else
                If modName = "CONTR" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "LicenAgreemnt", "javascript:window.open('/Universal_loginN/Pages/ContLicenseAgr.aspx?ServID=" + obj.Encrypt(hidTId.Value).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&Schema=" + obj.Encrypt(strCon).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "','_blank');", True)
                ElseIf modName = "PACKPROD" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "LicenAgreemnt", "javascript:window.open('/Universal_loginN/Pages/PProdLicenseAgr.aspx?ServID=" + obj.Encrypt(hidTId.Value).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&Schema=" + obj.Encrypt(strCon).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "','_blank');", True)
                Else
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "LicenAgreemnt", "javascript:window.open('/Universal_loginN/Pages/EconLicenseAgreement.aspx?ServID=" + obj.Encrypt(hidTId.Value).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&Schema=" + obj.Encrypt(strCon).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "','_blank');", True)
                End If
            End If
        Catch ex As Exception
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

    Protected Sub btnCntr_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCntr.Click
        Dim objGetData As New LoginGetData.Selectdata()
        Dim ds As New DataSet()
        Try
            If hidTId.Value = "21" Then
                Session("TID") = hidTId.Value
                ModuleKnowBaseRedirection("ContractConnectionString", "CONTR")
            ElseIf hidTId.Value = "20" Then
                Session("TID") = hidTId.Value
                ModuleKnowBaseRedirection("PackProdConnectionString", "PACKPROD")
            End If
            'Response.Redirect("../../Contract/security.asp?ID=" + Session("ID").ToString() + "&C1=" + ddlKBase.SelectedItem.Value.ToString(), True)
        Catch ex As Exception

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

            'Old Code: Inuse table check Start
            'dsUser = objGetData.GetInuseDetailsByUser(Session("UserId").ToString(), strCon)

            'Check License Entry first in Licnese table
            'dsLicenseUser = objGetData.ValidateLicenseUser(Session("USERID").ToString(), strCon, modName)
            'If dsLicenseUser.Tables(0).Rows.Count > 0 Then
            '    If CInt(dsUser.Tables(0).Rows(0).Item("count")) = 0 Then
            '        ds = objGetData.GetInuseDetails(Session("UserId").ToString(), strCon)
            '        If CInt(ds.Tables(0).Rows(0).Item("count")) >= CInt(Session("MaxLicKCount")) Then
            '            'Response.Redirect("Universal_loginN/Pages/ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE115").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&Schema=" + obj.Encrypt(strCon).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!"))
            '            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "javascript:window.open('Universal_loginN/Pages/ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE115").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&Schema=" + obj.Encrypt(strCon).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&ServID=" + obj.Encrypt(hidTId.Value).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "','_blank');", True)
            '        Else
            '            'Inserting Data in Inuse table
            '            If modName = "CONTR" Then
            '                objUpdateData.InuseLoginInsertContract(Session.SessionID, Session("USERID").ToString(), Session("LicenseNo").ToString().Replace("'", "''"), Request.ServerVariables("REMOTE_ADDR").ToString(), Request.ServerVariables("HTTP_ACCEPT_LANGUAGE").ToString(), Request.ServerVariables("HTTP_USER_AGENT").ToString(), strCon)
            '                dsCont = objGetData.GetSelectionData(Session("USERID").ToString(), Session("Password").ToString(), strCon)
            '                If dsCont.Tables(0).Rows.Count = 0 Then
            '                    objUpdateData.InsertSelection(Session("USERID").ToString(), Session("Password").ToString(), strCon)
            '                End If
            '            ElseIf modName = "PACKPROD" Then
            '                objUpdateData.InuseLoginInsertPackProd(Session.SessionID, Session("USERID").ToString(), Session("LicenseNo").ToString().Replace("'", "''"), Request.ServerVariables("REMOTE_ADDR").ToString(), Request.ServerVariables("HTTP_ACCEPT_LANGUAGE").ToString(), Request.ServerVariables("HTTP_USER_AGENT").ToString(), strCon)
            '                dsCont = objGetData.GetSelectionData(Session("USERID").ToString(), Session("Password").ToString(), strCon)
            '                If dsCont.Tables(0).Rows.Count = 0 Then
            '                    objUpdateData.InsertSelection(Session("USERID").ToString(), Session("Password").ToString(), strCon)
            '                End If
            '            End If
            '            Page.ClientScript.RegisterStartupScript(Me.GetType(), "UserServ", "javascript:window.open('Session.aspx?Moduls=" + modName + "&ID=" + "','_blank');", True)
            '        End If
            '    Else
            '        'Response.Redirect("Universal_loginN/Pages/ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE115").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&Schema=" + obj.Encrypt(strCon).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!"))
            '        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "javascript:window.open('Universal_loginN/Pages/ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE115").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&Schema=" + obj.Encrypt(strCon).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&ServID=" + obj.Encrypt(hidTId.Value).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "','_blank');", True)
            '    End If
            'Else
            '    If modName = "CONTR" Then
            '        Page.ClientScript.RegisterStartupScript(Me.GetType(), "UserServ", "javascript:window.open('Universal_loginN/Pages/ContLicenseAgr.aspx?ServID=" + obj.Encrypt(hidTId.Value).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&Schema=" + obj.Encrypt(strCon).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "','_blank');", True)
            '    ElseIf modName = "PACKPROD" Then
            '        Page.ClientScript.RegisterStartupScript(Me.GetType(), "UserServ", "javascript:window.open('Universal_loginN/Pages/PProdLicenseAgr.aspx?ServID=" + obj.Encrypt(hidTId.Value).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&Schema=" + obj.Encrypt(strCon).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "','_blank');", True)
            '    Else
            '        Page.ClientScript.RegisterStartupScript(Me.GetType(), "UserServ", "javascript:window.open('Universal_loginN/Pages/EconLicenseAgreement.aspx?ServID=" + obj.Encrypt(hidTId.Value).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&Schema=" + obj.Encrypt(strCon).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "','_blank');", True)
            '    End If
            'End If
            'Old Code: Inuse table check End

            dsLicenseUser = objGetData.ValidateLicenseUser(Session("USERID").ToString(), strCon, modName)
            If dsLicenseUser.Tables(0).Rows.Count > 0 Then
                ds = objGetData.GetInuseDetails(Session("UserId").ToString(), strCon)
                If CInt(ds.Tables(0).Rows(0).Item("count")) >= CInt(Session("MaxLicKCount")) Then
                    'Response.Redirect("Universal_loginN/Pages/ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE115").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&Schema=" + obj.Encrypt(strCon).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!"))
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "javascript:window.open('Universal_loginN/Pages/ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE115").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&Schema=" + obj.Encrypt(strCon).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&ServID=" + obj.Encrypt(hidTId.Value).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "','_blank');", True)
                Else
                    'Delete data from Inuse Tbale
                    objUpdateData.DelInUseEntry(Session("USERID").ToString(), modName)

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
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "UserServ", "javascript:window.open('Session.aspx?Moduls=" + modName + "&ID=" + "','_blank');", True)
                End If
            Else
                If modName = "CONTR" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "UserServ", "javascript:window.open('Universal_loginN/Pages/ContLicenseAgr.aspx?ServID=" + obj.Encrypt(hidTId.Value).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&Schema=" + obj.Encrypt(strCon).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "','_blank');", True)
                ElseIf modName = "PACKPROD" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "UserServ", "javascript:window.open('Universal_loginN/Pages/PProdLicenseAgr.aspx?ServID=" + obj.Encrypt(hidTId.Value).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&Schema=" + obj.Encrypt(strCon).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "','_blank');", True)
                Else
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "UserServ", "javascript:window.open('Universal_loginN/Pages/EconLicenseAgreement.aspx?ServID=" + obj.Encrypt(hidTId.Value).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&Schema=" + obj.Encrypt(strCon).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "','_blank');", True)
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnMrkt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMrkt.Click
        Dim objGetData As New LoginGetData.Selectdata()
        Dim objUpdateData As New LoginUpdateData.Selectdata()
        Dim obj As New CryptoHelper
        Dim ds, dsLic, dsUser As New DataSet()
        Dim dsLicenseUser As New DataSet()
        Try
            'Old Code: Inuse table check Start
            'dsLic = objGetData.GetMaxCountByLic(Session("UserId").ToString(), "Market1ConnectionString")
            'ds = objGetData.GetInuseDetailsByUserMKT(Session("UserId").ToString(), "Market1ConnectionString")
            'Session("MaxLicCount") = dsLic.Tables(0).Rows(0).Item("MAXCOUNT")

            'Check License Entry first in Licnese table
            'dsLicenseUser = objGetData.ValidateLicenseUser(Session("USERID").ToString(), "Market1ConnectionString", "Market1")
            'If dsLicenseUser.Tables(0).Rows.Count > 0 Then
            '    If CInt(ds.Tables(0).Rows(0).Item("count")) = 0 Then
            '        'Inserting Data in Inuse table
            '        objUpdateData.InuseLoginInsert(Session.SessionID, Session("USERID").ToString(), Session("LicenseNo").ToString().Replace("'", "''"), "Market1ConnectionString")
            '        Session("M1ServiceID") = hidTId.Value
            '        Session("TID") = hidTId.Value
            '        Page.ClientScript.RegisterStartupScript(Me.GetType(), "UserServ", "javascript:window.open('Pages/Market1Sub/Default.aspx');", True)
            '    Else
            '        'Response.Redirect("Universal_loginN/Pages/ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE115").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&Schema=" + obj.Encrypt("Market1ConnectionString").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!"))
            '        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "javascript:window.open('Universal_loginN/Pages/ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE115").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&Schema=" + obj.Encrypt("Market1ConnectionString").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&ServID=" + obj.Encrypt(hidTId.Value).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "','_blank');", True)
            '    End If
            'Else
            '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "UserServ", "javascript:window.open('Universal_loginN/Pages/MarketLicenseAgreement.aspx?ServID=" + obj.Encrypt(hidTId.Value).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&Schema=" + obj.Encrypt("Market1ConnectionString").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "');", True)
            'End If
            'Old Code: Inuse table check End

            dsLic = objGetData.GetMaxCountByLic(Session("UserId").ToString(), "Market1ConnectionString")
            ds = objGetData.GetInuseDetailsByUserMKT(Session("UserId").ToString(), "Market1ConnectionString")
            Session("MaxLicMCount") = dsLic.Tables(0).Rows(0).Item("MRKTMAXCOUNT")


            dsLicenseUser = objGetData.ValidateLicenseUser(Session("USERID").ToString(), "Market1ConnectionString", "Market1")
            If dsLicenseUser.Tables(0).Rows.Count > 0 Then
                If CInt(ds.Tables(0).Rows(0).Item("count")) >= CInt(Session("MaxLicMCount")) Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "javascript:window.open('Universal_loginN/Pages/ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE115").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&Schema=" + obj.Encrypt("Market1ConnectionString").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&ServID=" + obj.Encrypt(hidTId.Value).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "','_blank');", True)
                Else
                    'Delete data from Inuse Tbale
                    objUpdateData.DelInUseEntry(Session("USERID").ToString(), "M1")

                    'Inserting Data in Inuse table
                    objUpdateData.InuseLoginInsert(Session.SessionID, Session("USERID").ToString(), Session("LicenseNo").ToString().Replace("'", "''"), "Market1ConnectionString")
                    Session("M1ServiceID") = hidTId.Value
                    Session("TID") = hidTId.Value
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "UserServ", "javascript:window.open('Pages/Market1Sub/Default.aspx');", True)
                End If
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "UserServ", "javascript:window.open('Universal_loginN/Pages/MarketLicenseAgreement.aspx?ServID=" + obj.Encrypt(hidTId.Value).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&Schema=" + obj.Encrypt("Market1ConnectionString").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "');", True)
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class

