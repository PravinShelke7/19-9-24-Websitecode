Imports System.Data
Imports System.Data.OleDb
Imports System
Imports M1SubGetData
Imports M1SubUpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Market1Sub_PopUp_GetAllGroups
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            hidgrpDes.Value = Request.QueryString("Des").ToString()
            hidgrpId.Value = Request.QueryString("Id").ToString()
            hidGrpIdD.Value = Request.QueryString("IdD").ToString()
            If Not IsPostBack Then
                GetPReportGroupDetails()
                hvUserGrd.Value = "0"
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub


    Protected Sub GetPReportGroupDetails()
        Dim Dts As New DataSet
        Dim objGetData As New M1SubGetData.Selectdata
        Dim lst As New ListItem
        Try
            If Request.QueryString("ID").ToString() = "hidGrpId" Then
                Dts = objGetData.GetGroupIDByService(Session("USERID"), Session("M1ServiceID"), txtDes1.Text.Trim.ToString().Replace("'", "''"))
                If Dts.Tables(0).Rows.Count > 0 Then
                    Session("UsersAllGroups") = Dts
                    grdGroupSearch.DataSource = Dts
                    grdGroupSearch.DataBind()
                Else
                    Session("UsersAllGroups") = Dts
                    grdGroupSearch.DataSource = Dts
                    grdGroupSearch.DataBind()
                End If
            End If

            If Dts.Tables(0).Rows.Count <= 1 Then
                lblGGroup.Visible = True
                btnSearch.Enabled = False
            Else
                btnSearch.Enabled = True
                lblGGroup.Visible = False
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetAllGroups:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            GetGroupDetails()
        Catch ex As Exception
            _lErrorLble.Text = "Error:btnSearch_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetGroupDetails()
        Dim Dts As New DataSet
        Dim objGetData As New M1SubGetData.Selectdata
        Dim lst As New ListItem
        Try
            If Request.QueryString("ID").ToString() = "hidGrpId" Then
                Dts = objGetData.GetGroupIDByService(Session("USERID"), Session("M1ServiceID"), txtDes1.Text.Trim.ToString().Replace("'", "''"))
                If Dts.Tables(0).Rows.Count > 0 Then
                    Session("UsersAllGroups") = Dts
                    grdGroupSearch.DataSource = Dts
                    grdGroupSearch.DataBind()
                Else
                    Session("UsersAllGroups") = Dts
                    grdGroupSearch.DataSource = Dts
                    grdGroupSearch.DataBind()
                End If
            End If

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetGroupDetails:" + ex.Message.ToString()
        End Try
    End Sub

 Protected Sub grdGroupSearch_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdGroupSearch.Sorting
        Try
            Dim Dts As New DataSet
            Dim dv As DataView
            Dim ds As New DataSet
            Dim numberDiv As Integer
            numberDiv = Convert.ToInt16(hvUserGrd.Value.ToString())
            Dts = Session("UsersAllGroups")
            dv = Dts.Tables(0).DefaultView

            If ((numberDiv Mod 2) = 0) Then
                dv.Sort = e.SortExpression + " " + "DESC"
            Else
                dv.Sort = e.SortExpression + " " + "ASC"
            End If
            numberDiv += 1
            ds.Tables.Add(dv.ToTable())
            Session("UsersReportGroup") = ds
            hvUserGrd.Value = numberDiv.ToString()
            grdGroupSearch.DataSource = dv
            grdGroupSearch.DataBind()

        Catch ex As Exception

        End Try
    End Sub
End Class
