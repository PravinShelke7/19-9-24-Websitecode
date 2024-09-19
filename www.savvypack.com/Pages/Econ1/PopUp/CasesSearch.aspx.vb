Imports System.Data
Imports System.Data.OleDb
Imports System
Imports E1GetData
Imports E1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Econ1_PopUp_CasesSearch
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            hidCaseid.Value = Request.QueryString("ID").ToString()
            hidCaseDes.Value = Request.QueryString("Des").ToString()
            hidCaseidD.Value = Request.QueryString("IdD").ToString()
            If Not IsPostBack Then
                hidsortCase.Value = "0"
                hidsortCase1.Value = "0"

                GetCaseDetails()
                GetCaseDetails1()

                If hidCaseid.Value = "hidPropCase" Then
                    btnCaseViewer.Attributes.Add("onclick", "return CaseViewer('" + Request.QueryString("GrpID").ToString() + "')")
                Else
                    btnCaseViewer.Attributes.Add("onclick", "return CaseViewer('0')")
                End If

            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub tabCase_ActiveTabChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabCase.ActiveTabChanged
        Try
            If tabCase.ActiveTabIndex = "0" Then
                GetCaseDetails()
            Else
                GetCaseDetails1()
            End If
        Catch ex As Exception
            Response.Write("Error:tabCase_ActiveTabChanged:" + ex.Message.ToString())
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            GetCaseDetails()
            GetCaseDetails1()

        Catch ex As Exception
            _lErrorLble.Text = "Error:btnSearch_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetCaseDetails()
        Dim ds As New DataSet
        Dim objGetData As New E1GetData.Selectdata()
        Try
            If hidCaseid.Value = "hidPropCase" Or hidCaseid.Value = "hidTargetApp" Or hidCaseid.Value = "hidTargetProp" Then
                If Session("E1LicAdmin") = "N" Then
                    If Request.QueryString("GrpID") <> "0" Then
                        ds = objGetData.GetGroupPCases(Session("USERID"), txtKeyWord.Text.Trim.ToString().Replace("'", "''"), Request.QueryString("GrpID").ToString())
                    Else
                        ds = objGetData.GetPropCases(Session("USERID"), txtKeyWord.Text.Trim.ToString().Replace("'", "''"))
                    End If

                Else
                    If Request.QueryString("GrpID") <> "0" Then
                        ds = objGetData.GetGroupPCases(Session("USERID"), txtKeyWord.Text.Trim.ToString().Replace("'", "''"), Request.QueryString("GrpID").ToString())
                    Else
                        ds = objGetData.GetPropCasesByLicense(Session("USERID"), txtKeyWord.Text.Trim.ToString().Replace("'", "''"))
                    End If
                End If
            Else
                If Request.QueryString("GrpID") <> "0" Then
                    ds = objGetData.GetGroupPCases(Session("USERID"), txtKeyWord.Text.Trim.ToString().Replace("'", "''"), Request.QueryString("GrpID").ToString())
                Else
                    If Session("E1LicAdmin") = "N" Then
                        ds = objGetData.GetApprovedCases(Session("USERID"), txtKeyWord.Text.Trim.ToString().Replace("'", "''"))
                    Else
                        ds = objGetData.GetAppCasesByLicense(Session("USERID"), txtKeyWord.Text.Trim.ToString().Replace("'", "''"))
                    End If
                End If
            End If
            Session("DsCaseData") = ds
            grdCaseSearch.DataSource = ds
            grdCaseSearch.DataBind()
            If ds.Tables(0).Rows.Count > 0 Then
                lblRes.Text = ""

            Else
                lblRes.Text = "No record found."
            End If
            If ds.Tables(0).Rows.Count > 1 Then
                btnCaseViewer.Visible = True
            Else
                btnCaseViewer.Visible = False
            End If

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetCaseDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub grdCaseSearch_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdCaseSearch.Sorting
        Dim Dts As New DataSet()
        Dim dv As DataView
        Dim numberDiv As Integer
        Dim dsSorted As New DataSet()
        'Try
        '    numberDiv = Convert.ToInt16(hidsortCase.Value.ToString())
        '    Dts = Session("DsCaseData")
        '    dv = Dts.Tables(0).DefaultView

        '    If ((numberDiv Mod 2) = 0) Then
        '        dv.Sort = e.SortExpression + " " + "DESC"
        '    Else
        '        dv.Sort = e.SortExpression + " " + "ASC"
        '    End If
        '    numberDiv += 1
        '    hidsortCase.Value = numberDiv.ToString()
        '    grdCaseSearch.DataSource = dv
        '    grdCaseSearch.DataBind()

        '    dsSorted.Tables.Add(dv.ToTable())
        '    Session("DsCaseData") = dsSorted

        'Catch ex As Exception
        '    Response.Write("Error:grdCaseSearch_Sorting:" + ex.Message.ToString())
        'End Try

        Dim Dts1 As New DataSet()
        Dim dv1 As DataView
        Dim dsSorted1 As New DataSet()
        Try
            numberDiv = Convert.ToInt16(hidsortCase.Value.ToString())
            Dts = Session("DsCaseData")
            dv = Dts.Tables(0).DefaultView

            Dts1 = Session("DsCaseData1")
            dv1 = Dts1.Tables(0).DefaultView

            If ((numberDiv Mod 2) = 0) Then
                dv.Sort = e.SortExpression + " " + "DESC"
                dv1.Sort = e.SortExpression + " " + "DESC"
            Else
                dv.Sort = e.SortExpression + " " + "ASC"
                dv1.Sort = e.SortExpression + " " + "ASC"
            End If
            numberDiv += 1
            hidsortCase.Value = numberDiv.ToString()
            grdCaseSearch.DataSource = dv
            grdCaseSearch.DataBind()

            grdCaseSearch1.DataSource = dv1
            grdCaseSearch1.DataBind()

            dsSorted.Tables.Add(dv.ToTable())
            Session("DsCaseData") = dsSorted

            dsSorted1.Tables.Add(dv1.ToTable())
            Session("DsCaseData1") = dsSorted1

        Catch ex As Exception
            Response.Write("Error:grdCaseSearch_Sorting:" + ex.Message.ToString())
        End Try

    End Sub
