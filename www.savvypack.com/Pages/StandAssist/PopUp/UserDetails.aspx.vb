Imports System.Data
Imports System.Data.OleDb
Imports System
Imports StandGetData
Imports StandUpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Partial Class Pages_StandAssist_PopUp_UserDetails
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("Back") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If
            hidUserDes.Value = Request.QueryString("Des").ToString()
            hidUserId.Value = Request.QueryString("Id").ToString()
            hidUsernameD.Value = Request.QueryString("IdD").ToString()
            If Not IsPostBack Then
                GetUserDetails()
				
				 'Started Activity Log Changes
                Try
                    Dim objUpIns As New StandUpInsData.UpdateInsert
                    objUpIns.InsertLog1(Session("UserId").ToString(), "10", "Opened User Selector PopUp", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                Catch ex As Exception

                End Try
                'Ended Activity Log Changes
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub GetUserDetails()
        Dim ds As New DataSet
        Dim objGetData As New StandGetData.Selectdata()
        Try
            ds = objGetData.GetUserCompanyUsers(Session("SBAUserName").ToString())
           

            If ds.Tables(0).Rows.Count > 0 Then
                grdUserSearch.DataSource = ds
                grdUserSearch.DataBind()
            Else
                '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "alert('No Users available');", True)
            End If

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetCaseDetails:" + ex.Message.ToString()
        End Try
    End Sub
	
	Protected Sub btnPostback_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPostback.Click
        Try
            Dim objUpIns As New StandUpInsData.UpdateInsert()
            'Started Activity Log Changes
            Try
                objUpIns.InsertLog1(Session("UserId").ToString(), "10", "Selected User #" + hidUser.Value, "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
            Catch ex As Exception

            End Try
            'Ended Activity Log Changes
        Catch ex As Exception

        End Try
    End Sub
End Class
