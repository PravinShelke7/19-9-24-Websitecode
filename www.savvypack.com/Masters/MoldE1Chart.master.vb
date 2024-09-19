
Partial Class Masters_MoldE1Chart
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            lblTag.Text = "Copyright 1997 - " + Now.Year().ToString() + " SavvyPack Corporation"
        Catch ex As Exception
        End Try
        If Session("Back") = Nothing Then
            Dim obj As New CryptoHelper
            Response.Redirect("~/Pages/MoldEcon1/Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
        End If
        If Session("Service") = "COMP" Then
            E1Table.Attributes.Add("class", "E1CompModule")
        End If

    End Sub
End Class

