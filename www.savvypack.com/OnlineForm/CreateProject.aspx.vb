Imports System.Data
Imports System.Data.OleDb
Imports System
Imports SavvyGetData
Imports SavvyUpInsData
Imports AjaxControlToolkit
Imports System.Net.Mail
Imports System.Net
Imports System.Diagnostics

Partial Class OnlineForm_Popup_ProjectSummary1
    Inherits System.Web.UI.Page
    Dim objUpIns As New UpdateInsert()
    Dim ProjectId As String = ""
    Dim FileType As String = ""
    Dim UserId As String = ""
    Dim SCount As String = ""
    Dim pageid As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
		Session("SeqCnt") = "1"
                Session("ProjId") = Nothing
                Popup1.Visible = True
                Popup2.Visible = False
                Popup3.Visible = False
                Popup4.Visible = False
                Popup5.Visible = False
                Popup6.Visible = False
                hidAnalysisId.Value = "0"
                GetQuickPriceDetails()

                Dim obj As New CryptoHelper
                'Started Activity Log Changes
                Try
                    objUpIns.InsertLog1(Session("UserId").ToString(), "CreateProject.aspx", "Opened SavvyPack Project Summary Page to Create New Project", "", Session("SPROJLogInCount").ToString())
                Catch ex As Exception
                End Try
                'Ended Activity Log Changes
            End If
        Catch ex As Exception

        End Try
    End Sub

#Region "PopUp1"

    Protected Sub btnnxtT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnnxtT.Click
        Popup2.Visible = True
        Popup1.Visible = False
        If hidAmalysisDes.Value <> "" Then
            lnkAnalysis.Text = hidAmalysisDes.Value
        End If
        GetTitle()
    End Sub

    Public Sub GetTitle()
        Dim objUpIns As New SavvyUpInsData.UpdateInsert()
        Dim objGetData As New Selectdata()
        Dim ProjId As New Integer
        Dim ds As New DataSet
        Dim dsD As New DataSet
        Dim dsUser As New DataSet
        Dim DsChkProjNm As New DataSet()
        Dim UpdateFlag As Boolean = False
	Dim ht As Hashtable = New Hashtable()
        'Changes made by PK
        'Dim ObjGetEmailData As New UsersGetData.Selectdata()
        'Dim DsEmailConfig As New DataSet()
        'Dim link As String
        'Dim strBodyData As String
        'ENd 
        Try
            If Session("ProjId") = Nothing Then
                ds = objGetData.ExistingProjDetails(Session("UserId"), txtTitle.Text.Replace("'", "''"))
                If ds.Tables(0).Rows.Count = 0 Then
                    ProjId = objUpIns.InsertProjTitle(Session("UserId"), txtTitle.Text.ToString().Replace("'", "''"), "1", hidAnalysisId.Value)
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "RefreshPage", "return RefreshPage();", True)
                    Session("ProjId") = ProjId
                    If ProjId <> 0 Then
                        'Started Activity Log Changes
                        Try
                            objUpIns.InsertLog1(Session("UserId").ToString(), "CreateProject.aspx", "Created new SavvyPack Project ID:" + ProjId.ToString() + " and AnalysisId: " + hidAnalysisId.Value + "", ProjId.ToString(), Session("SPROJLogInCount").ToString())
			    If Session("ht") IsNot Nothing Then
                                ht = DirectCast(Session("ht"), Hashtable)
                            End If
                            Dim str() As String
                            For i = 1 To ht.Count
                                For Each item As Object In ht
                                    str = item.Key.ToString().Split("-")
                                    If str(1) = i Then
                                        objUpIns.EditInsertLog(Session("UserId").ToString(), "2", str(0), "1", item.Value.ToString().Replace("'", "''"), ProjId.ToString(), Session("SPROJLogInCount").ToString())
                                        Exit For
                                    End If
                                Next
                            Next
                            Session("ht") = Nothing
                            Session("SeqCnt") = "1"
                        Catch ex As Exception
                        End Try
                        'Ended Activity Log Changes

                        'Project Creation Email
                        'Try
                        '    DsEmailConfig = ObjGetEmailData.GetEmailConfigDetails("Y")
                        '    If DsEmailConfig.Tables(0).Rows.Count > 0 Then
                        '        link = DsEmailConfig.Tables(0).Rows(0).Item("URL").ToString()
                        '        strBodyData = GetEmailBodyData(link)
                        '        SendEmail(strBodyData)
                        '    End If
                        'Catch ex As Exception
                        'End Try
                        'End
                    End If
                Else
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Project name already exists.\nPlease use another name to avoid conflict.');", True)
                    Popup2.Visible = False
                    Popup1.Visible = True
                End If
            Else
                DsChkProjNm = objGetData.ChkProjectNm(Session("ProjId").ToString(), txtTitle.Text.ToString().Replace("'", "''"))
                If DsChkProjNm.Tables(0).Rows.Count > 0 Then
                    UpdateFlag = True
                Else
                    ds = objGetData.ExistingProjDetails(Session("UserId"), txtTitle.Text.Replace("'", "''"))
                    If ds.Tables(0).Rows.Count = 0 Then
                        UpdateFlag = True
                    Else
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Project name already exists.\nPlease use another name to avoid conflict.');", True)
                        Popup2.Visible = False
                        Popup1.Visible = True
                    End If
                End If

                If UpdateFlag Then
                    objUpIns.UpdateProjectNmType(Session("ProjId").ToString(), txtTitle.Text.ToString().Replace("'", "''"), hidAnalysisId.Value)
                    'Started Activity Log Changes
                    Try
                        objUpIns.InsertLog1(Session("UserId").ToString(), "CreateProject.aspx", "Update Title for the SavvyPack Project ID:" + Session("ProjId").ToString() + " and AnalysisId: " + hidAnalysisId.Value + "", ProjId.ToString(), Session("SPROJLogInCount").ToString())
			If Session("ht") IsNot Nothing Then
                                ht = DirectCast(Session("ht"), Hashtable)
                            End If
                            Dim str() As String
                            For i = 1 To ht.Count
                                For Each item As Object In ht
                                    str = item.Key.ToString().Split("-")
                                    If str(1) = i Then
                                        objUpIns.EditInsertLog(Session("UserId").ToString(), "2", str(0), "1", item.Value.ToString().Replace("'", "''"), Session("ProjId").ToString(), Session("SPROJLogInCount").ToString())
                                        Exit For
                                    End If
                                Next
                            Next
                            Session("ht") = Nothing
                            Session("SeqCnt") = "1"
                    Catch ex As Exception
                    End Try
                    'Ended Activity Log Changes
                End If
            End If

        Catch ex As Exception
            lblError.Text = "Error:GetTitle:" + ex.Message.ToString() + ""
        End Try
    End Sub

