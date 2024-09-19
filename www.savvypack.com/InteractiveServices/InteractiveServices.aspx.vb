
Partial Class InteractiveServices_InteractiveServices
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
        Session("MenuItem") = "ISERVICE"
        GetMasterPageControls()
    End Sub
End Class
