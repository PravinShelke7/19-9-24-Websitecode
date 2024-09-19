Imports System.Data
Imports System.Data.OleDb
Imports System
Imports ShopGetData
Imports ShopUpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter

Partial Class ShoppingCart_ShoppingCart
    Inherits System.Web.UI.Page
    Dim strbill As String
#Region "Get Set Variables"
    Dim _lErrorLble As Label
    Dim _ctlContentPlaceHolder As ContentPlaceHolder



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
        ctlContentPlaceHolder = Page.Master.FindControl("ContentPlaceHolder1")
    End Sub

#End Region

#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_SHOPPINGCART_SHOPPINGCART")
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        objRefresh.Render(Page)
    End Sub

#End Region


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
			 If Session("Back") = Nothing And Session("SBack") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("~/Universal_loginN/Pages/ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "", False)
            End If
            GetMasterPageControls()
            'If Not IsPostBack Then
            BindShipAddress()
            lnkBillAdd.Attributes.Add("onclick", "return OpenNewWindow('UserAddressDetails.aspx?lnkDesId=" + ctlContentPlaceHolder.ClientID.ToString() + "_lnkBillAdd&lnkhdnID=" + ctlContentPlaceHolder.ClientID.ToString() + "_hdnBillAddId" + "','BillPage');")

            ' End If
            'GetExpirayDate()
            'If (rdbCreditcard.Checked = True) Then
            '    rowCardNo.Visible = True
            '    rowCardType.Visible = True
            '    rowExpDate.Visible = True
            'End If

            ' If Session("Back") = Nothing Then
            ' Dim obj As New CryptoHelper
            ' Response.Redirect("~/Universal_loginN/Pages/ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "", False)
            ' Else
            ' GetMasterPageControls()
            ' BindShipAddress()
            ' GetExpirayDate()
            ' If (rdbCreditcard.Checked = True) Then
            ' rowCardNo.Visible = True
            ' rowCardType.Visible = True
            ' rowExpDate.Visible = True
            ' End If
            ' lnkBillAdd.Attributes.Add("onclick", "return OpenNewWindow('UserAddressDetails.aspx?lnkDesId=" + ctlContentPlaceHolder.ClientID.ToString() + "_lnkBillAdd&lnkhdnID=" + ctlContentPlaceHolder.ClientID.ToString() + "_hdnBillAddId" + "','BillPage');")
            ' 'lnkCardHAdd.Attributes.Add("onclick", "return OpenNewWindow('UserAddressDetails.aspx?lnkDesId=" + ctlContentPlaceHolder.ClientID.ToString() + "_lnkCardHAdd&lnkhdnID=" + ctlContentPlaceHolder.ClientID.ToString() + "_hdnCardHAddId" + "','CardHPage');")
            ' End If

            If Not IsPostBack Then
                hdnBillAddId.Value = "0"
                'hdnCardHAddId.Value = "0"
                hdnShipAddId.Value = "0"
                btnOrder.Attributes.Add("onclick", "return cardCheck('" + ctlContentPlaceHolder.ClientID + "','hidMatid');")
                GetSavedDetails()
            End If
            GetPageDetails()
            'If ViewState("SavedInfo") = "" Then                
            'Else
            '    ViewState("SavedInfo") = "false"
            'End If
        Catch ex As Exception
            ErrorLable.Text = "Error:Page_Load" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetPageDetails()
        Dim objGetData As New Selectdata()
        Dim objGetUserData As New UsersGetData.Selectdata()
        Dim ds As New DataSet()
        Dim objCrypto As New CryptoHelper()
        'Session("UserId") = "1621"
        Try
            'hypUser.NavigateUrl = "~/Users_Login/AddEditUser.aspx?Mode=" + objCrypto.Encrypt("Edit") + ""
            ds = objGetUserData.GetUserDetails(Session("UserId"))

            If ds.Tables(0).Rows.Count > 0 Then
                lblName.Text = ds.Tables(0).Rows(0).Item("PREFIX").ToString() + " " + ds.Tables(0).Rows(0).Item("FIRSTNAME").ToString() + " " + ds.Tables(0).Rows(0).Item("LASTNAME").ToString() + " "
                lblEmail.Text = ds.Tables(0).Rows(0).Item("EMAILADDRESS").ToString()                
                lblCompName.Text = ds.Tables(0).Rows(0).Item("COMPANYNAME").ToString()                
            End If

            lblRefNumber.Text = Session("RefNumber")
            lblAmt.Text = FormatNumber(Session("GrantTotal"), 2)

        Catch ex As Exception
            ErrorLable.Text = "Error:GetPageDetails" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub GetSavedDetails()
        Dim dsCust As New DataSet()
        Dim dsOrderIds As New DataSet()
        Dim dsBillToUser As New DataSet()
        Dim dsCardToUser As New DataSet()
        Dim objGetUserData As New UsersGetData.Selectdata()
        Dim objGetData As New Selectdata()
        Dim dsOrder As New DataSet()
        Try
            'ViewState("SavedInfo") = "false"
            dsOrder = objGetData.GetOrderReview(Session("RefNumber"))
            dsCust = objGetData.GetCustInfo(Session("RefNumber"))
            dsOrderIds = objGetData.GetOrderReviewAShop(Session("RefNumber"))
            If dsCust.Tables(0).Rows.Count > 0 Then

                'IF THE PATMENT TYPE IS INVOICE 
                If dsCust.Tables(0).Rows(0)("PAYMENTTYPE").ToString().ToUpper() = "INVOICE" Then
                    rdbInvoice.Checked = True
                   
                    'rowCardType.Style.Add("display", "none")
                    'rowExpDate.Style.Add("display", "none")
                    'trSecurityCode.Style.Add("display", "none")
                    ''rowCardHolderAdd.Style.Add("display", "none")
                    'rowCardNo.Style.Add("display", "none")

                Else
                    rdbCreditcard.Checked = True
                    'txtCC.Text = dsCust.Tables(0).Rows(0).Item("CARDNUMBER").ToString()
                    'txtAuthCode.Text = dsCust.Tables(0).Rows(0).Item("AUTH_CODE").ToString()
                    'ddlCCType.Text = dsCust.Tables(0).Rows(0).Item("CARDTYPE").ToString()
                    'ddlMonth.Text = Split(dsCust.Tables(0).Rows(0).Item("CARDEXP").ToString(), "/")(0).ToString()
                    'ddlYears.Text = Split(dsCust.Tables(0).Rows(0).Item("CARDEXP").ToString(), "/")(1).ToString()
                    ''dsCardToUser = objGetUserData.GetBillToShipToUserDetailsByAddID(dsOrderIds.Tables(0).Rows(0).Item("CARDHID").ToString())
                    'If dsCardToUser.Tables(0).Rows.Count > 0 Then
                    '    lnkCardHAdd.Text = dsCardToUser.Tables(0).Rows(0).Item("USERNAME").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + dsCardToUser.Tables(0).Rows(0).Item("ADDHEADER").ToString()
                    '    hdnCardHAddId.Value = dsOrderIds.Tables(0).Rows(0).Item("CARDHID").ToString()
                    'End If
                End If
          
                dsBillToUser = objGetUserData.GetBillToShipToUserDetailsByAddID(dsOrderIds.Tables(0).Rows(0).Item("BILLTOID").ToString())
                If dsBillToUser.Tables(0).Rows.Count > 0 Then
                    lnkBillAdd.Text = dsBillToUser.Tables(0).Rows(0).Item("USERNAME").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + dsBillToUser.Tables(0).Rows(0).Item("ADDHEADER").ToString()
                    hdnBillAddId.Value = dsOrderIds.Tables(0).Rows(0).Item("BILLTOID").ToString()
                End If
            Else
                lnkBillAdd.ToolTip = "Click to choose or create the Bill to address, which is the name on the credit card and the billing address of the credit card."
                'lnkCardHAdd.ToolTip = "Click to choose or create the Credit Card Holder’s address, which is normally the same as the bill to address."
            End If

        Catch ex As Exception
            'ErrorLable.Text = "Error:GetPageDetails" + ex.Message.ToString()
        End Try
    End Sub
    Public Sub BindShipAddress()
        Dim i As New Integer
        Dim j As New Integer
        Dim DWidth As String = String.Empty
        Dim trHeader As New TableRow
        Dim trInner As New TableRow
        Dim lbl As New Label
        Dim hid As New HiddenField
        Dim Link As New HyperLink
        Dim tdInner As TableCell
        Dim objGetData As New Selectdata()
        Dim dsOrder As New DataSet()
        Dim dsShipAdd As New DataSet()
        Dim objGetUserData As New UsersGetData.Selectdata()
        Dim Path As String = ""

        Try
            tblShipAdd.Rows.Clear()
            dsOrder = objGetData.GetOrderReview(Session("RefNumber"))

            hdnShipAddNo.Value = dsOrder.Tables(0).Rows.Count
            If dsOrder.Tables(0).Rows.Count > 0 Then
                'Inner
                For i = 1 To dsOrder.Tables(0).Rows.Count
                    trInner = New TableRow
                    For j = 1 To 2
                        tdInner = New TableCell

                        Select Case j
                            Case 1
                                'Layer
                                InnerTdSetting(tdInner, "136px", "left")
                                tdInner.Text = "<b>*Ship To Address" + i.ToString() + ":</b>"
                                tdInner.CssClass = "WebInnerTd"
                                tdInner.Style.Add("Color", "red")
                                tdInner.Attributes.Add("Title", "Person and address receiving the product or service.")
                                trInner.Controls.Add(tdInner)
                            Case 2
                                InnerTdSetting(tdInner, "464px", "Left")
                                Link = New HyperLink
                                hid = New HiddenField
                                lbl = New Label()
                                lbl.Width = 230
                                lbl.Text = "(" + dsOrder.Tables(0).Rows(i - 1).Item("ITEMDESCRIPTION").ToString() + " " + dsOrder.Tables(0).Rows(i - 1).Item("DELIVERYFORMAT").ToString() + ")"
                                lbl.CssClass = "SmallLabel"
                                Link.ID = "hypMatDes" + i.ToString()
                                hid.ID = "hidMatid" + i.ToString()
                                Link.Width = 210

                                Path = "UserAddressDetails.aspx?lnkDesId=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + Link.ID + "&lnkhdnID=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + hid.ID
                                Link.NavigateUrl = "javascript:ShowPopWindow('" + Path + "','SHIP')"
                                Link.CssClass = "Link"
                                If dsOrder.Tables(0).Rows(i - 1).Item("SHIPTOID").ToString() <> "" Then
                                    dsShipAdd = objGetUserData.GetBillToShipToUserDetailsByAddID(dsOrder.Tables(0).Rows(i - 1).Item("SHIPTOID").ToString())
                                    If dsShipAdd.Tables(0).Rows.Count > 0 Then
                                        Link.Text = dsShipAdd.Tables(0).Rows(0).Item("USERNAME").ToString() + "&nbsp;&nbsp;&nbsp;" + dsShipAdd.Tables(0).Rows(0).Item("ADDHEADER").ToString()
                                        hid.Value = dsOrder.Tables(0).Rows(i - 1).Item("SHIPTOID").ToString()
                                    Else
                                        Link.Text = "Nothing Selected"
                                        Link.ToolTip = "Click to choose or create the ship to address, which is the person and address receiving the product or service."
                                    End If
                                Else
                                    Link.Text = "Nothing Selected"
                                    Link.ToolTip = "Click to choose or create the ship to address, which is the person and address receiving the product or service."
                                End If


                                tdInner.Controls.Add(hid)
                                tdInner.Controls.Add(Link)
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                        End Select
                    Next
                    If (i Mod 2 = 0) Then
                        trInner.CssClass = "AlterNateColor1"
                    Else
                        trInner.CssClass = "AlterNateColor2"
                    End If
                    trInner.Height = 20
                    tblShipAdd.Controls.Add(trInner)
                Next
            End If

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub
    'Protected Sub GetExpirayDate()
    '    Dim i As New Integer
    '    Dim lst As New ListItem
    '    Try
    '        For i = 1 To 12
    '            lst = New ListItem
    '            lst.Text = i.ToString()
    '            lst.Value = i.ToString()
    '            ddlMonth.Items.Add(lst)
    '            ddlMonth.AppendDataBoundItems = True
    '        Next

    '        For i = 0 To 20
    '            lst = New ListItem
    '            lst.Text = System.DateTime.Now.Year + i.ToString()
    '            lst.Value = System.DateTime.Now.Year + i.ToString()
    '            ddlYears.Items.Add(lst)
    '            ddlYears.AppendDataBoundItems = True
    '        Next


    '    Catch ex As Exception
    '        ErrorLable.Text = "Error:GetExpirayDate:" + ex.Message.ToString()
    '    End Try
    'End Sub

    Protected Sub btnOrder_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOrder.Click
        Dim objUpIns As New UpdateInsert()
        Dim ExpDate As String = String.Empty        
        Dim objCrypto As New CryptoHelper()
        Dim objGetUserData As New UsersGetData.Selectdata()
        Dim ds As New DataSet
        Dim dsItems As New DataSet
        Dim dsTax As New DataSet
        Dim dsShippingCost As New DataSet
        Dim objGetShopdata As New ShopGetData.Selectdata()
        Dim total As Double
        Dim taxAmount As Double
        Dim flag As Boolean
        Dim shippingCost As Double
        Dim shipCreditCost As Double
        Dim flagnew As Boolean = False
        Try
            'ExpDate = ddlMonth.SelectedItem.Value.ToString() + "/" + ddlYears.SelectedItem.Value.ToString()
            'CALCULATION OF TAX
            dsItems = objGetShopdata.GetOrderReview(Session("RefNumber").ToString())
            dsTax = objGetShopdata.GetTax()
            Dim shipId As String
            For i = 0 To dsItems.Tables(0).Rows.Count - 1

                flag = objGetShopdata.IsTaxable(dsItems.Tables(0).Rows(i)("ITEMNUMBER").ToString())
                dsShippingCost = objGetShopdata.GetShippingInfo(dsItems.Tables(0).Rows(i)("ITEMNUMBER").ToString(), dsItems.Tables(0).Rows(i)("SEQUENCE").ToString())
                shippingCost = CDbl(dsItems.Tables(0).Rows(i)("QTY").ToString()) * CDbl(dsShippingCost.Tables(0).Rows(0)("SHIPPINGCOST").ToString())
                shipCreditCost = shippingCost
                total = CDbl(dsItems.Tables(0).Rows(i)("SUBTOTAL").ToString()) + shippingCost ' new change

                shipId = Request.Form("ctl00$ContentPlaceHolder1$hidMatid" + (i + 1).ToString() + "")
              
                If shipId = "" Then
                    flagnew = True
                End If
                If shipId <> "" Then

                    ds = objGetUserData.GetBillToShipToUserDetailsByAddID(shipId)
                    If ds.Tables(0).Rows.Count > 0 Then
                        If ds.Tables(0).Rows(0)("STATE").ToString().ToUpper() = "MINNESOTA" And ds.Tables(0).Rows(0)("COUNTRYDES").ToString().ToUpper() = "UNITED STATES" And flag = True Then
                            taxAmount = ((total * CDbl(dsTax.Tables(0).Rows(0)("SALESTAXVALUE").ToString())) / 100) 'new change

                            If rdbCreditcard.Checked = True Then
                                shipCreditCost = ((total * CDbl(dsTax.Tables(0).Rows(0)("CREDITCARDVALUE").ToString())) / 100) + shippingCost
                                total = shipCreditCost + total + taxAmount - shippingCost 'new change
                                objUpIns.UpdateTax(Session("RefNumber"), hdnBillAddId.Value, shipId, "NULL", taxAmount, total, dsItems.Tables(0).Rows(i)("ITEMNUMBER").ToString(), dsItems.Tables(0).Rows(i)("SEQUENCE").ToString(), shipCreditCost, dsItems.Tables(0).Rows(i)("QTYSEQ").ToString())
                            Else
                                total = total + taxAmount
                                objUpIns.UpdateTax(Session("RefNumber"), hdnBillAddId.Value, shipId, "0", taxAmount, total, dsItems.Tables(0).Rows(i)("ITEMNUMBER").ToString(), dsItems.Tables(0).Rows(i)("SEQUENCE").ToString(), shipCreditCost, dsItems.Tables(0).Rows(i)("QTYSEQ").ToString())
                            End If
                        Else
                            If rdbCreditcard.Checked = True Then
                                shipCreditCost = ((total * CDbl(dsTax.Tables(0).Rows(0)("CREDITCARDVALUE").ToString())) / 100) + shippingCost
                                total = shipCreditCost + total - shippingCost
                                objUpIns.UpdateTax(Session("RefNumber"), hdnBillAddId.Value, shipId, "NULL", "0.00", total, dsItems.Tables(0).Rows(i)("ITEMNUMBER").ToString(), dsItems.Tables(0).Rows(i)("SEQUENCE").ToString(), shipCreditCost, dsItems.Tables(0).Rows(i)("QTYSEQ").ToString())
                            Else
                                objUpIns.UpdateTax(Session("RefNumber"), hdnBillAddId.Value, shipId, "0", "0.00", total, dsItems.Tables(0).Rows(i)("ITEMNUMBER").ToString(), dsItems.Tables(0).Rows(i)("SEQUENCE").ToString(), shipCreditCost, dsItems.Tables(0).Rows(i)("QTYSEQ").ToString())
                            End If
                        End If
                    End If
                End If

                shipCreditCost = 0
                shippingCost = 0
                taxAmount = 0
            Next
            'Response.Redirect("ConfirmOrder.aspx?BillId=" + objCrypto.Encrypt(hdnBillAddId.Value).ToString() + "&ShipId=" + objCrypto.Encrypt(hdnShipAddId.Value).ToString())
            If flagnew = False And hdnBillAddId.Value <> "0" Then

                If rdbCreditcard.Checked = True Then
                    objUpIns.InsertCustomerInfo(Session("RefNumber"), "", "", "", Session("UserId"), hdnBillAddId.Value, hdnShipAddId.Value, "0", "CreditCard", "", "")
                Else
                    objUpIns.InsertCustomerInfo(Session("RefNumber"), "", "", "", Session("UserId"), hdnBillAddId.Value, hdnShipAddId.Value, "0", "Invoice", "", "")
                End If

                If rdbCreditcard.Checked = True Then
                    Response.Redirect("ConfirmOrder.aspx?BillId=" + objCrypto.Encrypt(hdnBillAddId.Value).ToString() + "&ShipId=" + objCrypto.Encrypt(hdnShipAddId.Value).ToString())
                Else
                    Response.Redirect("ConfirmOrder.aspx?BillId=" + objCrypto.Encrypt(hdnBillAddId.Value).ToString() + "&ShipId=" + objCrypto.Encrypt(hdnShipAddId.Value).ToString())
                End If
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Please select all details.');", True)

            End If
            BindShipAddress()
            If hdnBillAddId.Value <> "0" Then
                lnkBillAdd.Text = hdnbilladdDes.Value.ToString()

            End If
        Catch ex As Exception
            ErrorLable.Text = "Error:btnOrder_Click" + ex.Message.ToString()
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
            Td.Style.Add("font-size", "13px")
        Catch ex As Exception
            _lErrorLble.Text = "Error:InnerTdSetting:" + ex.Message.ToString()
        End Try
    End Sub
End Class
