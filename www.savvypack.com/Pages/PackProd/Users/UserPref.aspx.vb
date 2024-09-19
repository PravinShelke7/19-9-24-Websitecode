Imports System.Web.UI.HtmlTextWriter
Imports System.Data
Imports PackProdGetData
Imports PackProdUpInsData
Partial Class Pages_PackProd_Users_UserPref
    Inherits System.Web.UI.Page
#Region "Get Set Variables"
    Dim _lErrorLble As Label
    Dim _divMainHeading As HtmlGenericControl
    Dim _ctlContentPlaceHolder As ContentPlaceHolder
    Dim _strCompanyId As String
    Dim _strCategoryId As String
    Dim _btnLogOff As ImageButton
    Dim _btnUpdate As ImageButton
    Dim _btnClose As ImageButton
    Dim _strType As String


    Public Property ErrorLable() As Label
        Get
            Return _lErrorLble
        End Get
        Set(ByVal Value As Label)
            _lErrorLble = Value
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

    Public Property Closebtn() As ImageButton
        Get
            Return _btnClose
        End Get
        Set(ByVal value As ImageButton)
            _btnClose = value
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

    Public Property CompanyId() As String
        Get
            Return _strCompanyId
        End Get
        Set(ByVal value As String)
            _strCompanyId = value
        End Set
    End Property

    Public Property CategoryId() As String
        Get
            Return _strCategoryId
        End Get
        Set(ByVal value As String)
            _strCategoryId = value
        End Set
    End Property

    Public Property Type() As String
        Get
            Return _strType
        End Get
        Set(ByVal value As String)
            _strType = value
        End Set
    End Property





    Public DataCnt As Integer
    Public CaseDesp As New ArrayList
#End Region

#Region "MastePage Content Variables"

    Protected Sub GetMasterPageControls()
        GetContentPlaceHolder()
        GetErrorLable()
        GetLogOffbtn()
        GetUpdatebtn()
        GetClosebtn()
        GetMainHeadingdiv()
    End Sub

    Protected Sub GetErrorLable()
        ErrorLable = Page.Master.FindControl("lblError")
    End Sub

    Protected Sub GetLogOffbtn()
        LogOffbtn = Page.Master.FindControl("imgLogoff")
        LogOffbtn.Visible = False
    End Sub

    Protected Sub GetUpdatebtn()
        Updatebtn = Page.Master.FindControl("imgUpdate")
        Updatebtn.Visible = False
        AddHandler Updatebtn.Click, AddressOf Update_Click
    End Sub

    Protected Sub GetClosebtn()
        Closebtn = Page.Master.FindControl("imgClose")
        Closebtn.Visible = True
        AddHandler Closebtn.Click, AddressOf ReturnProfile_Click
        Closebtn.Attributes.Add("onclick", "return CheckAnnualSaleValue();")
    End Sub

    Protected Sub GetMainHeadingdiv()
        MainHeading = Page.Master.FindControl("divMainHeading")
        MainHeading.Attributes.Add("onmouseover", "Tip('User Intelligence ')")
        MainHeading.Attributes.Add("onmouseout", "UnTip()")
        MainHeading.InnerHtml = "Packaging Producer - User Intelligence  "
    End Sub

    Protected Sub GetContentPlaceHolder()
        ctlContentPlaceHolder = Page.Master.FindControl("PackProdContentPlaceHolder")
    End Sub



