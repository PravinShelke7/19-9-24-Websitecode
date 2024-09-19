Imports Corda
Imports System.Web.UI.HtmlControls
Imports System.Data
Imports System.Data.OleDb
Imports System.Web.HttpBrowserCapabilities
Partial Class Charts_SDistCharts_CStackWATERChart2
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


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("back") = "" Then
                'lblsession.Visible = True
                'Charts.Visible = False
                Dim obj As New CryptoHelper
                Response.Redirect("../../Pages/Sustain2/Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            Else
                'Begin Embedder Code
                InitializeComponent()
                Dim DsData As New DataTable
                Dim DsPref As New DataTable
                Dim objCryptoHelper As New CryptoHelper()
                'Session("C1") = "1"
                'Session("C2") = "1066"
                'Session("User") = "Administrator"
                Dim CaseId1 As String = Session("SDistCaseId")
                Dim CaseId2 As String = objCryptoHelper.Decrypt(Request.QueryString("CaseId").Replace("!Plus!", "+").Replace("!Hash!", "#").Replace("!And!", "&"))
                GetData(DsData, CaseId1, CaseId2)
                GetPref(DsPref, Session("USERID"))


                If ddlCnType.SelectedValue = "Total" Then
                    GetTotal(DsData, DsPref)
                ElseIf ddlCnType.SelectedValue = "CTotal" Then
                    GetCustomerTotal(DsData, DsPref)
                ElseIf ddlCnType.SelectedValue = "CPUnit" Then
                    GetCustomerPerUnit(DsData, DsPref)
                ElseIf ddlCnType.SelectedValue = "CPWeight" Then
                    GetCustomerPerWight(DsData, DsPref)
                    'ElseIf ddlCnType.SelectedValue = "Gasoline" Then
                    '    GetGasoline(DsData, DsPref)
                End If



            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GetData(ByRef Ds As DataTable, ByVal CaseId1 As String, ByVal CaseId2 As String)
        Try
            Dim StrSql As String = String.Empty
            Dim MyConnection As String = String.Empty
            Dim MyEConnection As String = String.Empty
            Dim odbutil As New DBUtil()

            MyConnection = System.Configuration.ConfigurationManager.AppSettings("SDistributionConnectionString")


            StrSql = "SELECT CASEID, RMERGY,  "
            StrSql = StrSql + "FROM TOTAL "
            StrSql = StrSql + "WHERE CASEID IN  (" + CaseId1 + "," + CaseId2 + ") "

            StrSql = "SELECT  DISTINCT  "
            StrSql = StrSql + "TOTAL.CASEID, "
            StrSql = StrSql + "RESULTSPL.CUSSALESVOLUME, "
            StrSql = StrSql + "RESULTSPL.CUSSALESUNIT, "
            'StrSql = StrSql + "( CASE WHEN TOTAL.CASEID <= 1000 THEN "
            'StrSql = StrSql + "( SELECT (BASECASES.CASEID||':'||BASECASES.CASEDE1||' '||BASECASES.CASEDE2 ) "
            'StrSql = StrSql + "FROM BASECASES "
            'StrSql = StrSql + "WHERE  BASECASES.CASEID = TOTAL.CASEID "
            'StrSql = StrSql + ") ELSE "
            'StrSql = StrSql + "( SELECT DISTINCT (PERMISSIONSCASES.CASEID||':'||NVL(PERMISSIONSCASES.CASEDE1,'') || ' ' || NVL(PERMISSIONSCASES.CASEDE2,'') ) "
            'StrSql = StrSql + "FROM PERMISSIONSCASES "
            'StrSql = StrSql + "WHERE  PERMISSIONSCASES.CASEID = TOTAL.CASEID "
            'StrSql = StrSql + ") "
            'StrSql = StrSql + "END  ) AS CASEDE, "
            StrSql = StrSql + "( CASE WHEN TOTAL.CASEID <= 1000 THEN "
            StrSql = StrSql + "( SELECT (BASECASES.CASEID||':'||REPLACE(REPLACE((REPLACE(NVL(BASECASES.CASEDE1,''),')','}')),'(','{')||' '||REPLACE((REPLACE(NVL(BASECASES.CASEDE2,''),')','}')),'(','{'),';',':') ) "
            StrSql = StrSql + "FROM BASECASES "
            StrSql = StrSql + "WHERE  BASECASES.CASEID = TOTAL.CASEID "
            StrSql = StrSql + ") ELSE "
            StrSql = StrSql + "( SELECT DISTINCT (PERMISSIONSCASES.CASEID||':'||REPLACE(REPLACE((REPLACE(NVL(PERMISSIONSCASES.CASEDE1,''),')','}')),'(','{') || ' ' || REPLACE((REPLACE(NVL(PERMISSIONSCASES.CASEDE2,''),')','}')),'(','{'),';',':') )  "
            StrSql = StrSql + "FROM PERMISSIONSCASES "
            StrSql = StrSql + "WHERE  PERMISSIONSCASES.CASEID = TOTAL.CASEID "
            StrSql = StrSql + ") "
            StrSql = StrSql + "END  ) AS CASEDE, "
            StrSql = StrSql + "RESULTSPL.FINVOLMSI,"
            StrSql = StrSql + "RESULTSPL.FINVOLMUNITS,"
            StrSql = StrSql + "RESULTSPL.VOLUME, "
            StrSql = StrSql + "RESULTSPL.SVOLUME, "
            StrSql = StrSql + "RESULTSPL.PVOLUSE, "
            StrSql = StrSql + "RMWATER, "
            StrSql = StrSql + "RMPACKWATER, "
            StrSql = StrSql + "RMANDPACKTRNSPTWATER, "
            StrSql = StrSql + "PROCWATER, "
            StrSql = StrSql + "DPPACKWATER, "
            StrSql = StrSql + "DPTRNSPTWATER, "
            StrSql = StrSql + "TRSPTTOCUSWATER, "
            StrSql = StrSql + "PACKGEDPRODPACKWATER, "
            StrSql = StrSql + "PPPTRNSPTWATER, "
            StrSql = StrSql + "PACKGEDPRODTRNSPTWATER, "
            StrSql = StrSql + "PROCWATERS2, "
            StrSql = StrSql + "(RMWATER+RMPACKWATER+DPPACKWATER+PACKGEDPRODPACKWATER)PURMATERIALWATER, "
            StrSql = StrSql + "(PROCWATER+PROCWATERS2)PROCESSWATER, "
            StrSql = StrSql + "(RMANDPACKTRNSPTWATER+DPTRNSPTWATER+TRSPTTOCUSWATER+PPPTRNSPTWATER+PACKGEDPRODTRNSPTWATER)TRNSPTWATER, "
            StrSql = StrSql + "(RMWATER+RMPACKWATER+RMANDPACKTRNSPTWATER+PROCWATER+DPPACKWATER+DPTRNSPTWATER+TRSPTTOCUSWATER+PROCWATERS2+PACKGEDPRODPACKWATER+PPPTRNSPTWATER+PACKGEDPRODTRNSPTWATER)TOTALENWATER, "
            StrSql = StrSql + "(CASE WHEN RESULTSPL.PVOLUSE=0 THEN RESULTSPL.VOLUME "
            StrSql = StrSql + "ELSE RESULTSPL.SVOLUME END) AS SALESVOLUMELB, "
            StrSql = StrSql + "NVL((CASE WHEN RESULTSPL.FINVOLMUNITS>0 THEN RESULTSPL.FINVOLMUNITS END),0) SALESVOLUMEUNIT "
            StrSql = StrSql + "FROM TOTAL "
            StrSql = StrSql + "INNER JOIN RESULTSPL "
            StrSql = StrSql + "ON RESULTSPL.CASEID = TOTAL.CASEID "
            StrSql = StrSql + "WHERE TOTAL.CASEID IN (" + CaseId1 + "," + CaseId2 + ") "


            Ds = odbutil.FillDataTable(StrSql, MyConnection)

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GetPref(ByRef DtsPref As DataTable, ByVal USERID As String)
        Try
            Dim StrSql As String = String.Empty
            Dim MyConnection As String = String.Empty
            Dim MyEConnection As String = String.Empty
            Dim odbutil As New DBUtil()

            MyEConnection = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")

            StrSql = "SELECT  USERID,  "
            StrSql = StrSql + "UNITS, "
            StrSql = StrSql + "CURRENCY, "
            StrSql = StrSql + "CURR, "
            StrSql = StrSql + "CONVERSIONFACTOR, "
            StrSql = StrSql + "TITLE1, "
            StrSql = StrSql + "TITLE2, "
            StrSql = StrSql + "TITLE3, "
            StrSql = StrSql + "TITLE4, "
            StrSql = StrSql + "TITLE5, "
            StrSql = StrSql + "TITLE6, "
            StrSql = StrSql + "CONVAREA, "
            StrSql = StrSql + "TITLE7, "
            StrSql = StrSql + "TITLE8, "
            StrSql = StrSql + "TITLE9, "
            StrSql = StrSql + "TITLE10, "
            StrSql = StrSql + "TITLE11, "
            StrSql = StrSql + "TITLE12, "
            StrSql = StrSql + "CONVWT, "
            StrSql = StrSql + "CONVAREA2, "
            StrSql = StrSql + "CONVTHICK, "
            StrSql = StrSql + "CONVTHICK2, "
            StrSql = StrSql + "CONVTHICK3, "
            StrSql = StrSql + "CONVGALLON "
            StrSql = StrSql + "FROM CHARTPREFERENCES "
            StrSql = StrSql + "WHERE USERID='" + USERID.ToUpper() + "' "

            DtsPref = odbutil.FillDataTable(StrSql, MyEConnection)

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GenrateChart(ByVal PcScript As String, ByVal GraphName As String, ByVal Pref As String)
        Try
            'Dim control As HtmlGenericControl = FindControl("EnergyComp")
            Dim myImage As CordaEmbedder = New CordaEmbedder()
            Dim MyEConnection As String = String.Empty
            Dim dsChartSetting As New DataTable
            Dim StrSqlChartSetting As String = String.Empty
            Dim odbutil As New DBUtil()
            Dim MyConfigConnection As String = String.Empty
            MyConfigConnection = System.Configuration.ConfigurationManager.AppSettings("ConfigurationConnectionString")


            MyEConnection = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            StrSqlChartSetting = "SELECT EXTSERVERADD, INTCOMPORTADD FROM CHARTSETTING"
            dsChartSetting = odbutil.FillDataTable(StrSqlChartSetting, MyConfigConnection)

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

    Protected Sub GetCustomerTotal(ByVal DsData As DataTable, ByVal DsPref As DataTable)
        Try

            Dim Data As String = String.Empty
            Dim Pref As String = String.Empty
            Dim pcScript As String = String.Empty
            Dim Graphtype As String = String.Empty
            Dim HoverText As String = String.Empty
            Dim GraphName As String = String.Empty
            Dim Cnt As New Integer
            Dim SalesVolumeUnit As New Decimal
            Dim customerSaleValue As Double
            Dim SALESVOLUMELB As Double
            Dim i As New Integer
            Dim Transportation As New Decimal
            Dim Process As New Decimal
            Dim Purchased As New Decimal
            Dim customerSaleUnit As Double


            GraphName = "Sargento_KraftPouchvsTray"
            Graphtype = "graph"
            Cnt = DsData.Rows.Count - 1

            'customerSaleValue = CDbl(DsData.Rows(0).Item("CUSSALESVOLUME").ToString()) * CDbl(DsPref.Rows(0).Item("Convwt"))
            customerSaleValue = 0.0
            For i = 0 To DsData.Rows.Count - 1
                If DsData.Rows(i).Item("CASEID").ToString() = Session("SDistCaseId") Then

                    If CDbl(DsData.Rows(i).Item("CUSSALESUNIT").ToString()) = 0.0 Then
                        customerSaleValue = CDbl(DsData.Rows(i).Item("CUSSALESVOLUME").ToString()) * CDbl(DsPref.Rows(0).Item("Convwt"))
                    Else
                        customerSaleValue = CDbl(DsData.Rows(i).Item("CUSSALESVOLUME").ToString())
                    End If
                    customerSaleUnit = CDbl(DsData.Rows(i).Item("CUSSALESUNIT").ToString())
                End If
            Next

            lblHeading.Text = "Customer Total Water Chart"

            pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(Transportation;Process;Purchased Materials)"
            For i = 0 To Cnt
                Transportation = 0
                Process = 0
                Purchased = 0
                SALESVOLUMELB = CDbl(DsData.Rows(i).Item("SALESVOLUMELB")) * DsPref.Rows(0).Item("CONVWT")
                If i = 0 Then
                    HoverText = DsPref.Rows(0).Item("TITLE10").ToString()
                    Pref = HoverText
                End If
                If customerSaleUnit = 0 Then
                    If CDbl(SALESVOLUMELB) > 0.0 Then
                        Transportation = FormatNumber(DsData.Rows(i).Item("TRNSPTWATER") * DsPref.Rows(0).Item("CONVGALLON") * customerSaleValue / SALESVOLUMELB, 0)
                        Process = FormatNumber(DsData.Rows(i).Item("PROCESSWATER") * DsPref.Rows(0).Item("CONVGALLON") * customerSaleValue / SALESVOLUMELB, 0)
                        Purchased = FormatNumber(DsData.Rows(i).Item("PURMATERIALWATER") * DsPref.Rows(0).Item("CONVGALLON") * customerSaleValue / SALESVOLUMELB, 0)
                    End If
                Else
                    If CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString()) > 0 Then
                        Transportation = FormatNumber(DsData.Rows(i).Item("TRNSPTWATER") * DsPref.Rows(0).Item("CONVGALLON") * customerSaleValue / CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString()), 0)
                        Process = FormatNumber(DsData.Rows(i).Item("PROCESSWATER") * DsPref.Rows(0).Item("CONVGALLON") * customerSaleValue / CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString()), 0)
                        Purchased = FormatNumber(DsData.Rows(i).Item("PURMATERIALWATER") * DsPref.Rows(0).Item("CONVGALLON") * customerSaleValue / CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString()), 0)
                    End If
                End If
                pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") + "<br/>(" + HoverText.ToString() + ");" & Transportation & ";" & Process & ";" & Purchased & ")"
                'pcScript = pcScript + "" + Graphtype + ".addHoverText(1," + (i + 1).ToString() + ",Value:" + Purchased.ToString() + ")"
                'pcScript = pcScript + "" + Graphtype + ".addHoverText(2," + (i + 1).ToString() + ",Value:" + Process.ToString() + ")"
                'pcScript = pcScript + "" + Graphtype + ".addHoverText(3," + (i + 1).ToString() + ",Value:" + Transportation.ToString() + ")"
                pcScript = pcScript + "" + Graphtype + ".addHoverText(1," + (i + 1).ToString() + ")"
                pcScript = pcScript + "" + Graphtype + ".addHoverText(2," + (i + 1).ToString() + ")"
                pcScript = pcScript + "" + Graphtype + ".addHoverText(3," + (i + 1).ToString() + ")"
            Next


            GenrateChart(pcScript, GraphName, Pref)

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GetCustomerPerUnit(ByVal DsData As DataTable, ByVal DsPref As DataTable)
        Try

            Dim Data As String = String.Empty
            Dim Text As String = String.Empty
            Dim Pref As String = String.Empty
            Dim pcScript As String = String.Empty
            Dim Graphtype As String = String.Empty
            Dim HoverText As String = String.Empty
            Dim GraphName As String = String.Empty
            Dim Cnt As New Integer
            Dim SalesVolumeUnit As New Decimal
            Dim customerSaleValue As Double
            Dim SALESVOLUMELB As Double
            Dim i As New Integer
            Dim Transportation As New Decimal
            Dim Process As New Decimal
            Dim Purchased As New Decimal
            Dim customerSaleUnit As Double
            Dim perUnit As Double
            Dim lbToUnit As Double

            GraphName = "Sargento_KraftPouchvsTray"
            Graphtype = "graph"
            Cnt = DsData.Rows.Count - 1

            'customerSaleValue = CDbl(DsData.Rows(0).Item("CUSSALESVOLUME").ToString()) * CDbl(DsPref.Rows(0).Item("Convwt"))
            customerSaleValue = 0.0
            For i = 0 To DsData.Rows.Count - 1
                If DsData.Rows(i).Item("CASEID").ToString() = Session("SDistCaseId") Then

                    If CDbl(DsData.Rows(i).Item("CUSSALESUNIT").ToString()) = 0.0 Then
                        customerSaleValue = CDbl(DsData.Rows(i).Item("CUSSALESVOLUME").ToString()) * CDbl(DsPref.Rows(0).Item("Convwt"))
                    Else
                        customerSaleValue = CDbl(DsData.Rows(i).Item("CUSSALESVOLUME").ToString())
                    End If
                    customerSaleUnit = CDbl(DsData.Rows(i).Item("CUSSALESUNIT").ToString())
                End If
            Next

            lblHeading.Text = "Customer Total Water Chart"

            pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(Transportation;Process;Purchased Materials)"
            For i = 0 To Cnt
                Transportation = 0
                Process = 0
                Purchased = 0
                SALESVOLUMELB = CDbl(DsData.Rows(i).Item("SALESVOLUMELB")) * DsPref.Rows(0).Item("CONVWT")
                If CDbl(DsData.Rows(i).Item("SALESVOLUMELB").ToString()) > 0 Then
                    lbToUnit = CDbl(DsData.Rows(i).Item("SALESVOLUMEUNIT").ToString()) / SALESVOLUMELB
                End If
                If i = 0 Then
                    HoverText = DsPref.Rows(0).Item("TITLE10").ToString() + " water/Unit"
                    Pref = HoverText
                End If
                If customerSaleUnit = 0 Then
                    If CDbl(SALESVOLUMELB) > 0.0 Then
                        If CDbl(customerSaleValue * lbToUnit) <> 0 Then
                            Transportation = FormatNumber((DsData.Rows(i).Item("TRNSPTWATER") * DsPref.Rows(0).Item("CONVGALLON") * customerSaleValue / SALESVOLUMELB) / CDbl(customerSaleValue * lbToUnit), 3)
                            Process = FormatNumber((DsData.Rows(i).Item("PROCESSWATER") * DsPref.Rows(0).Item("CONVGALLON") * customerSaleValue / SALESVOLUMELB) / CDbl(customerSaleValue * lbToUnit), 3)
                            Purchased = FormatNumber((DsData.Rows(i).Item("PURMATERIALWATER") * DsPref.Rows(0).Item("CONVGALLON") * customerSaleValue / SALESVOLUMELB) / CDbl(customerSaleValue * lbToUnit), 3)
                        End If
                    End If
                Else
                    If CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString()) > 0 Then
                        If CDbl(customerSaleValue) <> 0 Then
                            Transportation = FormatNumber((DsData.Rows(i).Item("TRNSPTWATER") * DsPref.Rows(0).Item("CONVGALLON") * customerSaleValue / CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString())) / CDbl(customerSaleValue), 3)
                            Process = FormatNumber((DsData.Rows(i).Item("PROCESSWATER") * DsPref.Rows(0).Item("CONVGALLON") * customerSaleValue / CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString())) / CDbl(customerSaleValue), 3)
                            Purchased = FormatNumber((DsData.Rows(i).Item("PURMATERIALWATER") * DsPref.Rows(0).Item("CONVGALLON") * customerSaleValue / CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString())) / CDbl(customerSaleValue), 3)
                        End If
                    End If
                End If
                pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") + "<br/>(" + HoverText.ToString() + ");" & Transportation & ";" & Process & ";" & Purchased & ")"
                'pcScript = pcScript + "" + Graphtype + ".addHoverText(1," + (i + 1).ToString() + ",Value:" + Purchased.ToString() + ")"
                'pcScript = pcScript + "" + Graphtype + ".addHoverText(2," + (i + 1).ToString() + ",Value:" + Process.ToString() + ")"
                'pcScript = pcScript + "" + Graphtype + ".addHoverText(3," + (i + 1).ToString() + ",Value:" + Transportation.ToString() + ")"
                pcScript = pcScript + "" + Graphtype + ".addHoverText(1," + (i + 1).ToString() + ")"
                pcScript = pcScript + "" + Graphtype + ".addHoverText(2," + (i + 1).ToString() + ")"
                pcScript = pcScript + "" + Graphtype + ".addHoverText(3," + (i + 1).ToString() + ")"
            Next


            GenrateChart(pcScript, GraphName, Pref)

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub GetCustomerPerWight(ByVal DsData As DataTable, ByVal DsPref As DataTable)
        Try

            Dim Data As String = String.Empty
            Dim Text As String = String.Empty
            Dim Pref As String = String.Empty
            Dim pcScript As String = String.Empty
            Dim Graphtype As String = String.Empty
            Dim HoverText As String = String.Empty
            Dim GraphName As String = String.Empty
            Dim Cnt As New Integer
            Dim SalesVolumeUnit As New Decimal
            Dim customerSaleValue As Double
            Dim SALESVOLUMELB As Double
            Dim i As New Integer
            Dim Transportation As New Decimal
            Dim Process As New Decimal
            Dim Purchased As New Decimal
            Dim customerSaleUnit As Double
            Dim perwt As Double
            Dim UnitTolb As Double

            GraphName = "Sargento_KraftPouchvsTray"
            Graphtype = "graph"
            Cnt = DsData.Rows.Count - 1

            'customerSaleValue = CDbl(DsData.Rows(0).Item("CUSSALESVOLUME").ToString()) * CDbl(DsPref.Rows(0).Item("Convwt"))
            customerSaleValue = 0.0
            For i = 0 To DsData.Rows.Count - 1
                If DsData.Rows(i).Item("CASEID").ToString() = Session("SDistCaseId") Then
                    If CDbl(DsData.Rows(i).Item("CUSSALESUNIT").ToString()) = 0.0 Then
                        customerSaleValue = CDbl(DsData.Rows(i).Item("CUSSALESVOLUME").ToString()) * CDbl(DsPref.Rows(0).Item("Convwt"))
                    Else
                        customerSaleValue = CDbl(DsData.Rows(i).Item("CUSSALESVOLUME").ToString())
                    End If
                    customerSaleUnit = CDbl(DsData.Rows(i).Item("CUSSALESUNIT").ToString())
                End If
            Next

            lblHeading.Text = "Customer Total Water Chart"

            pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(Transportation;Process;Purchased Materials)"
            For i = 0 To Cnt
                Transportation = 0
                Process = 0
                Purchased = 0
                SALESVOLUMELB = CDbl(DsData.Rows(i).Item("SALESVOLUMELB")) * DsPref.Rows(0).Item("CONVWT")
                If CDbl(DsData.Rows(i).Item("SALESVOLUMEUNIT")) > 0 Then
                    UnitTolb = SALESVOLUMELB / CDbl(DsData.Rows(i).Item("SALESVOLUMEUNIT"))
                End If
                If i = 0 Then
                    HoverText = DsPref.Rows(0).Item("Title10").ToString() + " water /" + DsPref.Rows(0).Item("Title8").ToString()
                    Pref = HoverText
                End If
                If customerSaleUnit = 0 Then
                    If CDbl(SALESVOLUMELB) > 0.0 Then
                        If CDbl(customerSaleValue) <> 0 Then
                            Transportation = FormatNumber((DsData.Rows(i).Item("TRNSPTWATER") * DsPref.Rows(0).Item("CONVGALLON") * customerSaleValue / SALESVOLUMELB) / CDbl(customerSaleValue), 3)
                            Process = FormatNumber((DsData.Rows(i).Item("PROCESSWATER") * DsPref.Rows(0).Item("CONVGALLON") * customerSaleValue / SALESVOLUMELB) / CDbl(customerSaleValue), 3)
                            Purchased = FormatNumber((DsData.Rows(i).Item("PURMATERIALWATER") * DsPref.Rows(0).Item("CONVGALLON") * customerSaleValue / SALESVOLUMELB) / CDbl(customerSaleValue), 3)
                        End If
                    End If
                Else
                    If CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString()) > 0 Then
                        If CDbl(customerSaleValue * UnitTolb) <> 0 Then
                            Transportation = FormatNumber((DsData.Rows(i).Item("TRNSPTWATER") * DsPref.Rows(0).Item("CONVGALLON") * customerSaleValue / CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString())) / CDbl(customerSaleValue * UnitTolb), 3)
                            Process = FormatNumber((DsData.Rows(i).Item("PROCESSWATER") * DsPref.Rows(0).Item("CONVGALLON") * customerSaleValue / CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString())) / CDbl(customerSaleValue * UnitTolb), 3)
                            Purchased = FormatNumber((DsData.Rows(i).Item("PURMATERIALWATER") * DsPref.Rows(0).Item("CONVGALLON") * customerSaleValue / CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString())) / CDbl(customerSaleValue * UnitTolb), 3)
                        End If
                    End If
                End If
                pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") + "<br/>(" + HoverText.ToString() + ");" & Transportation & ";" & Process & ";" & Purchased & ")"
                'pcScript = pcScript + "" + Graphtype + ".addHoverText(1," + (i + 1).ToString() + ",Value:" + Purchased.ToString() + ")"
                'pcScript = pcScript + "" + Graphtype + ".addHoverText(2," + (i + 1).ToString() + ",Value:" + Process.ToString() + ")"
                'pcScript = pcScript + "" + Graphtype + ".addHoverText(3," + (i + 1).ToString() + ",Value:" + Transportation.ToString() + ")"
                pcScript = pcScript + "" + Graphtype + ".addHoverText(1," + (i + 1).ToString() + ")"
                pcScript = pcScript + "" + Graphtype + ".addHoverText(2," + (i + 1).ToString() + ")"
                pcScript = pcScript + "" + Graphtype + ".addHoverText(3," + (i + 1).ToString() + ")"
            Next


            GenrateChart(pcScript, GraphName, Pref)

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub GetTotal(ByVal DsData As DataTable, ByVal DsPref As DataTable)
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
            lblHeading.Text = "Total Water Chart"

            pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(Transportation;Process;Purchased Materials)"
            For i = 0 To Cnt

                Transportation = FormatNumber(DsData.Rows(i).Item("TRNSPTWATER") * DsPref.Rows(0).Item("CONVGALLON"), 0)
                Process = FormatNumber(DsData.Rows(i).Item("PROCESSWATER") * DsPref.Rows(0).Item("CONVGALLON"), 0)
                Purchased = FormatNumber(DsData.Rows(i).Item("PURMATERIALWATER") * DsPref.Rows(0).Item("CONVGALLON"), 0)

                pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & Transportation & ";" & Process & ";" & Purchased & ")"
            Next

            GenrateChart(pcScript, GraphName, Pref)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub bynUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bynUpdate.Click
        Try

        Catch ex As Exception

        End Try
    End Sub
End Class
