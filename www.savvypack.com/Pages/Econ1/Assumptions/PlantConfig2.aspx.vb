Imports System.Data
Imports System.Data.OleDb
Imports System
Imports E1GetData
Imports E1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Econ1_Assumptions_PlantConfig2
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
        Notesbtn.OnClientClick = "return Notes('PLANTCONFIG');"
        'AddHandler Updatebtn.Click, AddressOf Update_Click
    End Sub

    Protected Sub GetMainHeadingdiv()
        MainHeading = Page.Master.FindControl("divMainHeading")
        MainHeading.Attributes.Add("onmouseover", "Tip('Plant Space Assumptions')")
        MainHeading.Attributes.Add("onmouseout", "UnTip()")
        MainHeading.InnerHtml = "Econ1 - Plant Space Assumptions"
    End Sub

    Protected Sub GetContentPlaceHolder()
        ctlContentPlaceHolder = Page.Master.FindControl("Econ1ContentPlaceHolder")
    End Sub

#End Region

#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_PAGES_ECON1_ASSUMPTIONS_PLANTCONFIG2")
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

        Try
            ds = objGetData.GetPlantConfig2Details(CaseId)
            For i = 1 To 2
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "70px", "Space Type", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 2
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE7").ToString() + ")"
                        HeaderTdSetting(tdHeader, "120px", "Area", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)

                End Select
            Next
            tblComparision.Controls.Add(trHeader)
            tblComparision.Controls.Add(trHeader2)
            tblComparision.Controls.Add(trHeader1)


            'Inner
            For i = 1 To 8
                If i > 1 And i < 6 Then
                Else
                    trInner = New TableRow
                    For j = 1 To 2
                        tdInner = New TableCell


                        Select Case j
                            Case 1
                                InnerTdSetting(tdInner, "", "left")
                                lbl = New Label
                                lbl.CssClass = "NormalLable"
                                lbl.Text = ds.Tables(0).Rows(0).Item("PSPACE" + i.ToString() + "").ToString()

                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)


                            Case 2
                                InnerTdSetting(tdInner, "", "Center")
                                If i = 1 Then
                                    InnerTdSetting(tdInner, "", "Right")
                                    lbl = New Label

                                    lbl.Text = "<span style='margin-right:10px'>" + FormatNumber(ds.Tables(0).Rows(0).Item("AR" + i.ToString() + "").ToString(), 3) + "</span>"


                                    tdInner.Controls.Add(lbl)
                                Else
                                    txt = New TextBox
                                    txt.CssClass = "SmallTextBox"
                                    txt.Width = 90
                                    txt.ID = "AR" + (i - 4).ToString()
                                    txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("AR" + (i - 4).ToString() + "").ToString(), 3)
                                    txt.MaxLength = 12
                                    tdInner.Controls.Add(txt)
                                End If

                                trInner.Controls.Add(tdInner)

                        End Select
                    Next
                End If
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
                tblComparision.Controls.Add(trInner)
            Next

            'Total
            trInner = New TableRow
            For i = 1 To 2
                tdInner = New TableCell
                Select Case i
                    Case 1
                        InnerTdSetting(tdInner, "", "Left")
                        tdInner.Text = "<span class='CalculatedFeilds'><b> Total </b></span>"
                        trInner.Controls.Add(tdInner)
                    Case 2
                        InnerTdSetting(tdInner, "", "Right")
                        tdInner.Text = "<span style='margin-right:8px'  class='CalculatedFeilds'><b>" + FormatNumber(ds.Tables(0).Rows(0).Item("ARTOT").ToString(), 3) + " </b> </span>"
                        trInner.Controls.Add(tdInner)
                End Select
            Next
            trInner.Height = 30
            trInner.CssClass = "AlterNateColor1"
            tblComparision.Controls.Add(trInner)

            'Lease calculations
            trHeader = New TableRow
            trHeader2 = New TableRow
            trHeader1 = New TableRow

            For i = 1 To 4
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "120px", "Lease Type", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 2
                        Dim TITLE1 As String
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE2") + Title.ToString() + ")"
                        TITLE1 = "(" + ds.Tables(0).Rows(0).Item("TITLE2") + "/" + ds.Tables(0).Rows(0).Item("TITLE7") + ")"
                        HeaderTdSetting(tdHeader, "", "Lease Cost " + Title, "2")
                        Header2TdSetting(tdHeader2, "90px", "Sugessted", "1")
                        Header2TdSetting(tdHeader1, "90px", TITLE1, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 3
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE2") + "/" + ds.Tables(0).Rows(0).Item("TITLE7") + ")"
                        Header2TdSetting(tdHeader2, "90px", "Preferred", "1")
                        Header2TdSetting(tdHeader1, "90px", Title, "1")
                        trHeader2.Controls.Add(tdHeader2)
                    Case 4
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE2") + Title.ToString() + ")"
                        HeaderTdSetting(tdHeader, "100px", "Total Lease", "1")
                        Header2TdSetting(tdHeader2, "100px", "", "1")
                        Header2TdSetting(tdHeader1, "100px", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)

                End Select
                trHeader1.Controls.Add(tdHeader1)
            Next
            tblComparision1.Controls.Add(trHeader)
            tblComparision1.Controls.Add(trHeader2)
            tblComparision1.Controls.Add(trHeader1)

            For i = 1 To 8
                trInner = New TableRow()
                For j = 1 To 4
                    tdInner = New TableCell()
                    Select Case j
                        Case 1
                            InnerTdSetting(tdInner, "", "Left")
                            If i > 1 And i < 6 Then
                                tdInner.Text = "<span style='margin-left:15px;'>" + ds.Tables(0).Rows(0).Item("PSPACE" + i.ToString() + "").ToString() + "</span>"
                                If i = 5 Then
                                    tdInner.Font.Bold = True
                                Else
                                    tdInner.Font.Bold = False
                                End If
                            Else
                                tdInner.Text = "<span style='font-weight:normal;'>" + ds.Tables(0).Rows(0).Item("PSPACE" + i.ToString() + "").ToString() + "</span>"
                            End If
                            trInner.Controls.Add(tdInner)
                        Case 2
                            InnerTdSetting(tdInner, "", "right")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            'If i > 1 Then
                            '    lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("SUG" + (i - 1).ToString() + "").ToString(), 3)
                            'Else
                            '    lbl.Text = ""
                            'End If
                            Select Case i
                                Case 2, 3, 4
                                    lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("SUG" + (i - 1).ToString() + "").ToString(), 3)
                                Case 6, 7, 8
                                    lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("SUG" + (i - 2).ToString() + "").ToString(), 3)
                                Case Else
                                    lbl.Text = ""
                            End Select
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 3
                            InnerTdSetting(tdInner, "", "Center")
                            'If i > 1 Then
                            '    txt = New TextBox
                            '    txt.CssClass = "SmallTextBox"
                            '    txt.Width = 50
                            '    txt.ID = "PREF" + (i - 1).ToString()
                            '    txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("PREF" + (i - 1).ToString() + "").ToString(), 3)
                            '    txt.MaxLength = 6
                            '    tdInner.Controls.Add(txt)
                            '    trInner.Controls.Add(tdInner)
                            'Else
                            '    InnerTdSetting(tdInner, "", "right")
                            '    lbl = New Label
                            '    lbl.CssClass = "NormalLable"
                            '    tdInner.Controls.Add(lbl)
                            '    trInner.Controls.Add(tdInner)
                            'End If
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.Width = 90
                            Select Case i
                                Case 2, 3, 4
                                    txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("PREF" + (i - 1).ToString() + "").ToString(), 3)
                                    txt.MaxLength = 10

                                    tdInner.Controls.Add(txt)
                                    trInner.Controls.Add(tdInner)
                                    txt.ID = "PREF" + (i - 1).ToString()
                                Case 6, 7, 8
                                    txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("PREF" + (i - 2).ToString() + "").ToString(), 3)
                                    txt.MaxLength = 10
                                    tdInner.Controls.Add(txt)
                                    trInner.Controls.Add(tdInner)
                                    txt.ID = "PREF" + (i - 2).ToString()
                                Case Else
                                    InnerTdSetting(tdInner, "", "right")
                                    lbl = New Label
                                    lbl.CssClass = "NormalLable"
                                    tdInner.Controls.Add(lbl)
                                    trInner.Controls.Add(tdInner)
                            End Select
                        Case 4
                            InnerTdSetting(tdInner, "", "right")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            'If i = 3 Then
                            '    lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("PRODTOTAL").ToString(), 0)
                            'ElseIf i = 5 Then
                            '    lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("WARHTOTAL").ToString(), 0)
                            'ElseIf i = 6 Then
                            '    lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("OFFTOTAL").ToString(), 0)
                            'ElseIf i = 7 Then
                            '    lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("SUPPTOTAL").ToString(), 0)
                            'Else
                            '    lbl.Text = ""
                            'End If
                            If i <> 1 Then
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("PTOT" + (i - 1).ToString() + "").ToString(), 0)
                                If i = 5 Then
                                    lbl.Font.Bold = True
                                Else
                                    lbl.Font.Bold = False
                                End If
                            Else
                                lbl.Text = ""
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)

                    End Select

                Next
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor2"
                Else
                    trInner.CssClass = "AlterNateColor1"
                End If
                tblComparision1.Controls.Add(trInner)
            Next
            'Total
            trInner = New TableRow
            For i = 1 To 4
                tdInner = New TableCell
                Select Case i
                    Case 1
                        InnerTdSetting(tdInner, "", "Left")
                        tdInner.Text = "<span class='CalculatedFeilds'><b> Total </b></span>"
                        trInner.Controls.Add(tdInner)
                    Case 4
                        InnerTdSetting(tdInner, "", "Right")
                        tdInner.Text = "<span class='CalculatedFeilds'><b>" + FormatNumber(ds.Tables(0).Rows(0).Item("LEASETOTAL").ToString(), 0) + " </b> </span>"
                        trInner.Controls.Add(tdInner)
                    Case 2, 3
                        InnerTdSetting(tdInner, "", "Left")
                        tdInner.Text = ""
                        trInner.Controls.Add(tdInner)
                End Select
            Next
            trInner.Height = 30
            trInner.CssClass = "AlterNateColor1"
            tblComparision1.Controls.Add(trInner)










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

        Dim AREA(2) As String
        Dim PrefLease(5) As String
        Dim obj As New CryptoHelper
        Dim i As New Integer
        Dim ObjUpIns As New E1UpInsData.UpdateInsert()
        Try
            If Not objRefresh.IsRefresh Then
                For i = 2 To 4
                    AREA(i - 2) = Request.Form("ctl00$Econ1ContentPlaceHolder$AR" + i.ToString() + "")
                    'Check For IsNumric
                    If Not IsNumeric(AREA(i - 2)) Then
                        Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                    End If
                Next

                For i = 0 To 5
                    PrefLease(i) = Request.Form("ctl00$Econ1ContentPlaceHolder$PREF" + (i + 1).ToString() + "")
                    'Check For IsNumric
                    If Not IsNumeric(PrefLease(i)) Then
                        Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                    End If
                Next

                ObjUpIns.PlantConfig2Update(CaseId, AREA, PrefLease)
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

        Dim AREA(2) As String
        Dim PrefLease(5) As String
        Dim obj As New CryptoHelper
        Dim i As New Integer
        Dim ObjUpIns As New E1UpInsData.UpdateInsert()
        Try
            For i = 2 To 4
                AREA(i - 2) = Request.Form("ctl00$Econ1ContentPlaceHolder$AR" + i.ToString() + "")
                'Check For IsNumric
                If Not IsNumeric(AREA(i - 2)) Then
                    Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                End If
            Next

            For i = 0 To 5
                PrefLease(i) = Request.Form("ctl00$Econ1ContentPlaceHolder$PREF" + (i + 1).ToString() + "")
                'Check For IsNumric
                If Not IsNumeric(PrefLease(i)) Then
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
                        ObjUpIns.PlantConfig2Update(BCaseId, AREA, PrefLease)
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

        Dim AREA(2) As String
        Dim PrefLease(5) As String
        Dim obj As New CryptoHelper
        Dim i As New Integer
        Dim ObjUpIns As New E1UpInsData.UpdateInsert()
        Try
            For i = 2 To 4
                AREA(i - 2) = Request.Form("ctl00$Econ1ContentPlaceHolder$AR" + i.ToString() + "")
                'Check For IsNumric
                If Not IsNumeric(AREA(i - 2)) Then
                    Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                End If
            Next

            For i = 0 To 5
                PrefLease(i) = Request.Form("ctl00$Econ1ContentPlaceHolder$PREF" + (i + 1).ToString() + "")
                'Check For IsNumric
                If Not IsNumeric(PrefLease(i)) Then
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
                        ObjUpIns.PlantConfig2Update(BCaseId, AREA, PrefLease)
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
