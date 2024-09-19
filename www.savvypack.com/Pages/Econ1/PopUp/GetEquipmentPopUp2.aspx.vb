Imports System.Data
Imports System.Data.OleDb
Imports System
Imports E1GetData
Imports E1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter

Partial Class Pages_Econ1_PopUp_GetEquipmentPopUp2
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
        
            If Not IsPostBack Then
                GetEquipmentDetails()
                GetMaterialGroupDetails()
                'linkClick()
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            GetEquipmentDetails()
        Catch ex As Exception
            _lErrorLble.Text = "Error:btnSearch_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetEquipmentDetails()
        Dim ds As New DataSet
        Dim objGetData As New E1GetData.Selectdata()
        Try
            ds = objGetData.GetEquipment(-1, txtMatDe1.Text.Trim.ToString(), txtMatDe2.Text.Trim.ToString())
            grdEquipment.DataSource = ds
            grdEquipment.DataBind()
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetMaterialDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetMaterialGroupDetails()
        Dim ds As New DataSet
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
            ds = objGetData.GetEqGroups1()
            For i = 0 To ds.Tables(0).Rows.Count - 1
                trInner = New TableRow
                For j = 1 To 12
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
                                Link.Text = ds.Tables(0).Rows(i).Item("GRPNAME").ToString()
                                Link.ToolTip = ds.Tables(0).Rows(i).Item("GRPNAME").ToString()



                                tdInner.Controls.Add(Link)
                                trInner.Controls.Add(tdInner)
                                Link.NavigateUrl = "GetEquipmentPopUp2.aspx?Des=" + hidEqdes.Value + "&Id=" + hidEqid.Value + " &Grp=" + Link.Text + "&LinkId=" + Link.ID + "&Flag=Y"
                                i = i + 1

                            Case 2
                                InnerTdSetting(tdInner, "", "Left")
                                Link = New HyperLink
                                hid = New HiddenField
                                hidDes = New HiddenField
                                Link.ID = "lnk" + i.ToString()
                                Link.Width = 120
                                Link.CssClass = "LinkMat"
                                Link.Text = ds.Tables(0).Rows(i).Item("GRPNAME").ToString()
                                Link.ToolTip = ds.Tables(0).Rows(i).Item("GRPNAME").ToString()



                                tdInner.Controls.Add(Link)
                                trInner.Controls.Add(tdInner)
                                Link.NavigateUrl = "GetEquipmentPopUp2.aspx?Des=" + hidEqdes.Value + "&Id=" + hidEqid.Value + " &Grp=" + Link.Text + "&LinkId=" + Link.ID + "&Flag=Y"
                                i = i + 1
                            Case 3
                                InnerTdSetting(tdInner, "", "Left")
                                Link = New HyperLink
                                hid = New HiddenField
                                hidDes = New HiddenField
                                Link.ID = "lnk" + i.ToString()
                                Link.Width = 120
                                Link.CssClass = "LinkMat"
                                Link.Text = ds.Tables(0).Rows(i).Item("GRPNAME").ToString()
                                Link.ToolTip = ds.Tables(0).Rows(i).Item("GRPNAME").ToString()

                                tdInner.Controls.Add(Link)
                                trInner.Controls.Add(tdInner)

                                Link.NavigateUrl = "GetEquipmentPopUp2.aspx?Des=" + hidEqdes.Value + "&Id=" + hidEqid.Value + " &Grp=" + Link.Text + "&LinkId=" + Link.ID + "&Flag=Y"
                                i = i + 1
                         
                            Case 4
                                InnerTdSetting(tdInner, "", "Left")
                                Link = New HyperLink
                                hid = New HiddenField
                                hidDes = New HiddenField
                                Link.ID = "lnk" + i.ToString()
                                Link.Width = 90
                                Link.CssClass = "LinkMat"
                                Link.Text = ds.Tables(0).Rows(i).Item("GRPNAME").ToString()
                                Link.ToolTip = ds.Tables(0).Rows(i).Item("GRPNAME").ToString()

                         
                                tdInner.Controls.Add(Link)
                                trInner.Controls.Add(tdInner)

                                Link.NavigateUrl = "GetEquipmentPopUp2.aspx?Des=" + hidEqdes.Value + "&Id=" + hidEqid.Value + " &Grp=" + Link.Text + "&LinkId=" + Link.ID + "&Flag=Y"
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


    'Private Sub linkClick()
    '    Dim ds As New DataSet
    '    Dim objGetData As New E1GetData.Selectdata()
    '    Dim val As String
    '    Try
    '        val = hidEqGrp.Value.ToString()
    '        If hidEqGrp.Value <> "N" And hidEqGrp.Value <> "Nothing " Then
    '            ds = objGetData.GetEqGroups2()
    '        Else
    '            ds = objGetData.GetEquip(-1, txtMatDe1.Text.Trim.ToString(), txtMatDe2.Text.Trim.ToString())
    '        End If
    '        grdEquipment.DataSource = ds
    '        grdEquipment.DataBind()
    '    Catch ex As Exception
    '        _lErrorLble.Text = "Error:linkClick:" + ex.Message.ToString()
    '    End Try


    'End Sub

End Class


