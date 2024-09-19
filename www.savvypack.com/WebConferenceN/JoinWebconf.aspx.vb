
Partial Class WebConferenceN_JoinWebconf
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("MenuItem") = "JWEBCONF"
    End Sub
End Class
