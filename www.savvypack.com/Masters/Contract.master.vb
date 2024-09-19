
Partial Class Masters_Contract
    Inherits System.Web.UI.MasterPage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Try
                lblTag.Text = "Copyright 1997 - " + Now.Year().ToString() + " SavvyPack Corporation"
            Catch ex As Exception
            End Try
            CheckSecurity()

        Catch ex As Exception
            lblError.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub CheckSecurity()
        If Session("Back") = Nothing Then
            ContentPage.Visible = False
            lblError.Text = "Session is Expired"
            Dim obj As New CryptoHelper
            Response.Redirect("~/Pages/Contract/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
        End If
    End Sub
End Class

