Imports Microsoft.VisualBasic
Imports ShopGetData
Imports ShopUpInsData
Imports System.Data
Public Class Calulate_ShopItem

    Public Sub Calculate(ByVal ItemNumber As String, ByVal Rpt As Repeater, ByVal RefNumber As String)
        Dim objGetData As New Selectdata()
        Dim dsInventory As New DataSet()
        Dim objUpIns As New UpdateInsert()
        Dim ItemQty As New Integer
        Dim Subtotal As New Integer


        Try
            dsInventory = objGetData.GetInventory(ItemNumber)

            For Each DataItem As RepeaterItem In Rpt.Items
                Dim txt As New TextBox()
                If DataItem.ItemType = ListItemType.Item Or DataItem.ItemType = ListItemType.AlternatingItem Then
                    txt = DataItem.FindControl(dsInventory.Tables(0).Rows(DataItem.ItemIndex).Item("FORMATID"))
                    If txt.Text <> "" Then
                        ItemQty = Convert.ToInt32(txt.Text)
                    Else
                        ItemQty = 0
                    End If

                    If ItemQty <> 0 Then
                        'Subtotal = ItemQty * Convert.ToInt32(dsInventory.Tables(0).Rows(DataItem.ItemIndex).Item("PRICE").ToString().Replace(",", ""))
                        Subtotal = Convert.ToInt32(dsInventory.Tables(0).Rows(DataItem.ItemIndex).Item("PRICE").ToString().Replace(",", ""))
                        objUpIns.UpdateInventory(ItemQty, Subtotal, RefNumber, dsInventory.Tables(0).Rows(DataItem.ItemIndex).Item("DELFORMAT").ToString(), ItemNumber)
                        objUpIns.InsertInventory(ItemQty, Subtotal, RefNumber, dsInventory.Tables(0).Rows(DataItem.ItemIndex).Item("DELFORMAT").ToString(), ItemNumber, dsInventory.Tables(0).Rows(DataItem.ItemIndex).Item("PARTDES").ToString(), dsInventory.Tables(0).Rows(DataItem.ItemIndex).Item("PRICE").ToString().Replace(",", ""), dsInventory.Tables(0).Rows(DataItem.ItemIndex).Item("SEQUENCE"), "N")
                    Else
                        'objUpIns.DeleteInventory(RefNumber, dsInventory.Tables(0).Rows(DataItem.ItemIndex).Item("DELFORMAT").ToString(), ItemNumber)
                        objUpIns.DeleteInventory(RefNumber, dsInventory.Tables(0).Rows(DataItem.ItemIndex).Item("SEQUENCE"), ItemNumber)
                    End If

                End If
            Next



        Catch ex As Exception
            Throw New Exception("Calulate_ShopItem:Calculate:" + ex.Message.ToString())
        End Try
    End Sub

    Public Sub Calculate_Web(ByVal WebCofId As String, ByVal RefNumber As String)
        Dim ItemQty As New Integer
        Dim Subtotal As New Integer
        Dim WebConf As New DataSet()
        Dim objGetData As New WebConf.Selectdata()
        Dim dsInventory As New DataSet()
        Dim objUpIns As New UpdateInsert()
        Try
            dsInventory = objGetData.GetWebConfDetailsById(WebCofId)
            ItemQty = 1
            Subtotal = ItemQty * Convert.ToInt32(dsInventory.Tables(0).Rows(0).Item("CONFCOSTVAL").ToString())

            If ItemQty <> 0 Then
                objUpIns.UpdateInventory(ItemQty, Subtotal, RefNumber, "", WebCofId)
                objUpIns.InsertInventory(ItemQty, Subtotal, RefNumber, "", WebCofId, dsInventory.Tables(0).Rows(0).Item("CONFTOPIC").ToString(), dsInventory.Tables(0).Rows(0).Item("CONFCOSTVAL").ToString(), "0", "Y")
            Else
                objUpIns.DeleteInventory(RefNumber, "", WebCofId)
            End If

        Catch ex As Exception

        End Try
    End Sub

End Class
