Imports System.Data
Imports System.Data.OleDb
Imports System
Imports SDistGetData
Imports SDistUpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_SDistribution_Assumptions_PlantConfig
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
        MainHeading.InnerHtml = "SDistribution - Plant Configuration"
    End Sub

    Protected Sub GetContentPlaceHolder()
        ctlContentPlaceHolder = Page.Master.FindControl("SDistContentPlaceHolder")
    End Sub

#End Region

#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_PAGES_SDistribution_ASSUMPTIONS_PLANTCONFIG")
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
            CaseId = Session("SDistCaseId")
            UserRole = Session("SDistUserRole")
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetSessionDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetPageDetails()
        Dim ds As New DataSet
        Dim objGetData As New SDistGetData.Selectdata
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

        Try
            ds = objGetData.GetPlantConfigDetails(CaseId)
            For i = 1 To 11
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
                    Case 3, 4, 5, 6, 7, 8, 9, 10, 11
                        HeaderTdSetting(tdHeader1, "100px", "Next Process " + (i - 2).ToString() + "", "1")
                        trHeader2.Controls.Add(tdHeader1)



                End Select




            Next
            trHeader1.Height = 25
            trHeader2.Height = 25
            tblComparisionFix.Controls.Add(trHeader1)
            tblComparision.Controls.Add(trHeader2)


            'Inner
            For i = 1 To 10
                trInner1 = New TableRow
                trInner2 = New TableRow
                For j = 1 To 11
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
                            Link.ID = ("D" + Chr(64 + j - 1)) + i.ToString()
                            hid.ID = "hidDep" + ("D" + Chr(64 + j - 1)) + i.ToString()
                            Link.Width = 130
                            Link.CssClass = "Link"
                            Dim x As String
                            x = (ds.Tables(0).Rows(0).Item(("D" + Chr(64 + j - 1)) + i.ToString()).ToString())
                            GetDeptDetails(Link, hid, CInt(x).ToString())
                            tdInner.Controls.Add(hid)
                            tdInner.Controls.Add(Link)
                            trInner1.Controls.Add(tdInner)

                        Case 3, 4, 5, 6, 7, 8, 9, 10, 11
                            Dim Str As String = Chr(63 + i)
                            InnerTdSetting(tdInner, "", "Left")
                            Link = New HyperLink
                            hid = New HiddenField
                            Link.ID = ("D" + Chr(64 + j - 1)) + i.ToString()
                            hid.ID = "hidDep" + ("D" + Chr(64 + j - 1)) + i.ToString()
                            Link.Width = 130
                            Link.CssClass = "Link"
                            Dim x As String
                            x = (ds.Tables(0).Rows(0).Item(("D" + Chr(64 + j - 1)) + i.ToString()).ToString())
                            GetDeptDetails(Link, hid, CInt(x))
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

    Protected Sub GetDeptDetails(ByRef LinkDep As HyperLink, ByVal hid As HiddenField, ByVal ProcId As Integer)
        Dim Ds As New DataSet
        Dim ObjGetdata As New SDistGetData.Selectdata()
        Dim Path As String = String.Empty
        Try
            Ds = ObjGetdata.GetDeptPlantConfig(ProcId, "", "")
            Path = "../PopUp/GetDeptPlantConfig.aspx?Des=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + LinkDep.ClientID + "&Id=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + hid.ClientID + ""
            LinkDep.Text = Ds.Tables(0).Rows(0).Item("PROCDE").ToString()
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
        Dim DEPTA(9) As String
        Dim DEPTB(9) As String
        Dim DEPTC(9) As String
        Dim DEPTD(9) As String
        Dim DEPTE(9) As String
        Dim DEPTF(9) As String
        Dim DEPTG(9) As String
        Dim DEPTH(9) As String
        Dim DEPTI(9) As String
        Dim DEPTJ(9) As String


        Dim i As New Integer
        Dim ObjUpIns As New SDistUpInsData.UpdateInsert()
        Try
            If Not objRefresh.IsRefresh Then
                For i = 1 To 10
                    DEPTA(i - 1) = Request.Form("ctl00$SDistContentPlaceHolder$hidDepDA" + i.ToString())
                    DEPTB(i - 1) = Request.Form("ctl00$SDistContentPlaceHolder$hidDepDB" + i.ToString())
                    DEPTC(i - 1) = Request.Form("ctl00$SDistContentPlaceHolder$hidDepDC" + i.ToString())
                    DEPTD(i - 1) = Request.Form("ctl00$SDistContentPlaceHolder$hidDepDD" + i.ToString())
                    DEPTE(i - 1) = Request.Form("ctl00$SDistContentPlaceHolder$hidDepDE" + i.ToString())
                    DEPTF(i - 1) = Request.Form("ctl00$SDistContentPlaceHolder$hidDepDF" + i.ToString())
                    DEPTG(i - 1) = Request.Form("ctl00$SDistContentPlaceHolder$hidDepDG" + i.ToString())
                    DEPTH(i - 1) = Request.Form("ctl00$SDistContentPlaceHolder$hidDepDH" + i.ToString())
                    DEPTI(i - 1) = Request.Form("ctl00$SDistContentPlaceHolder$hidDepDI" + i.ToString())
                    DEPTJ(i - 1) = Request.Form("ctl00$SDistContentPlaceHolder$hidDepDJ" + i.ToString())

                Next
                ObjUpIns.PlantConfigUpdate(CaseId, DEPTA, DEPTB, DEPTC, DEPTD, DEPTE, DEPTF, DEPTG, DEPTH, DEPTI, DEPTJ)
            End If
            Calculate()
            GetPageDetails()
        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
    Protected Sub Calculate()
        Try
            Dim Econ1Conn As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            Dim Sustain1Conn As String = System.Configuration.ConfigurationManager.AppSettings("Sustain1ConnectionString")
            Dim EDistConn As String = System.Configuration.ConfigurationManager.AppSettings("DistributionConnectionString")
            Dim SDistConn As String = System.Configuration.ConfigurationManager.AppSettings("SDistributionConnectionString")
            Dim obj As New SDistributionCalculation.SDistCalculations(SDistConn, EDistConn, Sustain1Conn, Econ1Conn)
            obj.SDistCalculate(CaseId)
        Catch ex As Exception
            ErrorLable.Text = "Error:Calculate:" + ex.Message.ToString() + ""
        End Try
    End Sub

End Class
