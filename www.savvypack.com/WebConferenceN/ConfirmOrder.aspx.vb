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
Partial Class WebConferenceN_ConfirmOrder
    Inherits System.Web.UI.Page
#Region "Get Set Variables"
    Dim _lErrorLble As Label
    Dim _ctlContentPlaceHolder As ContentPlaceHolder
    Dim _billAddressId As String
    Dim _ShipAddressId As String
    Dim _CardHAddressID As String
    Dim _AttAddressID1 As String
    Dim _AttAddressID2 As String
    Dim _AttAddressID3 As String
    Dim _AttAddressID4 As String
    Dim _AttAddressID5 As String
    Dim _AttAddressID6 As String
    Dim _AttAddressID7 As String
    Dim _AttAddressID8 As String
    Dim _AttAddressID9 As String
    Dim _AttAddressID10 As String

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
            _billAddressId = value
        End Set
    End Property
    Public Property AttAddressID1() As String
        Get
            Return _AttAddressID1
        End Get
        Set(ByVal value As String)
            Dim obj As New CryptoHelper
            _AttAddressID1 = value
        End Set
    End Property
    Public Property AttAddressID2() As String
        Get
            Return _AttAddressID2
        End Get
        Set(ByVal value As String)
            Dim obj As New CryptoHelper
            _AttAddressID2 = value
        End Set
    End Property
    Public Property AttAddressID3() As String
        Get
            Return _AttAddressID3
        End Get
        Set(ByVal value As String)
            Dim obj As New CryptoHelper
            _AttAddressID3 = value
        End Set
    End Property
    Public Property AttAddressID4() As String
        Get
            Return _AttAddressID4
        End Get
        Set(ByVal value As String)
            Dim obj As New CryptoHelper
            _AttAddressID4 = value
        End Set
    End Property
    Public Property AttAddressID5() As String
        Get
            Return _AttAddressID5
        End Get
        Set(ByVal value As String)
            Dim obj As New CryptoHelper
            _AttAddressID5 = value
        End Set
    End Property
    Public Property AttAddressID6() As String
        Get
            Return _AttAddressID6
        End Get
        Set(ByVal value As String)
            Dim obj As New CryptoHelper
            _AttAddressID6 = value
        End Set
    End Property
    Public Property AttAddressID7() As String
        Get
            Return _AttAddressID7
        End Get
        Set(ByVal value As String)
            Dim obj As New CryptoHelper
            _AttAddressID7 = value
        End Set
    End Property
    Public Property AttAddressID8() As String
        Get
            Return _AttAddressID8
        End Get
        Set(ByVal value As String)
            Dim obj As New CryptoHelper
            _AttAddressID8 = value
        End Set
    End Property
    Public Property AttAddressID9() As String
        Get
            Return _AttAddressID9
        End Get
        Set(ByVal value As String)
            Dim obj As New CryptoHelper
            _AttAddressID9 = value
        End Set
    End Property
    Public Property AttAddressID10() As String
        Get
            Return _AttAddressID10
        End Get
        Set(ByVal value As String)
            Dim obj As New CryptoHelper
            _AttAddressID10 = value
        End Set
    End Property
    Public Property CardHAddressID() As String
        Get
            Return _CardHAddressID
        End Get
        Set(ByVal value As String)
            Dim obj As New CryptoHelper
            _CardHAddressID = value
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
        ctlContentPlaceHolder = Page.Master.FindControl("Econ1ContentPlaceHolder")
    End Sub

#End Region

#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_WEBCONFERENCEN_CONFIRMORDER")
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        objRefresh.Render(Page)
    End Sub

