Imports System.Data
Imports System.Data.OleDb
Imports System
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter

Partial Class Pages_SavvyPackPro_Charts_PopUp_PriceSelection
    Inherits System.Web.UI.Page
    Dim objGetdata As New SavvyProGetData
    Dim objUpIns As New SavvyProUpInsData
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            hidPriceDes.Value = Request.QueryString("Des1").ToString()
            hidPriceId.Value = Request.QueryString("Id").ToString()
            hidlnkdes.Value = Request.QueryString("Des").ToString()
            If Not IsPostBack Then
                GetCountryDetails()
                hidSortIdGroup.Value = "0"
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub GetCountryDetails()
        Dim ds As New DataSet

        Try

            ds = objGetdata.GetPriceDes(Session("RFPID"))
            Session("pricesel") = ds
            If ds.Tables(0).Rows.Count > 0 Then
                grdPrice.DataSource = ds
                grdPrice.DataBind()
                lblGroupNoFound.Visible = False
                grdPrice.Visible = True
            Else
                lblGroupNoFound.Visible = True
                grdPrice.Visible = False
            End If

        Catch ex As Exception
            lblError.Text = "Error:GetCaseDetails:" + ex.Message.ToString()
        End Try
    End Sub

#Region "User Grid  Group"

    Protected Sub grdPrice_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdPrice.Sorting
        Dim Dts As New DataSet
        Dim dv As DataView
        Dim numberDiv As Integer
        Dim dsSorted As New DataSet()
        Try
            numberDiv = Convert.ToInt16(hidSortIdGroup.Value.ToString())
            Dts = Session("pricesel")
            dv = Dts.Tables(0).DefaultView

            If ((numberDiv Mod 2) = 0) Then
                dv.Sort = e.SortExpression + " " + "DESC"
            Else
                dv.Sort = e.SortExpression + " " + "ASC"
            End If
            numberDiv += 1
            hidSortIdGroup.Value = numberDiv.ToString()
            grdPrice.DataSource = dv
            grdPrice.DataBind()

            dsSorted.Tables.Add(dv.ToTable())
            Session("pricesel") = dsSorted

        Catch ex As Exception
            Response.Write("Error:grdGroup_Sorting:" + ex.Message.ToString())
        End Try
    End Sub

    Protected Sub grdPrice_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdPrice.PageIndexChanging
        Try
            grdPrice.PageIndex = e.NewPageIndex
            BindMGUsingSession()
        Catch ex As Exception
            Response.Write("Error:grdMGroup_PageIndexChanging:" + ex.Message.ToString())
        End Try
    End Sub

  
    Protected Sub BindMGUsingSession()
        Try
            Dim Dts As New DataSet
            Dts = Session("pricesel")
            grdPrice.DataSource = Dts
            grdPrice.DataBind()
            lblGroupNoFound.Visible = False
        Catch ex As Exception
            lblError.Text = "Error: BindMGUsingSession:" + ex.Message()
        End Try
    End Sub
#End Region
End Class
