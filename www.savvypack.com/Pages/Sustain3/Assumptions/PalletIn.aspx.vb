Imports System.Data
Imports System.Data.OleDb
Imports System
Imports S3GetData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Sustain3_Assumptions_PalletIn
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
        Dim k As Integer
        Dim DWidth As String = String.Empty
        Dim arrCaseID() As String


        Try
            arrCaseID = GetCaseIds()
            'ds = objGetData.PalletIn(CaseIds, UserName)
            DataCnt = arrCaseID.Length - 1


            Dim trHeader As New TableRow
            Dim tdHeader As TableCell
            Dim tdInner As TableCell
            Dim ddl As DropDownList
            Dim trInner As New TableRow
            Dim txt As TextBox
            Dim Cunits As New Integer
            Dim Units As New Integer
            Dim Title As String = String.Empty
            DWidth = txtDWidth.Text + "px"


            tdHeader = New TableCell
            HeaderTdSetting(tdHeader, DWidth, "<img alt='' src='../../Images/spacer.gif' Style='width:200px;height:0px;' />", 1)
            trHeader.Controls.Add(tdHeader)
            trHeader.Height = 20
            trHeader.CssClass = "PageSSHeading"


            For i = 0 To DataCnt

                ds = objGetData.GetPalletInDetails(arrCaseID(i))
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

            For i = 1 To 10

                For j = 1 To 21
                    trInner = New TableRow()

                    Select Case j
                        Case 1

                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "<b>Layer" + i.ToString() + "</b>", trInner, "AlterNateColor4")
                            For k = 0 To DataCnt
                                'Break
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Center")
                                tdInner.Text = "&nbsp;"
                                trInner.Controls.Add(tdInner)
                            Next



                        Case 2

                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Item", trInner, "AlterNateColor1")
                            trInner.ID = "IT_" + i.ToString()
                            For k = 0 To DataCnt
                                'Pallet Items
                                Dim dsMat As New DataSet
                                tdInner = New TableCell
                                'ddl = New DropDownList
                                dsMat = objGetData.GetPallets(dstbl.Tables(k).Rows(0).Item("M" + i.ToString() + "").ToString(), "", "")
                                tdInner.Text = dsMat.Tables(0).Rows(0).Item("PALLETDES").ToString()
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)
                            Next


                        Case 3

                            tdInner = New TableCell
                            Title = "(per pallet)"
                            LeftTdSetting(tdInner, "Number " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "N_" + i.ToString()
                            For k = 0 To DataCnt
                                'Number Of Uses
                                tdInner = New TableCell
                                'txt = New TextBox
                                'txt.Text = FormatNumber(ds.Tables(0).Rows(i).Item("R" + j.ToString() + "").ToString(), 3)
                                'TextBoxSetting(txt, "NormalTextBox")
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("NUM" + i.ToString() + "").ToString(), 2)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                'tdInner.Controls.Add(txt)
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 4


                            tdInner = New TableCell
                            Title = "(Units)"
                            LeftTdSetting(tdInner, "Number Of Uses " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "NU_" + i.ToString()
                            For k = 0 To DataCnt
                                'Number
                                tdInner = New TableCell
                                ' txt = New TextBox
                                'txt.Text = FormatNumber(ds.Tables(0).Rows(i).Item("THICK" + j.ToString() + "").ToString(), 3)
                                'TextBoxSetting(txt, "NormalTextBox")
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("NOUSE" + i.ToString() + "").ToString(), 2)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                'tdInner.Controls.Add(txt)
                                trInner.Controls.Add(tdInner)
                            Next


                        Case 5

                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("Title8").ToString() + ")"
                            LeftTdSetting(tdInner, "Weight Each Sugg. " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "WTS_" + i.ToString()
                            For k = 0 To DataCnt
                                'Weight Each Sugg
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, DWidth, "Right")
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("WS" + i.ToString() + "").ToString(), 2)
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 6
                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("Title8").ToString() + ")"
                            LeftTdSetting(tdInner, "Weight Each Pref. " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "WTP_" + i.ToString()
                            For k = 0 To DataCnt
                                'Weight Each Preff
                                tdInner = New TableCell
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("WP" + i.ToString() + "").ToString(), 2)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                'tdInner.Controls.Add(txt)
                                trInner.Controls.Add(tdInner)

                            Next


                        Case 7
                            tdInner = New TableCell
                            Title = "(MJ/" + dstbl.Tables(0).Rows(0).Item("Title8").ToString() + " mat)"
                            LeftTdSetting(tdInner, "Energy Sugg. " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "ES_" + i.ToString()
                            For k = 0 To DataCnt
                                'Energy Sugg
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, DWidth, "Right")
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("ERGYS" + i.ToString() + "").ToString(), 1)
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 8
                            tdInner = New TableCell
                            Title = "(MJ/" + dstbl.Tables(0).Rows(0).Item("Title8").ToString() + " mat)"
                            LeftTdSetting(tdInner, "Energy Pref. " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "EP_" + i.ToString()
                            For k = 0 To DataCnt
                                'Energy Preff
                                tdInner = New TableCell
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("ERGYP" + i.ToString() + "").ToString(), )
                                InnerTdSetting(tdInner, DWidth, "Right")
                                'tdInner.Controls.Add(txt)
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 9
                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("Title8").ToString() + " CO2/" + dstbl.Tables(0).Rows(0).Item("Title8").ToString() + " mat)"
                            LeftTdSetting(tdInner, "CO2 Equivalent Sugg. " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "GHGS_" + i.ToString()
                            For k = 0 To DataCnt
                                'GHG Sugg
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, DWidth, "Right")
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("GHGS" + i.ToString() + "").ToString(), 2)
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 10
                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("Title8").ToString() + " CO2/" + dstbl.Tables(0).Rows(0).Item("Title8").ToString() + " mat)"
                            LeftTdSetting(tdInner, "CO2 Equivalent Pref. " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "GHGP_" + i.ToString()
                            For k = 0 To DataCnt
                                'GHG Preff
                                tdInner = New TableCell
                                'txt = New TextBox
                                'txt.Text = FormatNumber(ds.Tables(0).Rows(i).Item("CO2P" + j.ToString() + "").ToString(), 3)
                                'TextBoxSetting(txt, "NormalTextBox")
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("GHGP" + i.ToString() + "").ToString(), 2)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                'tdInner.Controls.Add(txt)
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 11
                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("Title10").ToString() + " Water/" + dstbl.Tables(0).Rows(0).Item("Title8").ToString() + " mat)"
                            LeftTdSetting(tdInner, "Water Sugg. " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "WATERS_" + i.ToString()
                            For k = 0 To DataCnt
                                'GHG Sugg
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, DWidth, "Right")
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("WATERS" + i.ToString() + "").ToString(), 2)
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 12
                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("Title10").ToString() + " Water/" + dstbl.Tables(0).Rows(0).Item("Title8").ToString() + " mat)"
                            LeftTdSetting(tdInner, "Water Pref. " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "WATERP_" + i.ToString()
                            For k = 0 To DataCnt
                                'GHG Preff
                                tdInner = New TableCell
                                'txt = New TextBox
                                'txt.Text = FormatNumber(ds.Tables(0).Rows(i).Item("CO2P" + j.ToString() + "").ToString(), 3)
                                'TextBoxSetting(txt, "NormalTextBox")
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("WATERP" + i.ToString() + "").ToString(), 2)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                'tdInner.Controls.Add(txt)
                                trInner.Controls.Add(tdInner)
                            Next


                        Case 13

                            tdInner = New TableCell
                            Title = "(Unitless)"
                            LeftTdSetting(tdInner, "Recovery Sugg. " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "RECS_" + i.ToString()
                            For k = 0 To DataCnt
                                'Recovery Sugg
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, DWidth, "Right")
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("RECS" + i.ToString() + "").ToString(), 2)
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 14

                            tdInner = New TableCell
                            Title = "(Unitless)"
                            LeftTdSetting(tdInner, "Recovery Pref. " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "RECP_" + i.ToString()

                            For k = 0 To DataCnt
                                'Recovery Preff
                                tdInner = New TableCell
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("RECP" + i.ToString() + "").ToString(), 2)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                'tdInner.Controls.Add(txt)
                                trInner.Controls.Add(tdInner)
                            Next



                        Case 15

                            tdInner = New TableCell
                            Title = "(Unitless)"
                            LeftTdSetting(tdInner, "Sustainable Materials Sugg. " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "SUS_" + i.ToString()
                            For k = 0 To DataCnt
                                'Sustainable material Sugg
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, DWidth, "Right")
                                tdInner.Text = dstbl.Tables(k).Rows(0).Item("SUSMATS" + i.ToString() + "").ToString()
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 16
                            tdInner = New TableCell
                            Title = "(Unitless)"
                            LeftTdSetting(tdInner, "Sustainable Materials Pref. " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "SUP_" + i.ToString()

                            For k = 0 To DataCnt
                                'Sustainable material Preff
                                tdInner = New TableCell
                                If dstbl.Tables(k).Rows(0).Item("SUSMATP" + i.ToString() + "").ToString() <> "" Then
                                    tdInner.Text = dstbl.Tables(k).Rows(0).Item("SUSMATP" + i.ToString() + "").ToString()
                                Else
                                    tdInner.Text = "Nothing"
                                End If
                                ' tdInner.Text = dstbl.Tables(k).Rows(0).Item("SUSMATP" + i.ToString() + "").ToString()

                                InnerTdSetting(tdInner, DWidth, "Right")
                                'tdInner.Controls.Add(ddl)
                                trInner.Controls.Add(tdInner)

                            Next
                        Case 17

                            tdInner = New TableCell
                            Title = "(Unitless)"
                            LeftTdSetting(tdInner, "PC Recycle Sugg. " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "PCS_" + i.ToString()
                            For k = 0 To DataCnt
                                'PC Recycle Sugg
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, DWidth, "Right")
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("PCRECS" + i.ToString() + "").ToString(), 2)
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 18
                            tdInner = New TableCell
                            Title = "(Unitless)"
                            LeftTdSetting(tdInner, "PC Recycle Pref. " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "PCP_" + i.ToString()

                            For k = 0 To DataCnt
                                'PC Recycle Preff
                                tdInner = New TableCell
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("PCRECP" + i.ToString() + "").ToString(), 2)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                'tdInner.Controls.Add(txt)
                                trInner.Controls.Add(tdInner)


                            Next
                        Case 19
                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("Title4").ToString() + ")"
                            LeftTdSetting(tdInner, "Ship Sugg. " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "SS_" + i.ToString()
                            For k = 0 To DataCnt
                                'Ship Distance Sugg
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, DWidth, "Right")
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("SDS" + i.ToString() + "").ToString(), 0)
                                trInner.Controls.Add(tdInner)

                            Next
                        Case 20
                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("Title4").ToString() + ")"
                            LeftTdSetting(tdInner, "Ship Pref. " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "SP_" + i.ToString()
                            For k = 0 To DataCnt
                                'Ship Distance Preff
                                tdInner = New TableCell
                                'txt = New TextBox
                                'txt.Text = FormatNumber(ds.Tables(0).Rows(i).Item("SHIPP" + j.ToString() + "").ToString(), 3)
                                'TextBoxSetting(txt, "NormalTextBox")
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("SDP" + i.ToString() + "").ToString(), 2)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                'tdInner.Controls.Add(txt)
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 21

                            tdInner = New TableCell
                            Dim dsMat As New DataSet
                            Title = ""
                            'LeftTdSetting(tdInner, "Mfg. Department" + Title + "", trDept, "AlterNateColor1")
                            LeftTdSetting(tdInner, "Mfg. Department" + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "D_" + i.ToString()

                            For k = 0 To DataCnt
                                'Mfg. Department
                                tdInner = New TableCell
                                dsMat = objGetData.GetDept(dstbl.Tables(k).Rows(0).Item("D" + i.ToString() + "").ToString(), "", "", arrCaseID(k))
                                If dsMat.Tables(0).Rows.Count > 0 Then
                                    tdInner.Text = dsMat.Tables(0).Rows(0).Item("PROCDE").ToString()
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

    'Protected Function GetPalletCombo(ByVal InventoryType As String, ByVal Effdate As String) As DropDownList
    '    Dim ddl As New DropDownList
    '    Dim ds As New DataSet()
    '    Dim objGetData As New S1GetData.Selectdata
    '    Try
    '        ds = objGetData.GetPalletItems(InventoryType, Effdate)
    '        With ddl
    '            .DataSource = ds
    '            .DataTextField = "PalletDes"
    '            .DataValueField = "PalletId"
    '            .DataBind()
    '            .CssClass = "DropDown"
    '            .Width = 300
    '            .Enabled = False
    '        End With


    '    Catch ex As Exception
    '        _lErrorLble.Text = "Error:GetMateralCombo:" + ex.Message.ToString()
    '    End Try
    '    Return ddl
    'End Function

    'Protected Function GetSustainMaterials() As DropDownList
    '    Dim ddl As New DropDownList
    '    Dim ds As New DataSet()
    '    Dim objGetData As New S1GetData.Selectdata
    '    Try
    '        ds = objGetData.GetSustainMaterials()
    '        With ddl
    '            .DataSource = ds
    '            .DataTextField = "TYPE"
    '            .DataValueField = "TYPEID"
    '            .DataBind()
    '            .CssClass = "DropDown"
    '            .Width = 110
    '            .Enabled = False
    '        End With


    '    Catch ex As Exception
    '        _lErrorLble.Text = "Error:GetMateralCombo:" + ex.Message.ToString()
    '    End Try
    '    Return ddl
    'End Function

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


End Class
