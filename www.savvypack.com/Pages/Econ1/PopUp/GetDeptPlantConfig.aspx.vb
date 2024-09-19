Imports System.Data
Imports System.Data.OleDb
Imports System
Imports E1GetData
Imports E1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Econ1_PopUp_GetDeptPlantConfig
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            hidDepdes.Value = Request.QueryString("Des").ToString()
            hidDepid.Value = Request.QueryString("ID").ToString()
            If Not IsPostBack Then
                GetDeptDetails()
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            GetDeptDetails()
        Catch ex As Exception
            _lErrorLble.Text = "Error:btnSearch_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetDeptDetails()
        Dim ds As New DataSet
        Dim objGetData As New E1GetData.Selectdata()
        Try
            ds = objGetData.GetDeptPlantConfig(-1, txtDepDe1.Text.Trim.ToString(), txtDepDe2.Text.Trim.ToString())
            grdDepartment.DataSource = ds
            grdDepartment.DataBind()
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetDeptDetails:" + ex.Message.ToString()
        End Try
    End Sub
End Class
