Imports System.Data
Imports System.Data.OleDb
Imports System
Imports E1GetData
Imports E1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Econ1_Assumptions_DepreciationCost
    Inherits System.Web.UI.Page
    Public CaseDes As String = String.Empty
    Public investCost(30) As String
    Public investCostSE(30) As String
 
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
    Dim _btnResults As ImageButton
    Public Property Resultsbtn() As ImageButton
        Get
            Return _btnResults
        End Get
        Set(ByVal value As ImageButton)
            _btnResults = value
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
        Updatebtn.Attributes.Add("onclick", "return checkNumericAll();")
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
        Notesbtn.OnClientClick = "return Notes('PROCEQUIP');"
        'AddHandler Updatebtn.Click, AddressOf Update_Click
    End Sub

    Protected Sub GetMainHeadingdiv()
        MainHeading = Page.Master.FindControl("divMainHeading")
        MainHeading.Attributes.Add("onmouseover", "Tip('Depreciation Assumptions')")
        MainHeading.Attributes.Add("onmouseout", "UnTip()")
        MainHeading.InnerHtml = "Econ1 - Depreciation Assumptions"
    End Sub

    Protected Sub GetContentPlaceHolder()
        ctlContentPlaceHolder = Page.Master.FindControl("Econ1ContentPlaceHolder")
    End Sub

#End Region

