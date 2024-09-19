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
Partial Class Users_Login_AddEditUser
    Inherits System.Web.UI.Page
    Dim _strMode As String
    Dim _strType As String
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

    Public Property Type() As String
        Get
            Return _strType
        End Get
        Set(ByVal value As String)
            Dim obj As New CryptoHelper
            _strType = obj.Decrypt(value)
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
        objRefresh = New zCon.Net.Refresh("_USERS_LOGIN_ADDEDITUSER")
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        objRefresh.Render(Page)
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            lblTitleE.Text = "Edit Profile"
            GetMasterPageControls()
            Mode = Request.QueryString("Mode").ToString().Replace("!Plus!", "+").Replace("!Hash!", "#").Replace("!And!", "&")
            Session("MenuItem") = "ACNT1"
            If Mode = "" Then
                SetFocusOnSubmit(Request.QueryString("Mode").ToString().Replace("!Plus!", "+").Replace("!Hash!", "#").Replace("!And!", "&"))
            ElseIf Mode = "Comp" Then
                btncancelE.Visible = False
                LinkButton2.Visible = False
                lblTitleE.Text = "Account Information"
                UNAME = Request.QueryString("UN").ToString().Replace("!Plus!", "+").Replace("!Hash!", "#").Replace("!And!", "&")
                SetFocusOnSubmit(Mode)
            ElseIf Mode = "AddAcc" Then
                UNAME = Request.QueryString("UName").ToString().Replace("!Plus!", "+").Replace("!Hash!", "#").Replace("!And!", "&")
                SetFocusOnSubmit("Add")
            Else
                SetFocusOnSubmit(Mode)
                btncancelU.Visible = False
            End If
            hidCompMode.Value = Mode

            If Not IsPostBack Then
                If Mode = "Edit" Then
                    If Session("UserId") = "" Or Session("UserId") Is Nothing Then
                        Dim obj As New CryptoHelper
                        Response.Redirect("~/Universal_loginN/Pages/ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "", False)
                        'Response.Redirect("~/Index.aspx")
                    Else
                        If Request.QueryString("Msg") <> Nothing Then
                            If Request.QueryString("Msg").ToString() = "Y" Then
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Your account has been activated successfully.');", True)
                            End If
                        End If

                        dvEditUser.Style.Add("Display", "inline")
                        dvAddUser.Style.Add("Display", "none")

                        trPassE.Style.Add("Display", "none")
                        trCnfPassE.Style.Add("Display", "none")
                        BindCountry()
                        BindState(ddlCountry.SelectedValue)
                        GetUserData()
                    End If

                ElseIf Mode = "Comp" Then
                    dvEditUser.Style.Add("Display", "inline")
                    dvAddUser.Style.Add("Display", "none")
                    trPromoMail.Visible = False
                    BindCountry()
                    BindState(ddlCountry.SelectedValue)
                    GetUserDataComp()
                ElseIf Mode = "AddAcc" Then
                    dvEditUser.Style.Add("Display", "none")
                    dvAddUser.Style.Add("Display", "inline")
                    Session("MenuItem") = "ACNT"
                    ddlState.Visible = True
                    txtState.Visible = False
                    BindCountry()
                    BindState(ddlCountry.SelectedValue)
                    txtEmail.Focus()
                    GetNewUserInfo()
                Else
                    dvEditUser.Style.Add("Display", "none")
                    dvAddUser.Style.Add("Display", "inline")
                    ddlState.Visible = True
                    txtState.Visible = False
                    BindCountry()
                    BindState(ddlCountry.SelectedValue)
                    txtEmail.Focus()
                End If
            Else
                If Mode = "Edit" Then
                    If Session("Back") = Nothing Then
                        If Session("SBack") = Nothing Then
                            Dim obj As New CryptoHelper
                            Response.Redirect("~/Universal_loginN/Pages/ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "", False)
                        End If
                    End If
                End If

            End If

            If Request.QueryString("Type") <> "" Then
                Type = Request.QueryString("Type").ToString().Replace("!Plus!", "+").Replace("!Hash!", "#").Replace("!And!", "&")
            Else
                Type = ""
            End If

        Catch ex As Exception
            lblError.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub SetFocusOnSubmit(ByVal mode As String)
        Try
            If mode = "Add" Then
                'txtEmail.Focus()
                txtEmail.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnReg.ClientID + "')")
                txtCEmail.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnReg.ClientID + "')")
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
				txtState.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnReg.ClientID + "')")
            Else
                If mode = "Comp" Then
                    txtPasswordE.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnUpdate.ClientID + "')")
                    txtConfirmPasswordE.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnUpdate.ClientID + "')")
                End If
                txtEmailE.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnUpdate.ClientID + "')")
                txtpreFixE.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnUpdate.ClientID + "')")
                txtFNameE.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnUpdate.ClientID + "')")
                txtLNameE.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnUpdate.ClientID + "')")
                txtphoneE.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnUpdate.ClientID + "')")
                txtMobE.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnUpdate.ClientID + "')")
                txtFaxE.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnUpdate.ClientID + "')")
                txtCompNameE.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnUpdate.ClientID + "')")
                txtposE.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnUpdate.ClientID + "')")
                txtStAddress1E.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnUpdate.ClientID + "')")
                txtStAddress2E.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnUpdate.ClientID + "')")
                txtCityE.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnUpdate.ClientID + "')")
				txtStateE.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnUpdate.ClientID + "')")
                txtZipCodeE.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnUpdate.ClientID + "')")
				rYes.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnUpdate.ClientID + "')")
                rNo.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnUpdate.ClientID + "')")
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

            'Edit User Country Dropdown
            With ddlCountryE
                .DataValueField = "COUNTRYID"
                .DataTextField = "COUNTRYDES"
                .DataSource = ds
                .DataBind()
            End With

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
            ddlState.Items.Clear()
            list.Value = "0"
            list.Text = "Nothing Selected"
            ddlState.Items.Add(list)
            ddlState.AppendDataBoundItems = True
            If CountryId <> 0 Then
                ds = objGetData.GetStateByCountry(CountryId)
                With ddlState
                    .DataValueField = "STATEID"
                    .DataTextField = "NAME"
                    .DataSource = ds
                    .DataBind()
                End With
                ddlState.SelectedValue = "0"
            End If

            'Edit User
            list = New ListItem
            ddlStateE.Items.Clear()
            list.Value = "0"
            list.Text = "Nothing Selected"
            ddlStateE.Items.Add(list)
            ddlStateE.AppendDataBoundItems = True
            If CountryId <> 0 Then
                ds = objGetData.GetStateByCountry(CountryId)
                With ddlStateE
                    .DataValueField = "STATEID"
                    .DataTextField = "NAME"
                    .DataSource = ds
                    .DataBind()
                End With
                ddlStateE.SelectedValue = "0"
            End If



        Catch ex As Exception
            lblError.Text = "Error:BindCountry:" + ex.Message.ToString()
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

    Protected Sub btnReg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReg.Click
        Dim RType As String = ""
        Dim objGetData As New UsersGetData.Selectdata
        Dim objInsertData As New UsersUpdateData.UpdateInsert
        Dim ds As New DataSet
        Dim dsUserData As New DataSet
        Dim EmailLink, link As String
        Dim obj As New CryptoHelper
        Dim strBodyData As String
        Dim VerfCode As String = Guid.NewGuid.ToString().Trim().Substring(0, 8)
        Dim dsUserC As New DataSet

        Dim dsEmail, dsU As New DataSet
        Dim strBody As String

        'Company Changes #PR
        Dim CompanyNM As String = String.Empty
        Dim DsCompany As New DataSet
        Dim CompanyID As String
        Dim UserNm As String = String.Empty
        Dim DsDomain As New DataSet()
        Dim DomainStatusID As String = String.Empty
        Dim IsApproved As String = String.Empty
        Dim EncEmail, EncUserId, EncType As String
        Dim DomainID As String
        Dim ComStatus As Boolean = False
        Dim NewCompany As Boolean = False
        Dim NewDomain As Boolean = False
        Dim dsCompanyNew As New DataSet
        '14th_Dec
        Dim FirstName As String = String.Empty
        Dim LastName As String = String.Empty
        Dim LicUserNm As String = String.Empty

        Try
            If Type = "" Then
                RType = "NA"
            Else
                If Type = "WCNF" Then
                    'Remaining
                    RType = "~/WebConferenceN/UserDetails.aspx?ID=" & Session("FConFid") & ""
                End If
            End If

            'Check for Existing User
            dsUserC = objGetData.CheckUserExist(txtEmail.Text)
            dsEmail = objGetData.GetEmailConfigDetails("Y")
            If dsUserC.Tables(0).Rows.Count > 0 Then
                If dsUserC.Tables(0).Rows(0).Item("STATUSID").ToString() = "1" Then
                    'Users already exist with complete details
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Win3", "MessageWindow();", True)
                    lnkForgotPass.Style.Add("display", "inline")
                ElseIf dsUserC.Tables(0).Rows(0).Item("STATUSID").ToString() = "2" Then
                    'Updating details with password(Users already exist with inccomplete details)
                    hidStatusId.Value = "2"
                    If ddlCountry.SelectedItem.Text = "United States" Then
                        'objInsertData.UpdateINCOMPUserDetails(txtEmail.Text.Trim(), txtPassword.Text.Replace("'", "''"), txtCompanyName.Text.Replace("'", "''"), txtPrefix.Text.Replace("'", "''"), txtFirstName.Text.Replace("'", "''"), txtLastName.Text.Replace("'", "''"), txtPosition.Text.Replace("'", "''"), txtphone.Text.Replace("'", "''"), txtFax.Text.Replace("'", "''"), txtSAdress1.Text.Replace("'", "''"), txtSAdress2.Text.Replace("'", "''"), txtCity.Text.Replace("'", "''"), ddlState.SelectedItem.Text, txtZipCode.Text.Replace("'", "''"), ddlCountry.SelectedValue, VerfCode)
                    Else
                        'objInsertData.UpdateINCOMPUserDetails(txtEmail.Text.Trim(), txtPassword.Text.Replace("'", "''"), txtCompanyName.Text.Replace("'", "''"), txtPrefix.Text.Replace("'", "''"), txtFirstName.Text.Replace("'", "''"), txtLastName.Text.Replace("'", "''"), txtPosition.Text.Replace("'", "''"), txtphone.Text.Replace("'", "''"), txtFax.Text.Replace("'", "''"), txtSAdress1.Text.Replace("'", "''"), txtSAdress2.Text.Replace("'", "''"), txtCity.Text.Replace("'", "''"), txtState.Text.Replace("'", "''"), txtZipCode.Text.Replace("'", "''"), ddlCountry.SelectedValue, VerfCode)
                    End If
                    'Sending email to Internal User
                    dsU = objGetData.GetUDetails(txtEmail.Text)
                    strBody = GetEmailBodyDataU(dsEmail.Tables(0).Rows(0).Item("URL").ToString(), dsU.Tables(0).Rows(0).Item("Name").ToString(), dsU.Tables(0).Rows(0).Item("USERCONTACTID").ToString(), dsU.Tables(0).Rows(0).Item("EMAILADDRESS").ToString())
                    SendEmailU(strBody)
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "ShowConfirmMessage();", True)
                End If
            Else
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
                    DomainID = DsDomain.Tables(0).Rows(0).Item("DOMAINID").ToString()
                Else
                    DomainStatusID = 4
                    DomainID = objInsertData.UpdateURdomain(UserNm.ToString())
                    NewDomain = True
                End If

                If DomainStatusID = 3 Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "ShowAccountUpdation();", True)
                    hidPass.Value = txtPassword.Text
                    hidConf.Value = txtConfirmPassword.Text
                    hidDomID3.Value = DomainID.ToString()
                    txtPassword.Attributes.Add("value", hidPass.Value)
                    txtConfirmPassword.Attributes.Add("value", hidConf.Value)
                Else

                    'Company Changes #PR
                    If ddlCompany.Items.Count <> "0" Then
                        If ddlCompany.SelectedItem.Text <> "Other" Then
                            CompanyID = ddlCompany.SelectedValue.ToString()
                            CompanyNM = ddlCompany.SelectedItem.ToString()
                        Else
                            DsCompany = objGetData.CheckCompanyExist(txtCompanyName.Text.Trim.ToString())
                            If DsCompany.Tables(0).Rows.Count > 0 Then
                                CompanyID = DsCompany.Tables(0).Rows(0).Item("COMPANYID").ToString()
                                CompanyNM = DsCompany.Tables(0).Rows(0).Item("COMPANYNAME").ToString()
                            Else
                                CompanyID = objGetData.GetComapnyID()
                                CompanyNM = txtCompanyName.Text.Trim.ToString()
                                NewCompany = True
                            End If
                            ComStatus = True
                        End If
                    Else
                        DsCompany = objGetData.CheckCompanyExist(txtCompanyName.Text.Trim.ToString())
                        If DsCompany.Tables(0).Rows.Count > 0 Then
                            CompanyID = DsCompany.Tables(0).Rows(0).Item("COMPANYID").ToString()
                            CompanyNM = DsCompany.Tables(0).Rows(0).Item("COMPANYNAME").ToString()
                        Else
                            CompanyID = objGetData.GetComapnyID()
                            CompanyNM = txtCompanyName.Text.Trim.ToString()
                            NewCompany = True
                        End If
                        ComStatus = True
                    End If
                    'End

                    '24_Nov_2017
                    dsCompanyNew = objGetData.CheckNewCompany(CompanyID)
                    If dsCompanyNew.Tables(0).Rows.Count > 0 Then
                        If dsCompanyNew.Tables(0).Rows(0).Item("ISNEW").ToString() = "Y" Then
                            NewCompany = True
                        End If
                    End If
                    'End

                    If DomainStatusID = 1 Or DomainStatusID = 4 Then
                        IsApproved = "Y"
                    Else
                        IsApproved = ""
                    End If


                    'Inserting User Details
                    '10th_jan_2017
                    Dim CAvail As Integer = GetCountryInState(ddlCountry.SelectedValue.ToString())
                    If CAvail <> 0 Then
                        objInsertData.InsertUserDetails(txtEmail.Text.Trim(), txtPassword.Text.Replace("'", "''"), CompanyID.ToString(), txtPrefix.Text.Replace("'", "''"), txtFirstName.Text.Replace("'", "''"), txtLastName.Text.Replace("'", "''"), txtPosition.Text.Replace("'", "''"), txtphone.Text.Replace("'", "''"), txtFax.Text.Replace("'", "''"), txtSAdress1.Text.Replace("'", "''"), txtSAdress2.Text.Replace("'", "''"), txtCity.Text.Replace("'", "''"), ddlState.SelectedItem.Text, txtZipCode.Text.Replace("'", "''"), ddlCountry.SelectedValue, VerfCode, CompanyNM.Replace("'", "''").ToString(), DomainStatusID, IsApproved, txtMob.Text.Replace("'", "''"), DomainID.ToString(), ComStatus)
                    Else
                        objInsertData.InsertUserDetails(txtEmail.Text.Trim(), txtPassword.Text.Replace("'", "''"), CompanyID.ToString(), txtPrefix.Text.Replace("'", "''"), txtFirstName.Text.Replace("'", "''"), txtLastName.Text.Replace("'", "''"), txtPosition.Text.Replace("'", "''"), txtphone.Text.Replace("'", "''"), txtFax.Text.Replace("'", "''"), txtSAdress1.Text.Replace("'", "''"), txtSAdress2.Text.Replace("'", "''"), txtCity.Text.Replace("'", "''"), txtState.Text.Replace("'", "''"), txtZipCode.Text.Replace("'", "''"), ddlCountry.SelectedValue, VerfCode, CompanyNM.Replace("'", "''").ToString(), DomainStatusID, IsApproved, txtMob.Text.Replace("'", "''"), DomainID.ToString(), ComStatus)
                    End If

                    'If ddlCountry.SelectedItem.Text = "United States" Then
                    '    objInsertData.InsertUserDetails(txtEmail.Text.Trim(), txtPassword.Text.Replace("'", "''"), CompanyID.ToString(), txtPrefix.Text.Replace("'", "''"), txtFirstName.Text.Replace("'", "''"), txtLastName.Text.Replace("'", "''"), txtPosition.Text.Replace("'", "''"), txtphone.Text.Replace("'", "''"), txtFax.Text.Replace("'", "''"), txtSAdress1.Text.Replace("'", "''"), txtSAdress2.Text.Replace("'", "''"), txtCity.Text.Replace("'", "''"), ddlState.SelectedItem.Text, txtZipCode.Text.Replace("'", "''"), ddlCountry.SelectedValue, VerfCode, CompanyNM.Replace("'", "''").ToString(), DomainStatusID, IsApproved, txtMob.Text.Replace("'", "''"), DomainID.ToString(), ComStatus)
                    'Else
                    '    objInsertData.InsertUserDetails(txtEmail.Text.Trim(), txtPassword.Text.Replace("'", "''"), CompanyID.ToString(), txtPrefix.Text.Replace("'", "''"), txtFirstName.Text.Replace("'", "''"), txtLastName.Text.Replace("'", "''"), txtPosition.Text.Replace("'", "''"), txtphone.Text.Replace("'", "''"), txtFax.Text.Replace("'", "''"), txtSAdress1.Text.Replace("'", "''"), txtSAdress2.Text.Replace("'", "''"), txtCity.Text.Replace("'", "''"), txtState.Text.Replace("'", "''"), txtZipCode.Text.Replace("'", "''"), ddlCountry.SelectedValue, VerfCode, CompanyNM.Replace("'", "''").ToString(), DomainStatusID, IsApproved, txtMob.Text.Replace("'", "''"), DomainID.ToString(), ComStatus)
                    'End If

                    'Get Email Config Details
                    Dim objUpdateData As New UsersUpdateData.UpdateInsert()
                    ds = objGetData.GetEmailConfigDetails("Y")
                    dsUserData = objGetData.GetUserId(txtEmail.Text)
                    'dsUserData = objGetData.ValidateUserWithCode(txtEmail.Text.Trim(), txtPassword.Text.Trim().Replace("'", "''"), "#")
                    If ds.Tables(0).Rows.Count > 0 Then
                        link = ds.Tables(0).Rows(0).Item("URL").ToString()
                        If dsUserData.Tables(0).Rows.Count > 0 Then

                            'Bug#PK20_1
                            EncEmail = obj.Encrypt(dsUserData.Tables(0).Rows(0).Item("EMAILADD").ToString()).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + ""
                            EncUserId = obj.Encrypt(dsUserData.Tables(0).Rows(0).Item("UserId").ToString()).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + ""
                            EncType = obj.Encrypt(RType).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + ""
                            EmailLink = link + "/Users_Login/EmailVarification.aspx?Email=" + EncEmail + "&UserID=" + EncUserId + "&Type=" + EncType + ""
                            strBodyData = GetEmailBodyData(link, EmailLink, VerfCode)
                            'Code for sending Email
                            SendEmail(strBodyData, dsUserData.Tables(0).Rows(0).Item("EMAILADD").ToString(), txtFirstName.Text)
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "ShowPopWindow('Verification.aspx?UID=" + EncUserId + "&Type=Acc');", True)
                            'End Bug#PK20_1

                            'If DomainStatusID = 1 Then
                            '    If NewCompany = True Or NewDomain = True Then
                            '        strBodyData = String.Empty
                            '        strBodyData = GetEmailBodyDataNotify(link, CompanyID, CompanyNM, NewCompany, DomainID, UserNm.ToString(), NewDomain, dsUserData.Tables(0).Rows(0).Item("Name").ToString(), dsUserData.Tables(0).Rows(0).Item("USERCONTACTID").ToString(), dsUserData.Tables(0).Rows(0).Item("EMAILADD").ToString(), "Y")
                            '        SendEmailNotify(strBodyData, "NEWADDITIONS")
                            '    Else
                            '        strBodyData = GetEmailBodyData(link, dsUserData.Tables(0).Rows(0).Item("Name").ToString(), dsUserData.Tables(0).Rows(0).Item("USERCONTACTID").ToString(), dsUserData.Tables(0).Rows(0).Item("EMAILADD").ToString(), "Y")
                            '        SendEmail(strBodyData, "VERFEMAIL")
                            '    End If
                            '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "ShowStatusAccep();", True)
                            'Else
                            '    'If dsUserData.Tables(0).Rows.Count > 0 Then
                            '    '    'objUpdateData.UpdateEmailValidationFlag(dsUserData.Tables(0).Rows(0).Item("USERID").ToString(), dsUserData.Tables(0).Rows(0).Item("USERNAME").ToString())
                            '    '    'mail for verification
                            '    '    EmailLink = ds.Tables(0).Rows(0).Item("URL").ToString()
                            '    '    'Sending mail
                            '    '    strBodyData = GetEmailBodyData(EmailLink, dsUserData.Tables(0).Rows(0).Item("Name").ToString(), dsUserData.Tables(0).Rows(0).Item("USERCONTACTID").ToString(), dsUserData.Tables(0).Rows(0).Item("EMAILADDRESS").ToString())
                            '    '    SendEmail(strBodyData, "VERFEMAIL")
                            '    'End If
                            '    EncEmail = obj.Encrypt(dsUserData.Tables(0).Rows(0).Item("EMAILADD").ToString()).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + ""
                            '    EncUserId = obj.Encrypt(dsUserData.Tables(0).Rows(0).Item("UserId").ToString()).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + ""
                            '    EncType = obj.Encrypt(RType).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + ""
                            '    EmailLink = link + "/Users_Login/EmailVarification.aspx?Email=" + EncEmail + "&UserID=" + EncUserId + "&Type=" + EncType + ""
                            '    strBodyData = GetEmailBodyData(link, EmailLink, VerfCode)
                            '    'Code for sending Email
                            '    SendEmail(strBodyData, dsUserData.Tables(0).Rows(0).Item("EMAILADD").ToString(), txtFirstName.Text)
                            '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "ShowPopWindow('Verification.aspx?UID=" + EncUserId + "&Type=Acc');", True)
                            'End If

                            '14th_Dec_Assign_License
                            FirstName = txtFirstName.Text.Replace("'", "''").ToString().Substring(0, If(txtFirstName.Text.Replace("'", "''").ToString().Length >= 1, 1, txtFirstName.Text.Replace("'", "''").ToString().Length))
                            LastName = txtLastName.Text.Replace("'", "''").ToString().Substring(0, If(txtLastName.Text.Replace("'", "''").ToString().Length >= 1, 1, txtLastName.Text.Replace("'", "''").ToString().Length))
                            LicUserNm = FirstName.ToLower() + LastName.ToLower()
                            objInsertData.AddLicenseToNewUser(LicUserNm, dsUserData.Tables(0).Rows(0).Item("UserId").ToString())

                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error:btnReg_Click:" + ex.Message.ToString()
        End Try
    End Sub

