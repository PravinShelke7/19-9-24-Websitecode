Imports Microsoft.VisualBasic
Imports System.Net.Mail
Imports UsersGetData
Imports System.Data
Imports System.Net

Public Class EmailConfig

    Public Sub SendMail(ByVal _From As MailAddress, ByVal _To As MailAddressCollection, ByVal _CC As MailAddressCollection, ByVal _BCC As MailAddressCollection, ByVal _Body As String, ByVal _Subject As String)
        Dim ds As New DataSet()
        Dim objGetData As New Selectdata()
        Dim message As New MailMessage()
        Dim SMTP As New SmtpClient()
        Dim SMTPUserInfo As New NetworkCredential()
        Dim i As New Integer
        Try


            'Adding From
            message.From = _From


            'Adding To's 
            For i = 0 To _To.Count - 1
                message.To.Add(_To.Item(i))
            Next



            'Adding CC's 
            For i = 0 To _CC.Count - 1
               message.CC.Add(_CC.Item(i))
            Next

            'Adding BCC's 
            For i = 0 To _BCC.Count - 1
               message.Bcc.Add(_BCC.Item(i))
            Next

            'Subject
            message.Subject = _Subject

            'Body
            message.IsBodyHtml = True
            message.Body = _Body.ToString()

            Try
                ds = objGetData.GetEmailConfigDetailsByOrder()
                If ds.Tables(0).Rows.Count > 0 Then
                    For i = 0 To ds.Tables(0).Rows.Count - 1
                        Try
                            'Smtp Setting
                            SMTP.Host = ds.Tables(0).Rows(i).Item("SMTP").ToString()

                            SMTP.Port = ds.Tables(0).Rows(i).Item("SMTPPORT").ToString()

                            If ds.Tables(0).Rows(i).Item("SMTPISSSL").ToString() = "Y" Then
                                SMTP.EnableSsl = True
                            Else
                                SMTP.EnableSsl = False
                            End If


                            If ds.Tables(0).Rows(i).Item("ISAUNTH").ToString() = "Y" Then
                                SMTP.UseDefaultCredentials = False
                                SMTPUserInfo.UserName = ds.Tables(0).Rows(i).Item("SMTPUNAME").ToString()
                                SMTPUserInfo.Password = ds.Tables(0).Rows(i).Item("SMTPPWD").ToString()
                                SMTP.Credentials = SMTPUserInfo
                            End If

                            'Send Mail
                            SMTP.Send(message)
                            Exit For
                        Catch ex As Exception
                        End Try
                    Next
                End If
            Catch ex As Exception

            End Try

            'Try
            '    'Smtp Setting
            '    ds = objGetData.GetEmailConfigDetails("Y")
            '    SMTP.Host = ds.Tables(0).Rows(0).Item("SMTP").ToString()

            '    SMTP.Port = ds.Tables(0).Rows(0).Item("SMTPPORT").ToString()

            '    If ds.Tables(0).Rows(0).Item("SMTPISSSL").ToString() = "Y" Then
            '        SMTP.EnableSsl = True
            '    End If


            '    If ds.Tables(0).Rows(0).Item("ISAUNTH").ToString() = "Y" Then
            '        SMTP.UseDefaultCredentials = False
            '        SMTPUserInfo.UserName = ds.Tables(0).Rows(0).Item("SMTPUNAME").ToString()
            '        SMTPUserInfo.Password = ds.Tables(0).Rows(0).Item("SMTPPWD").ToString()
            '        SMTP.Credentials = SMTPUserInfo
            '    End If

            '    'Send Mail
            '    SMTP.Send(message)
            'Catch ex As Exception
            '    'Smtp Setting
            '    ds = objGetData.GetEmailConfigDetails("N")
            '    SMTP.Host = ds.Tables(0).Rows(0).Item("SMTP").ToString()

            '    SMTP.Port = ds.Tables(0).Rows(0).Item("SMTPPORT").ToString()

            '    If ds.Tables(0).Rows(0).Item("SMTPISSSL").ToString() = "Y" Then
            '        SMTP.EnableSsl = True
            '    End If


            '    If ds.Tables(0).Rows(0).Item("ISAUNTH").ToString() = "Y" Then
            '        SMTP.UseDefaultCredentials = False
            '        SMTPUserInfo.UserName = ds.Tables(0).Rows(0).Item("SMTPUNAME").ToString()
            '        SMTPUserInfo.Password = ds.Tables(0).Rows(0).Item("SMTPPWD").ToString()
            '        SMTP.Credentials = SMTPUserInfo
            '    End If

            '    'Send Mail
            '    Try
            '        SMTP.Send(message)
            '    Catch ex1 As Exception
            '    End Try
            'End Try


        Catch ex As Exception
            Throw New Exception("EmailConfig:SendMail" + ex.Message.ToString() + "")
        End Try
    End Sub

End Class
