Imports Microsoft.VisualBasic
Imports System.Web.SessionState
Imports System.Data
Imports System.Data.OleDb
Imports System
Imports PackProdGetData
Imports System.Math
Imports System.Web.UI.HtmlTextWriter


Public Class PackProdUpInsData
    Public Class UpdateInsert
        Dim PackProdConnection As String = System.Configuration.ConfigurationManager.AppSettings("PackProdConnectionString")
        Public Sub UpdateSelectionData(ByVal company As String, ByVal product As String, ByVal country As String, ByVal state As String, ByVal service As String, ByVal design As String, ByVal process As String, ByVal machine As String, ByVal customer As String, ByVal userName As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "UPDATE SELECTION SET COMPANY ='" + company + "', "
                StrSql = StrSql + " PRODUCT ='" + product + "', "
                StrSql = StrSql + " COUNTRY ='" + country + "', "
                StrSql = StrSql + " STATE ='" + state + "', "
                StrSql = StrSql + " SERVICE ='" + service + "', "
                StrSql = StrSql + " DESIGN ='" + design + "', "
                StrSql = StrSql + " PROCESS ='" + process + "', "
                StrSql = StrSql + " MACHINE ='" + machine + "', "
                StrSql = StrSql + " CUSTOMER ='" + customer + "' "
                StrSql = StrSql + " WHERE UPPER(USERNAME) ='" + userName.ToUpper() + "'"
                odbUtil.UpIns(StrSql, PackProdConnection)

            Catch ex As Exception
                Throw New Exception("PackProdUpdate:UpdateSelectionData:" + ex.Message.ToString())

            End Try

        End Sub

        Public Sub UpdateResultsData(ByVal company As String, ByVal product As String, ByVal Country As String, ByVal state As String, ByVal service As String, ByVal design As String, ByVal process As String, ByVal machine As String, ByVal customer As String, ByVal userName As String, ByVal UserId As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim Dts, dsState, dsProdA, dsProdServ, dsProdDevCap, dsProc, dsMac, dsRepCus, dsCountry As New DataSet
            Dim objGetData As New PackProdGetData.Selectdata()
            Dim i As Integer
            Try
                'Getting Data from Selection table for Users
                Dts = objGetData.GetSelectionDataByUser(userName)

                'Deleting Privious Results
                DeleteResults(userName)

                'Insert for selected company
                If Dts.Tables(0).Rows.Count > 0 Then
                    If Dts.Tables(0).Rows(0).Item("company").ToString() <> "0" Then
                        StrSql = "INSERT INTO RESULTS (USERNAME,COMPANY,CRITERIA) VALUES('" + userName.ToUpper() + "','" + Dts.Tables(0).Rows(0).Item("company").ToString() + "','company')"
                        odbUtil.UpIns(StrSql, PackProdConnection)
                    End If
                End If

                'Insert for selected Country
                dsCountry = objGetData.GetCompanyDetailsByCountry(Country)
                If dsCountry.Tables(0).Rows.Count > 0 Then
                    For i = 0 To dsCountry.Tables(0).Rows.Count - 1
                        If Dts.Tables(0).Rows(0).Item("Country").ToString() <> "0" Then
                            StrSql = "INSERT INTO RESULTS (USERNAME,COMPANY,CRITERIA,COMPANYNAME) VALUES('" + userName.ToUpper() + "','" + dsCountry.Tables(0).Rows(i).Item("ID").ToString() + "','country','" + dsCountry.Tables(0).Rows(i).Item("NAME").ToString() + "')"
                            odbUtil.UpIns(StrSql, PackProdConnection)
                        End If
                    Next

                End If

                'Insert for selected State
                dsState = objGetData.GetCompanyDetailsByState(state)
                If dsState.Tables(0).Rows.Count > 0 Then
                    For i = 0 To dsState.Tables(0).Rows.Count - 1
                        If Dts.Tables(0).Rows(0).Item("state").ToString() <> "0" Then
                            StrSql = "INSERT INTO RESULTS (USERNAME,COMPANY,CRITERIA,COMPANYNAME) VALUES('" + userName.ToUpper() + "','" + dsState.Tables(0).Rows(i).Item("ID").ToString() + "','state','" + dsState.Tables(0).Rows(i).Item("NAME").ToString() + "')"
                            odbUtil.UpIns(StrSql, PackProdConnection)
                        End If
                    Next

                End If

                'Insert for Selected Product Area
                dsProdA = objGetData.GetCompanyDetailsByCategory(product, UserId)
                If dsProdA.Tables(0).Rows.Count > 0 Then
                    For i = 0 To dsProdA.Tables(0).Rows.Count - 1
                        StrSql = "INSERT INTO RESULTS (USERNAME,COMPANY,CRITERIA,COMPANYNAME) VALUES('" + userName.ToUpper() + "','" + dsProdA.Tables(0).Rows(i).Item("COMPANYID").ToString() + "','product',REPLACE('" + dsProdA.Tables(0).Rows(i).Item("NAME").ToString() + "','##',''''))"
                        odbUtil.UpIns(StrSql, PackProdConnection)
                    Next

                End If

                'Insert for Selected Product Service
                dsProdServ = objGetData.GetCompanyDetailsByCategory(service, UserId)
                If dsProdServ.Tables(0).Rows.Count > 0 Then
                    For i = 0 To dsProdServ.Tables(0).Rows.Count - 1
                        StrSql = "INSERT INTO RESULTS (USERNAME,COMPANY,CRITERIA,COMPANYNAME) VALUES('" + userName.ToUpper() + "','" + dsProdServ.Tables(0).Rows(i).Item("COMPANYID").ToString() + "','SERVICE',REPLACE('" + dsProdServ.Tables(0).Rows(i).Item("NAME").ToString() + "','##',''''))"
                        odbUtil.UpIns(StrSql, PackProdConnection)
                    Next

                End If

                'Insert for Selected Product Development Capabilities
                dsProdDevCap = objGetData.GetCompanyDetailsByCategory(design, UserId)
                If dsProdDevCap.Tables(0).Rows.Count > 0 Then
                    For i = 0 To dsProdDevCap.Tables(0).Rows.Count - 1
                        StrSql = "INSERT INTO RESULTS (USERNAME,COMPANY,CRITERIA,COMPANYNAME) VALUES('" + userName.ToUpper() + "','" + dsProdDevCap.Tables(0).Rows(i).Item("COMPANYID").ToString() + "','DESIGN',REPLACE('" + dsProdDevCap.Tables(0).Rows(i).Item("NAME").ToString() + "','##',''''))"
                        odbUtil.UpIns(StrSql, PackProdConnection)
                    Next

                End If

                'Insert for Selected Processing Capabilities
                dsProc = objGetData.GetCompanyDetailsByCategory(process, UserId)
                If dsProc.Tables(0).Rows.Count > 0 Then
                    For i = 0 To dsProc.Tables(0).Rows.Count - 1
                        StrSql = "INSERT INTO RESULTS (USERNAME,COMPANY,CRITERIA,COMPANYNAME) VALUES('" + userName.ToUpper() + "','" + dsProc.Tables(0).Rows(i).Item("COMPANYID").ToString() + "','PROCESS',REPLACE('" + dsProc.Tables(0).Rows(i).Item("NAME").ToString() + "','##',''''))"
                        odbUtil.UpIns(StrSql, PackProdConnection)
                    Next

                End If

                'Insert for Selected Machinery Systems
                dsMac = objGetData.GetCompanyDetailsByCategory(machine, UserId)
                If dsMac.Tables(0).Rows.Count > 0 Then
                    For i = 0 To dsMac.Tables(0).Rows.Count - 1
                        StrSql = "INSERT INTO RESULTS (USERNAME,COMPANY,CRITERIA,COMPANYNAME) VALUES('" + userName.ToUpper() + "','" + dsMac.Tables(0).Rows(i).Item("COMPANYID").ToString() + "','MACHINE',REPLACE('" + dsMac.Tables(0).Rows(i).Item("NAME").ToString() + "','##',''''))"
                        odbUtil.UpIns(StrSql, PackProdConnection)
                    Next

                End If

                'Insert for Representative Customers
                dsRepCus = objGetData.GetCompanyDetailsByCategory(customer, UserId)
                If dsRepCus.Tables(0).Rows.Count > 0 Then
                    For i = 0 To dsRepCus.Tables(0).Rows.Count - 1
                        StrSql = "INSERT INTO RESULTS (USERNAME,COMPANY,CRITERIA,COMPANYNAME) VALUES('" + userName.ToUpper() + "','" + dsRepCus.Tables(0).Rows(i).Item("COMPANYID").ToString() + "','CUSTOMER',REPLACE('" + dsRepCus.Tables(0).Rows(i).Item("NAME").ToString() + "','##',''''))"
                        odbUtil.UpIns(StrSql, PackProdConnection)
                    Next

                End If



                UpdateUserResultsData(product, state, service, design, process, machine, customer, userName, UserId)


            Catch ex As Exception
                Throw New Exception("PackProdUpdate:UpdateResultsData:" + ex.Message.ToString())

            End Try

        End Sub

        Public Sub UpdateUserResultsData(ByVal product As String, ByVal state As String, ByVal service As String, ByVal design As String, ByVal process As String, ByVal machine As String, ByVal customer As String, ByVal userName As String, ByVal UserId As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim Dts, dsState, dsProdA, dsProdServ, dsProdDevCap, dsProc, dsMac, dsRepCus As New DataSet
            Dim objGetData As New PackProdGetData.Selectdata()
            Dim i As Integer
            Try
                'Getting Data from Selection table for Users
                Dts = objGetData.GetSelectionDataByUser(userName)



                'Insert for Selected Product Area
                dsProdA = objGetData.GetUserCategoryDetailsByDetialId(product, UserId)
                If dsProdA.Tables(0).Rows.Count > 0 Then
                    For i = 0 To dsProdA.Tables(0).Rows.Count - 1
                        StrSql = "INSERT INTO RESULTS (USERNAME,COMPANY,CRITERIA,COMPANYNAME) VALUES('" + userName.ToUpper() + "','" + dsProdA.Tables(0).Rows(i).Item("COMPANYID").ToString() + "','product',REPLACE('" + dsProdA.Tables(0).Rows(i).Item("NAME").ToString() + "','##',''''))"
                        odbUtil.UpIns(StrSql, PackProdConnection)
                    Next

                End If

                'Insert for Selected Product Service
                dsProdServ = objGetData.GetUserCategoryDetailsByDetialId(service, UserId)
                If dsProdServ.Tables(0).Rows.Count > 0 Then
                    For i = 0 To dsProdServ.Tables(0).Rows.Count - 1
                        StrSql = "INSERT INTO RESULTS (USERNAME,COMPANY,CRITERIA,COMPANYNAME) VALUES('" + userName.ToUpper() + "','" + dsProdServ.Tables(0).Rows(i).Item("COMPANYID").ToString() + "','SERVICE',REPLACE('" + dsProdServ.Tables(0).Rows(i).Item("NAME").ToString() + "','##',''''))"
                        odbUtil.UpIns(StrSql, PackProdConnection)
                    Next

                End If

                'Insert for Selected Product Development Capabilities
                dsProdDevCap = objGetData.GetUserCategoryDetailsByDetialId(design, UserId)
                If dsProdDevCap.Tables(0).Rows.Count > 0 Then
                    For i = 0 To dsProdDevCap.Tables(0).Rows.Count - 1
                        StrSql = "INSERT INTO RESULTS (USERNAME,COMPANY,CRITERIA,COMPANYNAME) VALUES('" + userName.ToUpper() + "','" + dsProdDevCap.Tables(0).Rows(i).Item("COMPANYID").ToString() + "','DESIGN',REPLACE('" + dsProdDevCap.Tables(0).Rows(i).Item("NAME").ToString() + "','##',''''))"
                        odbUtil.UpIns(StrSql, PackProdConnection)
                    Next

                End If

                'Insert for Selected Processing Capabilities
                dsProc = objGetData.GetUserCategoryDetailsByDetialId(process, UserId)
                If dsProc.Tables(0).Rows.Count > 0 Then
                    For i = 0 To dsProc.Tables(0).Rows.Count - 1
                        StrSql = "INSERT INTO RESULTS (USERNAME,COMPANY,CRITERIA,COMPANYNAME) VALUES('" + userName.ToUpper() + "','" + dsProc.Tables(0).Rows(i).Item("COMPANYID").ToString() + "','PROCESS',REPLACE('" + dsProc.Tables(0).Rows(i).Item("NAME").ToString() + "','##',''''))"
                        odbUtil.UpIns(StrSql, PackProdConnection)
                    Next

                End If

                'Insert for Selected Machinery Systems
                dsMac = objGetData.GetUserCategoryDetailsByDetialId(machine, UserId)
                If dsMac.Tables(0).Rows.Count > 0 Then
                    For i = 0 To dsMac.Tables(0).Rows.Count - 1
                        StrSql = "INSERT INTO RESULTS (USERNAME,COMPANY,CRITERIA,COMPANYNAME) VALUES('" + userName.ToUpper() + "','" + dsMac.Tables(0).Rows(i).Item("COMPANYID").ToString() + "','MACHINE',REPLACE('" + dsMac.Tables(0).Rows(i).Item("NAME").ToString() + "','##',''''))"
                        odbUtil.UpIns(StrSql, PackProdConnection)
                    Next

                End If

                'Insert for Representative Customers
                dsRepCus = objGetData.GetUserCategoryDetailsByDetialId(customer, UserId)
                If dsRepCus.Tables(0).Rows.Count > 0 Then
                    For i = 0 To dsRepCus.Tables(0).Rows.Count - 1
                        StrSql = "INSERT INTO RESULTS (USERNAME,COMPANY,CRITERIA,COMPANYNAME) VALUES('" + userName.ToUpper() + "','" + dsRepCus.Tables(0).Rows(i).Item("COMPANYID").ToString() + "','CUSTOMER',REPLACE('" + dsRepCus.Tables(0).Rows(i).Item("NAME").ToString() + "','##',''''))"
                        odbUtil.UpIns(StrSql, PackProdConnection)
                    Next

                End If






            Catch ex As Exception
                Throw New Exception("PackProdUpdate:UpdateResultsData:" + ex.Message.ToString())

            End Try

        End Sub

        Public Sub KeywordSearch(ByVal UserName As String, ByVal Keyword As String, ByVal UserId As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                'Deleting Privious Results
                DeleteResults(UserName)
                Keyword = Keyword.Replace("'", "''")
                'Inserting Record for keyword search in RESULT table
                StrSql = "INSERT INTO RESULTS  "
                StrSql = StrSql + "(USERNAME,COMPANY,CRITERIA) "
                StrSql = StrSql + "SELECT  '" + UserName.ToUpper() + "', "
                StrSql = StrSql + "CMP.COMPANYID, "
                StrSql = StrSql + "'keyword' "
                StrSql = StrSql + "FROM "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT CC.COMPANYID "
                StrSql = StrSql + "FROM CATEGORYDETAILS CD "
                StrSql = StrSql + "INNER JOIN COMPANYCATEGORIES CC "
                StrSql = StrSql + "ON CC.CATEGORYDETAILID = CD.CATEGORYDETAILID "
                StrSql = StrSql + "WHERE CONTAINS(CD.DETAILS,'BT(""" + Keyword + """,3,packaging)',1) >0 "
                StrSql = StrSql + "OR CONTAINS(CD.DETAILS,'$""" + Keyword + """',2) > 0 "
                StrSql = StrSql + "OR CONTAINS(CD.DETAILS,'RT(""" + Keyword + """,packaging)',3) >0 "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT CC.COMPANYID "
                StrSql = StrSql + "FROM COMPANYCATEGORIES CC "
                StrSql = StrSql + "WHERE CONTAINS(CC.DETAILS,'BT(""" + Keyword + """,3,packaging)',1) >0 "
                StrSql = StrSql + "OR CONTAINS(CC.DETAILS,'$""" + Keyword + """',2) > 0 "
                StrSql = StrSql + "OR CONTAINS(CC.DETAILS,'RT(""" + Keyword + """,packaging)',3) >0 "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT CCC.COMPANYID "
                StrSql = StrSql + "FROM COMPANYCATEGORYCOMM CCC "
                StrSql = StrSql + "WHERE CONTAINS(CCC.COMMENTS,'BT(""" + Keyword + """,3,packaging)',1) >0 "
                StrSql = StrSql + "OR CONTAINS(CCC.COMMENTS,'$""" + Keyword + """',2) > 0 "
                StrSql = StrSql + "OR CONTAINS(CCC.COMMENTS,'RT(""" + Keyword + """,packaging)',3) >0 "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT COMP.COMPANYID  "
                StrSql = StrSql + "FROM COMPANIES COMP "
                StrSql = StrSql + "INNER JOIN ADMINSITE.STATE "
                StrSql = StrSql + "ON STATE.STATEID = COMP. STATEID "
                StrSql = StrSql + "WHERE CONTAINS(STATE.NAME,'BT(""" + Keyword + """,3,packaging)',1) >0 "
                StrSql = StrSql + "OR CONTAINS(STATE.NAME,'$""" + Keyword + """',2) > 0 "
                StrSql = StrSql + "OR CONTAINS(STATE.NAME,'RT(""" + Keyword + """,packaging)',3) >0 "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT COMP.COMPANYID "
                StrSql = StrSql + "FROM COMPANIES COMP "
                StrSql = StrSql + "WHERE CONTAINS(COMP.NAME,'BT(""" + Keyword + """,3,packaging)',1) >0 "
                StrSql = StrSql + "OR CONTAINS(COMP.NAME,'$""" + Keyword + """',2) > 0 "
                StrSql = StrSql + "OR CONTAINS(COMP.NAME,'RT(""" + Keyword + """,packaging)',3) >0 "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT CATDET1.COMPANYID  "
                StrSql = StrSql + "FROM "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT UCC.COMPANYID, "
                StrSql = StrSql + "UCC.USERID "
                StrSql = StrSql + "FROM CATEGORYDETAILS UCD "
                StrSql = StrSql + "INNER JOIN USERCOMPANYCATEGORIES UCC "
                StrSql = StrSql + "ON UCD.CATEGORYDETAILID = UCC.CATEGORYDETAILID "
                StrSql = StrSql + "WHERE CONTAINS(UCD.DETAILS,'BT(""" + Keyword + """,3,packaging)',1) >0 "
                StrSql = StrSql + "OR CONTAINS(UCD.DETAILS,'$""" + Keyword + """',2) > 0 "
                StrSql = StrSql + "OR CONTAINS(UCD.DETAILS,'RT(""" + Keyword + """,packaging)',3) >0 "
                StrSql = StrSql + ") CATDET1 "
                StrSql = StrSql + "WHERE USERID = " + UserId + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT CATDET2.COMPANYID "
                StrSql = StrSql + "FROM "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT UCC.COMPANYID, "
                StrSql = StrSql + "UCC.USERID "
                StrSql = StrSql + "FROM USERCOMPANYCATEGORIES UCC "
                StrSql = StrSql + "WHERE CONTAINS(UCC.DETAILS,'BT(""" + Keyword + """,3,packaging)',1) >0 "
                StrSql = StrSql + "OR CONTAINS(UCC.DETAILS,'$""" + Keyword + """',2) > 0 "
                StrSql = StrSql + "OR CONTAINS(UCC.DETAILS,'RT(""" + Keyword + """,packaging)',3) >0 "
                StrSql = StrSql + ") CATDET2 "
                StrSql = StrSql + "WHERE USERID = " + UserId + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT CATDET3.COMPANYID "
                StrSql = StrSql + "FROM "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT UCCC.COMPANYID, "
                StrSql = StrSql + "UCCC.USERID "
                StrSql = StrSql + "FROM USERCOMPANYCATEGORYCOMM UCCC "
                StrSql = StrSql + "WHERE CONTAINS(UCCC.COMMENTS,'BT(""" + Keyword + """,3,packaging)',1) >0 "
                StrSql = StrSql + "OR CONTAINS(UCCC.COMMENTS,'$""" + Keyword + """',2) > 0 "
                StrSql = StrSql + "OR CONTAINS(UCCC.COMMENTS,'RT(""" + Keyword + """,packaging)',3) >0 "
                StrSql = StrSql + ") CATDET3 "
                StrSql = StrSql + "WHERE USERID = " + UserId + ""
                StrSql = StrSql + ") CMP "
                StrSql = StrSql + "INNER JOIN COMPANIES "
                StrSql = StrSql + "ON COMPANIES.COMPANYID = CMP.COMPANYID "
                StrSql = StrSql + "INNER JOIN USERCOUNTRIES "
                StrSql = StrSql + "ON USERCOUNTRIES.COUNTRYID = COMPANIES.COUNTRYID "
                StrSql = StrSql + "WHERE USERCOUNTRIES.USERID = " + UserId + ""

                odbUtil.UpIns(StrSql, PackProdConnection)

            Catch ex As Exception
                Throw New Exception("PackProdUpdate:KeywordSearch:" + ex.Message.ToString())

            End Try

        End Sub

        Public Sub DeleteResults(ByVal UserName As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try

                'Deleting Data from Results Tabel
                StrSql = "DELETE FROM RESULTS "
                StrSql = StrSql + " WHERE UPPER(USERNAME) ='" + UserName.ToUpper() + "' "
                odbUtil.UpIns(StrSql, PackProdConnection)

            Catch ex As Exception
                Throw New Exception("PackProdUpdate:DeleteResults:" + ex.Message.ToString())

            End Try

        End Sub


        Public Sub InsUpdateUserCompanyCatComm(ByVal CompanycategorycommId As String, ByVal CategoryId As String, ByVal CompanyId As String, ByVal UserId As String, ByVal Comments As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                Comments = Comments.Replace("'", "''")
                If CompanycategorycommId = 0 Then
                    If Comments.Trim.Length > 0 Then
                        StrSql = "INSERT INTO USERCOMPANYCATEGORYCOMM  "
                        StrSql = StrSql + "(COMPANYCATEGORYCOMMID,CATEGORYID,COMPANYID,USERID,COMMENTS) "
                        StrSql = StrSql + "VALUES "
                        StrSql = StrSql + "(USERCOMPCATEGORYCOMMIDSEQ.NEXTVAL," + CategoryId + "," + CompanyId + "," + UserId + ",'" + Comments + "') "
                    End If
                Else
                    If Comments.Trim.Length > 0 Then
                        StrSql = "UPDATE USERCOMPANYCATEGORYCOMM SET "
                        StrSql = StrSql + "COMMENTS ='" + Comments + "' "
                        StrSql = StrSql + "WHERE COMPANYCATEGORYCOMMID =" + CompanycategorycommId + " "
                    Else
                        StrSql = "DELETE FROM USERCOMPANYCATEGORYCOMM "
                        StrSql = StrSql + "WHERE COMPANYCATEGORYCOMMID =" + CompanycategorycommId + " "
                    End If

                End If


                odbUtil.UpIns(StrSql, PackProdConnection)

            Catch ex As Exception
                Throw New Exception("PackProdUpdate:DeleteResults:" + ex.Message.ToString())

            End Try

        End Sub

        Public Sub InsUpdateUserCompanyCatDetails(ByVal CategoryDetailId() As String, ByVal CategoryId As String, ByVal CompanyId As String, ByVal UserId As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim DetailId As String = String.Empty
            Dim i As New Integer
            Try

                StrSql = "DELETE FROM USERCOMPANYCATEGORIES "
                StrSql = StrSql + "WHERE COMPANYID =" + CompanyId + " "
                StrSql = StrSql + "AND CATEGORYID =" + CategoryId + " "
                StrSql = StrSql + "AND USERID =" + UserId + " "
                odbUtil.UpIns(StrSql, PackProdConnection)

                For i = 0 To CategoryDetailId.Length - 1
                    If CInt(CategoryDetailId(i)) <> CInt(0) Then
                        StrSql = "INSERT INTO USERCOMPANYCATEGORIES  "
                        StrSql = StrSql + "(COMPANYCATEGORYID,CATEGORYID,COMPANYID,CATEGORYDETAILID,USERID,UPDATEDDATETIME) "
                        StrSql = StrSql + "SELECT USERCOMPANYCATEGORYIDSEQ.NEXTVAL, "
                        StrSql = StrSql + "" + CategoryId + ", "
                        StrSql = StrSql + "" + CompanyId + ", "
                        StrSql = StrSql + "" + CategoryDetailId(i) + ", "
                        StrSql = StrSql + "" + UserId + ", "
                        StrSql = StrSql + "SYSDATE "
                        StrSql = StrSql + "FROM DUAL "
                        StrSql = StrSql + "WHERE NOT EXISTS (SELECT 1 "
                        StrSql = StrSql + "FROM USERCOMPANYCATEGORIES "
                        StrSql = StrSql + "WHERE CATEGORYID = " + CategoryId + " "
                        StrSql = StrSql + "AND CATEGORYDETAILID = " + CategoryDetailId(i) + " "
                        StrSql = StrSql + "AND COMPANYID =" + CompanyId + " "
                        StrSql = StrSql + "AND USERID = " + UserId + ") "
                        odbUtil.UpIns(StrSql, PackProdConnection)
                    End If
                Next





            Catch ex As Exception
                Throw New Exception("PackProdUpdate:DeleteResults:" + ex.Message.ToString())
            End Try
        End Sub

        Public Sub InsUpdateUserCompanyCatDetls(ByVal CompanycategoryId As String, ByVal CategoryId As String, ByVal CompanyId As String, ByVal UserId As String, ByVal Details As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                Details = Details.Replace("'", "''")
                If CompanycategoryId = 0 Then
                    StrSql = "INSERT INTO USERCOMPANYCATEGORIES  "
                    StrSql = StrSql + "(COMPANYCATEGORYID,CATEGORYID,COMPANYID,USERID,DETAILS) "
                    StrSql = StrSql + "VALUES "
                    StrSql = StrSql + "(USERCOMPANYCATEGORYIDSEQ.NEXTVAL," + CategoryId + "," + CompanyId + "," + UserId + ",'" + Details + "') "
                Else
                    If Details.Trim.Length > 0 Then
                        StrSql = "UPDATE USERCOMPANYCATEGORIES SET "
                        StrSql = StrSql + "DETAILS ='" + Details + "' "
                        StrSql = StrSql + "WHERE COMPANYCATEGORYID =" + CompanycategoryId + " "
                    Else
                        StrSql = "DELETE FROM USERCOMPANYCATEGORIES "
                        StrSql = StrSql + "WHERE COMPANYCATEGORYID =" + CompanycategoryId + " "
                    End If

                End If


                odbUtil.UpIns(StrSql, PackProdConnection)

            Catch ex As Exception
                Throw New Exception("PackProdUpdate:DeleteResults:" + ex.Message.ToString())

            End Try

        End Sub

        Public Sub UserRebulidIndex()
            Dim odbUtil As New DBUtil()
            Try
                odbUtil.UserRebuildIndex("PackProd")
            Catch ex As Exception
                Throw New Exception("PackProdUpdate:UserRebulidIndex:" + ex.Message.ToString())
            End Try
        End Sub

       


    End Class


End Class



