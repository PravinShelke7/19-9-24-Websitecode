Imports System.Data
Imports System.Data.OleDb
Imports System
Imports WebConf
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class WebConference_WebConf
    Inherits System.Web.UI.Page

#Region "Get Set Variables"
    Dim _lErrorLble As Label
    Dim _ctlContentPlaceHolder As ContentPlaceHolder
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
        GetErrorLable()
        'GetMWebConfLink()
        'GetSWebConfLink()
    End Sub

    Protected Sub GetErrorLable()
        ErrorLable = Page.Master.FindControl("lblError")
    End Sub

    Protected Sub GetMWebConfLink()
        MWebConf = Page.Master.FindControl("hypWebConf")
        MWebConf.CssClass = "LinkMasterMenuClick"
    End Sub

    Protected Sub GetSWebConfLink()
        SWebConf = Page.Master.FindControl("hypSWebConf")
        SWebConf.CssClass = "LinkSubMenuClick"
    End Sub


    Protected Sub GetContentPlaceHolder()
        ctlContentPlaceHolder = Page.Master.FindControl("Econ1ContentPlaceHolder")
    End Sub

#End Region

#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh



    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_WEBCONFERENCE_WEBCONF")
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        objRefresh.Render(Page)
    End Sub

#End Region


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
           
            GetMasterPageControls()
            GetPageDetails()
             Session("MenuItem") = "RWEBCONF"
             If Session("UserId") <> Nothing Then
                hdnUserId.Value = Session("UserId")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GetPageDetails()
        Dim objGetData As New Selectdata()
        Dim ds As New DataSet()
        Dim objCrypto As New CryptoHelper()

        Dim DsOrderRev As New DataSet()
        Dim ObjWGetData As New ShopGetData.Selectdata()


        Try
            ds = objGetData.GetWebConfDetails()

            rptWebConf.DataSource = ds
            rptWebConf.DataBind()

            DsOrderRev = ObjWGetData.GetOrderForRef(Session("WRefNumber").ToString())
            If DsOrderRev.Tables(0).Rows.Count = 0 Then
                Session("WRefNumber") = ObjWGetData.GetRefNumber()
            End If


            For Each DataItem As RepeaterItem In rptWebConf.Items
                Dim hyp As New LinkButton
                If DataItem.ItemType = ListItemType.Item Or DataItem.ItemType = ListItemType.AlternatingItem Then
                    hyp = DataItem.FindControl("hyp")
                    hyp.CommandName = ds.Tables(0).Rows(DataItem.ItemIndex).Item("WEBCONFERENCEID").ToString()
                    hyp.ValidationGroup = ds.Tables(0).Rows(DataItem.ItemIndex).Item("CONFCOST").ToString()
                    AddHandler hyp.Click, AddressOf Register_Click
                End If
            Next

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Register_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lnk As New LinkButton()
        Dim objCrypto As New CryptoHelper()
        Dim objCal As New Calulate_ShopItem()
        Dim link As String = String.Empty
        Dim obj As New CryptoHelper
        Try
			
            lnk = sender
			Session("FConFid") = lnk.CommandName
			Session("FConType") = lnk.ValidationGroup
			If Not objRefresh.IsRefresh Then
                link = "ShoppingCart.aspx"
                If lnk.ValidationGroup <> "Free" Then
                    objCal.Calculate_Web(lnk.CommandName.ToString(), Session("WRefNumber"))
                    Dim objGetData As New UsersGetData.Selectdata()
                    Dim ds As New DataSet()
                    ds = objGetData.GetEmailConfigDetails("Y")
                    link = ds.Tables(0).Rows(0).Item("HTTPSURL").ToString()
                    link = link.Replace("/ShoppingCart/", "/WebConferenceN/")
					Session("link") = link
				Else
                    Session("link") = link
                End If

                If Session("USERID") = "" Then					
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "OpenLoginPopup('WB');", True)
                Else
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "window.open('WebUserInformation.aspx?Link=" + obj.Encrypt(link) + "');", True)
                End If
                ' Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "window.open('WebUserInformation.aspx?Link=" + obj.Encrypt(link) + "');", True)
                'Response.Redirect(link, False)
            End If
        Catch ex As Exception

        End Try



    End Sub
	
	Protected Sub btnUser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUser.Click
        Try
            Dim obj As New CryptoHelper
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "window.open('WebUserInformation.aspx?Link=" + obj.Encrypt(Session("link")) + "');", True)
        Catch ex As Exception

        End Try
    End Sub
End Class
