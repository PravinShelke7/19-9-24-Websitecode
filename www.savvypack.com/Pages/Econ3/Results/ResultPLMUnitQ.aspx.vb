Imports System.Data
Imports System.Data.OleDb
Imports System
Imports E3GetData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Web.UI.HtmlTextWriter
Imports System.Activities.Expressions
Imports System.Runtime.InteropServices
Partial Class Pages_Econ3_Results_ResultPLMUnitQ
    Inherits System.Web.UI.Page
    Dim DateCount As String = ""

#Region "Get Set Variables"
    Dim _lErrorLble As Label
    Dim _strUserName As String
    Dim _strPassword As String
    Dim _iAssumptionId As Integer
    Dim _btnUpdate As ImageButton
    Dim _btnCompare As ImageButton


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
    Public Property Comparebtn() As ImageButton
        Get
            Return _btnCompare
        End Get
        Set(ByVal value As ImageButton)
            _btnCompare = value
        End Set
    End Property


    Public DataCnt As Integer
    Public CaseDesp As New ArrayList
    Public CaseDesp1 As New ArrayList

#End Region

#Region "MastePage Content Variables"
    Protected Sub GetErrorLable()
        ErrorLable = Page.Master.FindControl("lblError")
    End Sub

    Protected Sub GetUpdatebtn()
        Updatebtn = Page.Master.FindControl("imgUpdate")
        Updatebtn.Visible = False

    End Sub
    Protected Sub GetComparebtn()
        Dim obj As New CryptoHelper()
        Comparebtn = Page.Master.FindControl("imgCompare")
        Comparebtn.Visible = False
        Dim objCryptoHelper As New CryptoHelper()
        Comparebtn.OnClientClick = "return Compare('../../../Charts/E3Charts/CPrftCost.aspx?CaseId=" + obj.Encrypt(AssumptionId) + "&PrftCost=" + objCryptoHelper.Encrypt("PRFT") + "&ChartType=" + objCryptoHelper.Encrypt("RBC") + "&IsDep=" + objCryptoHelper.Encrypt("N") + "&CType=" + objCryptoHelper.Encrypt("Total") + "') "

    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Try
                GetErrorLable()
                GetUpdatebtn()
                GetSessionDetails()
                GetComparebtn()
                'GetPageDetails()
                GridDet()
                If Not IsPostBack Then
			hidSortId.Value = "0"
                    If Request.Cookies("W1") Is Nothing Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Layers", "removeSession('SSS');", True)
                    Else
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Layers", "removeSession('" + Request.Cookies("W1").Value + "');", True)
                    End If
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

            'lblAID.Text = AssumptionId
            'lblAdes.Text = Session("Description")

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetSessionDetails:" + ex.Message.ToString()
        End Try
    End Sub

#Region "Grid"
    Public Sub GridDet()
        Dim objGetData As New E1GetData.Selectdata()
        Dim groupId As Integer
        Dim dsCol As New DataSet
        Dim dsEquip As New DataSet
        Dim dt As New DataTable
        Dim dsEqid As New DataSet
        Dim CountArrB(15) As String
        Dim j As New Integer
        'Dim DateCount As String = ""
        Dim GrpId(1) As String
        Dim grpComma As String
        Dim QCol As String = "5"
        Try
            GrpId = objGetData.GetGrpByUserId(Session("UserId").ToString())
            grpComma = String.Join(",", GrpId)
            j = 0
            For i = 1 To 12
                CountArrB(j) = DateAdd("m", +(i), DateSerial(2022, 12, 1)).ToString("MM/dd/yyyy")
                j = j + 1
                DateCount = j
            Next

            dsEqid = objGetData.GetPackSpecGrpDataUNITQuarterly(grpComma, CountArrB, DateCount, Session("USerId"))
            If dsEqid.Tables(0).Rows.Count > 0 Then

                Session("PLMData") = dsEqid
                grdMaterials.Columns.Clear()
                grid.Visible = True

                Dim bField As New BoundField()
                bField.HeaderText = "Packspec Group"
 bField.ItemStyle.Width = 150
                bField.SortExpression = "D1"
                grdMaterials.Columns.Add(bField)
                Dim bField1 As New BoundField()
                bField1.HeaderText = "P&L Item"
                bField1.ItemStyle.Width = 150
                bField1.SortExpression = "D2"
                grdMaterials.Columns.Add(bField1)
                Dim bField2 As New BoundField()
                bField2.HeaderText = "Currency"
                bField2.ItemStyle.Width = 150
                bField2.SortExpression = "D3"
                grdMaterials.Columns.Add(bField2)
                Dim bField3 As New BoundField()
                bField3.HeaderText = "Unit"
                bField3.ItemStyle.Width = 150
                bField3.SortExpression = "D4"
                grdMaterials.Columns.Add(bField3)

                For i = 0 To DateCount - 1
                    Dim boundField As New BoundField()
                    If (i Mod 3 = 0) Then
                        boundField.HeaderText = CDate(CountArrB(i)).ToString("MMM") + "-" + CDate(CountArrB(i + 2)).ToString("MMM, yyyy")
                        boundField.ItemStyle.Width = 150
                        boundField.SortExpression = "D" + QCol.ToString() + ""
