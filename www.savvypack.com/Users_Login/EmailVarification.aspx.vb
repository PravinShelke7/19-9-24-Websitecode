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
Partial Class Users_Login_EmailVarification
    Inherits System.Web.UI.Page
    Dim _strType As String
    Dim _strEmail As String
    Dim _strUserID As String
    Public Property Type() As String
        Get
            Return _strType
        End Get
        Set(ByVal value As String)
            Dim obj As New CryptoHelper
            _strType = obj.Decrypt(value)
        End Set
    End Property
    Public Property Email() As String
        Get
            Return _strEmail
        End Get
        Set(ByVal value As String)
            Dim obj As New CryptoHelper
            _strEmail = obj.Decrypt(value)
        End Set
    End Property
    Public Property UserID() As String
        Get
            Return _strUserID
        End Get
        Set(ByVal value As String)
            Dim obj As New CryptoHelper
            _strUserID = obj.Decrypt(value)
        End Set
    End Property
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim objgetData As New UsersGetData.Selectdata
            Dim obj As New CryptoHelper
            Dim ds, dsEmail As New DataSet
            Dim objUpdateData As New UsersUpdateData.UpdateInsert
            Dim strLink As String = ""
            Dim strBody1, strBody2 As String
            If Not IsPostBack Then
                Email = Request.QueryString("Email").ToString().Replace("!Plus!", "+").Replace("!Hash!", "#").Replace("!And!", "&")
                UserID = Request.QueryString("UserID").ToString().Replace("!Plus!", "+").Replace("!Hash!", "#").Replace("!And!", "&")
                Type = Request.QueryString("Type").ToString().Replace("!Plus!", "+").Replace("!Hash!", "#").Replace("!And!", "&")
                ds = objgetData.GetUserDetails(UserID)
                If ds.Tables(0).Rows.Count > 0 Then
                    objUpdateData.UpdateEmailValidationFlag(UserID, Email)
                    dsEmail = objgetData.GetEmailConfigDetails("Y")
                    If dsEmail.Tables(0).Rows.Count > 0 Then
                        strLink = dsEmail.Tables(0).Rows(0).Item("URL").ToString()
                        If ds.Tables(0).Rows(0).Item("IsValidEmail").ToString() <> "Y" Then
                            strBody1 = GetEmailBodyData(strLink, ds.Tables(0).Rows(0).Item("Name").ToString(), ds.Tables(0).Rows(0).Item("USERCONTACTID").ToString(), ds.Tables(0).Rows(0).Item("EMAILADDRESS").ToString(), "Confirm")
                            strBody2 = GetEmailBodyData(strLink, ds.Tables(0).Rows(0).Item("Name").ToString(), ds.Tables(0).Rows(0).Item("USERCONTACTID").ToString(), ds.Tables(0).Rows(0).Item("EMAILADDRESS").ToString(), "Varify")
                            'Code for sending Confirmation Mail
                            'SendEmail(strBody1, "Confirmation", Email, Email)
                            SendEmail(strBody1, "CNFRM", Email, Email)
                            'Code for sending Verfied Mail

                            SendEmail(strBody2, "VERFEMAIL", "", "")



                            If Type <> "NA" Then
                                Session("UserId") = UserID
                                Response.Redirect(Type)
                            End If
                            Dim Flag As String = obj.Encrypt("Varify").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!")
                            Response.Redirect("ThankYou.aspx?Flag=" + Flag)

                        Else
                            Response.Redirect("Error.aspx?ErrorCode=" + obj.Encrypt("ALDE117").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        End If
                    End If
                End If

            End If
        Catch ex As Exception
            lblError.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub
    Public Sub SendEmail(ByVal strBody As String, ByVal code As String, ByVal EmailAddress As String, ByVal FirstName As String)
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
                ElseIf code = "CNFRM" Then
                    Item = New MailAddress(EmailAddress, FirstName)
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
            lblError.Text = "Error:SendEmail:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Function GetEmailBodyData(ByVal link As String, ByVal Name As String, ByVal USERCONTACTID As String, ByVal EMAILADDRESS As String, ByVal flag As String) As String
        Dim StrSqlBdy As String = ""
        Try
            If flag = "Confirm" Then
                StrSqlBdy = "<div style='font-family:Verdana;'>  "
                StrSqlBdy = StrSqlBdy + "<table cellpadding='0' cellspacing='0' style='width:650px'> "
                StrSqlBdy = StrSqlBdy + "<tr> "
                StrSqlBdy = StrSqlBdy + "<td><img src='" + link + "/Images/SavvyPackLogo3.gif' /> "
                StrSqlBdy = StrSqlBdy + "<br /> "
                StrSqlBdy = StrSqlBdy + "</td> "
                StrSqlBdy = StrSqlBdy + "</tr> "
                StrSqlBdy = StrSqlBdy + "<tr style='background-color:#336699;height:35px;'> "
                StrSqlBdy = StrSqlBdy + "<td> "
                StrSqlBdy = StrSqlBdy + "<div style='color:white;font-size:18px;font-family:Verdana;font-weight:bold;margin-left:5px;'><b>Account Confirmation:</b> </div> "
                StrSqlBdy = StrSqlBdy + "</td> "
                StrSqlBdy = StrSqlBdy + "</tr> "
                StrSqlBdy = StrSqlBdy + "</table> "
                StrSqlBdy = StrSqlBdy + "<div style='margin-top:10px;margin-right:10px;font-family:Verdana;font-size:12px;'> "
                StrSqlBdy = StrSqlBdy + "<p>Hi " + Name + ",<br/><br/>    Thank you for creating an account on SavvyPack Corporation's website.  This email confirms your activation.<br/><br/>   Having an account with us makes it easier to:"

                StrSqlBdy = StrSqlBdy + "<li style='margin-top:5px;'>register for webconferences</li>  "
                StrSqlBdy = StrSqlBdy + "<li style='margin-top:5px;'>purchase products or services</li> "
                StrSqlBdy = StrSqlBdy + "<li style='margin-top:5px;'>complete other online activities</li> "

                StrSqlBdy = StrSqlBdy + "<br><br/> Also, your name has been added to our promotional announcement list.  If at any time you wish to be removed from this list, sign on to your account and change your preference.</p> "
                StrSqlBdy = StrSqlBdy + "<p> "
                StrSqlBdy = StrSqlBdy + "<div style='font-family:Verdana;font-size:12px;'> "
                StrSqlBdy = StrSqlBdy + "Thank you,<br/> "
                StrSqlBdy = StrSqlBdy + "Customer Support<br/><br/>SavvyPack Corporation<br/>2800 East Cliff Road, Suite 140<br/>Burnsville, MN 55337 USA<br/><a style='font-family:verdana;' href='http://www.savvypack.com'>www.savvypack.com</a><br/>Phone: 1-952-405-7500</div> "
                StrSqlBdy = StrSqlBdy + "</p> "
                StrSqlBdy = StrSqlBdy + "</div> "
            ElseIf flag = "Varify" Then
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
            End If
            Return StrSqlBdy
        Catch ex As Exception
            lblError.Text = "Error:Page_Load:" + ex.Message.ToString()
            Return StrSqlBdy
        End Try
    End Function
End Class
