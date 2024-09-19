Imports System
Imports System.Data
Imports SavvyGetData
Imports SavvyUpInsData
Partial Class SavvyPack_Popup_TypeDetails
    Inherits System.Web.UI.Page
    Dim objUpIns As New SavvyUpInsData.UpdateInsert
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            hdnUserId.Value = Request.QueryString("ProjectId").ToString()
            hdnUserDes.Value = Request.QueryString("Des").ToString()
            hdnAnalysisID.Value = Request.QueryString("AID").ToString()
            If Not IsPostBack Then
                'Started Activity Log Changes
                Try
                    objUpIns.InsertLog1(Session("UserId").ToString(), "TypeDetails.aspx", "Opened TypeDetails Popup for ProjectId:" + hdnUserId.Value + "", hdnUserId.Value, Session("SPROJLogInCount").ToString())
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
            'ds = objGetData.GetAnalysisDetails()
            ds = objGetData.GetUserAnalysis(Session("USERID").ToString())
            Session("ds") = ds
            grdUserDetails.DataSource = ds
            grdUserDetails.DataBind()
            BindLink()
            If Session("SavvyAnalyst") <> "Y" Then
                If Session("UserId") <> Request.QueryString("UserId").ToString() Then
                    grdUserDetails.Columns(1).Visible = False
                    grdUserDetails.Columns(2).Visible = True
                End If
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:getUserDeatils:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub BindUserGridUsingSession()
        Try
            Dim Dts As New DataSet
            Dts = Session("ds")
            grdUserDetails.DataSource = Dts
            grdUserDetails.DataBind()
            BindLink()
        Catch ex As Exception
            _lErrorLble.Text = "Error:BindUserGridUsingSession:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub BindLink()
        Dim lblTypeID As New Label
        Dim lnkType As New HtmlAnchor
        Dim lblType As New Label
        Try
            If hdnAnalysisID.Value <> 6 Then
                For Each Gr As GridViewRow In grdUserDetails.Rows
                    lblTypeID = grdUserDetails.Rows(Gr.RowIndex).FindControl("lblTypeID")
                    'lnkType = grdUserDetails.Rows(Gr.RowIndex).FindControl("lnkType")
                    lblType = grdUserDetails.Rows(Gr.RowIndex).FindControl("lblType")

                    If lblTypeID.Text = "6" Then
                        Gr.Enabled = False
                        Gr.Cells(1).Text = lblType.Text
                        Gr.Cells(1).Font.Name = "Optima"
                        Gr.Cells(1).Font.Size = "9"
                    Else
                        Gr.Enabled = True
                    End If
                Next
            Else
                For Each Gr As GridViewRow In grdUserDetails.Rows
                    lblTypeID = grdUserDetails.Rows(Gr.RowIndex).FindControl("lblTypeID")
                    'lnkType = grdUserDetails.Rows(Gr.RowIndex).FindControl("lnkType")
                    lblType = grdUserDetails.Rows(Gr.RowIndex).FindControl("lblType")

                    If lblTypeID.Text = "6" Then
                        Gr.Enabled = True
                    Else
                        Gr.Enabled = False
                        Gr.Cells(1).Text = lblType.Text
                        Gr.Cells(1).Font.Name = "Optima"
                        Gr.Cells(1).Font.Size = "9"
                    End If
                Next
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:BindLink:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub grdUsers_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdUserDetails.PageIndexChanging
        Try
            grdUserDetails.PageIndex = e.NewPageIndex
            BindUserGridUsingSession()
        Catch ex As Exception

        End Try
    End Sub

    'Protected Sub grdUserDetails_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles grdUserDetails.RowDataBound
    '    If e.Row.RowType = DataControlRowType.DataRow Then

    '        'e.Row.Cells(1).Visible = False
    '        'e.Row.Cells(2).Visible = True

    '    End If
    'End Sub

    <System.Web.Services.WebMethod()> _
    Public Shared Function UpdateType(ByVal UserId As String, ByVal ProjectId As String) As Boolean
        Try
            Dim SavvyConnection As String = System.Configuration.ConfigurationManager.AppSettings("SavvyPackConnectionString")
            Dim StrSql As String = ""
            Dim odbutil As New DBUtil()
            Dim objUpIns As New SavvyUpInsData.UpdateInsert

            StrSql = "UPDATE PROJECTDETAILS SET ANALYSISID=" + UserId + " WHERE PROJECTID=" + ProjectId + " "
            odbutil.UpIns(StrSql, SavvyConnection)

            'Started Activity Log Changes
            Try
                objUpIns.InsertLog1(HttpContext.Current.Session("UserId").ToString(), "TypeDetails.aspx", _
                              "Updated Type Details for ProjectId:" + ProjectId + ". New AnalysisID:" + UserId + "", ProjectId, _
                              HttpContext.Current.Session("SPROJLogInCount").ToString())

            Catch ex As Exception

            End Try
            'Ended Activity Log Changes


            Return True
        Catch ex As Exception

            Return False
        End Try
    End Function

End Class
