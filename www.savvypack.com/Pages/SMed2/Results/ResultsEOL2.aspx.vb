Imports System.Data
Imports System.Data.OleDb
Imports System
Imports SMed2GetData
Imports SMed2UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_MedSustain2_Results_ResultsEOL2
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
    Dim _btnCompare As ImageButton
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
    Public Property Comparebtn() As ImageButton
        Get
            Return _btnCompare
        End Get
        Set(ByVal value As ImageButton)
            _btnCompare = value
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
        GetComparebtn()
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
        Updatebtn.Attributes.Add("onclick", "return checkNumericAll()")
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
    Protected Sub GetComparebtn()
        Dim obj As New CryptoHelper()
        Comparebtn = Page.Master.FindControl("imgCompare")
        Comparebtn.Visible = True
        Comparebtn.OnClientClick = "return Compare('Comparison.aspx?Type=" + obj.Encrypt("CustomerEOL") + "') "

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
        MainHeading.Attributes.Add("onmouseover", "Tip('Customer Material End-of-life Results Statement')")
        MainHeading.Attributes.Add("onmouseout", "UnTip()")
        MainHeading.InnerHtml = "SMed2 - Customer End-of-life Results Statement "
    End Sub

    Protected Sub GetContentPlaceHolder()
        ctlContentPlaceHolder = Page.Master.FindControl("SMed2ContentPlaceHolder")
    End Sub

#End Region

