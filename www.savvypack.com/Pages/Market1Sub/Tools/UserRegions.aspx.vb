Imports System.Data
Imports System.Data.OleDb
Imports System
Imports M1GetData
Imports M1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Market1_Tools_UserRegions
    Inherits System.Web.UI.Page
#Region "Get Set Variables"
    Dim _lErrorLble As Label
    Dim _iUserId As Integer
    Dim _strUserRole As String
    Dim _btnLogOff As ImageButton
    Dim _divMainHeading As HtmlGenericControl
    Dim _ctlContentPlaceHolder As ContentPlaceHolder


    Public Property ErrorLable() As Label
        Get
            Return _lErrorLble
        End Get
        Set(ByVal Value As Label)
            _lErrorLble = Value
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

    Public Property LogOffbtn() As ImageButton
        Get
            Return _btnLogOff
        End Get
        Set(ByVal value As ImageButton)
            _btnLogOff = value
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

    Public Property ctlContentPlaceHolder() As ContentPlaceHolder
        Get
            Return _ctlContentPlaceHolder
        End Get
        Set(ByVal value As ContentPlaceHolder)
            _ctlContentPlaceHolder = value
        End Set
    End Property



#End Region

#Region "MastePage Content Variables"

    Protected Sub GetMasterPageControls()
        GetErrorLable()
        GetLogOffbtn()
        GetMainHeadingdiv()
        'GetContentPlaceHolder()
    End Sub

    Protected Sub GetErrorLable()
        ErrorLable = Page.Master.FindControl("lblError")
    End Sub

    Protected Sub GetLogOffbtn()
        LogOffbtn = Page.Master.FindControl("imgLogoff")
        LogOffbtn.Visible = False
    End Sub

    Protected Sub GetMainHeadingdiv()
        MainHeading = Page.Master.FindControl("divMainHeading")
        MainHeading.Attributes.Add("onmouseover", "Tip('User Regions')")
        MainHeading.Attributes.Add("onmouseout", "UnTip()")
        MainHeading.InnerHtml = "Market - User Regions"
    End Sub

    Protected Sub GetContentPlaceHolder()
        ctlContentPlaceHolder = Page.Master.FindControl("Market1ContentPlaceHolder")
    End Sub

#End Region


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            GetMasterPageControls()
            GetPageDetails()

        Catch ex As Exception
            ErrorLable.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetPageDetails()
        Dim objGetData As New Selectdata
        Dim ds As New DataSet
        Dim trHeader As New TableRow
        Dim tdHeader As TableCell
        Dim trInner As New TableRow
        Dim tdInner As TableCell
        Dim i As New Integer
        Dim j As New Integer
        Dim hyp As New HyperLink
        Dim objCrypto As New CryptoHelper
        Try

            hypNew.NavigateUrl = "UsrRegionCntry.aspx?RegionId=" + objCrypto.Encrypt("0") + ""
            ds = objGetData.GetRegionDetails(Session("UserId"))
            trHeader = New TableRow

            For i = 1 To 2
                tdHeader = New TableCell
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "", "Region", "1")
                        trHeader.Controls.Add(tdHeader)
                    Case 2
                        HeaderTdSetting(tdHeader, "", "Region Countries", "1")
                        trHeader.Controls.Add(tdHeader)
                End Select
                tbl.Controls.Add(trHeader)
            Next

            For i = 0 To ds.Tables(0).Rows.Count - 1
                trInner = New TableRow
                For j = 1 To 2
                    tdInner = New TableCell
                    Select Case j
                        Case 1
                            hyp = New HyperLink()
                            InnerTdSetting(tdInner, "", "Left")
                            hyp.Text = ds.Tables(0).Rows(i).Item("REGRIONNAME").ToString()
                            hyp.NavigateUrl = "UsrRegionCntry.aspx?RegionId=" + objCrypto.Encrypt(ds.Tables(0).Rows(i).Item("REGIONID").ToString()) + ""
                            tdInner.Controls.Add(hyp)
                            trInner.Controls.Add(tdInner)
                        Case 2
                            InnerTdSetting(tdInner, "", "Left")
                            tdInner.Text = ds.Tables(0).Rows(i).Item("COUNTRYDES").ToString()
                            trInner.Controls.Add(tdInner)
                    End Select
                Next
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
                tbl.Controls.Add(trInner)
            Next


        Catch ex As Exception
            ErrorLable.Text = "Error:Page_Load:" + ex.Message.ToString()
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
End Class
