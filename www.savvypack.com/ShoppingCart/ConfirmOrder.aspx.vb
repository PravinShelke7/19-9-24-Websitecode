Imports System.Data
Imports System.Data.OleDb
Imports System
Imports ShopGetData
Imports ShopUpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Imports System.Net.Mail
Imports System.Net
Imports System.Configuration
Imports System.Collections.Generic
Imports System.IO
'Imports Stripe
'Imports Stripe.Checkout

Partial Class ShoppingCart_ConfirmOrder
    Inherits System.Web.UI.Page

Public sessionId As String = ""

#Region "Get Set Variables"
    Dim _lErrorLble As Label
    Dim _ctlContentPlaceHolder As ContentPlaceHolder
    Dim _billAddressId As String
    Dim _ShipAddressId As String
    Dim _CardHAddressID As String
    Dim _MenuSlider As Panel

    Public Property MenuSlider() As Panel
        Get
            Return _MenuSlider
        End Get
        Set(ByVal value As Panel)
            _MenuSlider = value
        End Set
    End Property

    Public Property ErrorLable() As Label
        Get
            Return _lErrorLble
        End Get
        Set(ByVal Value As Label)
            _lErrorLble = Value
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

    Public Property BillAddressID() As String
        Get
            Return _billAddressId
        End Get
        Set(ByVal value As String)
            Dim obj As New CryptoHelper
            _billAddressId = obj.Decrypt(value)
        End Set
    End Property

    Public Property ShipAddressID() As String
        Get
            Return _ShipAddressId
        End Get
        Set(ByVal value As String)
            Dim obj As New CryptoHelper
            _ShipAddressId = obj.Decrypt(value)
        End Set
    End Property

    Public Property CardHAddressID() As String
        Get
            Return _CardHAddressID
        End Get
        Set(ByVal value As String)
            _CardHAddressID = value
        End Set
    End Property


#End Region

#Region "MastePage Content Variables"

    Protected Sub GetMasterPageControls()
        Session("MenuItem") = "SHOPPING"
        GetErrorLable()
        GetContentPlaceHolder()
        GetMenuSlider()
    End Sub

    Protected Sub GetErrorLable()
        ErrorLable = Page.Master.FindControl("lblError")
    End Sub


    Protected Sub GetContentPlaceHolder()
        ctlContentPlaceHolder = Page.Master.FindControl("Econ1ContentPlaceHolder")
    End Sub

    Protected Sub GetMenuSlider()
        MenuSlider = Page.Master.FindControl("MenuSlider")
        MenuSlider.Visible = False
    End Sub
#End Region

#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_SHOPPINGCART_CONFIRMORDER")
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        objRefresh.Render(Page)
    End Sub

