Imports System
Imports System.Data
Partial Class Pages_StandAssist_PopUp_ComparisonDetails
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim objUpIns As New StandUpInsData.UpdateInsert
            If Session("Back") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If
            hidReportDes.Value = Request.QueryString("Des").ToString()
            hidReportDes1.Value = Request.QueryString("Des1").ToString()
            hidReportId.Value = Request.QueryString("ID").ToString()
            If Not IsPostBack Then
                GetReportDetails()
                'Started Activity Log Changes
                Try
                    objUpIns.InsertLog1(Session("UserId").ToString(), "20", "Opened Structures Comparison Selector Page (Under Structure #" + Session("SBACaseId").ToString() + ")", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                Catch ex As Exception

                End Try
                'Ended Activity Log Changes
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub


    Protected Sub GetReportDetails()
        Dim ds As New DataSet
        Dim objGetData As New StandGetData.Selectdata()
        Try
            ds = objGetData.GetSavedCaseAsperUser(Session("USERID").ToString(), "-1")
            grdReportSearch.DataSource = ds
            grdReportSearch.DataBind()

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetReportDetails:" + ex.Message.ToString()
        End Try
    End Sub

End Class
