Imports System.Data
Imports System.Data.OleDb
Imports System
Imports S3GetData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Sustain3_Results_EndOfLifeRes
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
        Dim dstbl As New DataSet
        Dim dsCaseDetails As New DataSet
        Dim objGetData As New S1GetData.Selectdata
        Dim CaseIds As String = String.Empty
        'Dim DataCnt As New Integer
        Dim i As New Integer
        Dim j As New Integer
        Dim k As New Integer
        Dim DWidth As String = String.Empty
        Dim str As StringBuilder

        Dim arrCaseID() As String

        Try
            arrCaseID = GetCaseIds()
            'ds = objGetData.EndOfLifeRes(CaseIds, UserName)
            DataCnt = arrCaseID.Length - 1

            Dim trHeader As New TableRow
            Dim tdHeader As TableCell
            Dim tdInner As TableCell
            Dim trInner = New TableRow()
            Dim Cunits As New Integer
            Dim Units As New Integer
            Dim Title As String = String.Empty
            Dim ColVar As String = String.Empty
            Dim strColor As String = String.Empty
            Dim m As Integer
            DWidth = txtDWidth.Text + "px"


            tdHeader = New TableCell
            HeaderTdSetting(tdHeader, DWidth, "<img alt='' src='../../Images/spacer.gif' Style='width:200px;height:0px;'  />", 1)
            trHeader.Controls.Add(tdHeader)
            trHeader.Height = 20
            trHeader.CssClass = "PageSSHeading"


            For i = 0 To DataCnt
                ds = objGetData.GetEOLResults(arrCaseID(i))
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
            ' Setting Sales Volume
            For i = 0 To 1
                trInner = New TableRow()
                tdInner = New TableCell
                trInner.ID = "SVOL" + (i + 1).ToString() + "_1"
                If (i Mod 2 = 0) Then
                    strColor = "AlterNateColor1"
                Else
                    strColor = "AlterNateColor2"
                End If
                If i = 0 Then
                    Title = "<b>Sales Volume (" + dstbl.Tables(0).Rows(0).Item("TITLE8").ToString() + ")</b> "
                Else
                    Title = "<b>Sales Volume (" + dstbl.Tables(0).Rows(0).Item("SUNIT").ToString() + ")</b> "
                End If
                LeftTdSetting(tdInner, Title, trInner, strColor)
                For k = 0 To DataCnt
                    tdInner = New TableCell
                    Dim strVolume As String = ""
                    If i = 0 Then
                        If dstbl.Tables(k).Rows(0).Item("SVOLUME").ToString() <> "" Then
                            strVolume = FormatNumber(dstbl.Tables(k).Rows(0).Item("SVOLUME").ToString(), 0).ToString()
                        Else
                            strVolume = "0"
                        End If
                    Else
                        If dstbl.Tables(k).Rows(0).Item("SUNITVAL").ToString() <> "" Then
                            strVolume = FormatNumber(dstbl.Tables(k).Rows(0).Item("SUNITVAL").ToString(), 0).ToString()
                        Else
                            strVolume = "0"
                        End If

                    End If


                    tdInner.Text = strVolume
                    InnerTdSetting(tdInner, "", "Right")
                    tdInner.Style.Add("padding-right", "15px")
                    trInner.Controls.Add(tdInner)
                Next
                tblComparision.Controls.Add(trInner)
            Next

            For i = 1 To 5
                For j = 1 To 14
                    trInner = New TableRow()
                    Dim strName As String = String.Empty
                    Dim strVal As String = String.Empty
                    Select Case j
                        Case 1
                            tdInner = New TableCell
                            If i = 1 Then
                                strName = "Material Balance"
                            ElseIf i = 2 Then
                                strName = "Material to Recycling"
                            ElseIf i = 3 Then
                                strName = "Material to Incineration"
                            ElseIf i = 4 Then
                                strName = "Material to Composting"
                            ElseIf i = 5 Then
                                strName = "Material to Landfill"
                            End If
                            LeftTdSetting(tdInner, "<b>" + strName + "</b>", trInner, "AlterNateColor4")
                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Center")
                                tdInner.Text = "(" + dstbl.Tables(k).Rows(0).Item("TITLE8") + ")"
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14
                            tdInner = New TableCell
                            If j = 2 Or j = 6 Or j = 9 Or j = 12 Then
                                LeftTdSetting(tdInner, "<span style='font-weight:normal;'><b>" + dstbl.Tables(0).Rows(0).Item("PLANTSPACE" + (j - 1).ToString() + "").ToString() + "</b></span>", trInner, "")
                            Else
                                LeftTdSetting(tdInner, "<span style='margin-left:15px;'>" + dstbl.Tables(0).Rows(0).Item("PLANTSPACE" + (j - 1).ToString() + "").ToString() + "</span>", trInner, "")

                            End If
                            If (j Mod 2 = 0) Then
                                trInner.CssClass = "AlterNateColor1"
                            Else
                                trInner.CssClass = "AlterNateColor2"
                            End If

                            trInner.ID = "M" + (j - 1).ToString() + "_" + i.ToString()
                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                If i = 1 Then
                                    strVal = "MATB"
                                ElseIf i = 2 Then
                                    strVal = "MATRE"
                                ElseIf i = 3 Then
                                    strVal = "MATIN"
                                ElseIf i = 4 Then
                                    strVal = "MATCM"
                                Else
                                    strVal = "MATLF"
                                End If

                                If j > 2 And j < 6 Then
                                    If i <> 5 Then
                                        tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item(strVal + (j - 1).ToString() + "").ToString(), 0)
                                    Else
                                        If j <> 3 Then
                                            tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item(strVal + (j - 1).ToString() + "").ToString(), 0)
                                        Else
                                            tdInner.Text = "0"
                                        End If
                                    End If

                                ElseIf j > 6 And j < 9 Then
                                    tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item(strVal + (j - 1).ToString() + "").ToString(), 0)
                                ElseIf j > 9 And j < 12 Then
                                    tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item(strVal + (j - 1).ToString() + "").ToString(), 0)
                                ElseIf j > 12 Then
                                    tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item(strVal + (j - 1).ToString() + "").ToString(), 0)
                                Else
                                    tdInner.Text = "0"
                                End If
                                InnerTdSetting(tdInner, DWidth, "Right")
                                tdInner.Style.Add("padding-right", "15px")
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
