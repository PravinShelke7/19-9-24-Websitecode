Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Net.Mail
Imports System.Net
Public Class WebConUpIns
    Public Class UpdateInsert
        Dim ShoppingConnection As String = System.Configuration.ConfigurationManager.AppSettings("ShoppingConnectionString")

        Public Sub InsertWebConfAttn(ByVal RefNo As String, ByVal WebConfID As String, ByVal UserId As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String
            Dim objUGetData As New UsersGetData.Selectdata()
            Dim dsUser As New DataSet()
            Try
                dsUser = objUGetData.GetUserDetails(UserId)
                StrSql = "INSERT INTO WEBCONFATTENDI  "
                StrSql = StrSql + "(REFNUMBER, WEBCONFID, USERCONTACTID,REGDATE) "
                StrSql = StrSql + "SELECT '" + RefNo + "','" + WebConfID + "'," + dsUser.Tables(0).Rows(0).Item("USERCONTACTID").ToString() + ",SYSDATE FROM DUAL "
                odbUtil.UpIns(StrSql, ShoppingConnection)

            Catch ex As Exception
                Throw New Exception("WebConUpIns:InsertWebConfAttn:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub InsertWebConfAttnd(ByVal RefNo As String, ByVal WebConfID As String, ByVal UserId As String, ByVal AddressId As String, ByVal seq As Integer)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String
            Dim objUGetData As New UsersGetData.Selectdata()
            Dim dsUser As New DataSet()
            Try
                dsUser = objUGetData.GetUserDetails(UserId)

                If seq = 1 Then
                    StrSql = " DELETE FROM WEBCONFATTENDI WHERE REFNUMBER ='" + RefNo + "' AND  WEBCONFID='" + WebConfID + "'"
                    odbUtil.UpIns(StrSql, ShoppingConnection)
                End If

                StrSql = ""
                StrSql = "INSERT INTO WEBCONFATTENDI  "
                StrSql = StrSql + "(REFNUMBER, WEBCONFID, USERCONTACTID,REGDATE,ADDRESSID,SEQ) "
                StrSql = StrSql + "SELECT '" + RefNo + "','" + WebConfID + "'," + dsUser.Tables(0).Rows(0).Item("USERCONTACTID").ToString() + ",SYSDATE,"
                StrSql = StrSql + AddressId.ToString() + "," + seq.ToString() + " FROM DUAL "            
                odbUtil.UpIns(StrSql, ShoppingConnection)

            Catch ex As Exception
                Throw New Exception("WebConUpIns:InsertWebConfAttn:" + ex.Message.ToString())
            End Try

        End Sub

        Public Function FreeMail(ByVal WebConfId As String, ByVal UserId As String, ByVal AttAdd() As String) As String
            Dim objUGetData As New UsersGetData.Selectdata()
            Dim objGetData As New WebConf.Selectdata()
            Dim dsUser As New DataSet()
            Dim dsWeb As New DataSet()
            Dim dsEmailConfig As New DataSet()
            Dim StrSqlBody As String = String.Empty
            Dim StrSqlBody1 As String = String.Empty
            Dim StrSqlBody2 As String = String.Empty

            Dim InnerHtml As String

            Try

                dsUser = objUGetData.GetUserDetails(UserId)
                dsEmailConfig = objUGetData.GetEmailConfigDetails("Y")
                dsWeb = objGetData.GetWebConfMailDetailsById(WebConfId)

                '


                'StrSqlBody1 = StrSqlBody1 + "<table cellpadding='0' cellspacing='0' style='width:650px'> "
                'StrSqlBody1 = StrSqlBody1 + "<tr> "
                'StrSqlBody1 = StrSqlBody1 + "<td><img src='" + dsEmailConfig.Tables(0).Rows(0).Item("URL") + "/Images/SavvyPackLogo3.gif' />"
                'StrSqlBody1 = StrSqlBody1 + "<div style='color:#336699;font-size:18px;font-family:Verdana;font-weight:bold;margin-left:5px'>SavvyPack Corporation Web Conference</div> "
                'StrSqlBody1 = StrSqlBody1 + "</td> "
                'StrSqlBody1 = StrSqlBody1 + "</tr> "
                'StrSqlBody1 = StrSqlBody1 + "<tr style='background-color:#336699;height:35px'> "
                'StrSqlBody1 = StrSqlBody1 + "<td> "
                'StrSqlBody1 = StrSqlBody1 + "<div style='color:white;font-size:15px;font-family:Verdana;font-weight:bold;margin-left:5px'>Conference Topic: " & dsWeb.Tables(0).Rows(0).Item("CONFTOPIC") & "</div> "
                'StrSqlBody1 = StrSqlBody1 + "</td> "
                'StrSqlBody1 = StrSqlBody1 + "</tr></table></br> "
                'StrSqlBody1 = StrSqlBody1 + "<div style='font-family:Verdana;font-size:12px;'>Hi&nbsp;" + dsUser.Tables(0).Rows(0).Item("PREFIX") + "&nbsp;" + dsUser.Tables(0).Rows(0).Item("FIRSTNAME") + "&nbsp;" + dsUser.Tables(0).Rows(0).Item("LASTNAME") + ",<br/><br/>Thank you for registering for an SavvyPack Corporation web conference. An email has been sent to you confirming your registration. <br />We also recommend you print this page for your records.</div><br/>"
                'StrSqlBody1 = StrSqlBody1 + "<div style='font-family:Verdana;font-size:15px;margin-top:10px;margin-bottom:10px;font-weight:bold'>Conference Details</div>"
                'StrSqlBody1 = StrSqlBody1 + "<table style='font-family:Verdana;width:700px;font-size:12px;border-collapse:collapse' border='1' bordercolor='black' cellpadding='0' cellspacing='0'>  "
                'StrSqlBody1 = StrSqlBody1 + "<tr style='background-color:#336699;height:20px;text-align:center;color:white'> "
                'StrSqlBody1 = StrSqlBody1 + "<td><b>Conference Date</b></td> "
                'StrSqlBody1 = StrSqlBody1 + "<td><b>Conference Time</b></td> "
                'StrSqlBody1 = StrSqlBody1 + "<td><b>Conference Topic</b></td> "
                'StrSqlBody1 = StrSqlBody1 + "</tr> "
                'StrSqlBody1 = StrSqlBody1 + "<tr style='height:20px;text-align:center'> "
                'StrSqlBody1 = StrSqlBody1 + "<td>" + dsWeb.Tables(0).Rows(0).Item("CONFDATE") + "</b></td> "
                'StrSqlBody1 = StrSqlBody1 + "<td>" + dsWeb.Tables(0).Rows(0).Item("CONFTIME") + "</b></td> "
                'StrSqlBody1 = StrSqlBody1 + "<td>" + dsWeb.Tables(0).Rows(0).Item("CONFTOPIC") + "</b></td> "
                'StrSqlBody1 = StrSqlBody1 + "</tr> "
                'StrSqlBody1 = StrSqlBody1 + "</table> "
                'StrSqlBody1 = StrSqlBody1 + "<div style='font-family:Verdana;font-size:15px;margin-top:10px;margin-bottom:10px;font-weight:bold'>Conference Credentials</div>"
                'StrSqlBody1 = StrSqlBody1 + "<table style='font-family:Verdana;width:600px;font-size:12px;border-collapse:collapse' border='1' bordercolor='black' cellpadding='0' cellspacing='0'>  "
                'StrSqlBody1 = StrSqlBody1 + "<tr style='background-color:#336699;height:20px;text-align:center;color:white'> "
                'StrSqlBody1 = StrSqlBody1 + "<td><b>" + dsWeb.Tables(0).Rows(0).Item("CONFUNAMETEXT") + "</b></td> "
                'StrSqlBody1 = StrSqlBody1 + "<td><b>" + dsWeb.Tables(0).Rows(0).Item("CONFPWDTEXT") + "</b></td> "
                'StrSqlBody1 = StrSqlBody1 + "<td><b>Conference Phone No.</b></td> "
                'StrSqlBody1 = StrSqlBody1 + "<td><b>Conference Access Code</b></td> "
                'StrSqlBody1 = StrSqlBody1 + "</tr> "
                'StrSqlBody1 = StrSqlBody1 + "<tr style='height:20px;text-align:center'> "
                'StrSqlBody1 = StrSqlBody1 + "<td>" + dsWeb.Tables(0).Rows(0).Item("CONFID") + "</b></td> "
                'StrSqlBody1 = StrSqlBody1 + "<td>" + dsWeb.Tables(0).Rows(0).Item("CONFKEY") + "</b></td> "
                'StrSqlBody1 = StrSqlBody1 + "<td>" + dsWeb.Tables(0).Rows(0).Item("CONFPHONE") + "</b></td> "
                'StrSqlBody1 = StrSqlBody1 + "<td>" + dsWeb.Tables(0).Rows(0).Item("CONFACCESSCODE") + "</b></td> "
                'StrSqlBody1 = StrSqlBody1 + "</tr> "
                'StrSqlBody1 = StrSqlBody1 + "</table> "


                'StrSqlBody2 = StrSqlBody2 + "<table cellpadding='0' cellspacing='0' style='width:650px'> "
                'StrSqlBody2 = StrSqlBody2 + "<tr> "
                'StrSqlBody2 = StrSqlBody2 + "<td><img src='../Images/SavvyPackLogo3.gif' /> "
                'StrSqlBody2 = StrSqlBody2 + "<div style='color:#336699;font-size:18px;font-family:Verdana;font-weight:bold;margin-left:5px'>Web Conference</div> "
                'StrSqlBody2 = StrSqlBody2 + "</td> "
                'StrSqlBody2 = StrSqlBody2 + "</tr> "
                'StrSqlBody2 = StrSqlBody2 + "<tr style='background-color:#336699;height:35px'> "
                'StrSqlBody2 = StrSqlBody2 + "<td> "
                'StrSqlBody2 = StrSqlBody2 + "<div style='color:white;font-size:15px;font-family:Verdana;font-weight:bold;margin-left:5px'>Web conference: " + dsWeb.Tables(0).Rows(0).Item("CONFTOPIC") + "</div> "
                'StrSqlBody2 = StrSqlBody2 + "</td> "
                'StrSqlBody2 = StrSqlBody2 + "</tr></table></br> "
                'StrSqlBody2 = StrSqlBody2 + "<div style='font-family:Verdana;font-size:12px;'>Thank you&nbsp;" + dsUser.Tables(0).Rows(0).Item("Prefix") + "&nbsp;" + dsUser.Tables(0).Rows(0).Item("FirstName") + "<br/><br/>Thank you for registering for an SavvyPack Corporation web conference. Your order confirmation will be emailed to you after finalization of payment.</div><br/>"




                If dsWeb.Tables(0).Rows(0).Item("CONFCOST").ToString() = 0 Then
                    InnerHtml = StrSqlBody1
                    SendWebConfDetails(dsWeb, dsUser, dsEmailConfig, AttAdd)
                    SendWebConfDetails1(dsWeb, dsUser, dsEmailConfig, AttAdd)
                Else
                    InnerHtml = StrSqlBody2
                End If




            Catch ex As Exception
                Throw New Exception("ShopUpInsData:PreSaleMail:" + ex.Message.ToString())
            End Try
            Return InnerHtml
        End Function

        Public Sub PaidMail(ByVal WebConfId As String, ByVal UserId As String, ByVal attendeeId() As String)

            Dim objUGetData As New UsersGetData.Selectdata()
            Dim objGetData As New WebConf.Selectdata()
            Dim dsUser As New DataSet()
            Dim dsWeb As New DataSet()
            Dim dsEmailConfig As New DataSet()
            Try
                dsUser = objUGetData.GetUserDetails(UserId)
                dsEmailConfig = objUGetData.GetEmailConfigDetails("Y")
                dsWeb = objGetData.GetWebConfMailDetailsById(WebConfId)
                SendWebConfDetails(dsWeb, dsUser, dsEmailConfig, attendeeId)
                SendWebConfDetails1(dsWeb, dsUser, dsEmailConfig, attendeeId)
            Catch ex As Exception
                Throw New Exception("WebConUpIns:PaidMail:" + ex.Message.ToString())
            End Try
        End Sub


        Public Sub SendWebConfDetails(ByVal dsWeb As DataSet, ByVal dsUser As DataSet, ByVal dsEmailConfig As DataSet, ByVal attendeeId() As String)
            Try
                Dim _To As New MailAddressCollection()
                Dim _CC As New MailAddressCollection()
                Dim _BCC As New MailAddressCollection()
                Dim Item As MailAddress
                Dim Email As New EmailConfig()
                Dim dsAttendeeUser As New DataSet
                Dim i As Integer = 0
                Dim flag As Boolean
                Dim objUGetData As New UsersGetData.Selectdata()
                Dim StrSqlBody As String = String.Empty
                Dim dsAttendee As New DataSet()


                StrSqlBody = StrSqlBody & "<table cellpadding='0' cellspacing='0' style='width:650px'> "
                StrSqlBody = StrSqlBody & "<tr> "
                StrSqlBody = StrSqlBody & "<td><img src='" + dsEmailConfig.Tables(0).Rows(0).Item("URL") + "/Images/SavvyPackLogo3.gif' />"
                StrSqlBody = StrSqlBody & "<div style='color:#336699;font-size:18px;font-family:Verdana;font-weight:bold;margin-left:5px'>SavvyPack Corporation Web Conference</div> "
                StrSqlBody = StrSqlBody & "</td> "
                StrSqlBody = StrSqlBody & "</tr> "
                StrSqlBody = StrSqlBody & "<tr style='background-color:#336699;height:35px'> "
                StrSqlBody = StrSqlBody & "<td> "
                StrSqlBody = StrSqlBody & "<div style='color:white;font-size:15px;font-family:Verdana;font-weight:bold;margin-left:5px'>Conference Topic: " & dsWeb.Tables(0).Rows(0).Item("CONFTOPIC") & "</div> "
                StrSqlBody = StrSqlBody & "</td> "
                StrSqlBody = StrSqlBody & "</tr></table></br> "
                StrSqlBody = StrSqlBody + "<div style='font-family:Verdana;font-size:12px;'>Hi&nbsp;" + dsUser.Tables(0).Rows(0).Item("PREFIX") + "&nbsp;" + dsUser.Tables(0).Rows(0).Item("FIRSTNAME") + "&nbsp;" + dsUser.Tables(0).Rows(0).Item("LASTNAME") + ",<br/><br/>Thank you for registering the following people for the web conference. We will send an email to each of them confirming their registration. <br /></div><br/>"


                StrSqlBody = StrSqlBody + "<div style='font-family:Verdana;font-size:15px;margin-top:10px;margin-bottom:10px;font-weight:bold'>Conference Attendees</div>"
                StrSqlBody = StrSqlBody + "<table style='font-family:Verdana;width:650px;font-size:12px;border-collapse:collapse' border='1' bordercolor='black' cellpadding='0' cellspacing='0'>  "
                StrSqlBody = StrSqlBody + "<tr style='background-color:#336699;height:20px;text-align:center;color:white'> "
                StrSqlBody = StrSqlBody + "<td><b>User Name </b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Email Address</b></td> "
                StrSqlBody = StrSqlBody + "</tr> "
                For i = 0 To 10
                    If attendeeId(i) <> "0" And attendeeId(i) <> Nothing Then
                        dsAttendee = objUGetData.GetBillToShipToUserDetailsByAddID(attendeeId(i))
                        StrSqlBody = StrSqlBody + "<tr style='height:20px;text-align:Left ;margin-left:10px;'> "
                        StrSqlBody = StrSqlBody + "<td>" + dsAttendee.Tables(0).Rows(0).Item("FULLNAME") + "</b></td> "
                        StrSqlBody = StrSqlBody + "<td>" + dsAttendee.Tables(0).Rows(0).Item("EMAILADDRESS") + "</b></td> "
                        StrSqlBody = StrSqlBody + "</tr> "
                    End If
                Next

                StrSqlBody = StrSqlBody + "</table> "

                StrSqlBody = StrSqlBody & "<div style='font-family:Verdana;font-size:15px;margin-top:10px;margin-bottom:10px;font-weight:bold'>Conference Details</div>"
                StrSqlBody = StrSqlBody & "<table style='font-family:Verdana;width:650px;font-size:12px;border-collapse:collapse' border='1' bordercolor='black' cellpadding='0' cellspacing='0'>  "
                StrSqlBody = StrSqlBody & "<tr style='background-color:#336699;height:20px;text-align:center;color:white'> "
                StrSqlBody = StrSqlBody & "<td><b>Conference Date</b></td> "
                StrSqlBody = StrSqlBody & "<td><b>Conference Time</b></td> "
                StrSqlBody = StrSqlBody & "<td><b>Conference Topic</b></td> "
                StrSqlBody = StrSqlBody & "</tr> "
                StrSqlBody = StrSqlBody & "<tr style='height:20px;text-align:center'> "
                StrSqlBody = StrSqlBody & "<td>" + dsWeb.Tables(0).Rows(0).Item("CONFDATE") + "</b></td> "
                StrSqlBody = StrSqlBody & "<td>" + dsWeb.Tables(0).Rows(0).Item("CONFTIME") + "</b></td> "
                StrSqlBody = StrSqlBody & "<td>" + dsWeb.Tables(0).Rows(0).Item("CONFTOPIC") + "</b></td> "
                StrSqlBody = StrSqlBody & "</tr> "
                StrSqlBody = StrSqlBody & "</table> "
                StrSqlBody = StrSqlBody & "<div style='font-family:Verdana;font-size:15px;margin-top:10px;margin-bottom:10px;font-weight:bold'>Conference Credentials</div>"
                StrSqlBody = StrSqlBody & "<table style='font-family:Verdana;width:650px;font-size:12px;border-collapse:collapse' border='1' bordercolor='black' cellpadding='0' cellspacing='0'>  "
                StrSqlBody = StrSqlBody & "<tr style='background-color:#336699;height:20px;text-align:center;color:white'> "
                StrSqlBody = StrSqlBody & "<td><b>" + dsWeb.Tables(0).Rows(0).Item("CONFUNAMETEXT") + "</b></td> "
                StrSqlBody = StrSqlBody & "<td><b>" + dsWeb.Tables(0).Rows(0).Item("CONFPWDTEXT") + "</b></td> "
                StrSqlBody = StrSqlBody & "<td><b>Conference Phone No.</b></td> "
                StrSqlBody = StrSqlBody & "<td><b>Conference Access Code</b></td> "
                StrSqlBody = StrSqlBody & "</tr> "
                StrSqlBody = StrSqlBody & "<tr style='height:20px;text-align:center'> "
                StrSqlBody = StrSqlBody & "<td>" + dsWeb.Tables(0).Rows(0).Item("CONFID") + "</b></td> "
                StrSqlBody = StrSqlBody & "<td>" + dsWeb.Tables(0).Rows(0).Item("CONFKEY") + "</b></td> "
                StrSqlBody = StrSqlBody & "<td>" + dsWeb.Tables(0).Rows(0).Item("CONFPHONE") + "</b></td> "
                StrSqlBody = StrSqlBody & "<td>" + dsWeb.Tables(0).Rows(0).Item("CONFACCESSCODE") + "</b></td> "
                StrSqlBody = StrSqlBody & "</tr> "
                StrSqlBody = StrSqlBody & "</table> "
                StrSqlBody = StrSqlBody & "<br/><br/><br/><div style='font-family:Verdana;font-size:12px;'>SavvyPack Corporation<br/>1850 East 121st Street Suite 106B<br/>Burnsville, MN USA 55337<br/><a style='font-family:verdana;' href='www.savvypack.com'>www.savvypack.com</a><br/>Phone: 1-952-405-7500</div>"


                Dim objGetData As New UsersGetData.Selectdata
                Dim ds As New DataSet
                ds = objGetData.GetAlliedMemberMail("WEBCONF")

                If ds.Tables(0).Rows.Count > 0 Then
                    Dim _From As New MailAddress(ds.Tables(0).Rows(0).Item("FROMADD").ToString(), ds.Tables(0).Rows(0).Item("FROMNAME").ToString())
                    Dim _Subject As String = ds.Tables(0).Rows(0).Item("SUBJECT").ToString()

                    If flag = False Then
                        Item = New MailAddress(dsUser.Tables(0).Rows(0).Item("EMAILADDRESS"), dsUser.Tables(0).Rows(0).Item("FIRSTNAME"))
                        _To.Add(Item)
                        flag = True
                    End If

                    'BCC's

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

                    Email.SendMail(_From, _To, _CC, _BCC, StrSqlBody, _Subject)
                End If



            Catch ex As Exception
                Throw New Exception("WebConUpIns:SendWebConfDetails:" + ex.Message.ToString())
            End Try




        End Sub
        Public Sub SendWebConfDetails1(ByVal dsWeb As DataSet, ByVal dsUser As DataSet, ByVal dsEmailConfig As DataSet, ByVal attendeeId() As String)
            Try
                Dim _To As New MailAddressCollection()
                Dim _CC As New MailAddressCollection()
                Dim _BCC As New MailAddressCollection()
                Dim Item As MailAddress
                Dim Email As New EmailConfig()
                Dim dsAttendeeUser As New DataSet
                Dim i As Integer = 0
                Dim j As Integer = 0
                Dim flag As Boolean
                Dim objUGetData As New UsersGetData.Selectdata()
                Dim StrSqlBody As String = String.Empty
                Dim dsAttendee As New DataSet()

                For j = 0 To 10
                    If attendeeId(j) <> "0" And attendeeId(j) <> Nothing Then
                        dsAttendee = objUGetData.GetBillToShipToUserDetailsByAddID(attendeeId(j))
                        flag = False
                        StrSqlBody = ""
                        StrSqlBody = StrSqlBody & "<table cellpadding='0' cellspacing='0' style='width:650px'> "
                        StrSqlBody = StrSqlBody & "<tr> "
                        StrSqlBody = StrSqlBody & "<td><img src='" + dsEmailConfig.Tables(0).Rows(0).Item("URL") + "/Images/SavvyPackLogo3.gif' />"
                        StrSqlBody = StrSqlBody & "<div style='color:#336699;font-size:18px;font-family:Verdana;font-weight:bold;margin-left:5px'>SavvyPack Corporation Web Conference</div> "
                        StrSqlBody = StrSqlBody & "</td> "
                        StrSqlBody = StrSqlBody & "</tr> "
                        StrSqlBody = StrSqlBody & "<tr style='background-color:#336699;height:35px'> "
                        StrSqlBody = StrSqlBody & "<td> "
                        StrSqlBody = StrSqlBody & "<div style='color:white;font-size:15px;font-family:Verdana;font-weight:bold;margin-left:5px'>Conference Topic: " & dsWeb.Tables(0).Rows(0).Item("CONFTOPIC") & "</div> "
                        StrSqlBody = StrSqlBody & "</td> "
                        StrSqlBody = StrSqlBody & "</tr></table></br> "
                        If dsUser.Tables(0).Rows(0).Item("EMAILADDRESS").ToString().ToUpper() = dsAttendee.Tables(0).Rows(0).Item("EMAILADDRESS").ToString().ToUpper() Then
                            StrSqlBody = StrSqlBody + "<div style='font-family:Verdana;font-size:12px;'>Hi&nbsp;" + dsUser.Tables(0).Rows(0).Item("PREFIX") + "&nbsp;" + dsUser.Tables(0).Rows(0).Item("FIRSTNAME") + "&nbsp;" + dsUser.Tables(0).Rows(0).Item("LASTNAME") + ",<br/><br/> Thank you for registering for an SavvyPack Corporation web conference.</div><br/>"

                        Else
                            StrSqlBody = StrSqlBody + "<div style='font-family:Verdana;font-size:12px;'>Hi&nbsp;" + dsAttendee.Tables(0).Rows(0).Item("PREFIX") + "&nbsp;" + dsAttendee.Tables(0).Rows(0).Item("FIRSTNAME") + "&nbsp;" + dsAttendee.Tables(0).Rows(0).Item("LASTNAME") + ",<br/><br/>"
                            StrSqlBody = StrSqlBody & "" + dsUser.Tables(0).Rows(0).Item("PREFIX") + "&nbsp;" + dsUser.Tables(0).Rows(0).Item("FIRSTNAME") + "&nbsp;" + dsUser.Tables(0).Rows(0).Item("LASTNAME") + " has registered you for an SavvyPack Corporation web conference.</div><br/>"
                        End If
                        StrSqlBody = StrSqlBody & "<div style='font-family:Verdana;font-size:12px;'>Starting 30 minutes before the conference, you can click on the following link to join the conference:<br/><a style='font-family:verdana;' href='" + dsWeb.Tables(0).Rows(0).Item("CONFJOINLINK") + "'>Join conference</a><br/><br/> Or you can paste the following link into your browser:<br/><b>" + dsWeb.Tables(0).Rows(0).Item("CONFMANLINK") + "</b> <br/>"
                        'StrSqlBody = StrSqlBody & "Then click join and enter the conference key and your email address.</br></div><br/>"
                        'StrSqlBody = StrSqlBody & "Then click join meeting (do not try to login) and enter the conference credentials.</br></div><br/>"
                        If dsWeb.Tables(0).Rows(0).Item("CONFTYPE") = "O" Then
                            StrSqlBody = StrSqlBody & "Then click join and enter the conference key and your email address.</br></div><br/>"
                        ElseIf dsWeb.Tables(0).Rows(0).Item("CONFTYPE") = "M" Then
                            StrSqlBody = StrSqlBody & "Then click join meeting (do not try to login) and enter the conference credentials.</br></div><br/>"
                        End If

                        StrSqlBody = StrSqlBody + "</tr> "


                        ''StrSqlBody = StrSqlBody + "</table> "

                        StrSqlBody = StrSqlBody & "<div style='font-family:Verdana;font-size:15px;margin-top:10px;margin-bottom:10px;font-weight:bold'>Conference Details</div>"
                        StrSqlBody = StrSqlBody & "<table style='font-family:Verdana;width:650px;font-size:12px;border-collapse:collapse' border='1' bordercolor='black' cellpadding='0' cellspacing='0'>  "
                        StrSqlBody = StrSqlBody & "<tr style='background-color:#336699;height:20px;text-align:center;color:white'> "
                        StrSqlBody = StrSqlBody & "<td><b>Conference Date</b></td> "
                        StrSqlBody = StrSqlBody & "<td><b>Conference Time</b></td> "
                        StrSqlBody = StrSqlBody & "<td><b>Conference Topic</b></td> "
                        StrSqlBody = StrSqlBody & "</tr> "
                        StrSqlBody = StrSqlBody & "<tr style='height:20px;text-align:center'> "
                        StrSqlBody = StrSqlBody & "<td>" + dsWeb.Tables(0).Rows(0).Item("CONFDATE") + "</b></td> "
                        StrSqlBody = StrSqlBody & "<td>" + dsWeb.Tables(0).Rows(0).Item("CONFTIME") + "</b></td> "
                        StrSqlBody = StrSqlBody & "<td>" + dsWeb.Tables(0).Rows(0).Item("CONFTOPIC") + "</b></td> "
                        StrSqlBody = StrSqlBody & "</tr> "
                        StrSqlBody = StrSqlBody & "</table> "
                        StrSqlBody = StrSqlBody & "<div style='font-family:Verdana;font-size:15px;margin-top:10px;margin-bottom:10px;font-weight:bold'>Conference Credentials</div>"
                        StrSqlBody = StrSqlBody & "<table style='font-family:Verdana;width:650px;font-size:12px;border-collapse:collapse' border='1' bordercolor='black' cellpadding='0' cellspacing='0'>  "
                        StrSqlBody = StrSqlBody & "<tr style='background-color:#336699;height:20px;text-align:center;color:white'> "
                        StrSqlBody = StrSqlBody & "<td><b>" + dsWeb.Tables(0).Rows(0).Item("CONFUNAMETEXT") + "</b></td> "
                        StrSqlBody = StrSqlBody & "<td><b>" + dsWeb.Tables(0).Rows(0).Item("CONFPWDTEXT") + "</b></td> "
                        StrSqlBody = StrSqlBody & "<td><b>Conference Phone No.</b></td> "
                        StrSqlBody = StrSqlBody & "<td><b>Conference Access Code</b></td> "
                        StrSqlBody = StrSqlBody & "</tr> "
                        StrSqlBody = StrSqlBody & "<tr style='height:20px;text-align:center'> "
                        StrSqlBody = StrSqlBody & "<td>" + dsWeb.Tables(0).Rows(0).Item("CONFID") + "</b></td> "
                        StrSqlBody = StrSqlBody & "<td>" + dsWeb.Tables(0).Rows(0).Item("CONFKEY") + "</b></td> "
                        StrSqlBody = StrSqlBody & "<td>" + dsWeb.Tables(0).Rows(0).Item("CONFPHONE") + "</b></td> "
                        StrSqlBody = StrSqlBody & "<td>" + dsWeb.Tables(0).Rows(0).Item("CONFACCESSCODE") + "</b></td> "
                        StrSqlBody = StrSqlBody & "</tr> "
                        StrSqlBody = StrSqlBody & "</table> "
                        StrSqlBody = StrSqlBody & "<br/><br/><br/><div style='font-family:Verdana;font-size:12px;'>SavvyPack Corporation<br/>1850 East 121st Street Suite 106B<br/>Burnsville, MN USA 55337<br/><a style='font-family:verdana;' href='www.savvypack.com'>www.savvypack.com</a><br/>Phone: 1-952-405-7500</div>"



                        Dim objGetData As New UsersGetData.Selectdata
                        Dim ds As New DataSet
                        ds = objGetData.GetAlliedMemberMail("WEBCONF")

                        If ds.Tables(0).Rows.Count > 0 Then
                            Dim _From As New MailAddress(ds.Tables(0).Rows(0).Item("FROMADD").ToString(), ds.Tables(0).Rows(0).Item("FROMNAME").ToString())
                            Dim _Subject As String = ds.Tables(0).Rows(0).Item("SUBJECT").ToString()



                            If flag = False Then
                                _To.Clear()
                                Item = New MailAddress(dsAttendee.Tables(0).Rows(0).Item("EMAILADDRESS"), dsAttendee.Tables(0).Rows(0).Item("FIRSTNAME"))
                                _To.Add(Item)
                                flag = True
                            End If







                            'BCC's
                            _BCC.Clear()
                            _CC.Clear()
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

                            Email.SendMail(_From, _To, _CC, _BCC, StrSqlBody, _Subject)
                        End If


                    End If
                Next


            Catch ex As Exception
                Throw New Exception("WebConUpIns:SendWebConfDetails:" + ex.Message.ToString())
            End Try




        End Sub


    End Class

End Class
