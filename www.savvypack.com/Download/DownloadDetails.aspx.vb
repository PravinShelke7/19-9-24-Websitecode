Imports System
Imports System.Data
Imports ShopGetData
Partial Class DownloadDetails
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim obj As New CryptoHelper
            Session("MenuItem") = "DWNLD1"
            ' If Session("UserId") Is Nothing Then

            '  Response.Redirect("Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            ' Else
            ViewState("UserId") = obj.Decrypt(Request.QueryString("UID"))
            GetPageDetails()
            '  End If

            ' End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub GetPageDetails()
        Dim ds As New DataSet
        Dim objGetData As New DownloadGetData()
        Dim i As New Integer
        Dim j As New Integer
        '  Dim DWidth As String = String.Empty
        Dim trHeader As New TableRow
        'Dim trHeader1 As New TableRow
        'Dim trHeader2 As New TableRow
        Dim trInner As New TableRow
        Dim tdHeader As TableCell
        'Dim tdHeader1 As TableCell
        'Dim tdHeader2 As TableCell
        Dim lbl As New Label
        Dim hid As New HiddenField
        Dim Link As New HyperLink
        Dim txt As New TextBox
        Dim tdInner As TableCell
        Dim lnkBtn As LinkButton
        Try
            ds = objGetData.GetDwnldDetails(ViewState("UserId").ToString())
            For i = 1 To 2
                tdHeader = New TableCell
                'tdHeader1 = New TableCell
                'tdHeader2 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "200px", "Report Name", "1")
                        trHeader.Controls.Add(tdHeader)

                    Case 2

                        HeaderTdSetting(tdHeader, "250px", "Click here to DownLoad Files", "1")
                        trHeader.Controls.Add(tdHeader)

                End Select

            Next

            trHeader.Height = 30

            tblDwnldList.Controls.Add(trHeader)


            'Inner
            For i = 1 To ds.Tables(0).Rows.Count
                trInner = New TableRow
                For j = 1 To 2
                    tdInner = New TableCell

                    Select Case j
                        Case 1
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            lbl.Text = ds.Tables(0).Rows(i - 1).Item("REPORTNAME").ToString()
                            lbl.ForeColor = Drawing.Color.Black
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)

                        Case 2
                            InnerTdSetting(tdInner, "", "center")
                            lnkBtn = New LinkButton
                            lnkBtn.CssClass = "Link"
                            lnkBtn.Text = "Download"
                            lnkBtn.ForeColor = Drawing.Color.Black
                            lnkBtn.ValidationGroup = ds.Tables(0).Rows(i - 1).Item("REPORTDETAILS").ToString()
                            AddHandler lnkBtn.Click, AddressOf lnkBtn_Click
                            tdInner.Controls.Add(lnkBtn)
                            trInner.Controls.Add(tdInner)
                    End Select
                Next

                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
                tblDwnldList.Controls.Add(trInner)
            Next
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub lnkBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim repDetails As String = ""
        Dim filename As String
        Dim DownloadPath As String = String.Empty
        Dim storagePath As String = "Z:\\" ' System.Configuration.ConfigurationManager.AppSettings("storagePath")
        Try
            Dim getLinkDetail As LinkButton = CType(sender, LinkButton)
            DownloadPath = storagePath + getLinkDetail.ValidationGroup
            filename = DownloadPath
            Response.ContentType = "application/octet-stream"
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + getLinkDetail.ValidationGroup)

            'fname = Server.MapPath(filename)
            Response.TransmitFile(filename)
            Response.End()
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
