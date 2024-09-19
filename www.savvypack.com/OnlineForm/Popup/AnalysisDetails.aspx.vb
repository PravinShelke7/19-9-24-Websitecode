Imports System
Imports System.Data
Imports SavvyGetData
Partial Class SavvyPack_Popup_TypeDetails
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            hdnUserId.Value = Request.QueryString("Id").ToString()
            hdnUserDes.Value = Request.QueryString("Des").ToString()
            hdnUserDes1.Value = Request.QueryString("Des1").ToString()
            If Not IsPostBack Then
                getUserDeatils("")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub getUserDeatils(ByVal Username As String)
        Dim ds As New DataSet
        Dim objGetData As New Selectdata
        Try
            'ds = objGetData.GetAnalysisDetails()
             ds = objGetData.GetUserAnalysis(Session("USERID").ToString())
            Session("ds") = ds
            grdUserDetails.DataSource = ds
            grdUserDetails.DataBind()
        Catch ex As Exception
            _lErrorLble.Text = "Error:getUserDeatils:" + ex.Message.ToString()
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
            
        Catch ex As Exception

        End Try
    End Sub

End Class
