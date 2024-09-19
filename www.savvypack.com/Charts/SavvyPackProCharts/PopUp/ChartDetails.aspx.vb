Imports System.Web.UI.HtmlControls
Imports System.Data
Imports System.Data.OleDb
Imports System.Web.HttpBrowserCapabilities
Partial Class Pages_SavvyPackPro_Charts_PopUp_ChartDetails
    Inherits System.Web.UI.Page
    Dim objGetdata As New SavvyProGetData
    Dim objUpIns As New SavvyProUpInsData
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            hidchartgrp.Value = Request.QueryString("GrpId").ToString()
            hidMatdes.Value = Request.QueryString("Des1").ToString()
            hidLnk.Value = Request.QueryString("Des").ToString()
            hidMatid.Value = Request.QueryString("ID").ToString()
            hidtype.Value = Request.QueryString("type").ToString()

            If Not IsPostBack Then
                hvUserGrd.Value = "0"
                If hidchartgrp.Value.ToString() = "" Or hidchartgrp.Value.ToString() = "0" Then
                    GetChartDetails()
              
                End If

              
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub


    Protected Sub GetChartDetails()
        Dim ds As New DataSet

        Try
            'ds = objGetData.GetUsersChartDetails(Session("USERID"))
            ds = objGetdata.GetUsersChartDet(txtKey.Text, Session("UserId"), Session("RFPID"))
            If ds.Tables(0).Rows.Count > 0 Then
                grdChartDetails.Visible = True
                Session("UsersDataGroup") = ds
                grdChartDetails.DataSource = ds
                grdChartDetails.DataBind()
            Else
                lblRecord.Text = "No Record Found"
                grdChartDetails.Visible = False
            End If
          
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetChartDetails:" + ex.Message.ToString()
        End Try
    End Sub

   

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            GetChartDetails()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub gvDocuments_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles grdChartDetails.RowDataBound
        Dim ds As New DataSet()
        ds = CType(Session("effdate"), DataSet)
        If (e.Row.RowType = DataControlRowType.DataRow) Then

            If (e.Row.RowIndex = 0) Then
                'e.Row.Style.Add("height", "10px")
                'e.Row.Style.Add("vertical-align", "bottom")
                e.Row.Cells(1).Style.Add("padding-top", "9px")
                '               e.Row.Cells(2).Style.Add("padding-top", "39px")

            End If
        End If

        grdChartDetails.Columns(1).ItemStyle.Width = 200
        'grdChartDetails.Columns(2).ItemStyle.Width = 100


        grdChartDetails.Columns(1).HeaderStyle.Width = 310
        '        grdChartDetails.Columns(2).HeaderStyle.Width = 150

    End Sub
    Protected Sub grdGroup_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdChartDetails.Sorting
        Try
            Dim Dts As New DataSet
            Dim dv As DataView
            Dim ds As New DataSet
            Dim numberDiv As Integer
            numberDiv = Convert.ToInt16(hvUserGrd.Value.ToString())
            Dts = Session("UsersDataGroup")
            dv = Dts.Tables(0).DefaultView

            If ((numberDiv Mod 2) = 0) Then
                dv.Sort = e.SortExpression + " " + "DESC"
            Else
                dv.Sort = e.SortExpression + " " + "ASC"
            End If
            numberDiv += 1
            ds.Tables.Add(dv.ToTable())
            Session("UsersDataGroup") = ds
            hvUserGrd.Value = numberDiv.ToString()
            grdChartDetails.DataSource = dv
            grdChartDetails.DataBind()


        Catch ex As Exception

        End Try
    End Sub
End Class
