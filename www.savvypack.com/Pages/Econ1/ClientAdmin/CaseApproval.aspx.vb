Imports Microsoft.VisualBasic
Imports System.Data.OleDb
Imports System.Data
Imports E1GetData
Imports E1UpInsData
Partial Class ClientAdmin_CaseApproval
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            lblTag.Text = "Copyright 1997 - " + Now.Year().ToString() + " SavvyPack Corporation"
            txtKey.Attributes.Add("onKeyDown", "return clickButton(event,'" + btnSearch.ClientID + "')")

            If Session("Back") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If

            ' BindTable()
            If Not IsPostBack Then
                hvUserGrd.Value = "0"
                hidCaseid1.Value = "0"
                hidCaseid2.Value = "0"
                hidGroupID.Value = "0"
                bindCaseGrid()
                GetCaseCount()
            End If

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub GetCaseCount()
        Dim objGetData As New E1GetData.Selectdata()
        Dim ds As New DataSet()
        Dim dv4, dv3 As DataView
        Dim dt4, dt3 As DataTable
        Try
            ds = objGetData.GetBemisCaseCount()

            'Approve Count
            dv4 = ds.Tables(0).DefaultView
            dv4.RowFilter = "CODE = 'APPROVE'"
            dt4 = dv4.ToTable()
            hidApprove.Value = dt4.Rows(0).Item("Count").ToString()

            'DisApprove Count
            dv3 = ds.Tables(0).DefaultView
            dv3.RowFilter = "CODE = 'DISAPPROVE'"
            dt3 = dv3.ToTable()
            hidDisApprove.Value = dt4.Rows(0).Item("Count").ToString()


        Catch ex As Exception

        End Try
    End Sub

#Region "Inner"


    Protected Sub bindCaseGrid()
        Dim objGetData As New E1GetData.Selectdata()
        Dim ds As New DataSet()
        Dim dsGrps As New DataSet
        Dim dtGrps As New DataTable
        Dim j As Integer
        Dim grpCount As Integer = 0

        Try
            ds = objGetData.GetLicenseCasesDesBemis(Session("UserName"), "0", "E1", txtKey.Text, hidGroupID.Value)
            dtGrps = ds.Tables(0).DefaultView.ToTable(True, "GROUPID")


            For j = 0 To dtGrps.Rows.Count - 1
                If dtGrps.Rows(j).Item("GROUPID").ToString() <> "0" Then
                    grpCount += 1
                End If
            Next

            Session("UsersData") = ds
            grdCase.DataSource = ds
            grdCase.DataBind()
            GetStatusForCase()
            lblCF.Text = ds.Tables(0).Rows.Count

        Catch ex As Exception
            Response.Write("Error:bindCaseGrid:" + ex.Message.ToString())
        End Try
    End Sub

    Protected Sub bindCaseGridSession()
        Dim objGetData As New E1GetData.Selectdata()
        Dim ds As New DataSet()
        Try
            ds = Session("UsersData")
            grdCase.DataSource = ds
            grdCase.DataBind()
            GetStatusForCase()

        Catch ex As Exception
            Response.Write("Error:bindCaseGrid:" + ex.Message.ToString())
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
        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "Grid"

    Protected Sub grdCase_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdCase.Sorting
        Try
            Dim Dts As New DataSet
            Dim dv As DataView
            Dim numberDiv As Integer
            Dim dsSorted As New DataSet()

            numberDiv = Convert.ToInt16(hvUserGrd.Value.ToString())
            Dts = Session("UsersData")
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
            'CheckBoxFill()
            GetStatusForCase()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "New_WinCopy1", "RefreshPageBar();", True)

            dsSorted.Tables.Add(dv.ToTable())
            Session("UsersData") = dsSorted

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            bindCaseGrid()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "New_WinCopy1", "RefreshPageBar();", True)
        Catch ex As Exception
            Response.Write("Error:btnSearch_Click:" + ex.Message.ToString())
        End Try
    End Sub

    Protected Sub GetStatusForCase()
        Dim linkBut As New LinkButton
        Dim lblGrpId As New Label
        For Each Gr As GridViewRow In grdCase.Rows
            lblGrpId = grdCase.Rows(Gr.RowIndex).FindControl("lblGroupID")
            linkBut = grdCase.Rows(Gr.RowIndex).FindControl("lnkGroId")
            ' If lblGrpId.Text = 0 Then
            'linkBut.Enabled = True
            ' Else
            linkBut.Enabled = True
            'If ddlmodule.SelectedValue = "E1" Or ddlmodule.SelectedValue = "S1" Then
            Dim lblCaseId As New Label
            Dim lblUserName As New Label
            Dim lblUserID As New Label
            lblCaseId = grdCase.Rows(Gr.RowIndex).FindControl("lblCaseID")
            lblUserName = grdCase.Rows(Gr.RowIndex).FindControl("lblUser")
            lblUserID = grdCase.Rows(Gr.RowIndex).FindControl("lblUserID")
            linkBut.Attributes.Add("onclick", "return testData('../Popup/StatusDetails.aspx?CaseId=" + lblCaseId.Text.Trim() + "&Owner=" + lblUserID.Text.Trim() + "&Mod=E1');")

            'ElseIf ddlmodule.SelectedValue = "E2" Or ddlmodule.SelectedValue = "S2" Then
            '    Dim lblCaseId1 As New Label
            '    Dim lblUserName1 As New Label
            '    lblCaseId1 = grdCase.Rows(Gr.RowIndex).FindControl("lblCaseID")
            '    lblUserName1 = grdCase.Rows(Gr.RowIndex).FindControl("lblUser")
            '    linkBut.Attributes.Add("onclick", "return testData('../Popup/StatusDetails.aspx?CaseId=" + lblCaseId1.Text.Trim() + "&UserName=" + lblUserName1.Text.Trim() + "&Mod=" + ddlmodule.SelectedValue + "');")

            ' End If

        Next
    End Sub

