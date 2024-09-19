Imports System.Data
Imports System.Data.OleDb
Imports System
Imports E3GetData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Econ3_Assumptions_Extrusion
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
    Protected Sub GetContentPlaceHolder()
        ctlContentPlaceHolder = Page.Master.FindControl("Econ3ContentPlaceHolder")
    End Sub

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
        Dim objGetDataE3 As New E3GetData.Selectdata
        Dim CaseIds As String = String.Empty
        'Dim DataCnt As New Integer
        Dim i As New Integer
        Dim j As New Integer
        Dim k As New Integer
        Dim DWidth As String = String.Empty
        Dim arrCaseID() As String
        Dim Cost(9, 9) As String
        Dim TotalCost(9) As String
        Dim CasesVal(9) As String
        Dim dsEmattbl As New DataSet
        Dim dv As New DataView
        Dim dt As New DataTable
        Dim dsMat1 As New DataSet
        Dim dsDept As New DataSet
        Try
            dsMat1 = objGetData.GetMaterials("-1", "", "")
            dsDept = objGetData.GetDeptN("-1", "", "")

            arrCaseID = GetCaseIds()
            DataCnt = arrCaseID.Length - 1

            For i = 0 To 9
                If i <= DataCnt Then
                    CasesVal(i) = arrCaseID(i)
                Else
                    CasesVal(i) = Nothing
                End If
            Next

            ' Updatebtn.Attributes.Add("onClick", "return UpdateAll('" + arrCaseID(0).ToString() + "','" + arrCaseID(1).ToString() + "','" + arrCaseID(2).ToString() + "','" + arrCaseID(3).ToString() + "','" + arrCaseID(4).ToString() + "','" + DataCnt.ToString() + "');")
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
            Dim Title As String = String.Empty
            Dim CurrTitle As String = String.Empty
            Dim CCurrTitle As String = String.Empty
            Dim btn As Button
            Dim objGetDataW As New E3GetData.Selectdata
            Dim dsW As New DataSet
            Dim hid As New HiddenField
            Dim Link As New HyperLink
            Dim lnkG As New HyperLink
            Dim hidG As New HiddenField

            dsW = objGetDataW.CompCOLWIDTH(AssumptionId)
            If dsW.Tables(0).Rows.Count > 0 Then
                txtDWidth.Text = dsW.Tables(0).Rows(0).Item("COLWIDTH").ToString() - 1
            End If

            DWidth = txtDWidth.Text + "px"
            Dim UnitError As String = String.Empty
            Dim CurrError As String = String.Empty

            tdHeader = New TableCell
            HeaderTdSetting(tdHeader, "200px", "<img alt='' src='../../Images/spacer.gif' Style='width:200px;height:0px;'  />", 1)
            trHeader.Controls.Add(tdHeader)
            trHeader.Height = 20
            trHeader.CssClass = "PageSSHeading"

            Dim caseId As String = ""
            For i = 0 To DataCnt
                caseId = caseId + "" + arrCaseID(i) + ","
            Next
            caseId = caseId.Remove(caseId.Length - 1)
            Dim dts As New DataSet
            Dim dvs As New DataView
            Dim dt1 As New DataTable

            Dim dstblPref As New DataSet
            Dim dstblSug As New DataSet
            Dim dtsPref As New DataSet
            Dim dvsPref As New DataView
            Dim dtPref As New DataTable
            Dim dtsSug As New DataSet
            Dim dvsSug As New DataView
            Dim dtSug As New DataTable
            Dim dsPref As New DataSet
            Dim dsSug As New DataSet

            'dts = objGetDataE3.GetExtrusionDetails(caseId)
            'dvs = dts.Tables(0).DefaultView

            For i = 0 To DataCnt
                Dim ds As New DataSet
                Dim dsEmat As New DataSet
                '' ds = objGetData.GetExtrusionDetails(arrCaseID(i))
                'dvs.RowFilter = "CASEID=" + arrCaseID(i).ToString()
                'dt1 = dvs.ToTable()
                'ds.Tables.Add(dt1)

                'ds.Tables(0).TableName = arrCaseID(i).ToString()
                'dstbl.Tables.Add(ds.Tables(arrCaseID(i).ToString()).Copy())

                dsPref = objGetData.GetExtrusionDetailsBarrP(arrCaseID(i))
                dsPref.Tables(0).TableName = arrCaseID(i).ToString()
                dstblPref.Tables.Add(dsPref.Tables(arrCaseID(i).ToString()).Copy())

                dsSug = objGetData.GetExtrusionDetailsBarrS(arrCaseID(i))
                dsSug.Tables(0).TableName = arrCaseID(i).ToString()
                dstblSug.Tables.Add(dsSug.Tables(arrCaseID(i).ToString()).Copy())

                For l = 1 To 10
                    If dstblPref.Tables(arrCaseID(i)).Rows(0).Item("PRP" + l.ToString() + "").ToString() <> "0" And dstblPref.Tables(arrCaseID(i)).Rows(0).Item("PRP" + l.ToString() + "").ToString() <> "" Then
                        Cost(i, l - 1) = CDbl(dstblPref.Tables(arrCaseID(i)).Rows(0).Item("PRP" + l.ToString()).ToString()) * CDbl(dstblSug.Tables(arrCaseID(i)).Rows(0).Item("WTPARA" + l.ToString()).ToString())
                    Else
                        Cost(i, l - 1) = CDbl(dstblSug.Tables(arrCaseID(i)).Rows(0).Item("PRS" + l.ToString()).ToString()) * CDbl(dstblSug.Tables(arrCaseID(i)).Rows(0).Item("WTPARA" + l.ToString()).ToString())
                    End If
                    TotalCost(i) = TotalCost(i) + Cost(i, l - 1)
                Next
            Next




            Dim dvCase As New DataView
            Dim dtCase As New DataTable
            dsCaseDetails = objGetDataE3.GetCaseDetails(caseId)
            dvCase = dsCaseDetails.Tables(0).DefaultView
            For i = 0 To DataCnt
                ' dsCaseDetails = objGetDataE3.GetCaseDetails(arrCaseID(i).ToString())
                dvCase.RowFilter = "CASEID=" + arrCaseID(i).ToString()
                dtCase = dvCase.ToTable()

                Cunits = Convert.ToInt32(dstblPref.Tables(arrCaseID(0)).Rows(0).Item("Units").ToString())
                Units = Convert.ToInt32(dstblPref.Tables(arrCaseID(i)).Rows(0).Item("Units").ToString())
                CCurrTitle = dstblPref.Tables(arrCaseID(0)).Rows(0).Item("Title2").ToString().Trim()
                CurrTitle = dstblPref.Tables(arrCaseID(i)).Rows(0).Item("Title2").ToString().Trim()

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

                Headertext = "Case#:" + arrCaseID(i).ToString() + " " + dsCaseDetails.Tables(0).Rows(i).Item("COUNTRYDES").ToString() + "<br/>" + dtCase.Rows(0).Item("CaseDes").ToString() + UnitError + CurrError + "<input type='hidden' value='" + arrCaseID(i).ToString() + "' name='Case" + i.ToString() + "'/>"

                CaseDesp.Add(arrCaseID(i).ToString())
                HeaderTdSetting(tdHeader, DWidth, Headertext, 1)
                trHeader.Controls.Add(tdHeader)


            Next
            tblComparision.Controls.Add(trHeader)
            For i = 1 To 10
                For j = 1 To 13
                    trInner = New TableRow()

                    Select Case j
                        Case 1
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "<b>Layer" + i.ToString() + "</b>", trInner, "AlterNateColor4")
                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                If i = 1 Then
                                    btn = New Button
                                    btn.ID = "btn_" + (k + 1).ToString()
                                    btn.Text = "Update"
                                    btn.Height = 15
                                    btn.Style.Add("font-size", "9px")
                                    btn.CommandArgument = arrCaseID(k).ToString()
                                    If arrCaseID(k) <= 1000 Then
                                        btn.Enabled = False
                                        btn.Style.Add("background-color", "#a6a6a6")
                                        btn.Style.Add("color", "#4d4d4d")
                                    End If
                                    'AddHandler btn.Click, AddressOf Update_Click
                                    btn.Attributes.Add("onClick", "return Update('" + arrCaseID(k).ToString() + "','" + (k + 1).ToString() + "');")
                                    tdInner.Controls.Add(btn)
                                Else
                                    tdInner.Text = "&nbsp;"
                                End If
                                InnerTdSetting(tdInner, "", "Center")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 2
                            '  If i = 1 Then
                            tdInner = New TableCell
                            Title = ""
                            LeftTdSetting(tdInner, "Effective Date" + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "EFFD_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                lbl.Text = dstblPref.Tables(k).Rows(0).Item("EDATE").ToString()
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next
                            ' End If
                        Case 3
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Material", trInner, "AlterNateColor1")
                            trInner.ID = "M_" + i.ToString()

                            For k = 0 To DataCnt
                                'lbl = New Label
                                'lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                ' lbl.Width = DWidth.Replace("px", "") - 5

                                Link = New HyperLink
                                hid = New HiddenField
                                Link.ID = "hypmatdes" + i.ToString() + "_" + (k + 1).ToString()
                                hid.ID = "hidmatid" + i.ToString() + "_" + (k + 1).ToString()
                                Link.Width = DWidth.Replace("px", "") - 5
                                Link.CssClass = "Link"
                                lnkG = New HyperLink
                                hidG = New HiddenField
                                lnkG.ID = "hypGradeDes" + i.ToString() + "_" + (k + 1).ToString()
                                lnkG.Style.Add("display", "none")
                                hidG.ID = "hidGradeId" + i.ToString() + "_" + (k + 1).ToString()

                                InnerTdSetting(tdInner, "", "Right")
                                'GetMaterialDetails(Link, hid, CInt(dstbl.Tables(k).Rows(0).Item("M" + i.ToString() + "").ToString()))
                                GetMaterialDetailsNew(Link, hid, CInt(dstblPref.Tables(k).Rows(0).Item("M" + i.ToString() + "").ToString()), "hypGradeDes" + i.ToString() + "_" + (k + 1).ToString(), "hidGradeId" + i.ToString() + "_" + (k + 1).ToString(), "SGS_" + i.ToString() + "_" + (k + 1).ToString(), arrCaseID(k).ToString(), dsMat1)
                                If arrCaseID(k) <= 1000 Then
                                    Link.Style.Add("color", "#4d4d4d")
                                    Link.Enabled = False
                                End If
                                tdInner.Controls.Add(hid)
                                tdInner.Controls.Add(Link)
                                tdInner.Controls.Add(hidG)
                                tdInner.Controls.Add(lnkG)
                                'tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 4
                            tdInner = New TableCell
                            Title = "(" + dstblPref.Tables(0).Rows(0).Item("Title1").ToString() + ")"
                            LeftTdSetting(tdInner, "Thickness " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "T_" + i.ToString()

                            For k = 0 To DataCnt
                                'lbl = New Label
                                'lbl.Style.Add("Width", DWidth)
                                txt = New TextBox
                                txt.ID = "Thick" + i.ToString() + "_" + (k + 1).ToString()
                                txt.Attributes.Add("OnkeyPress", "return clickButton(event)")
                                txt.CssClass = "PrefTextBox"
                                txt.Text = FormatNumber(dstblPref.Tables(k).Rows(0).Item("THICK" + i.ToString() + "").ToString(), 3).ToString()
                                txt.MaxLength = 6
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                'lbl.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("THICK" + i.ToString() + "").ToString(), 3).ToString()
                                'tdInner.Controls.Add(lbl)
                                If arrCaseID(k) <= 1000 Then
                                    txt.Style.Add("background-color", "#a6a6a6")
                                    txt.Enabled = False
                                End If
                                tdInner.Controls.Add(txt)
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 5
                            tdInner = New TableCell
                            Title = "(" + dstblPref.Tables(0).Rows(0).Item("Title2").ToString() + "/" + dstblPref.Tables(0).Rows(0).Item("Title8").ToString() + ")"
                            LeftTdSetting(tdInner, "Price Sugg. " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "PRS_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                lbl.ID = "PRS_" + i.ToString() + "_" + (k + 1).ToString()
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                lbl.Text = FormatNumber(dstblSug.Tables(k).Rows(0).Item("PRS" + i.ToString() + "").ToString(), 4).ToString()
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 6
                            tdInner = New TableCell
                            Title = "(" + dstblPref.Tables(0).Rows(0).Item("Title2").ToString() + "/" + dstblPref.Tables(0).Rows(0).Item("Title8").ToString() + ")"
                            LeftTdSetting(tdInner, "Price Pref. " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "PRP_" + i.ToString()

                            For k = 0 To DataCnt
                                txt = New TextBox
                                txt.MaxLength = 9
                                txt.Width = 70
                                txt.ID = "txtPref_" + i.ToString() + "_" + (k + 1).ToString()
                                txt.Attributes.Add("OnkeyPress", "return clickButton(event)")
                                'txt.Style.Add("Width", "100px")
                                txt.CssClass = "PrefTextBox"
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                If dstblPref.Tables(k).Rows(0).Item("PRP" + i.ToString() + "").ToString() <> "" Then
                                    txt.Text = FormatNumber(dstblPref.Tables(k).Rows(0).Item("PRP" + i.ToString() + "").ToString(), 4).ToString()
                                Else
                                    txt.Text = "0.0000"
                                End If
                                If arrCaseID(k) <= 1000 Then
                                    txt.Style.Add("background-color", "#a6a6a6")
                                    txt.Enabled = False
                                End If
                                tdInner.Controls.Add(txt)
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 7
                            tdInner = New TableCell
                            Title = "(%)"
                            LeftTdSetting(tdInner, "Recycle " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "RE_" + i.ToString()

                            For k = 0 To DataCnt
                                'lbl = New Label
                                'lbl.Style.Add("Width", DWidth)
                                txt = New TextBox
                                txt.ID = "RECY" + i.ToString() + "_" + (k + 1).ToString()
                                txt.Attributes.Add("OnkeyPress", "return clickButton(event)")
                                txt.CssClass = "PrefTextBox"
                                txt.Width = 50
                                txt.MaxLength = 6
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                'lbl.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("R" + i.ToString() + "").ToString(), 2).ToString()
                                txt.Text = FormatNumber(dstblPref.Tables(k).Rows(0).Item("R" + i.ToString() + "").ToString(), 2).ToString()
                                If arrCaseID(k) <= 1000 Then
                                    txt.Style.Add("background-color", "#a6a6a6")
                                    txt.Enabled = False
                                End If
                                tdInner.Controls.Add(txt)
                                ' tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 8
                            tdInner = New TableCell
                            Title = "(%)"
                            LeftTdSetting(tdInner, "Extra-process  " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "E_" + i.ToString()

                            For k = 0 To DataCnt
                                'lbl = New Label
                                'lbl.Style.Add("Width", DWidth)
                                txt = New TextBox
                                txt.ID = "EXPR" + i.ToString() + "_" + (k + 1).ToString()
                                txt.Attributes.Add("OnkeyPress", "return clickButton(event)")
                                txt.CssClass = "PrefTextBox"
                                txt.Width = 50
                                txt.MaxLength = 6
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                'lbl.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("E" + i.ToString() + "").ToString(), 2).ToString()
                                txt.Text = FormatNumber(dstblPref.Tables(k).Rows(0).Item("E" + i.ToString() + "").ToString(), 2).ToString()
                                If arrCaseID(k) <= 1000 Then
                                    txt.Style.Add("background-color", "#a6a6a6")
                                    txt.Enabled = False
                                End If
                                tdInner.Controls.Add(txt)
                                'tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 9
                            tdInner = New TableCell
                            Title = ""
                            LeftTdSetting(tdInner, "Specific Gravity Sugg. " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "SGS_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.ID = "SGS_" + i.ToString() + "_" + (k + 1).ToString()
                                lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                lbl.Text = FormatNumber(dstblSug.Tables(k).Rows(0).Item("SGS" + i.ToString() + "").ToString(), 3).ToString()
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 10
                            tdInner = New TableCell
                            Title = ""
                            LeftTdSetting(tdInner, "Specific Gravity Pref. " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "SGP_" + i.ToString()

                            For k = 0 To DataCnt
                                'lbl = New Label
                                'lbl.Style.Add("Width", DWidth)
                                txt = New TextBox
                                txt.ID = "SGP" + i.ToString() + "_" + (k + 1).ToString()
                                txt.Attributes.Add("OnkeyPress", "return clickButton(event)")
                                txt.CssClass = "PrefTextBox"
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                txt.Text = FormatNumber(dstblPref.Tables(k).Rows(0).Item("SGP" + i.ToString() + "").ToString(), 3).ToString()
                                If arrCaseID(k) <= 1000 Then
                                    txt.Style.Add("background-color", "#a6a6a6")
                                    txt.Enabled = False
                                End If
                                txt.MaxLength = 9
                                txt.Width = 70

                                tdInner.Controls.Add(txt)
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 11
                            tdInner = New TableCell
                            Title = "(" + dstblPref.Tables(0).Rows(0).Item("Title8").ToString() + "/" + dstblPref.Tables(0).Rows(0).Item("Title3").ToString() + ")"
                            LeftTdSetting(tdInner, "Weight/area " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "W_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                lbl.ID = "WPAREA_" + i.ToString() + "_" + (k + 1).ToString()
                                lbl.Text = FormatNumber(dstblSug.Tables(k).Rows(0).Item("WTPARA" + i.ToString() + "").ToString(), 3).ToString()
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 12
                            tdInner = New TableCell
                            Title = "(" + dstblPref.Tables(0).Rows(0).Item("Title2").ToString() + "/" + dstblPref.Tables(0).Rows(0).Item("Title3").ToString() + ")"
                            LeftTdSetting(tdInner, "Cost/area " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "COST_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                lbl.ID = "CPAREA_" + i.ToString() + "_" + (k + 1).ToString()
                                lbl.Text = FormatNumber(Cost(k, i - 1), 3).ToString()
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 13
                            tdInner = New TableCell
                            Title = ""
                            LeftTdSetting(tdInner, "Mfg. Department" + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "D_" + i.ToString()

                            For k = 0 To DataCnt
                                'lbl = New Label
                                ' lbl.Style.Add("Width", DWidth)
                                Link = New HyperLink
                                hid = New HiddenField
                                Link.ID = "hydep" + i.ToString() + "_" + (k + 1).ToString()
                                hid.ID = "hidDepId" + i.ToString() + "_" + (k + 1).ToString()
                                Link.Width = 100
                                Link.CssClass = "Link"
                                GetDeptDetails(Link, hid, dstblPref.Tables(k).Rows(0).Item("D" + i.ToString() + "").ToString(), arrCaseID(k).ToString(), dsDept)
                                If arrCaseID(k) <= 1000 Then
                                    Link.NavigateUrl = Nothing
                                End If
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                'GetDeptDetails(lbl, dstbl.Tables(k).Rows(0).Item("D" + i.ToString() + "").ToString(), arrCaseID(k).ToString())
                                'tdInner.Controls.Add(lbl)
                                tdInner.Controls.Add(Link)
                                tdInner.Controls.Add(hid)
                                trInner.Controls.Add(tdInner)
                            Next

                    End Select
                    tblComparision.Controls.Add(trInner)
                Next
            Next

            'Discrete Material
            For i = 1 To 3
                For j = 1 To 4
                    trInner = New TableRow
                    Select Case j
                        Case 1
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "<b>Discrete Materials" + i.ToString() + "</b>", trInner, "AlterNateColor4")
                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Center")
                                tdInner.Text = "&nbsp;"
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 2
                            tdInner = New TableCell
                            Dim dsMat As New DataSet
                            LeftTdSetting(tdInner, "Material", trInner, "AlterNateColor2")
                            trInner.ID = "DISM_" + i.ToString()
                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                dsMat = objGetData.GetDiscMaterials(CInt(dstblPref.Tables(k).Rows(0).Item("DISID" + i.ToString() + "").ToString()), "")
                                tdInner.Text = dsMat.Tables(0).Rows(0).Item("matDISde1").ToString()
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 3
                            tdInner = New TableCell
                            Title = "(" + dstblPref.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                            LeftTdSetting(tdInner, "Weight " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "DISW_" + i.ToString()
                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                tdInner.Text = FormatNumber(dstblPref.Tables(k).Rows(0).Item("DISW" + i.ToString() + "").ToString(), 4)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 4
                            tdInner = New TableCell
                            Title = "(" + dstblPref.Tables(0).Rows(0).Item("TITLE2").ToString() + "/unit)"
                            LeftTdSetting(tdInner, "Price " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "DISP_" + i.ToString()
                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                tdInner.Text = FormatNumber(dstblPref.Tables(k).Rows(0).Item("DISP" + i.ToString() + "").ToString(), 4)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)

                            Next

                    End Select
                    tblComparision.Controls.Add(trInner)
                Next
            Next

            For i = 1 To 1
                For j = 1 To 3
                    trInner = New TableRow()
                    Select Case j
                        Case 1
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "<b></b>", trInner, "AlterNateColor4")
                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Center")
                                tdInner.Text = "&nbsp;"
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 2

                            tdInner = New TableCell
                            tdInner.Width = "180"
                            LeftTdSetting(tdInner, "Include weight of discrete materials in P&L statements", trInner, "AlterNateColor1")
                            trInner.ID = "PCIN_" + i.ToString()
                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                If dstblPref.Tables(k).Rows(0).Item("discmatyn") = 1 Then
                                    tdInner.Text = "Yes"
                                Else
                                    tdInner.Text = "No"
                                End If
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 3
                            Dim cnt As Integer
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Printing Plates", trInner, "AlterNateColor2")
                            trInner.ID = "PCMQ_" + i.ToString()
                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                cnt = dstblPref.Tables(k).Rows(0).Item("plate")
                                Select Case cnt
                                    Case 0
                                        tdInner.Text = "not required"
                                    Case 1
                                        tdInner.Text = "produced by packaging supplier"
                                    Case 2
                                        tdInner.Text = "produced by outside supplier"
                                End Select

                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)
                            Next
                    End Select
                    tblComparision.Controls.Add(trInner)
                Next
            Next



        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetMaterialDetailsOLD(ByRef lbl As Label, ByVal MatId As Integer)
        Dim Ds As New DataSet
        Dim ObjGetdata As New E1GetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty

        Try

            Ds = ObjGetdata.GetMaterials(MatId, "", "")
            If Ds.Tables(0).Rows(0).Item("MATDES").ToString().Length > 25 Then
                'LinkMat.Font.Size = 8
            End If
            lbl.Text = Ds.Tables(0).Rows(0).Item("MATDES").ToString()
            lbl.ToolTip = Ds.Tables(0).Rows(0).Item("MATDES").ToString()


        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetMaterialDetails(ByRef LinkMat As HyperLink, ByVal hid As HiddenField, ByVal MatId As Integer)
        Dim Ds As New DataSet
        Dim ObjGetdata As New E1GetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty

        Try

            Ds = ObjGetdata.GetMaterials(MatId, "", "")
            If Ds.Tables(0).Rows(0).Item("MATDES").ToString().Length > 25 Then
                'LinkMat.Font.Size = 8
            End If
            LinkMat.Text = Ds.Tables(0).Rows(0).Item("MATDES").ToString()
            LinkMat.ToolTip = Ds.Tables(0).Rows(0).Item("MATDES").ToString()
            LinkMat.Attributes.Add("text-decoration", "none")
            hid.Value = MatId.ToString()
            Path = "../../Econ1/PopUp/GetMatPopUp.aspx?Des=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + LinkMat.ClientID + "&Id=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + hid.ClientID + ""
            LinkMat.NavigateUrl = "javascript:ShowPopWindow('" + Path + "')"

        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetMaterialDetailsNew(ByRef LinkMat As HyperLink, ByVal hid As HiddenField, ByVal MatId As Integer, ByVal linkGrade As String, ByVal hidGrade As String, _
                                        ByVal SG As String, ByVal CaseId As String, ByVal dsMat1 As DataSet)
        Dim Ds As New DataSet
        Dim ObjGetdata As New E1GetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty
        Dim dsEmat As New DataSet
        Dim str As String = ""
        Dim dv As New DataView
        Dim dt As New DataTable
        Dim dvMat1 As New DataView
        Dim dtMat1 As New DataTable
        Try
            dsEmat = ObjGetdata.GetEditMaterial(CaseId)

            dv = dsEmat.Tables(0).DefaultView

            If MatId <> 0 Then
                dv.RowFilter = "MATID=" + MatId.ToString()
                dt = dv.ToTable()

                If dt.Rows.Count > 0 Then
                    LinkMat.Text = dt.Rows(0).Item("MATDES").ToString().Replace("&#", "'")
                    LinkMat.ToolTip = dt.Rows(0).Item("MATDES").ToString()
                    LinkMat.Attributes.Add("text-decoration", "none")
                Else
                    'Ds = ObjGetdata.GetMaterials(MatId, "", "")
                    dvMat1 = dsMat1.Tables(0).DefaultView
                    dvMat1.RowFilter = "MATID = " + MatId.ToString()
                    dtMat1 = dvMat1.ToTable()

                    If dtMat1.Rows(0).Item("MATDES").ToString().Length > 25 Then
                        'LinkMat.Font.Size = 8
                    End If
                    LinkMat.Text = dtMat1.Rows(0).Item("MATDES").ToString()
                    LinkMat.ToolTip = dtMat1.Rows(0).Item("MATDES").ToString()
                    LinkMat.Attributes.Add("text-decoration", "none")
                End If
            Else
                'Ds = ObjGetdata.GetMaterials(MatId, "", "")
                dvMat1 = dsMat1.Tables(0).DefaultView
                dvMat1.RowFilter = "MATID = " + MatId.ToString()
                dtMat1 = dvMat1.ToTable()

                If dtMat1.Rows(0).Item("MATDES").ToString().Length > 25 Then
                    'LinkMat.Font.Size = 8
                End If
                LinkMat.Text = dtMat1.Rows(0).Item("MATDES").ToString()
                LinkMat.ToolTip = dtMat1.Rows(0).Item("MATDES").ToString()
                LinkMat.Attributes.Add("text-decoration", "none")
            End If

            hid.Value = MatId.ToString()
            Path = "../../Econ1/PopUp/GetMatPopUpGrade.aspx?Des=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + LinkMat.ClientID + "&Id=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + hid.ClientID + "&GradeDes=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + linkGrade + "&GradeId=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + hidGrade + "&SG=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + SG + "&CaseId=" + CaseId + ""
            LinkMat.NavigateUrl = "#"
            'LinkMat.NavigateUrl = "javascript:ShowMatPopWindow(this,'" + Path + "','" + ctlContentPlaceHolder.ClientID.ToString() + "_" + hid.ClientID + "')"
            LinkMat.Attributes.Add("onClick", "javascript:return ShowMatPopWindow(this,'" + Path + "','" + ctlContentPlaceHolder.ClientID.ToString() + "_" + hid.ClientID + "')")

        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
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

    Protected Sub GetDeptDetails(ByRef LinkDep As HyperLink, ByVal hid As HiddenField, ByVal ProcId As Integer, ByVal CaseId As Integer, ByVal dsDept As DataSet)
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
    Public Shared Function UpdateCase(ByVal Cost() As String, ByVal CaseID As String, ByVal index As String, ByVal MatIds() As String, ByVal Thick() As String, ByVal RECY() As String, ByVal EXPR() As String, ByVal SGP() As String, ByVal DEP() As String) As Array
        Try
            Dim WPT(10) As String
            Dim ObjGetData As New E1GetData.Selectdata()
            Dim EconConnection As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            Dim StrSqlUpadate As String = ""
            Dim StrSqlIUpadate As String = ""
            Dim odbUtil As New DBUtil()
            Dim Totalprice As Double
            Dim TotalThicKness As Double
            Dim dts As New DataSet()
            Dim ObjE3GetData As New E3GetData.Selectdata()
            Dim dsD As New DataSet
            Dim PageInfo As String = ""
            Dim dseffdatefrm As New DataSet()
            dseffdatefrm = ObjGetData.GetEffdateFrm(CaseID)

            dts = ObjGetData.GetPref(CaseID)


            Dim Convwt As String = dts.Tables(0).Rows(0).Item("convwt")
            Dim Curr As String '= dts.Tables(0).Rows(0).Item("curr")
            If dseffdatefrm.Tables(0).Rows(0).Item("EFFDATEFRM").ToString() <> "" Then
                Curr = dts.Tables(0).Rows(0).Item("curravg")
            Else
                Curr = dts.Tables(0).Rows(0).Item("curr")
            End If
            Dim Convthick As String = dts.Tables(0).Rows(0).Item("convthick")
            StrSqlUpadate = "UPDATE MATERIALINPUT SET"
            For Mat = 0 To 9
                Totalprice = CDbl(Cost(Mat) * Convwt / Curr)
                TotalThicKness = CDbl(Thick(Mat) / Convthick)
                StrSqlIUpadate = StrSqlIUpadate + " S" + (Mat + 1).ToString() + "=" + Totalprice.ToString() + ","
                StrSqlIUpadate = StrSqlIUpadate + " M" + (Mat + 1).ToString() + "=" + MatIds(Mat) + ","
                StrSqlIUpadate = StrSqlIUpadate + " T" + (Mat + 1).ToString() + " = " + TotalThicKness.ToString() + ","
                StrSqlIUpadate = StrSqlIUpadate + " R" + (Mat + 1).ToString() + " = " + RECY(Mat).ToString() + ","
                StrSqlIUpadate = StrSqlIUpadate + " E" + (Mat + 1).ToString() + " = " + EXPR(Mat).ToString() + ","
                StrSqlIUpadate = StrSqlIUpadate + " SG" + (Mat + 1).ToString() + " = " + SGP(Mat).ToString() + ","
                StrSqlIUpadate = StrSqlIUpadate + " D" + (Mat + 1).ToString() + " = " + DEP(Mat).ToString() + ","
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

            'Dim ds As New DataSet
            Dim l As Integer = 0
            'ds = ObjGetData.GetExtrusionDetails(CaseID)
            Dim dsPref, dsSug As New DataSet
            dsPref = ObjGetData.GetExtrusionDetailsBarrP(CaseID)

            dsSug = ObjGetData.GetExtrusionDetailsBarrS(CaseID)
           
            For i = 0 To 9
                If dsPref.Tables(0).Rows(0).Item("PRP" + (i + 1).ToString() + "").ToString() <> "" Then
                    If dsPref.Tables(0).Rows(0).Item("PRP" + (i + 1).ToString() + "").ToString() <> "0" Then
                        WPT(i) = FormatNumber(CDbl(dsPref.Tables(0).Rows(0).Item("PRP" + (i + 1).ToString()).ToString()) * CDbl(dsSug.Tables(0).Rows(0).Item("WTPARA" + (i + 1).ToString()).ToString()), 3)
                    Else
                        WPT(i) = FormatNumber(CDbl(dsSug.Tables(0).Rows(0).Item("PRS" + (i + 1).ToString()).ToString()) * CDbl(dsSug.Tables(0).Rows(0).Item("WTPARA" + (i + 1).ToString()).ToString()), 3)
                    End If
                Else
                    WPT(i) = FormatNumber(CDbl(dsSug.Tables(0).Rows(0).Item("PRS" + (i + 1).ToString()).ToString()) * CDbl(dsSug.Tables(0).Rows(0).Item("WTPARA" + (i + 1).ToString()).ToString()), 3)
                End If
                If i = 0 Then
                    WPT(i) = WPT(i) + "#" + index.ToString()
                End If
                WPT(i) = WPT(i) + "-" + FormatNumber(CDbl(dsPref.Tables(0).Rows(0).Item("PRP" + (i + 1).ToString()).ToString()), 4) + "-" + FormatNumber(dsSug.Tables(0).Rows(0).Item("WTPARA" + (i + 1).ToString() + "").ToString(), 3) + "-" + FormatNumber(dsSug.Tables(0).Rows(0).Item("PRS" + (i + 1).ToString() + "").ToString(), 4) + "-" + FormatNumber(dsSug.Tables(0).Rows(0).Item("SGS" + (i + 1).ToString() + "").ToString(), 3)
            Next

            Return WPT
        Catch ex As Exception

        End Try
    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function UpdateAllCases(ByVal Cost() As String, ByVal CaseID As String, ByVal index As String, ByVal flag As String, ByVal MatIds() As String, ByVal Thick() As String, ByVal RECY() As String, ByVal EXPR() As String, ByVal SGP() As String, ByVal DEP() As String) As Array
        Try
            Dim WPT(10) As String
            Dim ObjGetData As New E1GetData.Selectdata()
            Dim ObjE3GetData As New E3GetData.Selectdata()
            Dim EconConnection As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            Dim StrSqlUpadate As String = ""
            Dim StrSqlIUpadate As String = ""
            Dim odbUtil As New DBUtil()
            Dim Totalprice As String
            Dim dts As New DataSet()
            Dim dsD As New DataSet
            Dim PageInfo As String = ""
            Dim TotalThicKness As Double
            Dim dseffdatefrm As New DataSet()
            dseffdatefrm = ObjGetData.GetEffdateFrm(CaseID)

            dts = ObjGetData.GetPref(CaseID)
           


             Dim Convwt As String = dts.Tables(0).Rows(0).Item("convwt")
            Dim Curr As String '= dts.Tables(0).Rows(0).Item("curr")
            If dseffdatefrm.Tables(0).Rows(0).Item("EFFDATEFRM").ToString() <> "" Then
                Curr = dts.Tables(0).Rows(0).Item("curravg")
            Else
                Curr = dts.Tables(0).Rows(0).Item("curr")
            End If
            Dim Convthick As String = dts.Tables(0).Rows(0).Item("convthick")
            StrSqlUpadate = "UPDATE MATERIALINPUT SET"
            For Mat = 0 To 9
                Totalprice = CDbl(Cost(Mat) * Convwt / Curr)
                TotalThicKness = CDbl(Thick(Mat) / Convthick)
                StrSqlIUpadate = StrSqlIUpadate + " S" + (Mat + 1).ToString() + "=" + Totalprice.ToString() + ","
                StrSqlIUpadate = StrSqlIUpadate + " M" + (Mat + 1).ToString() + "=" + MatIds(Mat) + ","
                StrSqlIUpadate = StrSqlIUpadate + " T" + (Mat + 1).ToString() + " = " + TotalThicKness.ToString() + ","
                StrSqlIUpadate = StrSqlIUpadate + " R" + (Mat + 1).ToString() + " = " + RECY(Mat).ToString() + ","
                StrSqlIUpadate = StrSqlIUpadate + " E" + (Mat + 1).ToString() + " = " + EXPR(Mat).ToString() + ","
                StrSqlIUpadate = StrSqlIUpadate + " SG" + (Mat + 1).ToString() + " = " + SGP(Mat).ToString() + ","
                StrSqlIUpadate = StrSqlIUpadate + " D" + (Mat + 1).ToString() + " = " + DEP(Mat).ToString() + ","
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

            ' Dim ds As New DataSet
            Dim l As Integer = 0
            ' ds = ObjGetData.GetExtrusionDetails(CaseID)
            Dim dsPref, dsSug As New DataSet
            dsPref = ObjGetData.GetExtrusionDetailsBarrP(CaseID)

            dsSug = ObjGetData.GetExtrusionDetailsBarrS(CaseID)
            'For i = 0 To 9
            '    If ds.Tables(0).Rows(0).Item("PRP" + (i + 1).ToString() + "").ToString() <> "" Then
            '        If ds.Tables(0).Rows(0).Item("PRP" + (i + 1).ToString() + "").ToString() <> "0" Then
            '            WPT(i) = FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("PRP" + (i + 1).ToString()).ToString()) * CDbl(ds.Tables(0).Rows(0).Item("WTPARA" + (i + 1).ToString()).ToString()), 3)
            '        Else
            '            WPT(i) = FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("PRS" + (i + 1).ToString()).ToString()) * CDbl(ds.Tables(0).Rows(0).Item("WTPARA" + (i + 1).ToString()).ToString()), 3)
            '        End If
            '    Else
            '        WPT(i) = FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("PRS" + (i + 1).ToString()).ToString()) * CDbl(ds.Tables(0).Rows(0).Item("WTPARA" + (i + 1).ToString()).ToString()), 3)
            '    End If
            '    If i = 0 Then
            '        WPT(i) = WPT(i) + "#" + index.ToString() + "#" + flag.ToString()
            '    End If
            '    WPT(i) = WPT(i) + "-" + FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("PRP" + (i + 1).ToString()).ToString()), 3) + "-" + FormatNumber(ds.Tables(0).Rows(0).Item("WTPARA" + (i + 1).ToString() + "").ToString(), 3) + "-" + FormatNumber(ds.Tables(0).Rows(0).Item("PRS" + (i + 1).ToString() + "").ToString(), 3) + "-" + FormatNumber(ds.Tables(0).Rows(0).Item("SGS" + (i + 1).ToString() + "").ToString(), 3)
            'Next

            For i = 0 To 9
                If dsPref.Tables(0).Rows(0).Item("PRP" + (i + 1).ToString() + "").ToString() <> "" Then
                    If dsPref.Tables(0).Rows(0).Item("PRP" + (i + 1).ToString() + "").ToString() <> "0" Then
                        WPT(i) = FormatNumber(CDbl(dsPref.Tables(0).Rows(0).Item("PRP" + (i + 1).ToString()).ToString()) * CDbl(dsSug.Tables(0).Rows(0).Item("WTPARA" + (i + 1).ToString()).ToString()), 3)
                    Else
                        WPT(i) = FormatNumber(CDbl(dsSug.Tables(0).Rows(0).Item("PRS" + (i + 1).ToString()).ToString()) * CDbl(dsSug.Tables(0).Rows(0).Item("WTPARA" + (i + 1).ToString()).ToString()), 3)
                    End If
                Else
                    WPT(i) = FormatNumber(CDbl(dsSug.Tables(0).Rows(0).Item("PRS" + (i + 1).ToString()).ToString()) * CDbl(dsSug.Tables(0).Rows(0).Item("WTPARA" + (i + 1).ToString()).ToString()), 3)
                End If
                If i = 0 Then
                    WPT(i) = WPT(i) + "#" + index.ToString() + "#" + flag.ToString()
                End If
                WPT(i) = WPT(i) + "-" + FormatNumber(CDbl(dsPref.Tables(0).Rows(0).Item("PRP" + (i + 1).ToString()).ToString()), 4) + "-" + FormatNumber(dsSug.Tables(0).Rows(0).Item("WTPARA" + (i + 1).ToString() + "").ToString(), 3) + "-" + FormatNumber(dsSug.Tables(0).Rows(0).Item("PRS" + (i + 1).ToString() + "").ToString(), 4) + "-" + FormatNumber(dsSug.Tables(0).Rows(0).Item("SGS" + (i + 1).ToString() + "").ToString(), 3)
            Next

            Return WPT
        Catch ex As Exception

        End Try
    End Function

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

End Class
