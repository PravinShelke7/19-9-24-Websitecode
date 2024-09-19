Imports System.Data
Imports System.Data.OleDb
Imports System
Imports E2GetData
Imports E2UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter

Partial Class Pages_Econ2_PopUp_UpdateEquip
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            hidAssetId.Value = Request.QueryString("EqId").ToString()
            hidEqDes1.Value = Request.QueryString("Des").ToString()
            If Not IsPostBack Then
                GetPageDetails()
            End If

        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    Public Sub GetPageDetails()
        Dim ds As New DataSet
        Dim objGetData As New Selectdata()
        Dim dsEmat As New DataSet
        Dim dv As New DataView
        Dim dt As New DataTable
        Try
            ds = objGetData.GetEquipment(hidAssetId.Value, "", "")
            lblName.Text = ds.Tables(0).Rows(0).Item("EQUIPDES").ToString()
            dsEmat = objGetData.GetEditEquip(Session("E2CaseId"))
            dv = dsEmat.Tables(0).DefaultView
            dv.RowFilter = "EQUIPID=" + hidAssetId.Value
            dt = dv.ToTable()
            If dt.Rows.Count > 0 Then
                txtName.Text = dt.Rows(0).Item("EQUIPDES").ToString().Replace("&#", "'")
            End If


        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub


    Protected Sub btnCall_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCall.Click
        GetPageDetails()
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Dim objUpIns As New UpdateInsert()
        Try
            objUpIns.EditEqName(Session("E2CaseId"), hidAssetId.Value, txtName.Text.Replace("'", "''"))
            hidEqName.Value = txtName.Text.ToString()
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClosePage", "CloseWindow();", True)
        Catch ex As Exception
            _lErrorLble.Text = "Error:btnSubmit_Click:" + ex.Message.ToString()
        End Try

    End Sub
End Class
