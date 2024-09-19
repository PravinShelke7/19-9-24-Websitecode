Imports Corda
Imports System.Web.UI.HtmlControls
Imports System.Data
Imports System.Data.OleDb
Imports System.Web.HttpBrowserCapabilities
Partial Class Charts_S2Charts_CEnergyCharts2
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
                Dim CaseId1 As String = Session("S2CaseId")
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
                ElseIf ddlCnType.SelectedValue = "Electricity" Then
                    GetElectricity(DsData, DsPref)
                End If



            End If
        Catch ex As Exception
            Response.Write("Error:Page_Load:" + ex.Message.ToString() + "")
        End Try
    End Sub

    Protected Sub GetData(ByRef Ds As DataTable, ByVal CaseId1 As String, ByVal CaseId2 As String)
        Try
            Dim StrSql As String = String.Empty
            Dim MyConnection As String = String.Empty
            Dim MyEConnection As String = String.Empty
            Dim odbutil As New DBUtil()

            MyConnection = System.Configuration.ConfigurationManager.AppSettings("Sustain2ConnectionString")


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
            StrSql = StrSql + "RMERGY, "
            StrSql = StrSql + "RMPACKERGY, "
            StrSql = StrSql + "RMANDPACKTRNSPTERGY, "
            StrSql = StrSql + "PROCERGY, "
            StrSql = StrSql + "PROCERGYS2, "
            StrSql = StrSql + "DPPACKERGY, "
            StrSql = StrSql + "DPTRNSPTERGY, "
            StrSql = StrSql + "TRSPTTOCUS, "
            StrSql = StrSql + "PACKGEDPRODPACK, "
            StrSql = StrSql + "PPPTRNSPT, "
            StrSql = StrSql + "PACKGEDPRODTRNSPT, "
            StrSql = StrSql + "(RMERGY+RMPACKERGY+DPPACKERGY+PACKGEDPRODPACK)PURMATERIALERGY, "
            StrSql = StrSql + "(PROCERGY+PROCERGYS2)TPROCERGY, "
            StrSql = StrSql + "(RMANDPACKTRNSPTERGY+DPTRNSPTERGY+TRSPTTOCUS+PPPTRNSPT+PACKGEDPRODTRNSPT)TRNSPTERGY, "
            StrSql = StrSql + "(RMERGY+RMPACKERGY+RMANDPACKTRNSPTERGY+PROCERGY+DPPACKERGY+DPTRNSPTERGY+TRSPTTOCUS+PROCERGYS2+PACKGEDPRODPACK+PPPTRNSPT+PACKGEDPRODTRNSPT)TOTALENERGY, "
            StrSql = StrSql + "(CASE WHEN PLANTENERGY.ELECPRICE <> 0 THEN "
            StrSql = StrSql + "PLANTENERGY.ELECPRICE "
            StrSql = StrSql + "ELSE "
            StrSql = StrSql + "ELEC.PRICE "
            StrSql = StrSql + "END)ELECP, "
            StrSql = StrSql + "(SELECT MJPKWH FROM Econ.CONVFACTORS2) AS MJPKWH, "
            StrSql = StrSql + "(CASE WHEN RESULTSPL.PVOLUSE=0 THEN RESULTSPL.VOLUME "
            StrSql = StrSql + "ELSE RESULTSPL.SVOLUME END) AS SALESVOLUMELB, "
            StrSql = StrSql + "NVL((CASE WHEN RESULTSPL.FINVOLMUNITS>0 THEN RESULTSPL.FINVOLMUNITS END),0) SALESVOLUMEUNIT "
            StrSql = StrSql + "FROM TOTAL "
            StrSql = StrSql + "INNER JOIN RESULTSPL "
            StrSql = StrSql + "ON RESULTSPL.CASEID = TOTAL.CASEID "
            StrSql = StrSql + "Inner Join PLANTENERGY "
            StrSql = StrSql + "ON PLANTENERGY.Caseid= TOTAL.CASEID "
            StrSql = StrSql + "INNER JOIN SUSTAIN.ENERGY ELEC "
            StrSql = StrSql + "ON ELEC.ENERGYID = 1 "
            StrSql = StrSql + "WHERE TOTAL.CASEID IN (" + CaseId1 + "," + CaseId2 + ") "


            Ds = odbutil.FillDataTable(StrSql, MyConnection)

        Catch ex As Exception
            Response.Write("Error:GetData:" + ex.Message.ToString() + "")
        End Try
    End Sub

    Protected Sub GetPref(ByRef DtsPref As DataTable, ByVal UserId As String)
        Try
            Dim StrSql As String = String.Empty
            Dim MyConnection As String = String.Empty
            Dim MyEConnection As String = String.Empty
            Dim odbutil As New DBUtil()

            MyEConnection = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")

            StrSql = "SELECT  USERS.USERNAME,  "
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
            StrSql = StrSql + "FROM CHARTPREFERENCES "
            StrSql = StrSql + "INNER JOIN ECON.USERS ON USERS.USERID= CHARTPREFERENCES.USERID "
            StrSql = StrSql + "WHERE CHARTPREFERENCES.USERID=" + UserId.ToString() + " "

            DtsPref = odbutil.FillDataTable(StrSql, MyEConnection)

        Catch ex As Exception
            Response.Write("Error:GetPref:" + ex.Message.ToString() + "")
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
            Response.Write("Error:GenrateChart:" + ex.Message.ToString() + "")
        End Try
    End Sub

    Protected Sub GetCustomerTotal(ByVal DsData As DataTable, ByVal DsPref As DataTable)
        Try
            Dim Data As String = String.Empty
            Dim Text As String = String.Empty
            Dim Pref As String = String.Empty
            Dim pcScript As String = String.Empty
            Dim Graphtype As String = String.Empty
            Dim GraphName As String = String.Empty
            Dim Cnt As New Integer
            Dim customerSaleValue As Double
            Dim SALESVOLUMELB As Double
            Dim i As New Integer
            Dim customerSaleUnit As Double
            GraphName = "Sargento_KraftPouchvsTray"
            Graphtype = "graph"
            Cnt = DsData.Rows.Count - 1
            Pref = "(MJ)"
            lblHeading.Text = ddltype.SelectedItem.Text + " Customer Energy Chart"
            pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
            'customerSaleValue = FormatNumber(CDbl(DsData.Rows(0).Item("CUSSALESVOLUME").ToString()) * CDbl(DsPref.Rows(0).Item("Convwt")), 0)
            customerSaleValue = 0.0
            For i = 0 To DsData.Rows.Count - 1
                If DsData.Rows(i).Item("CASEID").ToString() = Session("S2CaseId") Then
                    If CDbl(DsData.Rows(i).Item("CUSSALESUNIT").ToString()) = 0.0 Then
                        customerSaleValue = CDbl(DsData.Rows(i).Item("CUSSALESVOLUME").ToString()) * CDbl(DsPref.Rows(0).Item("Convwt"))
                    Else
                        customerSaleValue = CDbl(DsData.Rows(i).Item("CUSSALESVOLUME").ToString())
                    End If
                    customerSaleUnit = CDbl(DsData.Rows(i).Item("CUSSALESUNIT").ToString())
                End If
            Next

            For i = 0 To Cnt
                SALESVOLUMELB = CDbl(DsData.Rows(i).Item("SALESVOLUMELB")) * DsPref.Rows(0).Item("CONVWT")
                Data = "0"
                If customerSaleUnit = 0 Then
                    If CDbl(SALESVOLUMELB) > 0.0 Then
                        Data = FormatNumber(CDbl(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * customerSaleValue / SALESVOLUMELB), 0)
                    End If
                Else
                    If CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString()) > 0 Then
                        Data = FormatNumber(CDbl(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * customerSaleValue / CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString())), 0)
                    End If

                End If
                pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") + "(MJ);" & Data & ")"

            Next




            GenrateChart(pcScript, GraphName, Pref)
        Catch ex As Exception
            Response.Write("Error:GetPerWeight:" + ex.Message.ToString() + "")
        End Try
    End Sub
    Protected Sub GetCustomerPerUnit(ByVal DsData As DataTable, ByVal DsPref As DataTable)
        Try
            Dim Data As String = String.Empty
            Dim Text As String = String.Empty
            Dim Pref As String = String.Empty
            Dim pcScript As String = String.Empty
            Dim Graphtype As String = String.Empty
            Dim GraphName As String = String.Empty
            Dim Cnt As New Integer
            Dim customerSaleValue As Double
            Dim SALESVOLUMELB As Double
            Dim i As New Integer
            Dim customerSaleUnit As Double

            GraphName = "Sargento_KraftPouchvsTray"
            Graphtype = "graph"
            Cnt = DsData.Rows.Count - 1
            Pref = "MJ / Units"
            lblHeading.Text = ddltype.SelectedItem.Text + " Customer Energy Chart"
            pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
            'customerSaleValue = FormatNumber(CDbl(DsData.Rows(0).Item("CUSSALESVOLUME").ToString()) * CDbl(DsPref.Rows(0).Item("Convwt")), 0)
            customerSaleValue = 0.0
            For i = 0 To DsData.Rows.Count - 1
                If DsData.Rows(i).Item("CASEID").ToString() = Session("S2CaseId") Then
                    customerSaleUnit = CDbl(DsData.Rows(i).Item("CUSSALESUNIT").ToString())
                    If customerSaleUnit = 0 Then
                        customerSaleValue = CDbl(DsData.Rows(i).Item("CUSSALESVOLUME").ToString()) * CDbl(DsPref.Rows(0).Item("Convwt"))
                    Else
                        customerSaleValue = CDbl(DsData.Rows(i).Item("CUSSALESVOLUME").ToString())
                    End If
                End If
            Next

            Dim perunit As Double
            Dim lbToUnit As Double
            For i = 0 To Cnt
                SALESVOLUMELB = CDbl(DsData.Rows(i).Item("SALESVOLUMELB")) * DsPref.Rows(0).Item("CONVWT")
                If CDbl(DsData.Rows(i).Item("SALESVOLUMELB").ToString()) > 0 Then
                    lbToUnit = CDbl(DsData.Rows(i).Item("SALESVOLUMEUNIT").ToString()) / SALESVOLUMELB
                End If
                Data = "0"
                perunit = 0
                If customerSaleUnit = 0 Then
                    If CDbl(SALESVOLUMELB) > 0.0 Then
                        perunit = FormatNumber(CDbl(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * customerSaleValue / SALESVOLUMELB), 0)
                    End If
                Else
                    If CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString()) > 0 Then
                        perunit = FormatNumber(CDbl(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * customerSaleValue / CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString())), 0)
                    End If
                End If
                If customerSaleUnit = 0 Then
                    If CDbl(customerSaleValue * lbToUnit) <> 0 Then
                        Data = FormatNumber(CDbl(perunit) / CDbl(customerSaleValue * lbToUnit), 3)
                    End If

                Else
                    If CDbl(customerSaleValue) <> 0 Then
                        Data = FormatNumber(CDbl(perunit) / CDbl(customerSaleValue), 3)
                    End If
                End If
                pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") + "(MJ);" & Data & ")"
            Next




            GenrateChart(pcScript, GraphName, Pref)
        Catch ex As Exception
            Response.Write("Error:GetPerWeight:" + ex.Message.ToString() + "")
        End Try
    End Sub
    Protected Sub GetCustomerPerWight(ByVal DsData As DataTable, ByVal DsPref As DataTable)
        Try
            Dim Data As String = String.Empty
            Dim Text As String = String.Empty
            Dim Pref As String = String.Empty
            Dim pcScript As String = String.Empty
            Dim Graphtype As String = String.Empty
            Dim GraphName As String = String.Empty
            Dim Cnt As New Integer
            Dim customerSaleValue As Double
            Dim SALESVOLUMELB As Double
            Dim i As New Integer
            Dim customerSaleUnit As Double

            GraphName = "Sargento_KraftPouchvsTray"
            Graphtype = "graph"
            Cnt = DsData.Rows.Count - 1
            Pref = "MJ / " + DsPref.Rows(0).Item("Title8").ToString()
            lblHeading.Text = ddltype.SelectedItem.Text + " Customer Energy Chart"
            pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
            'customerSaleValue = FormatNumber(CDbl(DsData.Rows(0).Item("CUSSALESVOLUME").ToString()) * CDbl(DsPref.Rows(0).Item("Convwt")), 0)
            customerSaleValue = 0.0
            For i = 0 To DsData.Rows.Count - 1
                If DsData.Rows(i).Item("CASEID").ToString() = Session("S2CaseId") Then
                    customerSaleUnit = CDbl(DsData.Rows(i).Item("CUSSALESUNIT").ToString())
                    If customerSaleUnit = 0 Then
                        customerSaleValue = CDbl(DsData.Rows(i).Item("CUSSALESVOLUME").ToString()) * CDbl(DsPref.Rows(0).Item("Convwt"))
                    Else
                        customerSaleValue = CDbl(DsData.Rows(i).Item("CUSSALESVOLUME").ToString())
                    End If
                End If

            Next

            Dim perwt As Double
            Dim UnitTolb As Double
            For i = 0 To Cnt
                Data = "0"
                perwt = 0
                SALESVOLUMELB = CDbl(DsData.Rows(i).Item("SALESVOLUMELB")) * DsPref.Rows(0).Item("CONVWT")
                If CDbl(DsData.Rows(i).Item("SALESVOLUMEUNIT")) > 0 Then
                    UnitTolb = SALESVOLUMELB / CDbl(DsData.Rows(i).Item("SALESVOLUMEUNIT"))
                End If
                If customerSaleUnit = 0 Then
                    If CDbl(SALESVOLUMELB) > 0.0 Then
                        perwt = FormatNumber(CDbl(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * customerSaleValue / SALESVOLUMELB), 0)
                    End If
                Else
                    If CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString()) > 0 Then
                        perwt = FormatNumber(CDbl(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * customerSaleValue / CDbl(DsData.Rows(i).Item("FINVOLMUNITS").ToString())), 0)
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
                pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") + "(MJ);" & Data & ")"
            Next




            GenrateChart(pcScript, GraphName, Pref)
        Catch ex As Exception
            Response.Write("Error:GetPerWeight:" + ex.Message.ToString() + "")
        End Try
    End Sub
    Protected Sub GetElectricity(ByVal DsData As DataTable, ByVal DsPref As DataTable)
        Try
            Dim Data As String = String.Empty
            Dim Text As String = String.Empty
            Dim Pref As String = String.Empty
            Dim pcScript As String = String.Empty
            Dim Graphtype As String = String.Empty
            Dim GraphName As String = String.Empty
            Dim Cnt As New Integer
            Dim customerSaleValue As Double
            Dim SALESVOLUMELB As Double
            Dim i As New Integer

            GraphName = "Sargento_KraftPouchvsTray"
            Graphtype = "graph"
            Cnt = DsData.Rows.Count - 1
            Pref = "(kwh)"
            lblHeading.Text = ddltype.SelectedItem.Text + " Energy Chart"
            pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
            'customerSaleValue = FormatNumber(CDbl(DsData.Rows(0).Item("CUSSALESVOLUME").ToString()) * CDbl(DsPref.Rows(0).Item("Convwt")), 0)
            
            customerSaleValue = 0.0
            For i = 0 To DsData.Rows.Count - 1
                If DsData.Rows(i).Item("CASEID").ToString() = Session("S2CaseId") Then
                    customerSaleValue = FormatNumber(CDbl(DsData.Rows(i).Item("CUSSALESVOLUME").ToString()) * CDbl(DsPref.Rows(0).Item("Convwt")), 0)
                End If
            Next



            For i = 0 To Cnt
                SalesVolumeLb = CDbl(DsData.Rows(i).Item("SALESVOLUMELB")) * DsPref.Rows(0).Item("CONVWT")
                If CDbl(SalesVolumeLb) > 0.0 Then
                    Data = FormatNumber(CDbl(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * customerSaleValue / SALESVOLUMELB) / CDbl(DsData.Rows(i).Item("ELECP").ToString()) / CDbl(DsData.Rows(i).Item("MJPKWH").ToString()), 0)
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") + "(kwh);" & Data & ")"
                    ' pcScript = pcScript + Graphtype + ".addHoverText(1," + (i + 1).ToString() + ",Value:" + Data.ToString() + "<br/>Unit:" + HoverText.ToString() + ")"
                Else
                    Data = "0"
                    pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") + "(kwh);" & Data & ")"
                    'pcScript = pcScript + Graphtype + ".addHoverText(1," + (i + 1).ToString() + ",Value:" + Data.ToString() + "<br/>Unit:" + HoverText.ToString() + ")"

                End If

            Next




            GenrateChart(pcScript, GraphName, Pref)
        Catch ex As Exception
            Response.Write("Error:GetPerWeight:" + ex.Message.ToString() + "")
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
            Pref = "MJ"
            lblHeading.Text = ddltype.SelectedItem.Text + " Customer Energy Chart"

            pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
            For i = 0 To Cnt

                Data = FormatNumber(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + ""), 0)

                pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & Data & ")"
            Next

            GenrateChart(pcScript, GraphName, Pref)
        Catch ex As Exception
            Response.Write("Error:GetTotal:" + ex.Message.ToString() + "")
        End Try
    End Sub

    Protected Sub bynUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bynUpdate.Click
        Try

        Catch ex As Exception

        End Try
    End Sub
End Class
