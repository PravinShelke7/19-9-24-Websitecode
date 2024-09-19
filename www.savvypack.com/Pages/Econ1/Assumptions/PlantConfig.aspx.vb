Imports System.Data
Imports System.Data.OleDb
Imports System
Imports E1GetData
Imports E1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Econ1_Assumptions_PlantConfig
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
        GetErrorLable()
        GetUpdatebtn()
        GetLogOffbtn()
        GetInstructionsbtn()
        GetChartbtn()
        GetFeedbackbtn()
        GetNotesbtn()
        GetMainHeadingdiv()
        GetContentPlaceHolder()
    End Sub

    Protected Sub GetErrorLable()
        ErrorLable = Page.Master.FindControl("lblError")
    End Sub

    Protected Sub GetUpdatebtn()
        Updatebtn = Page.Master.FindControl("imgUpdate")
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
        Notesbtn.OnClientClick = "return Notes('DEPTCONFIG');"
        'AddHandler Updatebtn.Click, AddressOf Update_Click
    End Sub

    Protected Sub GetMainHeadingdiv()
        MainHeading = Page.Master.FindControl("divMainHeading")
        MainHeading.Attributes.Add("onmouseover", "Tip('Plant Configuration')")
        MainHeading.Attributes.Add("onmouseout", "UnTip()")
        MainHeading.InnerHtml = "Econ1 - Plant Configuration"
    End Sub

    Protected Sub GetContentPlaceHolder()
        ctlContentPlaceHolder = Page.Master.FindControl("Econ1ContentPlaceHolder")
    End Sub

#End Region

