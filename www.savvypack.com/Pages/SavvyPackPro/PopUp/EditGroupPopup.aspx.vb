Imports System.Data
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
Partial Class Pages_SavvyPackPro_Popup_EditGroupPopup
    Inherits System.Web.UI.Page
    Dim objGetdata As New SavvyProGetData
    Dim objUpIns As New SavvyProUpInsData

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                hidSortIdGrpMngr.Value = "0"
                BindMasterGrp()
                GetGrpManagerData()
            End If
        Catch ex As Exception
            lblError.Text = "Error: Page_Load() " + ex.Message()
        End Try
    End Sub
    Protected Sub BindMasterGrp()
        Dim ds As New DataSet
        Try
            ds = objGetdata.GetMasterGrp(Session("USERID"))
            With ddlMGrp
                .DataValueField = "MGROUPID"
                .DataTextField = "DESCRIPTION"
                .DataSource = ds
                .DataBind()
            End With
        Catch ex As Exception
            lblError.Text = "Error: BindMasterGrp() " + ex.Message()
        End Try
    End Sub

    Protected Sub GetGrpManagerData()
        Dim DsE As DataSet
        Try
            ' Ds = objGetdata.GetGrpDetailsByMasterGrp(btnSrchGM.Text.Trim.ToString(), Session("USERID"), hidMasterGrpID.Value.ToString())
            DsE = objGetdata.GetGrpDetailsAll(txtKeyWordGM.Text.Trim.ToString().Replace("'", "''"), Session("LicenseNo").ToString())
            Session("AllGrpManagerB") = DsE
            lblRcrdCountGM.Text = DsE.Tables(0).Rows.Count

            If DsE.Tables(0).Rows.Count > 0 Then
                lblRF.Visible = True
                grdAllGrpMngr.Visible = True
                grdAllGrpMngr.DataSource = DsE
                grdAllGrpMngr.DataBind()
                BindGrpManagerUsingSession()
            Else
                lblRF.Visible = True
                grdAllGrpMngr.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = "Error:GetGrpManagerData:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub BindGrpManagerUsingSession()
        Try
            Dim Dts As New DataSet
            Dts = Session("AllGrpManagerB")
            grdAllGrpMngr.DataSource = Dts
            grdAllGrpMngr.DataBind()
            BindLink_Grpmngr()
            BindLink()
        Catch ex As Exception
            Response.Write("Error:BindGrpManagerUsingSession:" + ex.Message.ToString())
        End Try
    End Sub

    Protected Sub BindLink_Grpmngr()
        Dim lblUserID As New Label
        Dim lnkGrpNm As New LinkButton
        Dim lblSkuID As New Label
        Dim lblGrpID As New Label
        Try
            For Each Gr As GridViewRow In grdAllGrpMngr.Rows
                lblUserID = grdAllGrpMngr.Rows(Gr.RowIndex).FindControl("lblUserID")
                lnkGrpNm = grdAllGrpMngr.Rows(Gr.RowIndex).FindControl("lnkGrpNm")
                lblSkuID = grdAllGrpMngr.Rows(Gr.RowIndex).FindControl("lblSkuID")
                lblGrpID = grdAllGrpMngr.Rows(Gr.RowIndex).FindControl("lblGrpID")

                'lnkGrpNm.Attributes.Add("onclick", "return ShowGroupPopUp('Popup/SelectGroupPopup.aspx?');")
                '  lnkGrpNm.Attributes.Add("onclick", "return ShowGroupPopUp('Popup/SelectGroupPopup.aspx?MGrpID=" + hidMasterGrpID.Value + "&MtypeID=" + hidMTypeID.Value + "&SkuID=" + lblSkuID.Text + "&lnkInnerText=" + lnkGrpNm.ClientID + "&GrpID=" + lblGrpID.Text + "');")
            Next
        Catch ex As Exception
            lblError.Text = "Error: BindLink_Grpmngr:" + ex.Message()
        End Try
    End Sub

    Protected Sub btnSrchGM_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSrchGM.Click
        Try
            GetGrpManagerData()
        Catch ex As Exception
            lblError.Text = "Error:btnSrchGM_Click" + ex.Message.ToString()
        End Try
    End Sub

