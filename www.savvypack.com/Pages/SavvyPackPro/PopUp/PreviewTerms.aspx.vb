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
Partial Class Pages_SavvyPackPro_Popup_PreviewTerms
    Inherits System.Web.UI.Page
    Dim objUpIns As New SavvyProUpInsData()
    Dim objGetData As New SavvyProGetData()
    Dim SpecId As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        Try
            hidSpecId.Value = Request.QueryString.Item("SUId")
            GetTermsDetails()

        Catch ex As Exception
        End Try

    End Sub

    Public Sub GetTermsDetails()
        Dim ds As New DataSet
        Dim tdHeader As New TableCell
        Dim trHeader As New TableRow
        Dim tdHeader1 As New TableCell
        Dim trHeader1 As New TableRow
        Dim dsData As New DataSet
        Dim trInner As New TableRow
        Dim tdInner As New TableCell
        Dim lbl As New Label
        Dim dsPref As New DataSet
        Dim dsMat As New DataSet
        Dim rdb As New RadioButton
        Dim val As New Double
        Dim dvData As New DataView
        Dim dtData As New DataTable
        Dim dsCyl As New DataSet
        Dim dsDataT As New DataSet
        Dim txt As New TextBox
        Dim txtarea As New HtmlTextArea
        Dim lnkbtn As New LinkButton
        Dim count As New Integer
        Dim TotalCol As New Double
        Dim TotalSize As New Double
        Dim TotalCyl As New Double
        Dim dsDataS As New DataSet
        Dim dsDataC As New DataSet

        Try
            lbltermC.Visible = False
            tblterm.Rows.Clear()
            tbltermC.Rows.Clear()
            dsDataS = objGetData.GetTermsPrevNew(Session("hidRfpId"), Session("USERID"), "Y")
            dsDataC = objGetData.GetTermsPrevNew(Session("hidRfpId"), Session("USERID"), "N")
            For i = 0 To dsDataS.Tables(0).Rows.Count - 1
                trInner = New TableRow
                For b = 1 To 2
                    Select Case b
                        Case 1
                            tdInner = New TableCell
                            lbl = New Label
                            InnerTdSetting(tdInner, "350px", "left")
                            lbl.Font.Bold = True
                            lbl.CssClass = "NormalLable_Term"
                            lbl.Text = dsDataS.Tables(0).Rows(i).Item("TITLE").ToString() + "  : "
                            lbl.Style.Add("width", "350px")
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)

                        Case 2
                            tdInner = New TableCell
                            InnerTdSetting(tdInner, "350px", "left")
                            lbl = New Label
                            lbl.CssClass = "NormalLable_Term"
                            lbl.Text = dsDataS.Tables(0).Rows(i).Item("DESCRIPTION").ToString()
                            lbl.Style.Add("width", "350px")
                            tdInner.Controls.Add(lbl)
                            'HeaderTdSetting(tdInner, "200px", dsData.Tables(0).Rows(a).Item(k).ToString(), "1")
                            trInner.Controls.Add(tdInner)
                    End Select
                Next
                tblterm.Controls.Add(trInner)
            Next

            If dsDataC.Tables(0).Rows.Count > 0 Then
                lbltermC.Visible = True
                For i = 0 To dsDataC.Tables(0).Rows.Count - 1
                    trInner = New TableRow
                    For b = 1 To 2
                        Select Case b
                            Case 1
                                tdInner = New TableCell
                                lbl = New Label
                                InnerTdSetting(tdInner, "350px", "left")
                                lbl.Font.Bold = True
                                lbl.CssClass = "NormalLable_Term"
                                lbl.Text = dsDataC.Tables(0).Rows(i).Item("TITLE").ToString() + "  : "
                                lbl.Style.Add("width", "350px")
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)

                            Case 2
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "350px", "left")
                                lbl = New Label
                                lbl.CssClass = "NormalLable_Term"
                                lbl.Text = dsDataC.Tables(0).Rows(i).Item("DESCRIPTION").ToString()
                                lbl.Style.Add("width", "350px")
                                tdInner.Controls.Add(lbl)
                                'HeaderTdSetting(tdInner, "200px", dsData.Tables(0).Rows(a).Item(k).ToString(), "1")
                                trInner.Controls.Add(tdInner)
                        End Select
                    Next
                    tbltermC.Controls.Add(trInner)
                Next
            End If
        Catch ex As Exception
            lblError.Text = "Error: GetTermsDetails() " + ex.Message()
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
