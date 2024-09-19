Imports Corda
Imports System.Web.UI.HtmlControls
Imports System.Data
Imports System.Data.OleDb
Imports System.Web.HttpBrowserCapabilities
Imports S1GetData
Imports Configration
Partial Class Charts_S1Charts_CErgyGhgWat
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
        objRefresh = New zCon.Net.Refresh("_CHARTS_S1CHARTS_CERGYGHGWAT")
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

            CaseId1 = Session("S1CaseId")
            CaseId2 = objCryptoHelper.Decrypt(Request.QueryString("CaseId"))
            ErgyGHG = objCryptoHelper.Decrypt(Request.QueryString("ErgyGHG"))
            ChartType = objCryptoHelper.Decrypt(Request.QueryString("ChartType"))
            dsData = GetData(CaseId1, CaseId2)
            dsPref = GetPref(Session("S1UserName"))
            GetPageTitle()
            If Not IsPostBack Then
                Bindddl()
            End If
            If ErgyGHG = "CEOL" Then
                CnvT2.Visible = True
                CnvT1.Visible = False
                GetEOLResults(dsData.Tables(0), dsPref.Tables(0))
            Else
                If ddlCnType.SelectedValue = "Total" Then
                    If ErgyGHG = "ERGY" Then
                        GetBETotal(dsData.Tables(0), dsPref.Tables(0))
                    ElseIf ErgyGHG = "CERGY" Then
                        GetBETotalC(dsData.Tables(0), dsPref.Tables(0))
                    ElseIf ErgyGHG = "GHG" Then
                        GetBGTotal(dsData.Tables(0), dsPref.Tables(0))
                    ElseIf ErgyGHG = "CGHG" Then
                        GetBGTotalC(dsData.Tables(0), dsPref.Tables(0))
                    ElseIf ErgyGHG = "WATER" Then
                        GetBWTotal(dsData.Tables(0), dsPref.Tables(0))
                    ElseIf ErgyGHG = "CWATER" Then
                        GetBWTotalC(dsData.Tables(0), dsPref.Tables(0))
                    End If
                ElseIf ddlCnType.SelectedValue = "PUnit" Then
                    If ErgyGHG = "ERGY" Then
                        GetBEPerUnit(dsData.Tables(0), dsPref.Tables(0))
                    ElseIf ErgyGHG = "CERGY" Then
                        GetBEPerUnitC(dsData.Tables(0), dsPref.Tables(0))
                    ElseIf ErgyGHG = "GHG" Then
                        GetBGPerUnit(dsData.Tables(0), dsPref.Tables(0))
                    ElseIf ErgyGHG = "CGHG" Then
                        GetBGPerUnitC(dsData.Tables(0), dsPref.Tables(0))
                    ElseIf ErgyGHG = "WATER" Then
                        GetBWPerUnit(dsData.Tables(0), dsPref.Tables(0))
                    ElseIf ErgyGHG = "CWATER" Then
                        GetBWPerUnitC(dsData.Tables(0), dsPref.Tables(0))
                    End If
                ElseIf ddlCnType.SelectedValue = "PWeight" Then
                    If ErgyGHG = "ERGY" Then
                        GetBEPerWeight(dsData.Tables(0), dsPref.Tables(0))
                    ElseIf ErgyGHG = "CERGY" Then
                        GetBEPerWeightC(dsData.Tables(0), dsPref.Tables(0))
                    ElseIf ErgyGHG = "GHG" Then
                        GetBGPerWeight(dsData.Tables(0), dsPref.Tables(0))
                    ElseIf ErgyGHG = "CGHG" Then
                        GetBGPerWeightC(dsData.Tables(0), dsPref.Tables(0))
                    ElseIf ErgyGHG = "WATER" Then
                        GetBWPerWeight(dsData.Tables(0), dsPref.Tables(0))
                    ElseIf ErgyGHG = "CWATER" Then
                        GetBWPerWeightC(dsData.Tables(0), dsPref.Tables(0))
                    End If

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
                    Page.Title = "S1-Combine Energy Bar Chart"
                    rwType.Visible = True
                Else
                    Page.Title = "S1-Combine Energy Stack Chart"
                    rwType.Visible = False
                End If
            ElseIf ErgyGHG = "CERGY" Then
                If ChartType = "RBC" Then
                    Page.Title = "S1-Customer Combine Energy Bar Chart"
                    rwType.Visible = True
                Else
                    Page.Title = "S1-Customer Combine Energy Stack Chart"
                    rwType.Visible = False
                End If
            ElseIf ErgyGHG = "GHG" Then
                    If ChartType = "RBC" Then
                        Page.Title = "S1-Combine GHG Bar Chart"
                        rwType.Visible = True
                    Else
                        Page.Title = "S1-Combine GHG Stack Chart"
                        rwType.Visible = False
                End If
            ElseIf ErgyGHG = "CGHG" Then
                If ChartType = "RBC" Then
                    Page.Title = "S1-Customer Combine GHG Bar Chart"
                    rwType.Visible = True
                Else
                    Page.Title = "S1-Customer Combine GHG Stack Chart"
                    rwType.Visible = False
                End If
            ElseIf ErgyGHG = "WATER" Then
                If ChartType = "RBC" Then
                    Page.Title = "S1-Combine Water Bar Chart"
                    rwType.Visible = True
                Else
                    Page.Title = "S1-Combine Water Stack Chart"
                    rwType.Visible = False
                End If
            ElseIf ErgyGHG = "CWATER" Then
                If ChartType = "RBC" Then
                    Page.Title = "S1-Customer Combine Water Bar Chart"
                    rwType.Visible = True
                Else
                    Page.Title = "S1-Customer Combine Water Stack Chart"
                    rwType.Visible = False
                End If
            ElseIf ErgyGHG = "CEOL" Then
                If ChartType = "RBC" Then
                    Page.Title = "S1-Customer Combine End Of Life Bar Chart"
                    rwType.Visible = True
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
            If ErgyGHG = "ERGY" Or ErgyGHG = "CERGY" Then
                ds = objGetData.GetChartErgy()
            ElseIf ErgyGHG = "GHG" Or ErgyGHG = "CGHG" Then
                ds = objGetData.GetChartGhg()
            ElseIf ErgyGHG = "WATER" Or ErgyGHG = "CWATER" Then
                ds = objGetData.GetChartWater()
            ElseIf ErgyGHG = "CEOL" Then
                ds = objGetData.GetChartEOL()
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
        Dim objGetData As New S1GetData.Selectdata()
        Try

            If ErgyGHG = "ERGY" Or ErgyGHG = "CERGY" Then
                ds = objGetData.GetChartErgyRes(CaseId1, CaseId2)
            ElseIf ErgyGHG = "GHG" Or ErgyGHG = "CGHG" Then
                ds = objGetData.GetChartGhgRes(CaseId1, CaseId2)
            ElseIf ErgyGHG = "CEOL" Then
                ds = objGetData.GetChartEOLRes(CaseId1, CaseId2)
            Else
                ds = objGetData.GetChartWaterRes(CaseId1, CaseId2)
            End If
            Return ds
        Catch ex As Exception
            ErrorLable.Text = "Error:GetData:" + ex.Message.ToString()
            Return ds
        End Try
    End Function

    Protected Function GetPref(ByVal UserName As String) As DataSet
        Dim ds As New DataSet
        Dim objGetData As New S1GetData.Selectdata()
        Try
            ds = objGetData.GetChartPref(Session("USERID").ToString())
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




            If ChartType = "RBC" Then
                lblHeading.Text = ddltype.SelectedItem.Text + " Energy Chart"
                pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
            Else
                lblHeading.Text = "Energy Per Unit Chart"
                pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(Transportation;Process;Purchased Materials)"
            End If

            ''FOR UNITS DISPLAY
            i = 0
            If Cnt > 0 Then i = 1
            Pref = "MJ"
            If (DsData.Rows(0).Item("FINVOLMSI") > 0) And (DsData.Rows(i).Item("FINVOLMSI") > 0) Then
                Pref = Pref + "/" + DsPref.Rows(0).Item("TITLE3").ToString()
            ElseIf (DsData.Rows(0).Item("FINVOLMSI") > 0) And (DsData.Rows(i).Item("FINVOLMUNITS") > 0) Then
                Pref = Pref + "/(" + DsPref.Rows(0).Item("TITLE3").ToString() + " or unit)"
            ElseIf (DsData.Rows(0).Item("FINVOLMUNITS") > 0) And (DsData.Rows(i).Item("FINVOLMSI") > 0) Then
                Pref = Pref + "/(unit or " + DsPref.Rows(0).Item("TITLE3").ToString() + ")"
            ElseIf (DsData.Rows(0).Item("FINVOLMUNITS") > 0) And (DsData.Rows(i).Item("FINVOLMUNITS") > 0) Then
                Pref = Pref + "/unit "
            ElseIf (DsData.Rows(0).Item("FINVOLMSI") > 0) Then
                Pref = Pref + "/" + DsPref.Rows(0).Item("TITLE3").ToString()
            ElseIf (DsData.Rows(0).Item("FINVOLMUNITS") > 0) Then
                Pref = Pref + "/unit "
            ElseIf (DsData.Rows(i).Item("FINVOLMSI") > 0) Then
                Pref = Pref + "/" + DsPref.Rows(0).Item("TITLE3").ToString()
            ElseIf (DsData.Rows(i).Item("FINVOLMUNITS") > 0) Then
                Pref = Pref + "/unit "
            Else
                Pref = Pref + "/unit "
            End If



            For i = 0 To Cnt
                If DsData.Rows(i).Item("FINVOLMSI") > 0 Then                   
                    SalesVolumeUnit = FormatNumber(DsData.Rows(i).Item("FINVOLMSI") * DsPref.Rows(0).Item("ConvArea"), 0)
                ElseIf DsData.Rows(i).Item("FINVOLMUNITS") > 0 Then
                    SalesVolumeUnit = DsData.Rows(i).Item("FINVOLMUNITS")
                End If

                If ChartType = "RBC" Then
                    If SalesVolumeUnit > 0 Then
                        Data = FormatNumber(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") / SalesVolumeUnit, 3)
                    End If
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") + "(" + HoverText & ");" & Data & ")"
                    pcScript = pcScript + Graphtype + ".addHoverText(1," + (i + 4).ToString() + ",Value:" + Data.ToString() + "<br/>Unit:" + HoverText.ToString() + ")"
                Else
                    If SalesVolumeUnit > 0 Then
                        Transportation = FormatNumber(DsData.Rows(i).Item("TRNSPTERGY") / SalesVolumeUnit, 3)
                        Process = FormatNumber(DsData.Rows(i).Item("PROCERGY") / SalesVolumeUnit, 3)
                        Purchased = FormatNumber(DsData.Rows(i).Item("PURMATERIALERGY") / SalesVolumeUnit, 3)
                    End If
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") + "<br/>(" + HoverText.ToString() + ");" & Transportation & ";" & Process & ";" & Purchased & ")"
                    pcScript = pcScript + "" + Graphtype + ".addHoverText(1," + (i + 4).ToString() + ",Value:" + Transportation.ToString() + "<br/>Unit:" + HoverText.ToString() + ")"
                    pcScript = pcScript + "" + Graphtype + ".addHoverText(2," + (i + 4).ToString() + ",Value:" + Process.ToString() + "<br/>Unit:" + HoverText.ToString() + ")"
                    pcScript = pcScript + "" + Graphtype + ".addHoverText(3," + (i + 4).ToString() + ",Value:" + Purchased.ToString() + "<br/>Unit:" + HoverText.ToString() + ")"
                    End If
            Next

            GenrateChart(pcScript, GraphName, Pref)

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GetBEPerUnitC(ByVal DsData As DataTable, ByVal DsPref As DataTable)
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
            Dim customerSaleValue As Double
            Dim percentage As Double
            Dim perUnit As Double
            Dim DsDataView As DataView
            Dim DtData As DataTable
            Dim customerSaleUnit As Double
            Dim lbToUnit As Double
            Dim SalesVolumeLb As Double
            Dim percentage1 As Double
            Dim percentage2 As Double
            Dim percentage3 As Double
            GraphName = "Sargento_KraftPouchvsTray"
            Graphtype = "graph"
            Cnt = DsData.Rows.Count - 1

            DsDataView = DsData.DefaultView
            DsDataView.RowFilter = "CASEID = " + Session("S1CaseId")
            DtData = DsDataView.ToTable()

            customerSaleValue = CDbl(DtData.Rows(0).Item("CUSSALESVOLUME").ToString())
            customerSaleUnit = FormatNumber(((CDbl(DtData.Rows(0).Item("CUSSALESUNIT").ToString()))), 0).ToString()



            If ChartType = "RBC" Then
                lblHeading.Text = "Customer " + ddltype.SelectedItem.Text + " Energy Chart"
                pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
            Else
                lblHeading.Text = "Customer Energy Per Unit Chart"
                pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(Transportation;Process;Purchased Materials)"
            End If
            ''FOR UNITS DISPLAY
            i = 0
            If Cnt > 0 Then i = 1
            Pref = "MJ"
            If (DsData.Rows(0).Item("FINVOLMSI") > 0) And (DsData.Rows(i).Item("FINVOLMSI") > 0) Then
                Pref = Pref + "/" + DsPref.Rows(0).Item("TITLE3").ToString()
            ElseIf (DsData.Rows(0).Item("FINVOLMSI") > 0) And (DsData.Rows(i).Item("FINVOLMUNITS") > 0) Then
                Pref = Pref + "/(" + DsPref.Rows(0).Item("TITLE3").ToString() + " or unit)"
            ElseIf (DsData.Rows(0).Item("FINVOLMUNITS") > 0) And (DsData.Rows(i).Item("FINVOLMSI") > 0) Then
                Pref = Pref + "/(unit or " + DsPref.Rows(0).Item("TITLE3").ToString() + ")"
            ElseIf (DsData.Rows(0).Item("FINVOLMUNITS") > 0) And (DsData.Rows(i).Item("FINVOLMUNITS") > 0) Then
                Pref = Pref + "/unit "
            ElseIf (DsData.Rows(0).Item("FINVOLMSI") > 0) Then
                Pref = Pref + "/" + DsPref.Rows(0).Item("TITLE3").ToString()
            ElseIf (DsData.Rows(0).Item("FINVOLMUNITS") > 0) Then
                Pref = Pref + "/unit "
            ElseIf (DsData.Rows(i).Item("FINVOLMSI") > 0) Then
                Pref = Pref + "/" + DsPref.Rows(0).Item("TITLE3").ToString()
            ElseIf (DsData.Rows(i).Item("FINVOLMUNITS") > 0) Then
                Pref = Pref + "/unit "
            Else
                Pref = Pref + "/unit "
            End If

            For i = 0 To Cnt
                SalesVolumeLb = DsData.Rows(i).Item("VOLUME") * DsPref.Rows(0).Item("CONVWT")
                If DsData.Rows(i).Item("FINVOLMSI") > 0 Then
                    SalesVolumeUnit = DsData.Rows(i).Item("FINVOLMSI") * DsPref.Rows(0).Item("ConvArea")
                ElseIf DsData.Rows(i).Item("FINVOLMUNITS") > 0 Then
                    SalesVolumeUnit = DsData.Rows(i).Item("FINVOLMUNITS")
                End If
                If CDbl(SalesVolumeLb) <> 0 Then
                    lbToUnit = CDbl(SalesVolumeUnit) / CDbl(SalesVolumeLb)
                Else
                    lbToUnit = 0
                End If
                If ChartType = "RBC" Then
                    If CDbl(customerSaleUnit) = 0 Then
                        If CDbl(DsData.Rows(i).Item("VOLUME").ToString()) > 0 Then
                            percentage = CDbl(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "")) * customerSaleValue / CDbl(DsData.Rows(i).Item("VOLUME").ToString())
                        End If
                    Else
                        If CDbl(DsData.Rows(i).Item("SUNITVAL").ToString()) > 0 Then
                            percentage = CDbl(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "")) * customerSaleValue / CDbl(DsData.Rows(i).Item("SUNITVAL").ToString())
                        End If
                    End If
                    If CDbl(customerSaleUnit) = 0 Then
                        If CDbl(customerSaleValue) <> 0 And CDbl(lbToUnit) > 0 Then
                            perUnit = (CDbl(percentage) / CDbl(customerSaleValue * lbToUnit * DsPref.Rows(0).Item("CONVWT")))
                        Else
                            perUnit = 0
                        End If
                    Else
                        If CDbl(customerSaleValue) <> 0 Then
                            If CDbl(DsData.Rows(i).Item("FINVOLMSI").ToString()) > 0 Then
                                perUnit = (CDbl(percentage) / CDbl(customerSaleValue * DsPref.Rows(0).Item("ConvArea")))
                            ElseIf CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString()) > 0 Then
                                perUnit = (CDbl(percentage) / CDbl(customerSaleValue))
                            End If

                        Else
                            perUnit = 0
                        End If
                    End If
                    Data = FormatNumber(perUnit, 3)
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") + ";" & Data & ")"
                    pcScript = pcScript + Graphtype + ".addHoverText(1," + (i + 4).ToString() + ",Value:" + Data.ToString() + ")"
                Else
                    If CDbl(customerSaleUnit) = 0 Then
                        If CDbl(DsData.Rows(i).Item("VOLUME").ToString()) > 0 Then
                            percentage1 = CDbl(DsData.Rows(i).Item("TRNSPTERGY")) * customerSaleValue / CDbl(DsData.Rows(i).Item("VOLUME").ToString())
                            percentage2 = CDbl(DsData.Rows(i).Item("PROCERGY")) * customerSaleValue / CDbl(DsData.Rows(i).Item("VOLUME").ToString())
                            percentage3 = CDbl(DsData.Rows(i).Item("PURMATERIALERGY")) * customerSaleValue / CDbl(DsData.Rows(i).Item("VOLUME").ToString())
                        End If
                    Else
                        If CDbl(DsData.Rows(i).Item("SUNITVAL").ToString()) > 0 Then
                            percentage1 = CDbl(DsData.Rows(i).Item("TRNSPTERGY")) * customerSaleValue / CDbl(DsData.Rows(i).Item("SUNITVAL").ToString())
                            percentage2 = CDbl(DsData.Rows(i).Item("PROCERGY")) * customerSaleValue / CDbl(DsData.Rows(i).Item("SUNITVAL").ToString())
                            percentage3 = CDbl(DsData.Rows(i).Item("PURMATERIALERGY")) * customerSaleValue / CDbl(DsData.Rows(i).Item("SUNITVAL").ToString())
                        End If
                    End If
                    If CDbl(customerSaleUnit) = 0 Then
                        If CDbl(customerSaleValue) <> 0 And CDbl(lbToUnit) > 0 Then
                            Transportation = (CDbl(percentage1) / CDbl(customerSaleValue * lbToUnit * DsPref.Rows(0).Item("CONVWT")))
                            Process = (CDbl(percentage2) / CDbl(customerSaleValue * lbToUnit * DsPref.Rows(0).Item("CONVWT")))
                            Purchased = (CDbl(percentage3) / CDbl(customerSaleValue * lbToUnit * DsPref.Rows(0).Item("CONVWT")))
                        Else
                            Transportation = 0
                            Process = 0
                            Purchased = 0
                        End If
                    Else
                        If CDbl(customerSaleValue) <> 0 Then
                            If CDbl(DsData.Rows(i).Item("FINVOLMSI").ToString()) > 0 Then
                                Transportation = (CDbl(percentage1) / CDbl(customerSaleValue * DsPref.Rows(0).Item("ConvArea")))
                                Process = (CDbl(percentage2) / CDbl(customerSaleValue * DsPref.Rows(0).Item("ConvArea")))
                                Purchased = (CDbl(percentage3) / CDbl(customerSaleValue * DsPref.Rows(0).Item("ConvArea")))
                            ElseIf CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString()) > 0 Then
                                Transportation = (CDbl(percentage1) / CDbl(customerSaleValue))
                                Process = (CDbl(percentage2) / CDbl(customerSaleValue))
                                Purchased = (CDbl(percentage3) / CDbl(customerSaleValue))
                            End If
                        Else
                            Transportation = 0
                            Process = 0
                            Purchased = 0
                        End If
                    End If
                    Transportation = FormatNumber(Transportation, 3)
                    Process = FormatNumber(Process, 3)
                    Purchased = FormatNumber(Purchased, 3)

                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") + ";" & Transportation & ";" & Process & ";" & Purchased & ")"
                    pcScript = pcScript + "" + Graphtype + ".addHoverText(1," + (i + 4).ToString() + ",Value:" + Transportation.ToString() + ")"
                    pcScript = pcScript + "" + Graphtype + ".addHoverText(2," + (i + 4).ToString() + ",Value:" + Process.ToString() + ")"
                    pcScript = pcScript + "" + Graphtype + ".addHoverText(3," + (i + 4).ToString() + ",Value:" + Purchased.ToString() + ")"
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
            Dim Transportation As New Decimal
            Dim Process As New Decimal
            Dim Purchased As New Decimal
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
                SalesVolumeLb = FormatNumber(DsData.Rows(i).Item("VOLUME") * DsPref.Rows(0).Item("CONVWT"), 0)
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
                        Process = FormatNumber(DsData.Rows(i).Item("PROCERGY") / SalesVolumeLb, 3)
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

    Protected Sub GetBEPerWeightC(ByVal DsData As DataTable, ByVal DsPref As DataTable)
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
            Dim customerSaleValue As Double
            Dim percentage As Double
            Dim DsDataView As DataView
            Dim DtData As DataTable
            Dim customerSaleUnit As Double
            Dim UniToLb As Double
            Dim SalesVolumeUnit As Double
            Dim perWeight As Double
            Dim percentage1 As Double
            Dim percentage2 As Double
            Dim percentage3 As Double

            DsDataView = DsData.DefaultView
            DsDataView.RowFilter = "CASEID = " + Session("S1CaseId")
            DtData = DsDataView.ToTable()

            customerSaleValue = CDbl(DtData.Rows(0).Item("CUSSALESVOLUME").ToString())
            customerSaleUnit = FormatNumber(((CDbl(DtData.Rows(0).Item("CUSSALESUNIT").ToString()))), 0).ToString()

            GraphName = "Sargento_KraftPouchvsTray"
            Graphtype = "graph"
            Cnt = DsData.Rows.Count - 1
            Pref = "MJ/" + DsPref.Rows(0).Item("Title8").ToString()

            If ChartType = "RBC" Then
                lblHeading.Text = "Customer " + ddltype.SelectedItem.Text + " Energy Chart"
                pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
            Else
                lblHeading.Text = "Customer Energy Per Weight Chart"
                pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(Transportation;Process;Purchased Materials)"
            End If
            For i = 0 To Cnt
                If DsData.Rows(i).Item("FINVOLMSI") > 0 Then
                    SalesVolumeUnit = DsData.Rows(i).Item("FINVOLMSI") * DsPref.Rows(0).Item("ConvArea")
                ElseIf DsData.Rows(i).Item("FINVOLMUNITS") > 0 Then
                    SalesVolumeUnit = DsData.Rows(i).Item("FINVOLMUNITS")
                End If
                SalesVolumeLb = DsData.Rows(i).Item("VOLUME") * DsPref.Rows(0).Item("CONVWT")
                If CDbl(SalesVolumeUnit) <> 0 Then
                    UniToLb = CDbl(SalesVolumeLb) / CDbl(SalesVolumeUnit)
                Else
                    UniToLb = 0
                End If

                If ChartType = "RBC" Then
                    If CDbl(customerSaleUnit) = 0 Then
                        If CDbl(DsData.Rows(i).Item("VOLUME").ToString()) > 0 Then
                            percentage = CDbl(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "")) * customerSaleValue / CDbl(DsData.Rows(i).Item("VOLUME").ToString())
                        End If
                    Else
                        If CDbl(DsData.Rows(i).Item("SUNITVAL").ToString()) > 0 Then
                            percentage = CDbl(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "")) * customerSaleValue / CDbl(DsData.Rows(i).Item("SUNITVAL").ToString())
                        End If
                    End If
                    If CDbl(customerSaleUnit) <> 0 Then
                        If CDbl(customerSaleUnit) <> 0 And CDbl(UniToLb) > 0 Then
                            If CDbl(DsData.Rows(i).Item("FINVOLMSI").ToString()) > 0 Then
                                perWeight = (CDbl(percentage) / CDbl(customerSaleValue * UniToLb * DsPref.Rows(0).Item("CONVAREA")))
                            ElseIf CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString()) > 0 Then
                                perWeight = (CDbl(percentage) / CDbl(customerSaleValue * UniToLb))
                            End If
                        Else
                            perWeight = 0
                        End If
                    Else
                        If CDbl(customerSaleValue) <> 0 Then
                            perWeight = (CDbl(percentage) / CDbl(customerSaleValue * DsPref.Rows(0).Item("CONVWT")))
                        Else
                            perWeight = 0
                        End If
                    End If
                    Data = FormatNumber(perWeight, 3)
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & Data & ")"
                Else
                    If CDbl(customerSaleUnit) = 0 Then
                        If CDbl(DsData.Rows(i).Item("VOLUME").ToString()) > 0 Then
                            percentage1 = CDbl(DsData.Rows(i).Item("TRNSPTERGY")) * customerSaleValue / CDbl(DsData.Rows(i).Item("VOLUME").ToString())
                            percentage2 = CDbl(DsData.Rows(i).Item("PROCERGY")) * customerSaleValue / CDbl(DsData.Rows(i).Item("VOLUME").ToString())
                            percentage3 = CDbl(DsData.Rows(i).Item("PURMATERIALERGY")) * customerSaleValue / CDbl(DsData.Rows(i).Item("VOLUME").ToString())
                        End If
                    Else
                        If CDbl(DsData.Rows(i).Item("SUNITVAL").ToString()) > 0 Then
                            percentage1 = CDbl(DsData.Rows(i).Item("TRNSPTERGY")) * customerSaleValue / CDbl(DsData.Rows(i).Item("SUNITVAL").ToString())
                            percentage2 = CDbl(DsData.Rows(i).Item("PROCERGY")) * customerSaleValue / CDbl(DsData.Rows(i).Item("SUNITVAL").ToString())
                            percentage3 = CDbl(DsData.Rows(i).Item("PURMATERIALERGY")) * customerSaleValue / CDbl(DsData.Rows(i).Item("SUNITVAL").ToString())
                        End If
                    End If
                    If CDbl(customerSaleUnit) <> 0 Then
                        If CDbl(customerSaleUnit) <> 0 And CDbl(UniToLb) > 0 Then
                            If CDbl(DsData.Rows(i).Item("FINVOLMSI").ToString()) > 0 Then
                                Transportation = (CDbl(percentage1) / CDbl(customerSaleValue * UniToLb * DsPref.Rows(0).Item("CONVAREA")))
                                Process = (CDbl(percentage2) / CDbl(customerSaleValue * UniToLb * DsPref.Rows(0).Item("CONVAREA")))
                                Purchased = (CDbl(percentage3) / CDbl(customerSaleValue * UniToLb * DsPref.Rows(0).Item("CONVAREA")))
                            ElseIf CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString()) > 0 Then
                                Transportation = (CDbl(percentage1) / CDbl(customerSaleValue * UniToLb))
                                Process = (CDbl(percentage2) / CDbl(customerSaleValue * UniToLb))
                                Purchased = (CDbl(percentage3) / CDbl(customerSaleValue * UniToLb))
                            End If


                        Else
                            Transportation = 0
                            Process = 0
                            Purchased = 0
                        End If
                    Else
                        If CDbl(customerSaleValue) <> 0 Then
                            Transportation = (CDbl(percentage1) / CDbl(customerSaleValue * DsPref.Rows(0).Item("CONVWT")))
                            Process = (CDbl(percentage2) / CDbl(customerSaleValue * DsPref.Rows(0).Item("CONVWT")))
                            Purchased = (CDbl(percentage3) / CDbl(customerSaleValue * DsPref.Rows(0).Item("CONVWT")))
                        Else
                            Transportation = 0
                            Process = 0
                            Purchased = 0
                        End If
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
            Dim Transportation As New Decimal
            Dim Process As New Decimal
            Dim Purchased As New Decimal


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
                    Process = FormatNumber(DsData.Rows(i).Item("PROCERGY"), 0)
                    Purchased = FormatNumber(DsData.Rows(i).Item("PURMATERIALERGY"), 0)

                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & Transportation & ";" & Process & ";" & Purchased & ")"
                End If
            Next

            GenrateChart(pcScript, GraphName, Pref)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GetBETotalC(ByVal DsData As DataTable, ByVal DsPref As DataTable)
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
            Dim customerSaleValue As Double
            Dim percentage As Double
            Dim DsDataView As DataView
            Dim DtData As DataTable
            Dim customerSaleUnit As Double

            GraphName = "Sargento_KraftPouchvsTray"
            Graphtype = "graph"
            Cnt = DsData.Rows.Count - 1
            Pref = "MJ"

            DsDataView = DsData.DefaultView
            DsDataView.RowFilter = "CASEID = " + Session("S1CaseId")
            DtData = DsDataView.ToTable()

            customerSaleValue = CDbl(DtData.Rows(0).Item("CUSSALESVOLUME").ToString())
            customerSaleUnit = FormatNumber(((CDbl(DtData.Rows(0).Item("CUSSALESUNIT").ToString()))), 0).ToString()

            If ChartType = "RBC" Then
                lblHeading.Text = "Customer " + ddltype.SelectedItem.Text + " Energy Chart"
                pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
            Else
                lblHeading.Text = "Customer Total Energy Chart"
                pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(Transportation;Process;Purchased Materials)"
            End If
            For i = 0 To Cnt
                If ChartType = "RBC" Then
                    If CDbl(customerSaleUnit) = 0 Then
                        If CDbl(DsData.Rows(i).Item("VOLUME").ToString()) > 0 Then
                            percentage = CDbl(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "")) * customerSaleValue / CDbl(DsData.Rows(i).Item("VOLUME").ToString())
                        End If
                    Else
                        If CDbl(DsData.Rows(i).Item("SUNITVAL").ToString()) > 0 Then
                            percentage = CDbl(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "")) * customerSaleValue / CDbl(DsData.Rows(i).Item("SUNITVAL").ToString())
                        End If
                    End If
                    Data = FormatNumber(percentage, 0)
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & Data & ")"
                Else
                    If CDbl(customerSaleUnit) = 0 Then
                        If CDbl(DsData.Rows(i).Item("VOLUME").ToString()) > 0 Then
                            Transportation = CDbl(DsData.Rows(i).Item("TRNSPTERGY")) * customerSaleValue / CDbl(DsData.Rows(i).Item("VOLUME").ToString())
                            Process = CDbl(DsData.Rows(i).Item("PROCERGY")) * customerSaleValue / CDbl(DsData.Rows(i).Item("VOLUME").ToString())
                            Purchased = CDbl(DsData.Rows(i).Item("PURMATERIALERGY")) * customerSaleValue / CDbl(DsData.Rows(i).Item("VOLUME").ToString())
                        End If
                    Else
                        If CDbl(DsData.Rows(i).Item("SUNITVAL").ToString()) > 0 Then
                            Transportation = CDbl(DsData.Rows(i).Item("TRNSPTERGY")) * customerSaleValue / CDbl(DsData.Rows(i).Item("SUNITVAL").ToString())
                            Process = CDbl(DsData.Rows(i).Item("PROCERGY")) * customerSaleValue / CDbl(DsData.Rows(i).Item("SUNITVAL").ToString())
                            Purchased = CDbl(DsData.Rows(i).Item("PURMATERIALERGY")) * customerSaleValue / CDbl(DsData.Rows(i).Item("SUNITVAL").ToString())
                        End If
                    End If
                    Transportation = FormatNumber(Transportation, 0)
                    Process = FormatNumber(Process, 0)
                    Purchased = FormatNumber(Purchased, 0)

                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & Transportation & ";" & Process & ";" & Purchased & ")"
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

            ''FOR UNITS DISPLAY
            i = 0
            If Cnt > 0 Then i = 1
            Pref = DsPref.Rows(0).Item("TITLE8").ToString()
            If (DsData.Rows(0).Item("FINVOLMSI") > 0) And (DsData.Rows(i).Item("FINVOLMSI") > 0) Then
                Pref = Pref + " gr gas/" + DsPref.Rows(0).Item("TITLE3").ToString()
            ElseIf (DsData.Rows(0).Item("FINVOLMSI") > 0) And (DsData.Rows(i).Item("FINVOLMUNITS") > 0) Then
                Pref = Pref + " gr gas/(" + DsPref.Rows(0).Item("TITLE3").ToString() + " or unit)"
            ElseIf (DsData.Rows(0).Item("FINVOLMUNITS") > 0) And (DsData.Rows(i).Item("FINVOLMSI") > 0) Then
                Pref = Pref + " gr gas/(unit or " + DsPref.Rows(0).Item("TITLE3").ToString() + ")"
            ElseIf (DsData.Rows(0).Item("FINVOLMUNITS") > 0) And (DsData.Rows(i).Item("FINVOLMUNITS") > 0) Then
                Pref = Pref + " gr gas/unit "
            ElseIf (DsData.Rows(0).Item("FINVOLMSI") > 0) Then
                Pref = Pref + " gr gas/" + DsPref.Rows(0).Item("TITLE3").ToString()
            ElseIf (DsData.Rows(0).Item("FINVOLMUNITS") > 0) Then
                Pref = Pref + " gr gas/unit"
            ElseIf (DsData.Rows(i).Item("FINVOLMSI") > 0) Then
                Pref = Pref + " gr gas/" + DsPref.Rows(0).Item("TITLE3").ToString()
            ElseIf (DsData.Rows(i).Item("FINVOLMUNITS") > 0) Then
                Pref = Pref + " gr gas/unit "
            Else
                Pref = Pref + " gr gas/unit "
            End If

            For i = 0 To Cnt
                If DsData.Rows(i).Item("FINVOLMSI") > 0 Then
                    SalesVolumeUnit = FormatNumber(DsData.Rows(i).Item("FINVOLMSI") * DsPref.Rows(0).Item("ConvArea"), 0)
                ElseIf DsData.Rows(i).Item("FINVOLMUNITS") > 0 Then
                    SalesVolumeUnit = DsData.Rows(i).Item("FINVOLMUNITS")
                End If

                If ChartType = "RBC" Then
                    If SalesVolumeUnit > 0 Then
                        Data = FormatNumber(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * DsPref.Rows(0).Item("CONVWT") / SalesVolumeUnit, 3)
                    Else
                        Data = 0
                    End If
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") + "(" + HoverText & ");" & Data & ")"
                    pcScript = pcScript + Graphtype + ".addHoverText(1," + (i + 4).ToString() + ",Value:" + Data.ToString() + "<br/>Unit:" + HoverText.ToString() + ")"
                Else
                    If SalesVolumeUnit > 0 Then
                        Transportation = FormatNumber(DsData.Rows(i).Item("TRNSPTGRNHUSGAS") * DsPref.Rows(0).Item("CONVWT") / SalesVolumeUnit, 3)
                        Process = FormatNumber(DsData.Rows(i).Item("PROCGRNHUSGAS") * DsPref.Rows(0).Item("CONVWT") / SalesVolumeUnit, 3)
                        Purchased = FormatNumber(DsData.Rows(i).Item("PURMATERIALGRNHUSGAS") * DsPref.Rows(0).Item("CONVWT") / SalesVolumeUnit, 3)
                    Else
                        Transportation = 0
                        Process = 0
                        Purchased = 0
                    End If
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") + "<br/>(" + HoverText.ToString() + ");" & Transportation & ";" & Process & ";" & Purchased & ")"
                    pcScript = pcScript + "" + Graphtype + ".addHoverText(1," + (i + 4).ToString() + ",Value:" + Transportation.ToString() + "<br/>Unit:" + HoverText.ToString() + ")"
                    pcScript = pcScript + "" + Graphtype + ".addHoverText(2," + (i + 4).ToString() + ",Value:" + Process.ToString() + "<br/>Unit:" + HoverText.ToString() + ")"
                    pcScript = pcScript + "" + Graphtype + ".addHoverText(3," + (i + 4).ToString() + ",Value:" + Purchased.ToString() + "<br/>Unit:" + HoverText.ToString() + ")"
                    End If


            Next

            GenrateChart(pcScript, GraphName, Pref)

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GetBGPerUnitC(ByVal DsData As DataTable, ByVal DsPref As DataTable)
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
            Dim customerSaleValue As Double
            Dim percentage As Double
            Dim perUnit As Double
            Dim DsDataView As DataView
            Dim DtData As DataTable
            Dim customerSaleUnit As Double
            Dim lbToUnit As Double
            Dim SalesVolumeLb As Double
            Dim percentage1 As Double
            Dim percentage2 As Double
            Dim percentage3 As Double
            GraphName = "Sargento_KraftPouchvsTray"
            Graphtype = "graph"
            Cnt = DsData.Rows.Count - 1

            DsDataView = DsData.DefaultView
            DsDataView.RowFilter = "CASEID = " + Session("S1CaseId")
            DtData = DsDataView.ToTable()

            customerSaleValue = CDbl(DtData.Rows(0).Item("CUSSALESVOLUME").ToString())
            customerSaleUnit = FormatNumber(((CDbl(DtData.Rows(0).Item("CUSSALESUNIT").ToString()))), 0).ToString()



            If ChartType = "RBC" Then
                lblHeading.Text = "Customer " + ddltype.SelectedItem.Text + " GHG Chart"
                pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
            Else
                lblHeading.Text = "Customer GHG Per Unit Chart "
                pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(Transportation;Process;Purchased Materials)"
            End If


            ''FOR UNITS DISPLAY
            i = 0
            If Cnt > 0 Then i = 1
            Pref = DsPref.Rows(0).Item("TITLE8").ToString()
            If (DsData.Rows(0).Item("FINVOLMSI") > 0) And (DsData.Rows(i).Item("FINVOLMSI") > 0) Then
                Pref = Pref + " gr gas/" + DsPref.Rows(0).Item("TITLE3").ToString()
            ElseIf (DsData.Rows(0).Item("FINVOLMSI") > 0) And (DsData.Rows(i).Item("FINVOLMUNITS") > 0) Then
                Pref = Pref + " gr gas/(" + DsPref.Rows(0).Item("TITLE3").ToString() + " or unit)"
            ElseIf (DsData.Rows(0).Item("FINVOLMUNITS") > 0) And (DsData.Rows(i).Item("FINVOLMSI") > 0) Then
                Pref = Pref + " gr gas/(unit or " + DsPref.Rows(0).Item("TITLE3").ToString() + ")"
            ElseIf (DsData.Rows(0).Item("FINVOLMUNITS") > 0) And (DsData.Rows(i).Item("FINVOLMUNITS") > 0) Then
                Pref = Pref + " gr gas/unit "
            ElseIf (DsData.Rows(0).Item("FINVOLMSI") > 0) Then
                Pref = Pref + " gr gas/" + DsPref.Rows(0).Item("TITLE3").ToString()
            ElseIf (DsData.Rows(0).Item("FINVOLMUNITS") > 0) Then
                Pref = Pref + " gr gas/unit"
            ElseIf (DsData.Rows(i).Item("FINVOLMSI") > 0) Then
                Pref = Pref + " gr gas/" + DsPref.Rows(0).Item("TITLE3").ToString()
            ElseIf (DsData.Rows(i).Item("FINVOLMUNITS") > 0) Then
                Pref = Pref + " gr gas/unit "
            Else
                Pref = Pref + " gr gas/unit "
            End If


            For i = 0 To Cnt
                SalesVolumeLb = DsData.Rows(i).Item("VOLUME") * DsPref.Rows(0).Item("CONVWT")
                If DsData.Rows(i).Item("FINVOLMSI") > 0 Then
                    SalesVolumeUnit = DsData.Rows(i).Item("FINVOLMSI") * DsPref.Rows(0).Item("ConvArea")
                ElseIf DsData.Rows(i).Item("FINVOLMUNITS") > 0 Then
                    SalesVolumeUnit = DsData.Rows(i).Item("FINVOLMUNITS")
                End If
                If CDbl(SalesVolumeLb) <> 0 Then
                    lbToUnit = CDbl(SalesVolumeUnit) / CDbl(SalesVolumeLb)
                Else
                    lbToUnit = 0
                End If

                If ChartType = "RBC" Then
                    If CDbl(customerSaleUnit) = 0 Then
                        If CDbl(DsData.Rows(i).Item("VOLUME").ToString()) > 0 Then
                            percentage = CDbl(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * CDbl(DsPref.Rows(0).Item("CONVWT").ToString())) * customerSaleValue / CDbl(DsData.Rows(i).Item("VOLUME").ToString())
                        End If
                    Else
                        If CDbl(DsData.Rows(i).Item("SUNITVAL").ToString()) > 0 Then
                            percentage = CDbl(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * CDbl(DsPref.Rows(0).Item("CONVWT").ToString())) * customerSaleValue / CDbl(DsData.Rows(i).Item("SUNITVAL").ToString())
                        End If
                    End If
                    If CDbl(customerSaleUnit) = 0 Then
                        If CDbl(customerSaleValue) <> 0 And CDbl(lbToUnit) > 0 Then
                            perUnit = (CDbl(percentage) / CDbl(customerSaleValue * lbToUnit * DsPref.Rows(0).Item("CONVWT")))
                        Else
                            perUnit = 0
                        End If
                    Else
                        If CDbl(customerSaleValue) <> 0 Then
                            If CDbl(DsData.Rows(i).Item("FINVOLMSI").ToString()) > 0 Then
                                perUnit = (CDbl(percentage) / CDbl(customerSaleValue * DsPref.Rows(0).Item("ConvArea")))
                            ElseIf CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString()) > 0 Then
                                perUnit = (CDbl(percentage) / CDbl(customerSaleValue))
                            End If
                        Else
                            perUnit = 0
                        End If
                    End If
                    Data = FormatNumber(perUnit, 3)
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") + ";" & Data & ")"
                    pcScript = pcScript + Graphtype + ".addHoverText(1," + (i + 4).ToString() + ",Value:" + Data.ToString() + ")"
                Else
                    If CDbl(customerSaleUnit) = 0 Then
                        If CDbl(DsData.Rows(i).Item("VOLUME").ToString()) > 0 Then
                            percentage1 = CDbl(DsData.Rows(i).Item("TRNSPTGRNHUSGAS") * CDbl(DsPref.Rows(0).Item("CONVWT").ToString()) * customerSaleValue / CDbl(DsData.Rows(i).Item("VOLUME").ToString()))
                            percentage2 = CDbl(DsData.Rows(i).Item("PROCGRNHUSGAS") * CDbl(DsPref.Rows(0).Item("CONVWT").ToString()) * customerSaleValue / CDbl(DsData.Rows(i).Item("VOLUME").ToString()))
                            percentage3 = CDbl(DsData.Rows(i).Item("PURMATERIALGRNHUSGAS") * CDbl(DsPref.Rows(0).Item("CONVWT").ToString()) * customerSaleValue / CDbl(DsData.Rows(i).Item("VOLUME").ToString()))
                        End If
                    Else
                        If CDbl(DsData.Rows(i).Item("SUNITVAL").ToString()) > 0 Then
                            percentage1 = CDbl(DsData.Rows(i).Item("TRNSPTGRNHUSGAS") * CDbl(DsPref.Rows(0).Item("CONVWT").ToString()) * customerSaleValue / CDbl(DsData.Rows(i).Item("SUNITVAL").ToString()))
                            percentage2 = CDbl(DsData.Rows(i).Item("PROCGRNHUSGAS") * CDbl(DsPref.Rows(0).Item("CONVWT").ToString()) * customerSaleValue / CDbl(DsData.Rows(i).Item("SUNITVAL").ToString()))
                            percentage3 = CDbl(DsData.Rows(i).Item("PURMATERIALGRNHUSGAS") * CDbl(DsPref.Rows(0).Item("CONVWT").ToString()) * customerSaleValue / CDbl(DsData.Rows(i).Item("SUNITVAL").ToString()))
                        End If
                    End If
                    If CDbl(customerSaleUnit) = 0 Then
                        If CDbl(customerSaleValue) <> 0 And CDbl(lbToUnit) > 0 Then
                            Transportation = (CDbl(percentage1) / CDbl(customerSaleValue * lbToUnit * DsPref.Rows(0).Item("CONVWT")))
                            Process = (CDbl(percentage2) / CDbl(customerSaleValue * lbToUnit * DsPref.Rows(0).Item("CONVWT")))
                            Purchased = (CDbl(percentage3) / CDbl(customerSaleValue * lbToUnit * DsPref.Rows(0).Item("CONVWT")))
                        Else
                            Transportation = 0
                            Process = 0
                            Purchased = 0
                        End If
                    Else
                        If CDbl(customerSaleValue) <> 0 Then
                            If CDbl(DsData.Rows(i).Item("FINVOLMSI").ToString()) > 0 Then
                                Transportation = (CDbl(percentage1) / CDbl(customerSaleValue * DsPref.Rows(0).Item("ConvArea")))
                                Process = (CDbl(percentage2) / CDbl(customerSaleValue * DsPref.Rows(0).Item("ConvArea")))
                                Purchased = (CDbl(percentage3) / CDbl(customerSaleValue * DsPref.Rows(0).Item("ConvArea")))
                            ElseIf CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString()) > 0 Then
                                Transportation = (CDbl(percentage1) / CDbl(customerSaleValue))
                                Process = (CDbl(percentage2) / CDbl(customerSaleValue))
                                Purchased = (CDbl(percentage3) / CDbl(customerSaleValue))
                            End If
                        Else
                            Transportation = 0
                            Process = 0
                            Purchased = 0
                        End If
                    End If
                    Transportation = FormatNumber(Transportation, 3)
                    Process = FormatNumber(Process, 3)
                    Purchased = FormatNumber(Purchased, 3)

                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") + ";" & Transportation & ";" & Process & ";" & Purchased & ")"
                    pcScript = pcScript + "" + Graphtype + ".addHoverText(1," + (i + 4).ToString() + ",Value:" + Transportation.ToString() + ")"
                    pcScript = pcScript + "" + Graphtype + ".addHoverText(2," + (i + 4).ToString() + ",Value:" + Process.ToString() + ")"
                    pcScript = pcScript + "" + Graphtype + ".addHoverText(3," + (i + 4).ToString() + ",Value:" + Purchased.ToString() + ")"
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

            ''FOR UNITS DISPLAY
            i = 0
            Pref = " "
            If Cnt > 0 Then i = 1
            Pref = DsPref.Rows(0).Item("TITLE10").ToString()
            If (DsData.Rows(0).Item("FINVOLMSI") > 0) And (DsData.Rows(i).Item("FINVOLMSI") > 0) Then
                Pref = Pref + " water/" + DsPref.Rows(0).Item("TITLE3").ToString()
            ElseIf (DsData.Rows(0).Item("FINVOLMSI") > 0) And (DsData.Rows(i).Item("FINVOLMUNITS") > 0) Then
                Pref = Pref + " water/(" + DsPref.Rows(0).Item("TITLE3").ToString() + " or unit)"
            ElseIf (DsData.Rows(0).Item("FINVOLMUNITS") > 0) And (DsData.Rows(i).Item("FINVOLMSI") > 0) Then
                Pref = Pref + " water/(unit or " + DsPref.Rows(0).Item("TITLE3").ToString() + ")"
            ElseIf (DsData.Rows(0).Item("FINVOLMUNITS") > 0) And (DsData.Rows(i).Item("FINVOLMUNITS") > 0) Then
                Pref = Pref + " water/unit "
            ElseIf (DsData.Rows(0).Item("FINVOLMSI") > 0) Then
                Pref = Pref + " water/" + DsPref.Rows(0).Item("TITLE3").ToString()
            ElseIf (DsData.Rows(0).Item("FINVOLMUNITS") > 0) Then
                Pref = Pref + " water/unit"
            ElseIf (DsData.Rows(0).Item("FINVOLMSI") > 0) Then
                Pref = Pref + " water/" + DsPref.Rows(0).Item("TITLE3").ToString()
            ElseIf (DsData.Rows(0).Item("FINVOLMUNITS") > 0) Then
                Pref = Pref + " water/unit"
            ElseIf (DsData.Rows(i).Item("FINVOLMSI") > 0) Then
                Pref = Pref + " water/" + DsPref.Rows(0).Item("TITLE3").ToString()
            ElseIf (DsData.Rows(i).Item("FINVOLMUNITS") > 0) Then
                Pref = Pref + " water/unit"
            Else
                Pref = Pref + " water/unit"
            End If

            For i = 0 To Cnt
                If DsData.Rows(i).Item("FINVOLMSI") > 0 Then
                    SalesVolumeUnit = FormatNumber(DsData.Rows(i).Item("FINVOLMSI") * DsPref.Rows(0).Item("ConvArea"), 0)
                ElseIf DsData.Rows(i).Item("FINVOLMUNITS") > 0 Then
                    SalesVolumeUnit = DsData.Rows(i).Item("FINVOLMUNITS")
                End If

                If ChartType = "RBC" Then
                    If SalesVolumeUnit > 0 Then
                        Data = FormatNumber(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * DsPref.Rows(0).Item("CONVGALLON") / SalesVolumeUnit, 3)
                    Else
                        Data = 0
                    End If
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") + "(" + HoverText & ");" & Data & ")"
                    pcScript = pcScript + Graphtype + ".addHoverText(1," + (i + 4).ToString() + ",Value:" + Data.ToString() + "<br/>Unit:" + HoverText.ToString() + ")"
                Else
                    If SalesVolumeUnit > 0 Then
                        Transportation = FormatNumber(DsData.Rows(i).Item("TRNSPTWATER") * DsPref.Rows(0).Item("CONVGALLON") / SalesVolumeUnit, 3)
                        Process = FormatNumber(DsData.Rows(i).Item("PROCWATER") * DsPref.Rows(0).Item("CONVGALLON") / SalesVolumeUnit, 3)
                        Purchased = FormatNumber(DsData.Rows(i).Item("PURMATERIALWATER") * DsPref.Rows(0).Item("CONVGALLON") / SalesVolumeUnit, 3)
                    Else
                        Transportation = 0
                        Process = 0
                        Purchased = 0
                    End If
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") + "<br/>(" + HoverText.ToString() + ");" & Transportation & ";" & Process & ";" & Purchased & ")"
                    pcScript = pcScript + "" + Graphtype + ".addHoverText(1," + (i + 4).ToString() + ",Value:" + Transportation.ToString() + "<br/>Unit:" + HoverText.ToString() + ")"
                    pcScript = pcScript + "" + Graphtype + ".addHoverText(2," + (i + 4).ToString() + ",Value:" + Process.ToString() + "<br/>Unit:" + HoverText.ToString() + ")"
                    pcScript = pcScript + "" + Graphtype + ".addHoverText(3," + (i + 4).ToString() + ",Value:" + Purchased.ToString() + "<br/>Unit:" + HoverText.ToString() + ")"
                    End If


            Next

            GenrateChart(pcScript, GraphName, Pref)

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GetBWPerUnitC(ByVal DsData As DataTable, ByVal DsPref As DataTable)
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
            Dim customerSaleValue As Double
            Dim percentage As Double
            Dim perUnit As Double
            Dim DsDataView As DataView
            Dim DtData As DataTable
            Dim customerSaleUnit As Double
            Dim lbToUnit As Double
            Dim SalesVolumeLb As Double
            Dim percentage1 As Double
            Dim percentage2 As Double
            Dim percentage3 As Double
            GraphName = "Sargento_KraftPouchvsTray"
            Graphtype = "graph"
            Cnt = DsData.Rows.Count - 1

            DsDataView = DsData.DefaultView
            DsDataView.RowFilter = "CASEID = " + Session("S1CaseId")
            DtData = DsDataView.ToTable()

            customerSaleValue = CDbl(DtData.Rows(0).Item("CUSSALESVOLUME").ToString())
            customerSaleUnit = FormatNumber(((CDbl(DtData.Rows(0).Item("CUSSALESUNIT").ToString()))), 0).ToString()



            If ChartType = "RBC" Then
                lblHeading.Text = "Customer " + ddltype.SelectedItem.Text + " Water Chart"
                pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
            Else
                lblHeading.Text = "Customer Water Per Unit Chart "
                pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(Transportation;Process;Purchased Materials)"
            End If


            ''FOR UNITS DISPLAY
            i = 0
            If Cnt > 0 Then i = 1
            Pref = DsPref.Rows(0).Item("TITLE10").ToString()
            If (DsData.Rows(0).Item("FINVOLMSI") > 0) And (DsData.Rows(i).Item("FINVOLMSI") > 0) Then
                Pref = Pref + " water/" + DsPref.Rows(0).Item("TITLE3").ToString()
            ElseIf (DsData.Rows(0).Item("FINVOLMSI") > 0) And (DsData.Rows(i).Item("FINVOLMUNITS") > 0) Then
                Pref = Pref + " water/(" + DsPref.Rows(0).Item("TITLE3").ToString() + " or unit)"
            ElseIf (DsData.Rows(0).Item("FINVOLMUNITS") > 0) And (DsData.Rows(i).Item("FINVOLMSI") > 0) Then
                Pref = Pref + " water/(unit or " + DsPref.Rows(0).Item("TITLE3").ToString() + ")"
            ElseIf (DsData.Rows(0).Item("FINVOLMUNITS") > 0) And (DsData.Rows(i).Item("FINVOLMUNITS") > 0) Then
                Pref = Pref + " water/unit "
            ElseIf (DsData.Rows(0).Item("FINVOLMSI") > 0) Then
                Pref = Pref + " water/" + DsPref.Rows(0).Item("TITLE3").ToString()
            ElseIf (DsData.Rows(0).Item("FINVOLMUNITS") > 0) Then
                Pref = Pref + " water/unit"
            ElseIf (DsData.Rows(0).Item("FINVOLMSI") > 0) Then
                Pref = Pref + " water/" + DsPref.Rows(0).Item("TITLE3").ToString()
            ElseIf (DsData.Rows(0).Item("FINVOLMUNITS") > 0) Then
                Pref = Pref + " water/unit"
            ElseIf (DsData.Rows(i).Item("FINVOLMSI") > 0) Then
                Pref = Pref + " water/" + DsPref.Rows(0).Item("TITLE3").ToString()
            ElseIf (DsData.Rows(i).Item("FINVOLMUNITS") > 0) Then
                Pref = Pref + " water/unit"
            Else
                Pref = Pref + " water/unit"
            End If


            For i = 0 To Cnt
                SalesVolumeLb = DsData.Rows(i).Item("VOLUME") * DsPref.Rows(0).Item("CONVWT")
                If DsData.Rows(i).Item("FINVOLMSI") > 0 Then
                    SalesVolumeUnit = DsData.Rows(i).Item("FINVOLMSI") * DsPref.Rows(0).Item("ConvArea")
                ElseIf DsData.Rows(i).Item("FINVOLMUNITS") > 0 Then
                    SalesVolumeUnit = DsData.Rows(i).Item("FINVOLMUNITS")
                End If
                If CDbl(SalesVolumeLb) <> 0 Then
                    lbToUnit = CDbl(SalesVolumeUnit) / CDbl(SalesVolumeLb)
                Else
                    lbToUnit = 0
                End If

                If ChartType = "RBC" Then
                    If CDbl(customerSaleUnit) = 0 Then
                        If CDbl(DsData.Rows(i).Item("VOLUME").ToString()) > 0 Then
                            percentage = CDbl(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * CDbl(DsPref.Rows(0).Item("CONVGALLON").ToString())) * customerSaleValue / CDbl(DsData.Rows(i).Item("VOLUME").ToString())
                        End If
                    Else
                        If CDbl(DsData.Rows(i).Item("SUNITVAL").ToString()) > 0 Then
                            percentage = CDbl(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * CDbl(DsPref.Rows(0).Item("CONVGALLON").ToString())) * customerSaleValue / CDbl(DsData.Rows(i).Item("SUNITVAL").ToString())
                        End If
                    End If
                    If CDbl(customerSaleUnit) = 0 Then
                        If CDbl(customerSaleValue) <> 0 And CDbl(lbToUnit) > 0 Then
                            perUnit = (CDbl(percentage) / CDbl(customerSaleValue * lbToUnit * DsPref.Rows(0).Item("CONVWT")))
                        Else
                            perUnit = 0
                        End If
                    Else
                        If CDbl(customerSaleValue) <> 0 Then
                            If CDbl(DsData.Rows(i).Item("FINVOLMSI").ToString()) > 0 Then
                                perUnit = (CDbl(percentage) / CDbl(customerSaleValue * DsPref.Rows(0).Item("ConvArea")))
                            ElseIf CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString()) > 0 Then
                                perUnit = (CDbl(percentage) / CDbl(customerSaleValue))
                            End If
                        Else
                            perUnit = 0
                        End If
                    End If
                    Data = FormatNumber(perUnit, 3)
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") + ";" & Data & ")"
                    pcScript = pcScript + Graphtype + ".addHoverText(1," + (i + 4).ToString() + ",Value:" + Data.ToString() + ")"
                Else
                    If CDbl(customerSaleUnit) = 0 Then
                        If CDbl(DsData.Rows(i).Item("VOLUME").ToString()) > 0 Then
                            percentage1 = CDbl(DsData.Rows(i).Item("TRNSPTWATER") * CDbl(DsPref.Rows(0).Item("CONVGALLON").ToString()) * customerSaleValue / CDbl(DsData.Rows(i).Item("VOLUME").ToString()))
                            percentage2 = CDbl(DsData.Rows(i).Item("PROCWATER") * CDbl(DsPref.Rows(0).Item("CONVGALLON").ToString()) * customerSaleValue / CDbl(DsData.Rows(i).Item("VOLUME").ToString()))
                            percentage3 = CDbl(DsData.Rows(i).Item("PURMATERIALWATER") * CDbl(DsPref.Rows(0).Item("CONVGALLON").ToString()) * customerSaleValue / CDbl(DsData.Rows(i).Item("VOLUME").ToString()))
                        End If
                    Else
                        If CDbl(DsData.Rows(i).Item("SUNITVAL").ToString()) > 0 Then
                            percentage1 = CDbl(DsData.Rows(i).Item("TRNSPTWATER") * CDbl(DsPref.Rows(0).Item("CONVGALLON").ToString()) * customerSaleValue / CDbl(DsData.Rows(i).Item("SUNITVAL").ToString()))
                            percentage2 = CDbl(DsData.Rows(i).Item("PROCWATER") * CDbl(DsPref.Rows(0).Item("CONVGALLON").ToString()) * customerSaleValue / CDbl(DsData.Rows(i).Item("SUNITVAL").ToString()))
                            percentage3 = CDbl(DsData.Rows(i).Item("PURMATERIALWATER") * CDbl(DsPref.Rows(0).Item("CONVGALLON").ToString()) * customerSaleValue / CDbl(DsData.Rows(i).Item("SUNITVAL").ToString()))
                        End If
                    End If
                    If CDbl(customerSaleUnit) = 0 Then
                        If CDbl(customerSaleValue) <> 0 And CDbl(lbToUnit) > 0 Then
                            Transportation = (CDbl(percentage1) / CDbl(customerSaleValue * lbToUnit * DsPref.Rows(0).Item("CONVWT")))
                            Process = (CDbl(percentage2) / CDbl(customerSaleValue * lbToUnit * DsPref.Rows(0).Item("CONVWT")))
                            Purchased = (CDbl(percentage3) / CDbl(customerSaleValue * lbToUnit * DsPref.Rows(0).Item("CONVWT")))
                        Else
                            Transportation = 0
                            Process = 0
                            Purchased = 0
                        End If
                    Else
                        If CDbl(customerSaleValue) <> 0 Then
                            If CDbl(DsData.Rows(i).Item("FINVOLMSI").ToString()) > 0 Then
                                Transportation = (CDbl(percentage1) / CDbl(customerSaleValue * DsPref.Rows(0).Item("ConvArea")))
                                Process = (CDbl(percentage2) / CDbl(customerSaleValue * DsPref.Rows(0).Item("ConvArea")))
                                Purchased = (CDbl(percentage3) / CDbl(customerSaleValue * DsPref.Rows(0).Item("ConvArea")))
                            ElseIf CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString()) > 0 Then
                                Transportation = (CDbl(percentage1) / CDbl(customerSaleValue))
                                Process = (CDbl(percentage2) / CDbl(customerSaleValue))
                                Purchased = (CDbl(percentage3) / CDbl(customerSaleValue))
                            End If
                        Else
                            Transportation = 0
                            Process = 0
                            Purchased = 0
                        End If
                    End If
                    Transportation = FormatNumber(Transportation, 3)
                    Process = FormatNumber(Process, 3)
                    Purchased = FormatNumber(Purchased, 3)

                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") + ";" & Transportation & ";" & Process & ";" & Purchased & ")"
                    pcScript = pcScript + "" + Graphtype + ".addHoverText(1," + (i + 4).ToString() + ",Value:" + Transportation.ToString() + ")"
                    pcScript = pcScript + "" + Graphtype + ".addHoverText(2," + (i + 4).ToString() + ",Value:" + Process.ToString() + ")"
                    pcScript = pcScript + "" + Graphtype + ".addHoverText(3," + (i + 4).ToString() + ",Value:" + Purchased.ToString() + ")"
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
                SalesVolumeLb = FormatNumber(DsData.Rows(i).Item("VOLUME"), 0)
                If ChartType = "RBC" Then
                    If SalesVolumeLb > 0 Then
                        Data = FormatNumber(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") / SalesVolumeLb, 3)
                    Else
                        Data = 0
                    End If
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & Data & ")"
                Else
                    If SalesVolumeLb > 0 Then
                        Transportation = FormatNumber(DsData.Rows(i).Item("TRNSPTGRNHUSGAS") / SalesVolumeLb, 3)
                        Process = FormatNumber(DsData.Rows(i).Item("PROCGRNHUSGAS") / SalesVolumeLb, 3)
                        Purchased = FormatNumber(DsData.Rows(i).Item("PURMATERIALGRNHUSGAS") / SalesVolumeLb, 3)
                    Else
                        Transportation = 0
                        Process = 0
                        Purchased = 0
                    End If
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & Transportation & ";" & Process & ";" & Purchased & ")"
                    End If

            Next

            GenrateChart(pcScript, GraphName, Pref)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GetBGPerWeightC(ByVal DsData As DataTable, ByVal DsPref As DataTable)
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
            Dim customerSaleValue As Double
            Dim percentage As Double
            Dim DsDataView As DataView
            Dim DtData As DataTable
            Dim customerSaleUnit As Double
            Dim UniToLb As Double
            Dim SalesVolumeUnit As Double
            Dim perWeight As Double
            Dim percentage1 As Double
            Dim percentage2 As Double
            Dim percentage3 As Double

            DsDataView = DsData.DefaultView
            DsDataView.RowFilter = "CASEID = " + Session("S1CaseId")
            DtData = DsDataView.ToTable()

            customerSaleValue = CDbl(DtData.Rows(0).Item("CUSSALESVOLUME").ToString())
            customerSaleUnit = FormatNumber(((CDbl(DtData.Rows(0).Item("CUSSALESUNIT").ToString()))), 0).ToString()

            GraphName = "Sargento_KraftPouchvsTray"
            Graphtype = "graph"
            Cnt = DsData.Rows.Count - 1
            Pref = DsPref.Rows(0).Item("Title8").ToString() + " gr gas /" + DsPref.Rows(0).Item("Title8").ToString()

            If ChartType = "RBC" Then
                lblHeading.Text = "Customer " + ddltype.SelectedItem.Text + " GHG Chart"
                pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
            Else
                lblHeading.Text = "Customer GHG Per Weight Chart"
                pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(Transportation;Process;Purchased Materials)"
            End If
            For i = 0 To Cnt
                If DsData.Rows(i).Item("FINVOLMSI") > 0 Then
                    SalesVolumeUnit = DsData.Rows(i).Item("FINVOLMSI") * DsPref.Rows(0).Item("ConvArea")
                ElseIf DsData.Rows(i).Item("FINVOLMUNITS") > 0 Then
                    SalesVolumeUnit = DsData.Rows(i).Item("FINVOLMUNITS")
                End If
                SalesVolumeLb = DsData.Rows(i).Item("VOLUME") * DsPref.Rows(0).Item("CONVWT")
                If CDbl(SalesVolumeUnit) <> 0 Then
                    UniToLb = CDbl(SalesVolumeLb) / CDbl(SalesVolumeUnit)
                Else
                    UniToLb = 0
                End If

                If ChartType = "RBC" Then
                    If CDbl(customerSaleUnit) = 0 Then
                        If CDbl(DsData.Rows(i).Item("VOLUME").ToString()) > 0 Then
                            percentage = CDbl(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * CDbl(DsPref.Rows(0).Item("CONVWT").ToString()) * customerSaleValue / CDbl(DsData.Rows(i).Item("VOLUME").ToString()))
                        End If
                    Else
                        If CDbl(DsData.Rows(i).Item("SUNITVAL").ToString()) > 0 Then
                            percentage = CDbl(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * CDbl(DsPref.Rows(0).Item("CONVWT").ToString()) * customerSaleValue / CDbl(DsData.Rows(i).Item("SUNITVAL").ToString()))
                        End If
                    End If
                    If CDbl(customerSaleUnit) <> 0 Then
                        If CDbl(customerSaleUnit) <> 0 And CDbl(UniToLb) > 0 Then
                            If CDbl(DsData.Rows(i).Item("FINVOLMSI").ToString()) > 0 Then
                                perWeight = (CDbl(percentage) / CDbl(customerSaleValue * UniToLb * DsPref.Rows(0).Item("CONVAREA")))
                            ElseIf CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString()) > 0 Then
                                perWeight = (CDbl(percentage) / CDbl(customerSaleValue * UniToLb))
                            End If
                        Else
                            perWeight = 0
                        End If
                    Else
                        If CDbl(customerSaleValue) <> 0 Then
                            perWeight = (CDbl(percentage) / CDbl(customerSaleValue * DsPref.Rows(0).Item("CONVWT")))
                        Else
                            perWeight = 0
                        End If
                    End If
                    Data = FormatNumber(perWeight, 3)
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & Data & ")"
                Else
                    If CDbl(customerSaleUnit) = 0 Then
                        If CDbl(DsData.Rows(i).Item("VOLUME").ToString()) > 0 Then
                            percentage1 = CDbl(DsData.Rows(i).Item("TRNSPTGRNHUSGAS") * CDbl(DsPref.Rows(0).Item("CONVWT").ToString()) * customerSaleValue / CDbl(DsData.Rows(i).Item("VOLUME").ToString()))
                            percentage2 = CDbl(DsData.Rows(i).Item("PROCGRNHUSGAS") * CDbl(DsPref.Rows(0).Item("CONVWT").ToString()) * customerSaleValue / CDbl(DsData.Rows(i).Item("VOLUME").ToString()))
                            percentage3 = CDbl(DsData.Rows(i).Item("PURMATERIALGRNHUSGAS") * CDbl(DsPref.Rows(0).Item("CONVWT").ToString()) * customerSaleValue / CDbl(DsData.Rows(i).Item("VOLUME").ToString()))
                        End If
                    Else
                        If CDbl(DsData.Rows(i).Item("SUNITVAL").ToString()) > 0 Then
                            percentage1 = CDbl(DsData.Rows(i).Item("TRNSPTGRNHUSGAS") * CDbl(DsPref.Rows(0).Item("CONVWT").ToString()) * customerSaleValue / CDbl(DsData.Rows(i).Item("SUNITVAL").ToString()))
                            percentage2 = CDbl(DsData.Rows(i).Item("PROCGRNHUSGAS") * CDbl(DsPref.Rows(0).Item("CONVWT").ToString()) * customerSaleValue / CDbl(DsData.Rows(i).Item("SUNITVAL").ToString()))
                            percentage3 = CDbl(DsData.Rows(i).Item("PURMATERIALGRNHUSGAS") * CDbl(DsPref.Rows(0).Item("CONVWT").ToString()) * customerSaleValue / CDbl(DsData.Rows(i).Item("SUNITVAL").ToString()))
                        End If
                    End If
                    If CDbl(customerSaleUnit) <> 0 Then
                        If CDbl(customerSaleUnit) <> 0 And CDbl(UniToLb) > 0 Then
                            If CDbl(DsData.Rows(i).Item("FINVOLMSI").ToString()) > 0 Then
                                Transportation = (CDbl(percentage1) / CDbl(customerSaleValue * UniToLb * DsPref.Rows(0).Item("CONVAREA")))
                                Process = (CDbl(percentage2) / CDbl(customerSaleValue * UniToLb * DsPref.Rows(0).Item("CONVAREA")))
                                Purchased = (CDbl(percentage3) / CDbl(customerSaleValue * UniToLb * DsPref.Rows(0).Item("CONVAREA")))
                            ElseIf CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString()) > 0 Then
                                Transportation = (CDbl(percentage1) / CDbl(customerSaleValue * UniToLb))
                                Process = (CDbl(percentage2) / CDbl(customerSaleValue * UniToLb))
                                Purchased = (CDbl(percentage3) / CDbl(customerSaleValue * UniToLb))
                            End If
                        Else
                            Transportation = 0
                            Process = 0
                            Purchased = 0
                        End If
                    Else
                        If CDbl(customerSaleValue) <> 0 Then
                            Transportation = (CDbl(percentage1) / CDbl(customerSaleValue * DsPref.Rows(0).Item("CONVWT")))
                            Process = (CDbl(percentage2) / CDbl(customerSaleValue * DsPref.Rows(0).Item("CONVWT")))
                            Purchased = (CDbl(percentage3) / CDbl(customerSaleValue * DsPref.Rows(0).Item("CONVWT")))
                        Else
                            Transportation = 0
                            Process = 0
                            Purchased = 0
                        End If
                    End If
                    Transportation = FormatNumber(Transportation, 3)
                    Process = FormatNumber(Process, 3)
                    Purchased = FormatNumber(Purchased, 3)
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & Transportation & ";" & Process & ";" & Purchased & ")"
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
                SalesVolumeLb = FormatNumber(DsData.Rows(i).Item("VOLUME"), 0)
                If ChartType = "RBC" Then
                    If SalesVolumeLb > 0 Then
                        Data = FormatNumber((DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * DsPref.Rows(0).Item("CONVGALLON")) / (SalesVolumeLb * DsPref.Rows(0).Item("CONVWT")), 3)
                    Else
                        Data = 0
                    End If
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & Data & ")"
                Else
                    If SalesVolumeLb > 0 Then
                        Transportation = FormatNumber((DsData.Rows(i).Item("TRNSPTWATER") * DsPref.Rows(0).Item("CONVGALLON")) / (SalesVolumeLb * DsPref.Rows(0).Item("CONVWT")), 3)
                        Process = FormatNumber((DsData.Rows(i).Item("PROCWATER") * DsPref.Rows(0).Item("CONVGALLON")) / (SalesVolumeLb * DsPref.Rows(0).Item("CONVWT")), 3)
                        Purchased = FormatNumber((DsData.Rows(i).Item("PURMATERIALWATER") * DsPref.Rows(0).Item("CONVGALLON")) / (SalesVolumeLb * DsPref.Rows(0).Item("CONVWT")), 3)
                    Else
                        Transportation = 0
                        Process = 0
                        Purchased = 0
                    End If
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & Transportation & ";" & Process & ";" & Purchased & ")"
                    End If

            Next

            GenrateChart(pcScript, GraphName, Pref)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GetBWPerWeightC(ByVal DsData As DataTable, ByVal DsPref As DataTable)
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
            Dim customerSaleValue As Double
            Dim percentage As Double
            Dim DsDataView As DataView
            Dim DtData As DataTable
            Dim customerSaleUnit As Double
            Dim UniToLb As Double
            Dim SalesVolumeUnit As Double
            Dim perWeight As Double
            Dim percentage1 As Double
            Dim percentage2 As Double
            Dim percentage3 As Double

            DsDataView = DsData.DefaultView
            DsDataView.RowFilter = "CASEID = " + Session("S1CaseId")
            DtData = DsDataView.ToTable()

            customerSaleValue = CDbl(DtData.Rows(0).Item("CUSSALESVOLUME").ToString())
            customerSaleUnit = FormatNumber(((CDbl(DtData.Rows(0).Item("CUSSALESUNIT").ToString()))), 0).ToString()

            GraphName = "Sargento_KraftPouchvsTray"
            Graphtype = "graph"
            Cnt = DsData.Rows.Count - 1
            Pref = DsPref.Rows(0).Item("Title10").ToString() + " water /" + DsPref.Rows(0).Item("Title8").ToString()

            If ChartType = "RBC" Then
                lblHeading.Text = "Customer " + ddltype.SelectedItem.Text + " Water Chart"
                pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
            Else
                lblHeading.Text = "Customer Water Per Weight Chart"
                pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(Transportation;Process;Purchased Materials)"
            End If
            For i = 0 To Cnt
                If DsData.Rows(i).Item("FINVOLMSI") > 0 Then
                    SalesVolumeUnit = DsData.Rows(i).Item("FINVOLMSI") * DsPref.Rows(0).Item("ConvArea")
                ElseIf DsData.Rows(i).Item("FINVOLMUNITS") > 0 Then
                    SalesVolumeUnit = DsData.Rows(i).Item("FINVOLMUNITS")
                End If
                SalesVolumeLb = DsData.Rows(i).Item("VOLUME") * DsPref.Rows(0).Item("CONVWT")
                If CDbl(SalesVolumeUnit) <> 0 Then
                    UniToLb = CDbl(SalesVolumeLb) / CDbl(SalesVolumeUnit)
                Else
                    UniToLb = 0
                End If

                If ChartType = "RBC" Then
                    If CDbl(customerSaleUnit) = 0 Then
                        If CDbl(DsData.Rows(i).Item("VOLUME").ToString()) > 0 Then
                            percentage = CDbl(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * CDbl(DsPref.Rows(0).Item("CONVGALLON").ToString()) * customerSaleValue / CDbl(DsData.Rows(i).Item("VOLUME").ToString()))
                        End If
                    Else
                        If CDbl(DsData.Rows(i).Item("SUNITVAL").ToString()) > 0 Then
                            percentage = CDbl(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * CDbl(DsPref.Rows(0).Item("CONVGALLON").ToString()) * customerSaleValue / CDbl(DsData.Rows(i).Item("SUNITVAL").ToString()))
                        End If
                    End If
                    If CDbl(customerSaleUnit) <> 0 Then
                        If CDbl(customerSaleUnit) <> 0 And CDbl(UniToLb) > 0 Then
                            If CDbl(DsData.Rows(i).Item("FINVOLMSI").ToString()) > 0 Then
                                perWeight = (CDbl(percentage) / CDbl(customerSaleValue * UniToLb * DsPref.Rows(0).Item("CONVAREA")))
                            ElseIf CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString()) > 0 Then
                                perWeight = (CDbl(percentage) / CDbl(customerSaleValue * UniToLb))
                            End If
                        Else
                            perWeight = 0
                        End If
                    Else
                        If CDbl(customerSaleValue) <> 0 Then
                            perWeight = (CDbl(percentage) / CDbl(customerSaleValue * DsPref.Rows(0).Item("CONVWT")))
                        Else
                            perWeight = 0
                        End If
                    End If
                    Data = FormatNumber(perWeight, 3)
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & Data & ")"
                Else
                    If CDbl(customerSaleUnit) = 0 Then
                        If CDbl(DsData.Rows(i).Item("VOLUME").ToString()) > 0 Then
                            percentage1 = CDbl(DsData.Rows(i).Item("TRNSPTWATER") * CDbl(DsPref.Rows(0).Item("CONVGALLON").ToString()) * customerSaleValue / CDbl(DsData.Rows(i).Item("VOLUME").ToString()))
                            percentage2 = CDbl(DsData.Rows(i).Item("PROCWATER") * CDbl(DsPref.Rows(0).Item("CONVGALLON").ToString()) * customerSaleValue / CDbl(DsData.Rows(i).Item("VOLUME").ToString()))
                            percentage3 = CDbl(DsData.Rows(i).Item("PURMATERIALWATER") * CDbl(DsPref.Rows(0).Item("CONVGALLON").ToString()) * customerSaleValue / CDbl(DsData.Rows(i).Item("VOLUME").ToString()))
                        End If
                    Else
                        If CDbl(DsData.Rows(i).Item("SUNITVAL").ToString()) > 0 Then
                            percentage1 = CDbl(DsData.Rows(i).Item("TRNSPTWATER") * CDbl(DsPref.Rows(0).Item("CONVGALLON").ToString()) * customerSaleValue / CDbl(DsData.Rows(i).Item("SUNITVAL").ToString()))
                            percentage2 = CDbl(DsData.Rows(i).Item("PROCWATER") * CDbl(DsPref.Rows(0).Item("CONVGALLON").ToString()) * customerSaleValue / CDbl(DsData.Rows(i).Item("SUNITVAL").ToString()))
                            percentage3 = CDbl(DsData.Rows(i).Item("PURMATERIALWATER") * CDbl(DsPref.Rows(0).Item("CONVGALLON").ToString()) * customerSaleValue / CDbl(DsData.Rows(i).Item("SUNITVAL").ToString()))
                        End If
                    End If
                    If CDbl(customerSaleUnit) <> 0 Then
                        If CDbl(customerSaleUnit) <> 0 And CDbl(UniToLb) > 0 Then
                            If CDbl(DsData.Rows(i).Item("FINVOLMSI").ToString()) > 0 Then
                                Transportation = (CDbl(percentage1) / CDbl(customerSaleValue * UniToLb * DsPref.Rows(0).Item("CONVAREA")))
                                Process = (CDbl(percentage2) / CDbl(customerSaleValue * UniToLb * DsPref.Rows(0).Item("CONVAREA")))
                                Purchased = (CDbl(percentage3) / CDbl(customerSaleValue * UniToLb * DsPref.Rows(0).Item("CONVAREA")))
                            ElseIf CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString()) > 0 Then
                                Transportation = (CDbl(percentage1) / CDbl(customerSaleValue * UniToLb))
                                Process = (CDbl(percentage2) / CDbl(customerSaleValue * UniToLb))
                                Purchased = (CDbl(percentage3) / CDbl(customerSaleValue * UniToLb))
                            End If
                        Else
                            Transportation = 0
                            Process = 0
                            Purchased = 0
                        End If
                    Else
                        If CDbl(customerSaleValue) <> 0 Then
                            Transportation = (CDbl(percentage1) / CDbl(customerSaleValue * DsPref.Rows(0).Item("CONVWT")))
                            Process = (CDbl(percentage2) / CDbl(customerSaleValue * DsPref.Rows(0).Item("CONVWT")))
                            Purchased = (CDbl(percentage3) / CDbl(customerSaleValue * DsPref.Rows(0).Item("CONVWT")))
                        Else
                            Transportation = 0
                            Process = 0
                            Purchased = 0
                        End If
                    End If
                    Transportation = FormatNumber(Transportation, 3)
                    Process = FormatNumber(Process, 3)
                    Purchased = FormatNumber(Purchased, 3)
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
                    Process = FormatNumber(DsData.Rows(i).Item("PROCGRNHUSGAS") * DsPref.Rows(0).Item("CONVWT"), 0)
                    Purchased = FormatNumber(DsData.Rows(i).Item("PURMATERIALGRNHUSGAS") * DsPref.Rows(0).Item("CONVWT"), 0)
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & Transportation & ";" & Process & ";" & Purchased & ")"
                End If
            Next

            GenrateChart(pcScript, GraphName, Pref)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GetBGTotalC(ByVal DsData As DataTable, ByVal DsPref As DataTable)
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
            Dim customerSaleValue As Double
            Dim percentage As Double
            Dim DsDataView As DataView
            Dim DtData As DataTable
            Dim customerSaleUnit As Double

            GraphName = "Sargento_KraftPouchvsTray"
            Graphtype = "graph"
            Cnt = DsData.Rows.Count - 1

            DsDataView = DsData.DefaultView
            DsDataView.RowFilter = "CASEID = " + Session("S1CaseId")
            DtData = DsDataView.ToTable()

            customerSaleValue = CDbl(DtData.Rows(0).Item("CUSSALESVOLUME").ToString())
            customerSaleUnit = FormatNumber(((CDbl(DtData.Rows(0).Item("CUSSALESUNIT").ToString()))), 0).ToString()

            Pref = DsPref.Rows(0).Item("TITLE8").ToString()
            If ChartType = "RBC" Then
                lblHeading.Text = "Customer " + ddltype.SelectedItem.Text + " GHG Chart"
                pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
            Else
                lblHeading.Text = "Customer Total GHG Chart"
                pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(Transportation;Process;Purchased Materials)"
            End If

            For i = 0 To Cnt
                If ChartType = "RBC" Then
                    If CDbl(customerSaleUnit) = 0 Then
                        If CDbl(DsData.Rows(i).Item("VOLUME").ToString()) > 0 Then
                            percentage = CDbl(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * CDbl(DsPref.Rows(0).Item("CONVWT").ToString())) * customerSaleValue / CDbl(DsData.Rows(i).Item("VOLUME").ToString())
                        End If
                    Else
                        If CDbl(DsData.Rows(i).Item("SUNITVAL").ToString()) > 0 Then
                            percentage = CDbl(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * CDbl(DsPref.Rows(0).Item("CONVWT").ToString())) * customerSaleValue / CDbl(DsData.Rows(i).Item("SUNITVAL").ToString())
                        End If
                    End If
                    Data = FormatNumber(percentage, 0)
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & Data & ")"
                Else
                    If CDbl(customerSaleUnit) = 0 Then
                        If CDbl(DsData.Rows(i).Item("VOLUME").ToString()) > 0 Then
                            Transportation = CDbl(DsData.Rows(i).Item("TRNSPTGRNHUSGAS") * CDbl(DsPref.Rows(0).Item("CONVWT").ToString())) * customerSaleValue / CDbl(DsData.Rows(i).Item("VOLUME").ToString())
                            Process = CDbl(DsData.Rows(i).Item("PROCGRNHUSGAS") * CDbl(DsPref.Rows(0).Item("CONVWT").ToString())) * customerSaleValue / CDbl(DsData.Rows(i).Item("VOLUME").ToString())
                            Purchased = CDbl(DsData.Rows(i).Item("PURMATERIALGRNHUSGAS") * CDbl(DsPref.Rows(0).Item("CONVWT").ToString())) * customerSaleValue / CDbl(DsData.Rows(i).Item("VOLUME").ToString())
                        End If
                    Else
                        If CDbl(DsData.Rows(i).Item("SUNITVAL").ToString()) > 0 Then
                            Transportation = CDbl(DsData.Rows(i).Item("TRNSPTGRNHUSGAS") * CDbl(DsPref.Rows(0).Item("CONVWT").ToString())) * customerSaleValue / CDbl(DsData.Rows(i).Item("SUNITVAL").ToString())
                            Process = CDbl(DsData.Rows(i).Item("PROCGRNHUSGAS") * CDbl(DsPref.Rows(0).Item("CONVWT").ToString())) * customerSaleValue / CDbl(DsData.Rows(i).Item("SUNITVAL").ToString())
                            Purchased = CDbl(DsData.Rows(i).Item("PURMATERIALGRNHUSGAS") * CDbl(DsPref.Rows(0).Item("CONVWT").ToString())) * customerSaleValue / CDbl(DsData.Rows(i).Item("SUNITVAL").ToString())
                        End If
                    End If
                    Transportation = FormatNumber(Transportation, 0)
                    Process = FormatNumber(Process, 0)
                    Purchased = FormatNumber(Purchased, 0)


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
                    Transportation = FormatNumber(DsData.Rows(i).Item("TRNSPTWater") * DsPref.Rows(0).Item("CONVGALLON"), 0)
                    Process = FormatNumber(DsData.Rows(i).Item("PROCWater") * DsPref.Rows(0).Item("CONVGALLON"), 0)
                    Purchased = FormatNumber(DsData.Rows(i).Item("PURMATERIALWater") * DsPref.Rows(0).Item("CONVGALLON"), 0)
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & Transportation & ";" & Process & ";" & Purchased & ")"
                End If
            Next

            GenrateChart(pcScript, GraphName, Pref)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GetBWTotalC(ByVal DsData As DataTable, ByVal DsPref As DataTable)
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
            Dim customerSaleValue As Double
            Dim percentage As Double
            Dim DsDataView As DataView
            Dim DtData As DataTable
            Dim customerSaleUnit As Double

            GraphName = "Sargento_KraftPouchvsTray"
            Graphtype = "graph"
            Cnt = DsData.Rows.Count - 1

            DsDataView = DsData.DefaultView
            DsDataView.RowFilter = "CASEID = " + Session("S1CaseId")
            DtData = DsDataView.ToTable()

            customerSaleValue = CDbl(DtData.Rows(0).Item("CUSSALESVOLUME").ToString())
            customerSaleUnit = FormatNumber(((CDbl(DtData.Rows(0).Item("CUSSALESUNIT").ToString()))), 0).ToString()

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
                    If CDbl(customerSaleUnit) = 0 Then
                        If CDbl(DsData.Rows(i).Item("VOLUME").ToString()) > 0 Then
                            percentage = CDbl(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * CDbl(DsPref.Rows(0).Item("CONVGALLON").ToString())) * customerSaleValue / CDbl(DsData.Rows(i).Item("VOLUME").ToString())
                        End If
                    Else
                        If CDbl(DsData.Rows(i).Item("SUNITVAL").ToString()) > 0 Then
                            percentage = CDbl(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * CDbl(DsPref.Rows(0).Item("CONVGALLON").ToString())) * customerSaleValue / CDbl(DsData.Rows(i).Item("SUNITVAL").ToString())
                        End If
                    End If
                    Data = FormatNumber(percentage, 0)
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & Data & ")"
                Else
                    If CDbl(customerSaleUnit) = 0 Then
                        If CDbl(DsData.Rows(i).Item("VOLUME").ToString()) > 0 Then
                            Transportation = CDbl(DsData.Rows(i).Item("TRNSPTWater") * CDbl(DsPref.Rows(0).Item("CONVGALLON").ToString())) * customerSaleValue / CDbl(DsData.Rows(i).Item("VOLUME").ToString())
                            Process = CDbl(DsData.Rows(i).Item("PROCWater") * CDbl(DsPref.Rows(0).Item("CONVGALLON").ToString())) * customerSaleValue / CDbl(DsData.Rows(i).Item("VOLUME").ToString())
                            Purchased = CDbl(DsData.Rows(i).Item("PURMATERIALWater") * CDbl(DsPref.Rows(0).Item("CONVGALLON").ToString())) * customerSaleValue / CDbl(DsData.Rows(i).Item("VOLUME").ToString())
                        End If
                    Else
                        If CDbl(DsData.Rows(i).Item("SUNITVAL").ToString()) > 0 Then
                            Transportation = CDbl(DsData.Rows(i).Item("TRNSPTWater") * CDbl(DsPref.Rows(0).Item("CONVGALLON").ToString())) * customerSaleValue / CDbl(DsData.Rows(i).Item("SUNITVAL").ToString())
                            Process = CDbl(DsData.Rows(i).Item("PROCWater") * CDbl(DsPref.Rows(0).Item("CONVGALLON").ToString())) * customerSaleValue / CDbl(DsData.Rows(i).Item("SUNITVAL").ToString())
                            Purchased = CDbl(DsData.Rows(i).Item("PURMATERIALWater") * CDbl(DsPref.Rows(0).Item("CONVGALLON").ToString())) * customerSaleValue / CDbl(DsData.Rows(i).Item("SUNITVAL").ToString())
                        End If
                    End If
                    Transportation = FormatNumber(Transportation, 0)
                    Process = FormatNumber(Process, 0)
                    Purchased = FormatNumber(Purchased, 0)


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
#Region "End Of Life"


    Protected Sub GetEOLResults(ByVal DsData As DataTable, ByVal DsPref As DataTable)
        Try
            Dim Data As String = String.Empty
            Dim Text As String = String.Empty
            Dim Pref As String = String.Empty
            Dim pcScript As String = String.Empty
            Dim Graphtype As String = String.Empty
            Dim GraphName As String = String.Empty
            Dim Cnt As New Integer
            Dim i As New Integer
            Dim customerSaleValue As Double
            Dim percentage As Double
            Dim DsDataView As DataView
            Dim DtData As DataTable
            Dim customerSaleUnit As Double

            GraphName = "Sargento_KraftPouchvsTray"
            Graphtype = "graph"

            DsDataView = DsData.DefaultView
            DsDataView.RowFilter = "CASEID = " + Session("S1CaseId")
            DtData = DsDataView.ToTable()

            customerSaleValue = CDbl(DtData.Rows(0).Item("CUSSALESVOLUME").ToString())
            customerSaleUnit = FormatNumber(((CDbl(DtData.Rows(0).Item("CUSSALESUNIT").ToString()))), 0).ToString()

            Cnt = DsData.Rows.Count - 1
            Pref = DsPref.Rows(0).Item("Title8").ToString()
            lblHeading.Text = ddltype.SelectedItem.Text + " Chart"
            pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"


            For i = 0 To Cnt
                If CDbl(customerSaleUnit) = 0 Then
                    If CDbl(DsData.Rows(i).Item("VOLUME").ToString()) > 0 Then
                        If ddlCnTypeEol.SelectedValue = "MatBalance" Then
                            percentage = CDbl(DsData.Rows(i).Item("MATB" + ddltype.SelectedItem.Value + "") * CDbl(DsPref.Rows(0).Item("CONVWT").ToString())) * customerSaleValue / CDbl(DsData.Rows(i).Item("VOLUME").ToString())
                        ElseIf ddlCnTypeEol.SelectedValue = "MatRec" Then
                            percentage = CDbl(DsData.Rows(i).Item("MATRE" + ddltype.SelectedItem.Value + "") * CDbl(DsPref.Rows(0).Item("CONVWT").ToString())) * customerSaleValue / CDbl(DsData.Rows(i).Item("VOLUME").ToString())
                        ElseIf ddlCnTypeEol.SelectedValue = "MatInc" Then
                            percentage = CDbl(DsData.Rows(i).Item("MATIN" + ddltype.SelectedItem.Value + "") * CDbl(DsPref.Rows(0).Item("CONVWT").ToString())) * customerSaleValue / CDbl(DsData.Rows(i).Item("VOLUME").ToString())
                        ElseIf ddlCnTypeEol.SelectedValue = "MatComp" Then
                            percentage = CDbl(DsData.Rows(i).Item("MATCM" + ddltype.SelectedItem.Value + "") * CDbl(DsPref.Rows(0).Item("CONVWT").ToString())) * customerSaleValue / CDbl(DsData.Rows(i).Item("VOLUME").ToString())
                        ElseIf ddlCnTypeEol.SelectedValue = "MatLandFill" Then
                            If ddltype.SelectedItem.Value = "1" Then
                                percentage = 0.0
                            Else
                                percentage = CDbl(DsData.Rows(i).Item("MATLF" + ddltype.SelectedItem.Value + "") * CDbl(DsPref.Rows(0).Item("CONVWT").ToString())) * customerSaleValue / CDbl(DsData.Rows(i).Item("VOLUME").ToString())
                            End If

                        End If
                    End If
                Else
                    If CDbl(DsData.Rows(i).Item("SUNITVAL").ToString()) > 0 Then
                        If ddlCnTypeEol.SelectedValue = "MatBalance" Then
                            percentage = CDbl(DsData.Rows(i).Item("MATB" + ddltype.SelectedItem.Value + "") * CDbl(DsPref.Rows(0).Item("CONVWT").ToString())) * customerSaleValue / CDbl(DsData.Rows(i).Item("SUNITVAL").ToString())
                        ElseIf ddlCnTypeEol.SelectedValue = "MatRec" Then
                            percentage = CDbl(DsData.Rows(i).Item("MATRE" + ddltype.SelectedItem.Value + "") * CDbl(DsPref.Rows(0).Item("CONVWT").ToString())) * customerSaleValue / CDbl(DsData.Rows(i).Item("SUNITVAL").ToString())
                        ElseIf ddlCnTypeEol.SelectedValue = "MatInc" Then
                            percentage = CDbl(DsData.Rows(i).Item("MATIN" + ddltype.SelectedItem.Value + "") * CDbl(DsPref.Rows(0).Item("CONVWT").ToString())) * customerSaleValue / CDbl(DsData.Rows(i).Item("SUNITVAL").ToString())
                        ElseIf ddlCnTypeEol.SelectedValue = "MatComp" Then
                            percentage = CDbl(DsData.Rows(i).Item("MATCM" + ddltype.SelectedItem.Value + "") * CDbl(DsPref.Rows(0).Item("CONVWT").ToString())) * customerSaleValue / CDbl(DsData.Rows(i).Item("SUNITVAL").ToString())
                        ElseIf ddlCnTypeEol.SelectedValue = "MatLandFill" Then
                            If ddltype.SelectedItem.Value = "1" Then
                                percentage = 0.0
                            Else
                                percentage = CDbl(DsData.Rows(i).Item("MATLF" + ddltype.SelectedItem.Value + "") * CDbl(DsPref.Rows(0).Item("CONVWT").ToString())) * customerSaleValue / CDbl(DsData.Rows(i).Item("SUNITVAL").ToString())
                            End If

                        End If
                    End If
                End If
                Data = FormatNumber(percentage, 0)
                pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & Data & ")"
            Next




            GenrateChart(pcScript, GraphName, Pref)
        Catch ex As Exception
            Response.Write("Error:GetEOLResults:" + ex.Message.ToString() + "")
        End Try
    End Sub

#End Region
    Protected Sub bynUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bynUpdate.Click
        Try

        Catch ex As Exception

        End Try
    End Sub
End Class
