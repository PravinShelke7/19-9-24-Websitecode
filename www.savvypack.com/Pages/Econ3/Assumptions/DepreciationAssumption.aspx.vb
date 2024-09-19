﻿Imports System.Data
Imports System.Data.OleDb
Imports System
Imports E3GetData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Econ3_Assumptions_DepreciationAssumption
    Inherits System.Web.UI.Page
    Public investCost(10) As Double
    Public investCostSE(10) As Double
    Public DepCostPE(10) As Double
    Public DepCostSE(10) As Double

    Shared uName As String
#Region "Get Set Variables"
    Dim _lErrorLble As Label
    Dim _strUserName As String
    Dim _strPassword As String
    Dim _iAssumptionId As Integer
    Dim _btnUpdate As ImageButton


    Public Property ErrorLable() As Label
        Get
            Return _lErrorLble
        End Get
        Set(ByVal Value As Label)
            _lErrorLble = Value
        End Set
    End Property

    Public Property UserName() As String
        Get
            Return _strUserName
        End Get
        Set(ByVal Value As String)
            _strUserName = Value
        End Set
    End Property

    Public Property Password() As String
        Get
            Return _strPassword
        End Get
        Set(ByVal Value As String)
            _strPassword = Value
        End Set
    End Property

    Public Property AssumptionId() As Integer
        Get
            Return _iAssumptionId
        End Get
        Set(ByVal Value As Integer)
            _iAssumptionId = Value
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


    Public DataCnt As Integer
    Public CaseDesp As New ArrayList

#End Region

