Imports System.Data
Imports System.Data.OleDb
Imports System
Imports E1GetData
Imports E1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter

Partial Class Pages_Econ1_MaterialAssistant
    Inherits System.Web.UI.Page
    Dim dBaseMG As New Double
    Dim dBaseG As New Double
    Dim dMetG As New Double
    Dim dPondG As New Double
    Dim arrThickId(100) As String
    Dim arrdBaseM(100) As String
    Dim arrdBase(100) As String
    Dim arrdMet(100) As String
    Dim arrdPond(100) As String
    'Coat Global Variable
    Dim CdBaseG As New Double
    Dim CdMetG As New Double
    Dim CdPondG As New Double
    Dim arrCoatId(100) As String
    Dim arrCdBase(100) As String
    Dim arrCdMet(100) As String
    Dim arrCdPond(100) As String
    'Order Global Variable
    Dim OdBaseG As New Double
    Dim OdMetG As New Double
    Dim OdPondG As New Double
    Dim arrOrderId(100) As String
    Dim arrOdBase(100) As String
    Dim arrOdMet(100) As String
    Dim arrOdPond(100) As String
    'Coil Weight Global Variable
    Dim CWdBaseG As New Double
    Dim CWdMetG As New Double
    Dim CWdPondG As New Double
    Dim arrCoilWtId(100) As String
    Dim arrCWdBase(100) As String
    Dim arrCWdMet(100) As String
    Dim arrCWdPond(100) As String
    'Quality Global Variable
    Dim QdBaseG As New Double
    Dim QdMetG As New Double
    Dim QdPondG As New Double
    Dim arrQualId(100) As String
    Dim arrQdBase(100) As String
    Dim arrQdMet(100) As String
    Dim arrQdPond(100) As String
    'Coil Width Global Variable
    Dim CDdBaseG As New Double
    Dim CDdMetG As New Double
    Dim CDdPondG As New Double
    Dim arrCoilWdId(100) As String
    Dim arrCDdBase(100) As String
    Dim arrCDdMet(100) As String
    Dim arrCDdPond(100) As String
    'General Global Variable
    Dim GdBaseG As New Double
    Dim GdMetG As New Double
    Dim GdPondG As New Double
    Dim arrGenId(100) As String
    Dim arrGdBase(100) As String
    Dim arrGdMet(100) As String
    Dim arrGdPond(100) As String
    'Trim Tolerance Global Variable
    Dim TdBaseG As New Double
    Dim TdMetG As New Double
    Dim TdPondG As New Double
    Dim arrTrimId(100) As String
    Dim arrTdBase(100) As String
    Dim arrTdMet(100) As String
    Dim arrTdPond(100) As String
    'Cores Global Variable
    Dim CRdBaseG As New Double
    Dim CRdMetG As New Double
    Dim CRdPondG As New Double
    Dim arrCoresId(100) As String
    Dim arrCRdBase(100) As String
    Dim arrCRdMet(100) As String
    Dim arrCRdPond(100) As String
    'Coil ID Global Variable
    Dim CIdBaseG As New Double
    Dim CIdMetG As New Double
    Dim CIdPondG As New Double
    Dim arrCoilId(100) As String
    Dim arrCIdBase(100) As String
    Dim arrCIdMet(100) As String
    Dim arrCIdPond(100) As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                hidBox.Value = ""
                hidCoat.Value = ""
                hidOrder.Value = ""
                hidCoilWt.Value = ""
                hidQual.Value = ""
                hidCoilWd.Value = ""
                hidGen.Value = ""
                hidTrim.Value = ""
                hidPack.Value = ""
                hidPackg.Value = ""
                hidSelBox.Value = ""
                hidSelCoat.Value = ""
                hidSelOrder.Value = ""
                hidSelCoilWt.Value = ""
                hidSelQual.Value = ""
                hidSelCoilWd.Value = ""
                hidSelGen.Value = ""
                hidSelTrim.Value = ""
                hidSelPack.Value = ""
                hidSelPackg.Value = ""
                hidMatId.Value = Request.QueryString("MatId").ToString()
                GetDateDetails()
            End If
            GetDefaultValue()
            If Not IsPostBack Then
                GetHeadDetails()
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    Public Sub GetDateDetails()
        Dim ds As New DataSet
        Dim objGetData As New Selectdata()
        Try
            ds = objGetData.GetEffDateDetails()

            With ddlDate
                .DataSource = ds
                .DataTextField = "EFFDATE"
                .DataValueField = "EFFDATE"
                .DataBind()
            End With
        Catch ex As Exception

        End Try
    End Sub

    Public Sub GetHeadDetails()
        Dim tdHeader As New TableCell
        Dim trHeader As New TableRow
        Dim tdHeader1 As New TableCell
        Dim trHeader1 As New TableRow
        Dim trInner As New TableRow
        Dim tdInner As New TableCell
        Dim lbl As New Label
        Dim dsPref As New DataSet
        Dim dsMat As New DataSet
        Dim rdb As New RadioButton
        Dim BBTotal As New Double
        Dim metTotal As New Double
        Dim kgTotal As New Double
        Dim lbTotal As New Double
        Try
            tblHead.Rows.Clear()
            trHeader = New TableRow
            For i = 1 To 5
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "50px", "Price Components", "1")
                        Header2TdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 2
                        HeaderTdSetting(tdHeader, "70px", "$/baseBox", "1")
                        Header2TdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 3
                        HeaderTdSetting(tdHeader, "100px", "$/MT", "1")
                        Header2TdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 4
                        HeaderTdSetting(tdHeader, "120px", "$/kg", "1")
                        Header2TdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 5
                        HeaderTdSetting(tdHeader, "", "$/lb", "1")
                        Header2TdSetting(tdHeader1, "70px", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                End Select
            Next
            trHeader.Height = 30
            tblHead.Controls.Add(trHeader)
            tblHead.Controls.Add(trHeader1)

            'Inner
            For i = 0 To 10 'dsData.Tables(0).Rows.Count - 1
                trInner = New TableRow
                Dim thick As New Double
                Dim adj As New Double
                Dim baseM As New Double
                Dim dBase As New Double
                Dim dMet As New Double
                Dim dPond As New Double
                Dim val As New Double
                Dim metVal As New Double
                Dim kgVal As New Double
                For j = 1 To 5
                    tdInner = New TableCell
                    Select Case j
                        Case 1
                            InnerTdSetting(tdInner, "200px", "Left")
                            lbl = New Label
                            If i = 0 Then
                                lbl.Text = "Base price adjusted for Thickness"
                            ElseIf i = 1 Then
                                lbl.Text = "Adjustment for coat weight and type"
                            ElseIf i = 2 Then
                                lbl.Text = "Order Quantity"
                            ElseIf i = 3 Then
                                lbl.Text = "Coil Weight"
                            ElseIf i = 4 Then
                                lbl.Text = "Quality"
                            ElseIf i = 5 Then
                                lbl.Text = "Coil Width"
                            ElseIf i = 6 Then
                                lbl.Text = "General"
                            ElseIf i = 7 Then
                                lbl.Text = "Trim Tolerance"
                            ElseIf i = 8 Then
                                lbl.Text = "Cores"
                            ElseIf i = 9 Then
                                lbl.Text = "Coil Id"
                            ElseIf i = 10 Then
                                lbl.Text = "Total Price"
                            End If

                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 2
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            If i = 0 Then
                                val = dBaseG
                                BBTotal = BBTotal + dBaseG
                            ElseIf i = 1 Then
                                val = CdBaseG.ToString()
                                BBTotal = BBTotal + CdBaseG
                            ElseIf i = 2 Then
                                val = OdBaseG.ToString()
                                BBTotal = BBTotal + OdBaseG
                            ElseIf i = 3 Then
                                val = CWdBaseG.ToString()
                                BBTotal = BBTotal + CWdBaseG
                            ElseIf i = 4 Then
                                val = QdBaseG.ToString()
                                BBTotal = BBTotal + QdBaseG
                            ElseIf i = 5 Then
                                val = CDdBaseG.ToString()
                                BBTotal = BBTotal + CDdBaseG
                            ElseIf i = 6 Then
                                val = GdBaseG.ToString()
                                BBTotal = BBTotal + GdBaseG
                            ElseIf i = 7 Then
                                val = TdBaseG.ToString()
                                BBTotal = BBTotal + TdBaseG
                            ElseIf i = 8 Then
                                val = CRdBaseG.ToString()
                                BBTotal = BBTotal + CRdBaseG
                            ElseIf i = 9 Then
                                val = CIdBaseG.ToString()
                                BBTotal = BBTotal + CIdBaseG
                            Else
                                lbl.Text = FormatNumber(BBTotal, 2)
                            End If
                            If i <> 10 Then
                                lbl.Text = FormatNumber(val, 2)
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 3
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            If i = 0 Then
                                lbl.Text = FormatNumber(dMetG, 2)
                                metTotal = metTotal + dMetG
                                metVal = dMetG / 1000
                            ElseIf i = 10 Then
                                lbl.Text = FormatNumber(metTotal, 2)
                            Else
                                lbl.Text = FormatNumber(val * dBaseMG, 2)
                                metTotal = metTotal + (val * dBaseMG)
                                metVal = (val * dBaseMG) / 1000
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 4
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            If i = 10 Then
                                lbl.Text = FormatNumber(kgTotal, 3)
                            Else
                                lbl.Text = FormatNumber(metVal, 3)
                                kgTotal = kgTotal + metVal
                            End If

                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 5
                            InnerTdSetting(tdInner, "", "Center")
                            lbl = New Label
                            If i = 10 Then
                                lbl.Text = FormatNumber(lbTotal, 3)
                            Else
                                lbl.Text = FormatNumber(metVal * 0.453592, 3)
                                lbTotal = lbTotal + (metVal * 0.453592)
                            End If

                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                    End Select
                Next
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
                trInner.Height = 30
                tblHead.Controls.Add(trInner)
            Next

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetHeadDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Public Sub GetDefaultValue()
        Dim ds As New DataSet
        Dim objGetData As New Selectdata()
        Try
            ds = objGetData.GetSteelCaseDetails(Session("E1CaseId"), hidMatId.Value, ddlDate.SelectedValue)
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    If ds.Tables(0).Rows(i).Item("COMPONENTID").ToString() = "1" Then
                        hidSelBox.Value = ds.Tables(0).Rows(i).Item("SELVALUE").ToString()
                    ElseIf ds.Tables(0).Rows(i).Item("COMPONENTID").ToString() = "2" Then
                        hidSelCoat.Value = ds.Tables(0).Rows(i).Item("SELVALUE").ToString()
                    ElseIf ds.Tables(0).Rows(i).Item("COMPONENTID").ToString() = "3" Then
                        hidSelOrder.Value = ds.Tables(0).Rows(i).Item("SELVALUE").ToString()
                    ElseIf ds.Tables(0).Rows(i).Item("COMPONENTID").ToString() = "4" Then
                        hidSelCoilWt.Value = ds.Tables(0).Rows(i).Item("SELVALUE").ToString()
                    ElseIf ds.Tables(0).Rows(i).Item("COMPONENTID").ToString() = "5" Then
                        hidSelQual.Value = ds.Tables(0).Rows(i).Item("SELVALUE").ToString()
                    ElseIf ds.Tables(0).Rows(i).Item("COMPONENTID").ToString() = "6" Then
                        hidSelCoilWd.Value = ds.Tables(0).Rows(i).Item("SELVALUE").ToString()
                    ElseIf ds.Tables(0).Rows(i).Item("COMPONENTID").ToString() = "7" Then
                        hidSelGen.Value = ds.Tables(0).Rows(i).Item("SELVALUE").ToString()
                    ElseIf ds.Tables(0).Rows(i).Item("COMPONENTID").ToString() = "8" Then
                        hidSelTrim.Value = ds.Tables(0).Rows(i).Item("SELVALUE").ToString()
                    ElseIf ds.Tables(0).Rows(i).Item("COMPONENTID").ToString() = "9" Then
                        hidSelPack.Value = ds.Tables(0).Rows(i).Item("SELVALUE").ToString()
                    ElseIf ds.Tables(0).Rows(i).Item("COMPONENTID").ToString() = "10" Then
                        hidSelPackg.Value = ds.Tables(0).Rows(i).Item("SELVALUE").ToString()
                    End If
                Next
            End If

            GetPageDetails()
            GetCoatDetails()
            GetOrderDetails()
            GetCoilDetails()
            GetQualityDetails()
            GetCoilWdDetails()
            GetGeneralDetails()
            GetTrimTolDetails()
            GetPackDetails()
            GetPackagingDetails()
        Catch ex As Exception

        End Try
    End Sub

    Public Sub GetPageDetails()
        Dim ds As New DataSet
        Dim objGetData As New Selectdata()
        Dim tdHeader As New TableCell
        Dim trHeader As New TableRow
        Dim tdHeader1 As New TableCell
        Dim trHeader1 As New TableRow
        Dim dsData As New DataSet
        Dim trInner As New TableRow
        Dim tdInner As New TableCell
        Dim lbl As New Label
        Dim dsPref As New DataSet
        Dim dsMat As New DataSet
        Dim rdb As New RadioButton
        Dim val As New Double
        Dim dvData As New DataView
        Dim dtData As New DataTable
        Try
            tblData.Rows.Clear()
            ds = objGetData.GetMaterials(hidMatId.Value, "", "")
            dsMat = objGetData.GetMatArch(hidMatId.Value, Session("E1CaseId"))
            dsData = objGetData.GetThickAssist(ddlDate.SelectedValue)
            dvData = dsData.Tables(0).DefaultView
            dsPref = objGetData.GetPref(Session("E1CaseId"))
            lblTitle.Text = "Material " + ds.Tables(0).Rows(0).Item("MATID").ToString() + ":" + ds.Tables(0).Rows(0).Item("MATDES").ToString()
            dvData.RowFilter = "ISDEFAULT='Y'"
            dtData = dvData.ToTable
            trHeader = New TableRow
            For i = 1 To 9
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "50px", "", "1")
                        Header2TdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 2
                        HeaderTdSetting(tdHeader, "70px", "Thickness", "1")
                        Header2TdSetting(tdHeader1, "0", "(mm)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 3
                        HeaderTdSetting(tdHeader, "100px", "Steel Area", "1")
                        Header2TdSetting(tdHeader1, "0", "($/lb)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 4
                        HeaderTdSetting(tdHeader, "120px", "Adjustment", "1")
                        Header2TdSetting(tdHeader1, "0", "($/baseBox)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 5
                        HeaderTdSetting(tdHeader, "", "", "1")
                        Header2TdSetting(tdHeader1, "70px", "($/baseBox)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 6
                        HeaderTdSetting(tdHeader, "", "", "1")
                        Header2TdSetting(tdHeader1, "70px", "(baseBox/mt)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 7
                        HeaderTdSetting(tdHeader, "", "", "1")
                        Header2TdSetting(tdHeader1, "70px", "($/baseBox)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 8
                        HeaderTdSetting(tdHeader, "90px", "", "1")
                        Header2TdSetting(tdHeader1, "", "($/mt)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 9
                        HeaderTdSetting(tdHeader, "100px", "", "1")
                        Header2TdSetting(tdHeader1, "", "($/lb)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                End Select
            Next
            trHeader.Height = 30
            tblData.Controls.Add(trHeader)
            tblData.Controls.Add(trHeader1)

            If dtData.Rows.Count > 0 Then
                val = 2205 / dtData.Rows(0).Item("BASEBOXNO")
            End If
            Dim baseVal As New Double
            baseVal = dsMat.Tables(0).Rows(0).Item("PRICE") / val * 2205
            'Inner
            For i = 0 To dsData.Tables(0).Rows.Count - 1
                trInner = New TableRow
                Dim thick As New Double
                Dim adj As New Double
                Dim baseM As New Double
                Dim dBase As New Double
                Dim dMet As New Double
                Dim dPond As New Double

                thick = ((dsData.Tables(0).Rows(i).Item("BASEBOXNO") * 0.453592) * 1000 / 7.85) / 202322.2 * 10
                adj = dsData.Tables(0).Rows(i).Item("ADJUSTVAL") - 7.2
                baseM = 2205 / dsData.Tables(0).Rows(i).Item("BASEBOXNO")
                dBase = baseVal + adj
                dMet = dBase * baseM
                dPond = dBase / dsData.Tables(0).Rows(i).Item("BASEBOXNO")
                For j = 1 To 9
                    tdInner = New TableCell
                    Select Case j
                        Case 1
                            InnerTdSetting(tdInner, "", "Center")
                            rdb = New RadioButton
                            rdb.GroupName = "baseBox"
                            rdb.ID = "rdbBox" + (i + 1).ToString()
                            lbl = New Label()
                            lbl.ID = "lblBox" + dsData.Tables(0).Rows(i).Item("BASEBOXNO").ToString()

                            If hidBox.Value <> "" Then
                                If hidBox.Value <> rdb.ID.ToString() Then
                                    rdb.Checked = False
                                Else
                                    rdb.Checked = True
                                    hidSelBox.Value = dsData.Tables(0).Rows(i).Item("ASSISTTHICKID").ToString()
                                    dBaseMG = baseM
                                    dBaseG = dBase
                                    dMetG = dMet
                                    dPondG = dPond
                                End If
                            Else
                                If hidSelBox.Value <> "" Then
                                    If hidSelBox.Value = dsData.Tables(0).Rows(i).Item("ASSISTTHICKID").ToString() Then
                                        rdb.Checked = True
                                        dBaseMG = baseM
                                        dBaseG = dBase
                                        dMetG = dMet
                                        dPondG = dPond
                                    Else
                                        rdb.Checked = False
                                    End If
                                Else
                                    If dsData.Tables(0).Rows(i).Item("ISDEFAULT").ToString() = "N" Then
                                        rdb.Checked = False
                                    Else
                                        rdb.Checked = True
                                        hidSelBox.Value = dsData.Tables(0).Rows(i).Item("ASSISTTHICKID").ToString()
                                        dBaseMG = baseM
                                        dBaseG = dBase
                                        dMetG = dMet
                                        dPondG = dPond
                                    End If
                                End If

                            End If
                            arrdBaseM(i) = baseM
                            arrdBase(i) = dBase
                            arrdMet(i) = dMet
                            arrdPond(i) = dPond
                            arrThickId(i) = dsData.Tables(0).Rows(i).Item("ASSISTTHICKID").ToString()

                            AddHandler rdb.CheckedChanged, AddressOf rdb_CheckedChanged
                            rdb.AutoPostBack = True
                            lbl.Visible = False
                            tdInner.Controls.Add(rdb)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 2
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            lbl.Text = FormatNumber(thick, 3)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 3
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            lbl.Text = dsData.Tables(0).Rows(i).Item("BASEBOXNO").ToString()
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 4
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            lbl.Text = dsData.Tables(0).Rows(i).Item("ADJUSTVAL").ToString()
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 5
                            InnerTdSetting(tdInner, "", "Center")
                            lbl = New Label
                            lbl.Text = FormatNumber(adj, 1)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 6
                            InnerTdSetting(tdInner, "", "Center")
                            lbl = New Label
                            lbl.Text = FormatNumber(baseM, 2)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 7
                            InnerTdSetting(tdInner, "", "Center")
                            lbl = New Label
                            lbl.Text = FormatNumber(dBase, 2)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 8
                            InnerTdSetting(tdInner, "", "Center")
                            lbl = New Label
                            lbl.Text = FormatNumber(dMet, 2)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 9
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            lbl.Text = FormatNumber(dPond, 3)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 9
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            lbl.Text = dsData.Tables(0).Rows(i).Item("OWASTE").ToString()
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 10
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            lbl.Text = dsData.Tables(0).Rows(i).Item("WASTE").ToString()
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                    End Select
                Next
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
                trInner.Height = 30
                tblData.Controls.Add(trInner)
            Next

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Public Sub GetCoatDetails()
        Dim objGetData As New Selectdata()
        Dim tdHeader As New TableCell
        Dim trHeader As New TableRow
        Dim tdHeader1 As New TableCell
        Dim trHeader1 As New TableRow
        Dim dsData As New DataSet
        Dim trInner As New TableRow
        Dim tdInner As New TableCell
        Dim lbl As New Label
        Dim rdb As New RadioButton
        Try

            dsData = objGetData.GetCoatAssist(ddlDate.SelectedValue)
            trHeader = New TableRow
            For i = 1 To 5
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "50px", "", "1")
                        Header2TdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 2
                        HeaderTdSetting(tdHeader, "70px", "Side One", "1")
                        Header2TdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 3
                        HeaderTdSetting(tdHeader, "70px", "Side Two", "1")
                        Header2TdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 4
                        HeaderTdSetting(tdHeader, "100px", "Adjustment", "1")
                        Header2TdSetting(tdHeader1, "0", "($/baseBox)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 5
                        HeaderTdSetting(tdHeader, "120px", "", "1")
                        Header2TdSetting(tdHeader1, "0", "($/baseBox)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                End Select
            Next
            trHeader.Height = 30
            tblCoat.Controls.Add(trHeader)
            tblCoat.Controls.Add(trHeader1)

            'Inner
            For i = 0 To dsData.Tables(0).Rows.Count - 1
                trInner = New TableRow
                Dim adj As New Double

                adj = dsData.Tables(0).Rows(i).Item("ADJUSTVAL") - 3.25
                For j = 1 To 5
                    tdInner = New TableCell
                    Select Case j
                        Case 1
                            InnerTdSetting(tdInner, "", "Center")
                            rdb = New RadioButton
                            rdb.GroupName = "rdbCoat"
                            rdb.ID = "rdbCoat" + (i + 1).ToString()

                            If hidCoat.Value <> "" Then
                                If hidCoat.Value <> rdb.ID.ToString() Then
                                    rdb.Checked = False
                                Else
                                    rdb.Checked = True
                                    hidSelCoat.Value = dsData.Tables(0).Rows(i).Item("ASSISTCOATID").ToString()
                                    CdBaseG = adj
                                End If
                            Else
                                If hidSelCoat.Value <> "" Then
                                    If hidSelCoat.Value = dsData.Tables(0).Rows(i).Item("ASSISTCOATID").ToString() Then
                                        rdb.Checked = True
                                        CdBaseG = adj
                                    Else
                                        rdb.Checked = False
                                    End If
                                Else
                                    If dsData.Tables(0).Rows(i).Item("ISDEFAULT").ToString() = "N" Then
                                        rdb.Checked = False
                                    Else
                                        If dsData.Tables(0).Rows(i).Item("ISDEFAULT").ToString() = "Y" And dsData.Tables(0).Rows(i).Item("MATID").ToString() = hidMatId.Value Then
                                            rdb.Checked = True
                                            hidSelCoat.Value = dsData.Tables(0).Rows(i).Item("ASSISTCOATID").ToString()
                                            CdBaseG = adj
                                        Else
                                            rdb.Checked = False
                                        End If
                                    End If
                                End If
                            End If

                            arrCdBase(i) = adj
                            arrCoatId(i) = dsData.Tables(0).Rows(i).Item("ASSISTCOATID").ToString()

                            AddHandler rdb.CheckedChanged, AddressOf rdb_CheckedChanged
                            rdb.AutoPostBack = True
                            tdInner.Controls.Add(rdb)
                            trInner.Controls.Add(tdInner)
                        Case 2
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            lbl.Text = dsData.Tables(0).Rows(i).Item("D1").ToString()
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 3
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            lbl.Text = dsData.Tables(0).Rows(i).Item("D2").ToString()
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 4
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            lbl.Text = dsData.Tables(0).Rows(i).Item("ADJUSTVAL").ToString()
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 5
                            InnerTdSetting(tdInner, "", "Center")
                            lbl = New Label
                            lbl.Text = FormatNumber(adj, 2)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                    End Select
                Next
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
                trInner.Height = 30
                tblCoat.Controls.Add(trInner)
            Next

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetCoatDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Public Sub GetOrderDetails()
        Dim objGetData As New Selectdata()
        Dim tdHeader As New TableCell
        Dim trHeader As New TableRow
        Dim tdHeader1 As New TableCell
        Dim trHeader1 As New TableRow
        Dim dsData As New DataSet
        Dim trInner As New TableRow
        Dim tdInner As New TableCell
        Dim lbl As New Label
        Dim rdb As New RadioButton
        Try

            dsData = objGetData.GetOrderAssist(ddlDate.SelectedValue)
            trHeader = New TableRow
            For i = 1 To 3
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "50px", "", "1")
                        Header2TdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 2
                        HeaderTdSetting(tdHeader, "100px", "Order Qty", "1")
                        Header2TdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 3
                        HeaderTdSetting(tdHeader, "100px", "", "1")
                        Header2TdSetting(tdHeader1, "0", "($/BaseBox)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                End Select
            Next
            trHeader.Height = 30
            tblOrder.Controls.Add(trHeader)
            tblOrder.Controls.Add(trHeader1)

            'Inner
            For i = 0 To dsData.Tables(0).Rows.Count - 1
                trInner = New TableRow
                For j = 1 To 3
                    tdInner = New TableCell
                    Select Case j
                        Case 1
                            InnerTdSetting(tdInner, "", "Center")
                            rdb = New RadioButton
                            rdb.GroupName = "rdbOrder"
                            rdb.ID = "rdbOrder" + (i + 1).ToString()

                            If hidOrder.Value <> "" Then
                                If hidOrder.Value <> rdb.ID.ToString() Then
                                    rdb.Checked = False
                                Else
                                    rdb.Checked = True
                                    hidSelOrder.Value = dsData.Tables(0).Rows(i).Item("ASSISTORDERQID").ToString()
                                    OdBaseG = dsData.Tables(0).Rows(i).Item("BASEBOXNO").ToString()
                                End If
                            Else
                                If hidSelOrder.Value <> "" Then
                                    If hidSelOrder.Value = dsData.Tables(0).Rows(i).Item("ASSISTORDERQID").ToString() Then
                                        rdb.Checked = True
                                        OdBaseG = dsData.Tables(0).Rows(i).Item("BASEBOXNO").ToString()
                                    Else
                                        rdb.Checked = False
                                    End If
                                Else
                                    If dsData.Tables(0).Rows(i).Item("ISDEFAULT").ToString() = "N" Then
                                        rdb.Checked = False
                                    Else
                                        rdb.Checked = True
                                        hidSelOrder.Value = dsData.Tables(0).Rows(i).Item("ASSISTORDERQID").ToString()
                                        OdBaseG = dsData.Tables(0).Rows(i).Item("BASEBOXNO").ToString()
                                    End If
                                End If

                            End If

                            arrOdBase(i) = dsData.Tables(0).Rows(i).Item("BASEBOXNO").ToString()
                            arrOrderId(i) = dsData.Tables(0).Rows(i).Item("ASSISTORDERQID").ToString()

                            AddHandler rdb.CheckedChanged, AddressOf rdb_CheckedChanged
                            rdb.AutoPostBack = True
                            tdInner.Controls.Add(rdb)
                            trInner.Controls.Add(tdInner)
                        Case 2
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            lbl.Text = dsData.Tables(0).Rows(i).Item("ORDERQ").ToString()
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 3
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            lbl.Text = dsData.Tables(0).Rows(i).Item("BASEBOXNO").ToString()
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                    End Select
                Next
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
                trInner.Height = 30
                tblOrder.Controls.Add(trInner)
            Next

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetOrderDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Public Sub GetCoilDetails()
        Dim objGetData As New Selectdata()
        Dim tdHeader As New TableCell
        Dim trHeader As New TableRow
        Dim tdHeader1 As New TableCell
        Dim trHeader1 As New TableRow
        Dim dsData As New DataSet
        Dim trInner As New TableRow
        Dim tdInner As New TableCell
        Dim lbl As New Label
        Dim rdb As New RadioButton
        Try

            dsData = objGetData.GetCoilWAssist(ddlDate.SelectedValue)
            trHeader = New TableRow
            For i = 1 To 3
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "50px", "", "1")
                        Header2TdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 2
                        HeaderTdSetting(tdHeader, "100px", "Coil Weight", "1")
                        Header2TdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 3
                        HeaderTdSetting(tdHeader, "100px", "", "1")
                        Header2TdSetting(tdHeader1, "0", "($/BaseBox)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                End Select
            Next
            trHeader.Height = 30
            tblCoil.Controls.Add(trHeader)
            tblCoil.Controls.Add(trHeader1)

            'Inner
            For i = 0 To dsData.Tables(0).Rows.Count - 1
                trInner = New TableRow
                For j = 1 To 3
                    tdInner = New TableCell
                    Select Case j
                        Case 1
                            InnerTdSetting(tdInner, "", "Center")
                            rdb = New RadioButton
                            rdb.GroupName = "rdbCoilWt"
                            rdb.ID = "rdbCoilWt" + (i + 1).ToString()

                            If hidCoilWt.Value <> "" Then
                                If hidCoilWt.Value <> rdb.ID.ToString() Then
                                    rdb.Checked = False
                                Else
                                    rdb.Checked = True
                                    hidSelCoilWt.Value = dsData.Tables(0).Rows(i).Item("ASSISTCOILWID").ToString()
                                    CWdBaseG = dsData.Tables(0).Rows(i).Item("BASEBOXNO").ToString()
                                End If
                            Else
                                If hidSelCoilWt.Value <> "" Then
                                    If hidSelCoilWt.Value = dsData.Tables(0).Rows(i).Item("ASSISTCOILWID").ToString() Then
                                        rdb.Checked = True
                                        CWdBaseG = dsData.Tables(0).Rows(i).Item("BASEBOXNO").ToString()
                                    Else
                                        rdb.Checked = False
                                    End If
                                Else
                                    If dsData.Tables(0).Rows(i).Item("ISDEFAULT").ToString() = "N" Then
                                        rdb.Checked = False
                                    Else
                                        rdb.Checked = True
                                        hidSelCoilWt.Value = dsData.Tables(0).Rows(i).Item("ASSISTCOILWID").ToString()
                                        CWdBaseG = dsData.Tables(0).Rows(i).Item("BASEBOXNO").ToString()
                                    End If
                                End If
                            End If

                            arrCWdBase(i) = dsData.Tables(0).Rows(i).Item("BASEBOXNO").ToString()
                            arrCoilWtId(i) = dsData.Tables(0).Rows(i).Item("ASSISTCOILWID").ToString()

                            AddHandler rdb.CheckedChanged, AddressOf rdb_CheckedChanged
                            rdb.AutoPostBack = True
                            tdInner.Controls.Add(rdb)
                            trInner.Controls.Add(tdInner)
                        Case 2
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            lbl.Text = dsData.Tables(0).Rows(i).Item("COILWT").ToString()
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 3
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            lbl.Text = dsData.Tables(0).Rows(i).Item("BASEBOXNO").ToString()
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                    End Select
                Next
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
                trInner.Height = 30
                tblCoil.Controls.Add(trInner)
            Next

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetCoilDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Public Sub GetQualityDetails()
        Dim objGetData As New Selectdata()
        Dim tdHeader As New TableCell
        Dim trHeader As New TableRow
        Dim tdHeader1 As New TableCell
        Dim trHeader1 As New TableRow
        Dim dsData As New DataSet
        Dim trInner As New TableRow
        Dim tdInner As New TableCell
        Dim lbl As New Label
        Dim rdb As New RadioButton
        Try

            dsData = objGetData.GetQualityAssist(ddlDate.SelectedValue)
            trHeader = New TableRow
            For i = 1 To 3
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "50px", "", "1")
                        Header2TdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 2
                        HeaderTdSetting(tdHeader, "100px", "Quality", "1")
                        Header2TdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 3
                        HeaderTdSetting(tdHeader, "100px", "", "1")
                        Header2TdSetting(tdHeader1, "0", "($/BaseBox)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                End Select
            Next
            trHeader.Height = 30
            tblQual.Controls.Add(trHeader)
            tblQual.Controls.Add(trHeader1)

            'Inner
            For i = 0 To dsData.Tables(0).Rows.Count - 1
                trInner = New TableRow
                For j = 1 To 3
                    tdInner = New TableCell
                    Select Case j
                        Case 1
                            InnerTdSetting(tdInner, "", "Center")
                            rdb = New RadioButton
                            rdb.GroupName = "rdbQual"
                            rdb.ID = "rdbQual" + (i + 1).ToString()

                            If hidQual.Value <> "" Then
                                If hidQual.Value <> rdb.ID.ToString() Then
                                    rdb.Checked = False
                                Else
                                    rdb.Checked = True
                                    hidSelQual.Value = dsData.Tables(0).Rows(i).Item("ASSISTQUALITYID").ToString()
                                    QdBaseG = dsData.Tables(0).Rows(i).Item("BASEBOXNO").ToString()
                                End If
                            Else
                                If hidSelQual.Value <> "" Then
                                    If hidSelQual.Value = dsData.Tables(0).Rows(i).Item("ASSISTQUALITYID").ToString() Then
                                        rdb.Checked = True
                                        QdBaseG = dsData.Tables(0).Rows(i).Item("BASEBOXNO").ToString()
                                    Else
                                        rdb.Checked = False
                                    End If
                                Else
                                    If dsData.Tables(0).Rows(i).Item("ISDEFAULT").ToString() = "N" Then
                                        rdb.Checked = False
                                    Else
                                        rdb.Checked = True
                                        hidSelQual.Value = dsData.Tables(0).Rows(i).Item("ASSISTQUALITYID").ToString()
                                        QdBaseG = dsData.Tables(0).Rows(i).Item("BASEBOXNO").ToString()
                                    End If
                                End If
                            End If

                            arrQdBase(i) = dsData.Tables(0).Rows(i).Item("BASEBOXNO").ToString()
                            arrQualId(i) = dsData.Tables(0).Rows(i).Item("ASSISTQUALITYID").ToString()

                            AddHandler rdb.CheckedChanged, AddressOf rdb_CheckedChanged
                            rdb.AutoPostBack = True
                            tdInner.Controls.Add(rdb)
                            trInner.Controls.Add(tdInner)
                        Case 2
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            lbl.Text = dsData.Tables(0).Rows(i).Item("QUALITY").ToString()
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 3
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            lbl.Text = dsData.Tables(0).Rows(i).Item("BASEBOXNO").ToString()
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                    End Select
                Next
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
                trInner.Height = 30
                tblQual.Controls.Add(trInner)
            Next

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetQualityDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Public Sub GetCoilWdDetails()
        Dim objGetData As New Selectdata()
        Dim tdHeader As New TableCell
        Dim trHeader As New TableRow
        Dim tdHeader1 As New TableCell
        Dim trHeader1 As New TableRow
        Dim dsData As New DataSet
        Dim trInner As New TableRow
        Dim tdInner As New TableCell
        Dim lbl As New Label
        Dim rdb As New RadioButton
        Try

            dsData = objGetData.GetCoilWdAssist(ddlDate.SelectedValue)
            trHeader = New TableRow
            For i = 1 To 3
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "50px", "", "1")
                        Header2TdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 2
                        HeaderTdSetting(tdHeader, "100px", "Coil Width", "1")
                        Header2TdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 3
                        HeaderTdSetting(tdHeader, "100px", "", "1")
                        Header2TdSetting(tdHeader1, "0", "($/BaseBox)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                End Select
            Next
            trHeader.Height = 30
            tblCoilWd.Controls.Add(trHeader)
            tblCoilWd.Controls.Add(trHeader1)

            'Inner
            For i = 0 To dsData.Tables(0).Rows.Count - 1
                trInner = New TableRow
                For j = 1 To 3
                    tdInner = New TableCell
                    Select Case j
                        Case 1
                            InnerTdSetting(tdInner, "", "Center")
                            rdb = New RadioButton
                            rdb.GroupName = "rdbCoilWd"
                            rdb.ID = "rdbCoilWd" + (i + 1).ToString()

                            If hidCoilWd.Value <> "" Then
                                If hidCoilWd.Value <> rdb.ID.ToString() Then
                                    rdb.Checked = False
                                Else
                                    rdb.Checked = True
                                    hidSelCoilWd.Value = dsData.Tables(0).Rows(i).Item("ASSISTCOILWID").ToString()
                                    CDdBaseG = dsData.Tables(0).Rows(i).Item("BASEBOXNO").ToString()
                                End If
                            Else
                                If hidSelCoilWd.Value <> "" Then
                                    If hidSelCoilWd.Value = dsData.Tables(0).Rows(i).Item("ASSISTCOILWID").ToString() Then
                                        rdb.Checked = True
                                        CDdBaseG = dsData.Tables(0).Rows(i).Item("BASEBOXNO").ToString()
                                    Else
                                        rdb.Checked = False
                                    End If
                                Else
                                    If dsData.Tables(0).Rows(i).Item("ISDEFAULT").ToString() = "N" Then
                                        rdb.Checked = False
                                    Else
                                        rdb.Checked = True
                                        hidSelCoilWd.Value = dsData.Tables(0).Rows(i).Item("ASSISTCOILWID").ToString()
                                        CDdBaseG = dsData.Tables(0).Rows(i).Item("BASEBOXNO").ToString()
                                    End If
                                End If
                            End If

                            arrCDdBase(i) = dsData.Tables(0).Rows(i).Item("BASEBOXNO").ToString()
                            arrCoilWdId(i) = dsData.Tables(0).Rows(i).Item("ASSISTCOILWID").ToString()

                            AddHandler rdb.CheckedChanged, AddressOf rdb_CheckedChanged
                            rdb.AutoPostBack = True
                            tdInner.Controls.Add(rdb)
                            trInner.Controls.Add(tdInner)
                        Case 2
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            lbl.Text = dsData.Tables(0).Rows(i).Item("COILWIDTH").ToString()
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 3
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            lbl.Text = dsData.Tables(0).Rows(i).Item("BASEBOXNO").ToString()
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                    End Select
                Next
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
                trInner.Height = 30
                tblCoilWd.Controls.Add(trInner)
            Next

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetCoilWdDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Public Sub GetGeneralDetails()
        Dim objGetData As New Selectdata()
        Dim tdHeader As New TableCell
        Dim trHeader As New TableRow
        Dim tdHeader1 As New TableCell
        Dim trHeader1 As New TableRow
        Dim dsData As New DataSet
        Dim trInner As New TableRow
        Dim tdInner As New TableCell
        Dim lbl As New Label
        Dim rdb As New RadioButton
        Try

            dsData = objGetData.GetGeneralAssist(ddlDate.SelectedValue)
            trHeader = New TableRow
            For i = 1 To 3
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "50px", "", "1")
                        Header2TdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 2
                        HeaderTdSetting(tdHeader, "100px", "General", "1")
                        Header2TdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 3
                        HeaderTdSetting(tdHeader, "100px", "", "1")
                        Header2TdSetting(tdHeader1, "0", "($/BaseBox)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                End Select
            Next
            trHeader.Height = 30
            tblGen.Controls.Add(trHeader)
            tblGen.Controls.Add(trHeader1)

            'Inner
            For i = 0 To dsData.Tables(0).Rows.Count - 1
                trInner = New TableRow
                For j = 1 To 3
                    tdInner = New TableCell
                    Select Case j
                        Case 1
                            InnerTdSetting(tdInner, "", "Center")
                            rdb = New RadioButton
                            rdb.GroupName = "rdbGeneral"
                            rdb.ID = "rdbGeneral" + (i + 1).ToString()

                            If hidGen.Value <> "" Then
                                If hidGen.Value <> rdb.ID.ToString() Then
                                    rdb.Checked = False
                                Else
                                    rdb.Checked = True
                                    hidSelGen.Value = dsData.Tables(0).Rows(i).Item("ASSISTGENERALID").ToString()
                                    GdBaseG = dsData.Tables(0).Rows(i).Item("BASEBOXNO").ToString()
                                End If
                            Else
                                If hidSelGen.Value <> "" Then
                                    If hidSelGen.Value = dsData.Tables(0).Rows(i).Item("ASSISTGENERALID").ToString() Then
                                        rdb.Checked = True
                                        GdBaseG = dsData.Tables(0).Rows(i).Item("BASEBOXNO").ToString()
                                    Else
                                        rdb.Checked = False
                                    End If
                                Else
                                    If dsData.Tables(0).Rows(i).Item("ISDEFAULT").ToString() = "N" Then
                                        rdb.Checked = False
                                    Else
                                        rdb.Checked = True
                                        hidSelGen.Value = dsData.Tables(0).Rows(i).Item("ASSISTGENERALID").ToString()
                                        GdBaseG = dsData.Tables(0).Rows(i).Item("BASEBOXNO").ToString()
                                    End If
                                End If
                            End If

                            arrGdBase(i) = dsData.Tables(0).Rows(i).Item("BASEBOXNO").ToString()
                            arrGenId(i) = dsData.Tables(0).Rows(i).Item("ASSISTGENERALID").ToString()

                            AddHandler rdb.CheckedChanged, AddressOf rdb_CheckedChanged
                            rdb.AutoPostBack = True
                            tdInner.Controls.Add(rdb)
                            trInner.Controls.Add(tdInner)
                        Case 2
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            lbl.Text = dsData.Tables(0).Rows(i).Item("GENERAL").ToString()
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 3
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            lbl.Text = dsData.Tables(0).Rows(i).Item("BASEBOXNO").ToString()
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                    End Select
                Next
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
                trInner.Height = 30
                tblGen.Controls.Add(trInner)
            Next

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetGeneralDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Public Sub GetTrimTolDetails()
        Dim objGetData As New Selectdata()
        Dim tdHeader As New TableCell
        Dim trHeader As New TableRow
        Dim tdHeader1 As New TableCell
        Dim trHeader1 As New TableRow
        Dim dsData As New DataSet
        Dim trInner As New TableRow
        Dim tdInner As New TableCell
        Dim lbl As New Label
        Dim rdb As New RadioButton
        Try

            dsData = objGetData.GetTrimTolAssist(ddlDate.SelectedValue)
            trHeader = New TableRow
            For i = 1 To 3
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "50px", "", "1")
                        Header2TdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 2
                        HeaderTdSetting(tdHeader, "100px", "Trim Tolerance", "1")
                        Header2TdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 3
                        HeaderTdSetting(tdHeader, "100px", "", "1")
                        Header2TdSetting(tdHeader1, "0", "($/BaseBox)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                End Select
            Next
            trHeader.Height = 30
            tblTrimTol.Controls.Add(trHeader)
            tblTrimTol.Controls.Add(trHeader1)

            'Inner
            For i = 0 To dsData.Tables(0).Rows.Count - 1
                trInner = New TableRow
                For j = 1 To 3
                    tdInner = New TableCell
                    Select Case j
                        Case 1
                            InnerTdSetting(tdInner, "", "Center")
                            rdb = New RadioButton
                            rdb.GroupName = "rdbTrimTol"
                            rdb.ID = "rdbTrimTol" + (i + 1).ToString()

                            If hidTrim.Value <> "" Then
                                If hidTrim.Value <> rdb.ID.ToString() Then
                                    rdb.Checked = False
                                Else
                                    rdb.Checked = True
                                    hidSelTrim.Value = dsData.Tables(0).Rows(i).Item("ASSISTTRIMTID").ToString()
                                    TdBaseG = dsData.Tables(0).Rows(i).Item("BASEBOXNO").ToString()
                                End If
                            Else
                                If hidSelTrim.Value <> "" Then
                                    If hidSelTrim.Value = dsData.Tables(0).Rows(i).Item("ASSISTTRIMTID").ToString() Then
                                        rdb.Checked = True
                                        TdBaseG = dsData.Tables(0).Rows(i).Item("BASEBOXNO").ToString()
                                    Else
                                        rdb.Checked = False
                                    End If
                                Else
                                    If dsData.Tables(0).Rows(i).Item("ISDEFAULT").ToString() = "N" Then
                                        rdb.Checked = False
                                    Else
                                        rdb.Checked = True
                                        hidSelTrim.Value = dsData.Tables(0).Rows(i).Item("ASSISTTRIMTID").ToString()
                                        TdBaseG = dsData.Tables(0).Rows(i).Item("BASEBOXNO").ToString()
                                    End If
                                End If
                            End If

                            arrTdBase(i) = dsData.Tables(0).Rows(i).Item("BASEBOXNO").ToString()
                            arrTrimId(i) = dsData.Tables(0).Rows(i).Item("ASSISTTRIMTID").ToString()

                            AddHandler rdb.CheckedChanged, AddressOf rdb_CheckedChanged
                            rdb.AutoPostBack = True
                            tdInner.Controls.Add(rdb)
                            trInner.Controls.Add(tdInner)
                        Case 2
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            lbl.Text = dsData.Tables(0).Rows(i).Item("TRIMTOL").ToString()
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 3
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            lbl.Text = dsData.Tables(0).Rows(i).Item("BASEBOXNO").ToString()
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                    End Select
                Next
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
                trInner.Height = 30
                tblTrimTol.Controls.Add(trInner)
            Next

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetTrimTolDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Public Sub GetPackDetails()
        Dim objGetData As New Selectdata()
        Dim tdHeader As New TableCell
        Dim trHeader As New TableRow
        Dim tdHeader1 As New TableCell
        Dim trHeader1 As New TableRow
        Dim dsData As New DataSet
        Dim trInner As New TableRow
        Dim tdInner As New TableCell
        Dim lbl As New Label
        Dim rdb As New RadioButton
        Try

            dsData = objGetData.GetPackAssist(ddlDate.SelectedValue)
            trHeader = New TableRow
            For i = 1 To 3
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "50px", "", "1")
                        Header2TdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 2
                        HeaderTdSetting(tdHeader, "100px", "Packaging", "1")
                        Header2TdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 3
                        HeaderTdSetting(tdHeader, "100px", "", "1")
                        Header2TdSetting(tdHeader1, "0", "($/Coil)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                End Select
            Next
            trHeader.Height = 30
            tblPack.Controls.Add(trHeader)
            tblPack.Controls.Add(trHeader1)

            'Inner
            For i = 0 To dsData.Tables(0).Rows.Count - 1
                trInner = New TableRow
                For j = 1 To 3
                    tdInner = New TableCell
                    Select Case j
                        Case 1
                            InnerTdSetting(tdInner, "", "Center")
                            rdb = New RadioButton
                            rdb.GroupName = "rdbPack"
                            rdb.ID = "rdbPack" + (i + 1).ToString()

                            If hidPack.Value <> "" Then
                                If hidPack.Value <> rdb.ID.ToString() Then
                                    rdb.Checked = False
                                Else
                                    rdb.Checked = True
                                    hidSelPack.Value = dsData.Tables(0).Rows(i).Item("ASSISTPACKID").ToString()
                                    CRdBaseG = dsData.Tables(0).Rows(i).Item("DOLPERCOIL").ToString()
                                End If
                            Else
                                If hidSelPack.Value <> "" Then
                                    If hidSelPack.Value = dsData.Tables(0).Rows(i).Item("ASSISTPACKID").ToString() Then
                                        rdb.Checked = True
                                        CRdBaseG = dsData.Tables(0).Rows(i).Item("DOLPERCOIL").ToString()
                                    Else
                                        rdb.Checked = False
                                    End If
                                Else
                                    If dsData.Tables(0).Rows(i).Item("ISDEFAULT").ToString() = "N" Then
                                        rdb.Checked = False
                                    Else
                                        rdb.Checked = True
                                        hidSelPack.Value = dsData.Tables(0).Rows(i).Item("ASSISTPACKID").ToString()
                                        CRdBaseG = dsData.Tables(0).Rows(i).Item("DOLPERCOIL").ToString()
                                    End If
                                End If
                            End If

                            arrCRdBase(i) = dsData.Tables(0).Rows(i).Item("DOLPERCOIL").ToString()
                            arrCoresId(i) = dsData.Tables(0).Rows(i).Item("ASSISTPACKID").ToString()

                            AddHandler rdb.CheckedChanged, AddressOf rdb_CheckedChanged
                            rdb.AutoPostBack = True
                            tdInner.Controls.Add(rdb)
                            trInner.Controls.Add(tdInner)
                        Case 2
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            lbl.Text = dsData.Tables(0).Rows(i).Item("PACKDES").ToString()
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 3
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            lbl.Text = dsData.Tables(0).Rows(i).Item("DOLPERCOIL").ToString()
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                    End Select
                Next
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
                trInner.Height = 30
                tblPack.Controls.Add(trInner)
            Next

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPackDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Public Sub GetPackagingDetails()
        Dim objGetData As New Selectdata()
        Dim tdHeader As New TableCell
        Dim trHeader As New TableRow
        Dim tdHeader1 As New TableCell
        Dim trHeader1 As New TableRow
        Dim dsData As New DataSet
        Dim trInner As New TableRow
        Dim tdInner As New TableCell
        Dim lbl As New Label
        Dim rdb As New RadioButton
        Try

            dsData = objGetData.GetPackagingAssist(ddlDate.SelectedValue)
            trHeader = New TableRow
            For i = 1 To 3
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "50px", "", "1")
                        Header2TdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 2
                        HeaderTdSetting(tdHeader, "100px", "Packaging", "1")
                        Header2TdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 3
                        HeaderTdSetting(tdHeader, "100px", "", "1")
                        Header2TdSetting(tdHeader1, "0", "($/BaseBox)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                End Select
            Next
            trHeader.Height = 30
            tblPackaging.Controls.Add(trHeader)
            tblPackaging.Controls.Add(trHeader1)

            'Inner
            For i = 0 To dsData.Tables(0).Rows.Count - 1
                trInner = New TableRow
                For j = 1 To 3
                    tdInner = New TableCell
                    Select Case j
                        Case 1
                            InnerTdSetting(tdInner, "", "Center")
                            rdb = New RadioButton
                            rdb.GroupName = "rdbPackaging"
                            rdb.ID = "rdbPakg" + (i + 1).ToString()

                            If hidPackg.Value <> "" Then
                                If hidPackg.Value <> rdb.ID.ToString() Then
                                    rdb.Checked = False
                                Else
                                    rdb.Checked = True
                                    hidSelPackg.Value = dsData.Tables(0).Rows(i).Item("ASSISTPACKAGINGID").ToString()
                                    CIdBaseG = dsData.Tables(0).Rows(i).Item("DOLPERBBOX").ToString()
                                End If
                            Else
                                If hidSelPackg.Value <> "" Then
                                    If hidSelPackg.Value = dsData.Tables(0).Rows(i).Item("ASSISTPACKAGINGID").ToString() Then
                                        rdb.Checked = True
                                        CIdBaseG = dsData.Tables(0).Rows(i).Item("DOLPERBBOX").ToString()
                                    Else
                                        rdb.Checked = False
                                    End If
                                Else
                                    If dsData.Tables(0).Rows(i).Item("ISDEFAULT").ToString() = "N" Then
                                        rdb.Checked = False
                                    Else
                                        rdb.Checked = True
                                        hidSelPackg.Value = dsData.Tables(0).Rows(i).Item("ASSISTPACKAGINGID").ToString()
                                        CIdBaseG = dsData.Tables(0).Rows(i).Item("DOLPERBBOX").ToString()
                                    End If
                                End If
                            End If

                            arrCIdBase(i) = dsData.Tables(0).Rows(i).Item("DOLPERBBOX").ToString()
                            arrCoilId(i) = dsData.Tables(0).Rows(i).Item("ASSISTPACKAGINGID").ToString()

                            AddHandler rdb.CheckedChanged, AddressOf rdb_CheckedChanged
                            rdb.AutoPostBack = True
                            tdInner.Controls.Add(rdb)
                            trInner.Controls.Add(tdInner)
                        Case 2
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            lbl.Text = dsData.Tables(0).Rows(i).Item("PACKDES").ToString()
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 3
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            lbl.Text = dsData.Tables(0).Rows(i).Item("DOLPERBBOX").ToString()
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                    End Select
                Next
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
                trInner.Height = 30
                tblPackaging.Controls.Add(trInner)
            Next

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPackagingDetails:" + ex.Message.ToString()
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

    Protected Sub rdb_CheckedChanged(sender As Object, e As System.EventArgs)
        Dim rdb As New RadioButton
        Dim objUpIns As New E1UpInsData.UpdateInsert()
        rdb = DirectCast(sender, RadioButton)
        Dim CompId As String = ""
        Dim Value As String = ""
        Dim str As Integer = Integer.Parse(Regex.Replace(rdb.ID, "[^\d]", ""))
        If rdb.Checked = True Then
            If rdb.ID.Contains("Box") Then
                dBaseMG = arrdBaseM(str - 1)
                dBaseG = arrdBase(str - 1)
                dMetG = arrdMet(str - 1)
                dPondG = arrdPond(str - 1)
                hidBox.Value = rdb.ID.ToString()
                Value = arrThickId(str - 1)
                CompId = "1"
            ElseIf rdb.ID.Contains("Coat") Then
                CdBaseG = arrCdBase(str - 1)
                hidCoat.Value = rdb.ID.ToString()
                Value = arrCoatId(str - 1)
                CompId = "2"
            ElseIf rdb.ID.Contains("Order") Then
                OdBaseG = arrOdBase(str - 1)
                hidOrder.Value = rdb.ID.ToString()
                Value = arrOrderId(str - 1)
                CompId = "3"
            ElseIf rdb.ID.Contains("CoilWt") Then
                CWdBaseG = arrCWdBase(str - 1)
                hidCoilWt.Value = rdb.ID.ToString()
                Value = arrCoilWtId(str - 1)
                CompId = "4"
            ElseIf rdb.ID.Contains("Qual") Then
                QdBaseG = arrQdBase(str - 1)
                hidQual.Value = rdb.ID.ToString()
                Value = arrQualId(str - 1)
                CompId = "5"
            ElseIf rdb.ID.Contains("CoilWd") Then
                CDdBaseG = arrCDdBase(str - 1)
                hidCoilWd.Value = rdb.ID.ToString()
                Value = arrCoilWdId(str - 1)
                CompId = "6"
            ElseIf rdb.ID.Contains("General") Then
                GdBaseG = arrGdBase(str - 1)
                hidGen.Value = rdb.ID.ToString()
                Value = arrGenId(str - 1)
                CompId = "7"
            ElseIf rdb.ID.Contains("TrimTol") Then
                TdBaseG = arrTdBase(str - 1)
                hidTrim.Value = rdb.ID.ToString()
                Value = arrTrimId(str - 1)
                CompId = "8"
            ElseIf rdb.ID.Contains("Pack") Then
                CRdBaseG = arrCRdBase(str - 1)
                hidPack.Value = rdb.ID.ToString()
                Value = arrCoresId(str - 1)
                CompId = "9"
            ElseIf rdb.ID.Contains("Pakg") Then
                CIdBaseG = arrCIdBase(str - 1)
                hidPackg.Value = rdb.ID.ToString()
                Value = arrCoilId(str - 1)
                CompId = "10"
            End If
        End If
        objUpIns.UpInsSteelCaseDetails(Session("E1CaseId"), hidMatId.Value, ddlDate.SelectedValue, "1", CompId, Value)
        GetHeadDetails()
        'GetPageDetails()
    End Sub

    Protected Sub ddlDate_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlDate.SelectedIndexChanged
        Try
            GetDefaultValue()
        Catch ex As Exception

        End Try
    End Sub

End Class
