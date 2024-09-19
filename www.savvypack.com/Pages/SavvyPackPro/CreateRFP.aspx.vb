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
Partial Class Pages_SavvyPackPro_CreateRFP
    Inherits System.Web.UI.Page
    Dim objGetdata As New SavvyProGetData
    Dim objUpIns As New SavvyProUpInsData
    Dim LicId As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                hidSortIdBSpec.Value = "0"
                GetTypeDetails()
                LicId = Session("LicenseNo")
                GetPrefDetails()
            End If
        Catch ex As Exception
            lblError.Text = "Error: Page_Load() " + ex.Message()
        End Try
    End Sub

#Region "User Define Delegates"

    Protected Sub GetTypeDetails()
        Dim ds As New DataSet()
        Try
            ds = objGetdata.GetTypeDetails_Buyer(txtKey.Text.Trim.ToString(), Session("LicenseNo"))
            Session("CRFP") = ds
            lblRecondCnt.Text = ds.Tables(0).Rows.Count
            If ds.Tables(0).Rows.Count > 0 Then
                grdTDetails.DataSource = ds
                grdTDetails.DataBind()
                grdTDetails.Visible = True
                lblSpecNoFound.Visible = False
                BindLink()
            Else
                lblSpecNoFound.Visible = True
                grdTDetails.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = "Error: GetTypeDetails() " + ex.Message()
        End Try
    End Sub
    Protected Sub BindLink()
        Dim lblUserID As New Label
        Dim ds As DataSet
        Dim lblMouseover As New Label
        Dim lblStatusId As New Label
        Dim lnkSelect As New LinkButton
        Try
            For Each Gr As GridViewRow In grdTDetails.Rows
                lblUserID = grdTDetails.Rows(Gr.RowIndex).FindControl("lblUserID")
                lnkSelect = grdTDetails.Rows(Gr.RowIndex).FindControl("lnkSelect")
                lblStatusId = grdTDetails.Rows(Gr.RowIndex).FindControl("lblStatusId")
                ds = Session("CRFP")
                If Session("UserId") = lblUserID.Text Then

                    If lblStatusId.Text = "1" Or lblStatusId.Text = "2" Then
                        Gr.Enabled = True
                    Else
                        lblMouseover.Text = "You cannot edit this RFP,because it has already being issued"
                        'lnkSelect.Attributes.Add("onmouseover", "Tip('" + lblMouseover.Text + "')")
                        'lnkSelect.Attributes.Add("onmouseout", "UnTip('')")
                        Gr.Cells(1).Attributes.Add("onmouseover", "Tip('" + lblMouseover.Text + "')")
                        Gr.Cells(1).Attributes.Add("onmouseout", "UnTip('')")
                        Gr.Cells(0).Enabled = False
                        Gr.BackColor = Drawing.Color.LightGray
                        lnkSelect.Enabled = False
                    End If
                    '  Next
                    ' Gr.Enabled = True
                Else
                    Gr.Cells(0).Enabled = False
                    Gr.BackColor = Drawing.Color.LightGray
                End If

            Next
        Catch ex As Exception
            lblError.Text = "Error: BindLink:" + ex.Message()
        End Try
    End Sub
    Protected Sub BindRfpUsingSession()
        Try
            Dim Dts As New DataSet
            Dts = Session("CRFP")
            grdTDetails.DataSource = Dts
            grdTDetails.DataBind()
            lblSpecNoFound.Visible = False
        Catch ex As Exception
            lblError.Text = "Error: BindRfpUsingSession:" + ex.Message()
        End Try
    End Sub

#End Region

#Region "User Grid Event"

    Protected Sub grdTDetails_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdTDetails.Sorting
        Dim Dts As New DataSet
        Dim dv As DataView
        Dim numberDiv As Integer
        Dim dsSorted As New DataSet()
        Try
            numberDiv = Convert.ToInt16(hidSortIdBSpec.Value.ToString())
            Dts = Session("CRFP")
            dv = Dts.Tables(0).DefaultView

            If ((numberDiv Mod 2) = 0) Then
                dv.Sort = e.SortExpression + " " + "DESC"
            Else
                dv.Sort = e.SortExpression + " " + "ASC"
            End If
            numberDiv += 1
            hidSortIdBSpec.Value = numberDiv.ToString()
            grdTDetails.DataSource = dv
            grdTDetails.DataBind()

            dsSorted.Tables.Add(dv.ToTable())
            Session("CRFP") = dsSorted

        Catch ex As Exception
            Response.Write("Error:grdRfp_Sorting:" + ex.Message.ToString())
        End Try
    End Sub

    Protected Sub grdTDetails_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdTDetails.PageIndexChanging
        Try
            grdTDetails.PageIndex = e.NewPageIndex
            BindRfpUsingSession()
        Catch ex As Exception
            Response.Write("Error:grdRfp_PageIndexChanging:" + ex.Message.ToString())
        End Try
    End Sub

    Protected Sub ddlPageCountC_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageCountC.SelectedIndexChanged
        Try
            grdTDetails.PageSize = ddlPageCountC.SelectedItem.ToString()
            BindRfpUsingSession()
        Catch ex As Exception
            Response.Write("Error:ddlPageCountC_SelectedIndexChanged:" + ex.Message.ToString())
        End Try
    End Sub

