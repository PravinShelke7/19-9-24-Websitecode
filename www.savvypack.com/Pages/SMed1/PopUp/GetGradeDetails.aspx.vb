Imports System.Data
Imports System.Data.OleDb
Imports System
Imports SMed1GetData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_MedSustain1_PopUp_GetGradeDetails
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            hidGradedes.Value = Request.QueryString("Des").ToString()
            hidGradeId.Value = Request.QueryString("ID").ToString()
            hidSG.Value = Request.QueryString("SG").ToString()
            If Not IsPostBack Then
                GetGradesPopUp()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GetGradesPopUp()
        Dim ds As New DataSet
        Dim matId As Integer
        Dim objGetData As New SMed1GetData.Selectdata()
        Try
            matId = Request.QueryString("MatId")
            ds = objGetData.GetGrades(matId)
            grdGrade.DataSource = ds
            grdGrade.DataBind()
        Catch ex As Exception

        End Try
    End Sub
End Class
