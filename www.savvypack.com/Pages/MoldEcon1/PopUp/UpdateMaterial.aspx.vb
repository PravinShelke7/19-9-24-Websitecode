Imports System.Data
Imports System.Data.OleDb
Imports System
Imports MoldE1GetData
Imports MoldE1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter

Partial Class Pages_MoldEcon1_PopUp_UpdateMaterial
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            hidMatdes.Value = Request.QueryString("MatId").ToString()
            hidMatDes1.Value = Request.QueryString("Des").ToString()
            If Not IsPostBack Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Value", "MaterialDet();", True)
            End If

        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    Public Sub GetPageDetails()
        Dim ds As New DataSet
        Dim objGetData As New Selectdata()
        Dim dsEmat As New DataSet
        Try
            ds = objGetData.GetMaterials(hidMatId.Value, "", "")
            lblName.Text = ds.Tables(0).Rows(0).Item("MATDES").ToString()
            dsEmat = objGetData.GetEditMaterial(Session("MoldE1CaseId"))
            If hidMatId.Value = dsEmat.Tables(0).Rows(0).Item("M" + hidId.Value) Then
                txtName.Text = dsEmat.Tables(0).Rows(0).Item("MATDES" + hidId.Value)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnCall_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCall.Click
        GetPageDetails()
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Dim objUpIns As New UpdateInsert()
        Try
            objUpIns.EditMaterialName(Session("MoldE1CaseId"), hidMatId.Value, txtName.Text, hidId.Value)
            hidMatName.Value = txtName.Text.ToString()
            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "AlertMsg", "alert('Material Name Edited Successfully');", True)
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClosePage", "CloseWindow();", True)
        Catch ex As Exception

        End Try

    End Sub
End Class
