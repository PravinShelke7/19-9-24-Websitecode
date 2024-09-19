Imports System.Data
Imports System.Data.OleDb
Imports System
Imports S2GetData
Imports S2UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Sustain2_Assumptions_Extrusion
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
        Updatebtn.Attributes.Add("onclick", "return CheckForMaterialPage('" + ctlContentPlaceHolder.ClientID + "','hidMatid','WEIGHT','IC','hidCaseId','hypCaseDes','hidDepid','hypDep');")
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
        'Chartbtn.OnClientClick = "return Chart('../../../Charts/MaterialPrice.aspx','MatPriceChart');"
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
        Notesbtn.OnClientClick = "return Notes('MATANDSTR');"
        'AddHandler Updatebtn.Click, AddressOf Update_Click
    End Sub

    Protected Sub GetMainHeadingdiv()
        MainHeading = Page.Master.FindControl("divMainHeading")
        MainHeading.Attributes.Add("onmouseover", "Tip('Material Assumption')")
        MainHeading.Attributes.Add("onmouseout", "UnTip()")
        MainHeading.InnerHtml = "Sustain2 - Material Assumptions"
    End Sub

    Protected Sub GetContentPlaceHolder()
        ctlContentPlaceHolder = Page.Master.FindControl("Sustain2ContentPlaceHolder")
    End Sub

#End Region

