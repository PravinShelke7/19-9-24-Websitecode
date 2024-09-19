Imports System.Data
Imports System.Data.OleDb
Imports System
Imports SavvyGetData
Imports SavvyUpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_PopUp_ViewProducerDetails
    Inherits System.Web.UI.Page
    Dim objUpIns As New UpdateInsert()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("SBack") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If
            If Not IsPostBack Then
                PageDetails()
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    Public Sub PageDetails()
        Dim ds As New DataSet
        Dim objGetData As New Selectdata()
        Try
            ds = objGetData.GetProducerDetails(Request.QueryString("ProdId").ToString(), Request.QueryString("ProjId").ToString())

            lblName.Text = ds.Tables(0).Rows(0).Item("NAME").ToString()
            lblLoc.Text = ds.Tables(0).Rows(0).Item("LOCATION").ToString()
            lblCap.Text = ds.Tables(0).Rows(0).Item("CAPACITY").ToString()
            lblLines.Text = ds.Tables(0).Rows(0).Item("LINES").ToString()
            lblInfo.Text = ds.Tables(0).Rows(0).Item("INFORMATION").ToString()
            
            'Started Activity Log Changes
            Try
                objUpIns.InsertLog1(Session("UserId").ToString(), "ViewProducerDetails.aspx", "Opened Producer Popup to View Producer Details for Model:" + Request.QueryString("ModelId").ToString() + " And Producer Name:" + lblName.Text + "", Session("ProjectId"), Session("SPROJLogInCount").ToString())

            Catch ex As Exception

            End Try
            'Ended Activity Log Changes
        Catch ex As Exception

        End Try
    End Sub

    
End Class
