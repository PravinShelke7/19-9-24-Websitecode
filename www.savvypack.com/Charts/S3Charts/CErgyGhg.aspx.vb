Imports Corda
Imports System.Web.UI.HtmlControls
Imports System.Data
Imports System.Data.OleDb
Imports System.Web.HttpBrowserCapabilities
Imports S1GetData
Imports Configration
Partial Class Charts_S3Charts_CErgyGhg
    Inherits System.Web.UI.Page
#Region " Web Form Designer Generated Code "
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    End Sub
#End Region

#Region "Get Set Variables"
    Dim _lErrorLble As Label
    Dim _strErgyGHG As String
    Dim _strChartType As String
    Dim _strChartName As String
    Dim _iAssumptionId As Integer
    Dim _cnType As String
    Dim _PageName As String

    Public Property ErrorLable() As Label
        Get
            Return _lErrorLble
        End Get
        Set(ByVal Value As Label)
            _lErrorLble = Value
        End Set
    End Property

    Public Property ErgyGHG() As String
        Get
            Return _strErgyGHG
        End Get
        Set(ByVal value As String)
            _strErgyGHG = value
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
    Public Property AssumptionId() As Integer
        Get
            Return _iAssumptionId
        End Get
        Set(ByVal Value As Integer)
            _iAssumptionId = Value
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
    Public Property PageName() As String
        Get
            Return _PageName
        End Get
        Set(ByVal value As String)
            _PageName = value
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

