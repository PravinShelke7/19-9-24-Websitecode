
Partial Class Universal_loginN_Pages_ReportLicAgreement
    Inherits System.Web.UI.Page
    Protected Sub btnAccept_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAccept.Click
        Try
            Dim objUpdateData As New LoginUpdateData.Selectdata
            Dim password As String
            Dim userName As String
            userName = Session("USERID").ToString()
            password = Session("Password").ToString()
            objUpdateData.InsertLicense(userName, password, "REPORT")
            Response.Redirect("Index1.aspx")
        Catch ex As Exception
            lblError.Text = "Error:SetSessions:" + ex.Message.ToString()
        End Try

    End Sub

    Protected Sub btnDecline_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDecline.Click
        Try
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "CloseWindow", "javascript:window.close();", True)
        Catch ex As Exception
            lblError.Text = "Error:SetSessions:" + ex.Message.ToString()
        End Try

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            lblTag.Text = "Copyright 1997 - " + Now.Year().ToString() + " Allied Development Corp."
        Catch ex As Exception
        End Try
    End Sub
End Class
