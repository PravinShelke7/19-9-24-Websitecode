Imports System.Data
Imports System.Data.OleDb
Imports System
Imports S4GetData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Sustain4_Assumptions_ProductFormat
    Inherits System.Web.UI.Page
#Region "Get Set Variables"
    Dim _lErrorLble As Label
    Dim _strUserName As String
    Dim _strPassword As String
    Dim _iAssumptionId As Integer


    Public Property ErrorLable() As Label
        Get
            Return _lErrorLble
        End Get
        Set(ByVal Value As Label)
            _lErrorLble = Value
        End Set
    End Property

    Public Property UserName() As String
        Get
            Return _strUserName
        End Get
        Set(ByVal Value As String)
            _strUserName = Value
        End Set
    End Property

    Public Property Password() As String
        Get
            Return _strPassword
        End Get
        Set(ByVal Value As String)
            _strPassword = Value
        End Set
    End Property

    Public Property AssumptionId() As Integer
        Get
            Return _iAssumptionId
        End Get
        Set(ByVal Value As Integer)
            _iAssumptionId = Value
        End Set
    End Property


    Public DataCnt As Integer
    Public CaseDesp As New ArrayList

#End Region

#Region "MastePage Content Variables"
    Protected Sub GetErrorLable()
        ErrorLable = Page.Master.FindControl("lblError")
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Try
                GetErrorLable()
                GetSessionDetails()
                GetPageDetails()
            Catch ex As Exception

            End Try

        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetSessionDetails()
        Try
            UserName = Session("UserName")
            Password = Session("Password")
            AssumptionId = Session("AssumptionId")

            lblAID.Text = AssumptionId
            lblAdes.Text = Session("Description")

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetSessionDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Function GetCaseIds() As String()
        Dim CaseIds(0) As String
        Dim objGetData As New S4GetData.Selectdata

        Try
            CaseIds = objGetData.Cases1(AssumptionId)

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetCaseIds:" + ex.Message.ToString()
        End Try
        Return CaseIds
    End Function

    Protected Sub GetPageDetails()
        Dim ds As New DataSet
        Dim dstbl As New DataSet
        Dim dsCaseDetails As New DataSet
        Dim objGetData As New S2GetData.Selectdata
        Dim CaseIds As String = String.Empty
        'Dim DataCnt As New Integer
        Dim i As New Integer
        Dim DWidth As String = String.Empty
        Dim arrCaseID() As String

        Try
            arrCaseID = GetCaseIds()
            'ds = objGetData.ProductFormat(CaseIds, UserName)
            DataCnt = arrCaseID.Length - 1

            Dim trHeader As New TableRow
            Dim tdHeader As TableCell
            Dim tdInner As TableCell
            Dim trInner As New TableRow
            Dim ddl As DropDownList
            Dim lbl As Label
            Dim Cunits As New Integer
            Dim Units As New Integer
            Dim Title As String = String.Empty
            Dim k As Integer
            Dim j As Integer

            DWidth = txtDWidth.Text + "px"
            tdHeader = New TableCell
            HeaderTdSetting(tdHeader, DWidth, "<img alt='' src='../../Images/spacer.gif' Style='width:160px;height:0px;'  />", 1, "")
            trHeader.Controls.Add(tdHeader)
            trHeader.Height = 20
            trHeader.CssClass = "PageSSHeading"


            For i = 0 To DataCnt
                ds = objGetData.GetProductFromatIn(arrCaseID(i))
                ds.Tables(0).TableName = arrCaseID(i).ToString()
                dstbl.Tables.Add(ds.Tables(arrCaseID(i).ToString()).Copy())
            Next


            For i = 0 To DataCnt
                dsCaseDetails = objGetData.GetCaseDetails(arrCaseID(i).ToString())
                Cunits = Convert.ToInt32(dstbl.Tables(0).Rows(0).Item("Units").ToString())
                Units = Convert.ToInt32(dstbl.Tables(i).Rows(0).Item("Units").ToString())

                tdHeader = New TableCell
                Dim Headertext As String = String.Empty
                If Cunits <> Units Then
                    Headertext = "Case#:" + arrCaseID(i).ToString() + "<br/>" + dsCaseDetails.Tables(0).Rows(0).Item("CaseDes").ToString() + "<br/> <span  style='color:red'>Unit Mismatch</span>" + "<input type='hidden' value='" + arrCaseID(i).ToString() + "' name='Case" + i.ToString() + "'/>"
                Else
                    Headertext = "Case#:" + arrCaseID(i).ToString() + "<br/>" + dsCaseDetails.Tables(0).Rows(0).Item("CaseDes").ToString() + "<input type='hidden' value='" + arrCaseID(i).ToString() + "' name='Case" + i.ToString() + "'/>"
                End If
                CaseDesp.Add(arrCaseID(i).ToString())
                HeaderTdSetting(tdHeader, DWidth, Headertext, 1, arrCaseID(i).ToString())
                trHeader.Controls.Add(tdHeader)

            Next
            tblComparision.Controls.Add(trHeader)


            For i = 1 To 1
                For j = 1 To 8
                    trInner = New TableRow()

                    Select Case j
                        Case 1
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Product Format", trInner, "AlterNateColor1")
                            trInner.ID = "P1"

                            For k = 0 To DataCnt
                                'Product Format
                                Dim Unit As String
                                Dim dsMat As New DataSet
                                tdInner = New TableCell
                                'Unit = dstbl.Tables(k).Rows(0).Item("Units").ToString()

                                dsMat = objGetData.GetProductFormt(dstbl.Tables(k).Rows(0).Item("M1").ToString(), "", "")
                                tdInner.Text = dsMat.Tables(0).Rows(0).Item("FormatDes").ToString()
                                'tdInner.Controls.Add(ddl)
                                'tdInner.Text = ddl.SelectedItem.Text
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 2

                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Input 1", trInner, "AlterNateColor2")
                            trInner.ID = "I11"
                            For k = 0 To DataCnt

                                'Input1
                                tdInner = New TableCell
                                lbl = New Label
                                ' txt = New TextBox
                                'txt.Text = FormatNumber(ds.Tables(0).Rows(i).Item("M2").ToString(), 3)
                                'TextBoxSetting(txt, "NormalTextBox")
                                lbl.Text = "<b>" + dstbl.Tables(k).Rows(0).Item("FORMAT_M1").ToString() + "</b>"
                                tdInner.Text = lbl.Text + "<br/>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("M2").ToString(), 3)
                                InnerTdSetting(tdInner, "", "Right")
                                'tdInner.Controls.Add(txt)
                                'tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)


                            Next

                        Case 3

                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Input 2", trInner, "AlterNateColor1")
                            trInner.ID = "I21"
                            For k = 0 To DataCnt

                                'Input2
                                tdInner = New TableCell
                                lbl = New Label
                                ' txt = New TextBox
                                'txt.Text = FormatNumber(ds.Tables(0).Rows(i).Item("M2").ToString(), 3)
                                'TextBoxSetting(txt, "NormalTextBox")
                                lbl.Text = "<b>" + dstbl.Tables(k).Rows(0).Item("FORMAT_M2").ToString() + "</b>"
                                tdInner.Text = lbl.Text + "<br/>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("M3").ToString(), 3)
                                InnerTdSetting(tdInner, "", "Right")
                                'tdInner.Controls.Add(txt)
                                'tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)

                            Next

                        Case 4

                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Input 3", trInner, "AlterNateColor2")
                            trInner.ID = "I31"
                            For k = 0 To DataCnt
                                'Input3
                                tdInner = New TableCell
                                lbl = New Label
                                ' txt = New TextBox
                                'txt.Text = FormatNumber(ds.Tables(0).Rows(i).Item("M2").ToString(), 3)
                                'TextBoxSetting(txt, "NormalTextBox")
                                lbl.Text = "<b>" + dstbl.Tables(k).Rows(0).Item("FORMAT_M3").ToString() + "</b>"
                                tdInner.Text = lbl.Text + "<br/>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("M4").ToString(), 3)
                                InnerTdSetting(tdInner, "", "Right")
                                'tdInner.Controls.Add(txt)
                                'tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)


                            Next


                        Case 5

                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Input 4", trInner, "AlterNateColor1")
                            trInner.ID = "I41"
                            For k = 0 To DataCnt
                                'Input4
                                tdInner = New TableCell
                                lbl = New Label
                                ' txt = New TextBox
                                'txt.Text = FormatNumber(ds.Tables(0).Rows(i).Item("M2").ToString(), 3)
                                'TextBoxSetting(txt, "NormalTextBox")
                                lbl.Text = "<b>" + dstbl.Tables(k).Rows(0).Item("FORMAT_M4").ToString() + "</b>"
                                tdInner.Text = lbl.Text + "<br/>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("M5").ToString(), 3)
                                InnerTdSetting(tdInner, "", "Right")
                                'tdInner.Controls.Add(txt)
                                'tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)

                            Next

                        Case 6

                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Input 5", trInner, "AlterNateColor2")
                            trInner.ID = "I51"
                            For k = 0 To DataCnt
                                'Input5
                                tdInner = New TableCell
                                lbl = New Label
                                lbl.Text = "<b>" + dstbl.Tables(k).Rows(0).Item("FORMAT_M5").ToString() + "</b>"
                                tdInner.Text = lbl.Text + "<br/>" + FormatNumber(dstbl.Tables(k).Rows(0).Item("M6").ToString(), 3)
                                InnerTdSetting(tdInner, "", "Right")
                                'tdInner.Controls.Add(txt)
                                'tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 7

                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("Title8").ToString() + ")"
                            LeftTdSetting(tdInner, "Packaging and Product Weight " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "PW1"
                            For k = 0 To DataCnt

                                'Container  Weight
                                tdInner = New TableCell
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("PRODUCTWEIGHT").ToString(), 4)
                                InnerTdSetting(tdInner, "", "Right")
                                trInner.Controls.Add(tdInner)
                            Next



                        Case 8

                            tdInner = New TableCell
                            Title = ""
                            LeftTdSetting(tdInner, "Roll Diameter" + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "RD" + i.ToString()
                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                If CDbl(dstbl.Tables(k).Rows(0).Item("ROLLDIA").ToString()) > CDbl(0) Then
                                    lbl.Text = "<b>" + dstbl.Tables(k).Rows(0).Item("TITLE9").ToString() + "</b><br />" + FormatNumber(dstbl.Tables(k).Rows(0).Item("ROLLDIA").ToString(), 3).ToString()
                                Else
                                    lbl.Text = "NA"
                                End If



                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next

                    End Select
                    If (j Mod 2 = 0) Then
                        trInner.CssClass = "AlterNateColor1"
                    Else
                        trInner.CssClass = "AlterNateColor2"
                    End If
                    tblComparision.Controls.Add(trInner)
                Next
            Next


        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub HeaderTdSetting(ByVal Td As TableCell, ByVal Width As String, ByVal HeaderText As String, ByVal ColSpan As String, ByVal CaseId As String)
        Try
            If CaseId.Trim() <> "" Then
                Td.Text = HeaderText
            End If

            Td.ColumnSpan = ColSpan
            If Width <> "" Then
                Td.Style.Add("width", Width)
            End If
            Td.CssClass = "TdHeading"
            Td.Height = 30
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




        Catch ex As Exception
            _lErrorLble.Text = "Error:InnerTdSetting:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub TextBoxSetting(ByVal txt As TextBox, ByVal Css As String)
        Try


            txt.CssClass = Css
            txt.Enabled = False
        Catch ex As Exception
            _lErrorLble.Text = "Error:TextBoxSetting:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub LeftTdSetting(ByVal Td As TableCell, ByVal Text As String, ByVal tr As TableRow, ByVal Css As String)
        Try
            Td.Text = Text
            InnerTdSetting(Td, "", "Left")
            tr.Controls.Add(Td)
            tr.CssClass = Css




        Catch ex As Exception
            _lErrorLble.Text = "Error:LeftTdSetting:" + ex.Message.ToString()
        End Try
    End Sub

End Class
