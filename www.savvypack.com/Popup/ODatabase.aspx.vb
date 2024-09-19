Imports System.Data
Imports System.Data.OleDb
Imports System
Imports LoginGetData


Partial Class Popup_ODatabase
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim obj As New CryptoHelper
        Try
            If Session("Back") = Nothing And Session("SBack") = Nothing Then
                Response.Redirect("~/ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If

            If Not IsPostBack Then
                hidId.Value = Request.QueryString("Id").ToString()
                If Session("LicenseNo") <> "0" Then
                    GetKnowledgeBaseDetails()
                    GetSubscriptionDetails()
                Else
                    'Response.Redirect("~/ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE114").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
					Page.ClientScript.RegisterStartupScript(Me.GetType(), "SessioError", "ErrorWindow('/ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE114").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "')", True)
                End If
            End If

        Catch ex As Exception
        End Try
    End Sub

    Public Sub GetKnowledgeBaseDetails()
        Dim objGetData As New LoginGetData.Selectdata()
        Dim ds As New DataSet()
        Try

            ds = objGetData.GetKnowledgeBaseDetails(Session("UserId").ToString(), "")
            If ds.Tables(0).Rows.Count > 0 Then
                lnkCont.Enabled = True
                lnkCont.CssClass = "Service"
                lnkCont.Attributes.Add("CommandArgument", ds.Tables(0).Rows(0).Item("TID").ToString())
                lnkCont.Attributes.Add("OnClick", "return ClosePop('lnkCont');")
            End If
        Catch ex As Exception
            'lblError.Text = "GetData:GetKnowledgeBaseDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Public Sub GetSubscriptionDetails()
        Dim objGetData As New LoginGetData.Selectdata()
        Dim ds As New DataSet()
        Dim tr As TableRow
        Dim td As TableCell
        Dim lnk As LinkButton
        Try
            ds = objGetData.GetSubscriptionDetails(Session("UserId").ToString())
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    tr = New TableRow
                    td = New TableCell
                    lnk = New LinkButton
                    lnk.ID = "lnkMrkt" + (i + 1).ToString()
                    lnk.CssClass = "Service"
                    lnk.Text = ds.Tables(0).Rows(i).Item("SERVICENAME").ToString()
                    'lnk.Attributes.Add("OnClientClick", "return ClosePop('" + lnk.ID + "');")
                   ' lnk.CommandArgument = ds.Tables(0).Rows(i).Item("SERVICEID").ToString()
                    lnk.Attributes.Add("CommandArgument", ds.Tables(0).Rows(i).Item("SERVICEID").ToString())
                    lnk.OnClientClick = "return ClosePop('" + lnk.ID + "');"
                    td.Controls.Add(lnk)
                    tr.Controls.Add(td)
                    tr.CssClass = "AlterNateColor1"
                    tblMrkt.Controls.Add(tr)
                Next
            End If
        Catch ex As Exception
            ' lblError.Text = "GetSubscriptionDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub
    
End Class


