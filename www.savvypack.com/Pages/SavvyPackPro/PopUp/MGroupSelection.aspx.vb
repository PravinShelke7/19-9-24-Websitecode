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
Partial Class Pages_SavvyPackPro_PopUp_MGroupSelection
    Inherits System.Web.UI.Page
    Dim objGetdata As New SavvyProGetData
    Dim objUpIns As New SavvyProUpInsData
    Dim Type As String = String.Empty

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            hdnMGrpID.Value = Request.QueryString("MID").ToString()
            hdnMgrpDes.Value = Request.QueryString("MDes").ToString()
            hdnInnerLink.Value = Request.QueryString("MInner").ToString()
            Type = "B" ' Request.QueryString("Type").ToString()
            If Not IsPostBack Then
                GetMGroupDetails()
            End If
        Catch ex As Exception
            lblError.Text = "Error: Page_Load() " + ex.Message()
        End Try
    End Sub

    Protected Sub GetMGroupDetails()
        Dim ds As New DataSet()
        Try
            If Type = "B" Then
                ds = objGetdata.GetMGroupDetails_Buyer(Session("UserId"), txtKey.Text.Trim.ToString(), Session("LicenseNo"))
            Else
                ds = objGetdata.GetMGroupDetails_Supplier(Session("ShidRfpID"), txtKey.Text.Trim.ToString())
            End If

            Session("MasterGroupD") = ds
            lblRecondCnt.Text = ds.Tables(0).Rows.Count
            If ds.Tables(0).Rows.Count > 0 Then
                grdMGroup.DataSource = ds
                grdMGroup.DataBind()
                grdMGroup.Visible = True
                lblMGroupNoFound.Visible = False
                BindLinkMasterG()
            Else
                lblMGroupNoFound.Visible = True
                grdMGroup.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = "Error: GetMGroupDetails() " + ex.Message()
        End Try
    End Sub

    Protected Sub BindLinkMasterG()
        Dim lblUserID As New Label
        Try
            If Type = "B" Then
                For Each Gr As GridViewRow In grdMGroup.Rows
                    lblUserID = grdMGroup.Rows(Gr.RowIndex).FindControl("lblUserID")

                    If Session("UserId") = lblUserID.Text Then
                        Gr.Enabled = True
                        Gr.Cells(1).Enabled = True
                    Else
                        Gr.Cells(1).Enabled = False
                        Gr.BackColor = Drawing.Color.LightGray
                    End If
                Next
            Else
                grdMGroup.Columns(3).Visible = False
            End If

        Catch ex As Exception
            lblError.Text = "Error: BindLinkMasterG:" + ex.Message()
        End Try
    End Sub
End Class
