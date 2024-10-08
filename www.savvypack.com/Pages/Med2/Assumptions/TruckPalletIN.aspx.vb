﻿Imports System.Data
Imports System.Data.OleDb
Imports System
Imports Med2GetData
Imports Med2UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_MedEcon2_Assumptions_TruckPalletIN
    Inherits System.Web.UI.Page
    Public CaseDes As String = String.Empty

#Region "Get Set Variables"
    Dim _lErrorLble As Label
    Dim _iCaseId As Integer
    Dim _iUserId As Integer
    Dim _strUserRole As String
    Dim _btnUpdate As ImageButton
    Dim _btnLogOff As ImageButton
    Dim _btnChart As ImageButton
    Dim _btnNotes As ImageButton
    Dim _btnFeedBack As ImageButton
    Dim _btnInstrutions As ImageButton
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

    Public Property CaseId() As Integer
        Get
            Return _iCaseId
        End Get
        Set(ByVal Value As Integer)
            _iCaseId = Value
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

    Public Property Updatebtn() As ImageButton
        Get
            Return _btnUpdate
        End Get
        Set(ByVal value As ImageButton)
            _btnUpdate = value
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

    Public Property Chartbtn() As ImageButton
        Get
            Return _btnChart
        End Get
        Set(ByVal value As ImageButton)
            _btnChart = value
        End Set
    End Property

    Public Property Notesbtn() As ImageButton
        Get
            Return _btnNotes
        End Get
        Set(ByVal value As ImageButton)
            _btnNotes = value
        End Set
    End Property

    Public Property FeedBackbtn() As ImageButton
        Get
            Return _btnFeedBack
        End Get
        Set(ByVal value As ImageButton)
            _btnFeedBack = value
        End Set
    End Property

    Public Property Instrutionsbtn() As ImageButton
        Get
            Return _btnInstrutions
        End Get
        Set(ByVal value As ImageButton)
            _btnInstrutions = value
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



    Public DataCnt As Integer
    Public CaseDesp As New ArrayList
#End Region

#Region "MastePage Content Variables"

    Protected Sub GetMasterPageControls()
        GetContentPlaceHolder()
        GetErrorLable()
        GetUpdatebtn()
        GetLogOffbtn()
        GetInstructionsbtn()
        GetChartbtn()
        GetFeedbackbtn()
        GetNotesbtn()
        GetMainHeadingdiv()

    End Sub

    Protected Sub GetErrorLable()
        ErrorLable = Page.Master.FindControl("lblError")
    End Sub

    Protected Sub GetUpdatebtn()
        Updatebtn = Page.Master.FindControl("imgUpdate")
        Updatebtn.Attributes.Add("onclick", "return checkNumericAll();")
        Updatebtn.Visible = True
        AddHandler Updatebtn.Click, AddressOf Update_Click
    End Sub

    Protected Sub GetLogOffbtn()
        LogOffbtn = Page.Master.FindControl("imgLogoff")
        LogOffbtn.Visible = False
        'AddHandler Updatebtn.Click, AddressOf Update_Click
    End Sub

    Protected Sub GetInstructionsbtn()
        Instrutionsbtn = Page.Master.FindControl("imgInstructions")
        Instrutionsbtn.Visible = True
        'AddHandler Updatebtn.Click, AddressOf Update_Click
    End Sub

    Protected Sub GetChartbtn()
        Chartbtn = Page.Master.FindControl("imgChart")
        Chartbtn.Visible = False
        'AddHandler Updatebtn.Click, AddressOf Update_Click
    End Sub

    Protected Sub GetFeedbackbtn()
        FeedBackbtn = Page.Master.FindControl("imgFeedback")
        FeedBackbtn.Visible = True
        'AddHandler Updatebtn.Click, AddressOf Update_Click
    End Sub

    Protected Sub GetNotesbtn()
        Notesbtn = Page.Master.FindControl("imgNotes")
        Notesbtn.Visible = True
        Notesbtn.OnClientClick = "return Notes('PALLATNDTRUC');"
        'AddHandler Updatebtn.Click, AddressOf Update_Click
    End Sub

    Protected Sub GetMainHeadingdiv()
        MainHeading = Page.Master.FindControl("divMainHeading")
        MainHeading.Attributes.Add("onmouseover", "Tip('Pallet and Truck Configuration')")
        MainHeading.Attributes.Add("onmouseout", "UnTip()")
        MainHeading.InnerHtml = "Med2 - Pallet and Truck Configuration"
    End Sub

    Protected Sub GetContentPlaceHolder()
        ctlContentPlaceHolder = Page.Master.FindControl("Med2ContentPlaceHolder")
    End Sub

