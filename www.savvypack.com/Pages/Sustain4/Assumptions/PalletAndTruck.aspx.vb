Imports System.Data
Imports System.Data.OleDb
Imports System
Imports S4GetData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Sustain4_Assumptions_PalletAndTruck
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
            Return CaseIds
        Catch ex As Exception
            Return CaseIds
            _lErrorLble.Text = "Error:GetCaseIds:" + ex.Message.ToString()
        End Try

    End Function


    Protected Sub GetPageDetails()

        Dim ds As New DataSet
        Dim dsCaseDetails As New DataSet
        Dim dstbl As New DataSet
        Dim objGetData As New S2GetData.Selectdata
        Dim CaseIds As String = String.Empty
        'Dim DataCnt As New Integer
        Dim i As New Integer
        Dim DWidth As String = String.Empty
        Dim arrCaseID() As String

        Try
            'CaseIds = GetCaseIds()
            arrCaseID = GetCaseIds()
            'DataCnt = ds.Tables(0).Rows.Count - 1
            DataCnt = arrCaseID.Length - 1

            Dim trHeader As New TableRow
            Dim tdHeader As TableCell
            Dim trInner As New TableRow
            Dim tdInner As TableCell
            Dim ddl As DropDownList
            Dim txt As TextBox
            Dim lbl As Label
            Dim Cunits As New Integer
            Dim Units As New Integer
            Dim Title As String = String.Empty
            Dim k As Integer
            Dim j As Integer

            DWidth = txtDWidth.Text + "px"


            tdHeader = New TableCell
            HeaderTdSetting(tdHeader, DWidth, "<img alt='' src='../../Images/spacer.gif' Style='width:200px;height:0px;'  />", 1, "")
            trHeader.Controls.Add(tdHeader)
            trHeader.Height = 20
            trHeader.CssClass = "PageSSHeading"

            For i = 0 To DataCnt
                ds = objGetData.GetPalletAndTruck(arrCaseID(i))
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

            For j = 1 To 14
                trInner = New TableRow()


                Select Case j
                    Case 1
                        tdInner = New TableCell
                        LeftTdSetting(tdInner, "<b>Pallet Dimensions</b>", trInner, "AlterNateColor4")
                        trInner.ID = "P" + j.ToString() + "_1"

                        For k = 0 To DataCnt
                            'Break1
                            tdInner = New TableCell
                            InnerTdSetting(tdInner, "", "Center")
                            tdInner.Text = "&nbsp;"
                            trInner.Controls.Add(tdInner)

                        Next

                    Case 2
                        tdInner = New TableCell
                        Title = "(" + dstbl.Tables(0).Rows(0).Item("Title9").ToString() + ")"
                        LeftTdSetting(tdInner, "Width " + Title + "", trInner, "AlterNateColor1")
                        trInner.ID = "PW1"
                        For k = 0 To DataCnt

                            'Pallet Width
                            tdInner = New TableCell
                            tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("M" + (j - 1).ToString() + "").ToString(), 0)
                            InnerTdSetting(tdInner, "", "Right")
                            'tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Next

                    Case 3


                        tdInner = New TableCell
                        Title = "(" + dstbl.Tables(0).Rows(0).Item("Title9").ToString() + ")"
                        LeftTdSetting(tdInner, "Length " + Title + "", trInner, "AlterNateColor2")
                        trInner.ID = "PL1"
                        For k = 0 To Datacnt
                            'Pallet Length
                            tdInner = New TableCell
                            tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("M" + (j - 1).ToString() + "").ToString(), 0)
                            InnerTdSetting(tdInner, "", "Right")
                            'tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Next

                    Case 4

                        tdInner = New TableCell
                        Title = "(" + dstbl.Tables(0).Rows(0).Item("Title9").ToString() + ")"
                        LeftTdSetting(tdInner, "Height " + Title + "", trInner, "AlterNateColor1")
                        trInner.ID = "PH1"
                        For k = 0 To Datacnt
                            'Pallet Height
                            tdInner = New TableCell
                            tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("M" + (j - 1).ToString() + "").ToString(), 0)
                            InnerTdSetting(tdInner, "", "Right")
                            'tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)

                        Next

                    Case 5
                        tdInner = New TableCell
                        Title = "(Number)"
                        LeftTdSetting(tdInner, "Cartons Per Pallet " + Title + "", trInner, "AlterNateColor2")
                        trInner.ID = "CPP1"

                        For k = 0 To Datacnt

                            'Cartons Per Pallet
                            tdInner = New TableCell
                            tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("M" + (j - 1).ToString() + "").ToString(), 0)
                            InnerTdSetting(tdInner, "", "Right")
                            'tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)

                        Next


                    Case 6
                        tdInner = New TableCell
                        Title = "(Number)"
                        LeftTdSetting(tdInner, "Product Per Pallet " + Title + "", trInner, "AlterNateColor1")
                        trInner.ID = "PPP1"
                        For k = 0 To Datacnt

                            'Product Per Pallet
                            tdInner = New TableCell
                            ' txt = New TextBox
                            'txt.Text = FormatNumber(ds.Tables(0).Rows(i).Item("PRODUCTNUMBER").ToString(), 0)
                            'TextBoxSetting(txt, "NormalTextBox")
                            tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("M" + (j - 1).ToString() + "").ToString(), 0)
                            InnerTdSetting(tdInner, "", "Right")
                            'tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)

                        Next

                    Case 7

                        tdInner = New TableCell
                        LeftTdSetting(tdInner, "<b>Truck Dimensions</b>", trInner, "AlterNateColor4")
                        trInner.ID = "T" + j.ToString() + "_1"

                        For k = 0 To Datacnt

                            'Break2
                            tdInner = New TableCell
                            InnerTdSetting(tdInner, "", "Center")
                            tdInner.Text = "&nbsp;"
                            trInner.Controls.Add(tdInner)

                        Next


                    Case 8

                        tdInner = New TableCell
                        Title = "(" + dstbl.Tables(0).Rows(0).Item("Title9").ToString() + ")"
                        LeftTdSetting(tdInner, "Width " + Title + "", trInner, "AlterNateColor2")
                        trInner.ID = "TW1"
                        For k = 0 To Datacnt
                            'Truck Width
                            tdInner = New TableCell
                            ' txt = New TextBox
                            'txt.Text = FormatNumber(ds.Tables(0).Rows(i).Item("TRUCKWIDTH").ToString(), 0)
                            'TextBoxSetting(txt, "NormalTextBox")
                            tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("T" + (j - 7).ToString() + "").ToString(), 0)
                            InnerTdSetting(tdInner, "", "Right")
                            'tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)

                        Next

                    Case 9


                        tdInner = New TableCell
                        Title = "(" + dstbl.Tables(0).Rows(0).Item("Title9").ToString() + ")"
                        LeftTdSetting(tdInner, "Length " + Title + "", trInner, "AlterNateColor1")
                        trInner.ID = "TL1"

                        For k = 0 To Datacnt

                            'Truck Length
                            tdInner = New TableCell
                            ' txt = New TextBox
                            'txt.Text = FormatNumber(ds.Tables(0).Rows(i).Item("TRUCKLENGTH").ToString(), 0)
                            'TextBoxSetting(txt, "NormalTextBox")
                            tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("T" + (j - 7).ToString() + "").ToString(), 0)
                            InnerTdSetting(tdInner, "", "Right")
                            'tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)



                        Next



                    Case 10

                        tdInner = New TableCell
                        Title = "(" + dstbl.Tables(0).Rows(0).Item("Title9").ToString() + ")"
                        LeftTdSetting(tdInner, "Height " + Title + "", trInner, "AlterNateColor2")
                        trInner.ID = "TH1"

                        For k = 0 To Datacnt

                            'Truck Height
                            tdInner = New TableCell
                            ' txt = New TextBox
                            'txt.Text = FormatNumber(ds.Tables(0).Rows(i).Item("TRUCKHEIGHT").ToString(), 0)
                            'TextBoxSetting(txt, "NormalTextBox")
                            tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("T" + (j - 7).ToString() + "").ToString(), 0)
                            InnerTdSetting(tdInner, "", "Right")
                            'tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Next


                    Case 11

                        tdInner = New TableCell
                        Title = "(" + dstbl.Tables(0).Rows(0).Item("Title8").ToString() + ")"
                        LeftTdSetting(tdInner, "Weight Limit " + Title + "", trInner, "AlterNateColor1")
                        trInner.ID = "WL1"

                        For k = 0 To Datacnt
                            'Weight Limit
                            tdInner = New TableCell
                            ' txt = New TextBox
                            'txt.Text = FormatNumber(ds.Tables(0).Rows(i).Item("TRUCKWEIGHTLIMIT").ToString(), 0)
                            'TextBoxSetting(txt, "NormalTextBox")
                            tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("T" + (j - 7).ToString() + "").ToString(), 0)
                            InnerTdSetting(tdInner, "", "Right")
                            'tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Next



                    Case 12

                        tdInner = New TableCell
                        Title = "(Number)"
                        LeftTdSetting(tdInner, "Pallets Per Truck " + Title + "", trInner, "AlterNateColor2")
                        trInner.ID = "PPT1"
                        For k = 0 To Datacnt

                            'Pallets Per Truck
                            tdInner = New TableCell
                            ' txt = New TextBox
                            'txt.Text = FormatNumber(ds.Tables(0).Rows(i).Item("TRUCKNUMBER").ToString(), 0)
                            'TextBoxSetting(txt, "NormalTextBox")
                            tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("T" + (j - 7).ToString() + "").ToString(), 0)
                            InnerTdSetting(tdInner, "", "Right")
                            'tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)

                        Next


                    Case 13

                        tdInner = New TableCell
                        LeftTdSetting(tdInner, "<b>Calculated Weight</b>", trInner, "AlterNateColor4")
                        trInner.ID = "Calculate" + j.ToString() + "_1"

                        For k = 0 To Datacnt


                            'Break3
                            tdInner = New TableCell
                            InnerTdSetting(tdInner, "", "Center")
                            tdInner.Text = "&nbsp;"
                            trInner.Controls.Add(tdInner)



                        Next

                    Case 14

                        tdInner = New TableCell
                        Title = "(" + dstbl.Tables(0).Rows(0).Item("Title8").ToString() + ")"
                        LeftTdSetting(tdInner, "Calculated Weight " + Title + "", trInner, "AlterNateColor1")
                        trInner.ID = "CW1"
                        For k = 0 To Datacnt

                            'Calculated Weight
                            tdInner = New TableCell
                            ' txt = New TextBox
                            'txt.Text = FormatNumber(ds.Tables(0).Rows(i).Item("CALCULATEDWEIGHT").ToString(), 0)
                            'TextBoxSetting(txt, "NormalTextBox")
                            tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("CALWEIGHT").ToString(), 0)
                            InnerTdSetting(tdInner, "", "Right")
                            'tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Next
                End Select

                tblComparision.Controls.Add(trInner)


            Next


        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub




    Protected Sub HeaderTdSetting(ByVal Td As TableCell, ByVal Width As String, ByVal HeaderText As String, ByVal ColSpan As String, ByVal CaseId As String)
        Try

            Td.Text = HeaderText

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
