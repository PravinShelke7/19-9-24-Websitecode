Imports System.Data
Imports System.Data.OleDb
Imports System
Imports E2GetData
Imports E2UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Econ2_Assumptions_OperationsIN
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
        Updatebtn.Attributes.Add("OnClick", "return checkNumericAll();")
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
        MainHeading.InnerHtml = "Econ2 - Operating Assumptions"
    End Sub

    Protected Sub GetContentPlaceHolder()
        ctlContentPlaceHolder = Page.Master.FindControl("Econ2ContentPlaceHolder")
    End Sub

#End Region

#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_PAGES_ECON2_ASSUMPTIONS_OPERATIONSIN")
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
            CaseId = Session("E2CaseId")
            UserRole = Session("E2UserRole")
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetSessionDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetPageDetails()
        Dim ds As New DataSet
        Dim objGetData As New E2GetData.Selectdata
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
        Dim dsEquip As New DataSet
        Try
            ds = objGetData.GetOperationInDetails(CaseId)
            dsEquip = objGetData.GetEquipment("-1", "", "")
            For i = 1 To 8
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "20px", "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 2
                        HeaderTdSetting(tdHeader, "140px", "Equipment Description", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 3
                        HeaderTdSetting(tdHeader, "100px", "Number of Assets", "1")
                        Header2TdSetting(tdHeader2, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 4
                        HeaderTdSetting(tdHeader, "90px", "Maximum Annual Run Hours", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 5
                        Title = "(" + ds.Tables(0).Rows(0).Item("Title8").ToString() + "/hr)"
                        HeaderTdSetting(tdHeader, "90px", "Instantaneous Rate", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)

                    Case 6
                        Title = "(alternate units)"
                        HeaderTdSetting(tdHeader, "70px", "Instantaneous Rate", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 7
                        HeaderTdSetting(tdHeader, "80px", "Downtime", "1")
                        Header2TdSetting(tdHeader2, "", "(%)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 8
                        HeaderTdSetting(tdHeader, "80px", "Waste", "1")
                        Header2TdSetting(tdHeader2, "", "(%)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)


                End Select


            Next
            tblComparision.Controls.Add(trHeader)
            tblComparision.Controls.Add(trHeader2)

            'Setting Value for Array
            Dim ARRpercent(30) As Double
            Dim ArrDeptProdWt(30) As Double
            Dim k As Integer
            For k = 1 To 30
                ARRpercent(k) = 0
            Next
            Dim x, y As Integer
            For x = 1 To 30
                For y = 1 To 10
                    If CInt(ds.Tables(0).Rows(0).Item("EQDEP" + x.ToString() + "")) > 0 Then
                        If CInt(ds.Tables(0).Rows(0).Item("EQDEP" + x.ToString() + "")) = CInt(ds.Tables(0).Rows(0).Item("pCONFIG" + y.ToString() + "")) Then
                            ARRpercent(x) = CDbl(ds.Tables(0).Rows(0).Item("PCT" + y.ToString() + ""))
                        End If
                    End If
                Next
            Next

            For x = 1 To 30
                For y = 1 To 10
                    If CInt(ds.Tables(0).Rows(0).Item("EQDEP" + x.ToString() + "")) > 0 Then
                        If CInt(ds.Tables(0).Rows(0).Item("EQDEP" + x.ToString() + "")) = CInt(ds.Tables(0).Rows(0).Item("pCONFIG" + y.ToString() + "")) Then
                            ArrDeptProdWt(x) = CDbl(ds.Tables(0).Rows(0).Item("PCT" + y.ToString() + "") / 100 * ds.Tables(0).Rows(0).Item("PRODWT"))
                        End If
                    End If
                Next
            Next

            'Inner()
            For i = 1 To 30


                trInner = New TableRow
                For j = 1 To 8
                    tdInner = New TableCell

                    Select Case j
                        Case 1
                            'Layer
                            InnerTdSetting(tdInner, "", "Center")
                            tdInner.Text = "<b>" + i.ToString() + "</b>"
                            trInner.Controls.Add(tdInner)
                        Case 2
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            If ds.Tables(0).Rows(0).Item("LEQUIPDES" + i.ToString() + "").ToString() <> "" Then
                                lbl.Text = ds.Tables(0).Rows(0).Item("LEQUIPDES" + i.ToString() + "").ToString()
                            Else
                                lbl.Text = ds.Tables(0).Rows(0).Item("EQDES" + i.ToString() + "").ToString()
                            End If
                            'lbl.Text = ds.Tables(0).Rows(0).Item("EQDES" + i.ToString() + "").ToString()
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
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.Width = 70
                            txt.ID = "MAXHOUR" + i.ToString()
                            txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("MAXHOUR" + i.ToString() + "").ToString(), 0)
                            txt.MaxLength = 6
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 5
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.Width = 70
                            txt.ID = "INSTANTANEOUSRATE" + i.ToString()
                            Dim cal As Double
                            cal = CDbl(ds.Tables(0).Rows(0).Item("OPINSTGRSRATE" + i.ToString() + "") * ds.Tables(0).Rows(0).Item("convwt"))
                            txt.Text = FormatNumber(cal.ToString(), 2)
                            txt.MaxLength = 12
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)

                        Case 6
                            InnerTdSetting(tdInner, "", "Center")

                            lbl = New Label
                            Dim cal As Double
                            If ds.Tables(0).Rows(0).Item("UNITS" + i.ToString() + "").ToString().ToLower() = "cpm" Then
                                If ArrDeptProdWt(i) <> 0 Then
                                    cal = CDbl(ds.Tables(0).Rows(0).Item("OPINSTGRSRATE" + i.ToString() + "") / ArrDeptProdWt(i) / 60 / ds.Tables(0).Rows(0).Item("EXITS" + i.ToString() + ""))
                                Else
                                    cal = 0
                                End If
                                lbl.Text = FormatNumber(cal, 2).ToString() + " " + ds.Tables(0).Rows(0).Item("UNITS" + i.ToString() + "").ToString()
                            ElseIf ds.Tables(0).Rows(0).Item("UNITS" + i.ToString() + "").ToString().ToLower() = "fpm" Then
                                If ARRpercent(i) = 0 Then
                                    cal = 0
                                    If CInt(ds.Tables(0).Rows(0).Item("UNITS")) = 0 Then
                                        lbl.Text = FormatNumber(cal, 2).ToString() + " " + ds.Tables(0).Rows(0).Item("UNITS" + i.ToString() + "").ToString()
                                    Else
                                        lbl.Text = FormatNumber(cal, 2).ToString() + " " + ds.Tables(0).Rows(0).Item("UNITST" + i.ToString() + "").ToString()
                                    End If
                                Else
                                    cal = CDbl(ds.Tables(0).Rows(0).Item("OPINSTGRSRATE" + i.ToString() + "")) / CDbl(ds.Tables(0).Rows(0).Item("wtPERarea") * ARRpercent(i) / 100) * 1000 / 60 / CDbl(ds.Tables(0).Rows(0).Item("WIDTH" + i.ToString() + "")) / 12 * CDbl(ds.Tables(0).Rows(0).Item("convthick2"))
                                    If CInt(ds.Tables(0).Rows(0).Item("UNITS")) = 0 Then
                                        lbl.Text = FormatNumber(cal, 2).ToString() + " " + ds.Tables(0).Rows(0).Item("UNITS" + i.ToString() + "").ToString()
                                    Else
                                        lbl.Text = FormatNumber(cal, 2).ToString() + " " + ds.Tables(0).Rows(0).Item("UNITST" + i.ToString() + "").ToString()
                                    End If
                                End If
                            ElseIf ds.Tables(0).Rows(0).Item("UNITS" + i.ToString() + "").ToString().ToLower() = "none" Then
                                lbl.Text = "na"
                            End If

                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 7
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.Width = 60
                            txt.ID = "DOWNTIME" + i.ToString()
                            txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("DT" + i.ToString() + "").ToString(), 1)
                            txt.MaxLength = 6
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 8
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.Width = 60
                            txt.ID = "OPWASTE" + i.ToString()
                            txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("OPWASTE" + i.ToString() + "").ToString(), 3)
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

    Protected Sub GetEquipmentInDetails(ByRef LinkMat As HyperLink, ByVal hid As HiddenField, ByVal EqId As Integer, ByVal dsEquip As DataSet)
        Dim Ds As New DataSet
        Dim ObjGetdata As New E2GetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty
        Dim dvEquip As New DataView
        Dim dtEquip As New DataTable
        Try
            'Ds = ObjGetdata.GetEquipment(EqId, "", "")
            dvEquip = dsEquip.Tables(0).DefaultView
            dvEquip.RowFilter = "EQUIPID = " + EqId.ToString()
            dtEquip = dvEquip.ToTable()

            LinkMat.Text = dtEquip.Rows(0).Item("equipDES").ToString()
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
        Dim OMAXRH(29) As String
        Dim OPINSTR(29) As String
        Dim DT(29) As String
        Dim OPWASTE(29) As String
        Dim ds As New DataSet
        Dim objGetData As New E2GetData.Selectdata
        ds = objGetData.GetOperationInDetails(CaseId)
        Dim obj As New CryptoHelper


        Dim i As New Integer
        Dim ObjUpIns As New E2UpInsData.UpdateInsert()
        Try
            If Not objRefresh.IsRefresh Then
                For i = 1 To 30
                    OMAXRH(i - 1) = Request.Form("ctl00$Econ2ContentPlaceHolder$MAXHOUR" + i.ToString() + "").Replace(",", "")
                    OPINSTR(i - 1) = Request.Form("ctl00$Econ2ContentPlaceHolder$INSTANTANEOUSRATE" + i.ToString() + "").Replace(",", "")
                    DT(i - 1) = Request.Form("ctl00$Econ2ContentPlaceHolder$DOWNTIME" + i.ToString() + "").Replace(",", "")
                    OPWASTE(i - 1) = Request.Form("ctl00$Econ2ContentPlaceHolder$OPWASTE" + i.ToString() + "").Replace(",", "")
                   
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
                Next
                ObjUpIns.OperationInUpdate(CaseId, OMAXRH, OPINSTR, DT, OPWASTE)
                Calculate()
                'Update Server Date
                ObjUpIns.ServerDateUpdate(CaseId, Session("E2UserName"))
            End If
            GetPageDetails()
        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub Calculate()
        Try
            Dim Econ1Conn As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            Dim Econ2Conn As String = System.Configuration.ConfigurationManager.AppSettings("Econ2ConnectionString")
            Dim obj As New Econ2Calculation.Econ2Calculations(Econ2Conn, Econ1Conn)
            obj.Econ2Calculate(CaseId)
        Catch ex As Exception
            ErrorLable.Text = "Error:Calculate:" + ex.Message.ToString() + ""
        End Try
    End Sub
 
End Class
