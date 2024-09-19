Imports System.Data
Imports System.Data.OleDb
Imports System
Partial Class ShoppingCart_UserInfoEdit
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            hidUserID.Value = Request.QueryString("ShipToId").ToString()
            If Session("Back") = Nothing Then
                If Session("SBack") = Nothing Then
                    Dim obj As New CryptoHelper
                    Response.Redirect("~/Universal_loginN/Pages/ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "", False)
                End If
            End If

            SetFocusOnSubmit()

            If Not IsPostBack Then
                BindCountry()
                BindState(ddlCountryE.SelectedValue)
                GetUserInfo()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub SetFocusOnSubmit()
        Try
            txtEmailE.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnUpdate.ClientID + "')")
            txtpreFixE.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnUpdate.ClientID + "')")
            txtFNameE.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnUpdate.ClientID + "')")
            txtLNameE.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnUpdate.ClientID + "')")
            txtphoneE.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnUpdate.ClientID + "')")
            txtFaxE.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnUpdate.ClientID + "')")
            txtCompNameE.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnUpdate.ClientID + "')")
            txtposE.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnUpdate.ClientID + "')")
            txtStAddress1E.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnUpdate.ClientID + "')")
            txtStAddress2E.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnUpdate.ClientID + "')")
            txtCityE.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnUpdate.ClientID + "')")
            txtStateE.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnUpdate.ClientID + "')")
            txtZipCodeE.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnUpdate.ClientID + "')")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub BindCountry()
        Dim objGetData As New UsersGetData.Selectdata()
        Dim ds As New DataSet
        Try
            ds = objGetData.GetCountry()
            'Edit User Country Dropdown
            With ddlCountryE
                .DataValueField = "COUNTRYID"
                .DataTextField = "COUNTRYDES"
                .DataSource = ds
                .DataBind()
            End With

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub BindState(ByVal CountryId As Integer)
        Dim objGetData As New UsersGetData.Selectdata()
        Dim ds As New DataSet
        Try
            Dim list As New ListItem
            'Edit User
            list = New ListItem
            list.Value = "0"
            list.Text = "Nothing Selected"
            ddlStateE.Items.Add(list)
            ddlStateE.AppendDataBoundItems = True
            If CountryId <> 0 Then
                ds = objGetData.GetState()
                With ddlStateE
                    .DataValueField = "STATEID"
                    .DataTextField = "NAME"
                    .DataSource = ds
                    .DataBind()
                End With
                ddlStateE.SelectedValue = "0"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub GetUserInfo()
        Dim UserNm As String = String.Empty
        Dim DsDomain As New DataSet()
        Dim CompanyName As String = String.Empty
        Dim lst As New ListItem
        Dim DomNotAvail As Boolean = False
        Dim ComNotAvail As Boolean = False
        Try
            Dim objGetData As New UsersGetData.Selectdata()
            Dim ds As New DataSet()
            ds = objGetData.GetBillToShipToUserDetailsByAddID(hidUserID.Value)

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

                If ds.Tables(0).Rows(0).Item("COUNTRYID").ToString() = "175" Then
                    ddlStateE.SelectedValue = GetStateId(ds.Tables(0).Rows(0).Item("STATE").ToString())
                    ddlStateE.Visible = True
                    txtStateE.Visible = False
                Else
                    ddlStateE.Visible = False
                    txtStateE.Visible = True
                    txtStateE.Text = ds.Tables(0).Rows(0).Item("STATE").ToString()
                End If
            End If

        Catch ex As Exception

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

    Protected Sub ddlCountryE_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCountryE.SelectedIndexChanged
        Try
            If ddlCountryE.SelectedItem.Text = "United States" Then
                ddlStateE.Visible = True
                txtStateE.Visible = False
                BindState("0")
            Else
                ddlStateE.Visible = False
                txtStateE.Visible = True
                If ddlCountryE.SelectedValue = "0" Then
                    txtStateE.Enabled = False
                Else
                    txtStateE.Enabled = True
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Try

            Dim objUpdateData As New UsersUpdateData.UpdateInsert
            Dim ds As New DataSet
            Dim dsEmail As New DataSet
            Dim objGetData As New UsersGetData.Selectdata()
            If ddlCountryE.SelectedItem.Text = "United States" Then
                objUpdateData.UpdateUserAddressDetails(hidUserID.Value.ToString(), txtCompNameE.Text.Replace("'", "''"), txtpreFixE.Text.Replace("'", "''"), txtFNameE.Text.Replace("'", "''"), txtLNameE.Text.Replace("'", "''"), txtposE.Text.Replace("'", "''"), txtphoneE.Text.Replace("'", "''"), txtFaxE.Text.Replace("'", "''"), txtStAddress1E.Text.Replace("'", "''"), txtStAddress2E.Text.Replace("'", "''"), txtCityE.Text.Replace("'", "''"), ddlStateE.SelectedItem.Text, txtZipCodeE.Text.Replace("'", "''"), ddlCountryE.SelectedValue)
            Else
                objUpdateData.UpdateUserAddressDetails(hidUserID.Value.ToString(), txtCompNameE.Text.Replace("'", "''"), txtpreFixE.Text.Replace("'", "''"), txtFNameE.Text.Replace("'", "''"), txtLNameE.Text.Replace("'", "''"), txtposE.Text.Replace("'", "''"), txtphoneE.Text.Replace("'", "''"), txtFaxE.Text.Replace("'", "''"), txtStAddress1E.Text.Replace("'", "''"), txtStAddress2E.Text.Replace("'", "''"), txtCityE.Text.Replace("'", "''"), txtStateE.Text.Replace("'", "''"), txtZipCodeE.Text.Replace("'", "''"), ddlCountryE.SelectedValue)
            End If

            Page.ClientScript.RegisterStartupScript(Me.GetType(), "UserLogin", "CloseWindow();", True)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnCancelE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelE.Click
        Try
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "UserLogin", "window.close();", True)
        Catch ex As Exception

        End Try
    End Sub
End Class
