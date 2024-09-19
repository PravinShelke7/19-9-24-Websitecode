Imports System.Data
Imports System.Data.OleDb
Imports System
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Charts_SavvyPackProCharts_PopUp_Groupbatchselection
    Inherits System.Web.UI.Page

    Dim objGetdata As New SavvyProGetData
    Dim objUpIns As New SavvyProUpInsData
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            hidGroupId.Value = Request.QueryString("Id").ToString()
            hidGroupDes.Value = Request.QueryString("Des1").ToString()
            hidlnkdes.Value = Request.QueryString("Des").ToString()
            hidRfpPriceOpID.Value = Request.QueryString("RfpPriceID").ToString()
            hidType.Value = Request.QueryString("Type").ToString()
            If Not IsPostBack Then
                GetGroupDetails()
                hidSortIdMGroup.Value = "0"

            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub GetGroupDetails()
        Dim ds As New DataSet
        Try
            ds = objGetdata.GetGrpDETByBatchLine(hidRfpPriceOpID.Value, txtKeyword.Text)
            Session("GroupDet") = ds
            lblRecondCnt.Text = ds.Tables(0).Rows.Count
            If ds.Tables(0).Rows.Count > 0 Then
                lblGroupNoFound.Visible = False
                grdGroup.Visible = True
                grdGroup.DataSource = ds
                grdGroup.DataBind()
            Else
                lblGroupNoFound.Visible = True
                grdGroup.Visible = False
            End If

        Catch ex As Exception
            lblError.Text = "Error:GetCaseDetails:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub btnSearch_Click(sender As Object, e As System.EventArgs) Handles btnSearch.Click
        GetGroupDetails()
    End Sub
   
#Region "User Grid  Group"
    Protected Sub grdGroup_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdGroup.Sorting
        Dim Dts As New DataSet
        Dim dv As DataView
        Dim numberDiv As Integer
        Dim dsSorted As New DataSet()
        Try
            numberDiv = Convert.ToInt16(hidSortIdMGroup.Value.ToString())
            Dts = Session("GroupDet")
            dv = Dts.Tables(0).DefaultView

            If ((numberDiv Mod 2) = 0) Then
                dv.Sort = e.SortExpression + " " + "DESC"
            Else
                dv.Sort = e.SortExpression + " " + "ASC"
            End If
            numberDiv += 1
            hidSortIdMGroup.Value = numberDiv.ToString()
            grdGroup.DataSource = dv
            grdGroup.DataBind()

            dsSorted.Tables.Add(dv.ToTable())
            Session("GroupDet") = dsSorted

        Catch ex As Exception
            Response.Write("Error:grdMGroup_Sorting:" + ex.Message.ToString())
        End Try
    End Sub

    Protected Sub grdGroup_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdGroup.PageIndexChanging
        Try
            grdGroup.PageIndex = e.NewPageIndex
            BindMGUsingSession()
        Catch ex As Exception
            Response.Write("Error:grdMGroup_PageIndexChanging:" + ex.Message.ToString())
        End Try
    End Sub

    Protected Sub ddlPageCountC_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageCountC.SelectedIndexChanged
        Try
            grdGroup.PageSize = ddlPageCountC.SelectedItem.ToString()
            BindMGUsingSession()
        Catch ex As Exception
            Response.Write("Error:ddlPageCountC_SelectedIndexChanged:" + ex.Message.ToString())
        End Try
    End Sub
    Protected Sub BindMGUsingSession()
        Try
            Dim Dts As New DataSet
            Dts = Session("GroupDet")
            grdGroup.DataSource = Dts
            grdGroup.DataBind()
            lblGroupNoFound.Visible = False
        Catch ex As Exception
            lblError.Text = "Error: BindMGUsingSession:" + ex.Message()
        End Try
    End Sub

#End Region
End Class
