Imports System
Imports System.Data
Imports SurveyBundleGetData
Imports SurveyBundleUpdateData
Imports System.Net
Imports System.Net.Mail
Partial Class Pages_Survey_ViewSurveyBundle
    Inherits System.Web.UI.Page
    Dim SurveyId As String
    Dim sName As String
    Dim User As String = ""
    Public AnsID(50) As String
    Public UserId As String
    Public Display As String
    Public BlastId As String
    Public SurveyBundleID As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ObjGetData As New SurveyBundleGetData
        Dim obj As New CryptoHelper
        Try
            Response.Expires = 0
            Response.Cache.SetNoStore()
            Response.AppendHeader("Pragma", "no-cache")
            SurveyBundleID = obj.Decrypt(Request.QueryString.Item("SurveyBundleId").ToString())
            SurveyId = ObjGetData.GetSurveyIDBySurveyBundle(SurveyBundleID.ToString())
            User = obj.Decrypt(Request.QueryString.Item("User").ToString())


            If User = "User" Then
                UserId = obj.Decrypt(Request.QueryString.Item("UserId").ToString())
                BlastId = obj.Decrypt(Request.QueryString.Item("BlastId").ToString())
            ElseIf User = "Admin" Then
                Display = obj.Decrypt(Request.QueryString.Item("Display").ToString())
                If Display = "Answer" Then
                    UserId = obj.Decrypt(Request.QueryString.Item("UserId").ToString())
                    BlastId = obj.Decrypt(Request.QueryString.Item("BlastId").ToString())
                End If
            End If


            hidRowNum.Value = "5"
            If Not IsPostBack Then
                GetSurveyName()
                If User = "User" Then
                    GetUserDetails()
                Else
                   GetAdminDetails()
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetSurveyName()
        Dim ds As New DataSet
        Dim objGetData As New SurveyBundleGetData
        Try
            ds = objGetData.GetSurveyName(SurveyId)
            lblSName.Text = ds.Tables(0).Rows(0).Item("NAME").ToString()
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
            Td.Style.Add("font-size", "15px")
            Td.Style.Add("font-family", "Optima")
        Catch ex As Exception
            lblError.Text = "Error:InnerTdSetting:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetUserDetails()
        Try
            Dim objGet As New SurveyBundleGetData
            Dim dsQues As New DataSet
            Dim dsAns As New DataSet
            Dim dtAns As DataTable
            Dim dvAns As DataView
            Dim tbl As New Table
            Dim lblAns As Label
            Dim lblQues As Label
            Dim tdId As New TableCell
            Dim trRdb As New TableRow
            Dim trComment As New TableRow
            Dim trAns As New TableRow
            Dim tdRdb As New TableCell
            Dim tdComment As New TableCell
            Dim tdAns As New TableCell
            Dim tdQues As New TableCell
            Dim trQues As New TableRow
            Dim rdb As New RadioButton
            Dim txtComment As New TextBox
            Dim trSpace As New TableRow
            Dim trSpace2 As New TableRow
            Dim tblAns As Table
            Dim tblRdb As Table
            Dim tblComment As Table
            Dim trRow As TableRow
            Dim trRdbRow As TableRow
            Dim trCom As TableRow
            Dim tdCol As TableCell
            Dim tdRdbCol As TableCell
            Dim tdCom As TableCell
            Dim Count As Integer
            Dim hidQuesID As HiddenField
            Dim dsSAns As New DataSet
            Dim dvSAns As New DataView
            Dim dtSAns As New DataTable
            Dim dsUser As New DataSet
            Dim img As New Image
            tblSurvey.Rows.Clear()
            Dim QuesId As String = objGet.GetQuestionIds(SurveyId)

            If QuesId <> "" Then
                dsQues = objGet.GetQuestions(SurveyId)
                dsAns = objGet.GetAnswers(QuesId)
                dsSAns = objGet.GetSurveyAnswers(UserId, SurveyBundleID, BlastId)
                dvAns = dsAns.Tables(0).DefaultView()

                Count = Convert.ToInt16(hidRowNum.Value.ToString())
                Session("QuesCount") = dsQues.Tables(0).Rows.Count - 1

                If dsSAns.Tables(0).Rows.Count > 0 Then
                    btnsend.Visible = False
                    lblMsg.Visible = True
                Else
                    'trUser.Visible = True
                    dsUser = objGet.GetUserName(UserId)
                    lblUName.Text = dsUser.Tables(0).Rows(0).Item("USERNAME").ToString()
                    btnsend.Visible = True

                    For i = 0 To dsQues.Tables(0).Rows.Count - 1
                        trQues = New TableRow
                        tdId = New TableCell
                        tdQues = New TableCell
                        lblQues = New Label

                        For b = 1 To 2
                            Select Case b
                                Case 1
                                    InnerTdSetting(tdId, "100", "Center")
                                    tdId.Text = "<b>" + (i + 1).ToString() + ". </b>"
                                Case 2
                                    hidQuesID = New HiddenField
                                    hidQuesID.ID = "hidQuesID" + i.ToString()

                                    InnerTdSetting(tdQues, "500", "Left")
                                    lblQues.Text = dsQues.Tables(0).Rows(i).Item("QUESDESC").ToString()
                                    hidQuesID.Value = dsQues.Tables(0).Rows(i).Item("QUESTIONID").ToString()
                                    'lblQues.CssClass = "BorderAns"
                                    tdQues.Controls.Add(lblQues)
                                    tdQues.Controls.Add(hidQuesID)
                            End Select
                        Next

                        trQues.Controls.Add(tdId)
                        trQues.Controls.Add(tdQues)
                        tblSurvey.Controls.Add(trQues)

                        dvSAns = dsSAns.Tables(0).DefaultView()
                        dvAns.RowFilter = "QUESTIONID = " + dsQues.Tables(0).Rows(i).Item("QUESTIONID").ToString()
                        dtAns = dvAns.ToTable()

                        dvSAns.RowFilter = "QUESTIONID = " + dsQues.Tables(0).Rows(i).Item("QUESTIONID").ToString()
                        dtSAns = dvSAns.ToTable()

                        If dtAns.Rows.Count > 0 Then
                            For s = 1 To 2
                                Select Case s
                                    Case 1
                                        trRdb = New TableRow
                                        For w = 1 To 2
                                            tdRdb = New TableCell
                                            Select Case w
                                                Case 1
                                                    InnerTdSetting(tdRdb, "100", "Center")
                                                    trRdb.Controls.Add(tdRdb)
                                                Case 2
                                                    tblRdb = New Table
                                                    For f = 0 To dtAns.Rows.Count - 1
                                                        trRdbRow = New TableRow
                                                        If dtAns.Rows(f).Item("ANSDESC").ToString() <> "" Then
                                                            tdRdbCol = New TableCell
                                                            rdb = New RadioButton
                                                            rdb.ID = dtAns.Rows(f).Item("ANSWERID").ToString()
                                                            rdb.GroupName = "Answer" + i.ToString()
                                                            rdb.Width = 400
                                                            InnerTdSetting(tdRdb, "500", "left")
                                                            rdb.Text = dtAns.Rows(f).Item("ANSDESC").ToString()
                                                            rdb.Style.Add("font-weight", "bold")
                                                            If dtSAns.Rows.Count > 0 Then
                                                                If dtSAns.Rows(0).Item("ANSWERID").ToString() = dtAns.Rows(f).Item("ANSWERID").ToString() Then
                                                                    rdb.Checked = True
                                                                End If
                                                            End If
                                                            If User = "Admin" Then
                                                                tdRdbCol.Enabled = False
                                                            End If
                                                            tdRdbCol.Controls.Add(rdb)
                                                            trRdbRow.Controls.Add(tdRdbCol)
                                                        End If
                                                        tblRdb.Controls.Add(trRdbRow)
                                                    Next
                                                    tdRdb.Controls.Add(tblRdb)
                                                    trRdb.Controls.Add(tdRdb)
                                            End Select
                                        Next
                                    Case 2
                                        If dsQues.Tables(0).Rows(i).Item("FLAG").ToString() = "Y" Then
                                            trComment = New TableRow
                                            For w = 1 To 2
                                                tdComment = New TableCell
                                                Select Case w
                                                    Case 1
                                                        InnerTdSetting(tdComment, "100", "Center")
                                                        tdComment.Text = "<b> Comment: </b>"
                                                        trComment.Controls.Add(tdComment)
                                                    Case 2
                                                        tblComment = New Table
                                                        trCom = New TableRow
                                                        tdCom = New TableCell
                                                        txtComment = New TextBox
                                                        txtComment.ID = "txtComment" + (i + 1).ToString()
                                                        txtComment.Width = 350
                                                        txtComment.Height = 80
                                                        txtComment.MaxLength = 5
                                                        If dtSAns.Rows.Count > 0 Then
                                                            txtComment.Text = dtSAns.Rows(0).Item("COMMENTDESC").ToString()
                                                        End If
                                                        If User = "Admin" Then
                                                            txtComment.Enabled = False
                                                        End If
                                                        txtComment.TextMode = TextBoxMode.MultiLine
                                                        InnerTdSetting(tdCom, "500", "center")
                                                        tdCom.Controls.Add(txtComment)
                                                        trCom.Controls.Add(tdCom)
                                                        txtComment.Attributes.Add("onchange", "javascript:Count(this);")
                                                        tblComment.Controls.Add(trCom)
                                                        tdComment.Controls.Add(tblComment)
                                                        trComment.Controls.Add(tdComment)
                                                End Select
                                            Next
                                        Else
                                            trComment = New TableRow
                                            tdComment = New TableCell
                                            tdComment.Text = ""
                                            trComment.Controls.Add(tdComment)
                                        End If
                                End Select
                            Next
                            tblSurvey.Controls.Add(trAns)
                            tblSurvey.Controls.Add(trRdb)
                            tblSurvey.Controls.Add(trComment)
                        Else
                            trComment = New TableRow
                            For w = 1 To 2
                                tdComment = New TableCell
                                Select Case w
                                    Case 1
                                        InnerTdSetting(tdComment, "", "Center")
                                        tdComment.Text = "<b> Comment </b>"
                                        trComment.Controls.Add(tdComment)
                                    Case 2
                                        tblComment = New Table
                                        txtComment = New TextBox
                                        trCom = New TableRow
                                        tdCom = New TableCell
                                        txtComment.ID = "txtComment" + (i + 1).ToString()
                                        txtComment.Width = 350
                                        txtComment.Height = 80
                                        txtComment.TextMode = TextBoxMode.MultiLine
                                        InnerTdSetting(tdCom, "50px", "center")
                                        tdCom.Controls.Add(txtComment)
                                        trCom.Controls.Add(tdCom)

                                        tblComment.Controls.Add(trCom)
                                        tdComment.Controls.Add(tblComment)
                                        trComment.Controls.Add(tdComment)
                                End Select
                            Next
                        End If
                        tblSurvey.Controls.Add(trComment)

                        img = New Image
                        tdCol = New TableCell

                        trRow = New TableRow
                        img.ImageUrl = "../../Images/horizontalline.png"
                        img.Width = 550
                        img.Height = 5
                        tdCol.ColumnSpan = 2
                        tdCol.Controls.Add(img)
                        trRow.Controls.Add(tdCol)
                        tblSurvey.Controls.Add(trRow)
                    Next
                End If
            Else
                lblNoQuestion.Text = "No Questions added for this survey"
            End If
        Catch ex As Exception
            lblError.Text = "Error:GetUserDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetAdminDetails()
        Try
            Dim objGet As New SurveyBundleGetData
            Dim dsQues As New DataSet
            Dim dsAns As New DataSet
            Dim dtAns As DataTable
            Dim dvAns As DataView
            Dim tbl As New Table
            Dim lblAns As Label
            Dim lblQues As Label
            Dim tdId As New TableCell
            Dim trRdb As New TableRow
            Dim trComment As New TableRow
            Dim trAns As New TableRow
            Dim tdRdb As New TableCell
            Dim tdComment As New TableCell
            Dim tdAns As New TableCell
            Dim tdQues As New TableCell
            Dim trQues As New TableRow
            Dim rdb As New RadioButton
            Dim txtComment As New TextBox
            Dim trSpace As New TableRow
            Dim trSpace2 As New TableRow
            Dim tblAns As Table
            Dim tblRdb As Table
            Dim tblComment As Table
            Dim trRow As TableRow
            Dim trRdbRow As TableRow
            Dim trCom As TableRow
            Dim tdCol As TableCell
            Dim tdRdbCol As TableCell
            Dim tdCom As TableCell
            Dim Count As Integer
            Dim hidQuesID As HiddenField
            Dim dsSAns As New DataSet
            Dim dvSAns As New DataView
            Dim dtSAns As New DataTable
            Dim dsUser As New DataSet
            Dim img As New Image
            tblSurvey.Rows.Clear()
            Dim QuesId As String = objGet.GetQuestionIds(SurveyId)

            If QuesId <> "" Then
                dsQues = objGet.GetQuestions(SurveyId)
                dsAns = objGet.GetAnswers(QuesId)
                'dsSAns = objGet.GetSurveyAnswers(UserId, SurveyId, BlastId)
                If Display <> "View" Then
                    dsSAns = objGet.GetSurveyAnswers(UserId, SurveyBundleID, BlastId)
                End If
                dvAns = dsAns.Tables(0).DefaultView()

                Count = Convert.ToInt16(hidRowNum.Value.ToString())
                Session("QuesCount") = dsQues.Tables(0).Rows.Count - 1

                btnsend.Visible = False
                For i = 0 To dsQues.Tables(0).Rows.Count - 1
                    trQues = New TableRow
                    tdId = New TableCell
                    tdQues = New TableCell
                    lblQues = New Label

                    For b = 1 To 2
                        Select Case b
                            Case 1
                                InnerTdSetting(tdId, "100", "Center")
                                tdId.Text = "<b>" + (i + 1).ToString() + ". </b>"
                            Case 2
                                hidQuesID = New HiddenField
                                hidQuesID.ID = "hidQuesID" + i.ToString()

                                InnerTdSetting(tdQues, "500", "Left")
                                lblQues.Text = dsQues.Tables(0).Rows(i).Item("QUESDESC").ToString()
                                hidQuesID.Value = dsQues.Tables(0).Rows(i).Item("QUESTIONID").ToString()
                                'lblQues.CssClass = "BorderAns"
                                tdQues.Controls.Add(lblQues)
                                tdQues.Controls.Add(hidQuesID)
                        End Select
                    Next

                    trQues.Controls.Add(tdId)
                    trQues.Controls.Add(tdQues)
                    tblSurvey.Controls.Add(trQues)

                    dvAns.RowFilter = "QUESTIONID = " + dsQues.Tables(0).Rows(i).Item("QUESTIONID").ToString()
                    dtAns = dvAns.ToTable()

                    If Display <> "View" Then
                        dvSAns = dsSAns.Tables(0).DefaultView()
                        dvSAns.RowFilter = "QUESTIONID = " + dsQues.Tables(0).Rows(i).Item("QUESTIONID").ToString()
                        dtSAns = dvSAns.ToTable()
                    End If

                    If dtAns.Rows.Count > 0 Then
                        For s = 1 To 2
                            Select Case s
                                Case 1
                                    trRdb = New TableRow
                                    For w = 1 To 2
                                        tdRdb = New TableCell
                                        Select Case w
                                            Case 1
                                                InnerTdSetting(tdRdb, "100", "Center")
                                                trRdb.Controls.Add(tdRdb)
                                            Case 2
                                                tblRdb = New Table
                                                For f = 0 To dtAns.Rows.Count - 1
                                                    trRdbRow = New TableRow
                                                    If dtAns.Rows(f).Item("ANSDESC").ToString() <> "" Then
                                                        tdRdbCol = New TableCell
                                                        rdb = New RadioButton
                                                        rdb.ID = dtAns.Rows(f).Item("ANSWERID").ToString()
                                                        rdb.GroupName = "Answer" + i.ToString()
                                                        rdb.Width = 400
                                                        InnerTdSetting(tdRdb, "500", "left")
                                                        rdb.Text = dtAns.Rows(f).Item("ANSDESC").ToString()
                                                        rdb.Style.Add("font-weight", "bold")
                                                        If Display <> "View" Then
                                                            If dtSAns.Rows.Count > 0 Then
                                                                If dtSAns.Rows(0).Item("ANSWERID").ToString() = dtAns.Rows(f).Item("ANSWERID").ToString() Then
                                                                    rdb.Checked = True
                                                                End If
                                                            End If
                                                        End If

                                                        If User = "Admin" Then
                                                            tdRdbCol.Enabled = False
                                                        End If
                                                        tdRdbCol.Controls.Add(rdb)
                                                        trRdbRow.Controls.Add(tdRdbCol)
                                                    End If
                                                    tblRdb.Controls.Add(trRdbRow)
                                                Next
                                                tdRdb.Controls.Add(tblRdb)
                                                trRdb.Controls.Add(tdRdb)
                                        End Select
                                    Next
                                Case 2
                                    If dsQues.Tables(0).Rows(i).Item("FLAG").ToString() = "Y" Then
                                        trComment = New TableRow
                                        For w = 1 To 2
                                            tdComment = New TableCell
                                            Select Case w
                                                Case 1
                                                    InnerTdSetting(tdComment, "100", "Center")
                                                    tdComment.Text = "<b> Comment: </b>"
                                                    trComment.Controls.Add(tdComment)
                                                Case 2
                                                    tblComment = New Table
                                                    trCom = New TableRow
                                                    tdCom = New TableCell
                                                    txtComment = New TextBox
                                                    txtComment.ID = "txtComment" + (i + 1).ToString()
                                                    txtComment.Width = 350
                                                    txtComment.Height = 80
                                                    If dtSAns.Rows.Count > 0 Then
                                                        If dtSAns.Rows.Count > 0 Then
                                                            txtComment.Text = dtSAns.Rows(0).Item("COMMENTDESC").ToString()
                                                        End If
                                                    End If
                                                    If User = "Admin" Then
                                                        txtComment.Enabled = False
                                                    End If
                                                    txtComment.TextMode = TextBoxMode.MultiLine
                                                    InnerTdSetting(tdCom, "500", "center")
                                                    tdCom.Controls.Add(txtComment)
                                                    trCom.Controls.Add(tdCom)
                                                    txtComment.Attributes.Add("onchange", "javascript:Count(this);")
                                                    tblComment.Controls.Add(trCom)
                                                    tdComment.Controls.Add(tblComment)
                                                    trComment.Controls.Add(tdComment)
                                            End Select
                                        Next
                                    Else
                                        trComment = New TableRow
                                        tdComment = New TableCell
                                        tdComment.Text = ""
                                        trComment.Controls.Add(tdComment)
                                    End If
                            End Select
                        Next
                        tblSurvey.Controls.Add(trAns)
                        tblSurvey.Controls.Add(trRdb)
                        tblSurvey.Controls.Add(trComment)
                    Else
                        trComment = New TableRow
                        For w = 1 To 2
                            tdComment = New TableCell
                            Select Case w
                                Case 1
                                    InnerTdSetting(tdComment, "", "Center")
                                    tdComment.Text = "<b> Comment </b>"
                                    trComment.Controls.Add(tdComment)
                                Case 2
                                    tblComment = New Table
                                    txtComment = New TextBox
                                    trCom = New TableRow
                                    tdCom = New TableCell
                                    txtComment.ID = "txtComment" + (i + 1).ToString()
                                    txtComment.Width = 350
                                    txtComment.Height = 80
                                    If User = "Admin" Then
                                        txtComment.Enabled = False
                                    End If
                                    txtComment.TextMode = TextBoxMode.MultiLine
                                    InnerTdSetting(tdCom, "50px", "center")
                                    tdCom.Controls.Add(txtComment)
                                    trCom.Controls.Add(tdCom)

                                    tblComment.Controls.Add(trCom)
                                    tdComment.Controls.Add(tblComment)
                                    trComment.Controls.Add(tdComment)
                            End Select
                        Next
                    End If
                    tblSurvey.Controls.Add(trComment)

                    img = New Image
                    tdCol = New TableCell

                    trRow = New TableRow
                    img.ImageUrl = "../../Images/horizontalline.png"
                    img.Width = 550
                    img.Height = 5
                    tdCol.ColumnSpan = 2
                    tdCol.Controls.Add(img)
                    trRow.Controls.Add(tdCol)
                    tblSurvey.Controls.Add(trRow)
                Next
            Else
                lblNoQuestion.Text = "No Questions added for this survey"
            End If
        Catch ex As Exception
            lblError.Text = "Error:GetAdminDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnsend_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsend.Click
        Dim QFCount As Integer
        Dim objGet As New SurveyBundleGetData()
        Dim objUpIns As New SurveyBundleUpdateData()
        Dim objGetData As New UsersGetData.Selectdata()
        Dim strBody As String = String.Empty
        Dim strLink As String = String.Empty
        Dim dsSurveyBundle, dsEmail, dsUser As New DataSet()
        Try
            QFCount = Convert.ToInt32(Session("QuesCount"))
            Dim FAnsID(QFCount) As String
            Dim ans(QFCount) As String
            Dim comAns(QFCount) As String
            Dim Ques(QFCount) As String

            For i = 0 To QFCount
                Ques(i) = Request.Form("hidQuesID" + i.ToString() + "")
                ans(i) = Request.Form("Answer" + i.ToString() + "")
                comAns(i) = Request.Form("txtComment" + (i + 1).ToString() + "")
            Next
            'SurveyBundle Chnages
            objUpIns.AddSurveyAnswer(UserId, SurveyId, Ques, ans, comAns, QFCount, BlastId, SurveyBundleID)
            objUpIns.AddResponseDate(UserId, SurveyBundleID, BlastId)
            'End SBManager Changes

            'Send Email to allied contact
            dsEmail = objGetData.GetEmailConfigDetails("Y")
            dsUser = objGet.GetUserName(UserId)
            dsSurveyBundle = objGet.GetSurveyBundleName(SurveyBundleID)

            If dsEmail.Tables(0).Rows.Count > 0 Then
                strLink = dsEmail.Tables(0).Rows(0).Item("URL").ToString()
                strBody = GetEmailBodyDataNotify(strLink, dsUser.Tables(0).Rows(0).Item("USERNAME").ToString(), dsSurveyBundle.Tables(0).Rows(0).Item("SURVEYBUNDLENAME").ToString())
                SendEmailNotify(strBody, "USRSR")
            End If
            'End
            GetUserDetails()
        Catch ex As Exception
            lblError.Text = "Error:btnsend_Click:" + ex.Message.ToString()
        End Try
    End Sub

