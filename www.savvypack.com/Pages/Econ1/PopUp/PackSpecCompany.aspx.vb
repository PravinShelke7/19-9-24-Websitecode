Imports System.Data
Imports System.Data.OleDb
Imports System
Imports E1GetData
Imports E1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Econ1_PopUp_PackSpecCompany
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            hidGroupDes.Value = Request.QueryString("DES").ToString()
            hidGroupId.Value = Request.QueryString("ID").ToString()
            ' hidCaseidD.Value = Request.QueryString("IdD").ToString()
            If Not IsPostBack Then
                GetCaseDetails()

            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetCaseDetails()
        Dim ds As New DataSet
        Dim objGetData As New E1GetData.Selectdata()
        Try
            ds = objGetData.GetPackSpecCompany(Session("USERID").ToString())
            grdCaseSearch.DataSource = ds
            grdCaseSearch.DataBind()


        Catch ex As Exception
            _lErrorLble.Text = "Error:GetCaseDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub lnkCmpny_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCmpny.Click
        Try
            NewGrp.Style.Add("Display", "Block")
            GetCaseDetails()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub btnadd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnadd.Click
        Dim obj As New ToolCCS
        Dim ds As New DataSet()
        Dim objGetData As New E1GetData.Selectdata()
        Dim flag As Boolean = False
        Try
            If txtGrpName.Text.ToString() <> "" Then
                ds = objGetData.GetPackSpecCompany(Session("USERID").ToString())
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    If ds.Tables(0).Rows(i).Item("COMPANYNAME").ToString().ToUpper() = txtGrpName.Text.ToString().ToUpper() Then
                        flag = True
                        Exit For
                    End If
                Next
                If flag = True Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "", "alert('Company already exists.');", True)
                Else
                    obj.newCompany(Session("USERID").ToString(), txtGrpName.Text.ToString())
                    NewGrp.Style.Add("Display", "none")
                End If
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "", "alert('Please enter company name.');", True)
            End If


            GetCaseDetails()
        Catch ex As Exception

        End Try
    End Sub
End Class
