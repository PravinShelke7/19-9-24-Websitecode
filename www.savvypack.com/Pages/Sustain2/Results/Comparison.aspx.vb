﻿Imports System.Data
Imports System.Data.OleDb
Imports System
Imports S1GetData
Imports S1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Sustain2_Results_Comparison
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
          lblTag.Text = "Copyright 1997 - " + Now.Year().ToString() + " Savvypack Corporation."
            GetSessionDetails()
            Type = Convert.ToString(Request.QueryString("Type"))
            If Type.Length > 0 Then
                Dim objCryptoHelper As New CryptoHelper()
                Type = objCryptoHelper.Decrypt(Type)
            End If
            If Type = "ERGY" Then
                lblTitle.Text = "Sustain2 - Energy Comparison Selector"
                Page.Title = "S2-Energy Comparison Selector"
            ElseIf Type = "CERGY" Then
                lblTitle.Text = "Sustain2 - Customer Energy Comparison Selector"
                Page.Title = "S2-Customer Energy Comparison Selector"
            ElseIf Type = "CustomerGHG" Then
                lblTitle.Text = "Sustain2 - Customer GHG Comparison Selector"
                Page.Title = "S2-Customer GHG Comparison Selector"
            ElseIf Type = "CustomerEOL" Then
                lblTitle.Text = "Sustain2 - Customer End Of Life Comparison Selector"
                Page.Title = "S2-Customer End Of Life Comparison Selector"
            ElseIf Type = "WATER" Then
                lblTitle.Text = "Sustain2 - Water Comparison Selector"
                Page.Title = "S2-Water Comparison Selector"
            ElseIf Type = "CWATER" Then
                lblTitle.Text = "Sustain2 - Customer Water Comparison Selector"
                Page.Title = "S2-Customer Water Comparison Selector"
            ElseIf Type = "" Then

            End If
            If Not IsPostBack Then
                GetPCaseDetails()
                GetComparisionType()
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetSessionDetails()
        Try
            lblSessionCaseId.Text = Session("S2CaseId")

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetSessionDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetPCaseDetails()
        Dim objGetData As New S2GetData.Selectdata()
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

    Protected Sub GetComparisionType()
        Dim i As New Integer
        Try
            Dim Item As New ListItem
            If Type = "GHG" Then
                For i = 0 To ddlType.Items.Count - 1
                    Item = ddlType.Items(i)
                    Item.Text = Item.Text.Replace("Energy", "GHG")
                Next
            ElseIf Type = "CustomerGHG" Then
                btnChartType.Enabled = True
                'ddlType.Width = 150
                'ddlType.Items.RemoveAt(ddlType.Items.Count - 1)
                For i = 0 To ddlType.Items.Count - 1
                    Item = ddlType.Items(i)
                    'If i = 0 Then
                    Item.Text = Item.Text.Replace("Energy", "Customer GHG")
                    '    Item.Value = "CustomerGHG"
                    'Else
                    '    Item.Text = "Gasoline"
                    '    Item.Value = "Gasoline"
                    'End If
                Next
            ElseIf Type = "CERGY" Then
                btnChartType.Enabled = True
                'ddlType.Width = 160
                'ddlType.Items.RemoveAt(ddlType.Items.Count - 1)
                For i = 0 To ddlType.Items.Count - 1
                    Item = ddlType.Items(i)
                    'If i = 0 Then
                    Item.Text = Item.Text.Replace("Energy", "Customer Energy")
                    '    Item.Value = "CustomerEnergy"
                    'Else
                    '    Item.Text = "Electricity"
                    '    Item.Value = "Electricity"
                    'End If
                Next
            ElseIf Type = "CWATER" Then
                btnChartType.Enabled = True
                'ddlType.Width = 150
                'ddlType.Items.RemoveAt(ddlType.Items.Count - 1)

                For i = 0 To ddlType.Items.Count - 1
                    Item = ddlType.Items(i)
                    'If i = 0 Then
                    Item.Text = Item.Text.Replace("Energy", "Customer Water")
                    '    Item.Value = "Water"
                    'Else
                    '    Item.Text = Item.Text.Replace("Energy Per Weight", "Customer Water")
                    '    Item.Value = "CustomerWater"
                    'End If
                Next
            ElseIf Type = "WATER" Then
                For i = 0 To ddlType.Items.Count - 1
                    Item = ddlType.Items(i)
                    Item.Text = Item.Text.Replace("Energy", "Water")
                Next
            ElseIf Type = "CustomerEOL" Then
                btnChartType.Enabled = True
                ddlType.Width = 160

                'Removing Old parameters
                ddlType.Items.RemoveAt(ddlType.Items.Count - 1)
                ddlType.Items.RemoveAt(ddlType.Items.Count - 1)
                ddlType.Items.RemoveAt(ddlType.Items.Count - 1)

                'Adding new one
                ddlType.AppendDataBoundItems = True
                ddlType.Items.Insert(0, New ListItem("Material Balance", "MatBalance"))
                ddlType.Items.Insert(1, New ListItem("Material to Recycling", "MatRec"))
                ddlType.Items.Insert(2, New ListItem("Material to Incineration", "MatInc"))
                ddlType.Items.Insert(3, New ListItem("Material to Composting", "MatComp"))
                ddlType.Items.Insert(4, New ListItem("Material to Landfill", "MatLandFill"))

                'For Removing Stacked chart item from dropdown
                ddlChrtType.Items.RemoveAt(ddlChrtType.Items.Count - 1)


            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnType_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnType.Click
        Dim TypeDes As String = String.Empty
        Dim objCryptoHelper As New CryptoHelper()
        Dim CaseId As String = String.Empty
        Dim URL As String = String.Empty
        Dim Title As String = String.Empty
        Try
            TypeDes = objCryptoHelper.Encrypt(ddlType.SelectedValue).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!")
            Title = ddlType.SelectedValue
            CaseId = objCryptoHelper.Encrypt(ddlPCases.SelectedValue).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!")

            If Type = "ERGY" Then
                URL = "CErgyRes.aspx?Type=" + TypeDes + "&CaseId=" + CaseId + ""
            ElseIf Type = "GHG" Then
                URL = "CGhgRes.aspx?Type=" + TypeDes + "&CaseId=" + CaseId + ""
            ElseIf Type = "CustomerGHG" Then
                URL = "CGhgRes2.aspx?Type=" + TypeDes + "&CaseId=" + CaseId + ""
            ElseIf Type = "CERGY" Then
                URL = "CErgyRes2.aspx?Type=" + TypeDes + "&CaseId=" + CaseId + ""
            ElseIf Type = "CustomerEOL" Then
                URL = "CEndOfLife.aspx?Type=" + TypeDes + "&CaseId=" + CaseId + ""
            ElseIf Type = "WATER" Then
                URL = "CWaterRes.aspx?Type=" + TypeDes + "&CaseId=" + CaseId + ""
            ElseIf Type = "CWATER" Then
                URL = "CWaterRes2.aspx?Type=" + TypeDes + "&CaseId=" + CaseId + ""
            End If




            Page.ClientScript.RegisterStartupScript(Me.GetType(), Type, "OpenNewWindow('" + URL + "','" + Title + "');", True)


        Catch ex As Exception
            _lErrorLble.Text = "Error:btnType_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    'Protected Sub btnChartType_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChartType.Click
    '    Dim TypeDes As String = String.Empty
    '    Dim objCryptoHelper As New CryptoHelper()
    '    Dim CaseId As String = String.Empty
    '    Dim URL As String = String.Empty
    '    Dim Title As String = String.Empty
    '    Try
    '        TypeDes = ddlChrtType.SelectedValue.ToString()
    '        CaseId = objCryptoHelper.Encrypt(ddlPCases.SelectedValue).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!")
    '        If Type = "ERGY" Then
    '            If TypeDes = "RBC" Then
    '                URL = "../../../Charts/S2Charts/CEnergyCharts.aspx?&CaseId=" + CaseId + ""
    '            ElseIf TypeDes = "SBC" Then
    '                URL = "../../../Charts/S2Charts/CStackErgyChart.aspx?&CaseId=" + CaseId + ""
    '            End If
    '        ElseIf Type = "CERGY" Then
    '            If TypeDes = "RBC" Then
    '                URL = "../../../Charts/S2Charts/CEnergyCharts2.aspx?&CaseId=" + CaseId + ""
    '            ElseIf TypeDes = "SBC" Then
    '                URL = "../../../Charts/S2Charts/CStackErgyChart2.aspx?&CaseId=" + CaseId + ""
    '            End If
    '        ElseIf Type = "GHG" Then
    '            If TypeDes = "RBC" Then
    '                URL = "../../../Charts/S2Charts/CGHGCharts.aspx?&CaseId=" + CaseId + ""
    '            ElseIf TypeDes = "SBC" Then
    '                URL = "../../../Charts/S2Charts/CStackGHGChart.aspx?&CaseId=" + CaseId + ""
    '            End If
    '        ElseIf Type = "CustomerGHG" Then
    '            If TypeDes = "RBC" Then
    '                URL = "../../../Charts/S2Charts/CGHGCharts2.aspx?&CaseId=" + CaseId + ""
    '            ElseIf TypeDes = "SBC" Then
    '                URL = "../../../Charts/S2Charts/CStackGHGChart2.aspx?&CaseId=" + CaseId + ""
    '            End If
    '        ElseIf Type = "CustomerEOL" Then
    '            If TypeDes = "RBC" Then
    '                URL = "../../../Charts/S2Charts/CEolCharts.aspx?&CaseId=" + CaseId + ""
    '            ElseIf TypeDes = "SBC" Then
    '                URL = "../../../Charts/S2Charts/CStackGHGChart2.aspx?&CaseId=" + CaseId + ""
    '            End If

    '        End If

    '        Page.ClientScript.RegisterStartupScript(Me.GetType(), Type, "OpenNewWindow('" + URL + "','" + TypeDes + "');", True)
    '    Catch ex As Exception
    '        _lErrorLble.Text = "Error:btnType_Click:" + ex.Message.ToString() + ""
    '    End Try
    'End Sub

    Protected Sub btnChartType_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChartType.Click
        Dim TypeDes As String = String.Empty
        Dim objCryptoHelper As New CryptoHelper()
        Dim CaseId As String = String.Empty
        Dim URL As String = String.Empty
        Dim Title As String = String.Empty
        Try
            TypeDes = ddlChrtType.SelectedValue.ToString()
            CaseId = objCryptoHelper.Encrypt(ddlPCases.SelectedValue).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!")
            If Type = "ERGY" Or Type = "GHG" Or Type = "WATER" Then
                URL = "../../../Charts/S2Charts/CErgyGhgWAT.aspx?CaseId=" + CaseId + "&ErgyGHG=" + objCryptoHelper.Encrypt(Type) + "&ChartType=" + objCryptoHelper.Encrypt(TypeDes) + ""
            ElseIf Type = "CERGY" Then
                If TypeDes = "RBC" Then
                    URL = "../../../Charts/S2Charts/CEnergyCharts2.aspx?&CaseId=" + CaseId + ""
                ElseIf TypeDes = "SBC" Then
                    URL = "../../../Charts/S2Charts/CStackErgyChart2.aspx?&CaseId=" + CaseId + ""
                End If
            ElseIf Type = "CustomerGHG" Then
                If TypeDes = "RBC" Then
                    URL = "../../../Charts/S2Charts/CGHGCharts2.aspx?&CaseId=" + CaseId + ""
                ElseIf TypeDes = "SBC" Then
                    URL = "../../../Charts/S2Charts/CStackGHGChart2.aspx?&CaseId=" + CaseId + ""
                End If
            ElseIf Type = "CWATER" Then
                If TypeDes = "RBC" Then
                    URL = "../../../Charts/S2Charts/CWATERCharts2.aspx?&CaseId=" + CaseId + ""
                ElseIf TypeDes = "SBC" Then
                    URL = "../../../Charts/S2Charts/CStackWATERChart2.aspx?&CaseId=" + CaseId + ""
                End If
            ElseIf Type = "CustomerEOL" Then
                If TypeDes = "RBC" Then
                    URL = "../../../Charts/S2Charts/CEolCharts.aspx?&CaseId=" + CaseId + ""
                ElseIf TypeDes = "SBC" Then
                    URL = "../../../Charts/S2Charts/CStackGHGChart2.aspx?&CaseId=" + CaseId + ""
                End If

            End If

            Page.ClientScript.RegisterStartupScript(Me.GetType(), Type, "OpenNewWindow('" + URL + "','" + TypeDes + "');", True)
        Catch ex As Exception
            _lErrorLble.Text = "Error:btnType_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
End Class
