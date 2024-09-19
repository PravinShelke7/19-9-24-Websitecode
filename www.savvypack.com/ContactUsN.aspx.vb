Imports System.Data
Imports System.Data.OleDb
Imports System.Net.Mail
Imports System.Net
Partial Class ContactUsN
    Inherits System.Web.UI.Page  
	
#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_ContactUsN")
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        objRefresh.Render(Page)
    End Sub

#End Region

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Dim flag As String
                Dim objCrypto As New CryptoHelper()
                Session("MenuItem") = "CNCTUS"
                If Session("Question") <> Nothing Then
                    txtQuestion.Text = Session("Question").ToString()
                End If
                If Request.QueryString("Flag") <> Nothing Then
                    flag = objCrypto.Decrypt(Request.QueryString("Flag"))
                    If flag = "Y" Then
                        'submitQuestion()
                        txtQuestion.Text = Session("Question").ToString()
                    End If
                End If                
                'btnSubmit.Attributes.Add("onClick", "return OpenLoginPopup('Y');")
            End If
            
            If Session("UserId") <> Nothing Then
                hdnUserId.Value = Session("UserId")
                divLog.Visible = False
            Else
                divLog.Visible = True
            End If
        Catch ex As Exception
            lblError.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try
            Dim obj As New CryptoHelper
            If Session("UserId") <> Nothing Then
                'Session("Question") = txtQuestion.Text
                submitQuestion()
                ' Response.Redirect("~/ContactUsN.aspx?Flag=" + obj.Encrypt("Y").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!"))
            Else
                'Session("Question") = txtQuestion.Text
                'Session("URL") = "~/ContactUsN.aspx?Flag=" + obj.Encrypt("Y").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!")
                'Response.Redirect("Users_Login/Login.aspx")               
                Response.Redirect("~/Universal_loginN/Pages/ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "", False)
            End If

        Catch ex As Exception
            lblError.Text = "Error:btnSubmit_Click:" + ex.Message.ToString()
        End Try
    End Sub
    
    Public Sub submitQuestion()
        Try
            Dim dsUserData As New DataSet
            Dim objgetData As New StudyGetData.Selectdata
            Dim strBody As String = ""
            Dim strBodyUser As String = ""

            dsUserData = objgetData.GetUserDetails(Session("UserId").ToString())
            'strBody = GetEmailBodyData(Session("Question"), dsUserData.Tables(0).Rows(0).Item("PREFIX").ToString(), dsUserData.Tables(0).Rows(0).Item("FIRSTNAME").ToString(), dsUserData.Tables(0).Rows(0).Item("LASTNAME").ToString(), dsUserData.Tables(0).Rows(0).Item("COMPANYNAME").ToString(), dsUserData.Tables(0).Rows(0).Item("EMAILADDRESS").ToString(), dsUserData.Tables(0).Rows(0).Item("PHONENUMBER").ToString())
            strBody = GetEmailBodyData(txtQuestion.Text, dsUserData.Tables(0).Rows(0).Item("PREFIX").ToString(), dsUserData.Tables(0).Rows(0).Item("FIRSTNAME").ToString(), dsUserData.Tables(0).Rows(0).Item("LASTNAME").ToString(), dsUserData.Tables(0).Rows(0).Item("COMPANYNAME").ToString(), dsUserData.Tables(0).Rows(0).Item("EMAILADDRESS").ToString(), dsUserData.Tables(0).Rows(0).Item("PHONENUMBER").ToString(), "ADMIN")
            SendEmail(strBody, "ADMIN", dsUserData.Tables(0).Rows(0).Item("EMAILADDRESS").ToString(), dsUserData.Tables(0).Rows(0).Item("FIRSTNAME").ToString())
            strBody = GetEmailBodyData(txtQuestion.Text, dsUserData.Tables(0).Rows(0).Item("PREFIX").ToString(), dsUserData.Tables(0).Rows(0).Item("FIRSTNAME").ToString(), dsUserData.Tables(0).Rows(0).Item("LASTNAME").ToString(), dsUserData.Tables(0).Rows(0).Item("COMPANYNAME").ToString(), dsUserData.Tables(0).Rows(0).Item("EMAILADDRESS").ToString(), dsUserData.Tables(0).Rows(0).Item("PHONENUMBER").ToString(), "USER")
            SendEmail(strBody, "USER", dsUserData.Tables(0).Rows(0).Item("EMAILADDRESS").ToString(), dsUserData.Tables(0).Rows(0).Item("FIRSTNAME").ToString())
            txtQuestion.Text = ""
            Session("Question") = ""
            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Question Submitted Sucessfully.');", True)
            lblMessage.Text = "Question Submitted Successfully.You will also receive a confirmation email."

        Catch ex As Exception
            lblError.Text = "Error:submitQuestion:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Function GetEmailBodyData(ByVal txtQuestion As String, ByVal PREFIX As String, ByVal FIRSTNAME As String, ByVal LASTNAME As String, ByVal COMPANYNAME As String, ByVal EMAILADDRESS As String, ByVal PHONENUMBER As String, ByVal strName As String) As String
        Dim StrSqlBody As String = ""
        Try
            If strName = "ADMIN" Then
                StrSqlBody = StrSqlBody + "<div style='font-family:Verdana;font-size:15px;margin-top:10px;margin-bottom:10px;font-weight:bold'>Question Details</div>"
                StrSqlBody = StrSqlBody + "<table style='font-family:Verdana;width:700px;font-size:12px;border-collapse:collapse' border='1' bordercolor='black' cellpadding='0' cellspacing='0'>  "
                StrSqlBody = StrSqlBody + "<tr style='background-color:#336699;height:20px;text-align:center;color:white'> "
                StrSqlBody = StrSqlBody + "<td><b>Question</b></td> "
                StrSqlBody = StrSqlBody + "</tr> "
                StrSqlBody = StrSqlBody + "<tr style='height:20px;text-align:Left'> "
                StrSqlBody = StrSqlBody + "<td>" + txtQuestion + "</b></td> "
                StrSqlBody = StrSqlBody + "</tr> "
                StrSqlBody = StrSqlBody + "</table> "
                StrSqlBody = StrSqlBody + "<br /><br />"
                StrSqlBody = StrSqlBody + "<div style='font-family:Verdana;font-size:15px;margin-top:10px;margin-bottom:10px;font-weight:bold'>User Details</div>"
                StrSqlBody = StrSqlBody + "<table style='font-family:Verdana;width:700px;font-size:12px;border-collapse:collapse' border='1' bordercolor='black' cellpadding='0' cellspacing='0'>  "
                StrSqlBody = StrSqlBody + "<tr style='background-color:#336699;height:20px;text-align:center;color:white'> "
                StrSqlBody = StrSqlBody + "<td><b>Prefix</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>First Name</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Last Name</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Company Name</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Email Address</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Phone Number</b></td> "
                StrSqlBody = StrSqlBody + "</tr> "
                StrSqlBody = StrSqlBody + "<tr style='height:20px;text-align:center'> "
                StrSqlBody = StrSqlBody + "<td>" + PREFIX + "</b></td> "
                StrSqlBody = StrSqlBody + "<td>" + FIRSTNAME + "</b></td> "
                StrSqlBody = StrSqlBody + "<td>" + LASTNAME + "</b></td> "
                StrSqlBody = StrSqlBody + "<td>" + COMPANYNAME + "</b></td> "
                StrSqlBody = StrSqlBody + "<td>" + EMAILADDRESS + "</b></td> "
                StrSqlBody = StrSqlBody + "<td>" + PHONENUMBER + "</b></td> "
                StrSqlBody = StrSqlBody + "</tr> "
                StrSqlBody = StrSqlBody + "</table> "
            Else
                Dim dsEmail As New DataSet
                Dim objgetData As New UsersGetData.Selectdata
                dsEmail = objgetData.GetEmailConfigDetails("Y")
                StrSqlBody = "<div style='font-family:Verdana;'>  "
                StrSqlBody = StrSqlBody + "<table cellpadding='0' cellspacing='0' style='width:650px'> "
                StrSqlBody = StrSqlBody + "<tr> "
                StrSqlBody = StrSqlBody + "<td><img src='" + dsEmail.Tables(0).Rows(0).Item("URL").ToString() + "/Images/SavvyPackLogo3.gif' /> "
                StrSqlBody = StrSqlBody + "<br /> "
                StrSqlBody = StrSqlBody + "</td> "
                StrSqlBody = StrSqlBody + "</tr> "
                StrSqlBody = StrSqlBody + "<tr style='background-color:#336699;height:35px;'> "
                StrSqlBody = StrSqlBody + "<td> "
                StrSqlBody = StrSqlBody + "<div style='color:white;font-size:18px;font-family:Verdana;font-weight:bold;margin-left:5px;'><b>Question Submission Confirmation:</b> </div> "
                StrSqlBody = StrSqlBody + "</td> "
                StrSqlBody = StrSqlBody + "</tr> "
                StrSqlBody = StrSqlBody + "</table> "
                StrSqlBody = StrSqlBody + "<div style='margin-top:10px;margin-right:10px;font-family:Verdana;font-size:12px;'> "
                StrSqlBody = StrSqlBody + "<p>Hi " + FIRSTNAME + ",<br/><br/>   Thank you for submitting the following question to SavvyPack Corporation.</p>"
                StrSqlBody = StrSqlBody + " <p style='font-family:Verdana;font-size:12px;width:655px'>" + txtQuestion + "</p> "
                StrSqlBody = StrSqlBody + "<p>We will contact you as soon as possible. </p>"
                StrSqlBody = StrSqlBody + "<p> "
                StrSqlBody = StrSqlBody + "<div style='font-family:Verdana;font-size:12px;'> "
                StrSqlBody = StrSqlBody + "Thank you,<br/> "
                StrSqlBody = StrSqlBody + "Customer Support<br/><br/>SavvyPack Corporation<br/>1850 East 121st Street Suite 106B<br/>Burnsville, MN USA 55337<br/><a style='font-family:verdana;' href='www.savvypack.com'>www.savvypack.com</a><br/>Phone: 1-952-405-7500</div> "
                StrSqlBody = StrSqlBody + "</p> "
                StrSqlBody = StrSqlBody + "</div> "
            End If
            
            Return StrSqlBody
        Catch ex As Exception
            Return StrSqlBody
        End Try
    End Function

    Public Sub SendEmail(ByVal strBody As String, ByVal strName As String, ByVal EmailAdd As String, ByVal FirstName As String)
        Try
            Dim objGetData As New UsersGetData.Selectdata
            Dim ds As New DataSet
            If strName = "ADMIN" Then
                ds = objGetData.GetAlliedMemberMail("QUEST")
            ElseIf strName = "USER" Then
                ds = objGetData.GetAlliedMemberMail("QUESTSUB")
            End If


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
                If strName = "ADMIN" Then
                    Item = New MailAddress(ds.Tables(0).Rows(0).Item("TOADD").ToString(), ds.Tables(0).Rows(0).Item("TONAME").ToString())
                    _To.Add(Item)
                ElseIf strName = "USER" Then
                    Item = New MailAddress(EmailAdd, FirstName)
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

    'Protected Sub lnkLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkLogin.Click, lnkLogin1.Click
    '    Try
    '        If Not objRefresh.IsRefresh Then
    '            Session("Question") = txtQuestion.Text
    '            ScriptManager.RegisterStartupScript(Page, Me.GetType(), "JSScript", "window.open('Users_Login/Login.aspx');", True)
    '        End If

    '    Catch ex As Exception
    '        lblError.Text = "Error:lnkLogin_Click:" + ex.Message.ToString()
    '    End Try
    'End Sub


    'Protected Sub lnkCreate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCreate.Click
    '    Try
    '        If Not objRefresh.IsRefresh Then
    '            Session("Question") = txtQuestion.Text
    '            ScriptManager.RegisterStartupScript(Page, Me.GetType(), "JSScript", "window.open('Users_Login/AddEditUser.aspx?Mode=Add');", True)
    '        End If

    '    Catch ex As Exception
    '        lblError.Text = "Error:lnkCreate_Click:" + ex.Message.ToString()
    '    End Try
    'End Sub

    Protected Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Response.Redirect(HttpContext.Current.Request.Url.ToString(), True)
        Catch ex As Exception
            lblError.Text = "Error:btnRefresh_Click:" + ex.Message.ToString()
        End Try
    End Sub
End Class
