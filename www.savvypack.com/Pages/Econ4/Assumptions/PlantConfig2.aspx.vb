﻿Imports System.Data
Imports System.Data.OleDb
Imports System
Imports E4GetData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Econ4_Assumptions_PlantConfig2
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
        Updatebtn.Visible = False

    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Try
                GetErrorLable()
                GetUpdatebtn()
                GetSessionDetails()
                GetPageDetails()
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
        Dim objGetData As New E4GetData.Selectdata
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
        Dim objGetData As New E2GetData.Selectdata
        Dim CaseIds As String = String.Empty
        'Dim DataCnt As New Integer
        Dim i As New Integer
        Dim j As New Integer
        Dim k As New Integer
        Dim DWidth As String = String.Empty
        Dim arrCaseID() As String


        Try
            arrCaseID = GetCaseIds()
            DataCnt = arrCaseID.Length - 1

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

                Headertext = "Case#:" + arrCaseID(i).ToString() + "<br/>" + dsCaseDetails.Tables(0).Rows(0).Item("CaseDes").ToString() + UnitError + CurrError + "<input type='hidden' value='" + arrCaseID(i).ToString() + "' name='Case" + i.ToString() + "'/>"

                CaseDesp.Add(arrCaseID(i).ToString())
                HeaderTdSetting(tdHeader, DWidth, Headertext, 1)
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
                                tdInner = New TableCell
                                lbl = New Label()
                                lbl.Text = FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("AR2").ToString()), 3).ToString()
                                tdInner.Controls.Add(lbl)
                                InnerTdSetting(tdInner, "", "Right")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 4
                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("TITLE7").ToString() + ")"
                            LeftTdSetting(tdInner, "Office " + Title + "", trInner, "")
                            trInner.ID = "SOF_" + i.ToString()
                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                lbl = New Label()
                                lbl.Text = FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("AR3").ToString()), 3).ToString()
                                tdInner.Controls.Add(lbl)
                                InnerTdSetting(tdInner, "", "Right")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 5
                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("TITLE7").ToString() + ")"
                            LeftTdSetting(tdInner, "Support " + Title + "", trInner, "")
                            trInner.ID = "SOS_" + i.ToString()
                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                lbl = New Label()
                                lbl.Text = FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("AR4").ToString()), 3).ToString()
                                tdInner.Controls.Add(lbl)
                                InnerTdSetting(tdInner, "", "Right")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 6
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("TITLE7").ToString() + ")"
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "<b>Total " + Title + "</b>", trInner, "")
                            trInner.ID = "ST0T_" + i.ToString()
                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                lbl = New Label()
                                lbl.Text = "<b>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("ARTOT").ToString()), 3).ToString() + "</b>"
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
                                str = str + "<td  width='37%'>Lease Cost Sugg.</td>"
                                str = str + "<td  width='37%'>Lease Cost Pref.</td>"
                                str = str + "<td  width='30%'>Total Lease</td>"
                                str = str + "</tr>"
                                str = str + "<tr style='text-align:right;'>"
                                str = str + "<td  width='37%'>(" + dstbl.Tables(k).Rows(0).Item("TITLE2") + "/" + dstbl.Tables(k).Rows(0).Item("TITLE7") + ")</td>"
                                str = str + "<td  width='37%'>(" + dstbl.Tables(k).Rows(0).Item("TITLE2") + "/" + dstbl.Tables(k).Rows(0).Item("TITLE7") + ")</td>"
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
                                str = str + "<td width='35%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("SUG1").ToString()), 3).ToString() + "</td>"
                                str = str + "<td width='35%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("PREF1").ToString()), 3).ToString() + "</td>"
                                str = str + "<td width='33%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("PTOT1").ToString()), 0).ToString() + "</td>"
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
                                str = str + "<td width='35%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("SUG2").ToString()), 3).ToString() + "</td>"
                                str = str + "<td width='35%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("PREF2").ToString()), 3).ToString() + "</td>"
                                str = str + "<td width='33%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("PTOT2").ToString()), 0).ToString() + "</td>"
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
                                str = str + "<td width='35%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("SUG3").ToString()), 3).ToString() + "</td>"
                                str = str + "<td width='35%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("PREF3").ToString()), 3).ToString() + "</td>"
                                str = str + "<td width='33%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("PTOT3").ToString()), 0).ToString() + "</td>"
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
                                str = str + "<td width='33%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("PTOT4").ToString()), 0).ToString() + "</td>"
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
                                str = str + "<td width='35%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("SUG4").ToString()), 3).ToString() + "</td>"
                                str = str + "<td width='35%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("PREF4").ToString()), 3).ToString() + "</td>"
                                str = str + "<td width='33%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("PTOT5").ToString()), 0).ToString() + "</td>"
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
                                str = str + "<td width='35%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("SUG5").ToString()), 3).ToString() + "</td>"
                                str = str + "<td width='35%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("PREF5").ToString()), 3).ToString() + "</td>"
                                str = str + "<td width='33%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("PTOT6").ToString()), 0).ToString() + "</td>"
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
                                str = str + "<td width='35%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("SUG6").ToString()), 3).ToString() + "</td>"
                                str = str + "<td width='35%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("PREF6").ToString()), 3).ToString() + "</td>"
                                str = str + "<td width='33%'>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("PTOT7").ToString()), 0).ToString() + "</td>"
                                str = str + "</tr>"
                                str = str + "</table>"
                                tdInner = New TableCell
                                tdInner.Text = str
                                InnerTdSetting(tdInner, "", "Center")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 15
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "<b>Total</b>", trInner, "")
                            trInner.ID = "LT0T_" + i.ToString()
                            For k = 0 To DataCnt
                                Dim str As String = String.Empty
                                str = str + "<table cellpadding='3' border='0' width='100%'>"
                                str = str + "<tr style='text-align:right'>"
                                str = str + "<td width='35%'></td>"
                                str = str + "<td width='35%'></td>"
                                str = str + "<td width='33%'><b>" + FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("LEASETOTAL").ToString()), 0).ToString() + "</b></td>"
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

  

    Protected Sub GetDeptDetails(ByRef lbl As Label, ByVal ProcId As Integer, ByVal CaseId As String)
        Dim Ds As New DataSet
        Dim ObjGetdata As New E2GetData.Selectdata()
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
