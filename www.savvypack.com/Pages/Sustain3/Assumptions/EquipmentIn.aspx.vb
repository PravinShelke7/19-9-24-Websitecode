﻿Imports System.Data
Imports System.Data.OleDb
Imports System
Imports S3GetData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Sustain3_Assumptions_EquipmentIn
    Inherits System.Web.UI.Page
#Region "Get Set Variables"
    Dim _lErrorLble As Label
    Dim _strUserName As String
    Dim _strPassword As String
    Dim _iAssumptionId As Integer


    Public Property ErrorLable() As Label
        Get
            Return _lErrorLble
        End Get
        Set(ByVal Value As Label)
            _lErrorLble = Value
        End Set
    End Property

    Public Property UserName() As String
        Get
            Return _strUserName
        End Get
        Set(ByVal Value As String)
            _strUserName = Value
        End Set
    End Property

    Public Property Password() As String
        Get
            Return _strPassword
        End Get
        Set(ByVal Value As String)
            _strPassword = Value
        End Set
    End Property

    Public Property AssumptionId() As Integer
        Get
            Return _iAssumptionId
        End Get
        Set(ByVal Value As Integer)
            _iAssumptionId = Value
        End Set
    End Property


    Public DataCnt As Integer
    Public CaseDesp As New ArrayList

#End Region