#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_CHARTS_S3CHARTS_CERGYGHG")
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        objRefresh.Render(Page)
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim dsData As New DataSet
        Dim dsPref As New DataSet
        Dim objCryptoHelper As New CryptoHelper()
        Dim CaseId1 As String = String.Empty
        Dim CaseId2 As String = String.Empty
        Dim arrCaseID() As String
        Dim DataCnt As Integer

        Dim ds As New DataSet
        Dim objS3GetData As New S3GetData.Selectdata
        Dim i As Integer = 0
        Dim flag = False
        Dim flagVal = True
        Dim message As String = String.Empty
        Dim dt As New DataTable

        Dim dtCatSet As New DataTable

        Try
            GetMasterPageControls()

            AssumptionId = Session("AssumptionID")
            Try
                ChartType = objCryptoHelper.Decrypt(Request.QueryString("ChartType"))
            Catch ex As Exception
                ChartType = "RBC"
            End Try

            If Not IsPostBack Then
                If Request.QueryString("ErgyGHG") <> Nothing Then
                    ErgyGHG = objCryptoHelper.Decrypt(Request.QueryString("ErgyGHG"))
                    CNType = objCryptoHelper.Decrypt(Request.QueryString("CType"))
                    ddlPName.SelectedValue = ErgyGHG
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
                    dtCatSet = objS3GetData.GetCategorySetByID(ddlCategorySet.SelectedValue)
                    If dtCatSet.Rows.Count > 0 Then
                        dt = objS3GetData.GetCategoryBySet(ddlCategorySet.SelectedValue, "")
                        If dt.Rows.Count > 0 Then
                            For i = 0 To dt.Rows.Count - 1
                                ds = objS3GetData.GetCategoryItemBycategory(dt.Rows(i).Item("CATEGORYID").ToString())
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

            GetPageTitle()

            If flagVal Then
                If ddlCnType.SelectedValue = "Total" Then
                    If ErgyGHG = "ERGY" Then
                        GetBETotal(dsData.Tables(0), dsPref.Tables(0))
                    ElseIf ErgyGHG = "WATER" Then
                        GetBWTotal(dsData.Tables(0), dsPref.Tables(0))
                    Else
                        GetBGTotal(dsData.Tables(0), dsPref.Tables(0))
                    End If
                ElseIf ddlCnType.SelectedValue = "PUnit" Then
                    If ErgyGHG = "ERGY" Then
                        GetBEPerUnit(dsData.Tables(0), dsPref.Tables(0))
                    ElseIf ErgyGHG = "WATER" Then
                        GetBWPerUnit(dsData.Tables(0), dsPref.Tables(0))
                    Else
                        GetBGPerUnit(dsData.Tables(0), dsPref.Tables(0))
                    End If

                ElseIf ddlCnType.SelectedValue = "PWeight" Then
                    If ErgyGHG = "ERGY" Then
                        GetBEPerWeight(dsData.Tables(0), dsPref.Tables(0))
                    ElseIf ErgyGHG = "WATER" Then
                        GetBWPerWeight(dsData.Tables(0), dsPref.Tables(0))
                    Else
                        GetBGPerWeight(dsData.Tables(0), dsPref.Tables(0))
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
            ErgyGHG = PageName
            
        Catch ex As Exception

        End Try
    End Sub

    Protected Function GetCaseIds() As String()
        Dim CaseIds(0) As String
        Dim objGetData As New S3GetData.Selectdata
        Try
            CaseIds = objGetData.Cases1(AssumptionId)
            Return CaseIds
        Catch ex As Exception
            Return CaseIds
            _lErrorLble.Text = "Error:GetCaseIds:" + ex.Message.ToString()
        End Try

    End Function

    Protected Sub GetPageTitle()
        Try
            If ErgyGHG = "ERGY" Then
                If ChartType = "RBC" Then
                    Page.Title = "S3-Combine Energy Bar Chart"
                    rwType.Visible = True
                Else
                    Page.Title = "S3-Combine Energy Stack Chart"
                    rwType.Visible = False
                End If
            ElseIf ErgyGHG = "WATER" Then
                If ChartType = "RBC" Then
                    Page.Title = "S3-Combine Water Bar Chart"
                    rwType.Visible = True
                Else
                    Page.Title = "S3-Combine Water Stack Chart"
                    rwType.Visible = False
                End If
            Else
                If ChartType = "RBC" Then
                    Page.Title = "S3-Combine GHG Bar Chart"
                    rwType.Visible = True
                Else
                    Page.Title = "S3-Combine GHG Stack Chart"
                    rwType.Visible = False
                End If
            End If
        Catch ex As Exception
            ErrorLable.Text = "Error:GetPageTitle:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub Bindddl()
        Dim ds As New DataSet
        Dim objGetData As New S1GetData.Selectdata()
        Try
            GetDetails()
            If ErgyGHG = "ERGY" Then
                ds = objGetData.GetChartErgy()
            ElseIf ErgyGHG = "WATER" Then
                ds = objGetData.GetChartWater()
            Else
                ds = objGetData.GetChartGhg()
            End If

            With ddltype
                .DataSource = ds
                .DataTextField = "TEXT"
                .DataValueField = "VALUE"
                .DataBind()
            End With

        Catch ex As Exception
            ErrorLable.Text = "Error:BindDdl" + ex.Message.ToString()
        End Try
    End Sub

    Protected Function GetData(ByVal CaseIds() As String) As DataSet
        Dim ds As New DataSet
        Dim objGetData As New S3GetData.Selectdata()
        Try

            If ErgyGHG = "ERGY" Then
                ds = objGetData.GetChartErgyRes(CaseIds)
            ElseIf ErgyGHG = "WATER" Then
                ds = objGetData.GetChartWaterRes(CaseIds)
            Else
                ds = objGetData.GetChartGhgRes(CaseIds)
            End If
            Return ds
        Catch ex As Exception
            ErrorLable.Text = "Error:GetData:" + ex.Message.ToString()
            Return ds
        End Try
    End Function

    Protected Function GetPref(ByVal USERID As String) As DataSet
        Dim ds As New DataSet
        Dim objGetData As New S1GetData.Selectdata()
        Try
            ds = objGetData.GetChartPref(USERID)
            Return ds
        Catch ex As Exception
            ErrorLable.Text = "Error:GetPref:" + ex.Message.ToString()
            Return ds
        End Try
    End Function

    Protected Sub GetBEPerUnit(ByVal DsData As DataTable, ByVal DsPref As DataTable)
        Try

            Dim Data As String = String.Empty
            Dim Text As String = String.Empty
            Dim Pref As String = String.Empty
            Dim HoverText As String = String.Empty
            Dim pcScript As String = String.Empty
            Dim Graphtype As String = String.Empty
            Dim GraphName As String = String.Empty
            Dim Transportation As New Decimal
            Dim Process As New Decimal
            Dim Purchased As New Decimal
            Dim Cnt As New Integer
            Dim SalesVolumeUnit As New Decimal
            Dim i As New Integer

            GraphName = "Sargento_KraftPouchvsTray"
            Graphtype = "graph"
            Cnt = DsData.Rows.Count - 1
            ''FOR UNIT DISPLAY
            Dim j As New Integer
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
            Pref = "MJ"
            If j = 1 Then
                Pref = Pref + "/" + DsPref.Rows(0).Item("TITLE3").ToString()
            ElseIf j = 2 Then
                Pref = Pref + "/unit "
            Else
                Pref = Pref + "/(unit or " + DsPref.Rows(0).Item("TITLE3").ToString() + ")"
            End If

            If ddlPName.SelectedValue = "ERGY" Then
                lblHeading.Text = "Energy Page"
            Else
                lblHeading.Text = "GHG Page"
            End If
            lblHeadingS.Text = "(" + ddltype.SelectedItem.Text + " Chart" + ")"

            'If DsData.Rows(0).Item("FINVOLMSI") > 0 Then
            '    HoverText = "MJ/" + DsPref.Rows(0).Item("TITLE3").ToString()
            'ElseIf DsData.Rows(i).Item("FINVOLMUNITS") > 0 Then
            '    HoverText = "MJ/Unit"
            'End If
            'Pref = HoverText
            If ddlChartType.SelectedValue = "RBC" Then
                GraphName = "Sargento_KraftPouchvsTray"
                Graphtype = "graph"
                Cnt = DsData.Rows.Count - 1
                pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
               
                For i = 0 To Cnt
                    If DsData.Rows(i).Item("FINVOLMSI") > 0 Then
                        SalesVolumeUnit = FormatNumber(DsData.Rows(i).Item("FINVOLMSI") * DsPref.Rows(0).Item("ConvArea"), 0)
                    ElseIf DsData.Rows(i).Item("FINVOLMUNITS") > 0 Then
                        SalesVolumeUnit = DsData.Rows(i).Item("FINVOLMUNITS")
                    End If
                    If CDbl(SalesVolumeUnit) <> 0.0 Then
                        Data = FormatNumber(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") / SalesVolumeUnit, 3)
                    Else
                        Data = 0
                    End If
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEID") & ";" & Data & ")"
                Next
            ElseIf ddlChartType.SelectedValue = "PIE" Then
                GraphName = "Sargento_KraftPouchvsTray"
                Graphtype = "graph"
                Cnt = DsData.Rows.Count - 1
                pcScript &= Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
                For i = 0 To Cnt
                    If DsData.Rows(i).Item("FINVOLMSI") > 0 Then
                        SalesVolumeUnit = FormatNumber(DsData.Rows(i).Item("FINVOLMSI") * DsPref.Rows(0).Item("ConvArea"), 0)
                    ElseIf DsData.Rows(i).Item("FINVOLMUNITS") > 0 Then
                        SalesVolumeUnit = DsData.Rows(i).Item("FINVOLMUNITS")
                    End If
                    If CDbl(SalesVolumeUnit) <> 0.0 Then
                        Data = FormatNumber(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") / SalesVolumeUnit, 3)
                    Else
                        Data = 0
                    End If
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE").ToString() & ";" & Data & ")"
                Next
            ElseIf ddlChartType.SelectedValue = "SBAR" Then
                lblHeadingS.Text = ""
                GraphName = "Sargento_KraftPouchvsTray"
                Graphtype = "graph"
                Cnt = DsData.Rows.Count - 1
                If (ddlCategorySet.SelectedValue = "0" Or ddlCategorySet.SelectedValue = "") Then
                    pcScript &= Graphtype + ".transposed(true)" + Graphtype + ".setCategories(Purchased Materials;Process;Transportation;)"
                    For i = 0 To Cnt
                        If DsData.Rows(i).Item("FINVOLMSI") > 0 Then
                            SalesVolumeUnit = FormatNumber(DsData.Rows(i).Item("FINVOLMSI") * DsPref.Rows(0).Item("ConvArea"), 0)
                        ElseIf DsData.Rows(i).Item("FINVOLMUNITS") > 0 Then
                            SalesVolumeUnit = DsData.Rows(i).Item("FINVOLMUNITS")
                        End If

                        If CDbl(SalesVolumeUnit) <> 0.0 Then
                            Purchased = FormatNumber(DsData.Rows(i).Item("PURMATERIALERGY") / SalesVolumeUnit, 3)
                            Process = FormatNumber(DsData.Rows(i).Item("PROCERGY") / SalesVolumeUnit, 3)
                            Transportation = FormatNumber(DsData.Rows(i).Item("TRNSPTERGY") / SalesVolumeUnit, 3)
                        Else
                            Purchased = 0
                            Process = 0
                            Transportation = 0
                        End If

                        pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEID") & ";" & Purchased & ";" & Process & ";" & Transportation & ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(1," + (i + 1).ToString() + ",Purchased Materials:" + Purchased.ToString() + ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(2," + (i + 1).ToString() + ",Process:" + Process.ToString() + ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(3," + (i + 1).ToString() + ",Transportation:" + Transportation.ToString() + ")"
                    Next
                Else
                    pcScript = GetStackbarChartDetails(DsData, DsPref, "Unit")
                End If
            End If

            GenrateChart(pcScript, GraphName, Pref)

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GetBEPerWeight(ByVal DsData As DataTable, ByVal DsPref As DataTable)
        Try
            Dim Data As String = String.Empty
            Dim Text As String = String.Empty
            Dim Pref As String = String.Empty
            Dim pcScript As String = String.Empty
            Dim Graphtype As String = String.Empty
            Dim GraphName As String = String.Empty
            Dim Cnt As New Integer
            Dim SalesVolumeLb As New Decimal
            Dim Transportation As New Decimal
            Dim Process As New Decimal
            Dim Purchased As New Decimal
            Dim i As New Integer

            GraphName = "Sargento_KraftPouchvsTray"
            Graphtype = "graph"
            Cnt = DsData.Rows.Count - 1
            Pref = "MJ/" + DsPref.Rows(0).Item("Title8").ToString()

            If ddlPName.SelectedValue = "ERGY" Then
                lblHeading.Text = "Energy Page"
            Else
                lblHeading.Text = "GHG Page"
            End If
            lblHeadingS.Text = "(" + ddltype.SelectedItem.Text + " Chart" + ")"

            If ddlChartType.SelectedValue = "RBC" Then
                GraphName = "Sargento_KraftPouchvsTray"
                Graphtype = "graph"
                Cnt = DsData.Rows.Count - 1
                pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
                For i = 0 To Cnt
                    SalesVolumeLb = FormatNumber(DsData.Rows(i).Item("VOLUME") * DsPref.Rows(0).Item("CONVWT"), 0)
                    If CDbl(SalesVolumeLb) <> 0.0 Then
                        Data = FormatNumber(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") / SalesVolumeLb, 3)
                    Else
                        Data = 0
                    End If
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEID") & ";" & Data & ")"
                Next
            ElseIf ddlChartType.SelectedValue = "PIE" Then
                GraphName = "Sargento_KraftPouchvsTray"
                Graphtype = "graph"
                Cnt = DsData.Rows.Count - 1
                pcScript &= Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
                For i = 0 To Cnt
                    SalesVolumeLb = FormatNumber(DsData.Rows(i).Item("VOLUME") * DsPref.Rows(0).Item("CONVWT"), 0)
                    If CDbl(SalesVolumeLb) <> 0.0 Then
                        Data = FormatNumber(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") / SalesVolumeLb, 3)
                    Else
                        Data = 0
                    End If
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE").ToString() & ";" & Data & ")"
                Next
            ElseIf ddlChartType.SelectedValue = "SBAR" Then
                lblHeadingS.Text = ""
                GraphName = "Sargento_KraftPouchvsTray"
                Graphtype = "graph"
                Cnt = DsData.Rows.Count - 1
                If (ddlCategorySet.SelectedValue = "0" Or ddlCategorySet.SelectedValue = "") Then
                    pcScript &= Graphtype + ".transposed(true)" + Graphtype + ".setCategories(Purchased Materials;Process;Transportation;)"
                    For i = 0 To Cnt
                        SalesVolumeLb = FormatNumber(DsData.Rows(i).Item("VOLUME") * DsPref.Rows(0).Item("CONVWT"), 0)

                        If CDbl(SalesVolumeLb) <> 0.0 Then
                            Purchased = FormatNumber(DsData.Rows(i).Item("PURMATERIALERGY") / SalesVolumeLb, 3)
                            Process = FormatNumber(DsData.Rows(i).Item("PROCERGY") / SalesVolumeLb, 3)
                            Transportation = FormatNumber(DsData.Rows(i).Item("TRNSPTERGY") / SalesVolumeLb, 3)
                        Else
                            Purchased = 0
                            Process = 0
                            Transportation = 0
                        End If

                        pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEID") & ";" & Purchased & ";" & Process & ";" & Transportation & ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(1," + (i + 1).ToString() + ",Purchased Materials:" + Purchased.ToString() + ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(2," + (i + 1).ToString() + ",Process:" + Process.ToString() + ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(3," + (i + 1).ToString() + ",Transportation:" + Transportation.ToString() + ")"
                    Next
                Else
                    pcScript = GetStackbarChartDetails(DsData, DsPref, "Weight")
                End If
            End If

            GenrateChart(pcScript, GraphName, Pref)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GetBETotal(ByVal DsData As DataTable, ByVal DsPref As DataTable)
        Try
            Dim Data As String = String.Empty
            Dim Text As String = String.Empty
            Dim Pref As String = String.Empty
            Dim pcScript As String = String.Empty
            Dim Graphtype As String = String.Empty
            Dim GraphName As String = String.Empty
            Dim Cnt As New Integer
            Dim i As New Integer
            Dim Transportation As New Decimal
            Dim Process As New Decimal
            Dim Purchased As New Decimal


            GraphName = "Sargento_KraftPouchvsTray"
            Graphtype = "graph"
            Cnt = DsData.Rows.Count - 1
            Pref = "MJ"
            If ddlPName.SelectedValue = "ERGY" Then
                lblHeading.Text = "Energy Page"
            Else
                lblHeading.Text = "GHG Page"
            End If
            lblHeadingS.Text = "(" + ddltype.SelectedItem.Text + " Chart" + ")"
            If ddlChartType.SelectedValue = "RBC" Then
                pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
                GraphName = "Sargento_KraftPouchvsTray"
                Graphtype = "graph"
                Cnt = DsData.Rows.Count - 1
                pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
                For i = 0 To Cnt
                    If DsData.Rows(i).Item("SVOLUME").ToString() > 0 Then
                        Data = FormatNumber(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + ""), 0)
                    Else
                        Data = 0
                    End If
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEID") & ";" & Data & ")"
                Next
            ElseIf ddlChartType.SelectedValue = "PIE" Then
                GraphName = "Sargento_KraftPouchvsTray"
                Graphtype = "graph"
                Cnt = DsData.Rows.Count - 1
                pcScript &= Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
                For i = 0 To Cnt
                    If DsData.Rows(i).Item("SVOLUME").ToString() > 0 Then
                        Data = FormatNumber(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + ""), 0)
                    Else
                        Data = 0
                    End If
                    pcScript &= "" + Graphtype + ".setSeries(" + DsData.Rows(i).Item("CASEDE").ToString() & ";" & Data & ")"
                Next
            ElseIf ddlChartType.SelectedValue = "SBAR" Then
                lblHeadingS.Text = ""
                GraphName = "Sargento_KraftPouchvsTray"
                Graphtype = "graph"
                Cnt = DsData.Rows.Count - 1
                If (ddlCategorySet.SelectedValue = "0" Or ddlCategorySet.SelectedValue = "") Then
                    pcScript &= Graphtype + ".transposed(true)" + Graphtype + ".setCategories(Purchased Materials;Process;Transportation;)"
                    For i = 0 To Cnt
                        If DsData.Rows(i).Item("SVOLUME").ToString() > 0 Then
                            Purchased = FormatNumber(DsData.Rows(i).Item("PURMATERIALERGY"), 0)
                            Process = FormatNumber(DsData.Rows(i).Item("PROCERGY"), 0)
                            Transportation = FormatNumber(DsData.Rows(i).Item("TRNSPTERGY"), 0)
                        Else
                            Purchased = 0
                            Process = 0
                            Transportation = 0

                        End If

                        pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEID") & ";" & Purchased & ";" & Process & ";" & Transportation & ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(1," + (i + 1).ToString() + ",Purchased Materials:" + Purchased.ToString() + ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(2," + (i + 1).ToString() + ",Process:" + Process.ToString() + ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(3," + (i + 1).ToString() + ",Transportation:" + Transportation.ToString() + ")"
                    Next
                Else
                    pcScript = GetStackbarChartDetails(DsData, DsPref, "Total")
                End If
            End If

            GenrateChart(pcScript, GraphName, Pref)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GetBGPerUnit(ByVal DsData As DataTable, ByVal DsPref As DataTable)
        Try

            Dim Data As String = String.Empty
            Dim Text As String = String.Empty
            Dim Pref As String = String.Empty
            Dim pcScript As String = String.Empty
            Dim Graphtype As String = String.Empty
            Dim GraphName As String = String.Empty
            Dim HoverText As String = String.Empty
            Dim Cnt As New Integer
            Dim SalesVolumeUnit As New Decimal
            Dim i As New Integer
            Dim Transportation As New Decimal
            Dim Process As New Decimal
            Dim Purchased As New Decimal

            GraphName = "Sargento_KraftPouchvsTray"
            Graphtype = "graph"
            Cnt = DsData.Rows.Count - 1


            ''FOR UNIT DISPLAY
            Dim j As New Integer
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
            Pref = DsPref.Rows(0).Item("TITLE8").ToString()
            If j = 1 Then
                Pref = Pref + " gr gas/" + DsPref.Rows(0).Item("TITLE3").ToString()
            ElseIf j = 2 Then
                Pref = Pref + " gr gas/unit "
            Else
                Pref = Pref + " gr gas/(unit or " + DsPref.Rows(0).Item("TITLE3").ToString() + ")"
            End If


            If ddlPName.SelectedValue = "ERGY" Then
                lblHeading.Text = "Energy Page"
            Else
                lblHeading.Text = "GHG Page"
            End If
            lblHeadingS.Text = "(" + ddltype.SelectedItem.Text + " Chart" + ")"

            'If DsData.Rows(0).Item("FINVOLMSI") > 0 Then
            '    HoverText = DsPref.Rows(0).Item("TITLE8").ToString() + " gr gas/" + DsPref.Rows(0).Item("TITLE3").ToString()
            'ElseIf DsData.Rows(i).Item("FINVOLMUNITS") > 0 Then
            '    HoverText = DsPref.Rows(0).Item("TITLE8").ToString() + " gr gas/Unit"
            'End If
            'Pref = HoverText
            If ddlChartType.SelectedValue = "RBC" Then
                GraphName = "Sargento_KraftPouchvsTray"
                Graphtype = "graph"
                'Cnt = DsData.Rows.Count - 1
                pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
               
                For i = 0 To Cnt
                    If DsData.Rows(i).Item("FINVOLMSI") > 0 Then
                        SalesVolumeUnit = FormatNumber(DsData.Rows(i).Item("FINVOLMSI") * DsPref.Rows(0).Item("ConvArea"), 0)
                    ElseIf DsData.Rows(i).Item("FINVOLMUNITS") > 0 Then
                        SalesVolumeUnit = DsData.Rows(i).Item("FINVOLMUNITS")
                    End If
                    If CDbl(SalesVolumeUnit) <> 0.0 Then
                        Data = FormatNumber(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * DsPref.Rows(0).Item("CONVWT") / SalesVolumeUnit, 3)
                    Else
                        Data = 0
                    End If
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEID") & ";" & Data & ")"
                Next
            ElseIf ddlChartType.SelectedValue = "PIE" Then
                GraphName = "Sargento_KraftPouchvsTray"
                Graphtype = "graph"
                Cnt = DsData.Rows.Count - 1
                pcScript &= Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
                For i = 0 To Cnt
                    If DsData.Rows(i).Item("FINVOLMSI") > 0 Then
                        SalesVolumeUnit = FormatNumber(DsData.Rows(i).Item("FINVOLMSI") * DsPref.Rows(0).Item("ConvArea"), 0)
                    ElseIf DsData.Rows(i).Item("FINVOLMUNITS") > 0 Then
                        SalesVolumeUnit = DsData.Rows(i).Item("FINVOLMUNITS")
                    End If
                    If CDbl(SalesVolumeUnit) <> 0.0 Then
                        Data = FormatNumber(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * DsPref.Rows(0).Item("CONVWT") / SalesVolumeUnit, 3)
                    Else
                        Data = 0
                    End If
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE").ToString() & ";" & Data & ")"
                Next
            ElseIf ddlChartType.SelectedValue = "SBAR" Then
                lblHeadingS.Text = ""
                GraphName = "Sargento_KraftPouchvsTray"
                Graphtype = "graph"
                Cnt = DsData.Rows.Count - 1
                If (ddlCategorySet.SelectedValue = "0" Or ddlCategorySet.SelectedValue = "") Then
                    pcScript &= Graphtype + ".transposed(true)" + Graphtype + ".setCategories(Purchased Materials;Process;Transportation;)"
                    For i = 0 To Cnt
                        If DsData.Rows(i).Item("FINVOLMSI") > 0 Then
                            SalesVolumeUnit = FormatNumber(DsData.Rows(i).Item("FINVOLMSI") * DsPref.Rows(0).Item("ConvArea"), 0)
                        ElseIf DsData.Rows(i).Item("FINVOLMUNITS") > 0 Then
                            SalesVolumeUnit = DsData.Rows(i).Item("FINVOLMUNITS")
                        End If
                        If CDbl(SalesVolumeUnit) <> 0.0 Then
                            Purchased = FormatNumber(DsData.Rows(i).Item("PURMATERIALGRNHUSGAS") * DsPref.Rows(0).Item("CONVWT") / SalesVolumeUnit, 3)
                            Process = FormatNumber(DsData.Rows(i).Item("PROCGRNHUSGAS") * DsPref.Rows(0).Item("CONVWT") / SalesVolumeUnit, 3)
                            Transportation = FormatNumber(DsData.Rows(i).Item("TRNSPTGRNHUSGAS") * DsPref.Rows(0).Item("CONVWT") / SalesVolumeUnit, 3)
                        Else
                            Purchased = 0
                            Process = 0
                            Transportation = 0

                        End If

                        pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEID") & ";" & Purchased & ";" & Process & ";" & Transportation & ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(1," + (i + 1).ToString() + ",Purchased Materials:" + Purchased.ToString() + ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(2," + (i + 1).ToString() + ",Process:" + Process.ToString() + ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(3," + (i + 1).ToString() + ",Transportation:" + Transportation.ToString() + ")"
                    Next
                Else
                    pcScript = GetStackbarChartDetails(DsData, DsPref, "Unit")
                End If
            End If
            GenrateChart(pcScript, GraphName, Pref)

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GetBGPerWeight(ByVal DsData As DataTable, ByVal DsPref As DataTable)
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
            Dim Transportation As New Decimal
            Dim Process As New Decimal
            Dim Purchased As New Decimal

            GraphName = "Sargento_KraftPouchvsTray"
            Graphtype = "graph"
            Cnt = DsData.Rows.Count - 1
            Pref = DsPref.Rows(0).Item("Title8").ToString() + " gr gas /" + DsPref.Rows(0).Item("Title8").ToString()

            If ddlPName.SelectedValue = "ERGY" Then
                lblHeading.Text = "Energy Page"
            Else
                lblHeading.Text = "GHG Page"
            End If
            lblHeadingS.Text = "(" + ddltype.SelectedItem.Text + " Chart" + ")"

            If ddlChartType.SelectedValue = "RBC" Then
                GraphName = "Sargento_KraftPouchvsTray"
                Graphtype = "graph"
                Cnt = DsData.Rows.Count - 1
                pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
                For i = 0 To Cnt
                    SalesVolumeLb = FormatNumber(DsData.Rows(i).Item("VOLUME"), 0)
                    If CDbl(SalesVolumeLb) <> 0.0 Then
                        Data = FormatNumber(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") / SalesVolumeLb, 3)
                    Else
                        Data = 0
                    End If
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEID") & ";" & Data & ")"
                Next
            ElseIf ddlChartType.SelectedValue = "PIE" Then
                GraphName = "Sargento_KraftPouchvsTray"
                Graphtype = "graph"
                Cnt = DsData.Rows.Count - 1
                pcScript &= Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
                For i = 0 To Cnt
                    SalesVolumeLb = FormatNumber(DsData.Rows(i).Item("VOLUME"), 0)
                    If CDbl(SalesVolumeLb) <> 0.0 Then
                        Data = FormatNumber(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") / SalesVolumeLb, 3)
                    Else
                        Data = 0
                    End If
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE").ToString() & ";" & Data & ")"
                Next
            ElseIf ddlChartType.SelectedValue = "SBAR" Then
                lblHeadingS.Text = ""
                GraphName = "Sargento_KraftPouchvsTray"
                Graphtype = "graph"
                Cnt = DsData.Rows.Count - 1
                If (ddlCategorySet.SelectedValue = "0" Or ddlCategorySet.SelectedValue = "") Then
                    pcScript &= Graphtype + ".transposed(true)" + Graphtype + ".setCategories(Purchased Materials;Process;Transportation;)"
                    For i = 0 To Cnt
                        SalesVolumeLb = FormatNumber(DsData.Rows(i).Item("VOLUME"), 0)
                        If CDbl(SalesVolumeLb) <> 0.0 Then
                            Purchased = FormatNumber(DsData.Rows(i).Item("PURMATERIALGRNHUSGAS") / SalesVolumeLb, 3)
                            Process = FormatNumber(DsData.Rows(i).Item("PROCGRNHUSGAS") / SalesVolumeLb, 3)
                            Transportation = FormatNumber(DsData.Rows(i).Item("TRNSPTGRNHUSGAS") / SalesVolumeLb, 3)
                        Else
                            Purchased = 0
                            Process = 0
                            Transportation = 0

                        End If

                        pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEID") & ";" & Purchased & ";" & Process & ";" & Transportation & ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(1," + (i + 1).ToString() + ",Purchased Materials:" + Purchased.ToString() + ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(2," + (i + 1).ToString() + ",Process:" + Process.ToString() + ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(3," + (i + 1).ToString() + ",Transportation:" + Transportation.ToString() + ")"
                    Next
                Else
                    pcScript = GetStackbarChartDetails(DsData, DsPref, "Weight")
                End If
            End If

            

            GenrateChart(pcScript, GraphName, Pref)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GetBGTotal(ByVal DsData As DataTable, ByVal DsPref As DataTable)
        Try
            Dim Data As String = String.Empty
            Dim Text As String = String.Empty
            Dim Pref As String = String.Empty
            Dim pcScript As String = String.Empty
            Dim Graphtype As String = String.Empty
            Dim GraphName As String = String.Empty
            Dim Cnt As New Integer
            Dim i As New Integer
            Dim Transportation As New Decimal
            Dim Process As New Decimal
            Dim Purchased As New Decimal

            GraphName = "Sargento_KraftPouchvsTray"
            Graphtype = "graph"
            Cnt = DsData.Rows.Count - 1
            Pref = DsPref.Rows(0).Item("TITLE8").ToString()
          
            If ddlPName.SelectedValue = "ERGY" Then
                lblHeading.Text = "Energy Page"
            Else
                lblHeading.Text = "GHG Page"
            End If
            lblHeadingS.Text = "(" + ddltype.SelectedItem.Text + " Chart" + ")"
            If ddlChartType.SelectedValue = "RBC" Then
                pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
                GraphName = "Sargento_KraftPouchvsTray"
                Graphtype = "graph"
                Cnt = DsData.Rows.Count - 1
                pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
                For i = 0 To Cnt
                    Data = FormatNumber(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * DsPref.Rows(0).Item("CONVWT"), 0)
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEID") & ";" & Data & ")"
                Next
            ElseIf ddlChartType.SelectedValue = "PIE" Then
                GraphName = "Sargento_KraftPouchvsTray"
                Graphtype = "graph"
                Cnt = DsData.Rows.Count - 1
                pcScript &= Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
                For i = 0 To Cnt
                    Data = FormatNumber(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * DsPref.Rows(0).Item("CONVWT"), 0)
                    pcScript &= "" + Graphtype + ".setSeries(" + DsData.Rows(i).Item("CASEDE").ToString() & ";" & Data & ")"
                Next
            ElseIf ddlChartType.SelectedValue = "SBAR" Then
                lblHeadingS.Text = ""
                GraphName = "Sargento_KraftPouchvsTray"
                Graphtype = "graph"
                Cnt = DsData.Rows.Count - 1
                If (ddlCategorySet.SelectedValue = "0" Or ddlCategorySet.SelectedValue = "") Then
                    pcScript &= Graphtype + ".transposed(true)" + Graphtype + ".setCategories(Purchased Materials;Process;Transportation;)"
                    For i = 0 To Cnt

                        Purchased = FormatNumber(DsData.Rows(i).Item("PURMATERIALGRNHUSGAS") * DsPref.Rows(0).Item("CONVWT"), 0)
                        Process = FormatNumber(DsData.Rows(i).Item("PROCGRNHUSGAS") * DsPref.Rows(0).Item("CONVWT"), 0)
                        Transportation = FormatNumber(DsData.Rows(i).Item("TRNSPTGRNHUSGAS") * DsPref.Rows(0).Item("CONVWT"), 0)

                        pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEID") & ";" & Purchased & ";" & Process & ";" & Transportation & ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(1," + (i + 1).ToString() + ",Purchased Materials:" + Purchased.ToString() + ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(2," + (i + 1).ToString() + ",Process:" + Process.ToString() + ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(3," + (i + 1).ToString() + ",Transportation:" + Transportation.ToString() + ")"
                    Next
                Else
                    pcScript = GetStackbarChartDetails(DsData, DsPref, "Total")
                End If
            End If

            GenrateChart(pcScript, GraphName, Pref)
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub GetBWTotal(ByVal DsData As DataTable, ByVal DsPref As DataTable)
        Try
            Dim Data As String = String.Empty
            Dim Text As String = String.Empty
            Dim Pref As String = String.Empty
            Dim pcScript As String = String.Empty
            Dim Graphtype As String = String.Empty
            Dim GraphName As String = String.Empty
            Dim Cnt As New Integer
            Dim i As New Integer
            Dim Transportation As New Decimal
            Dim Process As New Decimal
            Dim Purchased As New Decimal


            GraphName = "Sargento_KraftPouchvsTray"
            Graphtype = "graph"
            Cnt = DsData.Rows.Count - 1
            Pref = DsPref.Rows(0).Item("TITLE10").ToString()
            If ddlPName.SelectedValue = "ERGY" Then
                lblHeading.Text = "Energy Page"
            ElseIf ddlPName.SelectedValue = "WATER" Then
                lblHeading.Text = "Water Page"
            Else
                lblHeading.Text = "GHG Page"
            End If
            lblHeadingS.Text = "(" + ddltype.SelectedItem.Text + " Chart" + ")"
            If ddlChartType.SelectedValue = "RBC" Then
                pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
                GraphName = "Sargento_KraftPouchvsTray"
                Graphtype = "graph"
                Cnt = DsData.Rows.Count - 1
                pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
                For i = 0 To Cnt
                    Data = FormatNumber(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * DsPref.Rows(0).Item("CONVGALLON"), 0)
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEID") & ";" & Data & ")"
                Next
            ElseIf ddlChartType.SelectedValue = "PIE" Then
                GraphName = "Sargento_KraftPouchvsTray"
                Graphtype = "graph"
                Cnt = DsData.Rows.Count - 1
                pcScript &= Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
                For i = 0 To Cnt
                    Data = FormatNumber(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * DsPref.Rows(0).Item("CONVGALLON"), 0)
                    pcScript &= "" + Graphtype + ".setSeries(" + DsData.Rows(i).Item("CASEDE").ToString() & ";" & Data & ")"
                Next
            ElseIf ddlChartType.SelectedValue = "SBAR" Then
                lblHeadingS.Text = ""
                GraphName = "Sargento_KraftPouchvsTray"
                Graphtype = "graph"
                Cnt = DsData.Rows.Count - 1

                If (ddlCategorySet.SelectedValue = "0" Or ddlCategorySet.SelectedValue = "") Then
                    pcScript &= Graphtype + ".transposed(true)" + Graphtype + ".setCategories(Purchased Materials;Process;Transportation;)"
                    For i = 0 To Cnt
                        Purchased = FormatNumber(DsData.Rows(i).Item("PURMATERIALWATER") * DsPref.Rows(0).Item("CONVGALLON"), 0)
                        Process = FormatNumber(DsData.Rows(i).Item("PROCWATER") * DsPref.Rows(0).Item("CONVGALLON"), 0)
                        Transportation = FormatNumber(DsData.Rows(i).Item("TRNSPTWATER") * DsPref.Rows(0).Item("CONVGALLON"), 0)

                        pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEID") & ";" & Purchased & ";" & Process & ";" & Transportation & ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(1," + (i + 1).ToString() + ",Purchased Materials:" + Purchased.ToString() + ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(2," + (i + 1).ToString() + ",Process:" + Process.ToString() + ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(3," + (i + 1).ToString() + ",Transportation:" + Transportation.ToString() + ")"
                    Next
                Else
                    pcScript = GetStackbarChartDetails(DsData, DsPref, "Total")
                End If
            End If

            GenrateChart(pcScript, GraphName, Pref)
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub GetBWPerUnit(ByVal DsData As DataTable, ByVal DsPref As DataTable)
        Try

            Dim Data As String = String.Empty
            Dim Text As String = String.Empty
            Dim Pref As String = String.Empty
            Dim pcScript As String = String.Empty
            Dim Graphtype As String = String.Empty
            Dim GraphName As String = String.Empty
            Dim HoverText As String = String.Empty
            Dim Cnt As New Integer
            Dim SalesVolumeUnit As New Decimal
            Dim i As New Integer
            Dim Transportation As New Decimal
            Dim Process As New Decimal
            Dim Purchased As New Decimal

            GraphName = "Sargento_KraftPouchvsTray"
            Graphtype = "graph"
            Cnt = DsData.Rows.Count - 1

            ''FOR UNIT DISPLAY
            Dim j As New Integer
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
            Pref = DsPref.Rows(0).Item("TITLE10").ToString()
            If j = 1 Then
                Pref = Pref + " water/" + DsPref.Rows(0).Item("TITLE3").ToString()
            ElseIf j = 2 Then
                Pref = Pref + " water/unit "
            Else
                Pref = Pref + " water/(unit or " + DsPref.Rows(0).Item("TITLE3").ToString() + ")"
            End If

            If ddlPName.SelectedValue = "ERGY" Then
                lblHeading.Text = "Energy Page"
            ElseIf ddlPName.SelectedValue = "WATER" Then
                lblHeading.Text = "Water Page"
            Else
                lblHeading.Text = "GHG Page"
            End If
            lblHeadingS.Text = "(" + ddltype.SelectedItem.Text + " Chart" + ")"

            'If DsData.Rows(0).Item("FINVOLMSI") > 0 Then
            '    HoverText = DsPref.Rows(0).Item("TITLE10").ToString() + " water/" + DsPref.Rows(0).Item("TITLE3").ToString()
            'ElseIf DsData.Rows(i).Item("FINVOLMUNITS") > 0 Then
            '    HoverText = DsPref.Rows(0).Item("TITLE10").ToString() + " water/Unit"
            'End If
            'Pref = HoverText
            If ddlChartType.SelectedValue = "RBC" Then
                GraphName = "Sargento_KraftPouchvsTray"
                Graphtype = "graph"
                Cnt = DsData.Rows.Count - 1
                pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"

                For i = 0 To Cnt
                    If DsData.Rows(i).Item("FINVOLMSI") > 0 Then
                        SalesVolumeUnit = FormatNumber(DsData.Rows(i).Item("FINVOLMSI") * DsPref.Rows(0).Item("ConvArea"), 0)
                    ElseIf DsData.Rows(i).Item("FINVOLMUNITS") > 0 Then
                        SalesVolumeUnit = DsData.Rows(i).Item("FINVOLMUNITS")
                    End If
                    If CDbl(SalesVolumeUnit) <> 0.0 Then
                        Data = FormatNumber(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * DsPref.Rows(0).Item("CONVGALLON") / SalesVolumeUnit, 3)
                    Else
                        Data = 0
                    End If
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEID") & ";" & Data & ")"
                Next
            ElseIf ddlChartType.SelectedValue = "PIE" Then
                GraphName = "Sargento_KraftPouchvsTray"
                Graphtype = "graph"
                Cnt = DsData.Rows.Count - 1
                pcScript &= Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
                For i = 0 To Cnt
                    If DsData.Rows(i).Item("FINVOLMSI") > 0 Then
                        SalesVolumeUnit = FormatNumber(DsData.Rows(i).Item("FINVOLMSI") * DsPref.Rows(0).Item("ConvArea"), 0)
                    ElseIf DsData.Rows(i).Item("FINVOLMUNITS") > 0 Then
                        SalesVolumeUnit = DsData.Rows(i).Item("FINVOLMUNITS")
                    End If
                    If CDbl(SalesVolumeUnit) <> 0.0 Then
                        Data = FormatNumber(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * DsPref.Rows(0).Item("CONVGALLON") / SalesVolumeUnit, 3)
                    Else
                        Data = 0
                    End If
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE").ToString() & ";" & Data & ")"
                Next
            ElseIf ddlChartType.SelectedValue = "SBAR" Then
                lblHeadingS.Text = ""
                GraphName = "Sargento_KraftPouchvsTray"
                Graphtype = "graph"
                Cnt = DsData.Rows.Count - 1

                If (ddlCategorySet.SelectedValue = "0" Or ddlCategorySet.SelectedValue = "") Then
                    pcScript &= Graphtype + ".transposed(true)" + Graphtype + ".setCategories(Purchased Materials;Process;Transportation;)"
                    For i = 0 To Cnt
                        If DsData.Rows(i).Item("FINVOLMSI") > 0 Then
                            SalesVolumeUnit = FormatNumber(DsData.Rows(i).Item("FINVOLMSI") * DsPref.Rows(0).Item("ConvArea"), 0)
                        ElseIf DsData.Rows(i).Item("FINVOLMUNITS") > 0 Then
                            SalesVolumeUnit = DsData.Rows(i).Item("FINVOLMUNITS")
                        End If
                        If CDbl(SalesVolumeUnit) <> 0.0 Then
                            Purchased = FormatNumber(DsData.Rows(i).Item("PURMATERIALWATER") * DsPref.Rows(0).Item("CONVGALLON") / SalesVolumeUnit, 3)
                            Process = FormatNumber(DsData.Rows(i).Item("PROCWATER") * DsPref.Rows(0).Item("CONVGALLON") / SalesVolumeUnit, 3)
                            Transportation = FormatNumber(DsData.Rows(i).Item("TRNSPTWATER") * DsPref.Rows(0).Item("CONVGALLON") / SalesVolumeUnit, 3)
                        Else
                            Purchased = 0
                            Process = 0
                            Transportation = 0
                        End If

                        pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEID") & ";" & Purchased & ";" & Process & ";" & Transportation & ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(1," + (i + 1).ToString() + ",Purchased Materials:" + Purchased.ToString() + ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(2," + (i + 1).ToString() + ",Process:" + Process.ToString() + ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(3," + (i + 1).ToString() + ",Transportation:" + Transportation.ToString() + ")"
                    Next
                Else
                    pcScript = GetStackbarChartDetails(DsData, DsPref, "Unit")
                End If
            End If
            GenrateChart(pcScript, GraphName, Pref)

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GetBWPerWeight(ByVal DsData As DataTable, ByVal DsPref As DataTable)
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
            Dim Transportation As New Decimal
            Dim Process As New Decimal
            Dim Purchased As New Decimal
            Dim HoverText As String = String.Empty

            GraphName = "Sargento_KraftPouchvsTray"
            Graphtype = "graph"
            Cnt = DsData.Rows.Count - 1


            If ddlPName.SelectedValue = "ERGY" Then
                lblHeading.Text = "Energy Page"
            ElseIf ddlPName.SelectedValue = "WATER" Then
                lblHeading.Text = "Water Page"
            Else
                lblHeading.Text = "GHG Page"
            End If
            Pref = DsPref.Rows(0).Item("TITLE10").ToString() + " water/" + DsPref.Rows(0).Item("TITLE8").ToString()

            lblHeadingS.Text = "(" + ddltype.SelectedItem.Text + " Chart" + ")"

            If ddlChartType.SelectedValue = "RBC" Then
                GraphName = "Sargento_KraftPouchvsTray"
                Graphtype = "graph"
                Cnt = DsData.Rows.Count - 1
                pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
                For i = 0 To Cnt
                    SalesVolumeLb = FormatNumber(DsData.Rows(i).Item("VOLUME"), 0)
                    If CDbl(SalesVolumeLb) <> 0.0 Then
                        Data = FormatNumber(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * DsPref.Rows(0).Item("CONVGALLON") / (SalesVolumeLb * DsPref.Rows(0).Item("CONVWT")), 3)
                    Else
                        Data = 0
                    End If
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEID") & ";" & Data & ")"
                Next
            ElseIf ddlChartType.SelectedValue = "PIE" Then
                GraphName = "Sargento_KraftPouchvsTray"
                Graphtype = "graph"
                Cnt = DsData.Rows.Count - 1
                pcScript &= Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
                For i = 0 To Cnt
                    SalesVolumeLb = FormatNumber(DsData.Rows(i).Item("VOLUME"), 0)
                    If CDbl(SalesVolumeLb) <> 0.0 Then
                        Data = FormatNumber(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * DsPref.Rows(0).Item("CONVGALLON") / (SalesVolumeLb * DsPref.Rows(0).Item("CONVWT")), 3)
                    Else
                        Data = 0
                    End If
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE").ToString() & ";" & Data & ")"
                Next
            ElseIf ddlChartType.SelectedValue = "SBAR" Then
                lblHeadingS.Text = ""
                GraphName = "Sargento_KraftPouchvsTray"
                Graphtype = "graph"
                Cnt = DsData.Rows.Count - 1

                If (ddlCategorySet.SelectedValue = "0" Or ddlCategorySet.SelectedValue = "") Then
                    pcScript &= Graphtype + ".transposed(true)" + Graphtype + ".setCategories(Purchased Materials;Process;Transportation;)"
                    For i = 0 To Cnt
                        SalesVolumeLb = FormatNumber(DsData.Rows(i).Item("VOLUME"), 0)
                        If CDbl(SalesVolumeLb) <> 0.0 Then
                            Purchased = FormatNumber(DsData.Rows(i).Item("PURMATERIALWATER") * DsPref.Rows(0).Item("CONVGALLON") / (SalesVolumeLb * DsPref.Rows(0).Item("CONVWT")), 3)
                            Process = FormatNumber(DsData.Rows(i).Item("PROCWATER") * DsPref.Rows(0).Item("CONVGALLON") / (SalesVolumeLb * DsPref.Rows(0).Item("CONVWT")), 3)
                            Transportation = FormatNumber(DsData.Rows(i).Item("TRNSPTWATER") * DsPref.Rows(0).Item("CONVGALLON") / (SalesVolumeLb * DsPref.Rows(0).Item("CONVWT")), 3)
                        Else
                            Purchased = 0
                            Process = 0
                            Transportation = 0
                        End If

                        pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEID") & ";" & Purchased & ";" & Process & ";" & Transportation & ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(1," + (i + 1).ToString() + ",Purchased Materials:" + Purchased.ToString() + ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(2," + (i + 1).ToString() + ",Process:" + Process.ToString() + ")"
                        pcScript = pcScript + "" + Graphtype + ".addHoverText(3," + (i + 1).ToString() + ",Transportation:" + Transportation.ToString() + ")"
                    Next
                Else
                    pcScript = GetStackbarChartDetails(DsData, DsPref, "Weight")
                End If
            End If



            GenrateChart(pcScript, GraphName, Pref)
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub GenrateChart(ByVal PcScript As String, ByVal GraphName As String, ByVal Pref As String)
        Try
            'Dim control As HtmlGenericControl = FindControl("ctl00_Sustain1ContentPlaceHolder_EnergyComp")
            Dim dsChartSetting As New DataTable
            Dim objGetData As New Configration.Selectdata()
            dsChartSetting = objGetData.GetChartSettings().Tables(0)

            Dim myImage As CordaEmbedder = New CordaEmbedder()
            'myImage.externalServerAddress = dsChartSetting.Rows(0)("EXTSERVERADD").ToString()
 Dim currenturl = Request.ServerVariables("HTTP_HOST")
 If currenturl.Contains("www.savvypack.com") Or currenturl.Contains("savvypack.com") Then
                myImage.externalServerAddress = dsChartSetting.Rows(0)("EXTSERVERADD").ToString()
