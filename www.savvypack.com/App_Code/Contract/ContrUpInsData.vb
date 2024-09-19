Imports Microsoft.VisualBasic
Imports System.Web.SessionState
Imports System.Data
Imports System.Data.OleDb
Imports System
Imports ContrGetData
Imports System.Math
Imports System.Web.UI.HtmlTextWriter


Public Class ContrUpInsData
    Public Class UpdateInsert
        Dim ContractConnection As String = System.Configuration.ConfigurationManager.AppSettings("ContractConnectionString")
		Dim odbUtil As New DBUtil()
		Dim EconConnection As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
		  
        Public Sub UpdateSelectionData(ByVal company As String, ByVal product As String, ByVal country As String, ByVal state As String, ByVal service As String, ByVal design As String, ByVal process As String, ByVal machine As String, ByVal customer As String, ByVal UserId As String)
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
                StrSql = StrSql + " WHERE USERID =" + UserId.ToString() + ""
                odbUtil.UpIns(StrSql, ContractConnection)

            Catch ex As Exception
                Throw New Exception("ContractUpdate:UpdateSelectionData:" + ex.Message.ToString())

            End Try

        End Sub

        Public Sub UpdateResultsData(ByVal company As String, ByVal product As String, ByVal Country As String, ByVal state As String, ByVal service As String, ByVal design As String, ByVal process As String, ByVal machine As String, ByVal customer As String, ByVal UserId As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim Dts, dsState, dsProdA, dsProdServ, dsProdDevCap, dsProc, dsMac, dsRepCus, dsCountry As New DataSet
            Dim objGetData As New ContrGetData.Selectdata()
            Dim i As Integer
            Try
                'Getting Data from Selection table for Users
                Dts = objGetData.GetSelectionDataByUser(UserId)

                'Deleting Privious Results
                DeleteResults(UserId)

                'Insert for selected company
                If Dts.Tables(0).Rows.Count > 0 Then
                    If Dts.Tables(0).Rows(0).Item("company").ToString() <> "0" Then
                        StrSql = "INSERT INTO RESULTS (USERID,COMPANY,CRITERIA) VALUES(" + UserId.ToString() + ",'" + Dts.Tables(0).Rows(0).Item("company").ToString() + "','company')"
                        odbUtil.UpIns(StrSql, ContractConnection)
                    End If
                End If

                'Insert for selected Country
                dsCountry = objGetData.GetCompanyDetailsByCountry(Country, UserId)
                If dsCountry.Tables(0).Rows.Count > 0 Then
                    For i = 0 To dsCountry.Tables(0).Rows.Count - 1
                        If Dts.Tables(0).Rows(0).Item("Country").ToString() <> "0" Then
                            StrSql = "INSERT INTO RESULTS (USERID,COMPANY,CRITERIA,COMPANYNAME) VALUES(" + UserId.ToString() + ",'" + dsCountry.Tables(0).Rows(i).Item("ID").ToString() + "','country','" + dsCountry.Tables(0).Rows(i).Item("NAME").ToString() + "')"
                            odbUtil.UpIns(StrSql, ContractConnection)
                        End If
                    Next

                End If

                'Insert for selected State
                dsState = objGetData.GetCompanyDetailsByState(state)
                If dsState.Tables(0).Rows.Count > 0 Then
                    For i = 0 To dsState.Tables(0).Rows.Count - 1
                        If Dts.Tables(0).Rows(0).Item("state").ToString() <> "0" Then
                            StrSql = "INSERT INTO RESULTS (USERID,COMPANY,CRITERIA,COMPANYNAME) VALUES(" + UserId.ToString() + ",'" + dsState.Tables(0).Rows(i).Item("ID").ToString() + "','state','" + dsState.Tables(0).Rows(i).Item("NAME").ToString() + "')"
                            odbUtil.UpIns(StrSql, ContractConnection)
                        End If
                    Next

                End If

                'Insert for Selected Product Area
                dsProdA = objGetData.GetCompanyDetailsByCategory(Country, product, UserId)
                If dsProdA.Tables(0).Rows.Count > 0 Then
                    For i = 0 To dsProdA.Tables(0).Rows.Count - 1
                        StrSql = "INSERT INTO RESULTS (USERID,COMPANY,CRITERIA,COMPANYNAME) VALUES(" + UserId.ToString() + ",'" + dsProdA.Tables(0).Rows(i).Item("COMPANYID").ToString() + "','product',REPLACE('" + dsProdA.Tables(0).Rows(i).Item("NAME").ToString() + "','##',''''))"
                        odbUtil.UpIns(StrSql, ContractConnection)
                    Next

                End If

                'Insert for Selected Product Service
                dsProdServ = objGetData.GetCompanyDetailsByCategory(Country, service, UserId)
                If dsProdServ.Tables(0).Rows.Count > 0 Then
                    For i = 0 To dsProdServ.Tables(0).Rows.Count - 1
                        StrSql = "INSERT INTO RESULTS (USERID,COMPANY,CRITERIA,COMPANYNAME) VALUES(" + UserId.ToString() + ",'" + dsProdServ.Tables(0).Rows(i).Item("COMPANYID").ToString() + "','SERVICE',REPLACE('" + dsProdServ.Tables(0).Rows(i).Item("NAME").ToString() + "','##',''''))"
                        odbUtil.UpIns(StrSql, ContractConnection)
                    Next

                End If

                'Insert for Selected Product Development Capabilities
                dsProdDevCap = objGetData.GetCompanyDetailsByCategory(Country, design, UserId)
                If dsProdDevCap.Tables(0).Rows.Count > 0 Then
                    For i = 0 To dsProdDevCap.Tables(0).Rows.Count - 1
                        StrSql = "INSERT INTO RESULTS (USERID,COMPANY,CRITERIA,COMPANYNAME) VALUES(" + UserId.ToString() + ",'" + dsProdDevCap.Tables(0).Rows(i).Item("COMPANYID").ToString() + "','DESIGN',REPLACE('" + dsProdDevCap.Tables(0).Rows(i).Item("NAME").ToString() + "','##',''''))"
                        odbUtil.UpIns(StrSql, ContractConnection)
                    Next

                End If

                'Insert for Selected Processing Capabilities
                dsProc = objGetData.GetCompanyDetailsByCategory(Country, process, UserId)
                If dsProc.Tables(0).Rows.Count > 0 Then
                    For i = 0 To dsProc.Tables(0).Rows.Count - 1
                        StrSql = "INSERT INTO RESULTS (USERID,COMPANY,CRITERIA,COMPANYNAME) VALUES(" + UserId.ToString() + ",'" + dsProc.Tables(0).Rows(i).Item("COMPANYID").ToString() + "','PROCESS',REPLACE('" + dsProc.Tables(0).Rows(i).Item("NAME").ToString() + "','##',''''))"
                        odbUtil.UpIns(StrSql, ContractConnection)
                    Next

                End If

                'Insert for Selected Machinery Systems
                dsMac = objGetData.GetCompanyDetailsByCategory(Country, machine, UserId)
                If dsMac.Tables(0).Rows.Count > 0 Then
                    For i = 0 To dsMac.Tables(0).Rows.Count - 1
                        StrSql = "INSERT INTO RESULTS (USERID,COMPANY,CRITERIA,COMPANYNAME) VALUES(" + UserId.ToString() + ",'" + dsMac.Tables(0).Rows(i).Item("COMPANYID").ToString() + "','MACHINE',REPLACE('" + dsMac.Tables(0).Rows(i).Item("NAME").ToString() + "','##',''''))"
                        odbUtil.UpIns(StrSql, ContractConnection)
                    Next

                End If

                'Insert for Representative Customers
                dsRepCus = objGetData.GetCompanyDetailsByCategory(Country, customer, UserId)
                If dsRepCus.Tables(0).Rows.Count > 0 Then
                    For i = 0 To dsRepCus.Tables(0).Rows.Count - 1
                        StrSql = "INSERT INTO RESULTS (USERID,COMPANY,CRITERIA,COMPANYNAME) VALUES(" + UserId.ToString() + ",'" + dsRepCus.Tables(0).Rows(i).Item("COMPANYID").ToString() + "','CUSTOMER',REPLACE('" + dsRepCus.Tables(0).Rows(i).Item("NAME").ToString() + "','##',''''))"
                        odbUtil.UpIns(StrSql, ContractConnection)
                    Next

                End If



                UpdateUserResultsData(product, state, service, design, process, machine, customer, UserId)


            Catch ex As Exception
                Throw New Exception("ContractUpdate:UpdateResultsData:" + ex.Message.ToString())

            End Try

        End Sub

        

        Public Sub UpdateUserResultsData(ByVal product As String, ByVal state As String, ByVal service As String, ByVal design As String, ByVal process As String, ByVal machine As String, ByVal customer As String, ByVal UserId As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim Dts, dsState, dsProdA, dsProdServ, dsProdDevCap, dsProc, dsMac, dsRepCus As New DataSet
            Dim objGetData As New ContrGetData.Selectdata()
            Dim i As Integer
            Try
                'Getting Data from Selection table for Users
                Dts = objGetData.GetSelectionDataByUser(UserId)



                'Insert for Selected Product Area
                dsProdA = objGetData.GetUserCategoryDetailsByDetialId(product, UserId)
                If dsProdA.Tables(0).Rows.Count > 0 Then
                    For i = 0 To dsProdA.Tables(0).Rows.Count - 1
                        StrSql = "INSERT INTO RESULTS (USERID,COMPANY,CRITERIA,COMPANYNAME) VALUES(" + UserId.ToString() + ",'" + dsProdA.Tables(0).Rows(i).Item("COMPANYID").ToString() + "','product',REPLACE('" + dsProdA.Tables(0).Rows(i).Item("NAME").ToString() + "','##',''''))"
                        odbUtil.UpIns(StrSql, ContractConnection)
                    Next

                End If

                'Insert for Selected Product Service
                dsProdServ = objGetData.GetUserCategoryDetailsByDetialId(service, UserId)
                If dsProdServ.Tables(0).Rows.Count > 0 Then
                    For i = 0 To dsProdServ.Tables(0).Rows.Count - 1
                        StrSql = "INSERT INTO RESULTS (USERID,COMPANY,CRITERIA,COMPANYNAME) VALUES(" + UserId.ToString() + ",'" + dsProdServ.Tables(0).Rows(i).Item("COMPANYID").ToString() + "','SERVICE',REPLACE('" + dsProdServ.Tables(0).Rows(i).Item("NAME").ToString() + "','##',''''))"
                        odbUtil.UpIns(StrSql, ContractConnection)
                    Next

                End If

                'Insert for Selected Product Development Capabilities
                dsProdDevCap = objGetData.GetUserCategoryDetailsByDetialId(design, UserId)
                If dsProdDevCap.Tables(0).Rows.Count > 0 Then
                    For i = 0 To dsProdDevCap.Tables(0).Rows.Count - 1
                        StrSql = "INSERT INTO RESULTS (USERID,COMPANY,CRITERIA,COMPANYNAME) VALUES(" + UserId.ToString() + ",'" + dsProdDevCap.Tables(0).Rows(i).Item("COMPANYID").ToString() + "','DESIGN',REPLACE('" + dsProdDevCap.Tables(0).Rows(i).Item("NAME").ToString() + "','##',''''))"
                        odbUtil.UpIns(StrSql, ContractConnection)
                    Next

                End If

                'Insert for Selected Processing Capabilities
                dsProc = objGetData.GetUserCategoryDetailsByDetialId(process, UserId)
                If dsProc.Tables(0).Rows.Count > 0 Then
                    For i = 0 To dsProc.Tables(0).Rows.Count - 1
                        StrSql = "INSERT INTO RESULTS (USERID,COMPANY,CRITERIA,COMPANYNAME) VALUES(" + UserId.ToString() + ",'" + dsProc.Tables(0).Rows(i).Item("COMPANYID").ToString() + "','PROCESS',REPLACE('" + dsProc.Tables(0).Rows(i).Item("NAME").ToString() + "','##',''''))"
                        odbUtil.UpIns(StrSql, ContractConnection)
                    Next

                End If

                'Insert for Selected Machinery Systems
                dsMac = objGetData.GetUserCategoryDetailsByDetialId(machine, UserId)
                If dsMac.Tables(0).Rows.Count > 0 Then
                    For i = 0 To dsMac.Tables(0).Rows.Count - 1
                        StrSql = "INSERT INTO RESULTS (USERID,COMPANY,CRITERIA,COMPANYNAME) VALUES(" + UserId.ToString() + ",'" + dsMac.Tables(0).Rows(i).Item("COMPANYID").ToString() + "','MACHINE',REPLACE('" + dsMac.Tables(0).Rows(i).Item("NAME").ToString() + "','##',''''))"
                        odbUtil.UpIns(StrSql, ContractConnection)
                    Next

                End If

                'Insert for Representative Customers
                dsRepCus = objGetData.GetUserCategoryDetailsByDetialId(customer, UserId)
                If dsRepCus.Tables(0).Rows.Count > 0 Then
                    For i = 0 To dsRepCus.Tables(0).Rows.Count - 1
                        StrSql = "INSERT INTO RESULTS (USERID,COMPANY,CRITERIA,COMPANYNAME) VALUES(" + UserId.ToString() + ",'" + dsRepCus.Tables(0).Rows(i).Item("COMPANYID").ToString() + "','CUSTOMER',REPLACE('" + dsRepCus.Tables(0).Rows(i).Item("NAME").ToString() + "','##',''''))"
                        odbUtil.UpIns(StrSql, ContractConnection)
                    Next

                End If






            Catch ex As Exception
                Throw New Exception("ContractUpdate:UpdateResultsData:" + ex.Message.ToString())

            End Try

        End Sub

        Public Sub KeywordSearch(ByVal Keyword As String, ByVal UserId As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                'Deleting Privious Results
                DeleteResults(UserId)
                Keyword = Keyword.Replace("'", "''")
                'Inserting Record for keyword search in RESULT table
                StrSql = "INSERT INTO RESULTS  "
                StrSql = StrSql + "(USERID,COMPANY,CRITERIA) "
                StrSql = StrSql + "SELECT  " + UserId.ToString() + ", "
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

                odbUtil.UpIns(StrSql, ContractConnection)

            Catch ex As Exception
                Throw New Exception("ContractUpdate:KeywordSearch:" + ex.Message.ToString())

            End Try

        End Sub

        Public Sub DeleteResults(ByVal UserId As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try

                'Deleting Data from Results Tabel
                StrSql = "DELETE FROM RESULTS "
                StrSql = StrSql + " WHERE USERID =" + UserId.ToString() + " "
                odbUtil.UpIns(StrSql, ContractConnection)

            Catch ex As Exception
                Throw New Exception("ContractUpdate:DeleteResults:" + ex.Message.ToString())

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


                odbUtil.UpIns(StrSql, ContractConnection)

            Catch ex As Exception
                Throw New Exception("ContractUpdate:DeleteResults:" + ex.Message.ToString())

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
                odbUtil.UpIns(StrSql, ContractConnection)

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
                        odbUtil.UpIns(StrSql, ContractConnection)
                    End If
                Next





            Catch ex As Exception
                Throw New Exception("ContractUpdate:DeleteResults:" + ex.Message.ToString())
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


                odbUtil.UpIns(StrSql, ContractConnection)

            Catch ex As Exception
                Throw New Exception("ContractUpdate:DeleteResults:" + ex.Message.ToString())

            End Try

        End Sub

        Public Sub UserRebulidIndex()
            Dim odbUtil As New DBUtil()
            Try
                odbUtil.UserRebuildIndex("CONTR")
            Catch ex As Exception
                Throw New Exception("ContractUpdate:UserRebulidIndex:" + ex.Message.ToString())
            End Try
        End Sub
        
		#Region "Manage Contract Users Admin Tool"
        Public Sub UpdateAdmin(ByVal UserId As String)
            Dim Ds, dsSer As New DataSet()
            Dim StrSql As String = String.Empty
            Try
                'Assign License To User
                StrSql = String.Empty
                StrSql = "UPDATE USERS "
                StrSql = StrSql + " SET ISIADMINLICUSR='Y' WHERE USERID=" + UserId + ""
                odbUtil.UpIns(StrSql, ContractConnection)
            Catch ex As Exception
            End Try
        End Sub

        Public Sub AddTransferLicense(ByVal licenseId As String, ByVal userId As String, ByVal type As String)
            Dim StrSql As String = String.Empty
            Try
                'Update  License Count
                StrSql = "UPDATE TRANSSERV SET USERID1 =" + userId + " "
                StrSql = StrSql + "WHERE LICENSEID = " + licenseId + " AND NVL(USERID1,-1) =-1 AND SEQ=(SELECT NVL(MAX(SEQ)+1,1) FROM TRANSSERV WHERE LICENSEID=" + licenseId + " AND NVL(USERID1,-1)<>-1 AND TYPE='" + type + "')"
                odbUtil.UpIns(StrSql, ContractConnection)

            Catch ex As Exception
                Throw New Exception("UtilityUpdate:AddTransferLicense:" + ex.Message.ToString())
            End Try
        End Sub
        Public Sub AddOrderUsers(ByVal UserId As String, ByVal serviceDesc As String)
            Dim Ds As New DataSet()
            Try
                Dim StrSql As String = String.Empty
                Dim Conn As String = String.Empty

                StrSql = "INSERT INTO USERPERMISSIONS (USERID,SERVICEID,USERROLE,MAXCASECOUNT)  "
                StrSql = StrSql + "SELECT " + UserId + ",(SELECT SERVICEID FROM SERVICES WHERE UPPER(SERVICEDE)='" + serviceDesc.ToUpper() + "'),'ReadWrite',500 FROM DUAL "
                StrSql = StrSql + "WHERE NOT EXISTS "
                StrSql = StrSql + " (SELECT 1 FROM USERPERMISSIONS "
                StrSql = StrSql + "WHERE USERID=" + UserId + " AND SERVICEID IN (SELECT SERVICEID FROM SERVICES WHERE UPPER(SERVICEDE)='" + serviceDesc.ToUpper() + "'))"

                odbUtil.UpIns(StrSql, EconConnection)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub
        Public Sub AddEditUserContractCountries(ByVal UserId As String)
            Dim StrSql As String = String.Empty
            Try
                'Deleting  User Countries
                StrSql = "DELETE FROM USERCOUNTRIES  "
                StrSql = StrSql + "WHERE USERID =" + UserId + " "
                odbUtil.UpIns(StrSql, ContractConnection)

                'Add  User Countries
                StrSql = "INSERT INTO USERCOUNTRIES  "
                StrSql = StrSql + "(USERCOUNTRYID,COUNTRYID,USERID) "
                StrSql = StrSql + "SELECT USERCOUNTRYIDSEQ.NEXTVAL, "
                StrSql = StrSql + "DIMCOUNTRIES.COUNTRYID, "
                StrSql = StrSql + "" + UserId + "  "
                StrSql = StrSql + "FROM ADMINSITE.DIMCOUNTRIES "
                StrSql = StrSql + "INNER JOIN ADMINSITE.COUNTRYAVAIL "
                StrSql = StrSql + "ON DIMCOUNTRIES.COUNTRYID =COUNTRYAVAIL.COUNTRYID "
                StrSql = StrSql + "WHERE COUNTRYAVAIL.MODULEID=1 "
                StrSql = StrSql + "AND NOT EXISTS (    SELECT 1 "
                StrSql = StrSql + "FROM USERCOUNTRIES "
                StrSql = StrSql + "WHERE USERID =" + UserId + " "
                StrSql = StrSql + ") "
                odbUtil.UpIns(StrSql, ContractConnection)

            Catch ex As Exception
                Throw New Exception("UtilityUpdate:AddEditUserCountries:" + ex.Message.ToString())
            End Try
        End Sub

        Public Sub AddOrderUsersD(ByVal user As String, ByVal serviceDesc As String)
            Dim Ds As New DataSet()
            Try
                Dim StrSql As String = String.Empty
                Dim Conn As String = String.Empty

                StrSql = "INSERT INTO USERPERMISSIONS (USERID,SERVICEID,USERROLE,MAXCASECOUNT)  "
                StrSql = StrSql + "SELECT (SELECT USERID FROM USERS WHERE UPPER(USERNAME)='" + user.ToUpper() + "'),(SELECT SERVICEID FROM SERVICES WHERE UPPER(SERVICEDE)='" + serviceDesc.ToUpper() + "'),'ReadWrite',500 FROM DUAL "
                StrSql = StrSql + "WHERE NOT EXISTS "
                StrSql = StrSql + " (SELECT 1 FROM USERPERMISSIONS "
                StrSql = StrSql + "WHERE USERID=(SELECT USERID FROM USERS WHERE UPPER(USERNAME)='" + user.ToUpper() + "') AND SERVICEID IN (SELECT SERVICEID FROM SERVICES WHERE UPPER(SERVICEDE)='" + serviceDesc.ToUpper() + "'))"
                odbUtil.UpIns(StrSql, EconConnection)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Sub AddEditUserContractCountriesD(ByVal user As String)

            Dim StrSql As String = String.Empty
            Try
                'Deleting  User Countries
                StrSql = "DELETE FROM USERCOUNTRIES  "
                StrSql = StrSql + "WHERE USERID =(SELECT USERID FROM ECON.USERS WHERE UPPER(USERNAME)='" + user.ToUpper() + "') "
                odbUtil.UpIns(StrSql, ContractConnection)

                'Add  User Countries
                StrSql = "INSERT INTO USERCOUNTRIES  "
                StrSql = StrSql + "(USERCOUNTRYID,COUNTRYID,USERID) "
                StrSql = StrSql + "SELECT USERCOUNTRYIDSEQ.NEXTVAL, "
                StrSql = StrSql + "DIMCOUNTRIES.COUNTRYID, "
                StrSql = StrSql + "(SELECT USERID FROM ECON.USERS WHERE UPPER(USERNAME)='" + user.ToUpper() + "') "
                StrSql = StrSql + "FROM ADMINSITE.DIMCOUNTRIES "
                StrSql = StrSql + "INNER JOIN ADMINSITE.COUNTRYAVAIL "
                StrSql = StrSql + "ON DIMCOUNTRIES.COUNTRYID =COUNTRYAVAIL.COUNTRYID "
                StrSql = StrSql + "WHERE COUNTRYAVAIL.MODULEID=1 "
                StrSql = StrSql + "AND NOT EXISTS (    SELECT 1 "
                StrSql = StrSql + "FROM USERCOUNTRIES "
                StrSql = StrSql + "WHERE USERID =(SELECT USERID FROM ECON.USERS WHERE UPPER(USERNAME)='" + user.ToUpper() + "')"
                StrSql = StrSql + ") "
                odbUtil.UpIns(StrSql, ContractConnection)

            Catch ex As Exception
                Throw New Exception("UtilityUpdate:AddEditUserContractCountriesD:" + ex.Message.ToString())
            End Try
        End Sub

        Public Sub DeleteOrderUsers(ByVal UserId As String, ByVal serviceDesc As String)
            Dim Ds As New DataSet()
            Try
                Dim StrSql As String = String.Empty
                Dim Conn As String = String.Empty

                  StrSql = "DELETE FROM USERPERMISSIONS "
                StrSql = StrSql + "WHERE USERID=" + UserId + " AND SERVICEID IN (SELECT SERVICEID FROM SERVICES WHERE UPPER(SERVICEDE)='" + serviceDesc.ToUpper() + "') "
                odbUtil.UpIns(StrSql, EconConnection)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Sub UpdateLicenseData(ByVal licenseId As String, ByVal fUserId As String, ByVal tUser As String)
            Dim StrSql As String = String.Empty
            Try
                'Update  License Count
                StrSql = "UPDATE TRANSSERV SET USERID2 =(SELECT USERID FROM ECON.USERS WHERE UPPER(USERNAME)='" + tUser.ToUpper() + "'),COUNT=1 "
                StrSql = StrSql + "WHERE LICENSEID = " + licenseId + " AND USERID1 =" + fUserId + " AND TYPE='KBCOPK'  "
                odbUtil.UpIns(StrSql, ContractConnection)

            Catch ex As Exception
                Throw New Exception("UpInsData:UpdateLicenseData:" + ex.Message.ToString())
            End Try
        End Sub

        Public Sub DeleteUserContractCountries(ByVal UserId As String)
            Dim StrSql As String = String.Empty
            Try
                'Deleting  User Countries
                StrSql = "DELETE FROM USERCOUNTRIES  "
                StrSql = StrSql + "WHERE USERID =" + UserId + " "
                odbUtil.UpIns(StrSql, ContractConnection)

            Catch ex As Exception
                Throw New Exception("UtilityUpdate:DeleteUserContractCountries:" + ex.Message.ToString())
            End Try
        End Sub
#End Region

    End Class
   

End Class
