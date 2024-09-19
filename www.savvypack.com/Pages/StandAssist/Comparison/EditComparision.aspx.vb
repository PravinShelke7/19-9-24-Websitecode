Imports System.Data
Imports System.Data.OleDb
Imports System.HttpStyleUriParser
Imports System.Web.UI.WebControls
Imports StandGetData
Imports StandUpInsData
Imports System.Drawing
Partial Class Pages_StandAssist_Tools_ComparisonTool
    Inherits System.Web.UI.Page
    Dim GetData As New Selectdata()
#Region "Get Set Variables"
    Dim _lErrorLble As Label
    Dim _strUserName As String
    Dim _strPassword As String
    Dim _iAssumptionId As String
    Dim _btnUpdate As ImageButton
    Dim _btnGlobal As ImageButton



    Public Property ErrorLable() As Label
        Get
            Return _lErrorLble
        End Get
        Set(ByVal Value As Label)
            _lErrorLble = Value
        End Set
    End Property

    Public Property UserName() As String
        Get
            Return _strUserName
        End Get
        Set(ByVal Value As String)
            _strUserName = Value
        End Set
    End Property

    Public Property Password() As String
        Get
            Return _strPassword
        End Get
        Set(ByVal Value As String)
            _strPassword = Value
        End Set
    End Property

    Public Property AssumptionId() As String
        Get
            Return _iAssumptionId
        End Get
        Set(ByVal Value As String)
            Dim obj As New CryptoHelper
            _iAssumptionId = obj.Decrypt(Value)
        End Set
    End Property

    Public DataCnt As Integer
    Public CaseDesp As New ArrayList

