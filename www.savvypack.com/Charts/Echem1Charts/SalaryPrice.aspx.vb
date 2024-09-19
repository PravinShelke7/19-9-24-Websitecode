Imports Corda
Imports System.Web.UI.HtmlControls
Imports System.Data
Imports System.Data.OleDb
Imports System.Web.HttpBrowserCapabilities
Imports SelectQuery
Partial Class Charts_Echem1Charts_SalaryPrice
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("back") = "" Then
                ' lblsession.Visible = True
                ' Charts.Visible = False
                Dim obj As New CryptoHelper
                Response.Redirect("../Pages/Echem1/Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            Else

                '  Charts.Visible = True
                'Begin Embedder Code
                InitializeComponent()
                Dim control As HtmlGenericControl = FindControl("SalaryPrice")
                Dim pcScript = ""
                Dim odbutil As New DBUtil()
                Dim Id As String = ""
                Id = SPC1.SelectedValue.ToString()
                Dim MatID As String = ""
                Check = Request.Form("Chart")
                Dim Graphtype As String = ""
                Dim GraphName As String = ""
                Dim YearValue As String = ""
                Dim UserName As String = Session("Echem1UserName")

                Graphtype = "Material_Price"
                GraphName = "MaterialPrice1"


                If Check = 1 Then
                    If Id = "" Then
                        MatID = "1"
                    Else
                        Matid = Id

                    End If
                Else
                    MatID = "1"
                End If


                'For One Year
                If a.Checked = True Then
                    lblyear.Text = "<b>Salary  - 1 Year Historical</b>"
                    YearValue = 1
                End If

                'For Three Year
                If b.Checked = True Then
                    YearValue = 3
                    lblyear.Text = "<b>Salary - 3 Year Historical</b>"

                End If

                'For Five Year
                If c.Checked = True Then
                    lblyear.Text = "<b>Salary - 5 Year Historical</b>"
                    YearValue = 5
                End If


                If a.Checked = False And b.Checked = False And c.Checked = False Then
                    c.Checked = True
                    YearValue = 5
                    lblyear.Text = "<b>Salary - 5 Year Historical</b>"
                End If



                Dim MyConnection As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
                Dim StrSQl As String = ""


                Dim Postion1 As String = ""
                Dim Postion2 As String = ""
                Dim MatStr1 As New Integer
                Dim MatStr2 As New Integer
                Dim mat1 As String = ""



                Dim StrSql1 As String = ""
                Dim StrSql2 As String = ""
                Dim StrSql3 As String = ""
                Dim StrSql4 As String = ""
                Dim M As New Integer
                Dim N As New Integer

                Dim StrSqlCountry As String = ""
                Dim StrSqlCountry1 As String = ""
                Dim StrSqlPostion1 As String = ""
                Dim StrSqlPostion2 As String = ""
                Dim StrSqlPref As String
                Dim CountryId As String = ""
                Dim CountryId1 As String = ""
                Dim CountryDetail1() As String
                Dim CountryDetail2() As String

                Dim Dt1 As New DataSet()
                Dim Dt2 As New DataSet()
                Dim Dt3 As New DataSet()
                Dim Dt4 As New DataSet()
                Dim DtsPref As New DataSet()

                StrSqlCountry = "Select CountryId,(CountryDE1||''||CountryDE2)As CountryDes From Country  order by CountryDes"
                StrSqlCountry1 = "Select CountryId,(CountryDE1||''||CountryDE2)As CountryDes From Country  order by CountryDes"


                Dt1 = odbutil.FillDataSet(StrSqlCountry, MyConnection)



                If Not IsPostBack Then

                    'Country Selection Combo
                    CountryDropDown1.DataSource = Dt1
                    CountryDropDown1.DataValueField = "CountryId"
                    CountryDropDown1.DataTextField = "CountryDes"
                    CountryDropDown1.SelectedValue = 0
                    CountryDropDown1.DataBind()

                    CountryDropDown2.DataSource = Dt1
                    CountryDropDown2.DataValueField = "CountryId"
                    CountryDropDown2.DataTextField = "CountryDes"
                    CountryDropDown2.SelectedValue = 0
                    CountryDropDown2.DataBind()


                End If

                CountryId = CountryDropDown1.SelectedValue
                CountryId1 = CountryDropDown2.SelectedValue
                CountryDetail1 = GetCountry(CountryId)
                CountryDetail2 = GetCountry(CountryId1)



                StrSqlPostion1 = "Select Persid,(Persde1||''||Persde2)As Des from " + CountryDetail1(0).ToString() + " Order by Des"
                StrSqlPostion2 = "Select Persid,(Persde1||''||Persde2)As Des from " + CountryDetail2(0).ToString() + "  Order by Des "
                StrSqlPref = "SELECT TITLE1,TITLE2,TITLE3,TITLE4 FROM  CHARTPREFERENCES WHERE USERID='" + Session("USERID").ToString().ToUpper() + "'"



                Dt3 = odbutil.FillDataSet(StrSqlPostion1, MyConnection)
                Dt4 = odbutil.FillDataSet(StrSqlPostion2, MyConnection)
                DtsPref = odbutil.FillDataSet(StrSqlPref, MyConnection)




                If Not IsPostBack Then
                    'Material Selection Combo
                    SPC1.DataSource = Dt3
                    SPC1.DataValueField = "Persid"
                    SPC1.DataTextField = "Des"
                    SPC1.SelectedValue = 1
                    SPC1.DataBind()

                    'Material Comparison Combo
                    SPC2.DataSource = Dt4
                    SPC2.DataValueField = "Persid"
                    SPC2.DataTextField = "Des"
                    SPC2.SelectedValue = 2
                    SPC2.DataBind()
                End If

                'Comparison of two materials
                Dim NewMatID As String = SPC2.SelectedValue
                If NewMatID = "" Then
                    NewMatID = 0
                End If

                Dim ComboStr1 As String = SPC1.SelectedItem.Text
                Dim Str1 As New Integer
                Postion1 = SPC1.SelectedItem.Text.ToString()
                Postion2 = SPC2.SelectedItem.Text.ToString()
                mat.Text = Postion1 + "  &   " + Postion2
                Country.Text = CountryDropDown1.SelectedItem.Text.ToString() + "  &   " + CountryDropDown2.SelectedItem.Text.ToString()


                If NewMatID = 0 Then
                    'NewMatID = MatID
                    Str1 = ComboStr1.IndexOf(":")
                    ComboStr1 = ComboStr1.Substring(Str1 + 1, ComboStr1.Length - Str1 - 1)
                    'mat.Text = ComboStr1
                Else
                    NewMatID = NewMatID
                    MatStr1 = Postion1.IndexOf(":")
                    MatStr2 = Postion2.IndexOf(":")
                    Postion1 = Postion1.Substring(MatStr1 + 1, Postion1.Length - MatStr1 - 1)
                    Postion2 = Postion2.Substring(MatStr2 + 1, Postion2.Length - MatStr2 - 1)
                    mat.Text = Postion1 + "  &   " + Postion2
                End If


                'StrSQl = "SELECT  TO_CHAR(A.EFFDATE,'MON-yyyy')AS EFFDATE,  "
                'StrSQl = StrSQl + "(A.SALARY*CHARTPREFERENCES.CURR/CHARTPREFERENCES.CONVERSIONFACTOR) ID1, "
                'StrSQl = StrSQl + "(B.SALARY*CHARTPREFERENCES.CURR/CHARTPREFERENCES.CONVERSIONFACTOR) ID2 "
                'StrSQl = StrSQl + "FROM " + CountryDetail1(1).ToString() + "  A, "
                'StrSQl = StrSQl + "" + CountryDetail2(1).ToString() + " B, "
                'StrSQl = StrSQl + "CHARTPREFERENCES "
                'StrSQl = StrSQl + "WHERE A.EFFDATE = B.EFFDATE "
                'StrSQl = StrSQl + "AND A.PERSID = " + MatID + ""
                'StrSQl = StrSQl + "AND B.PERSID = " + NewMatID + ""
                'StrSQl = StrSQl + "AND A.EFFDATE BETWEEN TO_DATE(ADD_MONTHS(TRUNC(SYSDATE, 'MONTH'),-12*" + YearValue + ")) "
                'StrSQl = StrSQl + "AND TO_DATE(ADD_MONTHS(TRUNC(SYSDATE, 'MONTH'),-0)) "
                'StrSQl = StrSQl + "AND CHARTPREFERENCES.USERNAME='" + Session("User").ToString() + "' "


                StrSQl = "SELECT  TO_CHAR(A.EFFDATE,'MON-yyyy')AS EFFDATE,  "
                StrSQl = StrSQl + "(A.SALARY*CURR.CURPUSD) ID1, "
                StrSQl = StrSQl + "(B.SALARY*CURR.CURPUSD) ID2 "
                StrSQl = StrSQl + "FROM " + CountryDetail1(1).ToString() + "  A, "
                StrSQl = StrSQl + "" + CountryDetail2(1).ToString() + " B, "
                StrSQl = StrSQl + "CHARTPREFERENCES, "
                StrSQl = StrSQl + "CURRENCYARCH CURR "
                StrSQl = StrSQl + "WHERE A.EFFDATE = B.EFFDATE "
                StrSQl = StrSQl + "AND A.PERSID = " + MatID + ""
                StrSQl = StrSQl + "AND B.PERSID = " + NewMatID + ""
                StrSQl = StrSQl + "AND CURR.EFFDATE = (CASE WHEN CHARTPREFERENCES.CURRENCY = 0 THEN "
                StrSQl = StrSQl + "(TO_DATE('01/01/1900','MM/DD/YYYY')) "
                StrSQl = StrSQl + "ELSE "
                StrSQl = StrSQl + "(A.EFFDATE) "
                StrSQl = StrSQl + "END "
                StrSQl = StrSQl + ") "
                StrSQl = StrSQl + "AND CURR.CURID= CHARTPREFERENCES.CURRENCY "
                StrSQl = StrSQl + "AND A.EFFDATE BETWEEN TO_DATE(ADD_MONTHS(TRUNC(SYSDATE, 'MONTH'),-12*" + YearValue + ")) "
                StrSQl = StrSQl + "AND TO_DATE(ADD_MONTHS(TRUNC(SYSDATE, 'MONTH'),-0)) "
                StrSQl = StrSQl + "AND UPPER(CHARTPREFERENCES.USERNAME)='" + Session("Echem1UserName").ToString() + "' "








                Dim Dts As New DataTable()
                Dts = odbutil.FillDataTable(StrSQl, MyConnection)
                Dim Count As String = Dts.Rows.Count

                Dim lbl As String = ""

                'Specify Graph Specific PCScript   
                'If CountryDetail1(1) <> CountryDetail1(1) Then
                pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" & Postion1.ToString() & ";" & Postion2.ToString() & ")"
                Dim i As New Integer
                For i = 0 To Count - 1
                    pcScript &= "" + Graphtype + ".setSeries(" & Dts.Rows(i).Item(0) & ";" & Dts.Rows(i).Item(1) & ";" & Dts.Rows(i).Item(2) & ")"
                Next
                '    Else
                '    ' pcScript &= "Y-axis.SetText(" + lbl + ")"
                '    pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" & mat.Text.ToString() & ")"
                '    Dim i As New Integer
                '    For i = 0 To Count - 1
                '        pcScript &= "" + Graphtype + ".setSeries(" & Dts.Rows(i).Item(0) & ";" & Dts.Rows(i).Item(1) & ")"
                '    Next

                'End If




                'Code for Image Template
                Dim MyConfigConnection As String = String.Empty
                MyConfigConnection = System.Configuration.ConfigurationManager.AppSettings("ConfigurationConnectionString")

                Dim dsChartSetting As New DataTable
                Dim StrSqlChartSetting As String = String.Empty
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
                myImage.pcScript = pcScript + "Y-axis.SetText(" + DtsPref.Tables(0).Rows(0).Item("TITLE2").ToString() + ")"
                myImage.outputType = "JPEG"
                control.InnerHtml = myImage.getEmbeddingHTML


            End If








        Catch ex As Exception
            Response.Write("Error:" + ex.Message.ToString())


        End Try


    End Sub

    Protected Function GetCountry(ByVal CountryID As String) As String()
        Dim CountryDetails(2) As String
        Try
            Dim CountryName As String = ""
            Dim CountryArchTable As String = ""

            If CountryID = 0 Then
                CountryName = "Personnel"
                CountryArchTable = "PERSARCHUS"
            End If
            If CountryID = 1 Then
                CountryName = "PersonnelChina"
                CountryArchTable = "PERSARCHCHINA"
            End If
            If CountryID = 2 Then
                CountryName = "PersonnelUk"
                CountryArchTable = "PERSARCHUK"
            End If
            If CountryID = 3 Then
                CountryName = "PersonnelGermany"
                CountryArchTable = "PERSARCHGERMANY"
            End If
            If CountryID = 4 Then
                CountryName = "PersonnelSkorea"
                CountryArchTable = "PERSARCHSKOREA"
            End If
            CountryDetails(0) = CountryName
            CountryDetails(1) = CountryArchTable



        Catch ex As Exception

        End Try
        Return CountryDetails
    End Function

End Class
