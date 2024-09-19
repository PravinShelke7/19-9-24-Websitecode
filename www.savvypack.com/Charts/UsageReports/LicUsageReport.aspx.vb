Imports Corda
Imports System.Web.UI.HtmlControls
Imports System.Data
Imports System.Data.OleDb
Imports System.Web.HttpBrowserCapabilities
Imports System.IO
Partial Class Charts_UsageReports_LicUsageReport
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            lblTag.Text = "Copyright 1997 - " + Now.Year().ToString() + " SavvyPack Corporation"
            Dim obj As New CryptoHelper
            Dim License As String = obj.Decrypt(Request.QueryString("Lic"))

            GetUsageReoprts("ECON1", License)
            GetUsageReoprts("ECON2", License)
            GetUsageReoprts("SUSTAIN1", License)
            GetUsageReoprts("SUSTAIN2", License)
        Catch ex As Exception
            lblError.Text = "Error:Page_Load" + ex.Message
        End Try
    End Sub

    Protected Sub GetUsageReoprts(ByVal Schema As String, ByVal License As String)
        Dim ds As New DataSet

        Dim i As New Integer
        Dim ChartComp As New HtmlGenericControl
        Dim tblComp As New Panel
        Dim tr As New TableRow
        Dim td As New TableCell
        Dim trInner As New TableRow
        Dim tdInner As New TableCell
        Dim tblInner As New Table
        Dim Path As String = Server.MapPath("UsageXML/" + Schema + "_License_" + License + ".xml")
        Try
            If File.Exists(Path) Then

                ds.ReadXml(Path)


                tr = New TableRow
                td = New TableCell
                HeaderTdSetting(td, "", "" + Schema + " Usages", "2")
                tr.Controls.Add(td)
                tblUsage.Controls.Add(tr)


                tr = New TableRow
                For i = 1 To 2
                    td = New TableCell
                    Select Case i
                        Case 1
                            GenrateTable(ds, tblInner)
                            tblComp.Controls.Add(tblInner)
                            td.Controls.Add(tblComp)
                            tr.Controls.Add(td)
                        Case 2
                            GenrateChart(ds, ChartComp)
                            td.Controls.Add(ChartComp)
                            tr.Controls.Add(td)
                    End Select
                Next
                tblUsage.Controls.Add(tr)
                tblComp.Style.Add("overflow", "auto")
                tblComp.Style.Add("Height", "400px")
            End If

        Catch ex As Exception
            lblError.Text = "Error:GetUsageReoprts" + ex.Message
        End Try
    End Sub

    Protected Sub GenrateTable(ByVal ds As DataSet, ByVal tbl As Table)
        Dim i As New Integer
        Dim j As New Integer
        Dim trInner As New TableRow
        Dim tdInner As New TableCell
        Try
            trInner = New TableRow
            For j = 1 To 2
                tdInner = New TableCell
                Select Case j
                    Case 1
                        HeaderTdSetting(tdInner, "100px", "Month", "1")
                        trInner.Controls.Add(tdInner)
                    Case 2
                        HeaderTdSetting(tdInner, "100px", "Usages", "1")
                        trInner.Controls.Add(tdInner)
                End Select
            Next
            tbl.Controls.Add(trInner)



            For i = 0 To ds.Tables(0).Rows.Count - 1
                trInner = New TableRow
                For j = 1 To 2
                    tdInner = New TableCell
                    Select Case j
                        Case 1
                            InnerTdSetting(tdInner, "", "")
                            tdInner.Text = ds.Tables(0).Rows(i).Item("MONTH").ToString()
                            trInner.Controls.Add(tdInner)
                        Case 2
                            InnerTdSetting(tdInner, "", "Right")
                            tdInner.Text = ds.Tables(0).Rows(i).Item("USG").ToString()
                            trInner.Controls.Add(tdInner)
                    End Select
                Next

                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
                tbl.Controls.Add(trInner)
            Next






        Catch ex As Exception
            lblError.Text = "Error:GenrateTable" + ex.Message
        End Try
    End Sub

    Protected Sub GenrateChart(ByVal ds As DataSet, ByVal ChartComp As HtmlGenericControl)
        Dim dsChartSetting As New DataTable
        Dim pcScript As String = String.Empty
        Dim Graphtype As String = String.Empty
        Dim i As New Integer
        Dim objGetData As New Configration.Selectdata()
        Dim myImage As CordaEmbedder = New CordaEmbedder()
        Try

            dsChartSetting = objGetData.GetChartSettings().Tables(0)
            Graphtype = "Material_Price"
            pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(Usages;Budget)"

            For i = 0 To ds.Tables(0).Rows.Count - 1
                pcScript &= "" + Graphtype + ".setSeries(" + ds.Tables(0).Rows(i).Item("MONTH") + ";" & ds.Tables(0).Rows(i).Item("USG") + ";300)"
            Next


            myImage.externalServerAddress = dsChartSetting.Rows(0)("EXTSERVERADD").ToString()
            myImage.internalCommPortAddress = dsChartSetting.Rows(0)("INTCOMPORTADD").ToString()
            myImage.imageTemplate = "UsageReports" + ".itxml"
            myImage.userAgent = Request.UserAgent
            myImage.width = 550
            myImage.height = 400
            myImage.returnDescriptiveLink = True
            myImage.language = "EN"
            myImage.pcScript = pcScript + "Y-axis.SetText(Usages in hr.)"
            myImage.outputType = "JPEG"
            myImage.fallback = "STRICT"
            ChartComp.InnerHtml = myImage.getEmbeddingHTML
        Catch ex As Exception
            lblError.Text = "Error:GetUsageReoprts" + ex.Message
        End Try
    End Sub

    Protected Sub HeaderTdSetting(ByVal Td As TableCell, ByVal Width As String, ByVal HeaderText As String, ByVal ColSpan As String)
        Try
            Td.Text = HeaderText
            Td.ColumnSpan = ColSpan
            If Width <> "" Then
                Td.Style.Add("width", Width)
            End If
            Td.CssClass = "TdHeading"
            Td.Height = 20
            Td.Font.Size = 10
            Td.Font.Bold = True
            Td.HorizontalAlign = HorizontalAlign.Center



        Catch ex As Exception
            lblError.Text = "Error:HeaderTdSetting:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub Header2TdSetting(ByVal Td As TableCell, ByVal Width As String, ByVal HeaderText As String, ByVal ColSpan As String)
        Try
            Td.Text = HeaderText
            Td.ColumnSpan = ColSpan
            If Width <> "" Then
                Td.Style.Add("width", Width)
            End If
            Td.CssClass = "TdHeading"
            Td.Font.Size = 8
            Td.Height = 20
            Td.HorizontalAlign = HorizontalAlign.Center



        Catch ex As Exception
            lblError.Text = "Error:HeaderTdSetting:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub InnerTdSetting(ByVal Td As TableCell, ByVal Width As String, ByVal Align As String)
        Try

            If Width <> "" Then
                Td.Style.Add("width", Width)
            End If
            Td.Style.Add("text-align", Align)
            If Align = "Left" Then
                Td.Style.Add("padding-left", "5px")
            End If
            If Align = "Right" Then
                Td.Style.Add("padding-right", "5px")
            End If
        Catch ex As Exception
            lblError.Text = "Error:InnerTdSetting:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub TextBoxSetting(ByVal txt As TextBox, ByVal Css As String)
        Try
            txt.CssClass = Css

        Catch ex As Exception
            lblError.Text = "Error:TextBoxSetting:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub LableSetting(ByVal lbl As Label, ByVal Css As String)
        Try
            lbl.CssClass = Css

        Catch ex As Exception
            lblError.Text = "Error:LableSetting:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub LeftTdSetting(ByVal Td As TableCell, ByVal Text As String, ByVal tr As TableRow, ByVal Css As String)
        Try
            Td.Text = Text
            InnerTdSetting(Td, "", "Left")
            tr.Controls.Add(Td)
            tr.CssClass = Css
        Catch ex As Exception
            lblError.Text = "Error:LeftTdSetting:" + ex.Message.ToString()
        End Try
    End Sub
End Class
