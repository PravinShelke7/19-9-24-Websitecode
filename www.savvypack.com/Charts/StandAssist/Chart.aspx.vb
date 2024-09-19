Imports Corda
Imports System.Web.UI.HtmlControls
Imports System.Data
Imports System.Data.OleDb
Imports System.Web.HttpBrowserCapabilities

Public Class MaterialPrice
    Inherits System.Web.UI.Page
    Dim Cmp As String = 0
    Dim Check As String = ""
    Protected WithEvents Chart1 As System.Web.UI.HtmlControls.HtmlGenericControl

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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try


            If Session("back") = "" Then
                'lblsession.Visible = True
                'Charts.Visible = False
                Dim obj As New CryptoHelper
                Response.Redirect("../../Pages/StandAssist/Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            Else
                'Begin Embedder Code
                InitializeComponent()
                ' Dim control As HtmlGenericControl = FindControl("MaterialPrice")
                Dim pcScript = ""
                Dim odbutil As New DBUtil()
                Dim Id As String = ""
                'Id = MPC.SelectedValue.ToString()
                Dim MatID As String = ""
                Dim MatCID As String = ""
                Dim Type As String = ""
                Check = Request.Form("Chart")
                Dim Graphtype As String = ""
                Dim GraphName As String = ""
                Dim YearValue As String = ""
                Dim MaterialName1 As String = ""
                Dim MaterialName2 As String = ""
                Dim MatStr1 As New Integer
                Dim MatStr2 As New Integer
                Dim mat1 As String = ""
                Dim StrSql1 As String = ""
                Dim StrSql2 As String = ""
                Dim StrSql3 As String = ""
                Dim StrSql4 As String = ""
                Dim M As New Integer
                Dim N As New Integer
                Dim UserName As String = ""
                Dim ISADJTHICK As String = ""
                Dim dsPref As DataSet
                Dim strPref As String = ""
                Dim layer As String = ""
                UserName = Session("SBAUserName")

                Graphtype = "Material_Price"
         
              
                MatCID = Request.QueryString("MatId").ToString()
                Type = Request.QueryString("Type").ToString()
				If Type = "OTR" Then
					GraphName = "MaterialPrice11_OTR"
					'Page.Title = "OTR Chart"
                Else
                    'Page.Title = "WVTR Chart"
					GraphName = "MaterialPrice11_WVTR"
				End If
                

                Dim MyConnection As String = System.Configuration.ConfigurationManager.AppSettings("SBAConnectionString")
                Dim StrSQl As String = ""
                Dim Dt As New DataSet()
                Dim dsOW As New DataSet
                Dim Dt1 As New DataSet()
                Dim StrSqlMaterial1 As String
                Dim Dts1 As New DataTable()
                Dim Dts2 As New DataSet
                Dim Dts3 As New DataTable
                Dim dsBar As New DataSet
                Dim dvBar As New DataView
                Dim dtBar As New DataTable
                Dim strBar As String = ""
                Dim dts As New DataSet
                Dim OTRB(20) As String
                Dim WVTRB(20) As String
                Dim OtrTemp As String = ""
                Dim Count As String
                Dim Thick As String = ""
                Dim arr() As String
                Dim strB As String = ""
                Dim dsBl As New DataSet
                Dim Unit As String = ""
                Dim arrTemp(12) As String
                Dim matCnt As Integer = 0
                Dim MatIds(2) As String
                Dim Thickn(2) As String
                Dim OTR(11, 12) As String
                Dim WVTR(11, 12) As String
                Dim MatDes(2) As String
                Dim MatType As String = ""
                Dim PERCENTOTR(10) As String
                Dim LAMINATEOTR(10) As String
                Dim PERCENTWVTR(10) As String
                Dim LAMINATEWVTR(10) As String
                Dim ThickPer(2) As String
				Dim OTRBL(12, 2) As String
                Dim WVTRBL(12, 2) As String
				Dim OTRPREF(10, 1) As String
                Dim WVTRPREF(10, 1) As String
                Dim OTR1 As Double
                Dim OTR2 As Double
                Dim WVTR1 As Double
                Dim WVTR2 As Double

                StrSqlMaterial1 = "SELECT CASEID,OTRTEMP,WVTRTEMP,OTRRH,WVTRRH,TO_CHAR(EFFDATE,'mm/dd/yyyy') EFFDATE,M1,M2,M3,M4,M5,M6,M7,M8,M9,M10,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10, "
                StrSqlMaterial1 = StrSqlMaterial1 + "TYPEM1,TYPEM2,TYPEM3,TYPEM4,TYPEM5,TYPEM6,TYPEM7,TYPEM8,TYPEM9,TYPEM10 FROM MATERIALINPUT WHERE CASEID=" + Session("SBACaseId")
                dsOW = odbutil.FillDataSet(StrSqlMaterial1, MyConnection)

                If Type = "OTR" Then
                    If MatCID.Contains("imgOTR") Then
                        layer = MatCID.Replace("ctl00_StandAssistContentPlaceHolder_tabSDesigner_tabpnl1_imgOTR", "")
                        If dsOW.Tables(0).Rows(0).Item("M" + layer).ToString() <> "0" Then
                            MatID = dsOW.Tables(0).Rows(0).Item("M" + layer).ToString()
                            Thick = dsOW.Tables(0).Rows(0).Item("T" + layer).ToString()
                            
                            matCnt = 1
                            MatType = "Single"
                        Else
                            MatID = dsOW.Tables(0).Rows(0).Item("TYPEM" + layer).ToString()
                            strB = "SELECT CASEID,BCM1,BCM2,BCM3,BCM4,BCM5,BCM6,BCM7,BCM8,BCM9,BCM10,"
                            strB = strB + "BCT1,BCT2,BCT3,BCT4,BCT5,BCT6,BCT7,BCT8,BCT9,BCT10,BCOTR1,BCOTR2,BCOTR3,BCOTR4,BCOTR5,BCOTR6,BCOTR7,BCOTR8,BCOTR9,BCOTR10 "
                            strB = strB + "FROM BLENDMATINPUT WHERE CASEID=" + Session("SBACaseId") + " AND TYPEM=" + MatID
                            dsBl = odbutil.FillDataSet(strB, MyConnection)

                            MatIds(0) = dsBl.Tables(0).Rows(0).Item("BCM1").ToString()
                            MatIds(1) = dsBl.Tables(0).Rows(0).Item("BCM2").ToString()
                            Thickn(0) = dsBl.Tables(0).Rows(0).Item("BCT1").ToString()
                            Thickn(1) = dsBl.Tables(0).Rows(0).Item("BCT2").ToString()
							
							For i = 0 To 10
                                If dsBl.Tables(0).Rows(0).Item("BCOTR1").ToString() <> "" Then
                                    OTRPREF(i, 0) = dsBl.Tables(0).Rows(0).Item("BCOTR1").ToString()
                                End If
                                If dsBl.Tables(0).Rows(0).Item("BCOTR2").ToString() <> "" Then
                                    OTRPREF(i, 1) = dsBl.Tables(0).Rows(0).Item("BCOTR2").ToString()
                                End If
                            Next

                            ThickPer(0) = (CDbl(Thickn(0)) / (CDbl(Thickn(0)) + CDbl(Thickn(1)))) * 100
                            ThickPer(1) = (CDbl(Thickn(1)) / (CDbl(Thickn(0)) + CDbl(Thickn(1)))) * 100

                            matCnt = 2
                            MatType = "Blend"
                        End If
                       
                    Else
                        layer = MatCID.Replace("ctl00_StandAssistContentPlaceHolder_tabSDesigner_tabpnl1_imgBOTR", "")
                        arr = layer.Split("_")
                        MatID = dsOW.Tables(0).Rows(0).Item("TYPEM" + arr(0)).ToString()
                        strB = "SELECT CASEID,BCM1,BCM2,BCM3,BCM4,BCM5,BCM6,BCM7,BCM8,BCM9,BCM10,"
                        strB = strB + "BCT1,BCT2,BCT3,BCT4,BCT5,BCT6,BCT7,BCT8,BCT9,BCT10 FROM BLENDMATINPUT WHERE CASEID=" + Session("SBACaseId") + " AND TYPEM=" + MatID
                        dsBl = odbutil.FillDataSet(strB, MyConnection)

                        MatID = dsBl.Tables(0).Rows(0).Item("BCM" + arr(1)).ToString()
                        Thick = dsBl.Tables(0).Rows(0).Item("BCT" + arr(1)).ToString()

                        matCnt = 1
                        MatType = "Single"
                    End If
                Else
                    If MatCID.Contains("imgWVTR") Then
                        layer = MatCID.Replace("ctl00_StandAssistContentPlaceHolder_tabSDesigner_tabpnl1_imgWVTR", "")
                        If dsOW.Tables(0).Rows(0).Item("M" + layer).ToString() <> "0" Then
                            MatID = dsOW.Tables(0).Rows(0).Item("M" + layer).ToString()
                            Thick = dsOW.Tables(0).Rows(0).Item("T" + layer).ToString()
                            
                            matCnt = 1
                            MatType = "Single"
                        Else
                            MatID = dsOW.Tables(0).Rows(0).Item("TYPEM" + layer).ToString()
                            strB = "SELECT CASEID,BCM1,BCM2,BCM3,BCM4,BCM5,BCM6,BCM7,BCM8,BCM9,BCM10,"
                            strB = strB + "BCT1,BCT2,BCT3,BCT4,BCT5,BCT6,BCT7,BCT8,BCT9,BCT10,BCWVTR1,BCWVTR2,BCWVTR3,BCWVTR4,BCWVTR5,BCWVTR6,BCWVTR7,BCWVTR8,BCWVTR9,BCWVTR10 "
                            strB = strB + "FROM BLENDMATINPUT WHERE CASEID=" + Session("SBACaseId") + " AND TYPEM=" + MatID
                            dsBl = odbutil.FillDataSet(strB, MyConnection)

                            MatIds(0) = dsBl.Tables(0).Rows(0).Item("BCM1").ToString()
                            MatIds(1) = dsBl.Tables(0).Rows(0).Item("BCM2").ToString()
                            Thickn(0) = dsBl.Tables(0).Rows(0).Item("BCT1").ToString()
                            Thickn(1) = dsBl.Tables(0).Rows(0).Item("BCT2").ToString()
							
							For i = 0 To 10
                                If dsBl.Tables(0).Rows(0).Item("BCWVTR1").ToString() <> "" Then
                                    WVTRPREF(i, 0) = dsBl.Tables(0).Rows(0).Item("BCWVTR1").ToString()
                                End If
                                If dsBl.Tables(0).Rows(0).Item("BCWVTR2").ToString() <> "" Then
                                    WVTRPREF(i, 1) = dsBl.Tables(0).Rows(0).Item("BCWVTR2").ToString()
                                End If
                            Next

                            ThickPer(0) = (CDbl(Thickn(0)) / (CDbl(Thickn(0)) + CDbl(Thickn(1)))) * 100
                            ThickPer(1) = (CDbl(Thickn(1)) / (CDbl(Thickn(0)) + CDbl(Thickn(1)))) * 100

                            matCnt = 2
                            MatType = "Blend"
                        End If
                    Else
                        layer = MatCID.Replace("ctl00_StandAssistContentPlaceHolder_tabSDesigner_tabpnl1_imgBWVTR", "")
                        arr = layer.Split("_")
                        MatID = dsOW.Tables(0).Rows(0).Item("TYPEM" + arr(0)).ToString()
                        strB = "SELECT CASEID,BCM1,BCM2,BCM3,BCM4,BCM5,BCM6,BCM7,BCM8,BCM9,BCM10,"
                        strB = strB + "BCT1,BCT2,BCT3,BCT4,BCT5,BCT6,BCT7,BCT8,BCT9,BCT10 FROM BLENDMATINPUT WHERE CASEID=" + Session("SBACaseId") + " AND TYPEM=" + MatID
                        dsBl = odbutil.FillDataSet(strB, MyConnection)

                        MatID = dsBl.Tables(0).Rows(0).Item("BCM" + arr(1)).ToString()
                        Thick = dsBl.Tables(0).Rows(0).Item("BCT" + arr(1)).ToString()
                        
                        matCnt = 1
                        MatType = "Single"
                    End If
                End If

                strPref = "SELECT CASEID,UNITS,CONVAREA,TITLE15 FROM PREFERENCES WHERE CASEID=" + Session("SBACaseId")
                dsPref = odbutil.FillDataSet(strPref, MyConnection)

                If Type = "OTR" Then
                    If dsPref.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                        Unit = "(cc/ " + dsPref.Tables(0).Rows(0).Item("TITLE15").ToString() + "/day)"
                    Else
                        Unit = "(cc/100 " + dsPref.Tables(0).Rows(0).Item("TITLE15").ToString() + "/day)"
                    End If

                Else
                    If dsPref.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                        Unit = "(gm/" + dsPref.Tables(0).Rows(0).Item("TITLE15").ToString() + "/day)"
                    Else
                        Unit = "(gm/100 " + dsPref.Tables(0).Rows(0).Item("TITLE15").ToString() + "/day)"
                    End If

                End If

                Dim StrSqlMaterial As String = "Select MATERIALS.Matid,(MATERIALS.Matid||':'||MATERIALS.MatDE1||''||MATERIALS.MatDE2) as MatDe,ISADJTHICK from MATERIALS where MATID=" + MatID
                Dt = odbutil.FillDataSet(StrSqlMaterial, MyConnection)
                ISADJTHICK = Dt.Tables(0).Rows(0).Item("ISADJTHICK").ToString()
                mat.Text = Dt.Tables(0).Rows(0).Item("MATDE").ToString()

                strBar = "SELECT MATID,EFFDATE,TEMPID,RHID,OTRVALUE,WVTRVALUE FROM MATBARRIERMETRIC WHERE MATID=" + MatID + " AND EFFDATE<=TO_DATE('" + dsOW.Tables(0).Rows(0).Item("EFFDATE").ToString() + "','MM//DD/YYYY')"
                dsBar = odbutil.FillDataSet(strBar, MyConnection)
                dvBar = dsBar.Tables(0).DefaultView

                StrSqlMaterial1 = "SELECT RHID,RHVALUE FROM BARRIERH ORDER BY RHID ASC "
                Dt1 = odbutil.FillDataSet(StrSqlMaterial1, MyConnection)

                If Type = "OTR" Then
                    StrSqlMaterial1 = "SELECT TEMPID,TEMPVAL FROM BARRIERTEMP WHERE TEMPVAL=" + dsOW.Tables(0).Rows(0).Item("OTRTEMP").ToString() + " "
                    Dts1 = odbutil.FillDataTable(StrSqlMaterial1, MyConnection)
                    OtrTemp = dsOW.Tables(0).Rows(0).Item("OTRTEMP").ToString()

                    If MatType = "Single" Then
                        If Dts1.Rows.Count > 0 Then
                            StrSQl = "SELECT OTRVALUE,WVTRVALUE,MATID,EFFDATE,TEMPID,RHID "
                            StrSQl = StrSQl + "FROM MATBARRIERMETRIC WHERE MATID=" + MatID + " AND TEMPID=" + Dts1.Rows(0).Item("TEMPID").ToString() + " ORDER BY RHID ASC "
                            Dts2 = odbutil.FillDataSet(StrSQl, MyConnection)

                            For i = 0 To Dts2.Tables(0).Rows.Count - 1
								Try								
									OTRB(i) = CDbl(Dts2.Tables(0).Rows(i).Item("OTRVALUE").ToString())
									If ISADJTHICK <> "N" Then
										OTRB(i) = CDbl((OTRB(i) / Thick) * 25.4)
									End If

									If dsPref.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
										OTRB(i) = FormatNumber(OTRB(i), 3)
									Else
										OTRB(i) = FormatNumber(CDbl(OTRB(i)) * CDbl(dsPref.Tables(0).Rows(0).Item("CONVAREA").ToString() / 1000) * 100, 3)
									End If
								Catch ex As Exception
                                    OTRB(i) = "0"
                                End Try
                            Next
                        Else
                            StrSQl = "SELECT (B.TEMPID) TEMPID,A.TEMPVAL FROM  "
                            StrSQl = StrSQl + "( "
                            StrSQl = StrSQl + "SELECT TEMPVAL FROM BARRIERTEMP WHERE TEMPVAL IN( "
                            StrSQl = StrSQl + "SELECT MAX(TEMPVAL)  FROM BARRIERTEMP "
                            StrSQl = StrSQl + "WHERE(TEMPVAL < " + OtrTemp + ") "
                            StrSQl = StrSQl + "UNION ALL "
                            StrSQl = StrSQl + "SELECT MIN(TEMPVAL) FROM BARRIERTEMP "
                            StrSQl = StrSQl + "WHERE TEMPVAL >" + OtrTemp + ") "
                            StrSQl = StrSQl + ")A "
                            StrSQl = StrSQl + "INNER JOIN BARRIERTEMP B "
                            StrSQl = StrSQl + "ON B.TEMPVAL=A.TEMPVAL "
                            dts = odbutil.FillDataSet(StrSQl, MyConnection)

                            Dim tempIdL As String = dts.Tables(0).Rows(0).Item("TEMPID").ToString()
                            Dim tempIdH As String = dts.Tables(0).Rows(1).Item("TEMPID").ToString()

                            Dim tempValL As String = dts.Tables(0).Rows(0).Item("TEMPVAL").ToString()
                            Dim tempValH As String = dts.Tables(0).Rows(1).Item("TEMPVAL").ToString()

                            Dim lowTemp As String = ""
                            Dim highTemp As String = ""

                            Count = Dt1.Tables(0).Rows.Count
                            For i = 0 To Dt1.Tables(0).Rows.Count - 1
                                Try
                                    'Getting lower RH
                                    dvBar.RowFilter = "MATID=" + MatID + " AND TEMPID=" + tempIdL + " AND RHID=" + Dt1.Tables(0).Rows(i).Item("RHID").ToString()
                                    dtBar = dvBar.ToTable()
                                    If dtBar.Rows(0).Item("OTRVALUE").ToString() = "" Then
                                        lowTemp = "10000"
                                    Else
                                        lowTemp = dtBar.Rows(0).Item("OTRVALUE").ToString()
                                    End If

                                    'Getting higher RH
                                    dvBar.RowFilter = "MATID=" + MatID + " AND TEMPID=" + tempIdH + " AND RHID=" + Dt1.Tables(0).Rows(i).Item("RHID").ToString()
                                    dtBar = dvBar.ToTable()
                                    If dtBar.Rows(0).Item("OTRVALUE").ToString() = "" Then
                                        highTemp = "10000"
                                    Else
                                        highTemp = dtBar.Rows(0).Item("OTRVALUE").ToString()
                                    End If

                                    OTRB(i) = CDbl(lowTemp) + ((OtrTemp - tempValL) / (tempValH - tempValL) * (CDbl(highTemp) - CDbl(lowTemp)))
                                    If ISADJTHICK <> "N" Then
                                        OTRB(i) = CDbl((OTRB(i) / Thick) * 25.4)
                                    End If

                                    If dsPref.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                                        OTRB(i) = FormatNumber(OTRB(i), 3)
                                    Else
                                        OTRB(i) = FormatNumber(CDbl(OTRB(i)) * CDbl(dsPref.Tables(0).Rows(0).Item("CONVAREA").ToString() / 1000) * 100, 3)
                                    End If

                                Catch ex As Exception
                                    OTRB(i) = "0"
                                End Try
                            Next
                        End If
                    Else
                        For k = 0 To 1
						
							StrSqlMaterial = "Select MATERIALS.Matid,(MATERIALS.Matid||':'||MATERIALS.MatDE1||''||MATERIALS.MatDE2) as MatDe,ISADJTHICK from MATERIALS where MATID=" + MatIds(k)
                            Dt = odbutil.FillDataSet(StrSqlMaterial, MyConnection)
                            ISADJTHICK = Dt.Tables(0).Rows(0).Item("ISADJTHICK").ToString()
							
                            If Dts1.Rows.Count > 0 Then
                                StrSQl = "SELECT OTRVALUE,WVTRVALUE,MATID,EFFDATE,TEMPID,RHID "
                                StrSQl = StrSQl + "FROM MATBARRIERMETRIC WHERE MATID=" + MatIds(k) + " AND TEMPID=" + Dts1.Rows(0).Item("TEMPID").ToString() + " ORDER BY RHID ASC "
                                Dts2 = odbutil.FillDataSet(StrSQl, MyConnection)

                                For i = 0 To Dts2.Tables(0).Rows.Count - 1
									Try
										OTR(i, k) = CDbl(Dts2.Tables(0).Rows(i).Item("OTRVALUE").ToString())
										OTRBL(i, k) = CDbl(Dts2.Tables(0).Rows(i).Item("OTRVALUE").ToString())

										If ISADJTHICK <> "N" Then
											OTRBL(i, k) = CDbl((Dts2.Tables(0).Rows(i).Item("OTRVALUE").ToString() / Thickn(k)) * 25.4)
										End If
									Catch ex As Exception
										OTR(i, k)="0"
										OTRBL(i, k) = "0"
									End Try
                                Next
                            Else
                                strBar = "SELECT MATID,EFFDATE,TEMPID,RHID,OTRVALUE,WVTRVALUE FROM MATBARRIERMETRIC WHERE MATID=" + MatIds(k) + " AND EFFDATE<=TO_DATE('" + dsOW.Tables(0).Rows(0).Item("EFFDATE").ToString() + "','MM//DD/YYYY')"
                                dsBar = odbutil.FillDataSet(strBar, MyConnection)

                                dvBar = dsBar.Tables(0).DefaultView

                                StrSQl = "SELECT (B.TEMPID) TEMPID,A.TEMPVAL FROM  "
                                StrSQl = StrSQl + "( "
                                StrSQl = StrSQl + "SELECT TEMPVAL FROM BARRIERTEMP WHERE TEMPVAL IN( "
                                StrSQl = StrSQl + "SELECT MAX(TEMPVAL)  FROM BARRIERTEMP "
                                StrSQl = StrSQl + "WHERE(TEMPVAL < " + OtrTemp + ") "
                                StrSQl = StrSQl + "UNION ALL "
                                StrSQl = StrSQl + "SELECT MIN(TEMPVAL) FROM BARRIERTEMP "
                                StrSQl = StrSQl + "WHERE TEMPVAL >" + OtrTemp + ") "
                                StrSQl = StrSQl + ")A "
                                StrSQl = StrSQl + "INNER JOIN BARRIERTEMP B "
                                StrSQl = StrSQl + "ON B.TEMPVAL=A.TEMPVAL "
                                dts = odbutil.FillDataSet(StrSQl, MyConnection)

                                Dim tempIdL As String = dts.Tables(0).Rows(0).Item("TEMPID").ToString()
                                Dim tempIdH As String = dts.Tables(0).Rows(1).Item("TEMPID").ToString()

                                Dim tempValL As String = dts.Tables(0).Rows(0).Item("TEMPVAL").ToString()
                                Dim tempValH As String = dts.Tables(0).Rows(1).Item("TEMPVAL").ToString()

                                Dim lowTemp As String = ""
                                Dim highTemp As String = ""

                                Count = Dt1.Tables(0).Rows.Count
                                For i = 0 To Dt1.Tables(0).Rows.Count - 1
                                    Try
                                        'Getting lower RH
                                        dvBar.RowFilter = "MATID=" + MatIds(k) + " AND TEMPID=" + tempIdL + " AND RHID=" + Dt1.Tables(0).Rows(i).Item("RHID").ToString()
                                        dtBar = dvBar.ToTable()
                                        If dtBar.Rows(0).Item("OTRVALUE").ToString() = "" Then
                                            lowTemp = "10000"
                                        Else
                                            lowTemp = dtBar.Rows(0).Item("OTRVALUE").ToString()
                                        End If

                                        'Getting higher RH
                                        dvBar.RowFilter = "MATID=" + MatIds(k) + " AND TEMPID=" + tempIdH + " AND RHID=" + Dt1.Tables(0).Rows(i).Item("RHID").ToString()
                                        dtBar = dvBar.ToTable()
                                        If dtBar.Rows(0).Item("OTRVALUE").ToString() = "" Then
                                            highTemp = "10000"
                                        Else
                                            highTemp = dtBar.Rows(0).Item("OTRVALUE").ToString()
                                        End If

                                        OTR(i,k) = CDbl(lowTemp) + ((OtrTemp - tempValL) / (tempValH - tempValL) * (CDbl(highTemp) - CDbl(lowTemp)))
                                        OTRBL(i, k) = CDbl(lowTemp) + ((OtrTemp - tempValL) / (tempValH - tempValL) * (CDbl(highTemp) - CDbl(lowTemp)))
                                        If ISADJTHICK <> "N" Then
                                            OTRBL(i, k) = CDbl((OTRBL(i, k) / Thickn(k)) * 25.4)
                                        End If

                                    Catch ex As Exception
                                        OTR(i,k) = "0"
										OTRBL(i, k) = "0"
                                    End Try
                                    'OTR(i, k) = CDbl(OTRB(i))
									
                                Next
                            End If
                        Next
                        For i = 0 To Dt1.Tables(0).Rows.Count - 1
                            If OTRPREF(i, 0) <> "" And OTRPREF(i, 1) <> "" Then
                                If OTRPREF(i, 0) <> "0" And OTRPREF(i, 1) <> "0" Then
                                    LAMINATEOTR(i) = (1 / ((1 / OTRPREF(i, 1)) + (1 / OTRPREF(i, 0))))
                                    OTR1 = CDbl(OTRPREF(i, 0) * Thickn(0) / 25.4)
                                    OTR2 = CDbl(OTRPREF(i, 1) * Thickn(1) / 25.4)
                                    If OTR1 < OTR2 Then
                                        PERCENTOTR(i) = ThickPer(0)
                                    ElseIf OTR2 < OTR1 Then
                                        PERCENTOTR(i) = ThickPer(1)
                                    Else
                                        PERCENTOTR(i) = ThickPer(0)
                                    End If
                                Else
                                    LAMINATEOTR(i) = "0"
                                    OTRB(i) = "0"
                                    PERCENTOTR(i) = "0"
                                End If
                            ElseIf OTRPREF(i, 0) <> "" Or OTRPREF(i, 1) <> "" Then
                                If OTRPREF(i, 0) = "" Then
                                    If OTRBL(i, 0) <> "0" And OTRPREF(i, 1) <> "0" Then
                                        LAMINATEOTR(i) = (1 / ((1 / OTRPREF(i, 1)) + (1 / OTRBL(i, 0))))
                                        OTR2 = CDbl(OTRPREF(i, 1) * Thickn(1) / 25.4)
                                        If OTR(i, 0) < OTR2 Then
                                            PERCENTOTR(i) = ThickPer(0)
                                        ElseIf OTR2 < OTR(i, 0) Then
                                            PERCENTOTR(i) = ThickPer(1)
                                        Else
                                            PERCENTOTR(i) = ThickPer(0)
                                        End If
                                    Else
                                        LAMINATEOTR(i) = "0"
                                        OTRB(i) = "0"
                                        PERCENTOTR(i) = "0"
                                    End If
                                ElseIf OTRPREF(i, 1) = "" Then
                                    If OTRPREF(i, 0) <> "0" And OTRBL(i, 1) <> "0" Then
                                        LAMINATEOTR(i) = (1 / ((1 / OTRBL(i, 1)) + (1 / OTRPREF(i, 0))))
                                        OTR1 = CDbl(OTRPREF(i, 0) * Thickn(0) / 25.4)
                                        If OTR1 < OTR(i, 1) Then
                                            PERCENTOTR(i) = ThickPer(0)
                                        ElseIf OTR(i, 1) < OTR1 Then
                                            PERCENTOTR(i) = ThickPer(1)
                                        Else
                                            PERCENTOTR(i) = ThickPer(0)
                                        End If
                                    Else
                                        LAMINATEOTR(i) = "0"
                                        OTRB(i) = "0"
                                        PERCENTOTR(i) = "0"
                                    End If
                                End If
                            ElseIf OTR(i, 0) <> "0" And OTR(i, 1) <> "0" Then
                                LAMINATEOTR(i) = (1 / ((1 / OTRBL(i, 1)) + (1 / OTRBL(i, 0))))
                                If CDbl(OTR(i, 0)) < CDbl(OTR(i, 1)) Then
                                    PERCENTOTR(i) = ThickPer(0)
                                ElseIf CDbl(OTR(i, 1)) < CDbl(OTR(i, 0)) Then
                                    PERCENTOTR(i) = ThickPer(1)
                                Else
                                    PERCENTOTR(i) = ThickPer(0)
                                End If
                            Else
                                LAMINATEOTR(i) = "0"
                                OTRB(i) = "0"
                                PERCENTOTR(i) = "0"
                            End If
                        Next
                        Dim DtsOTR As New DataSet
                        Dim dvOTR As New DataView
                        Dim dtOTR As New DataTable
                        StrSQl = "SELECT LOWPERM, HIGHPERM, FACTOR FROM PENALTYFACTOR WHERE LOWPERM IN (" + FormatNumber(PERCENTOTR(0), 0) + "," + FormatNumber(PERCENTOTR(1), 0) + "," + FormatNumber(PERCENTOTR(2), 0) + ","
                        StrSQl = StrSQl + FormatNumber(PERCENTOTR(3), 0) + "," + FormatNumber(PERCENTOTR(4), 0) + "," + FormatNumber(PERCENTOTR(5), 0) + "," + FormatNumber(PERCENTOTR(6), 0) + "," + FormatNumber(PERCENTOTR(7), 0) + "," + FormatNumber(PERCENTOTR(8), 0) + ","
                        StrSQl = StrSQl + FormatNumber(PERCENTOTR(9), 0) + FormatNumber(PERCENTOTR(10), 0) + ")"
                        DtsOTR = odbutil.FillDataSet(StrSQl, MyConnection)

                        If DtsOTR.Tables(0).Rows.Count > 0 Then
                            dvOTR = DtsOTR.Tables(0).DefaultView()
                            For i = 0 To Dt1.Tables(0).Rows.Count - 1
                                If OTRB(i) <> "0" Then
                                    dvOTR.RowFilter = "LOWPERM = " + FormatNumber(PERCENTOTR(i), 0)
                                    dtOTR = dvOTR.ToTable()
                                    If dtOTR.Rows.Count > 0 Then
                                        OTRB(i) = (LAMINATEOTR(i) + (LAMINATEOTR(i) * dtOTR.Rows(0).Item("FACTOR").ToString()))
                                    Else
                                        OTRB(i) = " 0"
                                    End If
									If dsPref.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                                        OTRB(i) = FormatNumber(OTRB(i), 3)
                                    Else
                                        OTRB(i) = FormatNumber(CDbl(OTRB(i)) * CDbl(dsPref.Tables(0).Rows(0).Item("CONVAREA").ToString() / 1000) * 100, 3)
                                    End If
                                End If
                            Next
                        End If
                    End If
                Else

                    StrSqlMaterial1 = "SELECT RHID,RHVALUE FROM BARRIERH WHERE RHVALUE=" + dsOW.Tables(0).Rows(0).Item("WVTRRH").ToString() + " "
                    Dts1 = odbutil.FillDataTable(StrSqlMaterial1, MyConnection)

                    StrSqlMaterial1 = "SELECT TEMPID,TEMPVAL FROM BARRIERTEMP ORDER BY TEMPID ASC "
                    Dt1 = odbutil.FillDataSet(StrSqlMaterial1, MyConnection)
                    OtrTemp = dsOW.Tables(0).Rows(0).Item("WVTRRH").ToString()

                    If MatType = "Single" Then
                        If Dts1.Rows.Count > 0 Then
                            StrSQl = "SELECT OTRVALUE,WVTRVALUE,MATID,EFFDATE,TEMPID,RHID "
                            StrSQl = StrSQl + "FROM MATBARRIERMETRIC WHERE MATID=" + MatID + " AND RHID=" + Dts1.Rows(0).Item("RHID").ToString() + " ORDER BY RHID ASC "
                            Dts2 = odbutil.FillDataSet(StrSQl, MyConnection)
                            dvBar = Dts2.Tables(0).DefaultView

                            Count = Dt1.Tables(0).Rows.Count
                            For i = 0 To Dt1.Tables(0).Rows.Count - 1
                                Try
                                    'Getting lower RH
                                    dvBar.RowFilter = "MATID=" + MatID + " AND RHID=" + Dts1.Rows(0).Item("RHID").ToString() + " AND TEMPID=" + Dt1.Tables(0).Rows(i).Item("TEMPID").ToString()
                                    dtBar = dvBar.ToTable()
                                    If dtBar.Rows(0).Item("WVTRVALUE").ToString() = "" Then
                                        WVTRB(i) = "10000"
                                    Else
                                        WVTRB(i) = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                    End If

                                    If ISADJTHICK <> "N" Then
                                        WVTRB(i) = CDbl((WVTRB(i) / Thick) * 25.4)
                                    End If

                                    If dsPref.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                                        WVTRB(i) = FormatNumber(WVTRB(i), 3)
                                    Else
                                        WVTRB(i) = FormatNumber(CDbl(WVTRB(i)) * CDbl(dsPref.Tables(0).Rows(0).Item("CONVAREA").ToString() / 1000) * 100, 3)
                                    End If
                                Catch ex As Exception
                                    WVTRB(i) = "0"
                                End Try
                            Next
                        Else
                            StrSQl = "SELECT (B.RHID) RHID,A.RHVALUE FROM  "
                            StrSQl = StrSQl + "( "
                            StrSQl = StrSQl + "SELECT RHVALUE FROM BARRIERH WHERE RHVALUE IN( "
                            StrSQl = StrSQl + "SELECT MAX(RHVALUE)  FROM BARRIERH "
                            StrSQl = StrSQl + "WHERE(RHVALUE < " + OtrTemp + ") "
                            StrSQl = StrSQl + "UNION ALL "
                            StrSQl = StrSQl + "SELECT MIN(RHVALUE) FROM BARRIERH "
                            StrSQl = StrSQl + "WHERE RHVALUE >" + OtrTemp + ") "
                            StrSQl = StrSQl + ")A "
                            StrSQl = StrSQl + "INNER JOIN BARRIERH B "
                            StrSQl = StrSQl + "ON B.RHVALUE=A.RHVALUE "
                            dts = odbutil.FillDataSet(StrSQl, MyConnection)

                            Dim tempIdL As String = dts.Tables(0).Rows(0).Item("RHID").ToString()
                            Dim tempIdH As String = dts.Tables(0).Rows(1).Item("RHID").ToString()

                            Dim tempValL As String = dts.Tables(0).Rows(0).Item("RHVALUE").ToString()
                            Dim tempValH As String = dts.Tables(0).Rows(1).Item("RHVALUE").ToString()

                            Dim lowTemp As String = ""
                            Dim highTemp As String = ""

                            Count = Dt1.Tables(0).Rows.Count
                            For i = 0 To Dt1.Tables(0).Rows.Count - 1
                                Try
                                    'Getting lower RH
                                    dvBar.RowFilter = "MATID=" + MatID + " AND RHID=" + tempIdL + " AND TEMPID=" + Dt1.Tables(0).Rows(i).Item("TEMPID").ToString()
                                    dtBar = dvBar.ToTable()
                                    If dtBar.Rows(0).Item("WVTRVALUE").ToString() = "" Then
                                        lowTemp = "10000"
                                    Else
                                        lowTemp = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                    End If

                                    'Getting higher RH
                                    dvBar.RowFilter = "MATID=" + MatID + " AND RHID=" + tempIdH + " AND TEMPID=" + Dt1.Tables(0).Rows(i).Item("TEMPID").ToString()
                                    dtBar = dvBar.ToTable()
                                    If dtBar.Rows(0).Item("WVTRVALUE").ToString() = "" Then
                                        highTemp = "10000"
                                    Else
                                        highTemp = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                    End If

                                    WVTRB(i) = CDbl(lowTemp) + ((OtrTemp - tempValL) / (tempValH - tempValL) * (CDbl(highTemp) - CDbl(lowTemp)))
                                    If ISADJTHICK <> "N" Then
                                        WVTRB(i) = CDbl((WVTRB(i) / Thick) * 25.4)
                                    End If

                                    If dsPref.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                                        WVTRB(i) = FormatNumber(WVTRB(i), 3)
                                    Else
                                        WVTRB(i) = FormatNumber(CDbl(WVTRB(i)) * CDbl(dsPref.Tables(0).Rows(0).Item("CONVAREA").ToString() / 1000) * 100, 3)
                                    End If
                                Catch ex As Exception
                                    WVTRB(i) = "0"
                                End Try
                            Next
                        End If
                    Else
                        For j = 0 To 1
						
							StrSqlMaterial = "Select MATERIALS.Matid,(MATERIALS.Matid||':'||MATERIALS.MatDE1||''||MATERIALS.MatDE2) as MatDe,ISADJTHICK from MATERIALS where MATID=" + MatIds(j)
                            Dt = odbutil.FillDataSet(StrSqlMaterial, MyConnection)
                            ISADJTHICK = Dt.Tables(0).Rows(0).Item("ISADJTHICK").ToString()
							
                            If Dts1.Rows.Count > 0 Then
                                StrSQl = "SELECT OTRVALUE,WVTRVALUE,MATID,EFFDATE,TEMPID,RHID "
                                StrSQl = StrSQl + "FROM MATBARRIERMETRIC WHERE MATID=" + MatIds(j) + " AND RHID=" + Dts1.Rows(0).Item("RHID").ToString() + " ORDER BY RHID ASC "
                                Dts2 = odbutil.FillDataSet(StrSQl, MyConnection)
                                dvBar = Dts2.Tables(0).DefaultView

                                Count = Dt1.Tables(0).Rows.Count
                                For i = 0 To Dt1.Tables(0).Rows.Count - 1
                                    Try
                                        'Getting lower RH
                                        dvBar.RowFilter = "MATID=" + MatIds(j) + " AND RHID=" + Dts1.Rows(0).Item("RHID").ToString() + " AND TEMPID=" + Dt1.Tables(0).Rows(i).Item("TEMPID").ToString()
                                        dtBar = dvBar.ToTable()
                                        If dtBar.Rows(0).Item("WVTRVALUE").ToString() = "" Then
                                            WVTR(i, j) = "10000"
											WVTRBL(i, j) = "10000"
                                        Else
                                            WVTR(i, j) = dtBar.Rows(0).Item("WVTRVALUE").ToString()
											WVTRBL(i, j) = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                        End If

                                        If ISADJTHICK <> "N" Then
                                            WVTRBL(i, j) = CDbl((WVTRBL(i, j) / Thickn(j)) * 25.4)
                                        End If
                                    Catch ex As Exception
                                        WVTR(i, j) = "0"
										WVTRBL(i, j) = "0"
                                    End Try
                                Next
                            Else
                                strBar = "SELECT MATID,EFFDATE,TEMPID,RHID,OTRVALUE,WVTRVALUE FROM MATBARRIERMETRIC WHERE MATID=" + MatIds(j) + " AND EFFDATE<=TO_DATE('" + dsOW.Tables(0).Rows(0).Item("EFFDATE").ToString() + "','MM//DD/YYYY')"
                                dsBar = odbutil.FillDataSet(strBar, MyConnection)

                                dvBar = dsBar.Tables(0).DefaultView

                                StrSQl = "SELECT (B.RHID) RHID,A.RHVALUE FROM  "
                                StrSQl = StrSQl + "( "
                                StrSQl = StrSQl + "SELECT RHVALUE FROM BARRIERH WHERE RHVALUE IN( "
                                StrSQl = StrSQl + "SELECT MAX(RHVALUE)  FROM BARRIERH "
                                StrSQl = StrSQl + "WHERE(RHVALUE < " + OtrTemp + ") "
                                StrSQl = StrSQl + "UNION ALL "
                                StrSQl = StrSQl + "SELECT MIN(RHVALUE) FROM BARRIERH "
                                StrSQl = StrSQl + "WHERE RHVALUE >" + OtrTemp + ") "
                                StrSQl = StrSQl + ")A "
                                StrSQl = StrSQl + "INNER JOIN BARRIERH B "
                                StrSQl = StrSQl + "ON B.RHVALUE=A.RHVALUE "
                                dts = odbutil.FillDataSet(StrSQl, MyConnection)

                                Dim tempIdL As String = dts.Tables(0).Rows(0).Item("RHID").ToString()
                                Dim tempIdH As String = dts.Tables(0).Rows(1).Item("RHID").ToString()

                                Dim tempValL As String = dts.Tables(0).Rows(0).Item("RHVALUE").ToString()
                                Dim tempValH As String = dts.Tables(0).Rows(1).Item("RHVALUE").ToString()

                                Dim lowTemp As String = ""
                                Dim highTemp As String = ""

                                Count = Dt1.Tables(0).Rows.Count
                                For i = 0 To Dt1.Tables(0).Rows.Count - 1
                                    Try
                                        'Getting lower RH
                                        dvBar.RowFilter = "MATID=" + MatIds(j) + " AND RHID=" + tempIdL + " AND TEMPID=" + Dt1.Tables(0).Rows(i).Item("TEMPID").ToString()
                                        dtBar = dvBar.ToTable()
                                        If dtBar.Rows(0).Item("WVTRVALUE").ToString() = "" Then
                                            lowTemp = "10000"
                                        Else
                                            lowTemp = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                        End If

                                        'Getting higher RH
                                        dvBar.RowFilter = "MATID=" + MatIds(j) + " AND RHID=" + tempIdH + " AND TEMPID=" + Dt1.Tables(0).Rows(i).Item("TEMPID").ToString()
                                        dtBar = dvBar.ToTable()
                                        If dtBar.Rows(0).Item("WVTRVALUE").ToString() = "" Then
                                            highTemp = "10000"
                                        Else
                                            highTemp = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                        End If

                                        WVTR(i, j) = CDbl(lowTemp) + ((OtrTemp - tempValL) / (tempValH - tempValL) * (CDbl(highTemp) - CDbl(lowTemp)))
                                        WVTRBL(i, j) = CDbl(lowTemp) + ((OtrTemp - tempValL) / (tempValH - tempValL) * (CDbl(highTemp) - CDbl(lowTemp)))
                                        If ISADJTHICK <> "N" Then
                                            WVTRBL(i, j) = CDbl((WVTRBL(i, j) / Thickn(j)) * 25.4)
                                        End If
										
                                    Catch ex As Exception
                                        WVTR(i, j) = "0"
										WVTRBL(i, j) = "0"
                                    End Try
                                Next
                            End If
                        Next
                        For i = 0 To Dt1.Tables(0).Rows.Count - 1
                            If WVTRPREF(i, 0) <> "" And WVTRPREF(i, 1) <> "" Then
                                If WVTRPREF(i, 0) <> "0" And WVTRPREF(i, 1) <> "0" Then
                                    LAMINATEWVTR(i) = (1 / ((1 / WVTRPREF(i, 1)) + (1 / WVTRPREF(i, 0))))
                                    WVTR1 = CDbl(WVTRPREF(i, 0) * Thickn(0) / 25.4)
                                    WVTR2 = CDbl(WVTRPREF(i, 1) * Thickn(1) / 25.4)
                                    If WVTR1 < WVTR2 Then
                                        PERCENTWVTR(i) = ThickPer(0)
                                    ElseIf WVTR2 < WVTR1 Then
                                        PERCENTWVTR(i) = ThickPer(1)
                                    Else
                                        PERCENTWVTR(i) = ThickPer(0)
                                    End If
                                Else
                                    LAMINATEWVTR(i) = "0"
                                    WVTRB(i) = "0"
                                    PERCENTWVTR(i) = "0"
                                End If
                            ElseIf WVTRPREF(i, 0) <> "" Or WVTRPREF(i, 1) <> "" Then
                                If WVTRPREF(i, 0) = "" Then
                                    If WVTRBL(i, 0) <> "0" And WVTRPREF(i, 1) <> "0" Then
                                        LAMINATEWVTR(i) = (1 / ((1 / WVTRPREF(i, 1)) + (1 / WVTRBL(i, 0))))
                                        WVTR2 = CDbl(WVTRPREF(i, 1) * Thickn(1) / 25.4)
                                        If WVTR(i, 0) < WVTR2 Then
                                            PERCENTWVTR(i) = ThickPer(0)
                                        ElseIf WVTR2 < WVTR(i, 0) Then
                                            PERCENTWVTR(i) = ThickPer(1)
                                        Else
                                            PERCENTWVTR(i) = ThickPer(0)
                                        End If
                                    Else
                                        LAMINATEWVTR(i) = "0"
                                        WVTRB(i) = "0"
                                        PERCENTWVTR(i) = "0"
                                    End If
                                ElseIf WVTRPREF(i, 1) = "" Then
                                    If WVTRPREF(i, 0) <> "0" And WVTRBL(i, 1) <> "0" Then
                                        LAMINATEWVTR(i) = (1 / ((1 / WVTRBL(i, 1)) + (1 / WVTRPREF(i, 0))))
                                        WVTR1 = CDbl(WVTRPREF(i, 0) * Thickn(0) / 25.4)
                                        If WVTR1 < WVTR(i, 1) Then
                                            PERCENTWVTR(i) = ThickPer(0)
                                        ElseIf WVTR(i, 1) < WVTR1 Then
                                            PERCENTWVTR(i) = ThickPer(1)
                                        Else
                                            PERCENTWVTR(i) = ThickPer(0)
                                        End If
                                    Else
                                        LAMINATEWVTR(i) = "0"
                                        WVTRB(i) = "0"
                                        PERCENTWVTR(i) = "0"
                                    End If
                                End If
                            ElseIf WVTR(i, 0) <> "0" And WVTR(i, 1) <> "0" Then
                                LAMINATEWVTR(i) = (1 / ((1 / WVTRBL(i, 1)) + (1 / WVTRBL(i, 0))))
                                If CDbl(WVTR(i, 0)) < CDbl(WVTR(i, 1)) Then
                                    PERCENTWVTR(i) = ThickPer(0)
                                ElseIf CDbl(WVTR(i, 1)) < CDbl(WVTR(i, 0)) Then
                                    PERCENTWVTR(i) = ThickPer(1)
                                Else
                                    PERCENTWVTR(i) = ThickPer(0)
                                End If
                            Else
                                LAMINATEWVTR(i) = "0"
                                WVTRB(i) = "0"
                                PERCENTWVTR(i) = "0"
                            End If
                        Next

                        Dim dsWVTR As New DataSet
                        Dim dvWVTR As New DataView
                        Dim dtWVTR As New DataTable
                        StrSql1 = "SELECT LOWPERM, HIGHPERM, FACTOR FROM PENALTYFACTOR WHERE LOWPERM IN (" + FormatNumber(PERCENTWVTR(0), 0) + "," + FormatNumber(PERCENTWVTR(1), 0) + "," + FormatNumber(PERCENTWVTR(2), 0) + ","
                        StrSql1 = StrSql1 + FormatNumber(PERCENTWVTR(3), 0) + "," + FormatNumber(PERCENTWVTR(4), 0) + "," + FormatNumber(PERCENTWVTR(5), 0) + "," + FormatNumber(PERCENTWVTR(6), 0) + "," + FormatNumber(PERCENTWVTR(7), 0) + "," + FormatNumber(PERCENTWVTR(8), 0) + ","
                        StrSql1 = StrSql1 + FormatNumber(PERCENTWVTR(9), 0) + "," + FormatNumber(PERCENTWVTR(10), 0) + ")"
                        dsWVTR = odbutil.FillDataSet(StrSql1, MyConnection)

                        If dsWVTR.Tables(0).Rows.Count > 0 Then
                            dvWVTR = dsWVTR.Tables(0).DefaultView()
                            For i = 0 To Dt1.Tables(0).Rows.Count - 1
                                If WVTRB(i) <> "0" Then
                                    dvWVTR.RowFilter = "LOWPERM = " + FormatNumber(PERCENTWVTR(i), 0)
                                    dtWVTR = dvWVTR.ToTable()
                                    If dtWVTR.Rows.Count > 0 Then
                                        WVTRB(i) = (LAMINATEWVTR(i) + (LAMINATEWVTR(i) * dtWVTR.Rows(0).Item("FACTOR").ToString()))
                                    Else
                                        WVTRB(i) = " 0"
                                    End If
									If dsPref.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                                        WVTRB(i) = WVTRB(i)
                                    Else
                                        WVTRB(i) = CDbl(WVTRB(i)) * CDbl(dsPref.Tables(0).Rows(0).Item("CONVAREA").ToString() / 1000) * 100
                                    End If
                                End If
                            Next
                        End If
                    End If
                End If


                If Not IsPostBack Then
                End If

                If Type = "OTR" Then
                    If dsPref.Tables(0).Rows(0).Item("UNITS").ToString() <> 1 Then
                        OtrTemp = FormatNumber((CDbl(OtrTemp) * (9 / 5)) + 32, 1) + " °F"
                    Else
                        OtrTemp = FormatNumber(CDbl(OtrTemp), 1) + " °C"
                    End If
                Else
                    For i = 0 To Dt1.Tables(0).Rows.Count - 1
                        If dsPref.Tables(0).Rows(0).Item("UNITS").ToString() <> 1 Then
                            arrTemp(i) = FormatNumber((CDbl(Dt1.Tables(0).Rows(i).Item("TEMPVAL").ToString()) * (9 / 5)) + 32, 1) '+ " °F"
                        Else
                            arrTemp(i) = Dt1.Tables(0).Rows(i).Item("TEMPVAL").ToString() '+ " °C"
                        End If
                    Next
                End If

                Count = Dt1.Tables(0).Rows.Count

                If Type = "OTR" Then
                    pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + OtrTemp + ")"
                    Dim i As New Integer
                    For i = 0 To Count - 1
                        pcScript &= "" + Graphtype + ".setSeries(" & Dt1.Tables(0).Rows(i).Item("RHVALUE").ToString() & ";(STYP_13)" & OTRB(i) & ")"
                    Next
                Else
                    pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + OtrTemp + " % RH)"
                    Dim i As New Integer
                    For i = 0 To Count - 1
                        pcScript &= "" + Graphtype + ".setSeries(" & arrTemp(i) & ";(STYP_13)" & WVTRB(i) & ")"
                    Next
            End If


            'Code for Image Template
            Dim dsChartSetting As New DataTable
            Dim StrSqlChartSetting As String = String.Empty
            Dim MyConfigConnection As String = String.Empty
            MyConfigConnection = System.Configuration.ConfigurationManager.AppSettings("ConfigurationConnectionString")


            StrSqlChartSetting = "SELECT EXTSERVERADD, INTCOMPORTADD FROM CHARTSETTING"
            dsChartSetting = odbutil.FillDataTable(StrSqlChartSetting, MyConfigConnection)
            Dim myImage As CordaEmbedder = New CordaEmbedder()
            myImage.externalServerAddress = dsChartSetting.Rows(0)("EXTSERVERADD").ToString()
            myImage.internalCommPortAddress = dsChartSetting.Rows(0)("INTCOMPORTADD").ToString()

            myImage.imageTemplate = GraphName + ".itxml"
            myImage.userAgent = Request.UserAgent
            myImage.width = 700
            myImage.height = 400
            myImage.returnDescriptiveLink = True
            myImage.language = "EN"
            If Type = "OTR" Then
	            myImage.pcScript = pcScript + "Y-axis.SetText(OTR " + Unit + ")"
            Else
              myImage.pcScript = pcScript + "Y-axis.SetText(WVTR " + Unit + ")"
            End If
            myImage.outputType = "FLASH"
            myImage.fallback = "STRICT"
            MaterialPrice.InnerHtml = myImage.getEmbeddingHTML


            End If



        Catch ex As Exception
            Response.Write("Error:" + ex.Message.ToString())


        End Try


    End Sub

End Class

