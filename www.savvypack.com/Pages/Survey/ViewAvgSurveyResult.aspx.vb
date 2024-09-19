Imports System
Imports System.Data
Imports SurveyBundleGetData
Imports SurveyBundleUpdateData
Partial Class Pages_Survey_ViewAvgSurveyResult
    Inherits System.Web.UI.Page
    Dim SurveyId As String
    Dim sName As String
    Dim User As String = String.Empty
    Public AnsID(50) As String
    Public UserId As String
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

            hidRowNum.Value = "5"
            If Not IsPostBack Then
                GetSurveyName()
                GetAdminAvgAnswer()
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

    Protected Sub GetAdminAvgAnswer()
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
            Dim avg As Label
            Dim AnsDes As Label
            Dim txtComment As New TextBox
            Dim trSpace As New TableRow
            Dim trSpace2 As New TableRow

            Dim tblAns As Table
            Dim tblRdb As Table
            Dim tblComment As Table
            Dim tblRes As Table

            Dim trRow As TableRow
            Dim trRdbRow As TableRow
            Dim trCom As TableRow
            Dim trTresponse As New TableRow
            Dim trRes As TableRow

            Dim tdCol As TableCell
            Dim tdAnsDesCol As TableCell
            Dim tdTResponse As TableCell
            Dim tdAvglbl As TableCell
            Dim tdCom As TableCell
            Dim tdRes As TableCell

            Dim Count As Integer
            Dim hidQuesID As HiddenField
            Dim dsSAns As New DataSet
            Dim dvSAns As New DataView
            Dim dtSAns As New DataTable
            Dim dsUser As New DataSet

            Dim DsCustCnt As New DataSet
            Dim DvCustCnt As New DataView
            Dim DtCustCnt As New DataTable

            Dim DsAnsCnt As New DataSet
            Dim DvAnsCnt As New DataView
            Dim DtAnsCnt As New DataTable

            Dim TResponse As Label

            Dim img As New Image
            tblSurvey.Rows.Clear()
            Dim QuesId As String = objGet.GetQuestionIds(SurveyId)

            If QuesId <> "" Then
                dsQues = objGet.GetQuestions(SurveyId)
                dsAns = objGet.GetAnswers(QuesId)
                dvAns = dsAns.Tables(0).DefaultView()

                'Get CustomerCount by QuestionID
                DsCustCnt = objGet.GetCustCnt(SurveyBundleID, QuesId)
                DvCustCnt = DsCustCnt.Tables(0).DefaultView()
                'End

                'Get Sum of AnsID by QuestionID
                DsAnsCnt = objGet.GetAnsCnt(SurveyBundleID)
                DvAnsCnt = DsAnsCnt.Tables(0).DefaultView()
                'End

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
                                tdId.Style.Add("font-size", "14px")
                                tdId.Style.Add("color", "#17202A")
                            Case 2
                                hidQuesID = New HiddenField
                                hidQuesID.ID = "hidQuesID" + i.ToString()

                                InnerTdSetting(tdQues, "500", "Left")
                                lblQues.Text = dsQues.Tables(0).Rows(i).Item("QUESDESC").ToString()
                                hidQuesID.Value = dsQues.Tables(0).Rows(i).Item("QUESTIONID").ToString()
                                lblQues.Style.Add("font-weight", "bold")
                                lblQues.Style.Add("font-size", "14px")
                                lblQues.Style.Add("color", "#17202A")
                                tdQues.Controls.Add(lblQues)
                                tdQues.Controls.Add(hidQuesID)
                        End Select
                    Next

                    trQues.Controls.Add(tdId)
                    trQues.Controls.Add(tdQues)
                    tblSurvey.Controls.Add(trQues)

                    dvAns.RowFilter = "QUESTIONID = " + dsQues.Tables(0).Rows(i).Item("QUESTIONID").ToString()
                    dtAns = dvAns.ToTable()

                    DvCustCnt.RowFilter = "QUESTIONID = " + dsQues.Tables(0).Rows(i).Item("QUESTIONID").ToString()
                    DtCustCnt = DvCustCnt.ToTable()

                    If dtAns.Rows.Count > 0 Then
                        For s = 1 To 2
                            Select Case s
                                Case 1
                                    trRdb = New TableRow
                                    For w = 1 To 3
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
                                                        tdAvglbl = New TableCell
                                                        tdAnsDesCol = New TableCell

                                                        Dim Average As Double = 0
                                                        AnsDes = New Label
                                                        avg = New Label


                                                        AnsDes.Width = 400
                                                        avg.Width = 50


                                                        DvAnsCnt.RowFilter = "QUESTIONID = " + dsQues.Tables(0).Rows(i).Item("QUESTIONID").ToString() + "AND ANSWERID=" + dtAns.Rows(f).Item("ANSWERID").ToString()
                                                        DtAnsCnt = DvAnsCnt.ToTable()

                                                        If DtCustCnt.Rows.Count > 0 And DtAnsCnt.Rows.Count > 0 Then
                                                            Average = Average + FormatNumber(DtAnsCnt.Rows(0).Item("ANSWERCOUNT").ToString() / DtCustCnt.Rows(0).Item("CUSTOMERCOUNT").ToString(), 2)
                                                            Average = Average * 100
                                                        End If

                                                        InnerTdSetting(tdRdb, "500", "left")
                                                        AnsDes.Text = dtAns.Rows(f).Item("ANSDESC").ToString()
                                                        avg.Text = Average.ToString() + "%"

                                                        'AnsDes.Style.Add("font-weight", "bold")
                                                        avg.Style.Add("font-weight", "bold")
                                                        avg.Style.Add("font-size", "14px")
                                                        AnsDes.Style.Add("font-size", "12px")
                                                        avg.Style.Add("color", "#17202A")

                                                        If User = "Admin" Then
                                                            tdAnsDesCol.Enabled = False
                                                        End If

                                                        tdAvglbl.Controls.Add(avg)
                                                        tdAnsDesCol.Controls.Add(AnsDes)

                                                        trRdbRow.Controls.Add(tdAvglbl)
                                                        trRdbRow.Controls.Add(tdAnsDesCol)
                                                    End If
                                                    tblRdb.Controls.Add(trRdbRow)
                                                Next
                                                tdRdb.Controls.Add(tblRdb)
                                                trRdb.Controls.Add(tdRdb)
                                            Case 3
                                                trTresponse = New TableRow
                                                For p = 1 To 2
                                                    tdTResponse = New TableCell
                                                    Select Case p
                                                        Case 1
                                                            InnerTdSetting(tdTResponse, "100", "Center")
                                                            trTresponse.Controls.Add(tdTResponse)
                                                        Case 2
                                                            tblRes = New Table
                                                            trRes = New TableRow
                                                            If dsQues.Tables(0).Rows(i).Item("TYPEID").ToString() <> "2" Then
                                                                tdRes = New TableCell
                                                                TResponse = New Label

                                                                If DtCustCnt.Rows.Count > 0 Then
                                                                    TResponse.Text = "Total responses: " + DtCustCnt.Rows(0).Item("CUSTOMERCOUNT").ToString()
                                                                Else
                                                                    TResponse.Text = "Total responses: 0"
                                                                End If

                                                                TResponse.Style.Add("font-size", "12px")
                                                                TResponse.Style.Add("color", "#BDC3C7")
                                                                InnerTdSetting(tdRes, "500", "center")
                                                                tdRes.Controls.Add(TResponse)
                                                                trRes.Controls.Add(tdRes)
                                                            End If
                                                            tblRes.Controls.Add(trRes)
                                                            tdTResponse.Controls.Add(tblRes)
                                                            trTresponse.Controls.Add(tdTResponse)
                                                    End Select
                                                Next
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
                                                    tdComment.Style.Add("font-size", "14px")
                                                    tdComment.Style.Add("color", "#17202A")
                                                    trComment.Controls.Add(tdComment)
                                                Case 2
                                                    tblComment = New Table
                                                    trCom = New TableRow
                                                    tdCom = New TableCell
                                                    txtComment = New TextBox
                                                    txtComment.TextMode = TextBoxMode.MultiLine
                                                    InnerTdSetting(tdCom, "500", "center")
                                                    txtComment.Text = "NA"
                                                    txtComment.Style.Add("font-size", "12px")
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
                        tblSurvey.Controls.Add(trTresponse)
                    Else
                        'trComment = New TableRow
                        'For w = 1 To 2
                        '    tdComment = New TableCell
                        '    Select Case w
                        '        Case 1
                        '            InnerTdSetting(tdComment, "", "Center")
                        '            tdComment.Text = "<b> Comment </b>"
                        '            tdComment.Style.Add("font-size", "14px")
                        '            tdComment.Style.Add("color", "#17202A")
                        '            trComment.Controls.Add(tdComment)
                        '        Case 2
                        '            tblComment = New Table
                        '            txtComment = New TextBox
                        '            trCom = New TableRow
                        '            tdCom = New TableCell
                        '            If User = "Admin" Then
                        '                txtComment.Enabled = False
                        '            End If
                        '            txtComment.TextMode = TextBoxMode.MultiLine
                        '            InnerTdSetting(tdCom, "50px", "center")
                        '            txtComment.Text = "NA"
                        '            txtComment.Style.Add("font-size", "12px")
                        '            tdCom.Controls.Add(txtComment)
                        '            trCom.Controls.Add(tdCom)

                        '            tblComment.Controls.Add(trCom)
                        '            tdComment.Controls.Add(tblComment)
                        '            trComment.Controls.Add(tdComment)
                        '    End Select
                        'Next
                    End If
                    'tblSurvey.Controls.Add(trComment)
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
            lblError.Text = "Error:GetAdminAvgAnswer:" + ex.Message.ToString()
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

End Class
