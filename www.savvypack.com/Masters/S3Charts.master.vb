
Partial Class Masters_S3Charts
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            lblTag.Text = "Copyright 1997 - " + Now.Year().ToString() + " Savvypack Corporation."
        Catch ex As Exception
        End Try
        If Session("Back") = Nothing Then
            Dim obj As New CryptoHelper
            Response.Redirect("~/Pages/Sustain3/Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
        End If
    End Sub
End Class

