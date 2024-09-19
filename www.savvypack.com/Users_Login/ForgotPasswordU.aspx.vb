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
Partial Class Users_Login_ForgotPasswordU
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            lbltitle.Text = "Retrieve Password"
            lbltitle.ToolTip = "Submit your email address to retrieve your password by email. We recommend you change your password after receiving it."
            txtEmail.Focus()
            If Request.QueryString("Type").ToString() <> "" Then
                hidtype.Value = Request.QueryString("Type").ToString()
            End If
        Catch ex As Exception
            Response.Write("Error:Page_Load" + ex.Message.ToString())
        End Try
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Dim objgetData As New UsersGetData.Selectdata
        Dim obj As New CryptoHelper
        Dim ds, dsEmail As New DataSet
        Dim strLink, strBody As String
        Try
            ds = objgetData.GetUserIdData(txtEmail.Text)
            If ds.Tables(0).Rows.Count > 0 Then
                If ds.Tables(0).Rows(0).Item("STATUSID").ToString() = 1 Then
                    dsEmail = objgetData.GetEmailConfigDetails("Y")
                    If dsEmail.Tables(0).Rows.Count > 0 Then
                        strLink = dsEmail.Tables(0).Rows(0).Item("URL").ToString()
                        strBody = GetEmailBodyData(strLink, ds.Tables(0).Rows(0).Item("UserName").ToString(), ds.Tables(0).Rows(0).Item("Password").ToString())
                        'Code for Sending Mail
                        SendEmail(strBody, ds.Tables(0).Rows(0).Item("EMAILADD").ToString(), ds.Tables(0).Rows(0).Item("FNAME").ToString())
                        'Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "CloseWindow();", True)
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Please check your mailbox for your credentials.');window.close();", True)
                    End If
                ElseIf ds.Tables(0).Rows(0).Item("STATUSID").ToString() = 2 Then
                    'Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Your account information is incomplete. Please go through create account process to complete it.');", True)
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "ShowAccountUpdation('../Users_Login/AddEditUser.aspx?UN=" + obj.Encrypt(txtEmail.Text) + "&Mode=" + obj.Encrypt("Comp") + "','" + hidtype.Value + "');", True)
                End If
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('This username does not exist. Please create an account.');", True)
            End If
        Catch ex As Exception
            Response.Write("Error:btnSubmit_Click" + ex.Message.ToString())
        End Try
    End Sub

    Public Function GetEmailBodyData(ByVal link As String, ByVal UserName As String, ByVal Password As String) As String
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
            StrSql = StrSql + "<div style='color:white;font-size:18px;font-family:Verdana;font-weight:bold;margin-left:5px;'><b>User Credentials</b> </div> "
            StrSql = StrSql + "</td> "
            StrSql = StrSql + "</tr> "
            StrSql = StrSql + "</table> "
            StrSql = StrSql + "<div style='margin-top:10px;margin-right:10px;font-family:Verdana;font-size:12px;'> "
            StrSql = StrSql + "<table style='font-family:Verdana;width:300px;font-size:12px;border-collapse:collapse' border='0' bordercolor='black' cellpadding='0' cellspacing='0'><tr style='font-weight:bold;text-align:center'>"
            StrSql = StrSql + "<tr><td><b>Email:</b></td><td>" + UserName + "</td></tr> "
            StrSql = StrSql + "<tr><td><b>Password:</b></td><td>" + Password + "</td></tr> "
            StrSql = StrSql + "</table> "
            StrSql = StrSql + "<br /> "
            StrSql = StrSql + "<p> "
            StrSql = StrSql + "<div style='font-family:Verdana;font-size:12px;'> "
            StrSql = StrSql + "SavvyPack Corporation<br/>1850 East 121st Street Suite 106B<br/>Burnsville, MN USA 55337<br/><a style='font-family:verdana;' href='http://www.savvypack.com'>www.savvypack.com</a><br/>Phone: 1-952-405-7500</div> "
            StrSql = StrSql + "</p> "
            StrSql = StrSql + "</div> "
            Return StrSql
        Catch ex As Exception
            Response.Write("Error:GetEmailBodyData:" + ex.Message.ToString())
            Return StrSql
        End Try
    End Function

    Public Sub SendEmail(ByVal strBody As String, ByVal UserName As String, ByVal FirstName As String)
        Try
            Dim objGetData As New UsersGetData.Selectdata
            Dim ds As New DataSet
            ds = objGetData.GetAlliedMemberMail("USRCRD")
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
            Response.Write("Error:SendEmail:" + ex.Message.ToString())
        End Try
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "New_WinCreate", "window.close();", True)
        Catch ex As Exception
            Response.Write("Error:SendEmail:" + ex.Message.ToString())
        End Try
    End Sub

End Class
