Imports Microsoft.VisualBasic
Imports System.Web.SessionState
Imports System.Data
Imports System.Data.OleDb
Imports System
Imports StandGetData
Imports StandUpInsData
Imports System.Math
Imports System.Web.UI.HtmlTextWriter


Public Class StandUpInsData
    Public Class UpdateInsert
        Dim SBAConnection As String = System.Configuration.ConfigurationManager.AppSettings("SBAConnectionString")
        Dim EconConnection As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
        Dim odbutil As New DBUtil()
#Region "Assumptions"

        Public Sub ExtrusionUpdate(ByVal CaseID As String, ByVal Material() As String, ByVal Thickness() As String, ByVal SGP() As String)
            Try

                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New StandGetData.Selectdata()

                'Preferences
                Dim dts As New DataSet()
                dts = ObjGetData.GetPref(CaseID)

                'Getting Values From Database assign to variable 
                Dim Convthick As String = dts.Tables(0).Rows(0).Item("convthick")
                Dim Convthick3 As String = dts.Tables(0).Rows(0).Item("convthick3")
                Dim Convwt As String = dts.Tables(0).Rows(0).Item("convwt")
                'Dim ConvGallon As String = dts.Tables(0).Rows(0).Item("CONVGALLON")
                Dim Curr As String = dts.Tables(0).Rows(0).Item("curr")
                Dim Mat As New Integer

                Dim StrSqlUpadate As String = ""
                Dim StrSqlIUpadate As String = ""
                StrSqlUpadate = "UPDATE MATERIALINPUT SET "
                For Mat = 0 To 9

                    Dim Matid As New Integer
                    Dim TotalThicKness As Decimal
                    TotalThicKness = CDbl(Thickness(Mat) * Convthick)

                    Matid = Mat + 1
                    If Material(Mat).ToString() > "500" And Material(Mat).ToString() < "506" Then
                        'Material 
                        StrSqlIUpadate = StrSqlIUpadate + "TYPEM" + Matid.ToString() + " = " + Material(Mat).ToString() + ","
                        'Preffered SG
                        StrSqlIUpadate = StrSqlIUpadate + "TYPEMSG" + Matid.ToString() + " = " + SGP(Mat).ToString() + ","
                        'Material 
                        StrSqlIUpadate = StrSqlIUpadate + " M" + Matid.ToString() + " = 0,"
                        'Thickness()
                        StrSqlIUpadate = StrSqlIUpadate + " T" + Matid.ToString() + " = 0,"
                    Else
                        'Material 
                        StrSqlIUpadate = StrSqlIUpadate + " M" + Matid.ToString() + " = " + Material(Mat).ToString() + ","
                        'Thickness()
                        StrSqlIUpadate = StrSqlIUpadate + " T" + Matid.ToString() + " = " + TotalThicKness.ToString() + ","
                        'Preffered SG
                        StrSqlIUpadate = StrSqlIUpadate + " SG" + Matid.ToString() + " = " + SGP(Mat).ToString() + ","
                        'Blend Material
                        StrSqlIUpadate = StrSqlIUpadate + " TYPEM" + Matid.ToString() + " = 0,"
                        'Blend Specific Gravity
                        StrSqlIUpadate = StrSqlIUpadate + " TYPEMSG" + Matid.ToString() + " = 0,"
                    End If
                Next

                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)

                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, SBAConnection)

            Catch ex As Exception
                Throw New Exception("StandUpdateData:ExtrusionUpdate:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub BarrierUpdateNew(ByVal CaseID As String, ByVal OTRTemp As String, ByVal WVTRTemp As String, ByVal OTRRH As String, ByVal WVTRRH As String, ByRef OTR() As String, ByVal WVTR() As String, ByVal TS1() As String, ByVal TS2() As String, ByVal GRADE() As String)
            Try


                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New StandGetData.Selectdata()


                'Preferences
                Dim dts As New DataSet()
                dts = ObjGetData.GetPref(CaseID)

                'Getting Values From Database assign to variable 
                Dim Convthick As String = dts.Tables(0).Rows(0).Item("convthick")
                Dim Convwt As String = dts.Tables(0).Rows(0).Item("convwt")
                Dim Curr As String = dts.Tables(0).Rows(0).Item("curr")
                Dim Conarea As String = dts.Tables(0).Rows(0).Item("CONVAREA")
                Dim ConPa As String = dts.Tables(0).Rows(0).Item("CONVPA")

                Dim Mat As New Integer
                Dim StrSqlUpadate As String = ""
                Dim StrSqlIUpadate As String = ""
                Dim totOTRTTemp As Decimal
                Dim totWVTRTemp As Decimal
                If dts.Tables(0).Rows(0).Item("UNITS").ToString <> 1 Then
                    totOTRTTemp = CDbl(OTRTemp - 32) * (5 / 9)
                    totWVTRTemp = CDbl(WVTRTemp - 32) * (5 / 9)
                Else
                    totOTRTTemp = CDbl(OTRTemp)
                    totWVTRTemp = CDbl(WVTRTemp)
                End If


                StrSqlUpadate = "UPDATE MATERIALINPUT SET"
                StrSqlIUpadate = StrSqlIUpadate + " OTRTEMP  = " + totOTRTTemp.ToString() + ","
                StrSqlIUpadate = StrSqlIUpadate + " WVTRTEMP  = " + totWVTRTemp.ToString() + ","
                StrSqlIUpadate = StrSqlIUpadate + " OTRRH  = " + OTRRH + ","
                StrSqlIUpadate = StrSqlIUpadate + " WVTRRH  = " + WVTRRH + ","
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, SBAConnection)


                StrSqlUpadate = String.Empty
                StrSqlIUpadate = String.Empty
                StrSqlUpadate = "UPDATE BARRIERINPUT SET"
                For Mat = 0 To 9
                    Dim Matid As New Integer
                    Matid = Mat + 1
                    'Material 

                    If IsNumeric(OTR(Mat)) Then
                        If dts.Tables(0).Rows(0).Item("UNITS").ToString <> 1 Then
                            OTR(Mat) = CDbl(OTR(Mat)) / (CDbl(Conarea) / 1000) / 100
                        End If
                        StrSqlIUpadate = StrSqlIUpadate + " OTR" + Matid.ToString() + " = " + CDbl(OTR(Mat)).ToString() + ","
                    Else
                        StrSqlIUpadate = StrSqlIUpadate + " OTR" + Matid.ToString() + " = null,"
                    End If
                    If IsNumeric(WVTR(Mat)) Then
                        If dts.Tables(0).Rows(0).Item("UNITS").ToString <> 1 Then
                            WVTR(Mat) = CDbl(WVTR(Mat)) / (CDbl(Conarea) / 1000) / 100
                        End If

                        StrSqlIUpadate = StrSqlIUpadate + " WVTR" + Matid.ToString() + " = " + CDbl(WVTR(Mat)).ToString() + ","
                    Else
                        StrSqlIUpadate = StrSqlIUpadate + " WVTR" + Matid.ToString() + " = null,"
                    End If

                    If IsNumeric(TS1(Mat)) Then
                        StrSqlIUpadate = StrSqlIUpadate + " TS1VAL" + Matid.ToString() + " = " + CDbl(TS1(Mat) * ConPa).ToString() + ","
                    Else
                        StrSqlIUpadate = StrSqlIUpadate + " TS1VAL" + Matid.ToString() + " = null,"
                    End If
                    If IsNumeric(TS2(Mat)) Then
                        StrSqlIUpadate = StrSqlIUpadate + " TS2VAL" + Matid.ToString() + " = " + CDbl(TS2(Mat) * ConPa).ToString() + ","
                    Else
                        StrSqlIUpadate = StrSqlIUpadate + " TS2VAL" + Matid.ToString() + " = null,"
                    End If

                    'If IsNumeric(GRADE(Mat)) Then
                    If GRADE(Mat) = "" Then
                        StrSqlIUpadate = StrSqlIUpadate + " GRADE" + Matid.ToString() + " = 0,"
                    Else
                        StrSqlIUpadate = StrSqlIUpadate + " GRADE" + Matid.ToString() + " = " + CDbl(GRADE(Mat)).ToString() + ","
                    End If

                    ' End If
                Next

                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, SBAConnection)

            Catch ex As Exception
                Throw New Exception("BarrierUpdateNew:" + ex.Message.ToString())
            End Try

        End Sub


        Public Sub PrefrencesUpdate(ByVal CaseID As String, ByVal Units As String, ByVal Effdate As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String
            Try
                StrSql = "UPDATE PREFERENCES  "
                StrSql = StrSql + "SET "
                StrSql = StrSql + "UNITS =" + Units + ", "
                StrSql = StrSql + "EFFDATE = TO_DATE('" + Effdate + "','MON DD,YYYY') "
                StrSql = StrSql + "WHERE CASEID =  " + CaseID + ""
                odbUtil.UpIns(StrSql, SBAConnection)

                StrSql = ""
                StrSql = "UPDATE MATERIALINPUT  "
                StrSql = StrSql + "SET "
                StrSql = StrSql + "EFFDATE = TO_DATE('" + Effdate + "','MON DD,YYYY') "
                StrSql = StrSql + "WHERE CASEID =  " + CaseID + ""
                odbUtil.UpIns(StrSql, SBAConnection)

                'StrSql = "UPDATE PLANTENERGY  "
                'StrSql = StrSql + "SET "
                'StrSql = StrSql + "EFFDATE = TO_DATE('" + Effdate + "','MON DD,YYYY') "
                'StrSql = StrSql + "WHERE CASEID =  " + CaseID + ""
                'odbUtil.UpIns(StrSql, SBAConnection)


                'StrSql = "UPDATE PERSONNELPOS  "
                'StrSql = StrSql + "SET "
                'StrSql = StrSql + "EFFDATE = TO_DATE('" + Effdate + "','MON DD,YYYY') "
                'StrSql = StrSql + "WHERE CASEID =  " + CaseID + ""
                'odbUtil.UpIns(StrSql, SBAConnection)

                PrefrencesCalc(CaseID, Units)
            Catch ex As Exception
                Throw New Exception("E1GetData:PrefrencesUpdate:" + ex.Message.ToString())
            End Try

        End Sub


        Protected Sub PrefrencesCalc(ByVal CaseID As String, ByVal Units As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String
            Dim dsConv As New DataSet()
            Dim ObjGetData As New StandGetData.Selectdata()

            Dim Title1 As String = String.Empty
            Dim Title3 As String = String.Empty
            Dim Title4 As String = String.Empty
            Dim Title7 As String = String.Empty
            Dim Title8 As String = String.Empty
            Dim Title9 As String = String.Empty
            Dim Title10 As String = String.Empty
            Dim Title15 As String = String.Empty
            Dim Title16 As String = String.Empty
            Dim Title19 As String = String.Empty
            Dim Title20 As String = String.Empty
            Dim Title21 As String = String.Empty

            Dim Convwt As New Decimal
            Dim Convarea As New Decimal
            Dim Convarea2 As New Decimal
            Dim Convthick As New Decimal
            Dim Convthick2 As New Decimal
            Dim Convthick3 As New Decimal
            Dim Convgallon As New Decimal
            Dim ConvMpa As New Decimal

            dsConv = ObjGetData.GetConversionFactor()


            Try



                If CInt(Units) = 0 Then

                    'Titles
                    Title1 = "mil"
                    Title3 = "msi"
                    Title4 = "miles"
                    Title7 = "sq ft"
                    Title8 = "lb"
                    Title9 = "in"
                    Title10 = "gallon"
                    Title15 = "in2"
                    Title16 = "tons"
                    Title19 = "in2"
                    Title20 = "°F"
                    Title21 = "lb/in²"
                    'Conversion
                    Convwt = CDbl(dsConv.Tables(0).Rows(0).Item("KGPLB").ToString())
                    Convarea = CDbl(dsConv.Tables(0).Rows(0).Item("M2PMSI").ToString())
                    Convarea2 = CDbl(dsConv.Tables(0).Rows(0).Item("M2PSQFT").ToString())
                    Convthick = CDbl(dsConv.Tables(0).Rows(0).Item("MICPMIL").ToString())
                    Convthick2 = CDbl(dsConv.Tables(0).Rows(0).Item("MPFT").ToString())
                    Convthick3 = CDbl(dsConv.Tables(0).Rows(0).Item("KMPMILE").ToString())
                    Convgallon = CDbl(dsConv.Tables(0).Rows(0).Item("LITPGAL").ToString())
                    ConvMpa = CDbl(dsConv.Tables(0).Rows(0).Item("MPAPLBIN2").ToString())

                Else

                    'Titles
                    Title1 = "micron"
                    Title3 = "m2"
                    Title4 = "kilometers"
                    Title7 = "sq m"
                    Title8 = "kg"
                    Title9 = "mm"
                    Title10 = "liter"
                    Title15 = "m2"
                    Title16 = "KN"
                    Title19 = "m2"
                    Title20 = "°C"
                    Title21 = "MPa"
                    'Conversion
                    Convwt = 1
                    Convarea = 1
                    Convarea2 = 1
                    Convthick = 1
                    Convthick2 = 1
                    Convthick3 = 1
                    Convgallon = 1
                    ConvMpa = 1
                End If

                StrSql = "UPDATE PREFERENCES  "
                StrSql = StrSql + "SET "
                StrSql = StrSql + "CONVWT=" + Convwt.ToString() + ", "
                StrSql = StrSql + "CONVAREA=" + Convarea.ToString() + ", "
                StrSql = StrSql + "CONVAREA2=" + Convarea2.ToString() + ", "
                StrSql = StrSql + "CONVTHICK=" + Convthick.ToString() + ", "
                StrSql = StrSql + "CONVTHICK2=" + Convthick2.ToString() + ", "
                StrSql = StrSql + "CONVTHICK3=" + Convthick3.ToString() + ", "
                StrSql = StrSql + "CONVPA=" + ConvMpa.ToString() + ", "
                'StrSql = StrSql + "CONVGALLON=" + Convgallon.ToString() + ", "
                StrSql = StrSql + "TITLE1='" + Title1 + "', "
                StrSql = StrSql + "TITLE3='" + Title3 + "', "
                StrSql = StrSql + "TITLE4='" + Title4 + "', "
                StrSql = StrSql + "TITLE7='" + Title7 + "', "
                StrSql = StrSql + "TITLE8='" + Title8 + "', "
                StrSql = StrSql + "TITLE9='" + Title9 + "', "
                StrSql = StrSql + "TITLE10='" + Title10 + "', "
                StrSql = StrSql + "TITLE15='" + Title15 + "', "
                StrSql = StrSql + "TITLE16='" + Title16 + "', "
                StrSql = StrSql + "TITLE19='" + Title19 + "', "
                StrSql = StrSql + "TITLE20='" + Title20 + "', "
                StrSql = StrSql + "TITLE21='" + Title21 + "' "
                StrSql = StrSql + "WHERE CASEID =  " + CaseID + ""
                odbUtil.UpIns(StrSql, SBAConnection)

            Catch ex As Exception
                Throw New Exception("StandGetData:PrefrencesCalc:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub ExtrusionUpdate_RH(ByVal CaseID As String, ByVal OutRH As String, ByVal InRH As String, ByVal WVTRTemp As String)
            Try

                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New StandGetData.Selectdata()

                'Preferences
                Dim dts As New DataSet()
                dts = ObjGetData.GetPref(CaseID)

                Dim StrSqlUpadate As String = ""
                Dim StrSqlIUpadate As String = ""

                Dim totWVTRTemp As Decimal
                If dts.Tables(0).Rows(0).Item("UNITS").ToString <> 1 Then
                    totWVTRTemp = CDbl(WVTRTemp - 32) * (5 / 9)
                Else
                    totWVTRTemp = CDbl(WVTRTemp)
                End If

                StrSqlUpadate = "UPDATE MATERIALINPUT SET"
                StrSqlIUpadate = StrSqlIUpadate + " WVTRTEMP  = " + totWVTRTemp.ToString() + ","
                StrSqlIUpadate = StrSqlIUpadate + " ORH  = " + OutRH + ","
                StrSqlIUpadate = StrSqlIUpadate + " IRH  = " + InRH + ","
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)

                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, SBAConnection)

            Catch ex As Exception
                Throw New Exception("StandUpdateData:ExtrusionUpdate_RH:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub UpdateCaseViewDet(ByVal CaseID As String, ByVal IsOTR As String, ByVal IsWVTR As String, ByVal IsTS1 As String, ByVal IsTS2 As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String
            Dim convPa As Double = 0.0
            Dim title21 As String = String.Empty
            Try
                StrSql = "UPDATE CASEVIEWDET  "
                StrSql = StrSql + "SET ISOTR='" + IsOTR + "',ISWVTR='" + IsWVTR + "',ISTS1='" + IsTS1 + "',ISTS2='" + IsTS2 + "' "
                StrSql = StrSql + "WHERE CASEID =  " + CaseID + ""
                odbUtil.UpIns(StrSql, SBAConnection)

            Catch ex As Exception
                Throw New Exception("E1GetData:PrefrencesUpdate:" + ex.Message.ToString())
            End Try

        End Sub

#End Region

#Region "Notes"
        Public Sub NotesUpdate(ByVal CaseId As String, ByVal AssumptionCode As String, ByVal Notes As String)
            Try


                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim StrSql As String = ""

                StrSql = "INSERT INTO NOTES  "
                StrSql = StrSql + "(CASEID,ASSUMPTIONTYPE,NOTE) "
                StrSql = StrSql + "SELECT " + CaseId + " ,ASSUMPTIONTYPES.ASSUMPTIONTYPECODE,'" + Notes + "' "
                StrSql = StrSql + "FROM ASSUMPTIONTYPES "
                StrSql = StrSql + "WHERE ASSUMPTIONTYPES.ASSUMPTIONTYPECODE='" + AssumptionCode + "' "
                StrSql = StrSql + "AND NOT EXISTS "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT 1 FROM NOTES "
                StrSql = StrSql + "WHERE NOTES.CASEID = " + CaseId + " "
                StrSql = StrSql + "AND NOTES.ASSUMPTIONTYPE= ASSUMPTIONTYPES.ASSUMPTIONTYPECODE "
                StrSql = StrSql + ") "
                odbUtil.UpIns(StrSql, SBAConnection)

                StrSql = "UPDATE NOTES  "
                StrSql = StrSql + "SET NOTE = '" + Notes + "' "
                StrSql = StrSql + "WHERE CASEID=  " + CaseId + "  "
                StrSql = StrSql + "AND ASSUMPTIONTYPE='" + AssumptionCode + "' "
                odbUtil.UpIns(StrSql, SBAConnection)

            Catch ex As Exception
                Throw New Exception("E1GetData:NotesUpdate:" + ex.Message.ToString())
            End Try

        End Sub
#End Region

#Region "ServerDate Update"
        Public Sub ServerDateUpdate(ByVal CaseId As Integer, ByVal UserName As String)
            Try
                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim StrSql As String = ""
                If CaseId < 10000 Then
                    StrSql = "Update BASECASES set ServerDate=SYSDATE WHERE CASEID=" + CaseId.ToString() + " "
                Else
                    'StrSql = "Update Permissionscases set ServerDate=SYSDATE WHERE CASEID=" + CaseId.ToString() + " AND UPPER(USERNAME)='" + UserName.ToUpper() + "' "
					StrSql = "Update Permissionscases set ServerDate=SYSDATE WHERE CASEID=" + CaseId.ToString() + " AND USERID=(SELECT USERID FROM ECON.USERS WHERE UPPER(USERNAME)='" + UserName.ToUpper() + "') "

                End If

                odbUtil.UpIns(StrSql, SBAConnection)

            Catch ex As Exception
                Throw New Exception("StandGetData:ServerDateUpdate:" + ex.Message.ToString())
            End Try

        End Sub
#End Region

#Region "Grouping"
        Public Sub UpdateGroupByCaseID(ByVal OldgrpID As String, ByVal grpID As String, ByVal CaseID As String)
            Dim DtsCount As New DataSet()
            Dim objGetData As New StandGetData.Selectdata()
            Dim seqCount As Integer = 0
            Dim odButil As New DBUtil()
            Dim strsql As String = String.Empty
            Dim StrSqlUpadate As String = ""
            Try
                'Deleting Previous Group Entry
                StrSqlUpadate = "DELETE FROM GROUPCASES WHERE GROUPID= " + OldgrpID + " AND CASEID=" + CaseID + " "
                odButil.UpIns(StrSqlUpadate, SBAConnection)


                'Updating Old Group Server Datae
                StrSqlUpadate = "UPDATE GROUPS  SET UPDATEDATE=sysdate WHERE GROUPID= " + OldgrpID + " "
                odButil.UpIns(StrSqlUpadate, SBAConnection)

                'Updating New Group Server Datae
                StrSqlUpadate = "UPDATE GROUPS  SET UPDATEDATE=sysdate WHERE GROUPID= " + grpID + " "
                odButil.UpIns(StrSqlUpadate, SBAConnection)

                DtsCount = objGetData.GetMaxSEQGCASE(grpID)
                If DtsCount.Tables(0).Rows.Count > 0 Then
                    seqCount = DtsCount.Tables(0).Rows(0).Item("MAXCOUNT").ToString()
                End If
                If grpID <> 0 Then
                    'Inserting new group Id
                    strsql = "INSERT INTO GROUPCASES  "
                    strsql = strsql + "( "
                    strsql = strsql + "GROUPCASEID, "
                    strsql = strsql + "GROUPID, "
                    strsql = strsql + "CASEID, "
                    strsql = strsql + "SEQ "
                    strsql = strsql + ") "
                    strsql = strsql + "SELECT SEQGROUPCASEID.NEXTVAL, "
                    strsql = strsql + grpID + ", "
                    strsql = strsql + CaseID + ", "
                    strsql = strsql + (seqCount + 1).ToString() + " "
                    strsql = strsql + "FROM DUAL "
                    strsql = strsql + "WHERE NOT EXISTS "
                    strsql = strsql + "( "
                    strsql = strsql + "SELECT 1 "
                    strsql = strsql + "FROM "
                    strsql = strsql + "GROUPCASES "
                    strsql = strsql + "WHERE "
                    strsql = strsql + "GROUPID=" + grpID + " AND "
                    strsql = strsql + "CASEID=" + CaseID + " "
                    strsql = strsql + ") "
                    odButil.UpIns(strsql, SBAConnection)
                End If
            Catch ex As Exception
                Throw New Exception("StandGetData:UpdateGroupByCaseID:" + ex.Message.ToString())
            End Try
        End Sub

        Public Sub UpdateGroupName(ByVal groupName() As String, ByVal groupDes() As String, ByVal groupTechDes() As String, ByVal groupApp() As String, ByVal Filename() As String, ByVal SponsorId() As String, ByVal GroupID() As String, ByVal count As Integer)
            Dim DtsCount As New DataSet()
            Dim objGetData As New E1GetData.Selectdata()
            Dim seqCount As Integer = 0
            Dim odButil As New DBUtil()
            Dim strsql As String = String.Empty
            Dim StrSqlUpadate As String = ""
            Dim i As Integer = 0
            Try
                For i = 0 To count - 1
                    If groupName(i) <> "" Then
                        StrSqlUpadate = "UPDATE GROUPS SET "
                        StrSqlUpadate = StrSqlUpadate + " DES1='" + groupName(i).ToString().Replace("'", "''") + "', "
                        StrSqlUpadate = StrSqlUpadate + " DES2='" + groupDes(i).ToString().Replace("'", "''") + "', "
                        StrSqlUpadate = StrSqlUpadate + " DES3=REPLACE(REPLACE('" + groupTechDes(i).ToString().Replace("'", "''") + "',chr(13)),chr(10),' '), "
                        StrSqlUpadate = StrSqlUpadate + " APPLICATION='" + groupApp(i).ToString().Replace("'", "''") + "', "
                        StrSqlUpadate = StrSqlUpadate + " FILENAME='" + Filename(i).ToString() + "' "
                        If SponsorId(i) <> "" Or SponsorId(i) <> Nothing Then
                            StrSqlUpadate = StrSqlUpadate + ", SUPPLIERID=" + SponsorId(i).ToString() + " "
                        End If
                        StrSqlUpadate = StrSqlUpadate + " WHERE GROUPID= " + GroupID(i).ToString()
                        odButil.UpIns(StrSqlUpadate, SBAConnection)
                    End If
                Next
            Catch ex As Exception
                Throw New Exception("StandGetData:UpdateGroupName:" + ex.Message.ToString())
            End Try
        End Sub

        Public Sub AddGroupName11(ByVal Des1 As String, ByVal Des2 As String, ByVal USERID As String, ByVal Type As String)
            Dim odButil As New DBUtil()
            Dim strsql As String = String.Empty
            Dim Dts As New DataSet
            Dim GROUPID As String = ""
            Dim i As Integer = 0
            Try
                'Getting GROUPID from Sequence
                strsql = String.Empty
                strsql = "SELECT SEQGROUPID.NEXTVAL AS GROUPID  "
                strsql = strsql + "FROM DUAL "
                Dts = odButil.FillDataSet(strsql, SBAConnection)
                If Dts.Tables(0).Rows.Count > 0 Then
                    GROUPID = Dts.Tables(0).Rows(0).Item("GROUPID").ToString()

                    'Inserting Into Groups Table
                    strsql = String.Empty
                    strsql = "INSERT INTO GROUPS  "
                    strsql = strsql + "( "
                    strsql = strsql + "GROUPID, "
                    strsql = strsql + "DES1, "
                    strsql = strsql + "DES2, "
                    strsql = strsql + "USERID, "
                    strsql = strsql + "CREATIONDATE, "
                    strsql = strsql + "UPDATEDATE, "
                    strsql = strsql + "GROUPTYPE "
                    strsql = strsql + ") "
                    strsql = strsql + "SELECT " + GROUPID + ", "
                    strsql = strsql + "'" + Des1 + "', "
                    strsql = strsql + "'" + Des2 + "', "
                    strsql = strsql + USERID + ", "
                    strsql = strsql + "sysdate, "
                    strsql = strsql + "sysdate, "
                    strsql = strsql + "'" + Type + "' "
                    strsql = strsql + "FROM DUAL "
                    strsql = strsql + "WHERE NOT EXISTS "
                    strsql = strsql + "( "
                    strsql = strsql + "SELECT 1 "
                    strsql = strsql + "FROM "
                    strsql = strsql + "GROUPS "
                    strsql = strsql + "WHERE "
                    strsql = strsql + "DES1='" + Des1 + "' "
                    strsql = strsql + "AND "
                    strsql = strsql + "DES2='" + Des2 + "' "
                    strsql = strsql + ") "
                    odButil.UpIns(strsql, SBAConnection)

                End If

            Catch ex As Exception
                Throw New Exception("StandGetData:AddGroupName:" + ex.Message.ToString())
            End Try
        End Sub

        Public Function AddGroupName(ByVal Des1 As String, ByVal Des2 As String, ByVal Des3 As String, ByVal Application As String, ByVal USERID As String, ByVal Type As String, ByVal LicenseId As String) As String
            Dim odButil As New DBUtil()
            Dim strsql As String = String.Empty
            Dim Dts As New DataSet
            Dim GROUPID As String = ""
            Dim i As Integer = 0
            Try
                'Getting GROUPID from Sequence
                strsql = String.Empty
                strsql = "SELECT SEQGROUPID.NEXTVAL AS GROUPID  "
                strsql = strsql + "FROM DUAL "
                Dts = odButil.FillDataSet(strsql, SBAConnection)
                If Dts.Tables(0).Rows.Count > 0 Then
                    GROUPID = Dts.Tables(0).Rows(0).Item("GROUPID").ToString()

                    'Inserting Into Groups Table
                    strsql = String.Empty
                    strsql = "INSERT INTO GROUPS  "
                    strsql = strsql + "( "
                    strsql = strsql + "GROUPID, "
                    strsql = strsql + "DES1, "
                    strsql = strsql + "DES2, "
                    strsql = strsql + "DES3, "
                    strsql = strsql + "APPLICATION, "
                    strsql = strsql + "USERID, "
                    strsql = strsql + "LICENSEID, "
                    strsql = strsql + "CREATIONDATE, "
                    strsql = strsql + "UPDATEDATE, "
                    strsql = strsql + "GROUPTYPE "
                    strsql = strsql + ") "
                    strsql = strsql + "SELECT " + GROUPID + ", "
                    strsql = strsql + "'" + Des1.ToString().Replace("'", "''") + "', "
                    strsql = strsql + "'" + Des2.ToString().Replace("'", "''") + "', "
                    strsql = strsql + "REPLACE(REPLACE('" + Des3.ToString().Replace("'", "''") + "',chr(13)),chr(10),' '), "
                    strsql = strsql + "'" + Application.ToString().Replace("'", "''") + "', "
                    strsql = strsql + USERID + ", "
                    strsql = strsql + LicenseId + ", "
                    strsql = strsql + "sysdate, "
                    strsql = strsql + "sysdate, "
                    strsql = strsql + "'" + Type + "' "
                    strsql = strsql + "FROM DUAL "
                    strsql = strsql + "WHERE NOT EXISTS "
                    strsql = strsql + "( "
                    strsql = strsql + "SELECT 1 "
                    strsql = strsql + "FROM "
                    strsql = strsql + "GROUPS "
                    strsql = strsql + "WHERE "
                    strsql = strsql + "DES1='" + Des1.ToString().Replace("'", "''") + "' "
                    strsql = strsql + "AND "
                    strsql = strsql + "DES2='" + Des2.ToString().Replace("'", "''") + "' "
                    strsql = strsql + ") "
                    odButil.UpIns(strsql, SBAConnection)

                End If
                Return GROUPID
            Catch ex As Exception
                Throw New Exception("StandGetData:AddGroupName:" + ex.Message.ToString())
            End Try
        End Function

        Public Sub DeleteGroups(ByVal grpId As String)
            Dim odButil As New DBUtil()
            Dim StrSqlUpadate As String = ""

            Try
                StrSqlUpadate = " DELETE FROM GROUPS WHERE GROUPID=" + grpId
                odButil.UpIns(StrSqlUpadate, SBAConnection)
                StrSqlUpadate = " DELETE FROM GROUPCASES WHERE GROUPID=" + grpId
                odButil.UpIns(StrSqlUpadate, SBAConnection)
            Catch ex As Exception
                Throw New Exception("E1Update:DeleteGroup:" + ex.Message.ToString())
            End Try
        End Sub
#End Region

#Region "LOG ENTRIES"
         Public Function InsertLog(ByVal UserId As String, ByVal PageId As String, ByVal ActivityType As String, ByVal SessionId As String) As Integer

            'Creating Database Connection
            Dim odbUtil As New DBUtil()
            Dim ObjGetData As New Selectdata()
            Dim StrSql As String = String.Empty
            Dim count As Integer

            Try
                StrSql = "SELECT SEQACTIVELOGCOUNT.NEXTVAL FROM DUAL"
                count = odbUtil.FillData(StrSql, SBAConnection)

                'Report Details
                StrSql = String.Empty
                StrSql = "INSERT INTO ACTIVITYLOG (LOGID,USERID,LOGINCOUNT,USERSEQUENCE,PAGEID,ACTIVITYDETAILS,ACTIVITYTIME,SESSIONID)  "
                StrSql = StrSql + "VALUES( SEQACTIVITYLOGWEB.NEXTVAL," + UserId + "," + count.ToString() + ",1,'" + PageId + "','" + ActivityType + "',sysdate,'" + SessionId + "')"
                odbUtil.UpIns(StrSql, SBAConnection)

                Return count
            Catch ex As Exception
                Throw New Exception("StandUpInsData:InsertLog:" + ex.Message.ToString())
            End Try

        End Function
        Public Sub InsertLog1(ByVal UserId As String, ByVal PageId As String, ByVal ActivityType As String, ByVal CaseId As String, ByVal logCount As String, ByVal SessionId As String, ByVal MatId As String, ByVal MatSup As String, ByVal flagSupId As String, ByVal gradeID As String, ByVal groupId As String)

            'Creating Database Connection
            Dim odbUtil As New DBUtil()
            Dim ObjGetData As New Selectdata()
            Dim StrSql As String = String.Empty
            Dim userseq As Integer

            Try
                StrSql = "SELECT NVL(MAX(USERSEQUENCE),1) USERSEQUENCE FROM ACTIVITYLOG WHERE LOGINCOUNT=" + logCount
                userseq = odbUtil.FillData(StrSql, SBAConnection) + 1

                'log Details
                StrSql = String.Empty
                StrSql = "INSERT INTO ACTIVITYLOG (LOGID,  "
                StrSql = StrSql + "USERID, "
                StrSql = StrSql + "LOGINCOUNT, "
                StrSql = StrSql + "USERSEQUENCE, "
                StrSql = StrSql + "PAGEID, "
                StrSql = StrSql + "ACTIVITYDETAILS, "
                StrSql = StrSql + "ACTIVITYTIME, "
                StrSql = StrSql + "CASEID, "
                StrSql = StrSql + "MATID, "
                StrSql = StrSql + "MATSUPPID, "
                StrSql = StrSql + "FLAGSUPPID, "
                StrSql = StrSql + "GRADEID, "
                StrSql = StrSql + "GROUPID, "
                StrSql = StrSql + "SESSIONID) "

                StrSql = StrSql + "VALUES( SEQACTIVITYLOGWEB.NEXTVAL,  "
                StrSql = StrSql + UserId + ", "
                StrSql = StrSql + logCount + ", "
                StrSql = StrSql + userseq.ToString() + " , "
                StrSql = StrSql + "'" + PageId + "', "
                StrSql = StrSql + "'" + ActivityType + "', "
                StrSql = StrSql + "sysdate, "
                StrSql = StrSql + "'" + CaseId + "', "
                StrSql = StrSql + "'" + MatId + "', "
                StrSql = StrSql + "'" + MatSup + "', "
                StrSql = StrSql + "'" + flagSupId + "', "
                StrSql = StrSql + "'" + gradeID + "', "
                StrSql = StrSql + "'" + groupId + "', "
                StrSql = StrSql + "'" + SessionId + " ') "

                odbUtil.UpIns(StrSql, SBAConnection)

            Catch ex As Exception
                Throw New Exception("StandUpInsData:InsertLog1:" + ex.Message.ToString())
            End Try

        End Sub

 Public Sub InsertLog3(ByVal UserId As String, ByVal PageId As String, ByVal ActivityType As String, ByVal CaseId As String, ByVal logCount As String, ByVal SessionId As String, ByVal TCaseId As String)

            'Creating Database Connection
            Dim odbUtil As New DBUtil()
            Dim ObjGetData As New Selectdata()
            Dim StrSql As String = String.Empty
            Dim userseq As Integer
            Dim dsMat As DataSet
            Try
                dsMat = ObjGetData.GetExistingMaterial(CaseId)

                For i = 1 To 10
                    If dsMat.Tables(0).Rows(0).Item("M" + i.ToString()) <> 0 Then
                        StrSql = "SELECT NVL(MAX(USERSEQUENCE),1) USERSEQUENCE FROM ACTIVITYLOG WHERE LOGINCOUNT=" + logCount
                        userseq = odbUtil.FillData(StrSql, SBAConnection) + 1

                        'log Details
                        StrSql = String.Empty
                         StrSql = "INSERT INTO ACTIVITYLOG (LOGID,USERID,LOGINCOUNT,USERSEQUENCE, PAGEID, "
                        StrSql = StrSql + "ACTIVITYDETAILS,ACTIVITYTIME, CASEID, MATID, SESSIONID) "

                        StrSql = StrSql + "VALUES (SEQACTIVITYLOGWEB.NEXTVAL, "
                        StrSql = StrSql + "" + UserId + ", "
                        StrSql = StrSql + "" + logCount + ", "
                        StrSql = StrSql + "" + userseq.ToString() + ", "
                        StrSql = StrSql + "'" + PageId + "', "
                        StrSql = StrSql + "'" + ActivityType + "',sysdate, "
                        StrSql = StrSql + "" + TCaseId + ", "
                        StrSql = StrSql + "'" + dsMat.Tables(0).Rows(0).Item("M" + i.ToString()).ToString() + "', "
                        StrSql = StrSql + "'" + SessionId + "' ) "
                        odbUtil.UpIns(StrSql, SBAConnection)
                    End If

                Next

            Catch ex As Exception
                Throw New Exception("StandUpInsData:InsertLog3:" + ex.Message.ToString())
            End Try

        End Sub

		
        Public Sub InsertFlagTemp(ByVal SessionId As String, ByVal FlagSupId As String, ByVal SupFlagImg As String, ByVal SupFlagUrl As String)


            'Creating Database Connection
            Dim StrSql As String = String.Empty
            Dim odbUtil As New DBUtil()
            Try
                'log Details
                StrSql = String.Empty

                StrSql = "INSERT INTO FLAGQUERYTEMP (SESSIONID,SUPPLIERIDS,SPONSORFLAGS,SPONSORURLS)  "
                StrSql = StrSql + "VALUES( '" + SessionId + "','" + FlagSupId + "','" + SupFlagImg + "','" + SupFlagUrl + "') "

                'StrSql = StrSql + "VALUES( SEQACTIVITYLOGWEB.NEXTVAL," + UserId + ",'" + UserName + "'," + logCount + "," + userseq.ToString() + ",'" + PageName + "','" + PageDetails + "','" + ActivityType + "',sysdate," + ReportId + ",'" + ReportName + "','" + SessionId + "')"
                odbUtil.UpIns(StrSql, SBAConnection)

            Catch ex As Exception
                Throw New Exception("StandUpInsData:InsertFlagTemp:" + ex.Message.ToString())
            End Try
        End Sub

        Public Sub DeleteFlagTemp(ByVal SessionId As String)

            'Creating Database Connection
            Dim StrSql As String = String.Empty
            Dim odbUtil As New DBUtil()
            Try
                'log Details
                StrSql = String.Empty

                StrSql = "DELETE FROM FLAGQUERYTEMP "
                StrSql = StrSql + "WHERE SESSIONID='" + SessionId + "' "

                odbUtil.UpIns(StrSql, SBAConnection)

            Catch ex As Exception
                Throw New Exception("StandUpInsData:DeleteFlagTemp:" + ex.Message.ToString())
            End Try
        End Sub

        Public Sub UpdateSponsor(ByVal name As String, ByVal email As String)

            'Creating Database Connection
            Dim odbUtil As New DBUtil()
            Dim ObjGetData As New Selectdata()
            Dim StrSql As String = String.Empty
            Dim supplierId As Integer

            Try
                StrSql = "SELECT MAX(SUPPLIERID)SUPPLIERID FROM SUPPLIER"
                supplierId = odbUtil.FillData(StrSql, SBAConnection) + 1

                'log Details
                StrSql = String.Empty

                StrSql = "INSERT INTO SUPPLIER (SUPPLIERID,  "
                StrSql = StrSql + "NAME,EMAILADDRESS) "


                StrSql = StrSql + "VALUES( " + supplierId.ToString() + ", "
                StrSql = StrSql + "'" + name + "', "
                StrSql = StrSql + "'" + email + " ') "

                'StrSql = StrSql + "VALUES( SEQACTIVITYLOGWEB.NEXTVAL," + UserId + ",'" + UserName + "'," + logCount + "," + userseq.ToString() + ",'" + PageName + "','" + PageDetails + "','" + ActivityType + "',sysdate," + ReportId + ",'" + ReportName + "','" + SessionId + "')"
                odbUtil.UpIns(StrSql, SBAConnection)

            Catch ex As Exception
                Throw New Exception("StandUpInsData:InsertLog4:" + ex.Message.ToString())
            End Try
        End Sub
		
        Public Function AddStructureGroup(ByVal Des1 As String, ByVal Des2 As String, ByVal Des3 As String, ByVal App As String, ByVal SPMessage As String, ByVal Sponsor As String, ByVal USERID As String, ByVal Type As String) As String
            Dim odButil As New DBUtil()
            Dim strsql As String = String.Empty
            Dim Dts As New DataSet
            Dim GROUPID As String = ""
            Dim i As Integer = 0
            Try
                'Getting GROUPID from Sequence
                strsql = String.Empty
                strsql = "SELECT SEQGROUPID.NEXTVAL AS GROUPID  "
                strsql = strsql + "FROM DUAL "
                Dts = odButil.FillDataSet(strsql, SBAConnection)
                If Dts.Tables(0).Rows.Count > 0 Then
                    GROUPID = Dts.Tables(0).Rows(0).Item("GROUPID").ToString()

                    'Inserting Into Groups Table
                    strsql = String.Empty
                    strsql = "INSERT INTO GROUPS  "
                    strsql = strsql + "( "
                    strsql = strsql + "GROUPID, "
                    strsql = strsql + "DES1, "
                    strsql = strsql + "DES2, "
                    strsql = strsql + "DES3, "
                    strsql = strsql + "APPLICATION, "
                    strsql = strsql + "FILENAME, "
                    strsql = strsql + "SUPPLIERID, "
                    strsql = strsql + "USERID, "
                    strsql = strsql + "CREATIONDATE, "
                    strsql = strsql + "UPDATEDATE, "
                    strsql = strsql + "GROUPTYPE "
                    strsql = strsql + ") "
                    strsql = strsql + "SELECT " + GROUPID + ", "
                    strsql = strsql + "'" + Des1.ToString().Replace("'", "''") + "', "
                    strsql = strsql + "'" + Des2.ToString().Replace("'", "''") + "', "
                    strsql = strsql + "REPLACE(REPLACE('" + Des3.ToString().Replace("'", "''") + "',chr(13)),chr(10),' '), "
                    strsql = strsql + "'" + App.ToString().Replace("'", "''") + "', "
                    strsql = strsql + "'" + SPMessage + "', "
                    strsql = strsql + "'" + Sponsor + "', "
                    strsql = strsql + USERID + ", "
                    strsql = strsql + "sysdate, "
                    strsql = strsql + "sysdate, "
                    strsql = strsql + "'" + Type + "' "
                    strsql = strsql + "FROM DUAL "
                    strsql = strsql + "WHERE NOT EXISTS "
                    strsql = strsql + "( "
                    strsql = strsql + "SELECT 1 "
                    strsql = strsql + "FROM "
                    strsql = strsql + "GROUPS "
                    strsql = strsql + "WHERE "
                    strsql = strsql + "DES1='" + Des1.ToString().Replace("'", "''") + "' "
                    strsql = strsql + "AND "
                    strsql = strsql + "DES2='" + Des2.ToString().Replace("'", "''") + "' "
                    strsql = strsql + ") "
                    odButil.UpIns(strsql, SBAConnection)

                End If
                Return GROUPID
            Catch ex As Exception
                Throw New Exception("StandGetData:AddStructureGroup:" + ex.Message.ToString())
            End Try
        End Function
		Public Sub AddStructureGroup(ByVal Des1 As String, ByVal Des2 As String, ByVal Des3 As String, ByVal App As String, ByVal Sponsor As String, ByVal USERID As String, ByVal Type As String)
            Dim odButil As New DBUtil()
            Dim strsql As String = String.Empty
            Dim Dts As New DataSet
            Dim GROUPID As String = ""
            Dim i As Integer = 0
            Try
                'Getting GROUPID from Sequence
                strsql = String.Empty
                strsql = "SELECT SEQGROUPID.NEXTVAL AS GROUPID  "
                strsql = strsql + "FROM DUAL "
                Dts = odButil.FillDataSet(strsql, SBAConnection)
                If Dts.Tables(0).Rows.Count > 0 Then
                    GROUPID = Dts.Tables(0).Rows(0).Item("GROUPID").ToString()

                    'Inserting Into Groups Table
                    strsql = String.Empty
                    strsql = "INSERT INTO GROUPS  "
                    strsql = strsql + "( "
                    strsql = strsql + "GROUPID, "
                    strsql = strsql + "DES1, "
                    strsql = strsql + "DES2, "
                    strsql = strsql + "DES3, "
                    strsql = strsql + "APPLICATION, "
                    strsql = strsql + "SUPPLIERID, "
                    strsql = strsql + "USERID, "
                    strsql = strsql + "CREATIONDATE, "
                    strsql = strsql + "UPDATEDATE, "
                    strsql = strsql + "GROUPTYPE "
                    strsql = strsql + ") "
                    strsql = strsql + "SELECT " + GROUPID + ", "
                    strsql = strsql + "'" + Des1 + "', "
                    strsql = strsql + "'" + Des2 + "', "
                    strsql = strsql + "'" + Des3 + "', "
                    strsql = strsql + "'" + App + "', "
                    strsql = strsql + "'" + Sponsor + "', "
                    strsql = strsql + USERID + ", "
                    strsql = strsql + "sysdate, "
                    strsql = strsql + "sysdate, "
                    strsql = strsql + "'" + Type + "' "
                    strsql = strsql + "FROM DUAL "
                    strsql = strsql + "WHERE NOT EXISTS "
                    strsql = strsql + "( "
                    strsql = strsql + "SELECT 1 "
                    strsql = strsql + "FROM "
                    strsql = strsql + "GROUPS "
                    strsql = strsql + "WHERE "
                    strsql = strsql + "DES1='" + Des1 + "' "
                    strsql = strsql + "AND "
                    strsql = strsql + "DES2='" + Des2 + "' "
                    strsql = strsql + ") "
                    odButil.UpIns(strsql, SBAConnection)

                End If

            Catch ex As Exception
                Throw New Exception("StandGetData:AddStructureGroup:" + ex.Message.ToString())
            End Try
        End Sub

#End Region

#Region "Comparison"

        Public Function AssumptionUpdate(ByVal Caseid As String, ByVal UserId As String) As Integer
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
                If (NewCaseArray.Length <= 10) Then

                    'SQL
                    Dim SqlId As String = "SELECT SEQASSUMPTIONID.nextval NewAssumptionId FROM dual"
                    Dim AssumptionID As Integer = oDbUtil.FillData(SqlId, SBAConnection)

                    Dim StrSqlInsert As String = "Insert into Assumptions (AssumptionId, Description, USERID, STRUCT1, STRUCT2, STRUCT3,"
                    StrSqlInsert = StrSqlInsert + "STRUCT4, STRUCT5, STRUCT6, STRUCT7, STRUCT8, STRUCT9, STRUCT10 ) "
                    StrSqlInsert = StrSqlInsert + "values (" + AssumptionID.ToString() + ",'New Comparision'," + UserId.ToString() + "," + NewCaseArray(0) + "," + NewCaseArray(1) + "," + NewCaseArray(2) + "," + NewCaseArray(3) + "," + NewCaseArray(4) + ","
                    StrSqlInsert = StrSqlInsert + "" + NewCaseArray(5) + "," + NewCaseArray(6) + "," + NewCaseArray(7) + "," + NewCaseArray(8) + "," + NewCaseArray(9) + "  "
                    StrSqlInsert = StrSqlInsert + ")"
                    oDbUtil.UpIns(StrSqlInsert, SBAConnection)
                    Return AssumptionID

                End If

            End If
        End Function

        Public Sub AlterComparison(ByVal Name As String, ByVal ID As String, ByVal AssID As String, ByVal UserId As String, ByVal Password As String)

            'Creating Database Connection
            Dim odButil As New DBUtil()
            Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Econ3ConnectionString")
            'SQL
            If ID = "1" Or ID = "2" Then
                Dim StrSqlSave As String = "UPDATE ASSUMPTIONS SET "
                StrSqlSave = StrSqlSave + "DESCRIPTION='" + Name + "' "
                StrSqlSave = StrSqlSave + "WHERE ASSUMPTIONID =" + AssID.ToString() + ""
                odButil.UpIns(StrSqlSave, SBAConnection)

            End If

            If ID = "3" Then

                Dim StrSqlDelete2 As String = "DELETE FROM ASSUMPTIONS WHERE ASSUMPTIONID=" + AssID.ToString() + " "
                StrSqlDelete2 = StrSqlDelete2 + "AND USERID=" + UserId.ToString() + "  "
                odButil.UpIns(StrSqlDelete2, SBAConnection)

            End If

            If ID = "4" Then
                Dim StrSqlSave As String = "UPDATE ASSUMPTIONS SET "
                StrSqlSave = StrSqlSave + " DESCRIPTION='" + Name + " '  "
                StrSqlSave = StrSqlSave + "WHERE ASSUMPTIONID =" + AssID.ToString() + ""
                odButil.UpIns(StrSqlSave, SBAConnection)
            End If



        End Sub

        Public Sub SharedCase(ByVal shareuserid As String, ByVal ID As String, ByVal Desc As String, ByVal StructIds As String)

            'Creating Database Connection
            Dim oDbUtil As New DBUtil()
            Dim CaseArray(9) As String
            Dim NewCaseArray(9) As String


            CaseArray = StructIds.Split(",")
            Dim I As Integer

            For I = 0 To 9
                If I <= UBound(CaseArray) Then
                    If CaseArray(I) <> "" Then
                        NewCaseArray(I) = CaseArray(I)
                    Else
                        NewCaseArray(I) = "NULL"
                    End If

                Else
                    NewCaseArray(I) = "NULL"
                End If
            Next

            Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Econ3ConnectionString")
            Dim StrsqlInsert As String = "INSERT INTO ASSUMPTIONS (USERID, ASSUMPTIONID,DESCRIPTION, STRUCT1, STRUCT2, STRUCT3,"
            StrsqlInsert = StrsqlInsert + "STRUCT4, STRUCT5, STRUCT6, STRUCT7, STRUCT8, STRUCT9, STRUCT10 ) "
            StrsqlInsert = StrsqlInsert + " SELECT " + shareuserid.ToString() + "," + ID + ",'" + Desc + "'," + NewCaseArray(0) + "," + NewCaseArray(1) + "," + NewCaseArray(2) + "," + NewCaseArray(3) + "," + NewCaseArray(4) + ","
            StrsqlInsert = StrsqlInsert + "" + NewCaseArray(5) + "," + NewCaseArray(6) + "," + NewCaseArray(7) + "," + NewCaseArray(8) + "," + NewCaseArray(9) + "   FROM DUAL "
            StrsqlInsert = StrsqlInsert + "WHERE NOT EXISTS (SELECT 1 FROM ASSUMPTIONS WHERE USERID=" + shareuserid.ToString() + " AND ASSUMPTIONID=" + ID + ")"
            oDbUtil.UpIns(StrsqlInsert, SBAConnection)
        End Sub

        Public Sub EditComparisonName(ByVal AID As String, ByVal CaseDes As String)
            Try
                Dim odButil As New DBUtil()
                Dim StrSqlUpadate As String = ""

                StrSqlUpadate = "UPDATE ASSUMPTIONS SET "
                StrSqlUpadate = StrSqlUpadate + " DESCRIPTION='" + CaseDes + " ' "
                StrSqlUpadate = StrSqlUpadate + " WHERE ASSUMPTIONID= " + AID.ToString()
                odButil.UpIns(StrSqlUpadate, SBAConnection)

            Catch ex As Exception

            End Try
        End Sub

        Public Sub EditComparison(ByVal AID As String, ByVal caseIds() As String, ByVal count As Integer)
            Try
                Dim odButil As New DBUtil()
                Dim i, val As Integer
                val = 1

                Dim StrSqlUpadate As String = ""
                Dim StrSqlIUpadate As String = ""
                StrSqlUpadate = "UPDATE ASSUMPTIONS SET "
                For i = count To 10
                    If (caseIds(val) <> Nothing) Then
                        StrSqlUpadate = StrSqlUpadate + "STRUCT" + i.ToString() + " =" + caseIds(val) + " ,"
                    Else
                        StrSqlUpadate = StrSqlUpadate + "STRUCT" + i.ToString() + " =NULL ,"
                    End If
                    val += 1
                Next
                StrSqlIUpadate = StrSqlUpadate.Remove(StrSqlUpadate.Length - 1)
                StrSqlIUpadate = StrSqlIUpadate + " WHERE ASSUMPTIONID= " + AID.ToString()

                odButil.UpIns(StrSqlIUpadate, SBAConnection)

            Catch ex As Exception

            End Try
        End Sub
#End Region

#Region "New Group"
        Public Sub EditGroups(ByVal grpID As String, ByVal caseId As String)
            Try
                Dim DtsCount As New DataSet()
                Dim odButil As New DBUtil()
                Dim objGetData As New StandGetData.Selectdata()
                Dim seqCount As Integer = 0
                Dim StrSqlUpadate As String = ""

                'Updating New Group Server Datae
                StrSqlUpadate = "UPDATE GROUPS  SET UPDATEDATE=sysdate WHERE GROUPID= " + grpID + " "
                odButil.UpIns(StrSqlUpadate, SBAConnection)

                StrSqlUpadate = String.Empty


                If (caseId <> Nothing) Then
                    'Count for number of structures in group
                    DtsCount = objGetData.GetMaxSEQGCASE(grpID)
                    If DtsCount.Tables(0).Rows.Count > 0 Then
                        seqCount = DtsCount.Tables(0).Rows(0).Item("MAXCOUNT").ToString()
                    End If
                    StrSqlUpadate = String.Empty
                    If grpID <> 0 Then
                        'Inserting new group Id
                        StrSqlUpadate = "INSERT INTO GROUPCASES  "
                        StrSqlUpadate = StrSqlUpadate + "( "
                        StrSqlUpadate = StrSqlUpadate + "GROUPCASEID, "
                        StrSqlUpadate = StrSqlUpadate + "GROUPID, "
                        StrSqlUpadate = StrSqlUpadate + "CASEID, "
                        StrSqlUpadate = StrSqlUpadate + "SEQ "
                        StrSqlUpadate = StrSqlUpadate + ") "
                        StrSqlUpadate = StrSqlUpadate + "SELECT SEQGROUPCASEID.NEXTVAL, "
                        StrSqlUpadate = StrSqlUpadate + grpID + ", "
                        StrSqlUpadate = StrSqlUpadate + caseId + ", "
                        StrSqlUpadate = StrSqlUpadate + (seqCount + 1).ToString() + " "
                        StrSqlUpadate = StrSqlUpadate + "FROM DUAL "
                        StrSqlUpadate = StrSqlUpadate + "WHERE NOT EXISTS "
                        StrSqlUpadate = StrSqlUpadate + "( "
                        StrSqlUpadate = StrSqlUpadate + "SELECT 1 "
                        StrSqlUpadate = StrSqlUpadate + "FROM "
                        StrSqlUpadate = StrSqlUpadate + "GROUPCASES "
                        StrSqlUpadate = StrSqlUpadate + "WHERE "
                        StrSqlUpadate = StrSqlUpadate + "GROUPID=" + grpID + " AND "
                        StrSqlUpadate = StrSqlUpadate + "CASEID=" + caseId + " "
                        StrSqlUpadate = StrSqlUpadate + ") "
                        odButil.UpIns(StrSqlUpadate, SBAConnection)
                    End If
                End If

            Catch ex As Exception
                Throw New Exception("StandUpInsData:EditGroups:" + ex.Message.ToString())
            End Try
        End Sub
        Public Sub DeleteCasesFrmGrp(ByVal grpID As String, ByVal caseId As String)
            Try
                Dim DtsCount As New DataSet()
                Dim odButil As New DBUtil()
                Dim objGetData As New StandGetData.Selectdata()
                Dim StrSqlUpadate As String = ""

                If (caseId <> Nothing) Then
                    StrSqlUpadate = String.Empty
                    If grpID <> 0 Then
                        'Deleting Previous Group Entry
                        StrSqlUpadate = "DELETE FROM GROUPCASES WHERE GROUPID= " + grpID + " AND CASEID=" + caseId + " "
                        odButil.UpIns(StrSqlUpadate, SBAConnection)
                        StrSqlUpadate = String.Empty
                        'Updating New Group Server Datae
                        StrSqlUpadate = "UPDATE GROUPS  SET UPDATEDATE=sysdate WHERE GROUPID= " + grpID + " "
                        odButil.UpIns(StrSqlUpadate, SBAConnection)
                    End If
                End If

            Catch ex As Exception
                Throw New Exception("StandUpInsData:DeleteCasesFrmGrp:" + ex.Message.ToString())
            End Try
        End Sub

        Public Sub DeleteStructureGrps(ByVal caseId As String)
            Try
                Dim DtsCount As New DataSet()
                Dim odButil As New DBUtil()
                Dim objGetData As New StandGetData.Selectdata()
                Dim StrSqlUpadate As String = ""
                Dim ds As New DataSet
                Dim i As Integer
                Dim grpID As String = ""

                StrSqlUpadate = "SELECT GROUPID FROM GROUPCASES WHERE CASEID= " + caseId + " "
                ds = odButil.FillDataSet(StrSqlUpadate, SBAConnection)

                If ds.Tables(0).Rows.Count > 0 Then
                    For i = 0 To ds.Tables(0).Rows.Count - 1
                        grpID = ds.Tables(0).Rows(i).Item("GROUPID").ToString()
                        If (caseId <> Nothing) Then
                            StrSqlUpadate = String.Empty

                            'Deleting Previous Group Entry
                            StrSqlUpadate = "DELETE FROM GROUPCASES WHERE CASEID=" + caseId + " AND GROUPID= " + grpID + " "
                            odButil.UpIns(StrSqlUpadate, SBAConnection)

                            StrSqlUpadate = String.Empty
                            'Updating New Group Server Datae
                            StrSqlUpadate = "UPDATE GROUPS  SET UPDATEDATE=sysdate WHERE GROUPID= " + grpID + " "
                            odButil.UpIns(StrSqlUpadate, SBAConnection)
                        End If

                    Next
                End If



            Catch ex As Exception
                Throw New Exception("StandUpInsData:DeleteCasesFrmGrp:" + ex.Message.ToString())
            End Try
        End Sub
#End Region

#Region "Blend"
        Public Sub BlendSubMatExtrusionUpdate(ByVal CaseID As String, ByVal Material As String, ByVal BThickness() As String, ByVal SGP() As String, ByVal BMaterial() As String, ByVal BOTR() As String, ByVal BWVTR() As String, ByVal BTS1() As String, ByVal BTS2() As String, ByVal BGrade() As String, ByVal BlType As String)
            Try
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New StandGetData.Selectdata()
                Dim dsMat As New DataSet()
                'Preferences
                Dim dts As New DataSet()
                dts = ObjGetData.GetPref(CaseID)

                'Getting Values From Database assign to variable 
                Dim Convthick As String = dts.Tables(0).Rows(0).Item("convthick")
                Dim Conarea As String = dts.Tables(0).Rows(0).Item("CONVAREA")
                Dim ConvPa As String = dts.Tables(0).Rows(0).Item("CONVPA")
                Dim Mat As New Integer
                Dim StrSqlUpadate As String = ""
                Dim StrSqlIUpadate As String = ""

                StrSqlUpadate = "DELETE FROM BLENDMATINPUT "
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + " AND TYPEM = " + Material.ToString()
                odbUtil.UpIns(StrSqlUpadate, SBAConnection)

                StrSqlUpadate = "INSERT INTO BLENDMATINPUT  "
                StrSqlUpadate = StrSqlUpadate + "(CASEID,TYPEID,TYPEM,BCM1,BCSG1,BCT1,BCOTR1,BCWVTR1,BCGRADE1,BCTS1VAL1,BCTS2VAL1,BCM2,BCSG2,BCT2,BCOTR2,BCWVTR2,BCGRADE2,BCTS1VAL2,BCTS2VAL2) "
                StrSqlUpadate = StrSqlUpadate + "SELECT " + CaseID + " ," + BlType + "," + Material + ", "
                For Mat = 0 To 1
                    Dim Matid As New Integer
                    Dim TotalThicKness As Decimal
                    TotalThicKness = CDbl(BThickness(Mat) * Convthick)
                    Matid = Mat + 1
                    StrSqlUpadate = StrSqlUpadate + "" + BMaterial(Mat).ToString() + ","
                    StrSqlUpadate = StrSqlUpadate + "" + SGP(Mat).ToString() + ","
                    StrSqlUpadate = StrSqlUpadate + "" + TotalThicKness.ToString() + ","

                    'OTR()
                    If IsNumeric(BOTR(Mat)) Then
                        If dts.Tables(0).Rows(0).Item("UNITS").ToString <> 1 Then
                            BOTR(Mat) = CDbl(BOTR(Mat)) / (CDbl(Conarea) / 1000) / 100
                        End If
                        StrSqlUpadate = StrSqlUpadate + " " + CDbl(BOTR(Mat)).ToString() + ","
                    Else
                        StrSqlUpadate = StrSqlUpadate + "null,"
                    End If

                    'WVTR()
                    If IsNumeric(BWVTR(Mat)) Then
                        If dts.Tables(0).Rows(0).Item("UNITS").ToString <> 1 Then
                            BWVTR(Mat) = CDbl(BWVTR(Mat)) / (CDbl(Conarea) / 1000) / 100
                        End If

                        StrSqlUpadate = StrSqlUpadate + " " + CDbl(BWVTR(Mat)).ToString() + ","
                    Else
                        StrSqlUpadate = StrSqlUpadate + "null,"
                    End If

                    'Blend Grade
                    If IsNumeric(BGrade(Mat)) Then
                        StrSqlUpadate = StrSqlUpadate + BGrade(Mat).ToString() + ","
                    Else
                        StrSqlUpadate = StrSqlUpadate + "null,"
                    End If

                    'Tensile Strength1
                    If IsNumeric(BTS1(Mat)) Then
                        StrSqlUpadate = StrSqlUpadate + (BTS1(Mat) * ConvPa).ToString + ","
                    Else
                        StrSqlUpadate = StrSqlUpadate + "null,"
                    End If

                    'Tensile Strength2
                    If IsNumeric(BTS2(Mat)) Then
                        StrSqlUpadate = StrSqlUpadate + (BTS2(Mat) * ConvPa).ToString() + ","
                    Else
                        StrSqlUpadate = StrSqlUpadate + "null,"
                    End If
                Next
                StrSqlUpadate = StrSqlUpadate.Remove(StrSqlUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + " FROM DUAL "

                odbUtil.UpIns(StrSqlUpadate, SBAConnection)

            Catch ex As Exception
                Throw New Exception("StandUpdateData:BlendSubMatExtrusionUpdate:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub BlendMaterialExtrusionUpdate(ByVal CaseID As String, ByVal Material As String, ByVal i As String)
            Try
                Dim odbUtil As New DBUtil()
                Dim StrSqlUpadate As String = ""
                Dim StrSqlIUpadate As String = ""

                StrSqlUpadate = "DELETE FROM BLENDMATINPUT "
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + " AND TYPEM = " + Material.ToString()

                odbUtil.UpIns(StrSqlUpadate, SBAConnection)

                StrSqlIUpadate = "UPDATE MATERIALINPUT SET "
                StrSqlIUpadate = StrSqlIUpadate + " TYPEM" + i.ToString() + " = 0,"
                StrSqlIUpadate = StrSqlIUpadate + " TYPEMSG" + i.ToString() + " = 0"
                StrSqlIUpadate = StrSqlIUpadate + " WHERE CASEID = " + CaseID

                odbUtil.UpIns(StrSqlIUpadate, SBAConnection)

            Catch ex As Exception
                Throw New Exception("StandUpdateData:BlendMaterialExtrusionUpdate:" + ex.Message.ToString())
            End Try

        End Sub
#End Region

#Region "Manage SA Users Admin Tool"

        Public Sub UpdateAdmin(ByVal UserId As String)
            Dim Ds, dsSer As New DataSet()
            Dim StrSql As String = String.Empty
            Try
                'Assign License To User
                StrSql = String.Empty
                StrSql = "UPDATE USERS "
                StrSql = StrSql + " SET ISIADMINLICUSR='Y' WHERE USERID=" + UserId + ""
                odbutil.UpIns(StrSql, EconConnection)
            Catch ex As Exception
            End Try
        End Sub

        Public Sub AddTransferStructLicense(ByVal licenseId As String, ByVal userId As String, ByVal Type As String)
            Dim StrSql As String = String.Empty
            Try
                'Update  License Count
                StrSql = "UPDATE TRANSSERV SET USERID1 =" + userId + " "
                StrSql = StrSql + "WHERE LICENSEID = " + licenseId + " AND NVL(USERID1,-1) =-1 AND SEQ=(SELECT NVL(MAX(SEQ)+1,1) FROM TRANSSERV WHERE LICENSEID=" + licenseId + " AND NVL(USERID1,-1)<>-1 AND TYPE='" + Type + "')"

                odbutil.UpIns(StrSql, SBAConnection)
            Catch ex As Exception
                Throw New Exception("UtilityUpdate:LicenseCountMnmg:" + ex.Message.ToString())
            End Try
        End Sub

        Public Sub AddStructUsers(ByVal UserId As String, ByVal serviceDesc As String)
            Dim Ds As New DataSet()
            Try
                Dim StrSql As String = String.Empty
                Dim Conn As String = String.Empty

                StrSql = "INSERT INTO USERPERMISSIONS (USERID,SERVICEID,USERROLE,MAXCASECOUNT)  "
                StrSql = StrSql + "SELECT " + UserId + ",(SELECT SERVICEID FROM SERVICES WHERE UPPER(SERVICEDE)='" + serviceDesc.ToUpper() + "'),'ReadWrite',500 FROM DUAL "
                StrSql = StrSql + "WHERE NOT EXISTS "
                StrSql = StrSql + " (SELECT 1 FROM USERPERMISSIONS "
                StrSql = StrSql + "WHERE USERID=" + UserId + " AND SERVICEID IN (SELECT SERVICEID FROM SERVICES WHERE UPPER(SERVICEDE)='" + serviceDesc.ToUpper() + "'))"

                odbutil.UpIns(StrSql, EconConnection)
            Catch ex As Exception
                Throw ex
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

                odbutil.UpIns(StrSql, EconConnection)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Sub DeleteOrderUsers(ByVal UserId As String, ByVal serviceDesc As String)
            Dim Ds As New DataSet()
            Try
                Dim StrSql As String = String.Empty
                Dim Conn As String = String.Empty

                StrSql = "DELETE FROM USERPERMISSIONS "
                StrSql = StrSql + "WHERE USERID=" + UserId + " AND SERVICEID IN (SELECT SERVICEID FROM SERVICES WHERE UPPER(SERVICEDE)='" + serviceDesc.ToUpper() + "') "

                odbutil.UpIns(StrSql, EconConnection)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Sub UpdateSLicenseData(ByVal licenseId As String, ByVal fUserId As String, ByVal tUser As String)
            Dim StrSql As String = String.Empty
            Try
                'Update  License Count
                StrSql = "UPDATE TRANSSERV SET USERID2 =(SELECT USERID FROM ECON.USERS WHERE UPPER(USERNAME)='" + tUser.ToUpper() + "'),COUNT=1 "
                StrSql = StrSql + "WHERE LICENSEID = " + licenseId + " AND USERID1 =" + fUserId + " AND TYPE='SA'  "
                odbutil.UpIns(StrSql, SBAConnection)

            Catch ex As Exception
                Throw New Exception("UpInsData:UpdateLicenseData:" + ex.Message.ToString())
            End Try
        End Sub
#End Region
    End Class

End Class
