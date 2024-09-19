Imports System.Data
Imports System.Data.OleDb
Imports System
Imports SavvyProGetData
Imports SavvyProUpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Imports System.Net.Mail
Imports System.Net
Imports AjaxControlToolkit
Partial Class Pages_SavvyPackPro_Popup_AddPriceOption
    Inherits System.Web.UI.Page
    Dim objGetdata As New SavvyProGetData
    Dim objUpIns As New SavvyProUpInsData

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        loadTab()
        GetPageDetails()

    End Sub
    Protected Sub loadTab()
        Try
           
            InsertDefaultPriceOption()
           
        Catch ex As Exception
            lblError.Text = "Error: GetRfpDetails() " + ex.Message()
        End Try
    End Sub
    Protected Sub GetPageDetails()
        Dim dsQues As New DataSet()
        Dim lnkQues As LinkButton
        Dim trQues As TableRow
        Dim tdId As TableCell
        Dim delBtn As Button
        Dim trSpace As New TableRow
        Dim txtTerm As New TextBox
        Dim txtItem As New TextBox
        Dim txtSeq As New TextBox
        Dim HidSeqQue As New HiddenField


        Dim trHeader As New TableRow
        Dim trHeader1 As New TableRow
        Dim trHeader2 As New TableRow

        Dim trInner As New TableRow

        Dim tdHeader As TableCell
        Dim chck As New CheckBox
        Dim hid As HiddenField
        Dim DvData As DataView
        Dim dsData As DataTable
        Dim calEx As CalendarExtender
        Try
            tblEditQ.Rows.Clear()
            dsQues = objGetdata.GetPriceOption(Session("hidRfpID"), Session("UserId"))
            Session("TermsData") = dsQues
            DvData = dsQues.Tables(0).DefaultView
            Session("count") = dsQues.Tables(0).Rows.Count - 1

            For i = 1 To 4
                tdHeader = New TableCell
                Dim Title As String = String.Empty

                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "50px", "Id", "1")
                        trHeader.Controls.Add(tdHeader)

                    Case 2
                        HeaderTdSetting(tdHeader, "50px", "Column Order", "1")
                        trHeader.Controls.Add(tdHeader)

                    Case 3

                        HeaderTdSetting(tdHeader, "250px", "Description", "1")
                        trHeader.Controls.Add(tdHeader)

                    Case 4

                        HeaderTdSetting(tdHeader, "350px", "Your Description", "1")
                        trHeader.Controls.Add(tdHeader)

                End Select

            Next

            trHeader.Height = 30
            tblEditQ.Controls.Add(trHeader)

            If dsQues.Tables(0).Rows.Count > 0 Then
                For i = 0 To dsQues.Tables(0).Rows.Count - 1
                    trQues = New TableRow
                    lnkQues = New LinkButton

                    For b = 1 To 4
                        tdId = New TableCell
                        Select Case b
                            Case 1
                                chck = New CheckBox
                                hid = New HiddenField
                                ' chck.ID = "chckBut" + (i + 1).ToString()
                                'hid.ID = "hidchck" + (i + 1).ToString()
                                chck.ID = dsQues.Tables(0).Rows(i).Item("PRICEOPTIONID").ToString()
                                If dsQues.Tables(0).Rows(i).Item("ISDEFAULT") = "Y" Then
                                    If dsQues.Tables(0).Rows(i).Item("ISDEFAULT").ToString() = "Y" Then
                                        chck.Checked = True
                                    Else
                                        chck.Checked = False
                                    End If
                                ElseIf dsQues.Tables(0).Rows(i).Item("ISDEFAULT") = "N" Then
                                    If dsQues.Tables(0).Rows(i).Item("ISDEFAULT").ToString() = "Y" Then
                                        chck.Checked = True
                                    Else
                                        chck.Checked = False
                                    End If
                                End If
                               
                                chck.AutoPostBack = True
                                AddHandler chck.CheckedChanged, AddressOf CheckBox_Check
                                tdId.Width = 30
                                tdId.Controls.Add(hid)
                                tdId.Controls.Add(chck)
                            Case 2
                                txtSeq = New TextBox
                                HidSeqQue = New HiddenField
                                hid = New HiddenField
                                InnerTdSetting(tdId, "", "Center")
                                txtSeq.Text = dsQues.Tables(0).Rows(i).Item("PRICEOPTIONSEQ").ToString()
                                txtSeq.ID = dsQues.Tables(0).Rows(i).Item("PRICEOPTIONID").ToString() + "_" + (i + 1).ToString()
                                HidSeqQue.Value = txtSeq.Text
                                txtSeq.Font.Size = 10
                                txtSeq.MaxLength = 3
                              
                                    txtSeq.Style.Add("background-color", "#FEFCA1")


                                txtSeq.Style.Add("font-family", "Verdana")
                                txtSeq.Style.Add("width", "28px")
                                txtSeq.Style.Add("height", "14px")
                                txtSeq.AutoPostBack = True
                                'txtSeq.Attributes.Add("onchange", "javascript:return CheckSP(this,'" + HidSeqQue.Value + "');")
                                AddHandler txtSeq.TextChanged, AddressOf TextBox_TextChanged
                                tdId.Controls.Add(txtSeq)
                                tdId.Controls.Add(HidSeqQue)
                                tdId.Controls.Add(hid)
                            Case 3
                                txtItem = New TextBox
                                'InnerTdSetting(lbl, "", "Center")

                                ' If dsQues.Tables(0).Rows(i).Item("ISDEFAULT") = "Y" Then
                                txtItem.Text = dsQues.Tables(0).Rows(i).Item("PRICEOPTIONDES").ToString()
                                txtItem.Enabled = False
                                txtItem.Style.Add("background-color", "#a6a6a6")
                                txtItem.Style.Add("color", "#4d4d4d")
                                'Else
                                'txtItem.Text = dsQues.Tables(0).Rows(i).Item("PRICEOPTIONDES").ToString()
                                'txtItem.Enabled = True
                                'txtItem.Style.Add("background-color", "#FEFCA1")
                                'End If
                                HidSeqQue.Value = txtItem.Text
                                txtItem.ID = dsQues.Tables(0).Rows(i).Item("PRICEOPTIONID").ToString() + "." + (i + 1).ToString()
                                txtItem.Style.Add("font-family", "Verdana")
                                txtItem.Style.Add("width", "300px")
                                txtItem.Style.Add("height", "14px")
                                txtItem.AutoPostBack = True
                                ' txtItem.Attributes.Add("onchange", "javascript:return CheckSP(this,'" + HidSeqQue.Value + "');")
                                'AddHandler txtItem.TextChanged, AddressOf TextBox_TextChangedT
                                tdId.Controls.Add(txtItem)
                            Case 4
                                txtTerm = New TextBox
                                txtTerm.ID = "T_" + (i + 1).ToString()
                                'txtItem.Text = dsQues.Tables(0).Rows(i).Item("USERPRICEOPTION").ToString()

                                HidSeqQue.Value = txtTerm.Text
                                'InnerTdSetting(lbl, "", "Center")
                              
                                    txtTerm.Style.Add("background-color", "#FEFCA1")

                                txtTerm.Text = dsQues.Tables(0).Rows(i).Item("USERPRICEOPTION").ToString()
                                txtTerm.Style.Add("font-family", "Verdana")
                                txtTerm.Style.Add("width", "350px")
                                txtTerm.Style.Add("height", "14px")
                                txtTerm.AutoPostBack = True
                                'txtTerm.Attributes.Add("onchange", "javascript:return CheckSP(this,'" + HidSeqQue.Value + "');")
                                AddHandler txtTerm.TextChanged, AddressOf TextBox_TextChangedT
                                tdId.Controls.Add(txtTerm)

                        End Select
                        trQues.Controls.Add(tdId)
                    Next
                    If i Mod 2 = 0 Then
                        trQues.CssClass = "AlterNateColor1"
                    Else
                        trQues.CssClass = "AlterNateColor2"
                    End If
                    tblEditQ.Controls.Add(trQues)
                Next
            End If
        Catch ex As Exception
            lblError.Text = "Error:GetPageDetails:" + ex.Message.ToString()
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
        Catch ex As Exception
            lblError.Text = "Error:InnerTdSetting:" + ex.Message.ToString()
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
    Protected Sub InsertDefaultPriceOption()
        Dim Dts As New DataSet()
        Dim i As New Integer
        Dim ds As New DataSet
        Dim DsTermSeq As New DataSet
        Dim dsDefaultPrice As DataSet
        Dim TermSeq As String = String.Empty
        Try

            dsDefaultPrice = objGetdata.GetPriceOption()
            Dts = objGetdata.GetDefaultPriceOption()
            ds = objGetdata.GetPriceOption(Session("hidRFPID"), Session("UserId"))
            'For i = 1 To dsDefaultPrice.Tables(0).Rows.Count
            '    If ds.Tables(0).Rows.Count = 0 Then
            '        DsTermSeq = objGetdata.GetPriceOptionSeq(Session("hidRfpID"))
            '        If DsTermSeq.Tables(0).Rows(0).Item("PRICEOPTIONSEQ").ToString() = "" Then
            '            TermSeq = "1"
            '        Else
            '            TermSeq = DsTermSeq.Tables(0).Rows(0).Item("PRICEOPTIONSEQ").ToString() + 1
            '        End If
            '        objUpIns.AddDefaultPriceOption(Session("UserId").ToString(), dsDefaultPrice.Tables(0).Rows(i - 1).Item("PRICEOPTIONDES").ToString(), "Y", TermSeq, Session("hidRFPID"))
            '    End If
            'Next
            For i = 1 To Dts.Tables(0).Rows.Count
                If ds.Tables(0).Rows.Count = 0 Then
                    DsTermSeq = objGetdata.GetPriceOptionSeq(Session("hidRfpID"))
                    If DsTermSeq.Tables(0).Rows(0).Item("PRICEOPTIONSEQ").ToString() = "" Then
                        TermSeq = "1"
                    Else
                        TermSeq = DsTermSeq.Tables(0).Rows(0).Item("PRICEOPTIONSEQ").ToString() + 1
                    End If
                    objUpIns.AddDefaultPriceOption(Session("UserId").ToString(), Dts.Tables(0).Rows(i - 1).Item("PRICEOPTIONDES").ToString(), "N", TermSeq, Session("hidRFPID"))
                End If
            Next
        Catch ex As Exception
            Response.Write("InsertAnalysisInformation" + ex.Message)
        End Try
    End Sub
    Protected Sub TextBox_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim TextSeq As String
        Dim QId As String
        Dim DsQuestionSeq As New DataSet()
        Dim Avail As New Boolean
        Try
            Dim TxtSeq = DirectCast(sender, TextBox)
            TextSeq = TxtSeq.Text
            QId = TxtSeq.ID

            Dim Temp As String() = QId.Split("_")
            Dim temp1 As String = Temp(0)

            Avail = objGetdata.GetPriceOptionseqAvail(TextSeq.ToString(), Session("hidRfpID").ToString())
            If Avail Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert1", "alert('Entered sequence number already exist.')", True)
                'objUpIns.UpdateQueSeqID(TextSeq.ToString(), temp1.ToString())
            Else
                objUpIns.UpdatePriceOptionSeqID(TextSeq.ToString(), temp1.ToString())
            End If
            ScriptManager.RegisterStartupScript(Page, Me.GetType(), "JSScript", "ClosePage();", True)
            GetPageDetails()
        Catch ex As Exception
            lblError.Text = "Error:TextBox_TextChanged:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub TextBox_TextChangedT(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim TextSeq As String
        Dim QId As String
        Dim DsQuestionSeq As New DataSet()
        Dim Avail As New Boolean
        Try
            Dim txtTerm = DirectCast(sender, TextBox)
            TextSeq = txtTerm.Text
            QId = txtTerm.ID
            
            Dim Temp As String() = QId.Split("_T")
            Dim temp1 As String = Temp(0)

            Avail = objGetdata.GetPriceOptionDesc(temp1, TextSeq.ToString())
            If Avail Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert1", "alert('Entered Price Option already exist.')", True)
                'objUpIns.UpdateQueSeqID(TextSeq.ToString(), temp1.ToString())
            Else
                objUpIns.UpdateOwnPriceOption(TextSeq.Replace("'", "''").ToString(), temp1.ToString(), Session("hidRfpID").ToString())
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert1", "alert('Price Option updated sucessfully.')", True)
            End If
            ScriptManager.RegisterStartupScript(Page, Me.GetType(), "JSScript", "ClosePage();", True)
            GetPageDetails()
        Catch ex As Exception
            lblError.Text = "Error:TextBox_TextChanged:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub CheckBox_Check(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objUpIns As New SavvyProUpInsData()
        Dim objGetData As New SavvyProGetData()
        Dim QId As String
        Try
            Dim chk = DirectCast(sender, CheckBox)
            If chk.Checked = True Then
                QId = chk.ID
                objUpIns.UpdateIsCheckedPriceOption(Session("hidRfpID").ToString(), QId.ToString())
            Else
                QId = chk.ID
                objUpIns.DeleteIsCheckedPriceOption(Session("hidRfpID").ToString(), QId.ToString())
            End If
            ScriptManager.RegisterStartupScript(Page, Me.GetType(), "JSScript", "ClosePage1();", True)
            GetPageDetails()
        Catch ex As Exception
            lblError.Text = "Error:CheckBox_Check:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnPriceOption_Click(sender As Object, e As System.EventArgs) Handles btnPriceOption.Click
        Dim objUpIns As New SavvyProUpInsData()
        Dim objGetData As New SavvyProGetData()
        Dim sta As String
        Dim ds As DataSet
        Dim count As Integer
        ds = objGetData.GetDefaultPriceOption()
        count = ds.Tables(0).Rows.Count
        Dim text(count) As String
        For i As Integer = 1 To ds.Tables(0).Rows.Count
            text(i) = Request.Form("T_" + i.ToString())
        Next

        'Request.Form("tabRfpSManager$tabSMPrice$hidSpecIDPrice" + i.ToString() + "")
        ' objUpIns.AddPriceOption(txtPriceOption.Text)
        'ScriptManager.RegisterStartupScript(Page, Me.GetType(), "JSScript", "alert('" + text(2) + "');", True)

    End Sub
End Class
