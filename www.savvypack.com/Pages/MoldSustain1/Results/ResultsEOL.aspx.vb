﻿Imports System.Data
Imports System.Data.OleDb
Imports System
Imports MoldS1GetData
Imports MoldS1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_MoldSustain1_Results_ResultsEOL
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
        Notesbtn.Visible = False
        'AddHandler Updatebtn.Click, AddressOf Update_Click
    End Sub

    Protected Sub GetMainHeadingdiv()
        MainHeading = Page.Master.FindControl("divMainHeading")
        MainHeading.Attributes.Add("onmouseover", "Tip('Material End-of-life Results Statement')")
        MainHeading.Attributes.Add("onmouseout", "UnTip()")
        MainHeading.InnerHtml = "Sustain1 Mold - Material End-of-life Results Statement "
    End Sub

    Protected Sub GetContentPlaceHolder()
        ctlContentPlaceHolder = Page.Master.FindControl("MoldSustain1ContentPlaceHolder")
    End Sub

#End Region

#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_PAGES_SUSTAIN1_RESULTS_RESULTSEOL")
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
            CaseId = Session("MoldS1CaseId")
            UserRole = Session("MoldS1UserRole")
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetSessionDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetPageDetails()
        Dim ds As New DataSet
        Dim objGetData As New MoldS1GetData.Selectdata
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
            ds = objGetData.GetEOLResults(CaseId)



            trHeader = New TableRow
            trHeader2 = New TableRow
            trHeader1 = New TableRow

            For i = 1 To 6
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "150px", "", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 2
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                        HeaderTdSetting(tdHeader, "120px", "Material Balance", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 3
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                        HeaderTdSetting(tdHeader, "120px", "Material to Recycling", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 4
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                        HeaderTdSetting(tdHeader, "120px", "Material to Incineration", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 5
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                        HeaderTdSetting(tdHeader, "120px", "Material to Composting", "1")
                        HeaderTdSetting(tdHeader1, "100px", "", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 6
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                        HeaderTdSetting(tdHeader, "120px", "Material to Landfill", "1")
                        HeaderTdSetting(tdHeader1, "100px", "", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                End Select

            Next
            tblComparision.Controls.Add(trHeader)
            tblComparision.Controls.Add(trHeader2)

            lblSalesVol.Text = "<b>Sales Volume (" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + "):</b> " + FormatNumber(ds.Tables(0).Rows(0).Item("SVOLUME").ToString(), 0).ToString()
            lblSalesVolUnit.Text = "<b>Sales Volume (" + ds.Tables(0).Rows(0).Item("SUNIT").ToString().Replace("MJ/", "") + "):</b> " + FormatNumber(ds.Tables(0).Rows(0).Item("SUNITVAL").ToString(), 0).ToString()


            'Inner
            For i = 1 To 13
                trInner = New TableRow
                For j = 1 To 6
                    tdInner = New TableCell
                    Select Case j
                        Case 1
                            InnerTdSetting(tdInner, "150px", "Left")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            If i = 1 Or i = 5 Or i = 8 Or i = 11 Then
                                lbl.Text = "<b>" + ds.Tables(0).Rows(0).Item("PLANTSPACE" + i.ToString() + "").ToString() + "</b>"
                            Else
                                lbl.Text = "<b style='margin-left:20px;'>" + ds.Tables(0).Rows(0).Item("PLANTSPACE" + i.ToString() + "").ToString() + "</b>"
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 2
                            InnerTdSetting(tdInner, "120px", "Right")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            If i = 1 Or i = 5 Or i = 8 Or i = 11 Then
                                lbl.Text = ""
                            Else
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("MATB" + i.ToString() + "").ToString(), 0)
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 3
                            InnerTdSetting(tdInner, "120px", "Right")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            If i = 1 Or i = 2 Or i = 5 Or i = 8 Or i = 11 Then
                                lbl.Text = ""
                            Else
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("MATRE" + i.ToString() + "").ToString(), 0)
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 4
                            InnerTdSetting(tdInner, "120px", "Right")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            If i = 1 Or i = 2 Or i = 5 Or i = 8 Or i = 11 Then
                                lbl.Text = ""
                            Else
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("MATIN" + i.ToString() + "").ToString(), 0)
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 5
                            InnerTdSetting(tdInner, "120px", "Right")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            If i = 1 Or i = 2 Or i = 5 Or i = 8 Or i = 11 Then
                                lbl.Text = ""
                            Else
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("MATCM" + i.ToString() + "").ToString(), 0)
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 6
                            InnerTdSetting(tdInner, "120px", "Right")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            If i = 1 Or i = 2 Or i = 5 Or i = 8 Or i = 11 Then
                                lbl.Text = ""
                            Else
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("MATLF" + i.ToString() + "").ToString(), 0)
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)

                    End Select

                Next
                trInner.Height = 20
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
        Dim ObjUpIns As New MoldS1UpInsData.UpdateInsert()
        Try
            If Not objRefresh.IsRefresh Then
                Calculate()
                'Update Server Date
                ObjUpIns.ServerDateUpdate(CaseId, Session("MoldS1UserName"))
            End If
            GetPageDetails()
        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub Calculate()
        Try
            Dim MoldS1Conn As String = System.Configuration.ConfigurationManager.AppSettings("MoldS1ConnectionString")
            Dim MoldE1Conn As String = System.Configuration.ConfigurationManager.AppSettings("MoldE1ConnectionString")
            Dim obj As New MoldSustain1Calculation.MoldSustain1Calculations(MoldS1Conn, MoldE1Conn)
            obj.MoldSustain1Calculate(CaseId)
        Catch ex As Exception
            ErrorLable.Text = "Error:Calculate:" + ex.Message.ToString() + ""
        End Try
    End Sub

End Class