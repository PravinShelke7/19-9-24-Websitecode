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
Partial Class Pages_SavvyPackPro_Popup_PricePopup
    Inherits System.Web.UI.Page
    Dim objUpIns As New SavvyProUpInsData()
    Dim objGetData As New SavvyProGetData()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            Dim PriceOptionID As String = String.Empty
            PriceOptionID = Request.QueryString("PriceOptnID").ToString()
            If PriceOptionID = "1" Then
                GetPriceCost()
            Else
                GetPrice()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GetPrice()
        Dim trHeader As New TableRow
        Dim tdHeader As TableCell
        Dim trInner As New TableRow
        Dim tdInner As New TableCell
        Dim DsPriceData As New DataSet()
        Dim DtPriceData As New DataTable()
        Dim DvPriceData As New DataView()
        Dim DsSKUByRfpID As New DataSet()
        Dim TxtPrice As TextBox
        Try
            tblP.Rows.Clear()
            'DsPriceData = objGetData.GetPriceByRfpID(hidRfpID.Value)
            'DvPriceData = DsPriceData.Tables(0).DefaultView
            'DsSKUByRfpID = objGetData.GetSkuByRfpID(hidRfpID.Value)
            For k = 1 To 2
                trHeader = New TableRow
                For i = 1 To 3
                    If k = 1 Then
                        Select Case i
                            Case 1
                                tdHeader = New TableCell
                                HeaderTdSetting(tdHeader, "50px", "SKU #", "1")
                                trHeader.Controls.Add(tdHeader)
                            Case 2
                                tdHeader = New TableCell
                                HeaderTdSetting(tdHeader, "220px", "SKU Des", "1")
                                trHeader.Controls.Add(tdHeader)
                            Case 3
                                For j = 0 To 0
                                    tdHeader = New TableCell
                                    HeaderTdSetting(tdHeader, "120px", "PRICE", "1")
                                    trHeader.Controls.Add(tdHeader)
                                Next
                        End Select
                    ElseIf k = 2 Then
                        Select Case i
                            Case 1
                                tdHeader = New TableCell
                                HeaderTdSetting(tdHeader, "50px", "", "1")
                                trHeader.Controls.Add(tdHeader)
                            Case 2
                                tdHeader = New TableCell
                                HeaderTdSetting(tdHeader, "220px", "", "1")
                                trHeader.Controls.Add(tdHeader)
                            Case 3
                                For j = 0 To 0
                                    tdHeader = New TableCell
                                    Header2TdSetting(tdHeader, "120px", "(US$/M)", "1")
                                    trHeader.Controls.Add(tdHeader)
                                Next
                        End Select
                    End If
                Next
                trHeader.Height = 30
                tblP.Controls.Add(trHeader)
            Next

            'For Data
            For i = 0 To 0 'For Number of terms
                trInner = New TableRow
                For j = 0 To 2
                    Select Case j
                        Case 0
                            tdInner = New TableCell
                            tdInner.Text = "###"
                            InnerTdSetting(tdInner, "50px", "")
                            trInner.Controls.Add(tdInner)
                            trInner.Controls.Add(tdInner)
                        Case 1
                            tdInner = New TableCell
                            tdInner.Text = "###"
                            InnerTdSetting(tdInner, "350px", "")
                            trInner.Controls.Add(tdInner)
                        Case 2
                            For l = 0 To 0 'for number of vendors
                                tdInner = New TableCell
                                TxtPrice = New TextBox
                                Try
                                    TxtPrice.Text = ""
                                Catch ex As Exception
                                    TxtPrice.Text = ""
                                End Try
                                TxtPrice.Enabled = False
                                TxtPrice.Width = 50
                                TxtPrice.Height = 10
                                InnerTdSetting(tdInner, "120px", "")
                                tdInner.Controls.Add(TxtPrice)
                                trInner.Controls.Add(tdInner)
                            Next
                    End Select
                Next
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
                tblP.Controls.Add(trInner)
            Next
            'End          
        Catch ex As Exception
            lblError.Text = "Error:GetPricePageDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetPriceCost()
        Dim trHeader As New TableRow
        Dim tdHeader As TableCell
        Dim trInner As New TableRow
        Dim tdInner As New TableCell
        Dim DsPriceData As New DataSet()
        Dim DtPriceData As New DataTable()
        Dim DvPriceData As New DataView()
        Dim DsSKUByRfpID As New DataSet()
        Dim TxtPrice As TextBox
        Try
            tblP.Rows.Clear()
            DsPriceData = objGetData.GetPriceCosCol()
            For k = 1 To 2
                trHeader = New TableRow
                For i = 1 To 3
                    If k = 1 Then
                        Select Case i
                            Case 1
                                tdHeader = New TableCell
                                HeaderTdSetting(tdHeader, "50px", "SKU #", "1")
                                trHeader.Controls.Add(tdHeader)
                            Case 2
                                tdHeader = New TableCell
                                HeaderTdSetting(tdHeader, "220px", "SKU Des", "1")
                                trHeader.Controls.Add(tdHeader)
                            Case 3
                                For j = 0 To 15
                                    tdHeader = New TableCell
                                    HeaderTdSetting(tdHeader, "120px", "" + DsPriceData.Tables(0).Rows(0).Item("DES" + (j + 1).ToString()) + "", "1")
                                    trHeader.Controls.Add(tdHeader)
                                Next
                        End Select
                    ElseIf k = 2 Then
                        Select Case i
                            Case 1
                                tdHeader = New TableCell
                                HeaderTdSetting(tdHeader, "50px", "", "1")
                                trHeader.Controls.Add(tdHeader)
                            Case 2
                                tdHeader = New TableCell
                                HeaderTdSetting(tdHeader, "220px", "", "1")
                                trHeader.Controls.Add(tdHeader)
                            Case 3
                                For j = 0 To 15
                                    tdHeader = New TableCell
                                    Header2TdSetting(tdHeader, "120px", "(US$/M)", "1")
                                    trHeader.Controls.Add(tdHeader)
                                Next
                        End Select
                    End If
                Next
                trHeader.Height = 30
                tblP.Controls.Add(trHeader)
            Next

            'For Data
            For i = 0 To 2 'For Number of terms                    
                trInner = New TableRow
                For j = 0 To 2
                    Select Case j
                        Case 0
                            tdInner = New TableCell
                            tdInner.Text = "####"
                            InnerTdSetting(tdInner, "50px", "")
                            trInner.Controls.Add(tdInner)
                            trInner.Controls.Add(tdInner)
                        Case 1
                            tdInner = New TableCell
                            tdInner.Text = "####"
                            InnerTdSetting(tdInner, "220px", "")
                            trInner.Controls.Add(tdInner)
                        Case 2
                            For l = 0 To 15 'for number of vendors
                                tdInner = New TableCell
                                TxtPrice = New TextBox
                                Try
                                    TxtPrice.Text = ""
                                Catch ex As Exception
                                    TxtPrice.Text = ""
                                End Try
                                TxtPrice.Enabled = False
                                TxtPrice.Width = 50
                                TxtPrice.Height = 10
                                InnerTdSetting(tdInner, "120px", "")
                                tdInner.Controls.Add(TxtPrice)
                                trInner.Controls.Add(tdInner)
                            Next
                    End Select
                Next
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
                tblP.Controls.Add(trInner)
            Next
        Catch ex As Exception
            lblError.Text = "Error:GetPricePageDetails:" + ex.Message.ToString()
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
            lblError.Text = "Error:HeaderTdSetting:" + ex.Message.ToString()
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
            lblError.Text = "Error:HeaderTdSetting:" + ex.Message.ToString()
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
            lblError.Text = "Error:InnerTdSetting:" + ex.Message.ToString()
        End Try
    End Sub
End Class
