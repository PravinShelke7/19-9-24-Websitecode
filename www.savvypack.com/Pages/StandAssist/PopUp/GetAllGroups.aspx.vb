Imports System.Data
Imports System.Data.OleDb
Imports System
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports StandGetData
Imports StandUpInsData
Partial Class Pages_StandAssist_PopUp_GetAllGroups
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("Back") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If

            hidgrpDes.Value = Request.QueryString("Des").ToString()
            hidgrpId.Value = Request.QueryString("Id").ToString()
            hidGrpIdD.Value = Request.QueryString("IdD").ToString()

            If Request.QueryString("PTYPE") <> Nothing Then
                hidPtype.Value = Request.QueryString("PTYPE").ToString()
            End If
            If Request.QueryString("File") <> Nothing Then
                hidFile.Value = Request.QueryString("File").ToString()
            End If

            If Request.QueryString("Notes") <> Nothing Then
                hidGNotes.Value = Request.QueryString("Notes").ToString()
            End If
            If Request.QueryString("SNotes") <> Nothing Then
                hidSNotes.Value = Request.QueryString("SNotes").ToString()

            End If

            Dim objUpIns As New StandUpInsData.UpdateInsert()
            If Not IsPostBack Then
                hvUserGrd.Value = "0"
                GetPReportGroupDetails()
				
				 'Started Activity Log Changes
                Try
                    If Request.QueryString("Type").ToString() = "PROP" Then
                        objUpIns.InsertLog1(Session("UserId").ToString(), "9", "Opened Proprietary Structure Group PopUp", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                    ElseIf Request.QueryString("Type").ToString() = "CPROP" Then
                        objUpIns.InsertLog1(Session("UserId").ToString(), "9", "Opened Company Structure Group PopUp", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                    Else
                        objUpIns.InsertLog1(Session("UserId").ToString(), "9", "Opened Public Structure Group PopUp", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                    End If
                Catch ex As Exception

                End Try
                'Ended Activity Log Changes
				
            End If
             Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub


    Protected Sub GetPReportGroupDetails()
        Dim Dts As New DataSet
        Dim objGetData As New StandGetData.Selectdata
        Dim lst As New ListItem
        Try
            If Request.QueryString("ID").ToString() = "hidGrpId" Then

                If Request.QueryString("Type").ToString() = "PROP" Then
                    Dts = objGetData.GetPGroupCaseDet1(Session("USERID"), Request.QueryString("Type").ToString(), txtkey.Text)
                    If Dts.Tables(0).Rows.Count > 0 Then
                        Session("UsersDataGroup") = Dts
                        grdGroupSearch.DataSource = Dts
                        grdGroupSearch.DataBind()
                    End If
                ElseIf Request.QueryString("Type").ToString() = "BASE" Then
                    Dts = objGetData.GetBGroupCaseDet1(Session("USERID"), Request.QueryString("Type").ToString(), txtkey.Text)
                    If Dts.Tables(0).Rows.Count > 0 Then
                        Session("UsersDataGroup") = Dts
                        grdGroupSearch.DataSource = Dts
                        grdGroupSearch.DataBind()
                    End If
                End If
            ElseIf Request.QueryString("ID").ToString() = "hidGGrpId" Then
                If Request.QueryString("Type").ToString() = "PROP" Then
                    Dts = objGetData.GetPGroupCaseDet1(Session("USERID"), Request.QueryString("Type").ToString(), txtkey.Text)
                    If Dts.Tables(0).Rows.Count > 0 Then
                        Session("UsersDataGroup") = Dts
                        grdGroupSearch.DataSource = Dts
                        grdGroupSearch.DataBind()
                    End If
                ElseIf Request.QueryString("Type").ToString() = "BASE" Then
                    Dts = objGetData.GetBGroupCaseDet1(Session("USERID"), Request.QueryString("Type").ToString(), txtkey.Text)
                    If Dts.Tables(0).Rows.Count > 0 Then
                        Session("UsersDataGroup") = Dts
                        grdGroupSearch.DataSource = Dts
                        grdGroupSearch.DataBind()
                    End If
                End If
            ElseIf Request.QueryString("ID").ToString() = "hidCGrpId" Then
                If Request.QueryString("Type").ToString() = "CPROP" Then
                    Dts = objGetData.GetCompGroupCaseDet1(Session("UserId").ToString(), Request.QueryString("Type").ToString(), txtkey.Text)
                    If Dts.Tables(0).Rows.Count > 0 Then
                        Session("UsersDataGroup") = Dts
                        grdGroupSearch.DataSource = Dts
                        grdGroupSearch.DataBind()
                    End If
                End If
            End If

            If Dts.Tables(0).Rows.Count <= 1 Then
                lblGGroup.Visible = True
                btnSearch.Enabled = False
            Else
                btnSearch.Enabled = True
                lblGGroup.Visible = False
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetAllGroups:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetGroupDetails()
        Dim Dts As New DataSet
        Dim objGetData As New StandGetData.Selectdata
        Dim lst As New ListItem
        Try
            If Request.QueryString("ID").ToString() = "hidGrpId" Then

                If Request.QueryString("Type").ToString() = "PROP" Then
                    Dts = objGetData.GetPGroupCaseDet1(Session("USERID"), Request.QueryString("Type").ToString(), txtkey.Text)
                   
                ElseIf Request.QueryString("Type").ToString() = "BASE" Then
                    Dts = objGetData.GetBGroupCaseDet1(Session("USERID"), Request.QueryString("Type").ToString(), txtkey.Text)
                    'If Dts.Tables(0).Rows.Count > 0 Then
                    '    Session("UsersDataGroup") = Dts
                    '    grdGroupSearch.DataSource = Dts
                    '    grdGroupSearch.DataBind()
                    'End If
                End If
            ElseIf Request.QueryString("ID").ToString() = "hidGGrpId" Then
                If Request.QueryString("Type").ToString() = "PROP" Then
                    Dts = objGetData.GetPGroupCaseDet1(Session("USERID"), Request.QueryString("Type").ToString(), txtkey.Text)
                    'If Dts.Tables(0).Rows.Count > 0 Then
                    '    Session("UsersDataGroup") = Dts
                    '    grdGroupSearch.DataSource = Dts
                    '    grdGroupSearch.DataBind()
                    'End If
                ElseIf Request.QueryString("Type").ToString() = "BASE" Then
                    Dts = objGetData.GetBGroupCaseDet1(Session("USERID"), Request.QueryString("Type").ToString(), txtkey.Text)
                    'If Dts.Tables(0).Rows.Count > 0 Then
                    '    Session("UsersDataGroup") = Dts
                    '    grdGroupSearch.DataSource = Dts
                    '    grdGroupSearch.DataBind()
                    'End If
                End If
            End If

            Session("UsersDataGroup") = Dts
            grdGroupSearch.DataSource = Dts
            grdGroupSearch.DataBind()


        Catch ex As Exception
            _lErrorLble.Text = "Error:GetAllGroups:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Dim objUpIns As New StandUpInsData.UpdateInsert()
            GetGroupDetails()
            'Started Activity Log Changes
            Try
                objUpIns.InsertLog1(Session("UserId").ToString(), "9", "Clicked on Button Search, Searched Text: " + txtkey.Text + " ", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
            Catch ex As Exception

            End Try
            'Ended Activity Log Changes
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub gvDocuments_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles grdGroupSearch.RowDataBound
        Dim ds As New DataSet()
        ds = CType(Session("UsersDataGroup"), DataSet)
        If Request.QueryString("Type").ToString() = "BASE" Then
            If (e.Row.RowType = DataControlRowType.DataRow) Then

                If (e.Row.RowIndex = 0) Then
                    'e.Row.Style.Add("height", "65px")
                    'e.Row.Style.Add("vertical-align", "bottom")
                    e.Row.Cells(2).Style.Add("padding-top", "39px")
                    e.Row.Cells(3).Style.Add("padding-top", "39px")
                    e.Row.Cells(4).Style.Add("padding-top", "39px")
                    e.Row.Cells(5).Style.Add("padding-top", "39px")
                    e.Row.Cells(6).Style.Add("padding-top", "39px")
                    e.Row.Cells(7).Style.Add("padding-top", "39px")
                    e.Row.Cells(8).Style.Add("padding-top", "39px")
                End If
            End If
        Else
            If (e.Row.RowType = DataControlRowType.DataRow) Then

                If (e.Row.RowIndex = 0) Then
                    'e.Row.Style.Add("height", "55px")
                    'e.Row.Style.Add("vertical-align", "bottom")
                    e.Row.Cells(2).Style.Add("padding-top", "39px")
                    e.Row.Cells(3).Style.Add("padding-top", "39px")
                    e.Row.Cells(4).Style.Add("padding-top", "39px")
                    e.Row.Cells(5).Style.Add("padding-top", "39px")
                    e.Row.Cells(6).Style.Add("padding-top", "39px")
                End If
            End If
        End If
        If Request.QueryString("Type").ToString() = "BASE" Then
            e.Row.Cells(7).Visible = True
            e.Row.Cells(8).Visible = True
        Else
            e.Row.Cells(7).Visible = False
            e.Row.Cells(8).Visible = False
        End If

      

        If Request.QueryString("Type").ToString() = "BASE" Then

            grdGroupSearch.Columns(2).ItemStyle.Width = 200
            grdGroupSearch.Columns(3).ItemStyle.Width = 200
            grdGroupSearch.Columns(4).ItemStyle.Width = 150
            grdGroupSearch.Columns(5).ItemStyle.Width = 150
            grdGroupSearch.Columns(6).ItemStyle.Width = 200
            grdGroupSearch.Columns(7).ItemStyle.Width = 120
            grdGroupSearch.Columns(8).ItemStyle.Width = 80

            grdGroupSearch.Columns(2).HeaderStyle.Width = 200
            grdGroupSearch.Columns(3).HeaderStyle.Width = 200
            grdGroupSearch.Columns(4).HeaderStyle.Width = 150
            grdGroupSearch.Columns(5).HeaderStyle.Width = 150
            grdGroupSearch.Columns(6).HeaderStyle.Width = 200
            grdGroupSearch.Columns(7).HeaderStyle.Width = 120
            grdGroupSearch.Columns(8).HeaderStyle.Width = 80

        Else

            grdGroupSearch.Columns(2).ItemStyle.Width = 300
            grdGroupSearch.Columns(3).ItemStyle.Width = 300
            grdGroupSearch.Columns(4).ItemStyle.Width = 150
            grdGroupSearch.Columns(5).ItemStyle.Width = 150
            grdGroupSearch.Columns(6).ItemStyle.Width = 200

            grdGroupSearch.Columns(2).HeaderStyle.Width = 300
            grdGroupSearch.Columns(3).HeaderStyle.Width = 300
            grdGroupSearch.Columns(4).HeaderStyle.Width = 150
            grdGroupSearch.Columns(5).HeaderStyle.Width = 150
            grdGroupSearch.Columns(6).HeaderStyle.Width = 200
        End If
       
    End Sub
    Protected Sub OpenPDF(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnk As LinkButton = DirectCast(sender, LinkButton)
        Dim objUpIns As New StandUpInsData.UpdateInsert()
        If lnk IsNot Nothing Then
            If lnk.CommandArgument.ToString() <> "" Then
                Response.AddHeader("content-disposition", "attachment; filename=" + "../../../Images/Sponsors/" + lnk.CommandArgument)
                Response.WriteFile(Server.MapPath("../../../Images/Sponsors/" + lnk.CommandArgument.ToString()))
                Response.[End]()
            Else
                Page.ClientScript.RegisterStartupScript([GetType], "MyScript", "<script>alert('No Sponsor Message Available')</script>", False)
            End If
			
			 'started Activity Log Changes
            Try
                objUpIns.InsertLog1(Session("UserId").ToString(), "9", "Clicked on Sponsor Message", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
            Catch ex As Exception

            End Try
            'Ended Activity Log Changes
        End If
    End Sub
    Protected Sub grdGroup_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdGroupSearch.Sorting
        Try
            Dim objUpIns As New StandUpInsData.UpdateInsert()
            Dim Dts As New DataSet
            Dim dv As DataView
            Dim ds As New DataSet
            Dim numberDiv As Integer
            numberDiv = Convert.ToInt16(hvUserGrd.Value.ToString())
            Dts = Session("UsersDataGroup")
            dv = Dts.Tables(0).DefaultView

            If ((numberDiv Mod 2) = 0) Then
                dv.Sort = e.SortExpression + " " + "DESC"
            Else
                dv.Sort = e.SortExpression + " " + "ASC"
            End If
            numberDiv += 1
            ds.Tables.Add(dv.ToTable())
            Session("UsersDataGroup") = ds
            hvUserGrd.Value = numberDiv.ToString()
            grdGroupSearch.DataSource = dv
            grdGroupSearch.DataBind()

            'started Activity Log Changes
            Try
                objUpIns.InsertLog1(Session("UserId").ToString(), "9", "Clicked on Sort, Sorted by: " + e.SortExpression + " ", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
            Catch ex As Exception

            End Try
            'Ended Activity Log Changes
        Catch ex As Exception

        End Try
    End Sub
	  Protected Sub btnPostback_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPostback.Click
        Try
            Dim objUpIns As New StandUpInsData.UpdateInsert()
            'Started Activity Log Changes
            Try
			 If Request.QueryString("Type").ToString() = "PROP" Then
                         objUpIns.InsertLog1(Session("UserId").ToString(), "9", "Selected Proprietary Group #" + hidGroup.Value, "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", hidGroup.Value)
            Else
                        objUpIns.InsertLog1(Session("UserId").ToString(), "9", "Selected Public Group #" + hidGroup.Value, "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", hidGroup.Value)
             End If
                Catch ex As Exception

            End Try
            'Ended Activity Log Changes
        Catch ex As Exception

        End Try
    End Sub
End Class
