Imports System.Data
Imports System.Data.OleDb
Imports System
Imports ShopGetData
Imports ShopUpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class WebConferenceN_ShoppingCart
    Inherits System.Web.UI.Page
#Region "Get Set Variables"
    Dim _lErrorLble As Label
    Dim _ctlContentPlaceHolder As ContentPlaceHolder
    Dim _btnLogOut As LinkButton
    Dim _strId As String


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

    Public Property Logbtn() As LinkButton
        Get
            Return _btnLogOut
        End Get
        Set(ByVal value As LinkButton)
            _btnLogOut = value
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
        ctlContentPlaceHolder = Page.Master.FindControl("ContentPlaceHolder1")
    End Sub

    Protected Sub GetLogoutButton()
        Logbtn = Page.Master.FindControl("lnkLogout")
        AddHandler Logbtn.Click, AddressOf lnkLogout_Click
    End Sub

#End Region

#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_WEBCONFERENCEN_SHOPPINGCART")
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        objRefresh.Render(Page)
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ObjCrypto As New CryptoHelper()
        Dim objUpdateData As New UsersUpdateData.UpdateInsert()
        Try
            GetMasterPageControls()
            Response.Cache.SetCacheability(HttpCacheability.NoCache)


            If (rdbCreditcard.Checked = True) Then
                rowCardNo.Visible = True
                rowCardType.Visible = True
                rowExpDate.Visible = True
            End If
            If Session("FConType") = "Free" Then
                btnOrder.Text = "Complete Registration Process"
                rowCardNo.Style.Add("Display", "None")
                rowExpDate.Style.Add("Display", "None")
                rowCardType.Style.Add("Display", "None")
                'rowCardHolderAdd.Style.Add("Display", "None")
                trSecurityCode.Style.Add("Display", "None")
                rowBilltoAdd.Style.Add("Display", "None")
                rowPayType.Style.Add("Display", "None")
            End If

            'lnkBillAdd.Attributes.Add("onclick", "return OpenNewWindow('../ShoppingCart/UserAddressDetails.aspx?lnkDesId=" + ctlContentPlaceHolder.ClientID.ToString() + "_lnkBillAdd&lnkhdnID=" + ctlContentPlaceHolder.ClientID.ToString() + "_hdnBillAddId" + "','BillPage');")
            'lnkCardHAdd.Attributes.Add("onclick", "return OpenNewWindow('../ShoppingCart/UserAddressDetails.aspx?lnkDesId=" + ctlContentPlaceHolder.ClientID.ToString() + "_lnkCardHAdd&lnkhdnID=" + ctlContentPlaceHolder.ClientID.ToString() + "_hdnCardHAddId" + "','CardHPage');")
            lnkBillAdd.Attributes.Add("onclick", "return OpenNewWindow('UserAddressDetails.aspx?lnkDesId=" + ctlContentPlaceHolder.ClientID.ToString() + "_lnkBillAdd&lnkhdnID=" + ctlContentPlaceHolder.ClientID.ToString() + "_hdnBillAddId&lnkhdnDes=" + ctlContentPlaceHolder.ClientID.ToString() + "_hdnlnkBillAddDes&Type=N" + "','BillPage');")
            lnkCardHAdd.Attributes.Add("onclick", "return OpenNewWindow('UserAddressDetails.aspx?lnkDesId=" + ctlContentPlaceHolder.ClientID.ToString() + "_lnkCardHAdd&lnkhdnID=" + ctlContentPlaceHolder.ClientID.ToString() + "_hdnCardHAddId&lnkhdnDes=" + ctlContentPlaceHolder.ClientID.ToString() + "_hdnlnkCardHAddDes" + "&Type=N','CardHPage');")

            lnkAtAdd1.Attributes.Add("onclick", "return OpenNewWindow('UserAddressDetails.aspx?lnkDesId=" + ctlContentPlaceHolder.ClientID.ToString() + "_lnkAtAdd1&lnkhdnID=" + ctlContentPlaceHolder.ClientID.ToString() + "_hdnlnkAtAdd1&lnkhdnDes=" + ctlContentPlaceHolder.ClientID.ToString() + "_hdnlnkAtAdd1Des" + "&Type=Y','At1');")
            lnkAtAdd2.Attributes.Add("onclick", "return OpenNewWindow('UserAddressDetails.aspx?lnkDesId=" + ctlContentPlaceHolder.ClientID.ToString() + "_lnkAtAdd2&lnkhdnID=" + ctlContentPlaceHolder.ClientID.ToString() + "_hdnlnkAtAdd2&lnkhdnDes=" + ctlContentPlaceHolder.ClientID.ToString() + "_hdnlnkAtAdd2Des" + "&Type=Y','At2');")
            lnkAtAdd3.Attributes.Add("onclick", "return OpenNewWindow('UserAddressDetails.aspx?lnkDesId=" + ctlContentPlaceHolder.ClientID.ToString() + "_lnkAtAdd3&lnkhdnID=" + ctlContentPlaceHolder.ClientID.ToString() + "_hdnlnkAtAdd3&lnkhdnDes=" + ctlContentPlaceHolder.ClientID.ToString() + "_hdnlnkAtAdd3Des" + "&Type=Y','At3');")
            lnkAtAdd4.Attributes.Add("onclick", "return OpenNewWindow('UserAddressDetails.aspx?lnkDesId=" + ctlContentPlaceHolder.ClientID.ToString() + "_lnkAtAdd4&lnkhdnID=" + ctlContentPlaceHolder.ClientID.ToString() + "_hdnlnkAtAdd4&lnkhdnDes=" + ctlContentPlaceHolder.ClientID.ToString() + "_hdnlnkAtAdd4Des" + "&Type=Y','At4');")
            lnkAtAdd5.Attributes.Add("onclick", "return OpenNewWindow('UserAddressDetails.aspx?lnkDesId=" + ctlContentPlaceHolder.ClientID.ToString() + "_lnkAtAdd5&lnkhdnID=" + ctlContentPlaceHolder.ClientID.ToString() + "_hdnlnkAtAdd5&lnkhdnDes=" + ctlContentPlaceHolder.ClientID.ToString() + "_hdnlnkAtAdd5Des" + "&Type=Y','At5');")
            lnkAtAdd6.Attributes.Add("onclick", "return OpenNewWindow('UserAddressDetails.aspx?lnkDesId=" + ctlContentPlaceHolder.ClientID.ToString() + "_lnkAtAdd6&lnkhdnID=" + ctlContentPlaceHolder.ClientID.ToString() + "_hdnlnkAtAdd6&lnkhdnDes=" + ctlContentPlaceHolder.ClientID.ToString() + "_hdnlnkAtAdd6Des" + "&Type=Y','At6');")
            lnkAtAdd7.Attributes.Add("onclick", "return OpenNewWindow('UserAddressDetails.aspx?lnkDesId=" + ctlContentPlaceHolder.ClientID.ToString() + "_lnkAtAdd7&lnkhdnID=" + ctlContentPlaceHolder.ClientID.ToString() + "_hdnlnkAtAdd7&lnkhdnDes=" + ctlContentPlaceHolder.ClientID.ToString() + "_hdnlnkAtAdd7Des" + "&Type=Y','At7');")
            lnkAtAdd8.Attributes.Add("onclick", "return OpenNewWindow('UserAddressDetails.aspx?lnkDesId=" + ctlContentPlaceHolder.ClientID.ToString() + "_lnkAtAdd8&lnkhdnID=" + ctlContentPlaceHolder.ClientID.ToString() + "_hdnlnkAtAdd8&lnkhdnDes=" + ctlContentPlaceHolder.ClientID.ToString() + "_hdnlnkAtAdd8Des" + "&Type=Y','At8');")
            lnkAtAdd9.Attributes.Add("onclick", "return OpenNewWindow('UserAddressDetails.aspx?lnkDesId=" + ctlContentPlaceHolder.ClientID.ToString() + "_lnkAtAdd9&lnkhdnID=" + ctlContentPlaceHolder.ClientID.ToString() + "_hdnlnkAtAdd9&lnkhdnDes=" + ctlContentPlaceHolder.ClientID.ToString() + "_hdnlnkAtAdd9Des" + "&Type=Y','At9');")
            lnkAtAdd10.Attributes.Add("onclick", "return OpenNewWindow('UserAddressDetails.aspx?lnkDesId=" + ctlContentPlaceHolder.ClientID.ToString() + "_lnkAtAdd10&lnkhdnID=" + ctlContentPlaceHolder.ClientID.ToString() + "_hdnlnkAtAdd10&lnkhdnDes=" + ctlContentPlaceHolder.ClientID.ToString() + "_hdnlnkAtAdd10Des" + "&Type=Y','At10');")
           
		    If Session("Back") = Nothing And Session("SBack") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("~/Universal_loginN/Pages/ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "", False)
            End If
			
            GetPageDetails()

            If Not IsPostBack Then
                GetExpirayDate()
                objUpdateData.InsShoppingCartAddDetails(Session("UserId").ToString())
                GetSavedDetails()
            End If


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
        Dim dsLoginUser As New DataSet()
        Try

            ds = objGetUserData.GetUserDetails(Session("UserId"))
            hdnUserId.value = Convert.ToString(objGetUserData.GetUserAddressIDByHeadID("Login User", Session("UserId")))

            If ds.Tables(0).Rows.Count > 0 Then
                hdnUserName.Value = ds.Tables(0).Rows(0).Item("NAME").ToString()
                lblName.Text = ds.Tables(0).Rows(0).Item("PREFIX").ToString() + " " + ds.Tables(0).Rows(0).Item("FIRSTNAME").ToString() + " " + ds.Tables(0).Rows(0).Item("LASTNAME").ToString() + " "
                lblEmail.Text = ds.Tables(0).Rows(0).Item("EMAILADDRESS").ToString()
                ' hdnUseremail.value = ds.Tables(0).Rows(0).Item("EMAILADDRESS").ToString()
              
                lblCompName.Text = ds.Tables(0).Rows(0).Item("COMPANYNAME").ToString()
               
            End If

            lblRefNumber.Text = Session("WRefNumber")
            lblAmt.Text = FormatNumber("0", 2)

            'GetSavedDetails()
            If hdnBillAddId.Value <> 0 Then
                lnkBillAdd.Text = hdnlnkBillAddDes.Value
            End If
            If hdnCardHAddId.Value <> 0 Then
                lnkCardHAdd.Text = hdnlnkCardHAddDes.Value
            End If
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
        Dim objWebCGetData As New WebConf.Selectdata()
        Dim objGetData As New Selectdata()
        Dim dsOrder As New DataSet()
        Dim dsWebC As New DataSet()
        Dim dsUserAdress As New DataSet()
        Try
            'ViewState("SavedInfo") = "false"
            If Session("FConType").ToString() <> "Free" Then
                dsOrder = objGetData.GetOrderReview(Session("WRefNumber"))
                dsCust = objGetData.GetCustInfo(Session("WRefNumber"))
                dsOrderIds = objGetData.GetOrderReviewAShop(Session("WRefNumber"))

                If dsCust.Tables(0).Rows.Count > 0 Then
                    'IF THE PATMENT TYPE IS INVOICE 
                    lblAmt.Text = dsOrder.Tables(0).Rows(0)("SUBTOTAL").ToString()
                    If dsCust.Tables(0).Rows(0)("PAYMENTTYPE").ToString().ToUpper() = "INVOICE" Then
                        rdbInvoice.Checked = True
                        rowCardType.Style.Add("display", "none")
                        rowExpDate.Style.Add("display", "none")
                        trSecurityCode.Style.Add("display", "none")
                        'rowCardHolderAdd.Style.Add("display", "none")
                        rowCardNo.Style.Add("display", "none")

                    Else
                        rdbCreditcard.Checked = True
                        txtCC.Text = dsCust.Tables(0).Rows(0).Item("CARDNUMBER").ToString()
                        txtAuthCode.Text = dsCust.Tables(0).Rows(0).Item("AUTH_CODE").ToString()
                        ddlCCType.Text = dsCust.Tables(0).Rows(0).Item("CARDTYPE").ToString()
                        ddlMonth.Text = Split(dsCust.Tables(0).Rows(0).Item("CARDEXP").ToString(), "/")(0).ToString()
                        ddlYears.Text = Split(dsCust.Tables(0).Rows(0).Item("CARDEXP").ToString(), "/")(1).ToString()
                        dsCardToUser = objGetUserData.GetBillToShipToUserDetailsByAddID(dsOrderIds.Tables(0).Rows(0).Item("CARDHID").ToString())
                        If dsCardToUser.Tables(0).Rows.Count > 0 Then
                            lnkCardHAdd.Text = dsCardToUser.Tables(0).Rows(0).Item("USERNAME").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + dsCardToUser.Tables(0).Rows(0).Item("ADDHEADER").ToString()
                            hdnCardHAddId.Value = dsOrderIds.Tables(0).Rows(0).Item("CARDHID").ToString()
                        End If
                    End If
                    dsBillToUser = objGetUserData.GetBillToShipToUserDetailsByAddID(dsOrderIds.Tables(0).Rows(0).Item("BILLTOID").ToString())
                    If dsBillToUser.Tables(0).Rows.Count > 0 Then
                        lnkBillAdd.Text = dsBillToUser.Tables(0).Rows(0).Item("USERNAME").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + dsBillToUser.Tables(0).Rows(0).Item("ADDHEADER").ToString()
                        hdnBillAddId.Value = dsOrderIds.Tables(0).Rows(0).Item("BILLTOID").ToString()
                    End If
                Else
                    'lnkBillAdd.ToolTip = "Click Nothing Selected to choose or create an address the Bill to address"
                    'lnkCardHAdd.ToolTip = "Click Nothing Selected to choose or create the Credit Card Holder’s address."
                End If
            Else
            End If



            dsWebC = objWebCGetData.GetWebConfAttDetails(Session("WRefNumber"))
           
            dsUserAdress = objGetUserData.GetUserAddressByUserID(Session("USERID").ToString(), "N")
            Dim dr() As DataRow
            If dsWebC.Tables(0).Rows.Count > 0 Then
                If Session("FConFid") = Nothing Then
                    Session("FConFid") = dsWebC.Tables(0).Rows(0).Item("WEBCONFID").ToString()
                End If
                For i = 0 To dsWebC.Tables(0).Rows.Count - 1
                    If i = 0 Then
                        hdnlnkAtAdd1.Value = dsWebC.Tables(0).Rows(0).Item("ADDRESSID").ToString()
                        dr = dsUserAdress.Tables(0).Select("USERADDRESSID =" + hdnlnkAtAdd1.Value)
                        If dr.Length > 0 Then
                            hdnlnkAtAdd1Des.Value = "" + dr(0).Item("USERNAME") + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + dr(0).Item("ADDHEADER")
                            lnkAtAdd1.Text = hdnlnkAtAdd1Des.Value
                        End If
                    ElseIf i = 1 Then
                        hdnlnkAtAdd2.Value = dsWebC.Tables(0).Rows(1).Item("ADDRESSID").ToString()
                        dr = dsUserAdress.Tables(0).Select("USERADDRESSID =" + hdnlnkAtAdd2.Value)
                        If dr.Length > 0 Then
                            hdnlnkAtAdd2Des.Value = "" + dr(0).Item("USERNAME") + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + dr(0).Item("ADDHEADER")
                            lnkAtAdd2.Text = hdnlnkAtAdd2Des.Value
                        End If
                    ElseIf i = 2 Then
                        hdnlnkAtAdd3.Value = dsWebC.Tables(0).Rows(2).Item("ADDRESSID").ToString()
                        dr = dsUserAdress.Tables(0).Select("USERADDRESSID =" + hdnlnkAtAdd3.Value)
                        If dr.Length > 0 Then
                            hdnlnkAtAdd3Des.Value = "" + dr(0).Item("USERNAME") + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + dr(0).Item("ADDHEADER")
                            lnkAtAdd3.Text = hdnlnkAtAdd3Des.Value
                        End If
                    ElseIf i = 3 Then
                        hdnlnkAtAdd4.Value = dsWebC.Tables(0).Rows(3).Item("ADDRESSID").ToString()
                        dr = dsUserAdress.Tables(0).Select("USERADDRESSID =" + hdnlnkAtAdd4.Value)
                        If dr.Length > 0 Then
                            hdnlnkAtAdd4Des.Value = "" + dr(0).Item("USERNAME") + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + dr(0).Item("ADDHEADER")
                            lnkAtAdd4.Text = hdnlnkAtAdd4Des.Value
                        End If
                    ElseIf i = 4 Then
                        hdnlnkAtAdd5.Value = dsWebC.Tables(0).Rows(4).Item("ADDRESSID").ToString()
                        dr = dsUserAdress.Tables(0).Select("USERADDRESSID =" + hdnlnkAtAdd5.Value)
                        If dr.Length > 0 Then
                            hdnlnkAtAdd5Des.Value = "" + dr(0).Item("USERNAME") + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + dr(0).Item("ADDHEADER")
                            lnkAtAdd5.Text = hdnlnkAtAdd5Des.Value
                        End If
                    ElseIf i = 6 Then
                        hdnlnkAtAdd6.Value = dsWebC.Tables(0).Rows(5).Item("ADDRESSID").ToString()
                        dr = dsUserAdress.Tables(0).Select("USERADDRESSID =" + hdnlnkAtAdd6.Value)
                        If dr.Length > 0 Then
                            hdnlnkAtAdd6Des.Value = "" + dr(0).Item("USERNAME") + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + dr(0).Item("ADDHEADER")
                            lnkAtAdd6.Text = hdnlnkAtAdd6Des.Value
                        End If
                    ElseIf i = 7 Then
                        hdnlnkAtAdd7.Value = dsWebC.Tables(0).Rows(6).Item("ADDRESSID").ToString()
                        dr = dsUserAdress.Tables(0).Select("USERADDRESSID =" + hdnlnkAtAdd7.Value)
                        If dr.Length > 0 Then
                            hdnlnkAtAdd7Des.Value = "" + dr(0).Item("USERNAME") + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + dr(0).Item("ADDHEADER")
                            lnkAtAdd7.Text = hdnlnkAtAdd7Des.Value
                        End If
                    ElseIf i = 8 Then
                        hdnlnkAtAdd8.Value = dsWebC.Tables(0).Rows(7).Item("ADDRESSID").ToString()
                        dr = dsUserAdress.Tables(0).Select("USERADDRESSID =" + hdnlnkAtAdd8.Value)
                        If dr.Length > 0 Then
                            hdnlnkAtAdd8Des.Value = "" + dr(0).Item("USERNAME") + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + dr(0).Item("ADDHEADER")
                            lnkAtAdd8.Text = hdnlnkAtAdd8Des.Value
                        End If
                    ElseIf i = 9 Then
                        hdnlnkAtAdd9.Value = dsWebC.Tables(0).Rows(8).Item("ADDRESSID").ToString()
                        dr = dsUserAdress.Tables(0).Select("USERADDRESSID =" + hdnlnkAtAdd9.Value)
                        If dr.Length > 0 Then
                            hdnlnkAtAdd9Des.Value = "" + dr(0).Item("USERNAME") + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + dr(0).Item("ADDHEADER")
                            lnkAtAdd9.Text = hdnlnkAtAdd9Des.Value
                        End If
                    ElseIf i = 9 Then
                        hdnlnkAtAdd10.Value = dsWebC.Tables(0).Rows(9).Item("ADDRESSID").ToString()
                        dr = dsUserAdress.Tables(0).Select("USERADDRESSID =" + hdnlnkAtAdd10.Value)
                        If dr.Length > 0 Then
                            hdnlnkAtAdd10Des.Value = "" + dr(0).Item("USERNAME") + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + dr(0).Item("ADDHEADER")
                            lnkAtAdd10.Text = hdnlnkAtAdd10Des.Value
                        End If
                    End If
                Next
            End If




        Catch ex As Exception
            'ErrorLable.Text = "Error:GetPageDetails" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub GetExpirayDate()
        Dim i As New Integer
        Dim lst As New ListItem
        Try
            For i = 1 To 12
                lst = New ListItem
                lst.Text = i.ToString()
                lst.Value = i.ToString()
                ddlMonth.Items.Add(lst)
                ddlMonth.AppendDataBoundItems = True
            Next

            For i = 0 To 20
                lst = New ListItem
                lst.Text = System.DateTime.Now.Year + i.ToString()
                lst.Value = System.DateTime.Now.Year + i.ToString()
                ddlYears.Items.Add(lst)
                ddlYears.AppendDataBoundItems = True
            Next


        Catch ex As Exception
            ErrorLable.Text = "Error:GetExpirayDate:" + ex.Message.ToString()
        End Try
    End Sub

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
        'Dim flag As Boolean
        'Dim shippingCost As Double
        Dim shipCreditCost As Double
        Dim AtAdd(10) As String
        Dim i As Integer = 0
        Dim cnt As Integer = 0
        Dim totalVal As Double = 0
        Dim objUpW As New WebConUpIns.UpdateInsert()
        Try

            If hdnlnkAtAdd1.Value <> Nothing Then
                AtAdd(0) = hdnlnkAtAdd1.Value
            End If
            If hdnlnkAtAdd2.Value <> Nothing Then
                AtAdd(1) = hdnlnkAtAdd2.Value
            End If
            If hdnlnkAtAdd3.Value <> Nothing Then
                AtAdd(2) = hdnlnkAtAdd3.Value
            End If
            If hdnlnkAtAdd4.Value <> Nothing Then
                AtAdd(3) = hdnlnkAtAdd4.Value
            End If
            If hdnlnkAtAdd5.Value <> Nothing Then
                AtAdd(4) = hdnlnkAtAdd5.Value
            End If
            If hdnlnkAtAdd6.Value <> Nothing Then
                AtAdd(5) = hdnlnkAtAdd6.Value
            End If
            If hdnlnkAtAdd7.Value <> Nothing Then
                AtAdd(6) = hdnlnkAtAdd7.Value
            End If
            If hdnlnkAtAdd8.Value <> Nothing Then
                AtAdd(7) = hdnlnkAtAdd8.Value
            End If
            If hdnlnkAtAdd9.Value <> Nothing Then
                AtAdd(8) = hdnlnkAtAdd9.Value
            End If
            If hdnlnkAtAdd10.Value <> Nothing Then
                AtAdd(9) = hdnlnkAtAdd10.Value
            End If

            If hdnlnkAtAdd1.Value <> "0" Then
                cnt += 1
            End If
            If hdnlnkAtAdd2.Value <> "0" Then
                cnt += 1
            End If
            If hdnlnkAtAdd3.Value <> "0" Then
                cnt += 1
            End If
            If hdnlnkAtAdd4.Value <> "0" Then
                cnt += 1
            End If
            If hdnlnkAtAdd5.Value <> "0" Then
                cnt += 1
            End If
            If hdnlnkAtAdd6.Value <> "0" Then
                cnt += 1
            End If
            If hdnlnkAtAdd7.Value <> "0" Then
                cnt += 1
            End If
            If hdnlnkAtAdd8.Value <> "0" Then
                cnt += 1
            End If
            If hdnlnkAtAdd9.Value <> "0" Then
                cnt += 1
            End If
            If hdnlnkAtAdd10.Value <> "0" Then
                cnt += 1
            End If



            
            cnt = 0

            For i = 0 To 9
                If AtAdd(i) <> Nothing And AtAdd(i) <> "0" Then
                    cnt += 1
                    objUpW.InsertWebConfAttnd(Session("WRefNumber"), Session("FConFid"), Session("UserID"), AtAdd(i), cnt)
                End If
            Next

            If Session("FConType").ToString() <> "Free" Then
                ExpDate = ddlMonth.SelectedItem.Value.ToString() + "/" + ddlYears.SelectedItem.Value.ToString()
                If rdbCreditcard.Checked = True Then
                    objUpIns.InsertCustomerInfoConf(Session("WRefNumber"), txtCC.Text.Trim(), ExpDate, ddlCCType.SelectedValue.Trim(), Session("UserId"), hdnBillAddId.Value, AtAdd, hdnCardHAddId.Value, "CreditCard", txtNameOnC.Text, txtAuthCode.Text)
                Else
                    objUpIns.InsertCustomerInfoConf(Session("WRefNumber"), "", "", "", Session("UserId"), hdnBillAddId.Value, AtAdd, "0", "Invoice", "", "")
                End If
                dsItems = objGetShopdata.GetOrderReview(Session("WRefNumber").ToString())

                totalVal = CDbl(dsItems.Tables(0).Rows(0).Item("UNITCOST").ToString() * cnt)

                dsTax = objGetShopdata.GetTax()
                For i = 0 To dsItems.Tables(0).Rows.Count - 1
                    shipCreditCost = 0.0
                    If rdbCreditcard.Checked = True Then
                        shipCreditCost = shipCreditCost + ((totalVal * CDbl(dsTax.Tables(0).Rows(0)("CREDITCARDVALUE").ToString())) / 100)
                        total = shipCreditCost + totalVal
                        objUpIns.UpdateTaxConf(Session("WRefNumber"), hdnBillAddId.Value, AtAdd, hdnCardHAddId.Value, totalVal, dsItems.Tables(0).Rows(i)("ITEMNUMBER").ToString(), dsItems.Tables(0).Rows(i)("SEQUENCE").ToString(), cnt, shipCreditCost, total)
                    Else
                        total = totalVal
                        objUpIns.UpdateTaxConf(Session("WRefNumber"), hdnBillAddId.Value, AtAdd, "0", totalVal, dsItems.Tables(0).Rows(i)("ITEMNUMBER").ToString(), dsItems.Tables(0).Rows(i)("SEQUENCE").ToString(), cnt, shipCreditCost, total)
                    End If
                    taxAmount = 0
                Next
                Response.Redirect("ConfirmOrder.aspx")
            Else
                'Response.Redirect("WebConfAtte.aspx")
                Response.Redirect("ConfirmOrder.aspx")
            End If




        Catch ex As Exception
            ErrorLable.Text = "Error:btnOrder_Click" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub lnkLogout_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If Session("UserId") <> Nothing Then
                Session.Abandon()
                Response.Redirect("~/Index.aspx")
            Else
                Response.Redirect("~/Index.aspx")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnRef_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRef.Click
        Dim objGetData As New ShopGetData.Selectdata()
        Dim countTotal As Double
        Dim cnt As Integer = 0
        Dim ds As New DataSet()
        Try
            lnkAtAdd1.Text = hdnlnkAtAdd1Des.Value
            lnkAtAdd2.Text = hdnlnkAtAdd2Des.Value
            lnkAtAdd3.Text = hdnlnkAtAdd3Des.Value
            lnkAtAdd4.Text = hdnlnkAtAdd4Des.Value
            lnkAtAdd5.Text = hdnlnkAtAdd5Des.Value
            lnkAtAdd6.Text = hdnlnkAtAdd6Des.Value
            lnkAtAdd7.Text = hdnlnkAtAdd7Des.Value
            lnkAtAdd8.Text = hdnlnkAtAdd8Des.Value
            lnkAtAdd9.Text = hdnlnkAtAdd9Des.Value
            lnkAtAdd10.Text = hdnlnkAtAdd10Des.Value
            ds = objGetData.GetOrderReview(Session("WRefNumber").ToString())
            If ds.Tables(0).Rows.Count > 0 Then
                countTotal = ds.Tables(0).Rows(0).Item("UNITCOST").ToString()
            End If
            hdnIsLogINuser.value = "N"
            If hdnlnkAtAdd1.Value <> "0" Then
                cnt += 1
                If hdnUserId.Value = hdnlnkAtAdd1.Value Then
                    hdnIsLogINuser.Value = "Y"
                End If
            End If
            If hdnlnkAtAdd2.Value <> "0" Then
                cnt += 1
                If hdnUserId.Value = hdnlnkAtAdd2.Value Then
                    hdnIsLogINuser.Value = "Y"
                End If
            End If
            If hdnlnkAtAdd3.Value <> "0" Then
                cnt += 1
                If hdnUserId.Value = hdnlnkAtAdd3.Value Then
                    hdnIsLogINuser.Value = "Y"
                End If
            End If
            If hdnlnkAtAdd4.Value <> "0" Then
                cnt += 1
                If hdnUserId.Value = hdnlnkAtAdd4.Value Then
                    hdnIsLogINuser.Value = "Y"
                End If
            End If
            If hdnlnkAtAdd5.Value <> "0" Then
                cnt += 1
                If hdnUserId.Value = hdnlnkAtAdd5.Value Then
                    hdnIsLogINuser.Value = "Y"
                End If
            End If
            If hdnlnkAtAdd6.Value <> "0" Then
                cnt += 1
                If hdnUserId.Value = hdnlnkAtAdd6.Value Then
                    hdnIsLogINuser.Value = "Y"
                End If
            End If
            If hdnlnkAtAdd7.Value <> "0" Then
                cnt += 1
                If hdnUserId.Value = hdnlnkAtAdd7.Value Then
                    hdnIsLogINuser.Value = "Y"
                End If
            End If
            If hdnlnkAtAdd8.Value <> "0" Then
                cnt += 1
                If hdnUserId.Value = hdnlnkAtAdd8.Value Then
                    hdnIsLogINuser.Value = "Y"
                End If
            End If
            If hdnlnkAtAdd9.Value <> "0" Then
                cnt += 1
                If hdnUserId.Value = hdnlnkAtAdd9.Value Then
                    hdnIsLogINuser.Value = "Y"
                End If
            End If
            If hdnlnkAtAdd10.Value <> "0" Then
                cnt += 1
                If hdnUserId.Value = hdnlnkAtAdd10.Value Then
                    hdnIsLogINuser.Value = "Y"
                End If
            End If
            lblAmt.Text = FormatNumber(CDbl(countTotal * cnt).ToString(), 2)
            If rdbInvoice.Checked = True Then
                rowCardNo.Style.Add("Display", "None")
                rowExpDate.Style.Add("Display", "None")
                rowCardType.Style.Add("Display", "None")
                'rowCardHolderAdd.Style.Add("Display", "None")
                trSecurityCode.Style.Add("Display", "None")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "UserLogin", "window.close();", True)
        Catch ex As Exception

        End Try
    End Sub

End Class
