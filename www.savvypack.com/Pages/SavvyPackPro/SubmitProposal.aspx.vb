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
Partial Class Pages_SavvyPackPro_SubmitProposal
    Inherits System.Web.UI.Page
    Dim objGetdata As New SavvyProGetData
    Dim objUpIns As New SavvyProUpInsData

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try

            pnlValidate.Visible = True
            'grdUsers.Enabled = False

            lblFooter.Text = "Copyright 1997 - " + Now.Year().ToString() + " SavvyPack Corporation"
            If Session("SBack") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If
            If Session("USERID") = Nothing Or Session("UserName") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If

            If Not IsPostBack Then
                hidSortIdBMVendor.Value = "0"
                ChkExistingRfp()
                tabISumbitP.Enabled = True
            End If

        Catch ex As Exception
            lblError.Text = "Error: Page_Load() " + ex.Message()
        End Try
    End Sub

    Protected Sub ChkExistingRfp()
        Dim DsCheckRfp As New DataSet()
        Try
            DsCheckRfp = objGetdata.GetLatestRSbyLicenseID(Session("LicenseNo"), Session("UserName"))
            If DsCheckRfp.Tables(0).Rows.Count > 0 Then
                GetRfpDetails_new(DsCheckRfp.Tables(0).Rows(0).Item("ID").ToString(), DsCheckRfp.Tables(0).Rows(0).Item("VENDOREMAILID").ToString())
            Else
                RfpDetail.Visible = False
                tabISumbitP.Enabled = False
            End If
        Catch ex As Exception
            lblError.Text = "Error: ChkExistingRfp() " + ex.Message()
        End Try
    End Sub

    Protected Sub GetRfpDetails_new(ByVal RfpID As String, ByVal SelROwnerEmailID As String)
        Dim DsRfpdet As New DataSet()
        Try
            If RfpID <> "" Or RfpID <> "0" Then
                DsRfpdet = objGetdata.GetRfpDetailOnVendor(RfpID, SelROwnerEmailID)
                If DsRfpdet.Tables(0).Rows.Count > 0 Then
                    RfpDetail.Visible = True
                    lnkSelRFP.Text = DsRfpdet.Tables(0).Rows(0).Item("DES1").ToString()
                    lblSelRfpID.Text = DsRfpdet.Tables(0).Rows(0).Item("ID").ToString()
                    Session("ShidRfpID") = lblSelRfpID.Text
                    Session("SVendorID") = DsRfpdet.Tables(0).Rows(0).Item("VENDORID").ToString()
                    Session("BUserID") = DsRfpdet.Tables(0).Rows(0).Item("BUYERID").ToString()
                    lblbuyer.Text = DsRfpdet.Tables(0).Rows(0).Item("BUYEREMAILADD").ToString()
                    lblType.Text = DsRfpdet.Tables(0).Rows(0).Item("TYPEDESC").ToString()
                    lblSelRfpDes.Text = DsRfpdet.Tables(0).Rows(0).Item("DES1").ToString()
                    loadTab()
                    If Session("UserName") = SelROwnerEmailID Then
                        tabISumbitP.Enabled = True
                    Else
                        tabISumbitP.Enabled = False
                    End If
                Else
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('System isn't able to find ID. Please try again.');", True)
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error: GetRfpDetails() " + ex.Message()
        End Try
    End Sub

    'Protected Sub GetRfpDetails(ByVal RfpID As String)
    '    Dim DsRfpdet As New DataSet()
    '    Try
    '        If RfpID <> "" Or RfpID <> "0" Then
    '            DsRfpdet = objGetdata.GetRFPbyID(RfpID)
    '            If DsRfpdet.Tables(0).Rows.Count > 0 Then
    '                RfpDetail.Visible = True
    '                lnkSelRFP.Text = DsRfpdet.Tables(0).Rows(0).Item("DES1").ToString()
    '                lblSelRfpID.Text = DsRfpdet.Tables(0).Rows(0).Item("ID").ToString()
    '                lblType.Text = DsRfpdet.Tables(0).Rows(0).Item("TYPEDESC").ToString()
    '                Session("ShidRfpID") = lblSelRfpID.Text
    '                lblSelRfpDes.Text = DsRfpdet.Tables(0).Rows(0).Item("DES1").ToString()
    '                If Session("USERID") = DsRfpdet.Tables(0).Rows(0).Item("USERID").ToString() Then
    '                    tabISumbitP.Enabled = True
    '                Else
    '                    tabISumbitP.Enabled = False
    '                End If
    '                loadTab()
    '            Else
    '                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('System isn't able to find RFPID. Please try again.');", True)
    '            End If
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = "Error: GetRfpDetails() " + ex.Message()
    '    End Try
    'End Sub

    Protected Sub loadTab()
        Try
            ' GetUsersList()
            GetSubmitProposalList()
            GetVdueDateList()
        Catch ex As Exception
            lblError.Text = "Error: GetRfpDetails() " + ex.Message()
        End Try
    End Sub

    Protected Sub GetSubmitProposalList()
        Dim Ds As New DataSet
        Try
            Ds = objGetdata.ChkSubmitted(Session("ShidRfpID"), Session("SVendorID"))
            If Ds.Tables(0).Rows.Count > 0 Then
                BtnAccept.Enabled = False
                BtnDecline.Enabled = False
            Else
                BtnAccept.Enabled = True
                BtnDecline.Enabled = True
            End If
        Catch ex As Exception
            lblError.Text = "Error:GetSubmitProposalList:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetVdueDateList()
        Dim Ds As New DataSet
        Try
            Ds = objGetdata.GetSubmitVDetails(Session("ShidRfpID"))
            If Ds.Tables(0).Rows.Count > 0 Then
                lblDueD.Text = Ds.Tables(0).Rows(0).Item("FDUEDATE").ToString()
                lblSubmitP.Text = Ds.Tables(0).Rows(0).Item("COMPANYNAME").ToString()
            Else
                lblDueD.Text = "NA"
                lblSubmitP.Text = "NA"
            End If
        Catch ex As Exception
            lblError.Text = "Error:GetVdueDateList:" + ex.Message.ToString()
        End Try
    End Sub

