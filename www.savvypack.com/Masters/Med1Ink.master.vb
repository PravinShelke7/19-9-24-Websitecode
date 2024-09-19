
Partial Class Masters_E1Ink
    Inherits System.Web.UI.MasterPage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Try
                lblTag.Text = "Copyright 1997 - " + Now.Year().ToString() + " SavvyPack Corporation"
            Catch ex As Exception
            End Try
            
        Catch ex As Exception
            lblError.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub
End Class

