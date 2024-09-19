Imports System.Data
Imports System.Data.OleDb
Imports System
Imports E1GetData
Imports E1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_SavvyPackPro_Resultspl
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not IsPostBack Then
                GetPageDetails()
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString() + ""
        End Try
    End Sub


    Protected Sub GetPageDetails()
        Dim ds As New DataSet
        Dim objGetData As New E1GetData.Selectdata
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
        Try
            ds = objGetData.GetPriceCost()
            lblSKU.Text = ds.Tables(0).Rows(0).Item("SKUID".ToString() + "").ToString()
            lblSKUDesc.Text = ds.Tables(0).Rows(0).Item("SKUDESC".ToString() + "").ToString()
          

            For i = 1 To 8
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
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE2").ToString() + ")"
                        HeaderTdSetting(tdHeader, "120px", "Total", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 3
                        HeaderTdSetting(tdHeader, "90px", "Total", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        Header2TdSetting(tdHeader2, "", "(%)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 4
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE2").ToString() + "/" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                        HeaderTdSetting(tdHeader, "90px", "By Weight", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 5
                        Title = "(" + ds.Tables(0).Rows(0).Item("PUN").ToString() + ")"
                        HeaderTdSetting(tdHeader, "90px", "By Volume", "1")
                        HeaderTdSetting(tdHeader1, "90px", "", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 6
                        'Title = "(" + ds.Tables(0).Rows(0).Item("TITLE2").ToString() + "/" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                        If ds.Tables(0).Rows(0).Item("UNITTYPE").ToString() = 0 Then
                            Title = "(" + ds.Tables(0).Rows(0).Item("TITLE2").ToString() + "/" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                        ElseIf ds.Tables(0).Rows(0).Item("UNITTYPE").ToString() = 1 Then
                            Title = "(" + ds.Tables(0).Rows(0).Item("PUN").ToString() + ")"
                        ElseIf ds.Tables(0).Rows(0).Item("UNITTYPE").ToString() = 2 Then
                            Title = "(" + ds.Tables(0).Rows(0).Item("TITLE2").ToString() + "/thousand)"
                        End If
                        HeaderTdSetting(tdHeader, "", "Change Price", "3")
                        Header2TdSetting(tdHeader2, "70px", "Suggested", "1")
                        Header2TdSetting(tdHeader1, "", Title, "3")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader1)
                        trHeader1.Controls.Add(tdHeader2)
                    Case 7
                        Header2TdSetting(tdHeader1, "70px", "Preferred", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 8
                        Header2TdSetting(tdHeader1, "70px", "Unit", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)


                End Select

            Next
            tblComparision.Controls.Add(trHeader)
            tblComparision.Controls.Add(trHeader2)
            tblComparision.Controls.Add(trHeader1)


            For i = 1 To 16
                If i <> 17 Then
                    trInner = New TableRow
                    For j = 1 To 8
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
                                    txt = New TextBox
                                    txt.ID = "txtP" + i.ToString()
                                    txt.Width = 100
                                    txt.CssClass = "SmallTextBox"
                                    tdInner.Controls.Add(txt)
                                    trInner.Controls.Add(tdInner)
                                ElseIf i = 2 Then
                                    txt = New TextBox
                                    txt.ID = "txtS" + i.ToString()
                                    txt.Width = 100
                                    txt.CssClass = "SmallTextBox"
                                    tdInner.Controls.Add(txt)
                                    trInner.Controls.Add(tdInner)
                                Else
                                    lbl = New Label()
                                    ' tdInner.Text = "<b>" + ds.Tables(0).Rows(0).Item("PS" + i.ToString() + "").ToString() + "</b>"
                                    lbl.Text = FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()))
                                    tdInner.Controls.Add(lbl)
                                    trInner.Controls.Add(tdInner)
                                End If


                            Case 3
                                Dim percentage As New Decimal
                                InnerTdSetting(tdInner, "", "Right")
                                lbl = New Label()
                                If i > 2 Then
                                    lbl.Text = ds.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()
                                Else
                                    lbl.Text = ""
                                End If
                               
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Case 4
                                InnerTdSetting(tdInner, "", "Right")
                                Dim perwt As New Decimal
                                lbl = New Label()
                                If i > 2 Then
                                    lbl.Text = ds.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()
                                Else
                                    lbl.Text = ""
                                End If
                              
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Case 5
                                InnerTdSetting(tdInner, "", "Right")
                                Dim perunit As New Decimal
                                lbl = New Label()
                                If i > 2 Then
                                    lbl.Text = ds.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()
                                Else
                                    lbl.Text = ""
                                End If
                              
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Case 6
                                InnerTdSetting(tdInner, "", "Right")
                                lbl = New Label()
                              
                                If i = 3 Then
                                    If CInt(ds.Tables(0).Rows(0).Item("UNITTYPE").ToString()) = 0 Then
                                        lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("UNITPS").ToString(), 3)
                                    ElseIf CInt(ds.Tables(0).Rows(0).Item("UNITTYPE").ToString()) = 1 Then
                                        strVol = CDbl(ds.Tables(0).Rows(0).Item("UNITPS").ToString()) * ((CDbl(ds.Tables(0).Rows(0).Item("SVOLUME").ToString()) * 100) / CDbl(ds.Tables(0).Rows(0).Item("SUNITVAL").ToString()))
                                        lbl.Text = FormatNumber(strVol, 3)
                                    ElseIf CInt(ds.Tables(0).Rows(0).Item("UNITTYPE").ToString()) = 2 Then
                                        strVol = ((CDbl(ds.Tables(0).Rows(0).Item("UNITPS").ToString()) * (CDbl(ds.Tables(0).Rows(0).Item("SVOLUME").ToString()))) / CDbl(ds.Tables(0).Rows(0).Item("SUNITVAL").ToString())) * 1000
                                        lbl.Text = FormatNumber(strVol, 3)
                                    End If
                                Else
                                    lbl.Text = ""
                                End If

                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Case 7
                                InnerTdSetting(tdInner, "", "Center")
                                txt = New TextBox
                                txt.CssClass = "SmallTextBox"
                                txt.ID = "UNITPP"

                                txt.MaxLength = 6
                                If i = 3 Then
                                    If CInt(ds.Tables(0).Rows(0).Item("UNITTYPE").ToString()) = 0 Then
                                        txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("UNITPP").ToString(), 3)
                                    ElseIf CInt(ds.Tables(0).Rows(0).Item("UNITTYPE").ToString()) = 1 Then
                                        strVol = CDbl(ds.Tables(0).Rows(0).Item("UNITPP").ToString()) * ((CDbl(ds.Tables(0).Rows(0).Item("SVOLUME").ToString()) * 100) / CDbl(ds.Tables(0).Rows(0).Item("SUNITVAL").ToString()))
                                        txt.Text = FormatNumber(strVol, 3)
                                    ElseIf CInt(ds.Tables(0).Rows(0).Item("UNITTYPE").ToString()) = 2 Then
                                        strVol = ((CDbl(ds.Tables(0).Rows(0).Item("UNITPP").ToString()) * (CDbl(ds.Tables(0).Rows(0).Item("SVOLUME").ToString()))) / CDbl(ds.Tables(0).Rows(0).Item("SUNITVAL").ToString())) * 1000
                                        txt.Text = FormatNumber(strVol, 3)
                                    End If
                                    tdInner.Controls.Add(txt)
                                Else
                                    lbl.Text = ""
                                End If
                                trInner.Controls.Add(tdInner)
                            Case 8
                                InnerTdSetting(tdInner, "", "Center")
                                ddl = New DropDownList()
                                If i = 3 Then
                                    BindUnitType(ddl, ds.Tables(0).Rows(0).Item("UNITTYPE").ToString())
                                    tdInner.Controls.Add(ddl)
                                Else
                                    lbl.Text = ""
                                End If
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
    Protected Sub BindUnitType(ByVal ddl As DropDownList, ByVal unitType As String)
        Dim objGetData As New E1GetData.Selectdata()
        Dim dts As New DataSet()
        Try
            dts = objGetData.getUnits("97047")
            ddl.CssClass = "DropDownConT"
            ddl.ID = "UNITTYPE"
            'Binding Dropdown
            With ddl
                .DataSource = dts
                .DataTextField = "UNIT"
                .DataValueField = "VAL"
                .DataBind()
            End With
            ddl.SelectedValue = unitType
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
