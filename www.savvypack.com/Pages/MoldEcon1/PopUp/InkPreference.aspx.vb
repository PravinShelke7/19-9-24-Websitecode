Imports System.Data
Imports System.Data.OleDb
Imports System
Imports MoldE1GetData
Imports MoldE1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_MoldEcon1_PopUp_InkPreference
    Inherits System.Web.UI.Page
    Public CaseDes As String = String.Empty

#Region "Get Set Variables"
    Dim _lErrorLble As Label
    Dim _iCaseId As Integer
    Dim _iUserId As Integer
    Dim _strUserRole As String
    Dim _ctlContentPlaceHolder As ContentPlaceHolder
    Dim _btnUpdate As ImageButton
    Dim _divMainHeading As HtmlGenericControl
    Dim _lnkPreferences As HyperLink

    Public Property PreferencesLink() As HyperLink
        Get
            Return _lnkPreferences
        End Get
        Set(ByVal Value As HyperLink)
            _lnkPreferences = Value
        End Set
    End Property

    Public Property ErrorLable() As Label
        Get
            Return _lErrorLble
        End Get
        Set(ByVal Value As Label)
            _lErrorLble = Value
        End Set
    End Property

    Public Property CaseId() As Integer
        Get
            Return _iCaseId
        End Get
        Set(ByVal Value As Integer)
            _iCaseId = Value
        End Set
    End Property

    Public Property UserId() As Integer
        Get
            Return _iUserId
        End Get
        Set(ByVal Value As Integer)
            _iUserId = Value
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
    Public Property ctlContentPlaceHolder() As ContentPlaceHolder
        Get
            Return _ctlContentPlaceHolder
        End Get
        Set(ByVal value As ContentPlaceHolder)
            _ctlContentPlaceHolder = value
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
    Public Property MainHeading() As HtmlGenericControl
        Get
            Return _divMainHeading
        End Get
        Set(ByVal value As HtmlGenericControl)
            _divMainHeading = value
        End Set
    End Property

    Public DataCnt As Integer
    Public CaseDesp As New ArrayList
#End Region

#Region "MastePage Content Variables"

    Protected Sub GetMasterPageControls()
        GetErrorLable()
        GetContentPlaceHolder()
        GetUpdatebtn()
        GetMainHeadingdiv()
        GetPreferencesLink()
    End Sub

    Protected Sub GetErrorLable()
        ErrorLable = Page.Master.FindControl("lblError")
    End Sub
    Protected Sub GetContentPlaceHolder()
        ctlContentPlaceHolder = Page.Master.FindControl("MoldE1InkContentPlaceHolder")
    End Sub
    Protected Sub GetUpdatebtn()
        Updatebtn = Page.Master.FindControl("imgUpdate")
        Updatebtn.Visible = True
        Updatebtn.Attributes.Add("onclick", "return checkNumericAll();")
        'Updatebtn.Attributes.Add("onclick", "return CheckForMaterialPage('" + ctlContentPlaceHolder.ClientID + "','hidColId','T','hidDepid','hypDep');")
        AddHandler Updatebtn.Click, AddressOf Update_Click
    End Sub
    Protected Sub GetPreferencesLink()
        PreferencesLink = Page.Master.FindControl("hypPref")
        PreferencesLink.Visible = False
    End Sub
    Protected Sub GetMainHeadingdiv()
        MainHeading = Page.Master.FindControl("divMainHeading")
        MainHeading.Attributes.Add("onmouseover", "Tip('Ink Cost Preferences')")
        MainHeading.Attributes.Add("onmouseout", "UnTip()")
        MainHeading.InnerHtml = "Econ1 Mold - Ink Cost Preferences"
    End Sub
#End Region

