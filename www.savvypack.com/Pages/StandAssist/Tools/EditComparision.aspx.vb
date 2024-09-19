Imports System.Data
Imports System.Data.OleDb
Imports System.HttpStyleUriParser
Imports System.Web.UI.WebControls
Imports S1GetData
Imports S1UpInsData
Partial Class Pages_Sustain1_Tools_EditComparision
    Inherits System.Web.UI.Page
    Dim GetData As New Selectdata()
#Region "Get Set Variables"
    Dim _lErrorLble As Label
    Dim _strUserName As String
    Dim _strPassword As String
    Dim _iGroupID As String
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

    Public Property GroupID() As String
        Get
            Return _iGroupID
        End Get
        Set(ByVal Value As String)
            Dim obj As New CryptoHelper
            _iGroupID = obj.Decrypt(Value)
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
        Updatebtn.Visible = False
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
            GroupID = Request.QueryString("Id").ToString().Replace("!Plus!", "+").Replace("!Hash!", "#").Replace("!And!", "&")
            If Not IsPostBack Then

                bindDetails()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub bindDetails()
        Try
            Dim Dts As New DataSet()
            Dim Dts1, Dts2 As New DataTable()
            Dts = GetData.GetGroupDetailsByID(GroupID)
            Dts1 = GetData.GetEditCases(Session("UserName"), GroupID, "true")
            Dts2 = GetData.GetEditCases(Session("UserName"), GroupID, "false")

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


            txtGName1.Text = Dts.Tables(0).Rows(0).Item("DES1").ToString()
            txtGName2.Text = Dts.Tables(0).Rows(0).Item("DES2").ToString()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Update_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Dim ObjUpIns As New UpdateInsert()
            'ObjUpIns.EditComparisonName(GroupID, txtCName.Text.Replace("'", "''"))
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
        Catch ex As Exception
            ErrorLable.Text = "Error:btnFwd_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnRew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRew.Click
        Try
            Transfer2(lstRegion1, lstRegion2, False)
        Catch ex As Exception
            ErrorLable.Text = "Error:btnRew_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
    Protected Sub Transfer1(ByVal Fromlst As ListBox, ByVal Tolst As ListBox, ByVal IsAll As Boolean)
        Dim i As New Integer
        Dim ObjUpIns As New S1UpInsData.UpdateInsert()
        Dim objGetData As New S1GetData.Selectdata()
        Dim dt As New DataSet()
        Dim Caseid As String

        Dim message As String
        Dim message1 As String
        Try
            Dim CaseIds(1000) As String
            Dim cnt As Integer = 1
            For i = 0 To Fromlst.Items.Count - 1
                If Fromlst.Items(i).Selected Then
                    CaseIds(cnt) = Fromlst.Items(i).Value
                    cnt += 1
                End If
            Next

            'Validating Cases
            Caseid = Request.Form("lstRegion1")
            dt = objGetData.ValiDateGroupcases(Caseid, Session("UserID").ToString())

            If dt.Tables(0).Rows.Count > 0 Then
                message = "--------------------------------------------------------------------\n"
                message1 = message + "Cases can only be included in one group.\n"
                For i = 0 To dt.Tables(0).Rows.Count - 1
                    message1 = message1 + "      Cases " + dt.Tables(0).Rows(i).Item("CaseID").ToString() + " is included in group " + dt.Tables(0).Rows(i).Item("GroupName").ToString()
                    If i = dt.Tables(0).Rows.Count - 1 Then
                        message1 = message1 + "\n" + message + "\n"
                    Else
                        message1 = message1 + "\n"
                    End If
                Next
                message = message + "--------------------------------------------------------------------\n"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "New_WinCopy", "alert('" + message1 + "');", True)
            Else
                'Adding Cases
                ObjUpIns.UpdateGroup(GroupID, txtGName1.Text.Replace("'", "''"), txtGName2.Text.Replace("'", "''"), Session("UserId"), CaseIds)
                bindDetails()
            End If

        Catch ex As Exception
            ErrorLable.Text = "Error:Transfer:" + ex.Message.ToString() + ""
        End Try
    End Sub
    Protected Sub Transfer2(ByVal Fromlst As ListBox, ByVal Tolst As ListBox, ByVal IsAll As Boolean)
        Dim i As New Integer
        Dim ObjUpIns As New S1UpInsData.UpdateInsert()
        Try
            Dim CaseIds(1000) As String
            Dim cnt As Integer = 1
            For i = 0 To Tolst.Items.Count - 1
                If Not Tolst.Items(i).Selected Then
                    CaseIds(cnt) = Tolst.Items(i).Value
                    cnt += 1
                End If
            Next
            ' ObjUpIns.EditComparison(GroupID, CaseIds, 1)
            'Deleting Old Cases
            ObjUpIns.DeleteGroup(GroupID)
            'Adding Cases
            ObjUpIns.UpdateGroup(GroupID, txtGName1.Text.Replace("'", "''"), txtGName2.Text.Replace("'", "''"), Session("UserId"), CaseIds)
            bindDetails()
        Catch ex As Exception
            ErrorLable.Text = "Error:Transfer:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub imgUpdate_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgUpdate.Click
        Try
            Try
                Dim ObjUpIns As New UpdateInsert()
                ObjUpIns.EditGroupName(GroupID, txtGName1.Text.Replace("'", "''"), txtGName2.Text.Replace("'", "''"))
                bindDetails()
            Catch ex As Exception
                ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
            End Try
        Catch ex As Exception
            ErrorLable.Text = "Error:imgUpdate_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnUpdateDes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdateDes.Click
        Try
            Try
                Dim ObjUpIns As New UpdateInsert()
                ObjUpIns.EditGroupName(GroupID, txtGName1.Text.Replace("'", "''"), txtGName2.Text.Replace("'", "''"))
                bindDetails()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "New_WinCopy", "alert('Group data updated successfully.');", True)
            Catch ex As Exception
                ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
            End Try
        Catch ex As Exception
            ErrorLable.Text = "Error:imgUpdate_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
End Class
