Imports system.Data
Imports System.Data.OleDb

Partial Class InteractiveServices_EnvModule1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ObjCrypto As New CryptoHelper()
        Try
            Session("MenuItem") = "ISERVICE"
            GetInventoryDetails()
            If Session("UserId") <> Nothing Then
                hdnUserId.Value = Session("UserId")
            End If
            hdnRepId.Value = ObjCrypto.Encrypt("ENVIRMO1")
        Catch ex As Exception
            lblError.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub
    Public Sub GetInventoryDetails()
        Dim objGetData As New InteractiveGetData.Selectdata
        Dim dsInventory As New DataSet
        Dim i As Integer = 0
        Dim var1, var2, var3, var4, var5, var6 As String
        Try
            dsInventory = objGetData.GetInventoryDetails("ENVIRMO1")
            If dsInventory.Tables(0).Rows.Count > 0 Then
                var1 = Convert.ToDateTime(dsInventory.Tables(0).Rows(0).Item("PUBdate").ToString()).Date()
                'Getting Study Number
                var2 = dsInventory.Tables(0).Rows(0).Item("PartID").ToString()
                'Getting CopyRight
                var3 = dsInventory.Tables(0).Rows(0).Item("copyright").ToString()

                lblPDate.Text = var1
                lblSNo.Text = var2
                lblCopyR.Text = var3
                For i = 0 To dsInventory.Tables(0).Rows.Count - 1
                    If dsInventory.Tables(0).Rows(i).Item("delFORMAT").ToString().ToUpper() = "ANNUAL SUBSCRIPTION" Then
                        var4 = "US$ " + FormatNumber(dsInventory.Tables(0).Rows(i).Item("Price").ToString(), 0)
                        lblS1.Text = "Call For Pricing" 'var4
                        'ElseIf dsInventory.Tables(0).Rows(i).Item("delFORMAT").ToString().ToUpper() = "EACH ADDITIONAL PREMIUM SUBSCRIPTION" Then
                        '    var5 = "US$ " + FormatNumber(dsInventory.Tables(0).Rows(i).Item("Price").ToString(), 0)
                        '    lblS2.Text = var5
                        'ElseIf dsInventory.Tables(0).Rows(i).Item("delFORMAT").ToString().ToUpper() = "ANNUAL READ ONLY SUBSCRIPTION" Then
                        '    var6 = "US$ " + FormatNumber(dsInventory.Tables(0).Rows(i).Item("PRICE").ToString(), 0)
                        '    lblS3.Text = var6

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
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "_New", " window.open('../ShoppingCart/Order.aspx?Id=" + ObjCrypto.Encrypt("ENVIRMO1") + "');", True)

        Catch ex As Exception
            lblError.Text = "Error:imgbtnOrder_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub imgbtnVwBrch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnVwBrch.Click
        Try

            Page.ClientScript.RegisterStartupScript(Me.GetType(), "_New", " window.open('../Email/EASTool_Final.pdf');", True)
        Catch ex As Exception
            lblError.Text = "Error:imgbtnVwBrch_Click:" + ex.Message.ToString()
        End Try
    End Sub
End Class
