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
Partial Class Users_Login_AddEditAccount
    Inherits System.Web.UI.Page
	Protected Shared Count As Integer = 1

#Region "Get Set Variables"
    Dim lblError As Label
    Dim _ctlContentPlaceHolder As ContentPlaceHolder



    Public Property ErrorLable() As Label
        Get
            Return lblError
        End Get
        Set(ByVal Value As Label)
            lblError = Value
        End Set
    End Property



    Public Property ctlContentPlaceHolder() As ContentPlaceHolder
        Get
            Return _ctlContentPlaceHolder
        End Get
        Set(ByVal value As ContentPlaceHolder)
            _ctlContentPlaceHolder = value
        End Set
    End Property



#End Region

#Region "MastePage Content Variables"

    Protected Sub GetMasterPageControls()
        Session("MenuItem") = "ACNT"
        GetErrorLable()
        GetContentPlaceHolder()
    End Sub

    Protected Sub GetErrorLable()
        ErrorLable = Page.Master.FindControl("lblError")
    End Sub


    Protected Sub GetContentPlaceHolder()
        ctlContentPlaceHolder = Page.Master.FindControl("Econ1ContentPlaceHolder")
    End Sub

#End Region

#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_USERS_LOGIN_ADDEDITACCOUNT")
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        objRefresh.Render(Page)
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            lbltitle.Text = "Create Account"
            If Not IsPostBack Then
                txtEmail.Focus()
            End If

            GetMasterPageControls()
            If Request.QueryString("Acc").ToString() <> "" Then
                hidAcc.Value = Request.QueryString("Acc").ToString()
            End If
        Catch ex As Exception
            lblError.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    'Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
    '    Dim objGetData As New UsersGetData.Selectdata
    '    Dim objUpdateData As New UsersUpdateData.UpdateInsert
    '    Dim obj As New CryptoHelper
    '    Dim DsUserCheck, DsValidUsr As New DataSet
    '    Try
    '        If txtEmail.Text.Trim.Length <> 0 Then
    '            DsUserCheck = objGetData.GetUsrInfo(txtEmail.Text)
    '            If DsUserCheck.Tables(0).Rows.Count > 0 Then
    '                If txtPass.Text.ToString() <> "" Then
    '                    DsValidUsr = objGetData.ValidateUser(txtEmail.Text.Trim(), txtPass.Text.Trim().Replace("'", "''"))
    '                    If DsValidUsr.Tables(0).Rows.Count > 0 Then
    '                        If DsValidUsr.Tables(0).Rows(0).Item("ISVALIDEMAIL").ToString() = "Y" And DsValidUsr.Tables(0).Rows(0).Item("ISAPPROVED").ToString() = "Y" Then
    '                            Session("UserId") = DsValidUsr.Tables(0).Rows(0).Item("UserID").ToString()
    '                            Session("UserName") = DsValidUsr.Tables(0).Rows(0).Item("USERNAME").ToString()
    '                            Session("Back") = "Secure"
    '                            objUpdateData.LoginLogOutLogInsert(txtEmail.Text.Trim(), Session.SessionID)
    '                            If Session("URL") = "" Or Session("URL") Is Nothing Then
    '                                Response.Redirect("AddEditUser.aspx?Mode=" + obj.Encrypt("Edit").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
    '                            Else
    '                                Response.Redirect(Session("URL").ToString())
    '                            End If
    '                        ElseIf DsValidUsr.Tables(0).Rows(0).Item("ISVALIDEMAIL").ToString() = "Y" And DsValidUsr.Tables(0).Rows(0).Item("ISAPPROVED").ToString() <> "Y" Then
    '                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Your account is being reviewed for approval, and you will be notified when your account is activiated.');", True)
    '                        Else
    '                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('The verification of your email address is incomplete, and the verification must be completed to use your account.  Please click my account on the index page, enter my account with your ID and password, and click on the submit button to re-start the verification process.');", True)
    '                        End If
    '                    Else
    '                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Password is incorrect.');", True)
    '                    End If
    '                ElseIf DsUserCheck.Tables(0).Rows(0).Item("PASSWORD").ToString() = "" Then
    '                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "ShowAccountUpdation('AddEditUser.aspx?UN=" + obj.Encrypt(txtEmail.Text) + "&Mode=" + obj.Encrypt("Comp") + "');", True)
    '                Else
    '                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Password can not be blank.');", True)
    '                End If
    '            Else
    '                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "ShowAccountAdd('AddEditUser.aspx?Mode=" + obj.Encrypt("AddAcc") + "&UName=" + obj.Encrypt(txtEmail.Text) + "');", True)
    '            End If
    '        Else
    '            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Please provide email.');", True)
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = "Error:btnSubmit_Click:" + ex.Message.ToString()
    '    End Try
    'End Sub

     Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Dim objGetData As New UsersGetData.Selectdata
        Dim objUpdateData As New UsersUpdateData.UpdateInsert
        Dim obj As New CryptoHelper
        Dim DsUserCheck, DsValidUsr As New DataSet
		Dim DsLicenseChk As New DataSet
        Dim objGetLoginData As New LoginGetData.Selectdata()
        Dim AccCheckDays As Integer
        Try
            If txtEmail.Text.Trim.Length <> 0 Then
				'27Nov_2017
                DsLicenseChk = objGetLoginData.ValidateUSRByUser(txtEmail.Text.Trim())
                If DsLicenseChk.Tables(0).Rows.Count > 0 Then
                    If txtPass.Text.ToString() <> "" Then
                        If DsLicenseChk.Tables(0).Rows(0).Item("LOCKDATE").ToString() <> "" Then
                            If DateDiff(DateInterval.Minute, CDate(DsLicenseChk.Tables(0).Rows(0).Item("LOCKDATE")), Date.Now) > 15 Then
                                Dim objVUpdateData As New LoginUpdateData.Selectdata()
                                objVUpdateData.UUnlockAccount(txtEmail.Text, "N")
                            End If
                        End If

                        DsLicenseChk = objGetLoginData.ValidateUSRByUser(txtEmail.Text.Trim())
                        If DsLicenseChk.Tables(0).Rows.Count > 0 Then
                            If DsLicenseChk.Tables(0).Rows(0).Item("ISLOCK").ToString = "Y" Then
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Your account has been locked due to multiple failed login attempts. You may login after 15 minutes.');", True)
                                txtEmail.Focus()
                            Else
                                UpdateLoginDetails()
                            End If
                        End If
                    ElseIf DsLicenseChk.Tables(0).Rows(0).Item("STATUSID").ToString() = 2 Then
                        If DsLicenseChk.Tables(0).Rows(0).Item("PASSWORD").ToString() <> "" Then
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('An account already exists for this email address.\nPlease enter your password to login.');", True)
                            trPassword.Visible = True
                            lnkForgotPass.Visible = True
                            txtPass.Focus()
                        Else
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "ShowAccountUpdation('AddEditUser.aspx?UN=" + obj.Encrypt(txtEmail.Text) + "&Mode=" + obj.Encrypt("Comp") + "');", True)
                            trPassword.Visible = False
                            lnkForgotPass.Visible = False
                        End If
                    ElseIf DsLicenseChk.Tables(0).Rows(0).Item("PASSWORD").ToString() <> "" Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('An account already exists for this email address.\nPlease enter your password to login.');", True)
                        trPassword.Visible = True
                        lnkForgotPass.Visible = True
                        txtPass.Focus()
                    Else
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Please enter your Password.');", True)
                    End If
                Else
                    DsUserCheck = objGetData.GetUsrInfo(txtEmail.Text)
                    If DsUserCheck.Tables(0).Rows.Count > 0 Then
                        If txtPass.Text.ToString() <> "" Then
                            DsValidUsr = objGetData.ValidateUser(txtEmail.Text.Trim(), txtPass.Text.Trim().Replace("'", "''"))
                            If DsValidUsr.Tables(0).Rows.Count > 0 Then
                                If DsValidUsr.Tables(0).Rows(0).Item("ISVALIDEMAIL").ToString() = "Y" And DsValidUsr.Tables(0).Rows(0).Item("ISAPPROVED").ToString() = "Y" Then
                                    Session("UserId") = DsValidUsr.Tables(0).Rows(0).Item("UserID").ToString()
                                    Session("UserName") = DsValidUsr.Tables(0).Rows(0).Item("USERNAME").ToString()
                                    Session("Password") = DsValidUsr.Tables(0).Rows(0).Item("PASSWORD").ToString()
                                    Session("LicenseNo") = Nothing
                                    Session("Back") = Nothing
                                    Session("SBack") = "Secure"
                                    'Session("Back") = "Secure"
                                    objUpdateData.LoginLogOutLogInsert(txtEmail.Text.Trim(), Session.SessionID)

                                    'Show Alert of account complition
                                    If DsValidUsr.Tables(0).Rows(0).Item("STATUSID").ToString() = 2 Then
                                        If DsValidUsr.Tables(0).Rows(0).Item("ACCCHECKDATE").ToString() <> "" Then
                                            AccCheckDays = DateDiff(DateInterval.Day, CDate(DsValidUsr.Tables(0).Rows(0).Item("ACCCHECKDATE")), Date.Now)
                                            If AccCheckDays > 365 Then
                                                objUpdateData.UpdateAccCheckDate(Session("UserId"))
                                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "AccCheck_a", "ShowAccCheckAlert('AddEditUser.aspx?Mode=" + obj.Encrypt("Edit") + "');", True)
                                            Else
                                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "AdddEditAcc", "CloseWindow('" + hidAcc.Value + "');", True)
                                            End If
                                        Else
                                            objUpdateData.UpdateAccCheckDate(Session("UserId"))
                                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "AccCheck_a", "ShowAccCheckAlert('AddEditUser.aspx?Mode=" + obj.Encrypt("Edit") + "');", True)
                                        End If
                                    Else
                                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "AdddEditAcc", "CloseWindow('" + hidAcc.Value + "');", True)
                                    End If
                                    'End

                                    'Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "CloseWindow('" + hidAcc.Value + "');", True)
                                    'Response.Redirect("~/Index.aspx", True)
                                ElseIf DsValidUsr.Tables(0).Rows(0).Item("ISVALIDEMAIL").ToString() = "Y" And DsValidUsr.Tables(0).Rows(0).Item("ISAPPROVED").ToString() <> "Y" Then
                                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Your account is being reviewed for approval. You will be notified when your account is activiated.');", True)
                                Else
                                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('The verification of your email address is incomplete, and the verification must be completed to use your account. Click on Login, then verify your email address to complete this verification process.');", True)
                                End If
                            Else
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Password is incorrect.');", True)
                            End If
                        ElseIf DsUserCheck.Tables(0).Rows(0).Item("PASSWORD").ToString() <> "" Then
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('An account already exists for this email address.\nPlease enter your password to login.');", True)
                            trPassword.Visible = True
                            lnkForgotPass.Visible = True
                            txtPass.Focus()
                        ElseIf DsUserCheck.Tables(0).Rows(0).Item("PASSWORD").ToString() = "" Then
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "ShowAccountUpdation('AddEditUser.aspx?UN=" + obj.Encrypt(txtEmail.Text) + "&Mode=" + obj.Encrypt("Comp") + "');", True)
                            trPassword.Visible = False
                            lnkForgotPass.Visible = False
                        Else
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Please enter your Password.');", True)
                        End If
                    Else
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "ShowAccountAdd('AddEditUser.aspx?Mode=" + obj.Encrypt("AddAcc") + "&UName=" + obj.Encrypt(txtEmail.Text) + "');", True)
                    End If
				End If
                'End 27Nov_2017                
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Please enter your email address.');", True)
                trPassword.Visible = False
                lnkForgotPass.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = "Error:btnSubmit_Click:" + ex.Message.ToString()
        End Try
    End Sub

    'Protected Sub txtEmail_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEmail.TextChanged
    '    Dim objGetData As New UsersGetData.Selectdata
    '    Dim objUpdateData As New UsersUpdateData.UpdateInsert
    '    Dim obj As New CryptoHelper
    '    Dim DsUserCheck, DsValidUsr As New DataSet
    '    Try
    '        If txtEmail.Text.Trim.Length <> 0 Then
    '            DsUserCheck = objGetData.GetUsrInfo(txtEmail.Text)
    '            If DsUserCheck.Tables(0).Rows.Count > 0 Then
    '                If DsUserCheck.Tables(0).Rows(0).Item("PASSWORD").ToString() <> "" Then
    '                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Your account is already exists. Please Enter Password to Login.');", True)
    '                    trPassword.Visible = True
    '                    lnkForgotPass.Visible = True
    '                    txtPass.Focus()
    '                Else
    '                    trPassword.Visible = False
    '                    lnkForgotPass.Visible = False
    '                End If
    '            End If
    '        Else
    '            trPassword.Visible = False
    '            lnkForgotPass.Visible = False
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = "Error:txtEmail_TextChanged:" + ex.Message.ToString()
    '    End Try
    'End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "New_WinRename", "window.close();", True)
        Catch ex As Exception
            lblError.Text = "Error:btnCancel_Click:" + ex.Message.ToString()
        End Try
    End Sub

    '27Nov_2017
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
        Dim AccCheckDays As Integer
        Try
            dsVer = objVGetData.ValidateUser(txtEmail.Text.Trim(), txtPass.Text.Trim().Replace("'", "''"))
            UserName = obj.Encrypt(txtEmail.Text.Trim()).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!")

            If dsVer.Tables(0).Rows.Count > 0 Then
                Count = 0
                Days = DateDiff(DateInterval.Day, CDate(dsVer.Tables(0).Rows(0).Item("VUPDATEDATE")), Date.Now)

                If dsVer.Tables(0).Rows(0).Item("ISVALIDEMAIL").ToString() = "Y" And dsVer.Tables(0).Rows(0).Item("ISAPPROVED").ToString() = "Y" Then
                    ds = objGetData.ValidateUser(txtEmail.Text.Trim(), txtPass.Text.Trim().Replace("'", "''"))
                    DsSecLvl = objGetData.GetSecuirtyDetails(ds.Tables(0).Rows(0).Item("SECURITYLVL").ToString())
                    If ds.Tables(0).Rows.Count > 0 Then
                        If Days > 45 And DsSecLvl.Tables(0).Rows(0).Item("SECLEVEL").ToString() <> "5" Then
                            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Your password has expired, please change your password.');window.open('../../Users_Login/ChangeExPasswordU.aspx?un=" + UserName + "&SecLvl=" + DsSecLvl.Tables(0).Rows(0).Item("SECLEVEL").ToString() + "');", True)
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Message", "if(confirm('Your password has expired, please change your password.')){window.open('../Users_Login/ChangeExPasswordU.aspx?un=" + UserName + "&SecLvl=" + DsSecLvl.Tables(0).Rows(0).Item("SECLEVEL").ToString() + "');}else{document.getElementById('txtEmail').focus();}", True)
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
                            'hidUserID.Value = Session("UserId").ToString()
                            dsPer = objGetData.ValidateUserPermissions(Session("UserId").ToString())
                            If dsPer.Tables(0).Rows.Count > 0 Then
                                If Session("SecurityLevel") <> "5" Then            'Code added to implement HTTPS
                                    Session("https") = "https"
                                    'Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "CloseWindow('" + hidAcc.Value + "');", True)
                                Else
                                    'Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "CloseWindow('" + hidAcc.Value + "');", True)
                                End If
                            Else
                                Session("Back") = Nothing
                                Session("SBack") = "Secure"
                                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "CloseWindow('" + hidAcc.Value + "');", True)
                            End If

                            'Show Alert of account complition
                            If dsVer.Tables(0).Rows(0).Item("STATUSID").ToString() = 2 Then
                                If dsVer.Tables(0).Rows(0).Item("ACCCHECKDATE").ToString() <> "" Then
                                    AccCheckDays = DateDiff(DateInterval.Day, CDate(dsVer.Tables(0).Rows(0).Item("ACCCHECKDATE")), Date.Now)
                                    If AccCheckDays > 365 Then
                                        objVUpdateData.UpdateAccCheckDate(Session("UserId"))
                                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "AccCheck_a", "ShowAccCheckAlert('AddEditUser.aspx?Mode=" + obj.Encrypt("Edit") + "');", True)
                                    Else
                                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "CloseEmailChk", "CloseWindow('" + hidAcc.Value + "');", True)
                                    End If
                                Else
                                    objVUpdateData.UpdateAccCheckDate(Session("UserId"))
                                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "AccCheck_a", "ShowAccCheckAlert('AddEditUser.aspx?Mode=" + obj.Encrypt("Edit") + "');", True)
                                End If
                            Else
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "CloseEmailChk", "CloseWindow('" + hidAcc.Value + "');", True)
                            End If
                            'End

                        End If
                    Else
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('The EMAIL or PASSWORD that you entered could not be authenticated.');", True)
                    End If
                ElseIf dsVer.Tables(0).Rows(0).Item("ISVALIDEMAIL").ToString() = "Y" And dsVer.Tables(0).Rows(0).Item("ISAPPROVED").ToString() <> "Y" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Your account is being reviewed for approval. You will be notified when your account is activiated.');", True)
                Else
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('The verification of your email address is incomplete, and the verification must be completed to use your account.  Click on Verify your email to re-start the verification process.');", True)
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
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('The EMAIL or PASSWORD that you entered could not be authenticated.');", True)
                    Else
                        If Count = 5 Then
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Your account has been locked due to multiple failed login attempts. You may login after 15 minutes.');", True)
                            txtEmail.Focus()
                            Count = 1
                            Dim objUpdateData As New LoginUpdateData.Selectdata()
                            objUpdateData.UAccountLocked(txtEmail.Text, "Y", Date.Now)
                        Else
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('The EMAIL or PASSWORD that you entered could not be authenticated.');", True)
                        End If
                    End If
                Else
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('The EMAIL or PASSWORD that you entered could not be authenticated.');", True)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    'End

    'Forgot Password
    Protected Sub lnkForgotPass_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkForgotPass.Click
        Dim DsVerfData As New DataSet()
        Dim DsEmailConfig As New DataSet()
        Dim DsUserData As New DataSet()
        Dim link As String
        Dim strBodyData As String
        Dim objGetData As New UsersGetData.Selectdata()
        Dim objUpdateData As New UsersUpdateData.UpdateInsert()
        Dim obj As New CryptoHelper
        Dim VerfCode As String = Guid.NewGuid.ToString().Trim().Substring(0, 6)
        'Issue Pk_2020_1
        Dim DsStatusIDCheck As New DataSet()
        Dim objGetLoginData As New LoginGetData.Selectdata()
        'End Pk_2020_1
        Try
            DsUserData = objGetData.GetUserIDByEmailID(txtEmail.Text.Trim())
            If DsUserData.Tables(0).Rows.Count > 0 Then
                DsStatusIDCheck = objGetLoginData.ValidateUserByUserName(txtEmail.Text.Trim())
                'Issue Pk_2020_1
                If DsStatusIDCheck.Tables(0).Rows(0).Item("STATUSID").ToString() = 2 Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "ShowAccountUpdation('../Users_Login/AddEditUser.aspx?UN=" + obj.Encrypt(txtEmail.Text) + "&Mode=" + obj.Encrypt("Comp") + "');", True)
                Else
                    DsVerfData = objGetData.GetPwdVerfCode(DsUserData.Tables(0).Rows(0).Item("USERID").ToString())
                    DsEmailConfig = objGetData.GetEmailConfigDetails("Y")
                    If DsVerfData.Tables(0).Rows.Count > 0 Then
                        If DateDiff(DateInterval.Minute, CDate(DsVerfData.Tables(0).Rows(0).Item("PWDVERIFYDATE")), Date.Now) > 10 Then
                            If DsEmailConfig.Tables(0).Rows.Count > 0 Then
                                'Update Verification code for retrieve password
                                objUpdateData.UpdatePwdVerfCode(DsUserData.Tables(0).Rows(0).Item("USERID").ToString(), VerfCode)
                                'End Update
                                link = DsEmailConfig.Tables(0).Rows(0).Item("URL").ToString()
                                strBodyData = GetEmailBodyData_FP(link, VerfCode)
                                SendEmail_FP(strBodyData, DsUserData.Tables(0).Rows(0).Item("EMAILADDRESS").ToString(), DsUserData.Tables(0).Rows(0).Item("FIRSTNAME").ToString())
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "ForgotPwd", "ShowPopupFP('../Users_Login/ChangePasswordVerfCode.aspx?UID=" + obj.Encrypt(DsUserData.Tables(0).Rows(0).Item("USERID").ToString()) + "');", True)
                            Else
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "EmailConfigAlert", "alert('Our system is facing some issues. Please try later.');", True)
                            End If
                        Else
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "ForgotPwd", "ShowPopupFP('../Users_Login/ChangePasswordVerfCode.aspx?UID=" + obj.Encrypt(DsUserData.Tables(0).Rows(0).Item("USERID").ToString()) + "');", True)
                        End If
                    Else
                        If DsEmailConfig.Tables(0).Rows.Count > 0 Then
                            'Insert Verification code for retrieve password
                            objUpdateData.InsertPwdVerfCode(DsUserData.Tables(0).Rows(0).Item("USERID").ToString(), VerfCode)
                            'End
                            link = DsEmailConfig.Tables(0).Rows(0).Item("URL").ToString()
                            strBodyData = GetEmailBodyData_FP(link, VerfCode)
                            SendEmail_FP(strBodyData, DsUserData.Tables(0).Rows(0).Item("EMAILADDRESS").ToString(), DsUserData.Tables(0).Rows(0).Item("FIRSTNAME").ToString())
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "ForgotPwd", "ShowPopupFP('../Users_Login/ChangePasswordVerfCode.aspx?UID=" + obj.Encrypt(DsUserData.Tables(0).Rows(0).Item("USERID").ToString()) + "');", True)
                        Else
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "EmailConfigAlert", "alert('Our system is facing some issues. Please try later.');", True)
                        End If
                    End If
                End If
                'End Pk_2020_1   





                
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "CreateNwAccAlert", "alert('We cannot find an account with that email address. Please create an account.');", True)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Function GetEmailBodyData_FP(ByVal link As String, ByVal VerfCode As String) As String
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
            StrSql = StrSql & "<div style='color:white;font-size:18px;font-family:Verdana;font-weight:bold;margin-left:5px;'><b>Password Verification:</b> </div> "
            StrSql = StrSql & "</td> "
            StrSql = StrSql & "</tr> "
            StrSql = StrSql & "</table> "
            StrSql = StrSql & "<div style='margin-top:10px;margin-right:10px;font-family:Verdana;font-size:12px;'> "
            StrSql = StrSql & "Your verification Code is <b>" + VerfCode.Trim() + "</b> "
            StrSql = StrSql & "<p>Please use this code in the process of Forgot Password on SavvyPack Corporation's website.<br/>Once your code is verified, you will be able to reset your password.</p> "
            StrSql = StrSql & "<p>Thank you for using SavvyPack Corporation.</p> "
            StrSql = StrSql & "<p><b>Note that this verification code is valid for 10 minutes.</b></p> "
            StrSql = StrSql & "<p> "
            StrSql = StrSql & "<div style='font-family:Verdana;font-size:12px;'> "
            StrSql = StrSql & "SavvyPack Corporation<br/>1850 East 121st Street<br/>Suite 106B<br/>Burnsville, MN USA 55337<br/><a style='font-family:verdana;' href='http://www.savvypack.com'>www.savvypack.com</a>"
            StrSql = StrSql & "<br/>Phone: [1] 952-405-7500</div>"
            StrSql = StrSql & "</p> "
            StrSql = StrSql & "</div> "
            Return StrSql
        Catch ex As Exception
            Return StrSql
        End Try
    End Function

    Public Sub SendEmail_FP(ByVal strBody As String, ByVal UserName As String, ByVal FirstName As String)
        Try
            Dim objGetData As New UsersGetData.Selectdata
            Dim ds As New DataSet
            ds = objGetData.GetAlliedMemberMail("PWDVERF")
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
    'End


End Class
