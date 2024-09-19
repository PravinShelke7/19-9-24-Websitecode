Imports System.Data
Imports System.Data.OleDb
Imports System
Imports E3GetData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Econ3_Assumptions_EnergyIn
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
            Dim btn As New Button
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
                ds = objGetData.GetEnergyDetails(arrCaseID(i))
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
            'SUD ENDED
            For i = 1 To 1
                For j = 1 To 21
                    trInner = New TableRow()

                    Select Case j
                        Case 1
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Energy Price", trInner, "AlterNateColor4")
                            For k = 0 To DataCnt
                                Dim str As String = String.Empty
                                str = str + "<table cellpadding='3' border='0' width='100%'>"
                                str = str + "<tr style='text-align:right;'>"
                                str = str + "<td  width='45%'>Energy Price Cost Sugg.</td>"
                                str = str + "<td  width='45%'>Energy Price Pref.</td>"
                                str = str + "</tr>"
                                str = str + "</table>"
                                tdInner = New TableCell
                                tdInner.Text = str
                                InnerTdSetting(tdInner, "", "Right")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 2
                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("TITLE2").ToString() + "/kwh)"
                            LeftTdSetting(tdInner, "Electricity " + Title + "", trInner, "")
                            trInner.ID = "Elec_" + i.ToString()
                            For k = 0 To DataCnt
                                Dim str As String = String.Empty
                                str = str + "<table cellpadding='3' border='0' width='100%'>"
                                str = str + "<tr style='text-align:right'>"
                                ' str = str + "<td width='45%'><label id='ELECS" + (k + 1).ToString() + "' runat='server'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("ERGPSUG1").ToString()), 3).ToString() + "<label/></td>"
                                str = str + "<td width='45%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("ERGPSUG1").ToString()), 3).ToString() + "</td>"

                                'str = str + "<td width='45%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("ERGPPREF1").ToString()), 3).ToString() + "</td>"
                                If arrCaseID(k) <= 1000 Then
                                    str = str + "<td width='45%'><input type='text' OnkeyPress='return clickButton(event)' style='background-color:#a6a6a6;' runat='server' id='ELEC" + (k + 1).ToString() + "' class='PrefTextBox' runat='server' name='ELEC" + (k + 1).ToString() + "' value='" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("ERGPPREF1").ToString()), 3).ToString() + "' disabled/></td>"
                                Else
                                    str = str + "<td width='45%'><input type='text' maxlength='6' OnkeyPress='return clickButton(event)' runat='server' id='ELEC" + (k + 1).ToString() + "' class='PrefTextBox' runat='server' name='ELEC" + (k + 1).ToString() + "' value='" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("ERGPPREF1").ToString()), 3).ToString() + "'/></td>"
                                End If
                               
                                str = str + "</tr>"
                                str = str + "</table>"
                                tdInner = New TableCell
                                tdInner.Text = str
                                InnerTdSetting(tdInner, "", "Center")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 3
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("TITLE2").ToString() + "/Mcf)"
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Natural Gas " + Title, trInner, "")
                            trInner.ID = "NATG_" + i.ToString()
                            For k = 0 To DataCnt
                                Dim str As String = String.Empty
                                str = str + "<table cellpadding='3' border='0' width='100%'>"
                                str = str + "<tr style='text-align:right'>"
                                str = str + "<td width='45%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("ERGPSUG2").ToString()), 3).ToString() + "</td>"
                                'str = str + "<td width='45%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("ERGPPREF2").ToString()), 3).ToString() + "</td>"
                                 If arrCaseID(k) <= 1000 Then
                                    str = str + "<td width='45%'><input type='text' OnkeyPress='return clickButton(event)' style='background-color:#a6a6a6;' class='PrefTextBox' runat='server' id='NGAS" + (k + 1).ToString() + "' name='NGAS" + (k + 1).ToString() + "' value='" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("ERGPPREF2").ToString()), 3).ToString() + "' disabled/></td>"
                                Else
                                    str = str + "<td width='45%'><input type='text' maxlength='6' OnkeyPress='return clickButton(event)' class='PrefTextBox' runat='server' id='NGAS" + (k + 1).ToString() + "' name='NGAS" + (k + 1).ToString() + "' value='" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("ERGPPREF2").ToString()), 3).ToString() + "'/></td>"
                                End If

                                str = str + "</tr>"
                                str = str + "</table>"
                                tdInner = New TableCell
                                tdInner.Text = str
                                InnerTdSetting(tdInner, "", "Center")
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 4
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Energy Requirements", trInner, "AlterNateColor4")

                            For k = 0 To DataCnt
                                Dim str As String = String.Empty
                                str = str + "<table cellpadding='3' border='0' width='100%'>"
                                str = str + "<tr style='text-align:right;'>"
                                str = str + "<td  width='45%'>Electricity</td>"
                                str = str + "<td  width='45%'>Natural Gas</td>"
                                str = str + "</tr>"
                                str = str + "<tr style='text-align:right;'>"
                                str = str + "<td  width='45%'>(kwh)</td>"
                                str = str + "<td  width='45%'>(cubic ft)</td>"
                                str = str + "</tr>"
                                str = str + "</table>"
                                tdInner = New TableCell
                                tdInner.Text = str
                                InnerTdSetting(tdInner, "", "Right")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 5
                            Title = ""
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Production" + Title, trInner, "")
                            trInner.ID = "PRODA_" + i.ToString()
                            For k = 0 To DataCnt
                                Dim str As String = String.Empty
                                str = str + "<table cellpadding='3' border='0' width='100%'>"
                                str = str + "<tr style='text-align:right'>"
                                str = str + "<td width='45%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("ENRGE1").ToString()), 0).ToString() + "</td>"
                                str = str + "<td width='45%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("ENRGN1").ToString()), 0).ToString() + "</td>"
                                str = str + "</tr>"
                                str = str + "</table>"
                                tdInner = New TableCell
                                tdInner.Text = str
                                InnerTdSetting(tdInner, "", "Center")
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 6
                            Title = ""
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Warehouse" + Title, trInner, "")
                            trInner.ID = "WARA_" + i.ToString()
                            For k = 0 To DataCnt
                                Dim str As String = String.Empty
                                str = str + "<table cellpadding='3' border='0' width='100%'>"
                                str = str + "<tr style='text-align:right'>"
                                str = str + "<td width='45%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("ENRGE2").ToString()), 0).ToString() + "</td>"
                                str = str + "<td width='45%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("ENRGN2").ToString()), 0).ToString() + "</td>"
                                str = str + "</tr>"
                                str = str + "</table>"
                                tdInner = New TableCell
                                tdInner.Text = str
                                InnerTdSetting(tdInner, "", "Center")
                                trInner.Controls.Add(tdInner)

                            Next
                        Case 7
                            Title = ""
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Office" + Title, trInner, "")
                            trInner.ID = "OFFA_" + i.ToString()
                            For k = 0 To DataCnt
                                Dim str As String = String.Empty
                                str = str + "<table cellpadding='3' border='0' width='100%'>"
                                str = str + "<tr style='text-align:right'>"
                                str = str + "<td width='45%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("ENRGE3").ToString()), 0).ToString() + "</td>"
                                str = str + "<td width='45%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("ENRGN3").ToString()), 0).ToString() + "</td>"
                                str = str + "</tr>"
                                str = str + "</table>"
                                tdInner = New TableCell
                                tdInner.Text = str
                                InnerTdSetting(tdInner, "", "Center")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 8
                            Title = ""
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Support" + Title, trInner, "")
                            trInner.ID = "SUPPA_" + i.ToString()
                            For k = 0 To DataCnt
                                Dim str As String = String.Empty
                                str = str + "<table cellpadding='3' border='0' width='100%'>"
                                str = str + "<tr style='text-align:right'>"
                                str = str + "<td width='45%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("ENRGE4").ToString()), 0).ToString() + "</td>"
                                str = str + "<td width='45%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("ENRGN4").ToString()), 0).ToString() + "</td>"
                                str = str + "</tr>"
                                str = str + "</table>"
                                tdInner = New TableCell
                                tdInner.Text = str
                                InnerTdSetting(tdInner, "", "Center")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 9
                            Title = ""
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Total" + Title, trInner, "")
                            trInner.ID = "TOTA_" + i.ToString()
                            For k = 0 To DataCnt
                                Dim str As String = String.Empty
                                str = str + "<table cellpadding='3' border='0' width='100%'>"
                                str = str + "<tr style='text-align:right'>"
                                str = str + "<td width='45%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("ENRGE5").ToString()), 0).ToString() + "</td>"
                                str = str + "<td width='45%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("ENRGN5").ToString()), 0).ToString() + "</td>"
                                str = str + "</tr>"
                                str = str + "</table>"
                                tdInner = New TableCell
                                tdInner.Text = str
                                InnerTdSetting(tdInner, "", "Center")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 10
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Energy Cost", trInner, "AlterNateColor4")


                            For k = 0 To DataCnt
                                Dim str As String = String.Empty
                                str = str + "<table cellpadding='3' border='0' width='100%'>"
                                str = str + "<tr style='text-align:right;'>"
                                str = str + "<td  width='35%'>Electricity</td>"
                                str = str + "<td  width='35%'>Natural Gas</td>"
                                str = str + "<td  width='30%'>Total</td>"
                                str = str + "</tr>"
                                str = str + "<tr style='text-align:right;'>"
                                str = str + "<td  width='35%'>(" + dstbl.Tables(k).Rows(0).Item("title2") + Title.ToString() + ")</td>"
                                str = str + "<td  width='35%'>(" + dstbl.Tables(k).Rows(0).Item("title2") + Title.ToString() + ")</td>"
                                str = str + "<td  width='30%'>(" + dstbl.Tables(k).Rows(0).Item("title2") + Title.ToString() + ")</td>"
                                str = str + "</tr>"
                                str = str + "</table>"
                                tdInner = New TableCell
                                tdInner.Text = str
                                InnerTdSetting(tdInner, "", "Right")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 11
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Production", trInner, "")
                            trInner.ID = "PRODB_" + i.ToString()
                            For k = 0 To DataCnt
                                Dim str As String = String.Empty
                                str = str + "<table cellpadding='3' border='0' width='100%'>"
                                str = str + "<tr style='text-align:right'>"
                                ' str = str + "<td width='35%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("ENRGCE1").ToString()), 0).ToString() + "</td>"
                                str = str + "<td width='35%'><label id='PRODELEC" + (k + 1).ToString() + "' runat='server'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("ENRGCE1").ToString()), 0).ToString() + "<label/></td>"
                                'str = str + "<td width='35%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("ENRGCG1").ToString()), 0).ToString() + "</td>"
                                str = str + "<td width='35%'><label id='PRODNATG" + (k + 1).ToString() + "' runat='server'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("ENRGCG1").ToString()), 0).ToString() + "<label/></td>"

                                'str = str + "<td width='30%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("TOTAL1").ToString()), 0).ToString() + "</td>"
                                str = str + "<td width='30%'><label id='PRODTOT" + (k + 1).ToString() + "' runat='server'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("TOTAL1").ToString()), 0).ToString() + "<label/></td>"

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
                            trInner.ID = "WARB_" + i.ToString()
                            For k = 0 To DataCnt
                                Dim str As String = String.Empty
                                str = str + "<table cellpadding='3' border='0' width='100%'>"
                                str = str + "<tr style='text-align:right'>"
                                'str = str + "<td width='35%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("ENRGCE2").ToString()), 0).ToString() + "</td>"
                                'str = str + "<td width='35%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("ENRGCG2").ToString()), 0).ToString() + "</td>"
                                'str = str + "<td width='30%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("TOTAL2").ToString()), 0).ToString() + "</td>"

                                str = str + "<td width='35%'><label id='WAREELEC" + (k + 1).ToString() + "' runat='server'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("ENRGCE2").ToString()), 0).ToString() + "<label/></td>"
                                str = str + "<td width='35%'><label id='WARENATG" + (k + 1).ToString() + "' runat='server'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("ENRGCG2").ToString()), 0).ToString() + "<label/></td>"
                                str = str + "<td width='30%'><label id='WARETOT" + (k + 1).ToString() + "' runat='server'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("TOTAL2").ToString()), 0).ToString() + "<label/></td>"

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
                            trInner.ID = "OFFB_" + i.ToString()
                            For k = 0 To DataCnt
                                Dim str As String = String.Empty
                                str = str + "<table cellpadding='3' border='0' width='100%'>"
                                str = str + "<tr style='text-align:right'>"
                                'str = str + "<td width='35%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("ENRGCE3").ToString()), 0).ToString() + "</td>"
                                'str = str + "<td width='35%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("ENRGCG3").ToString()), 0).ToString() + "</td>"
                                'str = str + "<td width='30%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("TOTAL3").ToString()), 0).ToString() + "</td>"


                                str = str + "<td width='35%'><label id='OFFCELEC" + (k + 1).ToString() + "' runat='server'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("ENRGCE3").ToString()), 0).ToString() + "<label/></td>"
                                str = str + "<td width='35%'><label id='OFFCNATG" + (k + 1).ToString() + "' runat='server'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("ENRGCG3").ToString()), 0).ToString() + "<label/></td>"
                                str = str + "<td width='30%'><label id='OFFCTOT" + (k + 1).ToString() + "' runat='server'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("TOTAL3").ToString()), 0).ToString() + "<label/></td>"

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
                            trInner.ID = "SUPPB_" + i.ToString()
                            For k = 0 To DataCnt
                                Dim str As String = String.Empty
                                str = str + "<table cellpadding='3' border='0' width='100%'>"
                                str = str + "<tr style='text-align:right'>"
                                'str = str + "<td width='35%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("ENRGCE4").ToString()), 0).ToString() + "</td>"
                                'str = str + "<td width='35%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("ENRGCG4").ToString()), 0).ToString() + "</td>"
                                'str = str + "<td width='30%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("TOTAL4").ToString()), 0).ToString() + "</td>"

                                str = str + "<td width='35%'><label id='SUPPELEC" + (k + 1).ToString() + "' runat='server'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("ENRGCE4").ToString()), 0).ToString() + "<label/></td>"
                                str = str + "<td width='35%'><label id='SUPPNATG" + (k + 1).ToString() + "' runat='server'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("ENRGCG4").ToString()), 0).ToString() + "<label/></td>"
                                str = str + "<td width='30%'><label id='SUPPTOT" + (k + 1).ToString() + "' runat='server'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("TOTAL4").ToString()), 0).ToString() + "<label/></td>"

                                str = str + "</tr>"
                                str = str + "</table>"
                                tdInner = New TableCell
                                tdInner.Text = str
                                InnerTdSetting(tdInner, "", "Center")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 15
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Total ", trInner, "")
                            trInner.ID = "TOTB_" + i.ToString()
                            For k = 0 To DataCnt
                                Dim str As String = String.Empty
                                str = str + "<table cellpadding='3' border='0' width='100%'>"
                                str = str + "<tr style='text-align:right'>"
                                'str = str + "<td width='35%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("ENRGCE5").ToString()), 0).ToString() + "</td>"
                                'str = str + "<td width='35%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("ENRGCG5").ToString()), 0).ToString() + "</td>"
                                'str = str + "<td width='30%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("TOTAL5").ToString()), 0).ToString() + "</td>"

                                str = str + "<td width='35%'><label id='TELEC" + (k + 1).ToString() + "' runat='server'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("ENRGCE5").ToString()), 0).ToString() + "<label/></td>"
                                str = str + "<td width='35%'><label id='TNATG" + (k + 1).ToString() + "' runat='server'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("ENRGCG5").ToString()), 0).ToString() + "<label/></td>"
                                str = str + "<td width='30%'><label id='TTOT" + (k + 1).ToString() + "' runat='server'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("TOTAL5").ToString()), 0).ToString() + "<label/></td>"

                                str = str + "</tr>"
                                str = str + "</table>"
                                tdInner = New TableCell
                                tdInner.Text = str
                                InnerTdSetting(tdInner, "", "Center")
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 16
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Energy Cost", trInner, "AlterNateColor4")

                            For k = 0 To DataCnt
                                Dim str As String = String.Empty
                                str = str + "<table cellpadding='3' border='0' width='100%'>"
                                str = str + "<tr style='text-align:right;'>"
                                str = str + "<td  width='50%'>Variable</td>"
                                str = str + "<td  width='50%'>Fixed</td>"
                                str = str + "</tr>"
                                str = str + "<tr style='text-align:right;'>"
                                str = str + "<td  width='50%'> (" + dstbl.Tables(k).Rows(0).Item("title2") + Title.ToString() + ")</td>"
                                str = str + "<td  width='50%'>(" + dstbl.Tables(k).Rows(0).Item("title2") + Title.ToString() + ")</td>"
                                str = str + "</tr>"
                                str = str + "</table>"
                                tdInner = New TableCell
                                tdInner.Text = str
                                InnerTdSetting(tdInner, "", "Right")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 17
                            Title = ""
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Production" + Title, trInner, "")
                            trInner.ID = "PRODTC_" + i.ToString()
                            For k = 0 To DataCnt
                                Dim str As String = String.Empty
                                str = str + "<table cellpadding='3' border='0' width='100%'>"
                                str = str + "<tr style='text-align:right'>"
                                ' str = str + "<td width='50%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("VTOTAL1").ToString()), 0).ToString() + "</td>"
                                str = str + "<td width='50%'><label id='PRODVAR" + (k + 1).ToString() + "' runat='server'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("VTOTAL1").ToString()), 0).ToString() + "<label/></td>"

                                'str = str + "<td width='50%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("FTOTAL1").ToString()), 0).ToString() + "</td>"
                                str = str + "<td width='50%'><label id='PRODFIXED" + (k + 1).ToString() + "' runat='server'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("FTOTAL1").ToString()), 0).ToString() + "<label/></td>"

                                str = str + "</tr>"
                                str = str + "</table>"
                                tdInner = New TableCell
                                tdInner.Text = str
                                InnerTdSetting(tdInner, "", "Center")
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 18
                            Title = ""
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Warehouse" + Title, trInner, "")
                            trInner.ID = "WARTC_" + i.ToString()
                            For k = 0 To DataCnt
                                Dim str As String = String.Empty
                                str = str + "<table cellpadding='3' border='0' width='100%'>"
                                str = str + "<tr style='text-align:right'>"
                                ' str = str + "<td width='50%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("VTOTAL2").ToString()), 0).ToString() + "</td>"
                                'str = str + "<td width='50%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("FTOTAL2").ToString()), 0).ToString() + "</td>"

                                str = str + "<td width='50%'><label id='WAREVAR" + (k + 1).ToString() + "' runat='server'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("VTOTAL2").ToString()), 0).ToString() + "<label/></td>"
                                str = str + "<td width='50%'><label id='WAREFIXED" + (k + 1).ToString() + "' runat='server'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("FTOTAL2").ToString()), 0).ToString() + "<label/></td>"


                                str = str + "</tr>"
                                str = str + "</table>"
                                tdInner = New TableCell
                                tdInner.Text = str
                                InnerTdSetting(tdInner, "", "Center")
                                trInner.Controls.Add(tdInner)

                            Next
                        Case 19
                            Title = ""
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Office" + Title, trInner, "")
                            trInner.ID = "OFFTC_" + i.ToString()
                            For k = 0 To DataCnt
                                Dim str As String = String.Empty
                                str = str + "<table cellpadding='3' border='0' width='100%'>"
                                str = str + "<tr style='text-align:right'>"
                                'str = str + "<td width='50%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("VTOTAL3").ToString()), 0).ToString() + "</td>"
                                ' str = str + "<td width='50%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("FTOTAL3").ToString()), 0).ToString() + "</td>"

                                str = str + "<td width='50%'><label id='OFFCVAR" + (k + 1).ToString() + "' runat='server'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("VTOTAL3").ToString()), 0).ToString() + "<label/></td>"
                                str = str + "<td width='50%'><label id='OFFCFIXED" + (k + 1).ToString() + "' runat='server'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("FTOTAL3").ToString()), 0).ToString() + "<label/></td>"


                                str = str + "</tr>"
                                str = str + "</table>"
                                tdInner = New TableCell
                                tdInner.Text = str
                                InnerTdSetting(tdInner, "", "Center")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 20
                            Title = ""
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Support" + Title, trInner, "")
                            trInner.ID = "SUPTC_" + i.ToString()
                            For k = 0 To DataCnt
                                Dim str As String = String.Empty
                                str = str + "<table cellpadding='3' border='0' width='100%'>"
                                str = str + "<tr style='text-align:right'>"
                                'str = str + "<td width='50%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("VTOTAL4").ToString()), 0).ToString() + "</td>"
                                'str = str + "<td width='50%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("FTOTAL4").ToString()), 0).ToString() + "</td>"

                                str = str + "<td width='50%'><label id='SUPVAR" + (k + 1).ToString() + "' runat='server'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("VTOTAL4").ToString()), 0).ToString() + "<label/></td>"
                                str = str + "<td width='50%'><label id='SUPFIXED" + (k + 1).ToString() + "' runat='server'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("FTOTAL4").ToString()), 0).ToString() + "<label/></td>"


                                str = str + "</tr>"
                                str = str + "</table>"
                                tdInner = New TableCell
                                tdInner.Text = str
                                InnerTdSetting(tdInner, "", "Center")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 21
                            Title = ""
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Total" + Title, trInner, "")
                            trInner.ID = "TOTTC_" + i.ToString()
                            For k = 0 To DataCnt
                                Dim str As String = String.Empty
                                str = str + "<table cellpadding='3' border='0' width='100%'>"
                                str = str + "<tr style='text-align:right'>"
                                'str = str + "<td width='50%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("VTOTAL5").ToString()), 0).ToString() + "</td>"
                                'str = str + "<td width='50%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("FTOTAL5").ToString()), 0).ToString() + "</td>"

                                str = str + "<td width='50%'><label id='TOTVAR" + (k + 1).ToString() + "' runat='server'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("VTOTAL5").ToString()), 0).ToString() + "<label/></td>"
                                str = str + "<td width='50%'><label id='TOTFIXED" + (k + 1).ToString() + "' runat='server'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("FTOTAL5").ToString()), 0).ToString() + "<label/></td>"


                                str = str + "</tr>"
                                str = str + "</table>"
                                tdInner = New TableCell
                                tdInner.Text = str
                                InnerTdSetting(tdInner, "", "Center")
                                trInner.Controls.Add(tdInner)
                            Next




                    End Select
                    If j = 1 Or j = 4 Or j = 10 Or j = 16 Then
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

    <System.Web.Services.WebMethod()> _
    Public Shared Function UpdateCase(ByVal CaseID As String, ByVal index As String, ByVal Elec As String, ByVal NGas As String) As Array
        Try
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

            dts = ObjGetData.GetPref(CaseID)

            Dim Convwt As String = dts.Tables(0).Rows(0).Item("convwt")
            Dim Curr As String = dts.Tables(0).Rows(0).Item("curr")
            Dim Convthick As String = dts.Tables(0).Rows(0).Item("convthick")
            StrSqlUpadate = "UPDATE plantENERGY SET"
            StrSqlIUpadate = StrSqlIUpadate + " ELECPRICE=" + CDbl(Elec.Replace(",", "").ToString() / Curr).ToString() + ","
            StrSqlIUpadate = StrSqlIUpadate + " NGASPRICE=" + CDbl(NGas.Replace(",", "").ToString() / Curr).ToString() + ","
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
            ds = ObjGetData.GetEnergyDetails(CaseID)
            For i = 0 To 3

                ArrVal(i) = FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("ENRGCE" + (i + 1).ToString()).ToString()), 0) + "#" + FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("ENRGCG" + (i + 1).ToString()).ToString()), 0) + "#" + FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("TOTAL" + (i + 1).ToString()).ToString()), 0) + "#" + FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("ENRGCE5").ToString()), 0) + "#" + FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("ENRGCG5").ToString()), 0) + "#" + FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("TOTAL5").ToString()), 0) + "#" + FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("VTOTAL" + (i + 1).ToString()).ToString()), 0) + "#" + FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("FTOTAL" + (i + 1).ToString()).ToString()), 0) + "#" + FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("VTOTAL5").ToString()), 0) + "#" + FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("FTOTAL5").ToString()), 0)
                If i = 0 Then
                    ArrVal(i) = ArrVal(i) + "#" + index.ToString()
                End If
            Next
            Return ArrVal
        Catch ex As Exception

        End Try
    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function UpdateAllCases(ByVal CaseID As String, ByVal index As String, ByVal flag As String, ByVal Elec As String, ByVal NGas As String) As Array
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
            Dim ArrVal(30) As String
            dts = ObjGetData.GetPref(CaseID)

            Dim Convwt As String = dts.Tables(0).Rows(0).Item("convwt")
            Dim Curr As String = dts.Tables(0).Rows(0).Item("curr")
            Dim Convthick As String = dts.Tables(0).Rows(0).Item("convthick")
            StrSqlUpadate = "UPDATE plantENERGY SET"
            StrSqlIUpadate = StrSqlIUpadate + " ELECPRICE=" + CDbl(Elec.Replace(",", "").ToString() / Curr).ToString() + ","
            StrSqlIUpadate = StrSqlIUpadate + " NGASPRICE=" + CDbl(NGas.Replace(",", "").ToString() / Curr).ToString() + ","
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
            ds = ObjGetData.GetEnergyDetails(CaseID)
            For i = 0 To 3

                ArrVal(i) = FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("ENRGCE" + (i + 1).ToString()).ToString()), 0) + "#" + FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("ENRGCG" + (i + 1).ToString()).ToString()), 0) + "#" + FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("TOTAL" + (i + 1).ToString()).ToString()), 0) + "#" + FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("ENRGCE5").ToString()), 0) + "#" + FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("ENRGCG5").ToString()), 0) + "#" + FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("TOTAL5").ToString()), 0) + "#" + FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("VTOTAL" + (i + 1).ToString()).ToString()), 0) + "#" + FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("FTOTAL" + (i + 1).ToString()).ToString()), 0) + "#" + FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("VTOTAL5").ToString()), 0) + "#" + FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("FTOTAL5").ToString()), 0)
                If i = 0 Then
                    ArrVal(i) = ArrVal(i) + "#" + index.ToString() + "#" + flag
                End If
            Next
            Return ArrVal
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


End Class
