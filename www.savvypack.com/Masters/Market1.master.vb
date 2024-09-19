
Partial Class Masters_Market1
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim obj As New CryptoHelper
        Try
            lblTag.Text = "Copyright 1997 - " + Now.Year().ToString() + " SavvyPack Corporation"
        Catch ex As Exception
        End Try
        If (Session("Back")) = Nothing Then
            ContentPage.Visible = False
            lblError.Text = "Session is Expired"
            Response.Redirect("~/Pages/Market1/Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
        Else
            ContentPage.Visible = True
        End If

    End Sub
End Class

