﻿Imports System.Data
Imports System.Data.OleDb
Imports System
Imports E4GetData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Econ4_Assumptions_AdditionalEnergy
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
            Dim UnitError As String = String.Empty
            Dim CurrError As String = String.Empty
            Dim Title As String = String.Empty
            DWidth = txtDWidth.Text + "px"


            tdHeader = New TableCell
            HeaderTdSetting(tdHeader, "200px", "<img alt='' src='../../Images/spacer.gif' Style='width:200px;height:0px;'  />", 1)
            trHeader.Controls.Add(tdHeader)
            trHeader.Height = 20
            trHeader.CssClass = "PageSSHeading"

            For i = 0 To DataCnt
                Dim ds As New DataSet
                ds = objGetData.GetAdditionalEnergyInfo(arrCaseID(i))
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
                For j = 1 To 5
                    trInner = New TableRow()

                    Select Case j
                        Case 1
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Plant Space Type", trInner, "AlterNateColor4")

                            For k = 0 To DataCnt
                                Dim str As String = String.Empty
                                str = str + "<table cellpadding='3' border='0' width='100%'>"
                                str = str + "<tr style='text-align:right;'>"
                                str = str + "<td  width='50%' colspan='2'>Energy Consumption</td>"
                                str = str + "<td  width='50%' colspan='2'>Energy Consumption</td>"
                                str = str + "</tr>"
                                str = str + "<tr style='text-align:center;'>"
                                str = str + "<td  width='50%' colspan='2'>" + "(kw/" + dstbl.Tables(k).Rows(0).Item("TITLE7").ToString() + "/year)" + "</td>"
                                str = str + "<td  width='50%' colspan='2'>" + "(ft3/" + dstbl.Tables(k).Rows(0).Item("TITLE7").ToString() + "/year)" + "</td>"
                                str = str + "</tr>"
                                str = str + "<tr style='text-align:right;'>"
                                str = str + "<td  width='25%'>Suggested</td>"
                                str = str + "<td  width='25%'>Preferred</td>"
                                str = str + "<td  width='25%'>Suggested</td>"
                                str = str + "<td  width='25%'>Preferred</td>"
                                str = str + "</tr>"
                                str = str + "</table>"
                                tdInner = New TableCell
                                tdInner.Text = str
                                InnerTdSetting(tdInner, "", "Right")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 2
                            Title = ""
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Production" + Title, trInner, "")
                            trInner.ID = "PROD" + i.ToString()
                            For k = 0 To DataCnt
                                Dim str As String = String.Empty
                                str = str + "<table cellpadding='3' border='0' width='100%'>"
                                str = str + "<tr style='text-align:right'>"
                                str = str + "<td width='25%'></td>"
                                str = str + "<td width='25%'></td>"
                                str = str + "<td width='25%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("ENERGYCONSUMBS1").ToString(), 3) + "</td>"
                                str = str + "<td width='25%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("ENERGYCONSUMBP1").ToString(), 3) + "</td>"
                                str = str + "</tr>"
                                str = str + "</table>"
                                tdInner = New TableCell
                                tdInner.Text = str
                                InnerTdSetting(tdInner, "", "Center")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 3
                            Title = ""
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Warehouse" + Title, trInner, "")
                            trInner.ID = "WAR" + i.ToString()
                            For k = 0 To DataCnt
                                Dim str As String = String.Empty
                                str = str + "<table cellpadding='3' border='0' width='100%'>"
                                str = str + "<tr style='text-align:right'>"
                                str = str + "<td width='25%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("ENERGYCONSUMAS1").ToString().ToString(), 3) + "</td>"
                                str = str + "<td width='25%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("ENERGYCONSUMAP1").ToString().ToString(), 3) + "</td>"

                                str = str + "<td width='25%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("ENERGYCONSUMBS2").ToString(), 3) + "</td>"
                                str = str + "<td width='25%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("ENERGYCONSUMBP2").ToString(), 3) + "</td>"
                                str = str + "</tr>"
                                str = str + "</table>"
                                tdInner = New TableCell
                                tdInner.Text = str
                                InnerTdSetting(tdInner, "", "Center")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 4
                            Title = ""
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Office" + Title, trInner, "")
                            trInner.ID = "OFF" + i.ToString()
                            For k = 0 To DataCnt
                                Dim str As String = String.Empty
                                str = str + "<table cellpadding='3' border='0' width='100%'>"
                                str = str + "<tr style='text-align:right'>"
                                str = str + "<td width='25%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("ENERGYCONSUMAS2").ToString().ToString(), 3) + "</td>"
                                str = str + "<td width='25%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("ENERGYCONSUMAP2").ToString().ToString(), 3) + "</td>"

                                str = str + "<td width='25%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("ENERGYCONSUMBS3").ToString(), 3) + "</td>"
                                str = str + "<td width='25%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("ENERGYCONSUMBP3").ToString(), 3) + "</td>"
                                str = str + "</tr>"
                                str = str + "</table>"
                                tdInner = New TableCell
                                tdInner.Text = str
                                InnerTdSetting(tdInner, "", "Center")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 5
                            Title = ""
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Support" + Title, trInner, "")
                            trInner.ID = "SUPP" + i.ToString()
                            For k = 0 To DataCnt
                                Dim str As String = String.Empty
                                str = str + "<table cellpadding='3' border='0' width='100%'>"
                                str = str + "<tr style='text-align:right'>"
                                str = str + "<td width='25%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("ENERGYCONSUMAS3").ToString().ToString(), 3) + "</td>"
                                str = str + "<td width='25%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("ENERGYCONSUMAP3").ToString().ToString(), 3) + "</td>"

                                str = str + "<td width='25%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("ENERGYCONSUMBS4").ToString(), 3) + "</td>"
                                str = str + "<td width='25%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("ENERGYCONSUMBP4").ToString(), 3) + "</td>"
                                str = str + "</tr>"
                                str = str + "</table>"
                                tdInner = New TableCell
                                tdInner.Text = str
                                InnerTdSetting(tdInner, "", "Center")
                                trInner.Controls.Add(tdInner)
                            Next





                    End Select
                    If j = 1 Then
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
