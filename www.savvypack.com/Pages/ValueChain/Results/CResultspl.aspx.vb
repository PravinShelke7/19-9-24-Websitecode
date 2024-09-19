Imports System
Imports System.Data
Imports VChainGetData
Imports VChainUpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Partial Class Pages_ValueChain_Results_CResultspl
    Inherits System.Web.UI.Page
#Region "Get Set Variables"
    Dim _lErrorLble As Label
    Dim _strUserName As String
    Dim _strPassword As String
    Dim _iValueChainId As Integer
    Dim _strUserRole As String
    Dim _btnUpdate As ImageButton
    Dim _strType As String

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

    Public Property ValueChainId() As Integer
        Get
            Return _iValueChainId
        End Get
        Set(ByVal Value As Integer)
            _iValueChainId = Value
        End Set
    End Property
    Public Property UserRole() As String
        Get
            Return _strUserRole
        End Get
        Set(ByVal Value As String)
            _strUserRole = Value
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
    Public Property Type() As String
        Get
            Return _strType
        End Get
        Set(ByVal Value As String)
            _strType = Value
        End Set
    End Property
#End Region

#Region "MastePage Content Variables"
    Protected Sub GetMasterPageControls()
        GetErrorLable()
        GetUpdatebtn()
    End Sub
    Protected Sub GetErrorLable()
        ErrorLable = Page.Master.FindControl("lblError")
    End Sub
    Protected Sub GetUpdatebtn()
        Updatebtn = Page.Master.FindControl("imgUpdate")
        'Updatebtn.Attributes.Add("onclick", "return checkNumericAll();")
        Updatebtn.Visible = False
        'AddHandler Updatebtn.Click, AddressOf Update_Click
    End Sub
#End Region
#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_PAGES_VALUECHAIN_ASSUMPTIONS_EXTRUSION")
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        objRefresh.Render(Page)
    End Sub

