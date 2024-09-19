Imports System.Data
Imports System.Data.OleDb
Imports System
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Imports System.Net.Mail
Imports System.Net
Partial Class Users_Login_ChangeExPassword
    Inherits System.Web.UI.Page

    Protected Sub Page_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        ' Session("UserId") = Nothing
    End Sub

    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            hidSecurityLvl.Value = Request.QueryString("SecLvl").ToString()
            txtCurrentPwd.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnUpdate.ClientID + "')")
            txtNewPwd.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnUpdate.ClientID + "')")
            txtConfirmPwd.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnUpdate.ClientID + "')")
            txtCurrentPwd.Focus()
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinB", "SetParentFocus();", True)
            'GetMasterPageControls()
            Dim strText As String = ""
            
        Catch ex As Exception
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
        Dim username As String
        Dim dsU, DsSecLvl As New DataSet() '389
        username = Request.QueryString("un").ToString()
        Dim objCryptoHelper As New CryptoHelper()
        username = objCryptoHelper.Decrypt(username)
        Try
            ds = objVGetData.ValidateUserByUserName(username)
            ds = objGetData.ValidateUserPWD(ds.Tables(0).Rows(0).Item("USERID").ToString(), txtCurrentPwd.Text.Trim())
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
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Password changed successfully. You will also receive an email.');window.close();", True)
                            rowCngPwd.Visible = False
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
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Password changed successfully. You will also receive an email.');window.close();", True)
                        rowCngPwd.Visible = False
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
            Throw New Exception("UsersUpdateData:ChangePWD:" + ex.Message.ToString())
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
            Session("UserId") = Nothing
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "window.close();", True)
        Catch ex As Exception
            lblError.Text = "Error:btnCancel_Click:" + ex.Message.ToString()
        End Try
    End Sub
End Class