#End Region

#Region "PopUp2"

    Protected Sub btnnxtD_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnnxtD.Click
        Dim obj As New CryptoHelper
        Dim ds As New DataSet
        Dim objGetData As New Selectdata
        Dim dvData As New DataView
        Dim dtData As New DataTable
        Try
            GetDescription()
            If hidAnalysisId.Value = "6" Then
                Popup2.Visible = False
                Popup3.Visible = True
                lblQuickPrice.Text = "(3/3)"
            Else
                Popup2.Visible = False
                Popup4.Visible = True
                Uploadfile()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Sub GetDescription()
        Dim objUpIns As New SavvyUpInsData.UpdateInsert()
        Dim objGetData As New Selectdata()
        Dim ProjId As New Integer
        Dim ds As New DataSet
        Dim dsUser As New DataSet
	Dim ht As Hashtable = New Hashtable()
        Try
            ProjId = objUpIns.UpdateProjDetails(Session("UserId"), txtDesc.Text.ToString().Replace("'", "''"), Session("ProjId").ToString())
            'Started Activity Log Changes
            Try
                objUpIns.InsertLog1(Session("UserId").ToString(), "CreateProject.aspx", "Created new SavvyPack Project ID:" + ProjId.ToString() + " and Project Description: " + txtDesc.Text.ToString().Replace("'", "''") + "", ProjId.ToString(), Session("SPROJLogInCount").ToString())
		If Session("ht") IsNot Nothing Then
                                ht = DirectCast(Session("ht"), Hashtable)
                            End If
                            Dim str() As String
                            For i = 1 To ht.Count
                                For Each item As Object In ht
                                    str = item.Key.ToString().Split("-")
                                    If str(1) = i Then
                                        objUpIns.EditInsertLog(Session("UserId").ToString(), "2", str(0), "1", item.Value.ToString().Replace("'", "''"), ProjId.ToString(), Session("SPROJLogInCount").ToString())
                                        Exit For
                                    End If
                                Next
                            Next
                            Session("ht") = Nothing
                            Session("SeqCnt") = "1"
            Catch ex As Exception
            End Try
            'Ended Activity Log Changes
        Catch ex As Exception
            lblError.Text = "Error:GetDescription:" + ex.Message.ToString() + ""
        End Try
    End Sub

#End Region

