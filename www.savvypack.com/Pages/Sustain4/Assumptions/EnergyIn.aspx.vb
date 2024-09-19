Imports System.Data
Imports System.Data.OleDb
Imports System
Imports S4GetData
Imports System.Collections
Imports System.IO.StringWriter
Partial Class Pages_Sustain4_Assumptions_EnergyIn
    Inherits System.Web.UI.Page
#Region "Get Set Variables"
    Dim _lErrorLble As Label
    Dim _strUserName As String
    Dim _strPassword As String
    Dim _iAssumptionId As Integer


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


    Public DataCnt As Integer
    Public CaseDesp As New ArrayList

#End Region

#Region "MastePage Content Variables"
    Protected Sub GetErrorLable()
        ErrorLable = Page.Master.FindControl("lblError")
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Try
                GetErrorLable()
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
            AssumptionId = Session("AssumptionId")

            lblAID.Text = AssumptionId
            lblAdes.Text = Session("Description")

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetSessionDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Function GetCaseIds() As String()
        Dim CaseIds(0)
        Dim objGetData As New S4GetData.Selectdata

        Try
            CaseIds = objGetData.Cases1(AssumptionId)

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetCaseIds:" + ex.Message.ToString()
        End Try
        Return CaseIds
    End Function

    Protected Sub GetPageDetails()
        Dim ds As New DataSet
        Dim dstbl As New DataSet
        Dim dsCaseDetails As New DataSet
        Dim objGetData As New S2GetData.Selectdata
        Dim CaseIds As String = String.Empty
        'Dim DataCnt As New Integer
        Dim i As New Integer
        Dim j As New Integer
        Dim k As New Integer
        Dim DWidth As String = String.Empty
        Dim arrCaseID() As String

        Try
            arrCaseID = GetCaseIds()
            'ds = objGetData.EnergyIn(CaseIds, UserName)
            DataCnt = arrCaseID.length - 1

            Dim trHeader As New TableRow
            Dim tdHeader As TableCell
            Dim tdInner As TableCell
            Dim trInner As New TableRow
            Dim ddl As DropDownList
            Dim txt As TextBox
            Dim str As StringBuilder
            Dim Cunits As New Integer
            Dim lbl As New Label
            Dim Units As New Integer
            Dim Title As String = String.Empty
            DWidth = txtDWidth.Text + "px"


            tdHeader = New TableCell
            HeaderTdSetting(tdHeader, DWidth, "<img alt='' src='../../Images/spacer.gif' style='width:200px;height:0px;'  />", 1)
            trHeader.Controls.Add(tdHeader)
            trHeader.Height = 20
            trHeader.CssClass = "PageSSHeading"


            For i = 0 To DataCnt
                ds = objGetData.GetEnergyDetails(arrCaseID(i))
                ds.Tables(0).TableName = arrCaseID(i).ToString()
                dstbl.Tables.Add(ds.Tables(arrCaseID(i).ToString()).Copy())
            Next



            For i = 0 To DataCnt
                dsCaseDetails = objGetData.GetCaseDetails(arrCaseID(i).ToString())
                Cunits = Convert.ToInt32(dstbl.Tables(0).Rows(0).Item("Units").ToString())
                Units = Convert.ToInt32(dstbl.Tables(i).Rows(0).Item("Units").ToString())
                tdHeader = New TableCell
                Dim Headertext As String = String.Empty
                If Cunits <> Units Then
                    Headertext = "Case#:" + arrCaseID(i).ToString() + "<br/>" + dsCaseDetails.Tables(0).Rows(0).Item("CaseDes").ToString() + "<br/> <span  style='color:red'>Unit Mismatch</span>" + "<input type='hidden' value='" + arrCaseID(i).ToString() + "' name='Case" + i.ToString() + "'/>"
                Else
                    Headertext = "Case#:" + arrCaseID(i).ToString() + "<br/>" + dsCaseDetails.Tables(0).Rows(0).Item("CaseDes").ToString() + "<input type='hidden' value='" + arrCaseID(i).ToString() + "' name='Case" + i.ToString() + "'/>"
                End If
                CaseDesp.Add(arrCaseID(i).ToString())
                HeaderTdSetting(tdHeader, DWidth, Headertext, 1)
                trHeader.Controls.Add(tdHeader)
            Next
            tblComparision.Controls.Add(trHeader)


            For i = 1 To 1
                For j = 1 To 36

                    trInner = New TableRow()
                    Select Case j

                        Case 1
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "<b>Energy Ratios - Cradle/Gate</b>", trInner, "AlterNateColor4")
                            For k = 0 To DataCnt
                                'Energy Type
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Center")
                                str = New StringBuilder()
                                str.Append("<table style='width:100%' border='0' >")
                                str.Append("<tr align='right' style='font-weight:bold'> <td style='width:50%'>Suggested</td> <td style='width:50%'>Preferred</td> </tr>")
                                str.Append("<tr align='right' style='font-weight:bold'> <td style='width:50%'>(unitless)</td> <td style='width:50%'>(unitless)</td> </tr>")
                                str.Append("</table>")
                                tdInner.Text = str.ToString()
                                InnerTdSetting(tdInner, DWidth, "Center")
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 2

                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Electricity", trInner, "AlterNateColor1")
                            trInner.ID = "EESP1"
                            For k = 0 To DataCnt
                                'Electricity
                                tdInner = New TableCell
                                str = New StringBuilder()
                                str.Append("<table style='width:100%' border='0' >")

                                str.Append("<tr align='right'> <td style='width:50%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("ERS" + (j - 1).ToString() + "").ToString(), 3) + "</td> <td>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("ERP" + (j - 1).ToString() + "").ToString(), 3) + "</td> </tr>")
                                str.Append("</table>")
                                tdInner.Text = str.ToString()
                                InnerTdSetting(tdInner, DWidth, "Center")
                                trInner.Controls.Add(tdInner)

                            Next

                        Case 3

                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Natural Gas", trInner, "AlterNateColor2")
                            trInner.ID = "NESP1"
                            For k = 0 To DataCnt
                                'Natural Gas
                                tdInner = New TableCell
                                str = New StringBuilder()
                                str.Append("<table style='width:100%' border='0' >")
                                str.Append("<tr align='right'> <td style='width:50%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("ERS" + (j - 1).ToString() + "").ToString(), 3) + "</td> <td>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("ERP" + (j - 1).ToString() + "").ToString(), 3) + "</td> </tr>")
                                str.Append("</table>")
                                tdInner.Text = str.ToString()
                                InnerTdSetting(tdInner, DWidth, "Center")
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 4
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "<b>Conversion Factors</b>", trInner, "AlterNateColor4")
                            For k = 0 To DataCnt
                                'Energy Type
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Center")
                                str = New StringBuilder()
                                str.Append("<table style='width:100%' border='0' >")
                                str.Append("<tr align='right' style='font-weight:bold'> <td style='width:50%'>Suggested</td> <td style='width:50%'>Preferred</td> </tr>")
                                str.Append("</table>")
                                tdInner.Text = str.ToString()
                                InnerTdSetting(tdInner, DWidth, "Center")
                                trInner.Controls.Add(tdInner)
                            Next



                        Case 5

                            tdInner = New TableCell
                            Title = dstbl.Tables(0).Rows(0).Item("CONVERSIONFACT" + (j - 4).ToString() + "").ToString()
                            LeftTdSetting(tdInner, "" + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "CA1"
                            For k = 0 To DataCnt
                                'Electricity
                                tdInner = New TableCell
                                str = New StringBuilder()
                                str.Append("<table style='width:100%' border='0' >")
                                str.Append("<tr align='right'> <td style='width:50%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("CFACTORSUG" + (j - 4).ToString() + "").ToString(), 3) + "</td> <td>" + "" + "</td></tr>")
                                str.Append("</table>")
                                tdInner.Text = str.ToString()
                                InnerTdSetting(tdInner, DWidth, "right")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 6

                            tdInner = New TableCell
                            Title = dstbl.Tables(0).Rows(0).Item("CONVERSIONFACT" + (j - 4).ToString() + "").ToString()
                            LeftTdSetting(tdInner, "" + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "CB1"
                            For k = 0 To DataCnt
                                'Electricity
                                tdInner = New TableCell
                                str = New StringBuilder()
                                str.Append("<table style='width:100%' border='0' >")
                                str.Append("<tr align='right'> <td style='width:50%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("CFACTORSUG" + (j - 4).ToString() + "").ToString(), 3) + "</td> <td>" + "" + "</td> </tr>")
                                str.Append("</table>")
                                tdInner.Text = str.ToString()
                                InnerTdSetting(tdInner, DWidth, "right")
                                trInner.Controls.Add(tdInner)

                            Next

                        Case 7

                            tdInner = New TableCell
                            Title = dstbl.Tables(0).Rows(0).Item("CONVERSIONFACT" + (j - 4).ToString() + "").ToString()
                            LeftTdSetting(tdInner, "" + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "CC1"
                            For k = 0 To DataCnt
                                'Electricity
                                tdInner = New TableCell
                                str = New StringBuilder()
                                str.Append("<table style='width:100%' border='0' >")
                                str.Append("<tr align='right'> <td style='width:50%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("CFACTORSUG" + (j - 4).ToString() + "").ToString(), 3) + "</td> <td>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("CFACTORPREF" + (j - 4).ToString()).ToString(), 3) + "</td> </tr>")
                                str.Append("</table>")
                                tdInner.Text = str.ToString()
                                InnerTdSetting(tdInner, DWidth, "right")
                                trInner.Controls.Add(tdInner)

                            Next
                        Case 8

                            tdInner = New TableCell
                            Title = dstbl.Tables(0).Rows(0).Item("CONVERSIONFACT" + (j - 4).ToString() + "").ToString()
                            LeftTdSetting(tdInner, "" + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "CD1"
                            For k = 0 To DataCnt
                                'Electricity
                                tdInner = New TableCell
                                str = New StringBuilder()
                                str.Append("<table style='width:100%' border='0' >")
                                str.Append("<tr align='right'> <td style='width:50%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("CFACTORSUG" + (j - 4).ToString() + "").ToString(), 3) + "</td> <td>" + "" + "</td> </tr>")
                                str.Append("</table>")
                                tdInner.Text = str.ToString()
                                InnerTdSetting(tdInner, DWidth, "right")
                                trInner.Controls.Add(tdInner)

                            Next
                        Case 9

                            tdInner = New TableCell
                            Title = dstbl.Tables(0).Rows(0).Item("CONVERSIONFACT" + (j - 4).ToString() + "").ToString()
                            LeftTdSetting(tdInner, "" + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "CE1"
                            For k = 0 To DataCnt
                                'Electricity
                                tdInner = New TableCell
                                str = New StringBuilder()
                                str.Append("<table style='width:100%' border='0' >")
                                str.Append("<tr align='right'> <td style='width:50%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("CFACTORSUG" + (j - 4).ToString() + "").ToString(), 3) + "</td> <td>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("CFACTORPREF" + (j - 4).ToString()).ToString(), 3) + "</td> </tr>")
                                str.Append("</table>")
                                tdInner.Text = str.ToString()
                                InnerTdSetting(tdInner, DWidth, "right")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 10
                            tdInner = New TableCell
                            Title = dstbl.Tables(0).Rows(0).Item("CONVERSIONFACT" + (j - 4).ToString() + "").ToString()
                            LeftTdSetting(tdInner, "" + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "CF1"
                            For k = 0 To DataCnt
                                'Electricity
                                tdInner = New TableCell
                                str = New StringBuilder()
                                str.Append("<table style='width:100%' border='0' >")
                                str.Append("<tr align='right'> <td style='width:50%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("CFACTORSUG" + (j - 4).ToString() + "").ToString(), 3) + "</td> <td>" + "" + "</td> </tr>")
                                str.Append("</table>")
                                tdInner.Text = str.ToString()
                                InnerTdSetting(tdInner, DWidth, "right")
                                trInner.Controls.Add(tdInner)

                            Next

                        Case 11, 12
                            Continue For
                            'tdInner = New TableCell
                            'Title = dstbl.Tables(0).Rows(0).Item("CONVERSIONFACT" + (j - 4).ToString() + "").ToString()
                            'LeftTdSetting(tdInner, "" + Title + "", trInner, "AlterNateColor1")
                            'trInner.ID = "CF_1"
                            'For k = 0 To DataCnt
                            '    'Electricity
                            '    tdInner = New TableCell
                            '    str = New StringBuilder()
                            '    str.Append("<table style='width:100%' border='0' >")
                            '    str.Append("<tr align='right'> <td style='width:50%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("CFACTORSUG" + (j - 4).ToString() + "").ToString(), 3) + "</td> <td>" + "" + "</td> </tr>")
                            '    str.Append("</table>")
                            '    tdInner.Text = str.ToString()
                            '    InnerTdSetting(tdInner, DWidth, "right")
                            '    trInner.Controls.Add(tdInner)
                            'Next
                        Case 13
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "<b>Energy Requirements</b>", trInner, "AlterNateColor4")
                            For k = 0 To DataCnt
                                'Energy Requirements
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Center")
                                str = New StringBuilder()
                                str.Append("<table style='width:100%' border='0' >")
                                str.Append("<tr align='right' style='font-weight:bold'> <td style='width:50%'>Electricity</td> <td style='width:50%'>Natural Gas</td> </tr>")
                                str.Append("<tr align='right' style='font-weight:bold'> <td style='width:50%'>(kwh)</td> <td style='width:50%'>(cubic ft)</td> </tr>")
                                str.Append("</table>")
                                tdInner.Text = str.ToString()
                                InnerTdSetting(tdInner, DWidth, "Center")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 14

                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Production", trInner, "AlterNateColor1")
                            trInner.ID = "ERPROD1"
                            For k = 0 To DataCnt
                                'Energy Requirements Production
                                tdInner = New TableCell
                                str = New StringBuilder()
                                str.Append("<table style='width:100%' border='0' >")
                                str.Append("<tr align='right'> <td style='width:50%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("ELECA" + (j - 13).ToString() + "").ToString(), 0) + "</td> <td>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("NGASA" + (j - 13).ToString() + "").ToString(), 0) + "</td> </tr>")
                                str.Append("</table>")
                                tdInner.Text = str.ToString()
                                InnerTdSetting(tdInner, DWidth, "Center")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 15
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Warehouse", trInner, "AlterNateColor2")
                            trInner.ID = "ERWARE1"
                            For k = 0 To DataCnt
                                'Energy Requirements Warehouse
                                tdInner = New TableCell
                                str = New StringBuilder()
                                str.Append("<table style='width:100%' border='0' >")
                                str.Append("<tr align='right'> <td style='width:50%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("ELECA" + (j - 13).ToString() + "").ToString(), 0) + "</td> <td>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("NGASA" + (j - 13).ToString() + "").ToString(), 0) + "</td> </tr>")
                                str.Append("</table>")
                                tdInner.Text = str.ToString()
                                InnerTdSetting(tdInner, DWidth, "Center")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 16
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Office", trInner, "AlterNateColor1")
                            trInner.ID = "EROFF1"
                            For k = 0 To DataCnt
                                'Energy Requirements Office
                                tdInner = New TableCell
                                str = New StringBuilder()
                                str.Append("<table style='width:100%' border='0' >")
                                str.Append("<tr align='right'> <td style='width:50%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("ELECA" + (j - 13).ToString() + "").ToString(), 0) + "</td> <td>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("NGASA" + (j - 13).ToString() + "").ToString(), 0) + "</td> </tr>")
                                str.Append("</table>")
                                tdInner.Text = str.ToString()
                                InnerTdSetting(tdInner, DWidth, "Center")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 17
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Support", trInner, "AlterNateColor2")
                            trInner.ID = "ERSUPP1"
                            For k = 0 To DataCnt
                                'Energy Requirements Support
                                tdInner = New TableCell
                                str = New StringBuilder()
                                str.Append("<table style='width:100%' border='0' >")
                                str.Append("<tr align='right'> <td style='width:50%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("ELECA" + (j - 13).ToString() + "").ToString(), 0) + "</td> <td>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("NGASA" + (j - 13).ToString() + "").ToString(), 0) + "</td> </tr>")
                                str.Append("</table>")
                                tdInner.Text = str.ToString()
                                InnerTdSetting(tdInner, DWidth, "Center")
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 18
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Total", trInner, "AlterNateColor1")
                            trInner.ID = "EATOT1"
                            For k = 0 To DataCnt

                                'Energy Requirements Support
                                tdInner = New TableCell
                                str = New StringBuilder()
                                str.Append("<table style='width:100%' border='0' >")
                                'str.Append("<tr align='right' style='font-weight:bold'> <td>Electricity</td> <td>Natural Gas</td> </tr>")
                                str.Append("<tr align='right'> <td style='width:50%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("ELECA5").ToString(), 0) + "</td> <td>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("NGASA5").ToString(), 0) + "</td> </tr>")
                                str.Append("</table>")
                                tdInner.Text = str.ToString()
                                InnerTdSetting(tdInner, DWidth, "right")
                                trInner.Controls.Add(tdInner)
                            Next



                        Case 19
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "<b>Energy in Cradle Equivalents</b>", trInner, "AlterNateColor4")
                            For k = 0 To DataCnt
                                'Energy in Cradle Equivalents 
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Center")
                                str = New StringBuilder()
                                str.Append("<table style='width:100%' ")
                                str.Append("<tr align='right' style='font-weight:bold'> <td style='width:33%'>Electricity</td> <td style='width:33%'>Nat. Gas</td> <td style='width:33%'>Total</td> </tr>")
                                str.Append("<tr align='right' style='font-weight:bold'> <td style='width:33%'>(MJ)</td> <td style='width:33%'>(MJ)</td> <td style='width:33%'>(MJ)</td> </tr>")
                                str.Append("</table>")
                                tdInner.Text = str.ToString()
                                InnerTdSetting(tdInner, DWidth, "right")
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 20
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Production", trInner, "AlterNateColor2")
                            trInner.ID = "ECEPROD1"
                            For k = 0 To DataCnt
                                'Energy in Cradle Equivalents Production
                                tdInner = New TableCell
                                str = New StringBuilder()
                                str.Append("<table style='width:100%' border='0' >")
                                'str.Append("<tr align='right' style='font-weight:bold'> <td style='width:33%'>Electricity</td> <td style='width:33%'>Natural Gas</td> <td style='width:33%'>Total</td> </tr>")
                                str.Append("<tr align='right'> <td style='width:33%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("ELECB" + (j - 19).ToString() + "").ToString(), 0) + "</td> <td style='width:33%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("NGASB" + (j - 19).ToString() + "").ToString(), 0) + "</td> <td>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("TOTALA" + (j - 19).ToString() + "").ToString(), 0) + "</td></tr>")
                                str.Append("</table>")
                                tdInner.Text = str.ToString()
                                InnerTdSetting(tdInner, DWidth, "right")
                                trInner.Controls.Add(tdInner)

                            Next

                        Case 21

                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Warehouse", trInner, "AlterNateColor1")
                            trInner.ID = "ECEWARE1"
                            For k = 0 To DataCnt
                                'Energy in Cradle Equivalents Warehouse
                                tdInner = New TableCell
                                str = New StringBuilder()
                                str.Append("<table style='width:100%' border='0' >")
                                'str.Append("<tr align='right' style='font-weight:bold'> <td style='width:33%'>Electricity</td> <td style='width:33%'>Natural Gas</td> <td style='width:33%'>Total</td> </tr>")
                                str.Append("<tr align='right'> <td style='width:33%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("ELECB" + (j - 19).ToString() + "").ToString(), 0) + "</td> <td style='width:33%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("NGASB" + (j - 19).ToString() + "").ToString(), 0) + "</td> <td>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("TOTALA" + (j - 19).ToString() + "").ToString(), 0) + "</td></tr>")
                                str.Append("</table>")
                                tdInner.Text = str.ToString()
                                InnerTdSetting(tdInner, DWidth, "right")
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 22


                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Office", trInner, "AlterNateColor2")
                            trInner.ID = "ECEOFF1"
                            For k = 0 To DataCnt
                                'Energy in Cradle Equivalents Office
                                tdInner = New TableCell
                                str = New StringBuilder()
                                str.Append("<table style='width:100%' border='0' >")
                                'str.Append("<tr align='right' style='font-weight:bold'> <td style='width:33%'>Electricity</td> <td style='width:33%'>Natural Gas</td> <td style='width:33%'>Total</td> </tr>")
                                str.Append("<tr align='right'> <td style='width:33%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("ELECB" + (j - 19).ToString() + "").ToString(), 0) + "</td> <td style='width:33%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("NGASB" + (j - 19).ToString() + "").ToString(), 0) + "</td> <td>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("TOTALA" + (j - 19).ToString() + "").ToString(), 0) + "</td></tr>")
                                str.Append("</table>")
                                tdInner.Text = str.ToString()
                                InnerTdSetting(tdInner, DWidth, "right")
                                trInner.Controls.Add(tdInner)

                            Next

                        Case 23


                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Support", trInner, "AlterNateColor1")
                            trInner.ID = "ECESUPP1"
                            For k = 0 To DataCnt
                                'Energy in Cradle Equivalents Suppourt
                                tdInner = New TableCell
                                str = New StringBuilder()
                                str.Append("<table style='width:100%' border='0' >")
                                'str.Append("<tr align='right' style='font-weight:bold'> <td style='width:33%'>Electricity</td> <td style='width:33%'>Natural Gas</td> <td style='width:33%'>Total</td> </tr>")
                                str.Append("<tr align='right'> <td style='width:33%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("ELECB" + (j - 19).ToString() + "").ToString(), 0) + "</td> <td style='width:33%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("NGASB" + (j - 19).ToString() + "").ToString(), 0) + "</td> <td>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("TOTALA" + (j - 19).ToString() + "").ToString(), 0) + "</td></tr>")
                                str.Append("</table>")
                                tdInner.Text = str.ToString()
                                InnerTdSetting(tdInner, DWidth, "right")
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 24
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Total", trInner, "AlterNateColor2")
                            trInner.ID = "EBTOT1"
                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                str = New StringBuilder()
                                str.Append("<table style='width:100%' border='0' >")
                                str.Append("<tr align='right'> <td style='width:33%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("ELECB5").ToString(), 0) + "</td> <td style='width:33%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("NGASB5").ToString(), 0) + "</td> <td>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("TOTALA5").ToString(), 0) + "</td></tr>")

                                str.Append("</table>")
                                tdInner.Text = str.ToString()
                                InnerTdSetting(tdInner, DWidth, "right")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 25
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "<b>CO2 in Cradle Equivalents</b>", trInner, "AlterNateColor4")
                            For k = 0 To DataCnt

                                'Co2 in Cradle Equivalents 
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Center")
                                str = New StringBuilder()
                                str.Append("<table style='width:100%' ")
                                str.Append("<tr align='right' style='font-weight:bold'> <td style='width:33%'>Electricity</td> <td style='width:33%'>Nat. Gas</td> <td style='width:33%'>Total</td> </tr>")
                                str.Append("<tr align='right' style='font-weight:bold'> <td style='width:33%'>(" + dstbl.Tables(k).Rows(0).Item("TITLE8").ToString() + ")</td> <td style='width:33%'>(" + dstbl.Tables(k).Rows(0).Item("TITLE8").ToString() + ")</td> <td style='width:33%'>(" + dstbl.Tables(k).Rows(0).Item("TITLE8").ToString() + ")</td> </tr>")
                                str.Append("</table>")
                                tdInner.Text = str.ToString()
                                InnerTdSetting(tdInner, DWidth, "right")
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 26
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Production", trInner, "AlterNateColor2")
                            trInner.ID = "GCEPROD1"
                            For k = 0 To DataCnt

                                'Co2 in Cradle Equivalents Production
                                tdInner = New TableCell
                                str = New StringBuilder()
                                str.Append("<table style='width:100%' border='0' >")
                                'str.Append("<tr align='right' style='font-weight:bold'> <td style='width:33%'>Electricity</td> <td style='width:33%'>Natural Gas</td> <td style='width:33%'>Total</td> </tr>")
                                str.Append("<tr align='right'> <td style='width:33%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("CO2ELEC" + (j - 25).ToString() + "").ToString(), 0) + "</td> <td style='width:33%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("CO2NG" + (j - 25).ToString() + "").ToString(), 0) + "</td> <td>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("TOTALB" + (j - 25).ToString() + "").ToString(), 0) + "</td></tr>")
                                str.Append("</table>")
                                tdInner.Text = str.ToString()
                                InnerTdSetting(tdInner, DWidth, "right")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 27
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Warehouse", trInner, "AlterNateColor1")
                            trInner.ID = "GCEWARE1"
                            For k = 0 To DataCnt

                                'Co2 in Cradle Equivalents Warehouse
                                tdInner = New TableCell
                                str = New StringBuilder()
                                str.Append("<table style='width:100%' border='0' >")
                                'str.Append("<tr align='right' style='font-weight:bold'> <td style='width:33%'>Electricity</td> <td style='width:33%'>Natural Gas</td> <td style='width:33%'>Total</td> </tr>")
                                str.Append("<tr align='right'> <td style='width:33%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("CO2ELEC" + (j - 25).ToString() + "").ToString(), 0) + "</td> <td style='width:33%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("CO2NG" + (j - 25).ToString() + "").ToString(), 0) + "</td> <td>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("TOTALB" + (j - 25).ToString() + "").ToString(), 0) + "</td></tr>")
                                str.Append("</table>")
                                tdInner.Text = str.ToString()
                                InnerTdSetting(tdInner, DWidth, "right")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 28
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Office", trInner, "AlterNateColor2")
                            trInner.ID = "GCEOFF1"
                            For k = 0 To DataCnt

                                'Co2 in Cradle Equivalents Office
                                tdInner = New TableCell
                                str = New StringBuilder()
                                str.Append("<table style='width:100%' border='0' >")
                                'str.Append("<tr align='right' style='font-weight:bold'> <td style='width:33%'>Electricity</td> <td style='width:33%'>Natural Gas</td> <td style='width:33%'>Total</td> </tr>")
                                str.Append("<tr align='right'> <td style='width:33%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("CO2ELEC" + (j - 25).ToString() + "").ToString(), 0) + "</td> <td style='width:33%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("CO2NG" + (j - 25).ToString() + "").ToString(), 0) + "</td> <td>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("TOTALB" + (j - 25).ToString() + "").ToString(), 0) + "</td></tr>")
                                str.Append("</table>")
                                tdInner.Text = str.ToString()
                                InnerTdSetting(tdInner, DWidth, "right")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 29
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Support", trInner, "AlterNateColor1")
                            trInner.ID = "GCESUPP1"
                            For k = 0 To DataCnt
                                'Co2 in Cradle Equivalents Suppourt
                                tdInner = New TableCell
                                str = New StringBuilder()
                                str.Append("<table style='width:100%' border='0' >")
                                'str.Append("<tr align='right' style='font-weight:bold'> <td style='width:33%'>Electricity</td> <td style='width:33%'>Natural Gas</td> <td style='width:33%'>Total</td> </tr>")
                                str.Append("<tr align='right'> <td style='width:33%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("CO2ELEC" + (j - 25).ToString() + "").ToString(), 0) + "</td> <td style='width:33%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("CO2NG" + (j - 25).ToString() + "").ToString(), 0) + "</td> <td>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("TOTALB" + (j - 25).ToString() + "").ToString(), 0) + "</td></tr>")
                                str.Append("</table>")
                                tdInner.Text = str.ToString()
                                InnerTdSetting(tdInner, DWidth, "right")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 30
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Total", trInner, "AlterNateColor2")
                            trInner.ID = "ECTOT1"
                            For k = 0 To DataCnt
                                'Energy Requirements Support
                                tdInner = New TableCell
                                str = New StringBuilder()
                                str.Append("<table style='width:100%' border='0' >")
                                'str.Append("<tr align='right' style='font-weight:bold'> <td>Electricity</td> <td>Natural Gas</td> </tr>")
                                str.Append("<tr align='right'> <td style='width:33%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("CO2ELEC5").ToString(), 0) + "</td> <td>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("CO2NG5").ToString(), 0) + "</td><td style='width:33%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("TOTALB5").ToString(), 0) + "</td> </tr>")
                                str.Append("</table>")
                                tdInner.Text = str.ToString()
                                InnerTdSetting(tdInner, DWidth, "right")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 31
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "<b>Water in Cradle Equivalents</b>", trInner, "AlterNateColor4")
                            For k = 0 To DataCnt

                                'Co2 in Cradle Equivalents 
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Center")
                                str = New StringBuilder()
                                str.Append("<table style='width:100%' ")
                                str.Append("<tr align='right' style='font-weight:bold'> <td style='width:10%'>Water</td><td style='width:10%'>Electricity</td> <td style='width:10%'>Nat. Gas</td> <td style='width:10%'>Total</td> </tr>")
                                str.Append("<tr align='right' style='font-weight:bold'><td style='width:10%'>(" + dstbl.Tables(k).Rows(0).Item("TITLE10").ToString() + ")</td> <td style='width:10%'>(" + dstbl.Tables(k).Rows(0).Item("TITLE10").ToString() + ")</td> <td style='width:10%'>(" + dstbl.Tables(k).Rows(0).Item("TITLE10").ToString() + ")</td> <td style='width:10%'>(" + dstbl.Tables(k).Rows(0).Item("TITLE10").ToString() + ")</td> </tr>")
                                str.Append("</table>")
                                tdInner.Text = str.ToString()
                                InnerTdSetting(tdInner, DWidth, "right")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 32
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Production", trInner, "AlterNateColor1")
                            trInner.ID = "WATPROD1"
                            For k = 0 To DataCnt

                                'Co2 in Cradle Equivalents Production
                                tdInner = New TableCell
                                str = New StringBuilder()
                                str.Append("<table style='width:100%' border='0' >")
                                'str.Append("<tr align='right' style='font-weight:bold'> <td style='width:33%'>Electricity</td> <td style='width:33%'>Natural Gas</td> <td style='width:33%'>Total</td> </tr>")
                                str.Append("<tr align='right'> <td style='width:25%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("ENWATERA" + (j - 31).ToString() + "").ToString(), 0) + "</td><td style='width:25%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("WATERELEC" + (j - 31).ToString() + "").ToString(), 0) + "</td> <td style='width:25%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("WATERNG" + (j - 31).ToString() + "").ToString(), 0) + "</td> <td>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("TOTALC" + (j - 31).ToString() + "").ToString(), 0) + "</td></tr>")
                                str.Append("</table>")
                                tdInner.Text = str.ToString()
                                InnerTdSetting(tdInner, DWidth, "right")
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 33
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Warehouse", trInner, "AlterNateColor2")
                            trInner.ID = "WATWARE1"
                            For k = 0 To DataCnt

                                'Co2 in Cradle Equivalents Warehouse
                                tdInner = New TableCell
                                str = New StringBuilder()
                                str.Append("<table style='width:100%' border='0' >")
                                'str.Append("<tr align='right'> <td style='width:33%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("WATERELEC" + (j - 31).ToString() + "").ToString(), 0) + "</td> <td style='width:33%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("WATERNG" + (j - 31).ToString() + "").ToString(), 0) + "</td> <td>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("TOTALC" + (j - 31).ToString() + "").ToString(), 0) + "</td></tr>")
                                str.Append("<tr align='right'> <td style='width:25%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("ENWATERA" + (j - 31).ToString() + "").ToString(), 0) + "</td><td style='width:25%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("WATERELEC" + (j - 31).ToString() + "").ToString(), 0) + "</td> <td style='width:25%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("WATERNG" + (j - 31).ToString() + "").ToString(), 0) + "</td> <td>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("TOTALC" + (j - 31).ToString() + "").ToString(), 0) + "</td></tr>")
                                str.Append("</table>")
                                tdInner.Text = str.ToString()
                                InnerTdSetting(tdInner, DWidth, "right")
                                trInner.Controls.Add(tdInner)


                            Next

                        Case 34
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Office", trInner, "AlterNateColor1")
                            trInner.ID = "WATOFF1"
                            For k = 0 To DataCnt

                                'Co2 in Cradle Equivalents Office
                                tdInner = New TableCell
                                str = New StringBuilder()
                                str.Append("<table style='width:100%' border='0' >")
                                'str.Append("<tr align='right'> <td style='width:33%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("WATERELEC" + (j - 31).ToString() + "").ToString(), 0) + "</td> <td style='width:33%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("WATERNG" + (j - 31).ToString() + "").ToString(), 0) + "</td> <td>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("TOTALC" + (j - 31).ToString() + "").ToString(), 0) + "</td></tr>")
                                str.Append("<tr align='right'> <td style='width:25%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("ENWATERA" + (j - 31).ToString() + "").ToString(), 0) + "</td><td style='width:25%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("WATERELEC" + (j - 31).ToString() + "").ToString(), 0) + "</td> <td style='width:25%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("WATERNG" + (j - 31).ToString() + "").ToString(), 0) + "</td> <td>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("TOTALC" + (j - 31).ToString() + "").ToString(), 0) + "</td></tr>")
                                str.Append("</table>")
                                tdInner.Text = str.ToString()
                                InnerTdSetting(tdInner, DWidth, "right")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 35
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Support", trInner, "AlterNateColor2")
                            trInner.ID = "WATSUPP1"
                            For k = 0 To DataCnt
                                'Co2 in Cradle Equivalents Suppourt
                                tdInner = New TableCell
                                str = New StringBuilder()
                                str.Append("<table style='width:100%' border='0' >")
                                'str.Append("<tr align='right'> <td style='width:33%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("WATERELEC" + (j - 31).ToString() + "").ToString(), 0) + "</td> <td style='width:33%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("WATERNG" + (j - 31).ToString() + "").ToString(), 0) + "</td> <td>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("TOTALC" + (j - 31).ToString() + "").ToString(), 0) + "</td></tr>")
                                str.Append("<tr align='right'> <td style='width:25%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("ENWATERA" + (j - 31).ToString() + "").ToString(), 0) + "</td><td style='width:25%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("WATERELEC" + (j - 31).ToString() + "").ToString(), 0) + "</td> <td style='width:25%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("WATERNG" + (j - 31).ToString() + "").ToString(), 0) + "</td> <td>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("TOTALC" + (j - 31).ToString() + "").ToString(), 0) + "</td></tr>")
                                str.Append("</table>")
                                tdInner.Text = str.ToString()
                                InnerTdSetting(tdInner, DWidth, "right")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 36
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Total", trInner, "AlterNateColor1")
                            trInner.ID = "EDTOT1"

                            For k = 0 To DataCnt

                                'Energy Requirements Support
                                tdInner = New TableCell
                                str = New StringBuilder()
                                str.Append("<table style='width:100%' border='0' >")
                                'str.Append("<tr align='right'> <td style='width:33%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("WATERELEC5").ToString(), 0) + "</td> <td>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("WATERNG5").ToString(), 0) + "</td><td>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("TOTALC5").ToString(), 0) + "</td> </tr>")
                                str.Append("<tr align='right'> <td style='width:25%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("ENWATERA" + (j - 31).ToString() + "").ToString(), 0) + "</td><td style='width:25%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("WATERELEC" + (j - 31).ToString() + "").ToString(), 0) + "</td> <td style='width:25%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("WATERNG" + (j - 31).ToString() + "").ToString(), 0) + "</td> <td>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("TOTALC" + (j - 31).ToString() + "").ToString(), 0) + "</td></tr>")
                                str.Append("</table>")
                                tdInner.Text = str.ToString()
                                InnerTdSetting(tdInner, DWidth, "right")
                                trInner.Controls.Add(tdInner)
                            Next
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

    Protected Sub TextBoxSetting(ByVal txt As TextBox, ByVal Css As String)
        Try


            txt.CssClass = Css
            txt.Enabled = False
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
End Class
