Imports System
Imports System.Data
Imports StandGetData
Imports StandUpInsData
Partial Class Pages_StandAssist_Tools_ComparisonTool
    Inherits System.Web.UI.Page
    
    Protected Sub btnCreate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreate.Click
        Dim GetData As New StandGetData.Selectdata()
        divCreateComp.Visible = True
        divShareComp.Visible = False
        Dim DsCases As New DataTable
        Dim i As Integer
        Dim objUpIns As New StandUpInsData.UpdateInsert
        Dim Compflag As String = "N"
        Try
            If Session("SBACompLib") = "Y" Then
                Compflag = "Y"
            End If
            DsCases = GetData.GetStructures(Session("USERID"), Compflag)
            CaseComp.DataSource = DsCases
            CaseComp.DataValueField = "caseID"
            CaseComp.DataTextField = "CaseDe"
            CaseComp.DataBind()

            'Started Activity Log Changes
            Try
                objUpIns.InsertLog1(Session("UserId").ToString(), "18", "Clicked on Create Comparison on Structures Comparison Tools Page (Under Structure #" + Session("SBACaseId").ToString() + ")", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
            Catch ex As Exception

            End Try
            'Ended Activity Log Changes


        Catch ex As Exception
            lblError.Text = "Error:UpdateGlobalbtn_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            Dim objUpIns As New StandUpInsData.UpdateInsert
            If Session("Back") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If

            If Not IsPostBack Then
                lblTag.Text = "Copyright 1997 - " + Now.Year().ToString() + " Savvypack Corporation."

                hidCompID.Value = "0"
                SavedSahredCombo()
                'Started Activity Log Changes
                Try
                    objUpIns.InsertLog1(Session("UserId").ToString(), "18", "Opened Structures Comparison Tools Page (Under Structure #" + Session("SBACaseId").ToString() + ")", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                Catch ex As Exception

                End Try
                'Ended Activity Log Changes
            End If
            If hidCompID.Value = "0" Then
                hidCompDes.Value = "Select a Comparison"
            End If
            lnkComparison.Text = hidCompDes.Value

        Catch ex As Exception
        End Try
    End Sub

    'Protected Sub CaseComp_DataBound(ByVal sender As Object, ByVal e As EventArgs)
    '    Dim GetData As New StandGetData.Selectdata()
    '    divCreateComp.Visible = True
    '    divShareComp.Visible = False
    '    Dim DsCases As New DataTable
    '    Dim i As Integer = 0
    '    If Session("SavvyModType") <> "1" Then
    '        If Session("E3LicAdmin") <> "Y" Then
    '            'DsCases = GetData.GetCasesE3(Session("UserName"), Session("Password"), Session("E3LicAdmin"))
    '            'For Each li As ListItem In CaseComp.Items


    '            '    If (DsCases.Rows(i).Item("STATUSID") = "5") Then
    '            '        Dim ccItem As ListItem = CaseComp.Items(i)
    '            '        If ccItem IsNot Nothing Then
    '            '            ccItem.Attributes.Add("disabled", "")
    '            '        End If
    '            '    End If
    '            '    i += 1
    '            'Next
    '        Else
    '            DsCases = GetData.GetCasesE3(Session("UserName"), Session("Password"), Session("E3LicAdmin"))
    '            For Each li As ListItem In CaseComp.Items
    '                If (DsCases.Rows(i).Item("STATUSID") = "4") Then
    '                    Dim ccItem As ListItem = CaseComp.Items(i)
    '                    If ccItem IsNot Nothing Then
    '                        ccItem.Attributes.Add("disabled", "")
    '                    End If
    '                End If
    '                i += 1
    '            Next
    '        End If

    '    End If





    'End Sub
    Protected Sub btnCreateComp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreateComp.Click
        Dim objUpIns As New UpdateInsert()
        Dim Caseid As String
        Dim ID As String = 1
        Dim Name As String = ""
        Dim dsGrps As New DataSet()

        Name = Trim(ComparisonName.Text.Replace("'", "''"))
        Dim CaseArray() As String

        If Name.Length <> 0 Then
            dsGrps = GetComparisionCheck(ComparisonName.Text, Session("USERID"))



            If dsGrps.Tables(0).Rows.Count > 0 Then

                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Comparison already exists');", True)
                'Started Activity Log Changes
                Try
                    objUpIns.InsertLog1(Session("UserId").ToString(), "18", "Got Error on Create Comparison:Comparison already exists (Under Structure #" + Session("SBACaseId").ToString() + ")", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                Catch ex As Exception

                End Try
                'Ended Activity Log Changes
            Else
                'Getting Selected CaseID From Combo
                CreateCompError.Visible = False
                Caseid = Request.Form("CaseComp")
                CaseArray = Split(Caseid, ",")
                If CaseArray.Length <= 10 And CaseArray.Length >= 2 Then
                    CreateCompError.Visible = False
                    Dim UpadateFun As New UpdateInsert()
                    Session("AssumptionID") = UpadateFun.AssumptionUpdate(Caseid, Session("USERID"))
                    Session("WhichForm") = "2"
                    UpadateFun.AlterComparison(Name, ID, Session("AssumptionID"), Session("USERID"), Session("Password"))
                    Call SavedSahredCombo()
                    ComparisonName.Text = ""
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "alert('Comparison#" + Session("AssumptionID").ToString() + " Created Successfully');", True)
                    divCreateComp.Visible = False

                    'Started Activity Log Changes
                    Try
                        Dim caseIdList() As String
                        caseIdList = Regex.Split(Caseid, ",")

                        objUpIns.InsertLog1(Session("UserId").ToString(), "18", "Comparison #" + Session("AssumptionID").ToString() + " Created Successfully.(Under Structure #" + Session("SBACaseId").ToString() + ")", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                        If caseIdList.Length > 0 Then
                            For i = 0 To caseIdList.Length - 1
                                objUpIns.InsertLog1(Session("UserId").ToString(), "18", "Structure #" + caseIdList(i).ToString() + " Added in Comparison #" + Session("AssumptionID").ToString() + ".(Under Structure #" + Session("SBACaseId").ToString() + ")", caseIdList(i).ToString(), Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                            Next

                        End If

                    Catch ex As Exception

                    End Try
                    'Ended Activity Log Changes

                Else
                    CaseComp.Focus()
                    If CaseArray.Length > 10 Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "alert('You cannot select the more than 10 Structures');", True)
                        'Started Activity Log Changes
                        Try
                            objUpIns.InsertLog1(Session("UserId").ToString(), "18", "Got Error on Comparison Creation:You cannot select the more than 10 Structures (Under Structure #" + Session("SBACaseId").ToString() + ")", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                        Catch ex As Exception

                        End Try
                        'Ended Activity Log Changes
                    Else
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "alert('Please select atleast 2 Structures for Comparison');", True)
                        'Started Activity Log Changes
                        Try
                            objUpIns.InsertLog1(Session("UserId").ToString(), "18", "Got Error on Comparison Creation:Please select atleast 2 Structures for Comparison (Under Structure #" + Session("SBACaseId").ToString() + ")", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                        Catch ex As Exception

                        End Try
                        'Ended Activity Log Changes
                    End If
                End If
            End If
        Else
            ComparisonName.Focus()
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "alert('Please enter the text for new Comparison');", True)
            'Started Activity Log Changes
            Try
                objUpIns.InsertLog1(Session("UserId").ToString(), "18", "Got Error on Comparison Creation:enter the text for new Comparison (Under Structure #" + Session("SBACaseId").ToString() + ")", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
            Catch ex As Exception

            End Try
            'Ended Activity Log Changes
        End If
            lnkComparison.Text = "Select Comparison"

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

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Dim objUpIns As New UpdateInsert()
        divCreateComp.Visible = False
        'Started Activity Log Changes
        Try
            objUpIns.InsertLog1(Session("UserId").ToString(), "18", "Create Comparison has been cancelled. (Under Structure #" + Session("SBACaseId").ToString() + ")", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
        Catch ex As Exception

        End Try
        'Ended Activity Log Changes
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            Dim ID As String = 3
            Dim Name As String = ""
            Dim Deletefun As New UpdateInsert()
            Dim objUpIns As New UpdateInsert()

            If hidCompID.Value <> "0" Then
                Deletefun.AlterComparison(Name, ID, hidCompID.Value, Session("USERID"), Session("Password"))
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Comparison#" + hidCompID.Value + " is Deleted Successfully');", True)
                Call SavedSahredCombo()
                'Started Activity Log Changes
                Try
                    objUpIns.InsertLog1(Session("UserId").ToString(), "18", "Comparison Id #" + hidCompID.Value + " is Deleted Successfully (Under Structure #" + Session("SBACaseId").ToString() + ")", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                Catch ex As Exception

                End Try
                'Ended Activity Log Changes
                hidCompID.Value = "0"
                hidCompDes.Value = "Select Comparison"
                lnkComparison.Text = hidCompDes.Value

              
            Else

            End If

        Catch ex As Exception
            lblError.Text = "Error:UpdateGlobalbtn_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
    Protected Sub SavedSahredCombo()
        Dim GetData As New Selectdata()
        Try
            Dim Dts As New DataSet()
            Dts = GetData.GetSavedCaseAsperUser(Session("USERID").ToString(), "-1")
            If Dts.Tables(0).Rows.Count = 0 Then
                lblPCase.Visible = True
                lnkComparison.Visible = False
            Else
                lnkComparison.Visible = True
                lblPCase.Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnShare_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShare.Click
        Dim GetData As New StandGetData.Selectdata()
        Try
            If hidCompID.Value = "0" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Please select Comparison');", True)
            Else
                If CheckAnyDelCase("Share") = True Then
                    divCreateComp.Visible = False
                    divShareComp.Visible = True
                    Dim DsCoUser As DataSet = GetData.GetUserCompanyUsers(Session("UserName"))
                    Coworker.DataSource = DsCoUser
                    Coworker.DataValueField = "USERID"
                    Coworker.DataTextField = "Username"
                    Coworker.DataBind()

                End If

            End If

          


        Catch ex As Exception
            lblError.Text = "Error:btnShare_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub SharedButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SharedButton.Click

        Dim shareusername As String = Coworker.SelectedValue
        'Dim ID As String = SharedComp.SelectedValue
        Dim Insert As New UpdateInsert()
        Dim ds As New DataSet()
        Dim ds1 As New DataSet()
        Dim dsDel As New DataSet()
        Dim dsCaseId As New DataSet()
        Dim strCase, strCaseIds() As String
        Dim cnt As Integer = 0
        Dim strCaseId As String = String.Empty
        Dim i As Integer
        Dim ListCaseIds As String = String.Empty
        Dim strMsg As String = String.Empty
        Dim GetData As New StandGetData.Selectdata
        Dim Dts As New DataSet()
        Dim objUpIns As New UpdateInsert()
        Dim Compflag As String = "N"
        Try
            If Session("SBACompLib") = "Y" Then
                Compflag = "Y"
            End If

            ds = GetData.GetAllPermissionCases(hidCompID.Value)

            ''CHECK USER HAS ACCESS OR NOT
            For i = 0 To ds.Tables(0).Rows.Count - 1
                ds1 = GetData.CheckUser(ds.Tables(0).Rows(i)(0).ToString(), shareusername)
                If (ds1.Tables(0).Rows.Count = 0) Then
                    ListCaseIds = ListCaseIds + ds.Tables(0).Rows(i)(0).ToString() + ","
                End If
            Next

            'Getting all caseids of Assumptions

            '''' strCase = GetData.GetAssumptionCaseId(SharedComp.SelectedValue)

            Dts = GetData.GetAssumptionCaseId(hidCompID.Value)
            strCase = Dts.Tables(0).Rows(0).Item("STRUCTID").ToString()
            strCaseIds = Regex.Split(strCase, ",")
            For i = 0 To strCaseIds.Length - 1
                If strCaseIds(i) <> "" Then
                    dsCaseId = GetData.GetCasesByCaseID(strCaseIds(i))
                    If dsCaseId.Tables(0).Rows.Count > 0 Then

                    Else
                        If cnt = 0 Then
                            strCaseId = strCaseIds(i)
                        Else
                            strCaseId = strCaseId + "," + strCaseIds(i)
                        End If
                        cnt += 1

                    End If
                End If
            Next
            Dim str11 As String = ""
            If ListCaseIds <> "" Then
                ListCaseIds = ListCaseIds.Remove(ListCaseIds.Length - 1)
                If strCaseId = "" Then
                    strMsg = "---------------------------------------------------------------------------------------------\n You cannot share this Comparison with User " + Coworker.SelectedItem.Text + ",\n because the following structure Id\'s have not been shared with User: " + ListCaseIds + "\n---------------------------------------------------------------------------------------------\n"
                    'Started Activity Log Changes
                    Try
                        objUpIns.InsertLog1(Session("UserId").ToString(), "18", "Got Error on Share Access for Comparison #" + hidCompID.Value + " because following Structure(s) " + ListCaseIds + " has not been shared with User " + Coworker.SelectedItem.Text, "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                    Catch ex As Exception

                    End Try
                    'Ended Activity Log Changes
                Else
                    strMsg = "---------------------------------------------------------------------------------------------\n You cannot share this Comparison with User " + Coworker.SelectedItem.Text + ",\n because the following structure Id\'s have not been shared with User: " + ListCaseIds + "\n Also following Structure is deleted from table:" + strCaseId + "\n---------------------------------------------------------------------------------------------\n"
                    'Started Activity Log Changes
                    Try
                        objUpIns.InsertLog1(Session("UserId").ToString(), "18", "Got Error on Share Access for Comparison #" + hidCompID.Value + " because following Structure(s) " + ListCaseIds + " has not been shared with User " + Coworker.SelectedItem.Text + "Also following Structure is deleted from table:" + strCaseId + " ", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                    Catch ex As Exception

                    End Try
                    'Ended Activity Log Changes
                End If
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('" + strMsg + "');", True)
            Else
                If strCaseId <> "" Then
                    strMsg = "---------------------------------------------------------------------------------------------\n You cannot share this Comparison with User " + Coworker.SelectedItem.Text + ",\n because the following Structure is deleted from table:" + strCaseId + "\n---------------------------------------------------------------------------------------------\n"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('" + strMsg + "');", True)
                    'Started Activity Log Changes
                    Try
                        objUpIns.InsertLog1(Session("UserId").ToString(), "18", "Got Error on Share Access for Comparison #" + hidCompID.Value + " because following Structure is deleted from table:" + strCaseId + " ", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                    Catch ex As Exception

                    End Try
                    'Ended Activity Log Changes
                Else
                    Insert.SharedCase(shareusername, hidCompID.Value, Dts.Tables(0).Rows(0).Item("DESCRIPTION").ToString(), Dts.Tables(0).Rows(0).Item("STRUCTID").ToString())
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('--------------------------------------------------------------------------------------------------------\n Comparison:--" + hidCompID.Value + " is successfully shared with user:-" + Coworker.SelectedItem.Text + "\n--------------------------------------------------------------------------------------------------------\n');", True)
                    'Started Activity Log Changes
                    Try
                        objUpIns.InsertLog1(Session("UserId").ToString(), "18", "Comparison #" + hidCompID.Value + " successfully shared with User " + Coworker.SelectedItem.Text + " (Under Structure #" + Session("SBACaseId").ToString() + ")", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                    Catch ex As Exception

                    End Try
                    'Ended Activity Log Changes
                    divShareComp.Visible = False
                End If
            End If

        Catch ex As Exception
            lblError.Text = "Error:SharedButton_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnSCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSCancel.Click
        Try
            Dim objUpIns As New StandUpInsData.UpdateInsert
            divShareComp.Visible = False
            'Started Activity Log Changes
            Try
                objUpIns.InsertLog1(Session("UserId").ToString(), "18", "Comparison #" + hidCompID.Value + " Share Access has been cancelled (Under Structure #" + Session("SBACaseId").ToString() + ")", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
            Catch ex As Exception

            End Try
            'Ended Activity Log Changes
        Catch ex As Exception
            lblError.Text = "Error:SharedButton_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            If CheckAnyDelCase("Edit") = True Then
                Dim obj As New CryptoHelper
                ' Response.Redirect("EditComparision.aspx?Id=" + obj.Encrypt(hidCompID.Value))

                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "openWindowU('EditComparision.aspx?Id=" + obj.Encrypt(hidCompID.Value) + "');", True)
            End If
        Catch ex As Exception
            lblError.Text = "Error:btnEdit_Click:" + ex.Message.ToString() + ""
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

                If flag = "Share" Then
                    message = "-----------------------------------------------------------------------"
                    If PropCase <> "" Then
                        message = message + "\nStructure(s) " + PropCase + " in this Comparison have been deleted, so \n Comparison " + hidCompID.Value + " will no longer function. We recommend you close \n this window and delete Comparison " + hidCompID.Value + "\n"
                    End If

                    message = message + "-----------------------------------------------------------------------\n"
                    'Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('--------------------------------------------------------------------------------\n Structure(s) " + DelCases + " in this comparison have been deleted, so \n comparison " + hidCompID.Value + " will no longer function. Please close \n this window and delete comparison " + hidCompID.Value + " \n ------------------------------------------------------------------------------\n');", True)
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('" + message + "');", True)
                    If PropCase = "" Then
                        Return True
                    Else
                        'Started Activity Log Changes
                        Try
                            objUpIns.InsertLog1(Session("UserId").ToString(), "18", "Clicked on Share Access for Comparison Id #" + hidCompID.Value + " (Under Structure #" + Session("SBACaseId").ToString() + ")", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                            objUpIns.InsertLog1(Session("UserId").ToString(), "18", "Got Error message for Comparison Id #" + hidCompID.Value + " selection because Structure(s) " + PropCase + " have been deleted." + " (Under Structure #" + Session("SBACaseId").ToString() + ")", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                        Catch ex As Exception

                        End Try
                        'Ended Activity Log Changes
                        Return False
                    End If

                ElseIf flag = "Edit" Then
                    message = "-----------------------------------------------------------------------"
                    If PropCase <> "" Then
                        message = message + "\nStructure(s) " + PropCase + " in this Comparison have been deleted, so \n Comparison " + hidCompID.Value + " will no longer editable. We recommend you close \n this window and delete Comparison " + hidCompID.Value + "\n"
                    End If

                    message = message + "-----------------------------------------------------------------------\n"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('" + message + "');", True)
                    'Started Activity Log Changes
                    Try
                        objUpIns.InsertLog1(Session("UserId").ToString(), "18", "Clicked on Edit Comparison for Comparison Id #" + hidCompID.Value + " (Under Structure #" + Session("SBACaseId").ToString() + ")", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                        objUpIns.InsertLog1(Session("UserId").ToString(), "18", "Got Error message for Comparison Id #" + hidCompID.Value + " selection because Structure(s) " + PropCase + " have been deleted." + " (Under Structure #" + Session("SBACaseId").ToString() + ")", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                    Catch ex As Exception

                    End Try
                    'Ended Activity Log Changes
                    Return False
                End If
            Else
                If flag = "Edit" Then
                    'Started Activity Log Changes
                    Try
                        objUpIns.InsertLog1(Session("UserId").ToString(), "18", "Clicked on Edit Comparison for Comparison Id #" + hidCompID.Value + " (Under Structure #" + Session("SBACaseId").ToString() + ")", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                    Catch ex As Exception

                    End Try
                    'Ended Activity Log Changes
                ElseIf flag = "Share" Then
                    'Started Activity Log Changes
                    Try
                        objUpIns.InsertLog1(Session("UserId").ToString(), "18", "Clicked on Share Access for Comparison Id #" + hidCompID.Value + " (Under Structure #" + Session("SBACaseId").ToString() + ")", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
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
End Class
