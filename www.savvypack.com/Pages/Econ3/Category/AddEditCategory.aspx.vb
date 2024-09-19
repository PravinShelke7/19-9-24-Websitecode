Imports System.Data
Imports System.Data.OleDb
Imports System.HttpStyleUriParser
Imports System.Web.UI.WebControls
Imports E3GetData
Imports E3UpInsData
Partial Class Pages_Econ3_Category_AddEditCategory
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                BindCategorySet()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub BindCategorySet()
        Dim ds As New DataTable
        Dim objGetData As New E3GetData.Selectdata
        Try
            Dim lst As New ListItem()
            Dim lstCat As New ListItem()
            lst.Text = "--Select Category set--"
            lst.Value = -1
            ddlCatSet.Items.Clear()
            ddlCatSet.Items.Add(lst)
            ddlCatSet.AppendDataBoundItems = True

            ds = objGetData.GetCategorySet("", Session("UserId").ToString(), ddlPName.SelectedValue)
            With ddlCatSet
                .DataValueField = "CATEGORYSETID"
                .DataTextField = "CATEGORYSETNAME"
                .DataSource = ds
                .DataBind()
            End With

            'Binding Category
            lstCat.Text = "--Select Category--"
            lstCat.Value = -1
            ddlCat.Items.Clear()
            ddlCat.Items.Add(lstCat)

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub BindCategory()
        Dim ds As New DataTable
        Dim objGetData As New E3GetData.Selectdata
        Try
            Dim lst As New ListItem()
            lst.Text = "--Select Category--"
            lst.Value = -1
            ddlCat.Items.Clear()
            ddlCat.Items.Add(lst)
            ddlCat.AppendDataBoundItems = True

            ds = objGetData.GetCategoryBySet(ddlCatSet.SelectedValue, "")
            With ddlCat
                .DataValueField = "CATEGORYID"
                .DataTextField = "CATEGORYNAME"
                .DataSource = ds
                .DataBind()
            End With

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlCatSet_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCatSet.SelectedIndexChanged
        Try
            BindCategory()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            bindDetails()
            trList.Visible = True
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub bindDetails()
        Dim objGetData As New E3GetData.Selectdata
        Dim objUpdateData As New E3UpInsData.UpdateInsert
        Dim dt As New DataTable
        Dim dt1 As New DataSet
        Dim dt2 As New DataSet
        Dim dv1 As New DataView
        Dim dv2 As New DataView
        Dim i As Integer = 0

        Dim dtt1 As New DataTable
        Dim strCode As String = String.Empty
        Try
            'dt = objGetData.GetCategorySet(ddlCatSet.SelectedItem.ToString, Session("userID").ToString(), "")
            ' If dt.Rows.Count > 0 Then

            ' End If
            dt1 = objGetData.GetCategoryItems(ddlPName.SelectedValue)
            dt2 = objGetData.GetItemsByCat(ddlCat.SelectedValue)
            'Getting each item of selected Category
            For i = 0 To dt2.Tables(0).Rows.Count - 1
                If i = 0 Then
                    strCode = "'" + dt2.Tables(0).Rows(i).Item("ITEMNAME").ToString() + "'"
                Else
                    strCode = strCode + "," + "'" + dt2.Tables(0).Rows(i).Item("ITEMNAME").ToString() + "'"
                End If
            Next
            If strCode <> "" Then
                dv2 = dt1.Tables(0).DefaultView
                dv2.RowFilter = "CODE IN (" + strCode + ")"
                With lstRegion2
                    .DataSource = dv2
                    .DataTextField = "DES"
                    .DataValueField = "CODE"
                    .DataBind()
                End With

                dv1 = dt1.Tables(0).DefaultView
                dv1.RowFilter = "CODE NOT IN (" + strCode + ")"
                With lstRegion1
                    .DataSource = dv1
                    .DataTextField = "DES"
                    .DataValueField = "CODE"
                    .DataBind()
                End With
            Else
                With lstRegion1
                    .DataSource = dt1
                    .DataTextField = "DES"
                    .DataValueField = "CODE"
                    .DataBind()
                End With
                With lstRegion2
                    .DataSource = dt2
                    .DataTextField = "DES"
                    .DataValueField = "CODE"
                    .DataBind()
                End With
            End If


        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnFwd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFwd.Click
        Try
            Transfer1(lstRegion1, lstRegion2, False)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnRew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRew.Click
        Try
            Transfer2(lstRegion1, lstRegion2, False)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Transfer1(ByVal Fromlst As ListBox, ByVal Tolst As ListBox, ByVal IsAll As Boolean)
        Dim i As New Integer
        Dim ObjUpIns As New UpdateInsert()
        Try

            Dim cnt As Integer = 0
            Dim cntVal As String = Request.Form("ctl00$Econ3ContentPlaceHolder$lstRegion1")
            Dim strCount() As String = Regex.Split(cntVal, ",")
            Dim CaseIds(strCount.Length - 1) As String


            For i = 0 To Fromlst.Items.Count - 1
                If Fromlst.Items(i).Selected Then
                    CaseIds(cnt) = Fromlst.Items(i).Value
                    cnt += 1
                End If
            Next
            ObjUpIns.AddEditCatItem(ddlCat.SelectedValue, CaseIds)
            bindDetails()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Transfer2(ByVal Fromlst As ListBox, ByVal Tolst As ListBox, ByVal IsAll As Boolean)
        Dim i As New Integer
        Dim ObjUpIns As New UpdateInsert()
        Try

            Dim cnt As Integer = 0
            Dim cntVal As String = Request.Form("ctl00$Econ3ContentPlaceHolder$lstRegion2")
            Dim strCount() As String = Regex.Split(cntVal, ",")
            Dim itemName As String = String.Empty


            For i = 0 To Tolst.Items.Count - 1
                If Tolst.Items(i).Selected Then
                    If cnt = 0 Then
                        itemName = "'" + Tolst.Items(i).Value + "'"
                    Else
                        itemName = itemName + "," + "'" + Tolst.Items(i).Value + "'"
                    End If
                    cnt += 1
                End If
            Next
            ObjUpIns.DeleteCatItem(ddlCat.SelectedValue, itemName)
            bindDetails()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnOk_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnOk.Click
        Try
            BindCategorySet()
            BindCategory()
            trList.Visible = False
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Dim ObjUpIns As New UpdateInsert()
        Try
            ObjUpIns.RenameCategory(ddlCat.SelectedValue, txtCat.Text.Trim(), ddlCatSet.SelectedValue)
            BindCategorySet()
            BindCategory()
            trList.Visible = False
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim ObjUpIns As New UpdateInsert()
        Dim message As String = String.Empty
        Try
            ObjUpIns.DeleteCategory(ddlCat.SelectedValue, ddlCatSet.SelectedValue)
            message = "Category " + ddlCat.SelectedItem.Text + " deleted successfully."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "New_WinCopy", "alert('" + message + "');", True)
            BindCategorySet()
            BindCategory()
            trList.Visible = False
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlPName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPName.SelectedIndexChanged
        Try
            BindCategorySet()
        Catch ex As Exception

        End Try
    End Sub
End Class
