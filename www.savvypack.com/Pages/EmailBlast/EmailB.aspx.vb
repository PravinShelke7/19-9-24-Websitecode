Imports System.Data
Imports System.Data.OleDb
Imports System
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter

Partial Class Pages_EmailBlast_EmailB
    Inherits System.Web.UI.Page
    Dim EconConnection As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim userId As String = String.Empty
            Dim pageN As String = String.Empty
            'Dim blastId As String = Request.QueryString("blast").ToString()
            Dim dt As New DataSet
            Dim blastId As String = String.Empty
            
            userId = Request.QueryString("Id").ToString()
            pageN = Request.QueryString("page").ToString()



            blastId = Request.QueryString("blast").ToString()
            Dim ipAddress As String = String.Empty
            ipAddress = HttpContext.Current.Request.Params("HTTP_CLIENT_IP")
            If Not String.IsNullOrEmpty(ipAddress) Then
            Else
                ipAddress = HttpContext.Current.Request.UserHostAddress
            End If
            dt = GetEmailUsers(userId)
            Dim userName As String = dt.Tables(0).Rows(0).Item("USERNAME").ToString()
            ManageEmailVisitorsLog(userName, pageN, blastId, ipAddress, userId)
            If pageN.Contains(".aspx") Then
                pageN = "http://www.savvypack.com/" + pageN
            End If

            Response.Redirect(pageN)

            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Test2 " + pageN.ToString() + " successfully.');", True)

        Catch ex As Exception

        End Try
    End Sub

#Region "Visitor Log"

    Public Sub ManageEmailVisitorsLog(ByVal email As String, ByVal page As String, ByVal blastId As String, ByVal ipAdd As String, ByVal userId As String)

        Dim ds As New DataSet()
        Dim odbUtil As New DBUtil()
        Dim StrSql As String = ""
        Try
            'Assign StructAssist to User
            StrSql = String.Empty
            StrSql = "INSERT INTO VISITORSLOG  "
            StrSql = StrSql + "(EMAILADD,PAGE,VISITDATE,BLASTID,IPADD,USERID) "
            StrSql = StrSql + "SELECT '" + email + "','" + page + "',SYSDATE, " + blastId + ", '" + ipAdd + "' , '" + userId + "'"
            StrSql = StrSql + " FROM DUAL "
            odbUtil.UpIns(StrSql, EconConnection)
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Function GetEmailUsers(ByVal UserId As String) As DataSet

        Dim Dts As New DataSet()
        Dim odbUtil As New DBUtil()
        Dim StrSql As String = String.Empty
        Try
            StrSql = "SELECT USERNAME  "
            StrSql = StrSql + "FROM USERS "
            StrSql = StrSql + "WHERE USERID = " + UserId
            Dts = odbUtil.FillDataSet(StrSql, EconConnection)
            Return Dts
        Catch ex As Exception
            Throw ex
        End Try

    End Function

#End Region
End Class