#Region "Edit/Complete Account"

    Public Sub GetUserData()
        Dim UserNm As String = String.Empty
        Dim DsDomain As New DataSet()
        Dim CompanyName As String = String.Empty
        Dim lst As New ListItem
        Dim DomNotAvail As Boolean = False
        Dim ComNotAvail As Boolean = False
        Try
            Dim objGetData As New UsersGetData.Selectdata()
            Dim ds As New DataSet()
            ds = objGetData.GetUserDetails(Session("UserId").ToString())

            Dim words As String() = ds.Tables(0).Rows(0).Item("USERNAME").ToString().Split("@")
            If words.Length() = 2 Then
                UserNm = "@" + words(1)
                DsDomain = objGetData.GetDomainFlag(UserNm.ToString())

                If DsDomain.Tables(0).Rows.Count > 0 Then
                    If ds.Tables(0).Rows(0).Item("COMPANYID").ToString() <> "" Then
                        CompanyName = ds.Tables(0).Rows(0).Item("COMPANYNAME").ToString()
                        lst.Text = ds.Tables(0).Rows(0).Item("COMPANYNAME").ToString()
                        lst.Value = ds.Tables(0).Rows(0).Item("COMPANYID").ToString()
                    Else
                        ComNotAvail = True
                    End If
                Else
                    DomNotAvail = True
                    If ds.Tables(0).Rows(0).Item("COMPANYID").ToString() <> "" Then
                        CompanyName = ds.Tables(0).Rows(0).Item("COMPANYNAME").ToString()
                        lst.Text = ds.Tables(0).Rows(0).Item("COMPANYNAME").ToString()
                        lst.Value = ds.Tables(0).Rows(0).Item("COMPANYID").ToString()
                    End If
                End If
            Else
                If ds.Tables(0).Rows(0).Item("COMPANYID").ToString() <> "" Then
                    CompanyName = ds.Tables(0).Rows(0).Item("COMPANYNAME").ToString()
                    lst.Text = ds.Tables(0).Rows(0).Item("COMPANYNAME").ToString()
                    lst.Value = ds.Tables(0).Rows(0).Item("COMPANYID").ToString()
                Else
                    ComNotAvail = True
                End If
            End If

            If ds.Tables(0).Rows.Count > 0 Then
                txtpreFixE.Text = ds.Tables(0).Rows(0).Item("PREFIX").ToString()
                txtFNameE.Text = ds.Tables(0).Rows(0).Item("FIRSTNAME").ToString()
                txtLNameE.Text = ds.Tables(0).Rows(0).Item("LASTNAME").ToString()
                txtphoneE.Text = ds.Tables(0).Rows(0).Item("PHONENUMBER").ToString()
                txtMobE.Text = ds.Tables(0).Rows(0).Item("MOBILENUMBER").ToString()
                txtFaxE.Text = ds.Tables(0).Rows(0).Item("FAXNUMBER").ToString()
                txtEmailE.Text = ds.Tables(0).Rows(0).Item("EMAILADDRESS").ToString()

                'Changes 17th_Nov
                If DomNotAvail = True Then
                    If CompanyName <> "" Then
                        ddlCompanyE.Items.Clear()
                        ddlCompanyE.AppendDataBoundItems = True
                        ddlCompanyE.Items.Add(lst)
                        ddlCompanyE.Items.Add("Other")
                    Else
                        ddlCompanyE.Visible = False
                        txtCompNameE.Visible = True
                        lblOtherCE.Visible = False
                    End If
                Else
                    If ComNotAvail = True Then
                        ddlCompanyE.Visible = False
                        txtCompNameE.Visible = True
                        lblOtherCE.Visible = False
                    Else
                        If CompanyName <> "" Then
                            ddlCompanyE.Items.Clear()
                            ddlCompanyE.AppendDataBoundItems = True
                            ddlCompanyE.Items.Add(lst)
                            ddlCompanyE.Items.Add("Other")
                        End If
                    End If
                End If
                'End

                txtposE.Text = ds.Tables(0).Rows(0).Item("JOBTITLE").ToString()
                txtStAddress1E.Text = ds.Tables(0).Rows(0).Item("STREETADDRESS1").ToString()
                txtStAddress2E.Text = ds.Tables(0).Rows(0).Item("STREETADDRESS2").ToString()
                txtCityE.Text = ds.Tables(0).Rows(0).Item("CITY").ToString()
                txtZipCodeE.Text = ds.Tables(0).Rows(0).Item("ZIPCODE").ToString()
                If ds.Tables(0).Rows(0).Item("COUNTRY").ToString() = "" Then
                    ddlCountryE.SelectedValue = 0
                Else
                    ddlCountryE.SelectedValue = ds.Tables(0).Rows(0).Item("COUNTRYID").ToString()
                End If

                '10th_jan_2017
                Dim CAvail As Integer = GetCountryInState(ds.Tables(0).Rows(0).Item("COUNTRYID").ToString())
                If CAvail <> 0 Then
                    BindState(ddlCountryE.SelectedValue)
                    ddlStateE.SelectedValue = GetStateId(ds.Tables(0).Rows(0).Item("STATE").ToString())
                    ddlStateE.Visible = True
                    txtStateE.Visible = False
                Else
                    ddlStateE.Visible = False
                    txtStateE.Visible = True
                    txtStateE.Text = ds.Tables(0).Rows(0).Item("STATE").ToString()
                End If
                'End

                'If ds.Tables(0).Rows(0).Item("COUNTRYID").ToString() = "175" Then
                '    ddlStateE.SelectedValue = GetStateId(ds.Tables(0).Rows(0).Item("STATE").ToString())
                '    ddlStateE.Visible = True
                '    txtStateE.Visible = False
                'Else
                '    ddlStateE.Visible = False
                '    txtStateE.Visible = True
                '    txtStateE.Text = ds.Tables(0).Rows(0).Item("STATE").ToString()
                'End If

                If ds.Tables(0).Rows(0).Item("ISPROMOMAIL").ToString() = "Y" Then
                    rYes.Checked = True
                Else
                    rNo.Checked = True
                End If

                If Session("UpdateeUserLbl") = 1 Then
                    lblMessage.Text = "User Updated Successfully."
                    Session("UpdateeUserLbl") = Nothing
                End If

            End If

        Catch ex As Exception
            lblError.Text = "Error:GetUserData:" + ex.Message.ToString()
        End Try
    End Sub

    Public Sub GetUserDataComp()
        Dim UserNm As String = String.Empty
        Dim DsDomain As New DataSet()
        Dim CompanyName As String = String.Empty
        Dim lstComp As New ListItem
        Dim DomNotAvail As Boolean = False
        Dim ComNotAvail As Boolean = False
        Try
            Dim objGetData As New UsersGetData.Selectdata()
            Dim ds As New DataSet()
            ds = objGetData.GetUDetails(UNAME)

            If ds.Tables(0).Rows.Count > 0 Then

                Dim words As String() = ds.Tables(0).Rows(0).Item("USERNAME").ToString().Split("@")
                UserNm = "@" + words(1)
                DsDomain = objGetData.GetDomainFlag(UserNm.ToString())

                '17Nov_2017
                If DsDomain.Tables(0).Rows.Count > 0 Then
                    If ds.Tables(0).Rows(0).Item("COMPANYID").ToString() <> "" Then
                        CompanyName = ds.Tables(0).Rows(0).Item("COMPANYNAME").ToString()
                        lstComp.Text = ds.Tables(0).Rows(0).Item("COMPANYNAME").ToString()
                        lstComp.Value = ds.Tables(0).Rows(0).Item("COMPANYID").ToString()
                    Else
                        ComNotAvail = True
                    End If
                Else
                    DomNotAvail = True
                    If ds.Tables(0).Rows(0).Item("COMPANYID").ToString() <> "" Then
                        CompanyName = ds.Tables(0).Rows(0).Item("COMPANYNAME").ToString()
                        lstComp.Text = ds.Tables(0).Rows(0).Item("COMPANYNAME").ToString()
                        lstComp.Value = ds.Tables(0).Rows(0).Item("COMPANYID").ToString()
                    End If
                End If
                'end

                'If ds.Tables(0).Rows(0).Item("COMPANYNAME").ToString() <> "" Then
                '    txtCompNameE.Enabled = False
                'Else
                '    txtCompNameE.Enabled = True
                'End If
                txtpreFixE.Text = ds.Tables(0).Rows(0).Item("PREFIX").ToString()
                txtFNameE.Text = ds.Tables(0).Rows(0).Item("FIRSTNAME").ToString()
                txtLNameE.Text = ds.Tables(0).Rows(0).Item("LASTNAME").ToString()
                txtphoneE.Text = ds.Tables(0).Rows(0).Item("PHONENUMBER").ToString()
                txtMobE.Text = ds.Tables(0).Rows(0).Item("MOBILENUMBER").ToString()
                txtFaxE.Text = ds.Tables(0).Rows(0).Item("FAXNUMBER").ToString()
                txtEmailE.Text = ds.Tables(0).Rows(0).Item("USERNAME").ToString()

                'Changes 17th_Nov
                If DomNotAvail = True Then
                    If CompanyName <> "" Then
                        ddlCompanyE.Items.Clear()
                        ddlCompanyE.AppendDataBoundItems = True
                        ddlCompanyE.Items.Add(lstComp)
                        ddlCompanyE.Items.Add("Other")
                    Else
                        ddlCompanyE.Visible = False
                        txtCompNameE.Visible = True
                        lblOtherCE.Visible = False
                    End If
                Else
                    If ComNotAvail = True Then
                        ddlCompanyE.Visible = False
                        txtCompNameE.Visible = True
                        lblOtherCE.Visible = False
                    Else
                        If CompanyName <> "" Then
                            ddlCompanyE.Items.Clear()
                            ddlCompanyE.AppendDataBoundItems = True
                            ddlCompanyE.Items.Add(lstComp)
                            ddlCompanyE.Items.Add("Other")
                        End If
                    End If
                End If
                'End

                'txtCompNameE.Text = ds.Tables(0).Rows(0).Item("COMPANYNAME").ToString()
                txtposE.Text = ds.Tables(0).Rows(0).Item("JOBTITLE").ToString()
                txtStAddress1E.Text = ds.Tables(0).Rows(0).Item("STREETADDRESS1").ToString()
                txtStAddress2E.Text = ds.Tables(0).Rows(0).Item("STREETADDRESS2").ToString()
                txtCityE.Text = ds.Tables(0).Rows(0).Item("CITY").ToString()
                txtZipCodeE.Text = ds.Tables(0).Rows(0).Item("ZIPCODE").ToString()
                If ds.Tables(0).Rows(0).Item("COUNTRY").ToString() = "" Then
                    ddlCountryE.SelectedValue = "175"
                Else
                    ddlCountryE.SelectedValue = ds.Tables(0).Rows(0).Item("COUNTRYID").ToString()
                End If

                '10th_jan_2017
                Dim CAvail As Integer = GetCountryInState(ddlCountryE.SelectedValue.ToString())
                If CAvail <> 0 Then
                    BindState(ddlCountryE.SelectedValue)
                    ddlStateE.SelectedValue = GetStateId(ds.Tables(0).Rows(0).Item("STATE").ToString())
                    ddlStateE.Visible = True
                    txtStateE.Visible = False
                Else
                    ddlStateE.Visible = False
                    txtStateE.Visible = True
                    txtStateE.Text = ds.Tables(0).Rows(0).Item("STATE").ToString()
                End If
                'End

                'If ddlCountryE.SelectedValue = "175" Then
                '    ddlStateE.SelectedValue = GetStateId(ds.Tables(0).Rows(0).Item("STATE").ToString())
                '    ddlStateE.Visible = True
                '    txtStateE.Visible = False
                'Else
                '    ddlStateE.Visible = False
                '    txtStateE.Visible = True
                '    txtStateE.Text = ds.Tables(0).Rows(0).Item("STATE").ToString()
                'End If

            End If

        Catch ex As Exception
            lblError.Text = "Error:GetUserData:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        'Chnages For UpdateUser
        Dim CompanyNM As String = String.Empty
        Dim DsCompany As New DataSet
        Dim CompanyID As String = String.Empty
        Dim UserNm As String = String.Empty
        Dim DsDomain As New DataSet()
        Dim DomainStatusID As String = String.Empty
        Dim IsApproved As String = String.Empty
        Dim EncEmail, EncUserId, EncType As String
        Dim link As String
        Dim privUSR As Boolean = False
        Dim dsUserC As New DataSet
        Dim obj As New CryptoHelper
        Dim strBodyData As String
        Dim VerfCode As String = Guid.NewGuid.ToString().Trim().Substring(0, 8)
        Dim EmailLink As String
        Dim RType As String = ""
        Dim objInsertData As New UsersUpdateData.UpdateInsert
        Dim DomainID As String = String.Empty
        Dim ComStatus As Boolean = False
        Dim NewCompany As Boolean = False
        Dim NewDomain As Boolean = False
        Dim dsCompanyNew As New DataSet
        'End
        '14th_Dec
        Dim FirstName As String = String.Empty
        Dim LastName As String = String.Empty
        Dim LicUserNm As String = String.Empty
        Dim DsLicenseChk As New DataSet()
        Dim objGetLoginData As New LoginGetData.Selectdata()
        Try
            Dim objGetData As New UsersGetData.Selectdata()
            Dim objUpdateData As New UsersUpdateData.UpdateInsert
            Dim ds, dsEmail As New DataSet()
            Dim promoMail, strBody As String

            dsEmail = objGetData.GetEmailConfigDetails("Y")
            If Mode = "Comp" Then
                'Company Changes #PR
                If ddlCompanyE.Items.Count <> "0" Then
                    If ddlCompanyE.SelectedItem.Text <> "Other" Then
                        CompanyID = ddlCompanyE.SelectedValue.ToString()
                        CompanyNM = ddlCompanyE.SelectedItem.ToString()
                    Else
                        DsCompany = objGetData.CheckCompanyExist(txtCompNameE.Text.Trim.ToString())
                        If DsCompany.Tables(0).Rows.Count > 0 Then
                            CompanyID = DsCompany.Tables(0).Rows(0).Item("COMPANYID").ToString()
                            CompanyNM = DsCompany.Tables(0).Rows(0).Item("COMPANYNAME").ToString()
                        Else
                            CompanyID = objGetData.GetComapnyID()
                            CompanyNM = txtCompNameE.Text.Trim.ToString()
                            NewCompany = True
                        End If
                    End If
                    ComStatus = True
                Else
                    DsCompany = objGetData.CheckCompanyExist(txtCompNameE.Text.Trim.ToString())
                    If DsCompany.Tables(0).Rows.Count > 0 Then
                        CompanyID = DsCompany.Tables(0).Rows(0).Item("COMPANYID").ToString()
                        CompanyNM = DsCompany.Tables(0).Rows(0).Item("COMPANYNAME").ToString()
                    Else
                        CompanyID = objGetData.GetComapnyID()
                        CompanyNM = txtCompNameE.Text.Trim.ToString()
                        NewCompany = True
                    End If
                    ComStatus = True
                End If
                'End

                '24_Nov_2017
                dsCompanyNew = objGetData.CheckNewCompany(CompanyID)
                If dsCompanyNew.Tables(0).Rows.Count > 0 Then
                    If dsCompanyNew.Tables(0).Rows(0).Item("ISNEW").ToString() = "Y" Then
                        NewCompany = True
                    End If
                End If
                'End

                dsUserC = objGetData.CheckUserExist(txtEmailE.Text)
                If dsUserC.Tables(0).Rows.Count > 0 Then
                    If dsUserC.Tables(0).Rows(0).Item("STATUSID").ToString() = "1" Then
                    ElseIf dsUserC.Tables(0).Rows(0).Item("STATUSID").ToString() = "2" Then
                        'Company Changes #PR
                        Dim words As String() = txtEmailE.Text.Split("@")
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
                            DomainID = DsDomain.Tables(0).Rows(0).Item("DOMAINID").ToString()
                        Else
                            DomainStatusID = 4
                            DomainID = objInsertData.UpdateURdomain(UserNm.ToString())
                            NewDomain = True
                        End If

                        If dsUserC.Tables(0).Rows(0).Item("ISVALIDEMAIL").ToString() = "Y" And dsUserC.Tables(0).Rows(0).Item("ISAPPROVED").ToString() = "Y" Then
                            privUSR = True
                        End If

                        'Updating details with password(Users already exist with inccomplete details)
                        '10th_jan_2017
                        Dim CAvail As Integer = GetCountryInState(ddlCountryE.SelectedValue.ToString())
                        If CAvail <> 0 Then
                            objUpdateData.UpdateINCOMPUserDetails(txtEmailE.Text.Trim(), txtPasswordE.Text.Replace("'", "''"), CompanyID.ToString(), txtpreFixE.Text.Replace("'", "''"), txtFNameE.Text.Replace("'", "''"), txtLNameE.Text.Replace("'", "''"), txtposE.Text.Replace("'", "''"), txtphoneE.Text.Replace("'", "''"), txtFaxE.Text.Replace("'", "''"), txtStAddress1E.Text.Replace("'", "''"), txtStAddress2E.Text.Replace("'", "''"), txtCityE.Text.Replace("'", "''"), ddlStateE.SelectedItem.Text, txtZipCodeE.Text.Replace("'", "''"), ddlCountryE.SelectedValue, VerfCode, CompanyNM.Replace("'", "''").ToString(), txtMobE.Text.Replace("'", "''"), DomainStatusID, privUSR, DomainID.ToString(), ComStatus, NewDomain)
                        Else
                            objUpdateData.UpdateINCOMPUserDetails(txtEmailE.Text.Trim(), txtPasswordE.Text.Replace("'", "''"), CompanyID.ToString(), txtpreFixE.Text.Replace("'", "''"), txtFNameE.Text.Replace("'", "''"), txtLNameE.Text.Replace("'", "''"), txtposE.Text.Replace("'", "''"), txtphoneE.Text.Replace("'", "''"), txtFaxE.Text.Replace("'", "''"), txtStAddress1E.Text.Replace("'", "''"), txtStAddress2E.Text.Replace("'", "''"), txtCityE.Text.Replace("'", "''"), txtStateE.Text.Replace("'", "''"), txtZipCodeE.Text.Replace("'", "''"), ddlCountryE.SelectedValue, VerfCode, CompanyNM.Replace("'", "''").ToString(), txtMobE.Text.Replace("'", "''"), DomainStatusID, privUSR, DomainID.ToString(), ComStatus, NewDomain)
                        End If

                        'If ddlCountryE.SelectedItem.Text = "United States" Then
                        '    objUpdateData.UpdateINCOMPUserDetails(txtEmailE.Text.Trim(), txtPasswordE.Text.Replace("'", "''"), CompanyID.ToString(), txtpreFixE.Text.Replace("'", "''"), txtFNameE.Text.Replace("'", "''"), txtLNameE.Text.Replace("'", "''"), txtposE.Text.Replace("'", "''"), txtphoneE.Text.Replace("'", "''"), txtFaxE.Text.Replace("'", "''"), txtStAddress1E.Text.Replace("'", "''"), txtStAddress2E.Text.Replace("'", "''"), txtCityE.Text.Replace("'", "''"), ddlStateE.SelectedItem.Text, txtZipCodeE.Text.Replace("'", "''"), ddlCountryE.SelectedValue, VerfCode, CompanyNM.Replace("'", "''").ToString(), txtMobE.Text.Replace("'", "''"), DomainStatusID, privUSR, DomainID.ToString(), ComStatus, NewDomain)
                        'Else
                        '    objUpdateData.UpdateINCOMPUserDetails(txtEmailE.Text.Trim(), txtPasswordE.Text.Replace("'", "''"), CompanyID.ToString(), txtpreFixE.Text.Replace("'", "''"), txtFNameE.Text.Replace("'", "''"), txtLNameE.Text.Replace("'", "''"), txtposE.Text.Replace("'", "''"), txtphoneE.Text.Replace("'", "''"), txtFaxE.Text.Replace("'", "''"), txtStAddress1E.Text.Replace("'", "''"), txtStAddress2E.Text.Replace("'", "''"), txtCityE.Text.Replace("'", "''"), txtStateE.Text.Replace("'", "''"), txtZipCodeE.Text.Replace("'", "''"), ddlCountryE.SelectedValue, VerfCode, CompanyNM.Replace("'", "''").ToString(), txtMobE.Text.Replace("'", "''"), DomainStatusID, privUSR, DomainID.ToString(), ComStatus, NewDomain)
                        'End If

                        ds = objGetData.GetUDetails(UNAME)
                        If privUSR = False Then
                            If dsEmail.Tables(0).Rows.Count > 0 Then
                                link = dsEmail.Tables(0).Rows(0).Item("URL").ToString()
                                If ds.Tables(0).Rows.Count > 0 Then
                                    'If DomainStatusID = 1 Then
                                    '    If NewCompany = True Or NewDomain = True Then
                                    '        strBodyData = String.Empty
                                    '        strBodyData = GetEmailBodyDataNotify(link, CompanyID, CompanyNM, NewCompany, DomainID, UserNm.ToString(), NewDomain, ds.Tables(0).Rows(0).Item("Name").ToString(), ds.Tables(0).Rows(0).Item("USERCONTACTID").ToString(), ds.Tables(0).Rows(0).Item("EMAILADDRESS").ToString(), "")
                                    '        SendEmailNotify(strBodyData, "ACTCOMPLCD")
                                    '    Else
                                    '        strBody = GetEmailBodyDataU(dsEmail.Tables(0).Rows(0).Item("URL").ToString(), ds.Tables(0).Rows(0).Item("Name").ToString(), ds.Tables(0).Rows(0).Item("USERCONTACTID").ToString(), ds.Tables(0).Rows(0).Item("EMAILADDRESS").ToString())
                                    '        SendEmailU(strBody)
                                    '    End If
                                    '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "ShowConfirmMessageU();", True)
                                    'Else
                                    '    EncEmail = obj.Encrypt(ds.Tables(0).Rows(0).Item("EMAILADDRESS").ToString()).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + ""
                                    '    EncUserId = obj.Encrypt(ds.Tables(0).Rows(0).Item("USERID").ToString()).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + ""
                                    '    EncType = obj.Encrypt(RType).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + ""
                                    '    EmailLink = link + "/Users_Login/EmailVarification.aspx?Email=" + EncEmail + "&UserID=" + EncUserId + "&Type=" + EncType + ""
                                    '    strBodyData = GetEmailBodyData(link, EmailLink, VerfCode)
                                    '    'Code for sending Email
                                    '    SendEmail(strBodyData, ds.Tables(0).Rows(0).Item("EMAILADDRESS").ToString(), txtFirstName.Text)
                                    '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "ShowPopWindow('VerificationU.aspx?UID=" + EncUserId + "');", True)
                                    'End If

                                    'Bug#PK20_1
                                    EncEmail = obj.Encrypt(ds.Tables(0).Rows(0).Item("EMAILADDRESS").ToString()).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + ""
                                    EncUserId = obj.Encrypt(ds.Tables(0).Rows(0).Item("USERID").ToString()).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + ""
                                    EncType = obj.Encrypt(RType).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + ""
                                    EmailLink = link + "/Users_Login/EmailVarification.aspx?Email=" + EncEmail + "&UserID=" + EncUserId + "&Type=" + EncType + ""
                                    strBodyData = GetEmailBodyData(link, EmailLink, VerfCode)
                                    'Code for sending Email
                                    SendEmail(strBodyData, ds.Tables(0).Rows(0).Item("EMAILADDRESS").ToString(), txtFirstName.Text)
                                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "ShowPopWindow('VerificationU.aspx?UID=" + EncUserId + "');", True)
                                    'Bug#PK20_1
                                End If
                            End If
                        Else
                            If dsEmail.Tables(0).Rows.Count > 0 Then
                                If ds.Tables(0).Rows.Count > 0 Then
                                    If NewCompany = True Or NewDomain = True Then
                                        strBodyData = String.Empty
                                        strBodyData = GetEmailBodyDataNotify(dsEmail.Tables(0).Rows(0).Item("URL").ToString(), CompanyID, CompanyNM, NewCompany, DomainID, UserNm.ToString(), NewDomain, ds.Tables(0).Rows(0).Item("Name").ToString(), ds.Tables(0).Rows(0).Item("USERCONTACTID").ToString(), ds.Tables(0).Rows(0).Item("EMAILADDRESS").ToString(), "")
                                        SendEmailNotify(strBodyData, "ACTCOMPLCD")
                                    Else
                                        'Sending email to Internal User
                                        strBody = GetEmailBodyDataU(dsEmail.Tables(0).Rows(0).Item("URL").ToString(), ds.Tables(0).Rows(0).Item("Name").ToString(), ds.Tables(0).Rows(0).Item("USERCONTACTID").ToString(), ds.Tables(0).Rows(0).Item("EMAILADDRESS").ToString())
                                        SendEmailU(strBody)
                                    End If
                                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "ShowConfirmMessageU();", True)
                                End If
                            End If
                        End If

                        'Issue_Pk_2020_2[22May_20]
                        DsLicenseChk = objGetLoginData.ValidateUSRByUser(ds.Tables(0).Rows(0).Item("USERNAME").ToString())
                        If DsLicenseChk.Tables(0).Rows.Count > 0 Then
                        Else
                            '14th_Dec_Assign_License
                            FirstName = txtFNameE.Text.Replace("'", "''").ToString().Substring(0, If(txtFNameE.Text.Replace("'", "''").ToString().Length >= 1, 1, txtFNameE.Text.Replace("'", "''").ToString().Length))
                            LastName = txtLNameE.Text.Replace("'", "''").ToString().Substring(0, If(txtLNameE.Text.Replace("'", "''").ToString().Length >= 1, 1, txtLNameE.Text.Replace("'", "''").ToString().Length))
                            LicUserNm = FirstName.ToLower() + LastName.ToLower()
                            objInsertData.AddLicenseToNewUser(LicUserNm, ds.Tables(0).Rows(0).Item("USERID").ToString())
                        End If
                        'Issue_Pk_2020_2[22May_20] 
                        
                    End If
                End If
            Else
                If rYes.Checked = True Then
                    promoMail = "Y"
                Else
                    promoMail = "N"
                End If

                'Company Changes #PR
                If ddlCompanyE.Items.Count <> "0" Then
                    If ddlCompanyE.SelectedItem.Text <> "Other" Then
                        CompanyID = ddlCompanyE.SelectedValue.ToString()
                        CompanyNM = ddlCompanyE.SelectedItem.ToString()
                    Else
                        DsCompany = objGetData.CheckCompanyExist(txtCompNameE.Text.Trim.ToString())
                        If DsCompany.Tables(0).Rows.Count > 0 Then
                            CompanyID = DsCompany.Tables(0).Rows(0).Item("COMPANYID").ToString()
                            CompanyNM = DsCompany.Tables(0).Rows(0).Item("COMPANYNAME").ToString()
                        Else
                            CompanyID = objGetData.GetComapnyID()
                            CompanyNM = txtCompNameE.Text.Trim.ToString()
                            NewCompany = True
                        End If
                    End If
                    ComStatus = True
                Else
                    DsCompany = objGetData.CheckCompanyExist(txtCompNameE.Text.Trim.ToString())
                    If DsCompany.Tables(0).Rows.Count > 0 Then
                        CompanyID = DsCompany.Tables(0).Rows(0).Item("COMPANYID").ToString()
                        CompanyNM = DsCompany.Tables(0).Rows(0).Item("COMPANYNAME").ToString()
                    Else
                        CompanyID = objGetData.GetComapnyID()
                        CompanyNM = txtCompNameE.Text.Trim.ToString()
                        NewCompany = True
                    End If
                    ComStatus = True
                End If
                'End

                'Changes Start 23_nov_2017
                ds = objGetData.GetUserDetails(Session("UserId").ToString())
                Dim words As String() = ds.Tables(0).Rows(0).Item("USERNAME").ToString().Split("@")
                If words.Length() = 2 Then
                    UserNm = "@" + words(1)
                    DsDomain = objGetData.GetDomainFlag(UserNm.ToString())

                    '17th_Nov_2017
                    If DsDomain.Tables(0).Rows.Count > 0 Then
                        DomainID = DsDomain.Tables(0).Rows(0).Item("DOMAINID").ToString()
                    Else
                        DomainID = objInsertData.UpdateURdomain(UserNm.ToString())
                        NewDomain = True
                    End If
                    'End
                Else
                    NewDomain = False
                    ComStatus = False
                End If
                'End

                dsEmail = objGetData.GetEmailConfigDetails("Y")
                'objUpdateData.UpdateUserDetails(Session("UserId").ToString(), txtEmailE.Text.Replace("'", "''"), txtCompNameE.Text.Replace("'", "''"), txtpreFixE.Text.Replace("'", "''"), txtFNameE.Text.Replace("'", "''"), txtLNameE.Text.Replace("'", "''"), txtposE.Text.Replace("'", "''"), txtphoneE.Text.Replace("'", "''"), txtFaxE.Text.Replace("'", "''"), txtStAddress1E.Text.Replace("'", "''"), txtStAddress2E.Text.Replace("'", "''"), txtCityE.Text.Replace("'", "''"), txtStateE.Text.Replace("'", "''"), txtZipCodeE.Text.Replace("'", "''"), txtCounE.Text.Replace("'", "''"), promoMail)
                Dim CAvail As Integer = GetCountryInState(ddlCountryE.SelectedValue.ToString())
                If CAvail <> 0 Then
                    objUpdateData.UpdateUserDetails(Session("UserId").ToString(), txtEmailE.Text.Replace("'", "''"), CompanyID, txtpreFixE.Text.Replace("'", "''"), txtFNameE.Text.Replace("'", "''"), txtLNameE.Text.Replace("'", "''"), txtposE.Text.Replace("'", "''"), txtphoneE.Text.Replace("'", "''"), txtFaxE.Text.Replace("'", "''"), txtStAddress1E.Text.Replace("'", "''"), txtStAddress2E.Text.Replace("'", "''"), txtCityE.Text.Replace("'", "''"), ddlStateE.SelectedItem.Text, txtZipCodeE.Text.Replace("'", "''"), ddlCountryE.SelectedValue, promoMail, txtMobE.Text.Replace("'", "''"), CompanyNM.Replace("'", "''").ToString(), DomainID.ToString(), ComStatus, NewDomain)
                Else
                    objUpdateData.UpdateUserDetails(Session("UserId").ToString(), txtEmailE.Text.Replace("'", "''"), CompanyID, txtpreFixE.Text.Replace("'", "''"), txtFNameE.Text.Replace("'", "''"), txtLNameE.Text.Replace("'", "''"), txtposE.Text.Replace("'", "''"), txtphoneE.Text.Replace("'", "''"), txtFaxE.Text.Replace("'", "''"), txtStAddress1E.Text.Replace("'", "''"), txtStAddress2E.Text.Replace("'", "''"), txtCityE.Text.Replace("'", "''"), txtStateE.Text.Replace("'", "''"), txtZipCodeE.Text.Replace("'", "''"), ddlCountryE.SelectedValue, promoMail, txtMobE.Text.Replace("'", "''"), CompanyNM.Replace("'", "''").ToString(), DomainID.ToString(), ComStatus, NewDomain)
                End If

                'If ddlCountryE.SelectedItem.Text = "United States" Then
                '    objUpdateData.UpdateUserDetails(Session("UserId").ToString(), txtEmailE.Text.Replace("'", "''"), CompanyID, txtpreFixE.Text.Replace("'", "''"), txtFNameE.Text.Replace("'", "''"), txtLNameE.Text.Replace("'", "''"), txtposE.Text.Replace("'", "''"), txtphoneE.Text.Replace("'", "''"), txtFaxE.Text.Replace("'", "''"), txtStAddress1E.Text.Replace("'", "''"), txtStAddress2E.Text.Replace("'", "''"), txtCityE.Text.Replace("'", "''"), ddlStateE.SelectedItem.Text, txtZipCodeE.Text.Replace("'", "''"), ddlCountryE.SelectedValue, promoMail, txtMobE.Text.Replace("'", "''"), CompanyNM.Replace("'", "''").ToString(), DomainID.ToString(), ComStatus, NewDomain)
                'Else
                '    objUpdateData.UpdateUserDetails(Session("UserId").ToString(), txtEmailE.Text.Replace("'", "''"), CompanyID, txtpreFixE.Text.Replace("'", "''"), txtFNameE.Text.Replace("'", "''"), txtLNameE.Text.Replace("'", "''"), txtposE.Text.Replace("'", "''"), txtphoneE.Text.Replace("'", "''"), txtFaxE.Text.Replace("'", "''"), txtStAddress1E.Text.Replace("'", "''"), txtStAddress2E.Text.Replace("'", "''"), txtCityE.Text.Replace("'", "''"), txtStateE.Text.Replace("'", "''"), txtZipCodeE.Text.Replace("'", "''"), ddlCountryE.SelectedValue, promoMail, txtMobE.Text.Replace("'", "''"), CompanyNM.Replace("'", "''").ToString(), DomainID.ToString(), ComStatus, NewDomain)
                'End If

                If NewCompany = True Or NewDomain = True Then
                    If ds.Tables(0).Rows(0).Item("ISPROMOMAIL").ToString() = promoMail Then
                        strBodyData = String.Empty
                        strBodyData = GetEmailBodyDataNotifyEdit(dsEmail.Tables(0).Rows(0).Item("URL").ToString(), CompanyID, CompanyNM, NewCompany, DomainID, UserNm.ToString(), NewDomain, ds.Tables(0).Rows(0).Item("Name").ToString(), ds.Tables(0).Rows(0).Item("USERCONTACTID").ToString(), ds.Tables(0).Rows(0).Item("EMAILADDRESS").ToString(), "S")
                        SendEmailNotify(strBodyData, "NEWADDITIONS")
                    Else
                        strBodyData = String.Empty
                        strBodyData = GetEmailBodyDataNotifyEdit(dsEmail.Tables(0).Rows(0).Item("URL").ToString(), CompanyID, CompanyNM, NewCompany, DomainID, UserNm.ToString(), NewDomain, ds.Tables(0).Rows(0).Item("Name").ToString(), ds.Tables(0).Rows(0).Item("USERCONTACTID").ToString(), ds.Tables(0).Rows(0).Item("EMAILADDRESS").ToString(), promoMail)
                        SendEmailNotify(strBodyData, "NEWADDITIONS")
                    End If
                Else
                    If ds.Tables(0).Rows(0).Item("ISPROMOMAIL").ToString() = promoMail Then
                        strBody = GetEmailBodyData(dsEmail.Tables(0).Rows(0).Item("URL").ToString(), ds.Tables(0).Rows(0).Item("Name").ToString(), ds.Tables(0).Rows(0).Item("USERCONTACTID").ToString(), ds.Tables(0).Rows(0).Item("EMAILADDRESS").ToString(), "S")
                        SendEmail(strBody)
                    Else
                        strBody = GetEmailBodyData(dsEmail.Tables(0).Rows(0).Item("URL").ToString(), ds.Tables(0).Rows(0).Item("Name").ToString(), ds.Tables(0).Rows(0).Item("USERCONTACTID").ToString(), ds.Tables(0).Rows(0).Item("EMAILADDRESS").ToString(), promoMail)
                        SendEmail(strBody)
                    End If
                End If

                lblMessage.Text = "User Updated Successfully."
                Session("UpdateeUserLbl") = 1
                Response.Redirect(HttpContext.Current.Request.Url.ToString(), True)
            End If

        Catch ex As Exception
            lblError.Text = "Error:btnUpdate_Click:" + ex.Message.ToString()
        End Try
    End Sub

