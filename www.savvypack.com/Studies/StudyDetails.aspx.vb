Imports System.Data
Imports System.Data.OleDb
Imports System
Imports StudyGetData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Studies_StudyDetails
    Inherits System.Web.UI.Page

#Region "Get Set Variables"
    Dim _lErrorLble As Label    
    Dim _strReportId As String    

    Public Property ErrorLable() As Label
        Get
            Return _lErrorLble
        End Get
        Set(ByVal Value As Label)
            _lErrorLble = Value
        End Set
    End Property
    
    Public Property ReportId() As String
        Get
            Return _strReportId
        End Get
        Set(ByVal Value As String)
            _strReportId = Value
        End Set
    End Property

#End Region

#Region "MastePage Content Variables"

    Protected Sub GetMasterPageControls()
        Session("MenuItem") = "MCB"
        GetErrorLable()
    End Sub

    Protected Sub GetErrorLable()
        ErrorLable = Page.Master.FindControl("lblError")
    End Sub
    
#End Region


#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_STUDIES_MULTICLIENTSTUDY")
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        objRefresh.Render(Page)
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load        
        Dim ObjCrypto As New CryptoHelper()
        Dim objGetData As New StudyGetData.Selectdata
        Dim ds As New DataSet
        Try
            If Session("UserId") <> Nothing Then
                hdnUserId.Value = Session("UserId")
            End If

            If Request.QueryString("ID") = Nothing Then
                ds = objGetData.GetStudyDetailsByTitle(Request.QueryString("TITLE").Replace("'", "''"))
                ReportId = ds.Tables(0).Rows(0).Item("REPORTID") 'ObjCrypto.Decrypt(Request.QueryString("ID"))
            Else
                ReportId = ObjCrypto.Decrypt(Request.QueryString("ID"))
            End If


            If ReportId = "RRP06A" Or ReportId = "RTO07A" Or ReportId = "RPP07A" Or ReportId = "ROF07A" Then
                imgbtnVwSample.Style.Add("display", "none")
            End If

            'If Not IsPostBack Then
            GetPageDetails(ReportId)
            GetStudyData()
            'End If
        Catch ex As Exception
            ErrorLable.Text = "Error:Page_Load:" + ex.Message.ToString() + ""
        End Try
    End Sub
    Protected Sub GetStudyData()
        Dim objgetData As New StudyGetData.Selectdata
        Dim ds As New DataSet
        Dim ObjCrypto As New CryptoHelper()
        Try
            ds = objgetData.GetStudyDetails(ReportId)
            'Putting Report Details on Hidden Field
            hdnRepId.Value = ReportId
            hdnRepEId.Value = ObjCrypto.Encrypt(ReportId)
            hdnBRPDF.Value = ds.Tables(0).Rows(0).Item("BROCHUREPAGES").ToString().Trim()
            hdnTOCPDF.Value = ds.Tables(0).Rows(0).Item("TOCPAGES").ToString().Trim()
            hdnSAMPDF.Value = ds.Tables(0).Rows(0).Item("BROCHUREPAGES").ToString().Trim()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GetPageDetails(ByVal strRepId As String)
        Try
            Dim ds As New DataSet
            Dim dsInventory As New DataSet
            Dim objGetData As New StudyGetData.Selectdata
            Dim str As String = String.Empty
            ds = GetData(strRepId)
            Dim i As Integer
            If ds.Tables(0).Rows.Count > 0 Then
                Page.Title = ds.Tables(0).Rows(0).Item("linkText")
                'Binding Header Data
                lblheader.Text = ds.Tables(0).Rows(0).Item("ReportHeader")
                Page.Title = ds.Tables(0).Rows(0).Item("linkText")
                'Binding Report Details
                dvContentData.InnerHtml = ds.Tables(0).Rows(0).Item("REPORTDETAILS")

                AddMetaData(Convert.ToString(ds.Tables(0).Rows(0).Item("MetaData")))

            End If
            'Binding Features

            dsInventory = objGetData.GetInventoryLineDetails(ReportId)

            If dsInventory.Tables(0).Rows.Count > 0 Then
                str = "<ul style='font-size:10.2pt;color:#000000;font-family:Optima;'>"
                For i = 0 To dsInventory.Tables(0).Rows.Count - 1
                    str = str + "<li style='margin-top:3px;'><b>" + dsInventory.Tables(0).Rows(i).Item("TITLE").ToString() + ":</b> " + dsInventory.Tables(0).Rows(i).Item("TITLEDES").ToString() + "</li>"
                Next
                str = str + "</ul>"
            End If

            divLineItems.InnerHtml = str

        Catch ex As Exception
            ErrorLable.Text = "Error:GetPageDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Function GetData(ByVal strRepId As String) As DataSet
        Dim ds As New DataSet
        Try
            Dim objGetData As New StudyGetData.Selectdata
            ds = objGetData.GetStudyDetails(strRepId)
            Return ds
        Catch ex As Exception
            Return ds
        End Try
    End Function

    Protected Sub AddMetaData(ByVal MetaData As String)
        Dim metaTag As New HtmlMeta
        Dim HeadTag As HtmlHead = CType(Page.Header, HtmlHead)
        Try
            metaTag.Attributes.Add("name", "keywords")
            metaTag.Attributes.Add("content", MetaData)
            'HeadTag.Controls.Add(metaTag)
        Catch ex As Exception
            ErrorLable.Text = "Error:AddMetaData:" + ex.Message.ToString() + ""
        End Try
    End Sub


    Protected Sub imgbtnVwBrch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnVwBrch.Click
        Try
            Dim objgetData As New StudyGetData.Selectdata
            Dim ds As New DataSet
            ds = objgetData.GetStudyDetails(ReportId)
            If ds.Tables(0).Rows.Count > 0 Then
                Page.Title = ds.Tables(0).Rows(0).Item("linkText")

                ' Page.ClientScript.RegisterStartupScript(Me.GetType(), "_New", " OpenNewWindow('../Studies2/" + ds.Tables(0).Rows(0).Item("BROCHUREPAGES").ToString().Trim() + "');", True)
            End If
        Catch ex As Exception
            ErrorLable.Text = "Error:imgbtnVwBrch_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub imgbtnOrder_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnOrder.Click
        Try
            If Not objRefresh.IsRefresh Then
                Dim ObjCrypto As New CryptoHelper()
                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "_New", " OpenNewWindow('../ShoppingCart/Order.aspx?Id=" + ObjCrypto.Encrypt(ReportId) + "');", True)
            End If
        Catch ex As Exception
            ErrorLable.Text = "Error:imgbtnOrder_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub imgbtnVwToc_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnVwToc.Click
        Try
            Dim objgetData As New StudyGetData.Selectdata
            Dim ds As New DataSet
            ds = objgetData.GetStudyDetails(ReportId)
            If ds.Tables(0).Rows.Count > 0 Then
                Page.Title = ds.Tables(0).Rows(0).Item("linkText")

                ' Page.ClientScript.RegisterStartupScript(Me.GetType(), "_New", " OpenNewWindow('../Studies2/" + ds.Tables(0).Rows(0).Item("TOCPAGES").ToString().Trim() + "');", True)
            End If
        Catch ex As Exception
            ErrorLable.Text = "Error:imgbtnVwBrch_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub imgbtnVwSample_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnVwSample.Click
        Try
            Dim objgetData As New StudyGetData.Selectdata
            Dim ds As New DataSet
            Dim EncRepId As String
            Dim obj As New CryptoHelper
            ds = objgetData.GetSamplePageDetails(ReportId)
            EncRepId = obj.Encrypt(ReportId).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + ""
            If Session("UserId") Is Nothing Or Session("UserId") = "" Then
                Session("URL") = "~/Studies/SamplePagesMail.aspx?Id=" + EncRepId + ""

                ' Page.ClientScript.RegisterStartupScript(Me.GetType(), "_New", " OpenNewWindow('../Users_Login/Login.aspx');", True)
            Else
                Session("URL") = ""
                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "_New", " OpenNewWindow('../Studies/SamplePagesMail.aspx?Id=" + EncRepId + "');", True)
            End If



        Catch ex As Exception
            ErrorLable.Text = "Error:imgbtnVwBrch_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnOrder_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOrder.Click
        Try
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "UManager", "OpenOrderPage();", True)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnSmple_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSmple.Click
        Try
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "UManager", "OpenSamplePage();", True)
        Catch ex As Exception

        End Try
    End Sub
End Class
