
Partial Class Consulting_Consulting
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Session("MenuItem") = "CONSULT"
        Catch ex As Exception
        End Try
    End Sub
End Class
