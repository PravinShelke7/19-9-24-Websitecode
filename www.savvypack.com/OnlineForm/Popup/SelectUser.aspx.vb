Imports System
Imports System.Data
Imports SavvyGetData
Imports SavvyUpInsData
Partial Class OnlineForm_Popup_SelectUser
    Inherits System.Web.UI.Page
    Dim objUpIns As New UpdateInsert()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            hdnUserId.Value = Request.QueryString("Id").ToString()
            hdnUserDes.Value = Request.QueryString("Des").ToString()
            hidUsrDes.Value = Request.QueryString("Des1").ToString()
            If Not IsPostBack Then
                'Started Activity Log Changes
                Try
                    objUpIns.InsertLog1(Session("UserId").ToString(), "SelectUser.aspx", "Opened Select User Popup to select user for sending message", "", Session("SPROJLogInCount").ToString())
                Catch ex As Exception

                End Try
                'Ended Activity Log Changes
                getUserDeatils("")
            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub getUserDeatils(ByVal Username As String)
        Dim ds As New DataSet
        Dim objGetData As New Selectdata
        Try
            ds = objGetData.GetUserDetail(Session("UserId"), Username)
            Session("ds") = ds
            grdUserDetails.DataSource = ds
            grdUserDetails.DataBind()
        Catch ex As Exception
            _lErrorLble.Text = "Error:getUserDeatils:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            getUserDeatils(txtUser.Text.Trim.ToString().Replace("'", "''"))
            'Started Activity Log Changes
            Try
                objUpIns.InsertLog1(Session("UserId").ToString(), "SelectUser.aspx", "Clicked on Search Button, Searched Text:" + txtUser.Text.ToString().Replace("'", "''"), "", Session("SPROJLogInCount").ToString())

            Catch ex As Exception

            End Try
            'Ended Activity Log Changes
        Catch ex As Exception
            _lErrorLble.Text = "Error:btnSearch_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub grdUsers_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdUserDetails.PageIndexChanging
        Try
            grdUserDetails.PageIndex = e.NewPageIndex
            BindUserGridUsingSession()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub BindUserGridUsingSession()
        Try
            Dim Dts As New DataSet
            Dts = Session("ds")
            grdUserDetails.DataSource = Dts
            grdUserDetails.DataBind()
            'lblmsg.Visible = False
            'lblUserAv.Text = ""
            'trUsername.Visible = False
            'trLicense.Visible = False
        Catch ex As Exception

        End Try
    End Sub
End Class