#End Region


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objGetData As New ShopGetData.Selectdata()
        Dim objWebCGetData As New WebConf.Selectdata()
        Dim objGetUserData As New UsersGetData.Selectdata()
        Dim ds As New DataSet()
        Dim dsWebC As New DataSet()
        Dim i As Integer
        Dim cnt As Integer = 0
        Dim LogInUserAddID As Integer
        Try
            If Session("WRefNumber") = "" Then
                'Response.Redirect("~/WebConferenceN/WebConf.aspx", False)
 Response.Redirect("~/Index.aspx", False)
            End If

            If Session("Back") = Nothing And Session("SBack") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("~/Universal_loginN/Pages/ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "", False)
            End If

            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            GetMasterPageControls()

            LogInUserAddID = objGetUserData.GetUserAddressIDByHeadID("Login User", Session("UserId"))
            hdnUserID.Value = LogInUserAddID

            dsWebC = objWebCGetData.GetWebConfAttDetails(Session("WRefNumber"))
            If dsWebC.Tables(0).Rows.Count > 0 Then
                AttAddressID1 = 0
                AttAddressID2 = 0
                AttAddressID3 = 0
                AttAddressID4 = 0
                AttAddressID5 = 0
                AttAddressID6 = 0
                AttAddressID7 = 0
                AttAddressID8 = 0
                AttAddressID9 = 0
                AttAddressID10 = 0
                For i = 0 To dsWebC.Tables(0).Rows.Count - 1
                    If i = 0 Then
                        AttAddressID1 = dsWebC.Tables(0).Rows(0).Item("ADDRESSID").ToString()
                        hdnShp1.Value = AttAddressID1
                    ElseIf i = 1 Then
                        AttAddressID2 = dsWebC.Tables(0).Rows(1).Item("ADDRESSID").ToString()
                        hdnShp2.Value = AttAddressID2
                    ElseIf i = 2 Then
                        AttAddressID3 = dsWebC.Tables(0).Rows(2).Item("ADDRESSID").ToString()
                        hdnShp3.Value = AttAddressID3
                    ElseIf i = 3 Then
                        AttAddressID4 = dsWebC.Tables(0).Rows(3).Item("ADDRESSID").ToString()
                        hdnShp4.Value = AttAddressID4
                    ElseIf i = 4 Then
                        AttAddressID5 = dsWebC.Tables(0).Rows(4).Item("ADDRESSID").ToString()
                        hdnShp5.Value = AttAddressID5
                    ElseIf i = 5 Then
                        AttAddressID6 = dsWebC.Tables(0).Rows(5).Item("ADDRESSID").ToString()
                        hdnShp6.Value = AttAddressID6
                    ElseIf i = 6 Then
                        AttAddressID7 = dsWebC.Tables(0).Rows(6).Item("ADDRESSID").ToString()
                        hdnShp7.Value = AttAddressID7
                    ElseIf i = 7 Then
                        AttAddressID8 = dsWebC.Tables(0).Rows(7).Item("ADDRESSID").ToString()
                        hdnShp8.Value = AttAddressID8
                    ElseIf i = 8 Then
                        AttAddressID9 = dsWebC.Tables(0).Rows(8).Item("ADDRESSID").ToString()
                        hdnShp9.Value = AttAddressID9
                    ElseIf i = 9 Then
                        AttAddressID10 = dsWebC.Tables(0).Rows(9).Item("ADDRESSID").ToString()
                        hdnShp10.Value = AttAddressID10
                    End If
                Next
            End If

            If Not IsPostBack Then
                hidlnkEdit.Value = "Enable"
            Else
                hidlnkEdit.Value = "Disable"
            End If

            If Session("FConType").ToString() <> "Free" Then
                lblHeading.Text = "Purchaser Address"
                ds = objGetData.GetOrderReviewAShop(Session("WRefNumber"))
                If ds.Tables(0).Rows.Count > 0 Then
                    BillAddressID = ds.Tables(0).Rows(0).Item("BILLTOID").ToString()
                    hdnBill.Value = BillAddressID
                    CardHAddressID = ds.Tables(0).Rows(0).Item("CARDHID").ToString()
                End If
                HideBillCardDetails(False)
                GetPageDetails()
                hdnWReffNumber.Value = Session("WRefNumber")
            Else
                lblHeading.Text = "Registrar Address"
                HideBillCardDetails(True)
                GetPageDetailsFree()
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
        Dim subTotal As Double
        Try
            ds = objGetUserData.GetUserDetails(Session("UserId"))
            dsOrder = objGetData.GetOrderReview(Session("WRefNumber"))
            dsCust = objGetData.GetCustInfo(Session("WRefNumber"))
            dsBill = objGetUserData.GetBillToShipToUserDetailsByAddID(BillAddressID)

            dsCardH = objGetUserData.GetBillToShipToUserDetailsByAddID(CardHAddressID)
            dsTax = objGetData.GetTax()

            'Order Review
            rptOrderReview.DataSource = dsOrder
            rptOrderReview.DataBind()

            'IF THE PATMENT TYPE IS INVOICE 
            If dsCust.Tables(0).Rows(0)("PAYMENTTYPE").ToString().ToUpper() = "INVOICE" Then
                lblMainHeader.Text = "Invoice Request"
                lblSubHeader.Text = "Please confirm the information shown below."
                btnOrder.Text = "Request Invoice"
                HideShowCardDetails(False)
            Else
                lblMainHeader.Text = "Confirm Order"
                lblSubHeader.Text = "Please confirm the information shown below."
                btnOrder.Text = "Charge Your Card"
                HideShowCardDetails(True)
            End If
            'TO DISPLAY IN TABLE FORMAT
            For Each DataItem As RepeaterItem In rptOrderReview.Items
                Dim lblUnitCost As New Label()
                Dim lblSubTotal As New Label()
                Dim lblTotal As New Label()
                Dim lblSTax As New Label()
                Dim lblShipCost As New Label()
                If DataItem.ItemType = ListItemType.Item Or DataItem.ItemType = ListItemType.AlternatingItem Then
                    lblUnitCost = DataItem.FindControl("lblUnitCost")
                    lblSubTotal = DataItem.FindControl("lblSubTotal")
                    lblTotal = DataItem.FindControl("lblTotal")
                    lblSTax = DataItem.FindControl("lblSTax")
                    lblShipCost = DataItem.FindControl("lblShipCost")

                    lblUnitCost.Text = FormatNumber(dsOrder.Tables(0).Rows(DataItem.ItemIndex).Item("UNITCOST").ToString(), 2)
                    lblSubTotal.Text = FormatNumber(dsOrder.Tables(0).Rows(DataItem.ItemIndex).Item("SUBTOTAL").ToString(), 2)
                    subTotal = subTotal + FormatNumber(dsOrder.Tables(0).Rows(DataItem.ItemIndex).Item("SUBTOTAL").ToString(), 2)
                    lblSTax.Text = FormatNumber(dsOrder.Tables(0).Rows(DataItem.ItemIndex).Item("SALESTAX").ToString(), 2)

                    lblTotal.Text = FormatNumber(dsOrder.Tables(0).Rows(DataItem.ItemIndex).Item("TOTAL").ToString(), 2)
                    lblShipCost.Text = FormatNumber(dsOrder.Tables(0).Rows(DataItem.ItemIndex).Item("SHIPPINGCOST").ToString(), 2)
                    shippingCost = shippingCost + FormatNumber(dsOrder.Tables(0).Rows(DataItem.ItemIndex).Item("SHIPPINGCOST").ToString(), 2)
                    If dsCust.Tables(0).Rows(0)("PAYMENTTYPE").ToString().ToUpper() = "INVOICE" Then
                    Else

                    End If
                    taxAmount = taxAmount + CDbl(lblSTax.Text)
                    total = total + CDbl(lblTotal.Text)
                End If
            Next

            Dim dsWeb As New DataSet()
            Dim objGetWebData As New WebConf.Selectdata()
            dsWeb = objGetWebData.GetWebConfMailDetailsById(Session("FConFid"))
            lblCDate.Text = dsWeb.Tables(0).Rows(0).Item("CONFDATE")
            lblCTime.Text = dsWeb.Tables(0).Rows(0).Item("CONFTIME")
            lblCTopic.Text = dsWeb.Tables(0).Rows(0).Item("CONFTOPIC")

            lblUNameText.Text = dsWeb.Tables(0).Rows(0).Item("CONFUNAMETEXT")
            lblPWDtext.Text = dsWeb.Tables(0).Rows(0).Item("CONFPWDTEXT")
            lblUName.Text = dsWeb.Tables(0).Rows(0).Item("CONFID")
            lblUPWD.Text = dsWeb.Tables(0).Rows(0).Item("CONFKEY")
            lblPhn.Text = dsWeb.Tables(0).Rows(0).Item("CONFPHONE")
            lblCCode.Text = dsWeb.Tables(0).Rows(0).Item("CONFACCESSCODE")

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

                lblCC.Text = dsCust.Tables(0).Rows(0).Item("CARDNUMBER").ToString()
                lblCCExp.Text = dsCust.Tables(0).Rows(0).Item("CARDEXP").ToString()
                lblCCType.Text = dsCust.Tables(0).Rows(0).Item("CARDTYPE").ToString()                
                lblAuth_Code.Text = dsCust.Tables(0).Rows(0).Item("AUTH_CODE").ToString()
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

                'Setting Attendee Address

                If AttAddressID1 <> 0 Then
                    trAt1.Visible = True
                    dsShip = objGetUserData.GetBillToShipToUserDetailsByAddID(AttAddressID1)
                    lblAttUser1.Text = dsShip.Tables(0).Rows(0).Item("FULLNAME").ToString()
                    lblAttEmail1.Text = dsShip.Tables(0).Rows(0).Item("EMAILADDRESS").ToString()
                    lblAttCompNm1.Text = dsShip.Tables(0).Rows(0).Item("COMPANYNAME").ToString()
                    lblAttphne1.Text = dsShip.Tables(0).Rows(0).Item("PHONENUMBER").ToString()
                    lblAttFax1.Text = dsShip.Tables(0).Rows(0).Item("FAXNUMBER").ToString()
                    lblAttAdd1.Text = dsShip.Tables(0).Rows(0).Item("STREETADDRESS1").ToString() + " " + dsShip.Tables(0).Rows(0).Item("STREETADDRESS2").ToString()
                    lblAttCity1.Text = dsShip.Tables(0).Rows(0).Item("CITY").ToString()
                    lblAttState1.Text = dsShip.Tables(0).Rows(0).Item("STATE").ToString()
                    lblAttZipCode1.Text = dsShip.Tables(0).Rows(0).Item("ZIPCODE").ToString()
                    lblAttCntry1.Text = dsShip.Tables(0).Rows(0).Item("COUNTRYDES").ToString()
                End If
                If AttAddressID2 <> 0 Then
                    trAt2.Visible = True
                    dsShip = objGetUserData.GetBillToShipToUserDetailsByAddID(AttAddressID2)
                    lblAttUser2.Text = dsShip.Tables(0).Rows(0).Item("FULLNAME").ToString()
                    lblAttEmail2.Text = dsShip.Tables(0).Rows(0).Item("EMAILADDRESS").ToString()
                    lblAttCompNm2.Text = dsShip.Tables(0).Rows(0).Item("COMPANYNAME").ToString()
                    lblAttphne2.Text = dsShip.Tables(0).Rows(0).Item("PHONENUMBER").ToString()
                    lblAttFax2.Text = dsShip.Tables(0).Rows(0).Item("FAXNUMBER").ToString()
                    lblAttAdd2.Text = dsShip.Tables(0).Rows(0).Item("STREETADDRESS1").ToString() + " " + dsShip.Tables(0).Rows(0).Item("STREETADDRESS2").ToString()
                    lblAttCity2.Text = dsShip.Tables(0).Rows(0).Item("CITY").ToString()
                    lblAttState2.Text = dsShip.Tables(0).Rows(0).Item("STATE").ToString()
                    lblAttZipCode2.Text = dsShip.Tables(0).Rows(0).Item("ZIPCODE").ToString()
                    lblAttCntry2.Text = dsShip.Tables(0).Rows(0).Item("COUNTRYDES").ToString()
                End If
                If AttAddressID3 <> 0 Then
                    trAt3.Visible = True
                    dsShip = objGetUserData.GetBillToShipToUserDetailsByAddID(AttAddressID3)
                    lblAttUser3.Text = dsShip.Tables(0).Rows(0).Item("FULLNAME").ToString()
                    lblAttEmail3.Text = dsShip.Tables(0).Rows(0).Item("EMAILADDRESS").ToString()
                    lblAttCompNm3.Text = dsShip.Tables(0).Rows(0).Item("COMPANYNAME").ToString()
                    lblAttphne3.Text = dsShip.Tables(0).Rows(0).Item("PHONENUMBER").ToString()
                    lblAttFax3.Text = dsShip.Tables(0).Rows(0).Item("FAXNUMBER").ToString()
                    lblAttAdd3.Text = dsShip.Tables(0).Rows(0).Item("STREETADDRESS1").ToString() + " " + dsShip.Tables(0).Rows(0).Item("STREETADDRESS2").ToString()
                    lblAttCity3.Text = dsShip.Tables(0).Rows(0).Item("CITY").ToString()
                    lblAttState3.Text = dsShip.Tables(0).Rows(0).Item("STATE").ToString()
                    lblAttZipCode3.Text = dsShip.Tables(0).Rows(0).Item("ZIPCODE").ToString()
                    lblAttCntry3.Text = dsShip.Tables(0).Rows(0).Item("COUNTRYDES").ToString()
                End If
                If AttAddressID4 <> 0 Then
                    trAt4.Visible = True
                    dsShip = objGetUserData.GetBillToShipToUserDetailsByAddID(AttAddressID4)
                    lblAttUser4.Text = dsShip.Tables(0).Rows(0).Item("FULLNAME").ToString()
                    lblAttEmail4.Text = dsShip.Tables(0).Rows(0).Item("EMAILADDRESS").ToString()
                    lblAttCompNm4.Text = dsShip.Tables(0).Rows(0).Item("COMPANYNAME").ToString()
                    lblAttphne4.Text = dsShip.Tables(0).Rows(0).Item("PHONENUMBER").ToString()
                    lblAttFax4.Text = dsShip.Tables(0).Rows(0).Item("FAXNUMBER").ToString()
                    lblAttAdd4.Text = dsShip.Tables(0).Rows(0).Item("STREETADDRESS1").ToString() + " " + dsShip.Tables(0).Rows(0).Item("STREETADDRESS2").ToString()
                    lblAttCity4.Text = dsShip.Tables(0).Rows(0).Item("CITY").ToString()
                    lblAttState4.Text = dsShip.Tables(0).Rows(0).Item("STATE").ToString()
                    lblAttZipCode4.Text = dsShip.Tables(0).Rows(0).Item("ZIPCODE").ToString()
                    lblAttCntry4.Text = dsShip.Tables(0).Rows(0).Item("COUNTRYDES").ToString()
                End If
                If AttAddressID5 <> 0 Then
                    trAt5.Visible = True
                    dsShip = objGetUserData.GetBillToShipToUserDetailsByAddID(AttAddressID5)
                    lblAttUser5.Text = dsShip.Tables(0).Rows(0).Item("FULLNAME").ToString()
                    lblAttEmail5.Text = dsShip.Tables(0).Rows(0).Item("EMAILADDRESS").ToString()
                    lblAttCompNm5.Text = dsShip.Tables(0).Rows(0).Item("COMPANYNAME").ToString()
                    lblAttphne5.Text = dsShip.Tables(0).Rows(0).Item("PHONENUMBER").ToString()
                    lblAttFax5.Text = dsShip.Tables(0).Rows(0).Item("FAXNUMBER").ToString()
                    lblAttAdd5.Text = dsShip.Tables(0).Rows(0).Item("STREETADDRESS1").ToString() + " " + dsShip.Tables(0).Rows(0).Item("STREETADDRESS2").ToString()
                    lblAttCity5.Text = dsShip.Tables(0).Rows(0).Item("CITY").ToString()
                    lblAttState5.Text = dsShip.Tables(0).Rows(0).Item("STATE").ToString()
                    lblAttZipCode5.Text = dsShip.Tables(0).Rows(0).Item("ZIPCODE").ToString()
                    lblAttCntry5.Text = dsShip.Tables(0).Rows(0).Item("COUNTRYDES").ToString()
                End If
                If AttAddressID6 <> 0 Then
                    trAt6.Visible = True
                    dsShip = objGetUserData.GetBillToShipToUserDetailsByAddID(AttAddressID6)
                    lblAttUser6.Text = dsShip.Tables(0).Rows(0).Item("FULLNAME").ToString()
                    lblAttEmail6.Text = dsShip.Tables(0).Rows(0).Item("EMAILADDRESS").ToString()
                    lblAttCompNm6.Text = dsShip.Tables(0).Rows(0).Item("COMPANYNAME").ToString()
                    lblAttphne6.Text = dsShip.Tables(0).Rows(0).Item("PHONENUMBER").ToString()
                    lblAttFax6.Text = dsShip.Tables(0).Rows(0).Item("FAXNUMBER").ToString()
                    lblAttAdd6.Text = dsShip.Tables(0).Rows(0).Item("STREETADDRESS1").ToString() + " " + dsShip.Tables(0).Rows(0).Item("STREETADDRESS2").ToString()
                    lblAttCity6.Text = dsShip.Tables(0).Rows(0).Item("CITY").ToString()
                    lblAttState6.Text = dsShip.Tables(0).Rows(0).Item("STATE").ToString()
                    lblAttZipCode6.Text = dsShip.Tables(0).Rows(0).Item("ZIPCODE").ToString()
                    lblAttCntry6.Text = dsShip.Tables(0).Rows(0).Item("COUNTRYDES").ToString()
                End If
                If AttAddressID7 <> 0 Then
                    trAt7.Visible = True
                    dsShip = objGetUserData.GetBillToShipToUserDetailsByAddID(AttAddressID7)
                    lblAttUser7.Text = dsShip.Tables(0).Rows(0).Item("FULLNAME").ToString()
                    lblAttEmail7.Text = dsShip.Tables(0).Rows(0).Item("EMAILADDRESS").ToString()
                    lblAttCompNm7.Text = dsShip.Tables(0).Rows(0).Item("COMPANYNAME").ToString()
                    lblAttphne7.Text = dsShip.Tables(0).Rows(0).Item("PHONENUMBER").ToString()
                    lblAttFax7.Text = dsShip.Tables(0).Rows(0).Item("FAXNUMBER").ToString()
                    lblAttAdd7.Text = dsShip.Tables(0).Rows(0).Item("STREETADDRESS1").ToString() + " " + dsShip.Tables(0).Rows(0).Item("STREETADDRESS2").ToString()
                    lblAttCity7.Text = dsShip.Tables(0).Rows(0).Item("CITY").ToString()
                    lblAttState7.Text = dsShip.Tables(0).Rows(0).Item("STATE").ToString()
                    lblAttZipCode7.Text = dsShip.Tables(0).Rows(0).Item("ZIPCODE").ToString()
                    lblAttCntry7.Text = dsShip.Tables(0).Rows(0).Item("COUNTRYDES").ToString()
                End If
                If AttAddressID8 <> 0 Then
                    trAt8.Visible = True
                    dsShip = objGetUserData.GetBillToShipToUserDetailsByAddID(AttAddressID8)
                    lblAttUser8.Text = dsShip.Tables(0).Rows(0).Item("FULLNAME").ToString()
                    lblAttEmail8.Text = dsShip.Tables(0).Rows(0).Item("EMAILADDRESS").ToString()
                    lblAttCompNm8.Text = dsShip.Tables(0).Rows(0).Item("COMPANYNAME").ToString()
                    lblAttphne8.Text = dsShip.Tables(0).Rows(0).Item("PHONENUMBER").ToString()
                    lblAttFax8.Text = dsShip.Tables(0).Rows(0).Item("FAXNUMBER").ToString()
                    lblAttAdd8.Text = dsShip.Tables(0).Rows(0).Item("STREETADDRESS1").ToString() + " " + dsShip.Tables(0).Rows(0).Item("STREETADDRESS2").ToString()
                    lblAttCity8.Text = dsShip.Tables(0).Rows(0).Item("CITY").ToString()
                    lblAttState8.Text = dsShip.Tables(0).Rows(0).Item("STATE").ToString()
                    lblAttZipCode8.Text = dsShip.Tables(0).Rows(0).Item("ZIPCODE").ToString()
                    lblAttCntry8.Text = dsShip.Tables(0).Rows(0).Item("COUNTRYDES").ToString()
                End If
                If AttAddressID9 <> 0 Then
                    trAt9.Visible = True
                    dsShip = objGetUserData.GetBillToShipToUserDetailsByAddID(AttAddressID9)
                    lblAttUser9.Text = dsShip.Tables(0).Rows(0).Item("FULLNAME").ToString()
                    lblAttEmail9.Text = dsShip.Tables(0).Rows(0).Item("EMAILADDRESS").ToString()
                    lblAttCompNm9.Text = dsShip.Tables(0).Rows(0).Item("COMPANYNAME").ToString()
                    lblAttphne9.Text = dsShip.Tables(0).Rows(0).Item("PHONENUMBER").ToString()
                    lblAttFax9.Text = dsShip.Tables(0).Rows(0).Item("FAXNUMBER").ToString()
                    lblAttAdd9.Text = dsShip.Tables(0).Rows(0).Item("STREETADDRESS1").ToString() + " " + dsShip.Tables(0).Rows(0).Item("STREETADDRESS2").ToString()
                    lblAttCity9.Text = dsShip.Tables(0).Rows(0).Item("CITY").ToString()
                    lblAttState9.Text = dsShip.Tables(0).Rows(0).Item("STATE").ToString()
                    lblAttZipCode9.Text = dsShip.Tables(0).Rows(0).Item("ZIPCODE").ToString()
                    lblAttCntry9.Text = dsShip.Tables(0).Rows(0).Item("COUNTRYDES").ToString()
                End If
                If AttAddressID10 <> 0 Then
                    trAt10.Visible = True
                    dsShip = objGetUserData.GetBillToShipToUserDetailsByAddID(AttAddressID10)
                    lblAttUser10.Text = dsShip.Tables(0).Rows(0).Item("FULLNAME").ToString()
                    lblAttEmail10.Text = dsShip.Tables(0).Rows(0).Item("EMAILADDRESS").ToString()
                    lblAttCompNm10.Text = dsShip.Tables(0).Rows(0).Item("COMPANYNAME").ToString()
                    lblAttphne10.Text = dsShip.Tables(0).Rows(0).Item("PHONENUMBER").ToString()
                    lblAttFax10.Text = dsShip.Tables(0).Rows(0).Item("FAXNUMBER").ToString()
                    lblAttAdd10.Text = dsShip.Tables(0).Rows(0).Item("STREETADDRESS1").ToString() + " " + dsShip.Tables(0).Rows(0).Item("STREETADDRESS2").ToString()
                    lblAttCity10.Text = dsShip.Tables(0).Rows(0).Item("CITY").ToString()
                    lblAttState10.Text = dsShip.Tables(0).Rows(0).Item("STATE").ToString()
                    lblAttZipCode10.Text = dsShip.Tables(0).Rows(0).Item("ZIPCODE").ToString()
                    lblAttCntry10.Text = dsShip.Tables(0).Rows(0).Item("COUNTRYDES").ToString()
                End If

               
                'Setting Card Holder Address
                'If dsCardH.Tables(0).Rows.Count > 0 Then
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

            If hidlnkEdit.Value.ToString() <> "Enable" Then
                lnkHEdit.Visible = False
                lnkBillTo.Visible = False
                lnkCardEdit.Visible = False
                lnkShipTo1.Visible = False
                lnkShipTo2.Visible = False
                lnkShipTo3.Visible = False
                lnkShipTo4.Visible = False
                lnkShipTo5.Visible = False
                lnkShipTo6.Visible = False
                lnkShipTo7.Visible = False
                lnkShipTo8.Visible = False
                lnkShipTo9.Visible = False
                lnkShipTo10.Visible = False
            Else
                lnkHEdit.Visible = True
                lnkBillTo.Visible = True
                lnkCardEdit.Visible = True
                lnkShipTo1.Visible = True
                lnkShipTo2.Visible = True
                lnkShipTo3.Visible = True
                lnkShipTo4.Visible = True
                lnkShipTo5.Visible = True
                lnkShipTo6.Visible = True
                lnkShipTo7.Visible = True
                lnkShipTo8.Visible = True
                lnkShipTo9.Visible = True
                lnkShipTo10.Visible = True
            End If

            lblRefNumber.Text = Session("WRefNumber")
            lblAmt.Text = FormatNumber(subTotal, 2)

            lblTax.Text = FormatNumber(taxAmount, 2)
            lblGrandTotal.Text = FormatNumber(total, 2)
            lblShippingCost.Text = FormatNumber(shippingCost, 2)

        Catch ex As Exception
            ErrorLable.Text = "Error:GetPageDetails" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetPageDetailsFree()
        Dim objGetData As New WebConf.Selectdata()
        Dim objUpIns As New WebConUpIns.UpdateInsert()
        Dim ds As New DataSet()
        Dim StrSql As String = String.Empty
        Dim objWebCGetData As New WebConf.Selectdata()
        Dim dsWebC As New DataSet()
        Dim AttAddressID1 As String = "0"
        Dim AttAddressID2 As String = "0"
        Dim AttAddressID3 As String = "0"
        Dim AttAddressID4 As String = "0"
        Dim AttAddressID5 As String = "0"
        Dim AttAddressID6 As String = "0"
        Dim AttAddressID7 As String = "0"
        Dim AttAddressID8 As String = "0"
        Dim AttAddressID9 As String = "0"
        Dim AttAddressID10 As String = "0"
        Dim ArrAtt(10) As String
        Dim dsOrder As New DataSet()
        Dim objGetOrderData As New Selectdata()
        Try

            lblMainHeader.Text = "Confirm Registration"
            lblSubHeader.Text = "Please confirm the information shown below."

            lblRefNumber.Text = Session("WRefNumber")
            lblAmt.Text = "0.0"

            lblTax.Text = "0.0"
            lblGrandTotal.Text = "0.0"
            lblShippingCost.Text = "0.0"

            dsWebC = objWebCGetData.GetWebConfAttDetails(Session("WRefNumber"))
            If dsWebC.Tables(0).Rows.Count > 0 Then
                For i = 0 To dsWebC.Tables(0).Rows.Count - 1
                    If i = 0 Then
                        AttAddressID1 = dsWebC.Tables(0).Rows(0).Item("ADDRESSID").ToString()
                        hdnShp1.Value = AttAddressID1
                    ElseIf i = 1 Then
                        AttAddressID2 = dsWebC.Tables(0).Rows(1).Item("ADDRESSID").ToString()
                        hdnShp2.Value = AttAddressID2
                    ElseIf i = 2 Then
                        AttAddressID3 = dsWebC.Tables(0).Rows(2).Item("ADDRESSID").ToString()
                        hdnShp3.Value = AttAddressID3
                    ElseIf i = 3 Then
                        AttAddressID4 = dsWebC.Tables(0).Rows(3).Item("ADDRESSID").ToString()
                        hdnShp4.Value = AttAddressID4
                    ElseIf i = 4 Then
                        AttAddressID5 = dsWebC.Tables(0).Rows(4).Item("ADDRESSID").ToString()
                        hdnShp5.Value = AttAddressID5
                    ElseIf i = 5 Then
                        AttAddressID6 = dsWebC.Tables(0).Rows(5).Item("ADDRESSID").ToString()
                        hdnShp6.Value = AttAddressID6
                    ElseIf i = 6 Then
                        AttAddressID7 = dsWebC.Tables(0).Rows(6).Item("ADDRESSID").ToString()
                        hdnShp7.Value = AttAddressID7
                    ElseIf i = 7 Then
                        AttAddressID8 = dsWebC.Tables(0).Rows(7).Item("ADDRESSID").ToString()
                        hdnShp8.Value = AttAddressID8
                    ElseIf i = 8 Then
                        AttAddressID9 = dsWebC.Tables(0).Rows(8).Item("ADDRESSID").ToString()
                        hdnShp9.Value = AttAddressID9
                    ElseIf i = 9 Then
                        AttAddressID10 = dsWebC.Tables(0).Rows(9).Item("ADDRESSID").ToString()
                        hdnShp10.Value = AttAddressID10
                    End If
                Next
                ArrAtt(0) = AttAddressID1
                ArrAtt(1) = AttAddressID2
                ArrAtt(2) = AttAddressID3
                ArrAtt(3) = AttAddressID4
                ArrAtt(4) = AttAddressID5
                ArrAtt(5) = AttAddressID6
                ArrAtt(6) = AttAddressID7
                ArrAtt(7) = AttAddressID8
                ArrAtt(8) = AttAddressID9
                ArrAtt(9) = AttAddressID10
            End If



            Dim dsUser As New DataSet()
            Dim dsWeb As New DataSet()
            Dim objGetUserData As New UsersGetData.Selectdata()
            Dim dsShip As New DataSet()
            'lblSubHeader.Text = "Thank you for registering the following people for the web conference. We will send an email to each of them confirming their registration. <br />We also recommend you print this page for your records."

            ds = objGetData.GetWebConfDetailsById(Session("FConFid"))
            dsUser = objGetUserData.GetUserDetails(Session("UserId"))
            dsWeb = objGetData.GetWebConfMailDetailsById(Session("FConFid"))

            lblCDate.Text = dsWeb.Tables(0).Rows(0).Item("CONFDATE")
            lblCTime.Text = dsWeb.Tables(0).Rows(0).Item("CONFTIME")
            lblCTopic.Text = dsWeb.Tables(0).Rows(0).Item("CONFTOPIC")

            lblUNameText.Text = dsWeb.Tables(0).Rows(0).Item("CONFUNAMETEXT")
            lblPWDtext.Text = dsWeb.Tables(0).Rows(0).Item("CONFPWDTEXT")
            lblUName.Text = dsWeb.Tables(0).Rows(0).Item("CONFID")
            lblUPWD.Text = dsWeb.Tables(0).Rows(0).Item("CONFKEY")
            lblPhn.Text = dsWeb.Tables(0).Rows(0).Item("CONFPHONE")
            lblCCode.Text = dsWeb.Tables(0).Rows(0).Item("CONFACCESSCODE")

            If dsUser.Tables(0).Rows.Count > 0 Then
                lblName.Text = dsUser.Tables(0).Rows(0).Item("PREFIX").ToString() + " " + dsUser.Tables(0).Rows(0).Item("FIRSTNAME").ToString() + " " + dsUser.Tables(0).Rows(0).Item("LASTNAME").ToString() + " "
                lblEmail.Text = dsUser.Tables(0).Rows(0).Item("EMAILADDRESS").ToString()
                lblCompName.Text = dsUser.Tables(0).Rows(0).Item("COMPANYNAME").ToString()
                lblHPhone.Text = dsUser.Tables(0).Rows(0).Item("PHONENUMBER").ToString()
                lblHFax.Text = dsUser.Tables(0).Rows(0).Item("FAXNUMBER").ToString()
                lblHStAdd.Text = dsUser.Tables(0).Rows(0).Item("STREETADDRESS1").ToString() + " " + dsUser.Tables(0).Rows(0).Item("STREETADDRESS2").ToString()
                lblHCity.Text = dsUser.Tables(0).Rows(0).Item("CITY").ToString()
                lblHState.Text = dsUser.Tables(0).Rows(0).Item("STATE").ToString()
                lblHZip.Text = dsUser.Tables(0).Rows(0).Item("ZIPCODE").ToString()
                lblHCountry.Text = dsUser.Tables(0).Rows(0).Item("COUNTRY").ToString()

                If AttAddressID1 <> 0 Then
                    trAt1.Visible = True
                    dsShip = objGetUserData.GetBillToShipToUserDetailsByAddID(AttAddressID1)
                    lblAttUser1.Text = dsShip.Tables(0).Rows(0).Item("FULLNAME").ToString()
                    lblAttEmail1.Text = dsShip.Tables(0).Rows(0).Item("EMAILADDRESS").ToString()
                    lblAttCompNm1.Text = dsShip.Tables(0).Rows(0).Item("COMPANYNAME").ToString()
                    lblAttphne1.Text = dsShip.Tables(0).Rows(0).Item("PHONENUMBER").ToString()
                    lblAttFax1.Text = dsShip.Tables(0).Rows(0).Item("FAXNUMBER").ToString()
                    lblAttAdd1.Text = dsShip.Tables(0).Rows(0).Item("STREETADDRESS1").ToString() + " " + dsShip.Tables(0).Rows(0).Item("STREETADDRESS2").ToString()
                    lblAttCity1.Text = dsShip.Tables(0).Rows(0).Item("CITY").ToString()
                    lblAttState1.Text = dsShip.Tables(0).Rows(0).Item("STATE").ToString()
                    lblAttZipCode1.Text = dsShip.Tables(0).Rows(0).Item("ZIPCODE").ToString()
                    lblAttCntry1.Text = dsShip.Tables(0).Rows(0).Item("COUNTRYDES").ToString()
                End If
                If AttAddressID2 <> 0 Then
                    trAt2.Visible = True
                    dsShip = objGetUserData.GetBillToShipToUserDetailsByAddID(AttAddressID2)
                    lblAttUser2.Text = dsShip.Tables(0).Rows(0).Item("FULLNAME").ToString()
                    lblAttEmail2.Text = dsShip.Tables(0).Rows(0).Item("EMAILADDRESS").ToString()
                    lblAttCompNm2.Text = dsShip.Tables(0).Rows(0).Item("COMPANYNAME").ToString()
                    lblAttphne2.Text = dsShip.Tables(0).Rows(0).Item("PHONENUMBER").ToString()
                    lblAttFax2.Text = dsShip.Tables(0).Rows(0).Item("FAXNUMBER").ToString()
                    lblAttAdd2.Text = dsShip.Tables(0).Rows(0).Item("STREETADDRESS1").ToString() + " " + dsShip.Tables(0).Rows(0).Item("STREETADDRESS2").ToString()
                    lblAttCity2.Text = dsShip.Tables(0).Rows(0).Item("CITY").ToString()
                    lblAttState2.Text = dsShip.Tables(0).Rows(0).Item("STATE").ToString()
                    lblAttZipCode2.Text = dsShip.Tables(0).Rows(0).Item("ZIPCODE").ToString()
                    lblAttCntry2.Text = dsShip.Tables(0).Rows(0).Item("COUNTRYDES").ToString()
                End If
                If AttAddressID3 <> 0 Then
                    trAt3.Visible = True
                    dsShip = objGetUserData.GetBillToShipToUserDetailsByAddID(AttAddressID3)
                    lblAttUser3.Text = dsShip.Tables(0).Rows(0).Item("FULLNAME").ToString()
                    lblAttEmail3.Text = dsShip.Tables(0).Rows(0).Item("EMAILADDRESS").ToString()
                    lblAttCompNm3.Text = dsShip.Tables(0).Rows(0).Item("COMPANYNAME").ToString()
                    lblAttphne3.Text = dsShip.Tables(0).Rows(0).Item("PHONENUMBER").ToString()
                    lblAttFax3.Text = dsShip.Tables(0).Rows(0).Item("FAXNUMBER").ToString()
                    lblAttAdd3.Text = dsShip.Tables(0).Rows(0).Item("STREETADDRESS1").ToString() + " " + dsShip.Tables(0).Rows(0).Item("STREETADDRESS2").ToString()
                    lblAttCity3.Text = dsShip.Tables(0).Rows(0).Item("CITY").ToString()
                    lblAttState3.Text = dsShip.Tables(0).Rows(0).Item("STATE").ToString()
                    lblAttZipCode3.Text = dsShip.Tables(0).Rows(0).Item("ZIPCODE").ToString()
                    lblAttCntry3.Text = dsShip.Tables(0).Rows(0).Item("COUNTRYDES").ToString()
                End If
                If AttAddressID4 <> 0 Then
                    trAt4.Visible = True
                    dsShip = objGetUserData.GetBillToShipToUserDetailsByAddID(AttAddressID4)
                    lblAttUser4.Text = dsShip.Tables(0).Rows(0).Item("FULLNAME").ToString()
                    lblAttEmail4.Text = dsShip.Tables(0).Rows(0).Item("EMAILADDRESS").ToString()
                    lblAttCompNm4.Text = dsShip.Tables(0).Rows(0).Item("COMPANYNAME").ToString()
                    lblAttphne4.Text = dsShip.Tables(0).Rows(0).Item("PHONENUMBER").ToString()
                    lblAttFax4.Text = dsShip.Tables(0).Rows(0).Item("FAXNUMBER").ToString()
                    lblAttAdd4.Text = dsShip.Tables(0).Rows(0).Item("STREETADDRESS1").ToString() + " " + dsShip.Tables(0).Rows(0).Item("STREETADDRESS2").ToString()
                    lblAttCity4.Text = dsShip.Tables(0).Rows(0).Item("CITY").ToString()
                    lblAttState4.Text = dsShip.Tables(0).Rows(0).Item("STATE").ToString()
                    lblAttZipCode4.Text = dsShip.Tables(0).Rows(0).Item("ZIPCODE").ToString()
                    lblAttCntry4.Text = dsShip.Tables(0).Rows(0).Item("COUNTRYDES").ToString()
                End If
                If AttAddressID5 <> 0 Then
                    trAt5.Visible = True
                    dsShip = objGetUserData.GetBillToShipToUserDetailsByAddID(AttAddressID5)
                    lblAttUser5.Text = dsShip.Tables(0).Rows(0).Item("FULLNAME").ToString()
                    lblAttEmail5.Text = dsShip.Tables(0).Rows(0).Item("EMAILADDRESS").ToString()
                    lblAttCompNm5.Text = dsShip.Tables(0).Rows(0).Item("COMPANYNAME").ToString()
                    lblAttphne5.Text = dsShip.Tables(0).Rows(0).Item("PHONENUMBER").ToString()
                    lblAttFax5.Text = dsShip.Tables(0).Rows(0).Item("FAXNUMBER").ToString()
                    lblAttAdd5.Text = dsShip.Tables(0).Rows(0).Item("STREETADDRESS1").ToString() + " " + dsShip.Tables(0).Rows(0).Item("STREETADDRESS2").ToString()
                    lblAttCity5.Text = dsShip.Tables(0).Rows(0).Item("CITY").ToString()
                    lblAttState5.Text = dsShip.Tables(0).Rows(0).Item("STATE").ToString()
                    lblAttZipCode5.Text = dsShip.Tables(0).Rows(0).Item("ZIPCODE").ToString()
                    lblAttCntry5.Text = dsShip.Tables(0).Rows(0).Item("COUNTRYDES").ToString()
                End If
                If AttAddressID6 <> 0 Then
                    trAt6.Visible = True
                    dsShip = objGetUserData.GetBillToShipToUserDetailsByAddID(AttAddressID6)
                    lblAttUser6.Text = dsShip.Tables(0).Rows(0).Item("FULLNAME").ToString()
                    lblAttEmail6.Text = dsShip.Tables(0).Rows(0).Item("EMAILADDRESS").ToString()
                    lblAttCompNm6.Text = dsShip.Tables(0).Rows(0).Item("COMPANYNAME").ToString()
                    lblAttphne6.Text = dsShip.Tables(0).Rows(0).Item("PHONENUMBER").ToString()
                    lblAttFax6.Text = dsShip.Tables(0).Rows(0).Item("FAXNUMBER").ToString()
                    lblAttAdd6.Text = dsShip.Tables(0).Rows(0).Item("STREETADDRESS1").ToString() + " " + dsShip.Tables(0).Rows(0).Item("STREETADDRESS2").ToString()
                    lblAttCity6.Text = dsShip.Tables(0).Rows(0).Item("CITY").ToString()
                    lblAttState6.Text = dsShip.Tables(0).Rows(0).Item("STATE").ToString()
                    lblAttZipCode6.Text = dsShip.Tables(0).Rows(0).Item("ZIPCODE").ToString()
                    lblAttCntry6.Text = dsShip.Tables(0).Rows(0).Item("COUNTRYDES").ToString()
                End If
                If AttAddressID7 <> 0 Then
                    trAt7.Visible = True
                    dsShip = objGetUserData.GetBillToShipToUserDetailsByAddID(AttAddressID7)
                    lblAttUser7.Text = dsShip.Tables(0).Rows(0).Item("FULLNAME").ToString()
                    lblAttEmail7.Text = dsShip.Tables(0).Rows(0).Item("EMAILADDRESS").ToString()
                    lblAttCompNm7.Text = dsShip.Tables(0).Rows(0).Item("COMPANYNAME").ToString()
                    lblAttphne7.Text = dsShip.Tables(0).Rows(0).Item("PHONENUMBER").ToString()
                    lblAttFax7.Text = dsShip.Tables(0).Rows(0).Item("FAXNUMBER").ToString()
                    lblAttAdd7.Text = dsShip.Tables(0).Rows(0).Item("STREETADDRESS1").ToString() + " " + dsShip.Tables(0).Rows(0).Item("STREETADDRESS2").ToString()
                    lblAttCity7.Text = dsShip.Tables(0).Rows(0).Item("CITY").ToString()
                    lblAttState7.Text = dsShip.Tables(0).Rows(0).Item("STATE").ToString()
                    lblAttZipCode7.Text = dsShip.Tables(0).Rows(0).Item("ZIPCODE").ToString()
                    lblAttCntry7.Text = dsShip.Tables(0).Rows(0).Item("COUNTRYDES").ToString()
                End If
                If AttAddressID8 <> 0 Then
                    trAt8.Visible = True
                    dsShip = objGetUserData.GetBillToShipToUserDetailsByAddID(AttAddressID8)
                    lblAttUser8.Text = dsShip.Tables(0).Rows(0).Item("FULLNAME").ToString()
                    lblAttEmail8.Text = dsShip.Tables(0).Rows(0).Item("EMAILADDRESS").ToString()
                    lblAttCompNm8.Text = dsShip.Tables(0).Rows(0).Item("COMPANYNAME").ToString()
                    lblAttphne8.Text = dsShip.Tables(0).Rows(0).Item("PHONENUMBER").ToString()
                    lblAttFax8.Text = dsShip.Tables(0).Rows(0).Item("FAXNUMBER").ToString()
                    lblAttAdd8.Text = dsShip.Tables(0).Rows(0).Item("STREETADDRESS1").ToString() + " " + dsShip.Tables(0).Rows(0).Item("STREETADDRESS2").ToString()
                    lblAttCity8.Text = dsShip.Tables(0).Rows(0).Item("CITY").ToString()
                    lblAttState8.Text = dsShip.Tables(0).Rows(0).Item("STATE").ToString()
                    lblAttZipCode8.Text = dsShip.Tables(0).Rows(0).Item("ZIPCODE").ToString()
                    lblAttCntry8.Text = dsShip.Tables(0).Rows(0).Item("COUNTRYDES").ToString()
                End If
                If AttAddressID9 <> 0 Then
                    trAt9.Visible = True
                    dsShip = objGetUserData.GetBillToShipToUserDetailsByAddID(AttAddressID9)
                    lblAttUser9.Text = dsShip.Tables(0).Rows(0).Item("FULLNAME").ToString()
                    lblAttEmail9.Text = dsShip.Tables(0).Rows(0).Item("EMAILADDRESS").ToString()
                    lblAttCompNm9.Text = dsShip.Tables(0).Rows(0).Item("COMPANYNAME").ToString()
                    lblAttphne9.Text = dsShip.Tables(0).Rows(0).Item("PHONENUMBER").ToString()
                    lblAttFax9.Text = dsShip.Tables(0).Rows(0).Item("FAXNUMBER").ToString()
                    lblAttAdd9.Text = dsShip.Tables(0).Rows(0).Item("STREETADDRESS1").ToString() + " " + dsShip.Tables(0).Rows(0).Item("STREETADDRESS2").ToString()
                    lblAttCity9.Text = dsShip.Tables(0).Rows(0).Item("CITY").ToString()
                    lblAttState9.Text = dsShip.Tables(0).Rows(0).Item("STATE").ToString()
                    lblAttZipCode9.Text = dsShip.Tables(0).Rows(0).Item("ZIPCODE").ToString()
                    lblAttCntry9.Text = dsShip.Tables(0).Rows(0).Item("COUNTRYDES").ToString()
                End If
                If AttAddressID10 <> 0 Then
                    trAt10.Visible = True
                    dsShip = objGetUserData.GetBillToShipToUserDetailsByAddID(AttAddressID10)
                    lblAttUser10.Text = dsShip.Tables(0).Rows(0).Item("FULLNAME").ToString()
                    lblAttEmail10.Text = dsShip.Tables(0).Rows(0).Item("EMAILADDRESS").ToString()
                    lblAttCompNm10.Text = dsShip.Tables(0).Rows(0).Item("COMPANYNAME").ToString()
                    lblAttphne10.Text = dsShip.Tables(0).Rows(0).Item("PHONENUMBER").ToString()
                    lblAttFax10.Text = dsShip.Tables(0).Rows(0).Item("FAXNUMBER").ToString()
                    lblAttAdd10.Text = dsShip.Tables(0).Rows(0).Item("STREETADDRESS1").ToString() + " " + dsShip.Tables(0).Rows(0).Item("STREETADDRESS2").ToString()
                    lblAttCity10.Text = dsShip.Tables(0).Rows(0).Item("CITY").ToString()
                    lblAttState10.Text = dsShip.Tables(0).Rows(0).Item("STATE").ToString()
                    lblAttZipCode10.Text = dsShip.Tables(0).Rows(0).Item("ZIPCODE").ToString()
                    lblAttCntry10.Text = dsShip.Tables(0).Rows(0).Item("COUNTRYDES").ToString()
                End If

            End If

            If hidlnkEdit.Value.ToString() <> "Enable" Then
                lnkHEdit.Visible = False
                lnkShipTo1.Visible = False
                lnkShipTo2.Visible = False
                lnkShipTo3.Visible = False
                lnkShipTo4.Visible = False
                lnkShipTo5.Visible = False
                lnkShipTo6.Visible = False
                lnkShipTo7.Visible = False
                lnkShipTo8.Visible = False
                lnkShipTo9.Visible = False
                lnkShipTo10.Visible = False
            Else
                lnkHEdit.Visible = True
                lnkShipTo1.Visible = True
                lnkShipTo2.Visible = True
                lnkShipTo3.Visible = True
                lnkShipTo4.Visible = True
                lnkShipTo5.Visible = True
                lnkShipTo6.Visible = True
                lnkShipTo7.Visible = True
                lnkShipTo8.Visible = True
                lnkShipTo9.Visible = True
                lnkShipTo10.Visible = True
            End If
            'StrSql = objUpIns.FreeMail(Session("FConFid"), Session("UserId"), ArrAtt)

        Catch ex As Exception
            'ErrorLable.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Public Sub HideBillCardDetails(ByVal flag As Boolean)
        Try
            If flag = False Then
                btnOrderCloseFree.Visible = False
            Else
                'tblorderdetail.Visible = False
                rptOrderReview.Visible = False
                trBt1.Visible = False
                trBt2.Visible = False
                trBt3.Visible = False
                trBt4.Visible = False
                trBt5.Visible = False
                trBt6.Visible = False
                trBt7.Visible = False
                trBt8.Visible = False
                trBt9.Visible = False
                trBt10.Visible = False
                trBt11.Visible = False
                rowCardhead.Visible = False
                rowCardNo.Visible = False
                rowExpdate.Visible = False
                rowCardtype.Visible = False
                Tr2.Visible = False
                btnOrder.Visible = False
                btnOrderClose.Visible = False
            End If

        Catch ex As Exception
            ErrorLable.Text = "Error:HideBillCardDetails()" + ex.Message.ToString()
        End Try
    End Sub

    Public Sub HideShowCardDetails(ByVal flag As Boolean)
        Try
            If flag = True Then
                'trC1.Visible = True
                'trC2.Visible = True
                'trC3.Visible = True
                'trC4.Visible = True
                'trC5.Visible = True
                'trC6.Visible = True
                'trC7.Visible = True
                'trC8.Visible = True
                'trC9.Visible = True
                'trC10.Visible = True
                rowCardhead.Visible = True
                rowCardNo.Visible = True
                rowExpdate.Visible = True
                rowCardtype.Visible = True
                Tr2.Visible = True
            Else
                'trC1.Visible = False
                'trC2.Visible = False
                'trC3.Visible = False
                'trC4.Visible = False
                'trC5.Visible = False
                'trC6.Visible = False
                'trC7.Visible = False
                'trC8.Visible = False
                'trC9.Visible = False
                'trC10.Visible = False
                rowCardhead.Visible = False
                rowCardNo.Visible = False
                rowExpdate.Visible = False
                rowCardtype.Visible = False
                Tr2.Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnOrder_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOrder.Click
        Dim objUpIns As New UpdateInsert()
        Dim objWebUpIns As New WebConUpIns.UpdateInsert()
        Dim arrOrderLog(30) As String
        Dim objGetData As New ShopGetData.Selectdata()
        Dim ArrAtt(10) As String
		 Dim flag As Boolean
        Try

            ArrAtt(0) = AttAddressID1
            ArrAtt(1) = AttAddressID2
            ArrAtt(2) = AttAddressID3
            ArrAtt(3) = AttAddressID4
            ArrAtt(4) = AttAddressID5
            ArrAtt(5) = AttAddressID6
            ArrAtt(6) = AttAddressID7
            ArrAtt(7) = AttAddressID8
            ArrAtt(8) = AttAddressID9
            ArrAtt(9) = AttAddressID10


            If Not objRefresh.IsRefresh Then
			  flag = True ' ChargePayment()
                If flag = True Then
                lblMainHeader.Text = "Thank You for Your Order!"
                lblSubHeader.Text = "Thank you for registering the following people for the web conference. We will send an email to each of them confirming their registration. <br />We also recommend you print this page for your records."
                objUpIns.SalesMail(Session("WRefNumber"), Session("UserId"))
                objUpIns.ConfirmOrderMailConf(Session("WRefNumber"), Session("UserId"), ArrAtt, BillAddressID, CardHAddressID)
                objUpIns.SaleOrder(Session("WRefNumber"))
                If Session("FConFid") <> Nothing Then
                    objWebUpIns.PaidMail(Session("FConFid"), Session("UserId"), ArrAtt)
                End If
                btnOrder.Visible = False
                btnOrderClose.Visible = True

                'Log for Ordering
                Try
                    objUpIns.UpdateWCOrderLog(Session("WRefNumber"), Session("userId"), Session("UserName"))
                Catch ex As Exception
                End Try


                Session("WRefNumber") = ""
                Session("GrantTotal") = ""
                Session("FConFid") = Nothing
				Else
                    Response.Redirect("OrderError.aspx")
                End If

            End If
        Catch ex As Exception
            ErrorLable.Text = "Error:btnOrder_Click" + ex.Message.ToString()
        End Try
    End Sub

	 Protected Function ChargePayment() As Boolean
        Dim PaymentFlag As Boolean = False
        Dim ExpDate() As String
        Dim Month As String = ""
        Dim Year As String = ""
        Dim curExpDate As String = ""
        Try
	    ServicePointManager.SecurityProtocol = DirectCast(3072, SecurityProtocolType)

            Dim ApiLogin As String = System.Configuration.ConfigurationManager.AppSettings("ApiLoginL")
            Dim TransactionKey As String = System.Configuration.ConfigurationManager.AppSettings("TransactionKeyL")

            'Dim post_url As [String] = "https://test.authorize.net/gateway/transact.dll"
            'Dim post_url As [String] = "https://secure.authorize.net/gateway/transact.dll"
             Dim post_url As [String] = "https://secure2.authorize.net/gateway/transact.dll"
          
            

            ExpDate = Regex.Split(hidExpDate.Value, "/")
            If ExpDate.Length > 0 Then
                Month = ExpDate(0)
                Year = ExpDate(1).Remove(0, 2)
            End If
            'curExpDate = Month + "" + Year

            Dim post_values As New Dictionary(Of String, String)()

            post_values.Add("x_login", ApiLogin)
            post_values.Add("x_tran_key", TransactionKey)
            post_values.Add("x_delim_data", "TRUE")
            post_values.Add("x_delim_char", "|")
            post_values.Add("x_relay_response", "FALSE")

            post_values.Add("x_type", "AUTH_CAPTURE")
            post_values.Add("x_method", "CC")
            post_values.Add("x_card_num", lblCC.Text)
            'post_values.Add("x_card_num", "4007000000027")
            post_values.Add("x_card_code", lblAuth_Code.Text)
            'post_values.Add("x_card_code", "027")
            post_values.Add("x_exp_date", Month + "" + Year)

            post_values.Add("x_amount", lblGrandTotal.Text)
			
	   
            post_values.Add("x_description", "")


            post_values.Add("x_first_name", hidFName.Value)
            post_values.Add("x_last_name", hidLName.Value)
            post_values.Add("x_company", hidCompanyName.Value)

            post_values.Add("x_address", lblAdd.Text)
            post_values.Add("x_state", lblState.Text)
            post_values.Add("x_zip", lblZipCode.Text)
            post_values.Add("x_city", lblCity.Text)
            post_values.Add("x_country", lblCntry.Text)


            Dim post_string As [String] = ""

            For Each post_value As KeyValuePair(Of String, String) In post_values
                post_string += post_value.Key + "=" + HttpUtility.UrlEncode(post_value.Value) + "&"
            Next
            post_string = post_string.TrimEnd("&"c)

            'Create an HttpWebRequest object to communicate with Authorize.net
            Dim objRequest As HttpWebRequest = DirectCast(WebRequest.Create(post_url), HttpWebRequest)
            objRequest.Method = "POST"
            objRequest.ContentLength = post_string.Length
            objRequest.ContentType = post_url

            'post data is sent as a stream
            Dim myWriter As StreamWriter = Nothing
            myWriter = New StreamWriter(objRequest.GetRequestStream())
            myWriter.Write(post_string)
            myWriter.Close()

            'returned values are returned as a stream, then read into a string
            Dim post_response As [String]
            Dim objResponse As HttpWebResponse = DirectCast(objRequest.GetResponse(), HttpWebResponse)
            Using responseStream As New StreamReader(objResponse.GetResponseStream())
                post_response = responseStream.ReadToEnd()
                responseStream.Close()
            End Using

            Dim details As String() = post_response.Split("|"c)

            If details(0) = "1" Then
                PaymentFlag = True
            Else
                PaymentFlag = False
                Session("PaidMessageWEB") = details(3)
            End If

            Return PaymentFlag
            ' Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('" + post_response + "');", True)

        Catch ex As Exception
            Return PaymentFlag
        End Try
    End Function
	
    Protected Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Response.Redirect(HttpContext.Current.Request.Url.ToString(), True)
        Catch ex As Exception
            ErrorLable.Text = "Error:btnRefresh_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnOrderCloseFree_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOrderCloseFree.Click
        Dim objUpIns As New WebConUpIns.UpdateInsert()
        Dim StrSql As String = String.Empty
        Dim objWebCGetData As New WebConf.Selectdata()
        Dim dsWebC As New DataSet()
        Dim ArrAtt(10) As String
        Try
            ArrAtt(0) = AttAddressID1
            ArrAtt(1) = AttAddressID2
            ArrAtt(2) = AttAddressID3
            ArrAtt(3) = AttAddressID4
            ArrAtt(4) = AttAddressID5
            ArrAtt(5) = AttAddressID6
            ArrAtt(6) = AttAddressID7
            ArrAtt(7) = AttAddressID8
            ArrAtt(8) = AttAddressID9
            ArrAtt(9) = AttAddressID10

            If Not objRefresh.IsRefresh Then
                lblMainHeader.Text = "Thank You for Registration!"
                lblSubHeader.Text = "Thank you for registering the following people for the web conference. We will send an email to each of them confirming their registration. <br />We also recommend you print this page for your records."
                StrSql = objUpIns.FreeMail(Session("FConFid"), Session("UserId"), ArrAtt)

                btnOrderCloseFree.Visible = False
                btnOrderClose.Visible = True

                Session("WRefNumber") = ""
                Session("FConFid") = Nothing
            End If

            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "UserLogin", "window.close();", True)
            'Response.Redirect("~\index.aspx")
        Catch ex As Exception
            ErrorLable.Text = "Error:btnOrderCloseFree_Click:" + ex.Message.ToString()
        End Try
    End Sub

End Class
