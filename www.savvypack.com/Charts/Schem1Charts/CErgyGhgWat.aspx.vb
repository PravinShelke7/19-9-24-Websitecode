Imports Corda
Imports System.Web.UI.HtmlControls
Imports System.Data
Imports System.Data.OleDb
Imports System.Web.HttpBrowserCapabilities
Imports Schem1GetData
Imports Configration
Partial Class Charts_Schem1Charts_CErgyGhgWat
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
    Dim _strErgyGHG As String
    Dim _strChartType As String
    Dim _strChartName As String

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
        objRefresh = New zCon.Net.Refresh("_CHARTS_SCHEM1CHARTS_CERGYGHGWAT")
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
        Try
            GetMasterPageControls()

            CaseId1 = Session("Schem1CaseId")
            CaseId2 = objCryptoHelper.Decrypt(Request.QueryString("CaseId"))
            ErgyGHG = objCryptoHelper.Decrypt(Request.QueryString("ErgyGHG"))
            ChartType = objCryptoHelper.Decrypt(Request.QueryString("ChartType"))
            dsData = GetData(CaseId1, CaseId2)
            dsPref = GetPref(Session("USERID"))
            GetPageTitle()
            If Not IsPostBack Then
                Bindddl()
            End If
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

        Catch ex As Exception
            ErrorLable.Text = "Error:Page_Load:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetPageTitle()
        Try
            If ErgyGHG = "ERGY" Then
                If ChartType = "RBC" Then
                    Page.Title = "Schem1-Combine Energy Bar Chart"
                    rwType.Visible = True
                Else
                    Page.Title = "Schem1-Combine Energy Stack Chart"
                    rwType.Visible = False
                End If
            ElseIf ErgyGHG = "GHG" Then
                If ChartType = "RBC" Then
                    Page.Title = "Schem1-Combine GHG Bar Chart"
                    rwType.Visible = True
                Else
                    Page.Title = "Schem1-Combine GHG Stack Chart"
                    rwType.Visible = False
                End If
            ElseIf ErgyGHG = "WATER" Then
                If ChartType = "RBC" Then
                    Page.Title = "Schem1-Combine Water Bar Chart"
                    rwType.Visible = True
                Else
                    Page.Title = "Schem1-Combine Water Stack Chart"
                    rwType.Visible = False
                End If
            End If
        Catch ex As Exception
            ErrorLable.Text = "Error:GetPageTitle:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub Bindddl()
        Dim ds As New DataSet
        Dim objGetData As New Schem1GetData.Selectdata()
        Try
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

    Protected Function GetData(ByVal CaseId1 As String, ByVal CaseId2 As String) As DataSet
        Dim ds As New DataSet
        Dim objGetData As New Schem1GetData.Selectdata()
        Try

            If ErgyGHG = "ERGY" Then
                ds = objGetData.GetChartErgyRes(CaseId1, CaseId2)
            ElseIf ErgyGHG = "WATER" Then
                ds = objGetData.GetChartWaterRes(CaseId1, CaseId2)
            Else
                ds = objGetData.GetChartGhgRes(CaseId1, CaseId2)
            End If
            Return ds
        Catch ex As Exception
            ErrorLable.Text = "Error:GetData:" + ex.Message.ToString()
            Return ds
        End Try
    End Function

    Protected Function GetPref(ByVal USERID As String) As DataSet
        Dim ds As New DataSet
        Dim objGetData As New Schem1GetData.Selectdata()
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
            Dim Transportation As String = String.Empty
            Dim Process As String = String.Empty
            Dim Purchased As String = String.Empty
            Dim Cnt As New Integer
            Dim SalesVolumeUnit As New Decimal
            Dim i As New Integer

            GraphName = "Sargento_KraftPouchvsTray"
            Graphtype = "graph"
            Cnt = DsData.Rows.Count - 1

            If ChartType = "RBC" Then
                lblHeading.Text = ddltype.SelectedItem.Text + " Energy Chart"
                pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
            Else
                lblHeading.Text = "Energy Per Unit Chart"
                pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(Transportation;Process;Purchased Materials)"
            End If
            For i = 0 To Cnt
                If DsData.Rows(i).Item("FINVOLMUNITS") > 0 Then
                    HoverText = "MJ/Unit"
                    SalesVolumeUnit = DsData.Rows(i).Item("FINVOLMUNITS")
                End If
                Pref = HoverText
                If ChartType = "RBC" Then
                    If SalesVolumeUnit > 0 Then
                        Data = FormatNumber(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") / SalesVolumeUnit, 3)
                    Else
                        Data = "0"
                    End If
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & Data & ")"
                    pcScript = pcScript + Graphtype + ".addHoverText(1," + (i + 2).ToString() + ",Value:" + Data.ToString() + "<br/>Unit:" + HoverText.ToString() + ")"
                Else
                    If SalesVolumeUnit > 0 Then
                        Transportation = FormatNumber(DsData.Rows(i).Item("TRNSPTERGY") / SalesVolumeUnit, 3)
                        Process = FormatNumber(DsData.Rows(i).Item("TPROCERGY") / SalesVolumeUnit, 3)
                        Purchased = FormatNumber(DsData.Rows(i).Item("PURMATERIALERGY") / SalesVolumeUnit, 3)
                    Else
                        Transportation = "0"
                        Process = "0"
                        Purchased = "0"
                    End If
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & Transportation & ";" & Process & ";" & Purchased & ")"
                    pcScript = pcScript + "" + Graphtype + ".addHoverText(1," + (i + 1).ToString() + ",Value:" + Purchased.ToString() + "<br/>Unit:" + HoverText.ToString() + ")"
                    pcScript = pcScript + "" + Graphtype + ".addHoverText(2," + (i + 1).ToString() + ",Value:" + Process.ToString() + "<br/>Unit:" + HoverText.ToString() + ")"
                    pcScript = pcScript + "" + Graphtype + ".addHoverText(3," + (i + 1).ToString() + ",Value:" + Transportation.ToString() + "<br/>Unit:" + HoverText.ToString() + ")"
                End If
            Next

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
            Dim Transportation As String = String.Empty
            Dim Process As String = String.Empty
            Dim Purchased As String = String.Empty
            Dim i As New Integer

            GraphName = "Sargento_KraftPouchvsTray"
            Graphtype = "graph"
            Cnt = DsData.Rows.Count - 1
            Pref = "MJ/" + DsPref.Rows(0).Item("Title8").ToString()

            If ChartType = "RBC" Then
                lblHeading.Text = ddltype.SelectedItem.Text + " Energy Chart"
                pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
            Else
                lblHeading.Text = "Energy Per Weight Chart"
                pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(Transportation;Process;Purchased Materials)"
            End If
            For i = 0 To Cnt
                If CDbl(DsData.Rows(i).Item("PVOLUSE")) = 0.0 Then
                    SalesVolumeLb = DsData.Rows(i).Item("VOLUME") * DsPref.Rows(0).Item("CONVWT")
                Else
                    SalesVolumeLb = DsData.Rows(i).Item("SVOLUME") * DsPref.Rows(0).Item("CONVWT")
                End If

                If ChartType = "RBC" Then
                    If SalesVolumeLb > 0 Then
                        Data = FormatNumber(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") / SalesVolumeLb, 3)
                    Else
                        Data = "0"
                    End If
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & Data & ")"
                Else
                    If SalesVolumeLb > 0 Then
                        Transportation = FormatNumber(DsData.Rows(i).Item("TRNSPTERGY") / SalesVolumeLb, 3)
                        Process = FormatNumber(DsData.Rows(i).Item("TPROCERGY") / SalesVolumeLb, 3)
                        Purchased = FormatNumber(DsData.Rows(i).Item("PURMATERIALERGY") / SalesVolumeLb, 3)
                    Else
                        Transportation = "0"
                        Process = "0"
                        Purchased = "0"
                    End If
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & Transportation & ";" & Process & ";" & Purchased & ")"
                End If
            Next

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
            Dim Transportation As String = String.Empty
            Dim Process As String = String.Empty
            Dim Purchased As String = String.Empty


            GraphName = "Sargento_KraftPouchvsTray"
            Graphtype = "graph"
            Cnt = DsData.Rows.Count - 1
            Pref = "MJ"

            If ChartType = "RBC" Then
                lblHeading.Text = ddltype.SelectedItem.Text + " Energy Chart"
                pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
            Else
                lblHeading.Text = "Total Energy Chart"
                pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(Transportation;Process;Purchased Materials)"
            End If
            For i = 0 To Cnt
                If ChartType = "RBC" Then
                    Data = FormatNumber(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + ""), 0)

                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & Data & ")"
                Else
                    Transportation = FormatNumber(DsData.Rows(i).Item("TRNSPTERGY"), 0)
                    Process = FormatNumber(DsData.Rows(i).Item("TPROCERGY"), 0)
                    Purchased = FormatNumber(DsData.Rows(i).Item("PURMATERIALERGY"), 0)

                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & Transportation & ";" & Process & ";" & Purchased & ")"
                End If
            Next

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
            Dim Transportation As String = String.Empty
            Dim Process As String = String.Empty
            Dim Purchased As String = String.Empty

            GraphName = "Sargento_KraftPouchvsTray"
            Graphtype = "graph"
            Cnt = DsData.Rows.Count - 1




            If ChartType = "RBC" Then
                lblHeading.Text = ddltype.SelectedItem.Text + " Water Chart"
                pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
            Else
                pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(Transportation;Process;Purchased Materials)"
            End If

            For i = 0 To Cnt
                If DsData.Rows(i).Item("FINVOLMUNITS") > 0 Then
                    SalesVolumeUnit = DsData.Rows(i).Item("FINVOLMUNITS")
                    HoverText = DsPref.Rows(0).Item("TITLE10").ToString() + " water/Unit"
                End If
                Pref = HoverText
                If ChartType = "RBC" Then
                    If SalesVolumeUnit > 0 Then
                        Data = FormatNumber((DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * DsPref.Rows(0).Item("CONVGALLON")) / SalesVolumeUnit, 3)
                    Else
                        Data = "0"
                    End If
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & Data & ")"
                    pcScript = pcScript + Graphtype + ".addHoverText(1," + (i + 10).ToString() + ",Value:" + Data.ToString() + "<br/>Unit:" + HoverText.ToString() + ")"
                Else
                    If SalesVolumeUnit > 0 Then
                        Transportation = FormatNumber((DsData.Rows(i).Item("TRNSPTWATER") * DsPref.Rows(0).Item("CONVGALLON")) / SalesVolumeUnit, 3)
                        Process = FormatNumber((DsData.Rows(i).Item("PROCESSWATER") * DsPref.Rows(0).Item("CONVGALLON")) / SalesVolumeUnit, 3)
                        Purchased = FormatNumber((DsData.Rows(i).Item("PURMATERIALWATER") * DsPref.Rows(0).Item("CONVGALLON")) / SalesVolumeUnit, 3)
                    Else
                        Transportation = "0"
                        Process = "0"
                        Purchased = "0"
                    End If
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & Transportation & ";" & Process & ";" & Purchased & ")"
                    pcScript = pcScript + "" + Graphtype + ".addHoverText(1," + (i + 1).ToString() + ",Value:" + Purchased.ToString() + "<br/>Unit:" + HoverText.ToString() + ")"
                    pcScript = pcScript + "" + Graphtype + ".addHoverText(2," + (i + 1).ToString() + ",Value:" + Process.ToString() + "<br/>Unit:" + HoverText.ToString() + ")"
                    pcScript = pcScript + "" + Graphtype + ".addHoverText(3," + (i + 1).ToString() + ",Value:" + Transportation.ToString() + "<br/>Unit:" + HoverText.ToString() + ")"
                End If


            Next

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
            Dim Transportation As String = String.Empty
            Dim Process As String = String.Empty
            Dim Purchased As String = String.Empty

            GraphName = "Sargento_KraftPouchvsTray"
            Graphtype = "graph"
            Cnt = DsData.Rows.Count - 1




            If ChartType = "RBC" Then
                lblHeading.Text = ddltype.SelectedItem.Text + " GHG Chart"
                pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
            Else
                pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(Transportation;Process;Purchased Materials)"
            End If

            For i = 0 To Cnt
                If DsData.Rows(i).Item("FINVOLMUNITS") > 0 Then
                    SalesVolumeUnit = DsData.Rows(i).Item("FINVOLMUNITS")
                    HoverText = DsPref.Rows(0).Item("TITLE8").ToString() + " gr gas/Unit"
                End If
                Pref = HoverText
                If ChartType = "RBC" Then
                    If SalesVolumeUnit > 0 Then
                        Data = FormatNumber(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * DsPref.Rows(0).Item("CONVWT") / SalesVolumeUnit, 3)
                    Else
                        Data = "0"
                    End If
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & Data & ")"
                    pcScript = pcScript + Graphtype + ".addHoverText(1," + (i + 1).ToString() + ",Value:" + Data.ToString() + "<br/>Unit:" + HoverText.ToString() + ")"
                Else
                    If SalesVolumeUnit > 0 Then
                        Transportation = FormatNumber(DsData.Rows(i).Item("TRNSPTGRNHUSGAS") * DsPref.Rows(0).Item("CONVWT") / SalesVolumeUnit, 3)
                        Process = FormatNumber(DsData.Rows(i).Item("PROCESSGRNHUSGAS") * DsPref.Rows(0).Item("CONVWT") / SalesVolumeUnit, 3)
                        Purchased = FormatNumber(DsData.Rows(i).Item("PURMATERIALGRNHUSGAS") * DsPref.Rows(0).Item("CONVWT") / SalesVolumeUnit, 3)
                    Else
                        Transportation = "0"
                        Process = "0"
                        Purchased = "0"
                    End If
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & Transportation & ";" & Process & ";" & Purchased & ")"
                    pcScript = pcScript + "" + Graphtype + ".addHoverText(1," + (i + 1).ToString() + ",Value:" + Purchased.ToString() + "<br/>Unit:" + HoverText.ToString() + ")"
                    pcScript = pcScript + "" + Graphtype + ".addHoverText(2," + (i + 1).ToString() + ",Value:" + Process.ToString() + "<br/>Unit:" + HoverText.ToString() + ")"
                    pcScript = pcScript + "" + Graphtype + ".addHoverText(3," + (i + 1).ToString() + ",Value:" + Transportation.ToString() + "<br/>Unit:" + HoverText.ToString() + ")"
                End If


            Next

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
            Dim Transportation As String = String.Empty
            Dim Process As String = String.Empty
            Dim Purchased As String = String.Empty

            GraphName = "Sargento_KraftPouchvsTray"
            Graphtype = "graph"
            Cnt = DsData.Rows.Count - 1
            Pref = DsPref.Rows(0).Item("Title10").ToString() + " water /" + DsPref.Rows(0).Item("Title8").ToString()

            If ChartType = "RBC" Then
                lblHeading.Text = ddltype.SelectedItem.Text + " Water Chart"
                pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
            Else
                lblHeading.Text = "Water Per Weight Chart"
                pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(Transportation;Process;Purchased Materials)"
            End If

            For i = 0 To Cnt
                If CDbl(DsData.Rows(i).Item("PVOLUSE")) = 0.0 Then
                    SalesVolumeLb = DsData.Rows(i).Item("VOLUME")
                Else
                    SalesVolumeLb = DsData.Rows(i).Item("SVOLUME")
                End If

                If ChartType = "RBC" Then
                    If SalesVolumeLb > 0 Then
                        Data = FormatNumber((DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * DsPref.Rows(0).Item("CONVGALLON")) / (SalesVolumeLb * DsPref.Rows(0).Item("CONVWT")), 3)
                    Else
                        Data = "0"
                    End If
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & Data & ")"
                Else
                    If SalesVolumeLb > 0 Then
                        Transportation = FormatNumber((DsData.Rows(i).Item("TRNSPTWATER") * DsPref.Rows(0).Item("CONVGALLON")) / (SalesVolumeLb * DsPref.Rows(0).Item("CONVWT")), 3)
                        Process = FormatNumber((DsData.Rows(i).Item("PROCESSWATER") * DsPref.Rows(0).Item("CONVGALLON")) / (SalesVolumeLb * DsPref.Rows(0).Item("CONVWT")), 3)
                        Purchased = FormatNumber((DsData.Rows(i).Item("PURMATERIALWATER") * DsPref.Rows(0).Item("CONVGALLON")) / (SalesVolumeLb * DsPref.Rows(0).Item("CONVWT")), 3)
                    Else
                        Transportation = "0"
                        Process = "0"
                        Purchased = "0"
                    End If
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & Transportation & ";" & Process & ";" & Purchased & ")"
                End If

            Next

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
            Dim Transportation As String = String.Empty
            Dim Process As String = String.Empty
            Dim Purchased As String = String.Empty

            GraphName = "Sargento_KraftPouchvsTray"
            Graphtype = "graph"
            Cnt = DsData.Rows.Count - 1
            Pref = DsPref.Rows(0).Item("Title8").ToString() + " gr gas /" + DsPref.Rows(0).Item("Title8").ToString()

            If ChartType = "RBC" Then
                lblHeading.Text = ddltype.SelectedItem.Text + " GHG Chart"
                pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
            Else
                lblHeading.Text = "GHG Per Weight Chart"
                pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(Transportation;Process;Purchased Materials)"
            End If

            For i = 0 To Cnt
                If CDbl(DsData.Rows(i).Item("PVOLUSE")) = 0.0 Then
                    SalesVolumeLb = DsData.Rows(i).Item("VOLUME")
                Else
                    SalesVolumeLb = DsData.Rows(i).Item("SVOLUME")
                End If
                If ChartType = "RBC" Then
                    If SalesVolumeLb > 0 Then
                        Data = FormatNumber(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") / SalesVolumeLb, 3)
                    Else
                        Data = "0"
                    End If
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & Data & ")"
                Else
                    If SalesVolumeLb > 0 Then
                        Transportation = FormatNumber(DsData.Rows(i).Item("TRNSPTGRNHUSGAS") / SalesVolumeLb, 3)
                        Process = FormatNumber(DsData.Rows(i).Item("PROCESSGRNHUSGAS") / SalesVolumeLb, 3)
                        Purchased = FormatNumber(DsData.Rows(i).Item("PURMATERIALGRNHUSGAS") / SalesVolumeLb, 3)
                    Else
                        Transportation = "0"
                        Process = "0"
                        Purchased = "0"
                    End If
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & Transportation & ";" & Process & ";" & Purchased & ")"
                End If

            Next

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
            Dim Transportation As String = String.Empty
            Dim Process As String = String.Empty
            Dim Purchased As String = String.Empty

            GraphName = "Sargento_KraftPouchvsTray"
            Graphtype = "graph"
            Cnt = DsData.Rows.Count - 1
            Pref = DsPref.Rows(0).Item("TITLE10").ToString()
            If ChartType = "RBC" Then
                lblHeading.Text = ddltype.SelectedItem.Text + " Water Chart"
                pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
            Else
                lblHeading.Text = "Total Water Chart"
                pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(Transportation;Process;Purchased Materials)"
            End If

            For i = 0 To Cnt
                If ChartType = "RBC" Then

                    Data = FormatNumber(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * DsPref.Rows(0).Item("CONVGALLON"), 0)
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & Data & ")"
                Else
                    Transportation = FormatNumber(DsData.Rows(i).Item("TRNSPTWATER") * DsPref.Rows(0).Item("CONVGALLON"), 3)
                    Process = FormatNumber(DsData.Rows(i).Item("PROCESSWATER") * DsPref.Rows(0).Item("CONVGALLON"), 3)
                    Purchased = FormatNumber(DsData.Rows(i).Item("PURMATERIALWATER") * DsPref.Rows(0).Item("CONVGALLON"), 3)
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & Transportation & ";" & Process & ";" & Purchased & ")"
                End If
            Next

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
            Dim Transportation As String = String.Empty
            Dim Process As String = String.Empty
            Dim Purchased As String = String.Empty

            GraphName = "Sargento_KraftPouchvsTray"
            Graphtype = "graph"
            Cnt = DsData.Rows.Count - 1
            Pref = DsPref.Rows(0).Item("TITLE8").ToString()
            If ChartType = "RBC" Then
                lblHeading.Text = ddltype.SelectedItem.Text + " GHG Chart"
                pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
            Else
                lblHeading.Text = "Total GHG Chart"
                pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(Transportation;Process;Purchased Materials)"
            End If

            For i = 0 To Cnt
                If ChartType = "RBC" Then
                    Data = FormatNumber(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * DsPref.Rows(0).Item("CONVWT"), 0)
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & Data & ")"
                Else
                    Transportation = FormatNumber(DsData.Rows(i).Item("TRNSPTGRNHUSGAS") * DsPref.Rows(0).Item("CONVWT"), 0)
                    Process = FormatNumber(DsData.Rows(i).Item("PROCESSGRNHUSGAS") * DsPref.Rows(0).Item("CONVWT"), 0)
                    Purchased = FormatNumber(DsData.Rows(i).Item("PURMATERIALGRNHUSGAS") * DsPref.Rows(0).Item("CONVWT"), 0)
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & Transportation & ";" & Process & ";" & Purchased & ")"
                End If
            Next

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
            myImage.externalServerAddress = dsChartSetting.Rows(0)("EXTSERVERADD").ToString()
            myImage.internalCommPortAddress = dsChartSetting.Rows(0)("INTCOMPORTADD").ToString()
            myImage.imageTemplate = "Sargento_KraftPouchvsTray_Html" + ".itxml"
            myImage.userAgent = Request.UserAgent
            myImage.width = 700
            myImage.height = 400
            myImage.returnDescriptiveLink = True
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
End Class
