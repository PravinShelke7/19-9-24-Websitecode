Imports System
Imports System.Data
Imports SavvyGetData
Imports SavvyUpInsData
Imports System.Net.Mail

Partial Class Pages_PopUp_ViewFiles
    Inherits System.Web.UI.Page
    Dim objUpIns As New UpdateInsert()
    Dim ProjectId As String = ""
    Dim UserId As String = ""
    Dim SCount As String = ""
    Dim strUsername As String = "Raxapack"
    Dim strPassword As String = "raxa@123"
    Dim strComputer As String = "192.168.20.103\Raxa_Backup\AnkitaKutal"
    Dim pageid As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim obj As New CryptoHelper
            If Session("SBack") = Nothing Then

                Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If

            If Not IsPostBack Then
                'Started Activity Log Changes
                Try
                    'objUpIns.InsertLog1(Session("UserId").ToString(), "ViewFile.aspx", "Opened Popup to View Uploaded File", Session("ProjectId"), Session("SPROJLogInCount").ToString())
                    objUpIns.InsertLog2(Session("UserId").ToString(), "ViewFile.aspx", "Opened View File Popup for ProjectId:" + Session("ProjectId"), Session("ProjectId"), Session("SPROJLogInCount").ToString(), "", "", "")

                Catch ex As Exception

                End Try
                'Ended Activity Log Changes
                ' ifrDownloadPage.Attributes.Add("src", "http://dataexchange.allied-dev.com/ViewFiles.aspx?ProjectId=" + obj.Encrypt(Session("ProjectId")) + "&UID=" + obj.Encrypt(Session("UserId")) + "&Count=" + obj.Encrypt(Session("SPROJLogInCount").ToString()) + "")
            End If
            ProjectId = Session("ProjectId").ToString()
            UserId = Session("UserId").ToString()
            SCount = Session("SPROJLogInCount").ToString()
            GetPageDetails()
        Catch ex As Exception
            lblError.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub
    Private Sub GetPageDetails()
        Dim ds As New DataSet
        Dim objGetData As New SavvyGetData.Selectdata()
        Dim i As New Integer
        Dim j As New Integer
        Dim trHeader As New TableRow
        Dim trInner As New TableRow
        Dim tdHeader As TableCell
        Dim lbl As New Label
        Dim hid As New HiddenField
        Dim Link As New HyperLink
        Dim txt As New TextBox
        Dim tdInner As TableCell
        Dim lnkBtn As LinkButton
        Try
            ds = objGetData.GetUserFileDetails(ProjectId)
            For i = 1 To 3
                tdHeader = New TableCell
                Dim Title As String = String.Empty

                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "250px", "File Name", "1")
                        trHeader.Controls.Add(tdHeader)

                    Case 2
                        HeaderTdSetting(tdHeader, "100px", "Sheet Type", "1")
                        trHeader.Controls.Add(tdHeader)

                    Case 3

                        HeaderTdSetting(tdHeader, "200px", "Download files", "1")
                        trHeader.Controls.Add(tdHeader)

                End Select

            Next

            trHeader.Height = 30

            tblDwnldList.Controls.Add(trHeader)


            'Inner
            For i = 1 To ds.Tables(0).Rows.Count
                trInner = New TableRow
                For j = 1 To 3
                    tdInner = New TableCell

                    Select Case j
                        Case 1
                            InnerTdSetting(tdInner, "", "Center")
                            lbl = New Label
                            lbl.Text = ds.Tables(0).Rows(i - 1).Item("FILENAME").ToString()
                            lbl.ForeColor = Drawing.Color.Black
                            lbl.Font.Size = 10
                            lbl.Width = 230
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)

                        Case 2
                            InnerTdSetting(tdInner, "", "Center")
                            lbl = New Label
                            lbl.Text = ds.Tables(0).Rows(i - 1).Item("TYPENAME").ToString()
                            lbl.ForeColor = Drawing.Color.Black
                            lbl.Font.Size = 10
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)

                        Case 3
                            InnerTdSetting(tdInner, "", "center")
                            lnkBtn = New LinkButton
                            lnkBtn.ID = "lnkBtn" + i.ToString()
                            lnkBtn.CssClass = "Link"
                            lnkBtn.Text = "Download"
                            lnkBtn.ForeColor = Drawing.Color.Black
                            lnkBtn.ValidationGroup = ds.Tables(0).Rows(i - 1).Item("FILENAME").ToString() + "," + ds.Tables(0).Rows(i - 1).Item("TYPENAME").ToString() + "," + ds.Tables(0).Rows(i - 1).Item("PROJECTFILEID").ToString() + ""
                            AddHandler lnkBtn.Click, AddressOf lnkBtn_Click
                            tdInner.Controls.Add(lnkBtn)
                            trInner.Controls.Add(tdInner)
                    End Select
                Next

                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
                tblDwnldList.Controls.Add(trInner)
            Next
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub lnkBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim repDetails As String = ""
        Dim filename() As String
        Dim DownloadPath As String = String.Empty
        Dim ds As New DataSet
        Dim objGetData As New SavvyGetData.Selectdata()
        Try
            ' Get Email App Data
            Dim logid As String
            Dim dsid As New DataSet
            Dim objGetDataa As New Selectdata()
            Dim str As String = System.IO.Path.GetFileName(Request.CurrentExecutionFilePath).ToString()
            dsid = objGetDataa.GetPageId(str.ToString())
            pageid = dsid.Tables(0).Rows(0).Item("PAGEID").ToString()
            logid = objGetDataa.GetLogid()


            Dim getLinkDetail As LinkButton = CType(sender, LinkButton)
            filename = Regex.Split(getLinkDetail.ValidationGroup, ",")

            DownloadPath = "\\192.168.3.31\SavvyPackRepository\Savvypack\" + "Project" + ProjectId + "_" + filename(0)

            Dim dv As New DataView
            Dim dt As New DataTable
            Dim dsUser As New DataSet

            ds = Session("ProjData")
            dv = ds.Tables(0).DefaultView()
            dv.RowFilter = "PROJECTID=" + ProjectId + ""
            dt = dv.ToTable()


            ' Dim ds As New DataSet
            Dim dsNew As New DataSet
            Dim _To As New MailAddressCollection()
            Dim _CC As New MailAddressCollection()
            Dim _BCC As New MailAddressCollection()
            Dim Item As MailAddress

            Dim _To1 As New MailAddressCollection()
            Dim _CC1 As New MailAddressCollection()
            Dim _BCC1 As New MailAddressCollection()
            Dim Item1 As MailAddress

            Dim Email As New EmailConfig()
            Dim strBody As String = String.Empty

            ds = objGetData.GetMailUserDetails(Session("UserId"))
            'strBody = strBody + "<div style='font-family:Verdana;font-size:12px;margin-top:10px;margin-bottom:10px;'>"
            'strBody = strBody + "<p>SavvyPack® User " + ds.Tables(0).Rows(0).Item("FIRSTNAME").ToString() + " " + ds.Tables(0).Rows(0).Item("LASTNAME").ToString() + " has Downloaded file from ViewFiles Popup of ModelInput Page, </p>"
            'strBody = strBody + "<p>Project Id: " + ProjectId.ToString() + " and Project Title: " + dt.Rows(0).Item("TITLE").ToString().Replace("&#", "'") + " .</p>"
            'strBody = strBody + "<p>Project Owner: " + dt.Rows(0).Item("OWNER").ToString() + " and Uploaded File: " + filename(0) + " .</p>"
            'strBody = strBody + "</div> "


            dsNew = objGetData.GetAlliedMemberMail("UPDWNFILES")
            dsUser = objGetData.GetAnalystUserDetails(Session("SavvyLicenseId"))

            If dsNew.Tables(0).Rows.Count > 0 Then
                Dim _From As New MailAddress(dsNew.Tables(0).Rows(0).Item("FROMADD").ToString(), dsNew.Tables(0).Rows(0).Item("FROMNAME").ToString())
                Dim _Subject As String = dsNew.Tables(0).Rows(0).Item("SUBJECT").ToString()
                'To's

                Item = New MailAddress(ds.Tables(0).Rows(0).Item("EMAILADDRESS").ToString(), ds.Tables(0).Rows(0).Item("FIRSTNAME").ToString() + " " + ds.Tables(0).Rows(0).Item("LASTNAME").ToString())
                _To.Add(Item)


                'BCC's

                'For i = 1 To 10
                '    ' BCC() 's
                '    If dsNew.Tables(0).Rows(0).Item("BCC" + i.ToString()).ToString() <> Nothing Then
                '        Item = New MailAddress(dsNew.Tables(0).Rows(0).Item("BCC" + i.ToString()).ToString(), dsNew.Tables(0).Rows(0).Item("BCC" + i.ToString() + "NAME").ToString())
                '        _BCC.Add(Item)
                '    End If
                '    If dsNew.Tables(0).Rows(0).Item("CC" + i.ToString()).ToString() <> Nothing Then
                '        Item = New MailAddress(dsNew.Tables(0).Rows(0).Item("CC" + i.ToString()).ToString(), dsNew.Tables(0).Rows(0).Item("CC" + i.ToString() + "NAME").ToString())
                '        _CC.Add(Item)
                '    End If

                'Next

                'Email.SendMail(_From, _To, _CC, _BCC, strBody, _Subject)

                strBody = String.Empty
                strBody = strBody + "<div style='font-family:Verdana;font-size:12px;margin-top:10px;margin-bottom:10px;'>"
                strBody = strBody + "<p>SavvyPack® User " + ds.Tables(0).Rows(0).Item("FIRSTNAME").ToString() + " " + ds.Tables(0).Rows(0).Item("LASTNAME").ToString() + " has downloaded file from ViewFiles Popup of ModelInput Page, </p>"
                strBody = strBody + "<p>Project Id: " + ProjectId.ToString() + " and Project Title: " + dt.Rows(0).Item("TITLE").ToString().Replace("&#", "'") + " .</p>"
                strBody = strBody + "<p>Project Owner: " + dt.Rows(0).Item("OWNER").ToString() + " and Downloaded File: " + filename(0) + " .</p>"
                strBody = strBody + "</div> "


                Dim _From1 As New MailAddress(dsNew.Tables(0).Rows(0).Item("FROMADD").ToString(), dsNew.Tables(0).Rows(0).Item("FROMNAME").ToString())
                Dim _Subject1 As String = dsNew.Tables(0).Rows(0).Item("SUBJECT").ToString()
                'To's
                Item = New MailAddress(dsNew.Tables(0).Rows(0).Item("TOADD").ToString(), dsNew.Tables(0).Rows(0).Item("TONAME").ToString())

                _To1.Add(Item)

                'BCC's

                For i = 1 To 10
                    ' BCC() 's
                    If dsNew.Tables(0).Rows(0).Item("BCC" + i.ToString()).ToString() <> Nothing Then
                        Item = New MailAddress(dsNew.Tables(0).Rows(0).Item("BCC" + i.ToString()).ToString(), dsNew.Tables(0).Rows(0).Item("BCC" + i.ToString() + "NAME").ToString())
                        _BCC1.Add(Item)
                    End If
                    If dsNew.Tables(0).Rows(0).Item("CC" + i.ToString()).ToString() <> Nothing Then
                        Item = New MailAddress(dsNew.Tables(0).Rows(0).Item("CC" + i.ToString()).ToString(), dsNew.Tables(0).Rows(0).Item("CC" + i.ToString() + "NAME").ToString())
                        _CC1.Add(Item)
                    End If

                Next

                'Email.SendMail(_From1, _To1, _CC1, _BCC1, strBody, _Subject1)
                objUpIns.InsertEmailStore(_To1.ToString(), logid.ToString(), dsNew.Tables(0).Rows(0).Item("CODE").ToString(), strBody, Session("UserId"), "3", pageid, "File Downloaded", dt.Rows(0).Item("PROJECTID").ToString())

            End If
            'Started Activity Log Changes
            Try
                ' objUpIns.InsertLog2(UserId, "FileUpload.aspx", "File Download for Project:" + ProjectId + " and File Name: " + filename(1) + "", ProjectId, SCount, "Download", "", filename(2), "1")
                objUpIns.InsertLog2(UserId, "ViewFiles.aspx", "File Downloaded for ProjectId:" + ProjectId + " and File Name: " + filename(0) + "", ProjectId, Session("SPROJLogInCount").ToString(), "Download", filename(1), filename(2))

            Catch ex As Exception

            End Try
            'Ended Activity Log Changes


            Response.ContentType = "application/octet-stream"
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + filename(0))

            Response.TransmitFile(DownloadPath)
            Response.End()


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
            lblError.Text = "Error:HeaderTdSetting:" + ex.Message.ToString()
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
            lblError.Text = "Error:HeaderTdSetting:" + ex.Message.ToString()
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
        Catch ex As Exception
            lblError.Text = "Error:InnerTdSetting:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub TextBoxSetting(ByVal txt As TextBox, ByVal Css As String)
        Try
            txt.CssClass = Css

        Catch ex As Exception
            lblError.Text = "Error:TextBoxSetting:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub LableSetting(ByVal lbl As Label, ByVal Css As String)
        Try
            lbl.CssClass = Css

        Catch ex As Exception
            lblError.Text = "Error:LableSetting:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub LeftTdSetting(ByVal Td As TableCell, ByVal Text As String, ByVal tr As TableRow, ByVal Css As String)
        Try
            Td.Text = Text
            InnerTdSetting(Td, "", "Left")
            tr.Controls.Add(Td)
            tr.CssClass = Css
        Catch ex As Exception
            lblError.Text = "Error:LeftTdSetting:" + ex.Message.ToString()
        End Try
    End Sub

End Class