#Region "MastePage Content Variables"
    Protected Sub GetErrorLable()
        ErrorLable = Page.Master.FindControl("lblError")
    End Sub

    Protected Sub GetUpdatebtn()
        Updatebtn = Page.Master.FindControl("imgUpdate")
        Updatebtn.Visible = True
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            GetErrorLable()
            GetUpdatebtn()
            GetSessionDetails()
            GetPageDetails()
            If Not IsPostBack Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Layers", "removeSession();", True)
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub


    Protected Sub GetSessionDetails()
        Try
            UserName = Session("UserName")
            Password = Session("Password")
            AssumptionId = Session("AssumptionID")
            uName = Session("UserName")

            lblAID.Text = AssumptionId
            lblAdes.Text = Session("Description")

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetSessionDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Function GetCaseIds() As String()
        Dim CaseIds(0) As String
        Dim objGetData As New E3GetData.Selectdata
        Try
            CaseIds = objGetData.Cases(AssumptionId)
            Return CaseIds
        Catch ex As Exception
            Return CaseIds
            _lErrorLble.Text = "Error:GetCaseIds:" + ex.Message.ToString()
        End Try
    End Function

    Protected Sub GetPageDetails()
        Dim dsPE As New DataSet
        Dim dsSE As New DataSet
        Dim dstblPE As New DataSet
        Dim dstblSE As New DataSet
        Dim dsCaseDetails As New DataSet
        Dim objGetData As New E1GetData.Selectdata
        Dim CaseIds As String = String.Empty
        'Dim DataCnt As New Integer
        Dim i As New Integer
        Dim j As New Integer
        Dim k As New Integer
        Dim DWidth As String = String.Empty
        Dim arrCaseID() As String
        Dim CountArrPE(30) As String
        Dim CountArrSE(30) As String
        Dim InvestCTotal As Decimal = 0
        Dim InvestCTotalSE As Decimal = 0
        Dim depTotal As Decimal = 0
        Dim depTotalSE As Decimal = 0
        Dim countS As Integer = 0
        Dim countP As Integer = 0
        Dim maxPE As Integer = 0
        Dim maxSE As Integer = 0
        Dim btn As Button
        Dim CasesVal(9) As String

        Try
            arrCaseID = GetCaseIds()
            DataCnt = arrCaseID.Length - 1

            For i = 0 To 9
                If i <= DataCnt Then
                    CasesVal(i) = arrCaseID(i)
                Else
                    CasesVal(i) = Nothing
                End If
            Next

            Updatebtn.Attributes.Add("onClick", "return UpdateAll('" + CasesVal(0).ToString() + "','" + CasesVal(1).ToString() + "','" + CasesVal(2) + "','" + CasesVal(3) + "','" + CasesVal(4) + "','" + CasesVal(5) + "','" + CasesVal(6) + "','" + CasesVal(7) + "','" + CasesVal(8) + "','" + CasesVal(9) + "','" + (DataCnt + 1).ToString() + "');")


            Dim trHeader As New TableRow
            Dim tdHeader As TableCell
            Dim tdInner As TableCell
            Dim trInner As New TableRow
            Dim ddl As DropDownList
            Dim lbl As New Label
            Dim txt As TextBox
            Dim Cunits As New Integer
            Dim Units As New Integer
            Dim CurrTitle As String = String.Empty
            Dim CCurrTitle As String = String.Empty
            Dim UnitError As String = String.Empty
            Dim CurrError As String = String.Empty
            Dim Title As String = String.Empty

            Dim objGetDataW As New E3GetData.Selectdata
            Dim dsW As New DataSet


            dsW = objGetDataW.CompCOLWIDTH(AssumptionId)
            If dsW.Tables(0).Rows.Count > 0 Then
                txtDWidth.Text = dsW.Tables(0).Rows(0).Item("COLWIDTH").ToString()
            End If
            DWidth = txtDWidth.Text + "px"

            For i = 0 To DataCnt
                dsPE = objGetData.GetAssetC(arrCaseID(i))
                dsSE = objGetData.GetAssetS(arrCaseID(i))

                dsPE.Tables(0).TableName = arrCaseID(i).ToString()
                dsSE.Tables(0).TableName = arrCaseID(i).ToString()

                dstblPE.Tables.Add(dsPE.Tables(arrCaseID(i).ToString()).Copy())
                dstblSE.Tables.Add(dsSE.Tables(arrCaseID(i).ToString()).Copy())
            Next

            'Getting Max Asset For Process Equip
            For k = 0 To DataCnt
                For i = 1 To 30
                    If dstblPE.Tables(k).Rows(0).Item("EQUIPDES" + i.ToString()).ToString() <> "Nothing Selected" Then
                        countP = i
                    End If
                Next
                CountArrPE(k) = countP
            Next
            maxPE = CountArrPE(0)
            For j = 0 To DataCnt
                If CountArrPE(j) > maxPE Then
                    maxPE = CountArrPE(j)
                End If
            Next
            hidmaxPE.Value = maxPE
			
			    Dim marginTop As String = ""
            If maxPE = 0 Then
                marginTop = (230).ToString() + "px"
            Else
                marginTop = (maxPE.ToString() * 200 + 150).ToString() + "px"
            End If
            'loadingSup.Style.Add("margin-top", marginTop)
            'end          


            tdHeader = New TableCell
            HeaderTdSetting(tdHeader, "200px", "Process Equipment", 1)
            trHeader.Controls.Add(tdHeader)
            trHeader.Height = 20
            trHeader.CssClass = "PageSSHeading"

            For i = 0 To DataCnt
                dsCaseDetails = objGetData.GetCaseDetails(arrCaseID(i).ToString())
                Cunits = Convert.ToInt32(dstblPE.Tables(0).Rows(0).Item("Units").ToString())
                Units = Convert.ToInt32(dstblPE.Tables(i).Rows(0).Item("Units").ToString())
                CCurrTitle = dstblPE.Tables(0).Rows(0).Item("ASSESTCOSTUNIT").ToString().Trim()
                CurrTitle = dstblPE.Tables(i).Rows(0).Item("ASSESTCOSTUNIT").ToString().Trim()

                tdHeader = New TableCell
                Dim Headertext As String = String.Empty
                'If Cunits <> Units Then
                '    UnitError = "<br/> <span  style='color:red'>Unit Mismatch</span>"
                'Else
                '    UnitError = ""
                'End If
                UnitError = ""

                If CCurrTitle <> CurrTitle Then
                    CurrError = "<br/> <span  style='color:red'>Currency Mismatch</span>"
                Else
                    CurrError = ""
                End If

                Headertext = "Case#:" + arrCaseID(i).ToString() + " " + dsCaseDetails.Tables(0).Rows(0).Item("COUNTRYDES").ToString() + "<br/>" + dsCaseDetails.Tables(0).Rows(0).Item("CaseDes").ToString() + UnitError + CurrError + "<input type='hidden' value='" + arrCaseID(i).ToString() + "' name='Case" + i.ToString() + "'/>"

                CaseDesp.Add(arrCaseID(i).ToString())
                HeaderTdSetting(tdHeader, DWidth, Headertext, 1)
                trHeader.Controls.Add(tdHeader)
            Next
            tblComparision.Controls.Add(trHeader)

            'SUD STARTED
            trHeader = New TableRow
            tdHeader = New TableCell
            tdHeader = New TableCell
            HeaderTdSetting(tdHeader, "200px", "<img alt='' src='../../Images/spacer.gif' Style='width:200px;height:0px;'  />", 1)
            trHeader.Controls.Add(tdHeader)
            trHeader.Height = 20
            trHeader.CssClass = "PageSSHeading"


            For i = 0 To DataCnt
                tdHeader = New TableCell
                Dim Headertext As String = String.Empty
                Headertext = "Case#:" + arrCaseID(i).ToString() + "<br/>"
                CaseDesp.Add(arrCaseID(i).ToString())


                btn = New Button
                btn.ID = "btn_" + (i + 1).ToString()
                btn.Text = "Update"
                btn.Height = 15
                btn.Style.Add("font-size", "9px")
                btn.CommandArgument = arrCaseID(i).ToString()
                If arrCaseID(i) <= 1000 Then
                    btn.Enabled = False
                    btn.Style.Add("background-color", "#a6a6a6")
                    btn.Style.Add("color", "#4d4d4d")
                End If
                'AddHandler btn.Click, AddressOf Update_Click
                btn.Attributes.Add("onClick", "return update('" + arrCaseID(i).ToString() + "','" + (i + 1).ToString() + "');")


                HeaderTdSetting(tdHeader, DWidth, Headertext, 1)
                ' tdHeader.Controls.Add(hid)
                tdHeader.Controls.Add(btn)
                trHeader.Controls.Add(tdHeader)

            Next
            tblComparision.Controls.Add(trHeader)
            'SUD ENDED


            For i = 1 To maxPE
                For j = 1 To 8
                    trInner = New TableRow()

                    Select Case j
                        Case 1
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "<b>Asset " + i.ToString() + "</b>", trInner, "AlterNateColor4")
                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Center")
                                tdInner.Text = "&nbsp;"
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 2
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Asset Description", trInner, "AlterNateColor1")
                            trInner.ID = "ADPE_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                If dstblPE.Tables(k).Rows(0).Item("LEQUIPDES" + i.ToString() + "").ToString() <> "" Then
                                    lbl.Text = dstblPE.Tables(k).Rows(0).Item("LEQUIPDES" + i.ToString() + "").ToString()
                                Else
                                    lbl.Text = dstblPE.Tables(k).Rows(0).Item("EQUIPDES" + i.ToString() + "").ToString()
                                End If
                                'lbl.Text = dstblPE.Tables(k).Rows(0).Item("EQUIPDES" + i.ToString() + "").ToString()
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 3
                            tdInner = New TableCell
                            Title = "(" + dstblPE.Tables(0).Rows(0).Item("ASSESTCOSTUNIT").ToString() + ")"
                            LeftTdSetting(tdInner, "Asset Cost Sugg. " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "ACSPE_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                lbl.Text = FormatNumber(dstblPE.Tables(k).Rows(0).Item("ASSETS" + i.ToString() + "").ToString(), 0).ToString()
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 4
                            tdInner = New TableCell
                            Title = "(" + dstblPE.Tables(0).Rows(0).Item("ASSESTCOSTUNIT").ToString() + ")"
                            LeftTdSetting(tdInner, "Asset Cost Pref. " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "ACPPE_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                lbl.Text = FormatNumber(dstblPE.Tables(k).Rows(0).Item("ASSETP" + i.ToString() + "").ToString(), 0).ToString()
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 5
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Number of Assets", trInner, "AlterNateColor2")
                            trInner.ID = "NOAPE_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                If (dstblPE.Tables(k).Rows(0).Item("NUM" + i.ToString() + "").ToString() <> "") Then
                                    lbl.Text = FormatNumber(dstblPE.Tables(k).Rows(0).Item("NUM" + i.ToString() + "").ToString(), 0)
                                End If
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 6
                            tdInner = New TableCell
                            Title = "(" + dstblPE.Tables(0).Rows(0).Item("ASSESTCOSTUNIT").ToString() + ")"
                            LeftTdSetting(tdInner, "Investment Cost " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "ICPE_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                If (dstblPE.Tables(k).Rows(0).Item("ICPE" + i.ToString() + "").ToString() <> "") Then
                                    lbl.Text = FormatNumber(dstblPE.Tables(k).Rows(0).Item("ICPE" + i.ToString() + "").ToString(), 0)
                                End If
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 7
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Depreciation Period (years)", trInner, "AlterNateColor2")
                            trInner.ID = "YTDPE_" + i.ToString()

                            For k = 0 To DataCnt
                                'lbl = New Label
                                'lbl.Style.Add("Width", DWidth)
                                txt = New TextBox
								 txt.MaxLength = 6
                                txt.ID = "DEPRE" + i.ToString() + "_" + (k + 1).ToString()
                                txt.CssClass = "PrefTextBox"
                                txt.Attributes.Add("OnkeyPress", "return clickButton(event)")
                                If arrCaseID(k) <= 1000 Then
                                    txt.Style.Add("background-color", "#a6a6a6")
                                    txt.Enabled = False
                                End If
                                If arrCaseID(k) <= 1000 Then
                                    txt.Style.Add("background-color", "#a6a6a6")
                                    txt.Enabled = False
                                End If

                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                'lbl.Text = FormatNumber(dstblPE.Tables(k).Rows(0).Item("DEPRE" + i.ToString() + "").ToString(), 0).ToString()
                                txt.Text = FormatNumber(dstblPE.Tables(k).Rows(0).Item("DEPRE" + i.ToString() + "").ToString(), 0).ToString()
                                tdInner.Controls.Add(txt)
                                ' tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 8
                            tdInner = New TableCell
                            Title = "(" + dstblPE.Tables(0).Rows(0).Item("ASSESTCOSTUNIT").ToString() + ")"
                            LeftTdSetting(tdInner, "Depreciation Cost " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "DCPE_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.ID = "DEPRECOST" + i.ToString() + "_" + (k + 1).ToString()
                                lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")

                                If (dstblPE.Tables(k).Rows(0).Item("DCPE" + i.ToString() + "").ToString() <> "") Then
                                    lbl.Text = FormatNumber(dstblPE.Tables(k).Rows(0).Item("DCPE" + i.ToString() + "").ToString(), 0)
                                End If
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next
                    End Select
                    If j <> 1 Then
                        If (j Mod 2 = 0) Then
                            trInner.CssClass = "AlterNateColor1"
                        Else
                            trInner.CssClass = "AlterNateColor2"
                        End If
                    End If
                    tblComparision.Controls.Add(trInner)
                Next
            Next

            'Total For Process Equip
            For j = 1 To 3
                trInner = New TableRow()
                Select Case j
                    Case 1
                        tdInner = New TableCell
                        LeftTdSetting(tdInner, "<b>Total</b>", trInner, "AlterNateColor4")
                        For k = 0 To DataCnt
                            tdInner = New TableCell
                            InnerTdSetting(tdInner, "", "Center")
                            tdInner.Text = "&nbsp;"
                            trInner.Controls.Add(tdInner)
                        Next
                    Case 2
                        tdInner = New TableCell
                        Title = "(" + dstblPE.Tables(0).Rows(0).Item("ASSESTCOSTUNIT").ToString() + ")"
                        LeftTdSetting(tdInner, "Investment Cost " + Title + "", trInner, "AlterNateColor1")
                        trInner.ID = "ITPE_1"

                        For k = 0 To DataCnt
                            lbl = New Label
                            lbl.Style.Add("Width", DWidth)
                            tdInner = New TableCell
                            InnerTdSetting(tdInner, "", "Right")
                            lbl.Text = FormatNumber(dstblPE.Tables(k).Rows(0).Item("INVESTTOTALPE").ToString(), 0)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Next

                    Case 3
                        tdInner = New TableCell
                        Title = "(" + dstblPE.Tables(0).Rows(0).Item("ASSESTCOSTUNIT").ToString() + ")"
                        LeftTdSetting(tdInner, "Depreciation Cost " + Title + "", trInner, "AlterNateColor1")
                        trInner.ID = "DTPE_1"

                        For k = 0 To DataCnt
                            lbl = New Label
                            lbl.Style.Add("Width", DWidth)
                            lbl.ID = "PETOT" + (k + 1).ToString()
                            tdInner = New TableCell
                            InnerTdSetting(tdInner, "", "Right")
                            lbl.Text = FormatNumber(dstblPE.Tables(k).Rows(0).Item("DEPRETOTALPE").ToString(), 0)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Next
                End Select
                If j <> 1 Then
                    If (j Mod 2 = 0) Then
                        trInner.CssClass = "AlterNateColor1"
                    Else
                        trInner.CssClass = "AlterNateColor2"
                    End If
                End If
                tblComparision.Controls.Add(trInner)
            Next

             'Space Change Start
            For j = 1 To 1
                trInner = New TableRow()
                Select Case j
                    Case 1
                        tdInner = New TableCell
                        LeftTdSetting(tdInner, "", trInner, "PageSection1")

                        For k = 0 To DataCnt
                            lbl = New Label
                            lbl.Style.Add("Width", DWidth)
                            tdInner = New TableCell
                            InnerTdSetting(tdInner, "", "Right")
                            lbl.Text = ""
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                            trInner.Height = "14"
                        Next

                End Select
                trInner.CssClass = "PageSection1"
                tblComparision.Controls.Add(trInner)
            Next
            'Space Change end

            'Getting Max Asset For Suport Equip
            For k = 0 To DataCnt
                For i = 1 To 30
                    If dstblSE.Tables(k).Rows(0).Item("EQUIPDES" + i.ToString()).ToString() <> "Nothing Selected" Then
                        countS = i
                    End If
                Next
                CountArrSE(k) = countS
            Next
            maxSE = CountArrSE(0)
            For j = 0 To DataCnt
                If CountArrSE(j) > maxSE Then
                    maxSE = CountArrSE(j)
                End If
            Next
            hidmaxSE.Value = maxSE
            'end

            trHeader = New TableRow
            tdHeader = New TableCell
            HeaderTdSetting(tdHeader, "200px", "Support Equipment", 1)
            trHeader.Controls.Add(tdHeader)
            trHeader.Height = 20
            trHeader.CssClass = "PageSSHeading"

            For i = 0 To DataCnt
                dsCaseDetails = objGetData.GetCaseDetails(arrCaseID(i).ToString())
                Cunits = Convert.ToInt32(dstblPE.Tables(0).Rows(0).Item("Units").ToString())
                Units = Convert.ToInt32(dstblPE.Tables(i).Rows(0).Item("Units").ToString())
                CCurrTitle = dstblPE.Tables(0).Rows(0).Item("ASSESTCOSTUNIT").ToString().Trim()
                CurrTitle = dstblPE.Tables(i).Rows(0).Item("ASSESTCOSTUNIT").ToString().Trim()

                tdHeader = New TableCell
                Dim Headertext As String = String.Empty
                'If Cunits <> Units Then
                '    UnitError = "<br/> <span  style='color:red'>Unit Mismatch</span>"
                'Else
                '    UnitError = ""
                'End If
                UnitError = ""

                If CCurrTitle <> CurrTitle Then
                    CurrError = "<br/> <span  style='color:red'>Currency Mismatch</span>"
                Else
                    CurrError = ""
                End If

                Headertext = "Case#:" + arrCaseID(i).ToString() + " " + dsCaseDetails.Tables(0).Rows(0).Item("COUNTRYDES").ToString() + "<br/>" + dsCaseDetails.Tables(0).Rows(0).Item("CaseDes").ToString() + UnitError + CurrError + "<input type='hidden' value='" + arrCaseID(i).ToString() + "' name='Case" + i.ToString() + "'/>"

                CaseDesp.Add(arrCaseID(i).ToString())
                HeaderTdSetting(tdHeader, DWidth, Headertext, 1)
                trHeader.Controls.Add(tdHeader)
            Next
            tblComparision.Controls.Add(trHeader)

            'SUD STARTED
            trHeader = New TableRow
            tdHeader = New TableCell
            tdHeader = New TableCell
            HeaderTdSetting(tdHeader, "200px", "<img alt='' src='../../Images/spacer.gif' Style='width:200px;height:0px;'  />", 1)
            trHeader.Controls.Add(tdHeader)
            trHeader.Height = 20
            trHeader.CssClass = "PageSSHeading"

            Dim hid As New HiddenField
            For i = 0 To DataCnt
                tdHeader = New TableCell
                Dim Headertext As String = String.Empty
                Headertext = "Case#:" + arrCaseID(i).ToString() + "<br/>"
                CaseDesp.Add(arrCaseID(i).ToString())


                btn = New Button
                btn.ID = "btnS_" + (i + 1).ToString()
                btn.Text = "Update"
                btn.Height = 15
                btn.Style.Add("font-size", "9px")
                btn.CommandArgument = arrCaseID(i).ToString()
                If arrCaseID(i) <= 1000 Then
                    btn.Enabled = False
                    btn.Style.Add("background-color", "#a6a6a6")
                    btn.Style.Add("color", "#4d4d4d")
                End If
                'AddHandler btn.Click, AddressOf Update_Click
                btn.Attributes.Add("onClick", "return UpdateSup('" + arrCaseID(i).ToString() + "','" + (i + 1).ToString() + "');")


                HeaderTdSetting(tdHeader, DWidth, Headertext, 1)
                ' tdHeader.Controls.Add(hid)
                tdHeader.Controls.Add(btn)
                trHeader.Controls.Add(tdHeader)


            Next
            tblComparision.Controls.Add(trHeader)
            'SUD ENDED

            For i = 1 To maxSE
                For j = 1 To 8
                    trInner = New TableRow()

                    Select Case j
                        Case 1
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "<b>Asset " + i.ToString() + "</b>", trInner, "AlterNateColor4")
                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Center")
                                tdInner.Text = "&nbsp;"
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 2
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Asset Description", trInner, "AlterNateColor1")
                            trInner.ID = "ADSE_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                lbl.Text = dstblSE.Tables(k).Rows(0).Item("EQUIPDES" + i.ToString() + "").ToString()
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 3
                            tdInner = New TableCell
                            Title = "(" + dstblSE.Tables(0).Rows(0).Item("ASSESTCOSTUNIT").ToString() + ")"
                            LeftTdSetting(tdInner, "Asset Cost Sugg. " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "ACSSE_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                lbl.Text = FormatNumber(dstblSE.Tables(k).Rows(0).Item("ASSETS" + i.ToString() + "").ToString(), 0).ToString()
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 4
                            tdInner = New TableCell
                            Title = "(" + dstblSE.Tables(0).Rows(0).Item("ASSESTCOSTUNIT").ToString() + ")"
                            LeftTdSetting(tdInner, "Asset Cost Pref. " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "ACPSE_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                lbl.Text = FormatNumber(dstblSE.Tables(k).Rows(0).Item("ASSETP" + i.ToString() + "").ToString(), 0).ToString()
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 5
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Number of Assets", trInner, "AlterNateColor2")
                            trInner.ID = "NOASE_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                If (dstblSE.Tables(k).Rows(0).Item("NUM" + i.ToString() + "").ToString() <> "") Then
                                    lbl.Text = FormatNumber(dstblSE.Tables(k).Rows(0).Item("NUM" + i.ToString() + "").ToString(), 0)
                                End If
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 6
                            tdInner = New TableCell
                            Title = "(" + dstblSE.Tables(0).Rows(0).Item("ASSESTCOSTUNIT").ToString() + ")"
                            LeftTdSetting(tdInner, "Investment Cost " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "ICSE_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                If (dstblSE.Tables(k).Rows(0).Item("ICSE" + i.ToString() + "").ToString() <> "") Then
                                    lbl.Text = FormatNumber(dstblSE.Tables(k).Rows(0).Item("ICSE" + i.ToString() + "").ToString(), 0)
                                End If
                               
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 7
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Depreciation Period (years)", trInner, "AlterNateColor2")
                            trInner.ID = "YTDSE_" + i.ToString()

                            For k = 0 To DataCnt
                                'lbl = New Label
                                'lbl.Style.Add("Width", DWidth)
                                txt = New TextBox
                                txt.MaxLength = 6
                                txt.ID = "DEPRES" + i.ToString() + "_" + (k + 1).ToString()
                                txt.Attributes.Add("OnkeyPress", "return clickButton(event)")
                                If arrCaseID(k) <= 1000 Then
                                    txt.Style.Add("background-color", "#a6a6a6")
                                    txt.Enabled = False
                                End If
                                If arrCaseID(k) <= 1000 Then
                                    txt.Style.Add("background-color", "#a6a6a6")
                                    txt.Enabled = False
                                End If

                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                'lbl.Text = FormatNumber(dstblSE.Tables(k).Rows(0).Item("DEPRES" + i.ToString() + "").ToString(), 0).ToString()
                                txt.Text = FormatNumber(dstblSE.Tables(k).Rows(0).Item("DEPRES" + i.ToString() + "").ToString(), 0).ToString()
                                txt.CssClass = "PrefTextBox"
                                'tdInner.Controls.Add(lbl)
                                tdInner.Controls.Add(txt)
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 8
                            tdInner = New TableCell
                            Title = "(" + dstblSE.Tables(0).Rows(0).Item("ASSESTCOSTUNIT").ToString() + ")"
                            LeftTdSetting(tdInner, "Depreciation Cost " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "DCSE_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                lbl.ID = "DEPSECOST" + i.ToString() + "_" + (k + 1).ToString()
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                If (dstblSE.Tables(k).Rows(0).Item("DCSE" + i.ToString() + "").ToString() <> "") Then
                                    lbl.Text = FormatNumber(dstblSE.Tables(k).Rows(0).Item("DCSE" + i.ToString() + "").ToString(), 0)
                                End If

                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next
                    End Select
                    If j <> 1 Then
                        If (j Mod 2 = 0) Then
                            trInner.CssClass = "AlterNateColor1"
                        Else
                            trInner.CssClass = "AlterNateColor2"
                        End If
                    End If
                    tblComparision.Controls.Add(trInner)
                Next
            Next

            'Total For Suppport Equip
            For j = 1 To 3
                trInner = New TableRow()
                Select Case j
                    Case 1
                        tdInner = New TableCell
                        LeftTdSetting(tdInner, "<b>Total</b>", trInner, "AlterNateColor4")
                        For k = 0 To DataCnt
                            tdInner = New TableCell
                            InnerTdSetting(tdInner, "", "Center")
                            tdInner.Text = "&nbsp;"
                            trInner.Controls.Add(tdInner)
                        Next
                    Case 2
                        tdInner = New TableCell
                        Title = "(" + dstblSE.Tables(0).Rows(0).Item("ASSESTCOSTUNIT").ToString() + ")"
                        LeftTdSetting(tdInner, "Investment Cost " + Title + "", trInner, "AlterNateColor1")
                        trInner.ID = "ITSE_1"

                        For k = 0 To DataCnt
                            lbl = New Label
                            lbl.Style.Add("Width", DWidth)
                            tdInner = New TableCell
                            InnerTdSetting(tdInner, "", "Right")
                            lbl.Text = FormatNumber(dstblSE.Tables(k).Rows(0).Item("INVESTTOTALSE").ToString(), 0)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Next

                    Case 3
                        tdInner = New TableCell
                        Title = "(" + dstblSE.Tables(0).Rows(0).Item("ASSESTCOSTUNIT").ToString() + ")"
                        LeftTdSetting(tdInner, "Depreciation Cost " + Title + "", trInner, "AlterNateColor1")
                        trInner.ID = "DTSE_1"
                        For k = 0 To DataCnt
                            lbl = New Label
                            lbl.Style.Add("Width", DWidth)
                            tdInner = New TableCell
                            InnerTdSetting(tdInner, "", "Right")
                            lbl.ID = "SUPTOT" + (k + 1).ToString()
                            lbl.Text = FormatNumber(dstblSE.Tables(k).Rows(0).Item("DEPRETOTALSE").ToString(), 0)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Next
                End Select
                If j <> 1 Then
                    If (j Mod 2 = 0) Then
                        trInner.CssClass = "AlterNateColor1"
                    Else
                        trInner.CssClass = "AlterNateColor2"
                    End If
                End If
                tblComparision.Controls.Add(trInner)
            Next

            'Space Change  Start
            For j = 1 To 1
                trInner = New TableRow()
                Select Case j
                    Case 1
                        tdInner = New TableCell
                        LeftTdSetting(tdInner, "", trInner, "PageSection1")

                        For k = 0 To DataCnt
                            lbl = New Label
                            lbl.Style.Add("Width", DWidth)
                            tdInner = New TableCell
                            InnerTdSetting(tdInner, "", "Right")
                            lbl.Text = ""
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                            trInner.Height = "14"
                        Next
                   
                End Select
                trInner.CssClass = "PageSection1"
                tblComparision.Controls.Add(trInner)
            Next
            'Space Change  end

            'Grand Total
            trHeader = New TableRow
            tdHeader = New TableCell
            HeaderTdSetting(tdHeader, "200px", "Grand Total", 1)
            trHeader.Controls.Add(tdHeader)
            trHeader.Height = 20
            trHeader.CssClass = "PageSSHeading"

            For i = 0 To DataCnt
                dsCaseDetails = objGetData.GetCaseDetails(arrCaseID(i).ToString())
                Cunits = Convert.ToInt32(dstblPE.Tables(0).Rows(0).Item("Units").ToString())
                Units = Convert.ToInt32(dstblPE.Tables(i).Rows(0).Item("Units").ToString())
                CCurrTitle = dstblPE.Tables(0).Rows(0).Item("ASSESTCOSTUNIT").ToString().Trim()
                CurrTitle = dstblPE.Tables(i).Rows(0).Item("ASSESTCOSTUNIT").ToString().Trim()

                tdHeader = New TableCell
                Dim Headertext As String = String.Empty
                'If Cunits <> Units Then
                '    UnitError = "<br/> <span  style='color:red'>Unit Mismatch</span>"
                'Else
                '    UnitError = ""
                'End If
                UnitError = ""

                If CCurrTitle <> CurrTitle Then
                    CurrError = "<br/> <span  style='color:red'>Currency Mismatch</span>"
                Else
                    CurrError = ""
                End If

                Headertext = "Case#:" + arrCaseID(i).ToString() + "<br/>" + dsCaseDetails.Tables(0).Rows(0).Item("CaseDes").ToString() + UnitError + CurrError + "<input type='hidden' value='" + arrCaseID(i).ToString() + "' name='Case" + i.ToString() + "'/>"

                CaseDesp.Add(arrCaseID(i).ToString())
                HeaderTdSetting(tdHeader, DWidth, Headertext, 1)
                trHeader.Controls.Add(tdHeader)
            Next
            tblComparision.Controls.Add(trHeader)

            'Grand Total
            For j = 1 To 2
                trInner = New TableRow()
                Select Case j
                    Case 1
                        tdInner = New TableCell
                        Title = "(" + dstblSE.Tables(0).Rows(0).Item("ASSESTCOSTUNIT").ToString() + ")"
                        LeftTdSetting(tdInner, "Investment Cost " + Title + "", trInner, "AlterNateColor1")
                        trInner.ID = "GITPE_1"

                        For k = 0 To DataCnt
                            lbl = New Label
                            lbl.Style.Add("Width", DWidth)
                            tdInner = New TableCell
                            InnerTdSetting(tdInner, "", "Right")
                            lbl.Text = FormatNumber(dstblPE.Tables(k).Rows(0).Item("ASSETTOTAL").ToString(), 0)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Next
                    Case 2
                        tdInner = New TableCell
                        Title = "(" + dstblSE.Tables(0).Rows(0).Item("ASSESTCOSTUNIT").ToString() + ")"
                        LeftTdSetting(tdInner, "Depreciation Cost " + Title + "", trInner, "AlterNateColor1")
                        trInner.ID = "GDTPE_1"
                        For k = 0 To DataCnt
                            lbl = New Label
                            lbl.ID = "EQSUPCOSTTOT" + (k + 1).ToString()
                            lbl.Style.Add("Width", DWidth)
                            tdInner = New TableCell
                            InnerTdSetting(tdInner, "", "Right")
                            lbl.Text = FormatNumber(dstblPE.Tables(k).Rows(0).Item("DEPTOTAL").ToString(), 0)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Next
                End Select
                If (j Mod 2 = 0) Then
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
            Td.Height = 30
            Td.HorizontalAlign = HorizontalAlign.Center
        Catch ex As Exception

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

        End Try
    End Sub

    Protected Sub TextBoxSetting(ByVal txt As TextBox, ByVal Css As String, ByVal CaseId As Integer)
        Try
            txt.CssClass = Css
            If CaseId <= 1000 And Session("Password") <> "9krh65sve3" Then
                txt.Enabled = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub LableSetting(ByVal lbl As Label, ByVal Css As String)
        Try
            lbl.CssClass = Css
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub LeftTdSetting(ByVal Td As TableCell, ByVal Text As String, ByVal tr As TableRow, ByVal Css As String)
        Try
            Td.Text = Text
            InnerTdSetting(Td, "", "Left")
            tr.Controls.Add(Td)
            tr.CssClass = Css
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub

    <System.Web.Services.WebMethod()> _
    Public Shared Function UpdateCase(ByVal CaseID As String, ByVal index As String, ByVal countP As Integer, ByVal DepreciateP() As String) As Array

        Try
            Dim WPT(10) As String
            Dim ObjGetData As New E1GetData.Selectdata()
            Dim EconConnection As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            Dim StrSqlUpadate As String = ""
            Dim StrSqlIUpadate As String = ""
            Dim odbUtil As New DBUtil()
            Dim dts As New DataSet()
            Dim ObjE3GetData As New E3GetData.Selectdata()
            Dim dsD As New DataSet
            Dim PageInfo As String = ""
            Dim maxPE As Integer = 0
            Dim maxSE As Integer = 0
            Dim ArrVal(30) As String
            'Updating Process equipment

            StrSqlUpadate = ""
            StrSqlIUpadate = ""
            StrSqlUpadate = "UPDATE EQUIPMENTNUMBER SET"
            For Mat = 0 To countP - 1

                StrSqlIUpadate = StrSqlIUpadate + " D" + (Mat + 1).ToString() + "=" + DepreciateP(Mat).ToString().Replace(",", "") + ","

            Next
            StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
            StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
            StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
            odbUtil.UpIns(StrSqlUpadate, EconConnection)


            Dim UserName As String = uName
            Dim ObjUpIns As New E1UpInsData.UpdateInsert
            ObjUpIns.ServerDateUpdate(CaseID, UserName)

            Dim Econ1Conn As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            Dim obj As New EconCalculation.EconCalculation(Econ1Conn)
            obj.EconCalculate(CaseID)


            dts = ObjGetData.GetAssetC(CaseID)
            'dsSE = ObjGetData.GetAssetS(arrCaseID(i))
            For i = 0 To countP

                ArrVal(i) = FormatNumber(CDbl(dts.Tables(0).Rows(0).Item("DCPE" + (i + 1).ToString()).ToString()), 0)
                If i = 0 Then
                    ArrVal(i) = ArrVal(i) + "#" + index.ToString() + "#" + FormatNumber(CDbl(dts.Tables(0).Rows(0).Item("DEPRETOTALPE").ToString()), 0) + "#" + FormatNumber(CDbl(dts.Tables(0).Rows(0).Item("DEPTOTAL").ToString()), 0)
                End If
            Next

           

            Return ArrVal
        Catch ex As Exception

        End Try
    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function UpdateCase2(ByVal CaseID As String, ByVal index As String, ByVal countS As Integer, ByVal DepreciatePS() As String) As Array

        Try
            Dim WPT(10) As String
            Dim ObjGetData As New E1GetData.Selectdata()
            Dim EconConnection As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            Dim StrSqlUpadate As String = ""
            Dim StrSqlIUpadate As String = ""
            Dim odbUtil As New DBUtil()
            Dim dts As New DataSet()
            Dim ObjE3GetData As New E3GetData.Selectdata()
            Dim dsD As New DataSet
            Dim PageInfo As String = ""
            Dim maxPE As Integer = 0
            Dim maxSE As Integer = 0
            Dim ArrVal(30) As String
            'Updating Process equipment

            'Updating DepreciationS period 
            StrSqlUpadate = ""
            StrSqlIUpadate = ""
            StrSqlUpadate = "UPDATE EQUIPMENT2NUMBER SET"
            For Mat = 0 To countS - 1
                StrSqlIUpadate = StrSqlIUpadate + " D" + (Mat + 1).ToString() + "=" + DepreciatePS(Mat).ToString().Replace(",", "") + ","

            Next
            StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
            StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
            StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
            odbUtil.UpIns(StrSqlUpadate, EconConnection)

            Dim UserName As String = uName
            Dim ObjUpIns As New E1UpInsData.UpdateInsert
            ObjUpIns.ServerDateUpdate(CaseID, UserName)

            Dim Econ1Conn As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            Dim obj As New EconCalculation.EconCalculation(Econ1Conn)
            obj.EconCalculate(CaseID)

            Dim dtsE As New DataSet
            dtsE = ObjGetData.GetAssetC(CaseID)
            dts = ObjGetData.GetAssetS(CaseID)
            'dsSE = ObjGetData.GetAssetS(arrCaseID(i))
            For i = 0 To countS

                ArrVal(i) = FormatNumber(CDbl(dts.Tables(0).Rows(0).Item("DCSE" + (i + 1).ToString()).ToString()), 0)
                If i = 0 Then
                    ArrVal(i) = ArrVal(i) + "#" + index.ToString() + "#" + FormatNumber(CDbl(dts.Tables(0).Rows(0).Item("DEPRETOTALSE").ToString()), 0) + "#" + FormatNumber(CDbl(dtsE.Tables(0).Rows(0).Item("DEPTOTAL").ToString()), 0)
                End If
            Next



            Return ArrVal
        Catch ex As Exception

        End Try
    End Function


    <System.Web.Services.WebMethod()> _
    Public Shared Function UpdateAllCases(ByVal CaseID As String, ByVal index As String, ByVal flag As String, ByVal countP As Integer, ByVal countS As Integer, ByVal DepreciateP() As String, ByVal DepreciatePS() As String) As Array


        Try
            Dim WPT(10) As String
            Dim ObjGetData As New E1GetData.Selectdata()
            Dim ObjE3GetData As New E3GetData.Selectdata()
            Dim EconConnection As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            Dim StrSqlUpadate As String = ""
            Dim StrSqlIUpadate As String = ""
            Dim odbUtil As New DBUtil()

            Dim dsD As New DataSet
            Dim PageInfo As String = ""
            Dim dsPE As New DataSet
            Dim dsSE As New DataSet
            Dim dstblPE As New DataSet
            Dim dstblSE As New DataSet
            Dim CountArrPE(30) As String
            Dim CountArrSE(30) As String
            Dim maxPE As Integer = 0
            Dim maxSE As Integer = 0
            Dim DataCnt As New Integer
            Dim ArrVal(30) As String
            'Updating Process & Support equipment together

            StrSqlUpadate = ""
            StrSqlIUpadate = ""
            StrSqlUpadate = "UPDATE EQUIPMENTNUMBER SET"
            For Mat = 0 To countP - 1
                StrSqlIUpadate = StrSqlIUpadate + " D" + (Mat + 1).ToString() + "=" + DepreciateP(Mat).ToString().Replace(",", "") + ","
            Next
            StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
            StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
            StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
            odbUtil.UpIns(StrSqlUpadate, EconConnection)


            'Updating DepreciationS period 
            StrSqlUpadate = ""
            StrSqlIUpadate = ""
            StrSqlUpadate = "UPDATE EQUIPMENT2NUMBER SET"
            For Mat = 0 To countS - 1
                StrSqlIUpadate = StrSqlIUpadate + " D" + (Mat + 1).ToString() + "=" + DepreciatePS(Mat).ToString().Replace(",", "") + ","
            Next
            StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
            StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
            StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
            odbUtil.UpIns(StrSqlUpadate, EconConnection)

            Dim UserName As String = uName
            Dim ObjUpIns As New E1UpInsData.UpdateInsert
            ObjUpIns.ServerDateUpdate(CaseID, UserName)

            Dim Econ1Conn As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            Dim obj As New EconCalculation.EconCalculation(Econ1Conn)
            obj.EconCalculate(CaseID)
           

            Dim dtsE, dtsS As New DataSet
            dtsE = ObjGetData.GetAssetC(CaseID)
            dtsS = ObjGetData.GetAssetS(CaseID)
            'dsSE = ObjGetData.GetAssetS(arrCaseID(i))
            For i = 0 To 29
                ArrVal(i) = FormatNumber(CDbl(dtsE.Tables(0).Rows(0).Item("DCPE" + (i + 1).ToString()).ToString()), 0) + "#" + FormatNumber(CDbl(dtsS.Tables(0).Rows(0).Item("DCSE" + (i + 1).ToString()).ToString()), 0)
                If i = 0 Then
                    ArrVal(i) = ArrVal(i) + "#" + index.ToString() + "#" + FormatNumber(CDbl(dtsE.Tables(0).Rows(0).Item("DEPRETOTALPE").ToString()), 0) + "#" + FormatNumber(CDbl(dtsS.Tables(0).Rows(0).Item("DEPRETOTALSE").ToString()), 0) + "#" + FormatNumber(CDbl(dtsE.Tables(0).Rows(0).Item("DEPTOTAL").ToString()), 0) + "#" + flag
                End If
            Next

            Return ArrVal
        Catch ex As Exception

        End Try
    End Function

End Class