#End Region

    Protected Sub lnkForgotPass_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkForgotPass.Click
        Try
            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Win3", "OpenNewWindow('ForgotPassword.aspx','Win2');", True)
            Response.Redirect("~/Users_Login/ForgotPassword.aspx")
        Catch ex As Exception
            lblError.Text = "Error:lnkForgotPass_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub ddlCountry_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCountry.SelectedIndexChanged
        Try
            Dim password As String = ""
            Dim cpassword As String = ""
            password = txtPassword.Text
            cpassword = txtConfirmPassword.Text

            '10th_jan_2017
            Dim CAvail As Integer = GetCountryInState(ddlCountry.SelectedValue.ToString())
            If CAvail <> 0 Then
                ddlState.Visible = True
                txtState.Visible = False
                BindState(ddlCountry.SelectedValue.ToString())
            Else
                ddlState.Visible = False
                txtState.Visible = True
                If ddlCountry.SelectedValue = "0" Then
                    txtState.Enabled = False
                Else
                    txtState.Enabled = True
                End If
            End If
            'end


            'If ddlCountry.SelectedItem.Text = "United States" Then
            '    ddlState.Visible = True
            '    txtState.Visible = False
            '    BindState("0")
            'Else
            '    ddlState.Visible = False
            '    txtState.Visible = True
            '    If ddlCountry.SelectedValue = "0" Then
            '        txtState.Enabled = False
            '    Else
            '        txtState.Enabled = True
            '    End If
            'End If
            txtPassword.Attributes.Add("value", password)
            txtConfirmPassword.Attributes.Add("value", cpassword)
        Catch ex As Exception
            lblError.Text = "Error:ddlCountry_SelectedIndexChanged:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub ddlCountryE_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCountryE.SelectedIndexChanged
        Try
            Dim password As String = ""
            Dim cpassword As String = ""
            password = txtPasswordE.Text
            cpassword = txtConfirmPasswordE.Text

            '10th_jan_2017
            Dim CAvail As Integer = GetCountryInState(ddlCountryE.SelectedValue.ToString())
            If CAvail <> 0 Then
                ddlStateE.Visible = True
                txtStateE.Visible = False
                BindState(ddlCountryE.SelectedValue.ToString())
            Else
                ddlStateE.Visible = False
                txtStateE.Visible = True
                If ddlCountryE.SelectedValue = "0" Then
                    txtStateE.Enabled = False
                Else
                    txtStateE.Enabled = True
                End If
            End If
            'end

            'If ddlCountryE.SelectedItem.Text = "United States" Then
            '    ddlStateE.Visible = True
            '    txtStateE.Visible = False
            '    BindState("0")
            'Else
            '    ddlStateE.Visible = False
            '    txtStateE.Visible = True
            '    If ddlCountryE.SelectedValue = "0" Then
            '        txtStateE.Enabled = False
            '    Else
            '        txtStateE.Enabled = True
            '    End If
            'End If
            txtPasswordE.Attributes.Add("value", password)
            txtConfirmPasswordE.Attributes.Add("value", cpassword)
        Catch ex As Exception
            lblError.Text = "Error:ddlCountryE_SelectedIndexChanged:" + ex.Message.ToString()
        End Try
    End Sub

