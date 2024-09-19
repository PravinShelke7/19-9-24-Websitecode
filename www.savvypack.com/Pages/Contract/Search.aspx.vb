Imports ContrGetData.Selectdata
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO.StringWriter
Partial Class Pages_Contract_Search
    Inherits System.Web.UI.Page
    Dim objGetdata As New ContrGetData.Selectdata
#Region "Get Set Variables"
    Dim _lErrorLble As Label
    Dim _divMainHeading As HtmlGenericControl
    Dim _ctlContentPlaceHolder As ContentPlaceHolder
    Dim _btnLogOff As ImageButton


    Public Property ErrorLable() As Label
        Get
            Return _lErrorLble
        End Get
        Set(ByVal Value As Label)
            _lErrorLble = Value
        End Set
    End Property

    Public Property LogOffbtn() As ImageButton
        Get
            Return _btnLogOff
        End Get
        Set(ByVal value As ImageButton)
            _btnLogOff = value
        End Set
    End Property

    Public Property MainHeading() As HtmlGenericControl
        Get
            Return _divMainHeading
        End Get
        Set(ByVal value As HtmlGenericControl)
            _divMainHeading = value
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




    Public DataCnt As Integer
    Public CaseDesp As New ArrayList
#End Region

#Region "MastePage Content Variables"

    Protected Sub GetMasterPageControls()
        GetContentPlaceHolder()
        GetLogOffbtn()
        GetErrorLable()
        GetMainHeadingdiv()
    End Sub

    Protected Sub GetErrorLable()
        ErrorLable = Page.Master.FindControl("lblError")
    End Sub

    Protected Sub GetLogOffbtn()
        LogOffbtn = Page.Master.FindControl("imgLogoff")
        LogOffbtn.Visible = True
    End Sub

    Protected Sub GetMainHeadingdiv()
        MainHeading = Page.Master.FindControl("divMainHeading")
        MainHeading.InnerHtml = "Contract - Search Manager"
    End Sub

    Protected Sub GetContentPlaceHolder()
        ctlContentPlaceHolder = Page.Master.FindControl("ContractContentPlaceHolder")
    End Sub



#End Region

