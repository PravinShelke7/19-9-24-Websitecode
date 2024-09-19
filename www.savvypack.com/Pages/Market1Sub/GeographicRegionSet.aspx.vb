Imports System
Imports System.Data
Imports M1SubGetData
Partial Class Pages_Market1_GeographicRegionSet
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                BindGrdAdminRegionSet()
                hidsortreg.Value = "0"
                'hvUserGrd.Value = "0"
                'lnkShowAll.Text = "Show All"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub BindGrdAdminRegionSet()
        Dim objGetData As New Selectdata()
        Dim ds As New DataSet()
        Dim RegionSetId As String = String.Empty
        Dim objCryptoHelper As New CryptoHelper

        Try
            ds = objGetData.GetRegDetails(Session("M1SubGroupId"))
            Session("RegDetails") = ds
            With grdRegionSet
                .DataSource = ds
                .DataBind()
                .PageIndex = 0
            End With

            ViewState("UserRegionSet") = ds
            'If ds.Tables(0).Rows.Count <= 11 Then
            '    lnkShowAll.Enabled = False
            'Else
            '    lnkShowAll.Enabled = True
            'End If


        Catch ex As Exception

        End Try
    End Sub
    Protected Sub grdRegionSet_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdRegionSet.Sorting
        Dim Dts As New DataSet
        Dim dv As DataView
        Dim numberDiv As Integer
        Dim dsSorted As New DataSet()
        Try
            numberDiv = Convert.ToInt16(hidsortreg.Value.ToString())
            Dts = Session("RegDetails")
            dv = Dts.Tables(0).DefaultView

            If ((numberDiv Mod 2) = 0) Then
                dv.Sort = e.SortExpression + " " + "DESC"
            Else
                dv.Sort = e.SortExpression + " " + "ASC"
            End If
            numberDiv += 1
            hidsortreg.Value = numberDiv.ToString()
            grdRegionSet.DataSource = dv
            grdRegionSet.DataBind()

            dsSorted.Tables.Add(dv.ToTable())
            Session("RegDetails") = dsSorted

        Catch ex As Exception
            Response.Write("Error:grdRegionSet_Sorting:" + ex.Message.ToString())
        End Try
    End Sub
End Class
