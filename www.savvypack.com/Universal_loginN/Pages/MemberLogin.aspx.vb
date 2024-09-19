Imports System.Data
Imports System.Data.OleDb
Imports System
Imports LoginGetData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Imports System.Net.Mail
Imports System.Net

Partial Class Universal_loginN_Pages_MemberLogin
    Inherits System.Web.UI.Page
    Protected Shared Count As Integer = 1
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            lblTag.Text = "Copyright 1997 - " + Now.Year().ToString() + " SavvyPack Corporation"
        Catch ex As Exception
        End Try
        txtUserName.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnSubmit.ClientID + "')")
        txtPass.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnSubmit.ClientID + "')")

        'txtCUserName.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnCSubmit.ClientID + "')")
        'txtCPass.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnCSubmit.ClientID + "')")
        Dim objGetData As New LoginGetData.Selectdata()
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
                            Response.Redirect("https://www.savvypack.com/Universal_loginN/Pages/UniversalM.aspx", True)
                            'Response.Redirect("UniversalM.aspx", True)
                        Else
                            Response.Redirect("UniversalM.aspx", True)
                        End If
                    Else
                        Session("Back") = Nothing
                        Session("SBack") = "Secure"
                        Response.Redirect("UniversalMgr.aspx", True)
                    End If

                End If

            Else
                Dim ds As New DataSet()
                Dim obj As New CryptoHelper
                If txtUserName.Text.Trim() <> "" Then
                    ds = objGetData.ValidateUser(txtUserName.Text.Trim(), txtPass.Text.Trim())
                    If ds.Tables(0).Rows.Count = 0 Then
                        Session("BackVal") = "1"
                        Response.Redirect("ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                    End If
                Else
                    If Session("BackVal") <> "1" Then
                        UpdateLoginDetailsByUserID()
                    Else
                        Session("BackVal") = Nothing
                    End If

                End If


            End If

        End If

        If Not IsPostBack Then
            txtUserName.Focus()
        Else
            If Not String.IsNullOrEmpty(txtPass.Text.Trim()) Then
                txtPass.Attributes.Add("value", txtPass.Text)
            End If
        End If
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Dim objGetData As New LoginGetData.Selectdata()
        Dim obj As New CryptoHelper
        Dim ds As New DataSet()
        Try
            ds = objGetData.ValidateUSRByUser(txtUserName.Text.Trim())
            If ds.Tables(0).Rows.Count > 0 Then
                If ds.Tables(0).Rows(0).Item("LOCKDATE").ToString() <> "" Then
                    If DateDiff(DateInterval.Minute, CDate(ds.Tables(0).Rows(0).Item("LOCKDATE")), Date.Now) > 15 Then
                        Dim objVUpdateData As New LoginUpdateData.Selectdata()
                        objVUpdateData.UUnlockAccount(txtUserName.Text, "N")
                    End If
                End If

                ds = objGetData.ValidateUSRByUser(txtUserName.Text.Trim())
                If ds.Tables(0).Rows.Count > 0 Then
                    If ds.Tables(0).Rows(0).Item("ISLOCK").ToString = "Y" Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Your account has been locked due to multiple failed login attempts. You may login after 15 minutes.');", True)
                        txtUserName.Focus()
                    Else
                        UpdateLoginDetails()
                    End If
                End If
            Else
                Dim dsU As New DataSet
                dsU = objGetData.ValidateUserSavvy(txtUserName.Text.Trim(), txtPass.Text.Trim().Replace("'", "''"))
                If dsU.Tables(0).Rows.Count > 0 Then
                    If dsU.Tables(0).Rows(0).Item("ISVALIDEMAIL").ToString() = "Y" And dsU.Tables(0).Rows(0).Item("ISAPPROVED").ToString() = "Y" Then
                        Session("UserId") = dsU.Tables(0).Rows(0).Item("USERID").ToString()
                        Session("UserName") = dsU.Tables(0).Rows(0).Item("USERNAME").ToString()
                        Session("Password") = dsU.Tables(0).Rows(0).Item("PASSWORD").ToString()
                        Session("LicenseNo") = Nothing
                        Session("Back") = Nothing
                        Session("SBack") = "Secure"
                        Response.Redirect("UniversalMgr.aspx", True)
                    ElseIf dsU.Tables(0).Rows(0).Item("ISVALIDEMAIL").ToString() = "Y" And dsU.Tables(0).Rows(0).Item("ISAPPROVED").ToString() <> "Y" Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Your account is being reviewed for approval, and you will be notified when your account is activiated.');", True)
                    Else
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('The verification of your email address is incomplete, and the verification must be completed to use your account.  Please click my account on the Home Page, enter my account with your ID and password, and click on the submit button to re-start the verification process.');", True)
                    End If
                Else
                    Response.Redirect("ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                End If
                'Response.Redirect("ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
            End If
            'UpdateLoginDetails()
        Catch ex As Exception
            lblError.Text = "Error:btnSubmit_Click:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub UpdateLoginDetails()
        Dim objGetData As New LoginGetData.Selectdata()
        Dim ds As New DataSet()
        Dim dsUId As New DataSet()
        Dim obj As New CryptoHelper
        Dim objVGetData As New UsersGetData.Selectdata()
        Dim dsVer, dsVerf, dsEmail As New DataSet()
        Dim DsSecLvl As New DataSet()
        Dim strLink As String = ""
        Dim strBody As String = ""
        Dim objVUpdateData As New UsersUpdateData.UpdateInsert
        Dim Days As Integer
        Dim UserName As String
        Dim dsver1, dsPer As New DataSet()
        Try
            dsVer = objVGetData.ValidateUser(txtUserName.Text.Trim(), txtPass.Text.Trim().Replace("'", "''"))
            UserName = obj.Encrypt(txtUserName.Text.Trim()).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!")

            If dsVer.Tables(0).Rows.Count > 0 Then
                Count = 0
                Days = DateDiff(DateInterval.Day, CDate(dsVer.Tables(0).Rows(0).Item("VUPDATEDATE")), Date.Now)

                If dsVer.Tables(0).Rows(0).Item("ISVALIDEMAIL").ToString() = "Y" Then
                    ds = objGetData.ValidateUser(txtUserName.Text.Trim(), txtPass.Text.Trim().Replace("'", "''"))
                    DsSecLvl = objGetData.GetSecuirtyDetails(ds.Tables(0).Rows(0).Item("SECURITYLVL").ToString())
                    If ds.Tables(0).Rows.Count > 0 Then
                        If Days > 45 And DsSecLvl.Tables(0).Rows(0).Item("SECLEVEL").ToString() <> "5" Then
                            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Your password has expired, please change your password.');window.open('../../Users_Login/ChangeExPassword.aspx?un=" + UserName + "&SecLvl=" + DsSecLvl.Tables(0).Rows(0).Item("SECLEVEL").ToString() + "');", True)
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Message", "if(confirm('Your password has expired, please change your password.')){window.open('../../Users_Login/ChangeExPassword.aspx?un=" + UserName + "&SecLvl=" + DsSecLvl.Tables(0).Rows(0).Item("SECLEVEL").ToString() + "');}else{document.getElementById('txtUserName').focus();}", True)
                        Else
                            'Get ULogin ID
                            dsUId = objGetData.GetUID()
                            If dsUId.Tables(0).Rows.Count > 0 Then
                                Session("ID") = dsUId.Tables(0).Rows(0).Item("NewId").ToString()
                                'Insert Into ULogin table 
                                Dim objUpdateData As New LoginUpdateData.Selectdata()
                                objUpdateData.ULoginInsert(dsUId.Tables(0).Rows(0).Item("NewId").ToString(), ds.Tables(0).Rows(0).Item("USERNAME").ToString(), ds.Tables(0).Rows(0).Item("PASSWORD").ToString())
                            End If
                            Session("UserId") = ds.Tables(0).Rows(0).Item("USERID").ToString()
                            Session("UserName") = ds.Tables(0).Rows(0).Item("USERNAME").ToString()
                            Session("Password") = ds.Tables(0).Rows(0).Item("PASSWORD").ToString()
                            Session("LicenseNo") = ds.Tables(0).Rows(0).Item("LICENSEID").ToString()
                            Session("SecurityLevel") = DsSecLvl.Tables(0).Rows(0).Item("SECLEVEL").ToString()
                            Session("Back") = "Secure"
                            Session("SBack") = "Secure"
                            dsPer = objGetData.ValidateUserPermissions(Session("UserId").ToString())
                            If dsPer.Tables(0).Rows.Count > 0 Then
                                If Session("SecurityLevel") <> "5" Then            'Code added to implement HTTPS
                                    'Response.Redirect("UniversalM.aspx", True)
                                    Response.Redirect("https://www.savvypack.com/Universal_loginN/Pages/UniversalM.aspx", True)
                                Else
                                    Response.Redirect("UniversalM.aspx", True)
                                End If
                            Else
                                Session("Back") = Nothing
                                Session("SBack") = "Secure"
                                Response.Redirect("UniversalMgr.aspx", True)
                            End If


                        End If
                    Else
                        Response.Redirect("ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                    End If
                End If
            Else
                dsver1 = objGetData.ValidateUSRByUser(txtUserName.Text.Trim())
                DsSecLvl = objGetData.GetSecuirtyDetails(dsver1.Tables(0).Rows(0).Item("SECURITYLVL").ToString())
                If DsSecLvl.Tables(0).Rows(0).Item("SECLEVEL").ToString() <> "5" Then
                    If Count < 5 Then
                        If Session("TempUName") = txtUserName.Text.Trim() Then
                            Count = Count + 1
                        Else
                            Session("TempUName") = txtUserName.Text.Trim()
                            Count = 1
                        End If

                        Response.Redirect("ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                    Else
                        If Count = 5 Then
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Your account has been locked due to multiple failed login attempts. You may login after 15 minutes.');", True)
                            txtUserName.Focus()
                            Count = 1
                            Dim objUpdateData As New LoginUpdateData.Selectdata()
                            objUpdateData.UAccountLocked(txtUserName.Text, "Y", Date.Now)
                        Else
                            Response.Redirect("ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        End If
                        'Response.Redirect("ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                    End If
                Else
                    Response.Redirect("ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                End If
                '
            End If
        Catch ex As Exception
            lblError.Text = "Error:UpdateLoginDetails:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub UpdateLoginDetailsByUserID()
        Dim objGetData As New LoginGetData.Selectdata()
        Dim ds As New DataSet()
        Dim dsUId As New DataSet()
        Dim obj As New CryptoHelper
        Dim dsSec, DsSecLvl As New DataSet()
        Dim ds1, DsSecLvll As DataSet 'start #389
        Dim Days As Integer
        Dim UserName As String = ""
        Dim dsPer As New DataSet()
        Try
            ds = objGetData.GetUserDetailsByID(Session("UserId").ToString())
            Days = DateDiff(DateInterval.Day, CDate(ds.Tables(0).Rows(0).Item("VUPDATEDATE")), Date.Now) 'start #389
            ds1 = objGetData.ValidateUser(ds.Tables(0).Rows(0).Item("USERNAME").ToString(), ds.Tables(0).Rows(0).Item("PASSWORD").ToString().Replace("'", "''")) 'start #389

            If ds.Tables(0).Rows.Count > 0 Then
                If ds1.Tables(0).Rows.Count > 0 Then
                    DsSecLvll = objGetData.GetSecuirtyDetails(ds1.Tables(0).Rows(0).Item("SECURITYLVL").ToString()) 'start #389
                    txtUserName.Text = ds.Tables(0).Rows(0).Item("USERNAME").ToString() 'start #389
                    txtPass.Text = ds.Tables(0).Rows(0).Item("PASSWORD").ToString()
                    If Days > 45 And DsSecLvll.Tables(0).Rows(0).Item("SECLEVEL").ToString() <> "5" Then 'start #389
                        UserName = obj.Encrypt(txtUserName.Text.Trim()).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!")
                        'Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Your password has expired, please change your password.');window.open('../../Users_Login/ChangeExPassword.aspx?un=" + UserName + "&SecLvl=" + DsSecLvll.Tables(0).Rows(0).Item("SECLEVEL").ToString() + "');", True)
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Message", "if(confirm('Your password has expired, please change your password.')){window.open('../../Users_Login/ChangeExPassword.aspx?un=" + UserName + "&SecLvl=" + DsSecLvll.Tables(0).Rows(0).Item("SECLEVEL").ToString() + "');}else{}", True)
                    Else 'start #389
                        'Get ULogin ID
                        dsUId = objGetData.GetUID()
                        If dsUId.Tables(0).Rows.Count > 0 Then
                            Session("ID") = dsUId.Tables(0).Rows(0).Item("NewId").ToString()
                            'Insert Into ULogin table 
                            Dim objUpdateData As New LoginUpdateData.Selectdata()
                            objUpdateData.ULoginInsert(dsUId.Tables(0).Rows(0).Item("NewId").ToString(), ds.Tables(0).Rows(0).Item("USERNAME").ToString(), ds.Tables(0).Rows(0).Item("PASSWORD").ToString())

                        End If
                        Session("UserId") = ds.Tables(0).Rows(0).Item("USERID").ToString()
                        Session("UserName") = ds.Tables(0).Rows(0).Item("USERNAME").ToString()
                        Session("Password") = ds.Tables(0).Rows(0).Item("PASSWORD").ToString()
                        Session("LicenseNo") = ds.Tables(0).Rows(0).Item("LICENSEID").ToString()
                        Session("Back") = "Secure"
                        Session("SBack") = "Secure"
                        dsSec = objGetData.ValidateUser(Session("UserName"), Session("Password"))
                        DsSecLvl = objGetData.GetSecuirtyDetails(dsSec.Tables(0).Rows(0).Item("SECURITYLVL").ToString())
                        Session("SecurityLevel") = DsSecLvl.Tables(0).Rows(0).Item("SECLEVEL").ToString()
                        dsPer = objGetData.ValidateUserPermissions(Session("UserId").ToString())
                        If dsPer.Tables(0).Rows.Count > 0 Then
                            If Session("SecurityLevel") <> "5" Then            'Code added to implement HTTPS
                                'Response.Redirect("UniversalM.aspx", True)
                                Response.Redirect("https://www.savvypack.com/Universal_loginN/Pages/UniversalM.aspx", True)
                            Else
                                Response.Redirect("UniversalM.aspx", True)
                            End If
                        Else
                            Session("Back") = Nothing
                            Session("SBack") = "Secure"
                            Response.Redirect("UniversalMgr.aspx", True)
                        End If

                    End If 'start #389
                Else
                    Dim dsU As New DataSet
                    'dsU = objGetData.ValidateUserSavvy(txtUserName.Text.Trim(), txtPass.Text.Trim().Replace("'", "''"))
                    dsU = objGetData.ValidateUserSavvy(ds.Tables(0).Rows(0).Item("USERNAME").ToString(), ds.Tables(0).Rows(0).Item("PASSWORD").ToString().Replace("'", "''"))

                    Session("UserId") = dsU.Tables(0).Rows(0).Item("USERID").ToString()
                    Session("UserName") = dsU.Tables(0).Rows(0).Item("USERNAME").ToString()
                    Session("Password") = dsU.Tables(0).Rows(0).Item("PASSWORD").ToString()
                    Session("LicenseNo") = Nothing
                    Session("Back") = Nothing
                    Session("SBack") = "Secure"
                    Response.Redirect("UniversalMgr.aspx", True)
                    'Response.Redirect("ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE114").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                End If

            Else
                Response.Redirect("ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
            End If

        Catch ex As Exception
            lblError.Text = "Error:UpdateLoginDetailsByUserID:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Function GetEmailBodyData(ByVal link As String, ByVal Name As String, ByVal USERCONTACTID As String, ByVal EMAILADDRESS As String) As String
        Dim StrSqlBdy As String = ""
        Try
            StrSqlBdy = "<div style='font-family:Verdana;'>  "
            StrSqlBdy = StrSqlBdy + "<table cellpadding='0' cellspacing='0' style='width:650px'> "
            StrSqlBdy = StrSqlBdy + "<tr> "
            StrSqlBdy = StrSqlBdy + "<td><img src='" + link + "/Images/SavvyPackLogo3.gif' /> "
            StrSqlBdy = StrSqlBdy + "<br /> "
            StrSqlBdy = StrSqlBdy + "</td> "
            StrSqlBdy = StrSqlBdy + "</tr> "
            StrSqlBdy = StrSqlBdy + "<tr style='background-color:#336699;height:35px;'> "
            StrSqlBdy = StrSqlBdy + "<td> "
            StrSqlBdy = StrSqlBdy + "<div style='color:white;font-size:18px;font-family:Verdana;font-weight:bold;margin-left:5px;'><b>Please Add this user in Email List</b> </div> "
            StrSqlBdy = StrSqlBdy + "</td> "
            StrSqlBdy = StrSqlBdy + "</tr> "
            StrSqlBdy = StrSqlBdy + "</table> "
            StrSqlBdy = StrSqlBdy + "<div style='margin-top:10px;margin-right:10px;font-family:Verdana;font-size:12px;'> "
            StrSqlBdy = StrSqlBdy + "<table style='font-family:Verdana;width:650px;font-size:12px;border-collapse:collapse' border='1' bordercolor='black' cellpadding='0' cellspacing='0'><tr style='font-weight:bold;text-align:center'><td>User Contact Id</td><td>Name</td><td>Email Address</td></tr> "
            StrSqlBdy = StrSqlBdy + "<tr><td>" + USERCONTACTID + "</td><td>" + Name + "</td><td>" + EMAILADDRESS + "</td></tr> "
            StrSqlBdy = StrSqlBdy + "</table> "
            StrSqlBdy = StrSqlBdy + "<p>Please Add this user in Email List</p> "
            StrSqlBdy = StrSqlBdy + "<br /> "
            StrSqlBdy = StrSqlBdy + "<p> "
            StrSqlBdy = StrSqlBdy + "<div style='font-family:Verdana;font-size:12px;'> "
            StrSqlBdy = StrSqlBdy + "SavvyPack Corporation<br/>2800 East Cliff Road, Suite 140<br/>Burnsville, MN 55337 USA<br/><a style='font-family:verdana;' href='http://www.savvypack.com'>www.savvypack.com</a><br/>Phone: 1-952-405-7500<br/>Fax: 1-952-898-2242</div> "
            StrSqlBdy = StrSqlBdy + "</p> "
            StrSqlBdy = StrSqlBdy + "</div> "

            Return StrSqlBdy
        Catch ex As Exception

            Return StrSqlBdy
        End Try
    End Function
    Public Sub SendEmail(ByVal strBody As String, ByVal code As String)
        Try
            Dim objGetData As New UsersGetData.Selectdata
            Dim ds As New DataSet
            ds = objGetData.GetAlliedMemberMail(code)
            If ds.Tables(0).Rows.Count > 0 Then
                Dim i As Integer
                Dim _To As New MailAddressCollection()
                Dim _From As New MailAddress(ds.Tables(0).Rows(0).Item("FROMADD").ToString(), ds.Tables(0).Rows(0).Item("FROMNAME").ToString())
                Dim _CC As New MailAddressCollection()
                Dim _BCC As New MailAddressCollection()
                Dim Item As MailAddress
                Dim Email As New EmailConfig()
                Dim _Subject As String = ds.Tables(0).Rows(0).Item("SUBJECT").ToString()

                'To's
                If code = "VERFEMAIL" Then
                    Item = New MailAddress(ds.Tables(0).Rows(0).Item("TOADD").ToString(), ds.Tables(0).Rows(0).Item("TONAME").ToString())
                    _To.Add(Item)
                End If


                For i = 1 To 10
                    ' BCC() 's
                    If ds.Tables(0).Rows(0).Item("BCC" + i.ToString()).ToString() <> Nothing Then
                        Item = New MailAddress(ds.Tables(0).Rows(0).Item("BCC" + i.ToString()).ToString(), ds.Tables(0).Rows(0).Item("BCC" + i.ToString() + "NAME").ToString())
                        _BCC.Add(Item)
                    End If
                    If ds.Tables(0).Rows(0).Item("CC" + i.ToString()).ToString() <> Nothing Then
                        Item = New MailAddress(ds.Tables(0).Rows(0).Item("CC" + i.ToString()).ToString(), ds.Tables(0).Rows(0).Item("CC" + i.ToString() + "NAME").ToString())
                        _CC.Add(Item)
                    End If
                Next
                Email.SendMail(_From, _To, _CC, _BCC, strBody, _Subject)
            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub ChangePassword_OLD()
	
        Dim objGetData As New LoginGetData.Selectdata()
        Dim ds As New DataSet()
        Dim dsUId As New DataSet()
        Dim DsSecLvl As New DataSet()
        Dim dsVer, dsVerf, dsEmail As New DataSet()
        Dim obj As New CryptoHelper
        Dim strLink As String = ""
        Dim strBody As String = ""
        Dim objVUpdateData As New UsersUpdateData.UpdateInsert
        Dim objVGetData As New UsersGetData.Selectdata()
        Dim Days As Integer
        Dim UserName As String
        Try
            If txtUserName.Text = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Please enter user name');", True)
                txtUserName.Focus()
            Else
                dsVer = objGetData.ValidateUserByUserName(txtUserName.Text.Trim())
                UserName = obj.Encrypt(txtUserName.Text.Trim()).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!")
                If dsVer.Tables(0).Rows.Count > 0 Then
                    If dsVer.Tables(0).Rows(0).Item("STATUSID").ToString() = 1 Then
                        Days = DateDiff(DateInterval.Day, CDate(dsVer.Tables(0).Rows(0).Item("VUPDATEDATE")), Date.Now)
                        If dsVer.Tables(0).Rows(0).Item("ISVALIDEMAIL").ToString() = "Y" Then
                            ds = objGetData.ValidateUSRByUser(txtUserName.Text.Trim())
                            If ds.Tables(0).Rows.Count > 0 Then
                                DsSecLvl = objGetData.GetSecurityDetails(ds.Tables(0).Rows(0).Item("SECURITYLVL").ToString())
                                dsUId = objGetData.GetUID()
                                If DsSecLvl.Tables(0).Rows(0).Item("SECLEVEL").ToString() <> "5" Then
                                    If Days > 0 Then
                                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "window.open('../../Users_Login/ChangeExPassword.aspx?un=" + UserName + "&SecLvl=" + DsSecLvl.Tables(0).Rows(0).Item("SECLEVEL").ToString() + "');", True)
                                    Else
                                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('You can not change your password now because you changed it recently.');", True)
                                        txtUserName.Focus()
                                    End If
                                Else
                                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "window.open('../../Users_Login/ChangeExPassword.aspx?un=" + UserName + "&SecLvl=" + DsSecLvl.Tables(0).Rows(0).Item("SECLEVEL").ToString() + "');", True)
                                End If
                            Else
                                Response.Redirect("ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                            End If
                        End If
                    ElseIf dsVer.Tables(0).Rows(0).Item("STATUSID").ToString() = 2 Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "ShowAccountUpdation('../../Users_Login/UpdateUsers.aspx?UN=" + obj.Encrypt(txtUserName.Text) + "&Mode=" + obj.Encrypt("Comp") + "');", True)
                    End If
                    
                Else
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "ShowAccountAdd('../../Users_Login/AddEditUser.aspx?Mode=" + obj.Encrypt("Add") + "');", True)
                    'Response.Redirect("ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                End If
            End If

        Catch ex As Exception
            lblError.Text = "Error:ChangePassword:" + ex.Message.ToString()
        End Try
    End Sub
	
	  Protected Sub ChangePassword()
        Dim objGetData As New LoginGetData.Selectdata()
        Dim ds As New DataSet()
        Dim dsUId As New DataSet()
        Dim DsSecLvl As New DataSet()
        Dim dsVer, dsVerf, dsEmail As New DataSet()
        Dim obj As New CryptoHelper
        Dim strLink As String = ""
        Dim strBody As String = ""
        Dim objVUpdateData As New UsersUpdateData.UpdateInsert
        Dim objVGetData As New UsersGetData.Selectdata()
        Dim Days As Integer
        Dim UserName As String
        Try
            If txtUserName.Text = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Please enter user name');", True)
                txtUserName.Focus()
            Else
                dsVer = objGetData.ValidateUserByUserName(txtUserName.Text.Trim())
                UserName = obj.Encrypt(txtUserName.Text.Trim()).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!")
                If dsVer.Tables(0).Rows.Count > 0 Then

                    If dsVer.Tables(0).Rows(0).Item("ISVALIDEMAIL").ToString() = "Y" And dsVer.Tables(0).Rows(0).Item("ISAPPROVED").ToString() = "Y" Then
                        If dsVer.Tables(0).Rows(0).Item("STATUSID").ToString() = 1 Then
                            Days = DateDiff(DateInterval.Day, CDate(dsVer.Tables(0).Rows(0).Item("VUPDATEDATE")), Date.Now)
                            If dsVer.Tables(0).Rows(0).Item("ISVALIDEMAIL").ToString() = "Y" Then
                                ds = objGetData.ValidateUSRWOLICENSE(txtUserName.Text.Trim())
                                If ds.Tables(0).Rows.Count > 0 Then
                                    DsSecLvl = objGetData.GetSecurityDetails(ds.Tables(0).Rows(0).Item("SECURITYLVL").ToString())
                                    dsUId = objGetData.GetUID()
                                    If DsSecLvl.Tables(0).Rows(0).Item("SECLEVEL").ToString() <> "5" Then
                                        If Days > 0 Then
                                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "window.open('../../Users_Login/ChangeExPassword.aspx?un=" + UserName + "&SecLvl=" + DsSecLvl.Tables(0).Rows(0).Item("SECLEVEL").ToString() + "');", True)
                                        Else
                                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('You can not change your password now because you changed it recently.');", True)
                                            txtUserName.Focus()
                                        End If
                                    Else
                                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "window.open('../../Users_Login/ChangeExPassword.aspx?un=" + UserName + "&SecLvl=" + DsSecLvl.Tables(0).Rows(0).Item("SECLEVEL").ToString() + "');", True)
                                    End If
                                Else
                                    Response.Redirect("ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                                End If
                            End If
                        ElseIf dsVer.Tables(0).Rows(0).Item("STATUSID").ToString() = 2 Then
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "ShowAccountUpdation('../../Users_Login/UpdateUsers.aspx?UN=" + obj.Encrypt(txtUserName.Text) + "&Mode=" + obj.Encrypt("Comp") + "');", True)
                        End If
                    ElseIf dsVer.Tables(0).Rows(0).Item("ISVALIDEMAIL").ToString() = "Y" And dsVer.Tables(0).Rows(0).Item("ISAPPROVED").ToString() <> "Y" Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Your account is being reviewed for approval, and you will be notified when your account is activiated.');", True)
                    Else
                        If dsVer.Tables(0).Rows(0).Item("STATUSID").ToString() = 2 Then
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "ShowAccountUpdation('../../Users_Login/AddEditUser.aspx?UN=" + obj.Encrypt(txtUserName.Text) + "&Mode=" + obj.Encrypt("Comp") + "');", True)
                        Else
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('The verification of your email address is incomplete, and the verification must be completed to use your account.  Please click my account on the Home Page, enter my account with your ID and password, and click on the submit button to re-start the verification process.');", True)
                        End If
                    End If
                    'sud
                Else
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "ShowAccountAdd('../../Users_Login/AddEditUser.aspx?Mode=" + obj.Encrypt("AddAcc") + "&UName=" + obj.Encrypt(txtUserName.Text) + "');", True)
                    'Response.Redirect("ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error:ChangePassword:" + ex.Message.ToString()
        End Try
    End Sub
End Class
