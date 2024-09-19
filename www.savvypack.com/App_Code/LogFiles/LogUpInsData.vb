Imports Microsoft.VisualBasic
Imports System.Web.SessionState
Imports System.Data
Imports System.Data.OleDb
Imports System
Imports LogUpInsData
Imports System.Math
Imports System.Web.UI.HtmlTextWriter

Public Class LogUpInsData
    Public Class UpdateInsert
        Dim LogConnection As String = System.Configuration.ConfigurationManager.AppSettings("LogConnectionString")
        Dim odbutil As New DBUtil()

#Region "LOG ENTRIES"
        Public Function InsertLog(ByVal UserId As String, ByVal PageId As String, ByVal ActivityDetailsId As String, ByVal CaseId As String, ByVal SessionId As String, ByVal ServiceId As String, ByVal ToCaseId As String, ByVal DestinationUserId As String, ByVal groupId As String) As Integer

            'Creating Database Connection
            Dim odbUtil As New DBUtil()
            ' Dim ObjGetData As New Selectdata()
            Dim StrSql As String = String.Empty
            Dim count As Integer

            Try
                StrSql = "SELECT SEQACTIVELOGCOUNT.NEXTVAL FROM DUAL"
                count = odbUtil.FillData(StrSql, LogConnection)


                'Report Details
                StrSql = String.Empty
                StrSql = "INSERT INTO SAVVYPACKAPP_ACTIVITYLOG (LOGID,USERID,LOGINCOUNT,USERSEQUENCE,PAGEID,CASEID,ACTIVITYDETAILSID,SESSIONID,GROUPID,SERVICEID,TO_CASEID,DESTINATION_USERID,ACTIVITYTIME)  "
                StrSql = StrSql + "VALUES( SEQACTIVITYLOGID.NEXTVAL," + UserId + "," + count.ToString() + ",1,'" + PageId + "','" + ActivityDetailsId + "','" + SessionId + " ',"
                StrSql = StrSql + "'" + groupId + "', "
                StrSql = StrSql + "'" + ServiceId + "', "
                StrSql = StrSql + "'" + ToCaseId + "', "
                StrSql = StrSql + "'" + DestinationUserId + "', "
                StrSql = StrSql + "sysdate) "

                odbUtil.UpIns(StrSql, LogConnection)
                Return count
            Catch ex As Exception
                Throw New Exception("StandUpInsData:InsertLog:" + ex.Message.ToString())
            End Try

        End Function
        Public Sub InsertLog1(ByVal UserId As String, ByVal PageId As String, ByVal ActivityDetailsId As String, ByVal CaseId As String, ByVal SessionId As String, ByVal logCount As String, ByVal ServiceId As String, ByVal ToCaseId As String, ByVal DestinationUserId As String, ByVal groupId As String)

            'Creating Database Connection
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim userseq As Integer
            'Dim logCount As Integer

            Try
                'StrSql = "SELECT SEQLOGINID.NEXTVAL FROM DUAL"
                'logCount = odbUtil.FillData(StrSql, LogConnection)

                'StrSql = "SELECT SEQLOGINID.NEXTVAL FROM DUAL"
                'logCount = odbUtil.FillData(StrSql, LogConnection)

                StrSql = String.Empty
                StrSql = "SELECT NVL(MAX(USERSEQUENCE),1) USERSEQUENCE FROM SAVVYPACK_ACTIVITYLOG WHERE LOGINCOUNT=" + logCount
                userseq = odbUtil.FillData(StrSql, LogConnection) + 1

                'log Details
                StrSql = String.Empty
                StrSql = "INSERT INTO SAVVYPACKAPP_ACTIVITYLOG (LOGID,  "
                StrSql = StrSql + "USERID, "
                StrSql = StrSql + "LOGINCOUNT, "
                StrSql = StrSql + "USERSEQUENCE, "
                StrSql = StrSql + "PAGEID, "
                StrSql = StrSql + "CASEID, "
                StrSql = StrSql + "ACTIVITYDETAILSID, "
                StrSql = StrSql + "SESSIONID "
                StrSql = StrSql + "GROUPID, "
                StrSql = StrSql + "SERVICEID, "
                StrSql = StrSql + "TO_CASEID, "
                StrSql = StrSql + "DESTINATION_USERID, "
                StrSql = StrSql + "ACTIVITYTIME) "

                StrSql = StrSql + "VALUES( SEQACTIVITYLOGID.NEXTVAL,  "
                StrSql = StrSql + UserId + ", "
                StrSql = StrSql + logCount + ", "
                StrSql = StrSql + userseq.ToString() + " , "
                StrSql = StrSql + "'" + PageId + "', "
                StrSql = StrSql + "'" + CaseId + "', "

                StrSql = StrSql + "'" + ActivityDetailsId + "', "
                StrSql = StrSql + "'" + SessionId + "', "
                StrSql = StrSql + "'" + groupId + "', "
                StrSql = StrSql + "'" + ServiceId + "', "
                StrSql = StrSql + "'" + ToCaseId + "', "
                StrSql = StrSql + "'" + DestinationUserId + "', "
                StrSql = StrSql + "sysdate)"

                odbUtil.UpIns(StrSql, LogConnection)

            Catch ex As Exception
                Throw New Exception("StandUpInsData:InsertLog1:" + ex.Message.ToString())
            End Try

        End Sub
#End Region





    End Class
End Class
