Imports System.Data
Imports System.Data.OleDb
Imports System
Imports E4GetData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Econ4_Results_Resultspl1
    Inherits System.Web.UI.Page

#Region "Get Set Variables"
    Dim _lErrorLble As Label
    Dim _strUserName As String
    Dim _strPassword As String
    Dim _iAssumptionId As Integer
    Dim _btnUpdate As ImageButton
    Dim _btnCompare As ImageButton



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

    Public Property Updatebtn() As ImageButton
        Get
            Return _btnUpdate
        End Get
        Set(ByVal value As ImageButton)
            _btnUpdate = value
        End Set
    End Property
    Public Property Comparebtn() As ImageButton
        Get
            Return _btnCompare
        End Get
        Set(ByVal value As ImageButton)
            _btnCompare = value
        End Set
    End Property

    Public DataCnt As Integer
    Public CaseDesp As New ArrayList

#End Region

#Region "MastePage Content Variables"
    Protected Sub GetErrorLable()
        ErrorLable = Page.Master.FindControl("lblError")
    End Sub

    Protected Sub GetUpdatebtn()
        Updatebtn = Page.Master.FindControl("imgUpdate")
        Updatebtn.Visible = False
    End Sub
    Protected Sub GetComparebtn()
        Dim obj As New CryptoHelper()
        Comparebtn = Page.Master.FindControl("imgCompare")
        Comparebtn.Visible = True
        Dim objCryptoHelper As New CryptoHelper()
        Comparebtn.OnClientClick = "return Compare('../../../Charts/E4Charts/CPrftCost.aspx?CaseId=" + obj.Encrypt(AssumptionId) + "&PrftCost=" + objCryptoHelper.Encrypt("PRFT") + "&ChartType=" + objCryptoHelper.Encrypt("RBC") + "&IsDep=" + objCryptoHelper.Encrypt("N") + "&CType=" + objCryptoHelper.Encrypt("Total") + "') "

    End Sub


