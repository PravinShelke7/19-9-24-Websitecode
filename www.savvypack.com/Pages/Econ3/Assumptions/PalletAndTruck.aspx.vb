Imports System.Data
Imports System.Data.OleDb
Imports System
Imports E3GetData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Econ3_Assumptions_PalletAndTruck
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

            Dim btn As Button

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
                ds = objGetData.GetPalletAndTruck(arrCaseID(i))
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

            Dim hid As New HiddenField
            For i = 0 To DataCnt
                tdHeader = New TableCell
                Dim Headertext As String = String.Empty
                Headertext = "Case#:" + arrCaseID(i).ToString() + "<br/>"
                CaseDesp.Add(arrCaseID(i).ToString())


                btn = New Button
                btn.ID = "btnss_" + (i + 1).ToString()
                btn.Text = "Update"
                btn.Height = 15
                btn.Style.Add("font-size", "9px")
                btn.CommandArgument = arrCaseID(k).ToString()
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

            For j = 1 To 14
                trInner = New TableRow()

                Select Case j
                    Case 1
                        tdInner = New TableCell
                        LeftTdSetting(tdInner, "<b>Pallet Details</b>", trInner, "AlterNateColor4")
                        trInner.ID = "P" + j.ToString() + "_1"

                        For k = 0 To DataCnt
                            tdInner = New TableCell
                            tdInner.Text = "&nbsp;"
                            InnerTdSetting(tdInner, "", "Right")
                            trInner.Controls.Add(tdInner)
                        Next
                    Case 2
                        tdInner = New TableCell
                        Title = "(" + dstbl.Tables(0).Rows(0).Item("TITLE9").ToString() + ")"
                        LeftTdSetting(tdInner, "Width " + Title, trInner, "AlterNateColor1")
                        trInner.ID = "P" + j.ToString() + "_1"

                        For k = 0 To DataCnt
                            tdInner = New TableCell
                            tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("P1").ToString(), 0)
                            InnerTdSetting(tdInner, "", "Right")
                            trInner.Controls.Add(tdInner)
                        Next
                    Case 3
                        tdInner = New TableCell
                        Title = "(" + dstbl.Tables(0).Rows(0).Item("TITLE9").ToString() + ")"
                        LeftTdSetting(tdInner, "Length " + Title, trInner, "AlterNateColor2")
                        trInner.ID = "P" + j.ToString() + "_1"

                        For k = 0 To DataCnt
                            tdInner = New TableCell
                            tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("P2").ToString(), 0)
                            InnerTdSetting(tdInner, "", "Right")
                            trInner.Controls.Add(tdInner)
                        Next
                    Case 4
                        tdInner = New TableCell
                        Title = "(" + dstbl.Tables(0).Rows(0).Item("TITLE9").ToString() + ")"
                        LeftTdSetting(tdInner, "Height " + Title, trInner, "AlterNateColor1")
                        trInner.ID = "P" + j.ToString() + "_1"

                        For k = 0 To DataCnt
                            tdInner = New TableCell
                            tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("P3").ToString(), 0)
                            InnerTdSetting(tdInner, "", "Right")
                            trInner.Controls.Add(tdInner)
                        Next

                    Case 5
                        tdInner = New TableCell
                        LeftTdSetting(tdInner, "Cartons Per Pallet", trInner, "AlterNateColor2")
                        trInner.ID = "P" + j.ToString() + "_1"

                        For k = 0 To DataCnt
                            tdInner = New TableCell
                            txt = New TextBox
							 txt.MaxLength = 6
                            txt.ID = "CPPAL" + (k + 1).ToString()

                            'txt.Style.Add("Width", "100px")
                            txt.CssClass = "PrefTextBox"
                            txt.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("P4").ToString(), 0)
                            txt.Attributes.Add("OnkeyPress", "return clickButton(event)")
                            If arrCaseID(k) <= 1000 Then
                                txt.Style.Add("background-color", "#a6a6a6")
                                txt.Enabled = False
                            End If
                            InnerTdSetting(tdInner, "", "Right")
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)

                        Next

                    Case 6
                        tdInner = New TableCell
                        LeftTdSetting(tdInner, "Product Per Pallet", trInner, "AlterNateColor1")
                        trInner.ID = "P" + j.ToString() + "_1"

                        For k = 0 To DataCnt
                            tdInner = New TableCell
                            txt = New TextBox
							txt.MaxLength = 10
                            txt.ID = "PPPAL" + (k + 1).ToString()
                            'txt.Style.Add("Width", "100px")
                            txt.CssClass = "PrefTextBox"
                            txt.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("P5").ToString(), 0)
                            txt.Attributes.Add("OnkeyPress", "return clickButton(event)")
                            If arrCaseID(k) <= 1000 Then
                                txt.Style.Add("background-color", "#a6a6a6")
                                txt.Enabled = False
                            End If
                            InnerTdSetting(tdInner, "", "Right")
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Next

                    Case 7
                        tdInner = New TableCell
                        LeftTdSetting(tdInner, "<b>Truck Dimensions</b>", trInner, "AlterNateColor4")
                        trInner.ID = "P" + j.ToString() + "_1"

                        For k = 0 To DataCnt
                            tdInner = New TableCell
                            tdInner.Text = "&nbsp;"
                            InnerTdSetting(tdInner, "", "Right")
                            trInner.Controls.Add(tdInner)
                        Next
                    Case 8
                        tdInner = New TableCell
                        Title = "(" + dstbl.Tables(0).Rows(0).Item("TITLE9").ToString() + ")"
                        LeftTdSetting(tdInner, "Width " + Title, trInner, "AlterNateColor2")
                        trInner.ID = "P" + j.ToString() + "_1"

                        For k = 0 To DataCnt
                            tdInner = New TableCell
                            tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("T1").ToString(), 0)
                            InnerTdSetting(tdInner, "", "Right")
                            trInner.Controls.Add(tdInner)
                        Next
                    Case 9
                        tdInner = New TableCell
                        Title = "(" + dstbl.Tables(0).Rows(0).Item("TITLE9").ToString() + ")"
                        LeftTdSetting(tdInner, "Length " + Title, trInner, "AlterNateColor1")
                        trInner.ID = "P" + j.ToString() + "_1"

                        For k = 0 To DataCnt
                            tdInner = New TableCell
                            tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("T2").ToString(), 0)
                            InnerTdSetting(tdInner, "", "Right")
                            trInner.Controls.Add(tdInner)
                        Next
                    Case 10
                        tdInner = New TableCell
                        Title = "(" + dstbl.Tables(0).Rows(0).Item("TITLE9").ToString() + ")"
                        LeftTdSetting(tdInner, "Height " + Title, trInner, "AlterNateColor2")
                        trInner.ID = "P" + j.ToString() + "_1"

                        For k = 0 To DataCnt
                            tdInner = New TableCell
                            tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("T3").ToString(), 0)
                            InnerTdSetting(tdInner, "", "Right")
                            trInner.Controls.Add(tdInner)
                        Next

                    Case 11
                        tdInner = New TableCell
                        Title = "(" + dstbl.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                        LeftTdSetting(tdInner, "Weight Limit " + Title, trInner, "AlterNateColor1")
                        trInner.ID = "P" + j.ToString() + "_1"

                        For k = 0 To DataCnt
                            tdInner = New TableCell
                            txt = New TextBox
							txt.MaxLength = 6
                            txt.ID = "WTLIM" + (k + 1).ToString()
                            'txt.Style.Add("Width", "100px")
                            txt.CssClass = "PrefTextBox"
                            txt.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("T4").ToString(), 0)

                            txt.Attributes.Add("OnkeyPress", "return clickButton(event)")
                            If arrCaseID(k) <= 1000 Then
                                txt.Style.Add("background-color", "#a6a6a6")
                                txt.Enabled = False
                            End If
                            InnerTdSetting(tdInner, "", "Right")
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Next

                    Case 12
                        tdInner = New TableCell
                        LeftTdSetting(tdInner, "Pallets Per Truck (Number)", trInner, "AlterNateColor2")
                        trInner.ID = "P" + j.ToString() + "_1"

                        For k = 0 To DataCnt
                            tdInner = New TableCell
                            txt = New TextBox
							txt.MaxLength = 6
                            txt.ID = "PPTCK" + (k + 1).ToString()
                            txt.CssClass = "PrefTextBox"
                            txt.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("T5").ToString(), 0)
                            txt.Attributes.Add("OnkeyPress", "return clickButton(event)")
                            If arrCaseID(k) <= 1000 Then
                                txt.Style.Add("background-color", "#a6a6a6")
                                txt.Enabled = False
                            End If

                            InnerTdSetting(tdInner, "", "Right")
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Next
                    Case 13
                        tdInner = New TableCell
                        LeftTdSetting(tdInner, "<b>Calculated Weight</b>", trInner, "AlterNateColor4")
                        trInner.ID = "CAL" + j.ToString() + "_1"

                        For k = 0 To DataCnt
                            tdInner = New TableCell
                            tdInner.Text = "&nbsp;"
                            InnerTdSetting(tdInner, "", "Right")
                            trInner.Controls.Add(tdInner)
                        Next
                    Case 14
                        tdInner = New TableCell
                        Title = "(" + dstbl.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                        LeftTdSetting(tdInner, "Calculated Weight " + Title, trInner, "AlterNateColor2")
                        trInner.ID = "CALW_1"

                        For k = 0 To DataCnt
                            tdInner = New TableCell
                            lbl = New Label
                            lbl.Style.Add("Width", DWidth)
                            InnerTdSetting(tdInner, "", "Right")
                            lbl.ID = "WEIGHT" + (k + 1).ToString()
                            lbl.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("CALCULATEDWEIGHT").ToString(), 0)
                            InnerTdSetting(tdInner, "", "Right")
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Next



                End Select
                tblComparision.Controls.Add(trInner)
            Next
           


        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub

    <System.Web.Services.WebMethod()> _
    Public Shared Function UpdateCase(ByVal CPPAL As String, ByVal PPPAL As String, ByVal WTLIM As String, ByVal PPTCK As String, ByVal CaseID As String, ByVal index As String) As String
        Try
            Dim Weight As String
            Dim ObjGetData As New E1GetData.Selectdata()
            Dim EconConnection As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            Dim StrSqlUpadate As String = ""
            Dim StrSqlIUpadate As String = ""
            Dim odbUtil As New DBUtil()
            Dim dts As New DataSet()
            Dim ObjE3GetData As New E3GetData.Selectdata()
            Dim dsD As New DataSet
            Dim PageInfo As String = ""


            dts = ObjGetData.GetPref(CaseID)


            Dim Convwt As String = dts.Tables(0).Rows(0).Item("convwt")
            Dim Curr As String = dts.Tables(0).Rows(0).Item("curr")
            Dim Convthick As String = dts.Tables(0).Rows(0).Item("convthick")
            StrSqlUpadate = "UPDATE TRUCKPALLETIN SET"
            StrSqlIUpadate = StrSqlIUpadate + " M4=" + CPPAL.Replace(",", "").ToString() + ","
            StrSqlIUpadate = StrSqlIUpadate + " M5=" + PPPAL.Replace(",", "").ToString() + ","
            StrSqlIUpadate = StrSqlIUpadate + " T4=" + (CDbl(WTLIM.Replace(",", "").ToString()) / CDbl(Convwt)).ToString() + ","
            StrSqlIUpadate = StrSqlIUpadate + " T5=" + PPTCK.Replace(",", "").ToString() + ","

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
            ds = ObjGetData.GetPalletAndTruck(CaseID)
            Weight = FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("CALCULATEDWEIGHT").ToString()), 0)
            Weight = Weight + "#" + index.ToString()
            Return Weight
        Catch ex As Exception

        End Try
    End Function


    <System.Web.Services.WebMethod()> _
    Public Shared Function UpdateAllCases(ByVal CPPAL As String, ByVal PPPAL As String, ByVal WTLIM As String, ByVal PPTCK As String, ByVal CaseID As String, ByVal index As String, ByVal flag As String) As String
        Try
            Dim Weight As String
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

            dts = ObjGetData.GetPref(CaseID)


            Dim Convwt As String = dts.Tables(0).Rows(0).Item("convwt")
            Dim Curr As String = dts.Tables(0).Rows(0).Item("curr")
            Dim Convthick As String = dts.Tables(0).Rows(0).Item("convthick")
            StrSqlUpadate = "UPDATE TRUCKPALLETIN SET"
            StrSqlIUpadate = StrSqlIUpadate + " M4=" + CPPAL.Replace(",", "").ToString() + ","
            StrSqlIUpadate = StrSqlIUpadate + " M5=" + PPPAL.Replace(",", "").ToString() + ","
            StrSqlIUpadate = StrSqlIUpadate + " T4=" + (CDbl(WTLIM.Replace(",", "").ToString()) / CDbl(Convwt)).ToString() + ","
            StrSqlIUpadate = StrSqlIUpadate + " T5=" + PPTCK.Replace(",", "").ToString() + ","

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
            ds = ObjGetData.GetPalletAndTruck(CaseID)
            Weight = FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("CALCULATEDWEIGHT").ToString()), 0)
            Weight = Weight + "#" + index.ToString() + "#" + flag.ToString()
            Return Weight

            Return Weight
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





End Class
