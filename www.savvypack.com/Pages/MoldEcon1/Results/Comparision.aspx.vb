Imports System.Data
Imports System.Data.OleDb
Imports System
Imports MoldE1GetData
Imports MoldE1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_MoldEcon1_Results_Comparision
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
            If Session("Service") = "COMP" Then
                E1Table.Attributes.Add("class", "E1CompModule")
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
                lblTitle.Text = "Econ1 Mold - Profit and Loss Comparison Selector"
                Page.Title = "E1 Mold-Profit and Loss Comparison Selector"
            ElseIf Type = "PLC" Then
                lblTitle.Text = "Econ1 Mold - Customer Profit and Loss Comparison Selector"
                Page.Title = "E1 Mold-Customer Profit and Loss Comparison Selector"
            ElseIf Type = "PLDEP" Then
                lblTitle.Text = "Econ1 Mold - Profit and Loss With Depreciation Comparison Selector"
                Page.Title = "E1 Mold-Profit and Loss With Depreciation Comparison Selector"
            ElseIf Type = "PLDEPC" Then
                lblTitle.Text = "Econ1 Mold - Customer Profit and Loss With Depreciation Comparison Selector"
                Page.Title = "E1 Mold-Customer Profit and Loss With Depreciation Comparison Selector"
            ElseIf Type = "CST" Then
                lblTitle.Text = "Econ1 Mold - Cost Comparison Selector"
                Page.Title = "E1 Mold-Cost Comparison Selector"
            ElseIf Type = "CSTC" Then
                lblTitle.Text = "Econ1 Mold - Customer Cost Comparison Selector"
                Page.Title = "E1 Mold-Customer Cost Comparison Selector"
            ElseIf Type = "CSTDEP" Then
                lblTitle.Text = "Econ1 Mold - Cost Comparison With Depreciation Selector"
                Page.Title = "E1 Mold-Cost Comparison With Depreciation Selector"
            ElseIf Type = "CSTDEPC" Then
                lblTitle.Text = "Econ1 Mold - Customer Cost Comparison With Depreciation Selector"
                Page.Title = "E1 Mold-Customer Cost Comparison With Depreciation Selector"
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

            lblSessionCaseId.Text = Session("MoldE1CaseId")

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetSessionDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetPCaseDetails()
        Dim objGetData As New MoldE1GetData.Selectdata()
        Dim ds As New DataSet()
        Try
            'ds = objGetData.GetPCaseDetails(Session("E1UserName").ToString())
            If Session("MoldE1LicAdmin") = "N" Then
                If Session("Service") = "COMP" Then
                    ds = objGetData.GetPropCaseDetails(Session("MoldE1UserName").ToString(), Session("ServiceId"))
                Else
                    ds = objGetData.GetPCaseDetails(Session("UserId").ToString())
                End If
            Else
                If Session("Service") = "COMP" Then
                    ds = objGetData.GetCompPCaseDetailsByLicense(Session("MoldE1UserName").ToString(), Session("ServiceId").ToString())
                Else
                    ds = objGetData.GetPCaseDetailsByLicense(Session("MoldE1UserName").ToString())
                End If

            End If
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
            If Type = "PL" Or Type = "PLDEP" Or Type = "PLC" Or Type = "PLDEPC" Then
                ddlChrtType.Items.Remove(Item)
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetChartType:" + ex.Message.ToString() + ""
        End Try

    End Sub
    Protected Sub GetComparisionType()
        Dim i As New Integer
        Try
            Dim Item As New ListItem
            If Type = "PLC" Or Type = "PLDEPC" Or Type = "CSTC" Or Type = "CSTDEPC" Then
                For i = 0 To ddlType.Items.Count - 1
                    Item = ddlType.Items(i)
                    Item.Text = Item.Text.Replace("Total", "Customer Total")
                Next
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

        Dim objGetData As New MoldE1GetData.Selectdata
        Dim ds As New DataSet
        Dim i As Integer
        Dim formatId1 As String
        Dim formatId2 As String
        Dim flag As Boolean
        Dim Units1 As String = ""
        Dim Units2 As String = ""
        Try
            TypeDes = objCryptoHelper.Encrypt(ddlType.SelectedValue)
            Title = ddlType.SelectedValue
            CaseId = objCryptoHelper.Encrypt(ddlPCases.SelectedValue)

            'getting Product Format
            ds = objGetData.GetProductFormtByID(Session("MoldE1CaseId"), ddlPCases.SelectedValue)
            If ds.Tables(0).Rows.Count = "1" Then
                flag = True
            Else
                formatId1 = ds.Tables(0).Rows(0).Item("M1").ToString()
                formatId2 = ds.Tables(0).Rows(1).Item("M1").ToString()
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    If ds.Tables(0).Rows(i).Item("CaseId") = Session("MoldE1CaseId") Then
                        Units1 = ds.Tables(0).Rows(i).Item("UNITS").ToString()
                    ElseIf ds.Tables(0).Rows(i).Item("CaseId") = ddlPCases.SelectedValue Then
                        Units2 = ds.Tables(0).Rows(i).Item("UNITS").ToString()
                    End If
                Next


                If (formatId1 = "1" Or formatId1 = "17") Then
                    If (formatId2 = "1" Or formatId2 = "17") Then
                        flag = True
                    Else
                        flag = False
                    End If
                Else
                    If (formatId2 = "1" Or formatId2 = "17") Then
                        flag = False
                    Else
                        flag = True
                    End If

                End If

            End If

            If Type = "PL" Then
                URL = "CResultspl.aspx?Type=" + TypeDes + "&CaseId=" + CaseId + ""
            ElseIf Type = "PLC" Then
                URL = "CResultspl2.aspx?Type=" + TypeDes + "&CaseId=" + CaseId + ""
            ElseIf Type = "PLDEP" Then
                URL = "CResultsplDep.aspx?Type=" + TypeDes + "&CaseId=" + CaseId + ""
            ElseIf Type = "PLDEPC" Then
                URL = "CResultsplDep2.aspx?Type=" + TypeDes + "&CaseId=" + CaseId + ""
            ElseIf Type = "CST" Then
                URL = "CResultcost.aspx?Type=" + TypeDes + "&CaseId=" + CaseId + ""
            ElseIf Type = "CSTC" Then
                URL = "CResultcost2.aspx?Type=" + TypeDes + "&CaseId=" + CaseId + ""
            ElseIf Type = "CSTDEP" Then
                URL = "CResultcostDep.aspx?Type=" + TypeDes + "&CaseId=" + CaseId + ""
            ElseIf Type = "CSTDEPC" Then
                URL = "CResultcostDep2.aspx?Type=" + TypeDes + "&CaseId=" + CaseId + ""
            End If

            If flag = True Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), Type, "OpenNewWindow('" + URL + "','" + Title + "');", True)
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('These cases are not comparable because Case " + Session("MoldE1CaseId").ToString() + " is in " + Units1 + " and Case " + ddlPCases.SelectedValue + " is in " + Units2 + ".');", True)

            End If

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

            Dim objGetData As New MoldE1GetData.Selectdata
            Dim ds As New DataSet
            Dim i As Integer
            Dim formatId1 As String
            Dim formatId2 As String
            Dim flag As Boolean = True
            Dim Units1 As String = ""
            Dim Units2 As String = ""

            Title = ddlChrtType.SelectedValue
            CaseId = objCryptoHelper.Encrypt(ddlPCases.SelectedValue)
            ChType = objCryptoHelper.Encrypt(ddlChrtType.SelectedValue.ToString())

            'getting Product Format
            ds = objGetData.GetProductFormtByID(Session("MoldE1CaseId"), ddlPCases.SelectedValue)
            If ds.Tables(0).Rows.Count = "1" Then
                flag = True
            Else
                formatId1 = ds.Tables(0).Rows(0).Item("M1").ToString()
                formatId2 = ds.Tables(0).Rows(1).Item("M1").ToString()
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    If ds.Tables(0).Rows(i).Item("CaseId") = Session("MoldE1CaseId") Then
                        Units1 = ds.Tables(0).Rows(i).Item("UNITS").ToString()
                    ElseIf ds.Tables(0).Rows(i).Item("CaseId") = ddlPCases.SelectedValue Then
                        Units2 = ds.Tables(0).Rows(i).Item("UNITS").ToString()
                    End If
                Next


                If (formatId1 = "1" Or formatId1 = "17") Then
                    If (formatId2 = "1" Or formatId2 = "17") Then
                        flag = True
                    Else
                        flag = False
                    End If
                Else
                    If (formatId2 = "1" Or formatId2 = "17") Then
                        flag = False
                    Else
                        flag = True
                    End If

                End If
            End If

            If Type = "CSTDEP" Or Type = "PLDEP" Or Type = "PLDEPC" Or Type = "CSTDEPC" Then
                IsDep = "D"
            ElseIf Type = "PL" Or Type = "CST" Or Type = "PLC" Or Type = "CSTC" Then
                IsDep = "N"
            End If

            If Type = "PL" Or Type = "PLDEP" Then
                PrftCost = "PRFT"
            ElseIf Type = "PLC" Or Type = "PLDEPC" Then
                PrftCost = "CPRFT"
            ElseIf Type = "CST" Or Type = "CSTDEP" Then
                PrftCost = "COST"
            ElseIf Type = "CSTC" Or Type = "CSTDEPC" Then
                PrftCost = "CCOST"
            End If

            URL = "../../../Charts/MoldE1Charts/CPrftCost.aspx?CaseId=" + CaseId + "&PrftCost=" + objCryptoHelper.Encrypt(PrftCost) + "&ChartType=" + ChType + "&IsDep=" + objCryptoHelper.Encrypt(IsDep) + ""


            If flag = True Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), Type, "OpenNewWindow('" + URL + "','" + Title + "');", True)
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('These cases are not comparable because Case " + Session("MoldE1CaseId").ToString() + " is in " + Units1 + " and Case " + ddlPCases.SelectedValue + " is in " + Units2 + ".');", True)
            End If
            '

        Catch ex As Exception
            _lErrorLble.Text = "Error:btnChartType_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub


End Class
