Imports System.Data
Imports System.Data.OleDb
Imports System.HttpStyleUriParser
Imports System.Web.UI.WebControls
Imports S3GetData
Imports S3UpInsData
Partial Class Pages_Sustain3_Tools_EditComparision
    Inherits System.Web.UI.Page

    Dim GetData As New Selectdata()

#Region "Get Set Variables"
    Dim _lErrorLble As Label
    Dim _strUserName As String
    Dim _strPassword As String
    Dim _iAssumptionId As String
    Dim _btnUpdate As ImageButton
    Dim _btnGlobal As ImageButton



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

    Public Property AssumptionId() As String
        Get
            Return _iAssumptionId
        End Get
        Set(ByVal Value As String)
            Dim obj As New CryptoHelper
            _iAssumptionId = obj.Decrypt(Value)
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

    Public Property Globalbtn() As ImageButton
        Get
            Return _btnGlobal
        End Get
        Set(ByVal value As ImageButton)
            _btnGlobal = value
        End Set
    End Property



    Public DataCnt As Integer
    Public CaseDesp As New ArrayList

#End Region

#Region "MastePage Content Variables"

    Protected Sub GetMasterPageControls()
        GetErrorLable()
        GetUpdatebtn()
        GetGlobalbtn()
    End Sub
    Protected Sub GetErrorLable()
        ErrorLable = Page.Master.FindControl("lblError")
    End Sub
    Protected Sub GetUpdatebtn()
        Updatebtn = Page.Master.FindControl("imgUpdate")
        Updatebtn.Visible = True
        AddHandler Updatebtn.Click, AddressOf Update_Click

    End Sub
    Protected Sub GetGlobalbtn()
        Globalbtn = Page.Master.FindControl("imgGlobal")
        Globalbtn.Visible = True
        Globalbtn.CausesValidation = False
        AddHandler Globalbtn.Click, AddressOf UpdateGlobalbtn_Click
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            GetMasterPageControls()
            AssumptionId = Request.QueryString("Id").ToString().Replace("!Plus!", "+").Replace("!Hash!", "#").Replace("!And!", "&")
            If Not IsPostBack Then
                bindDetails()
                SetListboxWidth()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub SetListboxWidth()
        Try
            'First List Box
            Dim lstWidth1 As Int32 = 0
            For Each items As ListItem In Me.lstRegion1.Items
                If lstWidth1 < items.ToString.Length Then
                    lstWidth1 = items.ToString.Length
                End If
            Next
            If CInt(lstWidth1.ToString()) < 70 Then
                lstRegion1.Width = 450
            Else
                lstRegion1.Width = Nothing
            End If

            'Second List Box
            Dim lstWidth2 As Int32 = 0
            For Each items As ListItem In Me.lstRegion2.Items
                If lstWidth2 < items.ToString.Length Then
                    lstWidth2 = items.ToString.Length
                End If
            Next
            If CInt(lstWidth2.ToString()) < 70 Then
                lstRegion2.Width = 450
            Else
                lstRegion2.Width = Nothing
            End If

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub bindDetails()
        Try
            Dim Dts As New DataSet()
            Dim Dts1, Dts2 As New DataTable()
            Dts = GetData.GetSavedCaseAsperUser(Session("USERID").ToString(), AssumptionId)
            'Dts1 = GetData.GetEditCases(Session("UserName").ToString(), AssumptionId, "true")
            'Dts2 = GetData.GetEditCases(Session("UserName").ToString(), AssumptionId, "false")
            If Session("SavvyModType") <> "1" Then
                Dts1 = GetData.GetEditCasesS3(Session("USERID").ToString(), AssumptionId, "true", Session("S3LicAdmin"))
                Dts2 = GetData.GetEditCasesS3(Session("USERID").ToString(), AssumptionId, "false", Session("S3LicAdmin"))
            Else
                Dts1 = GetData.GetEditCases(Session("USERID").ToString(), AssumptionId, "true")
                Dts2 = GetData.GetEditCases(Session("USERID").ToString(), AssumptionId, "false")
            End If

            'Region First
            lstRegion1.DataTextField = "CASEDE"
            lstRegion1.DataValueField = "CaseId"
            lstRegion1.DataSource = Dts1
            lstRegion1.DataBind()

            'Region Second
            lstRegion2.DataTextField = "CASEDE"
            lstRegion2.DataValueField = "CaseId"
            lstRegion2.DataSource = Dts2
            lstRegion2.DataBind()


            txtCName.Text = Dts.Tables(0).Rows(0).Item("DESCRIPTION").ToString()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Update_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Dim ObjUpIns As New UpdateInsert()
            ObjUpIns.EditComparisonName(AssumptionId, txtCName.Text.Trim().Replace("'", "''").ToString())
            bindDetails()
        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
    Protected Sub UpdateGlobalbtn_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try

            Response.Redirect("../default.aspx")
        Catch ex As Exception
            ErrorLable.Text = "Error:UpdateGlobalbtn_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnFwd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFwd.Click
        Try
            Transfer1(lstRegion1, lstRegion2, False)
            SetListboxWidth()
        Catch ex As Exception
            ErrorLable.Text = "Error:btnFwd_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnRew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRew.Click
        Try
            Transfer2(lstRegion1, lstRegion2, False)
            SetListboxWidth()
        Catch ex As Exception
            ErrorLable.Text = "Error:btnRew_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
    Protected Sub Transfer1(ByVal Fromlst As ListBox, ByVal Tolst As ListBox, ByVal IsAll As Boolean)
        Dim i As New Integer
        Dim ObjUpIns As New UpdateInsert()
        Try
            Dim CaseIds(10) As String
            Dim cnt As Integer = 1
            For i = 0 To Fromlst.Items.Count - 1
                If Fromlst.Items(i).Selected Then
                    CaseIds(cnt) = Fromlst.Items(i).Value
                    cnt += 1
                End If
            Next
            ObjUpIns.EditComparison(AssumptionId, CaseIds, Tolst.Items.Count + 1)
            bindDetails()
        Catch ex As Exception
            ErrorLable.Text = "Error:Transfer:" + ex.Message.ToString() + ""
        End Try
    End Sub
    Protected Sub Transfer2(ByVal Fromlst As ListBox, ByVal Tolst As ListBox, ByVal IsAll As Boolean)
        Dim i As New Integer
        Dim ObjUpIns As New UpdateInsert()
        Try
            Dim CaseIds(10) As String
            Dim cnt As Integer = 1
            For i = 0 To Tolst.Items.Count - 1
                If Not Tolst.Items(i).Selected Then
                    CaseIds(cnt) = Tolst.Items(i).Value
                    cnt += 1
                End If
            Next
            ObjUpIns.EditComparison(AssumptionId, CaseIds, 1)
            bindDetails()
        Catch ex As Exception
            ErrorLable.Text = "Error:Transfer:" + ex.Message.ToString() + ""
        End Try
    End Sub





End Class

