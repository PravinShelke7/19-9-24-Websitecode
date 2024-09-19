Imports System.Data
Imports System.Data.OleDb
Imports System.HttpStyleUriParser
Imports System.Web.UI.WebControls
Imports M1SubGetData
Imports M1SubUpInsData
Partial Class Pages_Market1Sub_PopUp_CreateGroup
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("Back") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If
            If Not IsPostBack Then
                hdnUpdate.Value = "0"
                hdnGrpID.Value = "0"
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnGCreate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGCreate.Click
        Dim ID As String = 1
        Dim Name As String = ""
        Dim flag As Boolean = True
        Name = Trim(txtGDES1.Text)
        Dim objUpdateData As New M1SubUpInsData.UpdateInsert
        Dim objGetData As New M1SubGetData.Selectdata()
        Dim dt As New DataSet()
        Dim dsGrps As New DataSet()
        Dim GROUPID As String = ""
        Try
            If Name.Length <> 0 Then
                If Request.QueryString("Type").ToString() = "PROP" Then
                    dsGrps = objGetData.GetGroupIDCheck(txtGDES1.Text, txtGDES2.Text, Session("UserId").ToString(), "PROP", Session("M1ServiceID"))
                    If dsGrps.Tables(0).Rows.Count > 0 Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Group already exist');", True)
                    Else
                        GROUPID = objUpdateData.AddGroupName(txtGDES1.Text, txtGDES2.Text, Session("UserId"), "PROP", Session("M1ServiceID"))
                        hdnGrpID.Value = GROUPID
                        txtGDES1.Text = ""
                        txtGDES2.Text = ""
                    End If
                End If
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:btnGCreate_Click:" + ex.Message.ToString()
        End Try
        If (flag) Then
            hdnUpdate.Value = "1"
            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Group #" + GROUPID + " created successfully');", True)
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "Close(" + hdnGrpID.Value + ");", True)
        End If
    End Sub

    Protected Sub btnGCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGCancel.Click
        Try
            txtGDES1.Text = ""
            txtGDES2.Text = ""
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "Close(0);", True)
        Catch ex As Exception
            _lErrorLble.Text = "Error:btnGCancel_Click:" + ex.Message.ToString()
        End Try
    End Sub
End Class