Else
		myImage.externalServerAddress = "http://192.168.3.31:3001/"
End If
            myImage.internalCommPortAddress = dsChartSetting.Rows(0)("INTCOMPORTADD").ToString()
            ' myImage.imageTemplate = "Sargento_KraftPouchvsTray_Html" + ".itxml"
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
            EnergyComp.InnerHtml = myImage.getEmbeddingHTML
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub bynUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bynUpdate.Click
        Try

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub ddlChartType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlChartType.SelectedIndexChanged
        Try
            If ddlChartType.SelectedValue = "SBAR" Then
                ddltype.Enabled = False
                ROWCategoryset.Visible = True
                BindCategorySet()
            Else
                ddltype.Enabled = True
                ROWCategoryset.Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub BindCategorySet()
        Dim dt As New DataTable
        Dim ObjGetData As New S3GetData.Selectdata
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
            Response.Write("S3Error:BindCategorySet:" + ex.Message.ToString())
        End Try
    End Sub
    Function GetStackbarChartDetails(ByVal DsData As DataTable, ByVal DsPref As DataTable, ByVal Contype As String) As String
        Dim ObjGetS3data As New S3GetData.Selectdata
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
        Dim prefUnit As String = String.Empty
        Try
            GraphName = "Sargento_KraftPouchvsTray"
            Graphtype = "graph"
            Cnt = DsData.Rows.Count - 1
            pcSetCategories &= Graphtype + ".transposed(true)" + Graphtype + ".setCategories("
            dsCategories = ObjGetS3data.GetCategoryBySet(ddlCategorySet.SelectedValue, "")


            If PageName = "ERGY" Then
                prefUnit = 1
            ElseIf PageName = "GHG" Then
                prefUnit = DsPref.Rows(0).Item("CONVWT")
            ElseIf PageName = "WATER" Then
                prefUnit = DsPref.Rows(0).Item("CONVGALLON")
            End If

            For k = 0 To Cnt

                If DsData.Rows(k).Item("FINVOLMSI") > 0 Then
                    SalesVolumeUnit = DsData.Rows(k).Item("FINVOLMSI") * DsPref.Rows(0).Item("ConvArea")
                ElseIf DsData.Rows(k).Item("FINVOLMUNITS") > 0 Then
                    SalesVolumeUnit = DsData.Rows(k).Item("FINVOLMUNITS")
                End If


                SalesVolumeLb = DsData.Rows(k).Item("VOLUME")

                pcsetSeries = pcsetSeries + "" + Graphtype + ".setSeries(" & DsData.Rows(k).Item("CASEID") & ";"
                For i = 0 To dsCategories.Rows.Count - 1
                    dsitems = ObjGetS3data.GetCategoryItemBycategory(dsCategories.Rows(i).Item("Categoryid").ToString())

                    pcAddHoveText = pcAddHoveText + "" + Graphtype + ".addHoverText(" + (i + 1).ToString() + "," + (k + 1).ToString()
                    matCost = 0


                    For j = 0 To dsitems.Tables(0).Rows.Count - 1

                        If (Contype = "Total") Then
                            matCost = FormatNumber(matCost + DsData.Rows(k).Item("" + dsitems.Tables(0).Rows(j).Item("ITEMNAME").ToString() + "") * prefUnit, 0)
                        ElseIf (Contype = "Unit") Then
                            If SalesVolumeUnit > 0 Then
                                matCost = matCost + DsData.Rows(k).Item("" + dsitems.Tables(0).Rows(j).Item("ITEMNAME").ToString() + "") * prefUnit / SalesVolumeUnit
                                matCost = FormatNumber(matCost, 3)
                            End If
                        ElseIf (Contype = "Weight") Then
                            If SalesVolumeLb <> 0 Then
                                matCost = matCost + DsData.Rows(k).Item("" + dsitems.Tables(0).Rows(j).Item("ITEMNAME").ToString() + "") * prefUnit / (SalesVolumeLb * DsPref.Rows(0).Item("CONVWT"))
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
End Class
