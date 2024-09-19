Imports System.Data
Imports System.Data.OleDb
Imports System
Imports E1GetData
Imports E1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter

Partial Class Pages_Econ1_Assumptions_ProductFormatIn
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
        Notesbtn.OnClientClick = "return Notes('PRODFOR');"
        'AddHandler Updatebtn.Click, AddressOf Update_Click
    End Sub

    Protected Sub GetMainHeadingdiv()
        MainHeading = Page.Master.FindControl("divMainHeading")
        MainHeading.Attributes.Add("onmouseover", "Tip('Product Format Assumptions')")
        MainHeading.Attributes.Add("onmouseout", "UnTip()")
        MainHeading.InnerHtml = "Econ1 - Product Format Assumptions"
    End Sub

    Protected Sub GetContentPlaceHolder()
        ctlContentPlaceHolder = Page.Master.FindControl("Econ1ContentPlaceHolder")
    End Sub

#End Region

#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_PAGES_ECON1_ASSUMPTIONS_PRODUCTFORMATIN")
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
        Dim dsProd As New DataSet
        Try
            ds = objGetData.GetProductFromatIn(CaseId)
            dsProd = objGetData.GetProductFormt("-1", "", "")

            For i = 1 To 9
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "120px", "Product Format", "1")
                        Header2TdSetting(tdHeader1, "0", "", "1")
                        Header2TdSetting(tdHeader2, "0", "", "1")
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)

                    Case 2, 3, 4, 5, 6
                        HeaderTdSetting(tdHeader, "90px", "Input", "1")
                        Header2TdSetting(tdHeader1, "0", "", "1")
                        Header2TdSetting(tdHeader2, "0", ds.Tables(0).Rows(0).Item("FORMAT_M" + (i - 1).ToString() + "").ToString(), "1")
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 7
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                        HeaderTdSetting(tdHeader, "", " Packaging Weight", "2")
                        Header2TdSetting(tdHeader1, "100px", "Suggested", "1")
                        Header2TdSetting(tdHeader2, "0", Title, "1")
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)

                    Case 8
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                        Header2TdSetting(tdHeader1, "100px", "Preferred", "1")
                        Header2TdSetting(tdHeader2, "0", Title, "1")
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 9
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE9").ToString() + ")"
                        HeaderTdSetting(tdHeader, "100px", " Roll Diameter", "1")
                        Header2TdSetting(tdHeader1, "0", "", "1")
                        Header2TdSetting(tdHeader2, "0", Title, "1")
                        If CDbl(ds.Tables(0).Rows(0).Item("ROLLDIA").ToString()) > 0 Then
                            trHeader1.Controls.Add(tdHeader1)
                            trHeader.Controls.Add(tdHeader)
                            trHeader2.Controls.Add(tdHeader2)
                        End If
                        
                End Select


                'If i = 9 Then
                '    If CDbl(ds.Tables(0).Rows(0).Item("ROLLDIA").ToString()) > 0 Then
                '        trHeader1.Controls.Add(tdHeader1)
                '        trHeader.Controls.Add(tdHeader)
                '        trHeader2.Controls.Add(tdHeader2)
                '    End If
                'Else
                '    trHeader1.Controls.Add(tdHeader1)
                '    trHeader.Controls.Add(tdHeader)
                '    trHeader2.Controls.Add(tdHeader2)
                'End If



            Next
            tblComparision.Controls.Add(trHeader)
            tblComparision.Controls.Add(trHeader1)
            tblComparision.Controls.Add(trHeader2)


            trInner = New TableRow
            For i = 1 To 9
                'Inner
                tdInner = New TableCell
                Select Case i
                    Case 1
                        InnerTdSetting(tdInner, "", "Left")
                        Link = New HyperLink
                        hid = New HiddenField
                        Link.ID = "hypProductDes"
                        hid.ID = "hidProductid"
                        Link.Width = 120
                        Link.CssClass = "Link"
                        GetProductDetails(Link, hid, CInt(ds.Tables(0).Rows(0).Item("M1").ToString()), dsProd)
                        tdInner.Controls.Add(hid)
                        tdInner.Controls.Add(Link)
                        trInner.Controls.Add(tdInner)
                    Case 2, 3, 4, 5, 6
                        InnerTdSetting(tdInner, "", "Center")
                        txt = New TextBox
                        txt.CssClass = "SmallTextBox"
                        txt.Width = 80
                        txt.ID = "M" + i.ToString()
                        txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("M" + i.ToString() + "").ToString(), 3)
                        txt.MaxLength = 6
                        tdInner.Controls.Add(txt)
                        trInner.Controls.Add(tdInner)
                    Case 7
                        InnerTdSetting(tdInner, "", "Right")
                        lbl = New Label
                        lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("PRODWT").ToString(), 4)
                        tdInner.Controls.Add(lbl)
                        trInner.Controls.Add(tdInner)
                    Case 8
                        InnerTdSetting(tdInner, "", "Center")
                        txt = New TextBox
                        txt.CssClass = "SmallTextBox"
                        txt.Width = 80
                        txt.ID = "PWT"
                        txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("PRODWTPREF").ToString(), 4)
                        txt.MaxLength = 10
                        tdInner.Controls.Add(txt)
                        trInner.Controls.Add(tdInner)
                    Case 9
                        InnerTdSetting(tdInner, "", "Right")
                        lbl = New Label
                        lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("ROLLDIA").ToString(), 3)
                        tdInner.Controls.Add(lbl)
                        If CDbl(ds.Tables(0).Rows(0).Item("ROLLDIA").ToString()) > 0 Then
                            trInner.Controls.Add(tdInner)
                        End If

                End Select

            Next

            trInner.CssClass = "AlterNateColor2"
            tblComparision.Controls.Add(trInner)


            If ds.Tables(0).Rows(0).Item("M1").ToString() = 1 Then
                GetPageDetails2()
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
        Dim trInner1 As New TableRow
        Dim tdHeader As TableCell
        Dim tdHeader1 As TableCell
        Dim tdHeader2 As TableCell
        Dim lbl As New Label
        Dim hid As New HiddenField
        Dim Link As New HyperLink
        Dim txt As New TextBox
        Dim tdInner As TableCell
        Dim tdInner1 As TableCell
        Dim dsProd As New DataSet
        Try
            ds = objGetData.GetProductFromatIn(CaseId)
            dsProd = objGetData.GetProductFormt("-1", "", "")


            For i = 1 To 3
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "120px", "Product Format", "1")
                        Header2TdSetting(tdHeader1, "0", "", "1")
                        Header2TdSetting(tdHeader2, "0", "", "1")
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 2
                        HeaderTdSetting(tdHeader, "90px", "Input", "1")
                        Header2TdSetting(tdHeader1, "0", "", "1")
                        Header2TdSetting(tdHeader2, "0", ds.Tables(0).Rows(0).Item("FORMAT_M" + (i - 1).ToString() + "").ToString(), "1")
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        
                    Case 3
                        HeaderTdSetting(tdHeader, "90px", "Input", "1")
                        Header2TdSetting(tdHeader1, "0", "", "1")
                        Header2TdSetting(tdHeader2, "0", ds.Tables(0).Rows(0).Item("FORMAT_M6").ToString(), "1")
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)

                End Select
            Next
            tblComparision2.Controls.Add(trHeader)
            tblComparision2.Controls.Add(trHeader1)
            tblComparision2.Controls.Add(trHeader2)

            trInner = New TableRow
            For i = 0 To 2
                'Inner
                tdInner = New TableCell
                Select Case i
                    Case 0
                        InnerTdSetting(tdInner, "", "Left")
                        lbl = New Label
                        hid = New HiddenField
                        lbl.Text = "Impression Size ".ToString()
                        tdInner.Controls.Add(hid)
                        tdInner.Controls.Add(lbl)
                        trInner.Controls.Add(tdInner)
                    Case 1, 2
                        InnerTdSetting(tdInner, "", "Center")
                        txt = New TextBox
                        txt.CssClass = "SmallTextBox"
                        txt.Width = 80
                        txt.ID = "I" + i.ToString()
                        txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("I" + i.ToString() + "").ToString(), 3)
                        txt.MaxLength = 6
                        tdInner.Controls.Add(txt)
                        trInner.Controls.Add(tdInner)

                End Select

            Next


            trInner.CssClass = "AlterNateColor2"
            tblComparision2.Controls.Add(trInner)

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetProductDetails(ByRef LinkMat As HyperLink, ByVal hid As HiddenField, ByVal FromatId As Integer, ByVal dsProd As DataSet)
        Dim Ds As New DataSet
        Dim ObjGetdata As New E1GetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty
        Dim dvProd As New DataView
        Dim dtProd As New DataTable
        Try

            'Ds = ObjGetdata.GetProductFormt(FromatId, "", "")

            dvProd = dsProd.Tables(0).DefaultView
            dvProd.RowFilter = "FORMATID = " + FromatId.ToString()
            dtProd = dvProd.ToTable()

            LinkMat.Text = dtProd.Rows(0).Item("FormatDes").ToString()
            hid.Value = FromatId.ToString()
            Path = "../PopUp/GetProdcuts.aspx?Des=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + LinkMat.ClientID + "&Id=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + hid.ClientID + "&Btn=" + btnHidden.ClientID
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
        Try
            Update2()
        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnHidden_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHidden.Click
        Try
            If Updatebtn.Visible Then
                Update()
            Else
                GetPageDetails()
            End If


        Catch ex As Exception
            ErrorLable.Text = "Error:btnHidden_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    'Protected Sub Update()

    '    Dim Input(6) As String
    '    Dim M1 As String
    '    Dim i As New Integer
    '    Dim ObjUpIns As New E1UpInsData.UpdateInsert()
    '    Dim ObjGetdata As New E1GetData.Selectdata()
    '    Dim DiscTotal As Decimal
    '    Dim UpdateFlag As Integer
    '    Dim ds As DataSet
    '    Dim obj As New CryptoHelper
    '    Try
    '        If Not objRefresh.IsRefresh Then
    '            UpdateFlag = 0
    '            ds = ObjGetdata.GetDiscretedMaterialTotal(CaseId)
    '            DiscTotal = CDbl(ds.Tables(0).Rows(0).Item("DISCRETEWT").ToString())
    '            M1 = Request.Form("ctl00$Econ1ContentPlaceHolder$hidProductid")
    '            If CInt(M1) = 1 Or CInt(M1) = 17 Then
    '                If DiscTotal > 0.0 Then
    '                    UpdateFlag = 1
    '                End If
    '            End If
    '            If UpdateFlag = CDbl(0) Then
    '                For i = 2 To 6
    '                    Input(i) = Request.Form("ctl00$Econ1ContentPlaceHolder$M" + i.ToString() + "").Replace(",", "")
    '                    'Check For IsNumric
    '                    If Not IsNumeric(Input(i)) Then
    '                        Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
    '                    End If
    '                Next

    '                ObjUpIns.ProductFormatUpdate(CaseId, M1, Input)
    '            End If

    '            Calculate()
    '        End If

    '        GetPageDetails()
    '    Catch ex As Exception
    '        ErrorLable.Text = "Error:Update:" + ex.Message.ToString() + ""
    '    End Try

    'End Sub

    Protected Sub Calculate()
        Try
            Dim Econ1Conn As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            Dim obj As New EconCalculation.EconCalculation(Econ1Conn)
            obj.EconCalculate(CaseId)
        Catch ex As Exception
            ErrorLable.Text = "Error:Calculate:" + ex.Message.ToString() + ""
        End Try
    End Sub
    Protected Sub Update()

        Dim Input(6) As String
        Dim M1 As String
        Dim i As New Integer
        Dim ObjUpIns As New E1UpInsData.UpdateInsert()
        Dim ObjGetdata As New E1GetData.Selectdata()
        Dim DiscTotal As Decimal
        Dim UpdateFlag As Integer
        Dim PwtPref As String = String.Empty
        Dim ds As DataSet
        Dim obj As New CryptoHelper
        Try
            If Not objRefresh.IsRefresh Then
                PwtPref = Request.Form("ctl00$Econ1ContentPlaceHolder$PWT").ToString()
                'Check for readonly User
                If Session("E1ServiceRole") <> "ReadOnly" Then
                    UpdateFlag = 0
                    ds = ObjGetdata.GetDiscretedMaterialTotal(CaseId)
                    DiscTotal = CDbl(ds.Tables(0).Rows(0).Item("DISCRETEWT").ToString())
                    M1 = Request.Form("ctl00$Econ1ContentPlaceHolder$hidProductid")
                    If CInt(M1) = 1 Or CInt(M1) = 17 Then
                        If DiscTotal > 0.0 Then
                            UpdateFlag = 1
                        End If
                    End If
                    If UpdateFlag = CDbl(0) Then
                        For i = 2 To 6
                            Input(i) = Request.Form("ctl00$Econ1ContentPlaceHolder$M" + i.ToString() + "").Replace(",", "")
                            'Check For IsNumric
                            If Not IsNumeric(Input(i)) Then
                                Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                            End If
                        Next

                        ObjUpIns.ProductFormatUpdate(CaseId, M1, Input, PwtPref)
                    End If
                    Calculate()
                    'Update Server Date
                    ObjUpIns.ServerDateUpdate(CaseId, Session("E1UserName"))
                End If

            End If

            GetPageDetails()
        Catch ex As Exception
            ErrorLable.Text = "Error:Update:" + ex.Message.ToString() + ""
        End Try

    End Sub
    Protected Sub Update2()

        Dim Input(6) As String
        Dim Input2(2) As String
        Dim M1 As String
        Dim i As New Integer
        Dim ObjUpIns As New E1UpInsData.UpdateInsert()
        Dim ObjGetdata As New E1GetData.Selectdata()
        Dim DiscTotal As Decimal
        Dim UpdateFlag As Integer
        Dim PwtPref As String = String.Empty
        Dim ds As DataSet
        Dim obj As New CryptoHelper
        Try
            If Not objRefresh.IsRefresh Then
                PwtPref = Request.Form("ctl00$Econ1ContentPlaceHolder$PWT").ToString()
                'Check for readonly User
                If Session("E1ServiceRole") <> "ReadOnly" Then
                    UpdateFlag = 0
                    ds = ObjGetdata.GetDiscretedMaterialTotal(CaseId)
                    DiscTotal = CDbl(ds.Tables(0).Rows(0).Item("DISCRETEWT").ToString())
                    M1 = Request.Form("ctl00$Econ1ContentPlaceHolder$hidProductid")
                    If CInt(M1) = 1 Or CInt(M1) = 17 Then
                        If DiscTotal > 0.0 Then
                            UpdateFlag = 1
                        End If
                    End If
                    If UpdateFlag = CDbl(0) Then
                        For i = 2 To 6
                            Input(i) = Request.Form("ctl00$Econ1ContentPlaceHolder$M" + i.ToString() + "").Replace(",", "")
                            'Check For IsNumric
                            If Not IsNumeric(Input(i)) Then
                                Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                            End If
                        Next


                        If M1 = 1 Then
                            If UpdateFlag = CDbl(0) Then
                                For j = 1 To 2
                                    Input2(j) = Request.Form("ctl00$Econ1ContentPlaceHolder$I" + j.ToString() + "").Replace(",", "")
                                    'Check For IsNumric
                                    If Not IsNumeric(Input2(j)) Then
                                        Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                                    End If
                                Next

                                ObjUpIns.ProductFormatUpdate(CaseId, M1, Input, PwtPref, Input2)

                            End If
                        Else
                            ObjUpIns.ProductFormatUpdate(CaseId, M1, Input, PwtPref)

                        End If

                    End If
                    Calculate()
                    'Update Server Date
                    ObjUpIns.ServerDateUpdate(CaseId, Session("E1UserName"))
                End If

            End If

            GetPageDetails()
        Catch ex As Exception
            ErrorLable.Text = "Error:Update:" + ex.Message.ToString() + ""
        End Try

    End Sub