#Region "MastePage Content Variables"
    Protected Sub GetErrorLable()
        ErrorLable = Page.Master.FindControl("lblError")
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Try
                GetErrorLable()
                GetSessionDetails()
                GetPageDetails()
            Catch ex As Exception

            End Try

        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetSessionDetails()
        Try
            UserName = Session("UserName")
            Password = Session("Password")
            AssumptionId = Session("AssumptionId")

            lblAID.Text = AssumptionId
            lblAdes.Text = Session("Description")

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetSessionDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Function GetCaseIds() As String()
        Dim CaseIds(0) As String
        Dim objGetData As New S3GetData.Selectdata

        Try
            CaseIds = objGetData.Cases1(AssumptionId)

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetCaseIds:" + ex.Message.ToString()
        End Try
        Return CaseIds
    End Function

    Protected Sub GetPageDetails()
        Dim ds As New DataSet
        Dim dstbl As New DataSet
        Dim dsCaseDetails As New DataSet
        Dim objGetData As New S1GetData.Selectdata
        Dim CaseIds As String = String.Empty
        'Dim DataCnt As New Integer
        Dim i As New Integer
        Dim j As New Integer
        Dim k As New Integer
        Dim DWidth As String = String.Empty
        Dim arrCaseID() As String

        Dim dsEquip As New DataSet
        Dim dvEquip As New DataView
        Dim dtEquip As New DataTable

        Dim dsDept As New DataSet
        Dim dvDept As New DataView
        Dim dtDept As New DataTable
        Dim lbl As Label

        Try

            dsEquip = objGetData.GetEquipment("-1", "", "")
            dvEquip = dsEquip.Tables(0).DefaultView()

            dsDept = objGetData.GetDeptN("-1", "", "")
            dvDept = dsDept.Tables(0).DefaultView()

            arrCaseID = GetCaseIds()
            'ds = objGetData.EquipmentIn(CaseIds, UserName)
            DataCnt = arrCaseID.Length - 1


            Dim trHeader As New TableRow
            Dim tdHeader As TableCell
            Dim tdInner As TableCell
            Dim trInner As New TableRow
            Dim ddl As DropDownList
            Dim Cunits As New Integer
            Dim Units As New Integer
            Dim Title As String = String.Empty
            DWidth = txtDWidth.Text + "px"


            tdHeader = New TableCell
            HeaderTdSetting(tdHeader, DWidth, "<img alt='' src='../../Images/spacer.gif' Style='width:200px;height:0px;'  />", 1)
            trHeader.Controls.Add(tdHeader)
            trHeader.Height = 20
            trHeader.CssClass = "PageSSHeading"

            For i = 0 To DataCnt
                'Dim ds As New DataSet
                ds = objGetData.GetEquipmentInDetails(arrCaseID(i))
                ds.Tables(0).TableName = arrCaseID(i).ToString()
                dstbl.Tables.Add(ds.Tables(arrCaseID(i).ToString()).Copy())
            Next





            For i = 0 To DataCnt
                dsCaseDetails = objGetData.GetCaseDetails(arrCaseID(i).ToString())
                Cunits = Convert.ToInt32(dstbl.Tables(0).Rows(0).Item("Units").ToString())
                Units = Convert.ToInt32(dstbl.Tables(i).Rows(0).Item("Units").ToString())
                tdHeader = New TableCell


                Dim Headertext As String = String.Empty
                If Cunits <> Units Then
                    Headertext = "Case#:" + arrCaseID(i).ToString() + "<br/>" + dsCaseDetails.Tables(0).Rows(0).Item("CaseDes").ToString() + "<br/> <span  style='color:red'>Unit Mismatch</span>" + "<input type='hidden' value='" + arrCaseID(i).ToString() + "' name='Case" + i.ToString() + "'/>"
                Else
                    Headertext = "Case#:" + arrCaseID(i).ToString() + "<br/>" + dsCaseDetails.Tables(0).Rows(0).Item("CaseDes").ToString() + "<input type='hidden' value='" + arrCaseID(i).ToString() + "' name='Case" + i.ToString() + "'/>"
                End If

                CaseDesp.Add(arrCaseID(i).ToString())
                HeaderTdSetting(tdHeader, DWidth, Headertext, 1)
                trHeader.Controls.Add(tdHeader)


            Next
            tblComparision.Controls.Add(trHeader)


            For i = 1 To 30
                For j = 1 To 12
                    trInner = New TableRow

                    Select Case j

                        Case 1

                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "<b>Asset " + i.ToString() + "</b>", trInner, "AlterNateColor4")
                            For k = 0 To DataCnt
                                'Break
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Center")
                                tdInner.Text = "&nbsp;"
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 2

                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Asset Description", trInner, "AlterNateColor1")
                            trInner.ID = "AD_" + i.ToString()
                            For k = 0 To DataCnt
                                ''Asset
                                'Dim dsMat As New DataSet
                                'tdInner = New TableCell
                                ''ddl = New DropDownList
                                'dsMat = objGetData.GetEquipment(dstbl.Tables(k).Rows(0).Item("M" + i.ToString() + "").ToString(), "", "")
                                'tdInner.Text = dsMat.Tables(0).Rows(0).Item("equipDES").ToString()
                                ''tdInner.Text = ddl.SelectedItem.Text
                                'InnerTdSetting(tdInner, DWidth, "Right")
                                ''tdInner.Controls.Add(ddl)
                                'trInner.Controls.Add(tdInner)

                                ''Asset
                                'tdInner = New TableCell
                                'dvEquip.RowFilter = "equipID=" + dstbl.Tables(k).Rows(0).Item("M" + i.ToString() + "").ToString()
                                'dtEquip = dvEquip.ToTable()
                                'tdInner.Text = dtEquip.Rows(0).Item("equipDES").ToString()
                                'InnerTdSetting(tdInner, DWidth, "Right")
                                'trInner.Controls.Add(tdInner)
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                GetEquipmentInDetails(lbl, dstbl.Tables(k).Rows(0).Item("M" + i.ToString() + "").ToString(), dsEquip, arrCaseID(k))
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)

                            Next


                        Case 3

                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Area Type", trInner, "AlterNateColor2")
                            trInner.ID = "AT_" + i.ToString()
                            For k = 0 To DataCnt

                                'Area Type
                                tdInner = New TableCell
                                tdInner.Text = dstbl.Tables(k).Rows(0).Item("AREATYPE" + i.ToString() + "").ToString()
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)
                            Next



                        Case 4

                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("Title7").ToString() + ")"
                            LeftTdSetting(tdInner, "Plant Area Sugg. " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "PAS_" + i.ToString()
                            For k = 0 To DataCnt
                                'Plant Area Sugg.
                                tdInner = New TableCell
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("PLANTAREAS" + i.ToString() + "").ToString(), 0)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)

                            Next


                        Case 5

                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("Title7").ToString() + ")"
                            LeftTdSetting(tdInner, "Plant Area Pref. " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "PAP_" + i.ToString()
                            For k = 0 To DataCnt
                                'Plant Area Pref.
                                tdInner = New TableCell
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("PLANTAREAP" + i.ToString() + "").ToString(), 0)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                'tdInner.Controls.Add(txt)
                                trInner.Controls.Add(tdInner)
                            Next


                        Case 6

                            tdInner = New TableCell
                            Title = "(kw)"
                            LeftTdSetting(tdInner, "Electricity Consumption Sugg. " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "ECS_" + i.ToString()
                            For k = 0 To DataCnt
                                'Electricity Consumption Sugg.
                                tdInner = New TableCell
                                tdInner.Text = dstbl.Tables(k).Rows(0).Item("ERGYS" + i.ToString() + "").ToString()
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)

                            Next


                        Case 7


                            tdInner = New TableCell
                            Title = "(kw)"
                            LeftTdSetting(tdInner, "Electricity Consumption Pref. " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "ECP_" + i.ToString()
                            For k = 0 To DataCnt
                                'Electricity Consumption Pref.
                                tdInner = New TableCell
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("ERGYP" + i.ToString() + "").ToString(), 0)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                'tdInner.Controls.Add(txt)
                                trInner.Controls.Add(tdInner)
                            Next


                        Case 8

                            tdInner = New TableCell
                            Title = "(cubic ft/hr)"
                            LeftTdSetting(tdInner, "Process Natural Gas Consumption Sugg. " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "NCS_" + i.ToString()
                            For k = 0 To DataCnt
                                'Process Natural Gas Consumption Sugg.
                                tdInner = New TableCell
                                tdInner.Text = dstbl.Tables(k).Rows(0).Item("NTGS" + i.ToString() + "").ToString()
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)
                            Next


                        Case 9

                            tdInner = New TableCell
                            Title = "(cubic ft/hr)"
                            LeftTdSetting(tdInner, "Process Natural Gas Consumption Pref. " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "NCP_" + i.ToString()
                            For k = 0 To DataCnt
                                'Process Natural Gas Consumption Pref.
                                tdInner = New TableCell
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("NTGP" + i.ToString() + "").ToString(), 0)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                'tdInner.Controls.Add(txt)
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 10

                            tdInner = New TableCell
                            Title = "(gallon/hr)"
                            LeftTdSetting(tdInner, "Water Consumption Sugg. " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "WaterS_" + i.ToString()
                            For k = 0 To DataCnt
                                'Process Natural Gas Consumption Sugg.
                                tdInner = New TableCell
                                tdInner.Text = dstbl.Tables(k).Rows(0).Item("WATERS" + i.ToString() + "").ToString()
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)
                            Next


                        Case 11

                            tdInner = New TableCell
                            Title = "(gallon/hr)"
                            LeftTdSetting(tdInner, "Water Consumption Pref. " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "WaterP_" + i.ToString()
                            For k = 0 To DataCnt
                                'Process Natural Gas Consumption Pref.
                                tdInner = New TableCell
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("WATERP" + i.ToString() + "").ToString(), 0)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                'tdInner.Controls.Add(txt)
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 12

                            tdInner = New TableCell
                            Dim dsMat As New DataSet
                            LeftTdSetting(tdInner, "Mfg. Dept.", trInner, "AlterNateColor1")
                            trInner.ID = "MD_" + i.ToString()
                            For k = 0 To DataCnt
                                'Mfg Dept.

                                'tdInner = New TableCell
                                ''ddl = New DropDownList
                                'dsMat = objGetData.GetDept(dstbl.Tables(k).Rows(0).Item("DEPT" + i.ToString() + "").ToString(), "", "", arrCaseID(k))
                                'If dsMat.Tables(0).Rows.Count > 0 Then
                                '    tdInner.Text = dsMat.Tables(0).Rows(0).Item("PROCDE").ToString()
                                'Else
                                '    tdInner.Text = "Dept. Conflict"
                                'End If

                                'InnerTdSetting(tdInner, DWidth, "Right")
                                ''tdInner.Controls.Add(ddl)
                                'trInner.Controls.Add(tdInner)


                                tdInner = New TableCell
                                dvDept.RowFilter = "PROCID=" + dstbl.Tables(k).Rows(0).Item("DEPT" + i.ToString() + "").ToString()
                                dtDept = dvDept.ToTable()

                                If dtDept.Rows.Count > 0 Then
                                    tdInner.Text = dtDept.Rows(0).Item("PROCDE").ToString()
                                Else
                                    tdInner.Text = "Dept. Conflict"
                                End If

                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)
                            Next

                    End Select

                    tblComparision.Controls.Add(trInner)
                Next
            Next
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub GetEquipmentInDetails(ByRef lbl As Label, ByVal EqId As Integer, ByVal dsEquip As DataSet, ByVal CaseId As String)
        Dim Ds As New DataSet
        Dim ObjGetdata As New S1GetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty
        Dim dvEquip As New DataView
        Dim dtEquip As New DataTable

        Dim dsEEquip As New DataSet
        Dim str As String = ""
        Dim dv As New DataView
        Dim dt As New DataTable
        Dim dvMat As New DataView
        Dim dtMat As New DataTable

        Try
            'Ds = ObjGetdata.GetEquipment(EqId, "", "")
            dsEEquip = ObjGetdata.GetEditEquip(CaseId)
            dv = dsEEquip.Tables(0).DefaultView
            If EqId <> 0 Then

                dv.RowFilter = "EQUIPID=" + EqId.ToString()
                dt = dv.ToTable()
                If dt.Rows.Count > 0 Then
                    lbl.Text = dt.Rows(0).Item("equipDES").ToString().Replace("&#", "'")
                    lbl.ToolTip = dt.Rows(0).Item("equipDES").ToString().Replace("&#", "'")
                Else
                    dvEquip = dsEquip.Tables(0).DefaultView
                    dvEquip.RowFilter = "EQUIPID = " + EqId.ToString()
                    dtEquip = dvEquip.ToTable()
                    lbl.Text = dtEquip.Rows(0).Item("equipDES").ToString()
                End If
            Else
                dvEquip = dsEquip.Tables(0).DefaultView
                dvEquip.RowFilter = "EQUIPID = " + EqId.ToString()
                dtEquip = dvEquip.ToTable()
                lbl.Text = dtEquip.Rows(0).Item("equipDES").ToString()
            End If
        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
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
            Td.Height = 30
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

    Protected Sub TextBoxSetting(ByVal txt As TextBox, ByVal Css As String)
        Try


            txt.CssClass = Css
            txt.Enabled = False
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub LeftTdSetting(ByVal Td As TableCell, ByVal Text As String, ByVal tr As TableRow, ByVal Css As String)
        Try
            Td.Text = Text
            InnerTdSetting(Td, "", "Left")
            tr.Controls.Add(Td)
            tr.CssClass = Css




        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub

    'Protected Function GetDepartment() As DropDownList
    '    Dim ddl As New DropDownList
    '    Dim ds As New DataSet()
    '    Dim objGetData As New S1GetData.Selectdata
    '    Try
    '        ds = objGetData.GetDepartment
    '        With ddl
    '            .DataSource = ds
    '            .DataTextField = "PROCDE"
    '            .DataValueField = "PROCID"
    '            .DataBind()
    '            .CssClass = "DropDown"
    '            .Width = 150
    '            .Enabled = False

    '        End With


    '    Catch ex As Exception
    '        _lErrorLble.Text = "Error:GetMateralCombo:" + ex.Message.ToString()
    '    End Try
    '    Return ddl
    'End Function

    'Protected Function GetEquipment() As DropDownList
    '    Dim ddl As New DropDownList
    '    Dim ds As New DataSet()
    '    Dim objGetData As New S1GetData.Selectdata
    '    Try
    '        ds = objGetData.GetEquipments()
    '        With ddl
    '            .DataSource = ds
    '            .DataTextField = "EQUIPDES"
    '            .DataValueField = "EQUIPID"
    '            .DataBind()
    '            .CssClass = "DropDown"
    '            .Width = 150
    '            .Enabled = False

    '        End With


    '    Catch ex As Exception
    '        _lErrorLble.Text = "Error:GetMateralCombo:" + ex.Message.ToString()
    '    End Try
    '    Return ddl
    'End Function

End Class
