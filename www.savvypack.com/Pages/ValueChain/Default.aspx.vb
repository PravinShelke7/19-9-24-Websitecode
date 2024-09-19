Imports System.Data
Imports System.Data.OleDb
Imports System.HttpStyleUriParser
Imports System.Web.UI.WebControls
Imports VChainGetData
Imports VChainUpInsData
Partial Class Pages_ValueChain_Default
    Inherits System.Web.UI.Page
    Dim GetData As New Selectdata()
#Region "Get Set Variables"
    Dim _lErrorLble As Label
    Dim _btnLogOff As ImageButton
    Public Property ErrorLable() As Label
        Get
            Return _lErrorLble
        End Get
        Set(ByVal Value As Label)
            _lErrorLble = Value
        End Set
    End Property
    Public Property LogOffbtn() As ImageButton
        Get
            Return _btnLogOff
        End Get
        Set(ByVal value As ImageButton)
            _btnLogOff = value
        End Set
    End Property
#End Region

#Region "MastePage Content Variables"
    Protected Sub GetErrorLable()
        ErrorLable = Page.Master.FindControl("lblError")
    End Sub
    Protected Sub GetLogoffBtn()
        LogOffbtn = Page.Master.FindControl("imgLogoff")
        LogOffbtn.Visible = True
        LogOffbtn.PostBackUrl = "~/Universal_loginN/Pages/ULogOff.aspx?Type=VChain"
    End Sub
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            GetLogoffBtn()
            SetSessions()
            If Not IsPostBack Then
                Session("VChainCases") = Nothing
                BindValueChain()
                GetPages("")
                GetResCases()

            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub SetSessions()
        Dim objGetData As New VChainGetData.Selectdata()
        Dim ds As New DataSet()
        Try
            ds = objGetData.GetUserDetails(Session("ID"))

            Session("UserId") = ds.Tables(0).Rows(0).Item("USERID").ToString()
            Session("VChainUserRole") = ds.Tables(0).Rows(0).Item("USERROLE").ToString()
            Session("VChainServiceRole") = ds.Tables(0).Rows(0).Item("SERVIECROLE").ToString()
            Session("VChainUserName") = ds.Tables(0).Rows(0).Item("USERNAME").ToString()
            Session("VChainToolUserName") = ds.Tables(0).Rows(0).Item("TOOLUSERNAME").ToString()

        Catch ex As Exception
            ErrorLable.Text = "Error:SetSessions:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub btnAddEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddEdit.Click
        Try
            Response.Redirect("Tools/AddEditVChainItem.aspx", False)
        Catch ex As Exception
            ErrorLable.Text = "btnAddEdit_Click"
        End Try
    End Sub
    Protected Sub BindValueChain()
        Dim ds As New DataTable
        Dim objGetData As New VChainGetData.Selectdata
        Try

            ds = objGetData.GetValueChain("", Session("UserId").ToString())
            With ddlVchain
                .DataValueField = "VALUECHAINID"
                .DataTextField = "VALUECHAINNAME1"
                .DataSource = ds
                .DataBind()
            End With

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub GetResCases()
        Dim ds As New DataSet
        Dim objGetData As New VChainGetData.Selectdata
        Try
            ds = objGetData.GetPCaseDetails(Session("UserName").ToString(), ddlResPage.SelectedValue)
            With ddlResCaseId
                .DataTextField = "CASEDES"
                .DataValueField = "CASEID"
                .DataSource = ds
                .DataBind()
            End With
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub GetPages(ByVal PageName As String)
        Dim ds1 As New DataSet
        Dim ds2 As New DataSet
        Dim objGetData As New VChainGetData.Selectdata
        Try

            ds1 = objGetData.GetPages(PageName)
            ds2 = objGetData.GetPages("Econ2")


            With ddlResPage
                .DataTextField = "PAGE"
                .DataValueField = "VALUE"
                .DataSource = ds2
                .DataBind()
            End With

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlResPage_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlResPage.SelectedIndexChanged
        Dim ds As New DataSet
        Dim objGetData As New VChainGetData.Selectdata
        Try
            If ddlResPage.SelectedValue <> "0" Then
                GetResCases()
            End If
        Catch ex As Exception
            ErrorLable.Text = "Error:ddlResPage_SelectedIndexChanged:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Dim objGetData As New VChainGetData.Selectdata
        Dim objUpdate As New VChainUpInsData.UpdateInsert
        Dim message As String
        Dim flag As Boolean = False
        Dim dt As New DataTable
        Dim dt1 As New DataTable
        Try
            dt = objGetData.GetValueChain(txtVChain.Text.Trim(), Session("UserId").ToString())
            If dt.Rows.Count > 0 Then
                message = "Value Chain " + txtVChain.Text.Trim() + " already exist."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "New_WinCopy", "alert('" + message + "');", True)
            Else
                objUpdate.AddValueChain(txtVChain.Text.Trim(), Session("USERID").ToString(), ddlResPage.SelectedItem.Text, ddlResCaseId.SelectedValue)
                dt1 = objGetData.GetValueChain(txtVChain.Text.Trim(), Session("UserId").ToString())
                If dt1.Rows.Count > 0 Then
                    Session("ValueChainId") = dt1.Rows(0)("VALUECHAINID").ToString()
                    'INSERT INTO THE RESULTSPL TABLE 

                    objUpdate.InsertResultSpl(Session("ValueChainId").ToString(), ddlResPage.SelectedItem.Text.ToUpper(), ddlResCaseId.SelectedValue.ToString())
            End If
                txtVChain.Text = ""
                flag = True
                BindValueChain()
                GetPages("")
                GetResCases()


            End If

        Catch ex As Exception
            flag = False
        End Try
        If flag Then
            message = "Value Chain " + txtVChain.Text.Trim() + " created successfully."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "New_WinCopy", "alert('" + message + "');", True)
        End If
    End Sub

    Protected Sub btnStart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnStart.Click
        Try
            If ddlVChain.SelectedValue.ToString() <> "" Then
                Session("ValueChainId") = ddlVChain.SelectedValue.ToString()
                Response.Write("<script language=JavaScript>window.open('Assumptions/ValueChainManager.aspx','new_Win');</script>")
            End If
        Catch ex As Exception
            ErrorLable.Text = "Error:btnStart_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim objGetData As New VChainUpInsData.UpdateInsert
        Try
            objGetData.DeleteVChain(Session("VChainUserName"), ddlVChain.SelectedValue.ToString())
            BindValueChain()
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('ValueChainId #" + ddlVChain.SelectedValue.ToString() + " deleted successfully');", True)
        Catch ex As Exception
            ErrorLable.Text = "Error:btnDelete_Click:" + ex.Message.ToString()
        End Try
    End Sub
End Class
