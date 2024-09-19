Imports System.Data
Imports System.Data.OleDb
Imports System
Imports VChainGetData
Imports VChainUpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_ValueChain_Assumptions_Extrusion
    Inherits System.Web.UI.Page
#Region "Get Set Variables"
    Dim _lErrorLble As Label
    Dim _strUserName As String
    Dim _strPassword As String
    Dim _iValueChainId As Integer
    Dim _strUserRole As String
    Dim _btnUpdate As ImageButton

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
        Updatebtn.Visible = True
        AddHandler Updatebtn.Click, AddressOf Update_Click
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
            ' = Request.QueryString("CaseId").ToString()
            'ModName = Request.QueryString("ModName").ToString()
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
        Dim objGetData As New Selectdata
        Dim i As New Integer
        Dim j As New Integer
        Dim trHeader As New TableRow
        Dim trHeader1 As New TableRow
        Dim trInner As New TableRow
        Dim tdHeader As TableCell
        Dim tdHeader1 As TableCell
        Dim tdHeader2 As TableCell
        Dim trHeader2 As New TableRow
        Dim tdInner As TableCell
        Dim txt As New TextBox
        Dim lbl As New Label
        Dim CaseId As String
        Dim ModName As String
        Dim ds1 As New DataSet
        Try
            CaseId = Request.QueryString("CaseId").ToString()
            ModName = Request.QueryString("ModName").ToString()
            ds = objGetData.GetMaterialsWithPrice(ModName, CaseId)
            ds1 = objGetData.GetCaseWithVPrice(Session("ValueChainId").ToString(), ModName, CaseId)


            For i = 1 To 5
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "40px", "", "1")
                        HeaderTdSetting(tdHeader1, "0", "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 2
                        HeaderTdSetting(tdHeader, "300px", "Primary Materials", "1")
                        HeaderTdSetting(tdHeader1, "", "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 3
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE2").ToString() + "/" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                        HeaderTdSetting(tdHeader, "", "Price", "3")
                        Header2TdSetting(tdHeader2, "", Title, "3")
                        Header2TdSetting(tdHeader1, "100px", "Suggested", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 4
                        Header2TdSetting(tdHeader1, "100px", "Preferred", "1")
                        trHeader1.Controls.Add(tdHeader1)
                    Case 5
                        Header2TdSetting(tdHeader1, "100px", "Value Chain Price", "1")
                        trHeader1.Controls.Add(tdHeader1)
                End Select
            Next
            tblExtrusion.Controls.Add(trHeader)
            tblExtrusion.Controls.Add(trHeader2)
            tblExtrusion.Controls.Add(trHeader1)


            'Inner
            For i = 1 To 10
                trInner = New TableRow
                For j = 1 To 5
                    tdInner = New TableCell

                    Select Case j
                        Case 1
                            'Item
                            InnerTdSetting(tdInner, "", "Center")
                            tdInner.Text = "<b>" + i.ToString() + "</b>"
                            trInner.Controls.Add(tdInner)
                        Case 2
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            lbl.CssClass = "Label"
                            lbl.Text = ds.Tables(0).Rows(0)("MATDES" + i.ToString()).ToString()
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                            
                        Case 3
                            InnerTdSetting(tdInner, "", "RIght")
                            lbl = New Label
                            lbl.CssClass = "Label"
                            If (ds.Tables(0).Rows(0)("PRS" + i.ToString()).ToString() <> "") Then
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0)("PRS" + i.ToString()).ToString(), 3)
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 4
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.CssClass = "Label"
                            If (ds.Tables(0).Rows(0)("PRP" + i.ToString()).ToString() <> "") Then
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0)("PRP" + i.ToString()).ToString(), 3)
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)

                        Case 5
                            InnerTdSetting(tdInner, "", "Right")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.ID = "P" + i.ToString()
                            txt.Width = 70
                            Try
                                If (ds1.Tables(0).Rows(i - 1)("PRICE").ToString() <> "") Then
                                    txt.Text = FormatNumber(ds1.Tables(0).Rows(i - 1)("PRICE").ToString(), 3)
                                Else
                                    txt.Text = ""
                                End If
                            Catch
                                txt.Text = ""
                            End Try
                            txt.MaxLength = 6

                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                    End Select
                Next

                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor2"
                Else
                    trInner.CssClass = "AlterNateColor1"
                End If
                tblExtrusion.Controls.Add(trInner)
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
            'Td.Height = 10
        Catch ex As Exception
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
    Protected Sub Update_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim Price(9) As String
        Dim Cases(9) As String
        Dim ModType(9) As String
        Dim objUpdate As New VChainUpInsData.UpdateInsert()
        Try
            If Not objRefresh.IsRefresh Then
                For i = 1 To 10
                    Cases(i - 1) = 0
                    ModType(i - 1) = 0
                    Price(i - 1) = Request.Form("ctl00$VChainContentPlaceHolder$P" + i.ToString() + "")
                Next
                objUpdate.CaseUpdate(Session("ValueChainId").ToString(), Request.QueryString("CaseId").ToString(), ModType, Cases, "", Request.QueryString("ModName").ToString(), Price, "N")
            End If
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
