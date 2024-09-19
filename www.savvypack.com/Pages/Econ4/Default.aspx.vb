Imports System.Data
Imports System.Data.OleDb
Imports System.HttpStyleUriParser
Imports System.Web.UI.WebControls
Imports E4GetData
Imports E4UpInsData
Partial Class Pages_Econ4_Default
    Inherits System.Web.UI.Page
    Dim GetData As New Selectdata()
#Region "Get Set Variables"
    Dim _lErrorLble As Label
    Dim _btnLogOff As ImageButton



    Public Property ErrorLable() As Label
        Get
            Return _lErrorLble
        End Get
        Set(ByVal Value As Label)
            _lErrorLble = Value
        End Set
    End Property

    Public Property LogOffbtn() As ImageButton
        Get
            Return _btnLogOff
        End Get
        Set(ByVal value As ImageButton)
            _btnLogOff = value
        End Set
    End Property




#End Region

#Region "MastePage Content Variables"
    Protected Sub GetErrorLable()
        ErrorLable = Page.Master.FindControl("lblError")
    End Sub

    Protected Sub GetLogoffBtn()
        LogOffbtn = Page.Master.FindControl("imgLogoff")
        LogOffbtn.Visible = True
        LogOffbtn.PostBackUrl = "~/Universal_loginN/Pages/ULogOff.aspx?Type=E4"
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            GetLogoffBtn()

            If Not IsPostBack Then
                Dim Dtsuser As New DataTable
                Dtsuser = GetData.GetUsernamePassword(Session("ID"))
                Session("UserName") = Dtsuser.Rows(0).Item("Uname").ToString()
                Session("Password") = Dtsuser.Rows(0).Item("Upwd").ToString()

                SetSessions()

                'Filling Case Combo
                Dim DsCases As DataTable '= GetData.GetCases(Session("UserName"), Session("Password"))
                Dim SisterCases As String = String.Empty
                If Session("SavvyModType") <> "1" Then
                    DsCases = GetData.GetCasesE4(Session("USERID"), Session("E4LicAdmin"))
                    For i = 0 To DsCases.Rows.Count - 1
                        SisterCases = SisterCases + DsCases.Rows(i).Item("STATUSID").ToString() + ","
                    Next
                    Session("E4SisterCases") = SisterCases
                Else
                    Session("E4SisterCases") = Nothing
                    DsCases = GetData.GetCases(Session("USERID"))
                End If

                CaseComp.DataSource = DsCases
                CaseComp.DataValueField = "caseID"
                CaseComp.DataTextField = "CaseDe"
                CaseComp.DataBind()

                'Filling Share Case Combo
                SavedSahredCombo()

                'Couser Combo
                Dim DsCoUser As DataTable = GetData.GetCouser(Session("UserName"))
                Coworker.DataSource = DsCoUser
                Coworker.DataValueField = "USERID"
                Coworker.DataTextField = "Username"
                Coworker.DataBind()
            End If


        Catch ex As Exception

        End Try
    End Sub
    Protected Sub SetSessions()
        Dim objGetData As New E1GetData.Selectdata()
        Dim ds As New DataSet()
        Try
            ds = objGetData.GetUserDetails(Session("ID"))
            Session("E4LicAdmin") = ds.Tables(0).Rows(0).Item("ISIADMINLICUSR").ToString()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub StartComp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles StartComp.Click

        Dim Caseid As String
        Dim ID As String = 1
        Dim Name As String = ""
        Name = Trim(ComparisonName.Text)
        Dim CaseArray() As String
        If Name.Length <> 0 Then
            CreateCompError.Visible = False

            'Getting Selected CaseID From Combo
            Caseid = Request.Form("ctl00$Econ4ContentPlaceHolder$CaseComp")
            CaseArray = Split(Caseid, ",")
            If CaseArray.Length < 10 And CaseArray.Length >= 2 Then
                CreateCompError.Visible = False
                Dim UpadateFun As New UpdateInsert()
                Session("AssumptionID") = UpadateFun.AssumptionUpdate(Caseid)

                Displaynote.Visible = False

                Session("WhichForm") = "2"
                UpadateFun.AlterComparison(Name, ID, Session("AssumptionID"), Session("USERID"))
                'Response.Redirect("FinancialManger.aspx")

                Call SavedSahredCombo()

                'Filling Case Combo
                Dim DsCases As DataTable
                If Session("SavvyModType") <> "1" Then
                    DsCases = GetData.GetCasesE4(Session("USERID"), Session("E4LicAdmin"))
                Else
                    DsCases = GetData.GetCases(Session("USERID"))
                End If

                CaseComp.DataSource = DsCases
                CaseComp.DataValueField = "caseID"
                CaseComp.DataTextField = "CaseDe"
                CaseComp.DataBind()
                ComparisonName.Text = ""
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "alert('Comparison#" + Session("AssumptionID").ToString() + " Created Successfully');", True)
                'Response.Write("<script language=JavaScript>window.open('FinancialManger.aspx','new_Win');</script>")

            Else
                CaseComp.Focus()
                If CaseArray.Length > 10 Then
                    CreateCompError.Text = "You cannot select the more than 10 cases"
                    CreateCompError.Visible = True
                Else
                    CreateCompError.Text = "Please atleast select 2 cases for comparison"
                    CreateCompError.Visible = True
                End If
            End If


            'Server.Transfer("FinancialManger.aspx")
        Else
            ComparisonName.Focus()
            CreateCompError.Text = "Please enter the text for new comparison"
            CreateCompError.Visible = True
        End If





    End Sub

    Protected Sub StartComp2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SavedButton.Click

        Session("AssumptionID") = SavedComp.SelectedValue
        Displaynote.Visible = False
        Session("WhichForm") = "2"
        ''Response.Redirect("FinancialManger.aspx")
        'Response.Write("<script language=JavaScript>window.open('Assumptions/CaseManager.aspx','new_Win');</script>")

        Try
            'If CheckAnyDelCase("Start") = True Then
            '    Response.Write("<script language=JavaScript>window.open('Assumptions/CaseManager.aspx','new_Win');</script>")
            'End If
            If Session("SavvyModType") <> "1" Then
                If CheckAnyDelCaseAppr("Start") = True Then
                    Response.Write("<script language=JavaScript>window.open('Assumptions/CaseManager.aspx','new_Win');</script>")
                End If
            Else
                If CheckAnyDelCase("Start") = True Then
                    Response.Write("<script language=JavaScript>window.open('Assumptions/CaseManager.aspx','new_Win');</script>")
                End If
            End If

        Catch ex As Exception
            Response.Write("Assumptions Comparisons.StartComparisonButton_Click.Error" + ex.Message.ToString())
        End Try


    End Sub

    Private Function CheckAnyDelCaseAppr(ByVal flag As String) As Boolean

        Dim DelCases As String = String.Empty
        Dim AllCases() As String
        Dim DelCase As String = String.Empty
        Dim SisCase As String = String.Empty
        Dim PropCase As String = String.Empty
        Dim flagCase As Boolean
        Dim i As Integer
        Try
            AllCases = GetData.Cases(SavedComp.SelectedValue)
            For i = 0 To AllCases.Length - 1
                'DelCase = GetData.GetDelCases(AllCases(i), Session("UserName").ToString())
                If Session("SavvyModType") <> "1" Then
                    DelCase = GetData.GetDelCasesE4(AllCases(i), Session("USERID").ToString(), Session("E4LicAdmin"))
                Else
                    DelCase = GetData.GetDelCases(AllCases(i), Session("USERID").ToString())
                End If
                If (DelCase <> "") Then
                    flagCase = GetData.GetSisterCases(DelCase)
                    If (flagCase = True) Then
                        SisCase = SisCase + DelCase + ","
                    Else
                        PropCase = PropCase + DelCase + ","
                    End If
                    DelCases = DelCases + DelCase + ","
                End If
            Next
            If Session("E4LicAdmin") = "Y" Then
                For i = 0 To AllCases.Length - 1
                    flagCase = GetData.GetSisterCases(AllCases(i))
                    If (flagCase = True) Then
                        SisCase = SisCase + AllCases(i) + ","
                        DelCases = DelCases + AllCases(i) + ","
                    End If

                Next
            End If
            If DelCases <> "" Then
                Dim message As String = ""
                DelCases = DelCases.Remove(DelCases.Length - 1)
                If SisCase <> "" Then
                    SisCase = SisCase.Remove(SisCase.Length - 1)
                End If

                If PropCase <> "" Then
                    PropCase = PropCase.Remove(PropCase.Length - 1)
                End If

                If flag = "Start" Then
                    message = "---------------------------------------------------------------------------"
                    If PropCase <> "" Then
                        message = message + "\nCase(s) " + PropCase + " in this comparison have been deleted, so \n comparison " + SavedComp.SelectedValue + " will no longer function. We recommend you close \n this window and delete comparison " + SavedComp.SelectedValue + "\n"
                        message = message + "---------------------------------------------------------------------------\n"
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('" + message + "');", True)
                        Return False
                    End If
                    If SisCase <> "" Then
                        message = message + "\n Following cases are Sister Case(s) " + SisCase + ".\n" _
                                + "The Sister Case(s) probably need to be upgraded to Approved \n cases in order for the comparision to make sense.\n"
                        message = message + "---------------------------------------------------------------------------\n"
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "confirmMessage('" + message + "');", True)
                        Return False
                    End If

                    'If PropCase = "" Then
                    '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "confirmMessage('" + message + "');", True)
                    '    Return False
                    'Else
                    '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('" + message + "');", True)
                    '    Return False
                    'End If
                    'Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('" + message + "');", True)
                    'Return False
                ElseIf flag = "Edit" Then
                    If PropCase <> "" Then
                        message = message + "\nCase(s) " + PropCase + " in this comparison have been deleted, so \n comparison " + SavedComp.SelectedValue + " will no longer editable. We recommend you close \n this window and delete comparison " + SavedComp.SelectedValue + "\n"
                    End If
                    If SisCase <> "" Then
                        message = message + "\n Following cases are Sister Case(s) " + SisCase + "." _
                                + "The Sister Case(s) probably need to be upgraded to Approved cases in order for the comparision to make sense.\n"
                    End If
                    message = message + "-----------------------------------------------------------------------\n"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('" + message + "');", True)
                    Return False
                End If
            Else
                Return True
            End If
        Catch ex As Exception
            Response.Write("Assumptions Comparisons.Check For el Cases .Error" + ex.Message.ToString())
        End Try

    End Function

    Protected Sub SharedButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SharedButton.Click
        Dim shareusername As String = Coworker.SelectedValue
        Dim ID As String = SharedComp.SelectedValue
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

        Dim SisCase As String = ""
        Dim PropCase As String = ""
        Dim flagCase As Boolean
        Try
            ''TO EXTRACT ALL PERMISSION CASES
            'ds = GetData.GetAllPermissionCases(ID)
            If Session("SavvyModType") <> "1" Then
                ds = GetData.GetAllPermissionCasesE4(ID, "N")
            Else
                ds = GetData.GetAllPermissionCases(ID)
            End If

            ''CHECK USER HAS ACCESS OR NOT
            For i = 0 To ds.Tables(0).Rows.Count - 1
                ds1 = GetData.CheckUser(ds.Tables(0).Rows(i)(0).ToString(), shareusername)
                If (ds1.Tables(0).Rows.Count = 0) Then
                    ListCaseIds = ListCaseIds + ds.Tables(0).Rows(i)(0).ToString() + ","
                    flagCase = GetData.GetSisterCases(ds.Tables(0).Rows(i)(0).ToString())
                    If (flagCase = True) Then
                        SisCase = SisCase + ds.Tables(0).Rows(i)(0).ToString() + ","
                    Else
                        PropCase = PropCase + ds.Tables(0).Rows(i)(0).ToString() + ","
                    End If
                End If
            Next

            'Getting all caseids of Assumptions
            strCase = GetData.GetAssumptionCaseId(SharedComp.SelectedValue)
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

            'If ListCaseIds <> "" Then
            '    ListCaseIds = ListCaseIds.Remove(ListCaseIds.Length - 1)
            '    If strCaseId = "" Then
            '        strMsg = "---------------------------------------------------------------------------------------------\n You can not share this comparison with User " + shareusername + ",\n as following CaseIds are not shared by User: " + ListCaseIds + "\n---------------------------------------------------------------------------------------------\n"
            '    Else
            '        strMsg = "---------------------------------------------------------------------------------------------\n You can not share this comparison with User " + shareusername + ",\n as following CaseIds are not shared by User: " + ListCaseIds + "\n Also following Case is deleted from table:" + strCaseId + "\n---------------------------------------------------------------------------------------------\n"
            '    End If
            '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('" + strMsg + "');", True)
            'Else
            '    If strCaseId <> "" Then
            '        strMsg = "---------------------------------------------------------------------------------------------\n You can not share this comparison with User " + shareusername + ",\n as following Case is deleted from table:" + strCaseId + "\n---------------------------------------------------------------------------------------------\n"
            '        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('" + strMsg + "');", True)

            '    Else
            '        Insert.SharedCase(shareusername, ID)
            '        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('--------------------------------------------------------------------------------------------------------\n Comparison:--" + ID + " is successfully shared with user:-" + shareusername + "\n--------------------------------------------------------------------------------------------------------\n');", True)

            '    End If
            'End If
            If ListCaseIds <> "" Then
                ListCaseIds = ListCaseIds.Remove(ListCaseIds.Length - 1)
                If SisCase.Length > 0 Then
                    SisCase = SisCase.Remove(SisCase.Length - 1)
                End If
                If PropCase.Length > 0 Then
                    PropCase = PropCase.Remove(PropCase.Length - 1)
                End If

                If strCaseId = "" Then
                    strMsg = "---------------------------------------------------------------------------------------------\nYou cannot share this comparison with user:" + Coworker.SelectedItem.Text + "\n"
                    If PropCase <> "" Then
                        strMsg = strMsg + "\n The Following Case(s) is not shared by User: " + PropCase + "\n"
                    End If
                    If SisCase <> "" Then
                        strMsg = strMsg + "\nThe Following Case(s) is a Sister Case(s):" + SisCase + ".\n" _
                                + "The Sister Case(s) needs to be upgraded to an Approved \n Case(s) in order for the sharing to be allowed.\n"
                    End If
                    strMsg = strMsg + "---------------------------------------------------------------------------------------------\n"
                Else
                    strMsg = "---------------------------------------------------------------------------------------------\nYou cannot share this comparison with user:" + Coworker.SelectedItem.Text + "\n"
                    If PropCase <> "" Then
                        strMsg = strMsg + "\n The Following Case(s) is not shared by User: " + PropCase + "\n"
                    End If
                    If SisCase <> "" Then
                        strMsg = strMsg + "\nThe Following Case(s) is a Sister Case(s):" + SisCase + ".\n" _
                             + "The Sister Case(s) needs to be upgraded to an Approved \nCase(s) in order for the sharing to be allowed.\n"
                    End If
                    strMsg = strMsg + "\nThe following case(s) has been deleted:" + strCaseId + "\n"
                    strMsg = strMsg + "---------------------------------------------------------------------------------------------\n"
                End If
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('" + strMsg + "');", True)
            Else
                If strCaseId <> "" Then
                    strMsg = "---------------------------------------------------------------------------------------------\nYou cannot share this comparison with user:" + Coworker.SelectedItem.Text + "\n"
                    strMsg = strMsg + "\nThe following case(s) has been deleted:" + strCaseId + "\n"
                    strMsg = strMsg + "---------------------------------------------------------------------------------------------\n"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('" + strMsg + "');", True)
                Else
                    Insert.SharedCase(shareusername, ID)
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('--------------------------------------------------------------------------------------------------------\n Comparison:--" + ID + " is successfully shared with user:-" + Coworker.SelectedItem.Text + "\n--------------------------------------------------------------------------------------------------------\n');", True)
                End If
            End If

        Catch ex As Exception
            Response.Write("Assumptions Comparisons.SharedButton_Click.Error" + ex.Message.ToString())
        End Try


    End Sub

    Protected Sub DeleteButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DeleteButton.Click
        Try
            Dim ID As String = 3
            Dim Name As String = ""
            Dim Deletefun As New UpdateInsert()
            Deletefun.AlterComparison(Name, ID, SavedComp.SelectedValue, Session("USERID"))
            Dim Dts As New DataSet()
            Call SavedSahredCombo()
        Catch ex As Exception
            Response.Write("Assumptions Comparisons.DeleteButton_Click.Error" + ex.Message.ToString())
        End Try
    End Sub

    Protected Sub RenameButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RenameButton.Click
        Try
            Dim ID As String = 4
            Dim Name As String = Trim(txtrename.Text)
            If Name.Length <> 0 Then
                Dim AssumptionID As String
                AssumptionID = SavedComp.SelectedValue
                Dim Renamefun As New UpdateInsert()
                Renamefun.AlterComparison(Name, ID, AssumptionID, Session("USERID"))
                Dim Dts As New DataSet()

                Call SavedSahredCombo()

                txtrename.Text = ""
                RenameError.Visible = False

            Else
                RenameError.Text = "Please enter the text for renaming comparison"
                RenameError.Visible = True

            End If


        Catch ex As Exception
            Response.Write("Assumptions Comparisons.RenameButton_Click.Error" + ex.Message.ToString())
        End Try
    End Sub

    Protected Sub SavedSahredCombo()
        Try
            Dim Dts As New DataSet()
            Dts = GetData.GetSavedCaseAsperUser(Session("USERID").ToString(), "-1")
            If Dts.Tables(0).Rows.Count = 0 Then
                nodatatr1.Visible = True
                nodatatr2.Visible = True
                ddtr1.Visible = False
                ddtr2.Visible = False
                ddtr3.Visible = False
                headingtr1.Visible = False
                headingtr2.Visible = False
                buttontr1.Visible = False
                buttontr11.Visible = False
                buttontr3.Visible = False
            Else
                nodatatr1.Visible = False
                nodatatr2.Visible = False
                ddtr1.Visible = True
                ddtr2.Visible = True
                ddtr3.Visible = True
                headingtr1.Visible = True
                headingtr2.Visible = True
                buttontr1.Visible = True
                buttontr11.Visible = True
                buttontr3.Visible = True
                'Saved Comparison Combo

                Dts = GetData.GetSavedCaseAsperUser(Session("USERID").ToString(), "-1")
                SavedComp.DataSource = Dts
                SavedComp.DataValueField = "AssumptionId"
                SavedComp.DataTextField = "Des"
                SavedComp.DataBind()

                'Saved Shared Comparison Combo

                SharedComp.DataSource = Dts
                SharedComp.DataValueField = "AssumptionId"
                SharedComp.DataTextField = "Des"
                SharedComp.DataBind()

            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnEditComp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEditComp.Click
        Try
            If CheckAnyDelCase("Edit") = True Then
                Dim obj As New CryptoHelper
                Response.Redirect("Tools/EditComparision.aspx?Id=" + obj.Encrypt(SavedComp.SelectedItem.Value))
            End If
        Catch ex As Exception
            Response.Write("Assumptions Comparisons.EditButton_Click.Error" + ex.Message.ToString())
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
        Try
            AllCases = GetData.Cases(SavedComp.SelectedValue)
            For i = 0 To AllCases.Length - 1
                If Session("SavvyModType") <> "1" Then
                    DelCase = GetData.GetDelCasesE4(AllCases(i), Session("USERID").ToString(), Session("E4LicAdmin"))
                Else
                    DelCase = GetData.GetDelCases(AllCases(i), Session("USERID").ToString())
                End If
                If (DelCase <> "") Then
                    flagCase = GetData.GetSisterCases(DelCase)
                    If (flagCase = True) Then
                        SisCase = SisCase + DelCase + ","
                    Else
                        PropCase = PropCase + DelCase + ","
                    End If
                    DelCases = DelCases + DelCase + ","
                End If
            Next
            If DelCases <> "" Then
                Dim message As String = ""
                DelCases = DelCases.Remove(DelCases.Length - 1)
                If SisCase <> "" Then
                    SisCase = SisCase.Remove(SisCase.Length - 1)
                End If

                If PropCase <> "" Then
                    PropCase = PropCase.Remove(PropCase.Length - 1)
                End If

                If flag = "Start" Then
                    message = "---------------------------------------------------------------------------"
                    If PropCase <> "" Then
                        message = message + "\nCase(s) " + PropCase + " in this comparison have been deleted, so \n comparison " + SavedComp.SelectedValue + " will no longer function. We recommend you close \n this window and delete comparison " + SavedComp.SelectedValue + "\n"
                    End If
                    If SisCase <> "" Then
                        message = message + "\n Following cases are Sister Case(s) " + SisCase + ".\n" _
                                + "The Sister Case(s) probably need to be upgraded to \n Approved cases in order for the comparision to make sense.\n"
                    End If
                    message = message + "---------------------------------------------------------------------------\n"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('" + message + "');", True)
                    'Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('--------------------------------------------------------------------------------\n Case(s) " + DelCases + " in this comparison have been deleted, so \n comparison " + SavedComp.SelectedValue + " will no longer function. Please close \n this window and delete comparison " + SavedComp.SelectedValue + " \n ------------------------------------------------------------------------------\n');", True)
                    Return False
                ElseIf flag = "Edit" Then
                    message = "---------------------------------------------------------------------------"
                    If PropCase <> "" Then
                        message = message + "\nCase(s) " + PropCase + " in this comparison have been deleted, so \n comparison " + SavedComp.SelectedValue + " will no longer editable. We recommend you close \n this window and delete comparison " + SavedComp.SelectedValue + "\n"
                    End If
                    'If SisCase <> "" Then
                    '    message = message + "\n Following cases are Sister Case(s) " + SisCase + ".\n" _
                    '            + "The Sister Case(s) probably need to be upgraded to \n Approved cases in order for the comparision to make sense.\n"
                    'End If
                    message = message + "---------------------------------------------------------------------------\n"
                    'Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('" + message + "');", True)
                    'Return False
                    If (PropCase = "") Then
                        Return True
                    Else
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('" + message + "');", True)
                        Return False
                    End If
                End If
            Else
                Return True
            End If
        Catch ex As Exception
            Response.Write("Assumptions Comparisons.Check For el Cases .Error" + ex.Message.ToString())
        End Try

    End Function

    Protected Sub CaseComp_DataBound(ByVal sender As Object, ByVal e As EventArgs)
        Dim GetData As New E4GetData.Selectdata()
        Dim DsCases As New DataTable
        Dim i As Integer = 0
        If Session("SavvyModType") <> "1" Then
            If Session("E4LicAdmin") <> "Y" Then
            Else
                DsCases = GetData.GetCasesE4(Session("USERID"), Session("E4LicAdmin"))
                For Each li As ListItem In CaseComp.Items
                    If (DsCases.Rows(i).Item("STATUSID") = "4") Then
                        Dim ccItem As ListItem = CaseComp.Items(i)
                        If ccItem IsNot Nothing Then
                            ccItem.Attributes.Add("disabled", "")
                        End If
                    End If
                    i += 1
                Next
            End If

        End If
    End Sub
End Class
