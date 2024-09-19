Imports System
Imports System.Data
Imports System.Net.Mail
Partial Class Structure_Assistant
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            lblTag.Text = "Copyright 1997 - " + Now.Year().ToString() + " SavvyPack® Corporation"
        Catch ex As Exception
            Response.Write("Page_Load" + ex.Message.ToString())
        End Try
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try
            Dim dsUserData As New DataSet
            Dim objUpdataData As New UsersUpdateData.UpdateInsert
            Dim strBody As String = ""
            Dim strBodyUser As String = ""

            strBody = GetEmailBodyData(txtFName.Text, txtLName.Text, txtEmail.Text)
            Try
                objUpdataData.InsertSAUserDetails(txtEmail.Text.Trim(), txtFName.Text.Trim(), txtLName.Text.Trim())

                SendEmail(strBody, txtEmail.Text, txtFName.Text)
                txtFName.Text = ""
                txtLName.Text = ""
                txtEmail.Text = ""
            Catch ex As Exception

            End Try

            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Thank you for contacting us. Your request has been recieved.');", True)

        Catch ex As Exception
            Response.Write("btnSubmit_Click" + ex.Message.ToString())
        End Try
    End Sub

    Protected Function GetEmailBodyData(ByVal fName As String, ByVal lName As String, ByVal EMAILADDRESS As String) As String
        Dim StrSqlBody As String = ""
        Try

            Dim dsEmail As New DataSet
            Dim objgetData As New UsersGetData.Selectdata
            dsEmail = objgetData.GetEmailConfigDetails("Y")
            StrSqlBody = "<div style='font-family:Verdana;'>  "
            StrSqlBody = StrSqlBody + "<table cellpadding='0' cellspacing='0' style='width:650px'> "
            StrSqlBody = StrSqlBody + "<tr> "
            StrSqlBody = StrSqlBody + "<td><img src='" + dsEmail.Tables(0).Rows(0).Item("URL").ToString() + "/Images/SavvyPackLogo3.gif' /> "
            StrSqlBody = StrSqlBody + "<br /> "
            StrSqlBody = StrSqlBody + "</td> "
            StrSqlBody = StrSqlBody + "</tr> "
            StrSqlBody = StrSqlBody + "<tr style='background-color:#336699;height:35px;'> "
            StrSqlBody = StrSqlBody + "<td> "
            StrSqlBody = StrSqlBody + "<div style='color:white;font-size:18px;font-family:verdana;font-weight:bold;margin-left:5px;'><b>Contact Request Confirmation:</b> </div> "
            StrSqlBody = StrSqlBody + "</td> "
            StrSqlBody = StrSqlBody + "</tr> "
            StrSqlBody = StrSqlBody + "</table> "
            StrSqlBody = StrSqlBody + "<div style='margin-top:10px;margin-right:10px;font-family:Verdana;font-size:12px;'> "
            StrSqlBody = StrSqlBody + "<p>Dear " + fName + " " + lName + ",<br/><br/>   Thank you for contacting us regarding your interest in SavvyPack® Structure Assistant. We have received your request and will respond within 24 hours. </p>"
            StrSqlBody = StrSqlBody + "<p> "
            StrSqlBody = StrSqlBody + "<div style='font-family:Verdana;font-size:12px;'> "
            StrSqlBody = StrSqlBody + "Regards,<br/> "
            StrSqlBody = StrSqlBody + "SavvyPack Corporation Sales Team<br/><br/>SavvyPack Corporation<br/>1850 East 121st Street Suite 106B<br/>Burnsville, MN USA 55044<br/><a style='font-family:verdana;' href='http://www.savvypack.com'>www.savvypack.com</a><br/>Phone: 1-952-405-7500</div> "
            StrSqlBody = StrSqlBody + "</p> "
            StrSqlBody = StrSqlBody + "</div> "


            Return StrSqlBody
        Catch ex As Exception
            Return StrSqlBody
            Response.Write("GetEmailBodyData" + ex.Message.ToString())
        End Try
    End Function

    Public Sub SendEmail(ByVal strBody As String, ByVal EmailAdd As String, ByVal FirstName As String)
        Try
            Dim objGetData As New UsersGetData.Selectdata
            Dim ds As New DataSet

            ds = objGetData.GetAlliedMemberMail("CONREQ")
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
                Item = New MailAddress(EmailAdd, FirstName)
                _To.Add(Item)

                Item = New MailAddress("sales@savvypack.com", "Sales")
                _CC.Add(Item)

                For i = 1 To 10
                    ' BCC() 's
                    If ds.Tables(0).Rows(0).Item("BCC" + i.ToString()).ToString() <> Nothing Then
                        Item = New MailAddress(ds.Tables(0).Rows(0).Item("BCC" + i.ToString()).ToString(), ds.Tables(0).Rows(0).Item("BCC" + i.ToString() + "NAME").ToString())
                        _BCC.Add(Item)
                    End If
                    If ds.Tables(0).Rows(0).Item("CC" + i.ToString()).ToString() <> Nothing Then
                        ' Item = New MailAddress(ds.Tables(0).Rows(0).Item("CC" + i.ToString()).ToString(), ds.Tables(0).Rows(0).Item("CC" + i.ToString() + "NAME").ToString())
                        '_CC.Add(Item)
                    End If

                Next

                Email.SendMail(_From, _To, _CC, _BCC, strBody, _Subject)
            End If
        Catch ex As Exception
            Response.Write("SendEmail" + ex.Message.ToString())
        End Try
    End Sub
End Class

