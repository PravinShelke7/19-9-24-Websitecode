Imports System.Data
Imports System.Data.OleDb
Imports System
Imports E1GetData
Imports E1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter

Partial Class Pages_Econ1_DownTimeInfo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            hidMatId.Value = Request.QueryString("EqId").ToString()
            If Not IsPostBack Then
                GetPageDetails()
            End If

        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    Public Sub GetPageDetails()
        Dim ds As New DataSet
        Dim objGetData As New Selectdata()
        Dim tdHeader As New TableCell
        Dim trHeader As New TableRow
        Dim tdHeader1 As New TableCell
        Dim trHeader1 As New TableRow
        Dim dsData As New DataSet
        Dim trInner As New TableRow
        Dim tdInner As New TableCell
        Dim lbl As New Label
        Dim dsPref As New DataSet
        Try
            ds = objGetData.GetEquipment(hidMatId.Value, "", "")
            dsData = objGetData.GetDownTimeInfo(hidMatId.Value)
            dsPref = objGetData.GetPref(Session("E1CaseId"))
            lblTitle.Text = "Equipment " + ds.Tables(0).Rows(0).Item("EQUIPID").ToString() + ": " + ds.Tables(0).Rows(0).Item("EQUIPDES").ToString()
            trHeader = New TableRow
            For i = 1 To 10
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "50px", "Case Id", "1")
                        Header2TdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 2
                        HeaderTdSetting(tdHeader, "300px", "Case Description", "1")
                        Header2TdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 3
                        HeaderTdSetting(tdHeader, "120px", "Case Creation Date", "1")
                        Header2TdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 4
                        HeaderTdSetting(tdHeader, "70px", "Layer", "1")
                        Header2TdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 5
                        HeaderTdSetting(tdHeader, "", "Instantaneous Rate", "3")
                        Header2TdSetting(tdHeader1, "70px", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 6
                        Header2TdSetting(tdHeader1, "60px", "Units", "1")
                        trHeader1.Controls.Add(tdHeader1)
                    Case 7
                        Title = "(" + dsPref.Tables(0).Rows(0).Item("TITLE8").ToString() + "/hr)"
                        Header2TdSetting(tdHeader1, "70px", Title, "1")
                        trHeader1.Controls.Add(tdHeader1)
                    Case 8
                        HeaderTdSetting(tdHeader, "90px", "Downtime", "1")
                        Header2TdSetting(tdHeader1, "", "(%)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 9
                        HeaderTdSetting(tdHeader, "100px", "Production Waste", "1")
                        Header2TdSetting(tdHeader1, "", "(%)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 10
                        HeaderTdSetting(tdHeader, "100px", "Design Waste", "1")
                        Header2TdSetting(tdHeader1, "", "(%)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)

                End Select
            Next
            trHeader.Height = 30
            tblData.Controls.Add(trHeader)
            tblData.Controls.Add(trHeader1)

            'Inner
            For i = 0 To dsData.Tables(0).Rows.Count - 1
                trInner = New TableRow
                For j = 1 To 10
                    tdInner = New TableCell
                    Select Case j
                        Case 1
                            InnerTdSetting(tdInner, "", "Center")
                            lbl = New Label
                            lbl.Text = dsData.Tables(0).Rows(i).Item("CASEID").ToString()
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 2
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            lbl.Text = dsData.Tables(0).Rows(i).Item("CASEDES").ToString()
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 3
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            lbl.Text = dsData.Tables(0).Rows(i).Item("CREATIONDATE").ToString()
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 4
                            InnerTdSetting(tdInner, "", "Center")
                            lbl = New Label
                            lbl.Text = dsData.Tables(0).Rows(i).Item("MATLAYER").ToString()
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 5
                            InnerTdSetting(tdInner, "", "Center")
                            lbl = New Label
                            lbl.Text = FormatNumber(dsData.Tables(0).Rows(i).Item("OPINSTR").ToString(), 2)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 6
                            InnerTdSetting(tdInner, "", "Center")
                            lbl = New Label
                            lbl.Text = dsData.Tables(0).Rows(i).Item("UNITS").ToString()
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 7
                            InnerTdSetting(tdInner, "", "Center")
                            lbl = New Label
                            lbl.Text = FormatNumber(dsData.Tables(0).Rows(i).Item("INSTR").ToString(), 2)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 8
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            lbl.Text = dsData.Tables(0).Rows(i).Item("DTVAL").ToString()
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 9
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            lbl.Text = dsData.Tables(0).Rows(i).Item("OWASTE").ToString()
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 10
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            lbl.Text = dsData.Tables(0).Rows(i).Item("WASTE").ToString()
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
                tblData.Controls.Add(trInner)
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


End Class
