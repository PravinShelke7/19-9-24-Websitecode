Imports System.Web.UI.HtmlTextWriter
Imports System.Data
Imports ContrGetData
Partial Class Pages_Contract_Profile
    Inherits System.Web.UI.Page
#Region "Get Set Variables"
    Dim _lErrorLble As Label
    Dim _divMainHeading As HtmlGenericControl
    Dim _ctlContentPlaceHolder As ContentPlaceHolder
    Dim _strCompanyId As String
    Dim _btnLogOff As ImageButton

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





    Public DataCnt As Integer
    Public CaseDesp As New ArrayList
#End Region

#Region "MastePage Content Variables"

    Protected Sub GetMasterPageControls()
        GetContentPlaceHolder()
        GetErrorLable()
        GetLogOffbtn()
        GetMainHeadingdiv()
    End Sub

    Protected Sub GetErrorLable()
        ErrorLable = Page.Master.FindControl("lblError")
    End Sub

    Protected Sub GetLogOffbtn()
        LogOffbtn = Page.Master.FindControl("imgLogoff")
        LogOffbtn.Visible = False
    End Sub

    Protected Sub GetMainHeadingdiv()
        MainHeading = Page.Master.FindControl("divMainHeading")
        MainHeading.Attributes.Add("onmouseover", "Tip('Keyword Search Results ')")
        MainHeading.Attributes.Add("onmouseout", "UnTip()")
        MainHeading.InnerHtml = "Contract - Keyword Search Results "
    End Sub

    Protected Sub GetContentPlaceHolder()
        ctlContentPlaceHolder = Page.Master.FindControl("Econ1ContentPlaceHolder")
    End Sub