#Region "PopUp3 FileUpload"

    Protected Sub btnnxtUpld_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnnxtUpld.Click
        Try
            Popup4.Visible = False
            Popup5.Visible = True
        Catch ex As Exception
        End Try
    End Sub

    Public Sub Uploadfile()
        Dim obj As New CryptoHelper
        Dim ProjectId As String = ""
        Dim FileType As String = ""
        Dim UserId As String = ""
        Dim SCount As String = ""
        Dim ProjId As New Integer
        If IsPostBack Then
            'Started Activity Log Changes
            Try
                'objUpIns.InsertLog1(Session("UserId").ToString(), "UploadFiles.aspx", "Opened Popup to View Uploaded File", Session("ProjId"), Session("SPROJLogInCount").ToString())
                objUpIns.InsertLog2(Session("UserId"), "CreateProject.aspx", "Opened Popup to Upload File during creating projectId:" + Session("ProjId").ToString() + "", Session("ProjId").ToString(), Session("SPROJLogInCount").ToString(), "Upload", "", "")
            Catch ex As Exception
            End Try
            'Ended Activity Log Changes
            FileType = "Universal1"
            GetPageDetails()
        End If

    End Sub

    Private Sub GetPageDetails()
        Dim ds As New DataSet
        Dim objGetData As New SavvyGetData.Selectdata
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
        Dim ProjId As New Integer
        Try
            ds = objGetData.GetUserFileDetails(Session("ProjId"))
            'File Overwrite changes
            Session("CPExistingFile") = objGetData.GetAllFileNameByProjID(Session("ProjId"))
            'End
            tblDwnldList.Rows.Clear()
            tdHeader = New TableCell
            Dim Title As String = String.Empty

            HeaderTdSetting(tdHeader, "250px", "File Name", "1")
            trHeader.Controls.Add(tdHeader)
            trHeader.Height = 30
            tblDwnldList.Controls.Add(trHeader)

            'Inner
            For i = 1 To ds.Tables(0).Rows.Count

                trInner = New TableRow
                For j = 1 To 1
                    tdInner = New TableCell

                    Select Case j
                        Case 1
                            tdInner = New TableCell

                            InnerTdSetting(tdInner, "", "Center")
                            lbl = New Label
                            lbl.Text = ds.Tables(0).Rows(i - 1).Item("FILENAME").ToString()
                            lbl.ForeColor = Drawing.Color.Black
                            lbl.Font.Size = 10
                            lbl.Style.Add("word-wrap", "break-word")
                            lbl.Width = 230
                            tdInner.Controls.Add(lbl)
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
        Try
            Dim getLinkDetail As LinkButton = CType(sender, LinkButton)
            filename = Regex.Split(getLinkDetail.ValidationGroup, ",")

            DownloadPath = "\\192.168.3.31\SavvyPackRepository\Savvypack\" + "Project" + Session("ProjId").ToString() + "_" + filename(0)
            'DownloadPath = "D:\SavvyPackRepository\Savvypack\" + "Project" + Session("ProjId").ToString() + "_" + filename(0)
            Response.ContentType = "application/octet-stream"
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + filename(0))
            Response.TransmitFile(DownloadPath)
            Response.End()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Upload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Dim objUpdateData As New UpdateInsert
        Dim ds As New DataSet
        Dim objGetData As New SavvyGetData.Selectdata()
        Dim PFileId As New Integer
        Dim FileType = "Universal1"
        Dim FileAction As String = String.Empty
        Try

            Dim filepath As String = String.Empty
            If fuSheet.FileName <> "" Then
                filepath = fileUpload()
                If fuSheet.PostedFile.ContentLength > 1074000000 Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('File size exceeds maximum limit of 1 GB');", True)
                Else
                    PFileId = objUpdateData.AddFileDetails_new(Session("ProjId").ToString(), fuSheet.FileName, filepath, FileType, Session("UserId"), "1", hidFileAction.Value)
                    'Page.ClientScript.RegisterStartupScript(Me.GetType(), "JS", "alert('File Uploaded Successfully.');", True)
                    'Started Activity Log Changes
                    Try
                        If hidFileAction.Value = "Update" Then
                            FileAction = "Overwritten"
                        Else
                            FileAction = "Uploaded"
                        End If
                        objUpIns.InsertLog2(Session("UserId"), "CreateProject.aspx", "File " + FileAction.ToString() + " during creating project for ProjectId:" + Session("ProjId").ToString() + " and File Name: " + fuSheet.FileName + "", Session("ProjId").ToString(), Session("SPROJLogInCount").ToString(), "Upload", FileType, PFileId)
                        'objUpIns.InsertLog1(Session("UserId"), "FileUpload.aspx", "File Uploaded during creating project for ProjectId:" + Session("ProjId").ToString() + " and File Name: " + fuSheet.FileName + "", Session("ProjId").ToString(), Session("SPROJLogInCount").ToString())
                    Catch ex As Exception
                    End Try
                    'Ended Activity Log Changes
                End If
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "JS", "alert('Please select file to upload.');", True)
            End If
            'Issue Fixed By Pk
            GetPageDetails()
            'End
        Catch ex As Exception

        End Try

    End Sub

    Private Function fileUpload() As String
        Dim Path As String = String.Empty
        Dim UploadPath As String = String.Empty

        Try
            If fuSheet.HasFile Then
                UploadPath = "\\192.168.3.31\SavvyPackRepository\Savvypack\" + "Project" + Session("ProjId").ToString() + "_" + fuSheet.FileName
                'UploadPath = "D:\SavvyPackRepository\Savvypack\" + "Project" + Session("ProjId").ToString() + "_" + fuSheet.FileName
                fuSheet.SaveAs(UploadPath)
                Path = UploadPath
            End If
        Catch ex As Exception
            Response.Write("fileUpload.Error:" + ex.Message.ToString())
        End Try
        Return Path

    End Function

#End Region

