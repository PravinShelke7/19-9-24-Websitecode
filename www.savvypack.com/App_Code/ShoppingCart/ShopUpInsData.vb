Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Net.Mail
Imports System.Net
Public Class ShopUpInsData
    Public Class UpdateInsert
        Dim ShoppingConnection As String = System.Configuration.ConfigurationManager.AppSettings("ShoppingConnectionString")
        Dim LogConnection As String = System.Configuration.ConfigurationManager.AppSettings("LogConnectionString")
#Region "Shopping Cart"

        Public Sub UpdateInventory(ByVal ItemQty As String, ByVal Subtotal As String, ByVal RefNo As String, ByVal DelFormat As String, ByVal ItemNumber As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String
            Try
                StrSql = " UPDATE ORDERREVIEW "
                StrSql = StrSql + "SET "
                StrSql = StrSql + "QTY=" + ItemQty + ",  "
                StrSql = StrSql + "SUBTOTAL='" + Subtotal + "'"
                StrSql = StrSql + "WHERE REFNUMBER = '" + RefNo + "' "
                StrSql = StrSql + "AND DELIVERYFORMAT = '" + DelFormat + "'  "
                StrSql = StrSql + "AND ITEMNUMBER = '" + ItemNumber + "'  "
                odbUtil.UpIns(StrSql, ShoppingConnection)

            Catch ex As Exception
                Throw New Exception("ShopUpInsData:UpdateInventory:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub InsertInventory(ByVal ItemQty As String, ByVal Subtotal As String, ByVal RefNo As String, ByVal DelFormat As String, ByVal ItemNumber As String, ByVal ItemDes As String, ByVal ItemPrice As String, ByVal Seq As String, ByVal IsWeb As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String
            Dim objCrypto As New CryptoHelper()
            Dim i As Integer
            Try
                DeleteInventory(RefNo, Seq, ItemNumber)
                For i = 1 To ItemQty
                    StrSql = " INSERT INTO ORDERREVIEW "
                    StrSql = StrSql + "( "
                    StrSql = StrSql + " REFNUMBER,  "
                    StrSql = StrSql + " QTY,  "
                    StrSql = StrSql + " ITEMNUMBER,  "
                    StrSql = StrSql + " ITEMDESCRIPTION,  "
                    StrSql = StrSql + " DELIVERYFORMAT,  "
                    StrSql = StrSql + " UNITCOST, "
                    StrSql = StrSql + " SUBTOTAL,  "
                    StrSql = StrSql + " LINK, "
                    StrSql = StrSql + " SEQUENCE, "
                    StrSql = StrSql + " QTYSEQ "
                    StrSql = StrSql + ") "
                    StrSql = StrSql + "SELECT '" + RefNo + "', "
                    StrSql = StrSql + "1, "
                    StrSql = StrSql + "'" + ItemNumber + "', "
                    StrSql = StrSql + "'" + ItemDes + "', "
                    StrSql = StrSql + "'" + DelFormat + "', "
                    StrSql = StrSql + "'" + ItemPrice + "', "
                    StrSql = StrSql + "'" + Subtotal + "', "
                    If IsWeb = "Y" Then
                        StrSql = StrSql + "'', "
                    Else
                        StrSql = StrSql + "'<a href=""Order.aspx?ID=" + objCrypto.Encrypt(ItemNumber) + """>Change Order</a>', "
                    End If

                    StrSql = StrSql + "" + Seq + ", "
                    StrSql = StrSql + "" + i.ToString() + " "
                    StrSql = StrSql + "FROM DUAL "
                    StrSql = StrSql + "WHERE NOT EXISTS "
                    StrSql = StrSql + "( "
                    StrSql = StrSql + "SELECT 1 FROM  "
                    StrSql = StrSql + "ORDERREVIEW "
                    StrSql = StrSql + "WHERE ORDERREVIEW.REFNUMBER = '" + RefNo + "'"
                    StrSql = StrSql + "AND ORDERREVIEW.ITEMNUMBER='" + ItemNumber + "' "
                    StrSql = StrSql + "AND ORDERREVIEW.DELIVERYFORMAT='" & DelFormat + "' "
                    StrSql = StrSql + "AND ORDERREVIEW.QTYSEQ=" + i.ToString() + ""
                    StrSql = StrSql + ") "
                    odbUtil.UpIns(StrSql, ShoppingConnection)
                Next
            Catch ex As Exception
                Throw New Exception("ShopUpInsData:InsertInventory:" + ex.Message.ToString())
            End Try

        End Sub
        Public Sub InsertInventoryConf(ByVal ItemQty As String, ByVal Subtotal As String, ByVal RefNo As String, ByVal DelFormat As String, ByVal ItemNumber As String, ByVal ItemDes As String, ByVal ItemPrice As String, ByVal Seq As String, ByVal IsWeb As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String
            Dim objCrypto As New CryptoHelper()
            Try
                StrSql = " INSERT INTO ORDERREVIEW "
                StrSql = StrSql + "( "
                StrSql = StrSql + " REFNUMBER,  "
                StrSql = StrSql + " QTY,  "
                StrSql = StrSql + " ITEMNUMBER,  "
                StrSql = StrSql + " ITEMDESCRIPTION,  "
                StrSql = StrSql + " DELIVERYFORMAT,  "
                StrSql = StrSql + " UNITCOST, "
                StrSql = StrSql + " SUBTOTAL,  "
                StrSql = StrSql + " LINK, "
                StrSql = StrSql + " SEQUENCE "
                StrSql = StrSql + ") "
                StrSql = StrSql + "SELECT '" + RefNo + "', "
                StrSql = StrSql + "" + ItemQty + ", "
                StrSql = StrSql + "'" + Replace(ItemNumber.ToString(), "'", "''") + "', "
                StrSql = StrSql + "'" + Replace(ItemDes.ToString(), "'", "''") + "', "
                StrSql = StrSql + "'" + Replace(DelFormat.ToString(), "'", "''") + "', "
                StrSql = StrSql + "'" + ItemPrice + "', "
                StrSql = StrSql + "'" + Subtotal + "', "
                If IsWeb = "Y" Then
                    StrSql = StrSql + "'', "
                Else
                    StrSql = StrSql + "'<a href=""Order.aspx?ID=" + objCrypto.Encrypt(ItemNumber) + """>Change Order</a>', "
                End If

                StrSql = StrSql + "" + Seq + " "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "WHERE NOT EXISTS "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT 1 FROM  "
                StrSql = StrSql + "ORDERREVIEW "
                StrSql = StrSql + "WHERE ORDERREVIEW.REFNUMBER = '" + RefNo + "'"
                StrSql = StrSql + "AND ORDERREVIEW.ITEMNUMBER='" + ItemNumber + "' "
                StrSql = StrSql + ") "
                odbUtil.UpIns(StrSql, ShoppingConnection)

            Catch ex As Exception
                Throw New Exception("ShopUpInsData:InsertInventory:" + ex.Message.ToString())
            End Try

        End Sub
        Public Sub DeleteInventory(ByVal RefNo As String, ByVal Seq As String, ByVal ItemNumber As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String
            Try
                StrSql = "DELETE FROM  ORDERREVIEW "
                StrSql = StrSql + "WHERE ORDERREVIEW.REFNUMBER = '" + RefNo + "' "
                StrSql = StrSql + "AND ORDERREVIEW.ITEMNUMBER='" + ItemNumber + "' "
                StrSql = StrSql + "AND ORDERREVIEW.SEQUENCE=" + Seq + " "
                odbUtil.UpIns(StrSql, ShoppingConnection)

            Catch ex As Exception
                Throw New Exception("ShopUpInsData:DeleteInventory:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub DeleteAllInventory(ByVal RefNo As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String
            Try
                StrSql = "DELETE FROM  ORDERREVIEW "
                StrSql = StrSql + "WHERE ORDERREVIEW.REFNUMBER = '" + RefNo + "' "
                odbUtil.UpIns(StrSql, ShoppingConnection)

            Catch ex As Exception
                Throw New Exception("ShopUpInsData:DeleteAllInventory:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub InsertCustomerInfo(ByVal RefNo As String, ByVal CardNumber As String, ByVal CardExp As String, ByVal CardType As String, ByVal UserId As String, ByVal bilToAdd As String, ByVal shipToAdd As String, ByVal cardHAdd As String, ByVal payType As String, ByVal NameOnCard As String, ByVal Auth_Code As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String
            Dim objUGetData As New UsersGetData.Selectdata()
            Dim dsUser As New DataSet()
            Try
                dsUser = objUGetData.GetUserDetails(UserId)

                'TO UPDATE THE CUSTOMER TABLE 
                StrSql = "UPDATE CUSTOMERINFO  "
                StrSql = StrSql + "SET CARDNUMBER='" + CardNumber + "', CARDEXP='" + CardExp + "',CARDTYPE='" + CardType + "',PAYMENTTYPE= '" + payType + "',AUTH_CODE='" + Auth_Code + "' "
                StrSql = StrSql + " WHERE REFNUMBER ='" + RefNo + "' AND  USERCONTRACTID = " + dsUser.Tables(0).Rows(0).Item("USERCONTACTID").ToString() + " "
                odbUtil.UpIns(StrSql, ShoppingConnection)

                'TO INSERT THE CUSTOMER TABLE 
                StrSql = "INSERT INTO CUSTOMERINFO  "
                StrSql = StrSql + "(REFNUMBER, CARDNUMBER, CARDEXP, CARDTYPE, USERCONTRACTID,REGDATE,PAYMENTTYPE,AUTH_CODE) "
                StrSql = StrSql + "SELECT '" + RefNo + "','" + CardNumber + "','" + CardExp + "','" + CardType + "'," + dsUser.Tables(0).Rows(0).Item("USERCONTACTID").ToString() + ",SYSDATE,'" + payType + "','" + Auth_Code + "'  FROM DUAL "
                StrSql = StrSql + "WHERE NOT EXISTS ( "
                StrSql = StrSql + "SELECT 1 FROM CUSTOMERINFO WHERE REFNUMBER ='" + RefNo + "' AND  USERCONTRACTID = " + dsUser.Tables(0).Rows(0).Item("USERCONTACTID").ToString() + " )"
                odbUtil.UpIns(StrSql, ShoppingConnection)
                PreSalesMail(RefNo, UserId, bilToAdd, cardHAdd)
            Catch ex As Exception
                Throw New Exception("ShopUpInsData:InsertCustomerInfo:" + ex.Message.ToString())
            End Try

        End Sub
        Public Sub InsertCustomerInfoConf(ByVal RefNo As String, ByVal CardNumber As String, ByVal CardExp As String, ByVal CardType As String, ByVal UserId As String, ByVal bilToAdd As String, ByVal AttAdd() As String, ByVal cardHAdd As String, ByVal payType As String, ByVal NameOnCard As String, ByVal Auth_Code As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String
            Dim objUGetData As New UsersGetData.Selectdata()
            Dim dsUser As New DataSet()
            Try
                dsUser = objUGetData.GetUserDetails(UserId)

                StrSql = "INSERT INTO CUSTOMERINFO  "
                StrSql = StrSql + "(REFNUMBER, CARDNUMBER, CARDEXP, CARDTYPE, USERCONTRACTID,REGDATE,PAYMENTTYPE,AUTH_CODE) "
                StrSql = StrSql + "SELECT '" + RefNo + "','" + CardNumber + "','" + CardExp + "','" + CardType + "'," + dsUser.Tables(0).Rows(0).Item("USERCONTACTID").ToString() + ",SYSDATE,'" + payType + "','" + Auth_Code + "' FROM DUAL "
                StrSql = StrSql + "WHERE NOT EXISTS ( "
                StrSql = StrSql + "SELECT 1 FROM CUSTOMERINFO WHERE REFNUMBER ='" + RefNo + "' AND  USERCONTRACTID = " + dsUser.Tables(0).Rows(0).Item("USERCONTACTID").ToString() + " )"
                odbUtil.UpIns(StrSql, ShoppingConnection)

                'TO UPDATE THE CUSTOMER TABLE 

                StrSql = "UPDATE CUSTOMERINFO  "
                StrSql = StrSql + "SET CARDNUMBER='" + CardNumber + "', CARDEXP='" + CardExp + "',CARDTYPE='" + CardType + "',PAYMENTTYPE= '" + payType + "',AUTH_CODE=" + Auth_Code + " "
                StrSql = StrSql + " WHERE REFNUMBER ='" + RefNo + "' AND  USERCONTRACTID = " + dsUser.Tables(0).Rows(0).Item("USERCONTACTID").ToString() + " "
                odbUtil.UpIns(StrSql, ShoppingConnection)

                PreSalesMailConf(RefNo, UserId, bilToAdd, AttAdd, cardHAdd)
            Catch ex As Exception
                Throw New Exception("ShopUpInsData:InsertCustomerInfo:" + ex.Message.ToString())
            End Try

        End Sub
        Public Sub SaleOrder(ByVal RefNo As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String
            Dim objUGetData As New UsersGetData.Selectdata()
            Dim dsUser As New DataSet()
            Try

                StrSql = "UPDATE ORDERREVIEW SET ISSHOP ='Y',SERVERDATE=SYSDATE WHERE REFNUMBER='" + RefNo + "' "
                odbUtil.UpIns(StrSql, ShoppingConnection)

            Catch ex As Exception
                Throw New Exception("ShopUpInsData:InsertCustomerInfo:" + ex.Message.ToString())
            End Try

        End Sub
        Public Sub UpdateTax(ByVal RefNo As String, ByVal billToId As String, ByVal shipToId As String, ByVal CardHID As String, ByVal salesTax As String, ByVal Total As String, ByVal itemNo As String, ByVal seq As String, ByVal shippingCost As String, ByVal QtySeq As String)

            Dim odbUtil As New DBUtil()
            Dim StrSql As String
            Dim dsUser As New DataSet()
            Try

                StrSql = "UPDATE ORDERREVIEW  "
                StrSql = StrSql + "SET BILLTOID=" + billToId + " , "
                StrSql = StrSql + "SHIPTOID= " + shipToId + ", "
                StrSql = StrSql + "CARDHID=" + CardHID + ", "
                StrSql = StrSql + "SALESTAX=" + salesTax + ", "
                StrSql = StrSql + "TOTAL=" + Total + ", "
                StrSql = StrSql + "SHIPPINGCOST=" + shippingCost + " "
                StrSql = StrSql + "WHERE REFNUMBER= '" + RefNo + "'"
                StrSql = StrSql + " AND SEQUENCE=" + seq + ""
                StrSql = StrSql + " AND ITEMNUMBER='" + itemNo + "'"
                StrSql = StrSql + " AND QTYSEQ=" + QtySeq + ""
                odbUtil.UpIns(StrSql, ShoppingConnection)

            Catch ex As Exception
                Throw New Exception("ShopUpInsData:InsertCustomerInfo:" + ex.Message.ToString())
            End Try
        End Sub
        Public Sub UpdateTaxConf(ByVal RefNo As String, ByVal billToId As String, ByVal AttID() As String, ByVal CardHID As String, ByVal SubTotal As String, ByVal itemNo As String, ByVal seq As String, ByVal qty As String, ByVal ShippingCost As String, ByVal Total As String)

            Dim odbUtil As New DBUtil()
            Dim StrSql As String
            Dim dsUser As New DataSet()
            Try

                StrSql = "UPDATE ORDERREVIEW  "
                StrSql = StrSql + "SET BILLTOID=" + billToId + " , "
                StrSql = StrSql + "QTY= " + qty + ", "

                StrSql = StrSql + "CARDHID=" + CardHID + ", "
                StrSql = StrSql + "SALESTAX=0, "
                StrSql = StrSql + "SUBTOTAL=" + SubTotal + ", "
                StrSql = StrSql + "TOTAL=" + Total + ", "
                StrSql = StrSql + "SHIPPINGCOST= " + ShippingCost + " "
                StrSql = StrSql + "WHERE REFNUMBER= '" + RefNo + "'"
                StrSql = StrSql + " AND SEQUENCE=" + seq + ""
                StrSql = StrSql + " AND ITEMNUMBER='" + itemNo + "'"
                odbUtil.UpIns(StrSql, ShoppingConnection)



            Catch ex As Exception
                Throw New Exception("ShopUpInsData:InsertCustomerInfo:" + ex.Message.ToString())
            End Try
        End Sub
        Public Sub PreSalesMail(ByVal RefNumber As String, ByVal UserId As String, ByVal biltoAdd As String, ByVal cardHAdd As String)
            Dim objUGetData As New UsersGetData.Selectdata()
            Dim objGetShopData As New ShopGetData.Selectdata()
            Dim dsUser As New DataSet()
            Dim dsbilAdd As New DataSet()
            Dim dsShipAdd As New DataSet()
            Dim dsCardHAdd As New DataSet()
            Dim dsOrderReview As New DataSet()
            Dim StrSqlBody As String = String.Empty
            Dim _To As New MailAddressCollection()
            Dim _CC As New MailAddressCollection()
            Dim _BCC As New MailAddressCollection()
            Dim Item As MailAddress
            Dim Email As New EmailConfig()


            Try
                dsUser = objUGetData.GetUserDetails(UserId)
                dsbilAdd = objUGetData.GetBillToShipToUserDetailsByAddID(biltoAdd)
                dsOrderReview = objGetShopData.GetOrderReview(RefNumber)
                dsCardHAdd = objUGetData.GetBillToShipToUserDetailsByAddID(cardHAdd)

                '
                StrSqlBody = StrSqlBody + "<div style='font-family:Verdana;font-size:15px;margin-top:10px;margin-bottom:10px;font-weight:bold'>Possible Sale Details</div>"
                StrSqlBody = StrSqlBody + "<table style='font-family:Verdana;width:700px;font-size:10px;border-collapse:collapse' border='1' bordercolor='black' cellpadding='0' cellspacing='0'>  "
                StrSqlBody = StrSqlBody + "<tr style='background-color:#336699;height:20px;text-align:center;color:white'> "
                StrSqlBody = StrSqlBody + "<td><b>Reference  Number</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Prefix</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>First Name</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Last Name</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Job Title</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Company Name</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Email Address</b></td> "
                StrSqlBody = StrSqlBody + "</tr> "

                StrSqlBody = StrSqlBody + "<tr style='height:20px;text-align:center'> "
                StrSqlBody = StrSqlBody + "<td>" + RefNumber + "</b></td> "
                StrSqlBody = StrSqlBody + "<td>" + dsUser.Tables(0).Rows(0).Item("PREFIX") + "</b></td> "
                StrSqlBody = StrSqlBody + "<td>" + dsUser.Tables(0).Rows(0).Item("FIRSTNAME") + "</b></td> "
                StrSqlBody = StrSqlBody + "<td>" + dsUser.Tables(0).Rows(0).Item("LASTNAME") + "</b></td> "
                StrSqlBody = StrSqlBody + "<td>" + dsUser.Tables(0).Rows(0).Item("JOBTITLE") + "</b></td> "
                StrSqlBody = StrSqlBody + "<td>" + dsUser.Tables(0).Rows(0).Item("COMPANYNAME") + "</b></td> "
                StrSqlBody = StrSqlBody + "<td>" + dsUser.Tables(0).Rows(0).Item("EMAILADDRESS") + "</b></td> "
                StrSqlBody = StrSqlBody + "</tr> "
                StrSqlBody = StrSqlBody + "</table> "

                Dim i As Integer
                For i = 0 To dsOrderReview.Tables(0).Rows.Count - 1

                    If dsOrderReview.Tables(0).Rows(i)("SHIPTOID").ToString().Trim <> "" Then
                        dsShipAdd = objUGetData.GetBillToShipToUserDetailsByAddID(dsOrderReview.Tables(0).Rows(i)("SHIPTOID").ToString())

                        '********************************Ship To Address******************************************
                        StrSqlBody = StrSqlBody + "<div style='font-family:Verdana;font-size:12px;margin-top:10px;margin-bottom:5px;font-weight:bold'>Item: " + dsOrderReview.Tables(0).Rows(i)("ITEMDESCRIPTION").ToString() + "</div>"
                        StrSqlBody = StrSqlBody + "<div style='font-family:Verdana;font-size:12px;margin-top:0px;margin-bottom:5px;font-weight:bold'>Delivery Format: " + dsOrderReview.Tables(0).Rows(i)("DELIVERYFORMAT").ToString() + "</div>"

                        StrSqlBody = StrSqlBody + "<div style='font-family:Verdana;font-size:12px;margin-top:0px;margin-bottom:5px;font-weight:bold'>Ship To Address</div>"
                        StrSqlBody = StrSqlBody + "<table style='font-family:Verdana;width:700px;font-size:10px;border-collapse:collapse' border='1' bordercolor='black' cellpadding='0' cellspacing='0'>  "
                        StrSqlBody = StrSqlBody + "<tr style='background-color:#336699;height:20px;text-align:center;color:white'> "
                        StrSqlBody = StrSqlBody + "<td><b>Name</b></td> "
                        StrSqlBody = StrSqlBody + "<td><b>Email Address</b></td> "
                        StrSqlBody = StrSqlBody + "<td><b>Phone Number</b></td> "
                        StrSqlBody = StrSqlBody + "<td><b>Fax Number</b></td> "
                        StrSqlBody = StrSqlBody + "<td><b>Address</b></td> "
                        StrSqlBody = StrSqlBody + "<td><b>City</b></td> "
                        StrSqlBody = StrSqlBody + "<td><b>State</b></td> "
                        StrSqlBody = StrSqlBody + "<td><b>Zip Code</b></td> "
                        StrSqlBody = StrSqlBody + "<td><b>Country</b></td> "
                        StrSqlBody = StrSqlBody + "</tr> "
                        StrSqlBody = StrSqlBody + "<tr style='height:20px;text-align:center'> "
                        StrSqlBody = StrSqlBody + "<td>" + dsShipAdd.Tables(0).Rows(0).Item("FULLNAME") + "</b></td> "
                        StrSqlBody = StrSqlBody + "<td>" + dsShipAdd.Tables(0).Rows(0).Item("EMAILADDRESS") + "</b></td> "

                        StrSqlBody = StrSqlBody + "<td>" + dsShipAdd.Tables(0).Rows(0).Item("PHONENUMBER") + "</b></td> "
                        StrSqlBody = StrSqlBody + "<td>" + dsShipAdd.Tables(0).Rows(0).Item("FAXNUMBER") + "</b></td> "
                        StrSqlBody = StrSqlBody + "<td>" + dsShipAdd.Tables(0).Rows(0).Item("STREETADDRESS1") + " " & dsUser.Tables(0).Rows(0).Item("STREETADDRESS2") + "</b></td> "
                        StrSqlBody = StrSqlBody + "<td>" + dsShipAdd.Tables(0).Rows(0).Item("CITY") + "</b></td> "
                        StrSqlBody = StrSqlBody + "<td>" + dsShipAdd.Tables(0).Rows(0).Item("STATE") + "</b></td> "
                        StrSqlBody = StrSqlBody + "<td>" + dsShipAdd.Tables(0).Rows(0).Item("ZIPCODE") + "</b></td> "
                        StrSqlBody = StrSqlBody + "<td>" + dsShipAdd.Tables(0).Rows(0).Item("COUNTRYDES") + "</b></td> "
                        StrSqlBody = StrSqlBody + "</tr> "
                        StrSqlBody = StrSqlBody + "</table> "
                        '**************************************************************************
                    End If
                Next


                '********************************Bill To Address******************************************
                StrSqlBody = StrSqlBody + "<div style='font-family:Verdana;font-size:12px;margin-top:10px;margin-bottom:5px;font-weight:bold'>Bill To Address</div>"
                StrSqlBody = StrSqlBody + "<table style='font-family:Verdana;width:700px;font-size:10px;border-collapse:collapse' border='1' bordercolor='black' cellpadding='0' cellspacing='0'>  "
                StrSqlBody = StrSqlBody + "<tr style='background-color:#336699;height:20px;text-align:center;color:white'> "
                StrSqlBody = StrSqlBody + "<td><b>Name</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Email Address</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Phone Number</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Fax Number</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Address</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>City</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>State</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Zip Code</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Country</b></td> "
                StrSqlBody = StrSqlBody + "</tr> "
                StrSqlBody = StrSqlBody + "<tr style='height:20px;text-align:center'> "
                StrSqlBody = StrSqlBody + "<td>" + dsbilAdd.Tables(0).Rows(0).Item("FULLNAME") + "</b></td> "
                StrSqlBody = StrSqlBody + "<td>" + dsbilAdd.Tables(0).Rows(0).Item("EMAILADDRESS") + "</b></td> "
                StrSqlBody = StrSqlBody + "<td>" + dsbilAdd.Tables(0).Rows(0).Item("PHONENUMBER") + "</b></td> "
                StrSqlBody = StrSqlBody + "<td>" + dsbilAdd.Tables(0).Rows(0).Item("FAXNUMBER") + "</b></td> "
                StrSqlBody = StrSqlBody + "<td>" + dsbilAdd.Tables(0).Rows(0).Item("STREETADDRESS1") + " " & dsbilAdd.Tables(0).Rows(0).Item("STREETADDRESS2") + "</b></td> "
                StrSqlBody = StrSqlBody + "<td>" + dsbilAdd.Tables(0).Rows(0).Item("CITY") + "</b></td> "
                StrSqlBody = StrSqlBody + "<td>" + dsbilAdd.Tables(0).Rows(0).Item("STATE") + "</b></td> "
                StrSqlBody = StrSqlBody + "<td>" + dsbilAdd.Tables(0).Rows(0).Item("ZIPCODE") + "</b></td> "
                StrSqlBody = StrSqlBody + "<td>" + dsbilAdd.Tables(0).Rows(0).Item("COUNTRYDES") + "</b></td> "
                StrSqlBody = StrSqlBody + "</tr> "
                StrSqlBody = StrSqlBody + "</table> "
                '**************************************************************************

                '********************************Card Holder Address******************************************
                If cardHAdd <> "0" Then
                    StrSqlBody = StrSqlBody + "<div style='font-family:Verdana;font-size:12px;margin-top:10px;margin-bottom:5px;font-weight:bold'>Card Holder Address</div>"
                    StrSqlBody = StrSqlBody + "<table style='font-family:Verdana;width:700px;font-size:10px;border-collapse:collapse' border='1' bordercolor='black' cellpadding='0' cellspacing='0'>  "
                    StrSqlBody = StrSqlBody + "<tr style='background-color:#336699;height:20px;text-align:center;color:white'> "
                    StrSqlBody = StrSqlBody + "<td><b>Name</b></td> "
                    StrSqlBody = StrSqlBody + "<td><b>Email Address</b></td> "
                    StrSqlBody = StrSqlBody + "<td><b>Phone Number</b></td> "
                    StrSqlBody = StrSqlBody + "<td><b>Fax Number</b></td> "
                    StrSqlBody = StrSqlBody + "<td><b>Address</b></td> "
                    StrSqlBody = StrSqlBody + "<td><b>City</b></td> "
                    StrSqlBody = StrSqlBody + "<td><b>State</b></td> "
                    StrSqlBody = StrSqlBody + "<td><b>Zip Code</b></td> "
                    StrSqlBody = StrSqlBody + "<td><b>Country</b></td> "
                    StrSqlBody = StrSqlBody + "</tr> "
                    StrSqlBody = StrSqlBody + "<tr style='height:20px;text-align:center'> "
                    StrSqlBody = StrSqlBody + "<td>" + dsCardHAdd.Tables(0).Rows(0).Item("FULLNAME") + "</b></td> "
                    StrSqlBody = StrSqlBody + "<td>" + dsCardHAdd.Tables(0).Rows(0).Item("EMAILADDRESS") + "</b></td> "

                    StrSqlBody = StrSqlBody + "<td>" + dsCardHAdd.Tables(0).Rows(0).Item("PHONENUMBER") + "</b></td> "
                    StrSqlBody = StrSqlBody + "<td>" + dsCardHAdd.Tables(0).Rows(0).Item("FAXNUMBER") + "</b></td> "
                    StrSqlBody = StrSqlBody + "<td>" + dsCardHAdd.Tables(0).Rows(0).Item("STREETADDRESS1") + " " & dsUser.Tables(0).Rows(0).Item("STREETADDRESS2") + "</b></td> "
                    StrSqlBody = StrSqlBody + "<td>" + dsCardHAdd.Tables(0).Rows(0).Item("CITY") + "</b></td> "
                    StrSqlBody = StrSqlBody + "<td>" + dsCardHAdd.Tables(0).Rows(0).Item("STATE") + "</b></td> "
                    StrSqlBody = StrSqlBody + "<td>" + dsCardHAdd.Tables(0).Rows(0).Item("ZIPCODE") + "</b></td> "
                    StrSqlBody = StrSqlBody + "<td>" + dsCardHAdd.Tables(0).Rows(0).Item("COUNTRYDES") + "</b></td> "
                    StrSqlBody = StrSqlBody + "</tr> "
                    StrSqlBody = StrSqlBody + "</table> "
                End If

                '**************************************************************************



                Dim objGetData As New UsersGetData.Selectdata
                Dim ds As New DataSet               
                ds = objGetData.GetAlliedMemberMail("PSALES")

                If ds.Tables(0).Rows.Count > 0 Then
                    Dim _From As New MailAddress(ds.Tables(0).Rows(0).Item("FROMADD").ToString(), ds.Tables(0).Rows(0).Item("FROMNAME").ToString())
                    Dim _Subject As String = ds.Tables(0).Rows(0).Item("SUBJECT").ToString()
                    'To's
                    Item = New MailAddress(ds.Tables(0).Rows(0).Item("TOADD"), ds.Tables(0).Rows(0).Item("TONAME"))
                    _To.Add(Item)

                    'BCC's

                    For i = 1 To 10
                        ' BCC() 's
                        If ds.Tables(0).Rows(0).Item("BCC" + i.ToString()).ToString() <> Nothing Then
                            Item = New MailAddress(ds.Tables(0).Rows(0).Item("BCC" + i.ToString()).ToString(), ds.Tables(0).Rows(0).Item("BCC" + i.ToString() + "NAME").ToString())
                            _BCC.Add(Item)
                        End If
                        If ds.Tables(0).Rows(0).Item("CC" + i.ToString()).ToString() <> Nothing Then
                            Item = New MailAddress(ds.Tables(0).Rows(0).Item("CC" + i.ToString()).ToString(), ds.Tables(0).Rows(0).Item("CC" + i.ToString() + "NAME").ToString())
                            _CC.Add(Item)
                        End If

                    Next

                    Email.SendMail(_From, _To, _CC, _BCC, StrSqlBody, _Subject)
                End If


            Catch ex As Exception
                Throw New Exception("ShopUpInsData:PreSaleMail:" + ex.Message.ToString())
            End Try
        End Sub
        Public Sub PreSalesMailConf(ByVal RefNumber As String, ByVal UserId As String, ByVal biltoAdd As String, ByVal AttAdd() As String, ByVal cardHAdd As String)
            Dim objUGetData As New UsersGetData.Selectdata()
            Dim dsUser As New DataSet()
            Dim dsEmailConfig As New DataSet()
            Dim dsbilAdd As New DataSet()
            Dim dsAttAdd As New DataSet()
            Dim dsCardHAdd As New DataSet()
            Dim StrSqlBody As String = String.Empty
            Dim _To As New MailAddressCollection()
            ' Dim _From As New MailAddress("sales@savvypack.com", "Sales")
            Dim _CC As New MailAddressCollection()
            Dim _BCC As New MailAddressCollection()
            Dim Item As MailAddress
            Dim Email As New EmailConfig()
            'Dim _Subject As String = "Possible Sales Details"

            Try
                dsUser = objUGetData.GetUserDetails(UserId)
                dsbilAdd = objUGetData.GetBillToShipToUserDetailsByAddID(biltoAdd)

                dsCardHAdd = objUGetData.GetBillToShipToUserDetailsByAddID(cardHAdd)

                StrSqlBody = StrSqlBody + "<div style='font-family:Verdana;font-size:15px;margin-top:10px;margin-bottom:10px;font-weight:bold'>Possible Sale Details</div>"
                StrSqlBody = StrSqlBody + "<table style='font-family:Verdana;width:700px;font-size:10px;border-collapse:collapse' border='1' bordercolor='black' cellpadding='0' cellspacing='0'>  "
                StrSqlBody = StrSqlBody + "<tr style='background-color:#336699;height:20px;text-align:center;color:white'> "
                StrSqlBody = StrSqlBody + "<td><b>Reference Number</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Prefix</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>First Name</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Last Name</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Job Title</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Company Name</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Email Address</b></td> "
                StrSqlBody = StrSqlBody + "</tr> "
                StrSqlBody = StrSqlBody + "<tr style='height:20px;text-align:center'> "
                StrSqlBody = StrSqlBody + "<td>" + RefNumber + "</b></td> "
                StrSqlBody = StrSqlBody + "<td>" + dsUser.Tables(0).Rows(0).Item("PREFIX") + "</b></td> "
                StrSqlBody = StrSqlBody + "<td>" + dsUser.Tables(0).Rows(0).Item("FIRSTNAME") + "</b></td> "
                StrSqlBody = StrSqlBody + "<td>" + dsUser.Tables(0).Rows(0).Item("LASTNAME") + "</b></td> "
                StrSqlBody = StrSqlBody + "<td>" + dsUser.Tables(0).Rows(0).Item("JOBTITLE") + "</b></td> "
                StrSqlBody = StrSqlBody + "<td>" + dsUser.Tables(0).Rows(0).Item("COMPANYNAME") + "</b></td> "
                StrSqlBody = StrSqlBody + "<td>" + dsUser.Tables(0).Rows(0).Item("EMAILADDRESS") + "</b></td> "
                StrSqlBody = StrSqlBody + "</tr> "
                StrSqlBody = StrSqlBody + "</table> "



                '********************************Attendee Address******************************************
                Dim i As Integer
                For i = 0 To AttAdd.Length - 1
                    If AttAdd(i) <> "0" And AttAdd(i) <> Nothing Then
                        dsAttAdd = objUGetData.GetBillToShipToUserDetailsByAddID(AttAdd(i))
                        StrSqlBody = StrSqlBody + "<div style='font-family:Verdana;font-size:12px;margin-top:10px;margin-bottom:5px;font-weight:bold'>Attendee " + (i + 1).ToString() + "</div>"
                        StrSqlBody = StrSqlBody + "<table style='font-family:Verdana;width:700px;font-size:10px;border-collapse:collapse' border='1' bordercolor='black' cellpadding='0' cellspacing='0'>  "
                        StrSqlBody = StrSqlBody + "<tr style='background-color:#336699;height:20px;text-align:center;color:white'> "
                        StrSqlBody = StrSqlBody + "<td><b>Name</b></td> "
                        StrSqlBody = StrSqlBody + "<td><b>Email Address</b></td> "
                        StrSqlBody = StrSqlBody + "<td><b>Phone Number</b></td> "
                        StrSqlBody = StrSqlBody + "<td><b>Fax Number</b></td> "
                        StrSqlBody = StrSqlBody + "<td><b>Address</b></td> "
                        StrSqlBody = StrSqlBody + "<td><b>City</b></td> "
                        StrSqlBody = StrSqlBody + "<td><b>State</b></td> "
                        StrSqlBody = StrSqlBody + "<td><b>Zip Code</b></td> "
                        StrSqlBody = StrSqlBody + "<td><b>Country</b></td> "
                        StrSqlBody = StrSqlBody + "</tr> "
                        StrSqlBody = StrSqlBody + "<tr style='height:20px;text-align:center'> "
                        StrSqlBody = StrSqlBody + "<td>" + dsAttAdd.Tables(0).Rows(0).Item("FULLNAME") + "</b></td> "
                        StrSqlBody = StrSqlBody + "<td>" + dsAttAdd.Tables(0).Rows(0).Item("EMAILADDRESS") + "</b></td> "

                        StrSqlBody = StrSqlBody + "<td>" + dsAttAdd.Tables(0).Rows(0).Item("PHONENUMBER") + "</b></td> "
                        StrSqlBody = StrSqlBody + "<td>" + dsAttAdd.Tables(0).Rows(0).Item("FAXNUMBER") + "</b></td> "
                        StrSqlBody = StrSqlBody + "<td>" + dsAttAdd.Tables(0).Rows(0).Item("STREETADDRESS1") + " " & dsUser.Tables(0).Rows(0).Item("STREETADDRESS2") + "</b></td> "
                        StrSqlBody = StrSqlBody + "<td>" + dsAttAdd.Tables(0).Rows(0).Item("CITY") + "</b></td> "
                        StrSqlBody = StrSqlBody + "<td>" + dsAttAdd.Tables(0).Rows(0).Item("STATE") + "</b></td> "
                        StrSqlBody = StrSqlBody + "<td>" + dsAttAdd.Tables(0).Rows(0).Item("ZIPCODE") + "</b></td> "
                        StrSqlBody = StrSqlBody + "<td>" + dsAttAdd.Tables(0).Rows(0).Item("COUNTRYDES") + "</b></td> "
                        StrSqlBody = StrSqlBody + "</tr> "
                        StrSqlBody = StrSqlBody + "</table> "
                    End If
                Next

                '**************************************************************************



                '********************************Bill To Address******************************************
                StrSqlBody = StrSqlBody + "<div style='font-family:Verdana;font-size:12px;margin-top:10px;margin-bottom:5px;font-weight:bold'>Bill To Address</div>"
                StrSqlBody = StrSqlBody + "<table style='font-family:Verdana;width:700px;font-size:10px;border-collapse:collapse' border='1' bordercolor='black' cellpadding='0' cellspacing='0'>  "
                StrSqlBody = StrSqlBody + "<tr style='background-color:#336699;height:20px;text-align:center;color:white'> "
                StrSqlBody = StrSqlBody + "<td><b>Name</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Email Address</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Phone Number</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Fax Number</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Address</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>City</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>State</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Zip Code</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Country</b></td> "
                StrSqlBody = StrSqlBody + "</tr> "
                StrSqlBody = StrSqlBody + "<tr style='height:20px;text-align:center'> "
                StrSqlBody = StrSqlBody + "<td>" + dsbilAdd.Tables(0).Rows(0).Item("FULLNAME") + "</b></td> "
                StrSqlBody = StrSqlBody + "<td>" + dsbilAdd.Tables(0).Rows(0).Item("EMAILADDRESS") + "</b></td> "
                StrSqlBody = StrSqlBody + "<td>" + dsbilAdd.Tables(0).Rows(0).Item("PHONENUMBER") + "</b></td> "
                StrSqlBody = StrSqlBody + "<td>" + dsbilAdd.Tables(0).Rows(0).Item("FAXNUMBER") + "</b></td> "
                StrSqlBody = StrSqlBody + "<td>" + dsbilAdd.Tables(0).Rows(0).Item("STREETADDRESS1") + " " & dsbilAdd.Tables(0).Rows(0).Item("STREETADDRESS2") + "</b></td> "
                StrSqlBody = StrSqlBody + "<td>" + dsbilAdd.Tables(0).Rows(0).Item("CITY") + "</b></td> "
                StrSqlBody = StrSqlBody + "<td>" + dsbilAdd.Tables(0).Rows(0).Item("STATE") + "</b></td> "
                StrSqlBody = StrSqlBody + "<td>" + dsbilAdd.Tables(0).Rows(0).Item("ZIPCODE") + "</b></td> "
                StrSqlBody = StrSqlBody + "<td>" + dsbilAdd.Tables(0).Rows(0).Item("COUNTRYDES") + "</b></td> "
                StrSqlBody = StrSqlBody + "</tr> "
                StrSqlBody = StrSqlBody + "</table> "
                '**************************************************************************

                '********************************Card Holder Address******************************************
                If cardHAdd <> "0" Then
                    StrSqlBody = StrSqlBody + "<div style='font-family:Verdana;font-size:12px;margin-top:10px;margin-bottom:5px;font-weight:bold'>Card Holder Address</div>"
                    StrSqlBody = StrSqlBody + "<table style='font-family:Verdana;width:700px;font-size:10px;border-collapse:collapse' border='1' bordercolor='black' cellpadding='0' cellspacing='0'>  "
                    StrSqlBody = StrSqlBody + "<tr style='background-color:#336699;height:20px;text-align:center;color:white'> "
                    StrSqlBody = StrSqlBody + "<td><b>Name</b></td> "
                    StrSqlBody = StrSqlBody + "<td><b>Email Address</b></td> "
                    StrSqlBody = StrSqlBody + "<td><b>Phone Number</b></td> "
                    StrSqlBody = StrSqlBody + "<td><b>Fax Number</b></td> "
                    StrSqlBody = StrSqlBody + "<td><b>Address</b></td> "
                    StrSqlBody = StrSqlBody + "<td><b>City</b></td> "
                    StrSqlBody = StrSqlBody + "<td><b>State</b></td> "
                    StrSqlBody = StrSqlBody + "<td><b>Zip Code</b></td> "
                    StrSqlBody = StrSqlBody + "<td><b>Country</b></td> "
                    StrSqlBody = StrSqlBody + "</tr> "
                    StrSqlBody = StrSqlBody + "<tr style='height:20px;text-align:center'> "
                    StrSqlBody = StrSqlBody + "<td>" + dsCardHAdd.Tables(0).Rows(0).Item("FULLNAME") + "</b></td> "
                    StrSqlBody = StrSqlBody + "<td>" + dsCardHAdd.Tables(0).Rows(0).Item("EMAILADDRESS") + "</b></td> "

                    StrSqlBody = StrSqlBody + "<td>" + dsCardHAdd.Tables(0).Rows(0).Item("PHONENUMBER") + "</b></td> "
                    StrSqlBody = StrSqlBody + "<td>" + dsCardHAdd.Tables(0).Rows(0).Item("FAXNUMBER") + "</b></td> "
                    StrSqlBody = StrSqlBody + "<td>" + dsCardHAdd.Tables(0).Rows(0).Item("STREETADDRESS1") + " " & dsUser.Tables(0).Rows(0).Item("STREETADDRESS2") + "</b></td> "
                    StrSqlBody = StrSqlBody + "<td>" + dsCardHAdd.Tables(0).Rows(0).Item("CITY") + "</b></td> "
                    StrSqlBody = StrSqlBody + "<td>" + dsCardHAdd.Tables(0).Rows(0).Item("STATE") + "</b></td> "
                    StrSqlBody = StrSqlBody + "<td>" + dsCardHAdd.Tables(0).Rows(0).Item("ZIPCODE") + "</b></td> "
                    StrSqlBody = StrSqlBody + "<td>" + dsCardHAdd.Tables(0).Rows(0).Item("COUNTRYDES") + "</b></td> "
                    StrSqlBody = StrSqlBody + "</tr> "
                    StrSqlBody = StrSqlBody + "</table> "
                End If

                '**************************************************************************

                Dim objGetData As New UsersGetData.Selectdata
                Dim ds As New DataSet

                ds = objGetData.GetAlliedMemberMail("PSALES")

                If ds.Tables(0).Rows.Count > 0 Then
                    Dim _From As New MailAddress(ds.Tables(0).Rows(0).Item("FROMADD").ToString(), ds.Tables(0).Rows(0).Item("FROMNAME").ToString())
                    Dim _Subject As String = ds.Tables(0).Rows(0).Item("SUBJECT").ToString()
                    'To's
                    Item = New MailAddress(ds.Tables(0).Rows(0).Item("TOADD"), ds.Tables(0).Rows(0).Item("TONAME"))
                    _To.Add(Item)

                    'BCC's

                    For i = 1 To 10
                        ' BCC() 's
                        If ds.Tables(0).Rows(0).Item("BCC" + i.ToString()).ToString() <> Nothing Then
                            Item = New MailAddress(ds.Tables(0).Rows(0).Item("BCC" + i.ToString()).ToString(), ds.Tables(0).Rows(0).Item("BCC" + i.ToString() + "NAME").ToString())
                            _BCC.Add(Item)
                        End If
                        If ds.Tables(0).Rows(0).Item("CC" + i.ToString()).ToString() <> Nothing Then
                            Item = New MailAddress(ds.Tables(0).Rows(0).Item("CC" + i.ToString()).ToString(), ds.Tables(0).Rows(0).Item("CC" + i.ToString() + "NAME").ToString())
                            _CC.Add(Item)
                        End If

                    Next

                    Email.SendMail(_From, _To, _CC, _BCC, StrSqlBody, _Subject)
                End If


            Catch ex As Exception
                Throw New Exception("ShopUpInsData:PreSaleMail:" + ex.Message.ToString())
            End Try
        End Sub
        Public Sub SalesMail(ByVal RefNumber As String, ByVal UserId As String)
            Dim objUGetData As New UsersGetData.Selectdata()
            Dim objGetData As New ShopGetData.Selectdata()
            Dim ds As New DataSet()

            Dim StrSqlBody As String = String.Empty
            Dim _To As New MailAddressCollection()

            Dim _CC As New MailAddressCollection()
            Dim _BCC As New MailAddressCollection()
            Dim Item As MailAddress
            Dim Email As New EmailConfig()

            Dim i As New Integer

            Try
                ds = objGetData.GetOrderReviewGroup(RefNumber)
                'dsEmailConfig = objUGetData.GetEmailConfigDetails()

                '
                StrSqlBody = StrSqlBody + "<div style='font-family:Verdana;font-size:12px;margin-top:10px;margin-bottom:10px;font-weight:bold'>Sale Details</div>"
                StrSqlBody = StrSqlBody + "<table style='font-family:Verdana;width:700px;font-size:11px;border-collapse:collapse' border='1' bordercolor='black' cellpadding='0' cellspacing='0'>  "
                StrSqlBody = StrSqlBody + "<tr style='background-color:#336699;height:20px;text-align:center;color:white'> "
                StrSqlBody = StrSqlBody + "<td><b>Reference  Number</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Quantity</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Item</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Item Description</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Format</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Page</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Unit</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>SubTotal</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Sales Tax</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Shipping & Handling Fees</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Total</b></td> "
                StrSqlBody = StrSqlBody + "</tr> "
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    StrSqlBody = StrSqlBody + "<tr style='height:20px;text-align:center'> "
                    StrSqlBody = StrSqlBody + "<td><b>" + RefNumber + "</b></td> "
                    StrSqlBody = StrSqlBody & "<td><b>" + ds.Tables(0).Rows(i).Item("QTY").ToString() + "</b></td> "
                    StrSqlBody = StrSqlBody & "<td><b>" + ds.Tables(0).Rows(i).Item("ITEMNUMBER").ToString() + "</b></td> "
                    StrSqlBody = StrSqlBody & "<td><b>" + ds.Tables(0).Rows(i).Item("ITEMDESCRIPTION").ToString() + "</b></td> "
                    StrSqlBody = StrSqlBody & "<td><b>" + ds.Tables(0).Rows(i).Item("DELIVERYFORMAT").ToString() + "</b></td> "
                    StrSqlBody = StrSqlBody & "<td><b>" + ds.Tables(0).Rows(i).Item("PAGE").ToString() + "</b></td> "
                    StrSqlBody = StrSqlBody & "<td><b>" + FormatCurrency(ds.Tables(0).Rows(i).Item("UNITCOST").ToString(), 0) + "</b></td> "
                    StrSqlBody = StrSqlBody & "<td><b>" + FormatCurrency(ds.Tables(0).Rows(i).Item("SUBTOTAL").ToString(), 0) + "</b></td> "
                    StrSqlBody = StrSqlBody & "<td><b>" + FormatCurrency(ds.Tables(0).Rows(i).Item("SALESTAX").ToString(), 2) + "</b></td> "
                    StrSqlBody = StrSqlBody & "<td><b>" + FormatCurrency(ds.Tables(0).Rows(i).Item("SHIPPINGCOST").ToString(), 2) + "</b></td> "
                    StrSqlBody = StrSqlBody & "<td><b>" + FormatCurrency(ds.Tables(0).Rows(i).Item("TOTAL").ToString(), 2) + "</b></td> "
                    StrSqlBody = StrSqlBody + "</tr> "
                Next

                StrSqlBody = StrSqlBody & "</table> "

                Dim objGetMailData As New UsersGetData.Selectdata
                Dim dsNew As New DataSet
                dsNew = objGetMailData.GetAlliedMemberMail("SALES")

                If dsNew.Tables(0).Rows.Count > 0 Then
                    Dim _From As New MailAddress(dsNew.Tables(0).Rows(0).Item("FROMADD").ToString(), dsNew.Tables(0).Rows(0).Item("FROMNAME").ToString())
                    Dim _Subject As String = dsNew.Tables(0).Rows(0).Item("SUBJECT").ToString()
                    'To's
                    Item = New MailAddress(dsNew.Tables(0).Rows(0).Item("TOADD"), dsNew.Tables(0).Rows(0).Item("TONAME"))
                    _To.Add(Item)

                    'BCC's

                    For i = 1 To 10
                        ' BCC() 's
                        If dsNew.Tables(0).Rows(0).Item("BCC" + i.ToString()).ToString() <> Nothing Then
                            Item = New MailAddress(dsNew.Tables(0).Rows(0).Item("BCC" + i.ToString()).ToString(), dsNew.Tables(0).Rows(0).Item("BCC" + i.ToString() + "NAME").ToString())
                            _BCC.Add(Item)
                        End If
                        If dsNew.Tables(0).Rows(0).Item("CC" + i.ToString()).ToString() <> Nothing Then
                            Item = New MailAddress(dsNew.Tables(0).Rows(0).Item("CC" + i.ToString()).ToString(), dsNew.Tables(0).Rows(0).Item("CC" + i.ToString() + "NAME").ToString())
                            _CC.Add(Item)
                        End If

                    Next

                    Email.SendMail(_From, _To, _CC, _BCC, StrSqlBody, _Subject)
                End If

                

            Catch ex As Exception
                Throw New Exception("ShopUpInsData:PreSaleMail:" + ex.Message.ToString())
            End Try
        End Sub

        Public Sub ConfirmOrderMail(ByVal RefNumber As String, ByVal UserId As String, ByVal biltoAdd As String, ByVal CardHAdd As String, ByVal Type As String)
            Dim objUGetData As New UsersGetData.Selectdata()
            Dim objGetData As New ShopGetData.Selectdata()
            Dim objGetMData As New UsersGetData.Selectdata
            Dim ds As New DataSet()
            Dim dsCust As New DataSet()
            Dim dsShipAdd As New DataSet()
            Dim dsbilAdd As New DataSet()
            Dim dsCardH As New DataSet()
            Dim dsNew As New DataSet()
            Dim dsGroupOrder As New DataSet
            Dim dsUser As New DataSet()
            Dim dsEmailConfig As New DataSet()
            Dim StrSqlBody As String = String.Empty
            Dim _To As New MailAddressCollection()
            Dim _CC As New MailAddressCollection()
            Dim _BCC As New MailAddressCollection()
            Dim Item As MailAddress
            Dim Email As New EmailConfig()
            Dim i As New Integer
            Dim strHeader As String = String.Empty

            Try
                ds = objGetData.GetOrderReview(RefNumber)
                dsEmailConfig = objUGetData.GetEmailConfigDetails("Y")
                dsUser = objUGetData.GetUserDetails(UserId)

                dsGroupOrder = objGetData.GetOrderReviewGroup(RefNumber)
                dsCust = objGetData.GetCustInfo(RefNumber)
                dsbilAdd = objUGetData.GetBillToShipToUserDetailsByAddID(biltoAdd)
                dsCardH = objUGetData.GetBillToShipToUserDetailsByAddID(CardHAdd)

                ''Getting Product Message
                'Dim msgNew As String = ""
                'Dim StrSql As String = ""
                'Dim Dts As New DataSet
                'Dim odbUtil As New DBUtil
                'StrSql = "SELECT * FROM PRODUCTTYPE WHERE TYPE=" + Type.ToString() + " "
                'Dts = odbUtil.FillDataSet(StrSql, ShoppingConnection)
                'If Dts.Tables(0).Rows.Count > 0 Then
                '    msgNew = Dts.Tables(0).Rows(0).Item("MESSAGE").ToString()
                'End If


                'IF THE PATMENT TYPE IS INVOICE 
                If dsCust.Tables(0).Rows(0)("PAYMENTTYPE").ToString().ToUpper() = "INVOICE" Then
                    strHeader = "Order confirmation for order "
                    dsNew = objGetMData.GetAlliedMemberMail("INVCNF")
                Else
                    strHeader = "Order confirmation for order "
                    dsNew = objGetMData.GetAlliedMemberMail("ORDCNF")
                End If

                StrSqlBody = StrSqlBody & "<table cellpadding='0' cellspacing='0' style='width:650px'> "
                StrSqlBody = StrSqlBody & "<tr> "
                StrSqlBody = StrSqlBody & "<td><img src='" + dsEmailConfig.Tables(0).Rows(0).Item("URL") + "/Images/SavvyPackLogo3.gif' />"
                StrSqlBody = StrSqlBody & "<div style='color:#336699;font-size:18px;font-family:Verdana;font-weight:bold;margin-left:2px'>Order confirmation for order " + RefNumber + "</div> "
                StrSqlBody = StrSqlBody & "</td></tr></table>"

                If Type = "SA" Then
                    StrSqlBody = StrSqlBody + "<br/><table><tr style='height:20px;text-align:left;font-family:Verdana;font-size:12px;'><td>"

                    If dsCust.Tables(0).Rows(0)("PAYMENTTYPE").ToString().ToUpper() = "INVOICE" Then
                        StrSqlBody = StrSqlBody & "<b><div style='font-family:Verdana;font-size:12px;'>Your access to Structure Assistant will be provided upon receipt of payment.</b></div>"
                        StrSqlBody = StrSqlBody & "</td> </tr> "
                        StrSqlBody = StrSqlBody + "<tr style='height:20px;text-align:left;font-family:Verdana;font-size:12px;'><td>"
                        StrSqlBody = StrSqlBody & "<br/><div style='font-family:Verdana;font-size:12px;'>Thank you for your order.</br>"
                        StrSqlBody = StrSqlBody & "</div></td> </tr></table> "
                    Else
                        StrSqlBody = StrSqlBody & "<b><div style='font-family:Verdana;font-size:12px;'>You now have access to Structure Assistant.<br/><br/></b></div>"
                        StrSqlBody = StrSqlBody & "</td> </tr> "
                        StrSqlBody = StrSqlBody + "<tr style='height:20px;text-align:left;font-family:Verdana;font-size:12px;'><td>"
                        StrSqlBody = StrSqlBody & "<br/><div style='font-family:Verdana;font-size:12px;'>Thank you for your order.</br>"
                        StrSqlBody = StrSqlBody & "</div></td> </tr></table> "
                    End If
                ElseIf Type = "KBCOPK" Then
                    StrSqlBody = StrSqlBody + "<br/><table><tr style='height:20px;text-align:left;font-family:Verdana;font-size:12px;'><td>"

                    If dsCust.Tables(0).Rows(0)("PAYMENTTYPE").ToString().ToUpper() = "INVOICE" Then
                        StrSqlBody = StrSqlBody & "<b><div style='font-family:Verdana;font-size:12px;'>Your access to Contract Packager Knowledgebase will be provided upon receipt of payment.</b></div>"
                        StrSqlBody = StrSqlBody & "</td> </tr> "
                        StrSqlBody = StrSqlBody + "<tr style='height:20px;text-align:left;font-family:Verdana;font-size:12px;'><td>"
                        StrSqlBody = StrSqlBody & "<br/><div style='font-family:Verdana;font-size:12px;'>Thank you for your order.</br>"
                        StrSqlBody = StrSqlBody & "</div></td> </tr></table> "
                    Else
                        StrSqlBody = StrSqlBody & "<b><div style='font-family:Verdana;font-size:12px;'>You now have access to Contract Packager Knowledgebase.<br/><br/></b></div>"
                        StrSqlBody = StrSqlBody & "</td> </tr> "
                        StrSqlBody = StrSqlBody + "<tr style='height:20px;text-align:left;font-family:Verdana;font-size:12px;'><td>"
                        StrSqlBody = StrSqlBody & "<br/><div style='font-family:Verdana;font-size:12px;'>Thank you for your order.</br>"
                        StrSqlBody = StrSqlBody & "</div></td> </tr></table> "
                    End If
                Else
                    StrSqlBody = StrSqlBody + "<br/><table><tr style='height:20px;text-align:left;font-family:Verdana;font-size:12px;'><td>"

                    If dsCust.Tables(0).Rows(0)("PAYMENTTYPE").ToString().ToUpper() = "INVOICE" Then
                        StrSqlBody = StrSqlBody & "<b><div style='font-family:Verdana;font-size:12px;'>Your study will be delivered in PDF format by email upon receipt of payment.</b></div></br>"
                        StrSqlBody = StrSqlBody + "<tr style='height:20px;text-align:left;font-family:Verdana;font-size:12px;'><td>"
                        StrSqlBody = StrSqlBody & "<div style='font-family:Verdana;font-size:12px;'>Thank you for your order.</br>"
                        StrSqlBody = StrSqlBody & "</div></td> </tr></table> "
                    Else
                        StrSqlBody = StrSqlBody & "<b><div style='font-family:Verdana;font-size:12px;'>Your study will be delivered within 24 hours in PDF format by email.</b></div></br>"
                        StrSqlBody = StrSqlBody + "<tr style='height:20px;text-align:left;font-family:Verdana;font-size:12px;'><td>"
                        StrSqlBody = StrSqlBody & "<div style='font-family:Verdana;font-size:12px;'>Thank you for your order.</br>"
                        StrSqlBody = StrSqlBody & "</div></td> </tr></table> "
                    End If


                End If


                StrSqlBody = StrSqlBody & "</table><br/> "

                '********************************Ship To Address******************************************

                For i = 0 To ds.Tables(0).Rows.Count - 1
                    If ds.Tables(0).Rows(i)("SHIPTOID").ToString().Trim <> "" Then
                        dsShipAdd = objUGetData.GetBillToShipToUserDetailsByAddID(ds.Tables(0).Rows(i)("SHIPTOID").ToString())

                        StrSqlBody = StrSqlBody + "<div style='font-family:Verdana;font-size:12px;margin-top:10px;margin-bottom:5px;font-weight:bold'>Item: " + ds.Tables(0).Rows(i)("ITEMDESCRIPTION").ToString() + "</div>"
                        StrSqlBody = StrSqlBody + "<div style='font-family:Verdana;font-size:12px;margin-top:0px;margin-bottom:5px;font-weight:bold'>Delivery Format: " + ds.Tables(0).Rows(i)("DELIVERYFORMAT").ToString() + "</div>"

                        StrSqlBody = StrSqlBody + "<div style='font-family:Verdana;font-size:12px;margin-top:0px;margin-bottom:5px;font-weight:bold'>Ship To Address</div>"
                        StrSqlBody = StrSqlBody + "<table style='font-family:Verdana;width:700px;font-size:10px;border-collapse:collapse' border='1' bordercolor='black' cellpadding='0' cellspacing='0'>  "
                        StrSqlBody = StrSqlBody + "<tr style='background-color:#336699;height:20px;text-align:center;color:white'> "
                        StrSqlBody = StrSqlBody + "<td><b>Name</b></td> "
                        StrSqlBody = StrSqlBody + "<td><b>Email Address</b></td> "
                        StrSqlBody = StrSqlBody + "<td><b>Phone Number</b></td> "
                        StrSqlBody = StrSqlBody + "<td><b>Fax Number</b></td> "
                        StrSqlBody = StrSqlBody + "<td><b>Address</b></td> "
                        StrSqlBody = StrSqlBody + "<td><b>City</b></td> "
                        StrSqlBody = StrSqlBody + "<td><b>State</b></td> "
                        StrSqlBody = StrSqlBody + "<td><b>Zip Code</b></td> "
                        StrSqlBody = StrSqlBody + "<td><b>Country</b></td> "
                        StrSqlBody = StrSqlBody + "</tr> "
                        StrSqlBody = StrSqlBody + "<tr style='height:20px;text-align:center'> "
                        StrSqlBody = StrSqlBody + "<td>" + dsShipAdd.Tables(0).Rows(0).Item("FULLNAME") + "</b></td> "
                        StrSqlBody = StrSqlBody + "<td>" + dsShipAdd.Tables(0).Rows(0).Item("EMAILADDRESS") + "</b></td> "

                        StrSqlBody = StrSqlBody + "<td>" + dsShipAdd.Tables(0).Rows(0).Item("PHONENUMBER") + "</b></td> "
                        StrSqlBody = StrSqlBody + "<td>" + dsShipAdd.Tables(0).Rows(0).Item("FAXNUMBER") + "</b></td> "
                        StrSqlBody = StrSqlBody + "<td>" + dsShipAdd.Tables(0).Rows(0).Item("STREETADDRESS1") + " " & dsUser.Tables(0).Rows(0).Item("STREETADDRESS2") + "</b></td> "
                        StrSqlBody = StrSqlBody + "<td>" + dsShipAdd.Tables(0).Rows(0).Item("CITY") + "</b></td> "
                        StrSqlBody = StrSqlBody + "<td>" + dsShipAdd.Tables(0).Rows(0).Item("STATE") + "</b></td> "
                        StrSqlBody = StrSqlBody + "<td>" + dsShipAdd.Tables(0).Rows(0).Item("ZIPCODE") + "</b></td> "
                        StrSqlBody = StrSqlBody + "<td>" + dsShipAdd.Tables(0).Rows(0).Item("COUNTRYDES") + "</b></td> "
                        StrSqlBody = StrSqlBody + "</tr> "
                        StrSqlBody = StrSqlBody + "</table><br/> "
                        '**************************************************************************
                    End If
                Next

                '********************************Bill To Address******************************************
                StrSqlBody = StrSqlBody + "<div style='font-family:Verdana;font-size:12px;margin-top:10px;margin-bottom:5px;font-weight:bold'>Bill To Address</div>"
                StrSqlBody = StrSqlBody + "<table style='font-family:Verdana;width:700px;font-size:10px;border-collapse:collapse' border='1' bordercolor='black' cellpadding='0' cellspacing='0'>  "
                StrSqlBody = StrSqlBody + "<tr style='background-color:#336699;height:20px;text-align:center;color:white'> "
                StrSqlBody = StrSqlBody + "<td><b>Name</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Email Address</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Phone Number</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Fax Number</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Address</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>City</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>State</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Zip Code</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Country</b></td> "
                StrSqlBody = StrSqlBody + "</tr> "
                StrSqlBody = StrSqlBody + "<tr style='height:20px;text-align:center'> "
                StrSqlBody = StrSqlBody + "<td>" + dsbilAdd.Tables(0).Rows(0).Item("FULLNAME") + "</b></td> "
                StrSqlBody = StrSqlBody + "<td>" + dsbilAdd.Tables(0).Rows(0).Item("EMAILADDRESS") + "</b></td> "
                StrSqlBody = StrSqlBody + "<td>" + dsbilAdd.Tables(0).Rows(0).Item("PHONENUMBER") + "</b></td> "
                StrSqlBody = StrSqlBody + "<td>" + dsbilAdd.Tables(0).Rows(0).Item("FAXNUMBER") + "</b></td> "
                StrSqlBody = StrSqlBody + "<td>" + dsbilAdd.Tables(0).Rows(0).Item("STREETADDRESS1") + " " & dsbilAdd.Tables(0).Rows(0).Item("STREETADDRESS2") + "</b></td> "
                StrSqlBody = StrSqlBody + "<td>" + dsbilAdd.Tables(0).Rows(0).Item("CITY") + "</b></td> "
                StrSqlBody = StrSqlBody + "<td>" + dsbilAdd.Tables(0).Rows(0).Item("STATE") + "</b></td> "
                StrSqlBody = StrSqlBody + "<td>" + dsbilAdd.Tables(0).Rows(0).Item("ZIPCODE") + "</b></td> "
                StrSqlBody = StrSqlBody + "<td>" + dsbilAdd.Tables(0).Rows(0).Item("COUNTRYDES") + "</b></td> "
                StrSqlBody = StrSqlBody + "</tr> "
                StrSqlBody = StrSqlBody + "</table> "
                '**************************************************************************

                '********************************Card Holder Address******************************************
                If CardHAdd <> "0" Then
                    StrSqlBody = StrSqlBody + "<div style='font-family:Verdana;font-size:12px;margin-top:10px;margin-bottom:5px;font-weight:bold'>Card Holder Address</div>"
                    StrSqlBody = StrSqlBody + "<table style='font-family:Verdana;width:700px;font-size:10px;border-collapse:collapse' border='1' bordercolor='black' cellpadding='0' cellspacing='0'>  "
                    StrSqlBody = StrSqlBody + "<tr style='background-color:#336699;height:20px;text-align:center;color:white'> "
                    StrSqlBody = StrSqlBody + "<td><b>Name</b></td> "
                    StrSqlBody = StrSqlBody + "<td><b>Email Address</b></td> "
                    StrSqlBody = StrSqlBody + "<td><b>Phone Number</b></td> "
                    StrSqlBody = StrSqlBody + "<td><b>Fax Number</b></td> "
                    StrSqlBody = StrSqlBody + "<td><b>Address</b></td> "
                    StrSqlBody = StrSqlBody + "<td><b>City</b></td> "
                    StrSqlBody = StrSqlBody + "<td><b>State</b></td> "
                    StrSqlBody = StrSqlBody + "<td><b>Zip Code</b></td> "
                    StrSqlBody = StrSqlBody + "<td><b>Country</b></td> "
                    StrSqlBody = StrSqlBody + "</tr> "
                    StrSqlBody = StrSqlBody + "<tr style='height:20px;text-align:center'> "
                    StrSqlBody = StrSqlBody + "<td>" + dsCardH.Tables(0).Rows(0).Item("FULLNAME") + "</b></td> "
                    StrSqlBody = StrSqlBody + "<td>" + dsCardH.Tables(0).Rows(0).Item("EMAILADDRESS") + "</b></td> "
                    StrSqlBody = StrSqlBody + "<td>" + dsCardH.Tables(0).Rows(0).Item("PHONENUMBER") + "</b></td> "
                    StrSqlBody = StrSqlBody + "<td>" + dsCardH.Tables(0).Rows(0).Item("FAXNUMBER") + "</b></td> "
                    StrSqlBody = StrSqlBody + "<td>" + dsCardH.Tables(0).Rows(0).Item("STREETADDRESS1") + " " & dsbilAdd.Tables(0).Rows(0).Item("STREETADDRESS2") + "</b></td> "
                    StrSqlBody = StrSqlBody + "<td>" + dsCardH.Tables(0).Rows(0).Item("CITY") + "</b></td> "
                    StrSqlBody = StrSqlBody + "<td>" + dsCardH.Tables(0).Rows(0).Item("STATE") + "</b></td> "
                    StrSqlBody = StrSqlBody + "<td>" + dsCardH.Tables(0).Rows(0).Item("ZIPCODE") + "</b></td> "
                    StrSqlBody = StrSqlBody + "<td>" + dsCardH.Tables(0).Rows(0).Item("COUNTRYDES") + "</b></td> "
                    StrSqlBody = StrSqlBody + "</tr> "
                    StrSqlBody = StrSqlBody + "</table> "
                End If

                '**************************************************************************

                '*****************************OrderDetails********************************
                StrSqlBody = StrSqlBody + "<div style='font-family:Verdana;font-size:12px;margin-top:10px;margin-bottom:5px;font-weight:bold'>Order Details</div>"
                StrSqlBody = StrSqlBody + "<table style='font-family:Verdana;width:700px;font-size:10px;border-collapse:collapse' border='1' bordercolor='black' cellpadding='0' cellspacing='0'>  "
                StrSqlBody = StrSqlBody + "<tr style='background-color:#336699;height:20px;text-align:center;color:white'> "
                StrSqlBody = StrSqlBody + "<td><b>Order Number</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Quantity</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Item</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Item Description</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Format</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Page</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Unit</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>SubTotal</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Sales Tax</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Shipping & Handling Fees</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Total</b></td> "
                StrSqlBody = StrSqlBody + "</tr> "
                For i = 0 To dsGroupOrder.Tables(0).Rows.Count - 1
                    StrSqlBody = StrSqlBody + "<tr style='height:20px;text-align:center'> "
                    StrSqlBody = StrSqlBody + "<td><b>" + RefNumber + "</b></td> "
                    StrSqlBody = StrSqlBody & "<td><b>" + dsGroupOrder.Tables(0).Rows(i).Item("QTY").ToString() + "</b></td> "
                    StrSqlBody = StrSqlBody & "<td><b>" + dsGroupOrder.Tables(0).Rows(i).Item("ITEMNUMBER").ToString() + "</b></td> "
                    StrSqlBody = StrSqlBody & "<td><b>" + dsGroupOrder.Tables(0).Rows(i).Item("ITEMDESCRIPTION").ToString() + "</b></td> "
                    StrSqlBody = StrSqlBody & "<td><b>" + dsGroupOrder.Tables(0).Rows(i).Item("DELIVERYFORMAT").ToString() + "</b></td> "
                    StrSqlBody = StrSqlBody & "<td><b>" + dsGroupOrder.Tables(0).Rows(i).Item("PAGE").ToString() + "</b></td> "
                    StrSqlBody = StrSqlBody & "<td><b>" + FormatCurrency(dsGroupOrder.Tables(0).Rows(i).Item("UNITCOST").ToString(), 0) + "</b></td> "
                    StrSqlBody = StrSqlBody & "<td><b>" + FormatCurrency(dsGroupOrder.Tables(0).Rows(i).Item("SUBTOTAL").ToString(), 0) + "</b></td> "
                    StrSqlBody = StrSqlBody & "<td><b>" + FormatCurrency(dsGroupOrder.Tables(0).Rows(i).Item("SALESTAX").ToString(), 2) + "</b></td> "
                    StrSqlBody = StrSqlBody & "<td><b>" + FormatCurrency(dsGroupOrder.Tables(0).Rows(i).Item("SHIPPINGCOST").ToString(), 2) + "</b></td> "
                    StrSqlBody = StrSqlBody & "<td><b>" + FormatCurrency(dsGroupOrder.Tables(0).Rows(i).Item("TOTAL").ToString(), 2) + "</b></td> "
                    StrSqlBody = StrSqlBody + "</tr> "
                Next
                StrSqlBody = StrSqlBody & "</table> "
                StrSqlBody = StrSqlBody + "<table><tr style='height:20px;text-align:left;font-family:Verdana;font-size:12px;'><td>"
                StrSqlBody = StrSqlBody & "<br/>If you have any questions or concerns about your order please call 1.952.898.2000 or email <a style='TEXT-DECORATION: none;font-style:italic;font-weight:bold' class='Link' href='mailto:sales@savvypack.com'>sales@savvypack.com</a>."

                StrSqlBody = StrSqlBody & "</div></td> </tr></table> "
                StrSqlBody = StrSqlBody & "<br/><br/><br/><div style='font-family:Verdana;font-size:12px;'>SavvyPack Corporation<br/>1850 East 121st Street Suite 106B<br/>Burnsville, MN USA 55337<br/><a style='font-family:verdana;' href='www.savvypack.com'>www.savvypack.com</a><br/>Phone: 1-952-405-7500</div>"




                If dsNew.Tables(0).Rows.Count > 0 Then
                    Dim _From As New MailAddress(dsNew.Tables(0).Rows(0).Item("FROMADD").ToString(), dsNew.Tables(0).Rows(0).Item("FROMNAME").ToString())
                    Dim _Subject As String = dsNew.Tables(0).Rows(0).Item("SUBJECT").ToString()
                    'To's
                    Item = New MailAddress(dsUser.Tables(0).Rows(0).Item("EMAILADDRESS"), dsUser.Tables(0).Rows(0).Item("FIRSTNAME"))
                    _To.Add(Item)

                    'BCC's

                    For i = 1 To 10
                        ' BCC() 's
                        If dsNew.Tables(0).Rows(0).Item("BCC" + i.ToString()).ToString() <> Nothing Then
                            Item = New MailAddress(dsNew.Tables(0).Rows(0).Item("BCC" + i.ToString()).ToString(), dsNew.Tables(0).Rows(0).Item("BCC" + i.ToString() + "NAME").ToString())
                            _BCC.Add(Item)
                        End If
                        If dsNew.Tables(0).Rows(0).Item("CC" + i.ToString()).ToString() <> Nothing Then
                            Item = New MailAddress(dsNew.Tables(0).Rows(0).Item("CC" + i.ToString()).ToString(), dsNew.Tables(0).Rows(0).Item("CC" + i.ToString() + "NAME").ToString())
                            _CC.Add(Item)
                        End If

                    Next

                    Email.SendMail(_From, _To, _CC, _BCC, StrSqlBody, _Subject)
                End If


            Catch ex As Exception
                Throw New Exception("ShopUpInsData:ConfirmOrderMail:" + ex.Message.ToString())
            End Try
        End Sub
        Public Sub ConfirmOrderMailConf(ByVal RefNumber As String, ByVal UserId As String, ByVal AttAdd() As String, ByVal biltoAdd As String, ByVal CardHAdd As String)
            Dim objUGetData As New UsersGetData.Selectdata()
            Dim objGetData As New ShopGetData.Selectdata()
            Dim ds As New DataSet()
            Dim dsCust As New DataSet()
            Dim dsUser As New DataSet()
            Dim dsEmailConfig As New DataSet()
            Dim dsAttAdd As New DataSet()
            Dim dsbilAdd As New DataSet()
            Dim dsCardH As New DataSet()
            Dim StrSqlBody As String = String.Empty
            Dim _To As New MailAddressCollection()
            'Dim _From As New MailAddress("sales@savvypack.com", "Sales")
            Dim _CC As New MailAddressCollection()
            Dim _BCC As New MailAddressCollection()
            Dim Item As MailAddress
            Dim Email As New EmailConfig()
            'Dim _Subject As String = "Order confirmation from SavvyPack Corporation"
            Dim i As New Integer
            Dim strHeader As String = String.Empty
            Dim cnt As Integer = 0

            Try
                Dim objGetMData As New UsersGetData.Selectdata
                Dim dsNew As New DataSet


                ds = objGetData.GetOrderReview(RefNumber)
                dsCust = objGetData.GetCustInfo(RefNumber)
                dsEmailConfig = objUGetData.GetEmailConfigDetails("Y")
                dsUser = objUGetData.GetUserDetails(UserId)
                dsbilAdd = objUGetData.GetBillToShipToUserDetailsByAddID(biltoAdd)
                dsCardH = objUGetData.GetBillToShipToUserDetailsByAddID(CardHAdd)
                'IF THE PATMENT TYPE IS INVOICE 
                If dsCust.Tables(0).Rows(0)("PAYMENTTYPE").ToString().ToUpper() = "INVOICE" Then
                    strHeader = "Order confirmation for order "
                    dsNew = objGetMData.GetAlliedMemberMail("INVCNF")
                Else
                    strHeader = "Order confirmation for order "
                    dsNew = objGetMData.GetAlliedMemberMail("ORDCNF")
                End If
                '
                StrSqlBody = StrSqlBody & "<table cellpadding='0' cellspacing='0' style='width:650px'> "
                StrSqlBody = StrSqlBody & "<tr> "
                StrSqlBody = StrSqlBody & "<td><img src='" + dsEmailConfig.Tables(0).Rows(0).Item("URL") + "/Images/SavvyPackLogo3.gif' />"
                StrSqlBody = StrSqlBody & "<div style='color:#336699;font-size:18px;font-family:Verdana;font-weight:bold;margin-left:2px'>Order confirmation for order " + RefNumber + "</div> "
                StrSqlBody = StrSqlBody & "</td> "
                StrSqlBody = StrSqlBody & "</tr></table><br/> "



                '********************************Ship To Address******************************************
                For i = 0 To 10
                    If AttAdd(i) <> "0" And AttAdd(i) <> Nothing Then
                        cnt += 1
                        dsAttAdd = objUGetData.GetBillToShipToUserDetailsByAddID(AttAdd(i))
                        StrSqlBody = StrSqlBody + "<div style='font-family:Verdana;font-size:12px;margin-top:10px;margin-bottom:5px;font-weight:bold'>Attendee " + cnt.ToString() + "</div>"
                        StrSqlBody = StrSqlBody + "<table style='font-family:Verdana;width:700px;font-size:10px;border-collapse:collapse' border='1' bordercolor='black' cellpadding='0' cellspacing='0'>  "
                        StrSqlBody = StrSqlBody + "<tr style='background-color:#336699;height:20px;text-align:center;color:white'> "
                        StrSqlBody = StrSqlBody + "<td><b>Name</b></td> "
                        StrSqlBody = StrSqlBody + "<td><b>Email Address</b></td> "
                        StrSqlBody = StrSqlBody + "<td><b>Phone Number</b></td> "
                        StrSqlBody = StrSqlBody + "<td><b>Fax Number</b></td> "
                        StrSqlBody = StrSqlBody + "<td><b>Address</b></td> "
                        StrSqlBody = StrSqlBody + "<td><b>City</b></td> "
                        StrSqlBody = StrSqlBody + "<td><b>State</b></td> "
                        StrSqlBody = StrSqlBody + "<td><b>Zip Code</b></td> "
                        StrSqlBody = StrSqlBody + "<td><b>Country</b></td> "
                        StrSqlBody = StrSqlBody + "</tr> "
                        StrSqlBody = StrSqlBody + "<tr style='height:20px;text-align:center'> "
                        StrSqlBody = StrSqlBody + "<td>" + dsAttAdd.Tables(0).Rows(0).Item("FULLNAME") + "</b></td> "
                        StrSqlBody = StrSqlBody + "<td>" + dsAttAdd.Tables(0).Rows(0).Item("EMAILADDRESS") + "</b></td> "

                        StrSqlBody = StrSqlBody + "<td>" + dsAttAdd.Tables(0).Rows(0).Item("PHONENUMBER") + "</b></td> "
                        StrSqlBody = StrSqlBody + "<td>" + dsAttAdd.Tables(0).Rows(0).Item("FAXNUMBER") + "</b></td> "
                        StrSqlBody = StrSqlBody + "<td>" + dsAttAdd.Tables(0).Rows(0).Item("STREETADDRESS1") + " " & dsUser.Tables(0).Rows(0).Item("STREETADDRESS2") + "</b></td> "
                        StrSqlBody = StrSqlBody + "<td>" + dsAttAdd.Tables(0).Rows(0).Item("CITY") + "</b></td> "
                        StrSqlBody = StrSqlBody + "<td>" + dsAttAdd.Tables(0).Rows(0).Item("STATE") + "</b></td> "
                        StrSqlBody = StrSqlBody + "<td>" + dsAttAdd.Tables(0).Rows(0).Item("ZIPCODE") + "</b></td> "
                        StrSqlBody = StrSqlBody + "<td>" + dsAttAdd.Tables(0).Rows(0).Item("COUNTRYDES") + "</b></td> "
                        StrSqlBody = StrSqlBody + "</tr> "
                        StrSqlBody = StrSqlBody + "</table><br/> "
                    End If
                Next

                '**************************************************************************


                '********************************Bill To Address******************************************
                StrSqlBody = StrSqlBody + "<div style='font-family:Verdana;font-size:12px;margin-top:10px;margin-bottom:5px;font-weight:bold'>Bill To Address</div>"
                StrSqlBody = StrSqlBody + "<table style='font-family:Verdana;width:700px;font-size:10px;border-collapse:collapse' border='1' bordercolor='black' cellpadding='0' cellspacing='0'>  "
                StrSqlBody = StrSqlBody + "<tr style='background-color:#336699;height:20px;text-align:center;color:white'> "
                StrSqlBody = StrSqlBody + "<td><b>Name</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Email Address</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Phone Number</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Fax Number</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Address</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>City</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>State</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Zip Code</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Country</b></td> "
                StrSqlBody = StrSqlBody + "</tr> "
                StrSqlBody = StrSqlBody + "<tr style='height:20px;text-align:center'> "
                StrSqlBody = StrSqlBody + "<td>" + dsbilAdd.Tables(0).Rows(0).Item("FULLNAME") + "</b></td> "
                StrSqlBody = StrSqlBody + "<td>" + dsbilAdd.Tables(0).Rows(0).Item("EMAILADDRESS") + "</b></td> "
                StrSqlBody = StrSqlBody + "<td>" + dsbilAdd.Tables(0).Rows(0).Item("PHONENUMBER") + "</b></td> "
                StrSqlBody = StrSqlBody + "<td>" + dsbilAdd.Tables(0).Rows(0).Item("FAXNUMBER") + "</b></td> "
                StrSqlBody = StrSqlBody + "<td>" + dsbilAdd.Tables(0).Rows(0).Item("STREETADDRESS1") + " " & dsbilAdd.Tables(0).Rows(0).Item("STREETADDRESS2") + "</b></td> "
                StrSqlBody = StrSqlBody + "<td>" + dsbilAdd.Tables(0).Rows(0).Item("CITY") + "</b></td> "
                StrSqlBody = StrSqlBody + "<td>" + dsbilAdd.Tables(0).Rows(0).Item("STATE") + "</b></td> "
                StrSqlBody = StrSqlBody + "<td>" + dsbilAdd.Tables(0).Rows(0).Item("ZIPCODE") + "</b></td> "
                StrSqlBody = StrSqlBody + "<td>" + dsbilAdd.Tables(0).Rows(0).Item("COUNTRYDES") + "</b></td> "
                StrSqlBody = StrSqlBody + "</tr> "
                StrSqlBody = StrSqlBody + "</table> "
                '**************************************************************************

                '********************************Card Holder Address******************************************
                If CardHAdd <> "0" Then
                    StrSqlBody = StrSqlBody + "<div style='font-family:Verdana;font-size:12px;margin-top:10px;margin-bottom:5px;font-weight:bold'>Card Holder Address</div>"
                    StrSqlBody = StrSqlBody + "<table style='font-family:Verdana;width:700px;font-size:10px;border-collapse:collapse' border='1' bordercolor='black' cellpadding='0' cellspacing='0'>  "
                    StrSqlBody = StrSqlBody + "<tr style='background-color:#336699;height:20px;text-align:center;color:white'> "
                    StrSqlBody = StrSqlBody + "<td><b>Name</b></td> "
                    StrSqlBody = StrSqlBody + "<td><b>Email Address</b></td> "
                    StrSqlBody = StrSqlBody + "<td><b>Phone Number</b></td> "
                    StrSqlBody = StrSqlBody + "<td><b>Fax Number</b></td> "
                    StrSqlBody = StrSqlBody + "<td><b>Address</b></td> "
                    StrSqlBody = StrSqlBody + "<td><b>City</b></td> "
                    StrSqlBody = StrSqlBody + "<td><b>State</b></td> "
                    StrSqlBody = StrSqlBody + "<td><b>Zip Code</b></td> "
                    StrSqlBody = StrSqlBody + "<td><b>Country</b></td> "
                    StrSqlBody = StrSqlBody + "</tr> "
                    StrSqlBody = StrSqlBody + "<tr style='height:20px;text-align:center'> "
                    StrSqlBody = StrSqlBody + "<td>" + dsCardH.Tables(0).Rows(0).Item("FULLNAME") + "</b></td> "
                    StrSqlBody = StrSqlBody + "<td>" + dsCardH.Tables(0).Rows(0).Item("EMAILADDRESS") + "</b></td> "
                    StrSqlBody = StrSqlBody + "<td>" + dsCardH.Tables(0).Rows(0).Item("PHONENUMBER") + "</b></td> "
                    StrSqlBody = StrSqlBody + "<td>" + dsCardH.Tables(0).Rows(0).Item("FAXNUMBER") + "</b></td> "
                    StrSqlBody = StrSqlBody + "<td>" + dsCardH.Tables(0).Rows(0).Item("STREETADDRESS1") + " " & dsbilAdd.Tables(0).Rows(0).Item("STREETADDRESS2") + "</b></td> "
                    StrSqlBody = StrSqlBody + "<td>" + dsCardH.Tables(0).Rows(0).Item("CITY") + "</b></td> "
                    StrSqlBody = StrSqlBody + "<td>" + dsCardH.Tables(0).Rows(0).Item("STATE") + "</b></td> "
                    StrSqlBody = StrSqlBody + "<td>" + dsCardH.Tables(0).Rows(0).Item("ZIPCODE") + "</b></td> "
                    StrSqlBody = StrSqlBody + "<td>" + dsCardH.Tables(0).Rows(0).Item("COUNTRYDES") + "</b></td> "
                    StrSqlBody = StrSqlBody + "</tr> "
                    StrSqlBody = StrSqlBody + "</table> "
                End If

                '**************************************************************************

                '*****************************OrderDetails********************************
                StrSqlBody = StrSqlBody + "<div style='font-family:Verdana;font-size:12px;margin-top:10px;margin-bottom:5px;font-weight:bold'>Order Details</div>"
                StrSqlBody = StrSqlBody + "<table style='font-family:Verdana;width:700px;font-size:10px;border-collapse:collapse' border='1' bordercolor='black' cellpadding='0' cellspacing='0'>  "
                StrSqlBody = StrSqlBody + "<tr style='background-color:#336699;height:20px;text-align:center;color:white'> "
                StrSqlBody = StrSqlBody + "<td><b>Order Number</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Quantity</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Item</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Item Description</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Format</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Page</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Unit</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>SubTotal</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Sales Tax</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Shipping & Handling Fees</b></td> "
                StrSqlBody = StrSqlBody + "<td><b>Total</b></td> "
                StrSqlBody = StrSqlBody + "</tr> "
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    StrSqlBody = StrSqlBody + "<tr style='height:20px;text-align:center'> "
                    StrSqlBody = StrSqlBody + "<td><b>" + RefNumber + "</b></td> "
                    StrSqlBody = StrSqlBody & "<td><b>" + ds.Tables(0).Rows(i).Item("QTY").ToString() + "</b></td> "
                    StrSqlBody = StrSqlBody & "<td><b>" + ds.Tables(0).Rows(i).Item("ITEMNUMBER").ToString() + "</b></td> "
                    StrSqlBody = StrSqlBody & "<td><b>" + ds.Tables(0).Rows(i).Item("ITEMDESCRIPTION").ToString() + "</b></td> "
                    StrSqlBody = StrSqlBody & "<td><b>" + ds.Tables(0).Rows(i).Item("DELIVERYFORMAT").ToString() + "</b></td> "
                    StrSqlBody = StrSqlBody & "<td><b>" + ds.Tables(0).Rows(i).Item("PAGE").ToString() + "</b></td> "
                    StrSqlBody = StrSqlBody & "<td><b>" + FormatCurrency(ds.Tables(0).Rows(i).Item("UNITCOST").ToString(), 0) + "</b></td> "
                    StrSqlBody = StrSqlBody & "<td><b>" + FormatCurrency(ds.Tables(0).Rows(i).Item("SUBTOTAL").ToString(), 0) + "</b></td> "
                    StrSqlBody = StrSqlBody & "<td><b>" + FormatCurrency(ds.Tables(0).Rows(i).Item("SALESTAX").ToString(), 2) + "</b></td> "
                    StrSqlBody = StrSqlBody & "<td><b>" + FormatCurrency(ds.Tables(0).Rows(i).Item("SHIPPINGCOST").ToString(), 2) + "</b></td> "
                    StrSqlBody = StrSqlBody & "<td><b>" + FormatCurrency(ds.Tables(0).Rows(i).Item("TOTAL").ToString(), 2) + "</b></td> "
                    StrSqlBody = StrSqlBody + "</tr> "
                Next
                StrSqlBody = StrSqlBody & "</table> "
                StrSqlBody = StrSqlBody + "<table><tr style='height:20px;text-align:left;font-family:Verdana;font-size:12px;'><td>"
                StrSqlBody = StrSqlBody & "<br/><div style='font-family:Verdana;font-size:12px;'>Thank you for your order.</br>"
                If dsCust.Tables(0).Rows(0)("PAYMENTTYPE").ToString().ToUpper() = "INVOICE" Then
                    StrSqlBody = StrSqlBody & "<br/>Preparation of an Invoice can take up to 24 hours.<br/>"
                    StrSqlBody = StrSqlBody & "<br/>Once preparation is complete, the invoice will be emailed to the Bill To Address."
                    StrSqlBody = StrSqlBody & "<br/>Invoice must be paid prior to the date of the Web Conference."
                Else
                    'StrSqlBody = StrSqlBody & "<br/>Processing of a Credit Card order can take up to 24 hours.<br/>"
                    StrSqlBody = StrSqlBody & "<br/>Once preparation is complete, information to join the Web Conference will be activated."
                End If

                StrSqlBody = StrSqlBody & "<br/>If you have any questions or concerns about your order please call 1.952.898.2000 or email <a style='TEXT-DECORATION: none;font-style:italic;font-weight:bold' class='Link' href='mailto:sales@savvypack.com'>sales@savvypack.com</a>."

                StrSqlBody = StrSqlBody & "</div></td> </tr></table> "
                StrSqlBody = StrSqlBody & "<br/><br/><br/><div style='font-family:Verdana;font-size:12px;'>SavvyPack Corporation<br/>1850 East 121st Street Suite 106B<br/>Burnsville, MN USA 55337<br/><a style='font-family:verdana;' href='www.savvypack.com'>www.savvypack.com</a><br/>Phone: 1-952-405-7500</div>"


                If dsNew.Tables(0).Rows.Count > 0 Then
                    Dim _From As New MailAddress(dsNew.Tables(0).Rows(0).Item("FROMADD").ToString(), dsNew.Tables(0).Rows(0).Item("FROMNAME").ToString())
                    Dim _Subject As String = dsNew.Tables(0).Rows(0).Item("SUBJECT").ToString()
                    'To's
                    Item = New MailAddress(dsUser.Tables(0).Rows(0).Item("EMAILADDRESS"), dsUser.Tables(0).Rows(0).Item("FIRSTNAME"))
                    _To.Add(Item)

                    'BCC's

                    For i = 1 To 10
                        ' BCC() 's
                        If dsNew.Tables(0).Rows(0).Item("BCC" + i.ToString()).ToString() <> Nothing Then
                            Item = New MailAddress(dsNew.Tables(0).Rows(0).Item("BCC" + i.ToString()).ToString(), dsNew.Tables(0).Rows(0).Item("BCC" + i.ToString() + "NAME").ToString())
                            _BCC.Add(Item)
                        End If
                        If dsNew.Tables(0).Rows(0).Item("CC" + i.ToString()).ToString() <> Nothing Then
                            Item = New MailAddress(dsNew.Tables(0).Rows(0).Item("CC" + i.ToString()).ToString(), dsNew.Tables(0).Rows(0).Item("CC" + i.ToString() + "NAME").ToString())
                            _CC.Add(Item)
                        End If

                    Next

                    Email.SendMail(_From, _To, _CC, _BCC, StrSqlBody, _Subject)
                End If

            Catch ex As Exception
                Throw New Exception("ShopUpInsData:ConfirmOrderMail:" + ex.Message.ToString())
            End Try
        End Sub
        Public Sub UpdateOrderLog(ByVal RefNo As String, ByVal UserId As String, ByVal userName As String)
            Dim odbUtil As New DBUtil()
            Dim objCrypt As New CryptoHelper()
            Dim StrSql As String
            Dim objGetData As New ShopGetData.Selectdata()
            Dim dsInv As New DataSet()
            Dim dsCus As New DataSet()
            Dim arrOrderLog(50) As String
            Dim objGetUData As New UsersGetData.Selectdata()
            Dim REFNUMBER As String = ""
            Dim QTY As String = ""
            Dim ITEMNUMBER As String = ""
            Dim BILLTOUSERID As String = ""
            Dim BILLTOUSERNAME As String = ""
            Dim SHIPLTOUSERID As String = ""

            Dim SHIPTOUSERNAME As String = ""
            Dim CARDHTOUSERID As String = ""
            Dim CARDHTOUSERNAME As String = ""
            Dim WEBCONFATTUSERID As String = ""
            Dim WEBCONFATTUSERNAME As String = ""

            Dim UNITCOST As String = ""
            Dim SUBTOTAL As String = ""
            Dim SALESTAX As String = ""
            Dim TOTAL As String = ""
            Dim PAYMENTTYPE As String = ""
            Dim CARDNUMBER As String = ""
            Dim CARDEXP As String = ""
            Dim CARDTYPE As String = ""
            Dim AUTH_CODE As String = ""
            Dim ITEMDESCRIPTION As String = ""
            Dim DELIVERYFORMAT As String = ""

            Dim i As Integer = 0


            Try
                dsInv = objGetData.GetOrderReviewAShop(RefNo)
                dsCus = objGetData.GetCustInfo(RefNo)

                If dsInv.Tables(0).Rows.Count > 0 Then
                    For i = 0 To dsInv.Tables(0).Rows.Count - 1
                        REFNUMBER = RefNo.ToString()
                        QTY = dsInv.Tables(0).Rows(i).Item("QTY").ToString()
                        ITEMNUMBER = dsInv.Tables(0).Rows(i).Item("ITEMNUMBER").ToString()
                        ITEMDESCRIPTION = dsInv.Tables(0).Rows(i).Item("ITEMDESCRIPTION").ToString()
                        DELIVERYFORMAT = dsInv.Tables(0).Rows(i).Item("DELIVERYFORMAT").ToString()

                        UserId = UserId.ToString()
                        userName = objGetUData.GetAddresNameByID(objGetUData.GetUserAddressIDByHeadID("Login User", UserId)) 'userName.ToString()
                        BILLTOUSERID = dsInv.Tables(0).Rows(0).Item("BILLTOID").ToString()
                        BILLTOUSERNAME = objGetUData.GetAddresNameByID(dsInv.Tables(0).Rows(0).Item("BILLTOID").ToString())
                        SHIPLTOUSERID = dsInv.Tables(0).Rows(i).Item("SHIPTOID").ToString()
                        SHIPTOUSERNAME = objGetUData.GetAddresNameByID(dsInv.Tables(0).Rows(i).Item("SHIPTOID").ToString())
                        CARDHTOUSERID = dsInv.Tables(0).Rows(0).Item("CARDHID").ToString()
                        CARDHTOUSERNAME = objGetUData.GetAddresNameByID(dsInv.Tables(0).Rows(0).Item("CARDHID").ToString())
                        WEBCONFATTUSERID = "null"
                        WEBCONFATTUSERNAME = ""

                        UNITCOST = dsInv.Tables(0).Rows(i).Item("UNITCOST").ToString()
                        SUBTOTAL = dsInv.Tables(0).Rows(i).Item("SUBTOTAL").ToString()
                        SALESTAX = dsInv.Tables(0).Rows(i).Item("SALESTAX").ToString()
                        TOTAL = dsInv.Tables(0).Rows(i).Item("TOTAL").ToString()

                        If dsCus.Tables(0).Rows.Count > 0 Then
                            PAYMENTTYPE = dsCus.Tables(0).Rows(0).Item("PAYMENTTYPE").ToString()
                            If dsCus.Tables(0).Rows(0).Item("CARDNUMBER").ToString() = "" Then
                                CARDNUMBER = ""
                            Else
                                CARDNUMBER = objCrypt.Encrypt(dsCus.Tables(0).Rows(0).Item("CARDNUMBER").ToString())
                            End If

                            CARDEXP = dsCus.Tables(0).Rows(0).Item("CARDEXP").ToString()
                            CARDTYPE = dsCus.Tables(0).Rows(0).Item("CARDTYPE").ToString()
                            AUTH_CODE = dsCus.Tables(0).Rows(0).Item("AUTH_CODE").ToString()
                        End If
                        'Query for Insert
                        StrSql = "INSERT INTO ORDERLOG  "
                        StrSql = StrSql + "( "
                        StrSql = StrSql + "ORDERLOGID, "
                        StrSql = StrSql + "REFNUMBER, "
                        StrSql = StrSql + "QTY, "
                        StrSql = StrSql + "SEQQTY, "
                        StrSql = StrSql + "ITEMNUMBER, "
                        StrSql = StrSql + "ITEMDESCRIPTION, "
                        StrSql = StrSql + "DELIVERYFORMAT, "
                        StrSql = StrSql + "USERID, "
                        StrSql = StrSql + "USERNAME, "
                        StrSql = StrSql + "BILLTOUSERID, "
                        StrSql = StrSql + "BILLTOUSERNAME, "
                        StrSql = StrSql + "SHIPLTOUSERID, "
                        StrSql = StrSql + "SHIPTOUSERNAME, "
                        StrSql = StrSql + "CARDHTOUSERID, "
                        StrSql = StrSql + "CARDHTOUSERNAME, "
                        StrSql = StrSql + "WEBCONFATTUSERID, "
                        StrSql = StrSql + "WEBCONFATTUSERNAME, "
                        StrSql = StrSql + "UNITCOST, "
                        StrSql = StrSql + "SUBTOTAL, "
                        StrSql = StrSql + "SALESTAX, "
                        StrSql = StrSql + "TOTAL, "
                        StrSql = StrSql + "PAYMENTTYPE, "
                        StrSql = StrSql + "CARDNUMBER, "
                        StrSql = StrSql + "CARDEXP, "
                        StrSql = StrSql + "CARDTYPE, "
                        StrSql = StrSql + "AUTH_CODE "
                        StrSql = StrSql + ") "
                        StrSql = StrSql + "SELECT SEQORDERLOGID.NEXTVAl, "
                        StrSql = StrSql + "'" + REFNUMBER + "', "
                        StrSql = StrSql + QTY + ", "
                        StrSql = StrSql + dsInv.Tables(0).Rows(i).Item("QTYSEQ").ToString() + ", "
                        StrSql = StrSql + "'" + ITEMNUMBER + "', "
                        StrSql = StrSql + "'" + ITEMDESCRIPTION + "', "
                        StrSql = StrSql + "'" + DELIVERYFORMAT + "', "
                        StrSql = StrSql + UserId + ", "
                        StrSql = StrSql + "'" + userName + "', "
                        StrSql = StrSql + BILLTOUSERID + ", "
                        StrSql = StrSql + "'" + BILLTOUSERNAME + "', "
                        StrSql = StrSql + SHIPLTOUSERID + ", "
                        StrSql = StrSql + "'" + SHIPTOUSERNAME + "', "
                        StrSql = StrSql + CARDHTOUSERID + ", "
                        StrSql = StrSql + "'" + CARDHTOUSERNAME + "', "
                        StrSql = StrSql + WEBCONFATTUSERID + ", "
                        StrSql = StrSql + "'" + WEBCONFATTUSERNAME + "', "
                        StrSql = StrSql + UNITCOST + ", "
                        StrSql = StrSql + SUBTOTAL + ", "
                        StrSql = StrSql + SALESTAX + ", "
                        StrSql = StrSql + TOTAL + ", "
                        StrSql = StrSql + "'" + PAYMENTTYPE + "', "
                        StrSql = StrSql + "'" + CARDNUMBER + "', "
                        StrSql = StrSql + "'" + CARDEXP + "', "
                        StrSql = StrSql + "'" + CARDTYPE + "', "
                        StrSql = StrSql + "'" + AUTH_CODE + "'"
                        StrSql = StrSql + "FROM DUAL "

                        odbUtil.UpIns(StrSql, LogConnection)
                    Next
                End If



            Catch ex As Exception
                Throw New Exception("ShopUpInsData:InsertCustomerInfo:" + ex.Message.ToString())
            End Try

        End Sub
        Public Sub UpdateWCOrderLog(ByVal RefNo As String, ByVal UserId As String, ByVal userName As String)
            Dim odbUtil As New DBUtil()
            Dim objCrypt As New CryptoHelper()
            Dim StrSql As String
            Dim objGetData As New ShopGetData.Selectdata()
            Dim objWGetData As New WebConf.Selectdata()
            Dim dsInv As New DataSet()
            Dim dsCus As New DataSet()
            Dim arrOrderLog(50) As String
            Dim objGetUData As New UsersGetData.Selectdata()
            Dim REFNUMBER As String = ""
            Dim QTY As String = ""
            Dim ITEMNUMBER As String = ""
            Dim BILLTOUSERID As String = ""
            Dim BILLTOUSERNAME As String = ""
            Dim SHIPLTOUSERID As String = ""

            Dim SHIPTOUSERNAME As String = ""
            Dim CARDHTOUSERID As String = ""
            Dim CARDHTOUSERNAME As String = ""
            Dim WEBCONFATTUSERID As String = ""
            Dim WEBCONFATTUSERNAME As String = ""

            Dim UNITCOST As String = ""
            Dim SUBTOTAL As String = ""
            Dim SALESTAX As String = ""
            Dim TOTAL As String = ""
            Dim PAYMENTTYPE As String = ""
            Dim CARDNUMBER As String = ""
            Dim CARDEXP As String = ""
            Dim CARDTYPE As String = ""
            Dim AUTH_CODE As String = ""
            Dim ITEMDESCRIPTION As String = ""
            Dim DELIVERYFORMAT As String = ""
            Dim dsAtt As New DataSet()

            Dim i As Integer = 0
            Dim j As Integer = 0

            Try
                dsInv = objGetData.GetOrderReviewAShop(RefNo)
                dsCus = objGetData.GetCustInfo(RefNo)
                dsAtt = objWGetData.GetWebConfAttDetails(RefNo)

                If dsInv.Tables(0).Rows.Count > 0 Then
                    For i = 0 To dsInv.Tables(0).Rows.Count - 1
                        REFNUMBER = RefNo.ToString()
                        QTY = dsInv.Tables(0).Rows(i).Item("QTY").ToString()
                        ITEMNUMBER = dsInv.Tables(0).Rows(i).Item("ITEMNUMBER").ToString()
                        ITEMDESCRIPTION = dsInv.Tables(0).Rows(i).Item("ITEMDESCRIPTION").ToString()
                        DELIVERYFORMAT = dsInv.Tables(0).Rows(i).Item("DELIVERYFORMAT").ToString()

                        UserId = UserId.ToString()
                        userName = objGetUData.GetAddresNameByID(objGetUData.GetUserAddressIDByHeadID("Login User", UserId)) 'userName.ToString()
                        BILLTOUSERID = dsInv.Tables(0).Rows(0).Item("BILLTOID").ToString()
                        BILLTOUSERNAME = objGetUData.GetAddresNameByID(dsInv.Tables(0).Rows(0).Item("BILLTOID").ToString())
                        SHIPLTOUSERID = "0"
                        SHIPTOUSERNAME = ""
                        CARDHTOUSERID = dsInv.Tables(0).Rows(0).Item("CARDHID").ToString()
                        CARDHTOUSERNAME = objGetUData.GetAddresNameByID(dsInv.Tables(0).Rows(0).Item("CARDHID").ToString())

                        UNITCOST = dsInv.Tables(0).Rows(i).Item("UNITCOST").ToString()
                        SUBTOTAL = dsInv.Tables(0).Rows(i).Item("SUBTOTAL").ToString()
                        SALESTAX = dsInv.Tables(0).Rows(i).Item("SALESTAX").ToString()
                        TOTAL = dsInv.Tables(0).Rows(i).Item("TOTAL").ToString()

                        If dsCus.Tables(0).Rows.Count > 0 Then
                            PAYMENTTYPE = dsCus.Tables(0).Rows(0).Item("PAYMENTTYPE").ToString()
                            If dsCus.Tables(0).Rows(0).Item("CARDNUMBER").ToString() = "" Then
                                CARDNUMBER = ""
                            Else
                                CARDNUMBER = objCrypt.Encrypt(dsCus.Tables(0).Rows(0).Item("CARDNUMBER").ToString())
                            End If

                            CARDEXP = dsCus.Tables(0).Rows(0).Item("CARDEXP").ToString()
                            CARDTYPE = dsCus.Tables(0).Rows(0).Item("CARDTYPE").ToString()
                            AUTH_CODE = dsCus.Tables(0).Rows(0).Item("AUTH_CODE").ToString()
                        End If

                        'Getting WebConferenceID
                        For j = 0 To dsAtt.Tables(0).Rows.Count - 1
                            WEBCONFATTUSERID = dsAtt.Tables(0).Rows(j).Item("ADDRESSID").ToString()
                            WEBCONFATTUSERNAME = objGetUData.GetAddresNameByID(WEBCONFATTUSERID)
                            'Query for Insert
                            StrSql = "INSERT INTO ORDERLOG  "
                            StrSql = StrSql + "( "
                            StrSql = StrSql + "ORDERLOGID, "
                            StrSql = StrSql + "REFNUMBER, "
                            StrSql = StrSql + "QTY, "
                            StrSql = StrSql + "SEQQTY, "
                            StrSql = StrSql + "ITEMNUMBER, "
                            StrSql = StrSql + "ITEMDESCRIPTION, "
                            StrSql = StrSql + "DELIVERYFORMAT, "
                            StrSql = StrSql + "USERID, "
                            StrSql = StrSql + "USERNAME, "
                            StrSql = StrSql + "BILLTOUSERID, "
                            StrSql = StrSql + "BILLTOUSERNAME, "
                            StrSql = StrSql + "SHIPLTOUSERID, "
                            StrSql = StrSql + "SHIPTOUSERNAME, "
                            StrSql = StrSql + "CARDHTOUSERID, "
                            StrSql = StrSql + "CARDHTOUSERNAME, "
                            StrSql = StrSql + "WEBCONFATTUSERID, "
                            StrSql = StrSql + "WEBCONFATTUSERNAME, "
                            StrSql = StrSql + "UNITCOST, "
                            StrSql = StrSql + "SUBTOTAL, "
                            StrSql = StrSql + "SALESTAX, "
                            StrSql = StrSql + "TOTAL, "
                            StrSql = StrSql + "PAYMENTTYPE, "
                            StrSql = StrSql + "CARDNUMBER, "
                            StrSql = StrSql + "CARDEXP, "
                            StrSql = StrSql + "CARDTYPE, "
                            StrSql = StrSql + "AUTH_CODE "
                            StrSql = StrSql + ") "
                            StrSql = StrSql + "SELECT SEQORDERLOGID.NEXTVAl, "
                            StrSql = StrSql + "'" + REFNUMBER + "', "
                            StrSql = StrSql + QTY + ", "
                            StrSql = StrSql + dsAtt.Tables(0).Rows(j).Item("SEQ").ToString() + ", "
                            StrSql = StrSql + "'" + ITEMNUMBER.Replace("'", "''") + "', "
                            StrSql = StrSql + "'" + ITEMDESCRIPTION.Replace("'", "''") + "', "
                            StrSql = StrSql + "'" + DELIVERYFORMAT.Replace("'", "''") + "', "
                            StrSql = StrSql + UserId + ", "
                            StrSql = StrSql + "'" + userName.Replace("'", "''") + "', "
                            StrSql = StrSql + BILLTOUSERID + ", "
                            StrSql = StrSql + "'" + BILLTOUSERNAME.Replace("'", "''") + "', "
                            StrSql = StrSql + SHIPLTOUSERID + ", "
                            StrSql = StrSql + "'" + SHIPTOUSERNAME.Replace("'", "''") + "', "
                            StrSql = StrSql + CARDHTOUSERID + ", "
                            StrSql = StrSql + "'" + CARDHTOUSERNAME.Replace("'", "''") + "', "
                            StrSql = StrSql + WEBCONFATTUSERID + ", "
                            StrSql = StrSql + "'" + WEBCONFATTUSERNAME.Replace("'", "''") + "', "
                            StrSql = StrSql + UNITCOST + ", "
                            StrSql = StrSql + SUBTOTAL + ", "
                            StrSql = StrSql + SALESTAX + ", "
                            StrSql = StrSql + TOTAL + ", "
                            StrSql = StrSql + "'" + PAYMENTTYPE + "', "
                            StrSql = StrSql + "'" + CARDNUMBER + "', "
                            StrSql = StrSql + "'" + CARDEXP + "', "
                            StrSql = StrSql + "'" + CARDTYPE + "', "
                            StrSql = StrSql + "'" + AUTH_CODE + "'"
                            StrSql = StrSql + "FROM DUAL "
                            odbUtil.UpIns(StrSql, LogConnection)
                        Next
                    Next
                End If

            Catch ex As Exception
                Throw New Exception("ShopUpInsData:UpdateWCOrderLog:" + ex.Message.ToString())
            End Try
        End Sub


        Public Sub UpdateCustomerCardInfo(ByVal RefNo As String, ByVal CardNumber As String, ByVal CardExp As String, ByVal CardType As String, ByVal UserId As String, ByVal payType As String, ByVal Auth_Code As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String
            Dim objUGetData As New UsersGetData.Selectdata()
            Dim dsUser As New DataSet()
            Try
                dsUser = objUGetData.GetUserDetails(UserId)

                'TO UPDATE THE CUSTOMER TABLE 
                StrSql = "UPDATE CUSTOMERINFO  "
                StrSql = StrSql + "SET CARDNUMBER='" + CardNumber + "', CARDEXP='" + CardExp + "',CARDTYPE='" + CardType + "',PAYMENTTYPE= '" + payType + "',AUTH_CODE=" + Auth_Code + " "
                StrSql = StrSql + " WHERE REFNUMBER ='" + RefNo + "' AND  USERCONTRACTID = " + dsUser.Tables(0).Rows(0).Item("USERCONTACTID").ToString() + " "
                odbUtil.UpIns(StrSql, ShoppingConnection)

            Catch ex As Exception
                Throw New Exception("ShopUpInsData:InsertCustomerInfo:" + ex.Message.ToString())
            End Try
        End Sub

#End Region
    End Class
  
End Class
