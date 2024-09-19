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
Partial Class Users_Login_ForgotPassword
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
            txtEmail.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnSubmit.ClientID + "')")
            GetMasterPageControls()
            Dim obj As New CryptoHelper
            If Not IsPostBack Then
                If Session("UserId") Is Nothing Or Session("UserId") = "" Then
                Else
                    Response.Redirect("AddEditUser.aspx?Mode=" + obj.Encrypt("Edit").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                End If
            End If
            txtEmail.Focus()
        Catch ex As Exception
            lblError.Text = "Error:Page_Load:" + ex.Message.ToString()
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
            lblError.Text = "Error:GetEmailBodyData:" + ex.Message.ToString()
            Return StrSql
        End Try
    End Function

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

                        lblPassMessage.Text = "Please check your mailbox for your credentials"
                        ContentPage.Visible = False
                        tblPassword.Visible = True
                    End If
                ElseIf ds.Tables(0).Rows(0).Item("STATUSID").ToString() = 2 Then
                    'Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "ShowAccountUpdation('UpdateUsers.aspx?UN=" + obj.Encrypt(txtEmail.Text) + "&Mode=" + obj.Encrypt("Comp") + "');", True)
                     Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "ShowAccountUpdation('AddEditUser.aspx?UN=" + obj.Encrypt(txtEmail.Text) + "&Mode=" + obj.Encrypt("Comp") + "');", True)
                    
                End If
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "ShowAccountAdd('AddEditUser.aspx?Mode=" + obj.Encrypt("AddAcc") + "&UName=" + obj.Encrypt(txtEmail.Text) + "');", True)
            End If
        Catch ex As Exception
            lblError.Text = "Error:btnSubmit_Click:" + ex.Message.ToString()
        End Try
    End Sub
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
            lblError.Text = "Error:SendEmail:" + ex.Message.ToString()
        End Try
    End Sub
End Class
