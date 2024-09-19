Imports System
Imports System.Data
Imports E2GetData

Partial Class Pages_Econ2_PopUp_LicenseUser
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            hidCaseid.Value = Request.QueryString("ID").ToString()
            hidCaseDes.Value = Request.QueryString("Des").ToString()

            If Not IsPostBack Then
                GetCaseDetails()

            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetCaseDetails()
        Dim ds As New DataSet
        Dim objGetData As New E2GetData.Selectdata()
        Try
            ds = objGetData.GetUserCompanyUsersBem(Session("E2UserName").ToString())
            If ds.Tables(0).Rows.Count > 0 Then
                grdCaseSearch.DataSource = ds
                grdCaseSearch.DataBind()
            End If



        Catch ex As Exception
            _lErrorLble.Text = "Error:GetCaseDetails:" + ex.Message.ToString()
        End Try
    End Sub
End Class
