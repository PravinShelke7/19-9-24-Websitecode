Imports E1GetData
Imports System
Imports System.Data
Partial Class Pages_Econ3_PopUp_GroupSelection
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                BindGrdGroup()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub BindGrdGroup()
        Dim objGetData As New E1GetData.Selectdata
        Dim ds As New DataSet()
        Dim RegionSetId As String = String.Empty
        Dim objCryptoHelper As New CryptoHelper
        Try
            If Session("Service") = "COMP" Then
                ds = objGetData.GetAllCompGroupDetails(Session("USERID").ToString(), Session("CompServiceId").ToString())
            Else
                ds = objGetData.GetAllGroupDetails(Session("USERID").ToString())
            End If

            If ds.Tables(0).Rows.Count = 0 Then
                lblmsg.Text = "No groups created"
                lblmsg.Visible = True
            End If
            With grdGroup
                .DataSource = ds
                .DataBind()
                .PageIndex = 0
            End With
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Dim linkBut As New LinkButton
        Dim lblGrpId As New Label
        Dim Chk As New CheckBox
        Dim SelGrpID As String = String.Empty
        Try
            For Each Gr As GridViewRow In grdGroup.Rows
                lblGrpId = grdGroup.Rows(Gr.RowIndex).FindControl("lblGroupID")
                Chk = grdGroup.Rows(Gr.RowIndex).FindControl("SelForGrp")
                If Chk.Checked = True Then
                    SelGrpID = SelGrpID + lblGrpId.Text + ","
                End If
            Next

            If SelGrpID <> "" Then
                SelGrpID = SelGrpID.Remove(SelGrpID.Length - 1)
            End If
            hidCaseid.Value = SelGrpID
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "AdvClose", "CloseAndUpdate();", True)
        Catch ex As Exception

        End Try
    End Sub
End Class
