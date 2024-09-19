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
Partial Class WebConferenceN_WebUserInformation
    Inherits System.Web.UI.Page
    Dim _strLink As String

    Public Property Link() As String
        Get
            Return _strLink
        End Get
        Set(ByVal value As String)
            Dim obj As New CryptoHelper
            _strLink = obj.Decrypt(value)
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
        Session("MenuItem") = "RWEBCONF"
        GetErrorLable()
        GetContentPlaceHolder()

    End Sub

    Protected Sub GetErrorLable()
        ErrorLable = Page.Master.FindControl("lblError")
    End Sub


    Protected Sub GetContentPlaceHolder()
        ctlContentPlaceHolder = Page.Master.FindControl("ContentPlaceHolder1")
    End Sub

#End Region

#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_WEBCONFERENCEN_WEBUSERINFORMATION")
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        objRefresh.Render(Page)
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "UserLogin", "Verify('Y');", True)
            End If
            Link = Request.QueryString("Link").ToString().Replace("!Plus!", "+").Replace("!Hash!", "#").Replace("!And!", "&")
            lblTitleE.Text = "Account Infromation"
            If Session("Back") = Nothing Then
                If Session("SBack") = Nothing Then
                    Dim obj As New CryptoHelper
                    Response.Redirect("~/Universal_loginN/Pages/ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "", False)
                End If
            End If

            GetMasterPageControls()
            SetFocusOnSubmit()

            If Not IsPostBack Then
                BindCountry()
                BindState(ddlCountryE.SelectedValue)
                GetUserInfo()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub SetFocusOnSubmit()
        Try
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
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub BindCountry()
        Dim objGetData As New UsersGetData.Selectdata()
        Dim ds As New DataSet
        Try
            ds = objGetData.GetCountry()
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
            'Edit User
            ddlStateE.Items.Clear()
            list = New ListItem
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

    Public Sub GetUserInfo()
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

                'txtCompNameE.Text = ds.Tables(0).Rows(0).Item("COMPANYNAME").ToString()
                txtposE.Text = ds.Tables(0).Rows(0).Item("JOBTITLE").ToString()
                txtStAddress1E.Text = ds.Tables(0).Rows(0).Item("STREETADDRESS1").ToString()
                txtStAddress2E.Text = ds.Tables(0).Rows(0).Item("STREETADDRESS2").ToString()
                txtCityE.Text = ds.Tables(0).Rows(0).Item("CITY").ToString()
                'txtStateE.Text = ds.Tables(0).Rows(0).Item("STATE").ToString()
                txtZipCodeE.Text = ds.Tables(0).Rows(0).Item("ZIPCODE").ToString()
                'txtCounE.Text = ds.Tables(0).Rows(0).Item("COUNTRY").ToString()
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
            End If

        Catch ex As Exception
            lblError.Text = "Error:GetUserInfo:" + ex.Message.ToString()
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

    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Dim objGetData As New UsersGetData.Selectdata()
        Dim objUpdateData As New UsersUpdateData.UpdateInsert
        Dim ds, dsEmail As New DataSet()
        Dim promoMail As String
        Dim DomainID As String = String.Empty
        Dim ComStatus As Boolean = False
        Dim NewCompany As Boolean = False
        Dim NewDomain As Boolean = False
        Dim dsCompanyNew As New DataSet
        Dim CompanyNM As String = String.Empty
        Dim DsCompany As New DataSet
        Dim CompanyID As String = String.Empty
        Dim UserNm As String = String.Empty
        Dim DsDomain As New DataSet()
        Dim DomainStatusID As String = String.Empty
        Dim IsApproved As String = String.Empty
        Dim privUSR As Boolean = False
        Dim dsUserC As New DataSet
        Dim obj As New CryptoHelper
        Dim strBodyData, strBody As String

        Dim objInsertData As New UsersUpdateData.UpdateInsert
        Try
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

            Response.Redirect(Link, False)
        Catch ex As Exception
            lblError.Text = "Error:btnUpdate_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub ddlCompanyE_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCompanyE.SelectedIndexChanged
        Try
            If ddlCompanyE.SelectedItem.Text = "Other" Then
                txtCompNameE.Visible = True
                lblOtherCE.Visible = True
            Else
                txtCompNameE.Visible = False
                lblOtherCE.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = "Error:ddlCompanyE_SelectedIndexChanged:" + ex.Message.ToString()
        End Try
    End Sub
	
	 Protected Sub ddlCountryE_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCountryE.SelectedIndexChanged
        Try
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
            'End

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
        Catch ex As Exception
            lblError.Text = "Error:ddlCountryE_SelectedIndexChanged:" + ex.Message.ToString()
        End Try
    End Sub

#Region "Send Mail"

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

#End Region

    Protected Sub btncancelE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btncancelE.Click
        Try
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "UserLogin", "window.close();", True)
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

End Class
