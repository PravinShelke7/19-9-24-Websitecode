Imports System.Data
Imports System.Data.OleDb
Partial Class Pages_SavvyPackPro_Charts_Default
    Inherits System.Web.UI.Page
    Dim objGetdata As New SavvyProGetData
    Dim objUpIns As New SavvyProUpInsData
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load



    End Sub
    Protected Sub btnrefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            lblError.Text = Session("GraphVendors")

        Catch ex As Exception
            lblError.Text = "Error:btnLoad_Click:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub btnPCase_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPCase.Click
        Dim repText As String = String.Empty
        Try
            Session("CRptId") = hidCRptId.Value.ToString()

            If hidCRptId.Value = "" Or hidCRptId.Value = "0" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinB", "alert('Please select Proprietary Report');", True)
                lnkCReports.Text = "Display Proprietary Report List"
            Else
                Session("CaseType") = "Prop"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "OpenNewWindow('ChartReport.aspx?type=" + hidtype.Value + "');", True)
            End If

            ' lnkCReports.Text = hidCRptDes.Value
            '  lnkCompGrp.Text = hidChartGrpDes.Value
            'lnkAllGrps.Text = hidGroupReportD.Value
        Catch ex As Exception
            lblError.Text = "Error:btnPCase_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
    Protected Sub btRename_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btRename.Click
        ' Dim objGetData As New GetData()
        Dim ds As New DataSet
        Dim repText As String = String.Empty
        Try
            Session("CRptId") = hidCRptId.Value.ToString()

            If hidCRptId.Value = "0" Or hidCRptId.Value = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "alert('Please select Proprietary Report');", True)
            Else
                divRename.Visible = True
                If hidtype.Value = "USER" Then
                    ds = objGetdata.GetUserChartByID(Session("CRptId"))
                    If ds.Tables(0).Rows.Count > 0 Then
                        txtRptName.Text = ds.Tables(0).Rows(0).Item("REPORTNAME").ToString()
                    End If
                Else
                    ds = objGetdata.GetBatchChartnmByID(Session("CRptId"))
                    If ds.Tables(0).Rows.Count > 0 Then
                        txtRptName.Text = ds.Tables(0).Rows(0).Item("CHARTNAME").ToString()
                    End If
                    lnkCReports.Text = hidCRptDes.Value
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error:btnPCase_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        ' Dim objUpIns As New UpInsData()
        Try
            If hidtype.Value = "USER" Then
                objUpIns.updatechartnm(txtRptName.Text.Trim.ToString(), hidCRptId.Value)

            Else
                objUpIns.updateBatchchartnm(txtRptName.Text.Trim.ToString(), hidCRptId.Value)

            End If
            lnkCReports.Text = "Select Chart Report List"
            hidCRptDes.Value = "Select Chart Report List"
            hidCRptId.Value = "0"
            divRename.Visible = False
        Catch ex As Exception
            lblError.Text = "Error:btnUpdate_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
    Protected Sub btnRenameC_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            lnkCReports.Text = "Select Chart Report List"
            hidCRptDes.Value = "Select Chart Report List"
            hidCRptId.Value = "0"

            divRename.Visible = False
        Catch ex As Exception

        End Try
    End Sub
End Class
