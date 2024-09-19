Imports System.Data
Imports System.Data.OleDb
Imports System
Imports E2GetData
Imports E2UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Masters_Econ2
    Inherits System.Web.UI.MasterPage

    Dim _iCaseId As Integer
    Dim _iUserId As Integer
    Dim _strUserRole As String
    Public CaseDes As String = String.Empty
    Public Property CaseId() As Integer
        Get
            Return _iCaseId
        End Get
        Set(ByVal Value As Integer)
            _iCaseId = Value
        End Set
    End Property

    Public Property UserId() As Integer
        Get
            Return _iUserId
        End Get
        Set(ByVal Value As Integer)
            _iUserId = Value
        End Set
    End Property

    Public Property UserRole() As String
        Get
            Return _strUserRole
        End Get
        Set(ByVal Value As String)
            _strUserRole = Value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Try
                lblTag.Text = "Copyright 1997 - " + Now.Year().ToString() + " Savvypack Corporation."
            Catch ex As Exception
            End Try
            If Session("Back") <> Nothing Then
                If Not IsPostBack Then
                    GetCaseDetails()
                End If
                UserRoleWisePermission()
            Else
                ContentPage.Visible = False
                lblError.Text = "Session is Expired"
                Dim obj As New CryptoHelper
                Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If

        Catch ex As Exception
            lblError.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetCaseDetails()
        Dim objGetData As New E2GetData.Selectdata()
        Dim ds As New DataSet
        Try
            CaseId = Session("E2CaseId")
            ds = objGetData.GetCaseDetails(CaseId.ToString())
            lblCaseID.Text = CaseId.ToString()
            lblCaseType.Text = ds.Tables(0).Rows(0).Item("CASETYPE").ToString()
            lblCaseDe2.Text = ds.Tables(0).Rows(0).Item("CASEDES").ToString()

            If ds.Tables(0).Rows(0).Item("CASEDE3").ToString().Length > 0 Then
                CaseDes = ds.Tables(0).Rows(0).Item("CASEDE3").ToString()
                caseDe3.Attributes.Add("onmouseover", "Tip('" + CaseDes + "')")
                caseDe3.Attributes.Add("onmouseout", "UnTip()")
            End If

        Catch ex As Exception
            lblError.Text = "Error:GetCaseDetails:" + ex.Message.ToString()
        End Try
    End Sub

    'Protected Sub UserRoleWisePermission()
    '    Try
    '        If Session("E2UserRole") <> "AADMIN" Then
    '            If Session("E2CaseId") < 1000 Then
    '                imgUpdate.Enabled = False
    '                imgUpdate.ImageUrl = "~/Images/UpdateDec.gif"
    '            End If
    '        End If
    '        If Session("E2ServiceRole") = "ReadOnly" Then
    '            imgUpdate.Enabled = False
    '            imgUpdate.ImageUrl = "~/Images/UpdateDec.gif"
    '        End If

    '    Catch ex As Exception

    '    End Try
    'End Sub
    Protected Sub UserRoleWisePermission()
        Try
            If Session("SavvyModType") <> "1" Then
                If Session("E2LicAdmin") <> "Y" Then
                    If Session("CaseType") = "Approved" Then
                        imgUpdate.Enabled = False
                        imgUpdate.ImageUrl = "~/Images/UpdateDec.gif"
                    Else
                        If Session("E2ServiceRole") = "ReadOnly" Then
                            'imgUpdate.Visible = False
                            imgUpdate.Enabled = False
                            imgUpdate.ImageUrl = "~/Images/UpdateDec.gif"
                        End If
                    End If
                Else
                    If Session("CaseType") = "Approved" Then

                    Else
                        If Session("E2ServiceRole") = "ReadOnly" Then
                            If Session("Status") = "True" Then
                                imgUpdate.Enabled = True
                                imgUpdate.ImageUrl = "~/Images/Update.gif"
                            Else
                                imgUpdate.Enabled = False
                                imgUpdate.ImageUrl = "~/Images/UpdateDec.gif"
                            End If
                        End If
                    End If
                End If
            Else
                If Session("E2UserRole") <> "AADMIN" Then
                    If Session("E2CaseId") < 1000 Then
                        'imgUpdate.Visible = False
                        imgUpdate.Enabled = False
                        imgUpdate.ImageUrl = "~/Images/UpdateDec.gif"
                    End If
                    If Session("E2ServiceRole") = "ReadOnly" Then
                        'imgUpdate.Visible = False
                        imgUpdate.Enabled = False
                        imgUpdate.ImageUrl = "~/Images/UpdateDec.gif"
                    End If
                End If
            End If




        Catch ex As Exception

        End Try
    End Sub
End Class

