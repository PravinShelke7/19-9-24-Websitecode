Imports Corda
Imports System.Web.UI.HtmlControls
Imports System.Data
Imports System.Data.OleDb
Imports System.Web.HttpBrowserCapabilities
Partial Class Charts_SavvyPackProCharts_Tools_Createmultiplechart
    Inherits System.Web.UI.Page
    Dim objGetdata As New SavvyProGetData
    Dim objUpIns As New SavvyProUpInsData

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load


    End Sub
    Protected Sub btnrefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "MakeVisible('divM');", True)

            ' divM.Visible = True
            ' lblError.Text = Session("GraphVendors")
            Session("RFPPRICEID") = hidPriceId.Value
            If hidPriceDes.Value <> "" Then
                lnkprice.Text = hidPriceDes.Value
                'lnkpricegrp.Text = hidPriceDes.Value
            End If
            ' lnkGroup.Text = hidGroupDes.Value
        Catch ex As Exception
            lblError.Text = "Error:btnrefresh_Click:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub btnrefresh1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh1.Click
        Try
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "MakeVisible('divMLG');", True)

            ' divM.Visible = True
            ' lblError.Text = Session("GraphVendors")
            Session("RFPPRICEID") = hidPriceId.Value
            If hidPriceDes.Value <> "" Then
                lnkPriceSelectionB.Text = hidPriceDes.Value
                'lnkpricegrp.Text = hidPriceDes.Value
            End If
            ' lnkGroup.Text = hidGroupDes.Value
        Catch ex As Exception
            lblError.Text = "Error:btnrefresh1_Click:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try
            lnkprice.Text = hidPriceDes.Value
            lnkGroup.Text = hidGroupDes.Value
            lnkvendor.Text = hidvendordes.Value
            lnkPriceCol.Text = hidPriceColDes.Value
            GetChart(Session("GraphVendors"), hidGroupDes.Value)
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "MakeVisible('divM');", True)
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "MakeInVisible('divML');", True)

        Catch ex As Exception
            lblError.Text = "Error:btnSubmit_Click" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetChart(ByVal Vendors As String, ByVal Groups As String)
        Try
            Dim Data As String = String.Empty
            Dim Text As String = String.Empty
            Dim Pref As String = String.Empty
            Dim pcScript As String = String.Empty
            Dim Graphtype As String = String.Empty
            Dim GraphName As String = String.Empty

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
            'ChartComp.Visible = True
            dsprice = objGetdata.GetPriceByRFP(hidPriceColId.Value, hidGroupDes.Value, Vendors)
            dsvendor = objGetdata.GetVendorDesNotExists(Vendors, hidPriceColId.Value, hidGroupDes.Value)
            If dsprice.Tables(0).Rows.Count > 0 Then
                If ddlChartType.SelectedValue = "RBC" Then
                    lblHeadingS.Text = "(" + hidPriceDes.Value + " Chart" + ")"
                    GraphName = "Sargento_KraftPouchvsTray"
                    Graphtype = "graph"
                    Cnt = arrVendor.Length
                    Pref = hidGroupDes.Value
                    pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + hidPriceDes.Value + ")"
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
                ElseIf ddlChartType.SelectedValue = "PIE" Then
                    lblHeadingS.Text = "(" + hidPriceDes.Value + " Chart" + ")"
                    GraphName = "Sargento_KraftPouchvsTray"
                    Graphtype = "graph"
                    Cnt = arrVendor.Length
                    Pref = hidGroupDes.Value
                    pcScript &= Graphtype + ".setCategories(" + hidPriceDes.Value + ")"

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
            ChartComp.Visible = True
            Dim myImage As CordaEmbedder = New CordaEmbedder()
            myImage.externalServerAddress = dsChartSetting.Rows(0)("EXTSERVERADD").ToString()
            myImage.internalCommPortAddress = dsChartSetting.Rows(0)("INTCOMPORTADD").ToString()
            If ddlChartType.SelectedValue = "PIE" Then
                myImage.imageTemplate = "Sargento_KraftPouchvsTray_Html4" + ".itxml"
            ElseIf ddlChartType.SelectedValue = "SBAR" Then
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

            ChartComp.InnerHtml = myImage.getEmbeddingHTML
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            '  If TxtChartN.Text <> "" Then  'Or hidPriceDes.Value <> "" Or hidGroupDes.Value <> "" Or hidvendordes.Value <> "" Or hidPriceColDes.Value <> "" Then
            If Session("Mode") = "Edit" Then
                objUpIns.updatechartById(TxtChartN.Text, hidPriceId.Value, hidvendorid.Value, hidGroupId.Value, hidPriceColId.Value, ddlChartType.SelectedValue.ToString(), hidGroupDes.Value, Session("UserId"), Session("ChartId"))
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "New_WinCopy", "alert('Chart updated successfully');", True)

            Else
                objUpIns.InsertIntoSupplierChart(TxtChartN.Text, hidPriceId.Value, hidvendorid.Value, hidGroupId.Value, hidPriceColId.Value, ddlChartType.SelectedValue.ToString(), hidGroupDes.Value, Session("UserId"))
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "New_WinCopy", "alert('Chart created successfully');", True)

            End If
            Session("GraphVendors") = ""
            Session("RFPPRICEID") = ""
            TxtChartN.Text = "TestChart"
            Session("Mode") = ""
            lblHeading.Text = ""
            lblHeadingS.Text = ""
            lnkprice.Text = "Select Dataset"
            lnkGroup.Text = "Select Group"
            lnkvendor.Text = "Select Vendor"
            lnkPriceCol.Text = "Select Price Column"
            hidPriceId.Value = ""
            hidPriceDes.Value = ""
            hidvendorid.Value = ""
            hidvendordes.Value = ""
            hidGroupDes.Value = ""
            hidGroupId.Value = ""
            hidPriceColDes.Value = ""
            hidPriceColId.Value = ""
            ChartComp.Visible = False
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "MakeInVisible('divM');", True)
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "MakeInVisible('ChartComp');", True)

        Catch ex As Exception
            lblError.Text = "Error:btnSubmit_Click" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnsubmitLine_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsubmitLine.Click
        Try
            lnkpriceLine.Text = hidPriceDes.Value
            lnkvendorLine.Text = hidvendordes.Value
            lnkPriceColumnLine.Text = hidPriceColDes.Value
            If ddlAxis.SelectedValue = "LINEGRP" Then
                GenerateGraphLine()
            Else
                GenerateGraphLineGrp()
            End If
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "MakeVisible('divML');", True)
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "MakeInVisible('divM');", True)

        Catch ex As Exception
            lblError.Text = "Error:btnSubmitLine_Click" + ex.Message.ToString()
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

        Dim arrVendor() As String = Session("GraphVendors").ToString().Split(",")
        Dim arrvendordes() As String = hidvendordes.Value.ToString().Split(",")
        Dim dsprice As DataSet
        Dim dsgroup As DataSet
        MaterialPrice.Visible = True
        dsprice = objGetdata.GetPriceByRFPLine(hidPriceColId.Value, Session("GraphVendors"))
        'dsgroup = objGetdata.GetGroupsDet(hidPriceId.Value)
        dsgroup = objGetdata.GetPRICEDATARFPN(hidPriceId.Value, Session("UserId"))

        Count = dsgroup.Tables(0).Rows.Count
        Dim Group(Count) As String
        Dim Groupid(Count) As String
        For i = 0 To Count - 1
            Group(i) = dsgroup.Tables(0).Rows(i).Item("format1").ToString() + "#" + dsgroup.Tables(0).Rows(i).Item("finition1").ToString() + "#" + dsgroup.Tables(0).Rows(i).Item("width").ToString()
            'Groupid(i) = dsgroup.Tables(0).Rows(i).Item("SEQ").ToString()

        Next
        Dim strTit(20) As String
        Dim price As New Double
        Dim dvC As New DataView
        Dim dtC As New DataTable

        Dim strvendors As String = ""
        If dsprice.Tables(0).Rows.Count > 0 Then
            For y As Integer = 0 To arrVendor.Length - 1
                Dim strv As String = arrvendordes(y).ToString()
                strvendors = strvendors + strv.Replace(",", "") + ";"
            Next
            strvendors = strvendors.Remove(strvendors.Length - 1)
            pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + strvendors + ")"

            Dim i As New Integer
            For i = 0 To Count - 1
                pcScript &= "" + Graphtype + ".setSeries(" & Group(i) & ""
                For j = 0 To arrVendor.Length - 1
                    dvC = dsprice.Tables(0).DefaultView
                    dvC.RowFilter = "CODE='" + Group(i).ToString() + "' and vendorid='" + arrVendor(j).ToString() + "' "
                    dtC = dvC.ToTable()
                    If dtC.Rows.Count > 0 Then
                        If dtC.Rows(0).Item("PRICE").ToString() <> "" Then
                            price = FormatNumber(dtC.Rows(0).Item("Price"), 4)
                            pcScript &= ";" & price & ""
                        End If
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
        myImage.width = 800
        myImage.height = 600
        myImage.returnDescriptiveLink = False
        myImage.language = "EN"
        myImage.pcScript = pcScript + "Y-axis.SetText(" + hidPriceDes.Value + ")"
        myImage.outputType = "FLASH"
        myImage.fallback = "STRICT"
        MaterialPrice.InnerHtml = myImage.getEmbeddingHTML
    End Sub
    Protected Sub btnSaveLine_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveLine.Click
        Try

            If Session("Mode") = "Edit" Then
                objUpIns.updateLinechartById(txtLineChart.Text, hidPriceId.Value, hidvendorid.Value, hidPriceColId.Value, ddlAxis.SelectedValue.ToString(), Session("UserId"), Session("ChartId"))
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "New_WinCopy", "alert('Chart updated successfully');", True)

            Else
                objUpIns.InsertIntoSupplierChartLine(txtLineChart.Text, hidPriceId.Value, hidvendorid.Value, hidPriceColId.Value, ddlAxis.SelectedValue.ToString(), Session("UserId"))
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "New_WinCopy", "alert('Chart created successfully');", True)

            End If
            txtLineChart.Text = "TestChart"
            Session("GraphVendors") = ""
            Session("RFPPRICEID") = ""
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "MakeInVisible('divML');", True)
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "MakeInVisible('ChartComp');", True)

            Session("Mode") = ""
            lblHeading.Text = ""
            lblHeadingS.Text = ""

            lnkpriceLine.Text = "Select Dataset"
            lnkvendorLine.Text = "Select Vendor"
            lnkPriceColumnLine.Text = "Select Price Column"

            hidPriceDes.Value = ""
            hidPriceId.Value = ""
            hidvendordes.Value = ""
            hidvendorid.Value = ""
            hidPriceColDes.Value = ""
            hidPriceColId.Value = ""

            ChartComp.Visible = False
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "MakeInVisible('divM');", True)

        Catch ex As Exception
            lblError.Text = "Error:btnSaveLine_Click" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnCopy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCopy.Click
        Dim ReportId As String = String.Empty
        Dim NewReportId As String = String.Empty
        Try
            ReportId = hidCRptId.Value

            If hidCRptId.Value = "0" Or hidCRptId.Value = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "alert('Please select Chart Report');", True)
            Else
                If hidtype.Value = "USER" Then
                    NewReportId = objUpIns.CreatChartReport(ReportId, Session("UserId").ToString())
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Report#" + NewReportId.ToString() + " created successfully.\nReport#" + ReportId.ToString() + " variables transferred to Report#" + NewReportId.ToString() + " successfully.');", True)
                Else
                    NewReportId = objUpIns.CreatBatchChartReport(ReportId, Session("UserId").ToString())
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Report#" + NewReportId.ToString() + " created successfully.\nReport#" + ReportId.ToString() + " variables transferred to Report#" + NewReportId.ToString() + " successfully.');", True)

                End If

            End If
            lnkCReports.Text = "Display Chart Report List"
            hidCRptId.Value = "0"
            hidCRptDes.Value = "Display Chart Report List"
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub btCEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btCEdit.Click
        Dim RptId As String = ""
        Dim ds As New DataSet
        Dim dspricedes As DataSet
        Dim dsvendor As DataSet
        Dim DsPriceCol As DataSet
        Try
            ' Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "MakeVisible('divM');", True)
            Session("Mode") = "Edit"
            Session("ChartId") = hidCRptId.Value
            RptId = hidCRptId.Value
            hidvendordes.Value = ""
            If hidtype.Value = "USER" Then
                ds = objGetdata.GetExistingChartDetails(RptId)
                If RptId <> "" Then
                    lnkCReports.Text = hidCRptDes.Value
                    If ds.Tables(0).Rows.Count > 0 Then
                        If ds.Tables(0).Rows(0).Item("CHARTTYPE").ToString = "LINEGRP" Then
                            txtLineChart.Text = ds.Tables(0).Rows(0).Item("REPORTNAME").ToString()
                            hidPriceId.Value = ds.Tables(0).Rows(0).Item("RFPPRICEID").ToString()
                            Session("RFPPRICEID") = hidPriceId.Value
                            hidvendorid.Value = ds.Tables(0).Rows(0).Item("VENDORID").ToString()
                            Session("GraphVendors") = hidvendorid.Value
                            hidPriceColId.Value = ds.Tables(0).Rows(0).Item("PRICECOLID").ToString()
                            dspricedes = objGetdata.GetRFPPriceDes(hidPriceId.Value)
                            hidPriceDes.Value = dspricedes.Tables(0).Rows(0).Item("PRICEDETAIL").ToString()
                            DsPriceCol = objGetdata.GetPriceColDes(hidPriceColId.Value)
                            hidPriceColDes.Value = DsPriceCol.Tables(0).Rows(0).Item("DESCRIPTION").ToString()
                            dsvendor = objGetdata.GetVendorDes(hidvendorid.Value)
                            For j = 0 To dsvendor.Tables(0).Rows.Count - 1
                                hidvendordes.Value = dsvendor.Tables(0).Rows(j).Item("EMAILADDRESS").ToString() + "," + hidvendordes.Value
                            Next
                            hidvendordes.Value = hidvendordes.Value.Remove(hidvendordes.Value.Length - 1)
                            lnkpriceLine.Text = hidPriceDes.Value
                            lnkvendorLine.Text = hidvendordes.Value
                            lnkPriceColumnLine.Text = hidPriceColDes.Value
                            ddlAxis.SelectedValue = "LINEGRP"
                            divMainHeading.Style.Add("display", "inline")
                            MaterialPrice.Style.Add("display", "inline")
                            divML.Style.Add("display", "inline")
                            GenerateGraphLine()
                            'GetChart(hidvendorid.Value, hidGroupId.Value)
                        ElseIf ds.Tables(0).Rows(0).Item("CHARTTYPE").ToString = "LINEVENDOR" Then
                            txtLineChart.Text = ds.Tables(0).Rows(0).Item("REPORTNAME").ToString()
                            hidPriceId.Value = ds.Tables(0).Rows(0).Item("RFPPRICEID").ToString()
                            Session("RFPPRICEID") = hidPriceId.Value
                            hidvendorid.Value = ds.Tables(0).Rows(0).Item("VENDORID").ToString()
                            Session("GraphVendors") = hidvendorid.Value
                            hidPriceColId.Value = ds.Tables(0).Rows(0).Item("PRICECOLID").ToString()
                            dspricedes = objGetdata.GetRFPPriceDes(hidPriceId.Value)
                            hidPriceDes.Value = dspricedes.Tables(0).Rows(0).Item("PRICEDETAIL").ToString()
                            DsPriceCol = objGetdata.GetPriceColDes(hidPriceColId.Value)
                            hidPriceColDes.Value = DsPriceCol.Tables(0).Rows(0).Item("DESCRIPTION").ToString()
                            dsvendor = objGetdata.GetVendorDes(hidvendorid.Value)
                            For j = 0 To dsvendor.Tables(0).Rows.Count - 1
                                hidvendordes.Value = dsvendor.Tables(0).Rows(j).Item("EMAILADDRESS").ToString() + "," + hidvendordes.Value
                            Next
                            hidvendordes.Value = hidvendordes.Value.Remove(hidvendordes.Value.Length - 1)
                            lnkpriceLine.Text = hidPriceDes.Value
                            lnkvendorLine.Text = hidvendordes.Value
                            lnkPriceColumnLine.Text = hidPriceColDes.Value
                            ddlAxis.SelectedValue = "LINEVENDOR"

                            divMainHeading.Style.Add("display", "inline")
                            MaterialPrice.Style.Add("display", "inline")
                            divML.Style.Add("display", "inline")
                            GenerateGraphLineGrp()
                        Else
                            TxtChartN.Text = ds.Tables(0).Rows(0).Item("REPORTNAME").ToString()
                            hidPriceId.Value = ds.Tables(0).Rows(0).Item("RFPPRICEID").ToString()
                            Session("RFPPRICEID") = hidPriceId.Value
                            hidvendorid.Value = ds.Tables(0).Rows(0).Item("VENDORID").ToString()
                            Session("GraphVendors") = hidvendorid.Value
                            hidGroupId.Value = ds.Tables(0).Rows(0).Item("SEQ").ToString()
                            hidPriceColId.Value = ds.Tables(0).Rows(0).Item("PRICECOLID").ToString()
                            dspricedes = objGetdata.GetRFPPriceDes(hidPriceId.Value)
                            hidPriceDes.Value = dspricedes.Tables(0).Rows(0).Item("PRICEDETAIL").ToString()
                            dsvendor = objGetdata.GetVendorDes(hidvendorid.Value)
                            DsPriceCol = objGetdata.GetPriceColDes(hidPriceColId.Value)
                            hidPriceColDes.Value = DsPriceCol.Tables(0).Rows(0).Item("DESCRIPTION").ToString()
                            hidGroupDes.Value = ds.Tables(0).Rows(0).Item("CODE").ToString()
                            For j = 0 To dsvendor.Tables(0).Rows.Count - 1
                                hidvendordes.Value = dsvendor.Tables(0).Rows(j).Item("EMAILADDRESS").ToString() + "," + hidvendordes.Value
                            Next
                            hidvendordes.Value = hidvendordes.Value.Remove(hidvendordes.Value.Length - 1)
                            lnkprice.Text = hidPriceDes.Value
                            lnkGroup.Text = hidGroupDes.Value
                            lnkvendor.Text = hidvendordes.Value
                            lnkPriceCol.Text = hidPriceColDes.Value
                            ddlChartType.SelectedValue = ds.Tables(0).Rows(0).Item("CHARTTYPE").ToString()
                            ddlChartType.Enabled = False
                            divMainHeading.Style.Add("display", "inline")
                            MaterialPrice.Style.Add("display", "inline")
                            divM.Style.Add("display", "inline")
                            GetChart(hidvendorid.Value, hidGroupDes.Value)
                        End If
                    End If
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "New_WinCopy", "alert('Please Select Chart report.');", True)
                End If

            ElseIf hidtype.Value = "BATCH" Then
                ds = objGetdata.GetExistingBatchChartDetails(RptId)
                If RptId <> "" Then
                    lnkCReports.Text = hidCRptDes.Value
                    txtcn.Text = ds.Tables(0).Rows(0).Item("CHARTNAME").ToString()
                    hidPriceId.Value = ds.Tables(0).Rows(0).Item("RFPPRICEID").ToString()
                    Session("RFPPRICEID") = hidPriceId.Value
                    hidvendorid.Value = ds.Tables(0).Rows(0).Item("VENDORID").ToString()
                    Session("GraphVendors") = hidvendorid.Value
                    dspricedes = objGetdata.GetRFPPriceDes(hidPriceId.Value)
                    hidPriceDes.Value = dspricedes.Tables(0).Rows(0).Item("PRICEDETAIL").ToString()
                    dsvendor = objGetdata.GetVendorDes(hidvendorid.Value)
                    For j = 0 To dsvendor.Tables(0).Rows.Count - 1
                        hidvendordes.Value = dsvendor.Tables(0).Rows(j).Item("EMAILADDRESS").ToString() + "," + hidvendordes.Value
                    Next
                    hidvendordes.Value = hidvendordes.Value.Remove(hidvendordes.Value.Length - 1)
                    lnkPriceSelectionB.Text = hidPriceDes.Value
                    lnkVendorB.Text = hidvendordes.Value
                    hidGroupDes.Value = ds.Tables(0).Rows(0).Item("CODE").ToString()
                    lnkGroupB.Text = hidGroupDes.Value
                    hidGroupId.Value = ds.Tables(0).Rows(0).Item("OTHERPREFRFPID").ToString()
                    divMainHeading.Style.Add("display", "inline")
                    MaterialPrice.Style.Add("display", "inline")
                    divMLG.Style.Add("display", "inline")
                    GenerateGraphLineBatch()
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "New_WinCopy", "alert('Please Select Chart report.');", True)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub btCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        lblHeading.Text = ""
        lblHeadingS.Text = ""

        Session("GraphVendors") = ""
        Session("RFPPRICEID") = ""
        TxtChartN.Text = "TestChart"
        Session("Mode") = ""
        lblHeading.Text = ""
        lblHeadingS.Text = ""
        lnkprice.Text = "Select Dataset"
        lnkGroup.Text = "Select Group"
        lnkvendor.Text = "Select Vendor"
        lnkPriceCol.Text = "Select Price Column"
        hidPriceId.Value = ""
        hidPriceDes.Value = ""
        hidvendorid.Value = ""
        hidvendordes.Value = ""
        hidGroupDes.Value = ""
        hidGroupId.Value = ""
        hidPriceColDes.Value = ""
        hidPriceColId.Value = ""
        ChartComp.Visible = False
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "MakeInVisible('divM');", True)

    End Sub
    Protected Sub btCancelLine_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btncancleline.Click
        txtLineChart.Text = "TestChart"
        Session("GraphVendors") = ""
        Session("RFPPRICEID") = ""

        Session("Mode") = ""
        lblHeading.Text = ""
        lblHeadingS.Text = ""

        lnkpriceLine.Text = "Select Dataset"
        lnkvendorLine.Text = "Select Vendor"
        lnkPriceColumnLine.Text = "Select Price Column"

        hidPriceDes.Value = ""
        hidPriceId.Value = ""
        hidvendordes.Value = ""
        hidvendorid.Value = ""
        hidPriceColDes.Value = ""
        hidPriceColId.Value = ""
        MaterialPrice.Visible = False
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "MakeInVisible('divML');", True)

    End Sub
    'Protected Sub btSubmitLineGrp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btSubmitLineGrp.Click
    '    GenerateGraphLineGrp()
    'End Sub
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

        Dim arrVendor() As String = Session("GraphVendors").ToString().Split(",")
        Dim arrvendordes() As String = hidvendordes.Value.ToString().Split(",")
        Dim dsprice As DataSet
        Dim dsgroup As DataSet
        MaterialPrice.Visible = True
        dsprice = objGetdata.GetPriceByRFPLine(hidPriceColId.Value, Session("GraphVendors"))
        'dsgroup = objGetdata.GetGroupsDet(hidPriceId.Value)
        dsgroup = objGetdata.GetPRICEDATARFPN(hidPriceId.Value, Session("UserId"))

        Count = dsgroup.Tables(0).Rows.Count
        Dim Group(Count) As String
        Dim Groupid(Count) As String
        For i = 0 To Count - 1
            Group(i) = dsgroup.Tables(0).Rows(i).Item("format1").ToString() + "#" + dsgroup.Tables(0).Rows(i).Item("finition1").ToString() + "#" + dsgroup.Tables(0).Rows(i).Item("width").ToString()
            'Groupid(i) = dsgroup.Tables(0).Rows(i).Item("SEQ").ToString()

        Next
        Dim strTit(20) As String
        Dim price As New Double
        Dim dvC As New DataView
        Dim dtC As New DataTable
        Dim dvvendor As DataView
        Dim dtvendor As DataTable
        Dim dsvendor As DataSet
        dsvendor = objGetdata.GetVendorDes(Session("GraphVendors"))
        Dim strvendors As String = ""
        If dsprice.Tables(0).Rows.Count > 0 Then
            'For y As Integer = 0 To arrVendor.Length - 1
            '    Dim strv As String = arrvendordes(y).ToString()
            '    strvendors = strvendors + strv.Replace(",", "") + ";"
            'Next
            'strvendors = strvendors.Remove(strvendors.Length - 1)

            Dim strgroup As String = ""
            For k = 0 To Count - 1
                Dim strg As String = Group(k).ToString()
                strgroup = strgroup + strg.Replace(",", "") + ";"
            Next
            strgroup = strgroup.Remove(strgroup.Length - 1)

            pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + strgroup + ")"

            Dim i As New Integer
            For j = 0 To arrVendor.Length - 1
                dvvendor = dsvendor.Tables(0).DefaultView
                dvvendor.RowFilter = "VENDORID=" + arrVendor(j).ToString() + ""
                dtvendor = dvvendor.ToTable()
                pcScript &= "" + Graphtype + ".setSeries(" & dtvendor.Rows(0).Item("EMAILADDRESS").ToString() & ""
                For i = 0 To Count - 1

                    dvC = dsprice.Tables(0).DefaultView
                    dvC.RowFilter = "CODE='" + Group(i).ToString() + "' and vendorid='" + arrVendor(j).ToString() + "' "
                    dtC = dvC.ToTable()
                    If dtC.Rows.Count > 0 Then
                        If dtC.Rows(0).Item("PRICE").ToString() <> "" Then
                            price = FormatNumber(dtC.Rows(0).Item("Price"), 4)
                            pcScript &= ";" & price & ""
                        End If
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

        myImage.pcScript = pcScript + "Y-axis.SetText(" + hidPriceDes.Value + ")"
        myImage.outputType = "FLASH"
        myImage.fallback = "STRICT"
        MaterialPrice.InnerHtml = myImage.getEmbeddingHTML
    End Sub

    Protected Sub btnsubmitb_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsubmitb.Click
        Try
            Dim trHeader As New TableRow
            Dim tdHeader As TableCell

            Dim div1 As HtmlGenericControl
            lnkPriceSelectionB.Text = hidPriceDes.Value
            lnkVendorB.Text = hidvendordes.Value
            lnkGroupB.Text = hidGroupDes.Value
            ' GenerateGraphLineBatch()
            hidGroupId.Value = "429,430,431,432"
            hidGroupDes.Value = "SLR T4-500,SLR Z1,SL L4-500,H2"
            Dim str() As String = hidGroupId.Value.ToString().Split(",")
            Dim strdes() As String = hidGroupId.Value.ToString().Split(",")
            For i As Integer = 0 To str.Count - 1
                div1 = New HtmlGenericControl
                tdHeader = New TableCell
                tdHeader.Controls.Add(div1)
                trHeader.Controls.Add(tdHeader)
                tblChart.Controls.Add(trHeader)
                GenerateGraphLineBatch(str(i).ToString(), strdes(i), div1)
            Next
            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "MakeVisible('divMLG');", True)
            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "MakeInVisible('divM');", True)

        Catch ex As Exception
            lblError.Text = "Error:btnSubmitLine_Click" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub GenerateGraphLineBatch(ByVal mgroupid As String, ByVal groupdes As String, ByVal div As HtmlGenericControl)
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

        Dim arrVendor() As String = Session("GraphVendors").ToString().Split(",")
        Dim arrvendordes() As String = hidvendordes.Value.ToString().Split(",")
        Dim dsprice As DataSet
        Dim dsgroup As DataSet
        MaterialPrice.Visible = True

        lblHeadingS.Text = groupdes
        dsprice = objGetdata.GetPriceByBatchLine(mgroupid, Session("GraphVendors"))
        'dsgroup = objGetdata.GetGroupsDet(hidPriceId.Value)
        dsgroup = objGetdata.GetGrpByBatchLine()

        Count = dsgroup.Tables(0).Rows.Count
        Dim Group(Count) As String
        Dim Groupid(Count) As String
        For i = 0 To Count - 1
            Group(i) = dsgroup.Tables(0).Rows(i).Item("BATCHVALUE").ToString()
            'Groupid(i) = dsgroup.Tables(0).Rows(i).Item("SEQ").ToString()
        Next
        Dim strTit(20) As String
        Dim price As New Double
        Dim dvC As New DataView
        Dim dtC As New DataTable
        Dim dvvendor As DataView
        Dim dtvendor As DataTable
        Dim dsvendor As DataSet
        dsvendor = objGetdata.GetVendorDes(Session("GraphVendors"))
        Dim strvendors As String = ""
        If dsprice.Tables(0).Rows.Count > 0 Then
            'For y As Integer = 0 To arrVendor.Length - 1
            '    Dim strv As String = arrvendordes(y).ToString()
            '    strvendors = strvendors + strv.Replace(",", "") + ";"
            'Next
            'strvendors = strvendors.Remove(strvendors.Length - 1)

            'Dim strgroup As String = ""
            'For k = 0 To Count - 1
            '    Dim strg As String = Group(k).ToString()
            '    strgroup = strgroup + strg.Replace(",", "") + ";"
            'Next
            'strgroup = strgroup.Remove(strgroup.Length - 1)
            If dsvendor.Tables(0).Rows.Count > 0 Then
                For m As Integer = 0 To dsvendor.Tables(0).Rows.Count - 1
                    Dim strv As String = dsvendor.Tables(0).Rows(m).Item("FIRSTNAME").ToString()
                    strvendors = strvendors + strv.Replace(",", "") + ";"
                Next
            End If
            strvendors = strvendors.Remove(strvendors.Length - 1)
            pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + strvendors + ")"
            '    pcScript = pcScript + Graphtype + ".addHoverText(Vendors:" + strvendors + "<br/>)"

            Dim i As New Integer
            For i = 0 To Count - 1
                pcScript &= "" + Graphtype + ".setSeries(" & Group(i) & ""
                For j = 0 To dsvendor.Tables(0).Rows.Count - 1
                    dvC = dsprice.Tables(0).DefaultView
                    dvC.RowFilter = "BATCHVALUE='" + Group(i).ToString() + "' and FIRSTNAME='" + dsvendor.Tables(0).Rows(j).Item("FIRSTNAME").ToString() + "' "
                    dtC = dvC.ToTable()
                    If dtC.Rows.Count > 0 Then
                        If dtC.Rows(0).Item("PRICE").ToString() <> "" Then
                            price = FormatNumber(dtC.Rows(0).Item("Price"), 4)
                            pcScript &= ";" & price & " "
                        End If
                    Else
                        pcScript &= " ;"
                    End If
                Next
                pcScript &= ")"
                'For m = 0 To dsvendor.Tables(0).Rows.Count - 1
                '    dvC = dsprice.Tables(0).DefaultView
                '    dvC.RowFilter = "BATCHVALUE='" + Group(i).ToString() + "' and FIRSTNAME='" + dsvendor.Tables(0).Rows(m).Item("FIRSTNAME").ToString() + "' "
                '    dtC = dvC.ToTable()
                '    If dtC.Rows.Count > 0 Then
                '        pcScript = pcScript + Graphtype + ".addHoverText(Price:" + dtC.Rows(0).Item("PRICE").ToString() + ",Value:" + Group(i) + "<br/>)"
                '    End If
                'Next

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
        myImage.width = 800
        myImage.height = 400
        myImage.returnDescriptiveLink = False
        myImage.language = "EN"

        myImage.pcScript = pcScript + "Y-axis.SetText()"
        myImage.outputType = "FLASH"
        myImage.fallback = "STRICT"

        MaterialPrice.InnerHtml = myImage.getEmbeddingHTML
    End Sub
    Protected Sub GenerateGraphLineBatch()
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

        Dim arrVendor() As String = Session("GraphVendors").ToString().Split(",")
        Dim arrvendordes() As String = hidvendordes.Value.ToString().Split(",")
        Dim dsprice As DataSet
        Dim dsgroup As DataSet
        MaterialPrice.Visible = True

        lblHeadingS.Text = hidGroupDes.Value
        dsprice = objGetdata.GetPriceByBatchLine(hidGroupId.Value, Session("GraphVendors"))
        'dsgroup = objGetdata.GetGroupsDet(hidPriceId.Value)
        dsgroup = objGetdata.GetGrpByBatchLine()

        Count = dsgroup.Tables(0).Rows.Count
        Dim Group(Count) As String
        Dim Groupid(Count) As String
        For i = 0 To Count - 1
            Group(i) = dsgroup.Tables(0).Rows(i).Item("BATCHVALUE").ToString()
            'Groupid(i) = dsgroup.Tables(0).Rows(i).Item("SEQ").ToString()
        Next
        Dim strTit(20) As String
        Dim price As New Double
        Dim dvC As New DataView
        Dim dtC As New DataTable
        Dim dvvendor As DataView
        Dim dtvendor As DataTable
        Dim dsvendor As DataSet
        dsvendor = objGetdata.GetVendorDes(Session("GraphVendors"))
        Dim strvendors As String = ""
        If dsprice.Tables(0).Rows.Count > 0 Then
            'For y As Integer = 0 To arrVendor.Length - 1
            '    Dim strv As String = arrvendordes(y).ToString()
            '    strvendors = strvendors + strv.Replace(",", "") + ";"
            'Next
            'strvendors = strvendors.Remove(strvendors.Length - 1)

            'Dim strgroup As String = ""
            'For k = 0 To Count - 1
            '    Dim strg As String = Group(k).ToString()
            '    strgroup = strgroup + strg.Replace(",", "") + ";"
            'Next
            'strgroup = strgroup.Remove(strgroup.Length - 1)
            If dsvendor.Tables(0).Rows.Count > 0 Then
                For m As Integer = 0 To dsvendor.Tables(0).Rows.Count - 1
                    Dim strv As String = dsvendor.Tables(0).Rows(m).Item("FIRSTNAME").ToString()
                    strvendors = strvendors + strv.Replace(",", "") + ";"
                Next
            End If
            strvendors = strvendors.Remove(strvendors.Length - 1)
            pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + strvendors + ")"
            '    pcScript = pcScript + Graphtype + ".addHoverText(Vendors:" + strvendors + "<br/>)"

            Dim i As New Integer
            For i = 0 To Count - 1
                pcScript &= "" + Graphtype + ".setSeries(" & Group(i) & ""
                For j = 0 To dsvendor.Tables(0).Rows.Count - 1
                    dvC = dsprice.Tables(0).DefaultView
                    dvC.RowFilter = "BATCHVALUE='" + Group(i).ToString() + "' and FIRSTNAME='" + dsvendor.Tables(0).Rows(j).Item("FIRSTNAME").ToString() + "' "
                    dtC = dvC.ToTable()
                    If dtC.Rows.Count > 0 Then
                        If dtC.Rows(0).Item("PRICE").ToString() <> "" Then
                            price = FormatNumber(dtC.Rows(0).Item("Price"), 4)
                            pcScript &= ";" & price & " "
                        End If
                    Else
                        pcScript &= " ;"
                    End If
                Next
                pcScript &= ")"
                'For m = 0 To dsvendor.Tables(0).Rows.Count - 1
                '    dvC = dsprice.Tables(0).DefaultView
                '    dvC.RowFilter = "BATCHVALUE='" + Group(i).ToString() + "' and FIRSTNAME='" + dsvendor.Tables(0).Rows(m).Item("FIRSTNAME").ToString() + "' "
                '    dtC = dvC.ToTable()
                '    If dtC.Rows.Count > 0 Then
                '        pcScript = pcScript + Graphtype + ".addHoverText(Price:" + dtC.Rows(0).Item("PRICE").ToString() + ",Value:" + Group(i) + "<br/>)"
                '    End If
                'Next

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
        myImage.width = 800
        myImage.height = 400
        myImage.returnDescriptiveLink = False
        myImage.language = "EN"

        myImage.pcScript = pcScript + "Y-axis.SetText()"
        myImage.outputType = "FLASH"
        myImage.fallback = "STRICT"

        MaterialPrice.InnerHtml = myImage.getEmbeddingHTML
    End Sub

    Protected Sub btnsaveb_Click(sender As Object, e As System.EventArgs) Handles btnsaveb.Click
        If Session("Mode") = "Edit" Then
            objUpIns.updateBatchLinechartById(txtcn.Text, hidPriceId.Value, hidvendorid.Value, hidGroupId.Value, Session("UserId"), Session("ChartId"))
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "New_WinCopy", "alert('Chart updated successfully');", True)

        Else
            objUpIns.InsertIntoBatchChartLine(txtcn.Text, hidPriceId.Value, hidvendorid.Value, hidGroupId.Value, hidGroupDes.Value, Session("UserId"))
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "New_WinCopy", "alert('Chart created successfully');", True)

        End If
        Session("GraphVendors") = ""

        TxtChartN.Text = "TestChart"
        Session("Mode") = ""
        lblHeading.Text = ""
        lblHeadingS.Text = ""
        lnkPriceSelectionB.Text = "Select Dataset"
        lnkGroupB.Text = "Select Group"
        lnkVendorB.Text = "Select Vendor"
        hidPriceId.Value = ""
        hidPriceDes.Value = ""
        hidvendorid.Value = ""
        hidvendordes.Value = ""
        hidGroupDes.Value = ""
        hidGroupId.Value = ""
        MaterialPrice.Visible = False
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "MakeInVisible('divMLG');", True)

    End Sub

    Protected Sub btncanelb_Click(sender As Object, e As System.EventArgs) Handles btncanelb.Click
        txtcn.Text = "TestChart"
        Session("GraphVendors") = ""
        Session("RFPPRICEID") = ""

        Session("Mode") = ""
        lblHeading.Text = ""
        lblHeadingS.Text = ""

        lnkPriceSelectionB.Text = "Select Price"
        lnkVendorB.Text = "Select Vendor"
        lnkGroupB.Text = "Select Group"

        hidPriceDes.Value = ""
        hidPriceId.Value = ""
        hidvendordes.Value = ""
        hidvendorid.Value = ""
        hidGroupDes.Value = ""
        hidGroupId.Value = ""
        MaterialPrice.Visible = False
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "MakeInVisible('divMLG');", True)
    End Sub
End Class
