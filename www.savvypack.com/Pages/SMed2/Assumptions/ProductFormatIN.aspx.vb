Imports System.Data
Imports System.Data.OleDb
Imports System
Imports SMed2GetData
Imports SMed2UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_MedSustain2_Assumptions_ProductFormatIN
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
        MainHeading.InnerHtml = "SMed2 - Product Format Assumptions"
    End Sub

    Protected Sub GetContentPlaceHolder()
        ctlContentPlaceHolder = Page.Master.FindControl("SMed2ContentPlaceHolder")
    End Sub

#End Region

#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_PAGES_MEDSUSTAIN2_ASSUMPTIONS_PRODUCTFORMATIN")
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

        Try
            ds = objGetData.GetProductFromatIn(CaseId)
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


                    Case 2, 3, 4, 5, 6
                        HeaderTdSetting(tdHeader, "90px", "Input", "1")
                        Header2TdSetting(tdHeader1, "0", ds.Tables(0).Rows(0).Item("FORMAT_M" + (i - 1).ToString() + "").ToString(), "1")

                    Case 7
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                        HeaderTdSetting(tdHeader, "85px", "Packaging and Product Weight", "1")
                        Header2TdSetting(tdHeader1, "0", Title, "1")

                    Case 9
                        Title = "(" + ds.Tables(0).Rows(0).Item("RUNIT").ToString() + ")"
                        HeaderTdSetting(tdHeader, "90px", " Roll Diameter", "1")
                        Header2TdSetting(tdHeader1, "0", Title, "1")
                End Select


                If i = 9 Then
                    If CDbl(ds.Tables(0).Rows(0).Item("ROLLDIA").ToString()) > 0 Then
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader.Controls.Add(tdHeader)
                    End If
                Else
                    trHeader1.Controls.Add(tdHeader1)
                    trHeader.Controls.Add(tdHeader)
                End If



            Next
            tblComparision.Controls.Add(trHeader)
            tblComparision.Controls.Add(trHeader1)


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
                        GetProductDetails(Link, hid, CInt(ds.Tables(0).Rows(0).Item("M1").ToString()))
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
                        lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("PRODUCTWEIGHT").ToString(), 4)
                        tdInner.Controls.Add(lbl)
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

    Protected Sub GetProductDetails(ByRef LinkMat As HyperLink, ByVal hid As HiddenField, ByVal FromatId As Integer)
        Dim Ds As New DataSet
        Dim ObjGetdata As New SMed2GetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty
        Try

            Ds = ObjGetdata.GetProductFormt(FromatId, "", "")
            LinkMat.Text = Ds.Tables(0).Rows(0).Item("FormatDes").ToString()
            hid.Value = FromatId.ToString()
            Path = "../PopUp/GetProducts.aspx?Des=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + LinkMat.ClientID + "&Id=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + hid.ClientID + "&Btn=" + btnHidden.ClientID
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

        Dim Input(7) As String
        Dim M1 As String
        Dim PRODWT As String
        Dim i As New Integer
        Dim ObjUpIns As New SMed2UpInsData.UpdateInsert()
        Dim ObjGetdata As New SMed2GetData.Selectdata()
        Dim ds As DataSet
        Dim obj As New CryptoHelper
        Try
            If Not objRefresh.IsRefresh Then
                If Session("SMed2ServiceRole") <> "ReadOnly" Then
                    M1 = Request.Form("ctl00$SMed2ContentPlaceHolder$hidProductid")

                    For i = 2 To 6
                        Input(i) = Request.Form("ctl00$SMed2ContentPlaceHolder$M" + i.ToString() + "").Replace(",", "")
                        'Check For IsNumric
                        If Not IsNumeric(Input(i)) Then
                            Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        End If
                    Next
                    ObjUpIns.ProductFormatUpdate(CaseId, M1, Input)
                    Calculate()
                    'Update Server Date
                    ObjUpIns.ServerDateUpdate(CaseId, Session("SMed2UserName"))
                End If
            End If

            GetPageDetails()
        Catch ex As Exception
            ErrorLable.Text = "Error:Update:" + ex.Message.ToString() + ""
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
