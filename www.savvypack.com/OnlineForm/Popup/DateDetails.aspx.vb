Imports System.Data
Imports System.Data.OleDb
Imports System
Imports SavvyGetData
Imports SavvyUpInsData
Imports AjaxControlToolkit

Partial Class SavvyPack_Popup_DateDetails
    Inherits System.Web.UI.Page
    Dim objUpIns As New UpdateInsert()


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            
            If Session("SBack") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If

            If Not IsPostBack Then
                ProjectDetails()
                'Started Activity Log Changes
                Try
                    objUpIns.InsertLog1(Session("UserId").ToString(), "DateDetails.aspx", "Opened MilestonePopup for ProjectId:" + hidProjectId.Value + "", "", Session("SPROJLogInCount").ToString())
                Catch ex As Exception

                End Try
                'Ended Activity Log Changes
            End If

        Catch ex As Exception

        End Try
    End Sub

    Public Sub ProjectDetails()
        Dim dsProjId As New DataSet
        Dim dsUser As New DataSet
        Dim dsDate As New DataSet
        Dim objGetData As New Selectdata()
        Dim tr As TableRow
        Dim td As TableCell
        Dim lbl As Label
        Dim txt As TextBox
        Dim calEx As CalendarExtender
        Dim hid As HiddenField
        Dim dsData As New DataSet
        Dim dvData As New DataView
        Dim dtData As New DataTable
        Try

            hidProjectId.Value = Request.QueryString("ProjectId").ToString()
            dsDate = objGetData.GetDateDetails()
            dsData = objGetData.GetExistingDateDetails(hidProjectId.Value)
            Session("dsData") = dsData
            dvData = dsData.Tables(0).DefaultView
            Session("count") = dsDate.Tables(0).Rows.Count - 1
            For i = 0 To dsDate.Tables(0).Rows.Count
                tr = New TableRow
                For j = 1 To 2
                    td = New TableCell
                    Select Case j
                        Case 1
                            If i = 0 Then

                            Else
                                lbl = New Label
                                hid = New HiddenField
                                hid.ID = "hidId" + i.ToString()
                                lbl.Text = dsDate.Tables(0).Rows(i - 1).Item("DATETYPENAME").ToString()
                                lbl.ToolTip = dsDate.Tables(0).Rows(i - 1).Item("TOOLTIPDS").ToString()
                                hid.Value = dsDate.Tables(0).Rows(i - 1).Item("DATETYPEID").ToString()
                                td.Controls.Add(lbl)
                                td.Controls.Add(hid)
                            End If
                        Case 2
                            If i = 0 Then

                            Else
                                txt = New TextBox
                                calEx = New CalendarExtender
                                txt.ID = "txtDate" + i.ToString()
                                calEx.ID = "calEx" + i.ToString()
                                calEx.TargetControlID = txt.ID
                                txt.Width = 200
                                'If i = 1 Then
                                '    calEx.CssClass = "black"
                                'ElseIf i = 2 Then
                                '    calEx.CssClass = "orange"
                                'ElseIf i = 3 Then
                                '    calEx.CssClass = "red"
                                'End If
                                If dsDate.Tables(0).Rows(i - 1).Item("ISCLIENT").ToString() <> "Y" Then
                                    txt.Enabled = False
                                Else
                                    txt.Enabled = True
                                    If Session("SavvyAnalyst") <> "Y" Then
                                        If Session("UserId") <> Request.QueryString("UserId").ToString() Then
                                            txt.Enabled = False
                                            btnUpdate.Enabled = False
                                        End If
                                    End If
                                    txt.CssClass = "MediumTextBox1"
                                End If

                                dvData.RowFilter = "DATETYPEID=" + dsDate.Tables(0).Rows(i - 1).Item("DATETYPEID").ToString()
                                dtData = dvData.ToTable()
                                If dtData.Rows.Count > 0 Then
                                    txt.Text = dtData.Rows(0).Item("VALUE").ToString()
                                End If
                                tr.CssClass = "AlterNateColor1"
                                td.Controls.Add(calEx)
                                td.Controls.Add(txt)
                            End If
                    End Select

                    tr.Controls.Add(td)
                Next
                tblDate.Controls.Add(tr)
            Next

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click

        Dim count As Integer = Convert.ToInt32(Session("count"))
        Dim Value(count) As String
        Dim DateId(count) As String
        Dim dsData As New DataSet
        Dim dvData As New DataView
        Dim dtData As New DataTable
        Dim flag As Boolean
        Dim Val As Integer = 0
        Try

            dsData = Session("dsData")
            dvData = dsData.Tables(0).DefaultView
            For i = 0 To count
                DateId(i) = Request.Form("hidId" + (i + 1).ToString() + "")
                Value(i) = Request.Form("txtDate" + (i + 1).ToString() + "")

                dvData.RowFilter = "DATETYPEID=" + DateId(i)
                dtData = dvData.ToTable()
                If dtData.Rows.Count > 0 Then
                    flag = objUpIns.UpdateProjectDate(Value(i), DateId(i), hidProjectId.Value)
                Else
                    flag = objUpIns.InsertProjectDate(Value(i), DateId(i), hidProjectId.Value)
                End If
                If flag = True Then
                    Val = 1
                End If
            Next
            If Val = 1 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename1", "alert('Dates updated successfully');", True)
                Try
                    objUpIns.InsertLog1(Session("UserId").ToString(), "DateDetails.aspx", " Edited MilestonePopup changed Desired complete date for ProjectId :" + hidProjectId.Value + "", hidProjectId.Value, Session("SPROJLogInCount").ToString())

                Catch ex As Exception

                End Try
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClosePage1", "ClosePage();", True)
            End If
            ProjectDetails()
        Catch ex As Exception
            _lErrorLble.Text = "Error:btnUpdate_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
End Class
