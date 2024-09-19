Imports Microsoft.VisualBasic
Imports System.Web.SessionState
Imports System.Data
Imports System.Data.OleDb
Imports System
Imports DistGetData
Imports System.Math
Imports System.Web.UI.HtmlTextWriter

Public Class DistUpInsData
    Class UpdateInsert
        Dim DistributionConnection As String = System.Configuration.ConfigurationManager.AppSettings("DistributionConnectionString")
#Region "Case Details"
        Public Sub CaseDesUpdate(ByVal CaseId As String, ByVal CaseDe1 As String, ByVal CaseDe2 As String)
            Try
                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New DistGetData.Selectdata()
                Dim StrSqlUpadate As String = ""
                StrSqlUpadate = "UPDATE PERMISSIONSCASES SET CASEDE1 ='" + CaseDe1 + "', CASEDE2 ='" + CaseDe2 + "' WHERE CASEID=" + CaseId.ToString() + " "
                odbUtil.UpIns(StrSqlUpadate, DistributionConnection)
            Catch ex As Exception
                Throw New Exception("DistUpInsData:CaseDesUpdate:" + ex.Message.ToString())
            End Try

        End Sub
#End Region

#Region "Assumptions"
        Public Sub ExtrusionUpdate(ByVal CaseID As String, ByVal Material() As String, ByVal Weight() As String, ByVal Price() As String, ByVal Recyle() As String, ByVal Extra() As String, ByVal PC() As String, ByVal Dept() As String, ByVal ModType() As String, ByVal ModCases() As String, ByVal plate As String, ByVal IsDisUpdate As Boolean)
            Try

                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New DistGetData.Selectdata()

                'Preferences
                Dim dts As New DataSet()
                dts = ObjGetData.GetPref(CaseID)

                'Getting Values From Database assign to variable 
                Dim Convthick As String = dts.Tables(0).Rows(0).Item("convthick")
                Dim Convwt As String = dts.Tables(0).Rows(0).Item("convwt")
                Dim Curr As String = dts.Tables(0).Rows(0).Item("curr")
                Dim Mat As New Integer
                Dim StrSqlUpadate As String = ""
                Dim StrSqlIUpadate As String = ""
                StrSqlUpadate = "UPDATE MATERIALINPUT SET"
                For Mat = 0 To 9

                    Dim Matid As New Integer
                    Dim TotalWeight As Decimal
                    Dim Totalprice As Decimal
                    TotalWeight = CDbl(Weight(Mat) / Convwt)
                    Totalprice = CDbl(Price(Mat) * Convwt / Curr)
                    Matid = Mat + 1

                    'Material 
                    StrSqlIUpadate = StrSqlIUpadate + " M" + Matid.ToString() + " = " + Material(Mat).ToString() + ","
                    'Thickness
                    StrSqlIUpadate = StrSqlIUpadate + " T" + Matid.ToString() + " = " + TotalWeight.ToString() + ","
                    'Price
                    StrSqlIUpadate = StrSqlIUpadate + " S" + Matid.ToString() + " = " + Totalprice.ToString() + ","
                    'Recycle
                    StrSqlIUpadate = StrSqlIUpadate + " R" + Matid.ToString() + " = " + Recyle(Mat).ToString() + ","
                    'Extra-Process
                    StrSqlIUpadate = StrSqlIUpadate + " E" + Matid.ToString() + " = " + Extra(Mat).ToString() + ","
                    'Sg
                    StrSqlIUpadate = StrSqlIUpadate + " IP" + Matid.ToString() + " = " + PC(Mat).ToString() + ","
                    'Dept
                    StrSqlIUpadate = StrSqlIUpadate + " D" + Matid.ToString() + " = " + Dept(Mat).ToString() + ","
                    'Mod Type Cases 
                    StrSqlIUpadate = StrSqlIUpadate + " MT" + Matid.ToString() + " = " + ModType(Mat).ToString() + ","
                    'modCase Cases 
                    StrSqlIUpadate = StrSqlIUpadate + " C" + Matid.ToString() + " = " + ModCases(Mat).ToString() + ","

                Next

                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, DistributionConnection)

                'Updating Printing Plates
                StrSqlUpadate = ""
                StrSqlIUpadate = ""
                StrSqlUpadate = "UPDATE MATERIALINPUT SET PLATE= " + plate.Replace(",", "").ToString()
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, DistributionConnection)

            Catch ex As Exception
                Throw New Exception("DistUpInsData:ExtrusionUpdate:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub PrefrencesUpdate(ByVal CaseID As String, ByVal Ocountry As String, ByVal DCountry As String, ByVal Currancy As String, ByVal Units As String, ByVal Effdate As String, ByVal PVOLUSE As String, ByVal ErgyCalc As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String
            Try
                StrSql = "UPDATE PREFERENCES  "
                StrSql = StrSql + "SET "
                StrSql = StrSql + "CURRENCY =" + Currancy + ", "
                StrSql = StrSql + "OCOUNTRY = " + Ocountry + ", "
                StrSql = StrSql + "DCOUNTRY =" + DCountry + ", "
                StrSql = StrSql + "UNITS =" + Units + ", "
                StrSql = StrSql + "ERGYCALC ='" + ErgyCalc + "', "
                StrSql = StrSql + "EFFDATE = TO_DATE('" + Effdate + "','MON DD,YYYY') "
                StrSql = StrSql + "WHERE CASEID =  " + CaseID + ""
                odbUtil.UpIns(StrSql, DistributionConnection)

                'StrSql = "UPDATE MATERIALINPUT  "
                'StrSql = StrSql + "SET "
                'StrSql = StrSql + "EFFDATE = TO_DATE('" + Effdate + "','MON DD,YYYY') "
                'StrSql = StrSql + "WHERE CASEID =  " + CaseID + ""
                'odbUtil.UpIns(StrSql, DistributionConnection)

                StrSql = "UPDATE PLANTENERGY  "
                StrSql = StrSql + "SET "
                StrSql = StrSql + "EFFDATE = TO_DATE('" + Effdate + "','MON DD,YYYY') "
                StrSql = StrSql + "WHERE CASEID =  " + CaseID + ""
                odbUtil.UpIns(StrSql, DistributionConnection)


                StrSql = "UPDATE PERSONNELPOS  "
                StrSql = StrSql + "SET "
                StrSql = StrSql + "EFFDATE = TO_DATE('" + Effdate + "','MON DD,YYYY') "
                StrSql = StrSql + "WHERE CASEID =  " + CaseID + ""
                odbUtil.UpIns(StrSql, DistributionConnection)

                StrSql = "UPDATE ResultsPL  "
                StrSql = StrSql + "SET "
                StrSql = StrSql + "PVOLUSE = " + PVOLUSE + " "
                StrSql = StrSql + "WHERE CASEID =  " + CaseID + ""
                odbUtil.UpIns(StrSql, DistributionConnection)

                PrefrencesCalc(CaseID, Currancy, Units)
            Catch ex As Exception
                Throw New Exception("DistUpInsData:PrefrencesUpdate:" + ex.Message.ToString())
            End Try

        End Sub

        Protected Sub PrefrencesCalc(ByVal CaseID As String, ByVal Currancy As String, ByVal Units As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String
            Dim dsConv As New DataSet()
            Dim dsCurr As New DataSet()
            Dim ObjGetData As New DistGetData.Selectdata()

            Dim Title1 As String = String.Empty
            Dim Title2 As String = String.Empty
            Dim Title3 As String = String.Empty
            Dim Title4 As String = String.Empty
            Dim Title6 As String = String.Empty
            Dim Title7 As String = String.Empty
            Dim Title8 As String = String.Empty
            Dim Title9 As String = String.Empty

            Dim Convwt As New Decimal
            Dim Convarea As New Decimal
            Dim Convarea2 As New Decimal
            Dim Convthick As New Decimal
            Dim Convthick2 As New Decimal
            Dim Convthick3 As New Decimal
            Dim Curr As New Decimal

            dsCurr = ObjGetData.GetCurrancyArch(CaseID, Currancy)
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

                    'Conversion
                    Convwt = 1
                    Convarea = 1
                    Convarea2 = 1
                    Convthick = 1
                    Convthick2 = 1
                    Convthick3 = 1



                Else

                    'Titles
                    Title1 = "micron"
                    Title3 = "m2"
                    Title4 = "kilometers"
                    Title7 = "sq m"
                    Title8 = "kg"
                    Title9 = "mm"

                    'Conversion
                    Convwt = CDbl(dsConv.Tables(0).Rows(0).Item("KGPLB").ToString())
                    Convarea = CDbl(dsConv.Tables(0).Rows(0).Item("M2PMSI").ToString())
                    Convarea2 = CDbl(dsConv.Tables(0).Rows(0).Item("M2PSQFT").ToString())
                    Convthick = CDbl(dsConv.Tables(0).Rows(0).Item("MICPMIL").ToString())
                    Convthick2 = CDbl(dsConv.Tables(0).Rows(0).Item("MPFT").ToString())
                    Convthick3 = CDbl(dsConv.Tables(0).Rows(0).Item("KMPMILE").ToString())

                End If

                Select Case CInt(Currancy)
                    Case 0
                        Curr = 1
                        Title2 = "US$"
                        Title6 = "cents"
                    Case 1
                        Curr = CDbl(dsCurr.Tables(0).Rows(0).Item("CURPUSD").ToString())
                        Title2 = "Yuan"
                        Title6 = "fen"
                    Case 2
                        Curr = CDbl(dsCurr.Tables(0).Rows(0).Item("CURPUSD").ToString())
                        Title2 = "British pound"
                        Title6 = "pence"
                    Case 3
                        Curr = CDbl(dsCurr.Tables(0).Rows(0).Item("CURPUSD").ToString())
                        Title2 = "Euro"
                        Title6 = "Eurocent"
                    Case 4
                        Curr = CDbl(dsCurr.Tables(0).Rows(0).Item("CURPUSD").ToString())
                        Title2 = "Won"
                        Title6 = "jeon"
                End Select

                StrSql = "UPDATE PREFERENCES  "
                StrSql = StrSql + "SET "
                StrSql = StrSql + "CURR=" + Curr.ToString() + ", "
                StrSql = StrSql + "CONVWT=" + Convwt.ToString() + ", "
                StrSql = StrSql + "CONVAREA=" + Convarea.ToString() + ", "
                StrSql = StrSql + "CONVAREA2=" + Convarea2.ToString() + ", "
                StrSql = StrSql + "CONVTHICK=" + Convthick.ToString() + ", "
                StrSql = StrSql + "CONVTHICK2=" + Convthick2.ToString() + ", "
                StrSql = StrSql + "CONVTHICK3=" + Convthick3.ToString() + ", "
                StrSql = StrSql + "TITLE1='" + Title1 + "', "
                StrSql = StrSql + "TITLE2='" + Title2 + "', "
                StrSql = StrSql + "TITLE3='" + Title3 + "', "
                StrSql = StrSql + "TITLE4='" + Title4 + "', "
                StrSql = StrSql + "TITLE6='" + Title6 + "', "
                StrSql = StrSql + "TITLE7='" + Title7 + "', "
                StrSql = StrSql + "TITLE8='" + Title8 + "', "
                StrSql = StrSql + "TITLE9='" + Title9 + "' "
                StrSql = StrSql + "WHERE CASEID =  " + CaseID + ""
                odbUtil.UpIns(StrSql, DistributionConnection)
            Catch ex As Exception
                Throw New Exception("DistUpInsData:PrefrencesUpdate:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub ProductFormatUpdate(ByVal CaseID As String, ByVal M1 As String, ByVal Input() As String)
            Try

                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New DistGetData.Selectdata()

                'Preferences
                Dim dts As New DataSet()
                dts = ObjGetData.GetPref(CaseID)

                'Getting Values From Database assign to variable a
                Dim Convthick As String = dts.Tables(0).Rows(0).Item("convthick")
                Dim Unit As String = dts.Tables(0).Rows(0).Item("Units")
                Dim i As New Integer
                Dim StrSqlUpadate As String = ""
                Dim StrSqlIUpadate As String = ""
                StrSqlUpadate = "UPDATE PRODUCTFORMATIN SET"
                StrSqlIUpadate = StrSqlIUpadate + " M1 = " + M1.ToString() + ","
                For i = 2 To 4

                    Dim InputDetails As Decimal
                    If i = 3 Then
                        If CInt(Unit) = 1 And CInt(M1) = 1 Then
                            InputDetails = CDbl(Input(i) / Convthick / 0.01204)
                        Else
                            InputDetails = CDbl(Input(i) / Convthick)
                        End If
                    Else
                        InputDetails = CDbl(Input(i) / Convthick)
                    End If

                    'Input Details 
                    StrSqlIUpadate = StrSqlIUpadate + " M" + i.ToString() + " = " + InputDetails.ToString() + ","

                Next
                For i = 5 To 6
                    StrSqlIUpadate = StrSqlIUpadate + " M" + i.ToString() + " = " + Input(i).ToString() + ","
                Next


                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""

                odbUtil.UpIns(StrSqlUpadate, DistributionConnection)

            Catch ex As Exception
                Throw New Exception("DistUpInsData:ProductFormatUpdate:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub PalletAndTruckUpdate(ByVal CaseID As String, ByVal Pallet() As String, ByVal Truck() As String)
            Try

                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New DistGetData.Selectdata()

                'Preferences
                Dim dts As New DataSet()
                dts = ObjGetData.GetPref(CaseID)

                'Getting Values From Database assign to variable a
                Dim Convthick As String = dts.Tables(0).Rows(0).Item("convthick")
                Dim Convwt As String = dts.Tables(0).Rows(0).Item("Convwt")
                Dim Unit As String = dts.Tables(0).Rows(0).Item("Units")
                Dim i As New Integer
                Dim StrSqlUpadate As String = ""
                Dim StrSqlIUpadate As String = ""
                StrSqlUpadate = "UPDATE TRUCKPALLETIN SET"

                For i = 1 To 3

                    Dim PalletVal As Decimal
                    Dim TruckVal As Decimal

                    PalletVal = CDbl(Pallet(i) / Convthick)
                    TruckVal = CDbl(Truck(i) / Convthick)

                    'Pallet Details 
                    StrSqlIUpadate = StrSqlIUpadate + " M" + i.ToString() + " = " + PalletVal.ToString() + ","

                    'Truck Details 
                    StrSqlIUpadate = StrSqlIUpadate + " T" + i.ToString() + " = " + TruckVal.ToString() + ","

                Next
                For i = 4 To 5
                    StrSqlIUpadate = StrSqlIUpadate + " M" + i.ToString() + " = " + Pallet(i).ToString() + ","

                    If i = 5 Then
                        StrSqlIUpadate = StrSqlIUpadate + " T" + i.ToString() + " = " + Truck(i).ToString() + ","
                    Else
                        StrSqlIUpadate = StrSqlIUpadate + " T" + i.ToString() + " = " + (Truck(i) / Convwt).ToString() + ","
                    End If
                Next


                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""

                odbUtil.UpIns(StrSqlUpadate, DistributionConnection)

            Catch ex As Exception
                Throw New Exception("DistUpInsData:PalletAndTruckUpdate:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub TransportConfigUpdate(ByVal CaseID As String, ByVal Col As String, ByVal TVAL() As String, ByVal tabId As Integer, ByVal transpCode As String)
            Try

                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New DistGetData.Selectdata()

                'Preferences
                Dim dts As New DataSet()
                Dim dtTransp As New DataSet()
                dts = ObjGetData.GetPref(CaseID)
                dtTransp = ObjGetData.GetTransportType(transpCode)
                'Getting Values From Database assign to variable a
                Dim Convthick As String = dts.Tables(0).Rows(0).Item("convthick")
                Dim Convwt As String = dts.Tables(0).Rows(0).Item("Convwt")
                Dim Unit As String = dts.Tables(0).Rows(0).Item("Units")
                Dim i As New Integer
                Dim StrSqlUpadate As String = ""
                Dim StrSqlIUpadate As String = ""
                StrSqlUpadate = "UPDATE TRNSPTCONFIG SET"

                For i = 1 To 10

                    Dim TValue As Decimal

                    If (i < 4) Then
                        TValue = CDbl(TVAL(i) / Convthick)
                    ElseIf (i < 6) Then
                        TValue = CDbl(TVAL(i).ToString())
                    ElseIf (i < 9) Then
                        TValue = CDbl(TVAL(i) / Convthick)
                    ElseIf (i < 11) Then
                        If i = 9 Then
                            TValue = CDbl(TVAL(i) / Convwt)
                        Else
                            TValue = CDbl(TVAL(i).ToString())
                        End If
                    End If
                    StrSqlIUpadate = StrSqlIUpadate + " " + Col + "" + i.ToString() + " = " + TValue.ToString() + ","
                Next



                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""

                odbUtil.UpIns(StrSqlUpadate, DistributionConnection)

                'Updating TRANSPORTATIONLEGS
                StrSqlUpadate = String.Empty
                StrSqlUpadate = "UPDATE TRANSPORTATIONLEGS "
                StrSqlUpadate = StrSqlUpadate + " SET M" + tabId.ToString() + "=" + dtTransp.Tables(0).Rows(0).Item("ID").ToString() + " "
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, DistributionConnection)

            Catch ex As Exception
                Throw New Exception("DistUpInsData:PalletAndTruckUpdate:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub PlantConfigUpdate(ByVal CaseID As String, ByVal DEPTA() As String, ByVal DEPTB() As String, ByVal DEPTC() As String, ByVal DEPTD() As String, ByVal DEPTE() As String, ByVal DEPTF() As String, ByVal DEPTG() As String, ByVal DEPTH() As String, ByVal DEPTI() As String, ByVal DEPTJ() As String)
            Try

                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New DistGetData.Selectdata()


                Dim i As New Integer
                Dim StrSqlUpadate As String = ""
                Dim StrSqlIUpadate As String = ""
                StrSqlUpadate = "UPDATE PlantCONFIG SET"
                For i = 0 To 9

                    Dim DeptId As New Integer
                    DeptId = i + 1


                    StrSqlIUpadate = StrSqlIUpadate + " m" + DeptId.ToString() + " = " + DEPTA(i).ToString() + ","

                    StrSqlIUpadate = StrSqlIUpadate + " t" + DeptId.ToString() + " = " + DEPTB(i).ToString() + ","

                    StrSqlIUpadate = StrSqlIUpadate + " s" + DeptId.ToString() + " = " + DEPTC(i).ToString() + ","

                    StrSqlIUpadate = StrSqlIUpadate + " Y" + DeptId.ToString() + " = " + DEPTD(i).ToString() + ","

                    StrSqlIUpadate = StrSqlIUpadate + " D" + DeptId.ToString() + " = " + DEPTE(i).ToString() + ","

                    StrSqlIUpadate = StrSqlIUpadate + " Z" + DeptId.ToString() + " = " + DEPTF(i).ToString() + ","

                    StrSqlIUpadate = StrSqlIUpadate + " B" + DeptId.ToString() + " = " + DEPTG(i).ToString() + ","

                    StrSqlIUpadate = StrSqlIUpadate + " R" + DeptId.ToString() + " = " + DEPTH(i).ToString() + ","

                    StrSqlIUpadate = StrSqlIUpadate + " K" + DeptId.ToString() + " = " + DEPTI(i).ToString() + ","

                    StrSqlIUpadate = StrSqlIUpadate + " P" + DeptId.ToString() + " = " + DEPTJ(i).ToString() + ","

                Next

                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, DistributionConnection)

                'Updating opDEPvol table
                StrSqlUpadate = ""
                StrSqlIUpadate = ""
                StrSqlUpadate = "UPDATE opDEPvol SET"
                For i = 0 To 9

                    Dim DeptId As New Integer
                    DeptId = i + 1
                    StrSqlIUpadate = StrSqlIUpadate + " PN" + DeptId.ToString() + " = " + DEPTA(i).ToString() + ","
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, DistributionConnection)

            Catch ex As Exception
                Throw New Exception("DistUpInsData:PlantConfigUpdate:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub EfficiencyUpdate(ByVal CaseID As String, ByVal MATA() As String, ByVal MATB() As String, ByVal MATC() As String, ByVal MATD() As String, ByVal MATE() As String, ByVal MATF() As String, ByVal MATG() As String, ByVal MATH() As String, ByVal MATI() As String, ByVal MATJ() As String)
            Try

                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New DistGetData.Selectdata()


                Dim i As New Integer
                Dim StrSqlUpadate As String = ""
                Dim StrSqlIUpadate As String = ""
                StrSqlUpadate = "UPDATE MaterialEFF SET"
                For i = 0 To 9

                    Dim MatVal As New Integer
                    MatVal = i + 1


                    StrSqlIUpadate = StrSqlIUpadate + " T" + MatVal.ToString() + " = " + MATA(i).ToString() + ","

                    StrSqlIUpadate = StrSqlIUpadate + " S" + MatVal.ToString() + " = " + MATB(i).ToString() + ","

                    StrSqlIUpadate = StrSqlIUpadate + " Y" + MatVal.ToString() + " = " + MATC(i).ToString() + ","

                    StrSqlIUpadate = StrSqlIUpadate + " D" + MatVal.ToString() + " = " + MATD(i).ToString() + ","

                    StrSqlIUpadate = StrSqlIUpadate + " E" + MatVal.ToString() + " = " + MATE(i).ToString() + ","

                    StrSqlIUpadate = StrSqlIUpadate + " Z" + MatVal.ToString() + " = " + MATF(i).ToString() + ","

                    StrSqlIUpadate = StrSqlIUpadate + " B" + MatVal.ToString() + " = " + MATG(i).ToString() + ","

                    StrSqlIUpadate = StrSqlIUpadate + " R" + MatVal.ToString() + " = " + MATH(i).ToString() + ","

                    StrSqlIUpadate = StrSqlIUpadate + " K" + MatVal.ToString() + " = " + MATI(i).ToString() + ","

                    StrSqlIUpadate = StrSqlIUpadate + " P" + MatVal.ToString() + " = " + MATJ(i).ToString() + ","



                Next


                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, DistributionConnection)

            Catch ex As Exception
                Throw New Exception("DistUpInsData:EfficiencyUpdate:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub EquipmentUpdate(ByVal CaseID As String, ByVal hidAssetId() As String, ByVal ASSETP() As String, ByVal PARP() As String, ByVal ECP() As String, ByVal NGCP() As String, ByVal hidAssetDep() As String, ByVal ASSETNUM() As String)
            Try

                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New DistGetData.Selectdata()

                'Preferences
                Dim dts As New DataSet()
                dts = ObjGetData.GetPref(CaseID)

                'Getting Values From Database assign to variable 
                Dim Convthick As String = dts.Tables(0).Rows(0).Item("convthick")
                Dim Convwt As String = dts.Tables(0).Rows(0).Item("convwt")
                Dim Curr As String = dts.Tables(0).Rows(0).Item("curr")
                Dim CONVAREA2 As String = dts.Tables(0).Rows(0).Item("CONVAREA2")
                Dim i As New Integer
                Dim AssetId As Integer
                Dim StrSqlUpadate As String = ""
                Dim StrSqlIUpadate As String = ""

                'Updating Equipment Asset
                StrSqlUpadate = "UPDATE equipmentTYPE SET"
                For i = 0 To 29
                    AssetId = i + 1
                    StrSqlIUpadate = StrSqlIUpadate + " M" + AssetId.ToString() + " = " + hidAssetId(i).ToString() + ","
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, DistributionConnection)

                'Updating Equipment Asset Number Preffered  
                StrSqlUpadate = "" 'Bug #375
                StrSqlIUpadate = ""
                StrSqlUpadate = "UPDATE EQUIPMENTNUMBER SET"
                For i = 0 To 29
                    StrSqlIUpadate = StrSqlIUpadate + " M" + (i + 1).ToString() + " = " + ASSETNUM(i).ToString() + ","
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, DistributionConnection)

                'Updating Equipment Asset Cost Preffered
                StrSqlUpadate = ""
                StrSqlIUpadate = ""
                Dim AssetPref As New Integer
                StrSqlUpadate = "UPDATE equipmentCOST SET"
                For i = 0 To 29
                    Dim AssetPreffered As Double
                    AssetPreffered = CDbl(ASSETP(i).Replace(",", "") / Curr)
                    AssetPref = i + 1
                    StrSqlIUpadate = StrSqlIUpadate + " M" + AssetPref.ToString() + " = " + AssetPreffered.ToString() + ","
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, DistributionConnection)

                'Updating Equipment Plant Area Preffered
                StrSqlUpadate = ""
                StrSqlIUpadate = ""
                Dim PlanAreaPref As New Integer
                StrSqlUpadate = "UPDATE EQUIPMENTAREA SET"
                For i = 0 To 29
                    Dim planArPreffered As Decimal
                    planArPreffered = CDbl(PARP(i).Replace(",", "") / CONVAREA2)
                    PlanAreaPref = i + 1
                    StrSqlIUpadate = StrSqlIUpadate + " M" + PlanAreaPref.ToString() + " = " + planArPreffered.ToString() + ","
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, DistributionConnection)

                'Updating Electricity consumption Preffered
                StrSqlUpadate = ""
                StrSqlIUpadate = ""
                Dim ECPref As New Integer
                StrSqlUpadate = "UPDATE EQUIPENERGYPREF SET"
                For i = 0 To 29
                    ECPref = i + 1
                    StrSqlIUpadate = StrSqlIUpadate + " M" + ECPref.ToString() + " = " + ECP(i).ToString().Replace(",", "") + ","
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, DistributionConnection)


                'Updating Natural Gas Preffered
                StrSqlUpadate = ""
                StrSqlIUpadate = ""
                Dim NgPref As New Integer
                StrSqlUpadate = "UPDATE EQUIPNATURALGASPREF SET"
                For i = 0 To 29
                    NgPref = i + 1
                    StrSqlIUpadate = StrSqlIUpadate + " M" + NgPref.ToString() + " = " + NGCP(i).ToString().Replace(",", "") + ","
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, DistributionConnection)


                'Updating(Department)
                StrSqlUpadate = ""
                StrSqlIUpadate = ""
                Dim AssetDept As New Integer
                StrSqlUpadate = "UPDATE EQUIPMENTDEP SET"
                For i = 0 To 29
                    AssetDept = i + 1
                    StrSqlIUpadate = StrSqlIUpadate + " M" + AssetDept.ToString() + " = " + hidAssetDep(i).ToString() + ","
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, DistributionConnection)

            Catch ex As Exception
                Throw New Exception("DistUpInsData:EquipmentUpdate:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub SupportEquipmentUpdate(ByVal CaseID As String, ByVal hidAssetId() As String, ByVal ASSETP() As String, ByVal ECP() As String, ByVal NGCP() As String, ByVal hidAssetDep() As String, ByVal ASSETNUM() As String)
            Try

                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New DistGetData.Selectdata()

                'Preferences
                Dim dts As New DataSet()
                dts = ObjGetData.GetPref(CaseID)

                'Getting Values From Database assign to variable 
                Dim Convthick As String = dts.Tables(0).Rows(0).Item("convthick")
                Dim Convwt As String = dts.Tables(0).Rows(0).Item("convwt")
                Dim Curr As String = dts.Tables(0).Rows(0).Item("curr")
                Dim CONVAREA2 As String = dts.Tables(0).Rows(0).Item("CONVAREA2")
                Dim i As New Integer
                Dim AssetId As Integer
                Dim StrSqlUpadate As String = ""
                Dim StrSqlIUpadate As String = ""

                'Updating Equipment Asset
                StrSqlUpadate = "UPDATE equipment2TYPE SET"
                For i = 0 To 29
                    AssetId = i + 1
                    StrSqlIUpadate = StrSqlIUpadate + " M" + AssetId.ToString() + " = " + hidAssetId(i).ToString() + ","
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, DistributionConnection)

                'Updating Equipment Asset Number Preffered
                StrSqlUpadate = ""
                StrSqlIUpadate = ""
                StrSqlUpadate = "UPDATE EQUIPMENT2NUMBER SET"
                For i = 0 To 29
                    StrSqlIUpadate = StrSqlIUpadate + " M" + (i + 1).ToString() + " = " + ASSETNUM(i).ToString() + ","
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, DistributionConnection)

                'Updating Equipment Asset Cost Preffered
                StrSqlUpadate = ""
                StrSqlIUpadate = ""
                Dim AssetPref As New Integer
                StrSqlUpadate = "UPDATE equipment2COST SET"
                For i = 0 To 29
                    Dim AssetPreffered As Decimal
                    AssetPreffered = CDbl(ASSETP(i).Replace(",", "") / Curr)
                    AssetPref = i + 1
                    StrSqlIUpadate = StrSqlIUpadate + " M" + AssetPref.ToString() + " = " + AssetPreffered.ToString() + ","
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, DistributionConnection)


                'Updating Electricity consumption Preffered
                StrSqlUpadate = ""
                StrSqlIUpadate = ""
                Dim ECPref As New Integer
                StrSqlUpadate = "UPDATE EQUIP2ENERGYPREF SET"
                For i = 0 To 29
                    ECPref = i + 1
                    StrSqlIUpadate = StrSqlIUpadate + " M" + ECPref.ToString() + " = " + ECP(i).ToString().Replace(",", "") + ","
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, DistributionConnection)


                'Updating Natural Gas Preffered
                StrSqlUpadate = ""
                StrSqlIUpadate = ""
                Dim NgPref As New Integer
                StrSqlUpadate = "UPDATE EQUIP2NATURALGASPREF SET"
                For i = 0 To 29
                    NgPref = i + 1
                    StrSqlIUpadate = StrSqlIUpadate + " M" + NgPref.ToString() + " = " + NGCP(i).ToString().Replace(",", "") + ","
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, DistributionConnection)


                'Updating(Department)
                StrSqlUpadate = ""
                StrSqlIUpadate = ""
                Dim AssetDept As New Integer
                StrSqlUpadate = "UPDATE Equipment2DEP SET"
                For i = 0 To 29
                    AssetDept = i + 1
                    StrSqlIUpadate = StrSqlIUpadate + " M" + AssetDept.ToString() + " = " + hidAssetDep(i).ToString() + ","
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, DistributionConnection)

            Catch ex As Exception
                Throw New Exception("DistUpInsData:SupportEquipmentUpdate:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub OperationInUpdate(ByVal CaseID As String, ByVal OMAXRH() As String, ByVal OPINSTR() As String, ByVal DT() As String, ByVal OPWASTE() As String)
            Try

                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New DistGetData.Selectdata()

                'Preferences
                Dim dts As New DataSet()
                dts = ObjGetData.GetPref(CaseID)

                'Getting Values From Database assign to variable 
                Dim Convthick2 As String = dts.Tables(0).Rows(0).Item("convthick2")
                Dim Convthick As String = dts.Tables(0).Rows(0).Item("convthick")
                Dim Convwt As String = dts.Tables(0).Rows(0).Item("convwt")
                Dim Curr As String = dts.Tables(0).Rows(0).Item("curr")
                Dim CONVAREA2 As String = dts.Tables(0).Rows(0).Item("CONVAREA2")
                Dim i As New Integer

                Dim StrSqlUpadate As String = ""
                Dim StrSqlIUpadate As String = ""


                'Updating Maximum Annual Run Hours
                StrSqlUpadate = ""
                StrSqlIUpadate = ""
                Dim maxRunHrs As New Integer
                StrSqlUpadate = "UPDATE OPmaxRUNhrs SET"
                For i = 0 To 29
                    maxRunHrs = i + 1
                    StrSqlIUpadate = StrSqlIUpadate + " M" + maxRunHrs.ToString() + " = " + OMAXRH(i).ToString() + ","
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, DistributionConnection)


                'Updating Instantaneous Rate 
                StrSqlUpadate = ""
                StrSqlIUpadate = ""
                Dim instRate As New Integer
                StrSqlUpadate = "UPDATE OPinstGRSrate SET"
                For i = 0 To 29
                    instRate = i + 1
                    Dim dblInstRate As Decimal = CDbl(OPINSTR(i) / Convwt)
                    StrSqlIUpadate = StrSqlIUpadate + " M" + instRate.ToString() + " = " + dblInstRate.ToString() + ","
                Next


                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, DistributionConnection)


                'Updating Downtime 
                StrSqlUpadate = ""
                StrSqlIUpadate = ""
                Dim downT As New Integer
                StrSqlUpadate = "UPDATE OPdowntime SET"
                For i = 0 To 29
                    downT = i + 1
                    StrSqlIUpadate = StrSqlIUpadate + " M" + downT.ToString() + " = " + DT(i).ToString() + ","
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, DistributionConnection)

                'Updating waste
                StrSqlUpadate = ""
                StrSqlIUpadate = ""
                Dim wasteVal As New Integer
                StrSqlUpadate = "UPDATE OPwaste SET"
                For i = 0 To 29
                    wasteVal = i + 1
                    StrSqlIUpadate = StrSqlIUpadate + " M" + wasteVal.ToString() + " = " + OPWASTE(i).ToString() + ","
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, DistributionConnection)




            Catch ex As Exception
                Throw New Exception("DistUpInsData:OperationInUpdate:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub PersonnelInUpdate(ByVal CaseID As String, ByVal PosDes() As String, ByVal NoWorker() As String, ByVal PrefSal() As String, ByVal CostType() As String, ByVal DEPTID() As String)
            Try

                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New DistGetData.Selectdata()

                'Preferences
                Dim dts As New DataSet()
                dts = ObjGetData.GetPref(CaseID)

                'Getting Values From Database assign to variable 
                Dim Convthick2 As String = dts.Tables(0).Rows(0).Item("convthick2")
                Dim Convthick As String = dts.Tables(0).Rows(0).Item("convthick")
                Dim Convwt As String = dts.Tables(0).Rows(0).Item("convwt")
                Dim Curr As String = dts.Tables(0).Rows(0).Item("curr")
                Dim CONVAREA2 As String = dts.Tables(0).Rows(0).Item("CONVAREA2")
                Dim i As New Integer

                Dim StrSqlUpadate As String = ""
                Dim StrSqlIUpadate As String = ""


                'Updating Position Description
                StrSqlUpadate = ""
                StrSqlIUpadate = ""
                Dim intPosDes As New Integer
                StrSqlUpadate = "UPDATE PersonnelPOS SET"
                For i = 0 To 29
                    intPosDes = i + 1
                    StrSqlIUpadate = StrSqlIUpadate + " M" + intPosDes.ToString() + " = " + PosDes(i).ToString() + ","
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, DistributionConnection)


                'Updating No of workers
                StrSqlUpadate = ""
                StrSqlIUpadate = ""
                Dim intNoOfWork As New Integer
                StrSqlUpadate = "UPDATE PersonnelNUM SET"
                For i = 0 To 29
                    intNoOfWork = i + 1
                    StrSqlIUpadate = StrSqlIUpadate + " M" + intNoOfWork.ToString() + " = " + NoWorker(i).ToString() + ","
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, DistributionConnection)


                'Updating  Salary Preffered
                StrSqlUpadate = ""
                StrSqlIUpadate = ""
                Dim intSalPref As New Integer
                Dim SalPrefCal As Decimal
                StrSqlUpadate = "UPDATE PersonnelSAL SET"
                For i = 0 To 29
                    intSalPref = i + 1
                    SalPrefCal = CDbl(PrefSal(i) / Curr)
                    StrSqlIUpadate = StrSqlIUpadate + " M" + intSalPref.ToString() + " = " + SalPrefCal.ToString() + ","
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, DistributionConnection)

                'Updating Cost Type
                StrSqlUpadate = ""
                StrSqlIUpadate = ""
                Dim wasteVal As New Integer
                StrSqlUpadate = "UPDATE PERSONNELVP SET"
                For i = 0 To 29
                    wasteVal = i + 1
                    StrSqlIUpadate = StrSqlIUpadate + " M" + wasteVal.ToString() + " = " + CostType(i).ToString() + ","
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, DistributionConnection)

                'Updating Department
                StrSqlUpadate = ""
                StrSqlIUpadate = ""
                Dim intDept As New Integer
                StrSqlUpadate = "UPDATE PersonnelDEP SET"
                For i = 0 To 29
                    intDept = i + 1
                    StrSqlIUpadate = StrSqlIUpadate + " M" + intDept.ToString() + " = " + DEPTID(i).ToString() + ","
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, DistributionConnection)

            Catch ex As Exception
                Throw New Exception("DistUpInsData:PersonnelInUpdate:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub PlantConfig2Update(ByVal CaseID As String, ByVal AREA() As String, ByVal PrefLease() As String)
            Try

                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New DistGetData.Selectdata()

                'Preferences
                Dim dts As New DataSet()
                dts = ObjGetData.GetPref(CaseID)

                'Getting Values From Database assign to variable 
                Dim Convthick As String = dts.Tables(0).Rows(0).Item("convthick")
                Dim Convwt As String = dts.Tables(0).Rows(0).Item("convwt")
                Dim Curr As String = dts.Tables(0).Rows(0).Item("curr")
                Dim CONVAREA2 As String = dts.Tables(0).Rows(0).Item("CONVAREA2")
                Dim UNITS As String = dts.Tables(0).Rows(0).Item("units")
                Dim i As New Integer
                Dim StrSqlUpadate As String = ""
                Dim StrSqlIUpadate As String = ""

                'Updating Plant Space Area
                Dim calVal As Decimal
                StrSqlUpadate = "UPDATE plantSPACE SET"
                For i = 2 To 4
                    calVal = CDbl(AREA(i - 2).Replace(",", "") / CONVAREA2)
                    StrSqlIUpadate = StrSqlIUpadate + " M" + i.ToString() + " = " + calVal.ToString() + ","
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, DistributionConnection)

                'Updating Preffered Cost
                Dim calValPref As Decimal
                StrSqlUpadate = ""
                StrSqlIUpadate = ""
                StrSqlUpadate = "UPDATE plantSPACE SET"
                For i = 0 To 5
                    'For Production highbay
                    If i = 0 Then
                        If UNITS = 0 Then
                            calValPref = CDbl(PrefLease(i) / Curr)
                            StrSqlIUpadate = StrSqlIUpadate + " PRODUCTIONLEASEHB" + " = " + calValPref.ToString() + ","
                        Else
                            calValPref = CDbl(PrefLease(i) * CONVAREA2 / Curr)
                            StrSqlIUpadate = StrSqlIUpadate + " PRODUCTIONLEASEHB" + " = " + calValPref.ToString() + ","
                        End If
                        'For Production Partial Highbay                       
                    ElseIf i = 1 Then
                        If UNITS = 0 Then
                            calValPref = CDbl(PrefLease(i) / Curr)
                            StrSqlIUpadate = StrSqlIUpadate + " PRODUCTIONLEASEPHB" + " = " + calValPref.ToString() + ","
                        Else
                            calValPref = CDbl(PrefLease(i) * CONVAREA2 / Curr)
                            StrSqlIUpadate = StrSqlIUpadate + " PRODUCTIONLEASEPHB" + " = " + calValPref.ToString() + ","
                        End If
                        'For Production Standard
                    ElseIf i = 2 Then
                        If UNITS = 0 Then
                            calValPref = CDbl(PrefLease(i) / Curr)
                            StrSqlIUpadate = StrSqlIUpadate + " PRODUCTIONLEASE" + " = " + calValPref.ToString() + ","
                        Else
                            calValPref = CDbl(PrefLease(i) * CONVAREA2 / Curr)
                            StrSqlIUpadate = StrSqlIUpadate + " PRODUCTIONLEASE" + " = " + calValPref.ToString() + ","
                        End If
                        'For Warehouse
                    ElseIf i = 3 Then
                        If UNITS = 0 Then
                            calValPref = CDbl(PrefLease(i) / Curr)
                            StrSqlIUpadate = StrSqlIUpadate + " WAREHOUSELEASE" + " = " + calValPref.ToString() + ","
                        Else
                            calValPref = CDbl(PrefLease(i) * CONVAREA2 / Curr)
                            StrSqlIUpadate = StrSqlIUpadate + " WAREHOUSELEASE" + " = " + calValPref.ToString() + ","
                        End If
                        'For Office
                    ElseIf i = 4 Then
                        If UNITS = 0 Then
                            calValPref = CDbl(PrefLease(i) / Curr)
                            StrSqlIUpadate = StrSqlIUpadate + " OFFICELEASE" + " = " + calValPref.ToString() + ","
                        Else
                            calValPref = CDbl(PrefLease(i) * CONVAREA2 / Curr)
                            StrSqlIUpadate = StrSqlIUpadate + " OFFICELEASE" + " = " + calValPref.ToString() + ","
                        End If
                        'For Support
                    ElseIf i = 5 Then
                        If UNITS = 0 Then
                            calValPref = CDbl(PrefLease(i) / Curr)
                            StrSqlIUpadate = StrSqlIUpadate + " SUPPORTLEASE" + " = " + calValPref.ToString() + ","
                        Else
                            calValPref = CDbl(PrefLease(i) * CONVAREA2 / Curr)
                            StrSqlIUpadate = StrSqlIUpadate + " SUPPORTLEASE" + " = " + calValPref.ToString() + ","
                        End If

                    End If


                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, DistributionConnection)

            Catch ex As Exception
                Throw New Exception("DistUpInsData:PlantConfig2Update:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub EnergyUpdate(ByVal CaseID As String, ByVal ERGPPREF() As String)
            Try

                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New DistGetData.Selectdata()

                'Preferences
                Dim dts As New DataSet()
                dts = ObjGetData.GetPref(CaseID)

                'Getting Values From Database assign to variable 
                Dim Convthick As String = dts.Tables(0).Rows(0).Item("convthick")
                Dim Convwt As String = dts.Tables(0).Rows(0).Item("convwt")
                Dim Curr As String = dts.Tables(0).Rows(0).Item("curr")
                Dim CONVAREA2 As String = dts.Tables(0).Rows(0).Item("CONVAREA2")
                Dim UNITS As String = dts.Tables(0).Rows(0).Item("units")
                Dim i As New Integer
                Dim StrSqlUpadate As String = ""
                Dim StrSqlIUpadate As String = ""
                Dim calValPref As Decimal
                StrSqlUpadate = "UPDATE plantENERGY SET"
                For i = 1 To 2

                    Dim MatVal As New Integer
                    MatVal = i + 1

                    If i = 1 Then
                        calValPref = CDbl(ERGPPREF(i - 1) / Curr)
                        StrSqlIUpadate = StrSqlIUpadate + " ELECPRICE" + " = " + calValPref.ToString() + ","
                    ElseIf i = 2 Then
                        calValPref = CDbl(ERGPPREF(i - 1) / Curr)
                        StrSqlIUpadate = StrSqlIUpadate + " NGASPRICE" + " = " + calValPref.ToString() + ","
                    End If
                Next


                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, DistributionConnection)

            Catch ex As Exception
                Throw New Exception("DistUpInsData:EnergyUpdate:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub CustomerInUpdate(ByVal CaseID As String, ByVal PRODPUR As String, ByVal SHIPDIST As String, ByVal MILCOST As String, ByVal SHIPTYPE As String)
            Try

                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New DistGetData.Selectdata()

                'Preferences
                Dim dts As New DataSet()
                dts = ObjGetData.GetPref(CaseID)

                'Getting Values From Database assign to variable 
                Dim Convthick2 As String = dts.Tables(0).Rows(0).Item("convthick2")
                Dim Convthick As String = dts.Tables(0).Rows(0).Item("convthick")
                Dim Convwt As String = dts.Tables(0).Rows(0).Item("convwt")
                Dim Curr As String = dts.Tables(0).Rows(0).Item("curr")
                Dim CONVAREA2 As String = dts.Tables(0).Rows(0).Item("CONVAREA2")
                Dim convthick3 As String = dts.Tables(0).Rows(0).Item("convthick3")
                Dim i As New Integer

                Dim StrSqlUpadate As String = ""
                Dim StrSqlIUpadate As String = ""


                'Updating Fixed Cost Guidelines values
                StrSqlUpadate = ""
                StrSqlIUpadate = ""
                Dim itemVal As Decimal
                StrSqlUpadate = "UPDATE customerIN SET"
                For i = 0 To 3
                    If i = 0 Then
                        itemVal = CDbl(PRODPUR / Curr * Convwt)
                        StrSqlIUpadate = StrSqlIUpadate + " M1" + " = " + itemVal.ToString() + ","
                    ElseIf i = 1 Then
                        itemVal = CDbl(SHIPDIST / convthick3)
                        StrSqlIUpadate = StrSqlIUpadate + " M2" + " = " + itemVal.ToString() + ","
                    ElseIf i = 2 Then
                        itemVal = CDbl(MILCOST / Curr * convthick3)
                        StrSqlIUpadate = StrSqlIUpadate + " M3" + " = " + itemVal.ToString() + ","
                    ElseIf i = 3 Then
                        StrSqlIUpadate = StrSqlIUpadate + " M4" + " = " + SHIPTYPE + ","
                    End If
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, DistributionConnection)

            Catch ex As Exception
                Throw New Exception("DistUpInsData:CustomerInUpdate:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub FixedCostUpdate(ByVal CaseID As String, ByVal FixedCostVal() As String, ByVal FixedCostPref() As String, ByVal Dept() As String, ByVal depreciate As String)
            Try

                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New DistGetData.Selectdata()

                'Preferences
                Dim dts As New DataSet()
                dts = ObjGetData.GetPref(CaseID)

                'Getting Values From Database assign to variable 
                Dim Convthick2 As String = dts.Tables(0).Rows(0).Item("convthick2")
                Dim Convthick As String = dts.Tables(0).Rows(0).Item("convthick")
                Dim Convwt As String = dts.Tables(0).Rows(0).Item("convwt")
                Dim Curr As String = dts.Tables(0).Rows(0).Item("curr")
                Dim CONVAREA2 As String = dts.Tables(0).Rows(0).Item("CONVAREA2")
                Dim i As New Integer

                Dim StrSqlUpadate As String = ""
                Dim StrSqlIUpadate As String = ""


                'Updating Fixed Cost Guidelines values
                StrSqlUpadate = ""
                StrSqlIUpadate = ""
                Dim intFixCostVal As New Integer
                Dim FixCostVal As Decimal
                StrSqlUpadate = "UPDATE fixedcostPCT SET"
                For i = 0 To 29
                    intFixCostVal = i + 1
                    If i = 0 Or i = 1 Or i = 4 Or i = 5 Or i = 6 Then
                        FixCostVal = CDbl(FixedCostVal(i) / Curr)
                        StrSqlIUpadate = StrSqlIUpadate + " M" + intFixCostVal.ToString() + " = " + FixCostVal.ToString() + ","
                    Else
                        StrSqlIUpadate = StrSqlIUpadate + " M" + intFixCostVal.ToString() + " = " + FixedCostVal(i) + ","
                    End If
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, DistributionConnection)


                'Updating Fixed Cost Preffered
                StrSqlUpadate = ""
                StrSqlIUpadate = ""
                Dim intCostPre As New Integer
                Dim PrefCostVal As Decimal
                StrSqlUpadate = "UPDATE fixedcostPRE SET"
                For i = 0 To 29
                    PrefCostVal = CDbl(FixedCostPref(i) / Curr)
                    intCostPre = i + 1

                    StrSqlIUpadate = StrSqlIUpadate + " M" + intCostPre.ToString() + " = " + PrefCostVal.ToString() + ","
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, DistributionConnection)


                'Updating  Department
                StrSqlUpadate = ""
                StrSqlIUpadate = ""
                Dim intDepId As New Integer
                StrSqlUpadate = "UPDATE fixedcostDEP SET"
                For i = 0 To 29
                    intDepId = i + 1
                    StrSqlIUpadate = StrSqlIUpadate + " M" + intDepId.ToString() + " = " + Dept(i).ToString() + ","
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, DistributionConnection)

                'Updating Depreciation
                StrSqlUpadate = ""
                StrSqlIUpadate = ""
                Dim wasteVal As New Integer
                StrSqlUpadate = "UPDATE depreciation SET years= " + depreciate.ToString()
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, DistributionConnection)




            Catch ex As Exception
                Throw New Exception("DistUpInsData:FixedCostUpdate:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub PalletUpdate(ByVal CaseID As String, ByVal Pallet() As String, ByVal Number() As String, ByVal NoOfUses() As String, ByVal PrefWeight() As String, ByVal PrefPrice() As String, ByVal Dept() As String, ByVal WNew() As String)
            Try

                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New DistGetData.Selectdata()

                'Preferences
                Dim dts As New DataSet()
                dts = ObjGetData.GetPref(CaseID)

                'Getting Values From Database assign to variable 
                Dim Convthick As String = dts.Tables(0).Rows(0).Item("convthick")
                Dim Convwt As String = dts.Tables(0).Rows(0).Item("convwt")
                Dim Curr As String = dts.Tables(0).Rows(0).Item("curr")
                Dim i As New Integer
                Dim StrSqlUpadate As String = ""
                Dim StrSqlIUpadate As String = ""
                StrSqlUpadate = "UPDATE PalletIN SET"
                For i = 0 To 9

                    Dim Palid As New Integer
                    Dim WeightPreffered As Decimal
                    Dim PRICEPreffered As Decimal
                    WeightPreffered = CDbl(PrefWeight(i) / Convwt)
                    PRICEPreffered = CDbl(PrefPrice(i) / Curr)
                    Palid = i + 1

                    'Pallet Item 
                    StrSqlIUpadate = StrSqlIUpadate + " M" + Palid.ToString() + " = " + Pallet(i).ToString() + ","
                    'Number
                    StrSqlIUpadate = StrSqlIUpadate + " T" + Palid.ToString() + " = " + Number(i).ToString() + ","
                    'Number Of uses
                    StrSqlIUpadate = StrSqlIUpadate + " R" + Palid.ToString() + " = " + NoOfUses(i).ToString() + ","
                    'Weight New
                    StrSqlIUpadate = StrSqlIUpadate + " NW" + Palid.ToString() + " = " + WNew(i).ToString() + ","
                    'Preffered Weight
                    StrSqlIUpadate = StrSqlIUpadate + " W" + Palid.ToString() + " = " + WeightPreffered.ToString() + ","
                    'Preffered price
                    StrSqlIUpadate = StrSqlIUpadate + " P" + Palid.ToString() + " = " + PRICEPreffered.ToString() + ","
                    'Dept
                    StrSqlIUpadate = StrSqlIUpadate + " D" + Palid.ToString() + " = " + Dept(i).ToString() + ","

                Next

                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""

                odbUtil.UpIns(StrSqlUpadate, DistributionConnection)

            Catch ex As Exception
                Throw New Exception("DistUpInsData:PalletUpdate:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub AdditionalEnergyUpdate(ByVal CaseID As String, ByVal EnergyConsum1() As String, ByVal EnergyConsum2() As String)
            Try

                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New DistGetData.Selectdata()

                'Preferences
                Dim dts As New DataSet()
                dts = ObjGetData.GetPref(CaseID)

                'Getting Values From Database assign to variable 
                Dim Convthick As String = dts.Tables(0).Rows(0).Item("convthick")
                Dim Convwt As String = dts.Tables(0).Rows(0).Item("convwt")
                Dim Curr As String = dts.Tables(0).Rows(0).Item("curr")
                Dim CONVAREA2 As String = dts.Tables(0).Rows(0).Item("CONVAREA2")
                Dim UNITS As String = dts.Tables(0).Rows(0).Item("units")
                Dim i As New Integer
                Dim StrSqlUpadate As String = ""
                Dim StrSqlIUpadate As String = ""

                StrSqlUpadate = "UPDATE SPACEENERGYPREFPERSQFT SET"
                For i = 0 To 2
                    Dim calVal As Double
                    calVal = CDbl(EnergyConsum1(i) * CONVAREA2)
                    Dim MatVal As New Integer
                    MatVal = i + 4
                    StrSqlIUpadate = StrSqlIUpadate + " P" + MatVal.ToString() + " = " + calVal.ToString() + ","

                Next
                For i = 7 To 10
                    Dim calVal As Double
                    calVal = CDbl(EnergyConsum2(i - 7) * CONVAREA2)
                    Dim MatVal As New Integer
                    If i <> 7 Then
                        MatVal = i + 2
                    Else
                        MatVal = i
                    End If

                    StrSqlIUpadate = StrSqlIUpadate + " P" + MatVal.ToString() + " = " + calVal.ToString() + ","

                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, DistributionConnection)



            Catch ex As Exception
                Throw New Exception("E2UpdateData:AdditionalEnergyUpdate:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub PalletInNewUpdate(ByVal CaseID As String, ByVal Col As String, ByVal Pallet() As String, ByVal Number() As String, ByVal NoOfUse() As String, ByVal PrefWeight() As String, ByVal PrefPrice() As String, ByVal Dept() As String)
            Try

                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New DistGetData.Selectdata()

                'Preferences
                Dim dts As New DataSet()
                dts = ObjGetData.GetPref(CaseID)

                'Getting Values From Database assign to variable 
                Dim curr As String = dts.Tables(0).Rows(0).Item("CURR")
                Dim Convwt As String = dts.Tables(0).Rows(0).Item("convwt")
                Dim Weight As Decimal
                Dim Price As Decimal
                Dim i As New Integer
                Dim StrSqlUpadate As String = ""
                Dim StrSqlIUpadate As String = ""




                '-----------------------------Pallet Item ----------------------------
                StrSqlUpadate = String.Empty
                StrSqlIUpadate = String.Empty
                StrSqlUpadate = "UPDATE PALLETINNEW SET "
                For i = 1 To 10
                    StrSqlIUpadate = StrSqlIUpadate + " " + Col + "" + i.ToString() + " = " + Pallet(i).ToString() + ","
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, DistributionConnection)
                '-----------------------------------------------------------------------------


                '-----------------------------Pallet Number --------------------------
                StrSqlUpadate = String.Empty
                StrSqlIUpadate = String.Empty
                StrSqlUpadate = "UPDATE PALLETINNUMBER SET "
                For i = 1 To 10
                    StrSqlIUpadate = StrSqlIUpadate + " " + Col + "" + i.ToString() + " = " + Number(i).ToString() + ","
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, DistributionConnection)
                '-----------------------------------------------------------------------------

                '-----------------------------Pallet Number Of Uses --------------------------
                StrSqlUpadate = String.Empty
                StrSqlIUpadate = String.Empty
                StrSqlUpadate = "UPDATE PALLETINONUSES SET "
                For i = 1 To 10
                    StrSqlIUpadate = StrSqlIUpadate + " " + Col + "" + i.ToString() + " = " + NoOfUse(i).ToString() + ","
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, DistributionConnection)
                '-----------------------------------------------------------------------------


                '-----------------------------Pallet Weight --------------------------
                StrSqlUpadate = String.Empty
                StrSqlIUpadate = String.Empty

                StrSqlUpadate = "UPDATE PALLETINWEIGHT SET "
                For i = 1 To 10
                    Weight = CDbl(PrefWeight(i)) / CDbl(Convwt)
                    StrSqlIUpadate = StrSqlIUpadate + " " + Col + "" + i.ToString() + " = " + Weight.ToString() + ","
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, DistributionConnection)
                '-----------------------------------------------------------------------------


                '-----------------------------Pallet Price --------------------------
                StrSqlUpadate = String.Empty
                StrSqlIUpadate = String.Empty

                StrSqlUpadate = "UPDATE PALLETINPRICE SET "
                For i = 1 To 10
                    Price = CDbl(PrefPrice(i)) / CDbl(curr)
                    StrSqlIUpadate = StrSqlIUpadate + " " + Col + "" + i.ToString() + " = " + Price.ToString() + ","
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, DistributionConnection)
                '-----------------------------------------------------------------------------
                '-----------------------------Pallet Department --------------------------
                StrSqlUpadate = String.Empty
                StrSqlIUpadate = String.Empty
                StrSqlUpadate = "UPDATE PALLETINDEPT SET "
                For i = 1 To 10
                    StrSqlIUpadate = StrSqlIUpadate + " " + Col + "" + i.ToString() + " = " + Dept(i).ToString() + ","
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, DistributionConnection)
                '-----------------------------------------------------------------------------

            Catch ex As Exception
                Throw New Exception("DistUpdateData:PalletInNewUpdate:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub TransportCustomerUpdate(ByVal CaseID As String, ByVal Col As String, ByVal TVAL() As String)
            Try

                'Creating Database Connection
                Dim dts As New DataSet
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New DistGetData.Selectdata()
                dts = ObjGetData.GetPref(CaseID)
                Dim Convthick As String = dts.Tables(0).Rows(0).Item("convthick")
                Dim Convthick3 As String = dts.Tables(0).Rows(0).Item("CONVTHICK3")
                Dim Convwt As String = dts.Tables(0).Rows(0).Item("Convwt")
                Dim Curr As String = dts.Tables(0).Rows(0).Item("CURR")
                Dim Unit As String = dts.Tables(0).Rows(0).Item("Units")
                Dim i As New Integer
                Dim StrSqlUpadate As String = ""
                Dim StrSqlIUpadate As String = ""
                StrSqlUpadate = "UPDATE TRNSPTCUSTOMER SET"

                For i = 1 To 3

                    Dim TValue As Decimal

                    If i = 1 Then
                        TValue = CDbl(TVAL(i) / Curr * Convwt)
                    ElseIf i = 2 Then
                        TValue = CDbl(TVAL(i).ToString() / Convthick3)
                    ElseIf i = 3 Then
                        TValue = CDbl(TVAL(i) / Curr * Convthick3)
                    End If
                    StrSqlIUpadate = StrSqlIUpadate + " " + Col + "" + i.ToString() + " = " + TValue.ToString() + ","
                Next


                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""

                odbUtil.UpIns(StrSqlUpadate, DistributionConnection)



            Catch ex As Exception
                Throw New Exception("DistUpInsData:TransportCustomerUpdate:" + ex.Message.ToString())
            End Try

        End Sub
#End Region

#Region "Results"
        Public Sub ResultsPl(ByVal CaseId As String, ByVal UnitPrice As String)
            Try
                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New DistGetData.Selectdata()
                'Preferences
                Dim dts As New DataSet()
                dts = ObjGetData.GetPref(CaseId)

                'Getting Values From Database assign to variable 
                Dim Convwt As String = dts.Tables(0).Rows(0).Item("convwt")
                Dim Curr As String = dts.Tables(0).Rows(0).Item("curr")
                Dim UnitPriceP As New Decimal
                Dim StrSqlUpadate As String = ""
                Dim StrSqlIUpadate As String = ""
                UnitPriceP = CDbl(UnitPrice) / Curr * Convwt
                StrSqlUpadate = "UPDATE RESULTSPL SET UNITPRICE=" + UnitPriceP.ToString() + " WHERE CASEID = " + CaseId.ToString() + ""
                odbUtil.UpIns(StrSqlUpadate, DistributionConnection)

            Catch ex As Exception
                Throw New Exception("DistUpInsData:ResultsPl:" + ex.Message.ToString())
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
                odbUtil.UpIns(StrSql, DistributionConnection)

                StrSql = "UPDATE NOTES  "
                StrSql = StrSql + "SET NOTE = '" + Notes + "' "
                StrSql = StrSql + "WHERE CASEID=  " + CaseId + "  "
                StrSql = StrSql + "AND ASSUMPTIONTYPE='" + AssumptionCode + "' "
                odbUtil.UpIns(StrSql, DistributionConnection)

            Catch ex As Exception
                Throw New Exception("DistUpInsData:NotesUpdate:" + ex.Message.ToString())
            End Try

        End Sub
#End Region
#Region "Wizard"
        Public Function UpdateWizard(ByVal CaseId As String) As Integer
            Dim WSessionID As New Integer
            Dim odbUtil As New DBUtil()
            Dim ObjGetData As New DistGetData.Selectdata()
            Dim ds As New DataSet()
            Try
                ds = ObjGetData.GetSessionWizardId()
                WSessionID = CInt(ds.Tables(0).Rows(0).Item("SessionId").ToString())
                Dim StrSqlUpadate As String = "INSERT INTO SESSIONVALUES SELECT " + WSessionID.ToString() + "," + CaseId.ToString() + " FROM DUAL"
                odbUtil.UpIns(StrSqlUpadate, DistributionConnection)
            Catch ex As Exception
                Throw (New Exception("DistUpInsData:UpdateWizard:" + ex.Message.ToString()))
            End Try
            Return WSessionID
        End Function
#End Region

    End Class

End Class
