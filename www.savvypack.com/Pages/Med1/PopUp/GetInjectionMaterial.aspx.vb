Imports System.Data
Imports System.Data.OleDb
Imports System
Imports Med1GetData
Imports Med1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_MedEcon1_PopUp_GetInjectionMaterial
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            hidMatdes.Value = Request.QueryString("Des").ToString()
            hidMatid.Value = Request.QueryString("ID").ToString()
            If Not IsPostBack Then
                hvMatGrd.Value = "0"
                GetMaterialDetails()
            End If

        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetMaterialDetails()
        Dim ds As New DataSet
        Dim objGetData As New Med1GetData.Selectdata()
        Try

            ds = objGetData.GetMaterialByText(txtKeyword.Text)
            Session("InjectData") = ds
            grdInject.DataSource = ds
            grdInject.DataBind()
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetInjectionMaterial:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            GetMaterialDetails()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub grdMaterial_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdInject.Sorting
        Try
            Dim Dts As New DataSet
            Dim dv As DataView
            Dim numberDiv As Integer
            Dim dsSorted As New DataSet()

            numberDiv = Convert.ToInt16(hvMatGrd.Value.ToString())
            Dts = Session("InjectData")
            dv = Dts.Tables(0).DefaultView

            If ((numberDiv Mod 2) = 0) Then
                dv.Sort = e.SortExpression + " " + "DESC"
            Else
                dv.Sort = e.SortExpression + " " + "ASC"
            End If

            numberDiv += 1
            hvMatGrd.Value = numberDiv.ToString()
            grdInject.DataSource = dv
            grdInject.DataBind()

            dsSorted.Tables.Add(dv.ToTable())
            Session("InjectData") = dsSorted

        Catch ex As Exception

        End Try
    End Sub
End Class
