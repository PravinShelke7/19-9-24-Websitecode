Imports System.Data
Imports System.Data.OleDb
Imports System
Imports Schem1GetData
Imports Schem1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Schem1_Assumptions_EquipmentIN
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
        Updatebtn.Attributes.Add("onclick", "return CheckForEquipmentIn('" + ctlContentPlaceHolder.ClientID + "','hidAssetId','hidEqDepid','hypEqDep','ASSETNUM')")
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
        Notesbtn.OnClientClick = "return Notes('PROCEQUIP');"
        'AddHandler Updatebtn.Click, AddressOf Update_Click
    End Sub

    Protected Sub GetMainHeadingdiv()
        MainHeading = Page.Master.FindControl("divMainHeading")
        MainHeading.Attributes.Add("onmouseover", "Tip('Equipment Assumptions')")
        MainHeading.Attributes.Add("onmouseout", "UnTip()")
        MainHeading.InnerHtml = "Schem1 - Equipment Assumptions"
    End Sub

    Protected Sub GetContentPlaceHolder()
        ctlContentPlaceHolder = Page.Master.FindControl("Schem1ContentPlaceHolder")
    End Sub

#End Region

#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_PAGES_SCHEM1_ASSUMPTIONS_EQUIPMENTIN")
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
                hidBarrier.Value = "0"
                GetPageDetails()
            End If
        Catch ex As Exception
            ErrorLable.Text = "Error:Page_Load:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetSessionDetails()
        Try
            UserId = Session("UserId")
            CaseId = Session("Schem1CaseId")
            UserRole = Session("Schem1UserRole")
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetSessionDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetPageDetails()
        Dim ds As New DataSet
        Dim objGetData As New Schem1GetData.Selectdata
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
        Dim dsDept As New DataSet
        '2 SEPT
        Dim ImgBut As New ImageButton
        Dim ImgDis As New ImageButton
        Dim dsEEquip As New DataSet
        Dim dvEEquip As New DataView
        Dim dtEEquip As New DataTable
        Dim lnkCount As New Integer
        'end
        Try
            ds = objGetData.GetEquipmentInDetails(CaseId)
            dsEquip = objGetData.GetEquipment("-1", "", "")
            dsDept = objGetData.GetDept("-1", "")
            '2Sept changes
            dsEEquip = objGetData.GetEditEquip(CaseId)
            dvEEquip = dsEEquip.Tables(0).DefaultView
            Session("dsEEquip") = dsEEquip
            'end
            For i = 1 To 13
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "50px", "Asset", "1")
                        Header2TdSetting(tdHeader1, "0", "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 2
                        HeaderTdSetting(tdHeader, "150px", "Asset Description", "1")
                        Header2TdSetting(tdHeader1, "0", "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 3
                        HeaderTdSetting(tdHeader, "100px", "Number of Assets", "1")
                        Header2TdSetting(tdHeader1, "0", "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 4
                        HeaderTdSetting(tdHeader, "100px", "Area Type", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 5
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE7").ToString() + ")"
                        HeaderTdSetting(tdHeader, "", "Plant Area", "2")
                        Header2TdSetting(tdHeader2, "", Title, "2")
                        Header2TdSetting(tdHeader1, "60px", "Suggested", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 6
                        Header2TdSetting(tdHeader1, "60px", "Preferred", "1")
                    Case 7
                        HeaderTdSetting(tdHeader, "", "Electricity Consumption", "2")
                        Header2TdSetting(tdHeader2, "", "(kw)", "2")
                        Header2TdSetting(tdHeader1, "60px", "Suggested", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 8
                        Header2TdSetting(tdHeader1, "60px", "Preferred", "1")
                    Case 9
                        HeaderTdSetting(tdHeader, "120px", "Process Natural Gas Consumption", "2")
                        Header2TdSetting(tdHeader2, "", "(cubic ft/hr)", "2")
                        Header2TdSetting(tdHeader1, "", "Suggested", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 10
                        Header2TdSetting(tdHeader1, "", "Preferred", "1")
                    Case 11
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE10").ToString() + "/hr)"
                        HeaderTdSetting(tdHeader, "120px", "Water Consumption", "2")
                        Header2TdSetting(tdHeader2, "", Title, "2")
                        Header2TdSetting(tdHeader1, "", "Suggested", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 12
                        Header2TdSetting(tdHeader1, "", "Preferred", "1")

                    Case 13
                        HeaderTdSetting(tdHeader, "100px", "Mfg. Dept.", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        HeaderTdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)

                End Select


                trHeader1.Controls.Add(tdHeader1)

            Next
            tblComparision.Controls.Add(trHeader)
            tblComparision.Controls.Add(trHeader2)
            tblComparision.Controls.Add(trHeader1)


            'Inner
            For i = 1 To 30
                trInner = New TableRow
                For j = 1 To 13
                    tdInner = New TableCell

                    Select Case j
                        Case 1
                            'Layer
                            InnerTdSetting(tdInner, "50px", "Center")
                            tdInner.Text = "<b>" + i.ToString() + "</b>"
                            trInner.Controls.Add(tdInner)
                        Case 2
                            'InnerTdSetting(tdInner, "", "Left")
                            'Link = New HyperLink
                            'hid = New HiddenField
                            'Link.ID = "hypAssetDes" + i.ToString()
                            'hid.ID = "hidAssetId" + i.ToString()
                            'Link.Width = 170
                            'Link.CssClass = "Link"
                            'GetEquipmentInDetails(Link, hid, CInt(ds.Tables(0).Rows(0).Item("M" + i.ToString() + "").ToString()), dsEquip)
                            'tdInner.Controls.Add(hid)
                            'tdInner.Controls.Add(Link)
                            'trInner.Controls.Add(tdInner)
                            '2sept
                            InnerTdSetting(tdInner, "", "Left")
                            Link = New HyperLink
                            hid = New HiddenField
                            Link.ID = "hypAssetDes" + i.ToString()
                            hid.ID = "hidAssetId" + i.ToString()
                            Link.Width = 120
                            Link.CssClass = "Link"
                            ImgBut = New ImageButton
                            ImgBut.ID = "imgBut" + i.ToString()
                            ImgBut.Width = 6
                            ImgBut.Height = 6
                            ImgDis = New ImageButton
                            ImgDis.ID = "imgDis" + i.ToString()
                            ImgDis.Width = 6
                            ImgDis.Height = 6
                            If (ds.Tables(0).Rows(0).Item("M" + i.ToString() + "").ToString()) = "0" Then

                                ImgBut.ImageUrl = "~/Images/595958.png"
                                ImgBut.ToolTip = "User Preferred Asset Description"
                                If hidBarrier.Value = "0" Then
                                    ImgBut.Attributes.Add("style", "float:right; margin-bottom:20px; Display:none;")
                                Else
                                    ImgBut.Attributes.Add("style", "float:right; margin-bottom:5px; Display:none;")
                                End If


                                ImgDis.ImageUrl = "~/Images/F1F1F2.png"
                                ImgDis.ToolTip = "System Suggested Asset Description"
                                If hidBarrier.Value = "0" Then
                                    ImgDis.Attributes.Add("style", "float:right; margin-bottom:20px; Display:none;")
                                Else
                                    ImgDis.Attributes.Add("style", "float:right; margin-bottom:5px; Display:none;")
                                End If

                                GetEquipmentInDetails(Link, hid, CInt(ds.Tables(0).Rows(0).Item("M" + i.ToString() + "").ToString()), dsEquip)

                                ImgBut.Attributes.Add("onclick", "ShowEditPopWindow('../PopUp/UpdateEquip.aspx?Des=" + ctlContentPlaceHolder.ClientID.ToString() + "_hypAssetDes" + i.ToString() + "','" + ctlContentPlaceHolder.ClientID.ToString() + "_hidAssetId" + i.ToString() + "'); return false;")
                                ImgDis.Attributes.Add("onclick", "ShowEditPopWindow('../PopUp/UpdateEquip.aspx?Des=" + ctlContentPlaceHolder.ClientID.ToString() + "_hypAssetDes" + i.ToString() + "','" + ctlContentPlaceHolder.ClientID.ToString() + "_hidAssetId" + i.ToString() + "'); return false;")
                            Else
                                dvEEquip.RowFilter = "EQUIPID=" + ds.Tables(0).Rows(0).Item("M" + i.ToString() + "").ToString()
                                dtEEquip = dvEEquip.ToTable()
                                If dtEEquip.Rows.Count > 0 Then
                                    ImgBut.ImageUrl = "~/Images/595958.png"
                                    ImgBut.ToolTip = "User Preferred Asset Description"

                                    ImgDis.ImageUrl = "~/Images/F1F1F2.png"
                                    ImgDis.ToolTip = "System Suggested Asset Description"

                                    If hidBarrier.Value = "0" Then
                                        ImgBut.Attributes.Add("style", "float:right; margin-bottom:20px; display:inline;")
                                        ImgDis.Attributes.Add("style", "float:right; margin-bottom:20px; display:none;")
                                    Else
                                        ImgBut.Attributes.Add("style", "float:right; margin-bottom:5px; display:inline;")
                                        ImgDis.Attributes.Add("style", "float:right; margin-bottom:5px; display:none;")
                                    End If

                                Else
                                    ImgBut.ImageUrl = "~/Images/595958.png"
                                    ImgBut.ToolTip = "User Preferred Asset Description"

                                    ImgDis.ImageUrl = "~/Images/F1F1F2.png"
                                    ImgDis.ToolTip = "System Suggested Asset Description"

                                    If hidBarrier.Value = "0" Then
                                        ImgBut.Attributes.Add("style", "float:right; margin-bottom:20px; display:none;")
                                        ImgDis.Attributes.Add("style", "float:right; margin-bottom:20px; display:inline;")
                                    Else
                                        ImgBut.Attributes.Add("style", "float:right; margin-bottom:5px; display:none;")
                                        ImgDis.Attributes.Add("style", "float:right; margin-bottom:5px; display:inline;")
                                    End If
                                End If
                                GetEquipmentInDetails(Link, hid, CInt(ds.Tables(0).Rows(0).Item("M" + i.ToString() + "").ToString()), dsEquip)

                                ImgBut.Attributes.Add("onclick", "ShowEditPopWindow('../PopUp/UpdateEquip.aspx?Des=" + ctlContentPlaceHolder.ClientID.ToString() + "_hypAssetDes" + i.ToString() + "','" + ctlContentPlaceHolder.ClientID.ToString() + "_hidAssetId" + i.ToString() + "'); return false;")
                                ImgDis.Attributes.Add("onclick", "ShowEditPopWindow('../PopUp/UpdateEquip.aspx?Des=" + ctlContentPlaceHolder.ClientID.ToString() + "_hypAssetDes" + i.ToString() + "','" + ctlContentPlaceHolder.ClientID.ToString() + "_hidAssetId" + i.ToString() + "'); return false;")

                                lnkCount = Link.Text.Length
                                If Link.Text.Length > 23 Then
                                    Link.Attributes.Add("style", "margin-bottom:5px; margin-top:3px;")
                                    If Link.Text.Length > 38 Then
                                        Link.Text = Link.Text.Substring(0, 20) + " " + Link.Text.Substring(20, 15) + "..."
                                    Else
                                        Link.Text = Link.Text.Substring(0, 20) + " " + Link.Text.Substring(20, Link.Text.Length - 20)
                                    End If
                                Else
                                    Link.Attributes.Add("style", "margin-bottom:5px; margin-top:5px;")
                                End If
                            End If

                            tdInner.Controls.Add(ImgBut)
                            tdInner.Controls.Add(ImgDis)
                            tdInner.Width = 120
                            tdInner.Height = 20
                            tdInner.Controls.Add(hid)
                            tdInner.Controls.Add(Link)
                            trInner.Controls.Add(tdInner)
                            'end changes
                        Case 3
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.Width = 70
                            txt.ID = "ASSETNUM" + i.ToString()
                            If (ds.Tables(0).Rows(0).Item("NUM" + i.ToString() + "").ToString() <> "") Then
                                txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("NUM" + i.ToString() + "").ToString(), 0)
                            End If
                            txt.MaxLength = 12
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 4
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            lbl.Width = 100
                            lbl.Text = ds.Tables(0).Rows(0).Item("AT" + i.ToString() + "").ToString()
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 5
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("PAS" + i.ToString() + "").ToString(), 0)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 6
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.ID = "PLANTAREAP" + i.ToString()
                            txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("PAP" + i.ToString() + "").ToString(), 0)
                            txt.MaxLength = 12
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 7
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("ECS" + i.ToString() + "").ToString(), 0)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 8
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.ID = "ERGYP" + i.ToString()
                            txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("ECP" + i.ToString() + "").ToString(), 0)
                            txt.MaxLength = 12
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 9
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Width = 60
                            lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("NGS" + i.ToString() + "").ToString(), 0)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 10
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.ID = "NTGP" + i.ToString()
                            txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("NGP" + i.ToString() + "").ToString(), 0)
                            txt.MaxLength = 12
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 11
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Width = 60
                            lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("WATERS" + i.ToString() + "").ToString(), 0)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 12
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.ID = "WATERP" + i.ToString()
                            txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("WATERP" + i.ToString() + "").ToString(), 0)
                            txt.MaxLength = 12
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 13
                            InnerTdSetting(tdInner, "", "Left")
                            Link = New HyperLink
                            hid = New HiddenField
                            Link.ID = "hypEqDep" + i.ToString()
                            hid.ID = "hidEqDepid" + i.ToString()
                            Link.Width = 100
                            Link.CssClass = "Link"
                            GetDeptDetails(Link, hid, CInt(ds.Tables(0).Rows(0).Item("DEP" + i.ToString() + "").ToString()), dsDept)
                            tdInner.Controls.Add(hid)
                            tdInner.Controls.Add(Link)
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
        Dim ObjGetdata As New Schem1GetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty
        Dim dvEquip As New DataView
        Dim dtEquip As New DataTable

        '29AUG
        Dim dsEEquip As New DataSet
        Dim str As String = ""
        Dim dv As New DataView
        Dim dt As New DataTable
        Dim dvMat As New DataView
        Dim dtMat As New DataTable


        Try
            dsEEquip = DirectCast(Session("dsEEquip"), DataSet)
            dv = dsEEquip.Tables(0).DefaultView
            If EqId <> 0 Then

                dv.RowFilter = "EQUIPID=" + EqId.ToString()
                dt = dv.ToTable()
                If dt.Rows.Count > 0 Then
                    LinkMat.Text = dt.Rows(0).Item("equipDES").ToString().Replace("&#", "'")
                    LinkMat.ToolTip = dt.Rows(0).Item("equipDES").ToString().Replace("&#", "'")
                    hid.Value = EqId.ToString()
                Else
                    dvEquip = dsEquip.Tables(0).DefaultView
                    dvEquip.RowFilter = "EQUIPID = " + EqId.ToString()
                    dtEquip = dvEquip.ToTable()
                    LinkMat.Text = dtEquip.Rows(0).Item("equipDES").ToString()
                    LinkMat.ToolTip = dtEquip.Rows(0).Item("equipDES").ToString().Replace("&#", "'")
                    hid.Value = EqId.ToString()
                End If
            Else
                dvEquip = dsEquip.Tables(0).DefaultView
                dvEquip.RowFilter = "EQUIPID = " + EqId.ToString()
                dtEquip = dvEquip.ToTable()
                LinkMat.Text = dtEquip.Rows(0).Item("equipDES").ToString()
                LinkMat.ToolTip = dtEquip.Rows(0).Item("equipDES").ToString().Replace("&#", "'")
                hid.Value = EqId.ToString()
            End If
            Path = "../PopUp/GetEquipmentPopUp.aspx?Des=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + LinkMat.ClientID + "&Id=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + hid.ClientID + "&Case=" + CaseId.ToString() + "" '&EId=" + EqId.ToString() + "
            LinkMat.NavigateUrl = "#"
            LinkMat.Attributes.Add("onClick", "javascript:return ShowPopWindownew(this,'" + Path + "','" + ctlContentPlaceHolder.ClientID.ToString() + "_" + hid.ClientID + "')")
        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try


    End Sub

    Protected Sub GetEquipmentInDetailsold(ByRef LinkMat As HyperLink, ByVal hid As HiddenField, ByVal EqId As Integer, ByVal dsEquip As DataSet)
        Dim Ds As New DataSet
        Dim ObjGetdata As New Schem1GetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty
        Dim dvEquip As New DataView
        Dim dtEquip As New DataTable
        Try
            'Ds = ObjGetdata.GetEquipment(EqId, "", "")

            dvEquip = dsEquip.Tables(0).DefaultView
            dvEquip.RowFilter = "EQUIPID= " + EqId.ToString()
            dtEquip = dvEquip.ToTable()

            LinkMat.Text = dtEquip.Rows(0).Item("equipDES").ToString()
            hid.Value = EqId.ToString()
            Path = "../PopUp/GetEquipmentPopUp.aspx?Des=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + LinkMat.ClientID + "&Id=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + hid.ClientID + ""
            LinkMat.NavigateUrl = "javascript:ShowPopWindow('" + Path + "')"

        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetDeptDetails(ByRef LinkDep As HyperLink, ByVal hid As HiddenField, ByVal ProcId As Integer, ByVal dsDept As DataSet)
        Dim Ds As New DataSet
        Dim ObjGetdata As New Schem1GetData.Selectdata()
        Dim Path As String = String.Empty
        Dim dvDept As New DataView
        Dim dtDept As New DataTable
        Try
            'Ds = ObjGetdata.GetDept(ProcId, "")
            dvDept = dsDept.Tables(0).DefaultView
            dvDept.RowFilter = "PROCID= " + ProcId.ToString()
            dtDept = dvDept.ToTable()

            Path = "../PopUp/GetDepPopUp.aspx?Des=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + LinkDep.ClientID + "&Id=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + hid.ClientID + ""
            If dtDept.Rows.Count = 0 Then
                LinkDep.Text = "Dept. Conflict"
                LinkDep.ForeColor = Drawing.Color.DarkRed
            Else
                LinkDep.Text = dtDept.Rows(0).Item("PROCDE").ToString()
            End If

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

        Dim hidAssetId(29) As String
        Dim PARP(29) As String
        Dim ECP(29) As String
        Dim NGCP(29) As String
        Dim hidAssetDep(29) As String
        Dim WATERP(29) As String
        Dim ASSETNUM(29) As String
        Dim i As New Integer
        Dim ObjUpIns As New Schem1UpInsData.UpdateInsert()
        Dim obj As New CryptoHelper
        Try
            If Not objRefresh.IsRefresh Then
                For i = 1 To 30


                    hidAssetId(i - 1) = Request.Form("ctl00$Schem1ContentPlaceHolder$hidAssetId" + i.ToString() + "")
                    PARP(i - 1) = Request.Form("ctl00$Schem1ContentPlaceHolder$PLANTAREAP" + i.ToString() + "").Replace(",", "")
                    ECP(i - 1) = Request.Form("ctl00$Schem1ContentPlaceHolder$ERGYP" + i.ToString() + "").Replace(",", "")
                    NGCP(i - 1) = Request.Form("ctl00$Schem1ContentPlaceHolder$NTGP" + i.ToString() + "").Replace(",", "")
                    hidAssetDep(i - 1) = Request.Form("ctl00$Schem1ContentPlaceHolder$hidEqDepid" + i.ToString() + "")
                    WATERP(i - 1) = Request.Form("ctl00$Schem1ContentPlaceHolder$WATERP" + i.ToString() + "").Replace(",", "")
                    ASSETNUM(i - 1) = Request.Form("ctl00$Schem1ContentPlaceHolder$ASSETNUM" + i.ToString() + "")

                    'Check For IsNumric
                    If Not IsNumeric(PARP(i - 1)) Then
                        Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                    End If

                    If Not IsNumeric(ECP(i - 1)) Then
                        Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                    End If

                    If Not IsNumeric(NGCP(i - 1)) Then
                        Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                    End If
                    If Not IsNumeric(WATERP(i - 1)) Then
                        Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                    End If

                    If Not IsNumeric(ASSETNUM(i - 1)) Then 'Bug #375
                        Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                    End If

                    ' Check For Dependant-Indepdant Error
                    If CInt(hidAssetId(i - 1)) <> 0 Then
                        'Checking Dept.
                        If CDbl(hidAssetDep(i - 1)) <= 0 Then
                            Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE109").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        End If
                    End If

                Next
                ObjUpIns.EquipmentUpdate(CaseId, hidAssetId, PARP, ECP, NGCP, WATERP, hidAssetDep, ASSETNUM)
                Calculate()
            End If
            GetPageDetails()
        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
    Protected Sub Calculate()
        Try
            Dim Econ1Conn As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            Dim Sustain1Conn As String = System.Configuration.ConfigurationManager.AppSettings("Sustain1ConnectionString")
            Dim Echem1Conn As String = System.Configuration.ConfigurationManager.AppSettings("Echem1ConnectionString")
            Dim Schem1Conn As String = System.Configuration.ConfigurationManager.AppSettings("Schem1ConnectionString")
            Dim obj As New Schem1Calculation.Schem1Calculations(Schem1Conn, Echem1Conn, Sustain1Conn, Econ1Conn)
            obj.Schem1Calculate(CaseId)
        Catch ex As Exception
            ErrorLable.Text = "Error:Calculate:" + ex.Message.ToString() + ""
        End Try
    End Sub

End Class
