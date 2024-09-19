
Partial Class ShoppingCart_Thanks
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
		 If Session("Back") = Nothing And Session("SBack") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("~/Universal_loginN/Pages/ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "", False)
            End If
			 Session("MenuItem") = "SHOPPING"
                lblRefNumber.Text = "<b>Reference Number: </b>" + Session("RefNumber").ToString()
            ' If Session("Back") = Nothing Then
                ' Dim obj As New CryptoHelper
                ' Response.Redirect("~/Universal_loginN/Pages/ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "", False)
            ' Else
                ' Session("MenuItem") = "SHOPPING"
                ' lblRefNumber.Text = "<b>Reference Number: </b>" + Session("RefNumber").ToString()
            ' End If
            
        Catch ex As Exception

        End Try
    End Sub
End Class
