Imports System.Data
Imports System.Data.OleDb
Imports System
Imports StandGetData
Imports StandUpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Imports System.Globalization

Partial Class Pages_StandAssist_PopUp_SupplierMaterials
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim objUpIn As New StandUpInsData.UpdateInsert()
            If Session("Back") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If
            hidMatId.Value = Request.QueryString("MatId").ToString()
            hidSponsor.Value = Request.QueryString("Sponsor").ToString()
            hidGradeId.Value = Request.QueryString("GradeId").ToString()
            Page.Title = "Supplier Details"
            If Not IsPostBack Then
                GetSupplierMaterial()
            End If

            GetGradeDetails()
            lblTag.Text = "Copyright 1997 - " + Now.Year().ToString() + " SavvyPack Corporation"
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GetSupplierMaterial()
        Try
            Dim objGetdata As New StandGetData.Selectdata()
            Dim ds As New DataSet()
            Dim i As New Integer
            Dim j As New Integer
            Dim DWidth As String = String.Empty
            Dim trHeader As New TableRow
            Dim trHeader1 As New TableRow
            Dim trHeader2 As New TableRow
            Dim trInner As New TableRow
            Dim tdHeader As TableCell
            Dim lbl As New Label
            Dim hid As New HiddenField
            Dim hidGrade As New HiddenField
            Dim Link As New HyperLink
            Dim txt As New TextBox
            Dim tdInner As TableCell
            Dim dsSeq As New DataSet()
            Dim dsSeqD As New DataSet()
            Dim dvSeq As New DataView
            Dim dsGrade As New DataSet()
            Dim GName As String = String.Empty
            Dim ds1 As New DataSet()
            Dim dsSeq1 As New DataSet()
            Dim dvSeq2 As New DataView
            Dim count As Integer
            Dim dsConFact As New DataSet()
            Dim strUnit(0) As String
            Dim dvConv As New DataView
            Dim dtConv As New DataTable
            Dim dsDesc As New DataSet()
            Dim dsConFactProp As New DataSet()
            Dim dvConvProp As New DataView
            Dim dtConvProp As New DataTable
            Dim dsTestConFactProp As New DataSet()
            Dim dvTestConvProp As New DataView
            Dim dtTestConvProp As New DataTable
            Dim strFilter As String = String.Empty
            ' Dim dblValue As Double

            If Request.QueryString("GradeId").ToString() <> "0" Then
                dsDesc = objGetdata.GetGradeDesc(hidMatId.Value, hidSponsor.Value.ToString(), hidGradeId.Value)
                ds = objGetdata.GetSupplierMaterial(hidMatId.Value, hidSponsor.Value.ToString(), hidGradeId.Value)
                dsSeq = objGetdata.GetSupplierMaterialSEQ(hidMatId.Value, hidSponsor.Value.ToString(), hidGradeId.Value)
                dsConFact = objGetdata.GetMetricConvFactor()
                dsConFactProp = GetMetricConvFactorProp()
                dsTestConFactProp = objGetdata.GetMetricTestConvFactorProp()
                dvSeq = dsSeq.Tables(0).DefaultView
                dvConv = dsConFact.Tables(0).DefaultView
                dvConvProp = dsConFactProp.Tables(0).DefaultView
                dvTestConvProp = dsTestConFactProp.Tables(0).DefaultView
                strFilter = "MATERIALID=" + hidMatId.Value + " AND SUPPLIERID=" + hidSponsor.Value.ToString() + " AND GRADEID=" + hidGradeId.Value + " AND "
                If dsDesc.Tables(0).Rows.Count > 0 Then
                    If dsDesc.Tables(0).Rows(0).Item("DESCRIPTION").ToString() <> "" Then
                        lblNoDesc.Visible = False
                        lblDescription.Text = dsDesc.Tables(0).Rows(0).Item("DESCRIPTION").ToString()
                    Else
                        lblDescription.Visible = False
                        lblNoDesc.Text = "No Description Currently Available."
                    End If
                End If
                If ds.Tables(0).Rows.Count > 0 Then
                    For i = 1 To 10
                        tdHeader = New TableCell
                        Dim Title As String = String.Empty
                        'Header
                        Select Case i
                            Case 1
                                HeaderTdSetting(tdHeader, "40px", "Items", "1")
                                trHeader.Controls.Add(tdHeader)
                            Case 2
                                HeaderTdSetting(tdHeader, "230px", "Property", "1")
                                trHeader.Controls.Add(tdHeader)
                            Case 3
                                HeaderTdSetting(tdHeader, "100px", "Metric Value", "1")
                                tdHeader.Attributes.Add("Title", "The values and units displayed in the Metric section of this screen adhere strictly to Metric units only.")
                                trHeader.Controls.Add(tdHeader)
                            Case 4
                                HeaderTdSetting(tdHeader, "230px", "Metric Unit", "1")
                                tdHeader.Attributes.Add("Title", "The values and units displayed in the Metric section of this screen adhere strictly to Metric units only.")
                                trHeader.Controls.Add(tdHeader)
                            Case 5
                                HeaderTdSetting(tdHeader, "400px", "Metric Test Condition", "1")
                                tdHeader.Attributes.Add("Title", "The values and units displayed in the Metric section of this screen adhere strictly to Metric units only.")
                                trHeader.Controls.Add(tdHeader)
                            Case 6
                                HeaderTdSetting(tdHeader, "140px", "English Value", "1")
                                tdHeader.Attributes.Add("Title", "The values and units displayed in the English section of this screen do NOT adhere strictly to English units only, because industry practice is to occasionally mix English and Metric units together.")
                                trHeader.Controls.Add(tdHeader)
                            Case 7
                                HeaderTdSetting(tdHeader, "450px", "English Unit", "1")
                                tdHeader.Attributes.Add("Title", "The values and units displayed in the English section of this screen do NOT adhere strictly to English units only, because industry practice is to occasionally mix English and Metric units together.")
                                trHeader.Controls.Add(tdHeader)
                            Case 8
                                HeaderTdSetting(tdHeader, "400px", "English Test Condition", "1")
                                tdHeader.Attributes.Add("Title", "The values and units displayed in the English section of this screen do NOT adhere strictly to English units only, because industry practice is to occasionally mix English and Metric units together.")
                                trHeader.Controls.Add(tdHeader)
                            Case 9
                                HeaderTdSetting(tdHeader, "200px", "Test Method", "1")
                                trHeader.Controls.Add(tdHeader)
                            Case 10
                                HeaderTdSetting(tdHeader, "200px", "Comments", "1")
                                trHeader.Controls.Add(tdHeader)
                        End Select

                    Next
                    trHeader.Height = 30
                    trHeader.Height = 30
                    tblComparision.Controls.Add(trHeader)

                    'Inner
                    For i = 0 To ds.Tables(0).Rows.Count - 1
                        Dim dtSeq As New DataTable

                        trInner = New TableRow
                        For j = 1 To 10
                            tdInner = New TableCell

                            Select Case j
                                Case 1
                                    'Layer
                                    InnerTdSetting(tdInner, "", "Center")
                                    tdInner.Text = "<b>" + (i + 1).ToString() + "</b>"
                                    trInner.Controls.Add(tdInner)
                                Case 2
                                    InnerTdSetting(tdInner, "", "Left")
                                    lbl = New Label
                                    lbl.Text = "<b>" + ds.Tables(0).Rows(i).Item("PROPNAME").ToString() + "</b>"
                                    'Dim colWidth As Integer
                                    'colWidth = CInt(lbl.Text.Length)
                                    'InnerTdSetting(tdInner, colWidth, "Left")
                                    tdInner.Controls.Add(lbl)
                                    trInner.Controls.Add(tdInner)
                                Case 3
                                    InnerTdSetting(tdInner, "", "Right")
                                    lbl = New Label
                                    Dim strVal As String = ""
                                    Dim strSpValue As String
                                    Dim strSpChar As String = ""
                                    Dim strSpChar1 As String = ""
                                    Dim strArr() As String
                                    Dim str As String
                                    Dim Newstr As String = String.Empty

                                    Dim c As Integer

                                    dvSeq.RowFilter = "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND SEQUENCE=" + ds.Tables(0).Rows(i).Item("SEQUENCE").ToString()
                                    dtSeq = dvSeq.ToTable()
                                    If dtSeq.Rows.Count > 0 Then
                                        If dtSeq.Rows(0).Item("SEQUENCE1").ToString() <> "" Then
                                            If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim()).ToString() = "1" Then
                                                str = ds.Tables(0).Rows(i).Item("VALUE").ToString()

                                                If str.Contains("(") Then
                                                    strVal = ds.Tables(0).Rows(i).Item("VALUE").ToString()
                                                ElseIf str.Contains(".") Then
                                                    'strArr = str.Split(" ")
                                                    'For c = 0 To strArr.Length - 1
                                                    '    strSpValue = strArr(c)
                                                    '    If strSpValue = ">" Then
                                                    '        strSpChar = ">"
                                                    '    ElseIf strSpValue = "<" Then
                                                    '        strSpChar = "<"
                                                    '    End If
                                                    'Next
                                                    'Newstr = System.Text.RegularExpressions.Regex.Replace(str, "[^\d\.\-]", "")
                                                    'strVal = strSpChar + " " + FormatNumber(Newstr.ToString(), 3)

                                                    strVal = ds.Tables(0).Rows(i).Item("VALUE").ToString()
                                                ElseIf str.Contains(",") Then
                                                    strVal = ds.Tables(0).Rows(i).Item("VALUE").ToString()
                                                ElseIf str.Contains("to") Then
                                                    strVal = ds.Tables(0).Rows(i).Item("VALUE").ToString()
                                                Else
                                                    strArr = str.Split(" ")
                                                    For c = 0 To strArr.Length - 1
                                                        strSpValue = strArr(c)
                                                        If strSpValue = ">" Then
                                                            strSpChar = ">"
                                                        ElseIf strSpValue = "<" Then
                                                            strSpChar = "<"
                                                        End If
                                                    Next
                                                    Newstr = System.Text.RegularExpressions.Regex.Replace(str, "[^\d\.\-]", "")
                                                    strVal = strSpChar + " " + FormatNumber(Newstr.ToString(), 0)
                                                End If
                                                'strVal = ds.Tables(0).Rows(i).Item("VALUE").ToString()
                                            Else

                                                strVal = ds.Tables(0).Rows(i).Item("VALUE").ToString()
                                            End If
                                        End If
                                    Else
                                        strVal = ds.Tables(0).Rows(i).Item("VALUE").ToString()
                                    End If

                                    lbl.Text = strVal.ToString()
                                    If strVal = "" Then

                                        lbl.Text = ds.Tables(0).Rows(i).Item("VALUE").ToString()

                                    End If
                                    tdInner.Controls.Add(lbl)
                                    trInner.Controls.Add(tdInner)
                                Case 4
                                    InnerTdSetting(tdInner, "", "Left")
                                    lbl = New Label
                                    Dim strVal As String = ""
                                    dvSeq.RowFilter = "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND SEQUENCE=" + ds.Tables(0).Rows(i).Item("SEQUENCE").ToString()
                                    dtSeq = dvSeq.ToTable()
                                    If dtSeq.Rows.Count > 0 Then
                                        If dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim() <> "" Then
                                            If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim()).ToString() <> "1" Then
                                                strVal = strVal + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim()).ToString() + "" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim() + "_U").ToString() + ds.Tables(0).Rows(i).Item("SEQUENCE1").ToString().Trim()
                                                If dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim() = "RH" Then
                                                    strVal = strVal + " " + " RH "
                                                End If
                                            Else

                                                strVal = strVal + ds.Tables(0).Rows(i).Item("" + dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim() + "_U").ToString() + "" + ds.Tables(0).Rows(i).Item("SEQUENCE1").ToString().Trim()
                                            End If
                                        End If
                                        If dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim() <> "" Then
                                            If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim()).ToString() <> "1" Then
                                                strVal = strVal + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim()).ToString() + "" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim() + "_U").ToString() + ds.Tables(0).Rows(i).Item("SEQUENCE2").ToString().Trim()
                                                If dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim() = "RH" Then
                                                    strVal = strVal + " " + " RH "
                                                End If
                                            Else

                                                strVal = strVal + ds.Tables(0).Rows(i).Item("" + dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim() + "_U").ToString() + "" + ds.Tables(0).Rows(i).Item("SEQUENCE2").ToString().Trim()
                                            End If

                                        End If

                                        If dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim().Trim() <> "" Then
                                            If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim()).ToString() <> "1" Then
                                                strVal = strVal + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim()).ToString() + "" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim() + "_U").ToString() + ds.Tables(0).Rows(i).Item("SEQUENCE3").ToString().Trim()
                                                If dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim() = "RH" Then

                                                    strVal = strVal + " " + " RH "
                                                End If
                                            Else

                                                strVal = strVal + ds.Tables(0).Rows(i).Item("" + dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim() + "_U").ToString() + "" + ds.Tables(0).Rows(i).Item("SEQUENCE3").ToString().Trim()
                                            End If
                                        End If

                                        If dtSeq.Rows(0).Item("SEQUENCE4").ToString().Trim() <> "" Then
                                            If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE4").ToString().Trim()).ToString() <> "1" Then
                                                strVal = strVal + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE4").ToString().Trim()).ToString() + "" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE4").ToString().Trim() + "_U").ToString() + ds.Tables(0).Rows(i).Item("SEQUENCE4").ToString().Trim()
                                                If dtSeq.Rows(0).Item("SEQUENCE4").ToString().Trim() = "RH" Then
                                                    strVal = strVal + " " + " RH "
                                                End If
                                            Else

                                                strVal = strVal + ds.Tables(0).Rows(i).Item("" + dtSeq.Rows(0).Item("SEQUENCE4").ToString().Trim() + "_U").ToString() + "" + ds.Tables(0).Rows(i).Item("SEQUENCE4").ToString().Trim()
                                            End If

                                        End If

                                        If dtSeq.Rows(0).Item("SEQUENCE5").ToString().Trim() <> "" Then
                                            If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE5").ToString().Trim()).ToString() <> "1" Then

                                                strVal = strVal + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE5").ToString().Trim()).ToString() + "" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE5").ToString().Trim() + "_U").ToString() + ds.Tables(0).Rows(i).Item("SEQUENCE5").ToString().Trim()
                                                If dtSeq.Rows(0).Item("SEQUENCE5").ToString().Trim() = "RH" Then
                                                    strVal = strVal + " " + " RH "
                                                End If
                                            Else
                                                strVal = strVal + ds.Tables(0).Rows(i).Item("" + dtSeq.Rows(0).Item("SEQUENCE5").ToString().Trim() + "_U").ToString() + "" + ds.Tables(0).Rows(i).Item("SEQUENCE5").ToString().Trim()

                                            End If

                                        End If

                                        If dtSeq.Rows(0).Item("SEQUENCE6").ToString().Trim() <> "" Then
                                            If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE6").ToString().Trim()).ToString() <> "1" Then

                                                strVal = strVal + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE6").ToString().Trim()).ToString() + "" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE6").ToString().Trim() + "_U").ToString() + ds.Tables(0).Rows(i).Item("SEQUENCE6").ToString().Trim()
                                                If dtSeq.Rows(0).Item("SEQUENCE6").ToString().Trim() = "RH" Then
                                                    strVal = strVal + " " + " RH "
                                                End If
                                            Else

                                                strVal = strVal + +ds.Tables(0).Rows(i).Item("" + dtSeq.Rows(0).Item("SEQUENCE6").ToString().Trim() + "_U").ToString() + "" + ds.Tables(0).Rows(i).Item("SEQUENCE6").ToString().Trim()
                                            End If
                                        End If

                                    End If

                                    lbl.Text = strVal.ToString()
                                    tdInner.Controls.Add(lbl)
                                    trInner.Controls.Add(tdInner)

                                Case 5
                                    InnerTdSetting(tdInner, "", "Left")
                                    lbl = New Label
                                    Dim strVal As String = ""
                                    dvSeq.RowFilter = "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND SEQUENCE=" + ds.Tables(0).Rows(i).Item("SEQUENCE").ToString()
                                    dtSeq = dvSeq.ToTable()
                                    If dtSeq.Rows.Count > 0 Then
                                        If dtSeq.Rows(0).Item("TSEQUENCE1").ToString().Trim() <> "" Then
                                            If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE1").ToString().Trim()).ToString() <> "1" Then
                                                strVal = ds.Tables(0).Rows(i).Item("TSEQUENCE1").ToString().Trim() + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE1").ToString().Trim()).ToString() + "" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE1").ToString().Trim() + "_U").ToString() + " "
                                                If dtSeq.Rows(0).Item("TSEQUENCE1").ToString().Trim() = "TRH" Then
                                                    strVal = strVal + " " + " RH "
                                                End If
                                            Else

                                                strVal = ds.Tables(0).Rows(i).Item("TSEQUENCE1").ToString().Trim() + ds.Tables(0).Rows(i).Item("" + dtSeq.Rows(0).Item("TSEQUENCE1").ToString().Trim() + "_U").ToString() + " "
                                            End If
                                        End If
                                        If dtSeq.Rows(0).Item("TSEQUENCE2").ToString().Trim() <> "" Then
                                            If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE2").ToString().Trim()).ToString() <> "1" Then
                                                strVal = strVal + ds.Tables(0).Rows(i).Item("TSEQUENCE2").ToString().Trim() + " " + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE2").ToString().Trim()).ToString() + "" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE2").ToString().Trim() + "_U").ToString() + " "
                                                If dtSeq.Rows(0).Item("TSEQUENCE2").ToString().Trim() = "TRH" Then
                                                    strVal = strVal + " " + " RH "
                                                End If
                                            Else

                                                strVal = strVal + ds.Tables(0).Rows(i).Item("TSEQUENCE2").ToString().Trim() + ds.Tables(0).Rows(i).Item("" + dtSeq.Rows(0).Item("TSEQUENCE2").ToString().Trim() + "_U").ToString() + " "
                                            End If

                                        End If

                                        If dtSeq.Rows(0).Item("TSEQUENCE3").ToString().Trim() <> "" Then
                                            If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE3").ToString().Trim()).ToString() <> "1" Then
                                                strVal = strVal + ds.Tables(0).Rows(i).Item("TSEQUENCE3").ToString().Trim() + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE3").ToString().Trim()).ToString() + "" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE3").ToString().Trim() + "_U").ToString() + " "
                                                If dtSeq.Rows(0).Item("TSEQUENCE3").ToString().Trim() = "TRH" Then

                                                    strVal = strVal + " " + " RH "
                                                End If
                                            Else

                                                strVal = strVal + ds.Tables(0).Rows(i).Item("TSEQUENCE3").ToString().Trim() + ds.Tables(0).Rows(i).Item("" + dtSeq.Rows(0).Item("TSEQUENCE3").ToString().Trim() + "_U").ToString() + " "
                                            End If
                                        End If

                                        If dtSeq.Rows(0).Item("TSEQUENCE4").ToString().Trim() <> "" Then
                                            If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE4").ToString().Trim()).ToString() <> "1" Then
                                                strVal = strVal + ds.Tables(0).Rows(i).Item("TSEQUENCE4").ToString().Trim() + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE4").ToString().Trim()).ToString() + "" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE4").ToString().Trim() + "_U").ToString() + " "
                                                If dtSeq.Rows(0).Item("TSEQUENCE4").ToString().Trim() = "TRH" Then
                                                    strVal = strVal + " " + " RH "
                                                End If
                                            Else

                                                strVal = strVal + ds.Tables(0).Rows(i).Item("TSEQUENCE4").ToString().Trim() + ds.Tables(0).Rows(i).Item("" + dtSeq.Rows(0).Item("TSEQUENCE4").ToString().Trim() + "_U").ToString() + " "
                                            End If

                                        End If

                                        If dtSeq.Rows(0).Item("TSEQUENCE5").ToString().Trim() <> "" Then
                                            If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE5")).ToString().Trim() <> "1" Then

                                                strVal = strVal + ds.Tables(0).Rows(i).Item("TSEQUENCE5").ToString().Trim() + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE5").ToString().Trim()).ToString() + "" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE5").ToString().Trim() + "_U").ToString() + " "
                                                If dtSeq.Rows(0).Item("TSEQUENCE5").ToString().Trim() = "TRH" Then
                                                    strVal = strVal + " " + " RH "
                                                End If
                                            Else
                                                strVal = strVal + ds.Tables(0).Rows(i).Item("TSEQUENCE5").ToString().Trim() + ds.Tables(0).Rows(i).Item("" + dtSeq.Rows(0).Item("TSEQUENCE5").ToString().Trim() + "_U").ToString() + " "

                                            End If

                                        End If

                                        If dtSeq.Rows(0).Item("TSEQUENCE6").ToString().Trim() <> "" Then
                                            If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE6").ToString().Trim()).ToString() <> "1" Then


                                                strVal = strVal + ds.Tables(0).Rows(i).Item("TSEQUENCE6").ToString().Trim() + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE6").ToString().Trim()).ToString() + "" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE6").ToString().Trim() + "_U").ToString() + " "
                                                If dtSeq.Rows(0).Item("TSEQUENCE6").ToString().Trim() = "TRH" Then
                                                    strVal = strVal + " " + " RH "
                                                End If
                                            Else

                                                strVal = strVal + ds.Tables(0).Rows(i).Item("TSEQUENCE6").ToString().Trim() + ds.Tables(0).Rows(i).Item("" + dtSeq.Rows(0).Item("TSEQUENCE6").ToString().Trim() + "_U").ToString() + " "
                                            End If
                                        End If

                                    End If

                                    lbl.Text = strVal.ToString()
                                    tdInner.Controls.Add(lbl)
                                    trInner.Controls.Add(tdInner)
                                Case 6
                                    InnerTdSetting(tdInner, "", "Right")
                                    Dim ConvFact As String
                                    lbl = New Label
                                    Dim strVal As String = ""
                                    Dim dblValue As Double
                                    Dim strSpValue As String
                                    Dim strSpValue1 As String
                                    Dim strSpValue2 As String
                                    Dim strSpChar As String = ""
                                    Dim strSpChar1 As String = ""
                                    Dim strArr() As String
                                    Dim strArr1() As String
                                    Dim str As String
                                    Dim Newstr As String = String.Empty

                                    Dim c As Integer


                                    dtConv = dvConv.ToTable()
                                    dvSeq.RowFilter = "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND SEQUENCE=" + ds.Tables(0).Rows(i).Item("SEQUENCE").ToString()
                                    dtSeq = dvSeq.ToTable()
                                    If dtSeq.Rows.Count > 0 Then
                                        If dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim() <> "" Then

                                            str = ds.Tables(0).Rows(i).Item("VALUE").ToString()
                                            strArr = str.Split(" ")
                                            For c = 0 To strArr.Length - 1
                                                strSpValue = strArr(c)
                                                If strSpValue = ">" Then
                                                    strSpChar = ">"
                                                ElseIf strSpValue = "<" Then
                                                    strSpChar = "<"
                                                End If
                                            Next
                                            Newstr = System.Text.RegularExpressions.Regex.Replace(str, "[^\d\.\-]", "")

                                            For l = 0 To dtConv.Rows.Count - 1
                                                If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim() + "_U").ToString() = dtConv.Rows(l).Item("METRICUNIT").ToString().Trim() Then
                                                    ConvFact = CDbl(dsConFact.Tables(0).Rows(l).Item("CONVFACTOR").ToString())
                                                    str = ds.Tables(0).Rows(i).Item("VALUE").ToString()
                                                    If dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim().ToUpper() = "TEMP" Then
                                                        If str.Contains("(") Then
                                                            strArr = str.Split("(")
                                                            For c = 0 To strArr.Length
                                                                If c < strArr.Length Then
                                                                    strSpValue = strArr(c)
                                                                    Dim Newstr1 As String = System.Text.RegularExpressions.Regex.Replace(strSpValue, "[^\d\.]", "")
                                                                    dblValue = CDbl(Newstr1) * (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim()) * ConvFact) + 32
                                                                    strVal = FormatNumber(dblValue.ToString(), 0) + "("
                                                                    c = c + 1
                                                                    strSpValue = strArr(c)
                                                                    strArr1 = strSpValue.Split("-")
                                                                    strSpValue1 = strArr1(0)
                                                                    Dim Newstr2 As String = System.Text.RegularExpressions.Regex.Replace(strSpValue1, "[^\d\.]", "")
                                                                    dblValue = CDbl(Newstr2) * (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim()) * ConvFact) + 32
                                                                    strVal = strVal + FormatNumber(dblValue.ToString(), 0) + "-"
                                                                    strSpValue2 = strArr1(1)
                                                                    Dim Newstr3 As String = System.Text.RegularExpressions.Regex.Replace(strSpValue2, "[^\d\.]", "")
                                                                    dblValue = CDbl(Newstr3) * (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim()) * ConvFact) + 32
                                                                    strVal = strVal + FormatNumber(dblValue.ToString(), 0) + ")"
                                                                End If

                                                            Next
                                                        ElseIf str.Contains("to") Then
                                                            strArr = str.Split("to")
                                                            For c = 0 To strArr.Length
                                                                If c < strArr.Length Then
                                                                    strSpValue = strArr(0)
                                                                    Dim Newstr1 As String = System.Text.RegularExpressions.Regex.Replace(strSpValue, "[^\d\.]", "")
                                                                    dblValue = CDbl(Newstr1) * (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim()) * ConvFact) + 32
                                                                    strVal = FormatNumber(dblValue.ToString(), 0)
                                                                    c = c + 1
                                                                    strSpValue2 = strArr(1)
                                                                    Dim Newstr3 As String = System.Text.RegularExpressions.Regex.Replace(strSpValue2, "[^\d\.]", "")
                                                                    dblValue = CDbl(Newstr3) * (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim()) * ConvFact) + 32
                                                                    strVal = strVal + " to " + FormatNumber(dblValue.ToString(), 0)
                                                                End If

                                                            Next
                                                        Else
                                                            'If dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim().ToUpper() = "TEMP" Then
                                                            'dblValue = CDbl(Newstr) * (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim()) * ConvFact) + 32
                                                            If dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim().ToUpper() = "TEMP" And ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim() + "_U").ToString() = "K" Then
                                                                dblValue = CDbl(Newstr) * (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim()) * ConvFact)
                                                            Else
                                                                dvConvProp.RowFilter = strFilter + "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND METRICUNIT='" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim() + "_U").ToString() + "' "
                                                                dtConvProp = dvConvProp.ToTable()
                                                                If dtConvProp.Rows.Count > 0 Then
                                                                    'dblValue = CDbl(Newstr) * (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim()) * dtConvProp.Rows(0).Item("CONVFACTOR").ToString())
																	dblValue = CDbl(Newstr) * (1 * dtConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                                Else
                                                                    dblValue = CDbl(Newstr) * (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim()) * ConvFact) + 32
                                                                End If
                                                                'dblValue = CDbl(Newstr) * (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim()) * ConvFact) + 32
                                                            End If
                                                        End If
                                                    Else
                                                        dvConvProp.RowFilter = strFilter + "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND METRICUNIT='" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim() + "_U").ToString() + "' "
                                                        dtConvProp = dvConvProp.ToTable()
                                                        If dtConvProp.Rows.Count > 0 Then
                                                            If dtConvProp.Rows(0).Item("METRICUNIT").ToString() = dtConvProp.Rows(0).Item("ENGLISHUNIT").ToString() Then
                                                                dblValue = CDbl(Newstr) * dtConvProp.Rows(0).Item("CONVFACTOR").ToString()
                                                            Else
                                                                dblValue = CDbl(Newstr) * (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim()) * dtConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                            End If


                                                        Else
                                                            If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim() + "_U").ToString() = dtConv.Rows(l).Item("METRICUNIT").ToString().Trim() Then
                                                                If str.Contains("(") Then
                                                                    strArr = str.Split("(")
                                                                    For c = 0 To strArr.Length
                                                                        If c < strArr.Length Then
                                                                            strSpValue = strArr(c)
                                                                            Dim Newstr1 As String = System.Text.RegularExpressions.Regex.Replace(strSpValue, "[^\d\.]", "")
                                                                            dblValue = CDbl(Newstr1) * (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim()) * ConvFact)
                                                                            strVal = FormatNumber(dblValue.ToString(), 3) + "("
                                                                            c = c + 1
                                                                            strSpValue = strArr(c)
                                                                            strArr1 = strSpValue.Split("-")
                                                                            strSpValue1 = strArr1(0)
                                                                            Dim Newstr2 As String = System.Text.RegularExpressions.Regex.Replace(strSpValue1, "[^\d\.]", "")
                                                                            dblValue = CDbl(Newstr2) * (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim()) * ConvFact)
                                                                            strVal = strVal + FormatNumber(dblValue.ToString(), 3) + "-"
                                                                            strSpValue2 = strArr1(1)
                                                                            Dim Newstr3 As String = System.Text.RegularExpressions.Regex.Replace(strSpValue2, "[^\d\.]", "")
                                                                            dblValue = CDbl(Newstr3) * (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim()) * ConvFact)
                                                                            strVal = strVal + FormatNumber(dblValue.ToString(), 3) + ")"
                                                                        End If

                                                                    Next
                                                                ElseIf str.Contains("to") Then
                                                                    strArr = str.Split("to")
                                                                    For c = 0 To strArr.Length
                                                                        If c < strArr.Length Then
                                                                            strSpValue = strArr(0)
                                                                            Dim Newstr1 As String = System.Text.RegularExpressions.Regex.Replace(strSpValue, "[^\d\.]", "")
                                                                            dblValue = CDbl(Newstr1) * (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim()) * ConvFact)
                                                                            strVal = FormatNumber(dblValue.ToString(), 3)
                                                                            c = c + 1
                                                                            strSpValue2 = strArr(1)
                                                                            Dim Newstr3 As String = System.Text.RegularExpressions.Regex.Replace(strSpValue2, "[^\d\.]", "")
                                                                            dblValue = CDbl(Newstr3) * (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim()) * ConvFact)
                                                                            strVal = strVal + " to " + FormatNumber(dblValue.ToString(), 3)
                                                                        End If

                                                                    Next
                                                                Else
                                                                    dblValue = CDbl(Newstr) * (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim()) * ConvFact)
                                                                End If

                                                            End If
                                                        End If

                                                    End If
                                                    Exit For
                                                End If

                                            Next


                                        End If


                                        If dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim() <> "" Then

                                            For l = 0 To dtConv.Rows.Count - 1
                                                If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim() + "_U").ToString() = dtConv.Rows(l).Item("METRICUNIT").ToString().Trim() Then
                                                    ConvFact = CDbl(dsConFact.Tables(0).Rows(l).Item("CONVFACTOR").ToString())

                                                    If dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim().ToUpper() = "TEMP" Then
                                                        'dblValue = CDbl(dblValue) * (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim()) * ConvFact) + 32
                                                        If ds.Tables(0).Rows(i).Item("SEQUENCE1").ToString().Trim() = "/" Then
                                                            If dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim().ToUpper() = "TEMP" And ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim() + "_U").ToString() = "K" Then
                                                                dblValue = CDbl(Newstr) / (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim()) * ConvFact)
                                                            Else
                                                                dvConvProp.RowFilter = strFilter + "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND METRICUNIT='" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim() + "_U").ToString() + "' "
                                                                dtConvProp = dvConvProp.ToTable()
                                                                If dtConvProp.Rows.Count > 0 Then
                                                                    dblValue = CDbl(Newstr) / (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim()) * dtConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                                Else
                                                                    dblValue = CDbl(Newstr) / (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim()) * ConvFact) + 32
                                                                End If
                                                                'dblValue = CDbl(Newstr) / (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim()) * ConvFact) + 32
                                                            End If

                                                        ElseIf ds.Tables(0).Rows(i).Item("SEQUENCE1").ToString().Trim() = "*" Or ds.Tables(0).Rows(i).Item("SEQUENCE1").ToString().Trim() = "." Then
                                                            If dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim().ToUpper() = "TEMP" And ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim() + "_U").ToString() = "K" Then
                                                                dblValue = CDbl(Newstr) * (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim()) * ConvFact)
                                                            Else
                                                                dvConvProp.RowFilter = strFilter + "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND METRICUNIT='" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim() + "_U").ToString() + "' "
                                                                dtConvProp = dvConvProp.ToTable()
                                                                If dtConvProp.Rows.Count > 0 Then
                                                                    dblValue = CDbl(Newstr) * (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim()) * dtConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                                Else
                                                                    dblValue = CDbl(Newstr) * (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim()) * ConvFact) + 32
                                                                End If
                                                                'dblValue = CDbl(Newstr) * (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim()) * ConvFact) + 32
                                                            End If
                                                        End If
                                                    Else
                                                        If ds.Tables(0).Rows(i).Item("SEQUENCE1").ToString().Trim() = "/" Then
                                                            If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim()).ToString() <> 1 Then
                                                                dvConvProp.RowFilter = strFilter + "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND METRICUNIT='"+ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim()).ToString()+"" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim() + "_U").ToString() + "' "
                                                                dtConvProp = dvConvProp.ToTable()
                                                                If dtConvProp.Rows.Count > 0 Then
                                                                    If dtConvProp.Rows(0).Item("METRICUNIT").ToString() = dtConvProp.Rows(0).Item("ENGLISHUNIT").ToString() Then
                                                                        dblValue = CDbl(dblValue) / (1 * dtConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                                    Else
                                                                        dblValue = CDbl(dblValue) / (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim()) * dtConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                                    End If

                                                                Else
                                                                    If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim() + "_U").ToString() = dtConv.Rows(l).Item("METRICUNIT").ToString().Trim() Then
                                                                        dblValue = CDbl(dblValue) / (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim()) * ConvFact)
                                                                    End If
                                                                End If
                                                            Else
                                                                dvConvProp.RowFilter = strFilter + "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND METRICUNIT='" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim() + "_U").ToString() + "' "
                                                                dtConvProp = dvConvProp.ToTable()
                                                                If dtConvProp.Rows.Count > 0 Then
                                                                    If dtConvProp.Rows(0).Item("METRICUNIT").ToString() = dtConvProp.Rows(0).Item("ENGLISHUNIT").ToString() Then
                                                                        dblValue = CDbl(dblValue) / (1 * dtConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                                    Else
                                                                        dblValue = CDbl(dblValue) / (1 * dtConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                                    End If

                                                                Else
                                                                    If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim() + "_U").ToString() = dtConv.Rows(l).Item("METRICUNIT").ToString().Trim() Then
                                                                        dblValue = CDbl(dblValue) / (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim()) * ConvFact)
                                                                    End If
                                                                End If
                                                            End If
                                                           

                                                        ElseIf ds.Tables(0).Rows(i).Item("SEQUENCE1").ToString().Trim() = "*" Or ds.Tables(0).Rows(i).Item("SEQUENCE1").ToString().Trim() = "." Then
                                                            If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim()).ToString() <> 1 Then
                                                                dvConvProp.RowFilter = strFilter + "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND METRICUNIT='" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim()).ToString() + "" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim() + "_U").ToString() + "' "
                                                                dtConvProp = dvConvProp.ToTable()
                                                                If dtConvProp.Rows.Count > 0 Then
                                                                    If dtConvProp.Rows(0).Item("METRICUNIT").ToString() = dtConvProp.Rows(0).Item("ENGLISHUNIT").ToString() Then
                                                                        dblValue = CDbl(dblValue) * (1 * dtConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                                    Else
                                                                        dblValue = CDbl(dblValue) * (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim()) * ConvFact)
                                                                    End If

                                                                Else
                                                                    If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim() + "_U").ToString() = dtConv.Rows(l).Item("METRICUNIT").ToString().Trim() Then
                                                                        dblValue = CDbl(dblValue) * (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim()) * ConvFact)
                                                                    End If
                                                                End If
                                                            Else
                                                                dvConvProp.RowFilter = strFilter + "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND METRICUNIT='" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim() + "_U").ToString() + "' "
                                                                dtConvProp = dvConvProp.ToTable()
                                                                If dtConvProp.Rows.Count > 0 Then
                                                                    If dtConvProp.Rows(0).Item("METRICUNIT").ToString() = dtConvProp.Rows(0).Item("ENGLISHUNIT").ToString() Then
                                                                        dblValue = CDbl(dblValue) * (1 * dtConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                                    Else
                                                                        dblValue = CDbl(dblValue) * (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim()) * dtConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                                    End If

                                                                Else
                                                                    If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim() + "_U").ToString() = dtConv.Rows(l).Item("METRICUNIT").ToString().Trim() Then
                                                                        dblValue = CDbl(dblValue) * (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim()) * ConvFact)
                                                                    End If
                                                                End If
                                                            End If

                                                        End If
                                                    End If

                                                    Exit For
                                                End If
                                            Next
                                        End If

                                        If dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim() <> "" Then

                                            For l = 0 To dtConv.Rows.Count - 1
                                                If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim() + "_U").ToString() = dtConv.Rows(l).Item("METRICUNIT").ToString().Trim() Then
                                                    ConvFact = CDbl(dsConFact.Tables(0).Rows(l).Item("CONVFACTOR").ToString())

                                                    If dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim().ToUpper() = "TEMP" Then
                                                        'dblValue = CDbl(dblValue) * ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim()) * ConvFact + 32
                                                        If ds.Tables(0).Rows(i).Item("SEQUENCE2").ToString().Trim() = "/" Then
                                                            If dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim().ToUpper() = "TEMP" And ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim() + "_U").ToString() = "K" Then
                                                                dblValue = CDbl(Newstr) / (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim()) * ConvFact)
                                                            Else
                                                                dvConvProp.RowFilter = strFilter + "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND METRICUNIT='" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim() + "_U").ToString() + "' "
                                                                dtConvProp = dvConvProp.ToTable()
                                                                If dtConvProp.Rows.Count > 0 Then
                                                                    dblValue = CDbl(Newstr) / (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim()) * dtConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                                Else
                                                                    dblValue = CDbl(Newstr) / (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim()) * ConvFact) + 32
                                                                End If
                                                                'dblValue = CDbl(Newstr) / (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim()) * ConvFact) + 32
                                                            End If

                                                        ElseIf ds.Tables(0).Rows(i).Item("SEQUENCE2").ToString().Trim() = "*" Or ds.Tables(0).Rows(i).Item("SEQUENCE2").ToString().Trim() = "." Then
                                                            If dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim().ToUpper() = "TEMP" And ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim() + "_U").ToString() = "K" Then
                                                                dblValue = CDbl(Newstr) * (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim()) * ConvFact)
                                                            Else
                                                                dvConvProp.RowFilter = strFilter + "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND METRICUNIT='" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim() + "_U").ToString() + "' "
                                                                dtConvProp = dvConvProp.ToTable()
                                                                If dtConvProp.Rows.Count > 0 Then
                                                                    dblValue = CDbl(Newstr) * (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim()) * dtConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                                Else
                                                                    dblValue = CDbl(Newstr) * (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim()) * ConvFact) + 32
                                                                End If
                                                                'dblValue = CDbl(Newstr) * (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim()) * ConvFact) + 32
                                                            End If
                                                        End If
                                                    Else
                                                        If ds.Tables(0).Rows(i).Item("SEQUENCE2").ToString().Trim() = "/" Then
                                                            If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim()).ToString() <> 1 Then
                                                                dvConvProp.RowFilter = strFilter + "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND METRICUNIT='" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim()).ToString() + "" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim() + "_U").ToString() + "' "
                                                                dtConvProp = dvConvProp.ToTable()
                                                                If dtConvProp.Rows.Count > 0 Then
                                                                    If dtConvProp.Rows(0).Item("METRICUNIT").ToString() = dtConvProp.Rows(0).Item("ENGLISHUNIT").ToString() Then
                                                                        dblValue = CDbl(dblValue) / (1 * dtConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                                    Else
                                                                        dblValue = CDbl(dblValue) / (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim()) * dtConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                                    End If

                                                                Else
                                                                    If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim() + "_U").ToString() = dtConv.Rows(l).Item("METRICUNIT").ToString().Trim() Then
                                                                        dblValue = CDbl(dblValue) / (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim()) * ConvFact)

                                                                    End If
                                                                End If
                                                            Else
                                                                dvConvProp.RowFilter = strFilter + "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND METRICUNIT='" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim() + "_U").ToString() + "' "
                                                                dtConvProp = dvConvProp.ToTable()
                                                                If dtConvProp.Rows.Count > 0 Then
                                                                    If dtConvProp.Rows(0).Item("METRICUNIT").ToString() = dtConvProp.Rows(0).Item("ENGLISHUNIT").ToString() Then
                                                                        dblValue = CDbl(dblValue) / (1 * dtConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                                    Else
                                                                        dblValue = CDbl(dblValue) / (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim()) * dtConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                                    End If

                                                                Else
                                                                    If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim() + "_U").ToString() = dtConv.Rows(l).Item("METRICUNIT").ToString().Trim() Then
                                                                        dblValue = CDbl(dblValue) / (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim()) * ConvFact)

                                                                    End If
                                                                End If
                                                            End If
                                                            

                                                        ElseIf ds.Tables(0).Rows(i).Item("SEQUENCE2").ToString().Trim() = "*" Or ds.Tables(0).Rows(i).Item("SEQUENCE2").ToString().Trim() = "." Then
                                                            If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim()).ToString() <> 1 Then
                                                                dvConvProp.RowFilter = strFilter + "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND METRICUNIT='" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim()).ToString() + "" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim() + "_U").ToString() + "' "
                                                                dtConvProp = dvConvProp.ToTable()
                                                                If dtConvProp.Rows.Count > 0 Then
                                                                    If dtConvProp.Rows(0).Item("METRICUNIT").ToString() = dtConvProp.Rows(0).Item("ENGLISHUNIT").ToString() Then
                                                                        dblValue = CDbl(dblValue) * (1 * dtConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                                    Else
                                                                        dblValue = CDbl(dblValue) * (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim()) * ConvFact)
                                                                    End If

                                                                Else
                                                                    If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim() + "_U").ToString() = dtConv.Rows(l).Item("METRICUNIT").ToString().Trim() Then
                                                                        dblValue = CDbl(dblValue) * (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim()) * ConvFact)

                                                                    End If
                                                                End If
                                                            Else
                                                                dvConvProp.RowFilter = strFilter + "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND METRICUNIT='" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim() + "_U").ToString() + "' "
                                                                dtConvProp = dvConvProp.ToTable()
                                                                If dtConvProp.Rows.Count > 0 Then
                                                                    If dtConvProp.Rows(0).Item("METRICUNIT").ToString() = dtConvProp.Rows(0).Item("ENGLISHUNIT").ToString() Then
                                                                        dblValue = CDbl(dblValue) * (1 * dtConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                                    Else
                                                                        dblValue = CDbl(dblValue) * (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim()) * ConvFact)
                                                                    End If

                                                                Else
                                                                    If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim() + "_U").ToString() = dtConv.Rows(l).Item("METRICUNIT").ToString().Trim() Then
                                                                        dblValue = CDbl(dblValue) * (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim()) * ConvFact)

                                                                    End If
                                                                End If
                                                            End If
                                                            
                                                        End If
                                                    End If

                                                    Exit For

                                                End If

                                            Next

                                        End If

                                        If dtSeq.Rows(0).Item("SEQUENCE4").ToString().Trim() <> "" Then
                                            For l = 0 To dtConv.Rows.Count - 1
                                                If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE4").ToString().Trim() + "_U").ToString() = dtConv.Rows(l).Item("METRICUNIT").ToString().Trim() Then
                                                    ConvFact = CDbl(dsConFact.Tables(0).Rows(l).Item("CONVFACTOR").ToString())

                                                    If dtSeq.Rows(0).Item("SEQUENCE4").ToString().Trim().ToUpper() = "TEMP" Then
                                                        'dblValue = CDbl(dblValue) * (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE4").ToString().Trim()) * ConvFact) + 32
                                                        If ds.Tables(0).Rows(i).Item("SEQUENCE3").ToString().Trim() = "/" Then
                                                            If dtSeq.Rows(0).Item("SEQUENCE4").ToString().Trim().ToUpper() = "TEMP" And ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE4").ToString().Trim() + "_U").ToString() = "K" Then
                                                                dblValue = CDbl(Newstr) / (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE4").ToString().Trim()) * ConvFact)
                                                            Else

                                                                dblValue = CDbl(Newstr) / (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE4").ToString().Trim()) * ConvFact) + 32
                                                            End If

                                                        ElseIf ds.Tables(0).Rows(i).Item("SEQUENCE3").ToString().Trim() = "*" Or ds.Tables(0).Rows(i).Item("SEQUENCE3").ToString().Trim() = "." Then
                                                            If dtSeq.Rows(0).Item("SEQUENCE4").ToString().Trim().ToUpper() = "TEMP" And ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE4").ToString().Trim() + "_U").ToString() = "K" Then
                                                                dblValue = CDbl(Newstr) * (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE4").ToString().Trim()) * ConvFact)
                                                            Else

                                                                dblValue = CDbl(Newstr) * (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim()) * ConvFact) + 32
                                                            End If
                                                        End If
                                                    Else
                                                        If ds.Tables(0).Rows(i).Item("SEQUENCE3").ToString().Trim() = "/" Then
                                                            If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE4").ToString().Trim()).ToString() <> 1 Then
                                                                dvConvProp.RowFilter = strFilter + "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND METRICUNIT='" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE4").ToString().Trim()).ToString() + "" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE4").ToString().Trim() + "_U").ToString() + "' "
                                                                dtConvProp = dvConvProp.ToTable()
                                                                If dtConvProp.Rows.Count > 0 Then
                                                                    If dtConvProp.Rows(0).Item("METRICUNIT").ToString() = dtConvProp.Rows(0).Item("ENGLISHUNIT").ToString() Then
                                                                        dblValue = CDbl(dblValue) / (1 * dtConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                                    Else
                                                                        dblValue = CDbl(dblValue) / (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE4").ToString().Trim()) * dtConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                                    End If

                                                                Else
                                                                    If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE4").ToString().Trim() + "_U").ToString() = dtConv.Rows(l).Item("METRICUNIT").ToString().Trim() Then
                                                                        dblValue = CDbl(dblValue) / (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE4").ToString().Trim()) * ConvFact)

                                                                    End If
                                                                End If
                                                            Else
                                                                dvConvProp.RowFilter = strFilter + "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND METRICUNIT='" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE4").ToString().Trim() + "_U").ToString() + "' "
                                                                dtConvProp = dvConvProp.ToTable()
                                                                If dtConvProp.Rows.Count > 0 Then
                                                                    If dtConvProp.Rows(0).Item("METRICUNIT").ToString() = dtConvProp.Rows(0).Item("ENGLISHUNIT").ToString() Then
                                                                        dblValue = CDbl(dblValue) / (1 * dtConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                                    Else
                                                                        dblValue = CDbl(dblValue) / (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE4").ToString().Trim()) * dtConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                                    End If

                                                                Else
                                                                    If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE4").ToString().Trim() + "_U").ToString() = dtConv.Rows(l).Item("METRICUNIT").ToString().Trim() Then
                                                                        dblValue = CDbl(dblValue) / (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE4").ToString().Trim()) * ConvFact)

                                                                    End If
                                                                End If
                                                            End If
                                                            

                                                        ElseIf ds.Tables(0).Rows(i).Item("SEQUENCE3").ToString().Trim() = "*" Or ds.Tables(0).Rows(i).Item("SEQUENCE3").ToString().Trim() = "." Then
                                                            If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE4").ToString().Trim()).ToString() <> 1 Then
                                                                dvConvProp.RowFilter = strFilter + "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND METRICUNIT='" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE4").ToString().Trim()).ToString() + "" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE4").ToString().Trim() + "_U").ToString() + "' "
                                                                dtConvProp = dvConvProp.ToTable()
                                                                If dtConvProp.Rows.Count > 0 Then
                                                                    If dtConvProp.Rows(0).Item("METRICUNIT").ToString() = dtConvProp.Rows(0).Item("ENGLISHUNIT").ToString() Then
                                                                        dblValue = CDbl(dblValue) * (1 * dtConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                                    Else
                                                                        dblValue = CDbl(dblValue) * (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE4").ToString().Trim()) * dtConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                                    End If

                                                                Else
                                                                    If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE4").ToString().Trim() + "_U").ToString() = dtConv.Rows(l).Item("METRICUNIT").ToString().Trim() Then
                                                                        dblValue = CDbl(dblValue) * (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE4").ToString().Trim()) * ConvFact)

                                                                    End If
                                                                End If
                                                            Else
                                                                dvConvProp.RowFilter = strFilter + "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND METRICUNIT='" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE4").ToString().Trim() + "_U").ToString() + "' "
                                                                dtConvProp = dvConvProp.ToTable()
                                                                If dtConvProp.Rows.Count > 0 Then
                                                                    If dtConvProp.Rows(0).Item("METRICUNIT").ToString() = dtConvProp.Rows(0).Item("ENGLISHUNIT").ToString() Then
                                                                        dblValue = CDbl(dblValue) * (1 * dtConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                                    Else
                                                                        dblValue = CDbl(dblValue) * (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE4").ToString().Trim()) * dtConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                                    End If

                                                                Else
                                                                    If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE4").ToString().Trim() + "_U").ToString() = dtConv.Rows(l).Item("METRICUNIT").ToString().Trim() Then
                                                                        dblValue = CDbl(dblValue) * (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE4").ToString().Trim()) * ConvFact)

                                                                    End If
                                                                End If
                                                            End If
                                                            
                                                        End If


                                                    End If
                                                    Exit For

                                                End If

                                            Next

                                        End If
                                        If dtSeq.Rows(0).Item("SEQUENCE5").ToString().Trim() <> "" Then

                                            For l = 0 To dtConv.Rows.Count - 1
                                                If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE5").ToString().Trim() + "_U").ToString() = dtConv.Rows(l).Item("METRICUNIT").ToString().Trim() Then
                                                    ConvFact = CDbl(dsConFact.Tables(0).Rows(l).Item("CONVFACTOR").ToString())

                                                    If dtSeq.Rows(0).Item("SEQUENCE5").ToString().Trim().ToUpper() = "TEMP" Then
                                                        'dblValue = CDbl(dblValue) * (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE5").ToString().Trim()) * ConvFact) + 32
                                                        If ds.Tables(0).Rows(i).Item("SEQUENCE4").ToString().Trim() = "/" Then
                                                            If dtSeq.Rows(0).Item("SEQUENCE5").ToString().Trim().ToUpper() = "TEMP" And ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE5").ToString().Trim() + "_U").ToString() = "K" Then
                                                                dblValue = CDbl(Newstr) / (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE5").ToString().Trim()) * ConvFact)
                                                            Else

                                                                dblValue = CDbl(Newstr) / (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE5").ToString().Trim()) * ConvFact) + 32
                                                            End If

                                                        ElseIf ds.Tables(0).Rows(i).Item("SEQUENCE4").ToString().Trim() = "*" Or ds.Tables(0).Rows(i).Item("SEQUENCE4").ToString().Trim() = "." Then
                                                            If dtSeq.Rows(0).Item("SEQUENCE5").ToString().Trim().ToUpper() = "TEMP" And ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE5").ToString().Trim() + "_U").ToString() = "K" Then
                                                                dblValue = CDbl(Newstr) * (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE5").ToString().Trim()) * ConvFact)
                                                            Else

                                                                dblValue = CDbl(Newstr) * (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE5").ToString().Trim()) * ConvFact) + 32
                                                            End If
                                                        End If
                                                    Else
                                                        If ds.Tables(0).Rows(i).Item("SEQUENCE4").ToString().Trim() = "/" Then
                                                            dvConvProp.RowFilter = strFilter + "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND METRICUNIT='" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE5").ToString().Trim() + "_U").ToString() + "' "
                                                            dtConvProp = dvConvProp.ToTable()
                                                            If dtConvProp.Rows.Count > 0 Then
                                                                dblValue = CDbl(dblValue) / (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE5").ToString().Trim()) * dtConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                            Else
                                                                If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE5").ToString().Trim() + "_U").ToString() = dtConv.Rows(l).Item("METRICUNIT").ToString().Trim() Then
                                                                    dblValue = CDbl(dblValue) / (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE5").ToString().Trim()) * ConvFact)

                                                                End If
                                                            End If

                                                        ElseIf ds.Tables(0).Rows(i).Item("SEQUENCE4").ToString().Trim() = "*" Or ds.Tables(0).Rows(i).Item("SEQUENCE4").ToString().Trim() = "." Then
                                                            dvConvProp.RowFilter = strFilter + "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND METRICUNIT='" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE5").ToString().Trim() + "_U").ToString() + "' "
                                                            dtConvProp = dvConvProp.ToTable()
                                                            If dtConvProp.Rows.Count > 0 Then
                                                                dblValue = CDbl(dblValue) * (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE5").ToString().Trim()) * dtConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                            Else
                                                                If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE5").ToString().Trim() + "_U").ToString() = dtConv.Rows(l).Item("METRICUNIT").ToString().Trim() Then
                                                                    dblValue = CDbl(dblValue) * (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE5").ToString().Trim()) * ConvFact)
                                                                End If
                                                            End If
                                                        End If


                                                    End If
                                                    Exit For

                                                End If

                                            Next

                                        End If
                                        If dtSeq.Rows(0).Item("SEQUENCE6").ToString().Trim() <> "" Then

                                            For l = 0 To dtConv.Rows.Count - 1
                                                If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE6").ToString().Trim() + "_U").ToString() = dtConv.Rows(l).Item("METRICUNIT").ToString().Trim() Then
                                                    ConvFact = CDbl(dsConFact.Tables(0).Rows(l).Item("CONVFACTOR").ToString())

                                                    If dtSeq.Rows(0).Item("SEQUENCE6").ToString().Trim().ToUpper() = "TEMP" Then
                                                        dblValue = CDbl(dblValue) * ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE6").ToString().Trim()) * ConvFact + 32
                                                    Else
                                                        If ds.Tables(0).Rows(i).Item("SEQUENCE5").ToString().Trim() = "/" Then
                                                            dvConvProp.RowFilter = strFilter + "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND METRICUNIT='" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE6").ToString().Trim() + "_U").ToString() + "' "
                                                            dtConvProp = dvConvProp.ToTable()
                                                            If dtConvProp.Rows.Count > 0 Then
                                                                dblValue = CDbl(dblValue) / (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE6").ToString().Trim()) * dtConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                            Else
                                                                If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE6").ToString().Trim() + "_U").ToString() = dtConv.Rows(l).Item("METRICUNIT").ToString().Trim() Then
                                                                    dblValue = CDbl(dblValue) / (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE6").ToString().Trim()) * ConvFact)

                                                                End If
                                                            End If

                                                        ElseIf ds.Tables(0).Rows(i).Item("SEQUENCE5").ToString().Trim() = "*" Or ds.Tables(0).Rows(i).Item("SEQUENCE5").ToString().Trim() = "." Then
                                                            dvConvProp.RowFilter = strFilter + "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND METRICUNIT='" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE6").ToString().Trim() + "_U").ToString() + "' "
                                                            dtConvProp = dvConvProp.ToTable()
                                                            If dtConvProp.Rows.Count > 0 Then
                                                                dblValue = CDbl(dblValue) * (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE6").ToString().Trim()) * dtConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                            Else
                                                                If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE6").ToString().Trim() + "_U").ToString() = dtConv.Rows(l).Item("METRICUNIT").ToString().Trim() Then
                                                                    dblValue = CDbl(dblValue) * (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE6").ToString().Trim()) * ConvFact)

                                                                End If
                                                            End If
                                                        End If
                                                    End If

                                                    Exit For

                                                End If

                                            Next

                                        End If
                                    End If
                                    'lbl.Text = FormatNumber(dblValue.ToString(), 3)
                                    If ds.Tables(0).Rows(i).Item("VALUE").ToString().Trim().ToUpper() = "CAST" Then
                                        strVal = ds.Tables(0).Rows(i).Item("VALUE").ToString()
                                    ElseIf ds.Tables(0).Rows(i).Item("VALUE").ToString().Trim().ToUpper() = "NO BREAK" Then
                                        strVal = ds.Tables(0).Rows(i).Item("VALUE").ToString()
                                    Else
                                        str = ds.Tables(0).Rows(i).Item("VALUE").ToString()
                                        If str.Contains("(") Then
                                        ElseIf str.Contains("to") Then

                                        Else
                                            strVal = strSpChar + " " + FormatNumber(dblValue.ToString(), 4)
                                        End If

                                    End If

                                    If strVal <> "" And IsNumeric(strVal) Then
                                        Dim val() As String
                                        val = Regex.Split(strVal.ToString(), "\.")

                                        If val(1) = "0000" Then
                                            lbl.Text = val(0).ToString()
                                        Else
                                            lbl.Text = strVal.ToString()
                                        End If
                                    Else
                                        lbl.Text = strVal.ToString()
                                    End If

                                    'lbl.Text = strVal.ToString()
                                    If dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim().ToUpper() = "TIME" Then
                                        lbl.Text = ds.Tables(0).Rows(i).Item("VALUE").ToString()
                                    End If

                                    If dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim() = "" Then
                                        lbl.Text = ds.Tables(0).Rows(i).Item("VALUE").ToString()
                                    Else
                                        If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim() + "_U").ToString() = "%" And ds.Tables(0).Rows(i).Item("PROPNAME").ToString().Trim().ToUpper() <> "MOLDING SHRINKAGE- FLOW" Then
                                            lbl.Text = ds.Tables(0).Rows(i).Item("VALUE").ToString()
                                        ElseIf ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim() + "_U").ToString() = "%" And ds.Tables(0).Rows(i).Item("PROPNAME").ToString().Trim().ToUpper() = "MOLDING SHRINKAGE- FLOW" Then

                                        End If
                                    End If
                                    tdInner.Controls.Add(lbl)
                                    trInner.Controls.Add(tdInner)

                                Case 7
                                    InnerTdSetting(tdInner, "", "Left")
                                    lbl = New Label
                                    Dim strVal As String = ""
                                    Dim ConvFact As String
                                    Dim dblValue As Decimal
                                    dtConv = dvConv.ToTable()
                                    dvSeq.RowFilter = "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND SEQUENCE=" + ds.Tables(0).Rows(i).Item("SEQUENCE").ToString()
                                    dtSeq = dvSeq.ToTable()
                                    If dtSeq.Rows.Count > 0 Then
                                        If dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim() <> "" Then
                                            For l = 0 To dtConv.Rows.Count - 1

                                                If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim()).ToString() <> "1" Then
                                                    If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim() + "_U").ToString() = dtConv.Rows(l).Item("METRICUNIT").ToString().Trim() Then
                                                        ConvFact = CDbl(dsConFact.Tables(0).Rows(l).Item("CONVFACTOR").ToString())

                                                        If dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim().ToUpper() = "TEMP" Then
                                                            dblValue = (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim()) * ConvFact) + 32
                                                        Else
                                                            dvConvProp.RowFilter = strFilter + "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND METRICUNIT='" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim() + "_U").ToString() + "' "
                                                            dtConvProp = dvConvProp.ToTable()
                                                            If dtConvProp.Rows.Count > 0 Then
                                                                dblValue = (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim()) * dtConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                            Else
                                                                dblValue = (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim()) * ConvFact)
                                                            End If
                                                        End If

                                                        If dtConvProp.Rows.Count > 0 Then
                                                            strVal = strVal + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim()) + dtConvProp.Rows(0).Item("ENGLISHUNIT").ToString().Trim() + ds.Tables(0).Rows(i).Item("SEQUENCE1").ToString().Trim()
                                                        Else
                                                            If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim() + "_U").ToString() = dtConv.Rows(l).Item("METRICUNIT").ToString().Trim() Then
                                                                strVal = strVal + FormatNumber(dblValue.ToString(), 4) + dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim() + ds.Tables(0).Rows(i).Item("SEQUENCE1").ToString().Trim()
                                                            End If
                                                        End If

                                                        Exit For
                                                    End If

                                                Else
                                                    If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim() + "_U").ToString() = dtConv.Rows(l).Item("METRICUNIT").ToString().Trim() Then
                                                        dvConvProp.RowFilter = strFilter + "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND METRICUNIT='" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim() + "_U").ToString() + "' "
                                                        dtConvProp = dvConvProp.ToTable()
                                                        If dtConvProp.Rows.Count > 0 Then
                                                            strVal = strVal + dtConvProp.Rows(0).Item("ENGLISHUNIT").ToString().Trim() + ds.Tables(0).Rows(i).Item("SEQUENCE1").ToString().Trim()
                                                        Else
                                                            If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim() + "_U").ToString() = dtConv.Rows(l).Item("METRICUNIT").ToString().Trim() Then
                                                                strVal = strVal + dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim() + ds.Tables(0).Rows(i).Item("SEQUENCE1").ToString().Trim()
                                                            End If
                                                        End If

                                                        If dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim().ToUpper() = "PERCENTAGE" And ds.Tables(0).Rows(i).Item("PROPNAME").ToString().Trim().ToUpper() <> "MOLDING SHRINKAGE- FLOW" Then
                                                            If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim() + "_U").ToString() = "%" And dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim() = "in/in" Then
                                                                strVal = "%" + ds.Tables(0).Rows(i).Item("SEQUENCE1").ToString().Trim()
                                                            End If
                                                        End If
                                                        Exit For
                                                    End If

                                                End If

                                            Next
                                            If dtSeq.Rows(0).Item("SEQUENCE1").ToString() = "RH" Then
                                                strVal = strVal + " " + " RH "
                                            End If
                                            If dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim().ToUpper() = "TIME" And ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim()).ToString() <> "1" Then
                                                strVal = strVal + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim()).ToString() + "" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim() + "_U").ToString() + ds.Tables(0).Rows(i).Item("SEQUENCE1").ToString().Trim()
                                            ElseIf dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim().ToUpper() = "TIME" And ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim()).ToString() = "1" Then
                                                strVal = strVal + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim() + "_U").ToString() + ds.Tables(0).Rows(i).Item("SEQUENCE1").ToString().Trim()
                                            End If
                                        End If

                                        If dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim() <> "" Then
                                            For l = 0 To dtConv.Rows.Count - 1
                                                If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim()).ToString() <> "1" Then

                                                    If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim() + "_U").ToString() = dtConv.Rows(l).Item("METRICUNIT").ToString().Trim() Then
                                                        ConvFact = CDbl(dsConFact.Tables(0).Rows(l).Item("CONVFACTOR").ToString())

                                                        If dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim().ToUpper() = "TEMP" Then
                                                            dvConvProp.RowFilter = strFilter + "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND METRICUNIT='" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim() + "_U").ToString() + "' "
                                                            dtConvProp = dvConvProp.ToTable()
                                                            If dtConvProp.Rows.Count > 0 Then
                                                                dblValue = (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim()) * dtConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                            Else
                                                                dblValue = (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim()) * ConvFact) + 32
                                                            End If
                                                            'dblValue = (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim()) * ConvFact) + 32
                                                        Else
                                                            dvConvProp.RowFilter = strFilter + "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND METRICUNIT='" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim()).ToString() + "" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim() + "_U").ToString() + "' "
                                                            dtConvProp = dvConvProp.ToTable()
                                                            If dtConvProp.Rows.Count > 0 Then
                                                                If dtConvProp.Rows(0).Item("METRICUNIT").ToString() = dtConvProp.Rows(0).Item("ENGLISHUNIT").ToString() Then
                                                                    dblValue = (1 * dtConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                                Else
                                                                    dblValue = (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim()) * dtConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                                End If

                                                            Else
                                                                dblValue = (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim()) * ConvFact)
                                                            End If
                                                        End If
                                                        If dtConvProp.Rows.Count > 0 Then
                                                            strVal = strVal + dtConvProp.Rows(0).Item("ENGLISHUNIT").ToString().Trim() + ds.Tables(0).Rows(i).Item("SEQUENCE2").ToString().Trim()
                                                        Else
                                                            If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim() + "_U").ToString() = dtConv.Rows(l).Item("METRICUNIT").ToString().Trim() Then
                                                                strVal = strVal + FormatNumber(dblValue.ToString(), 4) + dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim() + ds.Tables(0).Rows(i).Item("SEQUENCE2").ToString().Trim()
                                                            End If
                                                        End If

                                                        Exit For
                                                    End If

                                                Else
                                                    If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim() + "_U").ToString() = dtConv.Rows(l).Item("METRICUNIT").ToString().Trim() Then
                                                        dvConvProp.RowFilter = strFilter + "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND METRICUNIT='" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim() + "_U").ToString() + "' "
                                                        dtConvProp = dvConvProp.ToTable()
                                                        If dtConvProp.Rows.Count > 0 Then
                                                            strVal = strVal + dtConvProp.Rows(0).Item("ENGLISHUNIT").ToString().Trim() + ds.Tables(0).Rows(i).Item("SEQUENCE2").ToString().Trim()
                                                        Else
                                                            If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim() + "_U").ToString() = dtConv.Rows(l).Item("METRICUNIT").ToString().Trim() Then
                                                                strVal = strVal + dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim() + ds.Tables(0).Rows(i).Item("SEQUENCE2").ToString().Trim()
                                                            End If
                                                        End If

                                                        If dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim().ToUpper() = "PERCENTAGE" And ds.Tables(0).Rows(i).Item("PROPNAME").ToString().Trim().ToUpper() <> "MOLDING SHRINKAGE- FLOW" Then
                                                            If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim() + "_U").ToString() = "%" And dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim() = "in/in" Then
                                                                strVal = "%" + ds.Tables(0).Rows(i).Item("SEQUENCE2").ToString().Trim()
                                                            End If
                                                        End If

                                                        Exit For
                                                    End If

                                                End If

                                            Next

                                            If dtSeq.Rows(0).Item("SEQUENCE2").ToString() = "RH" Then
                                                strVal = strVal + " " + " RH "
                                            End If
                                            If dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim().ToUpper() = "TIME" And ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim()).ToString() <> "1" Then
                                                strVal = strVal + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim()).ToString() + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim() + "_U").ToString() + ds.Tables(0).Rows(i).Item("SEQUENCE2").ToString().Trim()
                                            ElseIf dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim().ToUpper() = "TIME" And ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim()).ToString() = "1" Then
                                                strVal = strVal + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE2").ToString().Trim() + "_U").ToString() + ds.Tables(0).Rows(i).Item("SEQUENCE2").ToString()
                                            End If

                                        End If

                                        If dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim() <> "" Then
                                            For l = 0 To dtConv.Rows.Count - 1
                                                If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim()).ToString() <> "1" Then

                                                    If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim() + "_U").ToString() = dtConv.Rows(l).Item("METRICUNIT").ToString().Trim() Then
                                                        ConvFact = CDbl(dsConFact.Tables(0).Rows(l).Item("CONVFACTOR").ToString())

                                                        If dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim().ToUpper() = "TEMP" Then
                                                            dblValue = (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim()) * ConvFact) + 32
                                                        Else
                                                            dvConvProp.RowFilter = strFilter + "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND METRICUNIT='" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim()).ToString() + "" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim() + "_U").ToString() + "' "
                                                            dtConvProp = dvConvProp.ToTable()
                                                            If dtConvProp.Rows.Count > 0 Then
                                                                If dtConvProp.Rows(0).Item("METRICUNIT").ToString() = dtConvProp.Rows(0).Item("ENGLISHUNIT").ToString() Then
                                                                    dblValue = (1 * dtConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                                Else
                                                                    dblValue = (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim()) * dtConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                                End If

                                                            Else
                                                                dblValue = (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim()) * ConvFact)
                                                            End If
                                                        End If

                                                        If dtConvProp.Rows.Count > 0 Then
                                                            If dtConvProp.Rows(0).Item("METRICUNIT").ToString() = dtConvProp.Rows(0).Item("ENGLISHUNIT").ToString() Then
                                                                strVal = strVal + dtConvProp.Rows(0).Item("ENGLISHUNIT").ToString().Trim() + ds.Tables(0).Rows(i).Item("SEQUENCE3").ToString().Trim()
                                                            Else
                                                                strVal = strVal + FormatNumber(dblValue.ToString(), 4) + dtConvProp.Rows(0).Item("ENGLISHUNIT").ToString().Trim() + ds.Tables(0).Rows(i).Item("SEQUENCE3").ToString().Trim()
                                                            End If

                                                        Else
                                                            If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim() + "_U").ToString() = dtConv.Rows(l).Item("METRICUNIT").ToString().Trim() Then
                                                                strVal = strVal + FormatNumber(dblValue.ToString(), 4) + dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim() + ds.Tables(0).Rows(i).Item("SEQUENCE3").ToString().Trim()
                                                            End If
                                                        End If

                                                        Exit For
                                                    End If

                                                Else
                                                    If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim() + "_U").ToString() = dtConv.Rows(l).Item("METRICUNIT").ToString().Trim() Then
                                                        dvConvProp.RowFilter = strFilter + "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND METRICUNIT='" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim() + "_U").ToString() + "' "
                                                        dtConvProp = dvConvProp.ToTable()
                                                        If dtConvProp.Rows.Count > 0 Then
                                                            strVal = strVal + dtConvProp.Rows(0).Item("ENGLISHUNIT").ToString().Trim() + ds.Tables(0).Rows(i).Item("SEQUENCE3").ToString().Trim()
                                                        Else
                                                            If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim() + "_U").ToString() = dtConv.Rows(l).Item("METRICUNIT").ToString().Trim() Then
                                                                strVal = strVal + dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim() + ds.Tables(0).Rows(i).Item("SEQUENCE3").ToString().Trim()
                                                            End If
                                                        End If
                                                        'strVal = strVal + dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim() + "" + ds.Tables(0).Rows(i).Item("SEQUENCE1").ToString()
                                                        If dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim().ToUpper() = "PERCENTAGE" And ds.Tables(0).Rows(i).Item("PROPNAME").ToString().Trim().ToUpper() <> "MOLDING SHRINKAGE- FLOW" Then
                                                            If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim() + "_U").ToString() = "%" And dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim() = "in/in" Then
                                                                strVal = "%" + ds.Tables(0).Rows(i).Item("SEQUENCE3").ToString().Trim()
                                                            End If
                                                        End If

                                                        Exit For
                                                    End If

                                                End If

                                            Next

                                            If dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim() = "RH" Then
                                                strVal = strVal + " " + " RH "
                                            End If
                                            If dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim().ToUpper() = "TIME" And ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim()).ToString() <> "1" Then
                                                strVal = strVal + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim()).ToString() + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim() + "_U").ToString() + ds.Tables(0).Rows(i).Item("SEQUENCE3").ToString().Trim()
                                            ElseIf dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim().ToUpper() = "TIME" And ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim()).ToString() = "1" Then
                                                strVal = strVal + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE3").ToString().Trim() + "_U").ToString() + ds.Tables(0).Rows(i).Item("SEQUENCE3").ToString().Trim()
                                            End If
                                        End If

                                        If dtSeq.Rows(0).Item("SEQUENCE4").ToString().Trim() <> "" Then
                                            For l = 0 To dtConv.Rows.Count - 1
                                                If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE4").ToString().Trim()).ToString() <> "1" Then

                                                    If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE4").ToString().Trim() + "_U").ToString() = dtConv.Rows(l).Item("METRICUNIT").ToString().Trim() Then
                                                        ConvFact = CDbl(dsConFact.Tables(0).Rows(l).Item("CONVFACTOR").ToString())

                                                        If dtSeq.Rows(0).Item("SEQUENCE4").ToString().Trim().ToUpper() = "TEMP" Then
                                                            dblValue = (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE4").ToString().Trim()) * ConvFact) + 32
                                                        Else
                                                            dvConvProp.RowFilter = strFilter + "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND METRICUNIT='" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE4").ToString().Trim()).ToString() + "" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE4").ToString().Trim() + "_U").ToString() + "' "
                                                            dtConvProp = dvConvProp.ToTable()
                                                            If dtConvProp.Rows.Count > 0 Then
                                                                dblValue = (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE4").ToString().Trim()) * dtConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                            Else
                                                                dblValue = (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE4").ToString().Trim()) * ConvFact)
                                                            End If
                                                        End If

                                                        If dtConvProp.Rows.Count > 0 Then
                                                            If dtConvProp.Rows(0).Item("METRICUNIT").ToString() = dtConvProp.Rows(0).Item("ENGLISHUNIT").ToString() Then
                                                                strVal = strVal + dtConvProp.Rows(0).Item("ENGLISHUNIT").ToString().Trim() + ds.Tables(0).Rows(i).Item("SEQUENCE4").ToString().Trim()
                                                            Else
                                                                strVal = strVal + FormatNumber(dblValue.ToString(), 4) + dtConvProp.Rows(0).Item("ENGLISHUNIT").ToString().Trim() + ds.Tables(0).Rows(i).Item("SEQUENCE4").ToString().Trim()
                                                            End If

                                                        Else
                                                            If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE4").ToString().Trim() + "_U").ToString() = dtConv.Rows(l).Item("METRICUNIT").ToString().Trim() Then
                                                                strVal = strVal + FormatNumber(dblValue.ToString(), 4) + dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim() + ds.Tables(0).Rows(i).Item("SEQUENCE4").ToString().Trim()
                                                            End If
                                                        End If
                                                        Exit For
                                                    End If

                                                Else
                                                    If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE4").ToString().Trim() + "_U").ToString() = dtConv.Rows(l).Item("METRICUNIT").ToString().Trim() Then
                                                        dvConvProp.RowFilter = strFilter + "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND METRICUNIT='" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE4").ToString().Trim() + "_U").ToString() + "' "
                                                        dtConvProp = dvConvProp.ToTable()
                                                        If dtConvProp.Rows.Count > 0 Then
                                                            strVal = strVal + dtConvProp.Rows(0).Item("ENGLISHUNIT").ToString().Trim() + ds.Tables(0).Rows(i).Item("SEQUENCE4").ToString().Trim()
                                                        Else
                                                            If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE4").ToString().Trim() + "_U").ToString() = dtConv.Rows(l).Item("METRICUNIT").ToString().Trim() Then
                                                                strVal = strVal + dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim() + ds.Tables(0).Rows(i).Item("SEQUENCE4").ToString().Trim()
                                                            End If
                                                        End If

                                                        'strVal = strVal + dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim() + "" + ds.Tables(0).Rows(i).Item("SEQUENCE1").ToString()
                                                        If dtSeq.Rows(0).Item("SEQUENCE4").ToString().Trim().ToUpper() = "PERCENTAGE" And ds.Tables(0).Rows(i).Item("PROPNAME").ToString().Trim().ToUpper() <> "MOLDING SHRINKAGE- FLOW" Then
                                                            If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE4").ToString().Trim() + "_U").ToString() = "%" And dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim() = "in/in" Then
                                                                strVal = "%" + ds.Tables(0).Rows(i).Item("SEQUENCE4").ToString().Trim()
                                                            End If
                                                        End If
                                                        Exit For
                                                    End If

                                                End If
                                            Next

                                            If dtSeq.Rows(0).Item("SEQUENCE4").ToString().Trim() = "RH" Then
                                                strVal = strVal + " " + " RH "
                                            End If

                                            If dtSeq.Rows(0).Item("SEQUENCE4").ToString().Trim().ToUpper() = "TIME" And ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE4").ToString().Trim()).ToString() <> "1" Then
                                                strVal = strVal + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE4").ToString().Trim()).ToString() + "" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE4").ToString().Trim() + "_U").ToString() + ds.Tables(0).Rows(i).Item("SEQUENCE4").ToString().Trim()
                                            ElseIf dtSeq.Rows(0).Item("SEQUENCE4").ToString().Trim().ToUpper() = "TIME" And ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE4").ToString().Trim()).ToString() = "1" Then
                                                strVal = strVal + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE4").ToString().Trim() + "_U").ToString() + ds.Tables(0).Rows(i).Item("SEQUENCE4").ToString().Trim()
                                            End If
                                        End If

                                        If dtSeq.Rows(0).Item("SEQUENCE5").ToString().Trim() <> "" Then
                                            For l = 0 To dtConv.Rows.Count - 1
                                                If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE5").ToString().Trim()).ToString() <> "1" Then

                                                    If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE5").ToString().Trim() + "_U").ToString() = dtConv.Rows(l).Item("METRICUNIT").ToString().Trim() Then
                                                        ConvFact = CDbl(dsConFact.Tables(0).Rows(l).Item("CONVFACTOR").ToString())

                                                        If dtSeq.Rows(0).Item("SEQUENCE5").ToString().Trim().ToUpper() = "TEMP" Then
                                                            dblValue = (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE5").ToString().Trim()) * ConvFact) + 32
                                                        Else
                                                            dvConvProp.RowFilter = strFilter + "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND METRICUNIT='" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE5").ToString().Trim() + "_U").ToString() + "' "
                                                            dtConvProp = dvConvProp.ToTable()
                                                            If dtConvProp.Rows.Count > 0 Then
                                                                dblValue = (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE5").ToString().Trim()) * dtConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                            Else
                                                                dblValue = (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE5").ToString().Trim()) * ConvFact)
                                                            End If
                                                        End If

                                                        If dtConvProp.Rows.Count > 0 Then
                                                            strVal = strVal + FormatNumber(dblValue.ToString(), 4) + dtConvProp.Rows(0).Item("ENGLISHUNIT").ToString().Trim() + ds.Tables(0).Rows(i).Item("SEQUENCE5").ToString().Trim()
                                                        Else
                                                            If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE5").ToString().Trim() + "_U").ToString() = dtConv.Rows(l).Item("METRICUNIT").ToString().Trim() Then
                                                                strVal = strVal + FormatNumber(dblValue.ToString(), 4) + dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim() + ds.Tables(0).Rows(i).Item("SEQUENCE5").ToString().Trim()
                                                            End If
                                                        End If
                                                        Exit For
                                                    End If

                                                Else
                                                    If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE5").ToString().Trim() + "_U").ToString() = dtConv.Rows(l).Item("METRICUNIT").ToString().Trim() Then
                                                        dvConvProp.RowFilter = strFilter + "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND METRICUNIT='" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE5").ToString().Trim() + "_U").ToString() + "' "
                                                        dtConvProp = dvConvProp.ToTable()
                                                        If dtConvProp.Rows.Count > 0 Then
                                                            strVal = strVal + dtConvProp.Rows(0).Item("ENGLISHUNIT").ToString().Trim() + ds.Tables(0).Rows(i).Item("SEQUENCE5").ToString().Trim()
                                                        Else
                                                            If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE5").ToString().Trim() + "_U").ToString() = dtConv.Rows(l).Item("METRICUNIT").ToString().Trim() Then
                                                                strVal = strVal + dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim() + ds.Tables(0).Rows(i).Item("SEQUENCE5").ToString().Trim()
                                                            End If
                                                        End If

                                                        'strVal = strVal + dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim() + "" + ds.Tables(0).Rows(i).Item("SEQUENCE1").ToString()
                                                        If dtSeq.Rows(0).Item("SEQUENCE5").ToString().Trim().ToUpper() = "PERCENTAGE" And ds.Tables(0).Rows(i).Item("PROPNAME").ToString().Trim().ToUpper() <> "MOLDING SHRINKAGE- FLOW" Then
                                                            If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE5").ToString().Trim() + "_U").ToString() = "%" And dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim() = "in/in" Then
                                                                strVal = ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE5").ToString().Trim() + "_U").ToString() + ds.Tables(0).Rows(i).Item("SEQUENCE5").ToString().Trim()
                                                            End If
                                                        End If
                                                        Exit For
                                                    End If

                                                End If
                                            Next
                                            If dtSeq.Rows(0).Item("SEQUENCE5").ToString().Trim() = "RH" Then
                                                strVal = strVal + " " + " RH "
                                            End If
                                            If dtSeq.Rows(0).Item("SEQUENCE5").ToString().Trim().ToUpper() = "TIME" And ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE5").ToString().Trim()).ToString() <> "1" Then
                                                strVal = strVal + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE5").ToString().Trim()).ToString() + "" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE5").ToString().Trim() + "_U").ToString() + ds.Tables(0).Rows(i).Item("SEQUENCE5").ToString().Trim()
                                            ElseIf dtSeq.Rows(0).Item("SEQUENCE5").ToString().Trim().ToUpper() = "TIME" And ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE5").ToString().Trim()).ToString() = "1" Then
                                                strVal = strVal + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE5").ToString().Trim() + "_U").ToString() + ds.Tables(0).Rows(i).Item("SEQUENCE5").ToString().Trim()
                                            End If
                                        End If

                                        If dtSeq.Rows(0).Item("SEQUENCE6").ToString().Trim() <> "" Then
                                            For l = 0 To dtConv.Rows.Count - 1
                                                If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE6").ToString().Trim()).ToString() <> "1" Then

                                                    If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE6").ToString().Trim() + "_U").ToString() = dtConv.Rows(l).Item("METRICUNIT").ToString().Trim() Then
                                                        ConvFact = CDbl(dsConFact.Tables(0).Rows(l).Item("CONVFACTOR").ToString())

                                                        If dtSeq.Rows(0).Item("SEQUENCE6").ToString().Trim().ToUpper() = "TEMP" Then
                                                            dblValue = (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE6").ToString().Trim()) * ConvFact) + 32
                                                        Else
                                                            dvConvProp.RowFilter = strFilter + "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND METRICUNIT='" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE6").ToString().Trim() + "_U").ToString() + "' "
                                                            dtConvProp = dvConvProp.ToTable()
                                                            If dtConvProp.Rows.Count > 0 Then
                                                                dblValue = (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE6").ToString().Trim()) * dtConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                            Else
                                                                dblValue = (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE6").ToString().Trim()) * ConvFact)
                                                            End If
                                                        End If

                                                        If dtConvProp.Rows.Count > 0 Then
                                                            strVal = strVal + FormatNumber(dblValue.ToString(), 4) + dtConvProp.Rows(0).Item("ENGLISHUNIT").ToString().Trim() + ds.Tables(0).Rows(i).Item("SEQUENCE6").ToString().Trim()
                                                        Else
                                                            If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE6").ToString().Trim() + "_U").ToString() = dtConv.Rows(l).Item("METRICUNIT").ToString().Trim() Then
                                                                strVal = strVal + FormatNumber(dblValue.ToString(), 4) + dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim() + ds.Tables(0).Rows(i).Item("SEQUENCE6").ToString().Trim()
                                                            End If
                                                        End If
                                                        Exit For
                                                    End If

                                                Else
                                                    If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE6").ToString().Trim() + "_U").ToString() = dtConv.Rows(l).Item("METRICUNIT").ToString().Trim() Then
                                                        dvConvProp.RowFilter = strFilter + "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND METRICUNIT='" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE6").ToString().Trim() + "_U").ToString() + "' "
                                                        dtConvProp = dvConvProp.ToTable()
                                                        If dtConvProp.Rows.Count > 0 Then
                                                            strVal = strVal + dtConvProp.Rows(0).Item("ENGLISHUNIT").ToString().Trim() + ds.Tables(0).Rows(i).Item("SEQUENCE6").ToString().Trim()
                                                        Else
                                                            If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE6").ToString().Trim() + "_U").ToString() = dtConv.Rows(l).Item("METRICUNIT").ToString().Trim() Then
                                                                strVal = strVal + dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim() + ds.Tables(0).Rows(i).Item("SEQUENCE6").ToString().Trim()
                                                            End If
                                                        End If

                                                        'strVal = strVal + dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim() + "" + ds.Tables(0).Rows(i).Item("SEQUENCE1").ToString()
                                                        If dtSeq.Rows(0).Item("SEQUENCE6").ToString().Trim().ToUpper() = "PERCENTAGE" And ds.Tables(0).Rows(i).Item("PROPNAME").ToString().Trim().ToUpper() <> "MOLDING SHRINKAGE- FLOW" Then
                                                            If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE6").ToString().Trim() + "_U").ToString() = "%" And dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim() = "in/in" Then
                                                                strVal = ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE6").ToString().Trim() + "_U").ToString() + ds.Tables(0).Rows(i).Item("SEQUENCE6").ToString().Trim()
                                                            End If
                                                        End If
                                                        Exit For
                                                    End If

                                                End If
                                            Next
                                            If dtSeq.Rows(0).Item("SEQUENCE6").ToString().Trim() = "RH" Then
                                                strVal = strVal + " " + " RH "
                                            End If
                                            'If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE6").ToString().Trim() + "_U").ToString() = "bar" Then
                                            '    strVal = strVal + "bar"
                                            'End If
                                            'If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE6").ToString().Trim() + "_U").ToString() = "atm" Then
                                            '    strVal = strVal + "atm" + ds.Tables(0).Rows(i).Item("SEQUENCE6").ToString()
                                            'End If
                                            If dtSeq.Rows(0).Item("SEQUENCE6").ToString().Trim().ToUpper() = "TIME" And ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE6").ToString().Trim()).ToString() <> "1" Then
                                                strVal = strVal + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE6").ToString().Trim()).ToString() + "" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE6").ToString().Trim() + "_U").ToString() + ds.Tables(0).Rows(i).Item("SEQUENCE6").ToString().Trim()
                                            ElseIf dtSeq.Rows(0).Item("SEQUENCE6").ToString().Trim().ToUpper() = "TIME" And ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE6")).ToString() = "1" Then
                                                strVal = strVal + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE6").ToString().Trim() + "_U").ToString() + ds.Tables(0).Rows(i).Item("SEQUENCE6").ToString().Trim()
                                            End If
                                        End If
                                    End If

                                    lbl.Text = strVal.ToString()
                                    tdInner.Controls.Add(lbl)
                                    trInner.Controls.Add(tdInner)
                                Case 8
                                    InnerTdSetting(tdInner, "", "Left")
                                    lbl = New Label
                                    Dim strVal As String = ""
                                    Dim dblValue As Double
                                    Dim ConvFact As String
                                    Dim strSpValue As String
                                    Dim strSpValue1 As String
                                    Dim strSpValue2 As String
                                    Dim strSpChar As String = ""
                                    Dim strSpChar1 As String = ""
                                    Dim strArr() As String
                                    Dim strArr1() As String
                                    Dim str As String
                                    Dim Newstr As String = String.Empty

                                    Dim c As Integer
                                    dtConv = dvConv.ToTable()
                                    dvSeq.RowFilter = "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND SEQUENCE=" + ds.Tables(0).Rows(i).Item("SEQUENCE").ToString()
                                    dtSeq = dvSeq.ToTable()

                                    If dtSeq.Rows.Count > 0 Then
                                        If dtSeq.Rows(0).Item("TSEQUENCE1").ToString().Trim() <> "" Then
                                            If ds.Tables(0).Rows(i).Item("TSEQUENCE1").ToString().Trim() = "@" Then
                                                strVal = "@"
                                            End If
                                            If ds.Tables(0).Rows(i).Item("TSEQUENCE1").ToString().Trim() = "at" Then
                                                strVal = "at"
                                            End If
                                            For l = 0 To dtConv.Rows.Count - 1
                                                If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE1")).ToString() <> "1" Then

                                                    If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE1").ToString().Trim() + "_U").ToString() = dtConv.Rows(l).Item("METRICUNIT").ToString().Trim() Then
                                                        ConvFact = CDbl(dsConFact.Tables(0).Rows(l).Item("CONVFACTOR").ToString())
                                                        str = ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE1").ToString().Trim())
                                                        If dtSeq.Rows(0).Item("TSEQUENCE1").ToString().Trim().ToUpper() = "TTEMP" Then
                                                            If str.Contains("(") Then
                                                                strArr = str.Split("(")
                                                                For c = 0 To strArr.Length
                                                                    If c < strArr.Length Then

                                                                        dvTestConvProp.RowFilter = strFilter + "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND METRICUNIT='" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE1").ToString().Trim() + "_U").ToString() + "' "
                                                                        dtTestConvProp = dvTestConvProp.ToTable()
                                                                        If dtTestConvProp.Rows.Count > 0 Then
                                                                            strSpValue = strArr(c)
                                                                            Dim Newstr1 As String = System.Text.RegularExpressions.Regex.Replace(strSpValue, "[^\d\.]", "")
                                                                            dblValue = CDbl(Newstr1) * (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim()) * dtTestConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                                            strVal = FormatNumber(dblValue.ToString(), 0) + "("
                                                                            c = c + 1
                                                                            strSpValue = strArr(c)
                                                                            strArr1 = strSpValue.Split("-")
                                                                            strSpValue1 = strArr1(0)
                                                                            Dim Newstr2 As String = System.Text.RegularExpressions.Regex.Replace(strSpValue1, "[^\d\.]", "")
                                                                            dblValue = CDbl(Newstr2) * (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim()) * dtTestConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                                            strVal = strVal + FormatNumber(dblValue.ToString(), 0) + "-"
                                                                            strSpValue2 = strArr1(1)
                                                                            Dim Newstr3 As String = System.Text.RegularExpressions.Regex.Replace(strSpValue2, "[^\d\.]", "")
                                                                            dblValue = CDbl(Newstr3) * (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim()) * dtTestConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                                            strVal = strVal + FormatNumber(dblValue.ToString(), 0) + ")"
                                                                        Else
                                                                            strSpValue = strArr(c)
                                                                            Dim Newstr1 As String = System.Text.RegularExpressions.Regex.Replace(strSpValue, "[^\d\.]", "")
                                                                            dblValue = CDbl(Newstr1) * (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim()) * ConvFact) + 32
                                                                            strVal = FormatNumber(dblValue.ToString(), 0) + "("
                                                                            c = c + 1
                                                                            strSpValue = strArr(c)
                                                                            strArr1 = strSpValue.Split("-")
                                                                            strSpValue1 = strArr1(0)
                                                                            Dim Newstr2 As String = System.Text.RegularExpressions.Regex.Replace(strSpValue1, "[^\d\.]", "")
                                                                            dblValue = CDbl(Newstr2) * (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim()) * ConvFact) + 32
                                                                            strVal = strVal + FormatNumber(dblValue.ToString(), 0) + "-"
                                                                            strSpValue2 = strArr1(1)
                                                                            Dim Newstr3 As String = System.Text.RegularExpressions.Regex.Replace(strSpValue2, "[^\d\.]", "")
                                                                            dblValue = CDbl(Newstr3) * (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim()) * ConvFact) + 32
                                                                            strVal = strVal + FormatNumber(dblValue.ToString(), 0) + ")"
                                                                        End If
                                                                        
                                                                    End If

                                                                Next
                                                            ElseIf str.Contains("to") Then
                                                                strArr = str.Split("to")
                                                                For c = 0 To strArr.Length
                                                                    If c < strArr.Length Then
                                                                        strSpValue = strArr(0)

                                                                        dvTestConvProp.RowFilter = strFilter + "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND METRICUNIT='" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE1").ToString().Trim() + "_U").ToString() + "' "
                                                                        dtTestConvProp = dvTestConvProp.ToTable()
                                                                        If dtTestConvProp.Rows.Count > 0 Then
                                                                            Dim Newstr1 As String = System.Text.RegularExpressions.Regex.Replace(strSpValue, "[^\d\.]", "")
                                                                            dblValue = CDbl(Newstr1) * (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim()) * dtTestConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                                            strVal = FormatNumber(dblValue.ToString(), 0)
                                                                            c = c + 1
                                                                            strSpValue2 = strArr(1)
                                                                            Dim Newstr3 As String = System.Text.RegularExpressions.Regex.Replace(strSpValue2, "[^\d\.]", "")
                                                                            dblValue = CDbl(Newstr3) * (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim()) * dtTestConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                                            strVal = strVal + " to " + FormatNumber(dblValue.ToString(), 0)
                                                                        Else
                                                                            Dim Newstr1 As String = System.Text.RegularExpressions.Regex.Replace(strSpValue, "[^\d\.]", "")
                                                                            dblValue = CDbl(Newstr1) * (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim()) * ConvFact) + 32
                                                                            strVal = FormatNumber(dblValue.ToString(), 0)
                                                                            c = c + 1
                                                                            strSpValue2 = strArr(1)
                                                                            Dim Newstr3 As String = System.Text.RegularExpressions.Regex.Replace(strSpValue2, "[^\d\.]", "")
                                                                            dblValue = CDbl(Newstr3) * (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("SEQUENCE1").ToString().Trim()) * ConvFact) + 32
                                                                            strVal = strVal + " to " + FormatNumber(dblValue.ToString(), 0)
                                                                        End If
                                                                        
                                                                    End If

                                                                Next
                                                                If dtTestConvProp.Rows.Count > 0 Then
                                                                    strVal = strVal + "" + dtTestConvProp.Rows(0).Item("ENGLISHUNIT").ToString().Trim()
                                                                Else
                                                                    strVal = strVal + "" + dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim()
                                                                End If

                                                            Else
                                                                dvTestConvProp.RowFilter = strFilter + "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND METRICUNIT='" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE1").ToString().Trim() + "_U").ToString() + "' "
                                                                dtTestConvProp = dvTestConvProp.ToTable()
                                                                If dtTestConvProp.Rows.Count > 0 Then
                                                                    dblValue = (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE1").ToString().Trim()) * dtTestConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                                    strVal = strVal + FormatNumber(dblValue.ToString(), 0) + dtTestConvProp.Rows(0).Item("ENGLISHUNIT").ToString().Trim()
                                                                Else
                                                                    dblValue = CDbl(ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE1").ToString().Trim())) * ConvFact + 32
                                                                    strVal = strVal + FormatNumber(dblValue.ToString(), 0) + dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim()
                                                                End If
                                                                'dblValue = CDbl(ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE1").ToString().Trim())) * ConvFact + 32
                                                                'strVal = strVal + FormatNumber(dblValue.ToString(), 0) + dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim()
                                                            End If

                                                        ElseIf dtSeq.Rows(0).Item("TSEQUENCE1").ToString().Trim().ToUpper() = "TRH" Then
                                                            strVal = strVal + "" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE1").ToString().Trim()) + "" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE1").ToString().Trim() + "_U").ToString()

                                                        Else
                                                            dvTestConvProp.RowFilter = strFilter + "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND METRICUNIT='" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE1").ToString().Trim() + "_U").ToString() + "' "
                                                            dtTestConvProp = dvTestConvProp.ToTable()
                                                            If dtTestConvProp.Rows.Count > 0 Then
                                                                dblValue = (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE1").ToString().Trim()) * dtTestConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                                strVal = strVal + FormatNumber(dblValue.ToString(), 1) + dtTestConvProp.Rows(0).Item("ENGLISHUNIT").ToString().Trim()
                                                            Else
                                                                dblValue = CDbl(ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE1").ToString().Trim()) * (ConvFact))
                                                                strVal = strVal + FormatNumber(dblValue.ToString(), 3) + dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim()
                                                            End If
                                                            'dblValue = CDbl(ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE1").ToString().Trim()) * (ConvFact))
                                                            'strVal = strVal + FormatNumber(dblValue.ToString(), 3) + dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim()
                                                        End If

                                                        Exit For
                                                    End If

                                                Else
                                                    If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE1").ToString().Trim() + "_U").ToString() = dtConv.Rows(l).Item("METRICUNIT").ToString().Trim() Then
                                                        ConvFact = CDbl(dsConFact.Tables(0).Rows(l).Item("CONVFACTOR").ToString())
                                                        If dtSeq.Rows(0).Item("TSEQUENCE1").ToString().Trim().ToUpper() = "TTEMP" Then
                                                            dvTestConvProp.RowFilter = strFilter + "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND METRICUNIT='" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE1").ToString().Trim() + "_U").ToString() + "' "
                                                            dtTestConvProp = dvTestConvProp.ToTable()
                                                            If dtTestConvProp.Rows.Count > 0 Then
                                                                dblValue = (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE1").ToString().Trim()) * dtTestConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                                strVal = strVal + FormatNumber(dblValue.ToString(), 0) + dtTestConvProp.Rows(0).Item("ENGLISHUNIT").ToString().Trim()
                                                            Else
                                                                dblValue = CDbl(ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE1").ToString().Trim())) * ConvFact + 32
                                                                strVal = strVal + FormatNumber(dblValue.ToString(), 0) + " " + dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim()
                                                            End If
                                                            'dblValue = CDbl(ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE1").ToString().Trim())) * ConvFact + 32
                                                            'strVal = strVal + FormatNumber(dblValue.ToString(), 0) + " " + dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim()
                                                        Else
                                                            dvTestConvProp.RowFilter = strFilter + "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND METRICUNIT='" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE1").ToString().Trim() + "_U").ToString() + "' "
                                                            dtTestConvProp = dvConvProp.ToTable()
                                                            If dtTestConvProp.Rows.Count > 0 Then
                                                                dblValue = (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE1").ToString().Trim()) * dtTestConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                                strVal = strVal + FormatNumber(dblValue.ToString(), 3) + dtTestConvProp.Rows(0).Item("ENGLISHUNIT").ToString().Trim()
                                                            Else
                                                                dblValue = CDbl(ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE1").ToString().Trim()) * (ConvFact))
                                                                strVal = strVal + FormatNumber(dblValue.ToString(), 3) + " " + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE1").ToString().Trim() + "_U").ToString()
                                                            End If
                                                            'dblValue = CDbl(ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE1").ToString().Trim()) * (ConvFact))
                                                            'strVal = strVal + FormatNumber(dblValue.ToString(), 3) + " " + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE1").ToString().Trim() + "_U").ToString()
                                                        End If

                                                        Exit For
                                                    End If

                                                End If
                                            Next
                                            If dtSeq.Rows(0).Item("TSEQUENCE1").ToString().Trim() = "TRH" Then
                                                strVal = strVal + " " + " RH "
                                            End If

                                            If dtSeq.Rows(0).Item("TSEQUENCE1").ToString().Trim().ToUpper() = "TTIME" And ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE1")).ToString().Trim() <> "1" Then
                                                strVal = strVal + ds.Tables(0).Rows(i).Item("TSEQUENCE1").ToString() + " " + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE1")).ToString() + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE1").ToString() + "_U").ToString()
                                            ElseIf dtSeq.Rows(0).Item("TSEQUENCE1").ToString().Trim().ToUpper() = "TTIME" And ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE1")).ToString().Trim() = "1" Then
                                                strVal = strVal + ds.Tables(0).Rows(i).Item("TSEQUENCE1").ToString().Trim() + " " + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE1").ToString().Trim() + "_U").ToString()
                                            End If
                                            If dtSeq.Rows(0).Item("TSEQUENCE1").ToString().Trim().ToUpper() = "TANGLE" Then
                                                strVal = ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE1").ToString().Trim()).ToString() + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE1").ToString().Trim() + "_U").ToString()

                                            End If

                                        End If
                                        If dtSeq.Rows(0).Item("TSEQUENCE2").ToString().Trim() <> "" Then
                                            If dtSeq.Rows(0).Item("TSEQUENCE2").ToString().Trim().ToUpper() <> "TRH" Then
                                                For l = 0 To dtConv.Rows.Count - 1
                                                    If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE2")).ToString() <> "1" Then

                                                        If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE2").ToString().Trim() + "_U").ToString() = dtConv.Rows(l).Item("METRICUNIT").ToString().Trim() Then
                                                            ConvFact = CDbl(dsConFact.Tables(0).Rows(l).Item("CONVFACTOR").ToString())
                                                            If dtSeq.Rows(0).Item("TSEQUENCE2").ToString().Trim().ToUpper() = "TTEMP" Then
                                                                dvTestConvProp.RowFilter = strFilter + "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND METRICUNIT='" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE2").ToString().Trim() + "_U").ToString() + "' "
                                                                dtTestConvProp = dvTestConvProp.ToTable()
                                                                If dtTestConvProp.Rows.Count > 0 Then
                                                                    dblValue = (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE2").ToString().Trim()) * dtTestConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                                    strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE2").ToString() + " " + FormatNumber(dblValue.ToString(), 0) + "" + dtTestConvProp.Rows(0).Item("ENGLISHUNIT").ToString().Trim()
                                                                Else
                                                                    dblValue = CDbl(ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE2").ToString().Trim())) * ConvFact + 32
                                                                    strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE2").ToString() + " " + FormatNumber(dblValue.ToString(), 0) + "" + dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim()
                                                                End If
                                                                'dblValue = CDbl(ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE2").ToString().Trim())) * ConvFact + 32
                                                                'strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE2").ToString() + " " + FormatNumber(dblValue.ToString(), 0) + "" + dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim()
                                                            Else
                                                                dvTestConvProp.RowFilter = strFilter + "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND METRICUNIT='" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE2").ToString().Trim() + "_U").ToString() + "' "
                                                                dtTestConvProp = dvTestConvProp.ToTable()
                                                                If dtTestConvProp.Rows.Count > 0 Then
                                                                    dblValue = (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE2").ToString().Trim()) * dtTestConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                                    strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE2").ToString().Trim() + " " + FormatNumber(dblValue.ToString(), 3) + "" + dtTestConvProp.Rows(0).Item("ENGLISHUNIT").ToString().Trim()
                                                                Else
                                                                    dblValue = CDbl(ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE2").ToString().Trim()) * (ConvFact))
                                                                    strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE2").ToString().Trim() + " " + FormatNumber(dblValue.ToString(), 3) + "" + dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim()
                                                                End If
                                                                'dblValue = CDbl(ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE2").ToString().Trim()) * (ConvFact))
                                                                'strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE2").ToString().Trim() + " " + FormatNumber(dblValue.ToString(), 3) + "" + dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim()
                                                            End If

                                                            Exit For
                                                        End If

                                                    Else
                                                        If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE2").ToString().Trim() + "_U").ToString() = dtConv.Rows(l).Item("METRICUNIT").ToString().Trim() Then
                                                            ConvFact = CDbl(dsConFact.Tables(0).Rows(l).Item("CONVFACTOR").ToString())
                                                            If dtSeq.Rows(0).Item("TSEQUENCE2").ToString().Trim().ToUpper() = "TTEMP" Then
                                                                dvTestConvProp.RowFilter = strFilter + "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND METRICUNIT='" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE2").ToString().Trim() + "_U").ToString() + "' "
                                                                dtTestConvProp = dvTestConvProp.ToTable()
                                                                If dtTestConvProp.Rows.Count > 0 Then
                                                                    dblValue = (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE2").ToString().Trim()) * dtTestConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                                    strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE2").ToString() + " " + FormatNumber(dblValue.ToString(), 0) + "" + dtTestConvProp.Rows(0).Item("ENGLISHUNIT").ToString().Trim()
                                                                Else
                                                                    dblValue = CDbl(ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE2").ToString().Trim())) * ConvFact + 32
                                                                    strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE2").ToString() + " " + FormatNumber(dblValue.ToString(), 0) + "" + dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim()
                                                                End If
                                                                'dblValue = CDbl(ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE2").ToString().Trim())) * ConvFact + 32
                                                                'strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE2").ToString().Trim() + FormatNumber(dblValue.ToString(), 0) + "" + dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim()
                                                            Else
                                                                dvTestConvProp.RowFilter = strFilter + "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND METRICUNIT='" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE2").ToString().Trim() + "_U").ToString() + "' "
                                                                dtTestConvProp = dvTestConvProp.ToTable()
                                                                If dtTestConvProp.Rows.Count > 0 Then
                                                                    dblValue = (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE2").ToString().Trim()) * dtTestConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                                    strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE2").ToString().Trim() + " " + FormatNumber(dblValue.ToString(), 3) + "" + dtTestConvProp.Rows(0).Item("ENGLISHUNIT").ToString().Trim()
                                                                Else
                                                                    dblValue = CDbl(ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE2").ToString().Trim()) * (ConvFact))
                                                                    strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE2").ToString() + " " + FormatNumber(dblValue.ToString(), 3) + "" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE2").ToString().Trim() + "_U").ToString()
                                                                End If
                                                                'dblValue = CDbl(ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE2").ToString().Trim()) * (ConvFact))
                                                                'strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE2").ToString() + " " + FormatNumber(dblValue.ToString(), 3) + "" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE2").ToString().Trim() + "_U").ToString()
                                                            End If

                                                            Exit For
                                                        End If

                                                    End If
                                                    If dtSeq.Rows(0).Item("TSEQUENCE2").ToString().Trim().ToUpper() = "TPERCENTAGE" Then
                                                        If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE2").ToString().Trim() + "_U").ToString() = "%" And dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim() = "in/in" Then
                                                        Else

                                                            strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE2").ToString().Trim() + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE2").ToString().Trim()) + "%"
                                                        End If
                                                        Exit For
                                                    End If

                                                Next

                                            Else

                                                strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE2").ToString().Trim() + " " + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE2")).ToString().Trim() + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE2").ToString().Trim() + "_U").ToString()

                                            End If
                                            If ds.Tables(0).Rows(i).Item("TSEQUENCE2").ToString().Trim() = "@" Then
                                                strVal = strVal + " " + "@"
                                            End If
                                            If dtSeq.Rows(0).Item("TSEQUENCE2").ToString().Trim() = "TRH" Then
                                                strVal = strVal + " " + " RH "
                                            End If
                                            If dtSeq.Rows(0).Item("TSEQUENCE2").ToString().Trim().ToUpper() = "TTIME" And ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE2").ToString().Trim()).ToString() <> "1" Then
                                                strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE2").ToString().Trim() + " " + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE2").ToString().Trim()).ToString() + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE2").ToString().Trim() + "_U").ToString()
                                            ElseIf dtSeq.Rows(0).Item("TSEQUENCE2").ToString().Trim().ToUpper() = "TTIME" And ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE2").ToString().Trim()).ToString() = "1" Then
                                                strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE2").ToString().Trim() + " " + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE2").ToString().Trim() + "_U").ToString()
                                            End If
                                            If dtSeq.Rows(0).Item("TSEQUENCE2").ToString().Trim().ToUpper() = "TANGLE" Then
                                                strVal = ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE2").ToString().Trim()).ToString() + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE2").ToString().Trim() + "_U").ToString()

                                            End If
                                        End If

                                        If dtSeq.Rows(0).Item("TSEQUENCE3").ToString().Trim() <> "" Then
                                            If dtSeq.Rows(0).Item("TSEQUENCE3").ToString().Trim().ToUpper() <> "TRH" Then
                                                For l = 0 To dtConv.Rows.Count - 1
                                                    If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE3").ToString().Trim()).ToString() <> "1" Then

                                                        If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE3").ToString().Trim() + "_U").ToString() = dtConv.Rows(l).Item("METRICUNIT").ToString().Trim() Then
                                                            ConvFact = CDbl(dsConFact.Tables(0).Rows(l).Item("CONVFACTOR").ToString())
                                                            If dtSeq.Rows(0).Item("TSEQUENCE3").ToString().Trim().ToUpper() = "TTEMP" Then
                                                                dvTestConvProp.RowFilter = strFilter + "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND METRICUNIT='" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE3").ToString().Trim() + "_U").ToString() + "' "
                                                                dtTestConvProp = dvTestConvProp.ToTable()
                                                                If dtConvProp.Rows.Count > 0 Then
                                                                    dblValue = (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE3").ToString().Trim()) * dtTestConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                                    strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE3").ToString() + " " + FormatNumber(dblValue.ToString(), 0) + "" + dtTestConvProp.Rows(0).Item("ENGLISHUNIT").ToString().Trim()
                                                                Else
                                                                    dblValue = CDbl(ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE3").ToString().Trim())) * ConvFact + 32
                                                                    strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE3").ToString().Trim() + " " + FormatNumber(dblValue.ToString(), 0) + "" + dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim()
                                                                End If
                                                                'dblValue = CDbl(ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE3").ToString().Trim())) * ConvFact + 32
                                                                'strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE3").ToString().Trim() + " " + FormatNumber(dblValue.ToString(), 0) + "" + dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim()
                                                            Else
                                                                dvTestConvProp.RowFilter = strFilter + "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND METRICUNIT='" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE3").ToString().Trim() + "_U").ToString() + "' "
                                                                dtTestConvProp = dvTestConvProp.ToTable()
                                                                If dtConvProp.Rows.Count > 0 Then
                                                                    dblValue = (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE3").ToString().Trim()) * dtTestConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                                    strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE3").ToString().Trim() + " " + FormatNumber(dblValue.ToString(), 3) + "" + dtTestConvProp.Rows(0).Item("ENGLISHUNIT").ToString().Trim()
                                                                Else
                                                                    dblValue = CDbl(ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE3").ToString().Trim()) * (ConvFact))
                                                                    strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE3").ToString().Trim() + " " + FormatNumber(dblValue.ToString(), 3) + "" + dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim()
                                                                End If
                                                                'dblValue = CDbl(ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE3").ToString().Trim()) * (ConvFact))
                                                                'strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE3").ToString().Trim() + " " + FormatNumber(dblValue.ToString(), 3) + "" + dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim()
                                                            End If

                                                            Exit For
                                                        End If

                                                    Else
                                                        If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE3").ToString().Trim() + "_U").ToString() = dtConv.Rows(l).Item("METRICUNIT").ToString().Trim() Then
                                                            ConvFact = CDbl(dsConFact.Tables(0).Rows(l).Item("CONVFACTOR").ToString())
                                                            If dtSeq.Rows(0).Item("TSEQUENCE3").ToString().Trim().ToUpper() = "TTEMP" Then
                                                                dvTestConvProp.RowFilter = strFilter + "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND METRICUNIT='" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE3").ToString().Trim() + "_U").ToString() + "' "
                                                                dtTestConvProp = dvTestConvProp.ToTable()
                                                                If dtTestConvProp.Rows.Count > 0 Then
                                                                    dblValue = (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE3").ToString().Trim()) * dtTestConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                                    strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE3").ToString().Trim() + " " + FormatNumber(dblValue.ToString(), 0) + "" + dtTestConvProp.Rows(0).Item("ENGLISHUNIT").ToString().Trim()
                                                                Else
                                                                    dblValue = CDbl(ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE3").ToString().Trim())) * ConvFact + 32
                                                                    strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE3").ToString().Trim() + " " + FormatNumber(dblValue.ToString(), 0) + dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim()
                                                                End If
                                                                'dblValue = CDbl(ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE3").ToString().Trim())) * ConvFact + 32
                                                                'strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE3").ToString().Trim() + " " + FormatNumber(dblValue.ToString(), 0) + dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim()
                                                            Else
                                                                dvTestConvProp.RowFilter = strFilter + "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND METRICUNIT='" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE3").ToString().Trim() + "_U").ToString() + "' "
                                                                dtTestConvProp = dvTestConvProp.ToTable()
                                                                If dtTestConvProp.Rows.Count > 0 Then
                                                                    dblValue = (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE3").ToString().Trim()) * dtTestConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                                    strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE3").ToString().Trim() + " " + FormatNumber(dblValue.ToString(), 3) + "" + dtTestConvProp.Rows(0).Item("ENGLISHUNIT").ToString().Trim()
                                                                Else
                                                                    dblValue = CDbl(ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE3").ToString().Trim()) * (ConvFact))
                                                                    strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE3").ToString().Trim() + " " + FormatNumber(dblValue.ToString(), 3) + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE3").ToString().Trim() + "_U").ToString()
                                                                End If
                                                                'dblValue = CDbl(ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE3").ToString().Trim()) * (ConvFact))
                                                                'strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE3").ToString().Trim() + " " + FormatNumber(dblValue.ToString(), 3) + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE3").ToString().Trim() + "_U").ToString()
                                                            End If

                                                                Exit For
                                                            End If

                                                    End If
                                                    If dtSeq.Rows(0).Item("TSEQUENCE3").ToString().Trim().ToUpper() = "TPERCENTAGE" Then
                                                        If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE3").ToString().Trim() + "_U").ToString() = "%" And dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim() = "in/in" Then
                                                        Else

                                                            strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE3").ToString().Trim() + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE3").ToString().Trim()) + "%"
                                                        End If
                                                        Exit For
                                                    End If

                                                Next

                                            Else

                                                strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE3").ToString().Trim() + " " + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE3").ToString().Trim()).ToString() + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE3").ToString().Trim() + "_U").ToString()

                                            End If
                                            If ds.Tables(0).Rows(i).Item("TSEQUENCE3").ToString().Trim() = "@" Then
                                                strVal = strVal + " " + "@"
                                            End If
                                            If dtSeq.Rows(0).Item("TSEQUENCE3").ToString().Trim() = "TRH" Then
                                                strVal = strVal + " " + " RH "
                                            End If
                                            If dtSeq.Rows(0).Item("TSEQUENCE3").ToString().Trim().ToUpper() = "TTIME" And ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE3").ToString().Trim()).ToString() <> "1" Then
                                                strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE3").ToString() + " " + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE3").ToString().Trim()).ToString() + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE3").ToString().Trim() + "_U").ToString()
                                            ElseIf dtSeq.Rows(0).Item("TSEQUENCE3").ToString().Trim().ToUpper() = "TTIME" And ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE3").ToString().Trim()).ToString() = "1" Then
                                                strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE3").ToString() + " " + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE3").ToString().Trim() + "_U").ToString()
                                            End If
                                            If dtSeq.Rows(0).Item("TSEQUENCE3").ToString().Trim().ToUpper() = "TANGLE" Then
                                                strVal = ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE3").ToString().Trim()).ToString() + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE3").ToString().Trim() + "_U").ToString()

                                            End If
                                        End If
                                        If dtSeq.Rows(0).Item("TSEQUENCE4").ToString().Trim() <> "" Then
                                            If dtSeq.Rows(0).Item("TSEQUENCE4").ToString().Trim().ToUpper() <> "TRH" Then
                                                For l = 0 To dtConv.Rows.Count - 1
                                                    If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE4").ToString().Trim()).ToString() <> "1" Then

                                                        If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE4").ToString().Trim() + "_U").ToString() = dtConv.Rows(l).Item("METRICUNIT").ToString().Trim() Then
                                                            ConvFact = CDbl(dsConFact.Tables(0).Rows(l).Item("CONVFACTOR").ToString())
                                                            If dtSeq.Rows(0).Item("TSEQUENCE4").ToString().Trim().ToUpper() = "TTEMP" Then
                                                                dvTestConvProp.RowFilter = strFilter + "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND METRICUNIT='" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE4").ToString().Trim() + "_U").ToString() + "' "
                                                                dtTestConvProp = dvTestConvProp.ToTable()
                                                                If dtTestConvProp.Rows.Count > 0 Then
                                                                    dblValue = (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE4").ToString().Trim()) * dtTestConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                                    strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE4").ToString().Trim() + " " + FormatNumber(dblValue.ToString(), 0) + "" + dtTestConvProp.Rows(0).Item("ENGLISHUNIT").ToString().Trim()
                                                                Else
                                                                    dblValue = CDbl(ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE4").ToString().Trim())) * ConvFact + 32
                                                                    strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE4").ToString().Trim() + " " + FormatNumber(dblValue.ToString(), 0) + "" + dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim()
                                                                End If
                                                                'dblValue = CDbl(ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE4").ToString().Trim())) * ConvFact + 32
                                                                'strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE4").ToString().Trim() + " " + FormatNumber(dblValue.ToString(), 0) + "" + dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim()
                                                            Else
                                                                dvTestConvProp.RowFilter = strFilter + "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND METRICUNIT='" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE4").ToString().Trim() + "_U").ToString() + "' "
                                                                dtTestConvProp = dvTestConvProp.ToTable()
                                                                If dtTestConvProp.Rows.Count > 0 Then
                                                                    dblValue = (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE4").ToString().Trim()) * dtTestConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                                    strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE4").ToString().Trim() + " " + FormatNumber(dblValue.ToString(), 3) + "" + dtTestConvProp.Rows(0).Item("ENGLISHUNIT").ToString().Trim()
                                                                Else
                                                                    dblValue = CDbl(ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE4").ToString().Trim()) * (ConvFact))
                                                                    strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE4").ToString().Trim() + " " + FormatNumber(dblValue.ToString(), 3) + "" + dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim()
                                                                End If
                                                                'dblValue = CDbl(ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE4").ToString().Trim()) * (ConvFact))
                                                                'strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE4").ToString().Trim() + " " + FormatNumber(dblValue.ToString(), 3) + "" + dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim()
                                                            End If

                                                                Exit For
                                                            End If

                                                    Else
                                                        If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE4").ToString().Trim() + "_U").ToString() = dtConv.Rows(l).Item("METRICUNIT").ToString().Trim() Then
                                                            ConvFact = CDbl(dsConFact.Tables(0).Rows(l).Item("CONVFACTOR").ToString())
                                                            If dtSeq.Rows(0).Item("TSEQUENCE4").ToString().Trim().ToUpper() = "TTEMP" Then
                                                                dvTestConvProp.RowFilter = strFilter + "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND METRICUNIT='" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE4").ToString().Trim() + "_U").ToString() + "' "
                                                                dtTestConvProp = dvTestConvProp.ToTable()
                                                                If dtTestConvProp.Rows.Count > 0 Then
                                                                    dblValue = (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE4").ToString().Trim()) * dtTestConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                                    strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE4").ToString().Trim() + " " + FormatNumber(dblValue.ToString(), 0) + "" + dtTestConvProp.Rows(0).Item("ENGLISHUNIT").ToString().Trim()
                                                                Else
                                                                    dblValue = CDbl(ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE4").ToString().Trim())) * ConvFact + 32
                                                                    strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE4").ToString().Trim() + " " + FormatNumber(dblValue.ToString(), 0) + "" + dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim()
                                                                End If
                                                                'dblValue = CDbl(ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE4").ToString().Trim())) * ConvFact + 32
                                                                'strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE4").ToString().Trim() + " " + FormatNumber(dblValue.ToString(), 0) + "" + dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim()
                                                            Else
                                                                dvTestConvProp.RowFilter = strFilter + "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND METRICUNIT='" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE4").ToString().Trim() + "_U").ToString() + "' "
                                                                dtTestConvProp = dvTestConvProp.ToTable()
                                                                If dtTestConvProp.Rows.Count > 0 Then
                                                                    dblValue = (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE4").ToString().Trim()) * dtTestConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                                    strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE4").ToString().Trim() + " " + FormatNumber(dblValue.ToString(), 3) + "" + dtTestConvProp.Rows(0).Item("ENGLISHUNIT").ToString().Trim()
                                                                Else
                                                                    dblValue = CDbl(ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE4").ToString().Trim()) * (ConvFact))
                                                                    strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE4").ToString().Trim() + " " + FormatNumber(dblValue.ToString(), 3) + "" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE4").ToString().Trim() + "_U").ToString()
                                                                End If
                                                                'dblValue = CDbl(ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE4").ToString().Trim()) * (ConvFact))
                                                                'strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE4").ToString().Trim() + " " + FormatNumber(dblValue.ToString(), 3) + "" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE4").ToString().Trim() + "_U").ToString()
                                                            End If

                                                                Exit For
                                                            End If

                                                    End If
                                                    If dtSeq.Rows(0).Item("TSEQUENCE4").ToString().Trim().ToUpper() = "TPERCENTAGE" Then
                                                        If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE4").ToString().Trim() + "_U").ToString() = "%" And dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim() = "in/in" Then
                                                        Else

                                                            strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE4").ToString().Trim() + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE4").ToString().Trim()) + "%"
                                                        End If
                                                        Exit For
                                                    End If

                                                Next

                                            Else

                                                strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE4").ToString().Trim() + " " + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE4").ToString().Trim()).ToString() + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE4").ToString().Trim() + "_U").ToString()

                                            End If
                                            If ds.Tables(0).Rows(i).Item("TSEQUENCE4").ToString().Trim() = "@" Then
                                                strVal = strVal + " " + "@"
                                            End If
                                            If dtSeq.Rows(0).Item("TSEQUENCE4").ToString().Trim() = "TRH" Then
                                                strVal = strVal + " " + " RH "
                                            End If
                                            If dtSeq.Rows(0).Item("TSEQUENCE4").ToString().Trim().ToUpper() = "TTIME" And ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE4").ToString().Trim()).ToString() <> "1" Then
                                                strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE4").ToString().Trim() + " " + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE4").ToString().Trim()).ToString() + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE4").ToString().Trim() + "_U").ToString()
                                            ElseIf dtSeq.Rows(0).Item("TSEQUENCE4").ToString().Trim().ToUpper() = "TTIME" And ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE4").ToString().Trim()).ToString() = "1" Then
                                                strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE4").ToString().Trim() + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE4").ToString().Trim() + "_U").ToString()
                                            End If
                                            If dtSeq.Rows(0).Item("TSEQUENCE4").ToString().Trim().Trim().ToUpper() = "TANGLE" Then
                                                strVal = ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE4").ToString().Trim()).ToString() + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE4").ToString().Trim() + "_U").ToString()

                                            End If
                                        End If
                                        If dtSeq.Rows(0).Item("TSEQUENCE5").ToString().Trim() <> "" Then
                                            If dtSeq.Rows(0).Item("TSEQUENCE5").ToString().Trim().ToUpper() <> "TRH" Then
                                                For l = 0 To dtConv.Rows.Count - 1
                                                    If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE5").ToString().Trim()).ToString() <> "1" Then

                                                        If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE5").ToString().Trim() + "_U").ToString() = dtConv.Rows(l).Item("METRICUNIT").ToString().Trim() Then
                                                            ConvFact = CDbl(dsConFact.Tables(0).Rows(l).Item("CONVFACTOR").ToString())
                                                            If dtSeq.Rows(0).Item("TSEQUENCE5").ToString().Trim().ToUpper() = "TTEMP" Then
                                                                dvTestConvProp.RowFilter = strFilter + "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND METRICUNIT='" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE5").ToString().Trim() + "_U").ToString() + "' "
                                                                dtTestConvProp = dvTestConvProp.ToTable()
                                                                If dtTestConvProp.Rows.Count > 0 Then
                                                                    dblValue = (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE5").ToString().Trim()) * dtTestConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                                    strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE5").ToString().Trim() + " " + FormatNumber(dblValue.ToString(), 0) + "" + dtTestConvProp.Rows(0).Item("ENGLISHUNIT").ToString().Trim()
                                                                Else
                                                                    dblValue = CDbl(ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE5").ToString().Trim())) * ConvFact + 32
                                                                    strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE5").ToString().Trim() + " " + FormatNumber(dblValue.ToString(), 0) + "" + dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim()
                                                                End If
                                                                'dblValue = CDbl(ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE5").ToString().Trim())) * ConvFact + 32
                                                                'strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE5").ToString().Trim() + " " + FormatNumber(dblValue.ToString(), 0) + "" + dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim()
                                                            Else
                                                                dvTestConvProp.RowFilter = strFilter + "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND METRICUNIT='" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE5").ToString().Trim() + "_U").ToString() + "' "
                                                                dtTestConvProp = dvTestConvProp.ToTable()
                                                                If dtTestConvProp.Rows.Count > 0 Then
                                                                    dblValue = (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE5").ToString().Trim()) * dtTestConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                                    strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE5").ToString().Trim() + " " + FormatNumber(dblValue.ToString(), 3) + "" + dtTestConvProp.Rows(0).Item("ENGLISHUNIT").ToString().Trim()
                                                                Else
                                                                    dblValue = CDbl(ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE5").ToString().Trim()) * (ConvFact))
                                                                    strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE5").ToString().Trim() + " " + FormatNumber(dblValue.ToString(), 3) + "" + dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim()
                                                                End If
                                                                'dblValue = CDbl(ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE5").ToString().Trim()) * (ConvFact))
                                                                'strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE5").ToString().Trim() + " " + FormatNumber(dblValue.ToString(), 3) + "" + dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim()
                                                            End If

                                                                Exit For
                                                            End If

                                                    Else
                                                        If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE5").ToString().Trim() + "_U").ToString() = dtConv.Rows(l).Item("METRICUNIT").ToString().Trim() Then
                                                            ConvFact = CDbl(dsConFact.Tables(0).Rows(l).Item("CONVFACTOR").ToString())
                                                            If dtSeq.Rows(0).Item("TSEQUENCE5").ToString().Trim().ToUpper() = "TTEMP" Then
                                                                dvTestConvProp.RowFilter = strFilter + "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND METRICUNIT='" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE5").ToString().Trim() + "_U").ToString() + "' "
                                                                dtTestConvProp = dvTestConvProp.ToTable()
                                                                If dtTestConvProp.Rows.Count > 0 Then
                                                                    dblValue = (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE5").ToString().Trim()) * dtTestConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                                    strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE5").ToString().Trim() + " " + FormatNumber(dblValue.ToString(), 0) + "" + dtTestConvProp.Rows(0).Item("ENGLISHUNIT").ToString().Trim()
                                                                Else
                                                                    dblValue = CDbl(ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE5").ToString().Trim())) * ConvFact + 32
                                                                    strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE5").ToString().Trim() + " " + FormatNumber(dblValue.ToString(), 0) + "" + dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim()
                                                                End If
                                                                'dblValue = CDbl(ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE5").ToString().Trim())) * ConvFact + 32
                                                                'strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE5").ToString().Trim() + " " + FormatNumber(dblValue.ToString(), 0) + "" + dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim()
                                                            Else
                                                                dvTestConvProp.RowFilter = strFilter + "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND METRICUNIT='" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE5").ToString().Trim() + "_U").ToString() + "' "
                                                                dtTestConvProp = dvConvProp.ToTable()
                                                                If dtTestConvProp.Rows.Count > 0 Then
                                                                    dblValue = (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE5").ToString().Trim()) * dtTestConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                                    strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE5").ToString().Trim() + " " + FormatNumber(dblValue.ToString(), 3) + "" + dtTestConvProp.Rows(0).Item("ENGLISHUNIT").ToString().Trim()
                                                                Else
                                                                    dblValue = CDbl(ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE5").ToString().Trim()) * (ConvFact))
                                                                    strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE5").ToString().Trim() + " " + FormatNumber(dblValue.ToString(), 3) + "" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE5").ToString().Trim() + "_U").ToString()
                                                                End If
                                                                'dblValue = CDbl(ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE5").ToString().Trim()) * (ConvFact))
                                                                'strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE5").ToString().Trim() + " " + FormatNumber(dblValue.ToString(), 3) + "" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE5").ToString().Trim() + "_U").ToString()
                                                            End If

                                                                Exit For
                                                            End If

                                                    End If
                                                    If dtSeq.Rows(0).Item("TSEQUENCE5").ToString().Trim().ToUpper() = "TPERCENTAGE" Then
                                                        If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE5").ToString().Trim() + "_U").ToString() = "%" And dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim() = "in/in" Then
                                                        Else

                                                            strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE5").ToString().Trim() + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE5").ToString().Trim()) + "%"
                                                        End If
                                                        Exit For
                                                    End If

                                                Next

                                            Else

                                                strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE5").ToString().Trim() + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE5").ToString().Trim()).ToString() + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE5").ToString().Trim() + "_U").ToString()

                                            End If
                                            If ds.Tables(0).Rows(i).Item("TSEQUENCE5").ToString().Trim() = "@" Then
                                                strVal = strVal + " " + "@"
                                            End If
                                            If dtSeq.Rows(0).Item("TSEQUENCE5").ToString().Trim() = "TRH" Then
                                                strVal = strVal + " " + " RH "
                                            End If
                                            If dtSeq.Rows(0).Item("TSEQUENCE5").ToString().Trim().ToUpper() = "TTIME" And ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE5").ToString().Trim()).ToString() <> "1" Then
                                                strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE5").ToString().Trim() + " " + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE5").ToString().Trim()).ToString() + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE5").ToString().Trim() + "_U").ToString()
                                            ElseIf dtSeq.Rows(0).Item("TSEQUENCE5").ToString().Trim().ToUpper() = "TTIME" And ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE5").ToString().Trim()).ToString() = "1" Then
                                                strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE5").ToString().Trim() + " " + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE5").ToString().Trim() + "_U").ToString()
                                            End If
                                            If dtSeq.Rows(0).Item("TSEQUENCE5").ToString().Trim().ToUpper() = "TANGLE" Then
                                                strVal = ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE5").ToString().Trim()).ToString() + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE5").ToString().Trim() + "_U").ToString()

                                            End If
                                        End If
                                        If dtSeq.Rows(0).Item("TSEQUENCE6").ToString().Trim() <> "" Then
                                            If dtSeq.Rows(0).Item("TSEQUENCE6").ToString().Trim().ToUpper() <> "TRH" Then
                                                For l = 0 To dtConv.Rows.Count - 1
                                                    If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE6").ToString().Trim()).ToString() <> "1" Then

                                                        If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE6").ToString().Trim() + "_U").ToString() = dtConv.Rows(l).Item("METRICUNIT").ToString().Trim() Then
                                                            ConvFact = CDbl(dsConFact.Tables(0).Rows(l).Item("CONVFACTOR").ToString())
                                                            If dtSeq.Rows(0).Item("TSEQUENCE6").ToString().Trim().ToUpper() = "TTEMP" Then
                                                                dvTestConvProp.RowFilter = strFilter + "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND METRICUNIT='" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE6").ToString().Trim() + "_U").ToString() + "' "
                                                                dtTestConvProp = dvTestConvProp.ToTable()
                                                                If dtTestConvProp.Rows.Count > 0 Then
                                                                    dblValue = (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE6").ToString().Trim()) * dtTestConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                                    strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE6").ToString().Trim() + " " + FormatNumber(dblValue.ToString(), 0) + "" + dtTestConvProp.Rows(0).Item("ENGLISHUNIT").ToString().Trim()
                                                                Else
                                                                    dblValue = CDbl(ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE6").ToString().Trim())) * ConvFact + 32
                                                                    strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE6").ToString().Trim() + FormatNumber(dblValue.ToString(), 0) + "" + dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim()
                                                                End If
                                                                'dblValue = CDbl(ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE6").ToString().Trim())) * ConvFact + 32
                                                                'strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE6").ToString().Trim() + FormatNumber(dblValue.ToString(), 0) + "" + dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim()
                                                            Else
                                                                dvTestConvProp.RowFilter = strFilter + "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND METRICUNIT='" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE6").ToString().Trim() + "_U").ToString() + "' "
                                                                dtTestConvProp = dvTestConvProp.ToTable()
                                                                If dtTestConvProp.Rows.Count > 0 Then
                                                                    dblValue = (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE6").ToString().Trim()) * dtTestConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                                    strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE6").ToString().Trim() + " " + FormatNumber(dblValue.ToString(), 3) + "" + dtTestConvProp.Rows(0).Item("ENGLISHUNIT").ToString().Trim()
                                                                Else
                                                                    dblValue = CDbl(ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE6").ToString().Trim()) * (ConvFact))
                                                                    strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE6").ToString().Trim() + FormatNumber(dblValue.ToString(), 3) + "" + dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim()
                                                                End If
                                                                'dblValue = CDbl(ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE6").ToString().Trim()) * (ConvFact))
                                                                'strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE6").ToString().Trim() + FormatNumber(dblValue.ToString(), 3) + "" + dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim()
                                                            End If

                                                                Exit For
                                                            End If

                                                    Else
                                                        If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE6").ToString().Trim() + "_U").ToString() = dtConv.Rows(l).Item("METRICUNIT").ToString().Trim() Then
                                                            ConvFact = CDbl(dsConFact.Tables(0).Rows(l).Item("CONVFACTOR").ToString())
                                                            If dtSeq.Rows(0).Item("TSEQUENCE6").ToString().Trim().ToUpper() = "TTEMP" Then
                                                                dvTestConvProp.RowFilter = strFilter + "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND METRICUNIT='" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE6").ToString().Trim() + "_U").ToString() + "' "
                                                                dtTestConvProp = dvTestConvProp.ToTable()
                                                                If dtTestConvProp.Rows.Count > 0 Then
                                                                    dblValue = (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE6").ToString().Trim()) * dtTestConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                                    strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE6").ToString().Trim() + " " + FormatNumber(dblValue.ToString(), 0) + "" + dtTestConvProp.Rows(0).Item("ENGLISHUNIT").ToString().Trim()
                                                                Else
                                                                    dblValue = CDbl(ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE6").ToString().Trim())) * ConvFact + 32
                                                                    strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE6").ToString().Trim() + FormatNumber(dblValue.ToString(), 0) + "" + dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim()
                                                                End If
                                                                'dblValue = CDbl(ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE6").ToString().Trim())) * ConvFact + 32
                                                                'strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE6").ToString().Trim() + FormatNumber(dblValue.ToString(), 0) + "" + dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim()
                                                            Else
                                                                dvTestConvProp.RowFilter = strFilter + "PROPERTYID=" + ds.Tables(0).Rows(i).Item("PROPERTYID").ToString() + " AND METRICUNIT='" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE6").ToString().Trim() + "_U").ToString() + "' "
                                                                dtTestConvProp = dvTestConvProp.ToTable()
                                                                If dtTestConvProp.Rows.Count > 0 Then
                                                                    dblValue = (ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE6").ToString().Trim()) * dtTestConvProp.Rows(0).Item("CONVFACTOR").ToString())
                                                                    strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE6").ToString().Trim() + " " + FormatNumber(dblValue.ToString(), 3) + "" + dtTestConvProp.Rows(0).Item("ENGLISHUNIT").ToString().Trim()
                                                                Else
                                                                    dblValue = CDbl(ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE6").ToString().Trim()) * (ConvFact))
                                                                    strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE6").ToString().Trim() + FormatNumber(dblValue.ToString(), 3) + "" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE6").ToString().Trim() + "_U").ToString()
                                                                End If
                                                                'dblValue = CDbl(ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE6").ToString().Trim()) * (ConvFact))
                                                                'strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE6").ToString().Trim() + FormatNumber(dblValue.ToString(), 3) + "" + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE6").ToString().Trim() + "_U").ToString()
                                                            End If

                                                                Exit For
                                                            End If

                                                    End If
                                                    If dtSeq.Rows(0).Item("TSEQUENCE6").ToString().Trim().ToUpper() = "TPERCENTAGE" Then
                                                        If ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE6").ToString().Trim() + "_U").ToString() = "%" And dtConv.Rows(l).Item("ENGLISHUNIT").ToString().Trim() = "in/in" Then
                                                        Else

                                                            strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE6").ToString().Trim() + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE6").ToString().Trim()) + "%"
                                                        End If
                                                        Exit For
                                                    End If

                                                Next

                                            Else

                                                strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE6").ToString().Trim() + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE6")).ToString().Trim() + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE6").ToString().Trim() + "_U").ToString()

                                            End If
                                            If ds.Tables(0).Rows(i).Item("TSEQUENCE6").ToString() = "@" Then
                                                strVal = strVal + " " + "@"
                                            End If
                                            If dtSeq.Rows(0).Item("TSEQUENCE6").ToString() = "TRH" Then
                                                strVal = strVal + " " + " RH "
                                            End If
                                            If dtSeq.Rows(0).Item("TSEQUENCE6").ToString().Trim().ToUpper() = "TTIME" And ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE6").ToString().Trim()).ToString() <> "1" Then
                                                strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE6").ToString().Trim() + " " + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE6").ToString().Trim()).ToString() + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE6").ToString().Trim() + "_U").ToString()
                                            ElseIf dtSeq.Rows(0).Item("TSEQUENCE6").ToString().Trim().ToUpper() = "TTIME" And ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE6")).ToString().Trim() = "1" Then
                                                strVal = strVal + " " + ds.Tables(0).Rows(i).Item("TSEQUENCE6").ToString().Trim() + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE6").ToString().Trim() + "_U").ToString()
                                            End If
                                            If dtSeq.Rows(0).Item("TSEQUENCE6").ToString().Trim().ToUpper() = "TANGLE" Then
                                                strVal = ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE6").ToString().Trim()).ToString() + ds.Tables(0).Rows(i).Item(dtSeq.Rows(0).Item("TSEQUENCE6").ToString().Trim() + "_U").ToString()

                                            End If
                                        End If

                                    End If
                                    lbl.Text = strVal
                                    tdInner.Controls.Add(lbl)
                                    trInner.Controls.Add(tdInner)
                                Case 9
                                    InnerTdSetting(tdInner, "", "Left")
                                    lbl = New Label
                                    lbl.Text = ds.Tables(0).Rows(i).Item("TESTBODY").ToString() + " " + ds.Tables(0).Rows(i).Item("TESTNUMBER").ToString()
                                    tdInner.Controls.Add(lbl)
                                    trInner.Controls.Add(tdInner)
                                Case 10
                                    InnerTdSetting(tdInner, "", "Left")
                                    lbl = New Label
                                    lbl.Text = " "
                                    tdInner.Controls.Add(lbl)
                                    trInner.Controls.Add(tdInner)
                            End Select
                        Next
                        If (i Mod 2 = 0) Then
                            trInner.CssClass = "AlterNateColor1"
                        Else
                            trInner.CssClass = "AlterNateColor2"
                        End If
                        trInner.Height = 30
                        tblComparision.Controls.Add(trInner)
                    Next
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub GetGradeDetails()
        Try
            Dim objUpIn As New StandUpInsData.UpdateInsert()
            Dim objGetdata As New StandGetData.Selectdata()
            Dim ds As New DataSet()
            Dim dsMat As New DataSet()
            ds = objGetdata.GetMatSupplierGrade(hidMatId.Value, hidSponsor.Value.ToString(), hidGradeId.Value)
            dsMat = objGetdata.GetMatSupGradeType(hidMatId.Value, hidSponsor.Value.ToString(), hidGradeId.Value)
            lblCompany.Text = ds.Tables(0).Rows(0).Item("NAME").ToString()
            lblGrade.Text = ds.Tables(0).Rows(0).Item("GRADENAME").ToString()
            lblGradeType.Text = dsMat.Tables(0).Rows(0).Item("MATERIAL").ToString()

            'Started Activity Log Changes
            Try
                objUpIn.InsertLog1(Session("UserId").ToString(), "13", "Opened Technical DataSheet for Company:" + lblCompany.Text + " ,Material:" + lblGradeType.Text + " ,Grade:" + lblGrade.Text, Session("SBACaseId"), Session("LogInCount").ToString(), Session.SessionID, hidMatId.Value, hidSponsor.Value.ToString(), "", hidGradeId.Value, "")
            Catch ex As Exception

            End Try
            'Ended Activity Log Changes

        Catch ex As Exception

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

        End Try
    End Sub

    Protected Sub TextBoxSetting(ByVal txt As TextBox, ByVal Css As String)
        Try
            txt.CssClass = Css

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

        End Try
    End Sub

    Public Function GetMetricConvFactorProp() As DataSet
        Dim Dts As New DataSet()
        Dim odbUtil As New DBUtil()
        Dim StrSql As String = String.Empty
        Dim SBAConnection As String = System.Configuration.ConfigurationManager.AppSettings("SBAConnectionString")
        Try

            StrSql = "SELECT PROPERTYID,METRICUNIT,ENGLISHUNIT,CONVFACTOR,MATERIALID,SUPPLIERID,GRADEID "
            StrSql = StrSql + "FROM METENGFACTORSP "
            Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
            Return Dts
        Catch ex As Exception
            Throw New Exception("SAssist:GetSupplierMaterialSEQ:" + ex.Message.ToString())
            Return Dts
        End Try
    End Function

End Class
