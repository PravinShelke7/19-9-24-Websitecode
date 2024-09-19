Imports System.Data
Imports System.Data.OleDb
Imports System
Imports StandGetData
Imports StandUpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Imports System.Net.Mail
Imports System.Net
Partial Class Pages_StandAssist_PopUp_GetSupplier
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("Back") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If

            Dim objUpIn As New StandUpInsData.UpdateInsert()
            hidGradedes.Value = Request.QueryString("Des").ToString()
            hidGradeId.Value = Request.QueryString("ID").ToString()
            hidSupId.Value = Request.QueryString("SupID").ToString()
            hidImgId.Value = Request.QueryString("ImgId").ToString()
            hidImgSet.Value = Request.QueryString("ImgDis").ToString()

            Page.Title = "Supplier Details"
            If Not IsPostBack Then
                hidMatID.Value = Request.QueryString("MatId").ToString()
                GetSupplierGrade()
				'Started Activity Log Changes
                Try
                    objUpIn.InsertLog1(Session("UserId").ToString(), "12", "Opened Sponsored/Non Sponsored Grade PopUp", Session("SBACaseId"), Session("LogInCount").ToString(), Session.SessionID, Request.QueryString("MatId").ToString(), "", "", "", "")
                Catch ex As Exception

                End Try
                'Ended Activity Log Changes
                End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GetSupplierGrade()
        Try
            Dim objGetdata As New StandGetData.Selectdata()
            Dim ds As New DataSet()
            Dim i As New Integer
            Dim j As New Integer
            Dim DWidth As String = String.Empty
            Dim trHeader As New TableRow
            Dim trHeader1 As New TableRow
            'Dim trHeader2 As New TableRow
            Dim trInner As New TableRow
            Dim trInner1 As New TableRow
            Dim trInner2 As New TableRow
            Dim tdHeader As TableCell
            Dim tdHeader1 As TableCell
            Dim lbl As New Label
            Dim hid As New HiddenField
            Dim hidGrade As New HiddenField
            Dim Link As New HyperLink
            Dim txt As New TextBox
            Dim tdInner As TableCell
            Dim tdInner1 As TableCell
            Dim dsSeq As New DataSet()
            Dim dsSeqD As New DataSet()
            Dim dvSeq As New DataView
            Dim dsSpons As New DataSet()
            Dim dsSupplier As New DataSet()
            Dim dsNonSpons As New DataSet()
            Dim Path As String = String.Empty
            Dim Mail As String
            Dim dvSup As New DataView
            Dim dtSup As New DataTable
            Dim dvSup1 As New DataView
            Dim dtSup1 As New DataTable
            Dim cnt As Integer
            Dim dvGrade As New DataView
            Dim dtGrade As New DataTable
            Dim l As Integer
            Dim n As Integer
            Dim dsSupplier1 As New DataSet()
            Dim ImgBut As New ImageButton
            Dim ImgDis As New ImageButton
            Dim hid1 As New HiddenField
            Dim dsSheet As New DataSet()


            dsSpons = objGetdata.GetSponsorSupplier(hidMatID.Value.ToString(), True)
            dsNonSpons = objGetdata.GetSponsorSupplier(hidMatID.Value.ToString(), False)
            If dsSpons.Tables(0).Rows.Count = 0 And dsNonSpons.Tables(0).Rows.Count = 0 Then
                lblPcase.Visible = True
            Else
                lblPcase.Visible = False
                If dsSpons.Tables(0).Rows.Count > 0 Then

                    dsSupplier = objGetdata.GetMatSupplier(hidMatID.Value.ToString(), True)
                    Dim strSupplier(dsSupplier.Tables(0).Rows.Count - 1) As String
                    For l = 0 To dsSupplier.Tables(0).Rows.Count - 1
                        If l = 0 Then
                            strSupplier(l) = dsSupplier.Tables(0).Rows(l).Item("SUPPLIERID").ToString()
                        Else
                            strSupplier(l) = dsSupplier.Tables(0).Rows(l).Item("SUPPLIERID").ToString()
                        End If
                    Next
                    For i = 1 To 2
                        tdHeader = New TableCell
                        Dim Title As String = String.Empty
                        'Header
                        Select Case i
                            Case 1
                                HeaderTdSetting(tdHeader, "200px", "Suppliers", "1")
                                trHeader.Controls.Add(tdHeader)
                            Case 2
                                HeaderTdSetting(tdHeader, "190px", "Grades", "1")
                                trHeader.Controls.Add(tdHeader)
                        End Select
                    Next
                    trHeader.Height = 30
                    trHeader.Height = 30
                    tblSupplierGrade.Controls.Add(trHeader)
                    'Inner


                    For l = 0 To strSupplier.Length - 1

                        dvSup = dsSpons.Tables(0).DefaultView
                        Dim dtSeq As New DataTable
                        dvSup.RowFilter = "SUPPLIERID=" + strSupplier(l).ToString()
                        dtSup = dvSup.ToTable()

                        'Getting Tec Data
                        'dsSheet = objGetdata.GetSupplierMatGradeCheck(hidMatID.Value, dtSup.Rows(0).Item("SUPPLIERID").ToString(), dtSup.Rows(0).Item("GRADEID").ToString())

                        If dtSup.Rows.Count > 0 Then
                            For i = 0 To dtSup.Rows.Count
                                cnt = i
                                trInner = New TableRow
                                For j = 1 To 2
                                    tdInner = New TableCell
                                    Select Case j

                                        Case 1
                                            If i = 0 Then
                                                InnerTdSetting(tdInner, "", "Left")
                                                lbl = New Label
                                                lbl.Text = dtSup.Rows(i).Item("NAME").ToString()
                                                lbl.Font.Bold = True
                                                If dtSup.Rows.Count > 1 Then
                                                    tdInner.RowSpan = dtSup.Rows.Count + 1
                                                Else
                                                    tdInner.RowSpan = dtSup.Rows.Count + 1
                                                End If

                                                tdInner.Controls.Add(lbl)
                                                trInner.Controls.Add(tdInner)
                                            End If


                                        Case 2
                                            InnerTdSetting(tdInner, "", "Left")
                                            If i = 0 Then
                                                Link = New HyperLink
                                                hid = New HiddenField
                                                Link.Width = 130
                                                Link.CssClass = "Link"
                                                Link.Text = "Nothing Selected"
                                                Link.ToolTip = "Nothing Selected"
                                                Link.NavigateUrl = "javascript:GradeDet('" + dtSup.Rows(i).Item("NAME").ToString() + "','" + dtSup.Rows(i).Item("SUPPLIERID").ToString() + "','Nothing Selected','0')"
                                                tdInner.Controls.Add(Link)
                                                trInner.Controls.Add(tdInner)
                                            Else
                                                'tdInner = New TableCell
                                                Link = New HyperLink
                                                hid = New HiddenField
                                                Link.Width = 150
                                                Link.CssClass = "Link"
                                                Link.Text = dtSup.Rows(i - 1).Item("GRADENAME").ToString()
                                                Link.ToolTip = dtSup.Rows(i - 1).Item("GRADENAME").ToString()

						'Getting Tec Data
                                                dsSheet = objGetdata.GetSupplierMatGradeCheck(hidMatID.Value, dtSup.Rows(0).Item("SUPPLIERID").ToString(), dtSup.Rows(i - 1).Item("GRADEID").ToString())


                                                'If dsSheet.Tables(0).Rows.Count > 0 Then
                                                '   Link.NavigateUrl = "javascript:GradeDet('" + dtSup.Rows(i - 1).Item("NAME").ToString() + "','" + dtSup.Rows(i - 1).Item("SUPPLIERID").ToString() + "','" + dtSup.Rows(i - 1).Item("GRADENAME").ToString() + "','" + dtSup.Rows(i - 1).Item("GRADEID").ToString() + "','True')"
                                                'Else
                                                '    Link.NavigateUrl = "javascript:GradeDet('" + dtSup.Rows(i - 1).Item("NAME").ToString() + "','" + dtSup.Rows(i - 1).Item("SUPPLIERID").ToString() + "','" + dtSup.Rows(i - 1).Item("GRADENAME").ToString() + "','" + dtSup.Rows(i - 1).Item("GRADEID").ToString() + "','False')"
                                                'End If

                                               
                                                'Enabled Button
                                                ImgBut = New ImageButton
                                                ImgBut.ID = "imgBut" + i.ToString()
                                                ImgBut.Width = 20
                                                ImgBut.Height = 18
                                                ImgBut.ImageUrl = "~/Images/Notes Icon2.png"

                                                'Disbaled Button
                                                ImgDis = New ImageButton
                                                ImgDis.ID = "imgDis" + i.ToString()
                                                ImgDis.Width = 20
                                                ImgDis.Height = 18
                                                ImgDis.ImageUrl = "~/Images/Notes Icon3.png"
                                                ImgDis.ToolTip = "Technical data sheet not available now."

                                                Path = "SupplierMaterials.aspx"
                                                tdInner.Controls.Add(Link)

                                                If dsSheet.Tables(0).Rows.Count > 0 Then
                                                    Link.NavigateUrl = "javascript:GradeDet('" + dtSup.Rows(i - 1).Item("NAME").ToString() + "','" + dtSup.Rows(i - 1).Item("SUPPLIERID").ToString() + "','" + dtSup.Rows(i - 1).Item("GRADENAME").ToString() + "','" + dtSup.Rows(i - 1).Item("GRADEID").ToString() + "','True')"
                                                    ImgBut.Attributes.Add("onclick", "OpenTDataSheet('" + dtSup.Rows(i - 1).Item("SUPPLIERID").ToString() + "','" + hidMatID.Value.ToString() + "','" + Path + "','" + dtSup.Rows(i - 1).Item("GRADEID").ToString() + "'); return false;")
                                                    tdInner.Controls.Add(ImgBut)
                                                Else
                                                    If dtSup.Rows(i - 1).Item("DESCRIPTION").ToString() = "" Then
                                                        Link.NavigateUrl = "javascript:GradeDet('" + dtSup.Rows(i - 1).Item("NAME").ToString() + "','" + dtSup.Rows(i - 1).Item("SUPPLIERID").ToString() + "','" + dtSup.Rows(i - 1).Item("GRADENAME").ToString() + "','" + dtSup.Rows(i - 1).Item("GRADEID").ToString() + "','False')"
                                                        ImgDis.Attributes.Add("onclick", "OpenTDataSheet('" + dtSup.Rows(i - 1).Item("SUPPLIERID").ToString() + "','" + hidMatID.Value.ToString() + "','" + Path + "','" + dtSup.Rows(i - 1).Item("GRADEID").ToString() + "'); return false;")
                                                        ImgDis.Enabled = False
                                                        tdInner.Controls.Add(ImgDis)
                                                    Else
                                                        Link.NavigateUrl = "javascript:GradeDet('" + dtSup.Rows(i - 1).Item("NAME").ToString() + "','" + dtSup.Rows(i - 1).Item("SUPPLIERID").ToString() + "','" + dtSup.Rows(i - 1).Item("GRADENAME").ToString() + "','" + dtSup.Rows(i - 1).Item("GRADEID").ToString() + "','True')"
                                                        ImgBut.Attributes.Add("onclick", "OpenTDataSheet('" + dtSup.Rows(i - 1).Item("SUPPLIERID").ToString() + "','" + hidMatID.Value.ToString() + "','" + Path + "','" + dtSup.Rows(i - 1).Item("GRADEID").ToString() + "'); return false;")
                                                        tdInner.Controls.Add(ImgBut)
                                                    End If

                                                End If

                                                

                                                trInner.Controls.Add(tdInner)

                                            End If

                                    End Select

                                Next
                                If (l Mod 2 = 0) Then
                                    trInner.CssClass = "AlterNateColor1"
                                Else

                                    trInner.CssClass = "AlterNateColor2"
                                End If

                                trInner.Height = 30
                                tblSupplierGrade.Controls.Add(trInner)

                            Next
                            cnt = i + 1
                        End If

                    Next
                End If

                If dsNonSpons.Tables(0).Rows.Count > 0 Then

                    dsSupplier1 = objGetdata.GetMatSupplier(hidMatID.Value.ToString(), False)
                    Dim strSupplier1(dsSupplier1.Tables(0).Rows.Count) As String
                    For n = 0 To dsSupplier1.Tables(0).Rows.Count - 1
                        If n = 0 Then
                            strSupplier1(n) = dsSupplier1.Tables(0).Rows(n).Item("SUPPLIERID").ToString()
                        Else
                            strSupplier1(n) = dsSupplier1.Tables(0).Rows(n).Item("SUPPLIERID").ToString()
                        End If
                    Next
                    For i = 1 To 3
                        tdHeader1 = New TableCell
                        Dim Title As String = String.Empty
                        'Header
                        Select Case i
                            Case 1
                                HeaderTdSetting(tdHeader1, "230px", "Non-Sponsors", "1")
                                trHeader1.Controls.Add(tdHeader1)
                            Case 2
                                HeaderTdSetting(tdHeader1, "100px", "Grades", "1")
                                trHeader1.Controls.Add(tdHeader1)
                            Case 3
                                HeaderTdSetting(tdHeader1, "100px", "Request Data", "1")
                                trHeader1.Controls.Add(tdHeader1)

                        End Select
                    Next
                    trHeader1.Height = 30
                    trHeader1.Height = 30
                    tblNonSponser.Controls.Add(trHeader1)
                    'Inner
                    For n = 0 To strSupplier1.Length - 2
                        dvSup1 = dsNonSpons.Tables(0).DefaultView
                        Dim dtSeq As New DataTable
                        dvSup1.RowFilter = "SUPPLIERID=" + strSupplier1(n).ToString()
                        dtSup1 = dvSup1.ToTable()

                        If dtSup1.Rows.Count > 0 Then
                            For i = 0 To dtSup1.Rows.Count - 1
                                trInner1 = New TableRow
                                For j = 1 To 3
                                    tdInner1 = New TableCell
                                    Select Case j

                                        Case 1
                                            If i = 0 Then
                                                InnerTdSetting(tdInner1, "", "Left")
                                                lbl = New Label
                                                lbl.Text = dtSup1.Rows(i).Item("NAME").ToString()
                                                lbl.Font.Bold = True
                                                If dtSup1.Rows.Count > 1 Then
                                                    tdInner1.RowSpan = dtSup1.Rows.Count
                                                Else
                                                    tdInner1.RowSpan = dtSup1.Rows.Count
                                                End If

                                                tdInner1.Controls.Add(lbl)
                                                trInner1.Controls.Add(tdInner1)
                                            End If


                                        Case 2
                                            InnerTdSetting(tdInner1, "", "Left")
                                            If i = -1 Then
                                                Link = New HyperLink
                                                hid = New HiddenField
                                                Link.Width = 150
                                                Link.CssClass = "Link"
                                                Link.Text = "All Grades"
                                                Link.ToolTip = "All Grades"
                                                Path = "../popup/suppliermaterials.aspx?sponsor=" + dtSup1.Rows(0).Item("supplierid").ToString() + "&matid=" + hidMatID.Value.ToString() + "&gradeid=0 "
                                                Link.NavigateUrl = "javascript:openpopup('" + Path + "')"
                                                Link.Enabled = False
                                                tdInner1.Controls.Add(Link)
                                                trInner1.Controls.Add(tdInner1)
                                            Else
                                                tdInner1 = New TableCell
                                                Link = New HyperLink
                                                hid = New HiddenField
                                                Link.Width = 150
                                                Link.CssClass = "Link"
                                                Link.Text = dtSup1.Rows(i).Item("GRADENAME").ToString()
                                                Link.ToolTip = dtSup1.Rows(i).Item("GRADENAME").ToString()
                                                Path = "../PopUp/SupplierMaterials.aspx?Sponsor=" + dtSup1.Rows(i).Item("SUPPLIERID").ToString() + "&MatId=" + hidMatID.Value.ToString() + "&GradeId=" + dtSup1.Rows(i).Item("GRADEID").ToString() + " "
                                                Link.NavigateUrl = "javascript:OpenPopup('" + Path + "')"
                                                Link.Enabled = False
                                                tdInner1.Controls.Add(Link)
                                                trInner1.Controls.Add(tdInner1)
                                                'i = i + 1
                                            End If
                                        Case 3
                                            If i < 1 Then
                                                InnerTdSetting(tdInner1, "", "Left")
                                                Link = New HyperLink
                                                Link.Width = 200
                                                Link.CssClass = "Link"
                                                Link.Text = "Send " + dtSup1.Rows(i).Item("NAME").ToString() + " an email requesting them to add their technical data sheets to the data base."
                                                'Link.ToolTip = "send an email"
                                                tdInner1.RowSpan = dtSup1.Rows.Count
                                                'Mail = dtSup1.Rows(i).Item("EMAILADDRESS").ToString()
                                                'If Mail <> "" Then
                                                Link.NavigateUrl = "javascript:SendEmail('" + dtSup1.Rows(i).Item("NAME").ToString() + "');"

                                                'End If
                                                tdInner1.Controls.Add(Link)
                                                trInner1.Controls.Add(tdInner1)
                                            End If
                                    End Select

                                Next
                                If (l Mod 2 = 0) Then
                                    trInner1.CssClass = "AlterNateColor1"
                                Else

                                    trInner1.CssClass = "AlterNateColor2"
                                End If

                                trInner1.Height = 30
                                tblNonSponser.Controls.Add(trInner1)
                            Next
                        End If
                    Next

                End If
            End If

            
        Catch ex As Exception

        End Try

    End Sub

    

    Protected Sub HeaderTdSetting(ByVal Td As TableCell, ByVal Width As String, ByVal HeaderText As String, ByVal ColSpan As String)
        Try
            Td.Text = HeaderText
            Td.ColumnSpan = ColSpan
            If Width <> "" Then
                Td.Style.Add("width", Width)
            End If
            Td.CssClass = "TdHeading"
            Td.Height = 20
            Td.Font.Size = 10
            Td.Font.Bold = True
            Td.HorizontalAlign = HorizontalAlign.Center

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Header2TdSetting(ByVal Td As TableCell, ByVal Width As String, ByVal HeaderText As String, ByVal ColSpan As String)
        Try
            Td.Text = HeaderText
            Td.ColumnSpan = ColSpan
            If Width <> "" Then
                Td.Style.Add("width", Width)
            End If
            Td.CssClass = "TdHeading"
            Td.Font.Size = 8
            Td.Height = 20
            Td.HorizontalAlign = HorizontalAlign.Center

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

    Protected Sub TextBoxSetting(ByVal txt As TextBox, ByVal Css As String)
        Try
            txt.CssClass = Css

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub LableSetting(ByVal lbl As Label, ByVal Css As String)
        Try
            lbl.CssClass = Css

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub LeftTdSetting(ByVal Td As TableCell, ByVal Text As String, ByVal tr As TableRow, ByVal Css As String)
        Try
            Td.Text = Text
            InnerTdSetting(Td, "", "Left")
            tr.Controls.Add(Td)
            tr.CssClass = Css
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnSendEmail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSendEmail.Click
        Try
            Dim objUpIns As New StandUpInsData.UpdateInsert
            Dim objGetData As New StandGetData.Selectdata
            Dim ds As New DataSet
            Dim strBody As String = String.Empty
            Dim strToName As String = String.Empty
            Dim strEmail As String = String.Empty
            ds = objGetData.GetSponsorUsers(hidMail.Value)
            'strBody = GetEmailBodyData()
            strBody = GetEmailBodyData(ds)
            strToName = ds.Tables(0).Rows(0).Item("FIRSTNAME").ToString() + " " + ds.Tables(0).Rows(0).Item("LASTNAME").ToString()
            SendEmail(strBody, ds.Tables(0).Rows(0).Item("EMAILADDRESS").ToString(), strToName.ToString(), ds)

            GetSupplierGrade()
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('An email has been sent to supplier on your behalf and a copy has also been sent to you.');", True)

            'Started Activity Log Changes
            Try
                objUpIns.InsertLog1(Session("UserId").ToString(), "12", "Clicked on Non Sponsor '" + hidMail.Value + "' link for requesting them for Technical DataSheet", Session("SBACaseId"), Session("LogInCount").ToString(), Session.SessionID, Request.QueryString("MatId").ToString(), "", "", "", "")
            Catch ex As Exception

            End Try
            'Ended Activity Log Changes
        Catch ex As Exception

        End Try
    End Sub
    Protected Function GetEmailBodyData(ByVal dsMail As DataSet) As String
        Dim StrSqlBody As String = ""
        Try

            Dim dsEmail As New DataSet
            Dim objgetData As New UsersGetData.Selectdata
            Dim dsUser As New DataSet
            Dim Supname As String = String.Empty
            Dim objGetData1 As New StandGetData.Selectdata
            dsEmail = objgetData.GetEmailConfigDetails("Y")
            dsUser = objGetData1.GetLogUserDetails(Session("UserId").ToString())

            For i = 0 To dsMail.Tables(0).Rows.Count - 1
                If i = 0 Then
                    If dsMail.Tables(0).Rows(i).Item("FIRSTNAME").ToString() <> "" Then
                        Supname = dsMail.Tables(0).Rows(i).Item("FIRSTNAME").ToString() + " " + dsMail.Tables(0).Rows(i).Item("LASTNAME").ToString()
                    End If

                Else
                    If dsMail.Tables(0).Rows(i).Item("FIRSTNAME").ToString() <> "" Then
                        If i = dsMail.Tables(0).Rows.Count - 1 Then
                            Supname = Supname + " and " + dsMail.Tables(0).Rows(i).Item("FIRSTNAME").ToString() + " " + dsMail.Tables(0).Rows(i).Item("LASTNAME").ToString()
                        Else
                            Supname = Supname + ", " + dsMail.Tables(0).Rows(i).Item("FIRSTNAME").ToString() + " " + dsMail.Tables(0).Rows(i).Item("LASTNAME").ToString()
                        End If

                    End If
                End If
            Next

            StrSqlBody = "<div style='font-family:Verdana;'>  "

            StrSqlBody = StrSqlBody + "<div style='margin-top:10px;margin-right:10px;font-family:Verdana;font-size:15px;'> "
            StrSqlBody = StrSqlBody + "<p>Hi " + " " + Supname.ToString() + ",<br/><br/>I am contacting you to request that you add your technical data sheets to the SavvyPack<sup>®</sup> Structure Assistant. I would appreciate having access to the technical data sheets of " + dsMail.Tables(0).Rows(0).Item("NAME").ToString() + " products when I am using this tool.<br/><br/>   Please contact SavvyPack Corporation at 952-405-7500 or sales@savvypack.com to make arrangements.</p>"

            StrSqlBody = StrSqlBody + "<p>Thank you for your consideration.</p>"
            StrSqlBody = StrSqlBody + "<p>" + dsUser.Tables(0).Rows(0).Item("FIRSTNAME").ToString() + " " + dsUser.Tables(0).Rows(0).Item("LASTNAME").ToString() + "</p>"
            StrSqlBody = StrSqlBody + "<p>" + dsUser.Tables(0).Rows(0).Item("COMPANY").ToString() + ".</p>"
            StrSqlBody = StrSqlBody + "</div> "

            Return StrSqlBody
        Catch ex As Exception
            Return StrSqlBody
        End Try
    End Function

    Public Sub SendEmail(ByVal strBody As String, ByVal EmailAdd As String, ByVal FirstName As String, ByVal dsMail As DataSet)
        Try
            Dim objGetData As New UsersGetData.Selectdata
            Dim objGetData1 As New StandGetData.Selectdata
            Dim ds As New DataSet
            Dim dsUser As New DataSet
            Dim supname As String = String.Empty
            Dim username As String = String.Empty

            ds = objGetData.GetAlliedMemberMail("REQDSHEET")
            dsUser = objGetData1.GetLogUserDetails(Session("UserId").ToString())
            username = dsUser.Tables(0).Rows(0).Item("FIRSTNAME").ToString() + " " + dsUser.Tables(0).Rows(0).Item("LASTNAME").ToString()

            If ds.Tables(0).Rows.Count > 0 Then
                Dim i As Integer
                Dim _To As New MailAddressCollection()
                Dim _From As New MailAddress(Session("USERNAME").ToString(), username.ToString())
                Dim _CC As New MailAddressCollection()
                Dim _BCC As New MailAddressCollection()
                Dim Item As MailAddress
                Dim Email As New EmailConfig()
                Dim _Subject As String = ds.Tables(0).Rows(0).Item("SUBJECT").ToString()

                'To's
                For i = 0 To dsMail.Tables(0).Rows.Count - 1
                    supname = dsMail.Tables(0).Rows(i).Item("FIRSTNAME").ToString() + " " + dsMail.Tables(0).Rows(i).Item("LASTNAME").ToString()
                    Item = New MailAddress(dsMail.Tables(0).Rows(i).Item("EMAILADDRESS").ToString(), supname.ToString())
                    _To.Add(Item)
                Next

                'CC's
                Item = New MailAddress(Session("USERNAME").ToString(), username.ToString())
                _CC.Add(Item)

                For i = 1 To 10
                    ' BCC() 's
                    If ds.Tables(0).Rows(0).Item("BCC" + i.ToString()).ToString() <> Nothing Then
                        Item = New MailAddress(ds.Tables(0).Rows(0).Item("BCC" + i.ToString()).ToString(), ds.Tables(0).Rows(0).Item("BCC" + i.ToString() + "NAME").ToString())
                        _BCC.Add(Item)
                    End If
                    If ds.Tables(0).Rows(0).Item("CC" + i.ToString()).ToString() <> Nothing Then
                        Item = New MailAddress(ds.Tables(0).Rows(0).Item("CC" + i.ToString()).ToString(), ds.Tables(0).Rows(0).Item("CC" + i.ToString() + "NAME").ToString())
                        _CC.Add(Item)
                    End If

                Next

                Email.SendMail(_From, _To, _CC, _BCC, strBody, _Subject)
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
