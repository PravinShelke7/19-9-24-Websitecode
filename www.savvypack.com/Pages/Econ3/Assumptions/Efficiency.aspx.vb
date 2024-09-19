Imports System.Data
Imports System.Data.OleDb
Imports System
Imports E3GetData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Econ3_Efficiency
    Inherits System.Web.UI.Page

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
        Updatebtn.Visible = Visible

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
        Dim l As New Integer
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
            Dim txt As TextBox
            Dim chk As New CheckBox
            Dim Cunits As New Integer
            Dim Units As New Integer
            Dim CurrTitle As String = String.Empty
            Dim CCurrTitle As String = String.Empty
            Dim Title As String = String.Empty
            Dim btn As Button
            Dim hid As HiddenField
            Dim objGetDataW As New E3GetData.Selectdata
            Dim dsW As New DataSet

            Dim UnitError As String = String.Empty
            Dim CurrError As String = String.Empty

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
                ds = objGetData.GetEffiencyDetails(arrCaseID(i))
                ds.Tables(0).TableName = arrCaseID(i).ToString()
                dstbl.Tables.Add(ds.Tables(arrCaseID(i).ToString()).Copy())
            Next

            For i = 0 To DataCnt
                dsCaseDetails = objGetData.GetCaseDetails(arrCaseID(i).ToString())
                'tdHeader = New TableCell
                'Dim Headertext As String = String.Empty
                'Headertext = "Case#:" + arrCaseID(i).ToString() + "<br/>" + dsCaseDetails.Tables(0).Rows(0).Item("CaseDes").ToString() + "<input type='hidden' value='" + arrCaseID(i).ToString() + "' name='Case" + i.ToString() + "'/>"

                'CaseDesp.Add(arrCaseID(i).ToString())
                'HeaderTdSetting(tdHeader, DWidth, Headertext, 1)
                'trHeader.Controls.Add(tdHeader)
                dsCaseDetails = objGetData.GetCaseDetails(arrCaseID(i).ToString())
                Cunits = Convert.ToInt32(dstbl.Tables(0).Rows(0).Item("Units").ToString())
                Units = Convert.ToInt32(dstbl.Tables(i).Rows(0).Item("Units").ToString())
                CCurrTitle = dstbl.Tables(0).Rows(0).Item("TITLE2").ToString().Trim()
                CurrTitle = dstbl.Tables(i).Rows(0).Item("TITLE2").ToString().Trim()

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
            For i = 1 To 15
                For j = 0 To 12
                    trInner = New TableRow()

                    Select Case j
                        Case 0
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "", trInner, "TdHeading")
                            tdInner.CssClass = "TdHeading"
                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                tdInner.CssClass = "TdHeading"
                                If i = 1 Then
                                    btn = New Button
                                    btn.ID = "btn_" + (k + 1).ToString()
                                    btn.Text = "Update"
                                    btn.Height = 15
                                    btn.Style.Add("font-size", "9px")
                                    btn.CommandArgument = arrCaseID(k).ToString()
                                    btn.Attributes.Add("onClick", "return Update('" + arrCaseID(k).ToString() + "','" + (k).ToString() + "'," + DataCnt.ToString() + ");")
                                    If arrCaseID(k) <= 1000 Then
                                        btn.Enabled = False
                                        btn.Style.Add("background-color", "#a6a6a6")
                                        btn.Style.Add("color", "#4d4d4d")
                                    End If
                                    tdInner.Controls.Add(btn)
                                Else
                                    tdInner.Text = "&nbsp;"
                                End If
                                InnerTdSetting(tdInner, "", "Center")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 1
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "<b>Layer" + i.ToString() + "</b>", trInner, "AlterNateColor4")
                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Center")
                                tdInner.Text = "&nbsp;"
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 2
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "<b>Department Selections</b>", trInner, "AlterNateColor3")
                            trInner.ID = "MS_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                hid = New HiddenField
                                hid.ID = "Material" + i.ToString()

                                lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Left")
                                lbl.Text = dstbl.Tables(k).Rows(0).Item("PROC" + i.ToString() + "").ToString()
                                hid.Value = dstbl.Tables(k).Rows(0).Item("PROC" + i.ToString() + "").ToString()

                                lbl.Font.Bold = True
                                tdInner.Controls.Add(lbl)
                                tdInner.Controls.Add(hid)

                                trInner.Controls.Add(tdInner)
                            Next
                        Case 3, 4, 5, 6, 7, 8, 9, 10, 11, 12
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Material. " + (j - 2).ToString() + " ", trInner, "AlterNateColor2")
                            trInner.ID = "DN" + (j - 2).ToString() + "_" + i.ToString()
                            For k = 0 To DataCnt
                                lbl = New Label
                                hid = New HiddenField
                                hid.ID = "MAT" + Chr(64 + Convert.ToInt32(i.ToString())) + "" + (j - 2).ToString() + "" + k.ToString()
                                chk = New CheckBox
                                chk.ID = "chkMAT" + Chr(64 + Convert.ToInt32(i.ToString())) + "" + (j - 2).ToString() + "" + k.ToString()
                                lbl.ID = "lblMAT" + Chr(64 + Convert.ToInt32(i.ToString())) + "" + (j - 2).ToString() + "" + k.ToString()

                                If dstbl.Tables(k).Rows(0).Item("MAT" + Chr(64 + Convert.ToInt32(i.ToString())) + "" + (j - 2).ToString() + "").ToString() > 0 Then
                                    chk.Checked = True
                                Else
                                    chk.Checked = False
                                End If
                                If arrCaseID(k) <= 1000 Then
                                    chk.Enabled = False
                                End If
                                'chk.Enabled = False
                                lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Left")
                                If dstbl.Tables(k).Rows(0).Item("MATDES" + (j - 2).ToString() + "").ToString() <> "" Then
                                    lbl.Text = dstbl.Tables(k).Rows(0).Item("MATDES" + (j - 2).ToString() + "").ToString()
                                Else
                                    lbl.Text = dstbl.Tables(k).Rows(0).Item("MAT" + (j - 2).ToString() + "").ToString()
                                End If
                                tdInner.Controls.Add(chk)
                                tdInner.Controls.Add(lbl)
                                tdInner.Controls.Add(hid)
                                trInner.Controls.Add(tdInner)
                            Next
                            If ((j - 2) Mod 2 = 0) Then
                                trInner.CssClass = "AlterNateColor2"
                            Else
                                trInner.CssClass = "AlterNateColor1"
                            End If
                    End Select
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
    Public Shared Function UpdateAllCases(ByVal CaseID As String, ByVal unit() As String, ByVal code As String) As String
        Try
            Dim ObjGetData As New E1GetData.Selectdata()
            Dim ObjE3GetData As New E3GetData.Selectdata()
            Dim EconConnection As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            Dim StrSqlUpadate As String = ""
            Dim StrSqlIUpadate As String = ""
            Dim odbUtil As New DBUtil()
            Dim str As String = String.Empty

            If code = "A" Then
                str = "T"
            ElseIf code = "B" Then
                str = "S"
            ElseIf code = "C" Then
                str = "Y"
            ElseIf code = "D" Then
                str = "D"
            ElseIf code = "E" Then
                str = "E"
            ElseIf code = "F" Then
                str = "Z"
            ElseIf code = "G" Then
                str = "B"
            ElseIf code = "H" Then
                str = "R"
            ElseIf code = "I" Then
                str = "K"
            ElseIf code = "J" Then
                str = "P"
                'dt
            ElseIf code = "K" Then
                str = "Q"
            ElseIf code = "L" Then
                str = "L"
            ElseIf code = "M" Then
                str = "U"
            ElseIf code = "N" Then
                str = "N"
            ElseIf code = "O" Then
                str = "V"
            End If

            'Updating No of Asset
            StrSqlUpadate = ""
            StrSqlIUpadate = ""
            StrSqlUpadate = "UPDATE MaterialEFF SET"
            For Mat = 0 To 9
                StrSqlIUpadate = StrSqlIUpadate + " " + str.ToString() + "" + (Mat + 1).ToString() + "=" + unit(Mat) + ","
            Next
            StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
            StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
            StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
            odbUtil.UpIns(StrSqlUpadate, EconConnection)

            Dim Econ1Conn As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            Dim obj As New EconCalculation.EconCalculation(Econ1Conn)
            obj.EconCalculate(CaseID)

            Return " "
        Catch ex As Exception

        End Try
    End Function





End Class
