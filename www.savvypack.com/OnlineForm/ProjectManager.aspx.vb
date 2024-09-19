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

Partial Class Pages_ProjectManager_Default
    Inherits System.Web.UI.Page
    Dim objUpIns As New UpdateInsert()
    Dim pageid As String

#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_PAGES_PROJECTMANAGER_DEFAULT")
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        objRefresh.Render(Page)
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Try
                lblTag.Text = "Copyright 1997 - " + Now.Year().ToString() + " SavvyPack Corporation"
            Catch ex As Exception
            End Try
            If Session("SBack") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If

            If Not IsPostBack Then
                InsertAnalysisInformation()
                Session("SORT") = "DATE"
                GetUserDetails()
                hidDesc.Value = "Min"
                GetProjectDetails()
                hidSortId.Value = "0"
                If Session("SPROJLogInCount") = Nothing Then
                    'Started Activity Log Changes
                    Try

                        Session("SPROJLogInCount") = objUpIns.InsertLog(Session("UserId").ToString(), "ProjectManager.aspx", "Opened SavvyPack Project Manager Page")
                    Catch ex As Exception

                    End Try
                    'Ended Activity Log Changes
                Else
                    'Started Activity Log Changes
                    Try
                        objUpIns.InsertLog1(Session("UserId").ToString(), "ProjectManager.aspx", "Opened SavvyPack Project Manager Page", "", Session("SPROJLogInCount").ToString())

                    Catch ex As Exception

                    End Try
                    'Ended Activity Log Changes
                End If
            End If

        Catch ex As Exception
            lblError.Text = "Error:Page_Load:" + ex.Message.ToString() + ""
        End Try
    End Sub
    Protected Sub InsertAnalysisInformation()
        Dim objGetData As New Selectdata()
        Dim Dts As New DataSet()
        Dim i As New Integer
        Dim ds As New DataSet
        Try
            Dts = objGetdata.GetAnalysis()
            ds = objGetdata.GetUserAnalysisDetails(Session("UserId").ToString().ToString())
            If ds.Tables(0).Rows.Count = 0 Then
                For i = 1 To Dts.Tables(0).Rows.Count - 1
                    objUpIns.AddUserAnalysis(Session("UserId").ToString(), Dts.Tables(0).Rows(i).Item("ANALYSISID").ToString(), Session("USERID"))
                Next
            End If


        Catch ex As Exception
            Response.Write("InsertAnalysisInformation" + ex.Message)
        End Try
    End Sub
    Protected Sub GetUserDetails()
        Dim objGetData As New Selectdata()
        Dim ds As New DataSet()
        Try
            ds = objGetData.GetUserDetails(Session("UserId"))

            Session("SavvyUserId") = ds.Tables(0).Rows(0).Item("USERID").ToString()
            Session("SavvyUserName") = ds.Tables(0).Rows(0).Item("USERNAME").ToString()

            'Set for License Administrator
            Session("SavvyLicAdmin") = ds.Tables(0).Rows(0).Item("ISIADMINLICUSR").ToString()
            If ds.Tables(0).Rows(0).Item("LICENSEID").ToString() = "" Then
                Session("SavvyLicenseId") = 0
            Else
                Session("SavvyLicenseId") = ds.Tables(0).Rows(0).Item("LICENSEID").ToString()
            End If

            Session("SavvyIntUser") = ds.Tables(0).Rows(0).Item("ISINTERNALUSR").ToString()
            Session("SavvyAnalyst") = ds.Tables(0).Rows(0).Item("ISANALYST").ToString()

        Catch ex As Exception
            lblError.Text = "Error:SetSessions:" + ex.Message.ToString()
        End Try
    End Sub
    Public Sub GetProjectDetails()
        Dim objGetData As New SavvyGetData.Selectdata()
        Dim ds As New DataSet
        Dim dsMemo As New DataSet
        Try
            dsMemo = objGetData.MemorizedDetails(Session("UserId"))

            If dsMemo.Tables(0).Rows.Count < 1 Then
                objUpIns.InsertMemorizedDetails(Session("UserId"))
                dsMemo = objGetData.MemorizedDetails(Session("UserId"))
            End If
            ddlSize.SelectedValue = dsMemo.Tables(0).Rows(0).Item("PAGESIZEID").ToString()
            ds = objGetData.GetAllProjectDetails(Session("UserId"), txtSearch.Text.Replace("'", "''"), dsMemo.Tables(0).Rows(0).Item("DESCTYPE").ToString(), _
                                                 dsMemo.Tables(0).Rows(0).Item("DATETYPEID").ToString(), dsMemo.Tables(0).Rows(0).Item("DISPLAYSTATUSID").ToString(), _
                                                 dsMemo.Tables(0).Rows(0).Item("SORTSTATUSID").ToString(), dsMemo.Tables(0).Rows(0).Item("DISPLAYMILESTONEID").ToString(), _
                                                 dsMemo.Tables(0).Rows(0).Item("SORTMILESDATE").ToString(), dsMemo.Tables(0).Rows(0).Item("SORTMILESID").ToString(), Session("SORT"))
            Session("dsPA") = ds
            If dsMemo.Tables(0).Rows(0).Item("DESCTYPE").ToString() = "Min" Then
                imgMin.Visible = False
                imgPls.Visible = True
            Else
                imgMin.Visible = True
                imgPls.Visible = False
            End If
            lblPCount.Text = ds.Tables(0).Rows.Count
            Session("ProjData") = ds
            If ds.Tables(0).Rows.Count > 0 Then
                grdProject.PageSize = ddlSize.SelectedItem.ToString()
                grdProject.Visible = True
                grdProject.DataSource = ds
                grdProject.DataBind()
                BindLink()
                trRow.Visible = False
                lblMsg.Visible = False
                If ds.Tables(0).Rows.Count <= 1 Then
                    grdProject.Height = 150
                End If
            Else
                lblMsg.Height = 100

                ContentPage.Width = 1360
                ContentPage.Height = 150
                'ContentPage.Width = 200
                trRow.Visible = True
                lblMsg.Visible = True
                grdProject.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = "Error:GetProjectDetails:" + ex.Message.ToString() + ""
        End Try

    End Sub

    Protected Sub grdProject_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdProject.Sorting
        Try
            Dim Dts As New DataSet
            Dim dv As DataView
            Dim numberDiv As Integer
            numberDiv = Convert.ToInt16(hidSortId.Value.ToString())
            Dts = Session("ProjData")
            dv = Dts.Tables(0).DefaultView

            If ((numberDiv Mod 2) = 0) Then
                dv.Sort = e.SortExpression + " " + "DESC"
            Else
                dv.Sort = e.SortExpression + " " + "ASC"
            End If
            ' numberDiv += 1
            hidSortId.Value = numberDiv.ToString()
            grdProject.DataSource = dv
            grdProject.DataBind()

            'Started Activity Log Changes
            Try
                objUpIns.InsertLog1(Session("UserId").ToString(), "ProjectManager.aspx", "Sorted Project Report Grid by Expression:" + e.SortExpression, "", Session("SPROJLogInCount").ToString())

            Catch ex As Exception

            End Try
            'Ended Activity Log Changes

            BindLink()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub grdUsers_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdProject.PageIndexChanging
        Try
            grdProject.PageIndex = e.NewPageIndex
            BindGridSession()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub BindGridSession()
        Try
            Dim Dts As New DataSet
            Dts = Session("ProjData")
            grdProject.DataSource = Dts
            grdProject.DataBind()
            'lblmsg.Visible = False
            'lblUserAv.Text = ""
            'trUsername.Visible = False
            'trLicense.Visible = False
            BindLink()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub BindLink()

        Dim lblProjId As New Label
        Dim lblCRes As New Label
        Dim lblROI As New Label
        Dim lblDesc As New Label
        Dim lblUserID As New Label
        Dim lblCount As New Label
        Dim lblTitle As New Label
        Dim lblSubmit As New Label
        Dim lnkProjId As New LinkButton
        Dim lnkTitle As New LinkButton
        Dim lnkCRes As New LinkButton
        Dim lnkROI As New LinkButton
        Dim lnkAnalysis As New LinkButton
        Dim lnkAnalyst As New LinkButton
        Dim lnkWord As New LinkButton
        Dim lnkDesc As New LinkButton
        Dim lnkGrid As New LinkButton
        Dim lnkDates As New LinkButton
        Dim lblNum As New Label
        Dim lblEmail As New Label
        Dim btnSubmit As New Button
        Dim ds As New DataSet
        Dim dsD As New DataSet
        Dim dsData As New DataSet
        Dim dsDate As New DataSet
        Dim dsCount As New DataSet
        Dim dvData As New DataSet
        Dim dsDesc As New DataSet
        Dim objGetData As New Selectdata()
        Dim strHover As String = String.Empty
        Dim dv As New DataView
        Dim dt As New DataTable
        Dim lblAEmail As New Label
        Dim lblAnalyst As New Label
        Dim lblAnalysID As New Label
        Dim lblANum As New Label
        Dim lblWord As New Label
        Dim lblUser As New Label
        Dim lblAnlId As New Label
        Dim dsProj As New DataSet
        For Each Gr As GridViewRow In grdProject.Rows
            lblProjId = grdProject.Rows(Gr.RowIndex).FindControl("lblProjId")
            lblDesc = grdProject.Rows(Gr.RowIndex).FindControl("lblDesc")
            lblCRes = grdProject.Rows(Gr.RowIndex).FindControl("lblCRes")
            lblROI = grdProject.Rows(Gr.RowIndex).FindControl("lblROI")
            lblUserID = grdProject.Rows(Gr.RowIndex).FindControl("lblUserID")
            lblEmail = grdProject.Rows(Gr.RowIndex).FindControl("lblEmail")
            lblNum = grdProject.Rows(Gr.RowIndex).FindControl("lblNum")
            lblSubmit = grdProject.Rows(Gr.RowIndex).FindControl("lblSubmit")
            lnkAnalysis = grdProject.Rows(Gr.RowIndex).FindControl("lnkAnalysis")
            lnkTitle = grdProject.Rows(Gr.RowIndex).FindControl("lnkTitle")
            lnkDates = grdProject.Rows(Gr.RowIndex).FindControl("lnkDates")
            lnkWord = grdProject.Rows(Gr.RowIndex).FindControl("lnkWord")
            lnkDesc = grdProject.Rows(Gr.RowIndex).FindControl("lnkDesc")
            lnkCRes = grdProject.Rows(Gr.RowIndex).FindControl("lnkCRes")
            lnkROI = grdProject.Rows(Gr.RowIndex).FindControl("lnkROI")
            lblAEmail = grdProject.Rows(Gr.RowIndex).FindControl("lblAEmail")
            lblANum = grdProject.Rows(Gr.RowIndex).FindControl("lblANum")
            lblAnalyst = grdProject.Rows(Gr.RowIndex).FindControl("lblAnalyst")
            lblWord = grdProject.Rows(Gr.RowIndex).FindControl("lblWord")
            lblUser = grdProject.Rows(Gr.RowIndex).FindControl("lblUser")
            lnkAnalyst = grdProject.Rows(Gr.RowIndex).FindControl("lnkAnalyst")
            lblAnlId = grdProject.Rows(Gr.RowIndex).FindControl("lblAnlId")
            lblAnalysID = grdProject.Rows(Gr.RowIndex).FindControl("lblAnalysID")
            btnSubmit = grdProject.Rows(Gr.RowIndex).FindControl("btnSubmit")

            If lblSubmit.Text = "1" Then
                btnSubmit.Enabled = True
                If Session("SavvyAnalyst") <> "Y" Then
                    If Session("UserId") <> lblUserID.Text Then
                        btnSubmit.Enabled = False
                    End If
                End If
            Else
                btnSubmit.Enabled = False
            End If


            Gr.Cells(6).Attributes.Add("onclick", "return ShowPopWindow6('Popup/SendMessage.aspx?ProjectId=" + lblProjId.Text.Trim() + "&FromUser=" + Session("UserId").ToString() + "&ToUser=" + lblAnlId.Text.Trim() + "&Type=Analyst');")
            Gr.Cells(7).Attributes.Add("onclick", "return ShowPopWindow6('Popup/SendMessage.aspx?ProjectId=" + lblProjId.Text.Trim() + "&FromUser=" + Session("UserId").ToString() + "&ToUser=" + lblUserID.Text.Trim() + "&Type=Owner');")
            If Session("UserId") = lblUserID.Text Then
                'Gr.Cells(13).Attributes.Add("onclick", "return OpenNewWindow('ModelInput.aspx?ProjectId=" + lblProjId.Text.Trim() + "&UserId=" + lblUserID.Text + "');")
                Gr.Cells(5).Attributes.Add("onclick", "return ShowPopWindow3('Popup/TypeDetails.aspx?ProjectId=" + lblProjId.Text.Trim() + "&Des=" + lnkAnalysis.ClientID.ToString() + "&UserId=" + lblUserID.Text + "&AID=" + lblAnalysID.Text + "');")
                Gr.Cells(3).Attributes.Add("onclick", "return ShowPopWindow('ProjectSummary.aspx?ProjectId=" + lblProjId.Text.Trim() + "&UserId=" + lblUserID.Text + "');")
                Gr.Cells(4).Attributes.Add("onclick", "return ShowPopWindow('ProjectSummary.aspx?ProjectId=" + lblProjId.Text.Trim() + "&UserId=" + lblUserID.Text + "');")
                Gr.Cells(2).Attributes.Add("onclick", "return ShowPopWindow('ProjectSummary.aspx?ProjectId=" + lblProjId.Text.Trim() + "&UserId=" + lblUserID.Text + "');")
                Gr.Cells(10).Attributes.Add("onclick", "return ShowPopWindow2('Popup/UniversalUploadFiles.aspx?ProjectId=" + lblProjId.Text.Trim() + "&UserId=" + lblUserID.Text + "&FileType=Universal1');")
                Gr.Cells(9).Attributes.Add("onclick", "return ShowPopWindow1('Popup/DateDetails.aspx?ProjectId=" + lblProjId.Text.Trim() + "&UserId=" + lblUserID.Text + "');")
                'Gr.Cells(11).Attributes.Add("onclick", "return ShowPopWindow('Popup/Feedback.aspx?ProjectId=" + lblProjId.Text.Trim() + "&UserId=" + lblUserID.Text + "');")
                Gr.Enabled = True
            Else
                Gr.BackColor = Drawing.Color.LightGray
                'Gr.Cells(13).Attributes.Add("onclick", "return OpenNewWindow('ModelInput.aspx?ProjectId=" + lblProjId.Text.Trim() + "&UserId=" + lblUserID.Text + "');")
                Gr.Cells(5).Attributes.Add("onclick", "return ShowPopWindow3('Popup/TypeDetails.aspx?ProjectId=" + lblProjId.Text.Trim() + "&Des=" + lnkAnalysis.ClientID.ToString() + "&UserId=" + lblUserID.Text + "&AID=" + lblAnalysID.Text + "');")
                Gr.Cells(3).Attributes.Add("onclick", "return ShowPopWindow('ProjectSummary.aspx?ProjectId=" + lblProjId.Text.Trim() + "&UserId=" + lblUserID.Text + "');")
                Gr.Cells(4).Attributes.Add("onclick", "return ShowPopWindow('ProjectSummary.aspx?ProjectId=" + lblProjId.Text.Trim() + "&UserId=" + lblUserID.Text + "');")
                Gr.Cells(2).Attributes.Add("onclick", "return ShowPopWindow('ProjectSummary.aspx?ProjectId=" + lblProjId.Text.Trim() + "&UserId=" + lblUserID.Text + "');")
                Gr.Cells(10).Attributes.Add("onclick", "return ShowPopWindow2('Popup/UniversalUploadFiles.aspx?ProjectId=" + lblProjId.Text.Trim() + "&UserId=" + lblUserID.Text + "&FileType=Universal1');")
                Gr.Cells(9).Attributes.Add("onclick", "return ShowPopWindow1('Popup/DateDetails.aspx?ProjectId=" + lblProjId.Text.Trim() + "&UserId=" + lblUserID.Text + "');")
                'Gr.Cells(11).Attributes.Add("onclick", "return ShowPopWindow('Popup/Feedback.aspx?ProjectId=" + lblProjId.Text.Trim() + "&UserId=" + lblUserID.Text + "');")
            End If
            If Session("SavvyAnalyst") = "Y" Then
                Gr.Enabled = True
                'Gr.Cells(13).Attributes.Add("onclick", "return OpenNewWindow('ModelInput.aspx?ProjectId=" + lblProjId.Text.Trim() + "&UserId=" + lblUserID.Text + "');")
                Gr.Cells(5).Attributes.Add("onclick", "return ShowPopWindow3('Popup/TypeDetails.aspx?ProjectId=" + lblProjId.Text.Trim() + "&Des=" + lnkAnalysis.ClientID.ToString() + "&UserId=" + lblUserID.Text + "&AID=" + lblAnalysID.Text + "');")
                Gr.Cells(3).Attributes.Add("onclick", "return ShowPopWindow('ProjectSummary.aspx?ProjectId=" + lblProjId.Text.Trim() + "&UserId=" + lblUserID.Text + "');")
                Gr.Cells(4).Attributes.Add("onclick", "return ShowPopWindow('ProjectSummary.aspx?ProjectId=" + lblProjId.Text.Trim() + "&UserId=" + lblUserID.Text + "');")
                Gr.Cells(2).Attributes.Add("onclick", "return ShowPopWindow('ProjectSummary.aspx?ProjectId=" + lblProjId.Text.Trim() + "&UserId=" + lblUserID.Text + "');")
                Gr.Cells(10).Attributes.Add("onclick", "return ShowPopWindow2('Popup/UniversalUploadFiles.aspx?ProjectId=" + lblProjId.Text.Trim() + "&UserId=" + lblUserID.Text + "&FileType=Universal1');")
                Gr.Cells(9).Attributes.Add("onclick", "return ShowPopWindow1('Popup/DateDetails.aspx?ProjectId=" + lblProjId.Text.Trim() + "&UserId=" + lblUserID.Text + "');")
                'Gr.Cells(11).Attributes.Add("onclick", "return ShowPopWindow('Popup/Feedback.aspx?ProjectId=" + lblProjId.Text.Trim() + "&UserId=" + lblUserID.Text + "');")
            End If

            Gr.Cells(2).Attributes.Add("style", "word-break:break-all;word-wrap:break-word;")
            Gr.Cells(3).Attributes.Add("style", "word-break:break-all;word-wrap:break-word;")
            Gr.Cells(4).Attributes.Add("style", "word-break:break-all;word-wrap:break-word;")
            Gr.Cells(11).Attributes.Add("style", "word-break:break-all;word-wrap:break-word;")
            Gr.Cells(12).Attributes.Add("style", "word-break:break-all;word-wrap:break-word;")

            dsProj = objGetData.GetEditProjectDetails(lblProjId.Text.Trim())
            hidAnalysisId.Value = dsProj.Tables(0).Rows(0).Item("ANALYSISID").ToString()

            If hidAnalysisId.Value = "6" Then
                'lnkQuan.Attributes.Add("onclick", "return ShowPopWindow5('Popup/QuickPricePopup.aspx?ProjectId=Nothing" + "&PType=E');")
                lnkCRes.Attributes.Add("onclick", "return ShowPopWindowQP('Popup/QuickPricePopup.aspx?ProjectId=" + lblProjId.Text.Trim() + "&PType=E');")
            Else
                lnkCRes.Attributes.Add("onclick", "return ShowPopWindow2('Popup/UniversalDeliveryFiles.aspx?ProjectId=" + lblProjId.Text.Trim() + "&UserId=" + lblUserID.Text + "&AnalysisId=" + hidAnalysisId.Value + "&FileType=Universal1');")

            End If


            lnkROI.Attributes.Add("onclick", "return ShowPopWindow('Popup/Feedback.aspx?ProjectId=" + lblProjId.Text.Trim() + "&UserId=" + lblUserID.Text + "');")

            lnkROI.Attributes.Add("onmouseover", "Tip('" + lblROI.Text + "')")
            lnkROI.Attributes.Add("onmouseout", "UnTip('')")
            lnkCRes.Attributes.Add("onmouseover", "Tip('" + lblCRes.Text + "')")
            lnkCRes.Attributes.Add("onmouseout", "UnTip('')")

            Gr.Cells(6).Attributes.Add("onmouseover", "Tip('Email:" + lblAEmail.Text + "</br> Phone: " + lblANum.Text + "')")
            Gr.Cells(6).Attributes.Add("onmouseout", "UnTip('')")
            Gr.Cells(7).Attributes.Add("onmouseover", "Tip('Email:" + lblEmail.Text + "</br> Phone: " + lblNum.Text + "')")
            Gr.Cells(7).Attributes.Add("onmouseout", "UnTip('')")

            ds = objGetData.GetCategoryDetails(lblProjId.Text)
            strHover = ""
            If ds.Tables(0).Rows.Count > 0 Then

                dv = ds.Tables(0).DefaultView()
                For i = 1 To 7
                    dv.RowFilter = "CATEGORYID=" + i.ToString() + ""
                    dt = dv.ToTable()
                    If dt.Rows.Count > 0 Then
                        If i = 4 Then
                            strHover = strHover + "<b>Product to Pack</b>:"
                        ElseIf i = 3 Then
                            strHover = strHover + "<b>Package Size</b>:"
                        ElseIf i = 1 Then
                            strHover = strHover + "<b>Package Type</b>:"
                        ElseIf i = 5 Then
                            strHover = strHover + "<b>Geography</b>:"
                        ElseIf i = 2 Then
                            strHover = strHover + "<b>Value Chain</b>:"
                        ElseIf i = 6 Then
                            strHover = strHover + "<b>Special Features 1</b>:"
                        ElseIf i = 7 Then
                            strHover = strHover + "<b>Special Features 2</b>:"
                        End If
                        For j = 0 To dt.Rows.Count - 1
                            If j = 0 Then
                                strHover = strHover + " " + dt.Rows(j).Item("VALUE").ToString()
                            Else
                                strHover = strHover + "," + dt.Rows(j).Item("VALUE").ToString()
                            End If
                        Next
                        strHover = strHover + "</br>"
                    End If
                Next
            End If

            For i = 2 To 13
                If i = 13 Then
                    'strHover = strHover + "<b>Click to Edit</b>"
                    If Session("SavvyAnalyst") <> "Y" Then
                        If Session("UserId") <> lblUserID.Text Then
                            strHover = strHover + "<b>Click to View</b>"
                        Else
                            strHover = strHover + "<b>Click to Edit</b>"
                        End If
                    Else
                        strHover = strHover + "<b>Click to Edit</b>"
                    End If
                    Gr.Cells(i).Attributes.Add("onmouseover", "Tip('" + strHover + "')")
                    Gr.Cells(i).Attributes.Add("onmouseout", "UnTip('')")
                ElseIf i < 6 Then
                    'Dim hover As String = "<b>Click to Edit</b>"
                    Dim hover As String = ""
                    If Session("SavvyAnalyst") <> "Y" Then
                        If Session("UserId") <> lblUserID.Text Then
                            hover = "<b>Click to View</b>"
                        Else
                            hover = "<b>Click to Edit</b>"
                        End If
                    Else
                        hover = "<b>Click to Edit</b>"
                    End If
                    Gr.Cells(i).Attributes.Add("onmouseover", "Tip('" + hover + "')")
                    Gr.Cells(i).Attributes.Add("onmouseout", "UnTip('')")

                End If

            Next
            dsD = objGetData.GetExistingDateDetails(lblProjId.Text)
            strHover = ""

            If dsD.Tables(0).Rows.Count > 0 Then
                dv = dsD.Tables(0).DefaultView()
                For k = 1 To 5
                    dv.RowFilter = "DATETYPEID=" + k.ToString() + ""
                    dt = dv.ToTable()
                    If dt.Rows.Count > 0 Then
                        If k = 1 Then
                            strHover = strHover + "<b>Submitted Date</b>:"
                        ElseIf k = 2 Then
                            strHover = strHover + "<b>Desired Complete Date</b>:"
                        ElseIf k = 3 Then
                            strHover = strHover + "<b>Agreed Complete Date</b>:"
                        ElseIf k = 4 Then
                            strHover = strHover + "<b>Complete Date</b>:"
                        ElseIf k = 5 Then
                            strHover = strHover + "<b>Hold Date</b>:"
                        End If
                        For l = 0 To dt.Rows.Count - 1
                            If l = 0 Then
                                strHover = strHover + " " + dt.Rows(l).Item("VALUE").ToString()
                            Else
                                strHover = strHover + "," + dt.Rows(l).Item("VALUE").ToString()
                            End If
                        Next
                        strHover = strHover + "</br>"
                    End If
                Next
            End If
            For k = 9 To 9
                If k = 9 Then
                    'strHover = strHover + "<b>Click to Edit</b>"
                    If Session("SavvyAnalyst") <> "Y" Then
                        If Session("UserId") <> lblUserID.Text Then
                            strHover = strHover + "<b>Click to view</b>"
                        Else
                            strHover = strHover + "<b>Click to Edit</b>"
                        End If
                    Else
                        strHover = strHover + "<b>Click to Edit</b>"
                    End If
                    Gr.Cells(k).Attributes.Add("onmouseover", "Tip('" + strHover + "')")
                    Gr.Cells(k).Attributes.Add("onmouseout", "UnTip('')")
                Else
                    strHover = "<b>Click to Edit</b>"
                End If
            Next

            Gr.Cells(3).Attributes.Add("onmouseover", "Tip('" + lblWord.Text.ToString().Replace("&#", "''") + "')")
            Gr.Cells(3).Attributes.Add("onmouseout", "UnTip('')")
            Gr.Cells(4).Attributes.Add("onmouseover", "Tip('" + lblDesc.Text.ToString().Replace("&#", "''") + "')")
            Gr.Cells(4).Attributes.Add("onmouseout", "UnTip('')")
        Next
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            GetProjectDetails()
            'Started Activity Log Changes
            Try
                objUpIns.InsertLog1(Session("UserId").ToString(), "ProjectManager.aspx", "Clicked on Search Button, Searched Text:" + txtSearch.Text, "", Session("SPROJLogInCount").ToString())

            Catch ex As Exception

            End Try
            'Ended Activity Log Changes
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub grd_popup_details_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles grdProject.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            For i As Integer = 0 To e.Row.Cells.Count - 1
                If i = 1 Then
                    e.Row.Cells(i).ToolTip = "Click to sort by Project ID"
                ElseIf i = 2 Then
                    e.Row.Cells(i).ToolTip = "Click to sort by Project Title"
                ElseIf i = 3 Then
                    e.Row.Cells(i).ToolTip = "Click to sort by Brief Features"
                ElseIf i = 4 Then
                    e.Row.Cells(i).ToolTip = "Click to sort by Full Description"
                ElseIf i = 5 Then
                    e.Row.Cells(i).ToolTip = "Click to sort by Type of Analysis"
                ElseIf i = 6 Then
                    e.Row.Cells(i).ToolTip = "Click to sort by Analyst"
                ElseIf i = 7 Then
                    e.Row.Cells(i).ToolTip = "Click to sort by Owner"
                ElseIf i = 8 Then
                    e.Row.Cells(i).ToolTip = "Click to open Status Display Setting Popup"
                ElseIf i = 9 Then
                    e.Row.Cells(i).ToolTip = "Click to open Milestone Display Setting Popup"
                End If

            Next
        End If
    End Sub

    Protected Sub grdProject_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles grdProject.RowCommand
        If e.CommandName = "EditProject" Then
            Dim lnkEdit As LinkButton = DirectCast(e.CommandSource, LinkButton)
            ' write link button click event code here
            Dim ProjectId As String = lnkEdit.CommandArgument
            lnkEdit.Attributes.Add("onclick", "return ShowPopWindow('ProjectSummary.aspx?ProjectId=" + ProjectId + "');")

        ElseIf e.CommandName = "Sort" Then
            Dim Dts As New DataSet
            Dim dv As DataView
            Dim numberDiv As Integer
            numberDiv = Convert.ToInt16(hidSortId.Value.ToString())
            Dts = Session("ProjData")
            dv = Dts.Tables(0).DefaultView

            If ((numberDiv Mod 2) = 0) Then
                dv.Sort = "VALUE" + " " + "DESC"
            Else
                dv.Sort = "VALUE" + " " + "ASC"
            End If
            numberDiv += 1
            hidSortId.Value = numberDiv.ToString()
            grdProject.DataSource = dv
            grdProject.DataBind()

            ''Started Activity Log Changes
            'Try
            '    objUpIns.InsertLog1(Session("UserId").ToString(), "ProjectManager.aspx", "Sorted Project Report Grid by Expression: VALUE", "", Session("SPROJLogInCount").ToString())

            'Catch ex As Exception

            'End Try
            ''Ended Activity Log Changes

            BindLink()

        ElseIf e.CommandName = "Grid" Then
            Dim lnkEdit As LinkButton = DirectCast(e.CommandSource, LinkButton)
            ' write link button click event code here
            Dim ProjectId As String = lnkEdit.CommandArgument
            'ScriptManager.RegisterStartupScript(Page, Me.GetType(), "JSScript", "OpenNewWindow('ModelInput.aspx?ProjectId=" + ProjectId + "&UserId=17074');", True)

        ElseIf e.CommandName = "Submit" Then
            ' Get Email App Data
            Dim logid As String
            Dim dsid As New DataSet
            Dim objGetDataa As New Selectdata()
            Dim str As String = System.IO.Path.GetFileName(Request.CurrentExecutionFilePath).ToString()
            dsid = objGetDataa.GetPageId(str.ToString())
            pageid = dsid.Tables(0).Rows(0).Item("PAGEID").ToString()
            logid = objGetDataa.GetLogid()


            Dim btnSubmit As Button = DirectCast(e.CommandSource, Button)
            Dim ProjectId As String = btnSubmit.CommandArgument

            Dim ds As New DataSet
            Dim dv As New DataView
            Dim dt As New DataTable
            Dim dsUser As New DataSet

            ds = Session("ProjData")
            dv = ds.Tables(0).DefaultView()
            dv.RowFilter = "PROJECTID=" + ProjectId + ""
            dt = dv.ToTable()

            Dim objGetData As New Selectdata()
            ' Dim ds As New DataSet
            Dim dsNew As New DataSet
            Dim _To As New MailAddressCollection()
            Dim _CC As New MailAddressCollection()
            Dim _BCC As New MailAddressCollection()
            Dim Item As MailAddress

            Dim _To1 As New MailAddressCollection()
            Dim _CC1 As New MailAddressCollection()
            Dim _BCC1 As New MailAddressCollection()
            Dim Item1 As MailAddress

            Dim Email As New EmailConfig()
            objUpIns.UpdateProjectStatus("2", ProjectId, Session("UserId").ToString())
            objUpIns.UpdateSubmitDate("1", ProjectId)

            ds = objGetData.GetMailUserDetails(Session("UserId"))
            Dim strBody As String = String.Empty
            strBody = strBody + "<div style='font-family:Verdana;font-size:12px;margin-top:10px;margin-bottom:10px;'>"
            strBody = strBody + "<p>Dear " + ds.Tables(0).Rows(0).Item("FIRSTNAME").ToString() + " " + ds.Tables(0).Rows(0).Item("LASTNAME").ToString() + "</p>"
            strBody = strBody + "<p>Thank you for choosing SavvyPack Corporation. </p>"
            strBody = strBody + "<p>We have received your submission for the following project: " + dt.Rows(0).Item("TITLE").ToString().Replace("&#", "'") + "  . One of our analysts will contact you shortly. </p>"
            strBody = strBody + "<p>If you have any questions, please feel free to contact us at <a href='mailto:support@savvypack.com'>support@savvypack.com</a> </p>"
            strBody = strBody + "</div> "

            dsNew = objGetData.GetAlliedMemberMail("PROJSUB")
            dsUser = objGetData.GetAnalystUserDetails(Session("SavvyLicenseId"))

            If dsNew.Tables(0).Rows.Count > 0 Then
                Dim _From As New MailAddress(dsNew.Tables(0).Rows(0).Item("FROMADD").ToString(), dsNew.Tables(0).Rows(0).Item("FROMNAME").ToString())
                Dim _Subject As String = dsNew.Tables(0).Rows(0).Item("SUBJECT").ToString()
                'To's

                Item = New MailAddress(ds.Tables(0).Rows(0).Item("EMAILADDRESS").ToString(), ds.Tables(0).Rows(0).Item("FIRSTNAME").ToString() + " " + ds.Tables(0).Rows(0).Item("LASTNAME").ToString())


                _To.Add(Item)


                'BCC's

                For i = 1 To 10
                    ' BCC() 's
                    If dsNew.Tables(0).Rows(0).Item("BCC" + i.ToString()).ToString() <> Nothing Then
                        Item = New MailAddress(dsNew.Tables(0).Rows(0).Item("BCC" + i.ToString()).ToString(), dsNew.Tables(0).Rows(0).Item("BCC" + i.ToString() + "NAME").ToString())
                        _BCC.Add(Item)
                    End If
                    If dsNew.Tables(0).Rows(0).Item("CC" + i.ToString()).ToString() <> Nothing Then
                        Item = New MailAddress(dsNew.Tables(0).Rows(0).Item("CC" + i.ToString()).ToString(), dsNew.Tables(0).Rows(0).Item("CC" + i.ToString() + "NAME").ToString())
                        _CC.Add(Item)
                    End If

                Next

                'Email.SendMail(_From, _To, _CC, _BCC, strBody, _Subject)
                objUpIns.InsertEmailStore(_To.ToString(), logid.ToString(), dsNew.Tables(0).Rows(0).Item("CODE").ToString(), strBody, Session("UserId"), "2", pageid, "PROJECT SUBMITTED", dt.Rows(0).Item("PROJECTID").ToString())


                strBody = String.Empty
                strBody = strBody + "<div style='font-family:Verdana;font-size:12px;margin-top:10px;margin-bottom:10px;'>"
                strBody = strBody + "<p>SavvyPack® User " + ds.Tables(0).Rows(0).Item("FIRSTNAME").ToString() + " " + ds.Tables(0).Rows(0).Item("LASTNAME").ToString() + " has submitted Project, </p>"
                strBody = strBody + "<p>Project Id: " + ProjectId.ToString() + " and Project Title: " + dt.Rows(0).Item("TITLE").ToString().Replace("&#", "'") + " .</p>"
                strBody = strBody + "</div> "


                Dim _From1 As New MailAddress(dsNew.Tables(0).Rows(0).Item("FROMADD").ToString(), dsNew.Tables(0).Rows(0).Item("FROMNAME").ToString())
                Dim _Subject1 As String = dsNew.Tables(0).Rows(0).Item("SUBJECT").ToString()
                'To's
                Item = New MailAddress(dsNew.Tables(0).Rows(0).Item("TOADD").ToString(), dsNew.Tables(0).Rows(0).Item("TONAME").ToString())

                _To1.Add(Item)

                'BCC's

                For i = 1 To 10
                    ' BCC() 's
                    If dsNew.Tables(0).Rows(0).Item("BCC" + i.ToString()).ToString() <> Nothing Then
                        Item = New MailAddress(dsNew.Tables(0).Rows(0).Item("BCC" + i.ToString()).ToString(), dsNew.Tables(0).Rows(0).Item("BCC" + i.ToString() + "NAME").ToString())
                        _BCC1.Add(Item)
                    End If
                    If dsNew.Tables(0).Rows(0).Item("CC" + i.ToString()).ToString() <> Nothing Then
                        Item = New MailAddress(dsNew.Tables(0).Rows(0).Item("CC" + i.ToString()).ToString(), dsNew.Tables(0).Rows(0).Item("CC" + i.ToString() + "NAME").ToString())
                        _CC1.Add(Item)
                    End If

                Next

                'Email.SendMail(_From1, _To1, _CC1, _BCC1, strBody, _Subject1)
                objUpIns.InsertEmailStore(_To1.ToString(), logid.ToString(), dsNew.Tables(0).Rows(0).Item("CODE").ToString(), strBody, Session("UserId"), "3", pageid, "PROJECT SUBMITTED", dt.Rows(0).Item("PROJECTID").ToString())

            End If

            'Started Activity Log Changes
            Try
                objUpIns.InsertLog1(Session("UserId").ToString(), "ProjectManager.aspx", "Submitted Project for Project Id:" + ProjectId.ToString() + "", ProjectId.ToString(), Session("SPROJLogInCount").ToString())
            Catch ex As Exception

            End Try
            'Ended Activity Log Changes

            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Project submitted successfully');", True)
            GetProjectDetails()
        End If
    End Sub

    Protected Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try

            GetProjectDetails()
            BindLink()
        Catch ex As Exception
            lblError.Text = "Error:btnLoad_Click:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub ImageButton_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Try
            Dim imagebut As ImageButton = DirectCast(sender, ImageButton)
            hidDesc.Value = imagebut.CommandArgument
            objUpIns.UpdateMemorizedDetails(Session("UserId"), ddlSize.SelectedValue.ToString(), "", hidDesc.Value, "", "", "", "", "")
            GetProjectDetails()
            'Started Activity Log Changes
            Try
                objUpIns.InsertLog1(Session("UserId").ToString(), "ProjectManager.aspx", "Changed Display Mode to:" + hidDesc.Value, "", Session("SPROJLogInCount").ToString())

            Catch ex As Exception

            End Try
            'Ended Activity Log Changes
        Catch ex As Exception
            lblError.Text = "Error:ImageButton_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub ddlSize_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSize.SelectedIndexChanged
        Try
            objUpIns.UpdateMemorizedDetails(Session("UserId"), ddlSize.SelectedValue.ToString(), "", "", "", "", "", "", "")
            'grdProject.PageSize = ddlSize.SelectedValue.ToString()
            GetProjectDetails()
            'Started Activity Log Changes
            Try
                objUpIns.InsertLog1(Session("UserId").ToString(), "ProjectManager.aspx", "Changed Page Size to:" + ddlSize.SelectedItem.ToString(), "", Session("SPROJLogInCount").ToString())

            Catch ex As Exception

            End Try
            'Ended Activity Log Changes
        Catch ex As Exception
            Throw New Exception("ddlSize_SelectedIndexChanged" + ex.Message)
        End Try
    End Sub

    Protected Sub grdProject_databound(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdProject.DataBound
        Try
            Dim gvr As GridViewRow = grdProject.TopPagerRow

            Dim lb1 As Label = DirectCast(gvr.Cells(0).FindControl("currentpage"), Label)
            lb1.Text = Convert.ToString(grdProject.PageIndex + 1)
            Dim page As Integer() = New Integer(10) {}
            page(0) = grdProject.PageIndex - 2
            page(1) = grdProject.PageIndex - 1
            page(2) = grdProject.PageIndex
            page(3) = grdProject.PageIndex + 1
            page(4) = grdProject.PageIndex + 2
            page(5) = grdProject.PageIndex + 3
            page(6) = grdProject.PageIndex + 4
            page(7) = grdProject.PageIndex + 5
            page(8) = grdProject.PageIndex + 6
            page(9) = grdProject.PageIndex + 7
            page(10) = grdProject.PageIndex + 8
            For i As Integer = 0 To 10
                If i <> 3 Then
                    If page(i) < 1 OrElse page(i) > grdProject.PageCount Then
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
            If grdProject.PageIndex = 0 Then
                Dim lb As LinkButton = DirectCast(gvr.Cells(0).FindControl("linkbutton1"), LinkButton)
                lb.Visible = False
                lb = DirectCast(gvr.Cells(0).FindControl("linkbutton2"), LinkButton)

                lb.Visible = False
            End If
            If grdProject.PageIndex = grdProject.PageCount - 1 Then
                Dim lb As LinkButton = DirectCast(gvr.Cells(0).FindControl("linkbutton3"), LinkButton)
                lb.Visible = False
                lb = DirectCast(gvr.Cells(0).FindControl("linkbutton4"), LinkButton)

                lb.Visible = False
            End If
            If grdProject.PageIndex > grdProject.PageCount - 5 Then
                Dim lbmore As Label = DirectCast(gvr.Cells(0).FindControl("nmore"), Label)
                lbmore.Visible = False
            End If
            If grdProject.PageIndex < 4 Then
                Dim lbmore As Label = DirectCast(gvr.Cells(0).FindControl("pmore"), Label)
                lbmore.Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub lb_command(ByVal sender As Object, ByVal e As CommandEventArgs)
        grdProject.PageIndex = Convert.ToInt32(e.CommandArgument) - 1
        'Started Activity Log Changes
        Try
            objUpIns.InsertLog1(Session("UserId").ToString(), "ProjectManager.aspx", "Changed Page Index to :" + e.CommandArgument, "", Session("SPROJLogInCount").ToString())

        Catch ex As Exception

        End Try
        'Ended Activity Log Changes
        BindGridSession()
    End Sub
    Protected Sub grdProject_rowcreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdProject.RowCreated
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

End Class
