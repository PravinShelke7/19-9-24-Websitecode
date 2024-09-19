Imports System.Data
Imports System.Data.OleDb
Imports System
Imports E1GetData
Imports E1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Econ1_Results_Result
    Inherits System.Web.UI.Page

    Public CaseDes As String = String.Empty

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
    Dim _btnCompare As ImageButton
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

    Public Property Comparebtn() As ImageButton
        Get
            Return _btnCompare
        End Get
        Set(ByVal value As ImageButton)
            _btnCompare = value
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
        GetComparebtn()
        GetMainHeadingdiv()
    End Sub

    Protected Sub GetErrorLable()
        ErrorLable = Page.Master.FindControl("lblError")
    End Sub

    Protected Sub GetUpdatebtn()
        Updatebtn = Page.Master.FindControl("imgUpdate")
        Updatebtn.Visible = True
        'Updatebtn.Attributes.Add("onclick", "return checkNumericAll()")
        Updatebtn.Attributes.Add("onclick", "return CheckResultpl('" + ctlContentPlaceHolder.ClientID + "','UNITPP','UNITPP2')")

        AddHandler Updatebtn.Click, AddressOf Update_Click
    End Sub

    Protected Sub GetLogOffbtn()
        LogOffbtn = Page.Master.FindControl("imgLogoff")
        LogOffbtn.Visible = False
        'AddHandler Updatebtn.Click, AddressOf Update_Click
    End Sub

    Protected Sub GetInstructionsbtn()
        Instrutionsbtn = Page.Master.FindControl("imgInstructions")
        Instrutionsbtn.Visible = False
        'AddHandler Updatebtn.Click, AddressOf Update_Click
    End Sub

    Protected Sub GetChartbtn()
        Chartbtn = Page.Master.FindControl("imgChart")
        Chartbtn.Visible = False
        'AddHandler Updatebtn.Click, AddressOf Update_Click
    End Sub

    Protected Sub GetFeedbackbtn()
        FeedBackbtn = Page.Master.FindControl("imgFeedback")
        FeedBackbtn.Visible = False
        'AddHandler Updatebtn.Click, AddressOf Update_Click
    End Sub

    Protected Sub GetNotesbtn()
        Notesbtn = Page.Master.FindControl("imgNotes")
        Notesbtn.Visible = False
        'AddHandler Updatebtn.Click, AddressOf Update_Click
    End Sub

    Protected Sub GetComparebtn()
        Dim obj As New CryptoHelper()
        Comparebtn = Page.Master.FindControl("imgCompare")
        Comparebtn.Visible = False
        Comparebtn.OnClientClick = "return Compare('Comparision.aspx?Type=" + obj.Encrypt("PL") + "') "

        'AddHandler Updatebtn.Click, AddressOf Update_Click
    End Sub

    Protected Sub GetMainHeadingdiv()
        MainHeading = Page.Master.FindControl("divMainHeading")
        MainHeading.Attributes.Add("onmouseover", "Tip('Direct Materials with Depreciation')")
        MainHeading.Attributes.Add("onmouseout", "UnTip()")
        MainHeading.InnerHtml = "Econ1 - Direct Materials with Depreciation"
    End Sub

    Protected Sub GetContentPlaceHolder()
        ctlContentPlaceHolder = Page.Master.FindControl("Econ1ContentPlaceHolder")
    End Sub

#End Region

