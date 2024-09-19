Imports System.Data
Imports System.Data.OleDb
Imports System
Imports ShopGetData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Imports CryptoHelper
Partial Class ShoppingCart_Order
    Inherits System.Web.UI.Page
#Region "Get Set Variables"
    Dim _lErrorLble As Label
    Dim _ctlContentPlaceHolder As ContentPlaceHolder
    Dim _strId As String



    Public Property ErrorLable() As Label
        Get
            Return _lErrorLble
        End Get
        Set(ByVal Value As Label)
            _lErrorLble = Value
        End Set
    End Property

    Public Property OrderId() As String
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
        Session("MenuItem") = "SHOPPING"
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
        objRefresh = New zCon.Net.Refresh("_SHOPPINGCART_ORDER")
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        objRefresh.Render(Page)
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
        
            GetMasterPageControls()
            Dim ObjCrypto As New CryptoHelper()
            OrderId = ObjCrypto.Decrypt(Request.QueryString("ID"))

			 If Session("Back") = Nothing And Session("SBack") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("~/Universal_loginN/Pages/ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "", False)
            End If
			
            If Session("UserId") = Nothing Then
                
                Response.Redirect("~/Universal_loginN/Pages/ErrorU.aspx?ErrorCode=" + ObjCrypto.Encrypt("ALDE112") + "", False)
            Else
                Session("URL") = ""
            End If
            GetPageDetails()
        Catch ex As Exception
            ErrorLable.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetPageDetails()
        Dim objGetData As New Selectdata()
        Dim ds As New DataSet()
        Dim DsOrderRev As New DataSet()

        'DsOrderRev = objGetData.GetOrderForRef(Session("RefNumber").ToString())
        DsOrderRev = objGetData.GetOrderReviewGroup(Session("RefNumber").ToString())
        If DsOrderRev.Tables(0).Rows.Count = 0 Then
            Session("RefNumber") = objGetData.GetRefNumber()
        End If


        ds = objGetData.GetOrderDetails(OrderId, Session("RefNumber"))


        lblRefNumber.Text = "<b>Reference Number: </b>" + Session("RefNumber").ToString()
        lblorder.Text = "" + ds.Tables(0).Rows(0).Item("PARTDES").ToString()
        Try
            rptOrder.DataSource = ds
            rptOrder.DataBind()

            For Each DataItem As RepeaterItem In rptOrder.Items
                Dim txt As New TextBox()
                Dim lbl As New Label()
                If DataItem.ItemType = ListItemType.Item Or DataItem.ItemType = ListItemType.AlternatingItem Then
                    txt = DataItem.FindControl("txtQuantity")

                    txt.MaxLength = 10
                    txt.Width = 100
                    txt.ID = ds.Tables(0).Rows(DataItem.ItemIndex).Item("FORMATID").ToString()

                    lbl = DataItem.FindControl("lblDelFromat")
                    If lbl.Text.Trim() = "Single User License - PDF" Or lbl.Text.Trim() = "Single User License - Hardcopy" Then
                        lbl.Attributes.Add("onmouseover", "Tip('The single user license is for one person and does not allow any copying, emailing, or re-distribution.')")
                        lbl.Attributes.Add("onmouseout", "UnTip()")
                    End If

                    If lbl.Text.Trim() = "Corporate License - PDF" Or lbl.Text.Trim() = "Corporate License - Hardcopy" Then
                        lbl.Attributes.Add("onmouseover", "Tip('The corporate license allows copying, emailing, and re-distribution within the company and its 100% owned subsidiaries.')")
                        lbl.Attributes.Add("onmouseout", "UnTip()")
                    End If


                End If




            Next

        Catch ex As Exception
            ErrorLable.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub

 



    Protected Sub btnAddToCart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddToCart.Click
        Dim objCal As New Calulate_ShopItem()
        Try
            If Not objRefresh.IsRefresh Then
                objCal.Calculate(OrderId, rptOrder, Session("RefNumber").ToString())
                Response.Redirect("Thanks.aspx", False)
            End If

        Catch ex As Exception
            ErrorLable.Text = "Error:btnAddToCart_Click:" + ex.Message.ToString()
        End Try
    End Sub
End Class
