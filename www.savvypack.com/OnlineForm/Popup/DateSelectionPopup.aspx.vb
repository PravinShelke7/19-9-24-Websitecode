Imports System.Data
Imports System.Data.OleDb
Imports System
Imports SavvyGetData
Imports SavvyUpInsData

Partial Class OnlineForm_Popup_DateSelectionPopup
    Inherits System.Web.UI.Page
    Dim objUpIns As New UpdateInsert()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            
            If Session("SBack") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If

            If Not IsPostBack Then

                'Started Activity Log Changes
                Try
                    objUpIns.InsertLog1(Session("UserId").ToString(), "ProjectSummary.aspx", "Opened SavvyPack Project Summary Page to Create New Project", "", Session("SPROJLogInCount").ToString())
                Catch ex As Exception

                End Try
                'Ended Activity Log Changes
            End If
            ProjectDetails()

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
        Dim rad As RadioButton
        Dim txt As TextBox
        Dim hid As HiddenField
        Dim dsData As New DataSet
        Dim dsMemo As New DataSet
        Dim dtData As New DataTable

        Try

            'hidProjectId.Value = Request.QueryString("ProjectId").ToString()
            dsDate = objGetData.GetDateDetails()
            'dsData = objGetData.GetExistingDateDetails(hidProjectId.Value)
            Session("dsData") = dsData
            dsMemo = objGetData.MemorizedDetails(Session("UserId"))
            Session("count") = dsDate.Tables(0).Rows.Count - 1
            For i = 0 To dsDate.Tables(0).Rows.Count
                tr = New TableRow
                For j = 1 To 2
                    td = New TableCell
                    Select Case j
                        Case 1
                            If i = 0 Then

                            Else
                                rad = New RadioButton
                                hid = New HiddenField
                                rad.ID = "radBut" + i.ToString()
                                hid.ID = "hidId" + i.ToString()
                                rad.GroupName = "DateGroup"
                                If (i).ToString = dsMemo.Tables(0).Rows(0).Item("DATETYPEID").ToString() Then
                                    rad.Checked = True
                                Else
                                    rad.Checked = False
                                End If
                                AddHandler rad.CheckedChanged, AddressOf onRadioClick
                                hid.Value = dsDate.Tables(0).Rows(i - 1).Item("DATETYPEID").ToString()
                                rad.AutoPostBack = True
                                td.Controls.Add(rad)
                                td.Controls.Add(hid)
                                td.Width = 20
                            End If
                        Case 2
                            If i = 0 Then

                            Else
                                lbl = New Label
                                lbl.Text = dsDate.Tables(0).Rows(i - 1).Item("DATETYPENAME").ToString()
                                lbl.ToolTip = dsDate.Tables(0).Rows(i - 1).Item("TOOLTIPDS").ToString()
                                td.Controls.Add(lbl)
                                tr.CssClass = "AlterNateColor1"
                            End If

                    End Select

                    tr.Controls.Add(td)
                Next

                tblDate.Controls.Add(tr)
            Next

        Catch ex As Exception

        End Try

    End Sub
    Protected Sub onRadioClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim RB As RadioButton = CType(sender, RadioButton)
        Dim radId As String = RB.ClientID.ToString()
        Dim flag As Boolean
        Dim Id As Integer = Integer.Parse(Regex.Replace(radId, "[^\d]", ""))
        Dim TypeId As String = Request.Form("hidId" + Id.ToString())
        flag = objUpIns.UpdateMemorizedDetails(Session("UserId"), "", TypeId, "")
        If flag = True Then
            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Date Selected successfully');", True)
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClosePage", "ClosePage()", True)
        End If


    End Sub

End Class