#Region "Changes for Company"

    Protected Sub txtEmail_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEmail.TextChanged
        Dim objGetData As New UsersGetData.Selectdata()
        Dim UserNm As String = String.Empty
        Dim DsDomain As New DataSet()
        Try
            If txtEmail.Text.Trim.Length <> 0 Then

                Dim UserCheck As Integer
                UserCheck = objGetData.GetAUserCount(txtEmail.Text.Trim.ToString(), "1")
                If txtEmail.Text.Trim.Length <> 0 Then
                    'Start changes 23_Nov_2017
                    Dim pattern As String = "^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"
                    Dim emailAddressMatch As Match = Regex.Match(txtEmail.Text, pattern)
                    If emailAddressMatch.Success Then
                        trUsername.Visible = True
                        If UserCheck <> 0 Then
                            lblUserAv.Text = "The UserName <b>" + txtEmail.Text.Trim.ToString() + " </b> is already exist."
                            lblUserAv.CssClass = "NotAvailable"
                            btnReg.Enabled = False
                            txtEmail.Focus()
                        Else
                            lblUserAv.Text = "The UserName <b>" + txtEmail.Text.Trim.ToString() + " </b> is  available."
                            lblUserAv.CssClass = "Available"
                            btnReg.Enabled = True
                            txtCEmail.Focus()
                        End If
                    Else
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Email Address is not in proper format.');", True)
                        trUsername.Visible = False
                    End If
                Else
                    trUsername.Visible = False
                End If

                Dim words As String() = txtEmail.Text.Split("@")
                If words.Length() = 2 Then
                    UserNm = "@" + words(1)
                    DsDomain = objGetData.GetDomainFlag(UserNm.ToString())

                    If DsDomain.Tables(0).Rows.Count > 0 Then
                        If DsDomain.Tables(0).Rows(0).Item("ACCEPTANCYFLAG") = 3 Or DsDomain.Tables(0).Rows(0).Item("ACCEPTANCYFLAG") = 4 Then
                            ddlCompany.Items.Clear()
                            ddlCompany.Visible = False
                            txtCompanyName.Visible = True
                            lblOtherC.Visible = False
                            txtCEmail.Focus()
                        Else
                            'btnReg.Enabled = True
                            ddlCompany.Items.Clear()
                            txtCompanyName.Visible = False
                            lblOtherC.Visible = False
                            ddlCompany.Visible = True
                            BindCompany(DsDomain.Tables(0).Rows(0).Item("DOMAINID"))
                            txtCEmail.Focus()
                        End If
                    Else
                        ddlCompany.Items.Clear()
                        ddlCompany.Visible = False
                        lblOtherC.Visible = False
                        txtCompanyName.Visible = True
                        txtCEmail.Focus()
                    End If
                End If
                'End 23_nov_2017
            Else
                ddlCompany.Items.Clear()
                ddlCompany.Visible = False
                txtCompanyName.Visible = True
                lblOtherC.Visible = False
                trUsername.Visible = False
            End If

        Catch ex As Exception
            lblError.Text = "Error:txtEmail_TextChanged:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub BindCompany(ByVal DomainID As String)
        Dim objGetData As New UsersGetData.Selectdata()
        Dim ds As New DataSet
        Try
            ds = objGetData.GetCompanyList(DomainID)
            If ds.Tables(0).Rows.Count > 0 Then
                With ddlCompany
                    .DataValueField = "COMPANYID"
                    .DataTextField = "COMPANYNAME"
                    .DataSource = ds
                    .DataBind()
                    ddlCompany.AppendDataBoundItems = True
                    ddlCompany.Items.Add("Other")
                End With
            Else
                ddlCompany.Visible = False
                txtCompanyName.Visible = True
                lblOtherC.Visible = False
            End If

        Catch ex As Exception
            lblError.Text = "Error:BindCompany:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub ddlCompany_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCompany.SelectedIndexChanged
        Try
            Dim password As String = ""
            Dim cpassword As String = ""
            password = txtPassword.Text
            cpassword = txtConfirmPassword.Text
            If ddlCompany.SelectedItem.Text = "Other" Then
                txtCompanyName.Visible = True
                lblOtherC.Visible = True
            Else
                txtCompanyName.Visible = False
                lblOtherC.Visible = False
            End If
            txtPassword.Attributes.Add("value", password)
            txtConfirmPassword.Attributes.Add("value", cpassword)
        Catch ex As Exception
            lblError.Text = "Error:ddlCompany_SelectedIndexChanged:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnUp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUp.Click
        Dim RType As String = ""
        Dim objGetData As New UsersGetData.Selectdata
        Dim objInsertData As New UsersUpdateData.UpdateInsert
        Dim ds As New DataSet
        Dim dsUserData As New DataSet
        Dim EmailLink, link As String
        Dim obj As New CryptoHelper
        Dim strBodyData As String
        Dim VerfCode As String = Guid.NewGuid.ToString().Trim().Substring(0, 8)
        Dim dsUserC As New DataSet
        Dim EncEmail, EncUserId, EncType As String
        Dim dsEmail, dsU As New DataSet
        Dim strBody As String
        'Company Changes #PR
        Dim CompanyNM As String = String.Empty
        Dim DsCompany As New DataSet
        Dim CompanyID As String
        Dim ComStatus As Boolean = False
        'End
        '14th_Dec
        Dim FirstName As String = String.Empty
        Dim LastName As String = String.Empty
        Dim LicUserNm As String = String.Empty
        'End

        Try
            If Type = "" Then
                RType = "NA"
            Else
                If Type = "WCNF" Then
                    'Remaining
                    RType = "~/WebConferenceN/UserDetails.aspx?ID=" & Session("FConFid") & ""
                End If
            End If

            'Company Changes #PR
            If ddlCompany.Items.Count <> "0" Then
                If ddlCompany.SelectedItem.Text <> "Other" Then
                    CompanyID = ddlCompany.SelectedValue.ToString()
                    CompanyNM = ddlCompany.SelectedItem.ToString()
                Else
                    DsCompany = objGetData.CheckCompanyExist(txtCompanyName.Text.Trim.ToString())
                    If DsCompany.Tables(0).Rows.Count > 0 Then
                        CompanyID = DsCompany.Tables(0).Rows(0).Item("COMPANYID").ToString()
                        CompanyNM = DsCompany.Tables(0).Rows(0).Item("COMPANYNAME").ToString()
                    Else
                        CompanyID = objGetData.GetComapnyID()
                        CompanyNM = txtCompanyName.Text.Trim.ToString()
                    End If
                    ComStatus = True
                End If
            Else
                DsCompany = objGetData.CheckCompanyExist(txtCompanyName.Text.Trim.ToString())
                If DsCompany.Tables(0).Rows.Count > 0 Then
                    CompanyID = DsCompany.Tables(0).Rows(0).Item("COMPANYID").ToString()
                    CompanyNM = DsCompany.Tables(0).Rows(0).Item("COMPANYNAME").ToString()
                Else
                    CompanyID = objGetData.GetComapnyID()
                    CompanyNM = txtCompanyName.Text.Trim.ToString()
                End If
                ComStatus = True
            End If
            'End

            'Check for Existing User
            dsUserC = objGetData.CheckUserExist(txtEmail.Text)
            dsEmail = objGetData.GetEmailConfigDetails("Y")
            If dsUserC.Tables(0).Rows.Count > 0 Then
                If dsUserC.Tables(0).Rows(0).Item("STATUSID").ToString() = "1" Then
                    'Users already exist with complete details
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Win3", "MessageWindow();", True)
                    lnkForgotPass.Style.Add("display", "inline")
                ElseIf dsUserC.Tables(0).Rows(0).Item("STATUSID").ToString() = "2" Then
                    'Updating details with password(Users already exist with inccomplete details)
                    hidStatusId.Value = "2"
                    If ddlCountry.SelectedItem.Text = "United States" Then
                        'objInsertData.UpdateINCOMPUserDetails(txtEmail.Text.Trim(), hidPass.Value.Replace("'", "''"), CompanyID.ToString(), txtPrefix.Text.Replace("'", "''"), txtFirstName.Text.Replace("'", "''"), txtLastName.Text.Replace("'", "''"), txtPosition.Text.Replace("'", "''"), txtphone.Text.Replace("'", "''"), txtFax.Text.Replace("'", "''"), txtSAdress1.Text.Replace("'", "''"), txtSAdress2.Text.Replace("'", "''"), txtCity.Text.Replace("'", "''"), ddlState.SelectedItem.Text, txtZipCode.Text.Replace("'", "''"), ddlCountry.SelectedValue, VerfCode, CompanyNM.Replace("'", "''").ToString(), txtMob.Text.Replace("'", "''"))
                    Else
                        'objInsertData.UpdateINCOMPUserDetails(txtEmail.Text.Trim(), hidPass.Value.Replace("'", "''"), CompanyID.ToString(), txtPrefix.Text.Replace("'", "''"), txtFirstName.Text.Replace("'", "''"), txtLastName.Text.Replace("'", "''"), txtPosition.Text.Replace("'", "''"), txtphone.Text.Replace("'", "''"), txtFax.Text.Replace("'", "''"), txtSAdress1.Text.Replace("'", "''"), txtSAdress2.Text.Replace("'", "''"), txtCity.Text.Replace("'", "''"), txtState.Text.Replace("'", "''"), txtZipCode.Text.Replace("'", "''"), ddlCountry.SelectedValue, VerfCode, CompanyNM.Replace("'", "''").ToString(), txtMob.Text.Replace("'", "''"))
                    End If
                    'Sending email to Internal User
                    dsU = objGetData.GetUDetails(txtEmail.Text)
                    strBody = GetEmailBodyDataU(dsEmail.Tables(0).Rows(0).Item("URL").ToString(), dsU.Tables(0).Rows(0).Item("Name").ToString(), dsU.Tables(0).Rows(0).Item("USERCONTACTID").ToString(), dsU.Tables(0).Rows(0).Item("EMAILADDRESS").ToString())
                    SendEmailU(strBody)
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "ShowConfirmMessage();", True)
                End If
            Else
                'Company Changes #PR           

                'Inserting User Details
                '10th_jan_2017
                Dim CAvail As Integer = GetCountryInState(ddlCountry.SelectedValue.ToString())
                If CAvail <> 0 Then
                    objInsertData.InsertUserDetails(txtEmail.Text.Trim(), hidPass.Value.Replace("'", "''"), CompanyID.ToString(), txtPrefix.Text.Replace("'", "''"), txtFirstName.Text.Replace("'", "''"), txtLastName.Text.Replace("'", "''"), txtPosition.Text.Replace("'", "''"), txtphone.Text.Replace("'", "''"), txtFax.Text.Replace("'", "''"), txtSAdress1.Text.Replace("'", "''"), txtSAdress2.Text.Replace("'", "''"), txtCity.Text.Replace("'", "''"), ddlState.SelectedItem.Text, txtZipCode.Text.Replace("'", "''"), ddlCountry.SelectedValue, VerfCode, CompanyNM.Replace("'", "''").ToString(), 3, "", txtMob.Text.Replace("'", "''"), hidDomID3.Value, ComStatus)
                Else
                    objInsertData.InsertUserDetails(txtEmail.Text.Trim(), hidPass.Value.Replace("'", "''"), CompanyID.ToString(), txtPrefix.Text.Replace("'", "''"), txtFirstName.Text.Replace("'", "''"), txtLastName.Text.Replace("'", "''"), txtPosition.Text.Replace("'", "''"), txtphone.Text.Replace("'", "''"), txtFax.Text.Replace("'", "''"), txtSAdress1.Text.Replace("'", "''"), txtSAdress2.Text.Replace("'", "''"), txtCity.Text.Replace("'", "''"), txtState.Text.Replace("'", "''"), txtZipCode.Text.Replace("'", "''"), ddlCountry.SelectedValue, VerfCode, CompanyNM.Replace("'", "''").ToString(), 3, "", txtMob.Text.Replace("'", "''"), hidDomID3.Value, ComStatus)
                End If
                'If ddlCountry.SelectedItem.Text = "United States" Then
                '    objInsertData.InsertUserDetails(txtEmail.Text.Trim(), hidPass.Value.Replace("'", "''"), CompanyID.ToString(), txtPrefix.Text.Replace("'", "''"), txtFirstName.Text.Replace("'", "''"), txtLastName.Text.Replace("'", "''"), txtPosition.Text.Replace("'", "''"), txtphone.Text.Replace("'", "''"), txtFax.Text.Replace("'", "''"), txtSAdress1.Text.Replace("'", "''"), txtSAdress2.Text.Replace("'", "''"), txtCity.Text.Replace("'", "''"), ddlState.SelectedItem.Text, txtZipCode.Text.Replace("'", "''"), ddlCountry.SelectedValue, VerfCode, CompanyNM.Replace("'", "''").ToString(), 3, "", txtMob.Text.Replace("'", "''"), hidDomID3.Value, ComStatus)
                'Else
                '    objInsertData.InsertUserDetails(txtEmail.Text.Trim(), hidPass.Value.Replace("'", "''"), CompanyID.ToString(), txtPrefix.Text.Replace("'", "''"), txtFirstName.Text.Replace("'", "''"), txtLastName.Text.Replace("'", "''"), txtPosition.Text.Replace("'", "''"), txtphone.Text.Replace("'", "''"), txtFax.Text.Replace("'", "''"), txtSAdress1.Text.Replace("'", "''"), txtSAdress2.Text.Replace("'", "''"), txtCity.Text.Replace("'", "''"), txtState.Text.Replace("'", "''"), txtZipCode.Text.Replace("'", "''"), ddlCountry.SelectedValue, VerfCode, CompanyNM.Replace("'", "''").ToString(), 3, "", txtMob.Text.Replace("'", "''"), hidDomID3.Value, ComStatus)
                'End If

                '#Region "Email Verification Code"
                'Get Email Config Details
                ds = objGetData.GetEmailConfigDetails("Y")
                dsUserData = objGetData.GetUserId(txtEmail.Text)
                If ds.Tables(0).Rows.Count > 0 Then
                    link = ds.Tables(0).Rows(0).Item("URL").ToString()
                    If dsUserData.Tables(0).Rows.Count > 0 Then
                        EncEmail = obj.Encrypt(dsUserData.Tables(0).Rows(0).Item("EMAILADD").ToString()).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + ""
                        EncUserId = obj.Encrypt(dsUserData.Tables(0).Rows(0).Item("UserId").ToString()).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + ""
                        EncType = obj.Encrypt(RType).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + ""
                        EmailLink = link + "/Users_Login/EmailVarification.aspx?Email=" + EncEmail + "&UserID=" + EncUserId + "&Type=" + EncType + ""
                        strBodyData = GetEmailBodyData(link, EmailLink, VerfCode)
                        'Code for sending Email
                        SendEmail(strBodyData, dsUserData.Tables(0).Rows(0).Item("EMAILADD").ToString(), txtFirstName.Text)
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "ShowPopWindow('Verification.aspx?UID=" + EncUserId + "&Type=Acc');", True)

                        '14th_Dec_Assign_License
                        FirstName = txtFirstName.Text.Replace("'", "''").ToString().Substring(0, If(txtFirstName.Text.Replace("'", "''").ToString().Length >= 1, 1, txtFirstName.Text.Replace("'", "''").ToString().Length))
                        LastName = txtLastName.Text.Replace("'", "''").ToString().Substring(0, If(txtLastName.Text.Replace("'", "''").ToString().Length >= 1, 1, txtLastName.Text.Replace("'", "''").ToString().Length))
                        LicUserNm = FirstName.ToLower() + LastName.ToLower()
                        objInsertData.AddLicenseToNewUser(LicUserNm, dsUserData.Tables(0).Rows(0).Item("UserId").ToString())

                    End If
                End If
                '#End Region 
                'End Company Changes #PR
            End If
        Catch ex As Exception
            lblError.Text = "Error:btnUp_Click:" + ex.Message.ToString()
        End Try
    End Sub

