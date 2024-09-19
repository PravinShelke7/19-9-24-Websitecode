Imports System.Data
Imports System.Data.OleDb
Imports System
Imports SavvyGetData
Imports SavvyUpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Imports System.Net.Mail
Imports System.Net
Partial Class Pages_ProjectSummary
    Inherits System.Web.UI.Page
    Dim objUpIns As New UpdateInsert()
#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_PAGES_PROJECTSUMMARY")
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        objRefresh.Render(Page)
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
           
            If Session("SBack") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If

            If Not IsPostBack Then
                Session("SeqCnt") = "1"
                hidAnalysisId.Value = "0"
                ProjectDetails()

                lblHeading.Text = "Edit Project"
                'Started Activity Log Changes
                Try
                    objUpIns.InsertLog1(Session("UserId").ToString(), "ProjectSummary.aspx", "Opened SavvyPack Project Summary Page to Edit a Project", hidProjectId.Value, Session("SPROJLogInCount").ToString())

                Catch ex As Exception

                End Try
                'Ended Activity Log Changes
            End If



        Catch ex As Exception
            lblError.Text = "Error:Page_Load:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Public Sub ProjectDetails()
        Dim dsProjId As New DataSet
        Dim dsUser As New DataSet
        Dim dsProj As New DataSet
        Dim objGetData As New Selectdata()
        Try
            hidProjectId.Value = Request.QueryString("ProjectId").ToString()
            dsProj = objGetData.GetEditProjectDetails(hidProjectId.Value)
            trNum.Visible = True
            lblNum.Text = dsProj.Tables(0).Rows(0).Item("PROJECTID").ToString()
            lblUser.Text = dsProj.Tables(0).Rows(0).Item("OWNER").ToString()
            txtTitle.Text = dsProj.Tables(0).Rows(0).Item("TITLE").ToString()
            txtDesc.Text = dsProj.Tables(0).Rows(0).Item("DESCRIPTION").ToString()
            txtWord.Text = dsProj.Tables(0).Rows(0).Item("KEYWORD").ToString()
            hidAnalysisId.Value = dsProj.Tables(0).Rows(0).Item("ANALYSISID").ToString()
            trAnalysis.Visible = False
            If Session("SavvyAnalyst") <> "Y" Then
                If Session("UserId") <> Request.QueryString("UserId").ToString() Then
                    txtTitle.Enabled = False
                    txtDesc.Enabled = False
                    txtWord.Enabled = False
                    btnUpdate.Enabled = False
                End If
            End If

        Catch ex As Exception
            lblError.Text = "Error:ProjectDetails:" + ex.Message.ToString() + ""
        End Try

    End Sub

    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Dim objUpIns As New SavvyUpInsData.UpdateInsert()
        Dim objGetData As New Selectdata()
        Dim flag As Boolean
        Dim ProjId As New Integer
        Dim ds As New DataSet
        Dim dsUser As New DataSet
        Try


            ds = objGetData.ExistingProjDetails(Session("UserId"), txtTitle.Text.Replace("'", "''"), hidProjectId.Value)
            If ds.Tables(0).Rows.Count = 0 Then
                If Session("SavvyAnalyst") = "Y" Then
                    flag = objUpIns.EditProjDetailsbyAnalyst(lblNum.Text, txtTitle.Text.Replace("'", "''"), txtWord.Text.Replace("'", "''"), txtDesc.Text.Replace("'", "''"), "1", hidAnalysisId.Value)
                Else
                    flag = objUpIns.EditProjDetails(lblNum.Text, Session("UserId"), txtTitle.Text.Replace("'", "''"), txtWord.Text.Replace("'", "''"), txtDesc.Text.Replace("'", "''"), hidAnalysisId.Value)
                End If

                If flag = True Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Project updated successfully');", True)
                    'Started Activity Log Changes
                    Try
                        objUpIns.InsertLog1(Session("UserId").ToString(), "ProjectSummary.aspx", "Edited One of SavvyPack Project detail of Peoject Summary page", hidProjectId.Value, Session("SPROJLogInCount").ToString())

                        Dim ht As Hashtable = New Hashtable()
                        If Session("ht") IsNot Nothing Then
                            ht = DirectCast(Session("ht"), Hashtable)
                        End If
                        Dim str() As String
                        For i = 1 To ht.Count
                            For Each item As Object In ht
                                str = item.Key.ToString().Split("-")
                                If str(1) = i Then
                                    objUpIns.EditInsertLog(Session("UserId").ToString(), "3", str(0), "1", item.Value.ToString().Replace("'", "''"), hidProjectId.Value, Session("SPROJLogInCount").ToString())
                                    Exit For
                                End If
                            Next
                        Next
                        Session("ht") = Nothing
                        Session("SeqCnt") = "1"
                    Catch ex As Exception

                    End Try
                    'Ended Activity Log Changes
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClosePage", "ClosePage()", True)
                End If
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Project Already Exist');", True)
            End If

        Catch ex As Exception
            lblError.Text = "Error:btnUpdate_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    <System.Web.Services.WebMethod()>
    Public Shared Function UpdateCase(ByVal Header As String, ByVal text As String) As String
        Try
            Dim ht As Hashtable = New Hashtable()
            Dim str() As String
            Dim seq As Integer = Convert.ToInt32(HttpContext.Current.Session("SeqCnt"))
            Dim flag As Boolean = False
            If HttpContext.Current.Session("ht") IsNot Nothing Then
                ht = DirectCast(HttpContext.Current.Session("ht"), Hashtable)
            End If
            For Each item As Object In ht
                str = item.Key.ToString().Split("-")
                If str(0) = Header Then
                    ht.Remove(item.Key)
                    flag = True
                    Exit For
                End If
            Next

            If flag Then
                ht.Add(Header + "-" + str(1).ToString(), text)
            Else
                ht.Add(Header + "-" + seq.ToString(), text)
                seq += 1
                HttpContext.Current.Session("SeqCnt") = seq
            End If

            HttpContext.Current.Session("ht") = ht

            Dim str1 As String = HttpContext.Current.Session("UserId").ToString()
            str1 = str1 + "Bhavesh"
            Return str1

        Catch ex As Exception

        End Try
    End Function

End Class