#End Region
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            GetMasterPageControls()
            GetSessionDetails()
            Dim obj As New CryptoHelper()
            lblModName.Text = obj.Decrypt(Request.QueryString("ModName").ToString())
            lblCaseId.Text = obj.Decrypt(Request.QueryString("CaseId").ToString())
            GetCaseDescription(lblModName.Text, lblCaseId.Text)
            Type = obj.Decrypt(Request.QueryString("Type").ToString())
            If Not IsPostBack Then
                GetPageDetails()
            End If
        Catch ex As Exception
            ErrorLable.Text = "Error:Page_Load:" + ex.Message.ToString() + ""
        End Try
    End Sub
    Protected Sub GetCaseDescription(ByVal ModName As String, ByVal CaseId As String)
        Dim Ds As New DataSet
        Dim ObjGetdata As New VChainGetData.Selectdata()
        Try
            Ds = ObjGetdata.GetCases(CaseId, (CInt(CaseId) + 1).ToString(), "", "", Session("USERNAME").ToString(), ModName)
            lblCaseDesc.Text = Ds.Tables(0).Rows(0)("CASDES").ToString()
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetCaseDescription:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub GetSessionDetails()
        Dim dt As New DataTable
        Dim objGetData As New Selectdata
        Try
            UserName = Session("UserName")
            Password = Session("Password")
            ValueChainId = Session("ValueChainId")
            dt = objGetData.GetDescription(ValueChainId)
            Session("VALUECHAINNAME") = dt.Rows(0).Item("VALUECHAINNAME")
            lblAID.Text = ValueChainId
            lblAdes.Text = Session("VALUECHAINNAME")

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetSessionDetails:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub GetPageDetails()
        Dim ds1 As New DataSet
        Dim ds2 As New DataSet
        Dim CaseIds As String = String.Empty


        Dim objVChainGetData As New VChainGetData.Selectdata
        Dim obj As New CryptoHelper()
        Try
            ds2 = objVChainGetData.GetProfitAndLossDetails(ValueChainId, lblCaseId.Text, lblModName.Text)

            If lblModName.Text.ToUpper() = "ECON2" And Type = "TOT" Then
                Dim objGetData As New E2GetData.Selectdata
                ds1 = objGetData.GetProfitAndLossDetails(lblCaseId.Text, False)
                GetByTotalEcon2(ds1, ds2)
            ElseIf lblModName.Text.ToUpper() = "ECON1" And Type = "TOT" Then
                Dim objGetData As New E1GetData.Selectdata
                ds1 = objGetData.GetProfitAndLossDetails(lblCaseId.Text, False)
                GetByTotalEcon1(ds1, ds2)
          
            ElseIf lblModName.Text.ToUpper() = "ECON2" And Type = "BYWT" Then
                Dim objGetData As New E2GetData.Selectdata
                ds1 = objGetData.GetProfitAndLossDetails(lblCaseId.Text, False)
                GetByWeightEcon2(ds1, ds2)
            ElseIf lblModName.Text.ToUpper() = "ECON1" And Type = "BYWT" Then
                Dim objGetData As New E1GetData.Selectdata
                ds1 = objGetData.GetProfitAndLossDetails(lblCaseId.Text, False)
                GetByWeightEcon1(ds1, ds2)
            
            ElseIf lblModName.Text.ToUpper() = "ECON2" And Type = "BYVOL" Then
                Dim objGetData As New E2GetData.Selectdata
                ds1 = objGetData.GetProfitAndLossDetails(lblCaseId.Text, False)
                GetByVolumeEcon2(ds1, ds2)
            ElseIf lblModName.Text.ToUpper() = "ECON1" And Type = "BYVOL" Then
                Dim objGetData As New E1GetData.Selectdata
                ds1 = objGetData.GetProfitAndLossDetails(lblCaseId.Text, False)
                GetByVolumeEcon1(ds1, ds2)
          
            End If




         

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub GetByTotalEcon1(ByVal ds1 As DataSet, ByVal ds2 As DataSet)

        Dim i As New Integer
        Dim j As New Integer
        Dim DWidth As String = String.Empty
        Dim trHeader As New TableRow
        Dim trHeader1 As New TableRow
        Dim trHeader2 As New TableRow
        Dim trHeader3 As New TableRow
        Dim trHeader4 As New TableRow
        Dim trHeader0 As New TableRow
        Dim trInner As New TableRow

        Dim tdHeader As TableCell
        Dim tdHeader1 As TableCell
        Dim tdHeader2 As TableCell
        Dim tdHeader3 As TableCell
        Dim tdHeader4 As TableCell
        Dim tdHeader0 As TableCell

        Dim lbl As New Label
        Dim hid As New HiddenField
        Dim Link As New HyperLink
        Dim txt As New TextBox
        Dim tdInner As TableCell

        Try

            For i = 1 To 7
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                tdHeader3 = New TableCell
                tdHeader4 = New TableCell
                tdHeader0 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader0, "200px", "Module:", "1")
                        HeaderTdSetting(tdHeader, "200px", "", "1")
                        HeaderTdSetting(tdHeader1, "", "Case Id:", "1")
                        HeaderTdSetting(tdHeader2, "", "Package Format:", "1")
                        HeaderTdSetting(tdHeader3, "", "Unique Feature:", "1")
                        HeaderTdSetting(tdHeader4, "", "Units:", "1")
                        trHeader0.Controls.Add(tdHeader0)
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader3.Controls.Add(tdHeader3)
                        trHeader4.Controls.Add(tdHeader4)

                        tdHeader0.HorizontalAlign = HorizontalAlign.Right
                        tdHeader1.HorizontalAlign = HorizontalAlign.Right
                        tdHeader2.HorizontalAlign = HorizontalAlign.Right
                        tdHeader3.HorizontalAlign = HorizontalAlign.Right
                        tdHeader4.HorizontalAlign = HorizontalAlign.Right
                    Case 2
                        HeaderTdSetting(tdHeader0, "200px", lblModName.Text, "1")
                        HeaderTdSetting(tdHeader, "150px", "Total", "1")
                        Header2TdSetting(tdHeader1, "", "" + ds1.Tables(0).Rows(0).Item("CaseId").ToString() + "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        Header2TdSetting(tdHeader3, "", "", "1")
                        GetCaseDetailsEcon1(tdHeader2, tdHeader3, ds1.Tables(0).Rows(0).Item("CaseId").ToString())
                        Header2TdSetting(tdHeader4, "", "(" + ds1.Tables(0).Rows(0).Item("TITLE2").ToString() + ")", "1")
                        trHeader0.Controls.Add(tdHeader0)
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader3.Controls.Add(tdHeader3)
                        trHeader4.Controls.Add(tdHeader4)
                    Case 3
                        HeaderTdSetting(tdHeader0, "200px", "Value Chain", "1")
                        HeaderTdSetting(tdHeader, "150px", "Total", "1")
                        Header2TdSetting(tdHeader1, "", "" + ds2.Tables(0).Rows(0).Item("CaseId").ToString() + "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        Header2TdSetting(tdHeader3, "", "", "1")
                        GetCaseDetailsEcon1(tdHeader2, tdHeader3, ds2.Tables(0).Rows(0).Item("CaseId").ToString())
                        Header2TdSetting(tdHeader4, "", "(" + ds2.Tables(0).Rows(0).Item("TITLE2").ToString() + ")", "1")
                        trHeader0.Controls.Add(tdHeader0)
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader3.Controls.Add(tdHeader3)
                        trHeader4.Controls.Add(tdHeader4)
                    Case 4
                        HeaderTdSetting(tdHeader0, "200px", "(Value Chain /" + lblModName.Text + ")", "1")
                        HeaderTdSetting(tdHeader, "200px", "Total Comparison", "1")
                        Header2TdSetting(tdHeader1, "", "(" + ds2.Tables(0).Rows(0).Item("CaseId").ToString() + "/" + ds1.Tables(0).Rows(0).Item("CaseId").ToString() + ")", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        Header2TdSetting(tdHeader3, "", "", "1")
                        Header2TdSetting(tdHeader4, "", "(%)", "1")
                        trHeader0.Controls.Add(tdHeader0)
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader3.Controls.Add(tdHeader3)
                        trHeader4.Controls.Add(tdHeader4)
                End Select





            Next
            tblComparision.Controls.Add(trHeader0)
            tblComparision.Controls.Add(trHeader)
            tblComparision.Controls.Add(trHeader1)
            tblComparision.Controls.Add(trHeader2)
            tblComparision.Controls.Add(trHeader3)
            tblComparision.Controls.Add(trHeader4)





            'Inner
            For i = 1 To 25
                If i <> 24 Then
                    trInner = New TableRow
                    For j = 1 To 7
                        tdInner = New TableCell
                        Select Case j
                            Case 1
                                'Layer
                                InnerTdSetting(tdInner, "", "Left")
                                If i = 1 Or i = 7 Or i = 25 Then

                                    tdInner.Text = "<b>" + ds1.Tables(0).Rows(0).Item("PDES" + i.ToString() + "").ToString() + "</b>"

                                Else

                                    tdInner.Text = "<span style='margin-left:20px;'><b>" + ds1.Tables(0).Rows(0).Item("PDES" + i.ToString() + "").ToString() + "</b></span>"
                                End If

                                trInner.Controls.Add(tdInner)
                            Case 2
                                InnerTdSetting(tdInner, "", "Right")
                                lbl = New Label()
                                lbl.Text = FormatNumber(CDbl(ds1.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()) * CDbl(ds1.Tables(0).Rows(0).Item("CURR").ToString()), 0)
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Case 3
                                InnerTdSetting(tdInner, "", "Right")
                                lbl = New Label()
                                lbl.Text = FormatNumber(CDbl(ds2.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()) * CDbl(ds2.Tables(0).Rows(0).Item("CURR").ToString()), 0)
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Case 4
                                InnerTdSetting(tdInner, "", "Right")
                                lbl = New Label()
                                If (CDbl(ds1.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()) * CDbl(ds1.Tables(0).Rows(0).Item("CURR").ToString())) <> 0 Then
                                    lbl.Text = FormatNumber((CDbl(ds2.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()) * CDbl(ds2.Tables(0).Rows(0).Item("CURR").ToString())) / (CDbl(ds1.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()) * CDbl(ds1.Tables(0).Rows(0).Item("CURR").ToString())) * 100, 0)
                                Else
                                    lbl.Text = "na"
                                End If


                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)

                            Case Else
                        End Select
                    Next


                    tblComparision.Controls.Add(trInner)
                End If
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
            Next
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub GetByWeightEcon1(ByVal ds1 As DataSet, ByVal ds2 As DataSet)

        Dim i As New Integer
        Dim j As New Integer
        Dim DWidth As String = String.Empty
        Dim trHeader As New TableRow
        Dim trHeader1 As New TableRow
        Dim trHeader2 As New TableRow
        Dim trHeader3 As New TableRow
        Dim trHeader4 As New TableRow
        Dim trHeader0 As New TableRow
        Dim trInner As New TableRow

        Dim tdHeader As TableCell
        Dim tdHeader1 As TableCell
        Dim tdHeader2 As TableCell
        Dim tdHeader3 As TableCell
        Dim tdHeader4 As TableCell
        Dim tdHeader0 As TableCell

        Dim lbl As New Label
        Dim hid As New HiddenField
        Dim Link As New HyperLink
        Dim txt As New TextBox
        Dim tdInner As TableCell

        Try

            For i = 1 To 7
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                tdHeader3 = New TableCell
                tdHeader4 = New TableCell
                tdHeader0 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader0, "200px", "Module:", "1")
                        HeaderTdSetting(tdHeader, "200px", "", "1")
                        HeaderTdSetting(tdHeader1, "", "Case Id:", "1")
                        HeaderTdSetting(tdHeader2, "", "Package Format:", "1")
                        HeaderTdSetting(tdHeader3, "", "Unique Feature:", "1")
                        HeaderTdSetting(tdHeader4, "", "Units:", "1")
                        trHeader0.Controls.Add(tdHeader0)
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader3.Controls.Add(tdHeader3)
                        trHeader4.Controls.Add(tdHeader4)

                        tdHeader0.HorizontalAlign = HorizontalAlign.Right
                        tdHeader.HorizontalAlign = HorizontalAlign.Right
                        tdHeader1.HorizontalAlign = HorizontalAlign.Right
                        tdHeader2.HorizontalAlign = HorizontalAlign.Right
                        tdHeader3.HorizontalAlign = HorizontalAlign.Right
                        tdHeader4.HorizontalAlign = HorizontalAlign.Right
                    Case 2
                        HeaderTdSetting(tdHeader0, "150px", lblModName.Text, "1")
                        HeaderTdSetting(tdHeader, "150px", "Total", "1")
                        Header2TdSetting(tdHeader1, "", "" + ds1.Tables(0).Rows(0).Item("CaseId").ToString() + "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        Header2TdSetting(tdHeader3, "", "", "1")
                        GetCaseDetailsEcon1(tdHeader2, tdHeader3, ds1.Tables(0).Rows(0).Item("CaseId").ToString())
                        Header2TdSetting(tdHeader4, "", "(" + ds1.Tables(0).Rows(0).Item("TITLE2").ToString() + "/" + ds1.Tables(0).Rows(0).Item("TITLE8").ToString() + ")", "1")
                        trHeader0.Controls.Add(tdHeader0)
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader3.Controls.Add(tdHeader3)
                        trHeader4.Controls.Add(tdHeader4)
                    Case 3
                        HeaderTdSetting(tdHeader0, "150px", "Value Chain", "1")
                        HeaderTdSetting(tdHeader, "150px", "Total", "1")
                        Header2TdSetting(tdHeader1, "", "" + ds2.Tables(0).Rows(0).Item("CaseId").ToString() + "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        Header2TdSetting(tdHeader3, "", "", "1")
                        GetCaseDetailsEcon1(tdHeader2, tdHeader3, ds2.Tables(0).Rows(0).Item("CaseId").ToString())
                        Header2TdSetting(tdHeader4, "", "(" + ds2.Tables(0).Rows(0).Item("TITLE2").ToString() + "/" + ds2.Tables(0).Rows(0).Item("TITLE8").ToString() + ")", "1")
                        trHeader0.Controls.Add(tdHeader0)
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader3.Controls.Add(tdHeader3)
                        trHeader4.Controls.Add(tdHeader4)
                    Case 4
                        HeaderTdSetting(tdHeader0, "200px", "(Value Chain /" + lblModName.Text + ")", "1")
                        HeaderTdSetting(tdHeader, "200px", "Total Comparison", "1")
                        Header2TdSetting(tdHeader1, "", "(" + ds2.Tables(0).Rows(0).Item("CaseId").ToString() + "/" + ds1.Tables(0).Rows(0).Item("CaseId").ToString() + ")", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        Header2TdSetting(tdHeader3, "", "", "1")
                        Header2TdSetting(tdHeader4, "", "(%)", "1")
                        trHeader0.Controls.Add(tdHeader0)
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader3.Controls.Add(tdHeader3)
                        trHeader4.Controls.Add(tdHeader4)
                End Select





            Next
            tblComparision.Controls.Add(trHeader0)
            tblComparision.Controls.Add(trHeader)
            tblComparision.Controls.Add(trHeader1)
            tblComparision.Controls.Add(trHeader2)
            tblComparision.Controls.Add(trHeader3)
            tblComparision.Controls.Add(trHeader4)





            'Inner
            For i = 1 To 25
                If i <> 24 Then
                    trInner = New TableRow
                    For j = 1 To 7
                        tdInner = New TableCell
                        Select Case j
                            Case 1
                                'Layer
                                InnerTdSetting(tdInner, "", "Left")
                                If i = 1 Or i = 7 Or i = 25 Then
                                    tdInner.Text = "<b>" + ds1.Tables(0).Rows(0).Item("PDES" + i.ToString() + "").ToString() + "</b>"
                                Else
                                    tdInner.Text = "<span style='margin-left:20px;'><b>" + ds1.Tables(0).Rows(0).Item("PDES" + i.ToString() + "").ToString() + "</b></span>"
                                End If

                                trInner.Controls.Add(tdInner)
                            Case 2
                                InnerTdSetting(tdInner, "", "Right")
                                Dim perwt As New Decimal
                                lbl = New Label()

                                If CDbl(ds1.Tables(0).Rows(0).Item("SVOLUME").ToString()) > 0 Then
                                    perwt = CDbl(CDbl(ds1.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()) * CDbl(ds1.Tables(0).Rows(0).Item("CURR").ToString())) / CDbl(ds1.Tables(0).Rows(0).Item("SVOLUME").ToString())
                                    lbl.Text = FormatNumber(perwt, 3)
                                Else
                                    lbl.Text = "na"
                                End If
                                '
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Case 3
                                InnerTdSetting(tdInner, "", "Right")
                                Dim perwt As New Decimal
                                lbl = New Label()

                                If CDbl(ds2.Tables(0).Rows(0).Item("SVOLUME").ToString()) > 0 Then
                                    perwt = CDbl(CDbl(ds2.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()) * CDbl(ds2.Tables(0).Rows(0).Item("CURR").ToString())) / CDbl(ds2.Tables(0).Rows(0).Item("SVOLUME").ToString())
                                    lbl.Text = FormatNumber(perwt, 3)
                                Else
                                    lbl.Text = "na"
                                End If
                                '
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Case 4
                                InnerTdSetting(tdInner, "", "Right")
                                Dim perwt As New Decimal
                                Dim perwt1 As New Decimal
                                Dim perwt2 As New Decimal
                                lbl = New Label()

                                If CDbl(ds1.Tables(0).Rows(0).Item("SVOLUME").ToString()) > 0 Then
                                    perwt1 = CDbl(CDbl(ds1.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()) * CDbl(ds1.Tables(0).Rows(0).Item("CURR").ToString())) / CDbl(ds1.Tables(0).Rows(0).Item("SVOLUME").ToString())
                                End If

                                If CDbl(ds2.Tables(0).Rows(0).Item("SVOLUME").ToString()) > 0 Then
                                    perwt2 = CDbl(CDbl(ds2.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()) * CDbl(ds2.Tables(0).Rows(0).Item("CURR").ToString())) / CDbl(ds2.Tables(0).Rows(0).Item("SVOLUME").ToString())
                                End If

                                If perwt1 <> 0 Then
                                    perwt = (perwt2 / perwt1) * 100
                                    lbl.Text = FormatNumber(perwt, 3)
                                Else
                                    lbl.Text = "na"
                                End If

                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)

                            Case Else
                        End Select
                    Next


                    tblComparision.Controls.Add(trInner)
                End If
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
            Next
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub GetByVolumeEcon1(ByVal ds1 As DataSet, ByVal ds2 As DataSet)

        Dim i As New Integer
        Dim j As New Integer
        Dim DWidth As String = String.Empty
        Dim trHeader As New TableRow
        Dim trHeader1 As New TableRow
        Dim trHeader2 As New TableRow
        Dim trHeader3 As New TableRow
        Dim trHeader4 As New TableRow
        Dim trHeader0 As New TableRow

        Dim trInner As New TableRow

        Dim tdHeader As TableCell
        Dim tdHeader1 As TableCell
        Dim tdHeader2 As TableCell
        Dim tdHeader3 As TableCell
        Dim tdHeader4 As TableCell
        Dim tdHeader0 As TableCell


        Dim lbl As New Label
        Dim hid As New HiddenField
        Dim Link As New HyperLink
        Dim txt As New TextBox
        Dim tdInner As TableCell

        Try

            For i = 1 To 7
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                tdHeader3 = New TableCell
                tdHeader4 = New TableCell
                tdHeader0 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader0, "200px", "Module", "1")
                        HeaderTdSetting(tdHeader, "200px", "", "1")
                        HeaderTdSetting(tdHeader1, "", "Case Id:", "1")
                        HeaderTdSetting(tdHeader2, "", "Package Format:", "1")
                        HeaderTdSetting(tdHeader3, "", "Unique Feature:", "1")
                        HeaderTdSetting(tdHeader4, "", "Units:", "1")
                        trHeader0.Controls.Add(tdHeader0)
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader3.Controls.Add(tdHeader3)
                        trHeader4.Controls.Add(tdHeader4)
                        tdHeader0.HorizontalAlign = HorizontalAlign.Right
                        tdHeader1.HorizontalAlign = HorizontalAlign.Right
                        tdHeader2.HorizontalAlign = HorizontalAlign.Right
                        tdHeader3.HorizontalAlign = HorizontalAlign.Right
                        tdHeader4.HorizontalAlign = HorizontalAlign.Right
                    Case 2
                        HeaderTdSetting(tdHeader0, "150px", lblModName.Text, "1")
                        HeaderTdSetting(tdHeader, "150px", "Total", "1")
                        Header2TdSetting(tdHeader1, "", "" + ds1.Tables(0).Rows(0).Item("CaseId").ToString() + "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        Header2TdSetting(tdHeader3, "", "", "1")
                        GetCaseDetailsEcon1(tdHeader2, tdHeader3, ds1.Tables(0).Rows(0).Item("CaseId").ToString())
                        Header2TdSetting(tdHeader4, "", "(" + ds1.Tables(0).Rows(0).Item("PUN").ToString() + ")", "1")
                        trHeader0.Controls.Add(tdHeader0)
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader3.Controls.Add(tdHeader3)
                        trHeader4.Controls.Add(tdHeader4)
                    Case 3
                        HeaderTdSetting(tdHeader0, "150px", "Value Chain", "1")
                        HeaderTdSetting(tdHeader, "150px", "Total", "1")
                        Header2TdSetting(tdHeader1, "", "" + ds2.Tables(0).Rows(0).Item("CaseId").ToString() + "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        Header2TdSetting(tdHeader3, "", "", "1")
                        GetCaseDetailsEcon1(tdHeader2, tdHeader3, ds2.Tables(0).Rows(0).Item("CaseId").ToString())
                        Header2TdSetting(tdHeader4, "", "(" + ds2.Tables(0).Rows(0).Item("PUN").ToString() + ")", "1")
                        trHeader0.Controls.Add(tdHeader0)
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader3.Controls.Add(tdHeader3)
                        trHeader4.Controls.Add(tdHeader4)
                    Case 4
                        HeaderTdSetting(tdHeader0, "200px", "(Value Chain /" + lblModName.Text + ")", "1")
                        HeaderTdSetting(tdHeader, "200px", "Total Comparison", "1")
                        Header2TdSetting(tdHeader1, "", "(" + ds2.Tables(0).Rows(0).Item("CaseId").ToString() + "/" + ds1.Tables(0).Rows(0).Item("CaseId").ToString() + ")", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        Header2TdSetting(tdHeader3, "", "", "1")
                        Header2TdSetting(tdHeader4, "", "(%)", "1")
                        trHeader0.Controls.Add(tdHeader0)
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader3.Controls.Add(tdHeader3)
                        trHeader4.Controls.Add(tdHeader4)
                End Select





            Next
            tblComparision.Controls.Add(trHeader0)
            tblComparision.Controls.Add(trHeader)
            tblComparision.Controls.Add(trHeader1)
            tblComparision.Controls.Add(trHeader2)
            tblComparision.Controls.Add(trHeader3)
            tblComparision.Controls.Add(trHeader4)





            'Inner
            For i = 1 To 25
                If i <> 24 Then
                    trInner = New TableRow
                    For j = 1 To 7
                        tdInner = New TableCell
                        Select Case j
                            Case 1
                                'Layer
                                InnerTdSetting(tdInner, "", "Left")
                                If i = 1 Or i = 7 Or i = 25 Then
                                    tdInner.Text = "<b>" + ds1.Tables(0).Rows(0).Item("PDES" + i.ToString() + "").ToString() + "</b>"
                                Else
                                    tdInner.Text = "<span style='margin-left:20px;'><b>" + ds1.Tables(0).Rows(0).Item("PDES" + i.ToString() + "").ToString() + "</b></span>"
                                End If

                                trInner.Controls.Add(tdInner)
                            Case 2
                                InnerTdSetting(tdInner, "", "Right")
                                Dim perunit As New Decimal
                                lbl = New Label()
                                If CDbl(ds1.Tables(0).Rows(0).Item("SUNIT").ToString()) > 0 Then
                                    perunit = CDbl(ds1.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()) * CDbl(ds1.Tables(0).Rows(0).Item("SUNIT").ToString())
                                    lbl.Text = FormatNumber(perunit, 4)
                                Else
                                    lbl.Text = "na"
                                End If
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Case 3
                                InnerTdSetting(tdInner, "", "Right")
                                Dim perunit As New Decimal
                                lbl = New Label()
                                If CDbl(ds2.Tables(0).Rows(0).Item("SUNIT").ToString()) > 0 Then
                                    perunit = CDbl(ds2.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()) * CDbl(ds2.Tables(0).Rows(0).Item("SUNIT").ToString())
                                    lbl.Text = FormatNumber(perunit, 4)
                                Else
                                    lbl.Text = "na"
                                End If
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Case 4
                                InnerTdSetting(tdInner, "", "Right")
                                Dim perunit As New Decimal
                                Dim perunit1 As New Decimal
                                Dim perunit2 As New Decimal
                                lbl = New Label()

                                If CDbl(ds1.Tables(0).Rows(0).Item("SUNIT").ToString()) > 0 Then
                                    perunit1 = CDbl(ds1.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()) * CDbl(ds1.Tables(0).Rows(0).Item("SUNIT").ToString())
                                End If

                                If CDbl(ds2.Tables(0).Rows(0).Item("SUNIT").ToString()) > 0 Then
                                    perunit2 = CDbl(ds2.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()) * CDbl(ds2.Tables(0).Rows(0).Item("SUNIT").ToString())
                                End If

                                If perunit1 <> 0 Then
                                    perunit = (perunit2 / perunit1) * 100
                                    lbl.Text = FormatNumber(perunit, 3)
                                Else
                                    lbl.Text = "na"
                                End If

                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)

                            Case Else
                        End Select
                    Next


                    tblComparision.Controls.Add(trInner)
                End If
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
            Next
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetByTotalEcon2(ByVal ds1 As DataSet, ByVal ds2 As DataSet)

        Dim i As New Integer
        Dim j As New Integer
        Dim DWidth As String = String.Empty
        Dim trHeader As New TableRow
        Dim trHeader1 As New TableRow
        Dim trHeader2 As New TableRow
        Dim trHeader3 As New TableRow
        Dim trHeader4 As New TableRow
        Dim trHeader0 As New TableRow
        Dim trInner As New TableRow

        Dim tdHeader As TableCell
        Dim tdHeader1 As TableCell
        Dim tdHeader2 As TableCell
        Dim tdHeader3 As TableCell
        Dim tdHeader4 As TableCell
        Dim tdHeader0 As TableCell

        Dim lbl As New Label
        Dim hid As New HiddenField
        Dim Link As New HyperLink
        Dim txt As New TextBox
        Dim tdInner As TableCell

        Try

            For i = 1 To 7
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                tdHeader3 = New TableCell
                tdHeader4 = New TableCell
                tdHeader0 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader0, "200px", "Module:", "1")
                        HeaderTdSetting(tdHeader, "200px", "", "1")
                        HeaderTdSetting(tdHeader1, "", "Case Id:", "1")
                        HeaderTdSetting(tdHeader2, "", "Package Format:", "1")
                        HeaderTdSetting(tdHeader3, "", "Unique Feature:", "1")
                        HeaderTdSetting(tdHeader4, "", "Units:", "1")
                        trHeader0.Controls.Add(tdHeader0)
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader3.Controls.Add(tdHeader3)
                        trHeader4.Controls.Add(tdHeader4)

                        tdHeader0.HorizontalAlign = HorizontalAlign.Right
                        tdHeader1.HorizontalAlign = HorizontalAlign.Right
                        tdHeader2.HorizontalAlign = HorizontalAlign.Right
                        tdHeader3.HorizontalAlign = HorizontalAlign.Right
                        tdHeader4.HorizontalAlign = HorizontalAlign.Right
                    Case 2
                        HeaderTdSetting(tdHeader0, "200px", lblModName.Text, "1")
                        HeaderTdSetting(tdHeader, "150px", "Total", "1")
                        Header2TdSetting(tdHeader1, "", "" + ds2.Tables(0).Rows(0).Item("CaseId").ToString() + "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        Header2TdSetting(tdHeader3, "", "", "1")
                        GetCaseDetailsEcon2(tdHeader2, tdHeader3, ds2.Tables(0).Rows(0).Item("CaseId").ToString())
                        Header2TdSetting(tdHeader4, "", "(" + ds2.Tables(0).Rows(0).Item("TITLE2").ToString() + ")", "1")
                        trHeader0.Controls.Add(tdHeader0)
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader3.Controls.Add(tdHeader3)
                        trHeader4.Controls.Add(tdHeader4)
                    Case 3
                        HeaderTdSetting(tdHeader0, "200px", "Value Chain", "1")
                        HeaderTdSetting(tdHeader, "150px", "Total", "1")
                        Header2TdSetting(tdHeader1, "", "" + ds2.Tables(0).Rows(0).Item("CaseId").ToString() + "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        Header2TdSetting(tdHeader3, "", "", "1")
                        GetCaseDetailsEcon2(tdHeader2, tdHeader3, ds2.Tables(0).Rows(0).Item("CaseId").ToString())
                        Header2TdSetting(tdHeader4, "", "(" + ds2.Tables(0).Rows(0).Item("TITLE2").ToString() + ")", "1")
                        trHeader0.Controls.Add(tdHeader0)
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader3.Controls.Add(tdHeader3)
                        trHeader4.Controls.Add(tdHeader4)
                    Case 4
                        HeaderTdSetting(tdHeader0, "200px", "(Value Chain /" + lblModName.Text + ")", "1")
                        HeaderTdSetting(tdHeader, "200px", "Total Comparison", "1")
                        Header2TdSetting(tdHeader1, "", "(" + ds2.Tables(0).Rows(0).Item("CaseId").ToString() + "/" + ds1.Tables(0).Rows(0).Item("CaseId").ToString() + ")", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        Header2TdSetting(tdHeader3, "", "", "1")
                        Header2TdSetting(tdHeader4, "", "(%)", "1")
                        trHeader0.Controls.Add(tdHeader0)
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader3.Controls.Add(tdHeader3)
                        trHeader4.Controls.Add(tdHeader4)
                End Select
            Next
            tblComparision.Controls.Add(trHeader0)
            tblComparision.Controls.Add(trHeader)
            tblComparision.Controls.Add(trHeader1)
            tblComparision.Controls.Add(trHeader2)
            tblComparision.Controls.Add(trHeader3)
            tblComparision.Controls.Add(trHeader4)





            'Inner
            For i = 1 To 25
                If i <> 24 Then
                    trInner = New TableRow
                    For j = 1 To 7
                        tdInner = New TableCell
                        Select Case j
                            Case 1
                                'Layer
                                InnerTdSetting(tdInner, "", "Left")
                                If i = 1 Or i = 7 Or i = 25 Then

                                    tdInner.Text = "<b>" + ds2.Tables(0).Rows(0).Item("PDES" + i.ToString() + "").ToString() + "</b>"

                                Else

                                    tdInner.Text = "<span style='margin-left:20px;'><b>" + ds2.Tables(0).Rows(0).Item("PDES" + i.ToString() + "").ToString() + "</b></span>"
                                End If

                                trInner.Controls.Add(tdInner)
                            Case 2
                                InnerTdSetting(tdInner, "", "Right")
                                lbl = New Label()
                                If i = 25 Then
                                    lbl.Text = FormatNumber(CDbl(ds1.Tables(0).Rows(0).Item("PL" + (i - 1).ToString() + "").ToString()) * CDbl(ds1.Tables(0).Rows(0).Item("CURR").ToString()), 0)
                                Else
                                    lbl.Text = FormatNumber(CDbl(ds1.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()) * CDbl(ds1.Tables(0).Rows(0).Item("CURR").ToString()), 0)
                                End If
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Case 3
                                InnerTdSetting(tdInner, "", "Right")
                                lbl = New Label()
                                lbl.Text = FormatNumber(CDbl(ds2.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()) * CDbl(ds2.Tables(0).Rows(0).Item("CURR").ToString()), 0)
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Case 4
                                InnerTdSetting(tdInner, "", "Right")
                                lbl = New Label()
                                If i = 25 Then
                                    If (CDbl(ds1.Tables(0).Rows(0).Item("PL" + (i - 1).ToString() + "").ToString()) * CDbl(ds1.Tables(0).Rows(0).Item("CURR").ToString())) <> 0 Then
                                        lbl.Text = FormatNumber((CDbl(ds2.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()) * CDbl(ds2.Tables(0).Rows(0).Item("CURR").ToString())) / (CDbl(ds1.Tables(0).Rows(0).Item("PL" + (i - 1).ToString() + "").ToString()) * CDbl(ds1.Tables(0).Rows(0).Item("CURR").ToString())) * 100, 0)
                                    Else
                                        lbl.Text = "na"
                                    End If
                                Else
                                    If (CDbl(ds1.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()) * CDbl(ds1.Tables(0).Rows(0).Item("CURR").ToString())) <> 0 Then
                                        lbl.Text = FormatNumber((CDbl(ds2.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()) * CDbl(ds2.Tables(0).Rows(0).Item("CURR").ToString())) / (CDbl(ds1.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()) * CDbl(ds1.Tables(0).Rows(0).Item("CURR").ToString())) * 100, 0)
                                    Else
                                        lbl.Text = "na"
                                    End If
                                End If                               
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Case Else
                        End Select
                    Next
                    tblComparision.Controls.Add(trInner)
                End If

                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
            Next
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub GetByWeightEcon2(ByVal ds1 As DataSet, ByVal ds2 As DataSet)

        Dim i As New Integer
        Dim j As New Integer
        Dim DWidth As String = String.Empty
        Dim trHeader As New TableRow
        Dim trHeader1 As New TableRow
        Dim trHeader2 As New TableRow
        Dim trHeader3 As New TableRow
        Dim trHeader4 As New TableRow
        Dim trHeader0 As New TableRow
        Dim trInner As New TableRow

        Dim tdHeader As TableCell
        Dim tdHeader1 As TableCell
        Dim tdHeader2 As TableCell
        Dim tdHeader3 As TableCell
        Dim tdHeader4 As TableCell
        Dim tdHeader0 As TableCell

        Dim lbl As New Label
        Dim hid As New HiddenField
        Dim Link As New HyperLink
        Dim txt As New TextBox
        Dim tdInner As TableCell

        Try

            For i = 1 To 7
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                tdHeader3 = New TableCell
                tdHeader4 = New TableCell
                tdHeader0 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader0, "200px", "Module:", "1")
                        HeaderTdSetting(tdHeader, "200px", "", "1")
                        HeaderTdSetting(tdHeader1, "", "Case Id:", "1")
                        HeaderTdSetting(tdHeader2, "", "Package Format:", "1")
                        HeaderTdSetting(tdHeader3, "", "Unique Feature:", "1")
                        HeaderTdSetting(tdHeader4, "", "Units:", "1")
                        trHeader0.Controls.Add(tdHeader0)
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader3.Controls.Add(tdHeader3)
                        trHeader4.Controls.Add(tdHeader4)

                        tdHeader0.HorizontalAlign = HorizontalAlign.Right
                        tdHeader.HorizontalAlign = HorizontalAlign.Right
                        tdHeader1.HorizontalAlign = HorizontalAlign.Right
                        tdHeader2.HorizontalAlign = HorizontalAlign.Right
                        tdHeader3.HorizontalAlign = HorizontalAlign.Right
                        tdHeader4.HorizontalAlign = HorizontalAlign.Right
                    Case 2
                        HeaderTdSetting(tdHeader0, "150px", lblModName.Text, "1")
                        HeaderTdSetting(tdHeader, "150px", "Total", "1")
                        Header2TdSetting(tdHeader1, "", "" + ds2.Tables(0).Rows(0).Item("CaseId").ToString() + "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        Header2TdSetting(tdHeader3, "", "", "1")
                        GetCaseDetailsEcon2(tdHeader2, tdHeader3, ds1.Tables(0).Rows(0).Item("CaseId").ToString())
                        Header2TdSetting(tdHeader4, "", "(" + ds2.Tables(0).Rows(0).Item("TITLE2").ToString() + "/" + ds2.Tables(0).Rows(0).Item("TITLE8").ToString() + ")", "1")
                        trHeader0.Controls.Add(tdHeader0)
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader3.Controls.Add(tdHeader3)
                        trHeader4.Controls.Add(tdHeader4)
                    Case 3
                        HeaderTdSetting(tdHeader0, "150px", "Value Chain", "1")
                        HeaderTdSetting(tdHeader, "150px", "Total", "1")
                        Header2TdSetting(tdHeader1, "", "" + ds2.Tables(0).Rows(0).Item("CaseId").ToString() + "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        Header2TdSetting(tdHeader3, "", "", "1")
                        GetCaseDetailsEcon2(tdHeader2, tdHeader3, ds2.Tables(0).Rows(0).Item("CaseId").ToString())
                        Header2TdSetting(tdHeader4, "", "(" + ds2.Tables(0).Rows(0).Item("TITLE2").ToString() + "/" + ds2.Tables(0).Rows(0).Item("TITLE8").ToString() + ")", "1")
                        trHeader0.Controls.Add(tdHeader0)
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader3.Controls.Add(tdHeader3)
                        trHeader4.Controls.Add(tdHeader4)
                    Case 4
                        HeaderTdSetting(tdHeader0, "200px", "(Value Chain /" + lblModName.Text + ")", "1")
                        HeaderTdSetting(tdHeader, "200px", "Total Comparison", "1")
                        Header2TdSetting(tdHeader1, "", "(" + ds2.Tables(0).Rows(0).Item("CaseId").ToString() + "/" + ds1.Tables(0).Rows(0).Item("CaseId").ToString() + ")", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        Header2TdSetting(tdHeader3, "", "", "1")
                        Header2TdSetting(tdHeader4, "", "(%)", "1")
                        trHeader0.Controls.Add(tdHeader0)
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader3.Controls.Add(tdHeader3)
                        trHeader4.Controls.Add(tdHeader4)
                End Select





            Next
            tblComparision.Controls.Add(trHeader0)
            tblComparision.Controls.Add(trHeader)
            tblComparision.Controls.Add(trHeader1)
            tblComparision.Controls.Add(trHeader2)
            tblComparision.Controls.Add(trHeader3)
            tblComparision.Controls.Add(trHeader4)





            'Inner
            For i = 1 To 25
                If i <> 24 Then
                    trInner = New TableRow
                    For j = 1 To 7
                        tdInner = New TableCell
                        Select Case j
                            Case 1
                                'Layer
                                InnerTdSetting(tdInner, "", "Left")
                                If i = 1 Or i = 7 Or i = 25 Then
                                    tdInner.Text = "<b>" + ds2.Tables(0).Rows(0).Item("PDES" + i.ToString() + "").ToString() + "</b>"
                                Else
                                    tdInner.Text = "<span style='margin-left:20px;'><b>" + ds2.Tables(0).Rows(0).Item("PDES" + i.ToString() + "").ToString() + "</b></span>"
                                End If

                                trInner.Controls.Add(tdInner)
                            Case 2
                                InnerTdSetting(tdInner, "", "Right")
                                Dim perwt As New Decimal
                                lbl = New Label()
                                Dim salesVolume As Double
                                salesVolume = CDbl(ds1.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()) / CDbl(ds1.Tables(0).Rows(0).Item("CONVWT").ToString())
                                If i = 25 Then
                                    If salesVolume > 0 Then 'SVOLUME CHANGED BY PVOLUME PVOLUME
                                        perwt = CDbl(ds1.Tables(0).Rows(0).Item("PL" + (i - 1).ToString() + "").ToString()) / salesVolume * CDbl(ds1.Tables(0).Rows(0).Item("CURR").ToString()) / CDbl(ds1.Tables(0).Rows(0).Item("CONVWT").ToString())
                                        lbl.Text = FormatNumber(perwt, 3)
                                    Else
                                        lbl.Text = "na"
                                    End If
                                Else
                                    If salesVolume > 0 Then 'SVOLUME CHANGED BY PVOLUME PVOLUME
                                        perwt = CDbl(ds1.Tables(0).Rows(0).Item("PL" + (i).ToString() + "").ToString()) / salesVolume * CDbl(ds1.Tables(0).Rows(0).Item("CURR").ToString()) / CDbl(ds1.Tables(0).Rows(0).Item("CONVWT").ToString())
                                        lbl.Text = FormatNumber(perwt, 3)
                                    Else
                                        lbl.Text = "na"
                                    End If
                                End If

                                    '
                                    tdInner.Controls.Add(lbl)
                                    trInner.Controls.Add(tdInner)
                            Case 3
                                    InnerTdSetting(tdInner, "", "Right")
                                    Dim perwt As New Decimal
                                    lbl = New Label()

                                    If CDbl(ds2.Tables(0).Rows(0).Item("SVOLUME").ToString()) > 0 Then
                                        perwt = CDbl(CDbl(ds2.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()) * CDbl(ds2.Tables(0).Rows(0).Item("CURR").ToString())) / CDbl(ds2.Tables(0).Rows(0).Item("SVOLUME").ToString())
                                        lbl.Text = FormatNumber(perwt, 3)
                                    Else
                                        lbl.Text = "na"
                                End If

                                    tdInner.Controls.Add(lbl)
                                    trInner.Controls.Add(tdInner)
                            Case 4
                                    InnerTdSetting(tdInner, "", "Right")
                                    Dim perwt As New Decimal
                                    Dim perwt1 As New Decimal
                                    Dim perwt2 As New Decimal
                                lbl = New Label()
                                Dim salesVolume As Double
                                salesVolume = CDbl(ds1.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()) / CDbl(ds1.Tables(0).Rows(0).Item("CONVWT").ToString())

                                If i = 25 Then
                                    If salesVolume > 0 Then
                                        perwt1 = CDbl(ds1.Tables(0).Rows(0).Item("PL" + (i - 1).ToString() + "").ToString()) / salesVolume * CDbl(ds1.Tables(0).Rows(0).Item("CURR").ToString()) / CDbl(ds1.Tables(0).Rows(0).Item("CONVWT").ToString())
                                    End If

                                    If CDbl(ds2.Tables(0).Rows(0).Item("SVOLUME").ToString()) > 0 Then
                                        perwt2 = CDbl(CDbl(ds2.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()) * CDbl(ds2.Tables(0).Rows(0).Item("CURR").ToString())) / CDbl(ds2.Tables(0).Rows(0).Item("SVOLUME").ToString())
                                    End If
                                Else
                                    If salesVolume > 0 Then
                                        perwt1 = CDbl(ds1.Tables(0).Rows(0).Item("PL" + (i).ToString() + "").ToString()) / salesVolume * CDbl(ds1.Tables(0).Rows(0).Item("CURR").ToString()) / CDbl(ds1.Tables(0).Rows(0).Item("CONVWT").ToString())
                                    End If

                                    If CDbl(ds2.Tables(0).Rows(0).Item("SVOLUME").ToString()) > 0 Then
                                        perwt2 = CDbl(CDbl(ds2.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()) * CDbl(ds2.Tables(0).Rows(0).Item("CURR").ToString())) / CDbl(ds2.Tables(0).Rows(0).Item("SVOLUME").ToString())
                                    End If
                                End If


                                If perwt1 <> 0 Then
                                    perwt = (perwt2 / perwt1) * 100
                                    lbl.Text = FormatNumber(perwt, 3)
                                Else
                                    lbl.Text = "na"
                                End If

                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)

                            Case Else
                        End Select
                    Next


                    tblComparision.Controls.Add(trInner)
                End If
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
            Next
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub GetByVolumeEcon2(ByVal ds1 As DataSet, ByVal ds2 As DataSet)

        Dim i As New Integer
        Dim j As New Integer
        Dim DWidth As String = String.Empty
        Dim trHeader As New TableRow
        Dim trHeader1 As New TableRow
        Dim trHeader2 As New TableRow
        Dim trHeader3 As New TableRow
        Dim trHeader4 As New TableRow
        Dim trHeader0 As New TableRow

        Dim trInner As New TableRow

        Dim tdHeader As TableCell
        Dim tdHeader1 As TableCell
        Dim tdHeader2 As TableCell
        Dim tdHeader3 As TableCell
        Dim tdHeader4 As TableCell
        Dim tdHeader0 As TableCell


        Dim lbl As New Label
        Dim hid As New HiddenField
        Dim Link As New HyperLink
        Dim txt As New TextBox
        Dim tdInner As TableCell

        Try

            For i = 1 To 7
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                tdHeader3 = New TableCell
                tdHeader4 = New TableCell
                tdHeader0 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader0, "200px", "Module", "1")
                        HeaderTdSetting(tdHeader, "200px", "", "1")
                        HeaderTdSetting(tdHeader1, "", "Case Id:", "1")
                        HeaderTdSetting(tdHeader2, "", "Package Format:", "1")
                        HeaderTdSetting(tdHeader3, "", "Unique Feature:", "1")
                        HeaderTdSetting(tdHeader4, "", "Units:", "1")
                        trHeader0.Controls.Add(tdHeader0)
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader3.Controls.Add(tdHeader3)
                        trHeader4.Controls.Add(tdHeader4)
                        tdHeader0.HorizontalAlign = HorizontalAlign.Right
                        tdHeader1.HorizontalAlign = HorizontalAlign.Right
                        tdHeader2.HorizontalAlign = HorizontalAlign.Right
                        tdHeader3.HorizontalAlign = HorizontalAlign.Right
                        tdHeader4.HorizontalAlign = HorizontalAlign.Right
                    Case 2
                        HeaderTdSetting(tdHeader0, "150px", lblModName.Text, "1")
                        HeaderTdSetting(tdHeader, "150px", "Total", "1")
                        Header2TdSetting(tdHeader1, "", "" + ds2.Tables(0).Rows(0).Item("CaseId").ToString() + "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        Header2TdSetting(tdHeader3, "", "", "1")
                        GetCaseDetailsEcon2(tdHeader2, tdHeader3, ds2.Tables(0).Rows(0).Item("CaseId").ToString())
                        Header2TdSetting(tdHeader4, "", "(" + ds2.Tables(0).Rows(0).Item("PUN").ToString() + ")", "1")
                        trHeader0.Controls.Add(tdHeader0)
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader3.Controls.Add(tdHeader3)
                        trHeader4.Controls.Add(tdHeader4)
                    Case 3
                        HeaderTdSetting(tdHeader0, "150px", "Value Chain", "1")
                        HeaderTdSetting(tdHeader, "150px", "Total", "1")
                        Header2TdSetting(tdHeader1, "", "" + ds2.Tables(0).Rows(0).Item("CaseId").ToString() + "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        Header2TdSetting(tdHeader3, "", "", "1")
                        GetCaseDetailsEcon2(tdHeader2, tdHeader3, ds2.Tables(0).Rows(0).Item("CaseId").ToString())
                        Header2TdSetting(tdHeader4, "", "(" + ds2.Tables(0).Rows(0).Item("PUN").ToString() + ")", "1")
                        trHeader0.Controls.Add(tdHeader0)
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader3.Controls.Add(tdHeader3)
                        trHeader4.Controls.Add(tdHeader4)
                    Case 4
                        HeaderTdSetting(tdHeader0, "200px", "(Value Chain /" + lblModName.Text + ")", "1")
                        HeaderTdSetting(tdHeader, "200px", "Total Comparison", "1")
                        Header2TdSetting(tdHeader1, "", "(" + ds2.Tables(0).Rows(0).Item("CaseId").ToString() + "/" + ds1.Tables(0).Rows(0).Item("CaseId").ToString() + ")", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        Header2TdSetting(tdHeader3, "", "", "1")
                        Header2TdSetting(tdHeader4, "", "(%)", "1")
                        trHeader0.Controls.Add(tdHeader0)
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader3.Controls.Add(tdHeader3)
                        trHeader4.Controls.Add(tdHeader4)
                End Select





            Next
            tblComparision.Controls.Add(trHeader0)
            tblComparision.Controls.Add(trHeader)
            tblComparision.Controls.Add(trHeader1)
            tblComparision.Controls.Add(trHeader2)
            tblComparision.Controls.Add(trHeader3)
            tblComparision.Controls.Add(trHeader4)





            'Inner
            For i = 1 To 25
                If i <> 24 Then
                    trInner = New TableRow
                    For j = 1 To 7
                        tdInner = New TableCell
                        Select Case j
                            Case 1
                                'Layer
                                InnerTdSetting(tdInner, "", "Left")
                                If i = 1 Or i = 7 Or i = 25 Then
                                    tdInner.Text = "<b>" + ds2.Tables(0).Rows(0).Item("PDES" + i.ToString() + "").ToString() + "</b>"
                                Else
                                    tdInner.Text = "<span style='margin-left:20px;'><b>" + ds2.Tables(0).Rows(0).Item("PDES" + i.ToString() + "").ToString() + "</b></span>"
                                End If

                                trInner.Controls.Add(tdInner)
                            Case 2
                                InnerTdSetting(tdInner, "", "Right")
                                Dim perunit As New Decimal
                                lbl = New Label()
                                If i = 25 Then
                                    If CDbl(ds1.Tables(0).Rows(0).Item("finvolmunits").ToString()) > 0 Then
                                        perunit = CDbl(ds1.Tables(0).Rows(0).Item("PL" + (i - 1).ToString() + "").ToString()) / CDbl(ds1.Tables(0).Rows(0).Item("finvolmunits").ToString()) * 100 * CDbl(ds1.Tables(0).Rows(0).Item("CURR").ToString())
                                        lbl.Text = FormatNumber(perunit, 4)
                                    Else
                                        lbl.Text = "na"
                                    End If
                                Else
                                    If CDbl(ds1.Tables(0).Rows(0).Item("finvolmunits").ToString()) > 0 Then
                                        perunit = CDbl(ds1.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()) / CDbl(ds1.Tables(0).Rows(0).Item("finvolmunits").ToString()) * 100 * CDbl(ds1.Tables(0).Rows(0).Item("CURR").ToString())
                                        lbl.Text = FormatNumber(perunit, 4)
                                    Else
                                        lbl.Text = "na"
                                    End If
                                End If
                               
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Case 3
                                InnerTdSetting(tdInner, "", "Right")
                                Dim perunit As New Decimal
                                lbl = New Label()
                                If CDbl(ds2.Tables(0).Rows(0).Item("SUNIT").ToString()) > 0 Then
                                    perunit = CDbl(ds2.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()) * CDbl(ds2.Tables(0).Rows(0).Item("SUNIT").ToString())
                                    lbl.Text = FormatNumber(perunit, 4)
                                Else
                                    lbl.Text = "na"
                                End If
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Case 4
                                InnerTdSetting(tdInner, "", "Right")
                                Dim perunit As New Decimal
                                Dim perunit1 As New Decimal
                                Dim perunit2 As New Decimal
                                lbl = New Label()
                                If i = 25 Then
                                    If CDbl(ds1.Tables(0).Rows(0).Item("finvolmunits").ToString()) > 0 Then
                                        perunit1 = CDbl(ds1.Tables(0).Rows(0).Item("PL" + (i - 1).ToString() + "").ToString()) / CDbl(ds1.Tables(0).Rows(0).Item("finvolmunits").ToString()) * 100 * CDbl(ds1.Tables(0).Rows(0).Item("CURR").ToString())
                                    End If

                                    If CDbl(ds2.Tables(0).Rows(0).Item("SUNIT").ToString()) > 0 Then
                                        perunit2 = CDbl(ds2.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()) * CDbl(ds2.Tables(0).Rows(0).Item("SUNIT").ToString())
                                    End If
                                Else
                                    If CDbl(ds1.Tables(0).Rows(0).Item("finvolmunits").ToString()) > 0 Then
                                        perunit1 = CDbl(ds1.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()) / CDbl(ds1.Tables(0).Rows(0).Item("finvolmunits").ToString()) * 100 * CDbl(ds1.Tables(0).Rows(0).Item("CURR").ToString())
                                    End If

                                    If CDbl(ds2.Tables(0).Rows(0).Item("SUNIT").ToString()) > 0 Then
                                        perunit2 = CDbl(ds2.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()) * CDbl(ds2.Tables(0).Rows(0).Item("SUNIT").ToString())
                                    End If
                                End If
                               

                                If perunit1 <> 0 Then
                                    perunit = (perunit2 / perunit1) * 100
                                    lbl.Text = FormatNumber(perunit, 3)
                                Else
                                    lbl.Text = "na"
                                End If

                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)

                            Case Else
                        End Select
                    Next


                    tblComparision.Controls.Add(trInner)
                End If
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
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
            Td.Height = 20
            Td.Font.Size = 10
            Td.Font.Bold = True
            Td.HorizontalAlign = HorizontalAlign.Center



        Catch ex As Exception
            _lErrorLble.Text = "Error:HeaderTdSetting:" + ex.Message.ToString()
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
        Catch ex As Exception
            _lErrorLble.Text = "Error:InnerTdSetting:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub GetCaseDetailsEcon1(ByVal td1 As TableCell, ByVal td2 As TableCell, ByVal CaseId12 As Integer)
        Dim objGetData As New E1GetData.Selectdata()
        Dim ds As New DataSet
        Try
            CaseId12 = CaseId12
            ds = objGetData.GetCaseDetails(CaseId12.ToString())
            td1.Text = ds.Tables(0).Rows(0).Item("CASEDE1").ToString()
            td2.Text = ds.Tables(0).Rows(0).Item("CASEDE2").ToString()
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetCaseDetails:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub GetCaseDetailsEcon2(ByVal td1 As TableCell, ByVal td2 As TableCell, ByVal CaseId12 As Integer)
        Dim objGetData As New E2GetData.Selectdata()
        Dim ds As New DataSet
        Try
            CaseId12 = CaseId12
            ds = objGetData.GetCaseDetails(CaseId12.ToString())
            td1.Text = ds.Tables(0).Rows(0).Item("CASEDE1").ToString()
            td2.Text = ds.Tables(0).Rows(0).Item("CASEDE2").ToString()
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetCaseDetails:" + ex.Message.ToString()
        End Try
    End Sub
End Class
