
Partial Class WebConferenceN_WebConfHome
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("MenuItem") = "WEBCONF"
    End Sub
End Class
