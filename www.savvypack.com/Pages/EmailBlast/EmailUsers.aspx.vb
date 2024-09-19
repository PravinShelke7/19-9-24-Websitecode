Imports System.Data
Imports System.Data.OleDb
Imports System
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Imports System.Diagnostics

Partial Class Pages_EmailBlast_EmailUsers
    Inherits System.Web.UI.Page

    Dim EconConnection As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim userId As String = String.Empty
        Dim pageN As String = String.Empty
        Dim dt As New DataSet
        Dim blastId As String = String.Empty
        Dim ipAddress As String = String.Empty
        Dim obj As New CryptoHelper
        Try

            userId = Request.QueryString("Id").ToString()
            pageN = Request.QueryString("page").ToString()
            blastId = Request.QueryString("blast").ToString()
            ipAddress = HttpContext.Current.Request.Params("HTTP_CLIENT_IP")

            If Not String.IsNullOrEmpty(ipAddress) Then
            Else
                ipAddress = HttpContext.Current.Request.UserHostAddress
            End If

            dt = GetEmailUsers(userId)
            Dim userName As String = dt.Tables(0).Rows(0).Item("USERNAME").ToString()
            ManageEmailVisitorsLog(userName, pageN, blastId, ipAddress, userId)
            If pageN.Contains(".aspx") Then
                'pageN = "http://www.savvypack.com/" + pageN
                pageN = "http://www.savvypack.com/" + pageN
            End If

            'Survey Changes
            If pageN.Contains("ViewSurvey.aspx") Then
                Dim SurveyId As String = Request.QueryString("SurveyId").ToString()
                Dim User As String = Request.QueryString("User").ToString()
                pageN = pageN + "?SurveyId=" + SurveyId + "&User=" + User + "&UserId=" + obj.Encrypt(userId) + "&BlastId=" + obj.Encrypt(blastId) + ""
                Response.Redirect(String.Format(pageN), False)
            ElseIf pageN.Contains("ViewSurveyBundle.aspx") Then
                Dim SurveyBundleId As String = Request.QueryString("SurveyBundleId").ToString()
                Dim User As String = Request.QueryString("User").ToString()
                pageN = pageN + "?SurveyBundleId=" + SurveyBundleId + "&User=" + User + "&UserId=" + obj.Encrypt(userId) + "&BlastId=" + obj.Encrypt(blastId) + ""
                Response.Redirect(String.Format(pageN), False)
            ElseIf pageN.Contains(".pdf") Then
                PDFDwnld(sender, e)
            Else
                Response.Redirect(pageN, False)
            End If
           
            'Response.Redirect(pageN)
            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Test2 " + pageN.ToString() + " successfully.');", True)

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
#Region "For Download PDF"
    Protected Sub PDFDwnld(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim repDetails As String = ""
        'Dim filename() As String
        Dim DownloadPath As String = String.Empty
        Dim ds As New DataSet
        Dim objGetData As New SavvyGetData.Selectdata()
        Try

            'DownloadPath = "//192.168.3.236/Email/SavvyPack%C2%AE_Aluminum_Price%20_Report_Sample.pdf"
            DownloadPath = "E:/Website_Code/15SepFixedCostChanges/Website/www.savvypack.com/Email/SavvyPack®_Aluminum_Price _Report_Sample.pdf"
            Response.ContentType = "application/octet-stream"
            Response.AppendHeader("Content-Disposition", "attachment;filename=SavvyPack®_Aluminum_Price _Report_Sample.pdf")
            Response.TransmitFile(DownloadPath)
            Response.End()
            Response.Write("<script>window.close();</script>")

        Catch ex As Exception

        End Try
    End Sub
 Protected Sub PDFDwnld1(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim FILE_NAME As String = "C:\Documents and Settings\All Users\Documents\SavvyPack®_Aluminum_Price _Report_Sample.pdf"
        If System.IO.File.Exists(FILE_NAME) = True Then
            If MsgBox("Do you want to replace file?", MsgBoxStyle.YesNo, "Title") = MsgBoxResult.Yes Then
                IO.File.Copy("C:/Users/Pratima/Documents/SavvyPack®_Aluminum_Price _Report_Sample.pdf", "C:\Documents and Settings\All Users\Documents\SavvyPack®_Aluminum_Price _Report_Sample.pdf", True)
                'My.Computer.Network.DownloadFile("C:/Users/Pratima/Documents/astapkir315.png", "C:\Documents and Settings\All Users\Documents\astapkir315.png")
            End If
        Else
            My.Computer.Network.DownloadFile("E:/Website_Code/15SepFixedCostChanges/Website/www.savvypack.com/Email/SavvyPack®_Aluminum_Price _Report_Sample.pdf", "C:\Documents and Settings\All Users\Documents\SavvyPack®_Aluminum_Price _Report_Sample.pdf")
        End If
        If System.IO.File.Exists(FILE_NAME) = True Then
            Diagnostics.Process.Start(FILE_NAME)
        End If
    End Sub
   
#End Region

End Class
