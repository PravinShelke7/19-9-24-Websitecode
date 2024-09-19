Imports System.Data
Imports System.Data.OleDb
Imports System
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_SDistribution_Results_CEndOfLife
    Inherits System.Web.UI.Page
#Region "Get Set Variables"

    Dim _iCaseId1 As String
    Dim _iCaseId2 As String
    Dim _iUserId As Integer
    Dim _strUserRole As String
    Dim _strType As String

    Public Property CaseId1() As String
        Get
            Return _iCaseId1
        End Get
        Set(ByVal Value As String)
            _iCaseId1 = Value
        End Set
    End Property

    Public Property CaseId2() As String
        Get
            Return _iCaseId2
        End Get
        Set(ByVal Value As String)
            _iCaseId2 = Value
        End Set
    End Property

    Public Property UserId() As Integer
        Get
            Return _iUserId
        End Get
        Set(ByVal Value As Integer)
            _iUserId = Value
        End Set
    End Property

    Public Property UserRole() As String
        Get
            Return _strUserRole
        End Get
        Set(ByVal Value As String)
            _strUserRole = Value
        End Set
    End Property

    Public Property Type() As String
        Get
            Return _strType
        End Get
        Set(ByVal Value As String)
            _strType = Value
        End Set
    End Property

#End Region


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("Back") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If
            GetSessionDetails()
            If Not IsPostBack Then
                Type = Convert.ToString(Request.QueryString("Type")).Replace("!Plus!", "+").Replace("!Hash!", "#").Replace("!And!", "&")
                CaseId2 = Convert.ToString(Request.QueryString("CaseId")).Replace("!Plus!", "+").Replace("!Hash!", "#").Replace("!And!", "&")
                GetPageDetails()
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetSessionDetails()
        Try
            UserId = Session("UserId")
            CaseId1 = Session("SDistCaseId")
            UserRole = Session("SDistUserRole")
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetSessionDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetPageDetails()
        Dim ds1 As New DataSet
        Dim ds2 As New DataSet
        Dim CaseIds As String = String.Empty
        Dim objGetData As New SDistGetData.Selectdata
        Dim obj As New CryptoHelper()
        Try
            Type = obj.Decrypt(Type)
            CaseId2 = obj.Decrypt(CaseId2)
            ds1 = objGetData.GetEOLResults(CaseId1)
            ds2 = objGetData.GetEOLResults(CaseId2)



            If Type = "MatBalance" Then
                GetMaterialBalance(ds1, ds2)
            ElseIf Type = "MatRec" Then
                GetMaterialRecycling(ds1, ds2)
            ElseIf Type = "MatInc" Then
                GetMaterialIncineration(ds1, ds2)
            ElseIf Type = "MatComp" Then
                GetMaterialComposting(ds1, ds2)
            ElseIf Type = "MatLandFill" Then
                GetMaterialLandfill(ds1, ds2)
            End If



        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetMaterialLandfill(ByVal ds1 As DataSet, ByVal ds2 As DataSet)
        Dim i As New Integer
        Dim j As New Integer
        Dim DWidth As String = String.Empty
        Dim trHeader As New TableRow
        Dim trHeader1 As New TableRow
        Dim trHeader2 As New TableRow
        Dim trHeader3 As New TableRow
        Dim trHeader4 As New TableRow

        Dim trInner As New TableRow

        Dim tdHeader As TableCell
        Dim tdHeader1 As TableCell
        Dim tdHeader2 As TableCell
        Dim tdHeader3 As TableCell
        Dim tdHeader4 As TableCell


        Dim lbl As New Label
        Dim hid As New HiddenField
        Dim Link As New HyperLink
        Dim txt As New TextBox
        Dim tdInner As TableCell
        Dim customerSaleValue1 As Double
        Dim customerSaleValue2 As Double

        Try
            customerSaleValue1 = FormatNumber(CDbl(((CDbl(ds1.Tables(0).Rows(0).Item("CUSSALESVOLUME").ToString()) * CDbl(ds1.Tables(0).Rows(0).Item("Convwt").ToString())))), 0)
            customerSaleValue2 = FormatNumber(CDbl(((CDbl(ds1.Tables(0).Rows(0).Item("CUSSALESVOLUME").ToString()) * CDbl(ds2.Tables(0).Rows(0).Item("Convwt").ToString())))), 0)

            For i = 1 To 5
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                tdHeader3 = New TableCell
                tdHeader4 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "200px", "", "1")
                        HeaderTdSetting(tdHeader1, "", "Case Id:", "1")
                        HeaderTdSetting(tdHeader2, "", "Package Format:", "1")
                        HeaderTdSetting(tdHeader3, "", "Unique Feature:", "1")
                        HeaderTdSetting(tdHeader4, "", "Units:", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader3.Controls.Add(tdHeader3)
                        trHeader4.Controls.Add(tdHeader4)

                        tdHeader1.HorizontalAlign = HorizontalAlign.Right
                        tdHeader2.HorizontalAlign = HorizontalAlign.Right
                        tdHeader3.HorizontalAlign = HorizontalAlign.Right
                        tdHeader4.HorizontalAlign = HorizontalAlign.Right
                    Case 2
                        Title = "(" + ds1.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                        HeaderTdSetting(tdHeader, "150px", "Material to Landfill", "1")
                        Header2TdSetting(tdHeader1, "", "" + ds1.Tables(0).Rows(0).Item("CaseId").ToString() + "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        Header2TdSetting(tdHeader3, "", "", "1")
                        GetCaseDetails(tdHeader2, tdHeader3, ds1.Tables(0).Rows(0).Item("CaseId").ToString())
                        Header2TdSetting(tdHeader4, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader3.Controls.Add(tdHeader3)
                        trHeader4.Controls.Add(tdHeader4)
                    Case 3
                        Title = "(" + ds2.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                        HeaderTdSetting(tdHeader, "150px", "Material to Landfill", "1")
                        Header2TdSetting(tdHeader1, "", "" + ds2.Tables(0).Rows(0).Item("CaseId").ToString() + "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        Header2TdSetting(tdHeader3, "", "", "1")
                        GetCaseDetails(tdHeader2, tdHeader3, ds2.Tables(0).Rows(0).Item("CaseId").ToString())
                        Header2TdSetting(tdHeader4, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader3.Controls.Add(tdHeader3)
                        trHeader4.Controls.Add(tdHeader4)
                    Case 4
                        HeaderTdSetting(tdHeader, "150px", "Improvement", "1")
                        Header2TdSetting(tdHeader1, "", ds1.Tables(0).Rows(0).Item("CaseId").ToString(), "1")
                        Header2TdSetting(tdHeader2, "", "Compared To", "1")
                        Header2TdSetting(tdHeader3, "", ds2.Tables(0).Rows(0).Item("CaseId").ToString(), "1")
                        Header2TdSetting(tdHeader4, "", "(%)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader3.Controls.Add(tdHeader3)
                        trHeader4.Controls.Add(tdHeader4)
                    Case 5
                        HeaderTdSetting(tdHeader, "150px", "Ratio", "1")
                        Header2TdSetting(tdHeader1, "", "(" + ds2.Tables(0).Rows(0).Item("CaseId").ToString() + "/" + ds1.Tables(0).Rows(0).Item("CaseId").ToString() + ")", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        Header2TdSetting(tdHeader3, "", "", "1")
                        Header2TdSetting(tdHeader4, "", "(%)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader3.Controls.Add(tdHeader3)
                        trHeader4.Controls.Add(tdHeader4)
                End Select





            Next
            tblComparision.Controls.Add(trHeader)
            tblComparision.Controls.Add(trHeader1)
            tblComparision.Controls.Add(trHeader2)
            tblComparision.Controls.Add(trHeader3)
            tblComparision.Controls.Add(trHeader4)


            'Inner
            Dim count1, count2 As Integer
            count1 = 0
            count2 = 0
            For i = 1 To 13
                trInner = New TableRow
                For j = 1 To 5
                    tdInner = New TableCell
                    Select Case j
                        Case 1
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            If i = 1 Or i = 5 Or i = 8 Or i = 11 Or i = 14 Or i = 22 Then
                                lbl.Text = "<b>" + ds1.Tables(0).Rows(0).Item("PLANTSPACE" + i.ToString() + "").ToString() + "</b>"
                            Else
                                lbl.Text = "<b style='margin-left:20px;'>" + ds1.Tables(0).Rows(0).Item("PLANTSPACE" + i.ToString() + "").ToString() + "</b>"
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 2
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            If i = 1 Or i = 5 Or i = 8 Or i = 11 Or i = 14 Or i = 22 Then
                                lbl.Text = ""
                            Else
                                count1 = count1 + 1
                                If CDbl(ds1.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()) > 0.0 Then
                                    If i = 2 Or i = 6 Or i = 9 Then
                                        lbl.Text = "0"
                                    Else
                                        lbl.Text = FormatNumber((CDbl(ds1.Tables(0).Rows(0).Item("MATLF" + count1.ToString() + "").ToString()) * customerSaleValue1) / CDbl(ds1.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()), 0)
                                    End If
                                Else
                                    lbl.Text = "na"
                                End If
                            End If

                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 3
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            If i = 1 Or i = 5 Or i = 8 Or i = 11 Or i = 14 Or i = 22 Then
                                lbl.Text = ""
                            Else
                                count2 = count2 + 1
                                If CDbl(ds2.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()) > 0.0 Then
                                    If i = 2 Or i = 6 Or i = 9 Then
                                        lbl.Text = "0"
                                    Else
                                        lbl.Text = FormatNumber((CDbl(ds2.Tables(0).Rows(0).Item("MATLF" + count2.ToString() + "").ToString()) * customerSaleValue2) / CDbl(ds2.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()), 0)
                                    End If
                                Else
                                    lbl.Text = "na"
                                End If

                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 4
                            InnerTdSetting(tdInner, "", "Right")
                            Dim Comp As String = String.Empty
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            If CDbl(ds1.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()) > 0.0 And CDbl(ds2.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()) > 0.0 Then
                                Dim cTotal1, cTotal2 As Double
                                If i = 1 Or i = 5 Or i = 8 Or i = 11 Or i = 14 Or i = 22 Then
                                    Comp = ""
                                ElseIf i = 2 Or i = 6 Or i = 9 Then
                                    Comp = "na"
                                Else
                                    cTotal1 = FormatNumber((CDbl(ds1.Tables(0).Rows(0).Item("MATLF" + count1.ToString() + "").ToString()) * customerSaleValue1) / CDbl(ds1.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()), 0)
                                    cTotal2 = FormatNumber((CDbl(ds2.Tables(0).Rows(0).Item("MATLF" + count1.ToString() + "").ToString()) * customerSaleValue2) / CDbl(ds2.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()), 0)
                                    If CDbl(cTotal1) > CDbl(0) Then
                                        Comp = FormatNumber(((cTotal1 - cTotal2) / cTotal1) * 100, 1)
                                    Else
                                        Comp = "na"
                                    End If
                                End If
                                lbl.Text = Comp
                            Else
                                If i = 1 Or i = 5 Or i = 8 Or i = 11 Or i = 14 Or i = 22 Then
                                    lbl.Text = ""
                                ElseIf i = 2 Or i = 6 Or i = 9 Then
                                    lbl.Text = "na"
                                Else
                                    lbl.Text = "na"
                                End If

                            End If


                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 5
                            InnerTdSetting(tdInner, "", "Right")
                            Dim Comp As String = String.Empty
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            If CDbl(ds1.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()) > 0.0 And CDbl(ds2.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()) > 0.0 Then
                                Dim cTotal1, cTotal2 As Double
                                If i = 1 Or i = 5 Or i = 8 Or i = 11 Or i = 14 Or i = 22 Then
                                    Comp = ""
                                ElseIf i = 2 Or i = 6 Or i = 9 Then
                                    Comp = "na"
                                Else
                                    cTotal1 = FormatNumber((CDbl(ds1.Tables(0).Rows(0).Item("MATLF" + count1.ToString() + "").ToString()) * customerSaleValue1) / CDbl(ds1.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()), 0)
                                    cTotal2 = FormatNumber((CDbl(ds2.Tables(0).Rows(0).Item("MATLF" + count1.ToString() + "").ToString()) * customerSaleValue2) / CDbl(ds2.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()), 0)
                                    If CDbl(cTotal1) > CDbl(0) Then
                                        Comp = FormatNumber((cTotal2 / cTotal1) * 100, 1)
                                    Else
                                        Comp = "na"
                                    End If
                                End If
                                lbl.Text = Comp
                            Else
                                If i = 1 Or i = 5 Or i = 8 Or i = 11 Or i = 14 Or i = 22 Then
                                    lbl.Text = ""
                                ElseIf i = 2 Or i = 6 Or i = 9 Then
                                    lbl.Text = "na"
                                Else
                                    lbl.Text = "na"
                                End If

                            End If


                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                    End Select
                Next
                trInner.Height = 20
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
                'If i = 12 Or i = 16 Then
                '    trInner.CssClass = "TdHeading"
                'End If
                tblComparision.Controls.Add(trInner)
            Next



        Catch ex As Exception
            _lErrorLble.Text = "Error:GetMaterialLandfill:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub GetMaterialComposting(ByVal ds1 As DataSet, ByVal ds2 As DataSet)
        Dim i As New Integer
        Dim j As New Integer
        Dim DWidth As String = String.Empty
        Dim trHeader As New TableRow
        Dim trHeader1 As New TableRow
        Dim trHeader2 As New TableRow
        Dim trHeader3 As New TableRow
        Dim trHeader4 As New TableRow

        Dim trInner As New TableRow

        Dim tdHeader As TableCell
        Dim tdHeader1 As TableCell
        Dim tdHeader2 As TableCell
        Dim tdHeader3 As TableCell
        Dim tdHeader4 As TableCell


        Dim lbl As New Label
        Dim hid As New HiddenField
        Dim Link As New HyperLink
        Dim txt As New TextBox
        Dim tdInner As TableCell
        Dim customerSaleValue1 As Double
        Dim customerSaleValue2 As Double

        Try
            customerSaleValue1 = FormatNumber(CDbl(((CDbl(ds1.Tables(0).Rows(0).Item("CUSSALESVOLUME").ToString()) * CDbl(ds1.Tables(0).Rows(0).Item("Convwt").ToString())))), 0)
            customerSaleValue2 = FormatNumber(CDbl(((CDbl(ds1.Tables(0).Rows(0).Item("CUSSALESVOLUME").ToString()) * CDbl(ds2.Tables(0).Rows(0).Item("Convwt").ToString())))), 0)

            For i = 1 To 5
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                tdHeader3 = New TableCell
                tdHeader4 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "200px", "", "1")
                        HeaderTdSetting(tdHeader1, "", "Case Id:", "1")
                        HeaderTdSetting(tdHeader2, "", "Package Format:", "1")
                        HeaderTdSetting(tdHeader3, "", "Unique Feature:", "1")
                        HeaderTdSetting(tdHeader4, "", "Units:", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader3.Controls.Add(tdHeader3)
                        trHeader4.Controls.Add(tdHeader4)

                        tdHeader1.HorizontalAlign = HorizontalAlign.Right
                        tdHeader2.HorizontalAlign = HorizontalAlign.Right
                        tdHeader3.HorizontalAlign = HorizontalAlign.Right
                        tdHeader4.HorizontalAlign = HorizontalAlign.Right
                    Case 2
                        Title = "(" + ds1.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                        HeaderTdSetting(tdHeader, "150px", "Material to Composting", "1")
                        Header2TdSetting(tdHeader1, "", "" + ds1.Tables(0).Rows(0).Item("CaseId").ToString() + "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        Header2TdSetting(tdHeader3, "", "", "1")
                        GetCaseDetails(tdHeader2, tdHeader3, ds1.Tables(0).Rows(0).Item("CaseId").ToString())
                        Header2TdSetting(tdHeader4, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader3.Controls.Add(tdHeader3)
                        trHeader4.Controls.Add(tdHeader4)
                    Case 3
                        Title = "(" + ds2.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                        HeaderTdSetting(tdHeader, "150px", "Material to Composting", "1")
                        Header2TdSetting(tdHeader1, "", "" + ds2.Tables(0).Rows(0).Item("CaseId").ToString() + "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        Header2TdSetting(tdHeader3, "", "", "1")
                        GetCaseDetails(tdHeader2, tdHeader3, ds2.Tables(0).Rows(0).Item("CaseId").ToString())
                        Header2TdSetting(tdHeader4, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader3.Controls.Add(tdHeader3)
                        trHeader4.Controls.Add(tdHeader4)
                    Case 4
                        HeaderTdSetting(tdHeader, "150px", "Improvement", "1")
                        Header2TdSetting(tdHeader1, "", ds1.Tables(0).Rows(0).Item("CaseId").ToString(), "1")
                        Header2TdSetting(tdHeader2, "", "Compared To", "1")
                        Header2TdSetting(tdHeader3, "", ds2.Tables(0).Rows(0).Item("CaseId").ToString(), "1")
                        Header2TdSetting(tdHeader4, "", "(%)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader3.Controls.Add(tdHeader3)
                        trHeader4.Controls.Add(tdHeader4)
                    Case 5
                        HeaderTdSetting(tdHeader, "150px", "Ratio", "1")
                        Header2TdSetting(tdHeader1, "", "(" + ds2.Tables(0).Rows(0).Item("CaseId").ToString() + "/" + ds1.Tables(0).Rows(0).Item("CaseId").ToString() + ")", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        Header2TdSetting(tdHeader3, "", "", "1")
                        Header2TdSetting(tdHeader4, "", "(%)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader3.Controls.Add(tdHeader3)
                        trHeader4.Controls.Add(tdHeader4)
                End Select





            Next
            tblComparision.Controls.Add(trHeader)
            tblComparision.Controls.Add(trHeader1)
            tblComparision.Controls.Add(trHeader2)
            tblComparision.Controls.Add(trHeader3)
            tblComparision.Controls.Add(trHeader4)


            'Inner
            Dim count1, count2 As Integer
            count1 = 0
            count2 = 0
            For i = 1 To 13
                trInner = New TableRow
                For j = 1 To 5
                    tdInner = New TableCell
                    Select Case j
                        Case 1
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            If i = 1 Or i = 5 Or i = 8 Or i = 11 Or i = 14 Or i = 22 Then
                                lbl.Text = "<b>" + ds1.Tables(0).Rows(0).Item("PLANTSPACE" + i.ToString() + "").ToString() + "</b>"
                            Else
                                lbl.Text = "<b style='margin-left:20px;'>" + ds1.Tables(0).Rows(0).Item("PLANTSPACE" + i.ToString() + "").ToString() + "</b>"
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 2
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            If i = 1 Or i = 5 Or i = 8 Or i = 11 Or i = 14 Or i = 22 Then
                                lbl.Text = ""
                            Else
                                count1 = count1 + 1
                                If CDbl(ds1.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()) > 0.0 Then
                                    If i = 2 Or i = 6 Or i = 9 Then
                                        lbl.Text = "0"
                                    Else
                                        lbl.Text = FormatNumber((CDbl(ds1.Tables(0).Rows(0).Item("MATCM" + count1.ToString() + "").ToString()) * customerSaleValue1) / CDbl(ds1.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()), 0)
                                    End If
                                Else
                                    lbl.Text = "na"
                                End If
                            End If

                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 3
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            If i = 1 Or i = 5 Or i = 8 Or i = 11 Or i = 14 Or i = 22 Then
                                lbl.Text = ""
                            Else
                                count2 = count2 + 1
                                If CDbl(ds2.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()) > 0.0 Then
                                    If i = 2 Or i = 6 Or i = 9 Then
                                        lbl.Text = "0"
                                    Else
                                        lbl.Text = FormatNumber((CDbl(ds2.Tables(0).Rows(0).Item("MATCM" + count2.ToString() + "").ToString()) * customerSaleValue2) / CDbl(ds2.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()), 0)
                                    End If
                                Else
                                    lbl.Text = "na"
                                End If

                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)

                        Case 4
                            InnerTdSetting(tdInner, "", "Right")
                            Dim Comp As String = String.Empty
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            If CDbl(ds1.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()) > 0.0 And CDbl(ds2.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()) > 0.0 Then
                                Dim cTotal1, cTotal2 As Double
                                If i = 1 Or i = 5 Or i = 8 Or i = 11 Or i = 14 Or i = 22 Then
                                    Comp = ""
                                ElseIf i = 2 Or i = 6 Or i = 9 Then
                                    Comp = "na"
                                Else
                                    cTotal1 = FormatNumber((CDbl(ds1.Tables(0).Rows(0).Item("MATCM" + count1.ToString() + "").ToString()) * customerSaleValue1) / CDbl(ds1.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()), 0)
                                    cTotal2 = FormatNumber((CDbl(ds2.Tables(0).Rows(0).Item("MATCM" + count1.ToString() + "").ToString()) * customerSaleValue2) / CDbl(ds2.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()), 0)
                                    If CDbl(cTotal1) > CDbl(0) Then
                                        Comp = FormatNumber(((cTotal1 - cTotal2) / cTotal1) * 100, 1)
                                    Else
                                        Comp = "na"
                                    End If
                                End If
                                lbl.Text = Comp
                            Else
                                If i = 1 Or i = 5 Or i = 8 Or i = 11 Or i = 14 Or i = 22 Then
                                    lbl.Text = ""
                                ElseIf i = 2 Or i = 6 Or i = 9 Then
                                    lbl.Text = "na"
                                Else
                                    lbl.Text = "na"
                                End If

                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 5
                            InnerTdSetting(tdInner, "", "Right")
                            Dim Comp As String = String.Empty
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            If CDbl(ds1.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()) > 0.0 And CDbl(ds2.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()) > 0.0 Then
                                Dim cTotal1, cTotal2 As Double
                                If i = 1 Or i = 5 Or i = 8 Or i = 11 Or i = 14 Or i = 22 Then
                                    Comp = ""
                                ElseIf i = 2 Or i = 6 Or i = 9 Then
                                    Comp = "na"
                                Else
                                    cTotal1 = FormatNumber((CDbl(ds1.Tables(0).Rows(0).Item("MATCM" + count1.ToString() + "").ToString()) * customerSaleValue1) / CDbl(ds1.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()), 0)
                                    cTotal2 = FormatNumber((CDbl(ds2.Tables(0).Rows(0).Item("MATCM" + count1.ToString() + "").ToString()) * customerSaleValue2) / CDbl(ds2.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()), 0)
                                    If CDbl(cTotal1) > CDbl(0) Then
                                        Comp = FormatNumber((cTotal2 / cTotal1) * 100, 1)
                                    Else
                                        Comp = "na"
                                    End If
                                End If
                                lbl.Text = Comp
                            Else
                                If i = 1 Or i = 5 Or i = 8 Or i = 11 Or i = 14 Or i = 22 Then
                                    lbl.Text = ""
                                ElseIf i = 2 Or i = 6 Or i = 9 Then
                                    lbl.Text = "na"
                                Else
                                    lbl.Text = "na"
                                End If
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                    End Select
                Next
                trInner.Height = 20
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
                'If i = 12 Or i = 16 Then
                '    trInner.CssClass = "TdHeading"
                'End If
                tblComparision.Controls.Add(trInner)
            Next



        Catch ex As Exception
            _lErrorLble.Text = "Error:GetMaterialComposting:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetMaterialIncineration(ByVal ds1 As DataSet, ByVal ds2 As DataSet)
        Dim i As New Integer
        Dim j As New Integer
        Dim DWidth As String = String.Empty
        Dim trHeader As New TableRow
        Dim trHeader1 As New TableRow
        Dim trHeader2 As New TableRow
        Dim trHeader3 As New TableRow
        Dim trHeader4 As New TableRow

        Dim trInner As New TableRow

        Dim tdHeader As TableCell
        Dim tdHeader1 As TableCell
        Dim tdHeader2 As TableCell
        Dim tdHeader3 As TableCell
        Dim tdHeader4 As TableCell


        Dim lbl As New Label
        Dim hid As New HiddenField
        Dim Link As New HyperLink
        Dim txt As New TextBox
        Dim tdInner As TableCell
        Dim customerSaleValue1 As Double
        Dim customerSaleValue2 As Double

        Try
            customerSaleValue1 = FormatNumber(CDbl(((CDbl(ds1.Tables(0).Rows(0).Item("CUSSALESVOLUME").ToString()) * CDbl(ds1.Tables(0).Rows(0).Item("Convwt").ToString())))), 0)
            customerSaleValue2 = FormatNumber(CDbl(((CDbl(ds1.Tables(0).Rows(0).Item("CUSSALESVOLUME").ToString()) * CDbl(ds2.Tables(0).Rows(0).Item("Convwt").ToString())))), 0)

            For i = 1 To 5
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                tdHeader3 = New TableCell
                tdHeader4 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "200px", "", "1")
                        HeaderTdSetting(tdHeader1, "", "Case Id:", "1")
                        HeaderTdSetting(tdHeader2, "", "Package Format:", "1")
                        HeaderTdSetting(tdHeader3, "", "Unique Feature:", "1")
                        HeaderTdSetting(tdHeader4, "", "Units:", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader3.Controls.Add(tdHeader3)
                        trHeader4.Controls.Add(tdHeader4)

                        tdHeader1.HorizontalAlign = HorizontalAlign.Right
                        tdHeader2.HorizontalAlign = HorizontalAlign.Right
                        tdHeader3.HorizontalAlign = HorizontalAlign.Right
                        tdHeader4.HorizontalAlign = HorizontalAlign.Right
                    Case 2
                        Title = "(" + ds1.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                        HeaderTdSetting(tdHeader, "150px", "Material to Incineration", "1")
                        Header2TdSetting(tdHeader1, "", "" + ds1.Tables(0).Rows(0).Item("CaseId").ToString() + "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        Header2TdSetting(tdHeader3, "", "", "1")
                        GetCaseDetails(tdHeader2, tdHeader3, ds1.Tables(0).Rows(0).Item("CaseId").ToString())
                        Header2TdSetting(tdHeader4, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader3.Controls.Add(tdHeader3)
                        trHeader4.Controls.Add(tdHeader4)
                    Case 3
                        Title = "(" + ds2.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                        HeaderTdSetting(tdHeader, "150px", "Material to Incineration", "1")
                        Header2TdSetting(tdHeader1, "", "" + ds2.Tables(0).Rows(0).Item("CaseId").ToString() + "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        Header2TdSetting(tdHeader3, "", "", "1")
                        GetCaseDetails(tdHeader2, tdHeader3, ds2.Tables(0).Rows(0).Item("CaseId").ToString())
                        Header2TdSetting(tdHeader4, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader3.Controls.Add(tdHeader3)
                        trHeader4.Controls.Add(tdHeader4)
                    Case 4
                        HeaderTdSetting(tdHeader, "150px", "Improvement", "1")
                        Header2TdSetting(tdHeader1, "", ds1.Tables(0).Rows(0).Item("CaseId").ToString(), "1")
                        Header2TdSetting(tdHeader2, "", "Compared To", "1")
                        Header2TdSetting(tdHeader3, "", ds2.Tables(0).Rows(0).Item("CaseId").ToString(), "1")
                        Header2TdSetting(tdHeader4, "", "(%)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader3.Controls.Add(tdHeader3)
                        trHeader4.Controls.Add(tdHeader4)
                    Case 5
                        HeaderTdSetting(tdHeader, "150px", "Ratio", "1")
                        Header2TdSetting(tdHeader1, "", "(" + ds2.Tables(0).Rows(0).Item("CaseId").ToString() + "/" + ds1.Tables(0).Rows(0).Item("CaseId").ToString() + ")", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        Header2TdSetting(tdHeader3, "", "", "1")
                        Header2TdSetting(tdHeader4, "", "(%)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader3.Controls.Add(tdHeader3)
                        trHeader4.Controls.Add(tdHeader4)
                End Select





            Next
            tblComparision.Controls.Add(trHeader)
            tblComparision.Controls.Add(trHeader1)
            tblComparision.Controls.Add(trHeader2)
            tblComparision.Controls.Add(trHeader3)
            tblComparision.Controls.Add(trHeader4)


            'Inner
            Dim count1, count2 As Integer
            count1 = 0
            count2 = 0
            For i = 1 To 13
                trInner = New TableRow
                For j = 1 To 5
                    tdInner = New TableCell
                    Select Case j
                        Case 1
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            If i = 1 Or i = 5 Or i = 8 Or i = 11 Or i = 14 Or i = 22 Then
                                lbl.Text = "<b>" + ds1.Tables(0).Rows(0).Item("PLANTSPACE" + i.ToString() + "").ToString() + "</b>"
                            Else
                                lbl.Text = "<b style='margin-left:20px;'>" + ds1.Tables(0).Rows(0).Item("PLANTSPACE" + i.ToString() + "").ToString() + "</b>"
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 2
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            If i = 1 Or i = 5 Or i = 8 Or i = 11 Or i = 14 Or i = 22 Then
                                lbl.Text = ""
                            Else
                                count1 = count1 + 1
                                If CDbl(ds1.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()) > 0.0 Then
                                    If i = 2 Or i = 6 Or i = 9 Then
                                        lbl.Text = "0"
                                    Else
                                        lbl.Text = FormatNumber((CDbl(ds1.Tables(0).Rows(0).Item("MATIN" + count1.ToString() + "").ToString()) * customerSaleValue1) / CDbl(ds1.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()), 0)
                                    End If
                                Else
                                    lbl.Text = "na"
                                End If
                            End If

                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 3
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            If i = 1 Or i = 5 Or i = 8 Or i = 11 Or i = 14 Or i = 22 Then
                                lbl.Text = ""
                            Else
                                count2 = count2 + 1
                                If CDbl(ds2.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()) > 0.0 Then
                                    If i = 2 Or i = 6 Or i = 9 Then
                                        lbl.Text = "0"
                                    Else
                                        lbl.Text = FormatNumber((CDbl(ds2.Tables(0).Rows(0).Item("MATIN" + count2.ToString() + "").ToString()) * customerSaleValue2) / CDbl(ds2.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()), 0)
                                    End If
                                Else
                                    lbl.Text = "na"
                                End If

                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)

                        Case 4
                            InnerTdSetting(tdInner, "", "Right")
                            Dim Comp As String = String.Empty
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            If CDbl(ds1.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()) > 0.0 And CDbl(ds2.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()) > 0.0 Then
                                Dim cTotal1, cTotal2 As Double
                                If i = 1 Or i = 5 Or i = 8 Or i = 11 Or i = 14 Or i = 22 Then
                                    Comp = ""
                                ElseIf i = 2 Or i = 6 Or i = 9 Then
                                    Comp = "na"
                                Else
                                    cTotal1 = FormatNumber((CDbl(ds1.Tables(0).Rows(0).Item("MATIN" + count1.ToString() + "").ToString()) * customerSaleValue1) / CDbl(ds1.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()), 0)
                                    cTotal2 = FormatNumber((CDbl(ds2.Tables(0).Rows(0).Item("MATIN" + count1.ToString() + "").ToString()) * customerSaleValue2) / CDbl(ds2.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()), 0)
                                    If CDbl(cTotal1) > CDbl(0) Then
                                        Comp = FormatNumber(((cTotal1 - cTotal2) / cTotal1) * 100, 1)
                                    Else
                                        Comp = "na"
                                    End If
                                End If
                                lbl.Text = Comp
                            Else
                                If i = 1 Or i = 5 Or i = 8 Or i = 11 Or i = 14 Or i = 22 Then
                                    lbl.Text = ""
                                ElseIf i = 2 Or i = 6 Or i = 9 Then
                                    lbl.Text = "na"
                                Else
                                    lbl.Text = "na"
                                End If

                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 5
                            InnerTdSetting(tdInner, "", "Right")
                            Dim Comp As String = String.Empty
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            If CDbl(ds1.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()) > 0.0 And CDbl(ds2.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()) > 0.0 Then
                                Dim cTotal1, cTotal2 As Double
                                If i = 1 Or i = 5 Or i = 8 Or i = 11 Or i = 14 Or i = 22 Then
                                    Comp = ""
                                ElseIf i = 2 Or i = 6 Or i = 9 Then
                                    Comp = "na"
                                Else
                                    cTotal1 = FormatNumber((CDbl(ds1.Tables(0).Rows(0).Item("MATIN" + count1.ToString() + "").ToString()) * customerSaleValue1) / CDbl(ds1.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()), 0)
                                    cTotal2 = FormatNumber((CDbl(ds2.Tables(0).Rows(0).Item("MATIN" + count1.ToString() + "").ToString()) * customerSaleValue2) / CDbl(ds2.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()), 0)
                                    If CDbl(cTotal1) > CDbl(0) Then
                                        Comp = FormatNumber((cTotal2 / cTotal1) * 100, 1)
                                    Else
                                        Comp = "na"
                                    End If
                                End If
                                lbl.Text = Comp
                            Else
                                If i = 1 Or i = 5 Or i = 8 Or i = 11 Or i = 14 Or i = 22 Then
                                    lbl.Text = ""
                                ElseIf i = 2 Or i = 6 Or i = 9 Then
                                    lbl.Text = "na"
                                Else
                                    lbl.Text = "na"
                                End If

                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                    End Select
                Next
                trInner.Height = 20
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
                'If i = 12 Or i = 16 Then
                '    trInner.CssClass = "TdHeading"
                'End If
                tblComparision.Controls.Add(trInner)
            Next



        Catch ex As Exception
            _lErrorLble.Text = "Error:GetMaterialIncineration:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetMaterialRecycling(ByVal ds1 As DataSet, ByVal ds2 As DataSet)
        Dim i As New Integer
        Dim j As New Integer
        Dim DWidth As String = String.Empty
        Dim trHeader As New TableRow
        Dim trHeader1 As New TableRow
        Dim trHeader2 As New TableRow
        Dim trHeader3 As New TableRow
        Dim trHeader4 As New TableRow

        Dim trInner As New TableRow

        Dim tdHeader As TableCell
        Dim tdHeader1 As TableCell
        Dim tdHeader2 As TableCell
        Dim tdHeader3 As TableCell
        Dim tdHeader4 As TableCell


        Dim lbl As New Label
        Dim hid As New HiddenField
        Dim Link As New HyperLink
        Dim txt As New TextBox
        Dim tdInner As TableCell
        Dim customerSaleValue1 As Double
        Dim customerSaleValue2 As Double

        Try
            customerSaleValue1 = FormatNumber(CDbl(((CDbl(ds1.Tables(0).Rows(0).Item("CUSSALESVOLUME").ToString()) * CDbl(ds1.Tables(0).Rows(0).Item("Convwt").ToString())))), 0)
            customerSaleValue2 = FormatNumber(CDbl(((CDbl(ds1.Tables(0).Rows(0).Item("CUSSALESVOLUME").ToString()) * CDbl(ds2.Tables(0).Rows(0).Item("Convwt").ToString())))), 0)

            For i = 1 To 5
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                tdHeader3 = New TableCell
                tdHeader4 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "200px", "", "1")
                        HeaderTdSetting(tdHeader1, "", "Case Id:", "1")
                        HeaderTdSetting(tdHeader2, "", "Package Format:", "1")
                        HeaderTdSetting(tdHeader3, "", "Unique Feature:", "1")
                        HeaderTdSetting(tdHeader4, "", "Units:", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader3.Controls.Add(tdHeader3)
                        trHeader4.Controls.Add(tdHeader4)

                        tdHeader1.HorizontalAlign = HorizontalAlign.Right
                        tdHeader2.HorizontalAlign = HorizontalAlign.Right
                        tdHeader3.HorizontalAlign = HorizontalAlign.Right
                        tdHeader4.HorizontalAlign = HorizontalAlign.Right
                    Case 2
                        Title = "(" + ds1.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                        HeaderTdSetting(tdHeader, "150px", "Material to Recycling", "1")
                        Header2TdSetting(tdHeader1, "", "" + ds1.Tables(0).Rows(0).Item("CaseId").ToString() + "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        Header2TdSetting(tdHeader3, "", "", "1")
                        GetCaseDetails(tdHeader2, tdHeader3, ds1.Tables(0).Rows(0).Item("CaseId").ToString())
                        Header2TdSetting(tdHeader4, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader3.Controls.Add(tdHeader3)
                        trHeader4.Controls.Add(tdHeader4)
                    Case 3
                        Title = "(" + ds2.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                        HeaderTdSetting(tdHeader, "150px", "Material to Recycling", "1")
                        Header2TdSetting(tdHeader1, "", "" + ds2.Tables(0).Rows(0).Item("CaseId").ToString() + "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        Header2TdSetting(tdHeader3, "", "", "1")
                        GetCaseDetails(tdHeader2, tdHeader3, ds2.Tables(0).Rows(0).Item("CaseId").ToString())
                        Header2TdSetting(tdHeader4, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader3.Controls.Add(tdHeader3)
                        trHeader4.Controls.Add(tdHeader4)
                    Case 4
                        HeaderTdSetting(tdHeader, "150px", "Improvement", "1")
                        Header2TdSetting(tdHeader1, "", ds1.Tables(0).Rows(0).Item("CaseId").ToString(), "1")
                        Header2TdSetting(tdHeader2, "", "Compared To", "1")
                        Header2TdSetting(tdHeader3, "", ds2.Tables(0).Rows(0).Item("CaseId").ToString(), "1")
                        Header2TdSetting(tdHeader4, "", "(%)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader3.Controls.Add(tdHeader3)
                        trHeader4.Controls.Add(tdHeader4)
                    Case 5
                        HeaderTdSetting(tdHeader, "150px", "Ratio", "1")
                        Header2TdSetting(tdHeader1, "", "(" + ds2.Tables(0).Rows(0).Item("CaseId").ToString() + "/" + ds1.Tables(0).Rows(0).Item("CaseId").ToString() + ")", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        Header2TdSetting(tdHeader3, "", "", "1")
                        Header2TdSetting(tdHeader4, "", "(%)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader3.Controls.Add(tdHeader3)
                        trHeader4.Controls.Add(tdHeader4)
                End Select





            Next
            tblComparision.Controls.Add(trHeader)
            tblComparision.Controls.Add(trHeader1)
            tblComparision.Controls.Add(trHeader2)
            tblComparision.Controls.Add(trHeader3)
            tblComparision.Controls.Add(trHeader4)


            'Inner
            Dim count1, count2 As Integer
            count1 = 0
            count2 = 0
            For i = 1 To 13
                trInner = New TableRow
                For j = 1 To 5
                    tdInner = New TableCell
                    Select Case j
                        Case 1
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            If i = 1 Or i = 5 Or i = 8 Or i = 11 Or i = 14 Or i = 22 Then
                                lbl.Text = "<b>" + ds1.Tables(0).Rows(0).Item("PLANTSPACE" + i.ToString() + "").ToString() + "</b>"
                            Else
                                lbl.Text = "<b style='margin-left:20px;'>" + ds1.Tables(0).Rows(0).Item("PLANTSPACE" + i.ToString() + "").ToString() + "</b>"
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 2
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            If i = 1 Or i = 5 Or i = 8 Or i = 11 Or i = 14 Or i = 22 Then
                                lbl.Text = ""
                            Else
                                count1 = count1 + 1
                                If CDbl(ds1.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()) > 0.0 Then
                                    If i = 2 Or i = 6 Or i = 9 Then
                                        lbl.Text = "0"
                                    Else
                                        lbl.Text = FormatNumber((CDbl(ds1.Tables(0).Rows(0).Item("MATRE" + count1.ToString() + "").ToString()) * customerSaleValue1) / CDbl(ds1.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()), 0)
                                    End If
                                Else
                                    lbl.Text = "na"
                                End If
                            End If

                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 3
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            If i = 1 Or i = 5 Or i = 8 Or i = 11 Or i = 14 Or i = 22 Then
                                lbl.Text = ""
                            Else
                                count2 = count2 + 1
                                If CDbl(ds2.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()) > 0.0 Then
                                    If i = 2 Or i = 6 Or i = 9 Then
                                        lbl.Text = "0"
                                    Else
                                        lbl.Text = FormatNumber((CDbl(ds2.Tables(0).Rows(0).Item("MATRE" + count2.ToString() + "").ToString()) * customerSaleValue2) / CDbl(ds2.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()), 0)
                                    End If
                                Else
                                    lbl.Text = "na"
                                End If

                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)

                        Case 4
                            InnerTdSetting(tdInner, "", "Right")
                            Dim Comp As String = String.Empty
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            If CDbl(ds1.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()) > 0.0 And CDbl(ds2.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()) > 0.0 Then
                                Dim cTotal1, cTotal2 As Double
                                If i = 1 Or i = 5 Or i = 8 Or i = 11 Or i = 14 Or i = 22 Then
                                    Comp = ""
                                ElseIf i = 2 Or i = 6 Or i = 9 Then
                                    Comp = "na"
                                Else
                                    cTotal1 = FormatNumber((CDbl(ds1.Tables(0).Rows(0).Item("MATRE" + count1.ToString() + "").ToString()) * customerSaleValue1) / CDbl(ds1.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()), 0)
                                    cTotal2 = FormatNumber((CDbl(ds2.Tables(0).Rows(0).Item("MATRE" + count1.ToString() + "").ToString()) * customerSaleValue2) / CDbl(ds2.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()), 0)
                                    If CDbl(cTotal1) > CDbl(0) Then
                                        Comp = FormatNumber(((cTotal1 - cTotal2) / cTotal1) * 100, 1)
                                    Else
                                        Comp = "na"
                                    End If
                                End If
                                lbl.Text = Comp
                            Else
                                If i = 1 Or i = 5 Or i = 8 Or i = 11 Or i = 14 Or i = 22 Then
                                    lbl.Text = ""
                                ElseIf i = 2 Or i = 6 Or i = 9 Then
                                    lbl.Text = "na"
                                Else
                                    lbl.Text = "na"
                                End If

                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 5
                            InnerTdSetting(tdInner, "", "Right")
                            Dim Comp As String = String.Empty
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            If CDbl(ds1.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()) > 0.0 And CDbl(ds2.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()) > 0.0 Then
                                Dim cTotal1, cTotal2 As Double
                                If i = 1 Or i = 5 Or i = 8 Or i = 11 Or i = 14 Or i = 22 Then
                                    Comp = ""
                                ElseIf i = 2 Or i = 6 Or i = 9 Then
                                    Comp = "na"
                                Else
                                    cTotal1 = FormatNumber((CDbl(ds1.Tables(0).Rows(0).Item("MATRE" + count1.ToString() + "").ToString()) * customerSaleValue1) / CDbl(ds1.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()), 0)
                                    cTotal2 = FormatNumber((CDbl(ds2.Tables(0).Rows(0).Item("MATRE" + count1.ToString() + "").ToString()) * customerSaleValue2) / CDbl(ds2.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()), 0)
                                    If CDbl(cTotal1) > CDbl(0) Then
                                        Comp = FormatNumber((cTotal2 / cTotal1) * 100, 1)
                                    Else
                                        Comp = "na"
                                    End If
                                End If
                                lbl.Text = Comp
                            Else
                                If i = 1 Or i = 5 Or i = 8 Or i = 11 Or i = 14 Or i = 22 Then
                                    lbl.Text = ""
                                ElseIf i = 2 Or i = 6 Or i = 9 Then
                                    lbl.Text = "na"
                                Else
                                    lbl.Text = "na"
                                End If

                            End If


                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                    End Select
                Next
                trInner.Height = 20
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
                'If i = 12 Or i = 16 Then
                '    trInner.CssClass = "TdHeading"
                'End If
                tblComparision.Controls.Add(trInner)
            Next



        Catch ex As Exception
            _lErrorLble.Text = "Error:GetMaterialRecycling:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetMaterialBalance(ByVal ds1 As DataSet, ByVal ds2 As DataSet)
        Dim i As New Integer
        Dim j As New Integer
        Dim DWidth As String = String.Empty
        Dim trHeader As New TableRow
        Dim trHeader1 As New TableRow
        Dim trHeader2 As New TableRow
        Dim trHeader3 As New TableRow
        Dim trHeader4 As New TableRow

        Dim trInner As New TableRow

        Dim tdHeader As TableCell
        Dim tdHeader1 As TableCell
        Dim tdHeader2 As TableCell
        Dim tdHeader3 As TableCell
        Dim tdHeader4 As TableCell


        Dim lbl As New Label
        Dim hid As New HiddenField
        Dim Link As New HyperLink
        Dim txt As New TextBox
        Dim tdInner As TableCell
        Dim customerSaleValue1 As Double
        Dim customerSaleValue2 As Double

        Try
            customerSaleValue1 = FormatNumber(CDbl(((CDbl(ds1.Tables(0).Rows(0).Item("CUSSALESVOLUME").ToString()) * CDbl(ds1.Tables(0).Rows(0).Item("Convwt").ToString())))), 0)
            customerSaleValue2 = FormatNumber(CDbl(((CDbl(ds1.Tables(0).Rows(0).Item("CUSSALESVOLUME").ToString()) * CDbl(ds2.Tables(0).Rows(0).Item("Convwt").ToString())))), 0)

            For i = 1 To 5
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                tdHeader3 = New TableCell
                tdHeader4 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "200px", "", "1")
                        HeaderTdSetting(tdHeader1, "", "Case Id:", "1")
                        HeaderTdSetting(tdHeader2, "", "Package Format:", "1")
                        HeaderTdSetting(tdHeader3, "", "Unique Feature:", "1")
                        HeaderTdSetting(tdHeader4, "", "Units:", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader3.Controls.Add(tdHeader3)
                        trHeader4.Controls.Add(tdHeader4)

                        tdHeader1.HorizontalAlign = HorizontalAlign.Right
                        tdHeader2.HorizontalAlign = HorizontalAlign.Right
                        tdHeader3.HorizontalAlign = HorizontalAlign.Right
                        tdHeader4.HorizontalAlign = HorizontalAlign.Right
                    Case 2
                        Title = "(" + ds1.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                        HeaderTdSetting(tdHeader, "150px", "Material Balance", "1")
                        Header2TdSetting(tdHeader1, "", "" + ds1.Tables(0).Rows(0).Item("CaseId").ToString() + "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        Header2TdSetting(tdHeader3, "", "", "1")
                        GetCaseDetails(tdHeader2, tdHeader3, ds1.Tables(0).Rows(0).Item("CaseId").ToString())
                        Header2TdSetting(tdHeader4, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader3.Controls.Add(tdHeader3)
                        trHeader4.Controls.Add(tdHeader4)
                    Case 3
                        Title = "(" + ds2.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                        HeaderTdSetting(tdHeader, "150px", "Material Balance", "1")
                        Header2TdSetting(tdHeader1, "", "" + ds2.Tables(0).Rows(0).Item("CaseId").ToString() + "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        Header2TdSetting(tdHeader3, "", "", "1")
                        GetCaseDetails(tdHeader2, tdHeader3, ds2.Tables(0).Rows(0).Item("CaseId").ToString())
                        Header2TdSetting(tdHeader4, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader3.Controls.Add(tdHeader3)
                        trHeader4.Controls.Add(tdHeader4)
                    Case 4
                        HeaderTdSetting(tdHeader, "150px", "Improvement", "1")
                        Header2TdSetting(tdHeader1, "", ds1.Tables(0).Rows(0).Item("CaseId").ToString(), "1")
                        Header2TdSetting(tdHeader2, "", "Compared To", "1")
                        Header2TdSetting(tdHeader3, "", ds2.Tables(0).Rows(0).Item("CaseId").ToString(), "1")
                        Header2TdSetting(tdHeader4, "", "(%)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader3.Controls.Add(tdHeader3)
                        trHeader4.Controls.Add(tdHeader4)
                    Case 5
                        HeaderTdSetting(tdHeader, "150px", "Ratio", "1")
                        Header2TdSetting(tdHeader1, "", "(" + ds2.Tables(0).Rows(0).Item("CaseId").ToString() + "/" + ds1.Tables(0).Rows(0).Item("CaseId").ToString() + ")", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        Header2TdSetting(tdHeader3, "", "", "1")
                        Header2TdSetting(tdHeader4, "", "(%)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader3.Controls.Add(tdHeader3)
                        trHeader4.Controls.Add(tdHeader4)
                End Select





            Next
            tblComparision.Controls.Add(trHeader)
            tblComparision.Controls.Add(trHeader1)
            tblComparision.Controls.Add(trHeader2)
            tblComparision.Controls.Add(trHeader3)
            tblComparision.Controls.Add(trHeader4)


            'Inner
            Dim count1, count2 As Integer
            count1 = 0
            count2 = 0
            For i = 1 To 13
                trInner = New TableRow
                For j = 1 To 5
                    tdInner = New TableCell
                    Select Case j
                        Case 1
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            If i = 1 Or i = 5 Or i = 8 Or i = 11 Or i = 14 Or i = 22 Then
                                lbl.Text = "<b>" + ds1.Tables(0).Rows(0).Item("PLANTSPACE" + i.ToString() + "").ToString() + "</b>"
                            Else
                                lbl.Text = "<b style='margin-left:20px;'>" + ds1.Tables(0).Rows(0).Item("PLANTSPACE" + i.ToString() + "").ToString() + "</b>"
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 2
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            If i = 1 Or i = 5 Or i = 8 Or i = 11 Or i = 14 Or i = 22 Then
                                lbl.Text = ""
                            Else
                                count1 = count1 + 1
                                If CDbl(ds1.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()) > 0.0 Then
                                    lbl.Text = FormatNumber((CDbl(ds1.Tables(0).Rows(0).Item("MATB" + count1.ToString() + "").ToString()) * customerSaleValue1) / CDbl(ds1.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()), 0)
                                Else
                                    lbl.Text = "na"
                                End If
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 3
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            If i = 1 Or i = 5 Or i = 8 Or i = 11 Or i = 14 Or i = 22 Then
                                lbl.Text = ""
                            Else
                                count2 = count2 + 1
                                If CDbl(ds2.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()) > 0.0 Then
                                    lbl.Text = FormatNumber((CDbl(ds2.Tables(0).Rows(0).Item("MATB" + count2.ToString() + "").ToString()) * customerSaleValue2) / CDbl(ds2.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()), 0)
                                Else
                                    lbl.Text = "na"
                                End If

                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)

                        Case 4
                            InnerTdSetting(tdInner, "", "Right")
                            Dim Comp As String = String.Empty
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            If CDbl(ds1.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()) > 0.0 And CDbl(ds2.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()) > 0.0 Then
                                Dim cTotal1, cTotal2 As Double
                                If i = 1 Or i = 5 Or i = 8 Or i = 11 Or i = 14 Or i = 22 Then
                                    Comp = ""
                                Else
                                    cTotal1 = FormatNumber((CDbl(ds1.Tables(0).Rows(0).Item("MATB" + count1.ToString() + "").ToString()) * customerSaleValue1) / CDbl(ds1.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()), 0)
                                    cTotal2 = FormatNumber((CDbl(ds2.Tables(0).Rows(0).Item("MATB" + count1.ToString() + "").ToString()) * customerSaleValue2) / CDbl(ds2.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()), 0)
                                    If CDbl(cTotal1) > CDbl(0) Then
                                        Comp = FormatNumber(((cTotal1 - cTotal2) / cTotal1) * 100, 1)
                                    Else
                                        Comp = "na"
                                    End If
                                End If
                                lbl.Text = Comp
                            Else
                                If i = 1 Or i = 5 Or i = 8 Or i = 11 Or i = 14 Or i = 22 Then
                                    lbl.Text = ""
                                Else
                                    lbl.Text = "na"
                                End If

                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 5
                            InnerTdSetting(tdInner, "", "Right")
                            Dim Comp As String = String.Empty
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            If CDbl(ds1.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()) > 0.0 And CDbl(ds2.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()) > 0.0 Then
                                Dim cTotal1, cTotal2 As Double
                                If i = 1 Or i = 5 Or i = 8 Or i = 11 Or i = 14 Or i = 22 Then
                                    Comp = ""
                                Else
                                    cTotal1 = FormatNumber((CDbl(ds1.Tables(0).Rows(0).Item("MATB" + count1.ToString() + "").ToString()) * customerSaleValue1) / CDbl(ds1.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()), 0)
                                    cTotal2 = FormatNumber((CDbl(ds2.Tables(0).Rows(0).Item("MATB" + count1.ToString() + "").ToString()) * customerSaleValue2) / CDbl(ds2.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()), 0)
                                    If CDbl(cTotal1) > CDbl(0) Then
                                        Comp = FormatNumber((cTotal2 / cTotal1) * 100, 1)
                                    Else
                                        Comp = "na"
                                    End If
                                End If
                                lbl.Text = Comp
                            Else
                                If i = 1 Or i = 5 Or i = 8 Or i = 11 Or i = 14 Or i = 22 Then
                                    lbl.Text = ""
                                Else
                                    lbl.Text = "na"
                                End If

                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                    End Select
                Next
                trInner.Height = 20
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
                'If i = 12 Or i = 16 Then
                '    trInner.CssClass = "TdHeading"
                'End If
                tblComparision.Controls.Add(trInner)
            Next



        Catch ex As Exception
            _lErrorLble.Text = "Error:GetMaterialBalance:" + ex.Message.ToString()
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
            Td.Height = 20
            Td.Font.Size = 10
            Td.Font.Bold = True
            Td.HorizontalAlign = HorizontalAlign.Center



        Catch ex As Exception
            _lErrorLble.Text = "Error:HeaderTdSetting:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub Header2TdSetting(ByVal Td As TableCell, ByVal Width As String, ByVal HeaderText As String, ByVal ColSpan As String)
        Try
            Td.Text = HeaderText
            Td.ColumnSpan = ColSpan
            If Width <> "" Then
                Td.Style.Add("width", Width)
            End If
            Td.CssClass = "TdHeading"
            Td.Font.Size = 8
            Td.Height = 20
            Td.HorizontalAlign = HorizontalAlign.Center



        Catch ex As Exception
            _lErrorLble.Text = "Error:HeaderTdSetting:" + ex.Message.ToString()
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
            Td.Style.Add("font-size", "13px")
        Catch ex As Exception
            _lErrorLble.Text = "Error:InnerTdSetting:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub TextBoxSetting(ByVal txt As TextBox, ByVal Css As String)
        Try
            txt.CssClass = Css

        Catch ex As Exception
            _lErrorLble.Text = "Error:TextBoxSetting:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub LableSetting(ByVal lbl As Label, ByVal Css As String)
        Try
            lbl.CssClass = Css

        Catch ex As Exception
            _lErrorLble.Text = "Error:LableSetting:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub LeftTdSetting(ByVal Td As TableCell, ByVal Text As String, ByVal tr As TableRow, ByVal Css As String)
        Try
            Td.Text = Text
            InnerTdSetting(Td, "", "Left")
            tr.Controls.Add(Td)
            tr.CssClass = Css
        Catch ex As Exception
            _lErrorLble.Text = "Error:LeftTdSetting:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetCaseDetails(ByVal td1 As TableCell, ByVal td2 As TableCell, ByVal CaseId12 As Integer)
        Dim objGetData As New SDistGetData.Selectdata()
        Dim ds As New DataSet
        Try
            CaseId12 = CaseId12
            ds = objGetData.GetCaseDetails(CaseId12.ToString())
            td1.Text = ds.Tables(0).Rows(0).Item("CASEDE1").ToString()
            td2.Text = ds.Tables(0).Rows(0).Item("CASEDE2").ToString()
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetCaseDetails:" + ex.Message.ToString()
        End Try
    End Sub
End Class
