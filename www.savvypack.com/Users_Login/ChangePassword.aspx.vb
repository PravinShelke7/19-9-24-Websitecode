Imports System.Data
Imports System.Data.OleDb
Imports System
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Imports System.Net.Mail
Imports System.Net
Imports DBUtil
Partial Class Users_Login_ChangePassword
    Inherits System.Web.UI.Page
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
        objRefresh = New zCon.Net.Refresh("_USERS_LOGIN_ADDEDITUSER")
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        objRefresh.Render(Page)
    End Sub

#End Region
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            GetMasterPageControls()
            Session("MenuItem") = "ACNT1"
            Dim strText As String = ""
            If Not IsPostBack Then
                Dim obj As New CryptoHelper
                lnkMyAccount.NavigateUrl = "AddEditUser.aspx?Mode=" + obj.Encrypt("Edit").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + ""
            Else
                If Session("Back") = Nothing Then
                    If Session("SBack") = Nothing Then
                        Dim obj As New CryptoHelper
                        Response.Redirect("~/Universal_loginN/Pages/ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "", False)
                    End If
                End If
            End If
            txtCurrentPwd.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnUpdate.ClientID + "')")
            txtNewPwd.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnUpdate.ClientID + "')")
            txtConfirmPwd.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnUpdate.ClientID + "')")
            txtCurrentPwd.Focus()

            'Setting Security Level Start
            Dim ds As New DataSet()
            Dim objGetdata As New UsersGetData.Selectdata()
            Dim objVGetData As New LoginGetData.Selectdata()
            Dim dsU, DsSecLvl As New DataSet() '389

            ds = GetUserDetails(Session("userId"))

            If ds.Tables(0).Rows.Count > 0 Then
                dsU = objVGetData.ValidateUSRWOLICENSE(ds.Tables(0).Rows(0).Item("USERNAME").ToString()) '389
                DsSecLvl = objVGetData.GetSecurityDetails(dsU.Tables(0).Rows(0).Item("SECURITYLVL").ToString())
                If DsSecLvl.Tables(0).Rows.Count > 0 Then
                    hidSecurityLvl.Value = DsSecLvl.Tables(0).Rows(0).Item("SECLEVEL").ToString()
                End If
            End If
            'Setting Security Level End
        Catch ex As Exception
        End Try
    End Sub
    Public Function GetUserDetails(ByVal userId As String) As DataSet
        Dim Dts As New DataSet()
        Dim odbUtil As New DBUtil()
        Dim StrSql As String = String.Empty
        Dim EconConnection As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
        Try
            StrSql = "SELECT  "
            StrSql = StrSql + "USERS.USERNAME, "
            StrSql = StrSql + "USERS.ISPROMOMAIL, "
            StrSql = StrSql + "USERCONTACTID, "
            StrSql = StrSql + "USERCONTACTS.USERID, "
            StrSql = StrSql + "PREFIX, "
            StrSql = StrSql + "FIRSTNAME, "
            StrSql = StrSql + "LASTNAME,(PREFIX||' '||FIRSTNAME||' '||LASTNAME)NAME,nvl(USERS.IsValidEmail,'N')IsValidEmail, "
            StrSql = StrSql + "JOBTITLE, "
            StrSql = StrSql + "EMAILADDRESS, "
            StrSql = StrSql + "PHONENUMBER, "
            StrSql = StrSql + "FAXNUMBER, "
            'StrSql = StrSql + "COMPANYNAME, "
            StrSql = StrSql + "COMPANY.COMPANYNAME COMPANYNAME, "
            StrSql = StrSql + "STREETADDRESS1, "
            StrSql = StrSql + "STREETADDRESS2, "
            StrSql = StrSql + "CITY, "
            StrSql = StrSql + "STATE, "
            StrSql = StrSql + "ZIPCODE, "
            StrSql = StrSql + "COUNTRY COUNTRYID,"
            StrSql = StrSql + "(CASE WHEN COUNTRY=0 THEN  '' ELSE DIMCOUNTRIES.COUNTRYDES END) COUNTRY "
            StrSql = StrSql + "FROM USERCONTACTS "
            StrSql = StrSql + "INNER JOIN USERS "
            StrSql = StrSql + "ON USERS.USERID=USERCONTACTS.USERID "
            StrSql = StrSql + "INNER JOIN ADMINSITE.DIMCOUNTRIES "
            StrSql = StrSql + "ON DIMCOUNTRIES.COUNTRYID=USERCONTACTS.COUNTRY "
            StrSql = StrSql + "LEFT OUTER JOIN SHOPPING.COMPANY "
            StrSql = StrSql + "ON COMPANY.COMPANYID=USERS.COMPANYID "

            StrSql = StrSql + "WHERE USERCONTACTS.USERID  = '" + userId + "'"
            Dts = odbUtil.FillDataSet(StrSql, EconConnection)

            Return Dts
        Catch ex As Exception
            Throw New Exception("UsersGetData:ValidateUser:" + ex.Message.ToString())
            Return Dts
        End Try
    End Function
    Protected Sub btnUpdate_Click1(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objGetData As New UsersGetData.Selectdata
        Dim objInsertData As New UsersUpdateData.UpdateInsert
        Dim ds As New DataSet
        Dim strLink, strBody As String
        Dim dsEmail As New DataSet
        Try
            ds = objGetData.ValidateUserPWD(Session("UserId").ToString(), txtCurrentPwd.Text.Trim())
            If ds.Tables(0).Rows.Count > 0 Then
                dsEmail = objGetData.GetEmailConfigDetails("Y")
                If dsEmail.Tables(0).Rows.Count > 0 Then
                    'UPDATE USER DETAILS
                    objInsertData.ChangePWD(Session("UserId").ToString(), txtNewPwd.Text.Trim())
                    UpdatePWDLogWithOSec(Session("UserId").ToString(), txtNewPwd.Text.Trim()) '389
                    strLink = dsEmail.Tables(0).Rows(0).Item("URL").ToString()
                    strBody = GetEmailBodyData(strLink, ds.Tables(0).Rows(0).Item("UserName").ToString(), ds.Tables(0).Rows(0).Item("Password").ToString(), ds.Tables(0).Rows(0).Item("FNAME").ToString())
                    'Code for Sending Mail
                    SendEmail(strBody, ds.Tables(0).Rows(0).Item("EMAILADD").ToString(), ds.Tables(0).Rows(0).Item("FNAME").ToString())

                    lblMessage.Text = "Password Changed Successfully. You will also receive a mail."
                    rowCngPwd.Visible = False
                    lnkMyAccount.Visible = True
                End If
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Current Password is incorrect');", True)
            End If
        Catch ex As Exception
            lblError.Text = "Error:btnUpdate_Click:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Dim objGetData As New UsersGetData.Selectdata
        Dim objVGetData As New LoginGetData.Selectdata()
        Dim objInsertData As New UsersUpdateData.UpdateInsert
        Dim objVInsertData As New LoginUpdateData.Selectdata()
        Dim ds As New DataSet
        Dim DsPwdLog As New DataSet
        Dim strLink, strBody As String
        Dim PwdLog(5) As String
        Dim dsEmail As New DataSet
        Dim dsU, DsSecLvl As New DataSet() '389
        Dim objCryptoHelper As New CryptoHelper()

        Try
            ds = objGetData.ValidateUserPWD(Session("UserId").ToString(), txtCurrentPwd.Text.Trim())
            If ds.Tables(0).Rows.Count > 0 Then
                dsU = objVGetData.ValidateUSRWOLICENSE(ds.Tables(0).Rows(0).Item("USERNAME").ToString()) '389
                DsSecLvl = objVGetData.GetSecurityDetails(dsU.Tables(0).Rows(0).Item("SECURITYLVL").ToString()) '389
                If DsSecLvl.Tables(0).Rows(0).Item("SECLEVEL").ToString() <> "5" Then
                    'Check Password log to avoid password repetition
                    DsPwdLog = objVGetData.GetPasswordLog(ds.Tables(0).Rows(0).Item("USERID").ToString(), txtNewPwd.Text)

                    If DsPwdLog.Tables(0).Rows.Count <= 0 Then
                        dsEmail = objGetData.GetEmailConfigDetails("Y")
                        If dsEmail.Tables(0).Rows.Count > 0 Then
                            'UPDATE USER DETAILS

                            objInsertData.ChangePWD(ds.Tables(0).Rows(0).Item("USERID").ToString(), txtNewPwd.Text.Trim())
                            objVInsertData.UpdatePWDLog(ds.Tables(0).Rows(0).Item("USERID").ToString(), txtNewPwd.Text.Trim(), DsPwdLog.Tables(0).Rows.Count)
                            strLink = dsEmail.Tables(0).Rows(0).Item("URL").ToString()
                            strBody = GetEmailBodyData(strLink, ds.Tables(0).Rows(0).Item("UserName").ToString(), ds.Tables(0).Rows(0).Item("Password").ToString(), ds.Tables(0).Rows(0).Item("FNAME").ToString())
                            'Code for Sending Mail
                            SendEmail(strBody, ds.Tables(0).Rows(0).Item("EMAILADD").ToString(), ds.Tables(0).Rows(0).Item("FNAME").ToString())

                            lblMessage.Text = "Password changed successfully. You will also receive an email."
                            Session("Password") = txtNewPwd.Text.Trim()
                            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Password changed successfully. You will also receive an email.');window.close();", True)
                            rowCngPwd.Visible = False
                            lnkMyAccount.Visible = True
                        End If
                    Else
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('" + txtNewPwd.Text + " is already used,use another password');", True)
                    End If
                Else
                    '389
                    dsEmail = objGetData.GetEmailConfigDetails("Y")
                    If dsEmail.Tables(0).Rows.Count > 0 Then
                        'UPDATE USER DETAILS
                        objInsertData.ChangePWD(ds.Tables(0).Rows(0).Item("USERID").ToString(), txtNewPwd.Text.Trim())
                        UpdatePWDLogWithOSec(ds.Tables(0).Rows(0).Item("USERID").ToString(), txtNewPwd.Text.Trim())
                        strLink = dsEmail.Tables(0).Rows(0).Item("URL").ToString()
                        strBody = GetEmailBodyData(strLink, ds.Tables(0).Rows(0).Item("UserName").ToString(), ds.Tables(0).Rows(0).Item("Password").ToString(), ds.Tables(0).Rows(0).Item("FNAME").ToString())
                        'Code for Sending Mail
                        SendEmail(strBody, ds.Tables(0).Rows(0).Item("EMAILADD").ToString(), ds.Tables(0).Rows(0).Item("FNAME").ToString())

                        lblMessage.Text = "Password changed successfully. You will also receive an email."
                        Session("Password") = txtNewPwd.Text.Trim()
                        'Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Password changed successfully. You will also receive an email.');window.close();", True)
                        rowCngPwd.Visible = False
                        lnkMyAccount.Visible = True
                    End If
                    '389
                End If
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Current Password is incorrect');", True)
            End If
        Catch ex As Exception

            lblError.Text = "Error:btnUpdate_Click:" + ex.Message.ToString()
        End Try
    End Sub
    Public Sub UpdatePWDLogWithOSec(ByVal UserID As String, ByVal pwd As String)
        Dim odbUtil As New DBUtil()
        Dim StrSql As String
        Dim Dts As New DataSet()
        Dim EconConnection = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
        Try
            StrSql = "UPDATE PASSWORDLOG SET PASSWORD='" + pwd + "'WHERE USERID=" + UserID.ToString() + ""
            odbUtil.UpIns(StrSql, EconConnection)
        Catch ex As Exception
            Throw New Exception("UpdatePWDLogWithOSec:" + ex.Message.ToString())
        End Try

    End Sub
    Public Function GetEmailBodyData(ByVal link As String, ByVal UserName As String, ByVal Password As String, ByVal Fname As String) As String
        Dim StrSql As String = ""
        Try
            StrSql = "<div style='font-family:Verdana;'>  "
            StrSql = StrSql + "<table cellpadding='0' cellspacing='0' style='width:650px'> "
            StrSql = StrSql + "<tr> "
            StrSql = StrSql + "<td><img src='" + link + "/Images/SavvyPackLogo3.gif' /> "
            StrSql = StrSql + "<br /> "
            StrSql = StrSql + "</td> "
            StrSql = StrSql + "</tr> "
            StrSql = StrSql + "<tr style='background-color:#336699;height:35px;'> "
            StrSql = StrSql + "<td> "
            StrSql = StrSql + "<div style='color:white;font-size:18px;font-family:Verdana;font-weight:bold;margin-left:5px;'><b>Change Password </b> </div> "
            StrSql = StrSql + "</td> "
            StrSql = StrSql + "</tr> "
            StrSql = StrSql + "</table> "
            StrSql = StrSql + "<div style='margin-top:10px;margin-right:10px;font-family:Verdana;font-size:12px;'> "
            StrSql = StrSql + "<table style='font-family:Verdana;width:600px;font-size:12px;border-collapse:collapse' border='0' bordercolor='black' cellpadding='0' cellspacing='0'><tr style='font-weight:bold;text-align:center'>"
            StrSql = StrSql + "<tr><td> Hi " + Fname + ",</td></tr><tr><td>&nbsp;</td></tr> "
            StrSql = StrSql + "<tr><td>The password for your <b>" + UserName + "</b> account was changed Successfully .</td></tr> "
            StrSql = StrSql + "</table> "
            StrSql = StrSql + "<br /> "
            StrSql = StrSql + "<p>"
            StrSql = StrSql + "<div style='font-family:Verdana;font-size:12px;'> "
            StrSql = StrSql + "<table cellpadding='0' cellspacing='0' style='width:650px;font-family:Verdana;font-size:12px;'> "
            StrSql = StrSql + "<tr> "
            StrSql = StrSql + "<td>Thank You,"
            StrSql = StrSql + "</td> "
            StrSql = StrSql + "</tr> "
            StrSql = StrSql + "<tr> "
            StrSql = StrSql + "<td>Customer Support"
            StrSql = StrSql + "</td> "
            StrSql = StrSql + "</tr> "
            StrSql = StrSql + "</table> "
            StrSql = StrSql + "</div>"
            StrSql = StrSql + "</p>"
            StrSql = StrSql + "<p> "
            StrSql = StrSql + "<div style='font-family:Verdana;font-size:12px;'> "
            StrSql = StrSql + "SavvyPack Corporation<br/>1850 East 121st Street Suite 106B<br/>Burnsville, MN USA 55337<br/><a style='font-family:verdana;' href='http://www.savvypack.com'>www.savvypack.com</a><br/>Phone: 1-952-405-7500</div> "
            StrSql = StrSql + "</p> "
            StrSql = StrSql + "</div> "
            Return StrSql
        Catch ex As Exception
            lblError.Text = "Error:GetEmailBodyData:" + ex.Message.ToString()
            Return StrSql
        End Try
    End Function
    Public Sub SendEmail(ByVal strBody As String, ByVal UserName As String, ByVal FirstName As String)
        Try
            Dim objGetData As New UsersGetData.Selectdata
            Dim ds As New DataSet
            ds = objGetData.GetAlliedMemberMail("CNGPWD")
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
            lblError.Text = "Error:SendEmail:" + ex.Message.ToString()
        End Try
    End Sub


    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCanel.Click
        Try
            Dim obj As New CryptoHelper
            Response.Redirect("AddEditUser.aspx?Mode=" + obj.Encrypt("Edit").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
        Catch ex As Exception
            lblError.Text = "Error:btnCancel_Click:" + ex.Message.ToString()
        End Try



    End Sub
End Class
