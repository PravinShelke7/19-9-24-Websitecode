Imports System.Data
Imports System.Data.OleDb
Imports System
Imports M1GetData
Imports M1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Market1_Reports
    Inherits System.Web.UI.Page
#Region "Get Set Variables"
    Dim _lErrorLble As Label
    Dim _iUserId As Integer
    Dim _strUserRole As String
    Dim _btnLogOff As ImageButton
    Dim _divMainHeading As HtmlGenericControl
    Dim _ctlContentPlaceHolder As ContentPlaceHolder


    Public Property ErrorLable() As Label
        Get
            Return _lErrorLble
        End Get
        Set(ByVal Value As Label)
            _lErrorLble = Value
        End Set
    End Property

    Public Property UserId() As Integer
        Get
            Return _iUserId
        End Get
        Set(ByVal Value As Integer)
            _iUserId = Value
        End Set
    End Property

    Public Property UserRole() As String
        Get
            Return _strUserRole
        End Get
        Set(ByVal Value As String)
            _strUserRole = Value
        End Set
    End Property

    Public Property LogOffbtn() As ImageButton
        Get
            Return _btnLogOff
        End Get
        Set(ByVal value As ImageButton)
            _btnLogOff = value
        End Set
    End Property

    Public Property MainHeading() As HtmlGenericControl
        Get
            Return _divMainHeading
        End Get
        Set(ByVal value As HtmlGenericControl)
            _divMainHeading = value
        End Set
    End Property

    Public Property ctlContentPlaceHolder() As ContentPlaceHolder
        Get
            Return _ctlContentPlaceHolder
        End Get
        Set(ByVal value As ContentPlaceHolder)
            _ctlContentPlaceHolder = value
        End Set
    End Property



#End Region

