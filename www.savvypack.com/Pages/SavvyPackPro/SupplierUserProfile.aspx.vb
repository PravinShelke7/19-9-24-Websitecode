Imports Microsoft.VisualBasic
Imports System.Web.SessionState
Imports System.Data
Imports System.Data.OleDb
Imports System
Partial Class Pages_SavvyPackPro_SupplierUserProfile
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            Try
                lblFooter.Text = "Copyright 1997 - " + Now.Year().ToString() + " SavvyPack Corporation"
            Catch ex As Exception
            End Try

            If Session("SBack") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If

            If Session("USERID") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If

            If Not IsPostBack Then
                GetUserDetails()
            End If

            If tabBuyerManager.ActiveTabIndex = "1" Then
                GetUserDetails()
            End If

        Catch ex As Exception
            lblError.Text = "Error: Page_Load() " + ex.Message()
        End Try

    End Sub

    Public Sub GetUserDetails()
        Dim ds As New DataSet
        Dim objGetdata As New SavvyProGetData
        Try

            ds = objGetdata.GetUserProfileDetail(Session("USERID"))
            lblFname.Text = ds.Tables(0).Rows(0).Item("FIRSTNAME").ToString()
            lblLname.Text = ds.Tables(0).Rows(0).Item("LASTNAME").ToString()
            lblEAdrs.Text = ds.Tables(0).Rows(0).Item("EMAILADDRESS").ToString()
            lblPN.Text = ds.Tables(0).Rows(0).Item("PHONENUMBER").ToString()
            lblMN.Text = ds.Tables(0).Rows(0).Item("MOBILENUMBER").ToString()
            lblSaddr.Text = ds.Tables(0).Rows(0).Item("STREETADDRESS").ToString()
            lblCtry.Text = ds.Tables(0).Rows(0).Item("COUNTRY").ToString()
            lblS.Text = ds.Tables(0).Rows(0).Item("STATE").ToString()
            lblC.Text = ds.Tables(0).Rows(0).Item("CITY").ToString()
            lblZC.Text = ds.Tables(0).Rows(0).Item("ZIPCODE").ToString()
        Catch ex As Exception

        End Try
    End Sub
End Class
