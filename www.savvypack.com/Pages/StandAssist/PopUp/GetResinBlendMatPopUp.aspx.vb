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
Partial Class Pages_StandAssist_PopUp_GetResinBlendMatPopUp
    Inherits System.Web.UI.Page
   

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            hidMatdes.Value = Request.QueryString("Des").ToString()
            hidMatid.Value = Request.QueryString("ID").ToString()
            hidGradedes.Value = Request.QueryString("GradeDes").ToString()
            hidGradeid.Value = Request.QueryString("GradeId").ToString()
            hidSG.Value = Request.QueryString("SG").ToString()
            hidMatD.Value = Request.QueryString("MatDes").ToString()
            hidLinkId.Value = Request.QueryString("LinkId").ToString()

            If Request.QueryString("Mcnt").ToString() <> "" Then
                hidMcnt.Value = Request.QueryString("Mcnt").ToString()
            End If
            If Not IsPostBack Then
                hidMatGrp.Value = Request.QueryString("Grp").ToString().Trim().ToUpper()
                If Request.QueryString("Flag") <> "Y" Then
                Else
                End If
                hvUserGrd.Value = "0"
                hvGroupQ.Value = Request.QueryString("Grp").ToString().Trim().ToUpper()
                hidMatCat.Value = Request.QueryString("Grp").ToString().Trim().ToUpper()
            End If
            If hidMatGrp.Value <> "NOTHING" Then
                Link_Click()
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            If hidMatGrp.Value <> "N" Then
                Link_Click()
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
                    ds = objGetData.GetResinMaterialSubLayer(-1, txtMatDe1.Text.ToString(), hidMatGrp.Value.Trim(), "BLEND")
                End If
                Session("UsersDataGroup") = ds
                'If ds.Tables(0).Rows.Count > 0 Then
                grdMaterials.DataSource = ds
                grdMaterials.DataBind()
                'End If

            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:Link_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub grdMaterials_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles grdMaterials.RowDataBound
        Dim ds As New DataSet()
        ds = CType(Session("UsersDataGroup"), DataSet)

        If (e.Row.RowType = DataControlRowType.DataRow) Then

            If (e.Row.RowIndex = 0) Then
                e.Row.Style.Add("height", "70px")
                e.Row.Style.Add("vertical-align", "bottom")
            End If
        End If
        GridSize()
        If ds.Tables(0).Rows().Count > 0 Then
            If e.Row.RowType = DataControlRowType.DataRow Then
                If ds.Tables(0).Rows(e.Row.RowIndex).Item("MATID").ToString() <> "0" Then
                    Dim btn As LinkButton = DirectCast(e.Row.FindControl("lnkBtnval"), LinkButton)

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
                                    GradeValue = GradeValue + "," + ds.Tables(0).Rows(e.Row.RowIndex).Item("" + GradeV(i).Trim() + "").ToString()
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


                    btn.Attributes("onclick") = "javascript:return BlendDetail('" + ds.Tables(0).Rows(e.Row.RowIndex).Item("MATDES").ToString() + "'," + ds.Tables(0).Rows(e.Row.RowIndex).Item("MATID").ToString() + ",'" + GradeValue + "','" + MouseoverValue + "','" + ds.Tables(0).Rows(e.Row.RowIndex).Item("MATERIAL").ToString() + "','" + hidMcnt.Value.ToString() + "')"

                Else
                    If ds.Tables(0).Rows(e.Row.RowIndex).Item("MATID").ToString() = "0" Then
                        Dim btn As LinkButton = DirectCast(e.Row.FindControl("lnkBtnval"), LinkButton)
                        btn.Attributes("onclick") = "javascript:return BlendDetail('" + ds.Tables(0).Rows(e.Row.RowIndex).Item("MATDES").ToString() + "'," + ds.Tables(0).Rows(e.Row.RowIndex).Item("MATID").ToString() + ",'','','" + ds.Tables(0).Rows(e.Row.RowIndex).Item("MATERIAL").ToString() + "','" + hidMcnt.Value.ToString() + "')"
                    Else
                        Dim btn As LinkButton = DirectCast(e.Row.FindControl("lnkBtnval"), LinkButton)
                        btn.Attributes("onclick") = "javascript:return BlendDetail('" + ds.Tables(0).Rows(e.Row.RowIndex).Item("MATDES").ToString() + "'," + ds.Tables(0).Rows(e.Row.RowIndex).Item("MATID").ToString() + ",'','','" + ds.Tables(0).Rows(e.Row.RowIndex).Item("MATERIAL").ToString() + "','" + hidMcnt.Value.ToString() + "')"

                    End If
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

            'Link_Click()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GridSize()
        Try

            grdMaterials.Columns(1).ItemStyle.Width = 150
            grdMaterials.Columns(1).HeaderStyle.Width = 150

            grdMaterials.Width = 1150
            grdMaterials.HeaderStyle.Width = 1150
           
        Catch ex As Exception
            _lErrorLble.Text = "Error:GridSize:" + ex.Message.ToString()
        End Try
    End Sub
   
End Class
