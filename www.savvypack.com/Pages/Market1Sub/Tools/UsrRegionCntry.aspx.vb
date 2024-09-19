Imports System.Data
Imports System.Data.OleDb
Imports System
Imports M1GetData
Imports M1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Partial Class Pages_Market1_Tools_UsrRegionCntry
    Inherits System.Web.UI.Page
#Region "Get Set Variables"
    Dim _lErrorLble As Label
    Dim _iUserId As Integer
    Dim _iRegionId As Integer
    Dim _strUserRole As String
    Dim _btnLogOff As ImageButton
    Dim _btnUpdate As ImageButton
    Dim _divMainHeading As HtmlGenericControl
    Dim _ctlContentPlaceHolder As ContentPlaceHolder


    Public Property ErrorLable() As Label
        Get
            Return _lErrorLble
        End Get
        Set(ByVal Value As Label)
            _lErrorLble = Value
        End Set
    End Property

    Public Property UserId() As Integer
        Get
            Return _iUserId
        End Get
        Set(ByVal Value As Integer)
            _iUserId = Value
        End Set
    End Property

    Public Property RegionId() As Integer
        Get
            Return _iRegionId
        End Get
        Set(ByVal Value As Integer)
            _iRegionId = Value
        End Set
    End Property

    Public Property UserRole() As String
        Get
            Return _strUserRole
        End Get
        Set(ByVal Value As String)
            _strUserRole = Value
        End Set
    End Property

    Public Property LogOffbtn() As ImageButton
        Get
            Return _btnLogOff
        End Get
        Set(ByVal value As ImageButton)
            _btnLogOff = value
        End Set
    End Property

    Public Property Updatebtn() As ImageButton
        Get
            Return _btnUpdate
        End Get
        Set(ByVal value As ImageButton)
            _btnUpdate = value
        End Set
    End Property

    Public Property MainHeading() As HtmlGenericControl
        Get
            Return _divMainHeading
        End Get
        Set(ByVal value As HtmlGenericControl)
            _divMainHeading = value
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
        GetErrorLable()
        GetLogOffbtn()
        GetMainHeadingdiv()
        GetUpdtaebtn()
        'GetContentPlaceHolder()
    End Sub

    Protected Sub GetErrorLable()
        ErrorLable = Page.Master.FindControl("lblError")
    End Sub

    Protected Sub GetLogOffbtn()
        LogOffbtn = Page.Master.FindControl("imgLogoff")
        LogOffbtn.Visible = False
    End Sub

    Protected Sub GetUpdtaebtn()
        Updatebtn = Page.Master.FindControl("imgUpdate")
        Updatebtn.Visible = True
        AddHandler Updatebtn.Click, AddressOf Update_Click
    End Sub

    Protected Sub GetMainHeadingdiv()
        MainHeading = Page.Master.FindControl("divMainHeading")
        MainHeading.Attributes.Add("onmouseover", "Tip('User Regions')")
        MainHeading.Attributes.Add("onmouseout", "UnTip()")
        MainHeading.InnerHtml = "CAGR Reports"
    End Sub

    Protected Sub GetContentPlaceHolder()
        ctlContentPlaceHolder = Page.Master.FindControl("Market1ContentPlaceHolder")
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objCrypto As New CryptoHelper
        Try
            RegionId = objCrypto.Decrypt(Request.QueryString("RegionId").ToString())
            GetMasterPageControls()
            If Not IsPostBack Then
                GetPageDetails()
            End If


        Catch ex As Exception
            ErrorLable.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetPageDetails()
        Dim objGetData As New Selectdata
        Dim ds As New DataSet
        Dim i As New Integer
        Dim j As New Integer
        Try
            ds = objGetData.GetRegionCntryDetails(RegionId)
            If RegionId <> 0 Then
                txtRegionName.Text = ds.Tables(0).Rows(0).Item("REGRIONNAME").ToString()
            End If



            With chkListCntries
                .DataSource = ds
                .DataTextField = "COUNTRYDES"
                .DataValueField = "COUNTRYID"
                .DataBind()
            End With


            For Each chk As ListItem In chkListCntries.Items
                If ds.Tables(0).Rows(i).Item("ISINREGION").ToString() = "Y" Then
                    chk.Selected = True
                Else
                    chk.Selected = False
                End If
                i = i + 1
            Next


        Catch ex As Exception
            ErrorLable.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub Update_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim CountryIds As String = String.Empty
        Dim UserId As String = String.Empty
        Dim i As New Integer
        Dim objUpIns As New UpdateInsert()
        Try
            UserId = Session("UserId")
            For Each lst As ListItem In chkListCntries.Items
                If lst.Selected Then
                    CountryIds = CountryIds + lst.Value.ToString() + ","
                End If
            Next
            CountryIds = CountryIds.Remove(CountryIds.Length - 1, 1)
            If RegionId = 0 Then
                objUpIns.InsertUpdateCountryRegions(RegionId, txtRegionName.Text.Trim.ToString(), CountryIds, UserId, True)
            Else
                objUpIns.InsertUpdateCountryRegions(RegionId, txtRegionName.Text.Trim.ToString(), CountryIds, UserId, False)
            End If

            Response.Redirect("UserRegions.aspx", True)

        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString()
        End Try
    End Sub

End Class