#Region "Hidden Buttons"

    Protected Sub btnHidRFPCreate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHidRFPCreate.Click
        Try
            If hidRfpID.Value <> "" Or hidRfpID.Value <> "0" Then
                tabISumbitP.Enabled = True
                lnkSelRFP.Text = hidRfpNm.Value
                'GetRfpDetails(hidRfpID.Value)
                GetRfpDetails_new(hidRfpID.Value, hidRSOwnerEmailID.Value)
            End If
        Catch ex As Exception
            lblError.Text = "Error: btnHidRFPCreate_Click() " + ex.Message()
        End Try
    End Sub

    Protected Sub btnRefreshVList_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefreshVList.Click
        Try

        Catch ex As Exception
            lblError.Text = "Error: btnRefreshVList_Click() " + ex.Message()
        End Try
    End Sub

#End Region

    Protected Sub BtnAccept_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAccept.Click
        Dim count As Integer = Convert.ToInt32(Session("count"))
        Dim ProjId As New Integer
        Dim ds As New DataSet
        Dim dv As New DataView
        Dim dt As New DataTable
        Dim dsUser As New DataSet
        Dim dsF As New DataSet
        Try
            If Session("ShidRfpID") = Nothing Or Session("SVendorID") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            Else
                objUpIns.InsertDates(Session("ShidRfpID"), Session("SVendorID"), "3")
                BtnAccept.Enabled = False
                BtnDecline.Enabled = False
                ScriptManager.RegisterStartupScript(Page, Me.GetType(), "Alert", "alert('Proposal Submitted!');", True)
            End If
            'Dim Elogid As String
            'Dim dsid As New DataSet
            'Dim str As String = System.IO.Path.GetFileName(Request.CurrentExecutionFilePath).ToString()
            'Elogid = objGetdata.GetELogid()
            'ds = objGetdata.GetVendorD(Session("hidRfpID").ToString())            
            'ScriptManager.RegisterStartupScript(Page, Me.GetType(), "JSScript", "ClosePage();", True)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub BtnDecline_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnDecline.Click
        Dim count As Integer = Convert.ToInt32(Session("count"))
        Dim ProjId As New Integer
        Dim ds As New DataSet
        Dim dv As New DataView
        Dim dt As New DataTable
        Dim dsUser As New DataSet
        Dim dsF As New DataSet
        Try
            If Session("ShidRfpID") = Nothing Or Session("SVendorID") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            Else
                objUpIns.InsertDates(Session("ShidRfpID"), Session("SVendorID"), "4")
                BtnAccept.Enabled = False
                BtnDecline.Enabled = False
                ScriptManager.RegisterStartupScript(Page, Me.GetType(), "Alert", "alert('Proposal Declined!');", True)
            End If
            'Dim Elogid As String
            'Dim dsid As New DataSet
            'Dim str As String = System.IO.Path.GetFileName(Request.CurrentExecutionFilePath).ToString()
            'Elogid = objGetdata.GetELogid()
            'ds = objGetdata.GetVendorD(Session("hidRfpID").ToString())           
            'ScriptManager.RegisterStartupScript(Page, Me.GetType(), "JSScript", "ClosePage();", True)
        Catch ex As Exception

        End Try
    End Sub
End Class
