Imports System.Data
Imports System.Data.OleDb
Imports System
Imports VChainGetData
Imports VChainUpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_ValueChain_Results_Resultspl
    Inherits System.Web.UI.Page
#Region "Get Set Variables"
    Dim _lErrorLble As Label
    Dim _strUserName As String
    Dim _strPassword As String
    Dim _iValueChainId As Integer
    Dim _strUserRole As String
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
    Public Property Comparebtn() As ImageButton
        Get
            Return _btnCompare
        End Get
        Set(ByVal value As ImageButton)
            _btnCompare = value
        End Set
    End Property
#End Region
#Region "MastePage Content Variables"
    Protected Sub GetMasterPageControls()
        GetErrorLable()
        GetUpdatebtn()
        GetComparebtn()
    End Sub
    Protected Sub GetErrorLable()
        ErrorLable = Page.Master.FindControl("lblError")
    End Sub
    Protected Sub GetUpdatebtn()
        Updatebtn = Page.Master.FindControl("imgUpdate")
        'Updatebtn.Attributes.Add("onclick", "return checkNumericAll();")
        Updatebtn.Visible = True
        AddHandler Updatebtn.Click, AddressOf Update_Click
    End Sub
    Protected Sub GetComparebtn()
        Dim obj As New CryptoHelper()
        Comparebtn = Page.Master.FindControl("imgCompare")
        Comparebtn.Visible = True
        Comparebtn.OnClientClick = "return Compare('Comparision.aspx?Type=" + obj.Encrypt("CST") + "&ModName=" + obj.Encrypt(Request.QueryString("ModName").ToString()) + "&CaseId=" + obj.Encrypt(Request.QueryString("CaseId").ToString()) + "') "
    End Sub
#End Region
#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh

  

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_PAGES_VALUECHAIN_RESULTS_RESULTSPL")
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        objRefresh.Render(Page)
    End Sub

#End Region
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            GetMasterPageControls()
            GetSessionDetails()
            lblModName.Text = Request.QueryString("ModName").ToString()
            lblCaseId.Text = Request.QueryString("CaseId").ToString()
            GetCaseDescription(lblModName.Text, lblCaseId.Text)
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
        Dim ds As New DataSet
        Dim trHeader As New TableRow
        Dim trHeader1 As New TableRow
        Dim trHeader2 As New TableRow

        Dim trInner As New TableRow

        Dim tdHeader As TableCell
        Dim tdHeader1 As TableCell
        Dim tdHeader2 As TableCell
        Dim tdInner As TableCell

        Dim lbl As New Label
        Dim hid As New HiddenField
        Dim Link As New HyperLink
        Dim ObjGetData As New VChainGetData.Selectdata()
        Try
            ds = ObjGetData.GetProfitAndLossDetails(Session("ValueChainId").ToString(), lblCaseId.Text, lblModName.Text)

            For i = 1 To 5
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "160px", "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 2
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE2").ToString() + ")"
                        HeaderTdSetting(tdHeader, "120px", "Total", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 3
                        HeaderTdSetting(tdHeader, "90px", "Total", "1")
                        trHeader.Controls.Add(tdHeader)
                        Header2TdSetting(tdHeader2, "", "(%)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 4
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE2").ToString() + "/" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                        HeaderTdSetting(tdHeader, "90px", "By Weight", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 5
                        Title = "(" + ds.Tables(0).Rows(0).Item("PUN").ToString() + ")"
                        HeaderTdSetting(tdHeader, "90px", "By Volume", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)

                End Select
            Next
            tblPft.Controls.Add(trHeader)
            tblPft.Controls.Add(trHeader2)

            lblSalesVol.Text = "<b>Sales Volume (" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + "):</b> " + FormatNumber(ds.Tables(0).Rows(0).Item("SVOLUME").ToString(), 0).ToString()
            lblSalesVolUnit.Text = "<b>Sales Volume (" + ds.Tables(0).Rows(0).Item("SUNITLBL").ToString() + "):</b> " + FormatNumber(ds.Tables(0).Rows(0).Item("SUNITVAL").ToString(), 0).ToString()

            For i = 1 To 25
                If i = 24 Then i = i + 1
                trInner = New TableRow
                For j = 1 To 8
                    tdInner = New TableCell
                    Select Case j
                        Case 1
                            'Layer
                            InnerTdSetting(tdInner, "", "Left")
                            If i = 1 Or i = 7 Or i = 25 Then
                                tdInner.Text = "<b>" + ds.Tables(0).Rows(0).Item("PDES" + i.ToString() + "").ToString() + "</b>"
                            Else
                                tdInner.Text = "<span style='margin-left:20px;'><b>" + ds.Tables(0).Rows(0).Item("PDES" + i.ToString() + "").ToString() + "</b></span>"
                            End If

                            trInner.Controls.Add(tdInner)
                        Case 2
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label()
                            lbl.Text = FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()) * CDbl(ds.Tables(0).Rows(0).Item("CURR").ToString()), 0)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 3
                            Dim percentage As New Decimal
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label()
                            If CDbl(ds.Tables(0).Rows(0).Item("PL1").ToString()) > 0 Then
                                percentage = CDbl(ds.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()) / CDbl(ds.Tables(0).Rows(0).Item("PL1").ToString()) * 100
                                lbl.Text = FormatNumber(percentage, 2)
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 4
                            InnerTdSetting(tdInner, "", "Right")
                            Dim perwt As New Decimal
                            lbl = New Label()

                            If CDbl(ds.Tables(0).Rows(0).Item("SVOLUME").ToString()) > 0 Then
                                perwt = CDbl(CDbl(ds.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()) * CDbl(ds.Tables(0).Rows(0).Item("CURR").ToString())) / CDbl(ds.Tables(0).Rows(0).Item("SVOLUME").ToString())
                                lbl.Text = FormatNumber(perwt, 3)
                            End If
                            '
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 5
                            InnerTdSetting(tdInner, "", "Right")
                            Dim perunit As New Decimal
                            lbl = New Label()
                            If CDbl(ds.Tables(0).Rows(0).Item("SUNIT").ToString()) > 0 Then
                                perunit = CDbl(ds.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()) * CDbl(ds.Tables(0).Rows(0).Item("SUNIT").ToString())
                                lbl.Text = FormatNumber(perunit, 4)
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)

                        Case Else
                    End Select
                Next


                tblPft.Controls.Add(trInner)
                If (i Mod 2 = 0) Or i = 25 Then
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

    Protected Sub Update_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Calculate()
            GetPageDetails()
        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
    Protected Sub Calculate()
        Try
            Dim Econ1Conn As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            Dim Econ2Conn As String = System.Configuration.ConfigurationManager.AppSettings("Econ2ConnectionString")
            Dim Echem1Conn As String = System.Configuration.ConfigurationManager.AppSettings("Echem1ConnectionString")
            Dim VChainConn As String = System.Configuration.ConfigurationManager.AppSettings("VChainConnectionString")
            Dim obj As New VChainCalculation.VChainCalculations(VChainConn, Econ2Conn, Econ1Conn, Echem1Conn)
            obj.VChainCalculate(Session("ValueChainID").ToString())
        Catch ex As Exception
            ErrorLable.Text = "Error:Calculate:" + ex.Message.ToString() + ""
        End Try
    End Sub
End Class
