Imports System
Imports System.Data
Imports MoldS2GetData
Partial Class Pages_MoldSustain2_PopUp_StatusDetails
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("Back") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If
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
