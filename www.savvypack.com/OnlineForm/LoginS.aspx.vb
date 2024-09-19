Imports System.Data
Imports System.Data.OleDb
Imports System
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Imports ToolCCS
Imports System.Net.Mail
Imports System.Net
Partial Class Users_Login_LoginS
    Inherits System.Web.UI.Page
    Protected Shared Count As Integer = 1
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lbltitle.Text = "Login"
        If Request.QueryString("Savvy") <> "Nothing" Then
            If Request.QueryString("Savvy").ToString() = "Y" Then
                Session("Savvy") = "Y"
            End If

        End If
        If Not IsPostBack Then
            txtEmail.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnSubmit.ClientID + "')")
            txtPass.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnSubmit.ClientID + "')")
            txtEmail.Focus()
        Else
            If Not String.IsNullOrEmpty(txtPass.Text.Trim()) Then
                txtPass.Attributes.Add("value", txtPass.Text)
            End If
        End If
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Dim objGetLoginData As New LoginGetData.Selectdata()
        Dim obj As New CryptoHelper
        Dim ds As New DataSet()
        Dim dsVer As New DataSet()

        Dim objGetData As New UsersGetData.Selectdata()
        Dim objUpdateData As New UsersUpdateData.UpdateInsert
        Dim dsU, dsEmail As New DataSet()
        Dim dsVerf As New DataSet()
        Dim strLink As String = ""
        Dim strBody As String = ""

        Try
            ds = objGetLoginData.ValidateUSRByUser(txtEmail.Text.Trim())
            If ds.Tables(0).Rows.Count > 0 Then
                If ds.Tables(0).Rows(0).Item("LOCKDATE").ToString() <> "" Then
                    If DateDiff(DateInterval.Minute, CDate(ds.Tables(0).Rows(0).Item("LOCKDATE")), Date.Now) > 15 Then
                        Dim objVUpdateData As New LoginUpdateData.Selectdata()
                        objVUpdateData.UUnlockAccount(txtEmail.Text, "N")
                    End If
                    
                End If
                If Session("Savvy") = "Y" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "UserLogin", "closewindow();", True)
                    'Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "window.open('../OnlineForm/ProjectManager.aspx','_blank');", True)
                Else
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "UserLogin", "closewindow();", True)
                End If
                ds = objGetLoginData.ValidateUSRByUser(txtEmail.Text.Trim())
                If ds.Tables(0).Rows.Count > 0 Then
                    If ds.Tables(0).Rows(0).Item("ISLOCK").ToString = "Y" Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Your account has been locked due to multiple failed login attempts. You may login after 15 minutes.');", True)
                        txtEmail.Focus()
                    Else
                        UpdateLoginDetails()
                    End If
                End If
            Else
                dsU = objGetData.ValidateUser(txtEmail.Text.Trim(), txtPass.Text.Trim().Replace("'", "''"))
                If dsU.Tables(0).Rows.Count > 0 Then
                    hdnUTYPE.Value = dsU.Tables(0).Rows(0).Item("UTYPE").ToString()
                    If dsU.Tables(0).Rows(0).Item("ISVALIDEMAIL").ToString() = "Y" And dsU.Tables(0).Rows(0).Item("ISAPPROVED").ToString() = "Y" Then
                        Session("UserId") = dsU.Tables(0).Rows(0).Item("UserID").ToString()
                        Session("UserName") = dsU.Tables(0).Rows(0).Item("USERNAME").ToString()
                        Session("Password") = dsU.Tables(0).Rows(0).Item("PASSWORD").ToString()
                        Session("LicenseNo") = Nothing
                        Session("Back") = Nothing
                        Session("SBack") = "Secure"
                        objUpdateData.LoginLogOutLogInsert(txtEmail.Text.Trim(), Session.SessionID)
                        hidUserID.Value = Session("UserId").ToString()
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "UserLogin", "closewindow();", True)

                    ElseIf dsU.Tables(0).Rows(0).Item("ISVALIDEMAIL").ToString() = "Y" And dsU.Tables(0).Rows(0).Item("ISAPPROVED").ToString() <> "Y" Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Your account is being reviewed for approval. You will be notified when your account is activiated.');", True)
                    Else
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('The verification of your email address is incomplete, and the verification must be completed to use your account.  Click on Verify your email to re-start the verification process.');", True)
                    End If
                Else
                    'Response.Redirect("Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                    dsVer = objGetLoginData.ValidateUserByUserName(txtEmail.Text.Trim())
                    If dsVer.Tables(0).Rows.Count <= 0 Then
                        'Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Your account information is incomplete. Please go through create account process to complete it.');", True)
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('This username does not exist. Please create an account.');", True)
                    ElseIf dsVer.Tables(0).Rows(0).Item("USERNAME").ToString() <> "" And dsVer.Tables(0).Rows(0).Item("STATUSID").ToString() <> 2 Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Password is incorrect');", True)
                    ElseIf dsVer.Tables(0).Rows(0).Item("STATUSID").ToString() = 2 Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "ShowAccountUpdation('../Users_Login/AddEditUser.aspx?UN=" + obj.Encrypt(txtEmail.Text) + "&Mode=" + obj.Encrypt("Comp") + "');", True)
                    End If
                End If
            End If
        Catch ex As Exception

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
            If txtEmail.Text = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Please enter your email address');", True)
                txtEmail.Focus()
            Else
                dsVer = objGetData.ValidateUserByUserName(txtEmail.Text.Trim())
                UserName = obj.Encrypt(txtEmail.Text.Trim()).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!")
                If dsVer.Tables(0).Rows.Count > 0 Then

                    If dsVer.Tables(0).Rows(0).Item("ISVALIDEMAIL").ToString() = "Y" And dsVer.Tables(0).Rows(0).Item("ISAPPROVED").ToString() = "Y" Then
                        If dsVer.Tables(0).Rows(0).Item("STATUSID").ToString() = 1 Then
                            Days = DateDiff(DateInterval.Day, CDate(dsVer.Tables(0).Rows(0).Item("VUPDATEDATE")), Date.Now)
                            If dsVer.Tables(0).Rows(0).Item("ISVALIDEMAIL").ToString() = "Y" Then
                                ds = objGetData.ValidateUSRWOLICENSE(txtEmail.Text.Trim())
                                If ds.Tables(0).Rows.Count > 0 Then
                                    DsSecLvl = objGetData.GetSecurityDetails(ds.Tables(0).Rows(0).Item("SECURITYLVL").ToString())
                                    dsUId = objGetData.GetUID()
                                    If DsSecLvl.Tables(0).Rows(0).Item("SECLEVEL").ToString() <> "5" Then
                                        If Days > 0 Then
                                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "ShowChangePassword('../Users_Login/ChangeExPasswordU.aspx?un=" + UserName + "&SecLvl=" + DsSecLvl.Tables(0).Rows(0).Item("SECLEVEL").ToString() + "');", True)
                                        Else
                                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('You can not change your password now because you changed it recently.');", True)
                                            txtEmail.Focus()
                                        End If
                                    Else
                                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "ShowChangePassword('../Users_Login/ChangeExPasswordU.aspx?un=" + UserName + "&SecLvl=" + DsSecLvl.Tables(0).Rows(0).Item("SECLEVEL").ToString() + "');", True)
                                        'Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "window.open('../Users_Login/ChangeExPasswordU.aspx?un=" + UserName + "&SecLvl=" + DsSecLvl.Tables(0).Rows(0).Item("SECLEVEL").ToString() + "');", True)
                                        'Page.ClientScript.RegisterStartupScript(Page.GetType(), "close", "<script language=javascript>window.opener.location.href='../Users_Login/ChangeExPasswordU.aspx?un=" + UserName + "&SecLvl=" + DsSecLvl.Tables(0).Rows(0).Item("SECLEVEL").ToString() + "';self.close();</script>")
                                    End If
                                Else
                                    Response.Redirect("ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                                End If
                            End If
                        ElseIf dsVer.Tables(0).Rows(0).Item("STATUSID").ToString() = 2 Then
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "ShowAccountUpdation('../Users_Login/AddEditUser.aspx?UN=" + obj.Encrypt(txtEmail.Text) + "&Mode=" + obj.Encrypt("Comp") + "');", True)
                        End If
                    ElseIf dsVer.Tables(0).Rows(0).Item("ISVALIDEMAIL").ToString() = "Y" And dsVer.Tables(0).Rows(0).Item("ISAPPROVED").ToString() <> "Y" Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Your account is being reviewed for approval. You will be notified when your account is activiated.');", True)
                    Else
                        If dsVer.Tables(0).Rows(0).Item("STATUSID").ToString() = 2 Then
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "ShowAccountUpdation('../Users_Login/AddEditUser.aspx?UN=" + obj.Encrypt(txtEmail.Text) + "&Mode=" + obj.Encrypt("Comp") + "');", True)
                        Else
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('The verification of your email address is incomplete, and the verification must be completed to use your account. Click on Verify your email to re-start the verification process.');", True)
                        End If
                    End If
                    'sud
                Else
                    'Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "ShowAccountAdd('../../Users_Login/AddEditUser.aspx?Mode=" + obj.Encrypt("AddAcc") + "&UName=" + obj.Encrypt(txtEmail.Text) + "');", True)
                    'Response.Redirect("ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('This username does not exist. Please create an account.');", True)
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub VerifyEmail()
        Dim objGetLoginData As New LoginGetData.Selectdata()
        Dim objGetData As New UsersGetData.Selectdata()
        Dim objUpdateData As New UsersUpdateData.UpdateInsert
        Dim ds, dsEmail As New DataSet()
        Dim obj As New CryptoHelper
        Dim dsVerf, dsVer As New DataSet()
        Dim strLink As String = ""
        Dim strBody As String = ""

        Dim dsUserData As New DataSet()
        Dim EncEmail, EncUserId, EncType As String
        Dim EmailLink, link As String
        Dim strBodyData As String
        Dim RType As String = ""
        Dim VerfCode As String = Guid.NewGuid.ToString().Trim().Substring(0, 8)
        Try
            ds = objGetData.ValidateUser(txtEmail.Text.Trim(), txtPass.Text.Trim().Replace("'", "''"))
            If ds.Tables(0).Rows.Count > 0 Then
                If ds.Tables(0).Rows(0).Item("ISVALIDEMAIL").ToString() = "Y" And ds.Tables(0).Rows(0).Item("ISAPPROVED").ToString() = "Y" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Your email address has already been verified.');", True)
                ElseIf ds.Tables(0).Rows(0).Item("ISVALIDEMAIL").ToString() = "Y" And ds.Tables(0).Rows(0).Item("ISAPPROVED").ToString() <> "Y" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Your account is being reviewed for approval. You will be notified when your account is activiated.');", True)
                Else
                     If DateDiff(DateInterval.Minute, CDate(ds.Tables(0).Rows(0).Item("VERIFYDATE")), Date.Now) > 10 Then
                        'Get Email Config Details
                        dsEmail = objGetData.GetEmailConfigDetails("Y")
                        dsUserData = objGetData.GetUserId(txtEmail.Text)
                        If ds.Tables(0).Rows.Count > 0 Then
                            link = dsEmail.Tables(0).Rows(0).Item("URL").ToString()
                            If dsUserData.Tables(0).Rows.Count > 0 Then
                                objUpdateData.InsertVerificationDate(dsUserData.Tables(0).Rows(0).Item("UserId").ToString(), VerfCode)
                                EncEmail = obj.Encrypt(dsUserData.Tables(0).Rows(0).Item("EMAILADD").ToString()).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + ""
                                EncUserId = obj.Encrypt(dsUserData.Tables(0).Rows(0).Item("UserId").ToString()).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + ""
                                EncType = obj.Encrypt(RType).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + ""
                                EmailLink = link + "/Users_Login/EmailVarification.aspx?Email=" + EncEmail + "&UserID=" + EncUserId + "&Type=" + EncType + ""
                                strBodyData = GetEmailBodyData(link, EmailLink, VerfCode)
                                'Code for sending Email
                                SendEmail(strBodyData, dsUserData.Tables(0).Rows(0).Item("EMAILADD").ToString(), dsUserData.Tables(0).Rows(0).Item("FNAME").ToString())
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "ShowPopWindowVerf('Verification.aspx?UID=" + obj.Encrypt(dsUserData.Tables(0).Rows(0).Item("UserId").ToString()) + "&Type=ReVerf');", True)
                            End If
                        End If
                    Else
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "ShowPopWindowVerf('Verification.aspx?UID=" + obj.Encrypt(ds.Tables(0).Rows(0).Item("UserID").ToString()) + "&Type=ReVerf');", True)
                    End If
                End If
            Else
                dsVer = objGetLoginData.ValidateUserByUserName(txtEmail.Text.Trim())
                If dsVer.Tables(0).Rows.Count <= 0 Then
                    'Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Your account information is incomplete. Please go through create account process to complete it.');", True)
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('This username does not exist. Please create an account.');", True)
                ElseIf dsVer.Tables(0).Rows(0).Item("USERNAME").ToString() <> "" And dsVer.Tables(0).Rows(0).Item("STATUSID").ToString() <> 2 Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Password is incorrect');", True)
                ElseIf dsVer.Tables(0).Rows(0).Item("STATUSID").ToString() = 2 Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "ShowAccountUpdation('../Users_Login/AddEditUser.aspx?UN=" + obj.Encrypt(txtEmail.Text) + "&Mode=" + obj.Encrypt("Comp") + "');", True)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

