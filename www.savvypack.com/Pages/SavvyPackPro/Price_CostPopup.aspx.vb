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
Partial Class Pages_SavvyPackPro_Popup_Price_CostPopup
    Inherits System.Web.UI.Page
    Dim objUpIns As New SavvyProUpInsData()
    Dim objGetData As New SavvyProGetData()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            GetPriceCost()
        Catch ex As Exception

        End Try

    End Sub
    Protected Sub GetPriceCost()
        Dim ds As New DataSet
        Dim i As New Integer
        Dim j As New Integer
        Dim DWidth As String = String.Empty
        Dim trHeader As New TableRow
        Dim trHeader1 As New TableRow
        Dim trHeader2 As New TableRow

        Dim trInner As New TableRow

        Dim tdHeader As TableCell
        Dim tdHeader1 As TableCell
        Dim tdHeader2 As TableCell


        Dim lbl As New Label
        Dim hid As New HiddenField
        Dim Link As New HyperLink
        Dim txt As New TextBox
        Dim tdInner As TableCell
        Dim ddl As New DropDownList
        Dim strVol As Double = 0.0
        Dim dp As DataSet
        Try
            ds = objGetData.GetPriceCost()
            dp = objGetData.GetPrice(Session("hidRfpID"), Session("UserId"))
            Session("SPrice") = dp
            lblSKU.Text = "####" 'ds.Tables(0).Rows(0).Item("SKUID".ToString() + "").ToString()
            lblSKUDesc.Text = ""


            For i = 1 To 2
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "200px", "", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 2
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE2").ToString() + "/M" + ")"
                        HeaderTdSetting(tdHeader, "120px", "Total", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)

                End Select
            Next
            tblPC.Controls.Add(trHeader)
            tblPC.Controls.Add(trHeader2)
            tblPC.Controls.Add(trHeader1)


            For i = 1 To 16
                If i <> 17 Then
                    trInner = New TableRow
                    For j = 1 To 2
                        tdInner = New TableCell
                        Select Case j
                            Case 1
                                'Layer
                                InnerTdSetting(tdInner, "", "Left")
                                If i = 1 Or i = 2 Then
                                    tdInner.Text = "<b>" + ds.Tables(0).Rows(0).Item("PS" + i.ToString() + "").ToString() + "</b>"
                                ElseIf i = 8 Or i = 12 Or i = 16 Then
                                    tdInner.Text = "<b>" + ds.Tables(0).Rows(0).Item("PDES" + i.ToString() + "").ToString() + "</b>"
                                Else
                                    tdInner.Text = "<span style='margin-left:20px;'><b>  " + ds.Tables(0).Rows(0).Item("PDES" + i.ToString() + "").ToString() + "</b></span>"
                                End If

                                trInner.Controls.Add(tdInner)
                            Case 2
                                InnerTdSetting(tdInner, "", "Left")
                                lbl = New Label()
                                If i = 1 Then
                                    lbl.ID = "txtP" + i.ToString()

                                    lbl.Text = "####"

                                    lbl.Width = 100
                                    'lbl.CssClass = "SmallTextBox"
                                    tdInner.Controls.Add(lbl)
                                    trInner.Controls.Add(tdInner)
                                ElseIf i = 2 Then
                                    lbl.ID = "txtS" + i.ToString()

                                    lbl.Text = "####"

                                    lbl.Width = 100
                                    'lbl.CssClass = "SmallTextBox"
                                    tdInner.Controls.Add(lbl)
                                    trInner.Controls.Add(tdInner)
                                Else
                                    'lbl = New Label()
                                    lbl.ID = "txtM" + i.ToString()
                                    lbl.Width = 100
                                    'lbl.CssClass = "SmallTextBox"
                                    tdInner.Controls.Add(lbl)
                                    trInner.Controls.Add(tdInner)
                                End If

                        End Select
                    Next


                    tblPC.Controls.Add(trInner)
                End If
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
            Next
        Catch ex As Exception
            lblError.Text = "Error:GetPriceCost:" + ex.Message.ToString()
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
