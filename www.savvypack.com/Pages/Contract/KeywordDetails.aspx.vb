Imports System.Web.UI.HtmlTextWriter
Imports System.Data
Imports ContrGetData
Partial Class Pages_Contract_KeywordDetails
    Inherits System.Web.UI.Page

#Region "Get Set Variables"
    Dim _lErrorLble As Label
    Dim _divMainHeading As HtmlGenericControl
    Dim _ctlContentPlaceHolder As ContentPlaceHolder
    Dim _strKeyword As String
    Dim _btnLogOff As ImageButton


    Public Property ErrorLable() As Label
        Get
            Return _lErrorLble
        End Get
        Set(ByVal Value As Label)
            _lErrorLble = Value
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

    Public Property Keyword() As String
        Get
            Return _strKeyword
        End Get
        Set(ByVal value As String)
            _strKeyword = value
        End Set
    End Property





    Public DataCnt As Integer
    Public CaseDesp As New ArrayList
#End Region

#Region "MastePage Content Variables"

    Protected Sub GetMasterPageControls()
        GetContentPlaceHolder()
        GetLogOffbtn()
        GetErrorLable()
        GetMainHeadingdiv()
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
        MainHeading.Attributes.Add("onmouseover", "Tip('Keyword Search Results ')")
        MainHeading.Attributes.Add("onmouseout", "UnTip()")
        MainHeading.InnerHtml = "Contract - Keyword Search Results "
    End Sub

    Protected Sub GetContentPlaceHolder()
        ctlContentPlaceHolder = Page.Master.FindControl("Econ1ContentPlaceHolder")
    End Sub



#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objCryptoHelper As New CryptoHelper()
        Try
            Keyword = objCryptoHelper.Decrypt(Request.QueryString("Keyword"))
            GetMasterPageControls()
            GetPageDetails()
        Catch ex As Exception
            ErrorLable.Text = "Error:Page_Load:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetPageDetails()
        Dim objGetdata As New Selectdata()
        Dim ds As New DataSet
        Dim i As New Integer
        Dim j As New Integer
        Dim trHeader As New TableRow
        Dim trInner As New TableRow
        Dim tdHeader As TableCell
        Dim tdInner As TableCell
        Dim hyp As New HyperLink
        Dim CompanyId As String = String.Empty
        Dim objCryptoHelper As New CryptoHelper()
        Dim RowCnt As New Integer
        Try
            ds = objGetdata.GetSearchResults(Session("USERID").ToString(), "keyword")

            trHeader = New TableRow
            For i = 1 To 2
                tdHeader = New TableCell
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "20%", "", "1")
                        trHeader.Controls.Add(tdHeader)
                    Case 2
                        HeaderTdSetting(tdHeader, "80%", "Search Results KEYWORD(<span style='color:red;'>" + Keyword + "</span>) Count:(<span style='color:red;'>" + ds.Tables(0).Rows.Count.ToString() + "</span>)", "1")
                        trHeader.Controls.Add(tdHeader)
                End Select
            Next
            tblComparision.Controls.Add(trHeader)

            For i = 0 To ds.Tables(0).Rows.Count - 1
                trInner = New TableRow
                For j = 1 To 2
                    tdInner = New TableCell
                    Select Case j

                        Case 1
                            InnerTdSetting(tdInner, "", "Right")
                            If i = 0 Then
                                tdInner.Text = "<b>Companies:</b>"
                            Else
                                tdInner.Text = ""
                            End If
                            trInner.Controls.Add(tdInner)
                        Case 2

                            InnerTdSetting(tdInner, "", "Left")
                            CompanyId = objCryptoHelper.Encrypt(ds.Tables(0).Rows(i).Item("COMPANYID").ToString())
                            hyp = New HyperLink
                            hyp.ID = "hyp_" + i.ToString()
                            hyp.Text = ds.Tables(0).Rows(i).Item("NAME").ToString()
                            hyp.NavigateUrl = "Profile.aspx?Id=" + CompanyId + ""
                            hyp.Target = "_blank"
                            hyp.CssClass = "Link"
                            tdInner.Controls.Add(hyp)
                            trInner.Controls.Add(tdInner)
                    End Select

                Next
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
                tblComparision.Controls.Add(trInner)
            Next
        Catch ex As Exception
            ErrorLable.Text = "Error:GetPageDetails:" + ex.Message.ToString() + ""
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
        Catch ex As Exception
            _lErrorLble.Text = "Error:InnerTdSetting:" + ex.Message.ToString()
        End Try
    End Sub

End Class