#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Try
                GetErrorLable()
                GetUpdatebtn()
                GetSessionDetails()
                GetPageDetails()
                GetComparebtn()
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
            AssumptionId = Session("AssumptionID")

            lblAID.Text = AssumptionId
            lblAdes.Text = Session("Description")

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetSessionDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Function GetCaseIds() As String()
        Dim CaseIds(0) As String
        Dim objGetData As New E4GetData.Selectdata
        Try
            CaseIds = objGetData.Cases(AssumptionId)
            Return CaseIds
        Catch ex As Exception
            Return CaseIds
            _lErrorLble.Text = "Error:GetCaseIds:" + ex.Message.ToString()
        End Try

    End Function

    Protected Sub GetPageDetails()
        Dim dstbl As New DataSet
        Dim dsCaseDetails As New DataSet
        Dim objGetData As New E2GetData.Selectdata
        Dim CaseIds As String = String.Empty
        Dim i As New Integer
        Dim j As New Integer
        Dim k As New Integer
        Dim DWidth As String = String.Empty
        Dim arrCaseID() As String


        Try
            arrCaseID = GetCaseIds()
            DataCnt = arrCaseID.Length - 1

            Dim trHeader As New TableRow
            Dim tdHeader As TableCell
            Dim tdInner As TableCell
            Dim trInner As New TableRow
            Dim ddl As DropDownList
            Dim lbl As New Label
            Dim txt As TextBox
            Dim Cunits As New Integer
            Dim Units As New Integer
            Dim Title As String = String.Empty
            Dim CurrTitle As String = String.Empty
            Dim CCurrTitle As String = String.Empty
            Dim UnitError As String = String.Empty
            Dim CurrError As String = String.Empty
            Dim strText As String = String.Empty
            Dim strColor As String = String.Empty
            DWidth = txtDWidth.Text + "px"


            tdHeader = New TableCell
            HeaderTdSetting(tdHeader, "200px", "<img alt='' src='../../Images/spacer.gif' Style='width:200px;height:0px;'  />", 1)
            trHeader.Controls.Add(tdHeader)
            trHeader.Height = 20
            trHeader.CssClass = "PageSSHeading"

            For i = 0 To DataCnt
                Dim ds As New DataSet
                ds = objGetData.GetProfitAndLossDetails(arrCaseID(i), False)
                ds.Tables(0).TableName = arrCaseID(i).ToString()
                dstbl.Tables.Add(ds.Tables(arrCaseID(i).ToString()).Copy())
            Next





            For i = 0 To DataCnt
                dsCaseDetails = objGetData.GetCaseDetails(arrCaseID(i).ToString())
                Cunits = Convert.ToInt32(dstbl.Tables(0).Rows(0).Item("Units").ToString())
                Units = Convert.ToInt32(dstbl.Tables(i).Rows(0).Item("Units").ToString())
                CCurrTitle = dstbl.Tables(0).Rows(0).Item("Title2").ToString().Trim()
                CurrTitle = dstbl.Tables(i).Rows(0).Item("Title2").ToString().Trim()


                tdHeader = New TableCell
                Dim Headertext As String = String.Empty
                If Cunits <> Units Then
                    UnitError = "<br/> <span  style='color:red'>Unit Mismatch</span>"
                Else
                    UnitError = ""
                End If

                If CCurrTitle <> CurrTitle Then
                    CurrError = "<br/> <span  style='color:red'>Currency Mismatch</span>"
                Else
                    CurrError = ""
                End If

                Headertext = "Case#:" + arrCaseID(i).ToString() + "<br/>" + dsCaseDetails.Tables(0).Rows(0).Item("CaseDes").ToString() + UnitError + CurrError + "<input type='hidden' value='" + arrCaseID(i).ToString() + "' name='Case" + i.ToString() + "'/>"
                CaseDesp.Add(arrCaseID(i).ToString())
                HeaderTdSetting(tdHeader, DWidth, Headertext, 1)
                trHeader.Controls.Add(tdHeader)


            Next
            tblComparision.Controls.Add(trHeader)

            ' Setting Sales Volume
            For i = 0 To 1
                trInner = New TableRow()
                tdInner = New TableCell
                trInner.ID = "SVOL" + (i + 1).ToString() + "_" + (i + 1).ToString()
                If (i Mod 2 = 0) Then
                    strColor = "AlterNateColor1"
                Else
                    strColor = "AlterNateColor2"
                End If
                If i = 0 Then
                    Title = "<b>" + dstbl.Tables(0).Rows(0).Item("VOLUNI1").ToString() + "</b>"
                Else
                    Title = "<b>" + dstbl.Tables(0).Rows(0).Item("VOLUNI2").ToString() + "</b> "
                End If
                LeftTdSetting(tdInner, Title, trInner, strColor)
                For k = 0 To DataCnt
                    tdInner = New TableCell
                    Dim str As String = ""
                    If i = 0 Then
                        str = FormatNumber(dstbl.Tables(k).Rows(0).Item("SALESVOLUMELB").ToString(), 0).ToString()
                    Else
                        str = FormatNumber(dstbl.Tables(k).Rows(0).Item("SALESVOLUMEUNIT").ToString(), 0).ToString()
                    End If


                    tdInner.Text = str
                    InnerTdSetting(tdInner, "", "Right")
                    tdInner.Style.Add("padding-right", "15px")
                    trInner.Controls.Add(tdInner)
                Next
                tblComparision.Controls.Add(trInner)
            Next





            ' Setting first heading
            trInner = New TableRow()
            tdInner = New TableCell
            LeftTdSetting(tdInner, "", trInner, "AlterNateColor4")
            For k = 0 To DataCnt
                tdInner = New TableCell
                Dim tbl As New Table
                Dim tr As New TableRow
                Dim td As TableCell
                Dim cnt As Integer
                tbl.Style.Add("Width", "75%")
                For cnt = 0 To 1
                    td = New TableCell
                    If cnt = 0 Then
                        Title = "(" + dstbl.Tables(k).Rows(0).Item("TITLE2").ToString() + ")"
                    Else
                        Title = "(% of Total)"
                    End If
                    td.Text = Title
                    td.Style.Add("text-align", "Right")
                    tr.Controls.Add(td)
                Next
                tbl.Controls.Add(tr)
                tdInner.Controls.Add(tbl)
                InnerTdSetting(tdInner, "", "Center")
                trInner.Controls.Add(tdInner)
            Next
            tblComparision.Controls.Add(trInner)

            For i = 1 To 24

                For j = 1 To 1
                    trInner = New TableRow()
                    Select Case j
                        Case 1
                            tdInner = New TableCell
                            If (i Mod 2 = 0) Then
                                strColor = "AlterNateColor2"
                            Else
                                strColor = "AlterNateColor1"
                            End If
                            If i = 1 Or i = 7 Or i = 25 Then
                                strText = "<b>" + dstbl.Tables(0).Rows(0).Item("DES" + i.ToString()) + "</b>"
                            Else
                                strText = "<span style='margin-left:20px;'><b>" + dstbl.Tables(0).Rows(0).Item("DES" + i.ToString()) + "</b></span>"
                            End If

                            LeftTdSetting(tdInner, strText, trInner, strColor)
                            trInner.ID = "PDES" + i.ToString() + "_" + i.ToString()
                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                Dim tbl As New Table
                                Dim tr As New TableRow
                                Dim td As TableCell
                                Dim cnt As Integer
                                Dim str As String = ""
                                tbl.Style.Add("Width", "100%")
                                For cnt = 0 To 1
                                    td = New TableCell
                                    td.ColumnSpan = "2"
                                    If cnt = 0 Then
                                        str = FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("PL" + i.ToString() + "").ToString()) * CDbl(dstbl.Tables(k).Rows(0).Item("CURR").ToString()), 0)
                                    Else
                                        Dim percentage As New Decimal
                                        If CDbl(dstbl.Tables(k).Rows(0).Item("PL1").ToString()) > 0 Then
                                            percentage = CDbl(dstbl.Tables(k).Rows(0).Item("PL" + i.ToString() + "").ToString()) / CDbl(dstbl.Tables(k).Rows(0).Item("PL1").ToString()) * 100
                                            str = FormatNumber(percentage, 2)
                                        End If
                                    End If
                                    td.Text = str
                                    InnerTdSetting(td, "50%", "Right")
                                    tr.Controls.Add(td)
                                Next
                                tbl.Controls.Add(tr)
                                tdInner.Controls.Add(tbl)
                                InnerTdSetting(tdInner, "", "Right")
                                trInner.Controls.Add(tdInner)
                            Next


                    End Select
                    tblComparision.Controls.Add(trInner)
                Next

            Next



        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
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
            Td.Height = 30
            Td.HorizontalAlign = HorizontalAlign.Center



        Catch ex As Exception

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
                Td.Style.Add("padding-right", "18px")
            End If




        Catch ex As Exception

        End Try
    End Sub

    Protected Sub TextBoxSetting(ByVal txt As TextBox, ByVal Css As String, ByVal CaseId As Integer)
        Try
            txt.CssClass = Css
            If CaseId <= 1000 And Session("Password") <> "9krh65sve3" Then
                txt.Enabled = False
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub LableSetting(ByVal lbl As Label, ByVal Css As String)
        Try
            lbl.CssClass = Css

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub LeftTdSetting(ByVal Td As TableCell, ByVal Text As String, ByVal tr As TableRow, ByVal Css As String)
        Try
            Td.Text = Text
            InnerTdSetting(Td, "", "Left")
            tr.Controls.Add(Td)
            tr.CssClass = Css




        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub
End Class