#Region "PopUp3 QP"

    Protected Sub btnnxtQP_Click(sender As Object, e As System.EventArgs) Handles btnnxtQP.Click
        Popup3.Visible = False
        Popup4.Visible = False
        Popup5.Visible = False
        Popup6.Visible = True
        GetQuickPrice()
    End Sub

    Protected Sub GetQuickPriceDetails()
        Dim Dts1 As New DataSet
        Dim Dts2 As New DataSet
        Dim Dts3 As New DataSet
        Dim Dts4 As New DataSet
        Dim Dts5 As New DataSet
        Dim Dts6 As New DataSet
        Dim Dts7 As New DataSet
        Dim Dts8 As New DataSet
        Dim Dts9 As New DataSet
        Dim Dts10 As New DataSet
        Dim Dts11 As New DataSet
        Dim Dts12 As New DataSet
        Dim Dts13 As New DataSet
        Dim objGetData As New Selectdata()
        Dim lst As New ListItem
        Dim dts As New DataSet
        Try

            dts = objGetData.GetQuickPriceDetails_UNIT()
            Session("Units") = dts
            LblAQ.Text = dts.Tables(0).Rows(0).Item("UNIT_NAME").ToString()
            LblOS.Text = dts.Tables(0).Rows(0).Item("UNIT_NAME").ToString()
            LblECT.Text = dts.Tables(0).Rows(1).Item("UNIT_NAME").ToString()
            LblmULLENS.Text = dts.Tables(0).Rows(2).Item("UNIT_NAME").ToString()
            LblBcom.Text = dts.Tables(0).Rows(0).Item("UNIT_NAME").ToString()
            LblPC1.Text = dts.Tables(0).Rows(0).Item("UNIT_NAME").ToString()

            Dts1 = objGetData.GetQuickPriceDetails_ECT()

            If Dts1.Tables(0).Rows.Count > 0 Then
                ECT_DDL.DataSource = Dts1
                ECT_DDL.DataValueField = "ECTID"
                ECT_DDL.DataTextField = "ECT_VALUE"
                ECT_DDL.DataBind()
            End If

            Dts2 = objGetData.GetQuickPriceDetails_MULLENS()

            If Dts2.Tables(0).Rows.Count > 0 Then
                MULLEN_DDL.DataSource = Dts2
                MULLEN_DDL.DataValueField = "MULLENID"
                MULLEN_DDL.DataTextField = "MULLEN_VALUE"
                MULLEN_DDL.DataBind()
            End If

            Dts3 = objGetData.GetQuickPriceDetails_PQ()

            If Dts3.Tables(0).Rows.Count > 0 Then
                PQ_DDL.DataSource = Dts3
                PQ_DDL.DataValueField = "PQID"
                PQ_DDL.DataTextField = "PQ_VALUE"
                PQ_DDL.DataBind()
            End If

            Dts4 = objGetData.GetQuickPriceDetails_BCOM()

            If Dts4.Tables(0).Rows.Count > 0 Then
                Bcom_DDL.DataSource = Dts4
                Bcom_DDL.DataValueField = "BCOMID"
                Bcom_DDL.DataTextField = "BCOM_VALUE"
                Bcom_DDL.DataBind()
            End If


            Dts5 = objGetData.GetQuickPriceDetails_FS()

            If Dts5.Tables(0).Rows.Count > 0 Then
                FS_DDL.DataSource = Dts5
                FS_DDL.DataValueField = "FSID"
                FS_DDL.DataTextField = "FS_VALUE"
                FS_DDL.DataBind()
            End If

            Dts6 = objGetData.GetQuickPriceDetails_CONSTSTYLE()

            If Dts6.Tables(0).Rows.Count > 0 Then
                BStyle_DDL.DataSource = Dts6
                BStyle_DDL.DataValueField = "CONTSTYLEID"
                BStyle_DDL.DataTextField = "CONTSTYLE_VALUE"
                BStyle_DDL.DataBind()
            End If

            Dts7 = objGetData.GetQuickPriceDetails_SFORMAT()

            If Dts7.Tables(0).Rows.Count > 0 Then
                SFormat_DDL.DataSource = Dts7
                SFormat_DDL.DataValueField = "SFORMATID"
                SFormat_DDL.DataTextField = "SFORMAT_VALUE"
                SFormat_DDL.DataBind()
            End If

            Dts8 = objGetData.GetQuickPriceDetails_Dimension_Unit()

            If Dts8.Tables(0).Rows.Count > 0 Then
                ddlFlat_BD.DataSource = Dts8
                ddlFlat_BD.DataValueField = "ID"
                ddlFlat_BD.DataTextField = "DIMENSION_UNIT_NAME"
                ddlFlat_BD.DataBind()
            End If

            Dts9 = objGetData.GetQuickPriceDetails_Dimension_Unit()

            If Dts9.Tables(0).Rows.Count > 0 Then
                ddl_COD.DataSource = Dts9
                ddl_COD.DataValueField = "ID"
                ddl_COD.DataTextField = "DIMENSION_UNIT_NAME"
                ddl_COD.DataBind()
            End If

            Dts10 = objGetData.GetQuickPriceDetails_Weight_Unit()

            If Dts10.Tables(0).Rows.Count > 0 Then
                ddlwc_BD.DataSource = Dts10
                ddlwc_BD.DataValueField = "ID"
                ddlwc_BD.DataTextField = "WEIGHT_UNIT_NAME"
                ddlwc_BD.DataBind()
            End If

            Dts11 = objGetData.GetQuickPriceDetails_Weight_Unit()

            If Dts11.Tables(0).Rows.Count > 0 Then
                ddlwpp_BD.DataSource = Dts10
                ddlwpp_BD.DataValueField = "ID"
                ddlwpp_BD.DataTextField = "WEIGHT_UNIT_NAME"
                ddlwpp_BD.DataBind()
            End If

            Dts12 = objGetData.GetQuickPriceDetails_OverallBoardW_Unit()

            If Dts12.Tables(0).Rows.Count > 0 Then
                Bw_BD.DataSource = Dts12
                Bw_BD.DataValueField = "ID"
                Bw_BD.DataTextField = "WEIGHT_UNIT"
                Bw_BD.DataBind()
            End If

            Dts13 = objGetData.GetQuickPriceDetails_Printed()

            If Dts13.Tables(0).Rows.Count > 0 Then
                Printed_DDL.DataSource = Dts13
                Printed_DDL.DataValueField = "PRINTEDID"
                Printed_DDL.DataTextField = "PRINTED_VALUE"
                Printed_DDL.DataBind()
            End If


        Catch ex As Exception
            lblError.Text = "Error:GetPCaseGroupDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Public Sub GetQuickPrice()
        Dim objUpIns As New SavvyUpInsData.UpdateInsert()
        Dim objGetData As New Selectdata()
        Dim ProjId As New Integer
        Dim ds As New DataSet
        Dim dsD As New DataSet
        Dim dsUser As New DataSet
        Dim flag As Boolean
        Dim dts As New DataSet
        Dim nextDate As String = ""
        Dim DsChkProjDate As New DataSet()
        Dim DsChkQPData As New DataSet()
        Dim FlagUpdate As Boolean = False
        Dim QpProjID As New Integer
        Try
            dts = Session("Units")

            If DateTime.Today.DayOfWeek.ToString().ToUpper() = "FRIDAY" Then
                nextDate = DateTime.Today.AddDays(3)
            ElseIf DateTime.Today.DayOfWeek.ToString().ToUpper() = "SATURDAY" Then
                nextDate = DateTime.Today.AddDays(3)
            ElseIf DateTime.Today.DayOfWeek.ToString().ToUpper() = "SUNDAY" Then
                nextDate = DateTime.Today.AddDays(2)
            Else
                nextDate = DateTime.Today.AddDays(1)
            End If

            'BackButton Changes
            DsChkProjDate = objGetData.ChkProjectDate(Session("ProjId").ToString())
            If DsChkProjDate.Tables(0).Rows.Count = 0 Then
                flag = objUpIns.InsertProjectDate(nextDate, "2", Session("ProjId").ToString())
            Else
                flag = objUpIns.UpdateProjectDate(nextDate, "2", Session("ProjId").ToString())
            End If

            DsChkQPData = objGetData.ChkQuickPriceData(Session("ProjId").ToString())
            If DsChkQPData.Tables(0).Rows.Count = 0 Then
                QpProjID = objUpIns.InsertQuickPrice(Session("ProjId"), txtOQ.Text.ToString().Replace("'", "''"), txtOS.Text.ToString().Replace("'", "''"), _
                                                     txtWidth.Text.ToString().Replace("'", "''"), txtHeight.Text.ToString().Replace("'", "''"), ddlFlat_BD.Text, _
                                                     txtWidth_COD.Text.ToString().Replace("'", "''"), txtHeight_COD.Text.ToString().Replace("'", "''"), _
                                                     txtLength_COD.Text.ToString().Replace("'", "''"), ddl_COD.Text, txtWEC.Text.ToString().Replace("'", "''"), _
                                                     txtWPP.Text.ToString().Replace("'", "''"), Printed_DDL.Text, ECT_DDL.Text, MULLEN_DDL.Text, PQ_DDL.Text, _
                                                     txtPC.Text.ToString().Replace("'", "''"), Bcom_DDL.Text, txtBW.Text.ToString().Replace("'", "''"), _
                                                     FS_DDL.Text, BStyle_DDL.Text, SFormat_DDL.Text, ddlwc_BD.Text, ddlwpp_BD.Text, Bw_BD.Text)
            Else
                objUpIns.UpdateQPData(Session("ProjId"), txtOQ.Text.ToString().Replace("'", "''"), txtOS.Text.ToString().Replace("'", "''"), _
                                                     txtWidth.Text.ToString().Replace("'", "''"), txtHeight.Text.ToString().Replace("'", "''"), ddlFlat_BD.Text, _
                                                     txtWidth_COD.Text.ToString().Replace("'", "''"), txtHeight_COD.Text.ToString().Replace("'", "''"), _
                                                     txtLength_COD.Text.ToString().Replace("'", "''"), ddl_COD.Text, txtWEC.Text.ToString().Replace("'", "''"), _
                                                     txtWPP.Text.ToString().Replace("'", "''"), Printed_DDL.Text, ECT_DDL.Text, MULLEN_DDL.Text, PQ_DDL.Text, _
                                                     txtPC.Text.ToString().Replace("'", "''"), Bcom_DDL.Text, txtBW.Text.ToString().Replace("'", "''"), _
                                                     FS_DDL.Text, BStyle_DDL.Text, SFormat_DDL.Text, ddlwc_BD.Text, ddlwpp_BD.Text, Bw_BD.Text)
                FlagUpdate = True
            End If
            'End

            Page.ClientScript.RegisterStartupScript(Me.GetType(), "RefreshPage", "return RefreshPage();", True)
            If QpProjID <> 0 Then
                'Started Activity Log Changes
                Try
                    If FlagUpdate Then
                        objUpIns.InsertLog1(Session("UserId").ToString(), "CreateProject.aspx", "Created new SavvyPack Project ID:" + Session("ProjId").ToString() + " and Updated Quick Price fields.", Session("ProjId").ToString(), Session("SPROJLogInCount").ToString())
                    Else
                        objUpIns.InsertLog1(Session("UserId").ToString(), "CreateProject.aspx", "Created new SavvyPack Project ID:" + Session("ProjId").ToString() + " and Inserted Quick Price fields.", Session("ProjId").ToString(), Session("SPROJLogInCount").ToString())
                    End If
                Catch ex As Exception
                End Try
                'Ended Activity Log Changes
            End If

        Catch ex As Exception
            lblError.Text = "Error:GetQuickPrice:" + ex.Message.ToString() + ""
        End Try
    End Sub

