Imports System.Data
Imports System.Data.OleDb
Imports System
Imports SavvyProGetData
Imports SavvyProUpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Imports System.Net.Mail
Imports System.Net
Imports AjaxControlToolkit

Partial Class Pages_SavvyPackPro_RFPManager
    Inherits System.Web.UI.Page
    Dim objGetdata As New SavvyProGetData
    Dim objUpIns As New SavvyProUpInsData

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            pnlBMConfigVendor.Visible = True
            lblFooter.Text = "Copyright 1997 - " + Now.Year().ToString() + " SavvyPack Corporation"
            If Session("SBack") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If
            If Session("USERID") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If

            If Not IsPostBack Then
                hidSortIdBSpec.Value = "0"
                hidSortIdBMVendor.Value = "0"
                ChkExistingRfp()
            Else
                GetPageDetails()
            End If
            '            GetPageDetails()
        Catch ex As Exception
            lblError.Text = "Error: Page_Load() " + ex.Message()
        End Try
    End Sub

    Protected Sub ChkExistingRfp()
        Dim DsCheckRfp As New DataSet()
        Try
            If Session("hidRfpID") = Nothing Then
                DsCheckRfp = objGetdata.GetLatestRFPbyLicenseID(Session("LicenseNo"), Session("USERID"))
                If DsCheckRfp.Tables(0).Rows.Count > 0 Then
                    GetRfpDetails(DsCheckRfp.Tables(0).Rows(0).Item("ID").ToString())
                Else
                    RfpDetail.Visible = False
                    tabRfpManager.Enabled = False
                End If
            Else
                GetRfpDetails(Session("hidRfpID"))
            End If
        Catch ex As Exception
            lblError.Text = "Error: ChkExistingRfp() " + ex.Message()
        End Try
    End Sub

    Protected Sub GetRfpDetails(ByVal RfpID As String)
        Dim DsRfpdet As New DataSet()
        Try
            If RfpID <> "" Or RfpID <> "0" Then
                DsRfpdet = objGetdata.GetRFPbyID(RfpID)
                Session("DsRfpdet") = DsRfpdet
                If DsRfpdet.Tables(0).Rows.Count > 0 Then
                    RfpDetail.Visible = True
                    lnkSelRFP.Text = DsRfpdet.Tables(0).Rows(0).Item("DES1").ToString()
                    lblSelRfpID.Text = DsRfpdet.Tables(0).Rows(0).Item("ID").ToString()
                    Session("hidRfpID") = lblSelRfpID.Text
                    lblSelRfpDes.Text = DsRfpdet.Tables(0).Rows(0).Item("DES1").ToString()
                    lblType.Text = DsRfpdet.Tables(0).Rows(0).Item("TYPEDESC").ToString()
                    'Check for LoggedIn User
                    If Session("USERID") = DsRfpdet.Tables(0).Rows(0).Item("USERID").ToString() Then
                        tabRfpManager.Enabled = True
                    Else
                        tabRfpManager.Enabled = False
                    End If
                    'End
                    loadTab()
                Else
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('System isn't able to find ID. Please try again.');", True)
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error: GetRfpDetails() " + ex.Message()
        End Try
    End Sub

    Protected Sub loadTab()
        Try
            Dim DsRfpdet As DataSet

            GetUsersList()
            'Changes made by PK to avoid multiple user entry for one RFP/RFI in TermManager table 
            If Session("hidRfpID") <> Nothing Then
                DsRfpdet = objGetdata.GetRFPbyID(Session("hidRfpID"))
                If Session("USERID") = DsRfpdet.Tables(0).Rows(0).Item("USERID").ToString() Then
                    InsertDefaultTerms()
                End If
            End If
            'End
            GetPriceCost()
            GetOpOption()
            GetPageDetails()
            ' GetPrefDetails()
        Catch ex As Exception
            lblError.Text = "Error: GetRfpDetails() " + ex.Message()
        End Try
    End Sub

#Region "Vendor Config"

    Protected Sub GetUsersList()
        Dim Ds As New DataSet
        Try
            Ds = objGetdata.GetVendorListByUserID(txtKey.Text.Trim.ToString(), Session("USERID"))
            Session("BMVendorList") = Ds
            lblRecondCnt.Text = Ds.Tables(0).Rows.Count

            If Ds.Tables(0).Rows.Count > 0 Then
                lblNOVendor.Visible = False
                grdUsers.Visible = True
                grdUsers.DataSource = Ds
                grdUsers.DataBind()
            Else
                lblNOVendor.Visible = True
                grdUsers.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = "Error:BindUser:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub BindUsersListUsingSession()
        Try
            Dim Dts As New DataSet
            Dts = Session("BMVendorList")
            grdUsers.DataSource = Dts
            grdUsers.DataBind()
        Catch ex As Exception
            Response.Write("Error:BindUsersListUsingSession:" + ex.Message.ToString())
        End Try
    End Sub

    'Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
    '    Try
    '        GetUsersList()

    '    Catch ex As Exception
    '        lblError.Text = "Error:btnSearch_Click" + ex.Message.ToString()
    '    End Try
    'End Sub

#Region "User Grid Event"

    Protected Sub grdUsers_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdUsers.Sorting
        Dim Dts As New DataSet
        Dim dv As DataView
        Dim numberDiv As Integer
        Dim dsSorted As New DataSet()
        Try
            numberDiv = Convert.ToInt16(hidSortIdBMVendor.Value.ToString())
            Dts = Session("BMVendorList")
            dv = Dts.Tables(0).DefaultView

            If ((numberDiv Mod 2) = 0) Then
                dv.Sort = e.SortExpression + " " + "DESC"
            Else
                dv.Sort = e.SortExpression + " " + "ASC"
            End If
            numberDiv += 1
            hidSortIdBMVendor.Value = numberDiv.ToString()
            grdUsers.DataSource = dv
            grdUsers.DataBind()

            dsSorted.Tables.Add(dv.ToTable())
            Session("BMVendorList") = dsSorted

        Catch ex As Exception
            Response.Write("Error:grdUsers_Sorting:" + ex.Message.ToString())
        End Try
    End Sub

    Protected Sub grdUsers_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdUsers.PageIndexChanging
        Try
            grdUsers.PageIndex = e.NewPageIndex
            BindUsersListUsingSession()
        Catch ex As Exception
            Response.Write("Error:grdUsers_PageIndexChanging:" + ex.Message.ToString())
        End Try
    End Sub

    Protected Sub ddlPageCountC_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageCountC.SelectedIndexChanged
        Try
            grdUsers.PageSize = ddlPageCountC.SelectedItem.ToString()
            BindUsersListUsingSession()
        Catch ex As Exception
            Response.Write("Error:ddlPageCountC_SelectedIndexChanged:" + ex.Message.ToString())
        End Try
    End Sub

    Protected Sub grdUsers_databound(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdUsers.DataBound
        Try
            Dim gvr As GridViewRow = grdUsers.TopPagerRow

            Dim lb1 As Label = DirectCast(gvr.Cells(0).FindControl("currentpage"), Label)
            lb1.Text = Convert.ToString(grdUsers.PageIndex + 1)
            Dim page As Integer() = New Integer(10) {}
            page(0) = grdUsers.PageIndex - 2
            page(1) = grdUsers.PageIndex - 1
            page(2) = grdUsers.PageIndex
            page(3) = grdUsers.PageIndex + 1
            page(4) = grdUsers.PageIndex + 2
            page(5) = grdUsers.PageIndex + 3
            page(6) = grdUsers.PageIndex + 4
            page(7) = grdUsers.PageIndex + 5
            page(8) = grdUsers.PageIndex + 6
            page(9) = grdUsers.PageIndex + 7
            page(10) = grdUsers.PageIndex + 8
            For i As Integer = 0 To 10
                If i <> 3 Then
                    If page(i) < 1 OrElse page(i) > grdUsers.PageCount Then
                        Dim lb As LinkButton = DirectCast(gvr.Cells(0).FindControl("p" & Convert.ToString(i)), LinkButton)
                        lb.Visible = False
                    Else
                        Dim lb As LinkButton = DirectCast(gvr.Cells(0).FindControl("p" & Convert.ToString(i)), LinkButton)
                        lb.Text = Convert.ToString(page(i))

                        lb.CommandName = "pageno"

                        lb.CommandArgument = lb.Text
                    End If
                End If
            Next
            If grdUsers.PageIndex = 0 Then
                Dim lb As LinkButton = DirectCast(gvr.Cells(0).FindControl("linkbutton1"), LinkButton)
                lb.Visible = False
                lb = DirectCast(gvr.Cells(0).FindControl("linkbutton2"), LinkButton)

                lb.Visible = False
            End If
            If grdUsers.PageIndex = grdUsers.PageCount - 1 Then
                Dim lb As LinkButton = DirectCast(gvr.Cells(0).FindControl("linkbutton3"), LinkButton)
                lb.Visible = False
                lb = DirectCast(gvr.Cells(0).FindControl("linkbutton4"), LinkButton)

                lb.Visible = False
            End If
            If grdUsers.PageIndex > grdUsers.PageCount - 5 Then
                Dim lbmore As Label = DirectCast(gvr.Cells(0).FindControl("nmore"), Label)
                lbmore.Visible = False
            End If
            If grdUsers.PageIndex < 4 Then
                Dim lbmore As Label = DirectCast(gvr.Cells(0).FindControl("pmore"), Label)
                lbmore.Visible = False
            End If
        Catch ex As Exception
            Response.Write("Error:grdUsers_databound:" + ex.Message.ToString())
        End Try
    End Sub

    Protected Sub lb_command_VC(ByVal sender As Object, ByVal e As CommandEventArgs)
        grdUsers.PageIndex = Convert.ToInt32(e.CommandArgument) - 1
        BindUsersListUsingSession()
    End Sub

    Protected Sub grdUsers_rowcreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdUsers.RowCreated
        If e.Row.RowType = DataControlRowType.Pager Then
            Dim gvr As GridViewRow = e.Row
            Dim lb As LinkButton = DirectCast(gvr.Cells(0).FindControl("p0"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_VC
            lb = DirectCast(gvr.Cells(0).FindControl("p1"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_VC
            lb = DirectCast(gvr.Cells(0).FindControl("p2"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_VC
            lb = DirectCast(gvr.Cells(0).FindControl("p4"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_VC
            lb = DirectCast(gvr.Cells(0).FindControl("p5"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_VC
            lb = DirectCast(gvr.Cells(0).FindControl("p6"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_VC
            lb = DirectCast(gvr.Cells(0).FindControl("p7"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_VC
            lb = DirectCast(gvr.Cells(0).FindControl("p8"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_VC
            lb = DirectCast(gvr.Cells(0).FindControl("p9"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_VC
            lb = DirectCast(gvr.Cells(0).FindControl("p10"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_VC
        End If
    End Sub

#End Region

    Protected Sub grdUsers_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdUsers.RowDataBound
        Dim DsExistusr As New DataSet
        Dim lblUserID As New Label
        Dim check As New CheckBox

        Try
            DsExistusr = objGetdata.GetLinkedVendor(Session("hidRfpID"))
            For Each Gr As GridViewRow In grdUsers.Rows
                lblUserID = grdUsers.Rows(Gr.RowIndex).FindControl("lblMUsrID")
                check = grdUsers.Rows(Gr.RowIndex).FindControl("select")
                For i = 0 To DsExistusr.Tables(0).Rows.Count - 1
                    If lblUserID.Text = DsExistusr.Tables(0).Rows(i).Item("VENDORID") Then
                        check.Checked = True
                    End If
                Next
            Next
        Catch ex As Exception
            lblError.Text = "Error:grdUsers_RowDataBound" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub chkchangedvendor(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rbdCPrice As New RadioButton
        Dim rbdOPrice As New RadioButton
        Try
            Dim check As CheckBox = TryCast(sender, CheckBox)
            Dim gvrow As GridViewRow = TryCast(check.NamingContainer, GridViewRow)
            Dim VendorID As String = CType(gvrow.FindControl("lblMUsrID"), Label).Text
            Dim VendorEmailID As String = CType(gvrow.FindControl("lblEmailID"), Label).Text

            Dim radB As RadioButton = TryCast(sender, RadioButton)
            If check.Checked = True Then
                objUpIns.ConnectRFPVendor(VendorID.ToString(), Session("hidRfpID"), "2", Session("UserId"), VendorEmailID.Replace("'", "''").ToString(), "1")
            Else
                objUpIns.DisconnectRFPVendor(VendorID.ToString(), Session("hidRfpID"), "1")
                'objUpIns.DisconnectRFPVendor(VendorID.ToString(), Session("hidRfpID"))
            End If
            ScriptManager.RegisterStartupScript(Page, Me.GetType(), "JSScript", "ClosePage();", True)
            GetUsersList()
        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "Terms"

    Protected Sub InsertDefaultTerms()
        Dim Dts As New DataSet()
        Dim i As New Integer
        Dim ds As New DataSet
        Dim DsTermSeq As New DataSet
        Dim TermSeq As String = String.Empty
        Dim IsAvl As Boolean = False
        Try
            Dts = objGetdata.GetDefaultTerms()
            ds = objGetdata.GetTerms(Session("hidRFPID"), Session("UserId"))

            For i = 1 To Dts.Tables(0).Rows.Count

                For j = 1 To ds.Tables(0).Rows.Count
                    If Dts.Tables(0).Rows(i - 1).Item("DTERMID").ToString() = ds.Tables(0).Rows(j - 1).Item("DTERMID").ToString() Then
                        IsAvl = True
                        Exit For
                    Else
                        IsAvl = False
                    End If
                Next

                If IsAvl = False Then
                    DsTermSeq = objGetdata.GetTermSeq(Session("hidRfpID"), "Y")
                    If DsTermSeq.Tables(0).Rows(0).Item("TERMSEQ").ToString() = "" Then
                        TermSeq = "1"
                    Else
                        TermSeq = DsTermSeq.Tables(0).Rows(0).Item("TERMSEQ").ToString() + 1
                    End If
                    objUpIns.AddDefaultTerms(Session("UserId").ToString(), Dts.Tables(0).Rows(i - 1).Item("TITLE").ToString(), _
                                             Dts.Tables(0).Rows(i - 1).Item("ISDEFAULT").ToString(), TermSeq, Session("hidRFPID"), _
                                             Dts.Tables(0).Rows(i - 1).Item("DTERMID").ToString())
                End If
            Next

            'Insert Print tech
            For k = 0 To 1
                If k = 0 Then
                    objUpIns.InsertPrintTech(Session("hidRFPID"), k + 1.ToString(), "ROTO")
                Else
                    objUpIns.InsertPrintTech(Session("hidRFPID"), k + 1.ToString(), "FLEXO")
                End If
            Next
            'End

            'Insert Additional Term
            objUpIns.InsertAddTerm(Session("hidRFPID"), "Add/Remove Color")
            'End

        Catch ex As Exception
            Response.Write("InsertAnalysisInformation" + ex.Message)
        End Try
    End Sub

    Protected Sub GetPageDetails()
        Dim dsQues As New DataSet()
        Dim lnkQues As LinkButton
        Dim trQues As TableRow
        Dim tdId As TableCell
        Dim delBtn As Button
        Dim trSpace As New TableRow
        Dim txtTerm As New TextBox
        Dim txtItem As New TextBox
        Dim txtSeq As New TextBox
        Dim HidSeqQue As New HiddenField

        Dim trHeader As New TableRow
        Dim trHeader1 As New TableRow
        Dim trHeader2 As New TableRow

        Dim trInner As New TableRow

        Dim tdHeader As TableCell
        Dim chck As New CheckBox
        Dim hid As HiddenField
        Dim DvData As DataView
        Dim dsData As DataTable
        Dim calEx As CalendarExtender
        Try
            tblEditQC.Rows.Clear()
            tblEditQS.Rows.Clear()
            dsQues = objGetdata.GetTerms(Session("hidRfpID"), Session("UserId"))
            Session("TermsData") = dsQues
            DvData = dsQues.Tables(0).DefaultView
            Session("count") = dsQues.Tables(0).Rows.Count - 1

            For i = 1 To 4
                tdHeader = New TableCell
                Dim Title As String = String.Empty

                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "50px", "Include In", "1")
                        trHeader.Controls.Add(tdHeader)

                    Case 2
                        HeaderTdSetting(tdHeader, "50px", "Terms Order", "1")
                        trHeader.Controls.Add(tdHeader)

                    Case 3

                        HeaderTdSetting(tdHeader, "250px", "Item", "1")
                        trHeader.Controls.Add(tdHeader)

                    Case 4

                        HeaderTdSetting(tdHeader, "350px", "Terms", "1")
                        trHeader.Controls.Add(tdHeader)

                End Select

            Next

            trHeader.Height = 30
            tblEditQS.Controls.Add(trHeader)

            'pt changes

            Dim trHeaderC As New TableRow

            For i = 1 To 4
                tdHeader = New TableCell
                Dim Title As String = String.Empty
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "50px", "Include In", "1")
                        trHeaderC.Controls.Add(tdHeader)

                    Case 2
                        HeaderTdSetting(tdHeader, "50px", "Terms Order", "1")
                        trHeaderC.Controls.Add(tdHeader)

                    Case 3

                        HeaderTdSetting(tdHeader, "250px", "Item", "1")
                        trHeaderC.Controls.Add(tdHeader)

                    Case 4

                        HeaderTdSetting(tdHeader, "350px", "Terms", "1")
                        trHeaderC.Controls.Add(tdHeader)

                End Select

            Next
            trHeaderC.Height = 30
            tblEditQC.Controls.Add(trHeaderC)

            tblEditQC.Visible = False
            lblcustomize.Visible = False

            'end pt

           
            If dsQues.Tables(0).Rows.Count > 0 Then
                For i = 0 To dsQues.Tables(0).Rows.Count - 1
                    
                    If dsQues.Tables(0).Rows(i).Item("ISDEFAULT").ToString() = "Y" Then
                        trQues = New TableRow
                        lnkQues = New LinkButton

                        For b = 1 To 4
                            tdId = New TableCell
                            Select Case b
                                Case 1
                                    chck = New CheckBox
                                    hid = New HiddenField
                                    ' chck.ID = "chckBut" + (i + 1).ToString()
                                    'hid.ID = "hidchck" + (i + 1).ToString()
                                    chck.ID = dsQues.Tables(0).Rows(i).Item("TERMID").ToString()
                                    If dsQues.Tables(0).Rows(i).Item("ISDEFAULT") = "Y" Then
                                        If dsQues.Tables(0).Rows(i).Item("ISCHECKED").ToString() = "Y" Then
                                            chck.Checked = True
                                        Else
                                            chck.Checked = False
                                        End If
                                    ElseIf dsQues.Tables(0).Rows(i).Item("ISDEFAULT") = "N" Then
                                        If dsQues.Tables(0).Rows(i).Item("ISCHECKED").ToString() = "Y" Then
                                            chck.Checked = True
                                        Else
                                            chck.Checked = False
                                        End If
                                    End If
                                    chck.AutoPostBack = True
                                    AddHandler chck.CheckedChanged, AddressOf CheckBox_Check
                                    tdId.Width = 30
                                    tdId.Controls.Add(hid)
                                    tdId.Controls.Add(chck)


                                Case 2
                                    txtSeq = New TextBox
                                    HidSeqQue = New HiddenField
                                    hid = New HiddenField
                                    InnerTdSetting(tdId, "", "Center")
                                    txtSeq.Text = dsQues.Tables(0).Rows(i).Item("TERMSEQ").ToString()
                                    txtSeq.ID = dsQues.Tables(0).Rows(i).Item("TERMID").ToString() + "_" + (i + 1).ToString()
                                    HidSeqQue.Value = txtSeq.Text
                                    txtSeq.Font.Size = 10
                                    txtSeq.MaxLength = 3
                                    txtSeq.Style.Add("background-color", "#FEFCA1")
                                    txtSeq.Style.Add("font-family", "Verdana")
                                    txtSeq.Style.Add("width", "28px")
                                    txtSeq.Style.Add("height", "14px")
                                    txtSeq.AutoPostBack = True
                                    txtSeq.Attributes.Add("onchange", "javascript:return CheckSP(this,'" + HidSeqQue.Value + "');")
                                    AddHandler txtSeq.TextChanged, AddressOf TextBox_TextChanged
                                    tdId.Controls.Add(txtSeq)
                                    tdId.Controls.Add(HidSeqQue)
                                    tdId.Controls.Add(hid)
                                Case 3
                                    txtItem = New TextBox
                                    'InnerTdSetting(lbl, "", "Center")

                                    If dsQues.Tables(0).Rows(i).Item("ISDEFAULT") = "Y" Then
                                        txtItem.Text = dsQues.Tables(0).Rows(i).Item("TITLE").ToString()
                                        txtItem.Enabled = False
                                        txtItem.Style.Add("background-color", "#a6a6a6")
                                        txtItem.Style.Add("color", "#4d4d4d")
                                    Else
                                        txtItem.Text = dsQues.Tables(0).Rows(i).Item("TITLE").ToString()
                                        txtItem.Enabled = True
                                        txtItem.Style.Add("background-color", "#FEFCA1")
                                    End If
                                    HidSeqQue.Value = txtItem.Text
                                    txtItem.ID = dsQues.Tables(0).Rows(i).Item("TERMID").ToString() + "." + (i + 1).ToString()
                                    txtItem.Style.Add("font-family", "Verdana")
                                    txtItem.Style.Add("width", "300px")
                                    txtItem.Style.Add("height", "14px")
                                    txtItem.AutoPostBack = True
                                    ' txtItem.Attributes.Add("onchange", "javascript:return CheckSP(this,'" + HidSeqQue.Value + "');")
                                    AddHandler txtItem.TextChanged, AddressOf TextBox_TextChangedI
                                    tdId.Controls.Add(txtItem)
                                Case 4
                                    txtTerm = New TextBox
                                    txtTerm.ID = dsQues.Tables(0).Rows(i).Item("TERMID").ToString() + "_T" + (i + 1).ToString()
                                    HidSeqQue.Value = txtTerm.Text
                                    If dsQues.Tables(0).Rows(i).Item("TITLE") = "RFP Due Date" Then
                                        calEx = New CalendarExtender
                                        calEx.ID = "calEx"
                                        calEx.TargetControlID = txtTerm.ID
                                        'calEx.Format = "MMM dd, yyyy"
                                        txtTerm.Text = dsQues.Tables(0).Rows(i).Item("DESCRIPTION").ToString()
                                        txtTerm.Style.Add("background-color", "#FEFCA1")
                                        txtTerm.Style.Add("font-family", "Verdana")
                                        txtTerm.Style.Add("width", "350px")
                                        txtTerm.Style.Add("height", "14px")
                                        txtTerm.AutoPostBack = True
                                        txtTerm.Attributes.Add("onchange", "javascript:return CheckSP(this,'" + HidSeqQue.Value + "');")
                                        AddHandler txtTerm.TextChanged, AddressOf TextBox_TextChangedTC
                                        tdId.Controls.Add(calEx)
                                        tdId.Controls.Add(txtTerm)

                                    ElseIf dsQues.Tables(0).Rows(i).Item("TITLE") = "Starting Date" Then
                                        calEx = New CalendarExtender
                                        calEx.ID = "calExS"
                                        calEx.TargetControlID = txtTerm.ID
                                        'calEx.Format = "MMM dd, yyyy"
                                        txtTerm.Text = dsQues.Tables(0).Rows(i).Item("DESCRIPTION").ToString()
                                        txtTerm.Style.Add("background-color", "#FEFCA1")
                                        txtTerm.Style.Add("font-family", "Verdana")
                                        txtTerm.Style.Add("width", "350px")
                                        txtTerm.Style.Add("height", "14px")
                                        txtTerm.AutoPostBack = True
                                        txtTerm.Attributes.Add("onchange", "javascript:return CheckSP(this,'" + HidSeqQue.Value + "');")
                                        AddHandler txtTerm.TextChanged, AddressOf TextBox_TextChangedTS
                                        tdId.Controls.Add(calEx)
                                        tdId.Controls.Add(txtTerm)
                                    ElseIf dsQues.Tables(0).Rows(i).Item("TITLE") = "Ending Date" Then
                                        calEx = New CalendarExtender
                                        calEx.ID = "calExE"
                                        calEx.TargetControlID = txtTerm.ID
                                        'calEx.Format = "MMM dd, yyyy"
                                        txtTerm.Text = dsQues.Tables(0).Rows(i).Item("DESCRIPTION").ToString()
                                        txtTerm.Style.Add("background-color", "#FEFCA1")
                                        txtTerm.Style.Add("font-family", "Verdana")
                                        txtTerm.Style.Add("width", "350px")
                                        txtTerm.Style.Add("height", "14px")
                                        txtTerm.AutoPostBack = True
                                        txtTerm.Attributes.Add("onchange", "javascript:return CheckSP(this,'" + HidSeqQue.Value + "');")
                                        AddHandler txtTerm.TextChanged, AddressOf TextBox_TextChangedTE
                                        tdId.Controls.Add(calEx)
                                        tdId.Controls.Add(txtTerm)
                                    Else

                                        'InnerTdSetting(lbl, "", "Center")
                                        txtTerm.Text = dsQues.Tables(0).Rows(i).Item("DESCRIPTION").ToString()
                                        txtTerm.Style.Add("background-color", "#FEFCA1")
                                        txtTerm.Style.Add("font-family", "Verdana")
                                        txtTerm.Style.Add("width", "350px")
                                        txtTerm.Style.Add("height", "14px")
                                        txtTerm.AutoPostBack = True
                                        txtTerm.Attributes.Add("onchange", "javascript:return CheckSP(this,'" + HidSeqQue.Value + "');")
                                        AddHandler txtTerm.TextChanged, AddressOf TextBox_TextChangedT
                                        tdId.Controls.Add(txtTerm)
                                    End If
                            End Select
                            trQues.Controls.Add(tdId)

                        Next
                        If i Mod 2 = 0 Then
                            trQues.CssClass = "AlterNateColor1"
                        Else
                            trQues.CssClass = "AlterNateColor2"
                        End If
                        tblEditQS.Controls.Add(trQues)

                        'ptchanges
                    Else
                        tblEditQC.Visible = True
                        lblcustomize.Visible = True
                        trQues = New TableRow
                        lnkQues = New LinkButton

                        For b = 1 To 4
                            tdId = New TableCell
                            Select Case b
                                Case 1
                                    chck = New CheckBox
                                    hid = New HiddenField
                                    ' chck.ID = "chckBut" + (i + 1).ToString()
                                    'hid.ID = "hidchck" + (i + 1).ToString()
                                    chck.ID = dsQues.Tables(0).Rows(i).Item("TERMID").ToString()
                                    If dsQues.Tables(0).Rows(i).Item("ISDEFAULT") = "Y" Then
                                        If dsQues.Tables(0).Rows(i).Item("ISCHECKED").ToString() = "Y" Then
                                            chck.Checked = True
                                        Else
                                            chck.Checked = False
                                        End If
                                    ElseIf dsQues.Tables(0).Rows(i).Item("ISDEFAULT") = "N" Then
                                        If dsQues.Tables(0).Rows(i).Item("ISCHECKED").ToString() = "Y" Then
                                            chck.Checked = True
                                        Else
                                            chck.Checked = False
                                        End If
                                    End If
                                    chck.AutoPostBack = True
                                    AddHandler chck.CheckedChanged, AddressOf CheckBox_Check
                                    tdId.Width = 30
                                    tdId.Controls.Add(hid)
                                    tdId.Controls.Add(chck)


                                Case 2
                                    txtSeq = New TextBox
                                    HidSeqQue = New HiddenField
                                    hid = New HiddenField
                                    InnerTdSetting(tdId, "", "Center")
                                    txtSeq.Text = dsQues.Tables(0).Rows(i).Item("TERMSEQ").ToString()
                                    txtSeq.ID = dsQues.Tables(0).Rows(i).Item("TERMID").ToString() + "_" + (i + 1).ToString()
                                    HidSeqQue.Value = txtSeq.Text
                                    txtSeq.Font.Size = 10
                                    txtSeq.MaxLength = 3
                                    txtSeq.Style.Add("background-color", "#FEFCA1")
                                    txtSeq.Style.Add("font-family", "Verdana")
                                    txtSeq.Style.Add("width", "28px")
                                    txtSeq.Style.Add("height", "14px")
                                    txtSeq.AutoPostBack = True
                                    txtSeq.Attributes.Add("onchange", "javascript:return CheckSP(this,'" + HidSeqQue.Value + "');")
                                    AddHandler txtSeq.TextChanged, AddressOf TextBox_TextChanged
                                    tdId.Controls.Add(txtSeq)
                                    tdId.Controls.Add(HidSeqQue)
                                    tdId.Controls.Add(hid)
                                Case 3
                                    txtItem = New TextBox
                                    'InnerTdSetting(lbl, "", "Center")

                                    If dsQues.Tables(0).Rows(i).Item("ISDEFAULT") = "Y" Then
                                        txtItem.Text = dsQues.Tables(0).Rows(i).Item("TITLE").ToString()
                                        txtItem.Enabled = False
                                        txtItem.Style.Add("background-color", "#a6a6a6")
                                        txtItem.Style.Add("color", "#4d4d4d")
                                    Else
                                        txtItem.Text = dsQues.Tables(0).Rows(i).Item("TITLE").ToString()
                                        txtItem.Enabled = True
                                        txtItem.Style.Add("background-color", "#FEFCA1")
                                    End If
                                    HidSeqQue.Value = txtItem.Text
                                    txtItem.ID = dsQues.Tables(0).Rows(i).Item("TERMID").ToString() + "." + (i + 1).ToString()
                                    txtItem.Style.Add("font-family", "Verdana")
                                    txtItem.Style.Add("width", "300px")
                                    txtItem.Style.Add("height", "14px")
                                    txtItem.AutoPostBack = True
                                    ' txtItem.Attributes.Add("onchange", "javascript:return CheckSP(this,'" + HidSeqQue.Value + "');")
                                    AddHandler txtItem.TextChanged, AddressOf TextBox_TextChangedI
                                    tdId.Controls.Add(txtItem)
                                Case 4
                                    txtTerm = New TextBox
                                    txtTerm.ID = dsQues.Tables(0).Rows(i).Item("TERMID").ToString() + "_T" + (i + 1).ToString()
                                    HidSeqQue.Value = txtTerm.Text
                                    If dsQues.Tables(0).Rows(i).Item("TITLE") = "RFP Due Date" Then
                                        calEx = New CalendarExtender
                                        calEx.ID = "calEx"
                                        calEx.TargetControlID = txtTerm.ID
                                        'calEx.Format = "MMM dd, yyyy"
                                        txtTerm.Text = dsQues.Tables(0).Rows(i).Item("DESCRIPTION").ToString()
                                        txtTerm.Style.Add("background-color", "#FEFCA1")
                                        txtTerm.Style.Add("font-family", "Verdana")
                                        txtTerm.Style.Add("width", "350px")
                                        txtTerm.Style.Add("height", "14px")
                                        txtTerm.AutoPostBack = True
                                        txtTerm.Attributes.Add("onchange", "javascript:return CheckSP(this,'" + HidSeqQue.Value + "');")
                                        AddHandler txtTerm.TextChanged, AddressOf TextBox_TextChangedTC
                                        tdId.Controls.Add(calEx)
                                        tdId.Controls.Add(txtTerm)

                                    ElseIf dsQues.Tables(0).Rows(i).Item("TITLE") = "Starting Date" Then
                                        calEx = New CalendarExtender
                                        calEx.ID = "calExS"
                                        calEx.TargetControlID = txtTerm.ID
                                        'calEx.Format = "MMM dd, yyyy"
                                        txtTerm.Text = dsQues.Tables(0).Rows(i).Item("DESCRIPTION").ToString()
                                        txtTerm.Style.Add("background-color", "#FEFCA1")
                                        txtTerm.Style.Add("font-family", "Verdana")
                                        txtTerm.Style.Add("width", "350px")
                                        txtTerm.Style.Add("height", "14px")
                                        txtTerm.AutoPostBack = True
                                        txtTerm.Attributes.Add("onchange", "javascript:return CheckSP(this,'" + HidSeqQue.Value + "');")
                                        AddHandler txtTerm.TextChanged, AddressOf TextBox_TextChangedTS
                                        tdId.Controls.Add(calEx)
                                        tdId.Controls.Add(txtTerm)
                                    ElseIf dsQues.Tables(0).Rows(i).Item("TITLE") = "Ending Date" Then
                                        calEx = New CalendarExtender
                                        calEx.ID = "calExE"
                                        calEx.TargetControlID = txtTerm.ID
                                        'calEx.Format = "MMM dd, yyyy"
                                        txtTerm.Text = dsQues.Tables(0).Rows(i).Item("DESCRIPTION").ToString()
                                        txtTerm.Style.Add("background-color", "#FEFCA1")
                                        txtTerm.Style.Add("font-family", "Verdana")
                                        txtTerm.Style.Add("width", "350px")
                                        txtTerm.Style.Add("height", "14px")
                                        txtTerm.AutoPostBack = True
                                        txtTerm.Attributes.Add("onchange", "javascript:return CheckSP(this,'" + HidSeqQue.Value + "');")
                                        AddHandler txtTerm.TextChanged, AddressOf TextBox_TextChangedTE
                                        tdId.Controls.Add(calEx)
                                        tdId.Controls.Add(txtTerm)
                                    Else

                                        'InnerTdSetting(lbl, "", "Center")
                                        txtTerm.Text = dsQues.Tables(0).Rows(i).Item("DESCRIPTION").ToString()
                                        txtTerm.Style.Add("background-color", "#FEFCA1")
                                        txtTerm.Style.Add("font-family", "Verdana")
                                        txtTerm.Style.Add("width", "350px")
                                        txtTerm.Style.Add("height", "14px")
                                        txtTerm.AutoPostBack = True
                                        txtTerm.Attributes.Add("onchange", "javascript:return CheckSP(this,'" + HidSeqQue.Value + "');")
                                        AddHandler txtTerm.TextChanged, AddressOf TextBox_TextChangedT
                                        tdId.Controls.Add(txtTerm)
                                    End If
                            End Select
                            trQues.Controls.Add(tdId)

                        Next
                        If i Mod 2 = 0 Then
                            trQues.CssClass = "AlterNateColor1"
                        Else
                            trQues.CssClass = "AlterNateColor2"
                        End If
                        tblEditQC.Controls.Add(trQues)
                    End If
                    'ptchanges end                   
                Next
            End If
        Catch ex As Exception
            lblError.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub CheckBox_Check(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objUpIns As New SavvyProUpInsData()
        Dim objGetData As New SavvyProGetData()
        Dim QId As String
        Try
            Dim chk = DirectCast(sender, CheckBox)
            If chk.Checked = True Then
                QId = chk.ID
                objUpIns.UpdateIsChecked(Session("hidRfpID").ToString(), QId.ToString())
            Else
                QId = chk.ID
                objUpIns.DeleteIsChecked(Session("hidRfpID").ToString(), QId.ToString())
            End If
            'GetPageDetails()
        Catch ex As Exception
            lblError.Text = "Error:CheckBox_Check:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub TextBox_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim TextSeq As String
        Dim QId As String
        Dim DsQuestionSeq As New DataSet()
        Dim Avail As New Boolean
        Try
            Dim TxtSeq = DirectCast(sender, TextBox)
            TextSeq = TxtSeq.Text
            QId = TxtSeq.ID

            Dim Temp As String() = QId.Split("_")
            Dim temp1 As String = Temp(0)

            Avail = objGetdata.GetTermSeqAvail(TextSeq.ToString(), Session("hidRfpID").ToString(), temp1.ToString())
            If Avail Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert1", "alert('Entered sequence number already exist.')", True)
                'objUpIns.UpdateQueSeqID(TextSeq.ToString(), temp1.ToString())
            Else
                objUpIns.UpdateQueSeqID(TextSeq.ToString(), temp1.ToString())
            End If
            GetPageDetails()
        Catch ex As Exception
            lblError.Text = "Error:TextBox_TextChanged:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub TextBox_TextChangedI(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim TextSeq As String
        Dim QId As String
        Dim DsQuestionSeq As New DataSet()
        Dim Avail As New Boolean
        Try
            Dim txtItem = DirectCast(sender, TextBox)
            TextSeq = txtItem.Text
            QId = txtItem.ID

            Dim Temp As String() = QId.Split(".")
            Dim temp1 As String = Temp(0)

            Avail = objGetdata.GetTermTitle(temp1, TextSeq.ToString())
            If TextSeq.Replace("'", "''").ToString() <> "" Then
                If Avail Then
                    ScriptManager.RegisterStartupScript(Page, Me.GetType(), "alert", "alert('Entered Item already exist.')", True)
                    'objUpIns.UpdateQueSeqID(TextSeq.ToString(), temp1.ToString())
                Else
                    objUpIns.UpdateTerm(TextSeq.Replace("'", "''").ToString(), temp1.ToString())
                    ScriptManager.RegisterStartupScript(Page, Me.GetType(), "alert", "alert('Item Updated Succesfully.')", True)

                End If
            Else
                ScriptManager.RegisterStartupScript(Page, Me.GetType(), "alert", "alert('You cannot keep Item blank.Please enter Item.')", True)
            End If

            GetPageDetails()
        Catch ex As Exception
            lblError.Text = "Error:TextBox_TextChanged:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub TextBox_TextChangedT(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim TextSeq As String
        Dim QId As String
        Dim DsQuestionSeq As New DataSet()
        Dim Avail As New Boolean
        Try
            Dim txtTerm = DirectCast(sender, TextBox)
            TextSeq = txtTerm.Text
            QId = txtTerm.ID

            Dim Temp As String() = QId.Split("_T")
            Dim temp1 As String = Temp(0)

            Avail = objGetdata.GetTermDesc(temp1, TextSeq.ToString())
            If Avail Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert1", "alert('Entered Term already exist.')", True)
                'objUpIns.UpdateQueSeqID(TextSeq.ToString(), temp1.ToString())
            Else
                objUpIns.UpdateTerm1(TextSeq.Replace("'", "''").ToString(), temp1.ToString(), Session("hidRfpID").ToString())
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert1", "alert('Term updated sucessfully.')", True)
            End If
            GetPageDetails()
        Catch ex As Exception
            lblError.Text = "Error:TextBox_TextChanged:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub TextBox_TextChangedTC(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim TextSeq As String
        Dim QId As String
        Dim DsQuestionSeq As New DataSet()
        Dim Avail As New Boolean
        Try
            Dim txtTerm = DirectCast(sender, TextBox)
            TextSeq = txtTerm.Text
            QId = txtTerm.ID

            Dim Temp As String() = QId.Split("_T")
            Dim temp1 As String = Temp(0)

            Avail = objGetdata.GetTermDesc(temp1, TextSeq.ToString())
            If Avail Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert1", "alert('Entered Term already exist.')", True)
                'objUpIns.UpdateQueSeqID(TextSeq.ToString(), temp1.ToString())
            Else
                objUpIns.UpdateTermCal(TextSeq.ToString(), temp1.ToString(), Session("hidRfpID").ToString(), "DueDate")
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert1", "alert('Term updated sucessfully.')", True)
            End If
            GetPageDetails()
        Catch ex As Exception
            lblError.Text = "Error:TextBox_TextChanged:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub TextBox_TextChangedTS(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim TextSeq As String
        Dim QId As String
        Dim DsQuestionSeq As New DataSet()
        Dim Avail As New Boolean
        Try
            Dim txtTerm = DirectCast(sender, TextBox)
            TextSeq = txtTerm.Text
            QId = txtTerm.ID

            Dim Temp As String() = QId.Split("_T")
            Dim temp1 As String = Temp(0)

            Avail = objGetdata.GetTermDesc(temp1, TextSeq.ToString())
            If Avail Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert1", "alert('Entered Term already exist.')", True)
                'objUpIns.UpdateQueSeqID(TextSeq.ToString(), temp1.ToString())
            Else
                objUpIns.UpdateTermCal(TextSeq.ToString(), temp1.ToString(), Session("hidRfpID").ToString(), "StartDate")
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert1", "alert('Term updated sucessfully.')", True)
            End If
            GetPageDetails()
        Catch ex As Exception
            lblError.Text = "Error:TextBox_TextChanged:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub TextBox_TextChangedTE(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim TextSeq As String
        Dim QId As String
        Dim DsQuestionSeq As New DataSet()
        Dim Avail As New Boolean
        Try
            Dim txtTerm = DirectCast(sender, TextBox)
            TextSeq = txtTerm.Text
            QId = txtTerm.ID

            Dim Temp As String() = QId.Split("_T")
            Dim temp1 As String = Temp(0)

            Avail = objGetdata.GetTermDesc(temp1, TextSeq.ToString())
            If Avail Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert1", "alert('Entered Term already exist.')", True)
                'objUpIns.UpdateQueSeqID(TextSeq.ToString(), temp1.ToString())
            Else
                objUpIns.UpdateTermCal(TextSeq.ToString(), temp1.ToString(), Session("hidRfpID").ToString(), "EndDate")
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert1", "alert('Term updated sucessfully.')", True)
            End If
            GetPageDetails()
        Catch ex As Exception
            lblError.Text = "Error:TextBox_TextChanged:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnrefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        Try
        Catch ex As Exception
            lblError.Text = "Error:btnLoad_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnrefreshT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnrefreshT.Click
        Try
            'GetPageDetails()
        Catch ex As Exception
            lblError.Text = "Error:btnLoad_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub HeaderTdSetting(ByVal Td As TableCell, ByVal Width As String, ByVal HeaderText As String, ByVal ColSpan As String)
        Try
            Td.Text = HeaderText
            Td.ColumnSpan = ColSpan
            If Width <> "" Then
                Td.Style.Add("width", Width)
            End If
            Td.CssClass = "TdHeading"
            Td.Height = 20
            Td.Font.Size = 10
            Td.Font.Bold = True
            Td.HorizontalAlign = HorizontalAlign.Center



        Catch ex As Exception
            lblError.Text = "Error:HeaderTdSetting:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub Header2TdSetting(ByVal Td As TableCell, ByVal Width As String, ByVal HeaderText As String, ByVal ColSpan As String)
        Try
            Td.Text = HeaderText
            Td.ColumnSpan = ColSpan
            If Width <> "" Then
                Td.Style.Add("width", Width)
            End If
            Td.CssClass = "TdHeading"
            Td.Font.Size = 8
            Td.Height = 20
            Td.HorizontalAlign = HorizontalAlign.Center



        Catch ex As Exception
            lblError.Text = "Error:HeaderTdSetting:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub InnerTdSetting(ByVal Td As TableCell, ByVal Width As String, ByVal Align As String)
        Try

            If Width <> "" Then
                Td.Style.Add("width", Width)
            End If
            Td.Style.Add("text-align", Align)
            If Align = "Left" Then
                Td.Style.Add("padding-left", "5px")
            End If
            If Align = "Right" Then
                Td.Style.Add("padding-right", "5px")
            End If
            Td.Style.Add("font-size", "13px")
        Catch ex As Exception
            lblError.Text = "Error:InnerTdSetting:" + ex.Message.ToString()
        End Try
    End Sub

#End Region

#Region "Hidden Buttons"

    Protected Sub btnHidRFPCreate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHidRFPCreate.Click
        Try
            If hidRfpID.Value <> "" Or hidRfpID.Value <> "0" Then
                tabRfpManager.Enabled = True
                lnkSelRFP.Text = hidRfpNm.Value
                GetRfpDetails(hidRfpID.Value)
            End If
        Catch ex As Exception
            lblError.Text = "Error: btnHidRFPCreate_Click() " + ex.Message()
        End Try
    End Sub

    Protected Sub btnRefreshVList_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefreshVList.Click
        Try
            'ScriptManager.RegisterStartupScript(Page, Me.GetType(), "ReloadDefault", "ReloadDefault();", True)
            'GetUsersList()
            'GetPriceCost()
            'loadTab()
            GetPriceCost()
        Catch ex As Exception
            lblError.Text = "Error: btnRefreshVList_Click() " + ex.Message()
        End Try
    End Sub

#End Region

#Region "Price and Cost"

    Protected Sub GetPriceCost()
        Dim Ds As New DataSet
        Dim dsS As New DataSet
        Dim dsC As New DataSet
        Try
            dsS = objGetdata.GetPriceOptionStandatrd(Session("hidRfpID"))
            dsC = objGetdata.GetPriceOptionCustomize(Session("hidRfpID"))
            Session("GrdPriceOption") = dsS
            Session("GrdPriceOptionC") = dsC

            If dsS.Tables(0).Rows.Count > 0 Then
                lblPPS.Visible = True
                GrdPriceOption.Visible = True
                GrdPriceOption.DataSource = dsS
                GrdPriceOption.DataBind()
            Else
                lblPPS.Visible = False
                GrdPriceOption.Visible = False

            End If

            BindPriceOption()

            If dsC.Tables(0).Rows.Count > 0 Then
                GrdCustomize.Visible = True
                lblPPC.Visible = True
                GrdCustomize.DataSource = dsC
                GrdCustomize.DataBind()
                BindPriceOptionC()

            Else
                lblPPC.Visible = False
                GrdCustomize.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = "Error:BindUser:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub BindPriceOption()
        Dim DsExistusr As New DataSet
        Dim lblPriceID As New Label
        Dim check As New CheckBox
        Dim lnkPriceOption As LinkButton
        Dim strArr() As String
        Try
            DsExistusr = objGetdata.GetRFPbyID(Session("hidRfpID"))
            For Each Gr As GridViewRow In GrdPriceOption.Rows
                lblPriceID = GrdPriceOption.Rows(Gr.RowIndex).FindControl("lblPriceID")
                lnkPriceOption = GrdPriceOption.Rows(Gr.RowIndex).FindControl("lnkPriceOption")
                check = GrdPriceOption.Rows(Gr.RowIndex).FindControl("selectP")

                If DsExistusr.Tables(0).Rows(0).Item("PRICEOPTIONID").ToString() <> "" Then
                    strArr = DsExistusr.Tables(0).Rows(0).Item("PRICEOPTIONID").ToString().Split(",")
                    For i = 0 To strArr.Length - 1
                        If lblPriceID.Text = strArr(i) Then
                            check.Checked = True
                        End If
                    Next
                End If

                If lblPriceID.Text = "1" Then
                    lnkPriceOption.Attributes.Add("onclick", "return ShowPopWindow_PriceCost('Popup/PricePopup.aspx?PriceOptnID=1');")
                ElseIf lblPriceID.Text = "2" Then
                    lnkPriceOption.Attributes.Add("onclick", "return ShowPopWindow_Price('Popup/PricePopup.aspx?PriceOptnID=2');")
                Else
                    lnkPriceOption.Attributes.Add("onclick", "return ShowPopUpPriceOptionStandard('BuyersPrice.aspx?PriceID=" + lblPriceID.Text + "'); return false;")
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub BindPriceOptionC()
        Dim DsExistusr As New DataSet
        Dim lblPriceID As New Label
        Dim check As New CheckBox
        Dim lnkPriceOption As LinkButton
        Dim strArr() As String
        Try
            DsExistusr = objGetdata.GetRFPbyID(Session("hidRfpID"))
            For Each Gr As GridViewRow In GrdCustomize.Rows
                lblPriceID = GrdCustomize.Rows(Gr.RowIndex).FindControl("lblPriceIDC")
                lnkPriceOption = GrdCustomize.Rows(Gr.RowIndex).FindControl("lnkPriceOptionC")
                check = GrdCustomize.Rows(Gr.RowIndex).FindControl("selectPC")

                If DsExistusr.Tables(0).Rows(0).Item("PRICEOPTIONID").ToString() <> "" Then
                    strArr = DsExistusr.Tables(0).Rows(0).Item("PRICEOPTIONID").ToString().Split(",")
                    For i = 0 To strArr.Length - 1
                        If lblPriceID.Text = strArr(i) Then
                            check.Checked = True
                        End If
                    Next
                End If

                If lblPriceID.Text = "1" Then
                    lnkPriceOption.Attributes.Add("onclick", "return ShowPopWindow_PriceCost('Popup/PricePopup.aspx?PriceOptnID=1');")
                ElseIf lblPriceID.Text = "2" Then
                    lnkPriceOption.Attributes.Add("onclick", "return ShowPopWindow_Price('Popup/PricePopup.aspx?PriceOptnID=2');")
                Else
                    lnkPriceOption.Attributes.Add("onclick", "return ShowPopUpPriceOptionStandard('BuyersPrice.aspx?PriceID=" + lblPriceID.Text + "'); return false;")
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CheckBox_CheckPriceOption(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim strchk As String = String.Empty
        Dim check As New CheckBox
        Dim dsC As New DataSet
        Try
            dsC = Session("GrdPriceOptionC")

            For Each Gr As GridViewRow In GrdPriceOption.Rows
                check = GrdPriceOption.Rows(Gr.RowIndex).FindControl("SelectP")
                If check.Checked = True Then
                    Dim PriceId As Integer = Convert.ToInt32(GrdPriceOption.DataKeys(Gr.RowIndex).Values(0))
                    strchk += PriceId.ToString() + ","
                End If
            Next
            If dsC.Tables(0).Rows.Count > 0 Then
                For Each Gr As GridViewRow In GrdCustomize.Rows
                    check = GrdCustomize.Rows(Gr.RowIndex).FindControl("SelectPC")
                    If check.Checked = True Then
                        Dim PriceId As Integer = Convert.ToInt32(GrdCustomize.DataKeys(Gr.RowIndex).Values(0))
                        strchk += PriceId.ToString() + ","
                    End If
                Next

            End If

            If strchk <> "" Then
                strchk = strchk.Trim().Remove(strchk.Length - 1)
                objUpIns.UpdatePriceOp(Session("hidRfpID"), strchk)
                ScriptManager.RegisterStartupScript(Page, Me.GetType(), "Alert", "alert('Price option updated successfully.');", True)
            Else
                objUpIns.UpdatePriceOp(Session("hidRfpID"), strchk)
                ScriptManager.RegisterStartupScript(Page, Me.GetType(), "Alert", "alert('Price option updated successfully.');", True)
            End If
            GetPriceCost()
        Catch ex As Exception
            lblError.Text = "Error:CheckBox_CheckPriceOption" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GrdPriceOption_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GrdPriceOption.PageIndexChanging
        Try
            GrdPriceOption.PageIndex = e.NewPageIndex
            BindGrdPriceOption()
        Catch ex As Exception
            Response.Write("Error:GrdPriceOption_PageIndexChanging:" + ex.Message.ToString())
        End Try
    End Sub

    Protected Sub BindGrdPriceOption()
        Try
            Dim Dts As New DataSet
            Dts = Session("GrdPriceOption")
            GrdPriceOption.DataSource = Dts
            GrdPriceOption.DataBind()
            BindPriceOption()
        Catch ex As Exception
            Response.Write("Error:BindGrdPriceOption:" + ex.Message.ToString())
        End Try
    End Sub

    Protected Sub GrdCustomize_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GrdCustomize.PageIndexChanging
        Try
            GrdCustomize.PageIndex = e.NewPageIndex
            BindGrdPriceOptionC()
        Catch ex As Exception
            Response.Write("Error:GrdPriceOption_PageIndexChanging:" + ex.Message.ToString())
        End Try
    End Sub

    Protected Sub BindGrdPriceOptionC()
        Try
            Dim Dts As New DataSet
            Dts = Session("GrdPriceOptionC")
            GrdCustomize.DataSource = Dts
            GrdCustomize.DataBind()
            BindPriceOptionC()
        Catch ex As Exception
            Response.Write("Error:BindGrdPriceOption:" + ex.Message.ToString())
        End Try
    End Sub

    Protected Sub CheckBox_CheckPriceOptionC(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim strchk As String = String.Empty
        Dim check As New CheckBox
        Try

            For Each Gr As GridViewRow In GrdPriceOption.Rows
                check = GrdPriceOption.Rows(Gr.RowIndex).FindControl("SelectP")
                If check.Checked = True Then
                    Dim PriceId As Integer = Convert.ToInt32(GrdPriceOption.DataKeys(Gr.RowIndex).Values(0))
                    strchk += PriceId.ToString() + ","
                End If
            Next

            For Each Gr As GridViewRow In GrdCustomize.Rows
                check = GrdCustomize.Rows(Gr.RowIndex).FindControl("SelectPC")
                If check.Checked = True Then
                    Dim PriceId As Integer = Convert.ToInt32(GrdCustomize.DataKeys(Gr.RowIndex).Values(0))
                    strchk += PriceId.ToString() + ","
                End If
            Next

            If strchk <> "" Then
                strchk = strchk.Trim().Remove(strchk.Length - 1)
                objUpIns.UpdatePriceOp(Session("hidRfpID"), strchk)
                ScriptManager.RegisterStartupScript(Page, Me.GetType(), "Alert", "alert('Price option updated successfully.');", True)
            Else
                objUpIns.UpdatePriceOp(Session("hidRfpID"), strchk)
                ScriptManager.RegisterStartupScript(Page, Me.GetType(), "Alert", "alert('Price option updated successfully.');", True)
            End If
            GetPriceCost()
        Catch ex As Exception
            lblError.Text = "Error:CheckBox_CheckPriceOptionC" + ex.Message.ToString()
        End Try
    End Sub


#End Region

#Region "Operating Option"

    Protected Sub GetOpOption()
        Dim Ds As New DataSet
        Try
            Ds = objGetdata.GetOption()
            If Ds.Tables(0).Rows.Count > 0 Then
                GrdOpOption.Visible = True
                GrdOpOption.DataSource = Ds
                GrdOpOption.DataBind()
            Else
                GrdOpOption.Visible = False
            End If
            BindOpOption()
        Catch ex As Exception
            lblError.Text = "Error:GetOpOption:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub BindOpOption()
        Dim DsExistSpec As New DataSet
        Dim lblOpID As New Label
        Dim check As New CheckBox
        Dim lblOption As Label
        Dim strArr() As String
        Try
            DsExistSpec = objGetdata.GetOPDetails(Session("hidRfpID"), Session("USERID"))
            For Each Gr As GridViewRow In GrdOpOption.Rows
                lblOpID = GrdOpOption.Rows(Gr.RowIndex).FindControl("lblOptionID")
                lblOption = GrdOpOption.Rows(Gr.RowIndex).FindControl("lblOpOption")
                check = GrdOpOption.Rows(Gr.RowIndex).FindControl("selectO")

                For i = 0 To DsExistSpec.Tables(0).Rows.Count - 1
                    strArr = DsExistSpec.Tables(0).Rows(0).Item("OPOPTIONID").ToString().Split(",")
                    For j = 0 To strArr.Length - 1
                        If lblOpID.Text = strArr(j) Then
                            check.Checked = True
                        End If
                    Next
                Next
            Next
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub CheckBox_CheckOpOption(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim strchk As String = String.Empty
        Dim check As New CheckBox
        Dim DsExistSpec As DataSet
        Dim SpecId As String
        Try
            DsExistSpec = objGetdata.GetSKUByRfpID(Session("hidRfpID"), Session("USERID"))
            For Each Gr As GridViewRow In GrdOpOption.Rows
                check = GrdOpOption.Rows(Gr.RowIndex).FindControl("SelectO")
                If check.Checked = True Then
                    Dim OpId As Integer = Convert.ToInt32(GrdOpOption.DataKeys(Gr.RowIndex).Values(0))
                    strchk += OpId.ToString() + ","
                End If
            Next
            ' For i = 0 To DsExistSpec.Tables(0).Rows.Count - 1
            If strchk <> "" Then
                strchk = strchk.Trim().Remove(strchk.Length - 1)
                'SpecId = "10"
                objUpIns.UpdateOpOption(Session("hidRfpID"), strchk)
                ScriptManager.RegisterStartupScript(Page, Me.GetType(), "Alert", "alert('Option updated successfully.');", True)
            Else
                'SpecId = "10"
                objUpIns.UpdateOpOption(Session("hidRfpID"), strchk)
                ScriptManager.RegisterStartupScript(Page, Me.GetType(), "Alert", "alert('Option updated successfully.');", True)
            End If
            ' Next
            GetOpOption()
        Catch ex As Exception
            lblError.Text = "Error:CheckBox_CheckOpOption" + ex.Message.ToString()
        End Try
    End Sub
#End Region

    '#Region "Set Preferences"

    '    Protected Sub GetPrefDetails()
    '        Dim ds As New DataSet
    '        Try
    '            ds = objGetdata.GetPrefD(Session("hidRfpID"))
    '            GetCurrancy()

    '            'If ds.Tables(0).Rows(0).Item("UNITS").ToString() = "0" Then
    '            '    ddlCurrancy.SelectedValue = "0"
    '            'Else
    '            ddlCurrancy.SelectedValue = ds.Tables(0).Rows(0).Item("CURRENCY").ToString()
    '            ' End If


    '            If ds.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
    '                rdMetric.Checked = True
    '                rdEnglish.Checked = False
    '            Else
    '                rdMetric.Checked = False
    '                rdEnglish.Checked = True
    '            End If


    '        Catch ex As Exception
    '            lblError.Text = "Error:GetPrefDetails:" + ex.Message.ToString()
    '        End Try
    '    End Sub

    '    Protected Sub GetCurrancy()
    '        Dim ds As New DataSet
    '        Dim objGetData As New E1GetData.Selectdata


    '        Try
    '            ds = objGetData.GetCurrancy("-1")

    '            With ddlCurrancy
    '                .DataSource = ds
    '                .DataTextField = "CURDE1"
    '                .DataValueField = "CURID"
    '                .DataBind()
    '                .Font.Size = 8
    '            End With

    '        Catch ex As Exception
    '            lblError.Text = "Error:GetCurrancy:" + ex.Message.ToString()
    '        End Try
    '    End Sub

    '    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
    '        Dim Unit As String = String.Empty
    '        Dim Currancy As String = String.Empty
    '        Try
    '            If rdEnglish.Checked Then
    '                Unit = "0"
    '            Else
    '                Unit = "1"
    '            End If

    '            Currancy = ddlCurrancy.SelectedValue.ToString()
    '            objUpIns.PrefrencesUpdate(Session("hidRfpID"), Currancy, Unit)
    '            ScriptManager.RegisterStartupScript(Page, Me.GetType(), "Alert", "alert('Preferences updated successfully.');", True)
    '            ' Calculate()
    '            'Update Server Date
    '            'ObjUpIns.ServerDateUpdate(CaseId, Session("E1UserName"))
    '            GetPrefDetails()
    '        Catch ex As Exception
    '            lblError.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
    '        End Try
    '    End Sub
    '#End Region

    Protected Sub btnSearch1_Click(sender As Object, e As System.EventArgs) Handles btnSearch1.Click
        GetUsersList()
    End Sub
End Class
