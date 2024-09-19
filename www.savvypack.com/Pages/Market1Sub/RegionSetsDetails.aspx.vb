Imports System
Imports System.Data
Imports M1GetData
Imports M1UpInsData
Partial Class Pages_Market1_RegionSetsDetails
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                BindGrdRegionSet()
                hvUserGrd.Value = "0"
                lnkShowAll.Text = "Show All"
            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub BindGrdRegionSet()
        Dim objGetData As New Selectdata()
        Dim ds As New DataSet()
        Dim RegionSetId As String = String.Empty
        Dim objCryptoHelper As New CryptoHelper
        Try
            ds = objGetData.GetRegionSet_Regions(Request.QueryString("RegionSetId").ToString())
            With grdRegionSet
                .DataSource = ds
                .DataBind()
                .PageIndex = 0
            End With
            ViewState("AdminRegionSet") = ds
            If ds.Tables(0).Rows.Count <= 5 Then
                lnkShowAll.Enabled = False
            Else
                lnkShowAll.Enabled = True
            End If



        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkShowAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkShowAll.Click
        Try
            If grdRegionSet.AllowPaging = True Then
                grdRegionSet.AllowPaging = False
                lnkShowAll.Text = "Show Paging"
            Else
                grdRegionSet.AllowPaging = True
                lnkShowAll.Text = "Show All"
            End If

            BindSessionAdminRegionSet()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub BindSessionAdminRegionSet()
        Dim ds As New DataSet
        Try

            ds = ViewState("AdminRegionSet")
            With grdRegionSet
                .DataSource = ds
                .DataBind()
            End With

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub grdCurr_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdRegionSet.PageIndexChanging
        Try
            grdRegionSet.PageIndex = e.NewPageIndex
            BindSessionAdminRegionSet()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub grdRegionSet_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdRegionSet.Sorting
        Try
            Dim Dts As New DataSet
            Dim dv As DataView
            Dim numberDiv As Integer
            numberDiv = Convert.ToInt16(hvUserGrd.Value.ToString())
            Dts = ViewState("AdminRegionSet")
            dv = Dts.Tables(0).DefaultView

            If ((numberDiv Mod 2) = 0) Then
                dv.Sort = e.SortExpression + " " + "DESC"
            Else
                dv.Sort = e.SortExpression + " " + "ASC"
            End If

            numberDiv += 1
            hvUserGrd.Value = numberDiv.ToString()
            grdRegionSet.DataSource = dv
            grdRegionSet.DataBind()
        Catch ex As Exception

        End Try
    End Sub
End Class
