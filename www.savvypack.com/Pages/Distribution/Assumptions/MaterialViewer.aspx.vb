Imports System.Data
Imports System.Data.OleDb
Imports System
Imports DistGetData
Imports DistUpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Distribution_PopUp_MaterialViewer
    Inherits System.Web.UI.Page
    Dim _iCaseId As Integer
    Dim _strCaseDe1 As String
    Dim _strCaseDe2 As String

    Public Property CaseId() As Integer
        Get
            Return _iCaseId
        End Get
        Set(ByVal Value As Integer)
            _iCaseId = Value
        End Set
    End Property

    Public Property CaseDe1() As String
        Get
            Return _strCaseDe1
        End Get
        Set(ByVal Value As String)
            _strCaseDe1 = Value
        End Set
    End Property

    Public Property CaseDe2() As String
        Get
            Return _strCaseDe2
        End Get
        Set(ByVal Value As String)
            _strCaseDe2 = Value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Dim MatId As Integer
                GetMaterials()

                MatId = ddlMat.SelectedValue
                GetMaterialDetails(MatId)
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetMaterials()
        Dim objGetData As New DistGetData.Selectdata()
        Dim ds As New DataSet()
        Try
            ds = objGetData.GetMaterials(-1, "", "")
            With ddlMat
                .DataSource = ds
                .DataTextField = "MATDES"
                .DataValueField = "MATID"
                .DataBind()
                .Font.Size = 8
            End With
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetMaterials:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetMaterialDetails(ByVal MatId As Integer)
        Dim ds As New DataSet
        Dim dsPref As New DataSet
        Dim objGetData As New DistGetData.Selectdata()
        Dim i As New Integer
        Dim Dept As String = String.Empty
        Try
            ds = objGetData.GetMaterials(MatId, "", "")
            dsPref = objGetData.GetPref(Session("DistCaseId").ToString())
            Dim priceVal As Double
            priceVal = CDbl(ds.Tables(0).Rows(0).Item("price").ToString() * dsPref.Tables(0).Rows(0).Item("curr").ToString() / dsPref.Tables(0).Rows(0).Item("convwt").ToString())
            lblPriceVal.Text = FormatNumber(priceVal, 3)
            lblSGVal.Text = FormatNumber(ds.Tables(0).Rows(0).Item("sg").ToString(), 2)
            lblMatId.Text = MatId.ToString()
            lblPrice.Text = "Price (" + dsPref.Tables(0).Rows(0).Item("TITLE2").ToString() + ")"

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetMaterialDetails:" + ex.Message.ToString()
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

    Protected Sub btnPrivious_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrivious.Click
        Try
            Dim MatID As Integer
            btnNext.Enabled = True
            If ddlMat.SelectedIndex = 0 Then
                btnPrivious.Enabled = False
            End If


            MatID = CInt(ddlMat.Items(ddlMat.SelectedIndex - 1).Value)
            GetMaterialDetails(MatID)
            ddlMat.SelectedValue = MatID
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Try
            Dim MatID As Integer
            btnPrivious.Enabled = True
            If ddlMat.SelectedIndex = ddlMat.Items.Count - 1 Then
                btnNext.Enabled = False
            End If
            MatID = CInt(ddlMat.Items(ddlMat.SelectedIndex + 1).Value)
            GetMaterialDetails(MatID)
            ddlMat.SelectedValue = MatID
        Catch ex As Exception

        End Try
    End Sub
End Class
