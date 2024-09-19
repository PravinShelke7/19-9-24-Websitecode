Imports System.Data
Imports System.Data.OleDb
Imports System
Imports S4GetData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Sustain4_Assumptions_OperationsIn
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
        Dim dsCaseDetails As New DataSet
        Dim dstbl As New DataSet
        Dim objGetData As New S2GetData.Selectdata
        Dim CaseIds As String = String.Empty
        'Dim DataCnt As New Integer
        Dim i As New Integer
        Dim j As New Integer
        Dim k As New Integer
        Dim m As Integer
        Dim DWidth As String = String.Empty
        Dim arrCaseID() As String


        Try
            arrCaseID = GetCaseIds()
            'ds = objGetData.OpreationsIn(CaseIds, UserName)
            DataCnt = arrCaseID.Length - 1

            Dim trHeader As New TableRow
            Dim tdHeader As TableCell
            Dim trInner As New TableRow
            Dim tdInner As TableCell
            Dim ddl As DropDownList
            Dim txt As TextBox
            Dim Cunits As New Integer
            Dim Units As New Integer
            Dim Title As String = String.Empty
            DWidth = txtDWidth.Text + "px"


            tdHeader = New TableCell
            HeaderTdSetting(tdHeader, DWidth, "<img alt='' src='../../Images/spacer.gif' style='width:200px;height:0px;'  />", 1)
            trHeader.Controls.Add(tdHeader)
            trHeader.Height = 20
            trHeader.CssClass = "PageSSHeading"

            For i = 0 To DataCnt
                ds = objGetData.GetOperationInDetails(arrCaseID(i))
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

            Dim arrwt(DataCnt, 30) As String
            For k = 0 To DataCnt
                For x = 1 To 30
                    For y = 1 To 10
                        If CInt(dstbl.Tables(k).Rows(0).Item("EQDEP" + x.ToString() + "")) > 0 Then
                            If CInt(dstbl.Tables(k).Rows(0).Item("EQDEP" + x.ToString() + "")) = CInt(dstbl.Tables(k).Rows(0).Item("pCONFIG" + y.ToString() + "")) Then
                                'ARRpercent(x) = CInt(ds.Tables(0).Rows(0).Item("PCT" + y.ToString() + ""))
                                arrwt(k, x) = CDbl(dstbl.Tables(k).Rows(0).Item("PCT" + y.ToString() + "") / 100 * dstbl.Tables(k).Rows(0).Item("PRODWT"))
                            End If
                        End If
                    Next
                Next
            Next

            tblComparision.Controls.Add(trHeader)
            For i = 1 To 30
                For j = 1 To 7

                    trInner = New TableRow()

                    Select Case j

                        Case 1

                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "<b>Equipment " + i.ToString() + "</b>", trInner, "AlterNateColor4")
                            For k = 0 To DataCnt
                                'Break
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Center")
                                tdInner.Text = "&nbsp;"
                                trInner.Controls.Add(tdInner)

                            Next


                        Case 2
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Equipment Description", trInner, "AlterNateColor1")
                            trInner.ID = "ED_" + i.ToString()
                            For k = 0 To DataCnt
                                'Equipment Description
                                tdInner = New TableCell
                                If dstbl.Tables(k).Rows(0).Item("LEQUIPDES" + i.ToString() + "").ToString() <> "" Then

                                    tdInner.Text = dstbl.Tables(k).Rows(0).Item("LEQUIPDES" + i.ToString() + "").ToString()
                                Else
                                    tdInner.Text = dstbl.Tables(k).Rows(0).Item("EQDES" + i.ToString() + "").ToString()
                                End If
                                'tdInner.Text = dstbl.Tables(k).Rows(0).Item("EQDES" + i.ToString() + "").ToString()
                                InnerTdSetting(tdInner, DWidth, "Right")
                                'tdInner.Controls.Add(ddl)
                                trInner.Controls.Add(tdInner)

                            Next




                        Case 3

                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Maximum Annual Run Hours", trInner, "AlterNateColor2")
                            trInner.ID = "AR_" + i.ToString()
                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("MAXHOUR" + i.ToString() + "").ToString(), 0)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)
                            Next



                        Case 4

                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("Title8").ToString() + "/hr)"
                            LeftTdSetting(tdInner, "Instantaneous Rate " + Title, trInner, "AlterNateColor1")
                            trInner.ID = "IR1_" + i.ToString()
                            For k = 0 To DataCnt

                                'Instantaneous Rate
                                tdInner = New TableCell
                                'txt = New TextBox
                                'txt.Text = FormatNumber(ds.Tables(0).Rows(i).Item("INSTANTANEOUSRATE" + j.ToString() + "").ToString(), 0)
                                'TextBoxSetting(txt, "NormalTextBox")
                                tdInner.Text = FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("OPINSTGRSRATE" + i.ToString() + "") * dstbl.Tables(k).Rows(0).Item("convwt")), 2)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                'tdInner.Controls.Add(txt)
                                trInner.Controls.Add(tdInner)


                            Next

                        Case 5

                            tdInner = New TableCell
                            Title = "(alternate units)"
                            LeftTdSetting(tdInner, "Instantaneous Rate " + Title, trInner, "AlterNateColor2")
                            trInner.ID = "UN_" + i.ToString()
                            For k = 0 To DataCnt

                                'Units
                                tdInner = New TableCell
                                '  tdInner.Text = dstbl.Tables(k).Rows(0).Item("EQUIPUNITS" + i.ToString() + "").ToString()


                                Dim cal As Double
                                Dim percent As Integer
                                If dstbl.Tables(k).Rows(0).Item("UNITS" + i.ToString() + "").ToString().ToLower() = "cpm" Then
                                    If arrwt(k, i) <> Nothing Then
                                        cal = CDbl(dstbl.Tables(k).Rows(0).Item("OPINSTGRSRATE" + i.ToString() + "") / arrwt(k, i) / 60 / dstbl.Tables(k).Rows(0).Item("EXITS" + i.ToString() + ""))
                                    Else
                                        cal = 0
                                    End If
                                    tdInner.Text = FormatNumber(cal, 2).ToString() + dstbl.Tables(k).Rows(0).Item("UNITS" + i.ToString() + "").ToString()
                                ElseIf dstbl.Tables(k).Rows(0).Item("UNITS" + i.ToString() + "").ToString().ToLower() = "fpm" Then
                                    If CInt(dstbl.Tables(k).Rows(0).Item("EQDEP" + i.ToString() + "")) > 0 Then
                                        percent = 0
                                        For m = 1 To 10
                                            If CInt(dstbl.Tables(k).Rows(0).Item("EQDEP" + i.ToString() + "")) = CInt(dstbl.Tables(k).Rows(0).Item("pCONFIG" + m.ToString() + "")) Then
                                                percent = CInt(dstbl.Tables(k).Rows(0).Item("PCT" + m.ToString() + ""))
                                            End If
                                        Next
                                    End If
                                    If (percent = 0) Then
                                        cal = 0
                                        If CInt(dstbl.Tables(k).Rows(0).Item("UNITS")) = 0 Then
                                            tdInner.Text = FormatNumber(cal, 2).ToString() + dstbl.Tables(k).Rows(0).Item("UNITS" + i.ToString() + "").ToString()
                                        Else
                                            tdInner.Text = FormatNumber(cal, 2).ToString() + dstbl.Tables(k).Rows(0).Item("UNITST" + i.ToString() + "").ToString()
                                        End If
                                    Else
                                        cal = CDbl(dstbl.Tables(k).Rows(0).Item("OPINSTGRSRATE" + i.ToString() + "")) / CDbl(dstbl.Tables(k).Rows(0).Item("wtPERarea") * percent / 100) * 1000 / 60 / CDbl(dstbl.Tables(k).Rows(0).Item("WIDTH" + i.ToString() + "")) / 12 * CDbl(dstbl.Tables(k).Rows(0).Item("convthick2"))
                                        If CInt(dstbl.Tables(k).Rows(0).Item("UNITS")) = 0 Then
                                            tdInner.Text = FormatNumber(cal, 2).ToString() + dstbl.Tables(k).Rows(0).Item("UNITS" + i.ToString() + "").ToString()
                                        Else
                                            tdInner.Text = FormatNumber(cal, 2).ToString() + dstbl.Tables(k).Rows(0).Item("UNITST" + i.ToString() + "").ToString()
                                        End If
                                    End If
                                ElseIf dstbl.Tables(k).Rows(0).Item("UNITS" + i.ToString() + "").ToString().ToLower() = "none" Then
                                    tdInner.Text = "na"
                                End If

                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)


                            Next



                        Case 6

                            tdInner = New TableCell
                            Title = "(%)"
                            LeftTdSetting(tdInner, "Downtime " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "DOW_" + i.ToString()
                            For k = 0 To DataCnt
                                'Downtime
                                tdInner = New TableCell                            
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("DT" + i.ToString() + "").ToString(), 1)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                'tdInner.Controls.Add(txt)
                                trInner.Controls.Add(tdInner)


                            Next

                        Case 7

                            tdInner = New TableCell
                            Title = "(%)"
                            LeftTdSetting(tdInner, "Waste " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "PW_" + i.ToString()
                            For k = 0 To DataCnt
                                'Production Waste
                                tdInner = New TableCell                              
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("OPWASTE" + i.ToString() + "").ToString(), 1)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                'tdInner.Controls.Add(txt)
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
