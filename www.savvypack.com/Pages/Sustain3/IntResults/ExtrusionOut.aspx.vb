Imports System.Data
Imports System.Data.OleDb
Imports System
Imports S3GetData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Sustain3_IntResults_ExtrusionOut
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
        Dim CaseIds(0)
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
        Dim dsCaseDetails As New DataSet
        Dim dstbl As New DataSet
        Dim objGetData As New S1GetData.Selectdata
        Dim CaseIds As String = String.Empty
        'Dim DataCnt As New Integer
        Dim i As New Integer
        Dim j As New Integer
        Dim k As New Integer
        Dim DWidth As String = String.Empty
        Dim arrCaseID() As String
        Dim dv As New DataView
        Dim dt As New DataTable
        Dim dsEmattbl As New DataSet

        Try
            arrCaseID = GetCaseIds()
            'ds = objGetData.ExtrusionOut(CaseIds, UserName)
            DataCnt = arrCaseID.Length - 1

            Dim trHeader As New TableRow
            Dim tdHeader As TableCell
            Dim tdInner As TableCell
            Dim Cunits As New Integer
            Dim Units As New Integer
            Dim Title As String = String.Empty
            Dim lbl As New Label
            DWidth = txtDWidth.Text + "px"


            tdHeader = New TableCell
            HeaderTdSetting(tdHeader, DWidth, "<img alt='' src='../../Images/spacer.gif' Style='width:200px;height:0px;'  />", 1)
            trHeader.Controls.Add(tdHeader)
            trHeader.Height = 20
            trHeader.CssClass = "PageSSHeading"

            For i = 0 To DataCnt
                ds = objGetData.GetExtrusionOutDetails(arrCaseID(i))
                ds.Tables(0).TableName = arrCaseID(i).ToString()
                dstbl.Tables.Add(ds.Tables(arrCaseID(i).ToString()).Copy())

                Dim dsEmat As New DataSet
                dsEmat = objGetData.GetEditMaterial(arrCaseID(i))
                dsEmat.Tables(0).TableName = arrCaseID(i).ToString()
                dsEmattbl.Tables.Add(dsEmat.Tables(arrCaseID(i).ToString()).Copy())
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

            For i = 1 To 10
                For j = 1 To 12
                    Dim trInner As New TableRow

                    tdInner = New TableCell

                    trInner = New TableRow()

                    Select Case j

                        Case 1
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "<b>Layer" + i.ToString() + "</b>", trInner, "AlterNateColor4")
                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Center")
                                tdInner.Text = "&nbsp;"
                                'trBreak.Controls.Add(tdInner)
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 2

                            tdInner = New TableCell
                            Title = "(kg)"
                            LeftTdSetting(tdInner, "Material " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "M_" + i.ToString()
                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                dv = dsEmattbl.Tables(k).DefaultView
                                If dstbl.Tables(k).Rows(0).Item("M" + i.ToString() + "").ToString() <> "0" Then
                                    dv.RowFilter = "MATID=" + dstbl.Tables(k).Rows(0).Item("M" + i.ToString() + "").ToString()
                                    dt = dv.ToTable()
                                    If dt.Rows.Count > 0 Then
                                        lbl.Text = dt.Rows(0).Item("MATDES").ToString().Replace("&#", "'")
                                    Else
                                        'lbl.Text = dstbl.Tables(k).Rows(0).Item("MATS" + i.ToString() + "").ToString()
                                        GetMaterialDetails(lbl, dstbl.Tables(k).Rows(0).Item("M" + i.ToString() + "").ToString())
                                    End If

                                Else
                                    'lbl.Text = dsEmattbl.Tables(k).Rows(0).Item("MATS" + i.ToString() + "").ToString()
                                    GetMaterialDetails(lbl, dstbl.Tables(k).Rows(0).Item("M" + i.ToString() + "").ToString())
                                End If
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next


                        Case 3

                            tdInner = New TableCell
                            Title = "(Unitless)"
                            LeftTdSetting(tdInner, "Specific Gravity " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "SG_" + i.ToString()
                            For k = 0 To DataCnt
                                'Specific Gravity
                                tdInner = New TableCell
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("SG" + i.ToString() + "").ToString(), 2)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)
                            Next


                        Case 4
                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("Title8").ToString() + "/" + dstbl.Tables(0).Rows(0).Item("Title3").ToString() + ")"
                            LeftTdSetting(tdInner, "Weight/Area " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "WPA_" + i.ToString()
                            For k = 0 To DataCnt
                                'Weight Per Area
                                tdInner = New TableCell
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("WT" + i.ToString() + "").ToString(), 3)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)

                            Next


                        Case 5

                            tdInner = New TableCell
                            Title = "(%)"
                            LeftTdSetting(tdInner, "Weight " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "WT_" + i.ToString()
                            For k = 0 To DataCnt

                                'Weight
                                tdInner = New TableCell
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("P" + i.ToString() + "").ToString(), 1)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)
                            Next


                        Case 6


                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("Title8").ToString() + ")"
                            LeftTdSetting(tdInner, "Purchases " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "PUR_" + i.ToString()
                            For k = 0 To DataCnt
                                'Purchase
                                tdInner = New TableCell
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("PUR" + i.ToString() + "").ToString(), 0)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)
                            Next



                        Case 7

                            tdInner = New TableCell
                            Title = "(MJ)"
                            LeftTdSetting(tdInner, "Energy " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "ERGY_" + i.ToString()
                            For k = 0 To DataCnt
                                'Energy
                                tdInner = New TableCell
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("EN" + i.ToString() + "").ToString(), 0)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 8

                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("Title8").ToString() + ")"
                            LeftTdSetting(tdInner, "CO2 Eqivalents " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "GHGE_" + i.ToString()
                            For k = 0 To DataCnt
                                'Co2 Equivalent
                                tdInner = New TableCell
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("PURZ" + i.ToString() + "").ToString(), 0)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 9

                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("Title10").ToString() + ")"
                            LeftTdSetting(tdInner, "Water " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "Water_" + i.ToString()
                            For k = 0 To DataCnt
                                'Co2 Equivalent
                                tdInner = New TableCell
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("PURW" + i.ToString() + "").ToString(), 0)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 10

                            tdInner = New TableCell
                            Title = "(MJ)"
                            LeftTdSetting(tdInner, "Incineration Energy " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "INERGY_" + i.ToString()
                            For k = 0 To DataCnt
                                'Incineration Energy
                                tdInner = New TableCell
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("INEN" + i.ToString() + "").ToString(), 0)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)
                            Next


                        Case 11

                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("Title8").ToString() + ")"
                            LeftTdSetting(tdInner, "Incineration GHG " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "INGHG_" + i.ToString()
                            For k = 0 To DataCnt
                                'Incineration GHG
                                tdInner = New TableCell
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("INGHG" + i.ToString() + "").ToString(), 0)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)

                            Next


                        Case 12

                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("Title8").ToString() + ")"
                            LeftTdSetting(tdInner, "Compost GHG " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "CMGHG_" + i.ToString()
                            For k = 0 To DataCnt
                                'Compost GHG
                                tdInner = New TableCell
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("CMGHG" + i.ToString() + "").ToString(), 0)
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

    Protected Sub GetMaterialDetails(ByRef lbl As Label, ByVal MatId As Integer)
        Dim Ds As New DataSet
        Dim ObjGetdata As New S1GetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty

        Try

            Ds = ObjGetdata.GetMaterials(MatId, "", "")
            If Ds.Tables(0).Rows(0).Item("MATDES").ToString().Length > 25 Then
                'LinkMat.Font.Size = 8
            End If
            lbl.Text = Ds.Tables(0).Rows(0).Item("MATDES").ToString()
            lbl.ToolTip = Ds.Tables(0).Rows(0).Item("MATDES").ToString()


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
End Class