#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_PAGES_ECON1_RESULTS_RESULTSPL")
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
        Dim objGetData As New E1GetData.Selectdata
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
        Dim ddl As New DropDownList
        Dim strVol As Double = 0.0
        Dim dsPref As New DataSet
        Dim dsSug As New DataSet
        Dim dsEmat As New DataSet
        Dim dvEmat As New DataView
        Dim DsMat As New DataSet
        Dim DsRes As New DataSet
        Dim DsResult As New DataSet
        Dim DsPrefrence As New DataSet
  Dim ds2 As New DataSet
        Try
            ds = objGetData.GetExtrusionOutDetails(CaseId)
            dsPref = objGetData.GetExtrusionDetailsBarrP(CaseId)
            dsSug = objGetData.GetExtrusionDetailsBarrS(CaseId)
            dsEmat = objGetData.GetEditMaterial(CaseId)
            dvEmat = dsEmat.Tables(0).DefaultView
            Session("dsEmat") = dsEmat
            DsRes = objGetData.GetProfitAndLossDetails(CaseId, False)
            DsMat = objGetData.GetMaterials("-1", "", "")
            DsPrefrence = objGetData.GetPref(CaseId)
            DsResult = objGetData.GetResultDetails(CaseId)

            lblSalesVol.Text = "<b>Sales Volume (" + DsRes.Tables(0).Rows(0).Item("TITLE8").ToString() + "):</b> " + FormatNumber(DsRes.Tables(0).Rows(0).Item("SVOLUME").ToString(), 0).ToString()
            lblSalesVolUnit.Text = "<b>Sales Volume (" + DsRes.Tables(0).Rows(0).Item("SUNITLBL").ToString() + "):</b> " + FormatNumber(DsRes.Tables(0).Rows(0).Item("SUNITVAL").ToString(), 0).ToString()
 ds2 = objGetData.GetProductFromatIn(CaseId)
            If ds2.Tables(0).Rows(0).Item("M1").ToString() = 1 Then
                GetPageDetails2()
            Else
            For i = 1 To 11
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i

                    Case 1
                        HeaderTdSetting(tdHeader, "200px", "Substrate", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        lbl = New Label
                        lbl.Width = 135
                        lbl.Text = ""
                        tdHeader2.Controls.Add(lbl)
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 2
                        Title = "(" + dsPref.Tables(0).Rows(0).Item("TITLE2").ToString() + ")"
                        HeaderTdSetting(tdHeader, "100px", "Total", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 3
                        Title = "(" + dsPref.Tables(0).Rows(0).Item("TITLE1").ToString() + ")"
                        HeaderTdSetting(tdHeader, "100px", "Thickness", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)

                    Case 4
                        HeaderTdSetting(tdHeader, "70px", "Specific Gravity", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)

                    Case 5
                        HeaderTdSetting(tdHeader, "100px", "Contribution", "1")
                        Header2TdSetting(tdHeader2, "", "(% - Materials Wt)", "1")
                        tdHeader2.ToolTip = "% based on Weight based on Direct Materials"
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 6
                        Title = "(" + DsPrefrence.Tables(0).Rows(0).Item("TITLE21").ToString() + "/" + ds.Tables(0).Rows(0).Item("TITLE3").ToString() + ")"
                        HeaderTdSetting(tdHeader, "100px", "Basis Weight", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 7
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE2").ToString() + "/" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                        HeaderTdSetting(tdHeader, "100px", "Market Price", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 8
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE2").ToString() + "/" + DsRes.Tables(0).Rows(0).Item("SUNITLBL").ToString() + ")"
                        HeaderTdSetting(tdHeader, "100px", "Material Cost", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 9
                        'Title = "(" + DsRes.Tables(0).Rows(0).Item("PUN1").ToString() + ")"
                        If DsRes.Tables(0).Rows(0).Item("FINVOLMSI").ToString() > 1 Then
                            Title = "(" + DsRes.Tables(0).Rows(0).Item("PUN").ToString() + ")"
                        Else
                            Title = "(" + DsRes.Tables(0).Rows(0).Item("PUN1").ToString() + ")"
                        End If
                        HeaderTdSetting(tdHeader, "100px", "Material Cost", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 10
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE2").ToString() + "/" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                        HeaderTdSetting(tdHeader, "100px", "Material Cost", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 11
                        Title = "(% - Price Wt)"
                        tdHeader2.ToolTip = "% based on Weight based on Material Price"
                        HeaderTdSetting(tdHeader, "100px", "Material Cost", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                End Select

            Next
            trHeader.Height = 30
            trHeader.Height = 30

            tblComparision.Controls.Add(trHeader)
            tblComparision.Controls.Add(trHeader2)
            tblComparision.Controls.Add(trHeader1)


            'Inner
            For i = 1 To 10
                trInner = New TableRow
                For j = 1 To 11
                    tdInner = New TableCell

                    Select Case j

                        Case 1
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            GetMaterialDetailsNew(lbl, hid, CInt(dsPref.Tables(0).Rows(0).Item("M" + i.ToString() + "").ToString()), "hypGradeDes" + i.ToString(), "hidGradeId" + i.ToString(), "SG" + i.ToString(), DsMat)
                            tdInner.Width = 120
                            tdInner.Height = 20
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 2
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            If ds.Tables(0).Rows(0).Item("PRODZ" + i.ToString() + "").ToString() <> "" Then
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("PRODZ" + i.ToString() + "").ToString(), 0)
                            Else
                                lbl.Text = "0"
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 3
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            If dsPref.Tables(0).Rows(0).Item("THICK" + i.ToString() + "").ToString() <> "" Then
                                lbl.Text = FormatNumber(dsPref.Tables(0).Rows(0).Item("THICK" + i.ToString() + "").ToString(), 3)
                            Else
                                lbl.Text = "0"
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 4
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            If dsPref.Tables(0).Rows(0).Item("SGP" + i.ToString() + "").ToString() <> "0" Then
                                lbl.Text = FormatNumber(dsPref.Tables(0).Rows(0).Item("SGP" + i.ToString() + "").ToString(), 3)
                            Else
                                lbl.Text = FormatNumber(dsSug.Tables(0).Rows(0).Item("SGS" + i.ToString() + "").ToString(), 3)
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 5
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            If ds.Tables(0).Rows(0).Item("P" + i.ToString() + "").ToString() <> "" Then
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("P" + i.ToString() + "").ToString(), 2)
                            Else
                                lbl.Text = "0"
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 6
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            If ds.Tables(0).Rows(0).Item("W" + i.ToString() + "").ToString() <> "" Then
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("W" + i.ToString() + "").ToString() * DsPrefrence.Tables(0).Rows(0).Item("CONVWT5").ToString(), 3)
                            Else
                                lbl.Text = "0.0"
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 7
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label()
                            If (dsPref.Tables(0).Rows(0).Item("PRP" + i.ToString() + "").ToString() <> "0") Then
                                lbl.Text = FormatNumber(dsPref.Tables(0).Rows(0).Item("PRP" + i.ToString() + "").ToString(), 3)
                            Else
                                If dsSug.Tables(0).Rows(0).Item("PRS" + i.ToString() + "").ToString() <> "" Then
                                    lbl.Text = FormatNumber(dsSug.Tables(0).Rows(0).Item("PRS" + i.ToString() + "").ToString(), 3)
                                Else
                                    lbl.Text = "0.00"
                                End If
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 8
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label()
                            If ds.Tables(0).Rows(0).Item("PRODZ" + i.ToString() + "").ToString() <> "" Then
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("PRODZ" + i.ToString() + "").ToString() / DsRes.Tables(0).Rows(0).Item("SUNITVAL").ToString(), 4)
                            Else
                                lbl.Text = "0.00"
                            End If

                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)

                        Case 9
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label()
                            Dim perunit As New Decimal
                            If DsRes.Tables(0).Rows(0).Item("FINVOLMSI").ToString() > 1 Then
                                If ds.Tables(0).Rows(0).Item("PRODZ" + i.ToString() + "").ToString() <> "" Then
                                    If CDbl(DsRes.Tables(0).Rows(0).Item("SUNIT").ToString()) > 0 Then
                                        perunit = CDbl(ds.Tables(0).Rows(0).Item("PRODZ" + i.ToString() + "").ToString()) * CDbl(DsRes.Tables(0).Rows(0).Item("SUNIT").ToString()) / CDbl(DsRes.Tables(0).Rows(0).Item("CURR").ToString())
                                        lbl.Text = FormatNumber(perunit, 4)
                                    Else
                                        lbl.Text = "0"
                                    End If
                                Else
                                    lbl.Text = "0"
                                End If
                            Else
                                If ds.Tables(0).Rows(0).Item("PRODZ" + i.ToString() + "").ToString() <> "" Then
                                    If CDbl(DsRes.Tables(0).Rows(0).Item("SUNIT1").ToString()) > 0 Then
                                        perunit = CDbl(ds.Tables(0).Rows(0).Item("PRODZ" + i.ToString() + "").ToString()) * CDbl(DsRes.Tables(0).Rows(0).Item("SUNIT1").ToString()) / CDbl(DsRes.Tables(0).Rows(0).Item("CURR").ToString())
                                        lbl.Text = FormatNumber(perunit, 4)
                                    Else
                                        lbl.Text = "0"
                                    End If
                                Else
                                    lbl.Text = "0"
                                End If
                            End If

                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 10
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label()
                            If ds.Tables(0).Rows(0).Item("PRODZ" + i.ToString() + "").ToString() <> "" And DsRes.Tables(0).Rows(0).Item("SVOLUME").ToString() <> "0" Then
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("PRODZ" + i.ToString() + "").ToString() / DsRes.Tables(0).Rows(0).Item("SVOLUME").ToString(), 3)
                            Else
                                lbl.Text = "0.000"
                            End If

                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 11
                            InnerTdSetting(tdInner, "", "Right")
                            Dim price As New Decimal
                            Dim perc As New Decimal
                            Dim total As New Decimal
                            lbl = New Label()
                            If ds.Tables(0).Rows(0).Item("PRODZ" + i.ToString() + "").ToString() <> "" And DsRes.Tables(0).Rows(0).Item("SVOLUME").ToString() <> "0" Then
                                price = CDbl(ds.Tables(0).Rows(0).Item("PRODZ" + i.ToString() + "").ToString() / DsRes.Tables(0).Rows(0).Item("SVOLUME").ToString())
                                total = CDbl(DsResult.Tables(0).Rows(0).Item("col87").ToString() / DsRes.Tables(0).Rows(0).Item("SVOLUME").ToString())
                                perc = (price / total) * 100
                                lbl.Text = FormatNumber(perc, 3)
                            Else
                                lbl.Text = "0.00"
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
                trInner.Height = 30
                tblComparision.Controls.Add(trInner)
            Next
           
            'DM
            Dim S1 As Decimal
            Dim S2 As Decimal
            Dim S3 As Decimal
            If DsResult.Tables(0).Rows(0).Item("col82").ToString() <> "" And DsResult.Tables(0).Rows(0).Item("col84").ToString() <> "" Then
                If DsResult.Tables(0).Rows(0).Item("col82").ToString() <> 0 And DsResult.Tables(0).Rows(0).Item("col84").ToString <> 0 Then
                    S1 = FormatNumber(DsResult.Tables(0).Rows(0).Item("col82").ToString() / DsRes.Tables(0).Rows(0).Item("SVOLUME").ToString(), 3)
                    S2 = FormatNumber(DsResult.Tables(0).Rows(0).Item("col84").ToString() / DsRes.Tables(0).Rows(0).Item("SVOLUME").ToString(), 3)
                    S3 = (S1 / S2) * 100
                End If
            End If
            If DsResult.Tables.Count > 0 Then
                For j = 1 To 7
                    trInner = New TableRow
                    For i = 1 To 11
                        tdInner = New TableCell
                        Select Case i
                            Case 1
                                If j = 5 Then
                                    HeaderTrSetting(tdInner, "", "", 2)
                                    tdInner.Text = DsResult.Tables(0).Rows(0).Item("des" + j.ToString()).ToString() '"  Direct Materials "
                                    If j <> 2 Then
                                        tdInner.Text = "<b>" + tdInner.Text + "</b>"
                                    End If
                                    trInner.Controls.Add(tdInner)
                                ElseIf j = 6 Then
                                Else
                                    HeaderTrSetting(tdInner, "", "", 1)
                                    tdInner.Text = DsResult.Tables(0).Rows(0).Item("des" + j.ToString()).ToString()
                                    'If j <> 2 Then
                                    tdInner.Text = "<b>" + tdInner.Text + "</b>"
                                    'End If
                                    trInner.Controls.Add(tdInner)
                                End If
                            Case 2
                                InnerTdSetting(tdInner, "", "Right")
                                If DsResult.Tables(0).Rows(0).Item("col" + (i - 1).ToString() + j.ToString()).ToString() <> "" Then
                                    tdInner.Text = FormatNumber(DsResult.Tables(0).Rows(0).Item("col" + (i - 1).ToString() + j.ToString()).ToString(), 0)
                                End If
                                trInner.Controls.Add(tdInner)
                            Case 3
                                InnerTdSetting(tdInner, "", "Left")
                                If DsResult.Tables(0).Rows(0).Item("col" + (i - 1).ToString() + j.ToString()).ToString() <> "" Then
                                    If DsResult.Tables(0).Rows(0).Item("col" + (i - 1).ToString() + j.ToString()).ToString().Contains("i") Then
                                        tdInner.Text = DsResult.Tables(0).Rows(0).Item("col" + (i - 1).ToString() + j.ToString()).ToString()
                                    Else
                                        tdInner.Text = FormatNumber(DsResult.Tables(0).Rows(0).Item("col" + (i - 1).ToString() + j.ToString()).ToString(), 1)
                                    End If
                                End If
                                trInner.Controls.Add(tdInner)
                            Case 4
                                InnerTdSetting(tdInner, "", "Right")
                                If DsResult.Tables(0).Rows(0).Item("col" + (i - 1).ToString() + j.ToString()).ToString() <> "" Then
                                    tdInner.Text = FormatNumber(DsResult.Tables(0).Rows(0).Item("col" + (i - 1).ToString() + j.ToString()).ToString(), 1)
                                End If
                                tdInner.Text = "<b>" + tdInner.Text + "</b>"
                                trInner.Controls.Add(tdInner)
                            Case 5
                                InnerTdSetting(tdInner, "", "Right")
                                tdInner.ID = "Total" + (i - 1).ToString()
                                If DsResult.Tables(0).Rows(0).Item("col" + (i - 1).ToString() + j.ToString()).ToString() <> "" Then
                                    tdInner.Text = FormatNumber(DsResult.Tables(0).Rows(0).Item("col" + (i - 1).ToString() + j.ToString()).ToString(), 2)
                                End If
                                tdInner.Text = "<b>" + tdInner.Text + "</b>"
                                trInner.Controls.Add(tdInner)
                            Case 6
                                InnerTdSetting(tdInner, "", "Right")
                                If DsResult.Tables(0).Rows(0).Item("col" + (i - 1).ToString() + j.ToString()).ToString() <> "" Then
                                    tdInner.Text = FormatNumber(DsResult.Tables(0).Rows(0).Item("col" + (i - 1).ToString() + j.ToString()).ToString(), 3)
                                End If
                                trInner.Controls.Add(tdInner)
                            Case 7
                                InnerTdSetting(tdInner, "", "Right")
                                If j = 2 Then
                                    tdInner.Text = FormatNumber(S3, 2) + "%"
                                Else
                                    If DsResult.Tables(0).Rows(0).Item("col" + (i - 1).ToString() + j.ToString()).ToString() <> "" Then
                                        tdInner.Text = FormatNumber(DsResult.Tables(0).Rows(0).Item("col" + (i - 1).ToString() + j.ToString()).ToString(), 3)
                                    End If
                                End If
                                trInner.Controls.Add(tdInner)
                            Case 8
                                InnerTdSetting(tdInner, "", "Right")
                                If DsResult.Tables(0).Rows(0).Item("col" + (i - 1).ToString() + j.ToString()).ToString() <> "" Then
                                    tdInner.Text = FormatNumber(DsResult.Tables(0).Rows(0).Item("col" + (i - 1).ToString() + j.ToString()).ToString() / DsRes.Tables(0).Rows(0).Item("SUNITVAL").ToString(), 4)
                                End If
                                trInner.Controls.Add(tdInner)

                            Case 9
                                InnerTdSetting(tdInner, "", "Right")
                                Dim perunit As New Decimal
                                If DsRes.Tables(0).Rows(0).Item("FINVOLMSI").ToString() > 1 Then
                                    If DsResult.Tables(0).Rows(0).Item("col" + (1).ToString() + j.ToString()).ToString() <> "" Then
                                        perunit = CDbl(DsResult.Tables(0).Rows(0).Item("col" + (1).ToString() + j.ToString()).ToString()) * CDbl(DsRes.Tables(0).Rows(0).Item("SUNIT").ToString()) / CDbl(DsRes.Tables(0).Rows(0).Item("CURR").ToString())
                                        tdInner.Text = FormatNumber(perunit, 4)
                                    End If
                                Else
                                    If DsResult.Tables(0).Rows(0).Item("col" + (1).ToString() + j.ToString()).ToString() <> "" Then
                                        perunit = CDbl(DsResult.Tables(0).Rows(0).Item("col" + (1).ToString() + j.ToString()).ToString()) * CDbl(DsRes.Tables(0).Rows(0).Item("SUNIT1").ToString()) / CDbl(DsRes.Tables(0).Rows(0).Item("CURR").ToString())
                                        tdInner.Text = FormatNumber(perunit, 4)
                                    End If
                                End If

                                trInner.Controls.Add(tdInner)
                            Case 10
                                InnerTdSetting(tdInner, "", "Right")
                                If DsResult.Tables(0).Rows(0).Item("col" + (i - 2).ToString() + j.ToString()).ToString() <> "" Then
                                    tdInner.Text = FormatNumber(DsResult.Tables(0).Rows(0).Item("col" + (i - 2).ToString() + j.ToString()).ToString() / DsRes.Tables(0).Rows(0).Item("SVOLUME").ToString(), 3)
                                End If
                                trInner.Controls.Add(tdInner)
                            Case 11                            
                                InnerTdSetting(tdInner, "", "Right")
                                Dim price As New Decimal
                                Dim perc As New Decimal
                                Dim total As New Decimal
                                lbl = New Label()
                                If DsResult.Tables(0).Rows(0).Item("col" + (i - 3).ToString() + (j).ToString()).ToString() <> "" And DsRes.Tables(0).Rows(0).Item("SVOLUME").ToString() <> "0" Then                                   
                                    price = CDbl(DsResult.Tables(0).Rows(0).Item("col" + (i - 3).ToString() + (j).ToString()).ToString() / DsRes.Tables(0).Rows(0).Item("SVOLUME").ToString())
                                    total = CDbl(DsResult.Tables(0).Rows(0).Item("col87").ToString() / DsRes.Tables(0).Rows(0).Item("SVOLUME").ToString())
                                    perc = (price / total) * 100
                                    lbl.Text = FormatNumber(perc, 3)
                                Else
                                    lbl.Text = "0.00"
                                End If

                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                        End Select
                    Next
                    trInner.Height = 30
                    If (j Mod 2 = 0) Then
                        trInner.CssClass = "AlterNateColor1"
                    Else
                        trInner.CssClass = "AlterNateColor2"
                    End If
                    tblComparision.Controls.Add(trInner)
                Next
            End If
  End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub
  Protected Sub GetPageDetails2()
        Dim ds As New DataSet
        Dim objGetData As New E1GetData.Selectdata
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
        Dim ddl As New DropDownList
        Dim strVol As Double = 0.0
        Dim dsPref As New DataSet
        Dim dsSug As New DataSet
        Dim dsEmat As New DataSet
        Dim dvEmat As New DataView
        Dim DsMat As New DataSet
        Dim DsRes As New DataSet
        Dim DsResult As New DataSet
        Dim DsPrefrence As New DataSet

        Try
            ds = objGetData.GetExtrusionOutDetails(CaseId)
            dsPref = objGetData.GetExtrusionDetailsBarrP(CaseId)
            dsSug = objGetData.GetExtrusionDetailsBarrS(CaseId)
            dsEmat = objGetData.GetEditMaterial(CaseId)
            dvEmat = dsEmat.Tables(0).DefaultView
            Session("dsEmat") = dsEmat
            DsRes = objGetData.GetProfitAndLossDetails(CaseId, False)
            DsMat = objGetData.GetMaterials("-1", "", "")
            DsPrefrence = objGetData.GetPref(CaseId)
            DsResult = objGetData.GetResultDetails(CaseId)

            lblSalesVol.Text = "<b>Sales Volume (" + DsRes.Tables(0).Rows(0).Item("TITLE8").ToString() + "):</b> " + FormatNumber(DsRes.Tables(0).Rows(0).Item("SVOLUME").ToString(), 0).ToString()
            lblSalesVolUnit.Text = "<b>Sales Volume (" + DsRes.Tables(0).Rows(0).Item("SUNITLBL").ToString() + "):</b> " + FormatNumber(DsRes.Tables(0).Rows(0).Item("SUNITVAL").ToString(), 0).ToString()
            lblSalesVolume.Text = "<b>Sales Volume (thousand Impressions):</b> " + FormatNumber(DsRes.Tables(0).Rows(0).Item("IMPRESSION").ToString(), 0).ToString()

            For i = 1 To 12
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i

                    Case 1
                        HeaderTdSetting(tdHeader, "200px", "Substrate", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        lbl = New Label
                        lbl.Width = 135
                        lbl.Text = ""
                        tdHeader2.Controls.Add(lbl)
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 2
                        Title = "(" + dsPref.Tables(0).Rows(0).Item("TITLE2").ToString() + ")"
                        HeaderTdSetting(tdHeader, "100px", "Total", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 3
                        Title = "(" + dsPref.Tables(0).Rows(0).Item("TITLE1").ToString() + ")"
                        HeaderTdSetting(tdHeader, "100px", "Thickness", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)

                    Case 4
                        HeaderTdSetting(tdHeader, "70px", "Specific Gravity", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)

                    Case 5
                        HeaderTdSetting(tdHeader, "100px", "Contribution", "1")
                        Header2TdSetting(tdHeader2, "", "(% - Materials Wt)", "1")
                        tdHeader2.ToolTip = "% based on Weight based on Direct Materials"
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 6
                        Title = "(" + DsPrefrence.Tables(0).Rows(0).Item("TITLE21").ToString() + "/" + ds.Tables(0).Rows(0).Item("TITLE3").ToString() + ")"
                        HeaderTdSetting(tdHeader, "100px", "Basis Weight", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 7
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE2").ToString() + "/" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                        HeaderTdSetting(tdHeader, "100px", "Market Price", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 8
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE2").ToString() + "/" + DsRes.Tables(0).Rows(0).Item("SUNITLBL").ToString() + ")"
                        HeaderTdSetting(tdHeader, "100px", "Material Cost", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)

                   

                    Case 9
                        'Title = "(" + DsRes.Tables(0).Rows(0).Item("PUN1").ToString() + ")"
                        If DsRes.Tables(0).Rows(0).Item("FINVOLMSI").ToString() > 1 Then
                            Title = "(" + DsRes.Tables(0).Rows(0).Item("PUN").ToString() + ")"
                        Else
                            Title = "(" + DsRes.Tables(0).Rows(0).Item("PUN1").ToString() + ")"
                        End If
                        HeaderTdSetting(tdHeader, "100px", "Material Cost", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 10
                        Title = ds.Tables(0).Rows(0).Item("Title2").ToString()
                        HeaderTdSetting(tdHeader, "90px", "By Volume", "1")
                        HeaderTdSetting(tdHeader1, "90px", "", "1")
                        Header2TdSetting(tdHeader2, "", " (" + Title + "/thousand Impressions )", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)

                    Case 11
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE2").ToString() + "/" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                        HeaderTdSetting(tdHeader, "100px", "Material Cost", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 12
                        Title = "(% - Price Wt)"
                        tdHeader2.ToolTip = "% based on Weight based on Material Price"
                        HeaderTdSetting(tdHeader, "100px", "Material Cost", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)

                End Select

            Next
            trHeader.Height = 30
            trHeader.Height = 30

            tblComparision.Controls.Add(trHeader)
            tblComparision.Controls.Add(trHeader2)
            tblComparision.Controls.Add(trHeader1)


            'Inner
            For i = 1 To 10
                trInner = New TableRow
                For j = 1 To 12
                    tdInner = New TableCell

                    Select Case j

                        Case 1
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            GetMaterialDetailsNew(lbl, hid, CInt(dsPref.Tables(0).Rows(0).Item("M" + i.ToString() + "").ToString()), "hypGradeDes" + i.ToString(), "hidGradeId" + i.ToString(), "SG" + i.ToString(), DsMat)
                            tdInner.Width = 120
                            tdInner.Height = 20
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 2
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            If ds.Tables(0).Rows(0).Item("PRODZ" + i.ToString() + "").ToString() <> "" Then
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("PRODZ" + i.ToString() + "").ToString(), 0)
                            Else
                                lbl.Text = "0"
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 3
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            If dsPref.Tables(0).Rows(0).Item("THICK" + i.ToString() + "").ToString() <> "" Then
                                lbl.Text = FormatNumber(dsPref.Tables(0).Rows(0).Item("THICK" + i.ToString() + "").ToString(), 3)
                            Else
                                lbl.Text = "0"
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 4
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            If dsPref.Tables(0).Rows(0).Item("SGP" + i.ToString() + "").ToString() <> "0" Then
                                lbl.Text = FormatNumber(dsPref.Tables(0).Rows(0).Item("SGP" + i.ToString() + "").ToString(), 3)
                            Else
                                lbl.Text = FormatNumber(dsSug.Tables(0).Rows(0).Item("SGS" + i.ToString() + "").ToString(), 3)
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 5
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            If ds.Tables(0).Rows(0).Item("P" + i.ToString() + "").ToString() <> "" Then
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("P" + i.ToString() + "").ToString(), 2)
                            Else
                                lbl.Text = "0"
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 6
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            If ds.Tables(0).Rows(0).Item("W" + i.ToString() + "").ToString() <> "" Then
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("W" + i.ToString() + "").ToString() * DsPrefrence.Tables(0).Rows(0).Item("CONVWT5").ToString(), 3)
                            Else
                                lbl.Text = "0.0"
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 7
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label()
                            If (dsPref.Tables(0).Rows(0).Item("PRP" + i.ToString() + "").ToString() <> "0") Then
                                lbl.Text = FormatNumber(dsPref.Tables(0).Rows(0).Item("PRP" + i.ToString() + "").ToString(), 3)
                            Else
                                If dsSug.Tables(0).Rows(0).Item("PRS" + i.ToString() + "").ToString() <> "" Then
                                    lbl.Text = FormatNumber(dsSug.Tables(0).Rows(0).Item("PRS" + i.ToString() + "").ToString(), 3)
                                Else
                                    lbl.Text = "0.00"
                                End If
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 8
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label()
                            If ds.Tables(0).Rows(0).Item("PRODZ" + i.ToString() + "").ToString() <> "" Then
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("PRODZ" + i.ToString() + "").ToString() / DsRes.Tables(0).Rows(0).Item("SUNITVAL").ToString(), 4)
                            Else
                                lbl.Text = "0.00"
                            End If

                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)

                        

                        Case 9
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label()
                            Dim perunit As New Decimal
                            If DsRes.Tables(0).Rows(0).Item("FINVOLMSI").ToString() > 1 Then
                                If ds.Tables(0).Rows(0).Item("PRODZ" + i.ToString() + "").ToString() <> "" Then
                                    If CDbl(DsRes.Tables(0).Rows(0).Item("SUNIT").ToString()) > 0 Then
                                        perunit = CDbl(ds.Tables(0).Rows(0).Item("PRODZ" + i.ToString() + "").ToString()) * CDbl(DsRes.Tables(0).Rows(0).Item("SUNIT").ToString()) / CDbl(DsRes.Tables(0).Rows(0).Item("CURR").ToString())
                                        lbl.Text = FormatNumber(perunit, 4)
                                    Else
                                        lbl.Text = "0"
                                    End If
                                Else
                                    lbl.Text = "0"
                                End If
                            Else
                                If ds.Tables(0).Rows(0).Item("PRODZ" + i.ToString() + "").ToString() <> "" Then
                                    If CDbl(DsRes.Tables(0).Rows(0).Item("SUNIT1").ToString()) > 0 Then
                                        perunit = CDbl(ds.Tables(0).Rows(0).Item("PRODZ" + i.ToString() + "").ToString()) * CDbl(DsRes.Tables(0).Rows(0).Item("SUNIT1").ToString()) / CDbl(DsRes.Tables(0).Rows(0).Item("CURR").ToString())
                                        lbl.Text = FormatNumber(perunit, 4)
                                    Else
                                        lbl.Text = "0"
                                    End If
                                Else
                                    lbl.Text = "0"
                                End If
                            End If

                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)

                        Case 10
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label()
                            If ds.Tables(0).Rows(0).Item("PRODZ" + i.ToString() + "").ToString() <> "" And DsRes.Tables(0).Rows(0).Item("SVOLUME").ToString() <> "0" Then
                                lbl.Text = FormatNumber((ds.Tables(0).Rows(0).Item("PRODZ" + i.ToString() + "").ToString() / DsRes.Tables(0).Rows(0).Item("IMPRESSION").ToString()), 4)
                            Else
                                lbl.Text = "0.0000"
                            End If

                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)

                        Case 11
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label()
                            If ds.Tables(0).Rows(0).Item("PRODZ" + i.ToString() + "").ToString() <> "" And DsRes.Tables(0).Rows(0).Item("SVOLUME").ToString() <> "0" Then
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("PRODZ" + i.ToString() + "").ToString() / DsRes.Tables(0).Rows(0).Item("SVOLUME").ToString(), 3)
                            Else
                                lbl.Text = "0.00"
                            End If

                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 12
                            InnerTdSetting(tdInner, "", "Right")
                            Dim price As New Decimal
                            Dim perc As New Decimal
                            Dim total As New Decimal
                            lbl = New Label()
                            If ds.Tables(0).Rows(0).Item("PRODZ" + i.ToString() + "").ToString() <> "" And DsRes.Tables(0).Rows(0).Item("SVOLUME").ToString() <> "0" Then
                                price = CDbl(ds.Tables(0).Rows(0).Item("PRODZ" + i.ToString() + "").ToString() / DsRes.Tables(0).Rows(0).Item("SVOLUME").ToString())
                                total = CDbl(DsResult.Tables(0).Rows(0).Item("col87").ToString() / DsRes.Tables(0).Rows(0).Item("SVOLUME").ToString())
                                perc = (price / total) * 100
                                lbl.Text = FormatNumber(perc, 3)
                            Else
                                lbl.Text = "0.00"
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
                trInner.Height = 30
                tblComparision.Controls.Add(trInner)
            Next

            'DM
            Dim S1 As Decimal
            Dim S2 As Decimal
            Dim S3 As Decimal
            If DsResult.Tables(0).Rows(0).Item("col82").ToString() <> "" And DsResult.Tables(0).Rows(0).Item("col84").ToString() <> "" Then
                If DsResult.Tables(0).Rows(0).Item("col82").ToString() <> 0 And DsResult.Tables(0).Rows(0).Item("col84").ToString <> 0 Then
                    S1 = FormatNumber(DsResult.Tables(0).Rows(0).Item("col82").ToString() / DsRes.Tables(0).Rows(0).Item("SVOLUME").ToString(), 3)
                    S2 = FormatNumber(DsResult.Tables(0).Rows(0).Item("col84").ToString() / DsRes.Tables(0).Rows(0).Item("SVOLUME").ToString(), 3)
                    S3 = (S1 / S2) * 100
                End If
            End If
            If DsResult.Tables.Count > 0 Then
                For j = 1 To 7
                    trInner = New TableRow
                    For i = 1 To 12
                        tdInner = New TableCell
                        Select Case i
                            Case 1
                                If j = 5 Then
                                    HeaderTrSetting(tdInner, "", "", 2)
                                    tdInner.Text = DsResult.Tables(0).Rows(0).Item("des" + j.ToString()).ToString() '"  Direct Materials "
                                    If j <> 2 Then
                                        tdInner.Text = "<b>" + tdInner.Text + "</b>"
                                    End If
                                    trInner.Controls.Add(tdInner)
                                ElseIf j = 6 Then
                                Else
                                    HeaderTrSetting(tdInner, "", "", 1)
                                    tdInner.Text = DsResult.Tables(0).Rows(0).Item("des" + j.ToString()).ToString()
                                    'If j <> 2 Then
                                    tdInner.Text = "<b>" + tdInner.Text + "</b>"
                                    'End If
                                    trInner.Controls.Add(tdInner)
                                End If
                            Case 2
                                InnerTdSetting(tdInner, "", "Right")
                                If DsResult.Tables(0).Rows(0).Item("col" + (i - 1).ToString() + j.ToString()).ToString() <> "" Then
                                    tdInner.Text = FormatNumber(DsResult.Tables(0).Rows(0).Item("col" + (i - 1).ToString() + j.ToString()).ToString(), 0)
                                End If
                                trInner.Controls.Add(tdInner)
                            Case 3
                                InnerTdSetting(tdInner, "", "Left")
                                If DsResult.Tables(0).Rows(0).Item("col" + (i - 1).ToString() + j.ToString()).ToString() <> "" Then
                                    If DsResult.Tables(0).Rows(0).Item("col" + (i - 1).ToString() + j.ToString()).ToString().Contains("i") Then
                                        tdInner.Text = DsResult.Tables(0).Rows(0).Item("col" + (i - 1).ToString() + j.ToString()).ToString()
                                    Else
                                        tdInner.Text = FormatNumber(DsResult.Tables(0).Rows(0).Item("col" + (i - 1).ToString() + j.ToString()).ToString(), 1)
                                    End If
                                End If
                                trInner.Controls.Add(tdInner)
                            Case 4
                                InnerTdSetting(tdInner, "", "Right")
                                If DsResult.Tables(0).Rows(0).Item("col" + (i - 1).ToString() + j.ToString()).ToString() <> "" Then
                                    tdInner.Text = FormatNumber(DsResult.Tables(0).Rows(0).Item("col" + (i - 1).ToString() + j.ToString()).ToString(), 1)
                                End If
                                tdInner.Text = "<b>" + tdInner.Text + "</b>"
                                trInner.Controls.Add(tdInner)
                            Case 5
                                InnerTdSetting(tdInner, "", "Right")
                                tdInner.ID = "Total" + (i - 1).ToString()
                                If DsResult.Tables(0).Rows(0).Item("col" + (i - 1).ToString() + j.ToString()).ToString() <> "" Then
                                    tdInner.Text = FormatNumber(DsResult.Tables(0).Rows(0).Item("col" + (i - 1).ToString() + j.ToString()).ToString(), 2)
                                End If
                                tdInner.Text = "<b>" + tdInner.Text + "</b>"
                                trInner.Controls.Add(tdInner)
                            Case 6
                                InnerTdSetting(tdInner, "", "Right")
                                If DsResult.Tables(0).Rows(0).Item("col" + (i - 1).ToString() + j.ToString()).ToString() <> "" Then
                                    tdInner.Text = FormatNumber(DsResult.Tables(0).Rows(0).Item("col" + (i - 1).ToString() + j.ToString()).ToString(), 3)
                                End If
                                trInner.Controls.Add(tdInner)
                            Case 7
                                InnerTdSetting(tdInner, "", "Right")
                                If j = 2 Then
                                    tdInner.Text = FormatNumber(S3, 2) + "%"
                                Else
                                    If DsResult.Tables(0).Rows(0).Item("col" + (i - 1).ToString() + j.ToString()).ToString() <> "" Then
                                        tdInner.Text = FormatNumber(DsResult.Tables(0).Rows(0).Item("col" + (i - 1).ToString() + j.ToString()).ToString(), 3)
                                    End If
                                End If
                                trInner.Controls.Add(tdInner)
                            Case 8
                                InnerTdSetting(tdInner, "", "Right")
                                If DsResult.Tables(0).Rows(0).Item("col" + (i - 1).ToString() + j.ToString()).ToString() <> "" Then
                                    tdInner.Text = FormatNumber(DsResult.Tables(0).Rows(0).Item("col" + (i - 1).ToString() + j.ToString()).ToString() / DsRes.Tables(0).Rows(0).Item("SUNITVAL").ToString(), 4)
                                End If
                                trInner.Controls.Add(tdInner)

                            

                            Case 9
                                InnerTdSetting(tdInner, "", "Right")
                                Dim perunit As New Decimal
                                If DsRes.Tables(0).Rows(0).Item("FINVOLMSI").ToString() > 1 Then
                                    If DsResult.Tables(0).Rows(0).Item("col" + (1).ToString() + j.ToString()).ToString() <> "" Then
                                        perunit = CDbl(DsResult.Tables(0).Rows(0).Item("col" + (1).ToString() + j.ToString()).ToString()) * CDbl(DsRes.Tables(0).Rows(0).Item("SUNIT").ToString()) / CDbl(DsRes.Tables(0).Rows(0).Item("CURR").ToString())
                                        tdInner.Text = FormatNumber(perunit, 4)
                                    End If
                                Else
                                    If DsResult.Tables(0).Rows(0).Item("col" + (1).ToString() + j.ToString()).ToString() <> "" Then
                                        perunit = CDbl(DsResult.Tables(0).Rows(0).Item("col" + (1).ToString() + j.ToString()).ToString()) * CDbl(DsRes.Tables(0).Rows(0).Item("SUNIT1").ToString()) / CDbl(DsRes.Tables(0).Rows(0).Item("CURR").ToString())
                                        tdInner.Text = FormatNumber(perunit, 4)
                                    End If
                                End If

                                trInner.Controls.Add(tdInner)

                            Case 10
                                InnerTdSetting(tdInner, "", "Right")
                                If DsResult.Tables(0).Rows(0).Item("col1" + j.ToString()).ToString() <> "" Then
                                    tdInner.Text = FormatNumber((DsResult.Tables(0).Rows(0).Item("col1" + j.ToString()).ToString() / DsRes.Tables(0).Rows(0).Item("IMPRESSION").ToString()) * 1000, 3)
                                End If
                                trInner.Controls.Add(tdInner)

                            Case 11
                                InnerTdSetting(tdInner, "", "Right")
                                If DsResult.Tables(0).Rows(0).Item("col" + (i - 3).ToString() + j.ToString()).ToString() <> "" Then
                                    tdInner.Text = FormatNumber(DsResult.Tables(0).Rows(0).Item("col" + (i - 3).ToString() + j.ToString()).ToString() / DsRes.Tables(0).Rows(0).Item("SVOLUME").ToString(), 3)
                                End If
                                trInner.Controls.Add(tdInner)
                            Case 12
                                InnerTdSetting(tdInner, "", "Right")
                                Dim price As New Decimal
                                Dim perc As New Decimal
                                Dim total As New Decimal
                                lbl = New Label()
                                If DsResult.Tables(0).Rows(0).Item("col" + (i - 4).ToString() + (j).ToString()).ToString() <> "" And DsRes.Tables(0).Rows(0).Item("SVOLUME").ToString() <> "0" Then
                                    price = CDbl(DsResult.Tables(0).Rows(0).Item("col" + (i - 4).ToString() + (j).ToString()).ToString() / DsRes.Tables(0).Rows(0).Item("SVOLUME").ToString())
                                    total = CDbl(DsResult.Tables(0).Rows(0).Item("col87").ToString() / DsRes.Tables(0).Rows(0).Item("SVOLUME").ToString())
                                    perc = (price / total) * 100
                                    lbl.Text = FormatNumber(perc, 3)
                                Else
                                    lbl.Text = "0.00"
                                End If

                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)


                        End Select
                    Next
                    trInner.Height = 30
                    If (j Mod 2 = 0) Then
                        trInner.CssClass = "AlterNateColor1"
                    Else
                        trInner.CssClass = "AlterNateColor2"
                    End If
                    tblComparision.Controls.Add(trInner)
                Next
            End If

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub GetMaterialDetailsNew(ByRef LinkMat As Label, ByVal hid As HiddenField, ByVal MatId As Integer, ByVal linkGrade As String, ByVal hidGrade As String, _
                                       ByVal SG As String, ByVal dsMat As DataSet)
        Dim Ds As New DataSet
        Dim ObjGetdata As New E1GetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty
        Dim dsEmat As New DataSet
        Dim str As String = ""
        Dim dv As New DataView
        Dim dt As New DataTable
        'Changes for AWS
        Dim dvMat As New DataView
        Dim dtMat As New DataTable
        'End
        Try

            dsEmat = DirectCast(Session("dsEmat"), DataSet)
            dv = dsEmat.Tables(0).DefaultView

            If MatId <> 0 Then
                dv.RowFilter = "MATID=" + MatId.ToString()
                dt = dv.ToTable()

                If dt.Rows.Count > 0 Then
                    LinkMat.Text = dt.Rows(0).Item("MATDES").ToString().Replace("&#", "'")
                    LinkMat.ToolTip = dt.Rows(0).Item("MATDES").ToString().Replace("&#", "'")
                    LinkMat.Attributes.Add("text-decoration", "none")
                Else
                
                    'Changes for AWS
                    dvMat = dsMat.Tables(0).DefaultView '
                    dvMat.RowFilter = "MATID = " + MatId.ToString() '
                    dtMat = dvMat.ToTable()

                    If dtMat.Rows(0).Item("MATDES").ToString().Length > 25 Then
                        LinkMat.Font.Size = 8
                    End If
                    'End

                    LinkMat.Text = dtMat.Rows(0).Item("MATDES").ToString()
                    LinkMat.ToolTip = dtMat.Rows(0).Item("MATDES").ToString()
                    LinkMat.Attributes.Add("text-decoration", "none")
                End If
            Else
             
                'Changes for AWS
                dvMat = dsMat.Tables(0).DefaultView
                dvMat.RowFilter = "MATID = " + MatId.ToString() '
                dtMat = dvMat.ToTable()

                If dtMat.Rows(0).Item("MATDES").ToString().Length > 25 Then
                    'LinkMat.Font.Size = 8
                End If
                'End

                LinkMat.Text = dtMat.Rows(0).Item("MATDES").ToString()
                LinkMat.ToolTip = dtMat.Rows(0).Item("MATDES").ToString()
                LinkMat.Attributes.Add("text-decoration", "none")
            End If

            hid.Value = MatId.ToString()
            
        Catch ex As Exception
            ErrorLable.Text = "Error:GetMaterialDetailsNew:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub HeaderTrSetting(ByVal Td As TableCell, ByVal Width As String, ByVal HeaderText As String, ByVal ColSpan As String)
        Try
            Td.Text = HeaderText
            Td.RowSpan = ColSpan
            If Width <> "" Then
                Td.Style.Add("width", Width)
            End If           
            'Td.Height = 20
            'Td.Font.Size = 10
           
        Catch ex As Exception
            _lErrorLble.Text = "Error:HeaderTdSetting:" + ex.Message.ToString()
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

    Protected Sub Update_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim UnitPrice As String = String.Empty
        Dim UnitPrice2 As String = String.Empty
        Dim UnitType As String = String.Empty
        Dim ObjUpIns As New E1UpInsData.UpdateInsert()
        Dim obj As New CryptoHelper
        Try
            If Not objRefresh.IsRefresh Then
                
                Calculate()
                'Update Server Date
                ObjUpIns.ServerDateUpdate(CaseId, Session("E1UserName"))
                'End If
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
End Class
