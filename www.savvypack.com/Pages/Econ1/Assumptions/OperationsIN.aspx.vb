Imports System.Data
Imports System.Data.OleDb
Imports System
Imports E1GetData
Imports E1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Econ1_Assumptions_OperationsIN
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
    Dim _btnWizard As ImageButton
    Dim _btnInk As ImageButton

    Public Property Inkbtn() As ImageButton
        Get
            Return _btnInk
        End Get
        Set(ByVal value As ImageButton)
            _btnInk = value
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

    Public Property Wizardbtn() As ImageButton
        Get
            Return _btnWizard
        End Get
        Set(ByVal value As ImageButton)
            _btnWizard = value
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
        GetWizard()
        GetMainHeadingdiv()
        GetInkbtn()
    End Sub

    Protected Sub GetInkbtn()
        Inkbtn = Page.Master.FindControl("imgInkCost")
        Inkbtn.Visible = True
        Inkbtn.ToolTip = "Injection Molding Assistant"
        Inkbtn.OnClientClick = "return Chart('../Popup/InjectionMolding2.aspx','MatPriceChart');"
        'AddHandler Updatebtn.Click, AddressOf Update_Click
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
        Notesbtn.OnClientClick = "return Notes('OPPARAM');"
        'AddHandler Updatebtn.Click, AddressOf Update_Click
    End Sub

    Protected Sub GetMainHeadingdiv()
        MainHeading = Page.Master.FindControl("divMainHeading")
        MainHeading.Attributes.Add("onmouseover", "Tip('Operating Assumptions')")
        MainHeading.Attributes.Add("onmouseout", "UnTip()")
        MainHeading.InnerHtml = "Econ1 - Operating Assumptions"
    End Sub

    Protected Sub GetContentPlaceHolder()
        ctlContentPlaceHolder = Page.Master.FindControl("Econ1ContentPlaceHolder")
    End Sub

    Protected Sub GetWizard()
        Wizardbtn = Page.Master.FindControl("imgWizard")
        Wizardbtn.Visible = True
        Wizardbtn.OnClientClick = "return Wizard('Wizard.aspx?Type=DSGNWASTE','wizDWaste');"
        Wizardbtn.ToolTip = "Design Waste Calculation Wizard"
        'AddHandler Updatebtn.Click, AddressOf Update_Click
    End Sub

#End Region

