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
Partial Class Pages_SavvyPackPro_Popup_GetCOLPopup
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                hypCOLDes.Value = Request.QueryString("Des").ToString()
                hidCOLid.Value = Request.QueryString("ID").ToString()
                BindColType()
            End If

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub BindColType()
        Try
            Dim lst As New ListItem
            Dim objGetData As New SavvyProGetData
            Dim ds As New DataSet()
            Dim dsChk As New DataSet()
            Dim flag As Integer
            If hidCOLid.Value = "hidColid1" Then
                ds = objGetData.GetColumnTypeCol1()

            Else
                dsChk = objGetData.GetRFPColumns(Session("PriceID"))
                If dsChk.Tables(0).Rows.Count > 0 Then
                    For i = 0 To dsChk.Tables(0).Rows.Count - 1
                        If dsChk.Tables(0).Rows(i).Item("COLTYPEID").ToString() = "5" Then
                            flag = 1
                            Exit For
                        Else
                            flag = 0
                        End If
                    Next
                    If flag = 1 Then
                        ds = objGetData.GetColumnTypeCol2("2,3,4")
                    Else
                        ds = objGetData.GetColumnTypeCol2("1,2,3,4")
                    End If
                Else
                    ds = objGetData.GetColumnTypeCol2("1,2,3,4")
                End If
                ' ds = objGetData.GetColumnTypeCol2()

            End If
            'ds = objGetData.GetColumnType()
            If ds.Tables(0).Rows.Count > 0 Then
                lst.Text = "--Select--"
                lst.Value = "0"
                lst.Attributes.Add("COLTYPEID", "-1")
                lst.Attributes.Add("DESCRIPTION", "Select Type")
                With ddlColType
                    .Items.Add(lst)
                    .SelectedItem.Value = "0"
                    .AppendDataBoundItems = True
                    .DataSource = ds
                    .DataTextField = "DESCRIPTION"
                    .DataValueField = "COLTYPEID"
                    .DataBind()
                End With
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlColType_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlColType.SelectedIndexChanged
        Dim objGetData As New SavvyProGetData
        Dim ds As New DataSet()
        rwCOLValue.Visible = True

        If ddlColType.SelectedValue = "1" Then
            ds = objGetData.GetMASTERGROUP(ddlColType.SelectedValue, Session("USERID").ToString(), Session("hidRfpID").ToString())
            If ds.Tables(0).Rows.Count > 0 Then
                lblColValue.Text = ddlColType.SelectedItem.Text + ":"

                With ddlCOLValue
                    .DataSource = ds
                    .DataTextField = "DESCRIPTION"
                    .DataValueField = "MGROUPID"
                    .DataBind()
                End With
            End If
        ElseIf ddlColType.SelectedValue = "2" Then
            ds = objGetData.GetPackMNTGROUP(ddlColType.SelectedValue)
            If ds.Tables(0).Rows.Count > 0 Then
                lblColValue.Text = ddlColType.SelectedItem.Text + ":"

                With ddlCOLValue
                    .DataSource = ds
                    .DataTextField = "DESCRIPTION"
                    .DataValueField = "PACKMMNTID"
                    .DataBind()
                End With
            End If
        ElseIf ddlColType.SelectedValue = "3" Then
            ds = objGetData.GetPRICEREQGROUP(ddlColType.SelectedValue)
            If ds.Tables(0).Rows.Count > 0 Then
                lblColValue.Text = ddlColType.SelectedItem.Text + ":"

                With ddlCOLValue
                    .DataSource = ds
                    .DataTextField = "DESCRIPTION"
                    .DataValueField = "PRICEREQID"
                    .DataBind()
                End With
            End If
        ElseIf ddlColType.SelectedValue = "4" Then
            ds = objGetData.GetOtherGROUP(ddlColType.SelectedValue)
            If ds.Tables(0).Rows.Count > 0 Then
                lblColValue.Text = ddlColType.SelectedItem.Text + ":"

                With ddlCOLValue
                    .DataSource = ds
                    .DataTextField = "DESCRIPTION"
                    .DataValueField = "OTHERVALID"
                    .DataBind()
                End With
            End If
        ElseIf ddlColType.SelectedValue = "5" Then
            ds = objGetData.GetSkuPOSubCol(ddlColType.SelectedValue)
            If ds.Tables(0).Rows.Count > 0 Then
                lblColValue.Text = ddlColType.SelectedItem.Text + ":"

                With ddlCOLValue
                    .DataSource = ds
                    .DataTextField = "DESCRIPTION"
                    .DataValueField = "SKUPOID"
                    .DataBind()
                End With
                rwCOLValue.Visible = True
            End If
        End If


    End Sub

    Protected Sub btnSumitt_Click(sender As Object, e As System.EventArgs) Handles btnSumitt.Click
        Try
            Dim id As String = ""
            Dim des As String = ""
            Dim ds As DataSet
            Dim objUpInsdata As New SavvyProUpInsData
            Dim objGetdata As New SavvyProGetData
            Dim flag As Integer
            Dim strdes As String = String.Empty
            Dim i As String = hidCOLid.Value.Substring(hidCOLid.Value.Length - 1, 1)
            
            id = ddlColType.SelectedValue + "-" + ddlCOLValue.SelectedValue
            des = ddlCOLValue.SelectedItem.Text
            'pt changes
            ds = objGetdata.GetRFPColumns(Session("PriceID"))
            If ds.Tables(0).Rows.Count > 0 Then
                For j = 0 To ds.Tables(0).Rows.Count - 1
                    strdes = ds.Tables(0).Rows(j).Item("COLTYPEID").ToString() + "-" + ds.Tables(0).Rows(j).Item("COLVALUEID").ToString()
                    If strdes = id Then 'ds.Tables(0).Rows(j).Item("DESCRIPTION").ToString() = des.ToString() Then
                        flag = 1
                        Exit For
                    Else
                        flag = 0
                    End If
                Next
                If flag = 1 Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert1", "alert('" + des.ToString() + " is already selected.')", True)

                Else
                    objUpInsdata.InsertRFPColumnsDataTemp(Session("PriceID"), ddlColType.SelectedValue, ddlCOLValue.SelectedValue, i)
                    ClientScript.RegisterStartupScript(Me.GetType(), "FilterSel", "ColumnDet('" + des.ToString() + "','" + id.ToString() + "');", True)

                End If
            Else
                objUpInsdata.InsertRFPColumnsDataTemp(Session("PriceID"), ddlColType.SelectedValue, ddlCOLValue.SelectedValue, i)
                ClientScript.RegisterStartupScript(Me.GetType(), "FilterSel", "ColumnDet('" + des.ToString() + "','" + id.ToString() + "');", True)

            End If

            'pt changes end
            ' objUpInsdata.InsertRFPColumnsDataTemp(Session("PriceID"), ddlColType.SelectedValue, ddlCOLValue.SelectedValue, i)

        Catch ex As Exception

        End Try
    End Sub
End Class
