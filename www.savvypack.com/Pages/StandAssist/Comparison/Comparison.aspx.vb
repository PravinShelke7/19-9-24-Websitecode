Imports System.Data
Imports System.Data.OleDb
Imports System
Imports StandGetData
Imports StandUpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_StructAssist_Default
    Inherits System.Web.UI.Page

#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_PAGES_STRUCTASSIST_DEFAULT")
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        objRefresh.Render(Page)
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objUpIns As New StandUpInsData.UpdateInsert
        Try
            Try
                lblTag.Text = "Copyright 1997 - " + Now.Year().ToString() + " SavvyPack Corporation"
            Catch ex As Exception
            End Try
            If Session("Back") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If
            
            If Not IsPostBack Then
                hidCompID.Value = "0"
                SavedSahredCombo()
                'Started Activity Log Changes
                Try
                    objUpIns.InsertLog1(Session("UserId").ToString(), "17", "Opened Structures Comparison Manager Page for Structures #" + Session("SBACaseId") + "", Session("SBACaseId"), Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                Catch ex As Exception

                End Try
                'Ended Activity Log Changes
            End If

        Catch ex As Exception
            lblError.Text = "Error:Page_Load:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub SavedSahredCombo()
        Dim GetData As New Selectdata()
        Try
            Dim Dts As New DataSet()
            Dts = GetData.GetSavedCaseAsperUser(Session("USERID").ToString(), "-1")
            If Dts.Tables(0).Rows.Count = 0 Then
                ''nodatatr1.Visible = True
                ''nodatatr2.Visible = True
                ''ddtr1.Visible = False
                ''ddtr2.Visible = False
                ''ddtr3.Visible = False
                ''headingtr1.Visible = False
                ''headingtr2.Visible = False
                ''buttontr1.Visible = False
                ''buttontr11.Visible = False
                ''buttontr3.Visible = False
                lblCompCase.Visible = True
                lnkComparison.Visible = False

            Else
                ''nodatatr1.Visible = False
                ''nodatatr2.Visible = False
                ''ddtr1.Visible = True
                ''ddtr2.Visible = True
                ''ddtr3.Visible = True
                ''headingtr1.Visible = True
                ''headingtr2.Visible = True
                ''buttontr1.Visible = True
                ''buttontr11.Visible = True
                ''buttontr3.Visible = True
                'Saved Comparison Combo
                lnkComparison.Visible = True
                lblCompCase.Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub
    
    Protected Sub btnCompRename_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCompRename.Click
        Try
            Dim objGetData As New StandGetData.Selectdata()
            Dim ds As New DataSet
            Dim objUpIns As New StandUpInsData.UpdateInsert
            If Not objRefresh.IsRefresh Then
                If hidCompID.Value = "0" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "alert('Please select a Comparison');", True)
                    'Started Activity Log Changes
                    Try
                        objUpIns.InsertLog1(Session("UserId").ToString(), "17", "Clicked on Rename Comparison But No Comparison was selected (Under Structure #" + Session("SBACaseId").ToString() + ")", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                    Catch ex As Exception

                    End Try
                    'Ended Activity Log Changes
                Else
                    divCompRename.Visible = True
                    ' ds = objGetData.GetReportsDetail(hidCompID.Value, Session("UserId"))
                    ds = objGetData.GetSavedCaseAsperUser(Session("USERID").ToString(), hidCompID.Value)
                    If ds.Tables(0).Rows.Count > 0 Then
                        txtCompName.Text = ds.Tables(0).Rows(0).Item("DESCRIPTION").ToString()
                    End If
                    lnkComparison.Text = hidCompDes.Value
                    'Started Activity Log Changes
                    Try
                        objUpIns.InsertLog1(Session("UserId").ToString(), "17", "Clicked on Rename Comparison #" + hidCompID.Value + " (Under Structure #" + Session("SBACaseId").ToString() + ")", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                    Catch ex As Exception

                    End Try
                    'Ended Activity Log Changes
                End If
            End If
            
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnCompToolBox_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCompToolBox.Click
        Try
            Dim objUpIns As New StandUpInsData.UpdateInsert
            'objUpIns.InsertLog1(Session("UserId").ToString(), Session("UserName").ToString(), "Default.aspx", Page.Title, "Clicked on Comparison ToolBox Button", "null", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "")

            Response.Redirect("ComparisonTool.aspx", True)
        Catch ex As Exception
            lblError.Text = "Error:btnCompToolBox_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    
    Protected Sub btnCompStart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCompStart.Click
        Try
            Dim objUpIns As New StandUpInsData.UpdateInsert
            If Not objRefresh.IsRefresh Then
                If hidCompID.Value = "0" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "alert('Please select a Comparison');", True)
                    lnkComparison.Text = "Select Comparison"
                    'Started Activity Log Changes
                    Try
                        objUpIns.InsertLog1(Session("UserId").ToString(), "17", "Clicked on Start a Comparison But No Comparison was selected( Under Structure #" + Session("SBACaseId").ToString() + ") ", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                    Catch ex As Exception

                    End Try
                    'Ended Activity Log Changes
                Else
                    Session("AssumptionID") = hidCompID.Value
                    Session("Description") = hidCompDes.Value
                    'Displaynote.Visible = False
                    Session("WhichForm") = "2"

                    Try

                        If CheckAnyDelCase("Start") = True Then
                            Response.Write("<script language=JavaScript>window.open('Extrusion.aspx','new_Win');</script>")

                        End If

                    Catch ex As Exception
                        Response.Write("Assumptions Comparisons.StartComparisonButton_Click.Error" + ex.Message.ToString())
                    End Try
                    lnkComparison.Text = hidCompDes.Value
                End If

            End If
        Catch ex As Exception
            lblError.Text = "btnToolBox_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Private Function CheckAnyDelCase(ByVal flag As String) As Boolean
        Dim DelCases As String = String.Empty
        Dim AllCases() As String
        Dim DelCase As String = String.Empty
        Dim SisCase As String = String.Empty
        Dim PropCase As String = String.Empty
        Dim flagCase As Boolean
        Dim i As Integer
        Dim GetData As New StandGetData.Selectdata()
        Dim objUpIns As New StandUpInsData.UpdateInsert
        Dim Compflag As String = "N"
        Try
            If Session("SBACompLib") = "Y" Then
                Compflag = "Y"
            End If

            AllCases = GetData.Cases(hidCompID.Value)
            For i = 0 To AllCases.Length - 1

                DelCase = GetData.GetDelCases(AllCases(i), Session("USERID").ToString(), Compflag)
                If (DelCase <> "") Then
                    PropCase = PropCase + DelCase + ","
                    DelCases = DelCases + DelCase + ","
                End If
            Next
            If DelCases <> "" Then
                Dim message As String = ""
                DelCases = DelCases.Remove(DelCases.Length - 1)

                If PropCase <> "" Then
                    PropCase = PropCase.Remove(PropCase.Length - 1)
                End If

                If flag = "Start" Then
                    message = "-----------------------------------------------------------------------"
                    If PropCase <> "" Then
                        message = message + "\nStructure(s) " + PropCase + " in this Comparison have been deleted, so \n Comparison " + hidCompID.Value + " will no longer function. We recommend you close \n this window and delete Comparison " + hidCompID.Value + "\n"
                    End If

                    message = message + "-----------------------------------------------------------------------\n"
                    'Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('--------------------------------------------------------------------------------\n Structure(s) " + DelCases + " in this comparison have been deleted, so \n comparison " + hidCompID.Value + " will no longer function. Please close \n this window and delete comparison " + hidCompID.Value + " \n ------------------------------------------------------------------------------\n');", True)
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('" + message + "');", True)
                    If PropCase = "" Then
                        'Started Activity Log Changes
                        Try
                            objUpIns.InsertLog1(Session("UserId").ToString(), "17", "Clicked on Start a Comparison for Comparison Id #" + hidCompID.Value + " (Under Structure #" + Session("SBACaseId").ToString() + ")", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                            objUpIns.InsertLog1(Session("UserId").ToString(), "21", "Opened Strcuture Comparison for Comparison Id #" + hidCompID.Value + " (Under Structure #" + Session("SBACaseId").ToString() + ")", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                        Catch ex As Exception

                        End Try
                        'Ended Activity Log Changes
                        Return True
                    Else
                        'Started Activity Log Changes
                        Try
                            objUpIns.InsertLog1(Session("UserId").ToString(), "17", "Clicked on Start a Comparison for Comparison Id #" + hidCompID.Value + " (Under Structure #" + Session("SBACaseId").ToString() + ") ", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                            objUpIns.InsertLog1(Session("UserId").ToString(), "17", "Got Error message for Comparison Id #" + hidCompID.Value + " selection because Structure(s) " + PropCase + " have been deleted." + " (Under Structure #" + Session("SBACaseId").ToString() + ")", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                        Catch ex As Exception

                        End Try
                        'Ended Activity Log Changes
                        Return False
                    End If

                ElseIf flag = "Edit" Then
                    If PropCase <> "" Then
                        message = message + "\nStructure(s) " + PropCase + " in this Comparison have been deleted, so \n Comparison " + hidCompID.Value + " will no longer editable. We recommend you close \n this window and delete Comparison " + hidCompID.Value + "\n"
                    End If

                    message = message + "-----------------------------------------------------------------------\n"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('" + message + "');", True)
                    'Started Activity Log Changes
                    Try
                        objUpIns.InsertLog1(Session("UserId").ToString(), "17", "Clicked on Rename Comparison for Comparison Id #" + hidCompID.Value + " (Under Structure #" + Session("SBACaseId").ToString() + ")", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                        objUpIns.InsertLog1(Session("UserId").ToString(), "17", "Got Error message for Comparison Id #" + hidCompID.Value + " selection because Structure(s) " + PropCase + " have been deleted." + " (Under Structure #" + Session("SBACaseId").ToString() + ")", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                    Catch ex As Exception

                    End Try
                    'Ended Activity Log Changes
                    Return False
                End If
            Else
                If flag = "Edit" Then
                    'Started Activity Log Changes
                    Try
                        objUpIns.InsertLog1(Session("UserId").ToString(), "17", "Clicked on Rename Comparison for Comparison Id #" + hidCompID.Value + " (Under Structure #" + Session("SBACaseId").ToString() + ")", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                    Catch ex As Exception

                    End Try
                    'Ended Activity Log Changes
                Else
                    'Started Activity Log Changes
                    Try
                        objUpIns.InsertLog1(Session("UserId").ToString(), "17", "Clicked on Start a Comparison for Comparison Id #" + hidCompID.Value + " (Under Structure #" + Session("SBACaseId").ToString() + ")", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                        objUpIns.InsertLog1(Session("UserId").ToString(), "21", "Opened Strcuture Comparison for Comparison Id #" + hidCompID.Value + " (Under Structure #" + Session("SBACaseId").ToString() + ")", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                    Catch ex As Exception

                    End Try
                    'Ended Activity Log Changes
                End If
                Return True
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Protected Sub btnCUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCUpdate.Click
        Dim objUpIns As New UpdateInsert()
        Dim ds As New DataSet()
        Try
            ds = GetComparisionCheck(txtCompName.Text, Session("USERID"))
            If ds.Tables(0).Rows.Count > 0 Then

                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Comparison already exists');", True)
                'Started Activity Log Changes
                Try
                    objUpIns.InsertLog1(Session("UserId").ToString(), "17", "Comparison name already exists", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                Catch ex As Exception

                End Try
                'Ended Activity Log Changes
            Else
                objUpIns.EditComparisonName(hidCompID.Value, txtCompName.Text.Replace("'", "''"))
                hidCompDes.Value = hidCompID.Value + ":" + txtCompName.Text.Trim.ToString()
                lnkComparison.Text = hidCompDes.Value
                divCompRename.Visible = False
                'Started Activity Log Changes
                Try
                    objUpIns.InsertLog1(Session("UserId").ToString(), "17", "Comparison updated for #" + hidCompID.Value + " ", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                Catch ex As Exception

                End Try
                'Ended Activity Log Changes
            End If


           
        Catch ex As Exception
            lblError.Text = "Error:btnCUpdate_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
    Public Function GetComparisionCheck(ByVal compName As String, ByVal UserId As String) As DataSet
        Dim Dts As New DataSet
        Dim SBAConnection As String = System.Configuration.ConfigurationManager.AppSettings("SBAConnectionString")
        Try
            Dim StrSqlSaved As String = ""
            Dim odbUtil As New DBUtil()
            StrSqlSaved = "SELECT 1 FROM Assumptions "
            StrSqlSaved = StrSqlSaved + " WHERE TRIM(UPPER(DESCRIPTION))='" + compName.ToUpper().Trim().Replace("'", "''") + "' AND USERID= " + UserId.ToString() + " "
            Dts = odbUtil.FillDataSet(StrSqlSaved, SBAConnection)
        Catch ex As Exception
            Throw
        End Try
        Return Dts
    End Function

    Protected Sub btnCompCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCompCancel.Click
        Try
            divCompRename.Visible = False
            lnkComparison.Text = hidCompDes.Value
        Catch ex As Exception

        End Try
    End Sub
End Class
