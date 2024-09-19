Imports System.Data
Imports System.Data.OleDb
Imports System.HttpStyleUriParser
Imports System.Web.UI.WebControls
Imports S4GetData
Imports S4UpInsData
Partial Class Pages_Sustain4_Category_ManageCategory
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                BindCategory()
                hdnPrevCount.Value = "0"
            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub BindCategory()
        Dim ds As New DataTable
        Dim objGetData As New S4GetData.Selectdata
        Try
            Dim lst As New ListItem()
            lst.Text = "--Select Category set--"
            lst.Value = -1
            ddlCatSet.Items.Clear()
            ddlCatSet.Items.Add(lst)
            ddlCatSet.AppendDataBoundItems = True

            ds = objGetData.GetCategorySet("", Session("UserId").ToString(), ddlPNameE.SelectedValue)
            With ddlCatSet
                .DataValueField = "CATEGORYSETID"
                .DataTextField = "CATEGORYSETNAME"
                .DataSource = ds
                .DataBind()
            End With

            ds = objGetData.GetCategorySet("", Session("UserId").ToString(), ddlPNameD.SelectedValue)
            ddlCatSetD.Items.Clear()
            ddlCatSetD.Items.Add(lst)
            ddlCatSetD.AppendDataBoundItems = True
            With ddlCatSetD
                .DataValueField = "CATEGORYSETID"
                .DataTextField = "CATEGORYSETNAME"
                .DataSource = ds
                .DataBind()
            End With

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Dim objGetData As New S4GetData.Selectdata
        Dim objUpdate As New S4UpInsData.UpdateInsert
        Dim message As String
        Dim flag As Boolean = False
        Dim dt As New DataTable
        Try
            dt = objGetData.GetCategorySet(txtCatSet.Text.Trim(), Session("UserId").ToString(), ddlPName.SelectedValue)
            If dt.Rows.Count > 0 Then
                message = "Category set " + txtCatSet.Text.Trim() + " already exist."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "New_WinCopy", "alert('" + message + "');", True)
            Else
                objUpdate.AddCategorySet(txtCatSet.Text.Trim(), Session("USERID").ToString(), ddlPName.SelectedValue)
                txtCatSet.Text = ""
                flag = True
                BindCategory()
            End If


        Catch ex As Exception
            flag = False
        End Try
        If flag Then
            message = "Category set " + txtCatSet.Text.Trim() + " created successfully."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "New_WinCopy", "alert('" + message + "');", True)
        End If
    End Sub


    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try
            trTable.visible = True
            trButton.visible = True
            GetPageDetails()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub GetPageDetails()
        Dim ds As New DataSet
        Dim objGetData As New S1GetData.Selectdata
        Dim objS4GetData As New S4GetData.Selectdata
        Dim i As New Integer
        Dim j As New Integer
        Dim trHeader As New TableRow
        Dim trInner As New TableRow
        Dim tdHeader As TableCell
        Dim lbl As New Label
        Dim hid As New HiddenField
        Dim Link As New HyperLink
        Dim txt As New TextBox
        Dim tdInner As TableCell
        Dim dt As New DataTable
        Dim message As String = String.Empty
        Try

            dt = objS4GetData.GetCategoryBySet(ddlCatSet.SelectedValue, "")



            For i = 1 To 2
                tdHeader = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "40px", "Item", "1")
                        trHeader.Controls.Add(tdHeader)
                    Case 2
                        HeaderTdSetting(tdHeader, "210px", "Category Name", "1")
                        trHeader.Controls.Add(tdHeader)

                End Select
            Next
            tblComp.Controls.Add(trHeader)


            'Inner
            For i = 1 To Convert.ToInt32(dt.Rows.Count + txtCatCount.Text)
                trInner = New TableRow
                For j = 1 To 2
                    tdInner = New TableCell

                    Select Case j
                        Case 1
                            'Item
                            InnerTdSetting(tdInner, "", "Center")
                            tdInner.Text = "<b>" + i.ToString() + "</b>"
                            trInner.Controls.Add(tdInner)
                        Case 2
                            InnerTdSetting(tdInner, "", "Center")

                            If i <= dt.Rows.Count Then
                                lbl = New Label
                                lbl.CssClass = "NormalLabel"
                                lbl.Text = dt.Rows(i - 1).Item("CATEGORYNAME").ToString()
                                lbl.Style.Add("text-align", "Left")
                                lbl.Width = 170
                                tdInner.Controls.Add(lbl)
                            Else
                                txt = New TextBox
                                txt.CssClass = "SmallTextBox"
                                txt.Style.Add("text-align", "Left")
                                txt.ID = "CATNAME" + (i - dt.Rows.Count).ToString()
                                txt.Text = ""
                                txt.Width = 170
                                txt.MaxLength = 50
                                tdInner.Controls.Add(txt)
                            End If
                            trInner.Controls.Add(tdInner)


                    End Select
                Next

                trInner.CssClass = "AlterNateColor3"
                tblComp.Controls.Add(trInner)
            Next
        Catch ex As Exception

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
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnAddCat_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddCat.Click
        Dim objGetData As New S4GetData.Selectdata
        Dim objUpdate As New S4UpInsData.UpdateInsert
        Dim message As String = String.Empty
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim strCategory(txtCatCount.Text - 1) As String
        Dim ds As New DataTable
        Dim strCat As String = String.Empty
        Dim flag As Boolean = False
        Dim flag1 As Boolean = False
        Try

            message = "**********************************************************\n"
            For i = 0 To txtCatCount.Text - 1
                strCategory(i) = Request.Form("ctl00$Sustain4ContentPlaceHolder$CATNAME" + (i + 1).ToString())
                'Checking Duplicate
                ds = objGetData.GetCategoryBySet(ddlCatSet.SelectedValue, strCategory(i).Trim())
                If ds.Rows.Count > 0 Then
                    message = message + "Category name " + strCategory(i) + " already exist.\n"
                    flag = True
                End If
            Next
            message = message + "**********************************************************\n"

            If (flag = False) Then
                'Checking Duplicate Data
                For i = 0 To txtCatCount.Text - 1
                    For j = i + 1 To txtCatCount.Text - 1
                        If strCategory(i) = strCategory(j) Then
                            flag1 = True
                        End If
                    Next
                Next
                If (flag1 = False) Then
                    objUpdate.AddCategory(strCategory, ddlCatSet.SelectedValue)
                    BindCategory()
                    txtCatCount.Text = ""
                    message = "Categories added successfully!"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "New_WinCopy", "alert('" + message + "');", True)
                    trTable.Visible = False
                    trButton.Visible = False
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "New_WinCopy", "alert('Category Name should not be duplicated!!!');", True)
                    GetPageDetails()
                End If

            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "New_WinCopy", "alert('" + message + "');", True)
                GetPageDetails()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlCatSet_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCatSet.SelectedIndexChanged
        Dim objGetData As New S4GetData.Selectdata
        Dim ds As New DataTable
        Dim message As String = String.Empty
        Try
            ds = objGetData.GetCategoryBySet(ddlCatSet.SelectedValue, "")
            txtCatCount.Enabled = True
            If ds.Rows.Count > 0 Then
                hdnPrevCount.Value = ds.Rows.Count.ToString()
                message = "for selected Category set," + ds.Rows.Count.ToString() + " Categories  is already added.You can add more.."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "New_WinCopy", "alert('" + message + "');", True)
                trTable.Visible = False
                trButton.Visible = False
            Else
                hdnPrevCount.Value = "0"
                lblMsg.Text = ""
                txtCatCount.Enabled = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            trTable.visible = False
            trButton.visible = False
            BindCategory()
            txtCatCount.Text = ""
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim ObjUpIns As New UpdateInsert()
        Dim message As String = String.Empty
        Try
            ObjUpIns.DeleteCategorySet(ddlCatSetD.SelectedValue)
            message = "Category set " + ddlCatSetD.SelectedItem.Text + " deleted successfully."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "New_WinCopy", "alert('" + message + "');", True)
            BindCategory()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlPNameE_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPNameE.SelectedIndexChanged
        Try
            BindCategory()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlPNameD_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPNameD.SelectedIndexChanged
        Try
            BindCategory()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnAddCategory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddCategory.Click
        Try
            Response.Redirect("~/Pages/Sustain4/Category/AddEditCategory.aspx")
        Catch ex As Exception

        End Try
    End Sub
End Class
