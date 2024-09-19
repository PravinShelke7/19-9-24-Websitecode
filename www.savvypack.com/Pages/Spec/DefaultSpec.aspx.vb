Imports System.Data
Imports System.Data.OleDb
Imports System
Imports SpecGetData
Imports SpecUpdateData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter

Partial Class Pages_Spec_Default
    Inherits System.Web.UI.Page
#Region "Get Set Variables"
    Dim _lErrorLble As Label
    Dim _strUserName As String
    Dim _strPassword As String
    Dim _iSpecId As Integer
    'Dim _btnUpdate As ImageButton
    Dim _btnClose As ImageButton
    Dim _strFormMode As String


    Public Property ErrorLable() As Label
        Get
            Return _lErrorLble
        End Get
        Set(ByVal Value As Label)
            _lErrorLble = Value
        End Set
    End Property

    Public Property UserName() As String
        Get
            Return _strUserName
        End Get
        Set(ByVal Value As String)
            _strUserName = Value
        End Set
    End Property

    Public Property Password() As String
        Get
            Return _strPassword
        End Get
        Set(ByVal Value As String)
            _strPassword = Value
        End Set
    End Property

    Public Property SpecId() As Integer
        Get
            Return _iSpecId
        End Get
        Set(ByVal Value As Integer)
            _iSpecId = Value
        End Set
    End Property

    Public Property LogOffbtn() As ImageButton
        Get
            Return _btnClose
        End Get
        Set(ByVal value As ImageButton)
            _btnClose = value
        End Set
    End Property

    Public Property FormMode() As String
        Get
            Return _strFormMode
        End Get
        Set(ByVal Value As String)
            _strFormMode = Value
        End Set
    End Property

   
#End Region

