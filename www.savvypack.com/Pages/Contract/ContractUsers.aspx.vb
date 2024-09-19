Imports System
Imports System.Data
Imports ContrGetData
Imports ContrUpInsData
Imports AjaxControlToolkit
Partial Class ContractUsers
    Inherits System.Web.UI.Page
    Dim objGetdata As New Selectdata()
    Dim objUpIns As New UpdateInsert()
    Dim count As Integer = 0
    Dim arrUser() As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not IsPostBack Then
                txtsrchUsername.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnUserSearch.ClientID + "')")
                'Updating License Admin
                ' objUpIns.UpdateAdmin(Session("UserId"))
                BindUserGrid()
                hvUserGrd.Value = "0"
                lnkShowAll.Text = "Show All"
            End If
            hidChkCount.Value = "0"

        Catch ex As Exception
            Response.Write("Error" + ex.Message.ToString())
        End Try
    End Sub

#Region "User Grid Events"
    Protected Sub grdUsers_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdUsers.PageIndexChanging
        Try
            grdUsers.PageIndex = e.NewPageIndex
            BindUserGridUsingSession()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub grdUsers_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdUsers.Sorting
        Try
            Dim Dts As New DataSet
            Dim dv As DataView
            Dim numberDiv As Integer
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
            grdUsers.DataSource = dv
            grdUsers.DataBind()
            lblmsg.Visible = False

            BindData()
        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "User Define Delegates"
    Protected Sub BindUserGrid()
        Dim Dts As New DataSet
        Dim Dts2 As New DataSet
        Dim Ds As New DataSet
        Dim dsLimit As New DataSet
        Dim dsLimitN As New DataSet
        Dim check As New CheckBox
        Dim radio As New CheckBox
        Dim lblUserId As New Label
        Dim count As Integer = 0
        Try
            arrUser = Regex.Split(Session("UserName"), "@")
            Dts = objGetdata.GetStructUserInforMation(arrUser(1), txtsrchUsername.Text.Trim.ToUpper.ToString(), Session("UserId"))
            Dts2 = objGetdata.GetTUserInforMation(Session("UserId"))

            Session("UsersData") = Dts
            grdUsers.DataSource = Dts
            grdUsers.DataBind()

            If Dts2.Tables(0).Rows.Count > 0 Then
                grdTUser.DataSource = Dts2
                grdTUser.DataBind()
            End If
           


            If Dts.Tables(0).Rows.Count <= 10 Then
                lnkShowAll.Enabled = False
            Else
                lnkShowAll.Enabled = True
            End If

            Ds = objGetdata.GetConAdminServiceInforMation(Session("UserId"))
			Session("LICID") = Ds.Tables(0).Rows(0).Item("LICENSEID").ToString()
            dsLimit = objGetdata.LicUserCount(Ds.Tables(0).Rows(0).Item("LICENSEID").ToString())
            dsLimitN = objGetdata.LicUserCountn(Ds.Tables(0).Rows(0).Item("LICENSEID").ToString())
            If Ds.Tables(0).Rows.Count > 0 Then
                hidTotalCount.Value = Ds.Tables(0).Rows(0).Item("TOTALCOUNT").ToString()
                lblLicCount.Text = hidTotalCount.Value
            End If

            For Each Gr As GridViewRow In grdUsers.Rows
                lblUserId = grdUsers.Rows(Gr.RowIndex).FindControl("lblUser")
                check = Gr.FindControl("Add")
                radio = Gr.FindControl("rdTransfer")
                'For i = 0 To Ds.Tables(0).Rows.Count - 1
                If hidTotalCount.Value = hidChkCount.Value Then
                    check.Enabled = False
                    check.ToolTip = "You have already assigned all " + hidTotalCount.Value + " of your available licenses."
                End If
                If hidTotalCount.Value = dsLimit.Tables(0).Rows(0).Item("CNT").ToString() Then
                    radio.Enabled = False
                    radio.ToolTip = "Transfer for this year is used up."
                ElseIf dsLimit.Tables(0).Rows(0).Item("CNT").ToString() = "0" Then
                    If dsLimitN.Tables(0).Rows(0).Item("CNT").ToString() = "0" Then
                        radio.Enabled = False
                        radio.ToolTip = "Transfer for this year is used up."
                    End If
                ElseIf hidUserCount.Value = dsLimit.Tables(0).Rows(0).Item("CNT").ToString() Then
                    radio.Enabled = False
                    radio.ToolTip = "Transfer for this year is used up."
                End If
                'Next
            Next
            BindData()

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub BindUserGridUsingSession()
        Dim Dts As New DataSet
        Dim Dts2 As New DataSet
        Dim Ds As New DataSet
        Dim dsLimit As New DataSet
        Dim dsLimitN As New DataSet
        Dim check As New CheckBox
        Dim radio As New CheckBox
        Dim lblUserId As New Label
        Dim count As Integer = 0

        Try
            Dts = Session("UsersData")
            Dts2 = objGetdata.GetTUserInforMation(Session("UserId"))

            grdUsers.DataSource = Dts
            grdUsers.DataBind()
            lblmsg.Visible = False

            'Binding Checkbox and Radiobutton feature
            If Dts2.Tables(0).Rows.Count > 0 Then
                grdTUser.DataSource = Dts2
                grdTUser.DataBind()
            End If



            If Dts.Tables(0).Rows.Count <= 10 Then
                lnkShowAll.Enabled = False
            Else
                lnkShowAll.Enabled = True
            End If

            Ds = objGetdata.GetConAdminServiceInforMation(Session("UserId"))
            Session("LICID") = Ds.Tables(0).Rows(0).Item("LICENSEID").ToString()
            dsLimit = objGetdata.LicUserCount(Ds.Tables(0).Rows(0).Item("LICENSEID").ToString())
            dsLimitN = objGetdata.LicUserCountn(Ds.Tables(0).Rows(0).Item("LICENSEID").ToString())
            If Ds.Tables(0).Rows.Count > 0 Then
                hidTotalCount.Value = Ds.Tables(0).Rows(0).Item("TOTALCOUNT").ToString()
                lblLicCount.Text = hidTotalCount.Value
            End If

            For Each Gr As GridViewRow In grdUsers.Rows
                lblUserId = grdUsers.Rows(Gr.RowIndex).FindControl("lblUser")
                check = Gr.FindControl("Add")
                radio = Gr.FindControl("rdTransfer")
                'For i = 0 To Ds.Tables(0).Rows.Count - 1
                If hidTotalCount.Value = hidChkCount.Value Then
                    check.Enabled = False
                    check.ToolTip = "You have already assigned all " + hidTotalCount.Value + " of your available licenses."
                End If
                If hidTotalCount.Value = dsLimit.Tables(0).Rows(0).Item("CNT").ToString() Then
                    radio.Enabled = False
                    radio.ToolTip = "Transfer for this year is used up."
                ElseIf dsLimit.Tables(0).Rows(0).Item("CNT").ToString() = "0" Then
                    If dsLimitN.Tables(0).Rows(0).Item("CNT").ToString() = "0" Then
                        radio.Enabled = False
                        radio.ToolTip = "Transfer for this year is used up."
                    End If
                ElseIf hidUserCount.Value = dsLimit.Tables(0).Rows(0).Item("CNT").ToString() Then
                    radio.Enabled = False
                    radio.ToolTip = "Transfer for this year is used up."
                End If
            Next

            BindData()
        Catch ex As Exception

        End Try
    End Sub
