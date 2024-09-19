Imports System.Data
Imports System.Data.OleDb
Imports System
Imports MoldE1GetData
Imports MoldE1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Imports System.Net.Mail
Imports System.Net
Partial Class Pages_MoldEcon1_Default2
    Inherits System.Web.UI.Page

#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_PAGES_ECON1_DEFAULT")
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
            If Session("Back") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If

            If Not IsPostBack Then
                SetSessions()
                GetPCaseGroupDetails()
                'GetBCaseDetails()
                'GetPCaseDetails()
                btSerach.Attributes.Add("onclick", "return ShowPopWindow('PopUp/CaseSearch.aspx?Id=ddlPCase&GrpID=" + ddlCaseGroup.SelectedValue + "')")
                hidGroupId.Value = "0"
                hidApprovedCase.Value = "0"
                hidPropCase.Value = "0"

                hidPropCaseD.Value = "Select case"
                hidGroupCaseD.Value = "All Groups and All Cases"

            End If

            SetToolButton()

            If Session("MoldE1LicAdmin") = "Y" Then
                btnToolBox.Enabled = True
                btnToolBox.Style.Add("color", "Grey")
                btnToolBox.Style.Add("background-color", "#DED9DD")
                btnToolBox.Attributes.Add("onclick", "return false;")
                btnSubmit.Visible = False
            End If


        Catch ex As Exception
            lblError.Text = "Error:Page_Load:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub SetSessions()
        Dim objGetData As New MoldE1GetData.Selectdata()
        Dim ds As New DataSet()
        Try

            ds = objGetData.GetUserDetails(Session("ID"))
            Session("UserId") = ds.Tables(0).Rows(0).Item("USERID").ToString()
            Session("MoldE1UserRole") = ds.Tables(0).Rows(0).Item("USERROLE").ToString()
            Session("MoldE1ServiceRole") = ds.Tables(0).Rows(0).Item("SERVIECROLE").ToString()
            Session("MoldE1UserName") = ds.Tables(0).Rows(0).Item("USERNAME").ToString()
            Session("MoldE1ToolUserName") = ds.Tables(0).Rows(0).Item("TOOLUSERNAME").ToString()
            Session("MoldE1MaxCaseCount") = ds.Tables(0).Rows(0).Item("MAXCASECOUNT").ToString()
            'Set for License Administrator
            Session("MoldE1LicAdmin") = ds.Tables(0).Rows(0).Item("ISIADMINLICUSR").ToString()
            Session("MoldE1LicenseId") = ds.Tables(0).Rows(0).Item("LICENSEID").ToString()

        Catch ex As Exception
            lblError.Text = "Error:SetSessions:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetPCaseGroupDetails()
        Dim Dts As New DataSet
        Dim objGetData As New MoldE1GetData.Selectdata
        Dim lst As New ListItem

        Try
            lst.Text = "-------------All Groups and All Cases------------"
            lst.Value = "0"
            lst.Selected = True
            ddlCaseGroup.Items.Add(lst)
            ddlCaseGroup.AppendDataBoundItems = True

            'Dts = objGetData.GetSavedCaseAsperUser(Session("E1UserName").ToString(), "-1")
            Dts = objGetData.GetGroupCaseDetails(Session("USERID").ToString())
            If Dts.Tables(0).Rows.Count > 0 Then
                ddlCaseGroup.DataSource = Dts
                ddlCaseGroup.DataValueField = "GROUPID"
                ddlCaseGroup.DataTextField = "GROUPDES"
                ddlCaseGroup.DataBind()
            End If

        Catch ex As Exception
            lblError.Text = "Error:GetPCaseGroupDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub


    Protected Sub SetToolButton()
        Try
            If Session("MoldE1ServiceRole") <> "ReadWrite" Then
                btnToolBox.Enabled = False
            End If
        Catch ex As Exception
            lblError.Text = "Error:SetToolButton:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnBCase_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBCase.Click
        Try
            If hidApprovedCase.Value = "" Or hidApprovedCase.Value = "0" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinB", "alert('Please select Approved Case');", True)
                lnkApprovedCase.Text = "Select case"
            Else
                lnkApprovedCase.Text = hidApprovedCaseD.Value
                Session("MoldE1CaseId") = hidApprovedCase.Value
                Session("CaseType") = "Approved"
                If Not objRefresh.IsRefresh Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinB", "OpenNewWindow('Assumptions/CaseManager.aspx');", True)
                End If
            End If

        Catch ex As Exception
            lblError.Text = "Error:btnBCase_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnPCase_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPCase.Click
        Dim ds As New DataSet
        Dim objgetdata As New MoldE1GetData.Selectdata()
        Dim dsSt As New DataSet()
        Dim CaseId As String = ""
        Dim User As String = ""
        Try
            If hidPropCase.Value = "" Or hidPropCase.Value = "0" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinB", "alert('Please select Proprietary Case');", True)
                lnkPropCase.Text = "Select case"
            Else
                Session("MoldE1CaseId") = hidPropCase.Value ' ddlPCase.SelectedValue.ToString()
                lnkPropCase.Text = hidPropCaseD.Value
                lnkCaseGroup.Text = hidGroupCaseD.Value
                Session("CaseType") = "Prop"

                CaseId = Session("MoldE1CaseId")
                User = Session("MoldE1UserName")
                dsSt = objgetdata.GetStatusForSister("MoldE1", CaseId, User)
                If dsSt.Tables(0).Rows.Count > 0 Then
                    Session("Status") = "True"
                Else
                    Session("Status") = "False"
                End If

                If hidPropCaseSt.Value = "5" Then
                    btnAdminSubmit.Attributes.Add("style", "display:block")
                Else
                    btnAdminSubmit.Attributes.Add("style", "display:none")
                End If

                If Not objRefresh.IsRefresh Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "OpenNewWindow('Assumptions/CaseManager.aspx');", True)
                End If
            End If

        Catch ex As Exception
            lblError.Text = "Error:btnPCase_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnToolBox_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnToolBox.Click
        Try
            Response.Redirect("Tools/Tools2.aspx", True)
        Catch ex As Exception
            lblError.Text = "Error:btnToolBox_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Dim obj As New ToolCCS
        Dim ds As New DataSet()
        Try

            obj.CaseRename(txtCaseid.Value, txtCaseDe1.Text.Trim().ToString().Replace("'", "''"), txtCaseDe2.Text.Trim().ToString().Replace("'", "''"), txtCaseDe3.Text.Trim.ToString().Replace("'", "''"), "MoldE1ConnectionString")
            'GetPCaseDetails()
            lnkPropCase.Text = "Select Case"
            hidPropCase.Value = "0"

            lnkCaseGroup.Text = hidGroupCaseD.Value
            divRename.Style.Add("Display", "none")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlCaseGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCaseGroup.SelectedIndexChanged
        Try
            'lnkPropCase.Attributes.Add("onclick", "return ShowPopWindow('PopUp/CasesSearch1.aspx?Id=ddlPCase&GrpID=" + ddlCaseGroup.SelectedValue + "')")
            hidGroupId.Value = ddlCaseGroup.SelectedValue
            'GetPCaseDetails()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btRename_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btRename.Click
        Try
            If hidPropCase.Value = "0" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinB", "alert('Please select Proprietary Case');", True)
            Else
                lnkPropCase.Text = hidPropCaseD.Value
                divRename.Style.Add("Display", "block")
                GetCaseDetails()
            End If


        Catch ex As Exception

        End Try
    End Sub
    Protected Sub GetCaseDetails()
        Dim objGetData As New MoldE1GetData.Selectdata()
        Dim ds As New DataSet
        Dim CaseId As New Integer
        Try
            CaseId = CInt(hidPropCase.Value)
            ds = objGetData.GetCaseDetails(CaseId.ToString())
            txtCaseDe1.Text = ds.Tables(0).Rows(0).Item("CASEDE1").ToString()
            txtCaseDe2.Text = ds.Tables(0).Rows(0).Item("CASEDE2").ToString()
            txtCaseDe3.Text = ds.Tables(0).Rows(0).Item("CASEDE3").ToString()


        Catch ex As Exception
            lblError.Text = "Error:GetCaseDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try
            Dim objGetData As New MoldE1GetData.Selectdata()
            Dim objUpdateData As New MoldE1UpInsData.UpdateInsert()
            Dim ds As New DataSet()
            'Update Permissionscases Status and Log
            If hidPropCase.Value = "0" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinB", "alert('Please select Proprietary Case');", True)
                lnkPropCase.Text = "Select case"
            Else
                objUpdateData.PermissionStatusUpdate(hidPropCase.Value, Session("MoldE1UserName").ToString())
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinB", "alert('Case " + hidPropCase.Value + " submitted successfully.');", True)
                'Sending Email
                SendEmailStatus(hidPropCase.Value)

                'Getting Updated Case Status
                ds = objGetData.GetPropCasesById(Session("UserId"), hidPropCase.Value)
                If ds.Tables(0).Rows.Count > 0 Then
                    hidPropCaseD.Value = ds.Tables(0).Rows(0).Item("CaseId").ToString() + ":" + ds.Tables(0).Rows(0).Item("CaseDe1").ToString() + "&nbsp;&nbsp;" + ds.Tables(0).Rows(0).Item("CaseDe2").ToString() + "&nbsp;&nbsp;&nbsp;" + ds.Tables(0).Rows(0).Item("STATUS").ToString()
                End If

                lnkPropCase.Text = hidPropCaseD.Value
                hidPropCaseSt.Value = "1"
            End If


        Catch ex As Exception

        End Try
    End Sub
    Protected Sub SendEmailStatus(ByVal CaseId As String)
        Try
            'Get Email Config Details
            Dim objE1GetData As New MoldE1GetData.Selectdata()
            Dim ds As New DataSet()
            Dim dsUserData As New DataSet
            Dim objGetData As New UsersGetData.Selectdata
            Dim link, EmailLink As String
            Dim strBodyData As String

            ds = objGetData.GetEmailConfigDetails("Y")
            dsUserData = objE1GetData.GetPermissionStatus(CaseId, Session("MoldE1UserName").ToString())
            If ds.Tables(0).Rows.Count > 0 Then
                If dsUserData.Tables(0).Rows.Count > 0 Then
                    'mail for verification
                    EmailLink = ds.Tables(0).Rows(0).Item("URL").ToString()
                    'Sending mail
                    strBodyData = GetEmailBodyData(EmailLink, dsUserData)
                    SendEmail(strBodyData)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Sub SendEmail(ByVal strBody As String)
        Try
            Dim objGetData As New UsersGetData.Selectdata
            Dim ds As New DataSet
            ds = objGetData.GetAlliedMemberMail("SUBFORAPP")
            If ds.Tables(0).Rows.Count > 0 Then
                Dim i As Integer
                Dim _To As New MailAddressCollection()
                Dim _From As New MailAddress(ds.Tables(0).Rows(0).Item("FROMADD").ToString(), ds.Tables(0).Rows(0).Item("FROMNAME").ToString())
                Dim _CC As New MailAddressCollection()
                Dim _BCC As New MailAddressCollection()
                Dim Item As MailAddress
                Dim Email As New EmailConfig()
                Dim _Subject As String = ds.Tables(0).Rows(0).Item("SUBJECT").ToString()

                'To's
                Item = New MailAddress(ds.Tables(0).Rows(0).Item("TOADD").ToString(), ds.Tables(0).Rows(0).Item("TONAME").ToString())
                _To.Add(Item)

                For i = 1 To 10
                    ' BCC() 's
                    If ds.Tables(0).Rows(0).Item("BCC" + i.ToString()).ToString() <> Nothing Then
                        Item = New MailAddress(ds.Tables(0).Rows(0).Item("BCC" + i.ToString()).ToString(), ds.Tables(0).Rows(0).Item("BCC" + i.ToString() + "NAME").ToString())
                        _BCC.Add(Item)
                    End If
                    If ds.Tables(0).Rows(0).Item("CC" + i.ToString()).ToString() <> Nothing Then
                        Item = New MailAddress(ds.Tables(0).Rows(0).Item("CC" + i.ToString()).ToString(), ds.Tables(0).Rows(0).Item("CC" + i.ToString() + "NAME").ToString())
                        _CC.Add(Item)
                    End If
                Next



                Email.SendMail(_From, _To, _CC, _BCC, strBody, _Subject)
            End If



        Catch ex As Exception
            lblError.Text = "Error:SendEmail:" + ex.Message.ToString()
        End Try
    End Sub
    Public Function GetEmailBodyData(ByVal link As String, ByVal ds As DataSet) As String
        Dim StrSqlBody As String = ""
        Try
            StrSqlBody = "<div style='font-family:Verdana;'>  "
            StrSqlBody = StrSqlBody + "<div style='font-family:Verdana;font-size:15px;margin-top:10px;margin-bottom:10px;font-weight:bold'>Request for Approval</div>"

            StrSqlBody = StrSqlBody + "<div style='font-family:Verdana;font-size:15px;margin-top:10px;margin-bottom:10px;font-weight:bold'></div>"
            StrSqlBody = StrSqlBody + "<table style='font-family:Verdana;width:700px;font-size:12px;border-collapse:collapse' border='1' bordercolor='black' cellpadding='0' cellspacing='0'>  "
            StrSqlBody = StrSqlBody + "<tr style='background-color:#336699;height:20px;text-align:center;color:white'> "
            StrSqlBody = StrSqlBody + "<td><b>Module</b></td> "
            StrSqlBody = StrSqlBody + "<td><b>Case Id</b></td> "
            StrSqlBody = StrSqlBody + "<td><b>Status</b></td> "
            StrSqlBody = StrSqlBody + "<td><b>Date</b></td> "
            StrSqlBody = StrSqlBody + "<td><b>Action By</b></td> "
            StrSqlBody = StrSqlBody + "<td><b>Comments</b></td> "
            StrSqlBody = StrSqlBody + "</tr> "
            StrSqlBody = StrSqlBody + "<tr style='height:20px;text-align:center'> "
            StrSqlBody = StrSqlBody + "<td>Econ1</b></td> "
            StrSqlBody = StrSqlBody + "<td>" + ds.Tables(0).Rows(0).Item("CASEID").ToString() + "</b></td> "
            StrSqlBody = StrSqlBody + "<td>Submitted For Approval</b></td> "
            StrSqlBody = StrSqlBody + "<td>" + ds.Tables(0).Rows(0).Item("DATED").ToString() + "</b></td> "
            StrSqlBody = StrSqlBody + "<td>" + ds.Tables(0).Rows(0).Item("ACTIONBY").ToString() + "</b></td> "
            StrSqlBody = StrSqlBody + "<td>" + ds.Tables(0).Rows(0).Item("COMMENTS").ToString() + "</b></td> "
            StrSqlBody = StrSqlBody + "</tr> "
            StrSqlBody = StrSqlBody + "</table> "
            StrSqlBody = StrSqlBody + "</div> "
            Return StrSqlBody
        Catch ex As Exception
            lblError.Text = "Error:GetEmailBodyData:" + ex.Message.ToString()
            Return StrSqlBody
        End Try
    End Function


    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            'lnkPropCase.Text = hidPropCaseD.Value
            'lnkCaseGroup.Text = hidGroupCaseD.Value
            hidPropCase.Value = "0"
            hidGroupId.Value = "0"
            hidPropCaseD.Value = "Select case"
            lnkPropCase.Text = hidPropCaseD.Value
            lnkCaseGroup.Text = hidGroupCaseD.Value

            divRename.Style.Add("display", "none")
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub btnAdminSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdminSubmit.Click
        Try
            Dim objGetData As New MoldE1GetData.Selectdata()
            Dim ds As New DataSet()
            Dim objUpdateData As New MoldE1UpInsData.UpdateInsert()

            'Update Permissionscases Status and Log
            If hidPropCase.Value = "0" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinB", "alert('Please select Proprietary Case');", True)
                lnkPropCase.Text = "Select case"
            Else
                objUpdateData.PermissionStatusUpdate(hidPropCase.Value, Session("MoldE1UserName").ToString())
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinB", "alert('Case " + hidPropCase.Value + " submitted successfully.');", True)
                'Sending Email
                SendEmailStatus(hidPropCase.Value)

                'Getting Updated Case Status
                ds = objGetData.GetPropCasesById(Session("UserId"), hidPropCase.Value)
                If ds.Tables(0).Rows.Count > 0 Then
                    hidPropCaseD.Value = ds.Tables(0).Rows(0).Item("CaseId").ToString() + ":" + ds.Tables(0).Rows(0).Item("CaseDe1").ToString() + "&nbsp;&nbsp;" + ds.Tables(0).Rows(0).Item("CaseDe2").ToString() + "&nbsp;&nbsp;&nbsp;" + ds.Tables(0).Rows(0).Item("STATUS").ToString()
                End If

                lnkPropCase.Text = hidPropCaseD.Value
                hidPropCaseSt.Value = "1"
            End If


        Catch ex As Exception

        End Try
    End Sub
End Class