#Region "Bulk Model Management"

    Protected Sub btnUpdateBulk_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpdateBulk.Click
        Dim Input(6) As String
        Dim M1 As String
        Dim i As New Integer
        Dim ObjUpIns As New E1UpInsData.UpdateInsert()
        Dim ObjGetdata As New E1GetData.Selectdata()
        Dim DiscTotal As Decimal
        Dim UpdateFlag As Integer
        Dim PwtPref As String = String.Empty
        Dim ds As DataSet
        Dim obj As New CryptoHelper
        Try
            PwtPref = Request.Form("ctl00$Econ1ContentPlaceHolder$PWT").ToString()
            'Check for readonly User
            If Session("E1ServiceRole") <> "ReadOnly" Then
                UpdateFlag = 0
                ds = ObjGetdata.GetDiscretedMaterialTotal(CaseId)
                DiscTotal = CDbl(ds.Tables(0).Rows(0).Item("DISCRETEWT").ToString())
                M1 = Request.Form("ctl00$Econ1ContentPlaceHolder$hidProductid")
                If CInt(M1) = 1 Or CInt(M1) = 17 Then
                    If DiscTotal > 0.0 Then
                        UpdateFlag = 1
                    End If
                End If
                If UpdateFlag = CDbl(0) Then
                    For i = 2 To 6
                        Input(i) = Request.Form("ctl00$Econ1ContentPlaceHolder$M" + i.ToString() + "").Replace(",", "")
                        'Check For IsNumric
                        If Not IsNumeric(Input(i)) Then
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
                                ObjUpIns.ProductFormatUpdate(BCaseId, M1, Input, PwtPref)
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
                End If
            End If
            loading.Style.Add("display", "none")
            lnkSelBulkModel.Visible = True
            GetPageDetails()
        Catch ex As Exception
            ErrorLable.Text = "Error:btnUpdateBulk_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnUpdateBulk1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpdateBulk1.Click
        Dim Input(6) As String
        Dim M1 As String
        Dim i As New Integer
        Dim ObjUpIns As New E1UpInsData.UpdateInsert()
        Dim ObjGetdata As New E1GetData.Selectdata()
        Dim DiscTotal As Decimal
        Dim UpdateFlag As Integer
        Dim PwtPref As String = String.Empty
        Dim ds As DataSet
        Dim obj As New CryptoHelper
        Try
            PwtPref = Request.Form("ctl00$Econ1ContentPlaceHolder$PWT").ToString()
            'Check for readonly User
            If Session("E1ServiceRole") <> "ReadOnly" Then
                UpdateFlag = 0
                ds = ObjGetdata.GetDiscretedMaterialTotal(CaseId)
                DiscTotal = CDbl(ds.Tables(0).Rows(0).Item("DISCRETEWT").ToString())
                M1 = Request.Form("ctl00$Econ1ContentPlaceHolder$hidProductid")
                If CInt(M1) = 1 Or CInt(M1) = 17 Then
                    If DiscTotal > 0.0 Then
                        UpdateFlag = 1
                    End If
                End If
                If UpdateFlag = CDbl(0) Then
                    For i = 2 To 6
                        Input(i) = Request.Form("ctl00$Econ1ContentPlaceHolder$M" + i.ToString() + "").Replace(",", "")
                        'Check For IsNumric
                        If Not IsNumeric(Input(i)) Then
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
                                ObjUpIns.ProductFormatUpdate(BCaseId, M1, Input, PwtPref)
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
                End If
            End If
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
