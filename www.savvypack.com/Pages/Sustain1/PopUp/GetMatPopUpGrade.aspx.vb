Imports System.Data
Imports System.Data.OleDb
Imports System
Imports S1GetData
Imports S1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Sustain1_PopUp_GetMatPopUpGrade
    Inherits System.Web.UI.Page
    Dim ds As New DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            hidMatdes.Value = Request.QueryString("Des").ToString()
            hidMatid.Value = Request.QueryString("ID").ToString()

            hidGradedes.Value = Request.QueryString("GradeDes").ToString()
            hidGradeid.Value = Request.QueryString("GradeId").ToString()

            hidSG.Value = Request.QueryString("SG").ToString()
			hidDMatId.Value = Request.QueryString("MatId").ToString()
            If Not IsPostBack Then
                'Page.ClientScript.RegisterStartupScript(Me.GetType, "GetId", "testData();", True)
                GetMaterialDetails()
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            GetMaterialDetails()
        Catch ex As Exception
            _lErrorLble.Text = "Error:btnSearch_Click:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub GetMaterialDetails()
        Dim ds As New DataSet
        Dim objGetData As New S1GetData.Selectdata()

        Try
            ds = objGetData.GetPopupMaterials("-1", txtMatDe1.Text.Trim.ToString(), txtMatDe2.Text.Trim.ToString(), Session("S1CaseId"))
            grdMaterials.DataSource = ds
            grdMaterials.DataBind()
            Session("ds") = ds
            BindLink()
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetMaterialDetails:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub btnCall_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCall.Click
        GetMaterialDetails()

    End Sub

    Protected Sub BindLink()
        Dim lblName As New Label
        Dim lblDes As New Label
        Dim lnkBtnval As New LinkButton
        Dim dsmat As New DataSet
        Dim ds As New DataSet
        Dim hyp As New HtmlAnchor
        Dim dsEmat As DataSet
        Dim objGetData As New S1GetData.Selectdata
        
        Try
            dsEmat = objGetData.GetEditMaterial(Session("S1CaseId"))
            Session("dsEmat") = dsmat
            dsmat = DirectCast(Session("dsEmat"), DataSet)
            ds = DirectCast(Session("ds"), DataSet)
            For Each Gr As GridViewRow In grdMaterials.Rows

                lblName = grdMaterials.Rows(Gr.RowIndex).FindControl("lblName")
                lnkBtnval = grdMaterials.Rows(Gr.RowIndex).FindControl("lnkBtnval")

                If hidDMatId.Value = grdMaterials.DataKeys(Gr.RowIndex).Value.ToString() Then
                    'If dsmat.Tables(0).Rows(0).Item("MATDES" + hidId.Value).ToString() <> "" Then
                    '    Gr.ToolTip = dsmat.Tables(0).Rows(0).Item("MATDES" + hidId.Value).ToString()
                    'Else
                    '    Gr.ToolTip = lblName.Text.ToString()
                    'End If

                    lnkBtnval.ForeColor = Drawing.Color.Red
                    'Gr.Height = 30
                    Gr.Attributes.Add("style", "color:Red;")


                    If Gr.RowIndex > 10 Then
                        If grdMaterials.Rows.Count - Gr.RowIndex > 10 Then
                            grdMaterials.Rows(Gr.RowIndex + 6).Focus()
                        ElseIf grdMaterials.Rows.Count - Gr.RowIndex < 10 Then
                            grdMaterials.Rows(grdMaterials.Rows.Count - 1).Focus()
                        Else
                            grdMaterials.Rows(Gr.RowIndex).Focus()
                        End If
                    Else
                        grdMaterials.Rows(Gr.RowIndex).Focus()
                    End If

                End If

                lnkBtnval.Attributes("onclick") = "javascript:return MaterialDet('" + ds.Tables(0).Rows(Gr.RowIndex).Item("MATDES").ToString() + "'," + ds.Tables(0).Rows(Gr.RowIndex).Item("MATID").ToString() + ",'" + ds.Tables(0).Rows(Gr.RowIndex).Item("GRADENAME").ToString() + "'," + ds.Tables(0).Rows(Gr.RowIndex).Item("GRADEID").ToString() + ",'" + ds.Tables(0).Rows(Gr.RowIndex).Item("WEIGHT").ToString() + "','" + ds.Tables(0).Rows(Gr.RowIndex).Item("SG").ToString() + "','" + ds.Tables(0).Rows(Gr.RowIndex).Item("MATDES3").ToString() + "')"
            Next
        Catch ex As Exception

        End Try

    End Sub
End Class
