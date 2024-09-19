Imports System.Data
Imports System.Data.OleDb
Imports System
Imports ShopGetData
Imports WebConf
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Partial Class WebConferenceN_UserDetails
    Inherits System.Web.UI.Page

#Region "Get Set Variables"
    Dim _lErrorLble As Label
    Dim _ctlContentPlaceHolder As ContentPlaceHolder
    Dim _strId As String
    Dim _hypMWebConf As HyperLink
    Dim _hypSWebConf As HyperLink



    Public Property ErrorLable() As Label
        Get
            Return _lErrorLble
        End Get
        Set(ByVal Value As Label)
            _lErrorLble = Value
        End Set
    End Property

    Public Property MWebConf() As HyperLink
        Get
            Return _hypMWebConf
        End Get
        Set(ByVal Value As HyperLink)
            _hypMWebConf = Value
        End Set
    End Property

    Public Property SWebConf() As HyperLink
        Get
            Return _hypSWebConf
        End Get
        Set(ByVal Value As HyperLink)
            _hypSWebConf = Value
        End Set
    End Property

    Public Property WebId() As String
        Get
            Return _strId
        End Get
        Set(ByVal Value As String)
            _strId = Value
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
        GetErrorLable()
        GetContentPlaceHolder()
        'GetMWebConfLink()
        'GetSWebConfLink()

    End Sub

    Protected Sub GetErrorLable()
        ErrorLable = Page.Master.FindControl("lblError")
    End Sub


    Protected Sub GetContentPlaceHolder()
        ctlContentPlaceHolder = Page.Master.FindControl("Econ1ContentPlaceHolder")
    End Sub

    Protected Sub GetMWebConfLink()
        MWebConf = Page.Master.FindControl("hypWebConf")
        MWebConf.CssClass = "LinkMasterMenuClick"
    End Sub

    Protected Sub GetSWebConfLink()
        SWebConf = Page.Master.FindControl("hypSWebConf")
        SWebConf.CssClass = "LinkSubMenuClick"
    End Sub


#End Region

#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_WEBCONFERENCEN_USERDETAILS")
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        objRefresh.Render(Page)
    End Sub

#End Region


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            GetMasterPageControls()
            Dim ObjCrypto As New CryptoHelper()
            Try
                WebId = ObjCrypto.Decrypt(Request.QueryString("ID"))
            Catch ex As Exception
                WebId = Request.QueryString("ID")
            End Try


            If Session("UserId") = Nothing Then
                'Session("URL") = "../WebConferenceN/UserDetails.aspx?Id=" + Request.QueryString("ID") + ""
                'Response.Redirect("../Users_Login/Login.aspx?Type=WCNF")
                Dim obj As New CryptoHelper
                Response.Redirect("~/Universal_loginN/Pages/ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            Else
                Session("URL") = ""
            End If
            GetPageDetails()
        Catch ex As Exception
            ErrorLable.Text = "Error:Page_Load" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetPageDetails()
        Dim objGetData As New UsersGetData.Selectdata()
        Dim objGetWebData As New WebConf.Selectdata()
        Dim dsWeb As New DataSet()
        Dim ds As New DataSet()
        Dim objCrypto As New CryptoHelper()
        'Session("UserId") = "1621"
        Try
            hypUser.NavigateUrl = "~/Users_Login/AddEditUser.aspx?Mode=" + objCrypto.Encrypt("Edit") + ""
            dsWeb = objGetWebData.GetWebConfDetailsById(WebId)
            ds = objGetData.GetUserDetails(Session("UserId"))

            If ds.Tables(0).Rows.Count > 0 Then
                lblName.Text = ds.Tables(0).Rows(0).Item("PREFIX").ToString() + " " + ds.Tables(0).Rows(0).Item("FIRSTNAME").ToString() + " " + ds.Tables(0).Rows(0).Item("LASTNAME").ToString() + " "
                lblEmail.Text = ds.Tables(0).Rows(0).Item("EMAILADDRESS").ToString()
                lblphne.Text = ds.Tables(0).Rows(0).Item("PHONENUMBER").ToString()
                lblFax.Text = ds.Tables(0).Rows(0).Item("FAXNUMBER").ToString()
                lblCompName.Text = ds.Tables(0).Rows(0).Item("COMPANYNAME").ToString()
                lblPosition.Text = ds.Tables(0).Rows(0).Item("JOBTITLE").ToString()
                lblAdd.Text = ds.Tables(0).Rows(0).Item("STREETADDRESS1").ToString() + " " + ds.Tables(0).Rows(0).Item("STREETADDRESS2").ToString()
                lblCity.Text = ds.Tables(0).Rows(0).Item("CITY").ToString()
                lblState.Text = ds.Tables(0).Rows(0).Item("STATE").ToString()
                lblZipCode.Text = ds.Tables(0).Rows(0).Item("ZIPCODE").ToString()
                lblCntry.Text = ds.Tables(0).Rows(0).Item("COUNTRY").ToString()
            End If

              lblAmt.Text = dsWeb.Tables(0).Rows(0).Item("CONFCOST").ToString()
            lblTopic.Text = dsWeb.Tables(0).Rows(0).Item("CONFTOPIC").ToString()


        Catch ex As Exception
            ErrorLable.Text = "Error:GetPageDetails" + ex.Message.ToString()
        End Try
    End Sub


    Protected Sub btnOrder_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOrder.Click
        Dim objUpIns As New WebConUpIns.UpdateInsert()
        Try
            objUpIns.InsertWebConfAttn(Session("RefNumber"), WebId, Session("UserID"))
            Response.Redirect("WebConfAtte.aspx", False)

        Catch ex As Exception
            ErrorLable.Text = "Error:btnOrder_Click" + ex.Message.ToString()
        End Try
    End Sub
End Class
