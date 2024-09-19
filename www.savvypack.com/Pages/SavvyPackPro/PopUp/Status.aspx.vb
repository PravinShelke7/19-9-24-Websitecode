Imports System.Data
Imports System.Data.OleDb
Imports System
Imports SavvyProGetData
Imports SavvyProUpInsData
Partial Class Pages_SavvyPackPro_PopUp_Status
    Inherits System.Web.UI.Page
    Dim objGetdata As New SavvyProGetData

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        GetStatusList()
    End Sub

    Protected Sub GetStatusList()
        Dim Ds As New DataSet

        Try
            Ds = objGetdata.GetStatuslist()

            If Ds.Tables(0).Rows.Count > 0 Then
                lblPPC.Visible = False
                grdstatus.Visible = True
                grdstatus.DataSource = Ds
                grdstatus.DataBind()
            Else
                lblPPC.Visible = True
                grdstatus.Visible = False
            End If
        Catch ex As Exception
            Response.Write("Error:GetStatusList:" + ex.Message.ToString())
        End Try
    End Sub

End Class
