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
Partial Class Users_Login_Verification
    Inherits System.Web.UI.Page
    Dim UserId As String = String.Empty

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            lbltitle.Text = "Account Verification"
            Dim obj As New CryptoHelper
            UserId = obj.Decrypt(Request.QueryString("UID").ToString())
            hidType.Value = Request.QueryString("Type").ToString()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Dim objGetData As New UsersGetData.Selectdata()
        Dim objUpdateData As New UsersUpdateData.UpdateInsert

        Dim ds, dsEmail As New DataSet()
        Dim obj As New CryptoHelper
        Dim dsVerf As New DataSet()
        Dim strLink As String = ""
        Dim strBody As String = ""

        '15th_Nov_2017
        Dim UserNm As String = String.Empty
        Dim DsDomain As New DataSet()
        Dim DomainStatusID As String = String.Empty
        'End

        Try
            ds = objGetData.ValidateUserIDCode(UserId, "")
            If ds.Tables(0).Rows.Count > 0 Then
                If txtVerfCode.Text <> "" Then
                    If DateDiff(DateInterval.Minute, CDate(ds.Tables(0).Rows(0).Item("VERIFYDATE")), Date.Now) > 10 Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Your verification Code is expired. Click on Login, then verify your email address to complete this verification process.');", True)
                    Else
                        dsVerf = objGetData.ValidateUserIDCode(UserId, txtVerfCode.Text)
                        If dsVerf.Tables(0).Rows.Count > 0 Then
                            objUpdateData.UpdateEmailValidationFlag(dsVerf.Tables(0).Rows(0).Item("USERID").ToString(), dsVerf.Tables(0).Rows(0).Item("USERNAME").ToString())
                            dsEmail = objGetData.GetEmailConfigDetails("Y")
                            If dsEmail.Tables(0).Rows.Count > 0 Then
                                strLink = dsEmail.Tables(0).Rows(0).Item("URL").ToString()

                                '14th_Nov_2017
                                Dim words As String() = dsVerf.Tables(0).Rows(0).Item("USERNAME").ToString().Split("@")
                                If words.Length() = 2 Then
                                    UserNm = "@" + words(1)
                                    DsDomain = objGetData.GetDomainFlag(UserNm.ToString())

                                    If DsDomain.Tables(0).Rows(0).Item("ISNEW").ToString() = "Y" Or dsVerf.Tables(0).Rows(0).Item("ISNEW").ToString() = "Y" Then
                                        strBody = String.Empty
                                        strBody = GetEmailBodyDataNotify(strLink, dsVerf.Tables(0).Rows(0).Item("COMPANYID").ToString(), _
                                                                         dsVerf.Tables(0).Rows(0).Item("COMPANY").ToString(), _
                                                                         dsVerf.Tables(0).Rows(0).Item("ISNEW").ToString(), _
                                                                         DsDomain.Tables(0).Rows(0).Item("DOMAINID").ToString(), _
                                                                         DsDomain.Tables(0).Rows(0).Item("DOMAINNAME").ToString(), _
                                                                         DsDomain.Tables(0).Rows(0).Item("ISNEW").ToString(), _
                                                                         dsVerf.Tables(0).Rows(0).Item("Name").ToString(), _
                                                                         dsVerf.Tables(0).Rows(0).Item("USERCONTACTID").ToString(), _
                                                                         dsVerf.Tables(0).Rows(0).Item("EMAILADDRESS").ToString())
                                        SendEmailNotify(strBody, "NEWADDITIONS")
                                    Else
                                        'Sending mail
                                        strBody = GetEmailBodyData(strLink, dsVerf.Tables(0).Rows(0).Item("Name").ToString(), dsVerf.Tables(0).Rows(0).Item("USERCONTACTID").ToString(), dsVerf.Tables(0).Rows(0).Item("EMAILADDRESS").ToString())
                                        SendEmail(strBody, "VERFEMAIL")
                                    End If
                                    'end

                                    'Checking for approved user
                                    If ds.Tables(0).Rows(0).Item("ISAPPROVED").ToString() = "Y" Then
                                        If ds.Tables(0).Rows(0).Item("UTYPE").ToString() = "2" Then
                                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "UserLogin", "CloseWindowUType('" + hidType.Value + "');", True)
                                        Else
                                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "UserLogin", "closewindow('Y','" + hidType.Value + "');", True)
                                        End If
                                    Else
                                        If DsDomain.Tables(0).Rows.Count > 0 Then
                                            If DsDomain.Tables(0).Rows(0).Item("ACCEPTANCYFLAG").ToString() = "1" Then
                                                DomainStatusID = 1
                                            ElseIf DsDomain.Tables(0).Rows(0).Item("ACCEPTANCYFLAG").ToString() = "2" Then
                                                DomainStatusID = 2
                                            ElseIf DsDomain.Tables(0).Rows(0).Item("ACCEPTANCYFLAG").ToString() = "3" Then
                                                DomainStatusID = 3
                                            ElseIf DsDomain.Tables(0).Rows(0).Item("ACCEPTANCYFLAG").ToString() = "4" Then
                                                DomainStatusID = 4
                                            End If
                                        End If
                                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "UserLogin", "CloseWindowWithoutLogin('" + DomainStatusID + "','" + hidType.Value + "');", True)
                                    End If
                                End If
                            End If
                        Else
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Invalid Verification Code.');", True)
                        End If
                    End If
                Else
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Please enter your verification code.');", True)
                End If
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

