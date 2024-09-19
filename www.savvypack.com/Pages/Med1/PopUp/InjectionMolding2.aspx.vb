Imports System.Data
Imports System.Data.OleDb
Imports System
Imports Med1GetData
Imports Med1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_MedEcon1_PopUp_InjectionMolding2
    Inherits System.Web.UI.Page
    Public CaseDes As String = String.Empty
    Public CycleTm As String
    Shared objCalc As New InjectionCalc.Calculation

#Region "Get Set Variables"
    Dim _lErrorLble As Label
    Dim _iCaseId As Integer
    Dim _iUserId As Integer
    Dim _strUserRole As String
    Dim _ctlContentPlaceHolder As ContentPlaceHolder
    Dim _btnUpdate As ImageButton
    Dim _divMainHeading As HtmlGenericControl
    Dim _hypPref As HyperLink
    Public Property MainHeading() As HtmlGenericControl
        Get
            Return _divMainHeading
        End Get
        Set(ByVal value As HtmlGenericControl)
            _divMainHeading = value
        End Set
    End Property
    Public Property PrefLink() As HyperLink
        Get
            Return _hypPref
        End Get
        Set(ByVal value As HyperLink)
            _hypPref = value
        End Set
    End Property

    Public Property ErrorLable() As Label
        Get
            Return _lErrorLble
        End Get
        Set(ByVal Value As Label)
            _lErrorLble = Value
        End Set
    End Property

    Public Property CaseId() As Integer
        Get
            Return _iCaseId
        End Get
        Set(ByVal Value As Integer)
            _iCaseId = Value
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

    Public Property UserRole() As String
        Get
            Return _strUserRole
        End Get
        Set(ByVal Value As String)
            _strUserRole = Value
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
    Public Property Updatebtn() As ImageButton
        Get
            Return _btnUpdate
        End Get
        Set(ByVal value As ImageButton)
            _btnUpdate = value
        End Set
    End Property

    Public DataCnt As Integer
    Public CaseDesp As New ArrayList
#End Region

#Region "MastePage Content Variables"

    Protected Sub GetMasterPageControls()
        GetErrorLable()
        GetContentPlaceHolder()
        GetUpdatebtn()
        GetMainHeadingdiv()
        GetPrefLink()
    End Sub

    Protected Sub GetErrorLable()
        ErrorLable = Page.Master.FindControl("lblError")
    End Sub
    Protected Sub GetPrefLink()
        PrefLink = Page.Master.FindControl("hypPref")

        PrefLink.NavigateUrl = "../Pages/Med1/Assumptions/Preferences.aspx"
        PrefLink.Visible = True
    End Sub
    Protected Sub GetContentPlaceHolder()
        ctlContentPlaceHolder = Page.Master.FindControl("Med1InkContentPlaceHolder")
    End Sub
    Protected Sub GetUpdatebtn()
        Updatebtn = Page.Master.FindControl("imgUpdate")
        Updatebtn.Visible = True
        Updatebtn.Attributes.Add("onclick", "return checkNumericAll();")
        'Updatebtn.Attributes.Add("onclick", "return CheckForMaterialPage('" + ctlContentPlaceHolder.ClientID + "','hidColId','T','hidDepid','hypDep');")
        AddHandler Updatebtn.Click, AddressOf Update_Click
    End Sub
    Protected Sub GetMainHeadingdiv()
        MainHeading = Page.Master.FindControl("divMainHeading")
        MainHeading.Attributes.Add("onmouseover", "Tip('Injection Molding Assistant')")
        MainHeading.Attributes.Add("onmouseout", "UnTip()")
        MainHeading.InnerHtml = "Med1 - Injection Molding Assistant"

    End Sub
#End Region