#End Region

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "UserLogin", "window.close();", True)
            'Response.Redirect("~/Index.aspx")
        Catch ex As Exception
            lblError.Text = "Error:btnCancel_Click:" + ex.Message.ToString()
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

    Protected Sub btnCloseU_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCloseU.Click
        Try
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "UserLogin", "CloseWindowU();", True)
        Catch ex As Exception
            lblError.Text = "Error:btnCloseU_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnCloseAcpU_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCloseAcpU.Click
        Try
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "UserLogin", "CloseWindowAcpU();", True)
        Catch ex As Exception
            lblError.Text = "Error:btnCloseAcpU_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnCloseUA_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCloseUA.Click
        Try
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "UserLogin", "CloseWindowUA();", True)
        Catch ex As Exception
            lblError.Text = "Error:btnCloseAcpU_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btncancelE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btncancelE.Click
        Try
            Response.Redirect("~/Index.aspx")
        Catch ex As Exception
            lblError.Text = "Error:btncancelE_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btncancelU_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btncancelU.Click
        Try
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "UserLogin", "window.close();", True)
        Catch ex As Exception
            lblError.Text = "Error:btncancelU_Click:" + ex.Message.ToString()
        End Try
    End Sub

#Region "AddEditAccount 30th_oct_2017"

    Public Sub GetNewUserInfo()
        Dim objGetData As New UsersGetData.Selectdata()
        Dim UserNm As String = String.Empty
        Dim DsDomain As New DataSet()
        Dim Ds As New DataSet()
        Dim obj As New CryptoHelper
        Try
            txtEmail.Text = UNAME
            'txtEmail.Enabled = False

            If txtEmail.Text.Trim.Length <> 0 Then

                Dim words As String() = txtEmail.Text.Split("@")
                If words.Length() = 2 Then
                    UserNm = "@" + words(1)
                    DsDomain = objGetData.GetDomainFlag(UserNm.ToString())

                    If DsDomain.Tables(0).Rows.Count > 0 Then
                        If DsDomain.Tables(0).Rows(0).Item("ACCEPTANCYFLAG") = 3 Or DsDomain.Tables(0).Rows(0).Item("ACCEPTANCYFLAG") = 4 Then
                            ddlCompany.Items.Clear()
                            ddlCompany.Visible = False
                            txtCompanyName.Visible = True
                            lblOtherC.Visible = False
                            txtCEmail.Focus()
                        Else
                            'btnReg.Enabled = True
                            ddlCompany.Items.Clear()
                            txtCompanyName.Visible = False
                            lblOtherC.Visible = False
                            ddlCompany.Visible = True
                            BindCompany(DsDomain.Tables(0).Rows(0).Item("DOMAINID"))
                            txtCEmail.Focus()
                        End If
                    Else
                        ddlCompany.Items.Clear()
                        ddlCompany.Visible = False
                        lblOtherC.Visible = False
                        txtCompanyName.Visible = True
                        txtCEmail.Focus()
                    End If
                End If
            Else
                ddlCompany.Items.Clear()
                ddlCompany.Visible = False
                txtCompanyName.Visible = True
                lblOtherC.Visible = False
                trUsername.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = "Error:GetNewUserInfo:" + ex.Message.ToString()
        End Try
    End Sub

