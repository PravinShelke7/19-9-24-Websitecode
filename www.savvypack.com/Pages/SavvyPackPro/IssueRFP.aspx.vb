﻿Imports System.Data
Imports System.Data.OleDb
Imports System
Imports SavvyProGetData
Imports SavvyProUpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Imports System.Net.Mail
Imports System.Net
Partial Class Pages_SavvyPackPro_IssueRFP
    Inherits System.Web.UI.Page
    Dim objGetdata As New SavvyProGetData
    Dim objUpIns As New SavvyProUpInsData
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try

            pnlValidate.Visible = True
            grdUsers.Enabled = False

            lblFooter.Text = "Copyright 1997 - " + Now.Year().ToString() + " SavvyPack Corporation"
            If Session("SBack") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If
            If Session("USERID") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If

            If Not IsPostBack Then
                hidSortIdBMVendor.Value = "0"
                ChkExistingRfp()
                ' btnSend.Enabled = False

            End If

        Catch ex As Exception
            lblError.Text = "Error: Page_Load() " + ex.Message()
        End Try
    End Sub

    Protected Sub ChkExistingRfp()
        Dim DsCheckRfp As New DataSet()
        Try
            If Session("hidRfpID") = Nothing Then
                DsCheckRfp = objGetdata.GetLatestRFPbyLicenseID(Session("LicenseNo"), Session("USERID"))
                If DsCheckRfp.Tables(0).Rows.Count > 0 Then
                    GetRfpDetails(DsCheckRfp.Tables(0).Rows(0).Item("ID").ToString())
                Else
                    RfpDetail.Visible = False
                    tabIssueRFP.Enabled = False
                End If
            Else
                GetRfpDetails(Session("hidRfpID"))
            End If
        Catch ex As Exception
            lblError.Text = "Error: ChkExistingRfp() " + ex.Message()
        End Try
    End Sub

    Protected Sub GetRfpDetails(ByVal RfpID As String)
        Dim DsRfpdet As New DataSet()
        Try
            If RfpID <> "" Or RfpID <> "0" Then
                DsRfpdet = objGetdata.GetRFPbyID(RfpID)
                If DsRfpdet.Tables(0).Rows.Count > 0 Then
                    RfpDetail.Visible = True
                    lnkSelRFP.Text = DsRfpdet.Tables(0).Rows(0).Item("DES1").ToString()
                    lblSelRfpID.Text = DsRfpdet.Tables(0).Rows(0).Item("ID").ToString()
                    Session("hidRfpID") = lblSelRfpID.Text
                    lblSelRfpDes.Text = DsRfpdet.Tables(0).Rows(0).Item("DES1").ToString()
                    lblType.Text = DsRfpdet.Tables(0).Rows(0).Item("TYPEDESC").ToString()
                    'tabIssueRFP.Enabled = True
                    If Session("USERID") = DsRfpdet.Tables(0).Rows(0).Item("USERID").ToString() Then
                        tabIssueRFP.Enabled = True
                    Else
                        tabIssueRFP.Enabled = False
                    End If
                    loadTab()
                Else
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('System isn't able to find RFPID. Please try again.');", True)
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error: GetRfpDetails() " + ex.Message()
        End Try
    End Sub

    Protected Sub loadTab()
        Try
            GetUsersList()
            GetIssueList()
        Catch ex As Exception
            lblError.Text = "Error: GetRfpDetails() " + ex.Message()
        End Try
    End Sub
    Protected Sub GetIssueList()
        Dim Ds As New DataSet
        Try
            Ds = objGetdata.GetIssueList(Session("hidRfpID"), Session("UserId"))
            If Ds.Tables(0).Rows.Count > 0 Then
                If Ds.Tables(0).Rows(0).Item("STATUSID").ToString() = "3" Or Ds.Tables(0).Rows(0).Item("STATUSID").ToString() = "6" Then
                    btnSend.Enabled = False
                    btnValidate.Enabled = False
                    btnSend.ForeColor = Drawing.Color.DarkGray
                    btnValidate.ForeColor = Drawing.Color.DarkGray
                Else
                    btnSend.Enabled = False
                End If
            Else
                btnSend.Enabled = False
                btnValidate.Enabled = True
            End If
        Catch ex As Exception
            lblError.Text = "Error:BindUser:" + ex.Message.ToString()
        End Try
    End Sub

#Region "Vendor Config"

    Protected Sub GetUsersList()
        Dim Ds As New DataSet
        Try
            Ds = objGetdata.GetUserList(txtKey.Text.Trim.ToString(), Session("hidRfpID"))
            Session("BMVendorList") = Ds
            lblRecondCnt.Text = Ds.Tables(0).Rows.Count

            If Ds.Tables(0).Rows.Count > 0 Then
                lblNOVendor.Visible = False
                lblDesc.Visible = True
                grdUsers.Visible = True
                grdUsers.DataSource = Ds
                grdUsers.DataBind()
                btnValidate.Enabled = True
            Else
                lblDesc.Visible = False
                lblNOVendor.Visible = True
                grdUsers.Visible = False
                btnSend.Enabled = False
                btnValidate.Enabled = False
            End If
        Catch ex As Exception
            lblError.Text = "Error:BindUser:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub BindUsersListUsingSession()
        Try
            Dim Dts As New DataSet
            Dts = Session("BMVendorList")
            grdUsers.DataSource = Dts
            grdUsers.DataBind()
        Catch ex As Exception
            Response.Write("Error:BindUsersListUsingSession:" + ex.Message.ToString())
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            GetUsersList()
        Catch ex As Exception
            lblError.Text = "Error:btnSearch_Click" + ex.Message.ToString()
        End Try
    End Sub

#Region "User Grid Event"

    Protected Sub grdUsers_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdUsers.Sorting
        Dim Dts As New DataSet
        Dim dv As DataView
        Dim numberDiv As Integer
        Dim dsSorted As New DataSet()
        Try
            numberDiv = Convert.ToInt16(hidSortIdBMVendor.Value.ToString())
            Dts = Session("BMVendorList")
            dv = Dts.Tables(0).DefaultView

            If ((numberDiv Mod 2) = 0) Then
                dv.Sort = e.SortExpression + " " + "DESC"
            Else
                dv.Sort = e.SortExpression + " " + "ASC"
            End If
            numberDiv += 1
            hidSortIdBMVendor.Value = numberDiv.ToString()
            grdUsers.DataSource = dv
            grdUsers.DataBind()

            dsSorted.Tables.Add(dv.ToTable())
            Session("BMVendorList") = dsSorted

        Catch ex As Exception
            Response.Write("Error:grdUsers_Sorting:" + ex.Message.ToString())
        End Try
    End Sub

    Protected Sub grdUsers_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdUsers.PageIndexChanging
        Try
            grdUsers.PageIndex = e.NewPageIndex
            BindUsersListUsingSession()
        Catch ex As Exception
            Response.Write("Error:grdUsers_PageIndexChanging:" + ex.Message.ToString())
        End Try
    End Sub

    Protected Sub ddlPageCountC_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageCountC.SelectedIndexChanged
        Try
            grdUsers.PageSize = ddlPageCountC.SelectedItem.ToString()
            BindUsersListUsingSession()
        Catch ex As Exception
            Response.Write("Error:ddlPageCountC_SelectedIndexChanged:" + ex.Message.ToString())
        End Try
    End Sub

    Protected Sub grdUsers_databound(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdUsers.DataBound
        Try
            Dim gvr As GridViewRow = grdUsers.TopPagerRow

            Dim lb1 As Label = DirectCast(gvr.Cells(0).FindControl("currentpage"), Label)
            lb1.Text = Convert.ToString(grdUsers.PageIndex + 1)
            Dim page As Integer() = New Integer(10) {}
            page(0) = grdUsers.PageIndex - 2
            page(1) = grdUsers.PageIndex - 1
            page(2) = grdUsers.PageIndex
            page(3) = grdUsers.PageIndex + 1
            page(4) = grdUsers.PageIndex + 2
            page(5) = grdUsers.PageIndex + 3
            page(6) = grdUsers.PageIndex + 4
            page(7) = grdUsers.PageIndex + 5
            page(8) = grdUsers.PageIndex + 6
            page(9) = grdUsers.PageIndex + 7
            page(10) = grdUsers.PageIndex + 8
            For i As Integer = 0 To 10
                If i <> 3 Then
                    If page(i) < 1 OrElse page(i) > grdUsers.PageCount Then
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
            If grdUsers.PageIndex = 0 Then
                Dim lb As LinkButton = DirectCast(gvr.Cells(0).FindControl("linkbutton1"), LinkButton)
                lb.Visible = False
                lb = DirectCast(gvr.Cells(0).FindControl("linkbutton2"), LinkButton)

                lb.Visible = False
            End If
            If grdUsers.PageIndex = grdUsers.PageCount - 1 Then
                Dim lb As LinkButton = DirectCast(gvr.Cells(0).FindControl("linkbutton3"), LinkButton)
                lb.Visible = False
                lb = DirectCast(gvr.Cells(0).FindControl("linkbutton4"), LinkButton)

                lb.Visible = False
            End If
            If grdUsers.PageIndex > grdUsers.PageCount - 5 Then
                Dim lbmore As Label = DirectCast(gvr.Cells(0).FindControl("nmore"), Label)
                lbmore.Visible = False
            End If
            If grdUsers.PageIndex < 4 Then
                Dim lbmore As Label = DirectCast(gvr.Cells(0).FindControl("pmore"), Label)
                lbmore.Visible = False
            End If
        Catch ex As Exception
            Response.Write("Error:grdUsers_databound:" + ex.Message.ToString())
        End Try
    End Sub

    Protected Sub lb_command_VC(ByVal sender As Object, ByVal e As CommandEventArgs)
        grdUsers.PageIndex = Convert.ToInt32(e.CommandArgument) - 1
        BindUsersListUsingSession()
    End Sub

    Protected Sub grdUsers_rowcreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdUsers.RowCreated
        If e.Row.RowType = DataControlRowType.Pager Then
            Dim gvr As GridViewRow = e.Row
            Dim lb As LinkButton = DirectCast(gvr.Cells(0).FindControl("p0"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_VC
            lb = DirectCast(gvr.Cells(0).FindControl("p1"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_VC
            lb = DirectCast(gvr.Cells(0).FindControl("p2"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_VC
            lb = DirectCast(gvr.Cells(0).FindControl("p4"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_VC
            lb = DirectCast(gvr.Cells(0).FindControl("p5"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_VC
            lb = DirectCast(gvr.Cells(0).FindControl("p6"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_VC
            lb = DirectCast(gvr.Cells(0).FindControl("p7"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_VC
            lb = DirectCast(gvr.Cells(0).FindControl("p8"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_VC
            lb = DirectCast(gvr.Cells(0).FindControl("p9"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_VC
            lb = DirectCast(gvr.Cells(0).FindControl("p10"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_VC
        End If
    End Sub

#End Region


    Protected Sub chkchangedvendor(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim check As CheckBox = TryCast(sender, CheckBox)
            Dim gvrow As GridViewRow = TryCast(check.NamingContainer, GridViewRow)
            Dim VendorID As String = CType(gvrow.FindControl("lblMUsrID"), Label).Text

            'If check.Checked = True Then
            '    objUpIns.ConnectRFPVendor(VendorID.ToString(), Session("hidRfpID"), "3", Session("UserId"))
            'Else
            '    objUpIns.DisconnectRFPVendor(VendorID.ToString(), Session("hidRfpID"))
            'End If
            ScriptManager.RegisterStartupScript(Page, Me.GetType(), "JSScript", "ClosePage();", True)
            GetUsersList()
        Catch ex As Exception

        End Try
    End Sub


#End Region

#Region "Hidden Buttons"

    Protected Sub btnHidRFPCreate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHidRFPCreate.Click
        Try
            If hidRfpID.Value <> "" Or hidRfpID.Value <> "0" Then
                tabIssueRFP.Enabled = True
                lnkSelRFP.Text = hidRfpNm.Value
                GetRfpDetails(hidRfpID.Value)
            End If
        Catch ex As Exception
            lblError.Text = "Error: btnHidRFPCreate_Click() " + ex.Message()
        End Try
    End Sub

    Protected Sub btnRefreshVList_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefreshVList.Click
        Try
            GetUsersList()
        Catch ex As Exception
            lblError.Text = "Error: btnRefreshVList_Click() " + ex.Message()
        End Try
    End Sub

#End Region

    Protected Sub btnValidate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnValidate.Click
        Dim ds As New DataSet
        Dim dsT As New DataSet
        Dim dsV As New DataSet
        Dim flag As Boolean = False

        Try
            ds = objGetdata.GetRFPValidateList(Session("hidRfpID"))
            dsT = objGetdata.GetTerms(Session("hidRFPID"), Session("UserId"))

            'pt changes

            If dsT.Tables(0).Rows.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then

                    For i = 0 To ds.Tables(0).Rows.Count - 1
                        If i = 0 Then
                            For j As Integer = 0 To dsT.Tables(0).Rows.Count - 1
                                If dsT.Tables(0).Rows(j).Item("DESCRIPTION").ToString() = "" Then
                                    flag = True
                                End If
                            Next

                        End If
                        If ds.Tables(0).Rows(i).Item("SKUID").ToString() = "" Or ds.Tables(0).Rows(i).Item("PRICEOPTIONID").ToString() = "" Or ds.Tables(0).Rows(i).Item("OPOPTIONID").ToString() = "" Or ds.Tables(0).Rows(i).Item("VENDORID").ToString() = "" Or flag = True Then
                            'If dsT.Tables(0).Rows(i).Item("DESCRIPTION").ToString() = "" Then
                            'ScriptManager.RegisterStartupScript(Page, Me.GetType(), "AlertV", "alert('Validation incomplete. Please check if your spec is assigned to RFP,vendor is connected to RFP or Terms  fields are entered or no..');", True)
                            'End If

                            Dim stralt As String = String.Empty
                            If ds.Tables(0).Rows(i).Item("SKUID").ToString() = "" Then
                                stralt += "\n -SKU not connected"
                            End If
                            If ds.Tables(0).Rows(i).Item("PRICEOPTIONID").ToString() = "" Then
                                stralt += "\n -Price Option not connected."
                            End If
                            If ds.Tables(0).Rows(i).Item("OPOPTIONID").ToString() = "" Then
                                stralt += "\n -Operating Option not connected."

                            End If
                            If ds.Tables(0).Rows(i).Item("VENDORID").ToString() = "" Then
                                stralt += "\n -Vendor not connected."
                            End If
                            If flag = True Then
                                stralt += "\n -Term not connected."
                            End If
                            ScriptManager.RegisterStartupScript(Page, Me.GetType(), "AlertV", "alert('Validation incomplete.\n Below items are incomplete please connect all items to issue RFP." + stralt.ToString() + "');", True)

                        Else
                            btnSend.Enabled = True
                            btnValidate.Enabled = False
                            ScriptManager.RegisterStartupScript(Page, Me.GetType(), "Alert", "alert('Validation done successfully,now you can send email to vendor');", True)
                        End If
                    Next
                Else
                    ScriptManager.RegisterStartupScript(Page, Me.GetType(), "AlertV", "alert('Validation incomplete.\n Below items are incomplete please connect all items to issue RFP.\n -SKU not connected.');", True)
                End If
            Else
                ScriptManager.RegisterStartupScript(Page, Me.GetType(), "AlertV", "alert('Validation incomplete.\n Below items are incomplete please connect all items to issue RFP.\n -Term not connected.');", True)
            End If

            'If dsT.Tables(0).Rows.Count > 0 Then
            '    For i = 0 To ds.Tables(0).Rows.Count - 1
            '        If ds.Tables(0).Rows(i).Item("SKUID").ToString() = "" Or ds.Tables(0).Rows(i).Item("VENDORID").ToString() = "" Or dsT.Tables(0).Rows(i).Item("DESCRIPTION").ToString() = "" Then
            '            'If dsT.Tables(0).Rows(i).Item("DESCRIPTION").ToString() = "" Then
            '            ScriptManager.RegisterStartupScript(Page, Me.GetType(), "AlertV", "alert('Validation incomplete.Please check if your spec is assigned to RFP,vendor is connected to RFP or Terms  fields are entered or no..');", True)
            '            'End If

            '        Else
            '            btnSend.Enabled = True
            '            btnValidate.Enabled = False
            '            ScriptManager.RegisterStartupScript(Page, Me.GetType(), "Alert", "alert('Validation done successfully,now you can send email to vendor');", True)
            '        End If
            '    Next
            'End If

            'end pt changes

            'If dsT.Tables(0).Rows.Count > 0 Then
            '    For i = 0 To ds.Tables(0).Rows.Count - 1
            '        For j = 0 To ds.Tables(0).Rows.Count - 1
            '            If ds.Tables(0).Rows(i).Item("SPECID").ToString() = "" Or ds.Tables(0).Rows(i).Item("VENDORID").ToString() = "" or dsT.Tables(0).Rows(j + 1).Item("DESCRIPTION").ToString() = "" Then
            '                ScriptManager.RegisterStartupScript(Page, Me.GetType(), "AlertV", "alert('Validation incomplete.Please check if your spec is assigned to RFP,vendor is connected to RFP or Terms  fields are entered or no..');", True)
            '            ElseIf dsT.Tables(0).Rows(j + 1).Item("DESCRIPTION").ToString() = "" Then
            '                ScriptManager.RegisterStartupScript(Page, Me.GetType(), "AlertV", "alert('Validation incomplete.Please check if your spec is assigned to RFP,vendor is connected to RFP or Terms  fields are entered or no..');", True)
            '            Else
            '                btnSend.Enabled = True
            '                btnValidate.Enabled = False
            '                ScriptManager.RegisterStartupScript(Page, Me.GetType(), "Alert", "alert('Validation done successfully,now you can send email to vendor');", True)
            '            End If
            '        Next
            '    Next
            'End If

        Catch ex As Exception
            lblError.Text = "Error: btnValidate " + ex.Message()
        End Try
    End Sub

    Protected Sub btnSend_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSend.Click
        Dim count As Integer = Convert.ToInt32(Session("count"))
        Dim ProjId As New Integer
        Dim ds As New DataSet
        Dim dsV As New DataSet
        Dim dv As New DataView
        Dim dt As New DataTable
        Dim dsUser As New DataSet
        Dim dsF As New DataSet
        Dim DsExistRFP As DataSet
        Try
            Dim Elogid As String
            Dim dsid As New DataSet
            Dim str As String = System.IO.Path.GetFileName(Request.CurrentExecutionFilePath).ToString()
            Elogid = objGetdata.GetELogid()

            DsExistRFP = objGetdata.GetIssueList(Session("hidRfpID"), Session("UserId"))
            dsV = objGetdata.GetVendorD(Session("hidRfpID").ToString())
            dv = dsV.Tables(0).DefaultView()
            dv.RowFilter = "ID=" + Session("hidRfpID").ToString() + ""
            dt = dv.ToTable()

            Dim _To As New MailAddressCollection()
            Dim _From As MailAddress
            Dim _CC As New MailAddressCollection()
            Dim _BCC As New MailAddressCollection()
            Dim Item As MailAddress
            Dim Email As New EmailConfig()
            Dim UserId As String = Session("UserId").ToString()
            ds = objGetdata.GetMailUserDetails(UserId)
            dv = ds.Tables(0).DefaultView

            For i = 1 To dsV.Tables(0).Rows.Count

                Dim strBody As String = String.Empty
                strBody = String.Empty
                strBody = strBody + "<div style='font-family:Verdana;font-size:12px;margin-top:10px;margin-bottom:10px;'>"
                strBody = strBody + "<p>SavvyPackPro® Buyer " + ds.Tables(0).Rows(0).Item("FIRSTNAME").ToString() + " " + ds.Tables(0).Rows(0).Item("LASTNAME").ToString() + " has Issued RFP, </p>"
                strBody = strBody + "<p>RFP Id: " + Session("hidRFPID").ToString() + " and Description: " + dt.Rows(0).Item("DES1").ToString().Replace("&#", "'") + " .</p>"
                strBody = strBody + "</div> "
                'strBody = strBody + "See Message <a style='font-family:verdana;' href='http://localhost:51788/www.savvypack.com/OnlineForm/MessageManager.aspx?Message=" + MsgId.ToString() + "'>click here</a>"
                strBody = strBody + "</div> "
                Dim _Subject As String = "SavvypackPro Message"

                'from
                _From = New MailAddress(ds.Tables(0).Rows(0).Item("EMAILADDRESS").ToString(), ds.Tables(0).Rows(0).Item("FIRSTNAME").ToString() + "" + ds.Tables(0).Rows(0).Item("LASTNAME").ToString())


                'To's
                If dt.Rows.Count > 0 Then
                    Item = New MailAddress(dsV.Tables(0).Rows(i - 1).Item("EMAILADDRESS").ToString(), dsV.Tables(0).Rows(i - 1).Item("FIRSTNAME").ToString() + " " + dsV.Tables(0).Rows(i - 1).Item("LASTNAME").ToString())
                    _To.Add(Item)
                End If

                Try
                    objUpIns.UpdateIssuedTerm(dt.Rows(0).Item("ID").ToString(), Elogid.ToString())
                    objUpIns.InsertStatusLog_Issue(Session("hidRfpID"), "3", Session("UserId"), "2", dsV.Tables(0).Rows(i - 1).Item("VENDORID").ToString())

                    Email.SendMail(_From, _To, _CC, _BCC, strBody, _Subject)

                Catch ex As Exception
                    ScriptManager.RegisterStartupScript(Page, Me.GetType(), "Alert", "alert('email not send');", True)
                End Try
            Next
            '------Storing Email data-----------
            ' objUpIns.InsertEmailStore(_To.ToString(), Elogid.ToString(), "ISSUERFP", strBody, Session("UserId"), "Issue RFP", dt.Rows(0).Item("RFPID").ToString())
            btnSend.Enabled = False
            ScriptManager.RegisterStartupScript(Page, Me.GetType(), "Alert", "alert('Email send successfully to vendor');", True)

            'Email.SendMail(_From1, _To1, _CC1, _BCC1, strBody, _Subject1)


        Catch ex As Exception

        End Try
    End Sub
End Class
