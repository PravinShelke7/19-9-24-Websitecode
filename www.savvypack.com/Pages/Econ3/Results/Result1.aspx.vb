Imports System.Data
Imports System.Data.OleDb
Imports System
Imports E3GetData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Web.UI.HtmlTextWriter
Imports System.Security.Cryptography
Partial Class Pages_Econ3_Results_Result1
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
        Comparebtn.OnClientClick = "return Compare('../../../Charts/E3Charts/CPrftCost.aspx?CaseId=" + obj.Encrypt(AssumptionId) + "&PrftCost=" + objCryptoHelper.Encrypt("PRFT") + "&ChartType=" + objCryptoHelper.Encrypt("RBC") + "&IsDep=" + objCryptoHelper.Encrypt("N") + "&CType=" + objCryptoHelper.Encrypt("Total") + "') "

    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Try
                GetErrorLable()
                GetUpdatebtn()
                GetSessionDetails()
                GetComparebtn()
                GetPageDetails()
                If Not IsPostBack Then

                    If Request.Cookies("W1") Is Nothing Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Layers", "removeSession('SSS');", True)
                    Else
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Layers", "removeSession('" + Request.Cookies("W1").Value + "');", True)
                    End If
                End If
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
        Dim objGetData As New E3GetData.Selectdata
        Try
            CaseIds = objGetData.Cases(AssumptionId)
            Return CaseIds
        Catch ex As Exception
            Return CaseIds
            _lErrorLble.Text = "Error:GetCaseIds:" + ex.Message.ToString()
        End Try

    End Function

    Protected Sub GetPageDetails()
        Dim dsMat As DataSet
        Dim dstbl As New DataSet
        Dim dsPreftbl As New DataSet
        Dim dsSugtbl As New DataSet
        Dim dsRestbl As New DataSet
        Dim dsCaseDetails As New DataSet
        Dim objGetData As New E1GetData.Selectdata
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

            Dim objGetDataW As New E3GetData.Selectdata
            Dim dsW As New DataSet

            dsW = objGetDataW.CompCOLWIDTH(AssumptionId)
            If dsW.Tables(0).Rows.Count > 0 Then
                txtDWidth.Text = dsW.Tables(0).Rows(0).Item("COLWIDTH").ToString()
            End If
            DWidth = txtDWidth.Text + "px"


            tdHeader = New TableCell
            HeaderTdSetting(tdHeader, "200px", "<img alt='' src='../../Images/spacer.gif' Style='width:200px;height:0px;'  />", 1)
            trHeader.Controls.Add(tdHeader)
            trHeader.Height = 20
            trHeader.CssClass = "PageSSHeading"
            DsMat = objGetData.GetMaterials("-1", "", "")
            For i = 0 To DataCnt
                Dim ds As New DataSet
                Dim dsRes As New DataSet
                Dim dsPref As New DataSet
                Dim dsSug As New DataSet

                dsRes = objGetData.GetProfitAndLossDetails(arrCaseID(i), False)
                dsRes.Tables(0).TableName = arrCaseID(i).ToString()
                dsRestbl.Tables.Add(dsRes.Tables(arrCaseID(i).ToString()).Copy())

                ds = objGetData.GetDirectMaterialDetailsE3(arrCaseID(i))
                ds.Tables(0).TableName = arrCaseID(i).ToString()
                dstbl.Tables.Add(ds.Tables(arrCaseID(i).ToString()).Copy())

            Next





            For i = 0 To DataCnt
                dsCaseDetails = objGetData.GetCaseDetails(arrCaseID(i).ToString())
                Cunits = Convert.ToInt32(dsRestbl.Tables(0).Rows(0).Item("Units").ToString())
                Units = Convert.ToInt32(dsRestbl.Tables(i).Rows(0).Item("Units").ToString())
                CCurrTitle = dsRestbl.Tables(0).Rows(0).Item("Title2").ToString().Trim()
                CurrTitle = dsRestbl.Tables(i).Rows(0).Item("Title2").ToString().Trim()


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

                Headertext = "Case#:" + arrCaseID(i).ToString() + " " + dsCaseDetails.Tables(0).Rows(0).Item("COUNTRYDES").ToString() + "<br/>" + dsCaseDetails.Tables(0).Rows(0).Item("CaseDes").ToString() + UnitError + CurrError + "<input type='hidden' value='" + arrCaseID(i).ToString() + "' name='Case" + i.ToString() + "'/>"
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
                    Title = "<b>Sales Volume (" + dsRestbl.Tables(0).Rows(0).Item("TITLE8").ToString() + ")</b>"
                Else
                    Title = "<b>Sales Volume (" + dsRestbl.Tables(0).Rows(0).Item("SUNITLBL").ToString() + ")</b> "
                End If
                LeftTdSetting(tdInner, Title, trInner, strColor)
                For k = 0 To DataCnt
                    tdInner = New TableCell
                    Dim str As String = ""
                    If i = 0 Then
                        str = FormatNumber(dsRestbl.Tables(k).Rows(0).Item("SVOLUME").ToString(), 0).ToString()
                    Else
                        str = FormatNumber(dsRestbl.Tables(k).Rows(0).Item("SUNITVAL").ToString(), 0).ToString()
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
                tbl.Style.Add("Width", "100%")
                For cnt = 0 To 1
                    td = New TableCell
                    If cnt = 0 Then
                        Title = "Substrate"
                        InnerTdSetting(td, "75%", "Left")
                    Else
                        Title = "(" + dstbl.Tables(k).Rows(0).Item("TITLE2").ToString() + ")"
                        InnerTdSetting(td, "25%", "Left")

                    End If
                    td.Text = Title                    
                    tr.Controls.Add(td)
                Next
               
                tbl.Controls.Add(tr)
                tdInner.Controls.Add(tbl)
                InnerTdSetting(tdInner, "", "Center")
                trInner.Controls.Add(tdInner)
            Next
            tblComparision.Controls.Add(trInner)

            For i = 1 To 17
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
                            trInner.ID = "MDES" + i.ToString() + "_" + i.ToString()
                            tdInner.Font.Bold = True

                            If i <= 10 Then
                                strText = "Material" + i.ToString()
                                LeftTdSetting(tdInner, strText, trInner, strColor)
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
                                        lbl = New Label

                                        If cnt = 0 Then
                                            lbl.Width = "200"
                                            If dstbl.Tables(0).Rows(0).Item("MATDES" + i.ToString()).ToString() <> "" Then
                                                lbl.Text = dstbl.Tables(k).Rows(0).Item("MATDES" + i.ToString())
                                            Else
                                                lbl.Text = dstbl.Tables(k).Rows(0).Item("MATS" + i.ToString())
                                            End If
                                            'lbl.Font.Bold = True
                                            InnerTdSetting(td, "50%", "left")

                                        Else
                                            If dstbl.Tables(k).Rows(0).Item("PRODZ" + i.ToString() + "").ToString() <> "" Then
                                                lbl.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("PRODZ" + i.ToString() + "").ToString(), 0)
                                            Else
                                                lbl.Text = "0"
                                            End If
                                            InnerTdSetting(td, "50%", "Right")
                                        End If

                                        td.Controls.Add(lbl)
                                        tr.Controls.Add(td)
                                    Next
                                    tbl.Controls.Add(tr)
                                    tdInner.Controls.Add(tbl)
                                    InnerTdSetting(tdInner, "", "Right")
                                    trInner.Controls.Add(tdInner)

                                Next

                            Else
                                strText = dstbl.Tables(0).Rows(0).Item("Des" + (i - 10).ToString()).ToString()
                                LeftTdSetting(tdInner, strText, trInner, strColor)
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
                                        lbl = New Label

                                        If cnt = 0 Then

                                        Else
                                            If dstbl.Tables(k).Rows(0).Item("Col1" + (i - 10).ToString() + "").ToString() <> "" Then
                                                lbl.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("Col1" + (i - 10).ToString() + "").ToString(), 0)
                                            Else
                                                lbl.Text = "0"
                                            End If
                                            InnerTdSetting(td, "50%", "Right")
                                        End If

                                        td.Controls.Add(lbl)
                                        tr.Controls.Add(td)
                                    Next
                                    tbl.Controls.Add(tr)
                                    tdInner.Controls.Add(tbl)
                                    InnerTdSetting(tdInner, "", "Right")
                                    trInner.Controls.Add(tdInner)

                                Next
                            End If
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
