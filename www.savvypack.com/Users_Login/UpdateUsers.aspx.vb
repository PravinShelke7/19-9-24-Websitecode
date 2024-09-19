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
Partial Class Users_Login_UpdateUsers
    Inherits System.Web.UI.Page
    Dim _strMode As String
    Dim _strName As String

    Public Property Mode() As String
        Get
            Return _strMode
        End Get
        Set(ByVal value As String)
            Dim obj As New CryptoHelper
            _strMode = obj.Decrypt(value)
        End Set
    End Property
    Public Property UNAME() As String
        Get
            Return _strName
        End Get
        Set(ByVal value As String)
            Dim obj As New CryptoHelper
            _strName = obj.Decrypt(value)
        End Set
    End Property

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
        objRefresh = New zCon.Net.Refresh("_USERS_LOGIN_UPDATEUSERS")
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        objRefresh.Render(Page)
    End Sub

#End Region


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            GetMasterPageControls()
            Mode = Request.QueryString("Mode").ToString().Replace("!Plus!", "+").Replace("!Hash!", "#").Replace("!And!", "&")
            UNAME = Request.QueryString("UN").ToString().Replace("!Plus!", "+").Replace("!Hash!", "#").Replace("!And!", "&")
            If Mode = "" Then
                SetFocusOnSubmit(Request.QueryString("Mode").ToString().Replace("!Plus!", "+").Replace("!Hash!", "#").Replace("!And!", "&"))
            Else
                SetFocusOnSubmit(Mode)
            End If
            If Not IsPostBack Then
                If Mode = "Comp" Then
                    dvAddUser.Style.Add("Display", "inline")
                    ddlState.Visible = True
                    txtState.Visible = False
                    BindCountry()
                    BindState(ddlCountry.SelectedValue)
                    GetUserData()
                End If
            End If

        Catch ex As Exception
            lblError.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub SetFocusOnSubmit(ByVal mode As String)
        Try
            If mode = "Comp" Then
                txtEmail.Enabled = False
                'txtPassword.Focus()
                txtPassword.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnReg.ClientID + "')")
                txtConfirmPassword.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnReg.ClientID + "')")
                txtPrefix.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnReg.ClientID + "')")
                txtFirstName.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnReg.ClientID + "')")
                txtLastName.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnReg.ClientID + "')")
                txtphone.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnReg.ClientID + "')")
                txtMob.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnReg.ClientID + "')")
                txtFax.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnReg.ClientID + "')")
                txtCompanyName.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnReg.ClientID + "')")
                txtPosition.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnReg.ClientID + "')")
                txtSAdress1.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnReg.ClientID + "')")
                txtSAdress2.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnReg.ClientID + "')")
                txtCity.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnReg.ClientID + "')")
                txtZipCode.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnReg.ClientID + "')")

            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub BindCountry()
        Dim objGetData As New UsersGetData.Selectdata()
        Dim ds As New DataSet
        Try
            ds = objGetData.GetCountry()
            'new user country dropdown
            With ddlCountry
                .DataValueField = "COUNTRYID"
                .DataTextField = "COUNTRYDES"
                .DataSource = ds
                .DataBind()
            End With
            ddlCountry.SelectedValue = 175

        Catch ex As Exception
            lblError.Text = "Error:BindCountry:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub BindState(ByVal CountryId As Integer)
        Dim objGetData As New UsersGetData.Selectdata()
        Dim ds As New DataSet
        Try
            Dim list As New ListItem
            'Add User
            list.Value = "0"
            list.Text = "Nothing Selected"
            ddlState.Items.Add(list)
            ddlState.AppendDataBoundItems = True
            If CountryId <> 0 Then
                ds = objGetData.GetState()
                With ddlState
                    .DataValueField = "STATEID"
                    .DataTextField = "NAME"
                    .DataSource = ds
                    .DataBind()
                End With
                ddlState.SelectedValue = "0"
            End If

        Catch ex As Exception
            lblError.Text = "Error:BindCountry:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub btnReg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReg.Click
        Dim RType As String = ""
        Dim objGetData As New UsersGetData.Selectdata
        Dim objInsertData As New UsersUpdateData.UpdateInsert
        Dim ds As New DataSet
        Dim dsUserData As New DataSet
        Dim EmailLink As String
        Dim obj As New CryptoHelper
        Dim strBodyData As String
        Dim VerfCode As String = Guid.NewGuid.ToString().Trim().Substring(0, 8)
        Dim dsUserC, dsEmail As New DataSet
        Dim strBody As String

        Dim CompanyNM As String = String.Empty
        Dim DsCompany As New DataSet
        Dim CompanyID As String
        Dim UserNm As String = String.Empty
        Dim DsDomain As New DataSet()
        Dim DomainStatusID As String = String.Empty
        Dim IsApproved As String = String.Empty
        Dim EncEmail, EncUserId, EncType As String
        Dim link As String
        Dim privUSR As Boolean = False
        Try
            DsCompany = objGetData.CheckCompanyExist(txtCompanyName.Text.Trim.ToString())
            If DsCompany.Tables(0).Rows.Count > 0 Then
                CompanyID = DsCompany.Tables(0).Rows(0).Item("COMPANYID").ToString()
                CompanyNM = DsCompany.Tables(0).Rows(0).Item("COMPANYNAME").ToString()
            Else
                CompanyID = objGetData.GetComapnyID()
                CompanyNM = txtCompanyName.Text.Trim.ToString()
            End If

            'Check for Existing User
            dsUserC = objGetData.CheckUserExist(txtEmail.Text)
            dsEmail = objGetData.GetEmailConfigDetails("Y")
            If dsUserC.Tables(0).Rows.Count > 0 Then
                If dsUserC.Tables(0).Rows(0).Item("STATUSID").ToString() = "1" Then
                ElseIf dsUserC.Tables(0).Rows(0).Item("STATUSID").ToString() = "2" Then
                    'Company Changes #PR
                    Dim words As String() = txtEmail.Text.Split("@")
                    UserNm = "@" + words(1)
                    DsDomain = objGetData.GetDomainFlag(UserNm.ToString())

                    If DsDomain.Tables(0).Rows.Count > 0 Then
                        If DsDomain.Tables(0).Rows(0).Item("ACCEPTANCYFLAG") = "1" Then
                            DomainStatusID = 1
                        ElseIf DsDomain.Tables(0).Rows(0).Item("ACCEPTANCYFLAG") = "2" Then
                            DomainStatusID = 2
                        ElseIf DsDomain.Tables(0).Rows(0).Item("ACCEPTANCYFLAG") = "3" Then
                            DomainStatusID = 3
                        ElseIf DsDomain.Tables(0).Rows(0).Item("ACCEPTANCYFLAG") = "4" Then
                            DomainStatusID = 4
                        End If
                    End If

                    If dsUserC.Tables(0).Rows(0).Item("ISVALIDEMAIL").ToString() = "Y" And dsUserC.Tables(0).Rows(0).Item("ISAPPROVED").ToString() = "Y" Then
                        privUSR = True
                    End If
                    'Updating details with password(Users already exist with inccomplete details)
                    If ddlCountry.SelectedItem.Text = "United States" Then
                        objInsertData.UpdateINCOMPUserDetails(txtEmail.Text.Trim(), txtPassword.Text.Replace("'", "''"), CompanyID.ToString(), txtPrefix.Text.Replace("'", "''"), txtFirstName.Text.Replace("'", "''"), txtLastName.Text.Replace("'", "''"), txtPosition.Text.Replace("'", "''"), txtphone.Text.Replace("'", "''"), txtFax.Text.Replace("'", "''"), txtSAdress1.Text.Replace("'", "''"), txtSAdress2.Text.Replace("'", "''"), txtCity.Text.Replace("'", "''"), ddlState.SelectedItem.Text, txtZipCode.Text.Replace("'", "''"), ddlCountry.SelectedValue, VerfCode, CompanyNM.Replace("'", "''").ToString(), txtMob.Text.Replace("'", "''"), DomainStatusID, privUSR)
                    Else
                        objInsertData.UpdateINCOMPUserDetails(txtEmail.Text.Trim(), txtPassword.Text.Replace("'", "''"), CompanyID.ToString(), txtPrefix.Text.Replace("'", "''"), txtFirstName.Text.Replace("'", "''"), txtLastName.Text.Replace("'", "''"), txtPosition.Text.Replace("'", "''"), txtphone.Text.Replace("'", "''"), txtFax.Text.Replace("'", "''"), txtSAdress1.Text.Replace("'", "''"), txtSAdress2.Text.Replace("'", "''"), txtCity.Text.Replace("'", "''"), txtState.Text.Replace("'", "''"), txtZipCode.Text.Replace("'", "''"), ddlCountry.SelectedValue, VerfCode, CompanyNM.Replace("'", "''").ToString(), txtMob.Text.Replace("'", "''"), DomainStatusID, privUSR)
                    End If
                    ds = objGetData.GetUDetails(UNAME)
                    If privUSR = False Then

                        If dsEmail.Tables(0).Rows.Count > 0 Then
                            link = dsEmail.Tables(0).Rows(0).Item("URL").ToString()
                            If ds.Tables(0).Rows.Count > 0 Then
                                EncEmail = obj.Encrypt(ds.Tables(0).Rows(0).Item("EMAILADDRESS").ToString()).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + ""
                                EncUserId = obj.Encrypt(ds.Tables(0).Rows(0).Item("USERID").ToString()).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + ""
                                EncType = obj.Encrypt(RType).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + ""
                                EmailLink = link + "/Users_Login/EmailVarification.aspx?Email=" + EncEmail + "&UserID=" + EncUserId + "&Type=" + EncType + ""
                                strBodyData = GetEmailBodyData(link, EmailLink, VerfCode)
                                'Code for sending Email
                                SendEmail(strBodyData, ds.Tables(0).Rows(0).Item("EMAILADDRESS").ToString(), txtFirstName.Text)
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "ShowPopWindow('VerificationU.aspx?UID=" + EncUserId + "');", True)
                            End If
                        End If
                    Else
                        'Sending email to Internal User
                        strBody = GetEmailBodyData(dsEmail.Tables(0).Rows(0).Item("URL").ToString(), ds.Tables(0).Rows(0).Item("Name").ToString(), ds.Tables(0).Rows(0).Item("USERCONTACTID").ToString(), ds.Tables(0).Rows(0).Item("EMAILADDRESS").ToString())
                        SendEmail(strBody)
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "ShowConfirmMessage();", True)
                    End If
                   
                    ''Sending email to Internal User
                    'strBody = GetEmailBodyData(dsEmail.Tables(0).Rows(0).Item("URL").ToString(), ds.Tables(0).Rows(0).Item("Name").ToString(), ds.Tables(0).Rows(0).Item("USERCONTACTID").ToString(), ds.Tables(0).Rows(0).Item("EMAILADDRESS").ToString())
                    'SendEmail(strBody)
                    'Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "ShowConfirmMessage();", True)
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error:btnReg_Click:" + ex.Message.ToString()
        End Try
    End Sub
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
    Public Sub GetUserData()
        Try
            Dim objGetData As New UsersGetData.Selectdata()
            Dim ds As New DataSet()
            ds = objGetData.GetUDetails(UNAME)
            If ds.Tables(0).Rows.Count > 0 Then
                If ds.Tables(0).Rows(0).Item("COMPANYNAME").ToString() <> "" Then
                    txtCompanyName.Enabled = False
                End If
                txtPrefix.Text = ds.Tables(0).Rows(0).Item("PREFIX").ToString()
                txtFirstName.Text = ds.Tables(0).Rows(0).Item("FIRSTNAME").ToString()
                txtLastName.Text = ds.Tables(0).Rows(0).Item("LASTNAME").ToString()
                txtphone.Text = ds.Tables(0).Rows(0).Item("PHONENUMBER").ToString()
                txtMob.Text = ds.Tables(0).Rows(0).Item("MOBILENUMBER").ToString()
                txtFax.Text = ds.Tables(0).Rows(0).Item("FAXNUMBER").ToString()
                txtEmail.Text = ds.Tables(0).Rows(0).Item("USERNAME").ToString()
                txtCompanyName.Text = ds.Tables(0).Rows(0).Item("COMPANYNAME").ToString()
                txtPosition.Text = ds.Tables(0).Rows(0).Item("JOBTITLE").ToString()
                txtSAdress1.Text = ds.Tables(0).Rows(0).Item("STREETADDRESS1").ToString()
                txtSAdress2.Text = ds.Tables(0).Rows(0).Item("STREETADDRESS2").ToString()
                txtCity.Text = ds.Tables(0).Rows(0).Item("CITY").ToString()
                txtZipCode.Text = ds.Tables(0).Rows(0).Item("ZIPCODE").ToString()
                If ds.Tables(0).Rows(0).Item("COUNTRY").ToString() = "" Then
                    ddlCountry.SelectedValue = "175"
                Else
                    ddlCountry.SelectedValue = ds.Tables(0).Rows(0).Item("COUNTRYID").ToString()
                End If

                If ddlCountry.SelectedValue = "175" Then
                    ddlState.SelectedValue = GetStateId(ds.Tables(0).Rows(0).Item("STATE").ToString())
                    ddlState.Visible = True
                    txtState.Visible = False
                Else
                    ddlState.Visible = False
                    txtState.Visible = True
                    txtState.Text = ds.Tables(0).Rows(0).Item("STATE").ToString()
                End If
            End If

        Catch ex As Exception
            lblError.Text = "Error:GetUserData:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Function GetStateId(ByVal stateDes As String) As Integer
        Dim objGetData As New UsersGetData.Selectdata
        Dim ds As New DataSet
        Try
            ds = objGetData.GetStateByDes(stateDes)
            If (ds.Tables(0).Rows(0).Item("STATEID").ToString() = "") Then
                Return (0)
            Else
                Return CInt(ds.Tables(0).Rows(0).Item("STATEID").ToString())
            End If

        Catch ex As Exception
            Return (0)
        End Try

    End Function
    Protected Sub lnkForgotPass_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkForgotPass.Click
        Try
            Response.Redirect("~/Users_Login/ForgotPassword.aspx")
        Catch ex As Exception
            lblError.Text = "Error:lnkForgotPass_Click:" + ex.Message.ToString()
        End Try
    End Sub
    Public Sub SendEmail(ByVal strBody As String)
        Try
            Dim objGetData As New UsersGetData.Selectdata
            Dim ds As New DataSet
            ds = objGetData.GetAlliedMemberMail("ACTCOMPL")
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
            lblError.Text = "Error:SendEmail:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Function GetEmailBodyData(ByVal link As String, ByVal Name As String, ByVal USERCONTACTID As String, ByVal EMAILADDRESS As String) As String
        Dim StrSqlBdy As String = ""
        Dim strAction As String = ""
        Try
            strAction = "This user has updated his details sucessfully."
            StrSqlBdy = "<div style='font-family:Verdana;'>  "
            StrSqlBdy = StrSqlBdy + "<table cellpadding='0' cellspacing='0' style='width:650px'> "
            StrSqlBdy = StrSqlBdy + "<tr> "
            StrSqlBdy = StrSqlBdy + "<td><img src='" + link + "/Images/SavvyPackLogo3.gif' /> "
            StrSqlBdy = StrSqlBdy + "<br /> "
            StrSqlBdy = StrSqlBdy + "</td> "
            StrSqlBdy = StrSqlBdy + "</tr> "
            StrSqlBdy = StrSqlBdy + "<tr style='background-color:#336699;height:35px;'> "
            StrSqlBdy = StrSqlBdy + "<td> "
            StrSqlBdy = StrSqlBdy + "<div style='color:white;font-size:18px;font-family:Verdana;font-weight:bold;margin-left:5px;'><b>" + strAction + "</b> </div> "
            StrSqlBdy = StrSqlBdy + "</td> "
            StrSqlBdy = StrSqlBdy + "</tr> "
            StrSqlBdy = StrSqlBdy + "</table> "
            StrSqlBdy = StrSqlBdy + "<div style='margin-top:10px;margin-right:10px;font-family:Verdana;font-size:12px;'> "
            StrSqlBdy = StrSqlBdy + "<table style='font-family:Verdana;width:650px;font-size:12px;border-collapse:collapse' border='1' bordercolor='black' cellpadding='0' cellspacing='0'><tr style='font-weight:bold;text-align:center'><td>User Contact Id</td><td>Name</td><td>Email Address</td></tr> "
            StrSqlBdy = StrSqlBdy + "<tr><td>" + USERCONTACTID + "</td><td>" + Name + "</td><td>" + EMAILADDRESS + "</td></tr> "
            StrSqlBdy = StrSqlBdy + "</table> "
            StrSqlBdy = StrSqlBdy + "<p>" + strAction + "</p> "
            StrSqlBdy = StrSqlBdy + "<br /> "
            StrSqlBdy = StrSqlBdy + "<p> "
            StrSqlBdy = StrSqlBdy + "<div style='font-family:Verdana;font-size:12px;'> "
            StrSqlBdy = StrSqlBdy + "SavvyPack Corporation<br/>1850 East 121st Street Suite 106B<br/>Burnsville, MN USA 55337<br/><a style='font-family:verdana;' href='http://www.savvypack.com'>www.savvypack.com</a><br/>Phone: 1-952-405-7500</div> "
            StrSqlBdy = StrSqlBdy + "</p> "
            StrSqlBdy = StrSqlBdy + "</div> "

            Return StrSqlBdy
        Catch ex As Exception
            lblError.Text = "Error:GetEmailBodyData:" + ex.Message.ToString()
            Return StrSqlBdy
        End Try
    End Function
    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            Response.Redirect("~/Index.aspx")
        Catch ex As Exception
            lblError.Text = "Error:btnCancel_Click:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub ddlCountry_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCountry.SelectedIndexChanged
        Try
            Dim password As String = ""
            Dim cpassword As String = ""
            password = txtPassword.Text
            cpassword = txtConfirmPassword.Text
            If ddlCountry.SelectedItem.Text = "United States" Then
                ddlState.Visible = True
                txtState.Visible = False
                BindState("0")
            Else
                ddlState.Visible = False
                txtState.Visible = True
                If ddlCountry.SelectedValue = "0" Then
                    txtState.Enabled = False
                Else
                    txtState.Enabled = True
                End If
            End If
            txtPassword.Attributes.Add("value", password)
            txtConfirmPassword.Attributes.Add("value", cpassword)
        Catch ex As Exception
            lblError.Text = "Error:ddlCountry_SelectedIndexChanged:" + ex.Message.ToString()
        End Try
    End Sub
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
    Protected Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Try
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "UserLogin", "CloseWindow();", True)
        Catch ex As Exception
            lblError.Text = "Error:btnClose_Click:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub btnCloseAcp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCloseAcp.Click
        Try
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "UserLogin", "CloseWindowAcp();", True)
        Catch ex As Exception
            lblError.Text = "Error:btnClose_Click:" + ex.Message.ToString()
        End Try
    End Sub
End Class
