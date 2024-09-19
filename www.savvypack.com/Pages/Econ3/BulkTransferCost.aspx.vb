Imports System.Data
Imports System.Data.OleDb
Imports System
Imports E3GetData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Web.UI.HtmlTextWriter
Imports Microsoft.Office.Interop

Partial Class Pages_Econ3_BulkTransferCost
    Inherits System.Web.UI.Page
    Shared uName As String

#Region "Get Set Variables"
    Dim _lErrorLble As Label
    Dim _strUserName As String
    Dim _strPassword As String
    Dim _iAssumptionId As String
    Dim _btnUpdate As ImageButton


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

    Public Property AssumptionId() As String
        Get
            Return _iAssumptionId
        End Get
        Set(ByVal Value As String)
            _iAssumptionId = Value
        End Set
    End Property

    Public Property Updatebtn() As ImageButton
        Get
            Return _btnUpdate
        End Get
        Set(ByVal value As ImageButton)
            _btnUpdate = value
        End Set
    End Property


    Public DataCnt As Integer
    Public CaseDesp As New ArrayList

#End Region

#Region "MastePage Content Variables"

    Protected Sub GetErrorLable()
        ErrorLable = Page.Master.FindControl("lblError")
    End Sub

    Protected Sub GetUpdatebtn()
        Updatebtn = Page.Master.FindControl("imgUpdate")
        Updatebtn.Visible = True
    End Sub

