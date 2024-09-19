Imports Corda
Imports System.Web.UI.HtmlControls
Imports System.Data
Imports System.Data.OleDb
Imports System.Web.HttpBrowserCapabilities
Partial Class Charts_E3Charts_CPrftCost
    Inherits System.Web.UI.Page
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub


    'Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
    '    'CODEGEN: This method call is required by the Web Form Designer
    '    'Do not modify it using the code editor.
    '    InitializeComponent()
    'End Sub

#End Region

#Region "Get Set Variables"
    Dim _lErrorLble As Label
    Dim _strPrftCost As String
    Dim _strChartType As String
    Dim _strChartName As String
    Dim _strIsDep As String
    Dim _iAssumptionId As Integer
    Dim _PageName As String
    Dim _cnType As String

    Public Property ErrorLable() As Label
        Get
            Return _lErrorLble
        End Get
        Set(ByVal Value As Label)
            _lErrorLble = Value
        End Set
    End Property

    Public Property PrftCost() As String
        Get
            Return _strPrftCost
        End Get
        Set(ByVal value As String)
            _strPrftCost = value
        End Set
    End Property

    Public Property ChartType() As String
        Get
            Return _strChartType
        End Get
        Set(ByVal value As String)
            _strChartType = value
        End Set
    End Property

    Public Property ChartName() As String
        Get
            Return _strChartName
        End Get
        Set(ByVal value As String)
            _strChartName = value
        End Set
    End Property

    Public Property IsDep() As String
        Get
            Return _strIsDep
        End Get
        Set(ByVal value As String)
            _strIsDep = value
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
    Public Property PageName() As String
        Get
            Return _PageName
        End Get
        Set(ByVal value As String)
            _PageName = value
        End Set
    End Property
    Public Property CNType() As String
        Get
            Return _Cntype
        End Get
        Set(ByVal value As String)
            _Cntype = value
        End Set
    End Property


#End Region

#Region "MastePage Content Variables"

    Protected Sub GetMasterPageControls()
        GetErrorLable()
    End Sub

    Protected Sub GetErrorLable()
        ErrorLable = Page.Master.FindControl("lblError")
    End Sub