#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_PAGES_ECON1_POPUP_INKPREFERENCE")
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        objRefresh.Render(Page)
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("Back") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If
            GetSessionDetails()
            GetMasterPageControls()

            If Not IsPostBack Then
                GetPageDetails()
            Else

            End If
        Catch ex As Exception
            ErrorLable.Text = "Error:Page_Load:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetSessionDetails()
        Try
            UserId = Session("UserId")
            CaseId = Session("MoldE1CaseId")
            UserRole = Session("MoldE1UserRole")
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetSessionDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetPageDetails()
        Dim ds As New DataSet
        Dim objGetData As New MoldE1GetData.Selectdata
        Dim i As New Integer
        Dim j As New Integer
        Dim DWidth As String = String.Empty
        Dim trHeader As New TableRow
        Dim trHeader1 As New TableRow
        Dim trHeader2 As New TableRow

        Dim trInner As New TableRow

        Dim tdHeader As TableCell
        Dim tdHeader2 As TableCell
        Dim tdHeader1 As TableCell

        Dim lbl As New Label
        Dim hid As New HiddenField
        Dim Link As New HyperLink
        Dim txt As New TextBox
        Dim tdInner As TableCell

        Dim chk As New CheckBox

        Dim radio As New RadioButton

        Try
            ds = objGetData.GetColorPrefDetails(Session("UserName").ToString(), CaseId)

            For i = 1 To 8
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "150px", "Base Color", "1")
                        HeaderTdSetting(tdHeader1, "0", "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 2
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE2").ToString() + "/" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                        HeaderTdSetting(tdHeader, "", "Wet Cost", "2")
                        Header2TdSetting(tdHeader2, "", Title, "2")
                        Header2TdSetting(tdHeader1, "60px", "Suggested", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 3
                        Header2TdSetting(tdHeader1, "80px", "Preferred", "1")
                        trHeader1.Controls.Add(tdHeader1)
                    Case 4
                        Title = "(%)"
                        HeaderTdSetting(tdHeader, "", "% solids", "2")
                        Header2TdSetting(tdHeader2, "", Title, "2")
                        Header2TdSetting(tdHeader1, "60px", "Suggested", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 5
                        Header2TdSetting(tdHeader1, "80px", "Preferred", "1")
                        trHeader1.Controls.Add(tdHeader1)
                    Case 6
                        Title = "(unitless)"
                        HeaderTdSetting(tdHeader, "", "Specific Gravity", "2")
                        Header2TdSetting(tdHeader2, "", Title, "2")
                        Header2TdSetting(tdHeader1, "60px", "Suggested", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 7
                        Header2TdSetting(tdHeader1, "80px", "Preferred", "1")
                        trHeader1.Controls.Add(tdHeader1)
                    Case 8
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE2").ToString() + "/" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                        HeaderTdSetting(tdHeader, "55px", "Dry Cost", "1")
                        HeaderTdSetting(tdHeader1, "0", "", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                End Select





            Next
            trHeader.Height = 30
            trHeader.Height = 30

            tblComparision.Controls.Add(trHeader)
            tblComparision.Controls.Add(trHeader2)
            tblComparision.Controls.Add(trHeader1)



            'Inner
            For i = 1 To 11
                trInner = New TableRow
                For j = 1 To 8
                    tdInner = New TableCell

                    Select Case j
                        Case 1
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            lbl.Style.Add("margin-left", "10px")
                            lbl.Text = ds.Tables(0).Rows(0).Item("COLOR" + i.ToString() + "").ToString()
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 2
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("WETPS" + i.ToString() + "").ToString(), 2)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 3
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.ID = "WETPP" + i.ToString()
                            txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("WETPP" + i.ToString() + "").ToString(), 2)
                            txt.MaxLength = 6
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 4
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("PERSOLS" + i.ToString() + "").ToString(), 2)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 5
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.ID = "PERSOLP" + i.ToString()
                            txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("PERSOLP" + i.ToString() + "").ToString(), 2)
                            txt.MaxLength = 6
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 6
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("SGRAVITYS" + i.ToString() + "").ToString(), 2)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 7
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.ID = "SGRAVITYP" + i.ToString()
                            txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("SGRAVITYP" + i.ToString() + "").ToString(), 2)
                            txt.MaxLength = 6

                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 8
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            Dim wetCost As New Double
                            Dim perSolid As New Double
                            Dim dryCost As New Double
                            'Calculation Of Dry Cost
                            If CDbl(ds.Tables(0).Rows(0).Item("WETPP" + i.ToString() + "").ToString()) <> 0 Then
                                wetCost = CDbl(ds.Tables(0).Rows(0).Item("WETPP" + i.ToString() + "").ToString())
                            Else
                                wetCost = CDbl(ds.Tables(0).Rows(0).Item("WETPS" + i.ToString() + "").ToString())
                            End If

                            If CDbl(ds.Tables(0).Rows(0).Item("PERSOLP" + i.ToString() + "").ToString()) <> 0 Then
                                perSolid = CDbl(ds.Tables(0).Rows(0).Item("PERSOLP" + i.ToString() + "").ToString())
                            Else
                                perSolid = CDbl(ds.Tables(0).Rows(0).Item("PERSOLS" + i.ToString() + "").ToString())
                            End If

                            If perSolid > 0 Then
                                dryCost = CDbl(wetCost / perSolid)
                            Else
                                dryCost = "na"
                            End If
                            If perSolid > 0 Then
                                lbl.Text = FormatNumber(dryCost, 2)
                            Else
                                lbl.Text = dryCost
                            End If

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
                tblComparision.Controls.Add(trInner)
            Next







        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetColorDetails(ByRef LinkCol As HyperLink, ByVal hid As HiddenField, ByVal ColId As Integer)
        Dim Ds As New DataSet
        Dim ObjGetdata As New MoldE1GetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty
        Try

            Ds = ObjGetdata.GetColors(ColId)
            LinkCol.Text = Ds.Tables(0).Rows(0).Item("COLORNAME").ToString()
            LinkCol.ToolTip = Ds.Tables(0).Rows(0).Item("COLORNAME").ToString()
            LinkCol.Attributes.Add("text-decoration", "none")

            hid.Value = ColId.ToString()
            Path = "../PopUp/GetColorPopup.aspx?Des=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + LinkCol.ClientID + "&Id=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + hid.ClientID + ""
            LinkCol.NavigateUrl = "javascript:ShowPopWindow('" + Path + "')"

        Catch ex As Exception
            ErrorLable.Text = "Error:GetColorDetails:" + ex.Message.ToString() + ""
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

    Protected Sub TextBoxSetting(ByVal txt As TextBox, ByVal Css As String)
        Try
            txt.CssClass = Css

        Catch ex As Exception
            _lErrorLble.Text = "Error:TextBoxSetting:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub LableSetting(ByVal lbl As Label, ByVal Css As String)
        Try
            lbl.CssClass = Css

        Catch ex As Exception
            _lErrorLble.Text = "Error:LableSetting:" + ex.Message.ToString()
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

    Protected Sub Update_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim WetCost(10) As String
        Dim PerSol(10) As String
        Dim SGravity(10) As String
        Dim i As New Integer
        Dim ObjUpIns As New MoldE1UpInsData.UpdateInsert()
        Dim ObjGetData As New MoldE1GetData.Selectdata()
        Dim obj As New CryptoHelper
        Try

            If Not objRefresh.IsRefresh Then
                For i = 1 To 11
                    WetCost(i - 1) = Request.Form("ctl00$MoldE1InkContentPlaceHolder$WETPP" + i.ToString() + "")
                    PerSol(i - 1) = Request.Form("ctl00$MoldE1InkContentPlaceHolder$PERSOLP" + i.ToString() + "")
                    SGravity(i - 1) = Request.Form("ctl00$MoldE1InkContentPlaceHolder$SGRAVITYP" + i.ToString() + "")
                Next
                ObjUpIns.ColorPreferenceUpdate(Session("UserName").ToString(), WetCost, PerSol, SGravity, CaseId)

                ' Calculate()

                'Update Server Date
                ObjUpIns.ServerDateUpdate(CaseId, Session("MoldE1UserName"))
            End If
            GetPageDetails()
        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub Calculate()
        Try
            Dim MoldEcon1Conn As String = System.Configuration.ConfigurationManager.AppSettings("MoldE1ConnectionString")
            Dim obj As New MoldEcon1Calculation.MoldEcon1Calculation(MoldEcon1Conn)
            obj.MoldEcon1Calculate(CaseId)
        Catch ex As Exception
            ErrorLable.Text = "Error:Calculate:" + ex.Message.ToString() + ""
        End Try
    End Sub
End Class
