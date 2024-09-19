Imports System.Data
Imports System.Data.OleDb
Imports System
Imports LoginGetData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class DownLoad_Error
    Inherits System.Web.UI.Page
    Dim _strErrorCode As String
    Dim _strSchema As String

    Public Property ErrorCode() As String
        Get
            Return _strErrorCode
        End Get
        Set(ByVal value As String)
            Dim obj As New CryptoHelper
            _strErrorCode = obj.Decrypt(value)
        End Set
    End Property
    Public Property Schema() As String
        Get
            Return _strSchema
        End Get
        Set(ByVal value As String)
            Dim obj As New CryptoHelper
            _strSchema = obj.Decrypt(value)
        End Set
    End Property
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            ErrorCode = Request.QueryString("ErrorCode").ToString().Replace("!Plus!", "+").Replace("!Hash!", "#").Replace("!And!", "&")
            If Request.QueryString("Schema") <> Nothing Then
                Schema = Request.QueryString("Schema").ToString().Replace("!Plus!", "+").Replace("!Hash!", "#").Replace("!And!", "&")
            End If



            GetPageDetails()
        Catch ex As Exception
            lblError.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub GetPageDetails()
        Dim objGetData As New E1GetData.Selectdata()
        Dim ds As New DataSet()
        Try
            ds = objGetData.GetErrors(ErrorCode)
            lblErrorCode.Text = ds.Tables(0).Rows(0).Item("ERRORCODE").ToString()
            lblErrorMessage.Text = ds.Tables(0).Rows(0).Item("ERRORDE1").ToString()
            If ds.Tables(0).Rows(0).Item("ERRORTYPE").ToString() = "LOGIN" Then
                divUpdate.Visible = True
                If ErrorCode = "ALDE115" Then
                    lblText.Text = " to go to the LogOn Renewal Page."
                    hypPage.NavigateUrl = "ULogRenewal.aspx"
                   
                ElseIf ErrorCode = "ALDE114" Then
                    lblText.Text = " to go SavvyPack Corporation Home Page."
                    hypPage.NavigateUrl = "~/Index.aspx"
                ElseIf ErrorCode = "ALDE113" Then
                    lblText.Text = " to go Previous Page."
                    hypPage.NavigateUrl = "../" + DirectCast(Request.UrlReferrer, System.Uri).Segments(Request.UrlReferrer.Segments.Length - 2) + DirectCast(Request.UrlReferrer, System.Uri).Segments(Request.UrlReferrer.Segments.Length - 1) + ""
                ElseIf ErrorCode = "ALDE116" Then
                    lblText.Text = " to go User Management Page."
                    hypPage.NavigateUrl = "CorpUser.aspx"
                End If


            Else
                divUpdate.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub
End Class
