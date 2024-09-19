
Partial Class Masters_MoldS1Chart
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            lblTag.Text = "Copyright 1997 - " + Now.Year().ToString() + " SavvyPack Corporation"
            If Session("Service") = "COMPS1" Then
                S1Table.Attributes.Add("class", "S1CompModule")
            End If
        Catch ex As Exception
        End Try
        If Session("Back") = Nothing Then
            Dim obj As New CryptoHelper
            Response.Redirect("~/Pages/MoldSustain1/Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
        End If
    End Sub
End Class

