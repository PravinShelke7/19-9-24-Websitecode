Imports System.Data
Imports System.Data.OleDb
Imports System
Imports M1GetData
Imports M1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Market1_PopUp_ColSelector
    Inherits System.Web.UI.Page

    Protected Sub btnSumitt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSumitt.Click
        Dim RptID As String = Request.QueryString("RptID").ToString()
        Dim Seq As String = Request.QueryString("Seq").ToString()
        Dim ColType As String = String.Empty
        Dim ColVal As String = String.Empty
        Dim UDFV1 As String = String.Empty
        Dim UDFV2 As String = String.Empty
        Dim CAGRYearId1 As String = String.Empty
        Dim CAGRYearId2 As String = String.Empty
        Dim objUpIns As New UpdateInsert()
        Dim Value As String = String.Empty
        Dim hidValue As Integer = CInt(Request.QueryString("hidValue"))
        Dim hidId As String = Request.QueryString("hidId")
        hidColDes.Value = Request.QueryString("Id").ToString()
        hidColId.Value = Request.QueryString("hidId").ToString()
        Dim colValueId As String = String.Empty
        Try

            If ddlColType.SelectedItem.Text = "Year" Then
                ColType = "Year"
                ColVal = ddlYear.SelectedItem.Text.ToString()
                UDFV1 = ""
                UDFV2 = ""
                CAGRYearId1 = ""
                CAGRYearId2 = ""
                Value = ColVal
                colValueId = ddlYear.SelectedValue
            ElseIf ddlColType.SelectedItem.Text = "CAGR" Then
                ColType = "Formula"
                ColVal = "CAGR"
                UDFV1 = ddlBYear.SelectedItem.ToString()
                UDFV2 = ddlEYear.SelectedItem.ToString()
                Value = ColVal + "(" + UDFV2 + "/" + UDFV1 + ")"
                colValueId = "''"
                CAGRYearId1 = ddlBYear.SelectedValue.ToString()
                CAGRYearId2 = ddlEYear.SelectedValue.ToString()
            End If
            If hidValue = 0 Then
                hidValue = objUpIns.AddColDetails(RptID, Seq, ColType, ColVal, UDFV1, UDFV2, ddlColType.SelectedValue, colValueId)
            Else
                objUpIns.UpdateColDetails(Seq, ColType, ColVal, UDFV1, UDFV2, hidValue, ddlColType.SelectedValue, colValueId, CAGRYearId1, CAGRYearId2, RptID)
            End If
            ClientScript.RegisterStartupScript(Me.GetType(), "ColSel", "ColSelection('" + Value.ToString() + "','" + hidValue.ToString() + "','" + hidId + "'," + Seq + ");", True)

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GetAndFillYears(ByVal ddl As DropDownList)
        Dim objGetDate As New Selectdata()
        Dim ds As New DataSet()
        Try
            'ds = objGetDate.GetYears()
            ds = objGetDate.GetColSQL(ddlColType.SelectedValue.ToString())
            With ddl
                .DataSource = ds
                .DataTextField = "VALUE"
                .DataValueField = "ID"
                .DataBind()
            End With


        Catch ex As Exception
            _lErrorLble.Text = "Error:GetAndFillYears" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub ddlColType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlColType.SelectedIndexChanged
        Try
            ReportType()
        Catch ex As Exception
            _lErrorLble.Text = "Error:ddlColType_SelectedIndexChanged" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ' ReportType()
            getColSelector()
        End If
    End Sub
    Protected Sub getColSelector()
        Dim objGetDate As New Selectdata()
        Dim ds As New DataSet()
        Dim lst As New ListItem
        Dim RptType As String = String.Empty
        Try

            ds = objGetDate.GetColsSelector(-1)
            lst.Text = "--Select--"
            lst.Value = "0"
            lst.Attributes.Add("COLTYPEID", "-1")
            lst.Attributes.Add("COLDES", "SELECT")

            With ddlColType
                .Items.Add(lst)
                .SelectedItem.Value = "0"
                .AppendDataBoundItems = True
                .DataSource = ds
                .DataTextField = "COLDES"
                .DataValueField = "COLTYPEID"
                .DataBind()
            End With
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetCOlSelector" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub ReportType()
        If ddlColType.SelectedItem.Text = "Year" Then
            divYear.Visible = True
            divCAGR.Visible = False
            GetAndFillYears(ddlYear)
        ElseIf ddlColType.SelectedItem.Text = "CAGR" Then
            divYear.Visible = False
            divCAGR.Visible = True
            GetAndFillByColumns(ddlBYear)
            GetAndFillByColumns(ddlEYear)
        End If
    End Sub

    Protected Sub GetAndFillByColumns(ByVal ddl As DropDownList)
        Dim objGetDate As New Selectdata()
        Dim ds As New DataSet()
        Dim strCluase As String = String.Empty
        Try
            strCluase = " AND CLUASE1=" + Request.QueryString("RptID").ToString() + " AND CLUASE2<" + Request.QueryString("Seq").ToString()
            ds = objGetDate.GetColSQLWithClause(ddlColType.SelectedValue.ToString(), strCluase)
            With ddl
                .DataSource = ds
                .DataTextField = "VALUE"
                .DataValueField = "ID"
                .DataBind()
            End With


        Catch ex As Exception
            _lErrorLble.Text = "Error:GetAndFillYears" + ex.Message.ToString()
        End Try
    End Sub
End Class
