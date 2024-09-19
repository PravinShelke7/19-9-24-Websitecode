Imports System.Data
Imports System.Data.OleDb
Imports System
Imports S1GetData
Imports S1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Sustain1_Assumptions_ProductFormatIN
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
        Updatebtn.Attributes.Add("onclick", "return CheckProdFormat();")
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
        MainHeading.InnerHtml = "Sustain1 - Product Format Assumptions"
    End Sub

    Protected Sub GetContentPlaceHolder()
        ctlContentPlaceHolder = Page.Master.FindControl("Sustain1ContentPlaceHolder")
    End Sub

#End Region

#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_PAGES_SUSTAIN1_ASSUMPTIONS_PRODUCTFORMATIN")
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
            CaseId = Session("S1CaseId")
            UserRole = Session("S1UserRole")
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetSessionDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetPageDetails()
        Dim ds As New DataSet
        Dim objGetData As New S1GetData.Selectdata
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
                        HeaderTdSetting(tdHeader, "110px", "Product Format", "1")
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
                        If CDbl(ds.Tables(0).Rows(0).Item("UNITS").ToString()) = 0 Then
                            Title = "(lb)"
                        Else
                            Title = "(kg)"
                        End If
                        HeaderTdSetting(tdHeader, "", "Packaging Weight", "2")
                        Header2TdSetting(tdHeader1, "100px", "Suggested", "1")
                        Header2TdSetting(tdHeader2, "0", Title, "1")
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 8
                        If CDbl(ds.Tables(0).Rows(0).Item("UNITS").ToString()) = 0 Then
                            Title = "(lb)"
                        Else
                            Title = "(kg)"
                        End If
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                        Header2TdSetting(tdHeader1, "100px", "Preferred", "1")
                        Header2TdSetting(tdHeader2, "0", Title, "1")
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 9
                        Title = "" + ds.Tables(0).Rows(0).Item("RUNIT").ToString() + ""
                        HeaderTdSetting(tdHeader, "90px", " Roll Diameter", "1")
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
                        Link.Width = 110
                        Link.CssClass = "Link"
                        GetProductDetails(Link, hid, CInt(ds.Tables(0).Rows(0).Item("M1").ToString()), dsProd)
                        tdInner.Controls.Add(hid)
                        tdInner.Controls.Add(Link)
                        trInner.Controls.Add(tdInner)
                    Case 2, 3, 4, 5, 6
                        InnerTdSetting(tdInner, "", "Center")
                        txt = New TextBox
                        txt.CssClass = "SmallTextBox"
                        txt.Width = 70
                        txt.ID = "M" + i.ToString()
                        txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("M" + i.ToString() + "").ToString(), 3)
                        txt.MaxLength = 6
                        tdInner.Controls.Add(txt)
                        trInner.Controls.Add(tdInner)

                    Case 7
                        InnerTdSetting(tdInner, "", "Right")
                        lbl = New Label
                        lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("CONTWT").ToString(), 4)
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
                        lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("ROLLDIA").ToString(), 4)
                        tdInner.Controls.Add(lbl)
                        If CDbl(ds.Tables(0).Rows(0).Item("ROLLDIA").ToString()) > 0 Then
                            trInner.Controls.Add(tdInner)
                        End If

                End Select

            Next

            trInner.CssClass = "AlterNateColor2"
            tblComparision.Controls.Add(trInner)




        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetProductDetails(ByRef LinkMat As HyperLink, ByVal hid As HiddenField, ByVal FromatId As Integer, ByVal dsProd As DataSet)
        Dim Ds As New DataSet
        Dim ObjGetdata As New S1GetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty
        Dim dvProd As New DataView
        Dim dtProd As New DataTable
        Try
            'Ds = ObjGetdata.GetProductFormt(FromatId, "", "")
            dvProd = dsProd.Tables(0).DefaultView '
            dvProd.RowFilter = "FORMATID= " + FromatId.ToString() '
            dtProd = dvProd.ToTable() '


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
            Update()
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

    Protected Sub Update()

        Dim Input(6) As String
        Dim M1 As String
        Dim PRODWT As String
        Dim i As New Integer
        Dim ObjUpIns As New S1UpInsData.UpdateInsert()
        Dim ObjGetdata As New S1GetData.Selectdata()
        Dim DiscTotal As Decimal
        Dim UpdateFlag As Integer
        Dim ds As DataSet
        Dim obj As New CryptoHelper
        Dim PwtPref As String = String.Empty
        Try
            If Not objRefresh.IsRefresh Then
                PwtPref = Request.Form("ctl00$Sustain1ContentPlaceHolder$PWT").ToString()
                If Session("S1ServiceRole") <> "ReadOnly" Then
                    UpdateFlag = 0
                    ds = ObjGetdata.GetDiscretedMaterialTotal(CaseId)
                    DiscTotal = CDbl(ds.Tables(0).Rows(0).Item("DISCRETEWT").ToString())
                    M1 = Request.Form("ctl00$Sustain1ContentPlaceHolder$hidProductid")

                    'Check for Isnumeric


                    If CInt(M1) = 1 Or CInt(M1) = 17 Then
                        If DiscTotal > 0.0 Then
                            UpdateFlag = 1
                        End If
                    End If
                    If UpdateFlag = CDbl(0) Then
                        For i = 2 To 6
                            Input(i) = Request.Form("ctl00$Sustain1ContentPlaceHolder$M" + i.ToString() + "").Replace(",", "")
                            'Check For IsNumric
                            If Not IsNumeric(Input(i)) Then
                                Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                            End If
                        Next

                        ObjUpIns.ProductFormatUpdate(CaseId, M1, Input, PwtPref)
                    End If
                    Calculate()
                    'Update Server Date
                    ObjUpIns.ServerDateUpdate(CaseId, Session("S1UserName"))
                End If
            End If

            GetPageDetails()
        Catch ex As Exception
            ErrorLable.Text = "Error:Update:" + ex.Message.ToString() + ""
        End Try

    End Sub
    Protected Sub Calculate()
        Try
            Dim Sustain1Conn As String = System.Configuration.ConfigurationManager.AppSettings("Sustain1ConnectionString")
            Dim Econ1Conn As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            Dim obj As New SustainCalculation.SustainCalculations(Sustain1Conn, Econ1Conn)
            obj.SustainCalculate(CaseId)
        Catch ex As Exception
            ErrorLable.Text = "Error:Calculate:" + ex.Message.ToString() + ""
        End Try
    End Sub



End Class
