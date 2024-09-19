
Partial Class ApplicationDevelopment_Examples
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("MenuItem") = "APPDEV"
    End Sub
End Class