#End Region


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objCryptoHelper As New CryptoHelper()
        Try
            GetMasterPageControls()
            CompanyId = objCryptoHelper.Decrypt(Request.QueryString("Id"))
            GetPageDetails()
        Catch ex As Exception
            ErrorLable.Text = "Error:Page_Load:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetPageDetails()
        Dim objGetdata As New Selectdata()
        Dim dsCat As New DataSet
        Dim dsComp As New DataSet
        Dim dsProfDet As New DataSet
        Dim dsProfComm As New DataSet
        Dim i As New Integer
        Dim j As New Integer
        Dim trHeader As New TableRow
        Dim trInner As New TableRow
        Dim tdHeader As TableCell
        Dim tdInner As TableCell
        Dim tdSInner As TableCell
        Dim trSInner As New TableRow
        Dim divDetails As New HtmlGenericControl
        Dim divComments As New HtmlGenericControl
        Dim objCryptoHelper As New CryptoHelper()
        Dim RowCnt As New Integer
        Dim comprof As String = String.Empty
        Dim hyp As New HyperLink
        Dim Curr As String = String.Empty
        Dim PageUrl As String = String.Empty
        Try
            dsCat = objGetdata.GetProfileCategories()
            dsComp = objGetdata.GetCompanyDetails(CompanyId)

            '(Session("UserName").ToString(), Session("Password").ToString(), "keyword")

            comprof = dsComp.Tables(0).Rows(0).Item("NAME").ToString()
            comprof = comprof + "<br />" + dsComp.Tables(0).Rows(0).Item("ADDRESS").ToString()
            If dsComp.Tables(0).Rows(0).Item("STATEID").ToString() = "0" Then
                comprof = comprof + "<br />" + dsComp.Tables(0).Rows(0).Item("CITY").ToString() + ", " + dsComp.Tables(0).Rows(0).Item("ZIPCODE").ToString()
            Else
                comprof = comprof + "<br />" + dsComp.Tables(0).Rows(0).Item("CITY").ToString() + ", " + dsComp.Tables(0).Rows(0).Item("STATE").ToString() + " " + dsComp.Tables(0).Rows(0).Item("ZIPCODE").ToString()
            End If

            comprof = comprof + "<br />" + dsComp.Tables(0).Rows(0).Item("COUNTRY").ToString()
            comprof = comprof + "<br />" + dsComp.Tables(0).Rows(0).Item("PHONENUMBER").ToString()
            If dsComp.Tables(0).Rows(0).Item("WEBADDRESS").ToString().Trim() <> "" Then
                comprof = comprof + "<br /><a href='http://" + dsComp.Tables(0).Rows(0).Item("WEBADDRESS").ToString() + "'>" + dsComp.Tables(0).Rows(0).Item("WEBADDRESS").ToString() + "</a>"
            End If
            divCompanyProf.InnerHtml = comprof
            Curr = dsComp.Tables(0).Rows(0).Item("CURRENCYNAME").ToString()

            trHeader = New TableRow
            For i = 1 To 3
                tdHeader = New TableCell
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "24%", "Type", "1")
                        trHeader.Controls.Add(tdHeader)
                    Case 2
                        HeaderTdSetting(tdHeader, "38%", "SavvyPack Corporation Intelligence", "1")
                        trHeader.Controls.Add(tdHeader)
                        tdHeader.Attributes.Add("onmouseover", "Tip('Public intelligence available to all users ')")
                        tdHeader.Attributes.Add("onmouseout", "UnTip()")
                    Case 3
                        HeaderTdSetting(tdHeader, "38%", "User Intelligence", "1")
                        trHeader.Controls.Add(tdHeader)
                        tdHeader.Attributes.Add("onmouseover", "Tip('Proprietary intelligence available ONLY to user that submits the information ')")
                        tdHeader.Attributes.Add("onmouseout", "UnTip()")

                End Select
            Next
            tblComparision.Controls.Add(trHeader)

            For i = 0 To dsCat.Tables(0).Rows.Count - 1
                trInner = New TableRow
                trSInner = New TableRow()
                For j = 1 To 3
                    tdInner = New TableCell
                    tdSInner = New TableCell()

                    Select Case j

                        Case 1
                            InnerTdSetting(tdInner, "", "Left")
                            tdInner.Text = "<b>" + dsCat.Tables(0).Rows(i).Item("NAME").ToString() + "</b>"
                            trInner.Controls.Add(tdInner)
                            tdInner.RowSpan = 2

                        Case 2

                            divDetails = New HtmlGenericControl()
                            divComments = New HtmlGenericControl()

                            InnerTdSetting(tdInner, "", "Left")
                            InnerTdSetting(tdSInner, "", "Left")
                            If dsCat.Tables(0).Rows(i).Item("ISDETAILS").ToString() = "N" And dsCat.Tables(0).Rows(i).Item("ISCOMMENTS").ToString() = "N" Then
                                dsProfDet = objGetdata.GetProfileCategoryDetails(CompanyId, dsCat.Tables(0).Rows(i).Item("CATEGORYID").ToString())
                                If dsCat.Tables(0).Rows(i).Item("CODE").ToString() = "ASALE" Then
                                    If dsProfDet.Tables(0).Rows(0).Item("DETAILS").ToString() <> "" Then
                                        divDetails.InnerHtml = Curr + "&nbsp;&nbsp;" + FormatNumber(dsProfDet.Tables(0).Rows(0).Item("DETAILS").ToString(), 0) + ""
                                    End If

                                Else
                                    divDetails.InnerHtml = dsProfDet.Tables(0).Rows(0).Item("DETAILS").ToString() + ""
                                End If
                                tdInner.Controls.Add(divDetails)
                                trInner.Controls.Add(tdInner)
                                tdInner.RowSpan = 2



                            End If

                            If dsCat.Tables(0).Rows(i).Item("ISDETAILS").ToString() = "Y" And dsCat.Tables(0).Rows(i).Item("ISCOMMENTS").ToString() = "Y" Then

                                dsProfDet = objGetdata.GetProfileCategoryDetComm(CompanyId, dsCat.Tables(0).Rows(i).Item("CATEGORYID").ToString())
                                dsProfComm = objGetdata.GetProfileCategoryComments(CompanyId, dsCat.Tables(0).Rows(i).Item("CATEGORYID").ToString())
                                If dsProfDet.Tables(0).Rows.Count > 0 Then
                                    divDetails.InnerHtml = "<br/>" + dsProfDet.Tables(0).Rows(0).Item("DETAILS").ToString() + "<br/><br/>"
                                End If
                                divComments.InnerHtml = "<b>Comments: </b>" + dsProfComm.Tables(0).Rows(0).Item("COMMENTS").ToString() + "<br/><br/>"

                                tdInner.Controls.Add(divDetails)
                                tdSInner.Controls.Add(divComments)
                                trInner.Controls.Add(tdInner)
                                trSInner.Controls.Add(tdSInner)
                                tdInner.VerticalAlign = VerticalAlign.Top
                                tdSInner.VerticalAlign = VerticalAlign.Top

                            End If

                            If dsCat.Tables(0).Rows(i).Item("ISDETAILS").ToString() = "N" And dsCat.Tables(0).Rows(i).Item("ISCOMMENTS").ToString() = "Y" Then
                                dsProfDet = objGetdata.GetProfileCategoryComments(CompanyId, dsCat.Tables(0).Rows(i).Item("CATEGORYID").ToString())
                                divDetails.InnerHtml = "<br/>" + dsProfDet.Tables(0).Rows(0).Item("COMMENTS").ToString() + "<br/><br/>"
                                tdInner.Controls.Add(divDetails)
                                trInner.Controls.Add(tdInner)
                                tdInner.RowSpan = 2
                                tdSInner.RowSpan = 2
                            End If

                           

                        Case 3
                            divDetails = New HtmlGenericControl()
                            divComments = New HtmlGenericControl()
                            InnerTdSetting(tdInner, "", "Left")
                            InnerTdSetting(tdSInner, "", "Left")
                            If dsCat.Tables(0).Rows(i).Item("ISDETAILS").ToString() = "N" And dsCat.Tables(0).Rows(i).Item("ISCOMMENTS").ToString() = "N" Then
                                dsProfDet = objGetdata.GetUserCategoryDetl(dsCat.Tables(0).Rows(i).Item("CATEGORYID").ToString(), CompanyId, Session("UserId"))
                                If dsProfDet.Tables(0).Rows.Count > 0 Then
                                    If dsCat.Tables(0).Rows(i).Item("CODE").ToString() = "ASALE" Then
                                        'divDetails.InnerHtml = Curr + FormatNumber(dsProfDet.Tables(0).Rows(0).Item("DETAILS").ToString(), 0) + ""
                                        If dsProfDet.Tables(0).Rows(0).Item("DETAILS").ToString() <> "" Then
                                            divDetails.InnerHtml = Curr + "&nbsp;&nbsp;" + FormatNumber(dsProfDet.Tables(0).Rows(0).Item("DETAILS").ToString(), 0) + ""
                                        End If
                                    Else
                                        divDetails.InnerHtml = dsProfDet.Tables(0).Rows(0).Item("DETAILS").ToString() + ""
                                    End If
                                End If
                                tdInner.Controls.Add(divDetails)
                                trInner.Controls.Add(tdInner)
                                tdInner.RowSpan = 2
                                PageUrl = "Users/UserPref.aspx?Id=" + Request.QueryString("Id") + "&CatId=" + objCryptoHelper.Encrypt(dsCat.Tables(0).Rows(i).Item("CATEGORYID").ToString()) + ""
                                tdInner.Attributes.Add("onclick", "UserPref('" + PageUrl + "&Type=C')")
                                tdInner.Style.Add("cursor", "hand")
                            End If
                            If dsCat.Tables(0).Rows(i).Item("ISDETAILS").ToString() = "Y" And dsCat.Tables(0).Rows(i).Item("ISCOMMENTS").ToString() = "Y" Then
                                dsProfComm = objGetdata.GetUserCategoryComments(dsCat.Tables(0).Rows(i).Item("CATEGORYID").ToString(), CompanyId, Session("UserId"))
                                dsProfDet = objGetdata.GetUserCategoryDetailsProfile(dsCat.Tables(0).Rows(i).Item("CATEGORYID").ToString(), CompanyId, Session("UserId"))
                                If dsProfDet.Tables(0).Rows.Count > 0 Then
                                    divDetails.InnerHtml = "<br/>" + dsProfDet.Tables(0).Rows(0).Item("DETAILS").ToString() + "<br/><br/>"
                                End If
                                If dsProfComm.Tables(0).Rows.Count > 0 Then
                                    divComments.InnerHtml = "<b>Comments: </b>" + dsProfComm.Tables(0).Rows(0).Item("COMMENTS").ToString() + "<br/><br/>"
                                End If

                                tdInner.Controls.Add(divDetails)
                                tdSInner.Controls.Add(divComments)
                                trInner.Controls.Add(tdInner)
                                trSInner.Controls.Add(tdSInner)
                                PageUrl = "Users/UserPref.aspx?Id=" + Request.QueryString("Id") + "&CatId=" + objCryptoHelper.Encrypt(dsCat.Tables(0).Rows(i).Item("CATEGORYID").ToString()) + ""
                                tdInner.Attributes.Add("onclick", "UserPref('" + PageUrl + "&Type=D')")
                                tdSInner.Attributes.Add("onclick", "UserPref('" + PageUrl + "&Type=C')")
                                tdInner.Style.Add("cursor", "hand")
                                tdSInner.Style.Add("cursor", "hand")
                                tdInner.VerticalAlign = VerticalAlign.Top
                                tdSInner.VerticalAlign = VerticalAlign.Top
                            End If
                            If dsCat.Tables(0).Rows(i).Item("ISDETAILS").ToString() = "N" And dsCat.Tables(0).Rows(i).Item("ISCOMMENTS").ToString() = "Y" Then
                                dsProfComm = objGetdata.GetUserCategoryComments(dsCat.Tables(0).Rows(i).Item("CATEGORYID").ToString(), CompanyId, Session("UserId"))
                                If dsProfComm.Tables(0).Rows.Count > 0 Then
                                    divComments.InnerHtml = "<br/>" + dsProfComm.Tables(0).Rows(0).Item("COMMENTS").ToString() + "<br/><br/>"
                                End If
                                tdInner.Controls.Add(divComments)
                                trInner.Controls.Add(tdInner)
                                tdInner.RowSpan = 2
                                PageUrl = "Users/UserPref.aspx?Id=" + Request.QueryString("Id") + "&CatId=" + objCryptoHelper.Encrypt(dsCat.Tables(0).Rows(i).Item("CATEGORYID").ToString()) + ""
                                tdInner.Attributes.Add("onclick", "UserPref('" + PageUrl + "&Type=C')")
                                tdInner.Style.Add("cursor", "hand")
                            End If
                           
                    End Select



                Next
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                    trSInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                    trSInner.CssClass = "AlterNateColor2"
                End If
                tblComparision.Controls.Add(trInner)
                tblComparision.Controls.Add(trSInner)
                tblComparision.Width = 820
            Next
        Catch ex As Exception
            ErrorLable.Text = "Error:GetPageDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub HeaderTdSetting(ByVal Td As TableCell, ByVal Width As String, ByVal HeaderText As String, ByVal ColSpan As String)
        Try
            Td.Text = HeaderText
            Td.ColumnSpan = ColSpan
            If Width <> "" Then
                Td.Style.Add("width", Width)
            End If
            Td.CssClass = "TdHeading"
            Td.Height = 20
            Td.Font.Size = 10
            Td.Font.Bold = True
            Td.HorizontalAlign = HorizontalAlign.Center



        Catch ex As Exception
            _lErrorLble.Text = "Error:HeaderTdSetting:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub Header2TdSetting(ByVal Td As TableCell, ByVal Width As String, ByVal HeaderText As String, ByVal ColSpan As String)
        Try
            Td.Text = HeaderText
            Td.ColumnSpan = ColSpan
            If Width <> "" Then
                Td.Style.Add("width", Width)
            End If
            Td.CssClass = "TdHeading"
            Td.Font.Size = 8
            Td.Height = 20
            Td.HorizontalAlign = HorizontalAlign.Center



        Catch ex As Exception
            _lErrorLble.Text = "Error:HeaderTdSetting:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub InnerTdSetting(ByVal Td As TableCell, ByVal Width As String, ByVal Align As String)
        Try

            If Width <> "" Then
                Td.Style.Add("width", Width)
            End If
            Td.Style.Add("text-align", Align)

            Td.Style.Add("padding-left", "5px")
            Td.Style.Add("padding-right", "5px")
            Td.Style.Add("padding-right", "2px")

        Catch ex As Exception
            _lErrorLble.Text = "Error:InnerTdSetting:" + ex.Message.ToString()
        End Try
    End Sub

End Class
