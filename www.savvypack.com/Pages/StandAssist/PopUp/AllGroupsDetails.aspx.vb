Imports StandGetData
Imports System
Imports System.Data
Partial Class Pages_StandAssist_PopUp_AllGroupsDetails
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim objUpIns As New StandUpInsData.UpdateInsert
            If Session("Back") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If
            If Not IsPostBack Then
                BindGrdGroup()
				
				  'Started Acticity Log Changes
            Try
                objUpIns.InsertLog1(Session("UserId").ToString(), "14", "Opened Group Details PopUp", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
            Catch ex As Exception

            End Try

            'Ended Acticity Log Changes
            End If
              Catch ex As Exception

        End Try
    End Sub
    Protected Sub BindGrdGroup()
        Dim objGetData As New StandGetData.Selectdata
        Dim ds As New DataSet()
        Dim RegionSetId As String = String.Empty
        Dim objCryptoHelper As New CryptoHelper
        Try
            If Request.QueryString("Type").ToString() = "PROP" Then
                ds = objGetData.GetAllGroupDetails(Session("USERID").ToString(), Request.QueryString("Type").ToString())
                If ds.Tables(0).Rows.Count = 0 Then
                    lblmsg.Text = "No groups created"
                    lblmsg.Visible = True
                End If
                With grdGroup
                    .DataSource = ds
                    .DataBind()

                End With

            ElseIf Request.QueryString("Type").ToString() = "BASE" Then
                ds = objGetData.GetAllBGroupDetails(Session("USERID").ToString(), Request.QueryString("Type").ToString())
                If ds.Tables(0).Rows.Count = 0 Then
                    lblmsg.Text = "No groups created"
                    lblmsg.Visible = True
                End If
                With grdGroup
                    .DataSource = ds
                    .DataBind()

                End With
            ElseIf Request.QueryString("Type").ToString() = "CPROP" Then
                ds = objGetData.GetAllCompGroupDetails(Session("USERID").ToString(), Request.QueryString("Type").ToString())
                If ds.Tables(0).Rows.Count = 0 Then
                    lblmsg.Text = "No groups created"
                    lblmsg.Visible = True
                End If
                With grdGroup
                    .DataSource = ds
                    .DataBind()

                End With

            End If
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
