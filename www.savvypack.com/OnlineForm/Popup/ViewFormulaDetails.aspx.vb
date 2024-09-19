Imports System.Data
Imports System.Data.OleDb
Imports System
Imports SavvyGetData
Imports SavvyUpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_PopUp_ViewFormulaDetails
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
                    objUpIns.InsertLog1(Session("UserId").ToString(), "ViewFormulaDetails.aspx", "Opened to view Formula Details Popup for Project:" + Session("ProjectId") + "", Session("ProjectId"), Session("SPROJLogInCount").ToString())

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
            ds = objGetData.GetFormulaDetails(Request.QueryString("FormulaId").ToString(), Request.QueryString("ProjId").ToString())
            lblName.Text = ds.Tables(0).Rows(0).Item("NAME").ToString()
            lblSize.Text = ds.Tables(0).Rows(0).Item("PACKSIZE").ToString()

            For i = 1 To 5
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
                        HeaderTdSetting(tdHeader, "170px", "Material", "1")
                        trHeader.Controls.Add(tdHeader)

                    Case 3
                        HeaderTdSetting(tdHeader, "170px", "Volume", "1")
                        trHeader.Controls.Add(tdHeader)

                    Case 4
                        HeaderTdSetting(tdHeader, "170px", "Price", "1")
                        trHeader.Controls.Add(tdHeader)

                    Case 5
                        HeaderTdSetting(tdHeader, "320px", "Additional Information ", "1")
                        trHeader.Controls.Add(tdHeader)

                End Select

            Next
            trHeader.Height = 30
            trHeader.Height = 30

            tblFormula.Controls.Add(trHeader)


            For i = 1 To 10
                trInner = New TableRow
                For j = 1 To 5
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
                            lbl.ID = "lblMat" + i.ToString()
                            lbl.Text = ds.Tables(0).Rows(0).Item("MAT" + i.ToString()).ToString()
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)

                        Case 3
                            InnerTdSetting(tdInner, "", "Center")
                            lbl = New Label
                            lbl.ID = "lblVol" + i.ToString()
                            lbl.Text = ds.Tables(0).Rows(0).Item("VOL" + i.ToString()).ToString()
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 4
                            InnerTdSetting(tdInner, "", "Center")
                            lbl = New Label
                            lbl.ID = "lblPrice" + i.ToString()
                            lbl.Text = ds.Tables(0).Rows(0).Item("PRICE" + i.ToString()).ToString()
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)

                        Case 5
                            InnerTdSetting(tdInner, "", "Center")
                            lbl = New Label
                            lbl.ID = "lblInfo" + i.ToString()
                            lbl.Text = ds.Tables(0).Rows(0).Item("INFO" + i.ToString()).ToString()
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
                tblFormula.Controls.Add(trInner)


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