#End Region



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("Back") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If
            AssumptionId = Request.QueryString("Id").ToString().Replace("!Plus!", "+").Replace("!Hash!", "#").Replace("!And!", "&")
            If Not IsPostBack Then
                lblTag.Text = "Copyright 1997 - " + Now.Year().ToString() + " Savvypack Corporation."

                bindDetails()
                SetListboxWidth()
                'Started Activity Log Changes
                Try
                    Dim objUpIns As New StandUpInsData.UpdateInsert
                    objUpIns.InsertLog1(Session("UserId").ToString(), "19", "Opened Edit Structures Comparison Page (Under Structure #" + Session("SBACaseId").ToString() + ")", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                Catch ex As Exception

                End Try
                'Ended Activity Log Changes
            End If

            '  Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "alert('" + lstWidth.ToString() + "');", True)
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub SetListboxWidth()
        Try
            'First List Box
            Dim lstWidth1 As Int32 = 0
            For Each items As ListItem In Me.lstRegion1.Items
                If lstWidth1 < items.ToString.Length Then
                    lstWidth1 = items.ToString.Length
                End If
            Next
            If CInt(lstWidth1.ToString()) < 70 Then
                lstRegion1.Width = 450
            Else
                lstRegion1.Width = Nothing
            End If

            'Second List Box
            Dim lstWidth2 As Int32 = 0
            For Each items As ListItem In Me.lstRegion2.Items
                If lstWidth2 < items.ToString.Length Then
                    lstWidth2 = items.ToString.Length
                End If
            Next
            If CInt(lstWidth2.ToString()) < 70 Then
                lstRegion2.Width = 450
            Else
                lstRegion2.Width = Nothing
            End If

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub bindDetails()
        Try
            Dim Dts As New DataSet()
            Dim Dts1, Dts2 As New DataTable()
            Dim Compflag As String = "N"
            If Session("SBACompLib") = "Y" Then
                Compflag = "Y"
            End If


            Dts = GetData.GetSavedCaseAsperUser(Session("USERID").ToString(), AssumptionId)

            Dts1 = GetData.GetEditCases(Session("USERID").ToString(), AssumptionId, "true", Compflag)
            Dts2 = GetData.GetEditCases(Session("USERID").ToString(), AssumptionId, "false", Compflag)



            'Region First
            lstRegion1.DataTextField = "CASEDE"
            lstRegion1.DataValueField = "CaseId"
            lstRegion1.DataSource = Dts1
            lstRegion1.DataBind()

            'Region Second
            lstRegion2.DataTextField = "CASEDE"
            lstRegion2.DataValueField = "CaseId"
            lstRegion2.DataSource = Dts2
            lstRegion2.DataBind()


            txtCName.Text = Dts.Tables(0).Rows(0).Item("DESCRIPTION").ToString()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnFwd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFwd.Click
        Try
            Transfer1(lstRegion1, lstRegion2, False)

            'First List Box
            Dim lstWidth1 As Int32 = 0
            For Each items As ListItem In Me.lstRegion1.Items
                If lstWidth1 < items.ToString.Length Then
                    lstWidth1 = items.ToString.Length
                End If
            Next
            If CInt(lstWidth1.ToString()) < 70 Then
                lstRegion1.Width = 450
            Else
                lstRegion1.Width = Nothing
            End If

            'Second List Box
            Dim lstWidth2 As Int32 = 0
            For Each items As ListItem In Me.lstRegion2.Items
                If lstWidth2 < items.ToString.Length Then
                    lstWidth2 = items.ToString.Length
                End If
            Next
            If CInt(lstWidth2.ToString()) < 70 Then
                lstRegion2.Width = 450
            Else
                lstRegion2.Width = Nothing
            End If
        Catch ex As Exception
            ErrorLable.Text = "Error:btnFwd_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnRew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRew.Click
        Try
            Transfer2(lstRegion1, lstRegion2, False)
            'First List Box
            Dim lstWidth1 As Int32 = 0
            For Each items As ListItem In Me.lstRegion1.Items
                If lstWidth1 < items.ToString.Length Then
                    lstWidth1 = items.ToString.Length
                End If
            Next
            If CInt(lstWidth1.ToString()) < 70 Then
                lstRegion1.Width = 450
            Else
                lstRegion1.Width = Nothing
            End If

            'Second List Box
            Dim lstWidth2 As Int32 = 0
            For Each items As ListItem In Me.lstRegion2.Items
                If lstWidth2 < items.ToString.Length Then
                    lstWidth2 = items.ToString.Length
                End If
            Next
            If CInt(lstWidth2.ToString()) < 70 Then
                lstRegion2.Width = 450
            Else
                lstRegion2.Width = Nothing
            End If
        Catch ex As Exception
            ErrorLable.Text = "Error:btnRew_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
    Protected Sub Transfer1(ByVal Fromlst As ListBox, ByVal Tolst As ListBox, ByVal IsAll As Boolean)
        Dim i As New Integer
        Dim ObjUpIns As New UpdateInsert()
        Try
            Dim CaseIds(10) As String
            Dim cnt As Integer = 1
            For i = 0 To Fromlst.Items.Count - 1
                If Fromlst.Items(i).Selected Then
                    CaseIds(cnt) = Fromlst.Items(i).Value
                    cnt += 1

                    'Started Activity Log Changes
                    Try
                        ObjUpIns.InsertLog1(Session("UserId").ToString(), "19", "Added Structure #" + Fromlst.Items(i).Value + " into Comparison #" + AssumptionId + " (Under Structure #" + Session("SBACaseId").ToString() + ")", Fromlst.Items(i).Value, Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                    Catch ex As Exception

                    End Try
                    'Ended Activity Log Changes
                End If
            Next
            ObjUpIns.EditComparison(AssumptionId, CaseIds, Tolst.Items.Count + 1)
            bindDetails()
        Catch ex As Exception
            ErrorLable.Text = "Error:Transfer:" + ex.Message.ToString() + ""
        End Try
    End Sub
    Protected Sub Transfer2(ByVal Fromlst As ListBox, ByVal Tolst As ListBox, ByVal IsAll As Boolean)
        Dim i As New Integer
        Dim ObjUpIns As New UpdateInsert()
        Try
            Dim CaseIds(10) As String
            Dim cnt As Integer = 1
            For i = 0 To Tolst.Items.Count - 1
                If Not Tolst.Items(i).Selected Then
                    CaseIds(cnt) = Tolst.Items(i).Value
                    cnt += 1
                    
                Else
                    'Started Activity Log Changes
                    Try
                        ObjUpIns.InsertLog1(Session("UserId").ToString(), "19", "Removed Structure #" + Tolst.Items(i).Value + " from Comparison #" + AssumptionId + " (Under Structure #" + Session("SBACaseId").ToString() + ")", Tolst.Items(i).Value, Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                    Catch ex As Exception

                    End Try
                    'Ended Activity Log Changes
                End If
            Next
            ObjUpIns.EditComparison(AssumptionId, CaseIds, 1)
            bindDetails()
        Catch ex As Exception
            ErrorLable.Text = "Error:Transfer:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub imgUpdate_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgUpdate.Click
        Try
            Dim ObjUpIns As New UpdateInsert()
            Dim ds As New DataSet()

            ds = GetComparisionCheck(txtCName.Text, Session("USERID"))
            If ds.Tables(0).Rows.Count > 0 Then

                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Comparison already exists');", True)
                'Started Activity Log Changes
                Try
                    ObjUpIns.InsertLog1(Session("UserId").ToString(), "19", "Got error on Update:Comparison name already exists (Under Structure #" + Session("SBACaseId").ToString() + ")", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                Catch ex As Exception

                End Try
                'Ended Activity Log Changes
            Else
                ObjUpIns.EditComparisonName(AssumptionId, txtCName.Text.Replace("'", "''"))
                bindDetails()
                'Started Activity Log Changes
                Try
                    ObjUpIns.InsertLog1(Session("UserId").ToString(), "19", "Updated Comparison #" + AssumptionId + " (Under Structure #" + Session("SBACaseId").ToString() + ")", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                Catch ex As Exception

                End Try
                'Ended Activity Log Changes
            End If


        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
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
End Class