#End Region

    Protected Sub lnkShowAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkShowAll.Click
        Try
            If grdUsers.AllowPaging = True Then
                grdUsers.AllowPaging = False
                lnkShowAll.Text = "Show Paging"
            Else
                grdUsers.AllowPaging = True
                lnkShowAll.Text = "Show All"
            End If

            BindUserGridUsingSession()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnUserSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUserSearch.Click
        Try
            BindUserGrid()
        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindData()
        Dim Dts As New DataSet()
        Dim Ds As New DataSet()
        Dim check As New CheckBox
        Dim radio As New RadioButton
        Dim lblUserId As New Label
        Dim count As Integer = 0
        arrUser = Regex.Split(Session("UserName"), "@")
        Dts = Session("UsersData")
        Ds = objGetdata.GetConServiceUserInforMation(arrUser(1), Session("UserId"), "Contract Packaging Knowledgebase")
        hidChkCount.Value = Ds.Tables(0).Rows.Count
        For Each Gr As GridViewRow In grdUsers.Rows
            lblUserId = grdUsers.Rows(Gr.RowIndex).FindControl("lblUser")
            check = Gr.FindControl("Add")
            radio = Gr.FindControl("rdTransfer")
            For i = 0 To Ds.Tables(0).Rows.Count - 1
                If lblUserId.Text = Ds.Tables(0).Rows(i).Item("USERID").ToString() Then
                    check.Checked = True
                    check.Enabled = False
                    radio.Enabled = False
                    count = count + 1
                    'hidChkCount.Value = count
                End If
            Next


        Next

        hidUserCount.Value = Ds.Tables(0).Rows.Count
    End Sub

    Protected Sub btnCall_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCall.Click
        Try
            Dim Dts As DataSet
            Dts = objGetdata.GetConAdminServiceInforMation(Session("UserId"))
            For Each i As GridViewRow In grdUsers.Rows
                Dim Chk As New CheckBox
                Dim UserId As New Integer
                Chk = i.FindControl("Add")
                UserId = Convert.ToInt32(grdUsers.DataKeys(i.RowIndex).Value)
                Dim ds As DataSet
                ds = objGetdata.GetServiceUser(UserId, "Contract Packaging Knowledgebase")
                Dim count As Integer = hidUserCount.Value
                If Chk.Checked = True Then
                    If ds.Tables(0).Rows.Count > 0 Then
                    Else
                        objUpIns.AddTransferLicense(Dts.Tables(0).Rows(0).Item("LICENSEID").ToString(), UserId, "KBCOPK")
                        objUpIns.AddOrderUsers(UserId, "Contract Packaging Knowledgebase")
                        objUpIns.AddEditUserContractCountries(UserId)
                    End If
                   
                End If
            Next

            BindUserGrid()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            BindUserGrid()
        Catch ex As Exception

        End Try
    End Sub
End Class
