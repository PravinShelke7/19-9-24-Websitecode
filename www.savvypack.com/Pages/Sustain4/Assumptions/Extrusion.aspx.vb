Imports System.Data
Imports System.Data.OleDb
Imports System
Imports S4GetData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Sustain4_Assumptions_Extrusion
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
        Dim objGetData As New S4GetData.Selectdata

        Try
            CaseIds = objGetData.Cases1(AssumptionId)
            Return CaseIds
        Catch ex As Exception
            Return CaseIds
            _lErrorLble.Text = "Error:GetCaseIds:" + ex.Message.ToString()
        End Try
        Return CaseIds
    End Function

    Protected Sub GetPageDetails()
        Dim ds As New DataSet
        Dim dstbl As New DataSet
        Dim dstblSustain As New DataSet
        Dim dsCaseDetails As New DataSet
        Dim objGetData As New S2GetData.Selectdata
        Dim CaseIds As String = String.Empty
        Dim i As New Integer
        Dim j As New Integer
        Dim l As New Integer
        Dim DWidth As String = String.Empty
        Dim arrCaseID() As String
        Try
            arrCaseID = GetCaseIds()
            DataCnt = arrCaseID.Length - 1

            Dim trHeader As New TableRow
            Dim tdHeader As TableCell
            Dim tdInner As TableCell
            Dim trInner As New TableRow
            Dim ddl As DropDownList


            trInner = New TableRow()
            Dim k As Integer
            Dim lbl As New Label
            Dim chk As New CheckBox
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
                ds = objGetData.GetExtrusionDetails(arrCaseID(i))
                ds.Tables(0).TableName = arrCaseID(i).ToString()
                dstbl.Tables.Add(ds.Tables(arrCaseID(i).ToString()).Copy())

                'for sustain1detail
                ds = objGetData.GetSustain1Cases(-1, arrCaseID(i).ToString(), "", "", Session("Userid"))
                ds.Tables(0).TableName = arrCaseID(i).ToString()
                dstblSustain.Tables.Add(ds.Tables(arrCaseID(i).ToString()).Copy())

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
                For j = 1 To 11
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
                            Dim dsMat As New DataSet
                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                dsMat = objGetData.GetMaterials(dstbl.Tables(k).Rows(0).Item("M" + i.ToString() + "").ToString(), "", "")
                                tdInner.Text = dsMat.Tables(0).Rows(0).Item("MATDES").ToString()
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)

                            Next

                        Case 3

                            tdInner = New TableCell
                            Title = ""
                            LeftTdSetting(tdInner, "Package Component " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "P_" + i.ToString()
                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                chk = New CheckBox
                                If (FormatNumber(dstbl.Tables(k).Rows(0).Item("IP" + i.ToString() + "").ToString(), 3) = 0) Then
                                    chk.Checked = False
                                Else
                                    chk.Checked = True
                                End If
                                chk.Enabled = False
                                tdInner.Controls.Add(chk)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 4

                            tdInner = New TableCell
                            Title = "(%)"
                            LeftTdSetting(tdInner, "Quantity " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "Q_" + i.ToString()

                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("Q" + i.ToString() + "").ToString(), 3)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                ' trRecycle.Controls.Add(tdInner)
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 5

                            tdInner = New TableCell
                            Title = "(%)"
                            LeftTdSetting(tdInner, "Sustain1 Cases " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "S_" + i.ToString()

                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, DWidth, "Right")
                                lbl.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("C" + i.ToString() + "").ToString(), 2).ToString()
                                GetSustain1Details(lbl, arrCaseID(k).ToString(), CInt(dstbl.Tables(k).Rows(0).Item("C" + i.ToString() + "").ToString()), Session("USERID"), dstblSustain)
                                tdInner.Text = lbl.Text
                                ' tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 6
                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("Title8").ToString() + " )"
                            LeftTdSetting(tdInner, "Weight Sugg. " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "WS_" + i.ToString()

                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("WEIGHTS" + i.ToString() + "").ToString(), 4)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 7

                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("Title8").ToString() + " )"
                            LeftTdSetting(tdInner, "Weight Pref. " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "WP_" + i.ToString()

                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("WEIGHTP" + i.ToString() + "").ToString(), 4)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 8


                            tdInner = New TableCell
                            Title = "(%)"
                            LeftTdSetting(tdInner, "Recycle " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "R_" + i.ToString()

                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("R" + i.ToString() + "").ToString(), 2)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)
                            Next



                        Case 9

                            tdInner = New TableCell
                            Title = "(%)"
                            LeftTdSetting(tdInner, "Extra-process " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "E_" + i.ToString()

                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, DWidth, "Right")
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("E" + i.ToString() + "").ToString(), 2)
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 10

                            tdInner = New TableCell
                            Title = "(%)"
                            LeftTdSetting(tdInner, "Weight " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "WT_" + i.ToString()

                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("P" + i.ToString() + "").ToString(), 1)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                ' trGHGP.Controls.Add(tdInner)
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 11
                            tdInner = New TableCell
                            Title = ""
                            Dim dsMat As New DataSet
                            LeftTdSetting(tdInner, "Mfg. Dept. " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "MD_" + i.ToString()

                            For k = 0 To DataCnt
                                'Ship Distance Sugg
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, DWidth, "Right")
                                dsMat = objGetData.GetDept(dstbl.Tables(k).Rows(0).Item("D" + i.ToString() + "").ToString(), "", "", arrCaseID(k))
                                If dsMat.Tables(0).Rows.Count > 0 Then
                                    tdInner.Text = dsMat.Tables(0).Rows(0).Item("PROCDE").ToString()
                                Else
                                    tdInner.Text = "Dept. Conflict"
                                End If
                                trInner.Controls.Add(tdInner)

                            Next
                    End Select
                    tblComparision.Controls.Add(trInner)
                Next
            Next



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
                            LeftTdSetting(tdInner, "Printing Plates", trInner, "AlterNateColor1")
                            trInner.ID = "PCMQ1"
                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                cnt = dstbl.Tables(k).Rows(0).Item("plate")
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
            _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
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
    Protected Sub GetSustain1Details(ByRef lbl As Label, ByVal CAseId As String, ByVal CaseIdE1 As Integer, ByVal UserId As String, ByVal dstblSustain1 As DataSet)
        Dim Ds As New DataSet
        Dim ObjGetdata As New S2GetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty
        Dim dvSustain1 As New DataView
        Dim dtSustain1 As New DataTable
        Try
            'Ds = ObjGetdata.GetSustain1Cases(CaseIdE1, CAseId, "", "", UserId)
            dvSustain1 = dstblSustain1.Tables(CAseId).DefaultView
            dvSustain1.RowFilter = "CASEID=" + CaseIdE1.ToString()
            dtSustain1 = dvSustain1.ToTable()

            If dtSustain1.Rows.Count = 0 Then
                lbl.Text = "Conflict"
                lbl.ForeColor = Drawing.Color.DarkRed
            Else
                lbl.Text = dtSustain1.Rows(0).Item("CASDES").ToString()
            End If

        Catch ex As Exception
            ErrorLable.Text = "Error:GetSustain1Details:" + ex.Message.ToString() + ""
        End Try
    End Sub
End Class
