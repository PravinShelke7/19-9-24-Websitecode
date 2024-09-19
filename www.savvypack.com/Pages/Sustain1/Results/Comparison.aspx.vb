Imports System.Data
Imports System.Data.OleDb
Imports System
Imports S1GetData
Imports S1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Sustain1_Results_Comparison
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
            'If Session("Back") = Nothing Then
            'Dim obj As New CryptoHelper
            'Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            'End If
            GetSessionDetails()
            If Session("Service") = "COMPS1" Then
                S1Table.Attributes.Add("class", "S1CompModule")
            End If
            Type = Convert.ToString(Request.QueryString("Type"))
            If Type.Length > 0 Then
                Dim objCryptoHelper As New CryptoHelper()
                Type = objCryptoHelper.Decrypt(Type)
            End If
            If Type = "ERGY" Then
                lblTitle.Text = "Sustain1 - Energy Comparison Selector"
                Page.Title = "S1-Energy Comparison Selector"
            ElseIf Type = "CERGY" Then
                lblTitle.Text = "Sustain1 - Customer Energy Comparison Selector"
                Page.Title = "S1-Customer Customer Energy Comparison Selector"
            ElseIf Type = "GHG" Then
                lblTitle.Text = "Sustain1 - GHG Comparison Selector"
                Page.Title = "S1-GHG Comparison Selector"
            ElseIf Type = "CGHG" Then
                lblTitle.Text = "Sustain1 - Customer GHG Comparison Selector"
                Page.Title = "S1-Customer GHG Comparison Selector"
            ElseIf Type = "CGHG" Then
                lblTitle.Text = "Sustain1 - Customer GHG Comparison Selector"
                Page.Title = "S1-Customer GHG Comparison Selector"
            ElseIf Type = "WATER" Then
                lblTitle.Text = "Sustain1 - Water Comparison Selector"
                Page.Title = "S1-Water Comparison Selector"
            ElseIf Type = "CWATER" Then
                lblTitle.Text = "Sustain1 - Water Comparison Selector"
                Page.Title = "S1-Water Comparison Selector"
            ElseIf Type = "CEOL" Then
                lblTitle.Text = "Sustain1 - Customer End Of Life Comparison Selector"
                Page.Title = "S1-Customer End Of Life Comparison Selector"
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
            lblSessionCaseId.Text = Session("S1CaseId")

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetSessionDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetPCaseDetails()
        Dim objGetData As New S1GetData.Selectdata()
        Dim ds As New DataSet()
        Try
            'If Session("S1LicAdmin") = "N" Then
            '    If Session("Service") = "COMPS1" Then
            '        ds = objGetData.GetPCaseCompDetails(Session("USERID").ToString(), Session("S1ServiceId").ToString())
            '    Else
            '        ds = objGetData.GetPCaseDetails(Session("USERID").ToString())
            '    End If

            'Else
            '    If Session("Service") = "COMPS1" Then
            '        ds = objGetData.GetCompPCaseDetailsByLicense(Session("USERID").ToString(), Session("S1ServiceId").ToString())
            '    Else
            '        ds = objGetData.GetPCaseDetailsByLicense(Session("USERID").ToString())
            '    End If

            'End If
            'AdminLicense
            If Session("Service") = "COMPS1" Then
                ds = objGetData.GetPCaseCompDetails(Session("USERID").ToString(), Session("S1ServiceId").ToString())
            Else
                ds = objGetData.GetPCaseDetails(Session("USERID").ToString())
            End If
            'AdminLicense
            With ddlPCases
                .DataSource = ds
                .DataTextField = "CASEDES"
                .DataValueField = "CASEID"
                .DataBind()
                .Font.Size = 8
            End With
            'AdminLicense

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
            ElseIf Type = "CERGY" Then
                For i = 0 To ddlType.Items.Count - 1
                    Item = ddlType.Items(i)
                    Item.Text = Item.Text.Replace("Energy", "Customer Energy")
                Next
            ElseIf Type = "CGHG" Then
                For i = 0 To ddlType.Items.Count - 1
                    Item = ddlType.Items(i)
                    Item.Text = Item.Text.Replace("Energy", "Customer GHG")
                Next
            ElseIf Type = "CWATER" Then
                For i = 0 To ddlType.Items.Count - 1
                    Item = ddlType.Items(i)
                    Item.Text = Item.Text.Replace("Energy", "Customer WATER")
                Next
            ElseIf Type = "WATER" Then
                For i = 0 To ddlType.Items.Count - 1
                    Item = ddlType.Items(i)
                    Item.Text = Item.Text.Replace("Energy", "Water")
                Next
            ElseIf Type = "CEOL" Then
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

        Dim formatId1 As String
        Dim formatId2 As String
        Dim flag As Boolean
        Dim Units1 As String = ""
        Dim Units2 As String = ""
        Dim objGetData As New S1GetData.Selectdata
        Dim ds As New DataSet
        Dim i As Integer
        Try

            'getting Product Format
            ds = objGetData.GetProductFormtByID(Session("S1CaseId"), ddlPCases.SelectedValue)
            If ds.Tables(0).Rows.Count = "1" Then
                flag = True
            Else
                formatId1 = ds.Tables(0).Rows(0).Item("M1").ToString()
                formatId2 = ds.Tables(0).Rows(1).Item("M1").ToString()
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    If ds.Tables(0).Rows(i).Item("CaseId") = Session("E1CaseId") Then
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
            TypeDes = objCryptoHelper.Encrypt(ddlType.SelectedValue).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!")
            Title = ddlType.SelectedValue
            CaseId = objCryptoHelper.Encrypt(ddlPCases.SelectedValue).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!")
            If Type = "ERGY" Then
                URL = "CErgyRes.aspx?Type=" + TypeDes + "&CaseId=" + CaseId + ""
            ElseIf Type = "CERGY" Then
                URL = "CErgyRes2.aspx?Type=" + TypeDes + "&CaseId=" + CaseId + ""
            ElseIf Type = "GHG" Then
                URL = "CGhgRes.aspx?Type=" + TypeDes + "&CaseId=" + CaseId + ""
            ElseIf Type = "CGHG" Then
                URL = "CGhgRes2.aspx?Type=" + TypeDes + "&CaseId=" + CaseId + ""
            ElseIf Type = "WATER" Then
                URL = "CWaterRes.aspx?Type=" + TypeDes + "&CaseId=" + CaseId + ""
            ElseIf Type = "CWATER" Then
                URL = "CWaterRes2.aspx?Type=" + TypeDes + "&CaseId=" + CaseId + ""
            ElseIf Type = "CEOL" Then
                URL = "CResultsEOL2.aspx?Type=" + TypeDes + "&CaseId=" + CaseId + ""
            End If

            If flag = True Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), Type, "OpenNewWindow('" + URL + "','" + Title + "');", True)
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('These cases are not comparable because Case " + Session("S1CaseId").ToString() + " is in " + Units1 + " and Case " + ddlPCases.SelectedValue + " is in " + Units2 + ".');", True)

            End If

        Catch ex As Exception
            _lErrorLble.Text = "Error:btnType_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnChartType_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChartType.Click
        Dim TypeDes As String = String.Empty
        Dim objCryptoHelper As New CryptoHelper()
        Dim CaseId As String = String.Empty
        Dim URL As String = String.Empty
        Dim Title As String = String.Empty

        Dim formatId1 As String
        Dim formatId2 As String
        Dim flag As Boolean
        Dim Units1 As String = ""
        Dim Units2 As String = ""
        Dim objGetData As New S1GetData.Selectdata
        Dim ds As New DataSet
        Dim i As Integer
        Try
            'getting Product Format
            ds = objGetData.GetProductFormtByID(Session("S1CaseId"), ddlPCases.SelectedValue)
            If ds.Tables(0).Rows.Count = "1" Then
                flag = True
            Else
                formatId1 = ds.Tables(0).Rows(0).Item("M1").ToString()
                formatId2 = ds.Tables(0).Rows(1).Item("M1").ToString()
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    If ds.Tables(0).Rows(i).Item("CaseId") = Session("E1CaseId") Then
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

            TypeDes = ddlChrtType.SelectedValue.ToString()
            CaseId = objCryptoHelper.Encrypt(ddlPCases.SelectedValue).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!")

            URL = "../../../Charts/S1Charts/CErgyGhgWat.aspx?CaseId=" + CaseId + "&ErgyGHG=" + objCryptoHelper.Encrypt(Type) + "&ChartType=" + objCryptoHelper.Encrypt(TypeDes) + ""

            If flag = True Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), Type, "OpenNewWindow('" + URL + "','" + TypeDes + "');", True)
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('These cases are not comparable because Case " + Session("S1CaseId").ToString() + " is in " + Units1 + " and Case " + ddlPCases.SelectedValue + " is in " + Units2 + ".');", True)

            End If

        Catch ex As Exception
            _lErrorLble.Text = "Error:btnType_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
End Class