#End Region

#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_PAGES_MEDECON2_ASSUMPTIONS_TRUCKPALLETIN")
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        objRefresh.Render(Page)
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            GetMasterPageControls()
            GetSessionDetails()
            If Not IsPostBack Then
                GetPageDetails()
            End If
        Catch ex As Exception
            ErrorLable.Text = "Error:Page_Load:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetSessionDetails()
        Try
            UserId = Session("UserId")
            CaseId = Session("Med2CaseId")
            UserRole = Session("Med2UserRole")
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetSessionDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetPageDetails()
        Dim ds As New DataSet
        Dim objGetData As New Med2GetData.Selectdata
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

        Try
            ds = objGetData.GetPalletAndTruck(CaseId)

            'Pallet Details
            For i = 1 To 5
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "300px", "Pallet Dimensions", "3")
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE9").ToString() + ")"
                        Header2TdSetting(tdHeader1, "0", Title, "3")
                        Header2TdSetting(tdHeader2, "", "Width", "1")
                        trHeader.Controls.Add(tdHeader)
                    Case 2
                        Header2TdSetting(tdHeader1, "0", "", "1")
                        Header2TdSetting(tdHeader2, "", "Length", "1")
                    Case 3
                        Header2TdSetting(tdHeader1, "0", "", "1")
                        Header2TdSetting(tdHeader2, "", "Height", "1")
                    Case 4
                        HeaderTdSetting(tdHeader, "", "Cartons Per Pallet", "1")
                        Header2TdSetting(tdHeader1, "0", "", "1")
                        Header2TdSetting(tdHeader2, "200px", "Number", "1")
                        trHeader.Controls.Add(tdHeader)
                    Case 5
                        HeaderTdSetting(tdHeader, "", "Product Per Pallet", "1")
                        Header2TdSetting(tdHeader1, "0", "", "1")
                        Header2TdSetting(tdHeader2, "200px", "Number", "1")
                        trHeader.Controls.Add(tdHeader)
                End Select

                trHeader1.Controls.Add(tdHeader1)
                trHeader2.Controls.Add(tdHeader2)
            Next
            tblComparision.Controls.Add(trHeader)
            tblComparision.Controls.Add(trHeader1)
            tblComparision.Controls.Add(trHeader2)

            'Inner
            trInner = New TableRow
            For i = 1 To 5
                tdInner = New TableCell()
                InnerTdSetting(tdInner, "", "Center")
                txt = New TextBox
                txt.CssClass = "SmallTextBox"
                txt.Width = 80
                txt.ID = "M" + i.ToString()
                txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("M" + i.ToString() + "").ToString(), 0)
                If i = 5 Then
                    txt.MaxLength = 10
                Else
                    txt.MaxLength = 6
                End If
                tdInner.Controls.Add(txt)
                trInner.Controls.Add(tdInner)

            Next
            trInner.CssClass = "AlterNateColor2"
            tblComparision.Controls.Add(trInner)



            'Truck Details
            trHeader = New TableRow
            trHeader1 = New TableRow
            trHeader2 = New TableRow
            For i = 1 To 5
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "300px", "Truck Dimensions", "3")
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE9").ToString() + ")"
                        Header2TdSetting(tdHeader1, "0", Title, "3")
                        Header2TdSetting(tdHeader2, "", "Width", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 2
                        Header2TdSetting(tdHeader2, "", "Length", "1")
                    Case 3
                        Header2TdSetting(tdHeader2, "", "Height", "1")
                    Case 4
                        HeaderTdSetting(tdHeader, "", "Weight Limit ", "1")
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                        Header2TdSetting(tdHeader1, "0", Title, "1")
                        Header2TdSetting(tdHeader2, "200px", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 5
                        HeaderTdSetting(tdHeader, "", "Pallets Per Truck", "1")
                        Header2TdSetting(tdHeader1, "0", "", "1")
                        Header2TdSetting(tdHeader2, "200px", "Number", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)

                End Select


                trHeader2.Controls.Add(tdHeader2)
            Next
            tblComparision1.Controls.Add(trHeader)
            tblComparision1.Controls.Add(trHeader1)
            tblComparision1.Controls.Add(trHeader2)

            'Inner
            trInner = New TableRow
            For i = 1 To 5
                tdInner = New TableCell()
                InnerTdSetting(tdInner, "", "Center")
                txt = New TextBox
                txt.CssClass = "SmallTextBox"
                txt.Width = 80
                txt.ID = "T" + i.ToString()
                txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("T" + i.ToString() + "").ToString(), 0)
                txt.MaxLength = 6
                tdInner.Controls.Add(txt)
                trInner.Controls.Add(tdInner)

            Next
            trInner.CssClass = "AlterNateColor2"
            tblComparision1.Controls.Add(trInner)

            trHeader = New TableRow
            trHeader1 = New TableRow
            tdHeader = New TableCell
            tdHeader1 = New TableCell
            HeaderTdSetting(tdHeader, "300px", "Calculated Weight", "1")
            Header2TdSetting(tdHeader1, "0", "(" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + ")", "1")
            trHeader.Controls.Add(tdHeader)
            trHeader1.Controls.Add(tdHeader1)

            tblComparision2.Controls.Add(trHeader)
            tblComparision2.Controls.Add(trHeader1)

            'Inner
            trInner = New TableRow
            tdInner = New TableCell()
            InnerTdSetting(tdInner, "", "Right")
            lbl = New Label
            lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("CALWEIGHT").ToString(), 0)
            tdInner.Controls.Add(lbl)
            trInner.Controls.Add(tdInner)
            trInner.CssClass = "AlterNateColor2"
            tblComparision2.Controls.Add(trInner)

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
        Catch ex As Exception
            _lErrorLble.Text = "Error:InnerTdSetting:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub TextBoxSetting(ByVal txt As TextBox, ByVal Css As String)
        Try
            txt.CssClass = Css

        Catch ex As Exception
            _lErrorLble.Text = "Error:TextBoxSetting:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub LableSetting(ByVal lbl As Label, ByVal Css As String)
        Try
            lbl.CssClass = Css

        Catch ex As Exception
            _lErrorLble.Text = "Error:LableSetting:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub LeftTdSetting(ByVal Td As TableCell, ByVal Text As String, ByVal tr As TableRow, ByVal Css As String)
        Try
            Td.Text = Text
            InnerTdSetting(Td, "", "Left")
            tr.Controls.Add(Td)
            tr.CssClass = Css
        Catch ex As Exception
            _lErrorLble.Text = "Error:LeftTdSetting:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub Update_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim Pallet(5) As String
        Dim Truck(5) As String
        Dim M1 As String
        Dim i As New Integer
        Dim obj As New CryptoHelper
        Dim ObjUpIns As New Med2UpInsData.UpdateInsert()
        Try
            If Not objRefresh.IsRefresh Then
                For i = 1 To 5
                    Pallet(i) = Request.Form("ctl00$Med2ContentPlaceHolder$M" + i.ToString() + "").Replace(",", "")
                    Truck(i) = Request.Form("ctl00$Med2ContentPlaceHolder$T" + i.ToString() + "").Replace(",", "")
                    'Check For IsNumric
                    If Not IsNumeric(Pallet(i)) Then
                        Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                    End If
                    If Not IsNumeric(Truck(i)) Then
                        Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                    End If
                Next
                ObjUpIns.PalletAndTruckUpdate(CaseId, Pallet, Truck)
                Calculate()
                'Update Server Date
                ObjUpIns.ServerDateUpdate(CaseId, Session("Med2UserName"))
            End If
            GetPageDetails()
        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
    Protected Sub Calculate()
        Try
            Dim Med1Conn As String = System.Configuration.ConfigurationManager.AppSettings("MedEcon1ConnectionString")
            Dim Med2Conn As String = System.Configuration.ConfigurationManager.AppSettings("MedEcon2ConnectionString")
            Dim obj As New Med2Calculation.Med2Calculation(Med2Conn, Med1Conn)
            obj.Med2Calculate(CaseId)
        Catch ex As Exception
            ErrorLable.Text = "Error:Calculate:" + ex.Message.ToString() + ""
        End Try
    End Sub

End Class
