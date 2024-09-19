Imports System.Data
Imports System.Data.OleDb
Imports System
Imports SavvyGetData
Imports SavvyUpInsData
Partial Class OnlineForm_Popup_EmailAnalyst
    Inherits System.Web.UI.Page
    Dim objUpIns As New UpdateInsert()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Try
                lblTag.Text = "Copyright 1997 - " + Now.Year().ToString() + " SavvyPack Corporation"
            Catch ex As Exception
            End Try

            If Not IsPostBack Then
                Session("TabM") = "1"
                btnReply.ToolTip = "Reply"
                btnForw.ToolTip = "Forward"
                Session("Savvy") = "N"
                Session("Message") = Nothing
                hiPrevUserId.Value = ""
                txtSearch.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnSearch.ClientID + "')")
                Try
                    objUpIns.InsertLog1(Session("UserId").ToString(), "MessageManager.aspx", "Opened Message Manager Page", "", Session("SPROJLogInCount").ToString())
                Catch ex As Exception
                End Try
            Else
                If Session("Savvy") = "Y" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "window.open('ProjectManager.aspx','_blank');", True)
                End If
            End If
            BtnLogin.Attributes.Add("onclick", "return OpenLoginPopup();")
            If Session("UserId") <> Nothing Then
                If Request.QueryString("Message").ToString() <> "Nothing" Then
                    BtnLogin.Visible = False
                    BtnSavvy.Visible = True
                    BtnSavvy.ToolTip = "Project Manager"
                    BtnLogout.Visible = True
                    BtnLogout.ToolTip = "Logout"
                    btnCreate.Attributes.Add("onclick", "return ShowPopWindow6('Popup/SendMessage.aspx?Type=Create')")
                    imgReply.Enabled = False
                    imgForw.Enabled = False
                    btnReply.Enabled = False
                    btnForw.Enabled = False

                    BtnSavvy.Attributes.Add("onclick", "return OpenSavvyPack();")
                    lblAnalyst.Text = ""
                    lblTName.Text = ""
                    lblS.Text = ""
                    lblPrj.Text = ""
                    txtContent.Text = ""
                    If Request.QueryString("Message") <> Nothing Then
                        If Request.QueryString("Message") <> "Nothing" Then
                            Session("Message") = Request.QueryString("Message").ToString()
                        End If
                    End If
                    If Session("Message") <> Nothing Then
                        Dim objGetData1 As New Selectdata()
                        Dim dsData1 As New DataSet
                        Dim dvData1 As New DataView
                        Dim dtData1 As New DataTable
                        dsData1 = objGetData1.GetMsgDetails(Session("Message"))
                        dvData1 = dsData1.Tables(0).DefaultView
                        dvData1.RowFilter = "MESSAGEID=" + Session("Message")
                        dtData1 = dvData1.ToTable()
                        If dtData1.Rows(0).Item("SENDTO").ToString() <> Session("UserId") Then
                            If hiPrevUserId.Value = "" Then
                                hiPrevUserId.Value = "1"
                                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alertMsg", "ShowPopWindow('Popup/ConfirmationPopup.aspx');", True)
                            End If
                            Session("Message") = Nothing
                        End If
                    End If
                Else
                    btnCreate.Attributes.Add("onclick", "return ShowPopWindow6('Popup/SendMessage.aspx?Type=Create');")
                    BtnSavvy.Attributes.Add("onclick", "return OpenSavvyPack();")
                    If Request.QueryString("Message") <> Nothing Then
                        If Request.QueryString("Message") <> "Nothing" Then
                            Session("Message") = Request.QueryString("Message").ToString()
                        End If
                    End If
                End If
                If Session("TabM") = "1" Then
                    EmailDetails()
                Else
                    EmailDetailsOutbox()
                End If


            Else
                Session("Message") = Request.QueryString("Message").ToString()
                BtnLogin.Visible = True
                'BtnLogout.Visible = False
                'btnCreate.Enabled = False
                BtnSavvy.Visible = True
                imgReply.Enabled = True
                btnReply.Enabled = True
                imgForw.Enabled = True
                btnForw.Enabled = True
                imgReply.Attributes.Add("onclick", "return OpenLoginPopup();")
                btnReply.Attributes.Add("onclick", "return OpenLoginPopup();")
                imgForw.Attributes.Add("onclick", "return OpenLoginPopup();")
                btnForw.Attributes.Add("onclick", "return OpenLoginPopup();")
                btnCreate.Attributes.Add("onclick", "return OpenLoginPopup();")
                BtnSavvy.Attributes.Add("onclick", "return OpenLoginPopupSavvy();")
                MessageDetails()
            End If

        Catch ex As Exception
        End Try

    End Sub
    Public Sub EmailDetails()
        Dim dsDate As New DataSet
        Dim objGetData As New Selectdata()
        Dim tr As TableRow
        Dim td As TableCell
        Dim lbl As Label
        Dim txt As TextBox
        Dim dsData As New DataSet
        Dim dvData As DataView
        Dim LinkButton As LinkButton
        Try

            dsData = objGetData.GetEmailADetails(Session("USERID"), txtSearch.Text.Trim.Replace("'", "''").ToString())
            Session("dsData") = dsData
            dvData = dsData.Tables(0).DefaultView
            Session("count") = dsData.Tables(0).Rows.Count
            If dsData.Tables(0).Rows.Count > 0 Then
                imgReply.Enabled = True
                btnReply.Enabled = True
                imgForw.Enabled = True
                btnForw.Enabled = True
                lblMsg.Visible = False
                tbleInbox.Visible = True
                tbleInbox.Rows.Clear()
                For i = 0 To dsData.Tables(0).Rows.Count
                    tr = New TableRow
                    For j = 1 To 1
                        td = New TableCell
                        Select Case j

                            Case 1
                                If i = 0 Then

                                Else
                                    LinkButton = New LinkButton
                                    LinkButton.ID = "lnkmail" + i.ToString()

                                    LinkButton.Text = dsData.Tables(0).Rows(i - 1).Item("FROMUSER").ToString() + "</br>" + dsData.Tables(0).Rows(i - 1).Item("Subject").ToString() + "</br>" + " &nbsp" + " &nbsp" + "&nbsp" + " &nbsp" + " &nbsp" + " &nbsp" + " &nbsp" + dsData.Tables(0).Rows(i - 1).Item("Receivedtime").ToString()
                                    'End If
                                    'If dsData.Tables(0).Rows(i - 1).Item("ISVIEWED").ToString() = "N" Then
                                    '    LinkButton.Style.Add("font-weight", "bold")
                                    'End If
                                    If dsData.Tables(0).Rows(i - 1).Item("ISVIEWED").ToString() = "N" Then
                                        LinkButton.Text = dsData.Tables(0).Rows(i - 1).Item("FROMUSER").ToString() + "</br>" + dsData.Tables(0).Rows(i - 1).Item("Subject").ToString() + "</br>" + " &nbsp" + " &nbsp" + " &nbsp" + " &nbsp" + " &nbsp" + dsData.Tables(0).Rows(i - 1).Item("Receivedtime").ToString()
                                        LinkButton.Style.Add("font-weight", "bold")
                                        'tr.BackColor = Drawing.Color.Gray
                                    End If
                                    If Session("TabM") = "1" Then
                                        If Session("Message") Is Nothing Then
                                            If i = 1 Then
                                                lblAnalyst.Text = dsData.Tables(0).Rows(i - 1).Item("FROMUSER").ToString()
                                                lblS.Text = dsData.Tables(0).Rows(i - 1).Item("SUBJECT").ToString()
                                                lblTName.Text = dsData.Tables(0).Rows(i - 1).Item("TOUSER").ToString()

                                                lblName.Text = dsData.Tables(0).Rows(i - 1).Item("FROMUSERNAME").ToString()
                                                lblDate.Text = dsData.Tables(0).Rows(i - 1).Item("RECEIVEDTIME").ToString()
                                                lblPrj.Text = dsData.Tables(0).Rows(i - 1).Item("TITLE").ToString()
                                                lblPrj.Text = dsData.Tables(0).Rows(i - 1).Item("TITLE").ToString()
                                                txtContent.Text = dsData.Tables(0).Rows(i - 1).Item("CONTENT").ToString().Replace("&nbsp;", "")
                                                txtContent.Text = txtContent.Text.ToString().Replace("<br />", Environment.NewLine)
                                                btnReply.Attributes.Add("onclick", "return ShowPopWindow6('Popup/SendMessage.aspx?ProjectId=" + dsData.Tables(0).Rows(i - 1).Item("PROJECTID").ToString() + "&ToUser=" + dsData.Tables(0).Rows(i - 1).Item("SENDFROM").ToString() + "&Type=Reply&MessageId=" + dsData.Tables(0).Rows(i - 1).Item("MESSAGEID").ToString() + "');")
                                                imgReply.Attributes.Add("onclick", "return ShowPopWindow6('Popup/SendMessage.aspx?ProjectId=" + dsData.Tables(0).Rows(i - 1).Item("PROJECTID").ToString() + "&ToUser=" + dsData.Tables(0).Rows(i - 1).Item("SENDFROM").ToString() + "&Type=Reply&MessageId=" + dsData.Tables(0).Rows(i - 1).Item("MESSAGEID").ToString() + "');")
                                                btnForw.Attributes.Add("onclick", "return ShowPopWindow6('Popup/SendMessage.aspx?ProjectId=" + dsData.Tables(0).Rows(i - 1).Item("PROJECTID").ToString() + "&ToUser=" + dsData.Tables(0).Rows(i - 1).Item("SENDFROM").ToString() + "&Type=Forward&MessageId=" + dsData.Tables(0).Rows(i - 1).Item("MESSAGEID").ToString() + "');")
                                                imgForw.Attributes.Add("onclick", "return ShowPopWindow6('Popup/SendMessage.aspx?ProjectId=" + dsData.Tables(0).Rows(i - 1).Item("PROJECTID").ToString() + "&ToUser=" + dsData.Tables(0).Rows(i - 1).Item("SENDFROM").ToString() + "&Type=Forward&MessageId=" + dsData.Tables(0).Rows(i - 1).Item("MESSAGEID").ToString() + "');")

                                            End If
                                        ElseIf Session("Message").ToString() = dsData.Tables(0).Rows(i - 1).Item("MESSAGEID").ToString() Then
                                            lblAnalyst.Text = dsData.Tables(0).Rows(i - 1).Item("FROMUSER").ToString()
                                            lblS.Text = dsData.Tables(0).Rows(i - 1).Item("SUBJECT").ToString()
                                            lblTName.Text = dsData.Tables(0).Rows(i - 1).Item("TOUSER").ToString()

                                            lblName.Text = dsData.Tables(0).Rows(i - 1).Item("FROMUSERNAME").ToString()
                                            lblDate.Text = dsData.Tables(0).Rows(i - 1).Item("RECEIVEDTIME").ToString()
                                            lblPrj.Text = dsData.Tables(0).Rows(i - 1).Item("TITLE").ToString()
                                            lblPrj.Text = dsData.Tables(0).Rows(i - 1).Item("TITLE").ToString()
                                            txtContent.Text = dsData.Tables(0).Rows(i - 1).Item("CONTENT").ToString().Replace("&nbsp;", "")
                                            txtContent.Text = txtContent.Text.ToString().Replace("<br />", Environment.NewLine)
                                            btnReply.Attributes.Add("onclick", "return ShowPopWindow6('Popup/SendMessage.aspx?ProjectId=" + dsData.Tables(0).Rows(i - 1).Item("PROJECTID").ToString() + "&ToUser=" + dsData.Tables(0).Rows(i - 1).Item("SENDFROM").ToString() + "&Type=Reply&MessageId=" + dsData.Tables(0).Rows(i - 1).Item("MESSAGEID").ToString() + "');")
                                            imgReply.Attributes.Add("onclick", "return ShowPopWindow6('Popup/SendMessage.aspx?ProjectId=" + dsData.Tables(0).Rows(i - 1).Item("PROJECTID").ToString() + "&ToUser=" + dsData.Tables(0).Rows(i - 1).Item("SENDFROM").ToString() + "&Type=Reply&MessageId=" + dsData.Tables(0).Rows(i - 1).Item("MESSAGEID").ToString() + "');")
                                            btnForw.Attributes.Add("onclick", "return ShowPopWindow6('Popup/SendMessage.aspx?ProjectId=" + dsData.Tables(0).Rows(i - 1).Item("PROJECTID").ToString() + "&ToUser=" + dsData.Tables(0).Rows(i - 1).Item("SENDFROM").ToString() + "&Type=Forward&MessageId=" + dsData.Tables(0).Rows(i - 1).Item("MESSAGEID").ToString() + "');")
                                            imgForw.Attributes.Add("onclick", "return ShowPopWindow6('Popup/SendMessage.aspx?ProjectId=" + dsData.Tables(0).Rows(i - 1).Item("PROJECTID").ToString() + "&ToUser=" + dsData.Tables(0).Rows(i - 1).Item("SENDFROM").ToString() + "&Type=Forward&MessageId=" + dsData.Tables(0).Rows(i - 1).Item("MESSAGEID").ToString() + "');")

                                        End If
                                    End If
                                    LinkButton.ForeColor = System.Drawing.Color.Black
                                    LinkButton.Font.Size = 11
                                    LinkButton.Width = 260
                                    'LinkButton.Height = 52
                                    LinkButton.CommandArgument = dsData.Tables(0).Rows(i - 1).Item("MESSAGEID").ToString()
                                    td.Controls.Add(LinkButton)

                                    AddHandler LinkButton.Click, AddressOf Me.LinkButton1_Click
                                End If
                                tr.BackColor = Drawing.Color.LightGray
                        End Select
                        tr.Controls.Add(td)
                    Next
                    tbleInbox.Controls.Add(tr)
                Next
            Else
                lblMsg.Visible = True
                tbleInbox.Visible = False
            End If


        Catch ex As Exception

        End Try
    End Sub

    Public Sub EmailDetailsOutbox()
        Dim dsDate As New DataSet
        Dim objGetData As New Selectdata()
        Dim tr As TableRow
        Dim td As TableCell
        Dim lbl As Label
        Dim txt As TextBox
        Dim dsData As New DataSet
        Dim dvData As DataView
        Dim LinkButton As LinkButton
        Try

            dsData = objGetData.GetEmailADetailsSend(Session("USERID"), txtSearch.Text.Trim.Replace("'", "''").ToString())
            Session("dsOutboxData") = dsData

            dvData = dsData.Tables(0).DefaultView
            Session("count") = dsData.Tables(0).Rows.Count
            If dsData.Tables(0).Rows.Count > 0 Then
                lblOMsg.Visible = False
                tbleOutbox.Visible = True
                tbleOutbox.Rows.Clear()
                For i = 0 To dsData.Tables(0).Rows.Count
                    tr = New TableRow
                    For j = 1 To 1
                        td = New TableCell
                        Select Case j

                            Case 1
                                If i = 0 Then

                                Else
                                    LinkButton = New LinkButton
                                    LinkButton.ID = "lnkmailOutbox" + i.ToString()
                                    LinkButton.Text = dsData.Tables(0).Rows(i - 1).Item("TOUSER").ToString() + "</br>" + dsData.Tables(0).Rows(i - 1).Item("Subject").ToString() + "</br>" + " &nbsp" + " &nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + " &nbsp" + " &nbsp" + dsData.Tables(0).Rows(i - 1).Item("SENDTIME").ToString()
                                    If Session("TabM") = "2" Then

                                        If i = 1 Then
                                            lblAnalyst.Text = dsData.Tables(0).Rows(i - 1).Item("FROMUSER").ToString()
                                            lblS.Text = dsData.Tables(0).Rows(i - 1).Item("SUBJECT").ToString()
                                            lblTName.Text = dsData.Tables(0).Rows(i - 1).Item("TOUSER").ToString()

                                            lblName.Text = dsData.Tables(0).Rows(i - 1).Item("TOUSERNAME").ToString()
                                            lblDate.Text = dsData.Tables(0).Rows(i - 1).Item("RECEIVEDTIME").ToString()
                                            lblPrj.Text = dsData.Tables(0).Rows(i - 1).Item("TITLE").ToString()
                                            'lblPrj.Text = dsData.Tables(0).Rows(i - 1).Item("TITLE").ToString()
                                            txtContent.Text = dsData.Tables(0).Rows(i - 1).Item("CONTENT").ToString().Replace("&nbsp;", "")
                                            txtContent.Text = txtContent.Text.ToString().Replace("<br />", Environment.NewLine)
                                            btnReply.Attributes.Add("onclick", "return ShowPopWindow6('Popup/SendMessage.aspx?ProjectId=" + dsData.Tables(0).Rows(i - 1).Item("PROJECTID").ToString() + "&ToUser=" + dsData.Tables(0).Rows(i - 1).Item("SENDTO").ToString() + "&Type=ReplyOutbox&MessageId=" + dsData.Tables(0).Rows(i - 1).Item("MESSAGEID").ToString() + "');")
                                            imgReply.Attributes.Add("onclick", "return ShowPopWindow6('Popup/SendMessage.aspx?ProjectId=" + dsData.Tables(0).Rows(i - 1).Item("PROJECTID").ToString() + "&ToUser=" + dsData.Tables(0).Rows(i - 1).Item("SENDTO").ToString() + "&Type=ReplyOutbox&MessageId=" + dsData.Tables(0).Rows(i - 1).Item("MESSAGEID").ToString() + "');")
                                            btnForw.Attributes.Add("onclick", "return ShowPopWindow6('Popup/SendMessage.aspx?ProjectId=" + dsData.Tables(0).Rows(i - 1).Item("PROJECTID").ToString() + "&ToUser=" + dsData.Tables(0).Rows(i - 1).Item("SENDFROM").ToString() + "&Type=Forward&MessageId=" + dsData.Tables(0).Rows(i - 1).Item("MESSAGEID").ToString() + "');")
                                            imgForw.Attributes.Add("onclick", "return ShowPopWindow6('Popup/SendMessage.aspx?ProjectId=" + dsData.Tables(0).Rows(i - 1).Item("PROJECTID").ToString() + "&ToUser=" + dsData.Tables(0).Rows(i - 1).Item("SENDFROM").ToString() + "&Type=Forward&MessageId=" + dsData.Tables(0).Rows(i - 1).Item("MESSAGEID").ToString() + "');")
                                        End If
                                    End If
                                    LinkButton.ForeColor = System.Drawing.Color.Black
                                    LinkButton.Font.Size = 11
                                    LinkButton.Width = 260
                                    'LinkButton.Height = 52
                                    LinkButton.CommandArgument = dsData.Tables(0).Rows(i - 1).Item("MESSAGEID").ToString()
                                    td.Controls.Add(LinkButton)
                                    AddHandler LinkButton.Click, AddressOf LinkButton2_Click

                                End If
                                tr.BackColor = Drawing.Color.LightGray
                        End Select
                        tr.Controls.Add(td)
                    Next
                    tbleOutbox.Controls.Add(tr)
                Next
            Else
                lblOMsg.Visible = True
                tbleOutbox.Visible = False
            End If

        Catch ex As Exception

        End Try
    End Sub

    Public Sub MessageDetails()

        Dim dsDate As New DataSet
        Dim objGetData As New Selectdata()
        Dim tr As TableRow
        Dim td As TableCell
        Dim lbl As Label
        Dim txt As TextBox
        Dim dsData As New DataSet
        Dim dvData As DataView
        Dim dtData As New DataTable
        Dim LinkButton As LinkButton
        Try

            dsData = objGetData.GetMsgDetails(Session("Message"))
            Session("dsData") = dsData
            dvData = dsData.Tables(0).DefaultView
            dvData.RowFilter = "MESSAGEID=" + Session("Message")
            dtData = dvData.ToTable()
            lblAnalyst.Text = dtData.Rows(0).Item("FROMUSER").ToString()
            lblS.Text = dtData.Rows(0).Item("SUBJECT").ToString()
            lblTName.Text = dtData.Rows(0).Item("TOUSER").ToString()
            Session("PrevUser") = dtData.Rows(0).Item("SENDTO").ToString()
            If dtData.Rows(0).Item("ISREPLY").ToString() <> "" Then
                lblPrj.Text = dtData.Rows(0).Item("TITLE").ToString()
                txtContent.Text = dtData.Rows(0).Item("CONTENT").ToString().Replace("&nbsp;", "")
                txtContent.Text = txtContent.Text.ToString().Replace("<br />", Environment.NewLine)
                ' lblS.Text = "Re: " + dtData.Rows(0).Item("SUBJECT").ToString()
            ElseIf dtData.Rows(0).Item("ISFORWARD").ToString() <> "" Then
                lblPrj.Text = dtData.Rows(0).Item("TITLE").ToString()
                txtContent.Text = dtData.Rows(0).Item("CONTENT").ToString().Replace("&nbsp;", "")
                txtContent.Text = txtContent.Text.ToString().Replace("<br />", Environment.NewLine)
                'lblS.Text = "Fwd: " + dtData.Rows(0).Item("SUBJECT").ToString()
            Else
                lblPrj.Text = dtData.Rows(0).Item("TITLE").ToString()
                txtContent.Text = dtData.Rows(0).Item("CONTENT").ToString().Replace("&nbsp;", "")
                txtContent.Text = txtContent.Text.ToString().Replace("<br />", Environment.NewLine)
                ' lblS.Text = dtData.Rows(0).Item("SUBJECT").ToString()
            End If
            'txtContent.Text = dtData.Rows(0).Item("CONTENT").ToString().Replace("<br />", Environment.NewLine)
            lblName.Text = dtData.Rows(0).Item("USERNAME1").ToString()
            lblDate.Text = dtData.Rows(0).Item("RECEIVEDTIME").ToString()
            objUpIns.MessageViewedInfo(Request.QueryString("Message").ToString())
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim dsData As New DataSet
        Dim dvData As New DataView
        Dim lbl As New Label
        Dim tr As New TableRow
        Dim td As New TableCell
        Dim dtData As New DataTable
        Dim objGetData As New Selectdata()
        Dim objUpIns As New SavvyUpInsData.UpdateInsert
        Try
            lbl = New Label
            Dim bt As LinkButton = CType(sender, LinkButton)

            'dsData = objGetData.GetEmailADetails()
            dsData = Session("dsData")
            dvData = dsData.Tables(0).DefaultView
            dvData.RowFilter = "MESSAGEID=" + bt.CommandArgument
            Session("Message") = bt.CommandArgument
            dtData = dvData.ToTable()
            lblAnalyst.Text = dtData.Rows(0).Item("FROMUSER").ToString()
            lblS.Text = dtData.Rows(0).Item("SUBJECT").ToString()
            lblTName.Text = dtData.Rows(0).Item("TOUSER").ToString()

            lblName.Text = dtData.Rows(0).Item("FROMUSERNAME").ToString()
            lblDate.Text = dtData.Rows(0).Item("RECEIVEDTIME").ToString()
            lblPrj.Text = dtData.Rows(0).Item("TITLE").ToString()
            If dtData.Rows(0).Item("ISREPLY").ToString() <> "" Then
                lblPrj.Text = dtData.Rows(0).Item("TITLE").ToString()
                txtContent.Text = dtData.Rows(0).Item("CONTENT").ToString().Replace("&nbsp;", "")
                txtContent.Text = txtContent.Text.ToString().Replace("<br />", Environment.NewLine)
                'lblS.Text = "Re: " + dtData.Rows(0).Item("SUBJECT").ToString()
            ElseIf dtData.Rows(0).Item("ISFORWARD").ToString() <> "" Then
                lblPrj.Text = dtData.Rows(0).Item("TITLE").ToString()
                txtContent.Text = dtData.Rows(0).Item("CONTENT").ToString().Replace("&nbsp;", "")
                txtContent.Text = txtContent.Text.ToString().Replace("<br />", Environment.NewLine)
                ' lblS.Text = "Fwd: " + dtData.Rows(0).Item("SUBJECT").ToString()

            Else
                lblPrj.Text = dtData.Rows(0).Item("TITLE").ToString()
                txtContent.Text = dtData.Rows(0).Item("CONTENT").ToString().Replace("&nbsp;", "")
                txtContent.Text = txtContent.Text.ToString().Replace("<br />", Environment.NewLine)
                ' lblS.Text = dtData.Rows(0).Item("SUBJECT").ToString()
            End If
            objUpIns.MessageViewedInfo(bt.CommandArgument)

            imgReply.Enabled = True
            btnReply.Enabled = True
            imgForw.Enabled = True
            btnForw.Enabled = True

            imgReply.Attributes.Add("onclick", "return ShowPopWindow6('Popup/SendMessage.aspx?ProjectId=" + dtData.Rows(0).Item("PROJECTID").ToString() + "&ToUser=" + dtData.Rows(0).Item("SENDFROM").ToString() + "&Type=Reply&MessageId=" + bt.CommandArgument + "');")
            btnReply.Attributes.Add("onclick", "return ShowPopWindow6('Popup/SendMessage.aspx?ProjectId=" + dtData.Rows(0).Item("PROJECTID").ToString() + "&ToUser=" + dtData.Rows(0).Item("SENDFROM").ToString() + "&Type=Reply&MessageId=" + bt.CommandArgument + "');")

            imgForw.Attributes.Add("onclick", "return ShowPopWindow6('Popup/SendMessage.aspx?ProjectId=" + dtData.Rows(0).Item("PROJECTID").ToString() + "&ToUser=" + dtData.Rows(0).Item("SENDFROM").ToString() + "&Type=Forward&MessageId=" + bt.CommandArgument + "');")
            btnForw.Attributes.Add("onclick", "return ShowPopWindow6('Popup/SendMessage.aspx?ProjectId=" + dtData.Rows(0).Item("PROJECTID").ToString() + "&ToUser=" + dtData.Rows(0).Item("SENDFROM").ToString() + "&Type=Forward&MessageId=" + bt.CommandArgument + "');")

            EmailDetails()
        Catch ex As Exception

        End Try


    End Sub

    Protected Sub LinkButton2_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim dsData As New DataSet
        Dim dvData As New DataView
        Dim lbl As New Label
        Dim tr As New TableRow
        Dim td As New TableCell
        Dim dtData As New DataTable
        Dim objGetData As New Selectdata()
        Dim objUpIns As New SavvyUpInsData.UpdateInsert
        Try
            lbl = New Label
            Dim bt As LinkButton = CType(sender, LinkButton)
            'dsData = objGetData.GetEmailADetails()
            dsData = Session("dsOutboxData")
            dvData = dsData.Tables(0).DefaultView
            dvData.RowFilter = "MESSAGEID=" + bt.CommandArgument
            'Session("Message1") = bt.CommandArgument
            dtData = dvData.ToTable()
            lblAnalyst.Text = dtData.Rows(0).Item("FROMUSER").ToString()
            lblS.Text = dtData.Rows(0).Item("SUBJECT").ToString()
            lblTName.Text = dtData.Rows(0).Item("TOUSER").ToString()

            lblName.Text = dtData.Rows(0).Item("TOUSERNAME").ToString()
            lblDate.Text = dtData.Rows(0).Item("RECEIVEDTIME").ToString()
            lblPrj.Text = dtData.Rows(0).Item("TITLE").ToString()
            If dtData.Rows(0).Item("ISREPLY").ToString() <> "" Then
                lblPrj.Text = " Re: " + dtData.Rows(0).Item("TITLE").ToString()
                txtContent.Text = dtData.Rows(0).Item("CONTENT").ToString().Replace("&nbsp;", "")
                txtContent.Text = txtContent.Text.ToString().Replace("<br />", Environment.NewLine)
            ElseIf dtData.Rows(0).Item("ISFORWARD").ToString() <> "" Then
                lblPrj.Text = " Fwd: " + dtData.Rows(0).Item("TITLE").ToString()
                txtContent.Text = dtData.Rows(0).Item("CONTENT").ToString().Replace("&nbsp;", "")
                txtContent.Text = txtContent.Text.ToString().Replace("<br />", Environment.NewLine)
            Else
                lblPrj.Text = dtData.Rows(0).Item("TITLE").ToString()
                txtContent.Text = dtData.Rows(0).Item("CONTENT").ToString().Replace("&nbsp;", "")
                txtContent.Text = txtContent.Text.ToString().Replace("<br />", Environment.NewLine)
            End If
            'objUpIns.MessageViewedInfo(bt.CommandArgument)

            imgReply.Enabled = True
            btnReply.Enabled = True
            imgForw.Enabled = True
            btnForw.Enabled = True

            imgReply.Attributes.Add("onclick", "return ShowPopWindow6('Popup/SendMessage.aspx?ProjectId=" + dtData.Rows(0).Item("PROJECTID").ToString() + "&ToUser=" + dtData.Rows(0).Item("SENDTO").ToString() + "&Type=ReplyOutbox&MessageId=" + bt.CommandArgument + "');")
            btnReply.Attributes.Add("onclick", "return ShowPopWindow6('Popup/SendMessage.aspx?ProjectId=" + dtData.Rows(0).Item("PROJECTID").ToString() + "&ToUser=" + dtData.Rows(0).Item("SENDTO").ToString() + "&Type=ReplyOutbox&MessageId=" + bt.CommandArgument + "');")

            imgForw.Attributes.Add("onclick", "return ShowPopWindow6('Popup/SendMessage.aspx?ProjectId=" + dtData.Rows(0).Item("PROJECTID").ToString() + "&ToUser=" + dtData.Rows(0).Item("SENDFROM").ToString() + "&Type=Forward&MessageId=" + bt.CommandArgument + "');")
            btnForw.Attributes.Add("onclick", "return ShowPopWindow6('Popup/SendMessage.aspx?ProjectId=" + dtData.Rows(0).Item("PROJECTID").ToString() + "&ToUser=" + dtData.Rows(0).Item("SENDFROM").ToString() + "&Type=Forward&MessageId=" + bt.CommandArgument + "');")

            'EmailDetailsO()
        Catch ex As Exception

        End Try


    End Sub

    Protected Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        If Request.QueryString("Message").ToString() Is Nothing Then
            Session("Message") = Nothing
        End If
        EmailDetails()
    End Sub

    Protected Sub btnRefresh1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh1.Click
        Session("Message") = Nothing
        EmailDetails()
    End Sub

    Protected Sub BtnLogout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnLogout.Click
        Try
            Dim objUpdate As New LoginUpdateData.Selectdata
            If Session("TID") <> Nothing Then
                'objUpdate.UpdateLogOffDetails2(Session("UserName"), Session("TID"), Session.SessionID)
                If Session("SPROJLogInCount") <> Nothing Then
                    objUpdate.UpdateLogOffDetails2(Session("TID"), Session.SessionID, Session("UserId"), Session("SPROJLogInCount").ToString())
                Else
                    objUpdate.UpdateLogOffDetails2(Session("TID"), Session.SessionID, Session("UserId"), "")

                End If
            End If
            Session("Savvy") = "N"
            Session("SavvyMail") = "N"
            Session("Message") = Nothing
            Session.Abandon()
            Session.RemoveAll()
            Response.Redirect("~/Index.aspx", True)


        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnReply_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReply.Click

        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alertMsg", "alert('Please check the messages from inbox');", True)

    End Sub

    Protected Sub btnForw_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnForw.Click

        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alertMsg", "alert('Please check the messages from inbox');", True)

    End Sub

    Protected Sub tabMessage_ActiveTabChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabMessage.ActiveTabChanged
        Try
            If tabMessage.ActiveTabIndex = 0 Then
                Session("TabM") = "1"
                EmailDetails()
                Try
                    objUpIns.InsertLog1(Session("UserId").ToString(), "MessageManager.aspx", "Clicked on Inbox Tab", "", Session("SPROJLogInCount").ToString())
                Catch ex As Exception
                End Try
            Else
                Session("TabM") = "2"
                EmailDetailsOutbox()
                Try
                    objUpIns.InsertLog1(Session("UserId").ToString(), "MessageManager.aspx", "Clicked on Outbox Tab", "", Session("SPROJLogInCount").ToString())
                Catch ex As Exception
                End Try
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSearch.Click
        Try
            If tabMessage.ActiveTabIndex = 0 Then
                EmailDetails()
            Else
                EmailDetailsOutbox()
            End If
            Try
                objUpIns.InsertLog1(Session("UserId").ToString(), "MessageManager.aspx", "Clicked on Search button image, Searched Text:" + txtSearch.Text.Replace("'", "''"), "", Session("SPROJLogInCount").ToString())
            Catch ex As Exception
            End Try
        Catch ex As Exception

        End Try
    End Sub
End Class
