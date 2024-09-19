
Partial Class OnlineForm_Popup_UUpload
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            'Response.Redirect("http://dataexchange.allied-dev.com/UniversalUploadFiles.aspx?ProjectId=" + Request.QueryString("ProjectId").ToString() + "&FileType=" + Request.QueryString("FileType").ToString() + "&UID=" + Session("UserId") + "&Flag=Y")
            Response.Redirect("http://dataexchange.allied-dev.com")
Response.write("Hi")
        Catch ex As Exception

        End Try
    End Sub
End Class