#Region "14th_Nov_2017"

    Protected Function GetEmailBodyDataNotify(ByVal link As String, ByVal CompanyID As String, ByVal CompanyNm As String, ByVal NewCom As String, _
                                         ByVal DomainID As String, ByVal DomainNm As String, ByVal NewDom As String, _
                                         ByVal Name As String, ByVal USERCONTACTID As String, ByVal EMAILADDRESS As String) As String
        Dim StrSqlBdy As String = ""
        Dim strAction As String = ""
        Dim strActionCom As String = String.Empty
        Dim strActionDom As String = String.Empty
        Try

            strAction = "Please Add this user in Email List"
            strActionCom = "Please note that this New Company has created."
            strActionDom = "Please note that this New Domain has created."
            StrSqlBdy = "<div style='font-family:Verdana;'>  "
            StrSqlBdy = StrSqlBdy + "<table cellpadding='0' cellspacing='0' style='width:650px'> "
            StrSqlBdy = StrSqlBdy + "<tr> "
            StrSqlBdy = StrSqlBdy + "<td><img src='" + link + "/Images/SavvyPackLogo3.gif' /> "
            StrSqlBdy = StrSqlBdy + "<br /> "
            StrSqlBdy = StrSqlBdy + "</td> "
            StrSqlBdy = StrSqlBdy + "</tr> "
            StrSqlBdy = StrSqlBdy + "<tr style='background-color:#336699;height:30px;'> "
            StrSqlBdy = StrSqlBdy + "<td> "
            StrSqlBdy = StrSqlBdy + "<div style='color:white;font-size:16px;font-family:Verdana;font-weight:bold;margin-left:5px;'><b>" + strAction + "</b> </div> "
            StrSqlBdy = StrSqlBdy + "</td> "
            StrSqlBdy = StrSqlBdy + "</tr> "
            StrSqlBdy = StrSqlBdy + "</table> "
            StrSqlBdy = StrSqlBdy + "<div style='margin-top:10px;margin-right:10px;font-family:Verdana;font-size:12px;'> "
            StrSqlBdy = StrSqlBdy + "<table style='font-family:Verdana;width:650px;font-size:12px;border-collapse:collapse' border='1' bordercolor='black' cellpadding='0' cellspacing='0'><tr style='font-weight:bold;text-align:left'><td>User Contact Id</td><td>Name</td><td>Email Address</td></tr> "
            StrSqlBdy = StrSqlBdy + "<tr><td>" + USERCONTACTID + "</td><td>" + Name + "</td><td>" + EMAILADDRESS + "</td></tr> "
            StrSqlBdy = StrSqlBdy + "</table> "
            StrSqlBdy = StrSqlBdy + "<br /> "


            If NewCom.ToString() = "Y" Then
                StrSqlBdy = StrSqlBdy + "<table cellpadding='0' cellspacing='0' style='width:650px'> "
                StrSqlBdy = StrSqlBdy + "<tr style='background-color:#336699;height:30px;'> "
                StrSqlBdy = StrSqlBdy + "<td> "
                StrSqlBdy = StrSqlBdy + "<div style='color:white;font-size:16px;font-family:Verdana;font-weight:bold;margin-left:5px;'><b>" + strActionCom + "</b> </div> "
                StrSqlBdy = StrSqlBdy + "</td> "
                StrSqlBdy = StrSqlBdy + "</tr> "
                StrSqlBdy = StrSqlBdy + "</table> "
                StrSqlBdy = StrSqlBdy + "<div style='margin-top:10px;margin-right:10px;font-family:Verdana;font-size:12px;'> "
                StrSqlBdy = StrSqlBdy + "<table style='font-family:Verdana;width:650px;font-size:12px;border-collapse:collapse' border='1' bordercolor='black' cellpadding='0' cellspacing='0'><tr style='font-weight:bold;text-align:left'><td>Company Id</td><td>Company Name</td></tr> "
                StrSqlBdy = StrSqlBdy + "<tr><td>" + CompanyID + "</td><td>" + CompanyNm + "</td></tr> "
                StrSqlBdy = StrSqlBdy + "</table> "
                StrSqlBdy = StrSqlBdy + "<br /> "
            End If

            If NewDom.ToString() = "Y" Then
                StrSqlBdy = StrSqlBdy + "<table cellpadding='0' cellspacing='0' style='width:650px'> "
                StrSqlBdy = StrSqlBdy + "<tr style='background-color:#336699;height:30px;'> "
                StrSqlBdy = StrSqlBdy + "<td> "
                StrSqlBdy = StrSqlBdy + "<div style='color:white;font-size:16px;font-family:Verdana;font-weight:bold;margin-left:5px;'><b>" + strActionDom + "</b> </div> "
                StrSqlBdy = StrSqlBdy + "</td> "
                StrSqlBdy = StrSqlBdy + "</tr> "
                StrSqlBdy = StrSqlBdy + "</table> "
                StrSqlBdy = StrSqlBdy + "<div style='margin-top:10px;margin-right:10px;font-family:Verdana;font-size:12px;'> "
                StrSqlBdy = StrSqlBdy + "<table style='font-family:Verdana;width:650px;font-size:12px;border-collapse:collapse' border='1' bordercolor='black' cellpadding='0' cellspacing='0'><tr style='font-weight:bold;text-align:left'><td>Domain Id</td><td>Domain Name</td></tr> "
                StrSqlBdy = StrSqlBdy + "<tr><td>" + DomainID + "</td><td>" + DomainNm + "</td></tr> "
                StrSqlBdy = StrSqlBdy + "</table> "
                StrSqlBdy = StrSqlBdy + "<br /> "
            End If

            StrSqlBdy = StrSqlBdy + "<p> "
            StrSqlBdy = StrSqlBdy + "<div style='font-family:Verdana;font-size:12px;'> "
            StrSqlBdy = StrSqlBdy + "SavvyPack Corporation<br/>1850 East 121st Street Suite 106B<br/>Burnsville, MN USA 55337<br/><a style='font-family:verdana;' href='http://www.savvypack.com'>www.savvypack.com</a><br/>Phone: 1-952-405-7500</div> "
            StrSqlBdy = StrSqlBdy + "</p> "
            StrSqlBdy = StrSqlBdy + "</div> "

            Return StrSqlBdy
        Catch ex As Exception
            Return StrSqlBdy
        End Try
    End Function

    Public Sub SendEmailNotify(ByVal strBody As String, ByVal code As String)
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
                Item = New MailAddress(ds.Tables(0).Rows(0).Item("TOADD").ToString(), ds.Tables(0).Rows(0).Item("TONAME").ToString())
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

#End Region
	
	Protected Sub btncls_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btncls.Click
        Try
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "UserLogin", "window.close();", True)
        Catch ex As Exception

        End Try
    End Sub
	
End Class