#Region "MastePage Content Variables"
    Protected Sub GetErrorLable()
        ErrorLable = Page.Master.FindControl("lblError")
    End Sub

    Protected Sub GetLoggOffbtn()
        LogOffbtn = Page.Master.FindControl("imgLogoff")
        LogOffbtn.Visible = True
        LogOffbtn.PostBackUrl = "~/Universal_loginN/Pages/ULogOff.aspx?Type=SPEC"

    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            GetErrorLable()
            GetLoggOffbtn()

            If Not IsPostBack Then
                GetUserDeatils()
                GetSpecDetails()
            End If

        Catch ex As Exception
            ErrorLable.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetSpecDetails()
        Dim ds As New DataSet()
        Dim objeGetData As New SpecGetData.Selectdata()
        Try
            ds = objeGetData.GetSpecDetails(0)

            If ds.Tables(0).Rows.Count <> 0 Then
                With ddlSpec
                    .DataSource = ds
                    .DataTextField = "DES"
                    .DataValueField = "PACKAGINGSPECIFICATIONID"
                    .DataBind()
                    .Visible = True
                End With
                lblNoSpec.Visible = False
                btnSelect.Enabled = True
                btnUpdate.Enabled = True

            Else
                lblNoSpec.Visible = True
                ddlSpec.Visible = False
                btnSelect.Enabled = False
                btnUpdate.Enabled = False

            End If


        Catch ex As Exception
            ErrorLable.Text = "Error:GetSpecDeatails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetSpecDetailsById(ByVal SpecId As Integer)
        Dim ds As New DataSet()
        Dim objeGetData As New SpecGetData.Selectdata()
        Try
            btnCreatePkg.Text = FormMode
            GetE1S1CaseId()
            GetSpecCompany()
            If SpecId <> 0 Then
                ds = objeGetData.GetSpecDetails(SpecId)
                txtPackName.Text = ds.Tables(0).Rows(0).Item("NAME").ToString()
                ddlE1S1CaseID.SelectedValue = CInt(ds.Tables(0).Rows(0).Item("E1S1CASEID").ToString())
                ddlPackComp.SelectedValue = ds.Tables(0).Rows(0).Item("COMPNAME").ToString()
            Else
                txtPackName.Text = String.Empty
            End If


        Catch ex As Exception
            ErrorLable.Text = "Error:GetSpecDeatails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetUserDeatils()
        Dim ds As New DataTable
        Dim objeGetData As New SpecGetData.Selectdata()
        Try
            ds = objeGetData.GetUsernamePassword(Session("ID").ToString())
            ViewState("UserName") = ds.Rows(0).Item("Uname").ToString().ToUpper()
            ViewState("Password") = ds.Rows(0).Item("Upwd").ToString()
            ViewState("Company") = ds.Rows(0).Item("Company").ToString()
            Session("Company") = ViewState("Company")
            Session("UserName") = ViewState("UserName")
            Session("Password") = ViewState("Password")
            If ViewState("Password").ToString() = "9Alde95Ad2" Then
                divAdmin.Visible = True
            Else
                divAdmin.Visible = False
            End If



        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GetE1S1CaseId()
        Dim ds As New DataTable
        Dim objeGetData As New SpecGetData.Selectdata()
        Try
            ds = objeGetData.GetE1S1Cases(ViewState("Company").ToString(), ViewState("UserName").ToString())
            With ddlE1S1CaseID
                .DataSource = ds
                .DataTextField = "CaseDe"
                .DataValueField = "caseID"
                .DataBind()
            End With



        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GetSpecCompany()
        Dim ds As New DataSet()
        Dim objeGetData As New SpecGetData.Selectdata()
        Try

            ds = objeGetData.GetSpecCompanies(ViewState("UserName"))
            With ddlPackComp
                .DataSource = ds
                .DataValueField = "COMPANY"
                .DataTextField = "COMPANY"
                .DataBind()
                .SelectedValue = ViewState("Company").ToString()
            End With
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnSelect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelect.Click
        Dim ds As New DataSet()
        Dim objeGetData As New SpecGetData.Selectdata()
        Try
            ds = objeGetData.GetSpecDetails(CInt(ddlSpec.SelectedValue.ToString()))
            Session("Description") = ds.Tables(0).Rows(0).Item("NAME").ToString()
            Session("SpecId") = ddlSpec.SelectedValue
            Response.Write("<script language=JavaScript>window.open('Assumptions/CaseManeger.aspx','new_Win');</script>")
        Catch ex As Exception
            ErrorLable.Text = "Error:btnSelect_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnCreate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreate.Click
        Try
            divPack.Visible = True
            ViewState("FormMode") = "Add"
            FormMode = "Add"
            GetSpecDetailsById(0)
            EnabledDisabledButton(False)

        Catch ex As Exception
            ErrorLable.Text = "Error:btnCreate_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Dim SpecId As New Integer
        Try
            divPack.Visible = True
            SpecId = CInt(ddlSpec.SelectedValue.ToString())
            ViewState("FormMode") = "Update"
            FormMode = "Update"
            GetSpecDetailsById(SpecId)
            EnabledDisabledButton(False)


        Catch ex As Exception
            ErrorLable.Text = "Error:btnUpdate_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnCreatePkg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreatePkg.Click
        Try
            Dim ObjUpIns As New SpecUpdateData.UpdateInsert
            If (ViewState("FormMode").ToString() = "Add") Then
                ObjUpIns.CreateSpecifications(txtPackName.Text.Trim.ToString(), ddlPackComp.SelectedValue.ToString(), CInt(ddlE1S1CaseID.SelectedValue.ToString()))
                GetSpecDetails()
            ElseIf (ViewState("FormMode").ToString() = "Update") Then
                SpecId = CInt(ddlSpec.SelectedValue.ToString())
                ObjUpIns.UpdateSpecifications(SpecId, txtPackName.Text.Trim.ToString(), ddlPackComp.SelectedValue.ToString(), CInt(ddlE1S1CaseID.SelectedValue.ToString()))
            End If
            divPack.Visible = False
            EnabledDisabledButton(True)
        Catch ex As Exception
            ErrorLable.Text = "Error:btnCreatePkg_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnCancle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancle.Click
        Try
            divPack.Visible = False
            EnabledDisabledButton(True)
        Catch ex As Exception
            ErrorLable.Text = "Error:btnCancle_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub EnabledDisabledButton(ByVal val As Boolean)
        Try
            btnSelect.Enabled = val
            btnUpdate.Enabled = val
            btnCreate.Enabled = val

        Catch ex As Exception
            ErrorLable.Text = "Error:EnabledDisabledButton:" + ex.Message.ToString()
        End Try
    End Sub

End Class
