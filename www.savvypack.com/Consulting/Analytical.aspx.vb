
Partial Class Consulting_Analytical
    Inherits System.Web.UI.Page

    Dim obj As New CryptoHelper

#Region "Get Set Variables"

    Dim _lnkSavvy As LinkButton
    Dim _lnkAcc As LinkButton
    Dim _lnkoData As LinkButton
    Dim _lnkLogin As LinkButton
    Dim _lnkTool As LinkButton
	Dim _lnkAnal As LinkButton
    Dim _lnkStrc As LinkButton

    Public Property Savvylnk() As LinkButton
        Get
            Return _lnkSavvy
        End Get
        Set(ByVal value As LinkButton)
            _lnkSavvy = value
        End Set
    End Property

    Public Property Acclnk() As LinkButton
        Get
            Return _lnkAcc
        End Get
        Set(ByVal value As LinkButton)
            _lnkAcc = value
        End Set
    End Property

    Public Property oDatalnk() As LinkButton
        Get
            Return _lnkoData
        End Get
        Set(ByVal value As LinkButton)
            _lnkoData = value
        End Set
    End Property

    Public Property Loginlnk() As LinkButton
        Get
            Return _lnkLogin
        End Get
        Set(ByVal value As LinkButton)
            _lnkLogin = value
        End Set
    End Property

    Public Property Toollnk() As LinkButton
        Get
            Return _lnkTool
        End Get
        Set(ByVal value As LinkButton)
            _lnkTool = value
        End Set
    End Property
	
	Public Property Anallnk() As LinkButton
        Get
            Return _lnkAnal
        End Get
        Set(ByVal value As LinkButton)
            _lnkAnal = value
        End Set
    End Property

    Public Property Strclnk() As LinkButton
        Get
            Return _lnkStrc
        End Get
        Set(ByVal value As LinkButton)
            _lnkStrc = value
        End Set
    End Property

#End Region

#Region "MastePage Content Variables"

    Protected Sub GetMasterPageControls()
        GetSavvybtn()
        GetAccbtn()
        GetoDatabtn()
        GetLoginbtn()
        GetToolbtn()
		GetAnalbtn()
        GetStrcbtn()
    End Sub

    Protected Sub GetSavvybtn()
        Savvylnk = Page.Master.FindControl("lnkSavvyPack")
        If Session("UserId") = Nothing Then
            Savvylnk.Attributes.Add("onClick", "return OpenLoginPopup('Y');")
        Else
            If Session("INTUSR") = "Y" Then
                Savvylnk.Attributes.Add("onClick", "return OpenUManagerPopUp('../Popup/UIntManager.aspx?Id=hidTId');")
            Else
                Savvylnk.Attributes.Add("onClick", "return OpenUManagerPopUp('../Popup/Umanager.aspx?Id=hidTId');")
            End If
        End If
    End Sub

    Protected Sub GetAccbtn()
        Acclnk = Page.Master.FindControl("lnkAcco")
        If Session("UserId") = Nothing Then
            Acclnk.Attributes.Add("onClick", "return OpenLoginPopup('Y');")
        Else
            Acclnk.Attributes.Add("onClick", "return OpenAccWindow('../Users_Login/AddEditUser.aspx?Mode=" + obj.Encrypt("Edit").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "');")
        End If
    End Sub

    Protected Sub GetoDatabtn()
        oDatalnk = Page.Master.FindControl("lnkOData")
        If Session("UserId") = Nothing Then
            oDatalnk.Attributes.Add("onClick", "return OpenLoginPopup('Y');")
        Else
            oDatalnk.Attributes.Add("onClick", "return OpenUManagerPopUp('../Popup/ODatabase.aspx?Id=hidTId');")
        End If
    End Sub

    Protected Sub GetLoginbtn()
        Loginlnk = Page.Master.FindControl("lnkLogout")
        If Session("UserId") = Nothing Then
            Loginlnk.Attributes.Add("onClick", "return OpenLoginPopup('N');")
        End If
    End Sub

    Protected Sub GetToolbtn()
        Toollnk = Page.Master.FindControl("lnkSavvy")
        If Session("UserId") = Nothing Then
            Toollnk.Attributes.Add("onClick", "return OpenLoginPopup('Y');")
        Else
            Toollnk.Attributes.Add("onClick", "return OpenNewWindow('../OnlineForm/ProjectManager.aspx');")
        End If
    End Sub
	
	Protected Sub GetAnalbtn()
        Anallnk = Page.Master.FindControl("lnkAnal")
        Anallnk.Attributes.Add("onClick", "return OpenNewWindow('../AnalyticalService.aspx');")
    End Sub

    Protected Sub GetStrcbtn()
        Strclnk = Page.Master.FindControl("lnkStrc")
        Strclnk.Attributes.Add("onClick", "return OpenNewWindow('../Structure Assistant.aspx');")
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Session("MenuItem") = "CONSULT"
            Dim UserLogin_Path As String = ""
            'If HttpContext.Current.Request.IsSecureConnection = True Then
			'UserLogin_Path = System.Configuration.ConfigurationManager.AppSettings("UserLogin_PathI")
			'Else
			UserLogin_Path = System.Configuration.ConfigurationManager.AppSettings("UserLogin_Path")
			'End If
            If UserLogin_Path <> "" Then
                hdnUserLoginPath.Value = UserLogin_Path
            End If
            GetMasterPageControls()
        Catch ex As Exception

        End Try
    End Sub

End Class
