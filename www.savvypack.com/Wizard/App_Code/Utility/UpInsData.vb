Imports Microsoft.VisualBasic
Imports System.Data.OleDb
Imports System.Data
Imports Allied.DataBaseUtility
Imports Allied.GetData


Namespace Allied.UpdateInsert

    Public Class UpInsData
        Dim connectionStringS1 As String = System.Configuration.ConfigurationManager.ConnectionStrings("SustainConn").ConnectionString.ToString()
        Dim connectionStringS2 As String = System.Configuration.ConfigurationManager.ConnectionStrings("Sustain2Conn").ConnectionString.ToString()
        Dim connectionStringE1 As String = System.Configuration.ConfigurationManager.ConnectionStrings("EconConn").ConnectionString.ToString()
        Dim connectionStringE2 As String = System.Configuration.ConfigurationManager.ConnectionStrings("EconConn").ConnectionString.ToString()
        Dim odbutil As New DbUtil.DbUtil()




#Region "Messages"


        Public Sub DeleteUserMessages(ByVal UserId As Integer, ByVal MessageId As String)
            Try
                Dim StrSql As String = String.Empty
                StrSql = "INSERT INTO USEERDELETDMSG  "
                StrSql = StrSql + "(USERID,MESSAGEID,SERVERDATE) "
                StrSql = StrSql + "VALUES "
                StrSql = StrSql + "(" + UserId.ToString() + "," + MessageId.ToString() + ",SYSDATE) "
                odbutil.UpIns(StrSql, connectionStringE1)
            Catch ex As Exception
                Throw New Exception("UpInsData:DeleteUserMessages:" + ex.Message.ToString())
            End Try
        End Sub

#End Region

#Region "EnergyWizard"
        Public Sub InsertWizardDetails(ByVal CaseId As String, ByVal RegionId As String, ByVal Effdate As String, ByVal Modules As String)
            Dim strSql As String = ""
            Dim Conn As String = ""
            Try
                strSql = "INSERT INTO WIZARDDETAILS(CASEID,REGIONID,EFFDATE) SELECT " + CaseId + "," + RegionId + ", TO_DATE('" + Effdate + "','MM?DD/YYYY') FROM DUAL WHERE NOT EXISTS( SELECT 1 FROM  WIZARDDETAILS WHERE CASEID=" + CaseId + " AND REGIONID=" + RegionId + " AND EFFDATE  =  TO_DATE('" + Effdate.ToString() + "','MON DD,YYYY'))"
                If Modules = "S1" Then
                    Conn = connectionStringS1
                ElseIf Modules = "S2" Then
                    Conn = connectionStringS2
                End If

                odbutil.UpIns(strSql, Conn)

            Catch ex As Exception
                Throw New Exception("UpInsData:InsertWizardDetails:" + ex.Message.ToString())
            End Try
        End Sub

        Public Sub UpdateWizardDetails(ByVal CaseId As String, ByVal Prod() As String, ByVal Ergy() As String, ByVal Ghg() As String, ByVal Water() As String, ByVal Loss() As String, _
                                       ByVal Trans() As String, ByVal TotErgy() As String, ByVal TotGhg() As String, ByVal TotWater() As String, ByVal RegionId As String, ByVal Effdate As String, ByVal Modules As String)
            Dim strSql As String = ""
            Dim Conn As String = ""
            Try
                strSql = "UPDATE WIZARDDETAILS SET REGIONID=" + RegionId + ", EFFDATE=TO_DATE('" + Effdate + "','MM?DD/YYYY'), "
                For i = 0 To 9
                    strSql = strSql + "PRODUCTION" + (i + 1).ToString() + "='" + Prod(i) + "', ERGYUSERATIO" + (i + 1).ToString() + "='" + Ergy(i) + "', "
                    strSql = strSql + "CO2USERATIO" + (i + 1).ToString() + "='" + Ghg(i) + "', WATERUSERATIO" + (i + 1).ToString() + "='" + Water(i) + "', "
                    strSql = strSql + "TRANSLOSS" + (i + 1).ToString() + "='" + Trans(i) + "', LOSSMFGFACI" + (i + 1).ToString() + "='" + Loss(i) + "', "
                    strSql = strSql + "TOTERGY" + (i + 1).ToString() + "='" + TotErgy(i) + "', TOTCO2" + (i + 1).ToString() + "='" + TotGhg(i) + "',"
                    strSql = strSql + "TOTWATER" + (i + 1).ToString() + "='" + TotWater(i) + "',"
                Next
                strSql = strSql.Remove(strSql.Length - 1)
                strSql = strSql + " WHERE CASEID=" + CaseId + " "

                If Modules = "S1" Then
                    Conn = connectionStringS1
                ElseIf Modules = "S2" Then
                    Conn = connectionStringS2
                End If

                odbutil.UpIns(strSql, Conn)

            Catch ex As Exception
                Throw New Exception("UpInsData:UpdateWizardDetails:" + ex.Message.ToString())
            End Try
        End Sub
#End Region



    End Class

End Namespace
