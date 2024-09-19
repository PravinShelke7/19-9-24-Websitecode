Imports Microsoft.VisualBasic
Imports System.Web.SessionState
Imports System.Data
Imports System.Data.OleDb
Imports System
Imports System.Math
Imports System.Web.UI.HtmlTextWriter

Public Class SavvyProUpInsData
    Dim odbUtil As New DBUtil()
    Dim SavvyProConnection As String = System.Configuration.ConfigurationManager.AppSettings("SavvyPackProConnectionString")

    Public Sub AddGroupName(ByVal Des1 As String, ByVal Des2 As String, ByVal USERID As String)
        Dim odButil As New DBUtil()
        Dim strsql As String = String.Empty
        Dim Dts As New DataSet
        Dim GROUPID As String = ""
        Dim i As Integer = 0
        Try
            'Getting GROUPID from Sequence
            strsql = String.Empty
            strsql = "SELECT SEQGROUPID.NEXTVAL AS GROUPID  "
            strsql = strsql + "FROM DUAL "
            Dts = odButil.FillDataSet(strsql, SavvyProConnection)
            If Dts.Tables(0).Rows.Count > 0 Then
                GROUPID = Dts.Tables(0).Rows(0).Item("GROUPID").ToString()

                'Inserting Into Groups Table
                strsql = String.Empty
                strsql = "INSERT INTO GROUPS  "
                strsql = strsql + "( "
                strsql = strsql + "GROUPID, "
                strsql = strsql + "DES1, "
                strsql = strsql + "DES2, "
                strsql = strsql + "USERID, "
                strsql = strsql + "CREATIONDATE, "
                strsql = strsql + "UPDATEDATE "
                strsql = strsql + ") "
                strsql = strsql + "SELECT " + GROUPID + ", "
                strsql = strsql + "'" + Des1 + "', "
                strsql = strsql + "'" + Des2 + "', "
                strsql = strsql + USERID + ", "
                strsql = strsql + "sysdate, "
                strsql = strsql + "sysdate "
                strsql = strsql + "FROM DUAL "
                odButil.UpIns(strsql, SavvyProConnection)
            End If
        Catch ex As Exception
            Throw New Exception("SavvyProUpInsData:AddGroupName:" + ex.Message.ToString())
        End Try
    End Sub

#Region "Investment Manager"

    Public Sub UpdateInvestment(ByVal Description() As String, ByVal amount() As String, ByVal comments() As String, ByVal userid As String)
        Try
            Dim StrSqlUpadate As String = ""
            'Creating Database Connection
            Dim odbUtil As New DBUtil()
            Dim ds As DataSet
            Dim ObjGetData As New SavvyProGetData
            Dim count As Integer
            count = ObjGetData.GetCountInvestmentManagerDet(userid)
            ds = ObjGetData.GetInvestmentManagerDet(userid)
            If ds.Tables(0).Rows.Count > 0 Then

                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Dim j As Integer = i + 1
                    StrSqlUpadate = "UPDATE INVESTMENTMANAGER SET IMDESCRIPTION='" + Description(i).ToString() + "',AMOUNTS=" + amount(i).ToString() + ",COMMENTS='" + comments(i).ToString() + "' WHERE USERID=" + userid.ToString() + " AND INVESTMENTMANAGERID=" + j.ToString() + ""
                    odbUtil.UpIns(StrSqlUpadate, SavvyProConnection)
                Next
                Dim cnt As Integer = ds.Tables(0).Rows.Count
                If ds.Tables(0).Rows.Count < Description.Length - 2 Then
                    For i As Integer = cnt To Description.Length - 3
                        Dim j As Integer = i + 1

                        StrSqlUpadate = "insert into investmentmanager(investmentmanagerid,imdescription,amounts,comments,userid) values('" + (j.ToString()) + "','" + Description(i).ToString() + "','" + amount(i).ToString() + "','" + comments(i).ToString() + "','" + userid.ToString() + "')"
                        odbUtil.UpIns(StrSqlUpadate, SavvyProConnection)
                    Next
                End If

            Else
                For i As Integer = 0 To Description.Length - 2
                    Dim j As Integer = i + 1
                    If Description(i).ToString() <> "Nothing" Then
                        StrSqlUpadate = "insert into investmentmanager(investmentmanagerid,imdescription,amounts,comments,userid) values('" + (j.ToString()) + "','" + Description(i).ToString() + "','" + amount(i).ToString() + "','" + comments(i).ToString() + "','" + userid.ToString() + "')"
                        odbUtil.UpIns(StrSqlUpadate, SavvyProConnection)
                    End If
                Next
            End If
        Catch ex As Exception
            Throw New Exception("E1UpInsData:UpdateInvestment:" + ex.Message.ToString())
        End Try
    End Sub

#End Region
End Class
