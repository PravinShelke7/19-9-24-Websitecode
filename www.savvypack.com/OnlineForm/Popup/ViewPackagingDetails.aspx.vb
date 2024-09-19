Imports System.Data
Imports System.Data.OleDb
Imports System
Imports SavvyGetData
Imports SavvyUpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_PopUp_ViewPackagingDetails
    Inherits System.Web.UI.Page
    Dim objUpIns As New UpdateInsert()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("SBack") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If
            If Not IsPostBack Then
                PageDetails()
                
                'Started Activity Log Changes
                Try
                    objUpIns.InsertLog1(Session("UserId").ToString(), "ViewPackagingDetails.aspx", "Opened to view Packaging Details Popup for Project:" + Session("ProjectId") + "", Session("ProjectId"), Session("SPROJLogInCount").ToString())

                Catch ex As Exception

                End Try
                'Ended Activity Log Changes
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub PageDetails()
        Dim ds As New DataSet
        Dim objGetData As New Selectdata
        Dim trHeader As New TableRow
        Dim trHeader1 As New TableRow
        Dim trHeader2 As New TableRow
        Dim trInner As New TableRow
        Dim tdHeader As TableCell
        Dim tdHeader1 As TableCell
        Dim tdHeader2 As TableCell
        Dim tdInner As TableCell
        Dim lbl As Label
        Try
            ds = objGetData.GetPackagingDetails(Request.QueryString("PackagingId").ToString(), Request.QueryString("ProjId").ToString())
            lblName.Text = ds.Tables(0).Rows(0).Item("NAME").ToString()
            lblOSize.Text = ds.Tables(0).Rows(0).Item("ORDERSIZE").ToString()
            lblRSize.Text = ds.Tables(0).Rows(0).Item("RUNSIZE").ToString()

            For i = 1 To 9
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "40px", "Layers", "1")
                        trHeader.Controls.Add(tdHeader)

                    Case 2
                        HeaderTdSetting(tdHeader, "120px", "Production Process", "1")
                        trHeader.Controls.Add(tdHeader)

                    Case 3
                        HeaderTdSetting(tdHeader, "120px", "Machine (#)", "1")
                        trHeader.Controls.Add(tdHeader)

                    Case 4
                        HeaderTdSetting(tdHeader, "120px", "Instantaneous Rate", "1")
                        trHeader.Controls.Add(tdHeader)

                    Case 5
                        HeaderTdSetting(tdHeader, "120px", "Throughput", "1")
                        trHeader.Controls.Add(tdHeader)

                    Case 6
                        HeaderTdSetting(tdHeader, "100px", "Downtime", "1")
                        trHeader.Controls.Add(tdHeader)

                    Case 7
                        HeaderTdSetting(tdHeader, "100px", "Waste", "1")
                        trHeader.Controls.Add(tdHeader)

                    Case 8
                        HeaderTdSetting(tdHeader, "100px", "Capital Cost", "1")
                        trHeader.Controls.Add(tdHeader)

                    Case 9
                        HeaderTdSetting(tdHeader, "200px", "Additional Information", "1")
                        trHeader.Controls.Add(tdHeader)

                End Select

            Next
            trHeader.Height = 30
            trHeader.Height = 30

            tblPackg.Controls.Add(trHeader)


            For i = 1 To 10
                trInner = New TableRow
                For j = 1 To 9
                    tdInner = New TableCell

                    Select Case j
                        Case 1
                            'Layer
                            InnerTdSetting(tdInner, "", "Center")
                            tdInner.Text = "<b>" + i.ToString() + "</b>"
                            trInner.Controls.Add(tdInner)
                        Case 2
                            InnerTdSetting(tdInner, "", "Center")
                            lbl = New Label
                            lbl.ID = "lblPP" + i.ToString()
                            lbl.Text = ds.Tables(0).Rows(0).Item("PP" + i.ToString()).ToString()
                            lbl.Width = 100
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 3
                            InnerTdSetting(tdInner, "", "Center")
                            lbl = New Label
                            lbl.ID = "lblMac" + i.ToString()
                            lbl.Text = ds.Tables(0).Rows(0).Item("MAC" + i.ToString()).ToString()
                            lbl.Width = 100
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 4
                            InnerTdSetting(tdInner, "", "Center")
                            lbl = New Label
                            lbl.ID = "lblRate" + i.ToString()
                            lbl.Text = ds.Tables(0).Rows(0).Item("RATE" + i.ToString()).ToString()
                            lbl.Width = 100
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 5
                            InnerTdSetting(tdInner, "", "Center")
                            lbl = New Label
                            lbl.CssClass = "SavvyLabel"
                            lbl.ID = "lblTPut" + i.ToString()
                            lbl.Text = ds.Tables(0).Rows(0).Item("TPUT" + i.ToString()).ToString()
                            lbl.Width = 100
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 6
                            InnerTdSetting(tdInner, "", "Center")
                            lbl = New Label
                            lbl.ID = "lblDTime" + i.ToString()
                            lbl.Text = ds.Tables(0).Rows(0).Item("DTIME" + i.ToString()).ToString()
                            lbl.Width = 100
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 7
                            InnerTdSetting(tdInner, "", "Center")
                            lbl = New Label
                            lbl.ID = "lblWaste" + i.ToString()
                            lbl.Text = ds.Tables(0).Rows(0).Item("WASTE" + i.ToString()).ToString()
                            lbl.Width = 100
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 8
                            InnerTdSetting(tdInner, "", "Center")
                            lbl = New Label
                            lbl.ID = "lblCC" + i.ToString()
                            lbl.Text = ds.Tables(0).Rows(0).Item("CC" + i.ToString()).ToString()
                            lbl.Width = 100
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 9
                            InnerTdSetting(tdInner, "", "Center")
                            lbl = New Label
                            lbl.ID = "lblInfo" + i.ToString()
                            lbl.Text = ds.Tables(0).Rows(0).Item("INFO" + i.ToString()).ToString()
                            lbl.Width = 180
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)

                    End Select
                Next
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
                trInner.Height = 30
                tblPackg.Controls.Add(trInner)

            Next

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub HeaderTdSetting(ByVal Td As TableCell, ByVal Width As String, ByVal HeaderText As String, ByVal ColSpan As String)
        Try
            Td.Text = HeaderText
            Td.ColumnSpan = ColSpan
            If Width <> "" Then
                Td.Style.Add("width", Width)
            End If
            Td.CssClass = "TdHeadingNew"
            Td.Height = 20
            Td.Font.Size = 9
            Td.Font.Bold = True
            Td.HorizontalAlign = HorizontalAlign.Center

        Catch ex As Exception
            _lErrorLble.Text = "Error:HeaderTdSetting:" + ex.Message.ToString()
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
            Td.Style.Add("font-size", "13px")
            Td.Style.Add("font-family", "Optima")
        Catch ex As Exception
            _lErrorLble.Text = "Error:InnerTdSetting:" + ex.Message.ToString()
        End Try
    End Sub

End Class
