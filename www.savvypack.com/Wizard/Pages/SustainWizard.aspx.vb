#Region "Imports Classes"
Imports System
Imports System.Data
Imports Allied.GetData
Imports Allied.UpdateInsert
Imports AjaxControlToolkit
Imports System.Math
#End Region
Partial Class Pages_SustainWizard
    Inherits System.Web.UI.Page

#Region "Get Set Variables"
    Dim _iCaseID As Integer
    Dim _lErrorLble As Label
    Dim _bUpdateBtn As ImageButton


    Public Property CaseID() As Integer
        Get
            Return _iCaseID
        End Get
        Set(ByVal Value As Integer)
            _iCaseID = Value
        End Set
    End Property

    Public Property ErrorLable() As Label
        Get
            Return _lErrorLble
        End Get
        Set(ByVal Value As Label)
            _lErrorLble = Value
        End Set
    End Property

    Public Property UpdateBtn() As ImageButton
        Get
            Return _bUpdateBtn
        End Get
        Set(ByVal Value As ImageButton)
            _bUpdateBtn = Value
        End Set
    End Property
#End Region


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            GerUpdateButton()
            GerErrorLable()
            If Not IsPostBack Then
                GetCaseId()
                GetGeoRegione()
                GetEffdate()
            End If
            GenrateWizard()

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub GetCaseId()
        Dim ds As New DataSet
        Dim objGetData As New GetData()
        Try
            ds = objGetData.GetCaseId(Session("SessionId"), Session("Modules"))
            Session("CaseId") = ds.Tables(0).Rows(0).Item("CASEID").ToString()
        Catch ex As Exception
			ErrorLable.Text = "Co2 Wizard:GetCaseId:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetGeoRegione()
        Dim objGetData As New GetData()
        Dim ds As New DataSet
        Try
            ds = objGetData.GetRegione()
            With ddlGeographic
                .DataSource = ds
                .DataTextField = "REGIONEDES"
                .DataValueField = "REGIONEID"
                .DataBind()
                '.SelectedValue = 1
            End With
            'GetEffdate()



        Catch ex As Exception
            ErrorLable.Text = "Co2 Wizard:GetGeoRegione:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetEffdate()
        Dim objGetData As New GetData()
        Dim ds As New DataSet
        Dim dsmax As New DataSet
        Dim RegioneId As New Integer
        Try
            RegioneId = Convert.ToInt32(ddlGeographic.SelectedValue.ToString())
            ds = objGetData.GetEffdate(RegioneId)
            dsmax = objGetData.GetMaxEffdate(RegioneId)
            With ddlEffdate
                .DataSource = ds
                .DataTextField = "EFFDATE"
                .DataValueField = "EFFDATE"
                .DataBind()
                '.SelectedValue = dsmax.Tables(0).Rows(0).Item("EFFDATE").ToString()
            End With



        Catch ex As Exception
            ErrorLable.Text = "Co2 Wizard:GetEffdate:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GenrateWizard()
        Dim objGetData As New GetData()
        Dim ds As New DataSet
        Dim dsPval As New DataSet
        Dim dsPref As New DataSet
        Dim RegioneId As New Integer
        Dim Effdate As String = String.Empty
        Dim Count As New Integer
        Dim i As New Integer
        Dim TC As New Decimal
        Dim TCO2 As New Decimal
        Dim TWater As New Decimal
        Dim TPROD As New Decimal
        Dim dsCase As New DataSet
        Try
		
            If Not IsPostBack Then
                dsCase = objGetData.GetWizrdPrefferedDetails(Session("CaseId"), Session("Modules"), ddlEffdate.SelectedValue.ToString(), ddlGeographic.SelectedValue)
                ddlGeographic.SelectedValue = dsCase.Tables(0).Rows(0).Item("REGIONID").ToString()
                ddlEffdate.SelectedValue = dsCase.Tables(0).Rows(0).Item("EFFDATE").ToString()
            End If
            
            RegioneId = Convert.ToInt32(ddlGeographic.SelectedValue.ToString())
            Effdate = ddlEffdate.SelectedValue.ToString()

            ds = objGetData.GetWizrdDetails(RegioneId, Effdate)
            dsPref = objGetData.GetPreferences(Session("SessionId").ToString(),  Session("Modules"))
            Session("SuggErg") = ds

            If Not IsPostBack Then
                dsPval = objGetData.GetWizrdPrefferedDetails(Session("CaseId"), Session("Modules"), Effdate, RegioneId)
                Session("dsPvalErg") = dsPval
                SCalculate()
            Else
                dsPval = Session("dsPvalErg")
            End If



            Count = ds.Tables(0).Rows.Count - 1
            Dim HdTr As New TableRow
            Dim HdTd As TableCell
            HdTr.BorderWidth = 1
            HdTd = New TableCell
            HeaderTdSetting(HdTd, "16%", "<div onmouseover=""Tip('Source')"" onmouseout=""UnTip()"">Source</div>", 1)
            HdTr.Controls.Add(HdTd)
            HdTd = New TableCell
            HeaderTdSetting(HdTd, "16%", "<div onmouseover=""Tip('Production Mix')"" onmouseout=""UnTip()"">Production Mix</div>", 2)
            HdTr.Controls.Add(HdTd)
            HdTd = New TableCell
            HeaderTdSetting(HdTd, "16%", "<div onmouseover=""Tip('Energy Use Ratio')"" onmouseout=""UnTip()"">Energy Use Ratio</div>", 2)
            HdTr.Controls.Add(HdTd)
            HdTd = New TableCell
            HeaderTdSetting(HdTd, "16%", "<div onmouseover=""Tip('GHG from Electricity Generation')"" onmouseout=""UnTip()"">GHG from Electricity Generation</div>", 2)
            HdTr.Controls.Add(HdTd)
            HdTd = New TableCell
            HeaderTdSetting(HdTd, "16%", "<div onmouseover=""Tip('Water Use')"" onmouseout=""UnTip()"">Water Use</div>", 2)
            HdTr.Controls.Add(HdTd)
            HdTd = New TableCell
            HeaderTdSetting(HdTd, "16%", "<div onmouseover=""Tip('Losses at Manufacturing Facility')"" onmouseout=""UnTip()"">Losses at Manufacturing Facility</div>", 2)
            HdTr.Controls.Add(HdTd)
            HdTd = New TableCell
            HeaderTdSetting(HdTd, "16%", "<div onmouseover=""Tip('Transmission Losses')"" onmouseout=""UnTip()"">Transmission Losses</div>", 2)
            HdTr.Controls.Add(HdTd)
            HdTd = New TableCell
            HeaderTdSetting(HdTd, "16%", "<div onmouseover=""Tip('Total Energy Use Ratio')"" onmouseout=""UnTip()"">Total Energy Use Ratio</div>", 1)
            HdTr.Controls.Add(HdTd)
            HdTd = New TableCell
            HeaderTdSetting(HdTd, "16%", "<div onmouseover=""Tip('Total GHG')"" onmouseout=""UnTip()"">Total GHG</div>", 1)
            HdTr.Controls.Add(HdTd)
            HdTd = New TableCell
            HeaderTdSetting(HdTd, "16%", "<div onmouseover=""Tip('Total Water')"" onmouseout=""UnTip()"">Total Water</div>", 1)
            HdTr.Controls.Add(HdTd)
            HdTr.CssClass = "PageSSHeading"
            HdTr.Height = 20
            tblWizard.Controls.Add(HdTr)

            HdTr = New TableRow
            HdTr.BorderWidth = 1
            HdTd = New TableCell
            HeaderTdSetting(HdTd, "16%", "Units", 1)
            HdTr.Controls.Add(HdTd)
            HdTd = New TableCell
            HeaderTdSetting(HdTd, "16%", "(%)", 2)
            HdTr.Controls.Add(HdTd)
            HdTd = New TableCell
            HeaderTdSetting(HdTd, "16%", "(unitless)", 2)
            HdTr.Controls.Add(HdTd)
            HdTd = New TableCell
            HeaderTdSetting(HdTd, "16%", "(" + dsPref.Tables(0).Rows(0).Item("TITLE8").ToString() + " CO2/kwh)", 2)
            HdTr.Controls.Add(HdTd)
            HdTd = New TableCell
            HeaderTdSetting(HdTd, "16%", "(" + dsPref.Tables(0).Rows(0).Item("TITLE10").ToString() + "/kwh)", 2)
            HdTr.Controls.Add(HdTd)
            HdTd = New TableCell
            HeaderTdSetting(HdTd, "16%", "(%)", 2)
            HdTr.Controls.Add(HdTd)
            HdTd = New TableCell
            HeaderTdSetting(HdTd, "16%", "(%)", 2)
            HdTr.Controls.Add(HdTd)
            HdTd = New TableCell
            HeaderTdSetting(HdTd, "16%", "(unitless)", 1)
            HdTr.Controls.Add(HdTd)
            HdTd = New TableCell
            HeaderTdSetting(HdTd, "16%", "(" + dsPref.Tables(0).Rows(0).Item("TITLE8").ToString() + " CO2/kwh)", 1)
            HdTr.Controls.Add(HdTd)
            HdTd = New TableCell
            HeaderTdSetting(HdTd, "16%", "(" + dsPref.Tables(0).Rows(0).Item("TITLE10").ToString() + "/kwh)", 1)
            HdTr.Controls.Add(HdTd)
            HdTr.Height = 20
            HdTr.CssClass = "PageSSHeading"
            tblWizard.Controls.Add(HdTr)


            HdTr = New TableRow
            HdTr.BorderWidth = 1
            HdTd = New TableCell

            HeaderTdSetting(HdTd, "", "", 1)
            HdTr.Controls.Add(HdTd)

            HdTd = New TableCell
            HeaderTdSetting(HdTd, "", "Suggested", 1)
            HdTr.Controls.Add(HdTd)
            HdTd = New TableCell
            HeaderTdSetting(HdTd, "", "Preferred", 1)
            HdTr.Controls.Add(HdTd)

            HdTd = New TableCell
            HeaderTdSetting(HdTd, "", "Suggested", 1)
            HdTr.Controls.Add(HdTd)
            HdTd = New TableCell
            HeaderTdSetting(HdTd, "", "Preferred", 1)
            HdTr.Controls.Add(HdTd)

            HdTd = New TableCell
            HeaderTdSetting(HdTd, "", "Suggested", 1)
            HdTr.Controls.Add(HdTd)
            HdTd = New TableCell
            HeaderTdSetting(HdTd, "", "Preferred", 1)
            HdTr.Controls.Add(HdTd)

            HdTd = New TableCell
            HeaderTdSetting(HdTd, "", "Suggested", 1)
            HdTr.Controls.Add(HdTd)
            HdTd = New TableCell
            HeaderTdSetting(HdTd, "", "Preferred", 1)
            HdTr.Controls.Add(HdTd)


            HdTd = New TableCell
            HeaderTdSetting(HdTd, "", "Suggested", 1)
            HdTr.Controls.Add(HdTd)
            HdTd = New TableCell
            HeaderTdSetting(HdTd, "", "Preferred", 1)
            HdTr.Controls.Add(HdTd)


            HdTd = New TableCell
            HeaderTdSetting(HdTd, "", "Suggested", 1)
            HdTr.Controls.Add(HdTd)
            HdTd = New TableCell
            HeaderTdSetting(HdTd, "", "Preferred", 1)
            HdTr.Controls.Add(HdTd)

            HdTd = New TableCell
            HeaderTdSetting(HdTd, "", "", 1)
            HdTr.Controls.Add(HdTd)

            HdTd = New TableCell
            HeaderTdSetting(HdTd, "", "", 1)
            HdTr.Controls.Add(HdTd)

            HdTd = New TableCell
            HeaderTdSetting(HdTd, "", "", 1)
            HdTr.Controls.Add(HdTd)

            HdTr.Height = 20
            HdTr.CssClass = "PageSSHeading"
            tblWizard.Controls.Add(HdTr)



            For i = 0 To Count
                Dim InTr As New TableRow
                Dim InTd As TableCell
                Dim TxtBox As TextBox

                InTd = New TableCell
                InTd.Text = "<div style='margin-left:5px;'>" + ds.Tables(0).Rows(i).Item("GHGDE1").ToString() + "<div>"
                InnerTdSetting(InTd, "", "left")
                InTr.Controls.Add(InTd)


                InTd = New TableCell
                InTd.Text = FormatNumber(ds.Tables(0).Rows(i).Item("PRODUCTION").ToString(), 3) + "&nbsp;"
                InnerTdSetting(InTd, "", "Right")
                InTr.Controls.Add(InTd)



                InTd = New TableCell
                TxtBox = New TextBox
                TxtBox.Text = FormatNumber(dsPval.Tables(0).Rows(0).Item("PRODUCTION" + (i + 1).ToString() + ""), 3)
                TextBoxSeting(TxtBox, "", "", "", "PROD" + (i + 1).ToString())
                InTd.Controls.Add(TxtBox)
                InnerTdSetting(InTd, "", "Right")
                InTr.Controls.Add(InTd)

                If Convert.ToDecimal(dsPval.Tables(0).Rows(0).Item("PRODUCTION" + (i + 1).ToString() + "")) <> Convert.ToDecimal(0) Then
                    TPROD = TPROD + Convert.ToDecimal(dsPval.Tables(0).Rows(0).Item("PRODUCTION" + (i + 1).ToString() + ""))
                Else
                    TPROD = TPROD + Convert.ToDecimal(ds.Tables(0).Rows(i).Item("PRODUCTION").ToString())
                End If



                InTd = New TableCell
                InTd.Text = FormatNumber(ds.Tables(0).Rows(i).Item("ERGYUSERATIO").ToString(), 3) + "&nbsp;"
                InnerTdSetting(InTd, "", "Right")
                InTr.Controls.Add(InTd)


                InTd = New TableCell
                TxtBox = New TextBox
                TxtBox.Text = FormatNumber(dsPval.Tables(0).Rows(0).Item("ERGYUSERATIO" + (i + 1).ToString() + ""), 3)
                TextBoxSeting(TxtBox, "", "", "", "ERGY" + (i + 1).ToString() + "")
                InTd.Controls.Add(TxtBox)
                InnerTdSetting(InTd, "", "Right")
                InTr.Controls.Add(InTd)

                InTd = New TableCell
                InTd.Text = FormatNumber(ds.Tables(0).Rows(i).Item("CO2FRMELEGEN").ToString() * dsPref.Tables(0).Rows(0).Item("CONVWT").ToString(), 3) + "&nbsp;"
                InnerTdSetting(InTd, "", "Right")
                InTr.Controls.Add(InTd)


                InTd = New TableCell
                TxtBox = New TextBox
                TxtBox.Text = FormatNumber(dsPval.Tables(0).Rows(0).Item("CO2USERATIO" + (i + 1).ToString() + "") * dsPref.Tables(0).Rows(0).Item("CONVWT").ToString(), 3)
                TextBoxSeting(TxtBox, "", "", "", "GHG" + (i + 1).ToString() + "")
                InTd.Controls.Add(TxtBox)
                InnerTdSetting(InTd, "", "Right")
                InTr.Controls.Add(InTd)

                InTd = New TableCell
                InTd.Text = FormatNumber(ds.Tables(0).Rows(i).Item("WATERUSERATIO").ToString() * dsPref.Tables(0).Rows(0).Item("CONVGALLON").ToString(), 3) + "&nbsp;"
                InnerTdSetting(InTd, "", "Right")
                InTr.Controls.Add(InTd)


                InTd = New TableCell
                TxtBox = New TextBox
                TxtBox.Text = FormatNumber(dsPval.Tables(0).Rows(0).Item("WATERUSERATIO" + (i + 1).ToString() + "") * dsPref.Tables(0).Rows(0).Item("CONVGALLON").ToString(), 3)
                TextBoxSeting(TxtBox, "", "", "", "WATER" + (i + 1).ToString() + "")
                InTd.Controls.Add(TxtBox)
                InnerTdSetting(InTd, "", "Right")
                InTr.Controls.Add(InTd)


                InTd = New TableCell
                InTd.Text = FormatNumber(ds.Tables(0).Rows(i).Item("LOSATMFGFACI").ToString(), 3) + "&nbsp;"
                InnerTdSetting(InTd, "", "Right")
                InTr.Controls.Add(InTd)


                InTd = New TableCell
                TxtBox = New TextBox
                TxtBox.Text = FormatNumber(dsPval.Tables(0).Rows(0).Item("LOSSMFGFACI" + (i + 1).ToString() + ""), 3)
                TextBoxSeting(TxtBox, "", "", "", "LMF" + (i + 1).ToString() + "")
                InTd.Controls.Add(TxtBox)
                InnerTdSetting(InTd, "", "Right")
                InTr.Controls.Add(InTd)

                InTr.Height = 20
                tblWizard.Controls.Add(InTr)



                InTd = New TableCell
                InTd.Text = FormatNumber(ds.Tables(0).Rows(i).Item("TRMSIONLOSS").ToString(), 3) + "&nbsp;"
                InnerTdSetting(InTd, "", "Right")
                InTr.Controls.Add(InTd)


                InTd = New TableCell
                TxtBox = New TextBox
                TxtBox.Text = FormatNumber(dsPval.Tables(0).Rows(0).Item("TRANSLOSS" + (i + 1).ToString() + ""), 3)
                TextBoxSeting(TxtBox, "", "", "", "TML" + (i + 1).ToString() + "")
                InTd.Controls.Add(TxtBox)
                InnerTdSetting(InTd, "", "Right")
                InTr.Controls.Add(InTd)


                InTd = New TableCell
                InTd.Text = FormatNumber(dsPval.Tables(0).Rows(0).Item("TOTERGY" + (i + 1).ToString() + ""), 3) + "&nbsp;"
                TC = TC + Convert.ToDecimal(dsPval.Tables(0).Rows(0).Item("TOTERGY" + (i + 1).ToString() + ""))
                InnerTdSetting(InTd, "", "Right")
                InTr.Controls.Add(InTd)

                InTd = New TableCell
                InTd.Text = FormatNumber(dsPval.Tables(0).Rows(0).Item("TOTCO2" + (i + 1).ToString() + "") * dsPref.Tables(0).Rows(0).Item("CONVWT").ToString(), 3) + "&nbsp;"
                TCO2 = TCO2 + Convert.ToDecimal(dsPval.Tables(0).Rows(0).Item("TOTCO2" + (i + 1).ToString() + "") * dsPref.Tables(0).Rows(0).Item("CONVWT").ToString())
                InnerTdSetting(InTd, "", "Right")
                InTr.Controls.Add(InTd)

                InTd = New TableCell
                InTd.Text = FormatNumber(dsPval.Tables(0).Rows(0).Item("TOTWATER" + (i + 1).ToString() + "") * dsPref.Tables(0).Rows(0).Item("CONVGALLON").ToString(), 3) + "&nbsp;"
                TWater = TWater + Convert.ToDecimal(dsPval.Tables(0).Rows(0).Item("TOTWATER" + (i + 1).ToString() + "") * dsPref.Tables(0).Rows(0).Item("CONVGALLON").ToString())
                InnerTdSetting(InTd, "", "Right")
                InTr.Controls.Add(InTd)

                InTr.Height = 20

                If (i Mod 2) = 0 Then
                    InTr.CssClass = "AlterNateColor1"
                Else
                    InTr.CssClass = "AlterNateColor2"
                End If

                'InTr.CssClass = ds.Tables(0).Rows(i).Item("CSSCLASS").ToString()


                tblWizard.Controls.Add(InTr)

            Next
            Dim InTrT As New TableRow
            Dim InTdT As TableCell
            InTrT = New TableRow
            InTdT = New TableCell
            ResultTdSetting(InTdT, 1, "", "Left")
            InTdT.Text = "<div style='margin-left:5px;'>" + "Total" + "</div>"
            InTrT.Controls.Add(InTdT)

            InTdT = New TableCell
            ResultTdSetting(InTdT, 2, "", "Center")
            InTdT.Text = FormatNumber(TPROD.ToString(), 3) + "&nbsp;"
            InTrT.Controls.Add(InTdT)

            InTdT = New TableCell
            ResultTdSetting(InTdT, 2, "", "Center")
            InTdT.Text = ""
            InTrT.Controls.Add(InTdT)

            InTdT = New TableCell
            ResultTdSetting(InTdT, 2, "", "Center")
            InTdT.Text = ""
            InTrT.Controls.Add(InTdT)

            InTdT = New TableCell
            ResultTdSetting(InTdT, 2, "", "Center")
            InTdT.Text = ""
            InTrT.Controls.Add(InTdT)

            InTdT = New TableCell
            ResultTdSetting(InTdT, 2, "", "Center")
            InTdT.Text = ""
            InTrT.Controls.Add(InTdT)

            InTdT = New TableCell
            ResultTdSetting(InTdT, 2, "", "Center")
            InTdT.Text = ""
            InTrT.Controls.Add(InTdT)

            InTdT = New TableCell
            ResultTdSetting(InTdT, 1, "", "right")
            InTdT.Text = FormatNumber(TC.ToString(), 3) + "&nbsp;"
            InTrT.Controls.Add(InTdT)

            InTdT = New TableCell
            ResultTdSetting(InTdT, 1, "", "right")
            InTdT.Text = FormatNumber(TCO2.ToString(), 3) + "&nbsp;"
            InTrT.Controls.Add(InTdT)

            InTdT = New TableCell
            ResultTdSetting(InTdT, 1, "", "right")
            InTdT.Text = FormatNumber(TWater.ToString(), 3) + "&nbsp;"
            InTrT.Controls.Add(InTdT)

            InTrT.Height = 20
            InTrT.CssClass = "AlterNateColor1"
            tblWizard.Controls.Add(InTrT)

        Catch ex As Exception

            ErrorLable.Text = "Co2 Wizard:GenrateWizard:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub HeaderTdSetting(ByVal Td As TableCell, ByVal Width As String, ByVal HeaderText As String, ByVal ColSpan As String)
        Try
            Td.Text = HeaderText
            Td.ColumnSpan = ColSpan
            If Width <> "" Then
                Td.Style.Add("width", Width)
            End If
            Td.CssClass = "TdHeadind"



        Catch ex As Exception

        End Try
    End Sub

    Protected Sub InnerTdSetting(ByVal Td As TableCell, ByVal Width As String, ByVal Align As String)
        Try

            If Width <> "" Then
                Td.Style.Add("width", Width)
            End If
            Td.Style.Add("text-align", Align)
            Td.Style.Add("margin-right", "5px")
            'Td.BorderWidth = 1
            'Td.BorderStyle = BorderStyle.Solid
            'Td.BorderColor = Drawing.Color.White


        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ResultTdSetting(ByVal Td As TableCell, ByVal ColSpan As String, ByVal Width As String, ByVal Align As String)
        Try
            If Width <> "" Then
                Td.Style.Add("width", Width)
            End If
            Td.ColumnSpan = ColSpan
            Td.Style.Add("text-align", Align)
            Td.CssClass = "ResultTd"
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub TextBoxSeting(ByVal txt As TextBox, ByVal Type As String, ByVal Pos As String, ByVal ToolTip As String, ByVal Id As String)
        Try
            txt.CssClass = "SmallTextBox"
            txt.ID = Id
            txt.ToolTip = ToolTip
            'txt.ValidationGroup
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Update_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim Prod(9) As String
        Dim Ergy(9) As String
        Dim Ghg(9) As String
        Dim Water(9) As String
        Dim Loss(9) As String
        Dim Trans(9) As String
        Dim TotErgy(9) As String
        Dim TotCo2(9) As String
        Dim TotWater(9) As String
        Dim ds As New DataSet
        Dim objUpIns As New Allied.UpdateInsert.UpInsData
        Try
            Update()
            ds = Session("dsPvalErg")
            For i = 1 To 10
                Prod(i - 1) = ds.Tables(0).Rows(0).Item("PRODUCTION" + i.ToString()).ToString()
                Ergy(i - 1) = ds.Tables(0).Rows(0).Item("ERGYUSERATIO" + i.ToString()).ToString()
                Ghg(i - 1) = ds.Tables(0).Rows(0).Item("CO2USERATIO" + i.ToString()).ToString()
                Water(i - 1) = ds.Tables(0).Rows(0).Item("WATERUSERATIO" + i.ToString()).ToString()
                Loss(i - 1) = ds.Tables(0).Rows(0).Item("LOSSMFGFACI" + i.ToString()).ToString()
                Trans(i - 1) = ds.Tables(0).Rows(0).Item("TRANSLOSS" + i.ToString()).ToString()
                TotErgy(i - 1) = ds.Tables(0).Rows(0).Item("TOTERGY" + i.ToString()).ToString()
                TotCo2(i - 1) = ds.Tables(0).Rows(0).Item("TOTCO2" + i.ToString()).ToString()
                TotWater(i - 1) = ds.Tables(0).Rows(0).Item("TOTWATER" + i.ToString()).ToString()
            Next
            objUpIns.UpdateWizardDetails(Session("CaseId"), Prod, Ergy, Ghg, Water, Loss, Trans, TotErgy, TotCo2, TotWater, ddlGeographic.SelectedValue, ddlEffdate.SelectedItem.ToString(), Session("Modules"))
            SustainCalculation()
            tblWizard.Rows.Clear()
            GenrateWizard()
        Catch ex As Exception
            ErrorLable.Text = "Co2 Wizard:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub Update()
        Try
            Dim ds As New DataSet()

            Dim dsPref As New DataSet
            Dim objGetData As New GetData()
            dsPref = objGetData.GetPreferences(Session("SessionId").ToString(), Session("Modules"))
            ds = Session("dsPvalErg")
            Dim i As Integer
            For i = 1 To 10
                ds.Tables(0).Rows(0).Item("PRODUCTION" + i.ToString() + "") = Request.Form("ctl00$Sustain1ContentPlaceHolder$PROD" + i.ToString() + "")
                ds.Tables(0).Rows(0).Item("ERGYUSERATIO" + i.ToString() + "") = Request.Form("ctl00$Sustain1ContentPlaceHolder$ERGY" + i.ToString() + "")
                ds.Tables(0).Rows(0).Item("CO2USERATIO" + i.ToString() + "") = (Request.Form("ctl00$Sustain1ContentPlaceHolder$GHG" + i.ToString() + "") / dsPref.Tables(0).Rows(0).Item("CONVWT").ToString())
                ds.Tables(0).Rows(0).Item("WATERUSERATIO" + i.ToString() + "") = (Request.Form("ctl00$Sustain1ContentPlaceHolder$WATER" + i.ToString() + "") / dsPref.Tables(0).Rows(0).Item("CONVGALLON").ToString())
                ds.Tables(0).Rows(0).Item("LOSSMFGFACI" + i.ToString() + "") = Request.Form("ctl00$Sustain1ContentPlaceHolder$LMF" + i.ToString() + "")
                ds.Tables(0).Rows(0).Item("TRANSLOSS" + i.ToString() + "") = Request.Form("ctl00$Sustain1ContentPlaceHolder$TML" + i.ToString() + "")
            Next
            Calculate(ds)
            Session("dsPvalErg") = ds
            
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub SustainCalculation()
        Try
            Dim SustainConn As String = System.Configuration.ConfigurationManager.ConnectionStrings("SustainConn").ConnectionString.ToString()
            Dim EconConn As String = System.Configuration.ConfigurationManager.ConnectionStrings("EconConn").ConnectionString.ToString()
            Dim Sustain2Conn As String = System.Configuration.ConfigurationManager.ConnectionStrings("Sustain2Conn").ConnectionString.ToString()
            Dim Econ2Conn As String = System.Configuration.ConfigurationManager.ConnectionStrings("Econ2Conn").ConnectionString.ToString()
            If Session("Modules") = "S1" Then
                Dim obj As New SustainCalculation.SustainCalculations(SustainConn, EconConn)
                obj.SustainCalculate(Session("CaseId"))
            Else
                Dim obj2 As New Sustain2Calculation.Sustain2Calculations(Sustain2Conn, Econ2Conn, SustainConn, EconConn)
                obj2.Sustain2Calculate(Session("CaseId"))
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Calculate(ByRef ds As DataSet)

        Try


            Dim i As Integer
            Dim DsSugg As New DataSet
            DsSugg = Session("SuggErg")


            For i = 1 To 10

                Dim PROD As Decimal
                Dim ERGY As Decimal
                Dim TML As Decimal
                Dim LMF As Decimal
                Dim TERGY As Decimal
                Dim GHG As Decimal
                Dim TGHG As Decimal
                Dim WATER As Decimal
                Dim TWATER As Decimal

                If Convert.ToDecimal(ds.Tables(0).Rows(0).Item("PRODUCTION" + i.ToString() + "")) <> 0.0 Then
                    PROD = Convert.ToDecimal(ds.Tables(0).Rows(0).Item("PRODUCTION" + i.ToString() + ""))
                Else
                    PROD = Convert.ToDecimal(DsSugg.Tables(0).Rows(i - 1).Item("PRODUCTION").ToString())
                End If

                If Convert.ToDecimal(ds.Tables(0).Rows(0).Item("ERGYUSERATIO" + i.ToString() + "")) <> 0.0 Then
                    ERGY = Convert.ToDecimal(ds.Tables(0).Rows(0).Item("ERGYUSERATIO" + i.ToString() + ""))
                Else
                    ERGY = Convert.ToDecimal(DsSugg.Tables(0).Rows(i - 1).Item("ERGYUSERATIO").ToString())
                End If

                If Convert.ToInt32(ds.Tables(0).Rows(0).Item("CO2USERATIO" + i.ToString() + "")) <> 0 Then
                    GHG = Convert.ToDecimal(ds.Tables(0).Rows(0).Item("CO2USERATIO" + i.ToString() + ""))
                Else
                    GHG = Convert.ToDecimal(DsSugg.Tables(0).Rows(i - 1).Item("CO2FRMELEGEN").ToString())
                End If

                If Convert.ToInt32(ds.Tables(0).Rows(0).Item("WATERUSERATIO" + i.ToString() + "")) <> 0 Then
                    WATER = Convert.ToDecimal(ds.Tables(0).Rows(0).Item("WATERUSERATIO" + i.ToString() + ""))
                Else
                    WATER = Convert.ToDecimal(DsSugg.Tables(0).Rows(i - 1).Item("WATERUSERATIO").ToString())
                End If

                If Convert.ToDecimal(ds.Tables(0).Rows(0).Item("TRANSLOSS" + i.ToString() + "")) <> 0.0 Then
                    TML = Convert.ToDecimal(ds.Tables(0).Rows(0).Item("TRANSLOSS" + i.ToString() + ""))
                Else
                    TML = Convert.ToDecimal(DsSugg.Tables(0).Rows(i - 1).Item("TRMSIONLOSS").ToString())
                End If

                If Convert.ToDecimal(ds.Tables(0).Rows(0).Item("LOSSMFGFACI" + i.ToString() + "")) <> 0.0 Then
                    LMF = Convert.ToDecimal(ds.Tables(0).Rows(0).Item("LOSSMFGFACI" + i.ToString() + ""))
                Else
                    LMF = Convert.ToDecimal(DsSugg.Tables(0).Rows(i - 1).Item("LOSATMFGFACI").ToString())
                End If


                TERGY = (PROD / 100) * ((ERGY) / (1 - ((TML / 100) + (LMF / 100))))

                ds.Tables(0).Rows(0).Item("TOTERGY" + i.ToString() + "") = TERGY.ToString()

                TGHG = (PROD / 100) * ((GHG) / (1 - ((TML / 100) + (LMF / 100))))

                ds.Tables(0).Rows(0).Item("TOTCO2" + i.ToString() + "") = TGHG.ToString()

                TWATER = PROD * (WATER / (100 - LMF) / (100 - TML)) * 100

                ds.Tables(0).Rows(0).Item("TOTWATER" + i.ToString() + "") = TWATER.ToString()


            Next


        Catch ex As Exception
            ErrorLable.Text = "Sustain Wizard:Calculate:" + ex.Message.ToString() + ""
        End Try

    End Sub

    Protected Sub SCalculate()
        Try
            Dim DsSugg As New DataSet
            Dim ds As New DataSet()
            DsSugg = Session("SuggErg")
            ds = Session("dsPvalErg")
            Dim i As Integer

            Dim PROD As Decimal
            Dim ERGY As Decimal
            Dim TML As Decimal
            Dim LMF As Decimal
            Dim TERGY As Decimal
            Dim GHG As Decimal
            Dim TGHG As Decimal
            Dim WATER As Decimal
            Dim TWATER As Decimal

            For i = 1 To 10
                If ds.Tables(0).Rows(0).Item("PRODUCTION" + i.ToString()).ToString() <> "0" Then
                    PROD = Convert.ToDecimal(ds.Tables(0).Rows(0).Item("PRODUCTION" + i.ToString()).ToString())
                Else
                    PROD = Convert.ToDecimal(DsSugg.Tables(0).Rows(i - 1).Item("PRODUCTION").ToString())
                End If

                If ds.Tables(0).Rows(0).Item("ERGYUSERATIO" + i.ToString()).ToString() <> "0" Then
                    ERGY = Convert.ToDecimal(ds.Tables(0).Rows(0).Item("ERGYUSERATIO" + i.ToString()).ToString())
                Else
                    ERGY = Convert.ToDecimal(DsSugg.Tables(0).Rows(i - 1).Item("ERGYUSERATIO").ToString())
                End If

                If ds.Tables(0).Rows(0).Item("CO2USERATIO" + i.ToString()).ToString() <> "0" Then
                    GHG = Convert.ToDecimal(ds.Tables(0).Rows(0).Item("CO2USERATIO" + i.ToString()).ToString())
                Else
                    GHG = Convert.ToDecimal(DsSugg.Tables(0).Rows(i - 1).Item("CO2FRMELEGEN").ToString())
                End If

                If ds.Tables(0).Rows(0).Item("WATERUSERATIO" + i.ToString()).ToString() <> "0" Then
                    WATER = Convert.ToDecimal(ds.Tables(0).Rows(0).Item("WATERUSERATIO" + i.ToString()).ToString())
                Else
                    WATER = Convert.ToDecimal(DsSugg.Tables(0).Rows(i - 1).Item("WATERUSERATIO").ToString())
                End If

                If ds.Tables(0).Rows(0).Item("TRANSLOSS" + i.ToString()).ToString() <> "0" Then
                    TML = Convert.ToDecimal(ds.Tables(0).Rows(0).Item("TRANSLOSS" + i.ToString()).ToString())
                Else
                    TML = Convert.ToDecimal(DsSugg.Tables(0).Rows(i - 1).Item("TRMSIONLOSS").ToString())
                End If

                If ds.Tables(0).Rows(0).Item("LOSSMFGFACI" + i.ToString()).ToString() <> "0" Then
                    LMF = Convert.ToDecimal(ds.Tables(0).Rows(0).Item("LOSSMFGFACI" + i.ToString()).ToString())
                Else
                    LMF = Convert.ToDecimal(DsSugg.Tables(0).Rows(i - 1).Item("LOSATMFGFACI").ToString())
                End If

                TERGY = (PROD / 100) * ((ERGY) / (1 - ((TML / 100) + (LMF / 100))))
                ds.Tables(0).Rows(0).Item("TOTERGY" + i.ToString() + "") = TERGY.ToString()

                TGHG = (PROD / 100) * ((GHG) / (1 - ((TML / 100) + (LMF / 100))))
                ds.Tables(0).Rows(0).Item("TOTCO2" + i.ToString() + "") = TGHG.ToString()

                TWATER = PROD * (WATER / (100 - LMF) / (100 - TML)) * 100
                ds.Tables(0).Rows(0).Item("TOTWATER" + i.ToString() + "") = TWATER.ToString()

            Next

        Catch ex As Exception
            ErrorLable.Text = "Sustain Wizard:SCalculate:" + ex.Message.ToString() + ""
        End Try
    End Sub

#Region "MastePage Content Variables"
    Protected Sub GerErrorLable()
        ErrorLable = Page.Master.FindControl("lblError")
    End Sub

    Protected Sub GerUpdateButton()
        UpdateBtn = Page.Master.FindControl("imgUpdate")
        UpdateBtn.Attributes.Add("onclick", "return checkNumericAll();")
        AddHandler UpdateBtn.Click, AddressOf Update_Click
    End Sub
#End Region

    Protected Sub ddlGeographic_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlGeographic.SelectedIndexChanged
        Try
            ' GetEffdate()
            tblWizard.Rows.Clear()
            GenrateWizard()
            Update()
            tblWizard.Rows.Clear()
            GenrateWizard()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlEffdate_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEffdate.SelectedIndexChanged
        Try
            tblWizard.Rows.Clear()
            GenrateWizard()
            Update()
            tblWizard.Rows.Clear()
            GenrateWizard()
        Catch ex As Exception

        End Try
    End Sub

End Class