#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_PAGES_MEDSUSTAIN2_RESULTS_RESULTSEOL2")
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
            CaseId = Session("SMed2CaseId")
            UserRole = Session("SMed2UserRole")
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetSessionDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetPageDetails()
        Dim ds As New DataSet
        Dim objGetData As New SMed2GetData.Selectdata
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
        Dim customerSaleValue As Double
        Dim divVal As Double
        Try
            ds = objGetData.GetEOLResults(CaseId)

            If CDbl(ds.Tables(0).Rows(0).Item("CUSSALESUNIT").ToString()) = 0 Then
                customerSaleValue = CDbl(ds.Tables(0).Rows(0).Item("CUSSALESVOLUME").ToString()) * CDbl(ds.Tables(0).Rows(0).Item("Convwt").ToString())
            Else
                customerSaleValue = CDbl(ds.Tables(0).Rows(0).Item("CUSSALESVOLUME").ToString())
            End If
            lblNewSalesValue.Text = "Customer Sales Volume :"
            txtNewSaleValue.Text = FormatNumber(customerSaleValue, 0)



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
                        HeaderTdSetting(tdHeader, "240px", "", "1")
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
                        HeaderTdSetting(tdHeader, "110px", "Material to Recycling", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 4
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                        HeaderTdSetting(tdHeader, "110px", "Material to Incineration", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 5
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                        HeaderTdSetting(tdHeader, "110px", "Material to Composting", "1")
                        HeaderTdSetting(tdHeader1, "", "", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 6
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                        HeaderTdSetting(tdHeader, "100px", "Material to Landfill", "1")
                        HeaderTdSetting(tdHeader1, "", "", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                End Select

            Next
            tblComparision.Controls.Add(trHeader)
            tblComparision.Controls.Add(trHeader2)

            If ds.Tables(0).Rows(0).Item("VOLUNI1").ToString() <> "" Then
                lblSalesVol.Text = "<b>" + ds.Tables(0).Rows(0).Item("VOLUNI1").ToString() + ":</b> " + FormatNumber(ds.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString(), 0).ToString()
            End If
            If ds.Tables(0).Rows(0).Item("VOLUNI2").ToString() <> "" Then
                lblSalesVolUnit.Text = "<b>" + ds.Tables(0).Rows(0).Item("VOLUNI2").ToString() + ":</b> " + FormatNumber(ds.Tables(0).Rows(0).Item("SALESVOLUMEUNIT").ToString(), 0).ToString()
            End If
            BindDDL(ds.Tables(0).Rows(0).Item("TITLE8").ToString(), "units", ds.Tables(0).Rows(0).Item("CUSSALESUNIT").ToString())

            If CDbl(ds.Tables(0).Rows(0).Item("CUSSALESUNIT").ToString()) = 0 And CDbl(ds.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()) > 0.0 Then
                divVal = CDbl(ds.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString())
            ElseIf CDbl(ds.Tables(0).Rows(0).Item("CUSSALESUNIT").ToString()) = 1 And CDbl(ds.Tables(0).Rows(0).Item("SALESVOLUMEUNIT").ToString()) > 0.0 Then
                divVal = CDbl(ds.Tables(0).Rows(0).Item("SALESVOLUMEUNIT").ToString())
            End If

            Dim count As Integer
            count = 0
            'Inner
            For i = 1 To 29
                trInner = New TableRow
                For j = 1 To 6
                    tdInner = New TableCell
                    Select Case j
                        Case 1
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            If i = 1 Or i = 5 Or i = 8 Or i = 11 Or i = 14 Or i = 22 Then
                                lbl.Text = "<b>" + ds.Tables(0).Rows(0).Item("PLANTSPACE" + i.ToString() + "").ToString() + "</b>"
                            Else
                                lbl.Text = "<b style='margin-left:20px;'>" + ds.Tables(0).Rows(0).Item("PLANTSPACE" + i.ToString() + "").ToString() + "</b>"
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 2
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            If i = 1 Or i = 5 Or i = 8 Or i = 11 Or i = 14 Or i = 22 Then
                                lbl.Text = ""
                            Else
                                count = count + 1
                                If CDbl(ds.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()) > 0.0 Then
                                    lbl.Text = FormatNumber((CDbl(ds.Tables(0).Rows(0).Item("MATB" + count.ToString() + "").ToString()) * customerSaleValue) / divVal, 0)
                                Else
                                    lbl.Text = "na"
                                End If

                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 3
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            If i = 1 Or i = 2 Or i = 5 Or i = 6 Or i = 8 Or i = 9 Or i = 11 Then
                                lbl.Text = ""
                            Else
                                If i < 14 Then
                                    If CDbl(ds.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()) > 0.0 Then
                                        lbl.Text = FormatNumber((CDbl(ds.Tables(0).Rows(0).Item("MATRE" + count.ToString() + "").ToString()) * customerSaleValue) / divVal, 0)
                                    Else
                                        lbl.Text = "na"
                                    End If

                                Else
                                    lbl.Text = ""
                                End If

                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 4
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            If i = 1 Or i = 2 Or i = 5 Or i = 6 Or i = 8 Or i = 9 Or i = 11 Or i = 14 Or i = 22 Then
                                lbl.Text = ""
                            Else
                                If CDbl(ds.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()) > 0.0 Then
                                    lbl.Text = FormatNumber((CDbl(ds.Tables(0).Rows(0).Item("MATIN" + count.ToString() + "").ToString()) * customerSaleValue) / divVal, 0)
                                Else
                                    lbl.Text = "na"

                                End If

                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 5
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            If i < 14 Or i > 22 Then
                                If i = 1 Or i = 2 Or i = 5 Or i = 6 Or i = 8 Or i = 9 Or i = 11 Or i = 22 Then
                                    lbl.Text = ""
                                Else
                                    If CDbl(ds.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()) > 0.0 Then
                                        lbl.Text = FormatNumber((CDbl(ds.Tables(0).Rows(0).Item("MATCM" + count.ToString() + "").ToString()) * customerSaleValue) / divVal, 0)
                                    Else
                                        lbl.Text = "na"
                                    End If

                                End If
                            End If

                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 6
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            If i = 1 Or i = 2 Or i = 5 Or i = 6 Or i = 8 Or i = 9 Or i = 11 Then
                                lbl.Text = ""
                            Else
                                If i < 14 Then
                                    If CDbl(ds.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()) > 0.0 Then
                                        lbl.Text = FormatNumber((CDbl(ds.Tables(0).Rows(0).Item("MATLF" + count.ToString() + "").ToString()) * customerSaleValue) / divVal, 0)
                                    Else
                                        lbl.Text = "na"
                                    End If

                                Else
                                    lbl.Text = ""
                                End If

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

    Protected Sub BindDDL(ByVal FirstVal As String, ByVal SecVal As String, ByVal Unit As String)
        ddlCustUnit.Items.Clear()
        Dim list(2) As ListItem
        list(0) = New ListItem
        list(0).Text = FirstVal ' ds.Tables(0).Rows(0).Item("TITLE8").ToString()
        list(0).Value = 0
        ddlCustUnit.Items.Add(list(0))
        list(1) = New ListItem
        list(1).Text = SecVal ' ds.Tables(0).Rows(0).Item("SUNITLBL").ToString()
        list(1).Value = 1
        ddlCustUnit.Items.Add(list(1))
        ddlCustUnit.SelectedValue = CInt(Unit)
    End Sub

#Region "UI Settings"
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
#End Region

    Protected Sub Update_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        'Try
        '    If Not objRefresh.IsRefresh Then
        '        Calculate()
        '    End If
        '    GetPageDetails()
        'Catch ex As Exception
        '    ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        'End Try
        Try
            Dim ObjUpIns As New SMed2UpInsData.UpdateInsert()
            Dim obj As New CryptoHelper
            Dim cusSalesVolume As String
            Dim cusSalesUnit As String
            If Not objRefresh.IsRefresh Then
                cusSalesVolume = Request.Form("ctl00$SMed2ContentPlaceHolder$txtNewSaleValue")
                cusSalesUnit = ddlCustUnit.SelectedValue
                If Not IsNumeric(cusSalesVolume) Then
                    Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                End If
                ObjUpIns.CSaleVolumeUpdate(CaseId, cusSalesVolume, cusSalesUnit)
                Calculate()
                'Update Server Date
                ObjUpIns.ServerDateUpdate(CaseId, Session("SMed2UserName"))
            End If
            GetPageDetails()
        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub Calculate()
        Try
            Dim Med1Conn As String = System.Configuration.ConfigurationManager.AppSettings("MedEcon1ConnectionString")
            Dim SMed1Conn As String = System.Configuration.ConfigurationManager.AppSettings("MedSustain1ConnectionString")
            Dim Med2Conn As String = System.Configuration.ConfigurationManager.AppSettings("MedEcon2ConnectionString")
            Dim SMed2Conn As String = System.Configuration.ConfigurationManager.AppSettings("MedSustain2ConnectionString")
            Dim obj As New SMed2Calculation.SMed2Calculations(SMed2Conn, Med2Conn, SMed1Conn, Med1Conn)
            obj.SMed2Calculate(CaseId)
        Catch ex As Exception
            ErrorLable.Text = "Error:Calculate:" + ex.Message.ToString() + ""
        End Try
    End Sub
End Class
