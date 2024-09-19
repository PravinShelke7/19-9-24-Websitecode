Imports System.Data
Imports System.Data.OleDb
Imports System
Imports SpecGetData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Imports SpecCalculation
Partial Class Pages_Spec_Assumptions_Extursion
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
        Updatebtn.Visible = True
        AddHandler Updatebtn.Click, AddressOf Update_Click
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Try
                GetErrorLable()
                GetUpdatebtn()
                GetSessionDetails()
                If Not IsPostBack Then

                    GetPageDetails()
                End If
                
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

            lblAID.Text = SpecId
            lblAdes.Text = Session("Description")

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

        Try

            ds = objGetData.ExtrusionInput(SpecId)
            DataCnt = ds.Tables(0).Rows.Count - 1

            Dim trHeader As New TableRow
            Dim tdHeader As TableCell
            Dim tdInner As TableCell
            Dim ddl As DropDownList
            Dim lbl As New Label
            Dim txt As TextBox
            Dim Cunits As New Integer
            Dim Units As New Integer
            Dim Title As String = String.Empty
            DWidth = "350px"
            tblComparision.Rows.Clear()


            'tdHeader = New TableCell
            'HeaderTdSetting(tdHeader, "200px", "<img alt='' src='../../Images/spacer.gif' Style='width:200px;height:0px;'  />", 1)
            'trHeader.Controls.Add(tdHeader)
            'trHeader.Height = 20
            'trHeader.CssClass = "PageSSHeading"




            For i = 1 To 7
                tdHeader = New TableCell
                If i = 1 Then
                    HeaderTdSetting(tdHeader, "80px", "<b>Layer</b>", 1)
                ElseIf i = 2 Then
                    HeaderTdSetting(tdHeader, DWidth, "<b>Specification Material</b>", 1)
                ElseIf i = 3 Then
                    HeaderTdSetting(tdHeader, DWidth, "<b>Mapped Sustain Material</b>", 1)
                ElseIf i = 4 Then
                    HeaderTdSetting(tdHeader, "120px", "<b>Thickness</b>", 1)
                ElseIf i = 5 Then
                    HeaderTdSetting(tdHeader, "120px", "<b>Recycle(%)</b>", 1)
                ElseIf i = 6 Then
                    HeaderTdSetting(tdHeader, "120px", "<b>Extra Process</b>", 1)
                ElseIf i = 7 Then
                    HeaderTdSetting(tdHeader, "150px", "<b>Mfg. Dept.</b>", 1)
                End If
                trHeader.Controls.Add(tdHeader)
            Next
            tblComparision.Controls.Add(trHeader)
            For j = 1 To 10

                Dim trBreak As New TableRow
                Dim trData As New TableRow

                For i = 1 To 7
                    tdInner = New TableCell
                    If i = 1 Then
                        tdInner.Text = "<b>Layer" + j.ToString() + ""
                        InnerTdSetting(tdInner, "", "Left")
                    ElseIf i = 2 Then
                        'ddl = New DropDownList
                        'ddl = GetSpecMateralCombo(ds.Tables(0).Rows(0).Item("M" + j.ToString() + ""))
                        'ddl.ID = "MSP_" + j.ToString()
                        'tdInner.Controls.Add(ddl)

                        txt = New TextBox
                        txt.Text = ds.Tables(0).Rows(0).Item("M" + j.ToString() + "").ToString().Trim()
                        txt.ID = "MSP_" + j.ToString()
                        TextBoxSetting(txt, "LongTextBox")
                        txt.Style.Add("text-align", "left")
                        tdInner.Controls.Add(txt)
                    ElseIf i = 3 Then
                        ddl = New DropDownList
                        ddl = GetESMateralCombo(ds.Tables(0).Rows(0).Item("EM" + j.ToString() + ""))
                        ddl.ID = "MES_" + j.ToString()
                        tdInner.Controls.Add(ddl)
                    ElseIf i = 4 Then
                        txt = New TextBox
                        txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("T" + j.ToString() + "").ToString(), 3)
                        txt.ID = "THICK_" + j.ToString()
                        TextBoxSetting(txt, "NormalTextBox")
                        tdInner.Controls.Add(txt)
                    ElseIf i = 5 Then
                        txt = New TextBox
                        txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("R" + j.ToString() + "").ToString(), 3)
                        txt.ID = "REC_" + j.ToString()
                        TextBoxSetting(txt, "NormalTextBox")
                        tdInner.Controls.Add(txt)
                    ElseIf i = 6 Then
                        txt = New TextBox
                        txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("E" + j.ToString() + "").ToString(), 3)
                        txt.ID = "EP_" + j.ToString()
                        TextBoxSetting(txt, "NormalTextBox")
                        tdInner.Controls.Add(txt)
                    ElseIf i = 7 Then
                        ddl = New DropDownList
                        ddl = GetESDept(ds.Tables(0).Rows(0).Item("D" + j.ToString() + ""))
                        ddl.ID = "DEP_" + j.ToString()
                        tdInner.Controls.Add(ddl)
                    End If
                    InnerTdSetting(tdInner, "", "center")
                    trData.Controls.Add(tdInner)
                Next

                If (j Mod 2 = 0) Then
                    trData.CssClass = "AlterNateColor1"
                Else
                    trData.CssClass = "AlterNateColor2"
                End If

                tblComparision.Controls.Add(trData)

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

    Protected Function GetSpecMateralCombo(ByVal id As Integer) As DropDownList
        Dim ddl As New DropDownList
        Dim ds As New DataSet()
        Dim objGetData As New SpecGetData.Selectdata()
        Try
            ds = objGetData.GetSpecMaterials(SpecId)
            With ddl
                .DataSource = ds
                .DataTextField = "DES"
                .DataValueField = "PKGMFGMATERIALID"
                .DataBind()
                .CssClass = "DropDown"
                .Width = 300
                .SelectedValue = id
            End With


        Catch ex As Exception
            _lErrorLble.Text = "Error:GetSpecMateralCombo:" + ex.Message.ToString()
        End Try
        Return ddl
    End Function

    Protected Function GetESMateralCombo(ByVal id As Integer) As DropDownList
        Dim ddl As New DropDownList
        Dim ds As New DataSet()
        Dim objGetData As New SpecGetData.Selectdata()
        Try
            ds = objGetData.GetESMaterials()
            With ddl
                .DataSource = ds
                .DataTextField = "MATERIALDES"
                .DataValueField = "MATID"
                .DataBind()
                .CssClass = "DropDown"
                .Width = 300
                .SelectedValue = id
            End With


        Catch ex As Exception
            _lErrorLble.Text = "Error:GetMateralCombo:" + ex.Message.ToString()
        End Try
        Return ddl
    End Function

    Protected Function GetESDept(ByVal id As Integer) As DropDownList
        Dim ddl As New DropDownList
        Dim ds As New DataSet()
        Dim objGetData As New SpecGetData.Selectdata()
        Try
            ds = objGetData.GetESDept()
            With ddl
                .DataSource = ds
                .DataTextField = "PROCDE"
                .DataValueField = "PROCID"
                .DataBind()
                .CssClass = "DropDown"
                .Width = 150
                .SelectedValue = id
            End With


        Catch ex As Exception
            _lErrorLble.Text = "Error:GetMateralCombo:" + ex.Message.ToString()
        End Try
        Return ddl
    End Function

    Protected Sub Update_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim MatSpec(11) As String
        Dim MatS1(11) As String
        Dim Thick(11) As String
        Dim Recycle(11) As String
        Dim Dept(11) As String
        Dim Expro(11) As String
        Dim objUpIns As New SpecUpdateData.UpdateInsert()
        Dim i As New Integer

        Try
            For i = 1 To 10
                MatSpec(i) = Request.Form("ctl00$Sustain3ContentPlaceHolder$MSP_" + i.ToString() + "").ToString()
                MatS1(i) = Request.Form("ctl00$Sustain3ContentPlaceHolder$MES_" + i.ToString() + "").ToString()
                Thick(i) = Request.Form("ctl00$Sustain3ContentPlaceHolder$THICK_" + i.ToString() + "").ToString()
                Recycle(i) = Request.Form("ctl00$Sustain3ContentPlaceHolder$REC_" + i.ToString() + "").ToString()
                Dept(i) = Request.Form("ctl00$Sustain3ContentPlaceHolder$DEP_" + i.ToString() + "").ToString()
                Expro(i) = Request.Form("ctl00$Sustain3ContentPlaceHolder$EP_" + i.ToString() + "").ToString()
            Next
            objUpIns.UpdateExtrusion(MatSpec, MatS1, Thick, Recycle, Dept, Expro, SpecId)
            Calculate()
            GetPageDetails()
            'UpdateExtrusion()
        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub Calculate()
        Try
            Dim SpecConn As String = System.Configuration.ConfigurationManager.AppSettings("SpecConnectionString")
            Dim Econ1Conn As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            Dim Sustain1Conn As String = System.Configuration.ConfigurationManager.AppSettings("Sustain1ConnectionString")
            Dim Obj As New SpecCalculations(SpecConn, Econ1Conn, Sustain1Conn)
            Obj.SpecCalculate(SpecId)
        Catch ex As Exception
            _lErrorLble.Text = "Error:Calculate:" + ex.Message.ToString()
        End Try
    End Sub


End Class
