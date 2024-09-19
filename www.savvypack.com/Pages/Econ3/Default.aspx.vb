Imports System.Data
Imports System.Data.OleDb
Imports System.HttpStyleUriParser
Imports System.Web.UI.WebControls
Imports E3GetData
Imports E3UpInsData
Imports AjaxControlToolkit
Imports System.Collections.Generic

Partial Class Pages_Econ3_Default
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
        LogOffbtn.PostBackUrl = "~/Universal_loginN/Pages/ULogOff.aspx?Type=E3"
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            GetLogoffBtn()
            'Response.Redirect("Assumptions/Extrusion.aspx", False)
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
                    DsCases = GetData.GetCasesE3(Session("USERID"), Session("E3LicAdmin"))
                    For i = 0 To DsCases.Rows.Count - 1
                        SisterCases = SisterCases + DsCases.Rows(i).Item("STATUSID").ToString() + ","
                    Next
                    Session("E3SisterCases") = SisterCases
                Else
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

                Session("Mod") = "E3"
                Session("EquipMod") = "E3"

            End If
            If tabNew.ActiveTabIndex = "0" Then
                SavedComp.Visible = True
                '    test.Style.Add("display", "none")
                'test.Style.Add("display", "inline")
                't1.Visible = True
            Else
                't1.Visible = False
            End If

            'If tabNew.ActiveTabIndex = "1" Then
            '    ' SavedComp.Visible = False
            '    test.Style.Add("display", "none")


            'End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub tabNew_ActiveTabChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabNew.ActiveTabChanged
        If tabNew.ActiveTabIndex = "1" Then

            'test.Style.Add("display", "none")



        End If

    End Sub

    Protected Sub SetSessions()
        Dim objGetData As New E1GetData.Selectdata()
        Dim ds As New DataSet()
        Try
            ds = objGetData.GetUserDetails(Session("ID"))
            Session("E3LicAdmin") = ds.Tables(0).Rows(0).Item("ISIADMINLICUSR").ToString()
            If ds.Tables(0).Rows(0).Item("USERROLE").ToString() = "AADMIN" Then
                btnView.Visible = True
            Else
                btnView.Visible = False
            End If
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
            Caseid = Request.Form("ctl00$Econ3ContentPlaceHolder$tabNew$modelanalysis$CaseComp")
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
                Dim DsCases As DataTable '= GetData.GetCases(Session("UserName"), Session("Password"))
                If Session("SavvyModType") <> "1" Then
                    DsCases = GetData.GetCasesE3(Session("USERID"), Session("E3LicAdmin"))
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
        Try
            'If CheckAnyDelCase("Start") = True Then
            '    Response.Write("<script language=JavaScript>window.open('Assumptions/CaseManager.aspx','new_Win');</script>")
            'End If
            If Session("SavvyModType") <> "1" Then
                If CheckAnyDelCaseAppr("Start") = True Then
                    ' Page.ClientScript.RegisterStartupScript(Me.GetType(), "RLM", "ShowPopupFP('Assumptions/CaseManager.aspx');", True)
                    ' Response.Write("<script language=JavaScript>window.open('Assumptions/CaseManager.aspx','new_Win');</script>")
                    ScriptManager.RegisterStartupScript(Page, Me.GetType(), "NewWindow", "OpenNewWindow('Assumptions/CaseManager.aspx');", True)
                End If
            Else
                If CheckAnyDelCase("Start") = True Then
                    'Page.ClientScript.RegisterStartupScript(Me.GetType(), "RLM", "ShowPopupFP('Assumptions/CaseManager.aspx');", True)
                    'Response.Write("<script language=JavaScript>window.open('Assumptions/CaseManager.aspx','new_Win');</script>")
                    ScriptManager.RegisterStartupScript(Page, Me.GetType(), "NewWindow", "OpenNewWindow('Assumptions/CaseManager.aspx');", True)
                End If
            End If
        Catch ex As Exception
            Response.Write("Assumptions Comparisons.StartComparisonButton_Click.Error" + ex.Message.ToString())
        End Try
    End Sub

    Private Function CheckAnyDelCase1(ByVal flag As String) As Boolean

        Dim DelCases As String = String.Empty
        Dim AllCases() As String
        Dim DelCase As String = String.Empty
        Dim i As Integer
        Try
            AllCases = GetData.Cases(SavedComp.SelectedValue)
            For i = 0 To AllCases.Length - 1
                DelCase = GetData.GetDelCases(AllCases(i), Session("USERID").ToString())
                If (DelCase <> "") Then
                    DelCases = DelCases + DelCase + ","
                End If
            Next
            If DelCases <> "" Then
                DelCases = DelCases.Remove(DelCases.Length - 1)
                If flag = "Start" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('--------------------------------------------------------------------------------\n Case(s) " + DelCases + " in this comparison have been deleted, so \n comparison " + SavedComp.SelectedValue + " will no longer function. Please close \n this window and delete comparison " + SavedComp.SelectedValue + " \n ------------------------------------------------------------------------------\n');", True)
                    Return False
                ElseIf flag = "Edit" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('--------------------------------------------------------------------------------\n Case(s) " + DelCases + " in this comparison have been deleted, so \n comparison " + SavedComp.SelectedValue + " will no longer editable. Please close \n this window and delete comparison " + SavedComp.SelectedValue + " \n ------------------------------------------------------------------------------\n');", True)
                    Return False
                End If
            Else
                Return True
            End If
        Catch ex As Exception
            Response.Write("Assumptions Comparisons.Check For el Cases .Error" + ex.Message.ToString())
        End Try

    End Function

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
                    DelCase = GetData.GetDelCasesE3(AllCases(i), Session("USERID").ToString(), Session("E3LicAdmin"))
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
                    'Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('--------------------------------------------------------------------------------\n Case(s) " + DelCases + " in this comparison have been deleted, so \n comparison " + hidCompID.Value + " will no longer function. Please close \n this window and delete comparison " + hidCompID.Value + " \n ------------------------------------------------------------------------------\n');", True)
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('" + message + "');", True)

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
            Throw ex
        End Try
    End Function

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
                    DelCase = GetData.GetDelCasesE3(AllCases(i), Session("USERID").ToString(), Session("E3LicAdmin"))
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
            If Session("E3LicAdmin") = "Y" Then
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
                        ' Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('" + message + "');", True)
                        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "New_WinCreate", "alert('" + message + "');", True)
                        Return False
                    End If
                    If SisCase <> "" Then
                        message = message + "\n Following cases are Sister Case(s) " + SisCase + ".\n" _
                               + "The Sister Case(s) probably need to be upgraded to \n Approved cases in order for the comparision to make sense.\n"
                        message = message + "---------------------------------------------------------------------------\n"
                        ' Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "confirmMessage('" + message + "');", True)
                        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "New_WinRename", "confirmMessage('" + message + "');", True)
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
                    message = message + "---------------------------------------------------------------------------\n"
                    If PropCase <> "" Then
                        message = message + "\nCase(s) " + PropCase + " in this comparison have been deleted, so \n comparison " + SavedComp.SelectedValue + " will no longer editable. We recommend you close \n this window and delete comparison " + SavedComp.SelectedValue + "\n"
                    End If
                    'If SisCase <> "" Then
                    '    message = message + "\n Following cases are Sister Case(s) " + SisCase + "." _
                    '            + "The Sister Case(s) probably need to be upgraded to Approved cases in order for the comparision to make sense.\n"
                    'End If
                    message = message + "---------------------------------------------------------------------------\n"
                    'Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('" + message + "');", True)
                    'Return False
                    If (PropCase = "") Then
                        Return True
                    Else
                        'Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('" + message + "');", True)
                        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "New_WinCreate", "alert('" + message + "');", True)

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
                ds = GetData.GetAllPermissionCasesE3(ID, "N")
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
            '    If SisCase.Length > 0 Then
            '        SisCase = SisCase.Remove(SisCase.Length - 1)
            '    End If
            '    If PropCase.Length > 0 Then
            '        PropCase = PropCase.Remove(PropCase.Length - 1)
            '    End If
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

                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('" + strMsg + "');", True)
                ScriptManager.RegisterStartupScript(Page, Me.GetType(), "New_WinCreate", "alert('" + strMsg + "');", True)

            Else
                If strCaseId <> "" Then
                    strMsg = "---------------------------------------------------------------------------------------------\nYou cannot share this comparison with user:" + Coworker.SelectedItem.Text + "\n"
                    strMsg = strMsg + "\nThe following case(s) has been deleted:" + strCaseId + "\n"
                    strMsg = strMsg + "---------------------------------------------------------------------------------------------\n"
                    'Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('" + strMsg + "');", True)
                    ScriptManager.RegisterStartupScript(Page, Me.GetType(), "New_WinCreate", "alert('" + strMsg + "');", True)

                Else
                    Insert.SharedCase(shareusername, ID)
                    ' Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('--------------------------------------------------------------------------------------------------------\n Comparison:--" + ID + " is successfully shared with user:-" + Coworker.SelectedItem.Text + "\n--------------------------------------------------------------------------------------------------------\n');", True)
                    ScriptManager.RegisterStartupScript(Page, Me.GetType(), "New_WinCreate", "alert('--------------------------------------------------------------------------------------------------------\n Comparison:--" + ID + " is successfully shared with user:-" + Coworker.SelectedItem.Text + "\n--------------------------------------------------------------------------------------------------------\n');", True)

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
                'Dts = GetData.GetSavedCaseAsperUser(Session("UserName").ToString())

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
                'Response.Redirect("Tools/EditComparision.aspx?Id=" + obj.Encrypt(SavedComp.SelectedItem.Value))
                ScriptManager.RegisterStartupScript(Page, Me.GetType(), "NewWindow", "OpenNewWindow(' Tools/EditComparision.aspx?Id=" + obj.Encrypt(SavedComp.SelectedItem.Value) + "');", True)

            End If
        Catch ex As Exception
            Response.Write("Assumptions Comparisons.btnEditComp_Click.Error" + ex.Message.ToString())
        End Try
    End Sub

    Protected Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            If hidselgrpID.Value <> "" Then
                lnkSelGrpE3.Text = hidselgrpID.Value
                btnView.Enabled = True
                Session("BulkCases") = hidselgrpID.Value
            Else
                lnkSelGrpE3.Text = "Nothing Selected"
                btnView.Enabled = False
            End If
            BindCaseCombo()
        Catch ex As Exception
            Response.Write("btnRefresh_Click:" + ex.Message.ToString())
        End Try
    End Sub

    Protected Sub BindCaseCombo()
        Try
            'Filling Case Combo
            Dim DsCases As DataTable
            Dim SisterCases As String = String.Empty
            If Session("SavvyModType") <> "1" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Alert", "alert('This facility isn't available for current user.');", True)
            Else
                If hidselgrpID.Value <> "" Then
                    DsCases = GetData.GetCasesByGrpIDs(Session("USERID"), hidselgrpID.Value)
                Else
                    DsCases = GetData.GetCases(Session("USERID"))
                End If
                CaseComp.DataSource = DsCases
                CaseComp.DataValueField = "caseID"
                CaseComp.DataTextField = "CaseDe"
                CaseComp.DataBind()

                'Filling Share Case Combo
                'SavedSahredCombo()
            End If

        Catch ex As Exception
            Response.Write("BindCaseCombo:" + ex.Message.ToString())
        End Try
    End Sub

End Class