#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_PAGES_ECON1_ASSUMPTIONS_OPERATIONSIN")
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
        Dim ImgBut As New ImageButton
        Dim ImgDis As New ImageButton
        Try
            ds = objGetData.GetOperationInDetails(CaseId)
            For i = 1 To 13
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "20px", "", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 2
                        HeaderTdSetting(tdHeader, "140px", "Equipment Description", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 3
                        HeaderTdSetting(tdHeader, "100px", "Number of Assets", "1")
                        Header2TdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 4
                        Title = "(" + ds.Tables(0).Rows(0).Item("WEBWIDTHUNIT").ToString() + ")"
                        HeaderTdSetting(tdHeader, "", "Web Width/Cavities", "3")
                        Header2TdSetting(tdHeader1, "", "Suggested", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 5
                        Title = "(" + ds.Tables(0).Rows(0).Item("WEBWIDTHUNIT").ToString() + ")"
                        Header2TdSetting(tdHeader1, "100px", "Preferred", "1")
                        trHeader1.Controls.Add(tdHeader1)
                    Case 6
                        Title = "" ' "(" + ds.Tables(0).Rows(0).Item("WEBWIDTHUNIT").ToString() + ")"
                        Header2TdSetting(tdHeader1, "", "Units", "1")
                        trHeader1.Controls.Add(tdHeader1)
                    Case 7
                        HeaderTdSetting(tdHeader, "100px", "Maximum Annual Run Time", "1")
                        Header2TdSetting(tdHeader1, "", "(hours)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 8
                        HeaderTdSetting(tdHeader, "", "Instantaneous Rate", "3")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        Header2TdSetting(tdHeader2, "100px", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 9
                        Header2TdSetting(tdHeader1, "60px", "Units", "1")
                        trHeader1.Controls.Add(tdHeader1)
                    Case 10
                        Title = "(" + ds.Tables(0).Rows(0).Item("INSTRUNIT").ToString() + "/hr)"
                        Header2TdSetting(tdHeader1, "70px", Title, "1")
                        trHeader1.Controls.Add(tdHeader1)
                    Case 11
                        HeaderTdSetting(tdHeader, "100px", "Downtime", "1")
                        Header2TdSetting(tdHeader1, "", "(%)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 12
                        HeaderTdSetting(tdHeader, "100px", "Production Waste", "1")
                        Header2TdSetting(tdHeader1, "", "(%)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 13
                        HeaderTdSetting(tdHeader, "100px", "Design Waste", "1")
                        Header2TdSetting(tdHeader1, "", "(%)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)

                End Select


            Next
            tblComparision.Controls.Add(trHeader)
            tblComparision.Controls.Add(trHeader1)
            'tblComparision.Controls.Add(trHeader2)

            'Inner()
            For i = 1 To 30


                trInner = New TableRow
                For j = 1 To 13
                    tdInner = New TableCell

                    Select Case j
                        Case 1
                            'Layer
                            InnerTdSetting(tdInner, "", "Center")
                            tdInner.Text = "<b>" + i.ToString() + "</b>"
                            trInner.Controls.Add(tdInner)
                        Case 2
                            'InnerTdSetting(tdInner, "", "left")
                            'lbl = New Label
                            'lbl.Text = ds.Tables(0).Rows(0).Item("EQUIPDES" + i.ToString() + "").ToString()
                            'lbl.Width = 140
                            'tdInner.Controls.Add(lbl)
                            'trInner.Controls.Add(tdInner)
                            InnerTdSetting(tdInner, "", "left")
                            lbl = New Label
                            If ds.Tables(0).Rows(0).Item("LEQUIPDES" + i.ToString() + "").ToString() <> "" Then
                                lbl.Text = ds.Tables(0).Rows(0).Item("LEQUIPDES" + i.ToString() + "").ToString()
                            Else
                                lbl.Text = ds.Tables(0).Rows(0).Item("EQUIPDES" + i.ToString() + "").ToString()
                            End If
                            'lbl.Text = ds.Tables(0).Rows(0).Item("EQUIPDES" + i.ToString() + "").ToString()
                            lbl.Width = 140
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 3
                            InnerTdSetting(tdInner, "", "left")
                            lbl = New Label
                            lbl.Text = ds.Tables(0).Rows(0).Item("NUM" + i.ToString() + "").ToString()
                            lbl.Width = 100
                            lbl.Style.Add("Text-align", "Center")
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 4
                            InnerTdSetting(tdInner, "", "Center")
                            lbl = New Label
                            If ds.Tables(0).Rows(0).Item("EqUnit" + i.ToString() + "").ToString() = "fpm" Then
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("OPWEBWIDTHSUGG" + i.ToString() + "").ToString(), 2)
                                tdInner.Controls.Add(lbl)
                            ElseIf ds.Tables(0).Rows(0).Item("EqUnit" + i.ToString() + "").ToString() = "cpm" Then
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("OPEXITSSUGG" + i.ToString() + "").ToString(), 2)
                                tdInner.Controls.Add(lbl)
                            End If
                            lbl.Width = 70
                            trInner.Controls.Add(tdInner)
                        Case 5
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.Width = 70
                            If ds.Tables(0).Rows(0).Item("EqUnit" + i.ToString() + "").ToString() = "fpm" Then
                                txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("OPWEBWIDTHPREF" + i.ToString() + "").ToString(), 2)
                                txt.ID = "OPWebWidth" + i.ToString()
                                tdInner.Controls.Add(txt)
                            ElseIf ds.Tables(0).Rows(0).Item("EqUnit" + i.ToString() + "").ToString() = "cpm" Then
                                txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("OPEXITSPREF" + i.ToString() + "").ToString(), 2)
                                txt.ID = "OPExits" + i.ToString()
                                tdInner.Controls.Add(txt)
                            End If
                            txt.MaxLength = 6
                            trInner.Controls.Add(tdInner)
                        Case 6
                            InnerTdSetting(tdInner, "", "Center")
                            lbl = New Label
                            If ds.Tables(0).Rows(0).Item("EqUnit" + i.ToString() + "").ToString() = "fpm" Then
                                lbl.Text = "(" + ds.Tables(0).Rows(0).Item("WEBWIDTHUNIT").ToString() + ")"
                            ElseIf ds.Tables(0).Rows(0).Item("EqUnit" + i.ToString() + "").ToString() = "cpm" Then
                                lbl.Text = "(#)"
                            End If
                            tdInner.Controls.Add(lbl)
                            lbl.Width = 40
                            trInner.Controls.Add(tdInner)
                        Case 7
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.Width = 70
                            txt.ID = "OMAXRH" + i.ToString()
                            txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("OMAXRH" + i.ToString() + "").ToString(), 0)
                            txt.MaxLength = 6
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 8
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.Width = 70
                            txt.ID = "OPINSTR" + i.ToString()
                            txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("OPINSTR" + i.ToString() + "").ToString(), 2)
                            txt.MaxLength = 6
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 9
                            InnerTdSetting(tdInner, "", "Center")
                            lbl = New Label
                            lbl.Text = ds.Tables(0).Rows(0).Item("Unit" + i.ToString() + "").ToString()
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 10
                            InnerTdSetting(tdInner, "", "Center")
                            lbl = New Label
                            lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("INSTR" + i.ToString() + "").ToString(), 2)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 11
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.Width = 70
                            txt.ID = "DT" + i.ToString()
                            txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("DT" + i.ToString() + "").ToString(), 1)
                            txt.MaxLength = 6
                            ImgBut = New ImageButton
                            ImgBut.ID = "imgPriceBut" + i.ToString()
                            ImgBut.Width = 6
                            ImgBut.Height = 6
                            ImgDis = New ImageButton
                            ImgDis.ID = "imgPriceDis" + i.ToString()
                            ImgDis.Width = 6
                            ImgDis.Height = 6
                            If ds.Tables(0).Rows(0).Item("ASSETID" + i.ToString() + "").ToString() <> "0" Then
                                ImgBut.ImageUrl = "~/Images/595958.png"
                                ImgDis.ImageUrl = "~/Images/595958-b.png"
                                ImgBut.Attributes.Add("style", "float:right; margin-bottom:5px; Display:inline;")
                                ImgDis.Attributes.Add("style", "position: relative; top: 10px; left: 90px; display:inline;")
                                ImgBut.Attributes.Add("onclick", "javascript:window.open('../PopUp/DownTimeInfo.aspx?EqId=" + ds.Tables(0).Rows(0).Item("ASSETID" + i.ToString() + "").ToString() + "','_blank'); return false;")
                                ImgDis.Attributes.Add("onclick", "javascript:window.open('../PopUp/" + ds.Tables(0).Rows(0).Item("PAGEURL" + i.ToString() + "").ToString() + "?EqId=" + ds.Tables(0).Rows(0).Item("ASSETID" + i.ToString() + "").ToString() + "&RowNum=" + i.ToString() + "','_blank'); return false;")
				If Session("E1UserRole") = "AADMIN" Then
                                	ImgBut.Visible = True
                                	ImgDis.Visible = True
                                	tdInner.Controls.Add(ImgBut)
                            	Else
                                	ImgBut.Visible = False
                                	ImgDis.Visible = False
                            	End If
                            	If ds.Tables(0).Rows(0).Item("ISASSIST" + i.ToString() + "").ToString() <> "N" Then
                                	tdInner.Controls.Add(ImgDis)
                            	End If
                            End If
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 12
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.Width = 70
                            txt.ID = "OPWASTE" + i.ToString()
                            txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("OPWASTE" + i.ToString() + "").ToString(), 1)
                            txt.MaxLength = 6
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 13
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.Width = 70
                            txt.ID = "DWASTE" + i.ToString()
                            txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("DESIGNWAST" + i.ToString() + "").ToString(), 1)
                            txt.MaxLength = 6
                            tdInner.Controls.Add(txt)
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




        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetEquipmentInDetails(ByRef LinkMat As HyperLink, ByVal hid As HiddenField, ByVal EqId As Integer)
        Dim Ds As New DataSet
        Dim ObjGetdata As New E1GetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty
        Try

            Ds = ObjGetdata.GetEquipment(EqId, "", "")
            LinkMat.Text = Ds.Tables(0).Rows(0).Item("equipDES").ToString()
            hid.Value = EqId.ToString()
            Path = "../PopUp/GetEquipmentPopUp.aspx?Des=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + LinkMat.ClientID + "&Id=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + hid.ClientID + ""
            LinkMat.NavigateUrl = "javascript:ShowPopWindow('" + Path + "')"

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

    Protected Sub Update_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim OPWebWidth(29) As String
        Dim OPExits(29) As String
        Dim OMAXRH(29) As String
        Dim OPINSTR(29) As String
        Dim DT(29) As String
        Dim OPWASTE(29) As String
        Dim DWASTE(29) As String
        Dim EqUnit(29) As String
        Dim ds As New DataSet
        Dim objGetData As New E1GetData.Selectdata
        ds = objGetData.GetOperationInDetails(CaseId)
        Dim obj As New CryptoHelper
        Dim k As Integer
        For k = 1 To 30
            EqUnit(k - 1) = ds.Tables(0).Rows(0).Item("EqUnit" + k.ToString()).ToString()
        Next

        Dim i As New Integer
        Dim ObjUpIns As New E1UpInsData.UpdateInsert()
        Try
            If Not objRefresh.IsRefresh Then
                For i = 1 To 30
                    'OPWebWidth(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$OPWebWidth" + i.ToString() + "")
                    If EqUnit(i - 1) = "fpm" Then
                        OPWebWidth(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$OPWebWidth" + i.ToString() + "")
                    ElseIf EqUnit(i - 1) = "cpm" Then
                        OPExits(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$OPExits" + i.ToString() + "")
                    End If
                    OMAXRH(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$OMAXRH" + i.ToString() + "")
                    OPINSTR(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$OPINSTR" + i.ToString() + "")
                    DT(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$DT" + i.ToString() + "")
                    OPWASTE(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$OPWASTE" + i.ToString() + "")
                    DWASTE(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$DWASTE" + i.ToString() + "")
                    'Check For IsNumric
                    Try

                        If EqUnit(i - 1) = "fpm" Then
                            If Not IsNumeric(OPWebWidth(i - 1)) Then
                                Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                            End If
                        ElseIf EqUnit(i - 1) = "cpm" Then
                            If Not IsNumeric(OPExits(i - 1)) Then
                                Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                            End If
                        End If
                        'Request.Form("ctl00$Econ1ContentPlaceHolder$OPWebWidth" + i.ToString() + "").ToString()
                        'If Not IsNumeric(OPWebWidth(i - 1)) Then
                        '    Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        'End If
                    Catch ex As Exception

                    End Try
                    If Not IsNumeric(OMAXRH(i - 1)) Then
                        Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                    End If

                    If Not IsNumeric(OPINSTR(i - 1)) Then
                        Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                    End If

                    If Not IsNumeric(DT(i - 1)) Then
                        Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                    End If

                    If Not IsNumeric(OPWASTE(i - 1)) Then
                        Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                    End If

                    If Not IsNumeric(DWASTE(i - 1)) Then
                        Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                    End If



                Next
                ObjUpIns.OperationInUpdate(CaseId, OPWebWidth, OPExits, OMAXRH, OPINSTR, DT, OPWASTE, DWASTE, EqUnit)
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
        Dim OPWebWidth(29) As String
        Dim OPExits(29) As String
        Dim OMAXRH(29) As String
        Dim OPINSTR(29) As String
        Dim DT(29) As String
        Dim OPWASTE(29) As String
        Dim DWASTE(29) As String
        Dim EqUnit(29) As String
        Dim ds As New DataSet
        Dim objGetData As New E1GetData.Selectdata
        ds = objGetData.GetOperationInDetails(CaseId)
        Dim obj As New CryptoHelper
        Dim k As Integer
        For k = 1 To 30
            EqUnit(k - 1) = ds.Tables(0).Rows(0).Item("EqUnit" + k.ToString()).ToString()
        Next
        Dim i As New Integer
        Dim ObjUpIns As New E1UpInsData.UpdateInsert()
        Try
            For i = 1 To 30
                'OPWebWidth(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$OPWebWidth" + i.ToString() + "")
                If EqUnit(i - 1) = "fpm" Then
                    OPWebWidth(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$OPWebWidth" + i.ToString() + "")
                ElseIf EqUnit(i - 1) = "cpm" Then
                    OPExits(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$OPExits" + i.ToString() + "")
                End If
                OMAXRH(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$OMAXRH" + i.ToString() + "")
                OPINSTR(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$OPINSTR" + i.ToString() + "")
                DT(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$DT" + i.ToString() + "")
                OPWASTE(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$OPWASTE" + i.ToString() + "")
                DWASTE(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$DWASTE" + i.ToString() + "")
                'Check For IsNumric
                Try

                    If EqUnit(i - 1) = "fpm" Then
                        If Not IsNumeric(OPWebWidth(i - 1)) Then
                            Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        End If
                    ElseIf EqUnit(i - 1) = "cpm" Then
                        If Not IsNumeric(OPExits(i - 1)) Then
                            Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        End If
                    End If
                    'Request.Form("ctl00$Econ1ContentPlaceHolder$OPWebWidth" + i.ToString() + "").ToString()
                    'If Not IsNumeric(OPWebWidth(i - 1)) Then
                    '    Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                    'End If
                Catch ex As Exception

                End Try
                If Not IsNumeric(OMAXRH(i - 1)) Then
                    Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                End If

                If Not IsNumeric(OPINSTR(i - 1)) Then
                    Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                End If

                If Not IsNumeric(DT(i - 1)) Then
                    Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                End If

                If Not IsNumeric(OPWASTE(i - 1)) Then
                    Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                End If

                If Not IsNumeric(DWASTE(i - 1)) Then
                    Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
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
                        ObjUpIns.OperationInUpdate(BCaseId, OPWebWidth, OPExits, OMAXRH, OPINSTR, DT, OPWASTE, DWASTE, EqUnit)
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
            'End Updating Bulk Model
            loading.Style.Add("display", "none")
            lnkSelBulkModel.Visible = True
            GetPageDetails()
        Catch ex As Exception
            ErrorLable.Text = "Error:btnUpdateBulk_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnUpdateBulk1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpdateBulk1.Click
        Dim OPWebWidth(29) As String
        Dim OPExits(29) As String
        Dim OMAXRH(29) As String
        Dim OPINSTR(29) As String
        Dim DT(29) As String
        Dim OPWASTE(29) As String
        Dim DWASTE(29) As String
        Dim EqUnit(29) As String
        Dim ds As New DataSet
        Dim objGetData As New E1GetData.Selectdata
        ds = objGetData.GetOperationInDetails(CaseId)
        Dim obj As New CryptoHelper
        Dim k As Integer
        For k = 1 To 30
            EqUnit(k - 1) = ds.Tables(0).Rows(0).Item("EqUnit" + k.ToString()).ToString()
        Next

        Dim i As New Integer
        Dim ObjUpIns As New E1UpInsData.UpdateInsert()
        Try
            For i = 1 To 30
                'OPWebWidth(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$OPWebWidth" + i.ToString() + "")
                If EqUnit(i - 1) = "fpm" Then
                    OPWebWidth(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$OPWebWidth" + i.ToString() + "")
                ElseIf EqUnit(i - 1) = "cpm" Then
                    OPExits(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$OPExits" + i.ToString() + "")
                End If
                OMAXRH(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$OMAXRH" + i.ToString() + "")
                OPINSTR(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$OPINSTR" + i.ToString() + "")
                DT(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$DT" + i.ToString() + "")
                OPWASTE(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$OPWASTE" + i.ToString() + "")
                DWASTE(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$DWASTE" + i.ToString() + "")
                'Check For IsNumric
                Try

                    If EqUnit(i - 1) = "fpm" Then
                        If Not IsNumeric(OPWebWidth(i - 1)) Then
                            Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        End If
                    ElseIf EqUnit(i - 1) = "cpm" Then
                        If Not IsNumeric(OPExits(i - 1)) Then
                            Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        End If
                    End If
                    'Request.Form("ctl00$Econ1ContentPlaceHolder$OPWebWidth" + i.ToString() + "").ToString()
                    'If Not IsNumeric(OPWebWidth(i - 1)) Then
                    '    Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                    'End If
                Catch ex As Exception

                End Try
                If Not IsNumeric(OMAXRH(i - 1)) Then
                    Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                End If

                If Not IsNumeric(OPINSTR(i - 1)) Then
                    Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                End If

                If Not IsNumeric(DT(i - 1)) Then
                    Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                End If

                If Not IsNumeric(OPWASTE(i - 1)) Then
                    Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                End If

                If Not IsNumeric(DWASTE(i - 1)) Then
                    Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
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
                        ObjUpIns.OperationInUpdate(BCaseId, OPWebWidth, OPExits, OMAXRH, OPINSTR, DT, OPWASTE, DWASTE, EqUnit)
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
            'End Updating Bulk Model
            loading.Style.Add("display", "none")
            lnkSelBulkModel.Visible = True
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