#End Region


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim LogInUserAddID As Integer
        Dim objGetUserData As New UsersGetData.Selectdata()
        Try
            If Session("RefNumber") = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "window.close();", True)
                'Response.Redirect("~/Index.aspx", False)
            End If
            Response.Cache.SetCacheability(HttpCacheability.NoCache)

            If Session("Back") = Nothing And Session("SBack") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("~/Universal_loginN/Pages/ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "", False)
            End If

            If Not IsPostBack Then
                hidItemNum.Value = "0"
                hidlnkEdit.Value = "Enable"
                lnkBillTo.Visible = True
                lnkHEdit.Visible = True
                'lnkCardEdit.Visible = True
            Else
                hidlnkEdit.Value = "Disable"
                lnkBillTo.Visible = False
                lnkHEdit.Visible = False
                'lnkCardEdit.Visible = False
            End If

            GetMasterPageControls()
            BillAddressID = Request.QueryString("BillId")
            hidBillToID.Value = BillAddressID
            ShipAddressID = Request.QueryString("ShipId")
            CardHAddressID = "0" 'Request.QueryString("CardHID")

            '21_Dec_2017
            LogInUserAddID = objGetUserData.GetUserAddressIDByHeadID("Login User", Session("UserId"))
            hdnUserID.Value = LogInUserAddID
            'end

            GetPageDetails()
            hdnReffNumber.Value = Session("RefNumber")
            'ChargePaymentStripe()
        Catch ex As Exception
            ErrorLable.Text = "Error:Page_Load" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetPageDetails()
        Dim objGetData As New Selectdata()
        Dim objGetUserData As New UsersGetData.Selectdata()
        Dim ds As New DataSet()
        Dim dsOrder As New DataSet()
        Dim dsCust As New DataSet()
        Dim dsBill As New DataSet()
        Dim dsShip As New DataSet()
        Dim dsCardH As New DataSet()
        Dim dsTax As New DataSet()
        Dim total As Double
        Dim taxAmount As Double
        Dim shippingCost As Double
        Try
            ds = objGetUserData.GetUserDetails(Session("UserId"))
            dsOrder = objGetData.GetOrderReviewGroup(Session("RefNumber"))
            dsCust = objGetData.GetCustInfo(Session("RefNumber"))
            dsBill = objGetUserData.GetBillToShipToUserDetailsByAddID(BillAddressID)
            dsCardH = objGetUserData.GetBillToShipToUserDetailsByAddID(CardHAddressID)
            dsTax = objGetData.GetTax()

            For i = 0 To dsOrder.Tables(0).Rows.Count - 1
                If dsOrder.Tables(0).Rows(i).Item("ITEMNUMBER").ToString() = "SA" Then
                    hidItemNum.Value = "1"
                ElseIf dsOrder.Tables(0).Rows(i).Item("ITEMNUMBER").ToString() = "KBCOPK" Then
                    hidItemNum.Value = "2"
                End If
            Next

            'IF THE PATMENT TYPE IS INVOICE 
            If dsCust.Tables(0).Rows(0)("PAYMENTTYPE").ToString().ToUpper() = "INVOICE" Then
                'hidItemNum.Value = "0"
                lblMainHeader.Text = "Invoice Request"
                lblSubHeader.Text = "Please confirm the information shown below."
                btnOrder.Text = "Request Invoice"
                lnkbtninvoice.Visible = True
                ' HideShowCardDetails(False)
            Else
                lblMainHeader.Text = "Confirm Order"
                lblSubHeader.Text = "Please confirm the information shown below."
                btnOrder.Text = "Charge Your Card"
                btnOrder.Visible = True
                ' HideShowCardDetails(True)
            End If

            'Order Review
            rptOrderReview.DataSource = dsOrder
            rptOrderReview.DataBind()
            'TO DISPLAY IN TABLE FORMAT
            For Each DataItem As RepeaterItem In rptOrderReview.Items
                Dim lblUnitCost As New Label()
                Dim lblSubTotal As New Label()
                Dim lblSTax As New Label()
                Dim lblShipCost As New Label()
                Dim lblTotal As New Label()
                If DataItem.ItemType = ListItemType.Item Or DataItem.ItemType = ListItemType.AlternatingItem Then
                    lblUnitCost = DataItem.FindControl("lblUnitCost")
                    lblSubTotal = DataItem.FindControl("lblSubTotal")
                    lblTotal = DataItem.FindControl("lblTotal")
                    lblSTax = DataItem.FindControl("lblSTax")
                    lblShipCost = DataItem.FindControl("lblShipCost")



                    lblUnitCost.Text = FormatNumber(dsOrder.Tables(0).Rows(DataItem.ItemIndex).Item("UNITCOST").ToString(), 2)
                    lblSubTotal.Text = FormatNumber(dsOrder.Tables(0).Rows(DataItem.ItemIndex).Item("SUBTOTAL").ToString(), 2)
                    lblSTax.Text = FormatNumber(dsOrder.Tables(0).Rows(DataItem.ItemIndex).Item("SALESTAX").ToString(), 2)
                    lblTotal.Text = FormatNumber(dsOrder.Tables(0).Rows(DataItem.ItemIndex).Item("TOTAL").ToString(), 2)
                    lblShipCost.Text = FormatNumber(dsOrder.Tables(0).Rows(DataItem.ItemIndex).Item("SHIPPINGCOST").ToString(), 2)
                    shippingCost = shippingCost + FormatNumber(dsOrder.Tables(0).Rows(DataItem.ItemIndex).Item("SHIPPINGCOST").ToString(), 2)

                    taxAmount = taxAmount + CDbl(lblSTax.Text)
                    total = total + CDbl(lblTotal.Text)
                End If
            Next


            'User Infromation

            'User Infromation

            If ds.Tables(0).Rows.Count > 0 Then
                lblName.Text = ds.Tables(0).Rows(0).Item("PREFIX").ToString() + " " + ds.Tables(0).Rows(0).Item("FIRSTNAME").ToString() + " " + ds.Tables(0).Rows(0).Item("LASTNAME").ToString() + " "
                lblEmail.Text = ds.Tables(0).Rows(0).Item("EMAILADDRESS").ToString()
                lblCompName.Text = ds.Tables(0).Rows(0).Item("COMPANYNAME").ToString()
                lblHPhone.Text = ds.Tables(0).Rows(0).Item("PHONENUMBER").ToString()
                lblHFax.Text = ds.Tables(0).Rows(0).Item("FAXNUMBER").ToString()
                lblHStAdd.Text = ds.Tables(0).Rows(0).Item("STREETADDRESS1").ToString() + " " + ds.Tables(0).Rows(0).Item("STREETADDRESS2").ToString()
                lblHCity.Text = ds.Tables(0).Rows(0).Item("CITY").ToString()
                lblHState.Text = ds.Tables(0).Rows(0).Item("STATE").ToString()
                lblHZip.Text = ds.Tables(0).Rows(0).Item("ZIPCODE").ToString()
                lblHCountry.Text = ds.Tables(0).Rows(0).Item("COUNTRY").ToString()

                'lblCC.Text = dsCust.Tables(0).Rows(0).Item("CARDNUMBER").ToString()
                'lblCCExp.Text = dsCust.Tables(0).Rows(0).Item("CARDEXP").ToString()
                'lblCCType.Text = dsCust.Tables(0).Rows(0).Item("CARDTYPE").ToString()
                'lblAuth_Code.Text = dsCust.Tables(0).Rows(0).Item("AUTH_CODE").ToString()

                hidExpDate.Value = dsCust.Tables(0).Rows(0).Item("CARDEXP").ToString()

                'Setting Bill To Address
                If dsBill.Tables(0).Rows.Count > 0 Then
                    lblBillToName.Text = dsBill.Tables(0).Rows(0).Item("FULLNAME").ToString()
                    lblBillToEmail.Text = dsBill.Tables(0).Rows(0).Item("EMAILADDRESS").ToString()
                    lblBillToComp.Text = dsBill.Tables(0).Rows(0).Item("COMPANYNAME").ToString()
                    lblphne.Text = dsBill.Tables(0).Rows(0).Item("PHONENUMBER").ToString()
                    lblFax.Text = dsBill.Tables(0).Rows(0).Item("FAXNUMBER").ToString()
                    lblAdd.Text = dsBill.Tables(0).Rows(0).Item("STREETADDRESS1").ToString() + " " + dsBill.Tables(0).Rows(0).Item("STREETADDRESS2").ToString()
                    lblCity.Text = dsBill.Tables(0).Rows(0).Item("CITY").ToString()
                    lblState.Text = dsBill.Tables(0).Rows(0).Item("STATE").ToString()
                    lblZipCode.Text = dsBill.Tables(0).Rows(0).Item("ZIPCODE").ToString()
                    lblCntry.Text = dsBill.Tables(0).Rows(0).Item("COUNTRYDES").ToString()

                    hidFName.Value = dsBill.Tables(0).Rows(0).Item("FIRSTNAME").ToString()
                    hidLName.Value = dsBill.Tables(0).Rows(0).Item("LASTNAME").ToString()
                    hidCompanyName.Value = dsBill.Tables(0).Rows(0).Item("COMPANYNAME").ToString()
                End If

                'Setting Ship To Address
                GetShipAddress()
                'Setting Card Holder Address
                'If CardHAddressID <> "0" Then
                '    lblCardHName.Text = dsCardH.Tables(0).Rows(0).Item("FULLNAME").ToString()
                '    lblCardHEmail.Text = dsCardH.Tables(0).Rows(0).Item("EMAILADDRESS").ToString()
                '    lblCardHphne.Text = dsCardH.Tables(0).Rows(0).Item("PHONENUMBER").ToString()
                '    lblCardHFax.Text = dsCardH.Tables(0).Rows(0).Item("FAXNUMBER").ToString()
                '    lblCardHAdd.Text = dsCardH.Tables(0).Rows(0).Item("STREETADDRESS1").ToString() + " " + dsCardH.Tables(0).Rows(0).Item("STREETADDRESS2").ToString()
                '    lblCardHCity.Text = dsCardH.Tables(0).Rows(0).Item("CITY").ToString()
                '    lblCardHState.Text = dsCardH.Tables(0).Rows(0).Item("STATE").ToString()
                '    lblCardHZipCode.Text = dsCardH.Tables(0).Rows(0).Item("ZIPCODE").ToString()
                '    lblCardHCntry.Text = dsCardH.Tables(0).Rows(0).Item("COUNTRYDES").ToString()
                'End If
            End If
            lblRefNumber.Text = Session("RefNumber")
            lblAmt.Text = FormatNumber(Session("GrantTotal"), 2)

            lblTax.Text = FormatNumber(taxAmount, 2)
            total = total + shippingCost

            lblGrandTotal.Text = FormatNumber((shippingCost + CDbl(Session("GrantTotal")) + taxAmount), 2)
            lblShippingCost.Text = FormatNumber(shippingCost, 2)

        Catch ex As Exception
            ErrorLable.Text = "Error:GetPageDetails" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub GetShipAddress()
        Dim i As Integer
        Dim k As Integer
        Dim lbl(10) As Label
        Dim tdInner(10) As TableCell
        Dim tdInner1 As TableCell
        Dim tdInner2 As TableCell
        Dim tdInner3 As TableCell

        Dim trInner(10) As TableRow
        Dim trInner1 As TableRow
        Dim trInner2 As TableRow
        Dim trInner3 As TableRow

        Dim dsShip As New DataSet
        Dim objGetUserData As New UsersGetData.Selectdata()
        Dim dsOrder As New DataSet()
        Dim objGetData As New Selectdata()

        Dim lnkEdit As New LinkButton
        Dim lblShp As Label
        Try
            dsOrder = objGetData.GetOrderReview(Session("RefNumber"))

            If dsOrder.Tables(0).Rows.Count > 0 Then
                For i = 0 To dsOrder.Tables(0).Rows.Count - 1
                    dsShip = objGetUserData.GetBillToShipToUserDetailsByAddID(dsOrder.Tables(0).Rows(i)("SHIPTOID").ToString().Trim())
                    For k = 1 To 10
                        trInner(k) = New TableRow
                    Next
                    trInner1 = New TableRow
                    trInner2 = New TableRow
                    trInner3 = New TableRow

                    For j = 1 To 2
                        For k = 1 To 10
                            tdInner(k) = New TableCell
                        Next
                        tdInner1 = New TableCell
                        tdInner2 = New TableCell
                        tdInner3 = New TableCell
                        Select Case j
                            Case 1
                                'Layer
                                For k = 1 To 10
                                    InnerTdSetting(tdInner(k), "33%", "left")
                                Next
                                lnkEdit = New LinkButton
                                lblShp = New Label
                                InnerTdSetting(tdInner1, "100%", "left")
                                InnerTdSetting(tdInner2, "100%", "left")
                                InnerTdSetting(tdInner3, "100%", "left")

                                tdInner1.Text = "<b>Item:</b>" + "" + dsOrder.Tables(0).Rows(i)("ITEMDESCRIPTION").ToString().Trim()
                                tdInner2.Text = "<b>Delivery Format:</b>" + dsOrder.Tables(0).Rows(i)("DELIVERYFORMAT").ToString().Trim()
                                'tdInner3.Text = "<b>Ship To Address" + (i + 1).ToString() + ":</b>"

                                lblShp.Text = "<b>Ship To Address" + (i + 1).ToString() + ":</b>"
                                lnkEdit.Text = "<b>Edit</b>"
                                lnkEdit.Style.Add("margin-left", "320px")
                                lnkEdit.ForeColor = Drawing.Color.Orange

                                If hidlnkEdit.Value.ToString() <> "Enable" Then
                                    lnkEdit.Visible = False
                                Else
                                    lnkEdit.CommandArgument = dsOrder.Tables(0).Rows(i)("SHIPTOID").ToString().Trim()
                                    lnkEdit.Attributes.Add("onclick", "return ShowEditPopup('" + lnkEdit.CommandArgument + "');")
                                End If

                                'lnkEdit.CommandArgument = dsOrder.Tables(0).Rows(i)("SHIPTOID").ToString().Trim()
                                'lnkEdit.Attributes.Add("onclick", "return ShowEditPopup('" + lnkEdit.CommandArgument + "');")
                                'AddHandler lnkEdit.Click, AddressOf LinkEdit_Select
                                tdInner3.Controls.Add(lblShp)
                                tdInner3.Controls.Add(lnkEdit)

                                tdInner3.ColumnSpan = 2
                                tdInner2.ColumnSpan = 2
                                tdInner1.ColumnSpan = 2
                                tdInner1.CssClass = "AlterNateColor1"
                                tdInner2.CssClass = "AlterNateColor2"
                                tdInner3.CssClass = "TdHeading"
                                tdInner3.Style.Add("font-size", "12px")
                                tdInner2.Style.Add("font-size", "12px")
                                tdInner1.Style.Add("font-size", "12px")
                                tdInner3.Height = "20"

                                tdInner(1).Text = "<b>Name: </b>"
                                tdInner(2).Text = "<b>Email: </b>"
                                tdInner(3).Text = "<b>Company Name: </b>"
                                tdInner(4).Text = "<b>Phone: </b>"
                                tdInner(5).Text = "<b>Fax: </b>"
                                tdInner(6).Text = "<b>Street Address: </b>"
                                tdInner(7).Text = "<b>City: </b>"
                                tdInner(8).Text = "<b>State: </b>"
                                tdInner(9).Text = "<b>Zip Code: </b>"
                                tdInner(10).Text = "<b>Country: </b>"

                                trInner3.Controls.Add(tdInner3)
                                trInner1.Controls.Add(tdInner1)
                                trInner2.Controls.Add(tdInner2)
                                For k = 1 To 10
                                    trInner(k).Controls.Add(tdInner(k))
                                Next
                            Case 2
                                For k = 1 To 10
                                    lbl(k) = New Label
                                    InnerTdSetting(tdInner(k), "67%", "Left")
                                Next

                                lbl(1).Text = dsShip.Tables(0).Rows(0).Item("FULLNAME").ToString()
                                lbl(2).Text = dsShip.Tables(0).Rows(0).Item("EMAILADDRESS").ToString()
                                lbl(3).Text = dsShip.Tables(0).Rows(0).Item("COMPANYNAME").ToString()
                                lbl(4).Text = dsShip.Tables(0).Rows(0).Item("PHONENUMBER").ToString()
                                lbl(5).Text = dsShip.Tables(0).Rows(0).Item("FAXNUMBER").ToString()
                                lbl(6).Text = dsShip.Tables(0).Rows(0).Item("STREETADDRESS1").ToString() + " " + dsShip.Tables(0).Rows(0).Item("STREETADDRESS2").ToString()
                                lbl(7).Text = dsShip.Tables(0).Rows(0).Item("CITY").ToString()
                                lbl(8).Text = dsShip.Tables(0).Rows(0).Item("STATE").ToString()
                                lbl(9).Text = dsShip.Tables(0).Rows(0).Item("ZIPCODE").ToString()
                                lbl(10).Text = dsShip.Tables(0).Rows(0).Item("COUNTRYDES").ToString()

                                For k = 1 To 10
                                    tdInner(k).Controls.Add(lbl(k))
                                    trInner(k).Controls.Add(tdInner(k))
                                Next
                        End Select
                        tblShipAdd.Controls.Add(trInner3)
                        tblShipAdd.Controls.Add(trInner1)
                        tblShipAdd.Controls.Add(trInner2)

                        For k = 1 To 10
                            If (k Mod 2 = 0) Then
                                trInner(k).CssClass = "AlterNateColor2"
                            Else
                                trInner(k).CssClass = "AlterNateColor1"
                            End If
                            tblShipAdd.Controls.Add(trInner(k))
                        Next
                    Next
                Next
            End If
        Catch ex As Exception
            ErrorLable.Text = "Error:GetShipAddress" + ex.Message.ToString()
        End Try
    End Sub

    'Protected Sub LinkEdit_Select(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Try
    '        Dim lnk As New LinkButton
    '        Dim lbl As New Label
    '        lnk = DirectCast(sender, LinkButton)
    '        lbl.Text = lnk.CommandArgument
    '        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "ShowEditPopup('" + lbl.Text + "');", True)
    '        'Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('" + lbl.Text + "');", True)
    '    Catch ex As Exception
    '        ErrorLable.Text = "Error:LinkEdit_Select:" + ex.Message.ToString()
    '    End Try
    'End Sub

    'Protected Sub lnkBillTo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBillTo.Click
    '    Try
    '        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "ShowEditPopup('" + BillAddressID + "');", True)
    '    Catch ex As Exception
    '        ErrorLable.Text = "Error:lnkBillTo_Click:" + ex.Message.ToString()
    '    End Try
    'End Sub

    Protected Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Response.Redirect(HttpContext.Current.Request.Url.ToString(), True)
        Catch ex As Exception
            ErrorLable.Text = "Error:btnRefresh_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub lnkbtninvoice_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtninvoice.Click
        Dim objCrypto As New CryptoHelper()
        Try
            Response.Redirect("OrderSuccess.aspx?BillId=" + objCrypto.Encrypt(BillAddressID.ToString()).ToString() + "&ShipId=" + objCrypto.Encrypt(ShipAddressID.ToString()).ToString())
        Catch ex As Exception
            ErrorLable.Text = "Error:lnkbtninvoice_Click" + ex.Message.ToString()
        End Try
    End Sub


    Protected Sub InnerTdSetting(ByVal Td As TableCell, ByVal Width As String, ByVal Align As String)
        Try

            If Width <> "" Then
                Td.Style.Add("width", Width)
            End If
            Td.Style.Add("text-align", Align)
            If Align = "Left" Then
                Td.Style.Add("padding-left", "5px")
            End If
            If Align = "Right" Then
                Td.Style.Add("padding-right", "5px")
            End If
            'Td.Style.Add("font-size", "13px")
            Td.CssClass = "WebInnerTd"
        Catch ex As Exception
            _lErrorLble.Text = "Error:InnerTdSetting:" + ex.Message.ToString()
        End Try
    End Sub
    'Public Sub HideShowCardDetails(ByVal flag As Boolean)
    '    Try
    '        If flag = True Then
    '            rowCardhead.Visible = True
    '            rowCardNo.Visible = True
    '            rowExpdate.Visible = True
    '            rowCardtype.Visible = True
    '            Tr2.Visible = True
    '        Else
    '            rowCardhead.Visible = False
    '            rowCardNo.Visible = False
    '            rowExpdate.Visible = False
    '            rowCardtype.Visible = False
    '            Tr2.Visible = False
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Protected Function ChargePaymentStripe() As Boolean
    '    Try

    '        StripeConfiguration.ApiKey = "sk_test_51JaOKiIWxVMfrD0Un7nJcoWvHJYb3OwqsKmt0peXZXYmqfiKmIeUDtBzO7Mo9mH52hJsZo8mCMYgZNffk4ekup2I005dzYboOS"
    '        Dim options = New SessionCreateOptions With {
    '            .SuccessUrl = "http://192.168.3.126:7020/ShoppingCart/OrderSuccess.aspx?BillId=" + Request.QueryString("BillId").ToString() + "&ShipId=" + Request.QueryString("BillId").ToString(),
    '            .CancelUrl = "http://192.168.3.126:7020/ShoppingCart/TransactionF.aspx",
    '            .PaymentMethodTypes = New List(Of String) From {
    '                "card"
    '            },
    '            .LineItems = New List(Of SessionLineItemOptions) From {
    '                New SessionLineItemOptions With {
    '                    .Name = "Confirm Order",
    '                    .Description = "Comfortable cotton t-shirt",
    '                    .Amount = lblGrandTotal.Text,
    '                    .Currency = "usd",
    '                    .Quantity = 1
    '                }
    '            },
    '            .Mode = "payment"
    '        }
    '        Dim service = New SessionService()
    '        Dim session As Session = service.Create(options)
    '        sessionId = session.Id
    '    Catch ex As Exception

    '    End Try
    'End Function
End Class
