Imports System.Data
Imports System.Data.OleDb
Imports System
Imports SavvyProGetData
Imports SavvyProUpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Imports System.Net.Mail
Imports System.Net
Partial Class Pages_SavvyPackPro_Contract
    Inherits System.Web.UI.Page
    Dim objGetdata As New SavvyProGetData
    Dim objUpIns As New SavvyProUpInsData

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                GetPageDetails()
                ChkExistingRfp()
            End If
        Catch ex As Exception
            'lblError.Text = "Error: Page_Load() " + ex.Message()
        End Try
    End Sub
    Protected Sub ChkExistingRfp()
        Dim DsCheckRfp As New DataSet()
        Try
            DsCheckRfp = objGetdata.GetLatestRFPbyLicenseID(Session("LicenseNo"), Session("USERID"))
            If DsCheckRfp.Tables(0).Rows.Count > 0 Then
                GetRfpDetails(DsCheckRfp.Tables(0).Rows(0).Item("ID").ToString())
            Else
                RfpDetail.Visible = False
                tabContract.Enabled = False

            End If
        Catch ex As Exception
            lblError.Text = "Error: ChkExistingRfp() " + ex.Message()
        End Try
    End Sub

    Protected Sub GetRfpDetails(ByVal RfpID As String)
        Dim DsRfpdet As New DataSet()
        Try
            If RfpID <> "" Or RfpID <> "0" Then
                DsRfpdet = objGetdata.GetRFPbyID(RfpID)
                If DsRfpdet.Tables(0).Rows.Count > 0 Then
                    RfpDetail.Visible = True
                    lnkSelRFP.Text = DsRfpdet.Tables(0).Rows(0).Item("DES1").ToString()
                    lblSelRfpID.Text = DsRfpdet.Tables(0).Rows(0).Item("ID").ToString()
                    Session("hidRfpID") = lblSelRfpID.Text
                    lblSelRfpDes.Text = DsRfpdet.Tables(0).Rows(0).Item("DES1").ToString()
                    lblType.Text = DsRfpdet.Tables(0).Rows(0).Item("TYPEDESC").ToString()
                    'tabIssueRFP.Enabled = True
                    If Session("USERID") = DsRfpdet.Tables(0).Rows(0).Item("USERID").ToString() Then
                        tabContract.Enabled = True
                    Else
                        tabContract.Enabled = False
                    End If
                    loadTab()
                Else
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('System isn't able to find RFPID. Please try again.');", True)
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error: GetRfpDetails() " + ex.Message()
        End Try
    End Sub

    Protected Sub loadTab()
        Try
            GetPageDetails()
            'GetIssueList()
        Catch ex As Exception
            lblError.Text = "Error: GetRfpDetails() " + ex.Message()
        End Try
    End Sub
    Protected Sub GetPageDetails()
        Dim dstbl As New DataSet
        Dim DsDes As New DataSet()
        Dim dsCaseDetails As New DataSet
        Dim objGetData As New E1GetData.Selectdata
        Dim CaseIds As String = String.Empty
        'Dim DataCnt As New Integer
        Dim i As New Integer
        Dim j As New Integer
        Dim k As New Integer
        Dim DWidth As String = String.Empty
        Dim arrCaseID(100) As String
        Dim btn As Button
        Dim CasesVal(9) As String
        Dim dsEx As New DataSet
        Try
           
            Dim trHeader As New TableRow
            Dim tdHeader As TableCell
            Dim tdInner As TableCell
            Dim trInner As New TableRow
            Dim ddl As DropDownList
            Dim lbl As New Label
            Dim txt As TextBox
            Dim Cunits As New Integer
            Dim Units As New Integer
            Dim CurrTitle As String = String.Empty
            Dim CCurrTitle As String = String.Empty
            Dim UnitError As String = String.Empty
            Dim CurrError As String = String.Empty
            Dim Title As String = String.Empty

            Dim objGetDataW As New E3GetData.Selectdata
            Dim dsW As New DataSet

            'dsW = objGetDataW.CompCOLWIDTH(AssumptionId)
            'If dsW.Tables(0).Rows.Count > 0 Then
            '    txtDWidth.Text = dsW.Tables(0).Rows(0).Item("COLWIDTH").ToString()
            'End If
            'DWidth = "150" + "px"
            Dim dtEx As New DataTable
            Dim Header As String = ""
            For i = 0 To 0
                trHeader = New TableRow
                For j = 0 To 8
                    tdHeader = New TableCell
                    If j = 0 Then
                        Header = "ID"
                    ElseIf j = 1 Then
                        Header = "TYPE"
                    ElseIf j = 2 Then
                        Header = "SPECID"
                    ElseIf j = 3 Then
                        Header = "VENDOR NAME"
                    ElseIf j = 4 Then
                        Header = "DUE DATE"
                    ElseIf j = 5 Then
                        Header = "STARTING DATE"
                    ElseIf j = 6 Then
                        Header = "ENDING DATE"
                    ElseIf j = 7 Then
                        Header = "PRICE"
                    ElseIf j = 8 Then
                        Header = "SETUP PRICE"
                        'ElseIf j = 9 Then
                        '    Header = "DEPRECIATION"
                        'ElseIf j = 10 Then
                        '    Header = "DISTRIBUTION PACKAGING"
                        'ElseIf j = 11 Then
                        '    Header = "FREIGHT"
                        'ElseIf j = 12 Then
                        '    Header = "TOTAL COST"
                    End If
                    dtEx.Columns.Add(Header)
                    HeaderTdSetting(tdHeader, "120px", Header, 1)
                    trHeader.Controls.Add(tdHeader)
                    trHeader.Height = 20
                    trHeader.CssClass = "PageSSHeading"
                Next
                tblComparision.Controls.Add(trHeader)
            Next

            'For i = 0 To DataCnt
            '    Dim ds As New DataSet
            'ds = objGetData.GetBulkCostDetails(arrCaseID(i), False)
            '    ds.Tables(0).TableName = arrCaseID(i).ToString()
            '    dstbl.Tables.Add(ds.Tables(arrCaseID(i).ToString()).Copy())
            'Next


            'For i = 0 To DataCnt
            '    trInner = New TableRow()
            '    Dim nRow As DataRow = dtEx.NewRow()
            '    For j = 0 To 12
            '        Select Case j
            '            Case 0
            '                lbl = New Label
            '                tdInner = New TableCell
            '                InnerTdSetting(tdInner, "", "left")
            '                lbl.Text = dstbl.Tables(i).Rows(0).Item("CASEID").ToString()
            '                nRow("SAVVYPACK CASE ID") = lbl.Text
            '                tdInner.Controls.Add(lbl)
            '                trInner.Controls.Add(tdInner)

            '            Case 1
            '                tdInner = New TableCell
            '                tdInner.Text = dstbl.Tables(i).Rows(0).Item("GRPDES").ToString()
            '                nRow("GROUP") = tdInner.Text
            '                InnerTdSetting(tdInner, "150px", "")
            '                trInner.Controls.Add(tdInner)
            '            Case 2
            '                tdInner = New TableCell
            '                Title = dstbl.Tables(i).Rows(0).Item("CASEDE1").ToString()
            '                LeftTdSetting(tdInner, Title, trInner, "AlterNateColor2")
            '                nRow("PACKAGE FORMAT (SKU)") = Title
            '                trInner.Controls.Add(tdInner)
            '            Case 3
            '                tdInner = New TableCell
            '                tdInner.Text = dstbl.Tables(i).Rows(0).Item("CASEDE2").ToString()
            '                InnerTdSetting(tdInner, "150px", "")
            '                nRow("UNIQUE FEATURES") = tdInner.Text
            '                trInner.Controls.Add(tdInner)
            '            Case 4
            '                tdInner = New TableCell
            '                Title = dstbl.Tables(i).Rows(0).Item("CASEDE3").ToString()
            '                LeftTdSetting(tdInner, Title, trInner, "AlterNateColor2")
            '                nRow("DESCRIPTION") = Title.Replace("vbCrLf", "")
            '                trInner.Controls.Add(tdInner)
            '            Case 5
            '                tdInner = New TableCell
            '                Dim perunit As New Decimal
            '                Dim perdoller As New Decimal
            '                If CDbl(dstbl.Tables(i).Rows(0).Item("SUNIT").ToString()) > 0 Then
            '                    perunit = CDbl(dstbl.Tables(i).Rows(0).Item("PL1").ToString()) * CDbl(dstbl.Tables(i).Rows(0).Item("SUNIT").ToString())
            '                    perdoller = perunit * 10
            '                End If
            '                LeftTdSetting(tdInner, FormatNumber(perdoller, 4), trInner, "AlterNateColor2")
            '                nRow("MATERIAL COST") = FormatNumber(perdoller, 4)
            '                trInner.Controls.Add(tdInner)
            '            Case 6
            '                tdInner = New TableCell
            '                Dim perunit As New Decimal
            '                Dim perdoller As New Decimal
            '                If CDbl(dstbl.Tables(i).Rows(0).Item("SUNIT").ToString()) > 0 Then
            '                    perunit = CDbl(dstbl.Tables(i).Rows(0).Item("PL2").ToString()) * CDbl(dstbl.Tables(i).Rows(0).Item("SUNIT").ToString())
            '                    perdoller = perunit * 10
            '                End If
            '                LeftTdSetting(tdInner, FormatNumber(perdoller, 4), trInner, "AlterNateColor2")
            '                nRow("VARIABLE LABOR") = FormatNumber(perdoller, 4)
            '                trInner.Controls.Add(tdInner)
            '            Case 7
            '                tdInner = New TableCell
            '                Dim perunit As New Decimal
            '                Dim perdoller As New Decimal
            '                If CDbl(dstbl.Tables(i).Rows(0).Item("SUNIT").ToString()) > 0 Then
            '                    perunit = CDbl(dstbl.Tables(i).Rows(0).Item("PL3").ToString()) * CDbl(dstbl.Tables(i).Rows(0).Item("SUNIT").ToString())
            '                    perdoller = perunit * 10
            '                End If
            '                LeftTdSetting(tdInner, FormatNumber(perdoller, 4), trInner, "AlterNateColor2")
            '                nRow("VARIABLE ENERGY") = FormatNumber(perdoller, 4)
            '                trInner.Controls.Add(tdInner)
            '            Case 8
            '                tdInner = New TableCell
            '                Dim perunit As New Decimal
            '                Dim perdoller As New Decimal
            '                If CDbl(dstbl.Tables(i).Rows(0).Item("SUNIT").ToString()) > 0 Then
            '                    perunit = CDbl(dstbl.Tables(i).Rows(0).Item("PL4").ToString()) * CDbl(dstbl.Tables(i).Rows(0).Item("SUNIT").ToString())
            '                    perdoller = perunit * 10
            '                End If
            '                LeftTdSetting(tdInner, FormatNumber(perdoller, 4), trInner, "AlterNateColor2")
            '                nRow("PLANT OVERHEAD") = FormatNumber(perdoller, 4)
            '                trInner.Controls.Add(tdInner)
            '            Case 9
            '                tdInner = New TableCell
            '                Dim perunit As New Decimal
            '                Dim perdoller As New Decimal
            '                If CDbl(dstbl.Tables(i).Rows(0).Item("SUNIT").ToString()) > 0 Then
            '                    perunit = CDbl(dstbl.Tables(i).Rows(0).Item("PL5").ToString()) * CDbl(dstbl.Tables(i).Rows(0).Item("SUNIT").ToString())
            '                    perdoller = perunit * 10
            '                End If
            '                LeftTdSetting(tdInner, FormatNumber(perdoller, 4), trInner, "AlterNateColor2")
            '                nRow("DEPRECIATION") = FormatNumber(perdoller, 4)
            '                trInner.Controls.Add(tdInner)
            '            Case 10
            '                tdInner = New TableCell
            '                Dim perunit As New Decimal
            '                Dim perdoller As New Decimal
            '                If CDbl(dstbl.Tables(i).Rows(0).Item("SUNIT").ToString()) > 0 Then
            '                    perunit = CDbl(dstbl.Tables(i).Rows(0).Item("PL6").ToString()) * CDbl(dstbl.Tables(i).Rows(0).Item("SUNIT").ToString())
            '                    perdoller = perunit * 10
            '                End If
            '                LeftTdSetting(tdInner, FormatNumber(perdoller, 4), trInner, "AlterNateColor2")
            '                nRow("DISTRIBUTION PACKAGING") = FormatNumber(perdoller, 4)
            '                trInner.Controls.Add(tdInner)
            '            Case 11
            '                tdInner = New TableCell
            '                Dim perunit As New Decimal
            '                Dim perdoller As New Decimal
            '                If CDbl(dstbl.Tables(i).Rows(0).Item("SUNIT").ToString()) > 0 Then
            '                    perunit = CDbl(dstbl.Tables(i).Rows(0).Item("PL7").ToString()) * CDbl(dstbl.Tables(i).Rows(0).Item("SUNIT").ToString())
            '                    perdoller = perunit * 10
            '                End If
            '                LeftTdSetting(tdInner, FormatNumber(perdoller, 4), trInner, "AlterNateColor2")
            '                nRow("FREIGHT") = FormatNumber(perdoller, 4)
            '                trInner.Controls.Add(tdInner)
            '            Case 12
            '                tdInner = New TableCell
            '                Dim perunit As New Decimal
            '                Dim perdoller As New Decimal
            '                If CDbl(dstbl.Tables(i).Rows(0).Item("SUNIT").ToString()) > 0 Then
            '                    perunit = CDbl(dstbl.Tables(i).Rows(0).Item("PL8").ToString()) * CDbl(dstbl.Tables(i).Rows(0).Item("SUNIT").ToString())
            '                    perdoller = perunit * 10
            '                End If
            '                LeftTdSetting(tdInner, FormatNumber(perdoller, 4), trInner, "AlterNateColor2")
            '                nRow("TOTAL COST") = FormatNumber(perdoller, 4)
            '                trInner.Controls.Add(tdInner)
            '        End Select

            '    Next
            '    dtEx.Rows.Add(nRow)
            If i <> 1 Then
                If (j Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
            End If
            tblComparision.Controls.Add(trInner)

            '  Next
            dsEx.Tables.Add(dtEx)
            Session("dtEx") = dsEx
        Catch ex As Exception
            '  _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
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
            '  _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Dim dt As DataTable
        Dim ds As New DataSet
        Try
            If txtName.Text.Trim.ToString() <> "" Then
                ds = CType(Session("dtEx"), DataSet)
                dt = ds.Tables(0)

                Response.Clear()
                Response.Buffer = True
                Response.AddHeader("content-disposition", "attachment;filename=" + txtName.Text.Trim.ToString() + ".CSV")
                Response.Charset = ""
                Response.ContentType = "application/text"

                Dim sb As New StringBuilder()
                For k As Integer = 0 To dt.Columns.Count - 1
                    'add separator
                    'If k = 1 Or k = 2 Or k = 3 Or k = 7 Then
                    sb.Append(dt.Columns(k).ColumnName + ","c)
                    'End If
                Next

                'append new line
                sb.Append(vbCr & vbLf)
                For i As Integer = 0 To dt.Rows.Count - 1
                    For k As Integer = 0 To dt.Columns.Count - 1
                        'add separator
                        'If k = 1 Or k = 2 Or k = 3 Or k = 7 Then
                        sb.Append(dt.Rows(i)(k).ToString().Replace(",", ";") + ","c)
                        'End If
                    Next
                    'append new line
                    sb.Append(vbCr & vbLf)
                Next

                Response.Output.Write(sb.ToString())
                Response.Flush()
                Response.End()
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Please enter File Name');", True)
            End If

        Catch ex As Exception

        End Try
    End Sub

End Class