boundField.ItemStyle.HorizontalAlign = HorizontalAlign.Right
                        QCol = QCol + 1
                        grdMaterials.Columns.Add(boundField)
                    End If

                Next
                grdMaterials.DataSource = dsEqid
                grdMaterials.DataBind()
                BindGrid()
            Else
                grid.Visible = False

            End If


        Catch ex As Exception
            _lErrorLble.Text = "Error:Link_Click:" + ex.Message.ToString()
        End Try
    End Sub
    Private Sub BindGrid()
        Try
            Dim ds As New DataSet()
            Dim dv As New DataView
            Dim dt As New DataTable
            Dim dsEqid As New DataSet
            Dim dvEqid As New DataView
            Dim dtEqid As New DataTable
            Dim dsCol As New DataSet

            dsEqid = CType(Session("PLMData"), DataSet)
            dv = dsEqid.Tables(0).DefaultView()

            For Each Gr As GridViewRow In grdMaterials.Rows

                Gr.Cells(0).Text = dsEqid.Tables(0).Rows(Gr.RowIndex).Item("D1").ToString()
                Gr.Cells(1).Text = dsEqid.Tables(0).Rows(Gr.RowIndex).Item("D2").ToString()
                Gr.Cells(2).Text = dsEqid.Tables(0).Rows(Gr.RowIndex).Item("D3").ToString()
                Gr.Cells(3).Text = dsEqid.Tables(0).Rows(Gr.RowIndex).Item("D4").ToString()
                Dim QCol As String = "5"

                For i = 0 To DateCount - 1
                    If (i Mod 3 = 0) Then
                        If dsEqid.Tables(0).Rows(Gr.RowIndex).Item("D" + QCol.ToString()).ToString() <> "" Then
                            Gr.Cells(QCol - 1).Text = FormatNumber(dsEqid.Tables(0).Rows(Gr.RowIndex).Item("D" + QCol.ToString()).ToString(), 4)
                        End If
                        QCol = QCol + 1
                    End If

                Next
            Next
        Catch ex As Exception
            _lErrorLble.Text = "Error:BindGrid:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub grdMaterials_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdMaterials.Sorting
        Try
            Dim Dts As New DataSet
            Dim ds As New DataSet
            Dim dv As New DataView
            Dim dt As New DataTable
            Dim numberDiv As Integer
            numberDiv = Convert.ToInt16(hidSortId.Value.ToString())
            Dts = Session("PLMData")
            dv = Dts.Tables(0).DefaultView

            If ((numberDiv Mod 2) = 0) Then
                dv.Sort = e.SortExpression + " " + "DESC"
            Else
                dv.Sort = e.SortExpression + " " + "ASC"
            End If
            numberDiv += 1
            hidSortId.Value = numberDiv.ToString()
            grdMaterials.DataSource = dv
            grdMaterials.DataBind()
            ds.Tables.Add(dv.ToTable())
            Session("PLMData") = ds
            BindGrid()


        Catch ex As Exception

        End Try
    End Sub
