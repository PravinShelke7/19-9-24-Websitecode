Imports System.Data
Imports System.Data.OleDb
Imports System
Imports ShopGetData
Imports ShopUpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class ShoppingCart_OrderReview
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
        objRefresh = New zCon.Net.Refresh("_SHOPPINGCART_ORDERREVIEW")
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        objRefresh.Render(Page)
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
		 If Session("Back") = Nothing And Session("SBack") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("~/Universal_loginN/Pages/ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "", False)
            End If

            If Session("RefNumber") = Nothing Then
                btnOrderProcedd.Enabled = False
                btnEmpty.Enabled = False
            Else
                btnOrderProcedd.Enabled = True
                btnEmpty.Enabled = True
            End If

			 GetMasterPageControls()
             GetPageDetails()
            ' If Session("Back") = Nothing Then
                ' If Session("SBack") = Nothing Then
                    ' Dim obj As New CryptoHelper
                    ' Response.Redirect("~/Universal_loginN/Pages/ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "", False)
                ' End If
            ' Else
                'GetMasterPageControls()
                'GetPageDetails()
            ' End If
           
        Catch ex As Exception
            ErrorLable.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetPageDetails()
        Dim objGetData As New Selectdata()
        Dim ds As New DataSet()
        Dim i As New Integer
        Dim GrantTotal As New Integer
        'ds = objGetData.GetOrderReview(Session("RefNumber"))
        If Session("RefNumber") <> Nothing Then

            ds = objGetData.GetOrderReviewGroup(Session("RefNumber"))

            lblRefNumber.Text = "<b>Order Number: </b>" + Session("RefNumber").ToString()
            Try
                rptOrderReview.DataSource = ds
                rptOrderReview.DataBind()

                For i = 0 To ds.Tables(0).Rows.Count - 1

                Next

                For Each DataItem As RepeaterItem In rptOrderReview.Items
                    Dim lblUnitCost As New Label()
                    Dim lblSubTotal As New Label()
                    If DataItem.ItemType = ListItemType.Item Or DataItem.ItemType = ListItemType.AlternatingItem Then
                        lblUnitCost = DataItem.FindControl("lblUnitCost")
                        lblSubTotal = DataItem.FindControl("lblSubTotal")
                        lblUnitCost.Text = FormatNumber(ds.Tables(0).Rows(DataItem.ItemIndex).Item("UNITCOST").ToString(), 2)
                        lblSubTotal.Text = FormatNumber(ds.Tables(0).Rows(DataItem.ItemIndex).Item("SUBTOTAL").ToString(), 2)
                        GrantTotal = GrantTotal + Convert.ToInt32(ds.Tables(0).Rows(DataItem.ItemIndex).Item("SUBTOTAL").ToString())
                    End If
                Next

                lblOrderTotal.Text = FormatNumber(GrantTotal, 2)
                Session("GrantTotal") = GrantTotal



            Catch ex As Exception
                ErrorLable.Text = "Error:Page_Load:" + ex.Message.ToString()
            End Try
        End If

    End Sub

    Protected Sub btnOrderProcedd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOrderProcedd.Click
        Dim objUpdateData As New UsersUpdateData.UpdateInsert()
        Try
             If Session("Back") <> Nothing Or Session("SBack") <> Nothing Then
                If Not objRefresh.IsRefresh Then
                    Dim objGetData As New UsersGetData.Selectdata()
                    Dim ds As New DataSet()
                    ds = objGetData.GetEmailConfigDetails("Y")

                    objUpdateData.InsShoppingCartAddDetails(Session("UserId").ToString())
                    'Response.Redirect("ShoppingCart.aspx")
                    'Response.Redirect(ds.Tables(0).Rows(0).Item("HTTPSURL").ToString(), False)
		     Response.Redirect("UserInformation.aspx", True)
                End If
            End If
        Catch ex As Exception
            ErrorLable.Text = "Error:btnOrderProcedd_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnEmpty_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEmpty.Click
        Dim objUpIns As New UpdateInsert()
        Try
            If Not objRefresh.IsRefresh Then
                objUpIns.DeleteAllInventory(Session("RefNumber"))
                btnOrderProcedd.Enabled = False
                Session("RefNumber") = Nothing

                GetPageDetails()
                btnEmpty.Enabled = False
            End If
        Catch ex As Exception
            ErrorLable.Text = "Error:btnEmpty_Click:" + ex.Message.ToString()
        End Try
    End Sub
End Class
