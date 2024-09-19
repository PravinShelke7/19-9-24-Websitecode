Imports System.Data
Imports System.Data.OleDb
Imports System
Imports WebConf
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports WebConUpIns

Partial Class WebConferenceN_WebConfAtte
    Inherits System.Web.UI.Page

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
        objRefresh = New zCon.Net.Refresh("_WEBCONFERENCE_WEBCONF")
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        objRefresh.Render(Page)
    End Sub

#End Region


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            GetMasterPageControls()
            If Not IsPostBack Then
                If Session("UserId") = Nothing Then
                    Dim obj As New CryptoHelper
                    Response.Redirect("~/Universal_loginN/Pages/ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
                Else
                    GetPageDetails()
                End If
            End If
        Catch ex As Exception
            ErrorLable.Text = "Error:Page_Load" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetPageDetails()
        'Dim objGetData As New Selectdata()
        'Dim objUpIns As New UpdateInsert()
        'Dim ds As New DataSet()
        'Dim StrSql As String = String.Empty
        'Try
        '    ds = objGetData.GetWebConfDetailsById(Session("FConFid"))
        '    StrSql = objUpIns.FreeMail(Session("FConFid"), Session("UserId"))
        '    If ds.Tables(0).Rows(0).Item("CONFCOST").ToString() = "Free" Then
        '        Session("FConFid") = Nothing
        '        btnRegister.OnClientClick = "javascript:window.close();"

        '    Else
        '        btnRegister.PostBackUrl = "~/ShoppingCart/OrderReview.aspx"
        '    End If
        '    divAttendee.InnerHtml = StrSql

        Dim objGetData As New Selectdata()
        Dim objUpIns As New UpdateInsert()
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
        Try
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
            lblSubHeader.Text = "Thank you for registering the following people for the web conference. We will send an email to each of them confirming their registration. <br />We also recommend you print this page for your records."

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


                If AttAddressID1 <> 0 Then
                    trAt1.Visible = True
                    dsShip = objGetUserData.GetBillToShipToUserDetailsByAddID(AttAddressID1)
                    lblAttUser1.Text = dsShip.Tables(0).Rows(0).Item("FULLNAME").ToString()
                    lblAttEmail1.Text = dsShip.Tables(0).Rows(0).Item("EMAILADDRESS").ToString()
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
                    lblAttphne10.Text = dsShip.Tables(0).Rows(0).Item("PHONENUMBER").ToString()
                    lblAttFax10.Text = dsShip.Tables(0).Rows(0).Item("FAXNUMBER").ToString()
                    lblAttAdd10.Text = dsShip.Tables(0).Rows(0).Item("STREETADDRESS1").ToString() + " " + dsShip.Tables(0).Rows(0).Item("STREETADDRESS2").ToString()
                    lblAttCity10.Text = dsShip.Tables(0).Rows(0).Item("CITY").ToString()
                    lblAttState10.Text = dsShip.Tables(0).Rows(0).Item("STATE").ToString()
                    lblAttZipCode10.Text = dsShip.Tables(0).Rows(0).Item("ZIPCODE").ToString()
                    lblAttCntry10.Text = dsShip.Tables(0).Rows(0).Item("COUNTRYDES").ToString()
                End If

            End If



            StrSql = objUpIns.FreeMail(Session("FConFid"), Session("UserId"), ArrAtt)

        Catch ex As Exception
            'ErrorLable.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub btnOrderClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOrderClose.Click
        Try
            Session("FConFid") = Nothing
            Response.Redirect("~\index.aspx")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Response.Redirect(HttpContext.Current.Request.Url.ToString(), True)
        Catch ex As Exception
            ErrorLable.Text = "Error:btnRefresh_Click:" + ex.Message.ToString()
        End Try
    End Sub
   
End Class
