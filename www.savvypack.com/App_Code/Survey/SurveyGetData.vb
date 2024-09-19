Imports Microsoft.VisualBasic
Imports DBUtil
Imports System.Data
Public Class SurveyGetData
    Dim ShopConnection As String = System.Configuration.ConfigurationManager.AppSettings("ShoppingConnectionString")

    Public Function GetQuestionIds(ByVal Id As String) As String
        Dim Dts As New DataSet
        Dim odbUtil As New DBUtil()
        Dim StrSql As String = String.Empty
        Dim QId As String = String.Empty
        Try
            StrSql = "SELECT QUESTIONID "
            StrSql = StrSql + "FROM QUESSURVEYCON "
            StrSql = StrSql + "WHERE SURVEYID = " + Id
            StrSql = StrSql + "ORDER BY QUESTIONID"
            Dts = odbUtil.FillDataSet(StrSql, ShopConnection)
            For i = 0 To Dts.Tables(0).Rows.Count - 1
                If i = Dts.Tables(0).Rows.Count - 1 Then
                    QId += Dts.Tables(0).Rows(i).Item("QUESTIONID").ToString()
                Else
                    QId += Dts.Tables(0).Rows(i).Item("QUESTIONID").ToString() + ","
                End If
            Next
            Return QId
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetQuestions(ByVal Id As String) As DataSet
        Dim Dts As New DataSet
        Dim odbUtil As New DBUtil()
        Dim StrSql As String = String.Empty
        Try
            StrSql = "SELECT Q.QUESTIONID,QUESDESC,FLAG,PARENTID,QSC.QUESTIONSEQ "
            StrSql = StrSql + "FROM QUESSURVEYCON QSC "
            StrSql = StrSql + "INNER JOIN QUESTIONS Q ON Q.QUESTIONID = QSC.QUESTIONID "
            StrSql = StrSql + "WHERE SURVEYID = " + Id + " ORDER BY QSC.QUESTIONSEQ "
            Dts = odbUtil.FillDataSet(StrSql, ShopConnection)
            Return Dts
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetAnswers(ByVal Id As String) As DataSet
        Dim Dts As New DataSet
        Dim odbUtil As New DBUtil()
        Dim StrSql As String = String.Empty
        Try
            StrSql = "SELECT ANSWERID,ANSDESC,QUESTIONID,RANK "
            StrSql = StrSql + "FROM ANSWERS "
            StrSql = StrSql + "WHERE QUESTIONID IN(" + Id + ") ORDER BY ANSWERID "
            Dts = odbUtil.FillDataSet(StrSql, ShopConnection)
            Return Dts
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetSurveyAnswers(ByVal UserId As String, ByVal SurveyId As String, ByVal BlastId As String) As DataSet
        Dim Dts As New DataSet
        Dim odbUtil As New DBUtil()
        Dim StrSql As String = String.Empty
        Try
            StrSql = "SELECT USERID,SURVEYID,QUESTIONID,ANSWERID,COMMENTDESC "
            StrSql = StrSql + "FROM SURVEYANSWERS  "
            StrSql = StrSql + "WHERE USERID=" + UserId + " AND SURVEYID=" + SurveyId + " AND BLASTID='" + BlastId + "' "
            Dts = odbUtil.FillDataSet(StrSql, ShopConnection)
            Return Dts
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetUserName(ByVal UserId As String) As DataSet
        Dim Dts As New DataSet
        Dim odbUtil As New DBUtil()
        Dim StrSql As String = String.Empty
        Try
            StrSql = "SELECT USERNAME,USERID "
            StrSql = StrSql + "FROM ECON.USERS "
            StrSql = StrSql + "WHERE USERID=" + UserId + ""
            Dts = odbUtil.FillDataSet(StrSql, ShopConnection)
            Return Dts
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetSurveyName(ByVal SurveyId As String) As DataSet
        Dim Dts As New DataSet
        Dim odbUtil As New DBUtil()
        Dim StrSql As String = String.Empty
        Try
            StrSql = "SELECT SURVEYID,NAME "
            StrSql = StrSql + "FROM SURVEY "
            StrSql = StrSql + "WHERE SURVEYID=" + SurveyId + ""
            Dts = odbUtil.FillDataSet(StrSql, ShopConnection)
            Return Dts
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
