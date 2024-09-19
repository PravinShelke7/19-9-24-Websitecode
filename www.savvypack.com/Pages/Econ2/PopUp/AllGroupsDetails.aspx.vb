Imports E2GetData
Imports System
Imports System.Data
Partial Class Pages_Econ1_PopUp_AllGroupsDetails
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                BindGrdGroup()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub BindGrdGroup()
        Dim objGetData As New E2GetData.Selectdata
        Dim ds As New DataSet()
        Dim RegionSetId As String = String.Empty
        Dim objCryptoHelper As New CryptoHelper
        Try
            ds = objGetData.GetAllGroupDetails(Session("USERID").ToString())
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
    Protected Sub GetGroupsForCase()
        Dim linkBut As New LinkButton
        Dim lblGrpId As New Label
        For Each Gr As GridViewRow In grdGroup.Rows
            lblGrpId = grdGroup.Rows(Gr.RowIndex).FindControl("lblGroupID")
            linkBut = grdGroup.Rows(Gr.RowIndex).FindControl("lnkGroId")
            If lblGrpId.Text = 0 Then
                linkBut.Enabled = False
            Else
                linkBut.Enabled = True
                linkBut.Attributes.Add("onclick", "return testData('Popup/GroupDetails.aspx?groupId=" + lblGrpId.Text.Trim() + "');")
            End If
        Next
    End Sub
End Class
