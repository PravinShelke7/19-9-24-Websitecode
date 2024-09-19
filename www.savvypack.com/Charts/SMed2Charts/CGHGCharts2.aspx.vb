Imports Corda
Imports System.Web.UI.HtmlControls
Imports System.Data
Imports System.Data.OleDb
Imports System.Web.HttpBrowserCapabilities
Partial Class Charts_SMed2Charts_CGHGCharts2
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
#End Region


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("back") = "" Then
                'lblsession.Visible = True
                'Charts.Visible = False
                Dim obj As New CryptoHelper
                Response.Redirect("../../Pages/SMed2/Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            Else
                'Begin Embedder Code
                InitializeComponent()
                Dim DsData As New DataTable
                Dim DsPref As New DataTable
                Dim objCryptoHelper As New CryptoHelper()
                Dim CaseId1 As String = Session("SMed2CaseId")
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
                ElseIf ddlCnType.SelectedValue = "Gasoline" Then
                    GetGasoline(DsData, DsPref)
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

            MyConnection = System.Configuration.ConfigurationManager.AppSettings("MedSustain2ConnectionString")


            StrSql = "SELECT CASEID, RMERGY,  "
            StrSql = StrSql + "FROM TOTAL "
            StrSql = StrSql + "WHERE CASEID IN  (" + CaseId1 + "," + CaseId2 + ") "

            StrSql = "SELECT  DISTINCT  "
            StrSql = StrSql + "TOTAL.CASEID, "
            StrSql = StrSql + "RESULTSPL.CUSSALESVOLUME, "
            StrSql = StrSql + "RESULTSPL.CUSSALESUNIT, "
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
            StrSql = StrSql + "RMGRNHUSGAS, "
            StrSql = StrSql + "RMPACKGRNHUSGAS, "
            StrSql = StrSql + "RMANDPACKTRNSPTGRNHUSGAS, "
            StrSql = StrSql + "PROCGRNHUSGAS, "
            StrSql = StrSql + "DPPACKGRNHUSGAS, "
            StrSql = StrSql + "DPTRNSPTGRNHUSGAS, "
            StrSql = StrSql + "TRSPTTOCUSGRNHUSGAS, "
            StrSql = StrSql + "PACKGEDPRODPACKGRNHUSGAS, "
            StrSql = StrSql + "PPPTRNSPTGRNHUSGAS, "
            StrSql = StrSql + "PACKGEDPRODTRNSPTGRNHUSGAS, "
            StrSql = StrSql + "PROCGRNHUSGASS2, "
            StrSql = StrSql + "(RMGRNHUSGAS+RMPACKGRNHUSGAS+DPPACKGRNHUSGAS+PACKGEDPRODPACKGRNHUSGAS)PURMATERIALGRNHUSGAS, "
            StrSql = StrSql + "(PROCGRNHUSGAS+PROCGRNHUSGASS2)PROCESSGRNHUSGAS, "
            StrSql = StrSql + "(RMANDPACKTRNSPTGRNHUSGAS+DPTRNSPTGRNHUSGAS+TRSPTTOCUSGRNHUSGAS+PPPTRNSPTGRNHUSGAS+PACKGEDPRODTRNSPTGRNHUSGAS)TRNSPTGRNHUSGAS, "
            StrSql = StrSql + "(RMGRNHUSGAS+RMPACKGRNHUSGAS+RMANDPACKTRNSPTGRNHUSGAS+PROCGRNHUSGAS+DPPACKGRNHUSGAS+DPTRNSPTGRNHUSGAS+TRSPTTOCUSGRNHUSGAS+PROCGRNHUSGASS2+PACKGEDPRODPACKGRNHUSGAS+PPPTRNSPTGRNHUSGAS+PACKGEDPRODTRNSPTGRNHUSGAS)TOTALENGRNHUSGAS ,"
            StrSql = StrSql + "(CASE WHEN RESULTSPL.PVOLUSE=0 THEN RESULTSPL.VOLUME "
            StrSql = StrSql + "ELSE RESULTSPL.SVOLUME END) AS SALESVOLUMELB, "
            StrSql = StrSql + "NVL((CASE WHEN RESULTSPL.FINVOLMUNITS>0 THEN RESULTSPL.FINVOLMUNITS END),0) SALESVOLUMEUNIT "
            StrSql = StrSql + "FROM TOTAL "
            StrSql = StrSql + "INNER JOIN RESULTSPL "
            StrSql = StrSql + "ON RESULTSPL.CASEID = TOTAL.CASEID "
            StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
            StrSql = StrSql + "ON PREF.CASEID = TOTAL.CASEID "
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

            MyEConnection = System.Configuration.ConfigurationManager.AppSettings("MedEcon1ConnectionString")

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
            StrSql = StrSql + "CONVTHICK3 "
            StrSql = StrSql + "FROM ECON.CHARTPREFERENCES "
            StrSql = StrSql + "WHERE USERID='" + USERID.ToString() + "' "

            DtsPref = odbutil.FillDataTable(StrSql, MyEConnection)

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GenrateChart(ByVal PcScript As String, ByVal GraphName As String, ByVal Pref As String)
        Try
            ' Dim control As HtmlGenericControl = FindControl("EnergyComp")
            Dim myImage As CordaEmbedder = New CordaEmbedder()
            Dim MyEConnection As String = String.Empty
            Dim dsChartSetting As New DataTable
            Dim StrSqlChartSetting As String = String.Empty
            Dim odbutil As New DBUtil()
            Dim MyConfigConnection As String = String.Empty
            MyConfigConnection = System.Configuration.ConfigurationManager.AppSettings("ConfigurationConnectionString")

            MyEConnection = System.Configuration.ConfigurationManager.AppSettings("MedEcon1ConnectionString")
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
            Dim customerSaleUnit As Double

            GraphName = "Sargento_KraftPouchvsTray"
            Graphtype = "graph"
            Cnt = DsData.Rows.Count - 1
            customerSaleValue = 0.0

            For i = 0 To DsData.Rows.Count - 1
                If DsData.Rows(i).Item("CASEID").ToString() = Session("SMed2CaseId") Then
                    If CDbl(DsData.Rows(i).Item("CUSSALESUNIT").ToString()) = 0.0 Then
                        customerSaleValue = CDbl(DsData.Rows(i).Item("CUSSALESVOLUME").ToString()) * CDbl(DsPref.Rows(0).Item("Convwt"))
                    Else
                        customerSaleValue = CDbl(DsData.Rows(i).Item("CUSSALESVOLUME").ToString())
                    End If
                    customerSaleUnit = CDbl(DsData.Rows(i).Item("CUSSALESUNIT").ToString())
                End If
            Next

            lblHeading.Text = ddltype.SelectedItem.Text + " Customer GHG Chart"
            pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
            For i = 0 To Cnt
                Data = "0"
                SALESVOLUMELB = CDbl(DsData.Rows(i).Item("SALESVOLUMELB")) * DsPref.Rows(0).Item("CONVWT")
                If i = 0 Then
                    HoverText = DsPref.Rows(0).Item("TITLE8").ToString()
                    Pref = HoverText
                End If
                If customerSaleUnit = 0 Then
                    If CDbl(SALESVOLUMELB) > 0.0 Then
                        Data = FormatNumber(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * DsPref.Rows(0).Item("CONVWT") * customerSaleValue / SALESVOLUMELB, 0)
                    End If
                Else
                    If CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString()) > 0 Then
                        Data = FormatNumber(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * DsPref.Rows(0).Item("CONVWT") * customerSaleValue / CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString()), 0)
                    End If
                End If
                pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") + "(" + HoverText & ");" & Data & ")"
            Next


            GenrateChart(pcScript, GraphName, Pref)

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub GetCustomerPerUnit(ByVal DsData As DataTable, ByVal DsPref As DataTable)
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
            Dim customerSaleUnit As Double
            Dim perUint As Double
            Dim lbToUnit As Double

            GraphName = "Sargento_KraftPouchvsTray"
            Graphtype = "graph"
            Cnt = DsData.Rows.Count - 1
            customerSaleValue = 0.0

            For i = 0 To DsData.Rows.Count - 1
                If DsData.Rows(i).Item("CASEID").ToString() = Session("SMed2CaseId") Then
                    If CDbl(DsData.Rows(i).Item("CUSSALESUNIT").ToString()) = 0.0 Then
                        customerSaleValue = CDbl(DsData.Rows(i).Item("CUSSALESVOLUME").ToString()) * CDbl(DsPref.Rows(0).Item("Convwt"))
                    Else
                        customerSaleValue = CDbl(DsData.Rows(i).Item("CUSSALESVOLUME").ToString())
                    End If
                    customerSaleUnit = CDbl(DsData.Rows(i).Item("CUSSALESUNIT").ToString())
                End If
            Next

            lblHeading.Text = ddltype.SelectedItem.Text + " Customer GHG Chart"
            pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"

            For i = 0 To Cnt
                Data = "0"
                perUint = 0
                SALESVOLUMELB = CDbl(DsData.Rows(i).Item("SALESVOLUMELB")) * DsPref.Rows(0).Item("CONVWT")
                If CDbl(DsData.Rows(i).Item("SALESVOLUMELB").ToString()) > 0 Then
                    lbToUnit = CDbl(DsData.Rows(i).Item("SALESVOLUMEUNIT").ToString()) / SALESVOLUMELB
                End If
                If i = 0 Then
                    HoverText = DsPref.Rows(0).Item("TITLE8").ToString() + " gr gas/Unit"
                    Pref = HoverText
                End If
                If customerSaleUnit = 0 Then
                    If CDbl(SALESVOLUMELB) > 0.0 Then
                        perUint = DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * DsPref.Rows(0).Item("CONVWT") * customerSaleValue / SALESVOLUMELB
                    End If
                Else
                    If CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString()) > 0 Then
                        perUint = DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * DsPref.Rows(0).Item("CONVWT") * customerSaleValue / CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString())
                    End If
                End If
                If customerSaleUnit = 0 Then
                    If CDbl(customerSaleValue * lbToUnit) <> 0 Then
                        Data = FormatNumber(CDbl(perUint) / CDbl(customerSaleValue * lbToUnit), 3)
                    End If
                Else
                    If CDbl(customerSaleValue) <> 0 Then
                        Data = FormatNumber(CDbl(perUint) / CDbl(customerSaleValue), 3)
                    End If
                End If
                pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") + "(" + HoverText & ");" & Data & ")"
            Next
            GenrateChart(pcScript, GraphName, Pref)

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub GetCustomerPerWight(ByVal DsData As DataTable, ByVal DsPref As DataTable)
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
            Dim customerSaleUnit As Double
            Dim perwt As Double
            Dim UnitTolb As Double

            GraphName = "Sargento_KraftPouchvsTray"
            Graphtype = "graph"
            Cnt = DsData.Rows.Count - 1
            customerSaleValue = 0.0

            For i = 0 To DsData.Rows.Count - 1
                If DsData.Rows(i).Item("CASEID").ToString() = Session("SMed2CaseId") Then
                    customerSaleUnit = CDbl(DsData.Rows(i).Item("CUSSALESUNIT").ToString())
                    If customerSaleUnit = 0 Then
                        customerSaleValue = CDbl(DsData.Rows(i).Item("CUSSALESVOLUME").ToString()) * CDbl(DsPref.Rows(0).Item("Convwt"))
                    Else
                        customerSaleValue = CDbl(DsData.Rows(i).Item("CUSSALESVOLUME").ToString())
                    End If
                End If
            Next

            lblHeading.Text = ddltype.SelectedItem.Text + " Customer GHG Chart"
            pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
            For i = 0 To Cnt
                Data = "0"
                perwt = 0
                SALESVOLUMELB = CDbl(DsData.Rows(i).Item("SALESVOLUMELB")) * DsPref.Rows(0).Item("CONVWT")
                If CDbl(DsData.Rows(i).Item("SALESVOLUMEUNIT")) > 0 Then
                    UnitTolb = SALESVOLUMELB / CDbl(DsData.Rows(i).Item("SALESVOLUMEUNIT"))
                End If
                If i = 0 Then
                    HoverText = DsPref.Rows(0).Item("Title8").ToString() + " gr gas /" + DsPref.Rows(0).Item("Title8").ToString()
                    Pref = HoverText
                End If

                If customerSaleUnit = 0 Then
                    If CDbl(SALESVOLUMELB) > 0.0 Then
                        perwt = DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * DsPref.Rows(0).Item("CONVWT") * customerSaleValue / SALESVOLUMELB
                    End If
                Else
                    If CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString()) > 0 Then
                        perwt = DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * DsPref.Rows(0).Item("CONVWT") * customerSaleValue / CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString())
                    End If
                End If

                If customerSaleUnit <> 0 Then
                    If CDbl(customerSaleValue * UnitTolb) <> 0 Then
                        Data = FormatNumber(perwt / CDbl(customerSaleValue * UnitTolb), 3)
                    End If
                Else
                    If CDbl(customerSaleValue) <> 0 Then
                        Data = FormatNumber(perwt / CDbl(customerSaleValue), 3)
                    End If
                End If

                pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") + "(" + HoverText & ");" & Data & ")"
            Next


            GenrateChart(pcScript, GraphName, Pref)

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub GetGasoline(ByVal DsData As DataTable, ByVal DsPref As DataTable)
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

            GraphName = "Sargento_KraftPouchvsTray"
            Graphtype = "graph"
            Cnt = DsData.Rows.Count - 1

            'customerSaleValue = FormatNumber(CDbl(DsData.Rows(0).Item("CUSSALESVOLUME").ToString()) * CDbl(DsPref.Rows(0).Item("Convwt")), 0)
            customerSaleValue = 0.0
            For i = 0 To DsData.Rows.Count - 1
                If DsData.Rows(i).Item("CASEID").ToString() = Session("SMed2CaseId") Then
                    customerSaleValue = FormatNumber(CDbl(DsData.Rows(i).Item("CUSSALESVOLUME").ToString()) * CDbl(DsPref.Rows(0).Item("Convwt")), 0)
                End If
            Next

            lblHeading.Text = ddltype.SelectedItem.Text + " Customer GHG Chart"

            pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
            For i = 0 To Cnt
                SALESVOLUMELB = CDbl(DsData.Rows(i).Item("SALESVOLUMELB")) * DsPref.Rows(0).Item("CONVWT")
                If i = 0 Then
                    HoverText = DsPref.Rows(0).Item("TITLE10").ToString()
                    Pref = HoverText
                End If
                If CDbl(SALESVOLUMELB) > 0.0 Then
                    Data = FormatNumber(CDbl(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * DsPref.Rows(0).Item("CONVWT") * customerSaleValue / SALESVOLUMELB) / 22.5, 1)
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") + "(" + HoverText & ");" & Data & ")"
                    ' pcScript = pcScript + Graphtype + ".addHoverText(1," + (i + 1).ToString() + ",Value:" + Data.ToString() + "<br/>Unit:" + HoverText.ToString() + ")"
                Else
                    Data = "0"
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") + "(" + HoverText & ");" & Data & ")"
                    'pcScript = pcScript + Graphtype + ".addHoverText(1," + (i + 1).ToString() + ",Value:" + Data.ToString() + "<br/>Unit:" + HoverText.ToString() + ")"

                End If

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

            GraphName = "Sargento_KraftPouchvsTray"
            Graphtype = "graph"
            Cnt = DsData.Rows.Count - 1
            Pref = DsPref.Rows(0).Item("TITLE8").ToString()
            lblHeading.Text = ddltype.SelectedItem.Text + " Customer GHG Chart"

            pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
            For i = 0 To Cnt

                Data = FormatNumber(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * DsPref.Rows(0).Item("CONVWT"), 0)

                pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & Data & ")"
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
