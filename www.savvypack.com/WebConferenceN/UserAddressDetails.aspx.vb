Imports System
Imports System.Data
Partial Class WebConferenceN_UserAddressDetails
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("Back") = Nothing And Session("SBack") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("~/Universal_loginN/Pages/ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "", False)
            End If

            If Not IsPostBack Then
                BindCountry()
                BindState(ddlCountry.SelectedValue)
                BindStateE(ddlCountryE.SelectedValue)
                hidBillToShipToid.Value = "0"
            End If

            'If Session("Back") = Nothing Then
            '    Dim obj As New CryptoHelper
            '    Response.Redirect("~/Universal_loginN/Pages/ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "", False)
            'Else
            '    If Not IsPostBack Then
            '        BindCountry()
            '        BindState(ddlCountry.SelectedValue)
            '        BindStateE(ddlCountryE.SelectedValue)
            '        hidBillToShipToid.Value = "0"
            '    End If
            'End If

            BindGrid()

            hidAddDes.Value = Request.QueryString("lnkDesId").ToString()
            hidAddID.Value = Request.QueryString("lnkhdnID").ToString()
            hidAddDesc.Value = Request.QueryString("lnkhdnDes").ToString()
            hidType.Value = Request.QueryString("Type").ToString()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub BindGrid()
        Dim ds As New DataSet
        Dim objGetData As New UsersGetData.Selectdata()
        Try
            If (Request.QueryString("Type").ToString() = "Y") Then
                ds = objGetData.GetUserAddressByUserID(Session("USERID").ToString(), "Y")
                hidType.Value = "Y"
            Else
                ds = objGetData.GetUserAddressByUserID(Session("USERID").ToString(), "N")
                hidType.Value = "N"
            End If
            If ds.Tables(0).Rows.Count > 0 Then
                grdUserAddress.DataSource = ds
                Session("UsersData") = ds
                grdUserAddress.DataBind()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub BindCountry()
        Dim objGetData As New UsersGetData.Selectdata()
        Dim ds As New DataSet
        Try
            ds = objGetData.GetCountry()
            'new user country dropdown
            With ddlCountry
                .DataValueField = "COUNTRYID"
                .DataTextField = "COUNTRYDES"
                .DataSource = ds
                .DataBind()
            End With
            ddlCountry.SelectedValue = 175

            'Edit User Country Dropdown
            With ddlCountryE
                .DataValueField = "COUNTRYID"
                .DataTextField = "COUNTRYDES"
                .DataSource = ds
                .DataBind()
            End With
            ddlCountryE.SelectedValue = 175
        Catch ex As Exception
            _lErrorLble.Text = "Error:BindCountry:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub BindState(ByVal CountryId As Integer)
        Dim objGetData As New UsersGetData.Selectdata()
        Dim ds As New DataSet
        Try
            Dim list As New ListItem
            ddlState.Items.Clear()
            If ddlState.Items.Count = 0 Then
                list.Value = "0"
                list.Text = "Nothing Selected"
                ddlState.Items.Add(list)
            End If

            ddlState.AppendDataBoundItems = True
            If CountryId <> 0 Then
                ' ddlState.Items.Clear()
                ds = objGetData.GetStateByCountry(CountryId)
                With ddlState
                    .DataValueField = "STATEID"
                    .DataTextField = "NAME"
                    .DataSource = ds
                    .DataBind()
                End With
                ddlState.SelectedValue = "0"
            End If


        Catch ex As Exception
            _lErrorLble.Text = "Error:BindState:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub BindStateE(ByVal CountryId As Integer)
        Dim objGetData As New UsersGetData.Selectdata()
        Dim ds As New DataSet
        Try
            Dim list As New ListItem
            'Add User
            ddlStateE.Items.Clear()
            If ddlStateE.Items.Count = 0 Then
                list.Value = "0"
                list.Text = "Nothing Selected"
                ddlStateE.Items.Add(list)
            End If

            ddlStateE.AppendDataBoundItems = True
            If CountryId <> 0 Then
                'ddlStateE.Items.Clear()
                ds = objGetData.GetStateByCountry(CountryId)
                With ddlStateE
                    .DataValueField = "STATEID"
                    .DataTextField = "NAME"
                    .DataSource = ds
                    .DataBind()
                End With
                ddlStateE.SelectedValue = "0"
            End If


        Catch ex As Exception
            _lErrorLble.Text = "Error:BindStateE:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub ddlCountry_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCountry.SelectedIndexChanged
        Try
            '10th_jan_2017
            Dim CAvail As Integer = GetCountryInState(ddlCountry.SelectedValue.ToString())
            If CAvail <> 0 Then
                ddlState.Visible = True
                txtState.Visible = False
                BindState(ddlCountry.SelectedValue.ToString())
            Else
                ddlState.Visible = False
                txtState.Visible = True
                If ddlCountry.SelectedValue = "0" Then
                    txtState.Enabled = False
                Else
                    txtState.Enabled = True
                End If
            End If
            'end           
        Catch ex As Exception
            _lErrorLble.Text = "Error:ddlCountry_SelectedIndexChanged:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnReg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReg.Click
        Dim objGetData As New UsersGetData.Selectdata
        Dim objInsertData As New UsersUpdateData.UpdateInsert
        Dim ds As New DataSet
        Dim dsEmail As New DataSet
        Try
            If hidBillToShipToid.Value.ToString() = "0" Then
                'insert into the user & useraddress table
                '10th_jan_2017
                Dim CAvail As Integer = GetCountryInState(ddlCountry.SelectedValue.ToString())
                If CAvail <> 0 Then
                    objInsertData.InsertUserAddressDetails(txtEmail.Text.Trim(), txtAHeader.Text, txtCompanyName.Text.Replace("'", "''"), txtPrefix.Text.Replace("'", "''"), txtFirstName.Text.Replace("'", "''"), txtLastName.Text.Replace("'", "''"), txtPosition.Text.Replace("'", "''"), txtphone.Text.Replace("'", "''"), txtFax.Text.Replace("'", "''"), txtSAdress1.Text.Replace("'", "''"), txtSAdress2.Text.Replace("'", "''"), txtCity.Text.Replace("'", "''"), ddlState.SelectedItem.Text, txtZipCode.Text.Replace("'", "''"), ddlCountry.SelectedValue, Session("USERID").ToString())
                Else
                    objInsertData.InsertUserAddressDetails(txtEmail.Text.Trim(), txtAHeader.Text, txtCompanyName.Text.Replace("'", "''"), txtPrefix.Text.Replace("'", "''"), txtFirstName.Text.Replace("'", "''"), txtLastName.Text.Replace("'", "''"), txtPosition.Text.Replace("'", "''"), txtphone.Text.Replace("'", "''"), txtFax.Text.Replace("'", "''"), txtSAdress1.Text.Replace("'", "''"), txtSAdress2.Text.Replace("'", "''"), txtCity.Text.Replace("'", "''"), txtState.Text.Replace("'", "''"), txtZipCode.Text.Replace("'", "''"), ddlCountry.SelectedValue, Session("USERID").ToString())
                End If
                'If ddlCountry.SelectedItem.Text = "United States" Then
                '    objInsertData.InsertUserAddressDetails(txtEmail.Text.Trim(), txtAHeader.Text, txtCompanyName.Text.Replace("'", "''"), txtPrefix.Text.Replace("'", "''"), txtFirstName.Text.Replace("'", "''"), txtLastName.Text.Replace("'", "''"), txtPosition.Text.Replace("'", "''"), txtphone.Text.Replace("'", "''"), txtFax.Text.Replace("'", "''"), txtSAdress1.Text.Replace("'", "''"), txtSAdress2.Text.Replace("'", "''"), txtCity.Text.Replace("'", "''"), ddlState.SelectedItem.Text, txtZipCode.Text.Replace("'", "''"), ddlCountry.SelectedValue, Session("USERID").ToString())
                'Else
                '    objInsertData.InsertUserAddressDetails(txtEmail.Text.Trim(), txtAHeader.Text, txtCompanyName.Text.Replace("'", "''"), txtPrefix.Text.Replace("'", "''"), txtFirstName.Text.Replace("'", "''"), txtLastName.Text.Replace("'", "''"), txtPosition.Text.Replace("'", "''"), txtphone.Text.Replace("'", "''"), txtFax.Text.Replace("'", "''"), txtSAdress1.Text.Replace("'", "''"), txtSAdress2.Text.Replace("'", "''"), txtCity.Text.Replace("'", "''"), txtState.Text.Replace("'", "''"), txtZipCode.Text.Replace("'", "''"), ddlCountry.SelectedValue, Session("USERID").ToString())
                'End If
                lblMessage.Text = "User created successfully and added as a BillTo/ShipTo/Card Holder User Address."
                ResetValues()
                BindGrid()
            End If

        Catch ex As Exception
            _lErrorLble.Text = "Error:btnReg_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub txtEmail_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEmail.TextChanged
        Try
            Dim ds As New DataSet
            Dim objGetData As New UsersGetData.Selectdata
            rowcheckuser.Visible = False
            If txtEmail.Text.Trim().Length = 0 Then
                Exit Sub
            End If
            'Check whether User Already exists or not
            ds = objGetData.GetUserAddressByNameID(txtEmail.Text, Session("USERID").ToString())
            If ds.Tables(0).Rows.Count > 0 Then 'User exists
                rowcheckuser.Visible = True
            Else
                rowcheckUAdd.Visible = False
                rowcheckuser.Visible = False
                EnableTxts(True)
                txtCEmail.Text = ""
                txtAHeader.Text = ""
                txtPrefix.Text = ""
                txtFirstName.Text = ""
                txtLastName.Text = ""
                txtphone.Text = ""
                txtFax.Text = ""
                txtCompanyName.Text = ""
                txtPosition.Text = ""
                txtSAdress1.Text = ""
                txtSAdress2.Text = ""
                txtCity.Text = ""
                txtState.Text = ""
                ddlState.SelectedValue = "0"
                txtZipCode.Text = ""
                ddlCountry.SelectedValue = "175"
                BindState(ddlCountry.SelectedValue)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub txtAHeader_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAHeader.TextChanged
        Try
            Dim ds As New DataSet
            Dim objGetData As New UsersGetData.Selectdata
            rowcheckuser.Visible = False
            If txtEmail.Text.Trim().Length = 0 Then
                Exit Sub
            End If
            'Check whether User Already exists or not
            ds = objGetData.GetUserAddressByNameHeadID(txtEmail.Text, txtAHeader.Text, Session("USerID"))
            If ds.Tables(0).Rows.Count > 0 Then 'User exists
                EnableTxts(False)
                rowcheckUAdd.Visible = True
                btnReg.Enabled = "false"
            Else
                rowcheckUAdd.Visible = False
                EnableTxts(True)

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub EnableTxts(ByVal val As String)
        Try
            Dim css As String
            If val = "True" Then
                css = "EnableTxts"
                hidBillToShipToid.Value = "0"
            Else
                css = "DisableTxts"
            End If
            txtFax.Enabled = val
            txtFax.CssClass = css
            txtFirstName.Enabled = val
            txtFirstName.CssClass = css
            txtLastName.Enabled = val
            txtLastName.CssClass = css
            txtphone.Enabled = val
            txtphone.CssClass = css
            txtPosition.Enabled = val
            txtPosition.CssClass = css
            txtPrefix.Enabled = val
            txtPrefix.CssClass = css
            txtSAdress1.Enabled = val
            txtSAdress1.CssClass = css
            txtSAdress2.Enabled = val
            txtSAdress2.CssClass = css
            txtState.Enabled = val
            txtState.CssClass = css
            txtZipCode.Enabled = val
            txtZipCode.CssClass = css
            txtCity.Enabled = val
            txtCity.CssClass = css
            txtCompanyName.Enabled = val
            txtCompanyName.CssClass = css
            ddlCountry.Enabled = val
            ddlCountry.CssClass = css
            ddlState.Enabled = val
            ddlState.CssClass = css

        Catch ex As Exception
            _lErrorLble.Text = "Error:EnableTxts :" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub ResetValues()
        Try
            txtEmail.Text = ""
            txtCEmail.Text = ""
            txtAHeader.Text = ""
            txtPrefix.Text = ""
            txtFirstName.Text = ""
            txtLastName.Text = ""
            txtphone.Text = ""
            txtFax.Text = ""
            txtCompanyName.Text = ""
            txtPosition.Text = ""
            txtSAdress1.Text = ""
            txtSAdress2.Text = ""
            txtCity.Text = ""
            txtState.Text = ""
            ddlState.SelectedValue = "0"
            txtZipCode.Text = ""
            ddlCountry.SelectedValue = "175"
            BindState(ddlCountry.SelectedValue)

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub grdEditUserAdd_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdUserAddress.SelectedIndexChanged
        Try
            Dim obj As New CryptoHelper
            Dim UserAddId As New Integer
            UserAddId = Convert.ToInt32(grdUserAddress.SelectedDataKey.Value)
            divGrid.Visible = False
            hvUserGrd.Value = UserAddId
            lblMessage.Visible = False
            GetUserData(UserAddId)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lunkEdit_User(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Public Sub GetUserData(ByVal userId As Integer)
        Try
            Dim objGetData As New UsersGetData.Selectdata()
            Dim ds As New DataSet()
            ds = objGetData.GetBillToShipToUserDetailsByAddID(userId)
            If ds.Tables(0).Rows.Count > 0 Then
                txtpreFixE.Text = ds.Tables(0).Rows(0).Item("PREFIX").ToString()
                txtAddHeader.Text = ds.Tables(0).Rows(0).Item("ADDHEADER").ToString()
                txtFNameE.Text = ds.Tables(0).Rows(0).Item("FIRSTNAME").ToString()
                txtLNameE.Text = ds.Tables(0).Rows(0).Item("LASTNAME").ToString()
                txtphoneE.Text = ds.Tables(0).Rows(0).Item("PHONENUMBER").ToString()
                txtFaxE.Text = ds.Tables(0).Rows(0).Item("FAXNUMBER").ToString()
                txtEmailE.Text = ds.Tables(0).Rows(0).Item("EMAILADDRESS").ToString()
                txtCompNameE.Text = ds.Tables(0).Rows(0).Item("COMPANYNAME").ToString()
                txtposE.Text = ds.Tables(0).Rows(0).Item("JOBTITLE").ToString()
                txtStAddress1E.Text = ds.Tables(0).Rows(0).Item("STREETADDRESS1").ToString()
                txtStAddress2E.Text = ds.Tables(0).Rows(0).Item("STREETADDRESS2").ToString()
                txtCityE.Text = ds.Tables(0).Rows(0).Item("CITY").ToString()
                txtZipCodeE.Text = ds.Tables(0).Rows(0).Item("ZIPCODE").ToString()
                If ds.Tables(0).Rows(0).Item("COUNTRY").ToString() = "" Then
                    ddlCountryE.SelectedValue = 0
                Else
                    ddlCountryE.SelectedValue = ds.Tables(0).Rows(0).Item("COUNTRYID").ToString()
                End If

                '10th_jan_2017
                Dim CAvail As Integer = GetCountryInState(ds.Tables(0).Rows(0).Item("COUNTRYID").ToString())
                If CAvail <> 0 Then
                    BindStateE(ddlCountryE.SelectedValue)
                    ddlStateE.SelectedValue = GetStateId(ds.Tables(0).Rows(0).Item("STATE").ToString())
                    ddlStateE.Visible = True
                    txtStateE.Visible = False
                Else
                    ddlStateE.Visible = False
                    txtStateE.Visible = True
                    txtStateE.Text = ds.Tables(0).Rows(0).Item("STATE").ToString()
                End If
                'End

                'If ds.Tables(0).Rows(0).Item("COUNTRYID").ToString() = "175" Then
                '    ddlStateE.SelectedValue = GetStateId(ds.Tables(0).Rows(0).Item("STATE").ToString())
                '    ddlStateE.Visible = True
                '    txtStateE.Visible = False
                'Else
                '    ddlStateE.Visible = False
                '    txtStateE.Visible = True
                '    txtStateE.Text = ds.Tables(0).Rows(0).Item("STATE").ToString()
                'End If


            End If

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetUserData:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Function GetStateId(ByVal stateDes As String) As Integer
        Dim objGetData As New UsersGetData.Selectdata
        Dim ds As New DataSet
        Try
            ds = objGetData.GetStateByDes(stateDes)
            If (ds.Tables(0).Rows(0).Item("STATEID").ToString() = "") Then
                Return (0)
            Else
                Return CInt(ds.Tables(0).Rows(0).Item("STATEID").ToString())
            End If

        Catch ex As Exception
            Return (0)
        End Try

    End Function

    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Try

            Dim objUpdateData As New UsersUpdateData.UpdateInsert
            Dim ds As New DataSet
            Dim dsEmail As New DataSet
            Dim objGetData As New UsersGetData.Selectdata()
            ds = objGetData.GetUserDetails(hvUserGrd.Value.ToString())
            '10th_jan_2017
            Dim CAvail As Integer = GetCountryInState(ddlCountryE.SelectedValue.ToString())
            If CAvail <> 0 Then
                objUpdateData.UpdateUserAddressDetails(hvUserGrd.Value.ToString(), txtCompNameE.Text.Replace("'", "''"), txtpreFixE.Text.Replace("'", "''"), txtFNameE.Text.Replace("'", "''"), txtLNameE.Text.Replace("'", "''"), txtposE.Text.Replace("'", "''"), txtphoneE.Text.Replace("'", "''"), txtFaxE.Text.Replace("'", "''"), txtStAddress1E.Text.Replace("'", "''"), txtStAddress2E.Text.Replace("'", "''"), txtCityE.Text.Replace("'", "''"), ddlStateE.SelectedItem.Text, txtZipCodeE.Text.Replace("'", "''"), ddlCountryE.SelectedValue)
            Else
                objUpdateData.UpdateUserAddressDetails(hvUserGrd.Value.ToString(), txtCompNameE.Text.Replace("'", "''"), txtpreFixE.Text.Replace("'", "''"), txtFNameE.Text.Replace("'", "''"), txtLNameE.Text.Replace("'", "''"), txtposE.Text.Replace("'", "''"), txtphoneE.Text.Replace("'", "''"), txtFaxE.Text.Replace("'", "''"), txtStAddress1E.Text.Replace("'", "''"), txtStAddress2E.Text.Replace("'", "''"), txtCityE.Text.Replace("'", "''"), txtStateE.Text.Replace("'", "''"), txtZipCodeE.Text.Replace("'", "''"), ddlCountryE.SelectedValue)
            End If

            'lblMessage.Text = "User Updated Successfully."
            lblMessage.Visible = True
            hvUserGrd.Value = "0"
            rowCngPwd.Visible = True
            rowEditUser.Visible = False
            BindGrid()
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('User Updated Successfully.');", True)
        Catch ex As Exception
            _lErrorLble.Text = "Error:btnUpdate_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub lnkEditUser_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim id As Integer
        Try
            id = Convert.ToInt32(grdUserAddress.DataKeys(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridViewRow).RowIndex).Value)
            If id <> 0 Then
                hvUserGrd.Value = id
                GetUserData(hvUserGrd.Value)
                rowEditUser.Visible = True
                rowCngPwd.Visible = False
            Else
                rowEditUser.Visible = False
                rowCngPwd.Visible = True
                hvUserGrd.Value = "0"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub grdEditUserAdd_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdUserAddress.Sorting
        Try
            Dim Dts As New DataSet
            Dim dv As DataView
            Dim numberDiv As Integer
            numberDiv = Convert.ToInt16(hvUserGrd.Value.ToString())
            Dts = Session("UsersData")
            dv = Dts.Tables(0).DefaultView

            If ((numberDiv Mod 2) = 0) Then
                dv.Sort = e.SortExpression + " " + "DESC"
            Else
                dv.Sort = e.SortExpression + " " + "ASC"
            End If

            numberDiv += 1
            hvUserGrd.Value = numberDiv.ToString()
            grdUserAddress.DataSource = dv
            grdUserAddress.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnCancelE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelE.Click
        Try
            rowEditUser.Visible = False
            rowCngPwd.Visible = True
            hvUserGrd.Value = "0"
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlCountryE_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCountryE.SelectedIndexChanged
        Try
            '10th_jan_2017
            Dim CAvail As Integer = GetCountryInState(ddlCountryE.SelectedValue.ToString())
            If CAvail <> 0 Then
                ddlStateE.Visible = True
                txtStateE.Visible = False
                BindStateE(ddlCountryE.SelectedValue.ToString())
            Else
                ddlStateE.Visible = False
                txtStateE.Visible = True
                If ddlCountryE.SelectedValue = "0" Then
                    txtStateE.Enabled = False
                Else
                    txtStateE.Enabled = True
                End If
            End If
            'end  
        Catch ex As Exception
            _lErrorLble.Text = "Error:ddlCountry_SelectedIndexChanged:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub grdUserAddress_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdUserAddress.PageIndexChanging
        Try
            grdUserAddress.PageIndex = e.NewPageIndex
            BindUserGridUsingSession()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub BindUserGridUsingSession()
        Try
            Dim Dts As New DataSet
            Dts = Session("UsersData")
            grdUserAddress.DataSource = Dts
            grdUserAddress.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkUser_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim id As Integer
        Dim objGetData As New ShopGetData.Selectdata()
        Dim ds As New DataSet()
        Dim countTotal As Integer
        Dim objUserData As New UsersGetData.Selectdata()
        Dim dsUser As New DataSet()
        Dim UserName As String = ""
        Try
            'getting hidden Field Value

            ds = objGetData.GetOrderReview(Session("WRefNumber").ToString())
            If ds.Tables(0).Rows.Count > 0 Then
                countTotal = ds.Tables(0).Rows(0).Item("UNITCOST").ToString()
            End If

            id = Convert.ToInt32(grdUserAddress.DataKeys(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridViewRow).RowIndex).Value)
            dsUser = objUserData.GetUserAddresDetailByID(id.ToString())
            If dsUser.Tables(0).Rows.Count > 0 Then
                UserName = dsUser.Tables(0).Rows(0).Item("USERNAME").ToString() + "     " + dsUser.Tables(0).Rows(0).Item("ADDHEADER").ToString()
            End If
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "AddressDet('" + UserName + "','" + id.ToString() + "','" + countTotal.ToString() + "');", True)
        Catch ex As Exception

        End Try
    End Sub

    '10th_Jan_2018
    Protected Function GetCountryInState(ByVal CountryID As String) As Integer
        Dim objGetData As New UsersGetData.Selectdata
        Dim ds As New DataSet
        Try
            ds = objGetData.CheckCountryInState(CountryID)
            If (ds.Tables(0).Rows.Count > 0) Then
                Return (1)
            Else
                Return (0)
            End If
        Catch ex As Exception
            Return (0)
        End Try
    End Function

End Class
