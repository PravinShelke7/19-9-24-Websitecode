Imports System.Data
Imports System.Data.OleDb
Imports System
Imports SMed2GetData
Imports SMed2UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_MedSustain2_PopUp_CaseViewer
    Inherits System.Web.UI.Page
    Dim _iCaseId As Integer
    Dim _strCaseDe1 As String
    Dim _strCaseDe2 As String

    Public Property CaseId() As Integer
        Get
            Return _iCaseId
        End Get
        Set(ByVal Value As Integer)
            _iCaseId = Value
        End Set
    End Property

    Public Property CaseDe1() As String
        Get
            Return _strCaseDe1
        End Get
        Set(ByVal Value As String)
            _strCaseDe1 = Value
        End Set
    End Property

    Public Property CaseDe2() As String
        Get
            Return _strCaseDe2
        End Get
        Set(ByVal Value As String)
            _strCaseDe2 = Value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim CaseType As String = String.Empty
        CaseType = Request.QueryString("CTYPE")
        CaseDe1 = Request.QueryString("CASEDE1").Replace("'", "''")
        CaseDe2 = Request.QueryString("CASEDE2").Replace("'", "''")
        Try
            If Not IsPostBack Then
                If CaseType = "P" Then
                    GetPCaseDetails()
                ElseIf CaseType = "B" Then
                    GetBCaseDetails()
                End If
                CaseId = ddlCases.SelectedValue
                GetCaseDetails()
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetBCaseDetails()
        Dim objGetData As New SMed2GetData.Selectdata()
        Dim ds As New DataSet()
        Try
            ds = objGetData.GetBCases(CaseDe1, CaseDe2)
            With ddlCases
                .DataSource = ds
                .DataTextField = "CASEDE"
                .DataValueField = "CASEID"
                .DataBind()
                .Font.Size = 8
            End With
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetBCaseDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetPCaseDetails()
        Dim objGetData As New SMed2GetData.Selectdata()
        Dim ds As New DataSet()

        Try
            If Session("SMed2LicAdmin") = "N" Then
                If Request.QueryString("GrpID") <> "0" Then
                    ds = objGetData.GetGroupCases(Session("USERID"), CaseDe1, CaseDe2, Request.QueryString("GrpID").ToString())
                Else
                    ds = objGetData.GetCases(Session("USERID"), CaseDe1, CaseDe2)
                End If
            Else
                ds = objGetData.GetCasesByLicense(Session("USERID"), CaseDe1, CaseDe2)
            End If
            With ddlCases
                .DataSource = ds
                .DataTextField = "CASEDE"
                .DataValueField = "CASEID"
                .DataBind()
                .Font.Size = 8
            End With
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPCaseDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetCaseDetails()
        Dim objGetData As New SMed2GetData.Selectdata()
        Dim ds As New DataSet
        Dim dsNotes As New DataSet
        Dim CaseDes As String = String.Empty
        Try
            ds = objGetData.GetCaseDetails(CaseId.ToString())
            lblCaseID.Text = CaseId.ToString()
            lblCaseType.Text = ds.Tables(0).Rows(0).Item("CASETYPE").ToString()
            lblCaseDe2.Text = ds.Tables(0).Rows(0).Item("CASEDES").ToString()

            If ds.Tables(0).Rows(0).Item("CASEDE3").ToString().Length > 0 Then
                CaseDes = ds.Tables(0).Rows(0).Item("CASEDE3").ToString()
                caseDe3.Attributes.Add("onmouseover", "Tip('" + CaseDes + "')")
                caseDe3.Attributes.Add("onmouseout", "UnTip()")
            End If
            lblCaseDe3.Text = CaseDes
            lblCaseDes.Text = ds.Tables(0).Rows(0).Item("CASETYPE").ToString()
            GetPageDetails()


        Catch ex As Exception
            _lErrorLble.Text = "Error:GetCaseDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetPageDetails()
        Dim ds As New DataSet
        Dim dsNotes As New DataSet
        Dim objGetData As New SMed2GetData.Selectdata()
        Dim i As New Integer
        Dim Dept As String = String.Empty
        Try
            ds = objGetData.GetCaseViewer(CaseId)
            dsNotes = objGetData.GetCaseNoteDetails(CaseId)
            For i = 1 To 10
                If CInt(ds.Tables(0).Rows(0).Item("M" + i.ToString() + "")) <> 0 Then
                    Dept = Dept + ds.Tables(0).Rows(0).Item("DEPT" + i.ToString() + "").ToString() + "<br/>"
                End If
            Next
            divDept.InnerHtml = Dept
            lblEnergy.Text = FormatNumber(ds.Tables(0).Rows(0).Item("TOTERGY").ToString(), 0)
            lblEnergyPref.Text = "MJ"

            If dsNotes.Tables(0).Rows.Count > 0 Then
                With grdCaseNotes
                    .DataSource = dsNotes
                    .DataBind()
                End With
                trNotes.Visible = False
            Else
                trNotes.Visible = True
            End If
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

    Protected Sub btnPrivious_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrivious.Click
        Try
            btnNext.Enabled = True
            If ddlCases.SelectedIndex = 0 Then
                btnPrivious.Enabled = False
            End If


            CaseId = CInt(ddlCases.Items(ddlCases.SelectedIndex - 1).Value)
            GetCaseDetails()
            ddlCases.SelectedValue = CaseId
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Try
            btnPrivious.Enabled = True
            If ddlCases.SelectedIndex = ddlCases.Items.Count - 1 Then
                btnNext.Enabled = False
            End If
            CaseId = CInt(ddlCases.Items(ddlCases.SelectedIndex + 1).Value)
            GetCaseDetails()
            ddlCases.SelectedValue = CaseId
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub ddlCases_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCases.SelectedIndexChanged
        Try
            CaseId = CInt(ddlCases.SelectedValue)
            GetCaseDetails()
            ddlCases.SelectedValue = CaseId
        Catch ex As Exception

        End Try
    End Sub
End Class
