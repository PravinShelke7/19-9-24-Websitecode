Imports System.Data
Imports System.Data.OleDb
Imports System
Imports SavvyProGetData
Imports SavvyProUpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Imports System.Net.Mail
Imports System.Net
Partial Class Pages_SavvyPackPro_PopUp_AddEditVendorInfo
    Inherits System.Web.UI.Page
    Dim objGetdata As New SavvyProGetData
    Dim objUpIns As New SavvyProUpInsData

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                BindCountry1()
                BindState1(ddlCountry.SelectedValue)
                ddlState.Visible = True
                txtState.Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub BindCountry1()
        Dim ds As New DataSet
        Try
            ds = objGetdata.GetCountry1()
            'new user country dropdown
            With ddlCountry
                .DataValueField = "COUNTRYID"
                .DataTextField = "COUNTRYDES"
                .DataSource = ds
                .DataBind()
            End With
            ddlCountry.SelectedValue = 175
            lblCountryCode.Text = objGetdata.GetCountryCode(ddlCountry.SelectedValue.ToString())
        Catch ex As Exception
            lblError.Text = "Error:BindCountry1:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub BindState1(ByVal CountryId As Integer)
        Dim ds As New DataSet
        Try
            Dim list As New ListItem
            'Add User
            ddlState.Items.Clear()
            list.Value = "0"
            list.Text = "Nothing Selected"
            ddlState.Items.Add(list)
            ddlState.AppendDataBoundItems = True
            If CountryId <> 0 Then
                ds = objGetdata.GetStateByCountry(CountryId)
                With ddlState
                    .DataValueField = "STATEID"
                    .DataTextField = "NAME"
                    .DataSource = ds
                    .DataBind()
                End With
                ddlState.SelectedValue = "0"
            End If
        Catch ex As Exception
            lblError.Text = "Error:BindState1:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Function GetCountryInState(ByVal CountryID As String) As Integer
        Dim ds As New DataSet
        Try
            ds = objGetdata.CheckCountryInState(CountryID)
            If (ds.Tables(0).Rows.Count > 0) Then
                Return (1)
            Else
                Return (0)
            End If
        Catch ex As Exception
            Return (0)
        End Try

    End Function

#Region "Add User"

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            Dim CAvail As Integer = GetCountryInState(ddlCountry.SelectedValue.ToString())
            If CAvail <> 0 Then
                objUpIns.AddVendorByUserID(Session("USERID"), txtEmail.Text.Trim.Replace("'", "''").ToString(), txtCompany.Text.Trim.Replace("'", "''"), _
                                        txtPrefix.Text.Trim.Replace("'", "''"), txtFname.Text.Trim.Replace("'", "''"), txtLname.Text.Trim.Replace("'", "''"), _
                                        txrtPhne.Text.Trim.Replace("'", "''").ToString(), txtFax.Text.Trim.Replace("'", "''").ToString(), txtPosition.Text.Trim.Replace("'", "''"), _
                                        txtStrAdd1.Text.Trim.Replace("'", "''"), txtStrAdd2.Text.Trim.Replace("'", "''"), txtCity.Text.Trim.Replace("'", "''").ToString(), _
                                        ddlState.SelectedItem.Text.Replace("'", "''"), txtZip.Text.Trim.Replace("'", "''").ToString(), ddlCountry.SelectedValue.Replace("'", "''"), _
                                        txtMob.Text.Replace("'", "''"))
            Else
                objUpIns.AddVendorByUserID(Session("USERID"), txtEmail.Text.Trim.Replace("'", "''").ToString(), txtCompany.Text.Trim.Replace("'", "''"), _
                                        txtPrefix.Text.Trim.Replace("'", "''"), txtFname.Text.Trim.Replace("'", "''"), txtLname.Text.Trim.Replace("'", "''"), _
                                        txrtPhne.Text.Trim.Replace("'", "''").ToString(), txtFax.Text.Trim.Replace("'", "''").ToString(), txtPosition.Text.Trim.Replace("'", "''"), _
                                        txtStrAdd1.Text.Trim.Replace("'", "''"), txtStrAdd2.Text.Trim.Replace("'", "''"), txtCity.Text.Trim.Replace("'", "''").ToString(), _
                                        txtState.Text.Trim.Replace("'", "''").ToString(), txtZip.Text.Trim.Replace("'", "''").ToString(), ddlCountry.SelectedValue.Replace("'", "''"), _
                                        txtMob.Text.Replace("'", "''"))
            End If
            ScriptManager.RegisterStartupScript(Page, Me.GetType(), "JSScript", "ClosePage();", True)
        Catch ex As Exception
            lblError.Text = "Error:btnAdd_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub ddlCountry_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCountry.SelectedIndexChanged
        Try
            If ddlCountry.SelectedValue = "0" Then
                lblCountryCode.Text = "[NA]"
            Else
                lblCountryCode.Text = objGetdata.GetCountryCode(ddlCountry.SelectedValue.ToString())
            End If

            Dim CAvail As Integer = GetCountryInState(ddlCountry.SelectedValue.ToString())
            If CAvail <> 0 Then
                ddlState.Visible = True
                txtState.Visible = False
                BindState1(ddlCountry.SelectedValue.ToString())
            Else
                ddlState.Visible = False
                txtState.Visible = True
                If ddlCountry.SelectedValue = "0" Then
                    txtState.Enabled = False
                Else
                    txtState.Enabled = True
                End If
            End If

        Catch ex As Exception
            lblError.Text = "Error:ddlCountry_SelectedIndexChanged:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub txtEmail_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEmail.TextChanged
        Dim UserNm As String = String.Empty
        Dim DsDomain As New DataSet()
        Dim dsUserC As New DataSet()
        Dim dsExistUser As New DataSet()
        Try
            If txtEmail.Text.Trim.Length <> 0 Then
                trUsername.Visible = True
                dsUserC = objGetdata.CheckVendorExist(txtEmail.Text.Trim.Replace("'", "''").ToString(), Session("USERID"))
                If dsUserC.Tables(0).Rows.Count > 0 Then
                    lblUserAv.Text = "The Vendor Email Address <b>" + txtEmail.Text.Trim.ToString() + " </b> is already added."
                    lblUserAv.CssClass = "NotAvailable"
                    btnAdd.Enabled = False
                    txtEmail.Focus()
                Else
                    lblUserAv.Text = "The Vendor Email Address <b>" + txtEmail.Text.Trim.ToString() + " </b> is available."
                    lblUserAv.CssClass = "Available"
                    btnAdd.Enabled = True
                    txtCEmail.Focus()
                End If
            End If

        Catch ex As Exception
            lblError.Text = "Error:txtEmail_TextChanged:" + ex.Message.ToString()
        End Try
    End Sub

#End Region

End Class
