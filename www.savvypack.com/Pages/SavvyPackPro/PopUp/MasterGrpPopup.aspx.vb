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
Partial Class Pages_SavvyPackPro_Popup_MasterGrpPopup
    Inherits System.Web.UI.Page
    Dim objGetdata As New SavvyProGetData
    Dim objUpIns As New SavvyProUpInsData
    Dim LicId As String
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                hidSortIdBSpec.Value = "0"
                LicId = Session("LicenseNo")
                ' BindMTypeDetails()
                GetTypeDetails()
                'ddlMType.Enabled = True
            End If
        Catch ex As Exception
            lblError.Text = "Error: Page_Load() " + ex.Message()
        End Try
    End Sub

#Region "User Define Delegates"

    Protected Sub GetTypeDetails()
        Dim ds As New DataSet()
        Try
            ds = objGetdata.GetMGroupDetails_Buyer(Session("UserId"), txtKey.Text.Trim.ToString(), Session("LicenseNo"))
            Session("MasterGroup") = ds
            lblRecondCnt.Text = ds.Tables(0).Rows.Count
            If ds.Tables(0).Rows.Count > 0 Then
                grdMGroup.DataSource = ds
                grdMGroup.DataBind()
                grdMGroup.Visible = True
                'lblSpecNoFound.Visible = False
                BindLink()
            Else
                'lblSpecNoFound.Visible = True
                grdMGroup.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = "Error: GetTypeDetails() " + ex.Message()
        End Try
    End Sub
    Protected Sub BindLink()
        Dim lblUserID As New Label
        Try
            For Each Gr As GridViewRow In grdMGroup.Rows
                lblUserID = grdMGroup.Rows(Gr.RowIndex).FindControl("lblUserID")

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
    Protected Sub BindRfpUsingSession()
        Try
            Dim Dts As New DataSet
            Dts = Session("MasterGroup")
            grdMGroup.DataSource = Dts
            grdMGroup.DataBind()
            BindLink()
            'lblSpecNoFound.Visible = False
        Catch ex As Exception
            lblError.Text = "Error: BindRfpUsingSession:" + ex.Message()
        End Try
    End Sub

#End Region
  
#Region "User Grid Event"

    Protected Sub grdMGroup_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdMGroup.Sorting
        Dim Dts As New DataSet
        Dim dv As DataView
        Dim numberDiv As Integer
        Dim dsSorted As New DataSet()
        Try
            numberDiv = Convert.ToInt16(hidSortIdBSpec.Value.ToString())
            Dts = Session("MasterGroup")
            dv = Dts.Tables(0).DefaultView

            If ((numberDiv Mod 2) = 0) Then
                dv.Sort = e.SortExpression + " " + "DESC"
            Else
                dv.Sort = e.SortExpression + " " + "ASC"
            End If
            numberDiv += 1
            hidSortIdBSpec.Value = numberDiv.ToString()
            grdMGroup.DataSource = dv
            grdMGroup.DataBind()
            BindLink()
            dsSorted.Tables.Add(dv.ToTable())
            Session("MasterGroup") = dsSorted

        Catch ex As Exception
            Response.Write("Error:grdRfp_Sorting:" + ex.Message.ToString())
        End Try
    End Sub

    Protected Sub grdMGroup_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdMGroup.PageIndexChanging
        Try
            grdMGroup.PageIndex = e.NewPageIndex
            BindRfpUsingSession()
        Catch ex As Exception
            Response.Write("Error:grdRfp_PageIndexChanging:" + ex.Message.ToString())
        End Try
    End Sub

    Protected Sub ddlPageCountC_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageCountC.SelectedIndexChanged
        Try
            grdMGroup.PageSize = ddlPageCountC.SelectedItem.ToString()
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

    Protected Sub grdMGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdMGroup.SelectedIndexChanged
        Dim ds As New DataSet
        Try
            Dim MID As New Integer
            MID = Convert.ToInt32(grdMGroup.SelectedDataKey.Value)
            hidMID.Value = MID
            btnCreateRFP.Visible = False
            btnEditRFP.Visible = True
            btnCancel.Visible = True

            ds = objGetdata.GetMasterDetByID(MID)
            If ds.Tables(0).Rows.Count > 0 Then
                txtMGNm.Text = ds.Tables(0).Rows(0).Item("DESCRIPTION").ToString()
                txtMGDes.Text = ds.Tables(0).Rows(0).Item("DESCRIPTION2").ToString()
                'ddlMType.SelectedValue = ds.Tables(0).Rows(0).Item("MTYPEID").ToString()
                'ddlMType.Enabled = False
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnCreateRFP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreateRFP.Click
        Dim lblSpecID As New Label
        Dim check As New CheckBox
        Dim Count As Integer = 0
        Dim ID As Integer
        Try
            If txtMGNm.Text = "" Or txtMGDes.Text = "" Then
                ScriptManager.RegisterStartupScript(Page, Me.GetType(), "Create", "alert(' Please fill all fields properly.');", True)
            Else
                objUpIns.AddMasterGroupDetails(txtMGNm.Text.Trim().Replace("'", "''").ToString(), txtMGDes.Text.Trim().Replace("'", "''").ToString(), "1", _
                                                     Session("UserId"), "1", txtMGNm.Text.Trim().Replace("'", "''").ToString(), Session("hidRfpID"))
                ScriptManager.RegisterStartupScript(Page, Me.GetType(), "Create", "alert('Master Group Created successfully.');", True)
                txtMGNm.Text = ""
                txtMGDes.Text = ""
            End If
          GetTypeDetails()

        Catch ex As Exception
            lblError.Text = "Error: btnCreateRFP_Click() " + ex.Message()
        End Try
    End Sub

    Protected Sub btnEditRFP_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEditRFP.Click
        Try
            If txtMGNm.Text = "" Or txtMGDes.Text = "" Then
                ScriptManager.RegisterStartupScript(Page, Me.GetType(), "Create", "alert(' Please fill all fields properly.');", True)

            Else
                objUpIns.UpdateMasterGroup(txtMGNm.Text.Trim().ToString(), txtMGDes.Text.Trim().ToString(), Session("UserId"), hidMID.Value)
                ScriptManager.RegisterStartupScript(Page, Me.GetType(), "Create", "alert('Master Group Updated successfully.');", True)
                btnCreateRFP.Visible = True
                btnEditRFP.Visible = False
                btnCancel.Visible = False
                txtMGNm.Text = ""
                txtMGDes.Text = ""
            End If
            GetTypeDetails()
        Catch ex As Exception
            lblError.Text = "Error: btnEditRFP_Click() " + ex.Message()
        End Try
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        Try
            btnCreateRFP.Visible = True
            btnEditRFP.Visible = False
            btnCancel.Visible = False
            txtMGNm.Text = ""
            txtMGDes.Text = ""
            GetTypeDetails()
        Catch ex As Exception
            lblError.Text = "Error: btnCancel_Click() " + ex.Message()
        End Try
    End Sub
End Class
