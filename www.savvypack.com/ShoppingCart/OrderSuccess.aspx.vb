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

Partial Class ShoppingCart_OrderSuccess
    Inherits System.Web.UI.Page
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
        objRefresh = New zCon.Net.Refresh("_SHOPPINGCART_ORDERSUCCESS")
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

                hidlnkEdit.Value = "Disable"
                lnkBillTo.Visible = False
                lnkHEdit.Visible = False
                ' lnkCardEdit.Visible = False          
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
                btnOrder_Click()
            hdnReffNumber.Value = Session("RefNumber")
            End If

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
        ' Try
        

        lblMainHeader.Text = "Thank You for Your Order!"
        lblSubHeader.Text = "Thank you for your order. A confirmation email will be sent to you. We also recommend that you print this page for your records."


        Dim objUpIns As New UpdateInsert()
        Dim objWebUpIns As New WebConUpIns.UpdateInsert()
        Dim flag As Boolean
        Dim arrOrderLog(30) As String
        'Dim objGetData As New UsersGetData.Selectdata()
        Dim objUserUpIns As New UsersUpdateData.UpdateInsert()
        ' Dim ds As New DataSet
        Dim FirstName As String = String.Empty
        Dim LastName As String = String.Empty
        Dim UserName As String = String.Empty

        Dim objGet As New Selectdata()
        '  Dim dsCust As New DataSet()

        ' Dim dsOrder As New DataSet()
        Dim objData As New Selectdata()
        Dim TotalCount As Integer = 0

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

        Try

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
            btnOrderClose.Visible = True

            ''''''''''''''''''''

            'dsCust = objGet.GetCustInfo(Session("RefNumber"))
            If Not objRefresh.IsRefresh Then
                lblMainHeader.Text = "Thank You for Your Order!"
                lblSubHeader.Text = "Thank you for your order. A confirmation email will be sent to you. We also recommend that you print this page for your records."

                If hidItemNum.Value = "1" Then
                    If dsCust.Tables(0).Rows(0)("PAYMENTTYPE").ToString().ToUpper() = "INVOICE" Then
                        If dsCust.Tables(0).Rows(0)("PAYMENTTYPE").ToString().ToUpper() = "INVOICE" Then
                            lblMidHeader.Text = "Your access to Structure Assistant will be provided upon receipt of payment."
                        End If
                    Else

                        flag = True

                        If flag = True Then
                            lblMidHeader.Text = "You now have access to Structure Assistant."
                        Else

                        End If
                    End If

                ElseIf hidItemNum.Value = "2" Then
                    If dsCust.Tables(0).Rows(0)("PAYMENTTYPE").ToString().ToUpper() = "INVOICE" Then
                        If dsCust.Tables(0).Rows(0)("PAYMENTTYPE").ToString().ToUpper() = "INVOICE" Then
                            lblMidHeader.Text = "Your access to Contract Packager Knowledgebase will be provided upon receipt of payment."
                        End If
                    Else
                        dsOrder = objData.GetOrderReview(Session("RefNumber"))
                        For i = 0 To dsOrder.Tables(0).Rows.Count - 1
                            If dsOrder.Tables(0).Rows(i)("UNITCOST").ToString().Trim() = 1995 Then
                                TotalCount = TotalCount + (dsOrder.Tables(0).Rows(i)("QTY").ToString().Trim() * 3)
                            End If
                        Next

                        flag = True

                        If flag = True Then
                            lblMidHeader.Text = "You now have access to Contract Packager Knowledgebase."


                        Else
                        End If
                    End If
                Else
                    If dsCust.Tables(0).Rows(0)("PAYMENTTYPE").ToString().ToUpper() = "INVOICE" Then
                        lblMidHeader.Text = "Your study will be delivered in PDF format by email upon receipt of payment."
                    Else
                        lblMidHeader.Text = "Your study will be delivered within 24 hours in PDF format by email."
                    End If
                End If


            End If


            ''''''''''''''''''


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

  
    'Protected Function ChargePayment() As Boolean
    '    Dim PaymentFlag As Boolean = False
    '    Dim ExpDate() As String
    '    Dim Month As String = ""
    '    Dim Year As String = ""
    '    Dim curExpDate As String = ""
    '    Try
    '        ServicePointManager.SecurityProtocol = DirectCast(3072, SecurityProtocolType)

    '        Dim ApiLogin As String = System.Configuration.ConfigurationManager.AppSettings("ApiLoginL")
    '        Dim TransactionKey As String = System.Configuration.ConfigurationManager.AppSettings("TransactionKeyL")

    '        'Dim post_url As [String] = "https://test.authorize.net/gateway/transact.dll"
    '        'Dim post_url As [String] = "https://secure.authorize.net/gateway/transact.dll"
    '        Dim post_url As [String] = "https://secure2.authorize.net/gateway/transact.dll"



    '        ExpDate = Regex.Split(hidExpDate.Value, "/")
    '        If ExpDate.Length > 0 Then
    '            Month = ExpDate(0)
    '            Year = ExpDate(1).Remove(0, 2)
    '        End If
    '        'curExpDate = Month + "" + Year

    '        Dim post_values As New Dictionary(Of String, String)()

    '        post_values.Add("x_login", ApiLogin)
    '        post_values.Add("x_tran_key", TransactionKey)
    '        post_values.Add("x_delim_data", "TRUE")
    '        post_values.Add("x_delim_char", "|")
    '        post_values.Add("x_relay_response", "FALSE")

    '        post_values.Add("x_type", "AUTH_CAPTURE")
    '        post_values.Add("x_method", "CC")
    '        post_values.Add("x_card_num", lblCC.Text)
    '        'post_values.Add("x_card_num", "4007000000027")
    '        post_values.Add("x_card_code", lblAuth_Code.Text)
    '        'post_values.Add("x_card_code", "027")
    '        post_values.Add("x_exp_date", Month + "" + Year)

    '        post_values.Add("x_amount", lblGrandTotal.Text)


    '        post_values.Add("x_description", "")


    '        post_values.Add("x_first_name", hidFName.Value)
    '        post_values.Add("x_last_name", hidLName.Value)
    '        post_values.Add("x_company", hidCompanyName.Value)

    '        post_values.Add("x_address", lblAdd.Text)
    '        post_values.Add("x_state", lblState.Text)
    '        post_values.Add("x_zip", lblZipCode.Text)
    '        post_values.Add("x_city", lblCity.Text)
    '        post_values.Add("x_country", lblCntry.Text)


    '        Dim post_string As [String] = ""

    '        For Each post_value As KeyValuePair(Of String, String) In post_values
    '            post_string += post_value.Key + "=" + HttpUtility.UrlEncode(post_value.Value) + "&"
    '        Next
    '        post_string = post_string.TrimEnd("&"c)

    '        'Create an HttpWebRequest object to communicate with Authorize.net
    '        Dim objRequest As HttpWebRequest = DirectCast(WebRequest.Create(post_url), HttpWebRequest)
    '        objRequest.Method = "POST"
    '        objRequest.ContentLength = post_string.Length
    '        objRequest.ContentType = post_url

    '        'post data is sent as a stream
    '        Dim myWriter As StreamWriter = Nothing
    '        myWriter = New StreamWriter(objRequest.GetRequestStream())
    '        myWriter.Write(post_string)
    '        myWriter.Close()

    '        'returned values are returned as a stream, then read into a string
    '        Dim post_response As [String]
    '        Dim objResponse As HttpWebResponse = DirectCast(objRequest.GetResponse(), HttpWebResponse)
    '        Using responseStream As New StreamReader(objResponse.GetResponseStream())
    '            post_response = responseStream.ReadToEnd()
    '            responseStream.Close()
    '        End Using

    '        Dim details As String() = post_response.Split("|"c)

    '        If details(0) = "1" Then
    '            PaymentFlag = True
    '        Else
    '            PaymentFlag = False
    '            Session("PaidMessage") = details(3)
    '        End If

    '        Return PaymentFlag
    '        ' Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('" + post_response + "');", True)

    '    Catch ex As Exception
    '        Return PaymentFlag
    '    End Try
    'End Function
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

    Protected Sub btnOrderClose_Click(sender As Object, e As System.EventArgs) Handles btnOrderClose.Click
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "window.close();", True)

    End Sub

    Protected Sub btnOrder_Click()
        Dim objUpIns As New UpdateInsert()
        Dim objWebUpIns As New WebConUpIns.UpdateInsert()
        Dim flag As Boolean
        Dim arrOrderLog(30) As String
        Dim objGetData As New UsersGetData.Selectdata()
        Dim objUserUpIns As New UsersUpdateData.UpdateInsert()
        Dim ds As New DataSet
        Dim FirstName As String = String.Empty
        Dim LastName As String = String.Empty
        Dim UserName As String = String.Empty

        Dim objGet As New Selectdata()
        Dim dsCust As New DataSet()

        Dim dsOrder As New DataSet()
        Dim objData As New Selectdata()
        Dim TotalCount As Integer = 0
        Try
            dsCust = objGet.GetCustInfo(Session("RefNumber"))
            If Not objRefresh.IsRefresh Then
                lblMainHeader.Text = "Thank You for Your Order!"
                lblSubHeader.Text = "Thank you for your order. A confirmation email will be sent to you. We also recommend that you print this page for your records."


                If hidItemNum.Value = "1" Then
                    If dsCust.Tables(0).Rows(0)("PAYMENTTYPE").ToString().ToUpper() = "INVOICE" Then
                        flag = False
                        objUpIns.SalesMail(Session("RefNumber"), Session("UserId"))
                        objUpIns.ConfirmOrderMail(Session("RefNumber"), Session("UserId"), BillAddressID, CardHAddressID, "SA")
                        objUpIns.SaleOrder(Session("RefNumber"))
                        If dsCust.Tables(0).Rows(0)("PAYMENTTYPE").ToString().ToUpper() = "INVOICE" Then
                            lblMidHeader.Text = "Your access to Structure Assistant will be provided upon receipt of payment."
                        End If
                    Else
                        dsOrder = objData.GetOrderReview(Session("RefNumber"))

                        For i = 0 To dsOrder.Tables(0).Rows.Count - 1
                            If dsOrder.Tables(0).Rows(i)("UNITCOST").ToString().Trim() = 10000 Then
                                TotalCount = TotalCount + (dsOrder.Tables(0).Rows(i)("QTY").ToString().Trim() * 10)
                            ElseIf dsOrder.Tables(0).Rows(i)("UNITCOST").ToString().Trim() = 5000 Then
                                TotalCount = TotalCount + (dsOrder.Tables(0).Rows(i)("QTY").ToString().Trim() * 3)
                            ElseIf dsOrder.Tables(0).Rows(i)("UNITCOST").ToString().Trim() = 2500 Then
                                TotalCount = TotalCount + (dsOrder.Tables(0).Rows(i)("QTY").ToString().Trim() * 1)
                            End If
                        Next

                        'flag =ChargePayment()
                        flag = True

                        If flag = True Then
                            lblMidHeader.Text = "You now have access to Structure Assistant."
                            ds = objGetData.GetUserDetails(Session("UserId").ToString())
                            If ds.Tables(0).Rows(0).Item("LICENSEID").ToString() = "" Or ds.Tables(0).Rows(0).Item("LICENSEID").ToString() = "0" Then
                                FirstName = ds.Tables(0).Rows(0).Item("FIRSTNAME").ToString().Substring(0, If(ds.Tables(0).Rows(0).Item("FIRSTNAME").ToString().Length >= 1, 1, ds.Tables(0).Rows(0).Item("FIRSTNAME").ToString().Length))
                                LastName = ds.Tables(0).Rows(0).Item("LASTNAME").ToString().Substring(0, If(ds.Tables(0).Rows(0).Item("LASTNAME").ToString().Length >= 1, 1, ds.Tables(0).Rows(0).Item("LASTNAME").ToString().Length))
                                UserName = FirstName.ToLower() + LastName.ToLower()
                                objUserUpIns.AddLicenseToUser(UserName, Session("UserId").ToString())
                                'Changes Started for SA License Admin Tool
                                objUserUpIns.InsertServicesDetails(Session("RefNumber").ToString(), Session("UserId").ToString(), TotalCount, "SA")
                                objUserUpIns.InsertUserAdmin(Session("UserId").ToString(), "SA")
                                'Changes Ended for SA License Admin Tool
                            Else
                                objUserUpIns.EditLicenseToUser(Session("UserId").ToString())
                                'Changes Started for SA License Admin Tool
                                objUserUpIns.InsertServicesDetails(Session("RefNumber").ToString(), Session("UserId").ToString(), TotalCount, "SA")
                                objUserUpIns.InsertUserAdmin(Session("UserId").ToString(), "SA")
                                'Changes Ended for SA License Admin Tool
                            End If
                            objUpIns.SalesMail(Session("RefNumber"), Session("UserId"))
                            objUpIns.ConfirmOrderMail(Session("RefNumber"), Session("UserId"), BillAddressID, CardHAddressID, "SA")
                            objUpIns.SaleOrder(Session("RefNumber"))
                            Session("Back") = "Secure"
                        Else
                            Response.Redirect("OrderError.aspx")
                        End If
                    End If

                ElseIf hidItemNum.Value = "2" Then
                    If dsCust.Tables(0).Rows(0)("PAYMENTTYPE").ToString().ToUpper() = "INVOICE" Then
                        flag = False
                        objUpIns.SalesMail(Session("RefNumber"), Session("UserId"))
                        objUpIns.ConfirmOrderMail(Session("RefNumber"), Session("UserId"), BillAddressID, CardHAddressID, "KBCOPK")
                        objUpIns.SaleOrder(Session("RefNumber"))
                        If dsCust.Tables(0).Rows(0)("PAYMENTTYPE").ToString().ToUpper() = "INVOICE" Then
                            lblMidHeader.Text = "Your access to Contract Packager Knowledgebase will be provided upon receipt of payment."
                        End If
                    Else
                        dsOrder = objData.GetOrderReview(Session("RefNumber"))
                        For i = 0 To dsOrder.Tables(0).Rows.Count - 1
                            If dsOrder.Tables(0).Rows(i)("UNITCOST").ToString().Trim() = 1995 Then
                                TotalCount = TotalCount + (dsOrder.Tables(0).Rows(i)("QTY").ToString().Trim() * 3)
                            End If
                        Next

                        'flag =ChargePayment()

                        flag = True

                        If flag = True Then
                            lblMidHeader.Text = "You now have access to Contract Packager Knowledgebase."
                            ds = objGetData.GetUserDetails(Session("UserId").ToString())
                            If ds.Tables(0).Rows(0).Item("LICENSEID").ToString() = "" Or ds.Tables(0).Rows(0).Item("LICENSEID").ToString() = "0" Then
                                FirstName = ds.Tables(0).Rows(0).Item("FIRSTNAME").ToString().Substring(0, If(ds.Tables(0).Rows(0).Item("FIRSTNAME").ToString().Length >= 1, 1, ds.Tables(0).Rows(0).Item("FIRSTNAME").ToString().Length))
                                LastName = ds.Tables(0).Rows(0).Item("LASTNAME").ToString().Substring(0, If(ds.Tables(0).Rows(0).Item("LASTNAME").ToString().Length >= 1, 1, ds.Tables(0).Rows(0).Item("LASTNAME").ToString().Length))
                                UserName = FirstName.ToLower() + LastName.ToLower()
                                objUserUpIns.AddLicenseUserServ(UserName, Session("UserId").ToString(), "KBCOPK")
                                'Changes Started for Contract Packager License Admin Tool
                                objUserUpIns.InsertServDetails(Session("RefNumber").ToString(), Session("UserId").ToString(), TotalCount, "KBCOPK")
                                'objUserUpIns.InsertUserAdmin(Session("UserId").ToString(), "KBCOPK")
                                'Changes Ended for Contract Packager License Admin Tool
                            Else
                                objUserUpIns.EditLicenseUserServ(Session("UserId").ToString(), "KBCOPK")
                                'Changes Started for SA License Admin Tool
                                objUserUpIns.InsertServDetails(Session("RefNumber").ToString(), Session("UserId").ToString(), TotalCount, "KBCOPK")
                                'objUserUpIns.InsertUserAdmin(Session("UserId").ToString(), "KBCOPK")
                                'Changes Ended for SA License Admin Tool
                            End If
                            objUserUpIns.AddEditUserCountries(Session("UserId"))
                            objUpIns.SalesMail(Session("RefNumber"), Session("UserId"))
                            objUpIns.ConfirmOrderMail(Session("RefNumber"), Session("UserId"), BillAddressID, CardHAddressID, "KBCOPK")
                            objUpIns.SaleOrder(Session("RefNumber"))
                            Session("Back") = "Secure"
                        Else
                            Response.Redirect("OrderError.aspx")
                        End If
                    End If
                Else

                    objUpIns.SalesMail(Session("RefNumber"), Session("UserId"))
                    objUpIns.ConfirmOrderMail(Session("RefNumber"), Session("UserId"), BillAddressID, CardHAddressID, "STUDY")
                    objUpIns.SaleOrder(Session("RefNumber"))
                    If dsCust.Tables(0).Rows(0)("PAYMENTTYPE").ToString().ToUpper() = "INVOICE" Then
                        lblMidHeader.Text = "Your study will be delivered in PDF format by email upon receipt of payment."
                    Else
                        lblMidHeader.Text = "Your study will be delivered within 24 hours in PDF format by email."
                    End If
                End If

                'btnOrder.Visible = False
                btnOrderClose.Visible = True
                'Log for Ordering
                Try
                    objUpIns.UpdateOrderLog(Session("RefNumber"), Session("userId"), Session("UserName"))
                Catch ex As Exception
                End Try

                Session("RefNumber") = ""
                Session("GrantTotal") = ""
                Session("FConFid") = Nothing

                'Response.Redirect("ConfirmOrderPage.aspx?BillId=" + Request.QueryString("BillId").ToString() + "&ShipId=" + Request.QueryString("ShipId").ToString() + "&CardHID=" + Request.QueryString("CardHID").ToString())
                'Page.Response.Cache.SetCacheability(HttpCacheability.NoCache)


            End If
        Catch ex As Exception
            ErrorLable.Text = "Error:btnOrder_Click" + ex.Message.ToString()
        End Try
    End Sub

End Class
