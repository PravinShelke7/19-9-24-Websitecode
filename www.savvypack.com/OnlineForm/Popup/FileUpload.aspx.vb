Imports System
Imports System.Data
Imports System.IO
Imports SavvyGetData
Imports SavvyUpInsData
Imports System.Diagnostics
Imports System.Security.Principal
Imports System.Net.Mail

Partial Class Pages_PopUp_FileUpload
    Inherits System.Web.UI.Page
    Dim objUpIns As New UpdateInsert()
    Dim ProjectId As String = ""
    Dim FileType As String = ""
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

                hidProjectId.Value = Request.QueryString("ProjectId").ToString()

                hidType.Value = Request.QueryString("Type").ToString()
                'Started Activity Log Changes
                Try
                    'objUpIns.InsertLog1(Session("UserId").ToString(), "FileUpload.aspx", "Opened File Upload Popup for Project:" + Session("ProjectId") + "", Session("ProjectId"), Session("SPROJLogInCount").ToString())
                    objUpIns.InsertLog2(Session("UserId").ToString(), "FileUpload.aspx", "Opened File Upload Popup for ProjectId:" + Session("ProjectId"), Session("ProjectId"), Session("SPROJLogInCount").ToString(), "", "", "")

                Catch ex As Exception

                End Try
                'Ended Activity Log Changes
                ' ifrDownloadPage.Attributes.Add("src", "http://dataexchange.allied-dev.com/UserUploadFiles.aspx?ProjectId=" + obj.Encrypt(Request.QueryString("ProjectId").ToString()) + "&FileType=" + obj.Encrypt(Request.QueryString("Type").ToString()) + "&UID=" + obj.Encrypt(Session("UserId")) + "&Count=" + obj.Encrypt(Session("SPROJLogInCount").ToString()) + "")
            End If
            ProjectId = hidProjectId.Value
            UserId = Session("UserId").ToString()
            FileType = Request.QueryString("Type").ToString()
            SCount = Session("SPROJLogInCount").ToString()
            BindExistingFileData()
        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub BindExistingFileData()
        Dim objGetData As New SavvyGetData.Selectdata()
        Try
            'File Overwrite changes
            Session("FUExistingFile") = objGetData.GetAllFileNameByProjID(ProjectId)
            'End
        Catch ex As Exception
            _lErrorLble.Text = "Error:BindExistingFileData:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub Upload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Dim objUpdateData As New UpdateInsert
        Dim ds As New DataSet
        Dim objGetData As New SavvyGetData.Selectdata()
        Dim PFileId As New Integer
        Dim FileAction As String = String.Empty
        Try

            Dim filepath As String = String.Empty
            If fuSheet.FileName <> "" Then
                filepath = fileUpload()
                If fuSheet.PostedFile.ContentLength > 1074000000 Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('File size exceeds maximum limit of 1 GB');", True)
                Else
                    PFileId = objUpdateData.AddFileDetails_new(ProjectId, fuSheet.FileName, filepath, FileType, UserId, "1", hidFileAction.Value)
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "JS", "alert('File Uploaded Successfully.');", True)
                    BindExistingFileData()
                    'Started Activity Log Changes
                    Try
                        If hidFileAction.Value = "Update" Then
                            FileAction = "Overwritten"
                        Else
                            FileAction = "Uploaded"
                        End If
                        objUpIns.InsertLog2(UserId, "FileUpload.aspx", "File " + FileAction.ToString() + " for ProjectId:" + ProjectId + " and File Name: " + fuSheet.FileName + "", ProjectId, SCount, "Upload", FileType, PFileId)
                        'objUpIns.InsertLog2(UserId, "FileUpload.aspx", "File Upload for Project:" + ProjectId + " and File Name: " + fuSheet.FileName + "", ProjectId, SCount, "Upload", FileType, PFileId, "1")
                    Catch ex As Exception
                    End Try
                    'Ended Activity Log Changes
                    SendMail()
                End If
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "JS", "alert('Please select file to upload.');", True)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function fileUpload() As String
        Dim Path As String = String.Empty
        Dim UploadPath As String = String.Empty

        Try
            If fuSheet.HasFile Then
                UploadPath = "\\192.168.3.31\SavvyPackRepository\Savvypack\" + "Project" + ProjectId + "_" + fuSheet.FileName
                'UploadPath = "D:\SavvyPackRepository\Savvypack\" + "Project" + ProjectId + "_" + fuSheet.FileName
                fuSheet.SaveAs(UploadPath)
                Path = UploadPath
            End If

        Catch ex As Exception
            Response.Write("fileUpload.Error:" + ex.Message.ToString())
        End Try
        Return Path
    End Function

    Protected Sub SendMail()
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
            Dim FileAction As String = String.Empty

            ds = objGetData.GetMailUserDetails(Session("UserId"))

            'strBody = strBody + "<div style='font-family:Verdana;font-size:12px;margin-top:10px;margin-bottom:10px;'>"
            'If FileType = "Structure" Then
            '    strBody = strBody + "<p>SavvyPack® User " + ds.Tables(0).Rows(0).Item("FIRSTNAME").ToString() + " " + ds.Tables(0).Rows(0).Item("LASTNAME").ToString() + " has Uploaded file from FileUpload Popup of StructureDetails Page, </p>"
            'Else
            '    strBody = strBody + "<p>SavvyPack® User " + ds.Tables(0).Rows(0).Item("FIRSTNAME").ToString() + " " + ds.Tables(0).Rows(0).Item("LASTNAME").ToString() + " has Uploaded file from FileUpload Popup of ModelInput Page , </p>"
            'End If
            'strBody = strBody + "<p>Project Id: " + ProjectId.ToString() + " and Project Title: " + dt.Rows(0).Item("TITLE").ToString().Replace("&#", "'") + " .</p>"
            'strBody = strBody + "<p>Project Owner: " + dt.Rows(0).Item("OWNER").ToString() + " and Uploaded File: " + fuSheet.FileName + " .</p>"
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

                If hidFileAction.Value = "Update" Then
                    FileAction = "Overwritten"
                Else
                    FileAction = "Uploaded"
                End If

                If FileType = "Structure" Then
                    strBody = strBody + "<p>SavvyPack® User " + ds.Tables(0).Rows(0).Item("FIRSTNAME").ToString() + " " + ds.Tables(0).Rows(0).Item("LASTNAME").ToString() + " has " + FileAction.ToString() + " file from FileUpload Popup of StructureDetails Page, </p>"
                ElseIf FileType = "Formula" Then
                    strBody = strBody + "<p>SavvyPack® User " + ds.Tables(0).Rows(0).Item("FIRSTNAME").ToString() + " " + ds.Tables(0).Rows(0).Item("LASTNAME").ToString() + " has " + FileAction.ToString() + " file from FileUpload Popup of FormulaDetails Page, </p>"
                Else
                    strBody = strBody + "<p>SavvyPack® User " + ds.Tables(0).Rows(0).Item("FIRSTNAME").ToString() + " " + ds.Tables(0).Rows(0).Item("LASTNAME").ToString() + " has " + FileAction.ToString() + " file from FileUpload Popup of ModelInput Page , </p>"
                End If
                strBody = strBody + "<p>Project Id: " + ProjectId.ToString() + " and Project Title: " + dt.Rows(0).Item("TITLE").ToString().Replace("&#", "'") + " .</p>"
                strBody = strBody + "<p>Project Owner: " + dt.Rows(0).Item("OWNER").ToString() + " and Uploaded File: " + fuSheet.FileName + " .</p>"
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
                objUpIns.InsertEmailStore(_To1.ToString(), logid.ToString(), dsNew.Tables(0).Rows(0).Item("CODE").ToString(), strBody, Session("UserId"), "3", pageid, "FILE UPLOADED", dt.Rows(0).Item("PROJECTID").ToString())

                'Email.SendMail(_From1, _To1, _CC1, _BCC1, strBody, _Subject1)
            End If
        Catch ex As Exception
        End Try
    End Sub

End Class
