Imports Microsoft.VisualBasic
Imports DBUtil
Imports System.Data

Public Class SurveyBundleUpdateData
    Dim ShopConnection As String = System.Configuration.ConfigurationManager.AppSettings("ShoppingConnectionString")

    Public Sub AddSurveyAnswer(ByVal UserId As String, ByVal SurveyId As String, ByVal QuestionId() As String, ByVal AnswerId() As String, ByVal CommentDesc() As String, _
                               ByVal count As Integer, ByVal BlastId As String, ByVal SBManagerID As String)
        Dim odbUtil As New DBUtil()
        Try
            For i = 0 To count
                Dim StrSql As String = String.Empty
                StrSql = "INSERT INTO SURVEYANSWERS(USERID,BLASTID,SURVEYID,SBMANAGERID,QUESTIONID,ANSWERID,COMMENTDESC,SERVERDATE) "
                If CommentDesc(i) Is Nothing Then
                    StrSql = StrSql + "SELECT " + UserId + "," + BlastId + "," + SurveyId + "," + SBManagerID + "," + QuestionId(i) + ",'" + AnswerId(i) + "','" + CommentDesc(i) + "',SYSDATE FROM DUAL "
                Else
                    StrSql = StrSql + "SELECT " + UserId + "," + BlastId + "," + SurveyId + "," + SBManagerID + "," + QuestionId(i) + ",'" + AnswerId(i) + "','" + CommentDesc(i).Replace("'", "''") + "',SYSDATE FROM DUAL "
                End If

                odbUtil.UpIns(StrSql, ShopConnection)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Public Sub AddResponseDate(ByVal UserId As String, ByVal SBManagerID As String, ByVal BlastId As String)
        Dim odbUtil As New DBUtil()
        Dim StrSql As String = String.Empty
        Try
            StrSql = "UPDATE SURVEYLOG SET "
            StrSql = StrSql + "RESPONSEDATE=SYSDATE "
            StrSql = StrSql + "WHERE CLIENTID=" + UserId + " AND SBMANAGERID=" + SBManagerID + " AND BLASTID=" + BlastId + " "

            odbUtil.UpIns(StrSql, ShopConnection)
        Catch ex As Exception

        End Try
    End Sub

End Class
