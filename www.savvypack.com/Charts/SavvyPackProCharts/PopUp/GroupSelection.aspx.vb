Imports System.Data
Imports System.Data.OleDb
Imports System
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_SavvyPackPro_Charts_PopUp_Group_Selection
    Inherits System.Web.UI.Page
    Dim objGetdata As New SavvyProGetData
    Dim objUpIns As New SavvyProUpInsData
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            hidGroupId.Value = Request.QueryString("Des1").ToString()
            hidGroupDes.Value = Request.QueryString("Id").ToString()
            hidlnkdes.Value = Request.QueryString("Des").ToString()
            If Not IsPostBack Then
                ' BindPRICETable()
            End If
            ChkRfpPriceDetails()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ChkRfpPriceDetails()
        Dim ds As New DataSet
        Dim flag As Integer = 0
        Try
            If Session("RFPPRICEID") = Nothing Then
                lblGroupNoFound.Visible = True

            Else
                lblGroupNoFound.Visible = False
                ds = objGetdata.GetMasterDataN(Session("RFPPRICEID"))
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    If ds.Tables(0).Rows(i).Item("MTYPEID").ToString() = "5" Then
                        flag = 0
                    ElseIf ds.Tables(0).Rows(i).Item("MTYPEID").ToString() = "1" Then
                        flag = 1
                    End If
                Next
                If flag = 0 Then
                    BindPRICETableSKUlevel()
                ElseIf flag = 1 Then
                    BindPRICETable()
                End If
            End If

        Catch ex As Exception
            ' _lErrorLble.Text = "Error:GetCaseDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub BindPRICETableSKUlevel()
        Dim ds As New DataSet
        Dim dsM As New DataSet
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
        Dim Link As New LinkButton
        Dim txt As New TextBox
        Dim tdInner As TableCell
        Dim objGetData As New SavvyProGetData()
        Dim dsP As New DataSet
        Dim code As String
        Try
            tblPRICE.Rows.Clear()

            lblGroupNoFound.Visible = False
            ds = objGetData.GetSKUPriceQry(Session("RFPPRICEID"))
            ' dssku = objGetData.GetRFPSKU(Session("RFPPRICEID"))
            dsM = objGetData.GetMasterDataN(Session("RFPPRICEID"))
            Session("DS") = ds
            Session("DSM") = dsM
            dsP = objGetData.GetRFPPRICE(Session("RFPPRICEID"))

            For i = 1 To dsM.Tables(0).Rows.Count

                Dim Title As String = String.Empty
                'Header
                If dsM.Tables(0).Rows(i - 1).Item("MTYPEID").ToString() = "5" Then
                    ' HeaderTdSetting(tdHeader, "130px", "", "1")

                    tdHeader = New TableCell
                    HeaderTdSetting(tdHeader, "310px", "", "1")
                    lbl = New Label
                    hid = New HiddenField
                    ' Link.CssClass = "LinkM"
                    lbl.Text = dsM.Tables(0).Rows(i - 1).Item("DESCRIPTION")
                    tdHeader.Controls.Add(lbl)

                    trHeader.Controls.Add(tdHeader)
                End If



            Next
            tblPRICE.Controls.Add(trHeader)



            'Inner
            For i = 1 To ds.Tables(0).Rows.Count
                trInner = New TableRow

                For j = 1 To dsM.Tables(0).Rows.Count
                    tdInner = New TableCell
                    InnerTdSetting(tdInner, "", "Left")
                    If dsM.Tables(0).Rows(j - 1).Item("TBLNAME").ToString() = "PRICEREQUIREMENT" Then
                        'lbl = New Label
                        'lbl.Text = "<b>" + FormatNumber(0, 3) + "</b>"
                        'lbl.Width = 70
                        'tdInner.Controls.Add(lbl)
                        'trInner.Controls.Add(tdInner)

                    Else
                        If j = 1 Then
                            Link = New LinkButton
                            Dim id As String = String.Empty
                            Link.ID = "hypColDes" + (i - 1).ToString()
                            'Link.Width = 80
                            code = ""
                            ' Link.CssClass = "LinkM"
                            For k = 0 To dsM.Tables(0).Rows.Count - 1
                                If dsM.Tables(0).Rows(k).Item("MTYPEID").ToString() = "5" Then
                                    code = ds.Tables(0).Rows(i - 1).Item("DETAILS").ToString()
                                    id = ds.Tables(0).Rows(i - 1).Item("SKUID").ToString()
                                End If
                            Next
                            Link.Text = "<b>" + id + ":" + code + "</br>" 'ds.Tables(0).Rows(i - 1).Item(dsM.Tables(0).Rows(j - 1).Item("COLMNS").ToString().Replace(" ", "")).ToString() + "</b>"
                            Link.CommandArgument = code
                            Link.Attributes.Add("onclick", "return CaseSearch('" + code.ToString() + "','" + id.ToString() + "');")
                            tdInner.Controls.Add(hid)
                            tdInner.Controls.Add(Link)
                            'tdInner.ColumnSpan = "3"
                            trInner.Controls.Add(tdInner)

                        End If

                    End If

                Next
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
                tblPRICE.Controls.Add(trInner)


            Next


        Catch ex As Exception
            '_lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetCountryDetails()
        Dim ds As New DataSet

        Try

            ds = objGetdata.GetPRICEDATARFPN(Session("RFPPRICEID"), Session("UserId"), Session("RFPID"))


        Catch ex As Exception
            ' _lErrorLble.Text = "Error:GetCaseDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub BindPRICETableOLD()
        Dim ds As New DataSet
        Dim dsM As New DataSet
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
        Dim Link As New LinkButton
        Dim txt As New TextBox
        Dim tdInner As TableCell
        Dim objGetData As New SavvyProGetData()
        Dim dsP As New DataSet
        Dim code As String
        Try


            ds = objGetData.GetPRICEDATARFPN(Session("RFPPRICEID"), Session("UserId"), Session("RFPID"))

            dsM = objGetData.GetMasterDataN(Session("RFPPRICEID"))
            Session("DS") = ds
            Session("DSM") = dsM
            dsP = objGetData.GetRFPPRICE(Session("RFPPRICEID"))

            For i = 1 To dsM.Tables(0).Rows.Count

                Dim Title As String = String.Empty
                'Header
                If dsM.Tables(0).Rows(i - 1).Item("TBLNAME").ToString() = "PRICEREQUIREMENT" Then
                    ' HeaderTdSetting(tdHeader, "130px", "", "1")
                Else
                    tdHeader = New TableCell
                    HeaderTdSetting(tdHeader, "110px", "", "1")
                    lbl = New Label
                    hid = New HiddenField
                    ' Link.CssClass = "LinkM"
                    lbl.Text = dsM.Tables(0).Rows(i - 1).Item("DESCRIPTION")
                    tdHeader.Controls.Add(lbl)
                    trHeader.Controls.Add(tdHeader)
                End If



            Next
            tblPRICE.Controls.Add(trHeader)



            'Inner
            For i = 1 To ds.Tables(0).Rows.Count
                trInner = New TableRow

                For j = 1 To dsM.Tables(0).Rows.Count
                    tdInner = New TableCell
                    InnerTdSetting(tdInner, "", "Left")
                    If dsM.Tables(0).Rows(j - 1).Item("TBLNAME").ToString() = "PRICEREQUIREMENT" Then
                        'lbl = New Label
                        'lbl.Text = "<b>" + FormatNumber(0, 3) + "</b>"
                        'lbl.Width = 70
                        'tdInner.Controls.Add(lbl)
                        'trInner.Controls.Add(tdInner)

                    Else
                        If j = 1 Then
                            Link = New LinkButton
                            Link.ID = "hypColDes" + (i - 1).ToString()
                            Link.Width = 80
                            ' Link.CssClass = "LinkM"
                            code = ds.Tables(0).Rows(i - 1).Item(dsM.Tables(0).Rows(j - 1).Item("COLMNS").ToString().Replace(" ", "")).ToString() + "#" + ds.Tables(0).Rows(i - 1).Item(dsM.Tables(0).Rows(j).Item("COLMNS").ToString().Replace(" ", "")).ToString() + "#" + ds.Tables(0).Rows(i - 1).Item(dsM.Tables(0).Rows(j + 1).Item("COLMNS").ToString().Replace(" ", "")).ToString()
                            'GetColType(Link, hid, i, code.ToString())                                                       
                            Link.Text = "<b>" + ds.Tables(0).Rows(i - 1).Item(dsM.Tables(0).Rows(j - 1).Item("COLMNS").ToString().Replace(" ", "")).ToString() + "</b>"
                            Link.CommandArgument = code
                            Link.Attributes.Add("onclick", "return CaseSearch('" + code.ToString() + "','" + j.ToString() + "');")

                            'AddHandler Link.Click, AddressOf Me.LinkButton1_Click
                            tdInner.Controls.Add(hid)
                            tdInner.Controls.Add(Link)
                            trInner.Controls.Add(tdInner)
                        Else
                            tdInner.Text = "<b>" + ds.Tables(0).Rows(i - 1).Item(dsM.Tables(0).Rows(j - 1).Item("COLMNS").ToString().Replace(" ", "")).ToString() + "</b>"
                            trInner.Controls.Add(tdInner)
                        End If

                    End If

                Next
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
                tblPRICE.Controls.Add(trInner)


            Next

        Catch ex As Exception
            '_lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub BindPRICETable()
        Dim ds As New DataSet
        Dim dsM As New DataSet
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
        Dim Link As New LinkButton
        Dim txt As New TextBox
        Dim tdInner As TableCell
        Dim objGetData As New SavvyProGetData()
        Dim dsP As New DataSet
        Dim code As String
        Try

            dsM = objGetData.GetMasterDataN(Session("RFPPRICEID"))


            ds = objGetData.GetPRICEDATARFPN(Session("RFPPRICEID"), Session("UserID"), Session("RFPID"))


            Session("DS") = ds
            Session("DSM") = dsM
            dsP = objGetData.GetRFPPRICE(Session("RFPPRICEID"))

            For i = 1 To dsM.Tables(0).Rows.Count

                Dim Title As String = String.Empty
                'Header
                If dsM.Tables(0).Rows(i - 1).Item("TBLNAME").ToString() = "PRICEREQUIREMENT" Then
                    ' HeaderTdSetting(tdHeader, "130px", "", "1")
                Else
                    tdHeader = New TableCell

                    HeaderTdSetting(tdHeader, "310px", "", "1")
                    lbl = New Label
                    hid = New HiddenField
                    ' Link.CssClass = "LinkM"
                    lbl.Text = dsM.Tables(0).Rows(i - 1).Item("DESCRIPTION")
                    tdHeader.Controls.Add(lbl)
                    trHeader.Controls.Add(tdHeader)
                End If



            Next
            tblPRICE.Controls.Add(trHeader)



            'Inner
            For i = 1 To ds.Tables(0).Rows.Count
                trInner = New TableRow

                For j = 1 To dsM.Tables(0).Rows.Count
                    tdInner = New TableCell
                    InnerTdSetting(tdInner, "", "Left")
                    If dsM.Tables(0).Rows(j - 1).Item("TBLNAME").ToString() = "PRICEREQUIREMENT" Then
                        'lbl = New Label
                        'lbl.Text = "<b>" + FormatNumber(0, 3) + "</b>"
                        'lbl.Width = 70
                        'tdInner.Controls.Add(lbl)
                        'trInner.Controls.Add(tdInner)

                    Else
                        If j = 1 Then
                            Link = New LinkButton
                            Link.ID = "hypColDes" + (i - 1).ToString()
                            Link.Width = 80
                            code = ""
                            ' Link.CssClass = "LinkM"
                            For k = 0 To dsM.Tables(0).Rows.Count - 1
                                If dsM.Tables(0).Rows(k).Item("MTYPEID").ToString() = "1" Then
                                    code = code + "#" + ds.Tables(0).Rows(i - 1).Item(dsM.Tables(0).Rows(k).Item("COLMNS").ToString().Replace(" ", "")).ToString()
                                End If
                            Next
                            code = code.Remove(0, 1)
                            'code = ds.Tables(0).Rows(i - 1).Item(dsM.Tables(0).Rows(j - 1).Item("COLMNS").ToString().Replace(" ", "")).ToString() + "#" + ds.Tables(0).Rows(i - 1).Item(dsM.Tables(0).Rows(j).Item("COLMNS").ToString().Replace(" ", "")).ToString() + "#" + ds.Tables(0).Rows(i - 1).Item(dsM.Tables(0).Rows(j + 1).Item("COLMNS").ToString().Replace(" ", "")).ToString()
                            'GetColType(Link, hid, i, code.ToString())                                                       
                            Link.Text = "<b>" + ds.Tables(0).Rows(i - 1).Item(dsM.Tables(0).Rows(j - 1).Item("COLMNS").ToString().Replace(" ", "")).ToString() + "</b>"
                            Link.CommandArgument = code
                            Link.Attributes.Add("onclick", "return CaseSearch('" + code.ToString() + "','" + j.ToString() + "');")

                            'AddHandler Link.Click, AddressOf Me.LinkButton1_Click
                            tdInner.Controls.Add(hid)
                            tdInner.Controls.Add(Link)
                            trInner.Controls.Add(tdInner)
                        Else
                            tdInner.Text = "<b>" + ds.Tables(0).Rows(i - 1).Item(dsM.Tables(0).Rows(j - 1).Item("COLMNS").ToString().Replace(" ", "")).ToString() + "</b>"
                            trInner.Controls.Add(tdInner)
                        End If

                    End If

                Next
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
                tblPRICE.Controls.Add(trInner)


            Next

        Catch ex As Exception
            '_lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim dsData As New DataSet
        Dim dvData As New DataView
        Dim lbl As New Label
        Dim tr As New TableRow
        Dim td As New TableCell
        Dim dtData As New DataTable
        Dim objUpIns As New SavvyUpInsData.UpdateInsert
        Try
            lbl = New Label
            Dim bt As LinkButton = CType(sender, LinkButton)

           
        Catch ex As Exception

        End Try


    End Sub

    Protected Sub GetColType(ByRef LinkCOL As LinkButton, ByVal hid As HiddenField, ByVal ColId As Integer, ByVal code As String)
        Dim Ds As New DataSet
        Dim ObjGetdata As New E1GetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty
        Try
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "New_WinCopy", "CaseSearch('" + code.ToString() + "','" + ColId.ToString() + "');", True)

          
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
            ' _lErrorLble.Text = "Error:HeaderTdSetting:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub InnerTdSetting(ByVal Td As TableCell, ByVal Width As String, ByVal Align As String)
        Try

            If Width <> "" Then
                Td.Style.Add("width", Width)
            End If
            Td.Style.Add("text-align", Align)
            If Align = "Left" Then
                Td.Style.Add("padding-left", "15px")
            End If
            If Align = "Right" Then
                Td.Style.Add("padding-right", "15px")
            End If
        Catch ex As Exception
            '_lErrorLble.Text = "Error:InnerTdSetting:" + ex.Message.ToString()
        End Try
    End Sub

End Class
