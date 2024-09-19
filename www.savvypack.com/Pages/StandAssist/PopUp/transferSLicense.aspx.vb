Imports System
Imports System.Data
Imports StandGetData
Imports StandUpInsData
Imports AjaxControlToolkit
Partial Class Popup_transferSLicense
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                BindUser()
            End If

        Catch ex As Exception
            Response.Write("Error" + ex.Message.ToString())
        End Try
    End Sub
    Public Sub BindUser()
        Dim ds As New DataSet()
        Dim objGetData As New Selectdata()
        Dim dsDetail As New DataSet
        Try
            ds = objGetData.LicenseFSTransferUser(Session("USERID").ToString())
            If ds.Tables(0).Rows.Count > 0 Then
                With ddlFromUser
                    .DataSource = ds
                    .DataTextField = "USERNAME"
                    .DataValueField = "USERID1"
                    .DataBind()
                End With
            End If
            lblToUser.Text = Request.QueryString("usr").ToString()

            dsDetail = objGetData.GetUserInforMation(ddlFromUser.SelectedValue)
            If dsDetail.Tables(0).Rows.Count > 0 Then
                If dsDetail.Tables(0).Rows(0).Item("ISIADMINLICUSR") = "Y" Then
                    hidAdminLic.Value = "Y"
                Else
                    hidAdminLic.Value = "N"
                End If

            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnTransfer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTransfer.Click
        Dim objGetdata As New Selectdata()
        Dim objUpIns As New UpdateInsert()
        Dim ds As DataSet
        Dim Dts As DataSet
        Try
            Dts = objGetdata.GetUserInforMation(ddlFromUser.SelectedValue)
            ds = objGetdata.GetServiceUserData(Request.QueryString("usr").ToString(), "StandAssist")
            If ds.Tables(0).Rows.Count > 0 Then
            Else

                ''Giving Acccess to New User
                ''objUpIns.AddTransferLicenseD(Dts.Tables(0).Rows(0).Item("LICENSEID").ToString(), Request.QueryString("usr").ToString(), "KBOK")
                'objUpIns.AddOrderUsersD(Request.QueryString("usr").ToString(), "StandAssist")

                ''Taking Out Access from Old User
                'objUpIns.DeleteOrderUsers(ddlFromUser.SelectedValue, "StandAssist")
                'objUpIns.UpdateSLicenseData(Session("LICID").ToString(), ddlFromUser.SelectedValue, Request.QueryString("usr").ToString())
            End If
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "Close();", True)


        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlFromUser_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlFromUser.SelectedIndexChanged
        Dim objGetData As New Selectdata()
        Dim dsDetail As New DataSet
        Try
            dsDetail = objGetData.GetUserInforMation(ddlFromUser.SelectedValue)
            If dsDetail.Tables(0).Rows.Count > 0 Then
                If dsDetail.Tables(0).Rows(0).Item("ISIADMINLICUSR") = "Y" Then
                    hidAdminLic.Value = "Y"
                Else
                    hidAdminLic.Value = "N"
                End If

            End If

        Catch ex As Exception

        End Try
    End Sub
End Class
