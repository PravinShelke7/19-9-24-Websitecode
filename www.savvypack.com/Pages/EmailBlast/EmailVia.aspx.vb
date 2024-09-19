Imports UsersUpdateData
Imports System.Data

Partial Class EmailVia
    Inherits System.Web.UI.Page
    Dim EconConnection As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objUpIns As New UsersUpdateData.UpdateInsert
        Dim userName As String = Request.QueryString("Id").ToString()
        Dim page As String = Request.QueryString("page").ToString()
        Dim blastId As String = Request.QueryString("blast").ToString()
        ManageVisitorsLog(userName, page, blastId)
        Response.Redirect(page)
    End Sub
#Region "Visitor Log"
    Public Sub ManageVisitorsLog(ByVal email As String, ByVal page As String, ByVal blastId As String)
        Dim ds As New DataSet()
        Dim odbUtil As New DBUtil()
        Dim StrSql As String = ""
        Try
            'Assign StructAssist to User
            StrSql = String.Empty
            StrSql = "INSERT INTO VISITORSLOG  "
            StrSql = StrSql + "(EMAILADD,PAGE,VISITDATE,BLASTID) "
            StrSql = StrSql + "SELECT '" + email + "','" + page + "',SYSDATE, " + blastId + " "
            StrSql = StrSql + " FROM DUAL "
            odbUtil.UpIns(StrSql, EconConnection)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region
End Class
