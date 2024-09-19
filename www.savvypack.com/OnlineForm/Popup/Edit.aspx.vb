Imports System
Imports System.Data
Partial Class Pages_PopUp_Edit
    Inherits System.Web.UI.Page

    Dim objUpIns As New SavvyUpInsData.UpdateInsert()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("SBack") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If
            If Not IsPostBack Then
                Session("ht") = Nothing
                Session("SeqCnt") = "1"
                hidType.Value = Request.QueryString("Type").ToString()
                hidProjectId.Value = Request.QueryString("ProjectId").ToString()
                Try
                    If hidType.Value = "PROD" Then
                        objUpIns.InsertLog1(Session("UserId").ToString(), "Edit.aspx", "Opened Edit Popup for Product to Pack", Session("ProjectId"), Session("SPROJLogInCount").ToString())

                    ElseIf hidType.Value = "SIZE" Then
                        objUpIns.InsertLog1(Session("UserId").ToString(), "Edit.aspx", "Opened Edit Popup for Package Size", Session("ProjectId"), Session("SPROJLogInCount").ToString())

                    ElseIf hidType.Value = "TYPE" Then
                        objUpIns.InsertLog1(Session("UserId").ToString(), "Edit.aspx", "Opened Edit Popup for Package Type", Session("ProjectId"), Session("SPROJLogInCount").ToString())

                    ElseIf hidType.Value = "GEOG" Then
                        objUpIns.InsertLog1(Session("UserId").ToString(), "Edit.aspx", "Opened Edit Popup for Geography", Session("ProjectId"), Session("SPROJLogInCount").ToString())

                    ElseIf hidType.Value = "VCHAIN" Then
                        objUpIns.InsertLog1(Session("UserId").ToString(), "Edit.aspx", "Opened Edit Popup for Value Chain", Session("ProjectId"), Session("SPROJLogInCount").ToString())

                    ElseIf hidType.Value = "SPFET1" Then
                        objUpIns.InsertLog1(Session("UserId").ToString(), "Edit.aspx", "Opened Edit Popup for Special Features 1", Session("ProjectId"), Session("SPROJLogInCount").ToString())

                    ElseIf hidType.Value = "SPFET2" Then
                        objUpIns.InsertLog1(Session("UserId").ToString(), "Edit.aspx", "Opened Edit Popup for Special Features 2", Session("ProjectId"), Session("SPROJLogInCount").ToString())
                    End If
                Catch ex As Exception

                End Try
                GetDetails()
                hdnUpdate.Value = "0"
                hvCaseGrd.Value = "0"
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub GetDetails()
        Dim ds As New DataSet
        Dim objGetData As New SavvyGetData.Selectdata()
        Try

            ds = objGetData.GetProductDetails(hidProjectId.Value, hidType.Value)
            Session("EditData") = ds
            grdProducts.DataSource = ds
            If hidType.Value = "PROD" Then
                grdProducts.Columns(2).HeaderText = "Product To Pack"
            ElseIf hidType.Value = "SIZE" Then
                grdProducts.Columns(2).HeaderText = "Package Size"
            ElseIf hidType.Value = "TYPE" Then
                grdProducts.Columns(2).HeaderText = "Package Type"
            ElseIf hidType.Value = "GEOG" Then
                grdProducts.Columns(2).HeaderText = "Geography"
            ElseIf hidType.Value = "VCHAIN" Then
                grdProducts.Columns(2).HeaderText = "Value Chain"
            ElseIf hidType.Value = "SPFET1" Then
                grdProducts.Columns(2).HeaderText = "Special Features 1"
            ElseIf hidType.Value = "SPFET2" Then
                grdProducts.Columns(2).HeaderText = "Special Features 2"
            End If

            grdProducts.DataBind()

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetReportDetails:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub grdProducts_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdProducts.Sorting
        Try
            Dim Dts As New DataSet
            Dim dv As DataView
            Dim numberDiv As Integer
            numberDiv = Convert.ToInt16(hvCaseGrd.Value.ToString())
            Dts = Session("EditData")
            dv = Dts.Tables(0).DefaultView

            If ((numberDiv Mod 2) = 0) Then
                dv.Sort = e.SortExpression + " " + "DESC"
            Else
                dv.Sort = e.SortExpression + " " + "ASC"
            End If

            numberDiv += 1
            hvCaseGrd.Value = numberDiv.ToString()
            grdProducts.DataSource = dv
            grdProducts.DataBind()

            objUpIns.InsertLog1(Session("UserId").ToString(), "Edit.aspx", "Sorted Data by its value", Session("ProjectId"), Session("SPROJLogInCount").ToString())
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Dim txtName As New TextBox

        Dim flag As Boolean = True
        Dim objUpdateData As New SavvyUpInsData.UpdateInsert()
        Dim Name(1000) As String
        Dim ProjCatID(1000) As String
        Dim CatID(1000) As String
        Dim oldName(1000) As String
        Dim count As Integer = 0
        Dim grpId As String = ""
        Dim lblProjCatId As New Label
        Dim lblCatId As New Label
        Dim lblName As New Label
        Dim msg As String = ""
        Dim i As Integer = 0
        Dim j As Integer = 0
        Try
            For Each Gr As GridViewRow In grdProducts.Rows
                txtName = grdProducts.Rows(Gr.RowIndex).FindControl("txtName")
                Name(count) = txtName.Text.Trim().Replace("'", "''")

                lblName = grdProducts.Rows(Gr.RowIndex).FindControl("lblName")
                oldName(count) = lblName.Text.Replace("'", "''")

                lblProjCatId = grdProducts.Rows(Gr.RowIndex).FindControl("lblProjCatID")
                lblCatId = grdProducts.Rows(Gr.RowIndex).FindControl("lblCategoryId")
                ProjCatID(count) = lblProjCatId.Text
                CatID(count) = lblCatId.Text
                count += 1
            Next
            'Check for Duplicate Entry for Group
            Dim Jval As String = ""
            For i = 0 To count - 1
                Dim countVal As Integer = 0
                If Jval.Contains(i) Then
                    Exit For
                Else
                    For j = 0 To count - 1
                        If Name(i).ToString() = Name(j).ToString() Then
                            countVal += 1
                            If countVal = 2 Then
                                flag = False
                                Jval = Jval + "," + j.ToString()
                                msg = msg + "Group Name:" + Name(i).ToString() + ""
                                Exit For

                            End If

                        End If
                    Next
                End If
            Next
            If flag = False Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "_New", " alert('These Product Name already exist:\n" + msg + "');", True)
            Else
                objUpdateData.UpdateProductName(Name, CatID, ProjCatID, count, hidProjectId.Value)

                For i = 0 To count - 1
                    If Name(i) <> oldName(i) Then
                        objUpdateData.UpdateModelDetail(hidProjectId.Value, Name(i), oldName(i), hidType.Value)
                    End If
                Next



            End If

        Catch ex As Exception
            flag = False
            Throw New Exception("btnUpdate_Click:" + ex.Message)
        End Try
        If (flag) Then
            hdnUpdate.Value = "1"
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "_New", " alert('Update was Successful.');", True)
            Try
                If hidType.Value = "PROD" Then
                    objUpIns.InsertLog1(Session("UserId").ToString(), "Edit.aspx", "Clicked on Update button for Edit Product to Pack value", Session("ProjectId"), Session("SPROJLogInCount").ToString())

                ElseIf hidType.Value = "SIZE" Then
                    objUpIns.InsertLog1(Session("UserId").ToString(), "Edit.aspx", "Clicked on Update button for Edit Package Size value", Session("ProjectId"), Session("SPROJLogInCount").ToString())

                ElseIf hidType.Value = "TYPE" Then
                    objUpIns.InsertLog1(Session("UserId").ToString(), "Edit.aspx", "Clicked on Update button for Edit Package Type value", Session("ProjectId"), Session("SPROJLogInCount").ToString())

                ElseIf hidType.Value = "GEOG" Then
                    objUpIns.InsertLog1(Session("UserId").ToString(), "Edit.aspx", "Clicked on Update button for Edit Geography value", Session("ProjectId"), Session("SPROJLogInCount").ToString())

                ElseIf hidType.Value = "VCHAIN" Then
                    objUpIns.InsertLog1(Session("UserId").ToString(), "Edit.aspx", "Clicked on Update button for Edit Value Chain value", Session("ProjectId"), Session("SPROJLogInCount").ToString())

                ElseIf hidType.Value = "SPFET1" Then
                    objUpIns.InsertLog1(Session("UserId").ToString(), "Edit.aspx", "Clicked on Update button for Edit Special Features 1 value", Session("ProjectId"), Session("SPROJLogInCount").ToString())

                ElseIf hidType.Value = "SPFET2" Then
                    objUpIns.InsertLog1(Session("UserId").ToString(), "Edit.aspx", "Clicked on Update button for Edit Special Features 2 value", Session("ProjectId"), Session("SPROJLogInCount").ToString())
                End If
                Dim ht As Hashtable = New Hashtable()
                If Session("ht") IsNot Nothing Then
                    ht = DirectCast(Session("ht"), Hashtable)
                End If
                Dim str() As String
                For i = 1 To ht.Count
                    For Each item As Object In ht
                        str = item.Key.ToString().Split("-")
                        If str(1) = i Then
                            objUpIns.EditInsertLog(Session("UserId").ToString(), "6", str(0), str(2), item.Value.ToString().Replace("'", "''"), hidProjectId.Value, Session("SPROJLogInCount").ToString())
                            Exit For
                        End If
                    Next
                Next
                Session("ht") = Nothing
                Session("SeqCnt") = "1"
            Catch ex As Exception
                Session("ht") = Nothing
                Session("SeqCnt") = "1"
            End Try
            GetDetails()
        End If
    End Sub

    Protected Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Try
            objUpIns.InsertLog1(Session("UserId").ToString(), "Edit.aspx", "Clicked on Close button", Session("ProjectId"), Session("SPROJLogInCount").ToString())
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "GroupSearch();", True)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click

        Dim chk As New CheckBox
        Dim lblProjCatId As New Label
        Dim lblName As New Label
        Dim objUpdateData As New SavvyUpInsData.UpdateInsert()
        Dim flag As Boolean = True
        Dim objGetData As New SavvyGetData.Selectdata()
        Dim ds As New DataSet
        Dim dv As New DataView
        Dim dt As New DataTable
        Try
            ds = objGetData.GetExistingModelDetails(Session("ProjectId"))
            dv = ds.Tables(0).DefaultView()
            For Each Gr As GridViewRow In grdProducts.Rows
                chk = grdProducts.Rows(Gr.RowIndex).FindControl("delete")
                If chk.Checked = True Then
                    lblProjCatId = grdProducts.Rows(Gr.RowIndex).FindControl("lblProjCatID")
                    objUpdateData.DeleteProducts(lblProjCatId.Text)

                    lblName = grdProducts.Rows(Gr.RowIndex).FindControl("lblName")
                    If ds.Tables(0).Rows.Count > 0 Then
                        If hidType.Value = "PROD" Then
                            dv.RowFilter = "PRODPACK='" + lblName.Text.Replace("'", "''") + "'"
                        ElseIf hidType.Value = "SIZE" Then
                            dv.RowFilter = "PACKSIZE='" + lblName.Text.Replace("'", "''") + "'"
                        ElseIf hidType.Value = "TYPE" Then
                            dv.RowFilter = "PACKTYPE='" + lblName.Text.Replace("'", "''") + "'"
                        ElseIf hidType.Value = "GEOG" Then
                            dv.RowFilter = "GEOGRAPHY='" + lblName.Text.Replace("'", "''") + "'"
                        ElseIf hidType.Value = "VCHAIN" Then
                            dv.RowFilter = "VCHAIN='" + lblName.Text.Replace("'", "''") + "'"
                        ElseIf hidType.Value = "SPFET1" Then
                            dv.RowFilter = "SPFET1='" + lblName.Text.Replace("'", "''") + "'"
                        ElseIf hidType.Value = "SPFET2" Then
                            dv.RowFilter = "SPFET2='" + lblName.Text.Replace("'", "''") + "'"
                        End If
                        dt = dv.ToTable()
                        If dt.Rows.Count > 0 Then
                            For i = 0 To dt.Rows.Count - 1
                                objUpdateData.DeleteModelByType(hidProjectId.Value, dt.Rows(i).Item("MODELID").ToString(), hidType.Value, lblName.Text.Replace("'", "''"))
                            Next
                        End If

                    End If
                End If
            Next
            GetDetails()
        Catch ex As Exception
            flag = False
        End Try
        If (flag) Then
            hdnUpdate.Value = "1"
            If hidType.Value = "PROD" Then
                objUpIns.InsertLog1(Session("UserId").ToString(), "Edit.aspx", "Clicked on Delete button for Delete Product to Pack value", Session("ProjectId"), Session("SPROJLogInCount").ToString())
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "_New", " alert('Product To Pack(s) deleted Successfully.');", True)
            ElseIf hidType.Value = "SIZE" Then
                objUpIns.InsertLog1(Session("UserId").ToString(), "Edit.aspx", "Clicked on Delete button for Delete Package Size value", Session("ProjectId"), Session("SPROJLogInCount").ToString())
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "_New", " alert('Package Size(s) deleted Successfully.');", True)
            ElseIf hidType.Value = "TYPE" Then
                objUpIns.InsertLog1(Session("UserId").ToString(), "Edit.aspx", "Clicked on Delete button for Delete Package Type value", Session("ProjectId"), Session("SPROJLogInCount").ToString())
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "_New", " alert('Package Type(s) deleted Successfully.');", True)
            ElseIf hidType.Value = "GEOG" Then
                objUpIns.InsertLog1(Session("UserId").ToString(), "Edit.aspx", "Clicked on Delete button for Delete Geography value", Session("ProjectId"), Session("SPROJLogInCount").ToString())
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "_New", " alert('Geography(s) deleted Successfully.');", True)
            ElseIf hidType.Value = "VCHAIN" Then
                objUpIns.InsertLog1(Session("UserId").ToString(), "Edit.aspx", "Clicked on Delete button for Delete Value Chain value", Session("ProjectId"), Session("SPROJLogInCount").ToString())
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "_New", " alert('Value Chain(s) deleted Successfully.');", True)
            ElseIf hidType.Value = "SPFET1" Then
                objUpIns.InsertLog1(Session("UserId").ToString(), "Edit.aspx", "Clicked on Delete button for Delete Special Features 1 value", Session("ProjectId"), Session("SPROJLogInCount").ToString())
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "_New", " alert('Special Features 1(s) deleted Successfully.');", True)
            ElseIf hidType.Value = "SPFET2" Then
                objUpIns.InsertLog1(Session("UserId").ToString(), "Edit.aspx", "Clicked on Delete button for Delete Special Features 2 value", Session("ProjectId"), Session("SPROJLogInCount").ToString())
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "_New", " alert('Special Features 2(s) deleted Successfully.');", True)
            End If

        End If
    End Sub

    <System.Web.Services.WebMethod()>
    Public Shared Function UpdateCase(ByVal Header As String, ByVal text As String, ByVal valSeq As String) As String
        Try
            Dim ht As Hashtable = New Hashtable()
            Dim str() As String
            Dim seq As Integer = Convert.ToInt32(HttpContext.Current.Session("SeqCnt"))
            Dim flag As Boolean = False
            If HttpContext.Current.Session("ht") IsNot Nothing Then
                ht = DirectCast(HttpContext.Current.Session("ht"), Hashtable)
            End If

            For Each item As Object In ht
                str = item.Key.ToString().Split("-")
                If str(0) = Header Then
                    ht.Remove(item.Key)
                    flag = True
                    Exit For
                End If
            Next

            If flag Then
                ht.Add(Header + "-" + str(1).ToString() + "-" + valSeq, text)
            Else
                ht.Add(Header + "-" + seq.ToString() + "-" + valSeq, text)
                seq += 1
                HttpContext.Current.Session("SeqCnt") = seq
            End If
            HttpContext.Current.Session("ht") = ht
            Dim str1 As String = HttpContext.Current.Session("UserId").ToString()
            str1 = str1 + "Bhavesh"
            Return str1

        Catch ex As Exception

        End Try
    End Function
End Class