#End Region

#Region "PopUp4"

    Protected Sub btnnxtS_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnnxtS.Click
        'Started Activity Log Changes
        Try
            UpdateDate()
            objUpIns.InsertLog1(Session("UserId").ToString(), "CreateProject.aspx", "Opened Date selection Popup for Project ID:" + Session("ProjId").ToString() + "", Session("ProjId").ToString(), Session("SPROJLogInCount").ToString())
        Catch ex As Exception
        End Try
        'Ended Activity Log Changes
    End Sub

    Public Sub UpdateDate()
        Dim count As Integer = Convert.ToInt32(Session("count"))
        Dim objGetData As New Selectdata()
        Dim Value(count) As String
        Dim DateId(count) As String
        Dim dsData As New DataSet
        Dim dvData As New DataView
        Dim dsDate As New DataView
        Dim dtData As New DataTable
        Dim ProjId As New Integer
        Dim flag As Boolean
        Dim Val As Integer = 0
        Dim DsChkProjDate As New DataSet()
        Try
            If txtDate.Text <> "" Then
                'BackButton Changes
                DsChkProjDate = objGetData.ChkProjectDate(Session("ProjId").ToString())
                If DsChkProjDate.Tables(0).Rows.Count = 0 Then
                    flag = objUpIns.InsertProjectDate(txtDate.Text, "2", Session("ProjId").ToString())
                Else
                    flag = objUpIns.UpdateProjectDate(txtDate.Text, "2", Session("ProjId").ToString())
                End If
            Else
                Popup5.Visible = False
                Popup6.Visible = True
            End If
            If flag = True Then
                Val = 1
            End If
            If Val = 1 Then
                Popup5.Visible = False
                Popup6.Visible = True
            End If

            'Started Activity Log Changes
            Try
                objUpIns.InsertLog1(Session("UserId").ToString(), "CreateProject.aspx", "Created new SavvyPack Project ID:" + Session("ProjId").ToString() + " and Desire Complete Date: " + txtDate.Text + "", Session("ProjId").ToString(), Session("SPROJLogInCount").ToString())
            Catch ex As Exception
            End Try
            'Ended Activity Log Changes
        Catch ex As Exception
            lblError.Text = "Error:UpdateDate:" + ex.Message.ToString() + ""
        End Try
    End Sub

