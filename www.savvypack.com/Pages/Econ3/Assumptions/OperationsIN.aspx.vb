Imports System.Data
Imports System.Data.OleDb
Imports System
Imports E3GetData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Econ3_Assumptions_OperationsIN
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
            Dim IR As String
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
                ds = objGetData.GetOperationInDetails(arrCaseID(i))
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
            For i = 1 To 30
                For j = 1 To 11
                    trInner = New TableRow()

                    Select Case j
                        Case 1
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "<b>Equipment " + i.ToString() + "</b>", trInner, "AlterNateColor4")
                          
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
                            'tdInner = New TableCell
                            'LeftTdSetting(tdInner, "Equipment Description", trInner, "AlterNateColor1")
                            'trInner.ID = "ED_" + i.ToString()

                            'For k = 0 To DataCnt
                            '    lbl = New Label
                            '    lbl.Style.Add("Width", DWidth)
                            '    tdInner = New TableCell
                            '    InnerTdSetting(tdInner, "", "Right")
                            '    lbl.Text = dstbl.Tables(k).Rows(0).Item("EQUIPDES" + i.ToString() + "").ToString()
                            '    tdInner.Controls.Add(lbl)
                            '    trInner.Controls.Add(tdInner)
                            'Next
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Equipment Description", trInner, "AlterNateColor1")
                            trInner.ID = "ED_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                If dstbl.Tables(k).Rows(0).Item("LEQUIPDES" + i.ToString() + "").ToString() <> "" Then
                                    lbl.Text = dstbl.Tables(k).Rows(0).Item("LEQUIPDES" + i.ToString() + "").ToString()
                                Else
                                    lbl.Text = dstbl.Tables(k).Rows(0).Item("EQUIPDES" + i.ToString() + "").ToString()
                                End If
                                'lbl.Text = dstbl.Tables(k).Rows(0).Item("EQUIPDES" + i.ToString() + "").ToString()
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 3

                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("WEBWIDTHUNIT").ToString() + ")"
                            LeftTdSetting(tdInner, "Web Width/Cavities Sugg." + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "WWS_" + i.ToString()
                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.ID = "WEBWIDTHS_" + i.ToString() + "_" + (k + 1).ToString()
                                lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                If dstbl.Tables(k).Rows(0).Item("EqUnit" + i.ToString() + "").ToString() = "fpm" Then
                                    lbl.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("OPWEBWIDTHSUGG" + i.ToString() + "").ToString(), 2).ToString()
                                ElseIf dstbl.Tables(k).Rows(0).Item("EqUnit" + i.ToString() + "").ToString() = "cpm" Then
                                    lbl.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("OPEXITSSUGG" + i.ToString() + "").ToString(), 2).ToString()
                                End If
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 4

                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("WEBWIDTHUNIT").ToString() + ")"
                            LeftTdSetting(tdInner, "Web Width/Cavities Pref." + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "WWP_" + i.ToString()
                            For k = 0 To DataCnt
                                txt = New TextBox
                                txt.MaxLength = 6
                                txt.Width = 70
                                txt.ID = "txtWWP_" + i.ToString() + "_" + (k + 1).ToString()
                                txt.Attributes.Add("OnkeyPress", "return clickButton(event)")
                                'txt.Style.Add("Width", "100px")
                                txt.CssClass = "PrefTextBox"
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                If dstbl.Tables(k).Rows(0).Item("EqUnit" + i.ToString() + "").ToString() = "fpm" Then
                                    ' lbl.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("OPWEBWIDTHPREF" + i.ToString() + "").ToString(), 2).ToString()
                                    txt.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("OPWEBWIDTHPREF" + i.ToString() + "").ToString(), 2).ToString()
                                ElseIf dstbl.Tables(k).Rows(0).Item("EqUnit" + i.ToString() + "").ToString() = "cpm" Then
                                    'lbl.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("OPEXITSPREF" + i.ToString() + "").ToString(), 2).ToString()
                                    txt.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("OPEXITSPREF" + i.ToString() + "").ToString(), 2).ToString()
                                Else
                                    txt.Text = "0.00"
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
                            LeftTdSetting(tdInner, "Maximum Annual Run Hours", trInner, "AlterNateColor1")
                            trInner.ID = "MRH_" + i.ToString()
                            For k = 0 To DataCnt
                                txt = New TextBox
                                txt.MaxLength = 6
                                txt.Width = 70
                                txt.ID = "txtMRH_" + i.ToString() + "_" + (k + 1).ToString()
                                txt.Attributes.Add("OnkeyPress", "return clickButton(event)")
                                'txt.Style.Add("Width", "100px")
                                txt.CssClass = "PrefTextBox"
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                If dstbl.Tables(k).Rows(0).Item("OMAXRH" + i.ToString() + "").ToString() <> "" Then
                                    txt.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("OMAXRH" + i.ToString() + "").ToString(), 0).ToString()
                                Else
                                    txt.Text = "0"
                                End If
                                If arrCaseID(k) <= 1000 Then
                                    txt.Style.Add("background-color", "#a6a6a6")
                                    txt.Enabled = False
                                End If
                                tdInner.Controls.Add(txt)
                                trInner.Controls.Add(tdInner)
                            Next
                           
                        Case 6
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Instantaneous Rate", trInner, "AlterNateColor2")
                            trInner.ID = "IR1_" + i.ToString()
                            For k = 0 To DataCnt
                                txt = New TextBox
                                txt.MaxLength = 6
                                txt.Width = 70
                                txt.ID = "txtIR1_" + i.ToString() + "_" + (k + 1).ToString()
                                txt.Attributes.Add("OnkeyPress", "return clickButton(event)")
                                'txt.Style.Add("Width", "100px")
                                txt.CssClass = "PrefTextBox"
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                If dstbl.Tables(k).Rows(0).Item("OPINSTR" + i.ToString() + "").ToString() <> "" Then
                                    txt.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("OPINSTR" + i.ToString() + "").ToString(), 2).ToString()
                                Else
                                    txt.Text = "0.00"
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
                            LeftTdSetting(tdInner, "Units ", trInner, "AlterNateColor2")
                            trInner.ID = "UT_" + i.ToString()
                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                lbl.Text = dstbl.Tables(k).Rows(0).Item("Unit" + i.ToString() + "").ToString()
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 8
                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("INSTRUNIT").ToString() + "/hr)"
                            LeftTdSetting(tdInner, "Instantaneous Rate " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "IR2_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                lbl.ID = "INSTRATE_" + i.ToString() + "_" + (k + 1).ToString()
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                lbl.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("INSTR" + i.ToString() + "").ToString(), 2).ToString()
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 9
                            tdInner = New TableCell
                            Title = "(%)"
                            LeftTdSetting(tdInner, "Downtime " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "DT_" + i.ToString()
                            For k = 0 To DataCnt
                                txt = New TextBox
                                txt.MaxLength = 6
                                txt.Width = 70
                                txt.ID = "txtDT_" + i.ToString() + "_" + (k + 1).ToString()
                                txt.Attributes.Add("OnkeyPress", "return clickButton(event)")
                                'txt.Style.Add("Width", "100px")
                                txt.CssClass = "PrefTextBox"
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                If dstbl.Tables(k).Rows(0).Item("DT" + i.ToString() + "").ToString() <> "" Then
                                    txt.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("DT" + i.ToString() + "").ToString(), 1).ToString()
                                Else
                                    txt.Text = "0.0"
                                End If
                                If arrCaseID(k) <= 1000 Then
                                    txt.Style.Add("background-color", "#a6a6a6")
                                    txt.Enabled = False
                                End If
                                tdInner.Controls.Add(txt)
                                trInner.Controls.Add(tdInner)
                            Next
                          
                        Case 10
                            tdInner = New TableCell
                            Title = "(%)"
                            LeftTdSetting(tdInner, "Production Waste " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "WS_" + i.ToString()
                            For k = 0 To DataCnt
                                txt = New TextBox
                                txt.MaxLength = 6
                                txt.Width = 70
                                txt.ID = "txtWS_" + i.ToString() + "_" + (k + 1).ToString()
                                txt.Attributes.Add("OnkeyPress", "return clickButton(event)")
                                'txt.Style.Add("Width", "100px")
                                txt.CssClass = "PrefTextBox"
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                If dstbl.Tables(k).Rows(0).Item("OPWASTE" + i.ToString() + "").ToString() <> "" Then
                                    txt.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("OPWASTE" + i.ToString() + "").ToString(), 1).ToString()
                                Else
                                    txt.Text = "0.0"
                                End If
                                If arrCaseID(k) <= 1000 Then
                                    txt.Style.Add("background-color", "#a6a6a6")
                                    txt.Enabled = False
                                End If
                                tdInner.Controls.Add(txt)
                                trInner.Controls.Add(tdInner)
                            Next
                           
                        Case 11
                            tdInner = New TableCell
                            Title = "(%)"
                            LeftTdSetting(tdInner, "Design Waste " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "DWS_" + i.ToString()
                            For k = 0 To DataCnt
                                txt = New TextBox
                                txt.MaxLength = 6
                                txt.Width = 70
                                txt.ID = "txtDWS_" + i.ToString() + "_" + (k + 1).ToString()
                                txt.Attributes.Add("OnkeyPress", "return clickButton(event)")
                                'txt.Style.Add("Width", "100px")
                                txt.CssClass = "PrefTextBox"
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                If dstbl.Tables(k).Rows(0).Item("DESIGNWAST" + i.ToString() + "").ToString() <> "" Then
                                    txt.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("DESIGNWAST" + i.ToString() + "").ToString(), 1).ToString()
                                Else
                                    txt.Text = "0.0"
                                End If
                                If arrCaseID(k) <= 1000 Then
                                    txt.Style.Add("background-color", "#a6a6a6")
                                    txt.Enabled = False
                                End If
                                tdInner.Controls.Add(txt)
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
    Public Shared Function UpdateAllCases(ByVal CaseID As String, ByVal index As String, ByVal flag As String, ByVal OpExit() As String, ByVal OMAXRH() As String, ByVal OPINSTR() As String, ByVal DT() As String, ByVal OPWASTE() As String, ByVal DWASTE() As String) As Array

        Dim ObjE3GetData As New E3GetData.Selectdata()
        Dim objGetData As New E1GetData.Selectdata
        Dim EconConnection As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
        Dim StrSqlUpadate As String = ""
        Dim StrSqlIUpadate As String = ""
        Dim odbUtil As New DBUtil()
        Dim dts As New DataSet()
        Dim dsD As New DataSet
        Dim ArrVal(30) As String
        Dim EqUnit(29) As String
        Dim ds As New DataSet
        Dim IR(30) As String
        'Preferences
        dts = objGetData.GetPref(CaseID)
        Dim Curr As String = dts.Tables(0).Rows(0).Item("curr")
        Dim Convthick As String = dts.Tables(0).Rows(0).Item("convthick")
        Dim Convthick2 As String = dts.Tables(0).Rows(0).Item("convthick2")
        ds = objGetData.GetOperationInDetails(CaseID)
        For k = 1 To 30
            EqUnit(k - 1) = ds.Tables(0).Rows(0).Item("EqUnit" + k.ToString()).ToString()
        Next


        Try


            'Updating Web Width
            '---------------------------------------
            StrSqlUpadate = ""
            StrSqlIUpadate = ""
            For i = 0 To 29
                EqUnit(i) = ds.Tables(0).Rows(0).Item("EqUnit" + (i + 1).ToString()).ToString()
                If EqUnit(i) = "fpm" Then
                    Dim opwidth As Integer
                    opwidth = i + 1
                    If OpExit(i) <> Nothing Then
                        OpExit(i) = OpExit(i).ToString().Replace(",", "")
                        Dim dblOpExit As Decimal
                        dblOpExit = CDbl(OpExit(i) / Convthick)
                        'dblOpExit = CDbl(OpExit(i))
                        StrSqlUpadate = "UPDATE OPwebwidth SET"
                        StrSqlIUpadate = StrSqlIUpadate + " M" + opwidth.ToString() + " = " + dblOpExit.ToString() + ","
                    End If
                End If
            Next
            If (StrSqlIUpadate.Length <> 0) Then
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, EconConnection)
            End If

            '-------- 'Updating OPExits----------
            StrSqlUpadate = ""
            StrSqlIUpadate = ""
            For i = 0 To 29
                If EqUnit(i) = "cpm" Then
                    Dim opexits As Integer
                    opexits = i + 1
                    If OpExit(i) <> Nothing Then
                        OpExit(i) = OpExit(i).ToString().Replace(",", "")
                        Dim dblOpExit As Decimal
                        'dblOpExit = CDbl(OpExit(i) / Convthick)
                        dblOpExit = CDbl(OpExit(i))
                        StrSqlUpadate = "UPDATE OPEXITS SET"
                        StrSqlIUpadate = StrSqlIUpadate + " M" + opexits.ToString() + " = " + dblOpExit.ToString() + ","
                    End If
                End If
            Next
            If (StrSqlIUpadate.Length <> 0) Then
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, EconConnection)
            End If

            '-----Updating Maximum Annual Run Hours---------
            StrSqlUpadate = ""
            StrSqlIUpadate = ""
            Dim maxRunHrs As New Integer
            StrSqlUpadate = "UPDATE OPmaxRUNhrs SET"
            For i = 0 To 29
                maxRunHrs = i + 1
                StrSqlIUpadate = StrSqlIUpadate + " M" + maxRunHrs.ToString() + " = " + OMAXRH(i).ToString().Replace(",", "") + ","
            Next
            StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
            StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
            StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
            odbUtil.UpIns(StrSqlUpadate, EconConnection)


            '-----------Updating Instantaneous Rate -------------
            StrSqlUpadate = ""
            StrSqlIUpadate = ""
            Dim instRate As New Integer
            StrSqlUpadate = "UPDATE OPinstGRSrate SET"
            For i = 0 To 29
                instRate = i + 1
                If EqUnit(i) = "fpm" Then
                    Dim dblInstRate As Decimal = CDbl(OPINSTR(i).Replace(",", "") / Convthick2)
                    StrSqlIUpadate = StrSqlIUpadate + " M" + instRate.ToString() + " = " + dblInstRate.ToString() + ","
                Else
                    StrSqlIUpadate = StrSqlIUpadate + " M" + instRate.ToString() + " = " + OPINSTR(i).ToString().Replace(",", "") + ","
                End If

            Next
            StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
            StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
            StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
            odbUtil.UpIns(StrSqlUpadate, EconConnection)


            '--------------Updating Downtime -------------
            StrSqlUpadate = ""
            StrSqlIUpadate = ""
            Dim downT As New Integer
            StrSqlUpadate = "UPDATE OPdowntime SET"
            For i = 0 To 29
                downT = i + 1
                StrSqlIUpadate = StrSqlIUpadate + " M" + downT.ToString() + " = " + DT(i).ToString().Replace(",", "") + ","
            Next
            StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
            StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
            StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
            odbUtil.UpIns(StrSqlUpadate, EconConnection)

            '---------------Updating waste--------------
            StrSqlUpadate = ""
            StrSqlIUpadate = ""
            Dim wasteVal As New Integer
            StrSqlUpadate = "UPDATE OPwaste SET"
            For i = 0 To 29
                wasteVal = i + 1
                StrSqlIUpadate = StrSqlIUpadate + " M" + wasteVal.ToString() + " = " + OPWASTE(i).ToString().Replace(",", "") + ","
                StrSqlIUpadate = StrSqlIUpadate + " W" + wasteVal.ToString() + " = " + DWASTE(i).ToString().Replace(",", "") + ","
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

            Dim dsOp As DataSet
            dsOp = objGetData.GetOperationInDetails(CaseID)

            Dim l As Integer = 0
            dsOp = objGetData.GetOperationInDetails(CaseID)
            For i = 0 To 29

                IR(i) = FormatNumber(CDbl(dsOp.Tables(0).Rows(0).Item("INSTR" + (i + 1).ToString()).ToString()), 2)
                If i = 0 Then
                    IR(i) = IR(i) + "#" + index.ToString() + "#" + flag
                End If
            Next
            Return IR
        Catch ex As Exception

        End Try
    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function UpdateCase(ByVal CaseID As String, ByVal index As String, ByVal OpExit() As String, ByVal OMAXRH() As String, ByVal OPINSTR() As String, ByVal DT() As String, ByVal OPWASTE() As String, ByVal DWASTE() As String) As Array
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
            Dim EqUnit(29) As String
            Dim ds As New DataSet

            'Preferences
            dts = ObjGetData.GetPref(CaseID)
            Dim Curr As String = dts.Tables(0).Rows(0).Item("curr")
            Dim Convthick As String = dts.Tables(0).Rows(0).Item("convthick")
            Dim Convthick2 As String = dts.Tables(0).Rows(0).Item("convthick2")
            Dim IR(30) As String

            ds = ObjGetData.GetOperationInDetails(CaseID)
            For k = 1 To 30
                EqUnit(k - 1) = ds.Tables(0).Rows(0).Item("EqUnit" + k.ToString()).ToString()
            Next


            'Updating Web Width
            '---------------------------------------
            StrSqlUpadate = ""
            StrSqlIUpadate = ""
            For i = 0 To 29
                EqUnit(i) = ds.Tables(0).Rows(0).Item("EqUnit" + (i + 1).ToString()).ToString()
                If EqUnit(i) = "fpm" Then
                    Dim opwidth As Integer
                    opwidth = i + 1
                    If OpExit(i) <> Nothing Then
                        OpExit(i) = OpExit(i).ToString().Replace(",", "")
                        Dim dblOpExit As Decimal
                        dblOpExit = CDbl(OpExit(i) / Convthick)
                        'dblOpExit = CDbl(OpExit(i))
                        StrSqlUpadate = "UPDATE OPwebwidth SET"
                        StrSqlIUpadate = StrSqlIUpadate + " M" + opwidth.ToString() + " = " + dblOpExit.ToString() + ","
                    End If
                End If
            Next
            If (StrSqlIUpadate.Length <> 0) Then
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, EconConnection)
            End If

            '-------- 'Updating OPExits----------
            StrSqlUpadate = ""
            StrSqlIUpadate = ""
            For i = 0 To 29
                If EqUnit(i) = "cpm" Then
                    Dim opexits As Integer
                    opexits = i + 1
                    If OpExit(i) <> Nothing Then
                        OpExit(i) = OpExit(i).ToString().Replace(",", "")
                        Dim dblOpExit As Decimal
                        'dblOpExit = CDbl(OpExit(i) / Convthick)
                        dblOpExit = CDbl(OpExit(i))
                        StrSqlUpadate = "UPDATE OPEXITS SET"
                        StrSqlIUpadate = StrSqlIUpadate + " M" + opexits.ToString() + " = " + dblOpExit.ToString() + ","
                    End If
                End If
            Next
            If (StrSqlIUpadate.Length <> 0) Then
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, EconConnection)
            End If


            '-----Updating Maximum Annual Run Hours---------
            StrSqlUpadate = ""
            StrSqlIUpadate = ""
            Dim maxRunHrs As New Integer
            StrSqlUpadate = "UPDATE OPmaxRUNhrs SET"
            For i = 0 To 29
                maxRunHrs = i + 1
                StrSqlIUpadate = StrSqlIUpadate + " M" + maxRunHrs.ToString() + " = " + OMAXRH(i).ToString().Replace(",", "") + ","
            Next
            StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
            StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
            StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
            odbUtil.UpIns(StrSqlUpadate, EconConnection)


            '-----------Updating Instantaneous Rate -------------
            StrSqlUpadate = ""
            StrSqlIUpadate = ""
            Dim instRate As New Integer
            StrSqlUpadate = "UPDATE OPinstGRSrate SET"
            For i = 0 To 29
                EqUnit(i) = ds.Tables(0).Rows(0).Item("EqUnit" + (i + 1).ToString()).ToString()
                instRate = i + 1
                If EqUnit(i) = "fpm" Then
                    Dim dblInstRate As Decimal = CDbl(OPINSTR(i).Replace(",", "") / Convthick2)
                    StrSqlIUpadate = StrSqlIUpadate + " M" + instRate.ToString() + " = " + dblInstRate.ToString() + ","
                Else
                    StrSqlIUpadate = StrSqlIUpadate + " M" + instRate.ToString() + " = " + OPINSTR(i).ToString().Replace(",", "") + ","
                End If

            Next
            StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
            StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
            StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
            odbUtil.UpIns(StrSqlUpadate, EconConnection)


            '--------------Updating Downtime -------------
            StrSqlUpadate = ""
            StrSqlIUpadate = ""
            Dim downT As New Integer
            StrSqlUpadate = "UPDATE OPdowntime SET"
            For i = 0 To 29
                downT = i + 1
                StrSqlIUpadate = StrSqlIUpadate + " M" + downT.ToString() + " = " + DT(i).ToString().Replace(",", "") + ","
            Next
            StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
            StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
            StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
            odbUtil.UpIns(StrSqlUpadate, EconConnection)

            '---------------Updating waste--------------
            StrSqlUpadate = ""
            StrSqlIUpadate = ""
            Dim wasteVal As New Integer
            StrSqlUpadate = "UPDATE OPwaste SET"
            For i = 0 To 29
                wasteVal = i + 1
                StrSqlIUpadate = StrSqlIUpadate + " M" + wasteVal.ToString() + " = " + OPWASTE(i).ToString().Replace(",", "") + ","
                StrSqlIUpadate = StrSqlIUpadate + " W" + wasteVal.ToString() + " = " + DWASTE(i).ToString().Replace(",", "") + ","
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


            Dim dsOp As DataSet
            dsOp = ObjGetData.GetOperationInDetails(CaseID)

            For i = 0 To 29
                IR(i) = FormatNumber(CDbl(dsOp.Tables(0).Rows(0).Item("INSTR" + (i + 1).ToString()).ToString()), 2)
                If i = 0 Then
                    IR(i) = IR(i) + "#" + index.ToString()
                End If

            Next
            Return IR

        Catch ex As Exception

        End Try
    End Function




End Class
