Imports System
Imports System.Data
Imports MoldE1GetData

Partial Class Pages_MoldEcon1_PopUp_StatusDetails
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                BindGrdGroup()
                lblStatus.Text = "Status log for case " + Request.QueryString("CaseId").ToString()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub BindGrdGroup()
        Dim objGetData As New Selectdata()
        Dim ds As New DataSet()
        Dim RegionSetId As String = String.Empty
        Dim objCryptoHelper As New CryptoHelper
        Try
            ds = objGetData.GetStatusDetailsByID(Request.QueryString("CaseId").ToString(), Request.QueryString("Owner").ToString())
            With grdCaseStatus
                .DataSource = ds
                .DataBind()
                .PageIndex = 0
            End With
        Catch ex As Exception

        End Try
    End Sub
End Class
