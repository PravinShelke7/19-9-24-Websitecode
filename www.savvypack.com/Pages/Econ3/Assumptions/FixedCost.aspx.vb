Imports System.Data
Imports System.Data.OleDb
Imports System
Imports E3GetData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Econ3_Assumptions_FixedCost
    Inherits System.Web.UI.Page

    Shared uName As String
#Region "Get Set Variables"
    Dim _lErrorLble As Label
    Dim _strUserName As String
    Dim _strPassword As String
    Dim _iAssumptionId As Integer
    Dim _btnUpdate As ImageButton
    Dim _ctlContentPlaceHolder As ContentPlaceHolder


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
    Protected Sub GetErrorLable()
        ErrorLable = Page.Master.FindControl("lblError")
    End Sub

    Protected Sub GetUpdatebtn()
        Updatebtn = Page.Master.FindControl("imgUpdate")
        Updatebtn.Visible = True

    End Sub

    Protected Sub GetContentPlaceHolder()
        ctlContentPlaceHolder = Page.Master.FindControl("Econ3ContentPlaceHolder")
    End Sub
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Try
                GetContentPlaceHolder()
                GetErrorLable()
                GetUpdatebtn()
                GetSessionDetails()
                GetPageDetails()
                If Not IsPostBack Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Layers", "removeSession();", True)
                End If
            Catch ex As Exception

            End Try

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
        Dim dstbl As New DataSet
        Dim dsCaseDetails As New DataSet
        Dim objGetData As New E1GetData.Selectdata
        Dim CaseIds As String = String.Empty
        'Dim DataCnt As New Integer
        Dim i As New Integer
        Dim j As New Integer
        Dim k As New Integer
        Dim DWidth As String = String.Empty
        Dim arrCaseID() As String
        Dim btn As Button
        Dim CasesVal(9) As String
        Dim Link As HyperLink
        Dim dsDept As New DataSet
        Try
            arrCaseID = GetCaseIds()
            DataCnt = arrCaseID.Length - 1

            dsDept = objGetData.GetDeptN("-1", "", "")

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


            tdHeader = New TableCell
            HeaderTdSetting(tdHeader, "200px", "<img alt='' src='../../Images/spacer.gif' Style='width:200px;height:0px;'  />", 1)
            trHeader.Controls.Add(tdHeader)
            trHeader.Height = 20
            trHeader.CssClass = "PageSSHeading"

            For i = 0 To DataCnt
                Dim ds As New DataSet
                ds = objGetData.GetFixedCostDetails(arrCaseID(i))
                ds.Tables(0).TableName = arrCaseID(i).ToString()
                dstbl.Tables.Add(ds.Tables(arrCaseID(i).ToString()).Copy())
            Next


            Dim dsFCost As New DataSet
            Dim dvFCost As New DataView
            Dim dtFCost As New DataTable
            dsFCost = objGetData.GetFixedCostArch()
            dvFCost = dsFCost.Tables(0).DefaultView


            For i = 0 To DataCnt
                dsCaseDetails = objGetData.GetCaseDetails(arrCaseID(i).ToString())
                Cunits = Convert.ToInt32(dstbl.Tables(0).Rows(0).Item("Units").ToString())
                Units = Convert.ToInt32(dstbl.Tables(i).Rows(0).Item("Units").ToString())
                CCurrTitle = dstbl.Tables(0).Rows(0).Item("Title2").ToString().Trim()
                CurrTitle = dstbl.Tables(i).Rows(0).Item("Title2").ToString().Trim()

                tdHeader = New TableCell
                Dim Headertext As String = String.Empty
                If Cunits <> Units Then
                    UnitError = "<br/> <span  style='color:red'>Unit Mismatch</span>"
                Else
                    UnitError = ""
                End If

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
                btn.Attributes.Add("onClick", "return Update('" + arrCaseID(i).ToString() + "','" + (i + 1).ToString() + "');")
                HeaderTdSetting(tdHeader, DWidth, Headertext, 1)
                ' tdHeader.Controls.Add(hid)
                tdHeader.Controls.Add(btn)
                trHeader.Controls.Add(tdHeader)


            Next
            tblComparision.Controls.Add(trHeader)
            'SUD ENDED

            'For j = 1 To 4
            '    trInner = New TableRow()
            '    Select Case j
            '        Case 1
            '            tdInner = New TableCell
            '            LeftTdSetting(tdInner, "<b>Depriciation</b>", trInner, "AlterNateColor4")
            '            For k = 0 To DataCnt
            '                lbl = New Label

            '                tdInner = New TableCell
            '                InnerTdSetting(tdInner, "", "Center")
            '                tdInner.Text = "&nbsp;"
            '                trInner.Controls.Add(tdInner)
            '            Next
            '        Case 2
            '            tdInner = New TableCell
            '            Title = "(" + dstbl.Tables(0).Rows(0).Item("TITLE2").ToString() + ")"
            '            LeftTdSetting(tdInner, "Total Asset Value " + Title + " ", trInner, "AlternateColor2")
            '            trInner.ID = "TOTASSET1"
            '            For k = 0 To DataCnt
            '                lbl = New Label
            '                lbl.Style.Add("Width", DWidth)
            '                tdInner = New TableCell
            '                InnerTdSetting(tdInner, "", "Right")
            '                lbl.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("ASSETTOTAL").ToString(), 0).ToString()
            '                tdInner.Controls.Add(lbl)
            '                trInner.Controls.Add(tdInner)
            '            Next
            '        Case 3

            '            tdInner = New TableCell
            '            LeftTdSetting(tdInner, "Years To Depreciate", trInner, "AlternateColor1")
            '            trInner.ID = "DEPYEARS1"
            '            For k = 0 To DataCnt
            '                lbl = New Label
            '                lbl.Style.Add("Width", DWidth)
            '                tdInner = New TableCell
            '                InnerTdSetting(tdInner, "", "Right")
            '                lbl.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("DEPYEARS").ToString(), 0).ToString()
            '                tdInner.Controls.Add(lbl)
            '                trInner.Controls.Add(tdInner)
            '            Next
            '        Case 4
            '            tdInner = New TableCell
            '            Title = ("(" + dstbl.Tables(0).Rows(0).Item("TITLE2").ToString() + ")")
            '            LeftTdSetting(tdInner, "Annual Depreciation Cost " + Title + " ", trInner, "AlternateColor2")
            '            trInner.ID = "DEPANNUAL1"
            '            For k = 0 To DataCnt
            '                lbl = New Label
            '                lbl.Style.Add("WIdth", DWidth)
            '                tdInner = New TableCell
            '                InnerTdSetting(tdInner, "", "Right")
            '                lbl.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("DEPANNUAL").ToString(), 0).ToString()
            '                tdInner.Controls.Add(lbl)
            '                trInner.Controls.Add(tdInner)
            '            Next
            '    End Select
            '    If j <> 1 Then
            '        If (j Mod 2 = 0) Then
            '            trInner.CssClass = "AlterNateColor2"
            '        Else
            '            trInner.CssClass = "AlterNateColor1"
            '        End If
            '    End If
            '    tblComparision.Controls.Add(trInner)
            'Next

            For i = 1 To 30
                For j = 0 To 7
                    trInner = New TableRow()

                    Select Case j
                        Case 0
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "<b>Category " + i.ToString() + "</b>", trInner, "AlterNateColor4")
                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Center")
                                tdInner.Text = "&nbsp;"
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 1
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Category", trInner, "AlterNateColor2")
                            trInner.ID = "CT_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                lbl.Text = dstbl.Tables(k).Rows(0).Item("CATEGORY" + i.ToString() + "").ToString()
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 2
                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("TITLE2").ToString() + " or %)"
                            LeftTdSetting(tdInner, "Fixed Cost Guidelines Value Sugg. " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "FGVS_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                'lbl.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("FCSG" + i.ToString() + "").ToString(), 0).ToString()
                                dvFCost.RowFilter = "CATEGORYDES='" + dstbl.Tables(k).Rows(0).Item("CATEGORY" + i.ToString() + "").ToString() + "' AND EFFDATE='" + dstbl.Tables(k).Rows(0).Item("EFFDATE").ToString() + "' AND COUNTRYID=" + dstbl.Tables(k).Rows(0).Item("OCOUNTRY").ToString() + ""
                                dtFCost = dvFCost.ToTable()
                                If dtFCost.Rows.Count > 0 Then
                                    If dtFCost.Rows(0).Item("RULE").ToString() = "perc" Then
                                        lbl.Text = FormatNumber(dtFCost.Rows(0).Item("value").ToString(), 2)
                                    Else
                                        lbl.Text = FormatNumber(dtFCost.Rows(0).Item("value").ToString() * dstbl.Tables(k).Rows(0).Item("CURR").ToString(), 2)
                                    End If
                                Else
                                    lbl.Text = "0.00"
                                End If
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 3
                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("TITLE2").ToString() + " or %)"
                            LeftTdSetting(tdInner, "Fixed Cost Guidelines Value Pref. " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "FGV_" + i.ToString()

                            For k = 0 To DataCnt
                                'lbl = New Label
                                'lbl.Style.Add("Width", DWidth)
                                txt = New TextBox
                                txt.MaxLength = 8
                                txt.ID = "FIXCOST" + i.ToString() + "_" + (k + 1).ToString()
                                txt.Attributes.Add("OnkeyPress", "return clickButton(event)")
                                If arrCaseID(k) <= 1000 Then
                                    txt.Style.Add("background-color", "#a6a6a6")
                                    txt.Enabled = False
                                End If
                                txt.CssClass = "PrefTextBox"

                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                ' txt.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("FIXCOST" + i.ToString() + "").ToString(), 2).ToString()
                                If dstbl.Tables(k).Rows(0).Item("FIXCOST" + i.ToString() + "").ToString() <> "" Then
                                    txt.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("FIXCOST" + i.ToString() + "").ToString(), 2).ToString()
                                End If
                                tdInner.Controls.Add(txt)
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 4
                            tdInner = New TableCell
                            Title = ""
                            LeftTdSetting(tdInner, "Rule" + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "RUL_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                lbl.Text = dstbl.Tables(k).Rows(0).Item("RULE" + i.ToString() + "").ToString()
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 5
                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("TITLE2").ToString() + ")"
                            LeftTdSetting(tdInner, "Fixed Cost Sugg. " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "FCS_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.ID = "FCSUG" + i.ToString() + "_" + (k + 1).ToString()
                                lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                lbl.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("FCSG" + i.ToString() + "").ToString(), 0).ToString()
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 6
                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("TITLE2").ToString() + ")"
                            LeftTdSetting(tdInner, "Fixed Cost Pref. " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "FCP_" + i.ToString()

                            For k = 0 To DataCnt
                                'lbl = New Label
                                'lbl.Style.Add("Width", DWidth)
                                txt = New TextBox
                                txt.MaxLength = 12
                                txt.ID = "FCPREF" + i.ToString() + "_" + (k + 1).ToString()
                                txt.Attributes.Add("OnkeyPress", "return clickButton(event)")
                                If arrCaseID(k) <= 1000 Then
                                    txt.Style.Add("background-color", "#a6a6a6")
                                    txt.Enabled = False
                                End If
                                txt.CssClass = "PrefTextBox"

                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                txt.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("FCPREF" + i.ToString() + "").ToString(), 0).ToString()
                                tdInner.Controls.Add(txt)
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 7
                            tdInner = New TableCell
                            Title = ""
                            LeftTdSetting(tdInner, "Mfg. Department" + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "MD_" + i.ToString()

                            'For k = 0 To DataCnt
                            '    lbl = New Label
                            '    lbl.Style.Add("Width", DWidth)
                            '    tdInner = New TableCell
                            '    InnerTdSetting(tdInner, "", "Right")
                            '    GetDeptDetails(lbl, dstbl.Tables(k).Rows(0).Item("DEPID" + i.ToString() + "").ToString(), arrCaseID(k).ToString())
                            '    tdInner.Controls.Add(lbl)
                            '    trInner.Controls.Add(tdInner)
                            'Next

                            For k = 0 To DataCnt
                                Link = New HyperLink
                                hid = New HiddenField
                                Link.ID = "hypMDepdes" + i.ToString() + "_" + (k + 1).ToString()
                                hid.ID = "hidMDepdesid" + i.ToString() + "_" + (k + 1).ToString()
                                Link.CssClass = "Link"
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                GetDeptDetails(Link, hid, dstbl.Tables(k).Rows(0).Item("DEPID" + i.ToString() + "").ToString(), arrCaseID(k), dsDept)
                                If arrCaseID(k) <= 1000 Then
                                    Link.NavigateUrl = Nothing
                                End If
                                tdInner.Controls.Add(hid)
                                tdInner.Controls.Add(Link)
                                trInner.Controls.Add(tdInner)
                            Next
                    End Select
                    If j <> 0 Then
                        If (j Mod 2 = 0) Then
                            trInner.CssClass = "AlterNateColor1"
                        Else
                            trInner.CssClass = "AlterNateColor2"
                        End If
                    End If
                    tblComparision.Controls.Add(trInner)
                Next
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

    Protected Sub GetEquipmentInDetails(ByRef lbl As Label, ByVal EqId As Integer)
        Dim Ds As New DataSet
        Dim ObjGetdata As New E1GetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty
        Try

            Ds = ObjGetdata.GetEquipment(EqId, "", "")
            lbl.Text = Ds.Tables(0).Rows(0).Item("equipDES").ToString()
        Catch ex As Exception
            ErrorLable.Text = "Error:GetEquipmentInDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetDeptDetailsOLD(ByRef lbl As Label, ByVal ProcId As Integer, ByVal CaseId As String)
        Dim Ds As New DataSet
        Dim ObjGetdata As New E1GetData.Selectdata()
        Dim Path As String = String.Empty
        Try
            Ds = ObjGetdata.GetDept(ProcId, "", "", CaseId)

            If Ds.Tables(0).Rows.Count = 0 Then
                lbl.Text = "Dept. Conflict"
                lbl.ForeColor = Drawing.Color.DarkRed
            Else
                lbl.Text = Ds.Tables(0).Rows(0).Item("PROCDE").ToString()
            End If



        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetDeptDetails(ByRef LinkDep As HyperLink, ByVal hid As HiddenField, ByVal ProcId As Integer, ByVal CaseId As String, ByVal dsDept As DataSet)
        Dim Ds As New DataSet
        Dim ObjGetdata As New E1GetData.Selectdata()
        Dim Path As String = String.Empty
        Dim dvDept As New DataView
        Dim dtDept As New DataTable
        Try
            'Ds = ObjGetdata.GetDept(ProcId, "", "", CaseId)
            dvDept = dsDept.Tables(0).DefaultView
            dvDept.RowFilter = "PROCID = " + ProcId.ToString()
            dtDept = dvDept.ToTable()

            Path = "../../Econ1/PopUp/GetDepPopUp.aspx?Des=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + LinkDep.ClientID + "&Id=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + hid.ClientID + "&CaseID=" + CaseId.ToString()
            If dtDept.Rows.Count = 0 Then
                LinkDep.Text = "Dept. Conflict"
                LinkDep.ForeColor = Drawing.Color.DarkRed
            Else
                LinkDep.Text = dtDept.Rows(0).Item("PROCDE").ToString()
            End If

            hid.Value = ProcId.ToString()
            LinkDep.NavigateUrl = "javascript:ShowPopWindow('" + Path + "')"
        Catch ex As Exception
            ErrorLable.Text = "Error:GetDeptDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub


    <System.Web.Services.WebMethod()> _
    Public Shared Function UpdateAllCases(ByVal CaseID As String, ByVal index As String, ByVal flag As String, ByVal FIXCOST() As String, ByVal FCPREF() As String, ByVal mdep() As String) As Array
        Try
            Dim WPT(10) As String
            Dim ObjGetData As New E1GetData.Selectdata()
            Dim ObjE3GetData As New E3GetData.Selectdata()
            Dim EconConnection As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            Dim StrSqlUpadate As String = ""
            Dim StrSqlIUpadate As String = ""
            Dim odbUtil As New DBUtil()
            Dim dts As New DataSet()
            Dim dsD As New DataSet
            Dim ArrVal(30) As String
            'Preferences
            dts = ObjGetData.GetPref(CaseID)
            Dim Curr As String = dts.Tables(0).Rows(0).Item("curr")

            StrSqlUpadate = ""
            StrSqlIUpadate = ""
            Dim FixCostVal As Double
            Dim intFixCostVal As New Integer
            StrSqlUpadate = "UPDATE fixedcostPCT SET"
            For Mat = 0 To 29
                'FixCostVal = CDbl(FIXCOST(Mat).Replace(",", "") / Curr)
                'StrSqlIUpadate = StrSqlIUpadate + " M" + (Mat + 1).ToString() + "=" + FixCostVal.ToString() + ","
                intFixCostVal = Mat + 1
                'If Mat = 0 Or Mat = 1 Or Mat = 4 Or Mat = 5 Or Mat = 6 Then
                '    FixCostVal = CDbl(FIXCOST(Mat).Replace(",", "") / Curr)
                '    StrSqlIUpadate = StrSqlIUpadate + " M" + intFixCostVal.ToString() + " = " + FixCostVal.ToString() + ","
                'Else
                '    StrSqlIUpadate = StrSqlIUpadate + " M" + intFixCostVal.ToString() + " = " + FIXCOST(Mat).Replace(",", "").ToString() + ","
                'End If
                If FIXCOST(Mat).ToString() <> "" Then
                    If Mat = 0 Or Mat = 1 Or Mat = 4 Or Mat = 5 Or Mat = 6 Then
                        FixCostVal = CDbl(FIXCOST(Mat).Replace(",", "") / Curr)
                        StrSqlIUpadate = StrSqlIUpadate + " M" + intFixCostVal.ToString() + " = " + FixCostVal.ToString() + ","
                    Else
                        StrSqlIUpadate = StrSqlIUpadate + " M" + intFixCostVal.ToString() + " = " + FIXCOST(Mat).Replace(",", "").ToString() + ","
                    End If
                Else
                    StrSqlIUpadate = StrSqlIUpadate + " M" + intFixCostVal.ToString() + " = NULL,"
                End If
            Next
            StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
            StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
            StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
            odbUtil.UpIns(StrSqlUpadate, EconConnection)


            StrSqlUpadate = ""
            StrSqlIUpadate = ""
            Dim PrefCostVal As Double
            StrSqlUpadate = "UPDATE fixedcostPRE SET"
            For Mat = 0 To 29
                PrefCostVal = CDbl(FCPREF(Mat).Replace(",", "") / Curr)
                StrSqlIUpadate = StrSqlIUpadate + " M" + (Mat + 1).ToString() + "=" + PrefCostVal.ToString() + ","
            Next
            StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
            StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
            StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
            odbUtil.UpIns(StrSqlUpadate, EconConnection)

            'update mdep
            StrSqlUpadate = ""
            StrSqlIUpadate = ""
            StrSqlUpadate = "UPDATE FIXEDCOSTDEP SET"
            For Mat = 0 To 29
                StrSqlIUpadate = StrSqlIUpadate + " M" + (Mat + 1).ToString() + "=" + mdep(Mat).ToString() + ","
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


            Dim ds As New DataSet
            Dim l As Integer = 0
            ds = ObjGetData.GetFixedCostDetails(CaseID)
            For i = 0 To 29
                ArrVal(i) = FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("FCSG" + (i + 1).ToString()).ToString()), 0)
                If i = 0 Then
                    ArrVal(i) = ArrVal(i) + "#" + index.ToString() + "#" + flag
                End If
            Next
            Return ArrVal

        Catch ex As Exception

        End Try
    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function UpdateCase(ByVal CaseID As String, ByVal index As String, ByVal FIXCOST() As String, ByVal FCPREF() As String, ByVal mdep() As String) As Array
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
            Dim ArrVal(30) As String

            'Preferences
            dts = ObjGetData.GetPref(CaseID)
            Dim Curr As String = dts.Tables(0).Rows(0).Item("curr")

            StrSqlUpadate = ""
            StrSqlIUpadate = ""
            Dim FixCostVal As Double
            Dim intFixCostVal As New Integer
            StrSqlUpadate = "UPDATE fixedcostPCT SET"
            For Mat = 0 To 29
                'FixCostVal = CDbl(FIXCOST(Mat).Replace(",", "") / Curr)
                'StrSqlIUpadate = StrSqlIUpadate + " M" + (Mat + 1).ToString() + "=" + FixCostVal.ToString() + ","
                intFixCostVal = Mat + 1
                'If Mat = 0 Or Mat = 1 Or Mat = 4 Or Mat = 5 Or Mat = 6 Then
                '    FixCostVal = CDbl(FIXCOST(Mat).Replace(",", "") / Curr)
                '    StrSqlIUpadate = StrSqlIUpadate + " M" + intFixCostVal.ToString() + " = " + FixCostVal.ToString() + ","
                'Else
                '    StrSqlIUpadate = StrSqlIUpadate + " M" + intFixCostVal.ToString() + " = " + FIXCOST(Mat).Replace(",", "").ToString() + ","
                'End If
                If FIXCOST(Mat).ToString() <> "" Then
                    If Mat = 0 Or Mat = 1 Or Mat = 4 Or Mat = 5 Or Mat = 6 Then
                        FixCostVal = CDbl(FIXCOST(Mat).Replace(",", "") / Curr)
                        StrSqlIUpadate = StrSqlIUpadate + " M" + intFixCostVal.ToString() + " = " + FixCostVal.ToString() + ","
                    Else
                        StrSqlIUpadate = StrSqlIUpadate + " M" + intFixCostVal.ToString() + " = " + FIXCOST(Mat).Replace(",", "").ToString() + ","
                    End If
                Else
                    StrSqlIUpadate = StrSqlIUpadate + " M" + intFixCostVal.ToString() + " = NULL,"
                End If
            Next
            StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
            StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
            StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
            odbUtil.UpIns(StrSqlUpadate, EconConnection)


            StrSqlUpadate = ""
            StrSqlIUpadate = ""
            Dim PrefCostVal As Double
            StrSqlUpadate = "UPDATE fixedcostPRE SET"
            For Mat = 0 To 29
                PrefCostVal = CDbl(FCPREF(Mat).Replace(",", "") / Curr)
                StrSqlIUpadate = StrSqlIUpadate + " M" + (Mat + 1).ToString() + "=" + PrefCostVal.ToString() + ","
            Next
            StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
            StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
            StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
            odbUtil.UpIns(StrSqlUpadate, EconConnection)

            'update mdep
            StrSqlUpadate = ""
            StrSqlIUpadate = ""
            StrSqlUpadate = "UPDATE FIXEDCOSTDEP SET"
            For Mat = 0 To 29
                StrSqlIUpadate = StrSqlIUpadate + " M" + (Mat + 1).ToString() + "=" + mdep(Mat).ToString() + ","
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


            Dim ds As New DataSet
            Dim l As Integer = 0
            ds = ObjGetData.GetFixedCostDetails(CaseID)
            For i = 0 To 29
                ArrVal(i) = FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("FCSG" + (i + 1).ToString()).ToString()), 0)
                If i = 0 Then
                    ArrVal(i) = ArrVal(i) + "#" + index.ToString()
                End If
            Next
            Return ArrVal


        Catch ex As Exception

        End Try
    End Function



End Class
