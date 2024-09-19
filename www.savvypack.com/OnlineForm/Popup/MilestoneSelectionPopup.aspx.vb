
Imports System.Data
Imports System.Data.OleDb
Imports System
Imports SavvyGetData
Imports SavvyUpInsData

Partial Class OnlineForm_Popup_MilestoneSelectionPopup
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
                    objUpIns.InsertLog1(Session("UserId").ToString(), "MilestoneSelectionPopup.aspx", "Opened Milestone Display Setting Popup " + "", "", Session("SPROJLogInCount").ToString())
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
        Dim dsS As New DataSet
        Dim dsStatus As New DataSet
        Dim objGetData As New Selectdata()
        Dim tr As TableRow
        Dim td As TableCell
        Dim lbl As Label
        Dim rad As RadioButton
        Dim radB As New RadioButton
        Dim chck As New CheckBox
        Dim txt As TextBox
        Dim hid As HiddenField
        Dim dsData As New DataSet
        Dim dsMemo As New DataSet
        Dim dtData As New DataTable
        Dim dsSortDate As New DataSet
        Try
            dsDate = objGetData.GetDateDetails()
            Session("dsData") = dsData
            dsMemo = objGetData.MemorizedDetails(Session("UserId"))
            Session("count") = dsDate.Tables(0).Rows.Count - 1
            If dsDate.Tables(0).Rows.Count > 0 Then
                For i = 0 To dsDate.Tables(0).Rows.Count - 1
                    tr = New TableRow
                    For j = 1 To 2
                        td = New TableCell
                        Select Case j
                            Case 1

                                rad = New RadioButton
                                hid = New HiddenField
                                rad.ID = "radBut" + (i + 1).ToString()
                                hid.ID = "hidId" + (i + 1).ToString()
                                rad.GroupName = "DateGroup"
                                If (i + 1).ToString = dsMemo.Tables(0).Rows(0).Item("SORTMILESDATE").ToString() Then
                                    rad.Checked = True
                                Else
                                    rad.Checked = False
                                End If
                                AddHandler rad.CheckedChanged, AddressOf onRadioClick
                                hid.Value = dsDate.Tables(0).Rows(i).Item("DATETYPEID").ToString()
                                rad.AutoPostBack = True
                                td.Controls.Add(rad)
                                td.Controls.Add(hid)
                                td.Width = 20

                            Case 2

                                lbl = New Label
                                lbl.Text = dsDate.Tables(0).Rows(i).Item("DATETYPENAME").ToString()
                                td.Controls.Add(lbl)
                                tr.CssClass = "AlterNateColor1"


                        End Select

                        tr.Controls.Add(td)
                    Next

                    tbldate.Controls.Add(tr)
                Next
            End If



            dsSortDate = objGetData.GetSortMilestoneDetails()
            Session("dsData") = dsData
            dsMemo = objGetData.MemorizedDetails(Session("UserId"))
            Session("count") = dsSortDate.Tables(0).Rows.Count - 1
            If dsSortDate.Tables(0).Rows.Count > 0 Then
                For i = 0 To dsSortDate.Tables(0).Rows.Count - 1
                    tr = New TableRow
                    For j = 1 To 2
                        td = New TableCell
                        Select Case j
                            Case 1

                                radB = New RadioButton
                                hid = New HiddenField
                                radB.ID = "rdButB" + (i + 1).ToString()
                                hid.ID = "hidIdB" + (i + 1).ToString()
                                radB.GroupName = "DateGroupB"
                                If (i + 1).ToString = dsMemo.Tables(0).Rows(0).Item("SORTMILESID").ToString() Then
                                    radB.Checked = True
                                Else
                                    radB.Checked = False
                                End If
                                AddHandler radB.CheckedChanged, AddressOf onRadioClickB
                                hid.Value = dsSortDate.Tables(0).Rows(i).Item("SORTMILESID").ToString()
                                radB.AutoPostBack = True
                                td.Controls.Add(radB)
                                td.Controls.Add(hid)
                                td.Width = 20


                            Case 2

                                lbl = New Label
                                lbl.Text = dsSortDate.Tables(0).Rows(i).Item("SORTMILESNAME").ToString()
                                td.Controls.Add(lbl)
                                tr.CssClass = "AlterNateColor1"
                        End Select
                        tr.Controls.Add(td)
                    Next
                    tblDateSelection.Controls.Add(tr)
                Next
            End If


            dsDate = objGetData.GetDateDetails()
            Session("dsDate1") = dsDate
            dsMemo = objGetData.MemorizedDetails(Session("UserId"))
            Session("count") = dsDate.Tables(0).Rows.Count - 1
            If dsDate.Tables(0).Rows.Count > 0 Then
                For i = 0 To dsDate.Tables(0).Rows.Count - 1
                    tr = New TableRow
                    For j = 1 To 2
                        td = New TableCell
                        Select Case j
                            Case 1

                                chck = New CheckBox
                                hid = New HiddenField
                                chck.ID = "chckButn" + (i + 1).ToString()
                                hid.ID = "hidchckb" + (i + 1).ToString()

                                If dsMemo.Tables(0).Rows(0).Item("DISPLAYMILESTONEID").Contains(i + 1).ToString() Then
                                    chck.Checked = True
                                Else
                                    chck.Checked = False
                                End If

                                hid.Value = dsDate.Tables(0).Rows(i).Item("DATETYPEID").ToString()
                                td.Controls.Add(chck)
                                td.Controls.Add(hid)
                                td.Width = 20

                            Case 2

                                lbl = New Label
                                lbl.Text = dsDate.Tables(0).Rows(i).Item("DATETYPENAME").ToString()
                                td.Controls.Add(lbl)
                                tr.CssClass = "AlterNateColor1"


                        End Select

                        tr.Controls.Add(td)
                    Next

                    tblDateDisplay.Controls.Add(tr)
                Next
            End If
        Catch ex As Exception

        End Try

    End Sub
    Protected Sub onRadioClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim RB As RadioButton = CType(sender, RadioButton)

        Dim radId As String = RB.ClientID.ToString()


        Dim Id As Integer = Integer.Parse(Regex.Replace(radId, "[^\d]", ""))

        hidSortVal.Value = Request.Form("hidId" + Id.ToString())

    End Sub
    Protected Sub onRadioClickB(ByVal sender As Object, ByVal e As EventArgs)

        Dim RBS As RadioButton = CType(sender, RadioButton)

        Dim radIdB As String = RBS.ClientID.ToString()


        Dim IdB As Integer = Integer.Parse(Regex.Replace(radIdB, "[^\d]", ""))

        hidSortValDate.Value = Request.Form("hidIdB" + IdB.ToString())


    End Sub
    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Dim flag As Boolean
        Dim dsData As New DataSet
        Dim dsMemo As New DataSet
        Dim dsDate As New DataSet
        Dim objGetData As New Selectdata()
        Dim strchk As String = String.Empty
        Try
            dsDate = Session("dsDate1")
            Session("count") = dsDate.Tables(0).Rows.Count - 1
            For i = 0 To dsDate.Tables(0).Rows.Count
                Dim chk As String
                chk = Request.Form("chckButn" + (i).ToString())
                If chk = "on" Then
                    strchk = strchk + Request.Form("hidchckb" + i.ToString()) + ","
                End If
            Next
            If strchk = "" Then
                'strchk = "0"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Please select atleast one checkbox.');", True)
            End If
            strchk = strchk.Remove(strchk.Length - 1)
            flag = objUpIns.UpdateMemorizedDetails(Session("UserId"), "", "", "", "", "", strchk, hidSortVal.Value, hidSortValDate.Value)
            'Started Activity Log Changes
            Try
                objUpIns.InsertLog1(Session("UserId").ToString(), "MilestoneSelectionPopup.aspx", "Milestone type:" + strchk + " Sort Value:" + hidSortVal.Value + " Sort by DateId:" + hidSortValDate.Value + "", Session("ProjectId"), Session("SPROJLogInCount").ToString())
            Catch ex As Exception

            End Try
            'Ended Activity Log Changes
            If flag = True Then
                Session("SORT") = "DATE"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClosePage", "ClosePage()", True)
            End If
        Catch ex As Exception

        End Try
    End Sub


End Class