#End Region

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            GetTypeDetails()
        Catch ex As Exception
            lblError.Text = "Error:btnSearch_Click" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub grdTDetails_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdTDetails.SelectedIndexChanged
        Dim ds As New DataSet
        Try
            Dim RfID As New Integer
            RfID = Convert.ToInt32(grdTDetails.SelectedDataKey.Value)
            hidRfpID.Value = RfID
            btnCreateRFP.Visible = False
            btnEditRFP.Visible = True
            btnCancel.Visible = True

            ds = objGetdata.GetRfpDetByID(hidRfpID.Value)
            If ds.Tables(0).Rows.Count > 0 Then
                txtRFPNm.Text = ds.Tables(0).Rows(0).Item("Name").ToString()
                txtRfpDes.Text = ds.Tables(0).Rows(0).Item("Des").ToString()
                If ds.Tables(0).Rows(0).Item("TYPEID").ToString() = "1" Then
                    If ds.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                        rdMetric.Checked = True
                        rdEnglish.Checked = False
                    Else
                        rdMetric.Checked = False
                        rdEnglish.Checked = True
                    End If
                    rdbtnRFP.Checked = True
                    rdbtnRFI.Checked = False
                Else
                    If ds.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                        rdMetric.Checked = True
                        rdEnglish.Checked = False
                    Else
                        rdMetric.Checked = False
                        rdEnglish.Checked = True
                    End If
                    rdbtnRFP.Checked = False
                    rdbtnRFI.Checked = True
                End If

                'rdEnglish.Enabled = True
                'rdMetric.Enabled = True
                'ddlCurrancy.Enabled = True

                ddlCurrancy.SelectedValue = ds.Tables(0).Rows(0).Item("CURRENCY").ToString()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnCreateRFP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreateRFP.Click
        Dim lblSpecID As New Label
        Dim check As New CheckBox
        Dim Count As Integer = 0
        Dim ID As Integer
        Dim Unit As String = String.Empty
        Dim Currancy As String = String.Empty
        Try
            If rdEnglish.Checked Then
                Unit = "0"
            Else
                Unit = "1"
            End If

            Currancy = ddlCurrancy.SelectedValue.ToString()
            If rdbtnRFP.Checked = True Then
                ID = objUpIns.AddTypeDetails(txtRFPNm.Text.Trim().Replace("'", "''").ToString(), txtRfpDes.Text.Trim().Replace("'", "''").ToString(), Session("UserId"), "1", Unit, Currancy)
            ElseIf rdbtnRFI.Checked = True Then
                ID = objUpIns.AddTypeDetails(txtRFPNm.Text.Trim().Replace("'", "''").ToString(), txtRfpDes.Text.Trim().Replace("'", "''").ToString(), Session("UserId"), "2", Unit, Currancy)
            End If
            ScriptManager.RegisterStartupScript(Page, Me.GetType(), "JSScript", "ClosePage();", True)
            txtRFPNm.Text = ""
            txtRfpDes.Text = ""
            GetTypeDetails()

        Catch ex As Exception
            lblError.Text = "Error: btnCreateRFP_Click() " + ex.Message()
        End Try
    End Sub

    Protected Sub GetPrefDetails()
        Dim ds As New DataSet
        Try
            GetCurrancy()
            ddlCurrancy.SelectedValue = "0"
            rdMetric.Checked = False
            rdEnglish.Checked = True
        Catch ex As Exception
            lblError.Text = "Error:GetPrefDetails:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub GetCurrancy()
        Dim ds As New DataSet
        Dim objGetData As New E1GetData.Selectdata


        Try
            ds = objGetData.GetCurrancy("-1")

            With ddlCurrancy
                .DataSource = ds
                .DataTextField = "CURDE1"
                .DataValueField = "CURID"
                .DataBind()
                .Font.Size = 8
            End With

        Catch ex As Exception
            lblError.Text = "Error:GetCurrancy:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub btnEditRFP_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEditRFP.Click
        Dim Unit As String = String.Empty
        Dim Currancy As String = String.Empty
        Try

            If hidRfpID.Value <> "" Then
                If rdEnglish.Checked Then
                    Unit = "0"
                Else
                    Unit = "1"
                End If

                Currancy = ddlCurrancy.SelectedValue.ToString()
                If rdbtnRFP.Checked = True Then
                    objUpIns.UpdateRfp(txtRFPNm.Text.Trim().Replace("'", "''").ToString(), txtRfpDes.Text.Trim().Replace("'", "''").ToString(), Session("UserId"), hidRfpID.Value, "1", Unit, Currancy)
                Else
                    objUpIns.UpdateRfp(txtRFPNm.Text.Trim().ToString(), txtRfpDes.Text.Trim().ToString(), Session("UserId"), hidRfpID.Value, "2", Unit, Currancy)
                End If
                ScriptManager.RegisterStartupScript(Page, Me.GetType(), "JSScript1", "ClosePage1();", True)
                btnCreateRFP.Visible = True
                btnEditRFP.Visible = False
                btnCancel.Visible = False
                txtRFPNm.Text = ""
                txtRfpDes.Text = ""
                ddlCurrancy.SelectedValue = "0"
                rdMetric.Checked = False
                rdEnglish.Checked = True
                GetTypeDetails()
            Else
                ScriptManager.RegisterStartupScript(Page, Me.GetType(), "CreateRFP", "alert('System cannot find any record.');", True)
            End If

        Catch ex As Exception
            lblError.Text = "Error: btnEditRFP_Click() " + ex.Message()
        End Try
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        Try
            btnCreateRFP.Visible = True
            btnEditRFP.Visible = False
            btnCancel.Visible = False
            txtRFPNm.Text = ""
            txtRfpDes.Text = ""
            rdMetric.Checked = False
            rdEnglish.Checked = True
            ddlCurrancy.SelectedValue = "0"
            GetTypeDetails()
            ScriptManager.RegisterStartupScript(Page, Me.GetType(), "JSScript", "RefreshPage();", True)
        Catch ex As Exception
            lblError.Text = "Error: btnCancel_Click() " + ex.Message()
        End Try
    End Sub
End Class
