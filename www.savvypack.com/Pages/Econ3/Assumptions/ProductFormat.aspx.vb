Imports System.Data
Imports System.Data.OleDb
Imports System
Imports E3GetData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Econ3_Assumptions_ProductFormat
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

            lblAID.Text = AssumptionId
            lblAdes.Text = Session("Description")

            uName = Session("UserName")
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
        Dim btn As Button
        Dim Link As HyperLink
        Dim dsProd As New DataSet
        Try
            dsProd = objGetData.GetProductFormt("-1", "", "")

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
            Dim Title As String = String.Empty
            Dim UnitError As String = String.Empty
            Dim CurrError As String = String.Empty

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
                ds = objGetData.GetProductFromatIn(arrCaseID(i))
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
                For j = 1 To 9
                    trInner = New TableRow()

                    Select Case j
                        Case 1
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Product Format", trInner, "AlterNateColor1")
                            trInner.ID = "PF_" + i.ToString()

                            'For k = 0 To DataCnt
                            '    lbl = New Label
                            '    lbl.Style.Add("Width", DWidth)
                            '    tdInner = New TableCell
                            '    InnerTdSetting(tdInner, "", "Right")
                            '    hid = New HiddenField
                            '    hid.ID = "hidProductid" + (k + 1).ToString()
                            '    hid.Value = dstbl.Tables(k).Rows(0).Item("M" + i.ToString() + "").ToString()
                            '    GetProductDetails(lbl, dstbl.Tables(k).Rows(0).Item("M" + i.ToString() + "").ToString())
                            '    tdInner.Controls.Add(lbl)
                            '    tdInner.Controls.Add(hid)
                            '    trInner.Controls.Add(tdInner)
                            'Next
                            For k = 0 To DataCnt
                                Link = New HyperLink
                                hid = New HiddenField
                                Link.ID = "hypPFdes" + i.ToString() + "_" + (k + 1).ToString()
                                hid.ID = "hidPFdesid" + i.ToString() + "_" + (k + 1).ToString()
                                Link.CssClass = "Link"

                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                GetProductDetails(Link, hid, dstbl.Tables(k).Rows(0).Item("M" + i.ToString() + "").ToString(), dsProd)
                                If arrCaseID(k) <= 1000 Then
                                    Link.NavigateUrl = Nothing
                                End If
                                tdInner.Controls.Add(hid)
                                tdInner.Controls.Add(Link)
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 2, 3, 4, 5, 6
                            tdInner = New TableCell
                            Title = (j - 1).ToString()
                            LeftTdSetting(tdInner, "Input " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "I" + j.ToString() + "_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                'lbl2 = New Label()
                                txt = New TextBox
                                txt.MaxLength = 6
                                txt.ID = "FORMAT_M" + (k + 1).ToString() + "_" + j.ToString()
                                txt.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("M" + (j).ToString() + "").ToString(), 3).ToString()
                                txt.CssClass = "PrefTextBox"
                                txt.Attributes.Add("OnkeyPress", "return clickButton(event)")
                                If arrCaseID(k) <= 1000 Then
                                    txt.Style.Add("background-color", "#a6a6a6")
                                    txt.Enabled = False
                                End If

                                lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                lbl.ID = "lblFORMAT_M" + (k + 1).ToString() + "_" + j.ToString()
                                lbl.Text = "<b>" + dstbl.Tables(k).Rows(0).Item("FORMAT_M" + (j - 1).ToString() + "").ToString() + "</b><br />"
                                tdInner.Controls.Add(lbl)
                                tdInner.Controls.Add(txt)
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 7
                            tdInner = New TableCell
                            Title = ""
                            LeftTdSetting(tdInner, "Packaging Weight Suggested" + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "PW_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                lbl.ID = "PRODWTSUG" + (k + 1).ToString()

                                lbl2 = New Label
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                lbl2.Text = "<b>" + dstbl.Tables(k).Rows(0).Item("TITLE8").ToString() + "</b><br />"
                                lbl.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("PRODWT").ToString(), 4).ToString()
                                tdInner.Controls.Add(lbl2)
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 8
                            tdInner = New TableCell
                            Title = ""
                            LeftTdSetting(tdInner, "Packaging Weight Preferred" + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "PWS_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)

                                txt = New TextBox
                                txt.MaxLength = 10
                                txt.ID = "PRODWTPREF" + (k + 1).ToString()
                                txt.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("PRODWTPREF").ToString(), 4).ToString()
                                txt.CssClass = "PrefTextBox"
                                txt.Attributes.Add("OnkeyPress", "return clickButton(event)")
                                If arrCaseID(k) <= 1000 Then
                                    txt.Style.Add("background-color", "#a6a6a6")
                                    txt.Enabled = False
                                End If

                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                lbl.Text = "<b>" + dstbl.Tables(k).Rows(0).Item("TITLE8").ToString() + "</b><br />"
                                tdInner.Controls.Add(lbl)
                                tdInner.Controls.Add(txt)
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 9
                            tdInner = New TableCell
                            Title = ""
                            LeftTdSetting(tdInner, "Roll Diameter" + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "RD_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl2 = New Label

                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                lbl.ID = "ROLLD" + (k + 1).ToString()
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                If CDbl(dstbl.Tables(k).Rows(0).Item("ROLLDIA").ToString()) > CDbl(0) Then
                                    lbl2.Text = "<b>" + dstbl.Tables(k).Rows(0).Item("TITLE9").ToString() + "</b><br />"
                                    lbl.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("ROLLDIA").ToString(), 3).ToString()
                                Else
                                    lbl2.Text = "NA"
                                    lbl.Text = ""
                                End If


                                tdInner.Controls.Add(lbl2)
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
            Next



        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetProductDetailsOLD(ByRef lbl As Label, ByVal FromatId As Integer)
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

    Protected Sub GetProductDetails(ByRef LinkMat As HyperLink, ByVal hid As HiddenField, ByVal FromatId As Integer, ByVal dsProd As DataSet)
        Dim Ds As New DataSet
        Dim ObjGetdata As New E1GetData.Selectdata()
        Dim hidval As New HiddenField
        Dim btn As New Button
        Dim Path As String = String.Empty
        Dim dvProd As New DataView
        Dim dtProd As New DataTable
        Try
            'Ds = ObjGetdata.GetProductFormt(FromatId, "", "")
            dvProd = dsProd.Tables(0).DefaultView
            dvProd.RowFilter = "FORMATID = " + FromatId.ToString()
            dtProd = dvProd.ToTable()

            LinkMat.Text = dtProd.Rows(0).Item("FormatDes").ToString()
            hid.Value = FromatId.ToString()
            Path = "../../Econ1/PopUp/GetProdcuts.aspx?Des=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + LinkMat.ClientID + "&Id=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + hid.ClientID + "&Btn=" + btn.ClientID + ""
            LinkMat.NavigateUrl = "javascript:ShowPopWindow('" + Path + "')"

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
    Public Shared Function UpdateCase(ByVal CaseID As String, ByVal index As String, ByVal ProdId As String, ByVal FORMAT_M2 As String, ByVal FORMAT_M3 As String, _
                                      ByVal FORMAT_M4 As String, ByVal FORMAT_M5 As String, ByVal FORMAT_M6 As String, ByVal PRODWTPREF As String, ByVal PF As String) As String
        Try
            Dim ObjGetData As New E1GetData.Selectdata()
            Dim EconConnection As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            Dim StrSqlUpadate As String = ""
            Dim StrSqlIUpadate As String = ""
            Dim odbUtil As New DBUtil()
            Dim dts As New DataSet()
            Dim ObjE3GetData As New E3GetData.Selectdata()
            Dim dsD As New DataSet
            dts = ObjGetData.GetPref(CaseID)
            Dim Convthick As String = dts.Tables(0).Rows(0).Item("convthick")
            Dim Unit As String = dts.Tables(0).Rows(0).Item("Units")
            Dim Convwt As String = dts.Tables(0).Rows(0).Item("CONVWT")
            Dim ObjUpIns As New E1UpInsData.UpdateInsert

            'update inputs
            StrSqlUpadate = ""
            StrSqlIUpadate = ""
            StrSqlUpadate = "UPDATE PRODUCTFORMATIN SET"
            For i = 2 To 4
                Dim InputDetails As Decimal
                If i = 3 Then
                    If CInt(Unit) = 1 And CInt(ProdId) = 1 Then
                        InputDetails = CDbl(FORMAT_M3.Replace(",", "").ToString() / Convthick / 0.01204)
                    Else
                        InputDetails = CDbl(FORMAT_M3.Replace(",", "").ToString() / Convthick)
                    End If
                ElseIf i = 2 Then
                    InputDetails = CDbl(FORMAT_M2.Replace(",", "").ToString() / Convthick)
                ElseIf i = 4 Then
                    InputDetails = CDbl(FORMAT_M4.Replace(",", "").ToString() / Convthick)
                End If
                'Input Details 
                StrSqlIUpadate = StrSqlIUpadate + " M" + i.ToString() + " = " + InputDetails.ToString() + ","

            Next
            StrSqlIUpadate = StrSqlIUpadate + " M1 =" + PF.Replace(",", "").ToString() + ","
            StrSqlIUpadate = StrSqlIUpadate + " M5 =" + FORMAT_M5.Replace(",", "").ToString() + ","
            StrSqlIUpadate = StrSqlIUpadate + " M6 =" + FORMAT_M6.Replace(",", "").ToString() + ","
            StrSqlIUpadate = StrSqlIUpadate + " PWT= " + (CDbl(PRODWTPREF.Replace(",", "").ToString()) / CDbl(Convwt)).ToString() + ","

            StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
            StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
            StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
            odbUtil.UpIns(StrSqlUpadate, EconConnection)

            Dim UserName As String = uName
            ObjUpIns.ServerDateUpdate(CaseID, UserName)

            Dim Econ1Conn As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            Dim obj As New EconCalculation.EconCalculation(Econ1Conn)
            obj.EconCalculate(CaseID)

            dts = ObjGetData.GetProductFromatIn(CaseID)

            Return index + "#" + dts.Tables(0).Rows(0).Item("PRODWT").ToString() + "#" + dts.Tables(0).Rows(0).Item("ROLLDIA").ToString() + "#" + dts.Tables(0).Rows(0).Item("FORMAT_M1").ToString() + "#" + dts.Tables(0).Rows(0).Item("FORMAT_M2").ToString() + "#" + dts.Tables(0).Rows(0).Item("FORMAT_M3").ToString() + "#" + dts.Tables(0).Rows(0).Item("FORMAT_M4").ToString() + "#" + dts.Tables(0).Rows(0).Item("FORMAT_M5").ToString()
        Catch ex As Exception

        End Try
    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function UpdateAllCases(ByVal CaseID As String, ByVal index As String, ByVal ProdId As String, ByVal flag As String, ByVal SessionIDD As String, ByVal CompIDD As String, ByVal FORMAT_M2 As String, ByVal FORMAT_M3 As String, _
                                          ByVal FORMAT_M4 As String, ByVal FORMAT_M5 As String, ByVal FORMAT_M6 As String, ByVal PRODWTPREF As String, ByVal PF As String) As String
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
            Dim PageInfo As String = ""
            dts = ObjGetData.GetPref(CaseID)
            Dim Convthick As String = dts.Tables(0).Rows(0).Item("convthick")
            Dim Unit As String = dts.Tables(0).Rows(0).Item("Units")
            Dim Convwt As String = dts.Tables(0).Rows(0).Item("CONVWT")

            'update inputs
            StrSqlUpadate = ""
            StrSqlIUpadate = ""
            StrSqlUpadate = "UPDATE PRODUCTFORMATIN SET"
            For i = 2 To 4
                Dim InputDetails As Decimal
                If i = 3 Then
                    If CInt(Unit) = 1 And CInt(ProdId) = 1 Then
                        InputDetails = CDbl(FORMAT_M3.Replace(",", "").ToString() / Convthick / 0.01204)
                    Else
                        InputDetails = CDbl(FORMAT_M3.Replace(",", "").ToString() / Convthick)
                    End If
                ElseIf i = 2 Then
                    InputDetails = CDbl(FORMAT_M2.Replace(",", "").ToString() / Convthick)
                ElseIf i = 4 Then
                    InputDetails = CDbl(FORMAT_M4.Replace(",", "").ToString() / Convthick)
                End If
                'Input Details 
                StrSqlIUpadate = StrSqlIUpadate + " M" + i.ToString() + " = " + InputDetails.ToString() + ","

            Next
            StrSqlIUpadate = StrSqlIUpadate + " M1 =" + PF.Replace(",", "").ToString() + ","
            StrSqlIUpadate = StrSqlIUpadate + " M5 =" + FORMAT_M5.Replace(",", "").ToString() + ","
            StrSqlIUpadate = StrSqlIUpadate + " M6 =" + FORMAT_M6.Replace(",", "").ToString() + ","
            StrSqlIUpadate = StrSqlIUpadate + " PWT= " + (CDbl(PRODWTPREF.Replace(",", "").ToString()) / CDbl(Convwt)).ToString() + ","

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


            dts = ObjGetData.GetProductFromatIn(CaseID)

            Return index + "#" + flag.ToString() + "#" + dts.Tables(0).Rows(0).Item("PRODWT").ToString() + "#" + dts.Tables(0).Rows(0).Item("ROLLDIA").ToString() + "#" + dts.Tables(0).Rows(0).Item("FORMAT_M1").ToString() + "#" + dts.Tables(0).Rows(0).Item("FORMAT_M2").ToString() + "#" + dts.Tables(0).Rows(0).Item("FORMAT_M3").ToString() + "#" + dts.Tables(0).Rows(0).Item("FORMAT_M4").ToString() + "#" + dts.Tables(0).Rows(0).Item("FORMAT_M5").ToString()


        Catch ex As Exception

        End Try
    End Function




End Class
