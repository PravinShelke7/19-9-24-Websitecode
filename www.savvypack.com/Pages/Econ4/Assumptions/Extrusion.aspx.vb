Imports System.Data
Imports System.Data.OleDb
Imports System
Imports E4GetData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Econ4_Assumptions_Extrusion
    Inherits System.Web.UI.Page

#Region "Get Set Variables"
    Dim _lErrorLble As Label
    Dim _strUserName As String
    Dim _strPassword As String
    Dim _iAssumptionId As Integer
    Dim _btnUpdate As ImageButton
    Dim _strUserId as string

Public Property UserId() As String
        Get
            Return _strUserId
        End Get
        Set(ByVal Value As String)
            _strUserId = Value
        End Set
    End Property


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


    Public DataCnt As Integer
    Public CaseDesp As New ArrayList

#End Region

#Region "MastePage Content Variables"
    Protected Sub GetErrorLable()
        ErrorLable = Page.Master.FindControl("lblError")
    End Sub

    Protected Sub GetUpdatebtn()
        Updatebtn = Page.Master.FindControl("imgUpdate")
        Updatebtn.Visible = False

    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Try
                GetErrorLable()
                GetUpdatebtn()
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
            UserId= Session("USERID")
            Password = Session("Password")
            AssumptionId = Session("AssumptionID")

            lblAID.Text = AssumptionId
            lblAdes.Text = Session("Description")

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetSessionDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Function GetCaseIds() As String()
        Dim CaseIds(0) As String
        Dim objGetData As New E4GetData.Selectdata
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
        Dim objGetData As New E2GetData.Selectdata
        Dim objGetData1 As New E1GetData.Selectdata
        Dim CaseIds As String = String.Empty
        'Dim DataCnt As New Integer
        Dim i As New Integer
        Dim j As New Integer
        Dim k As New Integer
        Dim DWidth As String = String.Empty
        Dim arrCaseID() As String

        Dim dsDept As New DataSet
        Dim dsMat As New DataSet
        Dim dsEcon1 As New DataSet
        Dim dsE1tbl As New DataSet
        Try
            dsMat = objGetData.GetMaterials("-1", "", "")
            dsDept = objGetData.GetDeptN(-1, "", "")

            arrCaseID = GetCaseIds()
            DataCnt = arrCaseID.Length - 1

            Dim trHeader As New TableRow
            Dim tdHeader As TableCell
            Dim tdInner As TableCell
            Dim trInner As New TableRow
            Dim ddl As DropDownList
            Dim lbl As New Label
            Dim txt As TextBox
            Dim chk As New CheckBox
            Dim Cunits As New Integer
            Dim Units As New Integer
            Dim Title As String = String.Empty
            Dim CurrTitle As String = String.Empty
            Dim CCurrTitle As String = String.Empty
            DWidth = txtDWidth.Text + "px"
            Dim UnitError As String = String.Empty
            Dim CurrError As String = String.Empty

            tdHeader = New TableCell
            HeaderTdSetting(tdHeader, "200px", "<img alt='' src='../../Images/spacer.gif' Style='width:200px;height:0px;'  />", 1)
            trHeader.Controls.Add(tdHeader)
            trHeader.Height = 20
            trHeader.CssClass = "PageSSHeading"

            For i = 0 To DataCnt
                Dim ds As New DataSet
                ds = objGetData.GetExtrusionDetails(arrCaseID(i))
                ds.Tables(0).TableName = arrCaseID(i).ToString()
                dstbl.Tables.Add(ds.Tables(arrCaseID(i).ToString()).Copy())

                dsEcon1 = objGetData.GetEcon1Cases(-1, arrCaseID(i).ToString(), "", "", UserId)
                dsEcon1.Tables(0).TableName = arrCaseID(i).ToString()
                dsE1tbl.Tables.Add(dsEcon1.Tables(arrCaseID(i).ToString()).Copy())

            Next





            For i = 0 To DataCnt
                dsCaseDetails = objGetData.GetCaseDetails(arrCaseID(i).ToString())
                Cunits = Convert.ToInt32(dstbl.Tables(0).Rows(0).Item("Units").ToString())
                Units = Convert.ToInt32(dstbl.Tables(i).Rows(0).Item("Units").ToString())
                CCurrTitle = dstbl.Tables(0).Rows(0).Item("Title2").ToString().Trim()
                CurrTitle = dstbl.Tables(i).Rows(0).Item("Title2").ToString().Trim()

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

                Headertext = "Case#:" + arrCaseID(i).ToString() + "<br/>" + dsCaseDetails.Tables(0).Rows(0).Item("CaseDes").ToString() + UnitError + CurrError + "<input type='hidden' value='" + arrCaseID(i).ToString() + "' name='Case" + i.ToString() + "'/>"

                CaseDesp.Add(arrCaseID(i).ToString())
                HeaderTdSetting(tdHeader, DWidth, Headertext, 1)
                trHeader.Controls.Add(tdHeader)


            Next
            tblComparision.Controls.Add(trHeader)



            For i = 1 To 10
                For j = 1 To 13
                    trInner = New TableRow()

                    Select Case j
                        Case 1
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "<b>Layer" + i.ToString() + "</b>", trInner, "AlterNateColor4")
                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Center")
                                tdInner.Text = "&nbsp;"
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 2
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Material", trInner, "AlterNateColor1")
                            trInner.ID = "M_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                GetMaterialDetails(lbl, dstbl.Tables(k).Rows(0).Item("M" + i.ToString() + "").ToString(), dsMat)
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 3
                            tdInner = New TableCell
                            Title = "(%)"
                            LeftTdSetting(tdInner, "Quantity " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "Q_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                lbl.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("Q" + i.ToString() + "").ToString(), 3).ToString()
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 4
                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("Title8").ToString() + ")"
                            LeftTdSetting(tdInner, "Weight Sugg. " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "WS_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                lbl.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("WEIGHTS" + i.ToString() + "").ToString(), 4).ToString()
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 5
                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("Title8").ToString() + ")"
                            LeftTdSetting(tdInner, "Weight Pref. " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "WP_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                lbl.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("WEIGHTP" + i.ToString() + "").ToString(), 4).ToString()
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 6
                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("Title2").ToString() + "/" + dstbl.Tables(0).Rows(0).Item("Title8").ToString() + ")"
                            LeftTdSetting(tdInner, "Price Sugg. " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "PRS_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                lbl.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("PRICES" + i.ToString() + "").ToString(), 3).ToString()
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 7
                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("Title2").ToString() + "/" + dstbl.Tables(0).Rows(0).Item("Title8").ToString() + ")"
                            LeftTdSetting(tdInner, "Price Pref. " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "PRP_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                lbl.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("PRICEP" + i.ToString() + "").ToString(), 3).ToString()
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 8
                            tdInner = New TableCell
                            Title = ""
                            LeftTdSetting(tdInner, "Package Component " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "PC_" + i.ToString()

                            For k = 0 To DataCnt
                                chk = New CheckBox
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                If (dstbl.Tables(k).Rows(0).Item("PCOMP" + i.ToString() + "").ToString() = "1") Then
                                    chk.Checked = True
                                Else
                                    chk.Checked = False
                                End If
                                chk.Enabled = False
                                tdInner.Controls.Add(chk)
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 9
                            tdInner = New TableCell
                            Title = ""
                            LeftTdSetting(tdInner, "Econ1 Cases " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "EC_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                'lbl.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("C" + i.ToString() + "").ToString(), 2).ToString()
                                GetEcon1Details(lbl, arrCaseID(k).ToString(), CInt(dstbl.Tables(k).Rows(0).Item("C" + i.ToString() + "").ToString()), UserId, dsE1tbl)
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 10
                            tdInner = New TableCell
                            Title = "(%)"
                            LeftTdSetting(tdInner, "Recycle " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "RE_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                lbl.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("REC" + i.ToString() + "").ToString(), 2).ToString()
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 11
                            tdInner = New TableCell
                            Title = "(%)"
                            LeftTdSetting(tdInner, "Extra-process " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "E_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                lbl.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("EP" + i.ToString() + "").ToString(), 2).ToString()
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 12
                            tdInner = New TableCell
                            Title = "(%)"
                            LeftTdSetting(tdInner, "Weight " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "W_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                lbl.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("WP" + i.ToString() + "").ToString(), 1).ToString()
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 13
                            tdInner = New TableCell
                            Title = ""
                            LeftTdSetting(tdInner, "Mfg. Dept." + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "D_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                GetDeptDetails(lbl, dstbl.Tables(k).Rows(0).Item("DEP" + i.ToString() + "").ToString(), arrCaseID(k).ToString(), dsDept)
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next

                    End Select
                    tblComparision.Controls.Add(trInner)
                Next
            Next

            ''Discrete Material
            'For i = 1 To 3
            '    For j = 1 To 4
            '        trInner = New TableRow
            '        Select Case j
            '            Case 1
            '                tdInner = New TableCell
            '                LeftTdSetting(tdInner, "<b>Discrete Materials" + i.ToString() + "</b>", trInner, "AlterNateColor4")
            '                For k = 0 To DataCnt
            '                    tdInner = New TableCell
            '                    InnerTdSetting(tdInner, "", "Center")
            '                    tdInner.Text = "&nbsp;"
            '                    trInner.Controls.Add(tdInner)
            '                Next

            '            Case 2
            '                tdInner = New TableCell
            '                Dim dsMat As New DataSet
            '                LeftTdSetting(tdInner, "Material", trInner, "AlterNateColor2")
            '                trInner.ID = "DISM_" + i.ToString()
            '                For k = 0 To DataCnt
            '                    tdInner = New TableCell
            '                    dsMat = objGetData1.GetDiscMaterials(CInt(dstbl.Tables(k).Rows(0).Item("DISID" + i.ToString() + "").ToString()), "")
            '                    tdInner.Text = dsMat.Tables(0).Rows(0).Item("matDISde1").ToString()
            '                    InnerTdSetting(tdInner, DWidth, "Right")
            '                    trInner.Controls.Add(tdInner)
            '                Next

            '            Case 3
            '                tdInner = New TableCell
            '                Title = "(" + dstbl.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
            '                LeftTdSetting(tdInner, "Weight " + Title + "", trInner, "AlterNateColor1")
            '                trInner.ID = "DISW_" + i.ToString()
            '                For k = 0 To DataCnt
            '                    tdInner = New TableCell
            '                    tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("DISW" + i.ToString() + "").ToString(), 4)
            '                    InnerTdSetting(tdInner, DWidth, "Right")
            '                    trInner.Controls.Add(tdInner)
            '                Next

            '            Case 4
            '                tdInner = New TableCell
            '                Title = "(" + dstbl.Tables(0).Rows(0).Item("TITLE2").ToString() + "/unit)"
            '                LeftTdSetting(tdInner, "Price " + Title + "", trInner, "AlterNateColor2")
            '                trInner.ID = "DISP_" + i.ToString()
            '                For k = 0 To DataCnt
            '                    tdInner = New TableCell
            '                    tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("DISP" + i.ToString() + "").ToString(), 4)
            '                    InnerTdSetting(tdInner, DWidth, "Right")
            '                    trInner.Controls.Add(tdInner)

            '                Next

            '        End Select
            '        tblComparision.Controls.Add(trInner)
            '    Next
            'Next

            For i = 1 To 1
                For j = 1 To 2
                    trInner = New TableRow()
                    Select Case j
                        Case 1
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "<b></b>", trInner, "AlterNateColor4")
                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Center")
                                tdInner.Text = "&nbsp;"
                                trInner.Controls.Add(tdInner)
                            Next


                        Case 2
                            Dim cnt As Integer
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Printing Plates", trInner, "AlterNateColor2")
                            trInner.ID = "PCMQ_" + i.ToString()
                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                cnt = dstbl.Tables(k).Rows(0).Item("PLATE")
                                Select Case cnt
                                    Case 0
                                        tdInner.Text = "not required"
                                    Case 1
                                        tdInner.Text = "produced by packaging supplier"
                                    Case 2
                                        tdInner.Text = "produced by outside supplier"
                                End Select

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

    Protected Sub GetMaterialDetails(ByRef lbl As Label, ByVal MatId As Integer, ByVal dsMat As DataSet)
        Dim Ds As New DataSet
        Dim ObjGetdata As New E2GetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty
        Dim dvMat As New DataView
        Dim dtMat As New DataTable
        Try
            'Ds = ObjGetdata.GetMaterials(MatId, "", "")
            dvMat = dsMat.Tables(0).DefaultView
            dvMat.RowFilter = "MATID= " + MatId.ToString()
            dtMat = dvMat.ToTable()

            If dtMat.Rows(0).Item("MATDES").ToString().Length > 25 Then
                'LinkMat.Font.Size = 8
            End If
            lbl.Text = dtMat.Rows(0).Item("MATDES").ToString()
            lbl.ToolTip = dtMat.Rows(0).Item("MATDES").ToString()


        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetDeptDetails(ByRef lbl As Label, ByVal ProcId As Integer, ByVal CaseId As String, ByVal dsDept As DataSet)
        Dim Ds As New DataSet
        Dim ObjGetdata As New E2GetData.Selectdata()
        Dim Path As String = String.Empty
        Dim dvDept As New DataView
        Dim dtDept As New DataTable
        Try
            'Ds = ObjGetdata.GetDept(ProcId, "", "", CaseId)
            dvDept = dsDept.Tables(0).DefaultView
            dvDept.RowFilter = "PROCID = " + ProcId.ToString()
            dtDept = dvDept.ToTable()

            If dtDept.Rows.Count = 0 Then
                lbl.Text = "Dept. Conflict"
                lbl.ForeColor = Drawing.Color.DarkRed
            Else
                lbl.Text = dtDept.Rows(0).Item("PROCDE").ToString()
            End If



        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetEcon1Details(ByRef lbl As Label, ByVal CAseId As String, ByVal CaseIdE1 As Integer, ByVal Username As String, ByVal dsEcon1 As DataSet)
        Dim Ds As New DataSet
        Dim ObjGetdata As New E2GetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty
        Dim dvEcon1 As New DataView
        Dim dtEcon1 As New DataTable
        Try
            'Ds = ObjGetdata.GetEcon1Cases(CaseIdE1, CAseId, "", "", Username)
            dvEcon1 = dsEcon1.Tables(CAseId).DefaultView
            dvEcon1.RowFilter = "CASEID=" + CaseIdE1.ToString()
            dtEcon1 = dvEcon1.ToTable()

            If dtEcon1.Rows.Count = 0 Then
                lbl.Text = "Conflict"
                lbl.ForeColor = Drawing.Color.DarkRed
            Else
                lbl.Text = dtEcon1.Rows(0).Item("CASDES").ToString()
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

End Class