#End Region

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            GetSessionDetails()
            GetPageDetails()
        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub GetSessionDetails()
        Try
            UserName = Session("UserName")
            Password = Session("Password")
            AssumptionId = Session("BulkCases")
            uName = Session("UserName")
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetSessionDetails:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Function GetCaseIds() As String()
        Dim CaseIds(100) As String
        Dim objGetData As New E3GetData.Selectdata
        Try
            CaseIds = objGetData.BulkCases(AssumptionId)
            Return CaseIds
        Catch ex As Exception
            Return CaseIds
            _lErrorLble.Text = "Error:GetCaseIds:" + ex.Message.ToString()
        End Try

    End Function
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
       ' Dim CasesVal(9) As String
        Dim dsEx As New DataSet
        Try
            arrCaseID = GetCaseIds()
            DataCnt = arrCaseID.Length - 1
            'ReDim arrCaseID(1)
            'arrCaseID(0) = "96980"
            'arrCaseID(1) = "96981"
  Dim CasesVal(DataCnt) As String
            For i = 0 To DataCnt
                CasesVal(i) = arrCaseID(i)
            Next
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
                For j = 0 To 12
                    tdHeader = New TableCell
                    If j = 0 Then
                        Header = "SAVVYPACK CASE ID"
                    ElseIf j = 1 Then
                        Header = "GROUP"
                    ElseIf j = 2 Then
                        Header = "PACKAGE FORMAT (SKU)"
                    ElseIf j = 3 Then
                        Header = "UNIQUE FEATURES"
                    ElseIf j = 4 Then
                        Header = "DESCRIPTION"
                    ElseIf j = 5 Then
                        Header = "MATERIAL COST"
                    ElseIf j = 6 Then
                        Header = "VARIABLE LABOR"
                    ElseIf j = 7 Then
                        Header = "VARIABLE ENERGY"
                    ElseIf j = 8 Then
                        Header = "PLANT OVERHEAD"
                    ElseIf j = 9 Then
                        Header = "DEPRECIATION"
                    ElseIf j = 10 Then
                        Header = "DISTRIBUTION PACKAGING"
                    ElseIf j = 11 Then
                        Header = "FREIGHT"
                    ElseIf j = 12 Then
                        Header = "TOTAL COST"
                    End If
                    dtEx.Columns.Add(Header)
                    HeaderTdSetting(tdHeader, "120px", Header, 1)
                    trHeader.Controls.Add(tdHeader)
                    trHeader.Height = 20
                    trHeader.CssClass = "PageSSHeading"
                Next
                tblComparision.Controls.Add(trHeader)
            Next

            For i = 0 To DataCnt
                Dim ds As New DataSet
                ds = objGetData.GetBulkCostDetails(arrCaseID(i), False)
                ds.Tables(0).TableName = arrCaseID(i).ToString()
                dstbl.Tables.Add(ds.Tables(arrCaseID(i).ToString()).Copy())
            Next


            For i = 0 To DataCnt
                trInner = New TableRow()
                Dim nRow As DataRow = dtEx.NewRow()
                For j = 0 To 12
                    Select Case j
                        Case 0
                            lbl = New Label
                            tdInner = New TableCell
                            InnerTdSetting(tdInner, "", "left")
                            lbl.Text = dstbl.Tables(i).Rows(0).Item("CASEID").ToString()
                            nRow("SAVVYPACK CASE ID") = lbl.Text
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)

                        Case 1
                            tdInner = New TableCell
                            tdInner.Text = dstbl.Tables(i).Rows(0).Item("GRPDES").ToString()
                            nRow("GROUP") = tdInner.Text
                            InnerTdSetting(tdInner, "150px", "")
                            trInner.Controls.Add(tdInner)
                        Case 2
                            tdInner = New TableCell
                            Title = dstbl.Tables(i).Rows(0).Item("CASEDE1").ToString()
                            LeftTdSetting(tdInner, Title, trInner, "AlterNateColor2")
                            nRow("PACKAGE FORMAT (SKU)") = Title
                            trInner.Controls.Add(tdInner)
                        Case 3
                            tdInner = New TableCell
                            tdInner.Text = dstbl.Tables(i).Rows(0).Item("CASEDE2").ToString()
                            InnerTdSetting(tdInner, "150px", "")
                            nRow("UNIQUE FEATURES") = tdInner.Text
                            trInner.Controls.Add(tdInner)
                        Case 4
                            tdInner = New TableCell
                            Title = dstbl.Tables(i).Rows(0).Item("CASEDE3N").ToString()
                            LeftTdSetting(tdInner, Title, trInner, "AlterNateColor2")
                            nRow("DESCRIPTION") = Title
                            trInner.Controls.Add(tdInner)
                        Case 5
                            tdInner = New TableCell
                            Dim perunit As New Decimal
                            Dim perdoller As New Decimal
                            If CDbl(dstbl.Tables(i).Rows(0).Item("SUNIT").ToString()) > 0 Then
                                perunit = CDbl(dstbl.Tables(i).Rows(0).Item("PL1").ToString()) * CDbl(dstbl.Tables(i).Rows(0).Item("SUNIT").ToString())
                                perdoller = perunit * 10
                            End If
                            LeftTdSetting(tdInner, FormatNumber(perdoller, 4), trInner, "AlterNateColor2")
                            nRow("MATERIAL COST") = FormatNumber(perdoller, 4).Replace(",", "")
                            trInner.Controls.Add(tdInner)
                        Case 6
                            tdInner = New TableCell
                            Dim perunit As New Decimal
                            Dim perdoller As New Decimal
                            If CDbl(dstbl.Tables(i).Rows(0).Item("SUNIT").ToString()) > 0 Then
                                perunit = CDbl(dstbl.Tables(i).Rows(0).Item("PL2").ToString()) * CDbl(dstbl.Tables(i).Rows(0).Item("SUNIT").ToString())
                                perdoller = perunit * 10
                            End If
                            LeftTdSetting(tdInner, FormatNumber(perdoller, 4), trInner, "AlterNateColor2")
                            nRow("VARIABLE LABOR") = FormatNumber(perdoller, 4).Replace(",", "")
                            trInner.Controls.Add(tdInner)
                        Case 7
                            tdInner = New TableCell
                            Dim perunit As New Decimal
                            Dim perdoller As New Decimal
                            If CDbl(dstbl.Tables(i).Rows(0).Item("SUNIT").ToString()) > 0 Then
                                perunit = CDbl(dstbl.Tables(i).Rows(0).Item("PL3").ToString()) * CDbl(dstbl.Tables(i).Rows(0).Item("SUNIT").ToString())
                                perdoller = perunit * 10
                            End If
                            LeftTdSetting(tdInner, FormatNumber(perdoller, 4), trInner, "AlterNateColor2")
                            nRow("VARIABLE ENERGY") = FormatNumber(perdoller, 4).Replace(",", "")
                            trInner.Controls.Add(tdInner)
                        Case 8
                            tdInner = New TableCell
                            Dim perunit As New Decimal
                            Dim perdoller As New Decimal
                            If CDbl(dstbl.Tables(i).Rows(0).Item("SUNIT").ToString()) > 0 Then
                                perunit = CDbl(dstbl.Tables(i).Rows(0).Item("PL4").ToString()) * CDbl(dstbl.Tables(i).Rows(0).Item("SUNIT").ToString())
                                perdoller = perunit * 10
                            End If
                            LeftTdSetting(tdInner, FormatNumber(perdoller, 4), trInner, "AlterNateColor2")
                            nRow("PLANT OVERHEAD") = FormatNumber(perdoller, 4).Replace(",", "")
                            trInner.Controls.Add(tdInner)
                        Case 9
                            tdInner = New TableCell
                            Dim perunit As New Decimal
                            Dim perdoller As New Decimal
                            If CDbl(dstbl.Tables(i).Rows(0).Item("SUNIT").ToString()) > 0 Then
                                perunit = CDbl(dstbl.Tables(i).Rows(0).Item("PL5").ToString()) * CDbl(dstbl.Tables(i).Rows(0).Item("SUNIT").ToString())
                                perdoller = perunit * 10
                            End If
                            LeftTdSetting(tdInner, FormatNumber(perdoller, 4), trInner, "AlterNateColor2")
                            nRow("DEPRECIATION") = FormatNumber(perdoller, 4).Replace(",", "")
                            trInner.Controls.Add(tdInner)
                        Case 10
                            tdInner = New TableCell
                            Dim perunit As New Decimal
                            Dim perdoller As New Decimal
                            If CDbl(dstbl.Tables(i).Rows(0).Item("SUNIT").ToString()) > 0 Then
                                perunit = CDbl(dstbl.Tables(i).Rows(0).Item("PL6").ToString()) * CDbl(dstbl.Tables(i).Rows(0).Item("SUNIT").ToString())
                                perdoller = perunit * 10
                            End If
                            LeftTdSetting(tdInner, FormatNumber(perdoller, 4), trInner, "AlterNateColor2")
                            nRow("DISTRIBUTION PACKAGING") = FormatNumber(perdoller, 4).Replace(",", "")
                            trInner.Controls.Add(tdInner)
                        Case 11
                            tdInner = New TableCell
                            Dim perunit As New Decimal
                            Dim perdoller As New Decimal
                            If CDbl(dstbl.Tables(i).Rows(0).Item("SUNIT").ToString()) > 0 Then
                                perunit = CDbl(dstbl.Tables(i).Rows(0).Item("PL7").ToString()) * CDbl(dstbl.Tables(i).Rows(0).Item("SUNIT").ToString())
                                perdoller = perunit * 10
                            End If
                            LeftTdSetting(tdInner, FormatNumber(perdoller, 4), trInner, "AlterNateColor2")
                            nRow("FREIGHT") = FormatNumber(perdoller, 4).Replace(",", "")
                            trInner.Controls.Add(tdInner)
                        Case 12
                            tdInner = New TableCell
                            Dim perunit As New Decimal
                            Dim perdoller As New Decimal
                            If CDbl(dstbl.Tables(i).Rows(0).Item("SUNIT").ToString()) > 0 Then
                                perunit = CDbl(dstbl.Tables(i).Rows(0).Item("PL8").ToString()) * CDbl(dstbl.Tables(i).Rows(0).Item("SUNIT").ToString())
                                perdoller = perunit * 10
                            End If
                            LeftTdSetting(tdInner, FormatNumber(perdoller, 4), trInner, "AlterNateColor2")
                            nRow("TOTAL COST") = FormatNumber(perdoller, 4).Replace(",", "")
                            trInner.Controls.Add(tdInner)
                    End Select

                Next
                dtEx.Rows.Add(nRow)
                If i <> 1 Then
                    If (j Mod 2 = 0) Then
                        trInner.CssClass = "AlterNateColor1"
                    Else
                        trInner.CssClass = "AlterNateColor2"
                    End If
                End If
                tblComparision.Controls.Add(trInner)

            Next
            dsEx.Tables.Add(dtEx)
            Session("dtEx") = dsEx
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
            _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetDeptDetails(ByRef lbl As Label, ByVal ProcId As Integer, ByVal CaseId As String)
        Dim Ds As New DataSet
        Dim ObjGetdata As New E1GetData.Selectdata()
        Dim Path As String = String.Empty
        Try
            Ds = ObjGetdata.GetDept(ProcId, "", "", CaseId)

            If Ds.Tables(0).Rows.Count = 0 Then
                lbl.Text = "Dept. Conflict"
                lbl.ForeColor = Drawing.Color.DarkRed
            Else
                lbl.Text = Ds.Tables(0).Rows(0).Item("PROCDE").ToString()
            End If

        Catch ex As Exception
            ErrorLable.Text = "Error:GetDeptDetails:" + ex.Message.ToString() + ""
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
