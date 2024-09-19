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
Partial Class Users_Login_UserCardDetails1
    Inherits System.Web.UI.Page
    Dim RefNum As String = String.Empty
    Dim type As String = String.Empty

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            RefNum = Request.QueryString("RefNum").ToString()
            type = Request.QueryString("Type").ToString()

            If Session("Back") = Nothing Then
                If Session("SBack") = Nothing Then
                    Dim obj As New CryptoHelper
                    Response.Redirect("~/Universal_loginN/Pages/ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "", False)
                End If
            End If
            If Not IsPostBack Then
                GetExpirayDate()
                GetDetails()
            End If

        Catch ex As Exception

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

        End Try
    End Sub

    Public Sub GetDetails()
        Dim objGetData As New Selectdata()
        Dim dsCust As New DataSet()
        Try
            dsCust = objGetData.GetCustInfo(RefNum)
            If dsCust.Tables(0).Rows.Count > 0 Then
                ddlCCType.SelectedValue = dsCust.Tables(0).Rows(0).Item("CARDTYPE").ToString()
                txtCC.Text = dsCust.Tables(0).Rows(0).Item("CARDNUMBER").ToString()
                txtAuthCode.Text = dsCust.Tables(0).Rows(0).Item("AUTH_CODE").ToString()
                Dim words As String() = dsCust.Tables(0).Rows(0).Item("CARDEXP").ToString().Split("/")
                ddlMonth.SelectedValue = words(0)
                ddlYears.SelectedValue = words(1)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnOrder_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOrder.Click
        Dim objUpIns As New UpdateInsert()
        Dim ExpDate As String = String.Empty
        Try
            If RefNum.ToString() <> "" And Session("UserId").ToString() <> "" Then
                ExpDate = ddlMonth.SelectedItem.Value.ToString() + "/" + ddlYears.SelectedItem.Value.ToString()
                objUpIns.UpdateCustomerCardInfo(RefNum, txtCC.Text.Trim(), ExpDate, ddlCCType.SelectedValue.Trim(), Session("UserId"), "CreditCard", txtAuthCode.Text)
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "UserLogin", "CloseWindow();", True)
            End If
        Catch ex As Exception
            Response.Write("")
        End Try
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "UserLogin", "window.close();", True)
        Catch ex As Exception

        End Try
    End Sub

End Class
