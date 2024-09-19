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
Partial Class Pages_SavvyPackPro_Popup_TermPopup
    Inherits System.Web.UI.Page
    Dim mode As String
    Dim QuesId As String
    Dim objGetData As New SavvyProGetData
    Dim objInsUpData As New SavvyProUpInsData
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            hidSpecId.Value = Request.QueryString.Item("SUId")
            hidRfpID.Value = Request.QueryString("RfpID").ToString()

            lblHeading.Text = "Add Term"

            If Not IsPostBack Then
                GetTerms()
            End If

        Catch ex As Exception
            lblError.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub GetTerms()
        Dim ds As New DataSet
        Dim dv As New DataView
        Dim dt As New DataTable
        Try
            ds = objGetData.GetTerms(Session("hidRfpID"), Session("UserId"))

            If mode = "E" Then
                dv = ds.Tables(0).DefaultView
                dv.RowFilter = "TERMID=" + QuesId
                dt = dv.ToTable()
                If dt.Rows.Count > 0 Then
                    txtI.Text = dt.Rows(0).Item("TITLE").ToString()
                    txtT.Text = dt.Rows(0).Item("DESCRIPTION").ToString()
                End If
            End If

        Catch ex As Exception
            lblError.Text = "Error:GetTerms " + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub Add_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Add.Click
        Dim DsQues As New DataSet()
        Dim DsQuesSeq As New DataSet()
        Dim DsTermSeq As DataSet
        Dim TermSeq As String = String.Empty
        Try

            If txtT.Text <> "" Then
                DsTermSeq = objGetData.GetTermSeq(Session("hidRfpID"), "N")
                If DsTermSeq.Tables(0).Rows(0).Item("TERMSEQ").ToString() = "" Then
                    TermSeq = "1"
                Else
                    TermSeq = DsTermSeq.Tables(0).Rows(0).Item("TERMSEQ").ToString() + 1
                End If
                objInsUpData.AddTerms(txtI.Text.Replace("'", "''").ToString(), txtT.Text.Replace("'", "''").ToString(), Session("UserId"), TermSeq, Session("hidRfpID"))
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "JS", "alert('Terms Added successfully');", True)
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClosePage", "ClosePage()", True)
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "JS", "alert('Please enter Title First');", True)
            End If

        Catch ex As Exception
            lblError.Text = "Error:Update_Click " + ex.Message.ToString()
        End Try

    End Sub

End Class