#End Region


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objCryptoHelper As New CryptoHelper()
        Try
            GetMasterPageControls()

            CompanyId = objCryptoHelper.Decrypt(Request.QueryString("Id"))
            CategoryId = objCryptoHelper.Decrypt(Request.QueryString("CatId"))
            hdncatg.Value = CategoryId
            Type = Request.QueryString("Type")
            If Not IsPostBack Then
                BindCategory(CategoryId)
            End If

        Catch ex As Exception
            ErrorLable.Text = "Error:Page_Load" + ex.Message.ToString()
        End Try
    End Sub


    Protected Sub BindCategory(ByVal CategoryId As String)
        Dim objGetData As New Selectdata()
        Dim ds As New DataSet()
        Dim lst As New ListItem
        Dim i As New Integer
        Try
            lst.Text = "none"
            lst.Value = "0"
            ddlCat.Items.Add(lst)
            ddlCat.AppendDataBoundItems = True
            ds = objGetData.GetUserCategories()
            For Each dr As DataRow In ds.Tables(0).Rows
                lst = New ListItem
                lst.Text = dr.Item("NAME").ToString()
                lst.Value = dr.Item("CATEGORYID").ToString() + ":" + dr.Item("ISDETAILS").ToString() + ":" + dr.Item("ISCOMMENTS").ToString() + ":" + dr.Item("CODE").ToString()
                ddlCat.Items.Add(lst)
                ddlCat.AppendDataBoundItems = True
                If CategoryId = dr.Item("CATEGORYID").ToString() Then
                    lst.Selected = True
                End If
            Next
            GetPageDetails()

        Catch ex As Exception
            ErrorLable.Text = "Error:BindCategory" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetPageDetails()
        Dim objGetData As New Selectdata
        Dim dsdet As New DataSet
        Dim dsdetSugg As New DataSet
        Dim dscommSugg As New DataSet
        Dim dscomm As New DataSet
        Dim aarValue() As String
        Dim CategoryId As Integer
        Dim UserId As String
        Dim IsComments As String
        Dim IsDetails As String
        Dim Code As String
        Dim i As Integer
        Try
            aarValue = ddlCat.SelectedValue.Split(":")
            chkDetails.Items.Clear()
            chkDetailsSugg.Items.Clear()
            CategoryId = aarValue(0)
            If CategoryId <> 0 Then
                UserId = Session("UserId")
                IsDetails = aarValue(1)
                IsComments = aarValue(2)
                Code = aarValue(3)

                If IsDetails = "Y" And IsComments = "Y" Then
                    dsdet = objGetData.GetUserCategoryDetails(CategoryId, CompanyId, UserId)
                    dscomm = objGetData.GetUserCategoryComments(CategoryId, CompanyId, UserId)
                    dsdetSugg = objGetData.GetProfileCategoryDetails2(CompanyId, CategoryId)
                    dscommSugg = objGetData.GetProfileCategoryComments(CompanyId, CategoryId)
                    lblSelect.Text = ddlCat.SelectedItem.Text
                    lblComment.Text = ddlCat.SelectedItem.Text + " Comments"
                    With chkDetails
                        .DataSource = dsdet
                        .DataTextField = "DETAILS"
                        .DataValueField = "ID"
                        .DataBind()
                    End With
                    With chkDetailsSugg
                        .DataSource = dsdetSugg
                        .DataTextField = "DATA"
                        .DataValueField = "ID"
                        .DataBind()
                    End With

                    i = 0
                    For Each lst As ListItem In chkDetailsSugg.Items
                        If dsdetSugg.Tables(0).Rows(i).Item("ISADDED").ToString() = "Y" Then
                            lst.Selected = True
                        End If
                        i = i + 1
                    Next

                    i = 0
                    For Each lst As ListItem In chkDetails.Items
                        If dsdet.Tables(0).Rows(i).Item("ISADDED").ToString() = "Y" Then
                            lst.Selected = True
                        End If
                        i = i + 1
                    Next


                    If dscomm.Tables(0).Rows.Count > 0 Then
                        txtComm.Text = dscomm.Tables(0).Rows(0).Item("COMMENTS").ToString()
                        txtComm.ValidationGroup = dscomm.Tables(0).Rows(0).Item("COMPANYCATEGORYCOMMID").ToString()
                    Else
                        txtComm.ValidationGroup = "0"
                    End If
                    If dscommSugg.Tables(0).Rows.Count > 0 Then
                        txtCommSugg.Text = dscommSugg.Tables(0).Rows(0).Item("COMMENTS").ToString()
                    End If
                    divCatDetails.Visible = True
                    divCatComments.Visible = True
                ElseIf IsDetails = "N" And IsComments = "Y" Then
                    dscomm = objGetData.GetUserCategoryComments(CategoryId, CompanyId, UserId)
                    dscommSugg = objGetData.GetProfileCategoryComments(CompanyId, CategoryId)
                    lblComment.Text = ddlCat.SelectedItem.Text + " Comments"
                    If dscomm.Tables(0).Rows.Count > 0 Then
                        txtComm.Text = dscomm.Tables(0).Rows(0).Item("COMMENTS").ToString()
                        txtComm.ValidationGroup = dscomm.Tables(0).Rows(0).Item("COMPANYCATEGORYCOMMID").ToString()
                    Else
                        txtComm.ValidationGroup = "0"
                    End If

                    If dscommSugg.Tables(0).Rows.Count > 0 Then
                        txtCommSugg.Text = dscommSugg.Tables(0).Rows(0).Item("COMMENTS").ToString()
                    End If
                    divCatDetails.Visible = False
                    divCatComments.Visible = True
                ElseIf IsDetails = "N" And IsComments = "N" Then
                    dscomm = objGetData.GetUserCategoryDetl(CategoryId, CompanyId, UserId)
                    dscommSugg = objGetData.GetProfileCategoryDetails(CompanyId, CategoryId)
                    lblComment.Text = ddlCat.SelectedItem.Text
                    If dscomm.Tables(0).Rows.Count > 0 Then
                        txtComm.Text = dscomm.Tables(0).Rows(0).Item("DETAILS").ToString()
                        txtComm.ValidationGroup = dscomm.Tables(0).Rows(0).Item("COMPANYCATEGORYID").ToString()
                    Else
                        txtComm.ValidationGroup = "0"
                    End If
                    If dscommSugg.Tables(0).Rows.Count > 0 Then
                        txtCommSugg.Text = dscommSugg.Tables(0).Rows(0).Item("DETAILS").ToString()
                    End If
                    divCatDetails.Visible = False
                    divCatComments.Visible = True
                End If
            Else
                divCatDetails.Visible = False
                divCatComments.Visible = False
            End If


            If Type = "C" Then
                divCatDetails.Visible = False
                divCatComments.Visible = True
            Else
                divCatDetails.Visible = True
                divCatComments.Visible = False
            End If


        Catch ex As Exception
            ErrorLable.Text = "Error:GetPageDetails" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub Update_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            UpdateData()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ReturnProfile_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            UpdateData()
            Response.Redirect("../Profile.aspx?Id=" + Request.QueryString("Id") + "", True)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub UpdateData()
        Dim CategoryId As Integer
        Dim UserId As String
        Dim IsComments As String
        Dim IsDetails As String
        Dim Code As String
        Dim aarValue() As String
        Dim DetailsId() As String
        Dim CatDetailId() As String
        Dim UserCatComDetailId() As String
        aarValue = ddlCat.SelectedValue.Split(":")
        CategoryId = aarValue(0)
        Dim objUpIns As New UpdateInsert()
        Dim i As New Integer
        Try
            If CategoryId <> 0 Then
                UserId = Session("UserId")
                IsDetails = aarValue(1)
                IsComments = aarValue(2)
                Code = aarValue(3)
                ReDim CatDetailId(chkDetails.Items.Count - 1)
                ReDim UserCatComDetailId(chkDetails.Items.Count - 1)
                i = 0
                If IsDetails = "Y" And IsComments = "Y" Then
                    For Each chk As ListItem In chkDetails.Items

                        If chk.Selected Then

                            DetailsId = chk.Value.Split(":")
                            CatDetailId(i) = DetailsId(0)
                            UserCatComDetailId(i) = DetailsId(1)
                        Else
                            CatDetailId(i) = 0
                        End If
                        i = i + 1
                    Next

                    objUpIns.InsUpdateUserCompanyCatDetails(CatDetailId, CategoryId, CompanyId, UserId)
                    objUpIns.InsUpdateUserCompanyCatComm(txtComm.ValidationGroup, CategoryId, CompanyId, UserId, txtComm.Text)
                ElseIf IsDetails = "N" And IsComments = "Y" Then
                    objUpIns.InsUpdateUserCompanyCatComm(txtComm.ValidationGroup, CategoryId, CompanyId, UserId, txtComm.Text)
                ElseIf IsDetails = "N" And IsComments = "N" Then
                    objUpIns.InsUpdateUserCompanyCatDetls(txtComm.ValidationGroup, CategoryId, CompanyId, UserId, txtComm.Text)
                End If
            End If
            objUpIns.UserRebulidIndex()
            txtComm.Text = ""
            GetPageDetails()

        Catch ex As Exception

        End Try
    End Sub


End Class
