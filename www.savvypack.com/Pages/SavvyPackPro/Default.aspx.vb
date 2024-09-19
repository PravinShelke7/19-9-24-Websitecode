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
Partial Class Pages_Emonitor_Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            lblTag.Text = "Copyright 1997 - " + Now.Year().ToString() + " SavvyPack Corporation"
        Catch ex As Exception
        End Try
        If Session("Back") = Nothing Then
            Dim obj As New CryptoHelper
            Response.Redirect("Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
        End If

        If Not IsPostBack Then
            GetUserSpecDetails()
            ChkExistingRfp() 'check status
            ChkSku() 'skumanager
            GetUserRFPDetails() 'config manager
            GetIssueRFPDetails() 'issue rfp
            GetIssueAnalyze() 'analyze rfp

            '  GetSubmitProposalbyrfp()
            GetSubmitPDetails()
            GetSupplierRDetails()

            GetEmailStored()
            'GetIssuedRFPDetails()


            ChkSku1()



            'ScriptManager.RegisterStartupScript(Page, Me.GetType(), "JSScript", "RefreshPage();", True)

        End If

    End Sub
    Public Sub GetUserSpecDetails()
        Dim dsS As New DataSet
        Dim objGetdata As New SavvyProGetData

        Dim DsCheckRfp As DataSet

        Try
            dsS = objGetdata.GetUSKUByLicenseID_N(Session("LicenseNo"), Session("UserId"))


            DsCheckRfp = objGetdata.GetRFPbyLicID1(Session("LicenseNo"), Session("USERID")) 'check rfp by licence id and userid

            If DsCheckRfp.Tables(0).Rows.Count > 0 Then
                If dsS.Tables(0).Rows.Count > 0 Then
                    'For i = 0 To dsS.Tables(0).Rows.Count - 1
                    '    If dsS.Tables(0).Rows(i).Item("USERID").ToString() = Session("UserId").ToString() Then
                    '        lnkRFPManager.Enabled = True
                    '        lnkRFPManager.CssClass = "Link"
                    '    End If
                    'Next
                    lnkRFPManager.Enabled = True
                    lnkRFPManager.CssClass = "Link"

                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

   



    Public Sub ChkSku()
        Dim dsS As New DataSet
        Dim dsR As New DataSet
        Dim objGetdata As New SavvyProGetData
        Dim ID As String = ""
        Dim DsE As New DataSet
        Dim ds As New DataSet
        Dim dsL As New DataSet
        Try

            dsS = objGetdata.GetSKUStruct(Session("UserId")) 'check sku by Uid
            dsR = objGetdata.ChkRFPbyUid(Session("UserId")) 'check rfp  by uid
            dsL = objGetdata.ChkRFPbyLicenseID(Session("LicenseNo")) '

            dsS = objGetdata.GetUSKUByLicenseID_N(Session("LicenseNo"), Session("UserId")) '


            'ID = dsR.Tables(0).Rows(0).Item("ID").ToString()

            ' DsE = objGetdata.GetSKUByRfpID(ID, Session("USERID"))


            If dsS.Tables(0).Rows.Count > 0 Then ' 

                If dsL.Tables(0).Rows.Count > 0 Then
                    If dsR.Tables(0).Rows.Count > 0 Then
                        If dsS.Tables(0).Rows.Count > 0 Then
                            lnkSkuManager.Enabled = True
                            lnkSkuManager.CssClass = "Link"

                        End If
                    End If
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub





    Public Sub ChkSku1()
        Dim dsS As New DataSet
        Dim dsR As New DataSet
        Dim objGetdata As New SavvyProGetData
        Dim ID As String = ""
        Dim DsE As New DataSet
        Dim ds As New DataSet
        Dim dsL As New DataSet
        Try


            dsL = objGetdata.ChkRFPbyLicenseID(Session("LicenseNo")) '

            ds = objGetdata.GetUSKUByLicenseID(Session("LicenseNo"))

            '  DsE = objGetdata.GetUSKUByLicenseID(Session("LicenseNo"))



            dsS = objGetdata.GetUSKUByLicenseID_N(Session("LicenseNo"), Session("UserId")) '


            'ID = dsR.Tables(0).Rows(0).Item("ID").ToString()

            ' DsE = objGetdata.GetSKUByRfpID(ID, Session("USERID"))


            If dsS.Tables(0).Rows.Count > 0 Then ' 

                If dsL.Tables(0).Rows.Count > 0 Then

                    If ds.Tables(0).Rows.Count > 0 Then
                        lnkSkuManager.Enabled = True
                        lnkSkuManager.CssClass = "Link"

                    End If
                End If
            End If


        Catch ex As Exception

        End Try
    End Sub






















    Public Sub GetUserRFPDetails()
        Dim dsR As New DataSet
        Dim objGetdata As New SavvyProGetData
        Dim dsS As New DataSet
        Dim DsChk As New DataSet
        Dim DsE As New DataSet
        Dim ID As String = ""
        Dim DsCheckRfp As New DataSet
        Try
            dsS = objGetdata.GetSKUStruct(Session("UserId"))

            dsR = objGetdata.ChkRFPbyLicenseID(Session("LicenseNo"))

            DsChk = objGetdata.ChkRFPbyUid(Session("UserId")) 'check rfp by uid


            ID = DsChk.Tables(0).Rows(0).Item("ID").ToString() ' RFP by ID
            DsE = objGetdata.GetSKUByRfpID(ID, Session("USERID")) 'Gets SKU BY RfpId

            dsS = objGetdata.GetUSKUByLicenseID_N(Session("LicenseNo"), Session("UserId")) '

            DsCheckRfp = objGetdata.GetRFPbyLicID1(Session("LicenseNo"), Session("USERID")) 'check rfp by licence id and userid

            If DsCheckRfp.Tables(0).Rows.Count > 0 Then '

                If dsS.Tables(0).Rows.Count > 0 Then '

                    If dsR.Tables(0).Rows.Count > 0 Then
                        If DsE.Tables(0).Rows.Count > 0 Then


                            'For i = 0 To dsR.Tables(0).Rows.Count - 1
                            '    If dsR.Tables(0).Rows(i).Item("USERID").ToString() = Session("UserId").ToString() Then
                            '        lnkConfRFP.Enabled = True
                            '        lnkConfRFP.CssClass = "Link"
                            '    End If
                            'Next
                            lnkConfRFP.Enabled = True ''''''''''''''''''''''''''''
                            lnkConfRFP.CssClass = "Link"
                            'lnkRFPStatus.Enabled = True
                            'lnkRFPStatus.CssClass = "Link"

                        End If
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub



    Public Sub GetIssueRFPDetails()
        Dim dsI As New DataSet
        Dim objGetdata As New SavvyProGetData
        Dim dsR As New DataSet
        Dim dsS As New DataSet
        Dim DsChk As New DataSet
        Dim DsE As New DataSet

        Dim DsCheckRfp As DataSet

        Try

            DsChk = objGetdata.ChkRFPbyUid(Session("UserId")) ' check rfp by uid'
            ID = DsChk.Tables(0).Rows(0).Item("ID").ToString()

            dsS = objGetdata.GetSKUStruct(Session("UserId")) ' check sku by uid'

            dsI = objGetdata.GetIssueRFP()

            DsE = objGetdata.GetVendorD(ID) 'get vendor by rfpid

            dsS = objGetdata.GetUSKUByLicenseID_N(Session("LicenseNo"), Session("UserId")) '


            DsCheckRfp = objGetdata.GetRFPbyLicID1(Session("LicenseNo"), Session("USERID")) 'check rfp by licence id and userid

            If DsCheckRfp.Tables(0).Rows.Count > 0 Then ''

                If DsE.Tables(0).Rows.Count > 0 Then
                    If dsS.Tables(0).Rows.Count > 0 Then ''
                        If dsI.Tables(0).Rows.Count > 0 Then
                            For i = 0 To dsI.Tables(0).Rows.Count - 1
                                If dsI.Tables(0).Rows(i).Item("VENDORID").ToString() <> Nothing Then
                                    lnkIssueRFP.Enabled = True
                                    lnkIssueRFP.CssClass = "Link"


                                End If

                            Next
                        End If
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Sub GetEmailStored()
        Dim dsI As New DataSet
        Dim objGetdata As New SavvyProGetData
        Try
            dsI = objGetdata.GetStoredEmailDetails()
            If dsI.Tables(0).Rows.Count > 0 Then
                For i = 0 To dsI.Tables(0).Rows.Count - 1
                    'If dsI.Tables(0).Rows(0).Item("STATUS").ToString() <> Nothing Then
                    '    lnkSConfigRFP.Enabled = True
                    '    lnkSConfigRFP.CssClass = "Link"
                    'End If
                Next
            End If
        Catch ex As Exception

        End Try
    End Sub
    'Public Sub GetIssuedRFPDetails()
    '    Dim dsIssued As New DataSet
    '    Dim objGetdata As New SavvyProGetData
    '    Dim ds As New DataSet

    '    Try
    '        dsIssued = objGetdata.GetRFPbyLicenseID(Session("LicenseNo"))

    '        'ds = objGetdata.GetRFPList1("", Session("USERID")) 'rfp list by uid 



    '        'If ds.Tables(0).Rows.Count > 0 Then ''

    '        If dsIssued.Tables(0).Rows.Count > 0 Then
    '            ' For i = 0 To dsIssued.Tables(0).Rows.Count - 1
    '            'If dsIssued.Tables(0).Rows(i).Item("USERID").ToString() = Session("UserId").ToString() Then
    '            '    lnkSConfigRFP.Enabled = True
    '            '    lnkSConfigRFP.CssClass = "Link"
    '            '    lnkRFPStatus.Enabled = True
    '            '    lnkRFPStatus.CssClass = "Link"
    '            'End If
    '            ' Next
    '            lnkSConfigRFP.Enabled = True
    '            lnkSConfigRFP.CssClass = "Link"


    '            lnkSubPro.Enabled = True
    '            lnkSubPro.CssClass = "Link"

    '            lnkanaProp.Enabled = True
    '            lnkanaProp.CssClass = "Link"



    '        End If
    '        '  End If ''
    '        'If dsIssued.Tables(0).Rows.Count > 0 Then
    '        '    For i = 0 To dsIssued.Tables(0).Rows.Count - 1
    '        '        If dsIssued.Tables(0).Rows(i).Item("ISSUED").ToString() = "Y" Then
    '        '            lnkSConfigRFP.Enabled = True
    '        '            lnkSConfigRFP.CssClass = "Link"
    '        '            lnkRFPStatus.Enabled = True
    '        '            lnkRFPStatus.CssClass = "Link"
    '        '        End If
    '        '    Next
    '        'End If
    '    Catch ex As Exception

    '    End Try
    'End Sub
    Public Sub GetSubmitPDetails()
        Dim dsS As New DataSet
        Dim objGetdata As New SavvyProGetData
        Try
            dsS = objGetdata.GetSubmitProposal(Session("LicenseNo"))

            'dsS = objGetdata.GetSubmitP(Session("USERID")) ''

            If dsS.Tables(0).Rows.Count > 0 Then
                For i = 0 To dsS.Tables(0).Rows.Count - 1
                    If dsS.Tables(0).Rows(i).Item("STATUSID").ToString() = "6" Then
                        lnkAProposal.Enabled = True
                        lnkAProposal.CssClass = "Link"
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub GetSupplierRDetails()
        Dim dsS As New DataSet
        Dim objGetdata As New SavvyProGetData
        Dim ds As New DataSet
        Dim dsS1 As New DataSet
        Try
            ds = objGetdata.GetRFPList1("", Session("USERID")) 'rfp list by uid 
            'dsS = objGetdata.GetAllRequestByLicense(Session("LicenseNo")) 'vendor list  


            'dsS1 = objGetdata.GetAllRequestByLicense1(Session("LicenseNo"), Session("USERID")) 'vendor list  


            If ds.Tables(0).Rows(0)("STATUS").ToString() = "ISSUED" Then
                'If ds.Tables(0).Rows.Count > 0 Then
                lnkSConfigRFP.Enabled = True
                lnkSConfigRFP.CssClass = "Link"


                lnkSubPro.Enabled = True
                lnkSubPro.CssClass = "Link"

                lnkanaProp.Enabled = True
                lnkanaProp.CssClass = "Link"



            End If
            ' End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Response.Redirect(HttpContext.Current.Request.Url.ToString(), True)
        Catch ex As Exception
            lblError.Text = "Error: btnRefresh_Click() " + ex.Message()
        End Try
    End Sub

    Protected Sub lnkBUser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBUser.Click

    End Sub


    Public Sub ChkExistingRfp()
        Dim DsCheckRfp As New DataSet()
        Dim DsChk As New DataSet()
        Dim Dv As New DataView()
        Dim UID As String = ""

        Dim ULIC As String = ""


        Dim objGetdata As New SavvyProGetData
        Try

            UID = Session("USERID").ToString()
            ULIC = Session("LicenseNo").ToString()

            DsCheckRfp = objGetdata.GetRFPbyLicID1(Session("LicenseNo"), Session("USERID")) 'check rfp by licence id and userid

            DsChk = objGetdata.ChkRFPbyUid(UID)
            Dv = DsChk.Tables(0).DefaultView

            If DsCheckRfp.Tables(0).Rows.Count > 0 Then

                If DsChk.Tables(0).Rows.Count > 0 Then

                    'lnkSkuManager.Enabled = True
                    'lnkSkuManager.CssClass = "Link"

                    lnkRFPStatus.Enabled = True
                    lnkRFPStatus.CssClass = "Link"
                    'GetRFPDetails(DsCheckRfp.Tables(0).Rows(0).Item("ID").ToString())
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub


    Public Sub GetRFPDetails(ByVal ID As String)
        Dim Ds As New DataSet()
        Dim objGetdata As New SavvyProGetData
        Try
            Ds = objGetdata.GetSKUByRfpID(Session("USERID"))

        Catch ex As Exception

        End Try

    End Sub

    Private Sub GetIssueAnalyze()
        Dim Ds As New DataSet()
        Dim Ds1 As New DataSet()
        Dim objGetdata As New SavvyProGetData

        Try

            Ds = objGetdata.GetRFPList1("", Session("USERID")) 'rfp list by uid 

            Ds1 = objGetdata.GetRFPList11("", Session("LicenseNo"), Session("USERID")) 'rfp list by uid and lic

            If Ds1.Tables(0).Rows.Count > 0 Then '


                If Ds.Tables(0).Rows(0)("STATUS").ToString() = "ISSUED" Then
                    lnkAProposal.Enabled = True
                    lnkAProposal.CssClass = "Link"
                End If

            End If
        Catch ex As Exception

        End Try

    End Sub

    'Private Sub GetSubmitProposalbyrfp()
    '    Dim DsChk As New DataSet
    '    Dim DsE As New DataSet
    '    Dim objGetdata As New SavvyProGetData
    '    Dim dsIssued As New DataSet
    '    Try
    '        DsChk = objGetdata.ChkRFPbyUid(Session("UserId")) ' check rfp by uid'
    '        dsIssued = objGetdata.GetRFPbyLicenseID(Session("LicenseNo"))

    '        If dsIssued.Tables(0).Rows.Count > 0 Then
    '            If DsChk.Tables(0).Rows.Count > 0 Then
    '                lnkSubPro.Enabled = True
    '                lnkSubPro.CssClass = "Link"

    '                lnkanaProp.Enabled = True
    '                lnkanaProp.CssClass = "Link"


    '            End If

    '        End If
    '    Catch ex As Exception

    '    End Try

    'End Sub

   
End Class


