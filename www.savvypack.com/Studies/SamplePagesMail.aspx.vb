Imports System.Data
Imports System.Data.OleDb
Imports System
Imports StudyGetData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Imports System.Net.Mail
Imports System.Net
Partial Class Studies_SamplePagesMail
    Inherits System.Web.UI.Page
    Dim _strReportId As String
    Public Property ReportId() As String
        Get
            Return _strReportId
        End Get
        Set(ByVal Value As String)
            _strReportId = Value
        End Set
    End Property


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("MenuItem") = "MCB"
        Dim ObjCrypto As New CryptoHelper()
        Dim objgetData As New StudyGetData.Selectdata
        Dim ds As New DataSet
        Dim dsUserData As New DataSet
        Dim strBody As String = ""

        ReportId = ObjCrypto.Decrypt(Request.QueryString("ID"))
       ' If Session("Back") = Nothing Then
	    If Session("Back") = Nothing And Session("SBack") = Nothing Then
            Dim obj As New CryptoHelper
            Response.Redirect("~/Universal_loginN/Pages/ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "", False)
        Else
            If Not IsPostBack Then
                If ReportId <> Nothing Then
                    ds = objgetData.GetSamplePageDetails(ReportId)
                    dsUserData = objgetData.GetUserDetails(Session("UserId").ToString())
                    If ds.Tables(0).Rows.Count > 0 And dsUserData.Tables(0).Rows.Count > 0 Then
                        strBody = GetEmailBodyData(ds.Tables(0).Rows(0).Item("PartID").ToString(), ds.Tables(0).Rows(0).Item("PARTDES").ToString(), dsUserData.Tables(0).Rows(0).Item("PREFIX").ToString(), dsUserData.Tables(0).Rows(0).Item("FIRSTNAME").ToString(), dsUserData.Tables(0).Rows(0).Item("LASTNAME").ToString(), dsUserData.Tables(0).Rows(0).Item("COMPANYNAME").ToString(), dsUserData.Tables(0).Rows(0).Item("EMAILADDRESS").ToString(), dsUserData.Tables(0).Rows(0).Item("PHONENUMBER").ToString())
                        SendEmail(strBody, "SAMRPT")
                        Response.Redirect("~/Studies2/" + ds.Tables(0).Rows(0).Item("SAMPLEPAGES").ToString())
                    Else
                        strBody = GetEmailBodyData(Session("UserId").ToString())
                        SendEmail(strBody, "ERRUSRCNCT")
                        Response.Redirect("~/Studies2/" + ds.Tables(0).Rows(0).Item("SAMPLEPAGES").ToString())

                    End If
                End If
            End If
        End If

    End Sub
    Protected Function GetEmailBodyData(ByVal PARTID As String, ByVal PARTDES As String, ByVal PREFIX As String, ByVal FIRSTNAME As String, ByVal LASTNAME As String, ByVal COMPANYNAME As String, ByVal EMAILADDRESS As String, ByVal PHONENUMBER As String) As String
        Dim StrSqlBody As String = ""
        Try
            StrSqlBody = StrSqlBody + "<div style='font-family:Verdana;font-size:15px;margin-top:10px;margin-bottom:10px;font-weight:bold'>Report Details</div>"
            StrSqlBody = StrSqlBody + "<table style='font-family:Verdana;width:500px;font-size:12px;border-collapse:collapse' border='1' bordercolor='black' cellpadding='0' cellspacing='0'>  "
            StrSqlBody = StrSqlBody + "<tr style='background-color:#336699;height:20px;text-align:center;color:white'> "
            StrSqlBody = StrSqlBody + "<td><b>ReportId</b></td> "
            StrSqlBody = StrSqlBody + "<td><b>Report Name</b></td> "
            StrSqlBody = StrSqlBody + "</tr> "
            StrSqlBody = StrSqlBody + "<tr style='height:20px;text-align:Left'> "
            StrSqlBody = StrSqlBody + "<td>" & PARTID + "</b></td> "
            StrSqlBody = StrSqlBody + "<td>" + PARTDES + "</b></td> "
            StrSqlBody = StrSqlBody + "</tr> "
            StrSqlBody = StrSqlBody + "</table> "
            StrSqlBody = StrSqlBody + "<br /><br />"
            StrSqlBody = StrSqlBody + "<div style='font-family:Verdana;font-size:15px;margin-top:10px;margin-bottom:10px;font-weight:bold'>User Details</div>"
            StrSqlBody = StrSqlBody + "<table style='font-family:Verdana;width:700px;font-size:12px;border-collapse:collapse' border='1' bordercolor='black' cellpadding='0' cellspacing='0'>  "
            StrSqlBody = StrSqlBody + "<tr style='background-color:#336699;height:20px;text-align:center;color:white'> "
            StrSqlBody = StrSqlBody + "<td><b>Prefix</b></td> "
            StrSqlBody = StrSqlBody + "<td><b>First Name</b></td> "
            StrSqlBody = StrSqlBody + "<td><b>Last Name</b></td> "
            StrSqlBody = StrSqlBody + "<td><b>Company Name</b></td> "
            StrSqlBody = StrSqlBody + "<td><b>Email Address</b></td> "
            StrSqlBody = StrSqlBody + "<td><b>Phone Number</b></td> "
            StrSqlBody = StrSqlBody + "</tr> "
            StrSqlBody = StrSqlBody + "<tr style='height:20px;text-align:center'> "
            StrSqlBody = StrSqlBody + "<td>" + PREFIX + "</b></td> "
            StrSqlBody = StrSqlBody + "<td>" + FIRSTNAME + "</b></td> "
            StrSqlBody = StrSqlBody + "<td>" + LASTNAME + "</b></td> "
            StrSqlBody = StrSqlBody + "<td>" + COMPANYNAME + "</b></td> "
            StrSqlBody = StrSqlBody + "<td>" + EMAILADDRESS + "</b></td> "
            StrSqlBody = StrSqlBody + "<td>" + PHONENUMBER + "</b></td> "
            StrSqlBody = StrSqlBody + "</tr> "
            StrSqlBody = StrSqlBody + "</table> "
            Return StrSqlBody
        Catch ex As Exception
            Return StrSqlBody
        End Try
    End Function
    Public Sub SendEmail(ByVal strBody As String, ByVal MailCode As String)
        Try
            Dim objGetData As New UsersGetData.Selectdata
            Dim ds As New DataSet
            ds = objGetData.GetAlliedMemberMail(MailCode)

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
            lblError.Text = "Error:SendEmail:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Function GetEmailBodyData(ByVal UserID As String) As String
        Dim StrSqlBody As String = ""
        Dim objgetData As New StudyGetData.Selectdata
        Dim ds As New DataSet
        Try
            ds = objgetData.GetUser(UserID)


            StrSqlBody = StrSqlBody + "<div style='font-family:Verdana;font-size:15px;margin-top:10px;margin-bottom:10px;font-weight:bold'>Following User is not Present in user contact table</div>"
            StrSqlBody = StrSqlBody + "<br /><br />"
            StrSqlBody = StrSqlBody + "<div style='font-family:Verdana;font-size:15px;margin-top:10px;margin-bottom:10px;font-weight:bold'>User Details</div>"
            StrSqlBody = StrSqlBody + "<table style='font-family:Verdana;width:700px;font-size:12px;border-collapse:collapse' border='1' bordercolor='black' cellpadding='0' cellspacing='0'>  "
            StrSqlBody = StrSqlBody + "<tr style='background-color:#336699;height:20px;text-align:center;color:white'> "
            StrSqlBody = StrSqlBody + "<td><b>User Id</b></td> "
            StrSqlBody = StrSqlBody + "<td><b>User Name</b></td> "
            StrSqlBody = StrSqlBody + "<td><b>Company Name</b></td> "
            StrSqlBody = StrSqlBody + "</tr> "
            StrSqlBody = StrSqlBody + "<tr style='height:20px;text-align:center'> "
            StrSqlBody = StrSqlBody + "<td>" + ds.Tables(0).Rows(0).Item("USERID").ToString() + "</b></td> "
            StrSqlBody = StrSqlBody + "<td>" + ds.Tables(0).Rows(0).Item("USERNAME").ToString() + "</b></td> "
            StrSqlBody = StrSqlBody + "<td>" + ds.Tables(0).Rows(0).Item("COMPANY").ToString() + "</b></td> "
            StrSqlBody = StrSqlBody + "</tr> "
            StrSqlBody = StrSqlBody + "</table> "
            Return StrSqlBody
        Catch ex As Exception
            Return StrSqlBody
        End Try
    End Function
End Class
