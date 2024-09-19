Imports System.Data
Imports System.Data.OleDb
Imports System
Imports SavvyProGetData
Imports SavvyProUpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Imports System.Net.Mail
Imports System.Net
Partial Class Pages_SavvyPackPro_PopUp_SelectRFP
    Inherits System.Web.UI.Page
    Dim objGetdata As New SavvyProGetData
    Dim objUpIns As New SavvyProUpInsData

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                hidSortId.Value = "0"
                hidRfpID.Value = Request.QueryString("RfpID").ToString()
                hidRfpDes.Value = Request.QueryString("RfpDes").ToString()
                hidRfpIT.Value = Request.QueryString("RfpInnertxt").ToString()
                lnkShowAll.Text = "Show All"
                GetRefDetails()
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Page_Load" + ex.Message.ToString()
        End Try
    End Sub

#Region "User Grid Event"

    Protected Sub grdRfpDetails_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdRfpDetails.Sorting
        Dim dsSorted As New DataSet()
        Try
            Dim Dts As New DataSet
            Dim dv As DataView
            Dim numberDiv As Integer
            numberDiv = Convert.ToInt16(hidSortId.Value.ToString())
            Dts = Session("SelRFPBuyer")
            dv = Dts.Tables(0).DefaultView

            If ((numberDiv Mod 2) = 0) Then
                dv.Sort = e.SortExpression + " " + "DESC"
            Else
                dv.Sort = e.SortExpression + " " + "ASC"
            End If
            numberDiv += 1
            hidSortId.Value = numberDiv.ToString()
            grdRfpDetails.DataSource = dv
            grdRfpDetails.DataBind()

            dsSorted.Tables.Add(dv.ToTable())
            Session("SelRFPBuyer") = dsSorted

            If dv.Table.Rows.Count <= 10 Then
                lnkShowAll.Enabled = False
            Else
                lnkShowAll.Enabled = True
            End If

        Catch ex As Exception
            _lErrorLble.Text = "grdRfpDetails_Sorting" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub grdRfpDetails_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdRfpDetails.PageIndexChanging
        Try
            grdRfpDetails.PageIndex = e.NewPageIndex
            BindRFPDetailSession()
        Catch ex As Exception
            _lErrorLble.Text = "grdRfpDetails_PageIndexChanging" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub lnkShowAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkShowAll.Click
        Try
            If grdRfpDetails.AllowPaging = True Then
                grdRfpDetails.AllowPaging = False
                lnkShowAll.Text = "Show Paging"
            Else
                grdRfpDetails.AllowPaging = True
                lnkShowAll.Text = "Show All"
            End If
            BindRFPDetailSession()
        Catch ex As Exception
            _lErrorLble.Text = "Error:lnkShowAll_Click" + ex.Message.ToString()
        End Try
    End Sub

#End Region

#Region "User Define Delegates"

    Protected Sub GetRefDetails()
        Dim ds As New DataSet
        Try
            ds = objGetdata.GetRefByLicenseID(txtKeyword.Text, Session("LicenseNo"))
            Session("SelRFPBuyer") = ds
            If ds.Tables(0).Rows.Count > 0 Then
                lblNRF.Visible = False
                grdRfpDetails.Visible = True
                grdRfpDetails.DataSource = ds
                grdRfpDetails.DataBind()

                If ds.Tables(0).Rows.Count <= 10 Then
                    lnkShowAll.Enabled = False
                Else
                    lnkShowAll.Enabled = True
                End If
            Else
                lblNRF.Visible = True
                grdRfpDetails.Visible = False
            End If
        Catch ex As Exception
            _lErrorLble.Text = "GetRefDetails" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub BindRFPDetailSession()
        Try
            Dim Dts As New DataSet
            Dts = Session("SelRFPBuyer")
            grdRfpDetails.DataSource = Dts
            grdRfpDetails.DataBind()

            If Dts.Tables(0).Rows.Count <= 10 Then
                lnkShowAll.Enabled = False
            Else
                lnkShowAll.Enabled = True
            End If
        Catch ex As Exception
            _lErrorLble.Text = "BindRFPDetailSession" + ex.Message.ToString()
        End Try
    End Sub

#End Region

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            GetRefDetails()
        Catch ex As Exception
            _lErrorLble.Text = "btnSearch_Click" + ex.Message.ToString()
        End Try

    End Sub

End Class
