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
Partial Class Users_Login_Login
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
        objRefresh = New zCon.Net.Refresh("_USERS_LOGIN_LOGIN")
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        objRefresh.Render(Page)
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            txtEmail.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnSubmit.ClientID + "')")
            txtPass.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnSubmit.ClientID + "')")
            'txtEmail.Focus()
            GetMasterPageControls()
            Dim obj As New CryptoHelper
            Dim modVal As String = obj.Encrypt("Add").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!")
            hdnMode.Value = modVal
            If Not IsPostBack Then
                If Request.QueryString("Msg") <> Nothing Then
                    If Request.QueryString("Msg").ToString() = "Y" Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Thank you for creating your account. Your account has been verified. You can now Login.');", True)
                    End If
                End If
                If Session("UserId") Is Nothing Then
                Else
                    Response.Redirect("AddEditUser.aspx?Mode=" + obj.Encrypt("Edit").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")

                End If
            Else
                If Not String.IsNullOrEmpty(txtPass.Text.Trim()) Then
                    txtPass.Attributes.Add("value", txtPass.Text)
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Dim objGetData As New UsersGetData.Selectdata()
        Dim objUpdateData As New UsersUpdateData.UpdateInsert

        Dim ds, dsEmail As New DataSet()
        Dim obj As New CryptoHelper
        Dim dsVerf As New DataSet()
        Dim strLink As String = ""
        Dim strBody As String
        'Chnages by Pr
        Dim Dsvalidmail As New DataSet()
        Dim dsUserData As New DataSet()
        Dim EncEmail, EncUserId, EncType As String
        Dim EmailLink, link As String
        Dim RType As String = ""
        Dim strBodyData As String
        Dim VerfCode As String = Guid.NewGuid.ToString().Trim().Substring(0, 8)

        Try
            ds = objGetData.ValidateUser(txtEmail.Text.Trim(), txtPass.Text.Trim().Replace("'", "''"))
            If ds.Tables(0).Rows.Count > 0 Then
                hdnUTYPE.Value = ds.Tables(0).Rows(0).Item("UTYPE").ToString()
                If ds.Tables(0).Rows(0).Item("ISVALIDEMAIL").ToString() = "Y" And ds.Tables(0).Rows(0).Item("ISAPPROVED").ToString() = "Y" Then
                    Session("UserId") = ds.Tables(0).Rows(0).Item("UserID").ToString()
                    Session("Back") = "Secure"
                    objUpdateData.LoginLogOutLogInsert(txtEmail.Text.Trim(), Session.SessionID)
                    If Session("URL") = "" Or Session("URL") Is Nothing Then
                        Response.Redirect("AddEditUser.aspx?Mode=" + obj.Encrypt("Edit").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")

                    Else
                        Response.Redirect(Session("URL").ToString())
                    End If
                ElseIf ds.Tables(0).Rows(0).Item("ISVALIDEMAIL").ToString() = "Y" And ds.Tables(0).Rows(0).Item("ISAPPROVED").ToString() <> "Y" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Your account is being reviewed for approval, and you will be notified when your account is activiated.');", True)
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
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "ShowPopWindowVerf('Verification.aspx?UID=" + obj.Encrypt(dsUserData.Tables(0).Rows(0).Item("UserId").ToString()) + "');", True)
                            End If
                        End If
                    Else
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "ShowPopWindowVerf('Verification.aspx?UID=" + obj.Encrypt(ds.Tables(0).Rows(0).Item("UserID").ToString()) + "');", True)
                    End If
                End If


            Else
                Response.Redirect("Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
            End If

        Catch ex As Exception
            lblError.Text = "Error:btnSubmit_Click:" + ex.Message.ToString()
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
            lblError.Text = "Error:Page_Load:" + ex.Message.ToString()
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
            lblError.Text = "Error:SendEmail:" + ex.Message.ToString()
        End Try
    End Sub
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
            lblError.Text = "Error:SendEmail:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Try
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "UserLogin", "CloseWindow();", True)
        Catch ex As Exception
            lblError.Text = "Error:btnClose_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnCloseUA_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCloseUA.Click
        Try
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "UserLogin", "CloseWindowUA();", True)
        Catch ex As Exception
            lblError.Text = "Error:btnCloseAcpU_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnCloseAcp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCloseAcp.Click
        Try
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "UserLogin", "CloseWindowAcp();", True)
        Catch ex As Exception
            lblError.Text = "Error:btnCloseAcp_Click" + ex.Message.ToString()
        End Try
    End Sub

End Class
