
Partial Class InteractiveServices_OnlineResearch
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("MenuItem") = "ISERVICE"
    End Sub
End Class
