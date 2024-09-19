Imports System.Data
Imports System.Data.OleDb
Imports System
Imports SpecGetData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports Spec
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Spec_Results_S1AllSpecErgy
    Inherits System.Web.UI.Page
#Region "Get Set Variables"
    Dim _lErrorLble As Label
    Dim _strUserName As String
    Dim _strPassword As String
    Dim _iSpecId As Integer
    Dim _btnUpdate As ImageButton


    Public Property ErrorLable() As Label
        Get
            Return _lErrorLble
        End Get
        Set(ByVal Value As Label)
            _lErrorLble = Value
        End Set
    End Property

    Public Property UserName() As String
        Get
            Return _strUserName
        End Get
        Set(ByVal Value As String)
            _strUserName = Value
        End Set
    End Property

    Public Property Password() As String
        Get
            Return _strPassword
        End Get
        Set(ByVal Value As String)
            _strPassword = Value
        End Set
    End Property

    Public Property SpecId() As Integer
        Get
            Return _iSpecId
        End Get
        Set(ByVal Value As Integer)
            _iSpecId = Value
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

    Public DataCnt As Integer
    Public CaseDesp As New ArrayList
#End Region

#Region "MastePage Content Variables"
    Protected Sub GetErrorLable()
        ErrorLable = Page.Master.FindControl("lblError")
    End Sub

    Protected Sub GetUpdatebtn()
        Updatebtn = Page.Master.FindControl("imgUpdate")
        AddHandler Updatebtn.Click, AddressOf Update_Click
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Try
                GetErrorLable()
                GetUpdatebtn()
                GetSessionDetails()
                GetPageDetails()

            Catch ex As Exception

            End Try

        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetSessionDetails()
        Try
            UserName = Session("UserName")
            Password = Session("Password")
            SpecId = Session("SpecId")


        Catch ex As Exception
            _lErrorLble.Text = "Error:GetSessionDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetPageDetails()
        Dim ds As New DataSet
        Dim objGetData As New SpecGetData.Selectdata
        Dim CaseIds As String = String.Empty
        'Dim DataCnt As New Integer
        Dim i As New Integer
        Dim j As New Integer
        Dim DWidth As String = String.Empty
        Dim IsSalesVolOrMsi As Boolean
        Dim IsAll As Boolean
        Dim Cnt As New Integer

        If (rdSaleUnit.checked) Then
            IsSalesVolOrMsi = True
        Else
            IsSalesVolOrMsi = False
        End If

        If (rdSaleAll.Checked) Then
            IsAll = True
            Cnt = 4
        Else
            IsAll = False
            Cnt = 5
        End If



        Try

            ds = objGetData.GetS1AllSpecErgyTotal(Session("Company"), IsSalesVolOrMsi, IsAll)
            lblVolume.Text = FormatNumber(ds.Tables(0).Rows(0).Item("VOLUME").ToString(), 0)
            If Not IsAll Then
                If CDbl(ds.Tables(0).Rows(0).Item("FINVOLMUNITS").ToString()) > 0 Then
                    lblUnit.Text = FormatNumber(ds.Tables(0).Rows(0).Item("FINVOLMUNITS").ToString(), 0)
                Else
                    lblUnit.Text = FormatNumber(ds.Tables(0).Rows(0).Item("FINVOLMSI").ToString(), 0)
                End If
            Else
                lblUnit.Text = ""
            End If
            Dim TrHeader As New TableRow
            Dim Tr As New TableRow
            Dim Td As New TableCell
            Dim TdHeader As TableCell

            For i = 1 To Cnt
                TdHeader = New TableCell
                Select Case i
                    Case 1
                        HeaderTdSetting(TdHeader, "200px", "", 1)
                    Case 2
                        HeaderTdSetting(TdHeader, "200px", "(MJ)", 1)
                    Case 3
                        HeaderTdSetting(TdHeader, "120px", "(% Of Total)", 1)
                    Case 4
                        HeaderTdSetting(TdHeader, "120px", "(MJ/lb)", 1)
                    Case 5
                        HeaderTdSetting(TdHeader, "120px", "(MJ/unit)", 1)
                    Case Else
                End Select
                TrHeader.Controls.Add(TdHeader)
            Next
            tblComparision.Controls.Add(TrHeader)


            For i = 1 To 12
                Tr = New TableRow
                For j = 1 To Cnt
                    Td = New TableCell
                    Dim val As New Decimal
                    Select Case j
                        Case 1
                            Td.Text = "<b>" + ds.Tables(0).Rows(0).Item("ET" + i.ToString() + "").ToString() + "</b>"
                            InnerTdSetting(Td, "", "Left")
                        Case 2
                            Td.Text = FormatNumber(ds.Tables(0).Rows(0).Item("E" + i.ToString() + "").ToString(), 0)
                            InnerTdSetting(Td, "", "Right")
                        Case 3
                            Td.Text = FormatNumber((ds.Tables(0).Rows(0).Item("E" + i.ToString() + "").ToString() / CDbl(ds.Tables(0).Rows(0).Item("E12").ToString())) * 100, 3)
                            InnerTdSetting(Td, "", "Right")
                        Case 4
                            Td.Text = FormatNumber((ds.Tables(0).Rows(0).Item("E" + i.ToString() + "").ToString() / CDbl(ds.Tables(0).Rows(0).Item("VOLUME").ToString())), 3)
                            InnerTdSetting(Td, "", "Right")
                        Case 5
                            If CDbl(ds.Tables(0).Rows(0).Item("FINVOLMUNITS").ToString()) > 0 Then
                                Td.Text = FormatNumber((ds.Tables(0).Rows(0).Item("E" + i.ToString() + "").ToString() / CDbl(ds.Tables(0).Rows(0).Item("FINVOLMUNITS").ToString())), 3)
                            Else
                                Td.Text = FormatNumber((ds.Tables(0).Rows(0).Item("E" + i.ToString() + "").ToString() / CDbl(ds.Tables(0).Rows(0).Item("FINVOLMSI").ToString())), 3)
                            End If

                            InnerTdSetting(Td, "", "Right")
                        Case Else
                    End Select
                    Tr.Controls.Add(Td)
                    If (i Mod 2 = 0) Then
                        Tr.CssClass = "AlterNateColor1"
                    Else
                        Tr.CssClass = "AlterNateColor2"
                    End If
                    tblComparision.Controls.Add(Tr)
                Next

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
            Td.Height = 30
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




        Catch ex As Exception

        End Try
    End Sub

    Protected Sub TextBoxSetting(ByVal txt As TextBox, ByVal Css As String)
        Try
            txt.CssClass = Css

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub LableSetting(ByVal lbl As Label, ByVal Css As String)
        Try
            lbl.CssClass = Css

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub LeftTdSetting(ByVal Td As TableCell, ByVal Text As String, ByVal tr As TableRow, ByVal Css As String)
        Try
            Td.Text = Text
            InnerTdSetting(Td, "", "Left")
            tr.Controls.Add(Td)
            tr.CssClass = Css




        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub Update_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Try

        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub rdSaleUnit_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdSaleUnit.CheckedChanged
        Try

        Catch ex As Exception
            ErrorLable.Text = "Error:rdSaleUnit_CheckedChanged:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub rdSaleMsi_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdSaleMsi.CheckedChanged
        Try

        Catch ex As Exception
            ErrorLable.Text = "Error:rdSaleUnit_CheckedChanged:" + ex.Message.ToString() + ""
        End Try
    End Sub
End Class
