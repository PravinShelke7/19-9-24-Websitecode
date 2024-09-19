Imports System.Data
Imports System.Data.OleDb
Imports System.Net.Mail
Imports System.Net
Partial Class Users_Login_ChangePasswordVerfCode
    Inherits System.Web.UI.Page
    Dim UserId As String = String.Empty
    Dim objGetData As New UsersGetData.Selectdata()
    Dim objUpdateData As New UsersUpdateData.UpdateInsert()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim obj As New CryptoHelper
        Dim DsUserInfo As New DataSet()
        Try
            UserId = obj.Decrypt(Request.QueryString("UID").ToString())
            DsUserInfo = objGetData.GetUserInfoByUID(UserId.ToString())
            hdnCurrentPwd.Value = DsUserInfo.Tables(0).Rows(0).Item("PASSWORD").ToString()
            lblVCInfo.Text = "For your security, we need to authenticate your request. We've sent an Vefication Code to the email <B>" + DsUserInfo.Tables(0).Rows(0).Item("EMAILADDRESS").ToString() + "</B>. Please enter it below to complete verification."
            lblPwdInfo.Text = "Reset your password below."
        Catch ex As Exception
            lblError.Text = "Error:Page_Load " + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Dim DsVerfData As New DataSet()
        Dim DsVrfy As New DataSet()
        Try
            If UserId <> "" Then
                DsVerfData = objGetData.GetPwdVerfCode(UserId.ToString())
                If DsVerfData.Tables(0).Rows.Count > 0 Then
                    If DateDiff(DateInterval.Minute, CDate(DsVerfData.Tables(0).Rows(0).Item("PWDVERIFYDATE")), Date.Now) > 10 Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Your verification Code has expired.');", True)
                    Else
                        DsVrfy = objGetData.CheckVerfCodeByUserID(UserId, txtVerfCode.Text.Trim.ToString())
                        If DsVrfy.Tables(0).Rows.Count > 0 Then
                            txtVerfCode.Enabled = False
                            btnSubmit.Enabled = False
                            dvChngePWd.Style.Add("Display", "inline")
                        Else
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Invalid Verification Code.');", True)
                        End If
                    End If
                Else
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "EmailConfigAlert", "alert('Our system is facing some issues. Please try later.');", True)
                End If
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "EmailConfigAlert", "alert('Our system is facing some issues. Please try later.');", True)
            End If
        Catch ex As Exception
            lblError.Text = "Error:btnSubmit_Click " + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Dim objVGetData As New LoginGetData.Selectdata()
        Dim objVInsertData As New LoginUpdateData.Selectdata()
        Dim DsUserInfo As New DataSet()
        Dim DsPwdLog As New DataSet()
        Dim dsU, DsSecLvl As New DataSet() '389
        Dim dsEmail As New DataSet()
        Dim strLink, strBody As String
        Try
            If UserId <> "" Then
                DsUserInfo = objGetData.GetUserInfoByUID(UserId.ToString())
                If DsUserInfo.Tables(0).Rows.Count > 0 Then
                    dsU = objVGetData.ValidateUSRWOLICENSE(DsUserInfo.Tables(0).Rows(0).Item("USERNAME").ToString()) '389
                    DsSecLvl = objVGetData.GetSecurityDetails(dsU.Tables(0).Rows(0).Item("SECURITYLVL").ToString()) '389
                    If DsSecLvl.Tables(0).Rows(0).Item("SECLEVEL").ToString() <> "5" Then
                        'Check Password log to avoid password repetition
                        DsPwdLog = objVGetData.GetPasswordLog(DsUserInfo.Tables(0).Rows(0).Item("USERID").ToString(), txtNewPwd.Text)
                        If DsPwdLog.Tables(0).Rows.Count <= 0 Then
                            dsEmail = objGetData.GetEmailConfigDetails("Y")
                            If dsEmail.Tables(0).Rows.Count > 0 Then
                                'UPDATE USER DETAILS
                                objUpdateData.ChangePWD(DsUserInfo.Tables(0).Rows(0).Item("USERID").ToString(), txtNewPwd.Text.Trim())
                                objVInsertData.UpdatePWDLog(DsUserInfo.Tables(0).Rows(0).Item("USERID").ToString(), txtNewPwd.Text.Trim(), DsPwdLog.Tables(0).Rows.Count)

                                'Sending Email
                                strLink = dsEmail.Tables(0).Rows(0).Item("URL").ToString()
                                strBody = GetEmailBodyData(strLink, DsUserInfo.Tables(0).Rows(0).Item("USERNAME").ToString(), DsUserInfo.Tables(0).Rows(0).Item("FIRSTNAME").ToString())
                                SendEmail(strBody, DsUserInfo.Tables(0).Rows(0).Item("EMAILADDRESS").ToString(), DsUserInfo.Tables(0).Rows(0).Item("FIRSTNAME").ToString())
                                '
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "PWDChangeSuccess", "alert('Password reset successfully. You can sign in with new password.');window.close();", True)
                            Else
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "EmailConfigAlert", "alert('Our system is facing some issues. Please try later.');", True)
                            End If
                        Else
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "PwdUsedAlert", "alert('" + txtNewPwd.Text + " is already used. Please use another password.');", True)
                        End If
                    Else
                        '389
                        dsEmail = objGetData.GetEmailConfigDetails("Y")
                        If dsEmail.Tables(0).Rows.Count > 0 Then
                            'UPDATE USER DETAILS
                            objUpdateData.ChangePWD(DsUserInfo.Tables(0).Rows(0).Item("USERID").ToString(), txtNewPwd.Text.Trim())
                            objUpdateData.UpdatePWDLogWithOSec(DsUserInfo.Tables(0).Rows(0).Item("USERID").ToString(), txtNewPwd.Text.Trim())

                            'Sending Email
                            strLink = dsEmail.Tables(0).Rows(0).Item("URL").ToString()
                            strBody = GetEmailBodyData(strLink, DsUserInfo.Tables(0).Rows(0).Item("USERNAME").ToString(), DsUserInfo.Tables(0).Rows(0).Item("FIRSTNAME").ToString())
                            SendEmail(strBody, DsUserInfo.Tables(0).Rows(0).Item("EMAILADDRESS").ToString(), DsUserInfo.Tables(0).Rows(0).Item("FIRSTNAME").ToString())
                            '
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "PWDChangeSuccess", "alert('Password reset successfully. You can sign in with new password.');window.close();", True)
                        End If
                        '389
                    End If
                Else
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "EmailConfigAlert", "alert('User information not found.');", True)
                End If
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "EmailConfigAlert", "alert('Our system is facing some issues. Please try later.');", True)
            End If
        Catch ex As Exception
            lblError.Text = "Error:btnUpdate_Click " + ex.Message.ToString()
        End Try
    End Sub


    Public Function GetEmailBodyData(ByVal link As String, ByVal UserName As String, ByVal Fname As String) As String
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
            StrSql = StrSql + "<div style='color:white;font-size:18px;font-family:Verdana;font-weight:bold;margin-left:5px;'><b>Reset Password </b> </div> "
            StrSql = StrSql + "</td> "
            StrSql = StrSql + "</tr> "
            StrSql = StrSql + "</table> "
            StrSql = StrSql + "<div style='margin-top:10px;margin-right:10px;font-family:Verdana;font-size:12px;'> "
            StrSql = StrSql + "<table style='font-family:Verdana;width:600px;font-size:12px;border-collapse:collapse' border='0' bordercolor='black' cellpadding='0' cellspacing='0'><tr style='font-weight:bold;text-align:center'> "
            StrSql = StrSql + "<tr><td> Hi " + Fname + ",</td></tr><tr><td>&nbsp;</td></tr> "
            StrSql = StrSql + "<tr><td>The password for your <b>" + UserName + "</b> account has been reset successfully.</td></tr> "
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
            StrSql = StrSql + "</div> "
            StrSql = StrSql + "</p> "
            StrSql = StrSql + "<p> "
            StrSql = StrSql + "<div style='font-family:Verdana;font-size:12px;'> "
            StrSql = StrSql + "SavvyPack Corporation<br/>1850 East 121st Street<br/>Suite 106B<br/>Burnsville, MN USA 55337<br/><a style='font-family:verdana;' href='http://www.savvypack.com'>www.savvypack.com</a> "
            StrSql = StrSql + "<br/>Phone: [1] 952-405-7500</div> "
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
            ds = objGetData.GetAlliedMemberMail("RSTPWDCNF")
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
End Class
