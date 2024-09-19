Imports PackProdGetData.Selectdata
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO.StringWriter
Partial Class Pages_PackProd_Search
    Inherits System.Web.UI.Page
    Dim objGetdata As New PackProdGetData.Selectdata
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
        MainHeading.InnerHtml = "Packaging Producer - Search Manager"
    End Sub

    Protected Sub GetContentPlaceHolder()
        ctlContentPlaceHolder = Page.Master.FindControl("PackProdContentPlaceHolder")
    End Sub



#End Region

#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_PAGES_PackProd_LOGICSEARCH")
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
        Catch ex As Exception
            ErrorLable.Text = "Error:Page_Load:" + ex.Message.ToString() + ""
        End Try
    End Sub
    Protected Sub bindCategory()
        Try
            Dim Dts, ds, dtsCategory As New DataSet

            ds = objGetdata.GetSelectionDataByUser(Session("UserName").ToString())
            'Binding Company
            Dts = objGetdata.GetCompanyData("", Session("UserId"))
            With ddlCompany
                .DataSource = Dts
                .DataTextField = "NAME"
                .DataValueField = "ID"
                .DataBind()
            End With

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

        Catch ex As Exception
            ErrorLable.Text = "Error:bindCategory:" + ex.Message.ToString() + ""
        End Try
    End Sub
    Protected Sub bindSelectionDetails()
        Try
            Dim Dts As New DataSet
            Dts = objGetdata.GetSelectionDataByUser(Session("UserName").ToString())
            If Dts.Tables(0).Rows.Count > 0 Then
                ddlCompany.SelectedValue = Dts.Tables(0).Rows(0).Item("company").ToString()
                ddlState.SelectedValue = Dts.Tables(0).Rows(0).Item("state").ToString()
                ddlProductType.SelectedValue = Dts.Tables(0).Rows(0).Item("product").ToString()
                ddlProdService.SelectedValue = Dts.Tables(0).Rows(0).Item("service").ToString()
                ddlProdDevCap.SelectedValue = Dts.Tables(0).Rows(0).Item("design").ToString()
                ddlProcCap.SelectedValue = Dts.Tables(0).Rows(0).Item("process").ToString()
                ddlMacSys.SelectedValue = Dts.Tables(0).Rows(0).Item("machine").ToString()
                ddlRepCus.SelectedValue = Dts.Tables(0).Rows(0).Item("customer").ToString()
                ddlCountry.SelectedValue = Dts.Tables(0).Rows(0).Item("country").ToString()
            End If

        Catch ex As Exception
            ErrorLable.Text = "Error:bindSelectionDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnLogSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogSearch.Click
        Try
            Dim objUpdate As New PackProdUpInsData.UpdateInsert()
            Dim URL As String = String.Empty
            Dim State As String = String.Empty
            If ddlState.SelectedValue = "" Then
                State = "0"
            Else
                State = ddlState.SelectedValue
            End If
            'Insert in to Selection Table
            objUpdate.UpdateSelectionData(ddlCompany.SelectedValue, ddlProductType.SelectedValue, ddlCountry.SelectedValue, State, ddlProdService.SelectedValue, ddlProdDevCap.SelectedValue, ddlProcCap.SelectedValue, ddlMacSys.SelectedValue, ddlRepCus.SelectedValue, Session("UserName").ToString())
            'Updating Reults
            objUpdate.UpdateResultsData(ddlCompany.SelectedValue, ddlProductType.SelectedValue, ddlCountry.SelectedValue, State, ddlProdService.SelectedValue, ddlProdDevCap.SelectedValue, ddlProcCap.SelectedValue, ddlMacSys.SelectedValue, ddlRepCus.SelectedValue, Session("UserName").ToString(), Session("UserId").ToString())
            Session("PSearchState") = ddlState.SelectedItem.Text
            RedirectToPage("LogicSearch.aspx", "LogicSearch")
            'Response.Redirect("LogicSearch.aspx")
        Catch ex As Exception
            ErrorLable.Text = "Error:btnLogSearch_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim objUpIns As New PackProdUpInsData.UpdateInsert()
        Dim objCryptoHelper As New CryptoHelper()
        Dim Keyword As String = String.Empty
        Try
            Keyword = objCryptoHelper.Encrypt(txtSearch.Text.ToString())
            objUpIns.KeywordSearch(Session("UserName").ToString(), txtSearch.Text.ToString(), Session("UserId"))
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

        Catch ex As Exception
            ErrorLable.Text = "Error:ddlCountry_SelectedIndexChanged:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnState_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnState.Click
        GetStates()
    End Sub

    Protected Sub GetStates()
        Try
            Dim Dts As New DataSet
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
