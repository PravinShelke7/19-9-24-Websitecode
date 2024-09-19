
Partial Class OnlineForm_Popup_ConfirmationPopup
    Inherits System.Web.UI.Page

  
   
    Protected Sub ok_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ok.Click

        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "close", "ClosePopup();", True)
        'Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "close", "ClosePopup();", True)

    End Sub
End Class