#Region "Send Mail"

    Protected Function GetEmailBodyData(ByVal link As String, ByVal EmailLink As String, ByVal VerfCode As String) As String
        Dim StrSql As String = ""
        Try
            StrSql = "<div style='font-family:Verdana;'>  "
            StrSql = StrSql & "<table cellpadding='0' cellspacing='0' style='width:650px'> "
            StrSql = StrSql & "<tr> "
            StrSql = StrSql & "<td><img src='" + link + "/Images/SavvyPackLogo3.gif' /> "
            StrSql = StrSql & "<br /> "
            StrSql = StrSql & "</td> "
            StrSql = StrSql & "</tr> "
            StrSql = StrSql & "<tr style='background-color:#336699;height:35px;'> "
            StrSql = StrSql & "<td> "
            StrSql = StrSql & "<div style='color:white;font-size:18px;font-family:Verdana;font-weight:bold;margin-left:5px;'><b>Email Verification:</b> </div> "
            StrSql = StrSql & "</td> "
            StrSql = StrSql & "</tr> "
            StrSql = StrSql & "</table> "
            StrSql = StrSql & "<div style='margin-top:10px;margin-right:10px;font-family:Verdana;font-size:12px;'> "
            StrSql = StrSql & "Your verification Code is <b>" + VerfCode.Trim() + "</b> "
            StrSql = StrSql & "<p>Please use this code to log into SavvyPack Corporation's web site. During the log in process you will be <br/> prompted to enter this code. When prompted, please enter this code, and your account will be activated immediately.</p> "
            StrSql = StrSql & "<p>Thank you for using SavvyPack Corporation</p> "
            StrSql = StrSql & "<br /> "
            StrSql = StrSql & "<b> Please note that your email address is your UserId"
            StrSql = StrSql & "<p> "
            StrSql = StrSql & "<div style='font-family:Verdana;font-size:12px;'> "
            StrSql = StrSql + "SavvyPack Corporation<br/>1850 East 121st Street Suite 106B<br/>Burnsville, MN USA 55337<br/><a style='font-family:verdana;' href='http://www.savvypack.com'>www.savvypack.com</a><br/>Phone: 1-952-405-7500</div> "
            StrSql = StrSql & "</p> "
            StrSql = StrSql & "</div> "
            Return StrSql
        Catch ex As Exception
            Return StrSql
        End Try
    End Function
    Public Sub SendEmail(ByVal strBody As String, ByVal UserName As String, ByVal FirstName As String)
        Try
            Dim objGetData As New UsersGetData.Selectdata
            Dim ds As New DataSet
            ds = objGetData.GetAlliedMemberMail("EMAILVERF")
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
                Item = New MailAddress(UserName, FirstName)
                _To.Add(Item)

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
            StrSqlBdy = StrSqlBdy + "SavvyPack Corporation<br/>2800 East Cliff Road, Suite 140<br/>Burnsville, MN 55337 USA<br/><a style='font-family:verdana;' href='http://www.savvypack.com'>www.savvypack.com</a><br/>Phone: 1-952-405-7500</div> "
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
#End Region

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
            dsVer = objVGetData.ValidateUser(txtEmail.Text.Trim(), txtPass.Text.Trim().Replace("'", "''"))
            UserName = obj.Encrypt(txtEmail.Text.Trim()).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!")

            If dsVer.Tables(0).Rows.Count > 0 Then
                Count = 0
                Days = DateDiff(DateInterval.Day, CDate(dsVer.Tables(0).Rows(0).Item("VUPDATEDATE")), Date.Now)
 
                If dsVer.Tables(0).Rows(0).Item("ISVALIDEMAIL").ToString() = "Y"  And dsVer.Tables(0).Rows(0).Item("ISAPPROVED").ToString() = "Y" Then
                    ds = objGetData.ValidateUser(txtEmail.Text.Trim(), txtPass.Text.Trim().Replace("'", "''"))
                    DsSecLvl = objGetData.GetSecuirtyDetails(ds.Tables(0).Rows(0).Item("SECURITYLVL").ToString())
                    If ds.Tables(0).Rows.Count > 0 Then
                        If Days > 45 And DsSecLvl.Tables(0).Rows(0).Item("SECLEVEL").ToString() <> "5" Then
                            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Your password has expired, please change your password.');window.open('../../Users_Login/ChangeExPasswordU.aspx?un=" + UserName + "&SecLvl=" + DsSecLvl.Tables(0).Rows(0).Item("SECLEVEL").ToString() + "');", True)
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Message", "if(confirm('Your password has expired, please change your password.')){window.open('../Users_Login/ChangeExPasswordU.aspx?un=" + UserName + "&SecLvl=" + DsSecLvl.Tables(0).Rows(0).Item("SECLEVEL").ToString() + "');}else{document.getElementById('txtEmail').focus();}", True)
                        Else
						'Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinB", "alert('1');", True)
                            'Get ULogin ID
                            dsUId = objGetData.GetUID()
							
                            If dsUId.Tables(0).Rows.Count > 0 Then
							'Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinB", "alert('3');", True)
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
                            hidUserID.Value = Session("UserId").ToString()
                            dsPer = objGetData.ValidateUserPermissions(Session("UserId").ToString())
                            If dsPer.Tables(0).Rows.Count > 0 Then
                                If Session("SecurityLevel") <> "5" Then            'Code added to implement HTTPS
								    'Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinB", "alert('Hi Login Https');", True)
                                    Session("https") = "https"
                                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "UserLogin", "closewindow();", True)
                                Else
                                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "UserLogin", "closewindow();", True)
                                End If
                            Else
                                Session("Back") = Nothing
                                Session("SBack") = "Secure"
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "UserLogin", "closewindow();", True)
                            End If
                        End If
                    Else
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('The EMAIL or PASSWORD that you entered could not be authenticated.');", True)
                    End If
                ElseIf dsVer.Tables(0).Rows(0).Item("ISVALIDEMAIL").ToString() = "Y" And dsVer.Tables(0).Rows(0).Item("ISAPPROVED").ToString() <> "Y" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Your account is being reviewed for approval. You will be notified when your account is activiated.');", True)
                Else
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('The verification of your email address is incomplete, and the verification must be completed to use your account. Click on Verify your email to re-start the verification process.');", True)
                End If
            Else
                dsver1 = objGetData.ValidateUSRByUser(txtEmail.Text.Trim())
                DsSecLvl = objGetData.GetSecuirtyDetails(dsver1.Tables(0).Rows(0).Item("SECURITYLVL").ToString())
                If DsSecLvl.Tables(0).Rows(0).Item("SECLEVEL").ToString() <> "5" Then
                    If Count < 5 Then
                        If Session("TempUName") = txtEmail.Text.Trim() Then
                            Count = Count + 1
                        Else
                            Session("TempUName") = txtEmail.Text.Trim()
                            Count = 1
                        End If
                        'Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('The EMAIL or PASSWORD that you entered could not be authenticated.');", True)
                       Response.Write("<script language=""javascript"">alert('The EMAIL or PASSWORD that you entered could not be authenticated.');</script>")
                    Else
                        If Count = 5 Then
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Your account has been locked due to multiple failed login attempts. You may login after 15 minutes.');", True)
                            txtEmail.Focus()
                            Count = 1
                            Dim objUpdateData As New LoginUpdateData.Selectdata()
                            objUpdateData.UAccountLocked(txtEmail.Text, "Y", Date.Now)
                        Else
                            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('The EMAIL or PASSWORD that you entered could not be authenticated.');", True)
 Response.Write("<script language=""javascript"">alert('The EMAIL or PASSWORD that you entered could not be authenticated.');</script>")
                        End If
                    End If
                Else
                    'Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('The EMAIL or PASSWORD that you entered could not be authenticated.');", True)
 Response.Write("<script language=""javascript"">alert('The EMAIL or PASSWORD that you entered could not be authenticated.');</script>")
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "UserLogin", "closewindow();", True)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnClosePopUp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClosePopUp.Click
        Try
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "UserLogin", "window.close();", True)
        Catch ex As Exception

        End Try
    End Sub

#Region "Verification Button"
    Protected Sub btnCloseCA_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCloseCA.Click
        Try
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "UserLogin", "CloseWindowCA();", True)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Try
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "UserLogin", "CloseWindowP();", True)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnCloseUA_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCloseUA.Click
        Try
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "UserLogin", "CloseWindowUA();", True)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnCloseAcp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCloseAcp.Click
        Try
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "UserLogin", "CloseWindowAcp();", True)
        Catch ex As Exception

        End Try
    End Sub
#End Region

    Protected Sub btnCls_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCls.Click
        Try
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "UserLogin", "window.close();", True)
        Catch ex As Exception

        End Try
    End Sub
End Class
