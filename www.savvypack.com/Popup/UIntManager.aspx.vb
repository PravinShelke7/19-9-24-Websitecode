Imports System.Data
Imports System.Data.OleDb
Imports System
Imports LoginGetData


Partial Class Popup_UIntManager
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim obj As New CryptoHelper
        Try
            If Session("Back") = Nothing And Session("SBack") = Nothing Then
                Response.Redirect("/~ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If

            If Not IsPostBack Then
                hidId.Value = Request.QueryString("Id").ToString()
                If Session("LicenseNo") <> "0" Then
                    GetModuleDetails()
                    GetModType()
                Else
                    'Response.Redirect("~/ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE114").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
					Page.ClientScript.RegisterStartupScript(Me.GetType(), "SessioError", "ErrorWindow('/ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE114").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "')", True)
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub GetModType()
        Dim objGetData As New E1GetData.Selectdata()
        Dim ds As New DataSet()
        Try
            ds = objGetData.GetModuleType(Session("USERNAME").ToString())
            If ds.Tables(0).Rows.Count > 0 Then
                Session("SavvyModType") = ds.Tables(0).Rows(0).Item("MODELTID").ToString()
            End If
        Catch ex As Exception
            'lblError.Text = "Error:GetMessageCount:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Public Sub GetModuleDetails()
        Dim objGetData As New LoginGetData.Selectdata()
        Dim ds As New DataSet()
        Try
            ds = objGetData.GetModuleDetails(Session("UserId").ToString(), "")
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    If ds.Tables(0).Rows(i).Item("SERVICEID").ToString() = "1" Then
                        lnkEcon1.Enabled = True
                        lnkEcon1.Attributes.Add("CommandArgument", ds.Tables(0).Rows(i).Item("TID").ToString())
                        lnkEcon1.Attributes.Add("OnClick", "return ClosePop('lnkEcon1');")
                        lnkEcon1.CssClass = "Service"

                    ElseIf ds.Tables(0).Rows(i).Item("SERVICEID").ToString() = "2" Then
                        lnkEcon2.Enabled = True
                        lnkEcon2.Attributes.Add("CommandArgument", ds.Tables(0).Rows(i).Item("TID").ToString())
                        lnkEcon2.Attributes.Add("OnClick", "return ClosePop('lnkEcon2');")
                        lnkEcon2.CssClass = "Service"

                    ElseIf ds.Tables(0).Rows(i).Item("SERVICEID").ToString() = "12" Then
                        lnkEcon3.Enabled = True
                        lnkEcon3.Attributes.Add("CommandArgument", ds.Tables(0).Rows(i).Item("TID").ToString())
                        lnkEcon3.Attributes.Add("OnClick", "return ClosePop('lnkEcon3');")
                        lnkEcon3.CssClass = "Service"

                    ElseIf ds.Tables(0).Rows(i).Item("SERVICEID").ToString() = "86" Then
                        lnkEcon4.Enabled = True
                        lnkEcon4.Attributes.Add("CommandArgument", ds.Tables(0).Rows(i).Item("TID").ToString())
                        lnkEcon4.Attributes.Add("OnClick", "return ClosePop('lnkEcon4');")
                        lnkEcon4.CssClass = "Service"

                    ElseIf ds.Tables(0).Rows(i).Item("SERVICEID").ToString() = "18" Then
                        lnkSustn1.Enabled = True
                        lnkSustn1.Attributes.Add("CommandArgument", ds.Tables(0).Rows(i).Item("TID").ToString())
                        lnkSustn1.Attributes.Add("OnClick", "return ClosePop('lnkSustn1');")
                        lnkSustn1.CssClass = "Service"

                    ElseIf ds.Tables(0).Rows(i).Item("SERVICEID").ToString() = "21" Then
                        lnkSustn2.Enabled = True
                        lnkSustn2.Attributes.Add("CommandArgument", ds.Tables(0).Rows(i).Item("TID").ToString())
                        lnkSustn2.Attributes.Add("OnClick", "return ClosePop('lnkSustn2');")
                        lnkSustn2.CssClass = "Service"

                    ElseIf ds.Tables(0).Rows(i).Item("SERVICEID").ToString() = "84" Then
                        lnkSustn3.Enabled = True
                        lnkSustn3.Attributes.Add("CommandArgument", ds.Tables(0).Rows(i).Item("TID").ToString())
                        lnkSustn3.Attributes.Add("OnClick", "return ClosePop('lnkSustn3');")
                        lnkSustn3.CssClass = "Service"

                    ElseIf ds.Tables(0).Rows(i).Item("SERVICEID").ToString() = "85" Then
                        lnkSustn4.Enabled = True
                        lnkSustn4.Attributes.Add("CommandArgument", ds.Tables(0).Rows(i).Item("TID").ToString())
                        lnkSustn4.Attributes.Add("OnClick", "return ClosePop('lnkSustn4');")
                        lnkSustn4.CssClass = "Service"

                    ElseIf ds.Tables(0).Rows(i).Item("SERVICEID").ToString() = "902" Then
                        lnkSA.Enabled = True
                        lnkSA.Attributes.Add("CommandArgument", ds.Tables(0).Rows(i).Item("TID").ToString())
                        lnkSA.Attributes.Add("OnClick", "return ClosePop('lnkSA');")
                        lnkSA.CssClass = "Service"

                    ElseIf ds.Tables(0).Rows(i).Item("SERVICEID").ToString() = "882" Then
                        lnkCompE1.Enabled = True
                        lnkCompE1.Attributes.Add("CommandArgument", ds.Tables(0).Rows(i).Item("TID").ToString())
                        lnkCompE1.Attributes.Add("OnClick", "return ClosePop('lnkCompE1');")
                        lnkCompE1.CssClass = "Service"

                    ElseIf ds.Tables(0).Rows(i).Item("SERVICEID").ToString() = "883" Then
                        lnkCompS1.Enabled = True
                        lnkCompS1.Attributes.Add("CommandArgument", ds.Tables(0).Rows(i).Item("TID").ToString())
                        lnkCompS1.Attributes.Add("OnClick", "return ClosePop('lnkCompS1');")
                        lnkCompS1.CssClass = "Service"

                    ElseIf ds.Tables(0).Rows(i).Item("SERVICEID").ToString() = "323" Then
                        lnkEdist.Enabled = True
                        lnkEdist.Attributes.Add("CommandArgument", ds.Tables(0).Rows(i).Item("TID").ToString())
                        lnkEdist.Attributes.Add("OnClick", "return ClosePop('lnkEdist');")
                        lnkEdist.CssClass = "Service"

                    ElseIf ds.Tables(0).Rows(i).Item("SERVICEID").ToString() = "382" Then
                        lnkSdist.Enabled = True
                        lnkSdist.Attributes.Add("CommandArgument", ds.Tables(0).Rows(i).Item("TID").ToString())
                        lnkSdist.Attributes.Add("OnClick", "return ClosePop('lnkSdist');")
                        lnkSdist.CssClass = "Service"

                    ElseIf ds.Tables(0).Rows(i).Item("SERVICEID").ToString() = "82" Then
                        lnkEchem.Enabled = True
                        lnkEchem.Attributes.Add("CommandArgument", ds.Tables(0).Rows(i).Item("TID").ToString())
                        lnkEchem.Attributes.Add("OnClick", "return ClosePop('lnkEchem');")
                        lnkEchem.CssClass = "Service"

                    ElseIf ds.Tables(0).Rows(i).Item("SERVICEID").ToString() = "83" Then
                        lnkSchem.Enabled = True
                        lnkSchem.Attributes.Add("CommandArgument", ds.Tables(0).Rows(i).Item("TID").ToString())
                        lnkSchem.Attributes.Add("OnClick", "return ClosePop('lnkSchem');")
                        lnkSchem.CssClass = "Service"

                    ElseIf ds.Tables(0).Rows(i).Item("SERVICEID").ToString() = "324" Then
                        lnkRetail.Enabled = True
                        lnkRetail.Attributes.Add("CommandArgument", ds.Tables(0).Rows(i).Item("TID").ToString())
                        lnkRetail.Attributes.Add("OnClick", "return ClosePop('lnkRetail');")
                        lnkRetail.CssClass = "Service"
                    ElseIf ds.Tables(0).Rows(i).Item("SERVICEID").ToString() = "922" Then
                        lnkMedEcon1.Enabled = True
                        lnkMedEcon1.Attributes.Add("CommandArgument", ds.Tables(0).Rows(i).Item("TID").ToString())
                        lnkMedEcon1.Attributes.Add("OnClick", "return ClosePop('lnkMedEcon1');")
                        lnkMedEcon1.CssClass = "Service"
                    ElseIf ds.Tables(0).Rows(i).Item("SERVICEID").ToString() = "923" Then
                        lnkMedEcon2.Enabled = True
                        lnkMedEcon2.Attributes.Add("CommandArgument", ds.Tables(0).Rows(i).Item("TID").ToString())
                        lnkMedEcon2.Attributes.Add("OnClick", "return ClosePop('lnkMedEcon2');")
                        lnkMedEcon2.CssClass = "Service"
                    ElseIf ds.Tables(0).Rows(i).Item("SERVICEID").ToString() = "924" Then
                        lnkSMed1.Enabled = True
                        lnkSMed1.Attributes.Add("CommandArgument", ds.Tables(0).Rows(i).Item("TID").ToString())
                        lnkSMed1.Attributes.Add("OnClick", "return ClosePop('lnkSMed1');")
                        lnkSMed1.CssClass = "Service"
                    ElseIf ds.Tables(0).Rows(i).Item("SERVICEID").ToString() = "925" Then
                        lnkSMed2.Enabled = True
                        lnkSMed2.Attributes.Add("CommandArgument", ds.Tables(0).Rows(i).Item("TID").ToString())
                        lnkSMed2.Attributes.Add("OnClick", "return ClosePop('lnkSMed2');")
                        lnkSMed2.CssClass = "Service"


                    ElseIf ds.Tables(0).Rows(i).Item("SERVICEID").ToString() = "942" Then
                        lnkMOLDE1.Enabled = True
                        lnkMOLDE1.Attributes.Add("CommandArgument", ds.Tables(0).Rows(i).Item("TID").ToString())
                        lnkMOLDE1.Attributes.Add("OnClick", "return ClosePop('lnkMOLDE1');")
                        lnkMOLDE1.CssClass = "Service"
                    ElseIf ds.Tables(0).Rows(i).Item("SERVICEID").ToString() = "943" Then
                        lnkMOLDE2.Enabled = True
                        lnkMOLDE2.Attributes.Add("CommandArgument", ds.Tables(0).Rows(i).Item("TID").ToString())
                        lnkMOLDE2.Attributes.Add("OnClick", "return ClosePop('lnkMOLDE2');")
                        lnkMOLDE2.CssClass = "Service"
                    ElseIf ds.Tables(0).Rows(i).Item("SERVICEID").ToString() = "944" Then
                        lnkMOLDS1.Enabled = True
                        lnkMOLDS1.Attributes.Add("CommandArgument", ds.Tables(0).Rows(i).Item("TID").ToString())
                        lnkMOLDS1.Attributes.Add("OnClick", "return ClosePop('lnkMOLDS1');")
                        lnkMOLDS1.CssClass = "Service"
                    ElseIf ds.Tables(0).Rows(i).Item("SERVICEID").ToString() = "945" Then
                        lnkMOLDS2.Enabled = True
                        lnkMOLDS2.Attributes.Add("CommandArgument", ds.Tables(0).Rows(i).Item("TID").ToString())
                        lnkMOLDS2.Attributes.Add("OnClick", "return ClosePop('lnkMOLDS2');")
                        lnkMOLDS2.CssClass = "Service"
                    ElseIf ds.Tables(0).Rows(i).Item("SERVICEID").ToString() = "950" Then
                        lnkEMonitor.Enabled = True
                        lnkEMonitor.Attributes.Add("CommandArgument", ds.Tables(0).Rows(i).Item("TID").ToString())
                        lnkEMonitor.Attributes.Add("OnClick", "return ClosePop('lnkEMonitor');")
                        lnkEMonitor.CssClass = "Service"
                    ElseIf ds.Tables(0).Rows(i).Item("SERVICEID").ToString() = "951" Then
                        lnkIContract.Enabled = True
                        lnkIContract.Attributes.Add("CommandArgument", ds.Tables(0).Rows(i).Item("TID").ToString())
                        lnkIContract.Attributes.Add("OnClick", "return ClosePop('lnkIContract');")
                        lnkIContract.CssClass = "Service"
                    End If
                Next
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub LinkButton_Click(ByVal sender As Object, ByVal e As EventArgs)
        Session.Clear()
        Session.RemoveAll()
        Session.Abandon()
        Response.Redirect("Login.aspx")
    End Sub

    Protected Sub lnkEdist_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkEdist.Click

    End Sub
End Class
