Imports System.Data
Imports System.Data.OleDb
Imports System
Imports StandGetData
Imports StandUpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Partial Class Pages_StandAssist_PopUp_SponsorDetails
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("Back") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If
            hidSponsorDes.Value = Request.QueryString("Des").ToString()
            hidSponsorId.Value = Request.QueryString("Id").ToString()

            If Not IsPostBack Then
                GetSponsorDetails()
                'Started Activity Log Changes
                Try
                    Dim objUpIns As New StandUpInsData.UpdateInsert
                    objUpIns.InsertLog1(Session("UserId").ToString(), "25", "Opened Sponsor Selector PopUp", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                Catch ex As Exception

                End Try
                'Ended Activity Log Changes
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub GetSponsorDetails()
        Dim ds As New DataSet
        Dim objGetData As New StandGetData.Selectdata()
        Try
            ds = objGetData.GetSponsorUsers(txtCaseDe1.Text)


            If ds.Tables(0).Rows.Count > 0 Then
                grdSponsor.DataSource = ds
                grdSponsor.DataBind()
            Else
                '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "alert('No Users available');", True)
            End If

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetCaseDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnSponsor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSponsor.Click
        Try
            divSponsor.Visible = True
            'Started Activity Log Changes
            Try
                Dim objUpIns As New StandUpInsData.UpdateInsert
                objUpIns.InsertLog1(Session("UserId").ToString(), "25", "Clicked on Add new Sponsor Button", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
            Catch ex As Exception

            End Try
            'Ended Activity Log Changes
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnSubmits_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmits.Click
        Try
            Dim objUpIns As New StandUpInsData.UpdateInsert
            objUpIns.UpdateSponsor(txtName.Text, txtEmail.Text)
            GetSponsorDetails()
            'Started Activity Log Changes
            Try
                objUpIns.InsertLog1(Session("UserId").ToString(), "25", "Added New Sponsor", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
            Catch ex As Exception

            End Try
            'Ended Activity Log Changes
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            GetSponsorDetails()
            'Started Activity Log Changes
            Try
                Dim objUpIns As New StandUpInsData.UpdateInsert
                objUpIns.InsertLog1(Session("UserId").ToString(), "25", "Clicked on Search Button, Searched Text: " + txtCaseDe1.Text + "", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
            Catch ex As Exception

            End Try
            'Ended Activity Log Changes
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            divSponsor.Visible = False
            'Started Activity Log Changes
            Try
                Dim objUpIns As New StandUpInsData.UpdateInsert
                objUpIns.InsertLog1(Session("UserId").ToString(), "25", "Clicked on Cancel Button", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
            Catch ex As Exception

            End Try
            'Ended Activity Log Changes
        Catch ex As Exception

        End Try
    End Sub
End Class
