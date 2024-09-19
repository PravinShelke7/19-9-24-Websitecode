Imports System
Imports System.Data
Partial Class DownLoad_DownloadList
    Inherits System.Web.UI.Page
#Region "Get Set Variables"
    Dim lblError As Label
    Dim _ctlContentPlaceHolder As ContentPlaceHolder



    Public Property ErrorLable() As Label
        Get
            Return lblError
        End Get
        Set(ByVal Value As Label)
            lblError = Value
        End Set
    End Property



    Public Property ctlContentPlaceHolder() As ContentPlaceHolder
        Get
            Return _ctlContentPlaceHolder
        End Get
        Set(ByVal value As ContentPlaceHolder)
            _ctlContentPlaceHolder = value
        End Set
    End Property



#End Region

#Region "MastePage Content Variables"

    Protected Sub GetMasterPageControls()
        Session("MenuItem") = "DWNLD"
        GetErrorLable()
        GetContentPlaceHolder()
    End Sub

    Protected Sub GetErrorLable()
        ErrorLable = Page.Master.FindControl("lblError")
    End Sub


    Protected Sub GetContentPlaceHolder()
        ctlContentPlaceHolder = Page.Master.FindControl("Econ1ContentPlaceHolder")
    End Sub

#End Region

#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_USERS_LOGIN_LOGIN")
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        objRefresh.Render(Page)
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim obj As New CryptoHelper
        Dim objGetData As New ShopGetData.DownloadGetData()
        Dim ds As New DataSet()
        Dim widthVal As String = ""
        Dim i As Integer
        Dim count As Integer = 0
        Session("MenuItem") = "DWNLD1"
        Dim Download_Path As String = System.Configuration.ConfigurationManager.AppSettings("PDFDOWNLOAD_Path")
        Try
            If Session("UserId") Is Nothing Then
                Response.Redirect("Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            Else
                'ifrDownloadPage.Attributes.Add("src", "http://localhost:3662/Production_AdminSiteNew/DownLoadDetails.aspx?UID=" + obj.Encrypt(Session("UserId")))
                ifrDownloadPage.Attributes.Add("src", Download_Path + "?UID=" + obj.Encrypt(Session("UserId")))

                If Request.QueryString("Msg") <> Nothing Then
                    If Request.QueryString("Msg").ToString() = "Y" Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Your account has been activated successfully.');", True)
                    End If
                End If

                ds = objGetData.GetDwnldDetails(Session("UserId").ToString())
                If ds.Tables(0).Rows.Count > 0 Then
                    count = 42
                    For i = 0 To ds.Tables(0).Rows.Count - 1
                        count = count + 22
                    Next
                    widthVal = count.ToString() + "px"
                    lblMessage.Visible = False
                Else
                    widthVal = "42px"
                    lblMessage.Visible = True
                End If
                ifrDownloadPage.Style.Add("Height", widthVal)
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
