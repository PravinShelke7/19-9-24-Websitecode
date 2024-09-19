Imports System.Data
Imports System.Data.OleDb
Imports System
Imports StandGetData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Masters_StandAssist
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
                lblTag.Text = "Copyright 1997 - " + Now.Year().ToString() + " SavvyPack Corporation"
                SBA.Visible = True
                BindFlag()
            Catch ex As Exception
            End Try
            CheckSecurity()

        Catch ex As Exception
            lblError.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub CheckSecurity()
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
    End Sub

    Protected Sub GetCaseDetails()
        Dim objGetData As New StandGetData.Selectdata()
        Dim ds As New DataSet
        Try
            CaseId = Session("SBACaseId")
            ds = objGetData.GetCaseDetails(CaseId.ToString())
            lblCaseID.Text = CaseId.ToString()
            lblCaseType.Text = ds.Tables(0).Rows(0).Item("CASETYPE").ToString()
            lblCaseDe2.Text = ds.Tables(0).Rows(0).Item("CASEDES").ToString()
 lblCaseUpdate.Text = ds.Tables(0).Rows(0).Item("SERVERDATE").ToString()
            If ds.Tables(0).Rows(0).Item("CASEDE3").ToString().Length > 0 Then
                CaseDes = ds.Tables(0).Rows(0).Item("CASEDE3").ToString()
                caseDe3.Attributes.Add("onmouseover", "Tip('" + CaseDes + "')")
                caseDe3.Attributes.Add("onmouseout", "UnTip()")
            End If

        Catch ex As Exception
            lblError.Text = "Error:GetCaseDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub UserRoleWisePermission()
        Try
            If Session("SBAUserRole") <> "AADMIN" Then
                If Session("SBACaseId") < 10000 Then
                    imgUpdate.Enabled = False
                    imgUpdate.ImageUrl = "~/Images/UpdateDec.gif"
                End If

                If Session("SBAServiceRole") = "ReadOnly" Then
                    imgUpdate.Enabled = False
                    imgUpdate.ImageUrl = "~/Images/UpdateDec.gif"
                End If
            End If

            If Session("CompLib") = "Y" And Session("SBALicAdmin") = "N" Then
                imgUpdate.Enabled = False
                imgUpdate.ImageUrl = "~/Images/UpdateDec.gif"
            End If
        Catch ex As Exception

        End Try
    End Sub
   

    Protected Sub BindFlag()
        Dim ds As New DataSet
        Dim dsSessions As New DataSet
        Dim objGetData As New StandGetData.Selectdata()
        Dim trHeader As New TableRow
        Dim trHeader1 As New TableRow
        Dim trHeader2 As New TableRow
        Dim trInner As New TableRow
        Dim lbl As New Label
        Dim FlagSupId As String = String.Empty
        Dim FlagSupImg As String = String.Empty
        Dim FlagSupUrl As String = String.Empty
        Dim hid As New HiddenField
        Dim hidGrade As New HiddenField
        Dim hidDes As New HiddenField
        Dim ImgBut As New ImageButton
        Dim txt As New TextBox
        Dim tdInner As TableCell
        Dim objUpIns As New StandUpInsData.UpdateInsert()
        Dim arrFlag() As String
        Dim arrSponFlag() As String
        Dim arrSponUrl() As String
        Dim dvFlag As New DataView
        Dim dtFlag As New DataTable
        Try

            dsSessions = objGetData.GetFlagTempQuery(Session.SessionID)
            If dsSessions.Tables(0).Rows.Count > 0 Then
                arrFlag = Regex.Split(dsSessions.Tables(0).Rows(0).Item("SUPPLIERIDS").ToString(), ",")
                arrSponFlag = Regex.Split(dsSessions.Tables(0).Rows(0).Item("SPONSORFLAGS").ToString(), ",")
                arrSponUrl = Regex.Split(dsSessions.Tables(0).Rows(0).Item("SPONSORURLS").ToString(), ",")
                ds = objGetData.GetFlagSponsors()
                'ds = Session("dsFlags")
                dvFlag = ds.Tables(0).DefaultView
                For i = 0 To ds.Tables(0).Rows.Count + 1
                    trInner = New TableRow
                    For j = 1 To 2
                        tdInner = New TableCell
                        If i < ds.Tables(0).Rows.Count + 1 Then
                            If i > 0 Then
                                Select Case j
                                    Case 1
                                        InnerTdSetting(tdInner, "", "Left")
                                        ImgBut = New ImageButton
                                        dvFlag.RowFilter = "SUPPLIERID =" + arrFlag(i - 1) + " AND IMAGE='" + arrSponFlag(i - 1) + "'"
                                        dtFlag = dvFlag.ToTable()

                                        ImgBut.ID = "imgBut" + i.ToString()
                                        ImgBut.Width = 120
                                        ImgBut.Height = 80
                                        ImgBut.CommandArgument = dtFlag.Rows(0).Item("SUPPLIERID").ToString() + "," + dtFlag.Rows(0).Item("IMAGEURL").ToString()
                                        ImgBut.ImageUrl = "~/Images/" + dtFlag.Rows(0).Item("IMAGE").ToString() + ""
                                        ImgBut.Attributes.Add("onclick", "OpenURL('" + dtFlag.Rows(0).Item("IMAGEURL").ToString() + "','" + dtFlag.Rows(0).Item("SUPPLIERID").ToString() + "'); return false;")

                                        tdInner.Controls.Add(ImgBut)
                                        trInner.Controls.Add(tdInner)
                                        i = i + 1

                                    Case 2
                                        InnerTdSetting(tdInner, "", "Left")
                                        ImgBut = New ImageButton
                                        dvFlag.RowFilter = "SUPPLIERID =" + arrFlag(i - 1) + " AND IMAGE='" + arrSponFlag(i - 1) + "'"
                                        dtFlag = dvFlag.ToTable()

                                        ImgBut.ID = "imgBut" + i.ToString()
                                        ImgBut.Width = 120
                                        ImgBut.Height = 80
                                        ImgBut.CommandArgument = dtFlag.Rows(0).Item("SUPPLIERID").ToString() + "," + dtFlag.Rows(0).Item("IMAGEURL").ToString()
                                        ImgBut.ImageUrl = "~/Images/" + dtFlag.Rows(0).Item("IMAGE").ToString() + ""
                                        ImgBut.Attributes.Add("onclick", "OpenURL('" + dtFlag.Rows(0).Item("IMAGEURL").ToString() + "','" + dtFlag.Rows(0).Item("SUPPLIERID").ToString() + "'); return false;")

                                        tdInner.Controls.Add(ImgBut)
                                        trInner.Controls.Add(tdInner)

                                End Select
                            Else
                                trInner.Height = 0
                            End If

                        End If
                    Next
                    tblFlag.Controls.Add(trInner)
                Next
            Else
                ds = objGetData.GetFlagSponsors()
                For i = 0 To ds.Tables(0).Rows.Count + 1
                    trInner = New TableRow
                    For j = 1 To 2
                        tdInner = New TableCell
                        If i < ds.Tables(0).Rows.Count + 1 Then
                            If i > 0 Then
                                Select Case j
                                    Case 1
                                        InnerTdSetting(tdInner, "", "Left")
                                        ImgBut = New ImageButton

                                        ImgBut.ID = "imgBut" + i.ToString()
                                        ImgBut.Width = 120
                                        ImgBut.Height = 80
                                        ImgBut.CommandArgument = ds.Tables(0).Rows(i - 1).Item("SUPPLIERID").ToString() + "," + ds.Tables(0).Rows(i - 1).Item("IMAGEURL").ToString()
                                        ImgBut.ImageUrl = "~/Images/" + ds.Tables(0).Rows(i - 1).Item("IMAGE").ToString() + ""
                                        ImgBut.Attributes.Add("onclick", "OpenURL('" + ds.Tables(0).Rows(i - 1).Item("IMAGEURL").ToString() + "','" + ds.Tables(0).Rows(i - 1).Item("SUPPLIERID").ToString() + "'); return false;")
                                        'AddHandler ImgBut.Click, AddressOf ImgBut_Click
                                        tdInner.Controls.Add(ImgBut)
                                        trInner.Controls.Add(tdInner)
                                        i = i + 1

                                    Case 2
                                        InnerTdSetting(tdInner, "", "Left")
                                        ImgBut = New ImageButton

                                        ImgBut.ID = "imgBut" + i.ToString()
                                        ImgBut.Width = 120
                                        ImgBut.Height = 80
                                        ImgBut.CommandArgument = ds.Tables(0).Rows(i - 1).Item("SUPPLIERID").ToString() + "," + ds.Tables(0).Rows(i - 1).Item("IMAGEURL").ToString()
                                        ImgBut.ImageUrl = "~/Images/" + ds.Tables(0).Rows(i - 1).Item("IMAGE").ToString() + ""
                                        ImgBut.Attributes.Add("onclick", "OpenURL('" + ds.Tables(0).Rows(i - 1).Item("IMAGEURL").ToString() + "','" + ds.Tables(0).Rows(i - 1).Item("SUPPLIERID").ToString() + "'); return false;")
                                        'AddHandler ImgBut.Click, AddressOf ImgBut_Click
                                        tdInner.Controls.Add(ImgBut)
                                        trInner.Controls.Add(tdInner)

                                End Select
                            Else
                                trInner.Height = 0
                            End If

                        End If
                    Next
                    tblFlag.Controls.Add(trInner)
                Next
                For j = 0 To ds.Tables(0).Rows.Count - 1
                    If j = 0 Then
                        FlagSupId = ds.Tables(0).Rows(j).Item("SUPPLIERID").ToString()
                        FlagSupImg = ds.Tables(0).Rows(j).Item("IMAGE").ToString()
                        FlagSupUrl = ds.Tables(0).Rows(j).Item("IMAGEURL").ToString()
                    Else
                        FlagSupId = FlagSupId + "," + ds.Tables(0).Rows(j).Item("SUPPLIERID").ToString()
                        FlagSupImg = FlagSupImg + "," + ds.Tables(0).Rows(j).Item("IMAGE").ToString()
                        FlagSupUrl = FlagSupUrl + "," + ds.Tables(0).Rows(j).Item("IMAGEURL").ToString()
                    End If
                Next
                objUpIns.InsertFlagTemp(Session.SessionID, FlagSupId, FlagSupImg, FlagSupUrl)
            End If
            'Session("dsFlags") = ds
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub InnerTdSetting(ByVal Td As TableCell, ByVal Width As String, ByVal Align As String)
        Try

            If Width <> "" Then
                Td.Style.Add("width", Width)
            End If
            Td.Style.Add("text-align", Align)
            If Align = "Left" Then
                Td.Style.Add("padding-left", "5px")
            End If
            If Align = "Right" Then
                Td.Style.Add("padding-right", "5px")
            End If
            Td.Style.Add("font-size", "13px")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ImgBut_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim objUpIns As New StandUpInsData.UpdateInsert()
        Dim FlagSupp As ImageButton = TryCast(sender, ImageButton)
        Dim argument() As String = FlagSupp.CommandArgument.Split(",")

        'objUpIns.InsertLog4(Session("UserId").ToString(), Session("UserName").ToString(), "SupplierMaterials.aspx", Page.Title, "Clicked on Flag for Sponsor: " + argument(0) + "", Session("SBACaseId"), "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "Y", argument(0))

        Response.Write("<script type='text/javascript'>window.open('" + argument(1) + "','_blank');</script>")

    End Sub

    'Protected Sub btnSearchCriteria_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchCriteria.Click

    'End Sub


End Class