#End Region

    Protected Function GetCaseIds(ByVal GrpId As String) As String()
        Dim CaseIds(0) As String
        Dim objGetData As New E3GetData.Selectdata
        Try
            CaseIds = objGetData.GetCasesByPLMGrp(GrpId.ToString(), Session("UserId"))
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
        Dim i As New Integer
        Dim j As New Integer
        Dim k As New Integer
        Dim DWidth As String = String.Empty
        Dim arrCaseID() As String
        Dim CountArrB(15) As String
        Dim count As Integer
        Dim GrpId(1) As String
        Try
            GrpId = objGetData.GetRAPACKSPECGRP()
            DataCnt = GrpId.Length - 1


            Dim trHeader As New TableRow
            Dim tdHeader As TableCell
            Dim tdInner As TableCell
            Dim trInner As New TableRow
            Dim ddl As DropDownList
            Dim lbl As New Label
            Dim txt As TextBox
            Dim Cunits As New Integer
            Dim Units As New Integer
            Dim Title As String = String.Empty
            Dim CurrTitle As String = String.Empty
            Dim CCurrTitle As String = String.Empty
            Dim UnitError As String = String.Empty
            Dim CurrError As String = String.Empty
            Dim strText As String = String.Empty
            Dim strColor As String = String.Empty
            Dim dsnew As DataSet
            Dim dsGrp As DataSet
            dsnew = objGetData.GetLine_Item()

            Dim DateCount As String = ""
            j = 0
            For i = 1 To 12
                CountArrB(j) = DateAdd("m", +(i), DateSerial(2022, 12, 1)).ToString("MM/dd/yyyy")
                j = j + 1
                DateCount = j
            Next
            'header
            For j = 0 To 4
                If j = 0 Then
                    tdHeader = New TableCell
                    HeaderTdSetting(tdHeader, "150px", "PACKSPEC Group #", 1)
                    trHeader.Controls.Add(tdHeader)
                    trHeader.Height = 20
                    trHeader.CssClass = "PageSSHeading"
                ElseIf j = 1 Then
                    tdHeader = New TableCell
                    HeaderTdSetting(tdHeader, "150px", "P&L Line Item", 1)
                    trHeader.Controls.Add(tdHeader)
                    trHeader.Height = 20
                    trHeader.CssClass = "PageSSHeading"
                ElseIf j = 2 Then
                    tdHeader = New TableCell
                    HeaderTdSetting(tdHeader, "60px", "Currency", 1)
                    trHeader.Controls.Add(tdHeader)
                    trHeader.Height = 20
                    trHeader.CssClass = "PageSSHeading"
                ElseIf j = 3 Then
                    tdHeader = New TableCell
                    HeaderTdSetting(tdHeader, "60px", "Units", 1)
                    trHeader.Controls.Add(tdHeader)
                    trHeader.Height = 20
                    trHeader.CssClass = "PageSSHeading"
                Else
                    For i = 0 To DateCount.ToString() - 1
                        'tdHeader = New TableCell
                        'Dim Headertext As String = String.Empty
                        'Headertext = CountArrB(i)
                        'HeaderTdSetting(tdHeader, "100px", Headertext, 1)
                        'trHeader.Controls.Add(tdHeader)
                        If (i Mod 3 = 0) Then
                            tdHeader = New TableCell
                            Dim Headertext As String = String.Empty
                            'Headertext = CountArrB(i) + " to " + CountArrB(i + 2)
                            Headertext = CDate(CountArrB(i)).ToString("MMM") + "-" + CDate(CountArrB(i + 2)).ToString("MMM, yyyy")
                            HeaderTdSetting(tdHeader, "80px", Headertext, 1)
                            trHeader.Controls.Add(tdHeader)
                        End If
                    Next
                End If
            Next

            'tblComparision.Controls.Add(trHeader)
            'inner
            For s = 0 To GrpId.Length - 1
                ' CaseDesp.Add(GrpId(s))
                arrCaseID = GetCaseIds(GrpId(s))
                count = arrCaseID.Length - 1

                dsGrp = objGetData.GetPackSpecGrpById(GrpId(s))
                CaseDesp.Add(dsGrp.Tables(0).Rows(0).Item("GRPDETAIL").ToString())


                For i = 0 To count
                    Dim ds As New DataSet
                    ds = objGetData.GetResultWeightDetails(arrCaseID(i))
                    ds.Tables(0).TableName = arrCaseID(i).ToString()
                    dstbl.Tables.Add(ds.Tables(arrCaseID(i).ToString()).Copy())
                Next



                For l = 0 To 0 'dsnew.Tables(0).Rows.Count - 1

                    CaseDesp1.Add(dstbl.Tables(arrCaseID(0)).Rows(0).Item("PDES" + (l + 1).ToString()).ToString())

                    trInner = New TableRow()
                    trInner.ID = "PACK" + (l + 1).ToString() + "_" + s.ToString()
                    For k = 0 To 4
                        tdInner = New TableCell
                        If k = 0 Then
                            lbl = New Label
                            lbl.Text = "<b>" + dsGrp.Tables(0).Rows(0).Item("GRPDETAIL").ToString() + ""
                            lbl.Width = 150
                            tdInner.Controls.Add(lbl)
                            InnerTdSetting(tdInner, "150px", "Left")
                            tdInner.Style.Add("padding-right", "5px")
                            trInner.Controls.Add(tdInner)
                        ElseIf k = 1 Then
                            lbl = New Label
                            lbl.Text = "<b>" + dstbl.Tables(arrCaseID(0)).Rows(0).Item("PDES" + (l + 1).ToString()).ToString() + "</b>"
                            lbl.Width = 150
                            tdInner.Controls.Add(lbl)
                            InnerTdSetting(tdInner, "150px", "Left")
                            tdInner.Style.Add("padding-right", "5px")
                            trInner.Controls.Add(tdInner)
                        ElseIf k = 2 Then
                            tdInner.Text = dstbl.Tables(arrCaseID(0)).Rows(0).Item("TITLE2").ToString()
                            InnerTdSetting(tdInner, "100px", "Left")
                            tdInner.Style.Add("padding-right", "5px")
                            trInner.Controls.Add(tdInner)
                        ElseIf k = 3 Then
                            tdInner.Text = dstbl.Tables(arrCaseID(0)).Rows(0).Item("SUNITLBL").ToString()
                            InnerTdSetting(tdInner, "80px", "Left")
                            tdInner.Style.Add("padding-right", "5px")
                            trInner.Controls.Add(tdInner)
                        Else
                            For i = 0 To count
                                'tdInner = New TableCell
                                'If dstbl.Tables(i).Rows(0).Item("PL" + (l + 1).ToString()).ToString() <> "" Then
                                'FormatNumber((CDbl(dstbl.Tables(arrCaseID(i)).Rows(0).Item("PL" + (l + 1).ToString()).ToString()) * CDbl(dstbl.Tables(arrCaseID(i)).Rows(0).Item("CURR").ToString())) / (CDbl(dstbl.Tables(arrCaseID(i)).Rows(0).Item("SUNITVAL").ToString())), 4)
                                'End If
                                'InnerTdSetting(tdInner, "100px", "Right")
                                'tdInner.Style.Add("padding-left", "5px")
                                'trInner.Controls.Add(tdInner)
                                Dim cost1 As Double
                                Dim cost2 As Double
                                Dim cost3 As Double
                                Dim Avg As Double
                                If (i Mod 3 = 0) Then
                                    If i <= count Then
                                        If dstbl.Tables(arrCaseID(i)).Rows(0).Item("PL" + (l + 1).ToString()).ToString() <> "" Then
                                            cost1 = FormatNumber((CDbl(dstbl.Tables(arrCaseID(i)).Rows(0).Item("PL" + (l + 1).ToString()).ToString()) * CDbl(dstbl.Tables(arrCaseID(i)).Rows(0).Item("CURR").ToString())) / (CDbl(dstbl.Tables(arrCaseID(i)).Rows(0).Item("SUNITVAL").ToString())), 4)
                                        End If
                                    End If
                                    If (i + 1) <= count Then
                                        If dstbl.Tables(arrCaseID(i + 1)).Rows(0).Item("PL" + (l + 1).ToString()).ToString() <> "" Then
                                            cost2 = FormatNumber((CDbl(dstbl.Tables(arrCaseID(i + 1)).Rows(0).Item("PL" + (l + 1).ToString()).ToString()) * CDbl(dstbl.Tables(arrCaseID(i + 1)).Rows(0).Item("CURR").ToString())) / (CDbl(dstbl.Tables(arrCaseID(i + 1)).Rows(0).Item("SUNITVAL").ToString())), 4)
                                        End If
                                    End If
                                    If (i + 2) <= count Then
                                        If dstbl.Tables(arrCaseID(i + 2)).Rows(0).Item("PL" + (l + 1).ToString()).ToString() <> "" Then
                                            cost3 = FormatNumber((CDbl(dstbl.Tables(arrCaseID(i + 2)).Rows(0).Item("PL" + (l + 1).ToString()).ToString()) * CDbl(dstbl.Tables(arrCaseID(i + 2)).Rows(0).Item("CURR").ToString())) / (CDbl(dstbl.Tables(arrCaseID(i + 2)).Rows(0).Item("SUNITVAL").ToString())), 4)
                                        End If
                                    End If
                                    Avg = (cost1 + cost2 + cost3) / 3
                                    tdInner = New TableCell
                                    tdInner.Text = FormatNumber(Avg, 3)
                                    InnerTdSetting(tdInner, "80", "Right")
                                    tdInner.Style.Add("padding-left", "5px")
                                    trInner.Controls.Add(tdInner)
                                End If
                            Next
                        End If
                    Next
                    If (s Mod 2 = 0) Then
                        trInner.CssClass = "AlterNateColor1"
                    Else
                        trInner.CssClass = "AlterNateColor2"
                    End If
                    ' tblComparision.Controls.Add(trInner)
                Next
            Next
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub BindLineItem(ByVal ddl1 As DropDownList)
        Dim objGetData As New E1GetData.Selectdata

        Dim dsNew As New DataSet
        Try
            dsNew = objGetData.GetLine_Item()
            ddl1.ID = "ID"
            'Binding Dropdown
            With ddl1
                .DataSource = dsNew
                .DataTextField = "Line_Item"
                .DataValueField = "ID"
                .DataBind()
            End With
            ddl1.SelectedValue = 1

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
                Td.Style.Add("padding-right", "18px")
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
