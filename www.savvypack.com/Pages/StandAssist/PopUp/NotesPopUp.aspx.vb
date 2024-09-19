Imports System.Data
Imports System.Data.OleDb
Imports System
Imports StandGetData
Imports StandUpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_StandAssist_PopUp_NotesPopUp
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("Back") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If
            hidNote.Value = Request.QueryString("Id").ToString()
            hidGNote.Value = Request.QueryString("GId").ToString()
            hidType.Value = Request.QueryString("Type").ToString()
            

            If Not IsPostBack Then
                If Request.QueryString("File").ToString() = "NOTE" Then
                    GetNotes()
                Else
                    GetSpons()
                End If
                GetActivity()
            End If


        Catch ex As Exception

        End Try
    End Sub
    Protected Sub GetNotes()
        Dim objGetdata As New StandGetData.Selectdata()
        Dim objUpIns As New StandUpInsData.UpdateInsert
        Dim ds As New DataSet()
        Try
            ds = objGetdata.GetNotes(hidNote.Value.ToString(), hidGNote.Value.ToString(), hidType.Value.ToString())
            If hidType.Value = "BGNOTES" Then
                lblTitle.Text = "Notes for Public Group"
            ElseIf hidType.Value = "PNOTES" Then
                lblTitle.Text = "Notes for Proprietary Structure"
            ElseIf hidType.Value = "BNOTES" Then
                lblTitle.Text = "Notes for Public Structure"
            ElseIf hidType.Value = "GNOTES" Then
                lblTitle.Text = "Notes for Proprietary Group"
            ElseIf hidType.Value = "CNOTES" Then
                lblTitle.Text = "Notes for Company Structure"
            ElseIf hidType.Value = "CGNOTES" Then
                lblTitle.Text = "Notes for Company Group"
            End If
            If ds.Tables(0).Rows.Count > 0 Then
                If ds.Tables(0).Rows(0).Item("NOTES").ToString() <> "" Then
                    lblNotes.Text = ds.Tables(0).Rows(0).Item("NOTES").ToString()
                Else
                    lblNotes.Text = "No Notes Available."
                    lblNotes.Style.Add("text-align", "center")
                End If
            Else
                lblNotes.Text = "No Notes Available."
                lblNotes.Style.Add("text-align", "center")
            End If
            
        Catch ex As Exception
            Throw New Exception("GetNotes:" + ex.Message.ToString())
        End Try
    End Sub

    Protected Sub GetSpons()
        Dim objGetdata As New StandGetData.Selectdata()
        Dim objUpIns As New StandUpInsData.UpdateInsert
        Dim ds As New DataSet()
        Try
            If Request.QueryString("Spons").ToString() = "GRP" Then
                lblTitle.Text = "Sponsor Message for Public Group"
            ElseIf Request.QueryString("Spons").ToString() = "STRUCT" Then
                lblTitle.Text = "Sponsor Message for Public Structure"
            End If
            If Request.QueryString("File").ToString() = "N" Then
                lblNotes.Text = "No Sponsor Message Available."
                lblNotes.Style.Add("text-align", "center")
                If Request.QueryString("Spons").ToString() = "GRP" Then
                    If hidGNote.Value.ToString() = "0" Then
                        objUpIns.InsertLog1(Session("UserId").ToString(), "1", "Clicked to View Sponsor Message But No Public Group was Selected", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                    Else
                        objUpIns.InsertLog1(Session("UserId").ToString(), "1", "Clicked to View Sponsor Message for Public Group # " + hidGNote.Value.ToString() + "", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "" + hidGNote.Value.ToString() + "")
                    End If

                ElseIf Request.QueryString("Spons").ToString() = "STRUCT" Then
                    If hidNote.Value.ToString() = "0" Then
                        If hidGNote.Value.ToString() = "0" Then
                            objUpIns.InsertLog1(Session("UserId").ToString(), "1", "Clicked to View Sponsor Message But No Public Structure was Selected", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                        Else
                            objUpIns.InsertLog1(Session("UserId").ToString(), "1", "Clicked to View Sponsor Message But No Public Structure was Selected", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "" + hidGNote.Value.ToString() + "")
                        End If

                    Else
                        If hidGNote.Value.ToString() = "0" Then
                            objUpIns.InsertLog1(Session("UserId").ToString(), "1", "Clicked to View Sponsor Message for Public Structure # " + hidNote.Value.ToString() + "", "" + hidNote.Value.ToString() + "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                        Else
                            objUpIns.InsertLog1(Session("UserId").ToString(), "1", "Clicked to View Sponsor Message for Public Structure # " + hidNote.Value.ToString() + "", "" + hidNote.Value.ToString() + "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "" + hidGNote.Value.ToString() + "")
                        End If

                    End If

                End If
            ElseIf Request.QueryString("File").ToString() <> "NOTE" Then
                Dim Path As String = String.Empty
                Path = "../../../Images/Sponsors/" + Request.QueryString("File").ToString()
                If System.IO.File.Exists(Server.MapPath(Path)) Then
                   
                    If Request.QueryString("Spons").ToString() = "GRP" Then
                        If hidGNote.Value.ToString() = "0" Then
                            objUpIns.InsertLog1(Session("UserId").ToString(), "1", "Clicked to View Sponsor Message But No Public Group was Selected", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                        Else
                            objUpIns.InsertLog1(Session("UserId").ToString(), "1", "Clicked to View Sponsor Message for Public Group # " + hidGNote.Value.ToString() + "", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "" + hidGNote.Value.ToString() + "")
                        End If

                    ElseIf Request.QueryString("Spons").ToString() = "STRUCT" Then
                        If hidNote.Value.ToString() = "0" Then
                            If hidGNote.Value.ToString() = "0" Then
                                objUpIns.InsertLog1(Session("UserId").ToString(), "1", "Clicked to View Sponsor Message But No Public Structure was Selected", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                            Else
                                objUpIns.InsertLog1(Session("UserId").ToString(), "1", "Clicked to View Sponsor Message But No Public Structure was Selected", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "" + hidGNote.Value.ToString() + "")
                            End If

                        Else
                            If hidGNote.Value.ToString() = "0" Then
                                objUpIns.InsertLog1(Session("UserId").ToString(), "1", "Clicked to View Sponsor Message for Public Structure # " + hidNote.Value.ToString() + "", "" + hidNote.Value.ToString() + "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                            Else
                                objUpIns.InsertLog1(Session("UserId").ToString(), "1", "Clicked to View Sponsor Message for Public Structure # " + hidNote.Value.ToString() + "", "" + hidNote.Value.ToString() + "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "" + hidGNote.Value.ToString() + "")
                            End If

                        End If

                    End If
                    Response.Redirect(Path)
                    'Response.End()
                Else
                    lblNotes.Text = "File does not exists."
                    lblNotes.Style.Add("text-align", "center")
                    If Request.QueryString("Spons").ToString() = "GRP" Then
                        If hidGNote.Value.ToString() = "0" Then
                            objUpIns.InsertLog1(Session("UserId").ToString(), "1", "Clicked to View Sponsor Message But No Public Group was Selected", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                        Else
                            objUpIns.InsertLog1(Session("UserId").ToString(), "1", "Clicked to View Sponsor Message for Public Group # " + hidGNote.Value.ToString() + " But File does not exists", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "" + hidGNote.Value.ToString() + "")
                        End If

                    ElseIf Request.QueryString("Spons").ToString() = "STRUCT" Then
                        If hidNote.Value.ToString() = "0" Then
                            If hidGNote.Value.ToString() = "0" Then
                                objUpIns.InsertLog1(Session("UserId").ToString(), "1", "Clicked to View Sponsor Message But No Public Structure was Selected", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                            Else
                                objUpIns.InsertLog1(Session("UserId").ToString(), "1", "Clicked to View Sponsor Message But No Public Structure was Selected", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "" + hidGNote.Value.ToString() + "")
                            End If

                        Else
                            If hidGNote.Value.ToString() = "0" Then
                                objUpIns.InsertLog1(Session("UserId").ToString(), "1", "Clicked to View Sponsor Message for Public Structure # " + hidNote.Value.ToString() + " But File does not exists", "" + hidNote.Value.ToString() + "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                            Else
                                objUpIns.InsertLog1(Session("UserId").ToString(), "1", "Clicked to View Sponsor Message for Public Structure # " + hidNote.Value.ToString() + " But File does not exists", "" + hidNote.Value.ToString() + "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "" + hidGNote.Value.ToString() + "")
                            End If

                        End If

                    End If
                End If
                
                

            End If
            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", " OpenPDF('" + Path + "');", True)



        Catch ex As Exception
            Throw New Exception("GetSpons:" + ex.Message.ToString())
        End Try
    End Sub

    Protected Sub GetActivity()
        Dim objUpIns As New StandUpInsData.UpdateInsert
        Try
            If hidType.Value = "BGNOTES" Then
                If hidGNote.Value.ToString() = "0" Then
                    objUpIns.InsertLog1(Session("UserId").ToString(), "1", "Clicked to View Notes But No Public Group was Selected", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                Else
                    objUpIns.InsertLog1(Session("UserId").ToString(), "1", "Clicked to View Notes for Public Group # " + hidGNote.Value.ToString() + "", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "" + hidGNote.Value.ToString() + "")
                End If

            ElseIf hidType.Value = "PNOTES" Then
                If hidNote.Value.ToString() = "0" Then
                    If hidGNote.Value.ToString() = "0" Then
                        objUpIns.InsertLog1(Session("UserId").ToString(), "1", "Clicked to View Notes But No Proprietary Structure was Selected", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                    Else
                        objUpIns.InsertLog1(Session("UserId").ToString(), "1", "Clicked to View Notes But No Proprietary Structure was Selected", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "" + hidGNote.Value.ToString() + "")
                    End If
                Else
                    If hidGNote.Value.ToString() = "0" Then
                        objUpIns.InsertLog1(Session("UserId").ToString(), "1", "Clicked to View Notes for Proprietary Structure # " + hidNote.Value.ToString() + "", "" + hidNote.Value.ToString() + "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                    Else
                        objUpIns.InsertLog1(Session("UserId").ToString(), "1", "Clicked to View Notes for Proprietary Structure # " + hidNote.Value.ToString() + "", "" + hidNote.Value.ToString() + "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "" + hidGNote.Value.ToString() + "")
                    End If
                End If
            ElseIf hidType.Value = "CNOTES" Then
                If hidNote.Value.ToString() = "0" Then
                    If hidGNote.Value.ToString() = "0" Then
                        objUpIns.InsertLog1(Session("UserId").ToString(), "1", "Clicked to View Notes But No Company Structure was Selected", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                    Else
                        objUpIns.InsertLog1(Session("UserId").ToString(), "1", "Clicked to View Notes But No Company Structure was Selected", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "" + hidGNote.Value.ToString() + "")
                    End If
                Else
                    If hidGNote.Value.ToString() = "0" Then

                        objUpIns.InsertLog1(Session("UserId").ToString(), "1", "Clicked to View Notes for Company Structure # " + hidNote.Value.ToString() + "", "" + hidNote.Value.ToString() + "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                    Else
                        objUpIns.InsertLog1(Session("UserId").ToString(), "1", "Clicked to View Notes for Company Structure # " + hidNote.Value.ToString() + "", "" + hidNote.Value.ToString() + "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "" + hidGNote.Value.ToString() + "")

                    End If
                End If

            ElseIf hidType.Value = "BNOTES" Then
                If hidNote.Value.ToString() = "0" Then
                    If hidGNote.Value.ToString() = "0" Then
                        objUpIns.InsertLog1(Session("UserId").ToString(), "1", "Clicked to View Notes But No Public Structure was Selected", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                    Else
                        objUpIns.InsertLog1(Session("UserId").ToString(), "1", "Clicked to View Notes But No Public Structure was Selected", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "" + hidGNote.Value.ToString() + "")
                    End If
                Else
                    If hidGNote.Value.ToString() = "0" Then

                        objUpIns.InsertLog1(Session("UserId").ToString(), "1", "Clicked to View Notes for Public Structure # " + hidNote.Value.ToString() + "", "" + hidNote.Value.ToString() + "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                    Else
                        objUpIns.InsertLog1(Session("UserId").ToString(), "1", "Clicked to View Notes for Public Structure # " + hidNote.Value.ToString() + "", "" + hidNote.Value.ToString() + "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "" + hidGNote.Value.ToString() + "")

                    End If
                End If

            ElseIf hidType.Value = "GNOTES" Then
                If hidGNote.Value.ToString() = "0" Then
                    objUpIns.InsertLog1(Session("UserId").ToString(), "1", "Clicked to View Notes But No Proprietary Group was Selected", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                Else

                    objUpIns.InsertLog1(Session("UserId").ToString(), "1", "Clicked to View Notes for Proprietary Group # " + hidGNote.Value.ToString() + "", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "" + hidGNote.Value.ToString() + "")
                End If

            ElseIf hidType.Value = "CGNOTES" Then
                If hidGNote.Value.ToString() = "0" Then
                    objUpIns.InsertLog1(Session("UserId").ToString(), "1", "Clicked to View Notes But No Company Group was Selected", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                Else

                    objUpIns.InsertLog1(Session("UserId").ToString(), "1", "Clicked to View Notes for Company Group # " + hidGNote.Value.ToString() + "", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "" + hidGNote.Value.ToString() + "")
                End If

            End If

        Catch ex As Exception
            Throw New Exception("GetActivity:" + ex.Message.ToString())
        End Try
    End Sub

End Class