#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_PAGES_SUSTAIN2_ASSUMPTIONS_EXTRUSION")
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        objRefresh.Render(Page)
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            GetMasterPageControls()
            GetSessionDetails()
            GetChartbtn()
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
            CaseId = Session("S2CaseId")
            UserRole = Session("S2UserRole")
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetSessionDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetPageDetails()
        Dim ds As New DataSet
        Dim objGetData As New S2GetData.Selectdata
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

        Dim radio As New RadioButton

        Dim dsMat As New DataSet
        Dim dsSD As New DataSet
        Dim dsDept As New DataSet
        Try
            ds = objGetData.GetExtrusionDetails(CaseId)

            dsMat = objGetData.GetMaterials("-1", "", "")
            dsSD = objGetData.GetSustain1Cases("-1", CaseId, "", "", Session("USERID").ToString())
            dsDept = objGetData.GetDept("-1", "")

            For i = 1 To 11
                tdHeader = New TableCell
                tdHeader2 = New TableCell
                tdHeader1 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "40px", "Layers", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 2
                        HeaderTdSetting(tdHeader, "115px", "Materials", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 3
                        HeaderTdSetting(tdHeader, "40px", "Package Component", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 4
                        HeaderTdSetting(tdHeader, "", "Quantity", "1")
                        Header2TdSetting(tdHeader2, "", "(%)", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 5
                        HeaderTdSetting(tdHeader, "120px", "Sustain1 Cases", "1")
                        Header2TdSetting(tdHeader2, "", "(%)", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                        'Case 6
                        '    Title = "(" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                        '    HeaderTdSetting(tdHeader, "50px", "Weight", "2")
                        '    Header2TdSetting(tdHeader2, "", Title, "1")
                        '    trHeader.Controls.Add(tdHeader)
                        '    trHeader2.Controls.Add(tdHeader2)
                        'Case 7
                        '    Title = "(" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                        '    HeaderTdSetting(tdHeader, "50px", "Weight", "1")
                        '    Header2TdSetting(tdHeader2, "", Title, "1")
                        '    trHeader.Controls.Add(tdHeader)
                        '    trHeader2.Controls.Add(tdHeader2)
                    Case 6
                        Title = "(" + ds.Tables(0).Rows(0).Item("Title8").ToString() + ")"
                        HeaderTdSetting(tdHeader, "", "Weight", "2")
                        Header2TdSetting(tdHeader2, "", Title, "2")
                        Header2TdSetting(tdHeader1, "70px", "Suggested", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 7
                        Header2TdSetting(tdHeader1, "70px", "Preferred", "1")
                        trHeader1.Controls.Add(tdHeader1)
                    Case 8
                        Title = "(%)"
                        HeaderTdSetting(tdHeader, "50px", "Recycle", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 9
                        Title = "(%)"
                        HeaderTdSetting(tdHeader, "50px", "Extra-process", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)

                    Case 10
                        Title = "(%)"
                        HeaderTdSetting(tdHeader, "50px", "Weight", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 11
                        HeaderTdSetting(tdHeader, "100px", "Mfg. Dept.", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
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
                            'Layer
                            InnerTdSetting(tdInner, "", "Center")
                            tdInner.Text = "<b>" + i.ToString() + "</b>"
                            trInner.Controls.Add(tdInner)
                        Case 2
                            InnerTdSetting(tdInner, "", "Left")
                            Link = New HyperLink
                            hid = New HiddenField
                            Link.ID = "hypMatDes" + i.ToString()
                            hid.ID = "hidMatid" + i.ToString()
                            Link.Width = 120
                            Link.CssClass = "Link"
                            GetMaterialDetails(Link, hid, CInt(ds.Tables(0).Rows(0).Item("M" + i.ToString() + "").ToString()), dsMat)
                            tdInner.Controls.Add(hid)
                            tdInner.Controls.Add(Link)
                            trInner.Controls.Add(tdInner)
                        Case 3
                            InnerTdSetting(tdInner, "", "Center")
                            chk = New CheckBox()
                            chk.ID = "IC" + i.ToString()
                            If ds.Tables(0).Rows(0).Item("IP" + i.ToString() + "").ToString() = 1 Then
                                chk.Checked = True
                            Else
                                chk.Checked = False
                            End If
                            tdInner.Controls.Add(chk)
                            trInner.Controls.Add(tdInner)
                        Case 4
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.Width = 50
                            txt.ID = "Q" + i.ToString()
                            txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("Q" + i.ToString() + "").ToString(), 3)
                            txt.MaxLength = 6
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 5
                            InnerTdSetting(tdInner, "", "Left")
                            Link = New HyperLink
                            hid = New HiddenField
                            Link.ID = "hypCaseDes" + i.ToString()
                            hid.ID = "hidCaseId" + i.ToString()
                            Link.Width = 120
                            Link.CssClass = "Link"
                            GetSustain1Details(Link, hid, CInt(ds.Tables(0).Rows(0).Item("C" + i.ToString() + "").ToString()), dsSD)
                            tdInner.Controls.Add(hid)
                            tdInner.Controls.Add(Link)
                            trInner.Controls.Add(tdInner)
                        Case 6
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("WEIGHTS" + i.ToString() + "").ToString(), 4)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 7
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.Width = 50
                            txt.ID = "WEIGHT" + i.ToString()
                            txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("WEIGHTP" + i.ToString() + "").ToString(), 4)
                            txt.MaxLength = 6
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 8
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.Width = 50
                            txt.ID = "R" + i.ToString()
                            txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("R" + i.ToString() + "").ToString(), 2)
                            txt.MaxLength = 6
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 9
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.Width = 50
                            txt.ID = "E" + i.ToString()
                            txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("E" + i.ToString() + "").ToString(), 2)
                            txt.MaxLength = 6
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 10
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Width = 50
                            lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("P" + i.ToString() + "").ToString(), 1)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 11
                            InnerTdSetting(tdInner, "", "Left")
                            Link = New HyperLink
                            hid = New HiddenField
                            Link.ID = "hypDep" + i.ToString()
                            hid.ID = "hidDepid" + i.ToString()
                            Link.Width = 90
                            Link.CssClass = "Link"
                            GetDeptDetails(Link, hid, CInt(ds.Tables(0).Rows(0).Item("D" + i.ToString() + "").ToString()), dsDept)
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


            'Total()
            trInner = New TableRow
            For i = 1 To 10
                tdInner = New TableCell
                Select Case i
                    Case 1
                        InnerTdSetting(tdInner, "", "Left")
                        tdInner.Text = "<span class='CalculatedFeilds'><b> Total </b></span>"
                        trInner.Controls.Add(tdInner)
                    Case 6
                        InnerTdSetting(tdInner, "", "Center")
                        tdInner.ColumnSpan = "2"
                        tdInner.Text = "<span class='CalculatedFeilds'><b>" + FormatNumber(ds.Tables(0).Rows(0).Item("WEIGHTTOT").ToString(), 4) + " </b> </span>"
                        trInner.Controls.Add(tdInner)

                    Case 9
                        InnerTdSetting(tdInner, "", "Right")
                        Dim TOTWP As Decimal
                        TOTWP = 0
                        Dim k As Integer
                        For k = 1 To 10
                            TOTWP = TOTWP + CDbl(ds.Tables(0).Rows(0).Item("P" + k.ToString() + "").ToString())
                        Next
                        tdInner.Text = "<span class='CalculatedFeilds'><b>" + FormatNumber(Math.Ceiling(TOTWP), 1) + " </b> </span>"
                        trInner.Controls.Add(tdInner)


                    Case 2, 3, 4, 5, 7, 8, 10
                        InnerTdSetting(tdInner, "", "Left")
                        trInner.Controls.Add(tdInner)
                End Select
            Next
            trInner.Height = 30
            trInner.CssClass = "AlterNateColor2"
            tblComparision.Controls.Add(trInner)


            'Printing Plates
            trHeader = New TableRow
            tdHeader = New TableCell

            HeaderTdSetting(tdHeader, "", "Printing Plates", "1")
            tdHeader.Style.Add("text-align", "left")
            trHeader.Controls.Add(tdHeader)
            trHeader.Height = 30
            trHeader.Height = 30
            tblComparision1.Controls.Add(trHeader)



            'Inner
            For i = 1 To 3
                trInner = New TableRow
                tdInner = New TableCell

                InnerTdSetting(tdInner, "230px", "left")
                radio = New RadioButton
                If ds.Tables(0).Rows(0).Item("plate") = (i - 1).ToString() Then
                    radio.Checked = True
                End If
                Select Case i
                    Case 1
                        radio.Text = "not required"
                    Case 2
                        radio.Text = "produced by packaging supplier"
                    Case 3
                        radio.Text = "produced by outside supplier"
                End Select

                radio.ID = i - 1
                radio.GroupName = "radPlate"
                tdInner.Controls.Add(radio)
                trInner.Controls.Add(tdInner)

                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
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

    Protected Sub GetMaterialDetails(ByRef LinkMat As HyperLink, ByVal hid As HiddenField, ByVal MatId As Integer, ByVal dsMat As DataSet)
        Dim Ds As New DataSet
        Dim ObjGetdata As New S2GetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty
        Dim dvMat As New DataView
        Dim dtMat As New DataTable
        Try
            'Ds = ObjGetdata.GetMaterials(MatId, "", "")
            dvMat = dsMat.Tables(0).DefaultView
            dvMat.RowFilter = "MATID= " + MatId.ToString()
            dtMat = dvMat.ToTable()

            If dtMat.Rows(0).Item("MATDES").ToString().Length > 25 Then
                'LinkMat.Font.Size = 8
            End If
            LinkMat.Text = dtMat.Rows(0).Item("MATDES").ToString()
            LinkMat.ToolTip = dtMat.Rows(0).Item("MATDES").ToString()
            LinkMat.Attributes.Add("text-decoration", "none")

            hid.Value = MatId.ToString()
            Path = "../PopUp/GetMatPopUp.aspx?Des=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + LinkMat.ClientID + "&Id=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + hid.ClientID + ""
            LinkMat.NavigateUrl = "javascript:ShowPopWindow('" + Path + "')"

        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetSustain1Details(ByRef LinkMat As HyperLink, ByVal hid As HiddenField, ByVal CaseIdS1 As Integer, ByVal dsSD As DataSet)
        Dim Ds As New DataSet
        Dim ObjGetdata As New S2GetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty
        Dim dvSD As New DataView
        Dim dtSD As New DataTable
        Try
            'Ds = ObjGetdata.GetSustain1Cases(CaseIdS1, CaseId, "", "", Session("USERID").ToString())
            dvSD = dsSD.Tables(0).DefaultView
            dvSD.RowFilter = "CASEID= " + CaseIdS1.ToString()
            dtSD = dvSD.ToTable()

            If dtSD.Rows.Count = 0 Then
                LinkMat.Text = "Conflict"
                LinkMat.Attributes.Add("text-decoration", "none")
            Else
                LinkMat.Text = dtSD.Rows(0).Item("CASDES").ToString()
                LinkMat.ToolTip = dtSD.Rows(0).Item("CASDES").ToString()
                LinkMat.Attributes.Add("text-decoration", "none")
            End If

            hid.Value = CaseIdS1.ToString()
            Path = "../PopUp/GetSustain1CasePopup.aspx?Des=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + LinkMat.ClientID + "&Id=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + hid.ClientID + ""
            LinkMat.NavigateUrl = "javascript:ShowPopWindow('" + Path + "')"

        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetDeptDetails(ByRef LinkDep As HyperLink, ByVal hid As HiddenField, ByVal ProcId As Integer, ByVal dsDept As DataSet)
        Dim Ds As New DataSet
        Dim ObjGetdata As New S2GetData.Selectdata()
        Dim Path As String = String.Empty
        Dim dvDept As New DataView
        Dim dtDept As New DataTable
        Try
            'Ds = ObjGetdata.GetDept(ProcId, "")
            dvDept = dsDept.Tables(0).DefaultView '
            dvDept.RowFilter = "PROCID= " + ProcId.ToString() '
            dtDept = dvDept.ToTable() '

            Path = "../PopUp/GetDepPopUp.aspx?Des=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + LinkDep.ClientID + "&Id=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + hid.ClientID + ""
            If dtDept.Rows.Count = 0 Then
                LinkDep.Text = "Dept. Conflict"
                LinkDep.ForeColor = Drawing.Color.DarkRed
            Else
                LinkDep.Text = dtDept.Rows(0).Item("PROCDE").ToString()
            End If


            hid.Value = ProcId.ToString()
            LinkDep.NavigateUrl = "javascript:ShowPopWindow('" + Path + "')"
        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub Update_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim Material(9) As String
        Dim Qty(9) As String
        Dim PComp(9) As String
        Dim SCase(9) As String
        Dim Weight(9) As String
        Dim Recycle(9) As String
        Dim Extra(9) As String
        Dim Dept(9) As String
        Dim plate As String
        Dim i As New Integer

        Dim ObjUpIns As New S2UpInsData.UpdateInsert()
        'Dim DisCount As New Integer
        'Dim ProductFormt As New DataSet
        Dim ObjGetData As New S2GetData.Selectdata()
        'ProductFormt = ObjGetData.GetProductFromatIn(CaseId)
        Dim obj As New CryptoHelper
        Try
            If Not objRefresh.IsRefresh Then
                For i = 1 To 10
                    Material(i - 1) = Request.Form("ctl00$Sustain2ContentPlaceHolder$hidMatid" + i.ToString() + "")
                    Qty(i - 1) = Request.Form("ctl00$Sustain2ContentPlaceHolder$Q" + i.ToString() + "")
                    If Request.Form("ctl00$Sustain2ContentPlaceHolder$IC" + i.ToString() + "") <> "" Then
                        PComp(i - 1) = 1
                    Else
                        PComp(i - 1) = 0
                    End If
                    SCase(i - 1) = Request.Form("ctl00$Sustain2ContentPlaceHolder$hidCaseId" + i.ToString() + "")
                    Weight(i - 1) = Request.Form("ctl00$Sustain2ContentPlaceHolder$WEIGHT" + i.ToString() + "")
                    Recycle(i - 1) = Request.Form("ctl00$Sustain2ContentPlaceHolder$R" + i.ToString() + "")
                    Extra(i - 1) = Request.Form("ctl00$Sustain2ContentPlaceHolder$E" + i.ToString() + "")
                    Dept(i - 1) = Request.Form("ctl00$Sustain2ContentPlaceHolder$hidDepid" + i.ToString() + "")
                    'Check For IsNumric
                    If Not IsNumeric(Qty(i - 1)) Then
                        Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                    End If
                    If Not IsNumeric(Weight(i - 1)) Then
                        Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                    End If

                    If Not IsNumeric(Recycle(i - 1)) Then
                        Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                    End If

                    If Not IsNumeric(Extra(i - 1)) Then
                        Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                    End If


                    'Check For Dependant-Indepdant Error                   
                    If CInt(Material(i - 1)) <> 0 Then
                        ''Checking Weight.
                        'If CDbl(Weight(i - 1)) <= 0 Then
                        '    Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE101").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        'End If
                        'Checking Dept.
                        If CDbl(Dept(i - 1)) <= 0 Then
                            Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE104").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        End If
                    End If

                Next
                plate = Request.Form("ctl00$Sustain2ContentPlaceHolder$radPlate")

                ObjUpIns.ExtrusionUpdate(CaseId, Material, Qty, PComp, SCase, Weight, Recycle, Extra, Dept, plate, False)
                Calculate()
                'Update Server Date
                ObjUpIns.ServerDateUpdate(CaseId, Session("S2UserName"))
            End If
            GetPageDetails()
        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
    Protected Sub Calculate()
        Try
            Dim Econ1Conn As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            Dim Sustain1Conn As String = System.Configuration.ConfigurationManager.AppSettings("Sustain1ConnectionString")
            Dim Econ2Conn As String = System.Configuration.ConfigurationManager.AppSettings("Econ2ConnectionString")
            Dim Sustain2Conn As String = System.Configuration.ConfigurationManager.AppSettings("Sustain2ConnectionString")
            Dim obj As New Sustain2Calculation.Sustain2Calculations(Sustain2Conn, Econ2Conn, Sustain1Conn, Econ1Conn)
            obj.Sustain2Calculate(CaseId)
        Catch ex As Exception
            ErrorLable.Text = "Error:Calculate:" + ex.Message.ToString() + ""
        End Try
    End Sub

End Class
