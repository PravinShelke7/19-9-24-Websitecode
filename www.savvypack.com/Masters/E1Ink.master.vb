
Partial Class Masters_E1Ink
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            lblTag.Text = "Copyright 1997 - " + Now.Year().ToString() + " Savvypack Corporation."
            If Session("Service") = "COMP" Then
                E1Table.Attributes.Add("class", "E1CompModule")
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class

