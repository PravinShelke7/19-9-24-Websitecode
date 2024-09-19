Imports Corda
Imports System.Web.UI.HtmlControls
Imports System.Data
Imports System.Data.OleDb
Imports System.Web.HttpBrowserCapabilities
Partial Class Charts_SMed2Charts_CEolCharts
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
                GetData(DsData, CaseId1, CaseId2, Session("UserId"))
                GetPref(DsPref, Session("USERID"))
                'If Not IsPostBack Then
                '    bindMaterialBalanceData()
                'End If


                If ddlCnType.SelectedValue = "MatBalance" Then
                    GetMaterialBalance(DsData, DsPref)
                ElseIf ddlCnType.SelectedValue = "MatRec" Then
                    GetMaterialRecycling(DsData, DsPref)
                ElseIf ddlCnType.SelectedValue = "MatInc" Then
                    GetMaterialIncineration(DsData, DsPref)
                ElseIf ddlCnType.SelectedValue = "MatComp" Then
                    GetMaterialComposting(DsData, DsPref)
                ElseIf ddlCnType.SelectedValue = "MatLandFill" Then
                    GetMaterialLandfill(DsData, DsPref)
                End If



            End If
        Catch ex As Exception
            Response.Write("Error:Page_Load:" + ex.Message.ToString() + "")
        End Try
    End Sub

    Protected Sub GetData(ByRef Ds As DataTable, ByVal CaseId1 As String, ByVal CaseId2 As String, ByVal userId As String)
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
            StrSql = StrSql + "MATB.CASEID, "
            'StrSql = StrSql + "Total.CUSSALESVOLUME*PREF.CONVWT AS CUSSALESVOLUME, "
            StrSql = StrSql + "RSPL.CUSSALESVOLUME  AS CUSSALESVOLUME, "
            StrSql = StrSql + "RSPL.CUSSALESUNIT, "
            StrSql = StrSql + "( CASE WHEN MATB.CASEID <= 1000 THEN "
            StrSql = StrSql + "( SELECT (BASECASES.CASEID||':'||REPLACE(REPLACE((REPLACE(NVL(BASECASES.CASEDE1,''),')','}')),'(','{')||' '||REPLACE((REPLACE(NVL(BASECASES.CASEDE2,''),')','}')),'(','{'),';',':') )  "
            StrSql = StrSql + "FROM BASECASES "
            StrSql = StrSql + "WHERE  BASECASES.CASEID = MATB.CASEID "
            StrSql = StrSql + ") ELSE "
            StrSql = StrSql + "( SELECT DISTINCT (PERMISSIONSCASES.CASEID||':'||REPLACE(REPLACE((REPLACE(NVL(PERMISSIONSCASES.CASEDE1,''),')','}')),'(','{') || ' ' || REPLACE((REPLACE(NVL(PERMISSIONSCASES.CASEDE2,''),')','}')),'(','{'),';',':') )  "
            StrSql = StrSql + "FROM PERMISSIONSCASES "
            StrSql = StrSql + "WHERE  PERMISSIONSCASES.CASEID = MATB.CASEID "
            StrSql = StrSql + ") "
            StrSql = StrSql + "END  ) AS CASEDE, "
            StrSql = StrSql + "round(MATB.M1*PREF.CONVWT,28)  AS MATB1, "
            StrSql = StrSql + "round(MATB.M2*PREF.CONVWT,28)  AS MATB2, "
            StrSql = StrSql + "round(MATB.M3*PREF.CONVWT,28)  AS MATB3, "
            StrSql = StrSql + "round(MATB.M4*PREF.CONVWT,28)  AS MATB4, "
            StrSql = StrSql + "round(MATB.M5*PREF.CONVWT,28)  AS MATB5, "
            StrSql = StrSql + "round(MATB.M6*PREF.CONVWT,28)  AS MATB6, "
            StrSql = StrSql + "round(MATB.M7*PREF.CONVWT,28)  AS MATB7, "
            StrSql = StrSql + "round(MATB.M8*PREF.CONVWT,28)  AS MATB8, "
            StrSql = StrSql + "round(MATB.M9*PREF.CONVWT,28)  AS MATB9, "

            StrSql = StrSql + "round(MALOUT.MR1*PREF.CONVWT,28)  AS MATRE1, "
            StrSql = StrSql + "round(MALOUT.MR2*PREF.CONVWT,28)  AS MATRE2, "
            StrSql = StrSql + "round(MALOUT.MR3*PREF.CONVWT,28)  AS MATRE3, "
            StrSql = StrSql + "round(MALOUT.MR4*PREF.CONVWT,28)  AS MATRE4, "
            StrSql = StrSql + "round(MALOUT.MR5*PREF.CONVWT,28)  AS MATRE5, "
            StrSql = StrSql + "round(MALOUT.MR6*PREF.CONVWT,28)  AS MATRE6, "
            StrSql = StrSql + "round(MALOUT.MR7*PREF.CONVWT,28)  AS MATRE7, "
            StrSql = StrSql + "round(MALOUT.MR8*PREF.CONVWT,28)  AS MATRE8, "
            StrSql = StrSql + "round(MALOUT.MR9*PREF.CONVWT,28)  AS MATRE9, "

            StrSql = StrSql + "round(MALOUT.MI1*PREF.CONVWT,28)  AS MATIN1, "
            StrSql = StrSql + "round(MALOUT.MI2*PREF.CONVWT,28)  AS MATIN2, "
            StrSql = StrSql + "round(MALOUT.MI3*PREF.CONVWT,28)  AS MATIN3, "
            StrSql = StrSql + "round(MALOUT.MI4*PREF.CONVWT,28)  AS MATIN4, "
            StrSql = StrSql + "round(MALOUT.MI5*PREF.CONVWT,28)  AS MATIN5, "
            StrSql = StrSql + "round(MALOUT.MI6*PREF.CONVWT,28)  AS MATIN6, "
            StrSql = StrSql + "round(MALOUT.MI7*PREF.CONVWT,28)  AS MATIN7, "
            StrSql = StrSql + "round(MALOUT.MI8*PREF.CONVWT,28)  AS MATIN8, "
            StrSql = StrSql + "round(MALOUT.MI9*PREF.CONVWT,28)  AS MATIN9, "

            StrSql = StrSql + "round(MALOUT.MC1*PREF.CONVWT,28)  AS MATCM1, "
            StrSql = StrSql + "round(MALOUT.MC2*PREF.CONVWT,28)  AS MATCM2, "
            StrSql = StrSql + "round(MALOUT.MC3*PREF.CONVWT,28)  AS MATCM3, "
            StrSql = StrSql + "round(MALOUT.MC4*PREF.CONVWT,28)  AS MATCM4, "
            StrSql = StrSql + "round(MALOUT.MC5*PREF.CONVWT,28)  AS MATCM5, "
            StrSql = StrSql + "round(MALOUT.MC6*PREF.CONVWT,28)  AS MATCM6, "
            StrSql = StrSql + "round(MALOUT.MC7*PREF.CONVWT,28)  AS MATCM7, "
            StrSql = StrSql + "round(MALOUT.MC8*PREF.CONVWT,28)  AS MATCM8, "
            StrSql = StrSql + "round(MALOUT.MC9*PREF.CONVWT,28)  AS MATCM9, "

            StrSql = StrSql + "round(MALOUT.ML1*PREF.CONVWT,28)  AS MATLF1, "
            StrSql = StrSql + "round(MALOUT.ML2*PREF.CONVWT,28)  AS MATLF2, "
            StrSql = StrSql + "round(MALOUT.ML3*PREF.CONVWT,28)  AS MATLF3, "
            StrSql = StrSql + "round(MALOUT.ML4*PREF.CONVWT,28)  AS MATLF4, "
            StrSql = StrSql + "round(MALOUT.ML5*PREF.CONVWT,28)  AS MATLF5, "
            StrSql = StrSql + "round(MALOUT.ML6*PREF.CONVWT,28)  AS MATLF6, "
            StrSql = StrSql + "round(MALOUT.ML7*PREF.CONVWT,28)  AS MATLF7, "
            StrSql = StrSql + "round(MALOUT.ML8*PREF.CONVWT,28)  AS MATLF8, "
            StrSql = StrSql + "round(MALOUT.ML9*PREF.CONVWT,28)  AS MATLF9, "

            StrSql = StrSql + "(CASE WHEN RSPL.PVOLUSE=0 THEN RSPL.VOLUME*PREF.CONVWT "
            StrSql = StrSql + "ELSE RSPL.SVOLUME*PREF.CONVWT END) AS SALESVOLUMELB, "
            StrSql = StrSql + "NVL((CASE WHEN RSPL.FINVOLMUNITS>0 THEN RSPL.FINVOLMUNITS END),0) SALESVOLUMEUNIT "
            StrSql = StrSql + "FROM MATERIALBALANCE MATB "
            StrSql = StrSql + "INNER JOIN MATERIALBALANCEWASTE MATBW "
            StrSql = StrSql + "ON MATBW.CASEID = MATB.CASEID "
            StrSql = StrSql + "INNER JOIN RESULTSPL RSPL "
            StrSql = StrSql + "ON RSPL.CASEID = MATB.CASEID "
            StrSql = StrSql + "Inner Join Total "
            StrSql = StrSql + "on Total.caseId=MATB.CASEID "
            StrSql = StrSql + "INNER JOIN MATENDOFLIFEOUT MALOUT "
            StrSql = StrSql + "ON MALOUT.CASEID = MATB.CASEID "
            StrSql = StrSql + "Inner Join ECON.CHARTPREFERENCES Pref "
            StrSql = StrSql + "on Upper(Pref.USERID)=" + userId + " "
            StrSql = StrSql + "WHERE MATB.CASEID IN (" + CaseId1 + "," + CaseId2 + ") "

            Ds = odbutil.FillDataTable(StrSql, MyConnection)

        Catch ex As Exception
            Response.Write("Error:GetData:" + ex.Message.ToString() + "")
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
            Response.Write("Error:GetPref:" + ex.Message.ToString() + "")
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
            Response.Write("Error:GenrateChart:" + ex.Message.ToString() + "")
        End Try
    End Sub

    Protected Sub GetMaterialLandfill(ByVal DsData As DataTable, ByVal DsPref As DataTable)
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
            customerSaleValue = 0.0
            Cnt = DsData.Rows.Count - 1
            Pref = DsPref.Rows(0).Item("Title8").ToString()
            lblHeading.Text = ddltype.SelectedItem.Text + " Chart"
            pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"

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

            For i = 0 To Cnt
                Data = "0"
                SALESVOLUMELB = CDbl(DsData.Rows(i).Item("SALESVOLUMELB"))

                If ddltype.SelectedItem.Value = "1" Or ddltype.SelectedItem.Value = "4" Or ddltype.SelectedItem.Value = "6" Then
                    Data = "0"
                Else
                    If customerSaleUnit = 0 Then
                        If CDbl(SALESVOLUMELB) > 0.0 Then
                            Data = FormatNumber(CDbl(DsData.Rows(i).Item("MATLF" + ddltype.SelectedItem.Value + "") * customerSaleValue / SALESVOLUMELB), 0)
                        End If
                    Else
                        If CDbl(DsData.Rows(i).Item("SALESVOLUMEUNIT").ToString()) > 0 Then
                            Data = FormatNumber(CDbl(DsData.Rows(i).Item("MATLF" + ddltype.SelectedItem.Value + "") * customerSaleValue / CDbl(DsData.Rows(i).Item("SALESVOLUMEUNIT").ToString())), 0)
                        End If
                    End If
                End If
                pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") + Pref + ";" & Data & ")"
            Next
            GenrateChart(pcScript, GraphName, Pref)
        Catch ex As Exception
            Response.Write("Error:GetMaterialLandfill:" + ex.Message.ToString() + "")
        End Try
    End Sub

    Protected Sub GetMaterialComposting(ByVal DsData As DataTable, ByVal DsPref As DataTable)
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
            customerSaleValue = 0.0
            Cnt = DsData.Rows.Count - 1
            Pref = DsPref.Rows(0).Item("Title8").ToString()
            lblHeading.Text = ddltype.SelectedItem.Text + " Chart"
            pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
            ' customerSaleValue = FormatNumber(CDbl(DsData.Rows(0).Item("CUSSALESVOLUME").ToString()), 0)
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

            For i = 0 To Cnt
                Data = "0"
                SALESVOLUMELB = CDbl(DsData.Rows(i).Item("SALESVOLUMELB"))

                If ddltype.SelectedItem.Value = "1" Or ddltype.SelectedItem.Value = "4" Or ddltype.SelectedItem.Value = "6" Then
                    Data = "0"
                Else
                    If customerSaleUnit = 0 Then
                        If CDbl(SALESVOLUMELB) > 0.0 Then
                            Data = FormatNumber(CDbl(DsData.Rows(i).Item("MATCM" + ddltype.SelectedItem.Value + "") * customerSaleValue / SALESVOLUMELB), 0)
                        End If
                    Else
                        If CDbl(DsData.Rows(i).Item("SALESVOLUMEUNIT").ToString()) > 0 Then
                            Data = FormatNumber(CDbl(DsData.Rows(i).Item("MATCM" + ddltype.SelectedItem.Value + "") * customerSaleValue / CDbl(DsData.Rows(i).Item("SALESVOLUMEUNIT").ToString())), 0)
                        End If
                    End If
                End If
                pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") + Pref + ";" & Data & ")"
            Next
            GenrateChart(pcScript, GraphName, Pref)
        Catch ex As Exception
            Response.Write("Error:GetMaterialComposting:" + ex.Message.ToString() + "")
        End Try
    End Sub

    Protected Sub GetMaterialIncineration(ByVal DsData As DataTable, ByVal DsPref As DataTable)
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
            customerSaleValue = 0.0
            Cnt = DsData.Rows.Count - 1
            Pref = DsPref.Rows(0).Item("Title8").ToString()
            lblHeading.Text = ddltype.SelectedItem.Text + " Chart"
            pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
            ' customerSaleValue = FormatNumber(CDbl(DsData.Rows(0).Item("CUSSALESVOLUME").ToString()), 0)
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

            For i = 0 To Cnt
                Data = "0"
                SALESVOLUMELB = CDbl(DsData.Rows(i).Item("SALESVOLUMELB"))

                If ddltype.SelectedItem.Value = "1" Or ddltype.SelectedItem.Value = "4" Or ddltype.SelectedItem.Value = "6" Then
                    Data = "0"
                Else
                    If customerSaleUnit = 0 Then
                        If CDbl(SALESVOLUMELB) > 0.0 Then
                            Data = FormatNumber(CDbl(DsData.Rows(i).Item("MATIN" + ddltype.SelectedItem.Value + "") * customerSaleValue / SALESVOLUMELB), 0)
                        End If
                    Else
                        If CDbl(DsData.Rows(i).Item("SALESVOLUMEUNIT").ToString()) > 0 Then
                            Data = FormatNumber(CDbl(DsData.Rows(i).Item("MATIN" + ddltype.SelectedItem.Value + "") * customerSaleValue / CDbl(DsData.Rows(i).Item("SALESVOLUMEUNIT").ToString())), 0)
                        End If
                    End If
                End If
                pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") + Pref + ";" & Data & ")"
            Next

            GenrateChart(pcScript, GraphName, Pref)
        Catch ex As Exception
            Response.Write("Error:GetMaterialIncineration:" + ex.Message.ToString() + "")
        End Try
    End Sub

    Protected Sub GetMaterialRecycling(ByVal DsData As DataTable, ByVal DsPref As DataTable)
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
            customerSaleValue = 0.0
            Cnt = DsData.Rows.Count - 1
            Pref = DsPref.Rows(0).Item("Title8").ToString()
            lblHeading.Text = ddltype.SelectedItem.Text + " Chart"
            pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"

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
            'customerSaleValue = FormatNumber(CDbl(DsData.Rows(0).Item("CUSSALESVOLUME").ToString()), 0)

            For i = 0 To Cnt
                Data = "0"
                SALESVOLUMELB = CDbl(DsData.Rows(i).Item("SALESVOLUMELB"))

                If ddltype.SelectedItem.Value = "1" Or ddltype.SelectedItem.Value = "4" Or ddltype.SelectedItem.Value = "6" Then
                    Data = "0"
                Else
                    If customerSaleUnit = 0 Then
                        If CDbl(SALESVOLUMELB) > 0.0 Then
                            Data = FormatNumber(CDbl(DsData.Rows(i).Item("MATRE" + ddltype.SelectedItem.Value + "") * customerSaleValue / SALESVOLUMELB), 0)
                        End If
                    Else
                        If CDbl(DsData.Rows(i).Item("SALESVOLUMEUNIT").ToString()) > 0 Then
                            Data = FormatNumber(CDbl(DsData.Rows(i).Item("MATRE" + ddltype.SelectedItem.Value + "") * customerSaleValue / CDbl(DsData.Rows(i).Item("SALESVOLUMEUNIT").ToString())), 0)
                        End If
                    End If
                End If
                pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") + Pref + ";" & Data & ")"
            Next




            GenrateChart(pcScript, GraphName, Pref)
        Catch ex As Exception
            Response.Write("Error:GetMaterialRecycling:" + ex.Message.ToString() + "")
        End Try
    End Sub

    Protected Sub GetMaterialBalance(ByVal DsData As DataTable, ByVal DsPref As DataTable)
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
            customerSaleValue = 0.0
            Cnt = DsData.Rows.Count - 1
            Pref = DsPref.Rows(0).Item("Title8").ToString()
            lblHeading.Text = ddltype.SelectedItem.Text + " Chart"
            pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"

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


            For i = 0 To Cnt
                Data = "0"
                SALESVOLUMELB = CDbl(DsData.Rows(i).Item("SALESVOLUMELB"))
                If customerSaleUnit = 0 Then
                    If CDbl(SALESVOLUMELB) > 0.0 Then
                        Data = FormatNumber(CDbl(DsData.Rows(i).Item("MATB" + ddltype.SelectedItem.Value + "") * customerSaleValue / SALESVOLUMELB), 0)
                    End If
                Else
                    If CDbl(DsData.Rows(i).Item("SALESVOLUMEUNIT").ToString()) > 0 Then
                        Data = FormatNumber(CDbl(DsData.Rows(i).Item("MATB" + ddltype.SelectedItem.Value + "") * customerSaleValue / CDbl(DsData.Rows(i).Item("SALESVOLUMEUNIT").ToString())), 0)
                    End If
                End If
                pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") + Pref + ";" & Data & ")"
            Next




            GenrateChart(pcScript, GraphName, Pref)
        Catch ex As Exception
            Response.Write("Error:GetMaterialBalance:" + ex.Message.ToString() + "")
        End Try
    End Sub

    Protected Sub bynUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bynUpdate.Click
        Try

        Catch ex As Exception

        End Try
    End Sub


    Public Sub bindMaterialBalanceData()
        Dim i As Integer
        If ddltype.Items.Count > 0 Then
            For i = 0 To ddltype.Items.Count - 1
                ddltype.Items.RemoveAt(i)
            Next

        End If

        ddltype.Items.Insert(0, New ListItem("Product-Raw Materials", "MATB1"))
        ddltype.Items.Insert(1, New ListItem("product-Production Waste", "MATB2"))
        ddltype.Items.Insert(2, New ListItem("Product-Finished Product", "MATB3"))
        ddltype.Items.Insert(3, New ListItem("Raw Material Packaging-Total Flow", "MATB4"))

        ddltype.Items.Insert(4, New ListItem("Raw Material Packaging-Waste", "MATB5"))
        ddltype.Items.Insert(5, New ListItem("Product Packaging-Total Flow", "MATB6"))
        ddltype.Items.Insert(6, New ListItem("Product Packaging-Waste", "MATB7"))
        ddltype.Items.Insert(7, New ListItem("Other Waste-Plant", "MATB8"))
        ddltype.Items.Insert(8, New ListItem("Other Waste-Office", "MATB9"))
        ddltype.Items.Insert(9, New ListItem("End Of life Energy-Finished Product(MJ)", "MATB10"))
        ddltype.Items.Insert(10, New ListItem("End Of life Energy-Production Waste (MJ)", "MATB11"))
        ddltype.Items.Insert(11, New ListItem("End Of life Energy-Raw Material Packaging Waste (MJ)", "MATB12"))
        ddltype.Items.Insert(12, New ListItem("End Of life Energy-Product Packaging Waste (MJ)", "MATB13"))
        ddltype.Items.Insert(13, New ListItem("End Of life Energy-Plant Waste (MJ)", "MATB14"))
        ddltype.Items.Insert(14, New ListItem("End Of life Energy-Office Waste (MJ)", "MATB15"))
        ddltype.Items.Insert(15, New ListItem("End Of life Energy-Total End Of life Energy (MJ)", "MATB16"))
        ddltype.Items.Insert(16, New ListItem("End Of Life GHG -Finished Product (kg)", "MATB17"))
        ddltype.Items.Insert(17, New ListItem("End Of Life GHG-Production Waste (kg)", "MATB18"))
        ddltype.Items.Insert(18, New ListItem("End Of Life GHG-Raw Material Packaging Waste (kg)", "MATB19"))

        ddltype.Items.Insert(19, New ListItem("End Of Life GHG-Product Packaging Waste (kg)", "MATB20"))
        ddltype.Items.Insert(20, New ListItem("End Of Life GHG-Plant Waste (kg)", "MATB21"))
        ddltype.Items.Insert(21, New ListItem("End Of Life GHG-Office Waste (kg)", "MATB22"))
        ddltype.Items.Insert(22, New ListItem("End Of Life GHG-Total End Of Life GHG (kg)", "MATB23"))



    End Sub
    Public Sub bindMaterialRecycleData()
        Dim i As Integer
        If ddltype.Items.Count > 0 Then
            For i = 0 To ddltype.Items.Count - 1
                ddltype.Items.RemoveAt(i)
            Next

        End If

        ddltype.Items.Insert(0, New ListItem("product-Production Waste", "MATRE2"))
        ddltype.Items.Insert(1, New ListItem("Product-Finished Product", "MATRE3"))
        ddltype.Items.Insert(3, New ListItem("Raw Material Packaging-Total Flow", "MATB4"))

        ddltype.Items.Insert(4, New ListItem("Raw Material Packaging-Waste", "MATB5"))
        ddltype.Items.Insert(5, New ListItem("Product Packaging-Total Flow", "MATB6"))
        ddltype.Items.Insert(6, New ListItem("Product Packaging-Waste", "MATB7"))
        ddltype.Items.Insert(7, New ListItem("Other Waste-Plant", "MATB8"))
        ddltype.Items.Insert(8, New ListItem("Other Waste-Office", "MATB9"))
        ddltype.Items.Insert(9, New ListItem("End Of life Energy-Finished Product(MJ)", "MATB10"))
        ddltype.Items.Insert(10, New ListItem("End Of life Energy-Production Waste (MJ)", "MATB11"))
        ddltype.Items.Insert(11, New ListItem("End Of life Energy-Raw Material Packaging Waste (MJ)", "MATB12"))
        ddltype.Items.Insert(12, New ListItem("End Of life Energy-Product Packaging Waste (MJ)", "MATB13"))
        ddltype.Items.Insert(13, New ListItem("End Of life Energy-Plant Waste (MJ)", "MATB14"))
        ddltype.Items.Insert(14, New ListItem("End Of life Energy-Office Waste (MJ)", "MATB15"))
        ddltype.Items.Insert(15, New ListItem("End Of life Energy-Total End Of life Energy (MJ)", "MATB16"))
        ddltype.Items.Insert(16, New ListItem("End Of Life GHG -Finished Product (kg)", "MATB17"))
        ddltype.Items.Insert(17, New ListItem("End Of Life GHG-Production Waste (kg)", "MATB18"))
        ddltype.Items.Insert(18, New ListItem("End Of Life GHG-Raw Material Packaging Waste (kg)", "MATB19"))

        ddltype.Items.Insert(19, New ListItem("End Of Life GHG-Product Packaging Waste (kg)", "MATB20"))
        ddltype.Items.Insert(20, New ListItem("End Of Life GHG-Plant Waste (kg)", "MATB21"))
        ddltype.Items.Insert(21, New ListItem("End Of Life GHG-Office Waste (kg)", "MATB22"))
        ddltype.Items.Insert(22, New ListItem("End Of Life GHG-Total End Of Life GHG (kg)", "MATB23"))



    End Sub
End Class
