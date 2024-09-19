Imports Corda
Imports System.Web.UI.HtmlControls
Imports System.Data
Imports System.Data.OleDb
Imports System.Web.HttpBrowserCapabilities
Partial Class Pages_SavvyPackPro_Charts_ChartReport
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
    Dim objGetdata As New SavvyProGetData
    Dim objUpIns As New SavvyProUpInsData
    Dim hidPriceColId As String
    Dim hidGroupId As String
    Dim ddlChartType As String
    Dim hidPriceId As String
    Dim groupdes As String
    Dim hidvendors As String
    Dim hidpricedes As String
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim DsChart As DataSet
        Dim chartname As DataSet
        hidtype.Value = Request.QueryString("type").ToString()
        If hidtype.Value = "USER" Then
            DsChart = objGetdata.GetUserChartByID(Session("CRptId"))
            hidPriceColId = DsChart.Tables(0).Rows(0).Item("PRICECOLID").ToString()
            ddlChartType = DsChart.Tables(0).Rows(0).Item("CHARTTYPE").ToString()
            hidPriceId = DsChart.Tables(0).Rows(0).Item("RFPPRICEID").ToString()
            hidvendors = DsChart.Tables(0).Rows(0).Item("VENDORID").ToString()
            chartname = objGetdata.GetRFPPriceDes(hidPriceId.ToString())
            mat.Text = chartname.Tables(0).Rows(0).Item("PRICEDETAIL").ToString()
            hidpricedes = mat.Text
            If ddlChartType = "LINEGRP" Then
                GenerateGraphLine()
            ElseIf ddlChartType = "LINEVENDOR" Then
                GenerateGraphLineGrp()
            Else
                groupdes = DsChart.Tables(0).Rows(0).Item("CODE").ToString()
                hidGroupId = DsChart.Tables(0).Rows(0).Item("CODE").ToString()
                GetChart(DsChart.Tables(0).Rows(0).Item("VENDORID").ToString(), DsChart.Tables(0).Rows(0).Item("code").ToString())
            End If
            lblHeadingS.Text = ""
        ElseIf hidtype.Value = "BATCH" Then
            DsChart = objGetdata.GetBatchChartByID(Session("CRptId"))
            'hidPriceColId = DsChart.Tables(0).Rows(0).Item("PRICECOLID").ToString()
            'ddlChartType = DsChart.Tables(0).Rows(0).Item("CHARTTYPE").ToString()
            hidPriceId = DsChart.Tables(0).Rows(0).Item("RFPPRICEID").ToString()
            hidvendors = DsChart.Tables(0).Rows(0).Item("VENDORID").ToString()
            chartname = objGetdata.GetRFPPriceDes(hidPriceId.ToString())
            mat.Text = chartname.Tables(0).Rows(0).Item("PRICEDETAIL").ToString()
            hidpricedes = mat.Text
            hidGroupId = DsChart.Tables(0).Rows(0).Item("OTHERPREFRFPID").ToString()
            GenerateGraphLineBatch()

            groupdes = DsChart.Tables(0).Rows(0).Item("CODE").ToString()
            hidGroupId = DsChart.Tables(0).Rows(0).Item("CODE").ToString()
            lblHeadingS.Text = groupdes.ToString()

        End If

    End Sub
    Protected Sub GenerateGraphLineGrp()
        Dim pcScript = ""
        Dim odbutil As New DBUtil()
        Dim Graphtype As String = ""
        Dim GraphName As String = ""

        Dim UserName As String = ""
        UserName = Session("UserName")
        Graphtype = "Material_Price"
        GraphName = "MaterialPrice1"
        Dim Count As Integer

        Dim conwt As String = ""
        Dim unit As String = ""
        Dim curr As String = ""

        Dim arrVendor() As String = hidvendors.ToString().Split(",")
        Dim dsprice As DataSet
        Dim dsgroup As DataSet
        Dim dsvendor As DataSet
        Dim dsM As DataSet
        Dim flag As Integer
        MaterialPrice.Visible = True
            dsvendor = objGetdata.GetVendorDes(hidvendors.ToString())
        dsM = objGetdata.GetMasterDataN(hidPriceId)
        For i = 0 To dsM.Tables(0).Rows.Count - 1
            If dsM.Tables(0).Rows(i).Item("MTYPEID") = "5" Then
                flag = 0
            ElseIf dsM.Tables(0).Rows(i).Item("MTYPEID") = "1" Then

                flag = 1
            End If
        Next
        If flag = 0 Then
            dsprice = objGetdata.GetPriceByRFPSKU(hidPriceColId, "", hidvendors, hidPriceId, "SKU")
            dsgroup = objGetdata.GetPriceByRFPSKU(hidPriceColId, "", hidvendors, hidPriceId, "SKU")
        Else
            dsprice = objGetdata.GetPriceByRFPLine(hidPriceColId, hidvendors, hidPriceId)
            dsgroup = objGetdata.GetPRICEDATARFPN(hidPriceId, Session("UserId"), Session("RFPID"))

        End If

        Dim arrvendordes(dsvendor.Tables(0).Rows.Count) As String

        For k As Integer = 0 To dsvendor.Tables(0).Rows.Count - 1
            arrvendordes(k) = dsvendor.Tables(0).Rows(k).Item("EMAILADDRESS").ToString()
        Next

        Count = dsgroup.Tables(0).Rows.Count
        Dim Group(Count) As String
        Dim Groupid(Count) As String
        For i = 0 To Count - 1
            For j = 0 To dsM.Tables(0).Rows.Count - 1
                If dsM.Tables(0).Rows(j).Item("MTYPEID") = "1" Then
                    Group(i) = Group(i) + "#" + dsgroup.Tables(0).Rows(i).Item(dsM.Tables(0).Rows(j).Item("COLMNS").ToString()).ToString()
                End If
                If dsM.Tables(0).Rows(j).Item("MTYPEID") = "5" Then
                    Group(i) = "#" + dsgroup.Tables(0).Rows(i).Item("DETAILS").ToString()
                End If
            Next
            Group(i) = Group(i).Remove(0, 1)
          
        Next
        Dim strTit(20) As String
        Dim price As New Double
        Dim dvC As New DataView
        Dim dtC As New DataTable
      
        Dim strvendors As String = ""
        If dsprice.Tables(0).Rows.Count > 0 Then
          
            Dim strgroup As String = ""
            For k = 0 To Count - 1
                Dim strg As String = Group(k).ToString()
                strgroup = strgroup + strg.Replace(",", "") + ";"
            Next
            strgroup = strgroup.Remove(strgroup.Length - 1)

            pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + strgroup + ")"

            Dim i As New Integer
            For j = 0 To arrVendor.Length - 1              
                pcScript &= "" + Graphtype + ".setSeries(" & dsvendor.Tables(0).Rows(j).Item("EMAILADDRESS").ToString() & ""
                For i = 0 To Count - 1

                    dvC = dsprice.Tables(0).DefaultView
                    If flag = 0 Then
                        dvC.RowFilter = "SKUID='" + dsgroup.Tables(0).Rows(i).Item("SKUID").ToString() + "' and vendorid='" + dsvendor.Tables(0).Rows(j).Item("VENDORID").ToString() + "' "
                    Else
                        dvC.RowFilter = "CODE='" + Group(i).ToString() + "' and vendorid='" + dsvendor.Tables(0).Rows(j).Item("VENDORID").ToString() + "' "
                    End If

                    'dvC.RowFilter = "CODE='" + Group(i).ToString() + "' and vendorid='" + arrVendor(j).ToString() + "' "
                    dtC = dvC.ToTable()
                    If dtC.Rows.Count > 0 Then
                        If dtC.Rows(0).Item("PRICE").ToString() <> "" Then
                            price = FormatNumber(dtC.Rows(0).Item("Price"), 4)
                            pcScript &= ";" & price & ""
                        Else
                            pcScript &= " ; "
                        End If
                    Else
                        pcScript &= " ; "
                    End If

                Next
                pcScript &= ")"

            Next
        End If
        MaterialPrice.Visible = True
        Dim dsChartSetting As New DataTable
        Dim objGetData1 As New Configration.Selectdata()
        dsChartSetting = objGetData1.GetChartSettings().Tables(0)

        Dim myImage As CordaEmbedder = New CordaEmbedder()
        myImage.externalServerAddress = dsChartSetting.Rows(0)("EXTSERVERADD").ToString()
        myImage.internalCommPortAddress = dsChartSetting.Rows(0)("INTCOMPORTADD").ToString()
        myImage.imageTemplate = GraphName + ".itxml"
        myImage.userAgent = Request.UserAgent
        myImage.width = 1000
        myImage.height = 600
        myImage.returnDescriptiveLink = False
        myImage.language = "EN"

        myImage.pcScript = pcScript + "Y-axis.SetText(" + hidpricedes + ")"
        myImage.outputType = "FLASH"
        myImage.fallback = "STRICT"
        MaterialPrice.InnerHtml = myImage.getEmbeddingHTML
    End Sub
    Protected Sub GetChart(ByVal Vendors As String, ByVal Groups As String)
        Try
            Dim Data As String = String.Empty
            Dim Text As String = String.Empty
            Dim Pref As String = String.Empty
            Dim pcScript As String = String.Empty
            Dim Graphtype As String = String.Empty
            Dim GraphName As String = String.Empty
            Dim VariableMargin As New Decimal
            Dim Revenue As New Decimal
            Dim PlantMargin As New Decimal
            Dim Cnt As New Integer
            Dim i As New Integer

            Dim matCost As New Decimal
            Dim labCost As New Decimal
            Dim ergyCost As New Decimal
            Dim DPCost As New Decimal
            Dim ShipCost As New Decimal
            Dim fixedCost As New Decimal
            Dim plantM As New Decimal
            Dim arrVendor() As String = Vendors.Split(",")
            Dim dsprice As DataSet
            Dim dsvendor As DataSet
            Dim dsM As DataSet
            Dim flag As Integer

            ' dsprice = objGetdata.GetPriceByRFP(hidPriceColId, hidGroupId, Vendors, hidPriceId)
            'dsvendor = objGetdata.GetVendorDesNotExists(Vendors, hidPriceColId, hidGroupId)
            dsM = objGetdata.GetMasterDataN(hidPriceId)
            For i = 0 To dsM.Tables(0).Rows.Count - 1
                If dsM.Tables(0).Rows(i).Item("MTYPEID") = "5" Then
                    flag = 0
                ElseIf dsM.Tables(0).Rows(i).Item("MTYPEID") = "1" Then

                    flag = 1
                End If
            Next
            If flag = 0 Then
                dsprice = objGetdata.GetPriceByRFPSKU(hidPriceColId, hidGroupId, Vendors, hidPriceId, "BAR")
                dsvendor = objGetdata.GetVendorDesNotExistsForSKU(Vendors, hidPriceColId, hidGroupId)
            Else
                dsprice = objGetdata.GetPriceByRFP(hidPriceColId, groupdes, Vendors, hidPriceId)
                dsvendor = objGetdata.GetVendorDesNotExists(Vendors, hidPriceColId, groupdes)
            End If
            If dsprice.Tables(0).Rows.Count > 0 Then
                If ddlChartType = "RBC" Then
                    GraphName = "Sargento_KraftPouchvsTray"
                    Graphtype = "graph"
                    Cnt = arrVendor.Length
                    Pref = groupdes
                    pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + hidpricedes + ")"
                    For i = 0 To dsprice.Tables(0).Rows.Count - 1

                        Data = FormatNumber(dsprice.Tables(0).Rows(i).Item("PRICE").ToString(), 0)
                        pcScript &= "" + Graphtype + ".setSeries(" & dsprice.Tables(0).Rows(i).Item("EMAILADDRESS") & ";" & Data & ")"

                    Next
                    If Cnt > dsprice.Tables(0).Rows.Count Then
                        For i = 0 To dsvendor.Tables(0).Rows.Count - 1 'dsprice.Tables(0).Rows.Count To Cnt - 1

                            Data = 0.0
                            pcScript &= "" + Graphtype + ".setSeries(" + dsvendor.Tables(0).Rows(i).Item("EMAILADDRESS") & ";" & Data & ")"
                        Next
                    End If
                ElseIf ddlChartType = "PIE" Then
                    GraphName = "Sargento_KraftPouchvsTray"
                    Graphtype = "graph"
                    Cnt = arrVendor.Length
                    Pref = hidGroupId
                    pcScript &= Graphtype + ".setCategories(" + hidpricedes + ")"
                    For i = 0 To dsprice.Tables(0).Rows.Count - 1
                        Data = FormatNumber(dsprice.Tables(0).Rows(i).Item("PRICE").ToString(), 0)
                        pcScript &= "" + Graphtype + ".setSeries(" + dsprice.Tables(0).Rows(i).Item("EMAILADDRESS") & ";" & Data & ")"
                    Next
                    If Cnt > dsprice.Tables(0).Rows.Count Then
                        For i = 0 To dsvendor.Tables(0).Rows.Count - 1 'dsprice.Tables(0).Rows.Count To Cnt - 1

                            Data = 0.0
                            pcScript &= "" + Graphtype + ".setSeries(" + dsvendor.Tables(0).Rows(i).Item("EMAILADDRESS") & ";" & Data & ")"
                        Next
                    End If
                End If
                GenrateChart(pcScript, GraphName, Pref)

            End If

        Catch ex As Exception
            Response.Write("Error:GetChart:" + ex.Message.ToString())
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
            If ddlChartType = "PIE" Then
                myImage.imageTemplate = "Sargento_KraftPouchvsTray_Html4" + ".itxml"
            ElseIf ddlChartType = "SBAR" Then
                myImage.imageTemplate = "Sargento_KraftPouchvsTray6.itxml"
            Else
                myImage.imageTemplate = "Sargento_KraftPouchvsTray_Html" + ".itxml"
            End If

            myImage.userAgent = Request.UserAgent
            myImage.width = 700
            myImage.height = 400
            myImage.returnDescriptiveLink = False
            myImage.language = "EN"
            myImage.pcScript = PcScript + "Y-axis.SetText(" + Pref + ")"
            myImage.outputType = "JPEG"
            myImage.fallback = "STRICT"

            MaterialPrice.InnerHtml = myImage.getEmbeddingHTML

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub GenerateGraphLine()
        Dim pcScript = ""
        Dim odbutil As New DBUtil()
        Dim Graphtype As String = ""
        Dim GraphName As String = ""

        Dim UserName As String = ""
        UserName = Session("UserName")
        Graphtype = "Material_Price"
        GraphName = "MaterialPrice1"
        Dim Count As Integer

        Dim conwt As String = ""
        Dim unit As String = ""
        Dim curr As String = ""

        Dim arrVendor() As String = hidvendors.ToString().Split(",")
        Dim dsprice As DataSet
        Dim dsgroup As DataSet
        Dim dsvendor As DataSet
        Dim dspricedes As DataSet
        Dim dsM As DataSet
        Dim flag As Integer
        dspricedes = objGetdata.GetRFPPriceDes(hidPriceId)
        hidpricedes = dspricedes.Tables(0).Rows(0).Item("PRICEDETAIL").ToString()
        dsvendor = objGetdata.GetVendorDes(hidvendors.ToString())
        Dim arrvendordes(dsvendor.Tables(0).Rows.Count - 1) As String
        For j = 0 To dsvendor.Tables(0).Rows.Count - 1
            arrvendordes(j) = dsvendor.Tables(0).Rows(j).Item("EMAILADDRESS").ToString()
        Next
        'dsprice = objGetdata.GetPriceByRFPLine(hidPriceColId, hidvendors, hidPriceId)
        'dsgroup = objGetdata.GetPRICEDATARFPN(hidPriceId, Session("UserId"), Session("RFPID"))
        dsM = objGetdata.GetMasterDataN(hidPriceId)
        For i = 0 To dsM.Tables(0).Rows.Count - 1
            If dsM.Tables(0).Rows(i).Item("MTYPEID") = "5" Then
                flag = 0
            ElseIf dsM.Tables(0).Rows(i).Item("MTYPEID") = "1" Then

                flag = 1
            End If
        Next
        If flag = 0 Then
            dsprice = objGetdata.GetPriceByRFPSKU(hidPriceColId, "", hidvendors, hidPriceId, "SKU")
            dsgroup = objGetdata.GetPriceByRFPSKU(hidPriceColId, "", hidvendors, hidPriceId, "SKU")
        Else
            dsprice = objGetdata.GetPriceByRFPLine(hidPriceColId, hidvendors, hidPriceId)
            dsgroup = objGetdata.GetPRICEDATARFPN(hidPriceId, Session("UserId"), Session("RFPID"))

        End If
        Count = dsgroup.Tables(0).Rows.Count
        Dim Group(Count) As String
        Dim Groupid(Count) As String
        For i = 0 To Count - 1
            For j = 0 To dsM.Tables(0).Rows.Count - 1
                If dsM.Tables(0).Rows(j).Item("MTYPEID") = "1" Then
                    Group(i) = Group(i) + "#" + dsgroup.Tables(0).Rows(i).Item(dsM.Tables(0).Rows(j).Item("COLMNS").ToString()).ToString()
                End If
                If dsM.Tables(0).Rows(j).Item("MTYPEID") = "5" Then
                    Group(i) = "#" + dsgroup.Tables(0).Rows(i).Item("DETAILS").ToString()
                End If
            Next
            Group(i) = Group(i).Remove(0, 1)
            ' Group(i) = dsgroup.Tables(0).Rows(i).Item("format1").ToString() + "#" + dsgroup.Tables(0).Rows(i).Item("finition1").ToString() + "#" + dsgroup.Tables(0).Rows(i).Item("width").ToString()
            'Groupid(i) = dsgroup.Tables(0).Rows(i).Item("FORMGROUPID").ToString()

        Next
        Dim strTit(20) As String
        Dim price As New Double
        Dim dvC As New DataView
        Dim dtC As New DataTable

        Dim strvendors As String = ""
        If dsprice.Tables(0).Rows.Count > 0 Then
            For y As Integer = 0 To dsvendor.Tables(0).Rows.Count - 1
                Dim strv As String = dsvendor.Tables(0).Rows(y).Item("EMAILADDRESS").ToString()
                strvendors = strvendors + strv.Replace(",", "") + ";"
            Next
            strvendors = strvendors.Remove(strvendors.Length - 1)
            pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + strvendors + ")"

            Dim i As New Integer
            For i = 0 To Count - 1
                pcScript &= "" + Graphtype + ".setSeries(" & Group(i) & ""
                For j = 0 To arrVendor.Length - 1
                    dvC = dsprice.Tables(0).DefaultView
                    'dvC.RowFilter = "CODE='" + Group(i).ToString() + "' and vendorid='" + dsvendor.Tables(0).Rows(j).Item("VENDORID").ToString() + "' "
                    If flag = 0 Then
                        dvC.RowFilter = "SKUID='" + dsgroup.Tables(0).Rows(i).Item("SKUID").ToString() + "' and vendorid='" + dsvendor.Tables(0).Rows(j).Item("VENDORID").ToString() + "' "
                    Else
                        dvC.RowFilter = "CODE='" + Group(i).ToString() + "' and vendorid='" + dsvendor.Tables(0).Rows(j).Item("VENDORID").ToString() + "' "
                    End If

                    dtC = dvC.ToTable()
                    If dtC.Rows.Count > 0 Then
                        If dtC.Rows(0).Item("PRICE").ToString() <> "" Then
                            price = FormatNumber(dtC.Rows(0).Item("Price"), 4)
                            pcScript &= ";" & price & ""
                        Else
                            pcScript &= " ; "
                        End If
                    Else
                        pcScript &= " ; "
                    End If


                Next
                pcScript &= ")"

            Next
        End If
        Dim dsChartSetting As New DataTable
        Dim objGetData1 As New Configration.Selectdata()
        dsChartSetting = objGetData1.GetChartSettings().Tables(0)

        Dim myImage As CordaEmbedder = New CordaEmbedder()
        myImage.externalServerAddress = dsChartSetting.Rows(0)("EXTSERVERADD").ToString()
        myImage.internalCommPortAddress = dsChartSetting.Rows(0)("INTCOMPORTADD").ToString()
        myImage.imageTemplate = GraphName + ".itxml"
        myImage.userAgent = Request.UserAgent
        myImage.width = 700
        myImage.height = 400
        myImage.returnDescriptiveLink = False
        myImage.language = "EN"
        myImage.pcScript = pcScript + "Y-axis.SetText(" + hidpricedes + ")"
        myImage.outputType = "FLASH"
        myImage.fallback = "STRICT"
        MaterialPrice.InnerHtml = myImage.getEmbeddingHTML
    End Sub
    Protected Sub GenerateGraphLineBatch()
        Dim pcScript = ""
        Dim odbutil As New DBUtil()
        Dim Graphtype As String = ""
        Dim GraphName As String = ""
        InitializeComponent()

        Dim UserName As String = ""
        UserName = Session("UserName")
        Graphtype = "Material_Price"
        GraphName = "MaterialPrice1_NEW"
        Dim Count As Integer

        Dim conwt As String = ""
        Dim unit As String = ""
        Dim curr As String = ""
        Dim dsvendor As DataSet

        dsvendor = objGetdata.GetVendorDes(hidvendors)

        ' dsvendor = objGetdata.GetVendorListByUserID("", Session("UserId"))
        ' dsvendor = objGetdata.GetNoOfVendorBatchByRFPIDPO(Session("hidRfpID"), hidPriceId.Value)
        Dim strvendor As String = ""
        Dim dsprice As DataSet
        Dim dsgroup As DataSet
        For i As Integer = 0 To dsvendor.Tables(0).Rows.Count - 1
            strvendor = dsvendor.Tables(0).Rows(i).Item("VENDORID").ToString() + "," + strvendor
        Next

        dsprice = objGetdata.GetPriceByBatchLine(hidGroupId, strvendor.ToString().Remove(strvendor.Length - 1))
        dsgroup = objGetdata.GetGrpByBatchLine()

        Count = dsgroup.Tables(0).Rows.Count
        Dim Group(Count) As String
        Dim Groupid(Count) As String
        For i = 0 To Count - 1
            Group(i) = dsgroup.Tables(0).Rows(i).Item("BATCHVALUE").ToString()
        Next
        Dim strTit(20) As String
        Dim price As New Double
        Dim dvC As New DataView
        Dim dtC As New DataTable
        Dim strvendors As String = ""
        If dsprice.Tables(0).Rows.Count > 0 Then
            ' lbl.Visible = True
            'lbl.Text = groupdes + "<br>"
            If dsvendor.Tables(0).Rows.Count > 0 Then
                For m As Integer = 0 To dsvendor.Tables(0).Rows.Count - 1
                    Dim strv As String = dsvendor.Tables(0).Rows(m).Item("FIRSTNAME").ToString()
                    strvendors = strvendors + strv.Replace(",", "") + ";"
                Next
            End If
            strvendors = strvendors.Remove(strvendors.Length - 1)
            pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + strvendors + ")"
            ' pcScript &= "" + Graphtype + ".addHoverText(" + strTit(x - 1) + "," & effdate1(i) & ",PRICE: " & FormatNumber(price, 4).Replace(",", "") & "\n Date: " & effdate1(i) & "\n Material: " & strTit(x - 1) + ")"
            Dim strHov As String = ""
            Dim i As New Integer
            For i = 0 To Count - 1
                pcScript &= "" + Graphtype + ".setSeries(" & Group(i) & ";"
                For j = 0 To dsvendor.Tables(0).Rows.Count - 1
                    dvC = dsprice.Tables(0).DefaultView
                    dvC.RowFilter = "BATCHVALUE='" + Group(i).ToString() + "' and FIRSTNAME='" + dsvendor.Tables(0).Rows(j).Item("FIRSTNAME").ToString() + "' "
                    dtC = dvC.ToTable()
                    If dtC.Rows.Count > 0 Then
                        ' strHov = dsvendor.Tables(0).Rows(j).Item("FIRSTNAME").ToString() + ", " + Group(i).ToString() + ",Vendor: " + dsvendor.Tables(0).Rows(j).Item("FIRSTNAME").ToString() + "\nBatch Value :" + Group(i).ToString() + "\nprice : " + dtC.Rows(0).Item("PRICE").ToString()
                        If dtC.Rows(0).Item("PRICE").ToString() <> "" Then
                            price = FormatNumber(dtC.Rows(0).Item("Price"), 4)
                            pcScript &= "(STYP_13)" & price & ";"
                        End If
                    Else
                        pcScript &= "(STYP_13);"
                    End If
                Next
                pcScript &= ")"
                'pcScript &= "" + Graphtype + ".addHoverText(" + strHov + ")"
                For j = 0 To dsvendor.Tables(0).Rows.Count - 1
                    dvC = dsprice.Tables(0).DefaultView
                    dvC.RowFilter = "BATCHVALUE='" + Group(i).ToString() + "' and FIRSTNAME='" + dsvendor.Tables(0).Rows(j).Item("FIRSTNAME").ToString() + "' "
                    dtC = dvC.ToTable()

                    If dtC.Rows.Count > 0 Then
                        strHov = dsvendor.Tables(0).Rows(j).Item("FIRSTNAME").ToString() + ", " + Group(i).ToString() + ",Vendor: " + dsvendor.Tables(0).Rows(j).Item("FIRSTNAME").ToString() + "\nBatch Value :" + Group(i).ToString() + "\nprice : " + dtC.Rows(0).Item("PRICE").ToString()
                        If dtC.Rows(0).Item("PRICE").ToString() <> "" Then

                        End If
                    Else
                        strHov = dsvendor.Tables(0).Rows(j).Item("FIRSTNAME").ToString() + ", " + Group(i).ToString() + ",Vendor: " + dsvendor.Tables(0).Rows(j).Item("FIRSTNAME").ToString() + "\nBatch Value :" + Group(i).ToString() + "\nprice : "
                    End If
                    pcScript &= "" + Graphtype + ".addHoverText(" + strHov + ")"

                Next
            Next
            MaterialPrice.Visible = True

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
            myImage.width = 800
            myImage.height = 400
            myImage.returnDescriptiveLink = True
            myImage.language = "EN"
            myImage.pcScript = pcScript + "Y-axis.SetText()"
            myImage.outputType = "FLASH"
            myImage.fallback = "STRICT"

            MaterialPrice.InnerHtml = myImage.getEmbeddingHTML
        End If

    End Sub
    'Protected Sub GenerateGraphLineBatch()
    '    Dim pcScript = ""
    '    Dim odbutil As New DBUtil()
    '    Dim Graphtype As String = ""
    '    Dim GraphName As String = ""

    '    Dim UserName As String = ""
    '    UserName = Session("UserName")
    '    Graphtype = "Material_Price"
    '    GraphName = "MaterialPrice1"
    '    Dim Count As Integer

    '    Dim conwt As String = ""
    '    Dim unit As String = ""
    '    Dim curr As String = ""

    '    Dim arrVendor() As String = hidvendors.ToString().Split(",")
    '    Dim arrvendordes() As String = hidvendors.ToString().Split(",")
    '    Dim dsprice As DataSet
    '    Dim dsgroup As DataSet
    '    MaterialPrice.Visible = True

    '    dsprice = objGetdata.GetPriceByBatchLine(hidGroupId, hidvendors)

    '    dsgroup = objGetdata.GetGrpByBatchLine()

    '    Count = dsgroup.Tables(0).Rows.Count
    '    Dim Group(Count) As String
    '    Dim Groupid(Count) As String
    '    For i = 0 To Count - 1
    '        Group(i) = dsgroup.Tables(0).Rows(i).Item("BATCHVALUE").ToString()

    '    Next
    '    Dim strTit(20) As String
    '    Dim price As New Double
    '    Dim dvC As New DataView
    '    Dim dtC As New DataTable

    '    Dim dsvendor As DataSet
    '    dsvendor = objGetdata.GetVendorDes(hidvendors.ToString())
    '    Dim strvendors As String = ""
    '    If dsprice.Tables(0).Rows.Count > 0 Then

    '        If dsvendor.Tables(0).Rows.Count > 0 Then
    '            For m As Integer = 0 To dsvendor.Tables(0).Rows.Count - 1
    '                Dim strv As String = dsvendor.Tables(0).Rows(m).Item("FIRSTNAME").ToString()
    '                strvendors = strvendors + strv.Replace(",", "") + ";"
    '            Next
    '        End If
    '        strvendors = strvendors.Remove(strvendors.Length - 1)
    '        pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + strvendors + ")"


    '        Dim i As New Integer
    '        For i = 0 To Count - 1
    '            pcScript &= "" + Graphtype + ".setSeries(" & Group(i) & ""
    '            For j = 0 To dsvendor.Tables(0).Rows.Count - 1
    '                dvC = dsprice.Tables(0).DefaultView
    '                dvC.RowFilter = "BATCHVALUE='" + Group(i).ToString() + "' and FIRSTNAME='" + dsvendor.Tables(0).Rows(j).Item("FIRSTNAME").ToString() + "' "
    '                dtC = dvC.ToTable()
    '                If dtC.Rows.Count > 0 Then
    '                    If dtC.Rows(0).Item("PRICE").ToString() <> "" Then
    '                        price = FormatNumber(dtC.Rows(0).Item("Price"), 4)
    '                        pcScript &= ";" & price & " "
    '                    End If
    '                Else
    '                    pcScript &= " ;"
    '                End If
    '            Next
    '            pcScript &= ")"

    '        Next
    '    End If
    '    MaterialPrice.Visible = True
    '    Dim dsChartSetting As New DataTable
    '    Dim objGetData1 As New Configration.Selectdata()
    '    dsChartSetting = objGetData1.GetChartSettings().Tables(0)

    '    Dim myImage As CordaEmbedder = New CordaEmbedder()
    '    myImage.externalServerAddress = dsChartSetting.Rows(0)("EXTSERVERADD").ToString()
    '    myImage.internalCommPortAddress = dsChartSetting.Rows(0)("INTCOMPORTADD").ToString()
    '    myImage.imageTemplate = GraphName + ".itxml"
    '    myImage.userAgent = Request.UserAgent
    '    myImage.width = 800
    '    myImage.height = 400
    '    myImage.returnDescriptiveLink = False
    '    myImage.language = "EN"

    '    myImage.pcScript = pcScript + "Y-axis.SetText()"
    '    myImage.outputType = "FLASH"
    '    myImage.fallback = "STRICT"

    '    MaterialPrice.InnerHtml = myImage.getEmbeddingHTML
    'End Sub
End Class
