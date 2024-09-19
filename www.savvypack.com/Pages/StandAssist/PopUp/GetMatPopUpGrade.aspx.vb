Imports System.Data
Imports System.Data.OleDb
Imports System
Imports StandGetData
Imports StandUpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.Services.WebService
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_StandAssist_PopUp_GetMatPopUpGrade
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim objUpIns As New StandUpInsData.UpdateInsert
            If Session("Back") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If
            hidMatdes.Value = Request.QueryString("Des").ToString()
            hidMatid.Value = Request.QueryString("ID").ToString()

            hidGradedes.Value = Request.QueryString("GradeDes").ToString()
            hidGradeid.Value = Request.QueryString("GradeId").ToString()

            hidSG.Value = Request.QueryString("SG").ToString()
            hidMatD.Value = Request.QueryString("MatDes").ToString()

            hidLinkId.Value = Request.QueryString("LinkId").ToString()


            If Not IsPostBack Then
                hidMatGrp.Value = Request.QueryString("Grp").ToString().Trim().ToUpper()
                If Request.QueryString("Flag") <> "Y" Then
                    'Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "testData();", True)
                    'hidSession.Value = "0"
                Else
                    'hidSession.Value = "1"
                End If
                hvUserGrd.Value = "0"
                hvGroupQ.Value = Request.QueryString("Grp").ToString().Trim().ToUpper()
                hidMatCat.Value = Request.QueryString("Grp").ToString().Trim().ToUpper()

                'Started Activity Log Changes
                Try
                    If Request.QueryString("FLAG") <> "Y" Then
                        objUpIns.InsertLog1(Session("UserId").ToString(), "11", "Opened Material Selector PopUp", Session("SBACaseId"), Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                    Else
                        objUpIns.InsertLog1(Session("UserId").ToString(), "11", "Selected Material Group: " + hidMatGrp.Value + " ", Session("SBACaseId"), Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")

                    End If
                Catch ex As Exception

                End Try
                'Ended Activity Log Changes

            End If



            If hidMatGrp.Value <> "NOTHING" Then
                Link_Click()
            End If
            GetMaterialGroupDetails()

        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            If hidMatGrp.Value <> "N" Then
                Link_Click()
				
				'Started Activity Log Changes
            Try
                Dim objUpIns As New StandUpInsData.UpdateInsert
                objUpIns.InsertLog1(Session("UserId").ToString(), "11", "Clicked on Search button, Searched Text: " + txtMatDe1.Text + "", Session("SBACaseId"), Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
            Catch ex As Exception

            End Try
            'Ended Activity Log Changes
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:btnSearch_Click:" + ex.Message.ToString()
        End Try
    End Sub


    Public Sub Link_Click()
        Dim ds As New DataSet
        Dim dt As New DataTable
        Dim objGetData As New StandGetData.Selectdata()

        Try
            If hidMatGrp.Value <> "N" And hidMatGrp.Value <> "Nothing " Then
                If (hidMatGrp.Value).ToUpper().Trim() = "RESIN" Then
                    ds = objGetData.GetResinMaterialbyGroups(-1, txtMatDe1.Text.ToString(), hidMatGrp.Value.Trim())
                ElseIf (hidMatGrp.Value).ToUpper() = "FILM" Then
                    ds = objGetData.GetFilmMaterialbyGroups(-1, txtMatDe1.Text.ToString(), hidMatGrp.Value.Trim())
                    'ds = GetFilmMaterialbyGroups(-1, txtMatDe1.Text.ToString(), hidMatGrp.Value.Trim())
                ElseIf (hidMatGrp.Value).ToUpper().Trim() = "ADHESIVE" Then

                    ds = objGetData.GetAdhesiveMaterialNew(-1, txtMatDe1.Text.ToString(), hidMatGrp.Value.Trim())
                ElseIf (hidMatGrp.Value).ToUpper().Trim() = "ALUMINUM" Then
                    ds = objGetData.GetAluminMaterialNew(-1, txtMatDe1.Text.ToString(), hidMatGrp.Value.Trim())

                ElseIf (hidMatGrp.Value).ToUpper().Trim() = "COATING" Then

                    ds = objGetData.GetCoatingMaterialbyGroups(-1, txtMatDe1.Text.ToString(), hidMatGrp.Value.Trim())
                ElseIf (hidMatGrp.Value).ToUpper().Trim() = "BASE FIBER" Then

                    ds = objGetData.GetBaseFMaterialbyGroups(-1, txtMatDe1.Text.ToString(), hidMatGrp.Value.Trim())
                ElseIf (hidMatGrp.Value).ToUpper().Trim() = "CONCENTRATE" Then

                    ds = objGetData.GetConcentrateMaterialbyGroups(-1, txtMatDe1.Text.ToString(), hidMatGrp.Value.Trim())
                ElseIf (hidMatGrp.Value).ToUpper().Trim() = "GLASS" Then

                    ds = objGetData.GetGlassMaterialbyGroups(-1, txtMatDe1.Text.ToString(), hidMatGrp.Value.Trim())
                ElseIf (hidMatGrp.Value).ToUpper().Trim() = "INK" Then

                    ds = objGetData.GetInkMaterialbyGroups(-1, txtMatDe1.Text.ToString(), hidMatGrp.Value.Trim())
                ElseIf (hidMatGrp.Value).ToUpper().Trim() = "NON-WOVEN" Then

                    ds = objGetData.GetNonWMaterialbyGroups(-1, txtMatDe1.Text.ToString(), hidMatGrp.Value.Trim())
                ElseIf (hidMatGrp.Value).ToUpper().Trim() = "PAPER" Then

                    ds = objGetData.GetPaperMaterialbyGroups(-1, txtMatDe1.Text.ToString(), hidMatGrp.Value.Trim())
                ElseIf (hidMatGrp.Value).ToUpper().Trim() = "PAPERBOARD" Then

                    ds = objGetData.GetPaperBMaterialbyGroups(-1, txtMatDe1.Text.ToString(), hidMatGrp.Value.Trim())
                ElseIf (hidMatGrp.Value).ToUpper().Trim() = "SHEET" Then

                    ds = objGetData.GetSheetMaterialbyGroups(-1, txtMatDe1.Text.ToString(), hidMatGrp.Value.Trim())
                ElseIf (hidMatGrp.Value).ToUpper().Trim() = "STEEL" Then
                    ds = objGetData.GetSteelMaterialbyGroups(-1, txtMatDe1.Text.ToString(), hidMatGrp.Value.Trim())
                End If
                Session("UsersDataGroup") = ds
                If ds.Tables(0).Rows.Count > 0 Then

                    grdMaterials.DataSource = ds
                   If (hidMatGrp.Value).ToUpper() = "RESIN" Then
                        grdMaterials.Columns(4).Visible = True
                        grdMaterials.Columns(5).Visible = True
                        grdMaterials.Columns(6).Visible = True
                        grdMaterials.Columns(7).Visible = True
                        grdMaterials.Columns(8).Visible = True
                        grdMaterials.Columns(9).Visible = True
                        'grdMaterials.Columns(10).Visible = True
                        grdMaterials.Columns(11).Visible = True
                        'grdMaterials.Columns(12).Visible = True
                        'grdMaterials.Columns(13).Visible = True
                        'grdMaterials.Columns(14).Visible = True
                        'grdMaterials.Columns(15).Visible = True

                    ElseIf (hidMatGrp.Value).ToUpper() = "ADHESIVE" Then
                        grdMaterials.Columns(27).Visible = True
                        grdMaterials.Columns(40).Visible = True
                        'grdMaterials.Columns(41).Visible = True
                        grdMaterials.Columns(16).Visible = True


                    ElseIf (hidMatGrp.Value).ToUpper() = "ALUMINUM" Then
                        grdMaterials.Columns(19).Visible = True
                        'grdMaterials.Columns(20).Visible = True
                        ' grdMaterials.Columns(21).Visible = True

                    ElseIf (hidMatGrp.Value).ToUpper() = "BASE FIBER" Then
                        grdMaterials.Columns(4).Visible = True


                    ElseIf (hidMatGrp.Value).ToUpper().Trim() = "COATING" Then
                        grdMaterials.Columns(4).Visible = True
                        grdMaterials.Columns(16).Visible = True
                        grdMaterials.Columns(12).Visible = True
                        grdMaterials.Columns(17).Visible = True
                        grdMaterials.Columns(22).Visible = True

                    ElseIf (hidMatGrp.Value).ToUpper().Trim() = "CONCENTRATE" Then
                        grdMaterials.Columns(23).Visible = True

                    ElseIf (hidMatGrp.Value).ToUpper().Trim() = "FILM" Then
                        grdMaterials.Columns(31).Visible = True
                        grdMaterials.Columns(32).Visible = True
                        grdMaterials.Columns(33).Visible = True
                        grdMaterials.Columns(34).Visible = True
                        grdMaterials.Columns(35).Visible = True
                        grdMaterials.Columns(36).Visible = True
                        grdMaterials.Columns(37).Visible = True
                        grdMaterials.Columns(38).Visible = True
                        grdMaterials.Columns(4).Visible = True
                        grdMaterials.Columns(39).Visible = True

                    ElseIf (hidMatGrp.Value).ToUpper().Trim() = "GLASS" Then
                        grdMaterials.Columns(4).Visible = True
                        grdMaterials.Columns(25).Visible = True

                    ElseIf (hidMatGrp.Value).ToUpper().Trim() = "INK" Then
                        grdMaterials.Columns(26).Visible = True
                        grdMaterials.Columns(27).Visible = True
                        'grdMaterials.Columns(16).Visible = True
                        grdMaterials.Columns(17).Visible = True

                        
                    ElseIf (hidMatGrp.Value).ToUpper().Trim() = "NON-WOVEN" Then
                        grdMaterials.Columns(4).Visible = True
                        grdMaterials.Columns(28).Visible = True

                    ElseIf (hidMatGrp.Value).ToUpper().Trim() = "PAPER" Then
                        grdMaterials.Columns(29).Visible = True
                        grdMaterials.Columns(30).Visible = True

                    ElseIf (hidMatGrp.Value).ToUpper().Trim() = "PAPERBOARD" Then
                        grdMaterials.Columns(29).Visible = True
                        grdMaterials.Columns(30).Visible = True

                    ElseIf (hidMatGrp.Value).ToUpper().Trim() = "SHEET" Then
                        grdMaterials.Columns(4).Visible = True

                    ElseIf (hidMatGrp.Value).ToUpper().Trim() = "STEEL" Then
                        grdMaterials.Columns(4).Visible = True

                    End If

                    grdMaterials.DataBind()

                End If

            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:Link_Click:" + ex.Message.ToString()
        End Try
    End Sub
  
   Protected Sub GetMaterialGroupDetails()
        Dim ds As New DataSet
        Dim objGetData As New StandGetData.Selectdata()
        Dim trHeader As New TableRow
        Dim trHeader1 As New TableRow
        Dim trHeader2 As New TableRow
        Dim trInner As New TableRow
        Dim lbl As New Label
        Dim hid As New HiddenField
        Dim hidGrade As New HiddenField
        Dim hidDes As New HiddenField
        Dim Link As New HyperLink
        Dim txt As New TextBox
        Dim tdInner As TableCell
        Try
            ds = objGetData.GetMaterialGroups()
            For i = 0 To ds.Tables(0).Rows.Count - 1
                trInner = New TableRow
                For j = 1 To 3
                    tdInner = New TableCell
                    If i < ds.Tables(0).Rows.Count Then
                        Select Case j
                            Case 1
                                InnerTdSetting(tdInner, "", "Left")
                                Link = New HyperLink
                                hid = New HiddenField
                                hidDes = New HiddenField
                                Link.ID = "lnk" + i.ToString()
                                Link.Width = 120
                                Link.CssClass = "LinkMat"
                                Link.Text = ds.Tables(0).Rows(i).Item("MATNAME").ToString()
                                Link.ToolTip = ds.Tables(0).Rows(i).Item("MATNAME").ToString()
                                
                                If (hvGroupQ.Value).ToUpper() = Link.Text Then
                                    Link.ForeColor = Drawing.Color.Red
                                    hidMatGrp.Value = hvGroupQ.Value
                                    ' hvGroupQ.Value = Nothing
                                End If
                                'If hidLinkId.Value = Link.ID Then
                                '    Link.ForeColor = Drawing.Color.Red
                                'End If

                                tdInner.Controls.Add(Link)
                                trInner.Controls.Add(tdInner)
                                Link.NavigateUrl = "GetMatPopUpGrade.aspx?Des=" + hidMatdes.Value + "&Id=" + hidMatid.Value + "&GradeDes=" + hidGradedes.Value + "&GradeId=" + hidGradeid.Value + "&SG=" + hidSG.Value + "&MatDes=" + hidMatD.Value + " &Grp=" + Link.Text + "&LinkId=" + Link.ID + "&Flag=Y"
                                i = i + 1

                            Case 2
                                InnerTdSetting(tdInner, "", "Left")
                                Link = New HyperLink
                                hid = New HiddenField
                                hidDes = New HiddenField
                                Link.ID = "lnk" + i.ToString()
                                Link.Width = 120
                                Link.CssClass = "LinkMat"
                                Link.Text = ds.Tables(0).Rows(i).Item("MATNAME").ToString()
                                Link.ToolTip = ds.Tables(0).Rows(i).Item("MATNAME").ToString()

                                If (hvGroupQ.Value).ToUpper() = Link.Text Then
                                    Link.ForeColor = Drawing.Color.Red
                                    hidMatGrp.Value = hvGroupQ.Value
                                    'hvGroupQ.Value = Nothing
                                End If
                                'If hidLinkId.Value = Link.ID Then
                                '    Link.ForeColor = Drawing.Color.Red
                                'End If

                                tdInner.Controls.Add(Link)
                                trInner.Controls.Add(tdInner)
                                Link.NavigateUrl = "GetMatPopUpGrade.aspx?Des=" + hidMatdes.Value + "&Id=" + hidMatid.Value + "&GradeDes=" + hidGradedes.Value + "&GradeId=" + hidGradeid.Value + "&SG=" + hidSG.Value + "&MatDes=" + hidMatD.Value + "&Grp=" + Link.Text + "&LinkId=" + Link.ID + "&Flag=Y"
                                i = i + 1

                            Case 3
                                InnerTdSetting(tdInner, "", "Left")
                                Link = New HyperLink
                                hid = New HiddenField
                                hidDes = New HiddenField
                                Link.ID = "lnk" + i.ToString()
                                Link.Width = 90
                                Link.CssClass = "LinkMat"
                                Link.Text = ds.Tables(0).Rows(i).Item("MATNAME").ToString()
                                Link.ToolTip = ds.Tables(0).Rows(i).Item("MATNAME").ToString()

                                If (hvGroupQ.Value).ToUpper() = Link.Text Then
                                    Link.ForeColor = Drawing.Color.Red
                                    hidMatGrp.Value = hvGroupQ.Value
                                    ' hvGroupQ.Value = Nothing
                                End If
                                'If hidLinkId.Value = Link.ID Then
                                '    Link.ForeColor = Drawing.Color.Red
                                'End If
                                tdInner.Controls.Add(Link)
                                trInner.Controls.Add(tdInner)
                                Link.NavigateUrl = "GetMatPopUpGrade.aspx?Des=" + hidMatdes.Value + "&Id=" + hidMatid.Value + "&GradeDes=" + hidGradedes.Value + "&GradeId=" + hidGradeid.Value + "&SG=" + hidSG.Value + "&MatDes=" + hidMatD.Value + " &Grp=" + Link.Text + "&LinkId=" + Link.ID + "&Flag=Y"
                        End Select
                    End If
                    
                Next

                trInner.Height = 10
                tblMaterials.Controls.Add(trInner)
            Next
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetMaterialGroupDetails:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub grdMaterials_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles grdMaterials.RowDataBound
        Dim ds As New DataSet()
        ds = CType(Session("UsersDataGroup"), DataSet)
        If (hidMatGrp.Value).ToUpper().Trim() = "RESIN" Then
            If (e.Row.RowType = DataControlRowType.DataRow) Then

                If (e.Row.RowIndex = 0) Then
                    e.Row.Style.Add("height", "70px")
                    e.Row.Style.Add("vertical-align", "bottom")
                End If
            End If
        ElseIf (hidMatGrp.Value).ToUpper().Trim() = "FILM" Then
            If (e.Row.RowType = DataControlRowType.DataRow) Then

                If (e.Row.RowIndex = 0) Then
                    e.Row.Style.Add("height", "75px")
                    e.Row.Style.Add("vertical-align", "bottom")
                End If
            End If
        Else
            If (e.Row.RowType = DataControlRowType.DataRow) Then

                If (e.Row.RowIndex = 0) Then
                    e.Row.Style.Add("height", "44px")
                    e.Row.Style.Add("vertical-align", "bottom")
                End If
            End If
        End If
        GridSize()
        If ds.Tables(0).Rows().Count > 0 Then
            If e.Row.RowType = DataControlRowType.DataRow Then
                If ds.Tables(0).Rows(e.Row.RowIndex).Item("MATID").ToString() <> "0" Then
                    Dim btn As LinkButton = DirectCast(e.Row.FindControl("lnkBtnval"), LinkButton)
                    If (hidMatGrp.Value).ToUpper() = "RESIN" Then
                        Dim GradeV() As String
                        Dim GradeValue As String = String.Empty

                        Dim MouseoverV() As String
                        Dim MouseoverV1() As String
                        Dim MouseoverValue As String = String.Empty


                        Dim i As Integer = 0
                        If ds.Tables(0).Rows(e.Row.RowIndex).Item("GRADECOL").ToString() <> "" Then
                            GradeV = Regex.Split(ds.Tables(0).Rows(e.Row.RowIndex).Item("GRADECOL").Trim().ToString(), ",")
                            If GradeV.Length > 0 Then
                                For i = 0 To GradeV.Length - 1
                                    If i = 0 Then
                                        GradeValue = ds.Tables(0).Rows(e.Row.RowIndex).Item("" + GradeV(i).Trim() + "").ToString()
                                    Else
                                        GradeValue = GradeValue + ", " + ds.Tables(0).Rows(e.Row.RowIndex).Item("" + GradeV(i).Trim() + "").ToString()
                                    End If

                                Next
                            End If
                        End If

                        If ds.Tables(0).Rows(e.Row.RowIndex).Item("MOUSEOVERCOL").ToString() <> "" Then
                            MouseoverV = Regex.Split(ds.Tables(0).Rows(e.Row.RowIndex).Item("MOUSEOVERCOL").Trim().ToString(), ",")
                            MouseoverV1 = Regex.Split(ds.Tables(0).Rows(e.Row.RowIndex).Item("MOUSEOVERCOL").Trim().ToString(), ",")
                            If MouseoverV.Length > 0 Then
                                For i = 0 To MouseoverV.Length - 1
                                    If MouseoverV(i).ToString() = "MATERIAL" Then
                                        MouseoverV1(i) = "Material Description"
                                    ElseIf MouseoverV(i).ToString() = "MATDES" Then
                                        MouseoverV1(i) = "Material Category"
                                    ElseIf MouseoverV(i).ToString() = "DENSITY" Then
                                        MouseoverV1(i) = "Density"
                                    ElseIf MouseoverV(i).ToString() = "PROCESS" Then
                                        MouseoverV1(i) = "Process"
                                    ElseIf MouseoverV(i).ToString() = "POLYMERSTRUCT" Then
                                        MouseoverV1(i) = "Polymer Structure"
                                    ElseIf MouseoverV(i).ToString() = "POLYMERDESC" Then
                                        MouseoverV1(i) = "Polymer Description"
                                    ElseIf MouseoverV(i).ToString() = "VISC" Then
                                        MouseoverV1(i) = "Viscosity"
                                    ElseIf MouseoverV(i).ToString() = "MELTFRATE" Then
                                        MouseoverV1(i) = "Melt Flow Rate"
                                    ElseIf MouseoverV(i).ToString() = "MELTINDEX" Then
                                        MouseoverV1(i) = "Melt Index"
                                    ElseIf MouseoverV(i).ToString() = "ETHYLENE" Then
                                        MouseoverV1(i) = "% Ethylene"
                                    ElseIf MouseoverV(i).ToString() = "TYPE" Then
                                        MouseoverV1(i) = "Type"
                                    ElseIf MouseoverV(i).ToString() = "ANHYDRIDEMOD" Then
                                        MouseoverV1(i) = "Anhydride Modification"
                                    ElseIf MouseoverV(i).ToString() = "WATERACTIVITY" Then
                                        MouseoverV1(i) = "Water Activity "
                                    ElseIf MouseoverV(i).ToString() = "ADHESIVE" Then
                                        MouseoverV1(i) = "Adhesive"
                                    End If
                                    If i = 0 Then

                                        MouseoverValue = "<b>" + MouseoverV1(i) + "</b>=" + ds.Tables(0).Rows(e.Row.RowIndex).Item("" + MouseoverV(i).Trim() + "").ToString() + "</br>"
                                    Else

                                        MouseoverValue = MouseoverValue + "<b>" + MouseoverV1(i) + "</b>=" + ds.Tables(0).Rows(e.Row.RowIndex).Item("" + MouseoverV(i).Trim() + "").ToString() + "</br>"
                                    End If

                                Next
                            End If
                        End If
                        btn.Attributes("onclick") = "javascript:return MaterialDetail('" + ds.Tables(0).Rows(e.Row.RowIndex).Item("MATDES").ToString() + "'," + ds.Tables(0).Rows(e.Row.RowIndex).Item("MATID").ToString() + ",'" + GradeValue + "','" + MouseoverValue + "','" + ds.Tables(0).Rows(e.Row.RowIndex).Item("MATDE2").ToString() + "')"

                    Else
                        Dim GradeV() As String
                        Dim GradeValue As String = String.Empty

                        Dim MouseoverV() As String
                        Dim MouseoverV1() As String
                        Dim MouseoverValue As String = String.Empty


                        Dim i As Integer = 0
                        If ds.Tables(0).Rows(e.Row.RowIndex).Item("GRADECOL").Trim().ToString() <> "" Then
                            GradeV = Regex.Split(ds.Tables(0).Rows(e.Row.RowIndex).Item("GRADECOL").Trim().ToString(), ",")
                            If GradeV.Length > 0 Then
                                For i = 0 To GradeV.Length - 1
                                    If i = 0 Then
                                        GradeValue = ds.Tables(0).Rows(e.Row.RowIndex).Item("" + GradeV(i).Trim() + "").ToString()
                                    Else
                                        GradeValue = GradeValue + ", " + ds.Tables(0).Rows(e.Row.RowIndex).Item("" + GradeV(i).Trim() + "").ToString()
                                    End If

                                Next
                            End If
                        End If

                        If ds.Tables(0).Rows(e.Row.RowIndex).Item("MOUSEOVERCOL").Trim().ToString() <> "" Then
                            MouseoverV = Regex.Split(ds.Tables(0).Rows(e.Row.RowIndex).Item("MOUSEOVERCOL").Trim().ToString(), ",")
                            MouseoverV1 = Regex.Split(ds.Tables(0).Rows(e.Row.RowIndex).Item("MOUSEOVERCOL").Trim().ToString(), ",")
                            If MouseoverV.Length > 0 Then
                                For i = 0 To MouseoverV.Length - 1
                                    If MouseoverV(i).ToString() = "DESCRIPTION" Then
                                        MouseoverV1(i) = "Material Description"
                                    ElseIf MouseoverV(i).ToString() = "TECHDESCRIPTION" Then
                                        MouseoverV1(i) = "Technical Description"
                                    ElseIf MouseoverV(i).ToString() = "SOLVENT" Then
                                        MouseoverV1(i) = "Solvent"
                                    ElseIf MouseoverV(i).ToString() = "DILUENT" Then
                                        MouseoverV1(i) = "Diluent"
                                    ElseIf MouseoverV(i).ToString() = "FEATURE" Then
                                        MouseoverV1(i) = "Feature"
                                    ElseIf MouseoverV(i).ToString() = "COATING" Then
                                        MouseoverV1(i) = "Coating"
                                    ElseIf MouseoverV(i).ToString() = "THICKNESS" Then
                                        MouseoverV1(i) = "Thickness"
                                    ElseIf MouseoverV(i).ToString() = "PARTICLESIZE" Then
                                        MouseoverV1(i) = "Particle Size"
                                    ElseIf MouseoverV(i).ToString() = "CURING" Then
                                        MouseoverV1(i) = "Curing"
                                    ElseIf MouseoverV(i).ToString() = "SUBSTRATE" Then
                                        MouseoverV1(i) = "Substrate"
                                    ElseIf MouseoverV(i).ToString() = "SUBSTRATES" Then
                                        MouseoverV1(i) = "Substrates"
                                    ElseIf MouseoverV(i).ToString() = "TYPE" Then
                                        MouseoverV1(i) = "Type"
                                    ElseIf MouseoverV(i).ToString() = "TYPEDESC" Then
                                        MouseoverV1(i) = "Type Description"
                                    ElseIf MouseoverV(i).ToString() = "MATERIAL" Then
                                        MouseoverV1(i) = "Material Description"
                                    ElseIf MouseoverV(i).ToString() = "PROCESS" Then
                                        MouseoverV1(i) = "Process"
                                    ElseIf MouseoverV(i).ToString() = "RESINFAMILY" Then
                                        MouseoverV1(i) = "Resin Family"
                                    ElseIf MouseoverV(i).ToString() = "ALLOY" Then
                                        MouseoverV1(i) = "Alloy"
                                    ElseIf MouseoverV(i).ToString() = "TEMPER" Then
                                        MouseoverV1(i) = "Temper"
                                    ElseIf MouseoverV(i).ToString() = "ALLOY" Then
                                        MouseoverV1(i) = "Alloy"
                                    ElseIf MouseoverV(i).ToString() = "TECHDESC" Then
                                        MouseoverV1(i) = "Technical Description"
                                    ElseIf MouseoverV(i).ToString() = "FUNCTION" Then
                                        MouseoverV1(i) = "Function"
                                    ElseIf MouseoverV(i).ToString() = "RECYCLE" Then
                                        MouseoverV1(i) = "Recycle"
                                    ElseIf MouseoverV(i).ToString() = "MATDES" Then
                                        MouseoverV1(i) = "Material Category"
                                    ElseIf MouseoverV(i).ToString() = "BARRIER" Then
                                        MouseoverV1(i) = "Barrier"
                                    ElseIf MouseoverV(i).ToString() = "DENSITY" Then
                                        MouseoverV1(i) = "Density"
                                    ElseIf MouseoverV(i).ToString() = "FORMULATION" Then
                                        MouseoverV1(i) = "Formulation"
                                    ElseIf MouseoverV(i).ToString() = "REACTIVE" Then
                                        MouseoverV1(i) = "Reactive"
                                    ElseIf MouseoverV(i).ToString() = "SUBLAYERS" Then
                                        MouseoverV1(i) = "Substrate Layers"
                                    ElseIf MouseoverV(i).ToString() = "PRESIDE" Then
                                        MouseoverV1(i) = "Pretreat Sides"
                                    ElseIf MouseoverV(i).ToString() = "PRETYPE" Then
                                        MouseoverV1(i) = "Pretreat Type"
                                    ElseIf MouseoverV(i).ToString() = "MODIFIERS" Then
                                        MouseoverV1(i) = "Modifiers"
                                    ElseIf MouseoverV(i).ToString() = "OUTCOATING" Then
                                        MouseoverV1(i) = "Outside Coating"
                                    ElseIf MouseoverV(i).ToString() = "PRODOUTCOATING" Then
                                        MouseoverV1(i) = "Product Side Coating"
                                    ElseIf MouseoverV(i).ToString() = "OXYGENBARR" Then
                                        MouseoverV1(i) = "Oxygen Barrier"
                                    ElseIf MouseoverV(i).ToString() = "MOISTUREBARR" Then
                                        MouseoverV1(i) = "Moisture Barrier"
                                    ElseIf MouseoverV(i).ToString() = "APPLICATION" Then
                                        MouseoverV1(i) = "Application"
                                    End If
                                    If i = 0 Then

                                        MouseoverValue = "<b>" + MouseoverV1(i) + "</b>=" + ds.Tables(0).Rows(e.Row.RowIndex).Item("" + MouseoverV(i).Trim() + "").ToString() + "</br>"
                                    Else

                                        MouseoverValue = MouseoverValue + "<b>" + MouseoverV1(i) + "</b>=" + ds.Tables(0).Rows(e.Row.RowIndex).Item("" + MouseoverV(i).Trim() + "").ToString() + "</br>"
                                    End If

                                Next
                            End If
                        End If
                        btn.Attributes("onclick") = "javascript:return MaterialDetail('" + ds.Tables(0).Rows(e.Row.RowIndex).Item("MATDES").ToString() + "'," + ds.Tables(0).Rows(e.Row.RowIndex).Item("MATID").ToString() + ",'" + GradeValue + "','" + MouseoverValue + "','" + ds.Tables(0).Rows(e.Row.RowIndex).Item("MATDE2").ToString() + "')"

                        End If
                Else
                        Dim btn As LinkButton = DirectCast(e.Row.FindControl("lnkBtnval"), LinkButton)
                    btn.Attributes("onclick") = "javascript:return MaterialDetail('" + ds.Tables(0).Rows(e.Row.RowIndex).Item("MATDES").ToString() + "'," + ds.Tables(0).Rows(e.Row.RowIndex).Item("MATID").ToString() + ",'','','" + "','" + ds.Tables(0).Rows(e.Row.RowIndex).Item("MATDE2").ToString() + "')"
                End If
            End If
        End If

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
            _lErrorLble.Text = "Error:InnerTdSetting:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub grdMaterials_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdMaterials.Sorting
         Try
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
            grdMaterials.DataSource = dv
            grdMaterials.DataBind()

            'Started Activity Log Changes
            Try
                Dim objUpIns As New StandUpInsData.UpdateInsert
                objUpIns.InsertLog1(Session("UserId").ToString(), "11", "List Sorted, Sorted by: " + e.SortExpression + " ", Session("SBACaseId"), Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
            Catch ex As Exception

            End Try
            'Ended Activity Log Changes
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub GridSize_ORG()
        Try

            If (hidMatGrp.Value).ToUpper() = "RESIN" Then
                grdMaterials.Columns(1).ItemStyle.Width = 100
                grdMaterials.Columns(3).ItemStyle.Width = 90
                grdMaterials.Columns(4).ItemStyle.Width = 100
                grdMaterials.Columns(12).ItemStyle.Width = 80
                grdMaterials.Columns(1).HeaderStyle.Width = 100
                grdMaterials.Columns(3).HeaderStyle.Width = 110
                grdMaterials.Columns(4).HeaderStyle.Width = 90
                grdMaterials.Columns(12).HeaderStyle.Width = 90
                grdMaterials.Width = 1200
                grdMaterials.HeaderStyle.Width = 1195
            ElseIf (hidMatGrp.Value).ToUpper() = "ADHESIVE" Then
                grdMaterials.Columns(1).HeaderStyle.Width = 150
                grdMaterials.Columns(3).HeaderStyle.Width = 160
                grdMaterials.Columns(17).HeaderStyle.Width = 230
                grdMaterials.Columns(27).HeaderStyle.Width = 160
                grdMaterials.Columns(1).ItemStyle.Width = 100
                grdMaterials.Columns(3).ItemStyle.Width = 160
                grdMaterials.Columns(17).ItemStyle.Width = 200
                grdMaterials.Columns(27).ItemStyle.Width = 120


            ElseIf (hidMatGrp.Value).ToUpper() = "ALUMINUM" Then
                grdMaterials.Columns(1).HeaderStyle.Width = 240
                grdMaterials.Columns(3).HeaderStyle.Width = 190
                grdMaterials.Columns(1).ItemStyle.Width = 130
                grdMaterials.Columns(3).ItemStyle.Width = 180

            ElseIf (hidMatGrp.Value).ToUpper() = "BASE FIBER" Then
                grdMaterials.Columns(1).HeaderStyle.Width = 280
                grdMaterials.Columns(3).HeaderStyle.Width = 450
                grdMaterials.Columns(4).HeaderStyle.Width = 400
                grdMaterials.Columns(1).ItemStyle.Width = 220
                grdMaterials.Columns(3).ItemStyle.Width = 430
                grdMaterials.Columns(4).ItemStyle.Width = 290


            ElseIf (hidMatGrp.Value).ToUpper().Trim() = "COATING" Then
                grdMaterials.Columns(1).HeaderStyle.Width = 130
                grdMaterials.Columns(3).HeaderStyle.Width = 120
                grdMaterials.Columns(4).HeaderStyle.Width = 160
                grdMaterials.Columns(17).HeaderStyle.Width = 170
                grdMaterials.Columns(16).HeaderStyle.Width = 120
                grdMaterials.Columns(12).HeaderStyle.Width = 140
                grdMaterials.Columns(1).ItemStyle.Width = 100
                grdMaterials.Columns(3).ItemStyle.Width = 120
                grdMaterials.Columns(4).ItemStyle.Width = 150
                grdMaterials.Columns(17).ItemStyle.Width = 140
                grdMaterials.Columns(16).ItemStyle.Width = 100
                grdMaterials.Columns(12).ItemStyle.Width = 130


            ElseIf (hidMatGrp.Value).ToUpper().Trim() = "CONCENTRATE" Then
                grdMaterials.Columns(1).HeaderStyle.Width = 370
                grdMaterials.Columns(3).HeaderStyle.Width = 370
                grdMaterials.Columns(1).ItemStyle.Width = 250
                grdMaterials.Columns(3).ItemStyle.Width = 300

            ElseIf (hidMatGrp.Value).ToUpper().Trim() = "FILM" Then
                grdMaterials.Columns(1).HeaderStyle.Width = 105
                grdMaterials.Columns(3).HeaderStyle.Width = 200
                grdMaterials.Columns(1).ItemStyle.Width = 100
                grdMaterials.Columns(3).ItemStyle.Width = 200
                grdMaterials.Columns(4).HeaderStyle.Width = 100
                grdMaterials.Columns(4).ItemStyle.Width = 80
                grdMaterials.Width = 1200
                grdMaterials.HeaderStyle.Width = 1195
            ElseIf (hidMatGrp.Value).ToUpper().Trim() = "GLASS" Then
                grdMaterials.Columns(1).HeaderStyle.Width = 280
                grdMaterials.Columns(3).HeaderStyle.Width = 300
                grdMaterials.Columns(1).ItemStyle.Width = 250
                grdMaterials.Columns(3).ItemStyle.Width = 280
                grdMaterials.Columns(4).HeaderStyle.Width = 280
                grdMaterials.Columns(4).ItemStyle.Width = 260

            ElseIf (hidMatGrp.Value).ToUpper().Trim() = "INK" Then
                grdMaterials.Columns(1).HeaderStyle.Width = 200
                grdMaterials.Columns(3).HeaderStyle.Width = 200
                grdMaterials.Columns(16).HeaderStyle.Width = 320
                grdMaterials.Columns(27).HeaderStyle.Width = 300
                grdMaterials.Columns(1).ItemStyle.Width = 200
                grdMaterials.Columns(3).ItemStyle.Width = 150
                grdMaterials.Columns(16).ItemStyle.Width = 300
                grdMaterials.Columns(27).ItemStyle.Width = 280


            ElseIf (hidMatGrp.Value).ToUpper().Trim() = "NON-WOVEN" Then
                grdMaterials.Columns(1).HeaderStyle.Width = 340
                grdMaterials.Columns(3).HeaderStyle.Width = 300
                grdMaterials.Columns(1).ItemStyle.Width = 300
                grdMaterials.Columns(3).ItemStyle.Width = 300
                grdMaterials.Columns(4).HeaderStyle.Width = 280
                grdMaterials.Columns(4).ItemStyle.Width = 250

            ElseIf (hidMatGrp.Value).ToUpper().Trim() = "PAPER" Then
                grdMaterials.Columns(1).HeaderStyle.Width = 410
                grdMaterials.Columns(3).HeaderStyle.Width = 450
                grdMaterials.Columns(1).ItemStyle.Width = 350
                grdMaterials.Columns(3).ItemStyle.Width = 350
                grdMaterials.Columns(4).HeaderStyle.Width = 500
                grdMaterials.Columns(4).ItemStyle.Width = 400

            ElseIf (hidMatGrp.Value).ToUpper().Trim() = "PAPERBOARD" Then
                grdMaterials.Columns(1).HeaderStyle.Width = 330
                grdMaterials.Columns(3).HeaderStyle.Width = 280
                grdMaterials.Columns(1).ItemStyle.Width = 300
                grdMaterials.Columns(3).ItemStyle.Width = 250


            ElseIf (hidMatGrp.Value).ToUpper().Trim() = "SHEET" Then
                grdMaterials.Columns(1).HeaderStyle.Width = 470
                grdMaterials.Columns(3).HeaderStyle.Width = 350
                grdMaterials.Columns(1).ItemStyle.Width = 400
                grdMaterials.Columns(3).ItemStyle.Width = 330
                grdMaterials.Columns(4).HeaderStyle.Width = 450
                grdMaterials.Columns(4).ItemStyle.Width = 400

            ElseIf (hidMatGrp.Value).ToUpper().Trim() = "STEEL" Then
                grdMaterials.Columns(1).HeaderStyle.Width = 470
                grdMaterials.Columns(3).HeaderStyle.Width = 350
                grdMaterials.Columns(1).ItemStyle.Width = 400
                grdMaterials.Columns(3).ItemStyle.Width = 330
                grdMaterials.Columns(4).HeaderStyle.Width = 450
                grdMaterials.Columns(4).ItemStyle.Width = 400
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:InnerTdSetting:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GridSize()
        Try

            If (hidMatGrp.Value).ToUpper() = "RESIN" Then
                grdMaterials.Columns(1).ItemStyle.Width = 150
                grdMaterials.Columns(3).ItemStyle.Width = 150
                grdMaterials.Columns(4).ItemStyle.Width = 150
                grdMaterials.Columns(5).ItemStyle.Width = 150
                grdMaterials.Columns(6).ItemStyle.Width = 150
                grdMaterials.Columns(7).ItemStyle.Width = 140
                grdMaterials.Columns(8).ItemStyle.Width = 100
                grdMaterials.Columns(9).ItemStyle.Width = 100
                'grdMaterials.Columns(10).ItemStyle.Width = 105
                grdMaterials.Columns(11).ItemStyle.Width = 100
                'grdMaterials.Columns(12).ItemStyle.Width = 70
                'grdMaterials.Columns(13).ItemStyle.Width = 125
                'grdMaterials.Columns(14).ItemStyle.Width = 110
                'grdMaterials.Columns(15).ItemStyle.Width = 80

                grdMaterials.Columns(1).HeaderStyle.Width = 150
                grdMaterials.Columns(3).HeaderStyle.Width = 150
                grdMaterials.Columns(4).HeaderStyle.Width = 150
                grdMaterials.Columns(5).HeaderStyle.Width = 150
                grdMaterials.Columns(6).HeaderStyle.Width = 150
                grdMaterials.Columns(7).HeaderStyle.Width = 140
                grdMaterials.Columns(8).HeaderStyle.Width = 100
                grdMaterials.Columns(9).HeaderStyle.Width = 100
                'grdMaterials.Columns(10).HeaderStyle.Width = 85
                grdMaterials.Columns(11).HeaderStyle.Width = 100
                'grdMaterials.Columns(12).HeaderStyle.Width = 80
                'grdMaterials.Columns(13).HeaderStyle.Width = 125
                'grdMaterials.Columns(14).HeaderStyle.Width = 80
                'grdMaterials.Columns(15).HeaderStyle.Width = 85
                grdMaterials.Width = 1190
                grdMaterials.HeaderStyle.Width = 1190
            ElseIf (hidMatGrp.Value).ToUpper() = "ADHESIVE" Then
                grdMaterials.Columns(1).HeaderStyle.Width = 300
                grdMaterials.Columns(3).HeaderStyle.Width = 300
                grdMaterials.Columns(27).HeaderStyle.Width = 200
                grdMaterials.Columns(16).HeaderStyle.Width = 200
                grdMaterials.Columns(40).HeaderStyle.Width = 200
                'grdMaterials.Columns(41).HeaderStyle.Width = 200

                grdMaterials.Columns(1).ItemStyle.Width = 300
                grdMaterials.Columns(3).ItemStyle.Width = 300
                grdMaterials.Columns(16).ItemStyle.Width = 200
                grdMaterials.Columns(27).ItemStyle.Width = 200
                grdMaterials.Columns(40).ItemStyle.Width = 200
                'grdMaterials.Columns(41).ItemStyle.Width = 200
                grdMaterials.Width = 1200
                grdMaterials.HeaderStyle.Width = 1200

            ElseIf (hidMatGrp.Value).ToUpper() = "ALUMINUM" Then
                grdMaterials.Columns(1).HeaderStyle.Width = 400
                grdMaterials.Columns(3).HeaderStyle.Width = 400
                grdMaterials.Columns(19).HeaderStyle.Width = 400
                'grdMaterials.Columns(20).HeaderStyle.Width = 240
                'grdMaterials.Columns(21).HeaderStyle.Width = 240
                grdMaterials.Columns(1).ItemStyle.Width = 400
                grdMaterials.Columns(3).ItemStyle.Width = 400
                grdMaterials.Columns(19).ItemStyle.Width = 400
                'grdMaterials.Columns(20).ItemStyle.Width = 240
                'grdMaterials.Columns(21).ItemStyle.Width = 240
                grdMaterials.Width = 1200
                grdMaterials.HeaderStyle.Width = 1200

            ElseIf (hidMatGrp.Value).ToUpper() = "BASE FIBER" Then
                grdMaterials.Columns(1).HeaderStyle.Width = 400
                grdMaterials.Columns(3).HeaderStyle.Width = 400
                grdMaterials.Columns(4).HeaderStyle.Width = 400
                grdMaterials.Columns(1).ItemStyle.Width = 400
                grdMaterials.Columns(3).ItemStyle.Width = 400
                grdMaterials.Columns(4).ItemStyle.Width = 400

                grdMaterials.Width = 1200
                grdMaterials.HeaderStyle.Width = 1200
            ElseIf (hidMatGrp.Value).ToUpper().Trim() = "COATING" Then
                grdMaterials.Columns(1).HeaderStyle.Width = 200
                grdMaterials.Columns(3).HeaderStyle.Width = 200
                grdMaterials.Columns(4).HeaderStyle.Width = 160
                grdMaterials.Columns(17).HeaderStyle.Width = 160
                grdMaterials.Columns(16).HeaderStyle.Width = 160
                grdMaterials.Columns(12).HeaderStyle.Width = 160
                grdMaterials.Columns(22).HeaderStyle.Width = 160
                grdMaterials.Columns(1).ItemStyle.Width = 200
                grdMaterials.Columns(3).ItemStyle.Width = 200
                grdMaterials.Columns(4).ItemStyle.Width = 160
                grdMaterials.Columns(17).ItemStyle.Width = 160
                grdMaterials.Columns(16).ItemStyle.Width = 160
                grdMaterials.Columns(12).ItemStyle.Width = 160
                grdMaterials.Columns(22).ItemStyle.Width = 160
                grdMaterials.Width = 1200
                grdMaterials.HeaderStyle.Width = 1200

            ElseIf (hidMatGrp.Value).ToUpper().Trim() = "CONCENTRATE" Then
                grdMaterials.Columns(1).HeaderStyle.Width = 400
                grdMaterials.Columns(3).HeaderStyle.Width = 400
                grdMaterials.Columns(23).HeaderStyle.Width = 400
                grdMaterials.Columns(1).ItemStyle.Width = 400
                grdMaterials.Columns(3).ItemStyle.Width = 400
                grdMaterials.Columns(23).ItemStyle.Width = 400
                grdMaterials.Width = 1200
                grdMaterials.HeaderStyle.Width = 1200

            ElseIf (hidMatGrp.Value).ToUpper().Trim() = "FILM" Then
                grdMaterials.Columns(1).HeaderStyle.Width = 120
                grdMaterials.Columns(3).HeaderStyle.Width = 120
                grdMaterials.Columns(4).HeaderStyle.Width = 60
                grdMaterials.Columns(31).HeaderStyle.Width = 100
                grdMaterials.Columns(32).HeaderStyle.Width = 100
                grdMaterials.Columns(33).HeaderStyle.Width = 100
                grdMaterials.Columns(34).HeaderStyle.Width = 100
                grdMaterials.Columns(35).HeaderStyle.Width = 100
                grdMaterials.Columns(36).HeaderStyle.Width = 100
                grdMaterials.Columns(37).HeaderStyle.Width = 100
                grdMaterials.Columns(38).HeaderStyle.Width = 100
                grdMaterials.Columns(39).HeaderStyle.Width = 100
                grdMaterials.Columns(1).ItemStyle.Width = 120
                grdMaterials.Columns(3).ItemStyle.Width = 120
                grdMaterials.Columns(4).ItemStyle.Width = 60
                grdMaterials.Columns(31).ItemStyle.Width = 100
                grdMaterials.Columns(32).ItemStyle.Width = 100
                grdMaterials.Columns(33).ItemStyle.Width = 100
                grdMaterials.Columns(34).ItemStyle.Width = 100
                grdMaterials.Columns(35).ItemStyle.Width = 100
                grdMaterials.Columns(36).ItemStyle.Width = 100
                grdMaterials.Columns(37).ItemStyle.Width = 100
                grdMaterials.Columns(38).ItemStyle.Width = 100
                grdMaterials.Columns(39).ItemStyle.Width = 100


                grdMaterials.Width = 1200
                grdMaterials.HeaderStyle.Width = 1200
            ElseIf (hidMatGrp.Value).ToUpper().Trim() = "GLASS" Then
                grdMaterials.Columns(1).HeaderStyle.Width = 300
                grdMaterials.Columns(3).HeaderStyle.Width = 300
                grdMaterials.Columns(4).HeaderStyle.Width = 300
                grdMaterials.Columns(25).HeaderStyle.Width = 300
                grdMaterials.Columns(1).ItemStyle.Width = 300
                grdMaterials.Columns(3).ItemStyle.Width = 300
                grdMaterials.Columns(4).ItemStyle.Width = 300
                grdMaterials.Columns(25).ItemStyle.Width = 300
                grdMaterials.Width = 1200
                grdMaterials.HeaderStyle.Width = 1200

            ElseIf (hidMatGrp.Value).ToUpper().Trim() = "INK" Then
                grdMaterials.Columns(1).HeaderStyle.Width = 240
                grdMaterials.Columns(3).HeaderStyle.Width = 240
                'grdMaterials.Columns(16).HeaderStyle.Width = 200
                grdMaterials.Columns(17).HeaderStyle.Width = 240
                grdMaterials.Columns(26).HeaderStyle.Width = 240
                grdMaterials.Columns(27).HeaderStyle.Width = 240
                grdMaterials.Columns(1).ItemStyle.Width = 240
                grdMaterials.Columns(3).ItemStyle.Width = 240
                'grdMaterials.Columns(16).ItemStyle.Width = 200
                grdMaterials.Columns(17).ItemStyle.Width = 240
                grdMaterials.Columns(26).ItemStyle.Width = 240
                grdMaterials.Columns(27).ItemStyle.Width = 240
                grdMaterials.Width = 1200
                grdMaterials.HeaderStyle.Width = 1200

            ElseIf (hidMatGrp.Value).ToUpper().Trim() = "NON-WOVEN" Then
                grdMaterials.Columns(1).HeaderStyle.Width = 300
                grdMaterials.Columns(3).HeaderStyle.Width = 300
                grdMaterials.Columns(1).ItemStyle.Width = 300
                grdMaterials.Columns(3).ItemStyle.Width = 300
                grdMaterials.Columns(4).HeaderStyle.Width = 300
                grdMaterials.Columns(4).ItemStyle.Width = 300
                grdMaterials.Columns(28).HeaderStyle.Width = 300
                grdMaterials.Columns(28).ItemStyle.Width = 300
                grdMaterials.Width = 1200
                grdMaterials.HeaderStyle.Width = 1200
            ElseIf (hidMatGrp.Value).ToUpper().Trim() = "PAPER" Then
                grdMaterials.Columns(1).HeaderStyle.Width = 300
                grdMaterials.Columns(3).HeaderStyle.Width = 310
                grdMaterials.Columns(28).HeaderStyle.Width = 270
                grdMaterials.Columns(29).HeaderStyle.Width = 280

                grdMaterials.Columns(1).ItemStyle.Width = 300
                grdMaterials.Columns(3).ItemStyle.Width = 300
               
                grdMaterials.Columns(28).ItemStyle.Width = 270
                grdMaterials.Columns(29).ItemStyle.Width = 270
                grdMaterials.Width = 1160
                grdMaterials.HeaderStyle.Width = 1160
            ElseIf (hidMatGrp.Value).ToUpper().Trim() = "PAPERBOARD" Then
                grdMaterials.Columns(1).HeaderStyle.Width = 300
                grdMaterials.Columns(3).HeaderStyle.Width = 310
                grdMaterials.Columns(28).HeaderStyle.Width = 270
                grdMaterials.Columns(29).HeaderStyle.Width = 280

                grdMaterials.Columns(1).ItemStyle.Width = 300
                grdMaterials.Columns(3).ItemStyle.Width = 300

                grdMaterials.Columns(28).ItemStyle.Width = 270
                grdMaterials.Columns(29).ItemStyle.Width = 270
                grdMaterials.Width = 1160
                grdMaterials.HeaderStyle.Width = 1160
            ElseIf (hidMatGrp.Value).ToUpper().Trim() = "SHEET" Then
                grdMaterials.Columns(1).HeaderStyle.Width = 400
                grdMaterials.Columns(3).HeaderStyle.Width = 400
                grdMaterials.Columns(1).ItemStyle.Width = 400
                grdMaterials.Columns(3).ItemStyle.Width = 400
                grdMaterials.Columns(4).HeaderStyle.Width = 400
                grdMaterials.Columns(4).ItemStyle.Width = 400
                grdMaterials.Width = 1200
                grdMaterials.HeaderStyle.Width = 1200
            ElseIf (hidMatGrp.Value).ToUpper().Trim() = "STEEL" Then
                grdMaterials.Columns(1).HeaderStyle.Width = 400
                grdMaterials.Columns(3).HeaderStyle.Width = 400
                grdMaterials.Columns(1).ItemStyle.Width = 400
                grdMaterials.Columns(3).ItemStyle.Width = 400
                grdMaterials.Columns(4).HeaderStyle.Width = 400
                grdMaterials.Columns(4).ItemStyle.Width = 400
                grdMaterials.Width = 1200
                grdMaterials.HeaderStyle.Width = 1200
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:InnerTdSetting:" + ex.Message.ToString()
        End Try
    End Sub
	
    Protected Sub btnPostback_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim objUpIns As New StandUpInsData.UpdateInsert()
            'Started Activity Log Changes
            Try
                objUpIns.InsertLog1(Session("UserId").ToString(), "11", "Selected Material #" + hidMatVal.Value, Session("SBACaseId"), Session("LogInCount").ToString(), Session.SessionID, hidMatVal.Value, "", "", "", "")
            Catch ex As Exception

            End Try
            'Ended Activity Log Changes
        Catch ex As Exception

        End Try
    End Sub
Protected Sub btnCall_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCall.Click
        Try
            hidSession.Value = "1"
            'GetMaterialGroupDetails()
            If hidMatGrp.Value <> "Nothing" Then
                Link_Click()
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
