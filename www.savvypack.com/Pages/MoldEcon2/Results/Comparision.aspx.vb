Imports System.Data
Imports System.Data.OleDb
Imports System
Imports MoldE2GetData
Imports MoldE2UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter


Partial Class Pages_MoldEcon2_Results_Comparision
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
                lblTitle.Text = "Econ2 Mold - Profit and Loss Comparison Selector"
                Page.Title = "E2 Mold-Profit and Loss Comparison Selector"
            ElseIf Type = "PLDEP" Then
                lblTitle.Text = "Econ2 Mold - Profit and Loss With Depreciation Comparison Selector"
                Page.Title = "E2 Mold-Profit and Loss With Depreciation Comparison Selector"
            ElseIf Type = "CST" Then
                lblTitle.Text = "Econ2 Mold - Cost Comparison Selector"
                Page.Title = "E2 Mold-Cost Comparison Selector"
            ElseIf Type = "CSTDEP" Then
                lblTitle.Text = "Econ2 Mold - Cost Comparison With Depreciation Selector"
                Page.Title = "E2 Mold-Cost Comparison With Depreciation Selector"
            ElseIf Type = "CustomerPL" Then
                lblTitle.Text = "Econ2 Mold - Customer Profit and Loss Comparison Selector"
                Page.Title = "E2 Mold-Customer Profit and Loss Comparison Selector"
            ElseIf Type = "CustomerPLDEP" Then
                lblTitle.Text = "Econ2 Mold - Customer Profit and Loss With Depreciation Comparison Selector"
                Page.Title = "E2 Mold-Customer Profit and Loss With Depreciation Comparison Selector"
            ElseIf Type = "CustomerCST" Then
                lblTitle.Text = "Econ2 Mold - Customer Cost Comparison Selector"
                Page.Title = "E2 Mold-Customer Cost Comparison Selector"
            ElseIf Type = "CustomerCSTDEP" Then
                lblTitle.Text = "Econ2 Mold - Customer Cost Comparison With Depreciation Selector"
                Page.Title = "E2 Mold-Customer Cost Comparison With Depreciation Selector"
            End If

            If Not IsPostBack Then
                GetPCaseDetails()
                GetChartType()
                GetComparisionType()
            End If

        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetSessionDetails()
        Try

            lblSessionCaseId.Text = Session("MoldE2CaseId")

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetSessionDetails:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub GetComparisionType()
        Dim i As New Integer
        Try
            Dim Item As New ListItem
            If Type = "CustomerPL" Or Type = "CustomerPLDEP" Or Type = "CustomerCST" Or Type = "CustomerCSTDEP" Then
                For i = 0 To ddlType.Items.Count - 1
                    Item = ddlType.Items(i)
                    Item.Text = Item.Text.Replace("Total", "Customer Total")
                Next
            End If

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub GetPCaseDetails()
        Dim objGetData As New MoldE2GetData.Selectdata()
        Dim ds As New DataSet()
        Try
            ds = objGetData.GetPCaseDetails(Session("UserId").ToString())
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
            If Type = "PL" Or Type = "PLDEP" Or Type = "CustomerPL" Or Type = "CustomerPLDEP" Then
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
                'FOR CUSTOMER RESULTS
            ElseIf Type = "CustomerPL" Then
                URL = "CResultspl2.aspx?Type=" + TypeDes + "&CaseId=" + CaseId + ""
            ElseIf Type = "CustomerPLDEP" Then
                URL = "CResultsplDep2.aspx?Type=" + TypeDes + "&CaseId=" + CaseId + ""
            ElseIf Type = "CustomerCST" Then
                URL = "CResultcost2.aspx?Type=" + TypeDes + "&CaseId=" + CaseId + ""
            ElseIf Type = "CustomerCSTDEP" Then
                URL = "CResultcostDep2.aspx?Type=" + TypeDes + "&CaseId=" + CaseId + ""
            End If



            Page.ClientScript.RegisterStartupScript(Me.GetType(), Type, "OpenNewWindow('" + URL + "','" + Title + "');", True)


        Catch ex As Exception
            _lErrorLble.Text = "Error:btnType_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    'Protected Sub btnChartType_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChartType.Click
    '    Try
    '        Dim objCryptoHelper As New CryptoHelper()
    '        Dim Title As String = String.Empty
    '        Dim CaseId As String = String.Empty
    '        Dim URL As String = String.Empty

    '        Title = ddlChrtType.SelectedValue
    '        CaseId = objCryptoHelper.Encrypt(ddlPCases.SelectedValue)
    '        ChType = objCryptoHelper.Encrypt(ddlChrtType.SelectedValue.ToString())

    '        If Type = "PL" Then
    '            If ddlChrtType.SelectedValue = "RBC" Then
    '                URL = "../../../Charts/E2Charts/CRProftAndLoss.aspx?&CaseId=" + CaseId + "&IsDep=" + objCryptoHelper.Encrypt("N") + ""
    '            ElseIf ddlChrtType.SelectedValue = "SBC" Then
    '                URL = "../../../Charts/E2Charts/CSProftAndLoss.aspx?&CaseId=" + CaseId + "&IsDep=" + objCryptoHelper.Encrypt("N") + ""
    '            End If
    '        ElseIf Type = "PLDEP" Then
    '            If ddlChrtType.SelectedValue = "RBC" Then
    '                URL = "../../../Charts/E2Charts/CRProftAndLoss.aspx?&CaseId=" + CaseId + "&IsDep=" + objCryptoHelper.Encrypt("D") + ""
    '            ElseIf ddlChrtType.SelectedValue = "SBC" Then
    '                URL = "../../../Charts/E2Charts/CSProftAndLoss.aspx?&CaseId=" + CaseId + "&IsDep=" + objCryptoHelper.Encrypt("D") + ""
    '            End If
    '        ElseIf Type = "CST" Then
    '            If ddlChrtType.SelectedValue = "RBC" Then
    '                URL = "../../../Charts/E2Charts/CRCost.aspx?&CaseId=" + CaseId + "&IsDep=" + objCryptoHelper.Encrypt("N") + ""
    '            ElseIf ddlChrtType.SelectedValue = "SBC" Then
    '                URL = "../../../Charts/E2Charts/CSCost.aspx?&CaseId=" + CaseId + "&IsDep=" + objCryptoHelper.Encrypt("N") + ""
    '            End If
    '        ElseIf Type = "CSTDEP" Then
    '            If ddlChrtType.SelectedValue = "RBC" Then
    '                URL = "../../../Charts/E2Charts/CRCost.aspx?&CaseId=" + CaseId + "&IsDep=" + objCryptoHelper.Encrypt("D") + ""
    '            ElseIf ddlChrtType.SelectedValue = "SBC" Then
    '                URL = "../../../Charts/E2Charts/CSCost.aspx?&CaseId=" + CaseId + "&IsDep=" + objCryptoHelper.Encrypt("D") + ""
    '            End If
    '        End If
    '        Page.ClientScript.RegisterStartupScript(Me.GetType(), Type, "OpenNewWindow('" + URL + "','" + Title + "');", True)


    '    Catch ex As Exception
    '        _lErrorLble.Text = "Error:btnChartType_Click:" + ex.Message.ToString() + ""
    '    End Try
    'End Sub
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

            If Type = "CSTDEP" Or Type = "PLDEP" Or Type = "CustomerCSTDEP" Or Type = "CustomerPLDEP" Then
                IsDep = "D"
            ElseIf Type = "PL" Or Type = "CST" Or Type = "CustomerPL" Or Type = "CustomerCST" Then
                IsDep = "N"
            End If

            If Type = "PL" Or Type = "PLDEP" Then
                PrftCost = "PRFT"
            ElseIf Type = "CustomerPL" Or Type = "CustomerPLDEP" Then
                PrftCost = "CPRFT"
            ElseIf Type = "CST" Or Type = "CSTDEP" Then
                PrftCost = "COST"
            ElseIf Type = "CustomerCST" Or Type = "CustomerCSTDEP" Then
                PrftCost = "CCOST"
            End If

            URL = "../../../Charts/MoldE2Charts/CPrftCost.aspx?CaseId=" + CaseId + "&PrftCost=" + objCryptoHelper.Encrypt(PrftCost) + "&ChartType=" + ChType + "&IsDep=" + objCryptoHelper.Encrypt(IsDep) + ""

            Page.ClientScript.RegisterStartupScript(Me.GetType(), Type, "OpenNewWindow('" + URL + "','" + Title + "');", True)


        Catch ex As Exception
            _lErrorLble.Text = "Error:btnChartType_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub




End Class