#End Region

#Region "PopUp5"

    Protected Sub submit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles submit.Click
        Dim count As Integer = Convert.ToInt32(Session("count"))
        Dim objGetData As New Selectdata()
        Dim ProjId As New Integer
        Dim ds As New DataSet
        Dim dv As New DataView
        Dim dt As New DataTable
        Dim dsUser As New DataSet
        Dim dsF As New DataSet
        Try
            Dim logid As String
            Dim dsid As New DataSet
            Dim str As String = System.IO.Path.GetFileName(Request.CurrentExecutionFilePath).ToString()
            dsid = objGetData.GetPageId(str.ToString())
            pageid = dsid.Tables(0).Rows(0).Item("PAGEID").ToString()
            logid = objGetData.GetLogid()


            objUpIns.UpdateProjectStatus("2", Session("ProjId").ToString(), Session("UserId").ToString())
            objUpIns.UpdateSubmitDate("1", Session("ProjId").ToString())
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename1", "alert('Thank you for submitting a project with SavvyPack. One of our analysts will review your submission and contact you soon.');", True)
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClosePage1", "ClosePage();", True)

            Dim strchk As String = String.Empty

            dsF = objGetData.GetFileName(Session("UserId"), Session("ProjId").ToString())
            Session("count") = dsF.Tables(0).Rows.Count - 1
            For i = 0 To dsF.Tables(0).Rows.Count - 1
                strchk = strchk + dsF.Tables(0).Rows(i).Item("FILENAME").ToString() + " , "
            Next
            If strchk <> "" Then
                strchk = strchk.Remove(strchk.Length - 2)
            Else
                strchk = "None"
            End If


            ds = objGetData.GetProjDetails(Session("UserId"), txtTitle.Text.Replace("'", "''"))
            ' ds = objGetData.GetExistProjectDetails(Session("ProjId").ToString())
            dv = ds.Tables(0).DefaultView()
            dv.RowFilter = "PROJECTID=" + Session("ProjId").ToString() + ""
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

            strBody = strBody + "<div style='font-family:Verdana;font-size:12px;margin-top:10px;margin-bottom:10px;'>"
            strBody = strBody + "<p>Dear " + ds.Tables(0).Rows(0).Item("FIRSTNAME").ToString() + " " + ds.Tables(0).Rows(0).Item("LASTNAME").ToString() + "</p>"
            strBody = strBody + "<p>Thank you for choosing SavvyPack Corporation. </p>"
            strBody = strBody + "<p>We have received your submission for the following project: " + dt.Rows(0).Item("TITLE").ToString().Replace("&#", "'") + "  . One of our analysts will contact you shortly. </p>"
            strBody = strBody + "<p>If you have any questions, please feel free to contact us at <a href='mailto:support@savvypack.com'>support@savvypack.com</a> </p>"
            strBody = strBody + "</div> "

            dsNew = objGetData.GetAlliedMemberMail("PROJSUB")
            dsUser = objGetData.GetAnalystUserDetails(Session("SavvyLicenseId"))

            If dsNew.Tables(0).Rows.Count > 0 Then
                Dim _From As New MailAddress(dsNew.Tables(0).Rows(0).Item("FROMADD").ToString(), dsNew.Tables(0).Rows(0).Item("FROMNAME").ToString())
                Dim _Subject As String = dsNew.Tables(0).Rows(0).Item("SUBJECT").ToString()
                'To's

                Item = New MailAddress(ds.Tables(0).Rows(0).Item("EMAILADDRESS").ToString(), ds.Tables(0).Rows(0).Item("FIRSTNAME").ToString() + " " + ds.Tables(0).Rows(0).Item("LASTNAME").ToString())


                _To.Add(Item)


                'BCC's

                For i = 1 To 10
                    ' BCC() 's
                    If dsNew.Tables(0).Rows(0).Item("BCC" + i.ToString()).ToString() <> Nothing Then
                        Item = New MailAddress(dsNew.Tables(0).Rows(0).Item("BCC" + i.ToString()).ToString(), dsNew.Tables(0).Rows(0).Item("BCC" + i.ToString() + "NAME").ToString())
                        _BCC.Add(Item)
                    End If
                    If dsNew.Tables(0).Rows(0).Item("CC" + i.ToString()).ToString() <> Nothing Then
                        Item = New MailAddress(dsNew.Tables(0).Rows(0).Item("CC" + i.ToString()).ToString(), dsNew.Tables(0).Rows(0).Item("CC" + i.ToString() + "NAME").ToString())
                        _CC.Add(Item)
                    End If

                Next

                Email.SendMail(_From, _To, _CC, _BCC, strBody, _Subject)
                objUpIns.InsertEmailStore(_To.ToString(), logid.ToString(), dsNew.Tables(0).Rows(0).Item("CODE").ToString(), strBody, Session("UserId"), "2", pageid.ToString(), "project created", dt.Rows(0).Item("PROJECTID").ToString())


                strBody = String.Empty
                strBody = strBody + "<div style='font-family:Verdana;font-size:12px;margin-top:10px;margin-bottom:10px;'>"
                strBody = strBody + "<p>SavvyPack® User " + ds.Tables(0).Rows(0).Item("FIRSTNAME").ToString() + " " + ds.Tables(0).Rows(0).Item("LASTNAME").ToString() + " has submitted Project, </p>"
                strBody = strBody + "<p>Project Id: " + Session("ProjId").ToString() + " and Project Title: " + dt.Rows(0).Item("TITLE").ToString().Replace("&#", "'") + " .</p>"
                strBody = strBody + "<p>Project Owner: " + dt.Rows(0).Item("OWNER").ToString() + " and Uploaded File: " + strchk + " .</p>"
                strBody = strBody + "</div> "

                Dim _From1 As New MailAddress(dsNew.Tables(0).Rows(0).Item("FROMADD").ToString(), dsNew.Tables(0).Rows(0).Item("FROMNAME").ToString())
                Dim _Subject1 As String = dsNew.Tables(0).Rows(0).Item("SUBJECT").ToString()
                'To's
                'Item = New MailAddress(dsUser.Tables(0).Rows(0).Item("USERNAME").ToString(), dsUser.Tables(0).Rows(0).Item("FIRSTNAME").ToString() + " " + dsUser.Tables(0).Rows(0).Item("LASTNAME").ToString())
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
                objUpIns.InsertEmailStore(_To1.ToString(), logid.ToString(), dsNew.Tables(0).Rows(0).Item("CODE").ToString(), strBody, Session("UserId"), "3", pageid, "project submitted", dt.Rows(0).Item("PROJECTID").ToString())

                Email.SendMail(_From1, _To1, _CC1, _BCC1, strBody, _Subject1)
            End If
            'Started Activity Log Changes
            Try
                objUpIns.InsertLog1(Session("UserId").ToString(), "CreateProject.aspx", "Submitted Project for Project Id:" + Session("ProjId").ToString() + "", Session("ProjId").ToString(), Session("SPROJLogInCount").ToString())
            Catch ex As Exception

            End Try
            'Ended Activity Log Changes

        Catch ex As Exception
            lblError.Text = "Error:btnUpdate_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

