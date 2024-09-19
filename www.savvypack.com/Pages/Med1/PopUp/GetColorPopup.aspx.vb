Imports System.Data
Imports System.Data.OleDb
Imports System
Imports Med1GetData
Imports Med1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_MedEcon1_PopUp_GetColorPopup
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            hidColdes.Value = Request.QueryString("Des").ToString()
            hidColid.Value = Request.QueryString("ID").ToString()
            If Not IsPostBack Then
                hvColGrd.Value = "0"
                GetColorDetails()
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetColorDetails()
        Dim ds As New DataSet
        Dim objGetData As New Med1GetData.Selectdata()
        Try

            ds = objGetData.GetColorsByText(txtKeyword.Text)
            Session("ColorData") = ds
            grdColor.DataSource = ds
            grdColor.DataBind()
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetColorDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            GetColorDetails()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub grdColor_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdColor.Sorting
        Try
            Dim Dts As New DataSet
            Dim dv As DataView
            Dim numberDiv As Integer
            Dim dsSorted As New DataSet()

            numberDiv = Convert.ToInt16(hvColGrd.Value.ToString())
            Dts = Session("ColorData")
            dv = Dts.Tables(0).DefaultView

            If ((numberDiv Mod 2) = 0) Then
                dv.Sort = e.SortExpression + " " + "DESC"
            Else
                dv.Sort = e.SortExpression + " " + "ASC"
            End If

            numberDiv += 1
            hvColGrd.Value = numberDiv.ToString()
            grdColor.DataSource = dv
            grdColor.DataBind()

            dsSorted.Tables.Add(dv.ToTable())
            Session("ColorData") = dsSorted

        Catch ex As Exception

        End Try
    End Sub
End Class
