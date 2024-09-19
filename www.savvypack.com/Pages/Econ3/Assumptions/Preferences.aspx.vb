Imports System.Data
Imports System.Data.OleDb
Imports System
Imports E3GetData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Econ3_Assumptions_Preferences
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
               

                If Not IsPostBack Then
 		    GetPageDetails()
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


            Dim rdeng As RadioButton
            Dim rdmatric As RadioButton
            Dim trHeader As New TableRow
            Dim tdHeader As TableCell
            Dim tdInner As TableCell
            Dim trInner As New TableRow
            Dim ddl As DropDownList
            Dim lbl As New Label
            Dim lbl2 As New Label
            Dim txt As TextBox
            Dim btn As Button
            Dim Cunits As New Integer
            Dim Units As New Integer
            Dim CurrTitle As String = String.Empty
            Dim CCurrTitle As String = String.Empty
            '  Dim CurrError As String = String.Empty
            Dim CurrError As Label
            Dim UError As Label

            Dim Title As String = String.Empty
            Dim dsW As New DataSet
            Dim objGetDataW As New E3GetData.Selectdata
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
                ds = objGetData.GetPref(arrCaseID(i))
                ds.Tables(0).TableName = arrCaseID(i).ToString()
                dstbl.Tables.Add(ds.Tables(arrCaseID(i).ToString()).Copy())
            Next

            For i = 0 To DataCnt
                dsCaseDetails = objGetData.GetCaseDetails(arrCaseID(i).ToString())
                Cunits = Convert.ToInt32(dstbl.Tables(0).Rows(0).Item("Units").ToString())
                Units = Convert.ToInt32(dstbl.Tables(i).Rows(0).Item("Units").ToString())
                CCurrTitle = dstbl.Tables(0).Rows(0).Item("CURRENCY").ToString().Trim()
                CurrTitle = dstbl.Tables(i).Rows(0).Item("CURRENCY").ToString().Trim()
                CurrError = New Label()
                UError  = New Label()
                UError.ID = "lblu" + i.ToString()
                CurrError.ID = "lblc" + i.ToString()
                If CCurrTitle <> CurrTitle Then
                    CurrError.Text = "<br/> <span  style='color:red'>Currency Mismatch</span>"
                Else
                    CurrError.Text = ""
                End If
                If Cunits <> Units Then
                    UError.Text = "<br/> <span  style='color:red'>Unit Mismatch</span>"
                Else
                    UError.Text = ""
                End If

                tdHeader = New TableCell
                'Dim Headertext As String = String.Empty
                Dim Headertext As New Label
                Headertext.ID = "lblh" + i.ToString()
                If Cunits <> Units Then
                    Headertext.Text = "Case#:" + arrCaseID(i).ToString() + "<br/>" + dsCaseDetails.Tables(0).Rows(0).Item("CaseDes").ToString() + "<input type='hidden' value='" + arrCaseID(i).ToString() + "' name='Case" + i.ToString() + "'/>"
                Else
                    Headertext.Text = "Case#:" + arrCaseID(i).ToString() + "<br/>" + dsCaseDetails.Tables(0).Rows(0).Item("CaseDes").ToString() + "<input type='hidden' value='" + arrCaseID(i).ToString() + "' name='Case" + i.ToString() + "'/>"
                End If
                tdHeader.Controls.Add(Headertext)

                tdHeader.Controls.Add(CurrError)
                tdHeader.Controls.Add(UError)
                CaseDesp.Add(arrCaseID(i).ToString())

                'HeaderTdSetting(tdHeader, DWidth, "", 1)

                tdHeader.ColumnSpan = 1
                If DWidth <> "" Then
                    tdHeader.Style.Add("width", DWidth)
                End If
                tdHeader.CssClass = "TdHeading"
                tdHeader.Height = 30
                tdHeader.HorizontalAlign = HorizontalAlign.Center
                trHeader.Controls.Add(tdHeader)


            Next
            tblComparision.Controls.Add(trHeader)
            For i = 1 To 1
                For j = 0 To 8
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
                            LeftTdSetting(tdInner, "Country of Manufacture: ", trInner, "")
                            trInner.ID = "CM" + i.ToString()
                            'For k = 0 To DataCnt
                            '    lbl = New Label()
                            '    tdInner = New TableCell
                            '    GetCountryDetails(lbl, dstbl.Tables(k).Rows(0).Item("OCOUNTRY").ToString())
                            '    tdInner.Controls.Add(lbl)
                            '    InnerTdSetting(tdInner, "", "Right")
                            '    trInner.Controls.Add(tdInner)
                            'Next
                            For k = 0 To DataCnt
                                lbl = New Label()
                                ddl = New DropDownList
                                ddl.CssClass = "dropdownunit"

                                tdInner = New TableCell
                                ddl.ID = "ddlOid_" + k.ToString() + "_" + j.ToString() + "_" + i.ToString()
                                GetCountryDetails(ddl, dstbl.Tables(k).Rows(0).Item("OCOUNTRY").ToString())
                                If arrCaseID(k) <= 1000 Then
                                    ddl.Style.Add("background-color", "#a6a6a6")
                                    ddl.Enabled = False
                                End If
                                tdInner.Controls.Add(ddl)
                                InnerTdSetting(tdInner, "", "right")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 2
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Country of Destination", trInner, "")
                            trInner.ID = "CD" + i.ToString()
                            'For k = 0 To DataCnt
                            '    lbl = New Label()
                            '    tdInner = New TableCell
                            '    GetCountryDetails(lbl, dstbl.Tables(k).Rows(0).Item("DCOUNTRY").ToString())
                            '    tdInner.Controls.Add(lbl)
                            '    InnerTdSetting(tdInner, "", "Right")
                            '    trInner.Controls.Add(tdInner)
                            'Next
                            For k = 0 To DataCnt
                                lbl = New Label()
                                tdInner = New TableCell
                                ddl = New DropDownList
                                ddl.CssClass = "dropdownunit"
                                ddl.ID = "ddlOid_" + k.ToString() + "_" + j.ToString() + "_" + i.ToString()

                                GetCountryDetails(ddl, dstbl.Tables(k).Rows(0).Item("DCOUNTRY").ToString())
                                If arrCaseID(k) <= 1000 Then
                                    ddl.Style.Add("background-color", "#a6a6a6")
                                    ddl.Enabled = False
                                End If
                                tdInner.Controls.Add(ddl)
                                InnerTdSetting(tdInner, "", "right")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 3
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Prefferred Unit", trInner, "")
                            trInner.ID = "PREFU" + i.ToString()
                            'For k = 0 To DataCnt
                            '    lbl = New Label()
                            '    tdInner = New TableCell
                            '    GetUnitDetails(lbl, dstbl.Tables(k).Rows(0).Item("UNITS").ToString())
                            '    tdInner.Controls.Add(lbl)
                            '    InnerTdSetting(tdInner, "", "Right")
                            '    trInner.Controls.Add(tdInner)
                            'Next
                            For k = 0 To DataCnt
                                lbl = New Label()
                                rdeng = New RadioButton
                                rdmatric = New RadioButton
                                rdeng.GroupName = "unit" + k.ToString()
                                rdmatric.GroupName = "unit" + k.ToString()
                                rdeng.ID = "rdeid_" + k.ToString() + "_" + j.ToString() + "_" + i.ToString()
                                rdmatric.ID = "rdmid_" + k.ToString() + "_" + j.ToString() + "_" + i.ToString()
                                rdmatric.Text = "Metric"
                                rdeng.Text = "English"
                                tdInner = New TableCell
                                GetUnitDetails(rdeng, rdmatric, dstbl.Tables(k).Rows(0).Item("UNITS").ToString())
                                If arrCaseID(k) <= 1000 Then
                                    rdeng.Style.Add("background-color", "#a6a6a6")
                                    rdeng.Enabled = False
                                    rdmatric.Style.Add("background-color", "#a6a6a6")
                                    rdmatric.Enabled = False
                                End If
                                tdInner.Controls.Add(rdeng)
                                tdInner.Controls.Add(rdmatric)

                                InnerTdSetting(tdInner, "", "Right")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 4
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Effective Date", trInner, "")
                            trInner.ID = "EFFDATE" + i.ToString()
                            'For k = 0 To DataCnt
                            '    lbl = New Label()
                            '    lbl.Text = dstbl.Tables(k).Rows(0).Item("EDATE").ToString()
                            '    tdInner = New TableCell
                            '    tdInner.Controls.Add(lbl)
                            '    InnerTdSetting(tdInner, "", "Right")
                            '    trInner.Controls.Add(tdInner)
                            'Next
                            For k = 0 To DataCnt
                                'lbl = New Label()
                                'lbl.Text = dstbl.Tables(k).Rows(0).Item("EDATE").ToString()
                                ddl = New DropDownList
                                ddl.CssClass = "dropdownunit"
                                ddl.ID = "ddlEffDateid_" + k.ToString() + "_" + j.ToString() + "_" + i.ToString()
                                GetEffdateDetails(ddl, dstbl.Tables(k).Rows(0).Item("EDATE").ToString())
                                If arrCaseID(k) <= 1000 Then
                                    ddl.Style.Add("background-color", "#a6a6a6")
                                    ddl.Enabled = False
                                End If
                                tdInner = New TableCell
                                tdInner.Controls.Add(ddl)
                                InnerTdSetting(tdInner, "", "Right")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 5
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Preferred Currency", trInner, "")
                            trInner.ID = "PREFCUR" + i.ToString()
                            'For k = 0 To DataCnt
                            '    lbl = New Label()
                            '    GetCurrencyDetails(lbl, dstbl.Tables(k).Rows(0).Item("CURRENCY").ToString())
                            '    tdInner = New TableCell
                            '    tdInner.Controls.Add(lbl)
                            '    InnerTdSetting(tdInner, "", "Right")
                            '    trInner.Controls.Add(tdInner)
                            'Next
                            For k = 0 To DataCnt
                                ddl = New DropDownList
                                ddl.CssClass = "dropdownunit"
                                ddl.ID = "ddlCurrid_" + k.ToString() + "_" + j.ToString() + "_" + i.ToString()
                                GetCurrencyDetails(ddl, dstbl.Tables(k).Rows(0).Item("CURRENCY").ToString())
                                If arrCaseID(k) <= 1000 Then
                                    ddl.Style.Add("background-color", "#a6a6a6")
                                    ddl.Enabled = False
                                End If
                                tdInner = New TableCell
                                tdInner.Controls.Add(ddl)
                                InnerTdSetting(tdInner, "", "Right")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 6
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Energy Calculations", trInner, "")
                            trInner.ID = "ERGYCAL" + i.ToString()
                            For k = 0 To DataCnt
                                lbl = New Label()
                                If dstbl.Tables(k).Rows(0).Item("ERGYCALC").ToString() = "Y" Then
                                    lbl.Text = "Adjust Automatically"
                                Else
                                    lbl.Text = "Use Capacity "
                                End If
                                tdInner = New TableCell
                                tdInner.Controls.Add(lbl)
                                InnerTdSetting(tdInner, "", "Right")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 7
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Discrete Calculations", trInner, "")
                            trInner.ID = "DISCAL" + i.ToString()
                            For k = 0 To DataCnt
                                lbl = New Label()
                                If dstbl.Tables(k).Rows(0).Item("ISDSCTNEW").ToString() = "Y" Then
                                    lbl.Text = "Shipped together"
                                Else
                                    lbl.Text = "Shipped separately"
                                End If
                                tdInner = New TableCell
                                tdInner.Controls.Add(lbl)
                                InnerTdSetting(tdInner, "", "Right")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 8
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Design Waste Calculations", trInner, "")
                            trInner.ID = "DESCAL" + i.ToString()
                            For k = 0 To DataCnt
                                lbl = New Label()
                                If dstbl.Tables(k).Rows(0).Item("DFLAG").ToString() = "N" Then
                                    lbl.Text = "Old"
                                Else
                                    lbl.Text = "New"
                                End If
                                tdInner = New TableCell
                                tdInner.Controls.Add(lbl)
                                InnerTdSetting(tdInner, "", "Right")
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

    Protected Sub GetCountryDetailsOLD(ByRef lbl As Label, ByVal CountryId As Integer)
        Dim Ds As New DataSet
        Dim ObjGetdata As New E1GetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty
        Try

            Ds = ObjGetdata.GetCountry(CountryId)
            lbl.Text = Ds.Tables(0).Rows(0).Item("COUNTRYDE1").ToString()

        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetCountryDetails(ByRef ddl As DropDownList, ByVal CountryId As Integer)
        Dim Ds As New DataSet
        Dim dscountries As DataSet
        Dim ObjGetdata As New E1GetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty
        Try

            Ds = ObjGetdata.GetCountry(CountryId)
            dscountries = ObjGetdata.GetCountry("-1")
            With ddl
                .DataSource = dscountries
                .DataTextField = "COUNTRYDE1"
                .DataValueField = "COUNTRYID"
                .DataBind()
            End With
            ddl.SelectedValue = Ds.Tables(0).Rows(0).Item("countryid").ToString()

        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetUnitDetailsOLD(ByRef lbl As Label, ByVal UnitId As Integer)
        Dim Ds As New DataSet
        Dim ObjGetdata As New E1GetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty
        Try

            If UnitId = 0 Then
                lbl.Text = "English"
            Else
                lbl.Text = "Metric"
            End If

        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetUnitDetails(ByRef rd As RadioButton, ByVal rd1 As RadioButton, ByVal UnitId As Integer)
        Dim Ds As New DataSet
        Dim ObjGetdata As New E1GetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty
        Try

            If UnitId = 0 Then
                rd.Checked = True
                rd1.Checked = False
            Else
                rd.Checked = False
                rd1.Checked = True
            End If
            hidval.Value = rd.Checked
        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetCurrencyDetailsOLD(ByRef lbl As Label, ByVal currId As Integer)
        Dim Ds As New DataSet
        Dim ObjGetdata As New E1GetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty
        Try

            Ds = ObjGetdata.GetCurrancy(currId)
            lbl.Text = Ds.Tables(0).Rows(0).Item("CURDE1").ToString()

        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetCurrencyDetails(ByRef ddl As DropDownList, ByVal currId As Integer)
        Dim Ds As New DataSet
        Dim dscurr As New DataSet
        Dim ObjGetdata As New E1GetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty
        Try

            Ds = ObjGetdata.GetCurrancy(currId)
            'lbl.Text = Ds.Tables(0).Rows(0).Item("CURDE1").ToString()
            dscurr = ObjGetdata.GetCurrancy("-1")
            With ddl
                .DataSource = dscurr
                .DataTextField = "CURDE1"
                .DataValueField = "CURID"
                .DataBind()
            End With
            ddl.SelectedValue = currId

        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetEffdateDetails(ByRef ddl As DropDownList, ByVal Effdate As String)
        Dim Ds As New DataSet
        Dim dseffdate As New DataSet
        Dim ObjGetdata As New E1GetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty
        Try
            dseffdate = ObjGetdata.GetEffDate()
            With ddl
                .DataSource = dseffdate
                .DataTextField = "EDATE"
                .DataValueField = "EDATE"
                .DataBind()
            End With
            ddl.SelectedValue = Effdate '.ToString()

            'lbl.Text = Ds.Tables(0).Rows(0).Item("COUNTRYDE1").ToString()

        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnWidthSet_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnWidthSet.Click
        Try
            Dim objUpdateData As New E3UpInsData.UpdateInsert
            objUpdateData.EditCOLWIDTH(AssumptionId, txtDWidth.Text)
            GetPageDetails()
        Catch ex As Exception

        End Try
    End Sub

    <System.Web.Services.WebMethod()> _
    Public Shared Function UpdateCase(ByVal ocntry() As String, ByVal index As String, ByVal CaseID As String, ByVal unit As String) As Array
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

            dts = ObjGetData.GetPref(CaseID)
            If CaseID <= 1000 Then
            Else
                StrSqlUpadate = "UPDATE preferences SET"
                For Mat = 1 To 1
                    StrSqlIUpadate = StrSqlIUpadate + " OCOUNTRY=" + ocntry(Mat).ToString() + ","
                    StrSqlIUpadate = StrSqlIUpadate + " DCOUNTRY=" + ocntry(Mat + 1).ToString() + ","
                    StrSqlIUpadate = StrSqlIUpadate + " UNITS=" + unit.ToString() + ","
                    StrSqlIUpadate = StrSqlIUpadate + " EFFDATE=TO_DATE('" + ocntry(Mat + 3).ToString() + "','MON DD,YYYY'), "
                    StrSqlIUpadate = StrSqlIUpadate + " CURRENCY=" + ocntry(Mat + 4).ToString() + ""

                Next

                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, EconConnection)


                Dim StrSql As String
                Dim Effdate As String = ocntry(4).ToString()
                StrSql = "UPDATE MATERIALINPUT  "
                StrSql = StrSql + "SET "
                StrSql = StrSql + "EFFDATE = TO_DATE('" + Effdate + "','MON DD,YYYY') "
                StrSql = StrSql + "WHERE CASEID =  " + CaseID + ""
                odbUtil.UpIns(StrSql, EconConnection)

                StrSql = "UPDATE PLANTENERGY  "
                StrSql = StrSql + "SET "
                StrSql = StrSql + "EFFDATE = TO_DATE('" + Effdate + "','MON DD,YYYY') "
                StrSql = StrSql + "WHERE CASEID =  " + CaseID + ""
                odbUtil.UpIns(StrSql, EconConnection)


                StrSql = "UPDATE PERSONNELPOS  "
                StrSql = StrSql + "SET "
                StrSql = StrSql + "EFFDATE = TO_DATE('" + Effdate + "','MON DD,YYYY') "
                StrSql = StrSql + "WHERE CASEID =  " + CaseID + ""
                odbUtil.UpIns(StrSql, EconConnection)

                '''''''''''''''''''''''''''''''''''''''
                Dim Currancy As String = ocntry(5).ToString()
                Dim Units As String = unit.ToString()
                Dim dsConv As New DataSet()
                Dim dsCurr As New DataSet()
                Dim dsCurrAVG As New DataSet()
                ' Dim ObjGetData As New E1GetData.Selectdata()

                Dim Title1 As String = String.Empty
                Dim Title2 As String = String.Empty
                Dim Title3 As String = String.Empty
                Dim Title4 As String = String.Empty
                Dim Title6 As String = String.Empty
                Dim Title7 As String = String.Empty
                Dim Title8 As String = String.Empty
                Dim Title9 As String = String.Empty
                'Bug#385
                Dim Title13 As String = String.Empty
                Dim Title14 As String = String.Empty
                Dim Title15 As String = String.Empty
                Dim Title16 As String = String.Empty
                Dim Convvol As New Decimal
                Dim Convwt2 As New Decimal
                Dim Convwt3 As New Decimal
                Dim Convwt4 As New Decimal
                Dim Convarea3 As New Decimal
                'Bug#385

                Dim Convwt As New Decimal
                Dim Convarea As New Decimal
                Dim Convarea2 As New Decimal
                Dim Convthick As New Decimal
                Dim Convthick2 As New Decimal
                Dim Convthick3 As New Decimal
                Dim Curr, CurrAVG As New Decimal
                'Bug#441
                Dim Title19 As String = String.Empty
                Dim Title20 As String = String.Empty

                dsCurr = ObjGetData.GetCurrancyArch(CaseID, Currancy)
                dsConv = ObjGetData.GetConversionFactor()
                dsCurrAVG = ObjGetData.GetCurrancyArchAVG(CaseID, Currancy)


                If CInt(Units) = 0 Then

                    'Titles
                    Title1 = "mil"
                    Title3 = "msi"
                    Title4 = "miles"
                    Title7 = "sq ft"
                    Title8 = "lb"
                    Title9 = "in"

                    'Bug#385
                    Title13 = "oz"
                    Title14 = "ft3"
                    Title15 = "in2"
                    Title16 = "tons"
                    'Bug#385

                    'Bug#441
                    Title19 = "in2"
                    Title20 = "°F"

                    'Conversion
                    Convwt = 1
                    Convarea = 1
                    Convarea2 = 1
                    Convthick = 1
                    Convthick2 = 1
                    Convthick3 = 1

                    'Bug#385
                    Convvol = 1
                    Convwt2 = 1
                    Convwt3 = 1
                    Convwt4 = 1
                    Convarea3 = 1
                    'Bug#385

                Else

                    'Titles
                    Title1 = "micron"
                    Title3 = "m2"
                    Title4 = "kilometers"
                    Title7 = "sq m"
                    Title8 = "kg"
                    Title9 = "mm"

                    'Bug#385
                    Title13 = "gms"
                    Title14 = "cm3"
                    Title15 = "mm2"
                    Title16 = "KN"
                    'Bug#385

                    'Bug#441
                    Title19 = "m2"
                    Title20 = "°C"

                    'Conversion
                    Convwt = CDbl(dsConv.Tables(0).Rows(0).Item("KGPLB").ToString())
                    Convarea = CDbl(dsConv.Tables(0).Rows(0).Item("M2PMSI").ToString())
                    Convarea2 = CDbl(dsConv.Tables(0).Rows(0).Item("M2PSQFT").ToString())
                    Convthick = CDbl(dsConv.Tables(0).Rows(0).Item("MICPMIL").ToString())
                    Convthick2 = CDbl(dsConv.Tables(0).Rows(0).Item("MPFT").ToString())
                    Convthick3 = CDbl(dsConv.Tables(0).Rows(0).Item("KMPMILE").ToString())

                    'Bug#385
                    Convvol = CDbl(dsConv.Tables(0).Rows(0).Item("CCMPCFT").ToString())
                    Convwt2 = CDbl(dsConv.Tables(0).Rows(0).Item("GMPLB").ToString())
                    Convarea3 = CDbl(dsConv.Tables(0).Rows(0).Item("MM2PIN2").ToString())
                    Convwt3 = CDbl(dsConv.Tables(0).Rows(0).Item("TPKN").ToString())
                    Convwt4 = CDbl(dsConv.Tables(0).Rows(0).Item("GMPOZ").ToString())
                    'Bug#385

                End If

                 Dim ds1 As New DataSet
                StrSql = "select * from COUNTRYUNIT"
                ds1 = odbUtil.FillDataSet(StrSql, EconConnection)

                For i = 0 To ds1.Tables(0).Rows.Count - 1
                    If ds1.Tables(0).Rows(i).Item("COUNTRYID").ToString() = Currancy Then

                        If Currancy = 175 Then
                            Curr = 1
                            Title2 = ds1.Tables(0).Rows(i).Item("UNITNAME").ToString()
                            Title6 = ds1.Tables(0).Rows(i).Item("UNITNAME2").ToString()
                            CurrAVG = 1
                        Else
                            Curr = CDbl(dsCurr.Tables(0).Rows(0).Item("CURPUSD").ToString())
                            Title2 = ds1.Tables(0).Rows(i).Item("UNITNAME").ToString()
                            Title6 = ds1.Tables(0).Rows(i).Item("UNITNAME2").ToString()
                            CurrAVG = CDbl(dsCurrAVG.Tables(0).Rows(0).Item("CURPUSD").ToString())
                        End If

                    End If
                Next

                StrSql = "UPDATE PREFERENCES  "
                StrSql = StrSql + "SET "
                StrSql = StrSql + "CURR=" + Curr.ToString() + ", "
                StrSql = StrSql + "CURRAVG=" + CurrAVG.ToString() + ", "
                StrSql = StrSql + "CONVWT=" + Convwt.ToString() + ", "
                'Bug#385
                StrSql = StrSql + "CONVWT2=" + Convwt2.ToString() + ", "
                StrSql = StrSql + "CONVWT3=" + Convwt3.ToString() + ", "
                StrSql = StrSql + "CONVWT4=" + Convwt4.ToString() + ", "
                StrSql = StrSql + "CONVAREA3=" + Convarea3.ToString() + ", "
                StrSql = StrSql + "CONVVOL=" + Convvol.ToString() + ", "
                'Bug#385
                StrSql = StrSql + "CONVAREA=" + Convarea.ToString() + ", "
                StrSql = StrSql + "CONVAREA2=" + Convarea2.ToString() + ", "
                StrSql = StrSql + "CONVTHICK=" + Convthick.ToString() + ", "
                StrSql = StrSql + "CONVTHICK2=" + Convthick2.ToString() + ", "
                StrSql = StrSql + "CONVTHICK3=" + Convthick3.ToString() + ", "
                StrSql = StrSql + "TITLE1='" + Title1 + "', "
                StrSql = StrSql + "TITLE2='" + Title2 + "', "
                StrSql = StrSql + "TITLE3='" + Title3 + "', "
                StrSql = StrSql + "TITLE4='" + Title4 + "', "
                StrSql = StrSql + "TITLE6='" + Title6 + "', "
                StrSql = StrSql + "TITLE7='" + Title7 + "', "
                StrSql = StrSql + "TITLE8='" + Title8 + "', "
                StrSql = StrSql + "TITLE9='" + Title9 + "', "
                'Bug#385
                StrSql = StrSql + "TITLE13='" + Title13 + "', "
                StrSql = StrSql + "TITLE14='" + Title14 + "', "
                StrSql = StrSql + "TITLE15='" + Title15 + "', "
                StrSql = StrSql + "TITLE16='" + Title16 + "', "
                'Bug#385
                StrSql = StrSql + "TITLE19='" + Title19 + "', "
                StrSql = StrSql + "TITLE20='" + Title20 + "' "
                StrSql = StrSql + "WHERE CASEID =  " + CaseID + ""
                odbUtil.UpIns(StrSql, EconConnection)

                '''''''''''''''''''''''''''''''''''''''

            End If

            Dim Econ1Conn As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            Dim obj As New EconCalculation.EconCalculation(Econ1Conn)
            obj.EconCalculate(CaseID)

            Dim ArrVal(30) As String
            Dim ds As New DataSet
            Dim l As Integer = 0
            ds = ObjGetData.GetPref(CaseID)
          
            ArrVal(0) = index
            ArrVal(1) = "N"
            ArrVal(2) = ds.Tables(0).Rows(0).Item("OCOUNTRY")
            ArrVal(3) = ds.Tables(0).Rows(0).Item("DCOUNTRY")
            ArrVal(4) = ds.Tables(0).Rows(0).Item("UNITS")
            ArrVal(5) = ds.Tables(0).Rows(0).Item("CURRENCY")
            ArrVal(6) = ds.Tables(0).Rows(0).Item("EDATE")
            'ArrVal(7)=ds.Tables (0).Rows (0).Item ("")


            Return ArrVal
        Catch ex As Exception

        End Try
    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function UpdateAllCases(ByVal ocntry() As String, ByVal index As String, ByVal flag As String, ByVal CaseID As String, ByVal unit As String) As Array
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

            dts = ObjGetData.GetPref(CaseID)
            If CaseID <= 1000 Then
            Else
                StrSqlUpadate = "UPDATE preferences SET"
                For Mat = 1 To 1
                    StrSqlIUpadate = StrSqlIUpadate + " OCOUNTRY=" + ocntry(Mat).ToString() + ","
                    StrSqlIUpadate = StrSqlIUpadate + " DCOUNTRY=" + ocntry(Mat + 1).ToString() + ","
                    StrSqlIUpadate = StrSqlIUpadate + " UNITS=" + unit.ToString() + ","
                    StrSqlIUpadate = StrSqlIUpadate + " EFFDATE=TO_DATE('" + ocntry(Mat + 3).ToString() + "','MON DD,YYYY'), "
                    StrSqlIUpadate = StrSqlIUpadate + " CURRENCY=" + ocntry(Mat + 4).ToString() + ""

                Next

                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, EconConnection)


                Dim StrSql As String
                Dim Effdate As String = ocntry(4).ToString()
                StrSql = "UPDATE MATERIALINPUT  "
                StrSql = StrSql + "SET "
                StrSql = StrSql + "EFFDATE = TO_DATE('" + Effdate + "','MON DD,YYYY') "
                StrSql = StrSql + "WHERE CASEID =  " + CaseID + ""
                odbUtil.UpIns(StrSql, EconConnection)

                StrSql = "UPDATE PLANTENERGY  "
                StrSql = StrSql + "SET "
                StrSql = StrSql + "EFFDATE = TO_DATE('" + Effdate + "','MON DD,YYYY') "
                StrSql = StrSql + "WHERE CASEID =  " + CaseID + ""
                odbUtil.UpIns(StrSql, EconConnection)


                StrSql = "UPDATE PERSONNELPOS  "
                StrSql = StrSql + "SET "
                StrSql = StrSql + "EFFDATE = TO_DATE('" + Effdate + "','MON DD,YYYY') "
                StrSql = StrSql + "WHERE CASEID =  " + CaseID + ""
                odbUtil.UpIns(StrSql, EconConnection)

                '''''''''''''''''''''''''''''''''''''''
                Dim Currancy As String = ocntry(5).ToString()
                Dim Units As String = unit.ToString()
                Dim dsConv As New DataSet()
                Dim dsCurr As New DataSet()
                Dim dsCurrAVG As New DataSet()
                ' Dim ObjGetData As New E1GetData.Selectdata()

                Dim Title1 As String = String.Empty
                Dim Title2 As String = String.Empty
                Dim Title3 As String = String.Empty
                Dim Title4 As String = String.Empty
                Dim Title6 As String = String.Empty
                Dim Title7 As String = String.Empty
                Dim Title8 As String = String.Empty
                Dim Title9 As String = String.Empty
                'Bug#385
                Dim Title13 As String = String.Empty
                Dim Title14 As String = String.Empty
                Dim Title15 As String = String.Empty
                Dim Title16 As String = String.Empty
                Dim Convvol As New Decimal
                Dim Convwt2 As New Decimal
                Dim Convwt3 As New Decimal
                Dim Convwt4 As New Decimal
                Dim Convarea3 As New Decimal
                'Bug#385

                Dim Convwt As New Decimal
                Dim Convarea As New Decimal
                Dim Convarea2 As New Decimal
                Dim Convthick As New Decimal
                Dim Convthick2 As New Decimal
                Dim Convthick3 As New Decimal
                Dim Curr, CurrAVG As New Decimal
                'Bug#441
                Dim Title19 As String = String.Empty
                Dim Title20 As String = String.Empty

                dsCurr = ObjGetData.GetCurrancyArch(CaseID, Currancy)
                dsConv = ObjGetData.GetConversionFactor()
                dsCurrAVG = ObjGetData.GetCurrancyArchAVG(CaseID, Currancy)


                If CInt(Units) = 0 Then

                    'Titles
                    Title1 = "mil"
                    Title3 = "msi"
                    Title4 = "miles"
                    Title7 = "sq ft"
                    Title8 = "lb"
                    Title9 = "in"

                    'Bug#385
                    Title13 = "oz"
                    Title14 = "ft3"
                    Title15 = "in2"
                    Title16 = "tons"
                    'Bug#385

                    'Bug#441
                    Title19 = "in2"
                    Title20 = "°F"

                    'Conversion
                    Convwt = 1
                    Convarea = 1
                    Convarea2 = 1
                    Convthick = 1
                    Convthick2 = 1
                    Convthick3 = 1

                    'Bug#385
                    Convvol = 1
                    Convwt2 = 1
                    Convwt3 = 1
                    Convwt4 = 1
                    Convarea3 = 1
                    'Bug#385

                Else

                    'Titles
                    Title1 = "micron"
                    Title3 = "m2"
                    Title4 = "kilometers"
                    Title7 = "sq m"
                    Title8 = "kg"
                    Title9 = "mm"

                    'Bug#385
                    Title13 = "gms"
                    Title14 = "cm3"
                    Title15 = "mm2"
                    Title16 = "KN"
                    'Bug#385

                    'Bug#441
                    Title19 = "m2"
                    Title20 = "°C"

                    'Conversion
                    Convwt = CDbl(dsConv.Tables(0).Rows(0).Item("KGPLB").ToString())
                    Convarea = CDbl(dsConv.Tables(0).Rows(0).Item("M2PMSI").ToString())
                    Convarea2 = CDbl(dsConv.Tables(0).Rows(0).Item("M2PSQFT").ToString())
                    Convthick = CDbl(dsConv.Tables(0).Rows(0).Item("MICPMIL").ToString())
                    Convthick2 = CDbl(dsConv.Tables(0).Rows(0).Item("MPFT").ToString())
                    Convthick3 = CDbl(dsConv.Tables(0).Rows(0).Item("KMPMILE").ToString())

                    'Bug#385
                    Convvol = CDbl(dsConv.Tables(0).Rows(0).Item("CCMPCFT").ToString())
                    Convwt2 = CDbl(dsConv.Tables(0).Rows(0).Item("GMPLB").ToString())
                    Convarea3 = CDbl(dsConv.Tables(0).Rows(0).Item("MM2PIN2").ToString())
                    Convwt3 = CDbl(dsConv.Tables(0).Rows(0).Item("TPKN").ToString())
                    Convwt4 = CDbl(dsConv.Tables(0).Rows(0).Item("GMPOZ").ToString())
                    'Bug#385

                End If

                Dim ds1 As New DataSet
                StrSql = "select * from COUNTRYUNIT"
                ds1 = odbUtil.FillDataSet(StrSql, EconConnection)

                For i = 0 To ds1.Tables(0).Rows.Count - 1
                    If ds1.Tables(0).Rows(i).Item("COUNTRYID").ToString() = Currancy Then

                        If Currancy = 175 Then
                            Curr = 1
                            Title2 = ds1.Tables(0).Rows(i).Item("UNITNAME").ToString()
                            Title6 = ds1.Tables(0).Rows(i).Item("UNITNAME2").ToString()
                            CurrAVG = 1
                        Else
                            Curr = CDbl(dsCurr.Tables(0).Rows(0).Item("CURPUSD").ToString())
                            Title2 = ds1.Tables(0).Rows(i).Item("UNITNAME").ToString()
                            Title6 = ds1.Tables(0).Rows(i).Item("UNITNAME2").ToString()
                            CurrAVG = CDbl(dsCurrAVG.Tables(0).Rows(0).Item("CURPUSD").ToString())
                        End If

                    End If
                Next

                StrSql = "UPDATE PREFERENCES  "
                StrSql = StrSql + "SET "
                StrSql = StrSql + "CURR=" + Curr.ToString() + ", "
                StrSql = StrSql + "CURRAVG=" + CurrAVG.ToString() + ", "
                StrSql = StrSql + "CONVWT=" + Convwt.ToString() + ", "
                'Bug#385
                StrSql = StrSql + "CONVWT2=" + Convwt2.ToString() + ", "
                StrSql = StrSql + "CONVWT3=" + Convwt3.ToString() + ", "
                StrSql = StrSql + "CONVWT4=" + Convwt4.ToString() + ", "
                StrSql = StrSql + "CONVAREA3=" + Convarea3.ToString() + ", "
                StrSql = StrSql + "CONVVOL=" + Convvol.ToString() + ", "
                'Bug#385
                StrSql = StrSql + "CONVAREA=" + Convarea.ToString() + ", "
                StrSql = StrSql + "CONVAREA2=" + Convarea2.ToString() + ", "
                StrSql = StrSql + "CONVTHICK=" + Convthick.ToString() + ", "
                StrSql = StrSql + "CONVTHICK2=" + Convthick2.ToString() + ", "
                StrSql = StrSql + "CONVTHICK3=" + Convthick3.ToString() + ", "
                StrSql = StrSql + "TITLE1='" + Title1 + "', "
                StrSql = StrSql + "TITLE2='" + Title2 + "', "
                StrSql = StrSql + "TITLE3='" + Title3 + "', "
                StrSql = StrSql + "TITLE4='" + Title4 + "', "
                StrSql = StrSql + "TITLE6='" + Title6 + "', "
                StrSql = StrSql + "TITLE7='" + Title7 + "', "
                StrSql = StrSql + "TITLE8='" + Title8 + "', "
                StrSql = StrSql + "TITLE9='" + Title9 + "', "
                'Bug#385
                StrSql = StrSql + "TITLE13='" + Title13 + "', "
                StrSql = StrSql + "TITLE14='" + Title14 + "', "
                StrSql = StrSql + "TITLE15='" + Title15 + "', "
                StrSql = StrSql + "TITLE16='" + Title16 + "', "
                'Bug#385
                StrSql = StrSql + "TITLE19='" + Title19 + "', "
                StrSql = StrSql + "TITLE20='" + Title20 + "' "
                StrSql = StrSql + "WHERE CASEID =  " + CaseID + ""
                odbUtil.UpIns(StrSql, EconConnection)

                '''''''''''''''''''''''''''''''''''''''

            End If

            Dim Econ1Conn As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            Dim obj As New EconCalculation.EconCalculation(Econ1Conn)
            obj.EconCalculate(CaseID)

            Dim ArrVal(30) As String
            Dim ds As New DataSet
            Dim l As Integer = 0
            ds = ObjGetData.GetPref(CaseID)

            ArrVal(0) = index
            ArrVal(1) = flag
            ArrVal(2) = ds.Tables(0).Rows(0).Item("OCOUNTRY")
            ArrVal(3) = ds.Tables(0).Rows(0).Item("DCOUNTRY")
            ArrVal(4) = ds.Tables(0).Rows(0).Item("UNITS")
            ArrVal(5) = ds.Tables(0).Rows(0).Item("CURRENCY")
            ArrVal(6) = ds.Tables(0).Rows(0).Item("EDATE")

            Return ArrVal

        Catch ex As Exception

        End Try
    End Function

 
End Class