#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_PAGES_CONTRACT_LOGICSEARCH")
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        objRefresh.Render(Page)
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            GetMasterPageControls()
            If Not IsPostBack Then
                bindCategory()
                bindSelectionDetails()
            End If
            txtSearch.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnSearch.ClientID + "')")
        Catch ex As Exception
            ErrorLable.Text = "Error:Page_Load:" + ex.Message.ToString() + ""
        End Try
    End Sub
    Protected Sub bindCategory()
        Try
            Dim Dts, ds, dtsCategory As New DataSet
			Dim dsDetail As New DataSet
			
            ds = objGetdata.GetSelectionDataByUser(Session("USERID").ToString())
          

            'Binding Category for Product Area
            dtsCategory = objGetdata.GetCategoryByDetails("PRODAREA")
            With ddlProductType
                .DataSource = dtsCategory
                .DataTextField = "DATA"
                .DataValueField = "ID"
                .DataBind()
            End With

            'Binding Category for Product Development Capabilities
            dtsCategory = objGetdata.GetCategoryByDetails("PRODDEVCAP")
            With ddlProdDevCap
                .DataSource = dtsCategory
                .DataTextField = "DATA"
                .DataValueField = "ID"
                .DataBind()
            End With

            'Binding Category for Processing Capabilities
            dtsCategory = objGetdata.GetCategoryByDetails("PROCCAP")
            With ddlProcCap
                .DataSource = dtsCategory
                .DataTextField = "DATA"
                .DataValueField = "ID"
                .DataBind()
            End With

            'Binding Category for Machinery Systems
            dtsCategory = objGetdata.GetCategoryByDetails("PACKMACHSYS")
            With ddlMacSys
                .DataSource = dtsCategory
                .DataTextField = "DATA"
                .DataValueField = "ID"
                .DataBind()
            End With


            'Binding Category for Representative Customers
            dtsCategory = objGetdata.GetCategoryByDetails("REPCUST")
            With ddlRepCus
                .DataSource = dtsCategory
                .DataTextField = "DATA"
                .DataValueField = "ID"
                .DataBind()
            End With

            'Binding Category for Contry
            dtsCategory = objGetdata.GetCountryDetails("", Session("UserId"))
            With ddlCountry
                .DataSource = dtsCategory
                .DataTextField = "NAME"
                .DataValueField = "COUNTRYID"
                .DataBind()
            End With


            'Binding Category for State


            dtsCategory = objGetdata.GetStateDetails("", ds.Tables(0).Rows(0).Item("Country").ToString())
            If dtsCategory.Tables(0).Rows.Count > 1 Then
                Dim lst As New ListItem
                lst.Text = "All"
                lst.Value = "-1"
                ddlState.Items.Add(lst)
                ddlState.AppendDataBoundItems = True
            End If
            With ddlState
                .DataSource = dtsCategory
                .DataTextField = "NAME"
                .DataValueField = "STATEID"
                .DataBind()
            End With

            'Binding Category for Product Services
            dtsCategory = objGetdata.GetCategoryByDetails("PRODSER")
            With ddlProdService
                .DataSource = dtsCategory
                .DataTextField = "DATA"
                .DataValueField = "ID"
                .DataBind()
            End With
			
			 dsDetail = objGetdata.GetUserInforMation(Session("UserId"))
            If dsDetail.Tables(0).Rows.Count > 0 Then
                If dsDetail.Tables(0).Rows(0).Item("ISIADMINLICUSR").ToString() = "Y" Then
                    ManageUser.Visible = True
                Else
                    ManageUser.Visible = False
                End If

            End If
        Catch ex As Exception
            ErrorLable.Text = "Error:bindCategory:" + ex.Message.ToString() + ""
        End Try
    End Sub
    Protected Sub bindSelectionDetails()
        Try
            Dim Dts, DtsN As New DataSet
            Dts = objGetdata.GetSelectionDataByUser(Session("USERID").ToString())
            If Dts.Tables(0).Rows.Count > 0 Then

                ddlState.SelectedValue = Dts.Tables(0).Rows(0).Item("state").ToString()
                ddlProductType.SelectedValue = Dts.Tables(0).Rows(0).Item("product").ToString()
                ddlProdService.SelectedValue = Dts.Tables(0).Rows(0).Item("service").ToString()
                ddlProdDevCap.SelectedValue = Dts.Tables(0).Rows(0).Item("design").ToString()
                ddlProcCap.SelectedValue = Dts.Tables(0).Rows(0).Item("process").ToString()
                ddlMacSys.SelectedValue = Dts.Tables(0).Rows(0).Item("machine").ToString()
                ddlRepCus.SelectedValue = Dts.Tables(0).Rows(0).Item("customer").ToString()
                ddlCountry.SelectedValue = Dts.Tables(0).Rows(0).Item("country").ToString()
                GetCompanies()
                ddlCompany.SelectedValue = Dts.Tables(0).Rows(0).Item("company").ToString()
               
            End If

        Catch ex As Exception
            ErrorLable.Text = "Error:bindSelectionDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnLogSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogSearch.Click
        Try
            Dim objUpdate As New ContrUpInsData.UpdateInsert()
            Dim URL As String = String.Empty
            Dim State As String = String.Empty
            If ddlState.SelectedValue = "" Then
                State = "0"
            Else
                State = ddlState.SelectedValue
            End If
            'Insert in to Selection Table
            objUpdate.UpdateSelectionData(ddlCompany.SelectedValue, ddlProductType.SelectedValue, ddlCountry.SelectedValue, State, ddlProdService.SelectedValue, ddlProdDevCap.SelectedValue, ddlProcCap.SelectedValue, ddlMacSys.SelectedValue, ddlRepCus.SelectedValue, Session("USERID").ToString())
            'Updating Reults
            objUpdate.UpdateResultsData(ddlCompany.SelectedValue, ddlProductType.SelectedValue, ddlCountry.SelectedValue, State, ddlProdService.SelectedValue, ddlProdDevCap.SelectedValue, ddlProcCap.SelectedValue, ddlMacSys.SelectedValue, ddlRepCus.SelectedValue, Session("USERID").ToString())
            Session("SearchState") = ddlState.SelectedItem.Text
            RedirectToPage("LogicSearch.aspx", "LogicSearch")
            'Response.Redirect("LogicSearch.aspx")
        Catch ex As Exception
            ErrorLable.Text = "Error:btnLogSearch_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim objUpIns As New ContrUpInsData.UpdateInsert()
        Dim objCryptoHelper As New CryptoHelper()
        Dim Keyword As String = String.Empty
        Try
            Keyword = objCryptoHelper.Encrypt(txtSearch.Text.ToString())
            objUpIns.KeywordSearch(txtSearch.Text.ToString(), Session("USERID"))
            RedirectToPage("KeywordDetails.aspx?Keyword=" + Keyword + "", "KeywordSearch")
        Catch ex As Exception
            ErrorLable.Text = "Error:btnSearch_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub RedirectToPage(ByVal URL As String, ByVal Type As String)
        Try
            If Not objRefresh.IsRefresh Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), Type, "OpenNewWindow('" + URL + "','" + Type + "');", True)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlCountry_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCountry.SelectedIndexChanged
        Try
            GetStates()
            GetCompanies()
        Catch ex As Exception
            ErrorLable.Text = "Error:ddlCountry_SelectedIndexChanged:" + ex.Message.ToString() + ""
        End Try
    End Sub
    Protected Sub GetCompanies()
        Dim DtsN As New DataSet()
        Try
            'Binding Company
            DtsN = objGetdata.GetCompanyData("", Session("UserId"), ddlCountry.SelectedValue)
            With ddlCompany
                .DataSource = DtsN
                .DataTextField = "NAME"
                .DataValueField = "ID"
                .DataBind()
            End With
            ddlCompany.SelectedValue = "0"
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub btnState_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnState.Click
        GetStates()
        GetCompanies()
    End Sub

    Protected Sub GetStates()
        Try
            Dim Dts, dtsCategory As New DataSet
            ddlState.Items.Clear()
            Dts = objGetdata.GetStateDetails("", ddlCountry.SelectedValue)
         
            With ddlState
                .DataSource = Dts
                .DataTextField = "NAME"
                .DataValueField = "STATEID"
                .DataBind()
            End With
            If Dts.Tables(0).Rows.Count > 1 Then
                Dim lst As New ListItem
                lst.Text = "All"
                lst.Value = "-1"
                ddlState.Items.Add(lst)
                ddlState.AppendDataBoundItems = True
            End If
        Catch ex As Exception
            ErrorLable.Text = "Error:GetStates:" + ex.Message.ToString() + ""
        End Try
    End Sub
End Class
