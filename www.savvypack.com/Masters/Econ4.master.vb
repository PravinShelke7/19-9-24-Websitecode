
Partial Class Masters_Econ4
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblTag.Text = "Copyright 1997 - " + Now.Year().ToString() + " Savvypack Corporation."
        If (Session("Back")) = Nothing Then
            ContentPage.Visible = False
            Dim obj As New CryptoHelper
            Response.Redirect("~/Pages/Econ4/Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
        Else
            ContentPage.Visible = True
        End If

    End Sub
End Class