#End Region

    Protected Sub BindCompanyE(ByVal DomainID As String)
        Dim objGetData As New UsersGetData.Selectdata()
        Dim ds As New DataSet
        Try
            ds = objGetData.GetCompanyList(DomainID)
            If ds.Tables(0).Rows.Count > 0 Then
                With ddlCompanyE
                    .DataValueField = "COMPANYID"
                    .DataTextField = "COMPANYNAME"
                    .DataSource = ds
                    .DataBind()
                    ddlCompanyE.AppendDataBoundItems = True
                    ddlCompanyE.Items.Add("Other")
                End With
            Else
                ddlCompanyE.Visible = False
                txtCompNameE.Visible = True
                lblOtherCE.Visible = False
            End If

        Catch ex As Exception
            lblError.Text = "Error:BindCompanyE:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub ddlCompanyE_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCompanyE.SelectedIndexChanged
        Try
            Dim password As String = ""
            Dim cpassword As String = ""
            password = txtPasswordE.Text
            cpassword = txtConfirmPasswordE.Text

            If ddlCompanyE.SelectedItem.Text = "Other" Then
                txtCompNameE.Visible = True
                lblOtherCE.Visible = True
            Else
                txtCompNameE.Visible = False
                lblOtherCE.Visible = False
            End If
            txtPasswordE.Attributes.Add("value", password)
            txtConfirmPasswordE.Attributes.Add("value", cpassword)
        Catch ex As Exception
            lblError.Text = "Error:ddlCompanyE_SelectedIndexChanged:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub ChangePassword()
        Dim DsVer, DsLic, DsSecLvl, dsUId As New DataSet()
        Dim objGetData As New UsersGetData.Selectdata()
        Dim objGetDataValid As New LoginGetData.Selectdata()
        Dim Days As Integer
        Dim obj As New CryptoHelper
        Try
            DsVer = objGetData.GetUserDetails(Session("UserId").ToString())
            Days = DateDiff(DateInterval.Day, CDate(DsVer.Tables(0).Rows(0).Item("VUPDATEDATE")), Date.Now)
            DsLic = objGetDataValid.ValidateUSRWOLICENSE(DsVer.Tables(0).Rows(0).Item("USERNAME").ToString())
            If DsLic.Tables(0).Rows.Count > 0 Then
                DsSecLvl = objGetDataValid.GetSecurityDetails(DsLic.Tables(0).Rows(0).Item("SECURITYLVL").ToString())
                dsUId = objGetDataValid.GetUID()
                If DsSecLvl.Tables(0).Rows(0).Item("SECLEVEL").ToString() <> "5" Then
                    If Days > 0 Then
                        Response.Redirect("../Users_Login/ChangePassword.aspx", True)
                    Else
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('You can not change your password now because you changed it recently.');", True)
                    End If
                Else
                    Response.Redirect("../Users_Login/ChangePassword.aspx", True)
                End If
            Else
                Response.Redirect("ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
            End If
        Catch ex As Exception

        End Try
    End Sub

    '10th_Jan_2018
    Protected Function GetCountryInState(ByVal CountryID As String) As Integer
        Dim objGetData As New UsersGetData.Selectdata
        Dim ds As New DataSet
        Try
            ds = objGetData.CheckCountryInState(CountryID)
            If (ds.Tables(0).Rows.Count > 0) Then
                Return (1)
            Else
                Return (0)
            End If
        Catch ex As Exception
            Return (0)
        End Try

    End Function
    'End 10th_Jan_2018

#Region "Sending email"

    Protected Function GetEmailBodyDataU(ByVal link As String, ByVal Name As String, ByVal USERCONTACTID As String, ByVal EMAILADDRESS As String) As String
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

    Protected Function GetEmailBodyData_OLD(ByVal link As String, ByVal EmailLink As String, ByVal VerfCode As String) As String
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
            StrSql = StrSql & "<p>This verification code is valid for 10 minutes, so please enter it immediately.</p> "
            StrSql = StrSql & "<p>Thank you for initiating a new account with SavvyPack Corporation.</p> "
            StrSql = StrSql & "<br /> "
            StrSql = StrSql & "<p> "
            StrSql = StrSql & "<div style='font-family:Verdana;font-size:12px;'> "
            StrSql = StrSql & "<b> The Support Team<br/>"
            StrSql = StrSql & "<br /> "
            StrSql = StrSql + "SavvyPack Corporation<br/>1850 East 121st Street Suite 106B<br/>Burnsville, MN USA 55337<br/><a style='font-family:verdana;' href='http://www.savvypack.com'>www.savvypack.com</a><br/>Phone: 1-952-405-7500</div> "
            StrSql = StrSql & "</p> "
            StrSql = StrSql & "</div> "
            Return StrSql
        Catch ex As Exception
            Return StrSql
        End Try
    End Function

    Public Sub SendEmail(ByVal strBody As String)
        Try
            Dim objGetData As New UsersGetData.Selectdata
            Dim ds As New DataSet
            ds = objGetData.GetAlliedMemberMail("VERFPEMAIL")
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

    Public Sub SendEmailU(ByVal strBody As String)
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

    Protected Function GetEmailBodyData(ByVal link As String, ByVal Name As String, ByVal USERCONTACTID As String, ByVal EMAILADDRESS As String, ByVal flag As String) As String
        Dim StrSqlBdy As String = ""
        Dim strAction As String = ""
        Try
            If flag = "Y" Then
                strAction = "Please Add this user in Email List"
            ElseIf flag = "N" Then
                strAction = "Please Delete this user from Email List"
            Else
                strAction = "This user has edited his details sucessfully."
            End If
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

    Protected Function GetEmailBodyDataNotify(ByVal link As String, ByVal CompanyID As String, ByVal CompanyNm As String, ByVal NewCom As Boolean, _
                                             ByVal DomainID As String, ByVal DomainNm As String, ByVal NewDom As Boolean, _
                                             ByVal Name As String, ByVal USERCONTACTID As String, ByVal EMAILADDRESS As String, ByVal flag As String) As String
        Dim StrSqlBdy As String = ""
        Dim strAction As String = ""
        Dim strActionCom As String = String.Empty
        Dim strActionDom As String = String.Empty
        Try
            If flag = "Y" Then
                strAction = "Please Add this user in Email List"
            Else
                strAction = "This user has updated his details sucessfully."
            End If
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


            If NewCom = True Then
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

            If NewDom = True Then
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
            lblError.Text = "Error:GetEmailBodyDataNotify:" + ex.Message.ToString()
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
            lblError.Text = "Error:SendEmailNotify:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Function GetEmailBodyDataNotifyEdit(ByVal link As String, ByVal CompanyID As String, ByVal CompanyNm As String, ByVal NewCom As Boolean, _
                                            ByVal DomainID As String, ByVal DomainNm As String, ByVal NewDom As Boolean, _
                                            ByVal Name As String, ByVal USERCONTACTID As String, ByVal EMAILADDRESS As String, ByVal flag As String) As String
        Dim StrSqlBdy As String = ""
        Dim strAction As String = ""
        Dim strActionCom As String = String.Empty
        Dim strActionDom As String = String.Empty
        Try
            If flag = "Y" Then
                strAction = "Please Add this user in Email List"
            ElseIf flag = "N" Then
                strAction = "Please Delete this user from Email List"
            Else
                strAction = "This user has edited his details sucessfully."
            End If
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


            If NewCom = True Then
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

            If NewDom = True Then
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
            lblError.Text = "Error:GetEmailBodyDataNotifyEdit:" + ex.Message.ToString()
            Return StrSqlBdy
        End Try
    End Function

#End Region
End Class