#Region "MastePage Content Variables"

    Protected Sub GetMasterPageControls()
        GetErrorLable()
        GetLogOffbtn()
        GetMainHeadingdiv()
        'GetContentPlaceHolder()
    End Sub

    Protected Sub GetErrorLable()
        ErrorLable = Page.Master.FindControl("lblError")
    End Sub

    Protected Sub GetLogOffbtn()
        LogOffbtn = Page.Master.FindControl("imgLogoff")
        LogOffbtn.Visible = False
    End Sub

    Protected Sub GetMainHeadingdiv()
        MainHeading = Page.Master.FindControl("divMainHeading")
        MainHeading.Attributes.Add("onmouseover", "Tip('Population')")
        MainHeading.Attributes.Add("onmouseout", "UnTip()")
        MainHeading.InnerHtml = "Population"
    End Sub

    Protected Sub GetContentPlaceHolder()
        ctlContentPlaceHolder = Page.Master.FindControl("Sustain1ContentPlaceHolder")
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        GetMasterPageControls()

        Dim objGetData As New E1GetData.Selectdata
        Dim obj As New CryptoHelper()
        Dim str As String = obj.Decrypt("NQXyX+oQqRw=")

        GetContinentwise(0)
        If (ViewState("Cid") <> "") Then
            GetContriwise(ViewState("Cid"), 0)
        End If







    End Sub

    Protected Sub GetContinentwise(ByVal Cnt As String)
        Dim dsyr As New DataSet()
        Dim dsContinent As New DataSet()
        Dim objGetData As New Selectdata()
        Dim trHeader As New TableRow
        Dim trHeader1 As New TableRow
        Dim td As New TableCell
        Dim tr As New TableRow
        Dim tdHeader As New TableCell
        Dim i As New Integer
        Dim rwcnt As New Integer
        Dim lnk As New LinkButton

        Try
            dsyr = objGetData.GetMinMaxYear()
            dsContinent = objGetData.GetContinentsWise()

            If dsyr.Tables.Count > 0 Then
                If dsyr.Tables(0).Rows.Count > 0 Then
                    lblFrom.Text = dsyr.Tables(0).Rows(0).Item("MINYR").ToString()
                    lblTo.Text = dsyr.Tables(0).Rows(0).Item("MAXYR").ToString()
                End If
            End If
            tblComparision.Rows.Clear()
            If dsContinent.Tables.Count > 0 Then
                If dsContinent.Tables(0).Rows.Count > 0 Then
                    For i = 1 To 4
                        tdHeader = New TableCell
                        Dim Title As String = String.Empty
                        Select Case i
                            Case 1
                                HeaderTdSetting(tdHeader, "80px", "", "1")
                                trHeader.Controls.Add(tdHeader)
                            Case 2
                                HeaderTdSetting(tdHeader, "", "Pouplutions<br/>(in million)", "1")
                                trHeader.Controls.Add(tdHeader)
                            Case 3
                                HeaderTdSetting(tdHeader, "", "GDP<br/>(in billion)", "1")
                                trHeader.Controls.Add(tdHeader)
                            Case 4
                                HeaderTdSetting(tdHeader, "", "GDP/Capita<br/>(in units)", "1")
                                trHeader.Controls.Add(tdHeader)
                        End Select
                    Next
                    trHeader.Height = 30
                    tblComparision.Controls.Add(trHeader)

                    For rwcnt = 0 To dsContinent.Tables(0).Rows.Count - 1
                        tr = New TableRow
                        For i = 1 To 4
                            td = New TableCell
                            lnk = New LinkButton
                            AddHandler lnk.Click, AddressOf Link_Click
                            Select Case i
                                Case 1
                                    InnerTdSetting(td, "", "Left")
                                    lnk.Text = dsContinent.Tables(0).Rows(rwcnt).Item("CONTINENTDES").ToString()
                                    lnk.ID = "Conti_" + dsContinent.Tables(0).Rows(rwcnt).Item("CONTINENTID").ToString()
                                    If lnk.ID.Replace("Conti_", "") = Cnt Then
                                        lnk.Style.Add("font-style", "italic")
                                        lnk.Style.Add("color", "gray")
                                    End If
                                    td.Controls.Add(lnk)
                                    td.Width = 80
                                Case 2
                                    InnerTdSetting(td, "", "Right")
                                    td.Text = FormatNumber(dsContinent.Tables(0).Rows(rwcnt).Item("FACTPOP").ToString() / (1000 * 1000), 0)
                                    td.Width = 160
                                Case 3
                                    InnerTdSetting(td, "", "Right")
                                    td.Text = FormatNumber(dsContinent.Tables(0).Rows(rwcnt).Item("FACTGDP").ToString() / (1000 * 1000 * 1000), 0)
                                    td.Width = 160
                                Case 4
                                    InnerTdSetting(td, "", "Right")
                                    td.Text = FormatNumber(dsContinent.Tables(0).Rows(rwcnt).Item("GDPPC").ToString(), 0)
                                    td.Width = 160
                            End Select
                            tr.Controls.Add(td)
                        Next

                        tblComparision.Controls.Add(tr)
                        If (i Mod 2 = 0) Then
                            tr.CssClass = "AlterNateColor1"
                        Else
                            tr.CssClass = "AlterNateColor2"
                        End If
                    Next






                End If
            End If





        Catch ex As Exception
            ErrorLable.Text = "Error:GetContinentwise:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetContriwise(ByVal Cid As String, ByVal Cntry As String)
        Dim dsyr As New DataSet()
        Dim dsConttry As New DataSet()
        Dim objGetData As New Selectdata()
        Dim trHeader As New TableRow
        Dim trHeader1 As New TableRow
        Dim td As New TableCell
        Dim tr As New TableRow
        Dim tdHeader As New TableCell
        Dim i As New Integer
        Dim rwcnt As New Integer
        Dim lnk As New LinkButton
        tblComparision2.Rows.Clear()
        Try
            dsyr = objGetData.GetMinMaxYear()
            dsConttry = objGetData.GetCountryWise(Cid)

            If dsyr.Tables.Count > 0 Then
                If dsyr.Tables(0).Rows.Count > 0 Then
                    lblFrom1.Text = dsyr.Tables(0).Rows(0).Item("MINYR").ToString()
                    lblTo1.Text = dsyr.Tables(0).Rows(0).Item("MAXYR").ToString()
                End If
            End If

            If dsConttry.Tables.Count > 0 Then
                If dsConttry.Tables(0).Rows.Count > 0 Then
                    For i = 1 To 4
                        tdHeader = New TableCell
                        Dim Title As String = String.Empty
                        Select Case i
                            Case 1
                                HeaderTdSetting(tdHeader, "80px", "", "1")
                                trHeader.Controls.Add(tdHeader)
                            Case 2
                                HeaderTdSetting(tdHeader, "", "Pouplutions<br/>(in million)", "1")
                                trHeader.Controls.Add(tdHeader)
                            Case 3
                                HeaderTdSetting(tdHeader, "", "GDP<br/>(in billion)", "1")
                                trHeader.Controls.Add(tdHeader)
                            Case 4
                                HeaderTdSetting(tdHeader, "", "GDP/Capita<br/>(in units)", "1")
                                trHeader.Controls.Add(tdHeader)
                        End Select
                    Next
                    trHeader.Height = 30
                    tblComparision2.Controls.Add(trHeader)

                    For rwcnt = 0 To dsConttry.Tables(0).Rows.Count - 1
                        tr = New TableRow
                        For i = 1 To 4
                            td = New TableCell
                            lnk = New LinkButton
                            AddHandler lnk.Click, AddressOf LinkCntry_Click
                            Select Case i
                                Case 1
                                    InnerTdSetting(td, "", "Left")
                                    lnk.Text = dsConttry.Tables(0).Rows(rwcnt).Item("COUNTRYDES").ToString()
                                    lnk.ID = dsConttry.Tables(0).Rows(rwcnt).Item("COUNTRYID").ToString()
                                    If lnk.ID = Cntry Then
                                        lnk.Style.Add("font-style", "italic")
                                        lnk.Style.Add("color", "gray")
                                    End If
                                    td.Controls.Add(lnk)
                                    td.Width = 80
                                Case 2
                                    InnerTdSetting(td, "", "Right")
                                    td.Text = FormatNumber(dsConttry.Tables(0).Rows(rwcnt).Item("FACTPOP").ToString() / (1000 * 1000), 0)
                                    td.Width = 160
                                Case 3
                                    InnerTdSetting(td, "", "Right")
                                    td.Text = FormatNumber(dsConttry.Tables(0).Rows(rwcnt).Item("FACTGDP").ToString() / (1000 * 1000 * 1000), 0)
                                    td.Width = 160
                                Case 4
                                    InnerTdSetting(td, "", "Right")
                                    td.Text = FormatNumber(dsConttry.Tables(0).Rows(rwcnt).Item("GDPPC").ToString(), 0)
                                    td.Width = 160
                            End Select
                            tr.Controls.Add(td)
                        Next

                        tblComparision2.Controls.Add(tr)
                        If (i Mod 2 = 0) Then
                            tr.CssClass = "AlterNateColor1"
                        Else
                            tr.CssClass = "AlterNateColor2"
                        End If
                    Next






                End If
                divCountry.Visible = True
            Else
                divCountry.Visible = False
            End If





        Catch ex As Exception
            ErrorLable.Text = "Error:GetContinentwise:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub YearWise(ByVal Cid As String)
        Dim dsyr As New DataSet()
        Dim dsConttry As New DataSet()
        Dim objGetData As New Selectdata()
        Dim trHeader As New TableRow
        Dim trHeader1 As New TableRow
        Dim td As New TableCell
        Dim tr As New TableRow
        Dim tdHeader As New TableCell
        Dim i As New Integer
        Dim rwcnt As New Integer
        Dim lnk As New LinkButton

        Try
            dsyr = objGetData.GetMinMaxYear()
            dsConttry = objGetData.GetCountrYearWise(Cid)

            If dsyr.Tables.Count > 0 Then
                If dsyr.Tables(0).Rows.Count > 0 Then
                    lblFrom2.Text = dsyr.Tables(0).Rows(0).Item("MINYR").ToString()
                    lblTo2.Text = dsyr.Tables(0).Rows(0).Item("MAXYR").ToString()
                End If
            End If

            If dsyr.Tables.Count > 0 Then
                If dsyr.Tables(0).Rows.Count > 0 Then
                    lblFrom1.Text = dsyr.Tables(0).Rows(0).Item("MINYR").ToString()
                    lblTo1.Text = dsyr.Tables(0).Rows(0).Item("MAXYR").ToString()
                End If
            End If

            If dsConttry.Tables.Count > 0 Then
                If dsConttry.Tables(0).Rows.Count > 0 Then
                    tdHeader = New TableCell
                    HeaderTdSetting(tdHeader, "80px", "", "1")
                    trHeader.Controls.Add(tdHeader)


                    For i = 0 To dsConttry.Tables(0).Rows.Count - 1
                        tdHeader = New TableCell
                        HeaderTdSetting(tdHeader, "80px", dsConttry.Tables(0).Rows(i).Item("YEAR").ToString(), "1")
                        trHeader.Controls.Add(tdHeader)


                    Next
                    trHeader.Height = 30
                    tblComparision3.Controls.Add(trHeader)


                    'Populations

                    tr = New TableRow
                    td = New TableCell
                    InnerTdSetting(td, "160", "Left")
                    td.Text = "Population<br />(in million)"
                    tr.Controls.Add(td)
                    For i = 0 To dsConttry.Tables(0).Rows.Count - 1
                        td = New TableCell
                        Dim lbl As New Label()
                        InnerTdSetting(td, "100", "Right")
                        td.Text = FormatNumber(dsConttry.Tables(0).Rows(i).Item("POPULATION").ToString() / (1000 * 1000), 0)

                        If dsConttry.Tables(0).Rows(i).Item("POPULATIONA_E").ToString() = "E" Then
                            td.Style.Add("color", "blue")
                        End If
                        tr.Controls.Add(td)
                        'System.Drawing.Color.DarkRed
                    Next
                    tblComparision3.Controls.Add(tr)
                    tr.CssClass = "AlterNateColor2"

                    'GDP

                    tr = New TableRow
                    td = New TableCell
                    InnerTdSetting(td, "160", "Left")
                    td.Text = "GDP<br />(in billion)"
                    tr.Controls.Add(td)
                    For i = 0 To dsConttry.Tables(0).Rows.Count - 1
                        td = New TableCell
                        Dim lbl As New Label()
                        InnerTdSetting(td, "100", "Right")
                        td.Text = FormatNumber(dsConttry.Tables(0).Rows(i).Item("GDP").ToString() / (1000 * 1000 * 1000), 0)

                        If dsConttry.Tables(0).Rows(i).Item("GDPA_E").ToString() = "E" Then
                            td.Style.Add("color", "blue")
                        End If
                        tr.Controls.Add(td)
                        'System.Drawing.Color.DarkRed
                    Next
                    tblComparision3.Controls.Add(tr)
                    tr.CssClass = "AlterNateColor2"

                    'GDP Per Capita

                    tr = New TableRow
                    td = New TableCell
                    InnerTdSetting(td, "160", "Left")
                    td.Text = "GDP/Capita<br />(in units)"
                    tr.Controls.Add(td)
                    For i = 0 To dsConttry.Tables(0).Rows.Count - 1
                        td = New TableCell
                        Dim lbl As New Label()
                        InnerTdSetting(td, "100", "Right")
                        td.Text = FormatNumber(dsConttry.Tables(0).Rows(i).Item("GDPPC").ToString(), 0)
                        tr.Controls.Add(td)
                        'System.Drawing.Color.DarkRed
                    Next
                    tblComparision3.Controls.Add(tr)
                    tr.CssClass = "AlterNateColor2"

                End If
                divCountryYear.Visible = True
            Else
                divCountryYear.Visible = False
            End If

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
        Catch ex As Exception
            _lErrorLble.Text = "Error:InnerTdSetting:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub Link_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lnk As New LinkButton()
        Try
            lnk = sender
            ViewState("Cid") = lnk.ID.Replace("Conti_", "")
            GetContinentwise(lnk.ID.Replace("Conti_", ""))
            GetContriwise(lnk.ID.Replace("Conti_", ""), 0)
            divCountryYear.Visible = False
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub LinkCntry_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lnk As New LinkButton()
        Try
            lnk = sender
            GetContinentwise(ViewState("Cid"))
            GetContriwise(ViewState("Cid"), lnk.ID)
            YearWise(lnk.ID)

        Catch ex As Exception

        End Try
    End Sub


End Class