#Region "new grid view"
    Protected Sub GetCaseDetails1()
        Dim ds As New DataSet
        Dim objGetData As New E1GetData.Selectdata()
        Try
            If hidCaseid.Value = "hidPropCase" Or hidCaseid.Value = "hidTargetApp" Or hidCaseid.Value = "hidTargetProp" Then
                If Session("E1LicAdmin") = "N" Then
                    If Request.QueryString("GrpID") <> "0" Then
                        ds = objGetData.GetGroupPCases1(Session("USERID"), txtKeyWord.Text.Trim.ToString().Replace("'", "''"), Request.QueryString("GrpID").ToString())
                    Else
                        ds = objGetData.GetPropCases1(Session("USERID"), txtKeyWord.Text.Trim.ToString().Replace("'", "''"))
                    End If

                Else
                    'ds = objGetData.GetPropCasesByLicense1(Session("USERID"), txtKeyWord.Text.Trim.ToString().Replace("'", "''"))
                    If Request.QueryString("GrpID") <> "0" Then
                        ds = objGetData.GetGroupPCases1(Session("USERID"), txtKeyWord.Text.Trim.ToString().Replace("'", "''"), Request.QueryString("GrpID").ToString())
                    Else
                        ds = objGetData.GetPropCasesByLicense1(Session("USERID"), txtKeyWord.Text.Trim.ToString().Replace("'", "''"))
                    End If
                End If
            Else
                If Request.QueryString("GrpID") <> "0" Then
                    ds = objGetData.GetGroupPCases1(Session("USERID"), txtKeyWord.Text.Trim.ToString().Replace("'", "''"), Request.QueryString("GrpID").ToString())
                Else
                    If Session("E1LicAdmin") = "N" Then
                        ds = objGetData.GetApprovedCases1(Session("USERID"), txtKeyWord.Text.Trim.ToString().Replace("'", "''"))
                    Else
                        ds = objGetData.GetAppCasesByLicense1(Session("USERID"), txtKeyWord.Text.Trim.ToString().Replace("'", "''"))
                    End If
                End If
            End If
            Session("DsCaseData1") = ds
            grdCaseSearch1.DataSource = ds
            grdCaseSearch1.DataBind()
            ' BindGrid1()
            If ds.Tables(0).Rows.Count > 0 Then
                lblRes1.Text = ""

            Else
                lblRes1.Text = "No record found."
            End If
            If ds.Tables(0).Rows.Count > 1 Then
                btnCaseViewer.Visible = True
            Else
                btnCaseViewer.Visible = False
            End If

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetCaseDetails1:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub grdCaseSearch1_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdCaseSearch1.Sorting
        Dim Dts As New DataSet()
        Dim dv As DataView
        Dim numberDiv As Integer
        Dim dsSorted As New DataSet()
        'Try
        '    numberDiv = Convert.ToInt16(hidsortCase1.Value.ToString())
        '    Dts = Session("DsCaseData1")
        '    dv = Dts.Tables(0).DefaultView

        '    If ((numberDiv Mod 2) = 0) Then
        '        dv.Sort = e.SortExpression + " " + "DESC"
        '    Else
        '        dv.Sort = e.SortExpression + " " + "ASC"
        '    End If
        '    numberDiv += 1
        '    hidsortCase1.Value = numberDiv.ToString()
        '    grdCaseSearch1.DataSource = dv
        '    grdCaseSearch1.DataBind()

        '    dsSorted.Tables.Add(dv.ToTable())
        '    Session("DsCaseData1") = dsSorted

        'Catch ex As Exception
        '    Response.Write("Error:grdCaseSearch1_Sorting:" + ex.Message.ToString())
        'End Try

        Dim Dts1 As New DataSet()
        Dim dv1 As DataView
        Dim dsSorted1 As New DataSet()
        Try
            numberDiv = Convert.ToInt16(hidsortCase.Value.ToString())
            Dts = Session("DsCaseData")
            dv = Dts.Tables(0).DefaultView

            Dts1 = Session("DsCaseData1")
            dv1 = Dts1.Tables(0).DefaultView

            If ((numberDiv Mod 2) = 0) Then
                dv.Sort = e.SortExpression + " " + "DESC"
                dv1.Sort = e.SortExpression + " " + "DESC"
            Else
                dv.Sort = e.SortExpression + " " + "ASC"
                dv1.Sort = e.SortExpression + " " + "ASC"
            End If
            numberDiv += 1
            hidsortCase.Value = numberDiv.ToString()
            grdCaseSearch.DataSource = dv
            grdCaseSearch.DataBind()

            grdCaseSearch1.DataSource = dv1
            grdCaseSearch1.DataBind()

            dsSorted.Tables.Add(dv.ToTable())
            Session("DsCaseData") = dsSorted

            dsSorted1.Tables.Add(dv1.ToTable())
            Session("DsCaseData1") = dsSorted1

        Catch ex As Exception
            Response.Write("Error:grdCaseSearch1_Sorting:" + ex.Message.ToString())
        End Try

    End Sub
#End Region
End Class
