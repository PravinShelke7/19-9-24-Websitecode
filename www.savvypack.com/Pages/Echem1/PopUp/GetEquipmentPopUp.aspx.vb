Imports System.Data
Imports System.Data.OleDb
Imports System
Imports Echem1GetData
Imports Echem1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Echem1_PopUp_GetEquipmentPopUp
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            hidEqdes.Value = Request.QueryString("Des").ToString()
            hidEqid.Value = Request.QueryString("EId").ToString()
            hidEid.Value = Request.QueryString("ID").ToString()
            hidMod.Value = Session("EquipMod")
            If Not IsPostBack Then
                GetEquipmentDetails()
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            GetEquipmentDetails()
        Catch ex As Exception
            _lErrorLble.Text = "Error:btnSearch_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetEquipmentDetails()
        Dim ds As New DataSet
        Dim objGetData As New Echem1GetData.Selectdata()
        Try
            'ds = objGetData.GetEquipment(-1, txtMatDe1.Text.Trim.ToString(), txtMatDe2.Text.Trim.ToString())
            'grdEquipment.DataSource = ds
            'grdEquipment.DataBind()
            ds = objGetData.GetPopUpEquipment(Request.QueryString("Case").ToString(), txtMatDe1.Text.Trim.ToString(), txtMatDe2.Text.Trim.ToString())
            grdEquipment.DataSource = ds
            grdEquipment.DataBind()
            Session("ds") = ds
            BindLink()
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetMaterialDetails:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub BindLink()
        Dim lblName As New Label
        Dim lblDes As New Label
        Dim lnkBtnval As New LinkButton
        Dim dsmat As New DataSet
        Dim ds As New DataSet
        Dim hyp As New HtmlAnchor
        Dim dsEmat As DataSet
        Dim objGetData As New Echem1GetData.Selectdata
        Try
            'dsEmat = objGetData.GetEditEquip(Session("Echem1CaseId"))
            'Session("dsEmat") = dsEmat
            'dsmat = DirectCast(Session("dsEmat"), DataSet)
            ds = DirectCast(Session("ds"), DataSet)
            For Each Gr As GridViewRow In grdEquipment.Rows
                lblName = grdEquipment.Rows(Gr.RowIndex).FindControl("lblName")
                lnkBtnval = grdEquipment.Rows(Gr.RowIndex).FindControl("lnkBtnval")

                If hidEqid.Value = grdEquipment.DataKeys(Gr.RowIndex).Value.ToString() Then
                    lnkBtnval.ForeColor = Drawing.Color.Red
                    Gr.Attributes.Add("style", "color:Red;")

                    If Gr.RowIndex > 10 Then
                        If grdEquipment.Rows.Count - Gr.RowIndex > 5 Then
                            grdEquipment.Rows(Gr.RowIndex + 6).Focus()
                        ElseIf grdEquipment.Rows.Count - Gr.RowIndex < 5 Then
                            grdEquipment.Rows(grdEquipment.Rows.Count - 1).Focus()
                        Else
                            grdEquipment.Rows(Gr.RowIndex).Focus()
                        End If
                    Else
                        grdEquipment.Rows(Gr.RowIndex).Focus()
                    End If
                End If

                lnkBtnval.Attributes("onclick") = "javascript:return EquipmentDet('" + ds.Tables(0).Rows(Gr.RowIndex).Item("EQUIPDES").ToString() + "'," + ds.Tables(0).Rows(Gr.RowIndex).Item("EQUIPID").ToString() + ",'" + ds.Tables(0).Rows(Gr.RowIndex).Item("ELabel").ToString() + "')"
            Next
        Catch ex As Exception

        End Try

    End Sub
End Class