#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_PAGES_ECON1_ASSUMPTIONS_PLANTCONFIG")
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
        'Dim trHeader As New TableRow
        Dim trHeader1 As New TableRow
        Dim trHeader2 As New TableRow
        Dim trInner1 As New TableRow
        Dim trInner2 As New TableRow
        Dim tdHeader1 As TableCell
        Dim lbl As New Label
        Dim hid As New HiddenField
        Dim Link As New HyperLink
        Dim txt As New TextBox
        Dim tdInner As TableCell
        Dim dsDept As New DataSet
        Try
            ds = objGetData.GetPlantConfigDetails(CaseId)
            dsDept = objGetData.GetDeptPlantConfig("-1", "", "")

            For i = 1 To 16
                tdHeader1 = New TableCell

                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader1, "100", "", "1")
                        trHeader1.Controls.Add(tdHeader1)
                    Case 2
                        HeaderTdSetting(tdHeader1, "", "Required Departments", "1")
                        trHeader1.Controls.Add(tdHeader1)
                    Case 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16
                        HeaderTdSetting(tdHeader1, "100px", "Next Process " + (i - 2).ToString() + "", "1")
                        trHeader2.Controls.Add(tdHeader1)



                End Select




            Next
            trHeader1.Height = 25
            trHeader2.Height = 25
            tblComparisionFix.Controls.Add(trHeader1)
            tblComparision.Controls.Add(trHeader2)


            'Inner
            For i = 1 To 15
                trInner1 = New TableRow
                trInner2 = New TableRow
                For j = 1 To 16
                    tdInner = New TableCell

                    Select Case j
                        Case 1
                            'Department
                            InnerTdSetting(tdInner, "", "Center")
                            tdInner.Text = "<b>" + i.ToString() + "</b>"
                            trInner1.Controls.Add(tdInner)
                            'tblComparisionFix.Controls.Add(trInner)
                        Case 2
                            InnerTdSetting(tdInner, "", "Left")
                            Link = New HyperLink
                            hid = New HiddenField
                            Link.ID = "hypDesDeptA" + i.ToString()
                            hid.ID = "hidIdDeptA" + i.ToString()
                            Link.Width = 130
                            Link.CssClass = "Link"
                            GetDeptDetails(Link, hid, CInt(ds.Tables(0).Rows(0).Item("DEPTA" + i.ToString() + "").ToString()), dsDept)
                            tdInner.Controls.Add(hid)
                            tdInner.Controls.Add(Link)
                            trInner1.Controls.Add(tdInner)

                        Case 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16
                            Dim Str As String = Chr(63 + i)
                            InnerTdSetting(tdInner, "", "Left")
                            Link = New HyperLink
                            hid = New HiddenField
                            Link.ID = "hypDesDept" + Chr(63 + j) + "" + i.ToString()
                            hid.ID = "hidIdDept" + Chr(63 + j) + "" + i.ToString()
                            Link.Width = 130
                            Link.CssClass = "Link"
                            GetDeptDetails(Link, hid, CInt(ds.Tables(0).Rows(0).Item("DEPT" + Chr(63 + j) + "" + i.ToString() + "").ToString()), dsDept)
                            tdInner.Controls.Add(hid)
                            tdInner.Controls.Add(Link)
                            trInner2.Controls.Add(tdInner)


                    End Select

                Next
                If (i Mod 2 = 0) Then
                    trInner1.CssClass = "AlterNateColor1"
                    trInner2.CssClass = "AlterNateColor1"
                Else
                    trInner1.CssClass = "AlterNateColor2"
                    trInner2.CssClass = "AlterNateColor2"
                End If
                trInner1.Height = 25
                trInner2.Height = 25
                tblComparisionFix.Controls.Add(trInner1)
                tblComparision.Controls.Add(trInner2)
            Next




        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetDeptDetails(ByRef LinkDep As HyperLink, ByVal hid As HiddenField, ByVal ProcId As Integer, ByVal dsDept As DataSet)
        Dim Ds As New DataSet
        Dim ObjGetdata As New E1GetData.Selectdata()
        Dim Path As String = String.Empty
        Dim dvDept As New DataView
        Dim dtDept As New DataTable
        Try
            'Ds = ObjGetdata.GetDeptPlantConfig(ProcId, "", "")

            dvDept = dsDept.Tables(0).DefaultView
            dvDept.RowFilter = "PROCID = " + ProcId.ToString()
            dtDept = dvDept.ToTable()

            Path = "../PopUp/GetDeptPlantConfig.aspx?Des=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + LinkDep.ClientID + "&Id=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + hid.ClientID + ""
            LinkDep.Text = dtDept.Rows(0).Item("PROCDE").ToString()
            hid.Value = ProcId.ToString()
            LinkDep.NavigateUrl = "javascript:ShowPopWindow('" + Path + "')"
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
        Dim DEPTA(14) As String
        Dim DEPTB(14) As String
        Dim DEPTC(14) As String
        Dim DEPTD(14) As String
        Dim DEPTE(14) As String
        Dim DEPTF(14) As String
        Dim DEPTG(14) As String
        Dim DEPTH(14) As String
        Dim DEPTI(14) As String
        Dim DEPTJ(14) As String
       
        Dim DEPTK(14) As String
        Dim DEPTL(14) As String
        Dim DEPTM(14) As String
        Dim DEPTN(14) As String
        Dim DEPTO(14) As String

        Dim i As New Integer
        Dim ObjUpIns As New E1UpInsData.UpdateInsert()
        Try
            If Not objRefresh.IsRefresh Then
                For i = 1 To 15
                    DEPTA(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$hidIdDeptA" + i.ToString() + "")
                    DEPTB(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$hidIdDeptB" + i.ToString() + "")
                    DEPTC(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$hidIdDeptC" + i.ToString() + "")
                    DEPTD(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$hidIdDeptD" + i.ToString() + "")
                    DEPTE(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$hidIdDeptE" + i.ToString() + "")
                    DEPTF(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$hidIdDeptF" + i.ToString() + "")
                    DEPTG(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$hidIdDeptG" + i.ToString() + "")
                    DEPTH(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$hidIdDeptH" + i.ToString() + "")
                    DEPTI(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$hidIdDeptI" + i.ToString() + "")
                    DEPTJ(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$hidIdDeptJ" + i.ToString() + "")

                    DEPTK(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$hidIdDeptK" + i.ToString() + "")
                    DEPTL(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$hidIdDeptL" + i.ToString() + "")
                    DEPTM(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$hidIdDeptM" + i.ToString() + "")
                    DEPTN(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$hidIdDeptN" + i.ToString() + "")
                    DEPTO(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$hidIdDeptO" + i.ToString() + "")
                Next
                'ObjUpIns.PlantConfigUpdate(CaseId, DEPTA, DEPTB, DEPTC, DEPTD, DEPTE, DEPTF, DEPTG, DEPTH, DEPTI, DEPTJ)
               ObjUpIns.PlantConfigUpdate(CaseId, DEPTA, DEPTB, DEPTC, DEPTD, DEPTE, DEPTF, DEPTG, DEPTH, DEPTI, DEPTJ, DEPTK, DEPTL, DEPTM, DEPTN, DEPTO)
            End If
            Calculate()
            'Update Server Date
            ObjUpIns.ServerDateUpdate(CaseId, Session("E1UserName"))
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
        Dim DEPTA(14) As String
        Dim DEPTB(14) As String
        Dim DEPTC(14) As String
        Dim DEPTD(14) As String
        Dim DEPTE(14) As String
        Dim DEPTF(14) As String
        Dim DEPTG(14) As String
        Dim DEPTH(14) As String
        Dim DEPTI(14) As String
        Dim DEPTJ(14) As String

        Dim DEPTK(14) As String
        Dim DEPTL(14) As String
        Dim DEPTM(14) As String
        Dim DEPTN(14) As String
        Dim DEPTO(14) As String

        Dim i As New Integer
        Dim ObjUpIns As New E1UpInsData.UpdateInsert()
        Try
            For i = 1 To 15
                DEPTA(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$hidIdDeptA" + i.ToString() + "")
                DEPTB(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$hidIdDeptB" + i.ToString() + "")
                DEPTC(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$hidIdDeptC" + i.ToString() + "")
                DEPTD(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$hidIdDeptD" + i.ToString() + "")
                DEPTE(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$hidIdDeptE" + i.ToString() + "")
                DEPTF(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$hidIdDeptF" + i.ToString() + "")
                DEPTG(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$hidIdDeptG" + i.ToString() + "")
                DEPTH(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$hidIdDeptH" + i.ToString() + "")
                DEPTI(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$hidIdDeptI" + i.ToString() + "")
                DEPTJ(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$hidIdDeptJ" + i.ToString() + "")

                DEPTK(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$hidIdDeptK" + i.ToString() + "")
                DEPTL(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$hidIdDeptL" + i.ToString() + "")
                DEPTM(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$hidIdDeptM" + i.ToString() + "")
                DEPTN(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$hidIdDeptN" + i.ToString() + "")
                DEPTO(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$hidIdDeptO" + i.ToString() + "")
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
                        'ObjUpIns.PlantConfigUpdate(BCaseId, DEPTA, DEPTB, DEPTC, DEPTD, DEPTE, DEPTF, DEPTG, DEPTH, DEPTI, DEPTJ)
                        ObjUpIns.PlantConfigUpdate(BCaseId, DEPTA, DEPTB, DEPTC, DEPTD, DEPTE, DEPTF, DEPTG, DEPTH, DEPTI, DEPTJ, DEPTK, DEPTL, DEPTM, DEPTN, DEPTO)
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
        Dim DEPTA(14) As String
        Dim DEPTB(14) As String
        Dim DEPTC(14) As String
        Dim DEPTD(14) As String
        Dim DEPTE(14) As String
        Dim DEPTF(14) As String
        Dim DEPTG(14) As String
        Dim DEPTH(14) As String
        Dim DEPTI(14) As String
        Dim DEPTJ(14) As String

        Dim DEPTK(14) As String
        Dim DEPTL(14) As String
        Dim DEPTM(14) As String
        Dim DEPTN(14) As String
        Dim DEPTO(14) As String

        Dim i As New Integer
        Dim ObjUpIns As New E1UpInsData.UpdateInsert()
        Try
            For i = 1 To 15
                DEPTA(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$hidIdDeptA" + i.ToString() + "")
                DEPTB(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$hidIdDeptB" + i.ToString() + "")
                DEPTC(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$hidIdDeptC" + i.ToString() + "")
                DEPTD(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$hidIdDeptD" + i.ToString() + "")
                DEPTE(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$hidIdDeptE" + i.ToString() + "")
                DEPTF(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$hidIdDeptF" + i.ToString() + "")
                DEPTG(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$hidIdDeptG" + i.ToString() + "")
                DEPTH(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$hidIdDeptH" + i.ToString() + "")
                DEPTI(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$hidIdDeptI" + i.ToString() + "")
                DEPTJ(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$hidIdDeptJ" + i.ToString() + "")

                DEPTK(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$hidIdDeptK" + i.ToString() + "")
                DEPTL(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$hidIdDeptL" + i.ToString() + "")
                DEPTM(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$hidIdDeptM" + i.ToString() + "")
                DEPTN(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$hidIdDeptN" + i.ToString() + "")
                DEPTO(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$hidIdDeptO" + i.ToString() + "")
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
                        'ObjUpIns.PlantConfigUpdate(BCaseId, DEPTA, DEPTB, DEPTC, DEPTD, DEPTE, DEPTF, DEPTG, DEPTH, DEPTI, DEPTJ)
                        ObjUpIns.PlantConfigUpdate(BCaseId, DEPTA, DEPTB, DEPTC, DEPTD, DEPTE, DEPTF, DEPTG, DEPTH, DEPTI, DEPTJ, DEPTK, DEPTL, DEPTM, DEPTN, DEPTO)
                        Calculate_Bulk(BCaseId)
                        'Update Server Date
                        ObjUpIns.ServerDateUpdate(BCaseId, Session("E1UserName"))
                    Catch ex As Exception
                    End Try
                Next
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alertMsg", "alert('Variables transferred and calculated successfully.');", True)
                'ScriptManager.RegisterStartupScript(Page, Me.GetType(), "alertMsg", "alert('Values successfully transfered and Calculated.');", True)
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