#End Region

#Region "Control Formatting"

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

#End Region

#Region "Back Button"
    Protected Sub btnBack1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack1.Click
        Try
            Popup2.Visible = False
            Popup1.Visible = True
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnBack2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack2.Click
        Try
            Popup4.Visible = False
            Popup2.Visible = True
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnback2_QP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnback2_QP.Click
        Try
            Popup3.Visible = False
            Popup2.Visible = True
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnBack3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack3.Click
        Try
            Popup5.Visible = False
            Popup4.Visible = True
            Uploadfile()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnBack4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack4.Click
        Try
            If hidAnalysisId.Value = "6" Then
                Popup6.Visible = False
                Popup3.Visible = True
                lblQuickPrice.Text = "(3/3)"
            Else
                Popup6.Visible = False
                Popup5.Visible = True
            End If
        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "Send Mail"

    'Protected Function GetEmailBodyData(ByVal link As String) As String
    '    Dim StrSql As String = ""
    '    Dim Ds As New DataSet()
    '    'Temp Sol
    '    Dim Dv As New DataView()
    '    Dim Dt As New DataTable()
    '    Dim DsUserInfo As New DataSet()
    '    'End Temp Sol
    '    Dim objGetData As New Selectdata()
    '    Try
    '        ''Get UserData
    '        'Ds = objGetData.GetUserDatabyProjID(Session("ProjId").ToString())
    '        ''ENd
    '        'StrSql = "<div style='font-family:Verdana;'>  "
    '        'StrSql = StrSql & "<table cellpadding='0' cellspacing='0' style='width:650px'> "
    '        'StrSql = StrSql & "<tr> "
    '        'StrSql = StrSql & "<td><img src='" + link + "/Images/SavvyPackLogo3.gif' /> "
    '        'StrSql = StrSql & "<br /> "
    '        'StrSql = StrSql & "</td> "
    '        'StrSql = StrSql & "</tr> "
    '        'StrSql = StrSql & "</table> "
    '        'StrSql = StrSql & "<div style='margin-top:10px;margin-right:10px;font-family:Verdana;font-size:12px;'> "
    '        'StrSql = StrSql + "<p>SavvyPack® User " + Ds.Tables(0).Rows(0).Item("FIRSTNAME").ToString() + " " + Ds.Tables(0).Rows(0).Item("LASTNAME").ToString() + " has Created Project. </p>"
    '        'StrSql = StrSql + "<p>Project Id: " + Session("ProjId").ToString() + " and Project Title: " + Ds.Tables(0).Rows(0).Item("TITLE").ToString() + " .</p>"
    '        'StrSql = StrSql + "<p>Project Owner: " + Ds.Tables(0).Rows(0).Item("OWNER").ToString() + "</p>"
    '        'StrSql = StrSql & "</div> "

    '        Ds = objGetData.GetProjDetails(Session("UserId"), txtTitle.Text.Replace("'", "''"))
    '        Dv = Ds.Tables(0).DefaultView()
    '        Dv.RowFilter = "PROJECTID=" + Session("ProjId").ToString() + ""
    '        Dt = Dv.ToTable()

    '        DsUserInfo = objGetData.GetMailUserDetails(Session("UserId"))

    '        StrSql = "<div style='font-family:Verdana;'>  "
    '        StrSql = StrSql & "<table cellpadding='0' cellspacing='0' style='width:650px'> "
    '        StrSql = StrSql & "<tr> "
    '        StrSql = StrSql & "<td><img src='" + link + "/Images/SavvyPackLogo3.gif' /> "
    '        StrSql = StrSql & "<br /> "
    '        StrSql = StrSql & "</td> "
    '        StrSql = StrSql & "</tr> "
    '        StrSql = StrSql & "</table> "
    '        StrSql = StrSql & "<div style='margin-top:10px;margin-right:10px;font-family:Verdana;font-size:12px;'> "
    '        StrSql = StrSql + "<p>SavvyPack® User " + DsUserInfo.Tables(0).Rows(0).Item("FIRSTNAME").ToString() + " " + DsUserInfo.Tables(0).Rows(0).Item("LASTNAME").ToString() + " has Created Project. </p>"
    '        StrSql = StrSql + "<p>Project Id: " + Session("ProjId").ToString() + " and Project Title: " + Dt.Rows(0).Item("TITLE").ToString() + ".</p>"
    '        StrSql = StrSql + "<p>Project Owner: " + Dt.Rows(0).Item("OWNER").ToString() + "</p>"
    '        StrSql = StrSql & "</div> "

    '        Return StrSql
    '    Catch ex As Exception
    '        Return StrSql
    '    End Try
    'End Function

    'Protected Function GetEmailBodyData(ByVal link As String) As String
    '    Dim StrSql As String = ""
    '    Dim Ds As New DataSet()
    '    Dim objGetData As New Selectdata()
    '    Try
    '        'Get UserData
    '        Ds = objGetData.GetUserDatabyProjID(Session("ProjId").ToString())
    '        'ENd
    '        StrSql = "<div style='font-family:Verdana;'>  "
    '        StrSql = StrSql & "<table cellpadding='0' cellspacing='0' style='width:650px'> "
    '        StrSql = StrSql & "<tr> "
    '        StrSql = StrSql & "<td><img src='" + link + "/Images/SavvyPackLogo3.gif' /> "
    '        StrSql = StrSql & "<br /> "
    '        StrSql = StrSql & "</td> "
    '        StrSql = StrSql & "</tr> "
    '        StrSql = StrSql & "</table> "
    '        StrSql = StrSql & "<div style='margin-top:10px;margin-right:10px;font-family:Verdana;font-size:12px;'> "
    '        StrSql = StrSql + "<p>SavvyPack® User " + Ds.Tables(0).Rows(0).Item("FIRSTNAME").ToString() + " " + Ds.Tables(0).Rows(0).Item("LASTNAME").ToString() + " has Created Project. </p>"
    '        StrSql = StrSql + "<p>Project Id: " + Session("ProjId").ToString() + " and Project Title: " + Ds.Tables(0).Rows(0).Item("TITLE").ToString() + " .</p>"
    '        StrSql = StrSql + "<p>Project Owner: " + Ds.Tables(0).Rows(0).Item("OWNER").ToString() + "</p>"
    '        StrSql = StrSql & "</div> "

    '        Return StrSql
    '    Catch ex As Exception
    '        Return StrSql
    '    End Try
    'End Function

    'Public Sub SendEmail(ByVal strBody As String)
    '    Try
    '        Dim objGetData As New UsersGetData.Selectdata
    '        Dim ds As New DataSet
    '        ds = objGetData.GetAlliedMemberMail("PROJSUB")
    '        If ds.Tables(0).Rows.Count > 0 Then
    '            Dim i As Integer
    '            Dim _To As New MailAddressCollection()
    '            Dim _From As New MailAddress(ds.Tables(0).Rows(0).Item("FROMADD").ToString(), ds.Tables(0).Rows(0).Item("FROMNAME").ToString())
    '            Dim _CC As New MailAddressCollection()
    '            Dim _BCC As New MailAddressCollection()
    '            Dim Item As MailAddress
    '            Dim Email As New EmailConfig()
    '            Dim _Subject As String = "SavvyPack Project Creation"

    '            'To's
    '            'Item = New MailAddress("pkandoi@allied-dev.com", "Prashant")
    '            Item = New MailAddress(ds.Tables(0).Rows(0).Item("TOADD").ToString(), ds.Tables(0).Rows(0).Item("TONAME").ToString())
    '            _To.Add(Item)

    '            For i = 1 To 10
    '                ' BCC() 's
    '                If ds.Tables(0).Rows(0).Item("BCC" + i.ToString()).ToString() <> Nothing Then
    '                    Item = New MailAddress(ds.Tables(0).Rows(0).Item("BCC" + i.ToString()).ToString(), ds.Tables(0).Rows(0).Item("BCC" + i.ToString() + "NAME").ToString())
    '                    _BCC.Add(Item)
    '                End If
    '                If ds.Tables(0).Rows(0).Item("CC" + i.ToString()).ToString() <> Nothing Then
    '                    Item = New MailAddress(ds.Tables(0).Rows(0).Item("CC" + i.ToString()).ToString(), ds.Tables(0).Rows(0).Item("CC" + i.ToString() + "NAME").ToString())
    '                    _CC.Add(Item)
    '                End If
    '            Next
    '            Email.SendMail(_From, _To, _CC, _BCC, strBody, _Subject)
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

#End Region

<System.Web.Services.WebMethod()>
    Public Shared Function UpdateCase(ByVal Header As String, ByVal text As String) As String
        Try
            Dim ht As Hashtable = New Hashtable()
            Dim str() As String
            Dim seq As Integer = Convert.ToInt32(HttpContext.Current.Session("SeqCnt"))
            Dim flag As Boolean = False
            If HttpContext.Current.Session("ht") IsNot Nothing Then
                ht = DirectCast(HttpContext.Current.Session("ht"), Hashtable)
            End If
            For Each item As Object In ht
                str = item.Key.ToString().Split("-")
                If str(0) = Header Then
                    ht.Remove(item.Key)
                    flag = True
                    Exit For
                End If
            Next

            If flag Then
                ht.Add(Header + "-" + str(1).ToString(), text)
            Else
                ht.Add(Header + "-" + seq.ToString(), text)
                seq += 1
                HttpContext.Current.Session("SeqCnt") = seq
            End If

            HttpContext.Current.Session("ht") = ht

            Dim str1 As String = HttpContext.Current.Session("UserId").ToString()
            str1 = str1 + "Bhavesh"
            Return str1

        Catch ex As Exception

        End Try
    End Function

End Class
