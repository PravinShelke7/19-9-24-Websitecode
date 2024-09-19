Imports Microsoft.VisualBasic
Imports System.Web.SessionState
Imports System.Data
Imports System.Data.OleDb
Imports System


Namespace UpdateData


    Public Class Update

        Public Function AssumptionUpdate(ByVal Caseid As String) As Integer
            Try

            If Caseid <> "" Then
                    'Converting The Comma Separetd CaseID String to an Array
                    Dim CaseArray(9) As String
                    Dim NewCaseArray(9) As String
                    Dim Cases As Integer

                    CaseArray = Caseid.Split(",")
                    Dim I As Integer

                    For I = 0 To 9
                        If I <= UBound(CaseArray) Then
                            NewCaseArray(I) = CaseArray(I)
                        Else
                            NewCaseArray(I) = "NULL"
                        End If
                    Next

                    Cases = (UBound(CaseArray) + 1)



                    'Creating Database Connection
                    Dim oDbUtil As New DBUtil()
                    Dim MyConnectionString As String
                    MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("Econ3ConnectionString")


                    If (NewCaseArray.Length <= 10) Then

                        'SQL
                        Dim SqlId As String = "SELECT SEQASSUMPTIONID.nextval NewAssumptionId FROM dual"
                        Dim AssumptionID As Integer = oDbUtil.FillData(SqlId, MyConnectionString)




                        Dim StrSqlInsert As String = "Insert into Assumptions (AssumptionId, Description, Module, Saved, Case1, Case2, Case3,"
                        StrSqlInsert = StrSqlInsert + "case4, case5, case6, case7, case8, case9, case10 ) "
                        StrSqlInsert = StrSqlInsert + "values (" + AssumptionID.ToString() + ",'New Comparision',1,0," + NewCaseArray(0) + "," + NewCaseArray(1) + "," + NewCaseArray(2) + "," + NewCaseArray(3) + "," + NewCaseArray(4) + ","
                        StrSqlInsert = StrSqlInsert + "" + NewCaseArray(5) + "," + NewCaseArray(6) + "," + NewCaseArray(7) + "," + NewCaseArray(8) + "," + NewCaseArray(9) + "  "
                        StrSqlInsert = StrSqlInsert + ")"
                        oDbUtil.UpIns(StrSqlInsert, MyConnectionString)
                        Return AssumptionID

                    End If

                End If
            Catch ex As Exception

            End Try

        End Function

        Public Sub SharedCase(ByVal shareusername As String, ByVal ID As String)
            Try

                'Creating Database Connection
                Dim oDbUtil As New DBUtil()

                'SQL
                Dim MyConnectionString As String
                MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("Econ3ConnectionString")
                Dim StrsqlInsert As String = "INSERT INTO econ3.PERMASSUMPTIONS (USERNAME, ASSUMPTIONID)"
                StrsqlInsert = StrsqlInsert + " VALUES('" + shareusername + "'," + ID + ")"
                oDbUtil.UpIns(StrsqlInsert, MyConnectionString)

            Catch ex As Exception

            End Try

        End Sub

        Public Sub AlterComparison(ByVal Name, ByVal ID, ByVal AssID, ByVal Username, ByVal Password)

            Try

                'Creating Database Connection
                Dim odButil As New DBUtil()
                Dim MyConnectionString As String = ""
                MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("Econ3ConnectionString")

                'SQL
                If ID = "1" Or ID = "2" Then
                    Dim StrSqlSave As String = "UPDATE ASSUMPTIONS SET SAVED = 1 ,"
                    StrSqlSave = StrSqlSave + " DESCRIPTION='" + Name + " '  "
                    StrSqlSave = StrSqlSave + "WHERE ASSUMPTIONID =" + AssID.ToString() + ""
                    odButil.UpIns(StrSqlSave, MyConnectionString)

                    Dim StrSqlSaved As String = "Insert into PERMASSUMPTIONS (USERNAME , ASSUMPTIONID  ) select"
                    StrSqlSaved = StrSqlSaved + "'" + Username.ToString() + "'," + AssID.ToString() + " From Dual where not exists ( select 1 from PERMASSUMPTIONS "
                    StrSqlSaved = StrSqlSaved + "Where PERMASSUMPTIONS.ASSUMPTIONID =" + AssID.ToString() + ")"
                    odButil.UpIns(StrSqlSaved, MyConnectionString)

                End If

                If ID = "3" Then

                    Dim StrSqlDelete As String = "DELETE FROM PERMASSUMPTIONS WHERE ASSUMPTIONID=" + AssID.ToString() + ""
                    odButil.UpIns(StrSqlDelete, MyConnectionString)

                    Dim StrSqlDelete2 As String = "DELETE FROM ASSUMPTIONS WHERE ASSUMPTIONID=" + AssID.ToString() + ""
                    odButil.UpIns(StrSqlDelete2, MyConnectionString)

                End If

                If ID = "4" Then
                    Dim StrSqlSave As String = "UPDATE ASSUMPTIONS SET "
                    StrSqlSave = StrSqlSave + " DESCRIPTION='" + Name + " '  "
                    StrSqlSave = StrSqlSave + "WHERE ASSUMPTIONID =" + AssID.ToString() + ""
                    odButil.UpIns(StrSqlSave, MyConnectionString)
                End If

            Catch ex As Exception

            End Try
        End Sub

        Public Sub MaterialUpdate(ByVal CaseID, ByVal M, ByVal T, ByVal S)
            Try

                Dim Material(9) As String
                Dim Thickness(9) As String
                Dim Perffred(9) As String

                Material = M.Split(",")
                Thickness = T.Split(",")
                Perffred = S.Split(",")

                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = ""
                MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")

                'SQL
                Dim StrSqlCoversion As String = "Select convthick,convwt,curr from PREFERENCES where caseid=" + CaseID + ""
                Dim dts As New DataTable()
                dts = odbUtil.FillDataTable(StrSqlCoversion, MyConnectionString)

                'Getting Values From Database assign to variable 
                Dim Convthick As String = dts.Rows(0).Item("convthick")
                Dim Convwt As String = dts.Rows(0).Item("convwt")
                Dim Curr As String = dts.Rows(0).Item("curr")
                Dim Mat As New Integer
                Dim StrSqlUpadate As String = ""
                Dim StrSqlMaterialUpdate As String = ""
                Dim StrSqlPreffredUpdate As String = ""
                Dim StrSqlThickNessUpdate As String = ""

                StrSqlUpadate = "UPDATE MaterialInput SET"
                For Mat = 0 To 9

                    Dim Matid As New Integer
                    Dim TotalThicKness As Double
                    Dim Totalprice As Double
                    TotalThicKness = Thickness(Mat) / Convthick
                    Totalprice = Perffred(Mat) * Convwt / Curr
                    Matid = Mat + 1

                    'Material 
                    StrSqlMaterialUpdate = StrSqlMaterialUpdate + " M" + Matid.ToString() + " = " + Material(Mat) + ","

                    'Thickness
                    StrSqlThickNessUpdate = StrSqlThickNessUpdate + " T" + Matid.ToString() + " = " + TotalThicKness.ToString() + ","

                    'Preffred
                    StrSqlPreffredUpdate = StrSqlPreffredUpdate + " S" + Matid.ToString() + " = "
                    StrSqlPreffredUpdate = StrSqlPreffredUpdate + "( Select (" + Totalprice.ToString() + ") "
                    StrSqlPreffredUpdate = StrSqlPreffredUpdate + "From PREFERENCES where caseid =" + CaseID + " and rownum < 2 ),"


                Next

                StrSqlPreffredUpdate = StrSqlPreffredUpdate.Remove(StrSqlPreffredUpdate.Length - 1)

                StrSqlUpadate = StrSqlUpadate + StrSqlMaterialUpdate + StrSqlThickNessUpdate + StrSqlPreffredUpdate
                StrSqlUpadate = StrSqlUpadate + " WHERE caseID = " + CaseID + ""

                odbUtil.UpIns(StrSqlUpadate, MyConnectionString)

            Catch ex As Exception

            End Try

        End Sub

        Public Sub ProductUpdate(ByVal CaseID As String, ByVal P As String, ByVal I1 As String, ByVal I2 As String, ByVal I3 As String, ByVal I4 As String, ByVal I5 As String)

            'DataBase Connctions 
            Dim odbUtil As New DBUtil()
            Dim MyConnectionString As String
            MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")

            'Conversion Factor Sql
            Dim StrSqlPref As String = "Select convthick, units from preferences where caseID =" + CaseID + ""
            Dim Dts As New DataTable()
            Dts = odbUtil.FillDataTable(StrSqlPref, MyConnectionString)


            Dim StrSqlMaterial As String = "select caseID,m1 from ProductFormatIN WHERE caseID=" + CaseID + ""
            Dim Dt As New DataTable()
            Dt = odbUtil.FillDataTable(StrSqlMaterial, MyConnectionString)

            Dim convthick As String = Dts.Rows(0).Item("convthick").ToString()
            Dim units As String = Dts.Rows(0).Item("units").ToString()
            Dim Roll As String = 1
            Dim M1 As String = Dt.Rows(0).Item("M1").ToString()
            If units = 1 Then
                If M1 = 1 Then
                    Roll = 0.01204
                End If
            End If


            'Feild After Convrting to Orginal  Format
            Dim TotalI1 As String = I1 / convthick
            Dim TotalI2 As String = I2 / convthick / Roll
            Dim TotalI3 As String = I3 / convthick
            Dim TotalI4 As String = I4
            Dim TotalI5 As String = I5


            'Update Query
            Dim StrSqlProductFormatUpdate As String = ""
            StrSqlProductFormatUpdate = "UPDATE ProductFormatIN SET M1=" + P + ","
            StrSqlProductFormatUpdate = StrSqlProductFormatUpdate + "M2=" + TotalI1 + ","
            StrSqlProductFormatUpdate = StrSqlProductFormatUpdate + "M3=" + TotalI2 + ","
            StrSqlProductFormatUpdate = StrSqlProductFormatUpdate + "M4=" + TotalI3 + ","
            StrSqlProductFormatUpdate = StrSqlProductFormatUpdate + "M5=" + TotalI4 + ","
            StrSqlProductFormatUpdate = StrSqlProductFormatUpdate + "M6=" + TotalI5 + "where caseID =" + CaseID + ""
            odbUtil.UpIns(StrSqlProductFormatUpdate, MyConnectionString)

        End Sub

        Public Sub PalletAndTruckUpdate(ByVal CaseID As String, ByVal Pwidth As String, ByVal Plength As String, ByVal Pheight As String, ByVal CartonsPP As String, ByVal ProdcutPP As String, ByVal Twidth As String, ByVal Tlength As String, ByVal Theight As String, ByVal PalletsPT As String, ByVal TruckWeightLimit As String)
            Try
                'DataBase Connctions 
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String
                MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")

                'Conversion Factor
                Dim StrSqlConvesrion As String = "Select convthick,convwt from PREFERENCES where caseid=" + CaseID + ""
                Dim Dts As New DataTable
                Dts = odbUtil.FillDataTable(StrSqlConvesrion, MyConnectionString)

                Dim Convthick As String = Dts.Rows(0).Item("convthick").ToString()
                Dim Convwt As String = Dts.Rows(0).Item("convwt").ToString()

                Dim PalletWidth As String = Pwidth / Convthick
                Dim PalletLength As String = Plength / Convthick
                Dim PalletHeight As String = Pheight / Convthick
                Dim TruckWidth As String = Twidth / Convthick
                Dim TruckLength As String = Tlength / Convthick
                Dim TruckHeight As String = Theight / Convthick


                'Update Sql
                Dim StrSqlUpdate As String = ""
                StrSqlUpdate = "UPDATE  TRUCKPALLETIN  "
                StrSqlUpdate = StrSqlUpdate + "SET "
                StrSqlUpdate = StrSqlUpdate + "M1=" + PalletWidth + ", "
                StrSqlUpdate = StrSqlUpdate + "M2=" + PalletLength + ", "
                StrSqlUpdate = StrSqlUpdate + "M3=" + PalletHeight + ", "
                StrSqlUpdate = StrSqlUpdate + "M4=" + CartonsPP + ", "
                StrSqlUpdate = StrSqlUpdate + "M5=" + ProdcutPP + ", "
                StrSqlUpdate = StrSqlUpdate + "T1=" + TruckWidth + ", "
                StrSqlUpdate = StrSqlUpdate + "T2=" + TruckLength + ", "
                StrSqlUpdate = StrSqlUpdate + "T3=" + TruckHeight + ", "
                StrSqlUpdate = StrSqlUpdate + "T4=" + PalletsPT + ", "
                StrSqlUpdate = StrSqlUpdate + "T5=" + TruckWeightLimit + " "
                StrSqlUpdate = StrSqlUpdate + "WHERE CASEID=" + CaseID + ""


                odbUtil.UpIns(StrSqlUpdate, MyConnectionString)

            Catch ex As Exception
                Throw
            End Try
        End Sub

        Public Sub PalletInUpdate(ByVal CaseID As String, ByVal Item As String, ByVal Number As String, ByVal Recycle As String, ByVal PriseP As String, ByVal MfgDept As String)
            Try
                'Database Connection
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String
                MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")

                'Conversion Factor
                Dim StrSqlConvesrion As String = "Select CURR from PREFERENCES where caseid=" + CaseID + ""
                Dim Dts As New DataTable
                Dts = odbUtil.FillDataTable(StrSqlConvesrion, MyConnectionString)

                Dim ItemArray(9) As String
                Dim NumberArray(9) As String
                Dim RecycleArray(9) As String
                Dim PrefredArray(9) As String
                Dim DeptArray(9) As String
                Dim PalletCount As New Integer
                Dim StrSqlUpadate As String = ""
                Dim ItemSql As String = ""
                Dim NumberSql As String = ""
                Dim RecycleSql As String = ""
                Dim PrefredSql As String = ""
                Dim DeptSql As String = ""

                Dim TotalPricePref As String = ""
                Dim Curr As String = Dts.Rows(0).Item("CURR").ToString()

                ItemArray = Split(Item, ",")
                NumberArray = Split(Number, ",")
                RecycleArray = Split(Recycle, ",")
                PrefredArray = Split(PriseP, ",")
                DeptArray = Split(MfgDept, ",")
                StrSqlUpadate = "UPDATE PALLETIN SET "

                For PalletCount = 0 To 9
                    Dim PC = PalletCount + 1

                    TotalPricePref = PrefredArray(PalletCount) / Curr

                    ItemSql = ItemSql + "M" + PC.ToString() + "=" + ItemArray(PalletCount) + ","
                    NumberSql = NumberSql + "T" + PC.ToString() + "=" + NumberArray(PalletCount) + ","
                    RecycleSql = RecycleSql + "R" + PC.ToString() + "=" + RecycleArray(PalletCount) + ","
                    PrefredSql = PrefredSql + "P" + PC.ToString() + "=" + TotalPricePref + ","
                    DeptSql = DeptSql + "D" + PC.ToString() + "=" + DeptArray(PalletCount) + ","
                Next

                DeptSql = DeptSql.Remove(DeptSql.Length - 1)
                StrSqlUpadate = StrSqlUpadate + ItemSql + NumberSql + RecycleSql + PrefredSql + DeptSql
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID=" + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, MyConnectionString)
            Catch ex As Exception
                Throw
            End Try
        End Sub

        Public Sub DepartmentConFigUpdate(ByVal CaseID As String, ByVal DF(,) As String)
            Try

                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String
                MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")

                Dim I As New Integer
                Dim J As New Integer
                Dim DF1 As String = ""
                Dim StrSqlUpadate As String = ""

                StrSqlUpadate = "UPDATE PLANTCONFIG SET "

                For J = 1 To 10
                    DF1 = DF1 + "M" + J.ToString() + "=" + DF(J, 1).ToString() + ","
                    DF1 = DF1 + "T" + J.ToString() + "=" + DF(J, 2).ToString() + ","
                    DF1 = DF1 + "S" + J.ToString() + "=" + DF(J, 3).ToString() + ","
                    DF1 = DF1 + "Y" + J.ToString() + "=" + DF(J, 4).ToString() + ","
                    DF1 = DF1 + "D" + J.ToString() + "=" + DF(J, 5).ToString() + ","
                    DF1 = DF1 + "Z" + J.ToString() + "=" + DF(J, 6).ToString() + ","
                    DF1 = DF1 + "B" + J.ToString() + "=" + DF(J, 7).ToString() + ","
                    DF1 = DF1 + "R" + J.ToString() + "=" + DF(J, 8).ToString() + ","
                    DF1 = DF1 + "K" + J.ToString() + "=" + DF(J, 9).ToString() + ","
                    DF1 = DF1 + "P" + J.ToString() + "=" + DF(J, 10).ToString() + ","
                Next
                DF1 = DF1.Remove(DF1.Length - 1)
                StrSqlUpadate = StrSqlUpadate + DF1
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID=" + CaseID + ""

                odbUtil.UpIns(StrSqlUpadate, MyConnectionString)

            Catch ex As Exception
                Throw
            End Try



            'Try

            '    'Database Connection
            '    Dim odbUtil As New DBUtil()
            '    Dim MyConnectionString As String
            '    MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")

            '    Dim RDArray(9) As String
            '    Dim NP1Array(9) As String
            '    Dim NP2Array(9) As String
            '    Dim NP3Array(9) As String
            '    Dim NP4Array(9) As String
            '    Dim NP5Array(9) As String
            '    Dim NP6Array(9) As String
            '    Dim NP7Array(9) As String
            '    Dim NP8Array(9) As String
            '    Dim NP9Array(9) As String

            '    Dim DeptCount As New Integer
            '    Dim StrSqlUpadate1 As String = ""
            '    Dim RDSql As String = ""
            '    Dim NP1Sql As String = ""
            '    Dim NP2Sql As String = ""
            '    Dim NP3Sql As String = ""
            '    Dim NP4Sql As String = ""
            '    Dim NP5Sql As String = ""
            '    Dim NP6Sql As String = ""
            '    Dim NP7Sql As String = ""
            '    Dim NP8Sql As String = ""
            '    Dim NP9Sql As String = ""
            '    Dim DC As String = ""


            '    RDArray = Split(RequiredDepartment, ",")
            '    NP1Array = Split(NextProcess1, ",")
            '    NP2Array = Split(NextProcess2, ",")
            '    NP3Array = Split(NextProcess3, ",")
            '    NP4Array = Split(NextProcess4, ",")
            '    NP5Array = Split(NextProcess5, ",")
            '    NP6Array = Split(NextProcess6, ",")
            '    NP7Array = Split(NextProcess7, ",")
            '    NP8Array = Split(NextProcess8, ",")
            '    NP9Array = Split(NextProcess9, ",")

            '    StrSqlUpadate1 = "UPDATE PlantCONFIG SET "

            '    For DeptCount = 0 To 9
            '        DC = DeptCount + 1
            '        RDSql = RDSql + "M" + DC.ToString() + "=" + RDArray(DeptCount) + ","
            '        NP1Sql = NP1Sql + "T" + DC.ToString() + "=" + NP1Array(DeptCount) + ","
            '        NP2Sql = NP2Sql + "S" + DC.ToString() + "=" + NP2Array(DeptCount) + ","
            '        NP3Sql = NP3Sql + "Y" + DC.ToString() + "=" + NP3Array(DeptCount) + ","
            '        NP4Sql = NP4Sql + "D" + DC.ToString() + "=" + NP4Array(DeptCount) + ","
            '        NP5Sql = NP5Sql + "Z" + DC.ToString() + "=" + NP5Array(DeptCount) + ","
            '        NP6Sql = NP6Sql + "B" + DC.ToString() + "=" + NP6Array(DeptCount) + ","
            '        NP7Sql = NP7Sql + "R" + DC.ToString() + "=" + NP7Array(DeptCount) + ","
            '        NP8Sql = NP8Sql + "K" + DC.ToString() + "=" + NP8Array(DeptCount) + ","
            '        NP9Sql = NP9Sql + "P" + DC.ToString() + "=" + NP9Array(DeptCount) + ","
            '    Next
            '    NP9Sql = NP9Sql.Remove(NP9Sql.Length - 1)

            '    StrSqlUpadate1 = StrSqlUpadate1 + RDSql + NP1Sql + NP2Sql + NP3Sql + NP4Sql + NP5Sql + NP6Sql + NP7Sql + NP8Sql + NP9Sql
            '    StrSqlUpadate1 = StrSqlUpadate1 + " WHERE CASEID=" + CaseID + ""
            '    odbUtil.UpIns(StrSqlUpadate1, MyConnectionString)
            'Catch ex As Exception
            '    Throw
            'End Try

        End Sub

        Public Sub MaterialEffiUpdate(ByVal CaseID As String, ByVal DN(,) As String)

            Try

                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String
                MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")

                Dim I As New Integer
                Dim J As New Integer
                Dim DN1 As String = ""
                Dim StrSqlUpadate As String = ""

                StrSqlUpadate = "UPDATE MATERIALEFF SET "


                For J = 1 To 10

                    DN1 = DN1 + "T" + J.ToString() + "=" + DN(J, 1).ToString() + ","
                    DN1 = DN1 + "S" + J.ToString() + "=" + DN(J, 2).ToString() + ","
                    DN1 = DN1 + "Y" + J.ToString() + "=" + DN(J, 3).ToString() + ","
                    DN1 = DN1 + "D" + J.ToString() + "=" + DN(J, 4).ToString() + ","
                    DN1 = DN1 + "E" + J.ToString() + "=" + DN(J, 5).ToString() + ","
                    DN1 = DN1 + "Z" + J.ToString() + "=" + DN(J, 6).ToString() + ","
                    DN1 = DN1 + "B" + J.ToString() + "=" + DN(J, 7).ToString() + ","
                    DN1 = DN1 + "R" + J.ToString() + "=" + DN(J, 8).ToString() + ","
                    DN1 = DN1 + "K" + J.ToString() + "=" + DN(J, 9).ToString() + ","
                    DN1 = DN1 + "P" + J.ToString() + "=" + DN(J, 10).ToString() + ","
                Next
                DN1 = DN1.Remove(DN1.Length - 1)
                StrSqlUpadate = StrSqlUpadate + DN1 + " WHERE CASEID=" + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, MyConnectionString)

            Catch ex As Exception
                Throw
            End Try



        End Sub

        Public Sub ProcessUpdate(ByVal CaseID As String, ByVal AD() As String, ByVal ACP() As String, ByVal PAP() As String, ByVal PEC() As String, ByVal MD() As String)
            Try

                'Database Connection

                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String
                MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")

                'Conversion Factor

                Dim StrSqlConvesrion As String = "SELECT CURR,CONVAREA2 FROM PREFERENCES WHERE CASEID=" + CaseID + ""
                Dim Dts As New DataTable
                Dts = odbUtil.FillDataTable(StrSqlConvesrion, MyConnectionString)

                ' declaring conversion

                Dim Convarea2 As String = Dts.Rows(0).Item("CONVAREA2").ToString()
                Dim Curr As String = Dts.Rows(0).Item("CURR").ToString()

                Dim SqlAssDesUpdate As String = ""
                Dim SqlAssCostUpdate As String = ""
                Dim SqlPlantAreaUpdate As String = ""
                Dim SqlProcEnegUpdate As String = ""
                Dim SqlManfProcDeptUpdate As String = ""
                Dim CostCurr As String = ""
                Dim AreaConv As String = ""



                Dim AD1 As String = ""
                Dim ACP1 As String = ""
                Dim PAP1 As String = ""
                Dim PEC1 As String = ""
                Dim MD1 As String = ""

                'update Query

                SqlAssDesUpdate = "UPDATE EQUIPMENTTYPE SET "
                SqlAssCostUpdate = "UPDATE EQUIPMENTCOST SET "
                SqlPlantAreaUpdate = "UPDATE EQUIPMENTAREA SET "
                SqlProcEnegUpdate = "UPDATE EQUIPENERGYPREF SET "
                SqlManfProcDeptUpdate = "UPDATE EQUIPMENTDEP SET "

                Dim I As Integer

                'converting

                For I = 1 To 30
                    CostCurr = ACP(I) / Curr
                    AreaConv = PAP(I) / Convarea2


                    AD1 = AD1 + "M" + I.ToString() + "=" + AD(I).ToString() + ","
                    ACP1 = ACP1 + "M" + I.ToString() + "=" + CostCurr + ","
                    PAP1 = PAP1 + "M" + I.ToString() + "=" + AreaConv + ","
                    PEC1 = PEC1 + Replace("M" + I.ToString() + "=" + PEC(I).ToString(), ",", "") + ","
                    MD1 = MD1 + "M" + I.ToString() + "=" + MD(I).ToString() + ","


                Next
                AD1 = AD1.Remove(AD1.Length - 1)
                ACP1 = ACP1.Remove(ACP1.Length - 1)
                PAP1 = PAP1.Remove(PAP1.Length - 1)
                PEC1 = PEC1.Remove(PEC1.Length - 1)
                MD1 = MD1.Remove(MD1.Length - 1)

                SqlAssDesUpdate = SqlAssDesUpdate + AD1 + " WHERE CASEID = " + CaseID + ""
                SqlAssCostUpdate = SqlAssCostUpdate + ACP1 + " WHERE CASEID = " + CaseID + ""
                SqlPlantAreaUpdate = SqlPlantAreaUpdate + PAP1 + " WHERE CASEID = " + CaseID + ""
                SqlProcEnegUpdate = SqlProcEnegUpdate + PEC1 + " WHERE CASEID = " + CaseID + ""
                SqlManfProcDeptUpdate = SqlManfProcDeptUpdate + MD1 + " WHERE CASEID = " + CaseID + ""

                odbUtil.UpIns(SqlAssDesUpdate, MyConnectionString)
                odbUtil.UpIns(SqlAssCostUpdate, MyConnectionString)
                odbUtil.UpIns(SqlPlantAreaUpdate, MyConnectionString)
                odbUtil.UpIns(SqlProcEnegUpdate, MyConnectionString)
                odbUtil.UpIns(SqlManfProcDeptUpdate, MyConnectionString)

            Catch ex As Exception
                Throw
            End Try
        End Sub

        Public Sub SupportUpdate(ByVal CaseID As String, ByVal AssDes() As String, ByVal AssCostPre() As String, ByVal ManDept() As String)
            Try


                'Database Connection
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String
                MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")

                'Conversion Factor
                Dim StrSqlConvesrion As String = "SELECT CURR FROM PREFERENCES WHERE CASEID=" + CaseID + ""
                Dim Dts As New DataTable
                Dts = odbUtil.FillDataTable(StrSqlConvesrion, MyConnectionString)

                Dim Curr As String = Dts.Rows(0).Item("CURR").ToString()

                Dim SqlAssetDes As String = ""
                Dim SqlAssetCostPre As String = ""
                Dim SqlManfDept As String = ""

                Dim CostCurr As String = ""

                Dim AssDes1 As String = ""
                Dim AssCostPre1 As String = ""
                Dim ManDept1 As String = ""

                SqlAssetDes = "UPDATE EQUIPMENT2TYPE SET "
                SqlAssetCostPre = "UPDATE EQUIPMENT2COST SET "
                SqlManfDept = "UPDATE EQUIPMENT2DEP SET "

                Dim I As Integer
                For I = 1 To 30
                    CostCurr = AssCostPre(I) / Curr
                    AssDes1 = AssDes1 + "M" + I.ToString() + "=" + AssDes(I).ToString() + ","
                    AssCostPre1 = AssCostPre1 + "M" + I.ToString() + "=" + CostCurr + ","
                    ManDept1 = ManDept1 + "M" + I.ToString() + "=" + ManDept(I).ToString() + ","
                Next
                AssDes1 = AssDes1.Remove(AssDes1.Length - 1)
                AssCostPre1 = AssCostPre1.Remove(AssCostPre1.Length - 1)
                ManDept1 = ManDept1.Remove(ManDept1.Length - 1)

                SqlAssetDes = SqlAssetDes + AssDes1 + " WHERE CASEID = " + CaseID + ""
                SqlAssetCostPre = SqlAssetCostPre + AssCostPre1 + " WHERE CASEID = " + CaseID + ""
                SqlManfDept = SqlManfDept + ManDept1 + " WHERE CASEID = " + CaseID + ""

                odbUtil.UpIns(SqlAssetDes, MyConnectionString)
                odbUtil.UpIns(SqlAssetCostPre, MyConnectionString)
                odbUtil.UpIns(SqlManfDept, MyConnectionString)

            Catch ex As Exception
                Throw
            End Try

        End Sub

        Public Sub OperationsUpdate(ByVal CaseID As String, ByVal WebWidth() As String, ByVal MaxAnnRunHrs() As String, ByVal InstantRate() As String, ByVal DownTime() As String, ByVal ProdWaste() As String, ByVal DesignWaste() As String)
            Try

                'Database Connection

                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String
                MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")

                'Conversion Factor

                Dim StrSqlConvesrion As String = "SELECT CONVTHICK2,CONVWT FROM PREFERENCES WHERE CASEID=" + CaseID + ""
                Dim Dts As New DataTable
                Dts = odbUtil.FillDataTable(StrSqlConvesrion, MyConnectionString)

                'declaring conversion

                Dim ConvThick As String = Dts.Rows(0).Item("CONVTHICK2").ToString()
                Dim Convwt As String = Dts.Rows(0).Item("CONVWT").ToString()

                Dim SqlWebWidth As String = ""
                Dim SqlMaxAnnRunHrs As String = ""
                Dim SqlInstantRate As String = ""
                Dim SqlDownTime As String = ""
                Dim SqlProdDesingWaste As String = ""

                Dim WebWidth1 As String = ""
                Dim MaxAnnRunHrs1 As String = ""
                Dim InstantRate1 As String = ""
                Dim DownTime1 As String = ""
                Dim ProdDesignWaste1 As String = ""

                Dim WebWidConThick As String = ""
                Dim InstRateConWet As String = ""


                'update Query

                SqlWebWidth = "UPDATE OPWEBWIDTH SET "
                SqlMaxAnnRunHrs = "UPDATE OPMAXRUNHRS SET "
                SqlInstantRate = "UPDATE OPINSTGRSRATE SET "
                SqlDownTime = "UPDATE OPDOWNTIME SET "
                SqlProdDesingWaste = "UPDATE OPWASTE SET "

                Dim I As Integer

                For I = 1 To 30
                    WebWidConThick = WebWidth(I) / ConvThick
                    InstRateConWet = InstantRate(I) / Convwt

                    WebWidth1 = WebWidth1 + "M" + I.ToString() + "=" + WebWidConThick + ","
                    MaxAnnRunHrs1 = MaxAnnRunHrs1 + Replace(Trim("M" + I.ToString() + "=" + MaxAnnRunHrs(I).ToString()), ",", "") + ","
                    InstantRate1 = InstantRate1 + "M" + I.ToString() + "=" + InstRateConWet + ","
                    DownTime1 = DownTime1 + "M" + I.ToString() + "=" + DownTime(I).ToString() + ","
                    ProdDesignWaste1 = ProdDesignWaste1 + "M" + I.ToString() + "=" + ProdWaste(I).ToString() + ","
                    ProdDesignWaste1 = ProdDesignWaste1 + "W" + I.ToString() + "=" + DesignWaste(I).ToString() + ","
                Next

                WebWidth1 = WebWidth1.Remove(WebWidth1.Length - 1)
                MaxAnnRunHrs1 = MaxAnnRunHrs1.Remove(MaxAnnRunHrs1.Length - 1)
                InstantRate1 = InstantRate1.Remove(InstantRate1.Length - 1)
                DownTime1 = DownTime1.Remove(DownTime1.Length - 1)
                ProdDesignWaste1 = ProdDesignWaste1.Remove(ProdDesignWaste1.Length - 1)

                SqlWebWidth = SqlWebWidth + WebWidth1 + " WHERE CASEID =" + CaseID + ""
                SqlMaxAnnRunHrs = SqlMaxAnnRunHrs + MaxAnnRunHrs1 + " WHERE CASEID =" + CaseID + ""
                SqlInstantRate = SqlInstantRate + InstantRate1 + " WHERE CASEID =" + CaseID + ""
                SqlDownTime = SqlDownTime + DownTime1 + " WHERE CASEID =" + CaseID + ""
                SqlProdDesingWaste = SqlProdDesingWaste + ProdDesignWaste1 + " WHERE CASEID =" + CaseID + ""

                odbUtil.UpIns(SqlWebWidth, MyConnectionString)
                odbUtil.UpIns(SqlMaxAnnRunHrs, MyConnectionString)
                odbUtil.UpIns(SqlInstantRate, MyConnectionString)
                odbUtil.UpIns(SqlDownTime, MyConnectionString)
                odbUtil.UpIns(SqlProdDesingWaste, MyConnectionString)

            Catch ex As Exception
                Throw
            End Try

        End Sub

        Public Sub PersonnelUpdate(ByVal CaseID As String, ByVal PosDesc() As String, ByVal NumOfWorkers() As String, ByVal SalPref() As String, ByVal CostType() As String, ByVal ManfDept() As String)
            Try
                'Database Connection
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String
                MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")

                'Conversion Factor

                Dim StrSqlConvesrion As String = "SELECT CURR FROM PREFERENCES WHERE CASEID=" + CaseID + ""
                Dim Dts As New DataTable
                Dts = odbUtil.FillDataTable(StrSqlConvesrion, MyConnectionString)

                Dim Curr As String = Dts.Rows(0).Item("CURR").ToString()

                Dim SqlPosDesc As String = ""
                Dim SqlNumOfWorkers As String = ""
                Dim SqlSalPref As String = ""
                Dim SqlCostType As String = ""
                Dim SqlManfDept As String = ""


                Dim PosDesc1 As String = ""
                Dim NumOfWorkers1 As String = ""
                Dim SalPref1 As String = ""
                Dim CostType1 As String = ""
                Dim ManfDept1 As String = ""

                Dim SalPreConv As String = ""

                SqlPosDesc = "UPDATE PERSONNELPOS SET "
                SqlNumOfWorkers = "UPDATE PERSONNELNUM SET "
                SqlSalPref = "UPDATE PERSONNELSAL SET "
                SqlCostType = "UPDATE PERSONNELVP SET "
                SqlManfDept = "UPDATE PERSONNELDEP SET "

                Dim I As Integer

                For I = 1 To 30
                    SalPreConv = SalPref(I) / Curr
                    PosDesc1 = PosDesc1 + "M" + I.ToString() + "=" + PosDesc(I).ToString() + ","
                    NumOfWorkers1 = NumOfWorkers1 + "M" + I.ToString() + "=" + NumOfWorkers(I).ToString() + ","
                    SalPref1 = SalPref1 + "M" + I.ToString() + "=" + SalPreConv + ","
                    CostType1 = CostType1 + "M" + I.ToString() + "=" + CostType(I).ToString() + ","
                    ManfDept1 = ManfDept1 + "M" + I.ToString() + "=" + ManfDept(I).ToString() + ","
                Next


                PosDesc1 = PosDesc1.Remove(PosDesc1.Length - 1)
                NumOfWorkers1 = NumOfWorkers1.Remove(NumOfWorkers1.Length - 1)
                SalPref1 = SalPref1.Remove(SalPref1.Length - 1)
                CostType1 = CostType1.Remove(CostType1.Length - 1)
                ManfDept1 = ManfDept1.Remove(ManfDept1.Length - 1)


                SqlPosDesc = SqlPosDesc + PosDesc1 + "WHERE CASEID =" + CaseID + ""
                SqlNumOfWorkers = SqlNumOfWorkers + NumOfWorkers1 + "WHERE CASEID =" + CaseID + ""
                SqlSalPref = SqlSalPref + SalPref1 + "WHERE CASEID = " + CaseID + ""
                SqlCostType = SqlCostType + CostType1 + "WHERE CASEID = " + CaseID + ""
                SqlManfDept = SqlManfDept + ManfDept1 + "WHERE CASEID = " + CaseID + ""


                odbUtil.UpIns(SqlPosDesc, MyConnectionString)
                odbUtil.UpIns(SqlNumOfWorkers, MyConnectionString)
                odbUtil.UpIns(SqlSalPref, MyConnectionString)
                odbUtil.UpIns(SqlCostType, MyConnectionString)
                odbUtil.UpIns(SqlManfDept, MyConnectionString)

            Catch ex As Exception
                Throw
            End Try


        End Sub

        Public Sub PlantEnergyUpdate(ByVal CaseID As String, ByVal ElectricityPre As String, ByVal NaturalGas As String)


            'DataBase Connctions 
            Dim odbUtil As New DBUtil()
            Dim MyConnectionString As String
            MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")

            'Conversion Factor Sql
            Dim StrSqlPref As String = "Select curr from preferences where caseID =" + CaseID + ""
            Dim Dts As New DataTable()
            Dts = odbUtil.FillDataTable(StrSqlPref, MyConnectionString)

            Dim Curr As String = Dts.Rows(0).Item("CURR").ToString()

            Dim ElePreConv As String
            Dim NaturalGasConv As String
            ElePreConv = ElectricityPre / Curr
            NaturalGasConv = NaturalGas / Curr


            Dim SqlUpDateString As String = ""
            SqlUpDateString = "UPDATE PLANTENERGY SET ELECPRICE= " + ElePreConv + ","
            SqlUpDateString = SqlUpDateString + "NGASPRICE= " + NaturalGasConv + " WHERE CASEID = " + CaseID + ""

            odbUtil.UpIns(SqlUpDateString, MyConnectionString)




        End Sub

        Public Sub PlantSpaceUpdate(ByVal CaseID As String, ByVal whouse As String, ByVal off As String, ByVal supp As String, ByVal tot As String, ByVal prodHB As String, ByVal prodPartHB As String, ByVal prodStd As String, ByVal warehouseLR As String, ByVal offLR As String, ByVal suppLR As String)



            'DataBase Connctions 
            Dim odbUtil As New DBUtil()
            Dim MyConnectionString As String
            MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")


            'Conversion Factor Sql
            Dim StrSqlPref As String = "Select CONVAREA2, CURR from preferences where caseID =" + CaseID + ""
            Dim Dts As New DataTable()
            Dts = odbUtil.FillDataTable(StrSqlPref, MyConnectionString)

            Dim ConvArea As String = Dts.Rows(0).Item("CONVAREA2").ToString()
            Dim curr As String = Dts.Rows(0).Item("CURR").ToString()

            'Feild After Convrting to Orginal  Format
            Dim whouse1 As String = whouse / ConvArea
            Dim off1 As String = off / ConvArea
            Dim supp1 As String = supp / ConvArea
            Dim tot1 As String = tot / ConvArea
            Dim prodHB1 As String = prodHB / curr
            Dim prodPartHB1 As String = prodPartHB / curr
            Dim prodStd1 As String = prodStd / curr
            Dim warehouseLR1 As String = warehouseLR / curr
            Dim offLR1 As String = offLR / curr
            Dim suppLR1 As String = suppLR / curr



            'Update Query
            Dim StrSqlPlantSpaceUpdate As String = ""
            StrSqlPlantSpaceUpdate = "UPDATE PLANTSPACE SET "
            StrSqlPlantSpaceUpdate = StrSqlPlantSpaceUpdate + "M2=" + whouse1 + ","
            StrSqlPlantSpaceUpdate = StrSqlPlantSpaceUpdate + "M3=" + off1 + ","
            StrSqlPlantSpaceUpdate = StrSqlPlantSpaceUpdate + "M4=" + supp1 + ","
            StrSqlPlantSpaceUpdate = StrSqlPlantSpaceUpdate + "M5=" + tot1 + ","
            StrSqlPlantSpaceUpdate = StrSqlPlantSpaceUpdate + "PRODUCTIONLEASEHB=" + prodHB1 + ","
            StrSqlPlantSpaceUpdate = StrSqlPlantSpaceUpdate + "PRODUCTIONLEASEPHB=" + prodPartHB1 + ","
            StrSqlPlantSpaceUpdate = StrSqlPlantSpaceUpdate + "PRODUCTIONLEASE=" + prodStd1 + ","
            StrSqlPlantSpaceUpdate = StrSqlPlantSpaceUpdate + "WAREHOUSELEASE=" + warehouseLR1 + ","
            StrSqlPlantSpaceUpdate = StrSqlPlantSpaceUpdate + "OFFICELEASE=" + offLR1 + ","
            StrSqlPlantSpaceUpdate = StrSqlPlantSpaceUpdate + "SUPPORTLEASE=" + suppLR1 + "where caseID =" + CaseID + ""

            odbUtil.UpIns(StrSqlPlantSpaceUpdate, MyConnectionString)


        End Sub

        Public Sub FixedCostUpdate(ByVal CaseID As String, ByVal FixCostGuidVal() As String, ByVal FixedCostPre() As String, ByVal Department() As String, ByVal YearDepreciation As String)


            'Database Connection

            Dim odbUtil As New DBUtil()
            Dim MyConnectionString As String
            MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")

            'Conversion Factor

            Dim StrSqlConvesrion As String = "SELECT CURR FROM PREFERENCES WHERE CASEID=" + CaseID + ""
            Dim Dts As New DataTable
            Dts = odbUtil.FillDataTable(StrSqlConvesrion, MyConnectionString)

            ' declaring conversion


            Dim Curr As String = Dts.Rows(0).Item("CURR").ToString()
            Dim PrefCost As String = ""

            Dim SqlFixedCostGV As String = ""
            Dim SqlFixedCostPr As String = ""
            Dim SqlDept As String = ""
            Dim SqlYearToDepre As String = ""
            Dim FixedCostCurr As String = ""

            Dim FixedCostGV1 As String = ""
            Dim FixedCostPr1 As String = ""
            Dim Dept1 As String = ""
            Dim YearToDepre1 As String = ""

            'update Query

            SqlFixedCostGV = "UPDATE FIXEDCOSTPCT SET "
            SqlFixedCostPr = "UPDATE FIXEDCOSTPRE SET "
            SqlDept = "UPDATE FIXEDCOSTDEP SET "
            SqlYearToDepre = "UPDATE DEPRECIATION SET "

            Dim I As Integer

            For I = 1 To 30
                PrefCost = FixedCostPre(I).ToString() / Curr


                FixedCostGV1 = FixedCostGV1 + "M" + I.ToString() + "=" + FixCostGuidVal(I).ToString() + ","
                FixedCostPr1 = FixedCostPr1 + "M" + I.ToString() + "=" + PrefCost + ","
                Dept1 = Dept1 + "M" + I.ToString() + "=" + Department(I).ToString() + ","


            Next
            YearToDepre1 = "YEARS =" + YearDepreciation.ToString()

            FixedCostGV1 = FixedCostGV1.Remove(FixedCostGV1.Length - 1)
            FixedCostPr1 = FixedCostPr1.Remove(FixedCostPr1.Length - 1)
            Dept1 = Dept1.Remove(Dept1.Length - 1)

            SqlFixedCostGV = SqlFixedCostGV + FixedCostGV1 + " WHERE CASEID = " + CaseID + ""
            SqlFixedCostPr = SqlFixedCostPr + FixedCostPr1 + " WHERE CASEID = " + CaseID + ""
            SqlDept = SqlDept + Dept1 + " WHERE CASEID = " + CaseID + ""
            SqlYearToDepre = SqlYearToDepre + YearToDepre1 + " WHERE CASEID = " + CaseID + ""


            odbUtil.UpIns(SqlFixedCostGV, MyConnectionString)
            odbUtil.UpIns(SqlFixedCostPr, MyConnectionString)
            odbUtil.UpIns(SqlDept, MyConnectionString)

            odbUtil.UpIns(SqlYearToDepre, MyConnectionString)




        End Sub

        Public Sub CustomerAssumptionUpdate(ByVal CaseID As String, ByVal ProductPurchase As String, ByVal ShippingDistance As String, ByVal MileageCost As String)


            'DataBase Connctions 
            Dim odbUtil As New DBUtil()
            Dim MyConnectionString As String = ""
            MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")

            'Conversion Factor Sql
            Dim StrSqlPref As String = "SELECT CONVWT,CONVTHICK3,CURR  FROM PREFERENCES WHERE CASEID =" + CaseID + ""
            Dim Dts As New DataTable()
            Dts = odbUtil.FillDataTable(StrSqlPref, MyConnectionString)

            Dim convwt As String = Dts.Rows(0).Item("CONVWT").ToString()
            Dim convthick As String = Dts.Rows(0).Item("CONVTHICK3").ToString()
            Dim Curr As String = Dts.Rows(0).Item("CURR").ToString()


            Dim ProductConv As String = ""
            Dim ShippingConv As String = ""
            Dim MileageConv As String = ""

            ProductConv = ProductPurchase.ToString() / Curr * convwt
            ShippingConv = ShippingDistance.ToString() / convthick
            MileageConv = MileageCost.ToString() / Curr * convthick

            Dim SqlUpdateCustAssum As String = ""

            SqlUpdateCustAssum = "UPDATE CUSTOMERIN SET  M1=" + ProductConv + ","
            SqlUpdateCustAssum = SqlUpdateCustAssum + "M2=" + ShippingConv + ","
            SqlUpdateCustAssum = SqlUpdateCustAssum + "M3=" + MileageConv + " WHERE CASEID = " + CaseID + ""

            odbUtil.UpIns(SqlUpdateCustAssum, MyConnectionString)

        End Sub

        Public Sub UpdatePreferences(ByVal UserName As String, ByVal Unit As String, ByVal CurId As String, ByVal CurrEffdate As String)

            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = ""
                MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
                Dim StrSql As String = ""
                Dim StrSql1 As String = ""
                Dim StrSql2 As String = ""
                Dim StrSql3 As String = ""
                Dim Dts As New DataTable()
                Dim Dts1 As New DataTable()
                Dim ConversionFactor As String = ""
                Dim Convwt As String = ""
                Dim Convarea As String = ""
                Dim Convarea2 As String = ""
                Dim Convthick As String = ""
                Dim Convthick2 As String = ""
                Dim Convthick3 As String = ""
                Dim Convgallon As String = ""
                Dim Currency As String = ""
                Dim Curr As String = ""

                Dim Title1 As String = ""
                Dim Title2 As String = ""
                Dim Title3 As String = ""
                Dim Title4 As String = ""
                Dim Title6 As String = ""
                Dim Title7 As String = ""
                Dim Title8 As String = ""
                Dim Title9 As String = ""
                Dim Title10 As String = ""

                Dim UnitType As String = ""



                'For Unit Conversion
                StrSql = "SELECT "
                StrSql = StrSql + "MICPMIL, KGPLB, M2PMSI, M2PSQFT, MPFT, KMPMILE, JPMJ, LITPGAL, IN2PSQFT "
                StrSql = StrSql + "FROM CONVFACTORS "
                Dts = odbUtil.FillDataTable(StrSql, MyConnectionString)

                'For Currecny Conversion
                StrSql = "SELECT CURPUSD FROM CURRENCY WHERE CURID = " + CurId + ""
                Dts1 = odbUtil.FillDataTable(StrSql, MyConnectionString)


                Curr = Dts1.Rows(0).Item("CURPUSD").ToString

                If Unit = 0 Then
                    UnitType = "lb"
                    Title1 = "mil"
                    Title3 = "msi"
                    Title4 = "miles"
                    Title7 = "sq ft"
                    Title8 = "lb"
                    Title9 = "in"
                    Title10 = "gallon"

                    Convwt = "1"
                    Convarea = "1"
                    Convarea2 = "1"
                    Convthick = "1"
                    Convthick2 = "1"
                    Convthick3 = "1"
                    Currency = "1"
                    Convgallon = "1"


                Else
                    UnitType = "kg"
                    Title1 = "micron"
                    Title3 = "m2"
                    Title4 = "kilometers"
                    Title7 = "sq m"
                    Title8 = "kg"
                    Title9 = "mm"
                    Title10 = "liter"


                    Convwt = Dts.Rows(0).Item("KGPLB").ToString()
                    Convarea = Dts.Rows(0).Item("M2PMSI").ToString()
                    Convarea2 = Dts.Rows(0).Item("M2PSQFT").ToString()
                    Convthick = Dts.Rows(0).Item("MICPMIL").ToString()
                    Convthick2 = Dts.Rows(0).Item("MPFT").ToString()
                    Convthick3 = Dts.Rows(0).Item("KMPMILE").ToString()
                    Convgallon = Dts.Rows(0).Item("LITPGAL").ToString()



                End If


                Select Case CurId
                    Case 0 'US$

                        Title2 = "US$"
                        Title6 = "cents"
                    Case 1 'Chinese yuan

                        Title2 = "Yuan"
                        Title6 = "fen"

                    Case 2 'British pound

                        Title2 = "British pound"
                        Title6 = "pence"

                    Case 3 'German Euro

                        Title2 = "Euro"
                        Title6 = "Eurocent"

                    Case 4 'South Korea Won

                        Title2 = "Won"
                        Title6 = "jeon"

                    Case Else

                End Select




                'INSERTING THE CHARTPREFERENCES
                StrSql = "INSERT INTO CHARTPREFERENCES  "
                StrSql = StrSql + "(USERNAME) "
                StrSql = StrSql + "SELECT '" + UserName + "' FROM DUAL "
                StrSql = StrSql + "WHERE NOT EXISTS (SELECT 1 FROM CHARTPREFERENCES WHERE USERNAME='" + UserName + "')"
                odbUtil.UpIns(StrSql, MyConnectionString)


                'UPDATING THE CHARTPREFERENCES
                StrSql = "UPDATE CHARTPREFERENCES  "
                StrSql = StrSql + "SET "
                StrSql = StrSql + "UNITS=" + Unit + ", "
                StrSql = StrSql + "CURRENCY=" + CurId + ", "
                StrSql = StrSql + "CURR=" + Curr + ", "
                StrSql = StrSql + "CONVWT=" + Convwt + ", "
                StrSql = StrSql + "CONVAREA=" + Convarea + ", "
                StrSql = StrSql + "CONVAREA2=" + Convarea2 + ", "
                StrSql = StrSql + "CONVTHICK=" + Convthick + ", "
                StrSql = StrSql + "CONVTHICK2=" + Convthick2 + ", "
                StrSql = StrSql + "CONVTHICK3=" + Convthick3 + ", "
                StrSql = StrSql + "CONVGALLON=" + Convgallon + ", "
                If CurrEffdate.Trim.Length > 0 Then
                    StrSql = StrSql + "CURREFFDATE=TO_DATE('" + CurrEffdate + "','MM/DD/YYYY'), "
                End If
                StrSql = StrSql + "TITLE1='" + Title1 + "', "
                StrSql = StrSql + "TITLE2='" + Title2 + "', "
                StrSql = StrSql + "TITLE3='" + Title3 + "', "
                StrSql = StrSql + "TITLE4='" + Title4 + "', "
                StrSql = StrSql + "TITLE6='" + Title6 + "', "
                StrSql = StrSql + "TITLE7='" + Title7 + "', "
                StrSql = StrSql + "TITLE8='" + Title8 + "', "
                StrSql = StrSql + "TITLE9='" + Title9 + "', "
                StrSql = StrSql + "TITLE10='" + Title10 + "' "
                StrSql = StrSql + "WHERE USERNAME ='" + UserName + "' "
                odbUtil.UpIns(StrSql, MyConnectionString)



            Catch ex As Exception
                Throw
            End Try
        End Sub



    End Class
End Namespace