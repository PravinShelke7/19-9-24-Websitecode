Imports System.Data
Imports System.Data.OleDb
Imports System
Imports DistGetData
Imports DistUpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter


Partial Class Pages_Distribution_Results_Comparision
    Inherits System.Web.UI.Page
#Region "Get Set Variables"

    Dim _strType As String
    Dim _strChType As String

    Public Property Type() As String
        Get
            Return _strType
        End Get
        Set(ByVal Value As String)
            _strType = Value
        End Set
    End Property

    Public Property ChType() As String
        Get
            Return _strChType
        End Get
        Set(ByVal Value As String)
            _strChType = Value
        End Set
    End Property

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("Back") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If
            GetSessionDetails()
            If Session("Back") = Nothing Then
                Response.Redirect("~/Errors/Error.aspx")
            End If

            Type = Convert.ToString(Request.QueryString("Type"))
            If Type.Length > 0 Then
                Dim objCryptoHelper As New CryptoHelper()
                Type = objCryptoHelper.Decrypt(Type)
            End If

            If Type = "PL" Then
                lblTitle.Text = "EDistribution - Profit and Loss Comparison Selector"
                Page.Title = "EDist-Profit and Loss Comparison Selector"
            ElseIf Type = "PLDEP" Then
                lblTitle.Text = "EDistribution - Profit and Loss With Depreciation Comparison Selector"
                Page.Title = "EDist-Profit and Loss With Depreciation Comparison Selector"
            ElseIf Type = "CST" Then
                lblTitle.Text = "EDistribution - Cost Comparison Selector"
                Page.Title = "EDist-Cost Comparison Selector"
            ElseIf Type = "CSTDEP" Then
                lblTitle.Text = "EDistribution - Cost Comparison With Depreciation Selector"
                Page.Title = "EDist-Cost Comparison With Depreciation Selector"
            End If

            If Not IsPostBack Then
                GetPCaseDetails()
                GetChartType()
            End If

        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetSessionDetails()
        Try

            lblSessionCaseId.Text = Session("DistCaseId")

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetSessionDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetPCaseDetails()
        Dim objGetData As New DistGetData.Selectdata()
        Dim ds As New DataSet()
        Try
            ds = objGetData.GetPCaseDetails(Session("USERID").ToString())
            With ddlPCases
                .DataSource = ds
                .DataTextField = "CASEDES"
                .DataValueField = "CASEID"
                .DataBind()
                .Font.Size = 8
            End With
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPCaseDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetChartType()
        Try
            Dim Item As New ListItem
            Item.Value = "SBC"
            Item.Text = "Stacked Bar Chart"
            If Type = "PL" Or Type = "PLDEP" Then
                ddlChrtType.Items.Remove(Item)
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetChartType:" + ex.Message.ToString() + ""
        End Try

    End Sub

    Protected Sub btnType_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnType.Click
        Dim TypeDes As String = String.Empty
        Dim objCryptoHelper As New CryptoHelper()
        Dim CaseId As String = String.Empty
        Dim URL As String = String.Empty
        Dim Title As String = String.Empty
        Try
            TypeDes = objCryptoHelper.Encrypt(ddlType.SelectedValue)
            Title = ddlType.SelectedValue
            CaseId = objCryptoHelper.Encrypt(ddlPCases.SelectedValue)
            If Type = "PL" Then
                URL = "CResultspl.aspx?Type=" + TypeDes + "&CaseId=" + CaseId + ""
            ElseIf Type = "PLDEP" Then
                URL = "CResultsplDep.aspx?Type=" + TypeDes + "&CaseId=" + CaseId + ""
            ElseIf Type = "CST" Then
                URL = "CResultcost.aspx?Type=" + TypeDes + "&CaseId=" + CaseId + ""
            ElseIf Type = "CSTDEP" Then
                URL = "CResultcostDep.aspx?Type=" + TypeDes + "&CaseId=" + CaseId + ""
            End If



            Page.ClientScript.RegisterStartupScript(Me.GetType(), Type, "OpenNewWindow('" + URL + "','" + Title + "');", True)


        Catch ex As Exception
            _lErrorLble.Text = "Error:btnType_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnChartType_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChartType.Click
        Try
            Dim objCryptoHelper As New CryptoHelper()
            Dim Title As String = String.Empty
            Dim CaseId As String = String.Empty
            Dim URL As String = String.Empty
            Dim IsDep As String = String.Empty
            Dim PrftCost As String = String.Empty


            Title = ddlChrtType.SelectedValue
            CaseId = objCryptoHelper.Encrypt(ddlPCases.SelectedValue)
            ChType = objCryptoHelper.Encrypt(ddlChrtType.SelectedValue.ToString())

            'If Type = "PL" Then
            '    If ddlChrtType.SelectedValue = "RBC" Then
            '        URL = "../../../Charts/E2Charts/CRProftAndLoss.aspx?&CaseId=" + CaseId + "&IsDep=" + objCryptoHelper.Encrypt("N") + ""
            '    ElseIf ddlChrtType.SelectedValue = "SBC" Then
            '        URL = "../../../Charts/E2Charts/CSProftAndLoss.aspx?&CaseId=" + CaseId + "&IsDep=" + objCryptoHelper.Encrypt("N") + ""
            '    End If
            'ElseIf Type = "PLDEP" Then
            '    If ddlChrtType.SelectedValue = "RBC" Then
            '        URL = "../../../Charts/E2Charts/CRProftAndLoss.aspx?&CaseId=" + CaseId + "&IsDep=" + objCryptoHelper.Encrypt("D") + ""
            '    ElseIf ddlChrtType.SelectedValue = "SBC" Then
            '        URL = "../../../Charts/E2Charts/CSProftAndLoss.aspx?&CaseId=" + CaseId + "&IsDep=" + objCryptoHelper.Encrypt("D") + ""
            '    End If
            'ElseIf Type = "CST" Then
            '    If ddlChrtType.SelectedValue = "RBC" Then
            '        URL = "../../../Charts/E2Charts/CRCost.aspx?&CaseId=" + CaseId + "&IsDep=" + objCryptoHelper.Encrypt("N") + ""
            '    ElseIf ddlChrtType.SelectedValue = "SBC" Then
            '        URL = "../../../Charts/E2Charts/CSCost.aspx?&CaseId=" + CaseId + "&IsDep=" + objCryptoHelper.Encrypt("N") + ""
            '    End If
            'ElseIf Type = "CSTDEP" Then
            '    If ddlChrtType.SelectedValue = "RBC" Then
            '        URL = "../../../Charts/E2Charts/CRCost.aspx?&CaseId=" + CaseId + "&IsDep=" + objCryptoHelper.Encrypt("D") + ""
            '    ElseIf ddlChrtType.SelectedValue = "SBC" Then
            '        URL = "../../../Charts/E2Charts/CSCost.aspx?&CaseId=" + CaseId + "&IsDep=" + objCryptoHelper.Encrypt("D") + ""
            '    End If
            'End If


            If Type = "CSTDEP" Or Type = "PLDEP" Then
                IsDep = "D"
            ElseIf Type = "PL" Or Type = "CST" Then
                IsDep = "N"
            End If

            If Type = "PL" Or Type = "PLDEP" Then
                PrftCost = "PRFT"
            ElseIf Type = "CST" Or Type = "CSTDEP" Then
                PrftCost = "COST"
            End If

            URL = "../../../Charts/DistCharts/CPrftCost.aspx?CaseId=" + CaseId + "&PrftCost=" + objCryptoHelper.Encrypt(PrftCost) + "&ChartType=" + ChType + "&IsDep=" + objCryptoHelper.Encrypt(IsDep) + ""

            Page.ClientScript.RegisterStartupScript(Me.GetType(), Type, "OpenNewWindow('" + URL + "','" + Title + "');", True)


        Catch ex As Exception
            _lErrorLble.Text = "Error:btnChartType_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub




End Class
