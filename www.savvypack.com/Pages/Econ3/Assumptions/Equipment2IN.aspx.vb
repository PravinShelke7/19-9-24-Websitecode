Imports System.Data
Imports System.Data.OleDb
Imports System
Imports E3GetData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Econ3_Assumptions_Equipment2IN
    Inherits System.Web.UI.Page
    Shared uName As String
#Region "Get Set Variables"
    Dim _lErrorLble As Label
    Dim _strUserName As String
    Dim _strPassword As String
    Dim _iAssumptionId As Integer
    Dim _btnUpdate As ImageButton
    Dim _ctlContentPlaceHolder As ContentPlaceHolder



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

    Public Property Updatebtn() As ImageButton
        Get
            Return _btnUpdate
        End Get
        Set(ByVal value As ImageButton)
            _btnUpdate = value
        End Set
    End Property

    Public Property ctlContentPlaceHolder() As ContentPlaceHolder
        Get
            Return _ctlContentPlaceHolder
        End Get
        Set(ByVal value As ContentPlaceHolder)
            _ctlContentPlaceHolder = value
        End Set
    End Property

    Public DataCnt As Integer
    Public CaseDesp As New ArrayList

#End Region

#Region "MastePage Content Variables"
    Protected Sub GetErrorLable()
        ErrorLable = Page.Master.FindControl("lblError")
    End Sub

    Protected Sub GetUpdatebtn()
        Updatebtn = Page.Master.FindControl("imgUpdate")
        Updatebtn.Visible = Visible
    End Sub

    Protected Sub GetContentPlaceHolder()
        ctlContentPlaceHolder = Page.Master.FindControl("Econ3ContentPlaceHolder")
    End Sub
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Try
                GetContentPlaceHolder()
                GetErrorLable()
                GetUpdatebtn()
                GetSessionDetails()
                GetPageDetails()
                If Not IsPostBack Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Layers", "removeSession();", True)
                End If
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
            AssumptionId = Session("AssumptionID")
            uName = Session("UserName")

            lblAID.Text = AssumptionId
            lblAdes.Text = Session("Description")

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetSessionDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Function GetCaseIds() As String()
        Dim CaseIds(0) As String
        Dim objGetData As New E3GetData.Selectdata
        Try
            CaseIds = objGetData.Cases(AssumptionId)
            Return CaseIds
        Catch ex As Exception
            Return CaseIds
            _lErrorLble.Text = "Error:GetCaseIds:" + ex.Message.ToString()
        End Try

    End Function

    Protected Sub GetPageDetails()
        Dim dstbl As New DataSet
        Dim dsCaseDetails As New DataSet
        Dim objGetData As New E1GetData.Selectdata
        Dim CaseIds As String = String.Empty
        'Dim DataCnt As New Integer
        Dim i As New Integer
        Dim j As New Integer
        Dim k As New Integer
        Dim DWidth As String = String.Empty
        Dim arrCaseID() As String
        Dim CasesVal(9) As String

        Dim dsEquip As New DataSet
        Dim dsSEquip As New DataSet
        Dim dsCost As New DataSet
        Dim dsDept As New DataSet

        Try
            dsEquip = objGetData.GetEquipment("-1", "", "")

            dsSEquip = objGetData.GetSupportEquipment("-1", "", "")
            dsCost = objGetData.GetCostTypeInfo("-1", "")
            dsDept = objGetData.GetDeptN("-1", "", "")

            arrCaseID = GetCaseIds()
            DataCnt = arrCaseID.Length - 1

            For i = 0 To 9
                If i <= DataCnt Then
                    CasesVal(i) = arrCaseID(i)
                Else
                    CasesVal(i) = Nothing
                End If
            Next
            Updatebtn.Attributes.Add("onClick", "return UpdateAll('" + CasesVal(0).ToString() + "','" + CasesVal(1).ToString() + "','" + CasesVal(2) + "','" + CasesVal(3) + "','" + CasesVal(4) + "','" + CasesVal(5) + "','" + CasesVal(6) + "','" + CasesVal(7) + "','" + CasesVal(8) + "','" + CasesVal(9) + "','" + (DataCnt + 1).ToString() + "');")


            Dim trHeader As New TableRow
            Dim tdHeader As TableCell
            Dim tdInner As TableCell
            Dim trInner As New TableRow
            Dim ddl As DropDownList
            Dim lbl As New Label
            Dim txt As TextBox
            Dim Cunits As New Integer
            Dim Units As New Integer
            Dim CurrTitle As String = String.Empty
            Dim CCurrTitle As String = String.Empty
            Dim UnitError As String = String.Empty
            Dim CurrError As String = String.Empty
            Dim Title As String = String.Empty
            Dim hid As New HiddenField
            Dim Link As New HyperLink
            Dim btn As New Button
            Dim objGetDataW As New E3GetData.Selectdata
            Dim dsW As New DataSet

            dsW = objGetDataW.CompCOLWIDTH(AssumptionId)
            If dsW.Tables(0).Rows.Count > 0 Then
                txtDWidth.Text = dsW.Tables(0).Rows(0).Item("COLWIDTH").ToString()
            End If
            DWidth = txtDWidth.Text + "px"


            tdHeader = New TableCell
            HeaderTdSetting(tdHeader, "200px", "<img alt='' src='../../Images/spacer.gif' Style='width:200px;height:0px;'  />", 1)
            trHeader.Controls.Add(tdHeader)
            trHeader.Height = 20
            trHeader.CssClass = "PageSSHeading"

            For i = 0 To DataCnt
                Dim ds As New DataSet
                ds = objGetData.GetSupportEquipmentInDetails(arrCaseID(i))
                ds.Tables(0).TableName = arrCaseID(i).ToString()
                dstbl.Tables.Add(ds.Tables(arrCaseID(i).ToString()).Copy())
            Next





            For i = 0 To DataCnt
                dsCaseDetails = objGetData.GetCaseDetails(arrCaseID(i).ToString())
                Cunits = Convert.ToInt32(dstbl.Tables(0).Rows(0).Item("Units").ToString())
                Units = Convert.ToInt32(dstbl.Tables(i).Rows(0).Item("Units").ToString())
                CCurrTitle = dstbl.Tables(0).Rows(0).Item("ASSESTCOSTUNIT").ToString().Trim()
                CurrTitle = dstbl.Tables(i).Rows(0).Item("ASSESTCOSTUNIT").ToString().Trim()


                tdHeader = New TableCell
                Dim Headertext As String = String.Empty
                If Cunits <> Units Then
                    UnitError = "<br/> <span  style='color:red'>Unit Mismatch</span>"
                Else
                    UnitError = ""
                End If

                If CCurrTitle <> CurrTitle Then
                    CurrError = "<br/> <span  style='color:red'>Currency Mismatch</span>"
                Else
                    CurrError = ""
                End If

                Headertext = "Case#:" + arrCaseID(i).ToString() + " " + dsCaseDetails.Tables(0).Rows(0).Item("COUNTRYDES").ToString() + "<br/>" + dsCaseDetails.Tables(0).Rows(0).Item("CaseDes").ToString() + UnitError + CurrError + "<input type='hidden' value='" + arrCaseID(i).ToString() + "' name='Case" + i.ToString() + "'/>"

                CaseDesp.Add(arrCaseID(i).ToString())
                HeaderTdSetting(tdHeader, DWidth, Headertext, 1)
                trHeader.Controls.Add(tdHeader)


            Next
            tblComparision.Controls.Add(trHeader)

            'SUD STARTED
            trHeader = New TableRow
            tdHeader = New TableCell
            tdHeader = New TableCell
            HeaderTdSetting(tdHeader, "200px", "<img alt='' src='../../Images/spacer.gif' Style='width:200px;height:0px;'  />", 1)
            trHeader.Controls.Add(tdHeader)
            trHeader.Height = 20
            trHeader.CssClass = "PageSSHeading"


            For i = 0 To DataCnt
                tdHeader = New TableCell
                Dim Headertext As String = String.Empty
                Headertext = "Case#:" + arrCaseID(i).ToString() + "<br/>"
                CaseDesp.Add(arrCaseID(i).ToString())

                btn = New Button
                btn.ID = "btn_" + (i + 1).ToString()
                btn.Text = "Update"
                btn.Height = 15
                btn.Style.Add("font-size", "9px")
                btn.CommandArgument = arrCaseID(i).ToString()
                If arrCaseID(i) <= 1000 Then
                    btn.Enabled = False
                    btn.Style.Add("background-color", "#a6a6a6")
                    btn.Style.Add("color", "#4d4d4d")
                End If
                'AddHandler btn.Click, AddressOf Update_Click
                btn.Attributes.Add("onClick", "return Update('" + arrCaseID(i).ToString() + "','" + (i + 1).ToString() + "');")


                HeaderTdSetting(tdHeader, DWidth, Headertext, 1)
                ' tdHeader.Controls.Add(hid)
                tdHeader.Controls.Add(btn)
                trHeader.Controls.Add(tdHeader)


            Next
            tblComparision.Controls.Add(trHeader)
            'SUD ENDED

            For i = 1 To 30
                For j = 1 To 12
                    trInner = New TableRow()

                    Select Case j
                        Case 1
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "<b>Asset " + i.ToString() + "</b>", trInner, "AlterNateColor4")
                            For k = 0 To DataCnt
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
                                'lbl = New Label
                                'lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                'GetSupportEquipmentInDetails(lbl, dstbl.Tables(k).Rows(0).Item("ASSETID" + i.ToString() + "").ToString())
                                'tdInner.Controls.Add(lbl)
                                Link = New HyperLink
                                hid = New HiddenField
                                Link.ID = "hypAssetDes" + i.ToString() + "_" + (k + 1).ToString()
                                hid.ID = "hidAssetId" + i.ToString() + "_" + (k + 1).ToString()
                                Link.Width = DWidth.Replace("px", "") - 5
                                Link.CssClass = "Link"
                                GetSupportEquipmentInDetails(Link, hid, CInt(dstbl.Tables(k).Rows(0).Item("ASSETID" + i.ToString() + "").ToString()), dsSEquip)
                                If arrCaseID(k) <= 1000 Then
                                    Link.NavigateUrl = Nothing
                                End If
                                tdInner.Controls.Add(Link)
                                tdInner.Controls.Add(hid)
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 3
                            tdInner = New TableCell
                            Title = ""
                            LeftTdSetting(tdInner, "Number of Assets" + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "NOASSET_" + i.ToString()

                            For k = 0 To DataCnt

                                txt = New TextBox
                                txt.ID = "NOASSET" + i.ToString() + "_" + (k + 1).ToString()
                                txt.Attributes.Add("OnkeyPress", "return clickButton(event)")
                                txt.CssClass = "PrefTextBox"
                                txt.MaxLength = 12
                                txt.Width = 70
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                txt.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("NUM" + i.ToString() + "").ToString(), 0).ToString()
                                ' tdInner.Controls.Add(lbl)
                                If arrCaseID(k) <= 1000 Then
                                    txt.Style.Add("background-color", "#a6a6a6")
                                    txt.Enabled = False
                                End If
                                tdInner.Controls.Add(txt)
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 4
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Maximum Annual Run Hours", trInner, "AlterNateColor1")
                            trInner.ID = "MAXHR_" + i.ToString()

                            'For k = 0 To DataCnt
                            '    lbl = New Label
                            '    lbl.Style.Add("Width", DWidth)
                            '    tdInner = New TableCell
                            '    InnerTdSetting(tdInner, "", "Right")
                            '    lbl.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("HRS" + i.ToString() + "").ToString(), 0).ToString()
                            '    tdInner.Controls.Add(lbl)
                            '    trInner.Controls.Add(tdInner)
                            'Next
                            For k = 0 To DataCnt
                                txt = New TextBox
                                txt.ID = "NOHR" + i.ToString() + "_" + (k + 1).ToString()
                                txt.Attributes.Add("OnkeyPress", "return clickButton(event)")
                                txt.CssClass = "PrefTextBox"
                                txt.MaxLength = 12
                                txt.Width = 70
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                txt.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("HRS" + i.ToString() + "").ToString(), 0).ToString()
                                ' tdInner.Controls.Add(lbl)
                                If arrCaseID(k) <= 1000 Then
                                    txt.Style.Add("background-color", "#a6a6a6")
                                    txt.Enabled = False
                                End If
                                tdInner.Controls.Add(txt)
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 5
                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("ASSESTCOSTUNIT").ToString() + ")"
                            LeftTdSetting(tdInner, "Asset Cost Sugg. " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "ACS_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                lbl.ID = "ASCOSTS" + i.ToString() + "_" + (k + 1).ToString()
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                lbl.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("ASSETS" + i.ToString() + "").ToString(), 0).ToString()
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 6
                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("ASSESTCOSTUNIT").ToString() + ")"
                            LeftTdSetting(tdInner, "Asset Cost Pref. " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "ACP_" + i.ToString()

                            For k = 0 To DataCnt
                                'lbl = New Label
                                'lbl.Style.Add("Width", DWidth)
                                txt = New TextBox
                                txt.ID = "ASCOSTP" + i.ToString() + "_" + (k + 1).ToString()
                                txt.Attributes.Add("OnkeyPress", "return clickButton(event)")
                                txt.CssClass = "PrefTextBox"

                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                ' lbl.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("ASSETP" + i.ToString() + "").ToString(), 0).ToString()
                                ' tdInner.Controls.Add(lbl)
                                txt.Width = 70
                                txt.MaxLength = 12
                                txt.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("ASSETP" + i.ToString() + "").ToString(), 0).ToString()
                                If arrCaseID(k) <= 1000 Then
                                    txt.Style.Add("background-color", "#a6a6a6")
                                    txt.Enabled = False
                                End If
                                tdInner.Controls.Add(txt)
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 7
                            tdInner = New TableCell
                            Title = "(kw/hr)"
                            LeftTdSetting(tdInner, "Elec. Consumption Sugg. " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "ECS_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                lbl.ID = "ECS" + i.ToString() + "_" + (k + 1).ToString()
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                lbl.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("ECS" + i.ToString() + "").ToString(), 0).ToString()
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 8
                            tdInner = New TableCell
                            Title = "(kw/hr)"
                            LeftTdSetting(tdInner, "Elec. Consumption Pref. " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "ECP_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                lbl.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("ECP" + i.ToString() + "").ToString(), 0).ToString()
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 9
                            tdInner = New TableCell
                            Title = "(cubic ft/hr)"
                            LeftTdSetting(tdInner, "Nat. Gas Consumption Sugg. " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "GCS_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                lbl.ID = "NGCS" + i.ToString() + "_" + (k + 1).ToString()
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                lbl.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("NGCS" + i.ToString() + "").ToString(), 0).ToString()
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 10
                            tdInner = New TableCell
                            Title = "(cubic ft/hr)"
                            LeftTdSetting(tdInner, "Nat. Gas Consumption Pref. " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "GCP_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                lbl.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("NGCP" + i.ToString() + "").ToString(), 0).ToString()
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 11
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Cost Type", trInner, "AlterNateColor2")
                            trInner.ID = "CT_" + i.ToString()

                            For k = 0 To DataCnt
                                'lbl = New Label
                                'lbl.Style.Add("Width", DWidth)
                                Link = New HyperLink
                                hid = New HiddenField
                                Link.ID = "hypCTdes" + i.ToString() + "_" + (k + 1).ToString()
                                hid.ID = "hidCTdesid" + i.ToString() + "_" + (k + 1).ToString()
                                Link.CssClass = "Link"
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                'GetCostTypeDetails(lbl, dstbl.Tables(k).Rows(0).Item("COSTTYPE" + i.ToString() + "").ToString())
                                'tdInner.Controls.Add(lbl)
                                GetCostTypeDetails(Link, hid, dstbl.Tables(k).Rows(0).Item("COSTTYPE" + i.ToString() + "").ToString(), dsCost)
                                If arrCaseID(k) <= 1000 Then
                                    Link.NavigateUrl = Nothing
                                End If
                                tdInner.Controls.Add(hid)
                                tdInner.Controls.Add(Link)
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 12
                            tdInner = New TableCell
                            Title = ""
                            LeftTdSetting(tdInner, "Mfg. Department" + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "MD_" + i.ToString()

                            'For k = 0 To DataCnt
                            '    lbl = New Label
                            '    lbl.Style.Add("Width", DWidth)
                            '    tdInner = New TableCell
                            '    InnerTdSetting(tdInner, "", "Right")
                            '    GetDeptDetails(lbl, dstbl.Tables(k).Rows(0).Item("DEP" + i.ToString() + "").ToString(), arrCaseID(k).ToString())
                            '    tdInner.Controls.Add(lbl)
                            '    trInner.Controls.Add(tdInner)
                            'Next
                            For k = 0 To DataCnt

                                Link = New HyperLink
                                hid = New HiddenField
                                Link.ID = "hypHRdes" + i.ToString() + "_" + (k + 1).ToString()
                                hid.ID = "hidHRdesid" + i.ToString() + "_" + (k + 1).ToString()
                                Link.CssClass = "Link"
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                GetDeptDetails(Link, dstbl.Tables(k).Rows(0).Item("DEP" + i.ToString() + "").ToString(), arrCaseID(k), hid, dsDept)
                                If arrCaseID(k) <= 1000 Then
                                    Link.NavigateUrl = Nothing
                                End If
                                tdInner.Controls.Add(hid)
                                tdInner.Controls.Add(Link)
                                trInner.Controls.Add(tdInner)
                            Next
                    End Select
                    If j <> 1 Then
                        If (j Mod 2 = 0) Then
                            trInner.CssClass = "AlterNateColor1"
                        Else
                            trInner.CssClass = "AlterNateColor2"
                        End If
                    End If
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

    Protected Sub TextBoxSetting(ByVal txt As TextBox, ByVal Css As String, ByVal CaseId As Integer)
        Try
            txt.CssClass = Css
            If CaseId <= 1000 And Session("Password") <> "9krh65sve3" Then
                txt.Enabled = False
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub LableSetting(ByVal lbl As Label, ByVal Css As String)
        Try
            lbl.CssClass = Css

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

    Protected Sub GetEquipmentInDetails(ByRef lbl As Label, ByVal EqId As Integer, ByVal dsEquip As DataSet)
        Dim Ds As New DataSet
        Dim ObjGetdata As New E1GetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty
        Dim dvEquip As New DataView
        Dim dtEquip As New DataTable
        Try
            'Ds = ObjGetdata.GetEquipment(EqId, "", "")
            dvEquip = dsEquip.Tables(0).DefaultView
            dvEquip.RowFilter = "EQUIPID = " + EqId.ToString()
            dtEquip = dvEquip.ToTable()

            lbl.Text = dtEquip.Rows(0).Item("equipDES").ToString()
        Catch ex As Exception
            ErrorLable.Text = "Error:GetEquipmentInDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetDeptDetailsOLD(ByRef lbl As Label, ByVal ProcId As Integer, ByVal CaseId As String)
        Dim Ds As New DataSet
        Dim ObjGetdata As New E1GetData.Selectdata()
        Dim Path As String = String.Empty
        Try
            Ds = ObjGetdata.GetDept(ProcId, "", "", CaseId)

            If Ds.Tables(0).Rows.Count = 0 Then
                lbl.Text = "Dept. Conflict"
                lbl.ForeColor = Drawing.Color.DarkRed
            Else
                lbl.Text = Ds.Tables(0).Rows(0).Item("PROCDE").ToString()
            End If



        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetDeptDetails(ByRef LinkCost As HyperLink, ByVal ProcId As Integer, ByVal CaseId As String, ByVal hid As HiddenField, ByVal dsDept As DataSet)
        Dim Ds As New DataSet
        Dim ObjGetdata As New E1GetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty
        Dim dvDept As New DataView
        Dim dtDept As New DataTable
        Try
            'Ds = ObjGetdata.GetDept(ProcId, "", "", CaseId)

            dvDept = dsDept.Tables(0).DefaultView
            dvDept.RowFilter = "PROCID =" + ProcId.ToString()
            dtDept = dvDept.ToTable()

            'If Ds.Tables(0).Rows.Count = 0 Then
            '    lbl.Text = "Dept. Conflict"
            '    lbl.ForeColor = Drawing.Color.DarkRed
            'Else
            '    lbl.Text = Ds.Tables(0).Rows(0).Item("PROCDE").ToString()
            'End If

            LinkCost.Text = dtDept.Rows(0).Item("PROCDE").ToString()
            hid.Value = ProcId.ToString()
            Path = "../../ECON1/PopUp/GetDepPopUp.aspx?Des=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + LinkCost.ClientID + "&Id=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + hid.ClientID + "&CaseId=" + CaseId + ""
            LinkCost.NavigateUrl = "javascript:ShowPopWindow('" + Path + "')"


        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetCostTypeDetailsOLD(ByRef lbl As Label, ByVal CostId As Integer)
        Dim Ds As New DataSet
        Dim ObjGetdata As New E1GetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty
        Try

            Ds = ObjGetdata.GetCostTypeInfo(CostId, "")
            lbl.Text = Ds.Tables(0).Rows(0).Item("costde1").ToString()
        Catch ex As Exception
            ErrorLable.Text = "Error:GetCostTypeDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetCostTypeDetails(ByRef LinkCost As HyperLink, ByVal hid As HiddenField, ByVal CostId As Integer, ByVal dsCost As DataSet)
        Dim Ds As New DataSet
        Dim ObjGetdata As New E1GetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty
        Dim dvCost As New DataView
        Dim dtCost As New DataTable
        Try
            'Ds = ObjGetdata.GetCostTypeInfo(CostId, "")
            dvCost = dsCost.Tables(0).DefaultView
            dvCost.RowFilter = "COSTID = " + CostId.ToString()
            dtCost = dvCost.ToTable()

            LinkCost.Text = dtCost.Rows(0).Item("costde1").ToString()
            hid.Value = CostId.ToString()
            Path = "../../ECON1/PopUp/GetCostTypePopup.aspx?Des=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + LinkCost.ClientID + "&Id=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + hid.ClientID + ""
            LinkCost.NavigateUrl = "javascript:ShowPopWindow('" + Path + "')"
        Catch ex As Exception
            ErrorLable.Text = "Error:GetCostTypeDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetSupportEquipmentInDetailsOLD(ByRef lbl As Label, ByVal EqId As Integer)
        Dim Ds As New DataSet
        Dim ObjGetdata As New E1GetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty
        Try

            Ds = ObjGetdata.GetSupportEquipment(EqId, "", "")
            lbl.Text = Ds.Tables(0).Rows(0).Item("equipDES").ToString()
        Catch ex As Exception
            ErrorLable.Text = "Error:GetSupportEquipmentInDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetSupportEquipmentInDetails(ByRef LinkMat As HyperLink, ByVal hid As HiddenField, ByVal EqId As Integer, ByVal dsSEquip As DataSet)
        Dim Ds As New DataSet
        Dim ObjGetdata As New E1GetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty
        Dim dvEquip As New DataView
        Dim dtEquip As New DataTable
        Try
            'Ds = ObjGetdata.GetSupportEquipment(EqId, "", "")
            dvEquip = dsSEquip.Tables(0).DefaultView
            dvEquip.RowFilter = "EQUIPID = " + EqId.ToString()
            dtEquip = dvEquip.ToTable()

            LinkMat.Text = dtEquip.Rows(0).Item("equipDES").ToString()
            hid.Value = EqId.ToString()
            Path = "../../Econ1/PopUp/GetEquip2Popup.aspx?Des=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + LinkMat.ClientID + "&Id=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + hid.ClientID + ""
            LinkMat.NavigateUrl = "javascript:ShowPopWindow('" + Path + "')"

        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    <System.Web.Services.WebMethod()> _
    Public Shared Function UpdateCase(ByVal CaseID As String, ByVal index As String, ByVal EQ() As String, ByVal NUM() As String, ByVal COST() As String, ByVal COSTTYPE() As String, ByVal hours() As String, ByVal dept() As String) As Array
        Try
            Dim ObjGetData As New E1GetData.Selectdata()
            Dim EconConnection As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            Dim StrSqlUpadate As String = ""
            Dim StrSqlIUpadate As String = ""
            Dim odbUtil As New DBUtil()

            Dim dts As New DataSet()
            Dim ObjE3GetData As New E3GetData.Selectdata()
            Dim dsD As New DataSet
            Dim PageInfo As String = ""
            Dim ArrVal(30) As String

            dts = ObjGetData.GetPref(CaseID)

            Dim Convwt As String = dts.Tables(0).Rows(0).Item("convwt")
            Dim Curr As String = dts.Tables(0).Rows(0).Item("curr")
            Dim Convthick As String = dts.Tables(0).Rows(0).Item("convthick")

            'Updating No of Assets
            StrSqlUpadate = ""
            StrSqlIUpadate = ""
            StrSqlIUpadate = String.Empty
            StrSqlUpadate = "UPDATE EQUIPMENT2NUMBER SET"
            For Mat = 0 To 29
                StrSqlIUpadate = StrSqlIUpadate + " M" + (Mat + 1).ToString() + "=" + NUM(Mat) + ","
            Next
            StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
            StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
            StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
            odbUtil.UpIns(StrSqlUpadate, EconConnection)

            'Updating Asset Description
            StrSqlUpadate = ""
            StrSqlIUpadate = ""
            StrSqlIUpadate = String.Empty
            StrSqlUpadate = "UPDATE equipment2TYPE SET"
            For Mat = 0 To 29
                StrSqlIUpadate = StrSqlIUpadate + " M" + (Mat + 1).ToString() + "=" + EQ(Mat) + ","
            Next
            StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
            StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
            StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
            odbUtil.UpIns(StrSqlUpadate, EconConnection)


            'Updating Asset Cost Pref
            StrSqlUpadate = ""
            StrSqlIUpadate = ""
            StrSqlIUpadate = String.Empty
            StrSqlUpadate = "UPDATE equipment2COST SET"
            For Mat = 0 To 29
                Dim AssetPreffered As Decimal
                AssetPreffered = CDbl(COST(Mat).Replace(",", "") / Curr)
                StrSqlIUpadate = StrSqlIUpadate + " M" + (Mat + 1).ToString() + "=" + AssetPreffered.ToString() + ","
            Next
            StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
            StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
            StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
            odbUtil.UpIns(StrSqlUpadate, EconConnection)


            'Updating CostType
            StrSqlUpadate = ""
            StrSqlIUpadate = ""
            StrSqlUpadate = "UPDATE EQUIPMENT2COSTTYPE SET"
            For Mat = 0 To 29
                StrSqlIUpadate = StrSqlIUpadate + " M" + (Mat + 1).ToString() + " = " + COSTTYPE(Mat).ToString().Replace(",", "") + ","
            Next
            StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
            StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
            StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
            odbUtil.UpIns(StrSqlUpadate, EconConnection)

            'Updating hours
            StrSqlUpadate = ""
            StrSqlIUpadate = ""
            StrSqlUpadate = "UPDATE EQUIPMENT2MAHRS SET"
            For Mat = 0 To 29
                StrSqlIUpadate = StrSqlIUpadate + " M" + (Mat + 1).ToString() + " = " + hours(Mat).ToString().Replace(",", "") + ","
            Next
            StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
            StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
            StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
            odbUtil.UpIns(StrSqlUpadate, EconConnection)

            'Updating Mgt.Hrs.
            StrSqlUpadate = ""
            StrSqlIUpadate = ""
            StrSqlUpadate = "UPDATE EQUIPMENT2DEP SET"
            For Mat = 0 To 29
                StrSqlIUpadate = StrSqlIUpadate + " M" + (Mat + 1).ToString() + " = " + dept(Mat).ToString().Replace(",", "") + ","
            Next
            StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
            StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
            StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
            odbUtil.UpIns(StrSqlUpadate, EconConnection)

            'Dim UserName As String = uName
            'Dim ObjUpIns As New E1UpInsData.UpdateInsert
            'ObjUpIns.ServerDateUpdate(CaseID, UserName)

            'Updating serverdate 
            Dim UserName As String = uName
            Dim ObjUpIns As New E1UpInsData.UpdateInsert
            Dim StrSql As String
            If CaseID < 1000 Then
                StrSql = "Update BASECASES set ServerDate=SYSDATE WHERE CASEID=" + CaseID.ToString() + " "
            Else
                StrSql = "Update Permissionscases set ServerDate=SYSDATE WHERE CASEID=" + CaseID.ToString() + " AND UPPER(USERNAME)='" + UserName.ToUpper() + "' "
            End If
            odbUtil.UpIns(StrSql, EconConnection)

            Dim Econ1Conn As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            Dim obj As New EconCalculation.EconCalculation(Econ1Conn)
            obj.EconCalculate(CaseID)

            Dim ds As New DataSet
            Dim l As Integer = 0
            ds = ObjGetData.GetSupportEquipmentInDetails(CaseID)
            For i = 0 To 29
                ArrVal(i) = FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("ASSETS" + (i + 1).ToString()).ToString()), 0) + "#" + FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("ECS" + (i + 1).ToString()).ToString()), 0) + "#" + FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("NGCS" + (i + 1).ToString()).ToString()), 0)
                If i = 0 Then
                    ArrVal(i) = ArrVal(i) + "#" + index.ToString()
                End If
            Next
            Return ArrVal
        Catch ex As Exception

        End Try
    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function UpdateAllCases(ByVal CaseID As String, ByVal index As String, ByVal flag As String, ByVal EQ() As String, ByVal NUM() As String, ByVal COST() As String, ByVal COSTTYPE() As String, ByVal hours() As String, ByVal dept() As String) As Array
        Try
            Dim WPT(10) As String
            Dim ObjGetData As New E1GetData.Selectdata()
            Dim ObjE3GetData As New E3GetData.Selectdata()
            Dim EconConnection As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            Dim StrSqlUpadate As String = ""
            Dim StrSqlIUpadate As String = ""
            Dim odbUtil As New DBUtil()
            Dim dts As New DataSet()
            Dim dsD As New DataSet
            Dim PageInfo As String = ""
            Dim ArrVal(30) As String
            dts = ObjGetData.GetPref(CaseID)

            Dim Convwt As String = dts.Tables(0).Rows(0).Item("convwt")
            Dim Curr As String = dts.Tables(0).Rows(0).Item("curr")
            Dim Convthick As String = dts.Tables(0).Rows(0).Item("convthick")

            'Updating No of Asset
            StrSqlUpadate = ""
            StrSqlIUpadate = ""
            StrSqlUpadate = "UPDATE EQUIPMENT2NUMBER SET"
            For Mat = 0 To 29
                StrSqlIUpadate = StrSqlIUpadate + " M" + (Mat + 1).ToString() + "=" + NUM(Mat) + ","
            Next
            StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
            StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
            StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
            odbUtil.UpIns(StrSqlUpadate, EconConnection)


            'Updating Asset description
            StrSqlUpadate = ""
            StrSqlIUpadate = ""
            StrSqlIUpadate = String.Empty
            StrSqlUpadate = "UPDATE equipment2TYPE SET"
            For Mat = 0 To 29
                StrSqlIUpadate = StrSqlIUpadate + " M" + (Mat + 1).ToString() + "=" + EQ(Mat) + ","
            Next
            StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
            StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
            StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
            odbUtil.UpIns(StrSqlUpadate, EconConnection)

            'Updating Asset Cost Pref
            StrSqlUpadate = ""
            StrSqlIUpadate = ""
            StrSqlIUpadate = String.Empty
            StrSqlUpadate = "UPDATE equipment2COST SET"
            For Mat = 0 To 29
                Dim AssetPreffered As Decimal
                AssetPreffered = CDbl(COST(Mat).Replace(",", "") / Curr)
                StrSqlIUpadate = StrSqlIUpadate + " M" + (Mat + 1).ToString() + "=" + AssetPreffered.ToString() + ","
            Next
            StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
            StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
            StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
            odbUtil.UpIns(StrSqlUpadate, EconConnection)


            'Updating CostType
            StrSqlUpadate = ""
            StrSqlIUpadate = ""
            StrSqlUpadate = "UPDATE EQUIPMENT2COSTTYPE SET"
            For Mat = 0 To 29
                StrSqlIUpadate = StrSqlIUpadate + " M" + (Mat + 1).ToString() + " = " + COSTTYPE(Mat).ToString().Replace(",", "") + ","
            Next
            StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
            StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
            StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
            odbUtil.UpIns(StrSqlUpadate, EconConnection)

            'Updating hours
            StrSqlUpadate = ""
            StrSqlIUpadate = ""
            StrSqlUpadate = "UPDATE EQUIPMENT2MAHRS SET"
            For Mat = 0 To 29
                StrSqlIUpadate = StrSqlIUpadate + " M" + (Mat + 1).ToString() + " = " + hours(Mat).ToString().Replace(",", "") + ","
            Next
            StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
            StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
            StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
            odbUtil.UpIns(StrSqlUpadate, EconConnection)

            'Updating Mgt.Hrs.
            StrSqlUpadate = ""
            StrSqlIUpadate = ""
            StrSqlUpadate = "UPDATE EQUIPMENT2DEP SET"
            For Mat = 0 To 29
                StrSqlIUpadate = StrSqlIUpadate + " M" + (Mat + 1).ToString() + " = " + dept(Mat).ToString().Replace(",", "") + ","
            Next
            StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
            StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
            StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
            odbUtil.UpIns(StrSqlUpadate, EconConnection)

            Dim UserName As String = uName
            Dim ObjUpIns As New E1UpInsData.UpdateInsert
            ObjUpIns.ServerDateUpdate(CaseID, UserName)

            Dim Econ1Conn As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            Dim obj As New EconCalculation.EconCalculation(Econ1Conn)
            obj.EconCalculate(CaseID)

            Dim ds As New DataSet
            Dim l As Integer = 0
            ds = ObjGetData.GetSupportEquipmentInDetails(CaseID)
            For i = 0 To 29

                ArrVal(i) = FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("ASSETS" + (i + 1).ToString()).ToString()), 0) + "#" + FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("ECS" + (i + 1).ToString()).ToString()), 0) + "#" + FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("NGCS" + (i + 1).ToString()).ToString()), 0)
                If i = 0 Then
                    ArrVal(i) = ArrVal(i) + "#" + index.ToString() + "#" + flag
                End If
            Next
            Return ArrVal
        Catch ex As Exception

        End Try
    End Function

End Class
