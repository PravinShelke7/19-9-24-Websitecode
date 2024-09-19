Imports System.Data
Imports System.Data.OleDb
Imports System
Imports S1GetData
Imports S1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Sustain1_Assumptions_RwMatPack
    Inherits System.Web.UI.Page

#Region "Get Set Variables"
    Dim _lErrorLble As Label
    Dim _iCaseId As Integer
    Dim _iUserId As Integer
    Dim _strUserRole As String
    Dim _btnUpdate As ImageButton
    Dim _btnLogOff As ImageButton
    Dim _btnChart As ImageButton
    Dim _btnNotes As ImageButton
    Dim _btnFeedBack As ImageButton
    Dim _btnInstrutions As ImageButton
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

    Public Property Updatebtn() As ImageButton
        Get
            Return _btnUpdate
        End Get
        Set(ByVal value As ImageButton)
            _btnUpdate = value
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

    Public Property Chartbtn() As ImageButton
        Get
            Return _btnChart
        End Get
        Set(ByVal value As ImageButton)
            _btnChart = value
        End Set
    End Property

    Public Property Notesbtn() As ImageButton
        Get
            Return _btnNotes
        End Get
        Set(ByVal value As ImageButton)
            _btnNotes = value
        End Set
    End Property

    Public Property FeedBackbtn() As ImageButton
        Get
            Return _btnFeedBack
        End Get
        Set(ByVal value As ImageButton)
            _btnFeedBack = value
        End Set
    End Property

    Public Property Instrutionsbtn() As ImageButton
        Get
            Return _btnInstrutions
        End Get
        Set(ByVal value As ImageButton)
            _btnInstrutions = value
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



    Public DataCnt As Integer
    Public CaseDesp As New ArrayList
#End Region

#Region "MastePage Content Variables"

    Protected Sub GetMasterPageControls()
        GetContentPlaceHolder()
        GetErrorLable()
        GetUpdatebtn()
        GetLogOffbtn()
        GetInstructionsbtn()
        GetChartbtn()
        GetFeedbackbtn()
        GetNotesbtn()
        GetMainHeadingdiv()

    End Sub

    Protected Sub GetErrorLable()
        ErrorLable = Page.Master.FindControl("lblError")
    End Sub

    Protected Sub GetUpdatebtn()
        Updatebtn = Page.Master.FindControl("imgUpdate")
        Updatebtn.Attributes.Add("onclick", "return CheckForRawMatPage('" + ctlContentPlaceHolder.ClientID + "','hidPalid','NUM')")
        Updatebtn.Visible = True
        AddHandler Updatebtn.Click, AddressOf Update_Click
    End Sub

    Protected Sub GetLogOffbtn()
        LogOffbtn = Page.Master.FindControl("imgLogoff")
        LogOffbtn.Visible = False
        'AddHandler Updatebtn.Click, AddressOf Update_Click
    End Sub

    Protected Sub GetInstructionsbtn()
        Instrutionsbtn = Page.Master.FindControl("imgInstructions")
        Instrutionsbtn.Visible = True
        'AddHandler Updatebtn.Click, AddressOf Update_Click
    End Sub

    Protected Sub GetChartbtn()
        Chartbtn = Page.Master.FindControl("imgChart")
        Chartbtn.Visible = False
        'AddHandler Updatebtn.Click, AddressOf Update_Click
    End Sub

    Protected Sub GetFeedbackbtn()
        FeedBackbtn = Page.Master.FindControl("imgFeedback")
        FeedBackbtn.Visible = True
        'AddHandler Updatebtn.Click, AddressOf Update_Click
    End Sub

    Protected Sub GetNotesbtn()
        Notesbtn = Page.Master.FindControl("imgNotes")
        Notesbtn.Visible = True
        Notesbtn.OnClientClick = "return Notes('PALLETPKG2');"
        'AddHandler Updatebtn.Click, AddressOf Update_Click
    End Sub

    Protected Sub GetMainHeadingdiv()
        MainHeading = Page.Master.FindControl("divMainHeading")
        MainHeading.Attributes.Add("onmouseover", "Tip('Raw Material Packaging Assumptions')")
        MainHeading.Attributes.Add("onmouseout", "UnTip()")
        MainHeading.InnerHtml = "Sustain1 - Raw Material Packaging Assumptions"
    End Sub

    Protected Sub GetContentPlaceHolder()
        ctlContentPlaceHolder = Page.Master.FindControl("Sustain1ContentPlaceHolder")
    End Sub