#End Region

    Protected Sub grdCase_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdCase.PageIndexChanging
        Try
            grdCase.PageIndex = e.NewPageIndex
            bindCaseGridSession()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "New_WinCopy1", "RefreshPageBar();", True)
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub ddlPageCount_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageCount.SelectedIndexChanged
        Try
            grdCase.PageSize = ddlPageCount.SelectedValue.ToString()

            bindCaseGridSession()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "New_WinCopy1", "RefreshPageBar();", True)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lb_command(ByVal sender As Object, ByVal e As CommandEventArgs)
        grdCase.PageIndex = Convert.ToInt32(e.CommandArgument) - 1
        bindCaseGridSession()
    End Sub

    Protected Sub grdCase_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdCase.RowCreated
        If e.Row.RowType = DataControlRowType.Pager Then
            Dim gvr As GridViewRow = e.Row
            Dim lb As LinkButton = DirectCast(gvr.Cells(0).FindControl("p0"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command
            lb = DirectCast(gvr.Cells(0).FindControl("p1"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command
            lb = DirectCast(gvr.Cells(0).FindControl("p2"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command
            lb = DirectCast(gvr.Cells(0).FindControl("p4"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command
            lb = DirectCast(gvr.Cells(0).FindControl("p5"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command
            lb = DirectCast(gvr.Cells(0).FindControl("p6"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command
            lb = DirectCast(gvr.Cells(0).FindControl("p7"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command
            lb = DirectCast(gvr.Cells(0).FindControl("p8"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command
            lb = DirectCast(gvr.Cells(0).FindControl("p9"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command
            lb = DirectCast(gvr.Cells(0).FindControl("p10"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command
        End If
    End Sub

    Protected Sub grdCase_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdCase.DataBound
        Try
            Dim gvr As GridViewRow = grdCase.TopPagerRow

            Dim lb1 As Label = DirectCast(gvr.Cells(0).FindControl("currentpage"), Label)
            lb1.Text = Convert.ToString(grdCase.PageIndex + 1)
            Dim page As Integer() = New Integer(10) {}
            page(0) = grdCase.PageIndex - 2
            page(1) = grdCase.PageIndex - 1
            page(2) = grdCase.PageIndex
            page(3) = grdCase.PageIndex + 1
            page(4) = grdCase.PageIndex + 2
            page(5) = grdCase.PageIndex + 3
            page(6) = grdCase.PageIndex + 4
            page(7) = grdCase.PageIndex + 5
            page(8) = grdCase.PageIndex + 6
            page(9) = grdCase.PageIndex + 7
            page(10) = grdCase.PageIndex + 8
            For i As Integer = 0 To 10
                If i <> 3 Then
                    If page(i) < 1 OrElse page(i) > grdCase.PageCount Then
                        Dim lb As LinkButton = DirectCast(gvr.Cells(0).FindControl("p" & Convert.ToString(i)), LinkButton)
                        lb.Visible = False
                    Else
                        Dim lb As LinkButton = DirectCast(gvr.Cells(0).FindControl("p" & Convert.ToString(i)), LinkButton)
                        lb.Text = Convert.ToString(page(i))

                        lb.CommandName = "pageno"

                        lb.CommandArgument = lb.Text
                    End If
                End If
            Next
            If grdCase.PageIndex = 0 Then
                Dim lb As LinkButton = DirectCast(gvr.Cells(0).FindControl("linkbutton1"), LinkButton)
                lb.Visible = False
                lb = DirectCast(gvr.Cells(0).FindControl("linkbutton2"), LinkButton)

                lb.Visible = False
            End If
            If grdCase.PageIndex = grdCase.PageCount - 1 Then
                Dim lb As LinkButton = DirectCast(gvr.Cells(0).FindControl("linkbutton3"), LinkButton)
                lb.Visible = False
                lb = DirectCast(gvr.Cells(0).FindControl("linkbutton4"), LinkButton)

                lb.Visible = False
            End If
            If grdCase.PageIndex > grdCase.PageCount - 5 Then
                Dim lbmore As Label = DirectCast(gvr.Cells(0).FindControl("nmore"), Label)
                lbmore.Visible = False
            End If
            If grdCase.PageIndex < 4 Then
                Dim lbmore As Label = DirectCast(gvr.Cells(0).FindControl("pmore"), Label)
                lbmore.Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            btnSearch_Click(sender, e)
        Catch ex As Exception

        End Try
    End Sub

#Region "Tools Function"

    Protected Function CreateCase(ByVal UserID As String, ByVal schem As String) As Integer
        Dim CaseId As Integer
        Dim obj As New ToolCCS
        Try
            If schem = "E1" Or schem = "S1" Then
                CaseId = obj.CreateCase(UserID, "EconConnectionString", "E1", 0)
                obj.CreateCase(UserID, "Sustain1ConnectionString", "E1", CaseId)
            ElseIf schem = "E2" Or schem = "S2" Then
                CaseId = obj.CreateCase(UserID, "Econ2Conn", "E2", 0)
                obj.CreateCase(UserID, "Sustain2Conn", "E2", CaseId)
            End If

            Return CaseId
        Catch ex As Exception

        End Try
    End Function

    Protected Function CopyCase(ByVal SCaseID As String, ByVal TCaseId As String, ByVal Schema As String) As Boolean
        Dim obj As New ToolCCS
        Try

            obj.CopyCase(SCaseID, TCaseId, Schema)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Protected Sub AddSisCase(ByVal schem As String, ByVal caseId As String, ByVal SisCaseId As String)

        Dim obj As New ToolCCS
        Try
            If schem = "E1" Or schem = "S1" Then
                obj.UpdatePermissionCasesSis("Sustain1ConnectionString", caseId, SisCaseId)
            ElseIf schem = "E2" Or schem = "S2" Then

            End If


        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "Approve"

    Protected Sub btnApprove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnApprove.Click
        Dim SCaseId As String = String.Empty
        Dim TCaseId As String = String.Empty
        Dim objUpdateData As New E1UpInsData.UpdateInsert
        ' Dim UserName(10) As String
        Dim schem As String = ""
        Dim Flag As Boolean
        Dim message As String = String.Empty
        Dim message2 As String = String.Empty
        Dim message3 As String = String.Empty
        Dim cnt As Integer = 0
        Dim CaseIds(10) As String
        Dim newCaseIds(10) As String

        Dim chkIsAdded As New CheckBox
        Dim lblCaseId As New Label

        Dim Users(10) As String
        Dim lblUser As New Label

        Dim UsersID(10) As String
        Dim lblUserID As New Label

        Dim objGetData As New E1GetData.Selectdata
        Dim statudId(10) As String
        Dim lblStatusId As Label

        Dim i As Integer
        Try
            schem = "E1" 'ddlmodule.SelectedValue

            'Getting Selected Cases
            For Each Gr As GridViewRow In grdCase.Rows
                chkIsAdded = grdCase.Rows(Gr.RowIndex).FindControl("chkIsAdded")
                If chkIsAdded.Checked = True Then
                    lblCaseId = grdCase.Rows(Gr.RowIndex).FindControl("lblCaseID")
                    CaseIds(cnt) = lblCaseId.Text

                    lblUser = grdCase.Rows(Gr.RowIndex).FindControl("lblUser")
                    Users(cnt) = lblUser.Text

                    lblUserID = grdCase.Rows(Gr.RowIndex).FindControl("lblUserId")
                    UsersID(cnt) = lblUserID.Text

                    lblStatusId = grdCase.Rows(Gr.RowIndex).FindControl("lblStatusID")
                    statudId(cnt) = lblStatusId.Text

                    cnt += 1

                End If
            Next

            'Re initialize count
            cnt = 0
            If schem = "E1" Or schem = "S1" Then
                For i = 0 To CaseIds.Length - 1
                    If CaseIds(i) <> "" Then
                        'objUpdateData.DeleteGroupCases(CaseIds(i))
                        If statudId(i) = "1" Then
                            Dim dsStSis As New DataSet()
                            Dim CaseIdSis As String
                            Dim UserSis As String
                            CaseIdSis = CaseIds(i)
                            UserSis = UsersID(i)
                            dsStSis = objGetData.GetStatusForSister(schem, CaseIdSis, UserSis)
                            If dsStSis.Tables(0).Rows.Count > 0 Then
                                Flag = objUpdateData.PermissionStatusUpdateSisN(schem, UserSis, CaseIdSis, Session("USERID"))
                                message3 = message3 + "\nSister Case " + CaseIds(i).ToString() + " Updated to Approved\n"
                            Else
                                Dim dscase As New DataSet()
                                dscase = objGetData.GetCaseIdSister(schem, CaseIds(i), Session("USERID").ToString())
                                If dscase.Tables(0).Rows(0).Item("SISTERCASEID").ToString() <> "" Then
                                    Flag = objUpdateData.PermissionStatusUpdateCasebySister(schem, UserSis, dscase.Tables(0).Rows(0).Item("SISTERCASEID").ToString(), Session("USERID"), CaseIds(i))
                                    message3 = message3 + "\nSister Case " + dscase.Tables(0).Rows(0).Item("SISTERCASEID").ToString() + " Updated to Approved\n"
                                Else
                                    SCaseId = CaseIds(i)
                                    TCaseId = CreateCase(Session("USERID").ToString(), schem)
                                    newCaseIds(cnt) = TCaseId
                                    'added siscase
                                    AddSisCase(schem, CaseIds(i), TCaseId)
                                    'end
                                    Flag = CopyCase(SCaseId, TCaseId, "EconConnectionString")
                                    Flag = CopyCase(SCaseId, TCaseId, "Sustain1ConnectionString")
                                    cnt += 1
                                End If
                            End If
                        ElseIf statudId(i) = "6" Then
                            Dim dsStSis As New DataSet()
                            Dim CaseIdSis As String
                            Dim UserSis As String
                            CaseIdSis = CaseIds(i)
                            UserSis = UsersID(i)

                            Dim dscase As New DataSet()
                            dscase = objGetData.GetCaseIdSister(schem, CaseIds(i), Session("USERID").ToString())
                            If dscase.Tables(0).Rows(0).Item("SISTERCASEID").ToString() <> "" Then
                                Flag = objUpdateData.PermissionStatusUpdateCasebySister(schem, UserSis, dscase.Tables(0).Rows(0).Item("SISTERCASEID").ToString(), Session("USERID"), CaseIds(i))
                                message3 = message3 + "\nSister Case " + dscase.Tables(0).Rows(0).Item("SISTERCASEID").ToString() + " Updated to Approved\n"
                            Else
                                SCaseId = CaseIds(i)
                                TCaseId = CreateCase(Session("USERID").ToString(), schem)
                                newCaseIds(cnt) = TCaseId
                                'added siscase
                                AddSisCase(schem, CaseIds(i), TCaseId)
                                'end
                                Flag = CopyCase(SCaseId, TCaseId, "EconConnectionString")
                                Flag = CopyCase(SCaseId, TCaseId, "Sustain1ConnectionString")
                                cnt += 1
                            End If

                        ElseIf statudId(i) = "4" Then
                            Dim dsSt As New DataSet()
                            Dim dsSt2 As New DataSet()
                            Dim CaseId As String
                            Dim User As String
                            CaseId = CaseIds(i)
                            User = UsersID(i)
                            dsSt = objGetData.GetStatusById(schem, CaseId, User)
                            dsSt2 = objGetData.GetStatusSisById(schem, CaseId, User)
                            If dsSt.Tables(0).Rows.Count > 0 Then
                                Flag = objUpdateData.PermissionStatusUpdateApp(schem, User, CaseId)
                                message2 = message2 + "\nCase " + CaseIds(i).ToString() + " Updated to Approved\n"
                            ElseIf dsSt2.Tables(0).Rows.Count > 0 Then
                                Flag = objUpdateData.PermissionStatusUpdateApp(schem, User, CaseId)
                                message2 = message2 + "\nCase " + CaseIds(i).ToString() + " Sister Case Updated to Approved\n"
                            Else
                                'create copy n change sttus
                                SCaseId = CaseIds(i)
                                TCaseId = CreateCase(Session("USERID").ToString(), schem)
                                newCaseIds(cnt) = TCaseId
                                Flag = CopyCase(SCaseId, TCaseId, "EconConnectionString")
                                Flag = CopyCase(SCaseId, TCaseId, "Sustain1ConnectionString")
                                cnt += 1
                            End If
                        End If

                    End If
                Next
                'ElseIf schem = "E2" Or schem = "S2" Then
                '    For i = 0 To CaseIds.Length - 1
                '        If CaseIds(i) <> "" Then
                '            If statudId(i) = "1" Then
                '                Dim dsStSis As New DataSet()
                '                Dim CaseIdSis As String
                '                Dim UserSis As String
                '                CaseIdSis = CaseIds(i)
                '                UserSis = Users(i)
                '                dsStSis = objGetData.GetStatusForSister(schem, CaseIdSis, UserSis)
                '                If dsStSis.Tables(0).Rows.Count > 0 Then
                '                    Flag = objUpdateData.PermissionStatusUpdateSis(schem, UserSis, CaseIdSis)
                '                    message3 = message3 + "\nSister Case " + CaseIds(i).ToString() + " Updated to Approved\n"
                '                Else
                '                    SCaseId = CaseIds(i)
                '                    TCaseId = CreateCase(Session("UserName").ToString(), schem)
                '                    newCaseIds(cnt) = TCaseId
                '                    Flag = CopyCase(SCaseId, TCaseId, "Econ2Conn")
                '                    Flag = CopyCase(SCaseId, TCaseId, "Sustain2Conn")
                '                    cnt += 1
                '                End If
                '            ElseIf statudId(i) = "4" Then
                '                Dim dsSt1 As New DataSet()
                '                Dim dsSt2 As New DataSet()
                '                Dim CaseId As String
                '                Dim User As String
                '                CaseId = CaseIds(i)
                '                User = Users(i)
                '                dsSt1 = objGetData.GetStatusById(schem, CaseId, User)
                '                dsSt2 = objGetData.GetStatusSisById(schem, CaseId, User)
                '                If dsSt1.Tables(0).Rows.Count > 0 Then
                '                    Flag = objUpdateData.PermissionStatusUpdateApp(schem, User, CaseId)
                '                    message2 = message2 + "\nCase " + CaseIds(i).ToString() + " Updated to Approved\n"
                '                ElseIf dsSt2.Tables(0).Rows.Count > 0 Then
                '                    Flag = objUpdateData.PermissionStatusUpdateApp(schem, User, CaseId)
                '                    message2 = message2 + "\nCase " + CaseIds(i).ToString() + " Sister Case Updated to Approved\n"
                '                Else
                '                    SCaseId = CaseIds(i)
                '                    TCaseId = CreateCase(Session("UserName").ToString(), schem)
                '                    newCaseIds(cnt) = TCaseId
                '                    Flag = CopyCase(SCaseId, TCaseId, "Econ2Conn")
                '                    Flag = CopyCase(SCaseId, TCaseId, "Sustain2Conn")
                '                    cnt += 1
                '                End If
                '            End If
                '            End If
                '    Next
            End If

            If Flag Then
                Dim CaseId As String
                Dim User As String
                Dim NewCaseId As String
                message = "\n Approved:\n"
                For i = 0 To cnt - 1
                    message = message + "Case " + newCaseIds(i) + " was created for " + Session("UserName").ToString() + ".\n"
                    message = message + "Case " + CaseIds(i) + " variables were transferred to Case " + newCaseIds(i).ToString() + "."
                    CaseId = CaseIds(i)
                    User = UsersID(i)
                    NewCaseId = newCaseIds(i)
                    objUpdateData.PermissionStatusUpdate(CaseId, User, NewCaseId, Session("USERID").ToString(), schem)
                    objUpdateData.PermissionStatusUpdateSis(CaseId, User, NewCaseId, Session("USERID").ToString(), schem)
                    If i = cnt - 1 Then
                        message = message + "\n"
                    Else
                        message = message + "\n\n"
                    End If
                Next
                If message2 <> "" Then
                    message = message + message2 + message3 + "\n"
                Else
                    message = message + message3 + "\n"
                End If

                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "New_WinCopy", "alert('" + message + "');", True)
                bindCaseGrid()
            End If
        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "Dissapprove"

    Protected Sub btnDisApprove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDisApprove.Click

        Dim objUpdateData As New E1UpInsData.UpdateInsert
        Dim message As String = String.Empty
        Dim chkIsAdded As New CheckBox
        Dim lblCaseId As New Label
        Dim NewCaseId(10) As String
        Dim schem As String = ""
        Dim lblUser As New Label
        Dim Users(10) As String

        Dim lblUserID As New Label
        Dim UsersID(10) As String

        Dim cnt As Integer = 0

        schem = "E1"

        'Getting Selected Cases
        For Each Gr As GridViewRow In grdCase.Rows
            chkIsAdded = grdCase.Rows(Gr.RowIndex).FindControl("chkIsAdded")
            If chkIsAdded.Checked = True Then
                lblCaseId = grdCase.Rows(Gr.RowIndex).FindControl("lblCaseID")
                NewCaseId(cnt) = lblCaseId.Text

                lblUser = grdCase.Rows(Gr.RowIndex).FindControl("lblUser")
                Users(cnt) = lblUser.Text

                lblUserID = grdCase.Rows(Gr.RowIndex).FindControl("lblUserID")
                UsersID(cnt) = lblUserID.Text

                cnt += 1

            End If
        Next

        If cnt Then
            Dim CaseId As String
            Dim User As String
            message = "\n Disapproved:\n"
            For i = 0 To cnt - 1
                'objUpdateData.DeleteGroupCases(NewCaseId(i))
                message = message + "Case " + NewCaseId(i) + " has been disapproved for " + Users(i).ToString() + ".\n"
                CaseId = NewCaseId(i)
                User = UsersID(i)
                objUpdateData.DenyPermissionStatusUpdate(CaseId, User, Session("USERID").ToString(), schem)
                If i = cnt - 1 Then
                    message = message + "\n"
                Else
                    message = message + "\n\n"
                End If
            Next
            message = message + "\n"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "New_WinCopy", "alert('" + message + "');", True)
            bindCaseGrid()
        End If

    End Sub

#End Region


    Protected Sub btnEvaluate_Click(sender As Object, e As EventArgs) Handles btnEvaluate.Click
        Try
            Dim SCaseId As String = String.Empty
            Dim TCaseId As String = String.Empty
            Dim objUpdateData As New E1UpInsData.UpdateInsert
            Dim schem As String = ""
            Dim Flag As Boolean
            Dim message As String = String.Empty
            Dim message2 As String = String.Empty
            Dim message3 As String = String.Empty
            Dim cnt As Integer = 0
            Dim CaseIds(10) As String
            Dim newCaseIds(10) As String

            Dim chkIsAdded As New CheckBox
            Dim lblCaseId As New Label

            Dim Users(10) As String
            Dim lblUser As New Label

            Dim UsersID(10) As String
            Dim lblUserID As New Label

            Dim objGetData As New E1GetData.Selectdata
            Dim statudId(10) As String
            Dim lblStatusId As Label

            Dim i As Integer
            Try
                schem = "E1" 'ddlmodule.SelectedValue

                'Getting Selected Cases
                For Each Gr As GridViewRow In grdCase.Rows
                    chkIsAdded = grdCase.Rows(Gr.RowIndex).FindControl("chkIsAdded")
                    If chkIsAdded.Checked = True Then
                        lblCaseId = grdCase.Rows(Gr.RowIndex).FindControl("lblCaseID")
                        CaseIds(cnt) = lblCaseId.Text

                        lblUser = grdCase.Rows(Gr.RowIndex).FindControl("lblUser")
                        Users(cnt) = lblUser.Text

                        lblUserID = grdCase.Rows(Gr.RowIndex).FindControl("lblUserId")
                        UsersID(cnt) = lblUserID.Text

                        lblStatusId = grdCase.Rows(Gr.RowIndex).FindControl("lblStatusID")
                        statudId(cnt) = lblStatusId.Text

                        cnt += 1

                    End If
                Next

                'Re initialize count
                cnt = 0
                If schem = "E1" Or schem = "S1" Then
                    For i = 0 To CaseIds.Length - 1
                        If CaseIds(i) <> "" Then
                            'objUpdateData.DeleteGroupCases(CaseIds(i))

                            If statudId(i) = "1" Then
                                Dim CaseId As String
                                Dim User As String
                                CaseId = CaseIds(i)
                                User = UsersID(i)
                                Flag = objUpdateData.PermissionStatusUpdateAppEV(schem, User, CaseId, Session("USERID").ToString())
                                message2 = message2 + "\nCase " + CaseIds(i).ToString() + " Updated to under Evaluation\n"
                                ' ScriptManager.RegisterStartupScript(Me, Me.GetType(), "New_WinCopy", "alert('" + message2 + "');", True)
                                bindCaseGrid()
                            End If
                            cnt += 1
                        End If
                    Next

                End If

                If Flag Then
                    message = "\n Under Evaluation:\n"
                    If message2 <> "" Then
                        message = message + message2 + "\n"
                   
                    End If

                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "New_WinCopy", "alert('" + message + "');", True)
                    bindCaseGrid()
                End If
            Catch ex As Exception

            End Try
        Catch ex As Exception

        End Try
    End Sub
End Class
