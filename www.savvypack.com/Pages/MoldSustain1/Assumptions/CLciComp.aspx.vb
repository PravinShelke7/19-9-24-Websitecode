Imports System.Data
Imports System.Data.OleDb
Imports System
Imports MoldS1GetData
Imports MoldS1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_MoldSustain1_Assumptions_CLciComp
    Inherits System.Web.UI.Page
    Dim _iCaseId As Integer
    Dim _iUserId As Integer
    Dim _Page As Integer = 10
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

    Protected Sub GetSessionDetails()
        Try
            UserId = Session("UserId")
            CaseId = Session("MoldS1CaseId")
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetSessionDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("Back") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If
            If Session("Service") = "COMPS1" Then
                S1Table.Attributes.Add("class", "S1CompModule")
            End If

            If Not IsPostBack Then
                ViewState("MaxCount") = "0"
                ViewState("Selected") = "1"
                ViewState("IsShowAll") = False
                btnPrivious.Enabled = False
                btnShowall.Text = "Show All"
                If Session("CompType") = "Energy" Then
                    lblHeaderText.Text = "Sustain1 Mold - LCI Comparison By Energy"
                    Page.Title = "S1 Mold-LCI Comparison By Energy"
                ElseIf Session("CompType") = "Green House Gas" Then
                    lblHeaderText.Text = "Sustain1 Mold - LCI Comparison By GHG"
                    Page.Title = "S1 Mold-LCI Comparison By GHG"
                Else
                    lblHeaderText.Text = "Sustain1 Mold - LCI Comparison By Water"
                    Page.Title = "S1 Mold-LCI Comparison By Water"
                End If
            End If
            GetSessionDetails()
            If Session("EffDate") <> "0" Then
                GetPageDetails()
            Else
                GetPageDetailsForLatestDate()
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString() + ""
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
        Dim trHeader3 As New TableRow
        Dim trHeaderFix As New TableRow
        Dim trHeaderFix1 As New TableRow
        Dim trHeaderFix2 As New TableRow
        Dim trInner As New TableRow
        Dim trInnerFix As New TableRow
        Dim tdHeader As TableCell
        Dim tdHeader2 As TableCell
        Dim tdHeader3 As TableCell
        Dim lbl As New Label
        Dim hid As New HiddenField
        Dim Link As New HyperLink
        Dim txt As New TextBox
        Dim tdInner As TableCell
        Dim chk As New CheckBox
        Dim radio As New RadioButton
        Dim dsMat As New DataSet
        Dim dsPref As New DataSet
        Dim lciId() As String
        Dim Title1 As String
        Dim _To As New Integer
        Dim _From As New Integer
        Dim _MatCnt As New Integer

        Try
            tblComparision.Rows.Clear()
            If Session("LSIIDS") <> Nothing Then
                lciId = Regex.Split(Session("LSIIDS"), ",")

                'Binding Header
                If lciId.Length > 0 Then
                    dsMat = objGetData.GetCompleteMaterials()
                    dsPref = objGetData.GetPref(CaseId)

                    trHeader = New TableRow
                    tdHeader2 = New TableCell
                    tdHeader = New TableCell
                    tdHeader2 = New TableCell
                    tdHeader3 = New TableCell

                    HeaderTdSetting(tdHeader, "120px", "Materials", "1")
                    Header2TdSetting(tdHeader2, "", "", "1")
                    Header2TdSetting(tdHeader3, "", "", "1")
                    tdHeader.Style.Add("text-align", "Center")
                    tdHeader.Style.Add("padding-left", "5px")
                    trHeader.Controls.Add(tdHeader)
                    trHeader2.Controls.Add(tdHeader2)
                    trHeader3.Controls.Add(tdHeader3)
                    trHeader.Height = 30
                    trHeader.Height = 30
                    tblComparision.Controls.Add(trHeader)
                    tblComparision.Controls.Add(trHeader2)
                    tblComparision.Controls.Add(trHeader3)


                    For i = 0 To lciId.Length - 1
                        tdHeader = New TableCell
                        tdHeader2 = New TableCell
                        tdHeader3 = New TableCell
                        If Session("CompType") = "Energy" Then
                            Title1 = "(MJ/" + dsPref.Tables(0).Rows(0).Item("TITLE8").ToString() + " mat)"
                        ElseIf Session("CompType") = "Green House Gas" Then
                            Title1 = "(" + dsPref.Tables(0).Rows(0).Item("TITLE8").ToString() + " CO2/" + dsPref.Tables(0).Rows(0).Item("TITLE8").ToString() + " mat)"
                        ElseIf Session("CompType") = "Water" Then
                            Title1 = "(" + dsPref.Tables(0).Rows(0).Item("TITLE10").ToString() + " Water/" + dsPref.Tables(0).Rows(0).Item("TITLE8").ToString() + " mat)"
                        End If

                        lbl = New Label()
                        HeaderTdSetting(tdHeader, "75px", GetInventoryDetails(lciId(i)), "1")
                        Header2TdSetting(tdHeader2, "", Title1, "1")
                        Header2TdSetting(tdHeader3, "", Session("EffDate").ToString(), "1")
                        tdHeader.Style.Add("text-align", "Center")
                        tdHeader.Style.Add("padding-left", "5px")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader3.Controls.Add(tdHeader3)
                        tblComparision.Controls.Add(trHeader)
                        tblComparision.Controls.Add(trHeader2)
                        tblComparision.Controls.Add(trHeader3)
                    Next
                End If

                _MatCnt = dsMat.Tables(0).Rows.Count
                ViewState("MaxCount") = _MatCnt
                If Convert.ToInt32(ViewState("Selected")) = 1 Then
                    _To = 0
                    _From = 9
                Else
                    '_To = Convert.ToInt32(ViewState("Selected")) + 9 * Convert.ToInt32(ViewState("Selected"))
                    _To = (Convert.ToInt32(ViewState("Selected")) - 1) + 9 * (Convert.ToInt32(ViewState("Selected")) - 1)
                    _From = _To + 9
                    If _From >= _MatCnt Then
                        _From = _MatCnt - 1
                    End If
                End If
                If ViewState("IsShowAll") Then
                    _To = 0
                    _From = _MatCnt - 1
                End If
                'Binding inner

                For i = _To To _From
                    trInner = New TableRow

                    'Adding MaterailDetails
                    tdInner = New TableCell
                    lbl = New Label()
                    InnerTdSetting(tdInner, "", "Left")
                    GetMaterialDetails(lbl, CInt(dsMat.Tables(0).Rows(i).Item("MATID").ToString()))
                    tdInner.Controls.Add(lbl)
                    trInner.Controls.Add(tdInner)
                    tblComparision.Controls.Add(trInner)

                    'Binding inventoryType calculations dynamically
                    For j = 0 To lciId.Length - 1
                        tdInner = New TableCell
                        lbl = New Label()
                        InnerTdSetting(tdInner, "", "Right")
                        If Session("CompType") = "Energy" Then
                            GetLCIDataByMatID(lbl, lciId(j), dsMat.Tables(0).Rows(i).Item("MATID").ToString(), Session("EffDate").ToString())
                        ElseIf Session("CompType") = "Green House Gas" Then
                            GetLCICo2DataByMatID(lbl, lciId(j), dsMat.Tables(0).Rows(i).Item("MATID").ToString(), Session("EffDate").ToString())
                        ElseIf Session("CompType") = "Water" Then
                            GetLCIWaterDataByMatID(lbl, lciId(j), dsMat.Tables(0).Rows(i).Item("MATID").ToString(), Session("EffDate").ToString())
                        End If


                        tdInner.Controls.Add(lbl)
                        trInner.Controls.Add(tdInner)
                        tblComparision.Controls.Add(trInner)
                    Next
                    If (i Mod 2 = 0) Then
                        trInner.CssClass = "AlterNateColor1"
                    Else
                        trInner.CssClass = "AlterNateColor2"
                    End If
                Next

            End If

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetPageDetailsForLatestDate()
        Dim ds As New DataSet
        Dim objGetData As New MoldS1GetData.Selectdata
        Dim i As New Integer
        Dim j As New Integer
        Dim DWidth As String = String.Empty
        Dim trHeader As New TableRow
        Dim trHeader1 As New TableRow
        Dim trHeader2 As New TableRow
        Dim trHeaderFix As New TableRow
        Dim trHeaderFix1 As New TableRow
        Dim trHeaderFix2 As New TableRow
        Dim trInner As New TableRow
        Dim trInnerFix As New TableRow
        Dim tdHeader As TableCell
        Dim tdHeader2 As TableCell
        Dim tdHeader1 As TableCell
        Dim lbl As New Label
        Dim hid As New HiddenField
        Dim Link As New HyperLink
        Dim txt As New TextBox
        Dim tdInner As TableCell
        Dim chk As New CheckBox
        Dim radio As New RadioButton
        Dim dsMat As New DataSet
        Dim dsPref As New DataSet
        Dim lciId() As String
        Dim LCIDates() As String
        Dim Title1 As String
        Dim _To As New Integer
        Dim _From As New Integer
        Dim _MatCnt As New Integer


        Try
            tblComparision.Rows.Clear()
            If Session("LSIIDS") <> Nothing Then
                lciId = Regex.Split(Session("LSIIDS"), ",")

                'Binding Header
                If lciId.Length > 0 Then
                    dsMat = objGetData.GetCompleteMaterials()
                    dsPref = objGetData.GetPref(CaseId)

                    trHeader = New TableRow
                    tdHeader2 = New TableCell
                    tdHeader = New TableCell
                    tdHeader2 = New TableCell
                    tdHeader1 = New TableCell

                    HeaderTdSetting(tdHeader, "200px", "Materials", "1")
                    Header2TdSetting(tdHeader2, "", "", "1")
                    Header2TdSetting(tdHeader1, "", "", "1")
                    tdHeader.Style.Add("text-align", "Center")
                    tdHeader2.Style.Add("text-align", "Center")
                    tdHeader1.Style.Add("text-align", "Center")
                    tdHeader.Style.Add("padding-left", "5px")
                    trHeader.Controls.Add(tdHeader)
                    trHeader2.Controls.Add(tdHeader2)
                    trHeader1.Controls.Add(tdHeader1)
                    trHeader.Height = 30
                    trHeader.Height = 30
                    tblComparision.Controls.Add(trHeader)
                    tblComparision.Controls.Add(trHeader2)
                    tblComparision.Controls.Add(trHeader1)

                    For i = 0 To lciId.Length - 1
                        tdHeader = New TableCell
                        tdHeader2 = New TableCell
                        tdHeader1 = New TableCell
                        If Session("CompType") = "Energy" Then
                            Title1 = "(MJ/" + dsPref.Tables(0).Rows(0).Item("TITLE8").ToString() + " mat)"
                        ElseIf Session("CompType") = "Green House Gas" Then
                            Title1 = "(" + dsPref.Tables(0).Rows(0).Item("TITLE8").ToString() + " CO2/" + dsPref.Tables(0).Rows(0).Item("TITLE8").ToString() + " mat)"
                        ElseIf Session("CompType") = "Water" Then
                            Title1 = "(" + dsPref.Tables(0).Rows(0).Item("TITLE10").ToString() + " Water/" + dsPref.Tables(0).Rows(0).Item("TITLE8").ToString() + " mat)"
                        End If
                        lbl = New Label()
                        HeaderTdSetting(tdHeader, "75px", GetInventoryDetails(lciId(i)), "1")
                        Header2TdSetting(tdHeader2, "", Title1, "1")
                        Header2TdSetting(tdHeader1, "", GetLatestLCIDate(lciId(i)), "1")
                        tdHeader.Style.Add("text-align", "Center")
                        tdHeader.Style.Add("padding-left", "5px")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                        tblComparision.Controls.Add(trHeader)
                        tblComparision.Controls.Add(trHeader2)
                        tblComparision.Controls.Add(trHeader1)
                    Next
                End If

                _MatCnt = dsMat.Tables(0).Rows.Count
                ViewState("MaxCount") = _MatCnt
                If Convert.ToInt32(ViewState("Selected")) = 1 Then
                    _To = 0
                    _From = 9
                Else
                    '_To = Convert.ToInt32(ViewState("Selected")) + 9 * Convert.ToInt32(ViewState("Selected"))
                    _To = (Convert.ToInt32(ViewState("Selected")) - 1) + 9 * (Convert.ToInt32(ViewState("Selected")) - 1)
                    _From = _To + 9
                    If _From >= _MatCnt Then
                        _From = _MatCnt - 1
                    End If
                End If
                If ViewState("IsShowAll") Then
                    _To = 0
                    _From = _MatCnt - 1
                End If
                'Binding inner

                'Splitting latest LCI Dates
                LCIDates = Regex.Split(hdnLatestDate.Value, "-")

                For i = _To To _From
                    trInner = New TableRow

                    'Adding MaterailDetails
                    tdInner = New TableCell
                    lbl = New Label()
                    InnerTdSetting(tdInner, "", "Left")
                    GetMaterialDetails(lbl, CInt(dsMat.Tables(0).Rows(i).Item("MATID").ToString()))
                    tdInner.Controls.Add(lbl)
                    trInner.Controls.Add(tdInner)
                    tblComparision.Controls.Add(trInner)

                    'Binding inventoryType calculations dynamically
                    For j = 0 To lciId.Length - 1
                        tdInner = New TableCell
                        lbl = New Label()
                        InnerTdSetting(tdInner, "", "Right")
                        If Session("CompType") = "Energy" Then
                            GetLCIDataByMatID(lbl, lciId(j), dsMat.Tables(0).Rows(i).Item("MATID").ToString(), LCIDates(j))
                        ElseIf Session("CompType") = "Green House Gas" Then
                            GetLCICo2DataByMatID(lbl, lciId(j), dsMat.Tables(0).Rows(i).Item("MATID").ToString(), LCIDates(j))
                        ElseIf Session("CompType") = "Water" Then
                            GetLCIWaterDataByMatID(lbl, lciId(j), dsMat.Tables(0).Rows(i).Item("MATID").ToString(), LCIDates(j))
                        End If

                        tdInner.Controls.Add(lbl)
                        trInner.Controls.Add(tdInner)
                        tblComparision.Controls.Add(trInner)
                    Next
                    If (i Mod 2 = 0) Then
                        trInner.CssClass = "AlterNateColor1"
                    Else
                        trInner.CssClass = "AlterNateColor2"
                    End If
                Next

            End If

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetLCIDataByMatID(ByRef lblInvDetail As Label, ByVal InventoryId As String, ByVal MatId As String, ByVal strDate As String)
        Dim Ds As New DataSet
        Dim ObjGetdata As New MoldS1GetData.Selectdata()

        Dim Path As String = String.Empty
        Try
            Ds = ObjGetdata.GetLCIDataByMatAndDate(InventoryId, MatId, CaseId, strDate)
            If Ds.Tables(0).Rows.Count > 0 Then
                lblInvDetail.Text = FormatNumber(Ds.Tables(0).Rows(0).Item("EnergyVal").ToString(), 1)
                lblInvDetail.CssClass = "NormalLable"
            Else
                lblInvDetail.Text = "na"
                lblInvDetail.CssClass = "NormalLable"
            End If

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetInventoryDetails" + ex.Message.ToString()

        End Try
    End Sub

    Protected Sub GetLCICo2DataByMatID(ByRef lblInvDetail As Label, ByVal InventoryId As String, ByVal MatId As String, ByVal strDate As String)
        Dim Ds As New DataSet
        Dim ObjGetdata As New MoldS1GetData.Selectdata()

        Dim Path As String = String.Empty
        Try
            Ds = ObjGetdata.GetLCIDataByMatAndDate(InventoryId, MatId, CaseId, strDate)
            If Ds.Tables(0).Rows.Count > 0 Then
                lblInvDetail.Text = FormatNumber(Ds.Tables(0).Rows(0).Item("Co2Val").ToString(), 3)
                lblInvDetail.CssClass = "NormalLable"
            Else
                lblInvDetail.Text = "na"
                lblInvDetail.CssClass = "NormalLable"
            End If

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetInventoryDetails" + ex.Message.ToString()

        End Try
    End Sub

    Protected Function GetInventoryDetails(ByVal InventoryId As String) As String
        Dim Ds As New DataSet
        Dim ObjGetdata As New MoldS1GetData.Selectdata()
        Dim lblInvDetail As String = ""
        Dim Path As String = String.Empty
        Try
            Ds = ObjGetdata.GetInventoryDetail(InventoryId)
            If Ds.Tables(0).Rows.Count > 0 Then
                lblInvDetail = Ds.Tables(0).Rows(0).Item("INVENTORY").ToString()
            End If
            Return lblInvDetail
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetInventoryDetails" + ex.Message.ToString()
            Return lblInvDetail
        End Try
    End Function

    Protected Function GetLatestLCIDate(ByVal InventoryId As String) As String
        Dim Ds As New DataSet
        Dim ObjGetdata As New MoldS1GetData.Selectdata()
        Dim strDate As String = ""
        Dim Path As String = String.Empty
        Try
            Ds = ObjGetdata.GetLatestLCIDate(InventoryId)
            If Ds.Tables(0).Rows.Count > 0 Then
                strDate = Ds.Tables(0).Rows(0).Item("EDATE").ToString()
                If hdnLatestDate.Value = "" Then
                    hdnLatestDate.Value = strDate
                Else
                    hdnLatestDate.Value = hdnLatestDate.Value + "-" + strDate
                End If
            End If
            Return strDate
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetInventoryDetails" + ex.Message.ToString()
            Return strDate
        End Try
    End Function

    Protected Sub GetMaterialDetails(ByRef lbl As Label, ByVal MatId As Integer)
        Dim Ds As New DataSet
        Dim ObjGetdata As New MoldS1GetData.Selectdata()
        Dim Path As String = String.Empty
        Try
            Ds = ObjGetdata.GetMaterials(MatId, "", "")
            lbl.Text = Ds.Tables(0).Rows(0).Item("MATDES").ToString()
        Catch ex As Exception
            _lErrorLble.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
    Protected Sub GetLCIWaterDataByMatID(ByRef lblInvDetail As Label, ByVal InventoryId As String, ByVal MatId As String, ByVal strDate As String)
        Dim Ds As New DataSet
        Dim ObjGetdata As New MoldS1GetData.Selectdata()

        Dim Path As String = String.Empty
        Try
            Ds = ObjGetdata.GetLCIDataByMatAndDate(InventoryId, MatId, CaseId, strDate)
            If Ds.Tables(0).Rows.Count > 0 Then
                lblInvDetail.Text = FormatNumber(Ds.Tables(0).Rows(0).Item("WaterVal").ToString(), 3)
                lblInvDetail.CssClass = "NormalLable"
            Else
                lblInvDetail.Text = "na"
                lblInvDetail.CssClass = "NormalLable"
            End If

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetLCIWaterDataByMatID" + ex.Message.ToString()

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
            Td.Style.Add("font-size", "13px")
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

    Protected Sub btnPrivious_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrivious.Click
        Try
            If Convert.ToInt32(ViewState("Selected")) > 1 Then
                ViewState("Selected") = Convert.ToInt32(ViewState("Selected")) - 1
                btnNext.Enabled = True
                If Session("EffDate") <> "0" Then
                    GetPageDetails()
                Else
                    GetPageDetailsForLatestDate()
                End If
            End If

            If Convert.ToInt32(ViewState("Selected")) = 1 Then
                btnPrivious.Enabled = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Try
            If Convert.ToInt32(ViewState("Selected")) <= Convert.ToInt32(ViewState("MaxCount")) \ _Page Then
                ViewState("Selected") = Convert.ToInt32(ViewState("Selected")) + 1
                btnPrivious.Enabled = True
                If Session("EffDate") <> "0" Then
                    GetPageDetails()
                Else
                    GetPageDetailsForLatestDate()
                End If
            End If
            If Convert.ToInt32(ViewState("Selected")) > Convert.ToInt32(ViewState("MaxCount")) \ _Page Then
                btnNext.Enabled = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnShowall_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShowall.Click
        Try
            If ViewState("IsShowAll") Then
                ViewState("IsShowAll") = False
                btnPrivious.Enabled = False
                btnNext.Enabled = True
                btnShowall.Text = "Show All"
                ViewState("Selected") = 1
            Else
                btnPrivious.Enabled = False
                btnNext.Enabled = False
                btnShowall.Text = "Show Paging"
                ViewState("IsShowAll") = True

            End If
            If Session("EffDate") <> "0" Then
                GetPageDetails()
            Else
                GetPageDetailsForLatestDate()
            End If

        Catch ex As Exception

        End Try
    End Sub
End Class
