Imports System.Data
Imports System.Data.OleDb
Imports System
Imports E2GetData
Imports E2UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Econ2_Results_CResultCost
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
            lblTag.Text = "Copyright 1997 - " + Now.Year().ToString() + " Savvypack Corporation."
            If Session("Back") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If
            GetSessionDetails()
            If Not IsPostBack Then
                Type = Convert.ToString(Request.QueryString("Type"))
                CaseId2 = Convert.ToString(Request.QueryString("CaseId"))
                GetPageDetails()
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetSessionDetails()
        Try
            UserId = Session("UserId")
            CaseId1 = Session("E2CaseId")
            UserRole = Session("E2UserRole")
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetSessionDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetPageDetails()
        Dim ds1 As New DataSet
        Dim ds2 As New DataSet
        Dim CaseIds As String = String.Empty
        CaseIds = "8227,7020"
        Dim objGetData As New E2GetData.Selectdata
        Dim obj As New CryptoHelper()
        Try
            Type = obj.Decrypt(Type)
            CaseId2 = obj.Decrypt(CaseId2)
            ds1 = objGetData.GetCostDetails(CaseId1, False)
            ds2 = objGetData.GetCostDetails(CaseId2, False)



            If Type = "TOT" Then
                GetByTotal(ds1, ds2)
            ElseIf Type = "BYWT" Then
                GetByWeight(ds1, ds2)
            ElseIf Type = "BYVOL" Then
                GetByVolume(ds1, ds2)
            End If



        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetByTotal(ByVal ds1 As DataSet, ByVal ds2 As DataSet)

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

        Try

            For i = 1 To 7
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
                        HeaderTdSetting(tdHeader, "150px", "Total", "1")
                        Header2TdSetting(tdHeader1, "", "" + ds1.Tables(0).Rows(0).Item("CaseId").ToString() + "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        Header2TdSetting(tdHeader3, "", "", "1")
                        GetCaseDetails(tdHeader2, tdHeader3, ds1.Tables(0).Rows(0).Item("CaseId").ToString())
                        Header2TdSetting(tdHeader4, "", "(" + ds1.Tables(0).Rows(0).Item("TITLE2").ToString() + ")", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader3.Controls.Add(tdHeader3)
                        trHeader4.Controls.Add(tdHeader4)
                    Case 3
                        HeaderTdSetting(tdHeader, "150px", "Total", "1")
                        Header2TdSetting(tdHeader1, "", "" + ds2.Tables(0).Rows(0).Item("CaseId").ToString() + "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        Header2TdSetting(tdHeader3, "", "", "1")
                        GetCaseDetails(tdHeader2, tdHeader3, ds2.Tables(0).Rows(0).Item("CaseId").ToString())
                        Header2TdSetting(tdHeader4, "", "(" + ds2.Tables(0).Rows(0).Item("TITLE2").ToString() + ")", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader3.Controls.Add(tdHeader3)
                        trHeader4.Controls.Add(tdHeader4)
                    Case 4
                        HeaderTdSetting(tdHeader, "150px", "Total Comparison", "1")
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
            For i = 1 To 24
                If i <> 25 Then
                    trInner = New TableRow
                    For j = 1 To 7
                        tdInner = New TableCell
                        Select Case j
                            Case 1
                                'Layer
                                InnerTdSetting(tdInner, "", "Left")
                                If i = 6 Or i = 24 Or i = 25 Then
                                    tdInner.Text = "<b>" + ds1.Tables(0).Rows(0).Item("DES" + i.ToString() + "").ToString() + "</b>" 'PDES Replaced by DES
                                Else
                                    tdInner.Text = "<span style='margin-left:20px;'><b>" + ds1.Tables(0).Rows(0).Item("DES" + i.ToString() + "").ToString() + "</b></span>" 'PDES Replaced by DES
                                End If

                                trInner.Controls.Add(tdInner)
                            Case 2
                                InnerTdSetting(tdInner, "", "Right")
                                lbl = New Label()
                                lbl.Text = FormatNumber(CDbl(ds1.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()) * CDbl(ds1.Tables(0).Rows(0).Item("CURR").ToString()), 0)
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Case 3
                                InnerTdSetting(tdInner, "", "Right")
                                lbl = New Label()
                                lbl.Text = FormatNumber(CDbl(ds2.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()) * CDbl(ds2.Tables(0).Rows(0).Item("CURR").ToString()), 0)
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Case 4
                                InnerTdSetting(tdInner, "", "Right")
                                lbl = New Label()
                                If (CDbl(ds1.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()) * CDbl(ds1.Tables(0).Rows(0).Item("CURR").ToString())) <> 0 Then
                                    lbl.Text = FormatNumber((CDbl(ds2.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()) * CDbl(ds2.Tables(0).Rows(0).Item("CURR").ToString())) / (CDbl(ds1.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()) * CDbl(ds1.Tables(0).Rows(0).Item("CURR").ToString())) * 100, 0)
                                Else
                                    lbl.Text = "na"
                                End If


                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)

                            Case Else
                        End Select
                    Next


                    tblComparision.Controls.Add(trInner)
                End If
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
            Next
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub


    Protected Sub GetByWeight(ByVal ds1 As DataSet, ByVal ds2 As DataSet)

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

        Try

            For i = 1 To 7
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
                        HeaderTdSetting(tdHeader, "150px", "Total", "1")
                        Header2TdSetting(tdHeader1, "", "" + ds1.Tables(0).Rows(0).Item("CaseId").ToString() + "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        Header2TdSetting(tdHeader3, "", "", "1")
                        GetCaseDetails(tdHeader2, tdHeader3, ds1.Tables(0).Rows(0).Item("CaseId").ToString())
                        Header2TdSetting(tdHeader4, "", "(" + ds1.Tables(0).Rows(0).Item("TITLE2").ToString() + "/" + ds1.Tables(0).Rows(0).Item("TITLE8").ToString() + ")", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader3.Controls.Add(tdHeader3)
                        trHeader4.Controls.Add(tdHeader4)
                    Case 3
                        HeaderTdSetting(tdHeader, "150px", "Total", "1")
                        Header2TdSetting(tdHeader1, "", "" + ds2.Tables(0).Rows(0).Item("CaseId").ToString() + "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        Header2TdSetting(tdHeader3, "", "", "1")
                        GetCaseDetails(tdHeader2, tdHeader3, ds2.Tables(0).Rows(0).Item("CaseId").ToString())
                        Header2TdSetting(tdHeader4, "", "(" + ds2.Tables(0).Rows(0).Item("TITLE2").ToString() + "/" + ds2.Tables(0).Rows(0).Item("TITLE8").ToString() + ")", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader3.Controls.Add(tdHeader3)
                        trHeader4.Controls.Add(tdHeader4)
                    Case 4
                        HeaderTdSetting(tdHeader, "150px", "Total Comparison", "1")
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
            For i = 1 To 24              'i =1 to 25
                If i <> 25 Then         'i <> 23
                    trInner = New TableRow
                    For j = 1 To 7
                        tdInner = New TableCell
                        Select Case j
                            Case 1
                                'Layer
                                InnerTdSetting(tdInner, "", "Left")
                                If i = 6 Or i = 24 Or i = 25 Then
                                    tdInner.Text = "<b>" + ds1.Tables(0).Rows(0).Item("DES" + i.ToString() + "").ToString() + "</b>" 'PDES Chenged by DES
                                Else
                                    tdInner.Text = "<span style='margin-left:20px;'><b>" + ds1.Tables(0).Rows(0).Item("DES" + i.ToString() + "").ToString() + "</b></span>"
                                End If

                                trInner.Controls.Add(tdInner)
                            Case 2
                                InnerTdSetting(tdInner, "", "Right")
                                Dim perwt As New Decimal
                                lbl = New Label()
                                Dim salesVolume As Double
                                salesVolume = CDbl(ds1.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()) / CDbl(ds1.Tables(0).Rows(0).Item("CONVWT").ToString())
                                If salesVolume > 1 Then
                                    perwt = CDbl(ds1.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()) / salesVolume * CDbl(ds1.Tables(0).Rows(0).Item("CURR").ToString()) / CDbl(ds1.Tables(0).Rows(0).Item("CONVWT").ToString())
                                    lbl.Text = FormatNumber(perwt, 3)
                                End If



                                'If CDbl(ds1.Tables(0).Rows(0).Item("SVOLUME").ToString()) > 0 Then
                                '    perwt = CDbl(ds1.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()) / CDbl(ds1.Tables(0).Rows(0).Item("SVOLUME").ToString())
                                '    lbl.Text = FormatNumber(perwt, 3)
                                'End If
                                '
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Case 3
                                InnerTdSetting(tdInner, "", "Right")
                                Dim perwt As New Decimal
                                lbl = New Label()
                                Dim salesVolume As Double
                                salesVolume = CDbl(ds2.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()) / CDbl(ds2.Tables(0).Rows(0).Item("CONVWT").ToString())
                                If salesVolume > 1 Then
                                    perwt = CDbl(ds2.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()) / salesVolume * CDbl(ds2.Tables(0).Rows(0).Item("CURR").ToString()) / CDbl(ds2.Tables(0).Rows(0).Item("CONVWT").ToString())
                                    lbl.Text = FormatNumber(perwt, 3)
                                End If


                                'If CDbl(ds2.Tables(0).Rows(0).Item("SVOLUME").ToString()) > 0 Then
                                '    perwt = CDbl(ds2.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()) / CDbl(ds2.Tables(0).Rows(0).Item("SVOLUME").ToString())
                                '    lbl.Text = FormatNumber(perwt, 3)
                                'End If
                                '
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Case 4
                                InnerTdSetting(tdInner, "", "Right")
                                Dim perwt As New Decimal
                                Dim perwt1 As New Decimal
                                Dim perwt2 As New Decimal
                                lbl = New Label()
                                Dim salesVolume1 As Double
                                Dim salesVolume2 As Double

                                salesVolume1 = CDbl(ds1.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()) / CDbl(ds1.Tables(0).Rows(0).Item("CONVWT").ToString())
                                If salesVolume1 > 1 Then
                                    perwt1 = CDbl(ds1.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()) / salesVolume1 * CDbl(ds1.Tables(0).Rows(0).Item("CURR").ToString()) / CDbl(ds1.Tables(0).Rows(0).Item("CONVWT").ToString())
                                    lbl.Text = FormatNumber(perwt, 3)
                                End If

                                salesVolume2 = CDbl(ds2.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()) / CDbl(ds2.Tables(0).Rows(0).Item("CONVWT").ToString())
                                If salesVolume2 > 1 Then
                                    perwt2 = CDbl(ds2.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()) / salesVolume2 * CDbl(ds2.Tables(0).Rows(0).Item("CURR").ToString()) / CDbl(ds2.Tables(0).Rows(0).Item("CONVWT").ToString())
                                    lbl.Text = FormatNumber(perwt, 3)
                                End If



                                'If CDbl(ds1.Tables(0).Rows(0).Item("SVOLUME").ToString()) > 0 Then
                                '    perwt1 = CDbl(ds1.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()) / CDbl(ds1.Tables(0).Rows(0).Item("SVOLUME").ToString())
                                'End If

                                'If CDbl(ds2.Tables(0).Rows(0).Item("SVOLUME").ToString()) > 0 Then
                                '    perwt2 = CDbl(ds2.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()) / CDbl(ds2.Tables(0).Rows(0).Item("SVOLUME").ToString())
                                'End If

                                If perwt1 > 0 Then
                                    perwt = (perwt2 / perwt1) * 100
                                    lbl.Text = FormatNumber(perwt, 3)
                                Else
                                    lbl.Text = "na"
                                End If

                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)

                            Case Else
                        End Select
                    Next


                    tblComparision.Controls.Add(trInner)
                End If
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
            Next
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetByVolume(ByVal ds1 As DataSet, ByVal ds2 As DataSet)

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

        Try

            For i = 1 To 7
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
                        HeaderTdSetting(tdHeader, "150px", "Total", "1")
                        Header2TdSetting(tdHeader1, "", "" + ds1.Tables(0).Rows(0).Item("CaseId").ToString() + "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        Header2TdSetting(tdHeader3, "", "", "1")
                        GetCaseDetails(tdHeader2, tdHeader3, ds1.Tables(0).Rows(0).Item("CaseId").ToString())
                        If CInt(ds1.Tables(0).Rows(0).Item("finvolmunits").ToString()) > 0 Then
                            Title = "(" + ds1.Tables(0).Rows(0).Item("title6").ToString() + " per unit)"
                        End If
                        Header2TdSetting(tdHeader4, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader3.Controls.Add(tdHeader3)
                        trHeader4.Controls.Add(tdHeader4)
                    Case 3
                        HeaderTdSetting(tdHeader, "150px", "Total", "1")
                        Header2TdSetting(tdHeader1, "", "" + ds2.Tables(0).Rows(0).Item("CaseId").ToString() + "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        Header2TdSetting(tdHeader3, "", "", "1")
                        GetCaseDetails(tdHeader2, tdHeader3, ds2.Tables(0).Rows(0).Item("CaseId").ToString())
                        If CInt(ds2.Tables(0).Rows(0).Item("finvolmunits").ToString()) > 0 Then
                            Title = "(" + ds2.Tables(0).Rows(0).Item("title6").ToString() + " per unit)"
                        End If
                        Header2TdSetting(tdHeader4, "", Title, "1")


                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader3.Controls.Add(tdHeader3)
                        trHeader4.Controls.Add(tdHeader4)
                    Case 4
                        HeaderTdSetting(tdHeader, "150px", "Total Comparison", "1")
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
            For i = 1 To 24              'i =1 to 25
                If i <> 25 Then         'i <> 23
                    trInner = New TableRow
                    For j = 1 To 7
                        tdInner = New TableCell
                        Select Case j
                            Case 1
                                'Layer
                                InnerTdSetting(tdInner, "", "Left")
                                If i = 6 Or i = 24 Or i = 25 Then
                                    tdInner.Text = "<b>" + ds1.Tables(0).Rows(0).Item("DES" + i.ToString() + "").ToString() + "</b>" 'PDES Changed by DES
                                Else
                                    tdInner.Text = "<span style='margin-left:20px;'><b>" + ds1.Tables(0).Rows(0).Item("DES" + i.ToString() + "").ToString() + "</b></span>"
                                End If

                                trInner.Controls.Add(tdInner)
                            Case 2
                                InnerTdSetting(tdInner, "", "Right")
                                Dim perunit As New Decimal
                                lbl = New Label()
                                If CInt(ds1.Tables(0).Rows(0).Item("finvolmunits").ToString()) > 0 Then
                                    perunit = CDbl(ds1.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()) / CDbl(ds1.Tables(0).Rows(0).Item("finvolmunits").ToString()) * 100 * CDbl(ds1.Tables(0).Rows(0).Item("CURR").ToString())
                                    lbl.Text = FormatNumber(perunit, 4)
                                End If
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Case 3
                                InnerTdSetting(tdInner, "", "Right")
                                Dim perunit As New Decimal
                                lbl = New Label()
                                If CInt(ds2.Tables(0).Rows(0).Item("finvolmunits").ToString()) > 0 Then
                                    perunit = CDbl(ds2.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()) / CDbl(ds2.Tables(0).Rows(0).Item("finvolmunits").ToString()) * 100 * CDbl(ds2.Tables(0).Rows(0).Item("CURR").ToString())
                                    lbl.Text = FormatNumber(perunit, 4)
                                End If
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Case 4
                                InnerTdSetting(tdInner, "", "Right")
                                Dim perunit As New Decimal
                                Dim perunit1 As New Decimal
                                Dim perunit2 As New Decimal
                                lbl = New Label()
                               If CInt(ds1.Tables(0).Rows(0).Item("finvolmunits").ToString()) > 0 Then
                                    perunit1 = CDbl(ds1.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()) / CDbl(ds1.Tables(0).Rows(0).Item("finvolmunits").ToString()) * 100 * CDbl(ds1.Tables(0).Rows(0).Item("CURR").ToString())
                                    lbl.Text = FormatNumber(perunit, 3)
                                End If

                                If CInt(ds2.Tables(0).Rows(0).Item("finvolmunits").ToString()) > 0 Then
                                    perunit2 = CDbl(ds2.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()) / CDbl(ds2.Tables(0).Rows(0).Item("finvolmunits").ToString()) * 100 * CDbl(ds2.Tables(0).Rows(0).Item("CURR").ToString())
                                    lbl.Text = FormatNumber(perunit, 3)
                                End If


                                If perunit1 > 0 Then
                                    perunit = (perunit2 / perunit1) * 100
                                    lbl.Text = FormatNumber(perunit, 3)
                                Else
                                    lbl.Text = "na"
                                End If

                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)

                            Case Else
                        End Select
                    Next


                    tblComparision.Controls.Add(trInner)
                End If
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
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
        Dim objGetData As New E2GetData.Selectdata()
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
