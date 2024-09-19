Imports System
Imports System.Data
Imports M1GetData
Imports M1UpInsData
Partial Class Pages_Market1_GeographicRegionSet
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                BindGrdAdminRegionSet()
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
            ds = objGetData.GetRegionsSetDetails()
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
End Class
