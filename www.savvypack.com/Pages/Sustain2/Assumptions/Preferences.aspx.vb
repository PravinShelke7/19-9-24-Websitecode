﻿Imports System.Data
Imports System.Data.OleDb
Imports System
Imports S1GetData
Imports S1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Sustain2_Assumptions_Preferences
    Inherits System.Web.UI.Page

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
        GetErrorLable()
        GetUpdatebtn()
        GetLogOffbtn()
        GetInstructionsbtn()
        GetChartbtn()
        GetFeedbackbtn()
        GetNotesbtn()
        GetMainHeadingdiv()
        GetContentPlaceHolder()
    End Sub

    Protected Sub GetErrorLable()
        ErrorLable = Page.Master.FindControl("lblError")
    End Sub

    Protected Sub GetUpdatebtn()
        Updatebtn = Page.Master.FindControl("imgUpdate")
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
        Notesbtn.Visible = False
        'AddHandler Updatebtn.Click, AddressOf Update_Click
    End Sub

    Protected Sub GetMainHeadingdiv()
        MainHeading = Page.Master.FindControl("divMainHeading")
        MainHeading.Attributes.Add("onmouseover", "Tip('Preferences ')")
        MainHeading.Attributes.Add("onmouseout", "UnTip()")
        MainHeading.InnerHtml = "Sustain2 - Preferences"
    End Sub

    Protected Sub GetContentPlaceHolder()
        ctlContentPlaceHolder = Page.Master.FindControl("Sustain1ContentPlaceHolder")
    End Sub

#End Region

#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_PAGES_SUSTAIN2_ASSUMPTIONS_PREFERENCES")
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
            CaseId = Session("S2CaseId")
            UserRole = Session("S2UserRole")
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetSessionDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetPageDetails()
        Dim ds As New DataSet
        Dim objGetData As New S2GetData.Selectdata


        Try
            ds = objGetData.GetPref(CaseId)

            GetLCI()
            ddlLCI.SelectedValue = ds.Tables(0).Rows(0).Item("INVENTORYTYPE").ToString()

            GetEffdate()
            ddlEffdate.SelectedValue = ds.Tables(0).Rows(0).Item("EDATE").ToString()

            If ds.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                rdMetric.Checked = True
                rdEnglish.Checked = False
            Else
                rdMetric.Checked = False
                rdEnglish.Checked = True
            End If

            If ds.Tables(0).Rows(0).Item("ERGYTYPE").ToString() = "0" Then
                rdbTot.Checked = True
                rdbNon.Checked = False
                rdbRen.Checked = False
            ElseIf ds.Tables(0).Rows(0).Item("ERGYTYPE").ToString() = "1" Then
                rdbTot.Checked = False
                rdbNon.Checked = True
                rdbRen.Checked = False
            Else
                rdbTot.Checked = False
                rdbNon.Checked = False
                rdbRen.Checked = True
            End If

            If ds.Tables(0).Rows(0).Item("PVOLUSE").ToString() = "0" Then
                rdvolume.Checked = True
                rdsvolume.Checked = False
            Else
                rdvolume.Checked = False
                rdsvolume.Checked = True
            End If

            If ds.Tables(0).Rows(0).Item("ERGYCALC").ToString() = "Y" Then
                rdErgyCalc.Checked = True
                rdErgyCalcA.Checked = False
            Else
                rdErgyCalc.Checked = False
                rdErgyCalcA.Checked = True
            End If





        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetEffdate()
        Dim ds As New DataSet
        Dim objGetData As New S2GetData.Selectdata
        Dim Inventorytype As String = String.Empty
        Inventorytype = ddlLCI.SelectedValue.ToString()

        Try
            ds = objGetData.GetEffDate(Inventorytype)
            With ddlEffdate
                .DataSource = ds
                .DataTextField = "EDATE"
                .DataValueField = "EDATE"
                .DataBind()
                .Font.Size = 8
            End With





        Catch ex As Exception
            _lErrorLble.Text = "Error:GetEffdate:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetLCI()
        Dim ds As New DataSet
        Dim objGetData As New S2GetData.Selectdata


        Try
            ds = objGetData.GetLCI()
            With ddlLCI
                .DataSource = ds
                .DataTextField = "INVENTORY"
                .DataValueField = "INVENTORYID"
                .DataBind()
                .Font.Size = 8
            End With





        Catch ex As Exception
            _lErrorLble.Text = "Error:GetEffdate:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub Update_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim Unit As String = String.Empty
        Dim Volume As String = String.Empty
        Dim Effdate As String = String.Empty
        Dim LCI As String = String.Empty
        Dim ErgyCalc As String = String.Empty
        Dim ObjUpIns As New S2UpInsData.UpdateInsert()
        Dim Type As String = String.Empty
        Try
            If rdEnglish.Checked Then
                Unit = "0"
            Else
                Unit = "1"
            End If
            If rdvolume.Checked Then
                Volume = "0"
            Else
                Volume = "1"
            End If
            If rdErgyCalc.Checked Then
                ErgyCalc = "Y"
            Else
                ErgyCalc = "N"
            End If

            If rdbTot.Checked Then
                Type = "0"
            ElseIf rdbNon.Checked Then
                Type = "1"
            Else
                Type = "2"
            End If

            Effdate = ddlEffdate.SelectedValue.ToString()
            LCI = ddlLCI.SelectedValue.ToString()
            ObjUpIns.PrefrencesUpdate(CaseId, LCI, Unit, Effdate, Volume, ErgyCalc, Type)
            Calculate()
            'Update Server Date
            ObjUpIns.ServerDateUpdate(CaseId, Session("S2UserName"))
            GetPageDetails()
        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub Calculate()
        Try
            Dim Econ1Conn As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            Dim Sustain1Conn As String = System.Configuration.ConfigurationManager.AppSettings("Sustain1ConnectionString")
            Dim Econ2Conn As String = System.Configuration.ConfigurationManager.AppSettings("Econ2ConnectionString")
            Dim Sustain2Conn As String = System.Configuration.ConfigurationManager.AppSettings("Sustain2ConnectionString")
            Dim obj As New Sustain2Calculation.Sustain2Calculations(Sustain2Conn, Econ2Conn, Sustain1Conn, Econ1Conn)
            obj.Sustain2Calculate(CaseId)
        Catch ex As Exception
            ErrorLable.Text = "Error:Calculate:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub ddlLCI_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlLCI.SelectedIndexChanged
        Try
            GetEffdate()
        Catch ex As Exception
            _lErrorLble.Text = "Error:ddlLCI_SelectedIndexChanged:" + ex.Message.ToString()
        End Try
    End Sub
End Class
