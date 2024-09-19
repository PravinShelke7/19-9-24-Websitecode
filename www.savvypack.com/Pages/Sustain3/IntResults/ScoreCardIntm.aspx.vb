Imports System.Data
Imports System.Data.OleDb
Imports System
Imports S3GetData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Sustain3_IntResults_ScoreCardIntm
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
        Dim i As New Integer
        Dim j As New Integer
        Dim k As New Integer
        Dim DWidth As String = String.Empty
        Dim arrCaseID() As String

        Try
            arrCaseID = GetCaseIds()
            'ds = objGetData.ScorecardIntm(CaseIds, UserName)
            DataCnt = arrCaseID.Length - 1

            Dim trHeader As New TableRow
            Dim tdHeader As TableCell
            Dim tdInner As TableCell
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
                ds = objGetData.GetScoreCard(arrCaseID(i))
                ds.Tables(0).TableName = arrCaseID(i).ToString()
                dstbl.Tables.Add(ds.Tables(arrCaseID(i).ToString()).Copy())
            Next




            For i = 0 To DataCnt
                dsCaseDetails = objGetData.GetCaseDetails(arrCaseID(i).ToString())
                Cunits = Convert.ToInt32(dstbl.Tables(0).Rows(0).Item("Units").ToString())
                Units = Convert.ToInt32(dstbl.Tables(i).Rows(0).Item("Units").ToString())


                tdHeader = New TableCell
                Dim Headertext As String = String.Empty
                'If Cunits <> Units Then
                '    Headertext = "Case#:" + arrCaseID(i).ToString() + "<br/>" + dsCaseDetails.Tables(0).Rows(0).Item("CaseDes").ToString() + "<br/> <span  style='color:red'>Unit Mismatch</span>" + "<input type='hidden' value='" + arrCaseID(i).ToString() + "' name='Case" + i.ToString() + "'/>"
                'Else
                Headertext = "Case#:" + arrCaseID(i).ToString() + "<br/>" + dsCaseDetails.Tables(0).Rows(0).Item("CaseDes").ToString() + "<input type='hidden' value='" + arrCaseID(i).ToString() + "' name='Case" + i.ToString() + "'/>"
                'End If
                CaseDesp.Add(arrCaseID(i).ToString())
                HeaderTdSetting(tdHeader, DWidth, Headertext, 1)
                trHeader.Controls.Add(tdHeader)


            Next
            tblComparision.Controls.Add(trHeader)
            For i = 1 To 1
                For j = 1 To 15
                    Dim trInner As New TableRow

                    Select Case j
                        Case 1

                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "<b>Greenhouse Gases</b>", trInner, "AlterNateColor4")
                            For k = 0 To DataCnt
                                'Break
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Center")
                                tdInner.Text = "&nbsp;"
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 2
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Shelf Unit Packages" + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "M1_" + i.ToString()
                            For k = 0 To DataCnt
                                'M1
                                tdInner = New TableCell
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("M1").ToString(), 5)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 3

                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "<b>Sustainable Material</b>", trInner, "AlterNateColor4")
                            For k = 0 To DataCnt
                                'Break1
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Center")
                                tdInner.Text = "&nbsp;"
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 4

                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Shelf Unit Packages" + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "M2_" + i.ToString()
                            For k = 0 To DataCnt
                                'M2
                                tdInner = New TableCell
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("M2").ToString(), 5)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 5

                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "<b>Transportation Distance</b>", trInner, "AlterNateColor4")
                            For k = 0 To DataCnt
                                'Break2
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Center")
                                tdInner.Text = "&nbsp;"
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 6

                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Shelf Unit Transport rating" + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "M3_" + i.ToString()
                            For k = 0 To DataCnt
                                'M3
                                tdInner = New TableCell
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("M3").ToString(), 5)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)

                            Next

                        Case 7

                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "<b>Package to Product ratio</b>", trInner, "AlterNateColor4")

                            For k = 0 To DataCnt
                                'Break3
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Center")
                                tdInner.Text = "&nbsp;"
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 8

                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Shelf Unit Packages" + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "M4_" + i.ToString()
                            For k = 0 To DataCnt
                                'M4
                                tdInner = New TableCell
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("M4").ToString(), 5)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 9

                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "<b>Cube Utilization</b>", trInner, "AlterNateColor4")
                            For k = 0 To DataCnt
                                'Break5
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Center")
                                tdInner.Text = "&nbsp;"
                                trInner.Controls.Add(tdInner)

                            Next
                        Case 10

                            tdInner = New TableCell
                            LeftTdSetting(tdInner, " Product Selling Unit Vol. ratio" + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "M5_" + i.ToString()
                            For k = 0 To DataCnt
                                'M5
                                tdInner = New TableCell
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("M5").ToString(), 5)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 11
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Pallet Transport Vol. use ratio" + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "M6_" + i.ToString()
                            For k = 0 To DataCnt
                                'M6
                                tdInner = New TableCell
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("M6").ToString(), 5)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 12

                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "<b>PC Recycled Content</b>", trInner, "AlterNateColor4")
                            For k = 0 To DataCnt
                                'Break6
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Center")
                                tdInner.Text = "&nbsp;"
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 13

                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Shelf Unit Packages" + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "M7_" + i.ToString()
                            For k = 0 To DataCnt
                                'M7
                                tdInner = New TableCell
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("M7").ToString(), 5)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 14


                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "<b>Recovery</b>", trInner, "AlterNateColor4")
                            For k = 0 To DataCnt
                                'Break7
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Center")
                                tdInner.Text = "&nbsp;"
                                trInner.Controls.Add(tdInner)

                            Next

                        Case 15
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Shelf Unit Packages" + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "M8_" + i.ToString()

                            For k = 0 To DataCnt
                                'M8
                                tdInner = New TableCell
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("M8").ToString(), 5)
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
