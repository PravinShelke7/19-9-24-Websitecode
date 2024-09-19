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
Partial Class Pages_SavvyPackPro_BagMaking
    Inherits System.Web.UI.Page
    Dim objGetdata As New SavvyProGetData
    Dim objUpIns As New SavvyProUpInsData
    'Investment
    Dim BMDes(50) As String
    Dim PCval(50) As String
    Dim CapVal(50) As String
    Dim FreeVal(50) As String
    Dim MacVal(50) As String

    Dim CylVal(50) As String
    Dim SizeTotal As New Integer
    Dim SizeMTotal As New Integer
    Dim ColTotal As New Integer
    Dim CylTotal As New Integer
    'End Investment
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        GetBagMakingDetails()
    End Sub
#Region "Bag Making"

    Public Sub GetBagMakingDetails()
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
        Dim dsEquip As DataSet
        Dim DsColumn As DataSet

        Try
            tblBagM.Rows.Clear()
            'dsEquip = Session("dsEquip")
            ' dsEquip = objGetData.GetEquipmentDetails()
            DsColumn = objGetdata.GetBagMakingCol(Session("USERID"))
            dsData = objGetdata.GetBagMakingDet(Session("USERID"))
            If hidRowNum.Value <> "2" Then
                count = hidRowNum.Value
            Else
                If dsData.Tables(0).Rows.Count > 0 Then
                    count = dsData.Tables(0).Rows.Count + 1
                    hidRowNum.Value = dsData.Tables(0).Rows.Count + 1
                Else
                    count = hidRowNum.Value
                End If
            End If



            trHeader = New TableRow
            For i = 1 To 5
                tdHeader = New TableCell
                tdHeader1 = New TableCell

                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "200px", DsColumn.Tables(0).Rows(0).Item(i - 1).ToString(), "1")
                        HeaderTdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 2
                        HeaderTdSetting(tdHeader, "180px", DsColumn.Tables(0).Rows(0).Item(i - 1).ToString(), "1")
                        HeaderTdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 3
                        HeaderTdSetting(tdHeader, "180px", DsColumn.Tables(0).Rows(0).Item(i - 1).ToString(), "1")
                        HeaderTdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 4
                        HeaderTdSetting(tdHeader, "180px", DsColumn.Tables(0).Rows(0).Item(i - 1).ToString(), "1")
                        HeaderTdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 5
                        HeaderTdSetting(tdHeader, "180px", DsColumn.Tables(0).Rows(0).Item(i - 1).ToString(), "1")
                        HeaderTdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)

                End Select
            Next
            tblBagM.Controls.Add(trHeader)
            tblBagM.Controls.Add(trHeader1)

            For i = 1 To count   'dsData.Tables(0).Rows.Count - 1
                trInner = New TableRow
                For j = 0 To 5
                    tdInner = New TableCell
                    If j = 0 Then
                        InnerTdSetting(tdInner, "20px", "Center")
                        txt = New TextBox
                        txt.CssClass = "MatSmallTextBox"
                        txt.ID = "txtBagMakingDes" + i.ToString()
                        If i = count Then
                            'tdInner.Text = "Total"
                        Else
                            If BMDes(i - 1) <> "" Then
                                txt.Text = BMDes(i - 1)
                            Else

                                If i > count Then
                                    txt.Text = ""
                                Else
                                    If dsData.Tables(0).Rows.Count >= i Then
                                        If dsData.Tables(0).Rows(i - 1).Item("BAGMAKINGDES").ToString() <> "" Then
                                            txt.Text = dsData.Tables(0).Rows(i - 1).Item("BAGMAKINGDES").ToString()
                                        Else
                                            'txt.Text = "description " + i.ToString()
                                        End If
                                    Else
                                        'txt.Text = "description " + i.ToString()
                                    End If
                                End If
                            End If
                            tdInner.Controls.Add(txt)
                        End If

                    ElseIf j = 1 Then
                        InnerTdSetting(tdInner, "60px", "Right")
                        txt = New TextBox
                        txt.CssClass = "MatSmallTextBox"
                        txt.ID = "txtPCFORMATSPRODUCED" + i.ToString()
                        If i = count Then
                            'tdInner.Text = TotalSize.ToString()
                            'SizeTotal = TotalSize
                        Else
                            If PCval(i - 1) <> "" Then
                                txt.Text = PCval(i - 1)
                            Else
                                If i = count Then

                                Else
                                    If dsData.Tables(0).Rows.Count >= i Then
                                        If dsData.Tables(0).Rows(i - 1).Item("PCFORMATSPRODUCED").ToString() <> "" Then
                                            txt.Text = dsData.Tables(0).Rows(i - 1).Item("PCFORMATSPRODUCED").ToString()
                                            'TotalSize = TotalSize + dsData.Tables(0).Rows(i - 1).Item("PCFORMATSPRODUCED")
                                        Else
                                            txt.Text = ""
                                        End If
                                    Else
                                        txt.Text = ""
                                    End If
                                End If
                            End If
                            'txt.Text = "Design " + (i + 1).ToString()
                            tdInner.Controls.Add(txt)
                        End If


                    ElseIf j = 2 Then
                        InnerTdSetting(tdInner, "90px", "Right")
                        txt = New TextBox
                        txt.CssClass = "MatSmallTextBox"
                        txt.ID = "txtCAPACITYUNITS" + i.ToString()
                        If i = count Then

                        Else
                            If CapVal(i - 1) <> "" Then
                                txt.Text = CapVal(i - 1)
                            Else
                                If i = count Then


                                Else
                                    If dsData.Tables(0).Rows.Count >= i Then
                                        If dsData.Tables(0).Rows(i - 1).Item("CAPACITYUNITS").ToString() <> "" Then
                                            txt.Text = dsData.Tables(0).Rows(i - 1).Item("CAPACITYUNITS").ToString()
                                            'TotalCol = TotalCol + dsData.Tables(0).Rows(i - 1).Item("COLVAL")
                                        Else
                                            txt.Text = ""
                                        End If
                                    Else
                                        txt.Text = ""
                                    End If
                                End If
                            End If
                            'txt.Text = "Design " + (i + 1).ToString()
                            tdInner.Controls.Add(txt)
                        End If


                    ElseIf j = 3 Then
                        InnerTdSetting(tdInner, "90px", "Right")
                        txt = New TextBox
                        txt.CssClass = "MatSmallTextBox"
                        txt.ID = "txtFREECAPACITY" + i.ToString()
                        If i = count Then

                        Else
                            If FreeVal(i - 1) <> "" Then
                                txt.Text = FreeVal(i - 1)
                            Else
                                If i = count Then

                                Else
                                    If dsData.Tables(0).Rows.Count >= i Then
                                        If dsData.Tables(0).Rows(i - 1).Item("FREECAPACITY").ToString() <> "" Then
                                            txt.Text = dsData.Tables(0).Rows(i - 1).Item("FREECAPACITY").ToString()
                                            'TotalCol = TotalCol + dsData.Tables(0).Rows(i - 1).Item("COLVAL")
                                        Else
                                            txt.Text = ""
                                        End If
                                    Else
                                        txt.Text = ""
                                    End If
                                End If
                            End If
                            'txt.Text = "Design " + (i + 1).ToString()
                            tdInner.Controls.Add(txt)
                        End If
                    ElseIf j = 4 Then
                        InnerTdSetting(tdInner, "90px", "Right")
                        txt = New TextBox
                        txt.CssClass = "MatSmallTextBox"
                        txt.ID = "txtMACHINEDATED" + i.ToString()
                        If i = count Then

                        Else
                            If MacVal(i - 1) <> "" Then
                                txt.Text = MacVal(i - 1)
                            Else
                                If i = count Then

                                Else
                                    If dsData.Tables(0).Rows.Count >= i Then
                                        If dsData.Tables(0).Rows(i - 1).Item("MACHINEDATED").ToString() <> "" Then
                                            txt.Text = dsData.Tables(0).Rows(i - 1).Item("MACHINEDATED").ToString()
                                            'TotalCol = TotalCol + dsData.Tables(0).Rows(i - 1).Item("COLVAL")
                                        Else
                                            txt.Text = ""
                                        End If
                                    Else
                                        txt.Text = ""
                                    End If
                                End If
                            End If
                            'txt.Text = "Design " + (i + 1).ToString()
                            tdInner.Controls.Add(txt)
                        End If

                    Else
                        InnerTdSetting(tdInner, "", "Left")
                        InnerTdSetting(tdInner, "", "Center")
                        lbl = New Label
                        lnkbtn = New LinkButton
                        lnkbtn.ID = "lnkAdd" + i.ToString()
                        lnkbtn.Text = "Add More+"
                        lnkbtn.Width = 80
                        lnkbtn.Height = 20
                        'lbl.ID = "lbl" + i.ToString()

                        If i = count Then

                            tdInner.Style.Add("display", "")

                        Else
                            tdInner.Style.Add("display", "none")

                        End If
                        AddHandler lnkbtn.Click, AddressOf LinkButton_Click
                        tdInner.Controls.Add(lnkbtn)
                        trInner.Controls.Add(tdInner)
                    End If
                    trInner.Controls.Add(tdInner)
                Next
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If

                If i > count Then
                    trInner.Style.Add("display", "none")
                End If

                tblBagM.Controls.Add(trInner)
            Next

        Catch ex As Exception

        End Try
    End Sub

    Private Sub LinkButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim count As Integer = Convert.ToInt32(hidRowNum.Value.ToString())
            For i = 0 To count
                BMDes(i) = Request.Form("txtBagMakingDes" + (i + 1).ToString() + "")
                PCval(i) = Request.Form("txtPCFORMATSPRODUCED" + (i + 1).ToString() + "")
                CapVal(i) = Request.Form("txtCAPACITYUNITS" + (i + 1).ToString() + "")
                FreeVal(i) = Request.Form("txtFREECAPACITY" + (i + 1).ToString() + "")
                MacVal(i) = Request.Form("txtMACHINEDATED" + (i + 1).ToString() + "")

            Next
            'Session("Linkbuttonclicked") = "Addmore"
            Dim numberDiv As Integer
            numberDiv = Convert.ToInt16(hidRowNum.Value.ToString())
            numberDiv += 1
            hidRowNum.Value = numberDiv.ToString()
            GetBagMakingDetails()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnupdate_Click(sender As Object, e As System.EventArgs) Handles btnUpdate.Click
        Dim count As Integer = Convert.ToInt32(hidRowNum.Value.ToString())
        Dim BGdes(count) As String
        Dim PCFormat(count) As String
        Dim Capacity(count) As String
        Dim FCapacity(count) As String
        Dim MachineDated(count) As String
        For i = 0 To count
            BGdes(i) = Request.Form("txtBagMakingDes" + (i + 1).ToString() + "")
            PCFormat(i) = Request.Form("txtPCFORMATSPRODUCED" + (i + 1).ToString() + "")
            Capacity(i) = Request.Form("txtCAPACITYUNITS" + (i + 1).ToString() + "")
            FCapacity(i) = Request.Form("txtFREECAPACITY" + (i + 1).ToString() + "")
            MachineDated(i) = Request.Form("txtMACHINEDATED" + (i + 1).ToString() + "")
        Next
        objUpIns.UpdateBagMaking(BGdes, PCFormat, Capacity, FCapacity, MachineDated, "50001", "1")
    End Sub

#End Region

#Region "Formatting"

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

#End Region
End Class
