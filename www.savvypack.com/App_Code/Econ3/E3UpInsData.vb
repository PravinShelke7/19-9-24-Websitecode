Imports Microsoft.VisualBasic
Imports System.Web.SessionState
Imports System.Data
Imports System.Data.OleDb
Imports System
Public Class E3UpInsData
    Public Class UpdateInsert

        Public Sub EditCOLWIDTH(ByVal AID As String, ByVal width As Integer)
            Try
                Dim odButil As New DBUtil()
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Econ3ConnectionString")
                Dim StrSqlUpadate As String = ""
                Dim StrSqlIUpadate As String = ""
                StrSqlUpadate = "UPDATE ASSUMPTIONS SET "
                StrSqlUpadate = StrSqlUpadate + "COLWIDTH=" + width.ToString() + " "
                StrSqlUpadate = StrSqlUpadate + " WHERE ASSUMPTIONID= " + AID.ToString()
                odButil.UpIns(StrSqlUpadate, MyConnectionString)

            Catch ex As Exception

            End Try
        End Sub

        Public Function AssumptionUpdate(ByVal Caseid As String) As Integer
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
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Econ3ConnectionString")


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
        End Function

        Public Sub SharedCase(ByVal shareUserId As String, ByVal ID As String)

            'Creating Database Connection
            Dim oDbUtil As New DBUtil()

            'SQL
            Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Econ3ConnectionString")
            Dim StrsqlInsert As String = "INSERT INTO PERMASSUMPTIONS (USERID, ASSUMPTIONID)"
            StrsqlInsert = StrsqlInsert + " SELECT " + shareUserId.ToString() + "," + ID + " FROM DUAL "
            StrsqlInsert = StrsqlInsert + "WHERE NOT EXISTS (SELECT 1 FROM PERMASSUMPTIONS WHERE USERID=" + shareUserId.ToString() + " AND ASSUMPTIONID=" + ID + ")"
            oDbUtil.UpIns(StrsqlInsert, MyConnectionString)

        End Sub

        Public Sub EditComparison(ByVal AID As String, ByVal caseIds() As String, ByVal count As Integer)
            Try
                Dim odButil As New DBUtil()
                Dim i, val As Integer
                val = 1
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Econ3ConnectionString")
                Dim StrSqlUpadate As String = ""
                Dim StrSqlIUpadate As String = ""
                StrSqlUpadate = "UPDATE ASSUMPTIONS SET "
                For i = count To 10
                    If (caseIds(val) <> Nothing) Then
                        StrSqlUpadate = StrSqlUpadate + "CASE" + i.ToString() + " =" + caseIds(val) + " ,"
                    Else
                        StrSqlUpadate = StrSqlUpadate + "CASE" + i.ToString() + " =NULL ,"
                    End If
                    val += 1
                Next
                StrSqlIUpadate = StrSqlUpadate.Remove(StrSqlUpadate.Length - 1)
                StrSqlIUpadate = StrSqlIUpadate + " WHERE ASSUMPTIONID= " + AID.ToString()

                odButil.UpIns(StrSqlIUpadate, MyConnectionString)

            Catch ex As Exception

            End Try
        End Sub
        Public Sub EditComparisonName(ByVal AID As String, ByVal CaseDes As String)
            Try
                Dim odButil As New DBUtil()
                Dim i, val As Integer
                val = 1
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Econ3ConnectionString")
                Dim StrSqlUpadate As String = ""
                Dim StrSqlIUpadate As String = ""
                StrSqlUpadate = "UPDATE ASSUMPTIONS SET "
                StrSqlUpadate = StrSqlUpadate + " DESCRIPTION='" + CaseDes + " ' "
                StrSqlUpadate = StrSqlUpadate + " WHERE ASSUMPTIONID= " + AID.ToString()
                odButil.UpIns(StrSqlUpadate, MyConnectionString)

            Catch ex As Exception

            End Try
        End Sub
        Public Sub AlterComparison(ByVal Name As String, ByVal ID As String, ByVal AssID As String, ByVal UserId As String)

            'Creating Database Connection
            Dim odButil As New DBUtil()
            Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Econ3ConnectionString")
            'SQL
            If ID = "1" Or ID = "2" Then
                Dim StrSqlSave As String = "UPDATE ASSUMPTIONS SET SAVED = 1 ,"
                StrSqlSave = StrSqlSave + " DESCRIPTION='" + Name.Replace("'", "''") + " '  "
                StrSqlSave = StrSqlSave + "WHERE ASSUMPTIONID =" + AssID.ToString() + ""
                odButil.UpIns(StrSqlSave, MyConnectionString)

                Dim StrSqlSaved As String = "Insert into PERMASSUMPTIONS (USERID , ASSUMPTIONID  ) select "
                StrSqlSaved = StrSqlSaved + "" + UserId.ToString() + "," + AssID.ToString() + " From Dual where not exists ( select 1 from PERMASSUMPTIONS "
                StrSqlSaved = StrSqlSaved + "Where PERMASSUMPTIONS.ASSUMPTIONID =" + AssID.ToString() + ")"
                odButil.UpIns(StrSqlSaved, MyConnectionString)

            End If

            If ID = "3" Then

                Dim StrSqlDelete As String = "DELETE FROM PERMASSUMPTIONS WHERE ASSUMPTIONID=" + AssID.ToString() + " AND USERID=" + UserId.ToString() + " "
                odButil.UpIns(StrSqlDelete, MyConnectionString)

                Dim StrSqlDelete2 As String = "DELETE FROM ASSUMPTIONS WHERE ASSUMPTIONID=" + AssID.ToString() + " "
                StrSqlDelete2 = StrSqlDelete2 + "AND NOT EXISTS  "
                StrSqlDelete2 = StrSqlDelete2 + "( "
                StrSqlDelete2 = StrSqlDelete2 + "SELECT 1 FROM PERMASSUMPTIONS "
                StrSqlDelete2 = StrSqlDelete2 + "WHERE ASSUMPTIONID= " + AssID.ToString() + " "
                StrSqlDelete2 = StrSqlDelete2 + ") "

            End If

            If ID = "4" Then
                Dim StrSqlSave As String = "UPDATE ASSUMPTIONS SET "
                StrSqlSave = StrSqlSave + " DESCRIPTION='" + Name + " '  "
                StrSqlSave = StrSqlSave + "WHERE ASSUMPTIONID =" + AssID.ToString() + ""
                odButil.UpIns(StrSqlSave, MyConnectionString)
            End If



        End Sub

