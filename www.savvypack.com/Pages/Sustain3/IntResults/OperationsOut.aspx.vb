Imports System.Data
Imports System.Data.OleDb
Imports System
Imports S3GetData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Sustain3_IntResults_OperationsOut
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
        'Dim DataCnt As New Integer
        Dim i As New Integer
        Dim j As New Integer
        Dim k As New Integer
        Dim DWidth As String = String.Empty
        Dim arrCaseID() As String

        Try
            arrCaseID = GetCaseIds()
            'ds = objGetData.OperationsOut(CaseIds, UserName)
            DataCnt = arrCaseID.Length - 1

            Dim trHeader As New TableRow
            Dim tdHeader As TableCell
            Dim tdInner As TableCell
            Dim Cunits As New Integer
            Dim lbl As New Label
            Dim Units As New Integer
            Dim Title As String = String.Empty
            DWidth = txtDWidth.Text + "px"


            tdHeader = New TableCell
            Dim trInner As New TableRow

            HeaderTdSetting(tdHeader, DWidth, "<img alt='' src='../../Images/spacer.gif' Style='width:200px;height:0px;'  />", 1)
            trHeader.Controls.Add(tdHeader)
            trHeader.Height = 20
            trHeader.CssClass = "PageSSHeading"

            For i = 0 To DataCnt
                ds = objGetData.GetOperationsOut(arrCaseID(i))
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
            For k = 1 To 10
                For j = 1 To 8
                    trInner = New TableRow()
                    Select Case j

                        Case 1
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "<b>" + "Layer" + k.ToString() + "</b>", trInner, "AlterNateColor4")

                            For i = 0 To DataCnt
                                'Break
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Center")
                                tdInner.Text = "&nbsp;"
                                trInner.Controls.Add(tdInner)

                            Next

                        Case 2
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Department", trInner, "AlterNateColor1")
                            trInner.ID = "DEP_" + k.ToString()
                            For i = 0 To DataCnt

                                'Department
                                tdInner = New TableCell
                                tdInner.Text = dstbl.Tables(i).Rows(0).Item("PROCDE" + k.ToString() + "").ToString()
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)


                            Next

                        Case 3
                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("Title8").ToString() + ")"
                            LeftTdSetting(tdInner, "Production Volume " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "PV_" + k.ToString()
                            For i = 0 To DataCnt

                                'Production Volume
                                tdInner = New TableCell
                                tdInner.Text = FormatNumber(dstbl.Tables(i).Rows(0).Item("PV" + k.ToString() + "").ToString(), 0)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 4
                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("Title8").ToString() + ")"
                            LeftTdSetting(tdInner, "Finished Equivalent Production Volume " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "FEPV_" + k.ToString()
                            For i = 0 To DataCnt

                                'Finished Equivalent Production Volume
                                tdInner = New TableCell
                                tdInner.Text = FormatNumber(dstbl.Tables(i).Rows(0).Item("FEV" + k.ToString() + "").ToString(), 0)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 5
                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("Title8").ToString() + ")"
                            LeftTdSetting(tdInner, "Final Production Volume " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "FPV_" + k.ToString()
                            For i = 0 To DataCnt

                                'Final Production Volume
                                tdInner = New TableCell
                                tdInner.Text = FormatNumber(dstbl.Tables(i).Rows(0).Item("FPV" + k.ToString() + "").ToString(), 0)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)

                            Next

                        Case 6
                            tdInner = New TableCell
                            Title = "(%)"
                            LeftTdSetting(tdInner, "Average Downtime " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "AD_" + k.ToString()
                            For i = 0 To DataCnt
                                'Average Downtime
                                tdInner = New TableCell
                                tdInner.Text = FormatNumber(dstbl.Tables(i).Rows(0).Item("DW" + k.ToString() + "").ToString(), 1)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)

                            Next

                        Case 7
                            tdInner = New TableCell
                            Title = "(%)"
                            LeftTdSetting(tdInner, "Average Waste " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "AW_" + k.ToString()
                            For i = 0 To DataCnt
                                'Average Waste
                                tdInner = New TableCell
                                tdInner.Text = FormatNumber(dstbl.Tables(i).Rows(0).Item("AW" + k.ToString() + "").ToString(), 1)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)


                            Next
                        Case 8
                            tdInner = New TableCell
                            Title = "(%)"
                            LeftTdSetting(tdInner, "Dept Utilization " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "DU_" + k.ToString()
                            For i = 0 To DataCnt
                                'Average Waste
                                tdInner = New TableCell
                                lbl = New Label()
                                Dim volPer As Double
                                If CDbl(dstbl.Tables(i).Rows(0).Item("PV" + k.ToString() + "").ToString()) > 0.0 Then
                                    volPer = (CDbl(dstbl.Tables(i).Rows(0).Item("FPV" + k.ToString() + "").ToString()) / CDbl(dstbl.Tables(i).Rows(0).Item("PV" + k.ToString() + "").ToString())) * 100
                                    lbl.Text = FormatNumber(volPer.ToString(), 1)
                                Else
                                    lbl.Text = "na"
                                End If
                                InnerTdSetting(tdInner, DWidth, "Right")
                                tdInner.Controls.Add(lbl)
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
