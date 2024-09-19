Imports System.Data
Imports System.Data.OleDb
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Drawing.Imaging
Partial Class InteractiveServices_InfoOnSponsor
    Inherits System.Web.UI.Page

#Region "Get Set Variables"
    Dim _hypSInteractiveService As HyperLink
    Public Property SIServices() As HyperLink
        Get
            Return _hypSInteractiveService
        End Get
        Set(ByVal Value As HyperLink)
            _hypSInteractiveService = Value
        End Set
    End Property
#End Region
#Region "MastePage Content Variables"
    Protected Sub GetMasterPageControls()
        ' GetSPublicationLink()
    End Sub
    Protected Sub GetSPublicationLink()
        SIServices = Page.Master.FindControl("hypSIServices")
        SIServices.CssClass = "LinkSubMenuClick"
    End Sub
#End Region
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ObjCrypto As New CryptoHelper()
        Try
            Session("MenuItem") = "ISERVICE"
            GetMasterPageControls()
            If Session("UserId") <> Nothing Then
                hdnUserId.Value = Session("UserId")
            End If
            'Set Report Id
            hdnRepId.Value = ObjCrypto.Encrypt("SA")
        Catch ex As Exception
            lblError.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub
End Class
