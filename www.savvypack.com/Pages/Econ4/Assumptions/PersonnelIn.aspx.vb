Imports System.Data
Imports System.Data.OleDb
Imports System
Imports E4GetData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Econ4_Assumptions_PersonnelIn
    Inherits System.Web.UI.Page

#Region "Get Set Variables"
    Dim _lErrorLble As Label
    Dim _strUserName As String
    Dim _strPassword As String
    Dim _iAssumptionId As Integer
    Dim _btnUpdate As ImageButton


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
        Dim CaseIds As String = String.Empty
        'Dim DataCnt As New Integer
        Dim i As New Integer
        Dim j As New Integer
        Dim k As New Integer
        Dim DWidth As String = String.Empty
        Dim arrCaseID() As String
        Dim dsCost As New DataSet
        Dim dsDept As New DataSet
        Dim dsTblPos As New DataSet()
        Try
            dsCost = objGetData.GetCostTypeInfo("-1", "")
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
            Dim Cunits As New Integer
            Dim Units As New Integer
            Dim CurrTitle As String = String.Empty
            Dim CCurrTitle As String = String.Empty
            Dim Title As String = String.Empty
            Dim UnitError As String = String.Empty
            Dim CurrError As String = String.Empty
           
            DWidth = txtDWidth.Text + "px"
           

            tdHeader = New TableCell
            HeaderTdSetting(tdHeader, "200px", "<img alt='' src='../../Images/spacer.gif' Style='width:200px;height:0px;'  />", 1)
            trHeader.Controls.Add(tdHeader)
            trHeader.Height = 20
            trHeader.CssClass = "PageSSHeading"

            For i = 0 To DataCnt
                Dim ds As New DataSet
               
                ds = objGetData.GetPersonnelInDetails(arrCaseID(i), "")
                ds.Tables(0).TableName = arrCaseID(i).ToString()
                dstbl.Tables.Add(ds.Tables(arrCaseID(i).ToString()).Copy())

                'Personnel Position
              
                ds = objGetData.GetPersonnelInfoE3(arrCaseID(i), "-1", "", "", "")
                ds.Tables(0).TableName = arrCaseID(i).ToString()
                dsTblPos.Tables.Add(ds.Tables(arrCaseID(i).ToString()).Copy())
            Next





            For i = 0 To DataCnt
                dsCaseDetails = objGetData.GetCaseDetails(arrCaseID(i).ToString())
                Cunits = Convert.ToInt32(dstbl.Tables(0).Rows(0).Item("Units").ToString())
                Units = Convert.ToInt32(dstbl.Tables(i).Rows(0).Item("Units").ToString())
                CCurrTitle = dstbl.Tables(0).Rows(0).Item("TITLE2").ToString().Trim()
                CurrTitle = dstbl.Tables(i).Rows(0).Item("TITLE2").ToString().Trim()


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
            For i = 1 To 30
                For j = 1 To 7
                    trInner = New TableRow()

                    Select Case j
                        Case 1
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "<b>Position " + i.ToString() + "</b>", trInner, "AlterNateColor4")
                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Center")
                                tdInner.Text = "&nbsp;"
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 2
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Position Description", trInner, "AlterNateColor1")
                            trInner.ID = "PD_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                               
                                GetPersonnelDetails(arrCaseID(k), lbl, dstbl.Tables(k).Rows(0).Item("PERSPOS" + i.ToString() + "").ToString(), "", dsTblPos)
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 3
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Number of Workers", trInner, "AlterNateColor2")
                            trInner.ID = "NOW_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                lbl.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("PERNUM" + i.ToString() + "").ToString(), 2)
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 4
                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("TITLE2").ToString() + "/year)"
                            LeftTdSetting(tdInner, "Salary & Benefits Sugg. " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "SBS_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                If dstbl.Tables(k).Rows(0).Item("SALS" + i.ToString() + "").ToString() <> "" Then
                                    lbl.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("SALS" + i.ToString() + "").ToString(), 0).ToString()
                                Else
                                    lbl.Text = dstbl.Tables(k).Rows(0).Item("SALS" + i.ToString() + "").ToString()
                                End If
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 5
                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("TITLE2").ToString() + "/year)"
                            LeftTdSetting(tdInner, "Salary & Benefits Pref. " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "SBP_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                lbl.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("SALPRE" + i.ToString() + "").ToString(), 0).ToString()
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 6
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Cost Type", trInner, "AlterNateColor2")
                            trInner.ID = "CT_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                GetCostTypeDetails(lbl, dstbl.Tables(k).Rows(0).Item("COSTTYPID" + i.ToString() + "").ToString(), dsCost)
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 7
                            tdInner = New TableCell
                            Title = ""
                            LeftTdSetting(tdInner, "Mfg. Department" + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "MD_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                GetDeptDetails(lbl, dstbl.Tables(k).Rows(0).Item("DEPID" + i.ToString() + "").ToString(), arrCaseID(k).ToString(), dsDept)
                                tdInner.Controls.Add(lbl)
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

    Protected Sub GetPersonnelDetails(ByVal CaseID As String, ByRef lbl As Label, ByVal PersId As Integer, ByVal COUNTRY As String, ByVal dsTblPos As DataSet)
        Dim Ds As New DataSet
        Dim ObjGetdata As New E2GetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty
        Dim dvEquip As New DataView
        Dim dtEquip As New DataTable
        Try

            'Ds = ObjGetdata.GetPersonnelInfo(PersId, "", "", COUNTRY)
            dvEquip = dsTblPos.Tables(CaseID.ToString()).DefaultView
            dvEquip.RowFilter = "persid = " + PersId.ToString() + "  AND CASEID=" + CaseID
            dtEquip = dvEquip.ToTable()

            lbl.Text = dtEquip.Rows(0).Item("persDES").ToString()
        Catch ex As Exception
            ErrorLable.Text = "Error:GetPersonnelDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetCostTypeDetails(ByRef lbl As Label, ByVal CostId As Integer, ByVal dsCost As DataSet)
        Dim Ds As New DataSet
        Dim ObjGetdata As New E2GetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty
        Dim dvCost As New DataView
        Dim dtCost As New DataTable
        Try
            'Ds = ObjGetdata.GetCostTypeInfo(CostId, "")
            dvCost = dsCost.Tables(0).DefaultView
            dvCost.RowFilter = "COSTID =" + CostId.ToString()
            dtCost = dvCost.ToTable()

            lbl.Text = dtCost.Rows(0).Item("costde1").ToString()
        Catch ex As Exception
            ErrorLable.Text = "Error:GetCostTypeDetails:" + ex.Message.ToString() + ""
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

End Class
