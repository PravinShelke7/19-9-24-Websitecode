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

Partial Class Pages_PopUp_StructureDetails
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
                PageDetails()
                hidProjectId.Value = Request.QueryString("ProjectId").ToString()
                If Request.QueryString("Type") = "N" Then
                    btnDelete.Visible = False
                    'Started Activity Log Changes
                    Try
                        objUpIns.InsertLog1(Session("UserId").ToString(), "StructureDetails.aspx", "Opened Structure Details Popup for Project:" + Session("ProjectId") + "", Session("ProjectId"), Session("SPROJLogInCount").ToString())

                    Catch ex As Exception

                    End Try
                    'Ended Activity Log Changes
                Else
                    btnDelete.Visible = True
                    'Started Activity Log Changes
                    Try
                        objUpIns.InsertLog1(Session("UserId").ToString(), "StructureDetails.aspx", "Opened Edit Structure Details Popup for Project:" + Session("ProjectId") + "", Session("ProjectId"), Session("SPROJLogInCount").ToString())

                    Catch ex As Exception

                    End Try
                    'Ended Activity Log Changes
                End If
                If Request.QueryString("isDisabled") = "1" Then
                    DisableControls(Page)
                End If
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

    Protected Sub PageDetails()
        Dim ds As New DataSet
        Dim objGetData As New Selectdata
        Dim trHeader As New TableRow
        Dim trHeader1 As New TableRow
        Dim trHeader2 As New TableRow
        Dim trInner As New TableRow
        Dim tdHeader As TableCell
        Dim tdHeader1 As TableCell
        Dim tdHeader2 As TableCell
        Dim tdInner As TableCell
        Dim txt As TextBox
        Try
            If Request.QueryString("Type") = "E" Then
                ds = objGetData.GetStructureDetails(Request.QueryString("StructureId").ToString(), Request.QueryString("ProjectId").ToString())
                txtName.Text = ds.Tables(0).Rows(0).Item("NAME").ToString()
                txtSize.Text = ds.Tables(0).Rows(0).Item("PACKSIZE").ToString()
                hidName.Value = ds.Tables(0).Rows(0).Item("NAME").ToString()
            End If
            For i = 1 To 6
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "40px", "Layers", "1")
                        trHeader.Controls.Add(tdHeader)

                    Case 2
                        HeaderTdSetting(tdHeader, "140px", "Material", "1")
                        trHeader.Controls.Add(tdHeader)

                    Case 3
                        HeaderTdSetting(tdHeader, "140px", "Dimension", "1")
                        trHeader.Controls.Add(tdHeader)

                    Case 4
                        HeaderTdSetting(tdHeader, "140px", "Density", "1")
                        trHeader.Controls.Add(tdHeader)

                    Case 5
                        HeaderTdSetting(tdHeader, "140px", "Price", "1")
                        trHeader.Controls.Add(tdHeader)

                    Case 6
                        HeaderTdSetting(tdHeader, "280px", "Additional Information ", "1")
                        trHeader.Controls.Add(tdHeader)

                End Select

            Next
            trHeader.Height = 30
            trHeader.Height = 30

            tblStruct.Controls.Add(trHeader)


            For i = 1 To 10
                trInner = New TableRow
                For j = 1 To 6
                    tdInner = New TableCell

                    Select Case j
                        Case 1
                            'Layer
                            InnerTdSetting(tdInner, "", "Center")
                            tdInner.Text = "<b>" + i.ToString() + "</b>"
                            trInner.Controls.Add(tdInner)
                        Case 2

                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SavvyTextBox"
                            txt.ID = "txtMat" + i.ToString()
                            If Request.QueryString("Type") = "E" Then
                                txt.Text = ds.Tables(0).Rows(0).Item("MAT" + i.ToString()).ToString()
                            End If
                            'txt.Attributes.Add("onchange", "javascript:CheckSP(this);")
                            txt.MaxLength = 100
                            txt.ToolTip = "Eg: Material Name"
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 3
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SavvyTextBox"
                            txt.ID = "txtDim" + i.ToString()
                            If Request.QueryString("Type") = "E" Then
                                txt.Text = ds.Tables(0).Rows(0).Item("DIM" + i.ToString()).ToString()
                            End If
                            'txt.Attributes.Add("onchange", "javascript:CheckSP(this);")
                            txt.MaxLength = 100
                            txt.ToolTip = "Eg: 5 mil, 6%, etc"
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 4
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SavvyTextBox"
                            txt.ID = "txtDen" + i.ToString()
                            If Request.QueryString("Type") = "E" Then
                                txt.Text = ds.Tables(0).Rows(0).Item("DEN" + i.ToString()).ToString()
                            End If
                            'txt.Attributes.Add("onchange", "javascript:CheckSP(this);")
                            txt.MaxLength = 100
                            txt.ToolTip = "Eg: 1.5g/cc, 6lb/gal, etc"
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 5
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SavvyTextBox"
                            txt.ID = "txtPrice" + i.ToString()
                            If Request.QueryString("Type") = "E" Then
                                txt.Text = ds.Tables(0).Rows(0).Item("PRICE" + i.ToString()).ToString()
                            End If
                            'txt.Attributes.Add("onchange", "javascript:CheckSP(this);")
                            txt.MaxLength = 100
                            txt.ToolTip = "Eg: 3.2$/gal,  5Euro/kg, etc"
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)

                        Case 6
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SavvyMultiTextBox"
                            txt.ID = "txtInfo" + i.ToString()
                            If Request.QueryString("Type") = "E" Then
                                txt.Text = ds.Tables(0).Rows(0).Item("INFO" + i.ToString()).ToString()
                            End If
                            'txt.Attributes.Add("onchange", "javascript:Count(this);")
                            txt.TextMode = TextBoxMode.MultiLine
                            txt.MaxLength = 500
                            txt.ToolTip = "Eg: Extra waste%, Recyle %, etc"
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)

                    End Select
                Next
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
                trInner.Height = 30
                tblStruct.Controls.Add(trInner)
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
            Td.CssClass = "TdHeadingNew"
            Td.Height = 20
            Td.Font.Size = 9
            Td.Font.Bold = True
            Td.HorizontalAlign = HorizontalAlign.Center

        Catch ex As Exception
            _lErrorLble.Text = "Error:HeaderTdSetting:" + ex.Message.ToString()
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
            Td.Style.Add("font-family", "Optima")
        Catch ex As Exception
            _lErrorLble.Text = "Error:InnerTdSetting:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Dim objUpIns As New UpdateInsert()
        Dim objGetData As New Selectdata
        Dim ds As New DataSet
        Dim dsData As New DataSet
        Dim Mat(9) As String
        Dim Dims(9) As String
        Dim Den(9) As String
        Dim Price(9) As String
        Dim Info(9) As String
        Dim seq As Integer = 0
        Try
            For i = 1 To 10
                Mat(i - 1) = Request.Form("txtMat" + i.ToString() + "")
                Dims(i - 1) = Request.Form("txtDim" + i.ToString() + "")
                Den(i - 1) = Request.Form("txtDen" + i.ToString() + "")
                Price(i - 1) = Request.Form("txtPrice" + i.ToString() + "")
                Info(i - 1) = Request.Form("txtInfo" + i.ToString() + "")
            Next
            If Request.QueryString("Type") = "N" Then
                If txtName.Text <> "" Then
                    ds = objGetData.GetModelValueDetails(Session("ProjectId"), "11", txtName.Text.Replace("'", "''"))
                    If ds.Tables(0).Rows.Count = 0 Then
                        objUpIns.AddStructureDetails(Session("ProjectId"), txtName.Text.Replace("'", "''"), txtSize.Text.Replace("'", "''"), Mat, Dims, Den, Price, Info)
                        seq = objUpIns.InsertProjCategoryDetails(Session("ProjectId"), "11", txtName.Text.Replace("'", "''"))

                        'Started Activity Log Changes
                        Try
                            objUpIns.InsertLog1(Session("UserId").ToString(), "StructureDetails.aspx", "Added New Structure Details for Project:" + Session("ProjectId") + "", Session("ProjectId"), Session("SPROJLogInCount").ToString())
                            Dim ht As Hashtable = New Hashtable()
                            If Session("ht") IsNot Nothing Then
                                ht = DirectCast(Session("ht"), Hashtable)
                            End If
                            Dim str() As String
                            For i = 1 To ht.Count
                                For Each item As Object In ht
                                    str = item.Key.ToString().Split("-")
                                    If str(1) = i Then
                                        objUpIns.EditInsertLog(Session("UserId").ToString(), "9", str(0), seq.ToString(), item.Value.ToString().Replace("'", "''"), Session("ProjectId"), Session("SPROJLogInCount").ToString())
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
                        PageDetails()
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Structure Name " + txtName.Text + " already exist.');", True)
                    End If
                Else
                    PageDetails()
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Structure not Added Successfully.Please Enter Structure Name.');", True)
                End If

            Else
                If txtName.Text <> "" Then
                    ds = objGetData.GetExistStructureDetails(txtName.Text.Replace("'", "''"), Request.QueryString("ProjectId").ToString(), Request.QueryString("StructureId").ToString())
                    If ds.Tables(0).Rows.Count = 0 Then
                        objUpIns.UpdateStructureDetails(Request.QueryString("StructureId").ToString(), Request.QueryString("ProjectId").ToString(), txtName.Text.Replace("'", "''"), txtSize.Text.Replace("'", "''"), Mat, Dims, Den, Price, Info)
                        objUpIns.UpdateProjCategoryDetails(Session("ProjectId"), "11", txtName.Text.Replace("'", "''"), Request.QueryString("ProjCatId").ToString())

                        dsData = objGetData.GetExistingModelDetails(Session("ProjectId"))

                        If dsData.Tables(0).Rows.Count > 0 Then
                            For i = 0 To dsData.Tables(0).Rows.Count - 1
                                If dsData.Tables(0).Rows(i).Item("STRUCTURES").ToString() = hidName.Value Then
                                    objUpIns.EditModelDetail(dsData.Tables(0).Rows(i).Item("MODELID").ToString(), Session("ProjectId"), txtName.Text.Replace("'", "''"), "STRUCT")
                                End If
                            Next
                        End If

                        'Started Activity Log Changes
                        Try
                            objUpIns.InsertLog1(Session("UserId").ToString(), "StructureDetails.aspx", "Edited Structure Details for Project:" + Session("ProjectId") + " and Structure:" + txtName.Text.Replace("'", "''") + "", Session("ProjectId"), Session("SPROJLogInCount").ToString())
                            Dim ht As Hashtable = New Hashtable()
                            If Session("ht") IsNot Nothing Then
                                ht = DirectCast(Session("ht"), Hashtable)
                            End If
                            Dim str() As String
                            For i = 1 To ht.Count
                                For Each item As Object In ht
                                    str = item.Key.ToString().Split("-")
                                    If str(1) = i Then
                                        objUpIns.EditInsertLog(Session("UserId").ToString(), "9", str(0), Request.QueryString("StructureId").ToString(), item.Value.ToString().Replace("'", "''"), Session("ProjectId"), Session("SPROJLogInCount").ToString())
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
                        PageDetails()
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Structure name Already Exist.');", True)
                    End If

                Else
                    PageDetails()
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Structure not Added Successfully.Please Enter Structure Name.');", True)
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
            objUpIns.DeleteStructureDetails(Request.QueryString("StructureId").ToString, Request.QueryString("ProjectId").ToString, hidName.Value.Replace("'", "''"))
            objUpIns.DeleteProjCategoryDetails(Session("ProjectId"), "11", txtName.Text.Replace("'", "''"), Request.QueryString("ProjCatId").ToString)
            ds = objGetData.GetExistingModelDetails(Session("ProjectId"))
            dv = ds.Tables(0).DefaultView()
            dv.RowFilter = "STRUCTURES='" + hidName.Value.Replace("'", "''") + "'"

            dt = dv.ToTable()
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    objUpIns.DeleteModelByType(Request.QueryString("ProjectId").ToString(), dt.Rows(i).Item("MODELID").ToString(), "STRUCT", hidName.Value.Replace("'", "''"))
                Next
            End If
            'Started Activity Log Changes
            Try
                objUpIns.InsertLog1(Session("UserId").ToString(), "ProducerDetails.aspx", "Deleted Structure Details for Project:" + Session("ProjectId") + "", Session("ProjectId"), Session("SPROJLogInCount").ToString())

            Catch ex As Exception

            End Try
            'Ended Activity Log Changes
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Structure deleted Successfully.');", True)
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