#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_PAGES_ECON1_ASSUMPTIONS_DEPRECIATIONCOST")
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        objRefresh.Render(Page)
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("INTUSR") = "Y" Then
                lnkSelBulkModel.Visible = True
            End If
            GetMasterPageControls()
            GetSessionDetails()
            If Not IsPostBack Then
                Session("EQE1_ICOST") = Nothing
                Session("SEQE1_ICOST") = Nothing
                GetPageDetails()
            End If
        Catch ex As Exception
            ErrorLable.Text = "Error:Page_Load:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetSessionDetails()
        Try
            UserId = Session("UserId")
            CaseId = Session("E1CaseId")
            UserRole = Session("E1UserRole")
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetSessionDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetPageDetails()
        Dim ds As New DataSet
        Dim dsSE As New DataSet
        Dim objGetData As New E1GetData.Selectdata
        Dim i As New Integer
        Dim j As New Integer
        Dim k As New Integer
        Dim DWidth As String = String.Empty
        Dim trHeader As New TableRow
        Dim trHeader1 As New TableRow
        Dim trHeader2 As New TableRow
        Dim trInner As New TableRow
        Dim trInner1 As New TableRow
        Dim tdHeader As TableCell
        Dim tdHeader1 As TableCell
        Dim tdHeader2 As TableCell
        Dim lbl As New Label
        Dim hid As New HiddenField
        Dim hid1 As New HiddenField
        Dim Link As New HyperLink
        Dim txt As New TextBox
        Dim tdInner As TableCell
        Dim tdInner1 As TableCell
        Dim dsOp As New DataSet()
        Dim q As Integer
        Dim c As Integer = 0
        Dim countB As Integer = 0
        Dim countP As Integer = 0

        Try
            ds = objGetData.GetAssetC(CaseId)
            dsSE = objGetData.GetAssetS(CaseId)
            For i = 1 To 30
                If ds.Tables(0).Rows(0).Item("EQUIPDES" + i.ToString()).ToString() <> "Nothing Selected" Then
                    countP = i
                End If
            Next
            For i = 1 To 30
                If dsSE.Tables(0).Rows(0).Item("EQUIPDES" + i.ToString()).ToString() <> "Nothing Selected" Then
                    countB = i
                End If
            Next
            Session("EQE1_ICOST") = ds
            Session("SEQE1_ICOST") = dsSE
           
            For i = 1 To 8
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                Dim Title As String = String.Empty
                'Header Process Equip
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "20px", "", "1")
                        Header2TdSetting(tdHeader1, "0", "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 2
                        HeaderTdSetting(tdHeader, "190px", "Process Equipment", "1")
                        Header2TdSetting(tdHeader1, "0", "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 3
                        Title = "(" + ds.Tables(0).Rows(0).Item("ASSESTCOSTUNIT").ToString() + ")"
                        HeaderTdSetting(tdHeader, "", "Asset Cost", "2")
                        Header2TdSetting(tdHeader2, "", Title, "2")
                        Header2TdSetting(tdHeader1, "76px", "Suggested", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 4
                        Header2TdSetting(tdHeader1, "76px", "Preferred", "1")
                    Case 5
                        HeaderTdSetting(tdHeader, "100px", "Number of Assets", "1")
                        Header2TdSetting(tdHeader1, "0", "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 6
                        Title = "(" + ds.Tables(0).Rows(0).Item("ASSESTCOSTUNIT").ToString() + ")"
                        HeaderTdSetting(tdHeader, "100px", "Investment Cost", "1")
                        Header2TdSetting(tdHeader1, "0", "", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 7
                        HeaderTdSetting(tdHeader, "100px", "Depreciation Period", "1")
                        Dim tipText As String = "A special function provides the capability to make depreciation zero by inputting a zero in the " + """" + "Depreciation Period" + """" + " column text boxes."
                        tdHeader.Attributes.Add("onmouseover", "Tip('" + tipText + "')")
                        tdHeader.Attributes.Add("onmouseout", "UnTip('')")
                        Header2TdSetting(tdHeader1, "0", "", "1")
                        Header2TdSetting(tdHeader2, "", "(years)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 8
                        Title = "(" + ds.Tables(0).Rows(0).Item("ASSESTCOSTUNIT").ToString() + ")"
                        HeaderTdSetting(tdHeader, "120px", "Depreciation Cost", "1")
                        Dim tipText As String = "A special function provides the capability to make depreciation zero by inputting a zero in the " + """" + "Depreciation Period" + """" + " column text boxes."
                        tdHeader.Attributes.Add("onmouseover", "Tip('" + tipText + "')")
                        tdHeader.Attributes.Add("onmouseout", "UnTip('')")
                        Header2TdSetting(tdHeader1, "0", "", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)

                End Select
                trHeader1.Controls.Add(tdHeader1)

            Next
            tblComparision.Controls.Add(trHeader)
            tblComparision.Controls.Add(trHeader2)
            tblComparision.Controls.Add(trHeader1)
           
            'Inner Process Equip
            For i = 1 To countP
                trInner = New TableRow
                For j = 1 To 8
                    tdInner = New TableCell
                    Select Case j
                        Case 1
                            'Layer
                            InnerTdSetting(tdInner, "", "Center")
                            tdInner.Text = "<b>" + i.ToString() + "</b>"
                            trInner.Controls.Add(tdInner)
                        Case 2
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            If ds.Tables(0).Rows(0).Item("LEQUIPDES" + i.ToString() + "").ToString() <> "" Then
                                lbl.Text = ds.Tables(0).Rows(0).Item("LEQUIPDES" + i.ToString() + "").ToString()
                            Else
                                lbl.Text = ds.Tables(0).Rows(0).Item("EQUIPDES" + i.ToString() + "").ToString()
                            End If
                            'lbl.Text = ds.Tables(0).Rows(0).Item("EQUIPDES" + i.ToString() + "").ToString()
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 3
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("ASSETS" + i.ToString() + "").ToString(), 0)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 4
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Width = 70
                            If (ds.Tables(0).Rows(0).Item("ASSETP" + i.ToString() + "").ToString() <> "") Then
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("ASSETP" + i.ToString() + "").ToString(), 0)
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 5
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            If (ds.Tables(0).Rows(0).Item("NUM" + i.ToString() + "").ToString() <> "") Then
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("NUM" + i.ToString() + "").ToString(), 0)
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 6
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            If (ds.Tables(0).Rows(0).Item("ICPE" + i.ToString() + "").ToString() <> "") Then
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("ICPE" + i.ToString() + "").ToString(), 0)
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 7
                            InnerTdSetting(tdInner, "", "Right")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.ID = "DEPRE" + i.ToString()
                            txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("DEPRE" + i.ToString() + "").ToString(), 0)
                            txt.MaxLength = 6
                            If ds.Tables(0).Rows(0).Item("EQUIPDES" + i.ToString()).ToString() <> "Nothing Selected" Then
                                tdInner.Controls.Add(txt)
                                trInner.Controls.Add(tdInner)
                            Else
                                txt.Enabled = False
                                tdInner.Controls.Add(txt)
                                trInner.Controls.Add(tdInner)
                            End If
                           
                        Case 8
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            If (ds.Tables(0).Rows(0).Item("DEPRE" + i.ToString() + "").ToString() <> "") Then
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("DCPE" + i.ToString() + "").ToString(), 0)
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                    End Select
                Next

                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
                tblComparision.Controls.Add(trInner)
            Next

            'Total For Process Equip
            trInner = New TableRow
            For k = 1 To 8
                tdInner = New TableCell
                Select Case k
                    Case 1
                        InnerTdSetting(tdInner, "", "Left")
                        tdInner.Text = "<span class='CalculatedFeilds'><b> Total </b></span>"
                        trInner.Controls.Add(tdInner)
                    Case 6
                        InnerTdSetting(tdInner, "", "Right")
                        tdInner.Text = "<span class='CalculatedFeilds'><b>" + FormatNumber(ds.Tables(0).Rows(0).Item("INVESTTOTALPE").ToString(), 0) + " </b> </span>"
                        trInner.Controls.Add(tdInner)
                    Case 8
                        InnerTdSetting(tdInner, "", "Right")
                        lbl = New Label
                        lbl.Text = "<span class='CalculatedFeilds'><b>" + FormatNumber(ds.Tables(0).Rows(0).Item("DEPRETOTALPE").ToString(), 0) + " </b> </span>"
                        tdInner.Controls.Add(lbl)
                        trInner.Controls.Add(tdInner)
                    Case 2, 3, 4, 5, 7
                        InnerTdSetting(tdInner, "", "Left")
                        trInner.Controls.Add(tdInner)
                End Select
                trInner.Height = 30
                trInner.CssClass = "AlterNateColor2"
                tblComparision.Controls.Add(trInner)
            Next

            'Header Support Equip
            Dim p As Integer
            trHeader = New TableRow
            trHeader1 = New TableRow
            trHeader2 = New TableRow
            For p = 1 To 8
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case p
                    Case 1
                        HeaderTdSetting(tdHeader, "20px", "", "1")
                        Header2TdSetting(tdHeader1, "0", "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 2
                        HeaderTdSetting(tdHeader, "190px", "Support Equipment", "1")
                        Header2TdSetting(tdHeader1, "0", "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 3
                        Title = "(" + ds.Tables(0).Rows(0).Item("ASSESTCOSTUNIT").ToString() + ")"
                        HeaderTdSetting(tdHeader, "", "Asset Cost", "2")
                        Header2TdSetting(tdHeader2, "", Title, "2")
                        Header2TdSetting(tdHeader1, "76px", "Suggested", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 4
                        Header2TdSetting(tdHeader1, "76px", "Preferred", "1")
                    Case 5
                        HeaderTdSetting(tdHeader, "100px", "Number of Assets", "1")
                        Header2TdSetting(tdHeader1, "0", "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 6
                        Title = "(" + ds.Tables(0).Rows(0).Item("ASSESTCOSTUNIT").ToString() + ")"
                        HeaderTdSetting(tdHeader, "100px", "Investment Cost", "1")
                        Header2TdSetting(tdHeader1, "0", "", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 7
                        HeaderTdSetting(tdHeader, "100px", "Depreciation Period", "1")
                        Dim tipText As String = "A special function provides the capability to make depreciation zero by inputting a zero in the " + """" + "Depreciation Period" + """" + " column text boxes."
                        tdHeader.Attributes.Add("onmouseover", "Tip('" + tipText + "')")
                        tdHeader.Attributes.Add("onmouseout", "UnTip('')")
                        Header2TdSetting(tdHeader1, "0", "", "1")
                        Header2TdSetting(tdHeader2, "", "(years)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 8
                        Title = "(" + ds.Tables(0).Rows(0).Item("ASSESTCOSTUNIT").ToString() + ")"
                        HeaderTdSetting(tdHeader, "120px", "Depreciation Cost", "1")
                        Dim tipText As String = "A special function provides the capability to make depreciation zero by inputting a zero in the " + """" + "Depreciation Period" + """" + " column text boxes."
                        tdHeader.Attributes.Add("onmouseover", "Tip('" + tipText + "')")
                        tdHeader.Attributes.Add("onmouseout", "UnTip('')")
                        Header2TdSetting(tdHeader1, "0", "", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)

                End Select
                trHeader1.Controls.Add(tdHeader1)
            Next
            tblSupportEqui.Controls.Add(trHeader)
            tblSupportEqui.Controls.Add(trHeader2)
            tblSupportEqui.Controls.Add(trHeader1)

            'Inner Support Equip
            For i = 1 To countB
                trInner1 = New TableRow
                For j = 1 To 8
                    tdInner1 = New TableCell
                    Select Case j
                        Case 1
                            'Layer
                            InnerTdSetting(tdInner1, "", "Center")
                            tdInner1.Text = "<b>" + i.ToString() + "</b>"
                            trInner1.Controls.Add(tdInner1)
                        Case 2
                            InnerTdSetting(tdInner1, "", "Left")
                            lbl = New Label
                            lbl.Text = dsSE.Tables(0).Rows(0).Item("EQUIPDES" + i.ToString() + "").ToString()
                            tdInner1.Controls.Add(lbl)
                            trInner1.Controls.Add(tdInner1)
                        Case 3
                            InnerTdSetting(tdInner1, "", "Right")
                            lbl = New Label
                            lbl.Text = FormatNumber(dsSE.Tables(0).Rows(0).Item("ASSETS" + i.ToString() + "").ToString(), 0)
                            tdInner1.Controls.Add(lbl)
                            trInner1.Controls.Add(tdInner1)
                        Case 4
                            InnerTdSetting(tdInner1, "", "Right")
                            lbl = New Label
                            lbl.Width = 70
                            If (dsSE.Tables(0).Rows(0).Item("ASSETP" + i.ToString() + "").ToString() <> "") Then
                                lbl.Text = FormatNumber(dsSE.Tables(0).Rows(0).Item("ASSETP" + i.ToString() + "").ToString(), 0)
                            End If
                            tdInner1.Controls.Add(lbl)
                            trInner1.Controls.Add(tdInner1)
                        Case 5
                            InnerTdSetting(tdInner1, "", "Right")
                            lbl = New Label
                            If (dsSE.Tables(0).Rows(0).Item("NUM" + i.ToString() + "").ToString() <> "") Then
                                lbl.Text = FormatNumber(dsSE.Tables(0).Rows(0).Item("NUM" + i.ToString() + "").ToString(), 0)
                            End If
                            tdInner1.Controls.Add(lbl)
                            trInner1.Controls.Add(tdInner1)
                        Case 6
                            InnerTdSetting(tdInner1, "", "Right")
                            lbl = New Label
                            If (dsSE.Tables(0).Rows(0).Item("ICSE" + i.ToString() + "").ToString() <> "") Then
                                lbl.Text = FormatNumber(dsSE.Tables(0).Rows(0).Item("ICSE" + i.ToString() + "").ToString(), 0)
                            End If
                            tdInner1.Controls.Add(lbl)
                            trInner1.Controls.Add(tdInner1)
                        Case 7
                            InnerTdSetting(tdInner1, "", "Right")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.ID = "DEPRES" + i.ToString()
                            txt.Text = FormatNumber(dsSE.Tables(0).Rows(0).Item("DEPRES" + i.ToString() + "").ToString(), 0)
                            txt.MaxLength = 6
                            If dsSE.Tables(0).Rows(0).Item("EQUIPDES" + i.ToString()).ToString() <> "Nothing Selected" Then
                                tdInner1.Controls.Add(txt)
                                trInner1.Controls.Add(tdInner1)
                            Else
                                txt.Enabled = False
                                tdInner1.Controls.Add(txt)
                                trInner1.Controls.Add(tdInner1)
                            End If
                            
                        Case 8
                            InnerTdSetting(tdInner1, "", "Right")
                            lbl = New Label
                            If (dsSE.Tables(0).Rows(0).Item("DEPRES" + i.ToString() + "").ToString() <> "") Then
                                lbl.Text = FormatNumber(dsSE.Tables(0).Rows(0).Item("DCSE" + i.ToString() + "").ToString(), 0)
                            End If
                           tdInner1.Controls.Add(lbl)
                            trInner1.Controls.Add(tdInner1)
                    End Select
                Next
                If (i Mod 2 = 0) Then
                    trInner1.CssClass = "AlterNateColor1"
                Else
                    trInner1.CssClass = "AlterNateColor2"
                End If
                tblSupportEqui.Controls.Add(trInner1)
            Next

            'Total For Support Equip
            trInner1 = New TableRow
            For k = 1 To 8
                tdInner1 = New TableCell
                Select Case k
                    Case 1
                        InnerTdSetting(tdInner1, "", "Left")
                        tdInner1.Text = "<span class='CalculatedFeilds'><b> Total </b></span>"
                        trInner1.Controls.Add(tdInner1)
                    Case 6
                        InnerTdSetting(tdInner1, "", "Right")
                        tdInner1.Text = "<span class='CalculatedFeilds'><b>" + FormatNumber(dsSE.Tables(0).Rows(0).Item("INVESTTOTALSE").ToString(), 0) + " </b> </span>"
                        trInner1.Controls.Add(tdInner1)
                    Case 8
                        InnerTdSetting(tdInner1, "", "Right")
                        lbl = New Label
                        lbl.Text = "<span class='CalculatedFeilds'><b>" + FormatNumber(dsSE.Tables(0).Rows(0).Item("DEPRETOTALSE").ToString(), 0) + " </b> </span>"
                        tdInner1.Controls.Add(lbl)
                        trInner1.Controls.Add(tdInner1)
                    Case 2, 3, 4, 5, 7
                        InnerTdSetting(tdInner1, "", "Left")
                        trInner1.Controls.Add(tdInner1)
                End Select
                trInner1.Height = 30
                trInner1.CssClass = "AlterNateColor2"
                tblSupportEqui.Controls.Add(trInner1)
            Next

            'Grand Total
            Dim investT As Double
            Dim depT As Double
            Dim depC As String
            trInner1 = New TableRow
            For k = 0 To 4
                If k <> 0 Then
                    tdInner1 = New TableCell
                    Select Case k
                        Case 1
                            InnerTdSetting(tdInner1, "478px", "Left")
                            tdInner1.Text = "<span class='CalculatedFeilds'><b>Grand Total </b></span>"
                            trInner1.Controls.Add(tdInner1)
                        Case 2
                            InnerTdSetting(tdInner1, "94px", "Right")
                            lbl = New Label
                            lbl.Text = "<b>" + FormatNumber(ds.Tables(0).Rows(0).Item("ASSETTOTAL").ToString(), 0) + "</b>"
                            tdInner1.Controls.Add(lbl)
                            trInner1.Controls.Add(tdInner1)
                        Case 3
                            InnerTdSetting(tdInner1, "94px", "Left")
                            lbl = New Label
                            lbl.Text = " "
                            tdInner1.Controls.Add(lbl)
                            trInner1.Controls.Add(tdInner1)
                        Case 4
                            InnerTdSetting(tdInner1, "116px", "Right")
                            lbl = New Label
                            lbl.Text = "<b>" + FormatNumber(ds.Tables(0).Rows(0).Item("DEPTOTAL").ToString(), 0) + "</b>"
                            tdInner1.Controls.Add(lbl)
                            trInner1.Controls.Add(tdInner1)
                       
                    End Select
                End If
                trInner1.Height = 30
                trInner1.CssClass = "AlterNateColor2"
                tblGTotal.Controls.Add(trInner1)
            Next
      
        Catch ex As Exception

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

    Protected Sub Header3TdSetting(ByVal Td As TableCell, ByVal Width As String, ByVal HeaderText As String, ByVal Rowspan As String)
        Try
            Td.Text = HeaderText
            Td.RowSpan = Rowspan
            If Width <> "" Then
                Td.Style.Add("width", Width)
            End If
            Td.CssClass = "TdHeading"
            Td.Font.Size = 10
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


    Protected Sub Update_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim DEPRE(30) As String
        Dim DEPRESE(30) As String
        Dim i As New Integer
        Dim ObjUpIns As New E1UpInsData.UpdateInsert()
        Dim obj As New CryptoHelper
        Dim dsEq, dsSEQ As New DataSet
        Try
            dsEq = CType(Session("EQE1_ICOST"), DataSet)
            dsSEQ = CType(Session("SEQE1_ICOST"), DataSet)

            If Not objRefresh.IsRefresh Then
                For i = 1 To 30
                    DEPRE(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$DEPRE" + i.ToString() + "")
                    DEPRESE(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$DEPRES" + i.ToString() + "")
                    If Not IsNumeric(DEPRE(i - 1)) Then
                        If (DEPRE(i - 1) <> "") Then 'ADDED CONDITION FOR BUG#210
                            Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        End If
                    End If
                    If Not IsNumeric(DEPRESE(i - 1)) Then
                        If (DEPRESE(i - 1) <> "") Then 'ADDED CONDITION FOR BUG#210
                            Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        End If
                    End If
                Next
               
                ObjUpIns.DepreciationUpdate(CaseId, DEPRE, DEPRESE)
                Calculate()
                'Update Server Date
                ObjUpIns.ServerDateUpdate(CaseId, Session("E1UserName"))
            End If
            GetPageDetails()
        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub Calculate()
        Try
            Dim Econ1Conn As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            Dim obj As New EconCalculation.EconCalculation(Econ1Conn)
            obj.EconCalculate(CaseId)
        Catch ex As Exception
            ErrorLable.Text = "Error:Calculate:" + ex.Message.ToString() + ""
        End Try
    End Sub

#Region "Bulk Model Management"

    Protected Sub btnUpdateBulk_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpdateBulk.Click
        Dim DEPRE(30) As String
        Dim DEPRESE(30) As String
        Dim i As New Integer
        Dim ObjUpIns As New E1UpInsData.UpdateInsert()
        Dim obj As New CryptoHelper
        Dim dsEq, dsSEQ As New DataSet
        Try
            dsEq = CType(Session("EQE1_ICOST"), DataSet)
            dsSEQ = CType(Session("SEQE1_ICOST"), DataSet)

            For i = 1 To 30
                DEPRE(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$DEPRE" + i.ToString() + "")
                DEPRESE(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$DEPRES" + i.ToString() + "")
                If Not IsNumeric(DEPRE(i - 1)) Then
                    If (DEPRE(i - 1) <> "") Then 'ADDED CONDITION FOR BUG#210
                        Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                    End If
                End If
                If Not IsNumeric(DEPRESE(i - 1)) Then
                    If (DEPRESE(i - 1) <> "") Then 'ADDED CONDITION FOR BUG#210
                        Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                    End If
                End If
            Next

            'Start Updating Bulk Model
            Dim str As String
            Dim strArr() As String
            Dim count As Integer
            Dim BCaseId As Integer
            str = Session("CaseIdString")
            If str <> "" Then
                strArr = str.Split(",")
                For count = 0 To strArr.Length - 1
                    BCaseId = strArr(count)
                    Try
                        ObjUpIns.DepreciationUpdate(BCaseId, DEPRE, DEPRESE)
                        'Update Server Date
                        ObjUpIns.ServerDateUpdate(BCaseId, Session("E1UserName"))
                    Catch ex As Exception
                    End Try
                Next
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alertMsg", "alert('Variables transferred successfully.');", True)
                'ScriptManager.RegisterStartupScript(Page, Me.GetType(), "alertMsg", "alert('Values successfully transfered.');", True)                
            Else
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alertMsg", "alert('Something went wrong! No model found for transfer.');", True)
            End If
            loading.Style.Add("display", "none")
            lnkSelBulkModel.Visible = True
            'End Updating Bulk Model       
            GetPageDetails()
        Catch ex As Exception
            ErrorLable.Text = "Error:btnUpdateBulk_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnUpdateBulk1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpdateBulk1.Click
        Dim DEPRE(30) As String
        Dim DEPRESE(30) As String
        Dim i As New Integer
        Dim ObjUpIns As New E1UpInsData.UpdateInsert()
        Dim obj As New CryptoHelper
        Dim dsEq, dsSEQ As New DataSet
        Try
            dsEq = CType(Session("EQE1_ICOST"), DataSet)
            dsSEQ = CType(Session("SEQE1_ICOST"), DataSet)

            For i = 1 To 30
                DEPRE(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$DEPRE" + i.ToString() + "")
                DEPRESE(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$DEPRES" + i.ToString() + "")
                If Not IsNumeric(DEPRE(i - 1)) Then
                    If (DEPRE(i - 1) <> "") Then 'ADDED CONDITION FOR BUG#210
                        Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                    End If
                End If
                If Not IsNumeric(DEPRESE(i - 1)) Then
                    If (DEPRESE(i - 1) <> "") Then 'ADDED CONDITION FOR BUG#210
                        Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                    End If
                End If
            Next

            'Start Updating Bulk Model
            Dim str As String
            Dim strArr() As String
            Dim count As Integer
            Dim BCaseId As Integer
            str = Session("CaseIdString")
            If str <> "" Then
                strArr = str.Split(",")
                For count = 0 To strArr.Length - 1
                    BCaseId = strArr(count)
                    Try
                        ObjUpIns.DepreciationUpdate(BCaseId, DEPRE, DEPRESE)
                        Calculate_Bulk(BCaseId)
                        'Update Server Date
                        ObjUpIns.ServerDateUpdate(BCaseId, Session("E1UserName"))
                    Catch ex As Exception
                    End Try
                Next
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alertMsg", "alert('Variables transferred successfully.');", True)
                'ScriptManager.RegisterStartupScript(Page, Me.GetType(), "alertMsg", "alert('Values successfully transfered.');", True)                
            Else
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alertMsg", "alert('Something went wrong! No model found for transfer.');", True)
            End If
            loading.Style.Add("display", "none")
            lnkSelBulkModel.Visible = True
            'End Updating Bulk Model 
            GetPageDetails()
        Catch ex As Exception
            ErrorLable.Text = "Error:btnUpdateBulk1_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub Calculate_Bulk(ByVal BCaseID As Integer)
        Try
            Dim Econ1Conn As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            Dim obj As New EconCalculation.EconCalculation(Econ1Conn)
            obj.EconCalculate(BCaseID)
        Catch ex As Exception
            ErrorLable.Text = "Error:Calculate_Bulk:" + ex.Message.ToString() + ""
        End Try
    End Sub

#End Region

End Class
