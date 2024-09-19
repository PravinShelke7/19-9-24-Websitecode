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
Partial Class OnlineForm_Popup_SendMail
    Inherits System.Web.UI.Page
    Dim objUpIns As New UpdateInsert()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        hidMsgType.Value = Request.QueryString("Type").ToString()

        If Not IsPostBack Then
            hidToUser.Value = "0"
            hidProjectId.Value = "0"
            hidProjDes.Value = "Select Project"
            hidUserDes.Value = "Select User"
            If hidMsgType.Value = "Analyst" Or hidMsgType.Value = "Owner" Or hidMsgType.Value = "Reply" Or hidMsgType.Value = "ReplyOutbox" Then
                lblTo.Visible = True
                lnkTo.Visible = False
                lblProjR.Visible = True
                lnkPrjR.Visible = False
                hidToUser.Value = Request.QueryString("ToUser").ToString()
                hidProjectId.Value = Request.QueryString("ProjectId").ToString()
                objUpIns.InsertLog1(Session("UserId").ToString(), "SendMessage.aspx", "Opened Send Message Popup for Analyst/Owner/Reply/Outbox for projectId:" + hidProjectId.Value + "", "", Session("SPROJLogInCount").ToString())
            ElseIf hidMsgType.Value = "Forward" Then
                lblTo.Visible = False
                lnkTo.Visible = True
                lblProjR.Visible = True
                lnkPrjR.Visible = False
                'hidToUser.Value = Request.QueryString("ToUser").ToString()
                hidProjectId.Value = Request.QueryString("ProjectId").ToString()
                objUpIns.InsertLog1(Session("UserId").ToString(), "SendMessage.aspx", "Opened Send Message Popup to forward message", "", Session("SPROJLogInCount").ToString())
            Else
                lblTo.Visible = False
                lnkTo.Visible = True
                lblProjR.Visible = False
                lnkPrjR.Visible = True
                objUpIns.InsertLog1(Session("UserId").ToString(), "SendMessage.aspx", "Opened Send Message Popup for creating new message", "", Session("SPROJLogInCount").ToString())
            End If
            ProjectDetails()
        End If

    End Sub

    Protected Sub btnSend_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSend.Click
        Dim objUpIns As New UpdateInsert
        Dim MsgId As Integer = 0
        Dim ds As New DataSet
        Dim objGetData As New Selectdata()
        Dim dv As New DataView
        Dim dt As New DataTable
        Try
            If hidToUser.Value = "0" Or hidProjectId.Value = "0" Or txtSub.Text = "" Or txtContent.Text = "" Then
                If hidToUser.Value = "0" Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "Alert", "alert('Select User');", True)
                ElseIf hidProjectId.Value = "0" Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "Alert", "alert('Select Project');", True)
                ElseIf txtSub.Text = "" Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "Alert", "alert('Enter Message Subject');", True)
                ElseIf txtContent.Text = "" Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "Alert", "alert('Enter Message Content');", True)
                End If
                lnkPrjR.Text = hidProjDes.Value
                lnkTo.Text = hidUserDes.Value
            Else
                Dim str As String = txtContent.Text.Replace(Environment.NewLine, "<br />")
                MsgId = objUpIns.InsertMessageInfo(Session("UserId"), hidToUser.Value, hidProjectId.Value, str.Replace("'", "''"), txtSub.Text.Replace("'", "''"), hidReplyId.Value, hidForwId.Value)

                If MsgId <> 0 Then
                    Dim _To As New MailAddressCollection()
                    Dim _From As MailAddress
                    Dim _CC As New MailAddressCollection()
                    Dim _BCC As New MailAddressCollection()
                    Dim Item As MailAddress
                    Dim Email As New EmailConfig()
                    Dim dsMail As New DataSet

                    Dim UserId As String = hidToUser.Value
                    ds = objGetData.GetMailUserDetails(UserId)
                    dv = ds.Tables(0).DefaultView
                    dsMail = objGetData.GetAlliedMemberMail("MSGTOOL")

                    Dim strBody As String = String.Empty
                    strBody = strBody + "<div style='font-family:Verdana;font-size:12px;margin-top:10px;margin-bottom:10px;'>"
                    strBody = strBody + "<p>You received new message, for </p>"
                    If hidMsgType.Value = "Create" Then
                        strBody = strBody + "<p>Regarding Project Title: " + hidProjDes.Value + " .</p><br />"
                    Else
                        strBody = strBody + "<p>Regarding Project Title: " + lblProjR.Text + " .</p><br />"
                    End If

                    strBody = strBody + "See Message <a style='font-family:verdana;' href='http://www.savvypack.com/OnlineForm/MessageManager.aspx?Message=" + MsgId.ToString() + "'>click here</a>"
                    strBody = strBody + "</div> "
                    Dim _Subject As String = "Savvypack Project Message"

                    'from
                    _From = New MailAddress(dsMail.Tables(0).Rows(0).Item("FROMADD").ToString(), dsMail.Tables(0).Rows(0).Item("FROMNAME").ToString())


                    'To's
                    dv.RowFilter = "USERID=" + hidToUser.Value
                    dt = dv.ToTable()
                    If dt.Rows.Count > 0 Then
                        Item = New MailAddress(dt.Rows(0).Item("EMAILADDRESS").ToString(), dt.Rows(0).Item("FIRSTNAME").ToString() + " " + dt.Rows(0).Item("LASTNAME").ToString())
                        _To.Add(Item)
                    End If

                    Email.SendMail(_From, _To, _CC, _BCC, strBody, _Subject)
                    objUpIns.UpdateReceiveTime(MsgId)
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "close", "ClosePopup();", True)
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "close1", "ClosePage();", True)
                    'Started Activity Log Changes
                    Try
                        objUpIns.InsertLog1(Session("UserId").ToString(), "SendMessage.aspx", "Message send for projectId:" + hidProjectId.Value + "  To:" + _To.ToString(), "", Session("SPROJLogInCount").ToString())

                    Catch ex As Exception

                    End Try
                    'Ended Activity Log Changes
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Sub ProjectDetails()
        Dim dsProjId As New DataSet
        Dim dsUser As New DataSet
        Dim dsProj As New DataSet
        Dim objGetData As New Selectdata()
        Dim dvProj As New DataView
        Dim dtProj As New DataTable
        Try
            If hidMsgType.Value = "Analyst" Then
                hidProjectId.Value = Request.QueryString("ProjectId").ToString()
                'dsProj = objGetData.GetEditProjectDetails(hidProjectId.Value)
                dsProj = Session("dsPA")
                dvProj = dsProj.Tables(0).DefaultView
                dvProj.RowFilter = "PROJECTID=" + hidProjectId.Value
                dtProj = dvProj.ToTable()
                If dtProj.Rows.Count > 0 Then
                    lblTo.Text = dtProj.Rows(0).Item("ANALYSTEMAILID").ToString()
                    lblProjR.Text = dtProj.Rows(0).Item("TITLE").ToString()
                End If
            ElseIf hidMsgType.Value = "Reply" Then
                hidReplyId.Value = Request.QueryString("MessageId").ToString()
                dsProj = objGetData.GetMessageDetails(hidReplyId.Value)
                lblTo.Text = dsProj.Tables(0).Rows(0).Item("FROMUSER").ToString()
                lblProjR.Text = dsProj.Tables(0).Rows(0).Item("TITLE").ToString()
			    txtContent.Text = "<br /><br /><br /><br />On" + " " + dsProj.Tables(0).Rows(0).Item("Receivedtime").ToString().Replace("&nbsp;", "") + ", " + dsProj.Tables(0).Rows(0).Item("FROMUSERNAME").ToString().Replace("&nbsp;", "") + " " + "wrote:" + "<br />" + dsProj.Tables(0).Rows(0).Item("CONTENT").ToString().Replace("&nbsp;", "")
                txtContent.Text = txtContent.Text.ToString().Replace("<br />", Environment.NewLine)
               
			   txtSub.Text = "Re: " + dsProj.Tables(0).Rows(0).Item("SUBJECT").ToString()
            ElseIf hidMsgType.Value = "ReplyOutbox" Then
                hidReplyId.Value = Request.QueryString("MessageId").ToString()
                dsProj = objGetData.GetMessageDetails(hidReplyId.Value)
                lblTo.Text = dsProj.Tables(0).Rows(0).Item("TOUSER").ToString()
                lblProjR.Text = dsProj.Tables(0).Rows(0).Item("TITLE").ToString()
                txtContent.Text = dsProj.Tables(0).Rows(0).Item("CONTENT").ToString().Replace("<br />", Environment.NewLine)
                txtContent.Text = "<br /><br /><br /><br />On" + " " + dsProj.Tables(0).Rows(0).Item("Receivedtime").ToString().Replace("&nbsp;", "") + ", " + dsProj.Tables(0).Rows(0).Item("FROMUSERNAME").ToString().Replace("&nbsp;", "") + " " + "wrote:" + "<br />" + dsProj.Tables(0).Rows(0).Item("CONTENT").ToString().Replace("&nbsp;", "")
                txtContent.Text = txtContent.Text.ToString().Replace("<br />", Environment.NewLine)
                txtSub.Text = dsProj.Tables(0).Rows(0).Item("SUBJECT").ToString()

                txtSub.Text = "Re: " + dsProj.Tables(0).Rows(0).Item("SUBJECT").ToString()
            ElseIf hidMsgType.Value = "Owner" Then
                hidProjectId.Value = Request.QueryString("ProjectId").ToString()
                'dsProj = objGetData.GetEditProjectDetails(hidProjectId.Value)
                dsProj = Session("dsPA")
                dvProj = dsProj.Tables(0).DefaultView
                dvProj.RowFilter = "PROJECTID=" + hidProjectId.Value
                dtProj = dvProj.ToTable()
                If dtProj.Rows.Count > 0 Then
                    lblTo.Text = dtProj.Rows(0).Item("OWNER").ToString()
                    lblProjR.Text = dtProj.Rows(0).Item("TITLE").ToString()
                End If
            ElseIf hidMsgType.Value = "Create" Then

            ElseIf hidMsgType.Value = "Forward" Then
                hidForwId.Value = Request.QueryString("MessageId").ToString()
                dsProj = objGetData.GetMessageDetails(hidForwId.Value)
                'lblTo.Text = dsProj.Tables(0).Rows(0).Item("FROMUSER").ToString()
                lblProjR.Text = dsProj.Tables(0).Rows(0).Item("TITLE").ToString()
                hidProjDes.Value = dsProj.Tables(0).Rows(0).Item("TITLE").ToString()
                'txtContent.Text = dsProj.Tables(0).Rows(0).Item("CONTENT").ToString().Replace("<br />", Environment.NewLine)
				txtContent.Text = "<br /><br /><br /><br />On" + " " + dsProj.Tables(0).Rows(0).Item("Receivedtime").ToString().Replace("&nbsp;", "") + ", " + dsProj.Tables(0).Rows(0).Item("FROMUSERNAME").ToString().Replace("&nbsp;", "") + " " + "wrote:" + "<br />" + dsProj.Tables(0).Rows(0).Item("CONTENT").ToString().Replace("&nbsp;", "")
                txtContent.Text = txtContent.Text.ToString().Replace("<br />", Environment.NewLine)
                
                txtSub.Text = "Fwd: " + dsProj.Tables(0).Rows(0).Item("SUBJECT").ToString()
            End If



        Catch ex As Exception
            ' lblError.Text = "Error:ProjectDetails:" + ex.Message.ToString() + ""
        End Try

    End Sub
End Class