#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_PAGES_MEDECON1_POPUP_INJECTIONMOLDING2")
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        objRefresh.Render(Page)
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            GetMasterPageControls()
            GetSessionDetails()

            If Not IsPostBack Then
                ViewState("TabId") = 1
                ViewState("UnitId1") = Session("UnitId")
                GetPageDetails()
            End If
            CreateTab()

        Catch ex As Exception
            ErrorLable.Text = "Error:Page_Load:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub CreateTab()
        Dim tr As New TableRow
        Dim td As New TableCell
        Dim i As New Integer
        Dim lnk As New LinkButton
        Try
            tblTab.Rows.Clear()
            For i = 1 To 5
                td = New TableCell
                lnk = New LinkButton
                If i = 1 Then
                    lnk.Text = "Part Assumptions "
                ElseIf i = 2 Then
                    lnk.Text = "Mold Assumptions "
                ElseIf i = 3 Then
                    lnk.Text = "Process Assumptions "
                ElseIf i = 4 Then
                    lnk.Text = "Results "
                Else
                    lnk.Text = "Energy"
                End If

                lnk.CssClass = "TabLink"
                lnk.ID = "lnkInject" + i.ToString()
                ' If ViewState("TabId").ToString() = "1" Then
                'lnk.Attributes.Add("onclick", "return CheckForPalletIn('" + ctlContentPlaceHolder.ClientID + "','hidMatId','PWT','MWT')")
                '  ElseIf ViewState("TabId").ToString() = "2" Then
                ' lnk.Attributes.Add("onclick", "return checkNumericAll();")
                ' End If

                AddHandler lnk.Click, AddressOf lnkInject_Click
                If i = Convert.ToInt32(ViewState("TabId").ToString()) Then
                    td.CssClass = "AlterNateTab3"
                Else
                    td.CssClass = "AlterNateTab4"
                End If
                td.Controls.Add(lnk)
                tr.Controls.Add(td)
            Next
            tblTab.Controls.Add(tr)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkInject_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If ViewState("UnitId1") <> Session("UnitId") Then
                'If Not Session("Post") Is Nothing And Session("Post") <> Session("Prev") Then
                GetPageDetails1()
                ScriptManager.RegisterStartupScript(Page, Me.GetType(), "JSScript", "alert('There has been done changes in Preference page,Please close this page and reopen again.');", True)
                Exit Sub
            End If
            Dim lnk As LinkButton = DirectCast(sender, System.Web.UI.WebControls.LinkButton)
            UpdatePage()
            ViewState("TabId") = Convert.ToInt32(lnk.ID.Replace("lnkInject", "").ToString())
            ' If Not IsNothing(ViewState("TabId")) Then
            'If ViewState("TabId").ToString() = "1" Then
            '  'Updatebtn.Attributes.Add("onclick", "return CheckForPalletIn('" + ctlContentPlaceHolder.ClientID + "','hidMatId','PWT','MWT')")
            ' ElseIf ViewState("TabId").ToString() = "2" Then
            ' Updatebtn.Attributes.Add("onclick", "return checkNumericAll();")
            '  End If
            ' End If
            CreateTab()

            GetPageDetails()
        Catch ex As Exception
            Response.Write("lnkPallet_Click" + ex.Message)
        End Try
    End Sub

    Protected Sub GetSessionDetails()
        Try
            UserId = Session("UserId")
            CaseId = Session("Med1CaseId")
            UserRole = Session("Med1UserRole")
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetSessionDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetPageDetails()
        Dim ds As New DataSet
        Dim objGetData As New Med1GetData.Selectdata
        Try

            objCalc.InjectionMoldCalculate(CaseId)
            ds = objGetData.GetInjectionDetails(CaseId)
            If ViewState("TabId") = "1" Then
                GetPageDetailsForTab1(ds, objCalc)
            ElseIf ViewState("TabId") = "2" Then
                GetPageDetailsForTab2(ds, objCalc)
            ElseIf ViewState("TabId") = "3" Then
                GetPageDetailsForTab3(ds, objCalc)
            ElseIf ViewState("TabId") = "4" Then
                GetPageDetailsForTab4(ds, objCalc)
            ElseIf ViewState("TabId") = "5" Then
                GetPageDetailsForTab5(ds, objCalc)
            End If

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub GetPageDetails1()
        Dim ds As New DataSet
        Dim objGetData As New Med1GetData.Selectdata
        Try

            'objCalc.InjectionMoldCalculate(CaseId)
            ds = objGetData.GetInjectionDetails(CaseId)
            If ViewState("TabId") = "1" Then
                GetPageDetailsForTab1(ds, objCalc)
            ElseIf ViewState("TabId") = "2" Then
                GetPageDetailsForTab2(ds, objCalc)
            ElseIf ViewState("TabId") = "3" Then
                GetPageDetailsForTab3(ds, objCalc)
            ElseIf ViewState("TabId") = "4" Then
                GetPageDetailsForTab4(ds, objCalc)
            ElseIf ViewState("TabId") = "5" Then
                GetPageDetailsForTab5(ds, objCalc)

            End If

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetPageDetailsForTab1(ByVal ds As DataSet, ByVal objCalc As InjectionCalc.Calculation)

        Dim i As New Integer
        Dim j As New Integer
        Dim DWidth As String = String.Empty
        Dim trHeader As New TableRow
        Dim trHeader1 As New TableRow
        Dim trHeader2 As New TableRow
        Dim trInner As New TableRow
        Dim tdHeader As TableCell
        Dim tdHeader1 As TableCell
        Dim tdHeader2 As TableCell
        Dim lbl As New Label
        Dim hid As New HiddenField
        Dim Link As New HyperLink
        Dim txt As New TextBox
        Dim tdInner As TableCell
        Dim chk As New CheckBox
        Dim rad1 As New RadioButton
        Dim rad2 As New RadioButton

        Try

            For i = 1 To 9
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "10px", "No.", "1")
                        Header2TdSetting(tdHeader2, "0", "", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 2
                        HeaderTdSetting(tdHeader, "160px", "Material", "1")
                        Header2TdSetting(tdHeader2, "0", "", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 3
                        Title = ds.Tables(0).Rows(0).Item("TITLE8")
                        HeaderTdSetting(tdHeader, "160px", "Part Weight", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        Header2TdSetting(tdHeader1, "0px", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 4
                        Title = ds.Tables(0).Rows(0).Item("TITLE14")
                        HeaderTdSetting(tdHeader, "60px", "Part Volume", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        Header2TdSetting(tdHeader1, "0px", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 5
                        Title = ds.Tables(0).Rows(0).Item("TITLE9")
                        HeaderTdSetting(tdHeader, "160px", "Max Wall Thickness", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        Header2TdSetting(tdHeader1, "0px", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)



                    Case 6
                        Title = ds.Tables(0).Rows(0).Item("TITLE15")
                        HeaderTdSetting(tdHeader, "160px", "Surface Area (across mold line)", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        Header2TdSetting(tdHeader1, "0px", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)

                    Case 7
                        Title = ds.Tables(0).Rows(0).Item("TITLE15")
                        HeaderTdSetting(tdHeader, "170px", "Total Part Surface Area", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        Header2TdSetting(tdHeader1, "0px", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)

                    Case 8
                        Title = ds.Tables(0).Rows(0).Item("TITLE15")
                        HeaderTdSetting(tdHeader, "170px", "Surface Area (sprue,runner,gate)", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        Header2TdSetting(tdHeader1, "0px", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)

                    Case 9
                        Title = ds.Tables(0).Rows(0).Item("TITLE15")
                        HeaderTdSetting(tdHeader, "170px", "Total Surface Area", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        Header2TdSetting(tdHeader1, "0px", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)


                End Select


            Next

            ' trHeader.Height = 30

            tblComparision.Controls.Add(trHeader)
            tblComparision.Controls.Add(trHeader2)
            tblComparision.Controls.Add(trHeader1)

            'Inner
            For i = 1 To 10
                trInner = New TableRow
                For j = 1 To 9
                    tdInner = New TableCell
                    Select Case j
                        Case 1

                            InnerTdSetting(tdInner, "", "Center")
                            tdInner.Text = "<b>" + i.ToString() + "</b>"
                            trInner.Controls.Add(tdInner)
                            'tblComparisionFix.Controls.Add(trInner)
                        Case 2
                            InnerTdSetting(tdInner, "", "Left")
                            Link = New HyperLink
                            hid = New HiddenField
                            Link.ID = "hypDesDeptA" + i.ToString()
                            hid.ID = "hidMatId" + i.ToString()
                            Link.Width = 130
                            Link.CssClass = "Link"
                            GetMaterialDetails(Link, hid, CInt(ds.Tables(0).Rows(0).Item("MATID" + i.ToString() + "")), Nothing)
                            tdInner.Controls.Add(hid)
                            tdInner.Controls.Add(Link)
                            trInner.Controls.Add(tdInner)

                        Case 3
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.Width = 70
                            txt.ID = "PWT" + i.ToString()
                            If (ds.Tables(0).Rows(0).Item("pwt" + i.ToString() + "").ToString() <> "") Then
                                txt.Text = FormatNumber(objCalc.PWT(i - 1) * CDbl(ds.Tables(0).Rows(0).Item("CONVWT").ToString()), 4)
                            End If
                            txt.MaxLength = 12
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 4
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Width = 100
                            lbl.Text = FormatNumber(objCalc.partvol(i - 1) * CDbl(ds.Tables(0).Rows(0).Item("CONVVOL").ToString()), 4)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 5
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.Width = 70
                            txt.ID = "MWT" + i.ToString()
                            If (ds.Tables(0).Rows(0).Item("MWT" + i.ToString() + "").ToString() <> "") Then
                                txt.Text = FormatNumber(objCalc.Mwt(i - 1) * CDbl(ds.Tables(0).Rows(0).Item("CONVTHICK").ToString()), 4)
                            End If
                            txt.MaxLength = 12
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)

                        Case 6
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.Width = 70
                            txt.ID = "SA" + i.ToString()
                            If (ds.Tables(0).Rows(0).Item("SA" + i.ToString() + "").ToString() <> "") Then
                                txt.Text = FormatNumber(objCalc.SA(i - 1) * CDbl(ds.Tables(0).Rows(0).Item("CONVAREA3").ToString()), 4)
                            End If
                            txt.MaxLength = 12
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)

                        Case 7
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Text = FormatNumber(objCalc.totpsa(i - 1) * CDbl(ds.Tables(0).Rows(0).Item("CONVAREA3").ToString()), 4)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 8
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Text = FormatNumber(objCalc.sarea(i - 1) * CDbl(ds.Tables(0).Rows(0).Item("CONVAREA3").ToString()), 4)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 9
                            InnerTdSetting(tdInner, "", "RIght")
                            lbl = New Label
                            lbl.Text = FormatNumber(objCalc.totsarea(i - 1) * CDbl(ds.Tables(0).Rows(0).Item("CONVAREA3").ToString()), 4)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)

                    End Select
                Next
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
                trInner.Height = 30

                tblComparision.Controls.Add(trInner)
            Next

            'Total
            trInner = New TableRow
            For i = 1 To 9
                tdInner = New TableCell
                Select Case i
                    Case 1
                        'Department
                        InnerTdSetting(tdInner, "", "Center")
                        tdInner.Text = "<b> Total </b>"
                        trInner.Controls.Add(tdInner)
                        'tblComparisionFix.Controls.Add(trInner)
                    Case 2
                        InnerTdSetting(tdInner, "", "Left")
                        Link = New HyperLink
                        hid = New HiddenField
                        Link.ID = "hypDesDeptA" + i.ToString()
                        hid.ID = "hidIdDeptA" + i.ToString()
                        Link.Width = 130
                        Link.CssClass = "Link"
                        'GetDeptDetails(Link, hid, CInt(ds.Tables(0).Rows(0).Item("DEPTA" + i.ToString() + "").ToString()))
                        tdInner.Controls.Add(hid)
                        tdInner.Controls.Add(Link)
                        trInner.Controls.Add(tdInner)

                    Case 3, 5, 6
                        Dim Str As String = Chr(63 + i)
                        InnerTdSetting(tdInner, "", "Left")
                        Link = New HyperLink
                        hid = New HiddenField
                        Link.ID = "hypDesDept" + Chr(63 + j) + "" + i.ToString()
                        hid.ID = "hidIdDept" + Chr(63 + j) + "" + i.ToString()
                        Link.Width = 130
                        Link.CssClass = "Link"
                        'GetDeptDetails(Link, hid, CInt(ds.Tables(0).Rows(0).Item("DEPT" + Chr(63 + j) + "" + i.ToString() + "").ToString()))
                        tdInner.Controls.Add(hid)
                        tdInner.Controls.Add(Link)
                        trInner.Controls.Add(tdInner)
                    Case 4
                        InnerTdSetting(tdInner, "", "Right")
                        lbl = New Label
                        lbl.Text = FormatNumber(objCalc.total(0) * CDbl(ds.Tables(0).Rows(0).Item("CONVVOL").ToString()), 4)
                        tdInner.Controls.Add(lbl)
                        trInner.Controls.Add(tdInner)
                    Case 7
                        InnerTdSetting(tdInner, "", "Right")
                        lbl = New Label
                        lbl.Text = FormatNumber(objCalc.total(1) * CDbl(ds.Tables(0).Rows(0).Item("CONVAREA3").ToString()), 4)
                        tdInner.Controls.Add(lbl)
                        trInner.Controls.Add(tdInner)
                    Case 8
                        InnerTdSetting(tdInner, "", "Right")
                        lbl = New Label
                        lbl.Text = FormatNumber(objCalc.total(2) * CDbl(ds.Tables(0).Rows(0).Item("CONVAREA3").ToString()), 4)
                        tdInner.Controls.Add(lbl)
                        trInner.Controls.Add(tdInner)
                    Case 9
                        InnerTdSetting(tdInner, "", "Right")
                        lbl = New Label
                        lbl.Text = FormatNumber(objCalc.total(3) * CDbl(ds.Tables(0).Rows(0).Item("CONVAREA3").ToString()), 4)
                        tdInner.Controls.Add(lbl)
                        trInner.Controls.Add(tdInner)
                End Select
            Next
            trInner.Height = 30
            trInner.CssClass = "AlterNateColor2"
            tblComparision.Controls.Add(trInner)

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetailsForTab1:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetPageDetailsForTab2(ByVal ds As DataSet, ByVal objCalc As InjectionCalc.Calculation)

        Dim i As New Integer
        Dim j As New Integer
        Dim DWidth As String = String.Empty
        Dim trHeader As New TableRow
        Dim trHeader1 As New TableRow
        Dim trHeader2 As New TableRow
        Dim trInner As New TableRow
        Dim tdHeader As TableCell
        Dim tdHeader1 As TableCell
        Dim tdHeader2 As TableCell
        Dim lbl As New Label
        Dim hid As New HiddenField
        Dim Link As New HyperLink
        Dim txt As New TextBox
        Dim tdInner As TableCell
        Dim chk As New CheckBox
        Dim rad1 As New RadioButton
        Dim rad2 As New RadioButton

        Try
            trHeader.Height = 30
            For i = 1 To 7
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                Dim Title As String = String.Empty

                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "10px", "No.", "1")
                        Header2TdSetting(tdHeader2, "0", "", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 2
                        HeaderTdSetting(tdHeader, "140px", "Material", "1")
                        Header2TdSetting(tdHeader2, "0", "", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 3
                        HeaderTdSetting(tdHeader, "140px", "Stack Mold", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        Header2TdSetting(tdHeader1, "0px", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)

                    Case 4
                        HeaderTdSetting(tdHeader, "140px", "Runner System ", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        Header2TdSetting(tdHeader1, "0px", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 5
                        HeaderTdSetting(tdHeader, "170px", "Levels", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        Header2TdSetting(tdHeader1, "0px", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)

                    Case 6
                        HeaderTdSetting(tdHeader, "170px", "Cavitation Per Level", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        Header2TdSetting(tdHeader1, "0px", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)

                    Case 7
                        HeaderTdSetting(tdHeader, "120px", "Total Cavitation", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        Header2TdSetting(tdHeader1, "0px", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)


                End Select


            Next



            tblComparision.Controls.Add(trHeader)
            tblComparision.Controls.Add(trHeader2)
            tblComparision.Controls.Add(trHeader1)

            'Inner
            For i = 1 To 10
                trInner = New TableRow
                For j = 1 To 7
                    tdInner = New TableCell
                    Select Case j
                        Case 1

                            InnerTdSetting(tdInner, "", "Center")
                            tdInner.Text = "<b>" + i.ToString() + "</b>"
                            trInner.Controls.Add(tdInner)
                            'tblComparisionFix.Controls.Add(trInner)
                        Case 2
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            lbl.Width = 140
                            GetMaterialDetails(Nothing, Nothing, CInt(ds.Tables(0).Rows(0).Item("MATID" + i.ToString() + "")), lbl)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 3
                            InnerTdSetting(tdInner, "", "Center")
                            chk = New CheckBox()
                            chk.ID = "ISM" + i.ToString()
                            If CInt(ds.Tables(0).Rows(0).Item("SMOLD" + i.ToString() + "").ToString()) = 1 Then
                                chk.Checked = True
                            Else
                                chk.Checked = False
                            End If
                            tdInner.Controls.Add(chk)
                            trInner.Controls.Add(tdInner)
                        Case 4
                            InnerTdSetting(tdInner, "", "Center")
                            rad1 = New RadioButton()
                            rad2 = New RadioButton()
                            rad1.GroupName = "RunnerSys" + i.ToString() + ""
                            rad2.GroupName = "RunnerSys" + i.ToString() + ""
                            rad1.ID = "IC" + i.ToString()
                            rad2.ID = "IH" + i.ToString()
                            rad1.Text = "Hot " + ""
                            rad2.Text = "Cold"
                            If CInt(ds.Tables(0).Rows(0).Item("RunSys" + i.ToString() + "")) = 1 Then
                                rad1.Checked = True

                            ElseIf CInt(ds.Tables(0).Rows(0).Item("RunSys" + i.ToString() + "")) = 2 Then
                                rad2.Checked = True
                            Else
                                rad1.Checked = False
                                rad2.Checked = False
                            End If
                            tdInner.Controls.Add(rad1)
                            tdInner.Controls.Add(rad2)
                            trInner.Controls.Add(tdInner)

                        Case 5
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.Width = 70
                            txt.ID = "LVLS" + i.ToString()
                            If (ds.Tables(0).Rows(0).Item("LVLS" + i.ToString() + "").ToString() <> "") Then
                                txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("LVLS" + i.ToString() + "").ToString(), 3)
                            End If
                            txt.MaxLength = 12
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 6
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.Width = 70
                            txt.ID = "CAVPL" + i.ToString()
                            If (ds.Tables(0).Rows(0).Item("CAVPL" + i.ToString() + "").ToString() <> "") Then
                                txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("CAVPL" + i.ToString() + "").ToString(), 3)
                            End If
                            txt.MaxLength = 12
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)

                        Case 7
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("TOTALCAV" + i.ToString() + "").ToString(), 3)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)


                    End Select
                Next
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
                trInner.Height = 30

                tblComparision.Controls.Add(trInner)
            Next

            'Total
            trInner = New TableRow
            For i = 1 To 7
                tdInner = New TableCell
                Select Case i
                    Case 1
                        'Department
                        InnerTdSetting(tdInner, "", "Center")
                        tdInner.Text = "<b> Total </b>"
                        trInner.Controls.Add(tdInner)
                        'tblComparisionFix.Controls.Add(trInner)
                    Case 2
                        InnerTdSetting(tdInner, "", "Left")
                        Link = New HyperLink
                        hid = New HiddenField
                        Link.ID = "hypDesDeptA" + i.ToString()
                        hid.ID = "hidIdDeptA" + i.ToString()
                        Link.Width = 130
                        Link.CssClass = "Link"
                        'GetDeptDetails(Link, hid, CInt(ds.Tables(0).Rows(0).Item("DEPTA" + i.ToString() + "").ToString()))
                        tdInner.Controls.Add(hid)
                        tdInner.Controls.Add(Link)
                        trInner.Controls.Add(tdInner)

                    Case 3, 4, 5, 6
                        Dim Str As String = Chr(63 + i)
                        InnerTdSetting(tdInner, "", "Left")
                        Link = New HyperLink
                        hid = New HiddenField
                        Link.ID = "hypDesDept" + Chr(63 + j) + "" + i.ToString()
                        hid.ID = "hidIdDept" + Chr(63 + j) + "" + i.ToString()
                        Link.Width = 130
                        Link.CssClass = "Link"
                        'GetDeptDetails(Link, hid, CInt(ds.Tables(0).Rows(0).Item("DEPT" + Chr(63 + j) + "" + i.ToString() + "").ToString()))
                        tdInner.Controls.Add(hid)
                        tdInner.Controls.Add(Link)
                        trInner.Controls.Add(tdInner)
                    Case 7
                        InnerTdSetting(tdInner, "", "Right")
                        lbl = New Label
                        lbl.Text = FormatNumber(objCalc.total(4), 3)
                        tdInner.Controls.Add(lbl)
                        trInner.Controls.Add(tdInner)


                End Select
            Next
            trInner.Height = 30
            trInner.CssClass = "AlterNateColor2"
            tblComparision.Controls.Add(trInner)

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetailsForTab1:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetPageDetailsForTab3(ByVal ds As DataSet, ByVal objCalc As InjectionCalc.Calculation)

        Dim i As New Integer
        Dim j As New Integer
        Dim DWidth As String = String.Empty
        Dim trHeader As New TableRow
        Dim trHeader1 As New TableRow
        Dim trHeader2 As New TableRow
        Dim trInner As New TableRow
        Dim tdHeader As TableCell
        Dim tdHeader1 As TableCell
        Dim tdHeader2 As TableCell
        Dim lbl As New Label
        Dim hid As New HiddenField
        Dim Link As New HyperLink
        Dim txt As New TextBox
        Dim tdInner As TableCell



        Try
            trHeader.Height = 30

            For i = 1 To 16
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i

                    Case 1
                        HeaderTdSetting(tdHeader, "100px", "No.", "1")
                        Header2TdSetting(tdHeader2, "0", "", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 2
                        HeaderTdSetting(tdHeader, "140px", "Material", "1")
                        Header2TdSetting(tdHeader2, "0", "", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 3
                        Title = ds.Tables(0).Rows(0).Item("TITLE9")
                        HeaderTdSetting(tdHeader, "120px", "Screw Diameter", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        Header2TdSetting(tdHeader1, "0px", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 4
                        Title = ds.Tables(0).Rows(0).Item("TITLE14") + "/sec"
                        HeaderTdSetting(tdHeader, "140px", "Injection Rate", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        Header2TdSetting(tdHeader1, "0px", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 5
                        Title = ds.Tables(0).Rows(0).Item("TITLE13")
                        HeaderTdSetting(tdHeader, "160px", "Shot Weight", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        Header2TdSetting(tdHeader1, "0px", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 6
                        Title = ds.Tables(0).Rows(0).Item("TITLE14")
                        HeaderTdSetting(tdHeader, "160px", "Injection Volume", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        Header2TdSetting(tdHeader1, "0px", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 7
                        Title = ds.Tables(0).Rows(0).Item("TITLE14")
                        HeaderTdSetting(tdHeader, "160px", "Recommended Barrel Capacity", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        Header2TdSetting(tdHeader1, "0px", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 8
                        Title = "sec"
                        HeaderTdSetting(tdHeader, "160px", "Fill Time", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        Header2TdSetting(tdHeader1, "0px", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 9
                        Title = "sec"
                        HeaderTdSetting(tdHeader, "160px", "Dwell Time", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        Header2TdSetting(tdHeader1, "0px", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 10

                        HeaderTdSetting(tdHeader, "160px", "Dry Cycle Time", "1")
                        Header2TdSetting(tdHeader2, "", "sec", "1")
                        Header2TdSetting(tdHeader1, "0px", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 11
                        Title = "°C"
                        HeaderTdSetting(tdHeader, "", "Ejection Temp", "2")
                        Header2TdSetting(tdHeader2, "", Title, "2")
                        Header2TdSetting(tdHeader1, "76px", "Suggested", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)

                    Case 12
                        Header2TdSetting(tdHeader1, "76px", "Preferred", "1")
                        trHeader1.Controls.Add(tdHeader1)

                    Case 13
                        Title = "°C"
                        HeaderTdSetting(tdHeader, "", "Injection Temp", "2")
                        Header2TdSetting(tdHeader2, "", Title, "2")
                        Header2TdSetting(tdHeader1, "76px", "Suggested", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)

                    Case 14
                        Header2TdSetting(tdHeader1, "76px", "Preferred", "1")
                        trHeader1.Controls.Add(tdHeader1)
                    Case 15
                        Title = "°C"
                        HeaderTdSetting(tdHeader, "", "Mold Temp", "2")
                        Header2TdSetting(tdHeader2, "", Title, "2")
                        Header2TdSetting(tdHeader1, "76px", "Suggested", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)

                    Case 16
                        Header2TdSetting(tdHeader1, "76px", "Preferred", "1")
                        trHeader1.Controls.Add(tdHeader1)


                End Select


            Next


            tblComparision.Controls.Add(trHeader)
            tblComparision.Controls.Add(trHeader2)
            tblComparision.Controls.Add(trHeader1)

            'Inner
            For i = 1 To 10
                trInner = New TableRow
                For j = 1 To 16
                    tdInner = New TableCell

                    Select Case j
                        Case 1
                            'Department
                            InnerTdSetting(tdInner, "", "Center")
                            tdInner.Text = "<b>" + i.ToString() + "</b>"
                            trInner.Controls.Add(tdInner)
                            'tblComparisionFix.Controls.Add(trInner)
                        Case 2
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            lbl.Width = 180
                            GetMaterialDetails(Nothing, Nothing, CInt(ds.Tables(0).Rows(0).Item("MATID" + i.ToString() + "")), lbl)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 3
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.Width = 70
                            txt.ID = "SD" + i.ToString()
                            If (ds.Tables(0).Rows(0).Item("SD" + i.ToString() + "").ToString() <> "") Then
                                txt.Text = FormatNumber(objCalc.screwDia(i - 1) * CDbl(ds.Tables(0).Rows(0).Item("CONVTHICK").ToString()), 3)
                            End If
                            txt.MaxLength = 12
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 4
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.Width = 70
                            txt.ID = "INJRATEPS" + i.ToString()
                            If (ds.Tables(0).Rows(0).Item("INJRATEPS" + i.ToString() + "").ToString() <> "") Then
                                txt.Text = FormatNumber(objCalc.InjRatePS(i - 1) * CDbl(ds.Tables(0).Rows(0).Item("CONVVOL").ToString()), 4)
                            End If
                            txt.MaxLength = 12
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 5
                            InnerTdSetting(tdInner, "130px", "Right")
                            lbl = New Label
                            lbl.Width = 100
                            lbl.Text = FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("ShotWt" + i.ToString() + "").ToString()) * CDbl(ds.Tables(0).Rows(0).Item("CONVWT4").ToString()), 3)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 6
                            InnerTdSetting(tdInner, "130px", "Right")
                            lbl = New Label
                            lbl.Width = 100
                            If CDbl(ds.Tables(0).Rows(0).Item("MD" + i.ToString() + "").ToString()) > 0.0 Then
                                lbl.Text = FormatNumber((CDbl(ds.Tables(0).Rows(0).Item("ShotWt" + i.ToString() + "").ToString()) / CDbl(ds.Tables(0).Rows(0).Item("MD" + i.ToString() + "").ToString())).ToString(), 4)
                                lbl.Text = FormatNumber(CDbl(lbl.Text) * CDbl(ds.Tables(0).Rows(0).Item("CONVVOL").ToString()), 4)
                            Else
                                lbl.Text = FormatNumber(0, 3)
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 7
                            InnerTdSetting(tdInner, "130px", "Right")
                            lbl = New Label
                            lbl.Width = 100
                            If CDbl(ds.Tables(0).Rows(0).Item("MD" + i.ToString() + "").ToString()) > 0.0 Then
                                lbl.Text = FormatNumber((((CDbl(ds.Tables(0).Rows(0).Item("ShotWt" + i.ToString() + "").ToString())) / CDbl(ds.Tables(0).Rows(0).Item("MD" + i.ToString() + "").ToString())) / 0.5) * (CDbl((0.94 * 998.8473) / CDbl(ds.Tables(0).Rows(0).Item("MD" + i.ToString() + "").ToString()))), 4)
                                lbl.Text = FormatNumber(CDbl(lbl.Text) * CDbl(ds.Tables(0).Rows(0).Item("CONVVOL").ToString()), 4)
                            Else
                                lbl.Text = FormatNumber(0, 3)
                            End If

                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 8
                            InnerTdSetting(tdInner, "130px", "Right")
                            lbl = New Label
                            lbl.Width = 100
                            If CDbl(ds.Tables(0).Rows(0).Item("MD" + i.ToString() + "").ToString()) > 0.0 And CDbl(ds.Tables(0).Rows(0).Item("INJRATEPS" + i.ToString() + "").ToString()) > 0.0 Then
                                'lbl.Text = FormatNumber((CDbl(ds.Tables(0).Rows(0).Item("ShotWt" + i.ToString() + "").ToString()) / CDbl(ds.Tables(0).Rows(0).Item("MD" + i.ToString() + "").ToString()) * (CDbl(0.94 / CDbl(ds.Tables(0).Rows(0).Item("MD" + i.ToString() + "").ToString()))) / CDbl(ds.Tables(0).Rows(0).Item("INJRATEPS" + i.ToString() + "").ToString())), 3)
                                lbl.Text = FormatNumber(objCalc.FillTm(i - 1), 3)
                            Else
                                lbl.Text = FormatNumber(0, 3)
                            End If

                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)

                        Case 9
                            InnerTdSetting(tdInner, "130px", "Right")
                            lbl = New Label
                            lbl.Width = 100
                            If CDbl(ds.Tables(0).Rows(0).Item("MD" + i.ToString() + "").ToString()) > 0.0 And CDbl(ds.Tables(0).Rows(0).Item("INJRATEPS" + i.ToString() + "").ToString()) > 0.0 Then
                                lbl.Text = FormatNumber((CDbl(ds.Tables(0).Rows(0).Item("ShotWt" + i.ToString() + "").ToString()) / CDbl(ds.Tables(0).Rows(0).Item("MD" + i.ToString() + "").ToString()) * (CDbl(0.94 / CDbl(ds.Tables(0).Rows(0).Item("MD" + i.ToString() + "").ToString()))) / CDbl(ds.Tables(0).Rows(0).Item("INJRATEPS" + i.ToString() + "").ToString())), 3)
                                lbl.Text = FormatNumber(objCalc.DwellTm(i - 1), 3)
                            Else
                                lbl.Text = FormatNumber(0, 3)
                            End If

                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 10
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.Width = 70
                            txt.ID = "DRYCT" + i.ToString()
                            If (ds.Tables(0).Rows(0).Item("DRYCT" + i.ToString() + "").ToString() <> "") Then
                                txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("DRYCT" + i.ToString() + "").ToString(), 2)
                            End If
                            txt.MaxLength = 12
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 11
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Width = 70
                            lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("EJECTTMPP" + i.ToString() + "").ToString(), 3)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)

                        Case 13
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Width = 70
                            lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("INJECTTMPP" + i.ToString() + "").ToString(), 3)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)

                        Case 15
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Width = 70
                            lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("MOLDTMPP" + i.ToString() + "").ToString(), 3)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)

                        Case 12
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.Width = 70
                            txt.ID = "EJECTTMP" + i.ToString()
                            If (ds.Tables(0).Rows(0).Item("EJECTTMP" + i.ToString() + "").ToString() <> "") Then
                                txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("EJECTTMP" + i.ToString() + "").ToString(), 3)
                            End If
                            txt.MaxLength = 12
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 14
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.Width = 70
                            txt.ID = "INJECTTMP" + i.ToString()
                            If (ds.Tables(0).Rows(0).Item("INJECTTMP" + i.ToString() + "").ToString() <> "") Then
                                txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("INJECTTMP" + i.ToString() + "").ToString(), 3)
                            End If
                            txt.MaxLength = 12
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 16
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.Width = 70
                            txt.ID = "MOLDTMP" + i.ToString()
                            If (ds.Tables(0).Rows(0).Item("MOLDTMP" + i.ToString() + "").ToString() <> "") Then
                                txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("MOLDTMP" + i.ToString() + "").ToString(), 3)
                            End If
                            txt.MaxLength = 12
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)

                    End Select
                Next
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
                trInner.Height = 30
                tblComparision.Controls.Add(trInner)
            Next

            'Total
            trInner = New TableRow
            For i = 1 To 16
                tdInner = New TableCell
                Select Case i
                    Case 1
                        'Department
                        InnerTdSetting(tdInner, "", "Center")
                        tdInner.Text = "<b> Total </b>"
                        trInner.Controls.Add(tdInner)
                        'tblComparisionFix.Controls.Add(trInner)
                    Case 2
                        InnerTdSetting(tdInner, "", "Left")
                        Link = New HyperLink
                        hid = New HiddenField
                        Link.ID = "hypDesDeptA" + i.ToString()
                        hid.ID = "hidMatId" + i.ToString()
                        Link.Width = 130
                        Link.CssClass = "Link"
                        'GetDeptDetails(Link, hid, CInt(ds.Tables(0).Rows(0).Item("DEPTA" + i.ToString() + "").ToString()))
                        tdInner.Controls.Add(hid)
                        tdInner.Controls.Add(Link)
                        trInner.Controls.Add(tdInner)

                    Case 3, 4, 10, 11, 12, 13, 14, 15, 16
                        Dim Str As String = Chr(63 + i)
                        InnerTdSetting(tdInner, "", "Left")
                        Link = New HyperLink
                        hid = New HiddenField
                        Link.ID = "hypDesDept" + Chr(63 + j) + "" + i.ToString()
                        hid.ID = "hidMatId" + Chr(63 + j) + "" + i.ToString()
                        Link.Width = 130
                        Link.CssClass = "Link"
                        'GetDeptDetails(Link, hid, CInt(ds.Tables(0).Rows(0).Item("DEPT" + Chr(63 + j) + "" + i.ToString() + "").ToString()))
                        tdInner.Controls.Add(hid)
                        tdInner.Controls.Add(Link)
                        trInner.Controls.Add(tdInner)
                    Case 5
                        InnerTdSetting(tdInner, "130px", "Right")
                        lbl = New Label
                        lbl.Text = FormatNumber(objCalc.total(5) * CDbl(ds.Tables(0).Rows(0).Item("CONVWT4").ToString()), 3)
                        tdInner.Controls.Add(lbl)
                        trInner.Controls.Add(tdInner)
                    Case 6
                        InnerTdSetting(tdInner, "130px", "Right")
                        lbl = New Label
                        lbl.Text = FormatNumber(objCalc.total(6) * CDbl(ds.Tables(0).Rows(0).Item("CONVVOL").ToString()), 3)
                        tdInner.Controls.Add(lbl)
                        trInner.Controls.Add(tdInner)
                    Case 7
                        InnerTdSetting(tdInner, "130px", "Right")
                        lbl = New Label
                        lbl.Text = FormatNumber(objCalc.total(7) * CDbl(ds.Tables(0).Rows(0).Item("CONVVOL").ToString()), 3)
                        tdInner.Controls.Add(lbl)
                        trInner.Controls.Add(tdInner)
                    Case 8
                        InnerTdSetting(tdInner, "130px", "Right")
                        lbl = New Label
                        lbl.Text = FormatNumber(objCalc.total(8), 3)
                        tdInner.Controls.Add(lbl)
                        trInner.Controls.Add(tdInner)
                    Case 9
                        InnerTdSetting(tdInner, "130px", "Right")
                        lbl = New Label
                        lbl.Text = FormatNumber(objCalc.total(9), 3)
                        tdInner.Controls.Add(lbl)
                        trInner.Controls.Add(tdInner)

                End Select
            Next
            trInner.Height = 30
            trInner.CssClass = "AlterNateColor2"
            tblComparision.Controls.Add(trInner)



        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetailsForTab2:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetPageDetailsForTab4(ByVal ds As DataSet, ByVal objCalc As InjectionCalc.Calculation)

        Dim i As New Integer
        Dim j As New Integer
        Dim DWidth As String = String.Empty
        Dim trHeader As New TableRow
        Dim trHeader1 As New TableRow
        Dim trHeader2 As New TableRow
        Dim trInner As New TableRow
        Dim tdHeader As TableCell
        Dim tdHeader1 As TableCell
        Dim tdHeader2 As TableCell
        Dim lbl As New Label
        Dim hid As New HiddenField
        Dim Link As New HyperLink
        Dim txt As New TextBox
        Dim tdInner As TableCell
        Dim Path As String



        Try
            ' Dim objCalc As New InjectionCalc.Calculation
            ' objCalc.InjectionMoldCalculate(CaseId)
            trHeader.Height = 30

            For i = 1 To 7
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "20px", "No.", "1")
                        Header2TdSetting(tdHeader2, "0", "", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        ' trHeader1.Controls.Add(tdHeader1)
                    Case 2
                        HeaderTdSetting(tdHeader, "140px", "Material", "1")
                        Header2TdSetting(tdHeader2, "0", "", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        ' trHeader1.Controls.Add(tdHeader1)
                    Case 3
                        Title = "sec"
                        HeaderTdSetting(tdHeader, "130px", "Cooling Time", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        Header2TdSetting(tdHeader1, "0px", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        '  trHeader1.Controls.Add(tdHeader1)
                    Case 4
                        Title = "sec"
                        HeaderTdSetting(tdHeader, "130px", "Cycle Time", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        Header2TdSetting(tdHeader1, "0px", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        '  trHeader1.Controls.Add(tdHeader1)
                    Case 5
                        Title = "cpm"
                        HeaderTdSetting(tdHeader, "130px", "Cycle Time", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        Header2TdSetting(tdHeader1, "0px", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        ' trHeader1.Controls.Add(tdHeader1)
                    Case 6
                        Title = ds.Tables(0).Rows(0).Item("TITLE16").ToString()
                        HeaderTdSetting(tdHeader, "130px", "clamp force estimate", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        Header2TdSetting(tdHeader1, "0px", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 7
                        Title = ""
                        HeaderTdSetting(tdHeader, "130px", "", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        Header2TdSetting(tdHeader1, "0px", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        ' trHeader1.Controls.Add(tdHeader1)

                        ' trHeader1.Controls.Add(tdHeader1)
                End Select

                trHeader1.Controls.Add(tdHeader1)
            Next

            '  trHeader.Height = 30

            tblComparision.Controls.Add(trHeader)
            tblComparision.Controls.Add(trHeader2)
            tblComparision.Controls.Add(trHeader1)

            'Inner
            For i = 1 To 10
                trInner = New TableRow
                For j = 1 To 7
                    tdInner = New TableCell

                    Select Case j
                        Case 1
                            'Department
                            InnerTdSetting(tdInner, "", "Left")
                            tdInner.Text = "<b>" + i.ToString() + "</b>"
                            trInner.Controls.Add(tdInner)
                            'tblComparisionFix.Controls.Add(trInner)
                        Case 2
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            lbl.Width = 180
                            GetMaterialDetails(Nothing, Nothing, CInt(ds.Tables(0).Rows(0).Item("MATID" + i.ToString() + "")), lbl)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 3
                            InnerTdSetting(tdInner, "130px", "Right")
                            lbl = New Label
                            If IsNumeric(objCalc.CoolingTm(i - 1)) Then
                                lbl.Text = FormatNumber(objCalc.CoolingTm(i - 1), 3)

                            End If

                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 4
                            InnerTdSetting(tdInner, "130px", "Right")
                            lbl = New Label
                            lbl.Text = FormatNumber(objCalc.CycleTm(i - 1), 3)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 5
                            InnerTdSetting(tdInner, "130px", "Right")
                            lbl = New Label
                            lbl.Text = FormatNumber(objCalc.CycleTmPrMin(i - 1), 3)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 6
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Width = 180
                            lbl.Text = FormatNumber(objCalc.CFE(i - 1) / CDbl(ds.Tables(0).Rows(0).Item("CONVWT3").ToString()), 3)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 7
                            InnerTdSetting(tdInner, "", "Left")
                            Link = New HyperLink
                            hid = New HiddenField
                            Link.ID = "hypDesDeptA" + i.ToString()
                            hid.ID = "hidMatId" + i.ToString()
                            Link.Width = 130
                            Link.CssClass = "Link"
                            Link.Text = "Check Result"

                            Path = "../PopUp/InjectionResult.aspx?CFE=" + (CDbl(objCalc.CFE(i - 1)) / CDbl(ds.Tables(0).Rows(0).Item("CONVWT3").ToString())).ToString() + "&Shotwt=" + (ds.Tables(0).Rows(0).Item("shotwt" + i.ToString() + "")).ToString() + "&InjRate=" + (CDbl(ds.Tables(0).Rows(0).Item("INJRATEPS" + i.ToString() + ""))).ToString() + ""
                            Link.NavigateUrl = "javascript:ShowPopWindow('" + Path + "')"

                            tdInner.Controls.Add(hid)
                            tdInner.Controls.Add(Link)
                            trInner.Controls.Add(tdInner)

                    End Select
                Next
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
                trInner.Height = 30
                tblComparision.Controls.Add(trInner)
            Next

            'Total
            trInner = New TableRow
            For i = 1 To 7
                tdInner = New TableCell
                Select Case i
                    Case 1
                        'Department
                        InnerTdSetting(tdInner, "", "Center")
                        tdInner.Text = "<b> Total </b>"
                        trInner.Controls.Add(tdInner)
                        'tblComparisionFix.Controls.Add(trInner)
                    Case 2, 7
                        InnerTdSetting(tdInner, "", "Center")
                        Link = New HyperLink
                        hid = New HiddenField
                        Link.ID = "hypDesDeptA" + i.ToString()
                        hid.ID = "hidMatId" + i.ToString()
                        Link.Width = 130
                        Link.CssClass = "Link"
                        'GetDeptDetails(Link, hid, CInt(ds.Tables(0).Rows(0).Item("DEPTA" + i.ToString() + "").ToString()))
                        tdInner.Controls.Add(hid)
                        tdInner.Controls.Add(Link)
                        trInner.Controls.Add(tdInner)
                    Case 3
                        InnerTdSetting(tdInner, "130px", "Right")
                        lbl = New Label
                        lbl.Text = FormatNumber(objCalc.total(10), 3)
                        tdInner.Controls.Add(lbl)
                        trInner.Controls.Add(tdInner)
                    Case 4
                        InnerTdSetting(tdInner, "130px", "Right")
                        lbl = New Label
                        lbl.Text = FormatNumber(objCalc.total(11), 3)
                        tdInner.Controls.Add(lbl)
                        trInner.Controls.Add(tdInner)
                    Case 5
                        InnerTdSetting(tdInner, "130px", "Right")
                        lbl = New Label
                        lbl.Text = FormatNumber(objCalc.total(12), 3)
                        tdInner.Controls.Add(lbl)
                        trInner.Controls.Add(tdInner)
                    Case 6
                        InnerTdSetting(tdInner, "", "Right")
                        lbl = New Label
                        lbl.Text = FormatNumber(objCalc.total(19) / CDbl(ds.Tables(0).Rows(0).Item("CONVWT3").ToString()), 3)
                        tdInner.Controls.Add(lbl)
                        trInner.Controls.Add(tdInner)
                End Select
            Next
            trInner.Height = 30
            trInner.CssClass = "AlterNateColor2"
            tblComparision.Controls.Add(trInner)



        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetailsForTab2:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetPageDetailsForTab5(ByVal ds As DataSet, ByVal objCalc As InjectionCalc.Calculation)

        Dim i As New Integer
        Dim j As New Integer
        Dim DWidth As String = String.Empty
        Dim trHeader As New TableRow
        Dim trHeader1 As New TableRow
        Dim trHeader2 As New TableRow
        Dim trInner As New TableRow
        Dim tdHeader As TableCell
        Dim tdHeader1 As TableCell
        Dim tdHeader2 As TableCell
        Dim lbl As New Label
        Dim hid As New HiddenField
        Dim Link As New HyperLink
        Dim txt As New TextBox
        Dim tdInner As TableCell



        Try
            'Dim objCalc As New InjectionCalc.Calculation
            ' objCalc.InjectionMoldCalculate(CaseId)
            trHeader.Height = 30

            For i = 1 To 13
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "20px", "No.", "1")
                        Header2TdSetting(tdHeader2, "0", "", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        ' trHeader1.Controls.Add(tdHeader1)
                    Case 2
                        HeaderTdSetting(tdHeader, "140px", "Material", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        ' trHeader1.Controls.Add(tdHeader1)
                    Case 3
                        Title = "parts/min"
                        HeaderTdSetting(tdHeader, "", "Instananeous Throughput", "3")
                        Header2TdSetting(tdHeader2, "70px", Title, "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)

                    Case 4
                        Title = "parts/hr"
                        Header2TdSetting(tdHeader2, "70px", Title, "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        trHeader2.Controls.Add(tdHeader2)

                    Case 5
                        Title = ds.Tables(0).Rows(0).Item("TITLE8") + "/hr"
                        Header2TdSetting(tdHeader2, "70px", Title, "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        trHeader2.Controls.Add(tdHeader2)
                    Case 6
                        HeaderTdSetting(tdHeader, "74px", "No of presses", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 7
                        Title = "kwh"
                        HeaderTdSetting(tdHeader, "160px", "Fixed Energy Load", "2")
                        Header2TdSetting(tdHeader2, "76px", Title, "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 8
                        Title = "kwh/" + ds.Tables(0).Rows(0).Item("TITLE8")
                        Header2TdSetting(tdHeader2, "76px", Title, "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        'trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 9
                        Title = "kwh"
                        HeaderTdSetting(tdHeader, "140px", "Process Load", "2")
                        Header2TdSetting(tdHeader2, "76px", Title, "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 10
                        Title = "kwh/" + ds.Tables(0).Rows(0).Item("TITLE8")
                        Header2TdSetting(tdHeader2, "70px", Title, "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        'trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 11
                        Title = "kwh/" + ds.Tables(0).Rows(0).Item("TITLE8")
                        HeaderTdSetting(tdHeader, "80px", "Total Load", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 12
                        Title = "kwh/" + ds.Tables(0).Rows(0).Item("TITLE8")
                        HeaderTdSetting(tdHeader, "80px", "Benchmark", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 13
                        Title = "kwh/" + ds.Tables(0).Rows(0).Item("TITLE8")
                        HeaderTdSetting(tdHeader, "80px", "Delta", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                End Select

                trHeader1.Controls.Add(tdHeader1)
            Next

            '  trHeader.Height = 30

            tblComparision.Controls.Add(trHeader)
            tblComparision.Controls.Add(trHeader2)
            tblComparision.Controls.Add(trHeader1)

            ' Inner()
            For i = 1 To 10
                trInner = New TableRow
                For j = 1 To 13
                    tdInner = New TableCell

                    Select Case j
                        Case 1
                            'Department
                            InnerTdSetting(tdInner, "", "Left")
                            tdInner.Text = "<b>" + i.ToString() + "</b>"
                            trInner.Controls.Add(tdInner)
                            'tblComparisionFix.Controls.Add(trInner)
                        Case 2
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            lbl.Width = 140
                            GetMaterialDetails(Nothing, Nothing, CInt(ds.Tables(0).Rows(0).Item("MATID" + i.ToString() + "")), lbl)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 3
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            If IsNumeric(objCalc.InsThruput(i - 1)) Then
                                lbl.Text = FormatNumber(objCalc.InsThruput(i - 1), 3)

                            End If

                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 4
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Text = FormatNumber(objCalc.InsThruputhr(i - 1), 3)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 5
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Text = FormatNumber(objCalc.InsThruputkg(i - 1), 3)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 6
                            InnerTdSetting(tdInner, "", "Right")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.Width = 70
                            txt.ID = "NoPr" + i.ToString()
                            If (ds.Tables(0).Rows(0).Item("Nopress" + i.ToString() + "").ToString() <> "") Then
                                txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("Nopress" + i.ToString() + "").ToString(), 0)
                            End If
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 7
                            InnerTdSetting(tdInner, "", "Right")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.Width = 70
                            txt.ID = "FEL" + i.ToString()
                            If (ds.Tables(0).Rows(0).Item("FixEnerLoad" + i.ToString() + "").ToString() <> "") Then
                                txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("FixEnerLoad" + i.ToString() + "").ToString(), 0)
                            End If
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 8
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Text = FormatNumber(objCalc.FixEnerLoad(i - 1), 3)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 9
                            InnerTdSetting(tdInner, "", "Right")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.Width = 70
                            txt.ID = "PL" + i.ToString()
                            If (ds.Tables(0).Rows(0).Item("ProcessLoad" + i.ToString() + "").ToString() <> "") Then
                                txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("ProcessLoad" + i.ToString() + "").ToString(), 0)
                            End If
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 10
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Text = FormatNumber(objCalc.ProcessLoad(i - 1), 3)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 11
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Text = FormatNumber(objCalc.TotalLoad(i - 1), 3)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 12
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Text = FormatNumber(objCalc.Benchmark(i - 1), 3)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 13
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Text = FormatNumber(objCalc.Delta(i - 1), 3)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)

                    End Select
                Next
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
                trInner.Height = 30
                tblComparision.Controls.Add(trInner)
            Next

            'Total
            trInner = New TableRow
            For i = 1 To 13
                tdInner = New TableCell
                Select Case i
                    Case 1
                        'Department
                        InnerTdSetting(tdInner, "", "Center")
                        tdInner.Text = "<b> Total </b>"
                        trInner.Controls.Add(tdInner)
                        'tblComparisionFix.Controls.Add(trInner)
                    Case 2
                        InnerTdSetting(tdInner, "", "Center")
                        Link = New HyperLink
                        hid = New HiddenField
                        Link.ID = "hypDesDeptA" + i.ToString()
                        hid.ID = "hidMatId" + i.ToString()
                        Link.Width = 130
                        Link.CssClass = "Link"
                        'GetDeptDetails(Link, hid, CInt(ds.Tables(0).Rows(0).Item("DEPTA" + i.ToString() + "").ToString()))
                        tdInner.Controls.Add(hid)
                        tdInner.Controls.Add(Link)
                        trInner.Controls.Add(tdInner)
                    Case 3
                        InnerTdSetting(tdInner, "130px", "Right")
                        lbl = New Label
                        lbl.Text = FormatNumber(objCalc.total(13), 3)
                        tdInner.Controls.Add(lbl)
                        trInner.Controls.Add(tdInner)
                    Case 4
                        InnerTdSetting(tdInner, "130px", "Right")
                        lbl = New Label
                        lbl.Text = FormatNumber(objCalc.total(14), 3)
                        tdInner.Controls.Add(lbl)
                        trInner.Controls.Add(tdInner)
                    Case 5
                        InnerTdSetting(tdInner, "130px", "Right")
                        lbl = New Label
                        lbl.Text = FormatNumber(objCalc.total(15), 3)
                        tdInner.Controls.Add(lbl)
                        trInner.Controls.Add(tdInner)
                    Case 6
                        InnerTdSetting(tdInner, "130px", "Right")
                        lbl = New Label
                        ' lbl.Text = FormatNumber(objCalc.total(12), 3)
                        tdInner.Controls.Add(lbl)
                        trInner.Controls.Add(tdInner)
                    Case 7
                        InnerTdSetting(tdInner, "130px", "Right")
                        lbl = New Label
                        ' lbl.Text = FormatNumber(objCalc.total(12), 3)
                        tdInner.Controls.Add(lbl)
                        trInner.Controls.Add(tdInner)
                    Case 8
                        InnerTdSetting(tdInner, "130px", "Right")
                        lbl = New Label
                        ' lbl.Text = FormatNumber(objCalc.total(12), 3)
                        tdInner.Controls.Add(lbl)
                        trInner.Controls.Add(tdInner)
                    Case 9
                        InnerTdSetting(tdInner, "130px", "Right")
                        lbl = New Label
                        ' lbl.Text = FormatNumber(objCalc.total(12), 3)
                        tdInner.Controls.Add(lbl)
                        trInner.Controls.Add(tdInner)
                    Case 10
                        InnerTdSetting(tdInner, "130px", "Right")
                        lbl = New Label
                        ' lbl.Text = FormatNumber(objCalc.total(12), 3)
                        tdInner.Controls.Add(lbl)
                        trInner.Controls.Add(tdInner)
                    Case 11
                        InnerTdSetting(tdInner, "130px", "Right")
                        lbl = New Label
                        lbl.Text = FormatNumber(objCalc.total(16), 3)
                        tdInner.Controls.Add(lbl)
                        trInner.Controls.Add(tdInner)
                    Case 12
                        InnerTdSetting(tdInner, "130px", "Right")
                        lbl = New Label
                        lbl.Text = FormatNumber(objCalc.total(17), 3)
                        tdInner.Controls.Add(lbl)
                        trInner.Controls.Add(tdInner)
                    Case 13
                        InnerTdSetting(tdInner, "130px", "Right")
                        lbl = New Label
                        lbl.Text = FormatNumber(objCalc.total(18), 3)
                        tdInner.Controls.Add(lbl)
                        trInner.Controls.Add(tdInner)

                End Select
            Next
            trInner.Height = 30
            trInner.CssClass = "AlterNateColor2"
            tblComparision.Controls.Add(trInner)



        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetailsForTab2:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetMaterialDetails(ByRef LinkMat As HyperLink, ByVal hid As HiddenField, ByVal MatId As Integer, ByVal lbl As Label)
        Dim Ds As New DataSet
        Dim ObjGetdata As New Med1GetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty

        Try

            Ds = ObjGetdata.GetMaterials(MatId, "", "")
            If Ds.Tables(0).Rows(0).Item("MATDES").ToString().Length > 25 Then
                'LinkMat.Font.Size = 8
            End If
            If Not IsNothing(LinkMat) Then
                LinkMat.Text = Ds.Tables(0).Rows(0).Item("MATDES").ToString()
                LinkMat.ToolTip = Ds.Tables(0).Rows(0).Item("MATDES").ToString()
                LinkMat.Attributes.Add("text-decoration", "none")
                'LinkMat.Attributes.Add("onmouseover", "Tip('" + Ds.Tables(0).Rows(0).Item("MATDES").ToString() + "')")
                'LinkMat.Attributes.Add("onmouseout", "UnTip()")

                hid.Value = MatId.ToString()
                Path = "../PopUp/GetInjectionMaterial.aspx?Des=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + LinkMat.ClientID + "&Id=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + hid.ClientID + ""
                LinkMat.NavigateUrl = "javascript:ShowPopWindow('" + Path + "')"

            ElseIf Not IsNothing(lbl) Then
                lbl.Text = Ds.Tables(0).Rows(0).Item("MATDES").ToString()
                lbl.ID = MatId.ToString()
            End If
        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
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
            If Align = "Left" Then
                Td.Style.Add("padding-left", "5px")
            End If
            If Align = "Right" Then
                Td.Style.Add("padding-right", "5px")
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:InnerTdSetting:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub TextBoxSetting(ByVal txt As TextBox, ByVal Css As String)
        Try
            txt.CssClass = Css

        Catch ex As Exception
            _lErrorLble.Text = "Error:TextBoxSetting:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub LableSetting(ByVal lbl As Label, ByVal Css As String)
        Try
            lbl.CssClass = Css

        Catch ex As Exception
            _lErrorLble.Text = "Error:LableSetting:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub LeftTdSetting(ByVal Td As TableCell, ByVal Text As String, ByVal tr As TableRow, ByVal Css As String)
        Try
            Td.Text = Text
            InnerTdSetting(Td, "", "Left")
            tr.Controls.Add(Td)
            tr.CssClass = Css
        Catch ex As Exception
            _lErrorLble.Text = "Error:LeftTdSetting:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub UpdatePage()
        Dim Material(9) As String
        Dim PartWt(9) As String
        Dim MaxWallThick(9) As String
        Dim AvgWallThick(9) As String
        Dim SurfaceArea(9) As String
        Dim Levels(9) As String
        Dim CavPrLevels(9) As String
        Dim ScrewDia(9) As String
        Dim InjRate(9) As String
        Dim DryCycleTm(9) As String
        Dim EjectTmp(9) As String
        Dim InjectTmp(9) As String
        Dim MoldTmp(9) As String
        Dim StackMold(9) As String
        Dim RunSys(9) As String
        Dim NoOfPresses(9) As String
        Dim FixEnrgyLoad(9) As String
        Dim ProcessLoad(9) As String
        Dim i As New Integer
        Dim ObjUpIns As New Med1UpInsData.UpdateInsert()
        Dim obj As New CryptoHelper
        Dim rb As New RadioButton

        Try

            If Not objRefresh.IsRefresh Then
                If ViewState("TabId").ToString() = "1" Then
                    For i = 1 To 10
                        Material(i - 1) = Request.Form("ctl00$Med1InkContentPlaceHolder$hidMatId" + i.ToString() + "")
                        PartWt(i - 1) = Request.Form("ctl00$Med1InkContentPlaceHolder$PWT" + i.ToString() + "").Replace(",", "")
                        MaxWallThick(i - 1) = Request.Form("ctl00$Med1InkContentPlaceHolder$MWT" + i.ToString() + "").Replace(",", "")
                        AvgWallThick(i - 1) = 0
                        SurfaceArea(i - 1) = Request.Form("ctl00$Med1InkContentPlaceHolder$SA" + i.ToString() + "").Replace(",", "")

                        'Check For IsNumric


                        If Not IsNumeric(PartWt(i - 1)) Then
                            Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        End If

                        If Not IsNumeric(MaxWallThick(i - 1)) Then
                            Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        End If

                        If Not IsNumeric(SurfaceArea(i - 1)) Then
                            Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        End If

                    Next
                    ObjUpIns.InjectionUpdate(CaseId, Material, PartWt, MaxWallThick, AvgWallThick, SurfaceArea, StackMold, RunSys, Levels, CavPrLevels, ScrewDia, InjRate, DryCycleTm, EjectTmp, InjectTmp, MoldTmp, NoOfPresses, FixEnrgyLoad, ProcessLoad, ViewState("TabId").ToString())

                ElseIf ViewState("TabId").ToString() = "2" Then
                    For i = 1 To 10
                        Levels(i - 1) = Request.Form("ctl00$Med1InkContentPlaceHolder$LVLS" + i.ToString() + "").Replace(",", "")
                        CavPrLevels(i - 1) = Request.Form("ctl00$Med1InkContentPlaceHolder$CAVPL" + i.ToString() + "").Replace(",", "")

                        If Request.Form("ctl00$Med1InkContentPlaceHolder$ISM" + i.ToString() + "") = "on" Then
                            StackMold(i - 1) = "1"
                        Else
                            StackMold(i - 1) = "0"
                        End If

                        If Request.Form("ctl00$Med1InkContentPlaceHolder$RunnerSys" + i.ToString() + "") = "IC" + i.ToString() + "" Then
                            RunSys(i - 1) = "1"
                        ElseIf Request.Form("ctl00$Med1InkContentPlaceHolder$RunnerSys" + i.ToString() + "") = "IH" + i.ToString() + "" Then
                            RunSys(i - 1) = "2"
                        Else
                            RunSys(i - 1) = "0"
                        End If

                        If Not IsNumeric(Levels(i - 1)) Then
                            Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        End If
                        If Not IsNumeric(CavPrLevels(i - 1)) Then
                            Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        End If
                        If Not IsNumeric(StackMold(i - 1)) Then
                            Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        End If
                        If Not IsNumeric(RunSys(i - 1)) Then
                            Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        End If


                    Next
                    ObjUpIns.InjectionUpdate(CaseId, Material, PartWt, MaxWallThick, AvgWallThick, SurfaceArea, StackMold, RunSys, Levels, CavPrLevels, ScrewDia, InjRate, DryCycleTm, EjectTmp, InjectTmp, MoldTmp, NoOfPresses, FixEnrgyLoad, ProcessLoad, ViewState("TabId").ToString())
                ElseIf ViewState("TabId").ToString() = "3" Then
                    For i = 1 To 10
                        ScrewDia(i - 1) = Request.Form("ctl00$Med1InkContentPlaceHolder$SD" + i.ToString() + "").Replace(",", "")
                        InjRate(i - 1) = Request.Form("ctl00$Med1InkContentPlaceHolder$INJRATEPS" + i.ToString() + "").Replace(",", "")
                        EjectTmp(i - 1) = Request.Form("ctl00$Med1InkContentPlaceHolder$EJECTTMP" + i.ToString() + "")
                        DryCycleTm(i - 1) = Request.Form("ctl00$Med1InkContentPlaceHolder$DRYCT" + i.ToString() + "").Replace(",", "")
                        InjectTmp(i - 1) = Request.Form("ctl00$Med1InkContentPlaceHolder$INJECTTMP" + i.ToString() + "").Replace(",", "")
                        MoldTmp(i - 1) = Request.Form("ctl00$Med1InkContentPlaceHolder$MOLDTMP" + i.ToString() + "").Replace(",", "")
                        'Check Numeric
                        If Not IsNumeric(ScrewDia(i - 1)) Then
                            Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        End If
                        If Not IsNumeric(InjRate(i - 1)) Then
                            Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        End If
                        If Not IsNumeric(DryCycleTm(i - 1)) Then
                            Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        End If
                        If Not IsNumeric(EjectTmp(i - 1)) Then
                            Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        End If
                        If Not IsNumeric(InjectTmp(i - 1)) Then
                            Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        End If
                        If Not IsNumeric(MoldTmp(i - 1)) Then
                            Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        End If

                    Next
                    ObjUpIns.InjectionUpdate(CaseId, Material, PartWt, MaxWallThick, AvgWallThick, SurfaceArea, StackMold, RunSys, Levels, CavPrLevels, ScrewDia, InjRate, DryCycleTm, EjectTmp, InjectTmp, MoldTmp, NoOfPresses, FixEnrgyLoad, ProcessLoad, ViewState("TabId").ToString())

                ElseIf ViewState("TabId").ToString() = "5" Then
                    For i = 1 To 10
                        NoOfPresses(i - 1) = Request.Form("ctl00$Med1InkContentPlaceHolder$NoPr" + i.ToString() + "").Replace(",", "")
                        FixEnrgyLoad(i - 1) = Request.Form("ctl00$Med1InkContentPlaceHolder$FEL" + i.ToString() + "").Replace(",", "")
                        ProcessLoad(i - 1) = Request.Form("ctl00$Med1InkContentPlaceHolder$PL" + i.ToString() + "")

                        'Check Numeric
                        If Not IsNumeric(NoOfPresses(i - 1)) Then
                            Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        End If
                        If Not IsNumeric(FixEnrgyLoad(i - 1)) Then
                            Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        End If
                        If Not IsNumeric(ProcessLoad(i - 1)) Then
                            Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        End If


                    Next
                    ObjUpIns.InjectionUpdate(CaseId, Material, PartWt, MaxWallThick, AvgWallThick, SurfaceArea, StackMold, RunSys, Levels, CavPrLevels, ScrewDia, InjRate, DryCycleTm, EjectTmp, InjectTmp, MoldTmp, NoOfPresses, FixEnrgyLoad, ProcessLoad, ViewState("TabId").ToString())
                End If
            End If
            'Calculate()


        Catch ex As Exception
            ErrorLable.Text = "Error:UpdatePage:" + ex.Message.ToString() + ""
        End Try
    End Sub
    Protected Sub Update_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            If ViewState("UnitId1") <> Session("UnitId") Then
                'If Not Session("Post") Is Nothing And Session("Post") <> Session("Prev") Then
                GetPageDetails1()
                ScriptManager.RegisterStartupScript(Page, Me.GetType(), "JSScript", "alert('There has been done changes in Preference page,Please close this page and reopen again.');", True)
                Exit Sub
            End If
            If Not objRefresh.IsRefresh Then
                UpdatePage()
            End If
            GetPageDetails()

        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
End Class
