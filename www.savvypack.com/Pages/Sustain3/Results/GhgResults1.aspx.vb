Imports System.Data
Imports System.Data.OleDb
Imports System
Imports S3GetData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Sustain3_Results_GhgResults1
    Inherits System.Web.UI.Page
#Region "Get Set Variables"
    Dim _lErrorLble As Label
    Dim _strUserName As String
    Dim _strPassword As String
    Dim _iAssumptionId As Integer
    Dim _btnCompare As ImageButton


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
    Public Property Comparebtn() As ImageButton
        Get
            Return _btnCompare
        End Get
        Set(ByVal value As ImageButton)
            _btnCompare = value
        End Set
    End Property


    Public DataCnt As Integer
    Public CaseDesp As New ArrayList

#End Region

#Region "MastePage Content Variables"
    Protected Sub GetErrorLable()
        ErrorLable = Page.Master.FindControl("lblError")
    End Sub
    Protected Sub GetComparebtn()
        Dim obj As New CryptoHelper()
        Comparebtn = Page.Master.FindControl("imgCompare")
        Comparebtn.Visible = True
        Dim objCryptoHelper As New CryptoHelper()
        Comparebtn.OnClientClick = "return Compare('../../../Charts/S3Charts/CErgyGHG.aspx?ErgyGHG=" + objCryptoHelper.Encrypt("GHG") + "&ChartType=" + objCryptoHelper.Encrypt("RBC") + "&CType=" + objCryptoHelper.Encrypt("Total") + "') "

    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Try
                GetErrorLable()
                GetSessionDetails()
                GetComparebtn()
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
        Dim objGetData As New S3GetData.Selectdata

        Try
            CaseIds = objGetData.Cases1(AssumptionId)

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetCaseIds:" + ex.Message.ToString()
        End Try
        Return CaseIds
    End Function

    Protected Sub GetPageDetails()
        Dim ds As New DataSet
        Dim dsCaseDetails As New DataSet
        Dim dstbl As New DataSet
        Dim objGetData As New S1GetData.Selectdata
        Dim CaseIds As String = String.Empty
        'Dim DataCnt As New Integer
        Dim i As New Integer
        Dim j As New Integer
        Dim k As New Integer
        Dim DWidth As String = String.Empty
        Dim arrCaseID() As String

        Try
            arrCaseID = GetCaseIds()
            'ds = objGetData.GhgResults(CaseIds, UserName)
            DataCnt = arrCaseID.Length - 1

            Dim trHeader As New TableRow
            Dim tdHeader As TableCell
            Dim tdInner As TableCell
            Dim str As StringBuilder
            Dim Cunits As New Integer
            Dim Units As New Integer
            Dim Title As String = String.Empty
            DWidth = txtDWidth.Text + "px"


            tdHeader = New TableCell
            HeaderTdSetting(tdHeader, DWidth, "<img alt='' src='../../Images/spacer.gif' Style='width:200px;height:0px;'  />", 1)
            trHeader.Controls.Add(tdHeader)
            trHeader.Height = 20
            trHeader.CssClass = "PageSSHeading"


            For i = 0 To DataCnt
                ds = objGetData.GetGhgResults(arrCaseID(i))
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
            For j = 1 To 1
                For i = 1 To 15
                    Dim trInner As New TableRow
                    Select Case i
                        Case 1
                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("Title8").ToString() + ")"
                            LeftTdSetting(tdInner, "Sales Volume " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "M1_" + j.ToString()
                            For k = 0 To DataCnt
                                'M1
                                tdInner = New TableCell
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("SVOLUME").ToString(), 0)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 2
                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("SUNIT").ToString().Replace((dstbl.Tables(0).Rows(0).Item("TITLE8").ToString() + " gr gas /"), "") + ")"
                            LeftTdSetting(tdInner, "Sales Volume " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "M2_" + j.ToString()
                            For k = 0 To DataCnt
                                'M2
                                tdInner = New TableCell
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("SUNITVAL").ToString(), 0)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 3

                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "", trInner, "AlterNateColor4")
                            For k = 0 To DataCnt
                                'Break2
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Center")
                                str = New StringBuilder()
                                str.Append("<table style='width:100%' border='0' >")
                                str.Append("<tr align='right' style='font-weight:bold'> <td style='width:50%'>(" + dstbl.Tables(k).Rows(0).Item("TITLE8").ToString() + ")</td> <td style='width:50%'>(% Of Total)</td> </tr>")
                                str.Append("</table>")
                                tdInner.Text = str.ToString()
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 4
                            Title = ""
                            tdInner = New TableCell
                            'Title = "(MJ)"
                            LeftTdSetting(tdInner, "Raw Materials" + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "M3_" + j.ToString()
                            For k = 0 To DataCnt
                                'M3
                                tdInner = New TableCell
                                str = New StringBuilder()
                                str.Append("<table style='width:100%' border='0' >")
                                If CDbl(dstbl.Tables(k).Rows(0).Item("SVOLUME")) <> 0 Then
                                    str.Append("<tr align='right'> <td style='width:50%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("T1").ToString(), 0) + "</td> <td style='width:50%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("T1").ToString() / dstbl.Tables(k).Rows(0).Item("T8").ToString() * 100, 2) + "</td> </tr>")
                                    str.Append("</table>")
                                    tdInner.Text = str.ToString()
                                Else
                                    tdInner.Text = "na"
                                End If
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)

                            Next

                        Case 5
                            tdInner = New TableCell
                            'Title = "(MJ)"
                            LeftTdSetting(tdInner, "Raw Materials Packaging" + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "M4_" + j.ToString()
                            For k = 0 To DataCnt
                                'M4
                                tdInner = New TableCell
                                str = New StringBuilder()
                                str.Append("<table style='width:100%' border='0' >")
                                If CDbl(dstbl.Tables(k).Rows(0).Item("SVOLUME")) <> 0 Then
                                    str.Append("<tr align='right'> <td style='width:50%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("T2").ToString(), 0) + "</td> <td style='width:50%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("T2").ToString() / dstbl.Tables(k).Rows(0).Item("T8").ToString() * 100, 2) + "</td> </tr>")
                                    str.Append("</table>")
                                    tdInner.Text = str.ToString()
                                Else
                                    tdInner.Text = "na"
                                End If
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)
                            Next


                        Case 6
                            tdInner = New TableCell
                            'Title = "(MJ)"
                            LeftTdSetting(tdInner, "RM & Pack Transport" + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "M5_" + j.ToString()
                            For k = 0 To DataCnt
                                'M5
                                tdInner = New TableCell
                                str = New StringBuilder()
                                str.Append("<table style='width:100%' border='0' >")
                                If CDbl(dstbl.Tables(k).Rows(0).Item("SVOLUME")) <> 0 Then
                                    str.Append("<tr align='right'> <td style='width:50%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("T3").ToString(), 0) + "</td> <td style='width:50%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("T3").ToString() / dstbl.Tables(k).Rows(0).Item("T8").ToString() * 100, 2) + "</td> </tr>")
                                    str.Append("</table>")
                                    tdInner.Text = str.ToString()
                                Else
                                    tdInner.Text = "na"
                                End If
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)

                            Next


                        Case 7
                            tdInner = New TableCell
                            'Title = "(MJ)"
                            LeftTdSetting(tdInner, "Process" + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "M6_" + j.ToString()
                            For k = 0 To DataCnt
                                'M6
                                tdInner = New TableCell
                                str = New StringBuilder()
                                str.Append("<table style='width:100%' border='0' >")
                                If CDbl(dstbl.Tables(k).Rows(0).Item("SVOLUME")) <> 0 Then
                                    str.Append("<tr align='right'> <td style='width:50%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("T4").ToString(), 0) + "</td> <td style='width:50%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("T4").ToString() / dstbl.Tables(k).Rows(0).Item("T8").ToString() * 100, 2) + "</td> </tr>")
                                    str.Append("</table>")
                                    tdInner.Text = str.ToString()
                                Else
                                    tdInner.Text = "na"
                                End If
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)
                            Next


                        Case 8
                            tdInner = New TableCell
                            'Title = "(MJ)"
                            LeftTdSetting(tdInner, "Distribution Packaging" + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "M7_" + j.ToString()
                            For k = 0 To DataCnt
                                'M7
                                tdInner = New TableCell
                                str = New StringBuilder()
                                str.Append("<table style='width:100%' border='0' >")
                                If CDbl(dstbl.Tables(k).Rows(0).Item("SVOLUME")) <> 0 Then
                                    str.Append("<tr align='right'> <td style='width:50%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("T5").ToString(), 0) + "</td> <td style='width:50%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("T5").ToString() / dstbl.Tables(k).Rows(0).Item("T8").ToString() * 100, 2) + "</td> </tr>")
                                    str.Append("</table>")
                                    tdInner.Text = str.ToString()
                                Else
                                    tdInner.Text = "na"
                                End If
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)

                            Next




                        Case 9
                            tdInner = New TableCell
                            'Title = "(MJ)"
                            LeftTdSetting(tdInner, "DP Transport" + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "M8_" + j.ToString()
                            For k = 0 To DataCnt
                                'M8
                                tdInner = New TableCell
                                str = New StringBuilder()
                                str.Append("<table style='width:100%' border='0' >")
                                If CDbl(dstbl.Tables(k).Rows(0).Item("SVOLUME")) <> 0 Then
                                    str.Append("<tr align='right'> <td style='width:50%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("T6").ToString(), 0) + "</td> <td style='width:50%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("T6").ToString() / dstbl.Tables(k).Rows(0).Item("T8").ToString() * 100, 2) + "</td> </tr>")
                                    str.Append("</table>")
                                    tdInner.Text = str.ToString()
                                Else
                                    tdInner.Text = "na"
                                End If
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)

                            Next


                        Case 10
                            tdInner = New TableCell
                            'Title = "(MJ)"
                            LeftTdSetting(tdInner, "Transport to Customer" + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "M9_" + j.ToString()
                            For k = 0 To DataCnt
                                'M9
                                tdInner = New TableCell
                                str = New StringBuilder()
                                str.Append("<table style='width:100%' border='0' >")
                                If CDbl(dstbl.Tables(k).Rows(0).Item("SVOLUME")) <> 0 Then
                                    str.Append("<tr align='right'> <td style='width:50%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("T7").ToString(), 0) + "</td> <td style='width:50%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("T7").ToString() / dstbl.Tables(k).Rows(0).Item("T8").ToString() * 100, 2) + "</td> </tr>")
                                    str.Append("</table>")
                                    tdInner.Text = str.ToString()
                                Else
                                    tdInner.Text = "na"
                                End If
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)

                            Next


                        Case 11
                            tdInner = New TableCell
                            'Title = "(MJ)"
                            LeftTdSetting(tdInner, "<b>" + "Total Greenhouse Gas" + "</b>", trInner, "AlterNateColor4")
                            trInner.ID = "M10_" + j.ToString()
                            For k = 0 To DataCnt


                                'M10
                                tdInner = New TableCell
                                str = New StringBuilder()
                                str.Append("<table style='width:100%' border='0' >")
                                If CDbl(dstbl.Tables(k).Rows(0).Item("SVOLUME")) <> 0 Then
                                    str.Append("<tr align='right'> <td style='width:50%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("T8").ToString(), 0) + "</td> <td style='width:50%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("T8").ToString() / dstbl.Tables(k).Rows(0).Item("T8").ToString() * 100, 2) + "</td> </tr>")
                                    str.Append("</table>")
                                    tdInner.Text = str.ToString()
                                Else
                                    tdInner.Text = "na"
                                End If
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)
                            Next


                        Case 12
                            tdInner = New TableCell
                            'Title = "(MJ)"
                            LeftTdSetting(tdInner, "Purchased Materials" + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "M11_" + j.ToString()
                            For k = 0 To DataCnt
                                'M11 
                                tdInner = New TableCell
                                str = New StringBuilder()
                                str.Append("<table style='width:100%' border='0' >")
                                If CDbl(dstbl.Tables(k).Rows(0).Item("SVOLUME")) <> 0 Then
                                    str.Append("<tr align='right'> <td style='width:50%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("T9").ToString(), 0) + "</td> <td style='width:50%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("T9").ToString() / dstbl.Tables(k).Rows(0).Item("T8").ToString() * 100, 2) + "</td> </tr>")
                                    str.Append("</table>")
                                    tdInner.Text = str.ToString()
                                Else
                                    tdInner.Text = "na"
                                End If
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)


                            Next



                        Case 13
                            tdInner = New TableCell
                            'Title = "(MJ)"
                            LeftTdSetting(tdInner, "Process" + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "M12_" + j.ToString()
                            For k = 0 To DataCnt
                                'M12
                                tdInner = New TableCell
                                str = New StringBuilder()
                                str.Append("<table style='width:100%' border='0' >")
                                If CDbl(dstbl.Tables(k).Rows(0).Item("SVOLUME")) <> 0 Then
                                    str.Append("<tr align='right'> <td style='width:50%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("T10").ToString(), 0) + "</td> <td style='width:50%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("T10").ToString() / dstbl.Tables(k).Rows(0).Item("T8").ToString() * 100, 2) + "</td> </tr>")
                                    str.Append("</table>")
                                    tdInner.Text = str.ToString()
                                Else
                                    tdInner.Text = "na"
                                End If
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)
                            Next



                        Case 14
                            tdInner = New TableCell
                            'Title = "(MJ)"
                            LeftTdSetting(tdInner, "Transportation" + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "M13_" + j.ToString()
                            For k = 0 To DataCnt
                                'M13
                                tdInner = New TableCell
                                str = New StringBuilder()
                                str.Append("<table style='width:100%' border='0' >")
                                If CDbl(dstbl.Tables(k).Rows(0).Item("SVOLUME")) <> 0 Then
                                    str.Append("<tr align='right'> <td style='width:50%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("T11").ToString(), 0) + "</td> <td style='width:50%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("T11").ToString() / dstbl.Tables(k).Rows(0).Item("T8").ToString() * 100, 2) + "</td> </tr>")
                                    str.Append("</table>")
                                    tdInner.Text = str.ToString()
                                Else
                                    tdInner.Text = "na"
                                End If
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)

                            Next


                        Case 15
                            tdInner = New TableCell
                            'Title = "(MJ)"
                            LeftTdSetting(tdInner, "<b>" + "Total Greenhouse Gas" + "</b>", trInner, "AlterNateColor4")
                            trInner.ID = "M14_" + j.ToString()
                            For k = 0 To DataCnt

                                'M14
                                tdInner = New TableCell
                                str = New StringBuilder()
                                str.Append("<table style='width:100%' border='0' >")
                                If CDbl(dstbl.Tables(k).Rows(0).Item("SVOLUME")) <> 0 Then
                                    str.Append("<tr align='right'> <td style='width:50%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("T12").ToString(), 0) + "</td> <td style='width:50%'>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("T12").ToString() / dstbl.Tables(k).Rows(0).Item("T8").ToString() * 100, 2) + "</td> </tr>")
                                    str.Append("</table>")
                                    tdInner.Text = str.ToString()
                                Else
                                    tdInner.Text = "na"
                                End If
                                InnerTdSetting(tdInner, DWidth, "Right")
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
