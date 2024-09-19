Imports System.Data
Imports System.Data.OleDb
Imports System
Imports StandGetData
Imports StandUpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Partial Class Pages_StandAssist_PopUp_CaseDetails
    Inherits System.Web.UI.Page
	 Dim GroupVal As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim objUpIns As New StandUpInsData.UpdateInsert()
            If Session("Back") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If
            hidCaseid.Value = Request.QueryString("ID").ToString()
            hidCaseDes.Value = Request.QueryString("Des").ToString()
            hidCaseidD.Value = Request.QueryString("IdD").ToString()
            hidSponsB.Value = Request.QueryString("SponsBy").ToString()
            hidGrpId.Value = Request.QueryString("GrpID").ToString()

			If Request.QueryString("File") <> Nothing Then
                hidBFile.Value = Request.QueryString("File").ToString()
            End If
            If Request.QueryString("Notes") <> Nothing Then
                hidNotes.Value = Request.QueryString("Notes").ToString()
            End If
			
			If Request.QueryString("PTYPE") <> Nothing Then
                hidPtype.Value = Request.QueryString("PTYPE").ToString()
            End If
            If Not IsPostBack Then
                GetCaseDetails()
                hvUserGrd.Value = "0"
				
				'Started Activity Log Changes
                Try
                    If Request.QueryString("GrpID").ToString() = "0" Then
                        GroupVal = ""
                    Else
                        GroupVal = Request.QueryString("GrpID").ToString()
                    End If

                    If Request.QueryString("Type").ToString() = "PROP" Then
                        objUpIns.InsertLog1(Session("UserId").ToString(), "8", "Opened Proprietary Structure PopUp", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", GroupVal)
                    Else
                        objUpIns.InsertLog1(Session("UserId").ToString(), "8", "Opened Public Structure PopUp", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", GroupVal)
                    End If
                Catch ex As Exception

                End Try
                'Ended Activity Log Changes
            End If
            
           
        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub
Protected Sub OpenPDF(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnk As LinkButton = DirectCast(sender, LinkButton)
        If lnk IsNot Nothing Then
            If lnk.CommandArgument.ToString() <> "" Then
                Response.AddHeader("content-disposition", "attachment; filename=" + "../../../Images/Sponsors/" + lnk.CommandArgument)
                Response.WriteFile(Server.MapPath("../../../Images/Sponsors/" + lnk.CommandArgument.ToString()))
                Response.[End]()
            Else
                Page.ClientScript.RegisterStartupScript([GetType], "MyScript", "<script>alert('No Sponsor Message Available')</script>", False)
            End If
        End If
    End Sub
    Protected Sub GetCaseDetails()
        Dim ds As New DataSet
        Dim objGetData As New StandGetData.Selectdata()
        Try


            If Request.QueryString("Type").ToString() = "BASE" Then
                If Request.QueryString("GrpID").ToString() = "0" Then

                    ds = objGetData.GetBaseCaseGrpDet(txtCaseDe1.Text.Trim.ToString().Replace("'", "''"))
                Else

                    ds = objGetData.GetBCaseDetailsByGroup(txtCaseDe1.Text.Trim.ToString().Replace("'", "''"), Request.QueryString("GrpID").ToString())
                End If
                grdCaseSearch.Columns(3).Visible = True
                grdCaseSearch.Columns(5).Visible = True
            Else
                If Request.QueryString("GrpID").ToString() = "0" Then

                    ds = objGetData.GetPCaseGrpDetailsByType(Session("USERID"), txtCaseDe1.Text.Trim.ToString().Replace("'", "''"))
                Else

                    ds = objGetData.GetPCaseDetailsByGroup(Session("USERID"), txtCaseDe1.Text.Trim.ToString().Replace("'", "''"), Request.QueryString("GrpID").ToString())
                End If
            End If
            If ds.Tables(0).Rows.Count <= 1 Then
                lblPcase.Visible = True
                btnSearch.Enabled = False
            Else
                btnSearch.Enabled = True
                lblPcase.Visible = False
            End If
            Session("UsersDataGroup") = ds
            grdCaseSearch.DataSource = ds
            grdCaseSearch.DataBind()
            
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetCaseDetails:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub GetCaseDetailsSearch()
        Dim ds As New DataSet
        Dim objGetData As New StandGetData.Selectdata()
        Try


            If Request.QueryString("Type").ToString() = "BASE" Then
                If Request.QueryString("GrpID").ToString() = "0" Then

                    ds = objGetData.GetBaseCaseGrpDet(txtCaseDe1.Text.Trim.ToString().Replace("'", "''"))
                Else

                    ds = objGetData.GetBCaseDetailsByGroup(txtCaseDe1.Text.Trim.ToString().Replace("'", "''"), Request.QueryString("GrpID").ToString())
                End If
                grdCaseSearch.Columns(3).Visible = True
                grdCaseSearch.Columns(5).Visible = True
            Else
                If Request.QueryString("GrpID").ToString() = "0" Then

                    ds = objGetData.GetPCaseGrpDetailsByType(Session("USERID"), txtCaseDe1.Text.Trim.ToString().Replace("'", "''"))
                Else

                    ds = objGetData.GetPCaseDetailsByGroup(Session("USERID"), txtCaseDe1.Text.Trim.ToString().Replace("'", "''"), Request.QueryString("GrpID").ToString())
                End If
            End If
          
            Session("UsersDataGroup") = ds
            grdCaseSearch.DataSource = ds
            grdCaseSearch.DataBind()

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetCaseDetails:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub gvDocuments_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles grdCaseSearch.RowDataBound
        Dim ds As New DataSet()
        ds = CType(Session("UsersDataGroup"), DataSet)
        If Request.QueryString("Type").ToString() = "BASE" Then
            If (e.Row.RowType = DataControlRowType.DataRow) Then

                If (e.Row.RowIndex = 0) Then

                    'e.Row.Style.Add("height", "60px")
                    'e.Row.Style.Add("vertical-align", "bottom")
                    e.Row.Cells(1).Style.Add("padding-top", "20px")
                    e.Row.Cells(2).Style.Add("padding-top", "20px")
                    e.Row.Cells(3).Style.Add("padding-top", "20px")
                    e.Row.Cells(4).Style.Add("padding-top", "20px")
                    e.Row.Cells(5).Style.Add("padding-top", "20px")
                    e.Row.Cells(6).Style.Add("padding-top", "20px")
                End If
            End If
        Else
            If (e.Row.RowType = DataControlRowType.DataRow) Then

                If (e.Row.RowIndex = 0) Then
                    'e.Row.Style.Add("height", "60px")
                    'e.Row.Style.Add("vertical-align", "bottom")
                    e.Row.Cells(1).Style.Add("padding-top", "20px")
                    e.Row.Cells(2).Style.Add("padding-top", "20px")
                    e.Row.Cells(3).Style.Add("padding-top", "20px")
                    e.Row.Cells(4).Style.Add("padding-top", "20px")
                End If
            End If
        End If
        If Request.QueryString("Type").ToString() = "BASE" Then
            e.Row.Cells(5).Visible = True
            e.Row.Cells(6).Visible = True
        Else
            e.Row.Cells(5).Visible = False
            e.Row.Cells(6).Visible = False
        End If

        If Request.QueryString("Type").ToString() = "BASE" Then
            grdCaseSearch.Columns(1).ItemStyle.Width = 200
            grdCaseSearch.Columns(2).ItemStyle.Width = 200
            grdCaseSearch.Columns(3).ItemStyle.Width = 200
            grdCaseSearch.Columns(4).ItemStyle.Width = 200
            grdCaseSearch.Columns(5).ItemStyle.Width = 150
            grdCaseSearch.Columns(6).ItemStyle.Width = 150
            grdCaseSearch.Columns(1).HeaderStyle.Width = 200
            grdCaseSearch.Columns(2).HeaderStyle.Width = 200
            grdCaseSearch.Columns(3).HeaderStyle.Width = 200
            grdCaseSearch.Columns(4).HeaderStyle.Width = 200
            grdCaseSearch.Columns(5).HeaderStyle.Width = 150
            grdCaseSearch.Columns(6).HeaderStyle.Width = 150
            'grdCaseSearch.Width = 1100
            'grdCaseSearch.HeaderStyle.Width = 1100

        Else

            grdCaseSearch.Columns(1).ItemStyle.Width = 300
            grdCaseSearch.Columns(2).ItemStyle.Width = 300
            grdCaseSearch.Columns(3).ItemStyle.Width = 300
            grdCaseSearch.Columns(4).ItemStyle.Width = 200
            grdCaseSearch.Columns(1).HeaderStyle.Width = 300
            grdCaseSearch.Columns(2).HeaderStyle.Width = 300
            grdCaseSearch.Columns(3).HeaderStyle.Width = 300
            grdCaseSearch.Columns(4).HeaderStyle.Width = 200
            'grdCaseSearch.Width = 1100
            'grdCaseSearch.HeaderStyle.Width = 1100

        End If

       
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Dim objUpIns As New StandUpInsData.UpdateInsert
            GetCaseDetailsSearch()
	  'Started Acticity Log Changes
            Try
                If Request.QueryString("GrpID").ToString() = "0" Then
                    GroupVal = ""
                Else
                    GroupVal = Request.QueryString("GrpID").ToString()
                End If
                objUpIns.InsertLog1(Session("UserId").ToString(), "8", "Clicked on Button Search, Searched Text: " + txtCaseDe1.Text + " ", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", GroupVal)
            Catch ex As Exception

            End Try

            'Ended Acticity Log Changes          
		  Catch ex As Exception
            _lErrorLble.Text = "Error:btnSearch_Click:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub grdCase_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdCaseSearch.Sorting
        Try
            Dim Dts As New DataSet
            Dim dv As DataView
            Dim ds As New DataSet
            Dim numberDiv As Integer
			  Dim objUpIns As New StandUpInsData.UpdateInsert()
            numberDiv = Convert.ToInt16(hvUserGrd.Value.ToString())
            Dts = Session("UsersDataGroup")
            dv = Dts.Tables(0).DefaultView

            If ((numberDiv Mod 2) = 0) Then
                If e.SortExpression = "CASEID,CaseDE1" Then
                    dv.Sort = "CASEID DESC,CaseDE1 DESC"
                Else
                    dv.Sort = e.SortExpression + " " + "DESC"
                End If

            Else
                If e.SortExpression = "CASEID,CaseDE1" Then
                    dv.Sort = "CASEID ASC,CaseDE1 ASC"
                Else
                    dv.Sort = e.SortExpression + " " + "ASC"
                End If

            End If
            numberDiv += 1
            ds.Tables.Add(dv.ToTable())
            Session("UsersDataGroup") = ds
            hvUserGrd.Value = numberDiv.ToString()
            grdCaseSearch.DataSource = dv
            grdCaseSearch.DataBind()

             'Started Acticity Log Changes
            Try
                If Request.QueryString("GrpID").ToString() = "0" Then
                    GroupVal = ""
                Else
                    GroupVal = Request.QueryString("GrpID").ToString()
                End If
                objUpIns.InsertLog1(Session("UserId").ToString(), "8", "Clicked on Sort, Sorted by: " + e.SortExpression + " ", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", GroupVal)
            Catch ex As Exception

            End Try

            'Ended Acticity Log Changes    
        Catch ex As Exception

        End Try
    End Sub
	 Protected Sub btnPostback_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPostback.Click
        Try
            Dim objUpIns As New StandUpInsData.UpdateInsert()
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "alert('Please select a Structure');", True)
            'Started Activity Log Changes
            Try
                If Request.QueryString("GrpID").ToString() = "0" Then
                    GroupVal = ""
                Else
                    GroupVal = Request.QueryString("GrpID").ToString()
                End If
                objUpIns.InsertLog1(Session("UserId").ToString(), "8", "Selected Structure #" + hidCase.Value, hidCase.Value, Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", GroupVal)
            Catch ex As Exception

            End Try
            'Ended Activity Log Changes
        Catch ex As Exception

        End Try
    End Sub
   
End Class
