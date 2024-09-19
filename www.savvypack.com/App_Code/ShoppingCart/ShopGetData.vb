Imports Microsoft.VisualBasic
Imports System.Web.SessionState
Imports System.Data
Imports System.Data.OleDb
Imports System

Public Class ShopGetData
    Public Class Selectdata
        Dim ShoppingConnection As String = System.Configuration.ConfigurationManager.AppSettings("ShoppingConnectionString")
#Region "Shopping Cart"

        Public Function GetRefNumber() As String
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim RefNumber As String = String.Empty
            Try
                StrSql = "SELECT REFNO.NEXTVAL AS REFNO FROM DUAL "
                Dts = odbUtil.FillDataSet(StrSql, ShoppingConnection)
                RefNumber = Dts.Tables(0).Rows(0).Item("REFNO").ToString()
                Return RefNumber
            Catch ex As Exception
                Throw New Exception("ShopGetData:GetRefNumber:" + ex.Message.ToString())
                Return RefNumber
            End Try
        End Function

        Public Function GetOrderForRef(ByVal RefNo As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT REFNUMBER, QTY, ITEMNUMBER, ITEMDESCRIPTION, NVL(DELIVERYFORMAT,'NA'), NVL(PAGE,'NA'), UNITCOST, SUBTOTAL, LINK, SEQUENCE FROM ORDERREVIEW WHERE REFNUMBER ='" + RefNo + "' AND ISSHOP<>'Y' ORDER BY ITEMNUMBER,SEQUENCE "
                StrSql = "SELECT REFNUMBER, QTY, ITEMNUMBER, ITEMDESCRIPTION, NVL(DELIVERYFORMAT,'NA'), NVL(PAGE,'NA'), UNITCOST, SUBTOTAL, LINK, SEQUENCE FROM ORDERREVIEW WHERE REFNUMBER ='" + RefNo + "' AND ISSHOP<>'Y' ORDER BY ITEMNUMBER,SEQUENCE "
                Dts = odbUtil.FillDataSet(StrSql, ShoppingConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("ShopGetData:GetOrderForRef:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetOrderDetails(ByVal PartId As String, ByVal RefNumber As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT INVENTORY.ID,  "
                StrSql = StrSql + "INVENTORY.PARTID, "
                StrSql = StrSql + "INVENTORY.PARTDES, "
                StrSql = StrSql + "INVENTORY.PRICE, "
                StrSql = StrSql + "INVENTORY.DELFORMAT, "
                StrSql = StrSql + "INVENTORY.PUBDATE, "
                StrSql = StrSql + "INVENTORY.COPYRIGHT, "
                StrSql = StrSql + "INVENTORY.FORMATID, "
                StrSql = StrSql + "INVENTORY.LINK, "
                StrSql = StrSql + "sum(NVL(ORDERREVIEW.QTY,0)) QTY "
                StrSql = StrSql + "FROM INVENTORY "
                StrSql = StrSql + "LEFT OUTER JOIN ORDERREVIEW "
                StrSql = StrSql + "ON ORDERREVIEW.ITEMNUMBER = INVENTORY.PARTID "
                StrSql = StrSql + "AND ORDERREVIEW.DELIVERYFORMAT = INVENTORY.DELFORMAT "
                StrSql = StrSql + "AND ORDERREVIEW.REFNUMBER = '" + RefNumber + "' "
                StrSql = StrSql + "WHERE PARTID = '" + PartId + "' "
                StrSql = StrSql + " GROUP BY INVENTORY.ID, "
                StrSql = StrSql + "INVENTORY.PARTID, "
                StrSql = StrSql + "INVENTORY.PARTDES, "
                StrSql = StrSql + "INVENTORY.PRICE, "
                StrSql = StrSql + "INVENTORY.DELFORMAT, "
                StrSql = StrSql + "INVENTORY.PUBDATE, "
                StrSql = StrSql + "INVENTORY.COPYRIGHT, "
                StrSql = StrSql + "INVENTORY.FORMATID, "
                StrSql = StrSql + "INVENTORY.LINK, "
                StrSql = StrSql + "INVENTORY.SEQUENCE "
                StrSql = StrSql + "ORDER BY INVENTORY.SEQUENCE "
                Dts = odbUtil.FillDataSet(StrSql, ShoppingConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("ShopGetData:GetOrderDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetInventory(ByVal PartId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = " SELECT ID,  "
                StrSql = StrSql + "PARTID,  "
                StrSql = StrSql + "PARTDES,  "
                StrSql = StrSql + "PRICE,  "
                StrSql = StrSql + "DELFORMAT,  "
                StrSql = StrSql + "PUBDATE,  "
                StrSql = StrSql + "COPYRIGHT,  "
                StrSql = StrSql + "SEQUENCE,  "
                StrSql = StrSql + "FORMATID,  "
                StrSql = StrSql + "LINK "
                StrSql = StrSql + "FROM INVENTORY "
                StrSql = StrSql + "WHERE PARTID = '" + PartId + "' "
                StrSql = StrSql + "ORDER BY SEQUENCE "

                Dts = odbUtil.FillDataSet(StrSql, ShoppingConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("ShopGetData:GetInventory:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetOrderReview(ByVal RefNo As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT REFNUMBER,  "
                StrSql = StrSql + "QTY, "
                StrSql = StrSql + "ITEMNUMBER, "
                StrSql = StrSql + "ITEMDESCRIPTION, "
                StrSql = StrSql + "NVL(DELIVERYFORMAT,'NA')DELIVERYFORMAT, "
                StrSql = StrSql + "NVL(PAGE,'NA')PAGE, "
                StrSql = StrSql + "UNITCOST, "
                StrSql = StrSql + "SUBTOTAL, "
                StrSql = StrSql + "LINK, "
                StrSql = StrSql + "SEQUENCE, "
                StrSql = StrSql + "SHIPTOID, "
                StrSql = StrSql + "SALESTAX, "
                StrSql = StrSql + "TOTAL, "
                StrSql = StrSql + "NVL(SHIPPINGCOST,0) SHIPPINGCOST, "
                StrSql = StrSql + " QTYSEQ "
                StrSql = StrSql + "FROM ORDERREVIEW "
                StrSql = StrSql + "WHERE REFNUMBER ='" + RefNo + "' "
                StrSql = StrSql + "AND ISSHOP<>'Y' "
                StrSql = StrSql + "ORDER BY ITEMNUMBER,SEQUENCE "


                Dts = odbUtil.FillDataSet(StrSql, ShoppingConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("ShopGetData:GetOrderReview:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetCustInfo(ByVal RefNo As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT REFNUMBER,  "
                StrSql = StrSql + "CARDNUMBER, "
                StrSql = StrSql + "CARDEXP, "
                StrSql = StrSql + "CARDTYPE, "
                StrSql = StrSql + "USERCONTRACTID "
                StrSql = StrSql + "REGDATE, "
                StrSql = StrSql + "PAYMENTTYPE, "
                StrSql = StrSql + "AUTH_CODE "
                StrSql = StrSql + "FROM CUSTOMERINFO "
                StrSql = StrSql + "WHERE REFNUMBER ='" + RefNo + "' "



                Dts = odbUtil.FillDataSet(StrSql, ShoppingConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("ShopGetData:GetCustInfo:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetOrderReviewGroup(ByVal RefNo As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT REFNUMBER,  "
                StrSql = StrSql + "sum(QTY) QTY, "
                StrSql = StrSql + "ITEMNUMBER, "
                StrSql = StrSql + "ITEMDESCRIPTION, "
                StrSql = StrSql + "NVL(DELIVERYFORMAT,'NA')DELIVERYFORMAT, "
                StrSql = StrSql + "NVL(PAGE,'NA')PAGE, "
                StrSql = StrSql + "UNITCOST, "
                StrSql = StrSql + "SUM(SUBTOTAL) SUBTOTAL, "               
                StrSql = StrSql + "LINK, "
                StrSql = StrSql + "SEQUENCE, "
                StrSql = StrSql + "sum(SALESTAX) SALESTAX, "
                StrSql = StrSql + "sum(TOTAL) TOTAL, "
                StrSql = StrSql + "sum(SHIPPINGCOST) SHIPPINGCOST "
                StrSql = StrSql + "FROM ORDERREVIEW "
                StrSql = StrSql + "WHERE REFNUMBER ='" + RefNo + "' "
                StrSql = StrSql + "AND ISSHOP<>'Y' "
                StrSql = StrSql + "GROUP BY  REFNUMBER,ITEMNUMBER,SEQUENCE,ITEMDESCRIPTION,DELIVERYFORMAT,PAGE,UNITCOST,LINK "
                StrSql = StrSql + "ORDER BY ITEMNUMBER,SEQUENCE "
                Dts = odbUtil.FillDataSet(StrSql, ShoppingConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("ShopGetData:GetOrderReview:" + ex.Message.ToString())
                Return Dts
            End Try

        End Function
        Public Function GetOrderReviewAShop(ByVal RefNo As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try

                StrSql = "SELECT REFNUMBER,  "
                StrSql = StrSql + "QTY, "
                StrSql = StrSql + "QTYSEQ, "
                StrSql = StrSql + "ITEMNUMBER, "
                StrSql = StrSql + "ITEMDESCRIPTION, "
                StrSql = StrSql + "NVL(DELIVERYFORMAT,'NA')DELIVERYFORMAT, "
                StrSql = StrSql + "NVL(PAGE,'NA')PAGE, "
                StrSql = StrSql + "UNITCOST, "
                StrSql = StrSql + "SUBTOTAL, "
                StrSql = StrSql + "LINK, "
                StrSql = StrSql + "SEQUENCE ,"
                StrSql = StrSql + "SALESTAX, "
                StrSql = StrSql + "TOTAL, "
                StrSql = StrSql + "BILLTOID,"
                StrSql = StrSql + "NVL(SHIPTOID,0) SHIPTOID,"
                StrSql = StrSql + "CARDHID "

                StrSql = StrSql + "FROM ORDERREVIEW "
                StrSql = StrSql + "WHERE REFNUMBER ='" + RefNo + "' "
                StrSql = StrSql + "ORDER BY ITEMNUMBER,SEQUENCE "

                Dts = odbUtil.FillDataSet(StrSql, ShoppingConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("ShopGetData:GetOrderReview:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetTax() As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT SALESTAXVALUE,CREDITCARDVALUE FROM SALESTAX"
                Dts = odbUtil.FillDataSet(StrSql, ShoppingConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("ShopGetData:GetTax:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function IsTaxable(ByVal ItemNo As String) As Boolean
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim flag As Boolean
            Dim StrSql As String = String.Empty
            Try
                flag = False
                StrSql = "SELECT ISTAXABLE FROM INVENTORY WHERE PARTID='" + ItemNo + "'"
                StrSql = StrSql + " UNION ALL "
                StrSql = StrSql + " SELECT ISTAXABLE FROM WEBCONFERENCES WHERE WEBCONFERENCEID='" + ItemNo + "'"
                Dts = odbUtil.FillDataSet(StrSql, ShoppingConnection)
                If Dts.Tables(0).Rows.Count > 0 Then
                    If Dts.Tables(0).Rows(0)("IsTaxable").ToString = "Y" Then
                        flag = True
                    End If
                End If
                Return flag
            Catch ex As Exception
                Throw New Exception("ShopGetData:IsTaxable:" + ex.Message.ToString())
                Return flag
            End Try
        End Function
        Public Function GetShippingInfo(ByVal ItemNo As String, ByVal SEQ As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT NVL(SHIPPINGCOST,0) SHIPPINGCOST  "
                StrSql = StrSql + "FROM INVENTORY "
                StrSql = StrSql + "WHERE PARTID='" + ItemNo + "' "
                StrSql = StrSql + "AND "
                StrSql = StrSql + "SEQUENCE=" + SEQ + " "
                Dts = odbUtil.FillDataSet(StrSql, ShoppingConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("ShopGetData:GetCustInfo:" + ex.Message.ToString())
            End Try
        End Function
#End Region

    End Class
    Public Class DownloadGetData
        Dim ShoppingConnection As String = System.Configuration.ConfigurationManager.AppSettings("ShoppingConnectionString")
        Public Function GetDwnldDetails(ByVal userId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  "
                StrSql = StrSql + "REPORTID, "
                StrSql = StrSql + "USERID, "
                StrSql = StrSql + "REPORTNAME, "
                StrSql = StrSql + "REPORTDETAILS "
                StrSql = StrSql + "FROM DWNLDDETAILS "
                StrSql = StrSql + "WHERE USERID=CASE WHEN " + userId + "=-1 THEN USERID ELSE " + userId + " END "
                StrSql = StrSql + " ORDER BY REPORTID"

                Dts = odbUtil.FillDataSet(StrSql, ShoppingConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("DownloadGetDat:GetDwnldDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
    End Class
End Class
