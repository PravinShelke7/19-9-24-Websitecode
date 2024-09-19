
Partial Class Masters_E1Chart
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            lblTag.Text = "Copyright 1997 - " + Now.Year().ToString() + " Savvypack Corporation."
        Catch ex As Exception
        End Try
        If Session("Back") = Nothing Then
            Dim obj As New CryptoHelper
            Response.Redirect("~/Pages/Econ1/Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
        End If
        If Session("Service") = "COMP" Then
            E1Table.Attributes.Add("class", "E1CompModule")
        End If

    End Sub
End Class

