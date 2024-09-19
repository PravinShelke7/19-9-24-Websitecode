Imports Corda
Imports System.Web.UI.HtmlControls
Imports System.Data
Imports System.Web.HttpBrowserCapabilities
Partial Class Charts_Med2Charts_CPrftCost2
    Inherits System.Web.UI.Page
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

#End Region

#Region "Get Set Variables"
    Dim _lErrorLble As Label
    Dim _strPrftCost As String
    Dim _strChartType As String
    Dim _strChartName As String
    Dim _strIsDep As String

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
        Try
            GetMasterPageControls()

            CaseId1 = Session("Med2CaseId")
            CaseId2 = objCryptoHelper.Decrypt(Request.QueryString("CaseId"))
            PrftCost = objCryptoHelper.Decrypt(Request.QueryString("PrftCost"))
            ChartType = objCryptoHelper.Decrypt(Request.QueryString("ChartType"))
            IsDep = objCryptoHelper.Decrypt(Request.QueryString("IsDep"))
            dsData = GetData(CaseId1, CaseId2)
            dsPref = GetPref(Session("UserId"))
            GetPageTitle()
            If Not IsPostBack Then
                Bindddl()
            End If

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
        Catch ex As Exception
            ErrorLable.Text = "Error:Page_Load:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetPageTitle()
        Try
            If PrftCost = "PRFT" Then
                If ChartType = "RBC" Then
                    Page.Title = "Med2-Combine Customer Profit And Loss Bar Chart"
                    rwType.Visible = True
                Else
                    Page.Title = "Med2-Combine Customer Profit And Loss  Stack Chart"
                    rwType.Visible = False
                End If
            Else
                If ChartType = "RBC" Then
                    Page.Title = "Med2-Combine Customer Cost Bar Chartt"
                    rwType.Visible = True
                Else
                    Page.Title = "Med2-Combine Customer Cost Stack Chart"
                    rwType.Visible = False
                End If
            End If
        Catch ex As Exception
            ErrorLable.Text = "Error:GetPageTitle:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub Bindddl()
        Dim ds As New DataSet
        Dim objGetData As New Med2GetData.Selectdata()
        Try
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

    Protected Function GetData(ByVal CaseId1 As String, ByVal CaseId2 As String) As DataSet
        Dim ds As New DataSet
        Dim objGetData As New Med2GetData.Selectdata()
        Try
            If PrftCost = "PRFT" Then
                ds = objGetData.GetChartProfitAndLossRes(CaseId1, CaseId2)
            Else
                ds = objGetData.GetChartCostRes(CaseId1, CaseId2)
            End If
            Return ds
        Catch ex As Exception
            ErrorLable.Text = "Error:GetData:" + ex.Message.ToString()
            Return ds
        End Try
    End Function

    Protected Function GetPref(ByVal UserId As String) As DataSet
        Dim ds As New DataSet
        Dim objGetData As New E1GetData.Selectdata()
        Try
            ds = objGetData.GetChartPrefrences(UserId)
            Return ds
        Catch ex As Exception
            ErrorLable.Text = "Error:GetPref:" + ex.Message.ToString()
            Return ds
        End Try
    End Function

    Protected Sub GetBPPerUnit(ByVal DsData As DataTable, ByVal DsPref As DataTable)
        Try

            Dim Data As String = String.Empty
            'Dim Text As String = String.Empty
            Dim Pref As String = String.Empty
            Dim pcScript As String = String.Empty
            Dim Graphtype As String = String.Empty
            Dim GraphName As String = String.Empty
            Dim Cnt As New Integer
            Dim SalesVolumeUnit As New Decimal
            'Dim VariableMargin As New Decimal
            ' Dim Revenue As New Decimal
            'Dim PlantMargin As New Decimal
            Dim i As New Integer
            Dim customerSaleValue As Double
            Dim PerUnit As Double
            Dim newVal As Double
            Dim SalesVolumeLb As Double

            GraphName = "Sargento_KraftPouchvsTray"
            Graphtype = "graph"
            Cnt = DsData.Rows.Count - 1
            ''FOR UNITS DISPLAY
            i = 0
            If Cnt > 0 Then i = 1
            Pref = DsPref.Rows(0).Item("Title6").ToString() + "/unit "


            lblHeading.Text = ddltype.SelectedItem.Text + " Chart"
            pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"

            For i = 0 To DsData.Rows.Count - 1
                If DsData.Rows(i).Item("CASEID").ToString() = Session("Med2CaseId") Then
                    If CDbl(DsData.Rows(i).Item("CUSSALESUNIT").ToString()) = 0.0 Then
                        customerSaleValue = CDbl(DsData.Rows(i).Item("CUSSALESVOLUME").ToString()) * DsPref.Rows(0).Item("CONVWT")
                    Else
                        customerSaleValue = CDbl(DsData.Rows(i).Item("CUSSALESVOLUME").ToString())
                    End If
                End If
            Next

            For i = 0 To Cnt
                If CDbl(DsData.Rows(i).Item("CUSSALESUNIT").ToString()) = 0 Then
                    newVal = CDbl(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "")) * CDbl(DsPref.Rows(0).Item("CURR").ToString()) * customerSaleValue
                    If CDbl(DsData.Rows(i).Item("PVOLUSE")) = 0.0 Then
                        SalesVolumeLb = DsData.Rows(i).Item("VOLUME") * DsPref.Rows(0).Item("CONVWT")
                    Else
                        SalesVolumeLb = DsData.Rows(i).Item("SVOLUME") * DsPref.Rows(0).Item("CONVWT")
                    End If
                    If SalesVolumeLb > 0 Then
                        PerUnit = newVal / SalesVolumeLb
                    End If
                Else
                    If CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString()) > 0 Then
                        PerUnit = CDbl(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "")) * CDbl(DsData.Rows(i).Item("CURR").ToString()) * customerSaleValue / CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString())
                    End If
                End If

                If DsData.Rows(i).Item("FINVOLMUNITS") > 0 Then
                    SalesVolumeUnit = DsData.Rows(i).Item("FINVOLMUNITS")
                End If

                Data = FormatNumber(PerUnit * 100 / SalesVolumeUnit, 4)
                pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & Data & ")"
                pcScript = pcScript + Graphtype + ".addHoverText(1," + (i + 1).ToString() + ",Value:" + Data.ToString() + "<br/>)"

            Next

            GenrateChart(pcScript, GraphName, Pref)

        Catch ex As Exception
            Response.Write("Error:GetPerUnit:" + ex.Message.ToString())
        End Try
    End Sub

    Protected Sub GetBPPerWeight(ByVal DsData As DataTable, ByVal DsPref As DataTable)
        Try
            Dim Data As String = String.Empty
            'Dim Text As String = String.Empty
            Dim Pref As String = String.Empty
            Dim pcScript As String = String.Empty
            Dim Graphtype As String = String.Empty
            Dim GraphName As String = String.Empty
            Dim Cnt As New Integer
            Dim SalesVolumeLb As New Decimal
            Dim i As New Integer
            Dim customerSaleValue As Double
            Dim PerWt As Double
            Dim newVal As Double

            GraphName = "Sargento_KraftPouchvsTray"
            Graphtype = "graph"
            Cnt = DsData.Rows.Count - 1
            Pref = DsPref.Rows(0).Item("Title2").ToString() + "/" + DsPref.Rows(0).Item("Title8").ToString()

            lblHeading.Text = ddltype.SelectedItem.Text + " Chart"
            pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"

            For i = 0 To DsData.Rows.Count - 1
                If DsData.Rows(i).Item("CASEID").ToString() = Session("Med2CaseId") Then
                    If DsData.Rows(i).Item("CASEID").ToString() = Session("Med2CaseId") Then
                        If CDbl(DsData.Rows(i).Item("CUSSALESUNIT").ToString()) = 0.0 Then
                            customerSaleValue = CDbl(DsData.Rows(i).Item("CUSSALESVOLUME").ToString()) * DsPref.Rows(0).Item("CONVWT")
                        Else
                            customerSaleValue = CDbl(DsData.Rows(i).Item("CUSSALESVOLUME").ToString())
                        End If
                    End If
                End If
            Next

            For i = 0 To Cnt
                'SalesVolumeLb = DsData.Rows(i).Item("VOLUME") * DsPref.Rows(0).Item("CONVWT")
                If CDbl(DsData.Rows(i).Item("PVOLUSE")) = 0.0 Then
                    SalesVolumeLb = DsData.Rows(i).Item("VOLUME") * DsPref.Rows(0).Item("CONVWT")
                Else
                    SalesVolumeLb = DsData.Rows(i).Item("SVOLUME") * DsPref.Rows(0).Item("CONVWT")
                End If
                If CDbl(DsData.Rows(i).Item("CUSSALESUNIT").ToString()) = 0 Then
                    newVal = CDbl(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "")) * CDbl(DsPref.Rows(0).Item("CURR").ToString()) * customerSaleValue

                    If SalesVolumeLb > 0 Then
                        PerWt = newVal / SalesVolumeLb
                    End If
                Else
                    If CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString()) > 0 Then
                        PerWt = CDbl(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "")) * CDbl(DsData.Rows(i).Item("CURR").ToString()) * customerSaleValue / CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString())
                    End If
                End If



                ''If CDbl(DsData.Rows(i).Item("SALESVOLUMELB").ToString()) > 0 Then
                ''    'PerWt = CDbl(DsData.Rows(i).Item("PL" + i.ToString() + "").ToString() * CDbl(DsData.Rows(i).Item("CURR").ToString())) * customerSaleValue / CDbl(DsData.Rows(i).Item("SALESVOLUMELB").ToString())
                ''    PerWt = CDbl(newVal) / SalesVolumeLb / CDbl(DsData.Rows(i).Item("CONVWT").ToString())
                ''    Data = FormatNumber(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * DsPref.Rows(0).Item("curr"), 0)
                ''Else
                ''    PerWt = 0
                ''End If
                If SalesVolumeLb > 0 Then
                    PerWt = PerWt / SalesVolumeLb
                Else
                    PerWt = 0
                End If
                Data = FormatNumber(PerWt, 3)

                pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & Data & ")"
            Next

            GenrateChart(pcScript, GraphName, Pref)
        Catch ex As Exception
            Response.Write("Error:GetPerWeight:" + ex.Message.ToString())
        End Try
    End Sub

    Protected Sub GetBPTotal(ByVal DsData As DataTable, ByVal DsPref As DataTable)
        Try
            Dim Data As String = String.Empty
            Dim Pref As String = String.Empty
            Dim pcScript As String = String.Empty
            Dim Graphtype As String = String.Empty
            Dim GraphName As String = String.Empty
            Dim Cnt As New Integer
            Dim i As New Integer
            Dim customerSaleValue As Double
            Dim newVal As Double
            Dim SalesVolumeLb As Double

            GraphName = "Sargento_KraftPouchvsTray"
            Graphtype = "graph"
            Cnt = DsData.Rows.Count - 1
            Pref = DsPref.Rows(0).Item("TITLE2").ToString() + ""

            lblHeading.Text = ddltype.SelectedItem.Text + " Chart"
            pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"

            For i = 0 To DsData.Rows.Count - 1
                If DsData.Rows(i).Item("CASEID").ToString() = Session("Med2CaseId") Then
                    If CDbl(DsData.Rows(i).Item("CUSSALESUNIT").ToString()) = 0.0 Then
                        customerSaleValue = CDbl(DsData.Rows(i).Item("CUSSALESVOLUME").ToString()) * DsPref.Rows(0).Item("CONVWT")
                    Else
                        customerSaleValue = CDbl(DsData.Rows(i).Item("CUSSALESVOLUME").ToString())
                    End If

                End If
            Next

            For i = 0 To Cnt
                Data = 0
                If CDbl(DsData.Rows(i).Item("CUSSALESUNIT").ToString()) = 0 Then
                    newVal = CDbl(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "")) * CDbl(DsPref.Rows(0).Item("CURR").ToString()) * customerSaleValue
                    If CDbl(DsData.Rows(i).Item("PVOLUSE")) = 0.0 Then
                        SalesVolumeLb = DsData.Rows(i).Item("VOLUME") * DsPref.Rows(0).Item("CONVWT")
                    Else
                        SalesVolumeLb = DsData.Rows(i).Item("SVOLUME") * DsPref.Rows(0).Item("CONVWT")
                    End If
                    If SalesVolumeLb > 0 Then
                        Data = FormatNumber(newVal / SalesVolumeLb, 0)
                    End If
                Else
                    If CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString()) > 0 Then
                        Data = FormatNumber(CDbl(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "")) * CDbl(DsData.Rows(i).Item("CURR").ToString()) * customerSaleValue / CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString()), 0)
                    End If
                End If


                pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & Data & ")"
            Next

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
            Dim VariableCost As String = String.Empty
            Dim FixedCost As String = String.Empty
            Dim TotalCost As String = String.Empty
            Dim i As New Integer
            Dim SalesVolumeLb As Double
            Dim customerSaleValue As Double
            Dim newVal As Double

            GraphName = "Sargento_KraftPouchvsTray"
            Graphtype = "graph"
            Cnt = DsData.Rows.Count - 1

            i = 0
            If Cnt > 0 Then i = 1
            Pref = DsPref.Rows(0).Item("Title6").ToString() + "/unit "

            If ChartType = "RBC" Then
                lblHeading.Text = ddltype.SelectedItem.Text + " Chart"
                pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
            Else
                lblHeading.Text = "Total Currency Per Unit Chart"
                pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(Variable Cost;Fixed Cost)"
            End If

            For i = 0 To DsData.Rows.Count - 1
                If DsData.Rows(i).Item("CASEID").ToString() = Session("Med2CaseId") Then
                    If CDbl(DsData.Rows(i).Item("CUSSALESUNIT").ToString()) = 0.0 Then
                        customerSaleValue = CDbl(DsData.Rows(i).Item("CUSSALESVOLUME").ToString()) * DsPref.Rows(0).Item("CONVWT")
                    Else
                        customerSaleValue = CDbl(DsData.Rows(i).Item("CUSSALESVOLUME").ToString())
                    End If

                End If
            Next


            For i = 0 To Cnt

                If CDbl(DsData.Rows(i).Item("PVOLUSE")) = 0.0 Then
                    SalesVolumeLb = DsData.Rows(i).Item("VOLUME") * DsPref.Rows(0).Item("CONVWT")
                Else
                    SalesVolumeLb = DsData.Rows(i).Item("SVOLUME") * DsPref.Rows(0).Item("CONVWT")
                End If

                SalesVolumeUnit = DsData.Rows(i).Item("FINVOLMUNITS")


                If CDbl(DsData.Rows(i).Item("CUSSALESUNIT").ToString()) = 0 Then
                    If SalesVolumeLb > 0 Then
                        Data = "0"
                        If ChartType = "RBC" Then
                            newVal = CDbl(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "")) * CDbl(DsPref.Rows(0).Item("CURR").ToString()) * customerSaleValue
                            newVal = newVal / SalesVolumeLb
                            If SalesVolumeUnit > 0 Then
                                Data = FormatNumber(newVal * 100 / SalesVolumeUnit, 4)
                            End If

                            pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & Data & ")"
                            pcScript = pcScript + Graphtype + ".addHoverText(1," + (i + 1).ToString() + ",Value:" + Data.ToString() + "<br/>)"
                        Else
                            If SalesVolumeUnit > 0 Then
                                If IsDep = "N" Then
                                    VariableCost = DsData.Rows(i).Item("VARIABLECOST") * DsPref.Rows(0).Item("curr") * customerSaleValue / SalesVolumeLb
                                    FixedCost = DsData.Rows(i).Item("FIXEDCOST") * DsPref.Rows(0).Item("curr") * customerSaleValue / SalesVolumeLb
                                    TotalCost = DsData.Rows(i).Item("TOTALCOST") * DsPref.Rows(0).Item("curr") * customerSaleValue / SalesVolumeLb
                                Else
                                    VariableCost = DsData.Rows(i).Item("VARIABLECOST") * DsPref.Rows(0).Item("curr") * customerSaleValue / SalesVolumeLb
                                    FixedCost = DsData.Rows(i).Item("FIXEDCOSTDEP") * DsPref.Rows(0).Item("curr") * customerSaleValue / SalesVolumeLb
                                    TotalCost = DsData.Rows(i).Item("TOTALCOSTDEP") * DsPref.Rows(0).Item("curr") * customerSaleValue / SalesVolumeLb
                                End If
                                VariableCost = FormatNumber(VariableCost * 100 / SalesVolumeUnit, 4)
                                FixedCost = FormatNumber(FixedCost * 100 / SalesVolumeUnit, 4)
                                TotalCost = FormatNumber(TotalCost * 100 / SalesVolumeUnit, 4)
                            End If
                            pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & VariableCost & ";" & FixedCost & ")"
                            pcScript = pcScript + "" + Graphtype + ".addHoverText(1," + (i + 1).ToString() + ",Value:" + VariableCost.ToString() + ")"
                            pcScript = pcScript + "" + Graphtype + ".addHoverText(2," + (i + 1).ToString() + ",Value:" + FixedCost.ToString() + ")"
                            pcScript = pcScript + "" + Graphtype + ".addHoverText(3," + (i + 1).ToString() + ",Value:" + TotalCost.ToString() + ")"
                        End If


                    End If
                Else
                    If CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString()) > 0 Then
                        Data = "0"
                        If ChartType = "RBC" Then
                            newVal = CDbl(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "")) * CDbl(DsPref.Rows(0).Item("CURR").ToString()) * customerSaleValue / CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString())
                            If SalesVolumeUnit > 0 Then
                                Data = FormatNumber(newVal * 100 / SalesVolumeUnit, 4)
                            End If
                            pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & Data & ")"
                        Else
                            If SalesVolumeUnit > 0 Then
                                If IsDep = "N" Then
                                    VariableCost = DsData.Rows(i).Item("VARIABLECOST") * DsPref.Rows(0).Item("curr") * customerSaleValue / CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString())
                                    FixedCost = DsData.Rows(i).Item("FIXEDCOST") * DsPref.Rows(0).Item("curr") * customerSaleValue / CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString())
                                    TotalCost = DsData.Rows(i).Item("TOTALCOST") * DsPref.Rows(0).Item("curr") * customerSaleValue / CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString())
                                Else
                                    VariableCost = DsData.Rows(i).Item("VARIABLECOST") * DsPref.Rows(0).Item("curr") * customerSaleValue / CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString())
                                    FixedCost = DsData.Rows(i).Item("FIXEDCOSTDEP") * DsPref.Rows(0).Item("curr") * customerSaleValue / CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString())
                                    TotalCost = DsData.Rows(i).Item("TOTALCOSTDEP") * DsPref.Rows(0).Item("curr") * customerSaleValue / CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString())
                                End If
                                VariableCost = FormatNumber(VariableCost * 100 / SalesVolumeUnit, 4)
                                FixedCost = FormatNumber(FixedCost * 100 / SalesVolumeUnit, 4)
                                TotalCost = FormatNumber(TotalCost * 100 / SalesVolumeUnit, 4)
                            End If
                            pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & VariableCost & ";" & FixedCost & ")"
                            pcScript = pcScript + "" + Graphtype + ".addHoverText(1," + (i + 1).ToString() + ",Value:" + VariableCost.ToString() + ")"
                            pcScript = pcScript + "" + Graphtype + ".addHoverText(2," + (i + 1).ToString() + ",Value:" + FixedCost.ToString() + ")"
                            pcScript = pcScript + "" + Graphtype + ".addHoverText(3," + (i + 1).ToString() + ",Value:" + TotalCost.ToString() + ")"
                            pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & VariableCost & ";" & FixedCost & ")"
                        End If
                    End If
                End If







                ''If ChartType = "RBC" Then
                ''    Data = FormatNumber((DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * DsPref.Rows(0).Item("curr")) * 100 / SalesVolumeUnit, 4)
                ''    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & Data & ")"
                ''    pcScript = pcScript + Graphtype + ".addHoverText(1," + (i + 1).ToString() + ",Value:" + Data.ToString() + "<br/>)"
                ''Else
                ''    If IsDep = "N" Then

                ''        VariableCost = FormatNumber((DsData.Rows(i).Item("VARIABLECOST") * DsPref.Rows(0).Item("curr")) * 100 / SalesVolumeUnit, 4)
                ''        FixedCost = FormatNumber((DsData.Rows(i).Item("FIXEDCOST") * DsPref.Rows(0).Item("curr")) * 100 / SalesVolumeUnit, 4)
                ''        TotalCost = FormatNumber((DsData.Rows(i).Item("TOTALCOST") * DsPref.Rows(0).Item("curr")) * 100 / SalesVolumeUnit, 4)
                ''        pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & VariableCost & ";" & FixedCost & ")"
                ''        pcScript = pcScript + "" + Graphtype + ".addHoverText(1," + (i + 1).ToString() + ",Value:" + VariableCost.ToString() + ")"
                ''        pcScript = pcScript + "" + Graphtype + ".addHoverText(2," + (i + 1).ToString() + ",Value:" + FixedCost.ToString() + ")"
                ''        pcScript = pcScript + "" + Graphtype + ".addHoverText(3," + (i + 1).ToString() + ",Value:" + TotalCost.ToString() + ")"
                ''    Else
                ''        VariableCost = FormatNumber((DsData.Rows(i).Item("VARIABLECOST") * DsPref.Rows(0).Item("curr")) * 100 / SalesVolumeUnit, 4)
                ''        FixedCost = FormatNumber((DsData.Rows(i).Item("FIXEDCOSTDEP") * DsPref.Rows(0).Item("curr")) * 100 / SalesVolumeUnit, 4)
                ''        TotalCost = FormatNumber((DsData.Rows(i).Item("TOTALCOSTDEP") * DsPref.Rows(0).Item("curr")) * 100 / SalesVolumeUnit, 4)
                ''        pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & VariableCost & ";" & FixedCost & ")"
                ''        pcScript = pcScript + "" + Graphtype + ".addHoverText(1," + (i + 1).ToString() + ",Value:" + VariableCost.ToString() + ")"
                ''        pcScript = pcScript + "" + Graphtype + ".addHoverText(2," + (i + 1).ToString() + ",Value:" + FixedCost.ToString() + ")"
                ''        pcScript = pcScript + "" + Graphtype + ".addHoverText(3," + (i + 1).ToString() + ",Value:" + TotalCost.ToString() + ")"
                ''    End If
                ''End If


            Next

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
            Dim VariableCost As String = String.Empty
            Dim FixedCost As String = String.Empty
            Dim TotalCost As String = String.Empty
            Dim Cnt As New Integer
            Dim SalesVolumeLb As New Decimal
            Dim i As New Integer

            Dim customerSaleValue As Double
            Dim newVal As Double

            GraphName = "Sargento_KraftPouchvsTray"
            Graphtype = "graph"
            Cnt = DsData.Rows.Count - 1
            Pref = DsPref.Rows(0).Item("Title2").ToString() + "/" + DsPref.Rows(0).Item("Title8").ToString()
            If ChartType = "RBC" Then
                lblHeading.Text = ddltype.SelectedItem.Text + " Cost Chart"
                pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
            Else
                lblHeading.Text = "Total Currency Per Weight Chart"
                pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(Variable Cost;Fixed Cost)"
            End If

            For i = 0 To DsData.Rows.Count - 1
                If DsData.Rows(i).Item("CASEID").ToString() = Session("Med2CaseId") Then
                    If CDbl(DsData.Rows(i).Item("CUSSALESUNIT").ToString()) = 0.0 Then
                        customerSaleValue = CDbl(DsData.Rows(i).Item("CUSSALESVOLUME").ToString()) * DsPref.Rows(0).Item("CONVWT")
                    Else
                        customerSaleValue = CDbl(DsData.Rows(i).Item("CUSSALESVOLUME").ToString())
                    End If

                End If
            Next

            For i = 0 To Cnt
                If CDbl(DsData.Rows(i).Item("PVOLUSE")) = 0.0 Then
                    SalesVolumeLb = DsData.Rows(i).Item("VOLUME") * DsPref.Rows(0).Item("CONVWT")
                Else
                    SalesVolumeLb = DsData.Rows(i).Item("SVOLUME") * DsPref.Rows(0).Item("CONVWT")
                End If


                If CDbl(DsData.Rows(i).Item("CUSSALESUNIT").ToString()) = 0 Then
                    If SalesVolumeLb > 0 Then
                        Data = "0"
                        If ChartType = "RBC" Then
                            newVal = CDbl(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "")) * CDbl(DsPref.Rows(0).Item("CURR").ToString()) * customerSaleValue
                            newVal = newVal / SalesVolumeLb
                            Data = FormatNumber(newVal / SalesVolumeLb, 0)
                            pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & Data & ")"
                        Else
                            If IsDep = "N" Then
                                VariableCost = DsData.Rows(i).Item("VARIABLECOST") * DsPref.Rows(0).Item("curr") * customerSaleValue / SalesVolumeLb
                                FixedCost = DsData.Rows(i).Item("FIXEDCOST") * DsPref.Rows(0).Item("curr") * customerSaleValue / SalesVolumeLb
                                TotalCost = DsData.Rows(i).Item("TOTALCOST") * DsPref.Rows(0).Item("curr") * customerSaleValue / SalesVolumeLb
                            Else
                                VariableCost = DsData.Rows(i).Item("VARIABLECOST") * DsPref.Rows(0).Item("curr") * customerSaleValue / SalesVolumeLb
                                FixedCost = DsData.Rows(i).Item("FIXEDCOSTDEP") * DsPref.Rows(0).Item("curr") * customerSaleValue / SalesVolumeLb
                                TotalCost = DsData.Rows(i).Item("TOTALCOSTDEP") * DsPref.Rows(0).Item("curr") * customerSaleValue / SalesVolumeLb
                            End If
                            VariableCost = FormatNumber(VariableCost / SalesVolumeLb, 4)
                            FixedCost = FormatNumber(FixedCost / SalesVolumeLb, 4)
                            TotalCost = FormatNumber(TotalCost / SalesVolumeLb, 4)
                            pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & VariableCost & ";" & FixedCost & ")"
                        End If
                    End If
                Else
                    If CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString()) > 0 Then
                        Data = "0"
                        If ChartType = "RBC" Then
                            newVal = CDbl(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "")) * CDbl(DsPref.Rows(0).Item("CURR").ToString()) * customerSaleValue / CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString())
                            If SalesVolumeLb > 0 Then
                                Data = FormatNumber(newVal / SalesVolumeLb, 0)
                            End If
                            pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & Data & ")"
                        Else
                            If SalesVolumeLb > 0 Then
                                If IsDep = "N" Then
                                    VariableCost = DsData.Rows(i).Item("VARIABLECOST") * DsPref.Rows(0).Item("curr") * customerSaleValue / CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString())
                                    FixedCost = DsData.Rows(i).Item("FIXEDCOST") * DsPref.Rows(0).Item("curr") * customerSaleValue / CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString())
                                    TotalCost = DsData.Rows(i).Item("TOTALCOST") * DsPref.Rows(0).Item("curr") * customerSaleValue / CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString())
                                Else
                                    VariableCost = DsData.Rows(i).Item("VARIABLECOST") * DsPref.Rows(0).Item("curr") * customerSaleValue / CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString())
                                    FixedCost = DsData.Rows(i).Item("FIXEDCOSTDEP") * DsPref.Rows(0).Item("curr") * customerSaleValue / CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString())
                                    TotalCost = DsData.Rows(i).Item("TOTALCOSTDEP") * DsPref.Rows(0).Item("curr") * customerSaleValue / CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString())
                                End If
                                VariableCost = FormatNumber(VariableCost / SalesVolumeLb, 4)
                                FixedCost = FormatNumber(FixedCost / SalesVolumeLb, 4)
                                TotalCost = FormatNumber(TotalCost / SalesVolumeLb, 4)
                            End If

                            pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & VariableCost & ";" & FixedCost & ")"
                        End If
                    End If
                End If


                ''If ChartType = "RBC" Then
                ''    Data = FormatNumber(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * DsPref.Rows(0).Item("curr") / SalesVolumeLb, 3)
                ''    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & Data & ")"
                ''Else
                ''    If IsDep = "N" Then
                ''        VariableCost = FormatNumber((DsData.Rows(i).Item("VARIABLECOST") * DsPref.Rows(0).Item("curr")) / SalesVolumeLb, 3)
                ''        FixedCost = FormatNumber((DsData.Rows(i).Item("FIXEDCOST") * DsPref.Rows(0).Item("curr")) / SalesVolumeLb, 3)
                ''        TotalCost = FormatNumber((DsData.Rows(i).Item("TOTALCOST") * DsPref.Rows(0).Item("curr")) / SalesVolumeLb, 3)
                ''    Else
                ''        VariableCost = FormatNumber((DsData.Rows(i).Item("VARIABLECOST") * DsPref.Rows(0).Item("curr")) / SalesVolumeLb, 3)
                ''        FixedCost = FormatNumber((DsData.Rows(i).Item("FIXEDCOSTDEP") * DsPref.Rows(0).Item("curr")) / SalesVolumeLb, 3)
                ''        TotalCost = FormatNumber((DsData.Rows(i).Item("TOTALCOSTDEP") * DsPref.Rows(0).Item("curr")) / SalesVolumeLb, 3)
                ''    End If

                ''    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & VariableCost & ";" & FixedCost & ")"
                ''End If

            Next

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
            Dim VariableCost As String = String.Empty
            Dim FixedCost As String = String.Empty
            Dim TotalCost As String = String.Empty
            Dim i As New Integer
            Dim SalesVolumeLb As Double
            Dim customerSaleValue As Double
            Dim newVal As Double

            GraphName = "Sargento_KraftPouchvsTray"
            Graphtype = "graph"
            Cnt = DsData.Rows.Count - 1
            Pref = DsPref.Rows(0).Item("TITLE2").ToString() + ""
            If ChartType = "RBC" Then
                lblHeading.Text = ddltype.SelectedItem.Text + " Chart"
                pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
            Else
                lblHeading.Text = "Total Currency Chart"
                pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(Variable Cost;Fixed Cost)"
            End If

            For i = 0 To DsData.Rows.Count - 1
                If DsData.Rows(i).Item("CASEID").ToString() = Session("Med2CaseId") Then
                    If CDbl(DsData.Rows(i).Item("CUSSALESUNIT").ToString()) = 0.0 Then
                        customerSaleValue = CDbl(DsData.Rows(i).Item("CUSSALESVOLUME").ToString()) * DsPref.Rows(0).Item("CONVWT")
                    Else
                        customerSaleValue = CDbl(DsData.Rows(i).Item("CUSSALESVOLUME").ToString())
                    End If

                End If
            Next

            For i = 0 To Cnt
                If CDbl(DsData.Rows(i).Item("PVOLUSE")) = 0.0 Then
                    SalesVolumeLb = DsData.Rows(i).Item("VOLUME") * DsPref.Rows(0).Item("CONVWT")
                Else
                    SalesVolumeLb = DsData.Rows(i).Item("SVOLUME") * DsPref.Rows(0).Item("CONVWT")
                End If

                If CDbl(DsData.Rows(i).Item("CUSSALESUNIT").ToString()) = 0 Then
                    If SalesVolumeLb > 0 Then
                        Data = "0"
                        If ChartType = "RBC" Then
                            newVal = CDbl(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "")) * CDbl(DsPref.Rows(0).Item("CURR").ToString()) * customerSaleValue
                            newVal = newVal / SalesVolumeLb
                            Data = FormatNumber(newVal, 0)
                            pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & Data & ")"
                        Else
                            If IsDep = "N" Then
                                VariableCost = FormatNumber(DsData.Rows(i).Item("VARIABLECOST") * DsPref.Rows(0).Item("curr") * customerSaleValue / SalesVolumeLb, 0)
                                FixedCost = FormatNumber(DsData.Rows(i).Item("FIXEDCOST") * DsPref.Rows(0).Item("curr") * customerSaleValue / SalesVolumeLb, 0)
                                TotalCost = FormatNumber(DsData.Rows(i).Item("TOTALCOST") * DsPref.Rows(0).Item("curr") * customerSaleValue / SalesVolumeLb, 0)
                            Else
                                VariableCost = FormatNumber(DsData.Rows(i).Item("VARIABLECOST") * DsPref.Rows(0).Item("curr") * customerSaleValue / SalesVolumeLb, 0)
                                FixedCost = FormatNumber(DsData.Rows(i).Item("FIXEDCOSTDEP") * DsPref.Rows(0).Item("curr") * customerSaleValue / SalesVolumeLb, 0)
                                TotalCost = FormatNumber(DsData.Rows(i).Item("TOTALCOSTDEP") * DsPref.Rows(0).Item("curr") * customerSaleValue / SalesVolumeLb, 0)
                            End If

                            pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & VariableCost & ";" & FixedCost & ")"
                        End If
                    End If
                Else
                    If CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString()) > 0 Then
                        Data = "0"
                        If ChartType = "RBC" Then
                            newVal = CDbl(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "")) * CDbl(DsPref.Rows(0).Item("CURR").ToString()) * customerSaleValue / CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString())
                            Data = FormatNumber(newVal, 0)
                            pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & Data & ")"
                        Else
                            If IsDep = "N" Then
                                VariableCost = FormatNumber(DsData.Rows(i).Item("VARIABLECOST") * DsPref.Rows(0).Item("curr") * customerSaleValue / CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString()), 0)
                                FixedCost = FormatNumber(DsData.Rows(i).Item("FIXEDCOST") * DsPref.Rows(0).Item("curr") * customerSaleValue / CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString()), 0)
                                TotalCost = FormatNumber(DsData.Rows(i).Item("TOTALCOST") * DsPref.Rows(0).Item("curr") * customerSaleValue / CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString()), 0)
                            Else
                                VariableCost = FormatNumber(DsData.Rows(i).Item("VARIABLECOST") * DsPref.Rows(0).Item("curr") * customerSaleValue / CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString()), 0)
                                FixedCost = FormatNumber(DsData.Rows(i).Item("FIXEDCOSTDEP") * DsPref.Rows(0).Item("curr") * customerSaleValue / CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString()), 0)
                                TotalCost = FormatNumber(DsData.Rows(i).Item("TOTALCOSTDEP") * DsPref.Rows(0).Item("curr") * customerSaleValue / CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString()), 0)
                            End If
                            pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & VariableCost & ";" & FixedCost & ")"
                        End If
                    End If
                End If

                ''If ChartType = "RBC" Then
                ''    Data = FormatNumber(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * DsPref.Rows(0).Item("curr"), 0)
                ''    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & Data & ")"
                ''Else
                ''    If IsDep = "N" Then
                ''        VariableCost = FormatNumber(DsData.Rows(i).Item("VARIABLECOST") * DsPref.Rows(0).Item("curr"), 0)
                ''        FixedCost = FormatNumber(DsData.Rows(i).Item("FIXEDCOST") * DsPref.Rows(0).Item("curr"), 0)
                ''        TotalCost = FormatNumber(DsData.Rows(i).Item("TOTALCOST") * DsPref.Rows(0).Item("curr"), 0)
                ''    Else
                ''        VariableCost = FormatNumber(DsData.Rows(i).Item("VARIABLECOST") * DsPref.Rows(0).Item("curr"), 0)
                ''        FixedCost = FormatNumber(DsData.Rows(i).Item("FIXEDCOSTDEP") * DsPref.Rows(0).Item("curr"), 0)
                ''        TotalCost = FormatNumber(DsData.Rows(i).Item("TOTALCOSTDEP") * DsPref.Rows(0).Item("curr"), 0)
                ''    End If

                ''    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & VariableCost & ";" & FixedCost & ")"
                ''End If

            Next

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
            myImage.imageTemplate = "Sargento_KraftPouchvsTray_Html" + ".itxml"
            myImage.userAgent = Request.UserAgent
            myImage.width = 700
            myImage.height = 400
            myImage.returnDescriptiveLink = True
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
End Class
