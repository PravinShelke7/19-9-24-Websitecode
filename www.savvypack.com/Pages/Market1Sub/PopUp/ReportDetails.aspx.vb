Imports System.Data
Imports System.Data.OleDb
Imports System
Imports M1SubGetData
Imports M1SubUpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Market1_PopUp_ReportDetails
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            hidReportDes.Value = Request.QueryString("Des").ToString()
            hidReportId.Value = Request.QueryString("ID").ToString()
            hidGrpId.Value = Request.QueryString("grpID").ToString()
            If Not IsPostBack Then
                GetReportDetails()
                hvUserGrd.Value = "0"
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub


    Protected Sub GetReportDetails()
        Dim ds As New DataSet
        Dim objGetData As New Selectdata()
        Try
            If Request.QueryString("Type").ToString() = "Base" Then
                ds = objGetData.GetBaseReports(Session("M1SubGroupId"), txtkey.Text.Trim.ToString().Replace("'", "''"))
            Else
                If Request.QueryString("grpID").ToString() = "0" Then
                    ds = objGetData.GetUserCustomReportsForSub(Session("UserId"), Session("M1ServiceID"), txtkey.Text.Trim.ToString().Replace("'", "''"))
                Else
                    ds = objGetData.GetUserCustomReportsbyGroup(Session("UserId"), Session("M1ServiceID"), Request.QueryString("grpID").ToString(), txtkey.Text.Trim.ToString().Replace("'", "''"))
                End If
            End If
            If ds.Tables(0).Rows.Count > 0 Then
                btnSearch.Enabled = True
                lblPreport.Visible = False
            Else
                lblPreport.Visible = True
                btnSearch.Enabled = False
            End If

            Session("UsersReportGroup") = ds
            grdReportSearch.DataSource = ds
            grdReportSearch.DataBind()

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetReportDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim ds As New DataSet
        Dim objGetData As New Selectdata()
        Try
            GetGroupDetails()
        Catch ex As Exception
            _lErrorLble.Text = "Error:btnSearch_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetGroupDetails()
        Dim ds As New DataSet
        Dim objGetData As New Selectdata()
        Try
            If Request.QueryString("Type").ToString() = "Base" Then
                ds = objGetData.GetBaseReports(Session("M1SubGroupId"), txtkey.Text.Trim.ToString().Replace("'", "''"))
            Else
                If Request.QueryString("grpID").ToString() = "0" Then
                    ds = objGetData.GetUserCustomReportsForSub(Session("UserId"), Session("M1ServiceID"), txtkey.Text.Trim.ToString().Replace("'", "''"))
                Else
                    ds = objGetData.GetUserCustomReportsbyGroup(Session("UserId"), Session("M1ServiceID"), Request.QueryString("grpID").ToString(), txtkey.Text.Trim.ToString().Replace("'", "''"))
                End If
            End If

            Session("UsersReportGroup") = ds
            grdReportSearch.DataSource = ds
            grdReportSearch.DataBind()

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetGroupDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub grdReportSearch_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdReportSearch.Sorting
        Try
            Dim Dts As New DataSet
            Dim dv As DataView
            Dim ds As New DataSet
            Dim numberDiv As Integer
            numberDiv = Convert.ToInt16(hvUserGrd.Value.ToString())
            Dts = Session("UsersReportGroup")
            dv = Dts.Tables(0).DefaultView

            If ((numberDiv Mod 2) = 0) Then
                dv.Sort = e.SortExpression + " " + "DESC"
            Else
                dv.Sort = e.SortExpression + " " + "ASC"
            End If
            numberDiv += 1
            ds.Tables.Add(dv.ToTable())
            Session("UsersReportGroup") = ds
            hvUserGrd.Value = numberDiv.ToString()
            grdReportSearch.DataSource = dv
            grdReportSearch.DataBind()

        Catch ex As Exception

        End Try
    End Sub
End Class
