Imports system.Data
Imports System.Data.OleDb
Partial Class InteractiveServices_InteractiveKb
    Inherits System.Web.UI.Page
    Dim obj As New CryptoHelper

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
        Dim ObjCrypto As New CryptoHelper()
        Try
            Session("MenuItem") = "ISERVICE"
            GetMasterPageControls()
            GetInventoryDetails()
            If Session("UserId") <> Nothing Then
                hdnUserId.Value = Session("UserId")
            End If
            hdnRepId.Value = ObjCrypto.Encrypt("KBCOPK")
        Catch ex As Exception
            lblError.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub
    Public Sub GetInventoryDetails()
        Dim objGetData As New InteractiveGetData.Selectdata
        Dim dsInventory As New DataSet
        Dim i As Integer = 0
        Dim var1, var2, var3, var4 As String
        Dim pubDate As DateTime
        Try
            dsInventory = objGetData.GetInventoryDetails("KBCOPK")
            If dsInventory.Tables(0).Rows.Count > 0 Then
                pubDate = Convert.ToDateTime(dsInventory.Tables(0).Rows(0).Item("PUBdate").ToString())
                var1 = pubDate.ToString("MMM dd , yyyy").ToString()

                'Getting Study Number
                var2 = dsInventory.Tables(0).Rows(0).Item("PartID").ToString()
                'Getting CopyRight
                var3 = dsInventory.Tables(0).Rows(0).Item("copyright").ToString()

                lblPDate.Text = var1
                lblKNo.Text = var2
                lblCopyR.Text = var3
                For i = 0 To dsInventory.Tables(0).Rows.Count - 1
                    If dsInventory.Tables(0).Rows(i).Item("delFORMAT").ToString().ToUpper() = " ANNUAL SUBSCRIPTION - 3 USERS" Then
                        var4 = "US$ " + FormatNumber(dsInventory.Tables(0).Rows(i).Item("Price").ToString(), 0)
                        'lblAnualSub.Text = var4
                        lblAnualSub.Text = "US$ " + FormatNumber(dsInventory.Tables(0).Rows(i).Item("Price").ToString(), 2) ' var4
                    End If
                Next
            End If
        Catch ex As Exception
            lblError.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub imgbtnOrder_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnOrder.Click
        Try
            Dim ObjCrypto As New CryptoHelper()
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "_New", " window.open('../ShoppingCart/Order.aspx?Id=" + ObjCrypto.Encrypt("KBCOPK") + "');", True)

        Catch ex As Exception
            lblError.Text = "Error:imgbtnOrder_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub imgbtnVwBrch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnVwBrch.Click
        Try
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "_New", " window.open('../Knowledgebase/ConPkgBrochure.pdf');", True)
        Catch ex As Exception
            lblError.Text = "Error:imgbtnOrder_Click:" + ex.Message.ToString()
        End Try
    End Sub
	
	Protected Sub btnOrder_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOrder.Click
        Try
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Contarct_Order", "OpenOrderPage();", True)
        Catch ex As Exception
            lblError.Text = "Error:btnOrder_Click:" + ex.Message.ToString()
        End Try
    End Sub
End Class
