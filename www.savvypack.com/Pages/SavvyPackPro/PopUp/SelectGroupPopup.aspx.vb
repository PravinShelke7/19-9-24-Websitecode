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
Partial Class Pages_SavvyPackPro_Popup_PopupGroups
    Inherits System.Web.UI.Page
    Dim objGetdata As New SavvyProGetData
    Dim objUpIns As New SavvyProUpInsData
    Public Shared MTypeIDt As String

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            hdnMasterGrpId.Value = Request.QueryString("MGrpID").ToString()
            hidSkuID.Value = Request.QueryString("SkuID").ToString()
            hidGrpInnerText.Value = Request.QueryString("lnkInnerText").ToString()
            hidGrpID.Value = Request.QueryString("GrpID").ToString()
            If Not IsPostBack Then
                GetGroupDetails()
                hidSortIdMGroup.Value = "0"
            End If
        Catch ex As Exception
            lblError.Text = "Page_Load" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub GetGroupDetails()
        Dim ds As New DataSet
        Try
            ds = objGetdata.GetGrpDetails(txtKeyword.Text.Trim.ToString().Replace("'", "''"), hdnMasterGrpId.Value, Session("LicenseNo").ToString())
            Session("GroupD") = ds
            lblRecondCnt.Text = ds.Tables(0).Rows.Count
            If ds.Tables(0).Rows.Count > 0 Then
                lblGroupNoFound.Visible = False
                grdGroupDetails.Visible = True
                grdGroupDetails.DataSource = ds
                grdGroupDetails.DataBind()
            Else
                lblGroupNoFound.Visible = True
                grdGroupDetails.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = "GetRefDetails" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As System.EventArgs) Handles btnSearch.Click
        Try
            GetGroupDetails()
        Catch ex As Exception
            lblError.Text = "btnSearch_Click" + ex.Message.ToString()
        End Try
    End Sub

#Region "User Grid  Group"

    Protected Sub grdGroupDetails_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdGroupDetails.Sorting
        Dim Dts As New DataSet
        Dim dv As DataView
        Dim numberDiv As Integer
        Dim dsSorted As New DataSet()
        Try
            numberDiv = Convert.ToInt16(hidSortIdMGroup.Value.ToString())
            Dts = Session("GroupD")
            dv = Dts.Tables(0).DefaultView

            If ((numberDiv Mod 2) = 0) Then
                dv.Sort = e.SortExpression + " " + "DESC"
            Else
                dv.Sort = e.SortExpression + " " + "ASC"
            End If
            numberDiv += 1
            hidSortIdMGroup.Value = numberDiv.ToString()
            grdGroupDetails.DataSource = dv
            grdGroupDetails.DataBind()

            dsSorted.Tables.Add(dv.ToTable())
            Session("GroupD") = dsSorted

        Catch ex As Exception
            Response.Write("Error:grdMGroup_Sorting:" + ex.Message.ToString())
        End Try
    End Sub

    Protected Sub grdGroupDetails_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdGroupDetails.PageIndexChanging
        Try
            grdGroupDetails.PageIndex = e.NewPageIndex
            BindMGUsingSession()
        Catch ex As Exception
            Response.Write("Error:grdMGroup_PageIndexChanging:" + ex.Message.ToString())
        End Try
    End Sub

    Protected Sub ddlPageCountC_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageCountC.SelectedIndexChanged
        Try
            grdGroupDetails.PageSize = ddlPageCountC.SelectedItem.ToString()
            BindMGUsingSession()
        Catch ex As Exception
            Response.Write("Error:ddlPageCountC_SelectedIndexChanged:" + ex.Message.ToString())
        End Try
    End Sub
    Protected Sub BindMGUsingSession()
        Try
            Dim Dts As New DataSet
            Dts = Session("GroupD")
            grdGroupDetails.DataSource = Dts
            grdGroupDetails.DataBind()
            lblGroupNoFound.Visible = False
        Catch ex As Exception
            lblError.Text = "Error: BindMGUsingSession:" + ex.Message()
        End Try
    End Sub
#End Region

    <System.Web.Services.WebMethod()> _
    Public Shared Function InsertUpdateSkuConn(ByVal GrpID As String, ByVal MasterGrpID As String, ByVal SKUID As String, ByVal ISGrpId As String) As Boolean
        Try
            Dim SavvyProConnection As String = System.Configuration.ConfigurationManager.AppSettings("SavvyPackProConnectionString")
            Dim StrSql As String = String.Empty
            Dim odbutil As New DBUtil()

            If ISGrpId = "0" Then
                'Insert Data
                StrSql = "INSERT INTO SKUGROUP "
                StrSql = StrSql + "(SKUGROUPID,SKUID,GROUPID,MTYPEID) "
                StrSql = StrSql + "SELECT SEQSKUGROUPID.NEXTVAL," + SKUID + "," + GrpID + ",1 FROM DUAL "
                StrSql = StrSql + "WHERE NOT EXISTS "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT 1 FROM "
                StrSql = StrSql + "SKUGROUP "
                StrSql = StrSql + "WHERE "
                StrSql = StrSql + "SKUID=" + SKUID + " AND GROUPID=" + GrpID + " "
                StrSql = StrSql + ") "
                odbutil.UpIns(StrSql, SavvyProConnection)
            Else
                'Update Data
                StrSql = "UPDATE SKUGROUP SET "
                StrSql = StrSql + "GROUPID=" + GrpID + " WHERE SKUID=" + SKUID + " AND GROUPID=" + ISGrpId + " "
                odbutil.UpIns(StrSql, SavvyProConnection)
            End If
            odbutil.UpIns(StrSql, SavvyProConnection)

            Return True
        Catch ex As Exception

            Return False
        End Try
    End Function
End Class
