Imports System.Data
Imports System.Web.UI.WebControls
Imports Echem1GetData
Imports Echem1UpInsData
Partial Class Pages_Echem1_Tools_ManageGroups
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("Back") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If
            txtKey.Attributes.Add("onKeyDown", "return clickButton(event,'" + btnSearch.ClientID + "')")
            If Not IsPostBack Then
                GetPCaseDetails()
                GetGroupsForCase()
                hvUserGrd.Value = "0"

            End If
        Catch ex As Exception
            lblError.Text = "Page_Load:" + ex.Message
        End Try
    End Sub
    Protected Sub GetPCaseDetails()
        Dim objGetData As New Echem1GetData.Selectdata
        Dim ds As New DataSet
        Dim dsGrps As New DataSet
        Dim dtGrps As New DataTable
        Dim j As Integer
        Dim grpCount As Integer = 0
        Try
            ds = objGetData.GetPCaseGrpDetails(Session("Echem1UserName").ToString(), Session("USERID").ToString(), txtKey.Text.Trim().Replace("'", "''").ToString())
            dtGrps = ds.Tables(0).DefaultView.ToTable(True, "GROUPID")

            For j = 0 To dtGrps.Rows.Count - 1
                If dtGrps.Rows(j).Item("GROUPID").ToString() <> "0" Then
                    grpCount += 1
                End If
            Next
            lblGroupCnt.Text = grpCount

            If ds.Tables(0).Rows.Count > 0 Then
                lblCF.Text = ds.Tables(0).Rows.Count
                trmsg.Visible = False
            Else
                lblCF.Text = 0
                trmsg.Visible = True
                lblmsg.Text = "No Data Available"
            End If

            Session("Echem1UsersData") = ds
            grdCase.DataSource = ds
            grdCase.DataBind()

        Catch ex As Exception
            lblError.Text = "GetPCaseDetails:" + ex.Message
        End Try
    End Sub
    Public Function GetPCaseGrpDetails(ByVal UserName As String, ByVal UserID As String, ByVal keyWord As String) As DataSet
        Dim Dts As New DataSet()
        Dim odbUtil As New DBUtil()
        Dim StrSql As String = String.Empty
        Dim Echem1Connection As String = System.Configuration.ConfigurationManager.AppSettings("Echem1ConnectionString")
        Try
            ''StrSql = "SELECT AA.CASEID,CASEDE1,CASEDE2 FROM ECHEM1.PERMISSIONSCASES AA LEFT OUTER JOIN GROUPCASES BB ON AA.CASEID=BB.CASEID "
            ''StrSql = StrSql + "LEFT OUTER Join GROUPS CC ON BB.GROUPID=CC.Groupid "

            'StrSql = "SELECT AA.CASEID,CASEDE1,CASEDE2,CASEDE3,  "
            'StrSql = StrSql + "(AA.CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDES, "
            'StrSql = StrSql + "CASE WHEN NVL(CC.GROUPID,'0')='0' THEN 'None' ELSE CC.DES1 END AS GROUPNAME, "
            'StrSql = StrSql + "NVL(CC.GROUPID,'0') AS GROUPID "
            'StrSql = StrSql + "FROM ECHEM1.PERMISSIONSCASES AA "
            'StrSql = StrSql + "LEFT OUTER JOIN GROUPCASES BB ON AA.CASEID=BB.CASEID "
            'StrSql = StrSql + "LEFT OUTER JOIN GROUPS CC ON BB.GROUPID=CC.GROUPID "


            StrSql = "SELECT GROUPNAME,GROUPID,CASEID,CASEDE1,CASEDE2,CASEDE3,CASEDES,CREATIONDATE,SERVERDATE "
            StrSql = StrSql + " FROM "
            StrSql = StrSql + " ( "
            StrSql = StrSql + "SELECT CASE WHEN NVL(GROUPS.GROUPID,'0')='0' THEN 'None' ELSE GROUPS.DES1 END AS GROUPNAME,  "
            StrSql = StrSql + "NVL(GROUPS.GROUPID,'0') AS GROUPID, "
            StrSql = StrSql + "PC.CASEID, "
            StrSql = StrSql + "PC.CASEDE1, "
            StrSql = StrSql + "PC.CASEDE2, "
            StrSql = StrSql + "PC.CASEDE3, "
            StrSql = StrSql + "(PC.CASEID||'. PACKAGE FORMAT= '||PC.CASEDE1||' UNIQUE FEATURES= '||PC.CASEDE2)CASEDES, "

            StrSql = StrSql + " PC.CREATIONDATE, "
            StrSql = StrSql + "CASE WHEN PC.CREATIONDATE-PC.SERVERDATE =0 THEN 'NA' ELSE to_char(PC.SERVERDATE,'mm/dd/yyyy hh:mi:ss AM'  ) END  SERVERDATE "


            StrSql = StrSql + "FROM GROUPS "
            StrSql = StrSql + "INNER JOIN GROUPCASES "
            StrSql = StrSql + "ON GROUPCASES.GROUPID=GROUPS.Groupid "
            StrSql = StrSql + "AND GROUPS.USERID=" + UserID + " "
            StrSql = StrSql + "RIGHT OUTER JOIN PERMISSIONSCASES PC "
            StrSql = StrSql + "ON PC.CASEID=GROUPCASES.CASEID "
            StrSql = StrSql + "WHERE PC.USERID =" + UserID + " "
            StrSql = StrSql + "ORDER BY CASEID DESC "
            StrSql = StrSql + " ) "

            StrSql = StrSql + "WHERE UPPER(CASEDE1) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
            StrSql = StrSql + "OR UPPER(CASEDE2) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
            StrSql = StrSql + "OR UPPER(CASEID) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
            StrSql = StrSql + "OR UPPER(CASEDE3) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
            StrSql = StrSql + "OR UPPER(GROUPNAME) LIKE '%" + keyWord.ToUpper().Trim() + "%' "

            Dts = odbUtil.FillDataSet(StrSql, Echem1Connection)
            Return Dts
        Catch ex As Exception
            Throw New Exception("Echem1GetData:GetPCaseGrpDetails:" + ex.Message.ToString())
            Return Dts
        End Try
    End Function
    Public Function GetPCaseGrpDetails1(ByVal UserName As String, ByVal UserID As String, ByVal keyWord As String) As DataSet
        Dim Dts As New DataSet()
        Dim odbUtil As New DBUtil()
        Dim StrSql As String = String.Empty
        Dim Econ2Connection As String = System.Configuration.ConfigurationManager.AppSettings("Econ2ConnectionString")
        Dim Echem1Connection As String = System.Configuration.ConfigurationManager.AppSettings("Echem1ConnectionString")
        Try
            StrSql = "SELECT GROUPNAME,GROUPID,CASEID,CASEDE1,CASEDE2,CASEDE3,CASEDES,CREATIONDATE,SERVERDATE "
            StrSql = StrSql + " FROM "
            StrSql = StrSql + " ( "
            StrSql = StrSql + "SELECT CASE WHEN NVL(GROUPS.GROUPID,'0')='0' THEN 'None' ELSE GROUPS.DES1 END AS GROUPNAME,  "
            StrSql = StrSql + "NVL(GROUPS.GROUPID,'0') AS GROUPID, "
            StrSql = StrSql + "PC.CASEID, "
            StrSql = StrSql + "PC.CASEDE1, "
            StrSql = StrSql + "PC.CASEDE2, "
            StrSql = StrSql + "PC.CASEDE3, "
            StrSql = StrSql + "(PC.CASEID||'. PACKAGE FORMAT= '||PC.CASEDE1||' UNIQUE FEATURES= '||PC.CASEDE2)CASEDES, "

            StrSql = StrSql + " PC.CREATIONDATE, "
            StrSql = StrSql + "CASE WHEN PC.CREATIONDATE-PC.SERVERDATE =0 THEN 'NA' ELSE to_char(PC.SERVERDATE,'mm/dd/yyyy hh:mi:ss AM'  ) END  SERVERDATE "


            StrSql = StrSql + "FROM GROUPS "
            StrSql = StrSql + "INNER JOIN GROUPCASES "
            StrSql = StrSql + "ON GROUPCASES.GROUPID=GROUPS.Groupid "
            StrSql = StrSql + "AND GROUPS.USERID=" + UserID + " "
            StrSql = StrSql + "RIGHT OUTER JOIN PERMISSIONSCASES PC "
            StrSql = StrSql + "ON PC.CASEID=GROUPCASES.CASEID "
            StrSql = StrSql + "WHERE UPPER(USERNAME) ='" + UserName.ToUpper().Trim() + "' "
            StrSql = StrSql + "ORDER BY CASEID DESC "
            StrSql = StrSql + " ) "

            StrSql = StrSql + "WHERE UPPER(CASEDE1) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
            StrSql = StrSql + "OR UPPER(CASEDE2) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
            StrSql = StrSql + "OR UPPER(CASEID) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
            StrSql = StrSql + "OR UPPER(CASEDE3) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
            StrSql = StrSql + "OR UPPER(GROUPNAME) LIKE '%" + keyWord.ToUpper().Trim() + "%' "

            Dts = odbUtil.FillDataSet(StrSql, Econ2Connection)
            Return Dts
        Catch ex As Exception
            Throw New Exception("E2GetData:GetPCaseGrpDetails:" + ex.Message.ToString())
            Return Dts
        End Try
    End Function
    Protected Sub btnCreateGrp_Click1(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim Caseid As String
            Dim Name As String = ""
            Name = Trim(txtGroupDe1.Text)
            Dim CaseArray() As String
            Dim objUpdateData As New Echem1UpInsData.UpdateInsert
            Dim objGetData As New Echem1GetData.Selectdata()
            Dim dt As New DataSet()
            Dim message As String
            Dim message1 As String
            'Validating Cases
            Caseid = Request.Form("lstCases")
            dt = objGetData.ValiDateGroupcases(Caseid, Session("UserId").ToString())

            If dt.Tables(0).Rows.Count > 0 Then
                message = "--------------------------------------------------------------------\n"
                message1 = message + "Cases can only be included in one group.\n"
                For i = 0 To dt.Tables(0).Rows.Count - 1
                    message1 = message1 + "      Cases " + dt.Tables(0).Rows(i).Item("CaseID").ToString() + " is included in group " + dt.Tables(0).Rows(i).Item("GroupName").ToString()
                    If i = dt.Tables(0).Rows.Count - 1 Then
                        message1 = message1 + "\n" + message + "\n"
                    Else
                        message1 = message1 + "\n"
                    End If
                Next
                message = message + "--------------------------------------------------------------------\n"
                trCreate.Style.Add("Display", "Inline")
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "New_WinCopy", "alert('" + message1 + "');", True)
            Else
                If Name.Length <> 0 Then
                    CaseArray = Split(Caseid, ",")
                    objUpdateData.AddGroup(txtGroupDe1.Text.Trim().Replace("'", "''").ToString(), txtGroupDe2.Text.Trim().Replace("'", "''").ToString(), Session("UserId"), CaseArray)

                    txtGroupDe1.Text = ""
                    txtGroupDe2.Text = ""
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Group created successfully');", True)
                    trCreate.Style.Add("Display", "none")
                End If
            End If

        Catch ex As Exception
            lblError.Text = "btnCreateGrp_Click:" + ex.Message
        End Try
    End Sub
    Protected Sub btnCreateGrp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreateGrp.Click
        Try
            Dim ID As String = 1
            Dim Name As String = ""
            Name = Trim(txtGroupDe1.Text)
            Dim objUpdateData As New Echem1UpInsData.UpdateInsert
            Dim objGetData As New Echem1GetData.Selectdata()
            Dim ds As New DataSet()

            If Name.Length <> 0 Then
                objUpdateData.AddGroupName(txtGroupDe1.Text.Trim().Replace("'", "''").ToString(), txtGroupDe2.Text.Trim().Replace("'", "''").ToString(), Session("UserId"))
                'ds = objGetData.GetGroupIDByUSer(Session("USERID").ToString())
                'If ds.Tables(0).Rows.Count > 0 Then
                '    lblGroupCnt.Text = ds.Tables(0).Rows.Count
                'Else
                '    lblGroupCnt.Text = 0
                'End If
                txtGroupDe1.Text = ""
                txtGroupDe2.Text = ""
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Group created successfully');", True)
                trCreate.Style.Add("Display", "none")
            End If

        Catch ex As Exception
            lblError.Text = "btnCreateGrp_Click:" + ex.Message
        End Try
    End Sub

    Protected Sub btnGlobalManager_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnGlobalManager.Click
        Try
            Session("Echem1GroupID") = Nothing
            Response.Redirect("~/Pages/Echem1/Default.aspx")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub grdCase_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdCase.Sorting
        Try
            Dim Dts As New DataSet
            Dim dv As DataView
            Dim numberDiv As Integer
            numberDiv = Convert.ToInt16(hvUserGrd.Value.ToString())
            Dts = Session("Echem1UsersData")
            dv = Dts.Tables(0).DefaultView

            If ((numberDiv Mod 2) = 0) Then
                dv.Sort = e.SortExpression + " " + "DESC"
            Else
                dv.Sort = e.SortExpression + " " + "ASC"
            End If
            numberDiv += 1
            hvUserGrd.Value = numberDiv.ToString()
            grdCase.DataSource = dv
            grdCase.DataBind()

            GetGroupsForCase()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub GetGroupsForCase()
        Dim linkBut As New LinkButton
        Dim lblGrpId As New Label
        Dim lblCaseID As New Label
        For Each Gr As GridViewRow In grdCase.Rows
            lblGrpId = grdCase.Rows(Gr.RowIndex).FindControl("lblGroupID")
            lblCaseID = grdCase.Rows(Gr.RowIndex).FindControl("lblCaseID")
            linkBut = grdCase.Rows(Gr.RowIndex).FindControl("lnkGroId")
            linkBut.Attributes.Add("onclick", "return OpenGroupPopup('../Popup/GroupDetails.aspx?groupId=" + lblGrpId.Text.Trim() + "&CaseID=" + lblCaseID.Text.Trim() + "');")
        Next
    End Sub
    Protected Sub grdCase_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdCase.PageIndexChanging
        Try
            grdCase.PageIndex = e.NewPageIndex
            bindCaseGridSession()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub bindCaseGridSession()
        Dim ds As New DataSet()
        Try
            ds = Session("Echem1UsersData")
            grdCase.DataSource = ds
            grdCase.DataBind()
            GetGroupsForCase()
        Catch ex As Exception
            Response.Write("Error:bindCaseGridSession:" + ex.Message.ToString())
        End Try
    End Sub

    Protected Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            GetPCaseDetails()
            GetGroupsForCase()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            GetPCaseDetails()
            GetGroupsForCase()
        Catch ex As Exception
            Throw New Exception("btnSearch_Click" + ex.Message)
        End Try
    End Sub
End Class
