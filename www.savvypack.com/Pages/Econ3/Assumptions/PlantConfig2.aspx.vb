Imports System.Data
Imports System.Data.OleDb
Imports System
Imports E3GetData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Econ3_Assumptions_PlantConfig2
    Inherits System.Web.UI.Page
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

            Try
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
        Dim btn As New Button
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
            Dim lbl2 As New Label
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

            Dim hid As New HiddenField
            Dim Link As New HyperLink
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
                ds = objGetData.GetPlantConfig2Details(arrCaseID(i))
                ds.Tables(0).TableName = arrCaseID(i).ToString()
                dstbl.Tables.Add(ds.Tables(arrCaseID(i).ToString()).Copy())
            Next

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
            For i = 1 To 1
                For j = 1 To 15
                    trInner = New TableRow()

                    Select Case j
                        Case 1
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Space Type", trInner, "AlterNateColor4")
                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                tdInner.Text = "Area"
                                InnerTdSetting(tdInner, "", "Right")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 2
                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("TITLE7").ToString() + ")"
                            LeftTdSetting(tdInner, "Production " + Title + "", trInner, "")
                            trInner.ID = "SPD_" + i.ToString()
                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                lbl = New Label()
                                lbl.Text = FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("AR1").ToString()), 3).ToString()
                                tdInner.Controls.Add(lbl)
                                InnerTdSetting(tdInner, "", "Right")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 3
                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("TITLE7").ToString() + ")"
                            LeftTdSetting(tdInner, "Warehouse " + Title + "", trInner, "")
                            trInner.ID = "SWH_" + i.ToString()
                            For k = 0 To DataCnt
                                txt = New TextBox
                                txt.MaxLength = 9
                                txt.Width = 70
                                txt.ID = "txtSWH_" + (k + 1).ToString()
                                'txt.ID = "txtSWH_" + i.ToString() + "_" + (k + 1).ToString()
                                txt.Attributes.Add("OnkeyPress", "return clickButton(event)")
                                'txt.Style.Add("Width", "100px")
                                txt.CssClass = "PrefTextBox"
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                If dstbl.Tables(k).Rows(0).Item("AR2".ToString() + "").ToString() <> "" Then
                                    txt.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("AR2".ToString() + "").ToString(), 3).ToString()
                                Else
                                    txt.Text = "0.000"
                                End If
                                If arrCaseID(k) <= 1000 Then
                                    txt.Style.Add("background-color", "#a6a6a6")
                                    txt.Enabled = False
                                End If
                                tdInner.Controls.Add(txt)
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 4
                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("TITLE7").ToString() + ")"
                            LeftTdSetting(tdInner, "Office " + Title + "", trInner, "")
                            trInner.ID = "SOF_" + i.ToString()
                            For k = 0 To DataCnt
                                txt = New TextBox
                                txt.MaxLength = 9
                                txt.Width = 70
                                txt.ID = "txtSOF_" + (k + 1).ToString()
                                txt.Attributes.Add("OnkeyPress", "return clickButton(event)")
                                'txt.Style.Add("Width", "100px")
                                txt.CssClass = "PrefTextBox"
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                If dstbl.Tables(k).Rows(0).Item("AR3".ToString() + "").ToString() <> "" Then
                                    txt.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("AR3".ToString() + "").ToString(), 3).ToString()
                                Else
                                    txt.Text = "0.000"
                                End If
                                If arrCaseID(k) <= 1000 Then
                                    txt.Style.Add("background-color", "#a6a6a6")
                                    txt.Enabled = False
                                End If
                                tdInner.Controls.Add(txt)
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 5
                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("TITLE7").ToString() + ")"
                            LeftTdSetting(tdInner, "Support " + Title + "", trInner, "")
                            trInner.ID = "SOS_" + i.ToString()
                            For k = 0 To DataCnt
                                txt = New TextBox
                                txt.MaxLength = 9
                                txt.Width = 70
                                txt.ID = "txtSOS_" + (k + 1).ToString()
                                ' txt.ID = "txtSOS_" + i.ToString() + "_" + (k + 1).ToString()
                                txt.Attributes.Add("OnkeyPress", "return clickButton(event)")
                                'txt.Style.Add("Width", "100px")
                                txt.CssClass = "PrefTextBox"
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                If dstbl.Tables(k).Rows(0).Item("AR4".ToString() + "").ToString() <> "" Then
                                    txt.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("AR4".ToString() + "").ToString(), 3).ToString()
                                Else
                                    txt.Text = "0.000"
                                End If
                                If arrCaseID(k) <= 1000 Then
                                    txt.Style.Add("background-color", "#a6a6a6")
                                    txt.Enabled = False
                                End If
                                tdInner.Controls.Add(txt)
                                trInner.Controls.Add(tdInner)
                            Next


                        Case 6
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("TITLE7").ToString() + ")"
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "<b>Total</b> " + Title + "", trInner, "")
                            trInner.ID = "ST0T_" + i.ToString()
                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                lbl = New Label()
                                'lbl.ID = "TotalST_" + i.ToString() + "_" + (k + 1).ToString()
                                lbl.ID = "TotalST_" + (k + 1).ToString()
                                lbl.Text = FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("ARTOT").ToString()), 3).ToString()
                                tdInner.Controls.Add(lbl)
                                InnerTdSetting(tdInner, "", "Right")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 7
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Lease Type", trInner, "AlterNateColor4")
                            For k = 0 To DataCnt
                                Dim str As String = String.Empty
                                str = str + "<table cellpadding='3' border='0' width='100%'>"
                                str = str + "<tr style='text-align:right;'>"
                                str = str + "<td  width='35%'>Lease Cost Sugg.</td>"
                                str = str + "<td  width='35%'>Lease Cost Pref.</td>"
                                str = str + "<td  width='30%'>Total Lease</td>"
                                str = str + "</tr>"
                                str = str + "<tr style='text-align:right;'>"
                                str = str + "<td  width='35%'>(" + dstbl.Tables(k).Rows(0).Item("TITLE2") + "/" + dstbl.Tables(k).Rows(0).Item("TITLE7") + ")</td>"
                                str = str + "<td  width='35%'>(" + dstbl.Tables(k).Rows(0).Item("TITLE2") + "/" + dstbl.Tables(k).Rows(0).Item("TITLE7") + ")</td>"
                                str = str + "<td  width='30%'>(" + dstbl.Tables(k).Rows(0).Item("TITLE2") + ")</td>"
                                str = str + "</tr>"
                                str = str + "</table>"
                                tdInner = New TableCell
                                tdInner.Text = str
                                InnerTdSetting(tdInner, "", "Right")
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 8
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Prod. High Bay ", trInner, "")
                            trInner.ID = "LPH_" + i.ToString()

                            For k = 0 To DataCnt
                                Dim str As String = String.Empty
                                str = str + "<table cellpadding='3' border='0' width='100%'>"
                                str = str + "<tr style='text-align:right'>"
                                str = str + "<td width='32%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("SUG1").ToString()), 3).ToString() + "</td>"
                                If arrCaseID(k) <= 1000 Then
                                    str = str + "<td width='28%'><input type='text' OnkeyPress='return clickButton(event)' style='background-color:#a6a6a6;' runat='server' id='LPH" + (k + 1).ToString() + "' class='PrefTextBox' runat='server' name='LPH" + (k + 1).ToString() + "' value='" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("PREF1").ToString()), 3).ToString() + "' disabled/></td>"
                                Else
                                    str = str + "<td width='28%'><input type='text' maxlength='10' OnkeyPress='return clickButton(event)' runat='server' id='LPH" + (k + 1).ToString() + "' class='PrefTextBox' runat='server' name='LPH" + (k + 1).ToString() + "' value='" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("PREF1").ToString()), 3).ToString() + "'/></td>"
                                End If
                                str = str + "<td width='40%'><label id='PRODHIGHBAY" + (k + 1).ToString() + "' runat='server'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("PTOT1").ToString()), 0).ToString() + "</td>"
                                'str = str + "<td width='40%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("PTOT1").ToString()), 0).ToString() + "</td>"
                                str = str + "</tr>"
                                str = str + "</table>"
                                tdInner = New TableCell
                                tdInner.Text = str
                                InnerTdSetting(tdInner, "", "Center")
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 9
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Prod. Partial High Bay", trInner, "")
                            trInner.ID = "LPPH_" + i.ToString()
                            For k = 0 To DataCnt
                                Dim str As String = String.Empty
                                str = str + "<table cellpadding='3' border='0' width='100%'>"
                                str = str + "<tr style='text-align:right'>"
                                str = str + "<td width='32%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("SUG2").ToString()), 3).ToString() + "</td>"
                                If arrCaseID(k) <= 1000 Then
                                    str = str + "<td width='28%'><input type='text' OnkeyPress='return clickButton(event)' style='background-color:#a6a6a6;' runat='server' id='LPPH" + (k + 1).ToString() + "' class='PrefTextBox' runat='server' name='LPPH" + (k + 1).ToString() + "' value='" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("PREF2").ToString()), 3).ToString() + "' disabled/></td>"
                                Else
                                    str = str + "<td width='28%'><input type='text' maxlength='10' OnkeyPress='return clickButton(event)' runat='server' id='LPPH" + (k + 1).ToString() + "' class='PrefTextBox' runat='server' name='LPPH" + (k + 1).ToString() + "' value='" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("PREF2").ToString()), 3).ToString() + "'/></td>"
                                End If
                                str = str + "<td width='40%'><label id='PRODPARHIGHBAY" + (k + 1).ToString() + "' runat='server'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("PTOT2").ToString()), 0).ToString() + "</td>"
                                ' str = str + "<td width='40%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("PTOT2").ToString()), 0).ToString() + "</td>"


                                str = str + "</tr>"
                                str = str + "</table>"
                                tdInner = New TableCell
                                tdInner.Text = str
                                InnerTdSetting(tdInner, "", "Center")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 10
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Prod. Standard", trInner, "")
                            trInner.ID = "LPS_" + i.ToString()
                            For k = 0 To DataCnt
                                Dim str As String = String.Empty
                                str = str + "<table cellpadding='3' border='0' width='100%'>"
                                str = str + "<tr style='text-align:right'>"
                                str = str + "<td width='32%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("SUG3").ToString()), 3).ToString() + "</td>"
                                If arrCaseID(k) <= 1000 Then
                                    str = str + "<td width='28%'><input type='text' OnkeyPress='return clickButton(event)' style='background-color:#a6a6a6;' runat='server' id='LPS" + (k + 1).ToString() + "' class='PrefTextBox' runat='server' name='LPS" + (k + 1).ToString() + "' value='" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("PREF3").ToString()), 3).ToString() + "' disabled/></td>"
                                Else
                                    str = str + "<td width='28%'><input type='text' maxlength='10' OnkeyPress='return clickButton(event)' runat='server' id='LPS" + (k + 1).ToString() + "' class='PrefTextBox' runat='server' name='LPS" + (k + 1).ToString() + "' value='" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("PREF3").ToString()), 3).ToString() + "'/></td>"
                                End If
                                ' str = str + "<td width='35%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("PREF3").ToString()), 3).ToString() + "</td>"
                                str = str + "<td width='40%'><label id='PRODSTAND" + (k + 1).ToString() + "' runat='server'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("PTOT3").ToString()), 0).ToString() + "</td>"


                                str = str + "</tr>"
                                str = str + "</table>"
                                tdInner = New TableCell
                                tdInner.Text = str
                                InnerTdSetting(tdInner, "", "Center")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 11
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Prod. Total", trInner, "")
                            trInner.ID = "LPT_" + i.ToString()
                            For k = 0 To DataCnt
                                Dim str As String = String.Empty
                                str = str + "<table cellpadding='3' border='0' width='100%'>"
                                str = str + "<tr style='text-align:right'>"
                                str = str + "<td width='35%'></td>"
                                str = str + "<td width='35%'></td>"
                                'str = str + "<td width='35%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("SUG4").ToString()), 0).ToString() + "</td>"
                                'str = str + "<td width='35%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("PREF4").ToString()), 0).ToString() + "</td>"
                                str = str + "<td width='33%'><label id='PRODTOTAL" + (k + 1).ToString() + "' runat='server'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("PTOT4").ToString()), 0).ToString() + "<label/></td>"
                                str = str + "</tr>"
                                str = str + "</table>"
                                tdInner = New TableCell
                                tdInner.Text = str
                                InnerTdSetting(tdInner, "", "Center")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 12
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Warehouse", trInner, "")
                            trInner.ID = "LWH_" + i.ToString()
                            For k = 0 To DataCnt
                                Dim str As String = String.Empty
                                str = str + "<table cellpadding='3' border='0' width='100%'>"
                                str = str + "<tr style='text-align:right'>"
                                str = str + "<td width='32%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("SUG4").ToString()), 3).ToString() + "</td>"
                                If arrCaseID(k) <= 1000 Then
                                    str = str + "<td width='28%'><input type='text' OnkeyPress='return clickButton(event)' style='background-color:#a6a6a6;' runat='server' id='LWH" + (k + 1).ToString() + "' class='PrefTextBox' runat='server' name='LWH" + (k + 1).ToString() + "' value='" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("PREF4").ToString()), 3).ToString() + "' disabled/></td>"
                                Else
                                    str = str + "<td width='28%'><input type='text' maxlength='10' OnkeyPress='return clickButton(event)' runat='server' id='LWH" + (k + 1).ToString() + "' class='PrefTextBox' runat='server' name='LWH" + (k + 1).ToString() + "' value='" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("PREF4").ToString()), 3).ToString() + "'/></td>"
                                End If
                                'str = str + "<td width='33%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("PTOT5").ToString()), 0).ToString() + "</td>"
                                str = str + "<td width='40%'><label id='WAREHOUSE" + (k + 1).ToString() + "' runat='server'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("PTOT5").ToString()), 0).ToString() + "<label/></td>"

                                str = str + "</tr>"
                                str = str + "</table>"
                                tdInner = New TableCell
                                tdInner.Text = str
                                InnerTdSetting(tdInner, "", "Center")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 13
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Office", trInner, "")
                            trInner.ID = "LOF_" + i.ToString()
                            For k = 0 To DataCnt
                                Dim str As String = String.Empty
                                str = str + "<table cellpadding='3' border='0' width='100%'>"
                                str = str + "<tr style='text-align:right'>"
                                str = str + "<td width='32%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("SUG5").ToString()), 3).ToString() + "</td>"
                                If arrCaseID(k) <= 1000 Then
                                    str = str + "<td width='28%'><input type='text' OnkeyPress='return clickButton(event)' style='background-color:#a6a6a6;' runat='server' id='LOF" + (k + 1).ToString() + "' class='PrefTextBox' runat='server' name='LOF" + (k + 1).ToString() + "' value='" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("PREF5").ToString()), 3).ToString() + "' disabled/></td>"
                                Else
                                    str = str + "<td width='28%'><input type='text' maxlength='10' OnkeyPress='return clickButton(event)' runat='server' id='LOF" + (k + 1).ToString() + "' class='PrefTextBox' runat='server' name='LOF" + (k + 1).ToString() + "' value='" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("PREF5").ToString()), 3).ToString() + "'/></td>"
                                End If
                                'str = str + "<td width='33%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("PTOT6").ToString()), 0).ToString() + "</td>"
                                str = str + "<td width='40%'><label id='OFFICE" + (k + 1).ToString() + "' runat='server'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("PTOT6").ToString()), 0).ToString() + "<label/></td>"

                                str = str + "</tr>"
                                str = str + "</table>"
                                tdInner = New TableCell
                                tdInner.Text = str
                                InnerTdSetting(tdInner, "", "Center")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 14
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Support", trInner, "")
                            trInner.ID = "LOS_" + i.ToString()
                            For k = 0 To DataCnt
                                Dim str As String = String.Empty
                                str = str + "<table cellpadding='3' border='0' width='100%'>"
                                str = str + "<tr style='text-align:right'>"
                                str = str + "<td width='32%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("SUG6").ToString()), 3).ToString() + "</td>"
                                If arrCaseID(k) <= 1000 Then
                                    str = str + "<td width='28%'><input type='text' OnkeyPress='return clickButton(event)' style='background-color:#a6a6a6;' runat='server' id='LOS" + (k + 1).ToString() + "' class='PrefTextBox' runat='server' name='LOS" + (k + 1).ToString() + "' value='" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("PREF6").ToString()), 3).ToString() + "' disabled/></td>"
                                Else
                                    str = str + "<td width='28%'><input type='text' maxlength='10' OnkeyPress='return clickButton(event)' runat='server' id='LOS" + (k + 1).ToString() + "' class='PrefTextBox' runat='server' name='LOS" + (k + 1).ToString() + "' value='" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("PREF6").ToString()), 3).ToString() + "'/></td>"
                                End If
                                'str = str + "<td width='33%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("PTOT7").ToString()), 0).ToString() + "</td>"
                                str = str + "<td width='40%'><label id='SUPPORT" + (k + 1).ToString() + "' runat='server'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("PTOT7").ToString()), 0).ToString() + "<label/></td>"

                                str = str + "</tr>"
                                str = str + "</table>"
                                tdInner = New TableCell
                                tdInner.Text = str
                                InnerTdSetting(tdInner, "", "Center")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 15
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Total", trInner, "")
                            trInner.ID = "LT0T_" + i.ToString()
                            For k = 0 To DataCnt
                                Dim str As String = String.Empty
                                str = str + "<table cellpadding='3' border='0' width='100%'>"
                                str = str + "<tr style='text-align:right'>"
                                str = str + "<td width='35%'></td>"
                                str = str + "<td width='35%'></td>"
                                str = str + "<td width='33%'><label id='TOTALLT" + (k + 1).ToString() + "' runat='server'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("LEASETOTAL").ToString()), 0).ToString() + "<label/></td>"
                                str = str + "</tr>"
                                str = str + "</table>"
                                tdInner = New TableCell
                                tdInner.Text = str
                                InnerTdSetting(tdInner, "", "Center")
                                trInner.Controls.Add(tdInner)
                            Next




                    End Select
                    If j = 1 Or j = 7 Then
                    Else
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
            Td.Width = 250
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

    Protected Sub GetProductDetails(ByRef lbl As Label, ByVal FromatId As Integer)
        Dim Ds As New DataSet
        Dim ObjGetdata As New E1GetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty
        Try

            Ds = ObjGetdata.GetProductFormt(FromatId, "", "")
            lbl.Text = Ds.Tables(0).Rows(0).Item("FormatDes").ToString()

        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetDeptDetails(ByRef lbl As Label, ByVal ProcId As Integer, ByVal CaseId As String)
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

    <System.Web.Services.WebMethod()> _
    Public Shared Function UpdateCase(ByVal CaseID As String, ByVal index As String, ByVal LPH As String, ByVal LPPH As String, ByVal LPS As String, ByVal LWH As String, ByVal LOF As String, ByVal LOS As String, ByVal SWF As String, ByVal SOF As String, ByVal SOS As String) As String
        Try
            Dim AREA(2) As String
            Dim PrefLease(5) As String
            Dim odbUtil As New DBUtil()
            Dim ObjGetData As New E1GetData.Selectdata()
            Dim EconConnection As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            'Preferences
            Dim dts As New DataSet()
            dts = ObjGetData.GetPref(CaseID)

            'Getting Values From Database assign to variable 
            Dim Convthick As String = dts.Tables(0).Rows(0).Item("convthick")
            Dim Convwt As String = dts.Tables(0).Rows(0).Item("convwt")
            Dim Curr As String = dts.Tables(0).Rows(0).Item("curr")
            Dim CONVAREA2 As String = dts.Tables(0).Rows(0).Item("CONVAREA2")
            Dim UNITS As String = dts.Tables(0).Rows(0).Item("units")
            Dim i As New Integer
            Dim StrSqlUpadate As String = ""
            Dim StrSqlIUpadate As String = ""
            Dim ValTotal As String

            For i = 2 To 4
                If i = 2 Then
                    AREA(i - 2) = SWF.ToString()
                ElseIf i = 3 Then
                    AREA(i - 2) = SOF.ToString()
                ElseIf i = 4 Then
                    AREA(i - 2) = SOS.ToString()
                End If
                'AREA(i - 2) = Request.Form("ctl00$Econ1ContentPlaceHolder$AR" + i.ToString() + "")

            Next

            For i = 0 To 5
                If i = 0 Then
                    PrefLease(i) = LPH.ToString()
                ElseIf i = 1 Then
                    PrefLease(i) = LPPH.ToString()
                ElseIf i = 2 Then
                    PrefLease(i) = LPS.ToString()
                ElseIf i = 3 Then
                    PrefLease(i) = LWH.ToString()
                ElseIf i = 4 Then
                    PrefLease(i) = LOF.ToString()
                ElseIf i = 5 Then
                    PrefLease(i) = LOS.ToString()
                End If
                'PrefLease(i) = Request.Form("ctl00$Econ1ContentPlaceHolder$PREF" + (i + 1).ToString() + "")
            Next


            StrSqlUpadate = "UPDATE plantSPACE SET"
            For i = 2 To 4
                If i = 2 Then
                    SWF = CDbl(AREA(i - 2).Replace(",", "") / CONVAREA2)
                    StrSqlIUpadate = StrSqlIUpadate + " M" + i.ToString() + " = " + SWF.ToString() + ","
                ElseIf i = 3 Then
                    SOF = CDbl(AREA(i - 2).Replace(",", "") / CONVAREA2)
                    StrSqlIUpadate = StrSqlIUpadate + " M" + i.ToString() + " = " + SOF.ToString() + ","

                ElseIf i = 4 Then
                    SOS = CDbl(AREA(i - 2).Replace(",", "") / CONVAREA2)
                    StrSqlIUpadate = StrSqlIUpadate + " M" + i.ToString() + " = " + SOS.ToString() + ","

                End If
            Next
            StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
            StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
            StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
            odbUtil.UpIns(StrSqlUpadate, EconConnection)

            'Updating Preffered Cost

            StrSqlUpadate = ""
            StrSqlIUpadate = ""
            StrSqlUpadate = "UPDATE plantSPACE SET"
            For i = 0 To 5

                'For Production highbay
                If i = 0 Then
                    If UNITS = 0 Then
                        LPH = CDbl(PrefLease(i).Replace(",", "") / Curr)
                        StrSqlIUpadate = StrSqlIUpadate + " PRODUCTIONLEASEHB" + " = " + LPH.ToString() + ","
                    Else
                        LPH = CDbl(PrefLease(i).Replace(",", "") * CONVAREA2 / Curr)
                        StrSqlIUpadate = StrSqlIUpadate + " PRODUCTIONLEASEHB" + " = " + LPH.ToString() + ","
                    End If

                    'For Production Partial Highbay                       
                ElseIf i = 1 Then
                    If UNITS = 0 Then
                        LPPH = CDbl(PrefLease(i).Replace(",", "") / Curr)
                        StrSqlIUpadate = StrSqlIUpadate + " PRODUCTIONLEASEPHB" + " = " + LPPH.ToString() + ","
                    Else
                        LPPH = CDbl(PrefLease(i).Replace(",", "") * CONVAREA2 / Curr)
                        StrSqlIUpadate = StrSqlIUpadate + " PRODUCTIONLEASEPHB" + " = " + LPPH.ToString() + ","
                    End If

                    'For Production Standard
                ElseIf i = 2 Then
                    If UNITS = 0 Then
                        LPS = CDbl(PrefLease(i).Replace(",", "") / Curr)
                        StrSqlIUpadate = StrSqlIUpadate + " PRODUCTIONLEASE" + " = " + LPS.ToString() + ","
                    Else
                        LPS = CDbl(PrefLease(i).Replace(",", "") * CONVAREA2 / Curr)
                        StrSqlIUpadate = StrSqlIUpadate + " PRODUCTIONLEASE" + " = " + LPS.ToString() + ","
                    End If

                    'For Warehouse
                ElseIf i = 3 Then
                    If UNITS = 0 Then
                        LWH = CDbl(PrefLease(i).Replace(",", "") / Curr)
                        StrSqlIUpadate = StrSqlIUpadate + " WAREHOUSELEASE" + " = " + LWH.ToString() + ","
                    Else
                        LWH = CDbl(PrefLease(i).Replace(",", "") * CONVAREA2 / Curr)
                        StrSqlIUpadate = StrSqlIUpadate + " WAREHOUSELEASE" + " = " + LWH.ToString() + ","
                    End If

                    'For Office
                ElseIf i = 4 Then
                    If UNITS = 0 Then
                        LOF = CDbl(PrefLease(i).Replace(",", "") / Curr)
                        StrSqlIUpadate = StrSqlIUpadate + " OFFICELEASE" + " = " + LOF.ToString() + ","
                    Else
                        LOF = CDbl(PrefLease(i).Replace(",", "") * CONVAREA2 / Curr)
                        StrSqlIUpadate = StrSqlIUpadate + " OFFICELEASE" + " = " + LOF.ToString() + ","
                    End If

                    'For Support
                ElseIf i = 5 Then
                    If UNITS = 0 Then
                        LOS = CDbl(PrefLease(i).Replace(",", "") / Curr)
                        StrSqlIUpadate = StrSqlIUpadate + " SUPPORTLEASE" + " = " + LOS.ToString() + ","
                    Else
                        LOS = CDbl(PrefLease(i).Replace(",", "") * CONVAREA2 / Curr)
                        StrSqlIUpadate = StrSqlIUpadate + " SUPPORTLEASE" + " = " + LOS.ToString() + ","
                    End If

                End If

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
            ds = ObjGetData.GetPlantConfig2Details(CaseID)
            ValTotal = FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("PTOT4").ToString()), 0) + "#" + FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("LEASETOTAL").ToString()), 0) + "#" + FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("ARTOT").ToString()), 3) + "#" + FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("PTOT3").ToString()), 0) + "#" + FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("PTOT5").ToString()), 0) + "#" + FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("PTOT6").ToString()), 0) + "#" + FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("PTOT7").ToString()), 0) + "#" + FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("PTOT1").ToString()), 0) + "#" + FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("PTOT2").ToString()), 0)
            ValTotal = ValTotal + "#" + index.ToString()

            Return ValTotal

        Catch ex As Exception

        End Try
    End Function


    <System.Web.Services.WebMethod()> _
    Public Shared Function UpdateAllCases(ByVal CaseID As String, ByVal index As String, ByVal flag As String, ByVal LPH As String, ByVal LPPH As String, ByVal LPS As String, ByVal LWH As String, ByVal LOF As String, ByVal LOS As String, ByVal SWF As String, ByVal SOF As String, ByVal SOS As String) As String
        Try
            Dim AREA(2) As String
            Dim PrefLease(5) As String
            Dim odbUtil As New DBUtil()
            Dim ObjGetData As New E1GetData.Selectdata()
            Dim EconConnection As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            'Preferences
            Dim dts As New DataSet()
            dts = ObjGetData.GetPref(CaseID)

            'Getting Values From Database assign to variable 
            Dim Convthick As String = dts.Tables(0).Rows(0).Item("convthick")
            Dim Convwt As String = dts.Tables(0).Rows(0).Item("convwt")
            Dim Curr As String = dts.Tables(0).Rows(0).Item("curr")
            Dim CONVAREA2 As String = dts.Tables(0).Rows(0).Item("CONVAREA2")
            Dim UNITS As String = dts.Tables(0).Rows(0).Item("units")
            Dim i As New Integer
            Dim StrSqlUpadate As String = ""
            Dim StrSqlIUpadate As String = ""
            Dim ValTotal As String


            For i = 2 To 4
                If i = 2 Then
                    AREA(i - 2) = SWF.ToString()
                ElseIf i = 3 Then
                    AREA(i - 2) = SOF.ToString()
                ElseIf i = 4 Then
                    AREA(i - 2) = SOS.ToString()
                End If
                'AREA(i - 2) = Request.Form("ctl00$Econ1ContentPlaceHolder$AR" + i.ToString() + "")

            Next

            For i = 0 To 5
                If i = 0 Then
                    PrefLease(i) = LPH.ToString()
                ElseIf i = 1 Then
                    PrefLease(i) = LPPH.ToString()
                ElseIf i = 2 Then
                    PrefLease(i) = LPS.ToString()
                ElseIf i = 3 Then
                    PrefLease(i) = LWH.ToString()
                ElseIf i = 4 Then
                    PrefLease(i) = LOF.ToString()
                ElseIf i = 5 Then
                    PrefLease(i) = LOS.ToString()
                End If
                'PrefLease(i) = Request.Form("ctl00$Econ1ContentPlaceHolder$PREF" + (i + 1).ToString() + "")
            Next


            StrSqlUpadate = "UPDATE plantSPACE SET"
            For i = 2 To 4
                If i = 2 Then
                    SWF = CDbl(AREA(i - 2).Replace(",", "") / CONVAREA2)
                    StrSqlIUpadate = StrSqlIUpadate + " M" + i.ToString() + " = " + SWF.ToString() + ","
                ElseIf i = 3 Then
                    SOF = CDbl(AREA(i - 2).Replace(",", "") / CONVAREA2)
                    StrSqlIUpadate = StrSqlIUpadate + " M" + i.ToString() + " = " + SOF.ToString() + ","

                ElseIf i = 4 Then
                    SOS = CDbl(AREA(i - 2).Replace(",", "") / CONVAREA2)
                    StrSqlIUpadate = StrSqlIUpadate + " M" + i.ToString() + " = " + SOS.ToString() + ","

                End If
            Next
            StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
            StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
            StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
            odbUtil.UpIns(StrSqlUpadate, EconConnection)

            'Updating Preffered Cost

            StrSqlUpadate = ""
            StrSqlIUpadate = ""
            StrSqlUpadate = "UPDATE plantSPACE SET"
            For i = 0 To 5
                'For Production highbay
                If i = 0 Then
                    If UNITS = 0 Then
                        LPH = CDbl(PrefLease(i).Replace(",", "") / Curr)
                        StrSqlIUpadate = StrSqlIUpadate + " PRODUCTIONLEASEHB" + " = " + LPH.ToString() + ","
                    Else
                        LPH = CDbl(PrefLease(i).Replace(",", "") * CONVAREA2 / Curr)
                        StrSqlIUpadate = StrSqlIUpadate + " PRODUCTIONLEASEHB" + " = " + LPH.ToString() + ","
                    End If

                    'For Production Partial Highbay                       
                ElseIf i = 1 Then
                    If UNITS = 0 Then
                        LPPH = CDbl(PrefLease(i).Replace(",", "") / Curr)
                        StrSqlIUpadate = StrSqlIUpadate + " PRODUCTIONLEASEPHB" + " = " + LPPH.ToString() + ","
                    Else
                        LPPH = CDbl(PrefLease(i).Replace(",", "") * CONVAREA2 / Curr)
                        StrSqlIUpadate = StrSqlIUpadate + " PRODUCTIONLEASEPHB" + " = " + LPPH.ToString() + ","
                    End If

                    'For Production Standard
                ElseIf i = 2 Then
                    If UNITS = 0 Then
                        LPS = CDbl(PrefLease(i).Replace(",", "") / Curr)
                        StrSqlIUpadate = StrSqlIUpadate + " PRODUCTIONLEASE" + " = " + LPS.ToString() + ","
                    Else
                        LPS = CDbl(PrefLease(i).Replace(",", "") * CONVAREA2 / Curr)
                        StrSqlIUpadate = StrSqlIUpadate + " PRODUCTIONLEASE" + " = " + LPS.ToString() + ","
                    End If

                    'For Warehouse
                ElseIf i = 3 Then
                    If UNITS = 0 Then
                        LWH = CDbl(PrefLease(i).Replace(",", "") / Curr)
                        StrSqlIUpadate = StrSqlIUpadate + " WAREHOUSELEASE" + " = " + LWH.ToString() + ","
                    Else
                        LWH = CDbl(PrefLease(i).Replace(",", "") * CONVAREA2 / Curr)
                        StrSqlIUpadate = StrSqlIUpadate + " WAREHOUSELEASE" + " = " + LWH.ToString() + ","
                    End If

                    'For Office
                ElseIf i = 4 Then
                    If UNITS = 0 Then
                        LOF = CDbl(PrefLease(i).Replace(",", "") / Curr)
                        StrSqlIUpadate = StrSqlIUpadate + " OFFICELEASE" + " = " + LOF.ToString() + ","
                    Else
                        LOF = CDbl(PrefLease(i).Replace(",", "") * CONVAREA2 / Curr)
                        StrSqlIUpadate = StrSqlIUpadate + " OFFICELEASE" + " = " + LOF.ToString() + ","
                    End If

                    'For Support
                ElseIf i = 5 Then
                    If UNITS = 0 Then
                        LOS = CDbl(PrefLease(i).Replace(",", "") / Curr)
                        StrSqlIUpadate = StrSqlIUpadate + " SUPPORTLEASE" + " = " + LOS.ToString() + ","
                    Else
                        LOS = CDbl(PrefLease(i).Replace(",", "") * CONVAREA2 / Curr)
                        StrSqlIUpadate = StrSqlIUpadate + " SUPPORTLEASE" + " = " + LOS.ToString() + ","
                    End If

                End If

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
            ds = ObjGetData.GetPlantConfig2Details(CaseID)
            ValTotal = FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("PTOT4").ToString()), 0) + "#" + FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("LEASETOTAL").ToString()), 0) + "#" + FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("ARTOT").ToString()), 3) + "#" + FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("PTOT3").ToString()), 0) + "#" + FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("PTOT5").ToString()), 0) + "#" + FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("PTOT6").ToString()), 0) + "#" + FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("PTOT7").ToString()), 0) + "#" + FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("PTOT1").ToString()), 0) + "#" + FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("PTOT2").ToString()), 0)
            ValTotal = ValTotal + "#" + index.ToString() + "#" + flag.ToString()

            Return ValTotal

        Catch ex As Exception

        End Try
    End Function



End Class