#Region "Email"

    Protected Function GetEmailBodyDataNotify(ByVal link As String, ByVal Username As String, ByVal SurveyBundNm As String) As String
        Dim StrSqlBdy As String = ""
        Dim strAction As String = ""
        Try
            StrSqlBdy = "<div style='font-family:Verdana;'>  "
            StrSqlBdy = StrSqlBdy + "<table cellpadding='0' cellspacing='0' style='width:650px'> "
            StrSqlBdy = StrSqlBdy + "<tr> "
            StrSqlBdy = StrSqlBdy + "<td><img src='" + link + "/Images/SavvyPackLogo3.gif' /> "
            StrSqlBdy = StrSqlBdy + "<br /> "
            StrSqlBdy = StrSqlBdy + "</td> "
            StrSqlBdy = StrSqlBdy + "</tr> "
            StrSqlBdy = StrSqlBdy + "<tr style='background-color:#336699;height:35px;'> "
            StrSqlBdy = StrSqlBdy + "<td> "
            StrSqlBdy = StrSqlBdy + "<div style='color:white;font-size:18px;font-family:Verdana;font-weight:bold;margin-left:5px;'><b>This User has responded on Survey</b> </div> "
            StrSqlBdy = StrSqlBdy + "</td> "
            StrSqlBdy = StrSqlBdy + "</tr> "
            StrSqlBdy = StrSqlBdy + "</table> "
            StrSqlBdy = StrSqlBdy + "<div style='margin-top:10px;margin-right:10px;font-family:Verdana;font-size:12px;'> "
            StrSqlBdy = StrSqlBdy + "<table style='font-family:Verdana;width:650px;font-size:12px;border-collapse:collapse' border='1' bordercolor='black' cellpadding='0' cellspacing='0'><tr style='font-weight:bold;text-align:center'><td>User Name</td><td>SurveyBundle Name</td></tr> "
            StrSqlBdy = StrSqlBdy + "<tr><td>" + Username + "</td><td>" + SurveyBundNm + "</td></tr> "
            StrSqlBdy = StrSqlBdy + "</table> "
            StrSqlBdy = StrSqlBdy + "<br /> "
            StrSqlBdy = StrSqlBdy + "<p> "
            StrSqlBdy = StrSqlBdy + "<div style='font-family:Verdana;font-size:12px;'> "
            StrSqlBdy = StrSqlBdy + "SavvyPack Corporation<br/>1850 East 121st Street Suite 106B<br/>Burnsville, MN USA 55337<br/><a style='font-family:verdana;' href='http://www.savvypack.com'>www.savvypack.com</a><br/>Phone: 1-952-405-7500</div> "
            StrSqlBdy = StrSqlBdy + "</p> "
            StrSqlBdy = StrSqlBdy + "</div> "

            Return StrSqlBdy
        Catch ex As Exception
            Return StrSqlBdy
        End Try
    End Function

    Public Sub SendEmailNotify(ByVal strBody As String, ByVal code As String)
        Try
            Dim objGetData As New UsersGetData.Selectdata
            Dim ds As New DataSet
            ds = objGetData.GetAlliedMemberMail(code)
            If ds.Tables(0).Rows.Count > 0 Then
                Dim i As Integer
                Dim _To As New MailAddressCollection()
                Dim _From As New MailAddress(ds.Tables(0).Rows(0).Item("FROMADD").ToString(), ds.Tables(0).Rows(0).Item("FROMNAME").ToString())
                Dim _CC As New MailAddressCollection()
                Dim _BCC As New MailAddressCollection()
                Dim Item As MailAddress
                Dim Email As New EmailConfig()
                Dim _Subject As String = ds.Tables(0).Rows(0).Item("SUBJECT").ToString()

                'To's
                Item = New MailAddress(ds.Tables(0).Rows(0).Item("TOADD").ToString(), ds.Tables(0).Rows(0).Item("TONAME").ToString())
                _To.Add(Item)

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

#End Region

End Class
