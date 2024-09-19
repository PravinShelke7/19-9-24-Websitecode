Imports System.Data
Imports System.Data.OleDb
Imports System
Imports MoldS1GetData
Imports MoldS1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_MoldSustain1_Assumptions_LCIComparison
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("Back") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If
            If Session("Service") = "COMPS1" Then
                S1Table.Attributes.Add("class", "S1CompModule")
            End If
            If Not IsPostBack Then
                GetLCI()
                GetEffdate()
            End If

        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString() + ""
        End Try

    End Sub
    Protected Sub GetLCI()
        Dim ds As New DataSet
        Dim objGetData As New MoldS1GetData.Selectdata

        Try
            ds = objGetData.GetLCI()
            With lstLCI
                .DataSource = ds
                .DataTextField = "INVENTORY"
                .DataValueField = "INVENTORYID"
                .DataBind()
                .Font.Size = 8
            End With

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetLCI:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub GetEffdate()
        Dim ds As New DataSet
        Dim objGetData As New MoldS1GetData.Selectdata
        Dim Inventorytype As String = String.Empty
        Try
            ds = objGetData.GetLCIEffDates()
            With ddlEffdate
                .DataSource = ds
                .DataTextField = "EDATE"
                .DataValueField = "EDATE"
                .DataBind()
                .Font.Size = 8
            End With
            'ddlEffdate.Items.Insert(ddlEffdate.Items.Count, New ListItem("LastDate", "MatBalance"))
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetEffdate:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnType_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnType.Click
        Try
            Dim i As Integer = 0
            Dim strSelecledItem As String = String.Empty
            strSelecledItem = Request.Form("lstLCI")
            Session("EffDate") = ddlEffdate.SelectedItem.Value
            Session("LSIIDS") = strSelecledItem
            Session("CompType") = ddlType.SelectedItem.Text
            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinB", "OpenNewWindow('CLciComp.aspx');", True)
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinB", "OpenNewWindow('CLciComp.aspx?Type=" + Session("CompType") + "');", True)
        Catch ex As Exception
            _lErrorLble.Text = "Error:btnType_Click:" + ex.Message.ToString()
        End Try
    End Sub
End Class
