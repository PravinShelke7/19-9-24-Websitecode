Imports system.Data
Imports System.Data.OleDb
Partial Class InteractiveServices_Sustain3Module
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Session("MenuItem") = "ISERVICE"
            If Session("UserId") <> Nothing Then
                hdnUserId.Value = Session("UserId")
            End If
        Catch ex As Exception
            lblError.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub
End Class