#Region "User Grid Event"

    Protected Sub BindLink()
        Dim lblUserID As New Label
        Try
            For Each Gr As GridViewRow In grdAllGrpMngr.Rows
                lblUserID = grdAllGrpMngr.Rows(Gr.RowIndex).FindControl("lblUserID")

                If Session("UserId") = lblUserID.Text Then
                    Gr.Enabled = True
                Else
                    Gr.Cells(0).Enabled = False
                    Gr.BackColor = Drawing.Color.LightGray
                End If

            Next
        Catch ex As Exception
            lblError.Text = "Error: BindLink:" + ex.Message()
        End Try
    End Sub

    Protected Sub grdAllGrpMngr_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdAllGrpMngr.Sorting
        Dim Dts As New DataSet
        Dim dv As DataView
        Dim numberDiv As Integer
        Dim dsSorted As New DataSet()
        Try
            numberDiv = Convert.ToInt16(hidSortIdGrpMngr.Value.ToString())
            Dts = Session("AllGrpManagerB")
            dv = Dts.Tables(0).DefaultView

            If ((numberDiv Mod 2) = 0) Then
                dv.Sort = e.SortExpression + " " + "DESC"
            Else
                dv.Sort = e.SortExpression + " " + "ASC"
            End If
            numberDiv += 1
            hidSortIdGrpMngr.Value = numberDiv.ToString()
            grdAllGrpMngr.DataSource = dv
            grdAllGrpMngr.DataBind()
            BindLink()

            dsSorted.Tables.Add(dv.ToTable())
            Session("AllGrpManagerB") = dsSorted
            '  BindLink_Grpmngr()
        Catch ex As Exception
            Response.Write("Error:grdAllGrpMngr_Sorting:" + ex.Message.ToString())
        End Try
    End Sub

    Protected Sub grdAllGrpMngr_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdAllGrpMngr.PageIndexChanging
        Try
            grdAllGrpMngr.PageIndex = e.NewPageIndex
            BindGrpManagerUsingSession()
        Catch ex As Exception
            Response.Write("Error:grdRfp_PageIndexChanging:" + ex.Message.ToString())
        End Try
    End Sub

    Protected Sub ddlPageCountGM_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageCountGM.SelectedIndexChanged
        Try
            grdAllGrpMngr.PageSize = ddlPageCountGM.SelectedItem.ToString()
            BindGrpManagerUsingSession()
        Catch ex As Exception
            Response.Write("Error:ddlPageCountGM_SelectedIndexChanged:" + ex.Message.ToString())
        End Try
    End Sub

#End Region

    Protected Sub btnCreateGrp_Click(sender As Object, e As EventArgs) Handles btnCreateGrp.Click
        Try
            If txtGrpName.Text = "" Then
                ScriptManager.RegisterStartupScript(Page, Me.GetType(), "Create", "alert(' Please enter Group Details.');", True)
            Else
                objUpIns.AddGrpByFormat(txtGrpName.Text.Trim().Replace("'", "''").ToString(), txtGrpDescription.Text.Trim().Replace("'", "''").ToString(), Session("UserId"), _
                                    ddlMGrp.SelectedValue.ToString())
                ScriptManager.RegisterStartupScript(Page, Me.GetType(), "Create", "alert('Group Created successfully.');", True)

            End If
            'ScriptManager.RegisterStartupScript(Page, Me.GetType(), "JSScript", "ClosePage();", True)
            txtGrpName.Text = ""
            txtGrpDescription.Text = ""
            GetGrpManagerData()
        Catch ex As Exception
            lblError.Text = "Error:btnCreateGrp_Click" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub grdAllGrpMngr_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdAllGrpMngr.SelectedIndexChanged
        Dim dsE As New DataSet
        Dim ds As New DataSet
        Dim hid As New HiddenField
        Try

            Dim MGID As New Integer
            MGID = Convert.ToInt32(grdAllGrpMngr.SelectedDataKey.Value)

            hidGID.Value = MGID
            btnCreateGrp.Visible = False
            btnEditRFP.Visible = True
            btnCancleGrp.Visible = True
            ddlMGrp.Enabled = True

            ds = objGetdata.GetSubGrpByMaster1(MGID, Session("UserID").ToString())
            If ds.Tables(0).Rows.Count > 0 Then
                txtGrpName.Text = ds.Tables(0).Rows(0).Item("GRPNM").ToString()
                txtGrpDescription.Text = ds.Tables(0).Rows(0).Item("GRPDES").ToString()
                ddlMGrp.SelectedValue = ds.Tables(0).Rows(0).Item("GRPID").ToString()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnCancleGrp_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancleGrp.Click
        Try
            btnCreateGrp.Visible = True
            btnEditRFP.Visible = False
            btnCancleGrp.Visible = False
            txtGrpName.Text = ""
            txtGrpDescription.Text = ""
            ddlMGrp.SelectedIndex = "0"
            ddlMGrp.Enabled = True
            'GetTypeDetails()
        Catch ex As Exception
            lblError.Text = "Error: btnCancel_Click() " + ex.Message()
        End Try
    End Sub

    Protected Sub btnEditRFP_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEditRFP.Click
        Dim GID As String
        Try
            GID = Convert.ToInt32(grdAllGrpMngr.SelectedDataKey.Value)
            objUpIns.UpdateGroup(txtGrpName.Text.Trim().Replace("'", "''").ToString(), txtGrpDescription.Text.Trim().Replace("'", "''").ToString(), Session("UserId"), GID, ddlMGrp.SelectedValue.ToString())
            ScriptManager.RegisterStartupScript(Page, Me.GetType(), "Create", "alert('Group Updated successfully.');", True)
            btnCreateGrp.Visible = True
            btnEditRFP.Visible = False
            btnCancleGrp.Visible = False
            txtGrpName.Text = ""
            txtGrpDescription.Text = ""
            GetGrpManagerData()
        Catch ex As Exception
            lblError.Text = "Error: btnEditRFP_Click() " + ex.Message()
        End Try
    End Sub
End Class
