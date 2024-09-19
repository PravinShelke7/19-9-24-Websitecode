Imports Corda
Imports System.Web.UI.HtmlControls
Imports System.Data
Imports System.Data.OleDb
Imports System.Web.HttpBrowserCapabilities
Imports E1GetData

Public Class EnergyPriceChart
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
            lblTag.Text = "Copyright 1997 - " + Now.Year().ToString() + " Savvypack Corporation."
            If Session("back") = "" Then
                'lblsession.Visible = True
                'Charts.Visible = False
                Dim obj As New CryptoHelper
                Response.Redirect("../Pages/Econ1/Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            Else
                If Not IsPostBack Then
                    BindCountry()
                End If
                If Session("Service") = "COMP" Then
                    E1Table.Attributes.Add("class", "E1CompModule")
                End If
                'Begin Embedder Code
                InitializeComponent()
                ' Dim control As HtmlGenericControl = FindControl("MaterialPrice")
                Dim pcScript = ""
                Dim odbutil As New DBUtil()
                Dim Id As String = ""
                Id = MPC.SelectedValue.ToString()
                Dim MatID As String = ""
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
                Dim USERID As String = ""
                Dim Unit As String = ""
                UserName = Session("E1UserName")
                USERID = Session("USERID")
                Graphtype = "Material_Price"
                GraphName = "MaterialPrice1"


                If Check = 1 Then
                    If Id = "" Then
                        MatID = "1"
                    Else
                        MatID = Id

                    End If
                Else
                    MatID = "1"
                End If


                'For One Year
                If a.Checked = True Then
                    lblyear.Text = "<b>Energy  - 1 Year Historical</b>"
                    YearValue = 1
                End If

                'For Three Year
                If b.Checked = True Then
                    YearValue = 3
                    lblyear.Text = "<b>Energy - 3 Year Historical</b>"

                End If

                'For Five Year
                If c.Checked = True Then
                    lblyear.Text = "<b>Energy - 5 Year Historical</b>"
                    YearValue = 5
                End If


                If a.Checked = False And b.Checked = False And c.Checked = False Then
                    YearValue = 5
                    lblyear.Text = "<b>Energy - 5 Year Historical</b>"
                    c.Checked = True
                End If



                Dim MyConnection As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
                Dim StrSQl As String = ""

                'Comparison of two materials
                Dim NewMatID As String = MPC2.SelectedValue
                If NewMatID = "" Then
                    NewMatID = 0
                End If






                Dim StrSqlMaterial As String = "SELECT ENERGYID,(ENERGYDE1||' '||ENERGYDE2)AS ENDGRYDES FROM ENERGY WHERE ENERGYID <> 0  ORDER BY ENDGRYDES "
                Dim Dt As New DataSet()


                Dim StrSqlMaterial1 As String = "SELECT ENERGYID,(ENERGYDE1||' '||ENERGYDE2)AS ENDGRYDES FROM ENERGY ORDER BY ENDGRYDES "
                Dim Dt1 As New DataSet()

                Dim StrSqlPref As String = "SELECT TITLE1,TITLE2,TITLE3,TITLE4 FROM  CHARTPREFERENCES WHERE USERID='" + Session("USERID").ToString().ToUpper() + "'"
                Dim DtsPref As New DataSet()





                Dim StrSqlCurrency As String = ""
                Dim Dt2 As New DataSet()
                StrSqlCurrency = "SELECT CURID,  "
                StrSqlCurrency = StrSqlCurrency + "CURDE1 "
                StrSqlCurrency = StrSqlCurrency + "FROM CURRENCY "



                Dt = odbutil.FillDataSet(StrSqlMaterial, MyConnection)
                Dt1 = odbutil.FillDataSet(StrSqlMaterial1, MyConnection)
                Dt2 = odbutil.FillDataSet(StrSqlCurrency, MyConnection)
                DtsPref = odbutil.FillDataSet(StrSqlPref, MyConnection)


                If Not IsPostBack Then
                    'Material Selection Combo
                    MPC.DataSource = Dt
                    MPC.DataValueField = "ENERGYID"
                    MPC.DataTextField = "ENDGRYDES"
                    MPC.SelectedValue = MatID
                    MPC.DataBind()

                    'Material Comparison Combo
                    MPC2.DataSource = Dt1
                    MPC2.DataValueField = "ENERGYID"
                    MPC2.DataTextField = "ENDGRYDES"
                    MPC2.SelectedValue = "0"
                    MPC2.DataBind()


                    'CurrConversion.DataSource = Dt2
                    'CurrConversion.DataValueField = "CURID"
                    'CurrConversion.DataTextField = "CURDE1"
                    'CurrConversion.DataBind()


                End If



                'Dim CurrType As String = ""
                'CurrType = CurrConversion.SelectedValue


                Dim ComboStr1 As String = MPC.SelectedItem.Text
                Dim Str1 As New Integer
                Dim cnvfac As String = ""

                'If perkg.Checked = True Then
                '    cnvfac = 1
                'Else
                '    cnvfac = 0
                'End If

                If NewMatID = 0 Then
                    NewMatID = MatID
                    Str1 = ComboStr1.IndexOf(":")
                    ComboStr1 = ComboStr1.Substring(Str1 + 1, ComboStr1.Length - Str1 - 1)
                    mat.Text = ComboStr1
                Else
                    NewMatID = NewMatID
                    MaterialName1 = MPC.SelectedItem.Text.ToString()
                    MaterialName2 = MPC2.SelectedItem.Text.ToString()
                    MatStr1 = MaterialName1.IndexOf(":")
                    MatStr2 = MaterialName2.IndexOf(":")
                    MaterialName1 = MaterialName1.Substring(MatStr1 + 1, MaterialName1.Length - MatStr1 - 1)
                    MaterialName2 = MaterialName2.Substring(MatStr2 + 1, MaterialName2.Length - MatStr2 - 1)
                    mat.Text = MaterialName1 + "  &   " + MaterialName2
                End If

                If NewMatID = 1 Then
                    Unit = "kwh"
                ElseIf NewMatID = 2 Then
                    Unit = "Mcf"
                End If




                'StrSQl = "SELECT  TO_CHAR(A.EFFDATE,'MON-yyyy')AS EFFDATE,  "
                'StrSQl = StrSQl + "(A.PRICE*CHARTPREFERENCES.CURR) ID1, "
                'StrSQl = StrSQl + "(B.PRICE*CHARTPREFERENCES.CURR) ID2 "
                'StrSQl = StrSQl + "FROM ENERGYARCH A, "
                'StrSQl = StrSQl + "ENERGYARCH B, "
                'StrSQl = StrSQl + "CHARTPREFERENCES "
                'StrSQl = StrSQl + "WHERE A.EFFDATE = B.EFFDATE "
                'StrSQl = StrSQl + "AND A.ENERGYID =  " + MatID + ""
                'StrSQl = StrSQl + "AND B.ENERGYID =" + NewMatID + ""
                'StrSQl = StrSQl + "AND A.EFFDATE BETWEEN TO_DATE(ADD_MONTHS(TRUNC(SYSDATE, 'MONTH'),-12* " + YearValue + ")) AND TO_DATE(ADD_MONTHS(TRUNC(SYSDATE, 'MONTH'),-0)) "
                'StrSQl = StrSQl + "AND CHARTPREFERENCES.USERNAME='" + UserName + "' "

                StrSQl = "SELECT  TO_CHAR(A.EFFDATE,'MON-yyyy')AS EFFDATE,  "
                StrSQl = StrSQl + "(A.PRICE*CURR.CURPUSD) ID1, "
                StrSQl = StrSQl + "(B.PRICE*CURR.CURPUSD) ID2, "
                StrSQl = StrSQl + "A.EFFDATE EDATE "
                StrSQl = StrSQl + "FROM ENERGYARCHNEW A, "
                StrSQl = StrSQl + "ENERGYARCHNEW B, "
                StrSQl = StrSQl + "CHARTPREFERENCES, "
                StrSQl = StrSQl + "CURRENCYARCH CURR "
                StrSQl = StrSQl + "WHERE A.EFFDATE = B.EFFDATE "
                StrSQl = StrSQl + "AND A.ENERGYID =  " + MatID + ""
                StrSQl = StrSQl + "AND B.ENERGYID = " + NewMatID + ""
                StrSQl = StrSQl + "AND CURR.EFFDATE = (CASE WHEN CHARTPREFERENCES.CURRENCY = 0 THEN "
                StrSQl = StrSQl + "(TO_DATE('01/01/1900','MM/DD/YYYY')) "
                StrSQl = StrSQl + "ELSE "
                StrSQl = StrSQl + "(A.EFFDATE) "
                StrSQl = StrSQl + "END "
                StrSQl = StrSQl + ") "
                StrSQl = StrSQl + "AND CURR.CURID= CHARTPREFERENCES.CURRENCY "
                StrSQl = StrSQl + "AND A.EFFDATE BETWEEN TO_DATE(ADD_MONTHS(TRUNC(SYSDATE, 'MONTH'),-12*  " + YearValue + ")) AND TO_DATE(ADD_MONTHS(TRUNC(SYSDATE, 'MONTH'),-0)) "
                StrSQl = StrSQl + "AND CHARTPREFERENCES.USERID='" + USERID.ToString() + "' "
                StrSQl = StrSQl + "AND A.COUNTRYID=" + ddlCountry.SelectedValue.ToString() + " AND B.COUNTRYID=" + ddlCountry.SelectedValue.ToString() + " "

                StrSQl = StrSQl + "ORDER BY EDATE ASC"



                Dim Dts As New DataTable()
                Dts = odbutil.FillDataTable(StrSQl, MyConnection)
                Dim Count As String = Dts.Rows.Count








                'Specify Graph Specific PCScript   
                If NewMatID <> MatID Then
                    pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" & MaterialName1.ToString() & ";" & MaterialName2.ToString() & ")"
                    Dim i As New Integer
                    For i = 0 To Count - 1
                        pcScript &= "" + Graphtype + ".setSeries(" & Dts.Rows(i).Item(0) & ";" & Dts.Rows(i).Item(1) & ";" & Dts.Rows(i).Item(2) & ")"
                    Next
                Else
                    pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" & mat.Text.ToString() & ")"
                    Dim i As New Integer
                    For i = 0 To Count - 1
                        pcScript &= "" + Graphtype + ".setSeries(" & Dts.Rows(i).Item(0) & ";" & Dts.Rows(i).Item(1) & ")"
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
                myImage.pcScript = pcScript + "Y-axis.SetText(" + DtsPref.Tables(0).Rows(0).Item("TITLE2").ToString() + "/" + Unit.ToString() + ")"
                myImage.outputType = "FLASH"
                myImage.fallback = "STRICT"
                MaterialPrice.InnerHtml = myImage.getEmbeddingHTML


            End If



        Catch ex As Exception
            Response.Write("Error:" + ex.Message.ToString())


        End Try


    End Sub

    Protected Sub BindCountry()
        Dim objGetData As New E1GetData.Selectdata

        Dim ds As New DataSet
        Try
            ds = objGetData.GetCountries()
            With ddlCountry
                .DataTextField = "COUNTRYDE1"
                .DataValueField = "COUNTRYID"
                .DataSource = ds
                .DataBind()
            End With

        Catch ex As Exception

        End Try
    End Sub

End Class
