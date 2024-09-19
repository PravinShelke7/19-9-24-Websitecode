Imports System.Web.UI.HtmlTextWriter
Imports System.Data
Imports PackProdGetData
Partial Class Pages_PackProd_LogicSearch
    Inherits System.Web.UI.Page
#Region "Get Set Variables"
    Dim _lErrorLble As Label
    Dim _divMainHeading As HtmlGenericControl
    Dim _ctlContentPlaceHolder As ContentPlaceHolder
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
        MainHeading.Attributes.Add("onmouseover", "Tip('Logic Search Results')")
        MainHeading.Attributes.Add("onmouseout", "UnTip()")
        MainHeading.InnerHtml = "Packaging Producer - Logic Search Results"
    End Sub

    Protected Sub GetContentPlaceHolder()
        ctlContentPlaceHolder = Page.Master.FindControl("PackProdContentPlaceHolder")
    End Sub



#End Region



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objCryptoHelper As New CryptoHelper()
        Try
            GetMasterPageControls()
            GetPageDetails()
        Catch ex As Exception
            ErrorLable.Text = "Error:Page_Load:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetPageDetails()
        Dim objGetdata As New Selectdata()
        Dim dsLogicOR As New DataSet
        Dim dsLogicAND As New DataSet
        Dim dsCompany As New DataSet
        Dim i As New Integer
        Dim j As New Integer
        Dim k As New Integer
        Dim trHeader As New TableRow
        Dim trInner As New TableRow
        Dim tdHeader As TableCell
        Dim tdInner As TableCell
        Dim hyp As New HyperLink
        Dim CompanyId As String = String.Empty
        Dim objCryptoHelper As New CryptoHelper()
        Dim RowCnt As New Integer
        Dim tbl As New Table
        Dim tr As New TableRow
        Dim td As New TableCell

        Try
            dsLogicOR = objGetdata.GetLogicORSearchResults(Session("UserName").ToString())

            'Logic OR
            trHeader = New TableRow
            For i = 1 To 3
                tdHeader = New TableCell
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "33%", "<b>Search Category</b><br/>(<span style='font-weight:normal;'>Search Criteria</span>)", "1")
                        trHeader.Controls.Add(tdHeader)
                    Case 2
                        HeaderTdSetting(tdHeader, "33%", "Logic Search Results - OR", "1")
                        trHeader.Controls.Add(tdHeader)
                    Case 3
                        HeaderTdSetting(tdHeader, "33%", "Logic Search Results - AND", "1")
                        trHeader.Controls.Add(tdHeader)
                End Select

            Next
            trHeader.Height = 30
            tblLogicOR.Controls.Add(trHeader)

            'Inner
            For i = 0 To dsLogicOR.Tables(0).Rows.Count - 1
                trInner = New TableRow
                For j = 1 To 3
                    tdInner = New TableCell
                    tdInner.VerticalAlign = VerticalAlign.Top
                    Select Case j
                        Case 1
                            InnerTdSetting(tdInner, "", "Left")
                            If dsLogicOR.Tables(0).Rows(i).Item("CODE").ToString() = "COUNTRY" Then
                                tdInner.Text = "<b>" + dsLogicOR.Tables(0).Rows(i).Item("CATEGORY").ToString() + "</b><br/>(" + dsLogicOR.Tables(0).Rows(i).Item("DETAILS").ToString() + ")" + "<br/><b>State</b><br/>(" + Session("PSearchState") + ")"
                            Else
                                tdInner.Text = "<b>" + dsLogicOR.Tables(0).Rows(i).Item("CATEGORY").ToString() + "</b><br/>(" + dsLogicOR.Tables(0).Rows(i).Item("DETAILS").ToString() + ")"
                            End If

                            trInner.Controls.Add(tdInner)
                        Case 2
                            InnerTdSetting(tdInner, "", "Left")
                            dsCompany = New DataSet
                            If dsLogicOR.Tables(0).Rows(i).Item("CODE").ToString() = "COUNTRY" Then
                                If Session("PSearchState") <> "All" Then
                                    dsCompany = objGetdata.GetSearchResults(Session("UserName").ToString(), "STATE")
                                Else
                                    dsCompany = objGetdata.GetSearchResults(Session("UserName").ToString(), "COUNTRY")
                                End If
                            Else
                                dsCompany = objGetdata.GetSearchResults(Session("UserName").ToString(), dsLogicOR.Tables(0).Rows(i).Item("CODE").ToString())
                            End If
                            tbl = New Table()
                            If dsCompany.Tables(0).Rows.Count > 0 Then
                                For k = 0 To dsCompany.Tables(0).Rows.Count - 1
                                    tr = New TableRow
                                    td = New TableCell
                                    hyp = New HyperLink
                                    hyp.CssClass = "Link"
                                    hyp.Target = "_blank"
                                    CompanyId = String.Empty
                                    CompanyId = objCryptoHelper.Encrypt(dsCompany.Tables(0).Rows(k).Item("COMPANYID").ToString())
                                    hyp.ID = i.ToString() + "_" + k.ToString()
                                    hyp.Text = dsCompany.Tables(0).Rows(k).Item("NAME").ToString()
                                    hyp.NavigateUrl = "Profile.aspx?Id=" + CompanyId + ""
                                    td.Controls.Add(hyp)
                                    tr.Controls.Add(td)
                                    tbl.Controls.Add(tr)
                                Next
                                tdInner.Controls.Add(tbl)
                            Else
                                tdInner.Text = ""
                            End If

                            trInner.Controls.Add(tdInner)

                        Case 3
                            If i = 0 Then
                                InnerTdSetting(tdInner, "", "Left")
                                dsCompany = New DataSet
                                dsCompany = objGetdata.GetLogicAndSearchResults(Session("UserName").ToString())
                                tbl = New Table()
                                If dsCompany.Tables(0).Rows.Count > 0 Then
                                    For k = 0 To dsCompany.Tables(0).Rows.Count - 1
                                        tr = New TableRow
                                        td = New TableCell
                                        hyp = New HyperLink
                                        hyp.CssClass = "Link"
                                        hyp.Target = "_blank"
                                        CompanyId = String.Empty
                                        CompanyId = objCryptoHelper.Encrypt(dsCompany.Tables(0).Rows(k).Item("COMPANYID").ToString())
                                        hyp.ID = i.ToString() + "_" + k.ToString()
                                        hyp.Text = dsCompany.Tables(0).Rows(k).Item("NAME").ToString()
                                        hyp.NavigateUrl = "Profile.aspx?Id=" + CompanyId + ""
                                        td.Controls.Add(hyp)
                                        tr.Controls.Add(td)
                                        tbl.Controls.Add(tr)
                                    Next
                                    tdInner.Controls.Add(tbl)
                                Else
                                    tdInner.Text = "No Record Found"
                                End If

                                trInner.Controls.Add(tdInner)
                            End If

                    End Select
                Next
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
                tblLogicOR.Controls.Add(trInner)
            Next

            tblLogicOR.Rows(1).Cells.Item(2).RowSpan = tblLogicOR.Rows.Count - 1
            tblLogicOR.Rows(1).Cells.Item(2).CssClass = "AlterNateColor3"








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
