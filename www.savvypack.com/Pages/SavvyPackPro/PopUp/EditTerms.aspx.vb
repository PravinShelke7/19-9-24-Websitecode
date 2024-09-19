Imports System.Data
Imports System.Data.OleDb
Imports System
Imports SavvyProGetData
Imports SavvyProUpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Imports System.Net.Mail
Imports System.Net
Partial Class Pages_SavvyPackPro_Popup_EditTerms
    Inherits System.Web.UI.Page
    Dim objUpIns As New SavvyProUpInsData()
    Dim objGetData As New SavvyProGetData()
    Dim SpecId As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            SpecId = "1"
            GetPageDetails()
        Catch ex As Exception
            lblError.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub GetPageDetails()
        Dim dsQues As New DataSet()
        Dim lnkQues As LinkButton
        Dim trQues As TableRow
        Dim tdId As TableCell
        Dim delBtn As Button
        Dim trSpace As New TableRow
        Dim lbl As New Label
        Dim txtSeq As New TextBox
        Dim HidSeqQue As New HiddenField
        Dim SpecId As String = "1"
        Try
            tblEditQ.Rows.Clear()
            dsQues = objGetData.GetTerms(SpecId)
            For i = 0 To dsQues.Tables(0).Rows.Count - 1
                trQues = New TableRow
                lnkQues = New LinkButton

                For b = 1 To 5
                    tdId = New TableCell
                    Select Case b
                        Case 1
                            txtSeq = New TextBox
                            HidSeqQue = New HiddenField

                            InnerTdSetting(tdId, "", "Center")
                            txtSeq.Text = dsQues.Tables(0).Rows(i).Item("TERMSEQ").ToString()
                            txtSeq.ID = dsQues.Tables(0).Rows(i).Item("TERMID").ToString() + "_" + i.ToString()
                            HidSeqQue.Value = txtSeq.Text

                            txtSeq.Font.Size = 10
                            txtSeq.MaxLength = 3
                            txtSeq.ToolTip = "To set sequence of Questions."
                            txtSeq.Style.Add("background-color", "#FEFCA1")
                            txtSeq.Style.Add("font-family", "Verdana")
                            txtSeq.Style.Add("width", "28px")
                            txtSeq.Style.Add("height", "14px")
                            txtSeq.AutoPostBack = True
                            txtSeq.Attributes.Add("onchange", "javascript:return CheckSP(this,'" + HidSeqQue.Value + "');")
                            AddHandler txtSeq.TextChanged, AddressOf TextBox_TextChanged

                            tdId.Controls.Add(txtSeq)
                            tdId.Controls.Add(HidSeqQue)
                        Case 2
                            lbl = New Label
                            'InnerTdSetting(lbl, "", "Center")
                            lbl.Text = dsQues.Tables(0).Rows(i).Item("TITLE").ToString()
                            lbl.Font.Size = 10
                            tdId.Width = 400
                            tdId.Controls.Add(lbl)
                        Case 3
                            lbl = New Label
                            'InnerTdSetting(lbl, "", "Center")
                            lbl.Text = dsQues.Tables(0).Rows(i).Item("DESCRIPTION").ToString()
                            lbl.Font.Size = 10
                            tdId.Width = 400
                            tdId.Controls.Add(lbl)
                        Case 4
                            InnerTdSetting(tdId, "", "Left")
                            lnkQues.Attributes.Add("onclick", "return ShowPopUpWindow('TermPopup.aspx?SUId=" + SpecId.Trim() + "&Edit=E&QuesId=" + dsQues.Tables(0).Rows(i).Item("TERMID").ToString() + "');")
                            lnkQues.Text = "Edit"
                            lnkQues.Font.Size = 10
                            tdId.Width = 50
                            tdId.Controls.Add(lnkQues)
                        Case 5
                            InnerTdSetting(tdId, "", "Left")
                            delBtn = New Button
                            delBtn.Text = "Delete"
                            delBtn.ID = dsQues.Tables(0).Rows(i).Item("TERMID").ToString()
                            AddHandler delBtn.Click, AddressOf Delbtn_Click
                            tdId.Width = 50
                            tdId.Controls.Add(delBtn)
                    End Select
                    trQues.Controls.Add(tdId)
                Next
                If i Mod 2 = 0 Then
                    trQues.CssClass = "AlterNateColor1"
                Else
                    trQues.CssClass = "AlterNateColor2"
                End If
                tblEditQ.Controls.Add(trQues)
            Next
        Catch ex As Exception
            lblError.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub CheckBox_Check(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim QId As Integer
        Try
            Dim chk = DirectCast(sender, CheckBox)
            If chk.Checked = True Then
                QId = chk.ID
            End If
        Catch ex As Exception
            lblError.Text = "Error:CheckBox_Check:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub Delbtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim QId As Integer
        Try
            Dim btn = DirectCast(sender, Button)
            QId = btn.ID
            objUpIns.DeleteQuestion(SpecId.ToString(), QId.ToString())
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert1", "alert('Term Deleted Successfully...')", True)
            GetPageDetails()
        Catch ex As Exception
            lblError.Text = "Error:Delbtn_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub TextBox_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim TextSeq As String
        Dim QId As String
        Dim DsQuestionSeq As New DataSet()
        Dim Avail As New Boolean
        Try
            Dim TxtSeq = DirectCast(sender, TextBox)
            TextSeq = TxtSeq.Text
            QId = TxtSeq.ID

            Dim Temp As String() = QId.Split("_")
            Dim temp1 As String = Temp(0)

            Avail = objGetData.GetTermSeqAvail(TextSeq.ToString(), SpecId.ToString())
            If Avail Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert1", "alert('Entered sequence number already exist.')", True)
                'objUpIns.UpdateQueSeqID(TextSeq.ToString(), temp1.ToString())
            Else
                objUpIns.UpdateQueSeqID(TextSeq.ToString(), temp1.ToString())
            End If
            GetPageDetails()
        Catch ex As Exception
            lblError.Text = "Error:TextBox_TextChanged:" + ex.Message.ToString()
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
            Td.Style.Add("font-size", "18px")
            Td.Style.Add("font-family", "Optima")
        Catch ex As Exception
            lblError.Text = "Error:InnerTdSetting:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnrefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        Try
        Catch ex As Exception
            Response.Write("Error:btnrefresh_Click:" + ex.Message.ToString())
        End Try
    End Sub
End Class
