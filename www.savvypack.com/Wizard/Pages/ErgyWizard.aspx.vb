#Region "Imports Classes"
Imports System
Imports System.Data
Imports Allied.GetData
Imports Allied.UpdateInsert
Imports AjaxControlToolkit
Imports System.Math
#End Region
Partial Class Pages_ErgyWizard
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
                GetGeoRegione()
            End If
            GenrateWizard()

        Catch ex As Exception

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
                .SelectedValue = 1
            End With
            GetEffdate()



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
                .SelectedValue = dsmax.Tables(0).Rows(0).Item("EFFDATE").ToString()
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
        Dim TPROD As New Decimal
        Try
            RegioneId = Convert.ToInt32(ddlGeographic.SelectedValue.ToString())
            Effdate = ddlEffdate.SelectedValue.ToString()
            ds = objGetData.GetWizrdDetails(RegioneId, Effdate)
            dsPref = objGetData.GetPreferences(Session("SessionId").ToString(), Session("Modules"))

            Session("SuggErg") = ds
            If Not IsPostBack Then
                dsPval = objGetData.GetWizrdPErgyDetails()
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
            HeaderTdSetting(HdTd, "16%", "<div onmouseover=""Tip('Energy Source')"" onmouseout=""UnTip()"">Energy Source</div>", 1)
            HdTr.Controls.Add(HdTd)
            HdTd = New TableCell
            HeaderTdSetting(HdTd, "16%", "<div onmouseover=""Tip('Production Mix')"" onmouseout=""UnTip()"">Production Mix</div>", 2)
            HdTr.Controls.Add(HdTd)
            HdTd = New TableCell
            HeaderTdSetting(HdTd, "16%", "<div onmouseover=""Tip('Energy Use Ratio')"" onmouseout=""UnTip()"">Energy Use Ratio</div>", 2)
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
            HeaderTdSetting(HdTd, "16%", "(%)", 2)
            HdTr.Controls.Add(HdTd)
            HdTd = New TableCell
            HeaderTdSetting(HdTd, "16%", "(%)", 2)
            HdTr.Controls.Add(HdTd)
            HdTd = New TableCell
            HeaderTdSetting(HdTd, "16%", "(unitless)", 2)
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
                TxtBox.Text = FormatNumber(dsPval.Tables(0).Rows(0).Item("PROD" + (i + 1).ToString() + ""), 3)
                TextBoxSeting(TxtBox, "", "", "", "PROD" + (i + 1).ToString())
                InTd.Controls.Add(TxtBox)
                InnerTdSetting(InTd, "", "Right")
                InTr.Controls.Add(InTd)

                If Convert.ToDecimal(dsPval.Tables(0).Rows(0).Item("PROD" + (i + 1).ToString() + "")) <> Convert.ToDecimal(0) Then
                    TPROD = TPROD + Convert.ToDecimal(dsPval.Tables(0).Rows(0).Item("PROD" + (i + 1).ToString() + ""))
                Else
                    TPROD = TPROD + Convert.ToDecimal(ds.Tables(0).Rows(i).Item("PRODUCTION").ToString())
                End If



                InTd = New TableCell
                InTd.Text = FormatNumber(ds.Tables(0).Rows(i).Item("ERGYUSERATIO").ToString(), 3) + "&nbsp;"
                InnerTdSetting(InTd, "", "Right")
                InTr.Controls.Add(InTd)


                InTd = New TableCell
                TxtBox = New TextBox
                TxtBox.Text = FormatNumber(dsPval.Tables(0).Rows(0).Item("ERGY" + (i + 1).ToString() + ""), 3)
                TextBoxSeting(TxtBox, "", "", "", "ERGY" + (i + 1).ToString() + "")
                InTd.Controls.Add(TxtBox)
                InnerTdSetting(InTd, "", "Right")
                InTr.Controls.Add(InTd)


                InTd = New TableCell
                InTd.Text = FormatNumber(ds.Tables(0).Rows(i).Item("LOSATMFGFACI").ToString(), 3) + "&nbsp;"
                InnerTdSetting(InTd, "", "Right")
                InTr.Controls.Add(InTd)


                InTd = New TableCell
                TxtBox = New TextBox
                TxtBox.Text = FormatNumber(dsPval.Tables(0).Rows(0).Item("LMF" + (i + 1).ToString() + ""), 3)
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
                TxtBox.Text = FormatNumber(dsPval.Tables(0).Rows(0).Item("TML" + (i + 1).ToString() + ""), 3)
                TextBoxSeting(TxtBox, "", "", "", "TML" + (i + 1).ToString() + "")
                InTd.Controls.Add(TxtBox)
                InnerTdSetting(InTd, "", "Right")
                InTr.Controls.Add(InTd)


                InTd = New TableCell
                InTd.Text = FormatNumber(dsPval.Tables(0).Rows(0).Item("TERGY" + (i + 1).ToString() + ""), 3) + "&nbsp;"
                TC = TC + Convert.ToDecimal(dsPval.Tables(0).Rows(0).Item("TERGY" + (i + 1).ToString() + ""))
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
            ResultTdSetting(InTdT, 1, "", "right")
            InTdT.Text = FormatNumber(TC.ToString(), 3) + "&nbsp;"
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
        Try
            Update()
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
                ds.Tables(0).Rows(0).Item("PROD" + i.ToString() + "") = Request.Form("ctl00$Sustain1ContentPlaceHolder$PROD" + i.ToString() + "")
                ds.Tables(0).Rows(0).Item("ERGY" + i.ToString() + "") = Request.Form("ctl00$Sustain1ContentPlaceHolder$ERGY" + i.ToString() + "")
                ds.Tables(0).Rows(0).Item("LMF" + i.ToString() + "") = Request.Form("ctl00$Sustain1ContentPlaceHolder$LMF" + i.ToString() + "")
                ds.Tables(0).Rows(0).Item("TML" + i.ToString() + "") = Request.Form("ctl00$Sustain1ContentPlaceHolder$TML" + i.ToString() + "")
            Next
            Calculate(ds)
            Session("dsPvalErg") = ds
            tblWizard.Rows.Clear()
            GenrateWizard()
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


                If Convert.ToDecimal(ds.Tables(0).Rows(0).Item("PROD" + i.ToString() + "")) <> 0.0 Then
                    PROD = Convert.ToDecimal(ds.Tables(0).Rows(0).Item("PROD" + i.ToString() + ""))
                Else
                    PROD = Convert.ToDecimal(DsSugg.Tables(0).Rows(i - 1).Item("PRODUCTION").ToString())
                End If

                If Convert.ToDecimal(ds.Tables(0).Rows(0).Item("ERGY" + i.ToString() + "")) <> 0.0 Then
                    ERGY = Convert.ToDecimal(ds.Tables(0).Rows(0).Item("ERGY" + i.ToString() + ""))
                Else
                    ERGY = Convert.ToDecimal(DsSugg.Tables(0).Rows(i - 1).Item("ERGYUSERATIO").ToString())
                End If

                If Convert.ToDecimal(ds.Tables(0).Rows(0).Item("TML" + i.ToString() + "")) <> 0.0 Then
                    TML = Convert.ToDecimal(ds.Tables(0).Rows(0).Item("TML" + i.ToString() + ""))
                Else
                    TML = Convert.ToDecimal(DsSugg.Tables(0).Rows(i - 1).Item("TRMSIONLOSS").ToString())
                End If

                If Convert.ToDecimal(ds.Tables(0).Rows(0).Item("LMF" + i.ToString() + "")) <> 0.0 Then
                    LMF = Convert.ToDecimal(ds.Tables(0).Rows(0).Item("LMF" + i.ToString() + ""))
                Else
                    LMF = Convert.ToDecimal(DsSugg.Tables(0).Rows(i - 1).Item("LOSATMFGFACI").ToString())
                End If


                TERGY = (PROD / 100) * ((ERGY) / (1 - ((TML / 100) + (LMF / 100))))

                ds.Tables(0).Rows(0).Item("TERGY" + i.ToString() + "") = TERGY.ToString()

            Next


        Catch ex As Exception
            ErrorLable.Text = "Co2 Wizard:Calculate:" + ex.Message.ToString() + ""
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

            For i = 1 To 10
                PROD = Convert.ToDecimal(DsSugg.Tables(0).Rows(i - 1).Item("PRODUCTION").ToString())
                ERGY = Convert.ToDecimal(DsSugg.Tables(0).Rows(i - 1).Item("ERGYUSERATIO").ToString())
                TML = Convert.ToDecimal(DsSugg.Tables(0).Rows(i - 1).Item("TRMSIONLOSS").ToString())
                LMF = Convert.ToDecimal(DsSugg.Tables(0).Rows(i - 1).Item("LOSATMFGFACI").ToString())

                TERGY = (PROD / 100) * ((ERGY) / (1 - ((TML / 100) + (LMF / 100))))
                ds.Tables(0).Rows(0).Item("TERGY" + i.ToString() + "") = TERGY.ToString()

            Next

        Catch ex As Exception
            ErrorLable.Text = "Co2 Wizard:SCalculate:" + ex.Message.ToString() + ""
        End Try
    End Sub

#Region "MastePage Content Variables"
    Protected Sub GerErrorLable()
        ErrorLable = Page.Master.FindControl("lblError")
    End Sub

    Protected Sub GerUpdateButton()
        UpdateBtn = Page.Master.FindControl("imgUpdate")
        AddHandler UpdateBtn.Click, AddressOf Update_Click
    End Sub
#End Region

    Protected Sub ddlGeographic_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlGeographic.SelectedIndexChanged
        Try
            GetEffdate()
            tblWizard.Rows.Clear()
            GenrateWizard()
            Update()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlEffdate_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEffdate.SelectedIndexChanged
        Try
            tblWizard.Rows.Clear()
            GenrateWizard()
            Update()
        Catch ex As Exception

        End Try
    End Sub

End Class
