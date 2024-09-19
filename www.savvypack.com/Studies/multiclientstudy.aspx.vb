Imports System.Data
Imports System.Data.OleDb
Imports System
Imports StudyGetData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Studies_multiclientstudy
    Inherits System.Web.UI.Page

#Region "Get Set Variables"
    Dim _lErrorLble As Label
    
    Public Property ErrorLable() As Label
        Get
            Return _lErrorLble
        End Get
        Set(ByVal Value As Label)
            _lErrorLble = Value
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

        GetPageDetails()

    End Sub
    Public Sub GetPageDetails()
        Dim ds As New DataSet
        Dim objGetData As New StudyGetData.Selectdata
        Dim i As New Integer
        Dim j As New Integer
        Dim DWidth As String = String.Empty
        Dim trHeader As New TableRow
        Dim trHeader1 As New TableRow
        Dim trHeader2 As New TableRow

        Dim trInner As New TableRow
        Dim tdHeader As TableCell
        Dim tdHeader1 As TableCell
        Dim tdHeader2 As TableCell
        Dim lbl As New Label
        Dim hid As New HiddenField
        Dim Link As New HyperLink
        Dim txt As New TextBox
        Dim tdInner As TableCell
        Dim chk As New CheckBox
        Dim radio As New RadioButton
        Dim objCrypto As New CryptoHelper()


        Try
            ds = objGetData.GetStudyDetails("")
            For i = 1 To ds.Tables(0).Rows.Count
                trInner = New TableRow
                tdInner = New TableCell
                InnerTdSetting(tdInner, "100%", "Left")
                Link = New HyperLink
                hid = New HiddenField
                Link.ID = "hypStudyDes" + i.ToString()
                Link.CssClass = "LinkStudy"
                Link.Text = ds.Tables(0).Rows(i - 1).Item("LinkText").ToString()
                
                Link.Style.Add("Width", "100%")

                Link.NavigateUrl = "~/Studies/" + ds.Tables(0).Rows(i - 1).Item("SHORTTITLE").ToString().Replace(" ", "_").Replace(".", "") + "_Details.aspx" '"~/Studies/Category.htm" ' "StudyDetails.aspx?ID=" + objCrypto.Encrypt(ds.Tables(0).Rows(i - 1).Item("ReportID").ToString()) + " "
                Link.NavigateUrl = Link.NavigateUrl.Replace("__", "_")
             
                Link.ToolTip = ds.Tables(0).Rows(i - 1).Item("ReportHeader").ToString()

                lbl = New Label()
                lbl.Text = "<span style='color:Maroon;font-weight:bold'>NEW - </span>  "
                If ds.Tables(0).Rows(i - 1).Item("IsNew").ToString() = "Y" Then
                    tdInner.Controls.Add(lbl)
                End If
                tdInner.Controls.Add(Link)

                'Adding Short Description
                lbl = New Label()
                lbl.Style.Add("width", "100%")
                lbl.Text = "<br/><span style='color:black;font-size:12px;font-family:Arial,Helvetica,Geneva,Swiss,SunSans-Regular;'>" + ds.Tables(0).Rows(i - 1).Item("ShortDescription").ToString() + " </span>  "
                tdInner.Controls.Add(lbl)
                trInner.Controls.Add(tdInner)

                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
                trInner.Height = 23
                tblComparision.Controls.Add(trInner)
            Next

        Catch ex As Exception
            lblError.Text = "Error:GetPageDetails:" + ex.Message.ToString()
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
            lblError.Text = "Error:InnerTdSetting:" + ex.Message.ToString()
        End Try
    End Sub

    
End Class
