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
Partial Class Pages_Econ1_PopUp_GetEquipmentPopUpList
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim objUpIns As New StandUpInsData.UpdateInsert
            If Session("Back") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If

            hidEqdes.Value = Request.QueryString("Des").ToString()
            hidEqid.Value = Request.QueryString("EId").ToString()
            hidEid.Value = Request.QueryString("ID").ToString()
            hidMod.Value = Session("EquipMod")
            If Not IsPostBack Then
                hidMatGrp.Value = Request.QueryString("Grp").ToString().Trim().ToUpper()
                hvGroupQ.Value = Request.QueryString("Grp").ToString().Trim().ToUpper()
                If hidMatGrp.Value <> "NOTHING SELECTED" Then
                    Link_Click()
                End If
                hidGroup.Value = Request.QueryString("ParentGrp").ToString().Trim().ToUpper()
                If hidGroup.Value = "0" Then
                    lnkGroup.Text = "Show All"
                Else
                    lnkGroup.Text = "Product Format Group"
                End If
                GetEquipmentGroupDetails()
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
            GetEquipmentGroupDetails()
        Catch ex As Exception
            _lErrorLble.Text = "Error:btnSearch_Click:" + ex.Message.ToString()
        End Try
    End Sub


    Public Sub Link_Click()
        Dim objGetData As New E1GetData.Selectdata()
        Dim groupId As Integer
        Dim dsCol As New DataSet
        Dim dsEquip As New DataSet
        Dim dt As New DataTable
        Dim dsEqid As New DataSet

        Try
            If hidMatGrp.Value <> "N" And hidMatGrp.Value.ToLower() <> "nothing selected" Then
                groupId = objGetData.GetEquipmentGroupId(hidMatGrp.Value.ToLower())
                dsCol = objGetData.GetEquipmentColumns(groupId)
                dsEqid = objGetData.GetEquipmentId(groupId, txtMatDe1.Text.ToString())

                If dsEqid.Tables(0).Rows.Count > 0 Then
                    Dim EqId As String = ""
                    For i = 0 To dsEqid.Tables(0).Rows.Count - 1
                        EqId = EqId + dsEqid.Tables(0).Rows(i).Item("EQUIPMENTID").ToString() + ","
                    Next
                    EqId = EqId.Remove(EqId.Length - 1)

                    dsEquip = objGetData.GetEquipmentList(groupId, txtMatDe1.Text.ToString(), EqId, Session("E1CaseId"))

                    Session("EquipIdData") = dsEqid
                    Session("UsersDataGroup") = dsEquip
                    Session("ColData") = dsCol

                    grdMaterials.Columns.Clear()

                    grid.Visible = True
                    gridMsg.Visible = False
                    Dim bField As New BoundField()
                    bField.HeaderText = "Equipment"
                    grdMaterials.Columns.Add(bField)
                    Dim bField1 As New BoundField()
                    bField1.HeaderText = "User Label"
                    bField1.ItemStyle.Width = 150
                    grdMaterials.Columns.Add(bField1)

                    If dsCol.Tables(0).Rows.Count > 2 Then
                        For i = 2 To dsCol.Tables(0).Rows.Count - 1
                            Dim boundField As New BoundField()
                            boundField.HeaderText = dsCol.Tables(0).Rows(i).Item("EQUIPCOLNAME").ToString()
                            boundField.ItemStyle.Width = 150
                            grdMaterials.Columns.Add(boundField)
                        Next
                    End If

                    grdMaterials.DataSource = dsEqid
                    grdMaterials.DataBind()
                    BindGrid()
                Else
                    grid.Visible = False
                    gridMsg.Visible = True
                End If

            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:Link_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetEquipmentGroupDetails()
        Dim ds As New DataSet
        Dim ds1 As New DataSet
        Dim objGetData As New E1GetData.Selectdata()
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
            If hidGroup.Value = "0" Then
                ds1 = objGetData.GetEquipmentGroups(Session("E1CaseId"))
                If ds1.Tables(0).Rows.Count > 0 Then
                    ds = objGetData.GetEquipmentGroups(Session("E1CaseId"))
                Else
                    ds = objGetData.GetEquipmentGroups()
                End If
            Else
                ds = objGetData.GetEquipmentGroups()
            End If
            tblMaterials.Rows.Clear()
            For i = 0 To ds.Tables(0).Rows.Count - 1
                trInner = New TableRow
                tdInner = New TableCell
                InnerTdSetting(tdInner, "", "Left")
                Link = New HyperLink
                hid = New HiddenField
                hidDes = New HiddenField
                Link.ID = "lnk" + i.ToString()
                Link.Width = 120
                Link.CssClass = "LinkMat"
                Link.Text = ds.Tables(0).Rows(i).Item("EQUIPGROUPNAME").ToString()
                Link.ToolTip = ds.Tables(0).Rows(i).Item("EQUIPGROUPNAME").ToString()

                If hvGroupQ.Value.ToString() = Link.Text.ToUpper() Then
                    Link.ForeColor = Drawing.Color.Red
                    hidMatGrp.Value = hvGroupQ.Value
                End If

                tdInner.Controls.Add(Link)
                trInner.Controls.Add(tdInner)
                Link.NavigateUrl = "GetEquipmentPopUpList.aspx?Des=" + hidEqdes.Value + "&Id=" + hidEid.Value + "&Grp=" + Link.Text + "&EId=" + hidEqid.Value + "&ParentGrp=" + hidGroup.Value

                trInner.Height = 10
                tblMaterials.Controls.Add(trInner)
            Next
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetEquipmentGroupDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Private Sub BindGrid()
        Try
            Dim ds As New DataSet()
            Dim dv As New DataView
            Dim dt As New DataTable
            Dim dsEqid As New DataSet
            Dim dvEqid As New DataView
            Dim dtEqid As New DataTable
            Dim dsCol As New DataSet
            Dim count As Integer
            Dim RowIndexCount As Integer

            ds = CType(Session("UsersDataGroup"), DataSet)
            dsEqid = CType(Session("EquipIdData"), DataSet)
            dsCol = CType(Session("ColData"), DataSet)

            dvEqid = dsEqid.Tables(0).DefaultView()
            dtEqid = dvEqid.ToTable()
            count = dsEqid.Tables(0).Rows.Count
            dv = ds.Tables(0).DefaultView()

            For Each Gr As GridViewRow In grdMaterials.Rows
                dv.RowFilter = "EQUIPID='" + dsEqid.Tables(0).Rows(Gr.RowIndex).Item("EQUIPMENTID").ToString() + "'"
                dt = dv.ToTable()

                Dim lnkbtn As New LinkButton
                lnkbtn.CssClass = "LinkMat"

 If dt.Rows.Count = 0 Then
                    If dsEqid.Tables(0).Rows(Gr.RowIndex).Item("EQUIPMENTID").ToString() = 0 Then
                        lnkbtn.Text = "0" + ":" + "Nothing Selected "
                        lnkbtn.Attributes("onclick") = "javascript:return EquipmentDet('" + "Nothing Selected" + "' ," + "0" + ",'')"
                        Gr.Cells(0).Controls.Add(lnkbtn)
                    End If
                Else
                If dt.Rows(0).Item("EQUIPID").ToString() = hidEqid.Value Then
                    lnkbtn.ForeColor = Drawing.Color.Red
                End If
                lnkbtn.Text = dt.Rows(0).Item("EQUIPID").ToString() + ":" + dt.Rows(0).Item("DETAILS").ToString() + " " + dt.Rows(1).Item("DETAILS").ToString()
                If dt.Rows(0).Item("EQUIPDES").ToString() <> "" Then
                    lnkbtn.Attributes("onclick") = "javascript:return EquipmentDet('" + dt.Rows(0).Item("EQUIPDES").ToString() + "'," + dt.Rows(0).Item("EQUIPID").ToString() + ",'')"
                Else
                    lnkbtn.Attributes("onclick") = "javascript:return EquipmentDet('" + dt.Rows(0).Item("DETAILS").ToString() + " " + dt.Rows(1).Item("DETAILS").ToString() + "'," + dt.Rows(0).Item("EQUIPID").ToString() + ",'')"
                End If
                Gr.Cells(0).Controls.Add(lnkbtn)
                Gr.Cells(1).Text = dt.Rows(0).Item("EQUIPDES").ToString()

                If dsCol.Tables(0).Rows.Count > 2 Then
                    For i = 2 To dsCol.Tables(0).Rows.Count - 1
                        dv.RowFilter = "EQUIPID='" + dsEqid.Tables(0).Rows(Gr.RowIndex).Item("EQUIPMENTID").ToString() + "' AND EQUIPCOLNAME='" + dsCol.Tables(0).Rows(i).Item("EQUIPCOLNAME").ToString() + "'"
                        dt = dv.ToTable()

                        Gr.Cells(i).Text = dt.Rows(0).Item("DETAILS").ToString()
                    Next
                End If
End If
            Next
        Catch ex As Exception
            _lErrorLble.Text = "Error:BindGrid:" + ex.Message.ToString()
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
            _lErrorLble.Text = "Error:InnerTdSetting:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub lnkGroup_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkGroup.Click
        Try

            If hidGroup.Value = "0" Then
                hidGroup.Value = "1"
                lnkGroup.Text = "Product Format Group"
            Else
                hidGroup.Value = "0"
                lnkGroup.Text = "Show All"
            End If
            GetEquipmentGroupDetails()
            grdMaterials.DataSource = Nothing
            grdMaterials.DataBind()
        Catch ex As Exception
            _lErrorLble.Text = "Error:btnSearch_Click:" + ex.Message.ToString()
        End Try
    End Sub

End Class