#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim dsData As New DataSet
        Dim dsPref As New DataSet
        Dim objCryptoHelper As New CryptoHelper()
        Dim CaseId1 As String = String.Empty
        Dim CaseId2 As String = String.Empty

        Dim ds As New DataSet
        Dim objE3GetData As New E3GetData.Selectdata
        Dim i As Integer = 0
        Dim flag = False
        Dim flagVal = True
        Dim message As String = String.Empty
        Dim dt As New DataTable

        Dim arrCaseID() As String
        Dim DataCnt As Integer

        Dim dtCatSet As New DataTable
        Try
            GetMasterPageControls()
            AssumptionId = Session("AssumptionID") ' objCryptoHelper.Decrypt(Request.QueryString("CaseId"))
            Try
                ChartType = objCryptoHelper.Decrypt(Request.QueryString("ChartType"))
            Catch ex As Exception
                ChartType = "RBC"
            End Try



            If Not IsPostBack Then
                If Request.QueryString("PrftCost") <> Nothing Then
                    PrftCost = objCryptoHelper.Decrypt(Request.QueryString("PrftCost"))
                    IsDep = objCryptoHelper.Decrypt(Request.QueryString("IsDep"))
                    CNType = objCryptoHelper.Decrypt(Request.QueryString("CType"))
                    If PrftCost = "PRFT" And IsDep = "N" Then
                        ddlPName.SelectedValue = "PFT"
                    ElseIf PrftCost = "PRFT" And IsDep = "Y" Then
                        ddlPName.SelectedValue = "PFTD"
                    ElseIf PrftCost = "COST" And IsDep = "N" Then
                        ddlPName.SelectedValue = "COST"
                    ElseIf PrftCost = "COST" And IsDep = "Y" Then
                        ddlPName.SelectedValue = "COSTD"
                    End If
                    ddlCnType.SelectedValue = CNType
                    Bindddl()
                Else
                    GetDetails()
                    Bindddl()
                End If
            Else
                If Session("PageName") <> ddlPName.SelectedValue Then
                    Bindddl()
                    BindCategorySet()
                Else
                    GetDetails()
                End If


            End If
            'Getting CaseIds
            arrCaseID = GetCaseIds()
            DataCnt = arrCaseID.Length - 1

            'Check for Undefined Category
            If ddlChartType.SelectedValue <> "SBAR" Then
                dsData = GetData(arrCaseID)
                dsPref = GetPref(Session("USERID"))
            Else
                If (ddlCategorySet.SelectedValue = "0" Or ddlCategorySet.SelectedValue = "") Then
                    dsData = GetData(arrCaseID)
                    dsPref = GetPref(Session("USERID"))
                Else
                    'Checking Category Set
                    dtCatSet = objE3GetData.GetCategorySetByID(ddlCategorySet.SelectedValue)
                    If dtCatSet.Rows.Count > 0 Then
                        dt = objE3GetData.GetCategoryBySet(ddlCategorySet.SelectedValue, "")
                        If dt.Rows.Count > 0 Then
                            For i = 0 To dt.Rows.Count - 1
                                ds = objE3GetData.GetCategoryItemBycategory(dt.Rows(i).Item("CATEGORYID").ToString())
                                If ds.Tables(0).Rows.Count > 0 Then
                                    flag = True
                                    Exit For
                                End If
                            Next
                        Else
                            flag = False
                        End If
                        If (Not flag) Then
                            flagVal = False
                            message = "Category Set " + ddlCategorySet.SelectedItem.Text + " is not fully defined.Please define it properly!!!."
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "New_WinCopy", "alert('" + message + "');", True)
                        Else
                            dsData = GetData(arrCaseID)
                            dsPref = GetPref(Session("USERID"))
                        End If
                    Else
                        flagVal = True
                        BindCategorySet()
                        dsData = GetData(arrCaseID)
                        dsPref = GetPref(Session("USERID"))
                    End If

                    

                End If
            End If
            'dsData = GetData(arrCaseID)
            ' dsPref = GetPref(Session("UserName"))
            GetPageTitle()



            If flagVal Then
                If ddlCnType.SelectedValue = "Total" Then
                    If PrftCost = "PRFT" Then
                        GetBPTotal(dsData.Tables(0), dsPref.Tables(0))
                    Else
                        GetBCTotal(dsData.Tables(0), dsPref.Tables(0))
                    End If
                ElseIf ddlCnType.SelectedValue = "PUnit" Then
                    If PrftCost = "PRFT" Then
                        GetBPPerUnit(dsData.Tables(0), dsPref.Tables(0))
                    Else
                        GetBCPerUnit(dsData.Tables(0), dsPref.Tables(0))
                    End If
                ElseIf ddlCnType.SelectedValue = "PWeight" Then
                    If PrftCost = "PRFT" Then
                        GetBPPerWeight(dsData.Tables(0), dsPref.Tables(0))
                    Else
                        GetBCPerWeight(dsData.Tables(0), dsPref.Tables(0))
                    End If
                End If
            End If
            
        Catch ex As Exception
            ErrorLable.Text = "Error:Page_Load:" + ex.Message.ToString() + ""
        End Try
    End Sub
    Protected Sub GetDetails()
        Try

            PageName = ddlPName.SelectedValue
            Session("PageName") = PageName
            If PageName = "PFT" Then
                PrftCost = "PRFT"
                IsDep = "N"
            ElseIf PageName = "PFTD" Then
                PrftCost = "PRFT"
                IsDep = "Y"
            ElseIf PageName = "COST" Then
                PrftCost = "COST"
                IsDep = "N"
            ElseIf PageName = "COSTD" Then
                PrftCost = "COST"
                IsDep = "Y"
            End If
        Catch ex As Exception

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
    Protected Sub GetPageTitle()
        Try
            If PrftCost = "PRFT" Then
                If ChartType = "RBC" Then
                    Page.Title = "E3-Combine Profit And Loss Bar Chart"
                    rwType.Visible = True
                Else
                    Page.Title = "E3-Combine Profit And Loss  Stack Chart"
                    rwType.Visible = False
                End If
            Else
                If ChartType = "RBC" Then
                    Page.Title = "E3-Combine Cost Bar Chart"
                    rwType.Visible = True
                Else
                    Page.Title = "E3-Combine Cost Stack Chart"
                    rwType.Visible = False
                End If
            End If
        Catch ex As Exception
            ErrorLable.Text = "Error:GetPageTitle:" + ex.Message.ToString() + ""
        End Try
    End Sub
    Protected Sub Bindddl()
        Dim ds As New DataSet
        Dim objGetData As New E1GetData.Selectdata()
        Try
            GetDetails()
            If PrftCost = "PRFT" Then
                ds = objGetData.GetChartProfitAndLoss(IsDep)
            Else
                ds = objGetData.GetChartCost(IsDep)
            End If

            With ddltype
                .DataSource = ds
                .DataTextField = "TYPEDES"
                .DataValueField = "TYPE"
                .DataBind()
            End With

        Catch ex As Exception
            ErrorLable.Text = "Error:BindDdl" + ex.Message.ToString()
        End Try
    End Sub
    Protected Function GetData(ByVal CaseIds() As String) As DataSet
        Dim ds As New DataSet
        Dim objGetData As New E3GetData.Selectdata()
        Try
            If PrftCost = "PRFT" Then
                ds = objGetData.GetChartProfitAndLossRes(CaseIds)
            Else
                ds = objGetData.GetChartCostRes(CaseIds)
            End If
            Return ds
        Catch ex As Exception
            ErrorLable.Text = "Error:GetData:" + ex.Message.ToString()
            Return ds
        End Try
    End Function
    Protected Function GetPref(ByVal USERID As String) As DataSet
        Dim ds As New DataSet
        Dim objGetData As New E1GetData.Selectdata()
        Try
            ds = objGetData.GetChartPrefrences(USERID)
            Return ds
        Catch ex As Exception
            ErrorLable.Text = "Error:GetPref:" + ex.Message.ToString()
            Return ds
        End Try
    End Function
    Protected Sub GetBPPerUnit(ByVal DsData As DataTable, ByVal DsPref As DataTable)
        Try

            Dim Data As String = String.Empty
            Dim Text As String = String.Empty
            Dim Pref As String = String.Empty
            Dim pcScript As String = String.Empty
            Dim Graphtype As String = String.Empty
            Dim GraphName As String = String.Empty
            Dim Cnt As New Integer
            Dim SalesVolumeUnit As New Decimal
            Dim VariableMargin As New Decimal
            Dim Revenue As New Decimal
            Dim PlantMargin As New Decimal
            Dim i As New Integer

            Dim matCost As New Decimal
            Dim labCost As New Decimal
            Dim ergyCost As New Decimal
            Dim DPCost As New Decimal
            Dim ShipCost As New Decimal
            Dim fixedCost As New Decimal
            Dim plantM As New Decimal
            Dim j As New Integer
            Cnt = DsData.Rows.Count - 1
            ''FOR UNIT DISPLAY
            Pref = DsPref.Rows(0).Item("Title6").ToString()
            j = 0
            For i = 0 To Cnt
                If (DsData.Rows(i).Item("FINVOLMSI") > 0) Then
                    If (j = 2) Then
                        j = 3 'SHOWS MSI & UNITS
                        Exit For
                    Else
                        j = 1 'SHOWS ALL ARE IN MSI
                    End If
                ElseIf (DsData.Rows(i).Item("FINVOLMUNITS") > 0) Then
                    If (j = 1) Then
                        j = 3 'SHOWS MSI & UNITS
                        Exit For
                    Else
                        j = 2 'SHOWS ALL ARE IN UNITS
                    End If
                End If
            Next

            If j = 1 Then
                Pref = Pref + "/" + DsPref.Rows(0).Item("TITLE3").ToString()
            ElseIf j = 2 Then
                Pref = Pref + "/unit "
            Else
                Pref = Pref + "/(unit or " + DsPref.Rows(0).Item("TITLE3").ToString() + ")"
            End If


            If ddlPName.SelectedValue = "PFT" Then
                lblHeading.Text = "Profit and Loss Page"
            Else
                lblHeading.Text = "Profit and Loss with Depreciation Page"
            End If

            If ddlChartType.SelectedValue = "RBC" Then
                lblHeadingS.Text = "(" + ddltype.SelectedItem.Text + " Chart" + ")"
                GraphName = "Sargento_KraftPouchvsTray"
                Graphtype = "graph"
                'Cnt = DsData.Rows.Count - 1
                'Pref = DsPref.Rows(0).Item("Title6").ToString() + "/unit"
                pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
                For i = 0 To Cnt
                    If DsData.Rows(i).Item("FINVOLMSI") > 0 Then
                        SalesVolumeUnit = DsData.Rows(i).Item("FINVOLMSI") * DsPref.Rows(0).Item("ConvArea")
                    ElseIf DsData.Rows(i).Item("FINVOLMUNITS") > 0 Then
                        SalesVolumeUnit = DsData.Rows(i).Item("FINVOLMUNITS")
                    End If
                    Data = FormatNumber((DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * DsPref.Rows(0).Item("curr")) * 100 / SalesVolumeUnit, 2)
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEID") & ";" & Data & ")"
                Next
            ElseIf ddlChartType.SelectedValue = "PIE" Then
                lblHeadingS.Text = "(" + ddltype.SelectedItem.Text + " Chart" + ")"
                GraphName = "Sargento_KraftPouchvsTray"
                Graphtype = "graph"
                'Cnt = DsData.Rows.Count - 1
                'Pref = "(" + DsPref.Rows(0).Item("Title6").ToString() + "/unit)"
                pcScript &= Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
                For i = 0 To Cnt
                    If DsData.Rows(i).Item("FINVOLMSI") > 0 Then
                        SalesVolumeUnit = DsData.Rows(i).Item("FINVOLMSI") * DsPref.Rows(0).Item("ConvArea")
                    ElseIf DsData.Rows(i).Item("FINVOLMUNITS") > 0 Then
                        SalesVolumeUnit = DsData.Rows(i).Item("FINVOLMUNITS")
                    End If
                    Data = FormatNumber((DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * DsPref.Rows(0).Item("curr")) * 100 / SalesVolumeUnit, 2)
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEID").ToString() + ":" + DsData.Rows(i).Item("CASEDE").ToString() & ";" & Data & ")"
                Next
            ElseIf ddlChartType.SelectedValue = "SBAR" Then
                lblHeadingS.Text = ""
                GraphName = "Sargento_KraftPouchvsTray"
                Graphtype = "graph"
                'Cnt = DsData.Rows.Count - 1
                'Pref = DsPref.Rows(0).Item("Title6").ToString() + "/unit"
                If (ddlCategorySet.SelectedValue = "0" Or ddlCategorySet.SelectedValue = "") Then
                    pcScript &= Graphtype + ".transposed(true)" + Graphtype + ".setCategories(Variable Material Cost;Variable Labor Cost;Variable Energy Cost;Dist. Pack. Energy;Shipping Cost;Fixed Cost;Plant Margin;)"
                    For i = 0 To Cnt
                        If DsData.Rows(i).Item("FINVOLMSI") > 0 Then
                            SalesVolumeUnit = DsData.Rows(i).Item("FINVOLMSI") * DsPref.Rows(0).Item("ConvArea")
                        ElseIf DsData.Rows(i).Item("FINVOLMUNITS") > 0 Then
                            SalesVolumeUnit = DsData.Rows(i).Item("FINVOLMUNITS")
                        End If

                        If SalesVolumeUnit <> 0 Then
                            matCost = FormatNumber(DsData.Rows(i).Item("VMATERIAL") * DsPref.Rows(0).Item("curr") * 100 / SalesVolumeUnit, 2)
                            labCost = FormatNumber(DsData.Rows(i).Item("VLABOR") * DsPref.Rows(0).Item("curr") * 100 / SalesVolumeUnit, 2)
                            ergyCost = FormatNumber(DsData.Rows(i).Item("VENERGY") * DsPref.Rows(0).Item("curr") * 100 / SalesVolumeUnit, 2)
                            DPCost = FormatNumber(DsData.Rows(i).Item("VPACK") * DsPref.Rows(0).Item("curr") * 100 / SalesVolumeUnit, 2)
                            ShipCost = FormatNumber(DsData.Rows(i).Item("VSHIP") * DsPref.Rows(0).Item("curr") * 100 / SalesVolumeUnit, 2)
                            If PrftCost = "PRFT" And IsDep = "N" Then
                                fixedCost = FormatNumber(DsData.Rows(i).Item("FIXEDCOST") * DsPref.Rows(0).Item("curr") * 100 / SalesVolumeUnit, 2)
                                plantM = FormatNumber(DsData.Rows(i).Item("PM") * DsPref.Rows(0).Item("curr") * 100 / SalesVolumeUnit, 2)
                            Else
                                fixedCost = FormatNumber(DsData.Rows(i).Item("FIXEDCOSTDEP") * DsPref.Rows(0).Item("curr") * 100 / SalesVolumeUnit, 2)
                                plantM = FormatNumber(DsData.Rows(i).Item("PMDEP") * DsPref.Rows(0).Item("curr") * 100 / SalesVolumeUnit, 2)
                            End If
                        End If

                        pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEID") & ";" & matCost & ";" & labCost & ";" & ergyCost & ";" & DPCost & ";" & ShipCost & ";" & fixedCost & ";" & plantM & ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(1," + (i + 1).ToString() + ",Variable Material Cost:" + matCost.ToString() + ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(2," + (i + 1).ToString() + ",Variable Labor Cost:" + labCost.ToString() + ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(3," + (i + 1).ToString() + ",Variable Energy Cost:" + ergyCost.ToString() + ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(4," + (i + 1).ToString() + ",Dist. Pack. Energy:" + DPCost.ToString() + ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(5," + (i + 1).ToString() + ",Shipping Cost:" + ShipCost.ToString() + ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(6," + (i + 1).ToString() + ",Fixed Cost:" + fixedCost.ToString() + ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(7," + (i + 1).ToString() + ",Plant Margin:" + plantM.ToString() + ")"
                    Next
                Else
                    pcScript = GetStackbarChartDetails(DsData, DsPref, "Unit")
                End If
            End If



            GenrateChart(pcScript, GraphName, Pref)

        Catch ex As Exception
            Response.Write("Error:GetPerUnit:" + ex.Message.ToString())
        End Try
    End Sub
    Protected Sub GetBPPerWeight(ByVal DsData As DataTable, ByVal DsPref As DataTable)
        Try
            Dim Data As String = String.Empty
            Dim Text As String = String.Empty
            Dim Pref As String = String.Empty
            Dim pcScript As String = String.Empty
            Dim Graphtype As String = String.Empty
            Dim GraphName As String = String.Empty
            Dim Cnt As New Integer
            Dim SalesVolumeLb As New Decimal
            Dim i As New Integer
            Dim matCost As New Decimal
            Dim labCost As New Decimal
            Dim ergyCost As New Decimal
            Dim DPCost As New Decimal
            Dim ShipCost As New Decimal
            Dim fixedCost As New Decimal
            Dim plantM As New Decimal

            If ddlPName.SelectedValue = "PFT" Then
                lblHeading.Text = "Profit and Loss Page"
            Else
                lblHeading.Text = "Profit and Loss with Depreciation Page"
            End If


            If ddlChartType.SelectedValue = "RBC" Then
                lblHeadingS.Text = "(" + ddltype.SelectedItem.Text + " Chart" + ")"
                GraphName = "Sargento_KraftPouchvsTray"
                Graphtype = "graph"
                Cnt = DsData.Rows.Count - 1
                Pref = DsPref.Rows(0).Item("Title2").ToString() + "/" + DsPref.Rows(0).Item("Title8").ToString()
                pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
                For i = 0 To Cnt
                    SalesVolumeLb = DsData.Rows(i).Item("VOLUME") * DsPref.Rows(0).Item("CONVWT")
                    If SalesVolumeLb <> 0 Then
                        Data = FormatNumber(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * DsPref.Rows(0).Item("curr") / SalesVolumeLb, 3)
                    Else
                        Data = 0
                    End If

                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEID") & ";" & Data & ")"
                Next
            ElseIf ddlChartType.SelectedValue = "PIE" Then
                lblHeadingS.Text = "(" + ddltype.SelectedItem.Text + " Chart" + ")"
                GraphName = "Sargento_KraftPouchvsTray"
                Graphtype = "graph"
                Cnt = DsData.Rows.Count - 1
                Pref = "(" + DsPref.Rows(0).Item("Title2").ToString() + "/" + DsPref.Rows(0).Item("Title8").ToString() + ")"
                pcScript &= Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
                For i = 0 To Cnt
                    SalesVolumeLb = DsData.Rows(i).Item("VOLUME") * DsPref.Rows(0).Item("CONVWT")
                    If SalesVolumeLb <> 0 Then
                        Data = FormatNumber(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * DsPref.Rows(0).Item("curr") / SalesVolumeLb, 3)
                    Else
                        Data = 0
                    End If

                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEID").ToString() + ":" + DsData.Rows(i).Item("CASEDE").ToString() & ";" & Data & ")"
                Next
            ElseIf ddlChartType.SelectedValue = "SBAR" Then
                lblHeadingS.Text = ""
                GraphName = "Sargento_KraftPouchvsTray"
                Graphtype = "graph"
                Cnt = DsData.Rows.Count - 1
                Pref = "(" + DsPref.Rows(0).Item("Title2").ToString() + "/" + DsPref.Rows(0).Item("Title8").ToString() + ")"
                If (ddlCategorySet.SelectedValue = "0" Or ddlCategorySet.SelectedValue = "") Then
                    pcScript &= Graphtype + ".transposed(true)" + Graphtype + ".setCategories(Variable Material Cost;Variable Labor Cost;Variable Energy Cost;Dist. Pack. Energy;Shipping Cost;Fixed Cost;Plant Margin;)"
                    For i = 0 To Cnt
                        SalesVolumeLb = DsData.Rows(i).Item("VOLUME") * DsPref.Rows(0).Item("CONVWT")
                        If SalesVolumeLb <> 0 Then
                            matCost = FormatNumber(DsData.Rows(i).Item("VMATERIAL") * DsPref.Rows(0).Item("curr") / SalesVolumeLb, 3)
                            labCost = FormatNumber(DsData.Rows(i).Item("VLABOR") * DsPref.Rows(0).Item("curr") / SalesVolumeLb, 3)
                            ergyCost = FormatNumber(DsData.Rows(i).Item("VENERGY") * DsPref.Rows(0).Item("curr") / SalesVolumeLb, 3)
                            DPCost = FormatNumber(DsData.Rows(i).Item("VPACK") * DsPref.Rows(0).Item("curr") / SalesVolumeLb, 3)
                            ShipCost = FormatNumber(DsData.Rows(i).Item("VSHIP") * DsPref.Rows(0).Item("curr") / SalesVolumeLb, 3)
                            If PrftCost = "PRFT" And IsDep = "N" Then
                                fixedCost = FormatNumber(DsData.Rows(i).Item("FIXEDCOST") * DsPref.Rows(0).Item("curr") / SalesVolumeLb, 3)
                                plantM = FormatNumber(DsData.Rows(i).Item("PM") * DsPref.Rows(0).Item("curr") / SalesVolumeLb, 3)
                            Else
                                fixedCost = FormatNumber(DsData.Rows(i).Item("FIXEDCOSTDEP") * DsPref.Rows(0).Item("curr") / SalesVolumeLb, 3)
                                plantM = FormatNumber(DsData.Rows(i).Item("PMDEP") * DsPref.Rows(0).Item("curr") / SalesVolumeLb, 3)
                            End If
                        Else
                            matCost = 0
                            labCost = 0
                            ergyCost = 0
                            DPCost = 0
                            ShipCost = 0
                            fixedCost = 0
                            plantM = 0
                        End If



                        pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEID") & ";" & matCost & ";" & labCost & ";" & ergyCost & ";" & DPCost & ";" & ShipCost & ";" & fixedCost & ";" & plantM & ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(1," + (i + 1).ToString() + ",Variable Material Cost:" + matCost.ToString() + ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(2," + (i + 1).ToString() + ",Variable Labor Cost:" + labCost.ToString() + ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(3," + (i + 1).ToString() + ",Variable Energy Cost:" + ergyCost.ToString() + ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(4," + (i + 1).ToString() + ",Dist. Pack. Energy:" + DPCost.ToString() + ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(5," + (i + 1).ToString() + ",Shipping Cost:" + ShipCost.ToString() + ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(6," + (i + 1).ToString() + ",Fixed Cost:" + fixedCost.ToString() + ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(7," + (i + 1).ToString() + ",Plant Margin:" + plantM.ToString() + ")"
                    Next
                Else
                    pcScript = GetStackbarChartDetails(DsData, DsPref, "Weight")
                End If
            End If


            GenrateChart(pcScript, GraphName, Pref)
        Catch ex As Exception
            Response.Write("Error:GetPerWeight:" + ex.Message.ToString())
        End Try
    End Sub
    Protected Sub GetBPTotal(ByVal DsData As DataTable, ByVal DsPref As DataTable)
        Try
            Dim Data As String = String.Empty
            Dim Text As String = String.Empty
            Dim Pref As String = String.Empty
            Dim pcScript As String = String.Empty
            Dim Graphtype As String = String.Empty
            Dim GraphName As String = String.Empty
            Dim VariableMargin As New Decimal
            Dim Revenue As New Decimal
            Dim PlantMargin As New Decimal
            Dim Cnt As New Integer
            Dim i As New Integer

            Dim matCost As New Decimal
            Dim labCost As New Decimal
            Dim ergyCost As New Decimal
            Dim DPCost As New Decimal
            Dim ShipCost As New Decimal
            Dim fixedCost As New Decimal
            Dim plantM As New Decimal

            If ddlPName.SelectedValue = "PFT" Then
                lblHeading.Text = "Profit and Loss Page"
            Else
                lblHeading.Text = "Profit and Loss with Depreciation Page"
            End If


            If ddlChartType.SelectedValue = "RBC" Then
                lblHeadingS.Text = "(" + ddltype.SelectedItem.Text + " Chart" + ")"
                GraphName = "Sargento_KraftPouchvsTray"
                Graphtype = "graph"
                Cnt = DsData.Rows.Count - 1
                Pref = DsPref.Rows(0).Item("TITLE2").ToString() + ""
                pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
                For i = 0 To Cnt
                    Data = FormatNumber(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * DsPref.Rows(0).Item("curr"), 0)
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEID") & ";" & Data & ")"

                Next
            ElseIf ddlChartType.SelectedValue = "PIE" Then
                lblHeadingS.Text = "(" + ddltype.SelectedItem.Text + " Chart" + ")"
                GraphName = "Sargento_KraftPouchvsTray"
                Graphtype = "graph"
                Cnt = DsData.Rows.Count - 1
                Pref = "(" + DsPref.Rows(0).Item("TITLE2").ToString() + ") "
                pcScript &= Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
                For i = 0 To Cnt

                    Data = FormatNumber(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * DsPref.Rows(0).Item("curr"), 0)
                    pcScript &= "" + Graphtype + ".setSeries(" + DsData.Rows(i).Item("CASEDE").ToString() & ";" & Data & ")"
                Next
            ElseIf ddlChartType.SelectedValue = "SBAR" Then
                lblHeadingS.Text = ""
                GraphName = "Sargento_KraftPouchvsTray"
                Graphtype = "graph"
                Cnt = DsData.Rows.Count - 1
                Pref = "(" + DsPref.Rows(0).Item("TITLE2").ToString() + ") "
                If (ddlCategorySet.SelectedValue = "0" Or ddlCategorySet.SelectedValue = "") Then
                    pcScript &= Graphtype + ".transposed(true)" + Graphtype + ".setCategories(Variable Material Cost;Variable Labor Cost;Variable Energy Cost;Dist. Pack. Energy;Shipping Cost;Fixed Cost;Plant Margin;)"
                    For i = 0 To Cnt
                        matCost = FormatNumber(DsData.Rows(i).Item("VMATERIAL") * DsPref.Rows(0).Item("curr"), 0)
                        labCost = FormatNumber(DsData.Rows(i).Item("VLABOR") * DsPref.Rows(0).Item("curr"), 0)
                        ergyCost = FormatNumber(DsData.Rows(i).Item("VENERGY") * DsPref.Rows(0).Item("curr"), 0)
                        DPCost = FormatNumber(DsData.Rows(i).Item("VPACK") * DsPref.Rows(0).Item("curr"), 0)
                        ShipCost = FormatNumber(DsData.Rows(i).Item("VSHIP") * DsPref.Rows(0).Item("curr"), 0)

                        If PrftCost = "PRFT" And IsDep = "N" Then
                            fixedCost = FormatNumber(DsData.Rows(i).Item("FIXEDCOST") * DsPref.Rows(0).Item("curr"), 0)
                            plantM = FormatNumber(DsData.Rows(i).Item("PM") * DsPref.Rows(0).Item("curr"), 0)
                        Else
                            fixedCost = FormatNumber(DsData.Rows(i).Item("FIXEDCOSTDEP") * DsPref.Rows(0).Item("curr"), 0)
                            plantM = FormatNumber(DsData.Rows(i).Item("PMDEP") * DsPref.Rows(0).Item("curr"), 0)
                        End If


                        pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEID") & ";" & matCost & ";" & labCost & ";" & ergyCost & ";" & DPCost & ";" & ShipCost & ";" & fixedCost & ";" & plantM & ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(1," + (i + 1).ToString() + ",Variable Material Cost:" + matCost.ToString() + ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(2," + (i + 1).ToString() + ",Variable Labor Cost:" + labCost.ToString() + ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(3," + (i + 1).ToString() + ",Variable Energy Cost:" + ergyCost.ToString() + ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(4," + (i + 1).ToString() + ",Dist. Pack. Energy:" + DPCost.ToString() + ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(5," + (i + 1).ToString() + ",Shipping Cost:" + ShipCost.ToString() + ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(6," + (i + 1).ToString() + ",Fixed Cost:" + fixedCost.ToString() + ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(7," + (i + 1).ToString() + ",Plant Margin:" + plantM.ToString() + ")"
                    Next
                Else
                    pcScript = GetStackbarChartDetails(DsData, DsPref, "Total")
                End If
            End If
            GenrateChart(pcScript, GraphName, Pref)

        Catch ex As Exception
            Response.Write("Error:GetTotal:" + ex.Message.ToString())
        End Try
    End Sub
    Protected Sub GetBCPerUnit(ByVal DsData As DataTable, ByVal DsPref As DataTable)
        Try

            Dim Data As String = String.Empty
            Dim Text As String = String.Empty
            Dim Pref As String = String.Empty
            Dim pcScript As String = String.Empty
            Dim Graphtype As String = String.Empty
            Dim GraphName As String = String.Empty
            Dim Cnt As New Integer
            Dim SalesVolumeUnit As New Decimal
            Dim VariableCost As New Decimal
            Dim FixedCost As New Decimal
            Dim TotalCost As New Decimal
            Dim i As New Integer
            Dim matCost As New Decimal
            Dim labCost As New Decimal
            Dim ergyCost As New Decimal
            Dim DPCost As New Decimal
            Dim ShipCost As New Decimal

            GraphName = "Sargento_KraftPouchvsTray"
            Graphtype = "graph"
            'Cnt = DsData.Rows.Count - 1
            'Pref = DsPref.Rows(0).Item("Title6").ToString() + "/unit"
            Cnt = DsData.Rows.Count - 1
            Dim j As New Integer
            ''FOR UNIT DISPLAY
            Pref = DsPref.Rows(0).Item("Title6").ToString()
            j = 0
            For i = 0 To Cnt
                If (DsData.Rows(i).Item("FINVOLMSI") > 0) Then
                    If (j = 2) Then
                        j = 3 'SHOWS MSI & UNITS
                        Exit For
                    Else
                        j = 1 'SHOWS ALL ARE IN MSI
                    End If
                ElseIf (DsData.Rows(i).Item("FINVOLMUNITS") > 0) Then
                    If (j = 1) Then
                        j = 3 'SHOWS MSI & UNITS
                        Exit For
                    Else
                        j = 2 'SHOWS ALL ARE IN UNITS
                    End If
                End If
            Next

            If j = 1 Then
                Pref = Pref + "/" + DsPref.Rows(0).Item("TITLE3").ToString()
            ElseIf j = 2 Then
                Pref = Pref + "/unit "
            Else
                Pref = Pref + "/(unit or " + DsPref.Rows(0).Item("TITLE3").ToString() + ")"
            End If


            If ddlPName.SelectedValue = "COST" Then
                lblHeading.Text = "Manufacturing Cost Page"
            Else
                lblHeading.Text = "Manufacturing Cost with Depreciation Page"
            End If
            ' lblHeadingS.Text = "(" + ddltype.SelectedItem.Text + " Chart" + ")"


            If ddlChartType.SelectedValue = "RBC" Then
                pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
            Else
                pcScript &= Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
            End If


         

            If ddlChartType.SelectedValue = "RBC" Then
                lblHeadingS.Text = "(" + ddltype.SelectedItem.Text + " Chart" + ")"
                For i = 0 To Cnt
                    If DsData.Rows(i).Item("FINVOLMSI") > 0 Then
                        SalesVolumeUnit = DsData.Rows(i).Item("FINVOLMSI") * DsPref.Rows(0).Item("ConvArea")
                    ElseIf DsData.Rows(i).Item("FINVOLMUNITS") > 0 Then
                        SalesVolumeUnit = DsData.Rows(i).Item("FINVOLMUNITS")
                    End If
                    Data = FormatNumber((DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * DsPref.Rows(0).Item("curr")) * 100 / SalesVolumeUnit, 2)
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEID") & ";" & Data & ")"

                Next
            ElseIf ddlChartType.SelectedValue = "PIE" Then
                lblHeadingS.Text = "(" + ddltype.SelectedItem.Text + " Chart" + ")"
                For i = 0 To Cnt
                    If DsData.Rows(i).Item("FINVOLMSI") > 0 Then
                        SalesVolumeUnit = DsData.Rows(i).Item("FINVOLMSI") * DsPref.Rows(0).Item("ConvArea")
                    ElseIf DsData.Rows(i).Item("FINVOLMUNITS") > 0 Then
                        SalesVolumeUnit = DsData.Rows(i).Item("FINVOLMUNITS")
                    End If
                    Data = FormatNumber((DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * DsPref.Rows(0).Item("curr")) * 100 / SalesVolumeUnit, 2)
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEID").ToString() + ":" + DsData.Rows(i).Item("CASEDE").ToString() & ";" & Data & ")"
                Next
            ElseIf ddlChartType.SelectedValue = "SBAR" Then
                lblHeadingS.Text = ""
                GraphName = "Sargento_KraftPouchvsTray"
                Graphtype = "graph"
                Cnt = DsData.Rows.Count - 1
                'Pref = DsPref.Rows(0).Item("Title6").ToString() + "/unit"
                If (ddlCategorySet.SelectedValue = "0" Or ddlCategorySet.SelectedValue = "") Then
                    pcScript &= Graphtype + ".transposed(true)" + Graphtype + ".setCategories(Variable Material Cost;Variable Labor Cost;Variable Energy Cost;Dist. Pack. Energy;Shipping Cost;Fixed Cost;)"
                    For i = 0 To Cnt
                        If DsData.Rows(i).Item("FINVOLMSI") > 0 Then
                            SalesVolumeUnit = DsData.Rows(i).Item("FINVOLMSI") * DsPref.Rows(0).Item("ConvArea")
                        ElseIf DsData.Rows(i).Item("FINVOLMUNITS") > 0 Then
                            SalesVolumeUnit = DsData.Rows(i).Item("FINVOLMUNITS")
                        End If
                        If SalesVolumeUnit <> 0 Then
                            matCost = FormatNumber(DsData.Rows(i).Item("VMATERIAL") * DsPref.Rows(0).Item("curr") * 100 / SalesVolumeUnit, 2)
                            labCost = FormatNumber(DsData.Rows(i).Item("VLABOR") * DsPref.Rows(0).Item("curr") * 100 / SalesVolumeUnit, 2)
                            ergyCost = FormatNumber(DsData.Rows(i).Item("VENERGY") * DsPref.Rows(0).Item("curr") * 100 / SalesVolumeUnit, 2)
                            DPCost = FormatNumber(DsData.Rows(i).Item("VPACK") * DsPref.Rows(0).Item("curr") * 100 / SalesVolumeUnit, 2)
                            ShipCost = FormatNumber(DsData.Rows(i).Item("VSHIP") * DsPref.Rows(0).Item("curr") * 100 / SalesVolumeUnit, 2)
                            If PrftCost = "COST" And IsDep = "N" Then
                                FixedCost = FormatNumber(DsData.Rows(i).Item("FIXEDCOST") * DsPref.Rows(0).Item("curr") * 100 / SalesVolumeUnit, 2)

                            Else
                                FixedCost = FormatNumber(DsData.Rows(i).Item("FIXEDCOSTDEP") * DsPref.Rows(0).Item("curr") * 100 / SalesVolumeUnit, 2)

                            End If
                        End If

                        pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEID") & ";" & matCost & ";" & labCost & ";" & ergyCost & ";" & DPCost & ";" & ShipCost & ";" & FixedCost & ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(1," + (i + 1).ToString() + ",Variable Material Cost:" + matCost.ToString() + ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(2," + (i + 1).ToString() + ",Variable Labor Cost:" + labCost.ToString() + ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(3," + (i + 1).ToString() + ",Variable Energy Cost:" + ergyCost.ToString() + ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(4," + (i + 1).ToString() + ",Dist. Pack. Energy:" + DPCost.ToString() + ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(5," + (i + 1).ToString() + ",Shipping Cost:" + ShipCost.ToString() + ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(6," + (i + 1).ToString() + ",Fixed Cost:" + FixedCost.ToString() + ")"

                    Next
                Else
                    pcScript = GetStackbarChartDetails(DsData, DsPref, "Unit")
                End If
            End If



            GenrateChart(pcScript, GraphName, Pref)

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub GetBCPerWeight(ByVal DsData As DataTable, ByVal DsPref As DataTable)
        Try
            Dim Data As String = String.Empty
            Dim Text As String = String.Empty
            Dim Pref As String = String.Empty
            Dim pcScript As String = String.Empty
            Dim Graphtype As String = String.Empty
            Dim GraphName As String = String.Empty
            Dim VariableCost As New Decimal
            Dim FixedCost As New Decimal
            Dim TotalCost As New Decimal
            Dim Cnt As New Integer
            Dim SalesVolumeLb As New Decimal
            Dim i As New Integer
            Dim matCost As New Decimal
            Dim labCost As New Decimal
            Dim ergyCost As New Decimal
            Dim DPCost As New Decimal
            Dim ShipCost As New Decimal

            GraphName = "Sargento_KraftPouchvsTray"
            Graphtype = "graph"
            Cnt = DsData.Rows.Count - 1
            Pref = DsPref.Rows(0).Item("Title2").ToString() + "/" + DsPref.Rows(0).Item("Title8").ToString()
            If ddlPName.SelectedValue = "COST" Then
                lblHeading.Text = "Manufacturing Cost Page"
            Else
                lblHeading.Text = "Manufacturing Cost with Depreciation Page"
            End If

            If ddlChartType.SelectedValue = "RBC" Then
                pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
            Else
                pcScript &= Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
            End If

            If ddlChartType.SelectedValue = "RBC" Then
                lblHeadingS.Text = "(" + ddltype.SelectedItem.Text + " Chart" + ")"
                For i = 0 To Cnt
                    SalesVolumeLb = DsData.Rows(i).Item("VOLUME") * DsPref.Rows(0).Item("CONVWT")
                    If SalesVolumeLb <> 0 Then
                        Data = FormatNumber(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * DsPref.Rows(0).Item("curr") / SalesVolumeLb, 3)
                    Else
                        Data = 0
                    End If
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEID") & ";" & Data & ")"
                Next
            ElseIf ddlChartType.SelectedValue = "PIE" Then
                lblHeadingS.Text = "(" + ddltype.SelectedItem.Text + " Chart" + ")"
                For i = 0 To Cnt
                    SalesVolumeLb = DsData.Rows(i).Item("VOLUME") * DsPref.Rows(0).Item("CONVWT")
                    If SalesVolumeLb <> 0 Then
                        Data = FormatNumber(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * DsPref.Rows(0).Item("curr") / SalesVolumeLb, 3)
                    Else
                        Data = 0
                    End If
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEID").ToString() + ":" + DsData.Rows(i).Item("CASEDE").ToString() & ";" & Data & ")"

                Next
            ElseIf ddlChartType.SelectedValue = "SBAR" Then
                lblHeadingS.Text = ""
                GraphName = "Sargento_KraftPouchvsTray"
                Graphtype = "graph"
                Cnt = DsData.Rows.Count - 1
                Pref = "(" + DsPref.Rows(0).Item("Title2").ToString() + "/" + DsPref.Rows(0).Item("Title8").ToString() + ")"
                If (ddlCategorySet.SelectedValue = "0" Or ddlCategorySet.SelectedValue = "") Then
                    pcScript &= Graphtype + ".transposed(true)" + Graphtype + ".setCategories(Variable Material Cost;Variable Labor Cost;Variable Energy Cost;Dist. Pack. Energy;Shipping Cost;Fixed Cost;)"
                    For i = 0 To Cnt
                        SalesVolumeLb = DsData.Rows(i).Item("VOLUME") * DsPref.Rows(0).Item("CONVWT")
                        If SalesVolumeLb <> 0 Then
                            matCost = FormatNumber(DsData.Rows(i).Item("VMATERIAL") * DsPref.Rows(0).Item("curr") / SalesVolumeLb, 3)
                            labCost = FormatNumber(DsData.Rows(i).Item("VLABOR") * DsPref.Rows(0).Item("curr") / SalesVolumeLb, 3)
                            ergyCost = FormatNumber(DsData.Rows(i).Item("VENERGY") * DsPref.Rows(0).Item("curr") / SalesVolumeLb, 3)
                            DPCost = FormatNumber(DsData.Rows(i).Item("VPACK") * DsPref.Rows(0).Item("curr") / SalesVolumeLb, 3)
                            ShipCost = FormatNumber(DsData.Rows(i).Item("VSHIP") * DsPref.Rows(0).Item("curr") / SalesVolumeLb, 3)
                            If PrftCost = "COST" And IsDep = "N" Then
                                FixedCost = FormatNumber(DsData.Rows(i).Item("FIXEDCOST") * DsPref.Rows(0).Item("curr") / SalesVolumeLb, 3)

                            Else
                                FixedCost = FormatNumber(DsData.Rows(i).Item("FIXEDCOSTDEP") * DsPref.Rows(0).Item("curr") / SalesVolumeLb, 3)

                            End If
                        Else
                            matCost = 0
                            labCost = 0
                            ergyCost = 0
                            DPCost = 0
                            ShipCost = 0
                            FixedCost = 0
                        End If
                        pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEID") & ";" & matCost & ";" & labCost & ";" & ergyCost & ";" & DPCost & ";" & ShipCost & ";" & FixedCost & ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(1," + (i + 1).ToString() + ",Variable Material Cost:" + matCost.ToString() + ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(2," + (i + 1).ToString() + ",Variable Labor Cost:" + labCost.ToString() + ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(3," + (i + 1).ToString() + ",Variable Energy Cost:" + ergyCost.ToString() + ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(4," + (i + 1).ToString() + ",Dist. Pack. Energy:" + DPCost.ToString() + ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(5," + (i + 1).ToString() + ",Shipping Cost:" + ShipCost.ToString() + ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(6," + (i + 1).ToString() + ",Fixed Cost:" + FixedCost.ToString() + ")"
                    Next
                Else
                    pcScript = GetStackbarChartDetails(DsData, DsPref, "Weight")
                End If

            End If

            GenrateChart(pcScript, GraphName, Pref)
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub GetBCTotal(ByVal DsData As DataTable, ByVal DsPref As DataTable)
        Try
            Dim Data As String = String.Empty
            Dim Text As String = String.Empty
            Dim Pref As String = String.Empty
            Dim pcScript As String = String.Empty
            Dim Graphtype As String = String.Empty
            Dim GraphName As String = String.Empty
            Dim Cnt As New Integer
            Dim VariableCost As New Decimal
            Dim FixedCost As New Decimal
            Dim TotalCost As New Decimal
            Dim i As New Integer

            Dim matCost As New Decimal
            Dim labCost As New Decimal
            Dim ergyCost As New Decimal
            Dim DPCost As New Decimal
            Dim ShipCost As New Decimal
            Dim plantM As New Decimal

            GraphName = "Sargento_KraftPouchvsTray"
            Graphtype = "graph"
            Cnt = DsData.Rows.Count - 1
            Pref = DsPref.Rows(0).Item("TITLE2").ToString() + ""

            If ddlPName.SelectedValue = "COST" Then
                lblHeading.Text = "Manufacturing Cost Page"
            Else
                lblHeading.Text = "Manufacturing Cost with Depreciation Page"
            End If
            lblHeadingS.Text = ""

            If ddlChartType.SelectedValue = "RBC" Then
                pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
            Else
                pcScript &= Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"

            End If
            If ddlChartType.SelectedValue = "RBC" Then
                For i = 0 To Cnt
                    Data = FormatNumber(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * DsPref.Rows(0).Item("curr"), 0)
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEID") & ";" & Data & ")"
                Next
            ElseIf ddlChartType.SelectedValue = "PIE" Then
                For i = 0 To Cnt
                    Data = FormatNumber(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * DsPref.Rows(0).Item("curr"), 0)
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEID").ToString() + ":" + DsData.Rows(i).Item("CASEDE").ToString() & ";" & Data & ")"

                Next
            ElseIf ddlChartType.SelectedValue = "SBAR" Then
                GraphName = "Sargento_KraftPouchvsTray"
                Graphtype = "graph"
                Cnt = DsData.Rows.Count - 1
                Pref = "(" + DsPref.Rows(0).Item("TITLE2").ToString() + ") "
                'pcScript &= Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
                If (ddlCategorySet.SelectedValue = "0" Or ddlCategorySet.SelectedValue = "") Then
                    pcScript &= Graphtype + ".transposed(true)" + Graphtype + ".setCategories(Variable Material Cost;Variable Labor Cost;Variable Energy Cost;Dist. Pack. Energy;Shipping Cost;Fixed Cost;)"
                    For i = 0 To Cnt
                        matCost = FormatNumber(DsData.Rows(i).Item("VMATERIAL") * DsPref.Rows(0).Item("curr"), 0)
                        labCost = FormatNumber(DsData.Rows(i).Item("VLABOR") * DsPref.Rows(0).Item("curr"), 0)
                        ergyCost = FormatNumber(DsData.Rows(i).Item("VENERGY") * DsPref.Rows(0).Item("curr"), 0)
                        DPCost = FormatNumber(DsData.Rows(i).Item("VPACK") * DsPref.Rows(0).Item("curr"), 0)
                        ShipCost = FormatNumber(DsData.Rows(i).Item("VSHIP") * DsPref.Rows(0).Item("curr"), 0)
                        If PrftCost = "COST" And IsDep = "N" Then
                            FixedCost = FormatNumber(DsData.Rows(i).Item("FIXEDCOST") * DsPref.Rows(0).Item("curr"), 0)
                        Else
                            FixedCost = FormatNumber(DsData.Rows(i).Item("FIXEDCOSTDEP") * DsPref.Rows(0).Item("curr"), 0)
                        End If


                        pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEID") & ";" & matCost & ";" & labCost & ";" & ergyCost & ";" & DPCost & ";" & ShipCost & ";" & FixedCost & ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(1," + (i + 1).ToString() + ",Variable Material Cost:" + matCost.ToString() + ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(2," + (i + 1).ToString() + ",Variable Labor Cost:" + labCost.ToString() + ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(3," + (i + 1).ToString() + ",Variable Energy Cost:" + ergyCost.ToString() + ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(4," + (i + 1).ToString() + ",Dist. Pack. Energy:" + DPCost.ToString() + ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(5," + (i + 1).ToString() + ",Shipping Cost:" + ShipCost.ToString() + ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(6," + (i + 1).ToString() + ",Fixed Cost:" + FixedCost.ToString() + ")"

                    Next
                Else
                    pcScript = GetStackbarChartDetails(DsData, DsPref, "Total")
                End If



            End If

            GenrateChart(pcScript, GraphName, Pref)
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub GenrateChart(ByVal PcScript As String, ByVal GraphName As String, ByVal Pref As String)
        Try
            Dim dsChartSetting As New DataTable
            Dim objGetData As New Configration.Selectdata()
            dsChartSetting = objGetData.GetChartSettings().Tables(0)

            Dim myImage As CordaEmbedder = New CordaEmbedder()
            myImage.externalServerAddress = dsChartSetting.Rows(0)("EXTSERVERADD").ToString()
            myImage.internalCommPortAddress = dsChartSetting.Rows(0)("INTCOMPORTADD").ToString()
            If ddlChartType.SelectedValue = "PIE" Then
                myImage.imageTemplate = "Sargento_KraftPouchvsTray_Html4" + ".itxml"
            ElseIf ddlChartType.SelectedValue = "SBAR" Then
                myImage.imageTemplate = "Sargento_KraftPouchvsTray6.itxml"
            Else
                myImage.imageTemplate = "Sargento_KraftPouchvsTray_Html" + ".itxml"
            End If

            myImage.userAgent = Request.UserAgent
            myImage.width = 700
            myImage.height = 400
            myImage.returnDescriptiveLink = False
            myImage.language = "EN"
            myImage.pcScript = PcScript + "Y-axis.SetText(" + Pref + ")"
            myImage.outputType = "JPEG"
            myImage.fallback = "STRICT"

            ChartComp.InnerHtml = myImage.getEmbeddingHTML
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub bynUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bynUpdate.Click
        Try

        Catch ex As Exception
            ErrorLable.Text = "Error:Page_Load:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub ddlPName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPName.SelectedIndexChanged
        Try
            Bindddl()
        Catch ex As Exception

        End Try
    End Sub
    Function GetStackbarChartDetails(ByVal DsData As DataTable, ByVal DsPref As DataTable, ByVal Contype As String) As String
        Dim ObjGetE3data As New E3GetData.Selectdata
        Dim dsCategories As New DataTable
        Dim arItemList As String = String.Empty
        Dim matCost As Decimal
        Dim SalesVolumeUnit As New Decimal
        Dim SalesVolumeLb As New Decimal
        Dim pcsetSeries As String = String.Empty
        Dim pcAddHoveText As String = String.Empty
        Dim pcSetCategories As String = String.Empty
        Dim Cnt As New Integer
        Dim Graphtype As String = String.Empty
        Dim GraphName As String = String.Empty
        Dim pcScript As String = String.Empty
        Dim dsitems As New DataSet
        Dim j As Integer
        Dim k As Integer
        Try
            GraphName = "Sargento_KraftPouchvsTray"
            Graphtype = "graph"
            Cnt = DsData.Rows.Count - 1
            pcSetCategories &= Graphtype + ".transposed(true)" + Graphtype + ".setCategories("
            dsCategories = ObjGetE3data.GetCategoryBySet(ddlCategorySet.SelectedValue, "")



            For k = 0 To Cnt

                If DsData.Rows(k).Item("FINVOLMSI") > 0 Then
                    SalesVolumeUnit = DsData.Rows(k).Item("FINVOLMSI") * DsPref.Rows(0).Item("ConvArea")
                ElseIf DsData.Rows(k).Item("FINVOLMUNITS") > 0 Then
                    SalesVolumeUnit = DsData.Rows(k).Item("FINVOLMUNITS")
                End If

                SalesVolumeLb = DsData.Rows(k).Item("VOLUME") * DsPref.Rows(0).Item("CONVWT")

                pcsetSeries = pcsetSeries + "" + Graphtype + ".setSeries(" & DsData.Rows(k).Item("CASEID") & ";"
                For i = 0 To dsCategories.Rows.Count - 1
                    dsitems = ObjGetE3data.GetCategoryItemBycategory(dsCategories.Rows(i).Item("Categoryid").ToString())

                    pcAddHoveText = pcAddHoveText + "" + Graphtype + ".addHoverText(" + (i + 1).ToString() + "," + (k + 1).ToString()
                    matCost = 0

                    For j = 0 To dsitems.Tables(0).Rows.Count - 1
                        'matCost = FormatNumber(DsData.Rows(i).Item("VMATERIAL") * DsPref.Rows(0).Item("curr") * 100 / SalesVolumeUnit, 2)
                        If (Contype = "Total") Then
                            matCost = FormatNumber(matCost + DsData.Rows(k).Item("" + dsitems.Tables(0).Rows(j).Item("ITEMNAME").ToString() + "") * DsPref.Rows(0).Item("curr"), 0)
                        ElseIf (Contype = "Unit") Then
                            If SalesVolumeUnit <> 0 Then
                                matCost = matCost + DsData.Rows(k).Item("" + dsitems.Tables(0).Rows(j).Item("ITEMNAME").ToString() + "") * DsPref.Rows(0).Item("curr") * 100 / SalesVolumeUnit
                                matCost = FormatNumber(matCost, 4)
                            End If
                        ElseIf (Contype = "Weight") Then
                            If SalesVolumeLb <> 0 Then
                                matCost = matCost + DsData.Rows(k).Item("" + dsitems.Tables(0).Rows(j).Item("ITEMNAME").ToString() + "") * DsPref.Rows(0).Item("curr") / SalesVolumeLb
                                matCost = FormatNumber(matCost, 3)
                            End If
                        End If
                            arItemList = arItemList + dsitems.Tables(0).Rows(j).Item("ITEMNAME").ToString()
                    Next
                    pcsetSeries &= "" & matCost & ";"
                    pcAddHoveText = pcAddHoveText + "," + dsCategories.Rows(i).Item("Categoryname").ToString() + ":" + matCost.ToString() + ")"
                    If (k = 0) Then
                        pcSetCategories &= "" & dsCategories.Rows(i).Item("Categoryname").ToString() & ";"
                    End If
                Next

                pcsetSeries &= "" & ")"
            Next
            pcSetCategories &= "" & ")"
            pcScript &= pcSetCategories + pcScript + pcsetSeries + pcAddHoveText
            Return pcScript
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Protected Sub ddlChartType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlChartType.SelectedIndexChanged
        Try
            If ddlChartType.SelectedValue = "SBAR" Then
                ddltype.Enabled = False
                ROWCategoryset.Visible = True
                lnkManageCategory.Visible = True
                BindCategorySet()
            Else
                ddltype.Enabled = True
                ROWCategoryset.Visible = False
                lnkManageCategory.Visible = False
            End If

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub BindCategorySet()
        Dim dt As New DataTable
        Dim ObjGetData As New E3GetData.Selectdata
        Dim lst As New ListItem
        Try
            ddlCategorySet.Items.Clear()
            dt = ObjGetData.GetCategorySet("", Session("userid"), ddlPName.SelectedValue.ToString())
            lst.Text = "SavvyPack Default Category"
            lst.Value = 0

            ddlCategorySet.Items.Add(lst)
            ddlCategorySet.AppendDataBoundItems = True

            With ddlCategorySet
                .DataSource = dt
                .DataTextField = "CATEGORYSETNAME"
                .DataValueField = "CATEGORYSETID"
                .DataBind()
            End With
            ddlCategorySet.SelectedValue = 0
        Catch ex As Exception
            Response.Write("Error:BindCategorySet:" + ex.Message.ToString())
        End Try
    End Sub
End Class
