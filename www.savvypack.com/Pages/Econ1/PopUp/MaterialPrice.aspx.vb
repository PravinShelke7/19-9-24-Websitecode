Imports System.Data
Imports System.Data.OleDb
Imports System
Imports E1GetData
Imports E1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter

Partial Class Pages_Econ1_PopUp_MaterialPrice
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            hidMatId.Value = Request.QueryString("MatId").ToString()
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
        Dim dsData As New DataSet
        Dim trInner As New TableRow
        Dim tdInner As New TableCell
        Dim lbl As New Label
        Try
            ds = objGetData.GetMaterials(hidMatId.Value, "", "")
            dsData = objGetData.GetMatPriceInfo(hidMatId.Value, Session("E1CaseId").ToString())
            lblTitle.Text = "Material " + ds.Tables(0).Rows(0).Item("MATID").ToString() + ":" + ds.Tables(0).Rows(0).Item("MATDES").ToString()
            trHeader = New TableRow
            For i = 1 To 8
                tdHeader = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "50px", "Case Id", "1")
                        trHeader.Controls.Add(tdHeader)
                    Case 2
                        HeaderTdSetting(tdHeader, "300px", "Case Decription", "1")
                        trHeader.Controls.Add(tdHeader)
                    Case 3
                        HeaderTdSetting(tdHeader, "120px", "Case Creation Date", "1")
                        trHeader.Controls.Add(tdHeader)
                    Case 4
                        HeaderTdSetting(tdHeader, "70px", "Layer", "1")
                        trHeader.Controls.Add(tdHeader)
                    Case 5
                        HeaderTdSetting(tdHeader, "200px", "Material Description", "1")
                        trHeader.Controls.Add(tdHeader)
                    Case 6
                        HeaderTdSetting(tdHeader, "110px", "Effective Date", "1")
                        trHeader.Controls.Add(tdHeader)
                    Case 7
                        HeaderTdSetting(tdHeader, "100px", "Suggested Price ($/lb)", "1")
                        trHeader.Controls.Add(tdHeader)
                    Case 8
                        HeaderTdSetting(tdHeader, "100px", "Preferred Price ($/lb)", "1")
                        trHeader.Controls.Add(tdHeader)
                End Select
            Next
            trHeader.Height = 30
            tblData.Controls.Add(trHeader)

            'Inner
            For i = 0 To dsData.Tables(0).Rows.Count - 1
                trInner = New TableRow
                For j = 1 To 8
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
                            If dsData.Tables(0).Rows(i).Item("MATDES").ToString() <> "" Then
                                lbl.Text = dsData.Tables(0).Rows(i).Item("MATDES").ToString()
                            Else
                                lbl.Text = ds.Tables(0).Rows(0).Item("MATDES").ToString()
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 6
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            lbl.Text = dsData.Tables(0).Rows(i).Item("EFFDATE").ToString()
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 7
                            InnerTdSetting(tdInner, "", "Center")
                            lbl = New Label
                            lbl.Text = FormatNumber(dsData.Tables(0).Rows(i).Item("SUGGPRICE").ToString(), 3)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 8
                            InnerTdSetting(tdInner, "", "Center")
                            lbl = New Label
                            lbl.Text = FormatNumber(dsData.Tables(0).Rows(i).Item("PREFPRICE").ToString(), 3)
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