#Region "Assumptions Update"
        Public Sub ExtrusionUpdate(ByVal CaseID As String, ByVal Material() As String, ByVal Thickness() As String, ByVal Perffred() As String, ByVal Recyle() As String, ByVal Extra() As String, ByVal Sg() As String, ByVal Dept() As String)
            Try


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
                Dim StrSqlRecyleUpdate As String = ""
                Dim StrSqlExtraUpdate As String = ""
                Dim StrSqlDeptUpdate As String = ""
                Dim StrSqlSgUpdate As String = ""

                StrSqlUpadate = "UPDATE MaterialInput SET"
                For Mat = 0 To 9

                    Dim Matid As New Integer
                    Dim TotalThicKness As Double
                    Dim Totalprice As Double
                    TotalThicKness = Thickness(Mat) / Convthick
                    Totalprice = Perffred(Mat) * Convwt / Curr
                    Matid = Mat + 1

                    'Material 
                    StrSqlMaterialUpdate = StrSqlMaterialUpdate + " M" + Matid.ToString() + " = " + Material(Mat).ToString() + ","

                    'Thickness
                    StrSqlThickNessUpdate = StrSqlThickNessUpdate + " T" + Matid.ToString() + " = " + TotalThicKness.ToString() + ","

                    'Preffred
                    StrSqlPreffredUpdate = StrSqlPreffredUpdate + " S" + Matid.ToString() + " = " + TotalThicKness.ToString() + ","

                    'Recycle
                    StrSqlRecyleUpdate = StrSqlPreffredUpdate + " S" + Matid.ToString() + " = " + Recyle(Mat).ToString() + ","


                    'Extra-Process
                    StrSqlExtraUpdate = StrSqlPreffredUpdate + " S" + Matid.ToString() + " = " + Extra(Mat).ToString() + ","

                    'Sg
                    StrSqlSgUpdate = StrSqlPreffredUpdate + " S" + Matid.ToString() + " = " + Sg(Mat).ToString() + ","


                    'Dept
                    StrSqlDeptUpdate = StrSqlPreffredUpdate + " S" + Matid.ToString() + " = " + Dept(Mat).ToString() + ","




                Next

                StrSqlDeptUpdate = StrSqlDeptUpdate.Remove(StrSqlDeptUpdate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlMaterialUpdate + StrSqlThickNessUpdate + StrSqlPreffredUpdate + StrSqlRecyleUpdate + StrSqlExtraUpdate + StrSqlSgUpdate + StrSqlDeptUpdate
                StrSqlUpadate = StrSqlUpadate + " WHERE caseID = " + CaseID + ""

                odbUtil.UpIns(StrSqlUpadate, MyConnectionString)

            Catch ex As Exception

            End Try

        End Sub
#End Region

#Region "User Category Update"
        Public Sub AddCategorySet(ByVal CatSetName As String, ByVal UserId As String, ByVal PageName As String)
            Try
                Dim odButil As New DBUtil()
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Econ3ConnectionString")
                Dim strsql As String = ""
                strsql = "INSERT INTO CATEGORYSET  "
                strsql = strsql + "( "
                strsql = strsql + "CATEGORYSETID, CATEGORYSETNAME,USERID,PAGENAME "
                strsql = strsql + ") "
                strsql = strsql + "SELECT SEQCATEGORYSETID.NEXTVAL, "
                strsql = strsql + "'" + CatSetName + "', "
                strsql = strsql + UserId + ","
                strsql = strsql + "'" + PageName + "' "
                strsql = strsql + "FROM DUAL "
                odButil.UpIns(strsql, MyConnectionString)
            Catch ex As Exception

            End Try
        End Sub
        Public Sub AddCategory(ByVal CatSetName() As String, ByVal CatSetId As String)
            Try
                Dim odButil As New DBUtil()
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Econ3ConnectionString")
                Dim strsql As String = ""
                Dim i As Integer = 0

                For i = 0 To CatSetName.Length
                    strsql = "INSERT INTO CATEGORY  "
                    strsql = strsql + "( "
                    strsql = strsql + "CATEGORYID, CATEGORYSETID, CATEGORYNAME "
                    strsql = strsql + ") "
                    strsql = strsql + "SELECT SEQCATEGORYID.NEXTVAL, "
                    strsql = strsql + CatSetId + ", "
                    strsql = strsql + "'" + CatSetName(i) + "' "
                    strsql = strsql + "FROM DUAL "
                    odButil.UpIns(strsql, MyConnectionString)
                Next


            Catch ex As Exception

            End Try
        End Sub
        Public Sub AddEditCatItem(ByVal CatId As String, ByVal CatVal() As String)
            Try
                Dim odButil As New DBUtil()
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Econ3ConnectionString")
                Dim strsql As String = ""
                Dim i As Integer = 0


                'Inserting Data
                For i = 0 To CatVal.Length - 1
                    strsql = "INSERT INTO  CATEGORYITEM  "
                    strsql = strsql + "(CATEGORYITEMID, CATEGORYID, ITEMNAME) "
                    strsql = strsql + "SELECT SEQCATEGORYITEMID.NEXTVAL, "
                    strsql = strsql + CatId + ", "
                    strsql = strsql + "'" + CatVal(i) + "' "
                    strsql = strsql + "FROM DUAL "
                    odButil.UpIns(strsql, MyConnectionString)
                Next


            Catch ex As Exception

            End Try
        End Sub
        Public Sub DeleteCatItem(ByVal CatId As String, ByVal itemName As String)
            Try
                Dim odButil As New DBUtil()
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Econ3ConnectionString")
                Dim strsql As String = ""
                strsql = "DELETE FROM  "
                strsql = strsql + "CATEGORYITEM "
                strsql = strsql + "WHERE CATEGORYID=" + CatId + " "
                strsql = strsql + "AND ITEMNAME IN (" + itemName + " ) "
                odButil.UpIns(strsql, MyConnectionString)
            Catch ex As Exception

            End Try
        End Sub
        Public Sub RenameCategory(ByVal CatId As String, ByVal CatName As String, ByVal CatSetid As String)
            Try
                Dim odButil As New DBUtil()
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Econ3ConnectionString")
                Dim strsql As String = ""
                strsql = "UPDATE CATEGORY  "
                strsql = strsql + "SET CATEGORYNAME='" + CatName + "' "
                strsql = strsql + "WHERE CATEGORYID= " + CatId + " "
                strsql = strsql + "AND CATEGORYSETID= " + CatSetid + " "
                odButil.UpIns(strsql, MyConnectionString)
            Catch ex As Exception

            End Try
        End Sub
        Public Sub DeleteCategory(ByVal CatId As String, ByVal CatSetid As String)
            Try
                Dim odButil As New DBUtil()
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Econ3ConnectionString")
                Dim strsql As String = ""

                'Delete Category
                strsql = "DELETE CATEGORY  "
                strsql = strsql + "WHERE CATEGORYID= " + CatId + " "
                strsql = strsql + "AND CATEGORYSETID= " + CatSetid + " "
                odButil.UpIns(strsql, MyConnectionString)

                'Delete Category Items
                strsql = "DELETE CATEGORYITEM  "
                strsql = strsql + "WHERE CATEGORYID= " + CatId + " "
                odButil.UpIns(strsql, MyConnectionString)
            Catch ex As Exception

            End Try
        End Sub
        Public Sub DeleteCategorySet(ByVal CatSetid As String)
            Try
                Dim odButil As New DBUtil()
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Econ3ConnectionString")
                Dim strsql As String = ""

                'Delete Category Items
                strsql = "DELETE FROM  "
                strsql = strsql + "CATEGORYITEM "
                strsql = strsql + "WHERE "
                strsql = strsql + "CATEGORYITEMID IN "
                strsql = strsql + "( "
                strsql = strsql + "SELECT CATEGORYITEMID FROM CATEGORYITEM "
                strsql = strsql + "INNER JOIN CATEGORY "
                strsql = strsql + "ON CATEGORY.CATEGORYID=CATEGORYITEM.CATEGORYID "
                strsql = strsql + "INNER JOIN CATEGORYSET "
                strsql = strsql + "ON CATEGORYSET.CATEGORYSETID=CATEGORY.CATEGORYSETID "
                strsql = strsql + "WHERE CATEGORYSET.CATEGORYSETID= " + CatSetid
                strsql = strsql + ") "
                odButil.UpIns(strsql, MyConnectionString)

                'Delete Category
                strsql = "DELETE FROM CATEGORY  "
                strsql = strsql + "WHERE "
                strsql = strsql + "CATEGORYSETID= " + CatSetid + " "
                odButil.UpIns(strsql, MyConnectionString)

                'Delete Category Set
                strsql = "DELETE CATEGORYSET  "
                strsql = strsql + "WHERE "
                strsql = strsql + "CATEGORYSETID= " + CatSetid + " "
                odButil.UpIns(strsql, MyConnectionString)



                odButil.UpIns(strsql, MyConnectionString)
            Catch ex As Exception

            End Try
        End Sub
#End Region
    End Class
End Class
