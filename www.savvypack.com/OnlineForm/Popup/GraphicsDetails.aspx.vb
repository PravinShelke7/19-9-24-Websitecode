Imports System.Data
Imports System.Data.OleDb
Imports System
Imports SavvyGetData
Imports SavvyUpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Imports System.Reflection

Partial Class Pages_PopUp_GraphicsDetails
    Inherits System.Web.UI.Page
    Dim objUpIns As New UpdateInsert()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("SBack") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If
            If Not IsPostBack Then
                Session("SeqCnt") = "1"
                If Request.QueryString("Type").ToString = "E" Then
                    GetGraphicDetails()
                    btnDelete.Visible = True
                    'Started Activity Log Changes
                    Try
                        objUpIns.InsertLog1(Session("UserId").ToString(), "GraphicsDetails.aspx", "Opened Edit Graphics Details Popup for Project:" + Session("ProjectId") + " and Graphics:" + txtName.Text + "", Session("ProjectId"), Session("SPROJLogInCount").ToString())

                    Catch ex As Exception

                    End Try
                    'Ended Activity Log Changes
                Else
                    btnDelete.Visible = False
                    'Started Activity Log Changes
                    Try
                        objUpIns.InsertLog1(Session("UserId").ToString(), "GraphicsDetails.aspx", "Opened Graphics Details Popup for Project:" + Session("ProjectId") + "", Session("ProjectId"), Session("SPROJLogInCount").ToString())

                    Catch ex As Exception

                    End Try
                    'Ended Activity Log Changes
                End If
                If Request.QueryString("isDisabled") = "1" Then
                    DisableControls(Page)
                End If
                ' btnUpdate.Attributes.Add("onclick", "return CheckGraphicsDetails('txtName','txtDesg','txtColor','txtVolDesg','txtSkuDesg','txtOrdSize','txtRunSize');")

            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    Private Sub DisableControls(ByVal control As System.Web.UI.Control)

        For Each c As System.Web.UI.Control In control.Controls

            ' Get the Enabled property by reflection.
            Dim type As Type = c.GetType
            Dim prop As PropertyInfo = type.GetProperty("Enabled")

            ' Set it to False to disable the control.
            If Not prop Is Nothing Then
                prop.SetValue(c, False, Nothing)
            End If

            ' Recurse into child controls.
            If c.Controls.Count > 0 Then
                Me.DisableControls(c)
            End If

        Next

    End Sub
    Protected Sub GetGraphicDetails()
        Dim ds As New DataSet
        Dim objGetData As New Selectdata
        Try
            ds = objGetData.GetGraphicsDetails(Request.QueryString("GraphicsId").ToString, Request.QueryString("ProjectId").ToString)
            txtName.Text = ds.Tables(0).Rows(0).Item("NAME").ToString()
            hidName.Value = ds.Tables(0).Rows(0).Item("NAME").ToString()
            txtDesg.Text = ds.Tables(0).Rows(0).Item("DESIGN").ToString()
            txtColor.Text = ds.Tables(0).Rows(0).Item("COLORS").ToString()
            txtVolDesg.Text = ds.Tables(0).Rows(0).Item("VOLDESIGN").ToString()
            txtInfo.Text = ds.Tables(0).Rows(0).Item("INFORMATION").ToString()
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetGraphicDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Dim objUpIns As New UpdateInsert()
        Dim ds As New DataSet
        Dim objGetData As New Selectdata()
        Dim dsData As New DataSet
        Dim seq As Integer = 0
        Try

            If Request.QueryString("Type") = "N" Then
                If txtName.Text <> "" Then
                    ds = objGetData.GetModelValueDetails(Session("ProjectId"), "10", txtName.Text.Replace("'", "''"))
                    If ds.Tables(0).Rows.Count = 0 Then
                        objUpIns.AddGraphicsDetails(Session("ProjectId"), txtName.Text.Replace("'", "''"), txtDesg.Text.Replace("'", "''"), txtColor.Text.Replace("'", "''"), txtVolDesg.Text.Replace("'", "''"), txtInfo.Text.Replace("'", "''"))
                        seq = objUpIns.InsertProjCategoryDetails(Session("ProjectId"), "10", txtName.Text.Replace("'", "''"))

                        'Started Activity Log Changes
                        Try
                            objUpIns.InsertLog1(Session("UserId").ToString(), "GraphicsDetails.aspx", "Added New Graphics Details for Project:" + Session("ProjectId") + "", Session("ProjectId"), Session("SPROJLogInCount").ToString())
                            Dim ht As Hashtable = New Hashtable()
                            If Session("ht") IsNot Nothing Then
                                ht = DirectCast(Session("ht"), Hashtable)
                            End If
                            Dim str() As String
                            For i = 1 To ht.Count
                                For Each item As Object In ht
                                    str = item.Key.ToString().Split("-")
                                    If str(1) = i Then
                                        objUpIns.EditInsertLog(Session("UserId").ToString(), "13", str(0), seq.ToString(), item.Value.ToString().Replace("'", "''"), Session("ProjectId"), Session("SPROJLogInCount").ToString())
                                        Exit For
                                    End If
                                Next
                            Next
                            Session("ht") = Nothing
                            Session("SeqCnt") = "1"
                        Catch ex As Exception

                        End Try
                        'Ended Activity Log Changes
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClosePage", "ClosePage('N')", True)
                    Else
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Graphics Name " + txtName.Text + " already exist.');", True)
                    End If
                Else
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Graphics not Added Successfully.Please Enter Graphics Name.');", True)
                End If
            Else
                If txtName.Text <> "" Then
                    ds = objGetData.GetExistGraphicDetails(txtName.Text.Replace("'", "''"), Request.QueryString("ProjectId").ToString(), Request.QueryString("GraphicsId").ToString())
                    If ds.Tables(0).Rows.Count = 0 Then
                        objUpIns.UpdateGraphicDetails(Request.QueryString("GraphicsId").ToString, Request.QueryString("ProjectId").ToString, txtName.Text.Replace("'", "''"), txtDesg.Text.Replace("'", "''"), txtColor.Text.Replace("'", "''"), txtVolDesg.Text.Replace("'", "''"), txtInfo.Text.Replace("'", "''"))
                        objUpIns.UpdateProjCategoryDetails(Session("ProjectId"), "10", txtName.Text.Replace("'", "''"), Request.QueryString("ProjCatId").ToString)

                        dsData = objGetData.GetExistingModelDetails(Session("ProjectId"))

                        If dsData.Tables(0).Rows.Count > 0 Then
                            For i = 0 To dsData.Tables(0).Rows.Count - 1
                                If dsData.Tables(0).Rows(i).Item("GRAPHICS").ToString() = hidName.Value Then
                                    objUpIns.EditModelDetail(dsData.Tables(0).Rows(i).Item("MODELID").ToString(), Session("ProjectId"), txtName.Text.Replace("'", "''"), "GRAPHICS")
                                End If
                            Next
                        End If
                        'Started Activity Log Changes
                        Try
                            objUpIns.InsertLog1(Session("UserId").ToString(), "GraphicsDetails.aspx", "Edited Graphics Details for Project:" + Session("ProjectId") + " and Graphics:" + txtName.Text.Replace("'", "''") + "", Session("ProjectId"), Session("SPROJLogInCount").ToString())
                            Dim ht As Hashtable = New Hashtable()
                            If Session("ht") IsNot Nothing Then
                                ht = DirectCast(Session("ht"), Hashtable)
                            End If
                            Dim str() As String
                            For i = 1 To ht.Count
                                For Each item As Object In ht
                                    str = item.Key.ToString().Split("-")
                                    If str(1) = i Then
                                        objUpIns.EditInsertLog(Session("UserId").ToString(), "13", str(0), Request.QueryString("GraphicsId").ToString(), item.Value.ToString().Replace("'", "''"), Session("ProjectId"), Session("SPROJLogInCount").ToString())
                                        Exit For
                                    End If
                                Next
                            Next
                            Session("ht") = Nothing
                            Session("SeqCnt") = "1"
                        Catch ex As Exception

                        End Try
                        'Ended Activity Log Changes
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClosePage", "ClosePage('E')", True)
                    Else
                        GetGraphicDetails()
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Packer name Already Exist.');", True)
                    End If

                Else
                    GetGraphicDetails()
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Graphic not Added Successfully.Please Enter Graphic Name.');", True)
                End If
            End If


        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim ds As New DataSet
        Dim dv As New DataView
        Dim dt As New DataTable
        Dim objGetData As New SavvyGetData.Selectdata()
        Try
            objUpIns.DeleteGraphicDetails(Request.QueryString("GraphicsId").ToString(), Request.QueryString("ProjectId").ToString(), hidName.Value.Replace("'", "''"))
            objUpIns.DeleteProjCategoryDetails(Session("ProjectId"), "10", hidName.Value.Replace("'", "''"), Request.QueryString("ProjCatId").ToString())
            ds = objGetData.GetExistingModelDetails(Session("ProjectId"))
            dv = ds.Tables(0).DefaultView()
            dv.RowFilter = "GRAPHICS='" + hidName.Value.Replace("'", "''") + "'"

            dt = dv.ToTable()
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    objUpIns.DeleteModelByType(Request.QueryString("ProjectId").ToString(), dt.Rows(i).Item("MODELID").ToString(), "GRAPH", hidName.Value.Replace("'", "''"))
                Next
            End If
            'Started Activity Log Changes
            Try
                objUpIns.InsertLog1(Session("UserId").ToString(), "GraphicsDetails.aspx", "Deleted Graphics Details for Project:" + Session("ProjectId") + "", Session("ProjectId"), Session("SPROJLogInCount").ToString())

            Catch ex As Exception

            End Try
            'Ended Activity Log Changes
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Graphic deleted Successfully.');", True)
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClosePage", "ClosePage('E')", True)
        Catch ex As Exception

        End Try

    End Sub

    <System.Web.Services.WebMethod()>
    Public Shared Function UpdateCase(ByVal Header As String, ByVal text As String) As String
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
                ht.Add(Header + "-" + str(1).ToString(), text)
            Else
                ht.Add(Header + "-" + seq.ToString(), text)
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
