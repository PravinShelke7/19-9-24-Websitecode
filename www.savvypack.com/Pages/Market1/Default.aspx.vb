Imports System.Data
Imports System.Data.OleDb
Imports System
Imports M1GetData
Imports M1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Market1_Default
    Inherits System.Web.UI.Page

#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_PAGES_MARKET_DEFAULT")
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        objRefresh.Render(Page)
    End Sub

#End Region

    Protected Sub SetSessions()
        Dim objGetData As New Selectdata()
        Dim ds As New DataSet()
        Try
            ds = objGetData.GetUserDetails(Session("ID"))
            Session("UserId") = ds.Tables(0).Rows(0).Item("USERID").ToString()
            Session("M1UserRole") = ds.Tables(0).Rows(0).Item("USERROLE").ToString()
            Session("M1ServiceRole") = ds.Tables(0).Rows(0).Item("SERVIECROLE").ToString()
            Session("M1UserName") = ds.Tables(0).Rows(0).Item("USERNAME").ToString()
            Session("M1ToolUserName") = ds.Tables(0).Rows(0).Item("TOOLUSERNAME").ToString()
            Session("M1MaxCaseCount") = ds.Tables(0).Rows(0).Item("MAXCASECOUNT").ToString()
        Catch ex As Exception
            lblError.Text = "Error:SetSessions:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Try
                lblTag.Text = "Copyright 1997 - " + Now.Year().ToString() + " SavvyPack Corporation"
            Catch ex As Exception
            End Try
            If Session("Back") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If

            If Not IsPostBack Then
                hidPropRpt.Value = "0"
                hidBaseRpt.Value = "0"
                SetSessions()
                'GetBReportsDetails()
                GetPReportsDetails()
                ' GetReports()
            End If
            SetToolButton()
        Catch ex As Exception
            lblError.Text = "Error:Page_Load:" + ex.Message.ToString() + ""
        End Try
    End Sub
    Protected Sub GetReports()
        Dim Link As New HyperLink
        Dim hid As New HiddenField
        Dim Path As String = ""
        Try
            Link = New HyperLink
            hid = New HiddenField
            Link.ID = "PropRpt"
            hid.ID = "hidPropRpt"
            Link.Text = "Select Proprietary Report"
            Link.Width = 300
            Link.Style.Add("Font-Size", "14px")
            Link.CssClass = "Link"
            hid.Value = "0"
            Path = "../PopUp/GetMatPopUp.aspx?Des="
            Link.NavigateUrl = "javascript:ShowPopWindow('" + Path + "')"

            tdPropCases.Controls.Add(hid)
            tdPropCases.Controls.Add(Link)
        Catch ex As Exception

        End Try
    End Sub
    

    Protected Sub GetPReportsDetails()
        Dim objGetData As New Selectdata()
        Dim ds As New DataSet()
        Try
            ds = objGetData.GetUserCustomReports(Session("UserId"))
            'With ddlPCase
            '    .DataSource = ds
            '    .DataTextField = "REPORTDES"
            '    .DataValueField = "USERREPORTID"
            '    .DataBind()
            '    .Font.Size = 8
            'End With
            If ds.Tables(0).Rows.Count <= 0 Then
                btnPCase.Visible = False
                btRename.Visible = False
                lblPCase.Visible = True
            Else
                btnPCase.Visible = True
                btRename.Visible = True
                lblPCase.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = "Error:GetPCaseDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub SetToolButton()
        Try
            If Session("M1ServiceRole") <> "ReadWrite" Then
                btnToolBox.Enabled = False
            End If
        Catch ex As Exception
            lblError.Text = "Error:SetToolButton:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnPCase_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPCase.Click
        Dim repText As String = String.Empty
        Try
            Session("M1RptId") = hidPropRpt.value.ToString()
            If Not objRefresh.IsRefresh Then
                If hidPropRpt.Value = "0" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "alert('Please select Propriertary Report');", True)
                Else
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "OpenNewWindow('CAGR/CagrReport.aspx?Type=Prop');", True)
                End If
            End If
            lnkReports.Text = hidPropRptDes.value
        Catch ex As Exception
            lblError.Text = "Error:btnPCase_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnToolBox_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnToolBox.Click
        Try
            Response.Redirect("Tools/Tool.aspx", True)
        Catch ex As Exception
            lblError.Text = "Error:btnToolBox_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Dim objUpIns As New UpdateInsert()
        Try
            objUpIns.RenameReport(hypReportId.Value, txtRptName.Text.Trim.ToString())
            GetPReportsDetails()
        Catch ex As Exception
            lblError.Text = "Error:btnUpdate_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnBCase_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBCase.Click
        Dim repText As String = String.Empty
        Try
            Session("M1RptId") = hidBaseRpt.Value.ToString()
            If Not objRefresh.IsRefresh Then
                If hidBaseRpt.Value = "0" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "alert('Please select Base Report');", True)
                Else
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "OpenNewWindow('CAGR/CagrReport.aspx?Type=Base');", True)
                End If
            End If
            lnkBReports.Text = hidBaseRptDes.Value
        Catch ex As Exception
            lblError.Text = "Error:btnPCase_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btRename_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btRename.Click
        Dim objGetData As New Selectdata()
        Dim ds As New DataSet()
        Dim repText As String = String.Empty
        Try
            Session("M1RptId") = hidPropRpt.Value.ToString()
            If Not objRefresh.IsRefresh Then
                If hidPropRpt.Value = "0" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "alert('Please select Proprietary Report');", True)
                Else
                    divRename.Visible = True
                    ds = objGetData.GetUserCustomReportsByRptId(Session("M1RptId"))
                    If ds.Tables(0).Rows.Count > 0 Then
                        txtRptName.Text = ds.Tables(0).Rows(0).Item("REPORTNAME").ToString()
                    End If
                End If
            End If
            lnkReports.Text = hidPropRptDes.Value
        Catch ex As Exception
            lblError.Text = "Error:btnPCase_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnRename_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Dim objUpdateData As New UpdateInsert()
        Try
            objUpdateData.RenameReport(Session("M1RptId"), txtRptName.Text)
            hidBaseRpt.Value = "0"
            hidPropRpt.Value = "0"
            lnkBReports.Text = "Select Base Report"
            lnkReports.Text = "Select Proprietary Report"
            divRename.Visible = False
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnRenameC_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            divRename.Visible = False
        Catch ex As Exception

        End Try
    End Sub
End Class