#End Region

#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_PAGES_SUSTAIN1_ASSUMPTIONS_RWMATPACK")
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
                GetPageDetails()
            End If
            CreateTab()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GetSessionDetails()
        Try
            UserId = Session("UserId")
            CaseId = Session("S1CaseId")
            UserRole = Session("S1UserRole")
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetSessionDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetPageDetails()
        Dim ds As New DataSet
        Dim dsSug As New DataSet
        Dim dsMat As New DataSet
        Dim dsTotal As New DataSet
        Dim objGetData As New S1GetData.Selectdata
        Dim i As New Integer
        Dim j As New Integer
        Dim DWidth As String = String.Empty
        Dim trHeader As New TableRow
        Dim trHeader1 As New TableRow
        Dim trHeader2 As New TableRow
        Dim trHeaderFix As New TableRow
        Dim trHeaderFix1 As New TableRow
        Dim trHeaderFix2 As New TableRow
        Dim trInner As New TableRow
        Dim trInnerFix As New TableRow
        Dim tdHeader As TableCell
        Dim tdHeader1 As TableCell
        Dim tdHeader2 As TableCell
        Dim lbl As New Label
        Dim hid As New HiddenField
        Dim Link As New HyperLink
        Dim txt As New TextBox
        Dim tdInner As TableCell
        Dim Col As String = String.Empty
        Dim dsEmat As New DataSet
        Dim dv As New DataView
        Dim dt As New DataTable
        Dim dsPallet As New DataSet
        Try
            dsPallet = objGetData.GetPallets("-1", "", "")

            dsEmat = objGetData.GetEditMaterial(CaseId)
            Col = Chr(64 + Convert.ToInt32(ViewState("TabId"))).ToString()
            ds = objGetData.GetRWPalletInPref(CaseId, Col, Convert.ToInt32(ViewState("TabId")))
            dsSug = objGetData.GetRWPalletInSugg(CaseId, Col)
            dsMat = objGetData.GetRWPalletMat(CaseId, ViewState("TabId").ToString())
            dsTotal = objGetData.GetRWPalletTotal(CaseId, ViewState("TabId").ToString())
            dv = dsEmat.Tables(0).DefaultView
            If dsMat.Tables(0).Rows(0).Item("MATID").ToString() <> "" Then
                dv.RowFilter = "MATID=" + dsMat.Tables(0).Rows(0).Item("MATID").ToString()
                dt = dv.ToTable()
                If dt.Rows.Count > 0 Then
                    lblMaterialName.Text = "<b>Material " + ViewState("TabId").ToString() + ":</b>" + dt.Rows(0).Item("MATDES").ToString().Replace("&#", "'") + ""
                Else
                    lblMaterialName.Text = "<b>Material " + ViewState("TabId").ToString() + ":</b>" + dsMat.Tables(0).Rows(0).Item("MATDES").ToString() + ""
                End If
            Else
                lblMaterialName.Text = "<b>Material " + ViewState("TabId").ToString() + ":</b>" + dsMat.Tables(0).Rows(0).Item("MATDES").ToString() + ""
            End If
            tblComparision.Rows.Clear()
            For i = 1 To 19
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "50px", "Layers", "1")
                        HeaderTdSetting(tdHeader1, "0", "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeaderFix1.Controls.Add(tdHeader1)
                        trHeaderFix.Controls.Add(tdHeader)
                        trHeaderFix2.Controls.Add(tdHeader2)
                    Case 2
                        HeaderTdSetting(tdHeader, "150px", "Item", "1")
                        HeaderTdSetting(tdHeader1, "0", "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeaderFix.Controls.Add(tdHeader)
                        trHeaderFix1.Controls.Add(tdHeader1)
                        trHeaderFix2.Controls.Add(tdHeader2)
                    Case 3
                        HeaderTdSetting(tdHeader, "70px", "Number", "1")
                        Header2TdSetting(tdHeader2, "", "(each)", "1")
                        HeaderTdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 4
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                        HeaderTdSetting(tdHeader, "90", "Weight", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 5
                        Title = "(MJ/" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + " Mat.)"
                        HeaderTdSetting(tdHeader, "", "Energy", "2")
                        Header2TdSetting(tdHeader2, "", Title, "2")
                        Header2TdSetting(tdHeader1, "70px", "Suggested", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 6
                        Header2TdSetting(tdHeader1, "70px", "Preferred", "1")
                        trHeader1.Controls.Add(tdHeader1)
                    Case 7
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + " CO2/" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + " Mat.)"
                        HeaderTdSetting(tdHeader, "", "CO2 Equivalent", "2")
                        Header2TdSetting(tdHeader2, "", Title, "2")
                        Header2TdSetting(tdHeader1, "70px", "Suggested", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 8
                        Header2TdSetting(tdHeader1, "70px", "Preferred", "1")
                        trHeader1.Controls.Add(tdHeader1)

                    Case 9
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE10").ToString() + " Water/" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + " Mat.)"
                        HeaderTdSetting(tdHeader, "", "Water", "2")
                        Header2TdSetting(tdHeader2, "", Title, "2")
                        Header2TdSetting(tdHeader1, "70px", "Suggested", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 10
                        Header2TdSetting(tdHeader1, "70px", "Preferred", "1")
                        trHeader1.Controls.Add(tdHeader1)


                    Case 11
                        Title = "(%)"
                        HeaderTdSetting(tdHeader, "90", "Recycle", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 12
                        Title = "(Unitless)"
                        HeaderTdSetting(tdHeader, "", "Recovery", "2")
                        Header2TdSetting(tdHeader2, "", Title, "2")
                        Header2TdSetting(tdHeader1, "70px", "Suggested", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 13
                        Header2TdSetting(tdHeader1, "70px", "Preferred", "1")
                        trHeader1.Controls.Add(tdHeader1)
                    Case 14
                        Title = "(Unitless)"
                        HeaderTdSetting(tdHeader, "", "Sustainable Materials", "2")
                        Header2TdSetting(tdHeader2, "", Title, "2")
                        Header2TdSetting(tdHeader1, "70px", "Suggested", "1")
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 15
                        Header2TdSetting(tdHeader1, "70px", "Preferred", "1")
                        trHeader1.Controls.Add(tdHeader1)
                    Case 16
                        Title = "(Unitless)"
                        HeaderTdSetting(tdHeader, "", "PC Recycle", "2")
                        Header2TdSetting(tdHeader2, "", Title, "2")
                        Header2TdSetting(tdHeader1, "70px", "Suggested", "1")
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 17
                        Header2TdSetting(tdHeader1, "70px", "Preferred", "1")
                        trHeader1.Controls.Add(tdHeader1)

                    Case 18
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE4").ToString() + ")"
                        HeaderTdSetting(tdHeader, "", "Ship", "2")
                        Header2TdSetting(tdHeader2, "", Title, "2")
                        Header2TdSetting(tdHeader1, "70px", "Suggested", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 19
                        Header2TdSetting(tdHeader1, "70px", "Preferred", "1")
                        trHeader1.Controls.Add(tdHeader1)

                End Select


            Next
            tblComparision.Controls.Add(trHeader)
            tblComparision.Controls.Add(trHeader2)
            tblComparision.Controls.Add(trHeader1)

            tblFixComparision.Controls.Add(trHeaderFix)
            tblFixComparision.Controls.Add(trHeaderFix2)
            tblFixComparision.Controls.Add(trHeaderFix1)


            'Inner 
            For i = 1 To 10
                trInner = New TableRow
                trInnerFix = New TableRow
                For j = 1 To 19
                    tdInner = New TableCell
                    Select Case j
                        Case 1
                            'Layer
                            InnerTdSetting(tdInner, "", "Center")
                            tdInner.Text = "<b>" + i.ToString() + "</b>"
                            trInnerFix.Controls.Add(tdInner)
                        Case 2
                            InnerTdSetting(tdInner, "", "Left")
                            Link = New HyperLink
                            hid = New HiddenField
                            Link.ID = "hypPalDes" + i.ToString()
                            hid.ID = "hidPalid" + i.ToString()
                            Link.Width = 150
                            Link.CssClass = "Link"
                            GetPalletDetails(Link, hid, CInt(ds.Tables(0).Rows(0).Item("PATID" + i.ToString() + "").ToString()), dsPallet)
                            tdInner.Controls.Add(hid)
                            tdInner.Controls.Add(Link)
                            trInnerFix.Controls.Add(tdInner)
                        Case 3
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.ID = "NUM" + i.ToString()
                            txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("NUM" + i.ToString() + "").ToString(), 0)
                            txt.MaxLength = 4
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 4
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Width = 50
                            lbl.Text = FormatNumber(dsSug.Tables(0).Rows(0).Item("WEIGHT" + i.ToString() + "").ToString(), 2)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 5
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Width = 50
                            lbl.Text = FormatNumber(dsSug.Tables(0).Rows(0).Item("ERGY" + i.ToString() + "").ToString(), 2)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 6
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.ID = "ERGY" + i.ToString()
                            txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("ERGY" + i.ToString() + "").ToString(), 3)
                            txt.MaxLength = 4
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 7
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Width = 50
                            lbl.Text = FormatNumber(dsSug.Tables(0).Rows(0).Item("GHG" + i.ToString() + "").ToString(), 2)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 8
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.ID = "GHG" + i.ToString()
                            txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("GHG" + i.ToString() + "").ToString(), 3)
                            txt.MaxLength = 4
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 9
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Width = 50
                            lbl.Text = FormatNumber(dsSug.Tables(0).Rows(0).Item("WATER" + i.ToString() + "").ToString(), 2)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 10
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.ID = "WATER" + i.ToString()
                            txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("WATER" + i.ToString() + "").ToString(), 3)
                            txt.MaxLength = 4
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 11
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.ID = "RECYCLE" + i.ToString()
                            txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("RECYCLE" + i.ToString() + "").ToString(), 3)
                            txt.MaxLength = 4
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 12
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Width = 50
                            lbl.Text = FormatNumber(dsSug.Tables(0).Rows(0).Item("RECO" + i.ToString() + "").ToString(), 0)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 13
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.ID = "RECO" + i.ToString()
                            txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("RECO" + i.ToString() + "").ToString(), 3)
                            txt.MaxLength = 4
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 14
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Width = 50
                            lbl.Text = dsSug.Tables(0).Rows(0).Item("OSHA" + i.ToString() + "").ToString()
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 15
                            InnerTdSetting(tdInner, "", "Left")
                            Link = New HyperLink
                            hid = New HiddenField
                            Link.ID = "hypSusMat" + i.ToString()
                            hid.ID = "hidSusMatId" + i.ToString()
                            Link.Width = 70
                            Link.CssClass = "Link"
                            GetSustainMaterialDetails(Link, hid, ds.Tables(0).Rows(0).Item("OSHA" + i.ToString() + "").ToString())
                            tdInner.Controls.Add(hid)
                            tdInner.Controls.Add(Link)
                            trInner.Controls.Add(tdInner)
                        Case 16
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Width = 50
                            lbl.Text = FormatNumber(dsSug.Tables(0).Rows(0).Item("PC" + i.ToString() + "").ToString(), 2)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 17
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.ID = "PC" + i.ToString()
                            txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("PC" + i.ToString() + "").ToString(), 3)
                            txt.MaxLength = 4
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 18
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Width = 50
                            lbl.Text = FormatNumber(dsSug.Tables(0).Rows(0).Item("SHIP" + i.ToString() + "").ToString(), 0)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 19
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.ID = "SHIP" + i.ToString()
                            txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("SHIP" + i.ToString() + "").ToString(), 3)
                            txt.MaxLength = 4
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)

                    End Select
                Next
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                    trInnerFix.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                    trInnerFix.CssClass = "AlterNateColor2"
                End If
                trInnerFix.Height = 30
                trInner.Height = 30
                tblComparision.Controls.Add(trInner)
                tblFixComparision.Controls.Add(trInnerFix)
            Next



            'Total
            trInner = New TableRow
            trInnerFix = New TableRow
            For j = 1 To 19
                tdInner = New TableCell
                Select Case j
                    Case 1
                        'Layer
                        InnerTdSetting(tdInner, "", "Center")
                        tdInner.Text = "<b>Total</b>"
                        trInnerFix.Controls.Add(tdInner)
                    Case 2
                        InnerTdSetting(tdInner, "", "Center")
                        tdInner.Text = ""
                        trInnerFix.Controls.Add(tdInner)
                    Case 3
                        InnerTdSetting(tdInner, "", "Center")
                        tdInner.Text = ""
                        trInner.Controls.Add(tdInner)
                    Case 4
                        InnerTdSetting(tdInner, "", "Right")
                        lbl = New Label
                        lbl.Width = 50
                        lbl.Text = FormatNumber(dsTotal.Tables(0).Rows(0).Item("WEIGHT").ToString(), 3)
                        tdInner.Controls.Add(lbl)
                        trInner.Controls.Add(tdInner)
                    Case 5
                        InnerTdSetting(tdInner, "", "Center")
                        lbl = New Label
                        tdInner.ColumnSpan = 2
                        lbl.Text = FormatNumber(dsTotal.Tables(0).Rows(0).Item("ERGY").ToString(), 3)
                        tdInner.Controls.Add(lbl)
                        trInner.Controls.Add(tdInner)
                    Case 6

                    Case 7
                        InnerTdSetting(tdInner, "", "Center")
                        lbl = New Label
                        tdInner.ColumnSpan = 2
                        lbl.Text = FormatNumber(dsTotal.Tables(0).Rows(0).Item("GHG").ToString(), 3)
                        tdInner.Controls.Add(lbl)
                        trInner.Controls.Add(tdInner)
                    Case 8
                    Case 9
                        InnerTdSetting(tdInner, "", "Center")
                        lbl = New Label
                        tdInner.ColumnSpan = 2
                        lbl.Text = FormatNumber(dsTotal.Tables(0).Rows(0).Item("WATER").ToString(), 3)
                        tdInner.Controls.Add(lbl)
                        trInner.Controls.Add(tdInner)
                    Case 10
                    Case 11
                        InnerTdSetting(tdInner, "", "Center")
                        tdInner.Text = ""
                        trInner.Controls.Add(tdInner)
                    Case 12
                        InnerTdSetting(tdInner, "", "Center")
                        lbl = New Label
                        tdInner.ColumnSpan = 2
                        lbl.Text = FormatNumber(dsTotal.Tables(0).Rows(0).Item("RECO").ToString(), 3)
                        tdInner.Controls.Add(lbl)
                        trInner.Controls.Add(tdInner)
                    Case 13

                    Case 14
                        InnerTdSetting(tdInner, "", "Center")
                        lbl = New Label
                        tdInner.ColumnSpan = 2
                        lbl.Text = FormatNumber(dsTotal.Tables(0).Rows(0).Item("OSHA").ToString(), 3)
                        tdInner.Controls.Add(lbl)
                        trInner.Controls.Add(tdInner)
                    Case 15

                    Case 16
                        InnerTdSetting(tdInner, "", "Center")
                        lbl = New Label
                        tdInner.ColumnSpan = 2
                        lbl.Text = FormatNumber(dsTotal.Tables(0).Rows(0).Item("PC").ToString(), 3)
                        tdInner.Controls.Add(lbl)
                        trInner.Controls.Add(tdInner)
                    Case 17

                    Case 18
                        InnerTdSetting(tdInner, "", "Center")
                        lbl = New Label
                        tdInner.ColumnSpan = 2
                        lbl.Text = FormatNumber(dsTotal.Tables(0).Rows(0).Item("SD").ToString(), 3)
                        tdInner.Controls.Add(lbl)
                        trInner.Controls.Add(tdInner)
                    Case 19

                End Select

                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                    trInnerFix.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                    trInnerFix.CssClass = "AlterNateColor2"
                End If
                trInnerFix.Height = 20
                trInner.Height = 20
                tblComparision.Controls.Add(trInner)
                tblFixComparision.Controls.Add(trInnerFix)
            Next

            'Transport Unit Cube 
            For i = 1 To 1
                trInner = New TableRow
                For j = 1 To 2
                    tdInner = New TableCell

                    Select Case j
                        Case 1
                            'Layer
                            InnerTdSetting(tdInner, "", "Center")
                            tdInner.Text = "<b> Transport Unit Cube  </b>"
                            trInner.Controls.Add(tdInner)
                        Case 2
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.ID = "Tuc"
                            txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("TRANSUNITCUBE").ToString(), 3)
                            txt.MaxLength = 9
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                    End Select
                Next
                trInner.CssClass = "AlterNateColor1"
                trInner.Height = 30
                tblComparision1.Controls.Add(trInner)
            Next






        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
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
            Td.Style.Add("font-size", "13px")
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

    Protected Sub GetPalletDetails(ByRef LinkMat As HyperLink, ByVal hid As HiddenField, ByVal palletId As Integer, ByVal dsPallet As DataSet)
        Dim Ds As New DataSet
        Dim ObjGetdata As New S1GetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty
        Dim dvPallet As New DataView
        Dim dtPallet As New DataTable
        Try
            'Ds = ObjGetdata.GetPallets(palletId, "", "")
            dvPallet = dsPallet.Tables(0).DefaultView '
            dvPallet.RowFilter = "PALLETID= " + palletId.ToString() '
            dtPallet = dvPallet.ToTable() '

            LinkMat.Text = dtPallet.Rows(0).Item("PalletDes").ToString()
            hid.Value = palletId.ToString()
            Path = "../PopUp/GetPalletPopUp.aspx?Des=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + LinkMat.ClientID + "&Id=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + hid.ClientID + ""
            LinkMat.NavigateUrl = "javascript:ShowPopWindow('" + Path + "')"

        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetDeptDetails(ByRef LinkDep As HyperLink, ByVal hid As HiddenField, ByVal ProcId As Integer)
        Dim Ds As New DataSet
        Dim ObjGetdata As New S1GetData.Selectdata()
        Dim Path As String = String.Empty
        Try
            Ds = ObjGetdata.GetDept(ProcId, "", "")
            Path = "../PopUp/GetDepPopUp.aspx?Des=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + LinkDep.ClientID + "&Id=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + hid.ClientID + ""
            If Ds.Tables(0).Rows.Count = 0 Then
                LinkDep.Text = "Dept. Conflict"
                LinkDep.ForeColor = Drawing.Color.DarkRed
            Else
                LinkDep.Text = Ds.Tables(0).Rows(0).Item("PROCDE").ToString()
            End If

            hid.Value = ProcId.ToString()
            LinkDep.NavigateUrl = "javascript:ShowPopWindow('" + Path + "')"
        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetSustainMaterialDetails(ByRef LinkMat As HyperLink, ByVal hid As HiddenField, ByVal MatDes As String)
        Dim Ds As New DataSet
        Dim ObjGetdata As New S1GetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty

        Try
            If MatDes.Trim().Length > 0 Then
                LinkMat.Text = MatDes.ToString()
                LinkMat.ToolTip = MatDes.ToString()
                hid.Value = MatDes.ToString()
            Else
                LinkMat.Text = "Nothing"
                LinkMat.ToolTip = "Nothing"
                hid.Value = "Nothing"
            End If
            LinkMat.Attributes.Add("text-decoration", "none")
          
            Path = "../PopUp/GetSusMatPopUp.aspx?Des=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + LinkMat.ClientID + "&Id=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + hid.ClientID + ""
            LinkMat.NavigateUrl = "javascript:ShowPopWindow('" + Path + "')"

        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub CreateTab()
        Dim tr As New TableRow
        Dim td As New TableCell
        Dim i As New Integer
        Dim lnk As New LinkButton
        Try
            tblTab.Rows.Clear()
            For i = 1 To 10
                td = New TableCell
                lnk = New LinkButton
                lnk.Text = "Material " + i.ToString()
                lnk.CssClass = "TabLink"
                lnk.ID = "lnkMat" + i.ToString()
                lnk.Attributes.Add("onclick", "return CheckForRawMatPage('" + ctlContentPlaceHolder.ClientID + "','hidPalid','NUM')")
                AddHandler lnk.Click, AddressOf lnkmat_Click
                If i = Convert.ToInt32(ViewState("TabId").ToString()) Then
                    td.CssClass = "AlterNateTab1"
                Else
                    td.CssClass = "AlterNateTab2"
                End If
                td.Controls.Add(lnk)
                tr.Controls.Add(td)
            Next
            tblTab.Controls.Add(tr)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkmat_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim lnk As LinkButton = DirectCast(sender, System.Web.UI.WebControls.LinkButton)
            UpdatePage()
            ViewState("TabId") = Convert.ToInt32(lnk.ID.Replace("lnkMat", "").ToString())
            CreateTab()
            GetPageDetails()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Update_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
    
        Try
            If Not objRefresh.IsRefresh Then
                UpdatePage()

            End If
            GetPageDetails()
        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub UpdatePage()
        Try
            Dim Pallet(10) As String
            Dim Number(10) As String
            Dim Ergy(10) As String
            Dim Ghg(10) As String
            Dim Water(10) As String
            Dim Recycle(10) As String
            Dim Reco(10) As String
            Dim Osha(10) As String
            Dim Pc(10) As String
            Dim Sd(10) As String
            Dim Tuc As String
            Dim i As New Integer
            Dim ObjUpIns As New S1UpInsData.UpdateInsert()
            Dim obj As New CryptoHelper
            Dim Col As String = String.Empty
            Col = Chr(64 + Convert.ToInt32(ViewState("TabId"))).ToString()
            For i = 1 To 10
                Pallet(i) = Request.Form("ctl00$Sustain1ContentPlaceHolder$hidPalid" + i.ToString() + "")
                Number(i) = Request.Form("ctl00$Sustain1ContentPlaceHolder$NUM" + i.ToString() + "")
                Ergy(i) = Request.Form("ctl00$Sustain1ContentPlaceHolder$ERGY" + i.ToString() + "")
                Ghg(i) = Request.Form("ctl00$Sustain1ContentPlaceHolder$GHG" + i.ToString() + "")
                Water(i) = Request.Form("ctl00$Sustain1ContentPlaceHolder$Water" + i.ToString() + "")
                Recycle(i) = Request.Form("ctl00$Sustain1ContentPlaceHolder$RECYCLE" + i.ToString() + "")
                Reco(i) = Request.Form("ctl00$Sustain1ContentPlaceHolder$RECO" + i.ToString() + "")
                Osha(i) = Request.Form("ctl00$Sustain1ContentPlaceHolder$hidSusMatId" + i.ToString() + "")
                Pc(i) = Request.Form("ctl00$Sustain1ContentPlaceHolder$PC" + i.ToString() + "")
                Sd(i) = Request.Form("ctl00$Sustain1ContentPlaceHolder$SHIP" + i.ToString() + "")

                'Check For IsNumric
                If Not IsNumeric(Number(i)) Then
                    Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                End If
                If Not IsNumeric(Ergy(i)) Then
                    Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                End If
                If Not IsNumeric(Ghg(i)) Then
                    Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                End If
                If Not IsNumeric(Water(i)) Then
                    Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                End If
                If Not IsNumeric(Recycle(i)) Then
                    Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                End If
                If Not IsNumeric(Reco(i)) Then
                    Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                End If
                If Not IsNumeric(Pc(i)) Then
                    Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                End If
                If Not IsNumeric(Sd(i)) Then
                    Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                End If

                'Check For Dependant-Indepdant Error
                If CInt(Pallet(i)) <> 0 Then
                    'Checking Number
                    If CDbl(Number(i)) <= CDbl(0.0) Then
                        Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE105").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                    End If
                End If



            Next
            Tuc = Request.Form("ctl00$Sustain1ContentPlaceHolder$Tuc")
            'Check for isNumeric
            If Not IsNumeric(Tuc) Then
                Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
            End If
            ObjUpIns.RwMaterialPackUpdate(CaseId, Col, Pallet, Number, Ergy, Ghg, Recycle, Reco, Osha, Pc, Sd, Tuc, Water)
            Calculate()
            'Update Server Date
            ObjUpIns.ServerDateUpdate(CaseId, Session("S1UserName"))
        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
    Protected Sub Calculate()
        Try
            Dim Sustain1Conn As String = System.Configuration.ConfigurationManager.AppSettings("Sustain1ConnectionString")
            Dim Econ1Conn As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            Dim obj As New SustainCalculation.SustainCalculations(Sustain1Conn, Econ1Conn)
            obj.SustainCalculate(CaseId)
        Catch ex As Exception
            ErrorLable.Text = "Error:Calculate:" + ex.Message.ToString() + ""
        End Try
    End Sub



End Class
